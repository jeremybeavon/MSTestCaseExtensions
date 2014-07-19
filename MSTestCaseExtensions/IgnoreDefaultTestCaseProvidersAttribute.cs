using System;

namespace MSTestCaseExtensions
{
    [AttributeUsage(AttributeTargets.Assembly)]
    public sealed class IgnoreDefaultTestCaseProvidersAttribute : Attribute
    {
    }
}
