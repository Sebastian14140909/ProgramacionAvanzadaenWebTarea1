using ReservationApp.Api.Exceptions;

namespace ReservationApp.Api.Middleware
{
    public class ErrorHandlingMiddleware: Exception
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;
        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
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
            catch (BusinessException ex)
            {
                _logger.LogError(ex, "Business exception occurred.");
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsJsonAsync(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                // Cambia la respuesta para ver el error real (SOLO PARA DESARROLLO)
                await context.Response.WriteAsJsonAsync(new
                {
                    error = ex.Message,
                    detail = ex.InnerException?.Message
                });
            }
        }
    }
}
