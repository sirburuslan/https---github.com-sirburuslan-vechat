/*
 * @class ServiceCollectionExtensions
 *
 * @author Ruslan Sirbu
 * @version 0.0.1
 * @updated 2024-04-18
 *
 * This class is used to inject the repositories as dependencies
 */

// System Utils
using Microsoft.AspNetCore.Identity;

// App Utils
using api.Models.Entities.Users;
using api.Repositories.Users;
using api.Repositories.Settings;
using api.Services.Interfaces.Users;
using api.Services.Interfaces.Settings;

// Namespace for Extensions
namespace api.Services.Extensions;

/// <summary>
/// ServiceCollectionExtensions Class
/// </summary>
public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<ISettingsRepository, SettingsRepository>();
        services.AddTransient<IPasswordHasher<UserEntity>, PasswordHasher<UserEntity>>();
        return services;
    }
}
