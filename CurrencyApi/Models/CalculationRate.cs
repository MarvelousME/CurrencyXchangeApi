using System;

namespace CurrencyApi.Models
{
    /// <summary>
    /// The calculation rate.
    /// </summary>
    public class CalculationRate
    {
        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        public double Amount { get; set; }
        /// <summary>
        /// Gets or sets the base.
        /// </summary>
        public double Base { get; set; }
        /// <summary>
        /// Gets or sets the target.
        /// </summary>
        public double Target { get; set; }
        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        public double Result { get; set; }
    }
}
