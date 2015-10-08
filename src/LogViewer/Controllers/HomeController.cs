using log4net;
using LogViewer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lumberjack.LogViewer.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ILog log = LogManager.GetLogger(typeof(HomeController));
            log.Debug("LoadingHomeController.");

            //TODO: Get logs from DB
            IEnumerable<Log4NetLog> logs = new List<Log4NetLog> {
                new Log4NetLog{Message = "Message1"},
                new Log4NetLog{Message = "Message2"},
                new Log4NetLog{Message = "Message3"},
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
