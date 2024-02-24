using FreeScheduler;
using Microsoft.Win32;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JSPTPD.XuZhou.Sparksiiot.Client
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }
        public ListView GetListView()
        {
            return this.lvLog;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            Text += Application.ProductVersion;
            AutoStart.Checked = isAutoStart();
            DoubleBufferedListView(lvLog, false);
        }

        private void DoubleBufferedListView(ListView lv, bool flag)
        {
            Type lvType = lv.GetType();
            PropertyInfo pi = lvType.GetProperty("DoubleBuffered",
                BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(lv, flag, null);
        }


        /// <summary>
        /// 添加界面日志信息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="objectTag"></param>
        /// <param name="logType"></param>
        private void AddListViewLog(string message, object objectTag = null, int logType = 1)
        {
            this.Invoke(new Action(() =>
            {
                if (lvLog.Items.Count > 50000)
                {
                    lvLog.Items.Clear();
                }
                try
                {
                    this.lvLog.BeginUpdate();
                    ListViewItem listViewItem = new ListViewItem();
                    listViewItem.Tag = objectTag;
                    listViewItem.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    listViewItem.SubItems.Add(message);
                    if (this.lvLog.Items == null || this.lvLog.Items.Count <= 0)
                    {
                        this.lvLog.Items.Add(listViewItem);
                    }
                    else
                    {
                        this.lvLog.Items.Insert(0, listViewItem);
                    }
                    this.lvLog.EndUpdate();
                }
                catch (Exception ex)
                {
                    Serilog.Log.Error("AddListViewLog", ex);
                }
            }));
        }

        private void AutoStart_Click(object sender, EventArgs e)
        {

            if (isAutoStart())
            {
                setAutoStart(false);
            }
            else
            {
                setAutoStart(true);
            }

            (sender as ToolStripMenuItem).Checked = isAutoStart();

        }

        bool isAutoStart()
        {
            try
            {
                var productName = System.Configuration.ConfigurationManager.AppSettings["ProductName"];
                RegistryKey reg = Registry.LocalMachine;
                RegistryKey software = reg.OpenSubKey(@"SOFTWARE");
                RegistryKey run = reg.OpenSubKey(@"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run\");
                object key = run.GetValue(productName);
                software.Close();
                run.Close();
                if (null == key || !Application.ExecutablePath.Equals(key.ToString()))
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Log.Error("检测是否已设置开机重启出现异常", ex);
                MessageBox.Show(ex.Message, "提示");
                return false;
            }
        }

        bool setAutoStart(bool start = true)
        {
            RegistryKey R_local = Registry.LocalMachine;
            RegistryKey R_run = R_local.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
            var productName = System.Configuration.ConfigurationManager.AppSettings["ProductName"];
#if DEBUG
            { R_run.DeleteValue(productName, false); }
#else
            try
            {
                if (start == true)
                {
                    R_run.SetValue(productName, Application.ExecutablePath);
                }
                else
                {
                    R_run.DeleteValue(productName, false);
                }
            }
            catch (Exception ex)
            {
                Log.Error("设置开机启动异常", ex);
                MessageBox.Show(ex.Message, "提示");
            }
#endif
            try
            {
                R_run.Close();
                R_local.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Free.scheduler.Dispose();
            Application.ExitThread();
            Application.Exit();
        }

        private void reApp_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
    }
}
