using SPO.CORE.TypeCAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPS.MASS.MODEL.SPEntity
{
    public class Employee_LeaveEntity
    {
        public int ID { get; set; }
        public CUserLookupValue Employee { get; set; }
        public string Detail { get; set; }


    }
}
