using SPO.CORE.DaTaAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPO.CORE.TypeCAttribute;
using System.Text.Json.Serialization;
using SPS.MASS.MODEL.BaseEntity;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json;
using System.Reflection.Emit;
using Microsoft.SharePoint.Administration.TenantAdmin;
using SPS.MASS.MODEL.SPEntity;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Security.Cryptography.Pkcs;

namespace SPS.MASS.MODEL.Entity
{

    public static class WF_BsonObject
    {
        public static WF_ReQuestEntity ConvertBsonObject(this WF_ReQuestEntity entity)
        {
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(entity.MetaData) && !string.IsNullOrEmpty(entity.MetaDataRaw))
                {
                    //Cần phải sử dụng BsonDocument
                    entity.MetaDataObj = BsonDocument.Parse(entity.MetaData); //
                    entity.MetaDataRawObj = BsonDocument.Parse(entity.MetaDataRaw);
                }
                if (!string.IsNullOrEmpty(entity.MetaDataProcess))
                {
                    entity.MetaDataProcessObj = JsonSerializer.Deserialize<List<MetaDataProcess>>(entity.MetaDataProcess);
                }
            }
            return entity;
        }
    }
    public class WF_ReQuestEntity : WF_ReQuestEntityView
    {

        public string? WorkFlowConfig { get; set; } // Config của Workflow hiện tại
        public CFieldLookupValue? Author_Profile { get; set; }
        public CFieldLookupValue? Department_Request_x003a_WorkGro { get; set; }
        public List<CFieldLookupValue> Department_Current_Process { get; set; } = new List<CFieldLookupValue>();
        public string? Current_Step { get; set; }
        public string? Description { get; set; }
        public string? Previous_Step { get; set; }
        public string? Next_Step { get; set; }
        public string? CurrentWorkflow { get; set; } = "{\"preStep\":null,\"current\":null,\"nextStep\":null}";
        public string? Content_Raw { get; set; }
        public string? RejectedReason { get; set; }
        public string? Task_Process { get; set; }
        public string? FilesLink { get; set; }
        public string? Fields { get; set; }
        public string? MetaData { get; set; }
        public string? MetaDataRaw { get; set; }
        [SPListItemCustomize]
        [JsonIgnore]
        public BsonDocument? MetaDataRawObj { get; set; }
        [SPListItemCustomize]
        [JsonIgnore]
        public BsonDocument? MetaDataObj { get; set; }
        public List<CFieldLookupValue> Assignor_Profile { get; set; } = new List<CFieldLookupValue>();
        public string? HistoryCurrentWorkflow { get; set; }
        public string? ReturnedReason { get; set; }
        public string? MetaDataProcess { get; set; }
        [SPListItemCustomize]
        [JsonIgnore]
        public List<MetaDataProcess>? MetaDataProcessObj { get; set; } = new List<MetaDataProcess>();
        //Reassigned
        
        public string? Reassigned_Profile { get; set; }
    }
    //Reassigned Object
    public class Reassigned_Profile
    {
        public UserAssignor_Assignee UserAssignor_AssigneeProcess { get; set; } = new UserAssignor_Assignee();
        public UserReassignedProcess UserProcess { get; set; } = new UserReassignedProcess();

        [BsonDateTimeOptions(Kind = DateTimeKind.Unspecified)]
        public DateTime? CreateDate { get; set; }

    }
    public class UserReassignedProcess
    {
        public int ID { get; set; }
        public string? Title { get; set; }
        public CFieldLookupValue? UserAD { get; set; }
        public CFieldLookupValue? JobTitle { get; set; }
        public CFieldLookupValue? Department { get; set; }


    }
    public class UserAssignor_Assignee
    {
        public int ID { get; set; }
        public string? Title { get; set; }
        public CFieldLookupValue? UserAD { get; set; }
        public CFieldLookupValue? JobTitle { get; set; }
        public CFieldLookupValue? Department { get; set; }


    }

    
    public class WF_ReQuestEntityDetail : WF_ReQuestEntity
    {
        public string? CurrentWorkflowEncode { get; set; }
        public RequestPermission Permission { get; set; }
        public double TimeLeft { get; set; }

        public bool ReassignDelegate { get; set; } = false;

    }

    public class WF_ReQuestEntityView : BaseItem
    {
        public CFieldLookupValue? WorkFlow_Name { get; set; }
        public List<CUserLookupValue>? UsersFollower { get; set; }
        public int Priority { get; set; }
        public List<CUserLookupValue>? Assignee { get; set; }
     
        public CUserLookupValue? Assignor { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Unspecified)]
        public DateTime? SentDate { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Unspecified)]
        public DateTime? DueDate { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Unspecified)]
        public DateTime? StepFinishDate { get; set; }
        public double Time_SLA { get; set; }
        public CFieldLookupValue? Department_Request { get; set; }
        public string? RequestStatusString { get; set; }
        public int Request_Status { get; set; }
        public List<CFieldLookupValue> Assignee_Profile { get; set; } = new List<CFieldLookupValue>();
        public List<CUserLookupValue>? UsersUnread { get; set; } = new List<CUserLookupValue>();
        public string? Priority_Reason { get; set; }
        public string? Json_Assignee_CUserProfile { get; set; }
        public string? Json_Assignor_CUserProfile { get; set; }
        public string? Viewers { get; set; }

        public List<CUserLookupValue>? Reassigned { get; set; }


    }

    public class WF_ReQuestEntityViewVM : WF_ReQuestEntityView
    {
        public int WorkflowActionType
        {
            get; set;
        }
     
        public int CommentCount { get; set; }
        public int FilesLinkCount { get; set; }
        public string? FilesLink { get; set; }
        // public bool Delete { get; set; } = false;
        public RequestPermission Permission { get; set; }
        public WF_ReQuestEntityViewVM()
        {
            Permission = new RequestPermission();
        }
    }
    public class WF_ReQuestEntityPost : BaseAddItem
    {
        public List<CUserProfile>? Assignee_Profile_CUserProfile { get; set; }
        public int Priority { get; set; }
        public string? Priority_Reason { get; set; }
        public string? ApprovalNote { get; set; }
        public double Time_SLA { get; set; }
        public List<CUserLookupValue>? UsersFollower { get; set; }
        public string? RequestStatusString { get; set; }
        [JsonIgnore]
        public string? MetaData { get; set; }
        public string? MetaDataRaw { get; set; }
        public string? StrMetaData { get; set; }
        public string? FilesLink { get; set; }
        public int Request_Status { get; set; } //nếu =200 tức là kết thúc quy trình

        [JsonIgnore]
        public string? HistoryCurrentWorkflow { get; set; }


    }

    public class WF_ReQuestEntityAddNew : BaseAddItem
    {
        [SPListItemCustomize]
        [BsonIgnore]
        public int WorkflowID { get; set; }
        [JsonIgnore]
        public CFieldLookupValue? WorkFlow_Name { get; set; } // Workflow nào
        [JsonIgnore]
        public string? WorkFlowConfig { get; set; } // Config của Workflow hiện tại
        public CFieldLookupValue? Department_Request { get; set; } // 
        [JsonIgnore]
        public CUserLookupValue? Assignor { get; set; }
        [SPListItemCustomize]
        [BsonIgnore]
        public List<CUserProfile>? Assignee_Profile_CUserProfile { get; set; }
        [JsonIgnore]
        public CFieldLookupValue? Author_Profile { get; set; } // 
        [JsonIgnore]
        public string? Json_Assignee_CUserProfile { get; set; }
        [JsonIgnore]
        public string? Json_Assignor_CUserProfile { get; set; }
        [JsonIgnore]
        public List<CUserLookupValue>? Assignee { get; set; } = new List<CUserLookupValue>();
        [JsonIgnore]
        public List<CFieldLookupValue>? Assignee_Profile { get; set; } = new List<CFieldLookupValue>();
        [JsonIgnore]
        public List<CFieldLookupValue>? Assignor_Profile { get; set; } = new List<CFieldLookupValue>();
        [JsonIgnore]
        public List<CFieldLookupValue> Department_Current_Process { get; set; } = new List<CFieldLookupValue>();
        public int Priority { get; set; }
        public string? Priority_Reason { get; set; }
        public string? Description { get; set; }
        public string? Previous_Step { get; set; }
        public string? Next_Step { get; set; }
        public string? Current_Step { get; set; }
        public string? Content_Raw { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Unspecified)]
        [JsonIgnore]
        public DateTime? SentDate { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Unspecified)]
        [JsonIgnore]
        public DateTime? DueDate { get; set; }
        public string? CurrentWorkflow { get; set; } = "{\"preStep\":null,\"current\":null,\"nextStep\":null}";
        public double Time_SLA { get; set; }
        public int Request_Status { get; set; }
        public List<CUserLookupValue>? UsersFollower { get; set; }
        public string? RequestStatusString { get; set; }
        [JsonIgnore]
        public string? Fields { get; set; }
        public string? MetaData { get; set; }
        public string? MetaDataRaw { get; set; }
        public string? FilesLink { get; set; }
        [JsonIgnore]
        public string? HistoryCurrentWorkflow { get; set; }
        [SPListItemCustomize]
        [JsonIgnore]
        public BsonDocument? MetaDataRawObj { get; set; }
        [SPListItemCustomize]
        [JsonIgnore]
        public BsonDocument? MetaDataObj { get; set; }
        [JsonIgnore]
        public string? MetaDataProcess { get; set; }
        [SPListItemCustomize]
        [JsonIgnore]
        public List<MetaDataProcess>? MetaDataProcessObj { get; set; } = new List<MetaDataProcess>();

    }
    public class WF_Return
    {
        public List<CUserProfile>? Assignee_Profile_CUserProfile { get; set; } = new List<CUserProfile>();
        public double Time_SLA { get; set; }
        public string ReturnedReason { get; set; }
        public int Request_Status { get; set; }
        public string RequestStatusString { get; set; }
        public string? MetaData { get; set; }
        public string? MetaDataRaw { get; set; }
        public string? StrMetaData { get; set; }
    }
    public class WF_ItemHistory
    {
        public string Key { get; set; }
        public object CurrentWorkflowAfterAprroval { get; set; }
        public object CurrentWorkflowBeforeApproval { get; set; }
        public CFieldLookupValue Assignee_Profile { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Unspecified)]
        public DateTime? DateTimeProcess { get; set; }
        //public CFieldLookupValue Author_Profile_Process { get; set; }
        public string ConvertObjecTostring(List<WF_ItemHistory> lstObect)
        {
            JsonSerializerOptions jso = new JsonSerializerOptions();
            jso.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
            var strObject = JsonSerializer.Serialize(lstObect, jso);
            return strObject;
        }

    }

    public class WorkFlowReport
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
    public class WF_Report
    {
        public CFieldLookupValue Workflow { set; get; }
        //public CFieldLookupValue Manager { set; get; }
        //public string Code { set; get; }
        public string WorkGroup { set; get; }
        public int Finished { set; get; }
        public int Reject { set; get; }
        public int InProcess { set; get; }
        public int OutOfDate { set; get; }
        public int FinishedOutOfDate { set; get; }
    }

    #region ReportDashBoard
    public class TotalReqeustDashboard : TaskReportObject
    {

    }

    public class ReQuestFinshDashboard
    {
        public int Finished { get; set; }
        public int Reject { get; set; }
    }
    public class ReQuestDepartmentDashboard : TaskReportObject
    {
        public CFieldLookupValue Department { get; set; }
    }
    public class RequestStepDashboard : TaskReportObject
    {
        public string Step { get; set; }
    }

    public class RequestTimeDepartmentLSADashboard
    {
        public List<double> LstAveragedLSA { get; set; } = new List<double>();
        public CFieldLookupValue Department { get; set; }
        public double AveragedLSA { get; set; }
    }
    public class RequestTimeStepLSADashboard
    {
        public List<double> LstAveragedLSA { get; set; } = new List<double>();
        public string Step { get; set; }
        public double AveragedLSA { get; set; }
    }
    public class RequestReportDashBoard
    {
        public int Total_ReQuest_process { get; set; }
        public RequestReport requestReport { get; set; } = new RequestReport();
        public TotalReqeustDashboard _TotalReqeustDashboard { get; set; }
        public ReQuestFinshDashboard _ReQuestFinshDashboard { get; set; }
        public List<RequestTimeDepartmentLSADashboard> _RequestTimeDepartmentLSADashboard { get; set; } = new List<RequestTimeDepartmentLSADashboard>();
        public List<RequestTimeStepLSADashboard> _RequestTimeStepLSADashboard { get; set; } = new List<RequestTimeStepLSADashboard>();
        public List<ReQuestDepartmentDashboard> _ReQuestDepartmentDashboard { get; set; } = new List<ReQuestDepartmentDashboard>();
        public List<RequestStepDashboard> _RequestStepDashboard { get; set; } = new List<RequestStepDashboard>();
        public RequestReportDashBoard()
        {
            _TotalReqeustDashboard = new TotalReqeustDashboard();
            _ReQuestFinshDashboard = new ReQuestFinshDashboard();

        }
    }
    //


    public class RequestReportDashBoardALL
    {
        public int TotalWorkFlow { get; set; }

        public RequestReport? requestReport { get; set; } = new RequestReport();
        public RequestTask_Process? Total_ReQuest_process { get; set; } = new RequestTask_Process();
        public List<WorkFlowName> Workflow { get; set; } = new List<WorkFlowName>();
        public List<ReQuestDepartmentDashboard> _ReQuestDepartmentDashboard { get; set; } = new List<ReQuestDepartmentDashboard>();
        public List<RequestTimeDepartmentLSADashboard> _RequestTimeDepartmentLSADashboard { get; set; } = new List<RequestTimeDepartmentLSADashboard>();
        public class RequestTask_Process : TaskReportObject
        {
            public int Reject { get; set; }
            public int Return { get; set; }
        }
        public class WorkFlowName : RequestTask_Process
        {
            public WorkFlowName()
            {
            }
            public string Name { get; set; }
        }

    }

    public class RequestReport
    {
        public int TotalRequest { get; set; }
        public int TotalRequestProcess { get; set; }
        public int TotalRequestFinished { get; set; }
        public int TotalRequestReject { get; set; }
    }

    #endregion

    #region MetaDataProcess

    public class MetaDataProcess
    {
        public CFieldLookupValue? UserProcess { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Unspecified)]
        public DateTime? StartDate { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Unspecified)]
        public DateTime? DueDate { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Unspecified)]
        public DateTime? FinishDate { get; set; }
        public string? StepName { get; set; }
        public double SLA { get; set; }
        public double SLA_Finish { get; set; }
        //  public int? StatusCode { get; set; }
        public MetaDataProcess()
        {
            UserProcess = new CFieldLookupValue();
        }
    }

    #endregion
}
