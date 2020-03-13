using AuthServer.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthServer.Infrastructures
{
    interface IToken
    {
        string GenerateJSONWebToken(List<Claim> claims, ApplicationSetting _applicationSetting);
    }
}
