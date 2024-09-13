using CurrencyApiDomain.UnitOfWorks;
using CurrencyApiInfrastructure.MapperConfigurations;
using CurrencyApiInfrastructure.Repositories;
using CurrencyApiLib.Dtos.CurrencyRate;
using CurrencyApiLib.Services.CurrencyRate.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyApiLib.Services.CurrencyRate.Classes
{
    /// <summary>
    /// The currency rate service.
    /// </summary>
    public class CurrencyRateService : ICurrencyRateService
    {
        /// <summary>
        /// The currency rates repo.
        /// </summary>
        private readonly ICurrencyRateRepo _currencyRatesRepo;
        /// <summary>
        /// The unit of work.
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;
        /// <summary>
        /// The mapper.
        /// </summary>
        private readonly ICustomMapper _mapper;
        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CurrencyRateService"/> class.
        /// </summary>
        /// <param name="currencyRatesRepo">The currency rates repo.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="customMapper">The custom mapper.</param>
        /// <param name="logger">The logger.</param>
        public CurrencyRateService(ICurrencyRateRepo currencyRatesRepo, IUnitOfWork unitOfWork, ICustomMapper customMapper, ILogger<CurrencyRateService> logger)
        {
            _currencyRatesRepo = currencyRatesRepo;
            _unitOfWork = unitOfWork;
            _mapper = customMapper;
            _logger = logger;
        }

        /// <summary>
        /// Get all currency rates from db asynchronously.
        /// </summary>
        /// <returns><![CDATA[Task<List<CurrencyRates>>]]></returns>
        public async Task<List<CurrencyRates>> GetAllCurrencyRatesFromDbAsync()
        {
            string message = string.Empty;
            try
            {
                var data = _currencyRatesRepo.GetData().ToList();
                var list = _mapper.Map<List<CurrencyApiInfrastructure.Entities.CurrencyRate>, List<CurrencyRates>>(data);

                message = "Successfully retrieved records from database";
                _logger.LogInformation(message);

                return await Task.FromResult(list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving records to the database");
            }

            message = "Error retrieving records to the database";
            _logger.LogError(message);

            return null;
        }

        public async Task<List<CurrencyRates>> GetAllCurrencyRatesFromDbAsync(string currencyCode)
        {
            string message = string.Empty;
            try
            {
                var data = _currencyRatesRepo.GetDataWithLinqExp(r => r.CurrencyCode.ToUpper() == currencyCode.ToUpper()).ToList();
                var list = _mapper.Map<List<CurrencyApiInfrastructure.Entities.CurrencyRate>, List<CurrencyRates>>(data);

                message = $"Successfully retrieved records from database for Currency Code: {currencyCode.ToUpper()}";
                _logger.LogInformation(message);

                return await Task.FromResult(list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving records to the database for Currency Code: {currencyCode.ToUpper()}");
            }

            message = "Error retrieving records to the database";
            _logger.LogError(message);

            return null;
        }

        public async Task<CurrencyRates> GetLatestCurrencyRatesFromDbAsync(string currencyCode)
        {
            string message = string.Empty;
            try
            {
                var data = _currencyRatesRepo.GetDataWithLinqExp(r => r.CurrencyCode.ToUpper() == currencyCode.ToUpper()).OrderByDescending(o => o.Id).First();
                var record = _mapper.Map<CurrencyApiInfrastructure.Entities.CurrencyRate, CurrencyRates>(data);

                message = $"Successfully retrieved latest record from database for Currency Code: {currencyCode.ToUpper()}";
                _logger.LogInformation(message);

                return await Task.FromResult(record);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving records to the database for Currency Code: {currencyCode.ToUpper()}");
            }

            message = "Error retrieving records to the database";
            _logger.LogError(message);

            return null;
        }

        public async Task<double> ConvertCurrencyRateAmountAsync(string currencyCode, string toCurrencyCode, double amount)
        {
            //string message = string.Empty;
            //try
            //{
            //    var data = _currencyRatesRepo.GetDataWithLinqExp(r => r.CurrencyCode.ToUpper() == currencyCode.ToUpper()).OrderByDescending(o => o.Id).AsQueryable().First();

            //    var ratio = data..Equals(toCurrencyCode);

            //    var calculate = amount;

            //    message = $"Successfully retrieved records from database for Currency Code: {currencyCode.ToUpper()}";
            //    _logger.LogInformation(message);

            //    return await Task.FromResult(list);
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError(ex, $"Error retrieving records to the database for Currency Code: {currencyCode.ToUpper()}");
            //}

            //message = "Error retrieving records to the database";
            //_logger.LogError(message);

            return Convert.ToDouble("0.00");
        }

        /// <summary>
        /// Currencies rates save converts to db asynchronously.
        /// </summary>
        /// <param name="dto">The data transfer object.</param>
        /// <returns><![CDATA[Task<string>]]></returns>
        public async Task<string> CurrencyRatesSaveToDbAsync(CurrencyRates dto,string currencyCode)
        {
            string message = string.Empty;
            try
            {
                var row = _mapper.Map<CurrencyRates, CurrencyApiInfrastructure.Entities.CurrencyRate>(dto);
                row.CurrencyCode = currencyCode;

                await _currencyRatesRepo.InsertAsync(row);
                _currencyRatesRepo.SaveChanges();

                message = $"Saved to database successfully for Currency Code: {currencyCode}";
                return await Task.FromResult(message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error saving record to the database for Currency Code: {currencyCode}");
            }

            message = "Failed to save record to database.";
            return await Task.FromResult(message);
        }

        /// <summary>
        /// Deletes currency rates asynchronously.
        /// </summary>
        /// <param name="dto">The data transfer object.</param>
        /// <returns>A Task</returns>
        public Task DeleteCurrencyRatesAsync(DeleteCurrencyRateDto dto)
        {
            return Task.FromResult(new DeleteCurrencyRateDto());
        }

        /// <summary>
        /// Does entity exist asynchronously.
        /// </summary>
        /// <param name="Id">The id.</param>
        /// <returns><![CDATA[Task<bool>]]></returns>
        public Task<bool> DoesEntityExistAsync(IConvertible Id)
        {
            return Task.FromResult(false);
        }

        /// <summary>
        /// Update currency rates info asynchronously.
        /// </summary>
        /// <param name="dto">The data transfer object.</param>
        /// <returns>A Task</returns>
        public Task UpdateCurrencyRatesInfoAsync(UpdateCurrencyRateDto dto)
        {
            return Task.FromResult(new UpdateCurrencyRateDto());
        }

    }
}
