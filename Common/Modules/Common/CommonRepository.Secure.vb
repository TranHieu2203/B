Imports Common.CommonBusiness


Partial Public Class CommonRepository
    Inherits CommonRepositoryBase
    Public Function GetPositionToPopupFind(ByVal _filter As PositionPopupFindListDTO,
                                            ByVal PageIndex As Integer,
                                            ByVal PageSize As Integer,
                                            ByRef Total As Integer,
                                            Optional ByVal Sorts As String = "EMPLOYEE_CODE asc",
                                            Optional ByVal _param As ParamDTO = Nothing) As List(Of PositionPopupFindListDTO)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetPositionToPopupFind(_filter, PageIndex, PageSize, Total, Sorts, Log, _param)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function SE_RESET_USER(ByVal UserIDs As String) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.SE_RESET_USER(UserIDs)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function
#Region "Group Organization"
    Public Function GetOrganizationChildByList(ByVal _status As Decimal,
                                               ByVal _lstOrgID As List(Of String)) As List(Of Decimal)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetOrganizationChildByList(Log.Username.ToUpper, _status, _lstOrgID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetOrganizationPermission(ByVal _lstOrgID As List(Of Decimal)) As List(Of Decimal)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetOrganizationPermission(Log.Username.ToUpper, _lstOrgID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetOrganizationChildTextByList(ByVal _status As String,
                                               ByVal _lstOrgID As List(Of String)) As List(Of String)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetOrganizationChildTextByList(Log.Username.ToUpper, _status, _lstOrgID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetGroupOrganization(ByVal _groupID As Decimal) As List(Of Decimal)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetGroupOrganization(_groupID)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetGroupOrganizationFunction(ByVal _groupID As Decimal) As List(Of Decimal)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetGroupOrganizationFunction(_groupID)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Public Function DeleteGroupOrganization(ByVal _groupId As Decimal)
        Using rep As New CommonBusinessClient
            Try
                Return rep.DeleteGroupOrganization(_groupId)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Public Function UpdateGroupOrganization(ByVal _lstOrg As List(Of GroupOrgAccessDTO)) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.UpdateGroupOrganization(_lstOrg)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Public Function UpdateGroupOrganizationFunction(ByVal _lstOrg As List(Of GroupOrgFunAccessDTO)) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.UpdateGroupOrganizationFunction(_lstOrg)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function
#End Region

#Region "Case Config"
    Public Function GetCaseConfigByID(ByVal codename As String, ByVal codecase As String) As Integer
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetCaseConfigByID(codename, codecase)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
#End Region

#Region "Controls Manage"
    ''' <summary>
    ''' Hiển thị d/sach Tên page điều chỉnh control
    ''' </summary>
    ''' <param name="_filter"></param>
    ''' <param name="PageIndex"></param>
    ''' <param name="PageSize"></param>
    ''' <param name="Total"></param>
    ''' <param name="Sorts"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Get_FunctionWithControl_List(ByVal _filter As FunctionDTO,
                            ByVal PageIndex As Integer,
                            ByVal PageSize As Integer,
                            ByRef Total As Integer,
                            Optional ByVal Sorts As String = "NAME ASC") As List(Of FunctionDTO)
        Using rep As New CommonBusinessClient
            Try
                Return rep.Get_FunctionWithControl_List(_filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function Get_FunctionWithControl_List(ByVal _filter As FunctionDTO, Optional ByVal Sorts As String = "NAME ASC")
        Using rep As New CommonBusinessClient
            Try
                Return rep.Get_FunctionWithControl_List(_filter, 0, Integer.MaxValue, 0, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function Insert_Update_Control_List(ByVal _id As String, ByVal Config As String) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.Insert_Update_Control_List(_id, Config, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
#End Region

#Region "User List"
    Public Function GetUser(ByVal _filter As UserDTO,
                        ByVal PageIndex As Integer,
                        ByVal PageSize As Integer,
                        ByRef Total As Integer,
                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of UserDTO)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetUser(_filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetUserList(ByVal _filter As UserDTO,
                        ByVal PageIndex As Integer,
                        ByVal PageSize As Integer,
                        ByRef Total As Integer,
                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of UserDTO)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetUserList(_filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetUserList(ByVal _filter As UserDTO,
                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of UserDTO)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetUserList(_filter, 0, Integer.MaxValue, 0, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function ValidateUser(ByVal _validate As UserDTO) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.ValidateUser(_validate)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function InsertUser(ByVal objUser As UserDTO) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.InsertUser(objUser, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function ModifyUser(ByVal objUser As UserDTO) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.ModifyUser(objUser, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteUser(ByVal lstUser As List(Of Decimal), ByRef _error As String) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.DeleteUser(lstUser, _error, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function UpdateUserListStatus(ByVal _lstUserID As List(Of Decimal), ByVal _status As String) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.UpdateUserListStatus(_lstUserID, _status, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function SyncUserList(ByRef _newUser As String, ByRef _modifyUser As String, ByRef _deleteUser As String) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.SyncUserList(_newUser, _modifyUser, _deleteUser, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function Get_user_info(ByVal id As Decimal) As List(Of String)
        Using rep As New CommonBusinessClient
            Try
                Return rep.Get_user_info(id)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GET_MAIL_TEMPLATE(ByVal code As String, ByVal group As String) As List(Of String)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GET_MAIL_TEMPLATE(code, group)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ResetUserPassword(ByVal _userid As List(Of System.Decimal), ByVal _minLength As Integer, ByVal _hasLowerChar As Boolean, ByVal _hasUpperChar As Boolean, ByVal _hasNumbericChar As Boolean, ByVal _hasSpecialChar As Boolean, ByVal _hasPasswordConfig As Boolean, ByVal _hasPasswordDefault As Boolean, ByVal _hasPasswordDefaultText As String) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.ResetUserPassword(_userid, _minLength, _hasLowerChar, _hasUpperChar, _hasNumbericChar, _hasSpecialChar, _hasPasswordConfig, _hasPasswordDefault, _hasPasswordDefaultText)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function SendMailConfirmUserPassword(ByVal _userid As List(Of System.Decimal), ByVal _subject As String, ByVal _content As String,
                                                Optional ByVal _orderBy As Decimal = 6) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.SendMailConfirmUserPassword(_userid, _subject, _content, _orderBy)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetUserNeedSendMail(ByVal _groupid As System.Decimal) As List(Of UserDTO)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetUserNeedSendMail(_groupid)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateUserForgetPass(ByVal _userName As String, ByRef uID As Decimal) As String
        Using rep As New CommonBusinessClient
            Try
                Return rep.ValidateUserForgetPass(_userName, uID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function checkRulePass(ByVal pass As String, ByVal oldpass As String, ByVal userid As Decimal) As Decimal
        Using rep As New CommonBusinessClient
            Try
                'Return rep.checkRulePass(pass, oldpass, userid)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function ChangePassByForget(ByVal _userName As String, ByVal _passWord As String) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.ChangePassByForget(_userName, _passWord)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#End Region

#Region "Group List"

    Public Function GetGroupListToComboListBox() As List(Of GroupDTO)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetGroupListToComboListBox()
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetGroupListToComboListBox1() As List(Of GroupDTO)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetGroupListToComboListBox1()
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetGroupList(ByVal _filter As GroupDTO,
                        ByVal PageIndex As Integer,
                        ByVal PageSize As Integer,
                        ByRef Total As Integer,
                        Optional ByVal Sorts As String = "MODIFIED_DATE desc") As List(Of GroupDTO)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetGroupList(_filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function ValidateGroupList(ByVal _validate As GroupDTO) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.ValidateGroupList(_validate)
            Catch ex As Exception
                rep.Abort()

                Throw ex
            End Try
        End Using
    End Function

    Public Function InsertGroup(ByVal lst As GroupDTO) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.InsertGroup(lst, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function UpdateGroup(ByVal lst As GroupDTO) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.UpdateGroup(lst, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteGroup(ByVal lstID As List(Of Decimal), ByRef _error As String) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.DeleteGroup(lstID, _error, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function UpdateGroupStatus(ByVal _lstID As List(Of System.Decimal), ByVal _ACTFLG As String) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.UpdateGroupStatus(_lstID, _ACTFLG, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region
#Region "regist user"
    Public Function GetRegisterUser(ByVal _filter As RegisterUserDTO,
                       ByVal PageIndex As Integer,
                       ByVal PageSize As Integer,
                       ByRef Total As Integer,
                       Optional ByVal Sorts As String = "MODIFIED_DATE desc") As List(Of RegisterUserDTO)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetRegisterUser(_filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function UpdateRegisterUser(ByVal _item As List(Of RegisterUserDTO)) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.UpdateRegisterUser(_item, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function InsertRegisterUser(ByVal _item As RegisterUserDTO) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.InsertRegisterUser(_item, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function DeleteRegisterUser(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.DeleteRegisterUser(lstID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function check_dup_reguser(ByVal effect_date As Date, Optional ByVal id As Decimal = 0) As Decimal
        Using rep As New CommonBusinessClient
            Try
                Return rep.check_dup_reguser(effect_date, id)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#End Region
#Region "Function List"

    Public Function GetFunctionList(ByVal _filter As FunctionDTO,
                        ByVal PageIndex As Integer,
                        ByVal PageSize As Integer,
                        ByRef Total As Integer,
                        Optional ByVal Sorts As String = "ORDER_BY asc") As List(Of FunctionDTO)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetFunctionList(_filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateFunctionList(ByVal _validate As FunctionDTO) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.ValidateFunctionList(_validate)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function UpdateFunctionList(ByVal _item As List(Of FunctionDTO)) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.UpdateFunctionList(_item, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function InsertFunctionList(ByVal _item As FunctionDTO) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.InsertFunctionList(_item, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetModuleList() As List(Of ModuleDTO)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetModuleList()
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetFunction(ByVal functionId As String) As FunctionDTO
        Using rep As New CommonBusinessClient
            Try
                If Common.FunctionListDataCache Is Nothing Then
                    Common.FunctionListDataCache = rep.GetFunctionList(New FunctionDTO, 0, -1, -1, "MODIFIED_DATE desc")
                End If
                Return (From p In Common.FunctionListDataCache Where p.FID = functionId).FirstOrDefault
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveFunctions(ByVal lstFunction As List(Of FunctionDTO), ByVal sActive As String) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.ActiveFunctions(lstFunction, sActive, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#End Region

#Region "Group User"

    Public Function GetUserListInGroup(ByVal GroupID As Decimal,
                                        ByVal _filter As UserDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of UserDTO)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetUserListInGroup(GroupID, _filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetUserListInGroup1(ByVal GroupID As Decimal,
                                        ByVal _filter As UserDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of UserDTO)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetUserListInGroup1(GroupID, _filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetUserListOutGroup(ByVal GroupID As Decimal,
                                        ByVal _filter As UserDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of UserDTO)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetUserListOutGroup(GroupID, _filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function InsertUserGroup(ByVal GroupID As System.Decimal, ByVal lst As List(Of Decimal)) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.InsertUserGroup(GroupID, lst, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteUserGroup(ByVal GroupID As System.Decimal, ByVal lst As List(Of Decimal)) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.DeleteUserGroup(GroupID, lst, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "Group Function"

    Public Function GetGroupFunctionPermision(ByVal GroupID As Decimal,
                                                ByVal _filter As GroupFunctionDTO,
                                                ByVal PageIndex As Integer,
                                                ByVal PageSize As Integer,
                                                ByRef Total As Integer,
                                                Optional ByVal Sorts As String = "ORDER_BY asc") As List(Of GroupFunctionDTO)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetGroupFunctionPermision(GroupID, _filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetGroupFunctionNotPermision(ByVal GroupID As Decimal,
                                                    ByVal _filter As FunctionDTO,
                                                    ByVal PageIndex As Integer,
                                                    ByVal PageSize As Integer,
                                                    ByRef Total As Integer,
                                                    Optional ByVal Sorts As String = "ORDER_BY asc") As List(Of FunctionDTO)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetGroupFunctionNotPermision(GroupID, _filter, PageIndex, PageSize, Total, Me.Log, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function InsertGroupFunction(ByVal lst As List(Of GroupFunctionDTO)) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.InsertGroupFunction(lst, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function CopyGroupFunction(ByVal groupCopyID As Decimal, ByVal groupID As Decimal) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.CopyGroupFunction(groupCopyID, groupID, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function UpdateGroupFunction(ByVal lst As List(Of GroupFunctionDTO)) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.UpdateGroupFunction(lst, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteGroupFunction(ByVal lst As List(Of Decimal)) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.DeleteGroupFunction(lst)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetCurMIDByFID(ByVal _fid As String) As String
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetCurMIDByFID(_fid)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "Group Report"
    ''' <summary>
    ''' Lấy danh sách Report đã phân quyền theo nhóm tài khoản
    ''' </summary>
    ''' <param name="_groupID">ID nhóm tài khoản</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetGroupReportList(ByVal _groupID As Decimal) As List(Of GroupReportDTO)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetGroupReportList(_groupID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    ''' <summary>
    ''' Lấy danh sách Report đã phân quyền theo nhóm tài khoản có Filter
    ''' </summary>
    ''' <param name="_groupID">ID nhóm tài khoản</param>
    ''' <param name="_filter">bộ lọc</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetGroupReportListFilter(ByVal _groupID As Decimal, ByVal _filter As GroupReportDTO) As List(Of GroupReportDTO)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetGroupReportListFilter(_groupID, _filter)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    ''' <summary>
    ''' Cập nhập danh sách Report
    ''' </summary>
    ''' <param name="_groupID">ID nhóm tài khoản</param>
    ''' <param name="_lstReport">Danh sách report cần cập nhập</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function UpdateGroupReport(ByVal _groupID As Decimal, ByVal _lstReport As List(Of GroupReportDTO)) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.UpdateGroupReport(_groupID, _lstReport)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "User Function"
    Public Function GetUserFunctionPermisionException(ByVal UserID As Decimal,
                                              ByVal _filter As UserFunctionDTO,
                                              ByVal PageIndex As Integer,
                                              ByVal PageSize As Integer,
                                              ByRef Total As Integer,
                                              Optional ByVal Sorts As String = "ORDER_BY asc") As List(Of UserFunctionDTO)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetUserFunctionPermisionException(UserID, _filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function UpdateUserFunctionException(ByVal lst As List(Of UserFunctionDTO)) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.UpdateUserFunctionException(lst, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function DeleteUserFunctionException(ByVal lst As List(Of Decimal)) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.DeleteUserFunctionException(lst)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetUserFunctionNotPermisionException(ByVal UserID As Decimal,
                                                    ByVal _filter As FunctionDTO,
                                                    ByVal PageIndex As Integer,
                                                    ByVal PageSize As Integer,
                                                    ByRef Total As Integer,
                                                    Optional ByVal Sorts As String = "ORDER_BY asc") As List(Of FunctionDTO)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetUserFunctionNotPermisionException(UserID, _filter, PageIndex, PageSize, Total, Me.Log, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function InsertUserFunctionException(ByVal lst As List(Of UserFunctionDTO), ByVal lstOrg As List(Of Decimal)) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.InsertUserFunctionException(lst, lstOrg, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetUserFunctionPermision(ByVal UserID As Decimal,
                                                ByVal _filter As UserFunctionDTO,
                                                ByVal PageIndex As Integer,
                                                ByVal PageSize As Integer,
                                                ByRef Total As Integer,
                                                Optional ByVal Sorts As String = "ORDER_BY asc") As List(Of UserFunctionDTO)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetUserFunctionPermision(UserID, _filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetUserFunctionNotPermision(ByVal UserID As Decimal,
                                                    ByVal _filter As FunctionDTO,
                                                    ByVal PageIndex As Integer,
                                                    ByVal PageSize As Integer,
                                                    ByRef Total As Integer,
                                                    Optional ByVal Sorts As String = "ORDER_BY asc") As List(Of FunctionDTO)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetUserFunctionNotPermision(UserID, _filter, PageIndex, PageSize, Total, Me.Log, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function InsertUserFunction(ByVal lst As List(Of UserFunctionDTO)) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.InsertUserFunction(lst, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function CopyUserFunction(ByVal UserCopyID As Decimal, ByVal UserID As Decimal) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.CopyUserFunction(UserCopyID, UserID, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function UpdateUserFunction(ByVal lst As List(Of UserFunctionDTO)) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.UpdateUserFunction(lst, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteUserFunction(ByVal lst As List(Of Decimal)) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.DeleteUserFunction(lst)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "User Organization"

    Public Function GetUserOrganization(ByVal UserID As Decimal) As List(Of Decimal)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetUserOrganization(UserID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function UpdateUserOrganization(ByVal OrgIDs As List(Of UserOrgAccessDTO)) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Dim r As Boolean = True
                If Not rep.DeleteUserOrganization(OrgIDs(0).USER_ID) Then Return False
                Return rep.UpdateUserOrganization(OrgIDs)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function GetListOrgImport(ByVal orgID As List(Of System.Decimal)) As List(Of OrganizationDTO)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetListOrgImport(orgID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#End Region

#Region "User Report"
    ''' <summary>
    ''' Lấy danh sách Report đã phân quyền theo nhóm tài khoản
    ''' </summary>
    ''' <param name="_UserID">ID nhóm tài khoản</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetUserReportList(ByVal _UserID As Decimal) As List(Of UserReportDTO)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetUserReportList(_UserID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    ''' <summary>
    ''' Lấy danh sách Report đã phân quyền theo nhóm tài khoản có Filter
    ''' </summary>
    ''' <param name="_UserID">ID nhóm tài khoản</param>
    ''' <param name="_filter">bộ lọc</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetUserReportListFilter(ByVal _UserID As Decimal, ByVal _filter As UserReportDTO) As List(Of UserReportDTO)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetUserReportListFilter(_UserID, _filter)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    ''' <summary>
    ''' Cập nhập danh sách Report
    ''' </summary>
    ''' <param name="_UserID">ID nhóm tài khoản</param>
    ''' <param name="_lstReport">Danh sách report cần cập nhập</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function UpdateUserReport(ByVal _UserID As Decimal, ByVal _lstReport As List(Of UserReportDTO)) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.UpdateUserReport(_UserID, _lstReport)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "Employee"
    Public Function GetEmployeeSignToPopupFind(ByVal _filter As EmployeePopupFindListDTO,
                                           ByVal PageIndex As Integer,
                                           ByVal PageSize As Integer,
                                           ByRef Total As Integer,
                                           Optional ByVal Sorts As String = "EMPLOYEE_CODE asc",
                                           Optional ByVal _param As ParamDTO = Nothing) As List(Of EmployeePopupFindListDTO)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetEmployeeSignToPopupFind(_filter, PageIndex, PageSize, Total, Sorts, Log, _param)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetEmployeeSignToPopupFind_V2(ByVal _filter As EmployeePopupFindListDTO,
                                                  ByVal PageIndex As Integer,
                                                  ByVal PageSize As Integer,
                                                  ByRef Total As Integer,
                                                  Optional ByVal Sorts As String = "EMPLOYEE_CODE asc",
                                                  Optional ByVal _param As ParamDTO = Nothing) As List(Of EmployeePopupFindListDTO)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetEmployeeSignToPopupFind_V2(_filter, PageIndex, PageSize, Total, Sorts, Log, _param)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetEmployeeFind(_filter As EmployeePopupFindListDTO,
                                            ByRef Total As Integer,
                                            Optional ByVal Sorts As String = "EMPLOYEE_CODE asc",
                                            Optional ByVal _param As ParamDTO = Nothing) As List(Of EmployeePopupFindListDTO)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetEmployeeFind(_filter, Total, Sorts, Log, _param)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function GetEmployeeToPopupFind(_filter As EmployeePopupFindListDTO,
                                            ByVal PageIndex As Integer,
                                            ByVal PageSize As Integer,
                                            ByRef Total As Integer,
                                            Optional ByVal Sorts As String = "EMPLOYEE_CODE asc",
                                            Optional ByVal _param As ParamDTO = Nothing) As List(Of EmployeePopupFindListDTO)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetEmployeeToPopupFind(_filter, PageIndex, PageSize, Total, Sorts, Log, _param)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetEmployeeToPopupFind2(ByVal _filter As EmployeePopupFindListDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        ByVal request_id As Integer,
                                        Optional ByVal Sorts As String = "EMPLOYEE_CODE asc",
                                        Optional ByVal _param As ParamDTO = Nothing) As List(Of EmployeePopupFindListDTO)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetEmployeeToPopupFind2(_filter, PageIndex, PageSize, Total, request_id, Sorts, Log, _param)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function GetEmployeeToPopupFind_EmployeeID(ByVal _empId As List(Of Decimal)) As List(Of EmployeePopupFindDTO)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetEmployeeToPopupFind_EmployeeID(_empId)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetEmployeeID(ByVal _empId As Decimal) As EmployeePopupFindDTO
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetEmployeeID(_empId)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
#End Region

#Region "Title"

    Public Function GetTitle(ByVal Filter As TitleDTO,
                                ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer,
                                Optional ByVal Sorts As String = "NAME_VN asc") As List(Of TitleDTO)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetTitle(Filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetTitleFromId(ByVal _lstIds As List(Of Decimal)) As List(Of TitleDTO)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetTitleFromId(_lstIds)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "AccessLog"

    Public Function GetAccessLog(ByVal filter As AccessLogFilter,
                                 ByVal PageIndex As Integer,
                                 ByVal PageSize As Integer,
                                 ByRef Total As Integer,
                                 Optional ByVal Sorts As String = "LoginDate desc") As List(Of AccessLog)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetAccessLog(filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using


    End Function

    Public Function GetAccessLog(ByVal filter As AccessLogFilter,
                                 Optional ByVal Sorts As String = "LoginDate desc") As List(Of AccessLog)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetAccessLog(filter, 0, Integer.MaxValue, 0, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using


    End Function

    Public Function InsertAccessLog(ByVal _accesslog As AccessLog) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.InsertAccessLog(_accesslog)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "ActionLog"

    Public Function GetActionLog(ByVal filter As ActionLogFilter,
                                 ByVal PageIndex As Integer,
                                 ByVal PageSize As Integer,
                                 ByRef Total As Integer,
                                 Optional ByVal Sorts As String = "ActionDate desc") As List(Of ActionLog)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetActionLog(filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetActionLog(ByVal filter As ActionLogFilter,
                                 Optional ByVal Sorts As String = "ActionDate desc") As List(Of ActionLog)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetActionLog(filter, 0, Integer.MaxValue, 0, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetActionLogByObjectId(ByVal ObjectId As Decimal) As List(Of ActionLog)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetActionLogByObjectId(ObjectId)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetActionLogByID(ByVal gID As Decimal) As List(Of AuditLogDtl)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetActionLogByID(gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteActionLogs(ByVal lstDeleteIds As List(Of Decimal)) As Integer
        Using rep As New CommonBusinessClient
            Try
                Return rep.DeleteActionLogs(lstDeleteIds)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using


    End Function

#End Region

#Region "MailTemplate"
    Public Function GetMailTemplate(ByVal _filter As MailTemplateDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of MailTemplateDTO)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetMailTemplate(_filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function GetMailTemplate(ByVal _filter As MailTemplateDTO,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of MailTemplateDTO)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetMailTemplate(_filter, 0, Integer.MaxValue, 0, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function GetMailTemplateBaseCode(ByVal code As String, ByVal group As String) As MailTemplateDTO
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetMailTemplateBaseCode(code, group)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function InsertMailTemplate(ByVal mailTemplate As MailTemplateDTO) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.InsertMailTemplate(mailTemplate, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function ModifyMailTemplate(ByVal mailTemplate As MailTemplateDTO) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.ModifyMailTemplate(mailTemplate, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function DeleteMailTemplate(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.DeleteMailTemplate(lstID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function CheckValidEmailTemplate(ByVal code As String, ByVal group As String) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.CheckValidEmailTemplate(code, group)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
#End Region
#Region "SE_SYSTEM_MAINTAIN"
    Public Function GetSystem_Maintain(ByVal _filter As SE_SYSTEM_MAINTAINDTO,
                       ByVal PageIndex As Integer,
                       ByVal PageSize As Integer,
                       ByRef Total As Integer,
                       Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of SE_SYSTEM_MAINTAINDTO)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetSystem_Maintain(_filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function ModifySystem_Maintain(ByVal lstObj As SE_SYSTEM_MAINTAINDTO) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.ModifySystem_Maintain(lstObj, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function InsertSystem_Maintain(ByVal _item As SE_SYSTEM_MAINTAINDTO) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.InsertSystem_Maintain(_item, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function CHECK_DUP_SYSTEM_MAINTAIN(ByVal effect_date As Date, Optional ByVal id As Decimal = 0) As Decimal
        Using rep As New CommonBusinessClient
            Try
                Return rep.CHECK_DUP_SYSTEM_MAINTAIN(effect_date, id)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function CHECK_SYSTEM_MAINTAIN() As String
        Using rep As New CommonBusinessClient
            Try
                Return rep.CHECK_SYSTEM_MAINTAIN()
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function GET_SYSTEM_MAINTAIN_IS_END() As String
        Using rep As New CommonBusinessClient
            Try
                Return rep.GET_SYSTEM_MAINTAIN_IS_END()
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function CHECK_SYSTEM_MAINTAIN_IS_END() As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return True
                Return rep.CHECK_SYSTEM_MAINTAIN_IS_END()
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function GetSE_BACKGROUND_PORTALList() As List(Of SE_BACKGROUND_PORTALDTO)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetSE_BACKGROUND_PORTALList()
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function GET_SE_BACKGROUND_PORTAL() As String
        Using rep As New CommonBusinessClient
            Try
                Return rep.GET_SE_BACKGROUND_PORTAL()
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function DeleteSE_BACKGROUND_PORTAL(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.DeleteSE_BACKGROUND_PORTAL(lstID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
#End Region
#Region "Title"
    Public Function GetTrainningRequest(ByVal _filter As RequestDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of RequestDTO)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetTrainningRequest(_filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
#End Region

#Region "Dashboard"
    Public Function SendMail_ctrlDashboardHome(ByVal lstDTO As List(Of SEMailDTO)) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.SendMail_ctrlDashboardHome(lstDTO)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
#End Region

End Class
