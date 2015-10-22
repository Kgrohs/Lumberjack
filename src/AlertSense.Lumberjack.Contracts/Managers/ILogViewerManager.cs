using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlertSense.Lumberjack.Contracts.Entities;
using AlertSense.Lumberjack.Contracts.Repositories;

namespace AlertSense.Lumberjack.Contracts.Managers
{
    public  interface ILogViewerManager
    {
        IEnumerable<AdoNetLog> GetAllLogs();
    }
}
