/*
 * @class Typing Entity
 *
 * @author Ruslan Sirbu
 * @version 0.0.1
 * @updated 2024-04-17
 *
 * This class is used build the Typing entity
 */

// System Namespaces
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

// Namespace for Messages entities
namespace api.Models.Entities.Messages;

/// <summary>
/// Typing Entity Class
/// </summary>
public class TypingEntity
{
    /// <summary>
    /// Typing ID Field
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("id")]
    public ObjectId Id { get; set; }

    /// <summary>
    /// Typing Thread ID Field
    /// </summary>
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("thread_id")]
    public ObjectId ThreadId { get; set; }

    /// <summary>
    /// Typing Member Id Field
    /// </summary>
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("member_id")]
    public ObjectId MemberId { get; set; }

    /// <summary>
    /// Typing Updated Field
    /// </summary>
    [BsonElement("Updated")]
    public long Updated { get; set; }
}
