/*
 * @class Users Read Repository
 *
 * @author Ruslan Sirbu
 * @version 0.0.1
 * @updated 2024-05-13
 *
 * This class is used to read users
 */

// System Utils
using Microsoft.AspNetCore.Builder.Extensions;
using Microsoft.Extensions.Caching.Memory;

// Installed Utils
using MongoDB.Bson;
using MongoDB.Driver;

// App Utils
using api.Models.Dtos;
using api.Models.Entities.Users;
using api.Utilities;
using api.Utilities.Db;

// Namespace for Users Account Repositories
namespace api.Repositories.Users.Account;

/// <summary>
/// Users Read Repository pattern
/// </summary>
/// <param name="db">Database connection</param>
/// <param name="memoryCache">Usery cache instance</param>
/// <param name="logger">Logger instance</param>
public class ReadRepository(MongoDb db, IMemoryCache memoryCache, ILogger logger)
{
    /// <summary>
    /// Get user data by User ID
    /// </summary>
    /// <param name="userId">User ID</param>
    /// <returns>UserDto with user's data</returns>
    public async Task<ResponseDto<UserDto>> GetUserAsync(ObjectId userId)
    {
        try
        {
            // Cache key for user
            string cacheKey = "vc_user_" + userId;

            // Verify if the user is saved in the cache
            if (!memoryCache.TryGetValue(cacheKey, out UserDto? user))
            {
                // Create a definition to search for user by user id
                FilterDefinition<UserEntity> filterDefinition = Builders<UserEntity>.Filter.Eq(m => m.UserId, userId);

                // Apply the definition filter to the read query
                var userData = await db.Users.Find(filterDefinition).Project(m => new UserDto
                {
                    UserId = m.UserId,
                    FirstName = m.FirstName,
                    LastName = m.LastName,
                    Email = m.Email,
                    Role = m.Role
                }).FirstOrDefaultAsync();

                // Add user data to user
                user = userData;

                // Create the options for cache storing
                MemoryCacheEntryOptions cacheOptions = new()
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1) // So long because i'm using the key to clear when the user's data is updated
                };

                // Create the cache
                memoryCache.Set(cacheKey, user, cacheOptions);

                // Save the cache key in the group
                //new Cache(_memoryCache).Save("users", cacheKey);

            }

            // Verify if user exists
            if (user != null)
            {
                // Return the user data
                return new ResponseDto<UserDto>
                {
                    Result = user,
                    Message = null
                };
            }
            else
            {
                // Return the error message
                return new ResponseDto<UserDto>
                {
                    Result = null,
                    Message = Words.Get("AccountNotFound")
                };
            }
        }
        catch (Exception ex)
        {

            // Logging the exception
            logger.LogError(ex, "An error occurred while user reading options.");

            // Return the error message
            return new ResponseDto<UserDto>
            {
                Result = null,
                Message = Words.Get("AccountNotFound")
            };
        }
    }
}