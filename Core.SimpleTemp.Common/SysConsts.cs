using System;
using System.Collections.Generic;
using System.Text;

namespace Core.SimpleTemp.Common
{
    public static class SysConsts
    {
        public const string AJAX_RESULT_SUCCESS = "Success";
        public const string AJAX_RESULT_FAILD = "Faild";


        #region CacheKey

        /// <summary>
        /// 用户登录后保存用户权限缓存KEY前缀（前缀+ID）
        /// </summary>
        public const string MENU_CACHEKEY_PREFIX = "MENU_CACHEKEY";

        #endregion
    }
}
