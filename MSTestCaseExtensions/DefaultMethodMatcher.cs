using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MSTestCaseExtensions
{
    public sealed class DefaultMethodMatcher : IMethodMatcher
    {
        public bool IsMatch(MethodInfo method, string methodName)
        {
            return method.Name == methodName && IsTestMethod(method);
        }

        private static bool IsTestMethod(MethodInfo method)
        {
            return Attribute.IsDefined(method, typeof(TestMethodAttribute)) &&
                Attribute.IsDefined(method, typeof(DataSourceAttribute));
        }
    }
}
