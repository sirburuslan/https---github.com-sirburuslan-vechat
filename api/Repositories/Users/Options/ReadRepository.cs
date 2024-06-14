/*
 * @class User Options Read Repository
 *
 * @author Ruslan Sirbu
 * @version 0.0.1
 * @updated 2024-05-20
 *
 * This class is used to read the user's options
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
/// Users Options Read Repository
/// </summary>
/// <param name="db">Database connection</param>
/// <param name="memoryCache">Usery cache instance</param>
/// <param name="logger">Logger instance</param>
public class ReadRepository(MongoDb db, IMemoryCache memoryCache, ILogger logger)
{

    /// <summary>
    /// Get the options list
    /// </summary>
    /// <param name="userId">User's ID</param>
    /// <returns>Get the options list</returns>
    public async Task<ResponseDto<List<UserOptionDto>>> OptionsListAsync(ObjectId userId)
    {

        try
        {

            // Create the cache key
            string cacheKey = "vc_user_options_" + userId;

            // Verify if the options are saved in the cache
            if (!memoryCache.TryGetValue(cacheKey, out List<UserOptionDto>? userOptions))
            {

                // Get all options
                FilterDefinition<UserOptionEntity> filterDefinition = Builders<UserOptionEntity>.Filter.Eq(m => m.UserId, userId);

                // Apply the definition filter to the read query
                userOptions = await db.UsersOptions.Find(filterDefinition).Project(o => new UserOptionDto
                {
                    OptionId = o.OptionId,
                    UserId = o.UserId,
                    OptionName = o.OptionName,
                    OptionValue = o.OptionValue
                }).ToListAsync();

                // Create the cache options for storing
                MemoryCacheEntryOptions cacheOptions = new()
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1)
                };

                // Save the request in the cache
                //_memoryCache.Set(cacheKey, optionsEntities, cacheOptions);

            }

            // Verify if options exists
            if (userOptions?.Count > 0)
            {

                // Return the response
                return new ResponseDto<List<UserOptionDto>>
                {
                    Result = userOptions,
                    Message = null
                };

            }
            else
            {

                // Return the response
                return new ResponseDto<List<UserOptionDto>>
                {
                    Result = null,
                    Message = Words.Get("NoOptionsFound")
                };

            }

        }
        catch (Exception ex)
        {

            // Logging the exception
            logger.LogError(ex, "An error occurred while user reading options.");

            // Return the response
            return new ResponseDto<List<UserOptionDto>>
            {
                Result = null,
                Message = Words.Get("NoOptionsFound")
            };

        }

    }

}