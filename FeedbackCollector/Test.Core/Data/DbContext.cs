using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Web;

namespace Test.Core.Data
{
    public class DbContext: IDisposable
    {
        private static DbContext _instance;
        private readonly DbProviderFactory _dbFactory;
        public readonly DbConnection _connection;
        private readonly string _connectionString;
        private DbDataAdapter _adapter;
        private DbCommand _command;
        private DbParameter _parameter;
        private int _commandTimeOut;

        public int CommandTimeOut
        {
            set { _commandTimeOut = value; }

        }

        public DbContext(string connectionString, string providerName)
        {
            _dbFactory = DbProviderFactories.GetFactory(providerName);
            _connectionString = connectionString;
            _connection = _dbFactory.CreateConnection();
            if (_connection != null) _connection.ConnectionString = _connectionString;
        }

        public DbContext GetInstance(string connectionString, string providerName="System.Data.SqlClient")
        {
            return _instance = new DbContext(connectionString, providerName);
        }

        public void Open()
        {
            if(_connection.State== ConnectionState.Broken || _connection.State == ConnectionState.Closed)
            {
                _connection.Close();
                _connection.Open();
            }
        }

        public void Close()
        {
            if (_connection.State == ConnectionState.Broken || _connection.State == ConnectionState.Open)
            {
                _connection.Close();
            }
        }

        public DataTable GetDataTable(string sqlQuery)
        {
            var dataTable = new DataTable();
            _command = _dbFactory.CreateCommand();
            if (_command != null)
            {
                _command.CommandType = CommandType.Text;
                _command.CommandText = sqlQuery;
                _command.CommandTimeout = _commandTimeOut;
                _command.Connection = _connection;
                _adapter = _dbFactory.CreateDataAdapter();
                if (_adapter != null) _adapter.SelectCommand = _command;
            }
            if (_adapter != null) _adapter.Fill(dataTable);
            return dataTable;
        }

        public DataTable GetDataTable(string sqlQuery, Parameter inputParameter)
        {
            var dataTable = new DataTable();
            _command = _dbFactory.CreateCommand();
            if (_command != null)
            {
                _command.CommandType = CommandType.Text;
                _command.CommandText = sqlQuery;

                _parameter = _dbFactory.CreateParameter();
                if (_parameter != null)
                {
                    _parameter.DbType = inputParameter.Type;
                    _parameter.Value = inputParameter.Value;
                    _parameter.ParameterName = inputParameter.Name;
                    _command.Parameters.Add(_parameter);
                }

                _command.CommandTimeout = _commandTimeOut;
                _command.Connection = _connection;
                _adapter = _dbFactory.CreateDataAdapter();
                if (_adapter != null) _adapter.SelectCommand = _command;
            }
            if (_adapter != null) _adapter.Fill(dataTable);
            return dataTable;
        }

        public DataTable GetDataTable(string sqlQuery,List<Parameter> inputParameters)
        {
            var dataTable = new DataTable();
            _command = _dbFactory.CreateCommand();
            if (_command != null)
            {
                _command.CommandType = CommandType.Text;
                _command.CommandText = sqlQuery;

                foreach(var t in inputParameters)
                {
                    _parameter = _dbFactory.CreateParameter();
                    if (_parameter != null)
                    {
                        _parameter.DbType = t.Type;
                        _parameter.Value = t.Value;
                        _parameter.ParameterName = t.Name;
                        _command.Parameters.Add(_parameter);
                    }

                }

                _command.CommandTimeout = _commandTimeOut;
                _command.Connection = _connection;
                _adapter = _dbFactory.CreateDataAdapter();
                if (_adapter != null) _adapter.SelectCommand = _command;
            }
            if (_adapter != null) _adapter.Fill(dataTable);
            return dataTable;
        }

        public DataTable GetData(string procedureName, List<Parameter> inputParameters)
        {
            var dataTable = new DataTable();
            _command = new OdbcCommand();
            _command = _dbFactory.CreateCommand();
            if (_command != null)
            {
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = procedureName;
                _command.CommandTimeout = _commandTimeOut;
                _command.Connection = _connection;
                foreach (var t in inputParameters)
                {
                    _parameter = _dbFactory.CreateParameter();
                    if (_parameter != null)
                    {
                        _parameter.DbType = t.Type;
                        _parameter.Value = t.Value;
                        _parameter.ParameterName = t.Name;
                        _command.Parameters.Add(_parameter);
                    }
                }               
                _adapter = _dbFactory.CreateDataAdapter();
                if (_adapter != null) _adapter.SelectCommand = _command;
            }
            if (_adapter != null) _adapter.Fill(dataTable);
            return dataTable;
        }

        public DataSet GetDataDataSet(string storedProcedure, List<Parameter> inputParameters)
        {
            var dataSet = new DataSet();
            _command = new OdbcCommand();
            _command = _dbFactory.CreateCommand();
            if (_command != null)
            {
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = storedProcedure;
               
                foreach (var t in inputParameters)
                {
                    _parameter = _dbFactory.CreateParameter();
                    if (_parameter != null)
                    {
                        _parameter.DbType = t.Type;
                        _parameter.Value = t.Value;
                        _parameter.ParameterName = t.Name;
                        _command.Parameters.Add(_parameter);
                    }
                }
                _command.CommandTimeout = _commandTimeOut;
                _command.Connection = _connection;
                _adapter = _dbFactory.CreateDataAdapter();
                if (_adapter != null) _adapter.SelectCommand = _command;
            }
            if (_adapter != null) _adapter.Fill(dataSet);
            return dataSet;
        }

        public DataTable GetData(string procedureName)
        {
            var dataTable = new DataTable();
            _command = new OdbcCommand();
            _command = _dbFactory.CreateCommand();
            if (_command != null)
            {
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = procedureName;
                _command.CommandTimeout = _commandTimeOut;
                _command.Connection = _connection;
               
                _adapter = _dbFactory.CreateDataAdapter();
                if (_adapter != null) _adapter.SelectCommand = _command;
            }
            if (_adapter != null) _adapter.Fill(dataTable);
            return dataTable;
        }

        public int ExecuteStoreProcedure(string procedureName, List<Parameter> inputParameters)
        {
            _command = new OdbcCommand();
            _command = _dbFactory.CreateCommand();
            if (_command != null)
            {
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = procedureName;
                _command.CommandTimeout = _commandTimeOut;
                _command.Connection = _connection;
                foreach (var t in inputParameters)
                {
                    _parameter = _dbFactory.CreateParameter();
                    if (_parameter != null)
                    {
                        _parameter.DbType = t.Type;
                        _parameter.Value = t.Value;
                        _parameter.ParameterName = t.Name;
                        _parameter.Direction = ParameterDirection.Input;
                        _command.Parameters.Add(_parameter);
                    }
                }
                var affectedRows = _command.ExecuteNonQuery();
                return affectedRows;
            }
            return 0;
        }

        public int ExecuteStoreProcedure(string procedureName, List<Parameter> inputParameters, ref List<Parameter> outputParameters)
        {
            var affectedRows = 0;
            _command = new OdbcCommand();
            _command = _dbFactory.CreateCommand();
            if (_command != null)
            {
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = procedureName;
                _command.CommandTimeout = _commandTimeOut;
                _command.Connection = _connection;
                foreach (var t in inputParameters)
                {
                    _parameter = _dbFactory.CreateParameter();
                    if (_parameter != null)
                    {
                        _parameter.DbType = t.Type;
                        _parameter.Value = t.Value;
                        _parameter.ParameterName = t.Name;
                        _parameter.Direction = ParameterDirection.Input;
                        _command.Parameters.Add(_parameter);
                    }
                }

                foreach (var t in outputParameters)
                {
                    if (t.Type==DbType.String) {
                        _parameter = _dbFactory.CreateParameter();
                        if (_parameter != null)
                        {
                            _parameter.DbType = t.Type;
                            _parameter.Value = t.Value;
                            _parameter.ParameterName = t.Name;
                            _parameter.Direction = ParameterDirection.Output;
                            _parameter.Size = 500;
                            _command.Parameters.Add(_parameter);
                        }
                    }
                    else
                    {
                        _parameter = _dbFactory.CreateParameter();
                        if (_parameter != null)
                        {
                            _parameter.DbType = t.Type;
                            _parameter.Value = t.Value;
                            _parameter.ParameterName = t.Name;
                            _parameter.Direction = ParameterDirection.Output;
                            _command.Parameters.Add(_parameter);
                        }
                    }

                }

                affectedRows = _command.ExecuteNonQuery();

                foreach(var v in outputParameters)
                {
                    v.Value = _command.Parameters[v.Name].Value;
                }
            }
            return affectedRows;
        }

        public string ExecuteScalerQuery(string sqlQuery, List<Parameter> inputParameters)
        {
            var result = string.Empty;
            _command = new OdbcCommand();
            _command = _dbFactory.CreateCommand();
            if (_command != null)
            {
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = sqlQuery;
                _command.CommandTimeout = _commandTimeOut;
                _command.Connection = _connection;
                foreach (var t in inputParameters)
                {
                    _parameter = _dbFactory.CreateParameter();
                    if (_parameter != null)
                    {
                        _parameter.DbType = t.Type;
                        _parameter.Value = t.Value;
                        _parameter.ParameterName = t.Name;
                        _command.Parameters.Add(_parameter);
                    }
                }
                 result = Convert.ToString(_command.ExecuteScalar());
            }
            return result;
        }

        public DateTime DateExecuteScalar(string sqlQuery)
        {
            DateTime dtmReturn;
            using(var sqlCon= new SqlConnection(_connectionString))
            {
                sqlCon.Open();
                using (var scm = new SqlCommand(sqlQuery, sqlCon))
                {
                    dtmReturn = Convert.ToDateTime(scm.ExecuteScalar());
                }
                sqlCon.Close();
            }
            return dtmReturn;
        }

        public DateTime DatabaseBackupExecuteScalar(string sqlQuery)
        {
            var sqlConnectionStringBuilder = new SqlConnectionStringBuilder(_connectionString);
            DateTime dtmReturn;
            using(var sqlCon=new SqlConnection(_connectionString))
            {
                sqlCon.Open();
                using(var scm=new SqlCommand(sqlQuery + sqlConnectionStringBuilder.InitialCatalog, sqlCon))
                {
                    dtmReturn= Convert.ToDateTime(scm.ExecuteScalar());
                }
                sqlCon.Close();
            }
            return dtmReturn;

        }

        public int ExecuteQuery(string sqlQuery)
        {
            var affectedRows = 0;
            _command = new OdbcCommand();
            _command = _dbFactory.CreateCommand();
            if (_command != null)
            {
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = sqlQuery;
                _command.CommandTimeout = _commandTimeOut;
                _command.Connection = _connection;

                affectedRows = _command.ExecuteNonQuery();
            }
            return affectedRows;
        }

        public int ExecuteQuery(string sqlQuery, List<Parameter> inputParameters)
        {
            var affectedRows = 0;
            _command = new OdbcCommand();
            _command = _dbFactory.CreateCommand();
            if (_command != null)
            {
                _command.CommandType = CommandType.Text;
                _command.CommandText = sqlQuery;
                _command.CommandTimeout = _commandTimeOut;
                _command.Connection = _connection;
                foreach (var t in inputParameters)
                {
                    _parameter = _dbFactory.CreateParameter();
                    if (_parameter != null)
                    {
                        _parameter.DbType = t.Type;
                        _parameter.Value = t.Value;
                        _parameter.ParameterName = t.Name;
                        _command.Parameters.Add(_parameter);
                    }
                }
                affectedRows = _command.ExecuteNonQuery();
            }
            return affectedRows;
        }

        public int ExecuteQuery(string sqlQuery, Parameter inputParameters)
        {
            var affectedRows = 0;
            _command = new OdbcCommand();
            _command = _dbFactory.CreateCommand();
            if (_command != null)
            {
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = sqlQuery;
                _command.CommandTimeout = _commandTimeOut;
                _command.Connection = _connection;
                _parameter = _dbFactory.CreateParameter();
                if (_parameter != null)
                {
                    _parameter.DbType = inputParameters.Type;
                    _parameter.Value = inputParameters.Value;
                    _parameter.ParameterName = inputParameters.Name;
                    _command.Parameters.Add(_parameter);
                }
                affectedRows = _command.ExecuteNonQuery();
            }
            return affectedRows;
        }

        public string GetSingleString(string sqlQuery) {
            DataTable dataTable = GetDataTable(sqlQuery);
            return dataTable.Rows.Count > 0 ? Convert.ToString(dataTable.Rows[0][0]) : null;
        }

        public string GetSingleString(string sqlQuery, Parameter inputParameter)
        {
            DataTable dataTable = GetDataTable(sqlQuery,inputParameter);
            return dataTable.Rows.Count > 0 ? Convert.ToString(dataTable.Rows[0][0]) : null;
        }
        public string GetSingleString(string sqlQuery, List<Parameter> inputParameters)
        {
            DataTable dataTable = GetDataTable(sqlQuery, inputParameters);
            return dataTable.Rows.Count > 0 ? Convert.ToString(dataTable.Rows[0][0]) : null;
        }

        public decimal GetDecimalString(string sqlQuery)
        {
            DataTable dataTable = GetDataTable(sqlQuery);
            return dataTable.Rows.Count > 0 ? Convert.ToDecimal(dataTable.Rows[0][0]) : 0;
        }

        public decimal GetDecimalString(string sqlQuery, Parameter inputParameter)
        {
            DataTable dataTable = GetDataTable(sqlQuery, inputParameter);
            return dataTable.Rows.Count > 0 ? Convert.ToDecimal(dataTable.Rows[0][0]) : 0;
        }
        public decimal GetDecimalString(string sqlQuery, List<Parameter> inputParameters)
        {
            DataTable dataTable = GetDataTable(sqlQuery, inputParameters);
            return dataTable.Rows.Count > 0 ? Convert.ToDecimal(dataTable.Rows[0][0]) : 0;
        }
        public int GetSingleInt (string sqlQuery)
        {
            DataTable dataTable = GetDataTable(sqlQuery);
            return dataTable.Rows.Count > 0 ? Convert.ToInt32(dataTable.Rows[0][0]) : 0;
        }

        public int GetSingleInt(string sqlQuery, Parameter inputParameter)
        {
            DataTable dataTable = GetDataTable(sqlQuery, inputParameter);
            return dataTable.Rows.Count > 0 ? Convert.ToInt32(dataTable.Rows[0][0]) : 0;
        }
        public int GetSingleInt(string sqlQuery, List<Parameter> inputParameters)
        {
            DataTable dataTable = GetDataTable(sqlQuery, inputParameters);
            return dataTable.Rows.Count > 0 ? Convert.ToInt32(dataTable.Rows[0][0]) : 0;
        }

        public DataTable ExecuteQueryForBulkData(string procedureName,DataTable dt, string TypeName,string TableName,string UserPin) {
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand(_connectionString);
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = procedureName;
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param = cmd.Parameters.AddWithValue(TableName, dt);
            param.SqlDbType = SqlDbType.Structured;
            param.TypeName = TypeName;
            param = cmd.Parameters.AddWithValue("@Username", UserPin);
            param.SqlDbType = SqlDbType.NVarChar;
            DataTable res = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(res);
            conn.Close();
            return res;
        }

        public DataTable ExecuteStoredProcedureWithDataTable(string procedureName, DataTable dt, string TypeName, string TableName, List<Parameter>inputParameters)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand(_connectionString);
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = procedureName;
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param = cmd.Parameters.AddWithValue(TableName, dt);
            param.SqlDbType = SqlDbType.Structured;
            param.TypeName = TypeName;

            if (inputParameters!=null){
                foreach (var t in inputParameters) {
                    if (t != null)
                    {
                        param = cmd.Parameters.AddWithValue(t.Name,t.Value);
                        param.DbType = t.Type;
                    }
                }
            }
            
            DataTable res = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(res);
            conn.Close();
            return res;
        }

        public DataTable ExecuteMultiDataTable(string procedureName, List<TableParameter> tblParameters, List<Parameter> inputParameters)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand(_connectionString);
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = procedureName;
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param;

            if (tblParameters!=null) {
                foreach(var t in tblParameters)
                {
                    if (t != null)
                    {
                        param= cmd.Parameters.AddWithValue(t.TableName, t.Table);
                        param.SqlDbType = SqlDbType.Structured;
                        param.TypeName = t.TypeName;
                    }
                }
            }
            
            

            if (inputParameters != null)
            {
                foreach (var t in inputParameters)
                {
                    if (t != null)
                    {
                        param = cmd.Parameters.AddWithValue(t.Name, t.Value);
                        param.DbType = t.Type;
                    }
                }
            }

            DataTable res = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(res);
            conn.Close();
            return res;
        }

        public DataTable ExecuteProcedureWithOutputParams(string procedureName, List<TableParameter> tblParameters, List<Parameter> inputParameters
            , List<Parameter> outputParameter)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand(_connectionString);
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = procedureName;
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param;

            if (tblParameters != null)
            {
                foreach (var t in tblParameters)
                {
                    if (t != null)
                    {
                        param = cmd.Parameters.AddWithValue(t.TableName, t.Table);
                        param.SqlDbType = SqlDbType.Structured;
                        param.TypeName = t.TypeName;
                    }
                }
            }



            if (inputParameters != null)
            {
                foreach (var t in inputParameters)
                {
                    if (t != null)
                    {
                        param = cmd.Parameters.AddWithValue(t.Name, t.Value);
                        param.DbType = t.Type;
                    }
                }
            }
            if (outputParameter != null)
            {
                foreach (var t in outputParameter)
                {
                    if (t != null)
                    {
                        param = cmd.Parameters.AddWithValue(t.Name, t.Value);
                        param.DbType = t.Type;
                        param.Direction = ParameterDirection.Output;
                    }
                }
            }

            DataTable res = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(res);
            conn.Close();

            foreach (var v in outputParameter)
            {
                v.Value = cmd.Parameters[v.Name].Value;
            }
            return res;
        }

        private bool _disposed;
        protected virtual void Dispose(bool disposing) {
            if (!_disposed) {
                if (disposing)
                {
                    _connection.Dispose();
                    _command.Dispose();
                    _adapter.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}