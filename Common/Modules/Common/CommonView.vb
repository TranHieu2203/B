Imports System.Threading
Imports Common.CommonBusiness
Imports Framework.UI
Imports Telerik.Web.UI
Imports WebAppLog

Public Class CommonView
    Inherits ViewBase
    'Content: Write log time and error
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Common\Modules\Common\" + Me.GetType().Name.ToString()
    Dim log As New UserLog

    Protected WithEvents _toolbar As RadToolBar
    ''' <lastupdate>
    ''' 13/07/2017 11:14
    ''' </lastupdate>
    ''' <summary>
    ''' Get maintoolbar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainToolBar As RadToolBar
        Get
            Return _toolbar
        End Get
        Set(ByVal value As RadToolBar)
            _toolbar = value
        End Set
    End Property
    Public _SE_CASE_CONFIG As DataTable
    Public Property SE_CASE_CONFIG As DataTable
        Get
            Return _SE_CASE_CONFIG
        End Get
        Set(ByVal value As DataTable)
            _SE_CASE_CONFIG = value
        End Set
    End Property

    Public Function getSE_CASE_CONFIG(ByVal codecase As String) As Integer
        Try
            Dim rep As New CommonRepository
            Dim count As Integer ' get thong config case theo Me.ID
            count = rep.GetCaseConfigByID(Me.ID, codecase)
            Return count
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    ''' <lastupdate>
    ''' 13/07/2017 11:14
    ''' </lastupdate>
    ''' <summary>
    ''' Check authen
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Function IsAuthenticated() As Boolean
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            'If Utilities.IsAuthenticated AndAlso LogHelper.CurrentUser IsNot Nothing Then
            If LogHelper.CurrentUser IsNot Nothing Then
                Return True
            End If
            Return False
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            ''DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            Return False
        End Try

    End Function
    ''' <lastupdate>
    ''' 13/07/2017 11:14
    ''' </lastupdate>
    ''' <summary>
    ''' Cập nhật trạng thái toolbar
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ChangeToolbarState()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If _toolbar Is Nothing Then Exit Sub
            Dim item As RadToolBarButton
            For i = 0 To _toolbar.Items.Count - 1
                item = CType(_toolbar.Items(i), RadToolBarButton)
                Select Case CurrentState
                    Case CommonMessage.STATE_EDIT, CommonMessage.STATE_NEW, "Copy"
                        If item.CommandName = CommonMessage.TOOLBARITEM_SAVE Or item.CommandName = CommonMessage.TOOLBARITEM_CANCEL Then
                            item.Enabled = True
                        Else
                            item.Enabled = False
                        End If
                    Case Else
                        If item.CommandName = CommonMessage.TOOLBARITEM_SAVE Or item.CommandName = CommonMessage.TOOLBARITEM_CANCEL Then
                            item.Enabled = False
                        ElseIf item.CommandName = "SEND_TRAINING_TO_PROFILE_DISABLE" Then
                            item.Enabled = False
                        Else
                            item.Enabled = True
                        End If
                End Select
            Next
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            ''DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <lastupdate>
    ''' 13/07/2017 11:14
    ''' </lastupdate>
    ''' <summary>
    ''' Get quyen hien thi toolbar của user
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ToolbarAuthorization()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If MainToolBar IsNot Nothing Then
                For Each item As RadToolBarItem In MainToolBar.Items
                    Dim bAllow As Boolean = False

                    If item.Attributes("Authorize") IsNot Nothing AndAlso item.Attributes("Authorize").Trim <> "" Then
                        Select Case item.Attributes("Authorize")
                            Case CommonMessage.AUTHORIZE_CREATE
                                bAllow = Me.AllowCreate
                            Case CommonMessage.AUTHORIZE_MODIFY
                                bAllow = Me.AllowModify
                            Case CommonMessage.AUTHORIZE_DELETE
                                bAllow = Me.AllowDelete
                            Case CommonMessage.AUTHORIZE_PRINT
                                bAllow = Me.AllowPrint
                            Case CommonMessage.AUTHORIZE_IMPORT
                                bAllow = Me.AllowImport
                            Case CommonMessage.AUTHORIZE_EXPORT
                                bAllow = Me.AllowExport
                            Case CommonMessage.AUTHORIZE_SPECIAL1
                                bAllow = Me.AllowSpecial1
                            Case CommonMessage.AUTHORIZE_SPECIAL2
                                bAllow = Me.AllowSpecial2
                            Case CommonMessage.AUTHORIZE_SPECIAL3
                                bAllow = Me.AllowSpecial3
                            Case CommonMessage.AUTHORIZE_SPECIAL4
                                bAllow = Me.AllowSpecial4
                            Case CommonMessage.AUTHORIZE_SPECIAL5
                                bAllow = Me.AllowSpecial5
                            Case CommonMessage.AUTHORIZE_RESET
                                bAllow = Me.AllowReset
                        End Select
                    Else
                        bAllow = True
                    End If
                    item.Visible = bAllow
                Next
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            ''DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub
    ''' <lastupdate>
    ''' 13/07/2017 11:14
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien click on toolbar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub OnMainToolbar_Click(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles _toolbar.ButtonClick
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            LogHelper.ViewName = Me.ViewName
            LogHelper.ViewDescription = Me.ViewDescription
            LogHelper.ViewGroup = Me.ViewGroup
            LogHelper.ActionName = CType(e.Item, RadToolBarButton).CommandName
            RaiseEvent OnMainToolbarClick(sender, e)
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Public Delegate Sub ToolBarClickDelegate(ByVal sender As Object, ByVal e As RadToolBarEventArgs)
    Public Event OnMainToolbarClick As ToolBarClickDelegate
    ''' <lastupdate>
    ''' 13/07/2017 11:14
    ''' </lastupdate>
    ''' <summary>
    ''' Check quyền của user
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub CheckAuthorization()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim rep As New CommonRepository
            Dim user = LogHelper.CurrentUser
            Dim strApp As String = LogHelper.GetSessionCurrentApp(Session.SessionID)
            If strApp = "Main" Then
                Dim strStatus As String = LogHelper.GetSessionStatus(Session.SessionID)
                If strStatus = "KILLED" Then
                    If LogHelper.OnlineUsers.ContainsKey(Session.SessionID) Then
                        LogHelper.SaveAccessLog(Session.SessionID, "Killed")
                        LogHelper.OnlineUsers.Remove(Session.SessionID)
                        Session.Abandon()
                        FormsAuthentication.SignOut()
                        Response.Redirect("/SessionKilled.aspx")
                    End If
                End If
            End If

            If Me.MustAuthorize Then
                'If Utilities.IsAuthenticated Then
                If LogHelper.CurrentUser IsNot Nothing Then
                    Dim GroupAdmin As Boolean = rep.CheckGroupAdmin(Common.GetUsername)
                    If GroupAdmin = False Then
                        Dim permissions As List(Of PermissionDTO) = rep.GetUserPermissions(Common.GetUsername)
                        If permissions IsNot Nothing Then
                            Dim ViewPermissions As List(Of PermissionDTO)
                            ViewPermissions = (From p In permissions Where p.FID = Me.ViewName And p.IS_REPORT = False).ToList
                            If ViewPermissions IsNot Nothing Then
                                For Each item In ViewPermissions
                                    Me.Allow = True
                                    Me.AllowCreate = Me.AllowCreate Or item.AllowCreate
                                    Me.AllowModify = Me.AllowModify Or item.AllowModify
                                    Me.AllowDelete = Me.AllowDelete Or item.AllowDelete
                                    Me.AllowPrint = Me.AllowPrint Or item.AllowPrint
                                    Me.AllowImport = Me.AllowImport Or item.AllowImport
                                    Me.AllowExport = Me.AllowExport Or item.AllowExport
                                    Me.AllowSpecial1 = Me.AllowSpecial1 Or item.AllowSpecial1
                                    Me.AllowSpecial2 = Me.AllowSpecial2 Or item.AllowSpecial2
                                    Me.AllowSpecial3 = Me.AllowSpecial3 Or item.AllowSpecial3
                                    Me.AllowSpecial4 = Me.AllowSpecial4 Or item.AllowSpecial4
                                    Me.AllowSpecial5 = Me.AllowSpecial5 Or item.AllowSpecial5
                                    Me.AllowReset = Me.AllowReset
                                Next
                            End If
                        End If
                    Else
                        If user.MODULE_ADMIN = "*" OrElse (user.MODULE_ADMIN IsNot Nothing AndAlso user.MODULE_ADMIN.Contains(Me.ModuleName)) Then
                            Me.Allow = True
                            Me.AllowCreate = True
                            Me.AllowModify = True
                            Me.AllowDelete = True
                            Me.AllowPrint = True
                            Me.AllowImport = True
                            Me.AllowExport = True
                            Me.AllowSpecial1 = True
                            Me.AllowSpecial2 = True
                            Me.AllowSpecial3 = True
                            Me.AllowSpecial4 = True
                            Me.AllowSpecial5 = True
                            Me.AllowReset = True
                        End If
                    End If
                End If
            Else
                Me.Allow = True
                Me.AllowCreate = True
                Me.AllowModify = True
                Me.AllowDelete = True
                Me.AllowPrint = True
                Me.AllowImport = True
                Me.AllowExport = True
                Me.AllowSpecial1 = True
                Me.AllowSpecial2 = True
                Me.AllowSpecial3 = True
                Me.AllowSpecial4 = True
                Me.AllowSpecial5 = True
                Me.AllowReset = True
            End If
            ToolbarAuthorization()
            If Me.Allow Then
                Dim func = rep.GetFunction(Me.ViewName)
                If func IsNot Nothing Then
                    Me.ViewDescription = Translate(func.NAME)
                    Me.ViewGroup = func.FUNCTION_GROUP_NAME
                End If

                If Me.EnableLogAccess Then
                    LogHelper.UpdateAccessLog(Me)
                End If

            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            ''DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub
    ''' <lastupdate>
    ''' 13/07/2017 11:14
    ''' </lastupdate>
    ''' <summary>
    ''' Hiển thị exception
    ''' </summary>
    ''' <param name="ViewName"></param>
    ''' <param name="ID"></param>
    ''' <param name="ex"></param>
    ''' <param name="ExtraInfo"></param>
    ''' <remarks></remarks>
    Public Overrides Sub DisplayException(ByVal ViewName As String, ByVal ID As String, ByVal ex As System.Exception, Optional ByVal ExtraInfo As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Common.DisplayException(Me, ex, "ViewName: " & ViewName & " ViewID:" & ID)

        Catch e As Exception
            ''DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, e, "")
        End Try

    End Sub
    Protected Overrides Sub FrameworkInitialize()
        Thread.CurrentThread.CurrentCulture = Common.SystemLanguage
        Thread.CurrentThread.CurrentUICulture = Common.SystemLanguage
        MyBase.FrameworkInitialize()
    End Sub

#Region "View config"
    Dim vcf As DataSet
    Public Sub ViewConfig(ByVal rp As RadPane)
        'If LogHelper.CurrentUser.USERNAME.ToUpper = "ADMIN" Then Return
        vcf = New DataSet
        Using rep = New CommonRepository
            vcf.ReadXml(New IO.StringReader(rep.GetConfigView(Me.ID).Rows(0)("config_data").ToString()))
        End Using
        Dim star As String = "*"
        Try
            If vcf IsNot Nothing AndAlso vcf.Tables("control") IsNot Nothing Then
                Dim dtCtrl As DataTable = vcf.Tables("control")
                For Each ctrs As Control In rp.Controls
                    Dim row As DataRow
                    Try
                        row = dtCtrl.Select("Ctl_ID ='" + ctrs.ID + "'")(0)
                    Catch ex As Exception
                        Continue For
                    End Try
                    If row IsNot Nothing Then
                        ctrs.Visible = If(IsDBNull(row("Is_Visible")), False, CBool(row("Is_Visible")))
                        Try
                            Dim validator As BaseValidator = rp.FindControl(row.Field(Of String)("Validator_ID"))
                            Dim labelCtr As Label = rp.FindControl(row.Field(Of String)("Label_ID").Trim())
                            If labelCtr IsNot Nothing Then
                                labelCtr.Visible = ctrs.Visible
                                labelCtr.Text = If(IsDBNull(row("Label_text")), labelCtr.Text, Translate(row("Label_text")))
                            End If
                            If validator IsNot Nothing Then
                                validator.Enabled = If(IsDBNull(row("Is_Validator")), True, CBool(row("Is_Validator")))
                                validator.ErrorMessage = If(IsDBNull(row("ErrorMessage")), validator.ErrorMessage, row("ErrorMessage"))
                                validator.ToolTip = If(IsDBNull(row("ErrorToolTip")), validator.ToolTip, row("ErrorToolTip"))
                                If validator.Enabled Then
                                    labelCtr.Text = If(IsDBNull(row("Label_text")), labelCtr.Text, Translate(row("Label_text"))) + String.Format("<span style='color:red'> {0}</span>", star)
                                Else
                                    labelCtr.Text = If(IsDBNull(row("Label_text")), labelCtr.Text, Translate(row("Label_text")))
                                End If
                            End If
                        Catch ex As Exception
                            Continue For
                        End Try
                    End If
                Next
            End If
        Catch ex As Exception
        End Try
    End Sub
    Public Sub GirdConfig(ByVal rg As RadGrid)
        rg.MasterTableView.Columns.Clear()
        'If CacheManager.GetValue("CONFIG" + Me.ID) IsNot Nothing Then

        '    For Each col In CacheManager.GetValue("CONFIG" + Me.ID)
        '        rg.MasterTableView.Columns.Add(col)
        '    Next
        '    Return
        'End If
        log = LogHelper.GetUserLog
        vcf = New DataSet
        Using rep = New CommonRepository
            vcf.ReadXml(New IO.StringReader(rep.GetConfigView(Me.ID).Rows(0)("config_data").ToString()))
        End Using
        'If LogHelper.CurrentUser.USERNAME.ToUpper = "ADMIN" Then Return
        Dim dtGrid As DataTable = vcf.Tables("girdColumm")
        If dtGrid.Rows.Count = 0 AndAlso dtGrid Is Nothing Then Exit Sub
        Dim view As DataView = New DataView(dtGrid)
        view.Sort = "Orderby"
        dtGrid = view.ToTable()

        Dim rColBut As GridButtonColumn
        Dim rColDownloadFamily As GridButtonColumn
        Dim rColViewFamily As GridButtonColumn
        Dim rColDownloadNPT As GridButtonColumn
        Dim rColViewNPT As GridButtonColumn
        Dim rColDownload As GridButtonColumn
        Dim rColView As GridButtonColumn
        Dim rColMap As GridButtonColumn
        Dim rCol As GridBoundColumn
        Dim rColDate As GridDateTimeColumn
        Dim rColClientSelect As GridClientSelectColumn
        Dim rColCheck As GridCheckBoxColumn
        Dim rColdImage As GridBinaryImageColumn


        For Each row As DataRow In dtGrid.Select("Is_Visible=True")

            If Me.ID = "ctrlHU_ChangeInfoMng" AndAlso CommonConfig.APP_SETTING_15() Then
                If row.Field(Of String)("ID").Trim() = "OBJECT_LABORNAME" Then
                    Continue For
                End If
            ElseIf Me.ID = "ctrlHU_Terminate" AndAlso log.Username.ToUpper <> "ADMIN" Then
                If row.Field(Of String)("ID").Trim() = "NOTIFY_NO" Then
                    Continue For
                End If
            ElseIf (Me.ID = "ctrlHU_WageMng" Or Me.ID = "ctrlHU_EmpDtlSalary" Or Me.ID = "ctrlPortalSalary") AndAlso CommonConfig.APP_SETTING_18() Then
                If row.Field(Of String)("ID").Trim() = "SAL_GROUP_NAME" Or row.Field(Of String)("ID").Trim() = "SAL_LEVEL_NAME" Or row.Field(Of String)("ID").Trim() = "SAL_RANK_NAME" Then
                    Continue For
                End If
            End If
            ' bổ sung xử lý HSV-506 and HSV-507
            Select Case Me.ID
                Case "ctrlHU_EmployeeMng"

                    If CommonConfig.APP_SETTING_20() Then
                        If row.Field(Of String)("ID").Trim() = "ORG_NAME2" Then
                            Continue For
                        End If
                    End If
                    If CommonConfig.APP_SETTING_21() Then
                        If row.Field(Of String)("ID").Trim() = "ORG_NAME3" Then
                            Continue For
                        End If
                    End If
                    If CommonConfig.APP_SETTING_22() Then
                        If row.Field(Of String)("ID").Trim() = "ORG_NAME4" Then
                            Continue For
                        End If
                    End If
                    If CommonConfig.APP_SETTING_23() Then
                        If row.Field(Of String)("ID").Trim() = "ORG_NAME5" Then
                            Continue For
                        End If
                    End If
                    If CommonConfig.APP_SETTING_24() Then
                        If row.Field(Of String)("ID").Trim() = "ORG_NAME6" Then
                            Continue For
                        End If
                    End If
                    If CommonConfig.APP_SETTING_25() Then
                        If row.Field(Of String)("ID").Trim() = "ORG_NAME7" Then
                            Continue For
                        End If
                    End If
                    If CommonConfig.APP_SETTING_15() Then
                        If row.Field(Of String)("ID").Trim() = "OBJECT_LABOR_NAME" Then
                            Continue For
                        End If
                    End If
                    If CommonConfig.APP_ISHIDE_IMAGE() Then
                        If row.Field(Of String)("ID").Trim() = "IMAGE_BINARY" Then
                            Continue For
                        End If
                    End If

                Case "ctrlHU_WageMng"
                    If CommonConfig.APP_SETTING_7() Then
                        If row.Field(Of String)("ID").Trim() = "SIGN_NAME" Or row.Field(Of String)("ID").Trim() = "SIGN_TITLE" Or row.Field(Of String)("ID").Trim() = "SIGN_DATE" Then
                            Continue For
                        End If
                    End If

                Case "ctrlHU_Contract"
                    If CommonConfig.APP_SETTING_20() Then
                        If row.Field(Of String)("ID").Trim() = "ORG_NAME2" Then
                            Continue For
                        End If
                    End If
                    If CommonConfig.APP_SETTING_21() Then
                        If row.Field(Of String)("ID").Trim() = "ORG_NAME3" Then
                            Continue For
                        End If
                    End If
                    If CommonConfig.APP_SETTING_22() Then
                        If row.Field(Of String)("ID").Trim() = "ORG_NAME4" Then
                            Continue For
                        End If
                    End If
                    If CommonConfig.APP_SETTING_23() Then
                        If row.Field(Of String)("ID").Trim() = "ORG_NAME5" Then
                            Continue For
                        End If
                    End If
                    If CommonConfig.APP_SETTING_24() Then
                        If row.Field(Of String)("ID").Trim() = "ORG_NAME6" Then
                            Continue For
                        End If
                    End If
                    If CommonConfig.APP_SETTING_25() Then
                        If row.Field(Of String)("ID").Trim() = "ORG_NAME7" Then
                            Continue For
                        End If
                    End If
                    If CommonConfig.APP_SETTING_8() Then
                        If row.Field(Of String)("ID").Trim() = "SIGNER_NAME" Or row.Field(Of String)("ID").Trim() = "SIGNER_TITLE" Or row.Field(Of String)("ID").Trim() = "SIGN_DATE" Then
                            Continue For
                        End If
                    End If
                Case "ctrlRC_Contract"
                    If CommonConfig.APP_SETTING_20() Then
                        If row.Field(Of String)("ID").Trim() = "ORG_NAME2" Then
                            Continue For
                        End If
                    End If
                    If CommonConfig.APP_SETTING_21() Then
                        If row.Field(Of String)("ID").Trim() = "ORG_NAME3" Then
                            Continue For
                        End If
                    End If
                    If CommonConfig.APP_SETTING_22() Then
                        If row.Field(Of String)("ID").Trim() = "ORG_NAME4" Then
                            Continue For
                        End If
                    End If
                    If CommonConfig.APP_SETTING_23() Then
                        If row.Field(Of String)("ID").Trim() = "ORG_NAME5" Then
                            Continue For
                        End If
                    End If
                    If CommonConfig.APP_SETTING_24() Then
                        If row.Field(Of String)("ID").Trim() = "ORG_NAME6" Then
                            Continue For
                        End If
                    End If
                    If CommonConfig.APP_SETTING_25() Then
                        If row.Field(Of String)("ID").Trim() = "ORG_NAME7" Then
                            Continue For
                        End If
                    End If
                    If CommonConfig.APP_SETTING_8() Then
                        If row.Field(Of String)("ID").Trim() = "SIGNER_NAME" Or row.Field(Of String)("ID").Trim() = "SIGNER_TITLE" Or row.Field(Of String)("ID").Trim() = "SIGN_DATE" Then
                            Continue For
                        End If
                    End If

                Case "ctrlHU_ContractAppendix"
                    If CommonConfig.APP_SETTING_20() Then
                        If row.Field(Of String)("ID").Trim() = "ORG_NAME2" Then
                            Continue For
                        End If
                    End If
                    If CommonConfig.APP_SETTING_21() Then
                        If row.Field(Of String)("ID").Trim() = "ORG_NAME3" Then
                            Continue For
                        End If
                    End If
                    If CommonConfig.APP_SETTING_22() Then
                        If row.Field(Of String)("ID").Trim() = "ORG_NAME4" Then
                            Continue For
                        End If
                    End If
                    If CommonConfig.APP_SETTING_23() Then
                        If row.Field(Of String)("ID").Trim() = "ORG_NAME5" Then
                            Continue For
                        End If
                    End If
                    If CommonConfig.APP_SETTING_24() Then
                        If row.Field(Of String)("ID").Trim() = "ORG_NAME6" Then
                            Continue For
                        End If
                    End If
                    If CommonConfig.APP_SETTING_25() Then
                        If row.Field(Of String)("ID").Trim() = "ORG_NAME7" Then
                            Continue For
                        End If
                    End If
                    If CommonConfig.APP_SETTING_9() Then
                        If row.Field(Of String)("ID").Trim() = "SIGNER_NAME" Or row.Field(Of String)("ID").Trim() = "SIGNER_TITLE" Or row.Field(Of String)("ID").Trim() = "SIGN_DATE" Then
                            Continue For
                        End If
                    End If

                Case "ctrlHU_ChangeInfoMng"
                    If CommonConfig.APP_SETTING_20() Then
                        If row.Field(Of String)("ID").Trim() = "ORG_NAME2" Then
                            Continue For
                        End If
                    End If
                    If CommonConfig.APP_SETTING_21() Then
                        If row.Field(Of String)("ID").Trim() = "ORG_NAME3" Then
                            Continue For
                        End If
                    End If
                    If CommonConfig.APP_SETTING_22() Then
                        If row.Field(Of String)("ID").Trim() = "ORG_NAME4" Then
                            Continue For
                        End If
                    End If
                    If CommonConfig.APP_SETTING_23() Then
                        If row.Field(Of String)("ID").Trim() = "ORG_NAME5" Then
                            Continue For
                        End If
                    End If
                    If CommonConfig.APP_SETTING_24() Then
                        If row.Field(Of String)("ID").Trim() = "ORG_NAME6" Then
                            Continue For
                        End If
                    End If
                    If CommonConfig.APP_SETTING_25() Then
                        If row.Field(Of String)("ID").Trim() = "ORG_NAME7" Then
                            Continue For
                        End If
                    End If
                    If CommonConfig.APP_SETTING_10() Then
                        If row.Field(Of String)("ID").Trim() = "SIGN_NAME" Or row.Field(Of String)("ID").Trim() = "SIGN_TITLE" Or row.Field(Of String)("ID").Trim() = "SIGN_DATE" Then
                            Continue For
                        End If
                    End If

                Case "ctrlHU_Terminate"
                    If CommonConfig.APP_SETTING_20() Then
                        If row.Field(Of String)("ID").Trim() = "ORG_NAME2" Then
                            Continue For
                        End If
                    End If
                    If CommonConfig.APP_SETTING_21() Then
                        If row.Field(Of String)("ID").Trim() = "ORG_NAME3" Then
                            Continue For
                        End If
                    End If
                    If CommonConfig.APP_SETTING_22() Then
                        If row.Field(Of String)("ID").Trim() = "ORG_NAME4" Then
                            Continue For
                        End If
                    End If
                    If CommonConfig.APP_SETTING_23() Then
                        If row.Field(Of String)("ID").Trim() = "ORG_NAME5" Then
                            Continue For
                        End If
                    End If
                    If CommonConfig.APP_SETTING_24() Then
                        If row.Field(Of String)("ID").Trim() = "ORG_NAME6" Then
                            Continue For
                        End If
                    End If
                    If CommonConfig.APP_SETTING_25() Then
                        If row.Field(Of String)("ID").Trim() = "ORG_NAME7" Then
                            Continue For
                        End If
                    End If
                    If CommonConfig.APP_SETTING_14() Then
                        If row.Field(Of String)("ID").Trim() = "SIGN_NAME" Or row.Field(Of String)("ID").Trim() = "SIGN_TITLE" Or row.Field(Of String)("ID").Trim() = "SIGN_DATE" Then
                            Continue For
                        End If
                    End If

            End Select
            Try
                Dim v = row.Field(Of String)("DataType").Trim().ToUpper
                If row.Field(Of String)("ID").Trim() = "cbStatus" Then
                    rColClientSelect = New GridClientSelectColumn()
                    rg.MasterTableView.Columns.Add(rColClientSelect)
                    rColClientSelect.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    rColClientSelect.HeaderStyle.Width = 30
                    rColClientSelect.HeaderTooltip = (row.Field(Of String)("Name").Trim())
                    rColClientSelect.ItemStyle.HorizontalAlign = HorizontalAlign.Center
                ElseIf row.Field(Of String)("DataType").Trim().ToUpper = "Boolean".ToUpper Then
                    rColCheck = New GridCheckBoxColumn()
                    rg.MasterTableView.Columns.Add(rColCheck)
                    rColCheck.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    rColCheck.DataField = row.Field(Of String)("ID").Trim()
                    If IsNumeric(row("Width")) Then
                        rColCheck.HeaderStyle.Width = Integer.Parse(row("Width").ToString())
                    End If
                    rColCheck.HeaderText = Translate(row.Field(Of String)("Name").Trim())
                    rColCheck.HeaderTooltip = (row.Field(Of String)("Name").Trim())
                    rColCheck.ItemStyle.HorizontalAlign = HorizontalAlign.Center
                    rColCheck.Visible = Boolean.Parse(row.Item("Is_Visible"))
                    rColCheck.AllowFiltering = False
                ElseIf row.Field(Of String)("DataType").Trim().ToUpper = "IMAGE".ToUpper Then
                    rColdImage = New GridBinaryImageColumn
                    rg.MasterTableView.Columns.Add(rColdImage)
                    rColdImage.DataField = row.Field(Of String)("ID").Trim()
                    rColdImage.HeaderText = Translate(row.Field(Of String)("Name").Trim())
                    rColdImage.Visible = Boolean.Parse(row.Item("Is_Visible"))
                    rColdImage.HeaderStyle.Width = Integer.Parse(row("Width").ToString())
                    rColdImage.DataAlternateTextField = row.Field(Of String)("ID").Trim()
                    rColdImage.ImageHeight = 80
                    rColdImage.ImageWidth = 80
                    rColdImage.ResizeMode = 3
                    rColdImage.DataAlternateTextFormatString = "Image of {0}"
                    rColdImage.AllowFiltering = False
                    rColdImage.AllowSorting = False
                ElseIf row.Field(Of String)("DataType").Trim().ToUpper.Contains("Short-DateTime".ToUpper) = True Then
                    rCol = New GridBoundColumn()
                    rg.MasterTableView.Columns.Add(rCol)
                    rCol.DataField = row.Field(Of String)("ID").Trim()
                    rCol.HeaderText = Translate(row.Field(Of String)("Name").Trim())
                    If IsNumeric(row("Width").ToString()) Then
                        rCol.HeaderStyle.Width = Integer.Parse(row("Width").ToString())
                        rCol.FilterControlWidth = Integer.Parse(row("Width").ToString())
                    End If
                    rCol.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    rCol.AllowFiltering = True
                    rCol.AllowSorting = True
                    rCol.AutoPostBackOnFilter = True
                    rCol.CurrentFilterFunction = GridKnownFunction.Contains
                    rCol.ShowFilterIcon = False
                    rCol.HeaderTooltip = (row.Field(Of String)("Name").Trim())
                    rCol.FilterControlToolTip = (row.Field(Of String)("Name").Trim())
                    rCol.Visible = Boolean.Parse(row.Item("Is_Visible"))
                    rCol.DataFormatString = ConfigurationManager.AppSettings("FDATEGRID")
                    Dim StrFormat As String = String.Empty
                    If row.Field(Of String)("DataType").Trim().Split("#").Count > 1 AndAlso row.Field(Of String)("DataType").Trim().Split("#")(1) IsNot Nothing Then
                        StrFormat = row.Field(Of String)("DataType").Trim().Split("#")(1).ToString
                        rCol.DataFormatString = StrFormat
                    End If
                ElseIf row.Field(Of String)("DataType").Trim().ToUpper.Contains("Short-Numeric".ToUpper) = True Then
                    rCol = New GridBoundColumn()
                    rg.MasterTableView.Columns.Add(rCol)
                    rCol.DataField = row.Field(Of String)("ID").Trim()
                    rCol.HeaderText = Translate(row.Field(Of String)("Name").Trim())
                    If IsNumeric(row("Width").ToString()) Then
                        rCol.HeaderStyle.Width = Integer.Parse(row("Width").ToString())
                        rCol.FilterControlWidth = Integer.Parse(row("Width").ToString())
                    End If
                    rCol.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    rCol.AllowFiltering = True
                    rCol.AllowSorting = True
                    rCol.AutoPostBackOnFilter = True
                    rCol.CurrentFilterFunction = GridKnownFunction.Contains
                    rCol.ShowFilterIcon = False
                    rCol.HeaderTooltip = (row.Field(Of String)("Name").Trim())
                    rCol.FilterControlToolTip = (row.Field(Of String)("Name").Trim())
                    rCol.Visible = Boolean.Parse(row.Item("Is_Visible"))
                    rCol.DataFormatString = ConfigurationManager.AppSettings("FDATEGRID")
                    Dim StrFormat As String = String.Empty
                    If row.Field(Of String)("DataType").Trim().Split("#").Count > 1 AndAlso row.Field(Of String)("DataType").Trim().Split("#")(1) IsNot Nothing Then
                        StrFormat = row.Field(Of String)("DataType").Trim().Split("#")(1).ToString
                        rCol.DataFormatString = StrFormat
                    End If

                ElseIf row.Field(Of String)("DataType") = "X" Then
                    rCol = New GridBoundColumn()
                    rg.MasterTableView.Columns.Add(rCol)
                    rCol.DataField = row.Field(Of String)("ID").Trim()
                    rCol.HeaderText = Translate(row.Field(Of String)("Name").Trim())
                    If IsNumeric(row("Width").ToString()) Then
                        rCol.HeaderStyle.Width = Integer.Parse(row("Width").ToString())
                        rCol.FilterControlWidth = Integer.Parse(row("Width").ToString())
                    End If
                    rCol.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    rCol.AllowFiltering = True
                    rCol.AllowSorting = True
                    rCol.AutoPostBackOnFilter = True
                    rCol.CurrentFilterFunction = GridKnownFunction.Contains
                    rCol.ShowFilterIcon = False
                    rCol.HeaderTooltip = (row.Field(Of String)("Name").Trim())
                    rCol.FilterControlToolTip = (row.Field(Of String)("Name").Trim())
                    rCol.Visible = Boolean.Parse(row.Item("Is_Visible"))
                    rCol.ItemStyle.HorizontalAlign = HorizontalAlign.Center
                ElseIf row.Field(Of String)("ID").Trim().Contains("FROM_MONTH") Or row.Field(Of String)("ID").Trim().Contains("TO_MONTH") Then
                    rCol = New GridBoundColumn()
                    rg.MasterTableView.Columns.Add(rCol)
                    rCol.DataField = row.Field(Of String)("ID").Trim()
                    rCol.HeaderText = Translate(row.Field(Of String)("Name").Trim())
                    If IsNumeric(row("Width").ToString()) Then
                        rCol.HeaderStyle.Width = Integer.Parse(row("Width").ToString())
                        rCol.FilterControlWidth = Integer.Parse(row("Width").ToString())
                    End If
                    rCol.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    rCol.AllowFiltering = True
                    rCol.AllowSorting = True
                    rCol.AutoPostBackOnFilter = True
                    rCol.CurrentFilterFunction = GridKnownFunction.Contains
                    rCol.ShowFilterIcon = False
                    rCol.HeaderTooltip = (row.Field(Of String)("Name").Trim())
                    rCol.FilterControlToolTip = (row.Field(Of String)("Name").Trim())
                    rCol.Visible = Boolean.Parse(row.Item("Is_Visible"))
                    If row.Field(Of String)("DataType").Trim().ToUpper = "DateTime".ToUpper Then
                        rCol.DataFormatString = ConfigurationManager.AppSettings("FDATE_MONTH_YEAR_GRID")
                    End If
                ElseIf row.Field(Of String)("DataType").Trim().ToUpper = "MonthYear".ToUpper Then
                    rCol = New GridBoundColumn()
                    rg.MasterTableView.Columns.Add(rCol)
                    rCol.DataField = row.Field(Of String)("ID").Trim()
                    rCol.HeaderText = Translate(row.Field(Of String)("Name").Trim())
                    If IsNumeric(row("Width").ToString()) Then
                        rCol.HeaderStyle.Width = Integer.Parse(row("Width").ToString())
                        rCol.FilterControlWidth = Integer.Parse(row("Width").ToString())
                    End If
                    rCol.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    rCol.AllowFiltering = True
                    rCol.AllowSorting = True
                    rCol.AutoPostBackOnFilter = True
                    rCol.CurrentFilterFunction = GridKnownFunction.Contains
                    rCol.ShowFilterIcon = False
                    rCol.HeaderTooltip = (row.Field(Of String)("Name").Trim())
                    rCol.FilterControlToolTip = (row.Field(Of String)("Name").Trim())
                    rCol.Visible = Boolean.Parse(row.Item("Is_Visible"))
                    rCol.DataFormatString = ConfigurationManager.AppSettings("FDATE_MONTH_YEAR_GRID")
                ElseIf row.Field(Of String)("DataType").Trim().ToUpper = "Time".ToUpper Then
                    rCol = New GridBoundColumn()
                    rg.MasterTableView.Columns.Add(rCol)
                    rCol.DataField = row.Field(Of String)("ID").Trim()
                    rCol.HeaderText = Translate(row.Field(Of String)("Name").Trim())
                    If IsNumeric(row("Width").ToString()) Then
                        rCol.HeaderStyle.Width = Integer.Parse(row("Width").ToString())
                        rCol.FilterControlWidth = Integer.Parse(row("Width").ToString())
                    End If
                    rCol.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    rCol.AllowFiltering = True
                    rCol.AllowSorting = True
                    rCol.AutoPostBackOnFilter = True
                    rCol.CurrentFilterFunction = GridKnownFunction.Contains
                    rCol.ShowFilterIcon = False
                    rCol.HeaderTooltip = (row.Field(Of String)("Name").Trim())
                    rCol.FilterControlToolTip = (row.Field(Of String)("Name").Trim())
                    rCol.Visible = Boolean.Parse(row.Item("Is_Visible"))
                    If row.Field(Of String)("DataType").Trim().ToUpper = "Time".ToUpper Then
                        rCol.DataFormatString = ConfigurationManager.AppSettings("TIME")
                    End If
                ElseIf row.Field(Of String)("DataType").Trim().ToUpper = "Numeric".ToUpper Then
                    rCol = New GridNumericColumn()
                    rg.MasterTableView.Columns.Add(rCol)
                    rCol.DataField = row.Field(Of String)("ID").Trim()
                    rCol.HeaderText = Translate(row.Field(Of String)("Name").Trim())
                    If IsNumeric(row("Width").ToString()) Then
                        rCol.HeaderStyle.Width = Integer.Parse(row("Width").ToString())
                        rCol.FilterControlWidth = Integer.Parse(row("Width").ToString())
                    End If
                    rCol.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    rCol.AllowFiltering = True
                    rCol.AllowSorting = True
                    rCol.AutoPostBackOnFilter = True
                    rCol.CurrentFilterFunction = GridKnownFunction.EqualTo
                    rCol.ShowFilterIcon = False
                    rCol.HeaderTooltip = (row.Field(Of String)("Name").Trim())
                    rCol.FilterControlToolTip = (row.Field(Of String)("Name").Trim())
                    rCol.Visible = Boolean.Parse(row.Item("Is_Visible"))
                    If row.Field(Of String)("DataType").Trim().ToUpper = "DateTime".ToUpper Then
                        rCol.DataFormatString = ConfigurationManager.AppSettings("FDATEGRID")
                    ElseIf row.Field(Of String)("DataType").Trim() = "Numeric" Then
                        rCol.ItemStyle.HorizontalAlign = HorizontalAlign.Right
                        rCol.DataFormatString = "{0:#,##0.##}"
                    End If
                ElseIf row.Field(Of String)("DataType").Trim().ToUpper = "NumericRate".ToUpper Then
                    rCol = New GridNumericColumn()
                    rg.MasterTableView.Columns.Add(rCol)
                    rCol.DataField = row.Field(Of String)("ID").Trim()
                    rCol.HeaderText = Translate(row.Field(Of String)("Name").Trim())
                    If IsNumeric(row("Width").ToString()) Then
                        rCol.HeaderStyle.Width = Integer.Parse(row("Width").ToString())
                        rCol.FilterControlWidth = Integer.Parse(row("Width").ToString())
                    End If
                    rCol.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    rCol.AllowFiltering = True
                    rCol.AllowSorting = True
                    rCol.AutoPostBackOnFilter = True
                    rCol.CurrentFilterFunction = GridKnownFunction.EqualTo
                    rCol.ShowFilterIcon = False
                    rCol.HeaderTooltip = (row.Field(Of String)("Name").Trim())
                    rCol.FilterControlToolTip = (row.Field(Of String)("Name").Trim())
                    rCol.Visible = Boolean.Parse(row.Item("Is_Visible"))
                    rCol.ItemStyle.HorizontalAlign = HorizontalAlign.Right
                    rCol.DataFormatString = "{0:#,##0.##}%"
                ElseIf row.Field(Of String)("DataType").Trim().ToUpper = "DateTime".ToUpper Then
                    rCol = New GridBoundColumn()
                    rg.MasterTableView.Columns.Add(rCol)
                    rCol.DataField = row.Field(Of String)("ID").Trim()
                    rCol.HeaderText = Translate(row.Field(Of String)("Name").Trim())
                    If IsNumeric(row("Width").ToString()) Then
                        rCol.HeaderStyle.Width = Integer.Parse(row("Width").ToString())
                        rCol.FilterControlWidth = Integer.Parse(row("Width").ToString())
                    End If
                    rCol.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    rCol.AllowFiltering = True
                    rCol.AllowSorting = True
                    rCol.AutoPostBackOnFilter = True
                    rCol.CurrentFilterFunction = GridKnownFunction.EqualTo
                    rCol.ShowFilterIcon = False
                    rCol.HeaderStyle.VerticalAlign = VerticalAlign.Middle
                    rCol.HeaderTooltip = (row.Field(Of String)("Name").Trim())
                    rCol.FilterControlToolTip = (row.Field(Of String)("Name").Trim())
                    rCol.Visible = Boolean.Parse(row.Item("Is_Visible"))
                    rCol.DataFormatString = ConfigurationManager.AppSettings("FDATEGRID")
                ElseIf row.Field(Of String)("DataType").Trim().ToUpper = "Button".ToUpper Then
                    rColBut = New GridButtonColumn()
                    rg.MasterTableView.Columns.Add(rColBut)
                    'rColBut.DataField = row.Field(Of String)("ID").Trim()
                    rColBut.HeaderText = Translate(row.Field(Of String)("Name").Trim())
                    If IsNumeric(row("Width").ToString()) Then
                        rColBut.HeaderStyle.Width = Integer.Parse(row("Width").ToString())
                        rColBut.FilterControlWidth = Integer.Parse(row("Width").ToString())
                    End If
                    rColBut.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    rColBut.ButtonType = GridButtonColumnType.PushButton
                    rColBut.Text = "Danh sách"
                    rColBut.CommandName = "DETAIL"
                    'rColBut.AllowFiltering = True
                    'rColBut.AllowSorting = True
                    rColBut.AutoPostBackOnFilter = True
                    rColBut.CurrentFilterFunction = GridKnownFunction.Contains
                    rColBut.ShowFilterIcon = False
                    rColBut.HeaderTooltip = (row.Field(Of String)("Name").Trim())
                    rColBut.FilterControlToolTip = (row.Field(Of String)("Name").Trim())
                    rColBut.Visible = Boolean.Parse(row.Item("Is_Visible"))
                    If row.Field(Of String)("DataType").Trim().ToUpper = "DateTime".ToUpper Then
                        rCol.DataFormatString = ConfigurationManager.AppSettings("FDATEGRID")
                    ElseIf row.Field(Of String)("DataType").Trim() = "Numeric" Then
                        rCol.ItemStyle.HorizontalAlign = HorizontalAlign.Right
                        rCol.DataFormatString = "{0:#,##0.##}"
                    End If
                ElseIf row.Field(Of String)("DataType").Trim().ToUpper = "DownloadNPT".ToUpper Then
                    rColDownloadNPT = New GridButtonColumn()
                    rg.MasterTableView.Columns.Add(rColDownloadNPT)
                    rColDownloadNPT.UniqueName = row.Field(Of String)("ID").Trim()
                    rColDownloadNPT.HeaderText = Translate(row.Field(Of String)("Name").Trim())
                    If IsNumeric(row("Width").ToString()) Then
                        rColDownloadNPT.HeaderStyle.Width = Integer.Parse(row("Width").ToString())
                        rColDownloadNPT.FilterControlWidth = Integer.Parse(row("Width").ToString())
                    End If
                    rColDownloadNPT.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    rColDownloadNPT.ButtonType = GridButtonColumnType.PushButton
                    rColDownloadNPT.Text = "Tải"
                    rColDownloadNPT.CommandName = "DowloadNPT"
                    rColDownloadNPT.ImageUrl = "~/Static/Images/Icons/16/icon_dowloadFile.png"
                    rColDownloadNPT.ButtonType = GridButtonColumnType.ImageButton
                    'rColDownloadNPT.AllowFiltering = True
                    'rColDownloadNPT.AllowSorting = True
                    rColDownloadNPT.AutoPostBackOnFilter = True
                    rColDownloadNPT.CurrentFilterFunction = GridKnownFunction.Contains
                    rColDownloadNPT.ShowFilterIcon = False
                    rColDownloadNPT.HeaderTooltip = (row.Field(Of String)("Name").Trim())
                    rColDownloadNPT.FilterControlToolTip = (row.Field(Of String)("Name").Trim())
                    rColDownloadNPT.Visible = Boolean.Parse(row.Item("Is_Visible"))
                    rColDownloadNPT.ItemStyle.HorizontalAlign = HorizontalAlign.Center
                ElseIf row.Field(Of String)("DataType").Trim().ToUpper = "ViewNPT".ToUpper Then
                    rColViewNPT = New GridButtonColumn()
                    rg.MasterTableView.Columns.Add(rColViewNPT)
                    rColViewNPT.UniqueName = row.Field(Of String)("ID").Trim()
                    rColViewNPT.HeaderText = Translate(row.Field(Of String)("Name").Trim())
                    If IsNumeric(row("Width").ToString()) Then
                        rColViewNPT.HeaderStyle.Width = Integer.Parse(row("Width").ToString())
                        rColViewNPT.FilterControlWidth = Integer.Parse(row("Width").ToString())
                    End If
                    rColViewNPT.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    rColViewNPT.ButtonType = GridButtonColumnType.PushButton
                    rColViewNPT.Text = "View"
                    rColViewNPT.CommandName = "ViewNPT"
                    rColViewNPT.ImageUrl = "~/Static/Images/Icons/16/ViewImgOrg.png"
                    rColViewNPT.ButtonType = GridButtonColumnType.ImageButton
                    'rColViewNPT.AllowFiltering = True
                    'rColViewNPT.AllowSorting = True
                    rColViewNPT.AutoPostBackOnFilter = True
                    rColViewNPT.CurrentFilterFunction = GridKnownFunction.Contains
                    rColViewNPT.ShowFilterIcon = False
                    rColViewNPT.HeaderTooltip = (row.Field(Of String)("Name").Trim())
                    rColViewNPT.FilterControlToolTip = (row.Field(Of String)("Name").Trim())
                    rColViewNPT.Visible = Boolean.Parse(row.Item("Is_Visible"))
                    rColViewNPT.ItemStyle.HorizontalAlign = HorizontalAlign.Center
                ElseIf row.Field(Of String)("DataType").Trim().ToUpper = "DownloadFamily".ToUpper Then
                    rColDownloadFamily = New GridButtonColumn()
                    rg.MasterTableView.Columns.Add(rColDownloadFamily)
                    rColDownloadFamily.UniqueName = row.Field(Of String)("ID").Trim()
                    rColDownloadFamily.HeaderText = Translate(row.Field(Of String)("Name").Trim())
                    If IsNumeric(row("Width").ToString()) Then
                        rColDownloadFamily.HeaderStyle.Width = Integer.Parse(row("Width").ToString())
                        rColDownloadFamily.FilterControlWidth = Integer.Parse(row("Width").ToString())
                    End If
                    rColDownloadFamily.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    rColDownloadFamily.ButtonType = GridButtonColumnType.PushButton
                    rColDownloadFamily.Text = "Tải"
                    rColDownloadFamily.CommandName = "DowloadFamily"
                    rColDownloadFamily.ImageUrl = "~/Static/Images/Icons/16/icon_dowloadFile.png"
                    rColDownloadFamily.ButtonType = GridButtonColumnType.ImageButton
                    'rColDownloadFamily.AllowFiltering = True
                    'rColDownloadFamily.AllowSorting = True
                    rColDownloadFamily.AutoPostBackOnFilter = True
                    rColDownloadFamily.CurrentFilterFunction = GridKnownFunction.Contains
                    rColDownloadFamily.ShowFilterIcon = False
                    rColDownloadFamily.HeaderTooltip = (row.Field(Of String)("Name").Trim())
                    rColDownloadFamily.FilterControlToolTip = (row.Field(Of String)("Name").Trim())
                    rColDownloadFamily.Visible = Boolean.Parse(row.Item("Is_Visible"))
                    rColDownloadFamily.ItemStyle.HorizontalAlign = HorizontalAlign.Center
                ElseIf row.Field(Of String)("DataType").Trim().ToUpper = "ViewFamily".ToUpper Then
                    rColViewFamily = New GridButtonColumn()
                    rg.MasterTableView.Columns.Add(rColViewFamily)
                    rColViewFamily.UniqueName = row.Field(Of String)("ID").Trim()
                    rColViewFamily.HeaderText = Translate(row.Field(Of String)("Name").Trim())
                    If IsNumeric(row("Width").ToString()) Then
                        rColViewFamily.HeaderStyle.Width = Integer.Parse(row("Width").ToString())
                        rColViewFamily.FilterControlWidth = Integer.Parse(row("Width").ToString())
                    End If
                    rColViewFamily.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    rColViewFamily.ButtonType = GridButtonColumnType.PushButton
                    rColViewFamily.Text = "View"
                    rColViewFamily.CommandName = "ViewFamily"
                    rColViewFamily.ImageUrl = "~/Static/Images/Icons/16/ViewImgOrg.png"
                    rColViewFamily.ButtonType = GridButtonColumnType.ImageButton
                    'rColViewFamily.AllowFiltering = True
                    'rColViewFamily.AllowSorting = True
                    rColViewFamily.AutoPostBackOnFilter = True
                    rColViewFamily.CurrentFilterFunction = GridKnownFunction.Contains
                    rColViewFamily.ShowFilterIcon = False
                    rColViewFamily.HeaderTooltip = (row.Field(Of String)("Name").Trim())
                    rColViewFamily.FilterControlToolTip = (row.Field(Of String)("Name").Trim())
                    rColViewFamily.Visible = Boolean.Parse(row.Item("Is_Visible"))
                    rColViewFamily.ItemStyle.HorizontalAlign = HorizontalAlign.Center
                ElseIf row.Field(Of String)("DataType").Trim().ToUpper = "Download".ToUpper Then
                    rColDownload = New GridButtonColumn()
                    rg.MasterTableView.Columns.Add(rColDownload)
                    rColDownload.UniqueName = row.Field(Of String)("ID").Trim()
                    rColDownload.HeaderText = Translate(row.Field(Of String)("Name").Trim())
                    If IsNumeric(row("Width").ToString()) Then
                        rColDownload.HeaderStyle.Width = Integer.Parse(row("Width").ToString())
                        rColDownload.FilterControlWidth = Integer.Parse(row("Width").ToString())
                    End If
                    rColDownload.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    rColDownload.ButtonType = GridButtonColumnType.PushButton
                    rColDownload.Text = "Tải"
                    rColDownload.CommandName = "Dowload"
                    rColDownload.ImageUrl = "~/Static/Images/Icons/16/icon_dowloadFile.png"
                    rColDownload.ButtonType = GridButtonColumnType.ImageButton
                    'rColDownload.AllowFiltering = True
                    'rColDownload.AllowSorting = True
                    rColDownload.AutoPostBackOnFilter = True
                    rColDownload.CurrentFilterFunction = GridKnownFunction.Contains
                    rColDownload.ShowFilterIcon = False
                    rColDownload.HeaderTooltip = (row.Field(Of String)("Name").Trim())
                    rColDownload.FilterControlToolTip = (row.Field(Of String)("Name").Trim())
                    rColDownload.Visible = Boolean.Parse(row.Item("Is_Visible"))
                    rColDownload.ItemStyle.HorizontalAlign = HorizontalAlign.Center
                ElseIf row.Field(Of String)("DataType").Trim().ToUpper = "View".ToUpper Then
                    rColView = New GridButtonColumn()
                    rg.MasterTableView.Columns.Add(rColView)
                    rColView.UniqueName = row.Field(Of String)("ID").Trim()
                    rColView.HeaderText = Translate(row.Field(Of String)("Name").Trim())
                    If IsNumeric(row("Width").ToString()) Then
                        rColView.HeaderStyle.Width = Integer.Parse(row("Width").ToString())
                        rColView.FilterControlWidth = Integer.Parse(row("Width").ToString())
                    End If
                    rColView.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    rColView.ButtonType = GridButtonColumnType.PushButton
                    rColView.Text = "View"
                    rColView.CommandName = "View"
                    rColView.ImageUrl = "~/Static/Images/Icons/16/ViewImgOrg.png"
                    rColView.ButtonType = GridButtonColumnType.ImageButton
                    'rColView.AllowFiltering = True
                    'rColView.AllowSorting = True
                    rColView.AutoPostBackOnFilter = True
                    rColView.CurrentFilterFunction = GridKnownFunction.Contains
                    rColView.ShowFilterIcon = False
                    rColView.HeaderTooltip = (row.Field(Of String)("Name").Trim())
                    rColView.FilterControlToolTip = (row.Field(Of String)("Name").Trim())
                    rColView.Visible = Boolean.Parse(row.Item("Is_Visible"))
                    rColView.ItemStyle.HorizontalAlign = HorizontalAlign.Center
                ElseIf row.Field(Of String)("DataType").Trim().ToUpper = "ViewImgNewTab".ToUpper Then
                    rColView = New GridButtonColumn()
                    rg.MasterTableView.Columns.Add(rColView)
                    rColView.UniqueName = row.Field(Of String)("ID").Trim()
                    rColView.HeaderText = Translate(row.Field(Of String)("Name").Trim())
                    If IsNumeric(row("Width").ToString()) Then
                        rColView.HeaderStyle.Width = Integer.Parse(row("Width").ToString())
                        rColView.FilterControlWidth = Integer.Parse(row("Width").ToString())
                    End If
                    rColView.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    rColView.ButtonType = GridButtonColumnType.PushButton
                    rColView.Text = "Check hình"
                    rColView.CommandName = "View"
                    rColView.ImageUrl = "~/Static/Images/Icons/16/ViewImgOrg.png"
                    rColView.ButtonType = GridButtonColumnType.ImageButton
                    rColView.AutoPostBackOnFilter = True
                    rColView.CurrentFilterFunction = GridKnownFunction.Contains
                    rColView.ShowFilterIcon = False
                    rColView.HeaderTooltip = (row.Field(Of String)("Name").Trim())
                    rColView.FilterControlToolTip = (row.Field(Of String)("Name").Trim())
                    rColView.Visible = Boolean.Parse(row.Item("Is_Visible"))
                    rColView.ItemStyle.HorizontalAlign = HorizontalAlign.Center
                ElseIf row.Field(Of String)("DataType").Trim().ToUpper = "ViewMap".ToUpper Then
                    rColMap = New GridButtonColumn()
                    rg.MasterTableView.Columns.Add(rColMap)
                    rColMap.UniqueName = row.Field(Of String)("ID").Trim()
                    rColMap.HeaderText = Translate(row.Field(Of String)("Name").Trim())
                    If IsNumeric(row("Width").ToString()) Then
                        rColMap.HeaderStyle.Width = Integer.Parse(row("Width").ToString())
                        rColMap.FilterControlWidth = Integer.Parse(row("Width").ToString())
                    End If
                    rColMap.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    rColMap.ButtonType = GridButtonColumnType.PushButton
                    rColMap.Text = "Check map"
                    rColMap.CommandName = "ViewMap"
                    rColMap.ImageUrl = "~/Static/Images/Icons/16/ViewImgOrg.png"
                    rColMap.ButtonType = GridButtonColumnType.ImageButton
                    rColMap.AutoPostBackOnFilter = True
                    rColMap.CurrentFilterFunction = GridKnownFunction.Contains
                    rColMap.ShowFilterIcon = False
                    rColMap.HeaderTooltip = (row.Field(Of String)("Name").Trim())
                    rColMap.FilterControlToolTip = (row.Field(Of String)("Name").Trim())
                    rColMap.Visible = Boolean.Parse(row.Item("Is_Visible"))
                    rColMap.ItemStyle.HorizontalAlign = HorizontalAlign.Center
                ElseIf row.Field(Of String)("DataType").Trim().ToUpper = "DowloadImage".ToUpper Then
                    rColDownloadFamily = New GridButtonColumn()
                    rg.MasterTableView.Columns.Add(rColDownloadFamily)
                    rColDownloadFamily.UniqueName = row.Field(Of String)("ID").Trim()
                    rColDownloadFamily.HeaderText = Translate(row.Field(Of String)("Name").Trim())
                    If IsNumeric(row("Width").ToString()) Then
                        rColDownloadFamily.HeaderStyle.Width = Integer.Parse(row("Width").ToString())
                        rColDownloadFamily.FilterControlWidth = Integer.Parse(row("Width").ToString())
                    End If
                    rColDownloadFamily.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    rColDownloadFamily.ButtonType = GridButtonColumnType.PushButton
                    rColDownloadFamily.Text = "Tải"
                    rColDownloadFamily.CommandName = "DowloadImage"
                    rColDownloadFamily.ImageUrl = "~/Static/Images/Icons/16/icon_dowloadFile.png"
                    rColDownloadFamily.ButtonType = GridButtonColumnType.ImageButton
                    'rColDownloadFamily.AllowFiltering = True
                    'rColDownloadFamily.AllowSorting = True
                    rColDownloadFamily.AutoPostBackOnFilter = True
                    rColDownloadFamily.CurrentFilterFunction = GridKnownFunction.Contains
                    rColDownloadFamily.ShowFilterIcon = False
                    rColDownloadFamily.HeaderTooltip = (row.Field(Of String)("Name").Trim())
                    rColDownloadFamily.FilterControlToolTip = (row.Field(Of String)("Name").Trim())
                    rColDownloadFamily.Visible = Boolean.Parse(row.Item("Is_Visible"))
                    rColDownloadFamily.ItemStyle.HorizontalAlign = HorizontalAlign.Center
                ElseIf row.Field(Of String)("DataType").Trim().ToUpper = "ViewImage".ToUpper Then
                    rColViewFamily = New GridButtonColumn()
                    rg.MasterTableView.Columns.Add(rColViewFamily)
                    rColViewFamily.UniqueName = row.Field(Of String)("ID").Trim()
                    rColViewFamily.HeaderText = Translate(row.Field(Of String)("Name").Trim())
                    If IsNumeric(row("Width").ToString()) Then
                        rColViewFamily.HeaderStyle.Width = Integer.Parse(row("Width").ToString())
                        rColViewFamily.FilterControlWidth = Integer.Parse(row("Width").ToString())
                    End If
                    rColViewFamily.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    rColViewFamily.ButtonType = GridButtonColumnType.PushButton
                    rColViewFamily.Text = "View"
                    rColViewFamily.CommandName = "ViewImage"
                    rColViewFamily.ImageUrl = "~/Static/Images/Icons/16/ViewImgOrg.png"
                    rColViewFamily.ButtonType = GridButtonColumnType.ImageButton
                    'rColViewFamily.AllowFiltering = True
                    'rColViewFamily.AllowSorting = True
                    rColViewFamily.AutoPostBackOnFilter = True
                    rColViewFamily.CurrentFilterFunction = GridKnownFunction.Contains
                    rColViewFamily.ShowFilterIcon = False
                    rColViewFamily.HeaderTooltip = (row.Field(Of String)("Name").Trim())
                    rColViewFamily.FilterControlToolTip = (row.Field(Of String)("Name").Trim())
                    rColViewFamily.Visible = Boolean.Parse(row.Item("Is_Visible"))
                    rColViewFamily.ItemStyle.HorizontalAlign = HorizontalAlign.Center
                ElseIf row.Field(Of String)("DataType").Trim().ToUpper = "DowloadCMND".ToUpper Then
                    rColDownloadFamily = New GridButtonColumn()
                    rg.MasterTableView.Columns.Add(rColDownloadFamily)
                    rColDownloadFamily.UniqueName = row.Field(Of String)("ID").Trim()
                    rColDownloadFamily.HeaderText = Translate(row.Field(Of String)("Name").Trim())
                    If IsNumeric(row("Width").ToString()) Then
                        rColDownloadFamily.HeaderStyle.Width = Integer.Parse(row("Width").ToString())
                        rColDownloadFamily.FilterControlWidth = Integer.Parse(row("Width").ToString())
                    End If
                    rColDownloadFamily.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    rColDownloadFamily.ButtonType = GridButtonColumnType.PushButton
                    rColDownloadFamily.Text = "Tải"
                    rColDownloadFamily.CommandName = "DowloadCMND"
                    rColDownloadFamily.ImageUrl = "~/Static/Images/Icons/16/icon_dowloadFile.png"
                    rColDownloadFamily.ButtonType = GridButtonColumnType.ImageButton
                    'rColDownloadFamily.AllowFiltering = True
                    'rColDownloadFamily.AllowSorting = True
                    rColDownloadFamily.AutoPostBackOnFilter = True
                    rColDownloadFamily.CurrentFilterFunction = GridKnownFunction.Contains
                    rColDownloadFamily.ShowFilterIcon = False
                    rColDownloadFamily.HeaderTooltip = (row.Field(Of String)("Name").Trim())
                    rColDownloadFamily.FilterControlToolTip = (row.Field(Of String)("Name").Trim())
                    rColDownloadFamily.Visible = Boolean.Parse(row.Item("Is_Visible"))
                    rColDownloadFamily.ItemStyle.HorizontalAlign = HorizontalAlign.Center
                ElseIf row.Field(Of String)("DataType").Trim().ToUpper = "ViewCMND".ToUpper Then
                    rColViewFamily = New GridButtonColumn()
                    rg.MasterTableView.Columns.Add(rColViewFamily)
                    rColViewFamily.UniqueName = row.Field(Of String)("ID").Trim()
                    rColViewFamily.HeaderText = Translate(row.Field(Of String)("Name").Trim())
                    If IsNumeric(row("Width").ToString()) Then
                        rColViewFamily.HeaderStyle.Width = Integer.Parse(row("Width").ToString())
                        rColViewFamily.FilterControlWidth = Integer.Parse(row("Width").ToString())
                    End If
                    rColViewFamily.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    rColViewFamily.ButtonType = GridButtonColumnType.PushButton
                    rColViewFamily.Text = "View"
                    rColViewFamily.CommandName = "ViewCMND"
                    rColViewFamily.ImageUrl = "~/Static/Images/Icons/16/ViewImgOrg.png"
                    rColViewFamily.ButtonType = GridButtonColumnType.ImageButton
                    'rColViewFamily.AllowFiltering = True
                    'rColViewFamily.AllowSorting = True
                    rColViewFamily.AutoPostBackOnFilter = True
                    rColViewFamily.CurrentFilterFunction = GridKnownFunction.Contains
                    rColViewFamily.ShowFilterIcon = False
                    rColViewFamily.HeaderTooltip = (row.Field(Of String)("Name").Trim())
                    rColViewFamily.FilterControlToolTip = (row.Field(Of String)("Name").Trim())
                    rColViewFamily.Visible = Boolean.Parse(row.Item("Is_Visible"))
                    rColViewFamily.ItemStyle.HorizontalAlign = HorizontalAlign.Center
                ElseIf row.Field(Of String)("DataType").Trim().ToUpper = "DowloadCMNDBack".ToUpper Then
                    rColDownloadFamily = New GridButtonColumn()
                    rg.MasterTableView.Columns.Add(rColDownloadFamily)
                    rColDownloadFamily.UniqueName = row.Field(Of String)("ID").Trim()
                    rColDownloadFamily.HeaderText = Translate(row.Field(Of String)("Name").Trim())
                    If IsNumeric(row("Width").ToString()) Then
                        rColDownloadFamily.HeaderStyle.Width = Integer.Parse(row("Width").ToString())
                        rColDownloadFamily.FilterControlWidth = Integer.Parse(row("Width").ToString())
                    End If
                    rColDownloadFamily.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    rColDownloadFamily.ButtonType = GridButtonColumnType.PushButton
                    rColDownloadFamily.Text = "Tải"
                    rColDownloadFamily.CommandName = "DowloadCMNDBack"
                    rColDownloadFamily.ImageUrl = "~/Static/Images/Icons/16/icon_dowloadFile.png"
                    rColDownloadFamily.ButtonType = GridButtonColumnType.ImageButton
                    'rColDownloadFamily.AllowFiltering = True
                    'rColDownloadFamily.AllowSorting = True
                    rColDownloadFamily.AutoPostBackOnFilter = True
                    rColDownloadFamily.CurrentFilterFunction = GridKnownFunction.Contains
                    rColDownloadFamily.ShowFilterIcon = False
                    rColDownloadFamily.HeaderTooltip = (row.Field(Of String)("Name").Trim())
                    rColDownloadFamily.FilterControlToolTip = (row.Field(Of String)("Name").Trim())
                    rColDownloadFamily.Visible = Boolean.Parse(row.Item("Is_Visible"))
                    rColDownloadFamily.ItemStyle.HorizontalAlign = HorizontalAlign.Center
                ElseIf row.Field(Of String)("DataType").Trim().ToUpper = "ViewCMNDBack".ToUpper Then
                    rColViewFamily = New GridButtonColumn()
                    rg.MasterTableView.Columns.Add(rColViewFamily)
                    rColViewFamily.UniqueName = row.Field(Of String)("ID").Trim()
                    rColViewFamily.HeaderText = Translate(row.Field(Of String)("Name").Trim())
                    If IsNumeric(row("Width").ToString()) Then
                        rColViewFamily.HeaderStyle.Width = Integer.Parse(row("Width").ToString())
                        rColViewFamily.FilterControlWidth = Integer.Parse(row("Width").ToString())
                    End If
                    rColViewFamily.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    rColViewFamily.ButtonType = GridButtonColumnType.PushButton
                    rColViewFamily.Text = "View"
                    rColViewFamily.CommandName = "ViewCMNDBack"
                    rColViewFamily.ImageUrl = "~/Static/Images/Icons/16/ViewImgOrg.png"
                    rColViewFamily.ButtonType = GridButtonColumnType.ImageButton
                    'rColViewFamily.AllowFiltering = True
                    'rColViewFamily.AllowSorting = True
                    rColViewFamily.AutoPostBackOnFilter = True
                    rColViewFamily.CurrentFilterFunction = GridKnownFunction.Contains
                    rColViewFamily.ShowFilterIcon = False
                    rColViewFamily.HeaderTooltip = (row.Field(Of String)("Name").Trim())
                    rColViewFamily.FilterControlToolTip = (row.Field(Of String)("Name").Trim())
                    rColViewFamily.Visible = Boolean.Parse(row.Item("Is_Visible"))
                    rColViewFamily.ItemStyle.HorizontalAlign = HorizontalAlign.Center
                ElseIf row.Field(Of String)("DataType").Trim().ToUpper = "DowloadAddress".ToUpper Then
                    rColDownloadFamily = New GridButtonColumn()
                    rg.MasterTableView.Columns.Add(rColDownloadFamily)
                    rColDownloadFamily.UniqueName = row.Field(Of String)("ID").Trim()
                    rColDownloadFamily.HeaderText = Translate(row.Field(Of String)("Name").Trim())
                    If IsNumeric(row("Width").ToString()) Then
                        rColDownloadFamily.HeaderStyle.Width = Integer.Parse(row("Width").ToString())
                        rColDownloadFamily.FilterControlWidth = Integer.Parse(row("Width").ToString())
                    End If
                    rColDownloadFamily.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    rColDownloadFamily.ButtonType = GridButtonColumnType.PushButton
                    rColDownloadFamily.Text = "Tải"
                    rColDownloadFamily.CommandName = "DowloadAddress"
                    rColDownloadFamily.ImageUrl = "~/Static/Images/Icons/16/icon_dowloadFile.png"
                    rColDownloadFamily.ButtonType = GridButtonColumnType.ImageButton
                    'rColDownloadFamily.AllowFiltering = True
                    'rColDownloadFamily.AllowSorting = True
                    rColDownloadFamily.AutoPostBackOnFilter = True
                    rColDownloadFamily.CurrentFilterFunction = GridKnownFunction.Contains
                    rColDownloadFamily.ShowFilterIcon = False
                    rColDownloadFamily.HeaderTooltip = (row.Field(Of String)("Name").Trim())
                    rColDownloadFamily.FilterControlToolTip = (row.Field(Of String)("Name").Trim())
                    rColDownloadFamily.Visible = Boolean.Parse(row.Item("Is_Visible"))
                    rColDownloadFamily.ItemStyle.HorizontalAlign = HorizontalAlign.Center
                ElseIf row.Field(Of String)("DataType").Trim().ToUpper = "ViewAddress".ToUpper Then
                    rColViewFamily = New GridButtonColumn()
                    rg.MasterTableView.Columns.Add(rColViewFamily)
                    rColViewFamily.UniqueName = row.Field(Of String)("ID").Trim()
                    rColViewFamily.HeaderText = Translate(row.Field(Of String)("Name").Trim())
                    If IsNumeric(row("Width").ToString()) Then
                        rColViewFamily.HeaderStyle.Width = Integer.Parse(row("Width").ToString())
                        rColViewFamily.FilterControlWidth = Integer.Parse(row("Width").ToString())
                    End If
                    rColViewFamily.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    rColViewFamily.ButtonType = GridButtonColumnType.PushButton
                    rColViewFamily.Text = "View"
                    rColViewFamily.CommandName = "ViewAddress"
                    rColViewFamily.ImageUrl = "~/Static/Images/Icons/16/ViewImgOrg.png"
                    rColViewFamily.ButtonType = GridButtonColumnType.ImageButton
                    'rColViewFamily.AllowFiltering = True
                    'rColViewFamily.AllowSorting = True
                    rColViewFamily.AutoPostBackOnFilter = True
                    rColViewFamily.CurrentFilterFunction = GridKnownFunction.Contains
                    rColViewFamily.ShowFilterIcon = False
                    rColViewFamily.HeaderTooltip = (row.Field(Of String)("Name").Trim())
                    rColViewFamily.FilterControlToolTip = (row.Field(Of String)("Name").Trim())
                    rColViewFamily.Visible = Boolean.Parse(row.Item("Is_Visible"))
                    rColViewFamily.ItemStyle.HorizontalAlign = HorizontalAlign.Center
                ElseIf row.Field(Of String)("DataType").Trim().ToUpper = "DowloadBank".ToUpper Then
                    rColDownloadFamily = New GridButtonColumn()
                    rg.MasterTableView.Columns.Add(rColDownloadFamily)
                    rColDownloadFamily.UniqueName = row.Field(Of String)("ID").Trim()
                    rColDownloadFamily.HeaderText = Translate(row.Field(Of String)("Name").Trim())
                    If IsNumeric(row("Width").ToString()) Then
                        rColDownloadFamily.HeaderStyle.Width = Integer.Parse(row("Width").ToString())
                        rColDownloadFamily.FilterControlWidth = Integer.Parse(row("Width").ToString())
                    End If
                    rColDownloadFamily.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    rColDownloadFamily.ButtonType = GridButtonColumnType.PushButton
                    rColDownloadFamily.Text = "Tải"
                    rColDownloadFamily.CommandName = "DowloadBank"
                    rColDownloadFamily.ImageUrl = "~/Static/Images/Icons/16/icon_dowloadFile.png"
                    rColDownloadFamily.ButtonType = GridButtonColumnType.ImageButton
                    'rColDownloadFamily.AllowFiltering = True
                    'rColDownloadFamily.AllowSorting = True
                    rColDownloadFamily.AutoPostBackOnFilter = True
                    rColDownloadFamily.CurrentFilterFunction = GridKnownFunction.Contains
                    rColDownloadFamily.ShowFilterIcon = False
                    rColDownloadFamily.HeaderTooltip = (row.Field(Of String)("Name").Trim())
                    rColDownloadFamily.FilterControlToolTip = (row.Field(Of String)("Name").Trim())
                    rColDownloadFamily.Visible = Boolean.Parse(row.Item("Is_Visible"))
                    rColDownloadFamily.ItemStyle.HorizontalAlign = HorizontalAlign.Center
                ElseIf row.Field(Of String)("DataType").Trim().ToUpper = "ViewBank".ToUpper Then
                    rColViewFamily = New GridButtonColumn()
                    rg.MasterTableView.Columns.Add(rColViewFamily)
                    rColViewFamily.UniqueName = row.Field(Of String)("ID").Trim()
                    rColViewFamily.HeaderText = Translate(row.Field(Of String)("Name").Trim())
                    If IsNumeric(row("Width").ToString()) Then
                        rColViewFamily.HeaderStyle.Width = Integer.Parse(row("Width").ToString())
                        rColViewFamily.FilterControlWidth = Integer.Parse(row("Width").ToString())
                    End If
                    rColViewFamily.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    rColViewFamily.ButtonType = GridButtonColumnType.PushButton
                    rColViewFamily.Text = "View"
                    rColViewFamily.CommandName = "ViewBank"
                    rColViewFamily.ImageUrl = "~/Static/Images/Icons/16/ViewImgOrg.png"
                    rColViewFamily.ButtonType = GridButtonColumnType.ImageButton
                    'rColViewFamily.AllowFiltering = True
                    'rColViewFamily.AllowSorting = True
                    rColViewFamily.AutoPostBackOnFilter = True
                    rColViewFamily.CurrentFilterFunction = GridKnownFunction.Contains
                    rColViewFamily.ShowFilterIcon = False
                    rColViewFamily.HeaderTooltip = (row.Field(Of String)("Name").Trim())
                    rColViewFamily.FilterControlToolTip = (row.Field(Of String)("Name").Trim())
                    rColViewFamily.Visible = Boolean.Parse(row.Item("Is_Visible"))
                    rColViewFamily.ItemStyle.HorizontalAlign = HorizontalAlign.Center
                ElseIf row.Field(Of String)("DataType").Trim().ToUpper = "DowloadOther".ToUpper Then
                    rColDownloadFamily = New GridButtonColumn()
                    rg.MasterTableView.Columns.Add(rColDownloadFamily)
                    rColDownloadFamily.UniqueName = row.Field(Of String)("ID").Trim()
                    rColDownloadFamily.HeaderText = Translate(row.Field(Of String)("Name").Trim())
                    If IsNumeric(row("Width").ToString()) Then
                        rColDownloadFamily.HeaderStyle.Width = Integer.Parse(row("Width").ToString())
                        rColDownloadFamily.FilterControlWidth = Integer.Parse(row("Width").ToString())
                    End If
                    rColDownloadFamily.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    rColDownloadFamily.ButtonType = GridButtonColumnType.PushButton
                    rColDownloadFamily.Text = "Tải"
                    rColDownloadFamily.CommandName = "DowloadOther"
                    rColDownloadFamily.ImageUrl = "~/Static/Images/Icons/16/icon_dowloadFile.png"
                    rColDownloadFamily.ButtonType = GridButtonColumnType.ImageButton
                    'rColDownloadFamily.AllowFiltering = True
                    'rColDownloadFamily.AllowSorting = True
                    rColDownloadFamily.AutoPostBackOnFilter = True
                    rColDownloadFamily.CurrentFilterFunction = GridKnownFunction.Contains
                    rColDownloadFamily.ShowFilterIcon = False
                    rColDownloadFamily.HeaderTooltip = (row.Field(Of String)("Name").Trim())
                    rColDownloadFamily.FilterControlToolTip = (row.Field(Of String)("Name").Trim())
                    rColDownloadFamily.Visible = Boolean.Parse(row.Item("Is_Visible"))
                    rColDownloadFamily.ItemStyle.HorizontalAlign = HorizontalAlign.Center
                ElseIf row.Field(Of String)("DataType").Trim().ToUpper = "ViewOther".ToUpper Then
                    rColViewFamily = New GridButtonColumn()
                    rg.MasterTableView.Columns.Add(rColViewFamily)
                    rColViewFamily.UniqueName = row.Field(Of String)("ID").Trim()
                    rColViewFamily.HeaderText = Translate(row.Field(Of String)("Name").Trim())
                    If IsNumeric(row("Width").ToString()) Then
                        rColViewFamily.HeaderStyle.Width = Integer.Parse(row("Width").ToString())
                        rColViewFamily.FilterControlWidth = Integer.Parse(row("Width").ToString())
                    End If
                    rColViewFamily.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    rColViewFamily.ButtonType = GridButtonColumnType.PushButton
                    rColViewFamily.Text = "View"
                    rColViewFamily.CommandName = "ViewOther"
                    rColViewFamily.ImageUrl = "~/Static/Images/Icons/16/ViewImgOrg.png"
                    rColViewFamily.ButtonType = GridButtonColumnType.ImageButton
                    'rColViewFamily.AllowFiltering = True
                    'rColViewFamily.AllowSorting = True
                    rColViewFamily.AutoPostBackOnFilter = True
                    rColViewFamily.CurrentFilterFunction = GridKnownFunction.Contains
                    rColViewFamily.ShowFilterIcon = False
                    rColViewFamily.HeaderTooltip = (row.Field(Of String)("Name").Trim())
                    rColViewFamily.FilterControlToolTip = (row.Field(Of String)("Name").Trim())
                    rColViewFamily.Visible = Boolean.Parse(row.Item("Is_Visible"))
                    rColViewFamily.ItemStyle.HorizontalAlign = HorizontalAlign.Center
                Else
                    rCol = New GridBoundColumn()
                    rg.MasterTableView.Columns.Add(rCol)
                    rCol.DataField = row.Field(Of String)("ID").Trim()
                    rCol.HeaderText = Translate(row.Field(Of String)("Name").Trim())
                    If IsNumeric(row("Width").ToString()) Then
                        rCol.HeaderStyle.Width = Integer.Parse(row("Width").ToString())
                        rCol.FilterControlWidth = Integer.Parse(row("Width").ToString())
                    End If
                    rCol.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    rCol.AllowFiltering = True
                    rCol.AllowSorting = True
                    rCol.AutoPostBackOnFilter = True
                    rCol.CurrentFilterFunction = GridKnownFunction.Contains
                    rCol.ShowFilterIcon = False
                    rCol.HeaderTooltip = (row.Field(Of String)("Name").Trim())
                    rCol.FilterControlToolTip = (row.Field(Of String)("Name").Trim())
                    rCol.Visible = Boolean.Parse(row.Item("Is_Visible"))
                    If row.Field(Of String)("DataType").Trim().ToUpper = "DateTime".ToUpper Then
                        rCol.DataFormatString = ConfigurationManager.AppSettings("FDATEGRID")
                        rCol.UniqueName = row.Field(Of String)("ID").Trim()
                    ElseIf row.Field(Of String)("DataType").Trim() = "DateTimeHour" Then
                        rCol.DataFormatString = "{0:dd/MM/yyyy HH:mm}"
                    ElseIf row.Field(Of String)("DataType").Trim() = "Number" Then
                        rCol.DataFormatString = "{0:#,##0.##}"
                    ElseIf row.Field(Of String)("DataType").Trim() = "Hour" Then
                        rCol.DataFormatString = "{0:HH:mm}"
                    End If
                End If
                'CacheManager.Insert("CONFIG" + Me.ID, rg.MasterTableView.Columns, Common.CacheConfigGrid)
            Catch ex As Exception
                Continue For
            End Try
        Next
    End Sub
#End Region
End Class
