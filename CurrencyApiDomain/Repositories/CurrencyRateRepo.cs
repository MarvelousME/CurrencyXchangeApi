using CurrencyApiDomain.ContextRelated;
using CurrencyApiDomain.DbBase.Repositories.Base;
using CurrencyApiInfrastructure.Entities;
using CurrencyApiInfrastructure.Repositories;

namespace CurrencyApiDomain.Repositories
{
    public class CurrencyRateRepo : BaseRepo<CurrencyRate, int>, ICurrencyRateRepo
    {
        public CurrencyRateRepo(AppDbContext dbContext) : base(dbContext)
        {

        }
    }
}
