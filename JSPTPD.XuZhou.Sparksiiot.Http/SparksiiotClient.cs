using JSPTPD.XuZhou.Sparksiiot.Http.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Serilog.Core;
using Serilog;

namespace JSPTPD.XuZhou.Sparksiiot.Http
{
    public class SparksiiotClient
    {

        string _baseUrl = "";

        string getToekn = "/login";
        string getData = "/subDevice/data/get";
        string getGatewayAttribute = "/gateway/attribute/get";
        string getDriverAttribute = "/subDevice/attribute/get";


        public SparksiiotClient(string baseUrl)
        {
            _baseUrl = baseUrl;
        }


        public async Task<DataDto> GetDriverDataAsync()
        {
            try
            {
                var str = await $"{_baseUrl}{getData}".GetStringAsync();
                var result = await $"{_baseUrl}{getData}".GetJsonAsync<ResultDto>();
                if (result.Code == 0)
                {
                    return result.Result["1"];
                }
            }
            catch (FlurlHttpException ex)
            {
                var strError = await ex.GetResponseStringAsync();
                Log.Error($"Error returned from {ex.Call.Request.RequestUri}: {strError}");
            }
            return null;
        }
    }
}
