/*
 * @class Transaction Entity
 *
 * @author Ruslan Sirbu
 * @version 0.0.1
 * @updated 2024-04-17
 *
 * This class is used build the Transaction entity
 */

// System Namespaces
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

// Namespace for Subscriptions entities
namespace api.Models.Entities.Subscriptions;

/// <summary>
/// Transaction Entity Class
/// </summary>
public class TransactionEntity
{
    /// <summary>
    /// Transaction Id Field
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("transaction_id")]
    public ObjectId TransactionId { get; set; }

    /// <summary>
    /// Transaction Member Id Field
    /// </summary>
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("member_id")]
    public ObjectId MemberId { get; set; }

    /// <summary>
    /// Transaction Subscription Id Field
    /// </summary>
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("subscription_id")]
    public ObjectId SubscriptionId { get; set; }

    /// <summary>
    /// Transaction Plan Id Field
    /// </summary>
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("plan_id")]
    public ObjectId PlanId { get; set; }

    /// <summary>
    /// Transaction Order Id Field
    /// </summary>
    [BsonElement("order_id")]
    public string? OrderId { get; set; }

    /// <summary>
    /// Transaction Net Id Field
    /// </summary>
    [BsonElement("net_id")]
    public string? NetId { get; set; }

    /// <summary>
    /// Transaction Source Field
    /// </summary>
    [BsonElement("source")]
    public string? Source { get; set; }

    /// <summary>
    /// Transaction Created Field
    /// </summary>
    [BsonElement("created")]
    public long Created { get; set; }
}
