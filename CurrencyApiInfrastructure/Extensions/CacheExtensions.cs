using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace CurrencyApiInfrastructure.Extensions
{
    /// <summary>
    /// The cache extensions.
    /// </summary>
    public static class CacheExtensions
    {
        /// <summary>
        /// Save the record asynchronously.
        /// </summary>
        /// <typeparam name="T"/>
        /// <param name="cache">The cache.</param>
        /// <param name="recordId">The record id.</param>
        /// <param name="data">The data.</param>
        /// <param name="absoluteExpireTime">The absolute expire time.</param>
        /// <param name="unusedExpireTime">The unused expire time.</param>
        /// <returns>A Task</returns>
        public static async Task SaveRecordAsync<T>(this IDistributedCache cache,
            string recordId,
            T data,
            TimeSpan? absoluteExpireTime = null,
            TimeSpan? unusedExpireTime = null)
        {
            var options = new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = absoluteExpireTime,
                SlidingExpiration = unusedExpireTime
            };

            await cache.SetStringAsync(recordId, JsonSerializer.Serialize(data), options);
        }

        /// <summary>
        /// Get the record asynchronously.
        /// </summary>
        /// <typeparam name="T"/>
        /// <param name="cache">The cache.</param>
        /// <param name="recordId">The record id.</param>
        /// <returns><![CDATA[ValueTask<T>]]></returns>
        public static async ValueTask<T> GetRecordAsync<T>(this IDistributedCache cache, string recordId)
        {
            var jsonData = await cache.GetStringAsync(recordId);

            if (jsonData is null)
                return default;

            return JsonSerializer.Deserialize<T>(jsonData);
        }

        /// <summary>
        /// Remove the record asynchronously.
        /// </summary>
        /// <param name="cache">The cache.</param>
        /// <param name="recordId">The record id.</param>
        /// <returns>A Task</returns>
        public static async Task RemoveRecordAsync(this IDistributedCache cache, string recordId)
        {
            await cache.RemoveAsync(recordId);
        }

        /// <summary>
        /// Remove the records.
        /// </summary>
        /// <param name="cache">The cache.</param>
        /// <param name="recordIds">The record ids.</param>
        public static void RemoveRecords(this IDistributedCache cache, IEnumerable<string> recordIds)
        {
            Parallel.ForEach(recordIds, async id => await cache.RemoveAsync(id));
        }
    }
}
