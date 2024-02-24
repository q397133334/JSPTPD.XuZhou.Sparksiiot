using Serilog.Configuration;
using Serilog.Core;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Configuration.Internal;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Serilog
{
    public class SerilogListView : ILogEventSink
    {

        private ListView _listView;

        public SerilogListView(ListView lv)
        {
            _listView = lv;
        }

        public void Emit(LogEvent logEvent)
        {
            _listView.Invoke(new Action(() =>
            {
                if (_listView.Items.Count > 500)
                {
                    _listView.Items.Clear();
                    //System.GC.Collect();
                }
                try
                {
                    //this._listView.BeginUpdate();
                    ListViewItem listViewItem = new ListViewItem();
                    listViewItem.Tag = null;
                    listViewItem.Text = DateTime.Now.ToString(_listView.Items.Count + ":yyyy-MM-dd HH:mm:ss");
                    listViewItem.SubItems.Add(logEvent.MessageTemplate.ToString());
                    if (this._listView.Items == null || this._listView.Items.Count <= 0)
                    {
                        this._listView.Items.Add(listViewItem);
                    }
                    else
                    {
                        this._listView.Items.Insert(0, listViewItem);
                    }
                    //this._listView.EndUpdate();
                }
                catch (Exception ex)
                {
                    Serilog.Log.Error("AddListViewLog", ex);
                }
            }));
        }
    }

    public static class LoggerConfigurationListViewExtensions
    {
        public static LoggerConfiguration ToListView(this LoggerSinkConfiguration LoggerSinkConfiguration, ListView listView)
        {
            ILogEventSink logEventSink = new SerilogListView(listView);
            return LoggerSinkConfiguration.Sink(logEventSink, Serilog.Events.LogEventLevel.Debug, null);
        }
    }
}
