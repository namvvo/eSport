using Amazon.Runtime;
using Amazon.S3;
using Media.API.Services;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



// Configure the HTTP request pipeline.



var r2 = builder.Configuration.GetSection("R2");
builder.Services.AddSingleton<IAmazonS3>(_ =>
{
    var credentials = new BasicAWSCredentials(
        r2["AccessKeyId"]!,
        r2["SecretAccessKey"]!);

    var config = new AmazonS3Config
    {
        ServiceURL =
            $"https://{r2["AccountId"]}.r2.cloudflarestorage.com",

        AuthenticationRegion = "auto"
    };

    return new AmazonS3Client(credentials, config);
});
builder.Services.AddScoped<R2StorageService>();
var app = builder.Build();
app.UseHttpsRedirection();

app.MapPost("/test/r2/upload", async (
    HttpRequest request,
    R2StorageService storage,
    CancellationToken ct) =>
{
    var form = await request.ReadFormAsync(ct);

    if (form.Files.Count == 0)
    {
        return Results.BadRequest(new
        {
            message = "No file received",
            fileCount = form.Files.Count
        });
    }

    var file = form.Files[0];

    var key = $"player/{Guid.NewGuid():N}-{file.FileName}";

    await using var stream = file.OpenReadStream();

    var etag = await storage.UploadAsync(
        stream,
        key,
        file.ContentType,
        ct);

    return Results.Ok(new
    {
        key,
        fileName = file.FileName,
        contentType = file.ContentType,
        size = file.Length,
        etag
    });
})
.DisableAntiforgery(); 

app.Run();
