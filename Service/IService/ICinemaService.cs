using BHDStarBooking.Entity;

namespace BHDStarBooking.Service.IService
{
    public interface ICinemaService
    {
        Task deleteById(string id);
        Task<List<CinemaEntity>> getAllAsync();
        Task<CinemaEntity> getByIdAsync(string id);
        Task<CinemaEntity> insertOne(CinemaEntity cinema);
        Task<CinemaEntity> updateAsync(CinemaEntity cinema);
        CinemaEntity insertListItem(CinemaEntity cinema);
        CinemaEntity updateListItem(CinemaEntity cinema);
        void deleteListItem(string id);
        int getItemIDById(string id);
    }
}
