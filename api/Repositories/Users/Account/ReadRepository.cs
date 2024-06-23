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
    /// Gets all users
    /// </summary>
    /// <param name="searchDto">Search parameters</param>
    /// <returns>List with users</returns>
    public async Task<ResponseDto<ElementsDto<UserDto>>> GetUsersAsync(SearchDto searchDto) {

        try {

            // Prepare the page
            int page = (searchDto.Page > 0)?searchDto.Page:1;

            // Prepare the total results
            const int total = 24;

            // Split the keys
            string[] searchKeys = searchDto.Search!.Split(' ') ?? [];

            // Create the cache key
            string cacheKey = "vc_users_" + string.Join("_", searchKeys) + '_' + searchDto.Page;

            // Verify if the cache is saved
            if ( !memoryCache.TryGetValue(cacheKey, out Tuple<List<UserDto>, long>? usersResponse ) ) {

                // Create a filter to search users
                FilterDefinitionBuilder<UserEntity> filterBuilder = Builders<UserEntity>.Filter;

                // Initialize the combined filter
                FilterDefinition<UserEntity> combinedFilter;

                if (searchKeys.Length == 0 || searchKeys.All(string.IsNullOrWhiteSpace)) {
                    // If no search keys no filters
                    combinedFilter = filterBuilder.Empty;

                } else {
                    Console.WriteLine("All");
                    // Container for filters
                    var filters = new List<FilterDefinition<UserEntity>>();
                    
                    // Apply filtering based on searchKeys
                    foreach (string key in searchKeys) {

                        // Set where parameters
                        var keyFilter = filterBuilder.Or(
                            filterBuilder.Regex(u => u.FirstName, new BsonRegularExpression(key, "i")),
                            filterBuilder.Regex(u => u.LastName, new BsonRegularExpression(key, "i")),
                            filterBuilder.Regex(u => u.LastName, new BsonRegularExpression(key, "i"))
                        );

                        // Add key filter to the list
                        filters.Add(keyFilter);

                    }

                    // Combine the filters
                    combinedFilter = filterBuilder.And(filters);

                }

                // Apply the filters
                var sort = Builders<UserEntity>.Sort.Descending(u => u.UserId);

                // Total count of matched documents
                long totalCount = await db.Users.CountDocumentsAsync(combinedFilter);

                // Paginated and sorted results
                List<UserDto> users = await db.Users.Find(combinedFilter)
                                            .Project(u => new UserDto {
                                                UserId = u.UserId,
                                                FirstName = u.FirstName,
                                                LastName = u.LastName,
                                                Email = u.Email,
                                                Role = u.Role,
                                                Created = u.Created
                                            })
                                            .Sort(sort)
                                            .Skip((page - 1) * total)
                                            .Limit(total)
                                            .ToListAsync();

                // Add data to user response
                usersResponse = new Tuple<List<UserDto>, long>(users, totalCount);

                // Create the cache options for storing
                MemoryCacheEntryOptions cacheOptions = new() {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1)
                };

                // Save the request in the cache
                //_memoryCache.Set(cacheKey, usersResponse, cacheOptions);

                // Save the cache key in the group
                //new Cache(_memoryCache).Save("users", cacheKey);

            }

            // Verify if users exists
            if ( usersResponse?.Item1.Count > 0 ) {

                // Return the response
                return new ResponseDto<ElementsDto<UserDto>> {
                    Result = new ElementsDto<UserDto> {
                        Elements = usersResponse.Item1,
                        Total = usersResponse.Item2,
                        Page = searchDto.Page
                    },
                    Message = null
                };

            } else {

                // Return the response
                return new ResponseDto<ElementsDto<UserDto>> {
                    Result = null,
                    Message = Words.Get("NoUsersFound")
                };

            }

        } catch ( InvalidOperationException e ) {

            // Return the response
            return new ResponseDto<ElementsDto<UserDto>> {
                Result = null,
                Message = e.Message
            };                

        }

    }

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

                // Add user data to user
                user = await db.Users.Find(filterDefinition).Project(u => new UserDto
                {
                    UserId = u.UserId,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    Role = u.Role
                }).FirstOrDefaultAsync();

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