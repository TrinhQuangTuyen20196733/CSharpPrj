using BHDStarBooking.Entity;
using BHDStarBooking.Repository;
using BHDStarBooking.Service.IService;

namespace BHDStarBooking.Service.ServiceImpl
{
    public class SeatOnSessionService : ISeatOnSessionService
    {
        private readonly IMongoRepository<SeatOnSessionEntity> seatOnSessionRepository;

        public SeatOnSessionService(IMongoRepository<SeatOnSessionEntity> seatOnSessionRepository)
        {
            this.seatOnSessionRepository = seatOnSessionRepository;
        }

        public async Task<List<SeatOnSessionEntity>> findAllBySessionId(object session_id)
        {
            return  await seatOnSessionRepository.GetByCondition(sos => sos.session.Id == session_id);
        }

        public async Task<SeatOnSessionEntity> InsertOneAsync(SeatOnSessionEntity seatOnSession)
        {
            return await seatOnSessionRepository.InsertAsync(seatOnSession);
        }
    }
}
