using AspNetCore.Identity.MongoDbCore.Infrastructure;
using BHDStarBooking.Entity;
using BHDStarBooking.Repository;
using BHDStarBooking.Service.IService;

namespace BHDStarBooking.Service.ServiceImpl
{
    public class CinemaService : ICinemaService
    {
        private readonly IMongoRepository<CinemaEntity> cinemaRepository;
        private readonly ISharePointRepository<CinemaEntity> sharePointRepository;

        public CinemaService(IMongoRepository<CinemaEntity> cinemaRepository, ISharePointRepository<CinemaEntity> sharePointRepository)
        {
            this.cinemaRepository = cinemaRepository;
            this.sharePointRepository = sharePointRepository;
        }

        public async Task deleteById(string id)
        {
            await cinemaRepository.DeleteAsync(id);
        }

        public void deleteListItem(string id)
        {
            sharePointRepository.deleteListItemById(id); 
        }

        public Task<List<CinemaEntity>> getAllAsync()
        {
             return cinemaRepository.GetAllAsync();
        }

        public Task<CinemaEntity> getByIdAsync(string id)
        {
           return cinemaRepository.GetByIdAsync(id);
        }

        public int getItemIDById(string id)
        {
            return sharePointRepository.getListItemIDByid(id);
        }

        public CinemaEntity insertListItem(CinemaEntity cinema)
        {
           return sharePointRepository.insertListItem(cinema);
        }

        public Task<CinemaEntity> insertOne(CinemaEntity cinema)
        {
            return cinemaRepository.InsertAsync(cinema);
        }

        public Task<CinemaEntity> updateAsync(CinemaEntity cinema)
        {
            return cinemaRepository.UpdateAsync(cinema);
        }

        public CinemaEntity updateListItem(CinemaEntity cinema)
        {
            return sharePointRepository.updateListItem(cinema);
        }
    }
}
