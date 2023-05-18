using AspNetCore.Identity.MongoDbCore.Infrastructure;
using BHDStarBooking.Entity;
using BHDStarBooking.ExceptionHandler.ExceptionModel;
using BHDStarBooking.Repository;
using BHDStarBooking.Service.IService;

namespace BHDStarBooking.Service.ServiceImpl
{
    public class RoleService : IRoleService
    {
        private readonly IMongoRepository<RoleEntity> roleRepository;
        private readonly ISharePointRepository<RoleEntity> rolesharePointRepository;

        public RoleService(IMongoRepository<RoleEntity> roleRepository, ISharePointRepository<RoleEntity> rolesharePointRepository)
        {
            this.roleRepository = roleRepository;
            this.rolesharePointRepository = rolesharePointRepository;
        }

        public async Task deleteRoleById(string id)
        {
            try
            {

                await roleRepository.DeleteAsync(id);
            }
            catch
            {
                throw new ResourceNotFoundException("Không tồn tại vai trò này trong hệ thống!");
            }

        }

        public void deleteRoleListItemById(string id)
        {
            try
            {

                rolesharePointRepository.deleteListItemById(id);
            }
            catch
            {
                throw new ResourceNotFoundException("Không tồn tại vai trò này trong hệ thống!");
            }
        }

        public async Task<List<RoleEntity>> getAllAsync()
        {
            return await roleRepository.GetAllAsync();
        }

        public List<RoleEntity> getAllRoleListItem()
        {
            return rolesharePointRepository.getAllListItem();

        }

        public async Task<List<RoleEntity>> getByCodeAsync(List<string>? roles)
        {
            return await roleRepository.GetByCondition(role => roles.Contains(role.code));
        }

        public async Task<RoleEntity> getByIdAsync(string id)
        {
            try
            {

                return await roleRepository.GetByIdAsync(id);
            }
            catch
            {
                throw new ResourceNotFoundException("Không tồn tại vai trò này trong hệ thống!");
            }

        }

        public int getItemIDByid(string id)
        {
         return rolesharePointRepository.getListItemIDByid(id);
        }

        public RoleEntity getRoleListItemById(string id)
        {
            try
            {

                return rolesharePointRepository.getListItemById(id);
            }
            catch
            {
                throw new ResourceNotFoundException("Không tồn tại vai trò này trong hệ thống!");
            }

        }

        public async Task<RoleEntity> insertOne(RoleEntity role)
        {
            return await roleRepository.InsertAsync(role);
        }

        public RoleEntity insertRoleListItem(RoleEntity role)
        {
            return rolesharePointRepository.insertListItem(role);
        }

        public Task<RoleEntity> updateAsync(RoleEntity role)
        {
            return roleRepository.UpdateAsync(role);
        }

        public RoleEntity updateRoleListItem(RoleEntity role)
        {
            return rolesharePointRepository.updateListItem(role);
        }
    }
}
