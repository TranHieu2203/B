Function: Histaff.DataAccessLayer.DataAccess.QueryData.DataAccess => ExecuteToDataSet
Datetime: 26/10/2017 3:12:03 PM
Error message: ORA-12545: Connect failed because target host or object does not exist
Stack trace:    at Histaff.DataAccessLayer.OracleHelperParameterCache.DiscoverSpParameterSet(String ConnectionString, String StoredProceduredName, Boolean IncludeReturnValueParameter, Object[] ParameterValues) in E:\workspaces\KYODO\05.DEPLOYMENT\SP Core Level2\Histaff.DataAccessLayer\OracleHelper.vb:line 852
   at Histaff.DataAccessLayer.OracleHelperParameterCache.GetSpParameterSet(String ConnectionString, String StoredProceduredName, Boolean IncludeReturnValueParameter) in E:\workspaces\KYODO\05.DEPLOYMENT\SP Core Level2\Histaff.DataAccessLayer\OracleHelper.vb:line 920
   at Histaff.DataAccessLayer.DataAccess.QueryData.ExecuteToDataSet(String storeName, List`1 parameterValue) in E:\workspaces\KYODO\05.DEPLOYMENT\SP Core Level2\Histaff.DataAccessLayer\DataAccess.vb:line 86
Other Text: PKG_COMMON_LIST.GET_LIST_INSURANCE 
{
1,
}

________________________________________________________________________________________________________

Function: HistaffFramework.ServiceImplementations.HistaffFramework.HistaffFramework => ExecuteToDataSet
Datetime: 26/10/2017 3:12:03 PM
Error message: ORA-12545: Connect failed because target host or object does not exist
Stack trace:    at Histaff.DataAccessLayer.OracleHelperParameterCache.DiscoverSpParameterSet(String ConnectionString, String StoredProceduredName, Boolean IncludeReturnValueParameter, Object[] ParameterValues) in E:\workspaces\KYODO\05.DEPLOYMENT\SP Core Level2\Histaff.DataAccessLayer\OracleHelper.vb:line 852
   at Histaff.DataAccessLayer.OracleHelperParameterCache.GetSpParameterSet(String ConnectionString, String StoredProceduredName, Boolean IncludeReturnValueParameter) in E:\workspaces\KYODO\05.DEPLOYMENT\SP Core Level2\Histaff.DataAccessLayer\OracleHelper.vb:line 920
   at Histaff.DataAccessLayer.DataAccess.QueryData.ExecuteToDataSet(String storeName, List`1 parameterValue) in E:\workspaces\KYODO\05.DEPLOYMENT\SP Core Level2\Histaff.DataAccessLayer\DataAccess.vb:line 126
   at HistaffFramework.ServiceImplementations.HistaffFramework.ExecuteToDataSet(String storeName, List`1 parameterValue) in E:\workspaces\KYODO\05.DEPLOYMENT\SP Core Level2\HistaffFramework\HistaffFramework.OracleDataAccess.vb:line 53
Other Text: PKG_COMMON_LIST.GET_LIST_INSURANCE 
{
1,
}

________________________________________________________________________________________________________

