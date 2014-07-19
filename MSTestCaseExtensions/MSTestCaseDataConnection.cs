using System;
using System.Data;
using System.Data.Common;

namespace MSTestCaseExtensions
{
    internal sealed class MSTestCaseDataConnection : DbConnection
    {
        protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
        {
            throw new NotImplementedException();
        }

        public override void ChangeDatabase(string databaseName)
        {
            throw new NotImplementedException();
        }

        public override void Close()
        {
        }

        public override string ConnectionString { get; set; }

        protected override DbCommand CreateDbCommand()
        {
            return new MSTestCaseDataCommand();
        }

        public override string DataSource
        {
            get { return ConnectionString; }
        }

        public override string Database
        {
            get { return ConnectionString; }
        }

        public override void Open()
        {
        }

        public override string ServerVersion
        {
            get { return "1"; }
        }

        public override ConnectionState State
        {
            get { return ConnectionState.Open; }
        }

        public override DataTable GetSchema(string collectionName, string[] restrictionValues)
        {
            return base.GetSchema(collectionName, restrictionValues);
        }
    }
}
