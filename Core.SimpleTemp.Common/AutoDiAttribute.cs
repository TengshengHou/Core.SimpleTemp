using System;
using System.Collections.Generic;
using System.Text;

namespace Core.SimpleTemp.Common
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AutoDiAttribute : Attribute
    {
        public Type _interfaceType;
        public AutoDiAttribute(Type interfaceType)
        {
            _interfaceType = interfaceType;
        }
    }
}
