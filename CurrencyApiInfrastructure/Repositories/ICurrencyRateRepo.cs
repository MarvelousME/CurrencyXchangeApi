using CurrencyApiInfrastructure.DbBase;
using CurrencyApiInfrastructure.Entities;

namespace CurrencyApiInfrastructure.Repositories
{
    public interface ICurrencyRateRepo : IBaseRepo<CurrencyRate, int>
    {
    }
}
