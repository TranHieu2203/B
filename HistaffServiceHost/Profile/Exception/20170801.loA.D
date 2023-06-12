Function: InsertBankBranch
Datetime: 01/08/2017 5:23:32 PM
Error message: An error occurred while updating the entries. See the inner exception for details.
Stack trace:    at System.Data.Mapping.Update.Internal.UpdateTranslator.Update(IEntityStateManager stateManager, IEntityAdapter adapter)
   at System.Data.EntityClient.EntityAdapter.Update(IEntityStateManager entityCache)
   at System.Data.Objects.ObjectContext.SaveChanges(SaveOptions options)
   at System.Data.Objects.ObjectContext.SaveChanges()
   at ProfileDAL.ProfileContext.SaveChanges(UserLog log) in D:\NhungDT\02.SVN\01.SERVER\05.DEPLOYMENT\01. Core Level1\01.SourceCode\ProfileDAL\ProfileContext.vb:line 56
   at ProfileDAL.ProfileRepository.InsertBankBranch(BankBranchDTO objBankBranch, UserLog log, Decimal& gID) in D:\NhungDT\02.SVN\01.SERVER\05.DEPLOYMENT\01. Core Level1\01.SourceCode\ProfileDAL\ProfileRepository.List.vb:line 2502
Inner error message: ORA-02291: integrity constraint (HISTAFF2017_DEV.FK_BANKBANK_BRANCH) violated - parent key not found
Inner stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, String procedure, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, Boolean bCheck)
   at Oracle.DataAccess.Client.OracleCommand.ExecuteNonQuery()
   at System.Data.Mapping.Update.Internal.DynamicUpdateCommand.Execute(UpdateTranslator translator, EntityConnection connection, Dictionary`2 identifierValues, List`1 generatedValues)
   at System.Data.Mapping.Update.Internal.UpdateTranslator.Update(IEntityStateManager stateManager, IEntityAdapter adapter)
Other Text: iProfile

