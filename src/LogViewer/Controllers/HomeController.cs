using System.Collections.Generic;
using System.Web.Mvc;
using AlertSense.Lumberjack.Contracts.Entities;
using log4net;

namespace LogViewer.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ILog log = LogManager.GetLogger(typeof(HomeController));
            log.Debug("LoadingHomeController.");

            IEnumerable<AdoNetLog> logs = new List<AdoNetLog> {
                new AdoNetLog{Message = "Broke the build", Exception = "Bad Things Happened.."},
                new AdoNetLog{Message = "No controller for /images/cat.png", Exception = "Bad Things Happened.."},
                new AdoNetLog{Message = "Fixed the build!"},
            };
            return View(logs);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
