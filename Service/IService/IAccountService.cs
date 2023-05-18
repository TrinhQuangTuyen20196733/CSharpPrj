using BHDStarBooking.DTO.Response;
using BHDStarBooking.Entity;
using BHDStarBooking.Entity.SharePoint;

namespace BHDStarBooking.Service.IService
{
    public interface IAccountService
    {
        Task deleteById(string id);
        Task<List<AccountEntity>> getAllAsync();
        Task<AccountEntity> GetByEmail(string? email);
        Task<List<AccountEntity>> GetByEmailAndPassword(string email, string password);
        Task<AccountEntity> getByIdAsync(string id);
        Task<AccountEntity> InsertAsync(AccountEntity account);
        Task<AccountEntity> UpdateAsync(AccountEntity accountEntity);
        SPAccountEntity InsertListItem(SPAccountEntity account);
        int InsertListItemAndReturnID(SPAccountEntity account);
        int getItemIDByid(string id);
        void DeleteListItem(string id);
        int UpdateListItemAndReturnID(SPAccountEntity account);
        SPAccount insertItem(SPAccount account);
        SPAccount updateItem(SPAccount account);
    }
}
