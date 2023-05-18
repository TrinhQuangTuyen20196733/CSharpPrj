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

  /*  [Authorize(Roles = "ADMIN")]*/
    public class CinemaController : ControllerBase
    {
        private readonly ICinemaService cinemaService;

        public CinemaController(ICinemaService cinemaService)
        {
            this.cinemaService = cinemaService;
        }
        [HttpGet]
        [AllowAnonymous]

        public async Task<List<CinemaEntity>> GetAllCinemaAsync()
        {
            return await cinemaService.getAllAsync();
        }
        [HttpGet("{id:length(24)}")]
        public async Task<CinemaEntity> GetCinemaByIdAsync(string id)
        {
            return await cinemaService.getByIdAsync(id);
        }

        [HttpPost]
        public async Task<CinemaEntity> CreateCinemaAsync([FromBody] CinemaEntity Cinema)
        {
           CinemaEntity cinemaEntity= await cinemaService.insertOne(Cinema);
            cinemaService.insertListItem(cinemaEntity);
            return cinemaEntity;

        }
        [HttpPut]
        public async Task<CinemaEntity> UpdateRoleAsync([FromBody] CinemaEntity Cinema)
        {
            cinemaService.updateListItem(Cinema);
            return await cinemaService.updateAsync(Cinema);
        }
        [HttpDelete("{id:length(24)}")]
        public async Task DeleteCinemaById(string id)
        {
            await cinemaService.deleteById(id);
            cinemaService.deleteListItem(id);
        }
    }
}

