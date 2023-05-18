using BHDStarBooking.DTO;

namespace BHDStarBooking.Entity
{
    public class Receipt : BaseEntity
    {
        public string? email { set; get; }
        public List<SeatOnSessionEntity>? seats { get; set; }
        public List<ServiceAmount>? services { get; set; }
    }
}
