using CE.DataAccess.Dtos;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CE.WebAPI.Options
{
    public class AuthOptions : IAuthOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int TokenLifetime { get; set; }
        public string Secret { get; set; }

        public SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secret));
        }

        public string GenerateToken(UserDto user)
        {
            var claims = new List<Claim>
        {
            new ("id", user.Id.ToString()),
            new ("role", user.Role),
            new ("name", user.Name),
            new ("email", user.Email),
        };

            var key = GetSymmetricSecurityKey();

            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var now = DateTime.Now;

            var jwt = new JwtSecurityToken(
                Issuer,
                Audience,
                claims,
                notBefore: now,
                expires: now.AddMinutes(TokenLifetime),
                signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
