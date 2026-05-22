using System.Net;
using System.Text.Json;


namespace ClinicSystem.Middleware
{
    public class GlobalExceptionMiddleware
    {

        private readonly RequestDelegate _next; // Middleware pipeline delegate

        private readonly ILogger<GlobalExceptionMiddleware> _logger; // Logger for logging exceptions

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger) // Constructor to initialize the middleware with the next delegate and logger
        {
            _next = next;
            _logger = logger;
        }

        //entry point for Middleware
        public async Task InvokeAsync(HttpContext context) // Method to handle the incoming HTTP request and catch any unhandled exceptions
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");
                await HandleExceptionAsync(context, ex);
            }
        }
        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)  // Method to handle the exception and return a standardized error response
        {
            context.Response.ContentType = "application/json"; // Set the response content type to JSON

            var (statusCode, message) = exception switch // Use pattern matching to determine the appropriate HTTP status code and message based on the type of exception
            {
                KeyNotFoundException => (HttpStatusCode.NotFound, exception.Message),  //404 Not Found
                InvalidOperationException => (HttpStatusCode.BadRequest, exception.Message), //400 Bad Request
                UnauthorizedAccessException => (HttpStatusCode.Unauthorized, exception.Message), //401 Unauthorized
                ArgumentException => (HttpStatusCode.BadRequest, exception.Message), //400 Bad Request
                _ => (HttpStatusCode.InternalServerError, "An unexpected error occurred.") //500 Internal Server Error for any other unhandled exceptions
            };

            context.Response.StatusCode = (int)statusCode; // Set the HTTP status code of the response

            var response = new
            {
                StatusCode = (int)statusCode,
                Message = message,
                Timestamp = DateTime.UtcNow
            };

            // Serialize the response object to JSON and write it to the response body
            await context.Response.WriteAsync(JsonSerializer.Serialize(response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            }));
        }
    }
}