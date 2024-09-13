using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CurrencyApiInfrastructure.Statics.Methods
{
    /// <summary>
    /// The token related.
    /// </summary>
    public static class TokenRelated
    {
        /// <summary>
        /// Get symmetric security key.
        /// </summary>
        /// <param name="securityKey">The security key.</param>
        /// <returns>A SecurityKey</returns>
        public static SecurityKey GetSymmetricSecurityKey(string securityKey)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
        }
    }
}
