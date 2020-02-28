using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.Extensions.Configuration;
using NHibernate;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ECM.BUSINESS.Sessions
{
    public static class SessionManager
    {
        private static readonly ISessionFactory _sessionFactory;
        private static readonly IConfiguration _configuration;
        static SessionManager()
        {
            _configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            var connString = _configuration.GetConnectionString("DefaultConnection");
            _sessionFactory = Fluently.Configure().
                Database(MsSqlConfiguration.MsSql2012.ConnectionString(connString))
                //.Mappings(p => p.FluentMappings.AddFromAssembly(typeof(SessionManager).Assembly)) // get assembly inner project.
                .Mappings(p => p.FluentMappings.AddFromAssembly(GetAssembly())) // get assembly outer project.
                .BuildSessionFactory();

        }
        public static void DoWork(Action<ISession> work)
        {
            using (ISession session = SessionManager._sessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    work.Invoke(session);
                    transaction.Commit();
                }
            }
        }
        public static void DoWork(Action<ISession, ITransaction> work)
        {
            using (ISession session = SessionManager._sessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    work.Invoke(session, transaction);
                }
            }
        }
        private static Assembly GetAssembly()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
            var assembly = assemblies.Single(p => p.GetName().Name == "ECM.DB");
            return assembly;
        }
    }
}
