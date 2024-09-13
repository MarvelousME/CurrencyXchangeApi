using CurrencyApiInfrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CurrencyApiDomain.Configurations
{
    /// <summary>
    /// The currency rates configuration.
    /// </summary>
    public class CurrencyRatesConfiguration : IEntityTypeConfiguration<CurrencyRate>
    {
        public void Configure(EntityTypeBuilder<CurrencyRate> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.GetRequestDate).IsRequired();
            builder.Property(b => b.AED);
            builder.Property(b => b.ARS);
            builder.Property(b => b.AUD);
            builder.Property(b => b.BGN);
            builder.Property(b => b.BRL);
            builder.Property(b => b.BSD);
            builder.Property(b => b.CAD);
            builder.Property(b => b.CHF);
            builder.Property(b => b.CLP);
            builder.Property(b => b.CNY);
            builder.Property(b => b.COP);
            builder.Property(b => b.CZK);
            builder.Property(b => b.DKK);
            builder.Property(b => b.DOP);
            builder.Property(b => b.EGP);
            builder.Property(b => b.EUR);
            builder.Property(b => b.FJD);
            builder.Property(b => b.GBP);
            builder.Property(b => b.GTQ);
            builder.Property(b => b.HKD);
            builder.Property(b => b.HRK);
            builder.Property(b => b.HUF);
            builder.Property(b => b.IDR);
            builder.Property(b => b.ILS);
            builder.Property(b => b.INR);
            builder.Property(b => b.ISK);
            builder.Property(b => b.JPY);
            builder.Property(b => b.KRW);
            builder.Property(b => b.KZT);
            builder.Property(b => b.MXN);
            builder.Property(b => b.MYR);
            builder.Property(b => b.NOK);
            builder.Property(b => b.NZD);
            builder.Property(b => b.PAB);
            builder.Property(b => b.PEN);
            builder.Property(b => b.PHP);
            builder.Property(b => b.PKR);
            builder.Property(b => b.PLN);
            builder.Property(b => b.PYG);
            builder.Property(b => b.RON);
            builder.Property(b => b.RUB);
            builder.Property(b => b.SAR);
            builder.Property(b => b.SEK);
            builder.Property(b => b.SGD);
            builder.Property(b => b.THB);
            builder.Property(b => b.TRY);
            builder.Property(b => b.TWD);
            builder.Property(b => b.UAH);
            builder.Property(b => b.USD);
            builder.Property(b => b.UYU);
            builder.Property(b => b.ZAR);
        }
    }
}
