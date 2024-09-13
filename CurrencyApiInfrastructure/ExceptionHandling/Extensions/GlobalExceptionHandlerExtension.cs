using CurrencyApiInfrastructure.ExceptionHandling.Dtos;
using CurrencyApiInfrastructure.Resources;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Localization;
using System.Net;
using System.Net.Mime;
using System.Text.Json;

namespace CurrencyApiInfrastructure.ExceptionHandling.Extensions;

/// <summary>
/// The global exception handler extension.
/// </summary>
public static class GlobalExceptionHandlerExtension
{
    /// <summary>
    /// Use custom global exception handler.
    /// </summary>
    /// <param name="app">The app.</param>
    /// <param name="localizer">The localizer.</param>
    public static void UseCustomGlobalExceptionHandler(this WebApplication app, IStringLocalizer<SharedResource> localizer)
    {
        app.UseExceptionHandler(builder =>
        {
            builder.Run(async context =>
            {
                var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                var exception = exceptionHandlerPathFeature!.Error;

                if (exception is CustomHttpException exception1)
                {
                    context.Response.ContentType = MediaTypeNames.Application.Json;
                    context.Response.StatusCode = (int)exception1.HttpStatusCode;
                    await context.Response.WriteAsync(JsonSerializer.Serialize(new ErrorDto(exception.Message, (int)exception1.HttpStatusCode)));
                }
                else
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = MediaTypeNames.Application.Json;

                    var errorObject = new ErrorDto(localizer["Internal_Server_Error"], 500);
                    if (app.Environment.IsDevelopment())
                    {
                        errorObject.InternalErrorMessage = exception.Message;
                        errorObject.InternalSource = exception.Source;
                        errorObject.InternalStackTrace = exception.StackTrace;
                    }

                    // ExceptionDispatchInfo.Capture(exception).Throw();
                    await context.Response.WriteAsync(JsonSerializer.Serialize(errorObject));
                }
            });
        });
    }
}