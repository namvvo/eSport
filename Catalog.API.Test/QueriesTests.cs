
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;

//using eSport.Catalog.API.GraphQL.Dto;
//using eSport.Catalog.API.Infrastructure;
//using eSport.Catalog.API.Models;
//using eSport.Catalog.API.Services;
//using eSport.Catalog.API.GraphQL.Categories;

//namespace Catalog.API.Test.Tests
//{
//    public class QueriesTests
//    {
//        private CatalogContext CreateContext()
//        {
//            var options = new DbContextOptionsBuilder<CatalogContext>()
//                .UseInMemoryDatabase(Guid.NewGuid().ToString())
//                .Options;
//            var config = new ConfigurationBuilder().Build();
//            return new CatalogContext(options, config);
//        }

//        private Category MakeCategory(string name)
//            => new Category
//            {
//                Name = name,
//                ParentCategoryId = 0,
//                Rounds = 0,
//                SeName = name.ToLowerInvariant(),
//                SofaScoreId = 0,
//                CountryId = 0,
//                Published = true,
//                DisplayOrder = 0,
//                FromMonth = 0,
//                ToMonth = 0,
//                PictureId = 0,
//                PageSize = 10,
//                ShowOnHomePage = false,
//                IncludeInTopMenu = false,
//                Deleted = false,
//                CreatedOnUtc = DateTime.UtcNow,
//                UpdatedOnUtc = DateTime.UtcNow,
//                IsTournament = false,
//                IsData = false
//            };

//        [Fact]
//        public void GetCategories_ReturnsAllCategories()
//        {
//            using var db = CreateContext();
//            db.Categories.Add(MakeCategory("A"));
//            db.Categories.Add(MakeCategory("B"));
//            db.SaveChanges();

//            var queries = new CategoryQueries();
//            var result = queries.GetCategories(db).ToList();

//            Assert.Equal(2, result.Count);
//            Assert.Contains(result, c => c.Name == "A");
//            Assert.Contains(result, c => c.Name == "B");
//        }

//        //[Fact]
//        //public void GetCategoryById_FiltersById()
//        //{
//        //    using var db = CreateContext();
//        //    var one = MakeCategory("One");
//        //    var two = MakeCategory("Two");
//        //    db.Categories.Add(one);
//        //    db.Categories.Add(two);
//        //    db.SaveChanges();

//        //    // retrieve the assigned Ids
//        //    var id = db.Categories.OrderBy(c => c.Name).First(c => c.Name == "One").Id;

//        //    var queries = new CategoryQueries();
//        //    var queryable = queries.GetCategoryById(db, id);
//        //    var result = queryable.SingleOrDefault();

//        //    Assert.NotNull(result);
//        //    Assert.Equal("One", result!.Name);
//        //}

//        //[Fact]
//        //public async Task GetStageById_ReturnsStage()
//        //{
//        //    using var db = CreateContext();
//        //    var stage = new Stage
//        //    {
//        //        Name = "Stage1",
//        //        DisplayOrder = 1,
//        //        Round = 1,
//        //        Display = true
//        //    };
//        //    db.Stages.Add(stage);
//        //    db.SaveChanges();

//        //    var id = db.Stages.First().Id;

//        //    var queries = new CategoryQueries();
//        //    var result =  queries.GetStageById(db, id).SingleOrDefault();

//        //    Assert.NotNull(result);
//        //    Assert.Equal("Stage1", result!.Name);
//        //}

//        private class FakeCatalogService : ICatalogService
//        {
//            private readonly IList<CategoryMenuDto> _items;
//            public FakeCatalogService(IList<CategoryMenuDto> items) => _items = items;
//            public Task<IList<CategoryMenuDto>> GetMenuCategoriesAsync(CancellationToken ct) => Task.FromResult(_items);
//            public Task<SeasonStage?> GetCurrentSeasonStageByCategoryAsync(int categoryId, CancellationToken ct) => Task.FromResult<SeasonStage?>(null);
//            public Task<IEnumerable<Stage>> GetStagesByCategoryAsync(int categoryId, CancellationToken ct) => Task.FromResult<IEnumerable<Stage>>(Array.Empty<Stage>());
//        }

//        [Fact]
//        public async Task GetMenuCategories_UsesService()
//        {
//            var expected = new List<CategoryMenuDto>
//            {
//                new CategoryMenuDto { Id = 1, Name = "Menu1", SeName = "menu1" }
//            };

//            var service = new FakeCatalogService(expected);
//            var queries = new CategoryQueries();
//            var result = await queries.GetMenuCategories(service, CancellationToken.None);

//            Assert.Same(expected, result);
//            Assert.Single(result);
//            Assert.Equal("Menu1", result[0].Name);
//        }
//    }
//}
