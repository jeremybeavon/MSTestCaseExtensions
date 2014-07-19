using System.Collections.Generic;
using System.Reflection;

namespace MSTestCaseExtensions
{
    public interface ITestCaseProvider
    {
        IEnumerable<object[]> GetTestCases(MethodInfo methodToInvoke);
    }
}
