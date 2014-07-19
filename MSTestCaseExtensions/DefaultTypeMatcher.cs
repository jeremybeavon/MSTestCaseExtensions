using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MSTestCaseExtensions
{
    public sealed class DefaultTypeMatcher : ITypeMatcher
    {
        public bool IsMatch(Type type, string className)
        {
            return type.Name == className && IsTestClass(type);
        }

        public static bool IsTestClass(Type type)
        {
            return Attribute.IsDefined(type, typeof(TestClassAttribute));
        }
    }
}
