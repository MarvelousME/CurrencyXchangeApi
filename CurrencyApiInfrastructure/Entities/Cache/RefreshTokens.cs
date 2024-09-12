using System;
using System.Collections.Generic;

namespace CurrencyApiInfrastructure.Entities.Cache
{
    public class RefreshTokens
    {
        public string TokenKey { get; set; }
        public DateTime DueTime { get; set; }

        public string UserName { get; set; }

        public string FullName { get; set; }

        public IEnumerable<string> UserRoleNames { get; set; }

        public string ClientIp { get; set; }

        public string ClientAgent { get; set; }
    }
}
