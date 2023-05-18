using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace BHDStarBooking.Entity
{
    
    public class BaseEntity
        
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }= ObjectId.GenerateNewId().ToString();

       
        /*
       public DateTime? createDate { get; set; }
       public DateTime? modifiedDate { get; set; }
       public string? createBy { get; set; }
       public string? modifiedBy { get; set; }*/


    }
}
