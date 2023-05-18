using BHDStarBooking.DTO;
using BHDStarBooking.Entity;
using BHDStarBooking.Entity.SharePoint;
using BHDStarBooking.Repository;
using BHDStarBooking.Service.IService;
using Microsoft.AspNetCore.Http;

namespace BHDStarBooking.Service.ServiceImpl
{
    public class SessionService : ISessionService
    {
        private readonly IMongoRepository<SessionEntity> sessionRepository;
        private readonly IRoomService roomService;
        private readonly IMovieService movieService;
        private readonly ISeatOnSessionService seatOnSessionService;
        private readonly ISeatService seatService;
        private readonly ISharePointRepository<SPSessionEntity> sharePointRepository;

        public SessionService(IMongoRepository<SessionEntity> sessionRepository, IRoomService roomService, IMovieService movieService, 
            ISeatOnSessionService seatOnSessionService, ISeatService seatService, ISharePointRepository<SPSessionEntity> sharePointRepository)
        {
            this.sessionRepository = sessionRepository;
            this.roomService = roomService;
            this.movieService = movieService;
            this.seatOnSessionService = seatOnSessionService;
            this.seatService = seatService;
            this.sharePointRepository = sharePointRepository;
        }

        public async Task deleteById(string id)
        {
             await sessionRepository.DeleteAsync(id);
        }

        public void deleteItemById(string id)
        {
           sharePointRepository.deleteListItemById(id);
        }

        public async Task<SessionDTOS> getByIdAsync(string id)
        {
            SessionEntity session = await sessionRepository.GetByIdAsync(id);
            MovieDTO movie = await movieService.getByIdAsync(session.movieId);
            var sessionDTOS = new SessionDTOS()
            {
                Id = session.Id,
                movieDTO = movie,
                cost = session.cost,
                startTime = session.startTime.ToString(),
                roomName = session.room.name,
                cinemaName = session.room.cinema.name,
            };
            return sessionDTOS;
        }

        public async Task<List<SessionDTOS>> getByPageAsync(int pageNumber)
        {
           List<SessionEntity> sessionEntities=  await sessionRepository.GetByPageAsync(pageNumber);
            var sessions = new List<SessionDTOS>();
            foreach (var session in sessionEntities)
            {
                MovieDTO movie = await movieService.getByIdAsync(session.movieId);
                var sessionDTOS = new SessionDTOS()
                {
                    Id = session.Id,
                    movieDTO=movie,
                    cost = session.cost,
                    startTime = session.startTime.ToString(),
                    roomName = session.room.name,
                    cinemaName = session.room.cinema.name,
                };
                sessions.Add(sessionDTOS);
            }
           return sessions;
        }

        public async Task<long> getTotalElementAsync()
        {
           return await sessionRepository.GetTotalItemAsync();
        }

        public async Task<List<SessionDTOS>> getUpcomingMovie(string id, DateTime startTimeDateTime)
        {
            List<SessionEntity> sessionEntities =  await sessionRepository.GetByCondition(session=>session.movieId == id&& session.startTime>startTimeDateTime);
            var sessions = new List<SessionDTOS>();
            foreach (var session in sessionEntities)
            {
                MovieDTO movie = await movieService.getByIdAsync(session.movieId);
                var sessionDTOS = new SessionDTOS()
                {
                    Id = session.Id,
                    movieDTO = movie,
                    cost = session.cost,
                    startTime = session.startTime.ToString(),
                    roomName = session.room.name,
                    cinemaName = session.room.cinema.name,
                };
                sessions.Add(sessionDTOS);
            }
            return sessions;
        }

        public SPSessionEntity insertItem(SPSessionEntity sessionDTO)
        {
            return sharePointRepository.insertListItem(sessionDTO);
        }

        public async Task<SessionDTO> insertOneAsync(SessionDTO sessionDTO)
        {
            var roomResult = await roomService.findByRoomNameAndCinemaId(sessionDTO.roomName, sessionDTO.movieSystem_id);
            /*string format = "yyyy-MM-dd'T'HH:mm:ss.fff'Z'";
            DateTime startTimeDateTime = DateTime.ParseExact(sessionDTO.startTime, format, null);*/
            DateTime startTimeDateTime = DateTime.Parse(sessionDTO.startTime);
            SessionEntity session = new SessionEntity
            { 
                movieId = sessionDTO.movie_id,
                cost = sessionDTO.cost,
                startTime=startTimeDateTime,
                room=roomResult
            };
            session = await sessionRepository.InsertAsync(session);
            List<SeatEntity> seatEntities = await seatService.getAllByRoomIdAsync(session.room.Id);
           foreach (var seatEntity in seatEntities)
            {
                var seatOnSession = new SeatOnSessionEntity()
                {
                    status=false,
                    session=session,
                    seat= seatEntity,
                };
               await seatOnSessionService.InsertOneAsync(seatOnSession);
            }
            var sessionResult = new SessionDTO
            {   
                Id = session.Id,
                movie_id = session.movieId,
                cost = session.cost,
                startTime = session.startTime.ToString(),
                roomName = session.room.name,
                movieSystem_id = session.room.cinema.Id
            };
            return sessionResult;
        }
    }
}
