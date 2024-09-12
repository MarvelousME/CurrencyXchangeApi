using CurrencyApiInfrastructure.BaseInterfaces.Pagination;
using CurrencyApiInfrastructure.Enums;

namespace CurrencyApiInfrastructure.DtoBase
{
    public class SortingDto : ISortingDto
    {
        public string SortingPropertyName { get; set; }

        public SortingDirections SortingDirection { get; set; }
    }
}
