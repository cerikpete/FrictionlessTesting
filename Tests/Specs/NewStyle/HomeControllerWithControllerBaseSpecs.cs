using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web.Controllers;
using Tests.Specs.BaseClasses;
using NUnit.Framework;
using Data.Domain;
using System.Web.Mvc;
using Data;

namespace Tests.Specs.NewStyle
{
    public class BaseHomeControllerWithControllerBaseSpecs : ControllerSpecificationsFor<HomeController>
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
    public class When_viewing_the_home_controller_index_page_with_controller_base_specs : BaseHomeControllerWithControllerBaseSpecs
    {
        [Test]
        public void Shouuld_load_all_documents_to_the_view()
        {
            Assert.AreEqual(_documents, GetViewDataFromResult(_result));
        }
    }
}
