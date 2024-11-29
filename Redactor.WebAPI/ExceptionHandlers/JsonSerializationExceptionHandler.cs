using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Redactor.ExceptionHandlers
{
    public class JsonSerializationExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<JsonReaderExceptionHandler> _logger;

        public JsonSerializationExceptionHandler(ILogger<JsonReaderExceptionHandler> logger) => _logger = logger;

        public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is not JsonSerializationException jsonSerializationException)
                return false;

            _logger.LogError(jsonSerializationException, $"Exception occurred: {jsonSerializationException.Message}");

            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Bad Request",
                Detail = jsonSerializationException.Message
            };

            context.Response.StatusCode = problemDetails.Status.Value;
            await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}
