Function: SendMail
Datetime: 06/28/2022 2:21:27 PM
Error message: The underlying provider failed on Open.
Stack trace:    at CommonDAL.CommonRepository.GetConfig(ModuleID eModule) in D:\GIT\BCG_DEV_NEW\CommonDAL\CommonRepository.Secure.vb:line 4024
   at CommonDAL.CommonRepository.SendMail() in D:\GIT\BCG_DEV_NEW\CommonDAL\CommonRepository.vb:line 1016
Inner error message: Connection request timed out
Inner stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck, Int32 isRecoverable, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, Object src, OracleLogicalTransaction m_oracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleConnectionOCP.Open(OracleConnection con)
   at Oracle.DataAccess.Client.OracleConnection.Open()
   at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
Other Text: iCommon

