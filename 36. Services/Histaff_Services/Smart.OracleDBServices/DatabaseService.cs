using Smart;
using System.Data.OracleClient;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using System.Configuration;

//using Microsoft.VisualBasic.CompilerServices;

//TruongNN
//Lop nay dung de thuc thi cac dich vu ve database
namespace Smart.OracleDBServices
{
	
    public class DatabaseService 
		{
			
		#region Public Method
		public void ExecuteQuery(string p_strConnStr, DataSet ds, string strsql, object[] p_arrValue)
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
		public short ExecuteNonQuery(string p_strConnStr, string strsql)
		{
			try
			{
                OracleDataAdapter adapter = new OracleDataAdapter();
                OracleConnection mConnection = new OracleConnection(p_strConnStr);
                try
                {
                   
                    OracleCommand cmd = mConnection.CreateCommand();
                    cmd.CommandText = strsql;
                    cmd.ExecuteNonQuery();
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
          			
			return 0;
		}		
		[STAThread()]
        public object ExecuteScalar(string p_strConnStr, string p_strSPname, params object[] p_arrValue)
			{
			try
			{
				bool includeReturnValueParameter = true;
				if ((p_arrValue != null) && (p_arrValue.Length > 0))
				{
					DatabaseServiceCache idb = new DatabaseServiceCache();
					DataTable p_dtData = new DataTable();
					OracleParameter[] arrSQLParameter = idb.GetSpParameterSet(p_strConnStr, p_strSPname, includeReturnValueParameter);
					AssignParameterValues(arrSQLParameter, p_arrValue, includeReturnValueParameter);
					return ExecuteScalar(p_strConnStr, p_dtData, p_strSPname, arrSQLParameter);
				}
				else
				{
					return ExecuteScalar(p_strConnStr, p_strSPname, null);
				}
			}
			catch (Exception ex)
			{
				throw (ex);
			}
			
		}		
		[STAThread()]public int ExecuteNonquery(string p_strConnStr, string p_strSPname, params object[] p_arrValue)
			{
			try
			{
				bool includeReturnValueParameter = false;
				if ((p_arrValue != null) && (p_arrValue.Length > 0))
				{
					DatabaseServiceCache idb = new DatabaseServiceCache();
					OracleParameter[] arrSQLParameter = idb.GetSpParameterSet(p_strConnStr, p_strSPname, includeReturnValueParameter);
					
					AssignParameterValues(arrSQLParameter, p_arrValue, includeReturnValueParameter);
					return ExecuteNonQuery_Renamed(p_strConnStr, p_strSPname, arrSQLParameter);
					
				}
				else
				{
					return ExecuteNonQuery_Renamed(p_strConnStr, p_strSPname, null);
				}
			}
			catch (Exception ex)
			{
				throw (ex);
			}
			
		}
		[STAThread()]public int ExecuteNonqueryFromTable(string p_strConnStr, string p_strSPname, DataTable dtParam)
			{
			try
			{
				bool includeReturnValueParameter = false;
				if ((dtParam == null) && (dtParam.Rows.Count >= 0))
				{
					return int.Parse("");
				}
				DatabaseServiceCache idb = new DatabaseServiceCache();
				OracleParameter[] arrSQLParameter = idb.GetSpParameterSet(p_strConnStr, p_strSPname, includeReturnValueParameter);
				OracleConnection conn = new OracleConnection(p_strConnStr);
				if (conn.State != ConnectionState.Open)
				{
					conn.Open();
				}
				OracleTransaction mTransaction = conn.BeginTransaction(IsolationLevel.ReadCommitted);
				object[] v_objResult = new object[1];
				short v_intResult = - 1;
				try
				{
					//Duyet het cac row table cua table
					for (int i = 0; i <= dtParam.Rows.Count - 1; i++)
					{
						object[] param = new object[dtParam.Columns.Count - 1+ 1];
						for (int j = 0; j <= dtParam.Columns.Count - 1; j++)
						{
							param[j] = dtParam.Rows[i][j];
						}
						AssignParameterValues(arrSQLParameter, param, includeReturnValueParameter);
						v_objResult = ExecuteNonQueryOneConnection(conn, mTransaction, p_strSPname, arrSQLParameter);
						v_intResult =  (short) (v_objResult[0]);
						if (v_intResult == 0)
						{
							mTransaction.Rollback();
							return 1; //Không tồn tại tài khoản
						}
					}
					mTransaction.Commit();
					return 0;
				}
				catch (Exception ex)
				{
					mTransaction.Rollback();
					///ErrorHandling.HandleError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name + "(" + p_strSPname + ")");
					return 1;
				}
				finally
				{
					if (conn.State == ConnectionState.Open)
					{
						conn.Close();                        
					}
					idb = null;
                    conn.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw (ex);
			}
			
			
		}
		[STAThread()]public object[] ExecuteNonquery_ReturnValue(string p_strConnStr, string p_strSPname, params object[] p_arrValue)
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
		[STAThread()]public DataSet ExecuteNonqueryToDataset(string p_strConnStr, string p_strSPname, params object[] p_arrValue)
			{
			try
			{
				bool includeReturnValueParameter = false;
				DataSet dset = new DataSet();
				if ((p_arrValue != null) && (p_arrValue.Length > 0))
				{
					DatabaseServiceCache idb = new DatabaseServiceCache();
					OracleParameter[] arrSQLParameter = idb.GetSpParameterSet(p_strConnStr, p_strSPname, includeReturnValueParameter);
					
					AssignParameterValues(arrSQLParameter, p_arrValue, includeReturnValueParameter);
					dset.Tables.Add(ExecuteNonQueryToDatatable(p_strConnStr, p_strSPname, arrSQLParameter));
					return dset;
				}
			}
			catch (Exception ex)
			{
				throw (ex);
			}
			
			return null;
		}
		
		[STAThread()]public int ExecuteNonquerySQL(string p_strConnStr, string p_strSQLCommand)
			{
			//bool includeReturnValueParameter = true;
			try
			{
				OracleConnection conn = new OracleConnection(p_strConnStr);
				OracleCommand cmd = new OracleCommand();
				int result = - 5;
				
				try
				{
					PrepareCommandSQL(cmd, conn, ((OracleTransaction) null), p_strSQLCommand);
					result = cmd.ExecuteNonQuery();
					cmd.Parameters.Clear();
					
				}
				catch (Exception ex)
				{
					//ErrorHandling.HandleError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name + "(" + p_strSQLCommand + ")");
					
				}
				finally
				{
					if (conn.State == ConnectionState.Open)
					{
						conn.Close();                        
					}
					cmd.Dispose();
                    conn.Dispose();
				}
				
				return result;
			}
			catch (Exception ex)
			{
				throw (ex);
			}
			
		}		
		
		[STAThread()]public void FillDataSet(string p_strConnStr, DataSet p_dsData, string p_strSPname, params object[] p_arrValue)
			{
			try
			{
				bool includeReturnValueParameter = true;
				if ((p_arrValue != null) && (p_arrValue.Length > 0))
				{
					DatabaseServiceCache idb = new DatabaseServiceCache();
					OracleParameter[] arrSQLParameter = idb.GetSpParameterSet(p_strConnStr, p_strSPname, includeReturnValueParameter);
					AssignParameterValues(arrSQLParameter, p_arrValue, includeReturnValueParameter);
					FillDataSet(p_strConnStr, p_dsData, p_strSPname, arrSQLParameter);
					
				}
				else
				{
					FillDataSet(p_strConnStr, p_dsData, p_strSPname, null);
				}
			}
			catch (Exception ex)
			{
				throw (ex);
			}
		}
		
		
		[STAThread()]public void FillDataSetSQL(string p_strConnStr, DataSet p_dsData, string p_strSQLCommand)
			{
			//bool includeReturnValueParameter = true;
			try
			{
				OracleConnection conn = new OracleConnection(p_strConnStr);
				OracleCommand cmd = new OracleCommand();
				OracleDataAdapter da = new OracleDataAdapter(cmd);
				
				try
				{
					PrepareCommandSQL(cmd, conn, ((OracleTransaction) null), p_strSQLCommand);
					da.Fill(p_dsData);
					
				}
				catch (Exception ex)
				{
					//ErrorHandling.HandleError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name + "(" + p_strSQLCommand + ")");
					
				}
				finally
				{
					if (conn.State == ConnectionState.Open)
					{
						conn.Close();                       
					}
					cmd.Dispose();
					da.Dispose();
                    conn.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw (ex);
			}
			
		}
		
		
		[STAThread()]public void FillDataTable(string p_strConnStr, DataTable p_dtData, string p_strSPname, params object[] p_arrValue)
			{
			try
			{
				bool includeReturnValueParameter = true;
				if ((p_arrValue != null) && (p_arrValue.Length > 0))
				{
					DatabaseServiceCache idb = new DatabaseServiceCache();
					OracleParameter[] arrSQLParameter = idb.GetSpParameterSet(p_strConnStr, p_strSPname, includeReturnValueParameter);
					
					AssignParameterValues(arrSQLParameter, p_arrValue, includeReturnValueParameter);
					FillDataTable(p_strConnStr, p_dtData, p_strSPname, arrSQLParameter);
					
				}
				else
				{
					FillDataTable(p_strConnStr, p_dtData, p_strSPname, null);
				}
			}
			catch (Exception ex)
			{
				throw (ex);
			}
			
		}
		
		
		[STAThread()]public void FillDataTableSQL(string p_strConnStr, DataTable p_dtData, string p_strSQLCommand)
			{
			try
			{
				OracleConnection conn = new OracleConnection(p_strConnStr);
				OracleCommand cmd = new OracleCommand();
				OracleDataAdapter da = new OracleDataAdapter(cmd);
				
				try
				{
					PrepareCommandSQL(cmd, conn, ((OracleTransaction) null), p_strSQLCommand);
					da.Fill(p_dtData);
					
				}
				catch (Exception ex)
				{
					//ErrorHandling.HandleError(ex, System.Reflection.Assembly.GetExecutingAssembly().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
				}
				finally
				{
					if (conn.State == ConnectionState.Open)
					{
						conn.Close();                        
					}
					cmd.Dispose();
					da.Dispose();
                    conn.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw (ex);
			}
			
		}
		#endregion
		
		#region Private Method
		
		private object ExecuteScalar(string p_strConnStr, string p_strStoreName, params OracleParameter[] p_arrSQLParameter)
		{
			OracleConnection conn = new OracleConnection(p_strConnStr);
			OracleCommand cmd = new OracleCommand();
			object result = null;
			try
			{
				PrepareCommand(cmd, conn, ((OracleTransaction) null), p_strStoreName, p_arrSQLParameter);
				// Execute Sql Command
				result = cmd.ExecuteOracleScalar();
				cmd.Parameters.Clear();
				
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
			
			return result;
		}
		
		
		private object ExecuteScalar(string p_strConnStr, DataTable p_dtData, string p_strStoreName, params OracleParameter[] p_arrSQLParameter)
		{
			OracleConnection conn = new OracleConnection(p_strConnStr);
			OracleCommand cmd = new OracleCommand();
			OracleDataAdapter da = new OracleDataAdapter(cmd);
			object result = null;
			try
			{
				PrepareCommand(cmd, conn, ((OracleTransaction) null), p_strStoreName, p_arrSQLParameter);
				da.Fill(p_dtData);
				foreach (DataRow row in p_dtData.Rows)
				{
					result = row[0];
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
				cmd.Dispose();
				da.Dispose();
			}
			
			return result;
		}
		
		private object[] ExecuteNonQuery_Return(string p_strConnStr, string p_strStoreName, params OracleParameter[] p_arrSQLParameter)
		{
			OracleConnection conn = new OracleConnection(p_strConnStr);
			OracleCommand cmd = new OracleCommand();
			int result = - 5;
			object[] objReturn = {- 1000};
			try
			{
				PrepareCommand(cmd, conn, ((OracleTransaction) null), p_strStoreName, p_arrSQLParameter);
				
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
				cmd.Dispose();
			}
			//If result > 0 Then
			return objReturn;
			// End If
			
		}
		private DataTable ExecuteNonQueryToDatatable(string p_strConnStr, string p_strStoreName, params OracleParameter[] p_arrSQLParameter)
		{
			OracleConnection conn = new OracleConnection(p_strConnStr);
			OracleCommand cmd = new OracleCommand();
			int result = - 5;
			object[] objReturn = {- 1000};
			DataTable dt = new DataTable();
			try
			{
				PrepareCommand(cmd, conn, ((OracleTransaction) null), p_strStoreName, p_arrSQLParameter);
				
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
						if (p_arrSQLParameter[i].Value is System.Data.OracleClient.OracleDataReader)
						{
							dt.Load((IDataReader) p_arrSQLParameter[i].Value);
							dt.TableName = p_strStoreName;
							return dt;
//							break;
						}
						
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
				cmd.Dispose();
			}
			
			
			return null;
		}
		private int ExecuteNonQuery_Renamed(string p_strConnStr, string p_strStoreName, params OracleParameter[] p_arrSQLParameter)
		{
			OracleConnection conn = new OracleConnection(p_strConnStr);
			OracleCommand cmd = new OracleCommand();
			int result = - 5;
			
			try
			{
				PrepareCommand(cmd, conn, ((OracleTransaction) null), p_strStoreName, p_arrSQLParameter);
				
				// Execute Sql Command
				result = cmd.ExecuteNonQuery();
				cmd.Parameters.Clear();
				
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
			
			return result;
		}
		private object[] ExecuteNonQueryOneConnection(OracleConnection conn, OracleTransaction p_trans, string p_strStoreName, params OracleParameter[] p_arrSQLParameter)
		{
			//Dim conn As New OracleConnection(p_strConnStr)
			OracleCommand cmd = new OracleCommand();
			int result = - 5;
			object[] objReturn = {- 1000};
			try
			{
				PrepareCommandWithTransaction(cmd, conn, p_trans, p_strStoreName, p_arrSQLParameter);
				
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
				
				cmd.Dispose();
			}
			//If result > 0 Then
			return objReturn;
			// End If
			
		}
		private void FillDataSet(string p_strConnStr, DataSet p_dsData, string p_strStoreName, params OracleParameter[] p_arrSQLParameter)
		{
			OracleConnection conn = new OracleConnection(p_strConnStr);
			OracleCommand cmd = new OracleCommand();
			OracleDataAdapter da = new OracleDataAdapter(cmd);
			
			try
			{
				PrepareCommand(cmd, conn, ((OracleTransaction) null), p_strStoreName, p_arrSQLParameter);
				da.Fill(p_dsData);
				
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
				da.Dispose();
			}
		}
		
		private void FillDataTable(string p_strConnStr, DataTable p_dtData, string p_strStoreName, params OracleParameter[] p_arrSQLParameter)
		{
			OracleConnection conn = new OracleConnection(p_strConnStr);
			OracleCommand cmd = new OracleCommand();
			OracleDataAdapter da = new OracleDataAdapter(cmd);
			
			try
			{
				PrepareCommand(cmd, conn, ((OracleTransaction) null), p_strStoreName, p_arrSQLParameter);
				da.Fill(p_dtData);
				
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
				da.Dispose();
			}
		}
		
		private void AssignParameterValues(OracleParameter[] p_arrSQLParameter, object[] p_arrValue, bool includeReturnValueParameter)
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
		
		
		private void PrepareCommand(OracleCommand p_cmd, OracleConnection p_conn, OracleTransaction p_trans, string p_strSPName, OracleParameter[] p_arrSQLParameter)
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
		private void PrepareCommandWithTransaction(OracleCommand p_cmd, OracleConnection p_conn, OracleTransaction p_trans, string p_strSPName, OracleParameter[] p_arrSQLParameter)
		{
			
			
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
		
		
		private void PrepareCommandSQL(OracleCommand p_cmd, OracleConnection p_conn, OracleTransaction p_trans, string p_strSQLCommand)
		{
			//if the provided connection is not open, we will open it
			if (p_conn.State != ConnectionState.Open)
			{
				p_conn.Open();
			}
			
			//associate the connection with the command
			p_cmd.Connection = p_conn;
			
			//set the command text (SQL statement)
			p_cmd.CommandText = p_strSQLCommand;
			
			//if we were provided a transaction, assign it.
			if (p_trans != null)
			{
				p_cmd.Transaction = p_trans;
			}
			
			//set the command type
			p_cmd.CommandType = CommandType.Text;
		}
		
		
		private void AttachParameters(OracleCommand p_cmd, OracleParameter[] p_arrSQLParameter)
		{
			foreach (OracleParameter p in p_arrSQLParameter)
			{
				if ((p.Direction == ParameterDirection.InputOutput || p.Direction == ParameterDirection.Output) && (p.Value == null))
				{
					p.Value = DBNull.Value;
				}
				
				p_cmd.Parameters.Add(p);
			}
		}
		#endregion
		
	}
	
}
