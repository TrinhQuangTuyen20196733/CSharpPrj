using BHDStarBooking.Config.SharePointConfig;
using BHDStarBooking.Entity;
using BHDStarBooking.ExceptionHandler.ExceptionModel;
using CamlexNET;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Search.Query;
using Microsoft.SharePoint.News.DataModel;
using System.Collections;
using System.Diagnostics.Eventing.Reader;
using System.Linq.Expressions;
using System.Reflection;

namespace BHDStarBooking.Repository
{
    public class SharePointRepository<TEntity> : ISharePointRepository<TEntity> where TEntity : BaseEntity,new()
    {
        public List list { get; }

       

        public ISharePointBaseContext _context;
        public SharePointRepository(ISharePointBaseContext context)
        {
            _context = context;
          list= _context.context.Web.Lists.GetByTitle(typeof(TEntity).Name);
            _context.context.ExecuteQuery();

        }
        public void ObjectToListItem(ListItem newItem,TEntity entity)
        {
            PropertyInfo[] properties = entity.GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                string propertyName = property.Name;
               
                object propertyValue = property.GetValue(entity);
                if (propertyValue != null)
                {
                    if (propertyName == "Id")
                    {

                        newItem["id0"] = propertyValue;
                    }
                    else if (propertyName == "title")
                    {
                        newItem["title0"] = propertyValue;
                    }

                    else if (propertyValue is IList list && list.Count > 1)
                    {
                        {

                            var lookupValues = new ArrayList();
                            var myList = new List<int?>();
                            foreach (var item in list)
                            {
                                myList.Add((int?)item);
                            }
                            foreach (int value in myList)
                            {
                                lookupValues.Add(new FieldLookupValue { LookupId = value });
                            }
                            newItem[propertyName] = lookupValues.ToArray();

                        }
                    }
                    else
                    {
                        newItem[propertyName] = propertyValue;
                    }
                }
            }
        }
        public TEntity ListItemToObject(ListItem item)
        {
            TEntity obj = new TEntity();
            PropertyInfo[] properties = obj.GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                string propertyName = property.Name;
                if (propertyName=="Id")
                {
                    object propertyValue = item["id0"];
                    property.SetValue(obj, propertyValue, null);
                } else if (propertyName == "title")
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
       

        public  TEntity insertListItem(TEntity entity)
        {
            try
            {
                ListItemCreationInformation itemCreateInfo = new ListItemCreationInformation();
                ListItem newItem = list.AddItem(itemCreateInfo);

                ObjectToListItem(newItem, entity);

                newItem.Update();
                list.Context.ExecuteQuery();
                return ListItemToObject(newItem);
            }
            catch
            {
                throw new IndexConstraintException("Không thỏa mãn ràng buộc của index");
            }
         
        }

        public List<TEntity> getAllListItem()
        {
            CamlQuery query = CamlQuery.CreateAllItemsQuery();
            ListItemCollection items = list.GetItems(query);
            list.Context.Load(items);
            list.Context.ExecuteQuery();
            return ListItemToListObject(items);
        }

        private List<TEntity> ListItemToListObject(ListItemCollection items)
        {
            List<TEntity> entities = new List<TEntity>();
            foreach (ListItem item in items)
            {
                entities.Add(ListItemToObject(item));
            }
            return entities;
        }

        public TEntity getListItemById(string id)
        {
            CamlQuery query = new CamlQuery();
            query.ViewXml = "<View><Query><Where><Eq><FieldRef Name='id0' /><Value Type='Text'>" + id + "</Value></Eq></Where></Query></View>";
            ListItemCollection items = list.GetItems(query);
            _context.context.Load(items);
            _context.context.ExecuteQuery();
            if (items.Count > 0)
            {
                ListItem item = items[0];
               return ListItemToObject(item);
            }
            else
            {
                throw new Exception();
            }
        }

        public TEntity updateListItem(TEntity entity)
        {
            CamlQuery query = new CamlQuery();
            query.ViewXml = "<View><Query><Where><Eq><FieldRef Name='id0' /><Value Type='Text'>" + entity.Id + "</Value></Eq></Where></Query></View>";
            ListItemCollection items = list.GetItems(query);
            _context.context.Load(items);
            _context.context.ExecuteQuery();
            if (items.Count > 0)
            {
                ListItem item = items[0];
                ObjectToListItem(item, entity);

                item.Update();
                list.Context.ExecuteQuery();

                return ListItemToObject(item);
            }
            else
            {
                throw new Exception();
            }
        }

        public void deleteListItemById(string id)
        {
            CamlQuery query = new CamlQuery();
            query.ViewXml = "<View><Query><Where><Eq><FieldRef Name='id0' /><Value Type='Text'>" + id + "</Value></Eq></Where></Query></View>";
            ListItemCollection items = list.GetItems(query);
            _context.context.Load(items);
            _context.context.ExecuteQuery();
            if (items.Count > 0)
            {
                ListItem item = items[0];
                item.DeleteObject();
                list.Context.ExecuteQuery();
            }
            else
            {
                throw new Exception();

            }
        }

        public int insertItemAndReturnID(TEntity entity)
        {
            try
            {
                ListItemCreationInformation itemCreateInfo = new ListItemCreationInformation();
                ListItem newItem = list.AddItem(itemCreateInfo);

                ObjectToListItem(newItem, entity);

                newItem.Update();
                list.Context.ExecuteQuery();
                return newItem.Id;
            }
            catch
            {
                throw new IndexConstraintException("Không thỏa mãn ràng buộc của index");
            }
        }


        public int getListItemIDByid(string id)
        {
            CamlQuery query = new CamlQuery();
            query.ViewXml = "<View><Query><Where><Eq><FieldRef Name='id0' /><Value Type='Text'>" + id + "</Value></Eq></Where></Query></View>";
            ListItemCollection items = list.GetItems(query);
            _context.context.Load(items);
            _context.context.ExecuteQuery();
            if (items.Count > 0)
            {
                ListItem item = items[0];
                return item.Id;
            }
            else
            {
                throw new Exception();
            }
        }

        public int updateItemAndReturnID(TEntity entity)
        {
            CamlQuery query = new CamlQuery();
            query.ViewXml = "<View><Query><Where><Eq><FieldRef Name='id0' /><Value Type='Text'>" + entity.Id + "</Value></Eq></Where></Query></View>";
            ListItemCollection items = list.GetItems(query);
            _context.context.Load(items);
            _context.context.ExecuteQuery();
            if (items.Count > 0)
            {
                ListItem item = items[0];
                ObjectToListItem(item, entity);

                item.Update();
                list.Context.ExecuteQuery();

                return item.Id;
            }
            else
            {
                throw new Exception();
            }
        }

        /* private Expression<Func<Microsoft.SharePoint.Client.ListItem, bool>> ConvertToListItemExpression(Expression<Func<TEntity, bool>> predicate)
         {
             var itemParam = Expression.Parameter(typeof(ListItem), "item");
             var visitor = new CamlExpressionVisitor(itemParam);
             var body = visitor.Visit(predicate.Body);
             var listItemPredicate = Expression.Lambda<Func<ListItem, bool>>(body, itemParam);
             return listItemPredicate;
         }*/

    }
}
