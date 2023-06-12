Function: GetListWorkingBefore
Datetime: 06/29/2022 8:59:51 AM
Error message: ORA-00001: unique constraint (BCG_DEV.PK_SE_CHOSEN_ORG) violated
ORA-06512: at "BCG_DEV.PKG_COMMON_LIST", line 2038
ORA-06512: at line 1
Stack trace:    at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 303
   at ProfileDAL.ProfileRepository.GetListWorkingBefore(WorkingBeforeDTO _filter, Int32 PageIndex, Int32 PageSize, Int32& Total, ParamDTO _param, String Sorts, UserLog log) in D:\GIT\BCG_DEV_NEW\ProfileDAL\ProfileRepository.Employee.vb:line 588
Other Text: iProfile

Function: GetListWorkingBefore
Datetime: 06/29/2022 11:27:56 AM
Error message: ORA-00028: your session has been killed
Stack trace:    at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 303
   at ProfileDAL.ProfileRepository.GetListWorkingBefore(WorkingBeforeDTO _filter, Int32 PageIndex, Int32 PageSize, Int32& Total, ParamDTO _param, String Sorts, UserLog log) in D:\GIT\BCG_DEV_NEW\ProfileDAL\ProfileRepository.Employee.vb:line 588
Other Text: iProfile

Function: GetListWorkingBefore
Datetime: 06/29/2022 11:38:20 AM
Error message: An error occurred while executing the command definition. See the inner exception for details.
Stack trace:    at System.Data.EntityClient.EntityCommandDefinition.ExecuteStoreCommands(EntityCommand entityCommand, CommandBehavior behavior)
   at System.Data.Objects.Internal.ObjectQueryExecutionPlan.Execute[TResultType](ObjectContext context, ObjectParameterCollection parameterValues)
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Linq.Enumerable.Single[TSource](IEnumerable`1 source)
   at ProfileDAL.ProfileRepository.GetListWorkingBefore(WorkingBeforeDTO _filter, Int32 PageIndex, Int32 PageSize, Int32& Total, ParamDTO _param, String Sorts, UserLog log) in D:\GIT\BCG_DEV_NEW\ProfileDAL\ProfileRepository.Employee.vb:line 679
Inner error message: ORA-00028: your session has been killed
Inner stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck, Int32 isRecoverable, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, String procedure, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, Boolean bCheck, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleCommand.ExecuteReader(Boolean requery, Boolean fillRequest, CommandBehavior behavior)
   at Oracle.DataAccess.Client.OracleCommand.ExecuteDbDataReader(CommandBehavior behavior)
   at System.Data.EntityClient.EntityCommandDefinition.ExecuteStoreCommands(EntityCommand entityCommand, CommandBehavior behavior)
Other Text: iProfile

Function: GetListWorkingBefore
Datetime: 06/29/2022 11:41:14 AM
Error message: ORA-03135: connection lost contact
Process ID: 472
Session ID: 216 Serial number: 21822
Stack trace:    at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 303
   at ProfileDAL.ProfileRepository.GetListWorkingBefore(WorkingBeforeDTO _filter, Int32 PageIndex, Int32 PageSize, Int32& Total, ParamDTO _param, String Sorts, UserLog log) in D:\GIT\BCG_DEV_NEW\ProfileDAL\ProfileRepository.Employee.vb:line 588
Other Text: iProfile

Function: ApproveContract_Background
Datetime: 06/29/2022 12:54:46 PM
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
   at ProfileDAL.ProfileRepository.ApproveContract_Background(ContractDTO objContract) in D:\GIT\BCG_DEV_NEW\ProfileDAL\ProfileRepository.Contract.vb:line 2038
Other Text: iProfile

Function: ApproveContract_Background
Datetime: 06/29/2022 12:54:47 PM
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
   at ProfileDAL.ProfileRepository.ApproveContract_Background(ContractDTO objContract) in D:\GIT\BCG_DEV_NEW\ProfileDAL\ProfileRepository.Contract.vb:line 2038
Other Text: iProfile

Function: ApproveContract_Background
Datetime: 06/29/2022 12:54:47 PM
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
   at ProfileDAL.ProfileRepository.ApproveContract_Background(ContractDTO objContract) in D:\GIT\BCG_DEV_NEW\ProfileDAL\ProfileRepository.Contract.vb:line 2038
Other Text: iProfile

Function: ApproveContract_Background
Datetime: 06/29/2022 12:54:47 PM
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
   at ProfileDAL.ProfileRepository.ApproveContract_Background(ContractDTO objContract) in D:\GIT\BCG_DEV_NEW\ProfileDAL\ProfileRepository.Contract.vb:line 2038
Other Text: iProfile

Function: ApproveContract_Background
Datetime: 06/29/2022 12:54:47 PM
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
   at ProfileDAL.ProfileRepository.ApproveContract_Background(ContractDTO objContract) in D:\GIT\BCG_DEV_NEW\ProfileDAL\ProfileRepository.Contract.vb:line 2038
Other Text: iProfile

Function: ApproveContract_Background
Datetime: 06/29/2022 12:54:47 PM
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
   at ProfileDAL.ProfileRepository.ApproveContract_Background(ContractDTO objContract) in D:\GIT\BCG_DEV_NEW\ProfileDAL\ProfileRepository.Contract.vb:line 2038
Other Text: iProfile

Function: GetOtherList
Datetime: 06/29/2022 1:35:18 PM
Error message: ORA-12170: TNS:Connect timeout occurred
Stack trace:    at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 303
   at ProfileDAL.ProfileRepository.GetOtherList(String sType, String sLang, Boolean isBlank) in D:\GIT\BCG_DEV_NEW\ProfileDAL\ProfileRepository.vb:line 55
Other Text: iProfile

Function: ApproveContract_Background
Datetime: 06/29/2022 3:04:52 PM
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
   at ProfileDAL.ProfileRepository.ApproveContract_Background(ContractDTO objContract) in D:\GIT\BCG_DEV_NEW\ProfileDAL\ProfileRepository.Contract.vb:line 2038
Other Text: iProfile

Function: ApproveContract_Background
Datetime: 06/29/2022 3:04:52 PM
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
   at ProfileDAL.ProfileRepository.ApproveContract_Background(ContractDTO objContract) in D:\GIT\BCG_DEV_NEW\ProfileDAL\ProfileRepository.Contract.vb:line 2038
Other Text: iProfile

Function: ApproveContract_Background
Datetime: 06/29/2022 3:04:52 PM
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
   at ProfileDAL.ProfileRepository.ApproveContract_Background(ContractDTO objContract) in D:\GIT\BCG_DEV_NEW\ProfileDAL\ProfileRepository.Contract.vb:line 2038
Other Text: iProfile

Function: ApproveContract_Background
Datetime: 06/29/2022 3:04:52 PM
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
   at ProfileDAL.ProfileRepository.ApproveContract_Background(ContractDTO objContract) in D:\GIT\BCG_DEV_NEW\ProfileDAL\ProfileRepository.Contract.vb:line 2038
Other Text: iProfile

Function: ApproveContract_Background
Datetime: 06/29/2022 3:04:52 PM
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
   at ProfileDAL.ProfileRepository.ApproveContract_Background(ContractDTO objContract) in D:\GIT\BCG_DEV_NEW\ProfileDAL\ProfileRepository.Contract.vb:line 2038
Other Text: iProfile

Function: ApproveContract_Background
Datetime: 06/29/2022 3:04:52 PM
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
   at ProfileDAL.ProfileRepository.ApproveContract_Background(ContractDTO objContract) in D:\GIT\BCG_DEV_NEW\ProfileDAL\ProfileRepository.Contract.vb:line 2038
Other Text: iProfile

Function: GetOtherList
Datetime: 06/29/2022 9:40:27 PM
Error message: ORA-12170: TNS:Connect timeout occurred
Stack trace:    at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 303
   at ProfileDAL.ProfileRepository.GetOtherList(String sType, String sLang, Boolean isBlank) in D:\GIT\BCG_DEV_NEW\ProfileDAL\ProfileRepository.vb:line 55
Other Text: iProfile

