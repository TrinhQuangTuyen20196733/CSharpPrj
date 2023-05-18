namespace BHDStarBooking.DTO.Response
{
    public class LoginResponse
    {
        public string? message { get; set; }
        public string? jwtToken { set; get; }
        public string? email { set; get; }
        public string? accountId { set; get; }
        public List<string>? roles { set; get; }

    }
}
