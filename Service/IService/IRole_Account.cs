using BHDStarBooking.Entity.SharePoint;
using BHDStarBooking.Repository;

namespace BHDStarBooking.Service.IService
{
    public interface IRole_Account
    {
        void insertRole_Account(Role_Account role_Account);
        Role_Account updateItem(Role_Account role_Account);
        List<Role_Account> getItemByAccountID(int accountID);
        void deleteById(string id);
    }
}
