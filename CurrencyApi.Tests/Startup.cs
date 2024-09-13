using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using CurrencyApiLib;
using CurrencyApiDomain.ContextRelated;
using CurrencyApiLib.Services.CurrencyRate.Interfaces;
using CurrencyApiLib.Services.CurrencyRate.Classes;
using Microsoft.EntityFrameworkCore;
using CurrencyApiLib.Services.Cache.Classes;
using CurrencyApiLib.Services.Cache.Interfaces;
using CurrencyApiInfrastructure.Repositories;
using CurrencyApiDomain.Repositories;
using CurrencyApiDomain.UnitOfWorks;
using CurrencyApiInfrastructure.MapperConfigurations;
using CurrencyApiLib.MapperConfigurations;

namespace CurrencyApi.Tests
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddApplicationPart(Assembly.Load("CurrencyApi")).AddControllersAsServices();

            //services.AddDbContext<AppDbContext>();
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseInMemoryDatabase("CurrencyDb-" + Guid.NewGuid());
            });
            services.AddScoped<ICurrencyRateService, CurrencyRateService>();
            services.AddScoped<ICacheService, CacheService>();

            services.AddScoped<ICurrencyRateRepo, CurrencyRateRepo>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICustomMapper, MapsterMapping>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
