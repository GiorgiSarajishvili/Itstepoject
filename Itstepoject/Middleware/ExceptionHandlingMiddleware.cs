using Microsoft.EntityFrameworkCore;

namespace Itstepoject.Middleware
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

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogWarning(ex, "Concurrency conflict while saving changes");
                await WriteJsonAsync(context, StatusCodes.Status409Conflict,
                    new { error = "The record was modified or deleted by another user. Refresh and try again." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception");
                await WriteJsonAsync(context, StatusCodes.Status500InternalServerError,
                    new { error = "An unexpected error occurred." });
            }
        }

        private async Task WriteJsonAsync(HttpContext context, int statusCode, object body)
        {
            if (context.Response.HasStarted)
            {
                return;
            }

            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsJsonAsync(body);
        }
    }
}
