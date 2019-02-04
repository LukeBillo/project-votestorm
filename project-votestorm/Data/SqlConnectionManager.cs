using Dapper;
using System.Data;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;

namespace ProjectVotestorm.Data
{
    public class SqlConnectionManager
    {
        private readonly string _connectionString;

        public SqlConnectionManager(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("PrimaryDb");
        }

        public IDbConnection GetConnection()
        {
            return new MySqlConnection(_connectionString);
        }
    }
}
