Function: ExecuteStore
Datetime: 07/19/2022 11:08:52 AM
Error message: ORA-04063: package body "BCG_DEV.PKG_COMMON_LIST" has errors
ORA-06508: PL/SQL: could not find program unit being called: "BCG_DEV.PKG_COMMON_LIST"
ORA-06512: at line 1
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 253
Other Text: DataAccess

