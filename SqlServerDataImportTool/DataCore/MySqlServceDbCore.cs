using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MySql.Data.MySqlClient;

namespace SqlServerDataImportTool.DataCore
{
    public class MySqlServceDbCore : BaseProvider
    {
        public MySqlServceDbCore(string _connectionstring) : base(_connectionstring)
        {
        }

        public MySqlServceDbCore(string host, string user, string pwd) : base(host, user, pwd)
        {
        }

        protected override string ConnectionTempate => "server={0};database=ezp-base;uid={1};pwd={2};Connection Timeout=300;Charset=utf8;Pooling=True;";

        protected override string GetAllDbSql => "show databases";

        protected override string GetDataBaseTablesSql => "select table_name from information_schema.tables where table_schema='{0}' and table_type='base table';";

        protected override string CountDataBaseTablesSql => "select count(1) from `{0}`.`{1}`";

        protected string GetDatasTempate = "select * from `{0}`.`{1}` limit {2},{3}";// 

        public override IDbConnection GetConnection()
        {
            return new MySqlConnection(connectionstring);
        }
        public bool ExportDataFromTable(string database,string tablename,int startindex,int endindex,SqlServerDBCore sqlServercore)
        {
            using (var conn=GetConnection())
            {
                var sql = string.Format(GetDatasTempate, database, tablename, startindex, endindex);
                using (var reader = conn.ExecuteReader(sql, commandTimeout: 3000))
                {
                    return sqlServercore.ImportData(database, tablename, reader);
                }
            }
        }       
    }
}
