using CurrencyApiInfrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace CurrencyApiDomain.ContextRelated
{
    /// <summary>
    /// The app db context.
    /// </summary>
    public partial class AppDbContext : DbContext
    {
        /// <summary>
        /// Gets or sets the currency rates.
        /// </summary>
        public DbSet<CurrencyRate> CurrencyRates { get; set; }
    }
}
