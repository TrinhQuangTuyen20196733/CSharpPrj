using BHDStarBooking.Config;
using BHDStarBooking.Config.SharePointConfig;
using BHDStarBooking.DTO.Request;
using BHDStarBooking.DTO.Response;
using BHDStarBooking.Entity;
using BHDStarBooking.Entity.SharePoint;
using BHDStarBooking.Service.IService;
using BHDStarBooking.Service.ServiceImpl;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Search.Query;
using MongoDB.Driver;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace BHDStarBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthenticationController : ControllerBase
    {/*
        private readonly UserManager<UserEntity> _userManager;*/

        private readonly IRole_Account role_accountService;
        private readonly IRoleService roleService;
        private readonly IAccountService accountService;
        private readonly JwtConfig jwtConfig;
        private readonly IUserService userService;
        private readonly ClientContext context;
        private readonly ITestService testService;

        public AuthenticationController(IAccountService accountService, IOptions<JwtConfig> jwtConfig, IUserService userService,
            ISharePointBaseContext _context, IRoleService roleService, IRole_Account role_accountService, ITestService testService)
        {
            this.accountService = accountService;
            this.jwtConfig = jwtConfig.Value;
            this.userService = userService;
            this.context = _context.context;
            this.roleService = roleService;
            this.role_accountService = role_accountService;
            this.testService = testService;
        }


        [HttpPost("/register")]
        public async Task<UserEntity> createUser([FromBody] RegisterRequest registerRequest)
        {
            UserEntity user = await userService.insertOneAsync(registerRequest);
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
            userService.insertItem(spUserEnitity);
            /* AccountEntity accountEntity = user.account;
             SPAccountEntity sPAccountEntity = new SPAccountEntity()
             {
                 email = accountEntity.email,
                 Id = accountEntity.Id,
                 password = accountEntity.password
             };
             var accountID = accountService.InsertListItemAndReturnID(sPAccountEntity);

             var roles = accountEntity.roles.Select(r => r.Id).ToList();
             foreach (string role in roles)
             {
                 var roleID = roleService.getItemIDByid(role);
                 Role_Account role_Account = new Role_Account()
                 {
                     accountID = accountID,
                     roleID = roleID
                 };
                 role_accountService.insertRole_Account(role_Account);

             }
             SPUserEntity spUserEnitity = new SPUserEntity()
             {   Id=user.Id,
                 lastName=user.lastName,
                 firstName=user.firstName,
                 accountID=accountID,
                 birthDay=user.birthDay,
                 address=user.address,
                 phoneNumber=user.phoneNumber,
             };
             userService.insertItem(spUserEnitity);*/
            return user;


        }

        [HttpPost]
        [Route("/login")]
        /*[ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(LoginResponse))]*/
        public async Task<LoginResponse> Login([FromBody] LoginRequest loginRequest)
        {
            Web web = context.Web;
            List accountList = web.Lists.GetByTitle("Account");
            CamlQuery query = CamlQuery.CreateAllItemsQuery(100);
            ListItemCollection items = accountList.GetItems(query);
            context.Load(items);

            context.ExecuteQuery();
            foreach (ListItem item in items)
            {
                Console.WriteLine(item["Email"] + " " + item["Password"]);
            }
            var accountResult = await accountService.GetByEmailAndPassword(loginRequest.email, loginRequest.password);
            if (accountResult == null || accountResult.Count != 1)
            {
                return new LoginResponse
                {
                    message = " Email or Password is invalid!"
                };
            }
            var result = accountResult[0];
            var claimss = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, result.Id.ToString()),
                new Claim(ClaimTypes.Name, result.email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, result.Id.ToString())
            };
            var roles = result.roles;
            List<string> roleDTO = new List<string>();
            foreach (var role in roles)
            {
                claimss.Add(new Claim(ClaimTypes.Role, role.code));
                roleDTO.Add(role.code);
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Secretkey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiress = DateTime.Now.AddDays(1);
            var token = new JwtSecurityToken
                (
                claims: claimss,
                expires: expiress,
                signingCredentials: creds

                );
            return new LoginResponse
            {
                jwtToken = new JwtSecurityTokenHandler().WriteToken(token),
                message = "Login succesfully",
                email = result.email,
                accountId = result.Id,
                roles = roleDTO,

            };
        }
        [HttpGet]
        [Route("/test")]
        public void DataAsync()
        {

            testService.DataAsync();
        }
    }
}
