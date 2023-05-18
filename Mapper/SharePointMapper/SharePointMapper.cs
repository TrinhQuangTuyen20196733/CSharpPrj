using Microsoft.SharePoint.Client;
using System.Reflection;

namespace BHDStarBooking.Mapper.SharePointMapper
{
    public class SharePointMapper<T>
    {
        public  static ListItem AddListItem(T obj, List list)
        {
            ListItemCreationInformation itemCreateInfo = new ListItemCreationInformation();
            ListItem newItem = list.AddItem(itemCreateInfo);
            PropertyInfo[] properties = obj.GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                string propertyName = property.Name;
                object propertyValue = property.GetValue(obj);
                if (propertyValue != null)
                {
                    newItem[propertyName] = propertyValue;
                }
            }

            newItem.Update();
            list.Context.ExecuteQuery();

            return newItem;
        }
    }
}
