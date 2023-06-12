Imports System.Net
Imports System.Reflection
Imports System.Text
Imports Framework.Data
Imports Framework.Data.DataAccess
Imports Framework.Data.System.Linq.Dynamic
Imports Framework.Data.SystemConfig
Imports Oracle.DataAccess.Client
Partial Public Class CommonRepository
    Public Function GetPositionToPopupFind(ByVal _filter As PositionPopupFindListDTO,
                                           ByVal PageIndex As Integer,
                                           ByVal PageSize As Integer,
                                           ByRef Total As Integer,
                                           Optional ByVal Sorts As String = "ID DESC",
                                           Optional ByVal log As UserLog = Nothing,
                                           Optional ByVal _param As ParamDTO = Nothing) As List(Of PositionPopupFindListDTO)

        Try
            Dim userName As String
            Using cls As New DataAccess.QueryData
                userName = log.Username.ToUpper
                If _filter.LoadAllOrganization Then
                    userName = "ADMIN"
                End If
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = userName,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using



            Dim query = (From p In Context.HU_TITLE
                         From lm In Context.HU_TITLE.Where(Function(f) f.ID = p.LM).DefaultIfEmpty
                         From csm In Context.HU_TITLE.Where(Function(f) f.ID = p.CSM).DefaultIfEmpty
                         From emp_lm In Context.HU_EMPLOYEE.Where(Function(f) f.ID = lm.MASTER).DefaultIfEmpty()
                         From emp_csm In Context.HU_EMPLOYEE.Where(Function(f) f.ID = csm.MASTER).DefaultIfEmpty()
                         From master In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.MASTER).DefaultIfEmpty()
                         From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                         From k In Context.SE_CHOSEN_ORG.Where(Function(f) p.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = userName)
                         Select New PositionPopupFindListDTO With {
                                                    .ID = p.ID,
                                                    .POSITION_CODE = p.CODE,
                                                    .EMP_CSM = master.EMPLOYEE_CODE & " - " & master.FULLNAME_VN,
                                                    .EMP_LM = emp_lm.EMPLOYEE_CODE & " - " & emp_lm.FULLNAME_VN,
                                                    .FULLNAME_VN = p.NAME_VN,
                                                    .FULLNAME_EN = p.NAME_EN,
                                                    .IS_NONPHYSICAL = If(p.IS_NONPHYSICAL IsNot Nothing AndAlso p.IS_NONPHYSICAL = -1, -1, 0),
                                                    .IS_UYBAN = If(o.UY_BAN IsNot Nothing AndAlso o.UY_BAN = -1, -1, 0)})

            Dim lst = query

            If _filter.IS_UYBAN Then
                lst = lst.Where(Function(p) p.IS_UYBAN = -1 And p.IS_NONPHYSICAL = -1)
            Else
                lst = lst.Where(Function(p) p.IS_UYBAN = 0 And p.IS_NONPHYSICAL = 0)
            End If
            If _filter.POSITION_CODE <> "" Then
                lst = lst.Where(Function(p) p.POSITION_CODE.ToUpper.Contains(_filter.POSITION_CODE.ToUpper))
            End If
            If _filter.FULLNAME_VN <> "" Then
                lst = lst.Where(Function(p) p.FULLNAME_VN.ToUpper.Contains(_filter.FULLNAME_VN.ToUpper))
            End If
            If _filter.FULLNAME_EN <> "" Then
                lst = lst.Where(Function(p) p.FULLNAME_EN.ToUpper.Contains(_filter.FULLNAME_EN.ToUpper))
            End If
            If _filter.EMP_CSM <> "" Then
                lst = lst.Where(Function(p) p.EMP_CSM.ToUpper.Contains(_filter.EMP_CSM.ToUpper))
            End If


            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList
        Catch ex As Exception
            Throw ex
        End Try

    End Function
#Region "Group Organization"
    Public Function GetGroupOrganization(ByVal _groupID As Decimal) As List(Of Decimal)
        Return (From p In Context.SE_GROUP_ORG_ACCESS Where p.GROUP_ID = _groupID Select p.ORG_ID).ToList
    End Function
    Public Function GetGroupOrganizationFunction(ByVal _groupID As Decimal) As List(Of Decimal)
        Return (From p In Context.SE_GROUP_ORG_FUN_ACCESS Where p.GROUP_ID = _groupID Select p.ORG_ID).ToList
    End Function
    Public Function DeleteGroupOrganization(ByVal _groupId As Decimal)
        Try
            Dim query As List(Of SE_GROUP_ORG_ACCESS) = (From p In Context.SE_GROUP_ORG_ACCESS
                                                         Where p.GROUP_ID = _groupId Select p).ToList
            For i = 0 To query.Count - 1
                Context.SE_GROUP_ORG_ACCESS.DeleteObject(query(i))
            Next
            Context.SaveChanges()

            Using cls As New DataAccess.NonQueryData
                cls.ExecuteSQL("DELETE SE_USER_ORG_ACCESS WHERE GROUP_ID =" & _groupId)
            End Using
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function UpdateGroupOrganization(ByVal _lstOrg As List(Of GroupOrgAccessDTO)) As Boolean
        If _lstOrg.Count > 0 Then
            Dim _groupId As Decimal
            Dim i As Integer
            For i = 0 To _lstOrg.Count - 1
                Dim _item As New SE_GROUP_ORG_ACCESS
                _item.ID = Utilities.GetNextSequence(Context, Context.SE_GROUP_ORG_ACCESS.EntitySet.Name)
                _item.GROUP_ID = _lstOrg(i).GROUP_ID
                _groupId = _lstOrg(i).GROUP_ID
                _item.ORG_ID = _lstOrg(i).ORG_ID
                Context.SE_GROUP_ORG_ACCESS.AddObject(_item)
            Next

            Dim lstEmp = (From p In Context.HUV_SE_GRP_SE_USR
                          Where p.SE_GROUPS_ID = _groupId
                          Select p).ToList()

            If lstEmp.Count > 0 Then
                For Each j As HUV_SE_GRP_SE_USR In lstEmp
                    For Each _org As GroupOrgAccessDTO In _lstOrg
                        Dim ckOrg = (From p In Context.SE_USER_ORG_ACCESS
                                     Where (p.USER_ID = j.SE_USERS_ID) And (p.ORG_ID = _org.ORG_ID)
                                     Select p).ToList()
                        If ckOrg.Count <= 0 Then
                            Dim _jtem As New SE_USER_ORG_ACCESS
                            _jtem.ID = Utilities.GetNextSequence(Context, Context.SE_USER_ORG_ACCESS.EntitySet.Name)
                            _jtem.ORG_ID = _org.ORG_ID
                            _jtem.USER_ID = j.SE_USERS_ID
                            _jtem.GROUP_ID = _groupId
                            Context.SE_USER_ORG_ACCESS.AddObject(_jtem)
                        End If
                    Next
                Next
            End If
            Context.SaveChanges()
            Return True
        Else
            Return False
        End If
    End Function
    Public Function UpdateGroupOrganizationFunction(ByVal _lstOrg As List(Of GroupOrgFunAccessDTO)) As Boolean
        'Delete All
        If _lstOrg.Count > 0 Then
            Dim GroupID As Decimal = _lstOrg(0).GROUP_ID
            Dim i As Integer
            Dim query As List(Of SE_GROUP_ORG_FUN_ACCESS) = (From p In Context.SE_GROUP_ORG_FUN_ACCESS
                                                             Where p.GROUP_ID = GroupID Select p).ToList
            For i = 0 To query.Count - 1
                Context.SE_GROUP_ORG_FUN_ACCESS.DeleteObject(query(i))
            Next
            Context.SaveChanges()
            For i = 0 To _lstOrg.Count - 1
                Dim _item As New SE_GROUP_ORG_FUN_ACCESS
                _item.ID = Utilities.GetNextSequence(Context, Context.SE_GROUP_ORG_FUN_ACCESS.EntitySet.Name)
                _item.GROUP_ID = _lstOrg(i).GROUP_ID
                _item.ORG_ID = _lstOrg(i).ORG_ID
                Context.SE_GROUP_ORG_FUN_ACCESS.AddObject(_item)
            Next
            Context.SaveChanges()
            Return True
        Else
            Return False
        End If
    End Function
#End Region
#Region "Case config"
    Public Function GetCaseConfigByID(ByVal codename As String, ByVal codecase As String) As Integer
        Try
            Using cls As New DataAccess.QueryData
                Dim obj = New With {.P_CODE_NAME = codename,
                                    .P_CODE_CASE = codecase,
                                    .P_OUT = cls.OUT_NUMBER}
                cls.ExecuteStore("PKG_COMMON_LIST.GET_SE_CASE_CONFIG", obj)
                Return Integer.Parse(obj.P_OUT)
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GET_VALUE_SE_CONFIG(ByVal P_EMP_CODE As String) As String
        Try
            Using cls As New DataAccess.QueryData
                Dim obj = New With {.P_CODENAME = P_EMP_CODE,
                                    .P_OUT = cls.OUT_STRING}
                cls.ExecuteStore("PKG_COMMON_LIST.GET_VALUE_SE_CONFIG", obj)
                Return obj.P_OUT
            End Using
        Catch ex As Exception
            Throw ex
        End Try
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

        Dim query = (From p In Context.SE_FUNCTION
                     From func In Context.SE_VIEW_CONFIG.Where(Function(f) f.CODE_NAME.ToUpper = p.FID.ToUpper)
                     Select New FunctionDTO With {
                        .ID = p.ID,
                        .NAME = p.NAME,
                        .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Không áp dụng"),
                        .FID = p.FID,
                        .FUNCTION_GROUP_ID = p.GROUP_ID
                    })
        If _filter.FID <> "" Then
            query = query.Where(Function(p) p.FID.ToUpper.Contains(_filter.FID.ToUpper))
        End If
        If _filter.NAME <> "" Then
            query = query.Where(Function(p) p.NAME.ToUpper.Contains(_filter.NAME.ToUpper))
        End If
        If _filter.FUNCTION_GROUP_ID <> 0 Then
            query = query.Where(Function(p) p.FUNCTION_GROUP_ID = _filter.FUNCTION_GROUP_ID)
        End If
        If _filter.ACTFLG <> "" Then
            query = query.Where(Function(p) p.ACTFLG = _filter.ACTFLG)
        End If

        If PageSize <> -1 Then
            query = query.OrderBy(Sorts)
            Total = query.Count
            query = query.Skip(PageIndex * PageSize).Take(PageSize)
        End If
        Dim result = (From p In query Select p)
        Return result.ToList
    End Function

    Public Function Insert_Update_Control_List(ByVal _id As String, ByVal Config As String, ByVal log As UserLog) As Boolean
        Dim objSe_ViewDataNew As New SE_VIEW_CONFIG
        Try
            Dim objSe_ViewData As SE_VIEW_CONFIG = (From p In Context.SE_VIEW_CONFIG Where p.CODE_NAME = _id).FirstOrDefault
            If objSe_ViewData IsNot Nothing Then
                Context.SE_VIEW_CONFIG.DeleteObject(objSe_ViewData)
                objSe_ViewDataNew.CODE_NAME = _id
                objSe_ViewDataNew.CONFIG_DATA = Config
                Context.SE_VIEW_CONFIG.AddObject(objSe_ViewDataNew)
            Else
                objSe_ViewDataNew.CODE_NAME = _id
                objSe_ViewDataNew.CONFIG_DATA = Config
                Context.SE_VIEW_CONFIG.AddObject(objSe_ViewDataNew)
            End If
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "User List"

    Public Function GetUser(ByVal _filter As UserDTO,
                            ByVal PageIndex As Integer,
                            ByVal PageSize As Integer,
                            ByRef Total As Integer,
                            Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of UserDTO)

        Dim query = (From p In Context.SE_USER
                     From E In Context.HU_EMPLOYEE.Where(Function(F) F.ID = p.EMPLOYEE_ID).DefaultIfEmpty
                     From O In Context.HUV_ORGANIZATION.Where(Function(F) F.ID = E.ORG_ID).DefaultIfEmpty
                     Where (From n In p.SE_GROUPS Where n.IS_ADMIN = True).Count = 0 And p.MODULE_ADMIN Is Nothing And p.EFFECTDATE_TERMINATION Is Nothing
                     Select New UserDTO With {
                        .ID = p.ID,
                        .EMAIL = p.EMAIL,
                        .PASSWORD = p.PASSWORD,
                        .TELEPHONE = p.TELEPHONE,
                        .USERNAME = p.USERNAME,
                        .FULLNAME = p.FULLNAME,
                        .IS_APP = p.IS_APP,
                        .IS_PORTAL = p.IS_PORTAL,
                        .IS_AD = p.IS_AD,
                        .ACTFLG = p.ACTFLG,
                        .EMPLOYEE_CODE = p.EMPLOYEE_CODE,
                        .EFFECT_DATE = p.EFFECT_DATE,
                        .EXPIRE_DATE = p.EXPIRE_DATE,
                         .ORG_ID = O.ORG_ID2,
                         .ORG_NAME = O.ORG_CODE2,
                        .CREATED_DATE = p.CREATED_DATE,
                        .IS_LOGIN = p.IS_LOGIN,
                        .IS_RC = p.IS_RC})

        If _filter.USERNAME <> "" Then
            query = query.Where(Function(p) p.USERNAME.ToUpper.Contains(_filter.USERNAME.ToUpper))
        End If
        If _filter.FULLNAME <> "" Then
            query = query.Where(Function(p) p.FULLNAME.ToUpper.Contains(_filter.FULLNAME.ToUpper))
        End If
        If _filter.EMPLOYEE_CODE <> "" Then
            query = query.Where(Function(p) p.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
        End If
        If _filter.IS_APP IsNot Nothing Then
            Dim is_short = If(_filter.IS_APP, 1, 0)
            query = query.Where(Function(p) p.IS_APP = is_short)
        End If
        If _filter.IS_PORTAL IsNot Nothing Then
            Dim is_short = If(_filter.IS_PORTAL, 1, 0)
            query = query.Where(Function(p) p.IS_PORTAL = is_short)
        End If
        If _filter.IS_AD IsNot Nothing Then
            Dim is_short = If(_filter.IS_AD, 1, 0)
            query = query.Where(Function(p) p.IS_AD = is_short)
        End If
        If _filter.ACTFLG <> "" Then
            query = query.Where(Function(p) p.ACTFLG = _filter.ACTFLG)
        End If

        query = query.OrderBy(Sorts)
        Total = query.Count
        query = query.Skip(PageIndex * PageSize).Take(PageSize)

        Return query.ToList
    End Function
    Public Function GetUserList(ByVal _filter As UserDTO,
                            ByVal PageIndex As Integer,
                            ByVal PageSize As Integer,
                            ByRef Total As Integer,
                            Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of UserDTO)

        Dim query = (From p In Context.SE_USER
                     From k In p.SE_GROUPS.DefaultIfEmpty
                     Where ((From n In p.SE_GROUPS Where n.IS_ADMIN = True).Count = 0 _
                     Or (From n In p.SE_GROUPS Where n.IS_ADMIN = -1 And n.IS_HR_ADMIN = -1).Count <> 0) And p.MODULE_ADMIN Is Nothing
                     Select New UserDTO With {
                        .ID = p.ID,
                        .EFFECTDATE_TERMINATION = p.EFFECTDATE_TERMINATION,
                        .EMAIL = p.EMAIL,
                        .PASSWORD = p.PASSWORD,
                        .TELEPHONE = p.TELEPHONE,
                        .USERNAME = If(String.IsNullOrEmpty(p.USERNAME), String.Empty, p.USERNAME),
                        .FULLNAME = p.FULLNAME,
                        .IS_APP = p.IS_APP,
                        .IS_PORTAL = p.IS_PORTAL,
                        .IS_AD = p.IS_AD,
                        .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                        .EMPLOYEE_CODE = p.EMPLOYEE_CODE,
                        .EMPLOYEE_ID = p.EMPLOYEE_ID,
                        .EFFECT_DATE = p.EFFECT_DATE,
                        .EXPIRE_DATE = p.EXPIRE_DATE,
                        .CREATED_DATE = p.CREATED_DATE,
                        .IS_LOGIN = p.IS_LOGIN,
                         .IS_HR = p.IS_HR,
                        .IS_RC = p.IS_RC})

        If _filter.USERNAME <> "" Then
            query = query.Where(Function(p) p.USERNAME.ToUpper.Contains(_filter.USERNAME.ToUpper))
        End If
        If _filter.FULLNAME <> "" Then
            query = query.Where(Function(p) p.FULLNAME.ToUpper.Contains(_filter.FULLNAME.ToUpper))
        End If
        If _filter.EMPLOYEE_CODE <> "" Then
            query = query.Where(Function(p) p.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
        End If
        If _filter.TELEPHONE <> "" Then
            query = query.Where(Function(p) p.TELEPHONE.ToUpper.Contains(_filter.TELEPHONE.ToUpper))
        End If
        If _filter.EMAIL <> "" Then
            query = query.Where(Function(p) p.EMAIL.ToUpper.Contains(_filter.EMAIL.ToUpper))
        End If
        'If _filter.EFFECT_DATE IsNot Nothing Then
        '    query = query.Where(Function(p) p.EFFECT_DATE = _filter.EFFECT_DATE)
        'End If
        'If _filter.EXPIRE_DATE IsNot Nothing Then
        '    query = query.Where(Function(p) p.EXPIRE_DATE = _filter.EXPIRE_DATE)
        'End If
        'If _filter.IS_AD = True Then
        '    'Dim is_short = If(_filter.IS_APP, 1, 0)
        '    query = query.Where(Function(p) p.IS_APP = _filter.IS_APP)
        'End If
        'If _filter.IS_PORTAL = True Then
        '    'Dim is_short = If(_filter.IS_PORTAL, 1, 0)
        '    query = query.Where(Function(p) p.IS_PORTAL = _filter.IS_PORTAL)
        'End If
        'If _filter.IS_AD = True Then
        '    'Dim is_short = If(_filter.IS_AD, 1, 0)
        '    query = query.Where(Function(p) p.IS_AD = _filter.IS_AD)
        'End If
        If _filter.ACTFLG <> "" Then
            query = query.Where(Function(p) p.ACTFLG.ToUpper.Contains(_filter.ACTFLG.ToUpper))
        End If
        'query = query.Where(Function(p) p.MODULE_ADMIN.ToUpper.Contains(String.Empty))
        query = query.Distinct
        query = query.OrderBy(Sorts)
        Total = query.Count
        query = query.Skip(PageIndex * PageSize).Take(PageSize)

        Dim lstTemp = query.ToList
        For Each obj In lstTemp
            obj.ORG_NAME = String.Join("<br/>", (From p In Context.SE_USER
                                                 From n In p.SE_GROUPS
                                                 Where p.USERNAME = obj.USERNAME
                                                 Select n.NAME).ToArray)
        Next
        Dim a As List(Of UserDTO) = lstTemp.ToList
        If _filter.ORG_NAME <> "" Then
            a = a.Where(Function(p) p.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
        End If

        Return a.ToList
    End Function

    Public Function ValidateUser(ByVal _validate As UserDTO) As Boolean
        Dim query
        If _validate.USERNAME <> "" Then
            query = (From p In Context.SE_USER Where p.USERNAME.ToUpper = _validate.USERNAME.ToUpper).FirstOrDefault
            Return (query Is Nothing)
        End If
        If _validate.EMAIL <> "" Then
            query = (From p In Context.SE_USER Where p.EMAIL.ToUpper = _validate.EMAIL.ToUpper).FirstOrDefault
            Return (query Is Nothing)
        End If
        If _validate.EMPLOYEE_CODE <> "" Then
            query = (From p In Context.HU_EMPLOYEE
                     Where p.EMPLOYEE_CODE.ToUpper = _validate.EMPLOYEE_CODE.ToUpper
                     Select p.EMPLOYEE_CODE).FirstOrDefault
            Return (query IsNot Nothing)
        End If
        Return True
    End Function

    Public Function InsertUser(ByVal _user As UserDTO, ByVal log As UserLog) As Boolean
        Dim objUserData As New SE_USER
        Try
            objUserData.ID = Utilities.GetNextSequence(Context, Context.SE_USER.EntitySet.Name)
            objUserData.PASSWORD = _user.PASSWORD
            objUserData.EMAIL = _user.EMAIL
            objUserData.TELEPHONE = _user.TELEPHONE
            objUserData.USERNAME = _user.USERNAME
            objUserData.FULLNAME = _user.FULLNAME
            objUserData.IS_APP = _user.IS_APP
            objUserData.IS_PORTAL = _user.IS_PORTAL
            objUserData.IS_AD = _user.IS_AD
            objUserData.ACTFLG = "A"
            objUserData.IS_CHANGE_PASS = _user.IS_CHANGE_PASS
            objUserData.EMPLOYEE_CODE = _user.EMPLOYEE_CODE
            objUserData.EMPLOYEE_ID = _user.EMPLOYEE_ID
            objUserData.IS_LOGIN = _user.IS_LOGIN
            objUserData.IS_RC = _user.IS_RC
            objUserData.IS_HR = CType(_user.IS_HR, Integer)
            'objUserData.GROUP_USER_ID = _user.GROUP_USER_ID
            If _user.EFFECT_DATE IsNot Nothing Then
                objUserData.EFFECT_DATE = _user.EFFECT_DATE
            End If
            If _user.EXPIRE_DATE IsNot Nothing Then
                objUserData.EXPIRE_DATE = _user.EXPIRE_DATE
            End If

            Context.SE_USER.AddObject(objUserData)
            Context.SaveChanges(log)

            For Each grpID In _user.GROUP_USER_ID
                Using cls As New DataAccess.QueryData
                    Dim obj = New With {
                   .USER_ID = objUserData.ID,
                   .GROUP_ID = grpID,
                   .P_OUT = cls.OUT_NUMBER
                    }

                    cls.ExecuteStore("PKG_HCM_SYSTEM.INSERT_SE_USER", obj)
                End Using
            Next

            Return True
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function ModifyUser(ByVal _user As UserDTO, ByVal log As UserLog) As Boolean
        Dim intSuccess As Integer
        'Try
        '    Dim objUserData As SE_USER = (From p In Context.SE_USER Where p.ID = _user.ID).FirstOrDefault
        '    If objUserData IsNot Nothing Then
        '        objUserData.PASSWORD = _user.PASSWORD
        '        objUserData.EMAIL = _user.EMAIL
        '        objUserData.TELEPHONE = _user.TELEPHONE
        '        objUserData.USERNAME = _user.USERNAME
        '        objUserData.FULLNAME = _user.FULLNAME
        '        objUserData.IS_APP = _user.IS_APP
        '        objUserData.IS_PORTAL = _user.IS_PORTAL
        '        objUserData.IS_AD = _user.IS_AD
        '        objUserData.EMPLOYEE_CODE = _user.EMPLOYEE_CODE
        '        objUserData.EMPLOYEE_ID = _user.EMPLOYEE_ID
        '        objUserData.EFFECT_DATE = _user.EFFECT_DATE
        '        objUserData.EXPIRE_DATE = _user.EXPIRE_DATE
        '        objUserData.MODIFIED_DATE = DateTime.Now
        '        objUserData.MODIFIED_BY = log.Username
        '        objUserData.MODIFIED_LOG = log.ComputerName
        '        Context.SaveChanges(log)
        '    Else
        '        Return False
        '    End If
        '    Return True
        'Catch ex As Exception
        '    Throw ex
        'End Try

        Using cls As New DataAccess.QueryData
            Try
                Dim obj = New With {
                   .P_PASSWORD = _user.PASSWORD,
                   .P_EMAIL = _user.EMAIL,
                   .P_TELEPHONE = _user.TELEPHONE,
                   .P_USERNAME = _user.USERNAME,
                   .P_IS_APP = _user.IS_APP,
                   .P_IS_PORTAL = _user.IS_PORTAL,
                   .P_IS_AD = _user.IS_AD,
                   .P_EMPLOYEE_CODE = _user.EMPLOYEE_CODE,
                   .P_EMPLOYEE_ID = _user.EMPLOYEE_ID,
                   .P_EFFECT_DATE = _user.EFFECT_DATE,
                   .P_EXPIRE_DATE = _user.EXPIRE_DATE,
                   .P_MODIFIED_DATE = DateTime.Now,
                   .P_MODIFIED_BY = log.Username,
                   .P_MODIFIED_LOG = log.ComputerName,
                   .P_USERID = _user.ID,
                   .P_GROUP_USER_ID = _user.GROUP_USER_ID,
                   .P_IS_LOGIN = _user.IS_LOGIN,
                   .P_IS_RC = _user.IS_RC,
                   .P_IS_HR = _user.IS_HR,
                   .P_OUT = cls.OUT_NUMBER
               }

                cls.ExecuteStore("PKG_HCM_SYSTEM.UPDATE_SE_USER1", obj)
                intSuccess = CType(obj.P_OUT, Integer)
            Catch ex As Exception
                Throw ex
            End Try

            Return intSuccess = 1
        End Using
    End Function

    Public Function DeleteUser(ByVal _lstUserID As List(Of Decimal), ByRef _error As String, ByVal log As UserLog) As Boolean
        Dim isDeleted As Boolean = False
        Dim intDelete As Integer = 0
        Dim countDelete As Integer = 0
        Dim cls As New QueryData
        Try
            'Dim lstIDUser As List(Of SE_USER) = (From p In Context.SE_USER Where _lstUserID.Contains(p.ID) Select p).ToList
            'If lstIDUser IsNot Nothing Then
            '    For index As Integer = 0 To lstIDUser.Count - 1
            '        Dim userid As Decimal = lstIDUser(index).ID
            '        If lstIDUser(index).SE_GROUPS.Count > 0 Then
            '            _error = "MESSAGE_DELETE_DATA_USED"
            '            Return False
            '        End If
            '        If (From p In Context.SE_USER_ORG_ACCESS
            '            Where userid = p.USER_ID).Count Then
            '            _error = "MESSAGE_DELETE_DATA_USED"
            '            Return False
            '        End If
            '        If (From p In Context.SE_USER_PERMISSION
            '            Where userid = p.USER_ID).Count Then
            '            _error = "MESSAGE_DELETE_DATA_USED"
            '            Return False
            '        End If
            '        Context.ExecuteStoreCommand("DELETE FROM SE_USER WHERE ID = {0}", userid)
            '    Next
            'Context.SaveChanges(log)

            For Each userId As Decimal In _lstUserID
                Dim query = (From p In Context.HUV_SE_GRP_SE_USR Where p.SE_USERS_ID = userId)

                ' kiểm tra xem user có trong nhóm user 
                If query.Count > 0 Then
                    _error = "MESSAGE_DELETE_DATA_USED"
                    Exit For
                End If

                ' kiểm tra xem đơn vị phòng ban được truy cập theo user
                Dim query2 = (From p In Context.SE_USER_ORG_ACCESS Where p.USER_ID = userId)

                If query2.Count > 0 Then
                    _error = "MESSAGE_DELETE_DATA_USED"
                    Exit For
                End If

                ' kiểm tra user có phân quyền
                Dim query1 = (From p In Context.SE_USER_PERMISSION Where p.USER_ID = userId)

                If query1.Count > 0 Then
                    _error = "MESSAGE_DELETE_DATA_USED"
                    Exit For
                End If
                Dim obj = New With {
                             .P_USER_ID = userId,
                            .P_OUT = cls.OUT_NUMBER
                    }
                ' thực thi delete
                cls.ExecuteStore("PKG_HCM_SYSTEM.DEL_SE_USER", obj)
                intDelete = CType(obj.P_OUT, Integer)
                If intDelete > 0 Then
                    countDelete = countDelete + 1
                End If
            Next

            If countDelete > 0 Then
                isDeleted = True
            End If

        Catch ex As Exception
            Throw ex
        End Try

        Return isDeleted
    End Function
    'aa
    Public Function UpdateUserListStatus(ByVal _lstUserID As List(Of Decimal), ByVal _status As String, ByVal log As UserLog) As Boolean
        Try
            'Dim lstIDUser As List(Of SE_USER) = (From p In Context.SE_USER Where _lstUserID.Contains(p.ID) Select p).ToList
            'If lstIDUser IsNot Nothing Then
            For index As Integer = 0 To _lstUserID.Count - 1
                Dim _user = New SE_USER With {.ID = _lstUserID(index)}
                Context.SE_USER.Attach(_user)
                _user.ACTFLG = _status
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function SyncUserList(ByRef _newUser As String,
                                 ByRef _modifyUser As String,
                                 ByRef _deleteUser As String,
                                 ByVal log As UserLog) As Boolean
        Try
            _newUser = ""
            _modifyUser = ""
            _deleteUser = ""
            Dim idTer As Decimal = CommonCommon.OT_WORK_STATUS.TERMINATE_ID
            'Kiểm tra nhân viên mới
            Dim lstUser As List(Of UserDTO) = (From p In Context.SE_USER
                                               Where p.EMPLOYEE_ID IsNot Nothing And p.USERNAME.ToUpper <> "ADMIN"
                                               Select New UserDTO With {
                                                   .ID = p.ID,
                                                   .EMPLOYEE_ID = p.EMPLOYEE_ID,
                                                   .USERNAME = p.USERNAME,
                                                   .FULLNAME = p.FULLNAME,
                                                   .TELEPHONE = p.TELEPHONE,
                                                   .EMAIL = p.EMAIL,
                                                   .EMPLOYEE_CODE = p.EMPLOYEE_CODE,
                                                   .MODULE_ADMIN = p.MODULE_ADMIN,
                                                   .ACTFLG = p.ACTFLG}).ToList

            'anhvn, 20201014
            Dim lst As List(Of Decimal?) = (From p In lstUser Select p.EMPLOYEE_ID).ToList

            Dim lstNew As List(Of UserDTO) = (From p In Context.HU_EMPLOYEE
                                              From cv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = p.ID)
                                              Where Not lst.Contains(p.ID) And
                                              (p.WORK_STATUS <> idTer Or p.WORK_STATUS Is Nothing) And
                                              cv.ID_NO IsNot Nothing And
                                              p.IS_KIEM_NHIEM Is Nothing
                                              Select New UserDTO With {
                                                 .EFFECT_DATE = DateTime.Now,
                                                 .EMPLOYEE_CODE = p.EMPLOYEE_CODE,
                                                 .FULLNAME = p.FULLNAME_VN,
                                                 .EMAIL = cv.WORK_EMAIL,
                                                 .TELEPHONE = cv.MOBILE_PHONE,
                                                 .IS_AD = False,
                                                 .IS_APP = False,
                                                 .IS_PORTAL = True,
                                                 .IS_CHANGE_PASS = "-1",
                                                 .ACTFLG = "A",
                                                 .PASSWORD = cv.ID_NO,
                                                 .USERNAME = p.EMPLOYEE_CODE,
                                                 .EMPLOYEE_ID = p.ID}).ToList

            If lstNew.Count > 0 Then
                Using EncryptData As New EncryptData
                    For i = 0 To lstNew.Count - 1
                        'Dim dangnhap() As String
                        'dangnhap = lstNew(i).USERNAME.Split("@")
                        '_newUser &= ", " & (dangnhap(0))
                        Dim _new As New SE_USER
                        _new.ID = Utilities.GetNextSequence(Context, Context.SE_USER.EntitySet.Name)
                        _new.EFFECT_DATE = lstNew(i).EFFECT_DATE
                        _new.EMPLOYEE_CODE = lstNew(i).EMPLOYEE_CODE
                        _new.FULLNAME = lstNew(i).FULLNAME
                        _new.EMAIL = lstNew(i).EMAIL
                        _new.TELEPHONE = lstNew(i).TELEPHONE
                        _new.IS_AD = False
                        _new.IS_APP = lstNew(i).IS_APP
                        _new.IS_PORTAL = lstNew(i).IS_PORTAL
                        _new.IS_CHANGE_PASS = lstNew(i).IS_CHANGE_PASS
                        _new.ACTFLG = lstNew(i).ACTFLG
                        _new.PASSWORD = EncryptData.EncryptString(lstNew(i).PASSWORD)
                        _new.USERNAME = lstNew(i).USERNAME
                        _new.MODULE_ADMIN = ""
                        _new.EMPLOYEE_ID = lstNew(i).EMPLOYEE_ID
                        'anhvn, 20200917
                        Insert_SE_User(_new, log)
                        Dim se_group = (From p In Context.SE_GROUP Where p.CODE = "PORTAL_USER").FirstOrDefault
                        Insert_Permit(se_group.ID, _new.ID, log)
                        'Context.SE_USER.AddObject(_new)
                    Next

                End Using
            End If
            If _newUser <> "" Then _newUser = _newUser.Substring(2)

            'Kiểm tra nhân viên có thay đổi thông tin
            Dim lstCompare As List(Of UserDTO)
            lstCompare = (From p In Context.HU_EMPLOYEE
                          From cv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = p.ID)
                          From user In Context.SE_USER.Where(Function(f) f.EMPLOYEE_ID = p.ID And
                                                                 f.MODULE_ADMIN Is Nothing And f.EMPLOYEE_ID IsNot Nothing And f.EMAIL IsNot Nothing)
                          Where (p.FULLNAME_VN <> user.FULLNAME Or
                          cv.MOBILE_PHONE <> user.TELEPHONE Or
                          cv.WORK_EMAIL <> user.EMAIL) And user.USERNAME.ToUpper <> "ADMIN"
                          Select New UserDTO With {
                              .ID = user.ID,
                              .USERNAME = user.USERNAME,
                              .FULLNAME = p.FULLNAME_VN,
                              .TELEPHONE = cv.MOBILE_PHONE,
                              .EMAIL = cv.WORK_EMAIL}).ToList


            For i = 0 To lstCompare.Count - 1
                Dim id = lstCompare(i).ID
                'Dim dangnhaps() As String
                'dangnhaps = lstCompare(i).EMAIL.ToUpper.Split("@")

                _modifyUser &= ", " & (lstCompare(i).USERNAME)
                Dim query = (From p In Context.SE_USER Where p.ID = id).FirstOrDefault
                query.FULLNAME = lstCompare(i).FULLNAME
                query.TELEPHONE = lstCompare(i).TELEPHONE
                query.EMAIL = lstCompare(i).EMAIL
                'query.USERNAME = dangnhaps(0)
            Next
            If _modifyUser <> "" Then _modifyUser = _modifyUser.Substring(2)

            ''Kiểm tra nhân viên bị xóa
            'Dim lstUserDelete = (From p In Context.HU_EMPLOYEE
            '                     From cv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = p.ID)
            '                     From user In Context.SE_USER.Where(Function(f) f.EMPLOYEE_CODE = p.EMPLOYEE_CODE)
            '                     Where (p.WORK_STATUS <> idTer Or p.WORK_STATUS Is Nothing Or cv.WORK_EMAIL Is Nothing) And
            '          user.ACTFLG = "A" And user.MODULE_ADMIN.Length = 0
            '                     Select user).ToList

            'For i = 0 To lstUserDelete.Count - 1
            '    _deleteUser &= ", " & (lstUserDelete(i).USERNAME)
            '    Dim id As Decimal = lstUserDelete(i).ID
            '    lstUserDelete(i).ACTFLG = "I"
            'Next
            'If _deleteUser <> "" Then _deleteUser = _deleteUser.Substring(2)

            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' anhvn
    ''' insert se_user and SE_GRP_SE_USR 
    ''' </summary>
    ''' <param name="users"></param>
    Private Sub Insert_SE_User(users As SE_USER, ByVal log As UserLog)
        Using conMng As New ConnectionManager
            Using conn As New OracleConnection(conMng.GetConnectionString())
                conn.Open()
                Using cmd As New OracleCommand()
                    Dim dem As Integer
                    cmd.Connection = conn
                    cmd.Transaction = cmd.Connection.BeginTransaction()
                    Try
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.CommandText = "PKG_COMMON_BUSINESS.INSERT_SE_GRP_SE_USR"
                        cmd.Parameters.Clear()
                        Using resource As New DataAccess.OracleCommon
                            Dim objParam = New With {.P_ID = users.ID,
                                                        .P_EFFECT_DATE = users.EFFECT_DATE,
                                                        .P_EMPLOYEE_CODE = users.EMPLOYEE_CODE,
                                                        .P_FULLNAME = users.FULLNAME,
                                                        .P_EMAIL = users.EMAIL,
                                                        .P_TELEPHONE = users.TELEPHONE,
                                                        .P_IS_AD = users.IS_AD,
                                                        .P_IS_APP = users.IS_APP,
                                                        .P_IS_PORTAL = users.IS_PORTAL,
                                                        .P_IS_CHANGE_PASS = users.IS_CHANGE_PASS,
                                                        .P_ACTFLG = users.ACTFLG,
                                                        .P_PASSWORD = users.PASSWORD,
                                                        .P_USERNAME = users.USERNAME,
                                                        .P_MODULE_ADMIN = users.MODULE_ADMIN,
                                                        .P_SE_USERS_ID = users.ID,
                                                        .P_USERLOG = log.Username,
                                                        .P_EMPLOYEE_ID = users.EMPLOYEE_ID}

                            If objParam IsNot Nothing Then
                                For Each info As PropertyInfo In objParam.GetType().GetProperties()
                                    Dim bOut As Boolean = False
                                    Dim para = resource.GetParameter(info.Name, info.GetValue(objParam, Nothing), bOut)
                                    If para IsNot Nothing Then
                                        cmd.Parameters.Add(para)
                                    End If
                                Next
                            End If
                            dem = cmd.ExecuteNonQuery()
                        End Using
                        cmd.Transaction.Commit()
                    Catch ex As Exception
                        cmd.Transaction.Rollback()
                    Finally
                        'Dispose all resource
                        cmd.Dispose()
                        conn.Close()
                        conn.Dispose()
                    End Try
                End Using
            End Using
        End Using
    End Sub

    Public Function Insert_Permit(ByVal _groupID As Decimal, ByVal _lstUserID As Decimal, ByVal log As UserLog) As Boolean

        Using conMng As New ConnectionManager
            Using conn As New OracleConnection(conMng.GetConnectionString())
                conn.Open()
                Dim dem As Integer

                Using cmd As New OracleCommand()
                    cmd.Connection = conn
                    cmd.Transaction = cmd.Connection.BeginTransaction()
                    Try
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.CommandText = "PKG_COMMON_BUSINESS.TRANSFER_GROUP_TO_USER"
                        cmd.Parameters.Clear()
                        Using resource As New DataAccess.OracleCommon
                            Dim objParam = New With {.P_USER_ID = _lstUserID,
                                                         .P_GROUP_ID = _groupID,
                                                         .P_USERNAME = log.Username}
                            If objParam IsNot Nothing Then
                                For Each info As PropertyInfo In objParam.GetType().GetProperties()
                                    Dim bOut As Boolean = False
                                    Dim para = resource.GetParameter(info.Name, info.GetValue(objParam, Nothing), bOut)
                                    If para IsNot Nothing Then
                                        cmd.Parameters.Add(para)
                                    End If
                                Next
                            End If
                            dem = cmd.ExecuteNonQuery()
                        End Using
                        cmd.Transaction.Commit()
                    Catch ex As Exception
                        cmd.Transaction.Rollback()
                    Finally
                        'Dispose all resource
                        cmd.Dispose()
                        conn.Close()
                        conn.Dispose()
                    End Try
                End Using
            End Using
        End Using

        Return True
    End Function

    Public Function Get_user_info(ByVal id As Decimal) As List(Of String)
        Try
            Dim res = (From p In Context.SE_USER Where p.ID = id Select p.USERNAME, p.PASSWORD, p.EMAIL).FirstOrDefault
            Dim str As New List(Of String)
            str.Add(res.USERNAME)
            Using encry As New EncryptData
                str.Add(encry.DecryptString(res.PASSWORD))
            End Using
            str.Add(res.EMAIL)
            Return str
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GET_MAIL_TEMPLATE(ByVal code As String, ByVal group As String) As List(Of String)
        Try
            Dim res = (From p In Context.SE_MAIL_TEMPLATE Where p.CODE = code And p.GROUP_MAIL = group Select p.CONTENT, p.MAIL_CC, p.TITLE).FirstOrDefault
            Dim str As New List(Of String)
            str.Add(res.CONTENT)
            str.Add(res.MAIL_CC)
            str.Add(res.TITLE)
            Return str
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ResetUserPassword(ByVal _userid As List(Of Decimal),
                                      ByVal _minLength As Integer,
                                      ByVal _hasLowerChar As Boolean,
                                      ByVal _hasUpperChar As Boolean,
                                      ByVal _hasNumbericChar As Boolean,
                                      ByVal _hasSpecialChar As Boolean,
                                      ByVal _hasPasswordConfig As Boolean, ByVal _hasPasswordDefault As Boolean, ByVal _hasPasswordDefaultText As String) As Boolean
        Try
            'Lấy danh sách user
            Dim lst As List(Of SE_USER) = (From p In Context.SE_USER
                                           Where _userid.Contains(p.ID)
                                           Select p).ToList
            If _hasPasswordConfig = True Then
                'Lấy thông tin config password
                Dim rndPass As New RandomPassword
                rndPass.HAS_LOWER_CHAR = _hasLowerChar
                rndPass.HAS_NUMERIC_CHAR = _hasNumbericChar
                rndPass.HAS_SPECIAL_CHAR = _hasSpecialChar
                rndPass.HAS_UPPER_CHAR = _hasUpperChar
                For i = 0 To lst.Count - 1
                    Using EncryptData As New EncryptData
                        lst(i).PASSWORD = EncryptData.EncryptString(rndPass.Generate(_minLength))
                        lst(i).IS_CHANGE_PASS = 0
                        lst(i).IS_LOGIN = True

                    End Using
                Next
            ElseIf _hasPasswordDefault = True AndAlso _hasPasswordDefaultText IsNot Nothing Then
                For i = 0 To lst.Count - 1
                    Using EncryptData As New EncryptData
                        lst(i).PASSWORD = EncryptData.EncryptString(_hasPasswordDefaultText)
                        lst(i).IS_CHANGE_PASS = 0
                        lst(i).IS_LOGIN = True

                    End Using
                Next
            End If
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ValidateUserForgetPass(ByVal _userName As String, ByRef uID As Decimal) As String
        Try
            Dim query = (From p In Context.SE_USER Where p.USERNAME.ToUpper.Equals(_userName.ToUpper)).FirstOrDefault
            If query IsNot Nothing Then
                If Not String.IsNullOrEmpty(query.EMAIL) Then
                    If query.ACTFLG = "I" Then
                        Return "3"
                    Else
                        uID = query.ID
                        Return query.EMAIL
                    End If
                Else
                    Return "2"
                End If
            Else
                Return "1"
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function checkRulePass(ByVal pass As String, ByVal oldpass As String, ByVal userid As Decimal) As Decimal

        Dim query As SE_USER
        query = (From p In Context.SE_USER
                 Where p.ID = userid
                 Select p).FirstOrDefault
        If query IsNot Nothing Then
            Using EncryptData As New EncryptData
                If query.PASSWORD <> EncryptData.EncryptString(oldpass) Then
                    Return -6
                End If
            End Using
        End If

        Using cls As New DataAccess.QueryData
            Dim obj = New With {.P_PASS = pass,
                                .P_OUT = cls.OUT_NUMBER}
            cls.ExecuteStore("PKG_API_MOBILE.CHECK_PASSWORD_FORGOT", obj)
            Return Integer.Parse(obj.P_OUT)

        End Using
    End Function


    Public Function ChangePassByForget(ByVal _userName As String, ByVal _passWord As String) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                Dim obj = New With {.P_PASS = _passWord,
                                    .P_OUT = cls.OUT_NUMBER}
                cls.ExecuteStore("PKG_API_MOBILE.CHECK_PASSWORD_FORGOT", obj)
                If Integer.Parse(obj.P_OUT) <> 1 Then
                    Return False
                End If
            End Using

            Dim query As SE_USER
            query = (From p In Context.SE_USER
                     Where p.USERNAME.ToUpper = _userName.ToUpper
                     Select p).FirstOrDefault
            If query IsNot Nothing Then
                Using EncryptData As New EncryptData
                    query.PASSWORD = EncryptData.EncryptString(_passWord)
                    query.IS_CHANGE_PASS = -1
                    query.CHANGE_PASS_DATE = Now.Date
                    query.IS_LOGIN = False
                    query.MODIFIED_DATE = DateTime.Now
                    query.MODIFIED_BY = _userName.ToUpper
                    query.MODIFIED_LOG = "Forget Pass"
                    Context.SaveChanges()
                End Using
            Else
                Return False
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ForgetPassSendCode(ByVal _email As String, ByVal _code As String) As Boolean
        Try
            Dim dataMail = GET_MAIL_TEMPLATE("QUEN_MK", "Common")

            Dim defaultFrom As String = ""
            Dim config As Dictionary(Of String, String)
            config = GetConfig(ModuleID.All)
            defaultFrom = If(config.ContainsKey("MailFrom"), config("MailFrom"), "")

            'Imports System.Data.Common
            Dim timeSP = (From p In Context.SE_CONFIG Where p.CODE = "EFFECT_TIME_FOR_CODE_RESET_PASSWORD" Select p.VALUE).FirstOrDefault

            '  If(config.ContainsKey("EffectTimeForCodeResetPassword"), config("EffectTimeForCodeResetPassword"), "")


            If defaultFrom = "" Then
                Return False
            End If
            'Lấy danh sách user
            InsertMail(defaultFrom, _email, If(dataMail(2) = "", "FORGET PASS", dataMail(2)), String.Format(dataMail(0), _code, If(CDec(Val(timeSP)) <> 0, timeSP, "60")), "", "", "", False, 1)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iCommon")
            'Logger.LogError(ex)
        End Try
    End Function
#End Region
#Region "register user"
    Public Function check_dup_reguser(ByVal effect_date As Date, Optional ByVal id As Decimal = 0) As Decimal

        If id = 0 Then
            Return (From p In Context.SE_REGISTER_USER Where p.EFFECT_DATE = effect_date).Count
        Else
            Return (From p In Context.SE_REGISTER_USER Where p.ID <> id And p.EFFECT_DATE = effect_date).Count
        End If
    End Function


    Public Function GetRegisterUser(ByVal _filter As RegisterUserDTO,
                                ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer,
                                Optional ByVal Sorts As String = "MODIFIED_DATE desc") As List(Of RegisterUserDTO)

        Dim query = (From p In Context.SE_REGISTER_USER
                     Order By p.EFFECT_DATE Descending Select New RegisterUserDTO With {
                     .ID = p.ID,
                     .APP_USER = p.APP_USER,
                     .PORTAL_USER = p.PORTAL_USER,
                     .EFFECT_DATE = p.EFFECT_DATE,
                     .NOTE = p.NOTE,
                     .MODIFIED_DATE = p.MODIFIED_DATE})

        If _filter.APP_USER IsNot Nothing Then
            query = query.Where(Function(p) p.APP_USER = _filter.APP_USER)
        End If
        If _filter.PORTAL_USER IsNot Nothing Then
            query = query.Where(Function(p) p.PORTAL_USER = _filter.PORTAL_USER)
        End If
        If _filter.NOTE <> "" Then
            query = query.Where(Function(p) p.NOTE.ToUpper.Contains(_filter.NOTE.ToUpper))
        End If
        If _filter.EFFECT_DATE IsNot Nothing Then
            query = query.Where(Function(p) p.EFFECT_DATE = _filter.EFFECT_DATE)
        End If
        query = query.AsQueryable().OrderBy(Sorts)
        Total = query.Count
        query = query.Skip(PageIndex * PageSize).Take(PageSize)

        Dim result = (From p In query Select p)

        Return result.ToList
    End Function
    Public Function UpdateRegisterUser(ByVal _function As List(Of RegisterUserDTO), ByVal log As UserLog) As Boolean
        Dim i As Integer
        For i = 0 To _function.Count - 1
            Dim obj As New SE_REGISTER_USER With {.ID = _function(0).ID}

            Context.SE_REGISTER_USER.Attach(obj)
            obj.APP_USER = _function(i).APP_USER
            obj.PORTAL_USER = _function(i).PORTAL_USER
            obj.EFFECT_DATE = _function(i).EFFECT_DATE
            obj.NOTE = _function(i).NOTE
            obj.MODIFIED_DATE = DateTime.Now
            obj.MODIFIED_BY = log.Username
            obj.MODIFIED_LOG = log.ComputerName
            Context.SaveChanges(log)

        Next
        Return True
    End Function

    Public Function InsertRegisterUser(ByVal _item As RegisterUserDTO, ByVal log As UserLog) As Boolean
        Try
            Dim _new As New SE_REGISTER_USER
            _new.ID = Utilities.GetNextSequence(Context, Context.SE_REGISTER_USER.EntitySet.Name)
            _new.APP_USER = _item.APP_USER
            _new.PORTAL_USER = _item.PORTAL_USER
            _new.EFFECT_DATE = _item.EFFECT_DATE
            _new.NOTE = _item.NOTE
            _new.MODIFIED_DATE = DateTime.Now
            _new.MODIFIED_BY = log.Username
            _new.MODIFIED_LOG = log.ComputerName
            Context.SE_REGISTER_USER.AddObject(_new)
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function DeleteRegisterUser(ByVal lst_id As List(Of Decimal)) As Boolean
        Try
            For Each item In lst_id
                Dim dt = (From p In Context.SE_REGISTER_USER Where p.ID = item Select p).FirstOrDefault
                Context.SE_REGISTER_USER.DeleteObject(dt)
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception

        End Try
    End Function
#End Region
#Region "Group List"
    Public Function GetGroupListToComboListBox() As List(Of GroupDTO)
        Dim query = (From p In Context.SE_GROUP.ToList
                     Where (CBool(p.IS_ADMIN) = False Or (p.IS_ADMIN = -1 And p.IS_HR_ADMIN = -1)) And p.ACTFLG.ToUpper = "A"
                     Order By p.NAME Select New GroupDTO With {
                     .ID = p.ID,
                     .NAME = p.NAME,
                     .CODE = p.CODE,
                     .IS_HR_ADMIN = p.IS_HR_ADMIN})
        Return query.ToList
    End Function

    Public Function GetGroupListToComboListBox1() As List(Of GroupDTO)
        Dim query = (From p In Context.SE_GROUP.ToList
                     Where Not (p.IS_ADMIN = -1 And p.IS_HR_ADMIN Is Nothing) And p.ACTFLG.ToUpper = "A"
                     Order By p.NAME Select New GroupDTO With {
                     .ID = p.ID,
                     .NAME = p.NAME,
                     .CODE = p.CODE})
        Return query.ToList
    End Function

    Public Function GetGroupList(ByVal _filter As GroupDTO,
                            ByVal PageIndex As Integer,
                            ByVal PageSize As Integer,
                            ByRef Total As Integer,
                            Optional ByVal Sorts As String = "MODIFIED_DATE desc") As List(Of GroupDTO)

        Dim query = (From p In Context.SE_GROUP
                     Where CBool(p.IS_ADMIN) = False Or (p.IS_ADMIN = -1 And p.IS_HR_ADMIN = -1)
                     Order By p.NAME Select New GroupDTO With {
                     .ID = p.ID,
                     .NAME = p.NAME,
                     .CODE = p.CODE,
                     .IS_HR_ADMIN = p.IS_HR_ADMIN,
                     .IS_ADMIN = p.IS_ADMIN,
                     .IS_FUNCTION_PERMISSION = p.IS_FUNCTION_PERMISSION,
                     .IS_ORG_PERMISSION = p.IS_ORG_PERMISSION,
                     .EFFECT_DATE = p.EFFECT_DATE,
                     .EXPIRE_DATE = p.EXPIRE_DATE,
                     .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                     .MODIFIED_DATE = p.MODIFIED_DATE})

        If _filter.CODE <> "" Then
            query = query.Where(Function(p) p.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
        End If
        If _filter.NAME <> "" Then
            query = query.Where(Function(p) p.NAME.ToUpper.Contains(_filter.NAME.ToUpper))
        End If
        If _filter.ACTFLG <> "" Then
            query = query.Where(Function(p) p.ACTFLG.ToUpper.Contains(_filter.ACTFLG.ToUpper))
        End If
        If _filter.EFFECT_DATE IsNot Nothing Then
            query = query.Where(Function(p) p.EFFECT_DATE = _filter.EFFECT_DATE)
        End If
        If _filter.EXPIRE_DATE IsNot Nothing Then
            query = query.Where(Function(p) p.EXPIRE_DATE = _filter.EXPIRE_DATE)
        End If
        query = query.AsQueryable().OrderBy(Sorts)
        Total = query.Count
        query = query.Skip(PageIndex * PageSize).Take(PageSize)

        Dim result = (From p In query Select p)

        Return result.ToList
    End Function

    Public Function ValidateGroupList(ByVal _validate As GroupDTO) As Boolean
        Dim query

        If _validate.CODE <> Nothing Then
            query = (From p In Context.SE_GROUP Where p.CODE.ToUpper = _validate.CODE.ToUpper AndAlso p.ID <> _validate.ID).FirstOrDefault
            Return (query Is Nothing)
        End If
        If _validate.NAME <> Nothing Then
            query = (From p In Context.SE_GROUP Where p.NAME.ToUpper = _validate.NAME.ToUpper AndAlso p.ID <> _validate.ID).FirstOrDefault
            Return (query Is Nothing)
        End If
        Return True
    End Function

    Public Function InsertGroup(ByVal _group As GroupDTO, ByVal log As UserLog) As Boolean
        Try
            Dim _new As New SE_GROUP
            _new.ID = Utilities.GetNextSequence(Context, Context.SE_GROUP.EntitySet.Name)
            _new.CODE = _group.CODE
            _new.NAME = _group.NAME
            _new.IS_ADMIN = _group.IS_ADMIN
            _new.IS_FUNCTION_PERMISSION = _group.IS_FUNCTION_PERMISSION
            _new.IS_ORG_PERMISSION = _group.IS_ORG_PERMISSION
            _new.ACTFLG = "A"
            _new.EFFECT_DATE = _group.EFFECT_DATE
            _new.EXPIRE_DATE = _group.EXPIRE_DATE
            Context.SE_GROUP.AddObject(_new)
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function UpdateGroup(ByVal _group As GroupDTO, ByVal log As UserLog) As Boolean
        Try

            Dim lstGroup As SE_GROUP = (From p In Context.SE_GROUP Select p Where p.ID = _group.ID).FirstOrDefault
            If lstGroup IsNot Nothing Then
                lstGroup.NAME = _group.NAME
                lstGroup.IS_ADMIN = _group.IS_ADMIN
                lstGroup.IS_FUNCTION_PERMISSION = _group.IS_FUNCTION_PERMISSION
                lstGroup.IS_ORG_PERMISSION = _group.IS_ORG_PERMISSION
                lstGroup.CODE = _group.CODE
                lstGroup.EFFECT_DATE = _group.EFFECT_DATE
                lstGroup.EXPIRE_DATE = _group.EXPIRE_DATE
                Context.SaveChanges(log)
            Else
                Return False
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function DeleteGroup(ByVal _lstID As List(Of Decimal), ByRef _error As String, ByVal log As UserLog) As Boolean
        Try
            _error = ""
            Dim lstGroup As List(Of SE_GROUP) = (From p In Context.SE_GROUP Select p Where _lstID.Contains(p.ID)).ToList
            Dim lstDeletes As New List(Of SE_GROUP)
            For i As Integer = 0 To lstGroup.Count - 1
                If lstGroup(i).SE_USERS.Count > 0 Then
                    If _error = "" Then
                        _error = lstGroup(i).NAME
                    Else
                        _error = _error & "," & lstGroup(i).NAME
                    End If

                    If _lstID.Contains(lstGroup(i).ID) Then
                        _lstID.Remove(lstGroup(i).ID)
                    End If
                Else
                    lstDeletes.Add(lstGroup(i))
                End If
            Next
            If _error <> "" Then
                Return True
            End If
            Dim lstPermissions As List(Of SE_GROUP_PERMISSION) = (From p In Context.SE_GROUP_PERMISSION Select p Where _lstID.Contains(p.GROUP_ID)).ToList
            If lstGroup.Count > 0 Then
                For i = 0 To lstPermissions.Count - 1
                    Context.SE_GROUP_PERMISSION.DeleteObject(lstPermissions(i))
                Next
                For Each item In (From p In Context.SE_GROUP_ORG_ACCESS Where _lstID.Contains(p.GROUP_ID) Select p).ToList()
                    Context.SE_GROUP_ORG_ACCESS.DeleteObject(item)
                Next

                For i = 0 To lstDeletes.Count - 1
                    lstDeletes(i).SE_REPORTS.Clear()
                    Context.SE_GROUP.DeleteObject(lstDeletes(i))
                Next
                Context.SaveChanges(log)
            End If
            Return True
        Catch ex As Exception
            _error = ex.ToString()
            Return False
        End Try
        Return False
    End Function
    Public Function UpdateGroupStatus(ByVal _lstID As List(Of Decimal), ByVal _ACTFLG As String, ByVal log As UserLog) As Boolean
        Try
            Dim lstGroup As List(Of SE_GROUP) = (From p In Context.SE_GROUP Select p Where _lstID.Contains(p.ID)).ToList
            If lstGroup.Count > 0 Then
                For i = 0 To lstGroup.Count - 1
                    lstGroup(i).ACTFLG = _ACTFLG
                    lstGroup(i).MODIFIED_DATE = DateTime.Now
                    lstGroup(i).MODIFIED_BY = log.Username
                    lstGroup(i).MODIFIED_LOG = log.ComputerName
                Next
                Context.SaveChanges(log)
            Else
                Return False
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "Function List"

    Public Function GetFunctionList(ByVal _filter As FunctionDTO,
                            ByVal PageIndex As Integer,
                            ByVal PageSize As Integer,
                            ByRef Total As Integer,
                            Optional ByVal Sorts As String = "NAME asc") As List(Of FunctionDTO)
        Dim query = (From p In Context.SE_FUNCTION
                     From m In Context.SE_MODULE.Where(Function(f) f.ID = p.MODULE_ID).DefaultIfEmpty
                     Where m.IS_ACTIVE = 1
                     Select New FunctionDTO With {
                     .ID = p.ID,
                     .FID = p.FID,
                     .NAME = p.NAME,
                     .SETUP_SIGNER = p.SETUP_SIGNER,
                     .ADMIN_HR = p.ADMIN_HR,
                     .MODULE_NAME = p.SE_MODULE.NAME_DESC,
                     .ORDER_BY = p.SE_MODULE.ORDER_BY,
                     .FUNCTION_GROUP_ID = p.SE_FUNCTION_GROUP.ID,
                     .FUNCTION_GROUP_NAME = p.SE_FUNCTION_GROUP.NAME,
                     .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Không áp dụng"),
                     .MODULE_ID = p.MODULE_ID})
        If _filter.ID <> 0 Then
            query = query.Where(Function(p) p.ID = _filter.ID)
        End If
        If _filter.FID <> "" Then
            query = query.Where(Function(p) p.FID.ToUpper.Contains(_filter.FID.ToUpper))
        End If
        If _filter.NAME <> "" Then
            query = query.Where(Function(p) p.NAME.ToUpper.Contains(_filter.NAME.ToUpper))
        End If
        If _filter.MODULE_ID <> 0 Then
            query = query.Where(Function(p) p.MODULE_ID = _filter.MODULE_ID)
        End If
        If _filter.FUNCTION_GROUP_ID <> 0 Then
            query = query.Where(Function(p) p.FUNCTION_GROUP_ID = _filter.FUNCTION_GROUP_ID)
        End If
        If _filter.ACTFLG <> "" Then
            query = query.Where(Function(p) p.ACTFLG = _filter.ACTFLG)
        End If
        If PageSize <> -1 Then
            query = query.OrderBy(Sorts)
            Total = query.Count
            query = query.Skip(PageIndex * PageSize).Take(PageSize)
        End If
        Dim result = (From p In query Select p)
        Return result.ToList
    End Function

    Public Function ValidateFunctionList(ByVal _validate As FunctionDTO) As Boolean
        Dim query
        If _validate.NAME <> "" Then
            query = (From p In Context.SE_FUNCTION Where p.NAME.ToUpper = _validate.NAME.ToUpper AndAlso p.ID <> _validate.ID).FirstOrDefault
            Return (query Is Nothing)
        End If
        If _validate.FID <> "" Then
            query = (From p In Context.SE_FUNCTION Where p.FID.ToUpper = _validate.FID.ToUpper AndAlso p.ID <> _validate.ID).FirstOrDefault
            Return (query Is Nothing)
        End If
        Return True
    End Function

    Public Function UpdateFunctionList(ByVal _function As List(Of FunctionDTO), ByVal log As UserLog) As Boolean
        Try
            Dim i As Integer
            Dim lst_objGroupF As New List(Of GroupFunctionDTO)
            For i = 0 To _function.Count - 1
                Dim obj As New SE_FUNCTION With {.ID = _function(0).ID}

                Context.SE_FUNCTION.Attach(obj)
                obj.NAME = _function(i).NAME
                obj.FID = _function(i).FID
                obj.SETUP_SIGNER = _function(i).SETUP_SIGNER
                obj.GROUP_ID = _function(i).FUNCTION_GROUP_ID
                obj.MODULE_ID = _function(i).MODULE_ID
                obj.ADMIN_HR = _function(i).ADMIN_HR
                obj.MODIFIED_DATE = DateTime.Now
                obj.MODIFIED_BY = log.Username
                obj.MODIFIED_LOG = log.ComputerName
                Context.SaveChanges()

                Dim lst = GET_SE_GROUP_ID_IS_HR_ADMIN()
                For Each item In lst
                    Dim SE_GR_PER_ID As Decimal? = (From p In Context.SE_GROUP_PERMISSION Where p.FUNCTION_ID = obj.ID And p.GROUP_ID = item Select p.ID).FirstOrDefault
                    If SE_GR_PER_ID <> 0 Then

                        'Update
                        If obj.ADMIN_HR Then

                            Dim objgr As New GroupFunctionDTO
                            objgr.ID = SE_GR_PER_ID
                            objgr.ALLOW_CREATE = -1
                            objgr.ALLOW_DELETE = -1
                            objgr.ALLOW_EXPORT = -1
                            objgr.ALLOW_IMPORT = -1
                            objgr.ALLOW_MODIFY = -1
                            objgr.ALLOW_PRINT = -1
                            objgr.ALLOW_SPECIAL1 = -1
                            objgr.ALLOW_SPECIAL2 = -1
                            objgr.ALLOW_SPECIAL3 = -1
                            objgr.ALLOW_SPECIAL4 = -1
                            objgr.ALLOW_SPECIAL5 = -1
                            objgr.FUNCTION_ID = obj.ID
                            objgr.GROUP_ID = item
                            lst_objGroupF.Add(objgr)

                            UpdateGroupFunction(lst_objGroupF, log)
                        Else

                            Dim lst_IDGrF As New List(Of Decimal)
                            lst_IDGrF.Add(SE_GR_PER_ID)
                            DeleteGroupFunction(lst_IDGrF)
                        End If
                    Else

                        ' Insert 
                        If obj.ADMIN_HR Then
                            Dim objgr As New GroupFunctionDTO
                            objgr.ALLOW_CREATE = -1
                            objgr.ALLOW_DELETE = -1
                            objgr.ALLOW_EXPORT = -1
                            objgr.ALLOW_IMPORT = -1
                            objgr.ALLOW_MODIFY = -1
                            objgr.ALLOW_PRINT = -1
                            objgr.ALLOW_SPECIAL1 = -1
                            objgr.ALLOW_SPECIAL2 = -1
                            objgr.ALLOW_SPECIAL3 = -1
                            objgr.ALLOW_SPECIAL4 = -1
                            objgr.ALLOW_SPECIAL5 = -1
                            objgr.FUNCTION_ID = obj.ID
                            objgr.GROUP_ID = item
                            lst_objGroupF.Add(objgr)
                            InsertGroupFunction(lst_objGroupF, log)
                        End If
                    End If
                Next
            Next

            Return True
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function GET_SE_GROUP_ID_IS_HR_ADMIN() As List(Of Decimal)
        Try
            Dim lst = (From p In Context.SE_GROUP Where p.IS_HR_ADMIN = -1 And p.ACTFLG = "A" Select p.ID).ToList
            Return lst
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function InsertFunctionList(ByVal _item As FunctionDTO, ByVal log As UserLog) As Boolean
        Try
            Dim lst_objGroupF As New List(Of GroupFunctionDTO)
            Dim _new As New SE_FUNCTION
            _new.ID = Utilities.GetNextSequence(Context, Context.SE_FUNCTION.EntitySet.Name)
            _new.ACTFLG = _item.ACTFLG
            _new.NAME = _item.NAME
            _new.FID = _item.FID
            _new.SETUP_SIGNER = _item.SETUP_SIGNER
            _new.ADMIN_HR = _item.ADMIN_HR
            _new.GROUP_ID = _item.FUNCTION_GROUP_ID
            _new.MODULE_ID = _item.MODULE_ID
            _new.MODIFIED_DATE = DateTime.Now
            _new.MODIFIED_BY = log.Username
            _new.MODIFIED_LOG = log.ComputerName
            Context.SE_FUNCTION.AddObject(_new)

            If _item.ADMIN_HR Then
                Dim lst = GET_SE_GROUP_ID_IS_HR_ADMIN()
                For Each item In lst
                    Dim objgr As New GroupFunctionDTO
                    objgr.ALLOW_CREATE = -1
                    objgr.ALLOW_DELETE = -1
                    objgr.ALLOW_EXPORT = -1
                    objgr.ALLOW_IMPORT = -1
                    objgr.ALLOW_MODIFY = -1
                    objgr.ALLOW_PRINT = -1
                    objgr.ALLOW_SPECIAL1 = -1
                    objgr.ALLOW_SPECIAL2 = -1
                    objgr.ALLOW_SPECIAL3 = -1
                    objgr.ALLOW_SPECIAL4 = -1
                    objgr.ALLOW_SPECIAL5 = -1
                    objgr.FUNCTION_ID = _new.ID
                    objgr.GROUP_ID = item
                    lst_objGroupF.Add(objgr)
                    InsertGroupFunction(lst_objGroupF, log)
                Next
            Else
                Context.SaveChanges(log)
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetModuleList() As List(Of ModuleDTO)
        Dim query = (From p In Context.SE_MODULE Select New ModuleDTO With {
                     .ID = p.ID,
                     .NAME = p.NAME,
                     .MID = p.MID})
        Return query.ToList
    End Function

    Public Function ActiveFunctions(ByVal lstFunction As List(Of FunctionDTO), ByVal sActive As String, ByVal log As UserLog) As Boolean
        Try
            Dim lstFunctionData As List(Of SE_FUNCTION)
            Dim lstIDFunction As List(Of Decimal) = (From p In lstFunction.ToList Select p.ID).ToList
            lstFunctionData = (From p In Context.SE_FUNCTION Where lstIDFunction.Contains(p.ID)).ToList
            For index = 0 To lstFunctionData.Count - 1
                lstFunctionData(index).ACTFLG = sActive
                lstFunctionData(index).MODIFIED_DATE = DateTime.Now
                lstFunctionData(index).MODIFIED_BY = log.Username
                lstFunctionData(index).MODIFIED_LOG = log.ComputerName
                'Return False
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            Throw ex
        End Try

    End Function

#End Region

#Region "Group User"

    Public Function GetUserListInGroup(ByVal _groupID As Decimal,
                                       ByVal _filter As UserDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of UserDTO)

        Dim query = (From p In Context.SE_USER
                     From m In p.SE_GROUPS
                     Where m.ID = _groupID
                     Select p)

        If _filter.USERNAME <> "" Then
            query = query.Where(Function(p) p.USERNAME.ToUpper.Contains(_filter.USERNAME.ToUpper))
        End If
        If _filter.FULLNAME <> "" Then
            query = query.Where(Function(p) p.FULLNAME.ToUpper.Contains(_filter.FULLNAME.ToUpper))
        End If
        If _filter.EMAIL <> "" Then
            query = query.Where(Function(p) p.EMAIL.ToUpper.Contains(_filter.EMAIL.ToUpper))
        End If
        If _filter.TELEPHONE <> "" Then
            query = query.Where(Function(p) p.TELEPHONE.ToUpper.Contains(_filter.TELEPHONE.ToUpper))
        End If
        If _filter.IS_APP IsNot Nothing Then
            query = query.Where(Function(p) p.IS_APP = _filter.IS_APP)
        End If
        If _filter.IS_PORTAL IsNot Nothing Then
            query = query.Where(Function(p) p.IS_PORTAL = _filter.IS_PORTAL)
        End If
        If _filter.IS_AD IsNot Nothing Then
            query = query.Where(Function(p) p.IS_AD = _filter.IS_AD)
        End If

        query = query.OrderBy(Sorts)
        Total = query.Count
        query = query.Skip(PageIndex * PageSize).Take(PageSize)

        Return (From p In query
                Select New UserDTO With {
                .ID = p.ID,
                .USERNAME = p.USERNAME,
                .FULLNAME = p.FULLNAME,
                .EMAIL = p.EMAIL,
                .TELEPHONE = p.TELEPHONE,
                .IS_AD = p.IS_AD,
                .IS_APP = p.IS_APP,
                .IS_PORTAL = p.IS_PORTAL}).ToList

    End Function
    Public Function GetUserListInGroup1(ByVal _groupID As Decimal,
                                       ByVal _filter As UserDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of UserDTO)

        Dim query = (From p In Context.SE_USER
                     From E In Context.HU_EMPLOYEE.Where(Function(F) F.ID = p.EMPLOYEE_ID).DefaultIfEmpty
                     From T In Context.HU_TITLE.Where(Function(F) F.ID = E.TITLE_ID).DefaultIfEmpty
                     From m In p.SE_GROUPS
                     Where m.ID = _groupID And Not (m.IS_ADMIN = -1 And m.IS_HR_ADMIN Is Nothing)
                     Select New UserDTO With {
                                                .ID = p.ID,
                                                .USERNAME = p.USERNAME,
                                                .FULLNAME = p.FULLNAME,
                                                .EMAIL = p.EMAIL,
                                                .TELEPHONE = p.TELEPHONE,
                                                .IS_AD = p.IS_AD,
                                                .IS_APP = p.IS_APP,
                                                .IS_PORTAL = p.IS_PORTAL,
                                                .CREATED_DATE = p.CREATED_DATE,
                                                .EFFECTDATE_TERMINATION = p.EFFECTDATE_TERMINATION,
                                                .TITLE_NAME = T.NAME_VN})

        If Not _filter.IS_SHOW_INACTIVE_USER Then
            query = query.Where(Function(p) p.EFFECTDATE_TERMINATION > Date.Now Or Not p.EFFECTDATE_TERMINATION.HasValue)
        Else
            query = query.Where(Function(p) p.EFFECTDATE_TERMINATION <= Date.Now Or p.EFFECTDATE_TERMINATION.HasValue)
        End If

        If _filter.USERNAME <> "" Then
            query = query.Where(Function(p) p.USERNAME.ToUpper.Contains(_filter.USERNAME.ToUpper))
        End If
        If _filter.FULLNAME <> "" Then
            query = query.Where(Function(p) p.FULLNAME.ToUpper.Contains(_filter.FULLNAME.ToUpper))
        End If
        If _filter.EMAIL <> "" Then
            query = query.Where(Function(p) p.EMAIL.ToUpper.Contains(_filter.EMAIL.ToUpper))
        End If
        If _filter.TELEPHONE <> "" Then
            query = query.Where(Function(p) p.TELEPHONE.ToUpper.Contains(_filter.TELEPHONE.ToUpper))
        End If
        If _filter.IS_APP IsNot Nothing Then
            query = query.Where(Function(p) p.IS_APP = _filter.IS_APP)
        End If
        If _filter.IS_PORTAL IsNot Nothing Then
            query = query.Where(Function(p) p.IS_PORTAL = _filter.IS_PORTAL)
        End If
        If _filter.IS_AD IsNot Nothing Then
            query = query.Where(Function(p) p.IS_AD = _filter.IS_AD)
        End If

        query = query.OrderBy(Sorts)
        Total = query.Count
        query = query.Skip(PageIndex * PageSize).Take(PageSize)

        'Return (From p In query
        '        Select New UserDTO With {
        '        .ID = p.ID,
        '        .USERNAME = p.USERNAME,
        '        .FULLNAME = p.FULLNAME,
        '        .EMAIL = p.EMAIL,
        '        .TELEPHONE = p.TELEPHONE,
        '        .IS_AD = p.IS_AD,
        '        .IS_APP = p.IS_APP,
        '        .IS_PORTAL = p.IS_PORTAL}).ToList
        Return query.ToList

    End Function

    Public Function GetUserListOutGroup(ByVal _groupID As Decimal,
                                        ByVal _filter As UserDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of UserDTO)
        Dim userIn = (From p In Context.SE_USER
                      From m In p.SE_GROUPS
                      Where m.ID = _groupID
                      Select p)
        Dim query = (From p In Context.SE_USER Where p.ACTFLG.ToUpper = "A" And p.EFFECTDATE_TERMINATION Is Nothing And p.MODULE_ADMIN Is Nothing Select p)
        If _filter.USERNAME <> "" Then
            query = query.Where(Function(p) p.USERNAME.ToUpper.Contains(_filter.USERNAME.ToUpper))
        End If
        If _filter.FULLNAME <> "" Then
            query = query.Where(Function(p) p.FULLNAME.ToUpper.Contains(_filter.FULLNAME.ToUpper))
        End If
        If _filter.IS_APP IsNot Nothing Then
            query = query.Where(Function(p) p.IS_APP = _filter.IS_APP)
        End If
        If _filter.IS_PORTAL IsNot Nothing Then
            query = query.Where(Function(p) p.IS_PORTAL = _filter.IS_PORTAL)
        End If
        If _filter.IS_AD IsNot Nothing Then
            query = query.Where(Function(p) p.IS_AD = _filter.IS_AD)
        End If
        If _filter.LST_ORG IsNot Nothing AndAlso _filter.LST_ORG.Count > 0 Then
            Dim emp_ID = (From p In Context.HU_EMPLOYEE Where _filter.LST_ORG.Contains(p.ORG_ID) Select p.ID).ToList
            query = query.Where(Function(p) emp_ID.Contains(p.EMPLOYEE_ID))
        End If
        query = query.Except(userIn)

        query = query.OrderBy(Sorts)
        Total = query.Count
        query = query.Skip(PageIndex * PageSize).Take(PageSize)

        Dim lst = (From p In query
                   From E In Context.HU_EMPLOYEE.Where(Function(F) F.ID = p.EMPLOYEE_ID).DefaultIfEmpty
                   From T In Context.HU_TITLE.Where(Function(F) F.ID = E.TITLE_ID).DefaultIfEmpty
                   Select New UserDTO With {
                .ID = p.ID,
                .USERNAME = p.USERNAME,
                .FULLNAME = p.FULLNAME,
                .EMAIL = p.EMAIL,
                .TELEPHONE = p.TELEPHONE,
                .IS_AD = p.IS_AD,
                .IS_APP = p.IS_APP,
                .IS_PORTAL = p.IS_PORTAL,
                .TITLE_NAME = T.NAME_VN})
        Return lst.ToList
    End Function



    Public Function InsertUserGroup(ByVal _groupID As Decimal, ByVal _lstUserID As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim lstGroup As SE_GROUP = (From p In Context.SE_GROUP Select p Where p.ID = _groupID).FirstOrDefault
        For i As Integer = 0 To _lstUserID.Count - 1
            Dim id As Decimal = _lstUserID(i)
            Dim user As SE_USER = (From p In Context.SE_USER Where p.ID = id Select p).FirstOrDefault
            lstGroup.SE_USERS.Add(user)
        Next
        Context.SaveChanges(log)
        Using conMng As New ConnectionManager
            Using conn As New OracleConnection(conMng.GetConnectionString())
                conn.Open()
                Using cmd As New OracleCommand()
                    Dim dem As Integer
                    cmd.Connection = conn
                    cmd.Transaction = cmd.Connection.BeginTransaction()
                    Try
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.CommandText = "PKG_COMMON_BUSINESS.TRANSFER_GROUP_TO_USER"
                        For i As Integer = 0 To _lstUserID.Count - 1
                            cmd.Parameters.Clear()
                            Using resource As New DataAccess.OracleCommon
                                Dim objParam = New With {.P_USER_ID = _lstUserID(i),
                                                         .P_GROUP_ID = _groupID,
                                                         .P_USERNAME = log.Username}

                                If objParam IsNot Nothing Then
                                    For Each info As PropertyInfo In objParam.GetType().GetProperties()
                                        Dim bOut As Boolean = False
                                        Dim para = resource.GetParameter(info.Name, info.GetValue(objParam, Nothing), bOut)
                                        If para IsNot Nothing Then
                                            cmd.Parameters.Add(para)
                                        End If
                                    Next
                                End If
                                dem = cmd.ExecuteNonQuery()
                            End Using
                        Next
                        cmd.Transaction.Commit()
                    Catch ex As Exception
                        cmd.Transaction.Rollback()
                    Finally
                        'Dispose all resource
                        cmd.Dispose()
                        conn.Close()
                        conn.Dispose()
                    End Try
                End Using
            End Using
        End Using

        Return True
    End Function

    Public Function DeleteUserGroup(ByVal _groupID As Decimal, ByVal _lstUserID As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim lstGroup As SE_GROUP = (From p In Context.SE_GROUP Select p Where p.ID = _groupID).FirstOrDefault
        If lstGroup IsNot Nothing Then
            For i As Integer = 0 To _lstUserID.Count - 1
                Dim user As SE_USER = (From p In lstGroup.SE_USERS.ToList Where p.ID = _lstUserID(i) Select p).FirstOrDefault
                lstGroup.SE_USERS.Remove(user)

                Dim uID = _lstUserID(i)

                For Each per In (From p In Context.SE_USER_PERMISSION Where p.USER_ID = uID And p.GROUP_ID = _groupID)
                    Context.SE_USER_PERMISSION.DeleteObject(per)
                Next
                For Each org In (From p In Context.SE_USER_ORG_ACCESS Where p.USER_ID = uID And p.GROUP_ID = _groupID)
                    Context.SE_USER_ORG_ACCESS.DeleteObject(org)
                Next
            Next
            Context.SaveChanges(log)
        Else
            Return False
        End If
        Return True
    End Function

    Public Function SendMailConfirmUserPassword(ByVal _userid As List(Of Decimal),
                                                ByVal _subject As String,
                                                ByVal _content As String,
                                                Optional ByVal _orderBy As Decimal = 6) As Boolean
        Try
            Dim defaultFrom As String = ""
            Dim config As Dictionary(Of String, String)
            config = GetConfig(ModuleID.All)
            defaultFrom = If(config.ContainsKey("MailFrom"), config("MailFrom"), "")

            If defaultFrom = "" Then
                Return False
            End If
            'Lấy danh sách user
            Dim lst As List(Of UserDTO) = (From p In Context.SE_USER
                                           Where _userid.Contains(p.ID) And p.EMAIL IsNot Nothing
                                           Select New UserDTO With {
                                           .FULLNAME = p.FULLNAME,
                                           .EMAIL = p.EMAIL,
                                           .PASSWORD = p.PASSWORD}).ToList
            For Each user As UserDTO In lst
                Using EncryptData As New EncryptData
                    If user.EMAIL <> "" AndAlso RegularExpressions.Regex.IsMatch(user.EMAIL, "^([a-zA-Z0-9_\.-]+)@([a-zA-Z0-9_\.-]+)\.([a-zA-Z\.]{2,6})$") Then
                        InsertMail(defaultFrom, user.EMAIL, _subject, _content, "", "", "", False, _orderBy)
                    End If
                End Using
            Next
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetUserNeedSendMail(ByVal _groupid As Decimal) As List(Of UserDTO)
        Try
            Dim lst As List(Of UserDTO) = (From p In Context.SE_USER
                                           From n In p.SE_GROUPS
                                           Where n.ID = _groupid And p.IS_CHANGE_PASS >= 0
                                           Select New UserDTO With {
                                               .ID = p.ID,
                                               .FULLNAME = p.FULLNAME,
                                               .USERNAME = p.USERNAME,
                                               .IS_CHANGE_PASS = p.IS_CHANGE_PASS,
                                               .EMAIL = p.EMAIL}).ToList
            For i = lst.Count - 1 To 0 Step -1
                If lst(i).EMAIL = "" OrElse Not RegularExpressions.Regex.IsMatch(lst(i).EMAIL, "^([a-zA-Z0-9_\.-]+)@([a-zA-Z0-9_\.-]+)\.([a-zA-Z\.]{2,6})$") Then
                    lst.RemoveAt(i)
                End If
            Next
            Return lst
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Group Function"

    Public Function GetGroupFunctionPermision(ByVal _groupID As Decimal,
                                                ByVal _filter As GroupFunctionDTO,
                                                ByVal PageIndex As Integer,
                                                ByVal PageSize As Integer,
                                                ByRef Total As Integer,
                                                Optional ByVal Sorts As String = "FUNCTION_NAME asc") As List(Of GroupFunctionDTO)
        Dim query = (From p In Context.SE_GROUP_PERMISSION
                     Where p.GROUP_ID = _groupID
                     Select New GroupFunctionDTO With {
                         .ID = p.ID,
                         .FUNCTION_ID = p.FUNCTION_ID,
                         .FUNCTION_CODE = p.SE_FUNCTION.FID,
                         .FUNCTION_NAME = p.SE_FUNCTION.NAME,
                         .GROUP_ID = p.GROUP_ID,
                         .MODULE_NAME = p.SE_FUNCTION.SE_MODULE.NAME_DESC,
                         .ALLOW_CREATE = p.ALLOW_CREATE,
                         .ALLOW_DELETE = p.ALLOW_DELETE,
                         .ALLOW_EXPORT = p.ALLOW_EXPORT,
                         .ALLOW_IMPORT = p.ALLOW_IMPORT,
                         .ALLOW_MODIFY = p.ALLOW_MODIFY,
                         .ALLOW_PRINT = p.ALLOW_PRINT,
                         .ALLOW_SPECIAL1 = p.ALLOW_SPECIAL1,
                         .ALLOW_SPECIAL2 = p.ALLOW_SPECIAL2,
                         .ALLOW_SPECIAL3 = p.ALLOW_SPECIAL3,
                         .ALLOW_SPECIAL4 = p.ALLOW_SPECIAL4,
                         .ORDER_BY = p.SE_FUNCTION.SE_MODULE.ORDER_BY,
                         .FUNCTION_GROUP_ID = p.SE_FUNCTION.SE_FUNCTION_GROUP.ID,
                         .FUNCTION_GROUP_NAME = p.SE_FUNCTION.SE_FUNCTION_GROUP.NAME,
                         .ALLOW_SPECIAL5 = p.ALLOW_SPECIAL5})

        If _filter.FUNCTION_NAME <> "" Then
            query = query.Where(Function(p) p.FUNCTION_NAME.ToUpper.Contains(_filter.FUNCTION_NAME.ToUpper) Or
                                    p.FUNCTION_CODE.ToUpper.Contains(_filter.FUNCTION_NAME.ToUpper))
        End If

        If _filter.MODULE_NAME <> "" Then
            query = query.Where(Function(p) p.MODULE_NAME = _filter.MODULE_NAME)
        End If
        If _filter.FUNCTION_GROUP_NAME <> "" Then
            query = query.Where(Function(p) p.FUNCTION_GROUP_NAME = _filter.FUNCTION_GROUP_NAME)
        End If
        query = query.OrderBy(Sorts)
        Total = query.Count
        query = query.Skip(PageIndex * PageSize).Take(PageSize)
        Return query.ToList
    End Function

    Public Function GetGroupFunctionNotPermision(ByVal _groupID As Decimal,
                                                ByVal _filter As FunctionDTO,
                                                ByVal PageIndex As Integer,
                                                ByVal PageSize As Integer,
                                                ByRef Total As Integer,
                                                ByVal log As UserLog,
                                                Optional ByVal Sorts As String = "NAME asc") As List(Of FunctionDTO)

        Dim lstTemp As New List(Of SE_FUNCTION)
        If log.Username = "ADMIN" Then
            lstTemp = (From p In Context.SE_FUNCTION
                       From m In Context.SE_MODULE.Where(Function(f) f.ID = p.MODULE_ID).DefaultIfEmpty
                       Where p.ACTFLG = "A" And m.IS_ACTIVE = 1 Select p).ToList
        Else
            lstTemp = (From p In Context.SE_FUNCTION
                       From m In Context.SE_MODULE.Where(Function(f) f.ID = p.MODULE_ID).DefaultIfEmpty
                       From q In Context.SE_USER_PERMISSION.Where(Function(f) f.FUNCTION_ID = p.ID)
                       From u In Context.SE_USER.Where(Function(f) f.ID = q.USER_ID And f.USERNAME = log.Username)
                       Where p.ACTFLG = "A" And m.IS_ACTIVE = 1
                       Select p).ToList()
        End If

        If _filter.MODULE_NAME <> "" Then
            lstTemp = lstTemp.Where(Function(p) p.SE_MODULE.NAME_DESC.ToUpper.Contains(_filter.MODULE_NAME.ToUpper)).ToList()
        End If
        If _filter.FUNCTION_GROUP_NAME <> "" Then
            lstTemp = lstTemp.Where(Function(p) p.SE_FUNCTION_GROUP.NAME.ToUpper.Contains(_filter.FUNCTION_GROUP_NAME.ToUpper)).ToList()
        End If
        If _filter.NAME <> "" Then
            lstTemp = lstTemp.Where(Function(p) p.NAME.ToUpper.Contains(_filter.NAME.ToUpper)).ToList()
        End If

        If _filter.FUNCTION_NAME_FID <> "" Then
            lstTemp = lstTemp.Where(Function(p) p.NAME.ToUpper.Contains(_filter.FUNCTION_NAME_FID.ToUpper)).ToList()
        End If
        If _filter.MODULE_NAME_FID <> "" Then
            lstTemp = lstTemp.Where(Function(p) p.SE_MODULE.NAME_DESC.ToUpper.Contains(_filter.MODULE_NAME_FID.ToUpper)).ToList()
        End If
        If _filter.FUNCTION_GROUP_NAME_FID <> "" Then
            lstTemp = lstTemp.Where(Function(p) p.SE_FUNCTION_GROUP.NAME.ToUpper.Contains(_filter.FUNCTION_GROUP_NAME_FID.ToUpper)).ToList()
        End If

        Dim lst1 = (From p In lstTemp Select p.ID)
        Dim lst2 = (From p In Context.SE_GROUP_PERMISSION Where p.GROUP_ID = _groupID Select p.FUNCTION_ID)
        Dim lstID As List(Of Decimal) = lst1.Except(lst2).ToList

        Dim query = (From p In Context.SE_FUNCTION Where lstID.Contains(p.ID) Order By p.NAME Select New FunctionDTO With {
                                                    .ID = p.ID,
                                                    .NAME = p.NAME,
                                                    .MODULE_NAME = p.SE_MODULE.NAME_DESC,
                                                    .FID = p.FID,
                                                    .ORDER_BY = p.SE_MODULE.ORDER_BY,
                                                    .FUNCTION_GROUP_NAME = p.SE_FUNCTION_GROUP.NAME})

        query = query.AsQueryable().OrderBy(Sorts)
        Total = query.Count
        query = query.Skip(PageIndex * PageSize).Take(PageSize)
        Return query.ToList
    End Function

    Public Function InsertGroupFunction(ByVal _lstGroupFunc As List(Of GroupFunctionDTO), ByVal log As UserLog) As Boolean
        Try
            For i As Integer = 0 To _lstGroupFunc.Count - 1

                Dim itemAdd = _lstGroupFunc(i)

                Dim functionCheck = Context.SE_GROUP_PERMISSION.FirstOrDefault(Function(p) p.FUNCTION_ID = itemAdd.FUNCTION_ID AndAlso p.GROUP_ID = itemAdd.GROUP_ID)

                If functionCheck IsNot Nothing Then Continue For

                Dim _new As New SE_GROUP_PERMISSION
                _new.ID = Utilities.GetNextSequence(Context, Context.SE_GROUP_PERMISSION.EntitySet.Name)
                _new.ALLOW_CREATE = _lstGroupFunc(i).ALLOW_CREATE
                _new.ALLOW_MODIFY = _lstGroupFunc(i).ALLOW_MODIFY
                _new.ALLOW_DELETE = _lstGroupFunc(i).ALLOW_DELETE
                _new.ALLOW_PRINT = _lstGroupFunc(i).ALLOW_PRINT
                _new.ALLOW_IMPORT = _lstGroupFunc(i).ALLOW_IMPORT
                _new.ALLOW_EXPORT = _lstGroupFunc(i).ALLOW_EXPORT
                _new.ALLOW_SPECIAL1 = _lstGroupFunc(i).ALLOW_SPECIAL1
                _new.ALLOW_SPECIAL2 = _lstGroupFunc(i).ALLOW_SPECIAL2
                _new.ALLOW_SPECIAL3 = _lstGroupFunc(i).ALLOW_SPECIAL3
                _new.ALLOW_SPECIAL4 = _lstGroupFunc(i).ALLOW_SPECIAL4
                _new.ALLOW_SPECIAL5 = _lstGroupFunc(i).ALLOW_SPECIAL5
                _new.FUNCTION_ID = _lstGroupFunc(i).FUNCTION_ID
                _new.GROUP_ID = _lstGroupFunc(i).GROUP_ID
                _new.CREATED_DATE = DateTime.Now
                _new.CREATED_BY = log.Username
                _new.CREATED_LOG = log.ComputerName
                _new.MODIFIED_DATE = DateTime.Now
                _new.MODIFIED_BY = log.Username
                _new.MODIFIED_LOG = log.ComputerName
                Context.SE_GROUP_PERMISSION.AddObject(_new)
            Next
            'Context.SaveChanges()
            For i As Integer = 0 To _lstGroupFunc.Count - 1
                Dim lst = _lstGroupFunc(i).GROUP_ID
                Dim query = (From p In Context.HUV_SE_GRP_SE_USR Where p.SE_GROUPS_ID = lst
                             Select New UserDTO With {.ID = p.SE_USERS_ID}).ToList()
                For Each ITEM In query
                    Dim _NEW1 As New SE_USER_PERMISSION
                    _NEW1.ID = Utilities.GetNextSequence(Context, Context.SE_USER_PERMISSION.EntitySet.Name)
                    _NEW1.ALLOW_CREATE = _lstGroupFunc(i).ALLOW_CREATE
                    _NEW1.ALLOW_MODIFY = _lstGroupFunc(i).ALLOW_MODIFY
                    _NEW1.ALLOW_DELETE = _lstGroupFunc(i).ALLOW_DELETE
                    _NEW1.ALLOW_PRINT = _lstGroupFunc(i).ALLOW_PRINT
                    _NEW1.ALLOW_IMPORT = _lstGroupFunc(i).ALLOW_IMPORT
                    _NEW1.ALLOW_EXPORT = _lstGroupFunc(i).ALLOW_EXPORT
                    _NEW1.ALLOW_SPECIAL1 = _lstGroupFunc(i).ALLOW_SPECIAL1
                    _NEW1.ALLOW_SPECIAL2 = _lstGroupFunc(i).ALLOW_SPECIAL2
                    _NEW1.ALLOW_SPECIAL3 = _lstGroupFunc(i).ALLOW_SPECIAL3
                    _NEW1.ALLOW_SPECIAL4 = _lstGroupFunc(i).ALLOW_SPECIAL4
                    _NEW1.ALLOW_SPECIAL5 = _lstGroupFunc(i).ALLOW_SPECIAL5
                    _NEW1.FUNCTION_ID = _lstGroupFunc(i).FUNCTION_ID
                    _NEW1.GROUP_ID = _lstGroupFunc(i).GROUP_ID
                    _NEW1.USER_ID = ITEM.ID
                    _NEW1.CREATED_DATE = DateTime.Now
                    _NEW1.CREATED_BY = log.Username
                    _NEW1.CREATED_LOG = log.ComputerName
                    _NEW1.MODIFIED_DATE = DateTime.Now
                    _NEW1.MODIFIED_BY = log.Username
                    _NEW1.MODIFIED_LOG = log.ComputerName
                    Context.SE_USER_PERMISSION.AddObject(_NEW1)
                Next
            Next

            Context.SaveChanges()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function UpdateGroupFunction(ByVal _lstGroupFunc As List(Of GroupFunctionDTO), ByVal log As UserLog) As Boolean
        Try
            Dim i As Integer
            Dim lstID As List(Of Decimal) = (From p In _lstGroupFunc Select p.ID).ToList
            Dim objUpdate As List(Of SE_GROUP_PERMISSION) = (From p In Context.SE_GROUP_PERMISSION Where lstID.Contains(p.ID) Select p).ToList
            For i = 0 To objUpdate.Count - 1
                Dim func As GroupFunctionDTO = _lstGroupFunc.Find(Function(item As GroupFunctionDTO) item.ID = objUpdate(i).ID)
                If func IsNot Nothing Then
                    objUpdate(i).ALLOW_CREATE = If(func.ALLOW_CREATE, 1, 0)
                    objUpdate(i).ALLOW_DELETE = If(func.ALLOW_DELETE, 1, 0)
                    objUpdate(i).ALLOW_EXPORT = If(func.ALLOW_EXPORT, 1, 0)
                    objUpdate(i).ALLOW_IMPORT = If(func.ALLOW_IMPORT, 1, 0)
                    objUpdate(i).ALLOW_MODIFY = If(func.ALLOW_MODIFY, 1, 0)
                    objUpdate(i).ALLOW_PRINT = If(func.ALLOW_PRINT, 1, 0)
                    objUpdate(i).ALLOW_SPECIAL1 = If(func.ALLOW_SPECIAL1, 1, 0)
                    objUpdate(i).ALLOW_SPECIAL2 = If(func.ALLOW_SPECIAL2, 1, 0)
                    objUpdate(i).ALLOW_SPECIAL3 = If(func.ALLOW_SPECIAL3, 1, 0)
                    objUpdate(i).ALLOW_SPECIAL4 = If(func.ALLOW_SPECIAL4, 1, 0)
                    objUpdate(i).ALLOW_SPECIAL5 = If(func.ALLOW_SPECIAL5, 1, 0)
                    objUpdate(i).MODIFIED_DATE = DateTime.Now
                    objUpdate(i).MODIFIED_BY = log.Username
                    objUpdate(i).MODIFIED_LOG = log.ComputerName
                End If
            Next
            '  Context.SaveChanges(log)
            For i = 0 To _lstGroupFunc.Count - 1
                Dim j As Integer
                Dim lst = _lstGroupFunc(i).GROUP_ID
                Dim lst1 = _lstGroupFunc(i).FUNCTION_ID
                Dim query As List(Of UserFunctionDTO) = (From p In Context.HUV_SE_GRP_SE_USR
                                                         From q In Context.SE_USER_PERMISSION.Where(Function(f) f.USER_ID = p.SE_USERS_ID).DefaultIfEmpty
                                                         Where p.SE_GROUPS_ID = lst And q.FUNCTION_ID = lst1 Select New UserFunctionDTO With {.ID = q.ID}).ToList

                'Dim _lstUserFunc As List(Of UserFunctionDTO) = Decimal.Parse(query.ToString)
                Dim lstID1 As List(Of Decimal) = (From p In query Select p.ID).ToList
                Dim objUpdate1 As List(Of SE_USER_PERMISSION) = (From p In Context.SE_USER_PERMISSION Where lstID1.Contains(p.ID) Select p).ToList
                For j = 0 To objUpdate1.Count - 1
                    'Dim func As UserFunctionDTO = query.Find(Function(item As UserFunctionDTO) item.ID = objUpdate(i).ID)
                    'Dim func1 As UserFunctionDTO = query.Find(Function(e As UserFunctionDTO) e.ID = objUpdate1(j).ID)
                    Dim func As GroupFunctionDTO = _lstGroupFunc.Find(Function(item As GroupFunctionDTO) item.ID = objUpdate(i).ID)
                    If func IsNot Nothing Then
                        objUpdate1(j).ALLOW_CREATE = If(func.ALLOW_CREATE, 1, 0)
                        objUpdate1(j).ALLOW_DELETE = If(func.ALLOW_DELETE, 1, 0)
                        objUpdate1(j).ALLOW_EXPORT = If(func.ALLOW_EXPORT, 1, 0)
                        objUpdate1(j).ALLOW_IMPORT = If(func.ALLOW_IMPORT, 1, 0)
                        objUpdate1(j).ALLOW_MODIFY = If(func.ALLOW_MODIFY, 1, 0)
                        objUpdate1(j).ALLOW_PRINT = If(func.ALLOW_PRINT, 1, 0)
                        objUpdate1(j).ALLOW_SPECIAL1 = If(func.ALLOW_SPECIAL1, 1, 0)
                        objUpdate1(j).ALLOW_SPECIAL2 = If(func.ALLOW_SPECIAL2, 1, 0)
                        objUpdate1(j).ALLOW_SPECIAL3 = If(func.ALLOW_SPECIAL3, 1, 0)
                        objUpdate1(j).ALLOW_SPECIAL4 = If(func.ALLOW_SPECIAL4, 1, 0)
                        objUpdate1(j).ALLOW_SPECIAL5 = If(func.ALLOW_SPECIAL5, 1, 0)
                    End If
                Next
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function DeleteGroupFunction(ByVal _lstID As List(Of Decimal)) As Boolean
        Try
            'Using conMng As New ConnectionManager
            '    Using conn As New OracleConnection(conMng.GetConnectionString())
            '        conn.Open()
            '        Using cmd As New OracleCommand()
            '            Try
            '                cmd.Connection = conn
            '                cmd.Transaction = cmd.Connection.BeginTransaction()
            '                For i As Integer = 0 To _lstID.Count - 1
            '                    cmd.CommandText = "DELETE SE_GROUP_PERMISSION WHERE ID =" & _lstID(i)
            '                    cmd.ExecuteNonQuery()
            '                Next
            '                cmd.Transaction.Commit()
            '            Catch ex As Exception
            '                cmd.Transaction.Rollback()
            '            Finally
            '                'Dispose all resource
            '                cmd.Dispose()
            '                conn.Close()
            '                conn.Dispose()
            '            End Try
            '        End Using
            '    End Using
            'End Using
            Dim str As String = ""
            For i As Integer = 0 To _lstID.Count - 1
                str += _lstID(i) & ";"
            Next
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.DELETE_GROUP_FUNCTION",
                                New With {.P_LIST_ID = str})
            End Using
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetCurMIDByFID(ByVal _fid As String) As String
        Try
            Dim mid = ""

            Dim query = From p In Context.SE_FUNCTION
                        From m In Context.SE_MODULE.Where(Function(f) f.ID = p.MODULE_ID)
                        Where p.FID.ToUpper().Equals(_fid.ToUpper)
            If query.Any Then
                mid = query.FirstOrDefault.m.MID
            End If
            Return mid
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function CopyGroupFunction(ByVal groupCopyID As Decimal, ByVal groupID As Decimal, ByVal log As UserLog) As Boolean
        ' bổ sung delete phục vụ cho chức năng copy function
        Dim lstDel = (From p In Context.SE_GROUP_PERMISSION Where p.GROUP_ID = groupID).ToList
        For Each itm In lstDel
            Context.SE_GROUP_PERMISSION.DeleteObject(itm)
        Next
        Context.SaveChanges(log)

        Dim _lstGroupFunc = (From p In Context.SE_GROUP_PERMISSION Where p.GROUP_ID = groupCopyID
                             Select New GroupFunctionDTO With {.GROUP_ID = groupID,
                                                               .FUNCTION_ID = p.FUNCTION_ID,
                                                              .ALLOW_CREATE = p.ALLOW_CREATE,
                                                               .ALLOW_DELETE = p.ALLOW_DELETE,
                                                               .ALLOW_EXPORT = p.ALLOW_EXPORT,
                                                               .ALLOW_IMPORT = p.ALLOW_IMPORT,
                                                               .ALLOW_MODIFY = p.ALLOW_MODIFY,
                                                               .ALLOW_PRINT = p.ALLOW_PRINT,
                                                               .ALLOW_SPECIAL1 = p.ALLOW_SPECIAL1,
                                                               .ALLOW_SPECIAL2 = p.ALLOW_SPECIAL2,
                                                               .ALLOW_SPECIAL3 = p.ALLOW_SPECIAL3,
                                                               .ALLOW_SPECIAL4 = p.ALLOW_SPECIAL4,
                                                               .ALLOW_SPECIAL5 = p.ALLOW_SPECIAL5}).ToList
        For i As Integer = 0 To _lstGroupFunc.Count - 1

            Dim itemAdd = _lstGroupFunc(i)

            Dim functionCheck = Context.SE_GROUP_PERMISSION.FirstOrDefault(Function(p) p.FUNCTION_ID = itemAdd.FUNCTION_ID AndAlso p.GROUP_ID = itemAdd.GROUP_ID)

            If functionCheck IsNot Nothing Then Continue For

            Dim _new As New SE_GROUP_PERMISSION
            _new.ID = Utilities.GetNextSequence(Context, Context.SE_GROUP_PERMISSION.EntitySet.Name)
            _new.ALLOW_CREATE = _lstGroupFunc(i).ALLOW_CREATE
            _new.ALLOW_MODIFY = _lstGroupFunc(i).ALLOW_MODIFY
            _new.ALLOW_DELETE = _lstGroupFunc(i).ALLOW_DELETE
            _new.ALLOW_PRINT = _lstGroupFunc(i).ALLOW_PRINT
            _new.ALLOW_IMPORT = _lstGroupFunc(i).ALLOW_IMPORT
            _new.ALLOW_EXPORT = _lstGroupFunc(i).ALLOW_EXPORT
            _new.ALLOW_SPECIAL1 = _lstGroupFunc(i).ALLOW_SPECIAL1
            _new.ALLOW_SPECIAL2 = _lstGroupFunc(i).ALLOW_SPECIAL2
            _new.ALLOW_SPECIAL3 = _lstGroupFunc(i).ALLOW_SPECIAL3
            _new.ALLOW_SPECIAL4 = _lstGroupFunc(i).ALLOW_SPECIAL4
            _new.ALLOW_SPECIAL5 = _lstGroupFunc(i).ALLOW_SPECIAL5
            _new.FUNCTION_ID = _lstGroupFunc(i).FUNCTION_ID
            _new.GROUP_ID = _lstGroupFunc(i).GROUP_ID
            Context.SE_GROUP_PERMISSION.AddObject(_new)
            Context.SaveChanges(log)
        Next
        Return True
    End Function

#End Region

#Region "Group Report"

    Public Function GetGroupReportList(ByVal _groupID As Decimal) As List(Of GroupReportDTO)
        Dim query = (From p In Context.SE_REPORT Order By p.NAME
                     Select New GroupReportDTO With {
                         .ID = p.ID,
                         .REPORT_NAME = p.NAME,
                         .MODULE_NAME = p.SE_MODULE.NAME,
                         .IS_USE = ((From n In p.SE_GROUPS Where n.ID = _groupID).Count > 0)})
        Return query.ToList
    End Function

    Public Function GetGroupReportListFilter(ByVal _groupID As Decimal, ByVal _filter As GroupReportDTO) As List(Of GroupReportDTO)
        Dim query = (From p In Context.SE_REPORT Order By p.NAME
                     Where (_filter.REPORT_NAME = Nothing Or p.NAME.ToUpper = _filter.REPORT_NAME.ToUpper) And
                     (_filter.MODULE_ID = Nothing Or p.SE_MODULE.ID = _filter.MODULE_ID)
                     Select New GroupReportDTO With {
                         .ID = p.ID,
                         .REPORT_NAME = p.NAME,
                         .MODULE_NAME = p.SE_MODULE.NAME,
                         .IS_USE = ((From n In p.SE_GROUPS Where n.ID = _groupID).Count > 0)})
        Return query.ToList
    End Function

    Public Function UpdateGroupReport(ByVal _groupID As Decimal, ByVal _lstReport As List(Of GroupReportDTO)) As Boolean
        Dim query As SE_GROUP
        query = (From p In Context.SE_GROUP
                 Where p.ID = _groupID
                 Select p).FirstOrDefault
        Dim Ids As List(Of Decimal) = (From p In _lstReport Select p.ID).ToList
        Dim lst = (From p In Context.SE_REPORT Where Ids.Contains(p.ID)).ToList
        If query IsNot Nothing Then
            query.SE_REPORTS.Clear()
            For i As Integer = 0 To lst.Count - 1
                query.SE_REPORTS.Add(lst(i))
            Next
            Context.SaveChanges()
        Else
            Return False
        End If
        Return True
    End Function

#End Region

#Region "User Function"

    Public Function GetUserFunctionPermision(ByVal _UserID As Decimal,
                                                ByVal _filter As UserFunctionDTO,
                                                ByVal PageIndex As Integer,
                                                ByVal PageSize As Integer,
                                                ByRef Total As Integer,
                                                Optional ByVal Sorts As String = "FUNCTION_NAME asc") As List(Of UserFunctionDTO)
        Try
            Dim query = (From p In Context.SE_USER_PERMISSION
                         From f In Context.SE_FUNCTION.Where(Function(f) f.ID = p.FUNCTION_ID)
                         Where p.USER_ID = _UserID
                         Select New UserFunctionDTO With {
                             .ID = p.ID,
                             .FUNCTION_ID = p.FUNCTION_ID,
                             .FUNCTION_CODE = f.FID,
                             .FUNCTION_NAME = f.NAME,
                             .USER_ID = p.USER_ID,
                             .MODULE_NAME = f.SE_MODULE.NAME_DESC,
                             .ALLOW_CREATE = p.ALLOW_CREATE,
                             .ALLOW_DELETE = p.ALLOW_DELETE,
                             .ALLOW_EXPORT = p.ALLOW_EXPORT,
                             .ALLOW_IMPORT = p.ALLOW_IMPORT,
                             .ALLOW_MODIFY = p.ALLOW_MODIFY,
                             .ALLOW_PRINT = p.ALLOW_PRINT,
                             .ORDER_BY = f.SE_MODULE.ORDER_BY,
                             .ALLOW_SPECIAL1 = p.ALLOW_SPECIAL1,
                             .ALLOW_SPECIAL2 = p.ALLOW_SPECIAL2,
                             .ALLOW_SPECIAL3 = p.ALLOW_SPECIAL3,
                             .ALLOW_SPECIAL4 = p.ALLOW_SPECIAL4,
                             .ALLOW_SPECIAL5 = p.ALLOW_SPECIAL5,
                             .FUNCTION_GROUP_NAME = f.SE_FUNCTION_GROUP.NAME,
                             .FUNCTION_GROUP_ID = f.SE_FUNCTION_GROUP.ID})

            If _filter.FUNCTION_NAME <> "" Then
                query = query.Where(Function(p) p.FUNCTION_NAME.ToUpper.Contains(_filter.FUNCTION_NAME.ToUpper) Or
                                        p.FUNCTION_CODE.ToUpper.Contains(_filter.FUNCTION_NAME.ToUpper))
            End If

            If _filter.MODULE_NAME <> "" Then
                query = query.Where(Function(p) p.MODULE_NAME = _filter.MODULE_NAME)
            End If
            If _filter.FUNCTION_GROUP_NAME <> "" Then
                query = query.Where(Function(p) p.FUNCTION_GROUP_NAME = _filter.FUNCTION_GROUP_NAME)
            End If
            query = query.OrderBy(Sorts)
            Total = query.Count
            query = query.Skip(PageIndex * PageSize).Take(PageSize)
            Return query.ToList

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''
    Public Function UpdateUserFunctionException(ByVal _lstUserFunc As List(Of UserFunctionDTO), ByVal log As UserLog) As Boolean
        Try
            Dim i As Integer
            Dim lstID As List(Of Decimal) = (From p In _lstUserFunc Select p.ID).ToList
            Dim objUpdate As List(Of SE_USER_PERMISSION_ORG) = (From p In Context.SE_USER_PERMISSION_ORG Where lstID.Contains(p.ID) Select p).ToList
            For i = 0 To objUpdate.Count - 1
                Dim func As UserFunctionDTO = _lstUserFunc.Find(Function(item As UserFunctionDTO) item.ID = objUpdate(i).ID)
                If func IsNot Nothing Then
                    objUpdate(i).ALLOW_CREATE = If(func.ALLOW_CREATE, 1, 0)
                    objUpdate(i).ALLOW_DELETE = If(func.ALLOW_DELETE, 1, 0)
                    objUpdate(i).ALLOW_EXPORT = If(func.ALLOW_EXPORT, 1, 0)
                    objUpdate(i).ALLOW_IMPORT = If(func.ALLOW_IMPORT, 1, 0)
                    objUpdate(i).ALLOW_MODIFY = If(func.ALLOW_MODIFY, 1, 0)
                    objUpdate(i).ALLOW_PRINT = If(func.ALLOW_PRINT, 1, 0)
                    objUpdate(i).ALLOW_SPECIAL1 = If(func.ALLOW_SPECIAL1, 1, 0)
                    objUpdate(i).ALLOW_SPECIAL2 = If(func.ALLOW_SPECIAL2, 1, 0)
                    objUpdate(i).ALLOW_SPECIAL3 = If(func.ALLOW_SPECIAL3, 1, 0)
                    objUpdate(i).ALLOW_SPECIAL4 = If(func.ALLOW_SPECIAL4, 1, 0)
                    objUpdate(i).ALLOW_SPECIAL5 = If(func.ALLOW_SPECIAL5, 1, 0)
                    If func.LST_ORG IsNot Nothing Then
                        Dim lstDel = (From p In Context.SE_USER_PERMISSION_ORG_DETAIL Where p.USER_ID = func.USER_ID And p.FUNCTION_ID = func.FUNCTION_ID).ToList
                        For Each itm In lstDel
                            Context.SE_USER_PERMISSION_ORG_DETAIL.DeleteObject(itm)
                        Next
                        For Each item In func.LST_ORG
                            Dim objOrg As New SE_USER_PERMISSION_ORG_DETAIL
                            objOrg.ID = Utilities.GetNextSequence(Context, Context.SE_USER_PERMISSION_ORG_DETAIL.EntitySet.Name)
                            objOrg.ORG_ID = item
                            objOrg.FUNCTION_ID = _lstUserFunc(i).FUNCTION_ID
                            objOrg.USER_ID = _lstUserFunc(i).USER_ID
                            Context.SE_USER_PERMISSION_ORG_DETAIL.AddObject(objOrg)
                        Next
                    End If
                    If func.LST_WL IsNot Nothing Then
                        Dim lstDel = (From p In Context.SE_USER_PERMISSION_ORG_WL Where p.USER_ID = func.USER_ID And p.FUNCTION_ID = func.FUNCTION_ID).ToList
                        For Each itm In lstDel
                            Context.SE_USER_PERMISSION_ORG_WL.DeleteObject(itm)
                        Next
                        For Each item In func.LST_WL
                            Dim objWl As New SE_USER_PERMISSION_ORG_WL
                            objWl.ID = Utilities.GetNextSequence(Context, Context.SE_USER_PERMISSION_ORG_WL.EntitySet.Name)
                            objWl.WL_ID = item
                            objWl.FUNCTION_ID = _lstUserFunc(i).FUNCTION_ID
                            objWl.USER_ID = _lstUserFunc(i).USER_ID
                            Context.SE_USER_PERMISSION_ORG_WL.AddObject(objWl)
                        Next
                    End If
                End If
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function DeleteUserFunctionException(ByVal _lstID As List(Of Decimal)) As Boolean
        Try
            Using conMng As New ConnectionManager
                Using conn As New OracleConnection(conMng.GetConnectionString())
                    conn.Open()
                    Using cmd As New OracleCommand()
                        Try
                            cmd.Connection = conn
                            cmd.Transaction = cmd.Connection.BeginTransaction()
                            For i As Integer = 0 To _lstID.Count - 1
                                cmd.CommandText = "DELETE SE_USER_PERMISSION_ORG_DETAIL WHERE USER_ID = (SELECT USER_ID FROM SE_USER_PERMISSION_ORG WHERE ID =" & _lstID(i) & ") AND FUNCTION_ID = (SELECT FUNCTION_ID FROM SE_USER_PERMISSION_ORG WHERE ID =" & _lstID(i) & ")"
                                cmd.ExecuteNonQuery()
                                cmd.CommandText = "DELETE SE_USER_PERMISSION_ORG_WL WHERE USER_ID = (SELECT USER_ID FROM SE_USER_PERMISSION_ORG WHERE ID =" & _lstID(i) & ") AND FUNCTION_ID = (SELECT FUNCTION_ID FROM SE_USER_PERMISSION_ORG WHERE ID =" & _lstID(i) & ")"
                                cmd.ExecuteNonQuery()
                                cmd.CommandText = "DELETE SE_USER_PERMISSION_ORG WHERE ID =" & _lstID(i)
                                cmd.ExecuteNonQuery()
                            Next
                            cmd.Transaction.Commit()
                        Catch ex As Exception
                            cmd.Transaction.Rollback()
                        Finally
                            'Dispose all resource
                            cmd.Dispose()
                            conn.Close()
                            conn.Dispose()
                        End Try
                    End Using
                End Using
            End Using

            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetUserFunctionPermisionException(ByVal _UserID As Decimal,
                                                ByVal _filter As UserFunctionDTO,
                                                ByVal PageIndex As Integer,
                                                ByVal PageSize As Integer,
                                                ByRef Total As Integer,
                                                Optional ByVal Sorts As String = "FUNCTION_NAME asc") As List(Of UserFunctionDTO)
        Try
            Dim query = (From p In Context.SE_USER_PERMISSION_ORG
                         From f In Context.SE_FUNCTION.Where(Function(f) f.ID = p.FUNCTION_ID)
                         From o In Context.HU_ORGANIZATION.Where(Function(f1) f1.ID = p.ORG_ID).DefaultIfEmpty
                         From w In Context.HU_WORK_PLACE.Where(Function(f2) f2.ID = p.LOCATION_ID).DefaultIfEmpty
                         Where p.USER_ID = _UserID
                         Select New UserFunctionDTO With {
                             .ID = p.ID,
                             .ORG_ID = p.ORG_ID,
                             .WORK_PLACE_ID = p.LOCATION_ID,
                             .ORG_NAME = o.NAME_VN,
                             .WORK_PLACE_NAME = w.NAME_VN,
                             .FUNCTION_ID = p.FUNCTION_ID,
                             .FUNCTION_CODE = f.FID,
                             .FUNCTION_NAME = f.NAME,
                             .USER_ID = p.USER_ID,
                             .MODULE_NAME = f.SE_MODULE.NAME_DESC,
                             .ALLOW_CREATE = p.ALLOW_CREATE,
                             .ALLOW_DELETE = p.ALLOW_DELETE,
                             .ALLOW_EXPORT = p.ALLOW_EXPORT,
                             .ALLOW_IMPORT = p.ALLOW_IMPORT,
                             .ALLOW_MODIFY = p.ALLOW_MODIFY,
                             .ALLOW_PRINT = p.ALLOW_PRINT,
                             .ORDER_BY = f.SE_MODULE.ORDER_BY,
                             .ALLOW_SPECIAL1 = p.ALLOW_SPECIAL1,
                             .ALLOW_SPECIAL2 = p.ALLOW_SPECIAL2,
                             .ALLOW_SPECIAL3 = p.ALLOW_SPECIAL3,
                             .ALLOW_SPECIAL4 = p.ALLOW_SPECIAL4,
                             .ALLOW_SPECIAL5 = p.ALLOW_SPECIAL5,
                             .FUNCTION_GROUP_NAME = f.SE_FUNCTION_GROUP.NAME,
                             .FUNCTION_GROUP_ID = f.SE_FUNCTION_GROUP.ID})
            If _filter.FUNCTION_NAME <> "" Then
                query = query.Where(Function(p) p.FUNCTION_NAME.ToUpper.Contains(_filter.FUNCTION_NAME.ToUpper) Or
                                        p.FUNCTION_CODE.ToUpper.Contains(_filter.FUNCTION_NAME.ToUpper))
            End If

            If _filter.MODULE_NAME <> "" Then
                query = query.Where(Function(p) p.MODULE_NAME = _filter.MODULE_NAME)
            End If
            If _filter.FUNCTION_GROUP_NAME <> "" Then
                query = query.Where(Function(p) p.FUNCTION_GROUP_NAME = _filter.FUNCTION_GROUP_NAME)
            End If
            If _filter.WORK_PLACE_NAME <> "" Then
                query = query.Where(Function(p) p.WORK_PLACE_NAME = _filter.WORK_PLACE_NAME)
            End If

            If _filter.LST_ORG IsNot Nothing AndAlso _filter.LST_ORG.Count > 0 Then
                query = query.Where(Function(p) _filter.LST_ORG.Contains(p.ORG_ID))
            End If

            query = query.OrderBy(Sorts)
            Total = query.Count
            query = query.Skip(PageIndex * PageSize).Take(PageSize)
            Dim rs = query.ToList
            For Each item In rs
                Dim lstOrg = (From p In Context.SE_USER_PERMISSION_ORG_DETAIL Where p.USER_ID = item.USER_ID And p.FUNCTION_ID = item.FUNCTION_ID Select p.ORG_ID).ToList
                If lstOrg IsNot Nothing AndAlso lstOrg.Count > 0 Then
                    Dim lstOrgId = New List(Of Decimal)
                    For Each orgId In lstOrg
                        lstOrgId.Add(orgId)
                    Next
                    item.LST_ORG = lstOrgId
                End If
                Dim lstWl = (From p In Context.SE_USER_PERMISSION_ORG_WL Where p.USER_ID = item.USER_ID And p.FUNCTION_ID = item.FUNCTION_ID Select p.WL_ID).ToList
                If lstWl IsNot Nothing AndAlso lstWl.Count > 0 Then
                    Dim lstWlId = New List(Of Decimal)
                    For Each wlId In lstWl
                        lstWlId.Add(wlId)
                    Next
                    item.LST_WL = lstWlId
                End If
            Next
            Return rs

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''
    Public Function GetUserFunctionNotPermisionException(ByVal _UserID As Decimal,
                                                ByVal _filter As FunctionDTO,
                                                ByVal PageIndex As Integer,
                                                ByVal PageSize As Integer,
                                                ByRef Total As Integer,
                                                ByVal log As UserLog,
                                                Optional ByVal Sorts As String = "NAME asc") As List(Of FunctionDTO)
        Try
            Dim lstTemp As New List(Of SE_FUNCTION)
            If log.Username = "ADMIN" Then
                lstTemp = (From p In Context.SE_FUNCTION
                           From m In Context.SE_MODULE.Where(Function(f) f.ID = p.MODULE_ID).DefaultIfEmpty
                           Where p.ACTFLG = "A" And m.IS_ACTIVE = 1 Select p).ToList
            Else
                lstTemp = (From p In Context.SE_FUNCTION
                           From m In Context.SE_MODULE.Where(Function(f) f.ID = p.MODULE_ID).DefaultIfEmpty
                           From q In Context.SE_USER_PERMISSION_ORG.Where(Function(f) f.FUNCTION_ID = p.ID)
                           From u In Context.SE_USER.Where(Function(f) f.ID = q.USER_ID And f.USERNAME = log.Username)
                           Where p.ACTFLG = "A" And m.IS_ACTIVE = 1
                           Select p).ToList
            End If

            If _filter.MODULE_NAME <> "" Then
                lstTemp = lstTemp.Where(Function(p) p.SE_MODULE.NAME_DESC.ToUpper.Contains(_filter.MODULE_NAME.ToUpper)).ToList
            End If
            If _filter.FUNCTION_GROUP_NAME <> "" Then
                lstTemp = lstTemp.Where(Function(p) p.SE_FUNCTION_GROUP.NAME.ToUpper.Contains(_filter.FUNCTION_GROUP_NAME.ToUpper)).ToList
            End If
            If _filter.NAME <> "" Then
                lstTemp = lstTemp.Where(Function(p) p.NAME.ToUpper.Contains(_filter.NAME.ToUpper)).ToList
            End If

            If _filter.FUNCTION_NAME_FID <> "" Then
                lstTemp = lstTemp.Where(Function(p) p.NAME.ToUpper.Contains(_filter.FUNCTION_NAME_FID.ToUpper)).ToList()
            End If
            If _filter.MODULE_NAME_FID <> "" Then
                lstTemp = lstTemp.Where(Function(p) p.SE_MODULE.NAME_DESC.ToUpper.Contains(_filter.MODULE_NAME_FID.ToUpper)).ToList()
            End If
            If _filter.FUNCTION_GROUP_NAME_FID <> "" Then
                lstTemp = lstTemp.Where(Function(p) p.SE_FUNCTION_GROUP.NAME.ToUpper.Contains(_filter.FUNCTION_GROUP_NAME_FID.ToUpper)).ToList()
            End If



            Dim lst1 = (From p In lstTemp Select p.ID)
            Dim lst2 = (From p In Context.SE_USER_PERMISSION_ORG Where p.USER_ID = _UserID Select p.FUNCTION_ID)
            Dim lstID As List(Of Decimal) = lst1.Except(lst2).ToList

            Dim query = (From p In Context.SE_FUNCTION Where lstID.Contains(p.ID)
                         Order By p.NAME
                         Select New FunctionDTO With {
                             .ID = p.ID,
                             .NAME = p.NAME,
                             .MODULE_NAME = p.SE_MODULE.NAME_DESC,
                             .ORDER_BY = p.SE_MODULE.ORDER_BY,
                             .FID = p.FID,
                             .FUNCTION_GROUP_NAME = p.SE_FUNCTION_GROUP.NAME})

            query = query.AsQueryable().OrderBy(Sorts)
            Total = query.Count
            query = query.Skip(PageIndex * PageSize).Take(PageSize)
            Return query.ToList

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function InsertUserFunctionException(ByVal _lstUserFunc As List(Of UserFunctionDTO), ByVal lstOrg As List(Of Decimal), ByVal log As UserLog) As Boolean
        Try
            For i As Integer = 0 To _lstUserFunc.Count - 1

                Dim itemAdd = _lstUserFunc(i)

                Dim count = (From f In Context.SE_USER_PERMISSION_ORG Where f.USER_ID = itemAdd.USER_ID _
                                                                                And f.FUNCTION_ID = itemAdd.FUNCTION_ID _
                                                                                And f.ORG_ID = itemAdd.ORG_ID
                             Select f).Count
                If count > 0 Then
                    Continue For
                End If

                Dim functionCheck = (From p In Context.SE_USER_PERMISSION_ORG
                                     Where p.FUNCTION_ID = itemAdd.FUNCTION_ID AndAlso
                                     p.USER_ID = itemAdd.USER_ID).FirstOrDefault

                If functionCheck IsNot Nothing Then Continue For

                Dim _new As New SE_USER_PERMISSION_ORG
                _new.ID = Utilities.GetNextSequence(Context, Context.SE_USER_PERMISSION_ORG.EntitySet.Name)
                _new.ALLOW_CREATE = _lstUserFunc(i).ALLOW_CREATE
                _new.ALLOW_MODIFY = _lstUserFunc(i).ALLOW_MODIFY
                _new.ALLOW_DELETE = _lstUserFunc(i).ALLOW_DELETE
                _new.ALLOW_PRINT = _lstUserFunc(i).ALLOW_PRINT
                _new.ALLOW_IMPORT = _lstUserFunc(i).ALLOW_IMPORT
                _new.ALLOW_EXPORT = _lstUserFunc(i).ALLOW_EXPORT
                _new.ALLOW_SPECIAL1 = _lstUserFunc(i).ALLOW_SPECIAL1
                _new.ALLOW_SPECIAL2 = _lstUserFunc(i).ALLOW_SPECIAL2
                _new.ALLOW_SPECIAL3 = _lstUserFunc(i).ALLOW_SPECIAL3
                _new.ALLOW_SPECIAL4 = _lstUserFunc(i).ALLOW_SPECIAL4
                _new.ALLOW_SPECIAL5 = _lstUserFunc(i).ALLOW_SPECIAL5
                _new.FUNCTION_ID = _lstUserFunc(i).FUNCTION_ID
                _new.USER_ID = _lstUserFunc(i).USER_ID

                Context.SE_USER_PERMISSION_ORG.AddObject(_new)
                If lstOrg IsNot Nothing Then
                    For Each item In lstOrg
                        Dim objOrg As New SE_USER_PERMISSION_ORG_DETAIL
                        objOrg.ID = Utilities.GetNextSequence(Context, Context.SE_USER_PERMISSION_ORG_DETAIL.EntitySet.Name)
                        objOrg.ORG_ID = item
                        objOrg.FUNCTION_ID = _lstUserFunc(i).FUNCTION_ID
                        objOrg.USER_ID = _lstUserFunc(i).USER_ID
                        Context.SE_USER_PERMISSION_ORG_DETAIL.AddObject(objOrg)
                    Next
                End If
                If _lstUserFunc(i).LST_WL IsNot Nothing Then
                    For Each item In _lstUserFunc(i).LST_WL
                        Dim objWl As New SE_USER_PERMISSION_ORG_WL
                        objWl.ID = Utilities.GetNextSequence(Context, Context.SE_USER_PERMISSION_ORG_WL.EntitySet.Name)
                        objWl.WL_ID = item
                        objWl.FUNCTION_ID = _lstUserFunc(i).FUNCTION_ID
                        objWl.USER_ID = _lstUserFunc(i).USER_ID
                        Context.SE_USER_PERMISSION_ORG_WL.AddObject(objWl)
                    Next
                End If
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function GetUserFunctionNotPermision(ByVal _UserID As Decimal,
                                                ByVal _filter As FunctionDTO,
                                                ByVal PageIndex As Integer,
                                                ByVal PageSize As Integer,
                                                ByRef Total As Integer,
                                                ByVal log As UserLog,
                                                Optional ByVal Sorts As String = "NAME asc") As List(Of FunctionDTO)
        Try
            Dim lstTemp As New List(Of SE_FUNCTION)
            If log.Username = "ADMIN" Then
                lstTemp = (From p In Context.SE_FUNCTION
                           From m In Context.SE_MODULE.Where(Function(f) f.ID = p.MODULE_ID).DefaultIfEmpty
                           Where p.ACTFLG = "A" And m.IS_ACTIVE = 1 Select p).ToList
            Else
                lstTemp = (From p In Context.SE_FUNCTION
                           From m In Context.SE_MODULE.Where(Function(f) f.ID = p.MODULE_ID).DefaultIfEmpty
                           From q In Context.SE_USER_PERMISSION.Where(Function(f) f.FUNCTION_ID = p.ID)
                           From u In Context.SE_USER.Where(Function(f) f.ID = q.USER_ID And f.USERNAME = log.Username)
                           Where p.ACTFLG = "A" And m.IS_ACTIVE = 1
                           Select p).ToList
            End If

            If _filter.MODULE_NAME <> "" Then
                lstTemp = lstTemp.Where(Function(p) p.SE_MODULE.NAME_DESC.ToUpper.Contains(_filter.MODULE_NAME.ToUpper)).ToList
            End If
            If _filter.FUNCTION_GROUP_NAME <> "" Then
                lstTemp = lstTemp.Where(Function(p) p.SE_FUNCTION_GROUP.NAME.ToUpper.Contains(_filter.FUNCTION_GROUP_NAME.ToUpper)).ToList
            End If
            If _filter.NAME <> "" Then
                lstTemp = lstTemp.Where(Function(p) p.NAME.ToUpper.Contains(_filter.NAME.ToUpper)).ToList
            End If

            If _filter.FUNCTION_NAME_FID <> "" Then
                lstTemp = lstTemp.Where(Function(p) p.NAME.ToUpper.Contains(_filter.FUNCTION_NAME_FID.ToUpper)).ToList()
            End If
            If _filter.MODULE_NAME_FID <> "" Then
                lstTemp = lstTemp.Where(Function(p) p.SE_MODULE.NAME_DESC.ToUpper.Contains(_filter.MODULE_NAME_FID.ToUpper)).ToList()
            End If
            If _filter.FUNCTION_GROUP_NAME_FID <> "" Then
                lstTemp = lstTemp.Where(Function(p) p.SE_FUNCTION_GROUP.NAME.ToUpper.Contains(_filter.FUNCTION_GROUP_NAME_FID.ToUpper)).ToList()
            End If

            Dim lst1 = (From p In lstTemp Select p.ID)
            Dim lst2 = (From p In Context.SE_USER_PERMISSION Where p.USER_ID = _UserID Select p.FUNCTION_ID)
            Dim lstID As List(Of Decimal) = lst1.Except(lst2).ToList

            Dim query = (From p In Context.SE_FUNCTION Where lstID.Contains(p.ID)
                         Order By p.NAME
                         Select New FunctionDTO With {
                             .ID = p.ID,
                             .NAME = p.NAME,
                             .MODULE_NAME = p.SE_MODULE.NAME_DESC,
                             .ORDER_BY = p.SE_MODULE.ORDER_BY,
                             .FID = p.FID,
                             .FUNCTION_GROUP_NAME = p.SE_FUNCTION_GROUP.NAME})

            query = query.AsQueryable().OrderBy(Sorts)
            Total = query.Count
            query = query.Skip(PageIndex * PageSize).Take(PageSize)
            Return query.ToList

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function InsertUserFunction(ByVal _lstUserFunc As List(Of UserFunctionDTO), ByVal log As UserLog) As Boolean
        Try
            For i As Integer = 0 To _lstUserFunc.Count - 1

                Dim itemAdd = _lstUserFunc(i)

                Dim functionCheck = (From p In Context.SE_USER_PERMISSION
                                     Where p.FUNCTION_ID = itemAdd.FUNCTION_ID AndAlso
                                     p.USER_ID = itemAdd.USER_ID).FirstOrDefault

                If functionCheck IsNot Nothing Then Continue For

                Dim _new As New SE_USER_PERMISSION
                _new.ID = Utilities.GetNextSequence(Context, Context.SE_USER_PERMISSION.EntitySet.Name)
                _new.ALLOW_CREATE = _lstUserFunc(i).ALLOW_CREATE
                _new.ALLOW_MODIFY = _lstUserFunc(i).ALLOW_MODIFY
                _new.ALLOW_DELETE = _lstUserFunc(i).ALLOW_DELETE
                _new.ALLOW_PRINT = _lstUserFunc(i).ALLOW_PRINT
                _new.ALLOW_IMPORT = _lstUserFunc(i).ALLOW_IMPORT
                _new.ALLOW_EXPORT = _lstUserFunc(i).ALLOW_EXPORT
                _new.ALLOW_SPECIAL1 = _lstUserFunc(i).ALLOW_SPECIAL1
                _new.ALLOW_SPECIAL2 = _lstUserFunc(i).ALLOW_SPECIAL2
                _new.ALLOW_SPECIAL3 = _lstUserFunc(i).ALLOW_SPECIAL3
                _new.ALLOW_SPECIAL4 = _lstUserFunc(i).ALLOW_SPECIAL4
                _new.ALLOW_SPECIAL5 = _lstUserFunc(i).ALLOW_SPECIAL5
                _new.FUNCTION_ID = _lstUserFunc(i).FUNCTION_ID
                _new.USER_ID = _lstUserFunc(i).USER_ID
                Context.SE_USER_PERMISSION.AddObject(_new)
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function UpdateUserFunction(ByVal _lstUserFunc As List(Of UserFunctionDTO), ByVal log As UserLog) As Boolean
        Try
            Dim i As Integer
            Dim lstID As List(Of Decimal) = (From p In _lstUserFunc Select p.ID).ToList
            Dim objUpdate As List(Of SE_USER_PERMISSION) = (From p In Context.SE_USER_PERMISSION Where lstID.Contains(p.ID) Select p).ToList
            For i = 0 To objUpdate.Count - 1
                Dim func As UserFunctionDTO = _lstUserFunc.Find(Function(item As UserFunctionDTO) item.ID = objUpdate(i).ID)
                If func IsNot Nothing Then
                    objUpdate(i).ALLOW_CREATE = If(func.ALLOW_CREATE, 1, 0)
                    objUpdate(i).ALLOW_DELETE = If(func.ALLOW_DELETE, 1, 0)
                    objUpdate(i).ALLOW_EXPORT = If(func.ALLOW_EXPORT, 1, 0)
                    objUpdate(i).ALLOW_IMPORT = If(func.ALLOW_IMPORT, 1, 0)
                    objUpdate(i).ALLOW_MODIFY = If(func.ALLOW_MODIFY, 1, 0)
                    objUpdate(i).ALLOW_PRINT = If(func.ALLOW_PRINT, 1, 0)
                    objUpdate(i).ALLOW_SPECIAL1 = If(func.ALLOW_SPECIAL1, 1, 0)
                    objUpdate(i).ALLOW_SPECIAL2 = If(func.ALLOW_SPECIAL2, 1, 0)
                    objUpdate(i).ALLOW_SPECIAL3 = If(func.ALLOW_SPECIAL3, 1, 0)
                    objUpdate(i).ALLOW_SPECIAL4 = If(func.ALLOW_SPECIAL4, 1, 0)
                    objUpdate(i).ALLOW_SPECIAL5 = If(func.ALLOW_SPECIAL5, 1, 0)
                End If
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function DeleteUserFunction(ByVal _lstID As List(Of Decimal)) As Boolean
        Try
            Using conMng As New ConnectionManager
                Using conn As New OracleConnection(conMng.GetConnectionString())
                    conn.Open()
                    Using cmd As New OracleCommand()
                        Try
                            cmd.Connection = conn
                            cmd.Transaction = cmd.Connection.BeginTransaction()
                            For i As Integer = 0 To _lstID.Count - 1
                                cmd.CommandText = "DELETE SE_USER_PERMISSION WHERE ID =" & _lstID(i)
                                cmd.ExecuteNonQuery()
                            Next
                            cmd.Transaction.Commit()
                        Catch ex As Exception
                            cmd.Transaction.Rollback()
                        Finally
                            'Dispose all resource
                            cmd.Dispose()
                            conn.Close()
                            conn.Dispose()
                        End Try
                    End Using
                End Using
            End Using

            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function CopyUserFunction(ByVal UserCopyID As Decimal, ByVal UserID As Decimal, ByVal log As UserLog) As Boolean
        ' bổ sung delete phục vụ cho chức năng copy function
        Dim lstDel = (From p In Context.SE_USER_PERMISSION Where p.USER_ID = UserID).ToList
        For Each itm In lstDel
            Context.SE_USER_PERMISSION.DeleteObject(itm)
        Next
        Context.SaveChanges(log)

        Dim _lstUserFunc = (From p In Context.SE_USER_PERMISSION Where p.USER_ID = UserCopyID
                            Select New UserFunctionDTO With {.USER_ID = UserID,
                                                               .FUNCTION_ID = p.FUNCTION_ID,
                                                              .ALLOW_CREATE = p.ALLOW_CREATE,
                                                               .ALLOW_DELETE = p.ALLOW_DELETE,
                                                               .ALLOW_EXPORT = p.ALLOW_EXPORT,
                                                               .ALLOW_IMPORT = p.ALLOW_IMPORT,
                                                               .ALLOW_MODIFY = p.ALLOW_MODIFY,
                                                               .ALLOW_PRINT = p.ALLOW_PRINT,
                                                               .ALLOW_SPECIAL1 = p.ALLOW_SPECIAL1,
                                                               .ALLOW_SPECIAL2 = p.ALLOW_SPECIAL2,
                                                               .ALLOW_SPECIAL3 = p.ALLOW_SPECIAL3,
                                                               .ALLOW_SPECIAL4 = p.ALLOW_SPECIAL4,
                                                               .ALLOW_SPECIAL5 = p.ALLOW_SPECIAL5}).ToList
        For i As Integer = 0 To _lstUserFunc.Count - 1

            Dim itemAdd = _lstUserFunc(i)

            Dim functionCheck = Context.SE_USER_PERMISSION.FirstOrDefault(Function(p) p.FUNCTION_ID = itemAdd.FUNCTION_ID AndAlso p.USER_ID = itemAdd.USER_ID)

            If functionCheck IsNot Nothing Then Continue For

            Dim _new As New SE_USER_PERMISSION
            _new.ID = Utilities.GetNextSequence(Context, Context.SE_USER_PERMISSION.EntitySet.Name)
            _new.ALLOW_CREATE = _lstUserFunc(i).ALLOW_CREATE
            _new.ALLOW_MODIFY = _lstUserFunc(i).ALLOW_MODIFY
            _new.ALLOW_DELETE = _lstUserFunc(i).ALLOW_DELETE
            _new.ALLOW_PRINT = _lstUserFunc(i).ALLOW_PRINT
            _new.ALLOW_IMPORT = _lstUserFunc(i).ALLOW_IMPORT
            _new.ALLOW_EXPORT = _lstUserFunc(i).ALLOW_EXPORT
            _new.ALLOW_SPECIAL1 = _lstUserFunc(i).ALLOW_SPECIAL1
            _new.ALLOW_SPECIAL2 = _lstUserFunc(i).ALLOW_SPECIAL2
            _new.ALLOW_SPECIAL3 = _lstUserFunc(i).ALLOW_SPECIAL3
            _new.ALLOW_SPECIAL4 = _lstUserFunc(i).ALLOW_SPECIAL4
            _new.ALLOW_SPECIAL5 = _lstUserFunc(i).ALLOW_SPECIAL5
            _new.FUNCTION_ID = _lstUserFunc(i).FUNCTION_ID
            _new.USER_ID = _lstUserFunc(i).USER_ID
            Context.SE_USER_PERMISSION.AddObject(_new)
            Context.SaveChanges(log)
        Next
        Return True
    End Function

#End Region

#Region "User Organization"

    Public Function GetUserOrganization(ByVal _UserID As Decimal) As List(Of Decimal)
        Return (From p In Context.SE_USER_ORG_ACCESS
                From o In Context.HU_ORGANIZATION.Where(Function(f) p.ORG_ID = f.ID)
                Where p.USER_ID = _UserID
                Select p.ORG_ID).ToList
    End Function

    Public Function DeleteUserOrganization(ByVal _UserId As Decimal)
        Try
            Using cls As New DataAccess.NonQueryData
                cls.ExecuteSQL("DELETE SE_USER_ORG_ACCESS WHERE USER_ID =" & _UserId)
            End Using
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function UpdateUserOrganization(ByVal _lstOrg As List(Of UserOrgAccessDTO)) As Boolean
        If _lstOrg.Count > 0 Then
            Dim i As Integer
            For i = 0 To _lstOrg.Count - 1
                Dim _item As New SE_USER_ORG_ACCESS
                _item.ID = Utilities.GetNextSequence(Context, Context.SE_USER_ORG_ACCESS.EntitySet.Name)
                _item.USER_ID = _lstOrg(i).USER_ID
                _item.ORG_ID = _lstOrg(i).ORG_ID
                Context.SE_USER_ORG_ACCESS.AddObject(_item)
            Next
            Context.SaveChanges()
            Return True
        Else
            Return False
        End If
    End Function

#End Region

#Region "User Report"

    Public Function GetUserReportList(ByVal _UserID As Decimal) As List(Of UserReportDTO)
        Dim query = (From p In Context.SE_REPORT
                     Order By p.NAME
                     Select New UserReportDTO With {
                         .ID = p.ID,
                         .REPORT_NAME = p.NAME,
                         .MODULE_NAME = p.SE_MODULE.NAME,
                         .IS_USE = ((From n In p.SE_USER Where n.ID = _UserID).Count > 0)})
        Return query.ToList
    End Function

    Public Function GetUserReportListFilter(ByVal _UserID As Decimal, ByVal _filter As UserReportDTO) As List(Of UserReportDTO)
        Dim query = (From p In Context.SE_REPORT
                     Order By p.NAME
                     Where (_filter.REPORT_NAME = Nothing Or p.NAME.ToUpper = _filter.REPORT_NAME.ToUpper) And
                     (_filter.MODULE_ID = Nothing Or p.SE_MODULE.ID = _filter.MODULE_ID)
                     Select New UserReportDTO With {
                         .ID = p.ID,
                         .REPORT_NAME = p.NAME,
                         .MODULE_NAME = p.SE_MODULE.NAME,
                         .IS_USE = ((From n In p.SE_USER Where n.ID = _UserID).Count > 0)})
        Return query.ToList
    End Function

    Public Function UpdateUserReport(ByVal _UserID As Decimal, ByVal _lstReport As List(Of UserReportDTO)) As Boolean
        Dim query As SE_USER
        query = (From p In Context.SE_USER
                 Where p.ID = _UserID
                 Select p).FirstOrDefault

        Dim Ids As List(Of Decimal) = (From p In _lstReport Select p.ID).ToList
        Dim lst = (From p In Context.SE_REPORT Where Ids.Contains(p.ID)).ToList
        If query IsNot Nothing Then
            query.SE_REPORT.Clear()
            For i As Integer = 0 To lst.Count - 1
                query.SE_REPORT.Add(lst(i))
            Next
            Context.SaveChanges()
        Else
            Return False
        End If
        Return True
    End Function

#End Region

#Region "AccessLog"

    Public Function GetAccessLog(ByVal filter As AccessLogFilter,
                                 ByVal PageIndex As Integer,
                                 ByVal PageSize As Integer,
                                 ByRef Total As Integer,
                                 Optional ByVal Sorts As String = "LoginDate desc") As List(Of AccessLog)
        Try
            Return AuditLogHelper.GetAccessLog(filter, PageIndex, PageSize, Total, Sorts)
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function InsertAccessLog(ByVal _accesslog As AccessLog) As Boolean
        Try
            Return AuditLogHelper.InsertAccessLog(_accesslog)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "ActionLog"

    Public Function GetActionLog(ByVal filter As ActionLogFilter,
                                 ByVal PageIndex As Integer,
                                 ByVal PageSize As Integer,
                                 ByRef Total As Integer,
                                 Optional ByVal Sorts As String = "ActionDate desc") As List(Of ActionLog)
        Try
            Return (AuditLogHelper.GetActionLog(filter, PageIndex, PageSize, Total, Sorts))
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function GetActionLog(ByVal objectId As Decimal) As List(Of ActionLog)
        Try
            Return AuditLogHelper.GetActionLog(objectId)
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function GetActionLogByID(ByVal gID As Decimal) As List(Of AuditLogDtl)
        Try
            Return AuditLogHelper.GetActionLogByID(gID)
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function DeleteActionLogs(ByVal lstDeleteIds As List(Of Decimal)) As Integer
        Try
            Return AuditLogHelper.DeleteActionLogs(lstDeleteIds)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Dim mydbcontext As New Entity.DbContext(Context, True)

#End Region

#Region "Check User Login"

    Public Function IsUsernameExist(ByVal Username As String) As Boolean
        Dim u As SE_USER = (From p In Context.SE_USER Where p.USERNAME.ToUpper.Equals(Username.ToUpper)).FirstOrDefault
        If u IsNot Nothing Then
            Return True
        End If
        Return False
    End Function

    Public Function GetUserPermissions(ByVal Username As String) As List(Of PermissionDTO)
        Try
            If Username = "" Then Return New List(Of PermissionDTO)
            Dim query = From p In Context.SE_USER.Include("SE_GROUPS") Where p.USERNAME.ToUpper = Username.ToUpper
            'Logger.LogInfo(query)
            Dim u As SE_USER = query.FirstOrDefault
            If u Is Nothing Then Return New List(Of PermissionDTO)
            Dim query1 = From p In u.SE_GROUPS Where p.ACTFLG = "A" And p.EFFECT_DATE <= Date.Now And (p.EXPIRE_DATE Is Nothing OrElse p.EXPIRE_DATE >= Date.Now) Select p.ID
            'Logger.LogInfo(query1)
            Dim lstGroupIds As List(Of Decimal) = query1.ToList

            Dim query2 = From p In Context.SE_USER_PERMISSION
                         From func In Context.SE_FUNCTION.Where(Function(f) f.ID = p.FUNCTION_ID)
                         From user In Context.SE_USER.Where(Function(f) f.ID = p.USER_ID)
                         Where user.USERNAME.ToUpper = Username.ToUpper
                         Select New PermissionDTO With {.ID = p.ID,
                                                        .FunctionID = p.FUNCTION_ID,
                                                        .GroupID = 0,
                                                        .FID = func.FID,
                                                        .MID = func.SE_MODULE.MID,
                                                        .AllowCreate = p.ALLOW_CREATE,
                                                        .AllowModify = p.ALLOW_MODIFY,
                                                        .AllowDelete = p.ALLOW_DELETE,
                                                        .AllowImport = p.ALLOW_IMPORT,
                                                        .AllowExport = p.ALLOW_EXPORT,
                                                        .AllowPrint = p.ALLOW_PRINT,
                                                        .AllowSpecial1 = p.ALLOW_SPECIAL1,
                                                        .AllowSpecial2 = p.ALLOW_SPECIAL2,
                                                        .AllowSpecial3 = p.ALLOW_SPECIAL3,
                                                        .AllowSpecial4 = p.ALLOW_SPECIAL4,
                                                        .AllowSpecial5 = p.ALLOW_SPECIAL5,
                                                        .IS_REPORT = False}

            Dim query22 = From p In Context.SE_USER_PERMISSION_ORG
                          From func In Context.SE_FUNCTION.Where(Function(f) f.ID = p.FUNCTION_ID)
                          From user In Context.SE_USER.Where(Function(f) f.ID = p.USER_ID)
                          Where user.USERNAME.ToUpper = Username.ToUpper
                          Select New PermissionDTO With {.ID = p.ID,
                                                         .FunctionID = p.FUNCTION_ID,
                                                         .GroupID = 0,
                                                         .FID = func.FID,
                                                         .MID = func.SE_MODULE.MID,
                                                         .AllowCreate = p.ALLOW_CREATE,
                                                         .AllowModify = p.ALLOW_MODIFY,
                                                         .AllowDelete = p.ALLOW_DELETE,
                                                         .AllowImport = p.ALLOW_IMPORT,
                                                         .AllowExport = p.ALLOW_EXPORT,
                                                         .AllowPrint = p.ALLOW_PRINT,
                                                         .AllowSpecial1 = p.ALLOW_SPECIAL1,
                                                         .AllowSpecial2 = p.ALLOW_SPECIAL2,
                                                         .AllowSpecial3 = p.ALLOW_SPECIAL3,
                                                         .AllowSpecial4 = p.ALLOW_SPECIAL4,
                                                         .AllowSpecial5 = p.ALLOW_SPECIAL5,
                                                         .IS_REPORT = False}

            Dim query3 = From p In Context.SE_REPORT
                         From n In p.SE_USER
                         Where n.USERNAME.ToUpper = Username.ToUpper
                         Select New PermissionDTO With {.ID = p.ID,
                                                        .FunctionID = p.ID,
                                                        .GroupID = 0,
                                                        .FID = p.CODE,
                                                        .MID = p.SE_MODULE.MID,
                                                        .AllowCreate = False,
                                                        .AllowModify = False,
                                                        .AllowDelete = False,
                                                        .AllowImport = False,
                                                        .AllowExport = False,
                                                        .AllowPrint = False,
                                                        .AllowSpecial1 = False,
                                                        .AllowSpecial2 = False,
                                                        .AllowSpecial3 = False,
                                                        .AllowSpecial4 = False,
                                                        .AllowSpecial5 = False,
                                                        .IS_REPORT = True}


            'Dim lstPermissions As List(Of PermissionDTO) = query2.Union(query3).ToList
            Dim lstPermissions As List(Of PermissionDTO) = query2.Union(query22).Union(query3).ToList

            Return lstPermissions
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function CheckUserAdmin(ByVal Username As String) As Boolean
        Try
            If Username = "" Then Return False
            Dim u = (From p In Context.SE_USER
                     Where p.USERNAME.ToUpper = Username.ToUpper).FirstOrDefault

            If u Is Nothing Then Return False

            If u.MODULE_ADMIN = "" Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetUserWithPermision(ByVal Username As String) As UserDTO
        Try
            Dim session_out = (From p In Context.SE_CONFIG Where p.CODE = "SESSION_TIMEOUT" Select p.VALUE).FirstOrDefault
            Dim session_warning = (From p In Context.SE_CONFIG Where p.CODE = "SESSION_TIMEWARNING" Select p.VALUE).FirstOrDefault

            Dim s_out = CDec(Val(session_out))
            Dim s_warning = CDec(Val(session_warning))

            If Username = "" Then Return New UserDTO
            Dim query = (From p In Context.SE_USER
                         From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID).DefaultIfEmpty
                         Where p.USERNAME.ToUpper = Username.ToUpper
                         Select New UserDTO With {
                             .ID = p.ID,
                             .USERNAME = p.USERNAME,
                             .PASSWORD = p.PASSWORD,
                             .FULLNAME = p.FULLNAME,
                             .EMAIL = p.EMAIL,
                             .TELEPHONE = p.TELEPHONE,
                             .IS_APP = p.IS_APP,
                             .IS_HR = p.IS_HR,
                             .IS_PORTAL = p.IS_PORTAL,
                             .EMPLOYEE_CODE = p.EMPLOYEE_CODE,
                             .EMPLOYEE_ID = p.EMPLOYEE_ID,
                             .EMPLOYEE_FULLNAME_VN = e.FULLNAME_VN,
                             .IS_AD = p.IS_AD,
                             .EFFECT_DATE = p.EFFECT_DATE,
                             .EXPIRE_DATE = p.EXPIRE_DATE,
                             .ACTFLG = p.ACTFLG,
                             .IS_CHANGE_PASS = p.IS_CHANGE_PASS,
                             .CREATED_DATE = p.CREATED_DATE,
                             .MODULE_ADMIN = p.MODULE_ADMIN,
                             .WORK_STATUS = e.WORK_STATUS,
                             .ORG_ID = e.ORG_ID,
                             .OBJECT_EMPLOYEE_ID = e.OBJECT_EMPLOYEE_ID,
                             .TITLE_ID = e.TITLE_ID,
                             .SESSION_OUT = s_out,
                             .SESSION_WARNING = s_warning,
                             .IS_LOGIN = p.IS_LOGIN}).FirstOrDefault
            Dim objUser = query
            If objUser IsNot Nothing Then
                Dim isUserPermission As Boolean = True
                If objUser.MODULE_ADMIN Is Nothing Then
                    If (From p In Context.SE_USER_ORG_ACCESS
                        Where p.USER_ID = objUser.ID).Count = 0 Then
                        isUserPermission = False
                    End If

                    If (From p In Context.SE_USER_PERMISSION
                        Where p.USER_ID = objUser.ID).Count = 0 Then
                        isUserPermission = False
                    Else
                        isUserPermission = True
                    End If
                End If

                Return New UserDTO With {.ID = objUser.ID,
                                         .EMAIL = objUser.EMAIL,
                                         .TELEPHONE = objUser.TELEPHONE,
                                         .USERNAME = objUser.USERNAME,
                                         .FULLNAME = objUser.FULLNAME,
                                         .PASSWORD = objUser.PASSWORD,
                                         .IS_APP = objUser.IS_APP,
                                         .IS_HR = objUser.IS_HR,
                                         .IS_PORTAL = objUser.IS_PORTAL,
                                         .IS_AD = objUser.IS_AD,
                                         .EMPLOYEE_CODE = objUser.EMPLOYEE_CODE,
                                         .ACTFLG = objUser.ACTFLG,
                                         .IS_CHANGE_PASS = objUser.IS_CHANGE_PASS,
                                         .MODULE_ADMIN = objUser.MODULE_ADMIN,
                                         .EMPLOYEE_ID = objUser.EMPLOYEE_ID,
                                         .EMPLOYEE_FULLNAME_VN = objUser.EMPLOYEE_FULLNAME_VN,
                                         .EFFECT_DATE = objUser.EFFECT_DATE,
                                         .EXPIRE_DATE = objUser.EXPIRE_DATE,
                                         .IS_USER_PERMISSION = isUserPermission,
                                         .WORK_STATUS = objUser.WORK_STATUS,
                                         .ORG_ID = objUser.ORG_ID,
                                         .TITLE_ID = objUser.TITLE_ID,
                                         .SESSION_OUT = objUser.SESSION_OUT,
                                         .SESSION_WARNING = objUser.SESSION_WARNING,
                                        .OBJECT_EMPLOYEE_ID = objUser.OBJECT_EMPLOYEE_ID,
                                        .IS_LOGIN = objUser.IS_LOGIN}
            End If
            Return Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetUserByUserName(ByVal UserName As String) As SE_USER
        Try
            Dim query = (From p In Context.SE_USER
                         Where p.USERNAME.ToUpper = UserName.ToUpper).FirstOrDefault
            Return query
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetUserByUserID(ByVal UserID As Decimal) As SE_USER
        Try
            Dim query = (From p In Context.SE_USER
                         Where p.ID = UserID).FirstOrDefault
            Return query
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ChangeUserPassword(ByVal Username As String, ByVal _oldpass As String, ByVal _newpass As String, ByVal log As UserLog) As Boolean
        Try
            Dim query As SE_USER
            query = (From p In Context.SE_USER
                     Where p.USERNAME.ToUpper = Username.ToUpper
                     Select p).FirstOrDefault
            If query IsNot Nothing Then
                Using EncryptData As New EncryptData
                    If EncryptData.DecryptString(query.PASSWORD) = _oldpass Then
                        query.PASSWORD = EncryptData.EncryptString(_newpass)
                        query.IS_CHANGE_PASS = -1
                        query.CHANGE_PASS_DATE = Now.Date
                        query.IS_LOGIN = False
                        Context.SaveChanges(log)
                    Else
                        Return False
                    End If

                End Using
            Else
                Return False
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetPassword(ByVal Username As String) As String
        Try
            Dim query As SE_USER
            query = (From p In Context.SE_USER
                     Where p.USERNAME.ToUpper = Username.ToUpper
                     Select p).FirstOrDefault
            Return query.PASSWORD
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function UpdateUserStatus(ByVal Username As String, ByVal _ACTFLG As String, ByVal log As UserLog) As Boolean
        Try
            Dim query As SE_USER
            query = (From p In Context.SE_USER
                     Where p.USERNAME.ToUpper = Username.ToUpper
                     Select p).FirstOrDefault

            If query IsNot Nothing Then
                query.ACTFLG = _ACTFLG
                Context.SaveChanges(log)
            Else
                Return False
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Organization"

    Public Function GetOrganizationList() As List(Of OrganizationDTO)
        Dim lstOrgs As New List(Of OrganizationDTO)
        lstOrgs = (From o In Context.HU_ORGANIZATION
                   Order By o.NAME_VN
                   Select New OrganizationDTO With {
                      .ID = o.ID,
                      .DESCRIPTION_PATH = o.DESCRIPTION_PATH}).ToList
        Return lstOrgs
    End Function

    Public Function GetOrganizationChildByList(ByVal _username As String,
                                            ByVal _status As String,
                                            ByVal _lstOrgID As List(Of String)) As List(Of Decimal)

        Dim lstOrgs As List(Of Decimal)

        Try
            Dim u As SE_USER = (From p In Context.SE_USER Where p.USERNAME.ToUpper = _username.ToUpper).FirstOrDefault
            If u IsNot Nothing Then
                Dim query1 = (From p In u.SE_GROUPS Where CBool(p.IS_ADMIN) = True Select p.ID).FirstOrDefault
                If query1 = Nothing Then
                    Dim lstGroupIds As List(Of Decimal) = (From p In u.SE_GROUPS Select p.ID).ToList
                    lstOrgs = (From p In Context.SE_GROUP_ORG_ACCESS
                               From o In Context.HUV_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID)
                               Where lstGroupIds.Contains(p.GROUP_ID) And (o.ACTFLG = "A" Or o.ACTFLG = _status) And
                               _lstOrgID.Contains(o.ORG_ID1) Or _lstOrgID.Contains(o.ORG_ID2) Or
                               _lstOrgID.Contains(o.ORG_ID3) Or _lstOrgID.Contains(o.ORG_ID4) Or
                               _lstOrgID.Contains(o.ORG_ID5) Or _lstOrgID.Contains(o.ORG_ID6) Or
                               _lstOrgID.Contains(o.ORG_ID7) Or _lstOrgID.Contains(o.ORG_ID8)
                               Select p.ORG_ID).Distinct.ToList
                Else
                    lstOrgs = (From o In Context.HUV_ORGANIZATION
                               Where (o.ACTFLG = "A" Or o.ACTFLG = _status) And
                               _lstOrgID.Contains(o.ORG_ID1) Or _lstOrgID.Contains(o.ORG_ID2) Or
                               _lstOrgID.Contains(o.ORG_ID3) Or _lstOrgID.Contains(o.ORG_ID4) Or
                               _lstOrgID.Contains(o.ORG_ID5) Or _lstOrgID.Contains(o.ORG_ID6) Or
                               _lstOrgID.Contains(o.ORG_ID7) Or _lstOrgID.Contains(o.ORG_ID8)
                               Select o.ID).ToList

                End If
            End If



            Return lstOrgs
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetOrganizationChildTextByList(ByVal _username As String,
                                            ByVal _status As String,
                                            ByVal _lstOrgID As List(Of String)) As List(Of String)

        Dim lstOrgs As List(Of String)

        Try
            Dim u As SE_USER = (From p In Context.SE_USER Where p.USERNAME.ToUpper = _username.ToUpper).FirstOrDefault
            If u IsNot Nothing Then
                Dim query1 = (From p In u.SE_GROUPS Where CBool(p.IS_ADMIN) = True Select p.ID).FirstOrDefault
                If query1 = Nothing Then
                    Dim lstGroupIds As List(Of Decimal) = (From p In u.SE_GROUPS Select p.ID).ToList
                    lstOrgs = (From p In Context.SE_GROUP_ORG_ACCESS
                               From o In Context.HUV_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID)
                               Where lstGroupIds.Contains(p.GROUP_ID) And (o.ACTFLG = "A" Or o.ACTFLG = _status) And
                               _lstOrgID.Contains(o.ORG_ID1) Or _lstOrgID.Contains(o.ORG_ID2) Or
                               _lstOrgID.Contains(o.ORG_ID3) Or _lstOrgID.Contains(o.ORG_ID4) Or
                               _lstOrgID.Contains(o.ORG_ID5) Or _lstOrgID.Contains(o.ORG_ID6) Or
                               _lstOrgID.Contains(o.ORG_ID7) Or _lstOrgID.Contains(o.ORG_ID8)
                               Select o.ORG_NAME).Distinct.ToList
                Else
                    lstOrgs = (From o In Context.HUV_ORGANIZATION
                               Where (o.ACTFLG = "A" Or o.ACTFLG = _status) And
                               _lstOrgID.Contains(o.ORG_ID1) Or _lstOrgID.Contains(o.ORG_ID2) Or
                               _lstOrgID.Contains(o.ORG_ID3) Or _lstOrgID.Contains(o.ORG_ID4) Or
                               _lstOrgID.Contains(o.ORG_ID5) Or _lstOrgID.Contains(o.ORG_ID6) Or
                               _lstOrgID.Contains(o.ORG_ID7) Or _lstOrgID.Contains(o.ORG_ID8)
                               Select o.ORG_NAME).ToList

                End If
            End If



            Return lstOrgs
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetOrganizationPermission(ByVal _username As String,
                                            ByVal _lstOrgID As List(Of Decimal)) As List(Of Decimal)

        Dim lstOrgs As New List(Of Decimal)
        Dim lstOrgPer As List(Of Decimal)
        Try
            Dim u As SE_USER = (From p In Context.SE_USER Where p.USERNAME.ToUpper = _username.ToUpper).FirstOrDefault
            If u IsNot Nothing Then
                Dim query1 = (From p In u.SE_GROUPS Where CBool(p.IS_ADMIN) = True Select p.ID).FirstOrDefault
                If query1 = Nothing Then
                    'Phân quyền User
                    Dim lstGroupIds As List(Of Decimal) = (From p In u.SE_GROUPS Select p.ID).ToList
                    lstOrgPer = (From p In Context.SE_GROUP_ORG_ACCESS
                                 Where lstGroupIds.Contains(p.GROUP_ID) And _lstOrgID.Contains(p.ORG_ID)
                                 Select p.ORG_ID).Distinct.ToList
                    lstOrgs = lstOrgPer
                Else
                    'ADMIN thì giữ nguyên
                    lstOrgs = _lstOrgID
                End If

            End If
            Return lstOrgs
        Catch ex As Exception
            Throw ex
        End Try
    End Function



    Public Function GetOrganizationAll(ByVal isOM) As List(Of OrganizationDTO)
        Dim lstOrgs As New List(Of OrganizationDTO)
        If (isOM) Then
            lstOrgs = (From p In Context.HU_ORGANIZATION_OM
                       Order By p.ORD_NO, p.CODE, p.NAME_VN
                       Select New OrganizationDTO With {
                           .ID = p.ID,
                           .CODE = p.CODE,
                           .NAME_VN = p.NAME_VN,
                           .NAME_EN = p.NAME_EN,
                           .PARENT_ID = p.PARENT_ID,
                           .PARENT_NAME = p.PARENT.NAME_VN,
                           .ORD_NO = p.ORD_NO,
                           .ACTFLG = p.ACTFLG,
                           .DISSOLVE_DATE = p.DISSOLVE_DATE,
                           .DESCRIPTION_PATH = p.DESCRIPTION_PATH,
                           .FOUNDATION_DATE = p.FOUNDATION_DATE}).ToList
        Else
            lstOrgs = (From p In Context.HU_ORGANIZATION
                       Order By p.ORD_NO, p.CODE, p.NAME_VN
                       Select New OrganizationDTO With {
                           .ID = p.ID,
                           .CODE = p.CODE,
                           .NAME_VN = p.NAME_VN,
                           .NAME_EN = p.NAME_EN,
                           .PARENT_ID = p.PARENT_ID,
                           .PARENT_NAME = p.PARENT.NAME_VN,
                           .ORD_NO = p.ORD_NO,
                           .ACTFLG = p.ACTFLG,
                           .DISSOLVE_DATE = p.DISSOLVE_DATE,
                           .DESCRIPTION_PATH = p.DESCRIPTION_PATH,
                           .FOUNDATION_DATE = p.FOUNDATION_DATE,
                           .COST_CENTER_CODE = p.COST_CENTER_CODE,
                           .UY_BAN = p.UY_BAN}).OrderBy("ORD_NO").ToList
        End If


        Return lstOrgs
    End Function
    ' so do to chuc Location da duoc phan quyen
    Public Function GetOrganizationLocationTreeView(ByVal _username As String, Optional ByVal fid As String = "", Optional ByVal state As String = "") As List(Of OrganizationDTO)
        Dim lstOrgs As New List(Of OrganizationDTO)
        Dim u As SE_USER = (From p In Context.SE_USER Where p.USERNAME.ToUpper = _username.ToUpper).FirstOrDefault
        If u IsNot Nothing Then
            Dim query1 = (From p In u.SE_GROUPS Where p.IS_ADMIN <> 0 Select p.ID).FirstOrDefault
            If query1 = Nothing Or query1 = 0 Then
                If state = "Copy" Then
                    lstOrgs = (From p In Context.HU_ORGANIZATION
                               Order By p.ORD_NO, p.CODE, p.NAME_VN
                               Select New OrganizationDTO With {
                                    .ID = p.ID,
                                    .CODE = p.CODE,
                                    .NAME_VN = p.NAME_VN,
                                    .NAME_EN = p.NAME_EN,
                                    .PARENT_ID = p.PARENT_ID,
                                    .PARENT_NAME = p.PARENT.NAME_VN,
                                    .ORD_NO = p.ORD_NO,
                                    .ACTFLG = p.ACTFLG,
                                    .DISSOLVE_DATE = p.DISSOLVE_DATE,
                                    .DESCRIPTION_PATH = p.DESCRIPTION_PATH,
                                    .FOUNDATION_DATE = p.FOUNDATION_DATE,
                                    .SHORT_NAME = p.SHORT_NAME,
                                    .COST_CENTER_CODE = p.COST_CENTER_CODE,
                                    .UY_BAN = p.UY_BAN}).OrderBy("ORD_NO").ToList
                Else

                    Dim lstGroupIds As List(Of Decimal) = (From p In u.SE_GROUPS Select p.ID).ToList

                    Dim lstorg = ""
                    Try
                        Using cls As New DataAccess.QueryData
                            Dim dsData As DataSet = cls.ExecuteStore("PKG_COMMON_LIST.GET_LSTORG_EX",
                                                       New With {.P_USERNAME = _username,
                                                                 .P_FID = fid,
                                                                 .P_CUR = cls.OUT_CURSOR}, False) ' FALSE : no datatable
                            If dsData.Tables.Count <> 0 Then
                                If dsData.Tables(0).Rows.Count > 0 Then
                                    lstorg = "," + dsData.Tables(0).Rows(0)(0).ToString + ","
                                End If
                            End If
                        End Using
                    Catch ex As Exception
                    End Try

                    Dim lstorgRC = ""
                    If fid = "ctrlRC_Request_Portal" Or fid = "ctrlRC_Request_PortalNewEdit" Then
                        Try
                            Using cls As New DataAccess.QueryData
                                Dim dsData As DataSet = cls.ExecuteStore("PKG_COMMON_LIST.GET_LSTORG_PER",
                                                       New With {.P_USERNAME = _username,
                                                                 .P_FID = fid,
                                                                 .P_CUR = cls.OUT_CURSOR}, False) ' FALSE : no datatable
                                If dsData.Tables.Count <> 0 Then
                                    If dsData.Tables(0).Rows.Count > 0 Then
                                        lstorgRC = "," + dsData.Tables(0).Rows(0)(0).ToString + ","
                                    End If
                                End If
                            End Using
                        Catch ex As Exception
                        End Try
                    End If
                    Dim queryPer = (From org In Context.HU_ORGANIZATION
                                    Where lstorgRC.Contains(org.ID)
                                    Select New OrganizationDTO With {
                                    .ID = org.ID,
                                    .CODE = org.CODE,
                                    .NAME_VN = org.NAME_VN,
                                    .NAME_EN = org.NAME_EN,
                                    .PARENT_ID = org.PARENT_ID,
                                    .PARENT_NAME = org.PARENT.NAME_VN,
                                    .ORD_NO = org.ORD_NO,
                                    .ACTFLG = org.ACTFLG,
                                    .DISSOLVE_DATE = org.DISSOLVE_DATE,
                                    .HIERARCHICAL_PATH = org.HIERARCHICAL_PATH,
                                    .DESCRIPTION_PATH = org.DESCRIPTION_PATH,
                                    .FOUNDATION_DATE = org.FOUNDATION_DATE,
                                    .SHORT_NAME = org.SHORT_NAME,
                                    .COST_CENTER_CODE = org.COST_CENTER_CODE,
                                    .UY_BAN = org.UY_BAN}).OrderBy("ORD_NO").Distinct


                    Dim query0 = (From org In Context.HU_ORGANIZATION
                                  Where lstorg.Contains(org.ID)
                                  Select New OrganizationDTO With {
                                    .ID = org.ID,
                                    .CODE = org.CODE,
                                    .NAME_VN = org.NAME_VN,
                                    .NAME_EN = org.NAME_EN,
                                    .PARENT_ID = org.PARENT_ID,
                                    .PARENT_NAME = org.PARENT.NAME_VN,
                                    .ORD_NO = org.ORD_NO,
                                    .ACTFLG = org.ACTFLG,
                                    .DISSOLVE_DATE = org.DISSOLVE_DATE,
                                    .HIERARCHICAL_PATH = org.HIERARCHICAL_PATH,
                                    .DESCRIPTION_PATH = org.DESCRIPTION_PATH,
                                    .FOUNDATION_DATE = org.FOUNDATION_DATE,
                                    .SHORT_NAME = org.SHORT_NAME,
                                     .COST_CENTER_CODE = org.COST_CENTER_CODE,
                                     .UY_BAN = org.UY_BAN}).OrderBy("ORD_NO").Distinct


                    Dim query = (From org In Context.HU_ORGANIZATION
                                 Where (From user In Context.SE_USER_ORG_ACCESS
                                        Where user.USER_ID = u.ID
                                        Select user.ORG_ID).Contains(org.ID)
                                 Select New OrganizationDTO With {
                                    .ID = org.ID,
                                    .CODE = org.CODE,
                                    .NAME_VN = org.NAME_VN,
                                    .NAME_EN = org.NAME_EN,
                                    .PARENT_ID = org.PARENT_ID,
                                    .PARENT_NAME = org.PARENT.NAME_VN,
                                    .ORD_NO = org.ORD_NO,
                                    .ACTFLG = org.ACTFLG,
                                    .DISSOLVE_DATE = org.DISSOLVE_DATE,
                                    .HIERARCHICAL_PATH = org.HIERARCHICAL_PATH,
                                    .DESCRIPTION_PATH = org.DESCRIPTION_PATH,
                                    .FOUNDATION_DATE = org.FOUNDATION_DATE,
                                    .SHORT_NAME = org.SHORT_NAME,
                                     .COST_CENTER_CODE = org.COST_CENTER_CODE,
                                     .UY_BAN = org.UY_BAN}).OrderBy("ORD_NO").Distinct
                    Dim functionId = 0
                    If fid <> "" Then
                        Dim obj = (From p In Context.SE_FUNCTION Where p.FID = fid).FirstOrDefault
                        If obj IsNot Nothing Then
                            functionId = obj.ID
                        End If
                    End If
                    Dim queryEx = (From org In Context.HU_ORGANIZATION
                                   Where (From user In Context.SE_USER_PERMISSION_ORG_DETAIL
                                          Where user.USER_ID = u.ID And user.FUNCTION_ID = functionId
                                          Select user.ORG_ID).Contains(org.ID)
                                   Select New OrganizationDTO With {
                                    .ID = org.ID,
                                    .CODE = org.CODE,
                                    .NAME_VN = org.NAME_VN,
                                    .NAME_EN = org.NAME_EN,
                                    .PARENT_ID = org.PARENT_ID,
                                    .PARENT_NAME = org.PARENT.NAME_VN,
                                    .ORD_NO = org.ORD_NO,
                                    .ACTFLG = org.ACTFLG,
                                    .DISSOLVE_DATE = org.DISSOLVE_DATE,
                                    .HIERARCHICAL_PATH = org.HIERARCHICAL_PATH,
                                    .DESCRIPTION_PATH = org.DESCRIPTION_PATH,
                                    .FOUNDATION_DATE = org.FOUNDATION_DATE,
                                    .SHORT_NAME = org.SHORT_NAME,
                                     .COST_CENTER_CODE = org.COST_CENTER_CODE,
                                     .UY_BAN = org.UY_BAN}).OrderBy("ORD_NO").Distinct


                    lstOrgs = query.Union(query0).Union(queryPer).Union(queryEx).ToList

                    'lstOrgs = query.ToList

                    If lstOrgs.Count > 0 AndAlso (From p In lstOrgs Where p.HIERARCHICAL_PATH = "1").Count = 0 Then
                        Dim lstOrgPer = lstOrgs.Select(Function(f) f.ID).ToList
                        Dim lstOrgID As New List(Of Decimal)
                        For Each org In lstOrgs
                            If org.HIERARCHICAL_PATH <> "" Then
                                If org.HIERARCHICAL_PATH.Split(";").Length > 1 Then
                                    For i As Integer = 0 To org.HIERARCHICAL_PATH.Split(";").Length - 2
                                        Dim str = org.HIERARCHICAL_PATH.Split(";")(i)
                                        If str <> "" Then
                                            Dim orgid = Decimal.Parse(str)
                                            If Not lstOrgPer.Contains(orgid) And Not lstOrgID.Contains(orgid) Then
                                                lstOrgID.Add(orgid)
                                            End If
                                        End If
                                    Next
                                End If
                            End If
                        Next

                        If lstOrgID.Count > 0 Then
                            Dim lstOrgNotPer = (From p In Context.HU_ORGANIZATION
                                                Where lstOrgID.Contains(p.ID)
                                                Select New OrganizationDTO With {
                                                    .ID = p.ID,
                                                    .CODE = p.CODE,
                                                    .NAME_VN = p.NAME_VN,
                                                    .NAME_EN = p.NAME_EN,
                                                    .PARENT_ID = p.PARENT_ID,
                                                    .PARENT_NAME = p.PARENT.NAME_VN,
                                                    .ORD_NO = p.ORD_NO,
                                                    .ACTFLG = p.ACTFLG,
                                                    .DISSOLVE_DATE = p.DISSOLVE_DATE,
                                                    .FOUNDATION_DATE = p.FOUNDATION_DATE,
                                                    .SHORT_NAME = p.SHORT_NAME,
                                                    .DESCRIPTION_PATH = p.DESCRIPTION_PATH,
                                                    .IS_NOT_PER = True,
                                                    .COST_CENTER_CODE = p.COST_CENTER_CODE,
                                                    .UY_BAN = p.UY_BAN}).OrderBy("ORD_NO").ToList

                            lstOrgs.AddRange(lstOrgNotPer)
                        End If

                    End If

                    lstOrgs = (From p In lstOrgs Order By p.ORD_NO, p.CODE, p.NAME_VN).ToList
                End If
            Else
                lstOrgs = (From p In Context.HU_ORGANIZATION
                           Order By p.ORD_NO, p.CODE, p.NAME_VN
                           Select New OrganizationDTO With {
                                .ID = p.ID,
                                .CODE = p.CODE,
                                .NAME_VN = p.NAME_VN,
                                .NAME_EN = p.NAME_EN,
                                .PARENT_ID = p.PARENT_ID,
                                .PARENT_NAME = p.PARENT.NAME_VN,
                                .ORD_NO = p.ORD_NO,
                                .ACTFLG = p.ACTFLG,
                                .DISSOLVE_DATE = p.DISSOLVE_DATE,
                                .DESCRIPTION_PATH = p.DESCRIPTION_PATH,
                                .FOUNDATION_DATE = p.FOUNDATION_DATE,
                                .SHORT_NAME = p.SHORT_NAME,
                               .COST_CENTER_CODE = p.COST_CENTER_CODE,
                               .UY_BAN = p.UY_BAN}).OrderBy("ORD_NO").ToList
            End If
        End If
        Return lstOrgs
    End Function
    ' list OM
    Public Function GetOrganizationLocationTreeView_New(ByVal _username As String) As List(Of OrganizationDTO)
        '-> neu ranh viet theo y tuong o tren khong nen dung for O(n^2) rat fuck tap. :((
        Dim lstOrgs As New List(Of OrganizationDTO)
        Dim u As SE_USER = (From p In Context.SE_USER Where p.USERNAME.ToUpper = _username.ToUpper).FirstOrDefault
        If u IsNot Nothing Then
            Dim query1 = (From p In u.SE_GROUPS Where CBool(p.IS_ADMIN) = True Select p.ID).FirstOrDefault
            If query1 = Nothing Then
                'Dim lstGroupIds As List(Of Decimal) = (From p In u.SE_GROUPS Select p.ID).ToList
                lstOrgs = (From p In Context.SE_USER_ORG_ACCESS
                           From org In Context.HUV_ORGANIZATION_CHILD.Where(Function(f) f.ID = p.ORG_ID)
                           From parent In Context.HU_ORGANIZATION.Where(Function(f) f.ID = org.PARENT_ID).DefaultIfEmpty
                           From comittee In Context.HU_ORGANIZATION.Where(Function(f) f.ID = org.ID).DefaultIfEmpty
                           Where p.USER_ID = u.ID
                           Order By org.CODE, org.NAME_VN
                           Select New OrganizationDTO With {
                .ID = org.ID,
                .CODE = org.CODE,
                .NAME_VN = org.NAME_VN,
                .NAME_EN = org.NAME_VN,
                .PARENT_ID = org.PARENT_ID,
                .PARENT_NAME = parent.NAME_VN,
                .HIERARCHICAL_PATH = org.HIERARCHICAL_PATH,
                .ACTFLG = org.STATUS,
                .DISSOLVE_DATE = org.DISSOLVE_DATE,
                .DESCRIPTION_PATH = org.DESCRIPTION_PATH,
                .CHILD_COUNT = org.CHILD_COUNT,
                .ORD_NO = org.ORD_NO,
                .UY_BAN = comittee.UY_BAN}).Distinct().ToList

                If lstOrgs.Count > 0 AndAlso (From p In lstOrgs Where p.HIERARCHICAL_PATH = "1").Count = 0 Then
                    Dim lstOrgPer = lstOrgs.Select(Function(f) f.ID).ToList
                    Dim lstOrgID As New List(Of Decimal)
                    For Each org In lstOrgs
                        If org.HIERARCHICAL_PATH <> "" Then
                            If org.HIERARCHICAL_PATH.Split(";").Length > 1 Then
                                For i As Integer = 0 To org.HIERARCHICAL_PATH.Split(";").Length - 2
                                    Dim str = org.HIERARCHICAL_PATH.Split(";")(i)
                                    If str <> "" Then
                                        Dim orgid = Decimal.Parse(str)
                                        If Not lstOrgPer.Contains(orgid) And Not lstOrgID.Contains(orgid) Then
                                            lstOrgID.Add(orgid)
                                        End If
                                    End If
                                Next
                            End If
                        End If
                    Next
                    If lstOrgID.Count > 0 Then
                        Dim lstOrgNotPer = (From p In Context.HUV_ORGANIZATION_CHILD
                                            From parent In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.PARENT_ID).DefaultIfEmpty
                                            From comittee In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ID).DefaultIfEmpty
                                            Where lstOrgID.Contains(p.ID)
                                            Select New OrganizationDTO With {
                        .ID = p.ID,
                        .CODE = p.CODE,
                        .NAME_VN = p.NAME_VN,
                        .NAME_EN = p.NAME_VN,
                        .PARENT_ID = p.PARENT_ID,
                        .PARENT_NAME = parent.NAME_VN,
                        .ACTFLG = p.STATUS,
                        .DISSOLVE_DATE = p.DISSOLVE_DATE,
                        .DESCRIPTION_PATH = p.DESCRIPTION_PATH,
                        .CHILD_COUNT = p.CHILD_COUNT,
                        .ORD_NO = p.ORD_NO,
                        .IS_NOT_PER = True,
                        .UY_BAN = comittee.UY_BAN}).ToList
                        lstOrgs.AddRange(lstOrgNotPer)
                        lstOrgs = (From p In lstOrgs Order By p.CODE, p.NAME_VN).ToList
                    End If
                End If

            Else
                lstOrgs = (From p In Context.HUV_ORGANIZATION_CHILD
                           From parent In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.PARENT_ID).DefaultIfEmpty
                           From comittee In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ID).DefaultIfEmpty
                           Order By p.CODE, p.NAME_VN
                           Select New OrganizationDTO With {
                .ID = p.ID,
                .CODE = p.CODE,
                .NAME_VN = p.NAME_VN,
                .NAME_EN = p.NAME_VN,
                .PARENT_ID = p.PARENT_ID,
                .PARENT_NAME = parent.NAME_VN,
                .ACTFLG = p.STATUS,
                .DISSOLVE_DATE = p.DISSOLVE_DATE,
                .DESCRIPTION_PATH = p.DESCRIPTION_PATH,
                .CHILD_COUNT = p.CHILD_COUNT,
                .ORD_NO = p.ORD_NO,
                .UY_BAN = comittee.UY_BAN}
                ).ToList
            End If
        End If
        Return lstOrgs
    End Function
    Public Function GetOrganizationLocationInfo(ByVal _orgId As Decimal) As OrganizationDTO
        Dim query As OrganizationDTO
        query = (From p In Context.HU_ORGANIZATION Where p.ID = _orgId
                 Select New OrganizationDTO With {
                     .ACTFLG = p.ACTFLG,
                     .CODE = p.CODE,
                     .DISSOLVE_DATE = p.DISSOLVE_DATE,
                     .FOUNDATION_DATE = p.FOUNDATION_DATE,
                     .ID = p.ID,
                     .NAME_EN = p.NAME_EN,
                     .NAME_VN = p.NAME_VN,
                     .PARENT_ID = p.PARENT_ID,
                     .PARENT_NAME = p.PARENT.NAME_VN,
                     .REMARK = p.REMARK,
                     .COST_CENTER_CODE = p.COST_CENTER_CODE}).FirstOrDefault
        Return query
    End Function

    Public Function GetOrganizationStructureInfo(ByVal _orgId As Decimal) As List(Of OrganizationStructureDTO)
        Dim query As New OrganizationStructureDTO
        Dim list As New List(Of OrganizationStructureDTO)
        query.PARENT_ID = _orgId

        Do While query.PARENT_ID IsNot Nothing
            query = (From p In Context.HU_ORGANIZATION Where p.ID = query.PARENT_ID
                     Select New OrganizationStructureDTO With {
                     .ID = p.ID,
                     .CODE = p.CODE,
                     .NAME_VN = p.NAME_VN,
                     .NAME_EN = p.NAME_EN,
                     .PARENT_ID = p.PARENT_ID}).FirstOrDefault
            list.Add(query)
        Loop
        Return list
    End Function
    Public Function GetListOrgImport(ByVal orgID As List(Of Decimal)) As List(Of OrganizationDTO)
        Dim result = (From p In Context.HU_ORGANIZATION
                      Where orgID.Contains(p.ID)
                      Select New OrganizationDTO With {.ID = p.ID, .NAME_VN = p.NAME_VN}).ToList()
        Return result
    End Function
#End Region

#Region "Employee"
    Public Function GetEmployeeSignToPopupFind(_filter As EmployeePopupFindListDTO,
                                            ByVal PageIndex As Integer,
                                            ByVal PageSize As Integer,
                                            ByRef Total As Integer,
                                            Optional ByVal Sorts As String = "EMPLOYEE_CODE asc",
                                            Optional ByVal log As UserLog = Nothing,
                                            Optional ByVal _param As ParamDTO = Nothing) As List(Of EmployeePopupFindListDTO)

        Try
            Dim userName As String
            Dim common As String = "Dùng chung"
            Using cls As New DataAccess.QueryData
                userName = log.Username
                If _filter.LoadAllOrganization Then
                    userName = "ADMIN"
                End If
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = userName,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using

            Dim query = From sign In Context.HU_SIGNER.Where(Function(f) f.ACTFLG = 1)
                        From p In Context.HU_EMPLOYEE.Where(Function(f) f.ID = sign.SIGNER_ID).DefaultIfEmpty
                        From cv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = p.ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = sign.ORG_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
                        From gender In Context.OT_OTHER_LIST.Where(Function(f) f.ID = cv.GENDER And f.TYPE_ID = 34).DefaultIfEmpty
                        From work_status In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.WORK_STATUS And f.TYPE_ID = 59).DefaultIfEmpty
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) sign.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = userName)
                        From te In Context.HU_TERMINATE.Where(Function(f) p.ID = f.EMPLOYEE_ID).DefaultIfEmpty
            'Dim queryCommon = From sign In Context.HU_SIGNER.Where(Function(f) f.ACTFLG = 1 And f.ORG_ID = -1)
            '                  From p In Context.HU_EMPLOYEE.Where(Function(f) f.EMPLOYEE_CODE = sign.SIGNER_CODE).DefaultIfEmpty
            '                  From cv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = p.ID).DefaultIfEmpty
            '                  From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = sign.ORG_ID).DefaultIfEmpty
            '                  From t In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
            '                  From gender In Context.OT_OTHER_LIST.Where(Function(f) f.ID = cv.GENDER And f.TYPE_ID = 34).DefaultIfEmpty
            '                  From work_status In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.WORK_STATUS And f.TYPE_ID = 59).DefaultIfEmpty
            '                  From k In Context.SE_CHOSEN_ORG.Where(Function(f) sign.ORG_ID = f.ORG_ID).DefaultIfEmpty
            '                  From te In Context.HU_TERMINATE.Where(Function(f) p.ID = f.EMPLOYEE_ID).DefaultIfEmpty

            'If _filter.MustHaveContract Then
            '    query = query.Where(Function(f) f.p.JOIN_DATE.HasValue)
            '    'queryCommon = queryCommon.Where(Function(f) f.p.JOIN_DATE.HasValue)
            'End If
            Dim dateNow As Date? = Date.Now.Date

            If Not _filter.IsOnlyWorkingWithoutTer Then
                If _filter.IS_TER Then
                    query = query.Where(Function(f) f.p.WORK_STATUS = 257 And f.p.TER_EFFECT_DATE <= dateNow)
                    'queryCommon = queryCommon.Where(Function(f) f.p.WORK_STATUS = 257 And f.p.TER_EFFECT_DATE <= dateNow)
                Else
                    query = query.Where(Function(f) f.p.WORK_STATUS <> 257)
                    'queryCommon = queryCommon.Where(Function(f) f.p.WORK_STATUS <> 257)
                End If
            End If

            If _filter.EMPLOYEE_CODE <> "" Then
                query = query.Where(Function(f) f.p.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper) Or
                                        f.p.FULLNAME_VN.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If
            'Dim tempQuery = query.Union(queryCommon)
            Dim tempQuery = query
            Dim lst = tempQuery.Select(Function(f) New EmployeePopupFindListDTO With {
                            .EMPLOYEE_CODE = f.sign.SIGNER_CODE,
                            .ID = f.p.ID,
                            .EMPLOYEE_ID = f.p.ID,
                            .FULLNAME_VN = f.p.FULLNAME_VN,
                            .JOIN_DATE = f.p.JOIN_DATE,
                            .ORG_NAME = If(f.sign.ORG_ID = -1, common, f.o.NAME_VN),
                            .ORG_DESC = f.o.DESCRIPTION_PATH,
                            .GENDER = f.gender.NAME_VN,
                            .TITLE_NAME = f.t.NAME_VN})
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function GetEmployeeSignToPopupFind_V2(_filter As EmployeePopupFindListDTO,
                                                  ByVal PageIndex As Integer,
                                                  ByVal PageSize As Integer,
                                                  ByRef Total As Integer,
                                                  Optional ByVal Sorts As String = "EMPLOYEE_CODE asc",
                                                  Optional ByVal log As UserLog = Nothing,
                                                  Optional ByVal _param As ParamDTO = Nothing) As List(Of EmployeePopupFindListDTO)

        Try
            Dim userName As String
            userName = log.Username
            If _filter.LoadAllOrganization Then
                userName = "ADMIN"
            End If

            Dim lst As New List(Of EmployeePopupFindListDTO)

            Using cls As New DataAccess.QueryData
                Dim table As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_EMPLOYEE_SIGN_TO_POPUP_FIND",
                                                          New With {.P_USERNAME = userName,
                                                                    .P_FUNCTION_NAME = _filter.FUNCTION_NAME,
                                                                    .P_ORG_ID = _param.ORG_ID,
                                                                    .P_IS_DISSOLVE = _param.IS_DISSOLVE,
                                                                    .P_EMPLOYEE_ORG = _filter.ORG_ID,
                                                                    .P_EFFECT_DATE = _filter.TER_EFFECT_DATE,
                                                                    .P_CUR = cls.OUT_CURSOR})

                If table IsNot Nothing AndAlso table.Rows.Count > 0 Then
                    lst = (From dr As DataRow In table.Rows
                           Select New EmployeePopupFindListDTO With {
                                .ID = If(Not IsDBNull(dr("ID")), Decimal.Parse(dr("ID").ToString), Nothing),
                                .EMPLOYEE_ID = If(Not IsDBNull(dr("EMPLOYEE_ID")), Decimal.Parse(dr("EMPLOYEE_ID").ToString), Nothing),
                                .EMPLOYEE_CODE = If(Not IsDBNull(dr("EMPLOYEE_CODE")), dr("EMPLOYEE_CODE").ToString, ""),
                                .FULLNAME_VN = If(Not IsDBNull(dr("FULLNAME_VN")), dr("FULLNAME_VN").ToString, ""),
                                .JOIN_DATE = If(Not IsDBNull(dr("JOIN_DATE")), Date.Parse(dr("JOIN_DATE").ToString), Nothing),
                                .WORK_STATUS = If(Not IsDBNull(dr("WORK_STATUS")), dr("WORK_STATUS").ToString, ""),
                                .TER_EFFECT_DATE = If(Not IsDBNull(dr("TER_EFFECT_DATE")), Date.Parse(dr("TER_EFFECT_DATE").ToString), Nothing),
                                .ORG_NAME = If(Not IsDBNull(dr("ORG_NAME")), dr("ORG_NAME").ToString, ""),
                                .ORG_DESC = If(Not IsDBNull(dr("ORG_DESC")), dr("ORG_DESC").ToString, ""),
                                .GENDER = If(Not IsDBNull(dr("GENDER")), dr("GENDER").ToString, ""),
                                .TITLE_NAME = If(Not IsDBNull(dr("TITLE_NAME")), dr("TITLE_NAME").ToString, ""),
                                .FUNCTION_NAME = If(Not IsDBNull(dr("FUNCTION_NAME")), dr("FUNCTION_NAME").ToString, "")
                          }).ToList
                End If
            End Using

            Dim query = lst.AsQueryable

            Dim dateNow As Date? = Date.Now.Date

            If Not _filter.IsOnlyWorkingWithoutTer Then
                If _filter.IS_TER Then
                    query = query.Where(Function(f) f.WORK_STATUS = "257" And f.TER_EFFECT_DATE <= dateNow)
                Else
                    query = query.Where(Function(f) f.WORK_STATUS <> "257")
                End If
            End If

            If _filter.EMPLOYEE_CODE <> "" Then
                query = query.Where(Function(f) f.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper) Or
                                        f.FULLNAME_VN.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If

            If _filter.FUNCTION_NAME <> "" Then
                query = query.Where(Function(f) f.FUNCTION_NAME.ToUpper.Contains(_filter.FUNCTION_NAME.ToUpper))
            End If

            query = query.OrderBy(Sorts)
            Total = query.Count
            query = query.Skip(PageIndex * PageSize).Take(PageSize)

            Return query.ToList
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetEmployeeToPopupFind(_filter As EmployeePopupFindListDTO,
                                            ByVal PageIndex As Integer,
                                            ByVal PageSize As Integer,
                                            ByRef Total As Integer,
                                            Optional ByVal Sorts As String = "EMPLOYEE_CODE asc",
                                            Optional ByVal log As UserLog = Nothing,
                                            Optional ByVal _param As ParamDTO = Nothing) As List(Of EmployeePopupFindListDTO)

        Try
            Dim userName As String
            Using cls As New DataAccess.QueryData
                userName = log.Username.ToUpper
                If _filter.LoadAllOrganization Or _filter.is_All = True Then
                    userName = "ADMIN"
                End If
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = userName,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using


            Dim query = From p In Context.HU_EMPLOYEE
                        From cv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = p.ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
                        From staffRank In Context.HU_JOB_BAND.Where(Function(f) f.ID = p.STAFF_RANK_ID).DefaultIfEmpty
                        From title_group In Context.OT_OTHER_LIST.Where(Function(f) f.ID = t.TITLE_GROUP_ID).DefaultIfEmpty
                        From gender In Context.OT_OTHER_LIST.Where(Function(f) f.ID = cv.GENDER And f.TYPE_ID = 34).DefaultIfEmpty
                        From work_status In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.WORK_STATUS And f.TYPE_ID = 59).DefaultIfEmpty
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) p.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = userName)
                        From te In Context.HU_TERMINATE.Where(Function(f) p.ID = f.EMPLOYEE_ID).DefaultIfEmpty
                        From emp_stt In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.EMP_STATUS).DefaultIfEmpty
            'From te_status In Context.OT_OTHER_LIST.Where(Function(f) f.ID = te.STATUS_ID And f.NAME_VN.ToUpper.Trim <> "PHÊ DUYỆT")

            If _filter.FUNCTION_NAME <> "" Then
                Dim lstSigner = (From p In Context.HU_SIGNER_SETUP
                                 From f In Context.SE_FUNCTION.Where(Function(f) f.ID = p.FUNC_ID)
                                 Where f.FID.ToUpper = _filter.FUNCTION_NAME.ToUpper And p.ORG_ID IsNot Nothing Select p.SIGNER_ID).ToList
                query = query.Where(Function(f) lstSigner.Contains(f.p.ID))
            End If
            If _filter.EMPLOYEE_CODE <> "" Then
                query = query.Where(Function(f) f.p.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper) Or
                                        f.p.FULLNAME_VN.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If

            If _filter.DIRECT_MANAGER IsNot Nothing Then
                query = query.Where(Function(f) f.p.DIRECT_MANAGER.ToString().Contains(_filter.DIRECT_MANAGER) Or
                                        f.p.FULLNAME_VN.ToUpper.Contains(_filter.DIRECT_MANAGER))
            End If

            If _filter.DM_ID.HasValue AndAlso _filter.DM_ID.Value Then
                query = query.Where(Function(f) f.p.DM_ID <> 0)
            End If
            'If _filter.IS_KIEMNHIEM = 0 Then
            '    query = query.Where(Function(f) f.p.EMP_STATUS <> 0)
            'End If

            If _filter.IS_SHOW_KIEMNHIEM Then
                If _filter.IS_KIEMNHIEM = -1 Then
                    query = query.Where(Function(f) f.p.IS_KIEM_NHIEM IsNot Nothing)
                Else
                    query = query.Where(Function(f) f.p.IS_KIEM_NHIEM Is Nothing)
                End If
                If _filter.MustHaveContract Then
                    query = query.Where(Function(f) f.p.CONTRACT_ID.HasValue)
                End If
            Else
                If _filter.MustHaveContract Then
                    query = query.Where(Function(f) f.p.CONTRACT_ID.HasValue And f.p.IS_KIEM_NHIEM Is Nothing)
                End If
            End If
            Dim str As String = "Kiêm nhiệm"


            Dim dateNow = Date.Now.Date
            If Not _filter.IsOnlyWorkingWithoutTer Then
                If _filter.IS_TER Then
                    query = query.Where(Function(f) f.p.WORK_STATUS = 257 And f.p.TER_EFFECT_DATE <= dateNow)
                Else
                    query = query.Where(Function(f) f.p.WORK_STATUS Is Nothing Or (f.p.WORK_STATUS IsNot Nothing And
                                             (f.p.WORK_STATUS <> 257 Or (f.p.WORK_STATUS <> 257 And f.te.LAST_DATE > dateNow))))
                End If
            Else
                query = query.Where(Function(f) f.p.WORK_STATUS IsNot Nothing And f.p.WORK_STATUS = 258)
            End If

            Select Case _filter.IS_3B
                Case 1
                    query = query.Where(Function(f) f.p.IS_3B = True)
                Case 2
                    query = query.Where(Function(f) f.p.IS_3B = False Or f.p.IS_3B Is Nothing)

            End Select
            Dim lst = query.Select(Function(f) New EmployeePopupFindListDTO With {
                            .EMPLOYEE_CODE = f.p.EMPLOYEE_CODE,
                            .ID = f.p.ID,
                            .EMPLOYEE_ID = f.p.ID,
                            .FULLNAME_VN = f.p.FULLNAME_VN,
                            .EMP_STATUS = If(f.p.IS_KIEM_NHIEM IsNot Nothing, str, f.emp_stt.NAME_VN),
                            .FULLNAME_EN = f.p.FULLNAME_EN,
                            .JOIN_DATE = f.p.JOIN_DATE,
                            .ORG_NAME = f.o.NAME_VN,
                            .ORG_DESC = f.o.DESCRIPTION_PATH,
                            .GENDER = f.gender.NAME_VN,
                            .WORK_STATUS = f.work_status.NAME_VN,
                            .TITLE_NAME = f.t.NAME_VN,
                            .TITLE_CODE = f.t.CODE,
                            .STAFF_RANK_NAME = f.staffRank.NAME_VN,
                            .IMAGE = f.cv.IMAGE,
                            .WORK_EMAIL = f.cv.WORK_EMAIL,
                            .MOBILE_PHONE = f.cv.MOBILE_PHONE,
                            .DIRECT_MANAGER = f.p.DIRECT_MANAGER,
                            .TITLE_GROUP_ID = f.t.TITLE_GROUP_ID,
                            .TITLE_GROUP_NAME = f.title_group.NAME_VN}) '.Where(Function(x) x.WORK_STATUS = "Đang làm việc")

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Function GetEmployeeFind(_filter As EmployeePopupFindListDTO,
                                            ByRef Total As Integer,
                                            Optional ByVal Sorts As String = "EMPLOYEE_CODE asc",
                                            Optional ByVal log As UserLog = Nothing,
                                            Optional ByVal _param As ParamDTO = Nothing) As List(Of EmployeePopupFindListDTO)

        Try
            Dim userName As String
            Using cls As New DataAccess.QueryData
                userName = log.Username.ToUpper
                If _filter.LoadAllOrganization Then
                    userName = "ADMIN"
                End If
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = userName,
                                           .P_ORGID = 1,
                                           .P_ISDISSOLVE = False})
            End Using
            Dim query = From p In Context.HU_EMPLOYEE
                        From cv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = p.ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
                        From gt In Context.OT_OTHER_LIST.Where(Function(f) f.ID = t.GROUP_EMPLOYEE_ID).DefaultIfEmpty
                        From gender In Context.OT_OTHER_LIST.Where(Function(f) f.ID = cv.GENDER And f.TYPE_ID = 34).DefaultIfEmpty
                        From work_status In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.WORK_STATUS And f.TYPE_ID = 59).DefaultIfEmpty
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) p.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = userName)
                        From te In Context.HU_TERMINATE.Where(Function(f) p.ID = f.EMPLOYEE_ID).DefaultIfEmpty
                        From mt In Context.HU_TITLE_BLD.Where(Function(f) f.ID = p.MATHE).DefaultIfEmpty
                        From emp_stt In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.EMP_STATUS).DefaultIfEmpty

            If _filter.EMPLOYEE_CODE <> "" Then
                query = query.Where(Function(f) f.p.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper) Or
                                        f.p.FULLNAME_VN.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If
            If _filter.DM_ID.HasValue And _filter.DM_ID Then
                query = query.Where(Function(f) f.p.DM_ID <> 0)
            End If
            Dim lst = query.Select(Function(f) New EmployeePopupFindListDTO With {
                            .EMPLOYEE_CODE = f.p.EMPLOYEE_CODE,
                            .ID = f.p.ID,
                            .WORK_EMAIL = f.cv.WORK_EMAIL,
                            .PER_EMAIL = f.cv.PER_EMAIL,
                            .MOBILE_PHONE = f.cv.MOBILE_PHONE,
                            .EMPLOYEE_ID = f.p.ID,
                            .FULLNAME_VN = f.p.FULLNAME_VN,
                            .FULLNAME_EN = f.p.FULLNAME_EN,
                            .JOIN_DATE = f.p.JOIN_DATE,
                            .ORG_NAME = f.o.NAME_VN,
                            .ORG_DESC = f.o.DESCRIPTION_PATH,
                            .GENDER = f.gender.NAME_VN,
                            .WORK_STATUS = f.work_status.NAME_VN,
                            .TITLE_NAME = f.t.NAME_VN,
                            .IMAGE = f.cv.IMAGE,
                            .DIRECT_MANAGER = f.p.DIRECT_MANAGER,
                            .TITLE_ID = f.p.TITLE_ID,
                            .ITIME_ID = f.p.ITIME_ID,
                            .ORG_ID = f.p.ORG_ID,
                            .MATHE = f.p.MATHE,
                            .MATHE_NAME = f.mt.NAME_VN,
                            .THETU = f.p.THETU,
                            .GROUP_EMPLOYEE_ID = f.t.GROUP_EMPLOYEE_ID,
                            .GROUP_EMPLOYEE_NAME = f.gt.NAME_VN}) '.Where(Function(x) x.WORK_STATUS = "Đang làm việc")

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            'lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Function GetEmployeeToPopupFind2(ByVal _filter As EmployeePopupFindListDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        ByVal request_id As Integer,
                                        Optional ByVal Sorts As String = "EMPLOYEE_CODE asc",
                                        Optional ByVal log As UserLog = Nothing,
                                        Optional ByVal _param As ParamDTO = Nothing) As List(Of EmployeePopupFindListDTO)

        Try
            Dim query = From p In Context.HU_EMPLOYEE
                        From cv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = p.ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
                        From gender In Context.OT_OTHER_LIST.Where(Function(f) f.ID = cv.GENDER And f.TYPE_ID = 34).DefaultIfEmpty
                        From work_status In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.WORK_STATUS And f.TYPE_ID = 59).DefaultIfEmpty
            'xemlai...
            'From k In Context.ORG_TEMP_TABLE.Where(Function(f) p.ORG_ID = f.ORG_ID And f.REQUEST_ID = request_id)

            If _filter.EMPLOYEE_CODE <> "" Then
                query = query.Where(Function(f) f.p.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper) Or
                                        f.p.FULLNAME_VN.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If

            If _filter.MustHaveContract Then
                query = query.Where(Function(f) f.p.CONTRACT_ID.HasValue)
            End If
            Dim dateNow = Date.Now.Date
            If Not _filter.IsOnlyWorkingWithoutTer Then
                If _filter.IS_TER Then
                    query = query.Where(Function(f) f.p.WORK_STATUS = 257 And f.p.TER_EFFECT_DATE <= dateNow)
                Else
                    query = query.Where(Function(f) f.p.WORK_STATUS Is Nothing Or (f.p.WORK_STATUS IsNot Nothing And
                                             (f.p.WORK_STATUS <> 257 Or (f.p.WORK_STATUS = 257 And f.p.TER_EFFECT_DATE > dateNow))))
                End If
            Else
                query = query.Where(Function(f) f.p.WORK_STATUS Is Nothing Or (f.p.WORK_STATUS IsNot Nothing And f.p.WORK_STATUS <> 257))
            End If
            Select Case _filter.IS_3B
                Case 1
                    query = query.Where(Function(f) f.p.IS_3B = True)
                Case 2
                    query = query.Where(Function(f) f.p.IS_3B = False)

            End Select

            Dim lst = query.Select(Function(f) New EmployeePopupFindListDTO With {
                            .EMPLOYEE_CODE = f.p.EMPLOYEE_CODE,
                            .ID = f.p.ID,
                            .EMPLOYEE_ID = f.p.ID,
                            .FULLNAME_VN = f.p.FULLNAME_VN,
                            .FULLNAME_EN = f.p.FULLNAME_EN,
                            .JOIN_DATE = f.p.JOIN_DATE,
                            .ORG_NAME = f.o.NAME_VN,
                            .ORG_DESC = f.o.DESCRIPTION_PATH,
                            .GENDER = f.gender.NAME_VN,
                            .WORK_STATUS = f.work_status.NAME_VN,
                            .TITLE_NAME = f.t.NAME_VN})

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function GetEmployeeToPopupFind_EmployeeID(ByVal _empId As List(Of Decimal)) As List(Of EmployeePopupFindDTO)
        Dim result = (From p In Context.HU_EMPLOYEE
                      From ov1 In Context.HUV_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                      From o2 In Context.HU_ORGANIZATION.Where(Function(f) f.ID = ov1.ORG_ID2).DefaultIfEmpty
                      From ov In Context.HUV_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                      From o1 In Context.HU_ORGANIZATION.Where(Function(f) f.ID = ov.ORG_ID1).DefaultIfEmpty
                      From cv In Context.HU_EMPLOYEE_CV.Where(Function(cv) cv.EMPLOYEE_ID = p.ID).DefaultIfEmpty
                      From s In Context.HU_JOB_BAND.Where(Function(s) s.ID = p.STAFF_RANK_ID).DefaultIfEmpty
                      From t In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
                      From gt In Context.OT_OTHER_LIST.Where(Function(f) f.ID = t.GROUP_EMPLOYEE_ID).DefaultIfEmpty
                      From titleGr In Context.OT_OTHER_LIST.Where(Function(f) f.ID = t.TITLE_GROUP_ID).DefaultIfEmpty
                      From birth In Context.HU_NATION.Where(Function(f) f.ID = cv.BIRTH_PLACE).DefaultIfEmpty
                      From ot1 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.OBJECT_LABOR).DefaultIfEmpty
                      From ot2 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.OBJECTTIMEKEEPING).DefaultIfEmpty
                      From obj_emp In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.OBJECT_EMPLOYEE_ID).DefaultIfEmpty
                      From obj_attendant In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.OBJECT_ATTENDANT_ID).DefaultIfEmpty
                      From work_place In Context.HU_WORK_PLACE.Where(Function(f) f.ID = p.WORK_PLACE_ID).DefaultIfEmpty
                      From card In Context.HU_TITLE_BLD.Where(Function(f) f.ID = p.MATHE).DefaultIfEmpty
                      Order By p.EMPLOYEE_CODE
                      Where (_empId.Contains(p.ID))
                      Select New EmployeePopupFindDTO With {
                          .EMPLOYEE_CODE = p.EMPLOYEE_CODE,
                          .WORK_EMAIL = cv.WORK_EMAIL,
                          .PER_EMAIL = cv.PER_EMAIL,
                          .MOBILE_PHONE = cv.MOBILE_PHONE,
                          .ORG_NAME_1 = o1.SHORT_NAME,
                          .ORG_NAME_2 = o2.SHORT_NAME,
                          .ID = p.ID,
                          .EMPLOYEE_ID = p.ID,
                          .JOIN_DATE = p.JOIN_DATE,
                          .FULLNAME_VN = p.FULLNAME_VN,
                          .ORG_ID = p.ORG_ID,
                          .ORG_NAME = p.HU_ORGANIZATION.NAME_VN,
                          .ORG_DESC = p.HU_ORGANIZATION.DESCRIPTION_PATH,
                          .TITLE_ID = p.TITLE_ID,
                          .TITLE_NAME = t.NAME_VN,
                          .TITLE_CODE = t.CODE,
                          .TITLE_GROUP_ID = t.TITLE_GROUP_ID,
                          .TITLE_GROUP_NAME = titleGr.NAME_VN,
                          .BIRTH_DATE = cv.BIRTH_DATE,
                          .BIRTH_PLACE = cv.BIRTH_PLACE,
                          .BIRTH_PLACE_NAME = birth.NAME_VN,
                          .STAFF_RANK_ID = p.STAFF_RANK_ID,
                          .STAFF_RANK_NAME = s.LEVEL_FROM,
                          .IMAGE = cv.IMAGE,
                          .OBJECT_LABOR = p.OBJECT_LABOR,
                          .OBJECTTIMEKEEPING = p.OBJECTTIMEKEEPING,
                          .OBJECT_NAME = ot2.NAME_VN,
                          .LABOUR_NAME = ot1.NAME_VN,
                          .ID_NO = cv.ID_NO,
                          .OBJECT_EMPLOYEE_ID = p.OBJECT_EMPLOYEE_ID,
                          .OBJECT_EMPLOYEE_NAME = obj_emp.NAME_VN,
                          .OBJECT_ATTENDANT_ID = p.OBJECT_ATTENDANT_ID,
                          .OBJECT_ATTENDANT_NAME = obj_attendant.NAME_VN,
                          .WORK_PLACE_ID = p.WORK_PLACE_ID,
                          .ITIME_ID = p.ITIME_ID,
                          .WORK_PLACE_NAME = work_place.NAME_VN,
                          .MATHE = p.MATHE,
                          .MATHE_NAME = card.NAME_VN,
                          .THETU = p.THETU,
                          .GROUP_EMPLOYEE_ID = t.GROUP_EMPLOYEE_ID,
                          .GROUP_EMPLOYEE_NAME = gt.NAME_VN})
        Return result.ToList
    End Function

    Public Function GetEmployeeID(ByVal _empId As Decimal) As EmployeePopupFindDTO
        Dim result = (From p In Context.HU_EMPLOYEE
                      From cv In Context.HU_EMPLOYEE_CV.Where(Function(cv) cv.EMPLOYEE_ID = p.ID).DefaultIfEmpty
                      From s In Context.HU_JOB_BAND.Where(Function(f) f.ID = p.STAFF_RANK_ID).DefaultIfEmpty
                      From t In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
                      From tg In Context.OT_OTHER_LIST.Where(Function(f) f.ID = t.TITLE_GROUP_ID).DefaultIfEmpty
                      From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                      Where p.ID = _empId
                      Select New EmployeePopupFindDTO With {
                          .EMPLOYEE_CODE = p.EMPLOYEE_CODE,
                          .ID = p.ID,
                          .EMPLOYEE_ID = p.ID,
                          .JOIN_DATE = p.JOIN_DATE,
                          .FULLNAME_VN = p.FULLNAME_VN,
                          .ORG_ID = p.ORG_ID,
                          .ORG_NAME = o.NAME_VN,
                          .ORG_DESC = o.DESCRIPTION_PATH,
                          .TITLE_ID = p.TITLE_ID,
                          .TITLE_NAME = t.NAME_VN,
                          .TITLE_GROUP_ID = t.TITLE_GROUP_ID,
                          .TITLE_GROUP_NAME = tg.NAME_VN,
                          .BIRTH_DATE = cv.BIRTH_DATE,
                          .BIRTH_PLACE = cv.BIRTH_PLACE,
                          .STAFF_RANK_ID = p.STAFF_RANK_ID,
                          .STAFF_RANK_NAME = s.LEVEL_FROM}).SingleOrDefault

        Return result
    End Function
#End Region

#Region "Title"

    Public Function GetTitle(ByVal _filter As TitleDTO, ByVal PageIndex As Integer,
                                    ByVal PageSize As Integer,
                                    ByRef Total As Integer,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of TitleDTO)

        Try
            Dim query = From p In Context.HU_TITLE
                        From group In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TITLE_GROUP_ID).DefaultIfEmpty
                        From orgLv In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        Where p.ACTFLG = "A"

            Dim lst = query.Select(Function(p) New TitleDTO With {
                                  .ID = p.p.ID,
                                  .CODE = p.p.CODE,
                                  .NAME_EN = p.p.NAME_EN,
                                  .NAME_VN = p.p.NAME_VN,
                                  .REMARK = p.p.REMARK,
                                  .TITLE_GROUP_ID = p.p.TITLE_GROUP_ID,
                                  .TITLE_GROUP_NAME = p.group.NAME_VN,
                                  .CREATED_DATE = p.p.CREATED_DATE,
                                  .ACTFLG = p.p.ACTFLG,
                                  .ORG_ID_NAME = p.orgLv.NAME_VN})

            If _filter.CODE <> "" Then
                lst = lst.Where(Function(p) p.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
            End If
            If _filter.ORG_ID_NAME <> "" Then
                lst = lst.Where(Function(p) p.ORG_ID_NAME.ToUpper.Contains(_filter.ORG_ID_NAME.ToUpper))
            End If
            If _filter.NAME_EN <> "" Then
                lst = lst.Where(Function(p) p.NAME_EN.ToUpper.Contains(_filter.NAME_EN.ToUpper))
            End If
            If _filter.NAME_VN <> "" Then
                lst = lst.Where(Function(p) p.NAME_VN.ToUpper.Contains(_filter.NAME_VN.ToUpper))
            End If
            If _filter.TITLE_GROUP_NAME <> "" Then
                lst = lst.Where(Function(p) p.TITLE_GROUP_NAME.ToUpper.Contains(_filter.TITLE_GROUP_NAME.ToUpper))
            End If
            If _filter.REMARK <> "" Then
                lst = lst.Where(Function(p) p.REMARK.ToUpper.Contains(_filter.REMARK.ToUpper))
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetTitleFromId(ByVal _lstIds As List(Of Decimal)) As List(Of TitleDTO)
        Try
            Dim query = (From p In Context.HU_TITLE Where _lstIds.Contains(p.ID)
                         Select New TitleDTO With {
                                  .ID = p.ID,
                                  .CODE = p.CODE,
                                  .NAME_EN = p.NAME_EN,
                                  .NAME_VN = p.NAME_VN,
                                  .ACTFLG = p.ACTFLG}).ToList
            Return query
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "System Configurate"

    Public Function GetConfig(ByVal eModule As ModuleID) As Dictionary(Of String, String)
        Using cofig As New SystemConfig
            Try
                Return cofig.GetConfig(eModule)
            Catch ex As Exception
                Throw ex
            End Try
        End Using

    End Function

    Public Function UpdateConfig(ByVal _lstConfig As Dictionary(Of String, String), ByVal eModule As ModuleID) As Boolean
        Using cofig As New SystemConfig
            Try
                Return cofig.UpdateConfig(_lstConfig, eModule)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

#End Region

#Region "Reminder per user"

    Public Function GetReminderConfig(ByVal username As String) As Dictionary(Of Integer, String)
        Using cofig As New SystemConfig
            Try
                Return cofig.GetReminderConfig(username)
            Catch ex As Exception
                Throw
            End Try
        End Using

    End Function

    Public Function SetReminderConfig(ByVal username As String, ByVal type As Integer, ByVal value As String) As Boolean
        Using cofig As New SystemConfig
            Try
                Return cofig.SetReminderConfig(username, type, value)
            Catch ex As Exception
                Throw
            End Try
        End Using
    End Function

    Public Function SetGeneralConfig(ByVal _lstConfig As Dictionary(Of String, String), ByVal eModule As ModuleID) As Boolean
        Using cofig As New SystemConfig
            Try
                Return cofig.UpdateConfig(_lstConfig, eModule)
            Catch ex As Exception
                Throw
            End Try
        End Using
    End Function

#End Region

#Region "Mail_Content"
    Public Function GetMailTemplate(ByVal _filter As MailTemplateDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of MailTemplateDTO)

        Try
            Dim query = From p In Context.SE_MAIL_TEMPLATE
                        From g In Context.SE_MODULE.Where(Function(x) x.MID = p.GROUP_MAIL).DefaultIfEmpty
                        Select New MailTemplateDTO With {.ID = p.ID,
                                                         .CODE = p.CODE,
                                                         .NAME = p.NAME,
                                                         .CONTENT = p.CONTENT,
                                                         .MAIL_CC = p.MAIL_CC,
                                                         .IS_MAIL_CC = p.IS_MAIL_CC,
                                                         .GROUP_MAIL = p.GROUP_MAIL,
                                                         .GROUP_MAIL_NAME = g.NAME_DESC,
                                                         .CREATED_DATE = p.CREATED_DATE,
                                                         .TITLE = p.TITLE,
                                                         .REMARK = p.REMARK}
            Dim lst = query

            If _filter.CODE <> "" Then
                lst = lst.Where(Function(p) p.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
            End If
            If _filter.NAME <> "" Then
                lst = lst.Where(Function(p) p.NAME.ToUpper.Contains(_filter.NAME.ToUpper))
            End If
            If _filter.MAIL_CC <> "" Then
                lst = lst.Where(Function(p) p.MAIL_CC.ToUpper.Contains(_filter.MAIL_CC.ToUpper))
            End If
            If _filter.REMARK <> "" Then
                lst = lst.Where(Function(p) p.REMARK.ToUpper.Contains(_filter.REMARK.ToUpper))
            End If
            If _filter.GROUP_MAIL_NAME <> "" Then
                lst = lst.Where(Function(p) p.GROUP_MAIL_NAME.ToUpper.Contains(_filter.GROUP_MAIL_NAME.ToUpper))
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetMailTemplateBaseCode(ByVal code As String, ByVal group As String) As MailTemplateDTO
        Try
            Dim query = (From p In Context.SE_MAIL_TEMPLATE Where p.CODE.ToUpper() = code.ToUpper() AndAlso p.GROUP_MAIL.ToUpper() = group.ToUpper()
                         Select New MailTemplateDTO With {.ID = p.ID,
                                                          .CODE = p.CODE,
                                                          .TITLE = p.TITLE,
                                                          .CONTENT = p.CONTENT,
                                                          .NAME = p.NAME,
                                                          .MAIL_CC = p.MAIL_CC,
                                                          .GROUP_MAIL = p.GROUP_MAIL}).FirstOrDefault

            Return query

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function InsertMailTemplate(ByVal mailTemplate As MailTemplateDTO, ByVal log As UserLog) As Boolean
        Dim objMailTemplate As New SE_MAIL_TEMPLATE
        Try
            objMailTemplate.ID = Utilities.GetNextSequence(Context, Context.SE_MAIL_TEMPLATE.EntitySet.Name)
            objMailTemplate.CODE = mailTemplate.CODE
            objMailTemplate.NAME = mailTemplate.NAME
            objMailTemplate.TITLE = mailTemplate.TITLE
            objMailTemplate.CONTENT = mailTemplate.CONTENT
            objMailTemplate.MAIL_CC = mailTemplate.MAIL_CC
            objMailTemplate.IS_MAIL_CC = mailTemplate.IS_MAIL_CC
            objMailTemplate.REMARK = mailTemplate.REMARK
            objMailTemplate.GROUP_MAIL = mailTemplate.GROUP_MAIL

            Context.SE_MAIL_TEMPLATE.AddObject(objMailTemplate)
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Function ModifyMailTemplate(ByVal mailTemplate As MailTemplateDTO, ByVal log As UserLog) As Boolean
        Try
            Dim objMailTemplate As SE_MAIL_TEMPLATE = (From p In Context.SE_MAIL_TEMPLATE Where p.ID = mailTemplate.ID).FirstOrDefault
            If objMailTemplate IsNot Nothing Then
                objMailTemplate.CODE = mailTemplate.CODE
                objMailTemplate.NAME = mailTemplate.NAME
                objMailTemplate.TITLE = mailTemplate.TITLE
                objMailTemplate.CONTENT = mailTemplate.CONTENT
                objMailTemplate.MAIL_CC = mailTemplate.MAIL_CC
                objMailTemplate.IS_MAIL_CC = mailTemplate.IS_MAIL_CC
                objMailTemplate.REMARK = mailTemplate.REMARK
                objMailTemplate.GROUP_MAIL = mailTemplate.GROUP_MAIL
                Context.SaveChanges(log)
            Else
                Return False
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function DeleteMailTemplate(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstMailTemplate As List(Of SE_MAIL_TEMPLATE)
        Try

            lstMailTemplate = (From p In Context.SE_MAIL_TEMPLATE Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstMailTemplate.Count - 1
                Context.SE_MAIL_TEMPLATE.DeleteObject(lstMailTemplate(index))
            Next

            Context.SaveChanges()
            Return True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function CheckValidEmailTemplate(ByVal code As String, ByVal group As String) As Boolean
        Dim db = Context.SE_MAIL_TEMPLATE.SingleOrDefault(Function(x) x.CODE.ToUpper() = code.ToUpper() AndAlso x.GROUP_MAIL.ToUpper() = group.ToUpper())
        If db IsNot Nothing Then
            Return False
        End If
        Return True
    End Function
#End Region
    Public Function SE_RESET_USER(ByVal UserIDs As String) As Boolean
        Dim valueFormat As Integer = 0
        Try
            Using cls As New DataAccess.QueryData
                Dim obj = New With {.P_USER_IDS = UserIDs,
                                           .P_OUT = cls.OUT_NUMBER}
                cls.ExecuteStore("PKG_HCM_SYSTEM.SE_RESET_USER", obj)
                valueFormat = Integer.Parse(obj.P_OUT)
                Return CBool(valueFormat)
            End Using
        Catch ex As Exception
            Return False
        End Try
    End Function
#Region "SE_SYSTEM_MAINTAIN"
    Public Function CHECK_DUP_SYSTEM_MAINTAIN(ByVal effect_date As Date, Optional ByVal id As Decimal = 0) As Decimal

        If id = 0 Then
            Return (From p In Context.SE_SYSTEM_MAINTAIN Where p.EFFECT_DATE >= effect_date).Count
        Else
            Return (From p In Context.SE_SYSTEM_MAINTAIN Where p.ID <> id And p.EFFECT_DATE >= effect_date).Count
        End If
    End Function


    Public Function GetSystem_Maintain(ByVal _filter As SE_SYSTEM_MAINTAINDTO,
                                ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer,
                                Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of SE_SYSTEM_MAINTAINDTO)

        Dim query = (From p In Context.SE_SYSTEM_MAINTAIN
                     Order By p.EFFECT_DATE Descending Select New SE_SYSTEM_MAINTAINDTO With {
                     .ID = p.ID,
                     .CONTRACT_MAINTAIN = p.CONTRACT_MAINTAIN,
                     .EFFECT_DATE = p.EFFECT_DATE,
                     .EXPIRE_TDATE = p.EXPIRE_TDATE,
                     .SIGN_DATE = p.SIGN_DATE,
                     .NOTE = p.NOTE,
                     .CREATED_DATE = p.CREATED_DATE})

        If _filter.CONTRACT_MAINTAIN <> "" Then
            query = query.Where(Function(p) p.CONTRACT_MAINTAIN = _filter.CONTRACT_MAINTAIN)
        End If
        If _filter.EFFECT_DATE IsNot Nothing Then
            query = query.Where(Function(p) p.EFFECT_DATE = _filter.EFFECT_DATE)
        End If
        If _filter.EXPIRE_TDATE IsNot Nothing Then
            query = query.Where(Function(p) p.EXPIRE_TDATE = _filter.EXPIRE_TDATE)
        End If
        If _filter.SIGN_DATE IsNot Nothing Then
            query = query.Where(Function(p) p.SIGN_DATE = _filter.SIGN_DATE)
        End If
        If _filter.NOTE <> "" Then
            query = query.Where(Function(p) p.NOTE = _filter.NOTE)
        End If
        query = query.AsQueryable().OrderBy(Sorts)
        Total = query.Count
        query = query.Skip(PageIndex * PageSize).Take(PageSize)

        Dim result = (From p In query Select p)

        Return result.ToList
    End Function
    Public Function ModifySystem_Maintain(ByVal lstObj As SE_SYSTEM_MAINTAINDTO,
                                   ByVal userLog As UserLog) As Boolean
        Dim objData As New SE_SYSTEM_MAINTAIN With {.ID = lstObj.ID}
        Try
            objData = (From p In Context.SE_SYSTEM_MAINTAIN Where p.ID = objData.ID).FirstOrDefault
            objData.CONTRACT_MAINTAIN = lstObj.CONTRACT_MAINTAIN
            objData.EFFECT_DATE = lstObj.EFFECT_DATE
            objData.EXPIRE_TDATE = lstObj.EXPIRE_TDATE
            objData.SIGN_DATE = lstObj.SIGN_DATE
            objData.NOTE = lstObj.NOTE
            Context.SaveChanges(userLog)
            'gID = objData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function InsertSystem_Maintain(ByVal _item As SE_SYSTEM_MAINTAINDTO, ByVal log As UserLog) As Boolean
        Try
            Dim objData As New SE_SYSTEM_MAINTAIN
            objData.ID = Utilities.GetNextSequence(Context, Context.SE_SYSTEM_MAINTAIN.EntitySet.Name)
            objData.CONTRACT_MAINTAIN = _item.CONTRACT_MAINTAIN
            objData.EFFECT_DATE = _item.EFFECT_DATE
            objData.EXPIRE_TDATE = _item.EXPIRE_TDATE
            objData.SIGN_DATE = _item.SIGN_DATE
            objData.NOTE = _item.NOTE
            Context.SE_SYSTEM_MAINTAIN.AddObject(objData)
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'Public Function DeleteRegisterUser(ByVal lst_id As List(Of Decimal)) As Boolean
    '    Try
    '        For Each item In lst_id
    '            Dim dt = (From p In Context.SE_REGISTER_USER Where p.ID = item Select p).FirstOrDefault
    '            Context.SE_REGISTER_USER.DeleteObject(dt)
    '        Next
    '        Context.SaveChanges()
    '        Return True
    '    Catch ex As Exception

    '    End Try
    'End Function
    Public Function CHECK_SYSTEM_MAINTAIN() As String
        Try
            Using cls As New DataAccess.QueryData
                Dim STR As String = ""
                Dim obj = New With {.P_OUT = cls.OUT_CURSOR}
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.CHECK_SYSTEM_MAINTAIN", obj)
                If dtData IsNot Nothing AndAlso dtData.Rows.Count > 0 Then
                    If Not IsDBNull(dtData.Rows(0)("STR")) Then
                        STR = dtData.Rows(0)("STR")
                    Else
                        STR = ""
                    End If

                End If
                Return STR
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function CHECK_SYSTEM_MAINTAIN_IS_END() As Boolean
        Try
            Using cls As New DataAccess.QueryData
                Dim obj = New With {.P_OUT = cls.OUT_NUMBER}
                cls.ExecuteStore("PKG_COMMON_LIST.CHECK_SYSTEM_MAINTAIN_IS_END", obj)
                Return CBool(Integer.Parse(obj.P_OUT))
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GET_SYSTEM_MAINTAIN_IS_END() As String
        Try
            Using cls As New DataAccess.QueryData
                Dim STR As String = ""
                Dim obj = New With {.P_OUT = cls.OUT_CURSOR}
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_SYSTEM_MAINTAIN_IS_END", obj)
                If dtData IsNot Nothing AndAlso dtData.Rows.Count > 0 Then
                    If Not IsDBNull(dtData.Rows(0)("STR")) Then
                        STR = dtData.Rows(0)("STR")
                    Else
                        STR = ""
                    End If
                End If
                Return STR
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetSE_BACKGROUND_PORTALList() As List(Of SE_BACKGROUND_PORTALDTO)
        Dim query = (From p In Context.SE_BACKGROUND_PORTAL
                     Select New SE_BACKGROUND_PORTALDTO With {
                         .ID = p.ID,
                         .FROM_DATE = p.FROM_DATE,
                         .TO_DATE = p.TO_DATE,
                         .BACKGROUND = p.BACKGROUND,
                         .NOTE = p.NOTE,
                         .FILEPATH = p.FILEPATH,
                         .VIEWIMG = p.FILEPATH & "/" & p.BACKGROUND})
        Dim result = (From p In query Select p)
        Return result.ToList
    End Function
    Public Function ModifyBACKGROUND_PORTAL(ByVal lstObj As SE_BACKGROUND_PORTALDTO,
                                   ByVal userLog As UserLog) As Boolean
        Dim objData As New SE_BACKGROUND_PORTAL With {.ID = lstObj.ID}
        Try
            objData = (From p In Context.SE_BACKGROUND_PORTAL Where p.ID = objData.ID).FirstOrDefault
            objData.FROM_DATE = lstObj.FROM_DATE
            objData.TO_DATE = lstObj.TO_DATE
            objData.NOTE = lstObj.NOTE
            objData.BACKGROUND = lstObj.BACKGROUND
            objData.FILEPATH = lstObj.FILEPATH
            Context.SaveChanges(userLog)
            'gID = objData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function InsertBACKGROUND_PORTAL(ByVal _item As SE_BACKGROUND_PORTALDTO, ByVal log As UserLog) As Boolean
        Try
            Dim objData As New SE_BACKGROUND_PORTAL
            objData.ID = Utilities.GetNextSequence(Context, Context.SE_BACKGROUND_PORTAL.EntitySet.Name)
            objData.FROM_DATE = _item.FROM_DATE
            objData.TO_DATE = _item.TO_DATE
            objData.NOTE = _item.NOTE
            objData.BACKGROUND = _item.BACKGROUND
            objData.FILEPATH = _item.FILEPATH
            Context.SE_BACKGROUND_PORTAL.AddObject(objData)
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GET_SE_BACKGROUND_PORTAL() As String
        Try
            Using cls As New DataAccess.QueryData
                Dim STR As String = ""
                Dim obj = New With {.P_OUT = cls.OUT_CURSOR}
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_SE_BACKGROUND_PORTAL", obj)
                If dtData IsNot Nothing AndAlso dtData.Rows.Count > 0 Then
                    If Not IsDBNull(dtData.Rows(0)("STR")) Then
                        STR = dtData.Rows(0)("STR")
                    Else
                        STR = ""
                    End If
                End If
                Return STR
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function DeleteSE_BACKGROUND_PORTAL(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstl As List(Of SE_BACKGROUND_PORTAL)
        Try
            lstl = (From p In Context.SE_BACKGROUND_PORTAL Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstl.Count - 1
                Context.SE_BACKGROUND_PORTAL.DeleteObject(lstl(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region
    Public Function GetUserPermisionPortal(ByVal userName As String, ByVal employeeID As Decimal) As List(Of PermissionDTO)
        Try
            Dim objUser = (From p In Context.SE_USER
                           From p1 In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                           Where p.USERNAME.ToLower = userName.ToLower And p1.ID = employeeID).FirstOrDefault

            If objUser IsNot Nothing Then
                Dim userID = objUser.p.ID
                Dim str = "/"
                If objUser.p.USERNAME.ToUpper = "ADMIN" Then
                    Dim lstPermis = (From p In Context.SE_FUNCTION
                                     Where p.FID.IndexOf(str) <> -1
                                     Select New PermissionDTO With {
                                         .FunctionID = p.ID,
                                         .FunctionName = p.FID.Trim
                                         }).ToList
                    Return lstPermis
                Else
                    Dim lstPermis = (From p In Context.SE_FUNCTION
                                     From p1 In Context.SE_USER_PERMISSION.Where(Function(f) f.FUNCTION_ID = p.ID)
                                     Where p1.USER_ID = userID And p.FID.IndexOf(str) <> -1
                                     Select New PermissionDTO With {
                                         .FunctionID = p.ID,
                                         .FunctionName = p.FID.Trim,
                                         .AllowPrint = p1.ALLOW_PRINT,
                                         .AllowExport = p1.ALLOW_EXPORT
                                         }).ToList
                    Return lstPermis
                End If
            Else
                Return Nothing
            End If

        Catch ex As Exception

        End Try
    End Function
    Public Function GetUserPermisionExceptionPortal(ByVal userName As String, ByVal fid As String, ByVal employeeID As Decimal) As List(Of PermissionDTO)
        Try
            Dim functionID = (From p In Context.SE_FUNCTION Where p.FID.ToUpper = fid.ToUpper).FirstOrDefault
            Dim objUser = (From p In Context.SE_USER
                           From p1 In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                           Where p.USERNAME.ToLower = userName.ToLower And p1.ID = employeeID).FirstOrDefault

            If objUser IsNot Nothing Then
                Dim userID = objUser.p.ID
                Dim str = "/"
                Dim lstPermis = (From p In Context.SE_FUNCTION
                                 From p1 In Context.SE_USER_PERMISSION_ORG.Where(Function(f) f.FUNCTION_ID = p.ID And f.USER_ID = userID)
                                 From p2 In Context.SE_USER_PERMISSION_ORG_DETAIL.Where(Function(f) f.FUNCTION_ID = p.ID And f.USER_ID = userID)
                                 From p3 In Context.SE_USER_PERMISSION_ORG_WL.Where(Function(f) f.FUNCTION_ID = p.ID And f.USER_ID = userID)
                                 Where p.ID = functionID.ID
                                 Select New PermissionDTO With {
                                    .FunctionID = p.ID,
                                    .FunctionName = p.FID.Trim,
                                    .OrgID = p2.ORG_ID,
                                    .WorkLevelID = p3.WL_ID
                                    }).ToList
                'Dim lstPermis2 = (From p In Context.SE_USER_ORG_ACCESS.Where(Function(f) f.USER_ID = userID)
                '                  Select New PermissionDTO With {
                '                    .FunctionID = -1,
                '                    .FunctionName = "",
                '                    .OrgID = p.ORG_ID,
                '                    .WorkLevelID = -1
                '                    }).ToList
                'Dim rs = lstPermis.Union(lstPermis2).ToList
                Dim rs = lstPermis.ToList
                Return rs
            Else
                Return Nothing
            End If

        Catch ex As Exception

        End Try
    End Function
    Public Function GetFrameSalaryAll() As List(Of PA_FRAME_SALARYDTO)
        Dim lstOrgs As New List(Of PA_FRAME_SALARYDTO)
        lstOrgs = (From p In Context.PA_FRAME_SALARY
                   From PARENT In Context.PA_FRAME_SALARY.Where(Function(f) f.ID = p.PARENT_ID).DefaultIfEmpty
                   Order By p.NAME_VN
                   Select New PA_FRAME_SALARYDTO With {
                        .ID = p.ID,
                        .CODE = p.CODE,
                        .NAME_VN = p.NAME_VN,
                        .NAME_EN = p.NAME_EN,
                        .PARENT_ID = p.PARENT_ID,
                        .PARENT_NAME = PARENT.NAME_VN,
                        .ACTFLG = p.ACTFLG,
                        .DESCRIPTION_PATH = p.DESCRIPTION_PATH,
                        .COEFFICIENT = p.COEFFICIENT,
                        .IS_LEVEL1 = p.IS_LEVEL1,
                        .IS_LEVEL2 = p.IS_LEVEL2,
                        .IS_LEVEL3 = p.IS_LEVEL3,
                        .PROMOTE_MONTH = p.PROMOTE_MONTH,
                        .NEXT_CODE = p.NEXT_CODE,
                        .REMARK = p.REMARK}).ToList

        Return lstOrgs
    End Function
    Public Function GetFrameProductivityAll() As List(Of PA_FRAME_PRODUCTIVITYDTO)
        Dim lst As New List(Of PA_FRAME_PRODUCTIVITYDTO)
        lst = (From p In Context.PA_FRAME_PRODUCTIVITY
               From PARENT In Context.PA_FRAME_PRODUCTIVITY.Where(Function(f) f.ID = p.PARENT_ID).DefaultIfEmpty
               Order By p.NAME_VN
               Select New PA_FRAME_PRODUCTIVITYDTO With {
                        .ID = p.ID,
                        .CODE = p.CODE,
                        .NAME_VN = p.NAME_VN,
                        .NAME_EN = p.NAME_EN,
                        .PARENT_ID = p.PARENT_ID,
                        .PARENT_NAME = PARENT.NAME_VN,
                        .ACTFLG = p.ACTFLG,
                        .COEFFICIENT = p.COEFFICIENT,
                        .CREATED_BY = p.CREATED_BY,
                        .CREATED_DATE = p.CREATED_DATE,
                        .CREATED_LOG = p.CREATED_LOG,
                        .MODIFIED_BY = p.MODIFIED_BY,
                        .MODIFIED_DATE = p.MODIFIED_DATE,
                        .MODIFIED_LOG = p.MODIFIED_LOG,
                        .DESCRIPTION_PATH = p.DESCRIPTION_PATH,
                        .HIERARCHICAL_PATH = p.HIERARCHICAL_PATH,
                        .IS_LEVEL1 = p.IS_LEVEL1,
                        .IS_LEVEL2 = p.IS_LEVEL2,
                        .IS_LEVEL3 = p.IS_LEVEL3,
                        .REMARK = p.REMARK}).ToList

        Return lst
    End Function
#Region "Folder - file"
    Public Function GetFoldersAll() As List(Of FolderDTO)
        Dim lstFolders As New List(Of FolderDTO)
        lstFolders = (From p In Context.HU_FOLDERS
                      From parent In Context.HU_FOLDERS.Where(Function(f) f.ID = p.PARENT_ID).DefaultIfEmpty
                      Order By p.ID Descending
                      Select New FolderDTO With {
                          .ID = p.ID,
                          .LINK = p.LINK,
                          .FOLDER_NAME = p.FOLDER_NAME,
                          .PARENT_ID = p.PARENT_ID,
                          .PARENT_NAME = parent.FOLDER_NAME}).ToList

        Return lstFolders
    End Function

    Public Function GetFoldersStructureInfo(ByVal _folderId As Decimal) As List(Of FolderDTO)
        Dim query As New FolderDTO
        Dim list As New List(Of FolderDTO)
        query.PARENT_ID = _folderId

        Do While query.PARENT_ID IsNot Nothing
            query = (From p In Context.HU_FOLDERS Where p.ID = query.PARENT_ID
                     Order By p.FOLDER_NAME
                     Select New FolderDTO With {
                     .ID = p.ID,
                     .FOLDER_NAME = p.FOLDER_NAME,
                     .PARENT_ID = p.PARENT_ID}).FirstOrDefault
            list.Add(query)
        Loop
        Return list
    End Function
#End Region

#Region "Dashboard"
    Public Function SendMail_ctrlDashboardHome(ByVal lstDTO As List(Of SEMailDTO)) As Boolean
        Try
            Dim defaultFrom As String = ""
            Dim config As Dictionary(Of String, String)
            config = GetConfig(ModuleID.All)
            defaultFrom = If(config.ContainsKey("MailFrom"), config("MailFrom"), "")

            If defaultFrom = "" Then
                Return False
            End If

            For Each dto In lstDTO
                InsertMail(defaultFrom, dto.MAIL_TO, dto.SUBJECT, dto.CONTENT, dto.MAIL_CC)
            Next

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
#End Region
End Class
