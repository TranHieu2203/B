Function: SendMail
Datetime: 07/14/2022 1:32:17 PM
Error message: An error occurred while executing the command definition. See the inner exception for details.
Stack trace:    at CommonDAL.CommonRepository.GetConfig(ModuleID eModule) in D:\GIT\BCG_DEV_NEW\CommonDAL\CommonRepository.Secure.vb:line 4304
   at CommonDAL.CommonRepository.SendMail() in D:\GIT\BCG_DEV_NEW\CommonDAL\CommonRepository.vb:line 1016
Inner error message: ORA-03135: connection lost contact
Process ID: 6100
Session ID: 991 Serial number: 32465
Inner stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck, Int32 isRecoverable, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, String procedure, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, Boolean bCheck, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleCommand.ExecuteReader(Boolean requery, Boolean fillRequest, CommandBehavior behavior)
   at Oracle.DataAccess.Client.OracleCommand.ExecuteDbDataReader(CommandBehavior behavior)
   at System.Data.EntityClient.EntityCommandDefinition.ExecuteStoreCommands(EntityCommand entityCommand, CommandBehavior behavior)
Other Text: iCommon

