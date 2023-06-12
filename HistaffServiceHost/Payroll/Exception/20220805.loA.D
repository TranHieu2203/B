Function: CheckAndSendPayslip
Datetime: 08/05/2022 1:58:07 PM
Error message: An error occurred while executing the command definition. See the inner exception for details.
Stack trace:    at System.Data.EntityClient.EntityCommandDefinition.ExecuteStoreCommands(EntityCommand entityCommand, CommandBehavior behavior)
   at System.Data.Objects.Internal.ObjectQueryExecutionPlan.Execute[TResultType](ObjectContext context, ObjectParameterCollection parameterValues)
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Linq.Enumerable.Single[TSource](IEnumerable`1 source)
   at PayrollDAL.PayrollRepository.CheckAndSendPayslip() in D:\GIT\BCG_DEV_NEW\PayrollDAL\PayrollRepository.vb:line 387
Inner error message: ORA-03135: connection lost contact
Process ID: 1276
Session ID: 1172 Serial number: 10073
Inner stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck, Int32 isRecoverable, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, String procedure, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, Boolean bCheck, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleCommand.ExecuteReader(Boolean requery, Boolean fillRequest, CommandBehavior behavior)
   at Oracle.DataAccess.Client.OracleCommand.ExecuteDbDataReader(CommandBehavior behavior)
   at System.Data.EntityClient.EntityCommandDefinition.ExecuteStoreCommands(EntityCommand entityCommand, CommandBehavior behavior)
Other Text: iPayroll

Function: CheckAndSendPayslip
Datetime: 08/05/2022 1:59:07 PM
Error message: The connection is not open.
Stack trace:    at System.Data.EntityClient.EntityConnection.Open()
   at System.Data.Objects.ObjectContext.EnsureConnection()
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Linq.Enumerable.Single[TSource](IEnumerable`1 source)
   at PayrollDAL.PayrollRepository.CheckAndSendPayslip() in D:\GIT\BCG_DEV_NEW\PayrollDAL\PayrollRepository.vb:line 387
Other Text: iPayroll

