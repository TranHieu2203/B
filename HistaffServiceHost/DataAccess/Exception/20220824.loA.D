Function: SQLHelper
Datetime: 08/24/2022 1:31:57 PM
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

