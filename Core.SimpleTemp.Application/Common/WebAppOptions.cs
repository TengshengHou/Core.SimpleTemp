using System;
using System.Collections.Generic;
using System.Text;

namespace Core.SimpleTemp.Common
{
    public class WebAppOptions
    {
        //登录有效时长
        //统一用此参数作为滑动时间单位分钟
        //1.票据 2.Cookie 3. SessionStore
        public int TimeOutOfLogin { get; set; }
        /// <summary>
        /// 管理员登录名
        /// </summary>
        public string AdminLoginName { get; set; }
        /// <summary>
        /// 用户初始化密码
        /// </summary>
        public string InitialPassword { get; set; }


        #region Jwt
        public string JwtValidAudience { get; set; }
        public string JwtValidIssuer { get; set; }
        public string JwtIssuerSigningKey { get; set; }
        #endregion

        /// <summary>
        /// 文件上传保存路径
        /// </summary>
        public string FileSaveBasePath { get; set; }

    }
}
