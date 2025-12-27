using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Data;

namespace Core.CrossCutting.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly IHostEnvironment _environment;

        public ExceptionHandlingMiddleware(
            RequestDelegate next, 
            ILogger<ExceptionHandlingMiddleware> logger, 
            IHostEnvironment environment)
        {
             _next = next;
            _logger = logger;
            _environment = environment;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var problems = new ProblemDetails
            {
                Instance = context.Request.Path
            };

            switch(exception)
            {
                case DuplicateNameException:
                    problems.Status = StatusCodes.Status401Unauthorized;
                    problems.Title = "Conflict";
                    problems.Detail = exception.Message;
                    break;

                case UnauthorizedAccessException:
                    problems.Status = StatusCodes.Status401Unauthorized;
                    problems.Title = "Unauthorized";
                    problems.Detail = exception.Message;
                    break;

                default:
                    problems.Status = StatusCodes.Status500InternalServerError;
                    problems.Title = "Internal Server Error";
                    problems.Detail = _environment.IsDevelopment() ? exception.ToString() : "An unexpected error occurred";
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = problems.Status.Value;

            await context.Response.WriteAsJsonAsync(problems);
        }
    }
}
