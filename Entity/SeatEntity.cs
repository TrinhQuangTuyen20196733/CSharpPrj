namespace BHDStarBooking.Entity
{
    public class SeatEntity:BaseEntity
    {
        public int? row { set; get; }
        public int? column { set; get; }
        public string? type { set; get; }
        public RoomEntity? room { set; get; }
    }
}
