using Dapper;  
using Microsoft.Extensions.Configuration;  
using Oracle.ManagedDataAccess.Client;  
using System;  
using System.Data;
using Core2API.Oracle;
using HistaffWebAPI.Models;
using HistaffWebAPI.Services;
namespace HistaffWebAPI.Repositories  
{  
    public class Process_approved  : IProcess_approved  
    {  
        IConfiguration configuration; 
        EncryptDecrypt decrypt; 
        public Process_approved(IConfiguration _configuration)  
        {  
            configuration = _configuration;  
            decrypt=new EncryptDecrypt();
        }  
        public object GetProcess_ApprovesDetails(string token)  
        {  
            object result = null;  
            var conn = this.GetConnection();  
            try  
            {  
                var dyParam = new OracleDynamicParameters();  
                dyParam.Add("P_TOKEN", OracleDbType.NVarchar2, ParameterDirection.Input, token);  
                dyParam.Add("P_CUR", OracleDbType.RefCursor, ParameterDirection.Output);  
  
                if (conn.State == ConnectionState.Closed)  
                {  
                    conn.Open();  
                }  
  
                if (conn.State == ConnectionState.Open)  
                {  
                    var query = "PKG_AT_PROCESS.GET_PROCESS_APPROVEBY_TOKEN";  
  
                    result = SqlMapper.Query(conn, query, param: dyParam, commandType: CommandType.StoredProcedure);  
                }  
            }  
            catch (Exception ex)  
            {  
                throw ex;  
            }  finally{
                conn.Close();
            }
  
            return result;  
        }  
  
        public object GetProcess_Approves()  
        {  
            object result = null;  
            var conn = this.GetConnection();  
            try  
            {  
                var dyParam = new OracleDynamicParameters();  
  
                dyParam.Add("EMPCURSOR", OracleDbType.RefCursor, ParameterDirection.Output);  
  
               
                if(conn.State == ConnectionState.Closed)  
                {  
                    conn.Open();  
                }  
  
                if (conn.State == ConnectionState.Open)  
                {  
                    var query = "USP_GETEMPLOYEES";  
  
                    result = SqlMapper.Query(conn, query, param: dyParam, commandType: CommandType.StoredProcedure);  
                }  
            }  
            catch (Exception ex)  
            {  
                throw ex;  
            }  finally{
                conn.Close();
            }
  
            return result;  
        }  
  
        public IDbConnection GetConnection()  
        {  
            var connectionString = configuration.GetSection("QTGc5IBDTSLCGu/4FOmuTrYkQZ1l6J76JJm7UYK7xoY=").GetSection("toChRnDGxKiLQ/w+r/YGWg==").Value;  
            //var connectstrEncrypt = decrypt.EncryptString(connectionString);
            //var keyConnectStr =decrypt.EncryptString("ConnectionStrings");
            var connectStrDecrypt =decrypt.DecryptString(connectionString);
            var conn = new OracleConnection(connectStrDecrypt);             
            return conn;  
        } 
        public object PRI_PROCESS(Process_approveEncrypt _proces_approve){
            object result = null;  
            var conn = this.GetConnection();  

            try{
                var dyParam = new OracleDynamicParameters();  
                //giai ma cac pram
                //EncryptDecrypt decrypt=new EncryptDecrypt();
                double value;
                double.TryParse(decrypt.DecryptString(_proces_approve.EMPLOYEE_APPROVED),out  value);
                dyParam.Add("P_EMPLOYEE_APP_ID", OracleDbType.Double, ParameterDirection.Input,value); 
                double.TryParse(decrypt.DecryptString(_proces_approve.EMPLOYEE_ID),out value);
                dyParam.Add("P_EMPLOYEE_ID", OracleDbType.Int32, ParameterDirection.Input, value);  
                double.TryParse(decrypt.DecryptString(_proces_approve.PE_PERIOD_ID),out  value);
                dyParam.Add("P_PE_PERIOD_ID", OracleDbType.Int32, ParameterDirection.Input, value);  
                double.TryParse(_proces_approve.APP_STATUS,out  value);
                dyParam.Add("P_STATUS_ID", OracleDbType.Double, ParameterDirection.Input,value);  
                dyParam.Add("P_PROCESS_TYPE", OracleDbType.NVarchar2, ParameterDirection.Input, _proces_approve.PROCESS_TYPE);  
                dyParam.Add("P_NOTES", OracleDbType.NVarchar2, ParameterDirection.Input, _proces_approve.APP_NOTES);  
                double.TryParse(decrypt.DecryptString(_proces_approve.ID_REGGROUP),out  value);
                dyParam.Add("P_ID_REGGROUP", OracleDbType.Int32, ParameterDirection.Input, value);   
                dyParam.Add("P_OUT", OracleDbType.Decimal, ParameterDirection.Output);  
  
                if(conn.State == ConnectionState.Closed)  
                {  
                    conn.Open();  
                }  
  
                if (conn.State == ConnectionState.Open)  
                {  
                    var query = "PKG_AT_PROCESS.PRI_PROCESS";  
  
                    result = SqlMapper.Query(conn, query, param: dyParam, commandType: CommandType.StoredProcedure);  
                }  
            } catch (Exception ex)  {
                throw ex;  
            }finally{
                conn.Close();
            }
            return result;
        }
    }  
}  
