using Microsoft.SharePoint.Client;
using System.Reflection;

namespace BHDStarBooking.Utils
{
    public static class SharePointConvert
    {
        public static TEntity ListItemToObject<TEntity>(ListItem item) where TEntity :  new()
        {
            TEntity obj = new TEntity();
            PropertyInfo[] properties = obj.GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                string propertyName = property.Name;
                if (propertyName == "Id")
                {
                  /*  object propertyValue = item["id0"];
                    property.SetValue(obj, propertyValue, null);*/
                }
                else if (propertyName == "title")
                {
                    object propertyValue = item["title0"];
                    property.SetValue(obj, propertyValue, null);
                }

                else if (item.FieldValues.ContainsKey(propertyName))
                {
                    object propertyValue = item[propertyName];
                    if (propertyValue is FieldLookupValue lookupValue)
                    {
                        propertyValue = lookupValue.LookupValue;
                        int value = int.Parse(propertyValue.ToString());
                        property.SetValue(obj, value, null);

                    }
                    else if (propertyValue is IEnumerable<FieldLookupValue> lookupValues)
                    {
                        // Multiple lookup values
                        List<int> values = new List<int>();
                        foreach (FieldLookupValue lookupValue1 in lookupValues)
                        {
                            int value = int.Parse(lookupValue1.LookupValue.ToString());
                            values.Add(value);
                        }
                        property.SetValue(obj, values, null);
                    }
                    else
                    {


                        property.SetValue(obj, propertyValue, null);

                    }

                }

            }

            return obj;
        }
    }
}
