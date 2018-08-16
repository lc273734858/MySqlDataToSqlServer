using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlServerDataImportTool.Modal
{
    public class TaskItem
    {
        public string DataBase { get; set; }
        public string TableName { get; set; }
        public int startindex { get; set; }
        public int pagesize { get; set; }
        public TaskItem(string d, string t,int s,int e)
        {
            DataBase = d;
            TableName = t;
            startindex = s;
            pagesize = e;
        }
    }
}
