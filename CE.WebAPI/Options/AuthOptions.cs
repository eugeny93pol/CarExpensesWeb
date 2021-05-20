using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace CE.WebAPI.Options
{
    public class AuthOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int TokenLifetime { get; set; }

        public string Secret { get; set; }

    public SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secret));
        }
    }
}
