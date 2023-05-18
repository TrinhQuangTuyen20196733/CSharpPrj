using SPO.CORE.DaTaAccessLayer;
using SPO.CORE.TypeCAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPS.MASS.MODEL.BaseEntity
{
    public class BaseComment: BaseItem
    {

        public List<CUserLookupValue>? TaskGroup { set; get; }


    }
}
