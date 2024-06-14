/*
 * @class Caches Manager
 *
 * @author Ruslan Sirbu
 * @version 0.0.1
 * @updated 2024-04-19
 *
 * This class is store, update and delete complex cache strings
 */

// System Namespaces
using Microsoft.Extensions.Caching.Memory;

// Namespace for General Utils
namespace api.Utilities;

/// <summary>
/// Caches Manager
/// </summary>
/// <param name="memoryCache">Memory cache instance</param>
public class Cache(IMemoryCache memoryCache)
{
    /// <summary>
    /// Memory cache container
    /// </summary>
    private readonly IMemoryCache _memoryCache = memoryCache;

    /// <summary>
    /// Save cache list
    /// </summary>
    /// <param name="cacheGroup">Name of the group</param>
    /// <param name="cacheKey">Name of the cache</param>
    public void Save(string cacheGroup, string cacheKey)
    {
        // Verify if the cache exists
        if (!_memoryCache.TryGetValue(cacheGroup, out List<string>? values))
        {
            // Create a new list to store cache keys
            values = [];
        }

        // Add the new cacheKey to the list
        values!.Add(cacheKey);

        // Create the options for cache storing
        MemoryCacheEntryOptions cacheOptions = new()
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1)
        };

        // Set or update the cache with the new list of values
        _memoryCache.Set(cacheGroup, values, cacheOptions);
    }

    /// <summary>
    /// Remove cache by group
    /// </summary>
    /// <param name="cacheGroup">Name of the group</param>
    public void Remove(string cacheGroup)
    {
        // Verify if the cache exists
        if (_memoryCache.TryGetValue(cacheGroup, out List<string>? records))
        {
            // List the groups records
            foreach (string record in records ?? [])
            {
                // Remove the cache by record
                _memoryCache.Remove(record);
            }
        }
    }
}