Function: CheckAndSendPayslip
Datetime: 08/29/2022 11:04:52 AM
Error message: An error occurred while executing the command definition. See the inner exception for details.
Stack trace:    at System.Data.EntityClient.EntityCommandDefinition.ExecuteStoreCommands(EntityCommand entityCommand, CommandBehavior behavior)
   at System.Data.Objects.Internal.ObjectQueryExecutionPlan.Execute[TResultType](ObjectContext context, ObjectParameterCollection parameterValues)
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Linq.Enumerable.Single[TSource](IEnumerable`1 source)
   at PayrollDAL.PayrollRepository.CheckAndSendPayslip() in D:\GIT\BCG_DEV_NEW\PayrollDAL\PayrollRepository.vb:line 387
Inner error message: ORA-03135: connection lost contact
Process ID: 6052
Session ID: 498 Serial number: 18170
Inner stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck, Int32 isRecoverable, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, String procedure, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, Boolean bCheck, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleCommand.ExecuteReader(Boolean requery, Boolean fillRequest, CommandBehavior behavior)
   at Oracle.DataAccess.Client.OracleCommand.ExecuteDbDataReader(CommandBehavior behavior)
   at System.Data.EntityClient.EntityCommandDefinition.ExecuteStoreCommands(EntityCommand entityCommand, CommandBehavior behavior)
Other Text: iPayroll

Function: CheckAndSendPayslip
Datetime: 08/29/2022 11:28:53 AM
Error message: An error occurred while executing the command definition. See the inner exception for details.
Stack trace:    at System.Data.EntityClient.EntityCommandDefinition.ExecuteStoreCommands(EntityCommand entityCommand, CommandBehavior behavior)
   at System.Data.Objects.Internal.ObjectQueryExecutionPlan.Execute[TResultType](ObjectContext context, ObjectParameterCollection parameterValues)
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Linq.Enumerable.Single[TSource](IEnumerable`1 source)
   at PayrollDAL.PayrollRepository.CheckAndSendPayslip() in D:\GIT\BCG_DEV_NEW\PayrollDAL\PayrollRepository.vb:line 387
Inner error message: ORA-03135: connection lost contact
Process ID: 7676
Session ID: 565 Serial number: 1432
Inner stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck, Int32 isRecoverable, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, String procedure, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, Boolean bCheck, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleCommand.ExecuteReader(Boolean requery, Boolean fillRequest, CommandBehavior behavior)
   at Oracle.DataAccess.Client.OracleCommand.ExecuteDbDataReader(CommandBehavior behavior)
   at System.Data.EntityClient.EntityCommandDefinition.ExecuteStoreCommands(EntityCommand entityCommand, CommandBehavior behavior)
Other Text: iPayroll

Function: CheckAndSendPayslip
Datetime: 08/29/2022 12:05:53 PM
Error message: An error occurred while executing the command definition. See the inner exception for details.
Stack trace:    at System.Data.EntityClient.EntityCommandDefinition.ExecuteStoreCommands(EntityCommand entityCommand, CommandBehavior behavior)
   at System.Data.Objects.Internal.ObjectQueryExecutionPlan.Execute[TResultType](ObjectContext context, ObjectParameterCollection parameterValues)
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Linq.Enumerable.Single[TSource](IEnumerable`1 source)
   at PayrollDAL.PayrollRepository.CheckAndSendPayslip() in D:\GIT\BCG_DEV_NEW\PayrollDAL\PayrollRepository.vb:line 387
Inner error message: ORA-03135: connection lost contact
Process ID: 15516
Session ID: 935 Serial number: 1953
Inner stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck, Int32 isRecoverable, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, String procedure, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, Boolean bCheck, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleCommand.ExecuteReader(Boolean requery, Boolean fillRequest, CommandBehavior behavior)
   at Oracle.DataAccess.Client.OracleCommand.ExecuteDbDataReader(CommandBehavior behavior)
   at System.Data.EntityClient.EntityCommandDefinition.ExecuteStoreCommands(EntityCommand entityCommand, CommandBehavior behavior)
Other Text: iPayroll

Function: CheckAndSendPayslip
Datetime: 08/29/2022 12:06:53 PM
Error message: The connection is not open.
Stack trace:    at System.Data.EntityClient.EntityConnection.Open()
   at System.Data.Objects.ObjectContext.EnsureConnection()
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Linq.Enumerable.Single[TSource](IEnumerable`1 source)
   at PayrollDAL.PayrollRepository.CheckAndSendPayslip() in D:\GIT\BCG_DEV_NEW\PayrollDAL\PayrollRepository.vb:line 387
Other Text: iPayroll

Function: CheckAndSendPayslip
Datetime: 08/29/2022 12:07:49 PM
Error message: The underlying provider failed on Open.
Stack trace:    at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
   at System.Data.EntityClient.EntityConnection.Open()
   at System.Data.Objects.ObjectContext.EnsureConnection()
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Linq.Enumerable.Single[TSource](IEnumerable`1 source)
   at PayrollDAL.PayrollRepository.CheckAndSendPayslip() in D:\GIT\BCG_DEV_NEW\PayrollDAL\PayrollRepository.vb:line 387
Inner error message: Connection request timed out
Inner stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck, Int32 isRecoverable, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, Object src, OracleLogicalTransaction m_oracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleConnectionOCP.Open(OracleConnection con)
   at Oracle.DataAccess.Client.OracleConnection.Open()
   at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
Other Text: iPayroll

Function: CheckAndSendPayslip
Datetime: 08/29/2022 12:08:49 PM
Error message: The underlying provider failed on Open.
Stack trace:    at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
   at System.Data.EntityClient.EntityConnection.Open()
   at System.Data.Objects.ObjectContext.EnsureConnection()
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Linq.Enumerable.Single[TSource](IEnumerable`1 source)
   at PayrollDAL.PayrollRepository.CheckAndSendPayslip() in D:\GIT\BCG_DEV_NEW\PayrollDAL\PayrollRepository.vb:line 387
Inner error message: Connection request timed out
Inner stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck, Int32 isRecoverable, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, Object src, OracleLogicalTransaction m_oracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleConnectionOCP.Open(OracleConnection con)
   at Oracle.DataAccess.Client.OracleConnection.Open()
   at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
Other Text: iPayroll

Function: CheckAndSendPayslip
Datetime: 08/29/2022 12:09:49 PM
Error message: The underlying provider failed on Open.
Stack trace:    at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
   at System.Data.EntityClient.EntityConnection.Open()
   at System.Data.Objects.ObjectContext.EnsureConnection()
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Linq.Enumerable.Single[TSource](IEnumerable`1 source)
   at PayrollDAL.PayrollRepository.CheckAndSendPayslip() in D:\GIT\BCG_DEV_NEW\PayrollDAL\PayrollRepository.vb:line 387
Inner error message: Connection request timed out
Inner stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck, Int32 isRecoverable, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, Object src, OracleLogicalTransaction m_oracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleConnectionOCP.Open(OracleConnection con)
   at Oracle.DataAccess.Client.OracleConnection.Open()
   at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
Other Text: iPayroll

Function: CheckAndSendPayslip
Datetime: 08/29/2022 12:10:49 PM
Error message: The underlying provider failed on Open.
Stack trace:    at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
   at System.Data.EntityClient.EntityConnection.Open()
   at System.Data.Objects.ObjectContext.EnsureConnection()
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Linq.Enumerable.Single[TSource](IEnumerable`1 source)
   at PayrollDAL.PayrollRepository.CheckAndSendPayslip() in D:\GIT\BCG_DEV_NEW\PayrollDAL\PayrollRepository.vb:line 387
Inner error message: Connection request timed out
Inner stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck, Int32 isRecoverable, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, Object src, OracleLogicalTransaction m_oracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleConnectionOCP.Open(OracleConnection con)
   at Oracle.DataAccess.Client.OracleConnection.Open()
   at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
Other Text: iPayroll

Function: CheckAndSendPayslip
Datetime: 08/29/2022 12:11:49 PM
Error message: The underlying provider failed on Open.
Stack trace:    at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
   at System.Data.EntityClient.EntityConnection.Open()
   at System.Data.Objects.ObjectContext.EnsureConnection()
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Linq.Enumerable.Single[TSource](IEnumerable`1 source)
   at PayrollDAL.PayrollRepository.CheckAndSendPayslip() in D:\GIT\BCG_DEV_NEW\PayrollDAL\PayrollRepository.vb:line 387
Inner error message: Connection request timed out
Inner stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck, Int32 isRecoverable, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, Object src, OracleLogicalTransaction m_oracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleConnectionOCP.Open(OracleConnection con)
   at Oracle.DataAccess.Client.OracleConnection.Open()
   at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
Other Text: iPayroll

Function: CheckAndSendPayslip
Datetime: 08/29/2022 12:12:38 PM
Error message: The underlying provider failed on Open.
Stack trace:    at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
   at System.Data.EntityClient.EntityConnection.Open()
   at System.Data.Objects.ObjectContext.EnsureConnection()
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Linq.Enumerable.Single[TSource](IEnumerable`1 source)
   at PayrollDAL.PayrollRepository.CheckAndSendPayslip() in D:\GIT\BCG_DEV_NEW\PayrollDAL\PayrollRepository.vb:line 387
Inner error message: ORA-12170: TNS:Connect timeout occurred
Inner stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck, Int32 isRecoverable, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, Object src, OracleLogicalTransaction m_oracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleConnectionOCP.Open(OracleConnection con)
   at Oracle.DataAccess.Client.OracleConnection.Open()
   at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
Other Text: iPayroll

Function: CheckAndSendPayslip
Datetime: 08/29/2022 12:13:49 PM
Error message: The underlying provider failed on Open.
Stack trace:    at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
   at System.Data.EntityClient.EntityConnection.Open()
   at System.Data.Objects.ObjectContext.EnsureConnection()
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Linq.Enumerable.Single[TSource](IEnumerable`1 source)
   at PayrollDAL.PayrollRepository.CheckAndSendPayslip() in D:\GIT\BCG_DEV_NEW\PayrollDAL\PayrollRepository.vb:line 387
Inner error message: Connection request timed out
Inner stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck, Int32 isRecoverable, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, Object src, OracleLogicalTransaction m_oracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleConnectionOCP.Open(OracleConnection con)
   at Oracle.DataAccess.Client.OracleConnection.Open()
   at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
Other Text: iPayroll

Function: CheckAndSendPayslip
Datetime: 08/29/2022 12:14:49 PM
Error message: The underlying provider failed on Open.
Stack trace:    at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
   at System.Data.EntityClient.EntityConnection.Open()
   at System.Data.Objects.ObjectContext.EnsureConnection()
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Linq.Enumerable.Single[TSource](IEnumerable`1 source)
   at PayrollDAL.PayrollRepository.CheckAndSendPayslip() in D:\GIT\BCG_DEV_NEW\PayrollDAL\PayrollRepository.vb:line 387
Inner error message: Connection request timed out
Inner stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck, Int32 isRecoverable, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, Object src, OracleLogicalTransaction m_oracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleConnectionOCP.Open(OracleConnection con)
   at Oracle.DataAccess.Client.OracleConnection.Open()
   at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
Other Text: iPayroll

Function: CheckAndSendPayslip
Datetime: 08/29/2022 12:15:49 PM
Error message: The underlying provider failed on Open.
Stack trace:    at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
   at System.Data.EntityClient.EntityConnection.Open()
   at System.Data.Objects.ObjectContext.EnsureConnection()
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Linq.Enumerable.Single[TSource](IEnumerable`1 source)
   at PayrollDAL.PayrollRepository.CheckAndSendPayslip() in D:\GIT\BCG_DEV_NEW\PayrollDAL\PayrollRepository.vb:line 387
Inner error message: Connection request timed out
Inner stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck, Int32 isRecoverable, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, Object src, OracleLogicalTransaction m_oracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleConnectionOCP.Open(OracleConnection con)
   at Oracle.DataAccess.Client.OracleConnection.Open()
   at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
Other Text: iPayroll

Function: CheckAndSendPayslip
Datetime: 08/29/2022 12:16:49 PM
Error message: The underlying provider failed on Open.
Stack trace:    at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
   at System.Data.EntityClient.EntityConnection.Open()
   at System.Data.Objects.ObjectContext.EnsureConnection()
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Linq.Enumerable.Single[TSource](IEnumerable`1 source)
   at PayrollDAL.PayrollRepository.CheckAndSendPayslip() in D:\GIT\BCG_DEV_NEW\PayrollDAL\PayrollRepository.vb:line 387
Inner error message: Connection request timed out
Inner stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck, Int32 isRecoverable, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, Object src, OracleLogicalTransaction m_oracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleConnectionOCP.Open(OracleConnection con)
   at Oracle.DataAccess.Client.OracleConnection.Open()
   at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
Other Text: iPayroll

Function: CheckAndSendPayslip
Datetime: 08/29/2022 12:17:49 PM
Error message: The underlying provider failed on Open.
Stack trace:    at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
   at System.Data.EntityClient.EntityConnection.Open()
   at System.Data.Objects.ObjectContext.EnsureConnection()
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Linq.Enumerable.Single[TSource](IEnumerable`1 source)
   at PayrollDAL.PayrollRepository.CheckAndSendPayslip() in D:\GIT\BCG_DEV_NEW\PayrollDAL\PayrollRepository.vb:line 387
Inner error message: Connection request timed out
Inner stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck, Int32 isRecoverable, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, Object src, OracleLogicalTransaction m_oracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleConnectionOCP.Open(OracleConnection con)
   at Oracle.DataAccess.Client.OracleConnection.Open()
   at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
Other Text: iPayroll

Function: CheckAndSendPayslip
Datetime: 08/29/2022 12:19:53 PM
Error message: An error occurred while executing the command definition. See the inner exception for details.
Stack trace:    at System.Data.EntityClient.EntityCommandDefinition.ExecuteStoreCommands(EntityCommand entityCommand, CommandBehavior behavior)
   at System.Data.Objects.Internal.ObjectQueryExecutionPlan.Execute[TResultType](ObjectContext context, ObjectParameterCollection parameterValues)
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Linq.Enumerable.Single[TSource](IEnumerable`1 source)
   at PayrollDAL.PayrollRepository.CheckAndSendPayslip() in D:\GIT\BCG_DEV_NEW\PayrollDAL\PayrollRepository.vb:line 387
Inner error message: ORA-03135: connection lost contact
Process ID: 10700
Session ID: 191 Serial number: 20828
Inner stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck, Int32 isRecoverable, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, String procedure, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, Boolean bCheck, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleCommand.ExecuteReader(Boolean requery, Boolean fillRequest, CommandBehavior behavior)
   at Oracle.DataAccess.Client.OracleCommand.ExecuteDbDataReader(CommandBehavior behavior)
   at System.Data.EntityClient.EntityCommandDefinition.ExecuteStoreCommands(EntityCommand entityCommand, CommandBehavior behavior)
Other Text: iPayroll

Function: CheckAndSendPayslip
Datetime: 08/29/2022 12:20:53 PM
Error message: The connection is not open.
Stack trace:    at System.Data.EntityClient.EntityConnection.Open()
   at System.Data.Objects.ObjectContext.EnsureConnection()
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Linq.Enumerable.Single[TSource](IEnumerable`1 source)
   at PayrollDAL.PayrollRepository.CheckAndSendPayslip() in D:\GIT\BCG_DEV_NEW\PayrollDAL\PayrollRepository.vb:line 387
Other Text: iPayroll

Function: CheckAndSendPayslip
Datetime: 08/29/2022 12:21:49 PM
Error message: The underlying provider failed on Open.
Stack trace:    at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
   at System.Data.EntityClient.EntityConnection.Open()
   at System.Data.Objects.ObjectContext.EnsureConnection()
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Linq.Enumerable.Single[TSource](IEnumerable`1 source)
   at PayrollDAL.PayrollRepository.CheckAndSendPayslip() in D:\GIT\BCG_DEV_NEW\PayrollDAL\PayrollRepository.vb:line 387
Inner error message: Connection request timed out
Inner stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck, Int32 isRecoverable, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, Object src, OracleLogicalTransaction m_oracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleConnectionOCP.Open(OracleConnection con)
   at Oracle.DataAccess.Client.OracleConnection.Open()
   at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
Other Text: iPayroll

Function: CheckAndSendPayslip
Datetime: 08/29/2022 12:22:49 PM
Error message: The underlying provider failed on Open.
Stack trace:    at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
   at System.Data.EntityClient.EntityConnection.Open()
   at System.Data.Objects.ObjectContext.EnsureConnection()
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Linq.Enumerable.Single[TSource](IEnumerable`1 source)
   at PayrollDAL.PayrollRepository.CheckAndSendPayslip() in D:\GIT\BCG_DEV_NEW\PayrollDAL\PayrollRepository.vb:line 387
Inner error message: Connection request timed out
Inner stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck, Int32 isRecoverable, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, Object src, OracleLogicalTransaction m_oracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleConnectionOCP.Open(OracleConnection con)
   at Oracle.DataAccess.Client.OracleConnection.Open()
   at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
Other Text: iPayroll

Function: CheckAndSendPayslip
Datetime: 08/29/2022 12:23:38 PM
Error message: The underlying provider failed on Open.
Stack trace:    at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
   at System.Data.EntityClient.EntityConnection.Open()
   at System.Data.Objects.ObjectContext.EnsureConnection()
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Linq.Enumerable.Single[TSource](IEnumerable`1 source)
   at PayrollDAL.PayrollRepository.CheckAndSendPayslip() in D:\GIT\BCG_DEV_NEW\PayrollDAL\PayrollRepository.vb:line 387
Inner error message: ORA-12170: TNS:Connect timeout occurred
Inner stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck, Int32 isRecoverable, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, Object src, OracleLogicalTransaction m_oracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleConnectionOCP.Open(OracleConnection con)
   at Oracle.DataAccess.Client.OracleConnection.Open()
   at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
Other Text: iPayroll

Function: CheckAndSendPayslip
Datetime: 08/29/2022 12:24:49 PM
Error message: The underlying provider failed on Open.
Stack trace:    at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
   at System.Data.EntityClient.EntityConnection.Open()
   at System.Data.Objects.ObjectContext.EnsureConnection()
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Linq.Enumerable.Single[TSource](IEnumerable`1 source)
   at PayrollDAL.PayrollRepository.CheckAndSendPayslip() in D:\GIT\BCG_DEV_NEW\PayrollDAL\PayrollRepository.vb:line 387
Inner error message: Connection request timed out
Inner stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck, Int32 isRecoverable, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, Object src, OracleLogicalTransaction m_oracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleConnectionOCP.Open(OracleConnection con)
   at Oracle.DataAccess.Client.OracleConnection.Open()
   at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
Other Text: iPayroll

Function: CheckAndSendPayslip
Datetime: 08/29/2022 12:25:38 PM
Error message: The underlying provider failed on Open.
Stack trace:    at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
   at System.Data.EntityClient.EntityConnection.Open()
   at System.Data.Objects.ObjectContext.EnsureConnection()
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Linq.Enumerable.Single[TSource](IEnumerable`1 source)
   at PayrollDAL.PayrollRepository.CheckAndSendPayslip() in D:\GIT\BCG_DEV_NEW\PayrollDAL\PayrollRepository.vb:line 387
Inner error message: ORA-12170: TNS:Connect timeout occurred
Inner stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck, Int32 isRecoverable, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, Object src, OracleLogicalTransaction m_oracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleConnectionOCP.Open(OracleConnection con)
   at Oracle.DataAccess.Client.OracleConnection.Open()
   at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
Other Text: iPayroll

Function: CheckAndSendPayslip
Datetime: 08/29/2022 12:26:49 PM
Error message: The underlying provider failed on Open.
Stack trace:    at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
   at System.Data.EntityClient.EntityConnection.Open()
   at System.Data.Objects.ObjectContext.EnsureConnection()
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Linq.Enumerable.Single[TSource](IEnumerable`1 source)
   at PayrollDAL.PayrollRepository.CheckAndSendPayslip() in D:\GIT\BCG_DEV_NEW\PayrollDAL\PayrollRepository.vb:line 387
Inner error message: Connection request timed out
Inner stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck, Int32 isRecoverable, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, Object src, OracleLogicalTransaction m_oracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleConnectionOCP.Open(OracleConnection con)
   at Oracle.DataAccess.Client.OracleConnection.Open()
   at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
Other Text: iPayroll

Function: CheckAndSendPayslip
Datetime: 08/29/2022 12:27:38 PM
Error message: The underlying provider failed on Open.
Stack trace:    at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
   at System.Data.EntityClient.EntityConnection.Open()
   at System.Data.Objects.ObjectContext.EnsureConnection()
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Linq.Enumerable.Single[TSource](IEnumerable`1 source)
   at PayrollDAL.PayrollRepository.CheckAndSendPayslip() in D:\GIT\BCG_DEV_NEW\PayrollDAL\PayrollRepository.vb:line 387
Inner error message: ORA-12170: TNS:Connect timeout occurred
Inner stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck, Int32 isRecoverable, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, Object src, OracleLogicalTransaction m_oracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleConnectionOCP.Open(OracleConnection con)
   at Oracle.DataAccess.Client.OracleConnection.Open()
   at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
Other Text: iPayroll

Function: CheckAndSendPayslip
Datetime: 08/29/2022 12:28:49 PM
Error message: The underlying provider failed on Open.
Stack trace:    at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
   at System.Data.EntityClient.EntityConnection.Open()
   at System.Data.Objects.ObjectContext.EnsureConnection()
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Linq.Enumerable.Single[TSource](IEnumerable`1 source)
   at PayrollDAL.PayrollRepository.CheckAndSendPayslip() in D:\GIT\BCG_DEV_NEW\PayrollDAL\PayrollRepository.vb:line 387
Inner error message: Connection request timed out
Inner stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck, Int32 isRecoverable, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, Object src, OracleLogicalTransaction m_oracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleConnectionOCP.Open(OracleConnection con)
   at Oracle.DataAccess.Client.OracleConnection.Open()
   at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
Other Text: iPayroll

Function: CheckAndSendPayslip
Datetime: 08/29/2022 12:29:49 PM
Error message: The underlying provider failed on Open.
Stack trace:    at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
   at System.Data.EntityClient.EntityConnection.Open()
   at System.Data.Objects.ObjectContext.EnsureConnection()
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Linq.Enumerable.Single[TSource](IEnumerable`1 source)
   at PayrollDAL.PayrollRepository.CheckAndSendPayslip() in D:\GIT\BCG_DEV_NEW\PayrollDAL\PayrollRepository.vb:line 387
Inner error message: Connection request timed out
Inner stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck, Int32 isRecoverable, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, Object src, OracleLogicalTransaction m_oracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleConnectionOCP.Open(OracleConnection con)
   at Oracle.DataAccess.Client.OracleConnection.Open()
   at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
Other Text: iPayroll

Function: CheckAndSendPayslip
Datetime: 08/29/2022 12:30:49 PM
Error message: The underlying provider failed on Open.
Stack trace:    at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
   at System.Data.EntityClient.EntityConnection.Open()
   at System.Data.Objects.ObjectContext.EnsureConnection()
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Linq.Enumerable.Single[TSource](IEnumerable`1 source)
   at PayrollDAL.PayrollRepository.CheckAndSendPayslip() in D:\GIT\BCG_DEV_NEW\PayrollDAL\PayrollRepository.vb:line 387
Inner error message: Connection request timed out
Inner stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck, Int32 isRecoverable, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, Object src, OracleLogicalTransaction m_oracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleConnectionOCP.Open(OracleConnection con)
   at Oracle.DataAccess.Client.OracleConnection.Open()
   at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
Other Text: iPayroll

Function: CheckAndSendPayslip
Datetime: 08/29/2022 12:31:38 PM
Error message: The underlying provider failed on Open.
Stack trace:    at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
   at System.Data.EntityClient.EntityConnection.Open()
   at System.Data.Objects.ObjectContext.EnsureConnection()
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Linq.Enumerable.Single[TSource](IEnumerable`1 source)
   at PayrollDAL.PayrollRepository.CheckAndSendPayslip() in D:\GIT\BCG_DEV_NEW\PayrollDAL\PayrollRepository.vb:line 387
Inner error message: ORA-12170: TNS:Connect timeout occurred
Inner stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck, Int32 isRecoverable, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, Object src, OracleLogicalTransaction m_oracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleConnectionOCP.Open(OracleConnection con)
   at Oracle.DataAccess.Client.OracleConnection.Open()
   at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
Other Text: iPayroll

Function: CheckAndSendPayslip
Datetime: 08/29/2022 12:32:49 PM
Error message: The underlying provider failed on Open.
Stack trace:    at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
   at System.Data.EntityClient.EntityConnection.Open()
   at System.Data.Objects.ObjectContext.EnsureConnection()
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Linq.Enumerable.Single[TSource](IEnumerable`1 source)
   at PayrollDAL.PayrollRepository.CheckAndSendPayslip() in D:\GIT\BCG_DEV_NEW\PayrollDAL\PayrollRepository.vb:line 387
Inner error message: Connection request timed out
Inner stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck, Int32 isRecoverable, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, Object src, OracleLogicalTransaction m_oracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleConnectionOCP.Open(OracleConnection con)
   at Oracle.DataAccess.Client.OracleConnection.Open()
   at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
Other Text: iPayroll

Function: CheckAndSendPayslip
Datetime: 08/29/2022 12:33:49 PM
Error message: The underlying provider failed on Open.
Stack trace:    at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
   at System.Data.EntityClient.EntityConnection.Open()
   at System.Data.Objects.ObjectContext.EnsureConnection()
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Linq.Enumerable.Single[TSource](IEnumerable`1 source)
   at PayrollDAL.PayrollRepository.CheckAndSendPayslip() in D:\GIT\BCG_DEV_NEW\PayrollDAL\PayrollRepository.vb:line 387
Inner error message: Connection request timed out
Inner stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck, Int32 isRecoverable, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, Object src, OracleLogicalTransaction m_oracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleConnectionOCP.Open(OracleConnection con)
   at Oracle.DataAccess.Client.OracleConnection.Open()
   at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
Other Text: iPayroll

Function: CheckAndSendPayslip
Datetime: 08/29/2022 12:34:49 PM
Error message: The underlying provider failed on Open.
Stack trace:    at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
   at System.Data.EntityClient.EntityConnection.Open()
   at System.Data.Objects.ObjectContext.EnsureConnection()
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Linq.Enumerable.Single[TSource](IEnumerable`1 source)
   at PayrollDAL.PayrollRepository.CheckAndSendPayslip() in D:\GIT\BCG_DEV_NEW\PayrollDAL\PayrollRepository.vb:line 387
Inner error message: Connection request timed out
Inner stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck, Int32 isRecoverable, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, Object src, OracleLogicalTransaction m_oracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleConnectionOCP.Open(OracleConnection con)
   at Oracle.DataAccess.Client.OracleConnection.Open()
   at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
Other Text: iPayroll

Function: CheckAndSendPayslip
Datetime: 08/29/2022 12:35:38 PM
Error message: The underlying provider failed on Open.
Stack trace:    at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
   at System.Data.EntityClient.EntityConnection.Open()
   at System.Data.Objects.ObjectContext.EnsureConnection()
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Linq.Enumerable.Single[TSource](IEnumerable`1 source)
   at PayrollDAL.PayrollRepository.CheckAndSendPayslip() in D:\GIT\BCG_DEV_NEW\PayrollDAL\PayrollRepository.vb:line 387
Inner error message: ORA-12170: TNS:Connect timeout occurred
Inner stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck, Int32 isRecoverable, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, Object src, OracleLogicalTransaction m_oracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleConnectionOCP.Open(OracleConnection con)
   at Oracle.DataAccess.Client.OracleConnection.Open()
   at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
Other Text: iPayroll

Function: CheckAndSendPayslip
Datetime: 08/29/2022 12:36:49 PM
Error message: The underlying provider failed on Open.
Stack trace:    at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
   at System.Data.EntityClient.EntityConnection.Open()
   at System.Data.Objects.ObjectContext.EnsureConnection()
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Linq.Enumerable.Single[TSource](IEnumerable`1 source)
   at PayrollDAL.PayrollRepository.CheckAndSendPayslip() in D:\GIT\BCG_DEV_NEW\PayrollDAL\PayrollRepository.vb:line 387
Inner error message: Connection request timed out
Inner stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck, Int32 isRecoverable, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, Object src, OracleLogicalTransaction m_oracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleConnectionOCP.Open(OracleConnection con)
   at Oracle.DataAccess.Client.OracleConnection.Open()
   at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
Other Text: iPayroll

Function: CheckAndSendPayslip
Datetime: 08/29/2022 12:37:38 PM
Error message: The underlying provider failed on Open.
Stack trace:    at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
   at System.Data.EntityClient.EntityConnection.Open()
   at System.Data.Objects.ObjectContext.EnsureConnection()
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Linq.Enumerable.Single[TSource](IEnumerable`1 source)
   at PayrollDAL.PayrollRepository.CheckAndSendPayslip() in D:\GIT\BCG_DEV_NEW\PayrollDAL\PayrollRepository.vb:line 387
Inner error message: ORA-12170: TNS:Connect timeout occurred
Inner stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck, Int32 isRecoverable, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, Object src, OracleLogicalTransaction m_oracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleConnectionOCP.Open(OracleConnection con)
   at Oracle.DataAccess.Client.OracleConnection.Open()
   at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
Other Text: iPayroll

Function: CheckAndSendPayslip
Datetime: 08/29/2022 12:38:49 PM
Error message: The underlying provider failed on Open.
Stack trace:    at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
   at System.Data.EntityClient.EntityConnection.Open()
   at System.Data.Objects.ObjectContext.EnsureConnection()
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Linq.Enumerable.Single[TSource](IEnumerable`1 source)
   at PayrollDAL.PayrollRepository.CheckAndSendPayslip() in D:\GIT\BCG_DEV_NEW\PayrollDAL\PayrollRepository.vb:line 387
Inner error message: Connection request timed out
Inner stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck, Int32 isRecoverable, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, Object src, OracleLogicalTransaction m_oracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleConnectionOCP.Open(OracleConnection con)
   at Oracle.DataAccess.Client.OracleConnection.Open()
   at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
Other Text: iPayroll

Function: CheckAndSendPayslip
Datetime: 08/29/2022 12:39:38 PM
Error message: The underlying provider failed on Open.
Stack trace:    at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
   at System.Data.EntityClient.EntityConnection.Open()
   at System.Data.Objects.ObjectContext.EnsureConnection()
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Linq.Enumerable.Single[TSource](IEnumerable`1 source)
   at PayrollDAL.PayrollRepository.CheckAndSendPayslip() in D:\GIT\BCG_DEV_NEW\PayrollDAL\PayrollRepository.vb:line 387
Inner error message: ORA-12170: TNS:Connect timeout occurred
Inner stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck, Int32 isRecoverable, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, Object src, OracleLogicalTransaction m_oracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleConnectionOCP.Open(OracleConnection con)
   at Oracle.DataAccess.Client.OracleConnection.Open()
   at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
Other Text: iPayroll

Function: CheckAndSendPayslip
Datetime: 08/29/2022 12:40:49 PM
Error message: The underlying provider failed on Open.
Stack trace:    at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
   at System.Data.EntityClient.EntityConnection.Open()
   at System.Data.Objects.ObjectContext.EnsureConnection()
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Linq.Enumerable.Single[TSource](IEnumerable`1 source)
   at PayrollDAL.PayrollRepository.CheckAndSendPayslip() in D:\GIT\BCG_DEV_NEW\PayrollDAL\PayrollRepository.vb:line 387
Inner error message: Connection request timed out
Inner stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck, Int32 isRecoverable, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, Object src, OracleLogicalTransaction m_oracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleConnectionOCP.Open(OracleConnection con)
   at Oracle.DataAccess.Client.OracleConnection.Open()
   at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
Other Text: iPayroll

Function: CheckAndSendPayslip
Datetime: 08/29/2022 12:41:49 PM
Error message: The underlying provider failed on Open.
Stack trace:    at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
   at System.Data.EntityClient.EntityConnection.Open()
   at System.Data.Objects.ObjectContext.EnsureConnection()
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Linq.Enumerable.Single[TSource](IEnumerable`1 source)
   at PayrollDAL.PayrollRepository.CheckAndSendPayslip() in D:\GIT\BCG_DEV_NEW\PayrollDAL\PayrollRepository.vb:line 387
Inner error message: Connection request timed out
Inner stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck, Int32 isRecoverable, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, Object src, OracleLogicalTransaction m_oracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleConnectionOCP.Open(OracleConnection con)
   at Oracle.DataAccess.Client.OracleConnection.Open()
   at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
Other Text: iPayroll

Function: CheckAndSendPayslip
Datetime: 08/29/2022 12:42:49 PM
Error message: The underlying provider failed on Open.
Stack trace:    at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
   at System.Data.EntityClient.EntityConnection.Open()
   at System.Data.Objects.ObjectContext.EnsureConnection()
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Linq.Enumerable.Single[TSource](IEnumerable`1 source)
   at PayrollDAL.PayrollRepository.CheckAndSendPayslip() in D:\GIT\BCG_DEV_NEW\PayrollDAL\PayrollRepository.vb:line 387
Inner error message: Connection request timed out
Inner stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck, Int32 isRecoverable, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, Object src, OracleLogicalTransaction m_oracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleConnectionOCP.Open(OracleConnection con)
   at Oracle.DataAccess.Client.OracleConnection.Open()
   at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
Other Text: iPayroll

Function: CheckAndSendPayslip
Datetime: 08/29/2022 12:43:49 PM
Error message: The underlying provider failed on Open.
Stack trace:    at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
   at System.Data.EntityClient.EntityConnection.Open()
   at System.Data.Objects.ObjectContext.EnsureConnection()
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Linq.Enumerable.Single[TSource](IEnumerable`1 source)
   at PayrollDAL.PayrollRepository.CheckAndSendPayslip() in D:\GIT\BCG_DEV_NEW\PayrollDAL\PayrollRepository.vb:line 387
Inner error message: Connection request timed out
Inner stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck, Int32 isRecoverable, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, Object src, OracleLogicalTransaction m_oracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleConnectionOCP.Open(OracleConnection con)
   at Oracle.DataAccess.Client.OracleConnection.Open()
   at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
Other Text: iPayroll

Function: CheckAndSendPayslip
Datetime: 08/29/2022 2:03:54 PM
Error message: An error occurred while executing the command definition. See the inner exception for details.
Stack trace:    at System.Data.EntityClient.EntityCommandDefinition.ExecuteStoreCommands(EntityCommand entityCommand, CommandBehavior behavior)
   at System.Data.Objects.Internal.ObjectQueryExecutionPlan.Execute[TResultType](ObjectContext context, ObjectParameterCollection parameterValues)
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Linq.Enumerable.Single[TSource](IEnumerable`1 source)
   at PayrollDAL.PayrollRepository.CheckAndSendPayslip() in D:\GIT\BCG_DEV_NEW\PayrollDAL\PayrollRepository.vb:line 387
Inner error message: ORA-03135: connection lost contact
Process ID: 14648
Session ID: 382 Serial number: 53591
Inner stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck, Int32 isRecoverable, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, String procedure, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, Boolean bCheck, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleCommand.ExecuteReader(Boolean requery, Boolean fillRequest, CommandBehavior behavior)
   at Oracle.DataAccess.Client.OracleCommand.ExecuteDbDataReader(CommandBehavior behavior)
   at System.Data.EntityClient.EntityCommandDefinition.ExecuteStoreCommands(EntityCommand entityCommand, CommandBehavior behavior)
Other Text: iPayroll

Function: CheckAndSendPayslip
Datetime: 08/29/2022 3:21:18 PM
Error message: An error occurred while executing the command definition. See the inner exception for details.
Stack trace:    at System.Data.EntityClient.EntityCommandDefinition.ExecuteStoreCommands(EntityCommand entityCommand, CommandBehavior behavior)
   at System.Data.Objects.Internal.ObjectQueryExecutionPlan.Execute[TResultType](ObjectContext context, ObjectParameterCollection parameterValues)
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Linq.Enumerable.Single[TSource](IEnumerable`1 source)
   at PayrollDAL.PayrollRepository.CheckAndSendPayslip() in D:\GIT\BCG_DEV_NEW\PayrollDAL\PayrollRepository.vb:line 387
Inner error message: ORA-01089: immediate shutdown or close in progress - no operations are permitted
Process ID: 5928
Session ID: 22 Serial number: 5074
Inner stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck, Int32 isRecoverable, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, String procedure, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, Boolean bCheck, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleCommand.ExecuteReader(Boolean requery, Boolean fillRequest, CommandBehavior behavior)
   at Oracle.DataAccess.Client.OracleCommand.ExecuteDbDataReader(CommandBehavior behavior)
   at System.Data.EntityClient.EntityCommandDefinition.ExecuteStoreCommands(EntityCommand entityCommand, CommandBehavior behavior)
Other Text: iPayroll

Function: CheckAndSendPayslip
Datetime: 08/29/2022 3:22:21 PM
Error message: The underlying provider failed on Open.
Stack trace:    at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
   at System.Data.EntityClient.EntityConnection.Open()
   at System.Data.Objects.ObjectContext.EnsureConnection()
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Linq.Enumerable.Single[TSource](IEnumerable`1 source)
   at PayrollDAL.PayrollRepository.CheckAndSendPayslip() in D:\GIT\BCG_DEV_NEW\PayrollDAL\PayrollRepository.vb:line 387
Inner error message: ORA-12514: TNS:listener does not currently know of service requested in connect descriptor
Inner stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck, Int32 isRecoverable, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, Object src, OracleLogicalTransaction m_oracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleConnectionOCP.Open(OracleConnection con)
   at Oracle.DataAccess.Client.OracleConnection.Open()
   at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
Other Text: iPayroll

Function: CheckAndSendPayslip
Datetime: 08/29/2022 3:23:21 PM
Error message: The underlying provider failed on Open.
Stack trace:    at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
   at System.Data.EntityClient.EntityConnection.Open()
   at System.Data.Objects.ObjectContext.EnsureConnection()
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Linq.Enumerable.Single[TSource](IEnumerable`1 source)
   at PayrollDAL.PayrollRepository.CheckAndSendPayslip() in D:\GIT\BCG_DEV_NEW\PayrollDAL\PayrollRepository.vb:line 387
Inner error message: ORA-12541: TNS:no listener
Inner stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck, Int32 isRecoverable, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, Object src, OracleLogicalTransaction m_oracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleConnectionOCP.Open(OracleConnection con)
   at Oracle.DataAccess.Client.OracleConnection.Open()
   at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
Other Text: iPayroll

Function: CheckAndSendPayslip
Datetime: 08/29/2022 4:17:38 PM
Error message: The connection is not open.
Stack trace:    at System.Data.EntityClient.EntityConnection.Open()
   at System.Data.Objects.ObjectContext.EnsureConnection()
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Linq.Enumerable.Single[TSource](IEnumerable`1 source)
   at PayrollDAL.PayrollRepository.CheckAndSendPayslip() in D:\GIT\BCG_DEV_NEW\PayrollDAL\PayrollRepository.vb:line 387
Other Text: iPayroll

Function: CheckAndSendPayslip
Datetime: 08/29/2022 4:18:38 PM
Error message: The connection is not open.
Stack trace:    at System.Data.EntityClient.EntityConnection.Open()
   at System.Data.Objects.ObjectContext.EnsureConnection()
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Linq.Enumerable.Single[TSource](IEnumerable`1 source)
   at PayrollDAL.PayrollRepository.CheckAndSendPayslip() in D:\GIT\BCG_DEV_NEW\PayrollDAL\PayrollRepository.vb:line 387
Other Text: iPayroll

Function: CheckAndSendPayslip
Datetime: 08/29/2022 4:19:34 PM
Error message: The underlying provider failed on Open.
Stack trace:    at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
   at System.Data.EntityClient.EntityConnection.Open()
   at System.Data.Objects.ObjectContext.EnsureConnection()
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Linq.Enumerable.Single[TSource](IEnumerable`1 source)
   at PayrollDAL.PayrollRepository.CheckAndSendPayslip() in D:\GIT\BCG_DEV_NEW\PayrollDAL\PayrollRepository.vb:line 387
Inner error message: Connection request timed out
Inner stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck, Int32 isRecoverable, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, Object src, OracleLogicalTransaction m_oracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleConnectionOCP.Open(OracleConnection con)
   at Oracle.DataAccess.Client.OracleConnection.Open()
   at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
Other Text: iPayroll

Function: CheckAndSendPayslip
Datetime: 08/29/2022 4:20:30 PM
Error message: The underlying provider failed on Open.
Stack trace:    at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
   at System.Data.EntityClient.EntityConnection.Open()
   at System.Data.Objects.ObjectContext.EnsureConnection()
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Linq.Enumerable.Single[TSource](IEnumerable`1 source)
   at PayrollDAL.PayrollRepository.CheckAndSendPayslip() in D:\GIT\BCG_DEV_NEW\PayrollDAL\PayrollRepository.vb:line 387
Inner error message: ORA-12170: TNS:Connect timeout occurred
Inner stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck, Int32 isRecoverable, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, Object src, OracleLogicalTransaction m_oracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleConnectionOCP.Open(OracleConnection con)
   at Oracle.DataAccess.Client.OracleConnection.Open()
   at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
Other Text: iPayroll

Function: CheckAndSendPayslip
Datetime: 08/29/2022 4:21:34 PM
Error message: The underlying provider failed on Open.
Stack trace:    at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
   at System.Data.EntityClient.EntityConnection.Open()
   at System.Data.Objects.ObjectContext.EnsureConnection()
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Linq.Enumerable.Single[TSource](IEnumerable`1 source)
   at PayrollDAL.PayrollRepository.CheckAndSendPayslip() in D:\GIT\BCG_DEV_NEW\PayrollDAL\PayrollRepository.vb:line 387
Inner error message: Connection request timed out
Inner stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck, Int32 isRecoverable, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, Object src, OracleLogicalTransaction m_oracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleConnectionOCP.Open(OracleConnection con)
   at Oracle.DataAccess.Client.OracleConnection.Open()
   at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
Other Text: iPayroll

Function: CheckAndSendPayslip
Datetime: 08/29/2022 4:22:34 PM
Error message: The underlying provider failed on Open.
Stack trace:    at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
   at System.Data.EntityClient.EntityConnection.Open()
   at System.Data.Objects.ObjectContext.EnsureConnection()
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Linq.Enumerable.Single[TSource](IEnumerable`1 source)
   at PayrollDAL.PayrollRepository.CheckAndSendPayslip() in D:\GIT\BCG_DEV_NEW\PayrollDAL\PayrollRepository.vb:line 387
Inner error message: Connection request timed out
Inner stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck, Int32 isRecoverable, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, Object src, OracleLogicalTransaction m_oracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleConnectionOCP.Open(OracleConnection con)
   at Oracle.DataAccess.Client.OracleConnection.Open()
   at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
Other Text: iPayroll

Function: CheckAndSendPayslip
Datetime: 08/29/2022 4:23:34 PM
Error message: The underlying provider failed on Open.
Stack trace:    at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
   at System.Data.EntityClient.EntityConnection.Open()
   at System.Data.Objects.ObjectContext.EnsureConnection()
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Linq.Enumerable.Single[TSource](IEnumerable`1 source)
   at PayrollDAL.PayrollRepository.CheckAndSendPayslip() in D:\GIT\BCG_DEV_NEW\PayrollDAL\PayrollRepository.vb:line 387
Inner error message: Connection request timed out
Inner stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck, Int32 isRecoverable, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, Object src, OracleLogicalTransaction m_oracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleConnectionOCP.Open(OracleConnection con)
   at Oracle.DataAccess.Client.OracleConnection.Open()
   at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
Other Text: iPayroll

Function: CheckAndSendPayslip
Datetime: 08/29/2022 4:24:23 PM
Error message: The underlying provider failed on Open.
Stack trace:    at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
   at System.Data.EntityClient.EntityConnection.Open()
   at System.Data.Objects.ObjectContext.EnsureConnection()
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Linq.Enumerable.Single[TSource](IEnumerable`1 source)
   at PayrollDAL.PayrollRepository.CheckAndSendPayslip() in D:\GIT\BCG_DEV_NEW\PayrollDAL\PayrollRepository.vb:line 387
Inner error message: ORA-12170: TNS:Connect timeout occurred
Inner stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck, Int32 isRecoverable, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, Object src, OracleLogicalTransaction m_oracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleConnectionOCP.Open(OracleConnection con)
   at Oracle.DataAccess.Client.OracleConnection.Open()
   at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
Other Text: iPayroll

Function: CheckAndSendPayslip
Datetime: 08/29/2022 4:25:34 PM
Error message: The underlying provider failed on Open.
Stack trace:    at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
   at System.Data.EntityClient.EntityConnection.Open()
   at System.Data.Objects.ObjectContext.EnsureConnection()
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Linq.Enumerable.Single[TSource](IEnumerable`1 source)
   at PayrollDAL.PayrollRepository.CheckAndSendPayslip() in D:\GIT\BCG_DEV_NEW\PayrollDAL\PayrollRepository.vb:line 387
Inner error message: Connection request timed out
Inner stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck, Int32 isRecoverable, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, Object src, OracleLogicalTransaction m_oracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleConnectionOCP.Open(OracleConnection con)
   at Oracle.DataAccess.Client.OracleConnection.Open()
   at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
Other Text: iPayroll

Function: CheckAndSendPayslip
Datetime: 08/29/2022 4:26:33 PM
Error message: The underlying provider failed on Open.
Stack trace:    at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
   at System.Data.EntityClient.EntityConnection.Open()
   at System.Data.Objects.ObjectContext.EnsureConnection()
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Linq.Enumerable.Single[TSource](IEnumerable`1 source)
   at PayrollDAL.PayrollRepository.CheckAndSendPayslip() in D:\GIT\BCG_DEV_NEW\PayrollDAL\PayrollRepository.vb:line 387
Inner error message: Connection request timed out
Inner stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck, Int32 isRecoverable, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, Object src, OracleLogicalTransaction m_oracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleConnectionOCP.Open(OracleConnection con)
   at Oracle.DataAccess.Client.OracleConnection.Open()
   at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
Other Text: iPayroll

Function: CheckAndSendPayslip
Datetime: 08/29/2022 4:27:34 PM
Error message: The underlying provider failed on Open.
Stack trace:    at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
   at System.Data.EntityClient.EntityConnection.Open()
   at System.Data.Objects.ObjectContext.EnsureConnection()
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Linq.Enumerable.Single[TSource](IEnumerable`1 source)
   at PayrollDAL.PayrollRepository.CheckAndSendPayslip() in D:\GIT\BCG_DEV_NEW\PayrollDAL\PayrollRepository.vb:line 387
Inner error message: Connection request timed out
Inner stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck, Int32 isRecoverable, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, Object src, OracleLogicalTransaction m_oracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleConnectionOCP.Open(OracleConnection con)
   at Oracle.DataAccess.Client.OracleConnection.Open()
   at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
Other Text: iPayroll

Function: CheckAndSendPayslip
Datetime: 08/29/2022 4:28:23 PM
Error message: The underlying provider failed on Open.
Stack trace:    at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
   at System.Data.EntityClient.EntityConnection.Open()
   at System.Data.Objects.ObjectContext.EnsureConnection()
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Linq.Enumerable.Single[TSource](IEnumerable`1 source)
   at PayrollDAL.PayrollRepository.CheckAndSendPayslip() in D:\GIT\BCG_DEV_NEW\PayrollDAL\PayrollRepository.vb:line 387
Inner error message: ORA-12170: TNS:Connect timeout occurred
Inner stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck, Int32 isRecoverable, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, Object src, OracleLogicalTransaction m_oracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleConnectionOCP.Open(OracleConnection con)
   at Oracle.DataAccess.Client.OracleConnection.Open()
   at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
Other Text: iPayroll

Function: CheckAndSendPayslip
Datetime: 08/29/2022 4:30:38 PM
Error message: The connection is not open.
Stack trace:    at System.Data.EntityClient.EntityConnection.Open()
   at System.Data.Objects.ObjectContext.EnsureConnection()
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Linq.Enumerable.Single[TSource](IEnumerable`1 source)
   at PayrollDAL.PayrollRepository.CheckAndSendPayslip() in D:\GIT\BCG_DEV_NEW\PayrollDAL\PayrollRepository.vb:line 387
Other Text: iPayroll

