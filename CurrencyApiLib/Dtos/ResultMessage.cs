using CurrencyApiLib.Dtos.CurrencyRate;
using System.Collections.Generic;

namespace CurrencyApiLib.Dtos
{
    /// <summary>
    /// The result message.
    /// </summary>
    public class ResultMessage
    {
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        public string Message { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        public CurrencyRates data { get; set; } = null;
        /// <summary>
        /// Gets or sets the list.
        /// </summary>
        public List<CurrencyRates> list { get; set; } = null;
    }
}
