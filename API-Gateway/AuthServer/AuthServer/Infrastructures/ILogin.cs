using AuthServer.Helper;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer.Infrastructures
{
    interface ILogin
    {
        dynamic Login(ISession ss, string email, string password, ApplicationSetting _applicationSetting);
        bool CheckPassword(string password, string dbPassword);
        dynamic BlockUser(ISession ss, Guid id);
        dynamic ChangePassword(ISession ss, Guid id, string password);
        dynamic Logout(ISession ss, Guid id);
    }
}
