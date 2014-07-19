using System.Reflection;
using System.Linq;

namespace MSTestCaseExtensions
{
    public sealed class DefaultMethodFinder : IMethodFinder
    {
        public MethodInfo GetMethodToInvoke(MethodBase method)
        {
            return method.DeclaringType.GetMethods().Single(info => info.Name == method.Name && info != method);
        }
    }
}
