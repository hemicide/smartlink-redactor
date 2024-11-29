using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Redactor.Application.Exceptions;

namespace Redactor.Application.ExceptionHandlers
{
    public class BadRequestExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<BadRequestExceptionHandler> _logger;

        public BadRequestExceptionHandler(ILogger<BadRequestExceptionHandler> logger) => _logger = logger;

        public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is not BadRequestException badFoundException)
                return false;

            _logger.LogError(badFoundException, $"Exception occurred: {badFoundException.Message}");

            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Bad Request",
                Detail = badFoundException.Message
            };

            context.Response.StatusCode = problemDetails.Status.Value;
            await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}
