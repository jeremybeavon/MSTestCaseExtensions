using System;

namespace MSTestCaseExtensions
{
    public interface ITypeMatcher
    {
        bool IsMatch(Type type, string className);
    }
}
