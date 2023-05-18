using SPO.CORE.DaTaAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPO.CORE.TypeCAttribute;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace SPS.MASS.MODEL.SPEntity
{
    public class HistoryEntity : BaseItem
    {
        public string? Content { get; set; }
        public CFieldLookupValue? ItemLookup { get; set; }

        public int ActionType { get; set; }

        public string? MetaData { get; set; }

        public int Rate { get; set; }

        public CUserLookupValue? UserRate { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Unspecified)]
        public DateTime? RateDateTime { get; set; }
        public string? FilesLink { get; set; }
        public string? Fields { get; set; }
        [BsonRequired]
        public string ModuleName { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Unspecified)]
        public DateTime? Step_StartDate { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Unspecified)]
        public DateTime? Step_DueDate { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Unspecified)]
        public DateTime? Step_Finish { get; set; }

        public string CurrentWorkflowEncode { get; set; }
        public string? Sender_Profile { get; set; }
        public string? Receiver_Profile { get; set; }
        public string? RateContent { get; set; }
        public int? CategoryId { get; set; }
    }

    public class ReQuest_History : HistoryEntity
    {
        //public string Fields { get; set; }
    }

    public class Task_History : HistoryEntity
    {
        //public string Fields { get; set; }
        public object Permission { get; set; }

        public Task_History()
        {
            Permission = new { Rate = false };

        }
    }
}
