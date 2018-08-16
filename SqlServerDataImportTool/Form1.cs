using SqlServerDataImportTool.DataCore;
using SqlServerDataImportTool.Modal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SqlServerDataImportTool
{
    public partial class Form1 : Form
    {
        DataService service;
        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DisableAllButton();
                InitService();
                service.StartWork();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void InitService()
        {      
            TaskInfo info = new TaskInfo();
            info.MySqlHost = txtMySqlHost.Text;
            info.MySqlUser = txtMySqlUser.Text;
            info.MySqlPWD = txtMySqlPWD.Text;
            info.MySqlDataBase = txtMySqlDataBase.Text;
            info.SqlServerHost = txtSqlServerHost.Text;
            info.SqlServerUser = txtSqlServerUser.Text;
            info.SqlServerPwd = txtSqlServerPWD.Text;
            info.Tables = txtTables.Text;
            info.NeedThread= int.Parse(ConfigurationManager.AppSettings.Get("thread"));
            info.BatchCount= int.Parse(ConfigurationManager.AppSettings.Get("BatchCount"));
            if (service == null)
            {
                service = new DataService(info, LogWrite, this);
            }
            else
            {
                service.dbworkinfo = info;
            }

        }
        private void LogWrite(string msg)
        {
            if (msg == "countover")
            {
                EnableAllButton();
            }
            else
            {
                txtMsg.AppendText(msg + "\r\n");
            }
        }
        public void EnableAllButton()
        {
            btn_Start.Enabled = true;
            btn_Stop.Enabled = true;
            btn_Clear.Enabled = true;
            btn_Check.Enabled = true;
        }
        private void DisableAllButton()
        {
            btn_Start.Enabled = false;
            btn_Stop.Enabled = false;
            btn_Clear.Enabled = false;
            btn_Check.Enabled = false;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            DisableAllButton();
            service.StopWork();
            LogWrite("正在停止....");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DisableAllButton();
            InitService();
            service.CheckCount();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var logcache = new CacheCore();
            logcache.Clear();
            MessageBox.Show("成功清理缓存");
        }
    }
}
