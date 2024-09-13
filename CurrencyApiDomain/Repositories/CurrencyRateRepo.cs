using CurrencyApiDomain.ContextRelated;
using CurrencyApiDomain.DbBase.Repositories.Base;
using CurrencyApiInfrastructure.Entities;
using CurrencyApiInfrastructure.Repositories;

namespace CurrencyApiDomain.Repositories
{
    /// <summary>
    /// The currency rate repo.
    /// </summary>
    public class CurrencyRateRepo : BaseRepo<CurrencyRate, int>, ICurrencyRateRepo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CurrencyRateRepo"/> class.
        /// </summary>
        /// <param name="dbContext">The db context.</param>
        public CurrencyRateRepo(AppDbContext dbContext) : base(dbContext)
        {

        }
    }
}
