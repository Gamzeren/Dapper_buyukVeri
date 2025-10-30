using System.Data;
using System.Data.SqlClient;

namespace Dapper_buyukVeri.Context
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("SqlConnection");
        }
        public string GetConnectionString() => _connectionString;
        public IDbConnection CreateConnection() {
            if (string.IsNullOrWhiteSpace(_connectionString))
            {
                throw new Exception("DapperContext: Connection string NULL/boş (veya sadece boşluk). appsettings.json 'ConnectionStrings:SqlConnection' kontrol edin.");
            }
            var builder = new SqlConnectionStringBuilder(_connectionString)
            {
                TrustServerCertificate = true
            };
            return new SqlConnection(builder.ConnectionString);
        }
    }
}
