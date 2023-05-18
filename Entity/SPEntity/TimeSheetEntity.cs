using SPO.CORE.TypeCAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPS.MASS.MODEL.SPEntity
{
    public class TimeSheetEntity
    {
        public int ID { get; set; }
        public string? Title { get; set; }
        public string? WorkOfWeek { get; set; }
        public CFieldLookupValue UserProfile { get; set; }
        public CUserLookupValue UserAD { get; set; }
        public CFieldLookupValue Workday { get; set; }

    }


}
