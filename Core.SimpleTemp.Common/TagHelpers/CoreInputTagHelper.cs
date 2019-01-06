using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.SimpleTemp.Mvc.TagHelpers
{
    /// <summary>
    /// 用于辅助前端创建 FilterModel
    /// </summary>
    [HtmlTargetElement("input", Attributes = DataFilterForAttributeName)]

    [HtmlTargetElement("input", Attributes = DataActionForAttributeName)]
    [HtmlTargetElement("input", Attributes = DataLogicForAttributeName)]
    [HtmlTargetElement("input", Attributes = DataDataTypeForAttributeName)]
    public class CoreInputTagHelper : TagHelper
    {
        private const string DataFilterForAttributeName = "filter-isFilter";
        private const string DataActionForAttributeName = "filter-action";
        private const string DataLogicForAttributeName = "filter-logic";
        private const string DataDataTypeForAttributeName = "filter-datatype";



        /// <summary>
        /// 是否是过滤条件
        /// </summary>
        [HtmlAttributeName(DataFilterForAttributeName)]
        public bool DataFilter { get; set; }


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
            if (DataFilter)
            {
                output.Attributes.SetAttribute("data-filter", DataFilter);
            }

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
