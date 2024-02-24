using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSPTPD.XuZhou.Sparksiiot.Http.Dto
{
    public class ResultDto<T>
    {
        public int Code { get; set; }

        public T Result { get; set; }
    }


    public class ResultDto : ResultDto<Dictionary<string,DataDto>>
    {

    }

}
