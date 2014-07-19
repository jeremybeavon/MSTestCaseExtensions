using System.Reflection;

namespace MSTestCaseExtensions
{
    public interface IMethodFinder
    {
        MethodInfo GetMethodToInvoke(MethodBase method);
    }
}
