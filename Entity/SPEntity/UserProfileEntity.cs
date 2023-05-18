using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using SPO.CORE.DaTaAccessLayer;
using SPO.CORE.TypeCAttribute;
using SPS.MASS.MODEL.BaseEntity;

namespace SPS.MASS.MODEL.Entity
{
    public class UserProfileEntity : CUserProfileWithAvatar
    {
        public string? Email { get; set; }
        public bool MainAccount { get; set; }
        public string? UserName { get; set; }
        public bool IsSiteAdmin { get; set; } = false;
        public bool DisableAccount { get; set; } = false;
        public UserProfileEntity()
        {
            JobTitle = new CFieldLookupValue();
            Department = new CFieldLookupValue();
            
            Roles_Group = new List<CFieldLookupValue>();
        }
    }

    public class UserProfileEntityAddNew : BaseOnLyListItem
    {
        public bool MainAccount { get; set; } = false;
        public string UserName { get; set; }
        public string? Email { get; set; }
        public CFieldLookupValue JobTitle { get; set; }
        public CFieldLookupValue Department { get; set; }
        public List<CFieldLookupValue>? Roles_Group { get; set; }
        public CUserLookupValue? UserAD { get; set; }
    }
}
