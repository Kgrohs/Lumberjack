using System.Data;
using AlertSense.Lumberjack.Contracts.Repositories;
using ServiceStack.Data;

namespace AlertSense.Lumberjack.Data.Repositories
{
    public abstract class BaseRepository : IRepositoryBase
    {
        public IDbConnectionFactory DbFactory { get; set; }
        private IDbConnection dbConnection;
        public IDbConnection DbConnection
        {
            get
            {
                if (dbConnection != null && dbConnection.State == ConnectionState.Closed)
                    dbConnection.Open();

                dbConnection = dbConnection ?? DbFactory.OpenDbConnection();

                return dbConnection;
            }
            set { dbConnection = value; }
        }
    }
}
