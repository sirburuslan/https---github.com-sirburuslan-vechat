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

// App Utils
using api.Models.Dtos;
using api.Models.Dtos.Settings;

// Namespace for Settings repositories
namespace api.Services.Interfaces.Settings;

/// <summary>
/// Repository for general settings
/// </summary>
/// <remarks>
/// Settings Repository Constructor
/// </remarks>
public interface ISettingsRepository
{
    /// <summary>
    /// Get the settings options
    /// </summary>
    /// <returns>The options list or null</returns>
    Task<ResponseDto<List<OptionDto>>> OptionsListAsync();
}