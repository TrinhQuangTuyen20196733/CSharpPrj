using BHDStarBooking.Entity;
using BHDStarBooking.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BHDStarBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    [Authorize(Roles = "ADMIN")]

    public class SeatOnSessionController : ControllerBase
    {
        private ISeatOnSessionService seatOnSessionService;

        public SeatOnSessionController(ISeatOnSessionService seatOnSessionService)
        {
            this.seatOnSessionService = seatOnSessionService;
        }
        [AllowAnonymous]
        [HttpGet("/api/sessions/{session_id:length(24)}/[controller]")]
        public async Task<List<SeatOnSessionEntity>> findAllSeatOnSessionsBySessionId(string session_id)
        {
            return await seatOnSessionService.findAllBySessionId(session_id);
        }
    }
}
