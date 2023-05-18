using BHDStarBooking.DTO;
using BHDStarBooking.DTO.Response;
using BHDStarBooking.DTO.Response.Page;
using BHDStarBooking.Entity;
using BHDStarBooking.Service.IService;
using BHDStarBooking.Service.ServiceImpl;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;

namespace BHDStarBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
/*
    [Authorize(Roles = "ADMIN")]*/
    public class SessionController : ControllerBase
    {
        private readonly ISessionService sessionService;

        public SessionController(ISessionService sessionService)
        {
            this.sessionService = sessionService;
        }
        [HttpPost]
        public async Task<SessionDTO> createSession([FromBody] SessionDTO sessionDTO)
        {
            return await sessionService.insertOneAsync(sessionDTO);
        }
        [HttpGet("pages/{pageNumber}")]
        public async Task<SessionPage> getSessionByPageAsync(int pageNumber)
        {
            List<SessionDTOS> sessionDTOS = await sessionService.getByPageAsync(pageNumber);
            long totalElements = await sessionService.getTotalElementAsync();
            var sessionPage = new SessionPage
            {
                totalItemPage = totalElements,
                totalPage = totalElements / 8 + 1,
                sessionDTOList = sessionDTOS

            };
            return sessionPage;
        }
        [HttpGet("/api/movies/{movie_id:length(24)}/upcoming/[controller]/")]
        [AllowAnonymous]

        public async Task<List<SessionDTOS>> findUpcomingSessionByMovieId([FromRoute] string movie_id, [FromQuery] string date)
        {
            DateTime startTimeDateTime = DateTime.Parse(date);
            return await sessionService.getUpcomingMovie(movie_id, startTimeDateTime);

        }
        [HttpDelete("{id:length(24)}")]
        public async Task<MessageResponse> DeleteSessionById(string id)
        {
            try
            {
                await sessionService.deleteById(id);
                return new MessageResponse()
                {
                    status = (int)HttpStatusCode.OK,
                    description = "Bạn đã xóa suất chiếu thành công"
                };
            }
            catch (Exception e)
            {
                return new MessageResponse()
                {
                    status = (int)HttpStatusCode.BadRequest,
                    description = "Xóa suất chiếu thất bại"
                };
            }
        }
        [HttpGet("{id:length(24)}")]
        [AllowAnonymous]

        public async Task<SessionDTOS> GetSesionByIdAsync(string id)
        {
            return await sessionService.getByIdAsync(id);
        }

    }
}
