using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alertsense.Lumberjack.Data.Repositories;
using AlertSense.Lumberjack.Contracts.Entities;
using AlertSense.Lumberjack.Contracts.Managers;
using AlertSense.Lumberjack.Contracts.Repositories;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;

namespace AlertSense.Lumberjack.Services.LogService
{
    public static class LogViewerManagerFactory
    {
        public static LogViewerManager CreateManager(ILogViewerRepository repository = null,
            IDbConnectionFactory connectionFactory = null)
        {
            if (repository == null)
                repository = new LogViewerRepository();

            if (connectionFactory == null && repository.DbFactory == null)
            {
                if (ConfigurationManager.ConnectionStrings["LoggingConnection"] == null)
                {
                    connectionFactory =
                        new OrmLiteConnectionFactory(
                            ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString,
                            SqlServerOrmLiteDialectProvider.Instance);
                    repository.DbFactory = connectionFactory;
                }
                else
                {
                    connectionFactory =
                        new OrmLiteConnectionFactory(
                            ConfigurationManager.ConnectionStrings["LoggingConnection"].ConnectionString,
                            SqlServerOrmLiteDialectProvider.Instance);

                    repository.DbFactory = connectionFactory;
                }
            }

            return new LogViewerManager
            {
                LogViewerRepo = repository
            };
        }
    }

    public class LogViewerManager : ILogViewerManager
    {
        public ILogViewerRepository LogViewerRepo { get; set; }

        public IEnumerable<AdoNetLog> GetAllLogs()
        {
            var logs = LogViewerRepo.GetAllLogs();
            return logs;
        }
    }
}
