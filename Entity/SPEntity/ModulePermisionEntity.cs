using SPO.CORE.TypeCAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPS.MASS.MODEL.SPEntity
{
    public class ModulePermisionEntity
    {
        public string Title { get; set; }
        public string Code { get; set; }
        public List<CUserLookupValue> UserAD { get; set; }


    }
}
