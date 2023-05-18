using Microsoft.AspNetCore.Routing;
using Microsoft.SharePoint.Client;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Serializers;
using SPO.CORE.DaTaAccessLayer;
using SPO.CORE.TypeCAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPS.MASS.MODEL.BaseEntity
{
    public class BaseTask : BaseItem
    {
        public int? RepeatCode { get; set; } = 0;
        public string? Bucket { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? StartDate { set; get; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? DueDate { set; get; }
        public List<CUserLookupValue>? UsersFollower { set; get; }
        public CUserLookupValue? Assignee { get; set; }
        public CFieldLookupValue? Assignee_Department { get; set; }
        public CFieldLookupValue? Assignor_Department { get; set; }
        public CUserLookupValue? Assignor { get; set; }
        public CUserLookupValue? AssigneeAprroval { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Unspecified)]
        public DateTime? Aprroval_Date { set; get; }
        public string? Description { get; set; }
        public double PercentComplete { get; set; }
        public double Priority { get; set; }
        public double Rate { get; set; }
        public BaseTask()
        {
            UsersFollower = new List<CUserLookupValue>();
        }

    }
    
    
}
