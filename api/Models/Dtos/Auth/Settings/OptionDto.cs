/*
 * @class Option Dto
 *
 * @author Ruslan Sirbu
 * @version 0.0.1
 * @updated 2024-05-09
 *
 * This class is used for option data transfer
 */

// System Namespaces
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

// Namespace for Settings Dtos
namespace api.Models.Dtos.Settings;

/// <summary>
/// Settings Option Dto
/// </summary>
public class OptionDto
{
    /// <summary>
    /// Option's ID
    /// </summary>
    public ObjectId OptionId { get; set; }

    /// <summary>
    /// Option's Name
    /// </summary>
    public required string OptionName { get; set; }

    /// <summary>
    /// Option's Value
    /// </summary>
    public string? OptionValue { get; set; }
}