using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using ShowTrack.Shared.Models;

namespace ShowTrack.Server.Models;

public class Show : ShowBase
{    
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
}
