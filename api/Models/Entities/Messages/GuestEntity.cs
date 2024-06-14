/*
 * @class Guest Entity
 *
 * @author Ruslan Sirbu
 * @version 0.0.1
 * @updated 2024-04-17
 *
 * This class is used build the Guest entity
 */

// System Namespaces
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

// Namespace for Messages entities
namespace api.Models.Entities.Messages;

/// <summary>
/// Guest Entity Class
/// </summary>
public class GuestEntity
{
    /// <summary>
    /// Guest ID Field
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("guest_id")]
    public ObjectId GuestId { get; set; }

    /// <summary>
    /// Guest Name Field
    /// </summary>
    [BsonElement("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Guest Email Field
    /// </summary>
    [BsonElement("email")]
    public string? Email { get; set; }

    /// <summary>
    /// Guest Ip Field
    /// </summary>
    [BsonElement("ip")]
    public string? Ip { get; set; }

    /// <summary>
    /// Guest Latitude Field
    /// </summary>
    [BsonElement("latitude")]
    public string? Latitude { get; set; }

    /// <summary>
    /// Guest Longitude Field
    /// </summary>
    [BsonElement("longitude")]
    public string? Longitude { get; set; }

    /// <summary>
    /// Created Field
    /// </summary>
    [BsonElement("created")]
    public long Created { get; set; }
}
