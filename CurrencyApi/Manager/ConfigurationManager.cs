namespace CurrencyApi.Manager
{
    /// <summary>
    /// The configuration manager.
    /// </summary>
    public static class ConfigurationManager
    {
        /// <summary>
        /// Gets the application settings.
        /// </summary>
        public static IConfiguration ApplicationSettings { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationManager"/> class.
        /// </summary>
        static ConfigurationManager()
        {
            ApplicationSettings = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
        }
    }
}
