using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using AlertSense.Lumberjack.Contracts.Entities;
using AlertSense.Lumberjack.Contracts.Repositories;
using AlertSense.Lumberjack.Data.Repositories;
using ServiceStack.Logging;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.Dapper;
namespace Alertsense.Lumberjack.Data.Repositories
{
    public class LogViewerRepository : BaseRepository, ILogViewerRepository
    {
        ILog log = LogManager.GetLogger(typeof(LogViewerRepository));

        public IEnumerable<AdoNetLog> GetAllLogs()
        {
            try
            {
                var qryResults = DbConnection.Query<AdoNetLog>("SELECT TOP 50 * FROM Log ORDER BY Date DESC").ToList();
                return qryResults;
            }
            catch (SqlException e)
            {
                log.Error(e.Message, e);
                return new List<AdoNetLog>();
            }
        }
        public List<string> GetDistinctLoggersList()
        {
            try
            {
                var qryResults = DbConnection.Query<string>("SELECT DISTINCT Logger FROM Log").ToList();
                return qryResults;
            }
            catch (SqlException)
            {
                return new List<string>();
            }
        }

        public List<AdoNetLog> GetLogsByIds(List<string> ids)
        {
            try
            {
                if (ids == null || ids.Count < 1)
                {
                    return new List<AdoNetLog>();
                }

                var str = "SELECT * FROM HickoryLog WHERE ";
                for (var i = 0; i < ids.Count; i++)
                {
                    if (i != 0 && ids[i] != "")
                        str += "OR ";
                    if (ids[i] != "")
                        str += "id = " + ids[i] + " ";
                }
                var qryResults = DbConnection.Query<AdoNetLog>(str).ToList();

                return qryResults;
            }
            catch (SqlException)
            {
                return new List<AdoNetLog>();
            }
        }

        public int GetCount(
            string logLevel,
            DateTime? startDate,
            DateTime? endDate,
            string loggerType,
            int? regionId,
            int? userId,
            string thread
        )
        {
            string sqlCount = "Select count(*) as COUNT FROM dbo.HickoryLog";
            sqlCount += " WHERE Id > -1";
            if (!String.IsNullOrEmpty(logLevel)) sqlCount += " AND Level = @loglevel";
            if (startDate.HasValue)
            {
                sqlCount += " AND Date >= @startdate";
            }

            if (endDate.HasValue)
            {
                sqlCount += " AND Date <= @enddate";
            }
            if (!String.IsNullOrEmpty(thread)) sqlCount += " AND Thread = @thread";
            if (regionId > -1) sqlCount += " AND RegionId = @regionid";
            if (userId > -1) sqlCount += " AND UserId = @userid";
            if (!String.IsNullOrEmpty(loggerType)) sqlCount += " AND Logger = @loggertype";

            int count;
            try
            {
                var logsCount = DbConnection.Query<string>(
                    sqlCount,
                    new
                    {
                        loglevel = logLevel,
                        startdate = startDate.HasValue ? startDate.Value.ToString(CultureInfo.InvariantCulture) : null,
                        enddate =
                            endDate.HasValue ? endDate.Value.AddSeconds(1).ToString(CultureInfo.InvariantCulture) : null,
                        thread,
                        regionid = regionId,
                        userid = userId,
                        loggertype = loggerType
                    }
                    );

                var countString = logsCount.First();

                int.TryParse(countString, out count);
            }
            catch (SqlException)
            {
                count = 0;
                throw;
            }

            return count;
        }

        public List<AdoNetLog> PagedFilteredLogs(
            string logLevel,
            DateTime? startDate,
            DateTime? endDate,
            string loggerType,
            int? regionId,
            int? userId,
            string thread,
            int pageNumber,
            int pageSize
        )
        {
            var emptyList = new List<AdoNetLog>();

            if (pageNumber < 1 || pageSize < 1)
                return emptyList;

            string sql =
                "DECLARE @PageNumber AS INT, @RowspPage AS INT" +
                    " SET @PageNumber = @pageno" +
                    " SET @RowspPage = @pagesize" +
                    " Select * from (" +
                        " SELECT ROW_NUMBER() OVER(ORDER BY Id desc) AS NUMBER," +
                        " * FROM dbo.HickoryLog";
            sql += " WHERE Id > -1";
            if (!String.IsNullOrEmpty(logLevel)) sql += " AND Level = @loglevel";
            if (startDate.HasValue)
            {
                sql += " AND Date >= @startdate";
            }

            if (endDate.HasValue)
            {
                sql += " AND Date < @enddate";
            }
            if (!String.IsNullOrEmpty(thread)) sql += " AND Thread = @thread";
            if (regionId > -1) sql += " AND RegionId = @regionid";
            if (userId > -1) sql += " AND UserId = @userid";
            if (!String.IsNullOrEmpty(loggerType)) sql += " AND Logger = @loggertype";
            sql += " ) AS TBL";
            sql += " WHERE NUMBER BETWEEN ((@PageNumber - 1) * @RowspPage + 1) AND (@PageNumber * @RowspPage)";
            sql += " order by Id desc";
            try
            {
                var logs = DbConnection.Query<AdoNetLog>(
                    sql,
                    new
                    {
                        pageno = pageNumber,
                        pagesize = pageSize,
                        loglevel = logLevel,
                        startdate = startDate.HasValue ? startDate.Value.ToString(CultureInfo.InvariantCulture) : null,
                        enddate =
                            endDate.HasValue ? endDate.Value.AddSeconds(1).ToString(CultureInfo.InvariantCulture) : null,
                        thread,
                        regionid = regionId,
                        userid = userId,
                        loggertype = loggerType
                    }
                    );
                return logs.ToList();
            }
            catch (SqlException)
            {
                return emptyList;
            }
        }

        public bool ColumnExists(string columnName)
        {
            if (String.IsNullOrEmpty(columnName))
                return false;

            var qryString = "Select top 1 COL_LENGTH('HickoryLog', @ColumnName) from dbo.HickoryLog";
            try
            {
                var qryResults = DbConnection.Query<string>(qryString, new { ColumnName = columnName });
                var colLength = qryResults.First();
                return colLength != null;
            }
            catch (SqlException)
            {

                return false;
            }
        }
    }


}
