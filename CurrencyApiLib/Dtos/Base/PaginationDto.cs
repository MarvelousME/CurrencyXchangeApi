using CurrencyApiInfrastructure.BaseInterfaces.Pagination;

namespace CurrencyApiLib.Dtos.Base
{
    public abstract class PaginationDto : IPaginationDto
    {
        public int Skip { get; set; }

        public int Take { get; set; }
    }
}
