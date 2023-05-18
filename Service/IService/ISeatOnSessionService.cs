using BHDStarBooking.Entity;

namespace BHDStarBooking.Service.IService
{
    public interface ISeatOnSessionService
    {
        Task<List<SeatOnSessionEntity>> findAllBySessionId(object session_id);
        Task<SeatOnSessionEntity> InsertOneAsync(SeatOnSessionEntity seatOnSession);
    }
}
