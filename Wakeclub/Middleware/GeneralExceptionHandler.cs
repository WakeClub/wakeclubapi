using Microsoft.AspNetCore.Diagnostics;

namespace Wakeclub.Middleware
{
    public class GeneralExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GeneralExceptionHandler> _logger;

        public GeneralExceptionHandler(ILogger<GeneralExceptionHandler> logger)
        {
            _logger = logger;
        }
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(exception, "Server Error. ");

            httpContext.Response.StatusCode = 500;
            await httpContext.Response.WriteAsync("Server Error. ", cancellationToken);
            return true;
        }
    }
}