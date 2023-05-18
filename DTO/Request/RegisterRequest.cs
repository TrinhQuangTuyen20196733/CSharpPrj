using BHDStarBooking.Entity;
using System.ComponentModel.DataAnnotations;

namespace BHDStarBooking.DTO.Request
{
    public class RegisterRequest : BaseEntity
    {
        [Required,EmailAddress]
        public string? email { get; set; }
        [Required, DataType(DataType.Password)]
        public string? password { get; set; }
        [Required]
        public string? lastName { get; set; }
        [Required]
        public List<string>? roles { get; set; }
        [Required]
        public string? firstName { get; set; }
        [Required, DataType(DataType.PhoneNumber)]
        public string? phoneNumber { get; set; }
        [Required]
        public string? address { get; set; }
        [Required, DataType(DataType.DateTime)]
        public DateTime? birthDay { get; set; }
    }
}
