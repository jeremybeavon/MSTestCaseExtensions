using System;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PostSharp.Aspects;

namespace MSTestCaseExtensions
{
    [Serializable]
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class ProvidesTestCasesAttribute : MethodInterceptionAspect
    {
        public const string DefaultTestContextPropertyName = "TestContext";
        private MethodInfo methodToInvoke;

        public ProvidesTestCasesAttribute()
            : this(DefaultTestContextPropertyName)
        {
        }

        public ProvidesTestCasesAttribute(string testContextPropertyName)
        {
            TestContextPropertyName = testContextPropertyName;
        }

        public string TestContextPropertyName { get; set; }

        public override void OnInvoke(MethodInterceptionArgs args)
        {
            TestContext testContext = GetTestContext(args.Instance);
            if (methodToInvoke == null)
            {
                methodToInvoke = MethodFinder.GetMethodToInvoke(args.Method);
            }

            try
            {
                methodToInvoke.Invoke(args.Instance, GetParameterValues(testContext, methodToInvoke.GetParameters()));
            }
            catch (TargetInvocationException exc)
            {
                // fix stacktrace
                FieldInfo remoteStackTraceString = typeof(Exception).GetField("_remoteStackTraceString", BindingFlags.Instance | BindingFlags.NonPublic); // MS.Net

                remoteStackTraceString.SetValue(exc.InnerException, exc.InnerException.StackTrace + Environment.NewLine);

                throw exc.InnerException;
            }
        }

        private static object[] GetParameterValues(TestContext context, ParameterInfo[] parameters)
        {
            Func<ParameterInfo, int, object> selectFunc =
                (parameter, index) =>
                {
                    context.WriteLine("{0} = {1}", parameter.Name, context.DataRow[index]);
                    return context.DataRow[index];
                };
            return parameters.Select(selectFunc).ToArray();
        }

        private TestContext GetTestContext(object instance)
        {
            return (TestContext)instance.GetType().GetProperty(TestContextPropertyName).GetValue(instance, null);
        }
    }
}
