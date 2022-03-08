using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace DG3.PracticeExam.DataAccess.DataManager
{
    public sealed class SqlDbManager : IDbManager
    {
        private SqlCommand command;
        private SqlConnection connection = null;
        private SqlTransaction transaction = null;

        private string storedProcedure = string.Empty;
        private string sqlScript = string.Empty;

        private readonly string connectionString;


        public SqlDbManager(string connectionString)
        {
            this.connectionString = connectionString;
            connection = new SqlConnection(connectionString);
            connection.Open();
        }

        public void AssignStoredProc(string storedProcedureName)
        {
            this.storedProcedure = storedProcedureName;

            command = new SqlCommand()
            { 
                CommandTimeout = 300,
                CommandText = storedProcedureName,
                Connection = connection,
                CommandType = CommandType.StoredProcedure,
            };


        }

        public void AssignSQLScript(string sql)
        {
            this.sqlScript = sql;
            command = new SqlCommand()
            {
                CommandTimeout = 300,
                CommandText = sql,
                Connection = connection,
                CommandType = CommandType.Text,
            };
        }

        public void SetInputParams(string paramName, DbType type, object value)
        {
            var param = new SqlParameter()
            {
                ParameterName = paramName,
                DbType = type,
                Value = value,
                Direction = ParameterDirection.Input,
                IsNullable = true,
            };

            command.Parameters.Add(param);

            //command.Parameters.AddWithValue(paramName, value).Direction = ParameterDirection.Input;
        }
           

        public void SetOutputParams(string paramName, DbType type)
        {
            var param = new SqlParameter()
            {
                ParameterName = paramName,
                Direction = ParameterDirection.Output,
                DbType = type
            };

            command.Parameters.Add(param);
        }

        public object GetOutputParams(string paramName)
        {
            int returnId = 0;

            if (command.Parameters[paramName]?.Value is not null)
            {
                int.TryParse(command.Parameters[paramName]?.Value?.ToString(), out returnId);
                return returnId;
            }

            return returnId;
        }

        public bool RunNonQuery()
        {
            bool executeSuccessful = true;
            try
            {
                int rowsAffected = 0;
                if (transaction != null)
                    rowsAffected = command.ExecuteNonQuery();
                else
                    rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected <= 0)
                {
                    //executeSuccessful = false;
                    if (storedProcedure.Length > 0)
                    {
                        Debug.WriteLine(String.Format("WARNING : '{0}' return 0 affected records.", storedProcedure));
                    }
                    else
                    {
                        Debug.WriteLine(String.Format("WARNING : '{0}' return 0 affected records.", sqlScript));
                    }
                }
            }
            catch (Exception e)
            {
                executeSuccessful = false;
                Debug.WriteLine(e.Message);
                throw e;
            }
            finally
            {
                command.Dispose();
            }

            return executeSuccessful;
        }

        public DataSet RunQuery()
        {
            DataSet set = new DataSet();
            try
            {
                //set = command.ExecuteDataSet(command);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);

                sqlDataAdapter.Fill(set);
            }
            catch (Exception e)
            {
                set = new DataSet();
                Debug.WriteLine(e.Message);
                throw e;
            }
            finally
            {
                command.Dispose();
            }

            return set;
        }

        public IDataReader RunQueryReader() => command.ExecuteReader();

        public void CreateTransaction()
        {
            if (connection == null)
            {
                connection = new SqlConnection(this.connectionString);
            }

            connection.Close();
            connection.Open();
            transaction = connection.BeginTransaction();
        }


        public void CreateReadUncommittedTransaction()
        {
            if (connection == null)
            {
                connection = new SqlConnection(this.connectionString);
            }
            connection.Close();
            connection.Open();
            transaction = connection.BeginTransaction(IsolationLevel.ReadUncommitted);
        }


        public void CommitTransaction()
        {
            if (transaction != null)
            {
                transaction.Commit();
            }

            EndTransaction();
        }

        public void RollbackTransaction()
        {
            if (transaction != null)
            {
                transaction.Rollback();
            }

            EndTransaction();
        }

        private void EndTransaction()
        {
            transaction.Dispose();
            
            connection.Close();
            connection.Dispose();
        }

        public void Dispose()
        {
            transaction?.Dispose();
            command?.Dispose();
            connection?.Dispose();
        }


    }
}
