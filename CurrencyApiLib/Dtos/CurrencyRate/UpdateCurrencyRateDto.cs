using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CurrencyApiLib.Dtos.CurrencyRate
{
    /// <summary>
    /// The update currency rate data transfer object.
    /// </summary>
    public class UpdateCurrencyRateDto : CreateCurrencyRateDto
    {
        //this annotations are required for GenericNotFoundFilter
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [Key]
        [DisplayName("CurrencyRate")]
        public int Id { get; set; }
    }
}
