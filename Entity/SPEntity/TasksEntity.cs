using SPO.CORE.TypeCAttribute;
using SPS.MASS.MODEL.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using SPO.CORE.DaTaAccessLayer;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;
using Microsoft.Identity.Client;
using Microsoft.AspNetCore.Mvc.Formatters;
using SPS.MASS.MODEL.SPEntity;

namespace SPS.MASS.MODEL.Entity
{
    public class TasksEntity : BaseTask
    {
        //[SPListItemCustomize(ConvertObjectToMogo = true)]
        //public object? Source_Object { get; set; }
        public string? RejectedReason { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Unspecified)]
        public DateTime? Date_Finished { set; get; }
        public int SourceID { get; set; }
        public string? SourceModule { get; set; }
        public string? Source { get; set; }
        public string ArrCheckList { get; set; }
        public CFieldLookupValue? Parent { get; set; }
        public string Status { get; set; } = Mass_Status.TasksStatus.InProcess;
        [BsonDateTimeOptions(Kind = DateTimeKind.Unspecified)]
        public DateTime? NewDueDate { get; set; }
        public string FilesLink { get; set; }
        public CFieldLookupValue Assignee_Profile { get; set; }
        public double StatusCode { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Unspecified)]
        public DateTime? SentDate { get; set; }
        public string? ResultConent { get; set; }
        public CFieldLookupValue? UserReviewer_Profile { get; set; }
        public List<CUserLookupValue>? UsersUnread { get; set; } = new List<CUserLookupValue>();
        public string? RepeatDetail { get; set; }
        public string? RepeatTasks { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Unspecified)]
        public DateTime? DueDateRepeat { get; set; }
        [SPListItemCustomize]
        public RepeatConfig? RepeatDetailObj { get; set; }
        public int RepeatType { get; set; }
        [SPListItemCustomize]
        public List<RepeatTasksEntity> RepeatTasksObj { get; set; } = new List<RepeatTasksEntity>();
        public int RepeatStatus { get; set; } = 0;
        public bool Import { get; set; } = false;
        public bool RepeatReport { get; set; } = false;
        public string? ImportGuid { get; set; }
        public string? Number { get; set; }
        public CFieldLookupValue? Assignor_Profile { get; set; }
        public CFieldLookupValue? Task_Group { get; set; }
        public string? LabelContent { get; set; }

    }
    public class TasksEntityVM : TasksEntity
    {
        public TaskPermission permission { get; set; }
        public List<TasksEntityVM> LstItems { get; set; }
        public TasksEntityVM()
        {
            LstItems = new List<TasksEntityVM>();
        }
    }
    public class TasksEntityList : TasksEntity
    {
        public int CommentCount { get; set; }
        public TaskPermission permission { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Unspecified)]
        public DateTime? NextReportRepeateDueDated { get; set; }

    }
    /// <summary>
    /// Áp dụng cho Detail của Task
    /// </summary>
    public class TasksEntityDetail : TasksEntity
    {
        public TaskPermission permission { get; set; }


    }
    /// <summary>
    /// Áp dụng cho môi trường UPDATE
    /// </summary>
    public class TasksEntityUpDate
    {
        public string doAction { get; set; }
        public TaskPostObjectData ObjectData { get; set; }
        public string? FilesLink { get; set; }

        public string? Fields { get; set; }

        public string? MetaData { get; set; }
        public string? Content { get; set; }
    }
    public class PutDataTasksEntity
    {
        public string doAction { get; set; }
        public string type { get; set; }
        public object Data { get; set; }
    }
    public class PutDataApproval
    {
        public double Rate { get; set; } = 0;
        public string? ApprovalConent { get; set; }
    }
    public class PutDataReject
    {
        public string RejectedReason { get; set; }
    }
    public class ChangeDueDate
    {
        [BsonDateTimeOptions(Kind = DateTimeKind.Unspecified)]
        public DateTime value { get; set; }
    }

    public class AddTaskToTaskGroup
    {
        public List<int> Ids { get; set; }
        public TaskGroupContent taskGroupContent { get; set; } = new TaskGroupContent();

    }
    public class TaskGroupContent
    {
        public CFieldLookupValue Group { get; set; }
        public LabelContentEntity LabelContent { get; set; }
    }
    //Client Send
    public class ReportResult
    {
        public string ResultConent { get; set; }
        public CUserProfile? UserReviewer { get; set; }
        public string? FilesLink { get; set; }

        public string? Fields { get; set; }

        public string? MetaData { get; set; }

    }
    public class TaskEntityAddNew : BaseAddItem
    {
        public string FilesLink { get; set; }
        public string? guid { get; set; }
        public string? Number { get; set; }
        public string? Description { get; set; }
        public double Priority { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Unspecified)]
        public DateTime StartDate { set; get; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Unspecified)]
        public DateTime DueDate { set; get; }
        public List<CUserLookupValue>? UsersFollower { set; get; }
        public List<CUserProfile>? Assignee_Multi { set; get; }
        public CUserProfile? Assignee_CLookup { get; set; }
        public CUserProfile Assignor_CLookup { get; set; }
        public int? RepeatCode { get; set; } = 0;
        public int SourceID { get; set; }
        public string? SourceModule { get; set; }
        public string? Source { get; set; }
        public List<ArrCheckListObject>? ArrCheckListObject { get; set; } = new List<ArrCheckListObject>();
        [BsonIgnore]
        [SPListItemCustomize]
        public int? ParentID { get; set; } = 0;
        public string? RepeatDetail { get; set; }
        public string? RepeatTasks { get; set; }
        public bool? Import { get; set; } = false;


        public CFieldLookupValue? Task_Group { get; set; }
        public LabelContentEntity? LabelContent { get; set; }

    }
    public class ArrCheckListObject
    {
        public string Guidid { get; set; }
        public string? Title { get; set; }
        public string? CheckList_Status { get; set; }

    }
    public class ImportEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Unspecified)]
        public DateTime StartDate { set; get; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Unspecified)]
        public DateTime DueDate { set; get; }
        public string Assignor { get; set; }
        public string Assignee { get; set; }
        public string ProjectId { get; set; }
        public string? Parent { get; set; }
        public double Priority { get; set; }

        public string? UsersFollower { set; get; }
        public string? Number { set; get; }
        public string? guid { set; get; }

    }
    /// <summary>
    /// Task repeat Manager
    /// </summary>
    public class RepeatConfig
    {
        public int CountDay { get; set; }
        public int Type { get; set; }
        public List<OnDays> OnDay { get; set; } = new List<OnDays>();
        public CountDaySelected? countDaySelected { get; set; }
        public int monthOptionSelected { get; set; }
        public OnNumberDayOfWeekSelected? onNumberDayOfWeekSelected { get; set; }
        public List<DayOfWeekSeleted> dayOfWeekSeleted { get; set; } = new List<DayOfWeekSeleted>();
        public OnDayOfWeekSelected? onDayOfWeekSelected { get; set; }
        public int dueDateRepeatOptionSelected { get; set; }
        public DateTime? DueDateRepeat { get; set; }


    }
    public class OnDays
    {
        public int value { get; set; }
    }
    public class OnNumberDayOfWeekSelected
    {
        public int key { get; set; }
        public int value { get; set; }
        public string? label { get; set; }
    }
    public class CountDaySelected
    {
        public int key { get; set; }
        public int value { get; set; }
        public string? label { get; set; }

    }
    public class DayOfWeekSeleted
    {
        public int key { get; set; }
        public int value { get; set; }
        public bool choose { get; set; }
        public string? label { get; set; }
        public string? name { get; set; }
    }
    public class OnDayOfWeekSelected
    {
        public int key { get; set; }
        public int value { get; set; }
        public bool choose { get; set; }
        public string? label { get; set; }
        public string? name { get; set; }
    }

    public class RepeatTasksEntityWithPermision : RepeatTasksEntity
    {
        public RepeatPermission permission { get; set; } = new RepeatPermission();
    }
    public class RepeatTasksEntity
    {
        public string? GuidID { get; set; }
        public string? Title { get; set; }
        public int HistoryID { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Unspecified)]
        public DateTime? StartDate { set; get; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Unspecified)]
        public DateTime? DueDate { set; get; }
        public CUserLookupValue? Assignee { get; set; }
        public int Status_Code { get; set; }
        public string? Status_string { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Unspecified)]
        public DateTime? Date_Finished { set; get; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Unspecified)]
        public DateTime? Created { set; get; }
        //public double Time_SLA { set; get; }
        public double Time_SLA_Finish { set; get; }
        public int PercentComplete { set; get; }
        public double Rate { set; get; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Unspecified)]
        public DateTime? DateTimeSystemCreated { set; get; } = DateTime.Now;
    }

    public class TaskReport
    {
        //public double TaskCompleted { get; set; }
        //public double TaskCompletedOverdue { get; set; }

        //public double TaskCompletedOverdue { get; set; }
        public CFieldLookupValue Department { set; get; }
        public CFieldLookupValue Manager { set; get; }
        public string Code { set; get; }
        public string WorkGroup { set; get; }
        public int Finished { set; get; }
        public int Reject { set; get; }
        public int InProcess { set; get; }
        public int OutOfDate { set; get; }
        public int FinishedOutOfDate { set; get; }

    }

    public class TaskReportObject
    {
        public int Finished { set; get; }
        public int InProcess { set; get; }
        public int FinishedOverdue { set; get; }
        public int DueDate { set; get; }

    }

    #region ReportDashBoard
    public class TotalDashboard : TaskReportObject
    {

    }
    public class MeDashboard : TaskReportObject
    {

    }
    public class ResourceReport
    {
        public string Name { get; set; }
        public int Total { get; set; }
    }
    public class DepartmentDashboard : TaskReportObject
    {
        public CFieldLookupValue Department { get; set; }
    }
    public class PriorityDashboard : TaskReportObject
    {
        public string Urgency { get; set; }
        public string high { get; set; }
        public string Normal { get; set; }
        public string Low { get; set; }
    }
    public class ProjectDashboard : TaskReportObject
    {
        public CFieldLookupValue Project { get; set; }

    }
    public class ReportDashboard
    {
        public List<ResourceReport> _ResourceReport { get; set; } = new List<ResourceReport>();
        public TotalDashboard _TotalDashboard { get; set; }
        public MeDashboard _MeDashboard { get; set; }
        public List<DepartmentDashboard> _DepartmentDashboard { get; set; } = new List<DepartmentDashboard>();
        public List<ProjectDashboard> _ProjectDashboard { get; set; } = new List<ProjectDashboard>();
        public List<PriorityDashboard> _PriorityDashboard { get; set; } = new List<PriorityDashboard>();
        public ReportDashboard()
        {
            _TotalDashboard = new TotalDashboard();
            _MeDashboard = new MeDashboard();

        }
    }
    public class ReportProjectDashboard
    {
        public TotalDashboard _TotalDashboard { get; set; }
        public MeDashboard _MeDashboard { get; set; }
        public List<DepartmentDashboard> _DepartmentDashboard { get; set; } = new List<DepartmentDashboard>();
        public List<ReportProjectMember> ReportProjectMember { get; set; } = new List<ReportProjectMember>();
        // public List<PriorityDashboard> _PriorityDashboard { get; set; } = new List<PriorityDashboard>();
        public ReportProjectDashboard()
        {
            _TotalDashboard = new TotalDashboard();
            _MeDashboard = new MeDashboard();

        }
    }
    public class ReportProjectMember : TaskReportObject
    {
        public CFieldLookupValue Member { get; set; }
    }
    #endregion
}
