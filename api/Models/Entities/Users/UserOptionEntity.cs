/*
 * @class User Option Entity
 *
 * @author Ruslan Sirbu
 * @version 0.0.1
 * @updated 2024-04-17
 *
 * This class is used build the User Option entity
 */

// System Namespaces
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

// Namespace for Users entities
namespace api.Models.Entities.Users;

/// <summary>
/// User Option Entity Class
/// </summary>
public class UserOptionEntity
{
    /// <summary>
    /// Option ID Field
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("option_id")]
    public ObjectId OptionId { get; set; }

    /// <summary>
    /// User ID Field
    /// </summary>
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("user_id")]
    public ObjectId UserId { get; set; }

    /// <summary>
    /// Option Name Field
    /// </summary>
    [BsonElement("option_name")]
    public string OptionName { get; set; } = string.Empty;

    /// <summary>
    /// Option Value Field
    /// </summary>
    [BsonElement("option_value")]
    public string OptionValue { get; set; } = string.Empty;
}
