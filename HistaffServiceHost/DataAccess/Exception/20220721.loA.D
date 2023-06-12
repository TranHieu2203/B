Function: SQLHelper
Datetime: 07/21/2022 12:30:19 PM
Error message: [DBNETLIB][ConnectionOpen (Connect()).]SQL Server does not exist or access denied.
Stack trace:    at System.Data.OleDb.OleDbConnectionInternal..ctor(OleDbConnectionString constr, OleDbConnection connection)
   at System.Data.OleDb.OleDbConnectionFactory.CreateConnection(DbConnectionOptions options, DbConnectionPoolKey poolKey, Object poolGroupProviderInfo, DbConnectionPool pool, DbConnection owningObject)
   at System.Data.ProviderBase.DbConnectionFactory.CreateNonPooledConnection(DbConnection owningConnection, DbConnectionPoolGroup poolGroup, DbConnectionOptions userOptions)
   at System.Data.ProviderBase.DbConnectionFactory.TryGetConnection(DbConnection owningConnection, TaskCompletionSource`1 retry, DbConnectionOptions userOptions, DbConnectionInternal oldConnection, DbConnectionInternal& connection)
   at System.Data.ProviderBase.DbConnectionInternal.TryOpenConnectionInternal(DbConnection outerConnection, DbConnectionFactory connectionFactory, TaskCompletionSource`1 retry, DbConnectionOptions userOptions)
   at System.Data.ProviderBase.DbConnectionInternal.OpenConnection(DbConnection outerConnection, DbConnectionFactory connectionFactory)
   at System.Data.OleDb.OleDbConnection.Open()
   at Framework.Data.SQLHelper.Connect(String strConn) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\SQLHelper.vb:line 54
Other Text: ConnectProvider=SQLOLEDB;Data Source=HSVSGNEDB03;Initial Catalog=WiseEyeOn39;User ID=HRIS;Password=wiseeye@hsv@2021;

Function: ExecuteStore
Datetime: 07/21/2022 5:43:51 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 5:43:52 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 5:43:55 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 5:43:59 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 5:44:05 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 5:44:05 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 5:44:09 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 5:44:12 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 5:44:15 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 5:44:32 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 5:44:33 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 5:44:33 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 5:45:18 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 5:45:19 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 5:48:19 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 5:48:20 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 5:48:20 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 5:48:21 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 5:48:24 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 5:48:24 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 5:48:27 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 5:48:28 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 5:48:28 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 5:51:39 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 5:51:41 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 5:51:41 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 5:51:44 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 5:51:45 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 5:52:02 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 5:52:02 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 5:52:02 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 5:52:02 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 5:52:02 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 5:52:02 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 5:52:03 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 5:52:03 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 5:52:03 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 5:54:47 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 5:54:49 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 5:54:50 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 5:54:50 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 5:54:50 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 5:54:50 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 5:54:50 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 5:54:50 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 5:54:51 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 5:54:51 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 5:54:51 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 5:54:51 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 5:54:51 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 5:54:51 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 5:54:52 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 5:54:53 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 5:54:53 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 5:54:53 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 5:54:53 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 5:54:53 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 5:54:53 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 5:54:56 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 5:54:57 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 5:54:58 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 5:54:58 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 6:16:26 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 6:16:27 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 6:16:29 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 6:16:33 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 6:16:34 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 6:16:34 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 6:16:35 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 6:16:40 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 6:16:43 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 6:16:44 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 6:16:44 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 6:24:56 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 6:24:57 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 6:24:59 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 6:25:06 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 6:25:07 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 6:25:07 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 6:25:12 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 6:25:28 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 6:25:29 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 6:25:29 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 6:25:49 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 6:25:50 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 6:25:50 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 6:25:51 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 6:26:02 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 6:26:22 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 6:26:25 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 6:26:26 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 6:26:26 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 6:29:02 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 6:29:03 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 6:29:03 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 6:29:04 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 6:29:05 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 6:29:09 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 6:29:31 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 6:29:32 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

Function: ExecuteStore
Datetime: 07/21/2022 6:29:32 PM
Error message: ORA-00604: error occurred at recursive SQL level 1
ORA-01003: no statement parsed
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 256
Other Text: DataAccess

