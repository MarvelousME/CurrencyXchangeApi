using CurrencyApiInfrastructure.BaseInterfaces.Pagination;
using CurrencyApiInfrastructure.DtoBase;
using System.Collections.Generic;

namespace CurrencyApiLib.Dtos.Base
{
    /// <summary>
    /// The paginate and sorting data transfer object.
    /// </summary>
    public class PaginateAndSortingDto : IPaginationDto
    {
        /// <summary>
        /// Gets or sets the skip.
        /// </summary>
        public int Skip { get; set; }

        /// <summary>
        /// Gets or sets the take.
        /// </summary>
        public int Take { get; set; }

        /// <summary>
        /// Gets or sets the sorting model.
        /// </summary>
        public List<SortingDto> SortingModel { get; set; }
    }
}
