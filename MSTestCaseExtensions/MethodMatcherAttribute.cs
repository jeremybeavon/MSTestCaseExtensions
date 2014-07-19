using System;

namespace MSTestCaseExtensions
{
    [AttributeUsage(AttributeTargets.Assembly)]
    public sealed class MethodMatcherAttribute : Attribute
    {
        private IMethodMatcher methodMatcher;

        public MethodMatcherAttribute(Type methodMatcherType)
        {
            MethodMatcherType = methodMatcherType;
        }

        public Type MethodMatcherType { get; private set; }

        public IMethodMatcher MethodMatcher
        {
            get { return methodMatcher ?? (methodMatcher = (IMethodMatcher)Activator.CreateInstance(MethodMatcherType)); }
        }
    }
}
