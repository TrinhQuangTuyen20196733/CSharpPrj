using BHDStarBooking.Entity.SharePoint;
using BHDStarBooking.Repository;
using BHDStarBooking.Service.IService;

namespace BHDStarBooking.Service.ServiceImpl
{
    public class Role_AccountService : IRole_Account
    {
        private readonly ISharePointRepository<Role_Account> sharePointRepository;

        public Role_AccountService(ISharePointRepository<Role_Account> sharePointRepository)
        {
            this.sharePointRepository = sharePointRepository;
        }

        public void deleteById(string id)
        {
           sharePointRepository.deleteListItemById(id);
        }

        public List<Role_Account> getItemByAccountID(int accountID)
        {
            throw new NotImplementedException();
        }

        public void insertRole_Account(Role_Account role_Account)
        {
            sharePointRepository.insertListItem(role_Account);
        }

        public Role_Account updateItem(Role_Account role_Account)
        {
           return sharePointRepository.updateListItem(role_Account);
        }
    }
}
