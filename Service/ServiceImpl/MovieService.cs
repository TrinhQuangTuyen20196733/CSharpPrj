using AspNetCore.Identity.MongoDbCore.Infrastructure;
using AutoMapper;
using BHDStarBooking.DTO;
using BHDStarBooking.Entity;
using BHDStarBooking.Repository;
using BHDStarBooking.Service.IService;
using BHDStarBooking.Utils;

namespace BHDStarBooking.Service.ServiceImpl
{
    public class MovieService : IMovieService
    {
        private readonly IMongoRepository<MovieEntity> movieRepository;

        private readonly IMapper mapper;

        private readonly ISharePointRepository<MovieEntity> sharePointRepository;

        public MovieService(IMongoRepository<MovieEntity> movieRepository, IMapper mapper, ISharePointRepository<MovieEntity> sharePointRepository)
        {
            this.movieRepository = movieRepository;
            this.mapper = mapper;
            this.sharePointRepository = sharePointRepository;
        }

        public  async Task deleteByIdAsync(string id)
        {
            await movieRepository.DeleteAsync(id);
        }

        public void deleteListItem(string id)
        {
            sharePointRepository.deleteListItemById(id);
        }

        public async Task<List<MovieDTO>> getAllAsync()
        {
           var movieList= await movieRepository.GetAllAsync();
            List<MovieDTO> result = new List<MovieDTO>();
            foreach (var movie in movieList)
            {
                result.Add(mapper.Map<MovieDTO>(movie));
            }
            return result;
        }

        public async Task<MovieDTO> getByIdAsync(string id)
        {
            MovieEntity movie = await movieRepository.GetByIdAsync(id);
            return mapper.Map<MovieDTO>(movie);
        }

        public async Task<List<MovieDTO>> getByPageAsync(int pageNumber)
        {
            List<MovieEntity> movies= await movieRepository.GetByPageAsync(pageNumber);
            List<MovieDTO> result= new List<MovieDTO>();
            foreach (MovieEntity movie in movies)
            {
                result.Add(mapper.Map<MovieDTO>(movie));
            }

            return result;
        }

        public Task<long> getTotalElementAsync()
        {
            return movieRepository.GetTotalItemAsync();
        }

        public MovieEntity insertListItem(MovieEntity movie)
        {
           return sharePointRepository.insertListItem(movie);
        }

        public async Task<MovieDTO> insertOneAsync(MovieEntity movie)
        {
           MovieEntity movieEntity = await movieRepository.InsertAsync(movie);

            return mapper.Map<MovieDTO>(movieEntity);

        }

        public async Task<List<MovieDTO>> searchByTitle(string title)
        {
            List<MovieEntity> movieEntities= await movieRepository.GetByCondition(movie=>movie.title.Contains(title));
            var movies = new List<MovieDTO>();
            foreach (MovieEntity movieEntity in movieEntities)
            {
                movies.Add(mapper.Map<MovieDTO>(movieEntity));
            }
            return movies;
        }

        public MovieEntity updateListItem(MovieEntity movie)
        {
           return sharePointRepository.updateListItem(movie);
        }

        public async Task<MovieDTO> updateMovieAsync(MovieEntity movie)
        {
            MovieEntity movieEntity = await movieRepository.UpdateAsync(movie);
            return mapper.Map<MovieDTO>(movieEntity);
        }

        public async Task<MovieDTO> updateMovieNoImage(MovieEntity movie)
        {
            MovieEntity movieEntity = await movieRepository.GetByIdAsync(movie.Id);
            movie.thumbnail = movieEntity.thumbnail;
            movie = await  movieRepository.UpdateAsync(movie);
            return mapper.Map<MovieDTO>(movie);
        }
    }
}
