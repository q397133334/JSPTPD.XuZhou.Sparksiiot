namespace JSPTPD.XuZhou.Sparksiiot.Client
{
    partial class Main
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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lvLog = new System.Windows.Forms.ListView();
            this.headerTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.headerInfo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.Setting = new System.Windows.Forms.ToolStripMenuItem();
            this.AutoStart = new System.Windows.Forms.ToolStripMenuItem();
            this.reApp = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lvLog);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 425);
            this.panel1.TabIndex = 0;
            // 
            // lvLog
            // 
            this.lvLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.headerTime,
            this.headerInfo});
            this.lvLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvLog.HideSelection = false;
            this.lvLog.Location = new System.Drawing.Point(0, 0);
            this.lvLog.Name = "lvLog";
            this.lvLog.Size = new System.Drawing.Size(800, 425);
            this.lvLog.TabIndex = 0;
            this.lvLog.UseCompatibleStateImageBehavior = false;
            this.lvLog.View = System.Windows.Forms.View.Details;
            // 
            // headerTime
            // 
            this.headerTime.Text = "时间";
            this.headerTime.Width = 150;
            // 
            // headerInfo
            // 
            this.headerInfo.Text = "信息";
            this.headerInfo.Width = 771;
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Setting});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 25);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // Setting
            // 
            this.Setting.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AutoStart,
            this.reApp});
            this.Setting.Name = "Setting";
            this.Setting.Size = new System.Drawing.Size(44, 21);
            this.Setting.Text = "设置";
            // 
            // AutoStart
            // 
            this.AutoStart.Name = "AutoStart";
            this.AutoStart.ShowShortcutKeys = false;
            this.AutoStart.Size = new System.Drawing.Size(180, 22);
            this.AutoStart.Text = "开机启动";
            this.AutoStart.Click += new System.EventHandler(this.AutoStart_Click);
            // 
            // reApp
            // 
            this.reApp.Name = "reApp";
            this.reApp.Size = new System.Drawing.Size(180, 22);
            this.reApp.Text = "重启软件";
            this.reApp.Click += new System.EventHandler(this.reApp_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Main";
            this.Text = "徐州供排水监测站数据采集软件";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Main_FormClosed);
            this.Load += new System.EventHandler(this.Main_Load);
            this.panel1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListView lvLog;
        private System.Windows.Forms.ColumnHeader headerTime;
        private System.Windows.Forms.ColumnHeader headerInfo;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem Setting;
        private System.Windows.Forms.ToolStripMenuItem AutoStart;
        private System.Windows.Forms.ToolStripMenuItem reApp;
    }
}

