using SPO.CORE.TypeCAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPS.MASS.MODEL.SPEntity
{
    public class WorkingTimeEntity
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public bool? Default { get; set; }
        public List<CFieldLookupValue>? Organezational { get; set; }
        public string Monday { get; set; }
        public string Tuesday { get; set; }
        public string Wednesday { get; set; }
        public string Thursday { get; set; }
        public string Friday { get; set; }
        public string Saturday { get; set; }
        public WorkingTimeEntity()
        {
            Organezational = new List<CFieldLookupValue>();
        }
    }

    public class WorkingHoliday
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public int? Year { get; set; }
    }

    public class DateTimeWorking
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
