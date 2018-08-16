using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SqlServerDataImportTool.DataCore
{
    public interface IDataProvider
    {
        IDbConnection GetConnection();
        List<string> GetAllDataBaseName();
    }
}
