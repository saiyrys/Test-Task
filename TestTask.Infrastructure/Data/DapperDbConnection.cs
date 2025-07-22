using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTask.Application.Interface.DapperInterface;

namespace TestTask.Infrastructure.Data
{
    public class DapperDbConnection : IDapperDbConnection
    {
        private readonly IConfiguration _configuration;

        private readonly string _connectionString;

        public DapperDbConnection(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("SqlConnection");
        }

        public IDbConnection CreateConnection()
        {
            return new NpgsqlConnection(_connectionString);
        }
    }
}
