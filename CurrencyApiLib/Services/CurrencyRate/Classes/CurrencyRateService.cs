using CurrencyApiDomain.UnitOfWorks;
using CurrencyApiInfrastructure.MapperConfigurations;
using CurrencyApiInfrastructure.Repositories;
using CurrencyApiLib.Dtos.CurrencyRate;
using CurrencyApiLib.Services.CurrencyRate.Interfaces;
using System;
using System.Threading.Tasks;

namespace CurrencyApiLib.Services.CurrencyRate.Classes
{
    public class CurrencyRateService : ICurrencyRateService
    {
        private readonly ICurrencyRateRepo _currencyRatesRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomMapper _mapper;

        public CurrencyRateService(ICurrencyRateRepo currencyRatesRepo, IUnitOfWork unitOfWork, ICustomMapper customMapper)
        {
            _currencyRatesRepo = currencyRatesRepo;
            _unitOfWork = unitOfWork;
            _mapper = customMapper;
        }

        public Task<int> CurrencyRateSaveAsync(CurrencyRateDto dto)
        {
            //Save to db here
            //_currencyRatesRepo.InsertAsync(dto);
            //_currencyRatesRepo.SaveChanges();
            return Task.FromResult(1);
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
