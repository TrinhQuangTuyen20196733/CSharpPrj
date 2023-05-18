using BHDStarBooking.Entity;
using BHDStarBooking.Entity.SharePoint;

namespace BHDStarBooking.Service.IService
{
    public interface ISeatService
    {
        Task deleteById(string id);
        Task<List<SeatEntity>> getAllAsync();
        Task<List<SeatEntity>> getAllByRoomIdAsync(string? id);
        Task<SeatEntity> getByIdAsync(string id);
        Task<SeatEntity> InsertAsync(SeatEntity seat);
        SPSeatEntity insertItem(SPSeatEntity seat);
        void deleteItem(string id);
        int getItemIDById(string id);
    }
}
