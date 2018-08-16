using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using log4net;

namespace SqlServerDataImportTool.DataCore
{
    public abstract class BaseProvider : IDataProvider
    {
        protected ILog loger = LogManager.GetLogger("SqlServerDataImportTool", "Provider");
        protected string connectionstring;
        public BaseProvider(string _connectionstring)
        {
            connectionstring = _connectionstring;
        }
        public BaseProvider(string host, string user, string pwd)
        {
            connectionstring = string.Format(ConnectionTempate, host, user, pwd);
        }
        abstract protected string ConnectionTempate { get; }
        public abstract IDbConnection GetConnection();
        protected abstract string GetAllDbSql { get; }
        protected abstract string GetDataBaseTablesSql { get; }
        /// <summary>
        /// 统计SQL模板
        /// </summary>
        protected abstract string CountDataBaseTablesSql { get; }
        
        private IEnumerable<T> ExcuteQuery<T>(string sql)
        {
            using (var conn = GetConnection())
            {
                return conn.Query<T>(sql);
            }
        }
        public List<string> GetAllDataBaseName()
        {
            return ExcuteQuery<string>(GetAllDbSql).ToList();
            
        }
        public List<string> GetDabaseTables(string databasename)
        {
            var sql = string.Format(GetDataBaseTablesSql, databasename);
            return ExcuteQuery<string>(sql).ToList();
        }
        public int CountTableRecord(string databaseName, string tablename)
        {
            var sql = string.Format(CountDataBaseTablesSql, databaseName, tablename);
            return ExcuteQuery<int>(sql).First();
        }
    }
}
