using System;
using System.Data;
using System.Data.Common;

namespace MSTestCaseExtensions
{
    internal sealed class MSTestCaseDataAdaptor : DbDataAdapter
    {

        protected override int Fill(DataTable[] dataTables, IDataReader dataReader, int startRecord, int maxRecords)
        {
            try
            {
                return base.Fill(dataTables, dataReader, startRecord, maxRecords);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                throw;
            }
        }
    }
}
