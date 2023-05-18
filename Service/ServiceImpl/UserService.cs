using AspNetCore.Identity.MongoDbCore.Infrastructure;
using BHDStarBooking.DTO.Request;
using BHDStarBooking.Entity;
using BHDStarBooking.Entity.SharePoint;
using BHDStarBooking.Repository;
using BHDStarBooking.Service.IService;

namespace BHDStarBooking.Service.ServiceImpl
{
    public class UserService : IUserService
    { 
        private readonly IMongoRepository<UserEntity> userRepository;
        private readonly IRoleService roleService;
        private IAccountService accountService;
        private readonly ISharePointRepository<SPUserEntity> sharePointRepository;

        public UserService(IMongoRepository<UserEntity> userRepository, IRoleService roleService, IAccountService accountService, ISharePointRepository<SPUserEntity> sharePointRepository)
        {
            this.userRepository = userRepository;
            this.roleService = roleService;
            this.accountService = accountService;
            this.sharePointRepository = sharePointRepository;
        }

        public async Task deleteUserById(string id)
        {
            await userRepository.DeleteAsync(id);
        }

        public async Task<UserEntity> getByIdAsync(string id)
        {
            return await userRepository.GetByIdAsync(id);
        }

        public Task<List<UserEntity>> getByPageAsync(int pageNumber)
        {
            return userRepository.GetByPageAsync(pageNumber);
        }

        public async Task<long> getTotalElementAsync()
        {
           return await userRepository.GetTotalItemAsync();
        }

        public async Task<UserEntity> getUserByEmailAsync(string? email)
        {
            return await userRepository.GetOneByCondition(user=>user.account.email== email);
        }

        public SPUserEntity insertItem(SPUserEntity user)
        {
           return sharePointRepository.insertListItem(user);
        }

        public async Task<UserEntity> insertOneAsync(RegisterRequest registerRequest)
        {
            List<RoleEntity> roles = await roleService.getByCodeAsync(registerRequest.roles);
            AccountEntity accountEntity = new AccountEntity
            {
                email = registerRequest.email,
                password = registerRequest.password,
                roles = roles
            };
            accountEntity=  await accountService.InsertAsync(accountEntity);
            UserEntity userEntity = new UserEntity
            {
                account = accountEntity,
                firstName = registerRequest.firstName,
                lastName = registerRequest.lastName,
                birthDay = registerRequest.birthDay,
                address = registerRequest.address,
                phoneNumber = registerRequest.phoneNumber,
            };
            return await userRepository.InsertAsync(userEntity);
        }

        public  async Task<UserEntity> updateUserAsync(RegisterRequest registerRequest)
        {
            List<RoleEntity> roles = await roleService.getByCodeAsync(registerRequest.roles);
            AccountEntity account = await accountService.GetByEmail(registerRequest.email);
            AccountEntity accountEntity = new AccountEntity();
            if (account == null)
            {
                accountEntity = new AccountEntity
                {
                    email = registerRequest.email,
                    password = registerRequest.password,
                    roles = roles
                };
                accountEntity = await accountService.InsertAsync(accountEntity);
            } else
            {
                accountEntity = new AccountEntity
                {  
                    Id = account.Id,
                    email = registerRequest.email,
                    password = registerRequest.password,
                    roles = roles
                };
                accountEntity = await accountService.UpdateAsync(accountEntity);
            }
            UserEntity userEntity = new UserEntity
            {  Id= registerRequest.Id,
                account = accountEntity,
                firstName = registerRequest.firstName,
                lastName = registerRequest.lastName,
                birthDay = registerRequest.birthDay,
                address = registerRequest.address,
                phoneNumber = registerRequest.phoneNumber,
            };
            return await userRepository.UpdateAsync(userEntity);
        }

        public SPUserEntity updateItem(SPUserEntity user)
        {
            return sharePointRepository.updateListItem(user);
        }

        public void deleteItemById(string id)
        {
           sharePointRepository.deleteListItemById(id);
        }
    }
}
