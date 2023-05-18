using BHDStarBooking.Entity;

namespace BHDStarBooking.DTO.Response.Page
{
    public class UserPage: BasePage
    {
        public List<UserEntity> userDTOList { get; set; } 
    }
}
