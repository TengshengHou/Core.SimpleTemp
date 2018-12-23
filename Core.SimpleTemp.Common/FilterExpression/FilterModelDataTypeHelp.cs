using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Core.SimpleTemp.Common.FilterExpression;
namespace Core.SimpleTemp.Common.FilterExpression
{
    /// <summary>
    /// 查询数据类型统一处理 
    /// 新类型请维护好此类
    /// </summary>
    public static class FilterModelDataTypeHelp
    {
        /// <summary>
        /// 根据DataType 转换为实际类型 
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        public static object DataSwitch(this FilterModel filterModel)
        {
            object retType;
            if (filterModel._value == null || string.IsNullOrEmpty(filterModel._value.ToString()))
            {
                return null;
            }
            switch (filterModel.DataType)
            {
                case "guid":
                    if (typeof(Newtonsoft.Json.Linq.JArray) == filterModel._value.GetType())
                        retType = (filterModel._value as Newtonsoft.Json.Linq.JArray).ToObject<List<Guid>>();
                    else
                        retType = Guid.Parse(filterModel._value.ToString());
                    break;
                case "string":
                    if (typeof(Newtonsoft.Json.Linq.JArray) == filterModel._value.GetType())
                        retType = (filterModel._value as Newtonsoft.Json.Linq.JArray).ToObject<List<string>>();
                    else
                        retType = filterModel._value;
                    break;
                case "int":
                    if (typeof(Newtonsoft.Json.Linq.JArray) == filterModel._value.GetType())
                        retType = (filterModel._value as Newtonsoft.Json.Linq.JArray).ToObject<List<int>>();
                    else
                        retType = Convert.ToInt32(filterModel._value.ToString());
                    break;
                case "datetime":
                    if (typeof(Newtonsoft.Json.Linq.JArray) == filterModel._value.GetType())
                        retType = (filterModel._value as Newtonsoft.Json.Linq.JArray).ToObject<List<DateTime>>();
                    else
                        retType = Convert.ToDateTime(filterModel._value.ToString());
                    break;
                default:
                    retType = filterModel._value;
                    break;
            }
            return retType;
        }

        public static Type RealityDataType(this FilterModel filterModel)
        {
            Type retType = typeof(string);
            switch (filterModel.DataType)
            {
                case "guid":
                    retType = typeof(Guid);

                    break;
                case "string":
                    retType = typeof(string);
                    break;
                case "int":
                    retType = typeof(int);
                    break;
                case "datetime":
                    retType = typeof(DateTime);
                    break;
                default:
                    retType = typeof(string);
                    break;
            }
            return retType;
        }

    }
}
