using System;
using System.Reflection;

namespace Tests.BaseClasses
{
    /// <summary>
    /// Class used by tests to loop through the properties of an object and set each property equal to random test data, based on the
    /// property's type.
    /// </summary>
    public class ReflectiveMapper
    {
        private object itemUnderTest;
        private Random _intGenerator = new Random();
        private Random _stringGenerator = new Random();

        public SystemUnderTest LoadObjectUsingReflection<SystemUnderTest>()
        {
            itemUnderTest = Activator.CreateInstance(typeof(SystemUnderTest));
            PropertyInfo[] properties = itemUnderTest.GetType().GetProperties();
            foreach (var propertyInfo in properties)
            {
                LoadPropertyWithTestValue(propertyInfo);
            }
            return (SystemUnderTest)itemUnderTest;
        }

        private void LoadPropertyWithTestValue(PropertyInfo propertyInfo)
        {
            if (propertyInfo.CanWrite)
            {
                propertyInfo.SetValue(itemUnderTest, CreateTestValueBasedOnPropertyNameAndType(propertyInfo), null);
            }
        }

        private object CreateTestValueBasedOnPropertyNameAndType(PropertyInfo propertyInfo)
        {
            Type propertyType = propertyInfo.PropertyType;
            object testValueToReturn = null;

            if (propertyType == typeof(string))
            {
                testValueToReturn = GenerateRandomString();
            }
            else if (propertyType == typeof(int))
            {
                testValueToReturn = GenerateRandomInteger();
            }
            else if (propertyType == typeof(DateTime) || propertyType == typeof(DateTime?))
            {
                testValueToReturn = DateTime.Now;
            }
            else if (propertyType == typeof(bool) || propertyType == typeof(bool?))
            {
                testValueToReturn = true;
            }
            else if (propertyType == typeof(Guid))
            {
                testValueToReturn = Guid.NewGuid();
            }

            return testValueToReturn;
        }

        private int GenerateRandomInteger()
        {
            return _intGenerator.Next(1000);
        }

        private string GenerateRandomString()
        {
            return _stringGenerator.Next(999).ToString();
        }

        public void SetField(object target, string fieldName, object value)
        {
            FieldInfo fi = target.GetType().GetField(fieldName, BindingFlags.DeclaredOnly | BindingFlags.NonPublic | BindingFlags.Instance);
            fi.SetValue(target, value);
        }

        public void SetProperty(object target, string fieldName, object value)
        {
            PropertyInfo pi = target.GetType().GetProperty(fieldName);
            pi.SetValue(target, value, null);
        }

        public void SetIdField(object target, string fieldName, int value)
        {
            SetField(target, fieldName, value);
        }
    }
}