using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

namespace Redactor.Application.ExceptionHandlers
{
    public class ExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<ExceptionHandler> _logger;

        public ExceptionHandler(ILogger<ExceptionHandler> logger) => _logger = logger;

        public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(exception, $"Exception occurred: {exception.Message}");

            var extensions = new Dictionary<string, object>();

            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Internal Server Error",
                Detail = exception.Message,
            };

            if (exception.StackTrace != null)
                extensions.Add("details", exception.StackTrace);

            extensions.Add("headers", context.Request.Headers);

            if (context.Request.Path.HasValue)
                extensions.Add("path", context.Request.Path.Value);

            var endpoint = context.Items["__OriginalEndpoint"] as RouteEndpoint;
            if (endpoint != null)
            {
                extensions.Add("endpoint", endpoint.DisplayName);
                extensions.Add("routeValues", endpoint.RoutePattern.RequiredValues);
            }

            problemDetails.Extensions.Add("exception", extensions);

            context.Response.StatusCode = problemDetails.Status.Value;
            await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}
