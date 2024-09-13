using CurrencyApiInfrastructure.AppSettings;
using CurrencyApiInfrastructure.ExceptionHandling.Extensions;
using CurrencyApiInfrastructure.Extensions;
using CurrencyApiInfrastructure.Extensions.TokenExtensions;
using CurrencyApiInfrastructure.Resources;
using CurrencyApiLib.MapperConfigurations;
using CurrencyApiLib.Services.Cache.Classes;
using CurrencyApiLib.Services.Cache.Interfaces;
using CurrencyApiLib.Services.CurrencyRate.Classes;
using CurrencyApiLib.Services.CurrencyRate.Interfaces;
using CurrencyApiLib.ServicesExtensions;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Localization;
using Serilog;
using System.Text.Json.Serialization;
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;


var builder = WebApplication.CreateBuilder(args);

builder.Host
    .UseSerilog(
        (hostContext, loggerConfiguration) =>
        {
            loggerConfiguration.ReadFrom.Configuration(builder.Configuration);
        });

var configuration = builder.Configuration;

//builder.Services.Configure<MyRateLimitOptions>(
//    builder.Configuration.GetSection(MyRateLimitOptions.MyRateLimit));

//Rate limiting of requests
builder.Services.AddRateLimiter(_ => _
    .AddFixedWindowLimiter(policyName: "fixed", options =>
    {
        options.PermitLimit = 4;
        options.Window = TimeSpan.FromSeconds(12);
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        options.QueueLimit = 2;
    }));

//var myOptions = new MyRateLimitOptions();
//builder.Configuration.GetSection(MyRateLimitOptions.MyRateLimit).Bind(myOptions);
//var fixedPolicy = "fixed";

builder.Services.Configure<AppSetting>(configuration);
var appSetting = configuration.Get<AppSetting>();

builder.Services.AddTransient<IAppSetting, AppSetting>();

builder.Services.AddScoped<ICacheService, CacheService>();

builder.Services.AddScoped<ICurrencyRateService, CurrencyRateService>();

builder.Services.AddCustomLocalizationConfiguration();

ValidatorOptions.Global.DefaultRuleLevelCascadeMode = CascadeMode.Continue;
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<MapsterMapping>();

builder.Services.AddRangeCustomServices(appSetting);
builder.Services
    .AddControllers()
    .AddJsonOptions(
        options =>
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

app.UseRateLimiter();
//static string GetTicks() => (DateTime.Now.Ticks & 0x11111).ToString("00000");

//app.MapGet("/", () => Results.Ok($"Fixed Window Limiter {GetTicks()}"))
//                           .RequireRateLimiting(fixedPolicy);

app.UseSerilogRequestLogging();

app.UseRequestLocalization();

app.UseStaticHttpContext();

app.UseCors(
    opts => opts.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().WithExposedHeaders("Content-Disposition")
);

app.UseCustomGlobalExceptionHandler(app.Services.GetService<IStringLocalizer<SharedResource>>());

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();

