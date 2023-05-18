using BHDStarBooking.Entity;

namespace BHDStarBooking.DTO.Response.Page
{
    public class SessionPage : BasePage
    {
       public List<SessionDTOS>? sessionDTOList {  get; set; }
    }
}
