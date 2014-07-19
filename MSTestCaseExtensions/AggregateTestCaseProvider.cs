using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MSTestCaseExtensions
{
    public sealed class AggregateTestCaseProvider : ITestCaseProvider
    {
        private readonly IEnumerable<ITestCaseProvider> testCaseProviders;

        public AggregateTestCaseProvider(IEnumerable<ITestCaseProvider> testCaseProviders)
        {
            this.testCaseProviders = testCaseProviders;
        }

        public IEnumerable<object[]> GetTestCases(MethodInfo methodToInvoke)
        {
            return testCaseProviders.SelectMany(provider => provider.GetTestCases(methodToInvoke));
        }
    }
}
