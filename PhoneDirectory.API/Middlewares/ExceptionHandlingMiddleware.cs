using Microsoft.AspNetCore.Http;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using FluentValidation; 
using System.Linq;
using Microsoft.Extensions.Logging;

namespace PhoneDirectory.API.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException validationException)
            {
                _logger.LogWarning(validationException, "FluentValidation error occurred.");
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                var errors = validationException.Errors.Select(e => e.ErrorMessage);
                var result = JsonSerializer.Serialize(new
                {
                    message = string.Join("; ", errors),
                    statusCode = 400
                });
                await context.Response.WriteAsync(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            var result = JsonSerializer.Serialize(new
            {
                message = "An internal server error has occurred.",
                statusCode = 500
            });
            return context.Response.WriteAsync(result);
        }
    }
}