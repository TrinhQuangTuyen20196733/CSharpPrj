using AspNetCore.Identity.MongoDbCore.Infrastructure;
using BHDStarBooking.Entity;
using BHDStarBooking.Entity.SharePoint;
using BHDStarBooking.Repository;
using BHDStarBooking.Service.IService;

namespace BHDStarBooking.Service.ServiceImpl
{
    public class RoomService : IRoomService
    {
        private readonly IMongoRepository<RoomEntity> roomRepository;
        private readonly ISharePointRepository<SPRoomEntity> sharePointRepository;

        public RoomService(IMongoRepository<RoomEntity> roomRepository, ISharePointRepository<SPRoomEntity> sharePointRepository)
        {
            this.roomRepository = roomRepository;
            this.sharePointRepository = sharePointRepository;
        }

        public async Task deleteById(string id)
        {
            await roomRepository.DeleteAsync(id);
        }

        public void deleteItem(string id)
        {
            sharePointRepository.deleteListItemById(id);
        }

        public async Task<RoomEntity> findByRoomNameAndCinemaId(string? roomName, string? movieSystem_id)
        {
            return await roomRepository.GetOneByCondition(room => room.name==roomName && room.cinema.Id==movieSystem_id);
          
        }

        public async Task<List<RoomEntity>> getAllAsync()
        {
            return await roomRepository.GetAllAsync();
        }

        public async Task<RoomEntity> getByIdAsync(string id)
        {
            return await roomRepository.GetByIdAsync(id);
        }

        public Task<RoomEntity> InsertAsync(RoomEntity room)
        {
            return roomRepository.InsertAsync(room);
        }

        public SPRoomEntity insertItem(SPRoomEntity room)
        {
            return sharePointRepository.insertListItem(room);
        }
    }
}
