using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace BHDStarBooking.Entity
{
    public class BEntity : SharePointBaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; } = ObjectId.GenerateNewId().ToString();
    }
}
