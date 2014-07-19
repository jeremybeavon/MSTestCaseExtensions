using System;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Text.RegularExpressions;

namespace MSTestCaseExtensions
{
    internal sealed class MSTestCaseDataCommand : DbCommand
    {
        private static readonly Regex selectRegex =
            new Regex(@"^select.+from\s+(?<MethodName>\w+)$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private string commandText;
        private DbConnection connection;

        public override void Cancel()
        {
        }

        public override string CommandText
        {
            get { return commandText; }
            set { commandText = value; }
        }

        public override int CommandTimeout { get; set; }

        public override CommandType CommandType { get; set; }

        protected override DbParameter CreateDbParameter()
        {
            throw new NotImplementedException();
        }

        protected override DbConnection DbConnection
        {
            get { return connection; }
            set { connection = value; }
        }

        protected override DbParameterCollection DbParameterCollection
        {
            get { throw new NotImplementedException(); }
        }

        protected override DbTransaction DbTransaction { get; set; }

        public override bool DesignTimeVisible { get; set; }

        protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
        {
            Match match = selectRegex.Match(CommandText);
            if (!match.Success)
            {
                throw new NotSupportedException();
            }

            string className = Connection.ConnectionString;
            string methodName = match.Groups["MethodName"].Value;
            MethodInfo methodToInvoke = MethodFinder.GetMethodToInvoke(className, methodName);
            return new MSTestCaseDataReader(DefaultTestCaseProvider.Instance.GetTestCases(methodToInvoke));
        }

        public override int ExecuteNonQuery()
        {
            throw new NotImplementedException();
        }

        public override object ExecuteScalar()
        {
            throw new NotImplementedException();
        }

        public override void Prepare()
        {
        }

        public override UpdateRowSource UpdatedRowSource { get; set; }
    }
}
