using System;
using CurrencyApi.Models;

namespace CurrencyApi.Helpers
{
    /// <summary>
    /// The conversion helper.
    /// </summary>
    public class ConversionHelper
    {
        public static CalculationRate Calculate(CalculationRate calculation)
        {
            calculation.Result = calculation.Amount / calculation.Base * calculation.Target;
            return calculation;
        }
    }
}
