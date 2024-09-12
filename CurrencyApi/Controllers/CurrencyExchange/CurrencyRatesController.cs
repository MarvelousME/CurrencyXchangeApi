using CurrencyApiLib.Dtos.CurrencyRate;
using CurrencyApiLib.Services.CurrencyRate.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CurrencyApiLib.Controllers.Book
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class CurrencyRatesController : ControllerBase
    {
        const string apiKey = "93ae23f11c432de09b438c96";
        private readonly ICurrencyRateService _currencyRateService;
        private ExternalAPI externalAPI;
        private readonly IConfiguration Configuration;
        public CurrencyRatesController(ICurrencyRateService currencyRateService, IConfiguration configuration)
        {
            _currencyRateService = currencyRateService;
            Configuration = configuration;
            Configuration.GetSection("ExternalApi").Bind(externalAPI);
        }

        [HttpGet]
        public Task<CurrencyRates> GetExternalCurrencyRatesAsync()
        {
            CurrencyRateDto data = new CurrencyRateDto();
            try
            {
                //Would of used RestSharp client
                //string URLString = string.Format(externalAPI.url, externalAPI.apiKey);
                string URLString = $"https://v6.exchangerate-api.com/v6/{apiKey}/latest/USD";
                using (var webClient = new System.Net.WebClient())
                {
                    var json = webClient.DownloadString(URLString);
                    data = JsonConvert.DeserializeObject<CurrencyRateDto>(json);

                    //Save to MySQL table here
                    //await _currencyRateService.CurrencyRateSaveAsync(data);

                    return Task.FromResult(data.conversion_rates);
                }
            }
            catch (Exception ex)
            {
                return Task.FromResult(data.conversion_rates);
            }

        }

    }

    public class ExternalAPI
    {
        public string url { get; set; }
        public string apiKey { get; set; }
    }
}
