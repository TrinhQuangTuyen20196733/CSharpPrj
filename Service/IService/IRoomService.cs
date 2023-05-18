using BHDStarBooking.Entity;
using BHDStarBooking.Entity.SharePoint;

namespace BHDStarBooking.Service.IService
{
    public interface IRoomService
    {
        Task deleteById(string id);
        Task<RoomEntity> findByRoomNameAndCinemaId(string? roomName, string? movieSystem_id);
        Task<List<RoomEntity>> getAllAsync();
        Task<RoomEntity> getByIdAsync(string id);
        Task<RoomEntity> InsertAsync(RoomEntity room);
        SPRoomEntity insertItem(SPRoomEntity room);
        void deleteItem(string id);
    }
}
