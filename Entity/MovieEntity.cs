using BHDStarBooking.DTO;

namespace BHDStarBooking.Entity
{
    public class MovieEntity : BaseEntity
    {
        public string? title { get; set; }
        public string? thumbnail { get; set; } = string.Empty;
        private string? alt { get; set; }
        public string? trailer { get; set; }
        public string? shortDescription { get; set; }
        public string? classify { get; set; }
        public string? director { get; set; }
        public string? actor { get; set; }
        public string? type { get; set; }
        public DateTime? startDate { get; set; }
        public int? length { get; set; }
        public string? language { get; set; }

        
    }
}
