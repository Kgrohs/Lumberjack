using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlertSense.Lumberjack.Contracts.Entities;
using AlertSense.Lumberjack.Contracts.Managers;
using AlertSense.Lumberjack.Services.LogService;
using log4net;
using LogViewer.Models;

namespace LogViewer.Controllers
{
    public class LogViewerController : Controller
    {
        private ILogViewerManager _logViewerManager;
        public ILogViewerManager LogViewerManager
        {
            get
            {
                return _logViewerManager ?? (_logViewerManager = LogViewerManagerFactory.CreateManager());
            }
            set { _logViewerManager = value; }
        }

        public ActionResult Index()
        {
            ILog log = LogManager.GetLogger(typeof(LogViewerController));
            log.Debug("Loading LogViewer Controller.");

            var logs = LogViewerManager.GetAllLogs();
            return View(logs);
        }

    }
}
