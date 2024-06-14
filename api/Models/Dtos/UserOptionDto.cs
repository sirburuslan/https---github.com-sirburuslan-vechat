/*
 * @class User Option Dto
 *
 * @author Ruslan Sirbu
 * @version 0.0.1
 * @updated 2024-05-20
 *
 * This class is used for user option data transfer
 */

// Installed Utils
using MongoDB.Bson;

// Namespace for Dtos
namespace api.Models.Dtos;

/// <summary>
/// Dto for User Options
/// </summary>
public class UserOptionDto
{
    /// <summary>
    /// Option's ID
    /// </summary>
    public ObjectId OptionId { get; set; }

    /// <summary>
    /// User's ID
    /// </summary>
    public ObjectId UserId { get; set; }

    /// <summary>
    /// Option's name
    /// </summary>
    public required string OptionName { get; set; }

    /// <summary>
    /// Option's value
    /// </summary>
    public required string OptionValue { get; set; }
}