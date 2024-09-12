using System;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace CurrencyApiInfrastructure.Statics.Methods
{
    public static class RandomGenerators
    {
        public static string RandomStringFromBytes()
        {
            var numberByte = new byte[32];

            using var rnd = RandomNumberGenerator.Create();

            rnd.GetBytes(numberByte);

            return Convert.ToBase64String(numberByte);
        }

        public static string RandomStringFromRegex()
        {
            return Regex.Replace(Convert.ToBase64String(Guid.NewGuid().ToByteArray()), "[/+=]", "");
        }
    }
}
