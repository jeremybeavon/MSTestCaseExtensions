using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace MSTestCaseExtensions
{
    public sealed class DefaultTestCaseProvider : ITestCaseProvider
    {
        public static readonly ITestCaseProvider Instance = new DefaultTestCaseProvider();
        public static readonly IEnumerable<ITestCaseProvider> DefaultTestCaseProviders = new ITestCaseProvider[]
        {
            new TestCaseProvider()
        };

        private static readonly IDictionary<Assembly, ITestCaseProvider> testCaseProviders =
            new Dictionary<Assembly, ITestCaseProvider>();

        private static readonly object testCaseProvidersLock = new object();

        public IEnumerable<object[]> GetTestCases(MethodInfo methodToInvoke)
        {
            return GetTestCaseProvider(methodToInvoke.DeclaringType.Assembly).GetTestCases(methodToInvoke).ToArray();
        }

        private static ITestCaseProvider GetTestCaseProvider(Assembly assembly)
        {
            lock (testCaseProvidersLock)
            {
                ITestCaseProvider provider;
                if (!testCaseProviders.TryGetValue(assembly, out provider))
                {
                    IEnumerable<ITestCaseProvider> providers = GetDefaultTestCaseProviders(assembly);
                    providers = providers.Concat(GetRegisteredTestCaseProviders(assembly));
                    provider = new AggregateTestCaseProvider(providers);
                    testCaseProviders.Add(assembly, provider);
                }

                return provider;
            }
        }

        private static IEnumerable<ITestCaseProvider> GetDefaultTestCaseProviders(Assembly assembly)
        {
            return AreDefaultTestCaseProvidersIgnored(assembly) ? new ITestCaseProvider[0] : DefaultTestCaseProviders;
        }

        private static bool AreDefaultTestCaseProvidersIgnored(Assembly assembly)
        {
            return Attribute.IsDefined(assembly, typeof(IgnoreDefaultTestCaseProvidersAttribute));
        }

        private static IEnumerable<ITestCaseProvider> GetRegisteredTestCaseProviders(Assembly assembly)
        {
            return GetCustomAttributes<RegisterTestCaseProviderAttribute>(assembly)
                .Select(attribute => (ITestCaseProvider)Activator.CreateInstance(attribute.TestCaseProvider));
        }

        private static T[] GetCustomAttributes<T>(Assembly assembly)
            where T : Attribute
        {
            return (T[])Attribute.GetCustomAttributes(assembly, typeof(T));
        }
    }
}
