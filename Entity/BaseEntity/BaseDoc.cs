using MongoDB.Bson.Serialization.Attributes;
using SPO.CORE.DaTaAccessLayer;
using SPO.CORE.TypeCAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPS.MASS.MODEL.BaseEntity
{
    public class BaseDoc : BaseItem
    {
        public string? DocNum { set; get; }
        public string? Status { set; get; }
        public int? StatusCode { set; get; }
        //public DateTime? DueDate { set; get; }
        public List<CUserLookupValue>? UsersFollower { set; get; }
        public CUserLookupValue? Assignee { get; set; }
        public CFieldLookupValue? Assignee_Department { get; set; }
        public CUserLookupValue? AssigneeAprroval { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Unspecified)]
        public DateTime? Aprroval_Date { set; get; }       
        //public double Priority { get; set; }
        public BaseDoc()
        {
            UsersFollower = new List<CUserLookupValue>();
        }
    }
}
