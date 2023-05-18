using BHDStarBooking.Entity;

namespace BHDStarBooking.DTO.Response.Page
{
    public class ServicePage: BasePage
    {
        public List<ServiceEntity>? serviceDTOList {  get; set; }
    }
}
