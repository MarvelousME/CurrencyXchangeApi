using System;

namespace CurrencyApi.Models
{
    /// <summary>
    /// The external API.
    /// </summary>
    public class ExternalAPI
    {
        /// <summary>
        /// Name.
        /// </summary>
        public const string Name = "ExternalAPI";

        /// <summary>
        /// Gets or sets the api key.
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        /// Gets or sets the url.
        /// </summary>
        public string Url { get; set; }
    }
}
