using BHDStarBooking.DTO.Response.Page;
using BHDStarBooking.DTO;
using BHDStarBooking.Entity;
using BHDStarBooking.Service.IService;
using BHDStarBooking.Service.ServiceImpl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BHDStarBooking.DTO.Response;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace BHDStarBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

  /*  [Authorize(Roles = "ADMIN")]*/
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService serviceService;

        public ServiceController(IServiceService serviceService)
        {
            this.serviceService = serviceService;
        }

        [HttpPost]
        public async Task<ServiceEntity> createService([FromBody] ServiceEntity entity)
        {
           ServiceEntity serviceEntity= await serviceService.insertOneAsync(entity);
            serviceService.insertItem(entity);
            return serviceEntity;
        }
        [HttpGet]
        [AllowAnonymous]

        public async Task<List<ServiceEntity>> getAllServiceAsync()
        {
            return await serviceService.getAllAsync();
        }
        [HttpGet("pages/{pageNumber}")]
        public async Task<ServicePage> getServiceByPageAsync(int pageNumber)
        {
            List<ServiceEntity> serviceEntities = await serviceService.getByPageAsync(pageNumber);
            long totalElements = await serviceService.getTotalElementAsync();
            var servicePage = new ServicePage
            {
                totalItemPage = totalElements,
                totalPage = totalElements / 8 + 1,
                serviceDTOList = serviceEntities

            };
            return servicePage;
        }
        [HttpPut]
        public async Task<ServiceEntity> UpdateServiceAsync([FromBody] ServiceEntity role)
        {
           ServiceEntity serviceEntity= await serviceService.updateAsync(role);
            serviceService.updateItem(role);
            return serviceEntity;
        }
        [HttpDelete("{id:length(24)}")]
        public async Task<MessageResponse> DeleteService(string id)
        {
            try
            {         serviceService.deleteItem(id);
                await serviceService.deleteServiceById(id);
                return new MessageResponse()
                {
                    status = (int)HttpStatusCode.OK,
                    description = "Bạn đã xóa dịch vụ thành công"
                };
            }
            catch (Exception e)
            {
                return new MessageResponse()
                {
                    status = (int)HttpStatusCode.BadRequest,
                    description = "Xóa dịch vụ thất bại"
                };
            }
        }
    }
}
