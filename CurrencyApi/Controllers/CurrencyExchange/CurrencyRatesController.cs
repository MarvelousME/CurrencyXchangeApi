using CurrencyApi.HttpClient;
using CurrencyApiLib.Dtos;
using CurrencyApiLib.Dtos.CurrencyRate;
using CurrencyApiLib.Services.Cache.Interfaces;
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
        /// Initializes a new instance of the <see cref="CurrencyRatesController"/> class.
        /// </summary>
        /// <param name="currencyRateService">The currency rate service.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="logger">The logger.</param>

        private string cacheKey = "currency-rates_";

        /// <summary>
        /// The cache service
        /// </summary>
        private readonly ICacheService _cacheService;
        /// <summary>
        /// The currency rate service.
        /// </summary>
        private readonly ICurrencyRateService _currencyRateService;
        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger _logger;
        /// <summary>
        /// The configuration.
        /// </summary>
        private readonly IConfiguration Configuration;
        /// <summary>
        /// The data.
        /// </summary>
        private CurrencyRateDto data = new CurrencyRateDto();
        /// <summary>
        /// The external API.
        /// </summary>
        private ExternalAPI externalAPI;
        /// <summary>
        /// The result.
        /// </summary>
        private ResultMessage result;
        /// <summary>
        /// Initializes a new instance of the <see cref="CurrencyRatesController"/> class.
        /// </summary>
        /// <param name="currencyRateService">The currency rate service.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="cacheService">The cache service.</param>
        /// <param name="logger">The logger.</param>
        public CurrencyRatesController(
            ICurrencyRateService currencyRateService,
            IConfiguration configuration,
            ICacheService cacheService,
            ILogger<CurrencyRatesController> logger)
        {
            _currencyRateService = currencyRateService;
            _cacheService = cacheService;
            _logger = logger;

            externalAPI = new ExternalAPI();
            Configuration = configuration;
            Configuration.GetSection("ExternalAPI").Bind(externalAPI);
        }

        /// <summary>
        /// Get all currency rates from db asynchronously.
        /// </summary>
        /// <returns><![CDATA[Task<ResultMessage>]]></returns>
        [HttpGet("/api/v1/GetAllCurrencyRatesFromDbAsync/")]
        public async Task<ResultMessage> GetAllCurrencyRatesFromDbAsync()
        {
            try
            {
                var listData = await _currencyRateService.GetAllCurrencyRatesFromDbAsync();
                if(listData == null)
                {
                    result = new ResultMessage
                    {
                        Message = $"No rows returned fom the database"
                    };
                    return await Task.FromResult(result);
                }


                result = new ResultMessage
                {
                    list = listData,
                    Message = $"{listData.Count()} rows returned fom the database"
                };
                return await Task.FromResult(result);

            }
            catch (Exception ex)
            {
                //log error
                _logger.LogError(ex, "The GetAllCurrencyRatesFromDbAsync request failed");
            }

            result = new ResultMessage
            {
                Message = $"No rows returned fom the database"
            };
            return null;
        }

        /// <summary>
        /// Get all currency rates from db asynchronously.
        /// </summary>
        /// <param name="currencyCode">The currency code.</param>
        /// <returns><![CDATA[Task<ResultMessage>]]></returns>
        [HttpGet("/api/v1/GetAllCurrencyRatesFromDbAsync/{currencyCode}")]
        public async Task<ResultMessage> GetAllCurrencyRatesFromDbAsync(string currencyCode)
        {
            currencyCode = currencyCode.ToUpper();
            try
            {
                var listData = await _currencyRateService.GetAllCurrencyRatesFromDbAsync(currencyCode);
                if (listData == null)
                {
                    result = new ResultMessage
                    {
                        Message = $"No rows returned fom the database"
                    };
                    return await Task.FromResult(result);
                }

                result = new ResultMessage
                {
                    list = listData,
                    Message = $"{listData.Count()} rows returned fom the database"
                };
                return await Task.FromResult(result);

            }
            catch (Exception ex)
            {
                //log error
                _logger.LogError(ex, "The GetAllCurrencyRatesFromDbAsync request failed");
            }

            result = new ResultMessage
            {
                Message = $"No rows returned fom the database"
            };
            return null;
        }

        /// <summary>
        /// Get latest currency rates from db asynchronously.
        /// </summary>
        /// <param name="currencyCode">The currency code.</param>
        /// <returns><![CDATA[Task<ResultMessage>]]></returns>
        [HttpGet("/api/v1/GetLatestCurrencyRatesFromDbAsync/{currencyCode}")]
        public async Task<ResultMessage> GetLatestCurrencyRatesFromDbAsync(string currencyCode)
        {
            currencyCode = currencyCode.ToUpper();
            try
            {
                var data = await _currencyRateService.GetLatestCurrencyRatesFromDbAsync(currencyCode);
                if (data == null)
                {
                    result = new ResultMessage
                    {
                        Message = $"No rows returned fom the database"
                    };
                    return await Task.FromResult(result);
                }

                result = new ResultMessage
                {
                    data = data,
                    Message = $"Last inserted record for Currency Code, { currencyCode } - from the database"
                };
                return await Task.FromResult(result);

            }
            catch (Exception ex)
            {
                //log error
                _logger.LogError(ex, "The GetAllCurrencyRatesFromDbAsync request failed");
            }

            result = new ResultMessage
            {
                Message = $"No rows returned fom the database"
            };
            return null;
        }
        /// <summary>
        /// Get external currency rates asynchronously.
        /// </summary>
        /// <returns></returns>
        [HttpGet("/api/v1/GetExternalApiCurrencyRatesAndSaveToDatabase/{currencyCode}")]
        public async Task<ResultMessage> GetExternalApiCurrencyRatesAndSaveToDbAsync(string currencyCode)
        {
            result = new ResultMessage();
            string message = string.Empty;
            currencyCode = currencyCode.ToUpper();
            cacheKey = cacheKey + $"{currencyCode}";

            try
            {
                var cacheData = _cacheService.GetData<CurrencyRates>(cacheKey);
                if (cacheData != null)
                {
                    result = new ResultMessage
                    {
                        Message = "loaded data from cache",
                        data = cacheData
                    };

                    return await Task.FromResult(result);
                }

                data = WebClient.Request(externalAPI.Url, externalAPI.ApiKey, currencyCode);

                var url = string.Format(externalAPI.Url, externalAPI.ApiKey, currencyCode);
                if (data.conversion_rates == null)
                {
                    return result = new ResultMessage
                    {
                        Message = $"Request to the external api at {url} returned null, Please try again later.",
                        data = data.conversion_rates
                    };
                }

                _logger.LogInformation($"The GetExternalApiCurrencyRatesAndSaveToDbAsync request executed successfully on url: {url}");

                message = await _currencyRateService.CurrencyRatesSaveToDbAsync(data.conversion_rates, currencyCode);

                _logger.LogInformation(message);

                //Set expiry of cache to 15 mins
                var expirationTime = DateTimeOffset.Now.AddMinutes(15.0);
                _cacheService.SetData<CurrencyRates>(cacheKey, data.conversion_rates, expirationTime);

                result = new ResultMessage
                {
                    Message = message,
                    data = data.conversion_rates
                };
                return await Task.FromResult((result));

            }
            catch (Exception ex)
            {
                //log error
                _logger.LogError(ex, "The GetExternalApiCurrencyRatesAndSavedToDbAsync method failed.");
            }

            result = new ResultMessage
            {
                Message = "failed to save record to the database.",
                data = null
            };
            return await Task.FromResult(result);
        }

        /// <summary>
        /// Get external api currency rates asynchronously.
        /// </summary>
        /// <returns></returns>
        [HttpGet("/api/v1/GetExternalApiCurrencyRatesAsync/")]
        public async Task<CurrencyRates> GetExternalApiCurrencyRatesAsync(string currencyCode)
        {
            currencyCode = currencyCode.ToUpper();
            try
            {
                data = WebClient.Request(externalAPI.Url, externalAPI.ApiKey, currencyCode);

                var url = string.Format(externalAPI.Url, externalAPI.ApiKey, currencyCode);

                _logger.LogInformation(
                    $"The GetExternalApiCurrencyRatesAsync request executed successfully on url: {url}");

                return await Task.FromResult(data.conversion_rates);
            }
            catch (Exception ex)
            {
                //log error
                _logger.LogError(ex, "The GetExternalApiCurrencyRatesAsync request failed");
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
        /// Gets or sets the api key.
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        /// Gets or sets the url.
        /// </summary>
        public string Url { get; set; }
    }
}
