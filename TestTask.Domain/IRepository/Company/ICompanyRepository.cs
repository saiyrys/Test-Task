using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask.Domain.IRepository.Company
{
    public interface ICompanyRepository
    {
        Task<bool> ExistAsync(int companyId);
    }
}
