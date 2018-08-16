namespace SqlServerDataImportTool
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_Start = new System.Windows.Forms.Button();
            this.txtMySqlHost = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtMySqlDataBase = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtMySqlPWD = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMySqlUser = new System.Windows.Forms.TextBox();
            this.lableUser = new System.Windows.Forms.Label();
            this.txtTables = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtSqlServerPWD = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSqlServerUser = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtSqlServerHost = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btn_Stop = new System.Windows.Forms.Button();
            this.txtMsg = new System.Windows.Forms.RichTextBox();
            this.btn_Check = new System.Windows.Forms.Button();
            this.btn_Clear = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_Start
            // 
            this.btn_Start.Location = new System.Drawing.Point(345, 254);
            this.btn_Start.Name = "btn_Start";
            this.btn_Start.Size = new System.Drawing.Size(75, 23);
            this.btn_Start.TabIndex = 0;
            this.btn_Start.Text = "开始";
            this.btn_Start.UseVisualStyleBackColor = true;
            this.btn_Start.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtMySqlHost
            // 
            this.txtMySqlHost.Location = new System.Drawing.Point(139, 20);
            this.txtMySqlHost.Name = "txtMySqlHost";
            this.txtMySqlHost.Size = new System.Drawing.Size(257, 21);
            this.txtMySqlHost.TabIndex = 1;
            this.txtMySqlHost.Text = "192.168.12.41";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "Host";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.txtMySqlDataBase);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtMySqlPWD);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtMySqlUser);
            this.groupBox1.Controls.Add(this.lableUser);
            this.groupBox1.Controls.Add(this.txtMySqlHost);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(432, 170);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "MySql";
            // 
            // txtMySqlDataBase
            // 
            this.txtMySqlDataBase.Location = new System.Drawing.Point(139, 133);
            this.txtMySqlDataBase.Name = "txtMySqlDataBase";
            this.txtMySqlDataBase.Size = new System.Drawing.Size(257, 21);
            this.txtMySqlDataBase.TabIndex = 3;
            this.txtMySqlDataBase.Text = "*";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(40, 136);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "database";
            // 
            // txtMySqlPWD
            // 
            this.txtMySqlPWD.Location = new System.Drawing.Point(139, 94);
            this.txtMySqlPWD.Name = "txtMySqlPWD";
            this.txtMySqlPWD.PasswordChar = '1';
            this.txtMySqlPWD.Size = new System.Drawing.Size(257, 21);
            this.txtMySqlPWD.TabIndex = 3;
            this.txtMySqlPWD.Text = "123456";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(40, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "password";
            // 
            // txtMySqlUser
            // 
            this.txtMySqlUser.Location = new System.Drawing.Point(139, 57);
            this.txtMySqlUser.Name = "txtMySqlUser";
            this.txtMySqlUser.Size = new System.Drawing.Size(257, 21);
            this.txtMySqlUser.TabIndex = 3;
            this.txtMySqlUser.Text = "root";
            // 
            // lableUser
            // 
            this.lableUser.AutoSize = true;
            this.lableUser.Location = new System.Drawing.Point(40, 60);
            this.lableUser.Name = "lableUser";
            this.lableUser.Size = new System.Drawing.Size(29, 12);
            this.lableUser.TabIndex = 4;
            this.lableUser.Text = "User";
            // 
            // txtTables
            // 
            this.txtTables.AcceptsReturn = true;
            this.txtTables.AcceptsTab = true;
            this.txtTables.AllowDrop = true;
            this.txtTables.Location = new System.Drawing.Point(151, 188);
            this.txtTables.Multiline = true;
            this.txtTables.Name = "txtTables";
            this.txtTables.Size = new System.Drawing.Size(704, 51);
            this.txtTables.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(52, 205);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "tables";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.txtSqlServerPWD);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtSqlServerUser);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtSqlServerHost);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Location = new System.Drawing.Point(459, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(432, 139);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "SqlServer";
            // 
            // txtSqlServerPWD
            // 
            this.txtSqlServerPWD.Location = new System.Drawing.Point(139, 94);
            this.txtSqlServerPWD.Name = "txtSqlServerPWD";
            this.txtSqlServerPWD.PasswordChar = '1';
            this.txtSqlServerPWD.Size = new System.Drawing.Size(257, 21);
            this.txtSqlServerPWD.TabIndex = 3;
            this.txtSqlServerPWD.Text = "YCHXBd@F@v2hVe";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(40, 97);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "password";
            // 
            // txtSqlServerUser
            // 
            this.txtSqlServerUser.Location = new System.Drawing.Point(139, 57);
            this.txtSqlServerUser.Name = "txtSqlServerUser";
            this.txtSqlServerUser.Size = new System.Drawing.Size(257, 21);
            this.txtSqlServerUser.TabIndex = 3;
            this.txtSqlServerUser.Text = "sa";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(40, 60);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 4;
            this.label6.Text = "User";
            // 
            // txtSqlServerHost
            // 
            this.txtSqlServerHost.Location = new System.Drawing.Point(139, 20);
            this.txtSqlServerHost.Name = "txtSqlServerHost";
            this.txtSqlServerHost.Size = new System.Drawing.Size(257, 21);
            this.txtSqlServerHost.TabIndex = 1;
            this.txtSqlServerHost.Text = "192.168.12.54";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(40, 23);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 2;
            this.label7.Text = "Host";
            // 
            // btn_Stop
            // 
            this.btn_Stop.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btn_Stop.Location = new System.Drawing.Point(514, 253);
            this.btn_Stop.Name = "btn_Stop";
            this.btn_Stop.Size = new System.Drawing.Size(75, 23);
            this.btn_Stop.TabIndex = 9;
            this.btn_Stop.Text = "停止";
            this.btn_Stop.UseVisualStyleBackColor = true;
            this.btn_Stop.Click += new System.EventHandler(this.button2_Click);
            // 
            // txtMsg
            // 
            this.txtMsg.AcceptsTab = true;
            this.txtMsg.AutoWordSelection = true;
            this.txtMsg.EnableAutoDragDrop = true;
            this.txtMsg.Location = new System.Drawing.Point(13, 289);
            this.txtMsg.Name = "txtMsg";
            this.txtMsg.ShowSelectionMargin = true;
            this.txtMsg.Size = new System.Drawing.Size(890, 260);
            this.txtMsg.TabIndex = 10;
            this.txtMsg.Text = "";
            // 
            // btn_Check
            // 
            this.btn_Check.Location = new System.Drawing.Point(695, 252);
            this.btn_Check.Name = "btn_Check";
            this.btn_Check.Size = new System.Drawing.Size(132, 23);
            this.btn_Check.TabIndex = 11;
            this.btn_Check.Text = "检查结果";
            this.btn_Check.UseVisualStyleBackColor = true;
            this.btn_Check.Click += new System.EventHandler(this.button3_Click);
            // 
            // btn_Clear
            // 
            this.btn_Clear.Location = new System.Drawing.Point(115, 254);
            this.btn_Clear.Name = "btn_Clear";
            this.btn_Clear.Size = new System.Drawing.Size(113, 23);
            this.btn_Clear.TabIndex = 12;
            this.btn_Clear.Text = "清空工具缓存";
            this.btn_Clear.UseVisualStyleBackColor = true;
            this.btn_Clear.Click += new System.EventHandler(this.button4_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(915, 561);
            this.Controls.Add(this.btn_Clear);
            this.Controls.Add(this.btn_Check);
            this.Controls.Add(this.txtMsg);
            this.Controls.Add(this.btn_Stop);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.txtTables);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_Start);
            this.Name = "Form1";
            this.Text = "从MySql导数据到SqlServer工具";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Start;
        private System.Windows.Forms.TextBox txtMySqlHost;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtMySqlPWD;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMySqlUser;
        private System.Windows.Forms.Label lableUser;
        private System.Windows.Forms.TextBox txtTables;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtSqlServerPWD;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSqlServerUser;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtSqlServerHost;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btn_Stop;
        private System.Windows.Forms.TextBox txtMySqlDataBase;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox txtMsg;
        private System.Windows.Forms.Button btn_Check;
        private System.Windows.Forms.Button btn_Clear;
    }
}

