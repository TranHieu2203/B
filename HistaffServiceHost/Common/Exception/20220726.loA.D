Function: SendMail
Datetime: 07/26/2022 3:17:17 PM
Error message: An error occurred while reading from the store provider's data reader. See the inner exception for details.
Stack trace:    at CommonDAL.CommonRepository.GetConfig(ModuleID eModule) in D:\GIT\BCG_DEV_NEW\CommonDAL\CommonRepository.Secure.vb:line 4313
   at CommonDAL.CommonRepository.SendMail() in D:\GIT\BCG_DEV_NEW\CommonDAL\CommonRepository.vb:line 1016
Inner error message: ORA-03135: connection lost contact
Process ID: 876
Session ID: 1361 Serial number: 7966
Inner stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck, Int32 isRecoverable, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleDataReader.Read()
   at System.Data.Common.Internal.Materialization.Shaper`1.StoreRead()
Other Text: iCommon

