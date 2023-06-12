using HiStaffAPI.AppHelpers;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;

namespace HiStaffAPI.AppCommon.DAO
{
    public class OracleDBManagerOM : IOracleDBManagerOM, IDisposable
    {
        public readonly string ConnectionString = ConfigurationManager.AppSettings["DbOMConnection"];
        public OracleCommand _command;

        public OracleDBManagerOM()
        {
            _command = new OracleCommand("");
            _command.CommandTimeout = 120;
            _command.CommandType = CommandType.StoredProcedure;
        }

        public OracleDBManagerOM(string commandText, CommandType commandType)
        {
            _command = new OracleCommand(commandText);
            _command.CommandTimeout = 120;
            _command.CommandType = commandType;
        }
        
        public OracleDBManagerOM(string commandText, CommandType commandType, string connectionString)
        {
            _command = new OracleCommand(commandText);
            _command.CommandTimeout = 120;
            _command.CommandType = commandType;
            ConnectionString = connectionString;
        }

        #region IndexOf Method

        /// <summary>
        /// Gets the location of a OracleParameter in the collection.
        /// </summary>
        /// <param name="value">The OracleParameter object to locate. </param>
        /// <returns>The zero-based location of the OracleParameter in the collection.</returns>
        public int IndexOf(object value)
        {
            return _command.Parameters.IndexOf(value);
        }

        /// <summary>
        /// Gets the location of the OracleParameter in the collection with a specific parameter name.
        /// </summary>
        /// <param name="parameterName">The name of the OracleParameter object to retrieve. </param>
        /// <returns>The zero-based location of the OracleParameter in the collection.</returns>
        public int IndexOf(string parameterName)
        {
            return _command.Parameters.IndexOf(parameterName);
        }

        #endregion IndexOf Method

        #region Indexers Property

        /// <summary>
        /// The parameters of the Transact-SQL statement or stored procedure. The default is an empty collection.
        /// </summary>
        public OracleParameter this[int index]
        {
            get => _command.Parameters[index];
            set => _command.Parameters[index] = value;
        }

        /// <summary>
        /// The parameters of the Transact-SQL statement or stored procedure. The default is an empty collection.
        /// </summary>
        public OracleParameter this[string parameterName]
        {
            get => _command.Parameters[parameterName];
            set => _command.Parameters[parameterName] = value;
        }

        #endregion Indexers Property

        #region ExecuteNonQuery
        public async Task<int> ExecuteNonQueryAsync()
        {
            OracleTransactionScope OracleTransactionScope = (OracleTransactionScope)HttpContext.Current.Items["DbTransactionScope"];
            if (OracleTransactionScope != null)
            {
                _command.Connection = OracleTransactionScope.Connection;
                _command.Transaction = OracleTransactionScope.Transaction;
                return await _command.ExecuteNonQueryAsync();
            }

            using (OracleConnection connection = new OracleConnection(ConnectionString))
            {
                _command.Connection = connection;
                await connection.OpenAsync();
                return await _command.ExecuteNonQueryAsync();
            }
        }

        public async Task<object> ExecuteScalarAsync()
        {
            OracleTransactionScope OracleTransactionScope = (OracleTransactionScope)HttpContext.Current.Items["DbTransactionScope"];
            if (OracleTransactionScope != null)
            {
                _command.Connection = OracleTransactionScope.Connection;
                _command.Transaction = OracleTransactionScope.Transaction;
                return await _command.ExecuteScalarAsync();
            }

            using (OracleConnection connection = new OracleConnection(ConnectionString))
            {
                _command.Connection = connection;
                await connection.OpenAsync();
                return await _command.ExecuteScalarAsync();
            }
        }

        #endregion

        #region GetDataTable
        public async Task<DataTable> GetDataTableAsync()
        {
            OracleTransactionScope OracleTransactionScope = (OracleTransactionScope)HttpContext.Current.Items["DbTransactionScope"];
            if (OracleTransactionScope != null)
            {
                _command.Connection = OracleTransactionScope.Connection;
                _command.Transaction = OracleTransactionScope.Transaction;

                DataTable rslt = new DataTable();
                OracleDataAdapter adapter = new OracleDataAdapter(_command);
                adapter.Fill(rslt);
                return rslt;
            }

            using (OracleConnection conn = new OracleConnection(ConnectionString))
            {
                _command.Connection = conn;
                await conn.OpenAsync();

                DataTable rslt = new DataTable();
                OracleDataAdapter adapter = new OracleDataAdapter(_command);
                adapter.Fill(rslt);
                return rslt;
            }
        } 
        
        public async Task<DataTable> GetDataTableAsync(string strCommand)
        {
            OracleTransactionScope OracleTransactionScope = (OracleTransactionScope)HttpContext.Current.Items["DbTransactionScope"];
            if (OracleTransactionScope != null)
            {
                _command = new OracleCommand(strCommand);
                _command.CommandType = CommandType.Text;
                _command.Connection = OracleTransactionScope.Connection;
                _command.Transaction = OracleTransactionScope.Transaction;

                DataTable rslt = new DataTable();
                OracleDataAdapter adapter = new OracleDataAdapter(_command);
                adapter.Fill(rslt);
                return rslt;
            }

            using (OracleConnection conn = new OracleConnection(ConnectionString))
            {
                _command = new OracleCommand(strCommand);
                _command.CommandType = CommandType.Text;
                _command.Connection = conn;
                await conn.OpenAsync();

                DataTable rslt = new DataTable();
                OracleDataAdapter adapter = new OracleDataAdapter(_command);
                adapter.Fill(rslt);
                return rslt;
            }
        }

        #endregion

        #region GetDataSet
        public async Task<DataSet> GetDataSetAsync()
        {
            OracleTransactionScope OracleTransactionScope = (OracleTransactionScope)HttpContext.Current.Items["DbTransactionScope"];
            if (OracleTransactionScope != null)
            {
                _command.Connection = OracleTransactionScope.Connection;
                _command.Transaction = OracleTransactionScope.Transaction;

                DataSet dataSet = new DataSet();
                OracleDataAdapter adapter = new OracleDataAdapter(_command);
                adapter.Fill(dataSet);
                return dataSet;
            }

            using (OracleConnection conn = new OracleConnection(ConnectionString))
            {
                _command.Connection = conn;
                await conn.OpenAsync();

                DataSet dataSet = new DataSet();
                OracleDataAdapter adapter = new OracleDataAdapter(_command);
                adapter.Fill(dataSet);
                return dataSet;
            }
        }


        #endregion

        #region Get Parameter
        public T GetParameter<T>(string parameterName)
        {
            try
            {
                return (T)Convert.ChangeType(_command.Parameters[parameterName].Value, typeof(T));
            }
            catch (ArgumentNullException)
            {
                return default(T);
            }
        }
        public T GetParameterOrDefault<T>(string parameterName)
        {
            try
            {
                return (T)Convert.ChangeType(_command.Parameters[parameterName].Value, typeof(T));
            }
            catch (Exception)
            {
                return default(T);
            }
        }
        #endregion

        #region Add parameter
        public void AddParameter(string name, OracleDbType type, int size, object value)
        {
            _command.Parameters.Add(new OracleParameter()
            {
                ParameterName = name,
                OracleDbType = type,
                Size = size,
                Value = value
            });
        }
        public void AddParameter(string name, OracleDbType type, object value)
        {
            _command.Parameters.Add(new OracleParameter()
            {
                ParameterName = name,
                OracleDbType = type,
                Value = value
            });
        }
        public void AddParameter(string name, int size, object value)
        {
            _command.Parameters.Add(new OracleParameter(name, size)
            {
                ParameterName = name,
                Size = size,
                Value = value
            });
        }
        public void AddParameter(string name, object value)
        {
            _command.Parameters.Add(name, value);
        }
        public void AddParameter(string name, ParameterDirection direction)
        {
            _command.Parameters.Add(new OracleParameter()
            {
                ParameterName = name,
                Direction = direction
            });
        }

        public void AddParameter(string name, OracleDbType type, ParameterDirection direction, object value)
        {
            _command.Parameters.Add(new OracleParameter()
            {
                ParameterName = name,
                OracleDbType = type,
                Direction = direction,
                Value = value ?? DBNull.Value
            });

        }

        public void AddParameter(string name, OracleDbType type, ParameterDirection direction)
        {
            _command.Parameters.Add(new OracleParameter()
            {
                ParameterName = name,
                OracleDbType = type,
                Direction = direction,
            });

        }

        public void AddParameter(string name, OracleDbType type, int size, ParameterDirection direction, object value)
        {
            _command.Parameters.Add(new OracleParameter()
            {
                ParameterName = name,
                OracleDbType = type,
                Size = size,
                Direction = direction,
                Value = value ?? DBNull.Value
            });

        }

        public void AddParameter(string name, OracleDbType type, int size, ParameterDirection direction)
        {
            _command.Parameters.Add(new OracleParameter()
            {
                ParameterName = name,
                OracleDbType = type,
                Size = size,
                Direction = direction
            });

        }

        #endregion

        #region Clear Method

        /// <summary>
        /// Removes all Parameter added.
        /// </summary>
        public void Clear()
        {
            this._command.Parameters.Clear();
        }

        #endregion Clear Method

        #region Get Values

        public T GetValue<T>(string columnName)
        {
            if (this[columnName].Value != DBNull.Value)
            {
                return (T)this[columnName].Value;
            }

            return default(T);
        }

        /// <summary>
        /// GetValueInString
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public string GetValueInString(string columnName)
        {
            if (this[columnName].Value != DBNull.Value)
            {
                return this[columnName].Value as string;
            }

            return null;
        }

        /// <summary>
        /// GetValueInInt
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public int? GetValueInInt(string columnName)
        {
            if (this[columnName].Value != DBNull.Value)
            {
                int i = Convert.ToInt32(this[columnName].Value);
                return i;
            }

            return null;
        }

        /// <summary>
        /// GetValueInLong
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public long? GetValueInLong(string columnName)
        {
            if (this[columnName].Value != DBNull.Value)
            {
                long i = Convert.ToInt64(this[columnName].Value);
                return i;
            }

            return null;
        }

        /// <summary>
        /// GetValueInChar
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public char? GetValueInChar(string columnName)
        {
            if (this[columnName].Value != DBNull.Value)
            {
                if (this[columnName].Value.GetType() == typeof(string))
                {
                    if (this[columnName].Value.ToString().Length > 0)
                    {
                        return this[columnName].Value.ToString()[0];
                    }

                    return null;
                }

                return (char)this[columnName].Value;
            }

            return null;
        }

        /// <summary>
        /// GetValueInByte
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public byte? GetValueInByte(string columnName)
        {
            if (this[columnName].Value != DBNull.Value)
            {
                return (byte)this[columnName].Value;
            }

            return null;
        }

        /// <summary>
        /// GetValueInDateTime
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public DateTime? GetValueInDateTime(string columnName)
        {
            if (this[columnName].Value != DBNull.Value)
            {
                return (DateTime)this[columnName].Value;
            }

            return null;
        }

        /// <summary>
        /// GetValueInBool
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public bool GetValueInBool(string columnName)
        {
            if (this[columnName].Value != DBNull.Value)
            {
                return Convert.ToBoolean(this[columnName].Value);
            }

            return false;
        }

        /// <summary>
        /// GetValueInDecimal
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public decimal? GetValueInDecimal(string columnName)
        {
            if (this[columnName].Value != DBNull.Value)
            {
                return (decimal)this[columnName].Value;
            }

            return null;
        }

        #endregion Get Values

        #region Dispose

        /// <summary>
        /// Releases the resources used by the Component.
        /// </summary>
        /// <param name="disposing">Send true value when Dispose Method is called by the program</param>
        public void Dispose(bool disposing)
        {
            if (disposing)
            {
                GC.SuppressFinalize(this);
            }

            if (this._command != null)
            {
                this._command.Parameters.Clear();
                this._command.Dispose();
            }
        }

        /// <summary>
        /// Releases the resources used by the Component.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
        }

        #endregion Dispose

        public void ExecuteStoreAPI(string functionName, string storeName, dynamic parameterInput,
           ref string parameterOutput, ref string json, ref int errorCode, ref string errorString, bool IsNoCache = true)
        {
            bool kt = false; dynamic d; int RSCount = 0; OracleCommand cmd; OracleParameter[] param = new OracleParameter[300];//OracleConnection Conn; 
            try
            {                                                                                                           //OpenDB(ref Conn);
                using (OracleConnection Conn = new OracleConnection(ConnectionString))
                {
                    Conn.Open();
                    cmd = new OracleCommand()
                    {
                        CommandText = storeName,
                        CommandType = CommandType.StoredProcedure,
                        Connection = Conn
                    };
                    string ItemType = ""; bool IsSetItemType = false; string keyCache = functionName + "." + storeName; bool IsCache = false;
                    json = ""; parameterOutput = ""; errorCode = (int)HttpStatusCode.NoContent; errorString = ("NOCONTENT");//"Không có giá trị"; 

                    d = parameterInput;
                    LogHelper.WriteExceptionToLog(String.Format("functionName: {0}\nstoreName: {1}\nparameterInput: null;", functionName, storeName));

                    if (d != null) kt = true;

                    try
                    {
                        // Create parameter
                        if (kt)
                        {
                            for (var i = 0; i < d.parameterInput.Count; i++)
                            {
                                if (d.parameterInput[i].ParamInOut.ToString() != "3") keyCache = keyCache + "." + d.parameterInput[i].ParamName.ToString() + "." + d.parameterInput[i].InputValue.ToString();
                                int nl = int.Parse(d.parameterInput[i].ParamLength.ToString());
                                int n1 = SwapDbType(d.parameterInput[i].ParamType, d.parameterInput[i].ParamLength, out nl);
                                if (n1 == 121)
                                {
                                    param[i] = new OracleParameter();
                                    param[i].ParameterName = "p_" + d.parameterInput[i].ParamName.ToString();
                                    param[i].OracleDbType = (OracleDbType)n1;// (OracleDbType)d.parameterInput[i].ParamType;
                                    param[i].Direction = (ParameterDirection)d.parameterInput[i].ParamInOut;
                                }
                                else if (n1 == 105 && d.parameterInput[i].ParamInOut.ToString() == "1") // Clob
                                {
                                    OracleClob clob = new OracleClob(Conn);
                                    var data = SetValToXMLByte(d.parameterInput[i].InputValue.ToString());
                                    clob.Write(data, 0, data.Length);
                                    param[i] = new OracleParameter();
                                    param[i].ParameterName = "p_" + d.parameterInput[i].ParamName.ToString();
                                    param[i].OracleDbType = (OracleDbType)n1;// (OracleDbType)d.parameterInput[i].ParamType;
                                    param[i].Direction = (ParameterDirection)d.parameterInput[i].ParamInOut;
                                    param[i].Value = clob;
                                }
                                else
                                {
                                    param[i] = new OracleParameter();
                                    param[i].ParameterName = "p_" + d.parameterInput[i].ParamName.ToString();
                                    param[i].OracleDbType = (OracleDbType)n1;// (OracleDbType)d.parameterInput[i].ParamType;
                                    param[i].Direction = (ParameterDirection)d.parameterInput[i].ParamInOut;
                                    param[i].Value = d.parameterInput[i].InputValue.ToString();
                                    if (nl > 0) param[i].Size = nl;
                                }
                                cmd.Parameters.Add(param[i]);
                            }
                        }
                        //IsCacheReq = (keyCache.Length < 100 && _appSettings.GetTimeCached() > 0);
                        //if (!IsNoCache) if (IsCacheReq) IsCache = _cache.Get(keyCache + "_" + _languageProcessor.GetLanguage(), out json);
                        if (!IsCache)
                        {
                            //TODO: return data table -> generate json
                            using (var reader = cmd.ExecuteReader())
                            {
                                RSCount = 0; //RS.RecordCount;
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        string jsonSub = ""; RSCount = RSCount + 1;
                                        for (int i = 0; i < reader.FieldCount; i++)
                                        {
                                            string dataType = reader.GetDataTypeName(i).ToString();
                                            if (!IsSetItemType)
                                            {
                                                ItemType = ItemType + ",\"" + reader.GetName(i).ToUpper() + "\":\"" + dataType + "\"";
                                            }
                                            if (",char,nchar,nclob,nvarchar2,clob,varchar2,".IndexOf("," + dataType.ToLower() + ",") > -1)
                                                jsonSub = jsonSub + "," + "\"" + reader.GetName(i).ToUpper() + "\":\"" + (reader.GetValue(i).ToString()) + "\"";
                                            else if (",raw,".IndexOf("," + dataType.ToLower() + ",") > -1)
                                            {
                                                byte[] rData = (byte[])reader.GetValue(i);
                                                jsonSub = jsonSub + "," + "\"" + reader.GetName(i).ToUpper() + "\":\"" + (new Guid(rData)) + "\"";
                                            }
                                            else if (",date,timestamp,timestampltz,timestamptz,".IndexOf("," + dataType.ToLower() + ",") > -1)
                                            {
                                                if (string.IsNullOrEmpty(reader.GetValue(i).ToString()))
                                                {
                                                    jsonSub = jsonSub + "," + "\"" + reader.GetName(i).ToUpper() + "\":\"\"";
                                                }
                                                else
                                                {
                                                    jsonSub = jsonSub + "," + "\"" + reader.GetName(i).ToUpper() + "\":\"" + reader.GetValue(i).ToString() + "\"";
                                                }
                                            }

                                            else
                                                jsonSub = jsonSub + "," + "\"" + reader.GetName(i).ToUpper() + "\":" + reader.GetValue(i).ToString();
                                        }
                                        json = json + ",{" + (jsonSub.Substring(1)) + "}";
                                        IsSetItemType = true;
                                    }
                                    json = (json.Substring(1));
                                    errorCode = (int)HttpStatusCode.Accepted; errorString = ("OK");//"Truy vấn thành công";
                                }
                                if (ItemType != "") ItemType = (ItemType.Substring(1));
                                ItemType = "{" + ItemType + "}";
                                //json = "{\"" + functionName + "\": {\"Count\": " + RSCount + ", \"ItemType\": " + ItemType + ", \"Items\":[" + json + "]}}";
                                reader.Close();
                            }

                            parameterOutput = ""; long StatusCode = 0; string Msg = ""; bool IsStatus = false;
                            if (kt)
                            {
                                for (var i = 0; i < d.parameterInput.Count; i++)
                                {
                                    SetDataOutput(d.parameterInput[i], param[i], ref parameterOutput, ref errorCode, ref errorString,
                                        ref StatusCode, ref Msg, ref IsStatus);
                                }
                            }
                            if (IsStatus)
                            {
                                parameterOutput = parameterOutput.Replace("[RESPONSESTATUS]", ", \"ResponseStatus\": " + StatusCode.ToString());
                            }
                            else
                            {
                                parameterOutput = parameterOutput.Replace("[RESPONSESTATUS]", "");
                            }
                            if (Msg != "")
                            {
                                parameterOutput = parameterOutput.Replace("[MESSAGE]", ", \"Message\": \"" + Msg + "\"");
                            }
                            else
                            {
                                parameterOutput = parameterOutput.Replace("[MESSAGE]", "");
                            }
                            /*
                            if (parameterOutput != "" || IsStatus)
                            {
                                parameterOutput = "\"ParameterOutput\": {" + Tools.RemoveFisrtChar(parameterOutput) + "}"; //json = Tools.RemoveFisrtChar(json);
                            }
                            else
                            {
                                parameterOutput = "\"ParameterOutput\": null";
                            }
                            */
                            if (parameterOutput != "" || IsStatus)
                            {
                                if (!IsStatus) // ko co ResponseStatus
                                    parameterOutput = "\"ParameterOutput\": {" + (parameterOutput.Substring(1)) + "}, ";
                                else if (StatusCode > 0) // Co ResponseStatus
                                    parameterOutput = "\"ParameterOutput\": {" + (parameterOutput.Substring(1)) + "}, " +
                                        "\"ResponseStatus\": " + StatusCode.ToString() + ", " +
                                        "\"Message\": \"" + Msg + "\", "
                                        ;
                                else
                                    parameterOutput = "\"ParameterOutput\": null, " +
                                        "\"ResponseStatus\": " + StatusCode.ToString() + ", " +
                                        "\"Message\": \"" + Msg + "\", ";
                            }


                            // Ghep response
                            json = "{\"" + functionName + "\":{" + parameterOutput + " \"Count\": " + RSCount + ", \"Items\":[" + json + "]}}";// + ", \"ItemType\": " + ItemType

                        }
                    }
                    catch (Exception e)
                    {
                        json = "{\"" + functionName + "\": {\"Count\": " + RSCount + ", \"Items\":[]}}"; RSCount = 0; errorCode = 500;

                        LogHelper.WriteExceptionToLog(String.Format("errorString: {0}\njson: {1}\ne: {2}", errorString, json, e.Message));
                    }
                    //CloseDB(ref Conn);
                    //RS = null;
                }
            }
            catch { }
        }

        /// <summary>
        /// Viết lại sau...
        /// </summary>
        /// <param name="functionName"></param>
        /// <param name="storeName"></param>
        /// <param name="parameterInput"></param>
        /// <param name="parameterOutput"></param>
        /// <param name="json"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorString"></param>
        /// <param name="IsNoCache"></param>
        public void ExecuteStore(string functionName, string storeName, CustomStoreParam parameterInput,
          ref string parameterOutput, ref string json, ref int errorCode, ref string errorString, bool IsNoCache = true)
        {
            bool kt = false; CustomStoreParam d; int RSCount = 0; OracleCommand cmd; OracleParameter[] param = new OracleParameter[300];
            try
            {
                using (OracleConnection Conn = new OracleConnection(ConnectionString))
                {
                    Conn.Open();
                    cmd = new OracleCommand()
                    {
                        CommandText = storeName,
                        CommandType = CommandType.StoredProcedure,
                        Connection = Conn
                    };
                    string ItemType = ""; bool IsSetItemType = false;
                    json = ""; parameterOutput = ""; errorCode = (int)HttpStatusCode.NoContent;/*HTTP_NO_CONTENT*/; errorString = ("NOCONTENT");//"Không có giá trị"; 

                    d = parameterInput;
                    if (d == null)
                        LogHelper.WriteExceptionToLog(String.Format("functionName: {0}\nstoreName: {1}\nparameterInput: null;", functionName, storeName));
                    else
                    {
                        kt = true;
                    }

                    try
                    {
                        // Create parameter
                        if (kt)
                        {
                            for (var i = 0; i < d.parameterInput.Count; i++)
                            {
                                int nl = int.Parse(d.parameterInput[i].ParamLength.ToString());
                                int n1 = SwapDbType(d.parameterInput[i].ParamType, d.parameterInput[i].ParamLength, out nl);
                                if (n1 == 121)
                                {
                                    param[i] = new OracleParameter();
                                    param[i].ParameterName = "p_" + d.parameterInput[i].ParamName.ToString();
                                    param[i].OracleDbType = (OracleDbType)n1;// (OracleDbType)d.parameterInput[i].ParamType;
                                    param[i].Direction = (ParameterDirection)d.parameterInput[i].ParamInOut;
                                }
                                else if (n1 == 105 && d.parameterInput[i].ParamInOut.ToString() == "1") // Clob
                                {
                                    OracleClob clob = new OracleClob(Conn);
                                    var data = SetValToXMLByte(d.parameterInput[i].InputValue.ToString());
                                    clob.Write(data, 0, data.Length);
                                    param[i] = new OracleParameter();
                                    param[i].ParameterName = "p_" + d.parameterInput[i].ParamName.ToString();
                                    param[i].OracleDbType = (OracleDbType)n1;// (OracleDbType)d.parameterInput[i].ParamType;
                                    param[i].Direction = (ParameterDirection)d.parameterInput[i].ParamInOut;
                                    param[i].Value = clob;
                                }
                                else
                                {
                                    param[i] = new OracleParameter();
                                    param[i].ParameterName = "p_" + d.parameterInput[i].ParamName.ToString();
                                    param[i].OracleDbType = (OracleDbType)n1;// (OracleDbType)d.parameterInput[i].ParamType;
                                    param[i].Direction = (ParameterDirection)d.parameterInput[i].ParamInOut;
                                    param[i].Value = d.parameterInput[i].InputValue.ToString();
                                    if (nl > 0) param[i].Size = nl;
                                }
                                cmd.Parameters.Add(param[i]);
                            }
                        }
                        {
                            using (var reader = cmd.ExecuteReader())
                            {
                                RSCount = 0; //RS.RecordCount;
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        string jsonSub = ""; RSCount = RSCount + 1;
                                        for (int i = 0; i < reader.FieldCount; i++)
                                        {
                                            string dataType = reader.GetDataTypeName(i).ToString();
                                            if (parameterInput.parameterInput.HasProperty(reader.GetName(i)))
                                            {

                                            }
                                            if (!IsSetItemType)
                                            {
                                                ItemType = ItemType + ",\"" + reader.GetName(i).ToUpper() + "\":\"" + dataType + "\"";
                                            }
                                            if (",char,nchar,nclob,nvarchar2,clob,varchar2,".IndexOf("," + dataType.ToLower() + ",") > -1)
                                                jsonSub = jsonSub + "," + "\"" + reader.GetName(i).ToUpper() + "\":\"" + (reader.GetValue(i).ToString()) + "\"";
                                            else if (",raw,".IndexOf("," + dataType.ToLower() + ",") > -1)
                                            {
                                                byte[] rData = (byte[])reader.GetValue(i);
                                                jsonSub = jsonSub + "," + "\"" + reader.GetName(i).ToUpper() + "\":\"" + (new Guid(rData)) + "\"";
                                            }
                                            else if (",date,timestamp,timestampltz,timestamptz,".IndexOf("," + dataType.ToLower() + ",") > -1)
                                            {
                                                if (string.IsNullOrEmpty(reader.GetValue(i).ToString()))
                                                {
                                                    jsonSub = jsonSub + "," + "\"" + reader.GetName(i).ToUpper() + "\":\"\"";
                                                }
                                                else
                                                {
                                                    jsonSub = jsonSub + "," + "\"" + reader.GetName(i).ToUpper() + "\":\"" + reader.GetValue(i).ToString() + "\"";
                                                }
                                            }

                                            else
                                                jsonSub = jsonSub + "," + "\"" + reader.GetName(i).ToUpper() + "\":" + reader.GetValue(i).ToString();
                                        }
                                        json = json + ",{" + (jsonSub.Substring(1)) + "}";
                                        IsSetItemType = true;
                                    }
                                    json = (json.Substring(1));
                                    errorCode = (int)HttpStatusCode.Accepted; errorString = ("OK");//"Truy vấn thành công";
                                }
                                if (ItemType != "") ItemType = (ItemType.Substring(1));
                                ItemType = "{" + ItemType + "}";
                                //json = "{\"" + functionName + "\": {\"Count\": " + RSCount + ", \"ItemType\": " + ItemType + ", \"Items\":[" + json + "]}}";
                                reader.Close();
                            }

                            parameterOutput = ""; long StatusCode = 0; string Msg = ""; bool IsStatus = false;
                            if (kt)
                            {
                                for (var i = 0; i < d.parameterInput.Count; i++)
                                {
                                    SetDataOutput(d.parameterInput[i], param[i], ref parameterOutput, ref errorCode, ref errorString,
                                        ref StatusCode, ref Msg, ref IsStatus);
                                }
                            }
                            if (IsStatus)
                            {
                                parameterOutput = parameterOutput.Replace("[RESPONSESTATUS]", ", \"ResponseStatus\": " + StatusCode.ToString());
                            }
                            else
                            {
                                parameterOutput = parameterOutput.Replace("[RESPONSESTATUS]", "");
                            }
                            if (Msg != "")
                            {
                                parameterOutput = parameterOutput.Replace("[MESSAGE]", ", \"Message\": \"" + Msg + "\"");
                            }
                            else
                            {
                                parameterOutput = parameterOutput.Replace("[MESSAGE]", "");
                            }
                            if (parameterOutput != "" || IsStatus)
                            {
                                if (!IsStatus) // ko co ResponseStatus
                                    parameterOutput = "\"ParameterOutput\": {" + (parameterOutput.Substring(1)) + "}, ";
                                else if (StatusCode > 0) // Co ResponseStatus
                                    parameterOutput = "\"ParameterOutput\": {" + (parameterOutput.Substring(1)) + "}, " +
                                        "\"ResponseStatus\": " + StatusCode.ToString() + ", " +
                                        "\"Message\": \"" + Msg + "\", "
                                        ;
                                else
                                    parameterOutput = "\"ParameterOutput\": null, " +
                                        "\"ResponseStatus\": " + StatusCode.ToString() + ", " +
                                        "\"Message\": \"" + Msg + "\", ";
                            }
                            // Ghep response
                            json = "{\"" + functionName + "\":{" + parameterOutput + " \"Count\": " + RSCount + ", \"Items\":[" + json + "]}}";// + ", \"ItemType\": " + ItemType
                        }
                    }
                    catch (Exception e)
                    {
                        json = "{\"" + functionName + "\": {\"Count\": " + RSCount + ", \"Items\":[]}}"; RSCount = 0; errorCode = 500;
                        LogHelper.WriteExceptionToLog(String.Format("errorString: {0}\njson: {1}\ne: {2}", errorString, json, e.Message));
                    }
                }
            }
            catch { }
        }


        public void ExecuteStoreToDataset(string functionName, string storeName, dynamic parameterInput, ref string parameterOutput, ref string json, ref int errorCode, ref string errorString, bool IsNoCache = false)
        {
            bool kt = false; dynamic d; int RSCount = 0; OracleCommand cmd; OracleParameter[] param = new OracleParameter[300];
            //OpenDB(ref Conn);
            try
            {
                using (OracleConnection Conn = new OracleConnection(ConnectionString))
                {
                    cmd = new OracleCommand()
                    {
                        CommandText = storeName,
                        CommandType = CommandType.StoredProcedure,
                        Connection = Conn
                    };
                    if (parameterInput != null)
                        LogHelper.WriteExceptionToLog(String.Format("functionName: {0}\nstoreName: {1}\nparameterInput:{2};", functionName, storeName, parameterInput.ToString()));
                    else
                        LogHelper.WriteExceptionToLog(String.Format("functionName: {0}\nstoreName: {1}\nparameterInput: null;", functionName, storeName));

                    string keyCache = functionName + "." + storeName; bool IsCache = false;
                    json = ""; parameterOutput = ""; errorCode = 0; errorString = ("NOCONTENT");//"Không có giá trị";

                    d = parameterInput;
                    if (d != null) kt = true;
                    try
                    {
                        // Create parameter
                        if (kt)
                        {
                            for (var i = 0; i < d.parameterInput.Count; i++)
                            {
                                if (d.parameterInput[i].ParamInOut.ToString() != "3") keyCache = keyCache + "." + d.parameterInput[i].ParamName.ToString() + "." + d.parameterInput[i].InputValue.ToString();
                                int nl = int.Parse(d.parameterInput[i].ParamLength.ToString());
                                int n1 = SwapDbType(d.parameterInput[i].ParamType, d.parameterInput[i].ParamLength, out nl);
                                if (n1 == 121)
                                {
                                    param[i] = new OracleParameter();
                                    param[i].ParameterName = "p_" + d.parameterInput[i].ParamName.ToString();
                                    param[i].OracleDbType = (OracleDbType)n1;
                                    param[i].Direction = (ParameterDirection)d.parameterInput[i].ParamInOut;
                                }
                                else if (n1 == 105 && d.parameterInput[i].ParamInOut.ToString() == "1") // Clob
                                {
                                    OracleClob clob = new OracleClob(Conn);
                                    var data = SetValToXMLByte(d.parameterInput[i].InputValue.ToString());
                                    clob.Write(data, 0, data.Length);
                                    param[i] = new OracleParameter();
                                    param[i].ParameterName = "p_" + d.parameterInput[i].ParamName.ToString();
                                    param[i].OracleDbType = (OracleDbType)n1;// (OracleDbType)d.parameterInput[i].ParamType;
                                    param[i].Direction = (ParameterDirection)d.parameterInput[i].ParamInOut;
                                    param[i].Value = clob;
                                }
                                else
                                {
                                    param[i] = new OracleParameter();
                                    param[i].ParameterName = "p_" + d.parameterInput[i].ParamName.ToString();
                                    param[i].OracleDbType = (OracleDbType)n1;
                                    param[i].Direction = (ParameterDirection)d.parameterInput[i].ParamInOut;
                                    param[i].Value = d.parameterInput[i].InputValue.ToString();
                                    if (nl > 0) param[i].Size = nl;
                                }
                                cmd.Parameters.Add(param[i]);
                            }
                        }

                        if (!IsCache)
                        {
                            using (OracleDataAdapter da = new OracleDataAdapter())
                            {
                                //cmd.ExecuteNonQuery();
                                da.SelectCommand = cmd;
                                //da.SelectCommand.CommandType = CommandType.StoredProcedure;
                                DataSet ds = new DataSet();
                                da.Fill(ds);
                                int i = 0;
                                string ItemTypes = "";
                                if (ds.Tables.Count > 0)
                                {
                                    foreach (DataTable data in ds.Tables)
                                    {
                                        string IType = "";
                                        if (i == 0)
                                        {
                                            data.TableName = "Items";
                                            RSCount = data.Rows.Count;
                                            for (int j = 0; j < data.Columns.Count; j++)
                                            {
                                                IType = IType + String.Format(", \"{0}\":\"{1}\"", data.Columns[j].ColumnName, data.Columns[j].DataType.Name);
                                            }
                                            ItemTypes = ItemTypes + String.Format(",\"ItemType\":{0}{2}{1}", "{", "}", (IType.Substring(1)));
                                        }
                                        else
                                        {
                                            data.TableName = "Items" + i;
                                            for (int j = 0; j < data.Columns.Count; j++)
                                            {
                                                IType = IType + String.Format(", \"{0}\":\"{1}\"", data.Columns[j].ColumnName, data.Columns[j].DataType.Name);
                                            }
                                            ItemTypes = ItemTypes + String.Format(",\"ItemType{3}\":{0}{2}{1}", "{", "}", (IType.Substring(1)), i.ToString());
                                        }
                                        i++;
                                    }
                                }
                                parameterOutput = ""; long StatusCode = 0; string Msg = ""; bool IsStatus = false;
                                if (kt)
                                {
                                    for (i = 0; i < d.parameterInput.Count; i++)
                                    {
                                        if ((d.parameterInput[i].ParamInOut.ToString() == "3") || (d.parameterInput[i].ParamInOut.ToString() == "2"))
                                        {
                                            SetDataOutput(d.parameterInput[i], da.SelectCommand.Parameters[i], ref parameterOutput, ref errorCode, ref errorString,
                                                ref StatusCode, ref Msg, ref IsStatus);
                                        }
                                    }
                                }
                                if (IsStatus)
                                {
                                    parameterOutput = parameterOutput.Replace("[RESPONSESTATUS]", ", \"ResponseStatus\": " + StatusCode.ToString());
                                }
                                else
                                {
                                    parameterOutput = parameterOutput.Replace("[RESPONSESTATUS]", "");
                                }
                                if (Msg != "")
                                {
                                    parameterOutput = parameterOutput.Replace("[MESSAGE]", ", \"Message\": \"" + Msg + "\"");
                                }
                                else
                                {
                                    parameterOutput = parameterOutput.Replace("[MESSAGE]", "");
                                }

                                if (parameterOutput != "")
                                {
                                    parameterOutput = "\"ParameterOutput\": {" + (parameterOutput.Substring(1)) + "}"; //json = Tools.RemoveFisrtChar(json);
                                }
                                else
                                {
                                    parameterOutput = "\"ParameterOutput\": null";
                                }
                                // Ghep response
                                json = string.Format("{0}\"{1}\":{0}{2}, \"Count\":{3},{4}{6}{5}{5}", "{", functionName, parameterOutput, RSCount, (((JsonConvert.SerializeObject(ds, Formatting.Indented)).ToString()).Substring(1)).Substring(1), "}", ItemTypes);

                            }
                        }
                        else
                        {
                        }
                    }
                    catch (Exception e)
                    {
                        json = "{\"" + functionName + "\": {\"ParameterOutput\": null, \"Count\": " + RSCount + ", \"Items\":[]}}"; RSCount = 0; errorCode = 500;

                        LogHelper.WriteExceptionToLog(String.Format("errorString: {0}\njson: {1}\ne: {2}", errorString, json, e.Message));
                    }
                }
            }
            catch { }
            //CloseDB(ref Conn);
        }


        private int SwapDbType(dynamic ParamType, dynamic ParamLength, out int nl)
        {
            int n = int.Parse(ParamType.ToString());
            nl = int.Parse(ParamLength.ToString());
            int n1 = 0;
            switch (n)
            {
                case 121:
                case 999:
                    n1 = 121; nl = -1;
                    break;
                case 0:
                    //n1 = 113;
                    n1 = 107;
                    nl = 19;
                    break;
                case 8:
                    //n1 = 112;
                    n1 = 107;
                    nl = 10;
                    break;
                case 16:
                    //n1 = 111;
                    n1 = 107;
                    nl = 5;
                    break;
                case 20:
                case 5:
                case 6:
                case 13:
                    n1 = 107;
                    nl = 19;
                    break;
                case 31:
                    n1 = 106; nl = -1;
                    break;
                case 4:
                    n1 = 123; nl = -1;
                    break;
                case 32:
                    n1 = 123; nl = -1;
                    break;
                case 9:
                    n1 = 107;
                    nl = 19;
                    break;
                case 17:
                    n1 = 107;
                    nl = 10;
                    break;
                case 10:
                    n1 = 117;
                    break;
                case 12:
                    n1 = 119; if (nl == -1) n1 = 105;
                    break;
                case 22:
                    n1 = 126; if (nl == -1) n1 = 105;
                    break;
                default:
                    n1 = n;
                    break;
            }
            return n1;
        }

        private byte[] SetValToXMLByte(string data)
        {
            //System.IO.MemoryStream ms = new System.IO.MemoryStream();
            return System.Text.Encoding.Unicode.GetBytes(data);
            //return ms.GetBuffer();
        }

        private void SetDataOutput(dynamic d, OracleParameter param,
            ref string parameterOutput, ref int errorCode, ref string errorString,
            ref long StatusCode, ref string Msg, ref bool IsStatus)
        {
            if ((d.ParamInOut.ToString() == "3") || (d.ParamInOut.ToString() == "2"))
            {
                if (d.ParamName.ToString().ToLower() == "responsestatus")
                {
                    long longCode = long.Parse(param.Value.ToString());
                    //errorCode = long.Parse(resStatus);
                    if (longCode > 0)
                        errorCode = (int)HttpStatusCode.Accepted;// 200;
                    else if (longCode == -99)
                        errorCode = 500;
                    else
                        errorCode = (int)HttpStatusCode.Accepted;//200; //HTTP_CODE.HTTP_BAD_REQUEST;

                    StatusCode = long.Parse(param.Value.ToString());
                    IsStatus = true;
                    parameterOutput = parameterOutput + "[RESPONSESTATUS]";
                }
                else if (d.ParamName.ToString().ToLower() == "message")
                {
                    parameterOutput = parameterOutput + "[MESSAGE]";
                    errorString = param.Value.ToString();
                    Msg = errorString.Replace("'","");
                }
                else
                {
                    int nll = 0; int dbType = SwapDbType(d.ParamType, d.ParamLength, out nll);
                    switch (dbType)
                    {
                        case 102:
                        case 105:
                        case 116:
                            parameterOutput = parameterOutput + ",\"" + d.ParamName + "\":" +
                                    "\"" + (((OracleClob)param.Value).Value.ToString().Replace("\"", "\\\"")) + "\"";

                            break;
                        case 104:
                        case 117:
                        case 119:
                        case 126:
                            parameterOutput = parameterOutput + ",\"" + d.ParamName + "\":" +
                                "\"" + (param.Value.ToString().Replace("\"", "\\\"")) + "\"";

                            break;
                        case 106:
                        case 123:
                        case 124:
                        case 125:
                            parameterOutput = parameterOutput + ",\"" + d.ParamName + "\": \"" + ((DateTime)param.Value).ToString("yyyy-MM-dd HH:mm:ss") + "\"";
                            break;
                        case 107:
                        case 108:
                        case 111:
                        case 112:
                        case 113:
                        case 122:
                            parameterOutput = parameterOutput + ",\"" + d.ParamName + "\": " + param.Value.ToString();
                            break;
                        default:
                            parameterOutput = parameterOutput + ",\"" + d.ParamName + "\": \"" + param.Value.ToString() + "\"";
                            break;
                    }
                }
            }
        }
    }
}