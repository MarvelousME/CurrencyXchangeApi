using CurrencyApiInfrastructure.Enums;

namespace CurrencyApiInfrastructure.BaseInterfaces.Pagination
{
    public interface ISortingDto
    {
        public string SortingPropertyName { get; set; }

        public SortingDirections SortingDirection { get; set; }
    }
}
