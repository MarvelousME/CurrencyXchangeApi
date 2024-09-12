using CurrencyApiInfrastructure.BaseInterfaces;
using CurrencyApiLib.Dtos.CurrencyRate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CurrencyApiLib.Services.CurrencyRate.Interfaces
{
    public interface ICurrencyRateService : IBaseCheckService
    {
        /// <summary>
        /// Save new CurrencyRates
        /// </summary>
        /// <param name="dto">CurrencyRates</param>
        /// <returns>CurrencyRate id</returns>
        Task<string> CurrencyRatesSaveToDbAsync(CurrencyRates dto);

        /// <summary>
        /// Update CurrencyRate's information of existing CurrencyRate
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task UpdateCurrencyRatesInfoAsync(UpdateCurrencyRateDto dto);

        /// <summary>
        /// Soft delete for CurrencyRate records
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task DeleteCurrencyRatesAsync(DeleteCurrencyRateDto dto);

        Task<List<CurrencyRates>> GetAllCurrencyRatesFromDbAsync();

    }
}
