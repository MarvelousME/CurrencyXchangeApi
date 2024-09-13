namespace CurrencyApiInfrastructure.AppSettings
{
    /// <summary>
    /// The app setting interface.
    /// </summary>
    public interface IAppSetting { }
    /// <summary>
    /// The app setting.
    /// </summary>
    public class AppSetting : IAppSetting
    {
        /// <summary>
        /// Gets or sets the token options.
        /// </summary>
        public TokenOptions TokenOptions { get; set; }

        /// <summary>
        /// Gets or sets the db connection string.
        /// </summary>
        public string DbConnectionString { get; set; }

        /// <summary>
        /// Gets or sets the redis connection.
        /// </summary>
        public RedisConnection RedisConnection { get; set; }
    }

    /// <summary>
    /// The token options.
    /// </summary>
    public class TokenOptions
    {
        /// <summary>
        /// Gets or sets the audience.
        /// </summary>
        public string Audience { get; set; }
        /// <summary>
        /// Gets or sets the issuer.
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// Gets or sets the access token expiration.
        /// </summary>
        public int AccessTokenExpiration { get; set; } //set as hours
        /// <summary>
        /// Gets or sets the refresh token expiration.
        /// </summary>
        public int RefreshTokenExpiration { get; set; } //set as hours

        /// <summary>
        /// Gets or sets the security key.
        /// </summary>
        public string SecurityKey { get; set; }
    }

    /// <summary>
    /// The redis connection.
    /// </summary>
    public class RedisConnection
    {
        /// <summary>
        /// Gets or sets the connection string.
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Gets or sets the instance name.
        /// </summary>
        public string InstanceName { get; set; }
    }
}
