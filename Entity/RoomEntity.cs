namespace BHDStarBooking.Entity
{
    public class RoomEntity : BaseEntity { 
  
        public string? name { set; get; }
        public string? type { set; get; }
       public CinemaEntity? cinema { set; get; }

    }
}
