using System;
using System.Threading.Tasks;

namespace CurrencyApiInfrastructure.BaseInterfaces
{
    public interface IBaseCheckService
    {
        Task<bool> DoesEntityExistAsync(IConvertible Id);
    }
}
