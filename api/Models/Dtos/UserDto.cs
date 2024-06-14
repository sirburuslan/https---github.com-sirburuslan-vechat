/*
 * @class User Dto
 *
 * @author Ruslan Sirbu
 * @version 0.0.1
 * @updated 2024-04-19
 *
 * This class is used for main user data transfer
 */

// System Namespaces
using MongoDB.Bson;

// Namespace for Dtos
namespace api.Models.Dtos;

/// <summary>
/// User Dto
/// </summary>
public class UserDto
{
    /// <summary>
    /// User's ID field
    /// </summary>
    public ObjectId UserId { get; set; }

    /// <summary>
    /// User's first name field
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    /// User's last name field
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    /// User's email field
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// User's phone field
    /// </summary>
    public string? Phone { get; set; }

    /// <summary>
    /// User's language field
    /// </summary>
    public string? Language { get; set; }

    /// <summary>
    /// User's role field
    /// </summary>
    public int Role { get; set; }

    /// <summary>
    /// User's password field
    /// </summary>
    public string? Password { get; set; }

    /// <summary>
    /// User's repeat password field
    /// </summary>
    public string? RepeatPassword { get; set; }

    /// <summary>
    /// User's reset code field
    /// </summary>
    public string? ResetCode { get; set; }

    /// <summary>
    /// Reset time
    /// </summary>
    public long ResetTime { get; set; }

    /// <summary>
    /// Joined time
    /// </summary>
    public long Created { get; set; }
}