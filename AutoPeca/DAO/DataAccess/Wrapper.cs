using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;

namespace AutoPeca.DAO.DataAccess
{
    /// <summary>
    /// Wraper ADO.NET Members.
    /// </summary>
    public class Wrapper
    {

        # region Properties

        public DbConnection Connection { get; set; }
        public DbCommand Command { get; set; }
        public DbDataAdapter DataAdapter { get; set; }
        public DbTransaction Transaction { get; set; }
        public DbParameters Parameters { get; set; }

        # endregion

        # region Constructors

        public Wrapper()
        {
            Parameters = new DbParameters();
        }

        # endregion

        # region Methods

        # endregion 
    }

    /// <summary>
    /// List of the generic parameters for Wrapper ADO.NET.
    /// </summary>
    public class DbParameters
    {

        # region Properties

        private List<DbParameter> parameters;

        #endregion

        # region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public DbParameters()
        {
            parameters = new List<System.Data.Common.DbParameter>();
        }

        #endregion

        # region Methods

        /// <summary>
        /// Add new parameter.
        /// </summary>
        /// <param name="name">Parameter Name.</param>
        /// <param name="value">Parameter Value.</param>
        /// <param name="direction">Parameter Direction</param>
        public void Add(string name, object value, System.Data.ParameterDirection direction)
        {
            DbParameter parameter = SingletonFactory.GetProviderFactory().Provider.CreateParameter();
            parameter.ParameterName = name;
            parameter.Value = value;
            parameter.Direction = direction;
            parameters.Add(parameter);
        }

        /// <summary>
        /// Clear parameters.
        /// </summary>
        public void Clear()
        {
            parameters.Clear();
        }

        /// <summary>
        /// Verifies the parameter existing in parameters.
        /// </summary>
        /// <param name="name">Parameter Name.</param>
        /// <returns></returns>
        public bool Contains(string name)
        {
            foreach (var item in parameters)
            {
                if (item.ParameterName.Equals(name))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Remove parameter.
        /// </summary>
        /// <param name="name">Parameter Name.</param>
        public void Remove(string name)
        {
            for (int i = 0; i < parameters.Count; i++)
            {
                if (parameters[i].ParameterName.Equals(name))
                {
                    parameters.RemoveAt(i);
                    break;
                }
            }
        }

        /// <summary>
        /// Return Array of the DbParameter.
        /// </summary>
        /// <returns>Array of the DbParameter.</returns>
        public DbParameter[] ToArray()
        {
            return parameters.ToArray();
        }

        /// <summary>
        /// Number of the parameters.
        /// </summary>
        public int Count()
        {
            return parameters.Count;
        }

        #endregion
    }

    /// <summary>
    /// Wrapper DbDataReader ADO.NET.
    /// </summary>
    public class DbDataReader : System.Data.IDataReader
    {

        # region Properties

        private System.Data.Common.DbDataReader reader;

        public bool HasRows
        {
            get { return reader.HasRows; }
        }

        public int Depth
        {
            get { return reader.Depth; }
        }

        public bool IsClosed
        {
            get { return reader.IsClosed; }
        }

        public int RecordsAffected
        {
            get { return reader.RecordsAffected; }
        }

        public int FieldCount
        {
            get { return reader.FieldCount; }
        }

        public object this[string name]
        {
            get { return reader[name]; }
        }

        public object this[int i]
        {
            get { return reader[i]; }
        }

        #endregion

        #region Constructor

        public DbDataReader(System.Data.Common.DbDataReader reader)
        {
            this.reader = reader;
        }

        #endregion

        # region Methods

        public bool Read()
        {

            if (reader.Read())
            {
                return true;
            }
            else
            {
                reader.Close();
                return false;
            }

        }

        public void Close()
        {
            reader.Close();
        }

        public DataTable GetSchemaTable()
        {
            return reader.GetSchemaTable();
        }

        public bool NextResult()
        {
            return reader.NextResult();
        }

        public void Dispose()
        {
            reader.Dispose();
        }

        public bool GetBoolean(int i)
        {
            return reader.GetBoolean(i);
        }

        public byte GetByte(int i)
        {
            return reader.GetByte(i);
        }

        public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        {
            return reader.GetBytes(i, fieldOffset, buffer, bufferoffset, length);
        }

        public char GetChar(int i)
        {
            return reader.GetChar(i);
        }

        public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
        {
            return reader.GetChars(i, fieldoffset, buffer, bufferoffset, length);
        }

        public IDataReader GetData(int i)
        {
            return reader.GetData(i);
        }

        public string GetDataTypeName(int i)
        {
            return reader.GetDataTypeName(i);
        }

        public DateTime GetDateTime(int i)
        {
            return reader.GetDateTime(i);
        }

        public decimal GetDecimal(int i)
        {
            return reader.GetDecimal(i);
        }

        public double GetDouble(int i)
        {
            return reader.GetDouble(i);
        }

        public Type GetFieldType(int i)
        {
            return reader.GetFieldType(i);
        }

        public float GetFloat(int i)
        {
            return reader.GetFloat(i);
        }

        public Guid GetGuid(int i)
        {
            return reader.GetGuid(i);
        }

        public short GetInt16(int i)
        {
            return reader.GetInt16(i);
        }

        public int GetInt32(int i)
        {
            return reader.GetInt32(i);
        }

        public long GetInt64(int i)
        {
            return reader.GetInt64(i);
        }

        public string GetName(int i)
        {
            return reader.GetName(i);
        }

        public int GetOrdinal(string name)
        {
            return reader.GetOrdinal(name);
        }

        public string GetString(int i)
        {
            return reader.GetString(i);
        }

        public object GetValue(int i)
        {
            return reader.GetValue(i);
        }

        public int GetValues(object[] values)
        {
            return reader.GetValues(values);
        }

        public bool IsDBNull(int i)
        {
            return reader.IsDBNull(i);
        }

        #endregion
    }
}
