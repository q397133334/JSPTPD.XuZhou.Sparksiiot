using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Serilog;
using Serilog.Events;

namespace JSPTPD.XuZhou.Sparksiiot.Client
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var mainForm = new Main();

            Log.Logger = new LoggerConfiguration()
#if DEBUG
                .MinimumLevel.Information()
#else
                .MinimumLevel.Information()
#endif
                .Enrich.FromLogContext()
                .WriteTo.Async(c => c.File("Logs/logs.txt",
                rollingInterval: RollingInterval.Hour,
                retainedFileCountLimit: 10,
                rollOnFileSizeLimit: true))
                .WriteTo.Async(c => c.ToListView(mainForm.GetListView()))
#if DEBUG
                .WriteTo.Async(c => c.Console())
#endif
                .CreateLogger();


            Free.scheduler.AddTaskCustom("Sparksiiot37", "", "0,5,10,15,20,25,30,35,40,45,50,55 * * * *");
            Free.scheduler.AddTask("Sparksiiot37", "", -1, 10);
            //Free.scheduler.AddTask("36", "", -1, 1);
            //Free.scheduler.AddTask("ReStart", "", -1, 60);
            Free.scheduler.AddScheduleHandle(new SparksiiotScheduleHandle("Sparksiiot37", "http://172.16.15.37:8888"));
            //Free.scheduler.AddScheduleHandle(new SparksiiotScheduleHandle("36", "http://172.16.15.36:8888"));
            //Free.scheduler.AddScheduleHandle(new FreeScheduleHandle("ReStart", (info) =>
            //{
            //    Application.Restart();
            //}));
            Application.Run(mainForm);
        }
    }
}
