using System.Threading.Tasks;

namespace CurrencyApiDomain.UnitOfWorks
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
        void Commit();
    }
}
