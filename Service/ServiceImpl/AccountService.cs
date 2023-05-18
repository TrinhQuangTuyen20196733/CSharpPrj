using AspNetCore.Identity.MongoDbCore.Infrastructure;
using BHDStarBooking.Config;
using BHDStarBooking.DTO.Response;
using BHDStarBooking.Entity;
using BHDStarBooking.Entity.SharePoint;
using BHDStarBooking.Repository;
using BHDStarBooking.Repository.CustomRepository;
using BHDStarBooking.Service.IService;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace BHDStarBooking.Service.ServiceImpl
{
    public class AccountService : IAccountService
    {
        private readonly IMongoRepository<AccountEntity> accountRepository;
        private readonly ISharePointRepository<SPAccountEntity> sharePointRepository;
        private readonly ISharePointRepository<Role_Account> roleAccountRepository;
        private readonly CustomAccountRepository customAccountRepository;
        private readonly ISharePointRepository<SPAccount> accountSharePointRepository;
        /*  private readonly UserManager<UserEntity> _userManager;*/
        private JwtConfig jwtConfig;

        public AccountService(IMongoRepository<AccountEntity> accountRepository, IOptions<JwtConfig> jwtConfig,
            ISharePointRepository<SPAccountEntity> sharePointRepository, ISharePointRepository<Role_Account> roleAccountRepository,
            CustomAccountRepository customAccountRepository, ISharePointRepository<SPAccount> accountSharePointRepository)
        {
            this.accountRepository = accountRepository;
            this.jwtConfig = jwtConfig.Value;
            this.sharePointRepository = sharePointRepository;
            this.roleAccountRepository = roleAccountRepository;
            this.customAccountRepository = customAccountRepository;
            this.accountSharePointRepository = accountSharePointRepository;
        }

        public async Task deleteById(string id)
        {
            await accountRepository.DeleteAsync(id);
        }

        public void DeleteListItem(string id)
        {
            /*customAccountRepository.deleteAccountById(id);*/
            accountSharePointRepository.deleteListItemById(id);
        }

        public async Task<List<AccountEntity>> getAllAsync()
        {
            return await accountRepository.GetAllAsync();
        }

        public async Task<AccountEntity?> GetByEmail(string email)
        {
           List<AccountEntity> accountEntities= await accountRepository.GetByCondition(acc => acc.email == email);
            return (accountEntities.Count == 0) ? null : accountEntities[0];
        }

        public async Task<List<AccountEntity>> GetByEmailAndPassword(string email, string password)
        {
            return await accountRepository.GetByCondition(acc => acc.email == email && acc.password == password);
        }

        public async Task<AccountEntity> getByIdAsync(string id)
        {
            return await accountRepository.GetByIdAsync(id);
        }

        public int getItemIDByid(string id)
        {
            return accountSharePointRepository.getListItemIDByid(id);
        }

        public async Task<AccountEntity> InsertAsync(AccountEntity account)
        {
            return await accountRepository.InsertAsync(account);
        }

        public SPAccount insertItem(SPAccount account)
        {
            return accountSharePointRepository.insertListItem(account);
        }

        public SPAccountEntity InsertListItem(SPAccountEntity account)
        {
            return sharePointRepository.insertListItem(account);
        }

        public int InsertListItemAndReturnID(SPAccountEntity account)
        {
            return sharePointRepository.insertItemAndReturnID(account);
        }

        public Task<AccountEntity> UpdateAsync(AccountEntity accountEntity)
        {
            return accountRepository.UpdateAsync(accountEntity);
        }

        public SPAccount updateItem(SPAccount account)
        {
            return accountSharePointRepository.updateListItem(account);
        }

        public int UpdateListItemAndReturnID(SPAccountEntity account)
        {
            return sharePointRepository.updateItemAndReturnID(account);
        }
    }
}
