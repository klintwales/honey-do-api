using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HoneyDoApi.models;

public class Reminder: IReminder
{
    public ObjectId Id { get; set; } = ObjectId.GenerateNewId();
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime ModifiedDate { get; set; }
    public bool Complete { get; set; }
}