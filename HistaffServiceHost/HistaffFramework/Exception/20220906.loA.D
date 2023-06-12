Function: Histaff.DataAccessLayer.DataAccess.QueryData.DataAccess => ExecuteToDataSet
Datetime: 09/06/2022 9:03:43 AM
Error message: ORA-00904: "NAME": invalid identifier
ORA-06512: at "BCG_DEV.PKG_COMMON_LIST", line 2296
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
Datetime: 09/06/2022 9:03:43 AM
Error message: ORA-00904: "NAME": invalid identifier
ORA-06512: at "BCG_DEV.PKG_COMMON_LIST", line 2296
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

