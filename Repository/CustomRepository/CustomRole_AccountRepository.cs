using BHDStarBooking.Config.SharePointConfig;
using BHDStarBooking.Entity.SharePoint;
using Microsoft.SharePoint.Client;

namespace BHDStarBooking.Repository.CustomRepository
{
    public class CustomRole_AccountRepository : SharePointRepository<Role_Account>
    {
        public CustomRole_AccountRepository(ISharePointBaseContext context) : base(context)
        {
        }

        public List<Role_Account> getAllByAccountID(int accountID)
        {
            ClientContext clientContext = _context.context;
            var web = clientContext.Web;
            var role_AccountList = web.Lists.GetByTitle("Role_Account");
            clientContext.Load(role_AccountList);
            clientContext.ExecuteQuery();
            // Lấy tất cả các Item trong danh sách Role_Account có giá trị accountID bằng accountId
            CamlQuery query = new CamlQuery();
            query.ViewXml = "<View><Query><Where><Eq><FieldRef Name='accountID' LookupId='True' /><Value Type='Lookup'>" + accountID + "</Value></Eq></Where></Query></View>";

            ListItemCollection items = role_AccountList.GetItems(query);
            clientContext.Load(items);
            clientContext.ExecuteQuery();
            List<Role_Account> raList = new List<Role_Account>();
            foreach (ListItem item in items)
            {
                Role_Account role_Account = new Role_Account();
                item["roleID"] = role_Account.roleID;
                item["accountID"] = role_Account.accountID;
                item["id0"] = role_Account.Id;
                raList.Add(role_Account);
            }
            return raList;
        }
        public void deleteByAccountIDAndRoleID(int accountID, int roleID)
        {
            ClientContext clientContext = _context.context;
            var web = clientContext.Web;
            var role_AccountList = web.Lists.GetByTitle("Role_Account");
            clientContext.Load(role_AccountList);
            clientContext.ExecuteQuery();
            CamlQuery query = new CamlQuery();
            query.ViewXml = "<View><Query><Where><And><Eq><FieldRef Name='accountID' LookupId='True' /><Value Type='Lookup'>" + accountID + "</Value></Eq><Eq><FieldRef Name='roleID' LookupId='True' /><Value Type='Lookup'>" + roleID + "</Value></Eq></And></Where></Query></View>";
            ListItemCollection items = role_AccountList.GetItems(query);
            clientContext.Load(items);
            clientContext.ExecuteQuery();

            // Xóa các Item trong danh sách
            foreach (ListItem item in items)
            {
                item.DeleteObject();
            }
            clientContext.ExecuteQuery();
        }


    }
}
