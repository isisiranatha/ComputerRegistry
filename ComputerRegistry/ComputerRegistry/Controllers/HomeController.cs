using System.Web.Mvc;

namespace ComputerRegistry.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to Computer Registry";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
