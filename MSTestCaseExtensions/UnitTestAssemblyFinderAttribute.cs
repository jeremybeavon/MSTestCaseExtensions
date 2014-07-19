using System;

namespace MSTestCaseExtensions
{
    [AttributeUsage(AttributeTargets.Assembly)]
    public sealed class UnitTestAssemblyFinderAttribute : Attribute
    {
        private IUnitTestAssemblyFinder unitTestAssemblyFinder;

        public UnitTestAssemblyFinderAttribute(Type unitTestAssemblyFinderType)
        {
            UnitTestAssemblyFinderType = unitTestAssemblyFinderType;
        }

        public Type UnitTestAssemblyFinderType { get; private set; }

        public IUnitTestAssemblyFinder UnitTestAssemblyFinder
        {
            get { return unitTestAssemblyFinder ?? (unitTestAssemblyFinder = CreateUnitTestAssemblyFinder()); }
        }

        private IUnitTestAssemblyFinder CreateUnitTestAssemblyFinder()
        {
            return (IUnitTestAssemblyFinder)Activator.CreateInstance(UnitTestAssemblyFinderType);
        }
    }
}
