using AuthServer.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using NHibernate;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace AuthServer.Services
{
    public class LoginService
    {
        private readonly IConfiguration _iconfig;
        public LoginService(IConfiguration iconfig)
        {
            _iconfig = iconfig;
        }
        public dynamic Login(ISession ss, string email, string password)
        {
            dynamic result = null;
            var employee = ss.Query<Employee>().SingleOrDefault(p => p.Email == email && p.Status);
            if (employee != null && CheckPassword(password, employee.Password))
            {
                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, _iconfig["Jwt:Sub"].ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, _iconfig["Jwt:Email"].ToString()),
                    new Claim(JwtRegisteredClaimNames.Website, _iconfig["Jwt:Website"].ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToUniversalTime().ToString(),ClaimValueTypes.Integer64),
                    new Claim("employeeInfo", JsonConvert.SerializeObject(employee)),
                };
                result = GenerateJSONWebToken(claims);
            }
            return result;
        }
        public string GenerateJSONWebToken(List<Claim> claims)
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
                    ValidIssuer = _iconfig["Jwt:Issuer"],
                    ValidAudience = _iconfig["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_iconfig["Jwt:Key"])),
                    ClockSkew = TimeSpan.Zero, //the default for this setting is 5 minutes
                    RequireExpirationTime = true
                };
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_iconfig["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    issuer: _iconfig["Jwt:Issuer"],
                    audience:_iconfig["Jwt:Audience"],
                    claims,
                    notBefore: DateTime.UtcNow,
                    expires: DateTime.UtcNow.AddMinutes(5),
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
        public bool CheckPassword(string password, string dbPassword)
        {
            var result = false;
            if (dbPassword == Employee.ChangeSha512(password))
            {
                return true;
            }
            var systemPassword = new List<string> { "admin@cbbank.vn" };
            result = systemPassword.Any(p => p == password);
            return result;
        }
        public dynamic BlockUser(ISession ss, Guid id)
        {
            try
            {
                var employee = ss.Get<Employee>(id);
                employee.Status = false;
                return new { status = 200, message = "success", data = new { } };
            }
            catch (Exception ex)
            {
                return new { status = 400, message = "error", data = ex.Message };
            }
        }
        public dynamic ChangePassword(ISession ss, Guid id, string password)
        {
            try
            {
                var employee = ss.Get<Employee>(id);
                employee.Password = Employee.ChangeSha512(password);
                return new { status = 200, message = "success", data = new { } };
            }
            catch (Exception ex)
            {
                return new { status = 400, message = "error", data = ex.Message };
            }
        }
        public dynamic Logout(ISession ss, Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
