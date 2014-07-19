using System;
using System.Data.Common;

namespace MSTestCaseExtensions
{
    public sealed class MSTestCaseDataProvider : DbProviderFactory
    {
        public static readonly MSTestCaseDataProvider Instance = new MSTestCaseDataProvider();

        public override DbConnection CreateConnection()
        {
            return new MSTestCaseDataConnection();
        }

        public override DbCommand CreateCommand()
        {
            return new MSTestCaseDataCommand();
        }

        public override DbCommandBuilder CreateCommandBuilder()
        {
            return new MSTestCaseCommandBuilder();
        }

        public override DbConnectionStringBuilder CreateConnectionStringBuilder()
        {
            throw new NotSupportedException();
        }

        public override DbDataAdapter CreateDataAdapter()
        {
            return new MSTestCaseDataAdaptor();
        }
    }
}
