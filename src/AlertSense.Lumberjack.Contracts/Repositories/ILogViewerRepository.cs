using System;
using System.Collections.Generic;
using AlertSense.Lumberjack.Contracts.Entities;
using ServiceStack.Data;


namespace AlertSense.Lumberjack.Contracts.Repositories
{
    public interface ILogViewerRepository : IRepositoryBase
    {
        IDbConnectionFactory DbFactory { get; set; }

        List<string> GetDistinctLoggersList();

        List<AdoNetLog> GetLogsByIds(List<string> ids);

        int GetCount(
            string logLevel,
            DateTime? startDate,
            DateTime? endDate,
            string loggerType,
            int? regionId,
            int? userId,
            string thread
        );

        List<AdoNetLog> PagedFilteredLogs(
            string logLevel,
            DateTime? startDate,
            DateTime? endDate,
            string loggerType,
            int? regionId,
            int? userId,
            string thread,
            int pageNumber,
            int pageSize
        );

        bool ColumnExists(string columnName);
        IEnumerable<AdoNetLog> GetAllLogs();
    }
}
