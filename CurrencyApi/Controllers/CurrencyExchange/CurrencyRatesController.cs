using CurrencyApi.HttpClient;
using CurrencyApiInfrastructure.Entities;
using CurrencyApiLib.Dtos.CurrencyRate;
using CurrencyApiLib.Services.CurrencyRate.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyApiLib.Controllers.CurrencyExchange
{

    /// <summary>
    /// The currency rates controller.
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class CurrencyRatesController : ControllerBase
    {
        /// <summary>
        /// The currency rate service.
        /// </summary>
        private readonly ICurrencyRateService _currencyRateService;
        /// <summary>
        /// The external API.
        /// </summary>
        private ExternalAPI externalAPI;
        /// <summary>
        /// The configuration.
        /// </summary>
        private readonly IConfiguration Configuration;
        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger _logger;
        /// <summary>
        /// The data.
        /// </summary>
        private CurrencyRateDto data = new CurrencyRateDto();
        /// <summary>
        /// Initializes a new instance of the <see cref="CurrencyRatesController"/> class.
        /// </summary>
        /// <param name="currencyRateService">The currency rate service.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="logger">The logger.</param>
        public CurrencyRatesController(ICurrencyRateService currencyRateService, IConfiguration configuration, ILogger<CurrencyRatesController> logger)
        {
            _currencyRateService = currencyRateService;
            _logger = logger;

            externalAPI = new ExternalAPI();
            Configuration = configuration;
            Configuration.GetSection("ExternalAPI").Bind(externalAPI);
        }

        /// <summary>
        /// Get external api currency rates asynchronously.
        /// </summary>
        /// <returns><![CDATA[Task<CurrencyRates>]]></returns>
        [HttpGet]
        public async Task<CurrencyRates> GetExternalApiCurrencyRatesAsync()
        {
            try
            {
                data = WebClient.Request(externalAPI.Url, externalAPI.ApiKey);

                var url = string.Format(externalAPI.Url, externalAPI.ApiKey);

                _logger.LogInformation($"The GetExternalApiCurrencyRatesAsync request executed successfully on url: {url}");

                return await Task.FromResult(data.conversion_rates);
            }
            catch (Exception ex)
            {
                //log error
                _logger.LogError(ex, "The GetExternalApiCurrencyRatesAsync request failed");
            }

            return null;
        }

        /// <summary>
        /// Get external currency rates asynchronously.
        /// </summary>
        /// <returns><![CDATA[Task<CurrencyRates>]]></returns>
        [HttpGet]
        public async Task<string> GetExternalApiCurrencyRatesAndSaveToDbAsync()
        {
            string message = string.Empty;
            try
            {
                data = WebClient.Request(externalAPI.Url, externalAPI.ApiKey);

                var url = string.Format(externalAPI.Url, externalAPI.ApiKey);

                _logger.LogInformation($"The GetExternalApiCurrencyRatesAndSavedToDbAsync request executed successfully on url: {url}");

                message = await _currencyRateService.CurrencyRatesSaveToDbAsync(data.conversion_rates);

                _logger.LogInformation(message);

                return await Task.FromResult(message);
            }
            catch (Exception ex)
            {
                //log error
                _logger.LogError(ex, "The GetExternalApiCurrencyRatesAndSavedToDbAsync method failed.");
            }

            return await Task.FromResult(message = "failed to save record to the database.");
        }

        [HttpGet]
        public async Task<List<CurrencyRates>> GetAllCurrencyRatesFromDbAsync()
        {
            try
            {
                var list = await _currencyRateService.GetAllCurrencyRatesFromDbAsync();
                return await Task.FromResult(list);
            }
            catch (Exception ex)
            {
                //log error
                _logger.LogError(ex, "The GetAllCurrencyRatesFromDbAsync request failed");
            }

            return null;
        }
    }

    /// <summary>
    /// The external API.
    /// </summary>
    public class ExternalAPI
    {
        /// <summary>
        /// Name.
        /// </summary>
        public const string Name = "ExternalAPI";
        /// <summary>
        /// Gets or sets the url.
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// Gets or sets the api key.
        /// </summary>
        public string ApiKey { get; set; }
    }
}
