/*
 * @class Plan Meta Entity
 *
 * @author Ruslan Sirbu
 * @version 0.0.1
 * @updated 2024-04-17
 *
 * This class is used build the Plan Meta entity
 */

// System Namespaces
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

// Namespace for Plans entities
namespace api.Models.Entities.Plans;

/// <summary>
/// Plan Meta Entity Class
/// </summary>
public class PlanMetaEntity
{
    /// <summary>
    /// Plan Meta Id Field
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("meta_id")]
    public ObjectId MetaId { get; set; }

    /// <summary>
    /// Plan Id Field
    /// </summary>
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("plan_id")]
    public ObjectId PlanId { get; set; }

    /// <summary>
    /// Meta Name Field
    /// </summary>
    [BsonElement("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Meta Value Field
    /// </summary>
    [BsonElement("value")]
    public string? Value { get; set; }
}
