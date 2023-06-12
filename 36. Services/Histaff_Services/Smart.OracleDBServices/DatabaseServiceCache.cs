using Smart;
using System.Data.OracleClient;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;


//*********************************************************************
//
// OracleHelperParameterCache provides functions to leverage a static cache of procedure parameters, and the
// ability to discover parameters for stored procedures at run-time.
//
//*********************************************************************
namespace Smart.OracleDBServices
{
	public sealed class DatabaseServiceCache
	{
		//*********************************************************************
		//
		// Since this class provides only static methods, make the default constructor private to prevent
		// instances from being created with "new SqlHelperParameterCache()".
		//
		//*********************************************************************
		
		public DatabaseServiceCache()
		{
			paramCache = Hashtable.Synchronized(new Hashtable());
			
		}
		
		private Hashtable paramCache;
		
		//*********************************************************************
		//
		// resolve at run time the appropriate set of SqlParameters for a stored procedure
		//
		// param name="connectionString" a valid connection string for a SqlConnection
		// param name="spName" the name of the stored procedure
		// param name="includeReturnValueParameter" whether or not to include their return value parameter
		//
		//*********************************************************************
		
		private OracleParameter[] DiscoverSpParameterSet(string connectionString, string spName, bool includeReturnValueParameter)
		{
			OracleConnection cn = new OracleConnection(connectionString);
			OracleCommand cmd = new OracleCommand(spName, cn);
			OracleParameter[] discoveredParameters;
			
			try
			{
				cn.Open();
				cmd.CommandType = CommandType.StoredProcedure;
				OracleCommandBuilder.DeriveParameters(cmd);
				discoveredParameters = new OracleParameter[cmd.Parameters.Count - 1+ 1];
				cmd.Parameters.CopyTo(discoveredParameters, 0);
				
			}
			catch (Exception ex)
			{
				throw (ex);
				
			}
			finally
			{
				cn.Close();
				cmd.Dispose();
			}
			
			return discoveredParameters;
		}
		
		private OracleParameter[] CloneParameters(OracleParameter[] originalParameters)
		{
			//deep copy of cached SqlParameter array
			OracleParameter[] clonedParameters = new OracleParameter[originalParameters.Length - 1 + 1];
			
			int i = 0;
			int j = originalParameters.Length;
			while (i < j)
			{
				clonedParameters[i] = (OracleParameter) ((((ICloneable) (originalParameters[i])) ).Clone());
				i++;
			}
			
			return clonedParameters;
		}
		
		//*********************************************************************
		//
		// add parameter array to the cache
		//
		// param name="connectionString" a valid connection string for a SqlConnection
		// param name="commandText" the stored procedure name or T-SQL command
		// param name="commandParameters" an array of SqlParamters to be cached
		//
		//*********************************************************************
		
		public void CacheParameterSet(string connectionString, string commandText, params OracleParameter[] commandParameters)
		{
			string hashKey = connectionString + ":" + commandText;
			paramCache[hashKey] = commandParameters;
		}
		
		//*********************************************************************
		//
		// Retrieve a parameter array from the cache
		//
		// param name="connectionString" a valid connection string for a SqlConnection
		// param name="commandText" the stored procedure name or T-SQL command
		// returns an array of SqlParamters
		//
		//*********************************************************************
		
		public OracleParameter[] GetCachedParameterSet(string connectionString, string commandText)
		{
			string hashKey = connectionString + ":" + commandText;
			OracleParameter[] cachedParameters = (OracleParameter[]) (paramCache[hashKey]);
			
			if (cachedParameters == null)
			{
				return null;
			}
			else
			{
				return CloneParameters(cachedParameters);
			}
		}
		
		//*********************************************************************
		//
		// Retrieves the set of SqlParameters appropriate for the stored procedure
		//
		// This method will query the database for this information, and then store it in a cache for future requests.
		//
		// param name="connectionString" a valid connection string for a SqlConnection
		// param name="spName" the name of the stored procedure
		// returns an array of SqlParameters
		//
		//*********************************************************************
		
		public OracleParameter[] GetSpParameterSet(string connectionString, string spName)
		{
			return GetSpParameterSet(connectionString, spName, false);
		}
		
		//*********************************************************************
		//
		// Retrieves the set of SqlParameters appropriate for the stored procedure
		//
		// This method will query the database for this information, and then store it in a cache for future requests.
		//
		// param name="connectionString" a valid connection string for a SqlConnection
		// param name="spName" the name of the stored procedure
		// param name="includeReturnValueParameter" a bool value indicating whether the return value parameter should be included in the results
		// returns an array of SqlParameters
		//
		//*********************************************************************
		
		public OracleParameter[] GetSpParameterSet(string connectionString, string spName, bool includeReturnValueParameter)
		{
			string hashKey = connectionString + ":" + spName + ":include ReturnValue Parameter";
			OracleParameter[] cachedParameters;
			
			cachedParameters = (OracleParameter[]) (paramCache[hashKey]);
			
			if (cachedParameters == null)
			{
				paramCache[hashKey] = DiscoverSpParameterSet(connectionString, spName, includeReturnValueParameter);
				cachedParameters = (OracleParameter[]) (paramCache[hashKey]);
			}
			
			return CloneParameters(cachedParameters);
		}
	}
	
}
