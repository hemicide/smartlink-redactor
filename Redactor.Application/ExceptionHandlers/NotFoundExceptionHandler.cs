using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Redactor.Application.Exceptions;

namespace Redactor.Application.ExceptionHandlers
{
    public class NotFoundExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<NotFoundExceptionHandler> _logger;

        public NotFoundExceptionHandler(ILogger<NotFoundExceptionHandler> logger) => _logger = logger;

        public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is not NotFoundException notFoundException)
                return false;

            _logger.LogError(notFoundException, $"Exception occurred: {notFoundException.Message}");

            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Not Found",
                Detail = notFoundException.Message
            };

            context.Response.StatusCode = problemDetails.Status.Value;
            await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}
