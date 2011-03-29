using System;
using System.Data;
using System.Data.SQLite;
using System.Linq.Expressions;
using NUnit.Framework;
using Tests.BaseClasses;

namespace Tests.Mapping.BaseClasses
{
    [Category("Mapping Tests")]
    public abstract class MappingTestFor<SystemUnderTest> : DatabaseTest
    {
        protected SystemUnderTest systemUnderTest;
        private DataRow testDataRow;
        private readonly ReflectiveMapper _reflectiveMapper;

        public MappingTestFor()
        {            
            _reflectiveMapper = new ReflectiveMapper();
        }

        /// <summary>
        /// Method to save the test data to the data store using the appropriate Dao.
        /// </summary>
        private void SaveSystemUnderTest()
        {            
            StartTransaction();

            // Save the system under test
            _repository.Save(systemUnderTest);
        }

        private void LoadSystemUnderTestUsingReflection()
        {
            systemUnderTest = LoadObjectUsingReflection<SystemUnderTest>();
        }

        protected abstract string SqlToRetrieveTestDataRow { get; }

        private void GetDataRowWithTestData()
        {          
            if (testDataRow == null)
            {
                var sqlLiteConnection = _connection as SQLiteConnection;
                var cmd = new SQLiteCommand(SqlToRetrieveTestDataRow, sqlLiteConnection);
                cmd.CommandType = CommandType.Text;
                var dataAdapter = new SQLiteDataAdapter(cmd);
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds);
                testDataRow = ds.Tables[0].Rows[0];
            }
        }

        protected void SetField(object target, string fieldName, object value)
        {
            _reflectiveMapper.SetField(target, fieldName, value);
        }

        protected ValueChecker EnsureValueIn(Expression<Func<SystemUnderTest, object>> property)
        {
            object valueToCheck = property.Compile().Invoke(systemUnderTest);

            // For bit fields (booleans) we need to convert "True" to a bit representation (1)
            var unaryExpression = property.Body as UnaryExpression;
            if (unaryExpression != null)
            {
                var propetyType = unaryExpression.Operand.Type;
                if (propetyType == typeof(bool) || propetyType == typeof(bool?))
                {
                    if (valueToCheck.ToString().ToLower() == "true")
                    {
                        valueToCheck = 1;
                    }
                    else
                    {
                        valueToCheck = 0;
                    }
                }
            }
            
            return EnsureThat(valueToCheck);
        }

        protected override System.Reflection.Assembly MappingAssembly
        {
            get { return typeof (SystemUnderTest).Assembly; }
        }

        protected ValueChecker EnsureThat(object valueToCheck)
        {
            GetDataRowWithTestData();
            return new ValueChecker(testDataRow, valueToCheck);
        }
      
        /// <summary>
        /// Virtual method used to set up test values on the system under test when the values inserted via reflection are not sufficient.  Also
        /// optionally will save any objects needed to correctly set up the system under test.
        /// </summary>
        protected virtual void SetUpTestDataOnSystemUnderTest()
        {
        }

        /// <summary>
        /// Sets up the system under test with test data.
        /// </summary>
        private void SetUpSystemUnderTest()
        {
            LoadSystemUnderTestUsingReflection();
            SetUpTestDataOnSystemUnderTest();
        }

        protected T GetObject<T>(object id)
        {
            return _repository.Get<T>(id);
        }

        /// <summary>
        /// A method that allows you to retrieve objects saved by the test.  This ensures you are in the correct spot 
        /// in the test life cycle to retrieve persisted objects.
        /// </summary>
        protected virtual void GetObjectsSavedByTheTest(){}

        public override void BaseSetUp()
        {
            base.BaseSetUp();

            SetUpSystemUnderTest();
            SaveSystemUnderTest();

            CommitTransaction();
            GetObjectsSavedByTheTest();
        }
    }
}