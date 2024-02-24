using FreeScheduler;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JSPTPD.XuZhou.Sparksiiot.Http;

namespace JSPTPD.XuZhou.Sparksiiot.Client
{
    public class SparksiiotScheduleHandle : IFreeSchedulePipeLienHandle
    {
        public string Name { get; set; } = "Sparksiiot";

        public SparksiiotClient SparksiiotClient { get; set; }

        public SparksiiotScheduleHandle(string address)
        {
            SparksiiotClient = new SparksiiotClient(address);
        }

        public SparksiiotScheduleHandle(string name, string address)
        {
            Name = name;
            SparksiiotClient = new SparksiiotClient(address);
        }

        public void Executing(TaskInfo taskInfo)
        {
            var data = SparksiiotClient.GetDriverDataAsync().Result;
            if(taskInfo.LastRunTime==null)
            {
                taskInfo.LastRunTime = taskInfo.CreateTime;
            }
            foreach (var item in data.Params)
            {

                try
                {
                    Log.Information(Free.MSSQL2005.Select<object>().AsTable((a, b) => { return "HISDATATEMP"; }).Where("TAGINDEX=" + item.Key).ToSql());
                    if (Free.MSSQL2005.Select<object>().AsTable((a, b) => { return "HISDATATEMP"; }).Where("TAGINDEX="+ item.Key).Count()==0)
                    {
                        var dic = new Dictionary<string, object>();
                        dic.Add("TAGINDEX", int.Parse(item.Key));
                        dic.Add("TAGVAL", item.Value);
                        dic.Add("TAGSTATUS", 0);
                        dic.Add("TAGTIME", taskInfo.LastRunTime.AddMinutes(-5));
                        dic.Add("TAGENDTIME", taskInfo.LastRunTime);
                        Log.Information(Free.MSSQL2005.InsertDict(dic).AsTable($"HISDATATEMP").ToSql());
                        Free.MSSQL2005.InsertDict(dic).AsTable($"HISDATATEMP").ExecuteAffrows();
                    }
                    else
                    {
                        var dic = new Dictionary<string, object>();
                        dic.Add("TAGINDEX", int.Parse(item.Key));
                        dic.Add("TAGVAL", item.Value);
                        dic.Add("TAGSTATUS", 0);
                        dic.Add("TAGTIME", taskInfo.LastRunTime.AddMinutes(-5));
                        dic.Add("TAGENDTIME", taskInfo.LastRunTime);
                        Log.Information(Free.MSSQL2005.UpdateDict(dic).AsTable($"HISDATATEMP").WherePrimary("TAGINDEX").ToSql());
                        Free.MSSQL2005.UpdateDict(dic).AsTable($"HISDATATEMP").WherePrimary("TAGINDEX").ExecuteAffrows();
                    }
                
                }
                catch (Exception ex)
                {
                    Log.Information(ex.Message);
                }
            }
            Log.Information(Name + ":" + Newtonsoft.Json.JsonConvert.SerializeObject(data));
        }
    }

    public class FreeScheduleHandle : IFreeSchedulePipeLienHandle
    {
        public string Name { get; set; }

        Action<TaskInfo> _excuting;

        public FreeScheduleHandle(string name, Action<TaskInfo> excuting)
        {
            Name = name;
            _excuting = excuting;
        }

        public void Executing(TaskInfo taskInfo)
        {
            _excuting.Invoke(taskInfo);
        }
    }
}
