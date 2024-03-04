using Microsoft.AspNetCore.Diagnostics;
using Wakeclub.Exceptions;

namespace Wakeclub.Middleware;
public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger )
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
            _ => (500, $"Exception message: {exception.Message} \n Stack trace: {httpContext.TraceIdentifier}")
        };
        
        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response.WriteAsJsonAsync(errorMessage);

        return false;
    }
}
