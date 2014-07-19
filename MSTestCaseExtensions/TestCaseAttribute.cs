using System;

namespace MSTestCaseExtensions
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public sealed class TestCaseAttribute : Attribute
    {
        public TestCaseAttribute(params object[] arguments)
        {
            Arguments = arguments;
        }

        public object[] Arguments { get; private set; }
    }
}
