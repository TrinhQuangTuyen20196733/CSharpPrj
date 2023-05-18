using BHDStarBooking.Entity;

namespace BHDStarBooking.DTO
{
    public class SessionDTOS : BaseEntity
    {
        public MovieDTO? movieDTO { get; set; }
        public string? cinemaName { get; set; }
        public string? roomName { get; set; }
        public string? startTime { get; set; }
        public int? cost { get; set; }
    }
}
