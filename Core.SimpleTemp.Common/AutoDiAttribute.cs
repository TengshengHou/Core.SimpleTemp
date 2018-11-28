using System;
using System.Collections.Generic;
using System.Text;

namespace Core.SimpleTemp.Common
{
    [AttributeUsage(AttributeTargets.Interface)]
    public class AutoDiAttribute : Attribute
    {
        public Type ImplementationType { get; private set; }
        public string DiType { get; private set; }
        public AutoDiAttribute(Type implementationType, string diType = AutoDiAttribute.Transient)
        {
            ImplementationType = implementationType;
            DiType = diType;
        }

        public const string Scoped = "Scoped";
        public const string Transient = "Transient";
        public const string Singleton = "Singleton";
    }
}
