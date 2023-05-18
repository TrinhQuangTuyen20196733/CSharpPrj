using Microsoft.BusinessData.Runtime;
using SPO.CORE.DaTaAccessLayer;
using SPO.CORE.TypeCAttribute;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SPS.MASS.MODEL.SPEntity
{
    public class CategoryLabel
    {
        public const string TaskGroup = "task-group";
        public const string Project = "project";
    }
    public class Task_GroupEntity : BaseCache
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public CFieldLookupValue? Group { get; set; }
        public List<CUserLookupValue>? Members { get; set; }
        public List<CUserLookupValue>? OwnerGroup { get; set; }

        [JsonIgnore]
        public CUserLookupValue? Author { get; set; }
        public string? LabelContent { get; set; }
        [SPListItemCustomize]
        public List<LabelContentEntity> LabelContentObj { get; set; } = new List<LabelContentEntity>();
        [SPListItemCustomize]
        public TaskGroupPermission? permission { get; set; }



    }
    public class Task_GroupEntityAddnew : BaseCache
    {
        public string Title { get; set; }
        public string Group { get; set; }
        public List<CUserLookupValue>? Members { get; set; }
        public List<CUserLookupValue>? OwnerGroup { get; set; }
    }
    public class Task_GroupSubEntityAddnew : BaseCache
    {
        public string Title { get; set; }
        public CFieldLookupValue Group { get; set; }
        public List<CUserLookupValue>? Members { get; set; }
        public List<CUserLookupValue>? OwnerGroup { get; set; }
    }
    public class Task_GroupEntityUpdate : BaseCache
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public CFieldLookupValue? Group { get; set; }
    }

    public class LabelContentEdit
    {
        public string GuidID { get; set; }
        public string Title { get; set; }
    }

    public class LabelContentEntity
    {
        public string? GuidID { get; set; }
        public string Title { get; set; }
        public int GroupID { get; set; }
        public string Category { get; set; }
        public int? Order { get; set; }
    }
    public class MenuTaskGroup
    {
        public CFieldLookupValue GroupLabel { get; set; }
        public List<CFieldLookupValue> GroupItems { get; set; } = new List<CFieldLookupValue>();
    }

    public class TaskGroupPermission
    {
        public bool Deleteline { get; set; }
        public bool Editline { get; set; }
        public bool Addline { get; set; }
        public bool Add { get; set; }
        public bool Delete { get; set; }
        public bool Edit { get; set; }
        public bool AddMember { get; set; }
        public bool AddOwner { get; set; }
        public bool DeleteMember { get; set; }
        public bool DeleteOwner { get; set; }
        public TaskGroupPermission(Task_GroupEntity entity, SPCurrentUser sPCurrentUser)
        {
            if (entity.Author != null && entity.Author.LookupId == sPCurrentUser.UserAD.Id)
            {
                //Thằng tạo ra thì có mọi quyền
                AddMember = true;
                AddOwner = true;
                DeleteMember = true;
                DeleteOwner = true;
                Add = true;
                Delete = true;
                Edit = true;
                Addline = true;
                Editline = true;
                Deleteline = true;
                //Nếu thằng này là cấp con tức là sub thì không được tạo thêm Sub nữa
                if (entity.Group != null && entity.Group.LookupId > 0)
                {
                    Add = false;

                }
                //Ngược lại nếu thằng này là cấp Cha tức là Group = null
                else
                {
                    //không được tạo line
                    Editline = false;
                    Deleteline = false;
                    Addline = false;
                    //Không được add member
                    AddMember = false;
                    AddOwner = false;
                    DeleteMember = false;
                    DeleteOwner = false;
                }
            }
            if (sPCurrentUser.UserAD.IsSiteAdmin)
            {
                Editline = true;
                Deleteline = true;
                AddMember = true;
                AddOwner = true;
                DeleteMember = true;
                DeleteOwner = true;
                Add = true;
                Delete = true;
                Edit = true;
                Addline = true;
                if (entity.Group != null && entity.Group.LookupId > 0)
                {
                    Add = false;

                }
                //Ngược lại nếu thằng này là cấp Cha tức là Group = null
                else
                {
                    //không được tạo line
                    Addline = false;
                    //Không được add member
                    AddMember = false;
                    AddOwner = false;
                    DeleteMember = false;
                    DeleteOwner = false;
                    Editline = false;
                    Deleteline = false;
                }
            }
            if (entity.OwnerGroup != null && entity.OwnerGroup.Any(x => x.LookupId == sPCurrentUser.UserAD.Id))
            {
                Editline = true;
                Deleteline = true;
                Addline = true;
                Edit = true;
                AddMember = true;
                DeleteMember = true;
            }
        }

    }

}
