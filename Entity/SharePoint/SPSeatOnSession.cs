namespace BHDStarBooking.Entity.SharePoint
{
    public class SPSeatOnSession : BaseEntity
    {
        public bool? status { get; set; }
        public int? sessionID { get; set; }
        public int? seatID { get; set; }
    }

}
