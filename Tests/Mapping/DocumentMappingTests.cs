using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tests.Mapping.BaseClasses;
using Data.Domain;
using NUnit.Framework;

namespace Tests.Mapping
{
    [TestFixture]
    public class When_mapping_a_Document : MappingTestFor<Document>    
    {
        [Test]
        public void The_value_in_name_property_should_be_equal_to_the_value_in_the_name_column()
        {
            EnsureValueIn(x => x.Name).IsEqualToValueInColumnWithName("Name");
        }

        protected override string SqlToRetrieveTestDataRow
        {
            get { return string.Format("select * from Document where Id = '{0}'", systemUnderTest.Id); }
        }
    }
}
