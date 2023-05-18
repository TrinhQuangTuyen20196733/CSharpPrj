namespace BHDStarBooking.Entity
{
    public class SessionEntity: BaseEntity
    {
        public int? cost { get; set; }
        public string? movieId { get; set; }
        public DateTime? startTime { get; set; }
        public RoomEntity? room { get; set; }
    }
}
