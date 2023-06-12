Imports Common.CommonBusiness
Imports Framework.UI



Partial Public Class CommonRepository
    Inherits CommonRepositoryBase

    Public Function CheckOtherListExistInDatabase(ByVal lstID As List(Of Decimal), ByVal typeID As Decimal) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.CheckOtherListExistInDatabase(lstID, typeID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetATOrgPeriod(ByVal periodID As Decimal) As DataTable
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetATOrgPeriod(periodID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

#Region "Check User Login"
    Public Function IsUsernameExist(ByVal Username As String) As Boolean
        Using rep As New CommonBusinessClient
            Try
                If Username = "" Then Return False
                Return rep.IsUsernameExist(Username)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetUserWithPermision(ByVal Username As String) As UserDTO
        Using rep As New CommonBusinessClient
            Try
                If Username = "" Then Return New UserDTO
                Return rep.GetUserWithPermision(Username)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetUserPermissions(ByVal Username As String) As List(Of PermissionDTO)
        Dim lstPermissions As New List(Of PermissionDTO)
        If Username = "" Then Return lstPermissions
        Using rep As New CommonBusinessClient
            Try
                lstPermissions = rep.GetUserPermissions(Username)
                Common.GetUserPermissionsDataSession = lstPermissions
                Return lstPermissions
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function CheckGroupAdmin(ByVal Username As String) As Boolean
        If Username = "" Then Return False
        Using rep As New CommonBusinessClient
            Try
                Dim GroupAdmin = Common.CheckUserAdmin
                If GroupAdmin Is Nothing Then
                    GroupAdmin = rep.CheckUserAdmin(Username)
                    Common.CheckUserAdmin = GroupAdmin
                End If
                Return GroupAdmin
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function


    Public Function ChangeUserPassword(ByVal Username As String, ByVal _oldpass As String, ByVal _newpass As String) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.ChangeUserPassword(Username, _oldpass, _newpass, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetPassword(ByVal Username As String) As String
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetPassword(Username)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function UpdateUserStatus(ByVal Username As String, ByVal _ACTFLG As String) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Dim log As New UserLog With {.ComputerName = System.Security.Principal.WindowsIdentity.GetCurrent.Name,
                                             .Ip = HttpContext.Current.Request.UserHostAddress,
                                             .Username = "ADMIN"}
                Return rep.UpdateUserStatus(Username, _ACTFLG, log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetUserAccessMenuModule(ByVal Username As String)
        Try
            If Username = "" Or LogHelper.CurrentUser Is Nothing Then Return ""
            Dim lstPer As List(Of PermissionDTO)
            If CheckGroupAdmin(Username) Then
                Common.UserAccessMenuModuleDataSession = LogHelper.CurrentUser.MODULE_ADMIN
            Else
                lstPer = GetUserPermissions(Username)
                Dim lstModule = (From p In lstPer
                                 Where p.IS_REPORT = False
                                 Select p.MID).Distinct.ToArray
                Common.UserAccessMenuModuleDataSession = String.Join(",", lstModule)
            End If
            Return Common.UserAccessMenuModuleDataSession
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "Organization"

    Public Function GetOrganizationList() As List(Of OrganizationDTO)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetOrganizationList()
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetOrganizationAll(ByVal IsOM As Boolean) As List(Of OrganizationDTO)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetOrganizationAll(IsOM)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    'edit function shared vi dung phan quyen trong thiet lap so do to chuc
    Public Shared Function GetOrganizationLocationTreeView() As List(Of OrganizationDTO)
        Dim lstOrgPermission As New List(Of OrganizationDTO)
        Dim lstAllOrg As New List(Of OrganizationDTO)
        Dim url As String = HttpContext.Current.Request.Url.AbsoluteUri
        Dim fid As String = HttpContext.Current.Request.Params("fid")
        Dim state As String = HttpContext.Current.Request.Params("state")
        Using rep As New CommonBusinessClient
            Try
                'lstOrgPermission = Common.OrganizationLocationDataSession
                If lstOrgPermission Is Nothing Then lstOrgPermission = New List(Of OrganizationDTO)

                If url.Contains("ctrlDeclaresOTNewEdit") Or url.Contains("ctrlOTRegistrationNewEdit") Or url.Contains("ctrlOTRegByAnotherNewEdit") _
                    Or url.Contains("ctrlRegisterDSVMNewEdit") Or url.Contains("ctrlDSVMNewEdit") Then
                    lstOrgPermission = rep.GetOrganizationLocationTreeView("ADMIN", "", "")
                ElseIf lstOrgPermission.Count = 0 Then
                    lstOrgPermission = rep.GetOrganizationLocationTreeView(Common.GetUsername, fid, state)
                    Common.OrganizationLocationDataSession = lstOrgPermission
                End If
                'If HttpContext.Current.Session("CallAllOrg") = "LoadAllOrg" Then
                '    lstAllOrg = rep.GetOrganizationLocationTreeView("ADMIN")
                '    Return lstAllOrg
                'End If
                Return lstOrgPermission
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Function GetOrganizationLocationTreeView_New() As List(Of OrganizationDTO)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetOrganizationLocationTreeView_New(Common.GetUsername)
            Catch ex As Exception

            End Try
        End Using
    End Function

    Public Function GetOrganizationLocationInfo(ByVal _orgId As Decimal) As OrganizationDTO
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetOrganizationLocationInfo(_orgId)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetOrganizationStructureInfo(ByVal _orgId As Decimal) As List(Of OrganizationStructureDTO)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetOrganizationStructureInfo(_orgId)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

#End Region

#Region "Get Combo List Data"
    ''' <summary>
    ''' Lấy dữ liệu đưa vào combobox
    ''' </summary>
    ''' <param name="_combolistDTO"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetComboList(ByRef _combolistDTO As ComboBoxDataDTO) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetComboList(_combolistDTO)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetAllTrCertificateList(ByVal isBlank As Boolean) As DataTable
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetAllTrCertificateList(Common.SystemLanguage.Name, isBlank)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetCourseByList(ByVal isBlank As Boolean) As DataTable
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetCourseByList(Common.SystemLanguage.Name, isBlank)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetPeriodYear(ByVal isBlank As Boolean) As DataTable
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetPeriodYear(isBlank)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetPeriodByYear(ByVal isBlank As Boolean, ByVal year As Decimal) As DataTable
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetPeriodByYear(isBlank, year)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetClassification(ByVal isBlank As Boolean) As DataTable
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetClassification(isBlank)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetLearningLevel(ByVal isBlank As Boolean) As DataTable
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetLearningLevel(Common.SystemLanguage.Name, isBlank)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetHU_CompetencyList(ByVal isBlank As Boolean) As DataTable
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetHU_CompetencyList(isBlank)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

#End Region

#Region "Config"

    Public Function GetConfig(ByVal eModule As SystemConfigModuleID) As Dictionary(Of String, String)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetConfig(eModule)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function UpdateConfig(ByVal lstConfig As Dictionary(Of String, String), ByVal eModule As SystemConfigModuleID) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.UpdateConfig(lstConfig, eModule)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "Config portal"
    Public Function SetGeneralConfig(ByVal _lstConfig As Dictionary(Of String, String), ByVal eModule As SystemConfigModuleID) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.SetGeneralConfig(_lstConfig, eModule)
            Catch ex As Exception
                Throw
            End Try
        End Using
    End Function
#End Region

#Region "SendMail"

    Public Function SendMail() As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.SendMail()
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function InsertMail(ByVal _from As String, ByVal _to As String, ByVal _subject As String, ByVal _content As String, Optional ByVal _cc As String = "", Optional ByVal _bcc As String = "", Optional ByVal _viewName As String = "", Optional ByVal DefaultRunNowIsZero As Boolean = False, Optional ByVal _orderBy As Decimal = 6)
        Using rep As New CommonBusinessClient
            Try
                Return rep.InsertMail(_from, _to, _subject, _content, _cc, _bcc, _viewName, DefaultRunNowIsZero, _orderBy)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "Reminder per user"

    Public Function GetReminderConfig(ByVal username As String) As Dictionary(Of Integer, String)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetReminderConfig(username)
            Catch ex As Exception
                rep.Abort()
                Throw
            End Try
        End Using
    End Function

    Public Function SetReminderConfig(ByVal username As String, ByVal type As Integer, ByVal value As String) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.SetReminderConfig(username, type, value)
            Catch ex As Exception
                rep.Abort()
                Throw
            End Try
        End Using

    End Function

#End Region

#Region "Other list"

    Public Function ActiveOtherListGroup(ByVal lstOtherListGroup As List(Of OtherListGroupDTO), ByVal sActive As String) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.ActiveOtherListGroup(lstOtherListGroup, sActive, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetOtherListGroupBySystem(ByVal systemName As String) As List(Of OtherListGroupDTO)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetOtherListGroupBySystem(systemName)

            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetOtherListGroup(Optional ByVal sACT As String = "") As List(Of OtherListGroupDTO)
        Dim lstOtherListGroup As List(Of OtherListGroupDTO)

        Using rep As New CommonBusinessClient
            Try
                lstOtherListGroup = rep.GetOtherListGroup(sACT)
                Return lstOtherListGroup
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function ModifyOtherListGroup(ByVal objOtherListGroup As OtherListGroupDTO) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.ModifyOtherListGroup(objOtherListGroup, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateOtherList(ByVal _validate As OtherListDTO) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.ValidateOtherList(_validate)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function DeleteOtherListGroup(ByVal lstOtherListGroup As List(Of OtherListGroupDTO)) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.DeleteOtherListGroup(lstOtherListGroup)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetOtherListType(Optional ByVal sACT As String = "") As List(Of OtherListTypeDTO)
        Dim lstOtherListType As List(Of OtherListTypeDTO)

        Using rep As New CommonBusinessClient
            Try
                lstOtherListType = rep.GetOtherListType(sACT)
                Return lstOtherListType
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetOtherListTypeSystem(ByVal systemName As String) As List(Of OtherListTypeDTO)
        Dim lstOtherListType As List(Of OtherListTypeDTO)

        Using rep As New CommonBusinessClient
            Try
                lstOtherListType = rep.GetOtherListTypeSystem(systemName)
                Return lstOtherListType
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function ActiveOtherListType(ByVal lstOtherListType As List(Of OtherListTypeDTO), ByVal sActive As String) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.ActiveOtherListType(lstOtherListType, sActive, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyOtherList(ByVal objOtherList As OtherListDTO) As Boolean
        Using rep As New CommonBusinessClient
            Try
                CacheManager.ClearValue("OT_" & objOtherList.TYPE_CODE & "_vi-VN" & IIf(True, "Blank", "NoBlank"))
                CacheManager.ClearValue("OT_" & objOtherList.TYPE_CODE & "_vi-VN" & IIf(False, "Blank", "NoBlank"))
                CacheManager.ClearValue("OT_" & objOtherList.TYPE_CODE & "_en-US" & IIf(True, "Blank", "NoBlank"))
                CacheManager.ClearValue("OT_" & objOtherList.TYPE_CODE & "_en-US" & IIf(False, "Blank", "NoBlank"))
                Return rep.ModifyOtherList(objOtherList, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function InsertOtherList(ByVal objOtherList As OtherListDTO) As Boolean
        Using rep As New CommonBusinessClient
            Try
                CacheManager.ClearValue("OT_" & objOtherList.TYPE_CODE & "_vi-VN" & IIf(True, "Blank", "NoBlank"))
                CacheManager.ClearValue("OT_" & objOtherList.TYPE_CODE & "_vi-VN" & IIf(False, "Blank", "NoBlank"))
                CacheManager.ClearValue("OT_" & objOtherList.TYPE_CODE & "_en-US" & IIf(True, "Blank", "NoBlank"))
                CacheManager.ClearValue("OT_" & objOtherList.TYPE_CODE & "_en-US" & IIf(False, "Blank", "NoBlank"))
                Return rep.InsertOtherList(objOtherList, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveOtherList(ByVal lstOtherList As List(Of Decimal), ByVal sActive As String, type_code As String) As Boolean
        Using rep As New CommonBusinessClient
            Try
                CacheManager.ClearValue("OT_" & type_code & "_vi-VN" & IIf(True, "Blank", "NoBlank"))
                CacheManager.ClearValue("OT_" & type_code & "_vi-VN" & IIf(False, "Blank", "NoBlank"))
                CacheManager.ClearValue("OT_" & type_code & "_en-US" & IIf(True, "Blank", "NoBlank"))
                CacheManager.ClearValue("OT_" & type_code & "_en-US" & IIf(False, "Blank", "NoBlank"))
                Return rep.ActiveOtherList(lstOtherList, sActive, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteOtherList(ByVal lstOtherList As List(Of OtherListDTO), type_code As String) As Boolean
        Using rep As New CommonBusinessClient
            Try
                CacheManager.ClearValue("OT_" & type_code & "_vi-VN" & IIf(True, "Blank", "NoBlank"))
                CacheManager.ClearValue("OT_" & type_code & "_vi-VN" & IIf(False, "Blank", "NoBlank"))
                CacheManager.ClearValue("OT_" & type_code & "_en-US" & IIf(True, "Blank", "NoBlank"))
                CacheManager.ClearValue("OT_" & type_code & "_en-US" & IIf(False, "Blank", "NoBlank"))
                Return rep.DeleteOtherList(lstOtherList)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function InsertOtherListGroup(ByVal objOtherListGroup As OtherListGroupDTO) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.InsertOtherListGroup(objOtherListGroup, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function InsertOtherListType(ByVal objOtherListType As OtherListTypeDTO) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.InsertOtherListType(objOtherListType, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetOtherListByType(ByVal gID As Decimal,
                                       ByVal _filter As OtherListDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "ORDERBYID ASC") As List(Of OtherListDTO)
        Dim lstOtherList As List(Of OtherListDTO)

        Using rep As New CommonBusinessClient
            Try
                lstOtherList = rep.GetOtherListByType(gID, _filter, PageIndex, PageSize, Total, Sorts)
                Return lstOtherList
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function ModifyOtherListType(ByVal objOtherListType As OtherListTypeDTO) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.ModifyOtherListType(objOtherListType, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteOtherListType(ByVal lstOtherListType As List(Of OtherListTypeDTO)) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.DeleteOtherListType(lstOtherListType)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
#End Region

#Region "SE_MAIL"

    Public Function Get_Se_Mail(ByVal _filter As SEMailDTO,
                             ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATE_DATE desc") As List(Of SEMailDTO)
        Dim lstLdap As List(Of SEMailDTO)

        Using rep As New CommonBusinessClient
            Try
                lstLdap = rep.Get_Se_Mail(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstLdap
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function Get_Se_Mail(ByVal _filter As SEMailDTO,
                                        Optional ByVal Sorts As String = "CREATE_DATE desc") As List(Of SEMailDTO)
        Dim lstLdap As List(Of SEMailDTO)

        Using rep As New CommonBusinessClient
            Try
                lstLdap = rep.Get_Se_Mail(_filter, 0, Integer.MaxValue, 0, Sorts)
                Return lstLdap
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

#End Region

#Region "Ldap"

    Public Function GetLdap(ByVal _filter As LdapDTO,
                             ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "LDAP_NAME") As List(Of LdapDTO)
        Dim lstLdap As List(Of LdapDTO)

        Using rep As New CommonBusinessClient
            Try
                lstLdap = rep.GetLdap(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstLdap
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetLdap(ByVal _filter As LdapDTO,
                                        Optional ByVal Sorts As String = "LDAP_NAME") As List(Of LdapDTO)
        Dim lstLdap As List(Of LdapDTO)

        Using rep As New CommonBusinessClient
            Try
                lstLdap = rep.GetLdap(_filter, 0, Integer.MaxValue, 0, Sorts)
                Return lstLdap
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertLdap(ByVal objLdap As LdapDTO, ByRef gID As Decimal) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.InsertLdap(objLdap, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyLdap(ByVal objLdap As LdapDTO, ByRef gID As Decimal) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.ModifyLdap(objLdap, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteLdap(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.DeleteLdap(lstID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region
#Region "Validate ID Table"
    Public Function ValidateComboboxActive(ByVal tableName As String, ByVal colName As String, ByVal ID As Decimal) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.ValidateComboboxActive(tableName, colName, ID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function CheckExistIDTable(ByVal lstID As List(Of Decimal), ByVal table As String, ByVal column As String) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.CheckExistIDTable(lstID, table, column)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function CheckExistValue(ByVal lstID As List(Of String), ByVal table As String, ByVal column As String) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.CheckExistValue(lstID, table, column)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function CheckWorkStatus(ByVal colName As String, ByVal Value As String) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.CheckWorkStatus(colName, Value)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function CheckExistProgram(ByVal strTableName As String, ByVal strcolName As String, ByVal strValue As String) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.CheckExistProgram(strTableName, strcolName, strValue)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function AutoGenColID(ByVal strTableName As String, ByVal strColName As String) As Decimal
        Using rep As New CommonBusinessClient
            Try
                Return rep.AutoGenColID(strTableName, strColName)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
#End Region
#Region "DynamicControl"
    Public Function GetListControl(ByVal KeyView As String) As DataTable ' Implements ServiceContracts.ICommonBusiness.GetListControl

        Using rep As New CommonBusinessClient
            Return rep.GetListControl(KeyView:=KeyView)
        End Using

    End Function

    Public Function Insert_Edit_Dynamic_Control(ByVal KeyView As String, ByVal dataView As String) As DataTable ' Implements ServiceContracts.ICommonBusiness.Insert_Edit_Dynamic_Control
        Using rep As New CommonBusinessClient
            Return rep.Insert_Edit_Dynamic_Control(KeyView, dataView)
        End Using
    End Function
#End Region
#Region "Config Display view"
    Public Function GetConfigView(ByVal KeyView As String) As DataTable
        Using rep As New CommonBusinessClient
            Return rep.GetConfigView(KeyView:=KeyView)
        End Using
    End Function
#End Region
    Public Function GetFrameSalaryAll() As List(Of PA_FRAME_SALARYDTO)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetFrameSalaryAll()
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function GetFrameProductivityAll() As List(Of PA_FRAME_PRODUCTIVITYDTO)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetFrameProductivityAll()
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
End Class
