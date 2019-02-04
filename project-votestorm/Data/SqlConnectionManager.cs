using Dapper;
using System.Data;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;

namespace ProjectVotestorm.Data
{
    public class SqlConnectionManager
    {
        private string connectionString;

        public SqlConnectionManager(IConfiguration config)
        {
            connectionString = config.GetConnectionString("PrimaryDb");
        }

        public IDbConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }
    }
}
