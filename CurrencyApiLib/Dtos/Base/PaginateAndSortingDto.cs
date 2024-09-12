using CurrencyApiInfrastructure.BaseInterfaces.Pagination;
using CurrencyApiInfrastructure.DtoBase;
using System.Collections.Generic;

namespace CurrencyApiLib.Dtos.Base
{
    public class PaginateAndSortingDto : IPaginationDto
    {
        public int Skip { get; set; }

        public int Take { get; set; }

        public List<SortingDto> SortingModel { get; set; }
    }
}
