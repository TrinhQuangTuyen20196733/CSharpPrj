using Microsoft.AspNetCore.Mvc.Formatters;
using SPO.CORE.TypeCAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace SPS.MASS.MODEL.BaseEntity
{
    public class CUserProfile : CLookupWithUserAD
    {
        public CFieldLookupValue? JobTitle { get; set; }
        public CFieldLookupValue? Department { get; set; }

        public List<CFieldLookupValue>? Roles_Group { get; set; }
        //Department_x003a_WorkGroup
        //Department_x003a__x0020_WorkGrou
        public CFieldLookupValue? Department_x003a_WorkGroup { get; set; }
        public CUserProfile()
        {
            JobTitle = new CFieldLookupValue();
            Department = new CFieldLookupValue();
            Roles_Group = new List<CFieldLookupValue>();
            Department_x003a_WorkGroup = new CFieldLookupValue();
        }
    }

    //   public string? Avatar { get; set; }

    public class CUserProfileWithAvatar : CUserProfile
    {

        public string? Avatar { get; set; }
    }
}
