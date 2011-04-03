using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web.Controllers;
using Tests.Specs.BaseClasses;
using Data;
using Data.Domain;
using NUnit.Framework;
using System.Web.Mvc;

namespace Tests.Specs.NewStyle
{
    public class BaseHomeControllerNewStyleSpecs : SpecificationsFor<HomeController>
    {
        protected IEnumerable<Document> _documents;

        public override void Set_up_context()
        {
            base.Set_up_context();

            // Set up dependencies
            var repository = AMockedDependencyOfType<IRepository>();

            // Set up the list of documents returned
            _documents = new List<Document>();
            WhenThe(repository).IsToldTo(x => x.All<Document>()).Return(_documents.AsQueryable());
        }

        public override void And_calling()
        {
            sut.Index();
        }
    }

    [TestFixture]
    public class When_view_the_home_controller_index_page : BaseHomeControllerNewStyleSpecs
    {
        [Test]
        public void Should_load_all_documents_to_the_view()
        {
            var result = sut.Index() as ViewResult;
            Assert.AreEqual(_documents, result.ViewData["Data"]);
        }
    }
}
