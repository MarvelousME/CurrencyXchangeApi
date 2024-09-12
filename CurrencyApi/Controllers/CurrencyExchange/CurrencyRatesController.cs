using CurrencyApi.HttpClient;
using CurrencyApiLib.Dtos.CurrencyRate;
using CurrencyApiLib.Services.CurrencyRate.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyApiLib.Controllers.CurrencyExchange
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class CurrencyRatesController : ControllerBase
    {
        private readonly ICurrencyRateService _currencyRateService;
        private ExternalAPI externalAPI;
        private readonly IConfiguration Configuration;
        private readonly ILogger _logger;
        private CurrencyRateDto data = new CurrencyRateDto();
        public CurrencyRatesController(ICurrencyRateService currencyRateService, IConfiguration configuration, ILogger logger)
        {
            _currencyRateService = currencyRateService;
            _logger = logger;

            externalAPI = new ExternalAPI();
            Configuration = configuration;
            Configuration.GetSection("ExternalAPI").Bind(externalAPI);
        }

        [HttpGet]
        public Task<CurrencyRates> GetExternalCurrencyRatesAsync()
        {
            try
            {
                data = WebClient.Request(externalAPI.Url, externalAPI.ApiKey);

                var url = string.Format(externalAPI.Url, externalAPI.ApiKey);

                _logger.LogInformation($"The GetExternalCurrencyRatesAsync request executed successfully on url: {url}");

                return Task.FromResult(data.conversion_rates);
            }
            catch (Exception ex)
            {
                //log error
                _logger.LogError(ex, "The GetExternalCurrencyRatesAsync request failed");
            }

            return null;
        }

    }

    public class ExternalAPI
    {
        public const string Name = "ExternalAPI";
        public string Url { get; set; }
        public string ApiKey { get; set; }
    }
}
