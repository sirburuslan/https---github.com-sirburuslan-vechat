/*
 * @class Plan Restriction Entity
 *
 * @author Ruslan Sirbu
 * @version 0.0.1
 * @updated 2024-04-17
 *
 * This class is used build the Plan Restriction entity
 */

// System Namespaces
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

// Namespace for Plans entities
namespace api.Models.Entities.Plans;

/// <summary>
/// Plan Restriction Entity Class
/// </summary>
public class PlanRestrictionEntity
{
    /// <summary>
    /// Plan Restriction Id Field
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("restriction_id")]
    public ObjectId RestrictionId { get; set; }

    /// <summary>
    /// Plan Id Field
    /// </summary>
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("plan_id")]
    public ObjectId PlanId { get; set; }

    /// <summary>
    /// Restriction Name Field
    /// </summary>
    [BsonElement("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Restriction Value Field
    /// </summary>
    [BsonElement("value")]
    public int Value { get; set; }
}
