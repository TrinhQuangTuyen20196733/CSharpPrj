using System.ComponentModel.DataAnnotations;

namespace BHDStarBooking.Entity.SharePoint
{
    public class SPAccountEntity: BaseEntity
    {
        [Required, EmailAddress]
        public string? email { get; set; }
        [Required, DataType(DataType.Password)]
        public string? password { get; set; }

    }
}
