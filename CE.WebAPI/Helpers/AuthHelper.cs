﻿using CE.DataAccess;
using CE.DataAccess.Constants;
using CE.WebAPI.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CE.WebAPI.Helpers
{
    public static class AuthHelper
    {
        
        public static long GetUserID(ClaimsPrincipal user)
        {
            //return long.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier));
            return long.Parse(user.FindFirstValue("id"));
        }

        public static bool IsHasAccess(ClaimsPrincipal user, long id)
        {
            return user.IsInRole(RolesConstants.Admin) || GetUserID(user) == id;
        }

        public static string GenerateToken(User user, AuthOptions authOptions)
        {
            var claims = new List<Claim>
            {
                new Claim("id", user.Id.ToString()),
                new Claim("role", user.Role),
                new Claim("name", user.Name),
                new Claim("email", user.Email),
                //new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                //new Claim(ClaimTypes.Role, user.Role)
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