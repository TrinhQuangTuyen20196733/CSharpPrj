
using BHDStarBooking.DTO;
using BHDStarBooking.Entity;
using BHDStarBooking.Entity.SharePoint;

namespace BHDStarBooking.Service.IService
{
    public interface ISessionService
    {
        Task deleteById(string id);
        Task<SessionDTOS> getByIdAsync(string id);
        Task<List<SessionDTOS>> getByPageAsync(int pageNumber);
        Task<long> getTotalElementAsync();
        Task<List<SessionDTOS>> getUpcomingMovie(string id, DateTime startTimeDateTime);
        Task<SessionDTO> insertOneAsync(SessionDTO sessionDTO);
        SPSessionEntity insertItem(SPSessionEntity sessionDTO);
        void deleteItemById(string id);
    }
}
