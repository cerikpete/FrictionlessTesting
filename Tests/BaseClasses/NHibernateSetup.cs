using System;
using System.Web;
using System.Web.Caching;
using NHibernate;
using NHibernate.Cfg;

namespace Tests.BaseClasses
{
    public class NHibernateSetup
    {
        private const string SessionFactoryKey = "SessionFactory";
        private readonly string _configPath;

        public NHibernateSetup(string configPath)
        {
            _configPath = configPath;
        }

        public ISessionFactory GetSessionFactory()
        {
            return (ISessionFactory)HttpRuntime.Cache.Get(SessionFactoryKey + _configPath);
        }

        public void CreateSessionFactory()
        {
            if (HttpRuntime.Cache.Get(SessionFactoryKey + _configPath) != null)
            {
                return;
            }

            Configuration cfg = new Configuration();
            cfg.Configure(_configPath);           
            var sessionFactory = cfg.BuildSessionFactory();
            HttpRuntime.Cache.Add(SessionFactoryKey + _configPath, sessionFactory, null, DateTime.Now.AddMinutes(15),
                                  Cache.NoSlidingExpiration,
                                  CacheItemPriority.Normal, null);
        }
    }
}