using System;
using System.Data;
using System.Xml;
using System.Data.OracleClient;
using System.Collections;
//using Oracle.DataAccess.Client;
//using Oracle.DataAccess.Types;


namespace Smart.OracleDBServices
{
    //[Transaction(TransactionOption.RequiresNew)]
    //[ObjectPooling(true, 5, 10)]
    public sealed class OracleHelper //: ServicedComponent
    {
        #region private utility methods & constructors
        //Since this class provides only static methods, make the default constructor private to prevent 
        //instances from being created with "new OracleHelper()".
        public OracleHelper() { }

        /// <summary>
        /// This method is used to attach array's of OracleParameters to an OracleCommand.
        /// 
        /// This method will assign a value of DbNull to any parameter with a direction of
        /// InputOutput and a value of null.  
        /// 
        /// This behavior will prevent default values from being used, but
        /// this will be the less common case than an intended pure output parameter (derived as InputOutput)
        /// where the user provided no input value.
        /// </summary>
        /// <param name="command">The command to which the parameters will be added</param>
        /// <param name="commandParameters">an array of OracleParameters tho be added to command</param>
        private static void AttachParameters(OracleCommand command, OracleParameter[] commandParameters)
        {
            foreach (OracleParameter p in commandParameters)
            {
                //check for derived output value with no value assigned
                if ((p.Direction == ParameterDirection.InputOutput) && (p.Value == null) || (p.Value==""))
                {
                    p.Value = DBNull.Value;
                }
                if (p.Value != null &&  p.Value.GetType() == typeof(String))
                {
                    if (p.Value.ToString().Length > 1000)
                    {
                        p.OracleType = OracleType.Clob;
                        //p.DbType = DbType.Xml;
                        p.Value = DBNull.Value;
                    }

                }

                command.Parameters.Add(p);
            }
        }

        /// <summary>
        /// This method assigns an array of values to an array of OracleParameters.
        /// </summary>
        /// <param name="commandParameters">array of OracleParameters to be assigned values</param>
        /// <param name="parameterValues">array of objects holding the values to be assigned</param>
        private static void AssignParameterValues(OracleParameter[] commandParameters, object[] parameterValues)
        {
            if ((commandParameters == null) || (parameterValues == null))
            {
                //do nothing if we get no data
                return;
            }

            // we must have the same number of values as we pave parameters to put them in
            //if (commandParameters.Length != parameterValues.Length)
            //{
            //    throw new ArgumentException("Parameter count does not match Parameter Value count.");
            //}
            int intParamInputNum = 0;
            for (int i = 0; i < commandParameters.Length; i++)
            {
                if (commandParameters[i].Direction.ToString() == "Input")
                {
                    intParamInputNum++;
                }
            }
            if (intParamInputNum != parameterValues.Length)
            {
                throw new ArgumentException("Parameter count does not match Parameter Value count.");
            }
            //iterate through the OracleParameters, assigning the values from the corresponding position in the 
            //value array
            for (int i = 0, j = intParamInputNum; i < j; i++)
            {
                commandParameters[i].Value = parameterValues[i];
            }
        }

        /// <summary>
        /// This method opens (if necessary) and assigns a connection, transaction, command type and parameters 
        /// to the provided command.
        /// </summary>
        /// <param name="command">the OracleCommand to be prepared</param>
        /// <param name="connection">a valid OracleConnection, on which to execute this command</param>
        /// <param name="transaction">a valid OracleTransaction, or 'null'</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or PL/SQL command</param> 
        /// <param name="commandParameters">an array of OracleParameters to be associated with the command or 'null' if no parameters are required</param>
        private static void PrepareCommand(OracleCommand command, OracleConnection connection, OracleTransaction transaction, CommandType commandType, string commandText, OracleParameter[] commandParameters)
        {
            //if the provided connection is not open, we will open it
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            //associate the connection with the command
            command.Connection = connection;

            //set the command text (stored procedure name or Oracle statement)
            command.CommandText = commandText;

            //if we were provided a transaction, assign it.
            if (transaction != null)
            {
                command.Transaction = transaction;
            }

            //command.Transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);
            //set the command type
            command.CommandType = commandType;

            //attach the command parameters if they are provided
            if (commandParameters != null)
            {
                AttachParameters(command, commandParameters);
            }

            return;
        }

        private static void AssignParameterValues(OracleParameter[] p_arrSQLParameter, object[] p_arrValue, bool includeReturnValueParameter)
        {
            if ((p_arrSQLParameter == null) || (p_arrValue == null))
            {
                return;
            }
            int ProParameterLength = 0;
            //Oracle Parameter have return parameter -> Parameter Input + 1 = Procedure Paramter
            if (includeReturnValueParameter == true)
            {
                ProParameterLength = p_arrValue.Length + 1;
            }
            else
            {
                ProParameterLength = p_arrValue.Length;
            }
            if (p_arrSQLParameter.Length != ProParameterLength)
            {
                throw (new Exception("Parameter count does not match Parameter Value count."));
            }

            int i = 0;
            int j = p_arrValue.Length;
            while (i < j)
            {
                if (p_arrValue[i] != null)
                {
                    p_arrSQLParameter[i].Value = p_arrValue[i];
                }
                else
                {
                    p_arrSQLParameter[i].Value = DBNull.Value;
                }
                i++;
            }
        }
        private static void PrepareCommand(OracleCommand p_cmd, OracleConnection p_conn, OracleTransaction p_trans, string p_strSPName, OracleParameter[] p_arrSQLParameter)
        {
            //if the provided connection is not open, we will open it
            if (p_conn.State != ConnectionState.Open)
            {
                p_conn.Open();
            }

            //associate the connection with the command
            p_cmd.Connection = p_conn;

            //set the command text (stored procedure name or SQL statement)
            p_cmd.CommandText = p_strSPName;

            //if we were provided a transaction, assign it.
            if (p_trans != null)
            {
                p_cmd.Transaction = p_trans;
            }

            //set the command type
            p_cmd.CommandType = CommandType.StoredProcedure;

            //attach the command parameters if they are provided
            if (p_arrSQLParameter != null)
            {
                AttachParameters(p_cmd, p_arrSQLParameter);
            }
        }
        #endregion private utility methods & constructors
        #region "ExecuteQuery"
        public static void ExecuteQuery(string p_strConnStr, DataSet ds, string strsql, object[] p_arrValue)
        {
            try
            {
                OracleDataAdapter adapter = new OracleDataAdapter();
                OracleConnection mConnection = new OracleConnection(p_strConnStr);
                try
                {
                    OracleCommand cmd = new OracleCommand();
                    OracleDataAdapter da = new OracleDataAdapter(cmd);
                    adapter.SelectCommand = new OracleCommand(strsql, mConnection);
                    adapter.Fill(ds, "new");
                }

                catch (Exception ex)
                {
                    throw (ex);

                }
                finally
                {
                    if (mConnection.State == ConnectionState.Open)
                    {
                        mConnection.Close();
                    }
                    mConnection.Dispose();
                }
             
            }
            catch (Exception ex)
            {
                throw (ex);
            }


        }
  
        #endregion
        #region ExecuteNonQuery

        /// <summary>
        /// Execute an OracleCommand (that returns no resultset and takes no parameters) against the database specified in 
        /// the connection string. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders");
        /// </remarks>
        /// <param name="connectionString">a valid connection string for an OracleConnection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or PL/SQL command</param>  
        /// <returns>an int representing the number of rows affected by the command</returns>
        public static int ExecuteNonquery(string connectionString, CommandType commandType, string commandText)
        {
            //pass through the call providing null for the set of OracleParameters
            return ExecuteNonquery(connectionString, commandType, commandText, (OracleParameter[])null);
        }

        /// <summary>
        /// Execute an OracleCommand (that returns no resultset) against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new OracleParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">a valid connection string for an OracleConnection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or PL/SQL command</param>  
        /// <param name="commandParameters">an array of OracleParameters used to execute the command</param>
        /// <returns>an int representing the number of rows affected by the command</returns>
        public static int ExecuteNonquery(string connectionString, CommandType commandType, string commandText, params OracleParameter[] commandParameters)
        {
            //create & open an OracleConnection, and dispose of it after we are done.
            using (OracleConnection cn = new OracleConnection(connectionString))
            {
                cn.Open();

                //call the overload that takes a connection in place of the connection string
                return ExecuteNonquery(cn, commandType, commandText, commandParameters);
            }
        }

        /// <summary>
        /// Execute a stored procedure via an OracleCommand (that returns no resultset) against the database specified in 
        /// the connection string using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// 
        /// e.g.:  
        ///  int result = ExecuteNonQuery(connString, "PublishOrders", 24, 36);
        /// </remarks>
        /// <param name="connectionString">a valid connection string for an OracleConnection</param>
        /// <param name="spName">the name of the stored procedure</param>
        /// <param name="parameterValues">an array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>an int representing the number of rows affected by the command</returns>
        public static int ExecuteNonquery(string connectionString, string spName, params object[] parameterValues)
        {
            //if we got parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                //pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                OracleParameter[] commandParameters = OracleHelperParameterCache.GetSpParameterSet(connectionString, spName);

                //assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                //call the overload that takes an array of OracleParameters
                return ExecuteNonquery(connectionString, CommandType.StoredProcedure, spName, commandParameters);
            }
            //otherwise we can just call the SP without params
            else
            {
                return ExecuteNonquery(connectionString, CommandType.StoredProcedure, spName);
            }
        }

        /// <summary>
        /// Execute an OracleCommand (that returns no resultset and takes no parameters) against the provided OracleConnection. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(conn, CommandType.StoredProcedure, "PublishOrders");
        /// </remarks>
        /// <param name="connection">a valid OracleConnection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or PL/SQL command</param>
        /// <returns>an int representing the number of rows affected by the command</returns>
        public static int ExecuteNonquery(OracleConnection connection, CommandType commandType, string commandText)
        {
            //pass through the call providing null for the set of OracleParameters
            return ExecuteNonquery(connection, commandType, commandText, (OracleParameter[])null);
        }

        /// <summary>
        /// Execute an OracleCommand (that returns no resultset) against the specified OracleConnection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(conn, CommandType.StoredProcedure, "PublishOrders", new OracleParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connection">a valid OracleConnection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or PL/SQL command</param>  
        /// <param name="commandParameters">an array of OracleParameters used to execute the command</param>
        /// <returns>an int representing the number of rows affected by the command</returns>
        public static int ExecuteNonquery(OracleConnection connection, CommandType commandType, string commandText, params OracleParameter[] commandParameters)
        {
            //create a command and prepare it for execution
            OracleCommand cmd = new OracleCommand();
            PrepareCommand(cmd, connection, (OracleTransaction)null, commandType, commandText, commandParameters);
            int result = 0;
            try
            {
                //finally, execute the command.
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        /// <summary>
        /// Execute a stored procedure via an OracleCommand (that returns no resultset) against the specified OracleConnection 
        /// using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// 
        /// e.g.:  
        ///  int result = ExecuteNonQuery(conn, "PublishOrders", 24, 36);
        /// </remarks>
        /// <param name="connection">a valid OracleConnection</param>
        /// <param name="spName">the name of the stored procedure</param>
        /// <param name="parameterValues">an array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>an int representing the number of rows affected by the command</returns>
        public static int ExecuteNonquery(OracleConnection connection, string spName, params object[] parameterValues)
        {
            //if we got parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                //pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                OracleParameter[] commandParameters = OracleHelperParameterCache.GetSpParameterSet(connection.ConnectionString, spName);

                //assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                //call the overload that takes an array of OracleParameters
                return ExecuteNonquery(connection, CommandType.StoredProcedure, spName, commandParameters);
            }
            //otherwise we can just call the SP without params
            else
            {
                return ExecuteNonquery(connection, CommandType.StoredProcedure, spName);
            }
        }

        /// <summary>
        /// Execute an OracleCommand (that returns no resultset and takes no parameters) against the provided OracleTransaction. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(trans, CommandType.StoredProcedure, "PublishOrders");
        /// </remarks>
        /// <param name="transaction">a valid OracleTransaction</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or PL/SQL command</param>  
        /// <returns>an int representing the number of rows affected by the command</returns>
        public static int ExecuteNonquery(OracleTransaction transaction, CommandType commandType, string commandText)
        {
            //pass through the call providing null for the set of OracleParameters
            return ExecuteNonquery(transaction, commandType, commandText, (OracleParameter[])null);
        }

        /// <summary>
        /// Execute an OracleCommand (that returns no resultset) against the specified OracleTransaction
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(trans, CommandType.StoredProcedure, "GetOrders", new OracleParameter("@prodid", 24));
        /// </remarks>
        /// <param name="transaction">a valid OracleTransaction</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or PL/SQL command</param>  
        /// <param name="commandParameters">an array of OracleParameters used to execute the command</param>
        /// <returns>an int representing the number of rows affected by the command</returns>
        public static int ExecuteNonquery(OracleTransaction transaction, CommandType commandType, string commandText, params OracleParameter[] commandParameters)
        {
            //create a command and prepare it for execution
            OracleCommand cmd = new OracleCommand();
            PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters);

            //finally, execute the command.
            return cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Execute a stored procedure via an OracleCommand (that returns no resultset) against the specified 
        /// OracleTransaction using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// 
        /// e.g.:  
        ///  int result = ExecuteNonQuery(conn, trans, "PublishOrders", 24, 36);
        /// </remarks>
        /// <param name="transaction">a valid OracleTransaction</param>
        /// <param name="spName">the name of the stored procedure</param>
        /// <param name="parameterValues">an array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>an int representing the number of rows affected by the command</returns>
        public static int ExecuteNonquery(OracleTransaction transaction, string spName, params object[] parameterValues)
        {
            //if we got parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                //pull the parameters for this stored procedure from the parameter cache (or discover them & populet the cache)
                OracleParameter[] commandParameters = OracleHelperParameterCache.GetSpParameterSet(transaction.Connection.ConnectionString, spName);

                //assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                //call the overload that takes an array of OracleParameters
                return ExecuteNonquery(transaction, CommandType.StoredProcedure, spName, commandParameters);
            }
            //otherwise we can just call the SP without params
            else
            {
                return ExecuteNonquery(transaction, CommandType.StoredProcedure, spName);
            }
        }

        //public static DataSet ExecuteNonqueryToDataset(string p_strConnStr, string p_strSPname, params object[] p_arrValue)
        //{
        //    try
        //    {
        //        bool includeReturnValueParameter = false;
        //        DataSet dset = new DataSet();
        //        int intValue = -1;
        //        string strValue = "";
        //        //if ((p_arrValue != null))// && (p_arrValue.Length > 0))
        //        //{
        //        DatabaseServiceCache idb = new DatabaseServiceCache();
        //        OracleParameter[] arrSQLParameter = idb.GetSpParameterSet(p_strConnStr, p_strSPname, includeReturnValueParameter);
        //        //Kiem tra xem co bao nhieu record set duoc out ra                   
        //        for (short i = 0; i <= arrSQLParameter.Length - 1; i++)
        //        {
        //            if (arrSQLParameter[i].Direction == ParameterDirection.Output)
        //            {
        //                Array.Resize(ref p_arrValue, (p_arrValue == null ? 0 : p_arrValue.Length) + 1);
        //                if (arrSQLParameter[i].DbType == System.Data.DbType.AnsiString)
        //                {
        //                    p_arrValue[p_arrValue.Length - 1] = strValue;
        //                }
        //                if (arrSQLParameter[i].DbType == System.Data.DbType.VarNumeric)
        //                {
        //                    p_arrValue[p_arrValue.Length - 1] = intValue;
        //                }
        //                if (arrSQLParameter[i].DbType == System.Data.DbType.Object)
        //                {
        //                    p_arrValue[p_arrValue.Length - 1] = dset;
        //                }
                        
        //            }
        //        }
        //        AssignParameterValues(arrSQLParameter, p_arrValue, includeReturnValueParameter);
        //        return ExecuteNonQueryToDatatableArray(p_strConnStr, p_strSPname, arrSQLParameter);

        //        // }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw (ex);
        //    }

        //    return null;
        //}
        public static Object ExecuteNonqueryToDataset(string p_strConnStr, string p_strSPname, params object[] p_arrValue)
        {
            try
            {
                bool includeReturnValueParameter = false;
                Object obj = new Object();
                int intValue = -1;
                string strValue = "";
                //if ((p_arrValue != null))// && (p_arrValue.Length > 0))
                //{
                DatabaseServiceCache idb = new DatabaseServiceCache();
                OracleParameter[] arrSQLParameter = idb.GetSpParameterSet(p_strConnStr, p_strSPname, includeReturnValueParameter);
                //Kiem tra xem co bao nhieu record set duoc out ra                   
                for (short i = 0; i <= arrSQLParameter.Length - 1; i++)
                {
                    if (arrSQLParameter[i].Direction == ParameterDirection.Output)
                    {
                        Array.Resize(ref p_arrValue, (p_arrValue == null ? 0 : p_arrValue.Length) + 1);
                        //if (arrSQLParameter[i].DbType == System.Data.DbType.AnsiString)
                        //{
                        //    p_arrValue[p_arrValue.Length - 1] = strValue;
                        //}
                        //if (arrSQLParameter[i].DbType == System.Data.DbType.VarNumeric)
                        //{
                        //    p_arrValue[p_arrValue.Length - 1] = intValue;
                        //}
                        //if (arrSQLParameter[i].DbType == System.Data.DbType.Object)
                        //{
                        //    p_arrValue[p_arrValue.Length - 1] = dset;
                        //}
                        p_arrValue[p_arrValue.Length - 1] = obj;

                    }
                }
                AssignParameterValues(arrSQLParameter, p_arrValue, includeReturnValueParameter);
                return ExecuteNonQueryToDatatableArray(p_strConnStr, p_strSPname, arrSQLParameter);

                // }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return null;
        }
        private static Object ExecuteNonQueryToDatatableArray(string p_strConnStr, string p_strStoreName, params OracleParameter[] p_arrSQLParameter)
        {
            OracleConnection conn = new OracleConnection(p_strConnStr);
            OracleCommand cmd = new OracleCommand();
            int result = -5;
            object[] objReturn = { -1000 };

            DataSet dset = new DataSet();
            try
            {
                PrepareCommand(cmd, conn, ((OracleTransaction)null), p_strStoreName, p_arrSQLParameter);

                // Execute Sql Command
                result = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                short j = 0;
                for (short i = 0; i <= p_arrSQLParameter.Length - 1; i++)
                {
                    if (p_arrSQLParameter[i].Direction == ParameterDirection.Output)
                    {
                        Array.Resize(ref objReturn, j + 1);
                        objReturn[j] = p_arrSQLParameter[i].Value;
                        if (p_arrSQLParameter[i].Value is System.Data.OracleClient.OracleDataReader)
                        {
                            DataTable dt = new DataTable();
                            dt.Load((IDataReader)p_arrSQLParameter[i].Value);
                            dt.TableName = p_strStoreName + i.ToString();
                            dset.Tables.Add(dt);

                            //							break;
                        }

                    }

                    else
                    {
                        //return (Object)p_arrSQLParameter[i].Value;
                    }
                }
                return (Object)dset;

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                cmd.Dispose();
            }


            return null;
        }
        //private static DataSet ExecuteNonQueryToDatatableArray(string p_strConnStr, string p_strStoreName, params OracleParameter[] p_arrSQLParameter)
        //{
        //    OracleConnection conn = new OracleConnection(p_strConnStr);
        //    OracleCommand cmd = new OracleCommand();
        //    int result = -5;
        //    object[] objReturn = { -1000 };

        //    DataSet dset = new DataSet();
        //    try
        //    {
        //        PrepareCommand(cmd, conn, ((OracleTransaction)null), p_strStoreName, p_arrSQLParameter);

        //        // Execute Sql Command
        //        result = cmd.ExecuteNonQuery();
        //        cmd.Parameters.Clear();
        //        short j = 0;
        //        for (short i = 0; i <= p_arrSQLParameter.Length - 1; i++)
        //        {
        //            if (p_arrSQLParameter[i].Direction == ParameterDirection.Output)
        //            {
        //                Array.Resize(ref objReturn, j + 1);
        //                objReturn[j] = p_arrSQLParameter[i].Value.ToString();
        //                if (p_arrSQLParameter[i].Value is System.Data.OracleClient.OracleDataReader)
        //                {
        //                    DataTable dt = new DataTable();
        //                    dt.Load((IDataReader)p_arrSQLParameter[i].Value);
        //                    dt.TableName = p_strStoreName + i.ToString();
        //                    dset.Tables.Add(dt);

        //                    //							break;
        //                }

        //            }
        //        }
        //        return dset;

        //    }
        //    catch (Exception ex)
        //    {
        //        throw (ex);
        //    }
        //    finally
        //    {
        //        if (conn.State == ConnectionState.Open)
        //        {
        //            conn.Close();
        //        }
        //        cmd.Dispose();
        //    }


        //    return null;
        //}
        #endregion ExecuteNonQuery

        #region ExecuteDataSet

        /// <summary>
        /// Execute an OracleCommand (that returns a resultset and takes no parameters) against the database specified in 
        /// the connection string. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  DataSet ds = ExecuteDataset(connString, CommandType.StoredProcedure, "GetOrders");
        /// </remarks>
        /// <param name="connectionString">a valid connection string for an OracleConnection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or PL/SQL command</param>  
        /// <returns>a dataset containing the resultset generated by the command</returns>
        /// 
        public static DataSet ExecuteDataset(string connectionString, CommandType commandType, string commandText)
        {
            //pass through the call providing null for the set of OracleParameters
            return ExecuteDataset(connectionString, commandType, commandText, (OracleParameter[])null);
        }

        /// <summary>
        /// Execute an OracleCommand (that returns a resultset) against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  DataSet ds = ExecuteDataset(connString, CommandType.StoredProcedure, "GetOrders", new OracleParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">a valid connection string for an OracleConnection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or PL/SQL command</param> 
        /// <param name="commandParameters">an array of OracleParameters used to execute the command</param>
        /// <returns>a dataset containing the resultset generated by the command</returns>
        public static DataSet ExecuteDataset(string connectionString, CommandType commandType, string commandText, params OracleParameter[] commandParameters)
        {
            //create & open an OracleConnection, and dispose of it after we are done.
            using (OracleConnection cn = new OracleConnection(connectionString))
            {
                try
                {
                    cn.Open();

                    //call the overload that takes a connection in place of the connection string
                    return ExecuteDataset(cn, commandType, commandText, commandParameters);
                }
                catch (Exception ex)
                {
                    throw (ex);                    

                }
                finally
                {
                    if (cn.State == ConnectionState.Open)
                    {
                        cn.Close();
                    }
                    cn.Dispose();
                }
            }
        }

        /// <summary>
        /// Execute a stored procedure via an OracleCommand (that returns a resultset) against the database specified in 
        /// the conneciton string using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// 
        /// e.g.:  
        ///  DataSet ds = ExecuteDataset(connString, "GetOrders", 24, 36);
        /// </remarks>
        /// <param name="connectionString">a valid connection string for an OracleConnection</param>
        /// <param name="spName">the name of the stored procedure</param>
        /// <param name="parameterValues">an array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>a dataset containing the resultset generated by the command</returns>
        public static DataSet ExecuteDataset(string connectionString, string spName, params object[] parameterValues)
        {
            OracleParameter c1 = new OracleParameter();
            c1.OracleType = OracleType.Cursor;
            c1.Direction = ParameterDirection.Output;
            c1.ParameterName = spName;

            /* Su dung AssignParameterValues nen rao doan code nay lai
             * int i = 0;
            object[] pp = new object[parameterValues.Length + 1];
            for (; i < parameterValues.Length; i++)
                pp[i] = parameterValues.GetValue(i);
            pp[i] = c1;
            parameterValues = pp;*/

            //if we got parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                //pull the parameters for this stored procedure from the parameter cache (or discover them & populet the cache)
                OracleParameter[] commandParameters = OracleHelperParameterCache.GetSpParameterSet(connectionString, spName);

                //assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                //call the overload that takes an array of OracleParameters
                return ExecuteDataset(connectionString, CommandType.StoredProcedure, spName, commandParameters);
            }
            //otherwise we can just call the SP without params
            else
            {
                return ExecuteDataset(connectionString, CommandType.StoredProcedure, spName);
            }
        }

        /// <summary>
        /// author : huynt10
        /// purpose : Get RecordSet from Procedure Store statement
        /// </summary>
        /// <param name="connectionString">OracleConnectionString</param>
        /// <param name="spName">Procedure Store Name</param>
        /// <param name="package">Package Name</param>
        /// <param name="parameterValues">Paramaters</param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(string connectionString, string spName, params object[] parameterValues)
        {
            OracleParameter c1 = new OracleParameter();
            c1.OracleType = OracleType.Cursor;
            c1.Direction = ParameterDirection.Output;
            c1.ParameterName = spName;

            /* Su dung AssignParameterValues nen rao doan code nay lai
             * int i = 0;
            object[] pp = new object[parameterValues.Length + 1];
            for (; i < parameterValues.Length; i++)
                pp[i] = parameterValues.GetValue(i);
            pp[i] = c1;
            parameterValues = pp;*/
            if (parameterValues == null)
            {
                parameterValues = new object[] { };
            }
            if ((parameterValues != null) && (parameterValues.Length >= 0))
            {
                //pull the parameters for this stored procedure from the parameter cache (or discover them & populet the cache)
                OracleParameter[] commandParameters = OracleHelperParameterCache.GetSpParameterSet(connectionString, spName);

                //assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                //call the overload that takes an array of OracleParameters
                return ExecuteDataset(connectionString, CommandType.StoredProcedure, spName, commandParameters).Tables[0];

            }
            //otherwise we can just call the SP without params
            return null;

        }




        public static void ExecuteDataTable(string connectionString, ref DataTable dt, string spName, params object[] parameterValues)
        {
            OracleParameter c1 = new OracleParameter();
            c1.OracleType = OracleType.Cursor;
            c1.Direction = ParameterDirection.Output;
            c1.ParameterName = spName;

            /* Su dung AssignParameterValues nen rao doan code nay lai
             * int i = 0;
            object[] pp = new object[parameterValues.Length + 1];
            for (; i < parameterValues.Length; i++)
                pp[i] = parameterValues.GetValue(i);
            pp[i] = c1;
            parameterValues = pp;*/

            if (parameterValues == null)
            {
                parameterValues = new object[] { };
            }
            if ((parameterValues != null) && (parameterValues.Length >= 0))
            {
                //pull the parameters for this stored procedure from the parameter cache (or discover them & populet the cache)
                OracleParameter[] commandParameters = OracleHelperParameterCache.GetSpParameterSet(connectionString, spName);

                //assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                //call the overload that takes an array of OracleParameters
                dt = ExecuteDataset(connectionString, CommandType.StoredProcedure, spName, commandParameters).Tables[0];

            }
            //otherwise we can just call the SP 

        }

        public static void FillDataTable(string connectionString, ref DataTable dt, string spName, params object[] parameterValues)
        {
            OracleParameter c1 = new OracleParameter();
            c1.OracleType = OracleType.Cursor;
            c1.Direction = ParameterDirection.InputOutput;
            c1.ParameterName = spName;
            //int i = 0;
            //object[] pp = new object[parameterValues.Length + 1];
            //for (; i < parameterValues.Length; i++)
            //    pp[i] = parameterValues.GetValue(i);
            //pp[i] = c1;

            //parameterValues = pp;
            if (parameterValues == null)
            {
                parameterValues = new object[] { };
            }
            if ((parameterValues != null) && (parameterValues.Length >= 0))
            {
                //pull the parameters for this stored procedure from the parameter cache (or discover them & populet the cache)
                OracleParameter[] commandParameters = OracleHelperParameterCache.GetSpParameterSet(connectionString, spName);

                //assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                //call the overload that takes an array of OracleParameters
                dt = ExecuteDataset(connectionString, CommandType.StoredProcedure, spName, commandParameters).Tables[0];

            }
            //otherwise we can just call the SP without params
            //else
            //{
            //    dt = ExecuteDataset(connectionString, CommandType.StoredProcedure, spName).Tables[0];

            //}

        }
        public static void FillDataSet(string connectionString, DataSet ds, string spName, params object[] parameterValues)
        {
            OracleConnection conn = new OracleConnection(connectionString);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            // OracleParameter[] orclParam= new OracleParameter[parameterValues.Length+1];


            OracleParameter c1 = new OracleParameter();
            c1.OracleType = OracleType.Cursor;
            c1.Direction = ParameterDirection.InputOutput;
            c1.ParameterName = spName;
            /* Su dung AssignParameterValues nen rao doan code nay lai
             * int i = 0;
            object[] pp = new object[parameterValues.Length + 1];
            for (; i < parameterValues.Length; i++)
                pp[i] = parameterValues.GetValue(i);
            pp[i] = c1;
            parameterValues = pp;*/


            OracleParameter[] commandParameters = OracleHelperParameterCache.GetSpParameterSet(connectionString, spName);

            //assign the provided values to these parameters based on parameter order
            if (parameterValues == null)
            {
                parameterValues = new object[] { };
            }
            AssignParameterValues(commandParameters, parameterValues);

            try
            {
                PrepareCommand(cmd, conn, (OracleTransaction)null, CommandType.StoredProcedure, spName, commandParameters);
                da.Fill(ds);
                if (ds != null)
                {
                    int j = parameterValues.Length;
                    for (int k = 0; k < ds.Tables.Count; k++)
                    {
                        ds.Tables[k].TableName = commandParameters[k+j].ParameterName.ToString();
                    }
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                cmd.Dispose();
                da.Dispose();
            }

        }
        /// <summary>
        /// Execute an OracleCommand (that returns a resultset and takes no parameters) against the provided OracleConnection. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  DataSet ds = ExecuteDataset(conn, CommandType.StoredProcedure, "GetOrders");
        /// </remarks>
        /// <param name="connection">a valid OracleConnection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or PL/SQL command</param>
        /// <returns>a dataset containing the resultset generated by the command</returns>
        public static DataSet ExecuteDataset(OracleConnection connection, CommandType commandType, string commandText)
        {
            //pass through the call providing null for the set of OracleParameters
            return ExecuteDataset(connection, commandType, commandText, (OracleParameter[])null);
        }

        /// <summary>
        /// Execute an OracleCommand (that returns a resultset) against the specified OracleConnection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  DataSet ds = ExecuteDataset(conn, CommandType.StoredProcedure, "GetOrders", new OracleParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connection">a valid OracleConnection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or PL/SQL command</param> 
        /// <param name="commandParameters">an array of OracleParameters used to execute the command</param>
        /// <returns>a dataset containing the resultset generated by the command</returns>
        public static DataSet ExecuteDataset(OracleConnection connection, CommandType commandType, string commandText, params OracleParameter[] commandParameters)
        {
            //create a command and prepare it for execution
            OracleCommand cmd = new OracleCommand();
            PrepareCommand(cmd, connection, (OracleTransaction)null, commandType, commandText, commandParameters);

            //create the DataAdapter & DataSet
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            DataSet ds = new DataSet();

            //fill the DataSet using default values for DataTable names, etc.
            da.Fill(ds);

            //return the dataset
            return ds;
        }
        /// <summary>
        /// Execute a stored procedure via an OracleCommand (that returns a resultset) against the specified OracleConnection 
        /// using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// 
        /// e.g.:  
        ///  DataSet ds = ExecuteDataset(conn, "GetOrders", 24, 36);
        /// </remarks>
        /// <param name="connection">a valid OracleConnection</param>
        /// <param name="spName">the name of the stored procedure</param>
        /// <param name="parameterValues">an array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>a dataset containing the resultset generated by the command</returns>
        public static DataSet ExecuteDataset(OracleConnection connection, string spName, params object[] parameterValues)
        {
            //if we got parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length >= 0))
            {
                //pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                OracleParameter[] commandParameters = OracleHelperParameterCache.GetSpParameterSet(connection.ConnectionString, spName);

                //assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                //call the overload that takes an array of OracleParameters
                return ExecuteDataset(connection, CommandType.StoredProcedure, spName, commandParameters);
            }
            //otherwise we can just call the SP without params
            else
            {
                return ExecuteDataset(connection, CommandType.StoredProcedure, spName);
            }
        }

        /// <summary>
        /// Execute an OracleCommand (that returns a resultset and takes no parameters) against the provided OracleTransaction. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  DataSet ds = ExecuteDataset(trans, CommandType.StoredProcedure, "GetOrders");
        /// </remarks>
        /// <param name="transaction">a valid OracleTransaction</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or PL/SQL command</param> 
        /// <returns>a dataset containing the resultset generated by the command</returns>
        public static DataSet ExecuteDataset(OracleTransaction transaction, CommandType commandType, string commandText)
        {
            //pass through the call providing null for the set of OracleParameters
            return ExecuteDataset(transaction, commandType, commandText, (OracleParameter[])null);
        }

        /// <summary>
        /// Execute an OracleCommand (that returns a resultset) against the specified OracleTransaction
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  DataSet ds = ExecuteDataset(trans, CommandType.StoredProcedure, "GetOrders", new OracleParameter("@prodid", 24));
        /// </remarks>
        /// <param name="transaction">a valid OracleTransaction</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or PL/SQL command</param> 
        /// <param name="commandParameters">an array of OracleParameters used to execute the command</param>
        /// <returns>a dataset containing the resultset generated by the command</returns>
        public static DataSet ExecuteDataset(OracleTransaction transaction, CommandType commandType, string commandText, params OracleParameter[] commandParameters)
        {
            //create a command and prepare it for execution
            OracleCommand cmd = new OracleCommand();
            PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters);

            //create the DataAdapter & DataSet
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            DataSet ds = new DataSet();

            //fill the DataSet using default values for DataTable names, etc.
            da.Fill(ds);

            //return the dataset
            return ds;
        }

        /// <summary>
        /// Execute a stored procedure via an OracleCommand (that returns a resultset) against the specified 
        /// OracleTransaction using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// 
        /// e.g.:  
        ///  DataSet ds = ExecuteDataset(trans, "GetOrders", 24, 36);
        /// </remarks>
        /// <param name="transaction">a valid OracleTransaction</param>
        /// <param name="spName">the name of the stored procedure</param>
        /// <param name="parameterValues">an array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>a dataset containing the resultset generated by the command</returns>
        public static DataSet ExecuteDataset(OracleTransaction transaction, string spName, params object[] parameterValues)
        {
            //if we got parameter values, we need to figure out where they go
            if (parameterValues == null)
            {
                parameterValues = new object[] { };
            }
            if ((parameterValues != null) && (parameterValues.Length >= 0))
            {
                //pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                OracleParameter[] commandParameters = OracleHelperParameterCache.GetSpParameterSet(transaction.Connection.ConnectionString, spName);

                //assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                //call the overload that takes an array of OracleParameters
                return ExecuteDataset(transaction, CommandType.StoredProcedure, spName, commandParameters);
            }
            //otherwise we can just call the SP without params
            else
            {
                return ExecuteDataset(transaction, CommandType.StoredProcedure, spName);
            }
        }

        #endregion ExecuteDataSet

        #region ExecuteReader

        /// <summary>
        /// this enum is used to indicate weather the connection was provided by the caller, or created by OracleHelper, so that
        /// we can set the appropriate CommandBehavior when calling ExecuteReader()
        /// </summary>
        private enum OracleConnectionOwnership
        {
            /// <summary>Connection is owned and managed by OracleHelper</summary>
            Internal,
            /// <summary>Connection is owned and managed by the caller</summary>
            External
        }


        /// <summary>
        /// Create and prepare an OracleCommand, and call ExecuteReader with the appropriate CommandBehavior.
        /// </summary>
        /// <remarks>
        /// If we created and opened the connection, we want the connection to be closed when the DataReader is closed.
        /// 
        /// If the caller provided the connection, we want to leave it to them to manage.
        /// </remarks>
        /// <param name="connection">a valid OracleConnection, on which to execute this command</param>
        /// <param name="transaction">a valid OracleTransaction, or 'null'</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or PL/SQL command</param> 
        /// <param name="commandParameters">an array of OracleParameters to be associated with the command or 'null' if no parameters are required</param>
        /// <param name="connectionOwnership">indicates whether the connection parameter was provided by the caller, or created by OracleHelper</param>
        /// <returns>OracleDataReader containing the results of the command</returns>
        private static OracleDataReader ExecuteReader(OracleConnection connection, OracleTransaction transaction, CommandType commandType, string commandText, OracleParameter[] commandParameters, OracleConnectionOwnership connectionOwnership)
        {
            //create a command and prepare it for execution
            OracleCommand cmd = new OracleCommand();
            PrepareCommand(cmd, connection, transaction, commandType, commandText, commandParameters);

            //create a reader
            OracleDataReader dr;

            // call ExecuteReader with the appropriate CommandBehavior
            if (connectionOwnership == OracleConnectionOwnership.External)
            {
                dr = cmd.ExecuteReader();
            }
            else
            {
                dr = cmd.ExecuteReader((CommandBehavior)((int)CommandBehavior.CloseConnection));
            }

            return (OracleDataReader)dr;
        }

        /// <summary>
        /// Execute an OracleCommand (that returns a resultset and takes no parameters) against the database specified in 
        /// the connection string. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  OracleDataReader dr = ExecuteReader(connString, CommandType.StoredProcedure, "GetOrders");
        /// </remarks>
        /// <param name="connectionString">a valid connection string for an OracleConnection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or PL/SQL command</param>  
        /// <returns>an OracleDataReader containing the resultset generated by the command</returns>
        public static OracleDataReader ExecuteReader(string connectionString, CommandType commandType, string commandText)
        {
            //pass through the call providing null for the set of OracleParameters
            return ExecuteReader(connectionString, commandType, commandText, (OracleParameter[])null);
        }

        /// <summary>
        /// Execute an OracleCommand (that returns a resultset) against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  OracleDataReader dr = ExecuteReader(connString, CommandType.StoredProcedure, "GetOrders", new OracleParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">a valid connection string for an OracleConnection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or PL/SQL command</param>  
        /// <param name="commandParameters">an array of OracleParameters used to execute the command</param>
        /// <returns>an OracleDataReader containing the resultset generated by the command</returns>
        public static OracleDataReader ExecuteReader(string connectionString, CommandType commandType, string commandText, params OracleParameter[] commandParameters)
        {
            //create & open an OraclebConnection
            OracleConnection cn = new OracleConnection(connectionString);
            cn.Open();

            try
            {
                //call the private overload that takes an internally owned connection in place of the connection string
                return ExecuteReader(cn, null, commandType, commandText, commandParameters, OracleConnectionOwnership.Internal);
            }
            catch
            {
                //if we fail to return the OracleDataReader, we need to close the connection ourselves
                cn.Close();
                throw;
            }
        }

        /// <summary>
        /// Execute a stored procedure via an OracleCommand (that returns a resultset) against the database specified in 
        /// the connection string using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// 
        /// e.g.:  
        ///  OracleDataReader dr = ExecuteReader(connString, "GetOrders", 24, 36);
        /// </remarks>
        /// <param name="connectionString">a valid connection string for an OracleConnection</param>
        /// <param name="spName">the name of the stored procedure</param>
        /// <param name="parameterValues">an array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>an OracleDataReader containing the resultset generated by the command</returns>
        public static OracleDataReader ExecuteReader(string connectionString, string spName, params object[] parameterValues)
        {
            //if we got parameter values, we need to figure out where they go
            if (parameterValues == null)
            {
                parameterValues = new object[] { };
            }
            if ((parameterValues != null) && (parameterValues.Length >= 0))
            {
                //pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                OracleParameter[] commandParameters = OracleHelperParameterCache.GetSpParameterSet(connectionString, spName);

                //assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                //call the overload that takes an array of OracleParameters
                return ExecuteReader(connectionString, CommandType.StoredProcedure, spName, commandParameters);
            }
            //otherwise we can just call the SP without params
            else
            {
                return ExecuteReader(connectionString, CommandType.StoredProcedure, spName);
            }
        }

        /// <summary>
        /// Execute an OracleCommand (that returns a resultset and takes no parameters) against the provided OracleConnection. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  OracleDataReader dr = ExecuteReader(conn, CommandType.StoredProcedure, "GetOrders");
        /// </remarks>
        /// <param name="connection">a valid OracleConnection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or PL/SQL command</param>
        /// <returns>an OracleDataReader containing the resultset generated by the command</returns>
        public static OracleDataReader ExecuteReader(OracleConnection connection, CommandType commandType, string commandText)
        {
            //pass through the call providing null for the set of OracleParameters
            return ExecuteReader(connection, commandType, commandText, (OracleParameter[])null);
        }

        /// <summary>
        /// Execute an OracleCommand (that returns a resultset) against the specified OracleConnection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  OracleDataReader dr = ExecuteReader(conn, CommandType.StoredProcedure, "GetOrders", new OracleParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connection">a valid OracleConnection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or PL/SQL command</param>  
        /// <param name="commandParameters">an array of OracleParameters used to execute the command</param>
        /// <returns>an OracleDataReader containing the resultset generated by the command</returns>
        public static OracleDataReader ExecuteReader(OracleConnection connection, CommandType commandType, string commandText, params OracleParameter[] commandParameters)
        {
            //pass through the call to the private overload using a null transaction value and an externally owned connection
            return ExecuteReader(connection, (OracleTransaction)null, commandType, commandText, commandParameters, OracleConnectionOwnership.External);
        }

        /// <summary>
        /// Execute a stored procedure via an OracleCommand (that returns a resultset) against the specified OracleConnection 
        /// using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// 
        /// e.g.:  
        ///  OracleDataReader dr = ExecuteReader(conn, "GetOrders", 24, 36);
        /// </remarks>
        /// <param name="connection">a valid OracleConnection</param>
        /// <param name="spName">the name of the stored procedure</param>
        /// <param name="parameterValues">an array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>an OracleDataReader containing the resultset generated by the command</returns>
        public static OracleDataReader ExecuteReader(OracleConnection connection, string spName, params object[] parameterValues)
        {
            //if we got parameter values, we need to figure out where they go
            if (parameterValues == null)
            {
                parameterValues = new object[] { };
            }
            if ((parameterValues != null) && (parameterValues.Length >= 0))
            {
                OracleParameter[] commandParameters = OracleHelperParameterCache.GetSpParameterSet(connection.ConnectionString, spName);

                AssignParameterValues(commandParameters, parameterValues);

                return ExecuteReader(connection, CommandType.StoredProcedure, spName, commandParameters);
            }
            //otherwise we can just call the SP without params
            else
            {
                return ExecuteReader(connection, CommandType.StoredProcedure, spName);
            }
        }

        /// <summary>
        /// Execute an OracleCommand (that returns a resultset and takes no parameters) against the provided OracleTransaction. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  OracleDataReader dr = ExecuteReader(trans, CommandType.StoredProcedure, "GetOrders");
        /// </remarks>
        /// <param name="transaction">a valid OracleTransaction</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or PL/SQL command</param>  
        /// <returns>an OracleDataReader containing the resultset generated by the command</returns>
        public static OracleDataReader ExecuteReader(OracleTransaction transaction, CommandType commandType, string commandText)
        {
            //pass through the call providing null for the set of OracleParameters
            return ExecuteReader(transaction, commandType, commandText, (OracleParameter[])null);
        }

        /// <summary>
        /// Execute an OracleCommand (that returns a resultset) against the specified OracleTransaction
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///   OracleDataReader dr = ExecuteReader(trans, CommandType.StoredProcedure, "GetOrders", new OracleParameter("@prodid", 24));
        /// </remarks>
        /// <param name="transaction">a valid OracleTransaction</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or PL/SQL command</param> 
        /// <param name="commandParameters">an array of OracleParameters used to execute the command</param>
        /// <returns>an OracleDataReader containing the resultset generated by the command</returns>
        public static OracleDataReader ExecuteReader(OracleTransaction transaction, CommandType commandType, string commandText, params OracleParameter[] commandParameters)
        {
            //pass through to private overload, indicating that the connection is owned by the caller
            return ExecuteReader(transaction.Connection, transaction, commandType, commandText, commandParameters, OracleConnectionOwnership.External);
        }

        /// <summary>
        /// Execute a stored procedure via an OracleCommand (that returns a resultset) against the specified
        /// OracleTransaction using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// 
        /// e.g.:  
        ///  OracleDataReader dr = ExecuteReader(trans, "GetOrders", 24, 36);
        /// </remarks>
        /// <param name="transaction">a valid OracleTransaction</param>
        /// <param name="spName">the name of the stored procedure</param>
        /// <param name="parameterValues">an array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>an OracleDataReader containing the resultset generated by the command</returns>
        public static OracleDataReader ExecuteReader(OracleTransaction transaction, string spName, params object[] parameterValues)
        {
            //if we got parameter values, we need to figure out where they go
            if (parameterValues == null)
            {
                parameterValues = new object[] { };
            }
            if ((parameterValues != null) && (parameterValues.Length >= 0))
            {
                OracleParameter[] commandParameters = OracleHelperParameterCache.GetSpParameterSet(transaction.Connection.ConnectionString, spName);

                AssignParameterValues(commandParameters, parameterValues);

                return ExecuteReader(transaction, CommandType.StoredProcedure, spName, commandParameters);
            }
            return null;
            //otherwise we can just call the SP without params
            //else
            //{
            //    return ExecuteReader(transaction, CommandType.StoredProcedure, spName);
            //}
        }

        #endregion ExecuteReader

        #region ExecuteScalar

        /// <summary>
        /// Execute an OracleCommand (that returns a 1x1 resultset and takes no parameters) against the database specified in 
        /// the connection string. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int orderCount = (int)ExecuteScalar(connString, CommandType.StoredProcedure, "GetOrderCount");
        /// </remarks>
        /// <param name="connectionString">a valid connection string for an OracleConnection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-Oracle command</param>
        /// <returns>an object containing the value in the 1x1 resultset generated by the command</returns>
        public static object ExecuteScalar(string connectionString, CommandType commandType, string commandText)
        {
            //pass through the call providing null for the set of OracleParameters
            return ExecuteScalar(connectionString, commandType, commandText, (OracleParameter[])null);
        }

        /// <summary>
        /// Execute an OracleCommand (that returns a 1x1 resultset) against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int orderCount = (int)ExecuteScalar(connString, CommandType.StoredProcedure, "GetOrderCount", new OracleParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">a valid connection string for an OracleConnection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-Oracle command</param>
        /// <param name="commandParameters">an array of OracleParameters used to execute the command</param>
        /// <returns>an object containing the value in the 1x1 resultset generated by the command</returns>
        public static object ExecuteScalar(string connectionString, CommandType commandType, string commandText, params OracleParameter[] commandParameters)
        {
            //create & open an OracleConnection, and dispose of it after we are done.
            using (OracleConnection cn = new OracleConnection(connectionString))
            {
                try
                {
                    cn.Open();

                    //call the overload that takes a connection in place of the connection string
                    return ExecuteScalar(cn, commandType, commandText, commandParameters);
                }
                catch (Exception ex)
                {
                    throw (ex);

                }
                finally
                {
                    if (cn.State == ConnectionState.Open)
                    {
                        cn.Close();
                    }
                    //cn.Dispose();
                }
            }
        }

        /// <summary>
        /// Execute a stored procedure via an OracleCommand (that returns a 1x1 resultset) against the database specified in 
        /// the conneciton string using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// 
        /// e.g.:  
        ///  int orderCount = (int)ExecuteScalar(connString, "GetOrderCount", 24, 36);
        /// </remarks>
        /// <param name="connectionString">a valid connection string for an OracleConnection</param>
        /// <param name="spName">the name of the stored procedure</param>
        /// <param name="parameterValues">an array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>an object containing the value in the 1x1 resultset generated by the command</returns>
        public static object ExecuteScalar(string connectionString, string spName, params object[] parameterValues)
        {
            //if we got parameter values, we need to figure out where they go
            if (parameterValues == null)
            {
                parameterValues = new object[] { };
            }
            if ((parameterValues != null) && (parameterValues.Length >= 0))
            {
                //pull the parameters for this stored procedure from the parameter cache (or discover them & populet the cache)
                OracleParameter[] commandParameters = OracleHelperParameterCache.GetSpParameterSet(connectionString, spName);

                //assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                //call the overload that takes an array of OracleParameters
                return ExecuteScalar(connectionString, CommandType.StoredProcedure, spName, commandParameters);
            }
            return null;
            //otherwise we can just call the SP without params
            //else
            //{
            //    return ExecuteScalar(connectionString, CommandType.StoredProcedure, spName);
            //}
        }

        /// <summary>
        /// Execute an OracleCommand (that returns a 1x1 resultset and takes no parameters) against the provided OracleConnection. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int orderCount = (int)ExecuteScalar(conn, CommandType.StoredProcedure, "GetOrderCount");
        /// </remarks>
        /// <param name="connection">a valid OracleConnection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-Oracle command</param>
        /// <returns>an object containing the value in the 1x1 resultset generated by the command</returns>
        public static object ExecuteScalar(OracleConnection connection, CommandType commandType, string commandText)
        {
            //pass through the call providing null for the set of OracleParameters
            return ExecuteScalar(connection, commandType, commandText, (OracleParameter[])null);
        }

        /// <summary>
        /// Execute an OracleCommand (that returns a 1x1 resultset) against the specified OracleConnection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int orderCount = (int)ExecuteScalar(conn, CommandType.StoredProcedure, "GetOrderCount", new OracleParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connection">a valid OracleConnection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-OleDb command</param>
        /// <param name="commandParameters">an array of OracleParameters used to execute the command</param>
        /// <returns>an object containing the value in the 1x1 resultset generated by the command</returns>
        public static object ExecuteScalar(OracleConnection connection, CommandType commandType, string commandText, params OracleParameter[] commandParameters)
        {
            //create a command and prepare it for execution
            OracleCommand cmd = new OracleCommand();
            PrepareCommand(cmd, connection, (OracleTransaction)null, commandType, commandText, commandParameters);

            //execute the command & return the results
            return cmd.ExecuteScalar();
        }

        /// <summary>
        /// Execute a stored procedure via an OracleCommand (that returns a 1x1 resultset) against the specified OracleConnection 
        /// using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// 
        /// e.g.:  
        ///  int orderCount = (int)ExecuteScalar(conn, "GetOrderCount", 24, 36);
        /// </remarks>
        /// <param name="connection">a valid OracleConnection</param>
        /// <param name="spName">the name of the stored procedure</param>
        /// <param name="parameterValues">an array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>an object containing the value in the 1x1 resultset generated by the command</returns>
        public static object ExecuteScalar(OracleConnection connection, string spName, params object[] parameterValues)
        {
            //if we got parameter values, we need to figure out where they go
            if (parameterValues == null)
            {
                parameterValues = new object[] { };
            }
            if ((parameterValues != null) && (parameterValues.Length >= 0))
            {
                //pull the parameters for this stored procedure from the parameter cache (or discover them & populet the cache)
                OracleParameter[] commandParameters = OracleHelperParameterCache.GetSpParameterSet(connection.ConnectionString, spName);

                //assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                //call the overload that takes an array of OracleParameters
                return ExecuteScalar(connection, CommandType.StoredProcedure, spName, commandParameters);
            }
            return null;
            //otherwise we can just call the SP without params
            //else
            //{
            //    return ExecuteScalar(connection, CommandType.StoredProcedure, spName);
            //}
        }

        /// <summary>
        /// Execute an OracleCommand (that returns a 1x1 resultset and takes no parameters) against the provided OracleTransaction. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int orderCount = (int)ExecuteScalar(trans, CommandType.StoredProcedure, "GetOrderCount");
        /// </remarks>
        /// <param name="transaction">a valid OracleTransaction</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-OleDb command</param>
        /// <returns>an object containing the value in the 1x1 resultset generated by the command</returns>
        public static object ExecuteScalar(OracleTransaction transaction, CommandType commandType, string commandText)
        {
            //pass through the call providing null for the set of OracleParameters
            return ExecuteScalar(transaction, commandType, commandText, (OracleParameter[])null);
        }

        /// <summary>
        /// Execute an OracleCommand (that returns a 1x1 resultset) against the specified OracleTransaction
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int orderCount = (int)ExecuteScalar(trans, CommandType.StoredProcedure, "GetOrderCount", new OracleParameter("@prodid", 24));
        /// </remarks>
        /// <param name="transaction">a valid OracleTransaction</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-OleDb command</param>
        /// <param name="commandParameters">an array of OracleParameters used to execute the command</param>
        /// <returns>an object containing the value in the 1x1 resultset generated by the command</returns>
        public static object ExecuteScalar(OracleTransaction transaction, CommandType commandType, string commandText, params OracleParameter[] commandParameters)
        {
            //create a command and prepare it for execution
            OracleCommand cmd = new OracleCommand();
            PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters);

            //execute the command & return the results
            return cmd.ExecuteScalar();

        }

        /// <summary>
        /// Execute a stored procedure via an OracleCommand (that returns a 1x1 resultset) against the specified
        /// OracleTransaction using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// 
        /// e.g.:  
        ///  int orderCount = (int)ExecuteScalar(trans, "GetOrderCount", 24, 36);
        /// </remarks>
        /// <param name="transaction">a valid OracleTransaction</param>
        /// <param name="spName">the name of the stored procedure</param>
        /// <param name="parameterValues">an array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>an object containing the value in the 1x1 resultset generated by the command</returns>
        public static object ExecuteScalar(OracleTransaction transaction, string spName, params object[] parameterValues)
        {
            //if we got parameter values, we need to figure out where they go
            if (parameterValues == null)
            {
                parameterValues = new object[] { };
            }
            if ((parameterValues != null) && (parameterValues.Length >= 0))
            {
                //pull the parameters for this stored procedure from the parameter cache (or discover them & populet the cache)
                OracleParameter[] commandParameters = OracleHelperParameterCache.GetSpParameterSet(transaction.Connection.ConnectionString, spName);

                //assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                //call the overload that takes an array of OracleParameters
                return ExecuteScalar(transaction, CommandType.StoredProcedure, spName, commandParameters);
            }
            //otherwise we can just call the SP without params
            else
            {
                return ExecuteScalar(transaction, CommandType.StoredProcedure, spName);
            }
        }

        #endregion ExecuteScalar

        #region "Execute Output value"

        public static ArrayList ExecuteNonquery_ReturnValue(string connString, string spName, params object[] parameterValues)
        {
            ArrayList returnValue = new ArrayList();

            try
            {
                if ((parameterValues != null) && (parameterValues.Length > 0))
                {
                    OracleParameter[] commandParameters = OracleHelperParameterCache.GetSpParameterSet(connString, spName);

                    //assign the provided values to these parameters based on parameter order
                    AssignParameterValues(commandParameters, parameterValues);

                    return ExecuteNonquery_Return(connString, CommandType.StoredProcedure, spName, commandParameters);
                }
                return returnValue;
            }
            catch (Exception ex)
            {
                return returnValue;
            }
            finally
            {
                returnValue = null;
            }

        }

        public static ArrayList ExecuteNonquery_Return(string connString, CommandType commandType, string commandText, params OracleParameter[] parameterValues)
        {
            ArrayList returnValue = new ArrayList();
            OracleConnection conn = new OracleConnection(connString);
            //create a command and prepare it for execution
            OracleCommand cmd = new OracleCommand();
            try
            {
                int result = -5;
               
                PrepareCommand(cmd, conn, (OracleTransaction)null, commandType, commandText, parameterValues);
                result = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                int i = 0;
                for (; i < parameterValues.Length; i++)
                {
                    if (parameterValues[i].Direction == ParameterDirection.Output)
                    {

                        returnValue.Add(parameterValues[i].Value);

                    }
                }
                return returnValue;
            }
            catch (Exception ex)
            {
                return returnValue;
            }
            finally
            {                
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Dispose();
                returnValue = null;
                
            }
        }
        private static object[] ExecuteNonQuery_Return(string p_strConnStr, string p_strStoreName, params OracleParameter[] p_arrSQLParameter)
        {
            OracleConnection conn = new OracleConnection(p_strConnStr);
            OracleCommand cmd = new OracleCommand();
            int result = -5;
            object[] objReturn = { -1000 };
            try
            {
                PrepareCommand(cmd, conn, ((OracleTransaction)null), p_strStoreName, p_arrSQLParameter);

                // Execute Sql Command
                result = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                short j = 0;
                for (short i = 0; i <= p_arrSQLParameter.Length - 1; i++)
                {
                    if (p_arrSQLParameter[i].Direction == ParameterDirection.Output)
                    {
                        Array.Resize(ref objReturn, j + 1);
                        objReturn[j] = p_arrSQLParameter[i].Value.ToString();
                        j++;
                    }
                }


            }
            catch (Exception ex)
            {
                throw (ex);

            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Dispose();
            }
            //If result > 0 Then
            return objReturn;
            // End If

        }
        public static object[] ExecuteNonquery_ReturnValues(string p_strConnStr, string p_strSPname, params object[] p_arrValue)
        {
            try
            {
                bool includeReturnValueParameter = false;
                if ((p_arrValue != null) && (p_arrValue.Length > 0))
                {
                    DatabaseServiceCache idb = new DatabaseServiceCache();
                    OracleParameter[] arrSQLParameter = idb.GetSpParameterSet(p_strConnStr, p_strSPname, includeReturnValueParameter);

                    AssignParameterValues(arrSQLParameter, p_arrValue, includeReturnValueParameter);
                    return ExecuteNonQuery_Return(p_strConnStr, p_strSPname, arrSQLParameter);
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return null;
        }
        #endregion
    }
}
