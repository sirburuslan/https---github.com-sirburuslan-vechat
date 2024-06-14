/*
 * @class User Options Update Repository
 *
 * @author Ruslan Sirbu
 * @version 0.0.1
 * @updated 2024-05-22
 *
 * This class is used to update the user's options
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
/// Users Options Update Repository
/// </summary>
/// <param name="db">Database connection</param>
/// <param name="memoryCache">Usery cache instance</param>
/// <param name="logger">Logger instance</param>
public class UpdateRepository(MongoDb db, IMemoryCache memoryCache, ILogger logger)
{

    /// <summary>
    /// Update bulk options
    /// </summary>
    /// <param name="optionsList">Users options list</param>
    /// <returns>Boolean response</returns>
    public async Task<bool> UpdateOptionsAsync(List<UserOptionDto> optionsList)
    {
        Console.WriteLine(memoryCache);
        try
        {

            List<WriteModel<UserOptionEntity>> optionEntities = [];

            // List the properties
            for (int p = 0; p < optionsList.Count; p++)
            {

                // Create the option
                UserOptionEntity option = new()
                {
                    OptionId = optionsList[p].OptionId,
                    UserId = optionsList[p].UserId,
                    OptionName = optionsList[p].OptionName,
                    OptionValue = optionsList[p].OptionValue
                };

                // Create the filters for conditions scope
                var filter = Builders<UserOptionEntity>.Filter.Eq(m => m.OptionId, option.OptionId);

                // Prepare the update data
                var update = Builders<UserOptionEntity>.Update
                    .Set(m => m.UserId, option.UserId)
                    .Set(m => m.OptionName, option.OptionName)
                    .Set(m => m.OptionValue, option.OptionValue);

                // Add data to the list
                optionEntities.Add(new UpdateOneModel<UserOptionEntity>(filter, update) { IsUpsert = true });
            }

            // Try to update
            await db.UsersOptions.BulkWriteAsync(optionEntities);

            return true;

        }
        catch (Exception ex)
        {

            // Logging the exception
            logger.LogError(ex, "An error occurred while user reading options.");

            return false;

        }

    }

}