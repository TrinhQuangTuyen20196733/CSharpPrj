namespace BHDStarBooking.ExceptionHandler.ExceptionModel
{
    public class ResourceNotFoundException : Exception
    {
        public ResourceNotFoundException(String message) : base(message) { }
    }
}
