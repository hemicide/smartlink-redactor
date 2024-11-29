using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Redactor.Application.DTO;
using Redactor.Application.Exceptions;
using Redactor.Application.Interfaces;
using System.Text.RegularExpressions;

namespace Redactor.Application.Middleware
{
    public class RequestBodyValidationMiddleware
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly RequestDelegate _next;

        public RequestBodyValidationMiddleware(IServiceProvider serviceProvider, RequestDelegate next)
        {
            _serviceProvider = serviceProvider;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Method == HttpMethods.Post || context.Request.Method == HttpMethods.Put)
            {
                context.Request.EnableBuffering();

                using (var reader = new StreamReader(context.Request.Body, leaveOpen: true))
                {
                    var body = await reader.ReadToEndAsync();
                    context.Request.Body.Position = 0;

                    var linkRequest = JsonConvert.DeserializeObject<LinkRequest>(body);
                    var validationArgsResults = new Dictionary<string, string[]>();

                    #region Validate link
                    var parrent = "^[^_][\\w\\d-_]+$";
                    var isMatch = new Regex(parrent).IsMatch(linkRequest!.Link);
                    if (!isMatch)
                        validationArgsResults["Link"] = new string[] { @$"Link value ""{linkRequest.Link}"" not valid. Parrent: ""{parrent}""" };
                    #endregion

                    foreach (var rule in linkRequest!.Rules)
                    {
                        #region Validate Args
                        foreach (var predicate in rule.Predicates)
                        {
                            var validator = _serviceProvider.GetKeyedService<IRequestBodyValidable>(predicate.ToLower());
                            if (validator == null)
                                throw new NotFoundException($"Validator for predicate \"{predicate}\" was not found");

                            var (ok, problems) = validator!.Validate(rule.Args);
                            if (!ok)
                                foreach (var field in problems.Keys)
                                    validationArgsResults[field] = problems[field];
                        }
                        #endregion

                        #region Validate Redirect field
                        if (!(Uri.TryCreate(rule.RedirectTo, UriKind.Absolute, out var uriResult)
                            && (uriResult?.Scheme == Uri.UriSchemeHttp || uriResult?.Scheme == Uri.UriSchemeHttps)))
                            validationArgsResults["RedirectTo"] = new string[] { @$"Redirect url ""{rule.RedirectTo}"" not valid" };
                        #endregion
                    }

                    if (validationArgsResults.Count > 0)
                        throw new ValidationException("Validation problems", validationArgsResults);
                }
            }

            await _next(context);
        }
    }
}
