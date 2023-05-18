using BHDStarBooking.DTO;

namespace BHDStarBooking.Entity.SharePoint
{
    public class SPReceipt : BaseEntity
    {
        public string? email { get; set; }
        public List<int>? seats { get; set; }
        public List<int>? services { get; set; }
    }
}
