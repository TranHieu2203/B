Function: SendMail
Datetime: 07/13/2022 3:04:01 PM
Error message: An error occurred while executing the command definition. See the inner exception for details.
Stack trace:    at CommonDAL.CommonRepository.GetConfig(ModuleID eModule) in D:\GIT\BCG_DEV_NEW\CommonDAL\CommonRepository.Secure.vb:line 4304
   at CommonDAL.CommonRepository.SendMail() in D:\GIT\BCG_DEV_NEW\CommonDAL\CommonRepository.vb:line 1016
Inner error message: ORA-03135: connection lost contact
Process ID: 4800
Session ID: 609 Serial number: 27126
Inner stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck, Int32 isRecoverable, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, String procedure, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, Boolean bCheck, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleCommand.ExecuteReader(Boolean requery, Boolean fillRequest, CommandBehavior behavior)
   at Oracle.DataAccess.Client.OracleCommand.ExecuteDbDataReader(CommandBehavior behavior)
   at System.Data.EntityClient.EntityCommandDefinition.ExecuteStoreCommands(EntityCommand entityCommand, CommandBehavior behavior)
Other Text: iCommon

Function: ValidateIDTable
Datetime: 07/13/2022 5:09:56 PM
Error message: ORA-00942: table or view does not exist
Stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck, Int32 isRecoverable, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, String procedure, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, Boolean bCheck, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleCommand.ExecuteReader(Boolean requery, Boolean fillRequest, CommandBehavior behavior)
   at Oracle.DataAccess.Client.OracleCommand.ExecuteDbDataReader(CommandBehavior behavior)
   at System.Data.Objects.ObjectContext.ExecuteStoreQueryInternal[TElement](String commandText, String entitySetName, MergeOption mergeOption, Object[] parameters)
   at System.Data.Objects.ObjectContext.ExecuteStoreQuery[TElement](String commandText, Object[] parameters)
   at CommonDAL.CommonRepository.ValidateIDTable(String tableName, String colName, String strListID) in D:\GIT\BCG_DEV_NEW\CommonDAL\CommonRepository.vb:line 1373
Other Text: iCommon

Function: ValidateIDTable
Datetime: 07/13/2022 5:10:05 PM
Error message: ORA-00942: table or view does not exist
Stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck, Int32 isRecoverable, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, String procedure, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, Boolean bCheck, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleCommand.ExecuteReader(Boolean requery, Boolean fillRequest, CommandBehavior behavior)
   at Oracle.DataAccess.Client.OracleCommand.ExecuteDbDataReader(CommandBehavior behavior)
   at System.Data.Objects.ObjectContext.ExecuteStoreQueryInternal[TElement](String commandText, String entitySetName, MergeOption mergeOption, Object[] parameters)
   at System.Data.Objects.ObjectContext.ExecuteStoreQuery[TElement](String commandText, Object[] parameters)
   at CommonDAL.CommonRepository.ValidateIDTable(String tableName, String colName, String strListID) in D:\GIT\BCG_DEV_NEW\CommonDAL\CommonRepository.vb:line 1373
Other Text: iCommon

Function: ValidateIDTable
Datetime: 07/13/2022 5:15:17 PM
Error message: ORA-00942: table or view does not exist
Stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck, Int32 isRecoverable, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, String procedure, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, Boolean bCheck, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleCommand.ExecuteReader(Boolean requery, Boolean fillRequest, CommandBehavior behavior)
   at Oracle.DataAccess.Client.OracleCommand.ExecuteDbDataReader(CommandBehavior behavior)
   at System.Data.Objects.ObjectContext.ExecuteStoreQueryInternal[TElement](String commandText, String entitySetName, MergeOption mergeOption, Object[] parameters)
   at System.Data.Objects.ObjectContext.ExecuteStoreQuery[TElement](String commandText, Object[] parameters)
   at CommonDAL.CommonRepository.ValidateIDTable(String tableName, String colName, String strListID) in D:\GIT\BCG_DEV_NEW\CommonDAL\CommonRepository.vb:line 1373
Other Text: iCommon

Function: ValidateIDTable
Datetime: 07/13/2022 5:15:17 PM
Error message: ORA-00942: table or view does not exist
Stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck, Int32 isRecoverable, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, String procedure, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, Boolean bCheck, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleCommand.ExecuteReader(Boolean requery, Boolean fillRequest, CommandBehavior behavior)
   at Oracle.DataAccess.Client.OracleCommand.ExecuteDbDataReader(CommandBehavior behavior)
   at System.Data.Objects.ObjectContext.ExecuteStoreQueryInternal[TElement](String commandText, String entitySetName, MergeOption mergeOption, Object[] parameters)
   at System.Data.Objects.ObjectContext.ExecuteStoreQuery[TElement](String commandText, Object[] parameters)
   at CommonDAL.CommonRepository.ValidateIDTable(String tableName, String colName, String strListID) in D:\GIT\BCG_DEV_NEW\CommonDAL\CommonRepository.vb:line 1373
Other Text: iCommon

Function: ValidateIDTable
Datetime: 07/13/2022 5:18:23 PM
Error message: ORA-00942: table or view does not exist
Stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck, Int32 isRecoverable, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, String procedure, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, Boolean bCheck, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleCommand.ExecuteReader(Boolean requery, Boolean fillRequest, CommandBehavior behavior)
   at Oracle.DataAccess.Client.OracleCommand.ExecuteDbDataReader(CommandBehavior behavior)
   at System.Data.Objects.ObjectContext.ExecuteStoreQueryInternal[TElement](String commandText, String entitySetName, MergeOption mergeOption, Object[] parameters)
   at System.Data.Objects.ObjectContext.ExecuteStoreQuery[TElement](String commandText, Object[] parameters)
   at CommonDAL.CommonRepository.ValidateIDTable(String tableName, String colName, String strListID) in D:\GIT\BCG_DEV_NEW\CommonDAL\CommonRepository.vb:line 1373
Other Text: iCommon

Function: ValidateIDTable
Datetime: 07/13/2022 5:18:59 PM
Error message: ORA-00942: table or view does not exist
Stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck, Int32 isRecoverable, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, String procedure, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, Boolean bCheck, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleCommand.ExecuteReader(Boolean requery, Boolean fillRequest, CommandBehavior behavior)
   at Oracle.DataAccess.Client.OracleCommand.ExecuteDbDataReader(CommandBehavior behavior)
   at System.Data.Objects.ObjectContext.ExecuteStoreQueryInternal[TElement](String commandText, String entitySetName, MergeOption mergeOption, Object[] parameters)
   at System.Data.Objects.ObjectContext.ExecuteStoreQuery[TElement](String commandText, Object[] parameters)
   at CommonDAL.CommonRepository.ValidateIDTable(String tableName, String colName, String strListID) in D:\GIT\BCG_DEV_NEW\CommonDAL\CommonRepository.vb:line 1373
Other Text: iCommon

Function: ValidateIDTable
Datetime: 07/13/2022 5:18:59 PM
Error message: ORA-00942: table or view does not exist
Stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck, Int32 isRecoverable, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, String procedure, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, Boolean bCheck, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleCommand.ExecuteReader(Boolean requery, Boolean fillRequest, CommandBehavior behavior)
   at Oracle.DataAccess.Client.OracleCommand.ExecuteDbDataReader(CommandBehavior behavior)
   at System.Data.Objects.ObjectContext.ExecuteStoreQueryInternal[TElement](String commandText, String entitySetName, MergeOption mergeOption, Object[] parameters)
   at System.Data.Objects.ObjectContext.ExecuteStoreQuery[TElement](String commandText, Object[] parameters)
   at CommonDAL.CommonRepository.ValidateIDTable(String tableName, String colName, String strListID) in D:\GIT\BCG_DEV_NEW\CommonDAL\CommonRepository.vb:line 1373
Other Text: iCommon

Function: ValidateIDTable
Datetime: 07/13/2022 5:19:21 PM
Error message: ORA-00942: table or view does not exist
Stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck, Int32 isRecoverable, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, String procedure, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, Boolean bCheck, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleCommand.ExecuteReader(Boolean requery, Boolean fillRequest, CommandBehavior behavior)
   at Oracle.DataAccess.Client.OracleCommand.ExecuteDbDataReader(CommandBehavior behavior)
   at System.Data.Objects.ObjectContext.ExecuteStoreQueryInternal[TElement](String commandText, String entitySetName, MergeOption mergeOption, Object[] parameters)
   at System.Data.Objects.ObjectContext.ExecuteStoreQuery[TElement](String commandText, Object[] parameters)
   at CommonDAL.CommonRepository.ValidateIDTable(String tableName, String colName, String strListID) in D:\GIT\BCG_DEV_NEW\CommonDAL\CommonRepository.vb:line 1373
Other Text: iCommon

Function: ValidateIDTable
Datetime: 07/13/2022 5:19:21 PM
Error message: ORA-00942: table or view does not exist
Stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck, Int32 isRecoverable, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, String procedure, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, Boolean bCheck, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleCommand.ExecuteReader(Boolean requery, Boolean fillRequest, CommandBehavior behavior)
   at Oracle.DataAccess.Client.OracleCommand.ExecuteDbDataReader(CommandBehavior behavior)
   at System.Data.Objects.ObjectContext.ExecuteStoreQueryInternal[TElement](String commandText, String entitySetName, MergeOption mergeOption, Object[] parameters)
   at System.Data.Objects.ObjectContext.ExecuteStoreQuery[TElement](String commandText, Object[] parameters)
   at CommonDAL.CommonRepository.ValidateIDTable(String tableName, String colName, String strListID) in D:\GIT\BCG_DEV_NEW\CommonDAL\CommonRepository.vb:line 1373
Other Text: iCommon

Function: ValidateIDTable
Datetime: 07/13/2022 5:20:37 PM
Error message: ORA-00942: table or view does not exist
Stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck, Int32 isRecoverable, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, String procedure, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, Boolean bCheck, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleCommand.ExecuteReader(Boolean requery, Boolean fillRequest, CommandBehavior behavior)
   at Oracle.DataAccess.Client.OracleCommand.ExecuteDbDataReader(CommandBehavior behavior)
   at System.Data.Objects.ObjectContext.ExecuteStoreQueryInternal[TElement](String commandText, String entitySetName, MergeOption mergeOption, Object[] parameters)
   at System.Data.Objects.ObjectContext.ExecuteStoreQuery[TElement](String commandText, Object[] parameters)
   at CommonDAL.CommonRepository.ValidateIDTable(String tableName, String colName, String strListID) in D:\GIT\BCG_DEV_NEW\CommonDAL\CommonRepository.vb:line 1373
Other Text: iCommon

Function: ValidateIDTable
Datetime: 07/13/2022 5:20:37 PM
Error message: ORA-00942: table or view does not exist
Stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck, Int32 isRecoverable, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, String procedure, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, Boolean bCheck, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleCommand.ExecuteReader(Boolean requery, Boolean fillRequest, CommandBehavior behavior)
   at Oracle.DataAccess.Client.OracleCommand.ExecuteDbDataReader(CommandBehavior behavior)
   at System.Data.Objects.ObjectContext.ExecuteStoreQueryInternal[TElement](String commandText, String entitySetName, MergeOption mergeOption, Object[] parameters)
   at System.Data.Objects.ObjectContext.ExecuteStoreQuery[TElement](String commandText, Object[] parameters)
   at CommonDAL.CommonRepository.ValidateIDTable(String tableName, String colName, String strListID) in D:\GIT\BCG_DEV_NEW\CommonDAL\CommonRepository.vb:line 1373
Other Text: iCommon

Function: ValidateIDTable
Datetime: 07/13/2022 5:22:21 PM
Error message: ORA-00942: table or view does not exist
Stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck, Int32 isRecoverable, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, String procedure, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, Boolean bCheck, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleCommand.ExecuteReader(Boolean requery, Boolean fillRequest, CommandBehavior behavior)
   at Oracle.DataAccess.Client.OracleCommand.ExecuteDbDataReader(CommandBehavior behavior)
   at System.Data.Objects.ObjectContext.ExecuteStoreQueryInternal[TElement](String commandText, String entitySetName, MergeOption mergeOption, Object[] parameters)
   at System.Data.Objects.ObjectContext.ExecuteStoreQuery[TElement](String commandText, Object[] parameters)
   at CommonDAL.CommonRepository.ValidateIDTable(String tableName, String colName, String strListID) in D:\GIT\BCG_DEV_NEW\CommonDAL\CommonRepository.vb:line 1373
Other Text: iCommon

Function: ValidateIDTable
Datetime: 07/13/2022 5:22:21 PM
Error message: ORA-00942: table or view does not exist
Stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck, Int32 isRecoverable, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, String procedure, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, Boolean bCheck, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleCommand.ExecuteReader(Boolean requery, Boolean fillRequest, CommandBehavior behavior)
   at Oracle.DataAccess.Client.OracleCommand.ExecuteDbDataReader(CommandBehavior behavior)
   at System.Data.Objects.ObjectContext.ExecuteStoreQueryInternal[TElement](String commandText, String entitySetName, MergeOption mergeOption, Object[] parameters)
   at System.Data.Objects.ObjectContext.ExecuteStoreQuery[TElement](String commandText, Object[] parameters)
   at CommonDAL.CommonRepository.ValidateIDTable(String tableName, String colName, String strListID) in D:\GIT\BCG_DEV_NEW\CommonDAL\CommonRepository.vb:line 1373
Other Text: iCommon

Function: ValidateIDTable
Datetime: 07/13/2022 5:25:22 PM
Error message: ORA-00942: table or view does not exist
Stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck, Int32 isRecoverable, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, String procedure, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, Boolean bCheck, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleCommand.ExecuteReader(Boolean requery, Boolean fillRequest, CommandBehavior behavior)
   at Oracle.DataAccess.Client.OracleCommand.ExecuteDbDataReader(CommandBehavior behavior)
   at System.Data.Objects.ObjectContext.ExecuteStoreQueryInternal[TElement](String commandText, String entitySetName, MergeOption mergeOption, Object[] parameters)
   at System.Data.Objects.ObjectContext.ExecuteStoreQuery[TElement](String commandText, Object[] parameters)
   at CommonDAL.CommonRepository.ValidateIDTable(String tableName, String colName, String strListID) in D:\GIT\BCG_DEV_NEW\CommonDAL\CommonRepository.vb:line 1373
Other Text: iCommon

Function: ValidateIDTable
Datetime: 07/13/2022 5:25:22 PM
Error message: ORA-00942: table or view does not exist
Stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck, Int32 isRecoverable, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, String procedure, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, Boolean bCheck, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleCommand.ExecuteReader(Boolean requery, Boolean fillRequest, CommandBehavior behavior)
   at Oracle.DataAccess.Client.OracleCommand.ExecuteDbDataReader(CommandBehavior behavior)
   at System.Data.Objects.ObjectContext.ExecuteStoreQueryInternal[TElement](String commandText, String entitySetName, MergeOption mergeOption, Object[] parameters)
   at System.Data.Objects.ObjectContext.ExecuteStoreQuery[TElement](String commandText, Object[] parameters)
   at CommonDAL.CommonRepository.ValidateIDTable(String tableName, String colName, String strListID) in D:\GIT\BCG_DEV_NEW\CommonDAL\CommonRepository.vb:line 1373
Other Text: iCommon

