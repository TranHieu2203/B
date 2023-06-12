Function: CheckAndSendBonusSlip
Datetime: 30/10/2017 4:35:00 PM
Error message: Thread was being aborted.
Stack trace:    at System.Threading.Monitor.ReliableEnter(Object obj, Boolean& lockTaken)
   at System.Threading.Monitor.Enter(Object obj, Boolean& lockTaken)
   at System.Web.CachedPathData.WaitForInit()
   at System.Web.CachedPathData.GetConfigPathData(String configPath)
   at System.Web.CachedPathData.GetConfigPathData(String configPath)
   at System.Web.CachedPathData.GetApplicationPathData()
   at System.Web.Configuration.HttpConfigurationSystem.GetApplicationSection(String sectionName)
   at System.Web.Configuration.HttpConfigurationSystem.GetSection(String sectionName)
   at System.Web.Configuration.HttpConfigurationSystem.System.Configuration.Internal.IInternalConfigSystem.GetSection(String configKey)
   at System.Configuration.ConfigurationManager.GetSection(String sectionName)
   at System.Configuration.ConfigurationManager.get_ConnectionStrings()
   at System.Data.EntityClient.EntityConnection.ChangeConnectionString(String newConnectionString)
   at System.Data.EntityClient.EntityConnection..ctor(String connectionString)
   at System.Data.Objects.ObjectContext.CreateEntityConnection(String connectionString)
   at System.Data.Objects.ObjectContext..ctor(String connectionString, String defaultContainerName)
   at PayrollDAL.PayrollContext..ctor() in E:\workspaces\KYODO\05.DEPLOYMENT\SP Core Level2\PayrollDAL\PayrollContext.Designer.vb:line 55
   at PayrollDAL.PayrollRepositoryBase.get_Context() in E:\workspaces\KYODO\05.DEPLOYMENT\SP Core Level2\PayrollDAL\PayrollRepositoryBase.vb:line 12
   at PayrollDAL.PayrollRepository.CheckAndSendBonusSlip() in E:\workspaces\KYODO\05.DEPLOYMENT\SP Core Level2\PayrollDAL\PayrollRepository.vb:line 399
Other Text: iPayroll

Function: CheckAndSendBonusSlip
Datetime: 30/10/2017 4:36:50 PM
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
Inner error message: ORA-12514: TNS:listener does not currently know of service requested in connect descriptor
Inner stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, Object src)
   at Oracle.DataAccess.Client.OracleConnection.Open()
   at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
Other Text: iPayroll

Function: CheckAndSendPayslip
Datetime: 30/10/2017 4:36:50 PM
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
   at PayrollDAL.PayrollRepository.CheckAndSendPayslip() in E:\workspaces\KYODO\05.DEPLOYMENT\SP Core Level2\PayrollDAL\PayrollRepository.vb:line 334
Inner error message: ORA-12514: TNS:listener does not currently know of service requested in connect descriptor
Inner stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, Object src)
   at Oracle.DataAccess.Client.OracleConnection.Open()
   at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
Other Text: iPayroll

Function: CheckAndSendBonusSlip
Datetime: 30/10/2017 4:46:32 PM
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
Inner error message: ORA-12170: TNS:Connect timeout occurred
Inner stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, Object src)
   at Oracle.DataAccess.Client.OracleConnection.Open()
   at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
Other Text: iPayroll

