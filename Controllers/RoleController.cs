using BHDStarBooking.Entity;
using BHDStarBooking.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BHDStarBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

   /* [Authorize(Roles = "ADMIN")]*/
    public class RoleController : ControllerBase
    {
        private readonly IRoleService roleService;

        public RoleController(IRoleService roleService)
        {
            this.roleService = roleService;
        }
        [HttpGet]
        public async Task<List<RoleEntity>> GetAllRoleAsync()
        {
            await roleService.getAllAsync();
            return roleService.getAllRoleListItem();
        }
        [HttpGet("{id:length(24)}")]
        public  RoleEntity GetRoleByIdAsync(string id)
        {
            return roleService.getRoleListItemById(id);
        }

        [HttpPost]
        public async Task<RoleEntity> CreateRoleAsync([FromBody] RoleEntity role)
        {
            RoleEntity roleAfter = await roleService.insertOne(role);
            roleService.insertRoleListItem(role);
            return roleAfter;
        }
        [HttpPut]
        public async Task<RoleEntity> UpdateRoleAsync([FromBody] RoleEntity role)
        {
           RoleEntity roleAfter=  await roleService.updateAsync(role);
            return roleService.updateRoleListItem(roleAfter);
        }
        [HttpDelete("{id:length(24)}")]
        public void DeleteRoleById(string id)
        {
            roleService.deleteRoleById(id);
            roleService.deleteRoleListItemById(id);
        }
    }
}

