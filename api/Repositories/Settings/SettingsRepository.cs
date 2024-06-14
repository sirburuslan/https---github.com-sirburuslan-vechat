/*
 * @class Settings Repository
 *
 * @author Ruslan Sirbu
 * @version 0.0.1
 * @updated 2024-05-09
 *
 * This class is used manage the general settings
 */

// System Utils
using Microsoft.Extensions.Caching.Memory;

// Installed Utils
using MongoDB.Driver;

// App Utils
using api.Models.Dtos;
using api.Models.Dtos.Settings;
using api.Models.Entities.Settings;
using api.Services.Interfaces.Settings;
using api.Utilities;
using api.Utilities.Db;

// Namespace for Settings repositories
namespace api.Repositories.Settings;

/// <summary>
/// Repository for general settings
/// </summary>
/// <remarks>
/// Settings Repository Constructor
/// </remarks>
/// <param name="memoryCache">Memory cache session</param>
/// <param name="db">Database connection</param>
/// <param name="logger">Logger instance</param>
public class SettingsRepository(IMemoryCache memoryCache, MongoDb db, ILogger<SettingsRepository> logger) : ISettingsRepository
{
    /// <summary>
    /// Memory cache container
    /// </summary>
    private readonly IMemoryCache _memoryCache = memoryCache;

    /// <summary>
    /// Settings table context container
    /// </summary>
    private readonly MongoDb _context = db;

    /// <summary>
    /// Get the settings options
    /// </summary>
    /// <returns>The options list or null</returns>
    public async Task<ResponseDto<List<OptionDto>>> OptionsListAsync()
    {
        try
        {
            // Create the cache key for settings
            const string cacheKey = "vc_member_settings";

            // Verify if the options are saved in the cache
            if (!_memoryCache.TryGetValue(cacheKey, out List<OptionDto>? optionsList))
            {
                // Emtpty definition for filter
                FilterDefinition<OptionEntity> filterDefinition = FilterDefinition<OptionEntity>.Empty;

                // Request the options with empty filter
                optionsList = await _context.Settings.Find(filterDefinition).Project(o => new OptionDto { OptionId = o.OptionId, OptionName = o.Name, OptionValue = o.Value }).ToListAsync();

                // Create the cache options for storing
                MemoryCacheEntryOptions cacheOptions = new()
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1)
                };

                // Save the request in the cache
                //_memoryCache.Set(cacheKey, optionsList, cacheOptions);

            }

            // Verify if options exists
            if (optionsList?.Count > 0)
            {
                // Return the options
                return new ResponseDto<List<OptionDto>>
                {
                    Result = optionsList,
                    Message = null
                };
            }
            else
            {
                // Return the missing options message
                return new ResponseDto<List<OptionDto>>
                {
                    Result = null,
                    Message = Words.Get("NoOptionsFound")
                };
            }
        }
        catch (Exception ex)
        {

            // Logging the exception
            logger.LogError(ex, "An error occurred while reading website options.");

            // Return the error message
            return new ResponseDto<List<OptionDto>>
            {
                Result = null,
                Message = Words.Get("NoOptionsFound")
            };
        }
    }
}