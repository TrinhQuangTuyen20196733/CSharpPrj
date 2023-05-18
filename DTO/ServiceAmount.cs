using BHDStarBooking.Entity;

namespace BHDStarBooking.DTO
{
    public class ServiceAmount: BaseEntity
    {
        public int? count { set; get; }
        public string? service_id { set; get; }
    }
}
