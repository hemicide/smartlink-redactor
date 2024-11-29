using Redactor.Application.Interfaces;
using System.Reflection;

namespace Redactor.Persistence
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddValidatorRegistration(this IServiceCollection services)
        {
            var rootPath = Assembly.GetExecutingAssembly().Location;
            var dirRootPath = Path.GetDirectoryName(rootPath);
            var mask = "Redactor.Application.Validators.*.dll";
            var files = Directory.EnumerateFiles(dirRootPath, mask, SearchOption.TopDirectoryOnly);

            foreach (var file in files)
            {
                var asm = Assembly.LoadFrom(file);
                var validatorType = asm.ExportedTypes.SingleOrDefault(t => t.Name.EndsWith("Validator"));
                if (validatorType != null)
                {
                    var predicateProperty = validatorType.GetProperty("Predicate");
                    var predicateValue = predicateProperty?.GetValue(validatorType)?.ToString()?.ToLower();
                    services.Add(new ServiceDescriptor(typeof(IRequestBodyValidable), predicateValue, validatorType, ServiceLifetime.Singleton));
                }
            }

            return services;
        }
    }
}
