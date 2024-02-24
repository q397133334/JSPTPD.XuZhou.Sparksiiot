using FreeScheduler;
using NCrontab;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace JSPTPD.XuZhou.Sparksiiot.Client
{
    public static class Free
    {

        public static IFreeSql freeScheduler = new FreeSql.FreeSqlBuilder()
        .UseConnectionString(FreeSql.DataType.Sqlite, @"Data Source=freeScheduler.db")
        .UseMonitorCommand(cmd => Console.WriteLine($"Sql：{cmd.CommandText}"))//监听SQL语句
        .UseAutoSyncStructure(true) //自动同步实体结构到数据库，FreeSql不会扫描程序集，只有CRUD时才会生成表。
        .Build();


        public static IFreeSql MSSQL2005 = new FreeSql.FreeSqlBuilder()
        .UseConnectionString(FreeSql.DataType.SqlServer, $"Server=localhost;Database=SyncBase;Trusted_Connection=True;")
        .UseMonitorCommand(cmd => Console.WriteLine($"Sql：{cmd.CommandText}"))//监听SQL语句
        .UseAutoSyncStructure(false) //自动同步实体结构到数据库，FreeSql不会扫描程序集，只有CRUD时才会生成表。
        .Build();


        public static Scheduler scheduler = new FreeSchedulerBuilder()
        .OnExecuting(task =>
        {
            
            FreeSchedulerPipeLine.Process(task);
        }).UseCustomInterval(task =>
        {
            var i = task.Topic;
            var s = CrontabSchedule.Parse(task.IntervalArgument);
            var next = s.GetNextOccurrence(DateTime.Now);
            task.CreateTime = next;
            Debug.WriteLine($"next:{next}");
            Log.Information($"next:{next}");
            return next - DateTime.Now;
        })
        .UseTimeZone(TimeSpan.FromHours(8))
        .Build();
    }

    public interface IFreeSchedulePipeLienHandle
    {
        string Name { get; set; }
        void Executing(TaskInfo taskInfo);
    }

    public static class SchedulerEx
    {
        public static void AddScheduleHandle(this Scheduler scheduler, IFreeSchedulePipeLienHandle freeSchedulePipeLienHandle)
        {
            if (FreeSchedulerPipeLine.FreeSchedulePipeLienHandles.Where(q => q.Name == freeSchedulePipeLienHandle.Name).Count() > 0)
                return;
            FreeSchedulerPipeLine.FreeSchedulePipeLienHandles.Add(freeSchedulePipeLienHandle);
        }
    }

    public class FreeSchedulerPipeLine
    {

        public TaskInfo TaskInfo { get; set; }
        public static List<IFreeSchedulePipeLienHandle> FreeSchedulePipeLienHandles = new List<IFreeSchedulePipeLienHandle>();

        public FreeSchedulerPipeLine(TaskInfo taskInfo)
        {
            TaskInfo = taskInfo;
        }

        public static void Process(TaskInfo taskInfo)
        {
            foreach (var item in FreeSchedulePipeLienHandles.Where(q=>q.Name== taskInfo.Topic))
            {
                try
                {
                    item.Executing(taskInfo);
                }
                catch (Exception ex)
                {
                    Serilog.Log.Error($"Task Run error{taskInfo.Topic}{taskInfo.LastRunTime}", ex);
                }
            }

        }

    }
}
