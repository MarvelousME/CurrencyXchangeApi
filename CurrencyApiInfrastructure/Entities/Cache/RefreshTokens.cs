using System;
using System.Collections.Generic;

namespace CurrencyApiInfrastructure.Entities.Cache
{
    /// <summary>
    /// The refresh tokens.
    /// </summary>
    public class RefreshTokens
    {
        /// <summary>
        /// Gets or sets the token key.
        /// </summary>
        public string TokenKey { get; set; }
        /// <summary>
        /// Gets or sets the due time.
        /// </summary>
        public DateTime DueTime { get; set; }

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the full name.
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Gets or sets the user role names.
        /// </summary>
        public IEnumerable<string> UserRoleNames { get; set; }

        /// <summary>
        /// Gets or sets the client ip.
        /// </summary>
        public string ClientIp { get; set; }

        /// <summary>
        /// Gets or sets the client agent.
        /// </summary>
        public string ClientAgent { get; set; }
    }
}
