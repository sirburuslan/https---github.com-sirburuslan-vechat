/*
 * @class Option Entity
 *
 * @author Ruslan Sirbu
 * @version 0.0.1
 * @updated 2024-04-17
 *
 * This class is used build the Settings Option entity
 */

// System Namespaces
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

// Namespace for Settings entities
namespace api.Models.Entities.Settings;

/// <summary>
/// Option Entity Class
/// </summary>
public class OptionEntity
{
    /// <summary>
    /// Option Id Field
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("option_id")]
    public ObjectId OptionId { get; set; }

    /// <summary>
    /// Option Name Field
    /// </summary>
    [BsonElement("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Option Value Field
    /// </summary>
    [BsonElement("value")]
    public string Value { get; set; } = string.Empty;
}
