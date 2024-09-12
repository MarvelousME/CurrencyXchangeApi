using CurrencyApiInfrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace CurrencyApiDomain.ContextRelated
{
    public partial class AppDbContext : DbContext
    {
        //public DbSet<Members> Members { get; set; }

        //public DbSet<Books> Books { get; set; }
        public DbSet<CurrencyRate> CurrencyRates { get; set; }

        //public DbSet<ActiveBookReservations> ActiveBookReservations { get; set; }

        //public DbSet<BookReservationHistories> BookReservationHistory { get; set; }

        //public DbSet<Roles> Roles { get; set; }

        //public DbSet<MemberRoles> MemberRoles { get; set; }
    }
}
