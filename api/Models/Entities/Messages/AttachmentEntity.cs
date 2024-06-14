/*
 * @class Attachment Entity
 *
 * @author Ruslan Sirbu
 * @version 0.0.1
 * @updated 2024-04-17
 *
 * This class is used build the Attachment entity
 */

// System Namespaces
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

// Namespace for Messages entities
namespace api.Models.Entities.Messages;

/// <summary>
/// Attachment Entity Class
/// </summary>
public class AttachmentEntity
{
    /// <summary>
    /// Attachment ID Field
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("attachment_id")]
    public ObjectId AttachmentId { get; set; }

    /// <summary>
    /// Message ID Field
    /// </summary>
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("message_id")]
    public ObjectId MessageId { get; set; }

    /// <summary>
    /// Link Field
    /// </summary>
    [BsonElement("link")]
    public string Link { get; set; } = string.Empty;

    /// <summary>
    /// Created Field
    /// </summary>
    [BsonElement("created")]
    public long Created { get; set; }
}
