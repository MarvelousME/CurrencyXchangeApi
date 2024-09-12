using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CurrencyApiLib.Dtos.CurrencyRate
{
    public class DeleteCurrencyRateDto
    {
        //this annotations are required for GenericNotFoundFilter
        [Key]
        [DisplayName("CurrencyRate")]
        public int Id { get; set; }
    }
}
