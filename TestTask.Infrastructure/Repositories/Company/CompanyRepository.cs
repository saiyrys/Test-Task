using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTask.Application.Interface.DapperInterface;
using TestTask.Domain.IRepository.Company;

namespace TestTask.Infrastructure.Repositories.Company
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly IDapperDbConnection _dapperConnection;

        public CompanyRepository(IDapperDbConnection dapperConnection)
        {
            _dapperConnection = dapperConnection;
        }
        public async Task<bool> ExistAsync(int companyId)
        {
            var connection = _dapperConnection.CreateConnection();

            const string sql = @"SELECT COUNT(1) FROM companies WHERE id = @id";
            var count = await connection.ExecuteScalarAsync<int>(sql, new { id = companyId });

            return count > 0;

        }
    }
}
