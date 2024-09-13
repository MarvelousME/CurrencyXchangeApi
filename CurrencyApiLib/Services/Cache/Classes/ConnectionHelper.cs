using StackExchange.Redis;
using System;

namespace CurrencyApiLib.Services.Cache.Classes
{
    /// <summary>
    /// The connection helper.
    /// </summary>
    public class ConnectionHelper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionHelper"/> class.
        /// </summary>
        static ConnectionHelper()
        {
            ConnectionHelper.lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
            {
                return ConnectionMultiplexer.Connect("127.0.0.1:6379");
            });
        }
        /// <summary>
        /// The lazy connection.
        /// </summary>
        private static Lazy<ConnectionMultiplexer> lazyConnection;
        /// <summary>
        /// Gets the connection.
        /// </summary>
        public static ConnectionMultiplexer Connection
        {
            get
            {
                return lazyConnection.Value;
            }
        }
    }
}
