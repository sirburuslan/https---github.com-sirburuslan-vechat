/*
 * @class Subscription Entity
 *
 * @author Ruslan Sirbu
 * @version 0.0.1
 * @updated 2024-04-17
 *
 * This class is used build the Subscription entity
 */

// System Namespaces
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

// Namespace for Subscriptions entities
namespace api.Models.Entities.Subscriptions;

/// <summary>
/// Subscription Entity Class
/// </summary>
public class SubscriptionEntity
{
    /// <summary>
    /// Subscription Id Field
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("subscription_id")]
    public ObjectId SubscriptionId { get; set; }

    /// <summary>
    /// Subscription Member Id Field
    /// </summary>
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("member_id")]
    public ObjectId MemberId { get; set; }

    /// <summary>
    /// Subscription Plan Id Field
    /// </summary>
    [BsonElement("plan_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId PlanId { get; set; }

    /// <summary>
    /// Subscription Order Id Field
    /// </summary>
    [BsonElement("order_id")]
    public string? OrderId { get; set; }

    /// <summary>
    /// Subscription Net Id Field
    /// </summary>
    [BsonElement("net_id")]
    public string? NetId { get; set; }

    /// <summary>
    /// Subscription Source Field
    /// </summary>
    [BsonElement("source")]
    public string? Source { get; set; }

    /// <summary>
    /// Subscription Expiration Field
    /// </summary>
    [BsonElement("expiration")]
    public long Expiration { get; set; }

    /// <summary>
    /// Subscription Created Field
    /// </summary>
    [BsonElement("created")]
    public long Created { get; set; }
}
