Date,Time,TimeProcess,Class,Method,leve-message,exception,InnerException,StackTrace
29/08/2022,12:30:15,15,AttendanceRepository.Business.vb,ReadCheckInOutData,Error-iTime.ReadCheckInOutData || ex:System.Data.EntityException: The underlying provider failed on Open. ---> Oracle.DataAccess.Client.OracleException: Connection request timed out
   at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck, Int32 isRecoverable, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, Object src, OracleLogicalTransaction m_oracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleConnectionOCP.Open(OracleConnection con)
   at Oracle.DataAccess.Client.OracleConnection.Open()
   at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
   --- End of inner exception stack trace ---
   at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
   at System.Data.EntityClient.EntityConnection.Open()
   at System.Data.Objects.ObjectContext.EnsureConnection()
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Collections.Generic.List`1..ctor(IEnumerable`1 collection)
   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
   at AttendanceDAL.AttendanceRepository.ReadCheckInOutData(DateTime dateFrom, DateTime dateTo, Decimal TerId) in D:\GIT\BCG_DEV_NEW\AttendanceDAL\AttendanceRepository.Business.vb:line 47,,,
