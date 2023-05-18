using BHDStarBooking.DTO.Request;
using BHDStarBooking.Entity;
using BHDStarBooking.Entity.SharePoint;

namespace BHDStarBooking.Service.IService
{
    public interface IUserService
    {
        Task deleteUserById(string id);
        Task<UserEntity> getByIdAsync(string id);
        Task<List<UserEntity>> getByPageAsync(int pageNumber);
        Task<long> getTotalElementAsync();
        Task<UserEntity> getUserByEmailAsync(string? email);
        Task<UserEntity> insertOneAsync(RegisterRequest registerRequest);
        Task<UserEntity> updateUserAsync(RegisterRequest registerRequest);
        SPUserEntity insertItem(SPUserEntity user);
        SPUserEntity updateItem(SPUserEntity user);
        void deleteItemById(string id);
    }
}
