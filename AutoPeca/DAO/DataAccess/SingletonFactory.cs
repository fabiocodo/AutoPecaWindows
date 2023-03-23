using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.OracleClient;
using MySql.Data.MySqlClient;

namespace AutoPeca.DAO.DataAccess
{
    /// <summary>
    /// Class SingletonFactory for ADO.NET Providers.
    /// (Singleton Pattern). 
    /// </summary>
    public class SingletonFactory
    {

        # region Properties

        private volatile static ProviderFactory uniqueInstance;
        private static object syncRoot = new Object();

        # endregion

        # region Constructor

        private SingletonFactory()
        {

        }

        # endregion

        # region Methods

        /// <summary>
        /// Método responsável por criar e retornar o Wrapper ADO.NET.
        /// </summary>
        /// <returns>Wrapper</returns>
        public static Wrapper GetWrapper()
        {

            if (uniqueInstance == null)
            {
                GetProviderFactory();
            }

            return CreateWraper();
        }

        public static void ClearInstance()
        {
            uniqueInstance = null;
        }

        /// <summary>
        /// Método responsável por retornar o única instância do ProviderFactory (Singleton).
        /// </summary>
        /// <returns></returns>
        public static ProviderFactory GetProviderFactory()
        {

            if (uniqueInstance == null)
            {

                // Bloqueia o objecto para as possíveis threads existentes,
                // de modo que é a única instâncias para as todas as threads.
                lock (syncRoot)
                {
                    if (uniqueInstance == null)
                    {
                        uniqueInstance = GetProvider();
                    }
                }
            }

            return uniqueInstance;
        }

        /// <summary>
        /// Get provider parameter in Configuration and create ProviderFactory for as unique instace.
        /// </summary>
        /// <returns></returns>
        private static ProviderFactory GetProvider()
        {

            switch (System.Configuration.ConfigurationManager.AppSettings["ProviderBancoDados"].ToUpper().Trim())
            {
                case "ORACLE":
                    //uniqueInstance =  new ProviderFactory(OracleClientFactory.Instance);
                    break;
                case "SQL":
                    //uniqueInstance = new ProviderFactory(SqlClientFactory.Instance);
                    break;
                case "MYSQL":
                    uniqueInstance = new ProviderFactory(MySqlClientFactory.Instance);
                    break;
            }

            uniqueInstance.ConnectionString = CreateConnectionString();
            return uniqueInstance;
        }

        /// <summary>
        /// Create Wrapper ADO.NET.
        /// </summary>
        /// <returns>Wrapper.</returns>
        private static Wrapper CreateWraper()
        {

            try
            {
                Wrapper wraper = new Wrapper();
                wraper.Connection = uniqueInstance.Provider.CreateConnection();
                wraper.Command = uniqueInstance.Provider.CreateCommand();
                wraper.DataAdapter = uniqueInstance.Provider.CreateDataAdapter();
                wraper.Connection.ConnectionString = uniqueInstance.ConnectionString;
                wraper.Command.Connection = wraper.Connection;
                wraper.DataAdapter.SelectCommand = wraper.Command;
                return wraper;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get parameters in Configuration and create ConnectionString.
        /// </summary>
        /// <returns>ConnectionString.</returns>
        private static string CreateConnectionString()
        {

            StringBuilder connStringBuilder = new StringBuilder();
            /*
            if (uniqueInstance.Provider is OracleClientFactory) {
                connStringBuilder.Append("Data Source=");
                connStringBuilder.Append(ConfigurationSettings.AppSettings["ServidorBancoDados"]);
                connStringBuilder.Append(";");
                connStringBuilder.Append("User Id=");
                connStringBuilder.Append(ConfigurationSettings.AppSettings["UsuarioBancoDados"]);
                connStringBuilder.Append(";");
                connStringBuilder.Append("Password=");
                connStringBuilder.Append(ConfigurationSettings.AppSettings["SenhaBancoDados"]);
                connStringBuilder.Append(";");
                connStringBuilder.Append("Integrated Security=");
                connStringBuilder.Append(ConfigurationSettings.AppSettings["UsuarioIntegrado"]);
            } else if (uniqueInstance.Provider is SqlClientFactory) {
                connStringBuilder.Append("Data Source=");
                connStringBuilder.Append(ConfigurationSettings.AppSettings["ServidorBancoDados"]);
                connStringBuilder.Append(";");
                connStringBuilder.Append("Initial Catalog=");
                connStringBuilder.Append(ConfigurationSettings.AppSettings["BancoDados"]);
                connStringBuilder.Append(";");
                connStringBuilder.Append("User Id=");
                connStringBuilder.Append(ConfigurationSettings.AppSettings["UsuarioBancoDados"]);
                connStringBuilder.Append(";");
                connStringBuilder.Append("Password=");
                connStringBuilder.Append(ConfigurationSettings.AppSettings["SenhaBancoDados"]);
                connStringBuilder.Append(";");
                connStringBuilder.Append("Integrated Security=");
                connStringBuilder.Append(ConfigurationSettings.AppSettings["UsuarioIntegrado"]);
			} else 
            */
            if (uniqueInstance.Provider is MySqlClientFactory)
            {
                connStringBuilder.Append("Data Source=");
                connStringBuilder.Append(ConfigurationManager.AppSettings["ServidorBancoDados"]);
                connStringBuilder.Append(";");
                connStringBuilder.Append("Database=");
                connStringBuilder.Append(ConfigurationManager.AppSettings["BancoDados"]);
                connStringBuilder.Append(";");
                connStringBuilder.Append("User Id=");
                connStringBuilder.Append(ConfigurationManager.AppSettings["UsuarioBancoDados"]);
                connStringBuilder.Append(";");
                connStringBuilder.Append("Password=");
                connStringBuilder.Append(ConfigurationManager.AppSettings["SenhaBancoDados"]);
                connStringBuilder.Append(";");
                connStringBuilder.Append("Port=");
                connStringBuilder.Append(ConfigurationManager.AppSettings["Porta"]);
            }

            return connStringBuilder.ToString();
        }

        # endregion 

    }
}
