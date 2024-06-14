/*
 * @class User Options Create Repository
 *
 * @author Ruslan Sirbu
 * @version 0.0.1
 * @updated 2024-05-21
 *
 * This class is used to create the user's options
 */

// System Utils
using Microsoft.Extensions.Caching.Memory;
using MongoDB.Bson;
using MongoDB.Driver;


// App Utils
using api.Models.Dtos;
using api.Models.Entities.Users;
using api.Utilities;
using api.Utilities.Db;

// Namespace for Users Options Repositories
namespace api.Repositories.Users.Options;

/// <summary>
/// Users Options Create Repository
/// </summary>
/// <param name="db">Database connection</param>
/// <param name="memoryCache">Usery cache instance</param>
/// <param name="logger">Logger instance</param>
public class CreateRepository(MongoDb db, IMemoryCache memoryCache, ILogger logger)
{

    /// <summary>
    /// Save bulk options
    /// </summary>
    /// <param name="optionsList">Users options list</param>
    /// <returns>Boolean response</returns>
    public async Task<bool> SaveOptionsAsync(List<UserOptionDto> optionsList)
    {
        Console.WriteLine(memoryCache);
        try
        {
            // Add range with options
            await db.UsersOptions.InsertManyAsync(optionsList.Select(o => new UserOptionEntity
            {
                UserId = o.UserId,
                OptionName = o.OptionName,
                OptionValue = o.OptionValue
            }));

            return true;

        }
        catch (Exception ex)
        {

            // Logging the exception
            logger.LogError(ex, "An error occurred while user saving options.");

            return false;

        }

    }

}