using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MSTestCaseExtensions
{
    internal static class MethodFinder
    {
        private static readonly IUnitTestAssemblyFinder defaultUnitTestAssemblyFinder = new DefaultUnitTestAssemblyFinder();
        private static readonly ITypeMatcher defaultTypeMatcher = new DefaultTypeMatcher();
        private static readonly IMethodMatcher defaultMethodMatcher = new DefaultMethodMatcher();
        private static readonly IMethodFinder defaultMethodFinder = new DefaultMethodFinder();

        public static MethodInfo GetMethodToInvoke(MethodBase method)
        {
            Assembly assembly = method.DeclaringType.Assembly;
            return GetMethodFinder(assembly).GetMethodToInvoke(method);
        }

        public static MethodInfo GetMethodToInvoke(string className, string methodName)
        {
            MethodInfo[] methodMatches =
                (from assembly in GetUnitTestAssemblies(AppDomain.CurrentDomain.GetAssemblies())
                 let typeMatcher = GetTypeMatcher(assembly)
                 let methodMatcher = GetMethodMatcher(assembly)
                 from type in assembly.GetTypes()
                 where typeMatcher.IsMatch(type, className)
                 from method in type.GetMethods()
                 where methodMatcher.IsMatch(method, methodName)
                 select method).ToArray();
            if (methodMatches.Length == 0)
            {
                const string message = "Class name \"{0}\" and method name \"{1}\" could not be found.";
                throw new InvalidOperationException(string.Format(message, className, methodName));
            }

            if (methodMatches.Length > 1)
            {
                StringBuilder messageBuilder = new StringBuilder();
                messageBuilder.AppendFormat("The method name \"{0}\" was matched in the following types:");
                messageBuilder.AppendLine();
                methodMatches.Aggregate(messageBuilder, BuildTypeNames);
                throw new AmbiguousMatchException(messageBuilder.ToString());
            }

            return GetMethodToInvoke(methodMatches[0]);
        }

        private static IEnumerable<Assembly> GetUnitTestAssemblies(IEnumerable<Assembly> assemblies)
        {
            UnitTestAssemblyFinderAttribute[] unitTestAssemblyFinderAttributes = GetUnitTestAssemblyFinderAttributes(assemblies);
            if (unitTestAssemblyFinderAttributes.Length == 0)
            {
                return defaultUnitTestAssemblyFinder.FindUnitTestAssemblies(assemblies);
            }

            foreach (UnitTestAssemblyFinderAttribute unitTestAssemblyFinderAttribute in unitTestAssemblyFinderAttributes)
            {
                assemblies = unitTestAssemblyFinderAttribute.UnitTestAssemblyFinder.FindUnitTestAssemblies(assemblies);
            }

            return assemblies;
        }

        private static UnitTestAssemblyFinderAttribute[] GetUnitTestAssemblyFinderAttributes(
            IEnumerable<Assembly> assemblies)
        {
            return assemblies.Select(GetUnitTestAssemblyFinderAttribute).Where(attribute => attribute != null).ToArray();
        }

        private static UnitTestAssemblyFinderAttribute GetUnitTestAssemblyFinderAttribute(Assembly assembly)
        {
            return GetCustomAttribute<UnitTestAssemblyFinderAttribute>(assembly);
        }

        private static ITypeMatcher GetTypeMatcher(Assembly assembly)
        {
            TypeMatcherAttribute typeMatcherAttribute = GetCustomAttribute<TypeMatcherAttribute>(assembly);
            return typeMatcherAttribute == null ? defaultTypeMatcher : typeMatcherAttribute.TypeMatcher;
        }

        private static IMethodMatcher GetMethodMatcher(Assembly assembly)
        {
            MethodMatcherAttribute methodMatcherAttribute = GetCustomAttribute<MethodMatcherAttribute>(assembly);
            return methodMatcherAttribute == null ? defaultMethodMatcher : methodMatcherAttribute.MethodMatcher;
        }

        private static IMethodFinder GetMethodFinder(Assembly assembly)
        {
            MethodFinderAttribute methodFinderAttribute = GetCustomAttribute<MethodFinderAttribute>(assembly);
            return methodFinderAttribute == null ? defaultMethodFinder : methodFinderAttribute.MethodFinder;
        }

        private static StringBuilder BuildTypeNames(StringBuilder builder, MethodInfo method)
        {
            return builder.AppendLine(method.DeclaringType.AssemblyQualifiedName);
        }

        private static T GetCustomAttribute<T>(Assembly assembly)
            where T : Attribute
        {
            return (T)Attribute.GetCustomAttribute(assembly, typeof(T));
        }
    }
}
