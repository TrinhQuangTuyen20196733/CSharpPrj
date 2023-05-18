using AspNetCore.Identity.MongoDbCore.Infrastructure;
using BHDStarBooking.Entity;
using BHDStarBooking.Entity.SharePoint;
using BHDStarBooking.Repository;
using BHDStarBooking.Service.IService;

namespace BHDStarBooking.Service.ServiceImpl
{
    public class SeatService : ISeatService
    {
        private readonly IMongoRepository<SeatEntity> seatRepository;
        private readonly ISharePointRepository<SPSeatEntity> sharePointRepository;

        public SeatService(IMongoRepository<SeatEntity> seatRepository, ISharePointRepository<SPSeatEntity> sharePointRepository)
        {
            this.seatRepository = seatRepository;
            this.sharePointRepository = sharePointRepository;
        }

        public async Task deleteById(string id)
        {
           await seatRepository.DeleteAsync(id);
        }

        public void deleteItem(string id)
        {
            sharePointRepository.deleteListItemById(id);
        }

        public async Task<List<SeatEntity>> getAllAsync()
        {
            return await seatRepository.GetAllAsync();
        }

        public async Task<List<SeatEntity>> getAllByRoomIdAsync(string? id)
        {
            return  await seatRepository.GetByCondition(seat => seat.room.Id == id);
        }

        public async Task<SeatEntity> getByIdAsync(string id)
        {
           return await seatRepository.GetByIdAsync(id);
        }

        public int getItemIDById(string id)
        {
            return sharePointRepository.getListItemIDByid(id);
        }

        public async Task<SeatEntity> InsertAsync(SeatEntity seat)
        {
            return await seatRepository.InsertAsync(seat);
        }

        public SPSeatEntity insertItem(SPSeatEntity seat)
        {
           return sharePointRepository.insertListItem(seat);
        }
    }
}
