/*
 * @class Plan Entity
 *
 * @author Ruslan Sirbu
 * @version 0.0.1
 * @updated 2024-04-17
 *
 * This class is used build the Plan entity
 */

// System Namespaces
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

// Namespace for Plans entities
namespace api.Models.Entities.Plans;

/// <summary>
/// Plan Entity for Plans representation
/// </summary>
public class PlanEntity
{
    /// <summary>
    /// Plan Id Field
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("plan_id")]
    public ObjectId PlanId { get; set; }

    /// <summary>
    /// Plan Name Field
    /// </summary>
    [BsonElement("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Plan Price Field
    /// </summary>
    [BsonElement("price")]
    public decimal Price { get; set; } = 0;

    /// <summary>
    /// Plan Currency Field
    /// </summary>
    [BsonElement("currency")]
    public string? Currency { get; set; }

    /// <summary>
    /// Plan Created Time Field
    /// </summary>
    [BsonElement("created")]
    public long Created { get; set; }
}
