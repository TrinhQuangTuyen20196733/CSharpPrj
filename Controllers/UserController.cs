using BHDStarBooking.DTO.Request;
using BHDStarBooking.DTO.Response;
using BHDStarBooking.DTO.Response.Page;
using BHDStarBooking.Entity;
using BHDStarBooking.Entity.SharePoint;
using BHDStarBooking.Repository.CustomRepository;
using BHDStarBooking.Service.IService;
using BHDStarBooking.Service.ServiceImpl;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BHDStarBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

  /*  [Authorize(Roles = "ADMIN")]*/
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IAccountService accountService;
        private readonly IRole_Account role_accountService;
        private readonly IRoleService roleService;
        private readonly CustomRole_AccountRepository customRole_AccountRepository;
        private readonly CustomAccountRepository customAccountRepository;

        public UserController(IUserService userService, IAccountService accountService, IRole_Account role_accountService, IRoleService roleService
            , CustomRole_AccountRepository customRole_AccountRepository, CustomAccountRepository customAccountRepository)
        {
            this.userService = userService;
            this.accountService = accountService;
            this.role_accountService = role_accountService;
            this.roleService = roleService;
            this.customRole_AccountRepository = customRole_AccountRepository;
            this.customAccountRepository = customAccountRepository;
        }

        [HttpGet("pages/{pageNumber}")]
        public async Task<UserPage> getUserByPageAsync(int pageNumber)
        {
            List<UserEntity> userEntities = await userService.getByPageAsync(pageNumber);
            long totalElements = await userService.getTotalElementAsync();
            var userPage = new UserPage
            {
                totalItemPage = totalElements,
                totalPage = totalElements / 8 + 1,
                userDTOList = userEntities

            };
            return userPage;
        }
        [HttpGet("{id:length(24)}")]
        public async Task<UserEntity> GetUserByIdAsync(string id)
        {
            return await userService.getByIdAsync(id);
        }
        [HttpDelete("{id:length(24)}")]
        public async Task<MessageResponse> DeleteUserById(string id)
        {
            try
            {
                await userService.deleteUserById(id);
                userService.deleteItemById(id);
            }
            catch (Exception ex)
            {
                return new MessageResponse
                {
                    status = (int)HttpStatusCode.BadRequest,
                    description = " Xóa người dùng không thành công. Vui lòng thử lại!"
                };
            }
            return new MessageResponse
            {
                status = (int)HttpStatusCode.OK,
                description = "Bạn đã xóa người dùng thành công!"
            };
        }
        [HttpPut]
        public async Task<UserEntity> UpdateUser([FromBody] RegisterRequest registerRequest)
        {
            UserEntity user = await userService.updateUserAsync(registerRequest);
            AccountEntity accountEntity = user.account;
            var roles = accountEntity.roles.Select(r => roleService.getItemIDByid(r.Id)).ToList();

            SPAccount sPAccount = new SPAccount()
            {
                Id = accountEntity.Id,
                email = accountEntity.email,
                password = accountEntity.password,
                roles = roles
            };
            SPAccount spaccount = accountService.insertItem(sPAccount);
            int accountID = accountService.getItemIDByid(spaccount.Id);
            SPUserEntity spUserEnitity = new SPUserEntity()
            {
                Id = user.Id,
                lastName = user.lastName,
                firstName = user.firstName,
                accountID = accountID,
                birthDay = user.birthDay,
                address = user.address,
                phoneNumber = user.phoneNumber,
            };
            userService.updateItem(spUserEnitity);
            return user;
        }
    }
}
