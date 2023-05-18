using BHDStarBooking.Entity;

namespace BHDStarBooking.DTO
{
    public class MovieDTO : BaseEntity
    {
        public string? title { get; set; }
        public byte[]? thumbnail { get; set; }
        private string? alt { get; set; }
        public string? trailer { get; set; }
        public string? shortDescription { get; set; }
        public string? description { get; set; }
        public string? classify { get; set; }
        public string? director { get; set; }
        public string? actor { get; set; }
        public string? type { get; set; }
        public DateTime? startDate { get; set; }
        public int? length { get; set; }
        public string? language { get; set; }
    }
}
