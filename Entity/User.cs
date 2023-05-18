using System.Text.Json.Serialization;

namespace BHDStarBooking.Entity
{
    public class User : BEntity
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? Age { get; set; }
        public string? Address { get; set; }
    }
}
