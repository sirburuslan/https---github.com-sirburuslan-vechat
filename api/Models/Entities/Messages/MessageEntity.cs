/*
 * @class Message Entity
 *
 * @author Ruslan Sirbu
 * @version 0.0.1
 * @updated 2024-04-17
 *
 * This class is used build the Message entity
 */

// System Namespaces
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

// Namespace for Messages entities
namespace api.Models.Entities.Messages;

/// <summary>
/// Message Entity Class
/// </summary>
public class MessageEntity
{
    /// <summary>
    /// Message ID Field
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("message_id")]
    public ObjectId MessageId { get; set; }

    /// <summary>
    /// Message Thread ID Field
    /// </summary>
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("thread_id")]
    public ObjectId ThreadId { get; set; }

    /// <summary>
    /// Message Member ID Field
    /// </summary>
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("member_id")]
    public ObjectId MemberId { get; set; }

    /// <summary>
    /// Message Content Field
    /// </summary>
    [BsonElement("message")]
    public string? Message { get; set; }

    /// <summary>
    /// Message Seen Status Field
    /// </summary>
    [BsonElement("seen")]
    public int Seen { get; set; }

    /// <summary>
    /// Message Created Field
    /// </summary>
    [BsonElement("created")]
    public long Created { get; set; }
}
