using eSport.Catalog.API.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace eSport.Catalog.API.GraphQL.SeasonStages;

[QueryType]
public static partial class SeasonStageQueries
{

    [UseFirstOrDefault]
    [UseProjection]   // Step 2: Project required fields

    public static IQueryable<Stage?> GetStageById(CatalogContext db,
                                          int id)
    => db.Stages.AsNoTracking().Where(s => s.Id == id);

    //public static async Task<SeasonStage?> GetCurrentSeasonStageByCategory(
    //    [Service] ICatalogService service,
    //    int categoryId,
    //    CancellationToken ct)
    //{
    //    return await service.GetCurrentSeasonStageByCategoryAsync(categoryId, ct);
    //}
    public static async Task<LatestSeasonByCategoryDto> LatestSeasonsByCategory([Service] ICatalogService _categoryService,
                                                                            [Service] ISeasonStageService _seasonStageService,
                                                                            string slug,
                                                                            int seasonStageId = 0,
                                                                            CancellationToken ct = default)
    {


        var category = await _categoryService.GetCategoryBySlugAsync(slug);
        if (category == null) throw new Exception("Category not found");
        int categoryId = category.Id;
        int seasonId = 0, stageId = 0;
        //var key = _staticCacheManager.PrepareKeyForDefaultCache(
        //                              NopDataCacheKeys.GET_LASTEST_SEASONSTAGE_BY_CATEGORY_SEASON_STAGE,
        //                              slug,
        //                              seasonId,
        //                              stageId);
        //var cacheItems = await _staticCacheManager.GetAsync(key, async () =>
        //{


        try
        {
            SeasonStage? seasonStage = new();
            if (seasonStageId > 0)
            {
                seasonStage = await _seasonStageService.GetSeasonStageMappingByIdAsync(seasonStageId, false, ct);
                if (seasonStage != null)
                {
                    seasonId = seasonStage.SeasonId;
                    stageId = seasonStage.StageId;
                }
                else throw new Exception("SeasonStage not found");
            }

            var allSeasonStagesByCategory = await _seasonStageService.GetCategorySeasonStagesAsync(categoryId);
            //if (!latestSeasonStages.Any()) return new LatestSeasonByCategoryModel();
            var orderedSeasonStages = allSeasonStagesByCategory.OrderByDescending(o => o.Year2); // most latest seasons
            var seasonStageModels = orderedSeasonStages.GroupBy(g => g.SeasonStageId);

            var currentSeasonStage = seasonStageId > 0 ? seasonStage :
                                     await _seasonStageService.GetCurrentSeasonStageByCategoryInTournamentAsync(categoryId, ct);  // cover both tournament and domestic

            if (currentSeasonStage == null) return new();
            var model = new LatestSeasonByCategoryDto()
            {
                CurrentSeasonStage = new SeasonStageDto()
                {
                    SeasonId = currentSeasonStage.SeasonId,
                    SeasonStageId = currentSeasonStage.Id,
                    StageId = currentSeasonStage.StageId
                }

            };



            #region categories

            var categories = await _categoryService.GetCategoriesAsync(true);
            var categoriesByCountry = categories.Where(w => w.CountryId == category.CountryId).ToList();  // all leagues in 1 country eg: bundesliga bundesliga 2...

            if (categoriesByCountry.Any())
            {
                foreach (var c in categoriesByCountry)
                {
                    //var country = 
                    var categoryModel = new CategoryModel()
                    {
                        Id = c.Id,
                        Name = c.Name,
                        //Country = c.CountryId > 0 ? new CountryModel()
                        //{
                        //    ThreeLetterIsoCode = country.ThreeLetterIsoCode,
                        //    Name = country.Name,
                        //    CountryCSS = country.TwoLetterIsoCode
                        //}; : null,
                        IsActive = c.Id == category.Id,
                        IsTournament = c.IsTournament,
                        SeName = c.SeName
                    };
                    if (String.IsNullOrWhiteSpace(categoryModel.SeName)) continue;

                    if (categoryModel.Country == null)
                        categoryModel.Country = new CountryModel()
                        {
                            CountryCSS = c.GroupName
                        };


                    //var bannerKey = _staticCacheManager.PrepareKeyForDefaultCache(NopDataCacheKeys.GET_PICTURE_BY_ID, c.PictureId);


                    //categoryModel.Banner = await _commonModelFactory.PreparePictureModel(c.PictureId, 0, 0);
                    //await _staticCacheManager.GetAsync(bannerKey, async () =>
                    //{
                    //    return await _mediaService.GetPictureUrlAsync(category.PictureId);
                    //});

                    //var picture = await _mediaService.GetPictureByIdAsync(category.PictureId);
                    //string imageUrl;

                    //(imageUrl, _) = await _mediaService.GetPictureUrlAsync(picture);
                    //categoryModel.Banner.ImageUrl = await _staticCacheManager.GetAsync(key, async () =>
                    //{
                    //    return await _mediaService.GetPictureUrlAsync(category.PictureId);
                    //});

                    model.Categories.Add(categoryModel);
                }
            }
            #endregion
            #region stages
            var stages = await _seasonStageService.GetStagesByCategoryAsync(categoryId, ct);
            foreach (var s in stages)
                model.Stages.Add(new StageDto()
                {
                    Id = s.Id,
                    Name = s.Name,
                    IsActive = s.Id == model.CurrentSeasonStage.StageId,
                    Display = s.Display
                });
            foreach (var ss in allSeasonStagesByCategory)
            {
                model.Seasons.Add(new SeasonDto()
                {
                    Id = ss.SeasonId,
                    Year = ss.Year,
                    //IsActive = ss.SeasonId == model.CurrentSeasonStage.SeasonId,
                    Year2 = ss.Year2,
                    //DisplayOrder = ss.Key.DisplayOrder
                });
            }

            #endregion
            #region seasons
            var season = await _seasonStageService.GetSeasonByIdAsync(model.CurrentSeasonStage.SeasonId);
            model.CurrentSeasonStage.Year = season.Year;
            var yearPairs = season.Year.Split("/");
            if (yearPairs is not null)
            {
                int year = 2000 + int.Parse(yearPairs[0]);
                model.CurrentSeasonStage.SeasonDateLimit.Add(new DateTime(year, category.FromMonth, 1));
                year = 2000 + int.Parse(yearPairs[1]);
                int days = DateTime.DaysInMonth(year, category.ToMonth);
                model.CurrentSeasonStage.SeasonDateLimit.Add(new DateTime(year, category.ToMonth, days));
            }
            if (model.Seasons != null && model.Seasons.Any())
            {

                var latestSeason = model.Seasons.OrderByDescending(o => o.Id).FirstOrDefault();
                if (latestSeason != null) latestSeason.IsActive = true;

                //var seasonStages = latestSeasonStages.Where(w => w.SeasonStageId == model.CurrentSeasonStage.SeasonStageId);
                //foreach (var ss in seasonStages)
                //{
                //    if (ss.TeamId > 0)  // reconsider the reasons
                //    {
                //        var team = _teamService.GetById(ss.TeamId);
                //        var simpleTeam = await playerModelFactory.PrepareTeamSimpleModelAsync(team, false);
                //        model.Teams.Add(simpleTeam);

                //    }

                //}
            }
            #endregion

            return model;
        }
        catch (Exception e)
        {

            throw new Exception(e.Message);
        }

        //});
        //return cacheItems;
    }

}
