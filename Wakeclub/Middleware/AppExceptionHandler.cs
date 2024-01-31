using Microsoft.AspNetCore.Diagnostics;
using Wakeclub.Exceptions;

namespace Wakeclub.Middleware
{
    public class AppExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<AppExceptionHandler> _logger;

        public AppExceptionHandler(ILogger<AppExceptionHandler> logger )
        {
            _logger = logger;
        }
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            (int statusCode, string errorMessage) = exception switch
            {
                BadRequestException badRequestException => (400, badRequestException.Message),
                NotFoundException notFoundException => (404, notFoundException.Message),
                InternalServerErrorException internalServerErrorException => (500, internalServerErrorException.Message),
                _ => default
            };

            if(statusCode == default)
            {
                return false;
            }
            _logger.LogError(exception, exception.Message);
            httpContext.Response.StatusCode = statusCode;
            await httpContext.Response.WriteAsJsonAsync(errorMessage);

            return true;
        }
    }
}