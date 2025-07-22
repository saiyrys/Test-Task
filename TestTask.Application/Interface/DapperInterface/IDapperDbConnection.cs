using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask.Application.Interface.DapperInterface
{
    public interface IDapperDbConnection
    {
        IDbConnection CreateConnection();
    }
}
