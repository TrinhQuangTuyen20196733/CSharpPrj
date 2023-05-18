using System.ComponentModel.DataAnnotations;

namespace BHDStarBooking.Entity.SharePoint
{
    public class SPAccount : BaseEntity 
    {

        [Required, EmailAddress]
        public string? email { get; set; }
        [Required, DataType(DataType.Password)]
        public string? password { get; set; }

        public List<int>? roles { get; set; }
    }
}
