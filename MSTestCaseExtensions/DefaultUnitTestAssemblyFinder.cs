using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MSTestCaseExtensions
{
    public sealed class DefaultUnitTestAssemblyFinder : IUnitTestAssemblyFinder
    {
        public static readonly IList<string> AssemblyNamesToIgnore = new List<string>()
        {
            "System"
        };

        public static readonly IList<string> AssemblyPrefixesToIgnore = new List<string>()
        {
            "mscorlib",
            "System.",
            "Microsoft."
        };

        public IEnumerable<Assembly> FindUnitTestAssemblies(IEnumerable<Assembly> assemblies)
        {
            Assembly[] nonIgnoredAssemblies = FindNonIgnoredAssemblies(assemblies).ToArray();
            Assembly[] unitTestAssemblies = FindAssembliesWithUnitTestAttribute(nonIgnoredAssemblies).ToArray();
            return unitTestAssemblies.Length == 0 ? nonIgnoredAssemblies : unitTestAssemblies;
        }

        public static IEnumerable<Assembly> FindNonIgnoredAssemblies(IEnumerable<Assembly> assemblies)
        {
            assemblies = assemblies.Where(assembly => !AssemblyNamesToIgnore.Contains(assembly.GetName().Name));
            assemblies = assemblies.Where(assembly => !AssemblyPrefixesToIgnore.Contains(assembly.FullName));
            return assemblies;
        }

        public static IEnumerable<Assembly> FindAssembliesWithUnitTestAttribute(IEnumerable<Assembly> assemblies)
        {
            return assemblies.Where(IsUnitTestAssembly);
        }

        public static bool IsUnitTestAssembly(Assembly assembly)
        {
            return Attribute.IsDefined(assembly, typeof(UnitTestAssemblyAttribute));
        }
    }
}
