using Microsoft.Identity.Client;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using Newtonsoft.Json;
using SPO.CORE.DaTaAccessLayer;
using SPO.CORE.TypeCAttribute;
using SPS.MASS.MODEL.BusinessModel;
using SPS.MASS.MODEL.SPEntity;

namespace SPS.MASS.MODEL.Entity
{
    public class ProjectEntity : BaseItem
    {
        public double Priority { set; get; } = 10;
        [BsonDateTimeOptions(Kind = DateTimeKind.Unspecified)]
        public DateTime StartDate { set; get; }

        public CFieldLookupValue ProjectDepartment_Mng { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Unspecified)]
        public DateTime DueDate { set; get; }

        public List<CUserLookupValue>? ProjectMng { set; get; }

        public List<CUserLookupValue>? ProjectMembers { set; get; }

        public List<CFieldLookupValue>? ProjectDepartment { set; get; }
        public string? Status { set; get; } = Mass_Status.TasksStatus.InProcess;
        public double StatusCode { set; get; }
        public double PercentComplete { get; set; }
        public string? Milestones { get; set; }
        public string? Description { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Unspecified)]
        public DateTime? DateFinish { set; get; }
    }

    public class ProjectEntityDetail : ProjectEntity
    {
        public ProjectPermission permission { get; set; }
        //public  object MilestonesObect { get; set; }

    }
    public class ProjectEntityView : ProjectEntityDetail
    {

        public int TaskCount { get; set; }
        public int TaskCompleteCount { get; set; }
        public int CommentCount { get; set; }
        // var Comment = _mongoContext.MogoDB.GetCollection<CommentEntity>(CommentModel.Collection);
    }

    public class ProJectMenu : BaseItem
    {
        public double StatusCode { set; get; } = 100;
        public CFieldLookupValue ProjectDepartment_Mng { get; set; }
        public List<CUserLookupValue>? ProjectMng { set; get; }

        public List<CUserLookupValue>? ProjectMembers { set; get; }

        public List<CFieldLookupValue>? ProjectDepartment { set; get; }
    }
    public class ProjectAddEntity : BaseAddItem
    {
        public string? Status { set; get; } = Mass_Status.TasksStatus.InProcess;
        public double Priority { set; get; } = 10;
        public double StatusCode { set; get; } = 100;
        [BsonDateTimeOptions(Kind = DateTimeKind.Unspecified)]
        public DateTime StartDate { set; get; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Unspecified)]
        public DateTime DueDate { set; get; }
        public List<CUserLookupValue>? ProjectMng { set; get; }
        public List<CUserLookupValue>? ProjectMembers { set; get; }
        public List<CFieldLookupValue>? ProjectDepartment { set; get; }
        public CFieldLookupValue? ProjectDepartment_Mng { set; get; }
        public double PercentComplete { get; set; }
        [JsonIgnore]
        public string? Milestones { get; set; }
        public List<MilestonesEntity>? MilestonesObject { get; set; } = new List<MilestonesEntity>();
        public string? Description { get; set; }
    }

    public class MilestonesEntity
    {
        public string GuidId { get; set; }
        public string Title { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Unspecified)]
        public DateTime DueDate { get; set; }
    }


    #region ReportProjedct
    //TaskReportObject
    public class CoutReportProject : TaskReportObject
    {
        public int Total { get; set; }
    }

    public class TotalTaskReport : TaskReportObject
    {
        public int Total { get; set; }
    }
    public class GroupProJect : TaskReportObject
    {
        public CFieldLookupValue Project { get; set; }
    }
    public class ProjectDashBoard
    {
        public CoutReportProject _CoutReportProject { get; set; }
        public TotalTaskReport _TotalTaskReport { get; set; }
        public List<GroupProJect> _GroupProJect { get; set; } = new List<GroupProJect>();
        public ProjectDashBoard()
        {
            _CoutReportProject = new CoutReportProject();
            _TotalTaskReport = new TotalTaskReport();
        }
    }

    #endregion 
}
