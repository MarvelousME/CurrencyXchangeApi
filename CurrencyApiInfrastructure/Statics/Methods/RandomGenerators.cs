using System;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace CurrencyApiInfrastructure.Statics.Methods
{
    /// <summary>
    /// The random generators.
    /// </summary>
    public static class RandomGenerators
    {
        /// <summary>
        /// Randoms string from bytes.
        /// </summary>
        /// <returns>A string</returns>
        public static string RandomStringFromBytes()
        {
            var numberByte = new byte[32];

            using var rnd = RandomNumberGenerator.Create();

            rnd.GetBytes(numberByte);

            return Convert.ToBase64String(numberByte);
        }

        /// <summary>
        /// Randoms string from regex.
        /// </summary>
        /// <returns>A string</returns>
        public static string RandomStringFromRegex()
        {
            return Regex.Replace(Convert.ToBase64String(Guid.NewGuid().ToByteArray()), "[/+=]", "");
        }
    }
}
