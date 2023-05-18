using BHDStarBooking.Config.SharePointConfig;
using BHDStarBooking.Entity.SharePoint;
using Microsoft.Identity.Client;
using Microsoft.SharePoint.Client;

namespace BHDStarBooking.Repository.CustomRepository
{
    public class CustomAccountRepository : SharePointRepository<SPAccountEntity>
    {
        public CustomAccountRepository(ISharePointBaseContext context) : base(context)
        {
        }
       public  void deleteAccountById(string accountID)
        {
            int id = getListItemIDByid(accountID);
           

          
            ClientContext clientContext = _context.context;
            var web = clientContext.Web;
            var role_AccountList = web.Lists.GetByTitle("Role_Account");
            clientContext.Load(role_AccountList);
            clientContext.ExecuteQuery();
            
            // Lấy tất cả các Item trong danh sách Role_Account có giá trị accountID bằng accountId
            CamlQuery query = new CamlQuery();
            query.ViewXml = "<View><Query><Where><Eq><FieldRef Name='accountID' LookupId='True' /><Value Type='Lookup'>" + id + "</Value></Eq></Where></Query></View>";

            ListItemCollection items = role_AccountList.GetItems(query);
            clientContext.Load(items);
            clientContext.ExecuteQuery();

            // Xóa các Item trong danh sách
            foreach (ListItem item in items)
            {
                item.DeleteObject();
            }
            clientContext.ExecuteQuery();
            base.deleteListItemById(accountID);
        }
       
        public string getItemIDByEmail (string email)
        {
            ClientContext clientContext = _context.context;
            var web = clientContext.Web;
            var SPAccountList = web.Lists.GetByTitle("SPAccountEntity");
            clientContext.Load(SPAccountList);
            clientContext.ExecuteQuery();

            // Lấy tất cả các Item trong danh sách Role_Account có giá trị accountID bằng accountId
            CamlQuery query = new CamlQuery();
            query.ViewXml = "<View><Query><Where><Eq><FieldRef Name='email'  /><Value Type='Text'>" + email + "</Value></Eq></Where></Query></View>";

            ListItemCollection items = SPAccountList.GetItems(query);
            clientContext.Load(items);
            clientContext.ExecuteQuery();
            ListItem item = items[0];
            return (string)item["id0"];
        }
    }
    
}
