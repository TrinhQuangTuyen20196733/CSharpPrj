namespace BHDStarBooking.ExceptionHandler.ExceptionModel
{
    public class IndexConstraintException : Exception
    {
        public IndexConstraintException(string message):base(message) { }
    }
}
