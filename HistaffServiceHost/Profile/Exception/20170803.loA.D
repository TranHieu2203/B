Function: InsertOrgTitle
Datetime: 03/08/2017 9:28:43 AM
Error message: An error occurred while updating the entries. See the inner exception for details.
Stack trace:    at System.Data.Mapping.Update.Internal.UpdateTranslator.Update(IEntityStateManager stateManager, IEntityAdapter adapter)
   at System.Data.EntityClient.EntityAdapter.Update(IEntityStateManager entityCache)
   at System.Data.Objects.ObjectContext.SaveChanges(SaveOptions options)
   at System.Data.Objects.ObjectContext.SaveChanges()
   at ProfileDAL.ProfileContext.SaveChanges(UserLog log) in D:\NhungDT\02.SVN\01.SERVER\05.DEPLOYMENT\01. Core Level1\01.SourceCode\ProfileDAL\ProfileContext.vb:line 56
   at ProfileDAL.ProfileRepository.InsertOrgTitle(List`1 lstOrgTitle, UserLog log, Decimal& gID, Boolean isSave) in D:\NhungDT\02.SVN\01.SERVER\05.DEPLOYMENT\01. Core Level1\01.SourceCode\ProfileDAL\ProfileRepository.List.vb:line 1273
Inner error message: ORA-02291: integrity constraint (HISTAFF2017_DEV.FK_HUTL_HUOT) violated - parent key not found
Inner stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, String procedure, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, Boolean bCheck)
   at Oracle.DataAccess.Client.OracleCommand.ExecuteNonQuery()
   at System.Data.Mapping.Update.Internal.DynamicUpdateCommand.Execute(UpdateTranslator translator, EntityConnection connection, Dictionary`2 identifierValues, List`1 generatedValues)
   at System.Data.Mapping.Update.Internal.UpdateTranslator.Update(IEntityStateManager stateManager, IEntityAdapter adapter)
Other Text: iProfile

Function: UpdateWorking
Datetime: 03/08/2017 3:55:23 PM
Error message: The underlying provider failed on Open.
Stack trace:    at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
   at System.Data.EntityClient.EntityConnection.Open()
   at System.Data.Objects.ObjectContext.EnsureConnection()
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Collections.Generic.List`1..ctor(IEnumerable`1 collection)
   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
   at ProfileDAL.ProfileRepository.UpdateWorking() in D:\NhungDT\02.SVN\01.SERVER\05.DEPLOYMENT\01. Core Level1\01.SourceCode\ProfileDAL\ProfileRepository.BackgroundProcess.vb:line 21
Inner error message: Connection request timed out
Inner stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, Object src)
   at Oracle.DataAccess.Client.OracleConnection.Open()
   at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
Other Text: iProfile

