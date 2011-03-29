using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Data;
using Data.Domain;
using NUnit.Framework;
using Rhino.Mocks;
using Web.Controllers;

namespace Tests.Specs.OldStyle
{
    [TestFixture]
    public class When_viewing_the_home_controller_index_page
    {
        private IEnumerable<Document> _documents;
        private HomeController _homeController;

        [SetUp]
        public void SetUp()
        {
            _documents = new List<Document> {new Document()};
            var repository = MockRepository.GenerateMock<IRepository>();
            _homeController = new HomeController(repository);

            repository.Stub(x => x.All<Document>()).Return(_documents.AsQueryable());
        }

        [Test]
        public void Should_load_all_documents_to_the_view()
        {
            var result = _homeController.Index() as ViewResult;
            Assert.AreEqual(_documents, result.ViewData["Data"]);
        }
    }
}