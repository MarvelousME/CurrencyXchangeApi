using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CurrencyApiInfrastructure.Statics.Methods
{
    public static class TokenRelated
    {
        public static SecurityKey GetSymmetricSecurityKey(string securityKey)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
        }
    }
}
