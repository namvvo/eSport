
using eSport.Catalog.API.Grpc;

[assembly: Module("CatalogTypes")]
var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddApplicationServices();
builder.Services.AddGrpc();
var app = builder.Build();
app.MapGrpcService<SeasonStageGrpcEndpoint>();
app.MapGraphQL();
#region updatesename
//using (var scope = app.Services.CreateScope())
//{
//    var db = scope.ServiceProvider.GetRequiredService<CatalogContext>();

//    var categories = db.Categories.Where(c => c.SeName == null || c.SeName == "").ToList();
//    foreach (var category in categories)
//    {
//        UpdateSeName(category.Id, db);
//    }

//}
//static void UpdateSeName(int categoryId, CatalogContext db)
//{
//    var entity = db.Categories.Find(categoryId);
//    if (entity is null || !String.IsNullOrWhiteSpace(entity.SeName)) return;
//    string slug = entity.Name.RemoveSign4VietnameseString().Trim().Replace(' ', '-');
//    slug = slug.Replace("---", "-").Replace("--", "-").RemoveIllegalCharacter();
//    slug = ValidateSlug(slug, db).ToLower();

//    entity.SeName = slug;
//    db.SaveChanges();
//}

/// <summary>
/// Validate slug and ensure it is unique for the given entity name. If the slug already exists, append a number to make it unique.
/// </summary>
/// <param name="slug"></param>
/// <param name="entityName"></param>
/// <returns></returns>
/// <exception cref="ArgumentNullException"></exception>
//static string ValidateSlug(string slug, CatalogContext db)
//{
//    if (String.IsNullOrWhiteSpace(slug))
//        throw new ArgumentNullException("no slug");

//    ////max length
//    //seName = CommonHelper.EnsureMaximumLength(seName, 400);                        

//    int i = 2;
//    var tempSeName = slug;
//    while (true)
//    {
//        //check whether such slug already exists (and that is not the current product)
//        var urlRecord = db.Categories.AsNoTracking().FirstOrDefault(f => f.SeName.Equals(tempSeName));
//        if (urlRecord == null) break;

//        tempSeName = string.Format("{0}-{1}", slug, i);
//        i++;
//    }
//    slug = tempSeName;

//    return slug;
//}
#endregion
app.Run();
await app.RunWithGraphQLCommandsAsync(args);