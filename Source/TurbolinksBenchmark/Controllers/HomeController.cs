namespace TurbolinksBenchmark.Controllers
{
    using System.Web.Mvc;

    public class HomeController : Controller
    {
        public ActionResult Index(int? id, bool? enableTurbolinks)
        {
            ViewBag.id = id.GetValueOrDefault();
            ViewBag.nextId = ViewBag.id + 1;
            ViewBag.enableTurbolinks = enableTurbolinks.GetValueOrDefault(true);

            return View();
        }
    }
}