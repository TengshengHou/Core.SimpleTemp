using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Core.SimpleTemp.Common.FilterExpression
{
    public class FilterModel
    {
        public string Field { get; set; }//过滤条件中使用的字段
        public string Action { get; set; }//过滤条件中的操作:==、!=等
        public string Logic { get; set; }//过滤条件之间的逻辑关系：AND和OR

        //过滤条件中的操作的值
        public object _value;
        public object Value
        {
            get
            {
                return this.DataSwitch();
            }
            set { _value = value; }
        }

        public string DataType { get; set; }//过滤条件中的操作的字段的类型
    }
}
