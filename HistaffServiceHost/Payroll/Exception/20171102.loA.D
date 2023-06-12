Function: CheckAndSendBonusSlip
Datetime: 02/11/2017 3:47:13 PM
Error message: The underlying provider failed on Open.
Stack trace:    at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
   at System.Data.EntityClient.EntityConnection.Open()
   at System.Data.Objects.ObjectContext.EnsureConnection()
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Linq.Enumerable.Single[TSource](IEnumerable`1 source)
   at System.Data.Objects.ELinq.ObjectQueryProvider.<>c__11`1.<GetElementFunction>b__11_3(IEnumerable`1 sequence)
   at System.Data.Objects.ELinq.ObjectQueryProvider.ExecuteSingle[TResult](IEnumerable`1 query, Expression queryRoot)
   at System.Data.Objects.ELinq.ObjectQueryProvider.System.Linq.IQueryProvider.Execute[S](Expression expression)
   at System.Linq.Queryable.Any[TSource](IQueryable`1 source)
   at PayrollDAL.PayrollRepository.CheckAndSendBonusSlip() in E:\workspaces\KYODO\05.DEPLOYMENT\SP Core Level2\PayrollDAL\PayrollRepository.vb:line 399
Inner error message: Connection request timed out
Inner stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, Object src)
   at Oracle.DataAccess.Client.OracleConnection.Open()
   at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
Other Text: iPayroll

Function: CheckAndSendBonusSlip
Datetime: 02/11/2017 3:47:13 PM
Error message: The underlying provider failed on Open.
Stack trace:    at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
   at System.Data.EntityClient.EntityConnection.Open()
   at System.Data.Objects.ObjectContext.EnsureConnection()
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Linq.Enumerable.Single[TSource](IEnumerable`1 source)
   at System.Data.Objects.ELinq.ObjectQueryProvider.<>c__11`1.<GetElementFunction>b__11_3(IEnumerable`1 sequence)
   at System.Data.Objects.ELinq.ObjectQueryProvider.ExecuteSingle[TResult](IEnumerable`1 query, Expression queryRoot)
   at System.Data.Objects.ELinq.ObjectQueryProvider.System.Linq.IQueryProvider.Execute[S](Expression expression)
   at System.Linq.Queryable.Any[TSource](IQueryable`1 source)
   at PayrollDAL.PayrollRepository.CheckAndSendBonusSlip() in E:\workspaces\KYODO\05.DEPLOYMENT\SP Core Level2\PayrollDAL\PayrollRepository.vb:line 399
Inner error message: Connection request timed out
Inner stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, Object src)
   at Oracle.DataAccess.Client.OracleConnection.Open()
   at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
Other Text: iPayroll

