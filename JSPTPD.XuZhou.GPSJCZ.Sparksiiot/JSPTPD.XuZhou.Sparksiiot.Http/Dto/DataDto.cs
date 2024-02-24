using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSPTPD.XuZhou.Sparksiiot.Http.Dto
{
    public class DataDto
    {
        public string DeviceId { get; set; }

        public string GatewayMac { get; set; }

        public string ServerId { get; set; }

        public long Time { get; set; }

        public Dictionary<string, object> Params { get; set; }
    }
        
}
