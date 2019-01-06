using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.SimpleTemp.Mvc.TagHelpers
{
    [HtmlTargetElement("input", Attributes = DataActionForAttributeName)]
    [HtmlTargetElement("input", Attributes = DataLogicForAttributeName)]
    [HtmlTargetElement("input", Attributes = DataDataTypeForAttributeName)]
    public class CoreInputTagHelper : TagHelper
    {
        private const string DataActionForAttributeName = "filter-action";
        private const string DataLogicForAttributeName = "filter-logic";
        private const string DataDataTypeForAttributeName = "filter-datatype";

        /// <summary>
        /// 查询字段运算符
        /// =、in、更多请参考 ExpressionExtension扩展类 CreateLambda方法
        /// </summary>
        [HtmlAttributeName(DataActionForAttributeName)]
        public string DataAction { get; set; }

        /// <summary>
        /// 关系运算符 and or
        /// </summary>
        [HtmlAttributeName(DataLogicForAttributeName)]
        public string DataLogic { get; set; }

        /// <summary>
        /// 数据类型 string  guid
        /// </summary>
        [HtmlAttributeName(DataDataTypeForAttributeName)]
        public string DataDataType { get; set; }


        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (DataAction != null)
            {
                //  var childContent = output.Content.IsModified ? output.Content.GetContent() :
                //(await output.GetChildContentAsync()).GetContent();
                output.Attributes.SetAttribute("data-action", DataAction);
            }
            if (DataLogic != null)
            {
                output.Attributes.SetAttribute("data-logic", DataLogic);
            }
            if (DataDataType != null)
            {
                output.Attributes.SetAttribute("data-datatype", DataDataType);
            }
        }
    }
}
