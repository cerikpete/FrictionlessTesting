using System.Data;
using NUnit.Framework;

namespace Tests.Mapping.BaseClasses
{
    public class ValueChecker
    {
        private readonly DataRow dataRow;
        private readonly object valueToCheck;

        public ValueChecker(DataRow dataRow, object valueToCheck)
        {
            this.dataRow = dataRow;
            this.valueToCheck = valueToCheck;
        }

        public void IsEqualToValueInColumnWithName(string columnName)
        {
            bool valuesAreEqual = (dataRow[columnName].ToString().Trim() == valueToCheck.ToString().Trim());
            Assert.IsTrue(valuesAreEqual, string.Format("Values are not equal: Column name: {0}, Value in data row: {1}, Value in object: {2}", columnName, dataRow[columnName], valueToCheck));
        }
    }
}