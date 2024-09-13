using CurrencyApiLib.Dtos.CurrencyRate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyApiLib.Dtos
{
    public class ResultMessage
    {
        public string Message { get; set; } = string.Empty;
        public CurrencyRates data { get; set; } = null;
        public List<CurrencyRates> list { get; set; } = null;
    }
}
