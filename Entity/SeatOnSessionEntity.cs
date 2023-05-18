namespace BHDStarBooking.Entity
{
    public class SeatOnSessionEntity : BaseEntity
    {
        public bool? status { get; set; }
        public SessionEntity? session { get; set; }
        public SeatEntity? seat { get; set; }
    }
}
