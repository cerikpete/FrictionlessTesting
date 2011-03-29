using System.Data;
using System.Data.SQLite;
using System.Reflection;
using Data;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;

namespace Tests.BaseClasses
{
    /// <summary>
    /// Base class used for test that will have some sort of database access
    /// </summary>
    public abstract class DatabaseTest
    {
        private ReflectiveMapper _reflectiveMapper;
        private ISessionFactory _sessionFactory;
        protected IDbConnection _connection;
        private Configuration SavedConfig;
        private ISession _session;
        protected IRepository _repository;

        private const string SQLiteConnectionString = "Data Source=:memory:;Version=3;BinaryGuid=False;";

        public DatabaseTest()
        {
            _reflectiveMapper = new ReflectiveMapper();
        }

        protected void StartTransaction()
        {
            _session.Transaction.Begin();
        }

        protected void CommitTransaction()
        {
            _session.Transaction.Commit();
        }

        private void SetUpSession()
        {
            _connection = new SQLiteConnection(SQLiteConnectionString);
            _connection.Open();

            _session = CreateSession();
            _repository = new Repository(CreateSession());
        }
        
        private ISessionFactory GetSessionFactory()
        {
            if (_sessionFactory == null)
            {
                _sessionFactory = Fluently
                    .Configure()
                    .Database(SQLiteConfiguration.Standard.InMemory())
                    .Mappings(m => m.FluentMappings.AddFromAssembly(MappingAssembly))
                    .ExposeConfiguration(c => SavedConfig = c)
                    .BuildSessionFactory();
                BuildSchema();
            }
            return _sessionFactory;
        }

        protected abstract Assembly MappingAssembly { get; }

        protected void SaveObject<T>(T objectToSave)
        {
            try
            {
                _repository.Save(objectToSave);
            }
            catch (NonUniqueObjectException)
            {
                // Suppress this exception since we don't care for testing if the same object was somehow added twice to the session
                // across tests.
            }
        }

        protected TestObjectType LoadObjectUsingReflection<TestObjectType>()
        {
            return _reflectiveMapper.LoadObjectUsingReflection<TestObjectType>();
        }

        private ISession CreateSession()
        {
            var factory = GetSessionFactory();
            var session = factory.OpenSession(_connection);
            return session;
        }

        private void BuildSchema()
        {
            var export = new SchemaExport(SavedConfig);
            export.Execute(false, true, false, _connection, null);
        }

        [TestFixtureSetUp]
        public virtual void BaseSetUp()
        {
            SetUpSession();
        }

        [TestFixtureTearDown]
        public void BaseTearDown()
        {
            _connection.Dispose();
        }
    }
}