using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace SqlServerDataImportTool.DataCore
{
    public class CacheCore
    {
        private string dbFilePath = "\\DataBase\\log.db";
        private string connectionstring ="DataSource="+ AppDomain.CurrentDomain.BaseDirectory+"\\DataBase\\log.db;Pooling=true;FailIfMissing=false";
        public CacheCore()
        {
            
            //if (File.Exists(dbFilePath) == false)
            //{
            //    CreateDataBase();
            //}
        }
        private SQLiteConnection GetConnection() {

            return new SQLiteConnection(connectionstring);
        }
        #region CreateDataBase
        private void CreateTable()
        {
            using (var conn = GetConnection())
            {
                conn.Execute("CREATE TABLE \"log\" (\"id\"  INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,\"database\"  TEXT(30) NOT NULL,\"tablename\"  TEXT(100) NOT NULL,\"taskname\"  TEXT(200) NOT NULL,\"status\"  INTEGER);");
            }
        }
        private void CreateDataBase()
        {
            SQLiteConnection.CreateFile(dbFilePath);
            CreateTable();
        } 
        #endregion

        public void Clear()
        {
            using (var conn = GetConnection())
            {
                conn.Execute("delete from log");
            }
        }
        public void AddLog(string db, string tb, string taskname)
        {
            var sql = "insert into log (taskname,database,tablename,status) values (@taskname,@database,@tablename,@status)";
            using (var conn = GetConnection())
            {
                conn.Execute(sql, new { taskname = taskname, database = db, tablename = tb, status = 1 });
            }
        }
        public bool ExistTask(string taskname)
        {
            using (var conn = GetConnection())
            {
                var result= conn.Query<string>("select taskname from 'log' where taskname=@taskname limit 1", new { taskname }).FirstOrDefault();
                if (string.IsNullOrEmpty(result))
                {
                    return false;
                }
                else {
                    return true;
                }
            }
        }
        private IEnumerable<T> ExcuteQuery<T>(string sql)
        {
            using (var conn = GetConnection())
            {
                return conn.Query<T>(sql);
            }
        }
    }
}
