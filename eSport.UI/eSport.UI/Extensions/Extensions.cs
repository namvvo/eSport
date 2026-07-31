using eSport.UI.Components.Account;
using eSport.UI.Data;
using eSport.UI.Shared.Infrastructure.State;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.Net.Http.Headers;

namespace eSport.UI.Extensions;

public static class Extensions
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        builder.AddAuthenticationServices();

        //builder.AddRabbitMqEventBus("EventBus")
        //       .AddEventBusSubscriptions();

        //builder.Services.AddHttpForwarderWithServiceDiscovery();

        // Application services
        //builder.Services.AddScoped<BasketState>();
        //builder.Services.AddScoped<LogOutService>();
        //builder.Services.AddSingleton<BasketService>();
        //builder.Services.AddSingleton<OrderStatusNotificationService>();
        //builder.Services.AddSingleton<IProductImageUrlProvider, ProductImageUrlProvider>();
        //builder.AddAIServices();

        //// HTTP and GRPC client registrations
        //builder.Services.AddGrpcClient<Basket.BasketClient>(o => o.Address = new("http://basket-api"))
        //    .AddAuthToken();

        //builder.Services.AddHttpClient<CatalogService>(o => o.BaseAddress = new("http://catalog-api"))
        //    .AddAuthToken();

        //builder.Services.AddHttpClient<OrderingService>(o => o.BaseAddress = new("http://ordering-api"))
        //    .AddAuthToken();
    }
    public static void AddAuthenticationServices(this IHostApplicationBuilder builder)
    {
        var configuration = builder.Configuration;
        var services = builder.Services;

        builder.Services.AddScoped<IdentityRedirectManager>();
        builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultScheme = IdentityConstants.ApplicationScheme;
            options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
        })
            .AddIdentityCookies();
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();




        //JsonWebTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");

        //var identityUrl = configuration.GetRequiredValue("IdentityUrl");
        //var callBackUrl = configuration.GetRequiredValue("CallBackUrl");
        //var sessionCookieLifetime = configuration.GetValue("SessionCookieLifetimeMinutes", 60);

        // Add Authentication services
        services.AddAuthorization();

        builder.Services.AddIdentityCore<ApplicationUser>(options =>
        {
            options.SignIn.RequireConfirmedAccount = true;
            options.Stores.SchemaVersion = IdentitySchemaVersions.Version3;
        })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

        
        //builder.Services.AddAuthentication(options =>
        //{
        //    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        //    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
        //})
        //.AddCookie(options => options.ExpireTimeSpan = TimeSpan.FromMinutes(sessionCookieLifetime))
        //.AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
        //{

        //    options.Authority = "https://localhost:5000";

        //    options.ClientId = "interactiveblazor";
        //    options.ClientSecret = "49C1A7E1-0C79-4A89-A3D6-A37998FB86B0";
        //    options.ResponseType = "code";
        //    options.SaveTokens = true;

        //    options.Scope.Clear();
        //    options.Scope.Add("openid");
        //    options.Scope.Add("profile");
        //    options.Scope.Add("scope1");
        //    options.Scope.Add("offline_access");
        //    options.Scope.Add("verification");
        //    options.Scope.Add("roles");

        //    options.Scope.Add("gender");
        //    options.Scope.Add("country");
        //    options.Scope.Add("birthdate");
        //    options.Scope.Add("aboutyou");

        //    options.GetClaimsFromUserInfoEndpoint = true;
        //    options.ClaimActions.MapAll();
        //    ///options.ClaimActions.MapUniqueJsonKey("favorite_color", "favorite_color");

        //    options.MapInboundClaims = false; // Don't rename claim types


        //});
        builder.Services.AddSingleton<CurrentSeasonState>();
        //builder.Services.AddHttpContextAccessor();
        //builder.Services.AddHttpClients();

        builder.Services
           .AddeSportClient()
           .ConfigureHttpClient(client => client.BaseAddress = new Uri(builder.Configuration["GraphQL:Endpoint"]!));

        builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();


        // Blazor auth services

        services.AddScoped<AuthenticationStateProvider, ServerAuthenticationStateProvider>();
        services.AddCascadingAuthenticationState();
    }
    //public static void AddHttpClients(this IServiceCollection services)
    //{
    //    // connect API from ApiGateway
    //    //services.AddHttpClient("APIGateway", client =>
    //    //{
    //    //    client.BaseAddress = new Uri("https://localhost:6501/");
    //    //    client.DefaultRequestHeaders.Clear();
    //    //    client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
    //    //})
    //    //   .AddHttpMessageHandler<AuthenticationDelegatingHandler>();

    //    ////directly connect to API
    //    //services.AddHttpClient("WeatherForecastAPIClient", client =>
    //    //{
    //    //    client.BaseAddress = new Uri("https://localhost:6001/");
    //    //    client.DefaultRequestHeaders.Clear();
    //    //    client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
    //    //})
    //    //    .AddHttpMessageHandler<AuthenticationDelegatingHandler>();

    //    // added for get user info
    //    services.AddHttpClient("IDPClient", client =>
    //    {
    //        client.BaseAddress = new Uri("https://localhost:5000/");
    //        client.DefaultRequestHeaders.Clear();
    //        client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
    //    });
    //}

}

