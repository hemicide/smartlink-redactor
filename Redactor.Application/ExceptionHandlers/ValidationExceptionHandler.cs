using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Redactor.Application.Exceptions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Redactor.Application.ExceptionHandlers
{
    public class ValidationExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<ValidationExceptionHandler> _logger;

        public ValidationExceptionHandler(ILogger<ValidationExceptionHandler> logger) => _logger = logger;

        public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
        {
            var b = exception.GetType();

            if (exception is not ValidationException validationException)
                return false;

            _logger.LogError(validationException, $"Exception occurred: {validationException.Message}");

            var problemDetails = new ValidationProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Bad request",
                Detail = validationException.Message,
                Errors = validationException.Problems
            };

            context.Response.StatusCode = problemDetails.Status.Value;
            await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}
