using CurrencyApiInfrastructure.AppSettings;
using CurrencyApiInfrastructure.ExceptionHandling.Extensions;
using CurrencyApiInfrastructure.Extensions;
using CurrencyApiInfrastructure.Extensions.TokenExtensions;
using CurrencyApiInfrastructure.Resources;
using CurrencyApiLib.MapperConfigurations;
using CurrencyApiLib.Services.CurrencyRate.Classes;
using CurrencyApiLib.Services.CurrencyRate.Interfaces;
using CurrencyApiLib.ServicesExtensions;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Localization;
using Serilog;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((hostContext, loggerConfiguration) =>
{
    loggerConfiguration.ReadFrom.Configuration(builder.Configuration);
});

var configuration = builder.Configuration;

builder.Services.Configure<AppSetting>(configuration);
var appSetting = configuration.Get<AppSetting>();

builder.Services.AddTransient<IAppSetting, AppSetting>();

builder.Services.AddScoped<ICurrencyRateService, CurrencyRateService>();

builder.Services.AddCustomLocalizationConfiguration();

ValidatorOptions.Global.DefaultRuleLevelCascadeMode = CascadeMode.Continue;
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<MapsterMapping>();

builder.Services.AddRangeCustomServices(appSetting);
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});

builder.Services.AddCustomSwaggerConfiguration();

builder.Services.AddCors();

var app = builder.Build();

GenerateAndVerifyPasswords.Localizer = app.Services.GetService<IStringLocalizer<ExceptionsResource>>();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CurrencyApi v1"));
}

app.UseSerilogRequestLogging();

app.UseRequestLocalization();

app.UseStaticHttpContext();

app.UseCors(opts =>
        opts.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().WithExposedHeaders("Content-Disposition")
);

app.UseCustomGlobalExceptionHandler(app.Services.GetService<IStringLocalizer<SharedResource>>());

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();

