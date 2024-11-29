using Redactor.Application.ExceptionHandlers;
using Redactor.Application.Interfaces;
using Redactor.Application.Middleware;
using Redactor.Application.Profiles;
using Redactor.Application.Services;
using Redactor.Persistence;
using Redactor.WebAPI.Swagger;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddProblemDetails();
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddSwaggerGenWithConfig().AddSwaggerGenNewtonsoftSupport();

#region ExceptionHandlers
builder.Services.AddExceptionHandler<ValidationExceptionHandler>();
builder.Services.AddExceptionHandler<BadRequestExceptionHandler>();
builder.Services.AddExceptionHandler<NotFoundExceptionHandler>();
builder.Services.AddExceptionHandler<JsonSerializationExceptionHandler>();
builder.Services.AddExceptionHandler<JsonReaderExceptionHandler>();
builder.Services.AddExceptionHandler<ExceptionHandler>();
#endregion

builder.Services.AddScoped<ISmartLinksService, SmartLinksService>();

#region Validators

builder.Services.AddValidatorRegistration();

//builder.Services.AddKeyedSingleton<IRequestBodyValidable, Redactor.WebAPI.Validators.Browser.Validator>(Redactor.WebAPI.Validators.Browser.Validator.Predicate);
//builder.Services.AddKeyedSingleton<IRequestBodyValidable, Redactor.WebAPI.Validators.DateRange.Validator>(Redactor.WebAPI.Validators.DateRange.Validator.Predicate);
//builder.Services.AddKeyedSingleton<IRequestBodyValidable, Redactor.WebAPI.Validators.Device.Validator>(Redactor.WebAPI.Validators.Device.Validator.Predicate);
//builder.Services.AddKeyedSingleton<IRequestBodyValidable, Redactor.WebAPI.Validators.Language.Validator>(Redactor.WebAPI.Validators.Language.Validator.Predicate);
#endregion

#region Repository
builder.Services.AddRepository(builder.Configuration);
#endregion

#region AutoMapper
builder.Services.AddAutoMapper(configuration => configuration.AddProfile<SmartLinkProfile>());
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();

app.UseMiddleware<RequestBodyValidationMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
