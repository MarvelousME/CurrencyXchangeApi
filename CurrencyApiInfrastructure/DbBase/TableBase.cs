using System;
using System.ComponentModel.DataAnnotations;

namespace CurrencyApiInfrastructure.DbBase
{
    public abstract class TableBase : ISoftDeletable
    {
        public DateTime CreatedAt { get; set; }

        public DateTime? LastUpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

        [MaxLength(30)]
        public string CreatedBy { get; set; }

        [MaxLength(30)]
        public string LastUpdatedBy { get; set; }

        [MaxLength(30)]
        public string DeletedBy { get; set; }
    }
}
