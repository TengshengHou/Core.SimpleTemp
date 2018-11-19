using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.SimpleTemp.Common
{
    public static class WebAppConfiguration
    {
        //登录有效时长
        //统一用此参数作为滑动时间单位分钟
        //1.票据 2.Cookie 3. SessionStore
        public static int TimeOutOfLogin = 1;
    }
}
