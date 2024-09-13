using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CurrencyApiLib.Dtos.CurrencyRate
{
    /// <summary>
    /// The delete currency rate data transfer object.
    /// </summary>
    public class DeleteCurrencyRateDto
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
