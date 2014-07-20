Overview
==========================================================================

Add ability for using a [TestCase] attribute when using MSTest. It is extensible so other test case providers can be added.

Example
==========================================================================
```csharp
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MSTestCaseExtensions
{
    [TestClass]
    public class UnitTest1
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        [ProvidesTestCases]
        [DataSource("MSTestCaseExtensions", "UnitTest1", "TestAdd", DataAccessMethod.Sequential)]
        public void TestAdd()
        {
        }

        [TestCase(1, 10, 11)]
        [TestCase(2, 20, 22)]
        [TestCase(3, 30, 33)]
        [TestCase(4, 40, 44)]
        [TestCase(5, 50, 55)]
        public void TestAdd(int value1, int value2, int expectedResult)
        {
            Assert.AreEqual(expectedResult, value1 + value2);
        }
    }
}
```

Requirements

1. Must have a property named TestContext which returns a TestContext
2. There must be 2 methods with the same name. One must have no parameters.
3. The method with no parameters must have a TestMethod attribute and a ProvidesTestCases attribute.
4. The method with no parameters must have a DataSource method with the following conditions:
  1. The first parameter must be MSTestCaseExtensions.
  2. The second parameter must be the class name.
  3. The third parameter must be the method name.
  4. The fourth parameter must be DataAccessMethod.Sequential
5. The method with no parameters must have an empty body.
5. The app.config file must contain the following config section:
```
<system.data>
  <DbProviderFactories>
    <add name="MSTest TestCase Provider"
         invariant="MSTestCaseExtensions"
         description=".NET Framework Data Provider for MSTestCaseExtensions"
         type="MSTestCaseExtensions.MSTestCaseDataProvider, MSTestCaseExtensions"/>
  </DbProviderFactories>
</system.data>
```

Optional assembly attributes

- [assembly: UnitTestAssembly]: If any assemblies contain this attribute, only these assemblies will be searched.
- [assembly: RegisterTestCaseProvider(typeof(...))]: Allows extensions for how test cases are found against a method. The test case provider is only registered in the assembly that the attribute is applied to. The type must implement ITestCaseProvider, which has a method with the following signature:
  * IEnumerable<object[]> GetTestCases(MethodInfo methodToInvoke);
- [assembly: IgnoreDefaultTestCaseProvider]: Disables the default test case providers for the assembly that it is applied to.
- [assembly: UnitTestAssemblyFinder(typeof(...))]: Allows extensions for how unit test assemblies are identified. If this attribute is applied to multiple assemblies, all assembly finders will be run in any order. The type must implement IUnitTestAssemblyFinder, which has a method with the following signature:
  * IEnumerable<Assembly> FindUnitTestAssemblies(IEnumerable<Assembly> assemblies);
- [assembly: TypeMatcher(typeof(...))]: Allows extensions for how a type is found in an assembly. It only applies to the assembly that the attribute has been applied to. The type must implement ITypeMatcher, which has a method with the following signature:
  * bool IsMatch(Type type, string className);
- [assembly: MethodMatcher(typeof(...))]: Allows extensions for how a method is found in a type. It only applies to the assembly that the attribute has been applied to. The type must implement IMethodMatcher, which has a method with the following signature:
  * bool IsMatch(MethodInfo method, string methodName);
- [assembly: MethodFinder(typeof(...))]: Allows extensions for how a parameterized method is found. It only applies to the assembly that the attribute has been applied to. The type must implement IMethodFinder, which has a method with the following signature:
  * MethodInfo GetMethodToInvoke(MethodBase method);


How it works
------------------------------------------------------------------------
The MSTestCaseExtensions is a custom ADO.Net provider, which finds the method with test cases on it and creates a data reader with each row being a test case.

The ProviderTestCases provides method interception using PostSharp. It calls the method in the same class with the same name.

Adapted from this [blog post.](http://blog.drorhelper.com/2011/09/enabling-parameterized-tests-in-mstest.html) Many thanks for that. :)
