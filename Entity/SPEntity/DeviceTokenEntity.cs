using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SPO.CORE.TypeCAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPS.MASS.MODEL.SPEntity
{
    public class DeviceTokenEntity
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public int UserAD { get; set; }
        public string Token { get; set; }
        public string APP { get; set; }
        public CFieldLookupValue CFieldUser { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Unspecified)]
        public DateTime Created { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Unspecified)]
        public DateTime Modify { get; set; }

        public string UserPrincipalName { get; set; }
    }
}
