Imports CommonBusiness.ServiceContracts
Imports CommonDAL
Imports Framework.Data


' NOTE: You can use the "Rename" command on the context menu to change the class name "Service1" in both code and config file together.
Namespace CommonBusiness.ServiceImplementations
    Partial Public Class CommonBusiness
        Implements ICommonBusiness

        Public Function CheckOtherListExistInDatabase(ByVal lstID As List(Of Decimal),
                                                      ByVal typeID As Decimal) As Boolean _
                                                  Implements ServiceContracts.ICommonBusiness.CheckOtherListExistInDatabase
            Using rep As New CommonRepository
                Try

                    Return rep.CheckOtherListExistInDatabase(lstID, typeID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
            Return False
        End Function

        Public Function GetATOrgPeriod(ByVal periodID As Decimal) As DataTable _
                                                  Implements ServiceContracts.ICommonBusiness.GetATOrgPeriod
            Using rep As New CommonRepository
                Try
                    Return rep.GetATOrgPeriod(periodID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

#Region "Check User Login"

        Public Function IsUsernameExist(ByVal Username As String) As Boolean _
            Implements ServiceContracts.ICommonBusiness.IsUsernameExist
            Using rep As New CommonRepository
                Try

                    Return rep.IsUsernameExist(Username)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
            Return False
        End Function

        Public Function GetUserWithPermision(ByVal Username As String) As UserDTO _
            Implements ServiceContracts.ICommonBusiness.GetUserWithPermision
            Using rep As New CommonRepository
                Try
                    Return rep.GetUserWithPermision(Username)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
            Return Nothing
        End Function

        Public Function GetUserPermissions(ByVal Username As String) As List(Of CommonDAL.PermissionDTO) _
            Implements ServiceContracts.ICommonBusiness.GetUserPermissions
            Using rep As New CommonRepository
                Try

                    Return rep.GetUserPermissions(Username)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
            Return Nothing
        End Function

        Public Function CheckUserAdmin(ByVal Username As String) As Boolean _
            Implements ServiceContracts.ICommonBusiness.CheckUserAdmin
            Using rep As New CommonRepository
                Try

                    Return rep.CheckUserAdmin(Username)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetUserByUserID(ByVal UserID As Decimal) As CommonDAL.SE_USER _
            Implements ServiceContracts.ICommonBusiness.GetUserByUserID
            Using rep As New CommonRepository
                Try
                    Return rep.GetUserByUserID(UserID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
            Return Nothing
        End Function

        Public Function GetUserByUserName(ByVal UserName As String) As CommonDAL.SE_USER _
            Implements ServiceContracts.ICommonBusiness.GetUserByUserName
            Using rep As New CommonRepository
                Try
                    Return rep.GetUserByUserName(UserName)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
            Return Nothing
        End Function

        Public Function ChangeUserPassword(ByVal Username As String,
                                           ByVal _oldpass As String,
                                           ByVal _newpass As String,
                                           ByVal log As UserLog) As Boolean _
                                       Implements ServiceContracts.ICommonBusiness.ChangeUserPassword
            Using rep As New CommonRepository
                Try

                    Return rep.ChangeUserPassword(Username, _oldpass, _newpass, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
            Return Nothing
        End Function

        Public Function GetPassword(ByVal Username As String) As String _
            Implements ServiceContracts.ICommonBusiness.GetPassword
            Using rep As New CommonRepository
                Try

                    Return rep.GetPassword(Username)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function UpdateUserStatus(ByVal Username As String,
                                         ByVal _ACTFLG As String,
                                         ByVal log As UserLog) As Boolean _
                                     Implements ServiceContracts.ICommonBusiness.UpdateUserStatus
            Using rep As New CommonRepository
                Try

                    Return rep.UpdateUserStatus(Username, _ACTFLG, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
            Return Nothing
        End Function
#End Region

#Region "Organization"
        Public Function GetOrganizationList() As List(Of OrganizationDTO) _
            Implements ICommonBusiness.GetOrganizationList
            Using rep As New CommonRepository
                Try
                    Dim lst = rep.GetOrganizationList()
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetOrganizationAll(ByVal IsOM As Boolean) As List(Of OrganizationDTO) _
            Implements ICommonBusiness.GetOrganizationAll
            Using rep As New CommonRepository
                Try

                    Dim lst = rep.GetOrganizationAll(IsOM)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetOrganizationLocationTreeView(ByVal _username As String, Optional ByVal fid As String = "", Optional ByVal state As String = "") As List(Of CommonDAL.OrganizationDTO) _
            Implements ServiceContracts.ICommonBusiness.GetOrganizationLocationTreeView
            Using rep As New CommonRepository
                Try

                    Dim lst = rep.GetOrganizationLocationTreeView(_username, fid, state)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetOrganizationLocationTreeView_New(ByVal _username As String) As List(Of OrganizationDTO) _
             Implements ServiceContracts.ICommonBusiness.GetOrganizationLocationTreeView_New
            Using rep As New CommonRepository
                Try
                    Dim lst = rep.GetOrganizationLocationTreeView_New(_username)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetOrganizationLocationInfo(ByVal _orgId As System.Decimal) As CommonDAL.OrganizationDTO _
            Implements ServiceContracts.ICommonBusiness.GetOrganizationLocationInfo
            Using rep As New CommonRepository
                Try

                    Dim lst = rep.GetOrganizationLocationInfo(_orgId)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetOrganizationStructureInfo(ByVal _orgId As System.Decimal) As List(Of CommonDAL.OrganizationStructureDTO) _
            Implements ServiceContracts.ICommonBusiness.GetOrganizationStructureInfo
            Using rep As New CommonRepository
                Try

                    Dim lst = rep.GetOrganizationStructureInfo(_orgId)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "Get Combo List Data"
        Public Function GetComboList(ByRef _combolistDTO As CommonDAL.ComboBoxDataDTO) As Boolean _
            Implements ServiceContracts.ICommonBusiness.GetComboList
            Using rep As New CommonRepository
                Try

                    Return rep.GetComboList(_combolistDTO)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetAllTrCertificateList(ByVal sLang As String, ByVal isBlank As Boolean) As DataTable _
            Implements ServiceContracts.ICommonBusiness.GetAllTrCertificateList
            Using rep As New CommonRepository
                Try

                    Return rep.GetAllTrCertificateList(sLang, isBlank)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetCourseByList(ByVal sLang As String, ByVal isBlank As Boolean) As DataTable _
            Implements ServiceContracts.ICommonBusiness.GetCourseByList
            Using rep As New CommonRepository
                Try

                    Return rep.GetCourseByList(sLang, isBlank)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetPeriodYear(ByVal isBlank As Boolean) As DataTable _
            Implements ServiceContracts.ICommonBusiness.GetPeriodYear
            Using rep As New CommonRepository
                Try

                    Return rep.GetPeriodYear(isBlank)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetPeriodByYear(ByVal isBlank As Boolean, ByVal year As Decimal) As DataTable _
            Implements ServiceContracts.ICommonBusiness.GetPeriodByYear
            Using rep As New CommonRepository
                Try

                    Return rep.GetPeriodByYear(isBlank, year)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetClassification(ByVal isBlank As Boolean) As DataTable _
            Implements ServiceContracts.ICommonBusiness.GetClassification
            Using rep As New CommonRepository
                Try

                    Return rep.GetClassification(isBlank)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetLearningLevel(ByVal sLang As String, ByVal isBlank As Boolean) As DataTable _
            Implements ServiceContracts.ICommonBusiness.GetLearningLevel
            Using rep As New CommonRepository
                Try

                    Return rep.GetLearningLevel(sLang, isBlank)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetHU_CompetencyList(ByVal isBlank As Boolean) As DataTable _
            Implements ServiceContracts.ICommonBusiness.GetHU_CompetencyList
            Using rep As New CommonRepository
                Try

                    Return rep.GetHU_CompetencyList(isBlank)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

#End Region

#Region "SendMail"
        Public Function InsertMail(ByVal _from As String,
                                   ByVal _to As String,
                                   ByVal _subject As String,
                                   ByVal _content As String,
                                   Optional ByVal _cc As String = "",
                                   Optional ByVal _bcc As String = "",
                                   Optional ByVal _viewName As String = "",
                                   Optional ByVal DefaultRunNowIsZero As Boolean = False,
                                    Optional ByVal _orderBy As Decimal = 6) As Object _
                               Implements ServiceContracts.ICommonBusiness.InsertMail
            Using rep As New CommonRepository
                Try
                    Return rep.InsertMail(_from, _to, _subject, _content, _cc, _bcc, _viewName, DefaultRunNowIsZero, _orderBy)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function SendMail() As Boolean _
            Implements ServiceContracts.ICommonBusiness.SendMail
            Using rep As New CommonRepository
                Try

                    Return rep.SendMail()
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

#End Region

#Region "OtherList"
        Public Function saveActionLogMB(ByVal userid As Decimal, ByVal objname As String, ByVal computername As String, ByVal viewName As String, ByVal lstpr As List(Of OtherListDTO)) As Decimal _
            Implements ServiceContracts.ICommonBusiness.saveActionLogMB
            Using rep As New CommonRepository
                Try

                    Dim lst = rep.saveActionLogMB(userid, objname, computername, viewName, lstpr)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetOtherListByTypeToCombo(ByVal sType As String) As List(Of CommonDAL.OtherListDTO) _
            Implements ServiceContracts.ICommonBusiness.GetOtherListByTypeToCombo
            Using rep As New CommonRepository
                Try

                    Dim lst = rep.GetOtherListByTypeToCombo(sType)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetOtherList(ByVal sACT As String) As List(Of CommonDAL.OtherListDTO) _
            Implements ServiceContracts.ICommonBusiness.GetOtherList
            Using rep As New CommonRepository
                Try

                    Dim lst = rep.GetOtherList(sACT)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetOtherListByType(ByVal gID As Decimal,
                                       ByVal _filter As OtherListDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "ORDERBYID ASC") As List(Of CommonDAL.OtherListDTO) _
                                    Implements ServiceContracts.ICommonBusiness.GetOtherListByType
            Using rep As New CommonRepository
                Try

                    Dim lst = rep.GetOtherListByType(gID, _filter, PageIndex, PageSize, Total, Sorts)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertOtherList(ByVal objOtherList As OtherListDTO,
                                        ByVal log As UserLog) As Boolean _
                                    Implements ServiceContracts.ICommonBusiness.InsertOtherList
            Using rep As New CommonRepository
                Try

                    Return rep.InsertOtherList(objOtherList, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyOtherList(ByVal objOtherList As OtherListDTO,
                                        ByVal log As UserLog) As Boolean _
                                    Implements ServiceContracts.ICommonBusiness.ModifyOtherList
            Using rep As New CommonRepository
                Try

                    Return rep.ModifyOtherList(objOtherList, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateOtherList(ByVal _validate As OtherListDTO) As Boolean _
            Implements ServiceContracts.ICommonBusiness.ValidateOtherList
            Using rep As New CommonRepository
                Try

                    Return rep.ValidateOtherList(_validate)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ActiveOtherList(ByVal lstOtherList As List(Of Decimal),
                                        ByVal sActive As String,
                                        ByVal log As UserLog) As Boolean _
                                    Implements ServiceContracts.ICommonBusiness.ActiveOtherList
            Using rep As New CommonRepository
                Try

                    Return rep.ActiveOtherList(lstOtherList, sActive, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteOtherList(ByVal lstOtherList As List(Of OtherListDTO)) As Boolean _
            Implements ServiceContracts.ICommonBusiness.DeleteOtherList
            Using rep As New CommonRepository
                Try

                    Return rep.DeleteOtherList(lstOtherList)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "OtherListGroup"
        Public Function GetOtherListGroupBySystem(ByVal systemName As String) As List(Of OtherListGroupDTO) _
            Implements ServiceContracts.ICommonBusiness.GetOtherListGroupBySystem
            Using rep As New CommonRepository
                Try

                    Return rep.GetOtherListGroupBySystem(systemName)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetOtherListGroup(ByVal sACT As String) As List(Of CommonDAL.OtherListGroupDTO) _
            Implements ServiceContracts.ICommonBusiness.GetOtherListGroup
            Using rep As New CommonRepository
                Try

                    Dim lst = rep.GetOtherListGroup(sACT)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertOtherListGroup(ByVal objOtherListGroup As OtherListGroupDTO,
                                             ByVal log As UserLog) As Boolean _
                                         Implements ServiceContracts.ICommonBusiness.InsertOtherListGroup
            Using rep As New CommonRepository
                Try

                    Return rep.InsertOtherListGroup(objOtherListGroup, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyOtherListGroup(ByVal objOtherListGroup As OtherListGroupDTO,
                                             ByVal log As UserLog) As Boolean _
                                         Implements ServiceContracts.ICommonBusiness.ModifyOtherListGroup
            Using rep As New CommonRepository
                Try

                    Return rep.ModifyOtherListGroup(objOtherListGroup, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ActiveOtherListGroup(ByVal lstOtherListGroup() As OtherListGroupDTO,
                                             ByVal sActive As String,
                                             ByVal log As UserLog) As Boolean _
                                         Implements ServiceContracts.ICommonBusiness.ActiveOtherListGroup
            Using rep As New CommonRepository
                Try

                    Return rep.ActiveOtherListGroup(lstOtherListGroup, sActive, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteOtherListGroup(ByVal lstOtherListGroup As List(Of OtherListGroupDTO)) As Boolean _
            Implements ServiceContracts.ICommonBusiness.DeleteOtherListGroup
            Using rep As New CommonRepository
                Try

                    Return rep.DeleteOtherListGroup(lstOtherListGroup)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

#End Region

#Region "OtherListType"
        Public Function GetOtherListTypeSystem(ByVal systemName As String) As List(Of OtherListTypeDTO) _
            Implements ServiceContracts.ICommonBusiness.GetOtherListTypeSystem
            Using rep As New CommonRepository
                Try

                    Dim lst = rep.GetOtherListTypeSystem(systemName)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetOtherListType(ByVal sACT As String) As List(Of OtherListTypeDTO) _
            Implements ServiceContracts.ICommonBusiness.GetOtherListType
            Using rep As New CommonRepository
                Try

                    Dim lst = rep.GetOtherListType(sACT)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertOtherListType(ByVal objOtherListType As OtherListTypeDTO,
                                            ByVal log As UserLog) As Boolean _
                                        Implements ServiceContracts.ICommonBusiness.InsertOtherListType
            Using rep As New CommonRepository
                Try

                    Return rep.InsertOtherListType(objOtherListType, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyOtherListType(ByVal objOtherListType As OtherListTypeDTO,
                                            ByVal log As UserLog) As Boolean _
                                        Implements ServiceContracts.ICommonBusiness.ModifyOtherListType
            Using rep As New CommonRepository
                Try

                    Return rep.ModifyOtherListType(objOtherListType, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ActiveOtherListType(ByVal lstOtherListType() As OtherListTypeDTO,
                                            ByVal sActive As String,
                                            ByVal log As UserLog) As Boolean _
                                        Implements ServiceContracts.ICommonBusiness.ActiveOtherListType
            Using rep As New CommonRepository
                Try

                    Return rep.ActiveOtherListType(lstOtherListType, sActive, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteOtherListType(ByVal lstOtherListType As List(Of OtherListTypeDTO)) As Boolean _
            Implements ServiceContracts.ICommonBusiness.DeleteOtherListType
            Using rep As New CommonRepository
                Try

                    Return rep.DeleteOtherListType(lstOtherListType)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "Reminder per user"

        Public Function GetReminderConfig(ByVal username As String) As Dictionary(Of Integer, String) _
            Implements ICommonBusiness.GetReminderConfig
            Using rep As New CommonRepository
                Try


                    Return rep.GetReminderConfig(username)
                Catch ex As Exception
                    Throw
                End Try
            End Using

        End Function

        Public Function SetReminderConfig(ByVal username As String,
                                          ByVal type As Integer,
                                          ByVal value As String) As Boolean _
                                      Implements ICommonBusiness.SetReminderConfig
            Using rep As New CommonRepository
                Try


                    Return rep.SetReminderConfig(username, type, value)
                Catch ex As Exception
                    Throw
                End Try
            End Using
        End Function

#End Region

#Region "SE_MAIL"

        Public Function Get_Se_Mail(ByVal _filter As SEMailDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATE_DATE desc") As List(Of SEMailDTO) _
                                    Implements ServiceContracts.ICommonBusiness.Get_Se_Mail
            Using rep As New CommonRepository
                Try

                    Dim lst = rep.Get_Se_Mail(_filter, PageIndex, PageSize, Total, Sorts)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

#End Region

#Region "Ldap"

        Public Function GetLdap(ByVal _filter As LdapDTO,
                             ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "LDAP_NAME") As List(Of LdapDTO) _
                                    Implements ServiceContracts.ICommonBusiness.GetLdap
            Using rep As New CommonRepository
                Try

                    Dim lst = rep.GetLdap(_filter, PageIndex, PageSize, Total, Sorts)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertLdap(ByVal objLdap As LdapDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.ICommonBusiness.InsertLdap
            Using rep As New CommonRepository
                Try

                    Return rep.InsertLdap(objLdap, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyLdap(ByVal objLdap As LdapDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.ICommonBusiness.ModifyLdap
            Using rep As New CommonRepository
                Try

                    Return rep.ModifyLdap(objLdap, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteLdap(ByVal lstID As List(Of Decimal)) As Boolean _
            Implements ServiceContracts.ICommonBusiness.DeleteLdap
            Using rep As New CommonRepository
                Try

                    Return rep.DeleteLdap(lstID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

#End Region

#Region "Validate ID Table"
        Public Function ValidateComboboxActive(ByVal tableName As String, ByVal colName As String, ByVal ID As Decimal) As Boolean _
            Implements ICommonBusiness.ValidateComboboxActive
            Using rep As New CommonRepository
                Try
                    Return rep.ValidateComboboxActive(tableName, colName, ID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function CheckExistIDTable(ByVal lstID As List(Of Decimal), ByVal table As String, ByVal column As String) As Boolean _
            Implements ICommonBusiness.CheckExistIDTable
            Using rep As New CommonRepository
                Try
                    Return rep.CheckExistIDTable(lstID, table, column)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function CheckExistValue(ByVal lstID As List(Of String), ByVal table As String, ByVal column As String) As Boolean _
           Implements ICommonBusiness.CheckExistValue
            Using rep As New CommonRepository
                Try
                    Return rep.CheckExistValue(lstID, table, column)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function CheckWorkStatus(ByVal colName As String, ByVal Value As String) As Boolean _
            Implements ICommonBusiness.CheckWorkStatus
            Using rep As New CommonRepository
                Try
                    Return rep.CheckWorkStatus(colName, Value)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function CheckExistProgram(ByVal strTableName As String, ByVal strcolName As String, ByVal strValue As String) As Boolean _
            Implements ICommonBusiness.CheckExistProgram
            Using rep As New CommonRepository
                Try
                    Return rep.CheckExistProgram(strTableName, strcolName, strValue)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function AutoGenColID(ByVal strTableName As String, ByVal strColName As String) As Decimal _
            Implements ICommonBusiness.AutoGenColID
            Using rep As New CommonRepository
                Try
                    Return rep.AutoGenColID(strTableName, strColName)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region
#Region "DynamicControl"
        Public Function GetListControl(ByVal KeyView As String) As DataTable Implements ServiceContracts.ICommonBusiness.GetListControl

            Using rep As New CommonRepository
                Return rep.GetListControl(KeyView:=KeyView)
            End Using

        End Function

        Public Function Insert_Edit_Dynamic_Control(ByVal KeyView As String, ByVal dataView As String) As DataTable Implements ServiceContracts.ICommonBusiness.Insert_Edit_Dynamic_Control
            Using rep As New CommonRepository
                Return rep.Insert_Edit_Dynamic_Control(KeyView, dataView)
            End Using
        End Function
#End Region
#Region "Config Display view"
        Public Function GetConfigView(ByVal KeyView As String) As DataTable Implements ServiceContracts.ICommonBusiness.GetConfigView
            Using rep As New CommonRepository
                Return rep.GetConfigView(KeyView)
            End Using
        End Function
        Public Function GetConfigViewAndFillData(ByVal keyView As String, ByRef viewcontrol As se_view_config_control_DTO, ByRef girdcollum As se_view_config_girdColumm_DTO) Implements ServiceContracts.ICommonBusiness.GetConfigViewAndFillData
            viewcontrol = New se_view_config_control_DTO
            girdcollum = New se_view_config_girdColumm_DTO
            Return 0
        End Function

#End Region
        Public Function GetTrainningRequest(ByVal _filter As RequestDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of RequestDTO) Implements ServiceContracts.ICommonBusiness.GetTrainningRequest
            Using rep As New CommonRepository
                Return rep.GetTrainningRequest(_filter, PageIndex, PageSize, Total, Sorts)
            End Using
        End Function

#Region "Trainning Request"

#End Region

#Region "Portal Process"
        Public Function GetProcessByIDReg(ByVal _id_RegGroup As Decimal, ByVal _process_Type As String) As List(Of ProcessDTO) _
           Implements ICommonBusiness.GetProcessByIDReg
            Using rep As New CommonRepository
                Try

                    Dim lst = rep.GetProcessByIDReg(_id_RegGroup, _process_Type)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

        Public Function GetFrameSalaryAll() As List(Of PA_FRAME_SALARYDTO) _
           Implements ICommonBusiness.GetFrameSalaryAll
            Using rep As New CommonRepository
                Try

                    Dim lst = rep.GetFrameSalaryAll()
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetFrameProductivityAll() As List(Of PA_FRAME_PRODUCTIVITYDTO) _
           Implements ICommonBusiness.GetFrameProductivityAll
            Using rep As New CommonRepository
                Try

                    Dim lst = rep.GetFrameProductivityAll()
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
    End Class
End Namespace