using BHDStarBooking.Entity;

namespace BHDStarBooking.Service.IService
{
    public interface IServiceService
    {
        Task deleteServiceById(string id);
        Task<List<ServiceEntity>> getAllAsync();
        Task<List<ServiceEntity>> getByPageAsync(int pageNumber);
        Task<long> getTotalElementAsync();
        Task<ServiceEntity> insertOneAsync(ServiceEntity entity);
        Task<ServiceEntity> updateAsync(ServiceEntity role);
        ServiceEntity insertItem(ServiceEntity entity);
        ServiceEntity updateItem(ServiceEntity entity);
        void deleteItem(string id);
    }
}
