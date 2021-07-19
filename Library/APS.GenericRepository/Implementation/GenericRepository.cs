using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace APS.GenericRepository.Implementation
{
    public class GenericRepository : IGenericRepository, IDisposable
    {
        #region Global Variable
        /// <summary>
        ///  Declare connectionstring  variable and assign value in generic repository constructor 
        /// </summary>
        private static string _connectionString;
        #endregion

        /// <summary>
        /// Constructor to declare connection string
        /// </summary>
        /// <param name="connectionString"></param>
        public GenericRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// To execute query/stored procedure with single out
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameter"></param>
        /// <returns>Dynamic model filled with data</returns>
        public async Task<IEnumerable<T>> ExecuteList<T>(GenericParameter parameter) where T : class
        {
            IEnumerable<T> resultData = null;
            try
            {
                SqlMapper.SetTypeMap(typeof(T), new CustomPropertyTypeMap(
                       typeof(T), (type, columnName) => type.GetProperties().FirstOrDefault(prop =>
                       prop.GetCustomAttributes(false).OfType<ColumnAttribute>().Any(attr => attr.Name == columnName))));

                var dbConnection = GetConnection();

                if (dbConnection.State == ConnectionState.Closed)
                    dbConnection.Open();

                if (dbConnection.State == ConnectionState.Open)
                {
                    resultData = await SqlMapper.QueryAsync<T>(dbConnection, parameter.SqlCommand, param: parameter, commandType: parameter.ExecuteType);
                    dbConnection.Dispose();
                    dbConnection.Close();
                }
            }
            catch (Exception exception)
            {

            }
            return resultData;
        }

        public async Task<int> Execute(GenericParameter parameter)
        {
            int resultData = -1;
            var dbConnection = GetConnection();

            if (dbConnection.State == ConnectionState.Closed)
                dbConnection.Open();

            if (dbConnection.State == ConnectionState.Open)
            {
                resultData = await SqlMapper.ExecuteAsync(dbConnection, parameter.SqlCommand, param: parameter, commandType: parameter.ExecuteType);
                dbConnection.Dispose();
                dbConnection.Close();
            }
            return resultData;
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }

        #region IDisposable
        /// <inheritdoc />
        /// <summary>
        /// Disposes the current object
        /// </summary>
        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes all external resources.
        /// </summary>
        /// <param name="disposing">The dispose indicator.</param>
        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                _connectionString = null;
            }
        }
        #endregion

    }
}
