using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HoneyDoApi.models;

public class Reminder: IReminder
{
    [BsonRepresentation(BsonType.ObjectId)]

    public string Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;
    public bool Complete { get; set; }
}