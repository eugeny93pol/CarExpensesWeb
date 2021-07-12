using CE.DataAccess.Constants;
using CE.WebAPI.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using CE.DataAccess.Dtos;

namespace CE.WebAPI.Helpers
{
    public static class AuthHelper
    {
        
        public static Guid GetUserId(ClaimsPrincipal user)
        {
            return Guid.Parse(user.FindFirstValue("id"));
        }

        public static bool IsHasAccess(ClaimsPrincipal user, Guid? id)
        {
            return user.IsInRole(RolesConstants.Admin) || GetUserId(user) == id;
        }

        public static string GenerateToken(UserDto user, AuthOptions authOptions)
        {
            var claims = new List<Claim>
            {
                new ("id", user.Id.ToString()),
                new ("role", user.Role),
                new ("name", user.Name),
                new ("email", user.Email),
            };

            var key = authOptions.GetSymmetricSecurityKey();

            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var now = DateTime.Now;

            var jwt = new JwtSecurityToken(
                authOptions.Issuer,
                authOptions.Audience,
                claims,
                notBefore: now,
                expires: now.AddMinutes(authOptions.TokenLifetime),
                signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
