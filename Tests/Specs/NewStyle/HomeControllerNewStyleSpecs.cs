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
        protected ActionResult _result;

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
            _result = sut.Index();
        }
    }

    [TestFixture]
    public class When_viewing_the_home_controller_index_page : BaseHomeControllerNewStyleSpecs
    {
        [Test]
        public void Should_load_all_documents_to_the_view()
        {
            var viewResult = _result as ViewResult;
            Assert.AreEqual(_documents, viewResult.ViewData["Data"]);
        }
    }
}
