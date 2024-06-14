/*
 * @class Subscription Meta Entity
 *
 * @author Ruslan Sirbu
 * @version 0.0.1
 * @updated 2024-04-17
 *
 * This class is used build the Subscription Meta entity
 */

// System Namespaces
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

// Namespace for Subscriptions entities
namespace api.Models.Entities.Subscriptions;

/// <summary>
/// Subscription Meta Entity Class
/// </summary>
public class SubscriptionMetaEntity
{
    /// <summary>
    /// Subscription Meta Id Field
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("meta_id")]
    public ObjectId MetaId { get; set; }

    /// <summary>
    /// Subscription Id Field
    /// </summary>
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("subscription_id")]
    public ObjectId SubscriptionId { get; set; }

    /// <summary>
    /// Subscription Meta Name Field
    /// </summary>
    [BsonElement("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Subscription Meta Value Field
    /// </summary>
    [BsonElement("value")]
    public string? Value { get; set; }
}
