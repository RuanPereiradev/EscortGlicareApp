using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace GlicareApp.Repositories.DataConnection
{
    public class PostgreDbSession : IDisposable
    {
        public IDbConnection Connection { get; }
        private readonly IConfiguration _configuration;
        public IDbTransaction Transaction { get; set; }

        public PostgreDbSession(IConfiguration configuration)
        {
            _configuration = configuration;
            var connectionString = _configuration["ConnectionStrings:PostgreSQL"];
            Connection = new NpgsqlConnection(connectionString);
            Connection.Open();
        }
        public void Dispose()
        {
            Transaction?.Dispose();
            Connection?.Close();
            Connection?.Dispose();
        }
    }
}
