using System.Reflection;

namespace MSTestCaseExtensions
{
    public interface IMethodMatcher
    {
        bool IsMatch(MethodInfo method, string methodName);
    }
}
