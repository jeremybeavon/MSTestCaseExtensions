using System;

namespace MSTestCaseExtensions
{
    [AttributeUsage(AttributeTargets.Assembly)]
    public sealed class MethodFinderAttribute : Attribute
    {
        private IMethodFinder methodFinder;

        public MethodFinderAttribute(Type methodFinderType)
        {
            MethodFinderType = methodFinderType;
        }

        public Type MethodFinderType { get; private set; }

        public IMethodFinder MethodFinder
        {
            get { return methodFinder ?? (methodFinder = (IMethodFinder)Activator.CreateInstance(MethodFinderType)); }
        }
    }
}
