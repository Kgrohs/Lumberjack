using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;
using LogViewer.Models;
using Lumberjack.LogViewer.Controllers;

namespace LogViewer.Controllers
{
    public class LogViewerController : Controller
    {
        //
        // GET: /LogViewer/

        public ActionResult Index()
        {
            ILog log = LogManager.GetLogger(typeof(HomeController));
            log.Debug("LoadingHomeController.");

            
            IEnumerable<Log4NetLog> logs = new List<Log4NetLog> {
                new Log4NetLog{Message = "Message1"},
                new Log4NetLog{Message = "Message2"},
                new Log4NetLog{Message = "Message3"},
            };
            return View(logs);
        }

    }
}
