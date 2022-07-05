using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ShowTrack.Shared.Models;

public class Show
{    
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public ShowType Type { get; set; }

    public string Title { get; set; } = null!;

    public string[]? Cast { get; set; }

    public int ReleaseYear { get; set; }

    public string[]? Directors { get; set; }

    public DateTime DateAdded { get; set; }

    public string Duration { get; set; } = null!;

    public string[] Categories { get; set; } = null!;

    public string Rating { get; set; } = null!;

    public string Description { get; set; } = null!;    

    public SubscriptionType SubscriptionType { get; set; }
}
