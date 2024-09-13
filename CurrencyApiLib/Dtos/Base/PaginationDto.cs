using CurrencyApiInfrastructure.BaseInterfaces.Pagination;

namespace CurrencyApiLib.Dtos.Base
{
    /// <summary>
    /// The pagination data transfer object.
    /// </summary>
    public abstract class PaginationDto : IPaginationDto
    {
        /// <summary>
        /// Gets or sets the skip.
        /// </summary>
        public int Skip { get; set; }

        /// <summary>
        /// Gets or sets the take.
        /// </summary>
        public int Take { get; set; }
    }
}
