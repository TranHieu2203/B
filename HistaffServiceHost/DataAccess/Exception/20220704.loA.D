Function: SQLHelper
Datetime: 07/04/2022 12:30:19 PM
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
Datetime: 07/04/2022 1:48:56 PM
Error message: ORA-00001: unique constraint (BCG_DEV.PK_SE_CHOSEN_ORG) violated
ORA-06512: at "BCG_DEV.PKG_COMMON_LIST", line 2038
ORA-06512: at line 1
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Symbols.Container.InvokeMethod(Method TargetProcedure, Object[] Arguments, Boolean[] CopyBack, BindingFlags Flags)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.CallMethod(Container BaseReference, String MethodName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, BindingFlags InvocationFlags, Boolean ReportErrors, ResolutionFailure& Failure)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.ObjectLateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateCall(Object Instance, Type Type, String MemberName, Object[] Arguments, String[] ArgumentNames, Type[] TypeArguments, Boolean[] CopyBack, Boolean IgnoreReturn)
   at Framework.Data.DataAccess.QueryData.ExecuteStore[T](String sql, T& obj, Boolean isDataTable) in D:\GIT\BCG_DEV_NEW\Framework.Data\DataAccess\Query.vb:line 253
Other Text: DataAccess

