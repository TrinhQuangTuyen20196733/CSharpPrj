using BHDStarBooking.Entity;
using BHDStarBooking.Entity.SharePoint;
using BHDStarBooking.Service;
using BHDStarBooking.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BHDStarBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
/*    [Authorize(Roles = "ADMIN")]*/
    public class AccountController : ControllerBase

    {

        private readonly IAccountService accountService;
        private readonly IRole_Account role_accountService;
        private readonly IRoleService roleService;

        public AccountController(IAccountService accountService, IRole_Account role_accountService, IRoleService roleService)
        {
            this.accountService = accountService;
            this.role_accountService = role_accountService;
            this.roleService = roleService;
        }

        [HttpPost]
        public async Task<AccountEntity> createAccount([FromBody] AccountEntity account)
        {
            AccountEntity accountEntity = await accountService.InsertAsync(account);
            var roles = accountEntity.roles.Select(r => roleService.getItemIDByid(r.Id)).ToList();

            SPAccount sPAccount = new SPAccount() {
                Id = accountEntity.Id,
               email=accountEntity.email,
               password=accountEntity.password,
               roles= roles
            };
            accountService.insertItem(sPAccount);

            /* SPAccountEntity sPAccountEntity = new SPAccountEntity()
             {
                 email = accountEntity.email,
                 Id=accountEntity.Id,
                 password=accountEntity.password
             };
            var accountID= accountService.UpdateListItemAndReturnID(sPAccountEntity);

             var roles = accountEntity.roles.Select(r => r.Id).ToList();
             foreach (string role in roles)
             {
                 var roleID = roleService.getItemIDByid(role);
                 Role_Account role_Account = new Role_Account()
                 { 
                     accountID= accountID,
                     roleID= roleID
                 };
                 role_accountService.insertRole_Account(role_Account);

             }*/

            return accountEntity;
        }
        [HttpGet]
        public async Task<List<AccountEntity>> GetAllAccountAsync()
        {
            return await accountService.getAllAsync();
        }
        [HttpGet("{id:length(24)}")]
        public async Task<AccountEntity> GetAccountByIdAsync(string id)
        {
            return await accountService.getByIdAsync(id);
        }
        [HttpDelete("{id:length(24)}")]
        public async Task DeleteAccountById(string id)
        {
            await accountService.deleteById(id);
            accountService.DeleteListItem(id);
        }

    }
}
