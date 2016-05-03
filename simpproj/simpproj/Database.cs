using NHibernate;
using NHibernate.Cfg;
using NHibernate.Mapping.ByCode;
using simpproj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace simpproj
{
    public static class Database
    {
        private const string SessionKey = "simpproj.Database.SessionKey";

        private static ISessionFactory _sessionFactory;

        public static ISession Session
        {
            get { return (ISession)HttpContext.Current.Items[SessionKey]; }
        }
        // Invoked at app start
        public static void Configure()
        {
            var config = new Configuration();

            // Configure the connection string
            config.Configure();
            // Add mappings
            var mapper = new ModelMapper();
            mapper.AddMapping<UserMap>();
            mapper.AddMapping<RoleMap>();

            config.AddMapping(mapper.CompileMappingForAllExplicitlyAddedEntities());
            // create the session factory
            _sessionFactory = config.BuildSessionFactory();
        }

        // Invoked at the beginning of every request
        public static void OpenSession()
        {
            HttpContext.Current.Items[SessionKey] = _sessionFactory.OpenSession();
        }

        // Invoked at the end of every request
        public static void CloseSession()
        {
            var session = HttpContext.Current.Items[SessionKey] as ISession;

            if (session != null)
                session.Close();

            HttpContext.Current.Items.Remove(SessionKey);
        }

    }
}