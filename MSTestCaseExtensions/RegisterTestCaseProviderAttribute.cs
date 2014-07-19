using System;

namespace MSTestCaseExtensions
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public sealed class RegisterTestCaseProviderAttribute : Attribute
    {
        public RegisterTestCaseProviderAttribute(Type testCaseProvider)
        {
            TestCaseProvider = testCaseProvider;
        }

        public Type TestCaseProvider { get; private set; }
    }
}
