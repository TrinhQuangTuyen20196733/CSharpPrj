
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SPO.CORE.TypeCAttribute;
using SPS.MASS.MODEL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SPS.MASS.MODEL.SPEntity
{
    public class WF_ReQuest_ProcessEntity : SPO.CORE.DaTaAccessLayer.BaseEntity
    {
        public CFieldLookupValue? ItemLookup { get; set; }
        public int ActionType { get; set; }
        public double Rate { get; set; }
        public string? ModuleName { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Unspecified)]
        public DateTime? Step_StartDate { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Unspecified)]
        public DateTime? Step_DueDate { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Unspecified)]
        public DateTime? Step_Finish { get; set; }
        public CFieldLookupValue? Department { get; set; }
        public int? CategoryId { get; set; }
        public int Status_Code { get; set; }
        public string? Step_Name { get; set; }
        public double? Time_SLA { get; set; }
        public string? Current_CUserProfile { get; set; }
        public int? Step_Status { get; set; }
        public CUserLookupValue? Assignee { get; set; }
        public double? Time_SLA_Finish { get; set; }
        public CFieldLookupValue? UserProfile { get; set; }
        public string? Json_Data { get; set; }
        [SPListItemCustomize]
        [JsonIgnore]
        public BsonDocument? Json_DataObj { get; set; }
    }
    public class WF_ReQuest_ProcessEntityDetail : WF_ReQuest_ProcessEntity
    {
        public bool Delegate { get; set; } = false;
        public double TimeAgo { get; set; }
        // public double TimeLeft
    }
    public static class WF_ReQuest_BsonObject
    {
        public static WF_ReQuest_ProcessEntity ConvertBsonObject(this WF_ReQuest_ProcessEntity entity)
        {
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(entity.Json_Data) && !string.IsNullOrEmpty(entity.Json_Data))
                {
                    //Cần phải sử dụng BsonDocument// Convert mọi loại đối tượng thành 1 đối tượng
                    entity.Json_DataObj = BsonDocument.Parse(entity.Json_Data); //
                }
            }
            return entity;
        }
    }
}

