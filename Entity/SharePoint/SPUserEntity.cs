namespace BHDStarBooking.Entity.SharePoint
{
    public class SPUserEntity : BaseEntity
    {
        public string? lastName { get; set; }
        public string? firstName { get; set; }
        public string? phoneNumber { get; set; }
        public string? address { get; set; }
        public DateTime? birthDay { get; set; }
        public int? accountID { get; set; }
    }
}
