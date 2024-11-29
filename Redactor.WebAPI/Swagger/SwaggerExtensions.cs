using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Redactor.WebAPI.Swagger
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwaggerGenWithConfig(this IServiceCollection services)
        {
            return services.AddSwaggerGen(c =>
            {
                var xmlPath = Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
                c.IncludeXmlComments(xmlPath);
                c.UseAllOfForInheritance();
                c.UseOneOfForPolymorphism();
                c.UseDateOnlyTimeOnlyStringConverters();

                //options.EnableAnnotations(true, true);
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "1",
                    Title = "Title",
                    Description = "Description"
                });

                c.SwaggerGeneratorOptions.Servers = new List<OpenApiServer>()
                {
                    // set the urls folks can reach server
                    new() {Url = "/" }
                };

                //c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                //{
                //    Name = "Authorization",
                //    In = ParameterLocation.Header,
                //    Type = SecuritySchemeType.ApiKey,
                //    Scheme = "Bearer",
                //    BearerFormat = "JWT",
                //    Description = "Input your Bearer token in this format - Bearer {your token here} to access this API",
                //});

                //c.AddSecurityRequirement(new OpenApiSecurityRequirement
                //{
                //    {
                //        new OpenApiSecurityScheme
                //        {
                //            Reference = new OpenApiReference
                //            {
                //                Type = ReferenceType.SecurityScheme,
                //                Id = "Bearer",
                //            },
                //            Scheme = "Bearer",
                //            Name = "Bearer",
                //            In = ParameterLocation.Header,
                //        }, new List<string>()
                //    },
                //});
            });
        }
    }
}
