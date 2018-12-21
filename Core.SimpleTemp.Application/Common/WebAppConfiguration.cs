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
        public static int TimeOutOfLogin = 30;
        /// <summary>
        /// 管理员登录名
        /// </summary>
        public static string AdminLoginName = "admin";
        /// <summary>
        /// 用户初始化密码
        /// </summary>
        public static string InitialPassword = "123456";


        #region Jwt
        public static string JwtValidAudience = "api";
        public static string JwtValidIssuer = "http://hts92.com";
        public static string JwtIssuerSigningKey = "I Love Net Core I Love Net Core I Love Net Core ";
        #endregion
    }
}
