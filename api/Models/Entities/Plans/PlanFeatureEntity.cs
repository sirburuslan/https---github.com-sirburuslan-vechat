/*
 * @class Plan Feature Entity
 *
 * @author Ruslan Sirbu
 * @version 0.0.1
 * @updated 2024-04-17
 *
 * This class is used build the Plan Feature entity
 */

// System Namespaces
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

// Namespace for Plans entities
namespace api.Models.Entities.Plans;

/// <summary>
/// Plan Feature Entity Class
/// </summary>
public class PlanFeatureEntity
{
    /// <summary>
    /// Plan Feature Id Field
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("feature_id")]
    public ObjectId FeatureId { get; set; }

    /// <summary>
    /// Plan Id Field
    /// </summary>
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("plan_id")]
    public ObjectId PlanId { get; set; }

    /// <summary>
    /// Feature Text Field
    /// </summary>
    [BsonElement("feature_text")]
    public string FeatureText { get; set; } = string.Empty;
}
