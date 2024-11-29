using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Redactor.Application.ExceptionHandlers
{
    public class JsonReaderExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<JsonReaderExceptionHandler> _logger;

        public JsonReaderExceptionHandler(ILogger<JsonReaderExceptionHandler> logger) => _logger = logger;

        public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is not JsonReaderException jsonReaderException)
                return false;

            _logger.LogError(jsonReaderException, $"Exception occurred: {jsonReaderException.Message}");

            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Bad Request",
                Detail = jsonReaderException.Message
            };

            context.Response.StatusCode = problemDetails.Status.Value;
            await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}
