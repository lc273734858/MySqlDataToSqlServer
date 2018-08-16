using SqlServerDataImportTool.DataCore;
using SqlServerDataImportTool.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using log4net;
using System.Diagnostics;

namespace SqlServerDataImportTool
{
    public class DataService
    {
        #region Fields
        SqlServerDBCore ServiceSQL;
        MySqlServceDbCore ServiceMySQL;
        CacheCore CacheService;
        Action<string> logerUI;
        private ILog loger = LogManager.GetLogger("SqlServerDataImportTool", "DataService");
        private bool needstop = false;
        private bool canStop = false;
        private int currentThreadCount = 0;
        private Form1 form1;
        private readonly int needThreadNum = 1;
        private Stopwatch stopWatch;
        public TaskInfo dbworkinfo { get; set; }
        /// <summary>
        /// 分页数据量
        /// </summary>
        private readonly int batchCount = 50000;
        //private static Queue<TaskItem> TaskQueue = new Queue<TaskItem>();
        private static List<Queue<TaskItem>> QueuePool = new List<Queue<TaskItem>>();
        #endregion

        #region Constructor
        public DataService(TaskInfo _info, Action<string> _loger, Form1 _form1)
        {
            stopWatch = new Stopwatch();
            batchCount = _info.BatchCount;
            needThreadNum = _info.NeedThread;
            form1 = _form1;
            dbworkinfo = _info;
            logerUI = _loger;
            ServiceSQL = new SqlServerDBCore(dbworkinfo.SqlServerHost, dbworkinfo.SqlServerUser, dbworkinfo.SqlServerPwd);
            ServiceMySQL = new MySqlServceDbCore(dbworkinfo.MySqlHost, dbworkinfo.MySqlUser, dbworkinfo.MySqlPWD);
            CacheService = new CacheCore();
        }
        public void StartWork()
        {
            stopWatch.Reset();
            stopWatch.Start();
            initQueuePool();

            ThreadPool.QueueUserWorkItem(StartWorkInThread);
        }
        #endregion

        #region Queue
        private void initQueuePool()
        {
            currentTurn = 0;
            QueuePool.Clear();
            for (int i = 0; i < needThreadNum; i++)
            {
                QueuePool.Add(new Queue<TaskItem>());
            }
        }
        private Queue<TaskItem> GetQueuePool(int index)
        {
            return QueuePool[index];
        }
        private static int currentTurn = 0;
        /// <summary>
        /// 根据排队算法获取队列
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private Queue<TaskItem> GetQueuePoolInTurn()
        {
            currentTurn++;
            if (currentTurn >= needThreadNum)
            {
                currentTurn = 0;
            }
            loger.Info($"当前获取的队列为:{currentTurn},needThreadNum{needThreadNum}");
            return GetQueuePool(currentTurn);
        }
        private int GetTaskCount()
        {
            return QueuePool.Sum(p => p.Count);
        }
        #endregion

        #region CheckResult
        internal void CheckCount()
        {
            ThreadPool.QueueUserWorkItem(CheckCountInThread);
        }
        internal void CheckCountInThread(object obj)
        {
            int count = 0;
            var sqlserverdblist = ServiceSQL.GetAllDataBaseName();
            var mysqldblist = ServiceMySQL.GetAllDataBaseName();
            var existdblist = MergeList(sqlserverdblist, mysqldblist);
            logerUI.Invoke("互相都存在的数据库:" + string.Join(",", existdblist));
            Action<string> checkcount = (dbname) => {
                count+=CheckCountDb(dbname);
            };
            if (dbworkinfo.MySqlDataBase.Equals("*"))
            {
                foreach (var db in existdblist)
                {
                    checkcount(db);
                }
            }
            else
            {
                foreach (var db in dbworkinfo.MySqlDataBase.Split(','))
                {
                    if (existdblist.Contains(db))
                    {
                        checkcount(db);
                    }
                    else
                    {
                        logerUI.Invoke($"数据库{db}两方数据库不匹配，不进行操作！");
                    }
                }
            }
            logerUI.Invoke($"总记录数：{count}");
            logerUI.Invoke("countover");
        }
        internal int CheckCountDb(string databasename)
        {
            int count = 0;
            logerUI.Invoke($"开始检查{databasename}");
            var sqlserverdblist = ServiceSQL.GetDabaseTables(databasename);
            //loger.Invoke(databasename+"在SqlServer中的表:" + string.Join(",", sqlserverdblist));
            var mysqldblist = ServiceMySQL.GetDabaseTables(databasename);
            //loger.Invoke("MySql数据库:" + string.Join(",", mysqldblist));
            var existtablelist = MergeList(sqlserverdblist, mysqldblist);
            //logerUI.Invoke("两边数据库中都存在的表:" + string.Join(",", existtablelist));
            StringBuilder errortable = new StringBuilder();
            StringBuilder successtable = new StringBuilder();
            errortable.AppendLine("所有不一致的表如下:");
            successtable.AppendLine("其中一致的表如下:");

            Action<string> checktable = (tbname) =>
            {
                if (CheckCounttable(databasename, tbname,ref count))
                {
                    successtable.AppendLine($"`{databasename}`.`{tbname}`");
                }
                else
                {
                    errortable.AppendLine($"`{databasename}`.`{tbname}`");
                }
            };
            if (string.IsNullOrEmpty(dbworkinfo.Tables) || dbworkinfo.Tables.Equals("*"))
            {
                foreach (var table in existtablelist)
                {
                    checktable(table);
                }
            }
            else
            {
                foreach (var table in dbworkinfo.Tables.Split(','))
                {
                    if (existtablelist.Contains(table))
                    {
                        checktable(table);
                    }
                    else
                    {
                        loger.Info($"数据库{databasename}-表{table}两方数据库不匹配，不进行操作！");
                    }
                }
            }
            logerUI.Invoke(errortable.ToString());
            loger.Info(errortable.ToString());
            loger.Info(successtable.ToString());
            return count;
        }
        internal bool CheckCounttable(string db, string tablename,ref int count)
        {
            var sqlcount = ServiceSQL.CountTableRecord(db, tablename);
            var mysqlcount = ServiceMySQL.CountTableRecord(db, tablename);
            count += mysqlcount;
            if (sqlcount == mysqlcount)
            {
                logerUI.Invoke($"数据库{db}，表{tablename}数据一致:{sqlcount}");
                return true;
            }
            else
            {
                logerUI.Invoke($"数据库{db}，表{tablename}数据不一致：MySql:{mysqlcount}  SqlServer:{sqlcount}");
                return false;
            }
        }
        #endregion

        #region ExportData
        private void StartWorkInThread(object obj)
        {
            needstop = false;
            canStop = false;
            var sqlserverdblist = ServiceSQL.GetAllDataBaseName();
            //logerUI.Invoke("Sqlserver数据库:" + string.Join(",", sqlserverdblist));
            var mysqldblist = ServiceMySQL.GetAllDataBaseName();
            //logerUI.Invoke("MySql数据库:" + string.Join(",", mysqldblist));
            var existdblist = MergeList(sqlserverdblist, mysqldblist);
            logerUI.Invoke("互相都存在的数据库:" + string.Join(",", existdblist));
            if (existdblist.Count <= 0)
            {
                logerUI.Invoke("不存在名称相同的数据库，任务停止");
                return;
            }
            bool begined = false;
            if (dbworkinfo.MySqlDataBase.Equals("*"))
            {
                foreach (var db in existdblist)
                {
                    ExportData(db);
                    if (begined == false)
                    {
                        begined = true;
                        BeginThreadPool();
                    }
                }
            }
            else
            {
                foreach (var db in dbworkinfo.MySqlDataBase.Split(','))
                {
                    if (existdblist.Contains(db))
                    {
                        ExportData(db);
                        if (begined == false)
                        {
                            begined = true;
                            BeginThreadPool();
                        }
                    }
                    else
                    {
                        logerUI.Invoke($"数据库{db}两方数据库不匹配，不进行操作！");
                    }
                }
            }
            canStop = true;
        }
        public void BeginThreadPool()
        {
            for (int i = 0; i < needThreadNum; i++)
            {
                currentThreadCount++;
                ThreadPool.QueueUserWorkItem(WorkInThread, i);
            }
            ThreadPool.QueueUserWorkItem(CheckStatus);
        }
        public void StopWork()
        {
            needstop = true;
            logerUI.Invoke($"等待当前线程最后一次任务执行完毕，正在停止.....");
        }
        public void ExportData(string databasename)
        {
            var sqlserverdblist = ServiceSQL.GetDabaseTables(databasename);
            //loger.Invoke(databasename+"在SqlServer中的表:" + string.Join(",", sqlserverdblist));
            var mysqldblist = ServiceMySQL.GetDabaseTables(databasename);
            //loger.Invoke("MySql数据库:" + string.Join(",", mysqldblist));
            var existtablelist = MergeList(sqlserverdblist, mysqldblist);
            //logerUI.Invoke("两边数据库中都存在的表:" + string.Join(",", existtablelist));
            loger.Info("两边数据库中都存在的表:" + string.Join(",", existtablelist));
            if (string.IsNullOrEmpty(dbworkinfo.Tables) || dbworkinfo.Tables.Equals("*"))
            {
                foreach (var table in existtablelist)
                {
                    AddTask(databasename, table);
                }
            }
            else
            {
                foreach (var table in dbworkinfo.Tables.Split(','))
                {
                    if (existtablelist.Contains(table)) { AddTask(databasename, table); }
                    else
                    {
                        //logerUI.Invoke($"数据库{databasename}-表{table}两方数据库不匹配，不进行操作！");
                        loger.Info($"数据库{databasename}-表{table}两方数据库不匹配，不进行操作！");
                    }
                }
            }
        }
        private void CheckStatus(object obj)
        {
            while (currentThreadCount > 0)
            {
                Thread.Sleep(5000);
                logerUI.Invoke($"当前线程数:{currentThreadCount},任务数:{GetTaskCount()}");
            }
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
            logerUI.Invoke($"线程总共执行时间{elapsedTime}");
            form1.EnableAllButton();
        }
        private void EndOneThread()
        {
            lock (lockobject)
            {
                currentThreadCount--;
            }
        }
        private string GenerateTaskName(TaskItem item)
        {
            return $"{item.DataBase}-{item.TableName}-{item.startindex}";
        }
        public void AddTask(string databasename, string tablename)
        {
            var queue = GetQueuePoolInTurn();
            int count = ServiceMySQL.CountTableRecord(databasename, tablename);
            int startindex = 0;
            while (count > 0)
            {
                var taskitem = new TaskItem(databasename, tablename, startindex, batchCount);
                var taskname = GenerateTaskName(taskitem);
                if (CacheService.ExistTask(taskname) == false)
                {
                    queue.Enqueue(taskitem);
                    logerUI.Invoke($"创建任务:{taskname}");
                }
                else
                {
                    logerUI.Invoke($"任务:{taskname} 已经执行完毕，跳过");
                }
                startindex += batchCount;
                count -= batchCount;
            }
        }
        private static object lockobject = new object();
        public void WorkInThread(object objIndex)
        {
            int index = (int)objIndex;
            var queue = GetQueuePool(index);
            bool go = true;
            while (go && needstop == false)
            {
                TaskItem tinfo = null;
                try
                {
                    tinfo = queue.Dequeue();
                }
                catch (Exception)
                {
                    if (canStop)
                    {
                        go = false;
                        logerUI.Invoke($"线程{Thread.CurrentThread.ManagedThreadId}-执行完毕");
                        break;
                    }
                    else
                    {
                        Thread.Sleep(1000);
                    }
                }
                if (tinfo != null)
                {
                    var taskname = GenerateTaskName(tinfo);
                    logerUI.Invoke($"开始执行:{taskname}");
                    var success = ServiceMySQL.ExportDataFromTable(tinfo.DataBase, tinfo.TableName, tinfo.startindex, tinfo.pagesize, ServiceSQL);
                    if (success)
                    {
                        //添加执行完毕的记录
                        CacheService.AddLog(tinfo.DataBase, tinfo.TableName, taskname);
                    }
                    logerUI.Invoke($"执行完毕{taskname}");
                }
            }
            EndOneThread();
        }

        public List<string> MergeList(List<string> leftList, List<string> rightList)
        {
            var result = new List<string>();
            foreach (var db in leftList)
            {
                if (rightList.Contains(db, StringComparer.OrdinalIgnoreCase))
                {
                    result.Add(db);
                }
            }
            return result;
        }
        #endregion
    }
}
