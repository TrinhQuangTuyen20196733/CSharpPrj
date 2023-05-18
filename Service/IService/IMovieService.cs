using BHDStarBooking.DTO;
using BHDStarBooking.Entity;

namespace BHDStarBooking.Service.IService
{
    public interface IMovieService
    {
        Task deleteByIdAsync(string id);
        Task<List<MovieDTO>> getAllAsync();
        Task<MovieDTO> getByIdAsync(string id);
        Task<List<MovieDTO>> getByPageAsync(int pageNumber);
        Task<long> getTotalElementAsync();
        Task<MovieDTO> insertOneAsync(MovieEntity movie);
        Task<List<MovieDTO>> searchByTitle(string title);
        Task<MovieDTO> updateMovieAsync(MovieEntity movie);
        Task<MovieDTO> updateMovieNoImage(MovieEntity movie);
        MovieEntity insertListItem(MovieEntity movie);
        MovieEntity updateListItem(MovieEntity movie);
        void deleteListItem(string id);
    }
}
