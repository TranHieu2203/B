Function: GetOtherList
Datetime: 07/05/2022 9:40:07 AM
Error message: ORA-01017: invalid username/password; logon denied
Stack trace:    at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 303
   at ProfileDAL.ProfileRepository.GetOtherList(String sType, String sLang, Boolean isBlank) in D:\GIT\BCG_DEV_NEW\ProfileDAL\ProfileRepository.vb:line 55
Other Text: iProfile

Function: GetWorking
Datetime: 07/05/2022 9:42:37 AM
Error message: The cast to value type 'DateTime' failed because the materialized value is null. Either the result type's generic parameter or the query must use a nullable type.
Stack trace:    at System.Data.Common.Internal.Materialization.Shaper.ErrorHandlingValueReader`1.GetValue(DbDataReader reader, Int32 ordinal)
   at lambda_method(Closure , Shaper )
   at System.Data.Common.Internal.Materialization.Coordinator`1.ReadNextElement(Shaper shaper)
   at System.Data.Common.Internal.Materialization.Shaper`1.SimpleEnumerator.MoveNext()
   at System.Collections.Generic.List`1..ctor(IEnumerable`1 collection)
   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
   at ProfileDAL.ProfileRepository.GetWorking(WorkingDTO _filter, Int32 PageIndex, Int32 PageSize, Int32& Total, ParamDTO _param, String Sorts, UserLog log) in D:\GIT\BCG_DEV_NEW\ProfileDAL\ProfileRepository.Working.vb:line 1273
Other Text: iProfile

Function: ProfileDAL.ProfileRepository.GetPositionByOrgID
Datetime: 07/05/2022 10:13:09 AM
Error message: An error occurred while executing the command definition. See the inner exception for details.
Stack trace:    at System.Data.EntityClient.EntityCommandDefinition.ExecuteStoreCommands(EntityCommand entityCommand, CommandBehavior behavior)
   at System.Data.Objects.Internal.ObjectQueryExecutionPlan.Execute[TResultType](ObjectContext context, ObjectParameterCollection parameterValues)
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Collections.Generic.List`1..ctor(IEnumerable`1 collection)
   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
   at ProfileDAL.ProfileRepository.GetPositionByOrgID(TitleDTO _filter, Int32 PageIndex, Int32 PageSize, Int32& Total, String Sorts) in D:\GIT\BCG_DEV_NEW\ProfileDAL\ProfileRepository.List.vb:line 11378
Inner error message: ORA-12704: character set mismatch
Inner stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck, Int32 isRecoverable, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, String procedure, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, Boolean bCheck, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleCommand.ExecuteReader(Boolean requery, Boolean fillRequest, CommandBehavior behavior)
   at Oracle.DataAccess.Client.OracleCommand.ExecuteDbDataReader(CommandBehavior behavior)
   at System.Data.EntityClient.EntityCommandDefinition.ExecuteStoreCommands(EntityCommand entityCommand, CommandBehavior behavior)
Other Text: 

Function: ApproveContract_Background
Datetime: 07/05/2022 12:53:02 PM
Error message: Non-static method requires a target.
Stack trace:    at System.Reflection.RuntimeMethodInfo.CheckConsistency(Object target)
   at System.Reflection.RuntimeMethodInfo.InvokeArgumentsCheck(Object obj, BindingFlags invokeAttr, Binder binder, Object[] parameters, CultureInfo culture)
   at System.Reflection.RuntimeMethodInfo.Invoke(Object obj, BindingFlags invokeAttr, Binder binder, Object[] parameters, CultureInfo culture)
   at System.Reflection.RuntimePropertyInfo.GetValue(Object obj, Object[] index)
   at System.Data.Objects.ELinq.QueryParameterExpression.TryGetFieldOrPropertyValue(MemberExpression me, Object instance, Object& memberValue)
   at System.Data.Objects.ELinq.QueryParameterExpression.TryEvaluatePath(Expression expression, ConstantExpression& constantExpression)
   at System.Data.Objects.ELinq.QueryParameterExpression.EvaluateParameter(Object[] arguments)
   at System.Data.Objects.ELinq.ELinqQueryState.GetExecutionPlan(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Linq.Enumerable.FirstOrDefault[TSource](IEnumerable`1 source)
   at ProfileDAL.ProfileRepository.ApproveContract_Background(ContractDTO objContract) in D:\GIT\BCG_DEV_NEW\ProfileDAL\ProfileRepository.Contract.vb:line 2050
Other Text: iProfile

Function: ApproveContract_Background
Datetime: 07/05/2022 12:53:02 PM
Error message: Non-static method requires a target.
Stack trace:    at System.Reflection.RuntimeMethodInfo.CheckConsistency(Object target)
   at System.Reflection.RuntimeMethodInfo.InvokeArgumentsCheck(Object obj, BindingFlags invokeAttr, Binder binder, Object[] parameters, CultureInfo culture)
   at System.Reflection.RuntimeMethodInfo.Invoke(Object obj, BindingFlags invokeAttr, Binder binder, Object[] parameters, CultureInfo culture)
   at System.Reflection.RuntimePropertyInfo.GetValue(Object obj, Object[] index)
   at System.Data.Objects.ELinq.QueryParameterExpression.TryGetFieldOrPropertyValue(MemberExpression me, Object instance, Object& memberValue)
   at System.Data.Objects.ELinq.QueryParameterExpression.TryEvaluatePath(Expression expression, ConstantExpression& constantExpression)
   at System.Data.Objects.ELinq.QueryParameterExpression.EvaluateParameter(Object[] arguments)
   at System.Data.Objects.ELinq.ELinqQueryState.GetExecutionPlan(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Linq.Enumerable.FirstOrDefault[TSource](IEnumerable`1 source)
   at ProfileDAL.ProfileRepository.ApproveContract_Background(ContractDTO objContract) in D:\GIT\BCG_DEV_NEW\ProfileDAL\ProfileRepository.Contract.vb:line 2050
Other Text: iProfile

Function: ApproveContract_Background
Datetime: 07/05/2022 12:53:02 PM
Error message: Non-static method requires a target.
Stack trace:    at System.Reflection.RuntimeMethodInfo.CheckConsistency(Object target)
   at System.Reflection.RuntimeMethodInfo.InvokeArgumentsCheck(Object obj, BindingFlags invokeAttr, Binder binder, Object[] parameters, CultureInfo culture)
   at System.Reflection.RuntimeMethodInfo.Invoke(Object obj, BindingFlags invokeAttr, Binder binder, Object[] parameters, CultureInfo culture)
   at System.Reflection.RuntimePropertyInfo.GetValue(Object obj, Object[] index)
   at System.Data.Objects.ELinq.QueryParameterExpression.TryGetFieldOrPropertyValue(MemberExpression me, Object instance, Object& memberValue)
   at System.Data.Objects.ELinq.QueryParameterExpression.TryEvaluatePath(Expression expression, ConstantExpression& constantExpression)
   at System.Data.Objects.ELinq.QueryParameterExpression.EvaluateParameter(Object[] arguments)
   at System.Data.Objects.ELinq.ELinqQueryState.GetExecutionPlan(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Linq.Enumerable.FirstOrDefault[TSource](IEnumerable`1 source)
   at ProfileDAL.ProfileRepository.ApproveContract_Background(ContractDTO objContract) in D:\GIT\BCG_DEV_NEW\ProfileDAL\ProfileRepository.Contract.vb:line 2050
Other Text: iProfile

Function: ApproveContract_Background
Datetime: 07/05/2022 12:53:03 PM
Error message: Non-static method requires a target.
Stack trace:    at System.Reflection.RuntimeMethodInfo.CheckConsistency(Object target)
   at System.Reflection.RuntimeMethodInfo.InvokeArgumentsCheck(Object obj, BindingFlags invokeAttr, Binder binder, Object[] parameters, CultureInfo culture)
   at System.Reflection.RuntimeMethodInfo.Invoke(Object obj, BindingFlags invokeAttr, Binder binder, Object[] parameters, CultureInfo culture)
   at System.Reflection.RuntimePropertyInfo.GetValue(Object obj, Object[] index)
   at System.Data.Objects.ELinq.QueryParameterExpression.TryGetFieldOrPropertyValue(MemberExpression me, Object instance, Object& memberValue)
   at System.Data.Objects.ELinq.QueryParameterExpression.TryEvaluatePath(Expression expression, ConstantExpression& constantExpression)
   at System.Data.Objects.ELinq.QueryParameterExpression.EvaluateParameter(Object[] arguments)
   at System.Data.Objects.ELinq.ELinqQueryState.GetExecutionPlan(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Linq.Enumerable.FirstOrDefault[TSource](IEnumerable`1 source)
   at ProfileDAL.ProfileRepository.ApproveContract_Background(ContractDTO objContract) in D:\GIT\BCG_DEV_NEW\ProfileDAL\ProfileRepository.Contract.vb:line 2050
Other Text: iProfile

Function: ApproveContract_Background
Datetime: 07/05/2022 12:53:03 PM
Error message: Non-static method requires a target.
Stack trace:    at System.Reflection.RuntimeMethodInfo.CheckConsistency(Object target)
   at System.Reflection.RuntimeMethodInfo.InvokeArgumentsCheck(Object obj, BindingFlags invokeAttr, Binder binder, Object[] parameters, CultureInfo culture)
   at System.Reflection.RuntimeMethodInfo.Invoke(Object obj, BindingFlags invokeAttr, Binder binder, Object[] parameters, CultureInfo culture)
   at System.Reflection.RuntimePropertyInfo.GetValue(Object obj, Object[] index)
   at System.Data.Objects.ELinq.QueryParameterExpression.TryGetFieldOrPropertyValue(MemberExpression me, Object instance, Object& memberValue)
   at System.Data.Objects.ELinq.QueryParameterExpression.TryEvaluatePath(Expression expression, ConstantExpression& constantExpression)
   at System.Data.Objects.ELinq.QueryParameterExpression.EvaluateParameter(Object[] arguments)
   at System.Data.Objects.ELinq.ELinqQueryState.GetExecutionPlan(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Linq.Enumerable.FirstOrDefault[TSource](IEnumerable`1 source)
   at ProfileDAL.ProfileRepository.ApproveContract_Background(ContractDTO objContract) in D:\GIT\BCG_DEV_NEW\ProfileDAL\ProfileRepository.Contract.vb:line 2050
Other Text: iProfile

Function: ApproveContract_Background
Datetime: 07/05/2022 12:53:03 PM
Error message: Non-static method requires a target.
Stack trace:    at System.Reflection.RuntimeMethodInfo.CheckConsistency(Object target)
   at System.Reflection.RuntimeMethodInfo.InvokeArgumentsCheck(Object obj, BindingFlags invokeAttr, Binder binder, Object[] parameters, CultureInfo culture)
   at System.Reflection.RuntimeMethodInfo.Invoke(Object obj, BindingFlags invokeAttr, Binder binder, Object[] parameters, CultureInfo culture)
   at System.Reflection.RuntimePropertyInfo.GetValue(Object obj, Object[] index)
   at System.Data.Objects.ELinq.QueryParameterExpression.TryGetFieldOrPropertyValue(MemberExpression me, Object instance, Object& memberValue)
   at System.Data.Objects.ELinq.QueryParameterExpression.TryEvaluatePath(Expression expression, ConstantExpression& constantExpression)
   at System.Data.Objects.ELinq.QueryParameterExpression.EvaluateParameter(Object[] arguments)
   at System.Data.Objects.ELinq.ELinqQueryState.GetExecutionPlan(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Linq.Enumerable.FirstOrDefault[TSource](IEnumerable`1 source)
   at ProfileDAL.ProfileRepository.ApproveContract_Background(ContractDTO objContract) in D:\GIT\BCG_DEV_NEW\ProfileDAL\ProfileRepository.Contract.vb:line 2050
Other Text: iProfile

Function: ApproveContract_Background
Datetime: 07/05/2022 2:37:05 PM
Error message: Non-static method requires a target.
Stack trace:    at System.Reflection.RuntimeMethodInfo.CheckConsistency(Object target)
   at System.Reflection.RuntimeMethodInfo.InvokeArgumentsCheck(Object obj, BindingFlags invokeAttr, Binder binder, Object[] parameters, CultureInfo culture)
   at System.Reflection.RuntimeMethodInfo.Invoke(Object obj, BindingFlags invokeAttr, Binder binder, Object[] parameters, CultureInfo culture)
   at System.Reflection.RuntimePropertyInfo.GetValue(Object obj, Object[] index)
   at System.Data.Objects.ELinq.QueryParameterExpression.TryGetFieldOrPropertyValue(MemberExpression me, Object instance, Object& memberValue)
   at System.Data.Objects.ELinq.QueryParameterExpression.TryEvaluatePath(Expression expression, ConstantExpression& constantExpression)
   at System.Data.Objects.ELinq.QueryParameterExpression.EvaluateParameter(Object[] arguments)
   at System.Data.Objects.ELinq.ELinqQueryState.GetExecutionPlan(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Linq.Enumerable.FirstOrDefault[TSource](IEnumerable`1 source)
   at ProfileDAL.ProfileRepository.ApproveContract_Background(ContractDTO objContract) in D:\GIT\BCG_DEV_NEW\ProfileDAL\ProfileRepository.Contract.vb:line 2050
Other Text: iProfile

Function: ApproveContract_Background
Datetime: 07/05/2022 2:37:05 PM
Error message: Non-static method requires a target.
Stack trace:    at System.Reflection.RuntimeMethodInfo.CheckConsistency(Object target)
   at System.Reflection.RuntimeMethodInfo.InvokeArgumentsCheck(Object obj, BindingFlags invokeAttr, Binder binder, Object[] parameters, CultureInfo culture)
   at System.Reflection.RuntimeMethodInfo.Invoke(Object obj, BindingFlags invokeAttr, Binder binder, Object[] parameters, CultureInfo culture)
   at System.Reflection.RuntimePropertyInfo.GetValue(Object obj, Object[] index)
   at System.Data.Objects.ELinq.QueryParameterExpression.TryGetFieldOrPropertyValue(MemberExpression me, Object instance, Object& memberValue)
   at System.Data.Objects.ELinq.QueryParameterExpression.TryEvaluatePath(Expression expression, ConstantExpression& constantExpression)
   at System.Data.Objects.ELinq.QueryParameterExpression.EvaluateParameter(Object[] arguments)
   at System.Data.Objects.ELinq.ELinqQueryState.GetExecutionPlan(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Linq.Enumerable.FirstOrDefault[TSource](IEnumerable`1 source)
   at ProfileDAL.ProfileRepository.ApproveContract_Background(ContractDTO objContract) in D:\GIT\BCG_DEV_NEW\ProfileDAL\ProfileRepository.Contract.vb:line 2050
Other Text: iProfile

Function: ApproveContract_Background
Datetime: 07/05/2022 2:37:05 PM
Error message: Non-static method requires a target.
Stack trace:    at System.Reflection.RuntimeMethodInfo.CheckConsistency(Object target)
   at System.Reflection.RuntimeMethodInfo.InvokeArgumentsCheck(Object obj, BindingFlags invokeAttr, Binder binder, Object[] parameters, CultureInfo culture)
   at System.Reflection.RuntimeMethodInfo.Invoke(Object obj, BindingFlags invokeAttr, Binder binder, Object[] parameters, CultureInfo culture)
   at System.Reflection.RuntimePropertyInfo.GetValue(Object obj, Object[] index)
   at System.Data.Objects.ELinq.QueryParameterExpression.TryGetFieldOrPropertyValue(MemberExpression me, Object instance, Object& memberValue)
   at System.Data.Objects.ELinq.QueryParameterExpression.TryEvaluatePath(Expression expression, ConstantExpression& constantExpression)
   at System.Data.Objects.ELinq.QueryParameterExpression.EvaluateParameter(Object[] arguments)
   at System.Data.Objects.ELinq.ELinqQueryState.GetExecutionPlan(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Linq.Enumerable.FirstOrDefault[TSource](IEnumerable`1 source)
   at ProfileDAL.ProfileRepository.ApproveContract_Background(ContractDTO objContract) in D:\GIT\BCG_DEV_NEW\ProfileDAL\ProfileRepository.Contract.vb:line 2050
Other Text: iProfile

Function: ApproveContract_Background
Datetime: 07/05/2022 2:37:05 PM
Error message: Non-static method requires a target.
Stack trace:    at System.Reflection.RuntimeMethodInfo.CheckConsistency(Object target)
   at System.Reflection.RuntimeMethodInfo.InvokeArgumentsCheck(Object obj, BindingFlags invokeAttr, Binder binder, Object[] parameters, CultureInfo culture)
   at System.Reflection.RuntimeMethodInfo.Invoke(Object obj, BindingFlags invokeAttr, Binder binder, Object[] parameters, CultureInfo culture)
   at System.Reflection.RuntimePropertyInfo.GetValue(Object obj, Object[] index)
   at System.Data.Objects.ELinq.QueryParameterExpression.TryGetFieldOrPropertyValue(MemberExpression me, Object instance, Object& memberValue)
   at System.Data.Objects.ELinq.QueryParameterExpression.TryEvaluatePath(Expression expression, ConstantExpression& constantExpression)
   at System.Data.Objects.ELinq.QueryParameterExpression.EvaluateParameter(Object[] arguments)
   at System.Data.Objects.ELinq.ELinqQueryState.GetExecutionPlan(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Linq.Enumerable.FirstOrDefault[TSource](IEnumerable`1 source)
   at ProfileDAL.ProfileRepository.ApproveContract_Background(ContractDTO objContract) in D:\GIT\BCG_DEV_NEW\ProfileDAL\ProfileRepository.Contract.vb:line 2050
Other Text: iProfile

Function: ApproveContract_Background
Datetime: 07/05/2022 2:37:05 PM
Error message: Non-static method requires a target.
Stack trace:    at System.Reflection.RuntimeMethodInfo.CheckConsistency(Object target)
   at System.Reflection.RuntimeMethodInfo.InvokeArgumentsCheck(Object obj, BindingFlags invokeAttr, Binder binder, Object[] parameters, CultureInfo culture)
   at System.Reflection.RuntimeMethodInfo.Invoke(Object obj, BindingFlags invokeAttr, Binder binder, Object[] parameters, CultureInfo culture)
   at System.Reflection.RuntimePropertyInfo.GetValue(Object obj, Object[] index)
   at System.Data.Objects.ELinq.QueryParameterExpression.TryGetFieldOrPropertyValue(MemberExpression me, Object instance, Object& memberValue)
   at System.Data.Objects.ELinq.QueryParameterExpression.TryEvaluatePath(Expression expression, ConstantExpression& constantExpression)
   at System.Data.Objects.ELinq.QueryParameterExpression.EvaluateParameter(Object[] arguments)
   at System.Data.Objects.ELinq.ELinqQueryState.GetExecutionPlan(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Linq.Enumerable.FirstOrDefault[TSource](IEnumerable`1 source)
   at ProfileDAL.ProfileRepository.ApproveContract_Background(ContractDTO objContract) in D:\GIT\BCG_DEV_NEW\ProfileDAL\ProfileRepository.Contract.vb:line 2050
Other Text: iProfile

Function: ApproveContract_Background
Datetime: 07/05/2022 2:37:05 PM
Error message: Non-static method requires a target.
Stack trace:    at System.Reflection.RuntimeMethodInfo.CheckConsistency(Object target)
   at System.Reflection.RuntimeMethodInfo.InvokeArgumentsCheck(Object obj, BindingFlags invokeAttr, Binder binder, Object[] parameters, CultureInfo culture)
   at System.Reflection.RuntimeMethodInfo.Invoke(Object obj, BindingFlags invokeAttr, Binder binder, Object[] parameters, CultureInfo culture)
   at System.Reflection.RuntimePropertyInfo.GetValue(Object obj, Object[] index)
   at System.Data.Objects.ELinq.QueryParameterExpression.TryGetFieldOrPropertyValue(MemberExpression me, Object instance, Object& memberValue)
   at System.Data.Objects.ELinq.QueryParameterExpression.TryEvaluatePath(Expression expression, ConstantExpression& constantExpression)
   at System.Data.Objects.ELinq.QueryParameterExpression.EvaluateParameter(Object[] arguments)
   at System.Data.Objects.ELinq.ELinqQueryState.GetExecutionPlan(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Linq.Enumerable.FirstOrDefault[TSource](IEnumerable`1 source)
   at ProfileDAL.ProfileRepository.ApproveContract_Background(ContractDTO objContract) in D:\GIT\BCG_DEV_NEW\ProfileDAL\ProfileRepository.Contract.vb:line 2050
Other Text: iProfile

Function: GetFileByte_Userfile
Datetime: 07/05/2022 3:34:05 PM
Error message: Access to the path 'D:\GIT\BCG_DEV_NEW\HistaffServiceHost\UserfileExport20220705153345' is denied.
Stack trace:    at System.IO.__Error.WinIOError(Int32 errorCode, String maybeFullPath)
   at System.IO.File.InternalDelete(String path, Boolean checkHost)
   at ProfileDAL.ProfileRepository.GetFileByte_Userfile(String fileUpload) in D:\GIT\BCG_DEV_NEW\ProfileDAL\ProfileRepository.Employee.vb:line 5962
Other Text: iProfile

Function: ProfileDAL.ProfileRepository.ModifyTitleById
Datetime: 07/05/2022 5:14:09 PM
Error message: Object reference not set to an instance of an object.
Stack trace:    at ProfileDAL.ProfileRepository.ModifyTitleById(TitleDTO obj, Decimal OrgRight, Decimal Address, UserLog log, Decimal OrgIDDefault, Decimal IsDissolveDefault) in D:\GIT\BCG_DEV_NEW\ProfileDAL\ProfileRepository.List.vb:line 10606
Other Text: 

Function: ProfileDAL.ProfileRepository.ModifyTitleById
Datetime: 07/05/2022 5:14:39 PM
Error message: Object reference not set to an instance of an object.
Stack trace:    at ProfileDAL.ProfileRepository.ModifyTitleById(TitleDTO obj, Decimal OrgRight, Decimal Address, UserLog log, Decimal OrgIDDefault, Decimal IsDissolveDefault) in D:\GIT\BCG_DEV_NEW\ProfileDAL\ProfileRepository.List.vb:line 10606
Other Text: 

