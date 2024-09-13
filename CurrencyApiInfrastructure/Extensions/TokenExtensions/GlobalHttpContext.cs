using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace CurrencyApiInfrastructure.Extensions.TokenExtensions
{

    /// <summary>
    /// The global http context.
    /// </summary>
    public static class GlobalHttpContext
    {
        /// <summary>
        /// The context accessor.
        /// </summary>
        public static IHttpContextAccessor _contextAccessor;

        internal static void Configure(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
    }

    /// <summary>
    /// The static http context extensions.
    /// </summary>
    public static class StaticHttpContextExtensions
    {
        public static IApplicationBuilder UseStaticHttpContext(this IApplicationBuilder app)
        {
            var httpContextAccessor = app.ApplicationServices.GetRequiredService<IHttpContextAccessor>();
            GlobalHttpContext.Configure(httpContextAccessor);
            return app;
        }
    }

}
