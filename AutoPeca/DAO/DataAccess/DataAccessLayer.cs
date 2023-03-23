using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using MySql.Data.MySqlClient;

namespace AutoPeca.DAO.DataAccess
{

    public class DataAccessLayer : IDisposable
    {

        #region Properties

        public Wrapper Wrapper { get; set; }
        public Wrapper WrapperNuvem { get; set; }
        public Wrapper Wrapperlicenca { get; set; }
        private bool transaction;
        public bool replicar;
        private bool disposed;

        #endregion

        #region Constructors

        public DataAccessLayer()
        {
            Wrapper = SingletonFactory.GetWrapper();
          
        }

        #endregion

        #region Methods

        public bool TestConnection()
        {
            Wrapper.Connection.Open();
            Wrapper.Connection.Close();
            return true;
        }

        public void AddParameter(string name, object value, ParameterDirection direction)
        {
            Wrapper.Parameters.Add(name, value, direction);
        }

        public void BeginTransaction(IsolationLevel level)
        {

            try
            {
                Wrapper.Connection.Open();
                Wrapper.Transaction = Wrapper.Connection.BeginTransaction(level);
                Wrapper.Command.Transaction = Wrapper.Transaction;
                transaction = true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void CommitTransaction()
        {
            Wrapper.Transaction.Commit();
            Wrapper.Connection.Close();
            Wrapper.Parameters.Clear();
            Wrapper.Command.Parameters.Clear();
            Wrapper.Command.Dispose();
            transaction = false;
        }

        public void RollbackTransaction()
        {
            Wrapper.Transaction.Rollback();
            Wrapper.Connection.Close();
            Wrapper.Parameters.Clear();
            Wrapper.Command.Parameters.Clear();
            Wrapper.Command.Dispose();
            transaction = false;
        }

        public DbDataReader ExecuteReader(string sql, CommandType commandType)
        {

            try
            {
                Wrapper.Connection.Open();
                Wrapper.Command.CommandText = sql;
                Wrapper.Command.CommandType = commandType;
                Wrapper.Command.Parameters.AddRange(Wrapper.Parameters.ToArray());
                DbDataReader result = new DbDataReader(Wrapper.Command.ExecuteReader(CommandBehavior.CloseConnection));

                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                Wrapper.Parameters.Clear();
                Wrapper.Command.Parameters.Clear();
            }
        }

        public int Execute(string sql, CommandType commandType)
        {

            try
            {
                Wrapper.Command.CommandType = commandType;
                Wrapper.Command.CommandText = sql;
                Wrapper.Command.CommandTimeout = 1000;
                Wrapper.Command.Parameters.Clear();
                Wrapper.Command.Parameters.AddRange(Wrapper.Parameters.ToArray());

                if (!transaction)
                {
                    Wrapper.Connection.Open();
                }

                int num = Wrapper.Command.ExecuteNonQuery();


                if (replicar)
                {
                    foreach (var item in Wrapper.Parameters.ToArray())
                    {
                        switch (item.DbType)
                        {
                            case DbType.String:
                                if (item.Value != null)
                                {
                                    sql = sql.Replace("@" + item.ParameterName.ToString(), "'" + item.Value.ToString() + "'");
                                }
                                break;
                            case DbType.Date:
                                sql = sql.Replace("@" + item.ParameterName.ToString(), "'" + item.Value.ToString() + "'");
                                break;
                            default:
                                sql = sql.Replace("@" + item.ParameterName.ToString(), item.Value.ToString());
                                break;
                        }

                    }
                   
                }
                Wrapper.Parameters.Clear();

                return num;

            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (!transaction)
                    Wrapper.Connection.Close();


            }


        }
       
        public int Execute(string sql, CommandType commandType, ref int? identity)
        {

            try
            {
                Wrapper.Command.CommandType = commandType;
                Wrapper.Command.CommandText = string.Concat(sql, "; SELECT LAST_INSERT_ID();");
                Wrapper.Command.Parameters.Clear();
                Wrapper.Command.Parameters.AddRange(Wrapper.Parameters.ToArray());
                int affecteds = -1;

                if (!transaction && Wrapper.Connection.State == ConnectionState.Closed)
                {
                    Wrapper.Connection.Open();
                }

                identity = Convert.ToInt32(Wrapper.Command.ExecuteScalar());

                Wrapper.Parameters.Clear();
                return affecteds;

            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (!transaction && Wrapper.Connection.State == ConnectionState.Open)
                    Wrapper.Connection.Close();
            }
        }

        public DataTable ExecuteDateTable(string sql, CommandType commandType)
        {

            try
            {
                DataTable table = new DataTable();
                Wrapper.Command.CommandText = sql;
                Wrapper.Command.CommandType = commandType;
                Wrapper.Command.Parameters.AddRange(Wrapper.Parameters.ToArray());
                Wrapper.DataAdapter.Fill(table);

                return table;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                Wrapper.Parameters.Clear();
                Wrapper.Command.Parameters.Clear();
            }
        }

        /// <summary>
        ///  Immediately dispose the resources of the object.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {

            //Caso o objeto ainda não tenha sido destruído, libera-se os recursos.
            if (!disposed)
            {

                //Se o objeto estiver sendo destruído através do metodo Dispose, libera-se os recurso gerenciados.    
                if (disposing)
                {

                    //Libera recursos gerenciados
                    Wrapper.Connection = null;
                    Wrapper.Command = null;
                    Wrapper.Parameters = null;
                    Wrapper.DataAdapter = null;
                }

                disposed = true;
            }
        }

        #endregion

    }
}
