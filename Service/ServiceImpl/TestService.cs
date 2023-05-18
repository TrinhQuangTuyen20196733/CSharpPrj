using BHDStarBooking.Config.MongoConfig;
using BHDStarBooking.Config.SharePointConfig;
using BHDStarBooking.ExceptionHandler.ExceptionModel;
using BHDStarBooking.Service.IService;
using BHDStarBooking.Utils;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.News.DataModel;
using MongoDB.Driver;
using SPO.CORE.Utility;
using SPS.MASS.MODEL;
using SPS.MASS.MODEL.SPEntity;
using System.Collections;
using System.Reflection;
using IMongoBaseContext = BHDStarBooking.Config.MongoConfig.IMongoBaseContext;

namespace BHDStarBooking.Service.ServiceImpl
{
    public class TestService : ITestService
    {
        public ISharePointBaseContext _context;
        private IMongoBaseContext _MongoContext;
        private IMongoDatabase mongoDataBase;
        private readonly Web web;

        public TestService(ISharePointBaseContext context, IMongoBaseContext _MongoContext)
        {
            _context = context;
            web = _context.context.Web;
            this._MongoContext = _MongoContext;
            mongoDataBase = _MongoContext.Database;
        }
        public string GetEntityName(string listTitle, Type entityType)
        {
            Type type = entityType;

            foreach (FieldInfo field in type.GetFields())
            {
                if (string.Equals(field.GetValue(null), listTitle))
                {
                    string propertyName = field.Name;
                    return propertyName;
                }
            }

            return null;
        }

        public async void DataAsync()
        {
            _context.context.Load(web.Lists,
             lists => lists.Include(list => list.Title,
                                    list => list.Id));

            _context.context.ExecuteQuery();
            var collectionNames = mongoDataBase.ListCollectionNames().ToList();

            foreach (var collectionName in collectionNames)
            {
                mongoDataBase.DropCollection(collectionName);
            }

            Console.WriteLine("Đã xóa tất cả các collection trong database.");
            foreach (List list in web.Lists)
            {
                if (list != null && (list.Title == "WF_ReQuest" || list.Title == "User" || list.Title == "Movie"))
                {
                    string EntityName = GetEntityName(list.Title, typeof(SPointTitleList));

                    Type type1 = typeof(MongoCollectionList);
                    string collectionName = (string)type1.GetField(EntityName)?.GetValue(null);

                    await mongoDataBase.CreateCollectionAsync(collectionName);

                    if (!string.IsNullOrEmpty(EntityName))
                    {
                        CamlQuery query = CamlQuery.CreateAllItemsQuery();
                        ListItemCollection items = list.GetItems(query);

                        _context.context.Load(items);
                        _context.context.ExecuteQuery();

                        Type type2 = typeof(SharePointListToEntity);
                        string fullNameEntity = (string)type2.GetField(list.Title)?.GetValue(null);

                        Type type = Type.GetType(fullNameEntity);
                        var instance = Activator.CreateInstance(type);


                        foreach (ListItem listItem in items)
                        {
                            object[] parameters = new object[] { listItem, "" };
                            var method = typeof(SPSVNCoreExtensions).GetMethod("ToListTObject", new Type[] { typeof(ListItem), typeof(string) });
                            var obj = method.MakeGenericMethod(type).Invoke(null, parameters);
                            instance = Convert.ChangeType(obj, type);
                            PropertyInfo idProperty = type.GetProperty("Id");
                            idProperty.SetValue(instance, null);
                            MethodInfo insertMethod = _MongoContext.GetType().GetMethod("InsertDataX").MakeGenericMethod(type);
                            insertMethod.Invoke(_MongoContext, new object[] { collectionName, instance });

                        }

                    }

                    else
                    {
                        throw new CastingException("Can't cast list on Share Point to Object");
                    }

                }
            }
        }

    }
}
