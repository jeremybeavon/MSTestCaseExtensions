using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace MSTestCaseExtensions
{
    internal sealed class MSTestCaseCommandBuilder : DbCommandBuilder
    {
        public MSTestCaseCommandBuilder()
        {
        }

        public override string CatalogSeparator
        {
            get
            {
                return "'";
            }
            set
            {
            }
        }

        public override string QuotePrefix
        {
            get
            {
                return "'";
            }
            set
            {
            }
        }

        public override string QuoteSuffix
        {
            get
            {
                return "'";
            }
            set
            {
            }
        }

        protected override void ApplyParameterInfo(DbParameter parameter, DataRow row, StatementType statementType, bool whereClause)
        {
            throw new NotImplementedException();
        }

        protected override string GetParameterName(string parameterName)
        {
            throw new NotImplementedException();
        }

        protected override string GetParameterName(int parameterOrdinal)
        {
            throw new NotImplementedException();
        }

        protected override string GetParameterPlaceholder(int parameterOrdinal)
        {
            throw new NotImplementedException();
        }

        protected override void SetRowUpdatingHandler(DbDataAdapter adapter)
        {
            throw new NotImplementedException();
        }

        public override string QuoteIdentifier(string unquotedIdentifier)
        {
            return unquotedIdentifier;
        }

        public override string UnquoteIdentifier(string quotedIdentifier)
        {
            return quotedIdentifier;
        }
    }
}
