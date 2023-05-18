using Microsoft.SharePoint.Client.Publishing;
using SPO.CORE.TypeCAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPS.MASS.MODEL.BaseEntity
{
    public class DepartmentLookup : CFieldLookupValue
    {
        public bool MyDepartment { get; set; } = false;
        public bool AssignedDepartment { get; set; } = false;
        public int CountTask { get; set; }
        public CFieldLookupValue DepartmentParent { get; set; }
        public DepartmentLookup()
        {
            DepartmentParent = new CFieldLookupValue();
        }
    }
}
