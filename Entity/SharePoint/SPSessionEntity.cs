namespace BHDStarBooking.Entity.SharePoint
{
    public class SPSessionEntity : BaseEntity
    {
        public double? cost { get; set; }
        public int? movieId { get; set; }
        public DateTime? startTime { get; set; }
        public int? roomID { get; set; }
    }

}
