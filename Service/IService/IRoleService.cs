using BHDStarBooking.Entity;

namespace BHDStarBooking.Service.IService
{
    public interface IRoleService
    {
        Task deleteRoleById(string id);
        Task<List<RoleEntity>> getAllAsync();
        Task<List<RoleEntity>> getByCodeAsync(List<string>? roles);
        Task<RoleEntity> getByIdAsync(string id);
        Task<RoleEntity> insertOne(RoleEntity role);
        Task<RoleEntity> updateAsync(RoleEntity role);
        RoleEntity insertRoleListItem(RoleEntity role);
        List<RoleEntity> getAllRoleListItem();
        RoleEntity getRoleListItemById(string id);
        RoleEntity updateRoleListItem(RoleEntity role);
        void deleteRoleListItemById(string id);
        int getItemIDByid(string id);
    }
}
