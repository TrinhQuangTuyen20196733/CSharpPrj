namespace BHDStarBooking.Entity
{
    public class Movie : BEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? Length { get; set; }
        public string? Language { get; set; }
    }
}
