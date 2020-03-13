using AuthServer.Helper;
using AuthServer.Infrastructures;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthServer.Services
{
    public class TokenService : IToken
    {
        public string GenerateJSONWebToken(List<Claim> claims, ApplicationSetting _applicationSetting)
        {
            var result = string.Empty;
            try
            {
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _applicationSetting.Jwt.Issuer,
                    ValidAudience = _applicationSetting.Jwt.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_applicationSetting.Jwt.Key)),
                    ClockSkew = TimeSpan.Zero, //the default for this setting is 5 minutes
                    RequireExpirationTime = true
                };
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_applicationSetting.Jwt.Key));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    issuer: _applicationSetting.Jwt.Issuer,
                    audience: _applicationSetting.Jwt.Audience,
                    claims,
                    notBefore: DateTime.UtcNow,
                    //expires: DateTime.UtcNow.AddMinutes(5),
                    expires: DateTime.UtcNow.Add(TimeSpan.Parse(_applicationSetting.Jwt.TokenLifeTime)),
                    signingCredentials: credentials
                );
                result = new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }
    }
}
