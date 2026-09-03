using Amazon.Runtime;
using Amazon.S3;
using eSport.ServiceDefaults.APIExtensions;
using Media.API.Infrastructure;
using Media.API.Options;
using Media.API.Services;
using Microsoft.Extensions.Options;

public static class Extensions
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        if (builder.Environment.IsBuild())
        {
            builder.Services.AddDbContext<MediaDbContext>();
            return;
        }

        builder.AddNpgsqlDbContext<MediaDbContext>("mediadb");
        builder.Services.AddMigration<MediaDbContext>();

        builder.Services
            .AddOptions<R2Options>()
            .BindConfiguration(R2Options.SectionName)
            .Validate(x => !string.IsNullOrWhiteSpace(x.AccountId), "R2:AccountId is required")
            .Validate(x => !string.IsNullOrWhiteSpace(x.AccessKeyId), "R2:AccessKeyId is required")
            .Validate(x => !string.IsNullOrWhiteSpace(x.SecretAccessKey), "R2:SecretAccessKey is required")
            .Validate(x => !string.IsNullOrWhiteSpace(x.BucketName), "R2:BucketName is required")
            //.Validate(x => Uri.TryCreate(x.PublicBaseUrl, UriKind.Absolute, out _), "R2:PublicBaseUrl must be an absolute URL")
            .ValidateOnStart();

        builder.Services.AddSingleton<IAmazonS3>(sp =>
        {
            var r2 = sp.GetRequiredService<IOptions<R2Options>>().Value;
            var credentials = new BasicAWSCredentials(r2.AccessKeyId, r2.SecretAccessKey);
            var config = new AmazonS3Config
            {
                ServiceURL = $"https://{r2.AccountId}.r2.cloudflarestorage.com",
                AuthenticationRegion = "auto",
                ForcePathStyle = true
            };
            return new AmazonS3Client(credentials, config);
        });

        builder.Services.AddScoped<IMediaObjectStorage, R2StorageService>();
        builder.Services.AddSingleton<MediaUrlGenerator>();
        builder.Services
            .AddGraphQLServer("Media")
            .AddQueryType()
            .AddMutationType()
            .AddMediaTypes();
    }
}
