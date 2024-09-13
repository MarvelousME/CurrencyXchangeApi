using CurrencyApi.HttpClient;
using CurrencyApiLib.Dtos;
using CurrencyApiLib.Dtos.CurrencyRate;
using CurrencyApiLib.Services.Cache.Interfaces;
using CurrencyApiLib.Services.CurrencyRate.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Reflection;
using static MongoDB.Driver.WriteConcern;
using CurrencyApi.Helpers;
using CurrencyApi.Models;

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
        [HttpGet("/api/v1/GetExternalApiCurrencyRatesAsync/{currencyCode}")]
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

        /// <summary>
        /// Convert the currency asynchronously.
        /// </summary>
        /// <param name="fromCurrency">The from currency.</param>
        /// <param name="toCurrency">The to currency.</param>
        /// <param name="amount">The amount.</param>
        /// <returns><![CDATA[Task<ResultMessage>]]></returns>
        [HttpGet("/api/v1/ConvertCurrencyAsync/{fromCurrency}/{toCurrency}/{amount}")]
        public async Task<ResultMessage> ConvertCurrencyAsync(string fromCurrency, string toCurrency, string amount)
        {

            string message = string.Empty;
            string toRateValue = string.Empty;
            string fromRateValue = string.Empty;

            fromCurrency = fromCurrency.ToUpper();
            toCurrency = toCurrency.ToUpper();
            cacheKey = cacheKey + $"{fromCurrency}";
            double calculation = double.Parse("0.00");
        
            try
            {
                var inputCheck = InputHelper.CheckInputValue(InputType.Amount, amount);
                if(!inputCheck.Item1) return new ResultMessage(){ Message = inputCheck.Item2 };

                inputCheck = InputHelper.CheckInputValue(InputType.CurrencyCode, fromCurrency);
                if (!inputCheck.Item1) return new ResultMessage() { Message = inputCheck.Item2 };

                inputCheck = InputHelper.CheckInputValue(InputType.CurrencyCode, toCurrency);
                if (!inputCheck.Item1) return new ResultMessage() { Message = inputCheck.Item2 };



                #region Calculate from Cache
                var cacheData = _cacheService.GetData<CurrencyRates>(cacheKey);
                if (cacheData != null)
                {
                    //Do calculation from cache
                    toRateValue = PropertyHelper.FindPropertValueinClass(toCurrency, cacheData).ToString();

                    //Do calculation from cache results coming from the external api
                    toRateValue = PropertyHelper.FindPropertValueinClass(toCurrency, cacheData).ToString();
                    fromRateValue = PropertyHelper.FindPropertValueinClass(fromCurrency, cacheData).ToString();

                    var inputCache = new CalculationRate
                    {
                        Base = Convert.ToDouble(fromRateValue),
                        Target = Convert.ToDouble(toRateValue),
                        Amount = Convert.ToDouble(amount)
                    };

                    result = new ResultMessage
                    {
                        Message = $"Amount: {amount} converted from {fromCurrency} to {toCurrency} = {calculation.ToString()}",
                    };

                    return await Task.FromResult(result);
                }
                #endregion

                #region Calculate from External Api
                data = WebClient.Request(externalAPI.Url, externalAPI.ApiKey, fromCurrency);

                var url = string.Format(externalAPI.Url, externalAPI.ApiKey, fromCurrency);
                if (data.conversion_rates == null)
                {
                    return result = new ResultMessage
                    {
                        Message = $"Request to the external api at {url} returned null, Please try again later.",
                        data = data.conversion_rates
                    };
                }
               
                //Do calculation from live results coming from the external api
                toRateValue = PropertyHelper.FindPropertValueinClass(toCurrency, data.conversion_rates).ToString();
                fromRateValue = PropertyHelper.FindPropertValueinClass(fromCurrency, data.conversion_rates).ToString();

                var input = new CalculationRate
                {
                    Base = Convert.ToDouble(fromRateValue),
                    Target = Convert.ToDouble(toRateValue),
                    Amount = Convert.ToDouble(amount)
                };

                input = ConversionHelper.Calculate(input);

                _logger.LogInformation($"Amount: {input.Amount} converted from {fromCurrency} to {toCurrency} = {input.Result.ToString()}");
                result = new ResultMessage
                {
                    Message = $"Amount: {input.Amount} converted from {fromCurrency} to {toCurrency} = {input.Result.ToString()}",
                };
                #endregion

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
    }
}
