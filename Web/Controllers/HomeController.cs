using System.Web.Mvc;
using Data;
using Data.Domain;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository _repository;

        public HomeController(IRepository repository)
        {
            _repository = repository;
        }

        public ActionResult Index()
        {
            ViewData["Message"] = "Welcome to ASP.NET MVC!";

            ViewData["Data"] = _repository.All<Document>();
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
