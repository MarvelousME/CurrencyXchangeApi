using CurrencyApiDomain.UnitOfWorks;
using CurrencyApiInfrastructure.MapperConfigurations;
using CurrencyApiInfrastructure.Repositories;
using CurrencyApiInfrastructure.Entities;
using CurrencyApiLib.Dtos.CurrencyRate;
using CurrencyApiLib.Services.CurrencyRate.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace CurrencyApiLib.Services.CurrencyRate.Classes
{
    public class CurrencyRateService : ICurrencyRateService
    {
        private readonly ICurrencyRateRepo _currencyRatesRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomMapper _mapper;
        private readonly ILogger _logger;

        public CurrencyRateService(ICurrencyRateRepo currencyRatesRepo, IUnitOfWork unitOfWork, ICustomMapper customMapper, ILogger<CurrencyRateService> logger)
        {
            _currencyRatesRepo = currencyRatesRepo;
            _unitOfWork = unitOfWork;
            _mapper = customMapper;
            _logger = logger;
        }

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

        public async Task<string> CurrencyRatesSaveToDbAsync(CurrencyRates dto)
        {
            string message = string.Empty;
            try
            {
                var row = _mapper.Map<CurrencyRates, CurrencyApiInfrastructure.Entities.CurrencyRate>(dto);

                await _currencyRatesRepo.InsertAsync(row);
                _currencyRatesRepo.SaveChanges();

                message = "Saved to database successfully";
                return await Task.FromResult(message);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error saving record to the database");
            }

            message = "Failed to save record to database.";
            return await Task.FromResult(message);
        }

        public Task DeleteCurrencyRatesAsync(DeleteCurrencyRateDto dto)
        {
            return Task.FromResult(new DeleteCurrencyRateDto());
        }

        public Task<bool> DoesEntityExistAsync(IConvertible Id)
        {
            return Task.FromResult(false);
        }

        public Task UpdateCurrencyRatesInfoAsync(UpdateCurrencyRateDto dto)
        {
            return Task.FromResult(new UpdateCurrencyRateDto());
        }
    }
}
