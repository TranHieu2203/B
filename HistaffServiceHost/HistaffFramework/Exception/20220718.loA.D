Function: Histaff.DataAccessLayer.DataAccess.QueryData.DataAccess => ExecuteToDataSet
Datetime: 07/18/2022 10:57:25 AM
Error message: ORA-00904: "NAME": invalid identifier
ORA-06512: at "BCG_DEV.PKG_COMMON_LIST", line 2278
ORA-06512: at line 1
Stack trace:    at Histaff.DataAccessLayer.OracleHelper.ExecuteDataset(String ConnectionString, CommandType CommandType, String CommandText, OracleParameter[] CommandParameters) in D:\GIT\BCG_DEV_NEW\Histaff.DataAccessLayer\OracleHelper.vb:line 375
   at Histaff.DataAccessLayer.DataAccess.QueryData.ExecuteToDataSet(String storeName, List`1 parameterValue) in D:\GIT\BCG_DEV_NEW\Histaff.DataAccessLayer\DataAccess.vb:line 105
Other Text: PKG_COMMON_LIST.GET_LIST_COMBOBOX 
{
OT_OTHER_LIST,
ID,
NAME,
 TYPE_CODE='DECISION_STATUS' ,
NAME,
True,
}

________________________________________________________________________________________________________

Function: HistaffFramework.ServiceImplementations.HistaffFramework.HistaffFramework => ExecuteToDataSet
Datetime: 07/18/2022 10:57:25 AM
Error message: ORA-00904: "NAME": invalid identifier
ORA-06512: at "BCG_DEV.PKG_COMMON_LIST", line 2278
ORA-06512: at line 1
Stack trace:    at Histaff.DataAccessLayer.OracleHelper.ExecuteDataset(String ConnectionString, CommandType CommandType, String CommandText, OracleParameter[] CommandParameters) in D:\GIT\BCG_DEV_NEW\Histaff.DataAccessLayer\OracleHelper.vb:line 375
   at Histaff.DataAccessLayer.DataAccess.QueryData.ExecuteToDataSet(String storeName, List`1 parameterValue) in D:\GIT\BCG_DEV_NEW\Histaff.DataAccessLayer\DataAccess.vb:line 126
   at HistaffFramework.ServiceImplementations.HistaffFramework.ExecuteToDataSet(String storeName, List`1 parameterValue) in D:\GIT\BCG_DEV_NEW\HistaffFramework\HistaffFramework.OracleDataAccess.vb:line 53
Other Text: PKG_COMMON_LIST.GET_LIST_COMBOBOX 
{
OT_OTHER_LIST,
ID,
NAME,
 TYPE_CODE='DECISION_STATUS' ,
NAME,
True,
}

________________________________________________________________________________________________________

Function: HistaffFramework.ServiceImplementations.HistaffFramework.HistaffFramework => ExecuteStoreScalar
Datetime: 07/18/2022 4:12:50 PM
Error message: Object reference not set to an instance of an object.
Stack trace:    at HistaffFramework.ServiceImplementations.HistaffFramework.ExecuteStoreScalar(String storeName, List`1 parameters) in D:\GIT\BCG_DEV_NEW\HistaffFramework\HistaffFramework.OracleDataAccess.vb:line 92
Other Text: PKG_COMMON_LIST.UPDATE_AUTHOR_USER 
{
1218,
}

________________________________________________________________________________________________________

Function: HistaffFramework.ServiceImplementations.HistaffFramework.HistaffFramework => ExecuteStoreScalar
Datetime: 07/18/2022 4:14:20 PM
Error message: Object reference not set to an instance of an object.
Stack trace:    at HistaffFramework.ServiceImplementations.HistaffFramework.ExecuteStoreScalar(String storeName, List`1 parameters) in D:\GIT\BCG_DEV_NEW\HistaffFramework\HistaffFramework.OracleDataAccess.vb:line 92
Other Text: PKG_COMMON_LIST.UPDATE_AUTHOR_USER 
{
1219,
}

________________________________________________________________________________________________________

