using Microsoft.SharePoint.Client;

namespace BHDStarBooking.Config.SharePointConfig
{
    public class SharePointIndex
    {
        private ISharePointBaseContext _context;

        public SharePointIndex(ISharePointBaseContext context)
        {
            _context = context;
        }

        public  void  createPrimary()
        {
            ClientContext clientContext = _context.context;
            var web = clientContext.Web;
            var lists = web.Lists;
            clientContext.Load(lists);
            clientContext.ExecuteQuery();
            foreach (var list in lists)
            {
                var fields = list.Fields;
                clientContext.Load(fields);
                clientContext.ExecuteQuery();

                foreach (var field in fields)
                {
                    if (field.InternalName == "id0")
                    {
                        field.Indexed = true;
                        field.EnforceUniqueValues = true;
                        field.Update();
                    }
                }

                clientContext.ExecuteQuery();
            }
        }
        public void createLookUpField()
        {
            ClientContext clientContext = _context.context;
            var web = clientContext.Web;
            var role_AccountList= web.Lists.GetByTitle("Role_Account");
            List roleList = web.Lists.GetByTitle("RoleEntity");
            clientContext.Load(role_AccountList);
            clientContext.Load(roleList);
            clientContext.ExecuteQuery();


            var roleIDField = role_AccountList.Fields.AddFieldAsXml("<Field Type='Lookup' DisplayName='roleID' Name='roleID' />", true, AddFieldOptions.DefaultValue);


            var roleID = clientContext.CastTo<FieldLookup>(roleIDField);



            roleID.LookupList = roleList.Id.ToString();
            roleID.LookupField = "id0";
            roleIDField.Indexed = true;
            roleID.RelationshipDeleteBehavior = RelationshipDeleteBehaviorType.Restrict;
            roleID.Update(); 
           clientContext.ExecuteQuery();
        }


    }
}
