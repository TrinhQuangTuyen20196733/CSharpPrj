namespace BHDStarBooking.Entity.SharePoint
{
    public class SPSeatEntity :BaseEntity
    {
        public int? row { set; get; }
        public int? column { set; get; }
        public string? type { set; get; }
        public int? roomID { set; get; }
    }
}
