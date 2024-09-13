using System;
using System.ComponentModel.DataAnnotations;

namespace CurrencyApiInfrastructure.DbBase
{
    /// <summary>
    /// The table base.
    /// </summary>
    public abstract class TableBase : ISoftDeletable
    {
        /// <summary>
        /// Gets or sets the created at.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the last updated at.
        /// </summary>
        public DateTime? LastUpdatedAt { get; set; }

        /// <summary>
        /// Gets or sets the deleted at.
        /// </summary>
        public DateTime? DeletedAt { get; set; }

        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        [MaxLength(30)]
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the last updated by.
        /// </summary>
        [MaxLength(30)]
        public string LastUpdatedBy { get; set; }

        /// <summary>
        /// Gets or sets the deleted by.
        /// </summary>
        [MaxLength(30)]
        public string DeletedBy { get; set; }
    }
}
