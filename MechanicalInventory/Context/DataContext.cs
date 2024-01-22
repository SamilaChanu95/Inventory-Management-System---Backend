using Microsoft.Data.SqlClient;
using System.Data;

namespace MechanicalInventory.Context
{
    public class DataContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DataContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DBConnection");
        }

        public IDbConnection CreateDbConnection() => new SqlConnection(_connectionString);
    }
}
