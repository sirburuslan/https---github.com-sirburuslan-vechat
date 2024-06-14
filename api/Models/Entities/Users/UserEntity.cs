/*
 * @class User Entity
 *
 * @author Ruslan Sirbu
 * @version 0.0.1
 * @updated 2024-04-16
 *
 * This class is used build the User entity
 */

// System Namespaces
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

// Namespace for Users entities
namespace api.Models.Entities.Users;

/// <summary>
/// User Entity Class
/// </summary>
public class UserEntity
{
    /// <summary>
    /// User ID Field
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("user_id")]
    public ObjectId UserId { get; set; }

    /// <summary>
    /// First Name Field
    /// </summary>
    [BsonElement("first_name")]
    public string? FirstName { get; set; }

    /// <summary>
    /// Last Name Field
    /// </summary>
    [BsonElement("last_name")]
    public string? LastName { get; set; }

    /// <summary>
    /// Email Field
    /// </summary>
    [BsonElement("email")]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// User Role Field
    /// </summary>
    [BsonElement("role")]
    public int Role { get; set; }

    /// <summary>
    /// Password Field
    /// </summary>
    [BsonElement("password")]
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// Reset Code Field
    /// </summary>
    [BsonElement("reset_code")]
    public string? ResetCode { get; set; }

    /// <summary>
    /// Reset Time Field
    /// </summary>
    [BsonElement("reset_time")]
    public long? ResetTime { get; set; }

    /// <summary>
    /// Created Field
    /// </summary>
    [BsonElement("created")]
    public long Created { get; set; }
}
