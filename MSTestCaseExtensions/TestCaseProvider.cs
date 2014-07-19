using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MSTestCaseExtensions
{
    public sealed class TestCaseProvider : ITestCaseProvider
    {
        public IEnumerable<object[]> GetTestCases(MethodInfo methodToInvoke)
        {
            return GetTestCaseAttributes(methodToInvoke).Select(attribute => attribute.Arguments);
        }

        private static TestCaseAttribute[] GetTestCaseAttributes(MethodInfo methodToInvoke)
        {
            return (TestCaseAttribute[])Attribute.GetCustomAttributes(methodToInvoke, typeof(TestCaseAttribute));
        }
    }
}
