using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;


namespace PhoneDirectory.API.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                // Move to next middleware/controller
                await _next(context);
            }
            catch (Exception ex)
            {
                // Catch any unhandled exceptions
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var error = new
            {
                message = ex.Message,
                statusCode = response.StatusCode
            };

            var json = JsonSerializer.Serialize(error);
            return response.WriteAsync(json);
        }
    }
}
