using BHDStarBooking.Entity;

namespace BHDStarBooking.DTO
{
    public class ReceiptDTO
    {
        public UserEntity? user {  get; set; }
        public List<SeatOnSessionEntity> seats { get; set; }
        public List<ServiceAmount> services { get; set; }
    }
}
