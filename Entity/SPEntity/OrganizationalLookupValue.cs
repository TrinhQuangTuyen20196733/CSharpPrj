using MongoDB.Bson.Serialization.Attributes;
using SPO.CORE;
using SPO.CORE.TypeCAttribute;
using SPS.MASS.MODEL.Entity;
using SPS.MASS.MODEL.Util;


namespace SPS.MASS.MODEL.SPEntity
{
    public static class OrganizationalLookupValueExtent
    {
        public static CFieldLookupValue SysrchOrganizational(this CFieldLookupValue cFieldLookupValue)
        {
            if (cFieldLookupValue != null && cFieldLookupValue.LookupId > 0)
            {
                string cacheKey = NameCache.OrganizationalCache;
                List<OrganizationalEntity> lst = new List<OrganizationalEntity>();
                var GetValueCachek = MemoryCachingModel.Get(cacheKey);
                if (GetValueCachek != null)
                {
                    lst = (List<OrganizationalEntity>)GetValueCachek;
                    var FisrtItem = lst.FirstOrDefault(d => d.ID == cFieldLookupValue.LookupId);
                    if (FisrtItem != null)
                    {
                        cFieldLookupValue.LookupValue = FisrtItem.Title;
                        return cFieldLookupValue;
                    }
                }
            }
            return new CFieldLookupValue();
        }
        //
        public static List<CFieldLookupValue> SysrchOrganizational(this List<CFieldLookupValue> cFieldLookupValue)
        {
            if (cFieldLookupValue != null && cFieldLookupValue.Count > 0)
            {
                string cacheKey = NameCache.OrganizationalCache;
                List<OrganizationalEntity> lst = new List<OrganizationalEntity>();
                var GetValueCachek = MemoryCachingModel.Get(cacheKey);
                foreach (var d in cFieldLookupValue)
                {
                    if (GetValueCachek != null)
                    {
                        lst = (List<OrganizationalEntity>)GetValueCachek;
                        var FisrtItem = lst.FirstOrDefault(x => x.ID == d.LookupId);
                        if (FisrtItem != null)
                        {
                            d.LookupValue = FisrtItem.Title;
                            return cFieldLookupValue;
                        }
                    }
                }
            }
            return new List<CFieldLookupValue>();
        }
    }
}
