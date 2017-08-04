using System.Web.Mvc;

namespace AgeRanger.Controllers
{
    /// <summary>
    /// Provides landing page for Angular Application
    /// </summary>
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        
    }
}