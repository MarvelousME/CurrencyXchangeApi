using CurrencyApiInfrastructure.BaseInterfaces.Pagination;
using CurrencyApiInfrastructure.Enums;

namespace CurrencyApiInfrastructure.DtoBase
{
    /// <summary>
    /// The sorting data transfer object.
    /// </summary>
    public class SortingDto : ISortingDto
    {
        /// <summary>
        /// Gets or sets the sorting property name.
        /// </summary>
        public string SortingPropertyName { get; set; }

        /// <summary>
        /// Gets or sets the sorting direction.
        /// </summary>
        public SortingDirections SortingDirection { get; set; }
    }
}
