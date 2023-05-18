using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPO.CORE.DaTaAccessLayer;
using SPO.CORE.TypeCAttribute;

namespace SPS.MASS.MODEL.Entity
{
    public class WorkFlow_NameEntity : BaseListItem
    {
        public List<CFieldLookupValue>? Roles_Group { get; set; }
        public CFieldLookupValue? Department { get; set; }
        public string? Workflow_Design { get; set; }

        public string? Workflow_Config { get; set; }

        public CFieldLookupValue? Department_x003a_Code { get; set; }
        public string? Description { get; set; }
        public string? MetaData { get; set; }
        public string? Fields { get; set; }
        public string? Document_Url { get; set; }
        public string? Code { get; set; }
        //
    }
    public class WorkFlowDetail : WorkFlow_NameEntity
    {
        //public object Workflow_Config_Obj { get; set; }

        //public object? Workflow_Design_Obj { get; set; }

        //public object? MetaData_Obj { get; set; }
        //public object? Fields_Obj { get; set; }
        public List<CUserLookupValue>? UserView { get; set; } = new List<CUserLookupValue>();
        public object Permission { get; set; } = new { Add = false };
    }

    public class WorkflowView : BaseListItem
    {
        public List<CFieldLookupValue>? Roles_Group { get; set; }
        public CFieldLookupValue? Department { get; set; }
        public string? Description { get; set; }
        public CFieldLookupValue? Department_x003a_Code { get; set; }
        public WorkFlowPermission Permission { get; set; } = new WorkFlowPermission();
        public List<CUserLookupValue> UserView { get; set; } = new List<CUserLookupValue>();
        public string? Code { get; set; }

    }


    public class AddEditNewWorkflow
    {
        public string Title { get; set; }
        public CFieldLookupValue? Department { get; set; }

        public List<CFieldLookupValue>? Roles_Group { get; set; }
        public string? Description { get; set; }
        public List<CUserLookupValue>? UserView { get; set; } = new List<CUserLookupValue>();
    }

    public class UpdateConfigWorkflow
    {

        public string? Workflow_Design { get; set; }
        public string? Workflow_Config { get; set; }
        public string? MetaData { get; set; }
        public string? Fields { get; set; }



    }
    public class UpdateWorkflow
    {
        public string Title { get; set; }
        public List<CUserLookupValue>? UserView { get; set; } = new List<CUserLookupValue>();
        public string? Description { get; set; }
        public List<CFieldLookupValue>? Roles_Group { get; set; } = new List<CFieldLookupValue>();
        public CFieldLookupValue? Department { get; set; }
        public string? Code { get; set; }

    }


    public class WorkFlowConfig
    {
        public string type { get; set; }
        public Metadata metadata { get; set; } = new Metadata();
    }
    public class Metadata
    {
        public string? title { get; set; }
    }
}
