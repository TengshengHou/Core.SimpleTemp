using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.SimpleTemp.Mvc.Models
{
    /// <summary>
    /// 用于返回Ajax请求数据格式
    /// </summary>
    public class AjaxJsonModel
    {
        /// <summary>
        /// 业务处理结果 成功/失败
        /// </summary>
        public string Result { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public object Data { get; set; }
    }
}
