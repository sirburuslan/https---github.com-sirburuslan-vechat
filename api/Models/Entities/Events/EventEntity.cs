/*
 * @class Event Entity
 *
 * @author Ruslan Sirbu
 * @version 0.0.1
 * @updated 2024-04-15
 *
 * This class is used build the Event entity
 */

// System Namespaces
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

// Namespace for Events entities
namespace api.Models.Entities.Events;

/// <summary>
/// Event Entity for Events representation
/// </summary>
public class EventEntity
{
    /// <summary>
    /// Event ID Field
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("event_id")]
    public ObjectId EventId { get; set; }

    /// <summary>
    /// Member ID Field from the Members Collection
    /// </summary>
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("member_id")]
    public ObjectId MemberId { get; set; }

    /// <summary>
    /// Type ID Field
    /// </summary>
    [BsonElement("type_id")]
    public int TypeId { get; set; }

    /// <summary>
    /// Created Field
    /// </summary>
    [BsonElement("created")]
    public long Created { get; set; }
}
