using BCrypt.Net;
using CurrencyApiInfrastructure.ExceptionHandling;
using CurrencyApiInfrastructure.Resources;
using Microsoft.Extensions.Localization;

namespace CurrencyApiInfrastructure.Extensions
{
    /// <summary>
    /// The generate and verify passwords.
    /// </summary>
    public static class GenerateAndVerifyPasswords
    {
        /// <summary>
        /// The minimum password length.
        /// </summary>
        private const byte MinimumPasswordLength = 8;

        /// <summary>
        /// The localizer.
        /// </summary>
        public static IStringLocalizer<ExceptionsResource> Localizer;
        /// <summary>
        /// Creates password hash.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="passwordHash">The password hash.</param>
        /// <exception cref="CustomBusinessException"></exception>
        public static void CreatePasswordHash(this string password, out string passwordHash)
        {
            if (password == null)
                throw new CustomBusinessException(Localizer["Password_Null"]);
            if (string.IsNullOrWhiteSpace(password))
                throw new CustomBusinessException(Localizer["Password_Space"]);
            if (password.Length < MinimumPasswordLength)
                throw new CustomBusinessException(string.Format(Localizer["Password_Min_Length"], MinimumPasswordLength));

            passwordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(password, hashType: HashType.SHA384);
        }

        /// <summary>
        /// Verifies password hash.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="passwordHash">The password hash.</param>
        /// <exception cref="CustomBusinessException"></exception>
        /// <returns>A bool</returns>
        public static bool VerifyPasswordHash(this string password, string passwordHash)
        {
            if (password == null)
                throw new CustomBusinessException(Localizer["Password_Null"]);
            if (string.IsNullOrWhiteSpace(password))
                throw new CustomBusinessException(Localizer["Password_Space"]);

            return BCrypt.Net.BCrypt.EnhancedVerify(password, passwordHash, hashType: HashType.SHA384);
        }
    }
}
