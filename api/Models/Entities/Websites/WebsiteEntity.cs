/*
 * @class Website Entity
 *
 * @author Ruslan Sirbu
 * @version 0.0.1
 * @updated 2024-04-17
 *
 * This class is used build the Website entity
 */

// System Namespaces
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

// Namespace for Websites entities
namespace api.Models.Entities.Websites;

/// <summary>
/// Website Entity Class
/// </summary>
public class WebsiteEntity
{
    /// <summary>
    /// Website Id Field
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("website_id")]
    public ObjectId WebsiteId { get; set; }

    /// <summary>
    /// Member Id Field
    /// </summary>
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("member_id")]
    public ObjectId MemberId { get; set; }

    /// <summary>
    /// Chat Status Field
    /// </summary>
    [BsonElement("enabled")]
    public int ChatEnabled { get; set; }

    /// <summary>
    /// Chat Header Field
    /// </summary>
    [BsonElement("header")]
    public string? ChatHeader { get; set; }

    /// <summary>
    /// Website Name Field
    /// </summary>
    [BsonElement("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Website Url Field
    /// </summary>
    [BsonElement("url")]
    public string Url { get; set; } = string.Empty;

    /// <summary>
    /// Website Domain Field
    /// </summary>
    [BsonElement("domain")]
    public string Domain { get; set; } = string.Empty;

    /// <summary>
    /// Website Created Time Field
    /// </summary>
    [BsonElement("created")]
    public long Created { get; set; }
}
