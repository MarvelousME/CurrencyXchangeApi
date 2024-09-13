using System.Linq;
using System.Security.Claims;

namespace CurrencyApiInfrastructure.Extensions.TokenExtensions
{
    /// <summary>
    /// The acces info from token.
    /// </summary>
    public class AccesInfoFromToken
    {
        /// <summary>
        /// Get my user id.
        /// </summary>
        /// <returns>A string</returns>
        public static string GetMyUserId()
        {
            return GlobalHttpContext._contextAccessor.HttpContext!.User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier)
                                                                             .Select(x => x.Value)
                                                                             .FirstOrDefault();
        }
    }
}
