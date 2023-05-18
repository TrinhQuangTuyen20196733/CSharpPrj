using AspNetCore.Identity.MongoDbCore.Infrastructure;
using BHDStarBooking.Entity;
using BHDStarBooking.Repository;
using BHDStarBooking.Service.IService;

namespace BHDStarBooking.Service.ServiceImpl
{
    public class ServiceService: IServiceService
    {
        private IMongoRepository<ServiceEntity> serviceRepository;
        private ISharePointRepository<ServiceEntity> sharePointRepository;

        public ServiceService(IMongoRepository<ServiceEntity> serviceRepository, ISharePointRepository<ServiceEntity> sharePointRepository)
        {
            this.serviceRepository = serviceRepository;
            this.sharePointRepository = sharePointRepository;
        }

        public void deleteItem(string id)
        {
            sharePointRepository.deleteListItemById(id);
        }

        public Task deleteServiceById(string id)
        {
           return serviceRepository.DeleteAsync(id);
        }

        public async Task<List<ServiceEntity>> getAllAsync()
        {
            return await serviceRepository.GetAllAsync();
        }

        public async Task<List<ServiceEntity>> getByPageAsync(int pageNumber)
        {
            return await serviceRepository.GetByPageAsync(pageNumber);
        }

        public async Task<long> getTotalElementAsync()
        {
            return await serviceRepository.GetTotalItemAsync();
        }

        public ServiceEntity insertItem(ServiceEntity entity)
        {
            return sharePointRepository.insertListItem(entity);
        }

        public async Task<ServiceEntity> insertOneAsync(ServiceEntity entity)
        {
            return await serviceRepository.InsertAsync(entity);
        }

        public async Task<ServiceEntity> updateAsync(ServiceEntity service)
        {
            return await serviceRepository.UpdateAsync(service);
        }

        public ServiceEntity updateItem(ServiceEntity entity)
        {
            return sharePointRepository.updateListItem(entity);
        }
    }
}
