using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlServerDataImportTool.Modal
{
    public class TaskInfo
    {
        public string MySqlHost { get; set; }
        public string MySqlUser { get; set; }
        public string MySqlPWD { get; set; }
        public string MySqlDataBase { get; set; }
        public string SqlServerHost { get; set; }
        public string SqlServerUser { get; set; }
        public string SqlServerPwd { get; set; }
        public string Tables { get; internal set; }
        public int NeedThread { get; set; }
        public int BatchCount { get; set; }

    }
}
