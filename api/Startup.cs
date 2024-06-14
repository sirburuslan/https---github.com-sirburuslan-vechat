/*
 * @class Startup
 *
 * @author Ruslan Sirbu
 * @version 0.0.1
 * @updated 2024-04-15
 *
 * This class contains the configuration of services and apps
 */

// System Namespaces
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

// App Namespaces
using api.Options;
using api.Services.Extensions;
using api.Utilities.Db;
using api.Utilities.Swagger;

// App namespace
namespace VeChat;

/// <summary>
/// Startup Class
/// </summary>
/// <param name="configuration">App Configuration</param>
/// <param name="env">Information about the web hosting environment</param>
public class Startup(IConfiguration configuration, IWebHostEnvironment env)
{
    /// <summary>
    /// Configuration container
    /// </summary>
    public IConfiguration Configuration { get; } = configuration;

    /// <summary>
    /// Environment container
    /// </summary>
    public IWebHostEnvironment Environment { get; } = env;

    /// <summary>
    /// Configure the app services
    /// </summary>
    /// <param name="services">Collection with services</param>
    public void ConfigureServices(IServiceCollection services)
    {
        // Bind app settings to the app settings class
        services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

        // Add app settings class in the IOptions
        services.AddSingleton(provider => provider.GetRequiredService<IOptions<AppSettings>>().Value);

        // Add the Cors Service
        services.AddCors(options =>
        {
            options.AddPolicy(name: "MainPolicy",
            policy =>
            {
                policy.WithOrigins(Configuration.GetValue<string>("AppSettings:SiteUrl") ?? string.Empty);
                policy.AllowAnyMethod();
                policy.AllowAnyHeader();
            });
        });

        // Set rules for routes
        services.AddRouting(options =>
        {
            // All urls will be lowercase
            options.LowercaseUrls = true;
        });

        // Register MongoDb database
        services.AddSingleton<MongoDb>(_ => new MongoDb("mongodb://localhost:27017/VeChat", "VeChat"));

        // Register the library for cache storing
        services.AddMemoryCache();

        // Add the Api Versioning in url support
        services.AddApiVersioning();

        // Register Repositories
        services.AddRepositories();

        // Configure the session state
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = Configuration.GetValue<string>("AppSettings:JwtSettings:Issuer"),
                ValidAudience = Configuration.GetValue<string>("AppSettings:JwtSettings:Audience"),
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetValue<string>("AppSettings:JwtSettings:Key") ?? string.Empty))
            };
        });

        // Enable the Controllers support
        services.AddControllers(options =>
        {
            options.Filters.Add<JsonExceptionFilter>();
        });

        // Run code only for development
        if (Environment.IsDevelopment())
        {
            // Add Api Explorer Service
            services.AddEndpointsApiExplorer();

            // Add Swagger Support
            services.AddSwaggerGen(c =>
            {
                // Generate unique Ids
                c.CustomSchemaIds(type => type.FullName);

                // Configure SwashBuckle to generate a documentation for v1
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "VeChat Api", Version = "v1" });

                // Define a custom filter to remove URLs without a version
                c.DocumentFilter<Filter>();
            });
        }
    }

    /// <summary>
    /// Configure the HTTP request pipeline
    /// </summary>
    /// <param name="app">Application's request pipeline</param>
    public void Configure(IApplicationBuilder app)
    {
        // Use the available routes
        app.UseRouting();

        // Use the Main Cors Policy
        app.UseCors("MainPolicy");

        // Run code only for development
        if (Environment.IsDevelopment())
        {
            // Activate the Swagger middleware
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // List all endpoints and map the controllers
        app.UseEndpoints(
            endpoints => endpoints.MapControllers()
        );
    }
}
