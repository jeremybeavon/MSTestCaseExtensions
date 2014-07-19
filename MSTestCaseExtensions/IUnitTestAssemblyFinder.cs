using System.Collections.Generic;
using System.Reflection;

namespace MSTestCaseExtensions
{
    public interface IUnitTestAssemblyFinder
    {
        IEnumerable<Assembly> FindUnitTestAssemblies(IEnumerable<Assembly> assemblies);
    }
}
