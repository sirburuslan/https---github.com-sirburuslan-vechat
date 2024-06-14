/*
 * @class Thread Entity
 *
 * @author Ruslan Sirbu
 * @version 0.0.1
 * @updated 2024-04-17
 *
 * This class is used build the Thread entity
 */

// System Namespaces
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

// Namespace for Messages entities
namespace api.Models.Entities.Messages;

/// <summary>
/// Thread Entity Class
/// </summary>
public class ThreadEntity
{
    /// <summary>
    /// Thread ID Field
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("thread_id")]
    public ObjectId ThreadId { get; set; }

    /// <summary>
    /// Thread Secret Field
    /// </summary>
    [BsonElement("thread_secret")]
    public string ThreadSecret { get; set; } = string.Empty;

    /// <summary>
    /// Thread Member Id Field
    /// </summary>
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("member_id")]
    public ObjectId MemberId { get; set; }

    /// <summary>
    /// Thread Website Id Field
    /// </summary>
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("website_id")]
    public ObjectId WebsiteId { get; set; }

    /// <summary>
    /// Thread Guest Id Field
    /// </summary>
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("guest_id")]
    public ObjectId GuestId { get; set; }

    /// <summary>
    /// Created Field
    /// </summary>
    [BsonElement("created")]
    public long Created { get; set; }
}
