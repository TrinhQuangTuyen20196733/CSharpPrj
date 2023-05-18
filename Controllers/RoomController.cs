using BHDStarBooking.Entity;
using BHDStarBooking.Entity.SharePoint;
using BHDStarBooking.Service.IService;
using BHDStarBooking.Service.ServiceImpl;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BHDStarBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

   /* [Authorize(Roles = "ADMIN")]*/
    public class RoomController : ControllerBase
    {
        private readonly IRoomService roomService;
        private readonly ICinemaService cinemaService;

        public RoomController(IRoomService roomService, ICinemaService courseService)
        {
            this.roomService = roomService;
            this.cinemaService = courseService;
        }

        [HttpPost]
        public async Task<RoomEntity> createRoom([FromBody] RoomEntity room)
        {
           RoomEntity roomEntity = await roomService.InsertAsync(room);
            int cinemaID = cinemaService.getItemIDById(roomEntity.cinema.Id);
            SPRoomEntity sPRoomEntity = new SPRoomEntity()
            {
                name = roomEntity.name,
                type = roomEntity.type,
                Id= roomEntity.Id,
                cinemaID=cinemaID
                
            };
            roomService.insertItem(sPRoomEntity);
            return roomEntity;
        }
        [HttpGet]
        public async Task<List<RoomEntity>> GetAllRoomAsync()
        {
            return await roomService.getAllAsync();
        }
        [HttpGet("{id:length(24)}")]
        public async Task<RoomEntity> GetRoomByIdAsync(string id)
        {
            return await roomService.getByIdAsync(id);
        }
        [HttpDelete("{id:length(24)}")]
        public async Task DeleteRoomById(string id)
        {
            await roomService.deleteById(id);
            roomService.deleteItem(id);
        }

    }
}

