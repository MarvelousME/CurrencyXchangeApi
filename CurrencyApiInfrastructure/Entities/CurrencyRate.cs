using CurrencyApiInfrastructure.DbBase;
using System;
using System.ComponentModel.DataAnnotations;

namespace CurrencyApiInfrastructure.Entities
{
    public class CurrencyRate : TableBase
    {
        [Key]
        public int? Id { get; set; }
        public DateTime GetRequestDate { get; set; } = DateTime.UtcNow;
        public double AED { get; set; } = default!;
        public double ARS { get; set; } = default!;
        public double AUD { get; set; } = default!;
        public double BGN { get; set; } = default!;
        public double BRL { get; set; } = default!;
        public double BSD { get; set; } = default!;
        public double CAD { get; set; } = default!;
        public double CHF { get; set; } = default!;
        public double CLP { get; set; } = default!;
        public double CNY { get; set; } = default!;
        public double COP { get; set; } = default!;
        public double CZK { get; set; } = default!;
        public double DKK { get; set; } = default!;
        public double DOP { get; set; } = default!;
        public double EGP { get; set; } = default!;
        public double EUR { get; set; } = default!;
        public double FJD { get; set; } = default!;
        public double GBP { get; set; } = default!;
        public double GTQ { get; set; } = default!;
        public double HKD { get; set; } = default!;
        public double HRK { get; set; } = default!;
        public double HUF { get; set; } = default!;
        public double IDR { get; set; } = default!;
        public double ILS { get; set; } = default!;
        public double INR { get; set; } = default!;
        public double ISK { get; set; } = default!;
        public double JPY { get; set; } = default!;
        public double KRW { get; set; } = default!;
        public double KZT { get; set; } = default!;
        public double MXN { get; set; } = default!;
        public double MYR { get; set; } = default!;
        public double NOK { get; set; } = default!;
        public double NZD { get; set; } = default!;
        public double PAB { get; set; } = default!;
        public double PEN { get; set; } = default!;
        public double PHP { get; set; } = default!;
        public double PKR { get; set; } = default!;
        public double PLN { get; set; } = default!;
        public double PYG { get; set; } = default!;
        public double RON { get; set; } = default!;
        public double RUB { get; set; } = default!;
        public double SAR { get; set; } = default!;
        public double SEK { get; set; } = default!;
        public double SGD { get; set; } = default!;
        public double THB { get; set; } = default!;
        public double TRY { get; set; } = default!;
        public double TWD { get; set; } = default!;
        public double UAH { get; set; } = default!;
        public double USD { get; set; } = default!;
        public double UYU { get; set; } = default!;
        public double ZAR { get; set; } = default!;
    }
}
