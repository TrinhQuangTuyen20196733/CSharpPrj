using BHDStarBooking.Entity;

namespace BHDStarBooking.DTO
{
    public class SessionDTO : BaseEntity
    {
        public string? movie_id { get; set; }
        public string? movieSystem_id { get;set; }
        public string? roomName { get; set; }
        public string? startTime { get; set;}
        public int? cost { get; set;}
    }
}
