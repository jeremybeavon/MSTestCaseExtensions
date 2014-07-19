using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text.RegularExpressions;

namespace MSTestCaseExtensions
{
    internal sealed class MSTestCaseDataReader : DbDataReader
    {
        private static readonly Regex valueRegex = new Regex(@"^value(?<Ordinal>\d+)$", RegexOptions.Compiled);
        private readonly IEnumerator<object[]> enumerator;
        private readonly object[] firstRow;

        public MSTestCaseDataReader(IEnumerable<object[]> data)
        {
            enumerator = data.GetEnumerator();
            firstRow = data.FirstOrDefault() ?? new object[0];
        }

        public override void Close()
        {
            enumerator.Dispose();
        }

        public override int Depth
        {
            get { return 1; }
        }

        public override int FieldCount
        {
            get { return firstRow.Length; }
        }

        public override bool GetBoolean(int ordinal)
        {
            return (bool)GetValue(ordinal);
        }

        public override byte GetByte(int ordinal)
        {
            return (byte)GetValue(ordinal);
        }

        public override long GetBytes(int ordinal, long dataOffset, byte[] buffer, int bufferOffset, int length)
        {
            throw new NotImplementedException();
        }

        public override char GetChar(int ordinal)
        {
            return (char)GetValue(ordinal);
        }

        public override long GetChars(int ordinal, long dataOffset, char[] buffer, int bufferOffset, int length)
        {
            throw new NotImplementedException();
        }

        public override string GetDataTypeName(int ordinal)
        {
            throw new NotImplementedException();
        }

        public override DateTime GetDateTime(int ordinal)
        {
            return (DateTime)GetValue(ordinal);
        }

        public override decimal GetDecimal(int ordinal)
        {
            return (decimal)GetValue(ordinal);
        }

        public override double GetDouble(int ordinal)
        {
            return (double)GetValue(ordinal);
        }

        public override System.Collections.IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public override Type GetFieldType(int ordinal)
        {
            object value = firstRow[ordinal];
            return value == null ? typeof(object) : value.GetType();
        }

        public override float GetFloat(int ordinal)
        {
            return (float)GetValue(ordinal);
        }

        public override Guid GetGuid(int ordinal)
        {
            return (Guid)GetValue(ordinal);
        }

        public override short GetInt16(int ordinal)
        {
            return (short)GetValue(ordinal);
        }

        public override int GetInt32(int ordinal)
        {
            return (int)GetValue(ordinal);
        }

        public override long GetInt64(int ordinal)
        {
            return (long)GetValue(ordinal);
        }

        public override string GetName(int ordinal)
        {
            return "value" + ordinal;
        }

        public override int GetOrdinal(string name)
        {
            Match match = valueRegex.Match(name);
            if (!match.Success)
            {
                throw new IndexOutOfRangeException();
            }

            return int.Parse(match.Groups["Ordinal"].Value);
        }

        public override DataTable GetSchemaTable()
        {
            throw new NotImplementedException();
        }

        public override string GetString(int ordinal)
        {
            return (string)GetValue(ordinal);
        }

        public override object GetValue(int ordinal)
        {
            return enumerator.Current;
        }

        public override int GetValues(object[] values)
        {
            int length = Math.Min(enumerator.Current.Length, values.Length);
            Array.Copy(enumerator.Current, values, length);
            return length;
        }

        public override bool HasRows
        {
            get { return true; }
        }

        public override bool IsClosed
        {
            get { return false; }
        }

        public override bool IsDBNull(int ordinal)
        {
            return GetValue(ordinal) == null;
        }

        public override bool NextResult()
        {
            return false;
        }

        public override bool Read()
        {
            return enumerator.MoveNext();
        }

        public override int RecordsAffected
        {
            get { return 0; }
        }

        public override object this[string name]
        {
            get { return GetValue(GetOrdinal(name)); }
        }

        public override object this[int ordinal]
        {
            get { return GetValue(ordinal); }
        }
    }
}
