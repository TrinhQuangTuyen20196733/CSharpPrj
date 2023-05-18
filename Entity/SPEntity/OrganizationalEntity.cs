using SPO.CORE.DaTaAccessLayer;
using SPO.CORE.TypeCAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SPS.MASS.MODEL.Entity
{
    public class OrganizationalEntity
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public CFieldLookupValue? Parent { get; set; }
        public string WorkGroup { get; set; }
        public CFieldLookupValue? Managers { get; set; }
        public string Code { get; set; }
        public bool? Block { get; set; }=false;

    }


    public class OrganizationalEntityVM : OrganizationalEntity
    {
        public List<OrganizationalEntityVM> lstList { get; set; }
    }





}
