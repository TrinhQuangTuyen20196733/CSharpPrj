using Amazon.Runtime.Internal;
using BHDStarBooking.ExceptionHandler.ExceptionModel;
using System.Net;
using System.Text.Json;
using ErrorResponse = BHDStarBooking.ExceptionHandler.ExceptionModel.ErrorResponse;

namespace BHDStarBooking.ExceptionHandler
{
    public class ExceptionHandlerMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleWare> _logger;

        public ExceptionHandlerMiddleWare(RequestDelegate next, ILogger<ExceptionHandlerMiddleWare> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var response = context.Response;

            var errorResponse = new ErrorResponse();
            switch (exception)
            {
                case ResourceNotFoundException ex:
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    errorResponse.message = ex.Message;
                    errorResponse.status= (int)HttpStatusCode.NotFound;
                    break;
                case IndexConstraintException ex:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    errorResponse.message = ex.Message;
                    errorResponse.status = (int)HttpStatusCode.BadRequest;
                    break;
                case ApplicationException ex:
                    if (ex.Message.Contains("Invalid Token"))
                    {
                        response.StatusCode = (int)HttpStatusCode.Forbidden;
                        errorResponse.message = ex.Message;
                        errorResponse.status = (int)HttpStatusCode.Forbidden;
                        break;
                    }
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    errorResponse.message = ex.Message;
                    errorResponse.status = (int)HttpStatusCode.BadRequest;
                    break;
                
                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    errorResponse.message = "Internal server error!";
                    errorResponse.status = (int)HttpStatusCode.InternalServerError;
                    break;
            }
            _logger.LogError(exception.Message);
            var result = JsonSerializer.Serialize(errorResponse);
            await context.Response.WriteAsync(result);
        }
    }
}
