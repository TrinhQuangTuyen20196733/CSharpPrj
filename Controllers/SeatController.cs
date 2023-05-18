using BHDStarBooking.Entity;
using BHDStarBooking.Entity.SharePoint;
using BHDStarBooking.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BHDStarBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
/*
    [Authorize(Roles = "ADMIN")]*/
    public class SeatController : ControllerBase
    {
        private readonly ISeatService seatService;
        private readonly IRoomService roomService;

        public SeatController(ISeatService seatService, IRoomService roomService)
        {
            this.seatService = seatService;
            this.roomService = roomService;
        }

        [HttpPost]
        public async Task<SeatEntity> createSeat([FromBody] SeatEntity seat)
        {
            SeatEntity seatEntity = await seatService.InsertAsync(seat);
            int roomID = seatService.getItemIDById(seatEntity.room.Id);
            SPSeatEntity sPSeat = new SPSeatEntity()
            {
                Id=seatEntity.Id,
                row=seatEntity.row,
                column=seatEntity.column,
                roomID=roomID,
            };
            seatService.insertItem(sPSeat);
            return seatEntity;
        }
        [HttpGet]
        public async Task<List<SeatEntity>> GetAllSeatAsync()
        {
            return await seatService.getAllAsync();
        }
        [HttpGet("{id:length(24)}")]
        public async Task<SeatEntity> GetSeatByIdAsync(string id)
        {
            return await seatService.getByIdAsync(id);
        }
        [HttpDelete("{id:length(24)}")]
        public async Task DeleteSeatById(string id)
        {
            await seatService.deleteById(id);
            seatService.deleteItem(id);
        }
        [HttpGet("AutoPost")]
        public async void autoPost()
        {
            List<RoomEntity> roomEntities = await roomService.getAllAsync();
            foreach (RoomEntity roomEntity in roomEntities)
            {
                for (int row = 1; row < 7; row++)
                {
                    for (int col = 1; col < 9; col++)
                    {
                        SeatEntity seat = new SeatEntity
                        {
                            row = row,
                            column = col,
                            type = "MEDIUM",
                            room = roomEntity
                        };
                      SeatEntity seatEntity =  await seatService.InsertAsync(seat);
                        int roomID = seatService.getItemIDById(seatEntity.room.Id);
                        SPSeatEntity sPSeat = new SPSeatEntity()
                        {
                            Id = seatEntity.Id,
                            row = seatEntity.row,
                            column = seatEntity.column,
                            roomID = roomID,
                        };
                        seatService.insertItem(sPSeat);
                    }
                }
            }

        }
    }
}

