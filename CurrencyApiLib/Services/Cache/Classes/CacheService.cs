using CurrencyApiLib.Services.Cache.Interfaces;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;

namespace CurrencyApiLib.Services.Cache.Classes
{
    /// <summary>
    /// The cache service.
    /// </summary>
    public class CacheService : ICacheService
    {
        /// <summary>
        /// The db.
        /// </summary>
        private IDatabase _db;
        /// <summary>
        /// Initializes a new instance of the <see cref="CacheService"/> class.
        /// </summary>
        public CacheService()
        {
            ConfigureRedis();
        }
        /// <summary>
        /// Configures the redis.
        /// </summary>
        private void ConfigureRedis()
        {
            _db = ConnectionHelper.Connection.GetDatabase();
        }
        /// <summary>
        /// Get the data.
        /// </summary>
        /// <typeparam name="T"/>
        /// <param name="key">The key.</param>
        /// <returns>A <typeparamref name="T"/></returns>
        public T GetData<T>(string key)
        {
            var value = _db.StringGet(key);
            if (!string.IsNullOrEmpty(value))
            {
                return JsonConvert.DeserializeObject<T>(value);
            }
            return default;
        }
        /// <summary>
        /// Set data.
        /// </summary>
        /// <typeparam name="T"/>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="expirationTime">The expiration time.</param>
        /// <returns>A bool</returns>
        public bool SetData<T>(string key, T value, DateTimeOffset expirationTime)
        {
            TimeSpan expiryTime = expirationTime.DateTime.Subtract(DateTime.Now);
            var isSet = _db.StringSet(key, JsonConvert.SerializeObject(value), expiryTime);
            return isSet;
        }
        /// <summary>
        /// Remove the data.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>An object</returns>
        public object RemoveData(string key)
        {
            bool _isKeyExist = _db.KeyExists(key);
            if (_isKeyExist == true)
            {
                return _db.KeyDelete(key);
            }
            return false;
        }
    }
}

