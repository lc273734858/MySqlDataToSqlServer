using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dapper;
using System.Data;

namespace SqlServerDataImportTool.DataCore
{
    public class SqlServerDBCore : BaseProvider
    {
        public const string GetAllDB = "SELECT name FROM  master..sysdatabases WHERE name NOT IN ( 'master', 'model', 'msdb', 'tempdb', 'northwind','pubs' )";

        protected override string ConnectionTempate => "Data Source={0};User ID={1};Pwd={2}";

        protected override string GetAllDbSql => GetAllDB;

        protected override string GetDataBaseTablesSql => "SELECT name FROM [{0}]..sysobjects Where xtype='U' ORDER BY name";

        protected override string CountDataBaseTablesSql => "select count(1) from [{0}].dbo.[{1}] with(nolock)";

        public SqlServerDBCore(string _connectionstring) : base(_connectionstring)
        {
        }

        public SqlServerDBCore(string host, string user, string pwd) : base(host, user, pwd)
        {
        }

        public override IDbConnection GetConnection()
        {
            return new SqlConnection(connectionstring);
        }

        public bool ImportData(string database, string tablename, IDataReader reader)
        {
            using (var destinationConnection = GetConnection() as SqlConnection)
            {
                destinationConnection.Open();

                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(destinationConnection))
                {
                    bulkCopy.BulkCopyTimeout = 300;
                    bulkCopy.DestinationTableName = string.Format("[{0}].dbo.[{1}]", database, tablename);
                    try
                    {
                        bulkCopy.WriteToServerAsync(reader).Wait();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.IndexOf("PRIMARY KEY", StringComparison.OrdinalIgnoreCase) > 0)
                        {
                            return true;
                        }
                        loger.Error($"数据库{database}-表{tablename}{ex.ToString()}");
                        return false;
                    }                    
                }
            }
        }
    }
}
