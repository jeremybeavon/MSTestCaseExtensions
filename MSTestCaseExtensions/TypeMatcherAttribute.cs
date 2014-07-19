using System;

namespace MSTestCaseExtensions
{
    [AttributeUsage(AttributeTargets.Assembly)]
    public sealed class TypeMatcherAttribute : Attribute
    {
        private ITypeMatcher typeMatcher;

        public TypeMatcherAttribute(Type typeMatcherType)
        {
            TypeMatcherType = typeMatcherType;
        }

        public Type TypeMatcherType { get; private set; }

        public ITypeMatcher TypeMatcher
        {
            get { return typeMatcher ?? (typeMatcher = (ITypeMatcher)Activator.CreateInstance(TypeMatcherType)); }
        }
    }
}
