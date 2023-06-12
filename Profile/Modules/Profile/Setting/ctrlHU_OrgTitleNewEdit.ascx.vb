Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlHU_OrgTitleNewEdit
    Inherits CommonView

    Protected WithEvents _ctrlFindTitlePopup As ctrlFindTitlePopup
    Protected WithEvents _ctrlFindTitlePopupParent As ctrlFindTitlePopup
    Protected WithEvents _ctrlFindOrgPopup As ctrlFindOrgPopup
    Public Overrides Property MustAuthorize As Boolean = False
    Dim psp As New ProfileStoreProcedure
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Profile/Module/Profile/Setting/" + Me.GetType().Name.ToString()
#Region "Property"
    Private _decIDSelectPopup As Decimal
    Public Property IDSelectPopup() As Decimal
        Get
            Return _decIDSelectPopup
        End Get
        Set(ByVal value As Decimal)
            _decIDSelectPopup = value
        End Set
    End Property
    Property Commend As CommendDTO
        Get
            Return ViewState(Me.ID & "_Commend")
        End Get
        Set(ByVal value As CommendDTO)
            ViewState(Me.ID & "_Commend") = value
        End Set
    End Property
    ''0 - normal
    ''1 - Employee
    ''2 - Org
    ''3 - Sign
    Property isLoadPopup As Integer
        Get
            Return ViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isLoadPopup") = value
        End Set
    End Property

    Property Employee_Commend As List(Of TitleDTO)
        Get
            Return ViewState(Me.ID & "_Employee_Commend")
        End Get
        Set(ByVal value As List(Of TitleDTO))
            ViewState(Me.ID & "_Employee_Commend") = value
        End Set
    End Property
    ''0 - NEW
    ''1 - EDIT
    Dim _FormType As Integer
    Dim _IDSelect As Decimal?
    Dim _OrgID As Decimal?
#End Region

#Region "Page"

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            GetParams()
            Refresh()
            UpdateControlState()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        InitControl()
    End Sub
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim rep As New ProfileBusinessRepository
        Try

            Select Case Message
                Case "UpdateView"
                    Commend = rep.GetCommendByID(New CommendDTO With {.ID = _IDSelect})
                    CurrentState = CommonMessage.STATE_EDIT
                    hidDecisionID.Value = Commend.ID.ToString
                    hidID.Value = Commend.ID.ToString
                    If Commend.SIGN_ID IsNot Nothing Then
                        hidSignID.Value = Commend.SIGN_ID
                    End If
                    If Commend.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID Then
                        RadPane2.Enabled = False
                        CType(MainToolBar.Items(0), RadToolBarButton).Enabled = False
                    End If
                Case "InsertView"
                    CurrentState = CommonMessage.STATE_NEW
                    Employee_Commend = New List(Of TitleDTO)
            End Select
            rep.Dispose()
            rgTitle.Rebind()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Public Overrides Sub BindData()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try

            Dim dic As New Dictionary(Of String, Control)
            Utilities.OnClientRowSelectedChanged(rgTitle, dic)
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Protected Sub InitControl()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Me.MainToolBar = tbarOrg
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Save, ToolbarItem.Seperator,
                                       ToolbarItem.Cancel)
            CType(MainToolBar.Items(0), RadToolBarButton).CausesValidation = True
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
#End Region

#Region "Event"


    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim objOrgTitle As New OrgTitleDTO
        Dim lstOrgTitle As New List(Of OrgTitleDTO)
        Dim gID As Decimal
        Dim rep As New ProfileRepository
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        For Each i As GridDataItem In rgTitle.Items
                            Dim Org As New OrgTitleDTO
                            Org.TITLE_ID = i.GetDataKeyValue("ID")
                            Org.ACTFLG = "A"
                            Org.PARENT_ID = Decimal.Parse(txtParentID.Text)
                            Org.ORG_ID = Decimal.Parse(hidOrgID.Value)
                            lstOrgTitle.Add(Org)
                        Next
                        If rgTitle.Items.Count = 0 Then
                            ShowMessage(Translate("Vui lòng chọn các chức danh"), NotifyType.Warning)
                            Exit Sub
                        End If
                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                If rep.InsertOrgTitle(lstOrgTitle, gID) Then
                                    Response.Redirect("/Default.aspx?mid=Organize&fid=ctrlHU_OrgTitle&group=Business")
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select
                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    Response.Redirect("/Default.aspx?mid=Organize&fid=ctrlHU_OrgTitle&group=Business")
            End Select
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    Private Sub rgTitle_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgTitle.NeedDataSource
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            rgTitle.DataSource = Employee_Commend
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    Protected Sub btnFindOrg_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFindOrg.Click
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            isLoadPopup = 3
            UpdateControlState()
            _ctrlFindOrgPopup.Show()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    Protected Sub btnFindSign_Click(ByVal sender As Object,
                                 ByVal e As EventArgs) Handles _
                                     btnFindTitle.Click
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            isLoadPopup = 6

            UpdateControlState()
            _ctrlFindTitlePopupParent.Show()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    Public Overrides Sub UpdateControlState()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Select Case isLoadPopup
                Case 1
                    If Not Me.Controls.Contains(_ctrlFindTitlePopup) Then
                        _ctrlFindTitlePopup = Me.Register("ctrlFindTitlePopup", "Common", "ctrlFindTitlePopup")
                        Dim strIDOtherSelect As String = ""

                        For Each TitleDTO In Me.Employee_Commend
                            ' Xu ly lay list
                            strIDOtherSelect += TitleDTO.ID.ToString() + ","
                        Next
                        If strIDOtherSelect.Length > 1 Then
                            strIDOtherSelect = strIDOtherSelect.Substring(0, strIDOtherSelect.Length - 1)
                        End If
                        _ctrlFindTitlePopup.TitleOtherSelect = strIDOtherSelect
                        Me.Controls.Add(_ctrlFindTitlePopup)
                    End If
                Case 6
                    If Not Me.Controls.Contains(_ctrlFindTitlePopupParent) Then
                        _ctrlFindTitlePopupParent = Me.Register("ctrlFindTitlePopup", "Common", "ctrlFindTitlePopup")

                        Me.Controls.Add(_ctrlFindTitlePopupParent)
                    End If
                Case 3
                    If Not Me.Controls.Contains(_ctrlFindOrgPopup) Then
                        _ctrlFindOrgPopup = Me.Register("ctrlFindOrgPopup", "Common", "ctrlFindOrgPopup")
                        Me.Controls.Add(_ctrlFindOrgPopup)
                    End If

            End Select
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    Private Sub ctrlFindOrgPopup_OrganizationSelected(ByVal sender As Object, ByVal e As Common.OrganizationSelectedEventArgs) Handles _ctrlFindOrgPopup.OrganizationSelected
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim orgItem = _ctrlFindOrgPopup.CurrentItemDataObject
            If orgItem IsNot Nothing Then
                hidOrgID.Value = e.CurrentValue 'gán org đã chọn vào hiddenfield
                txtOrgName.Text = orgItem.NAME_VN
                If orgItem.DESCRIPTION_PATH IsNot Nothing AndAlso orgItem.DESCRIPTION_PATH <> "" Then
                    txtOrgName.ToolTip = Utilities.DrawTreeByString(orgItem.DESCRIPTION_PATH)
                End If
            End If
            isLoadPopup = 0
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Sub ctrlFindEmpImportPopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles _ctrlFindTitlePopup.CancelClicked, _ctrlFindTitlePopupParent.CancelClicked
        isLoadPopup = 0
    End Sub

    Private Sub ctrlFindEmpImportPopup_TitleSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles _ctrlFindTitlePopup.TitleSelected
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim lstCommonEmployee As New List(Of Common.CommonBusiness.TitleDTO)
        'Dim rep As New ProfileBusinessRepository
        'Dim repNew As New ProfileRepository
        Try

            lstCommonEmployee = CType(_ctrlFindTitlePopup.SelectedTitle1, List(Of Common.CommonBusiness.TitleDTO))

            If lstCommonEmployee.Count > 0 Then
                If Employee_Commend Is Nothing Then
                    Employee_Commend = New List(Of TitleDTO)
                End If
                For Each emp As CommonBusiness.TitleDTO In lstCommonEmployee
                    Dim employee As New TitleDTO
                    employee.ID = emp.ID
                    employee.CODE = emp.CODE
                    employee.NAME_VN = emp.NAME_VN
                    Employee_Commend.Add(employee)
                Next
                rgTitle.Rebind()
                For Each i As GridItem In rgTitle.Items
                    i.Edit = True
                Next
                rgTitle.Rebind()
            End If
            isLoadPopup = 0
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub ctrlFindTitlePopupParent_TitleSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles _ctrlFindTitlePopupParent.TitleSelected
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            _decIDSelectPopup = _ctrlFindTitlePopupParent.SelectedTitle(0)

            FillDataParent(_decIDSelectPopup)
            isLoadPopup = 0
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' Hàm chọn nút thêm chức danh trên lưới hiển thị danh sách chức danh được chọn
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgTitle_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles rgTitle.ItemCommand
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Select Case e.CommandName
                Case "FindTitle"
                    isLoadPopup = 1 'set giá trị show popup
                    UpdateControlState() 'Add control popup
                    _ctrlFindTitlePopup.Show() 'hiển thị popup
                Case "DeleteTitle"
                    For Each i As GridDataItem In rgTitle.SelectedItems
                        Dim s = (From q In Employee_Commend Where
                                 q.ID = i.GetDataKeyValue("ID")).FirstOrDefault
                        Employee_Commend.Remove(s)
                    Next
                    rgTitle.Rebind()
            End Select
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Sub FillDataParent(ByVal empid As Decimal)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim rep As New ProfileRepository
            Dim Titles As List(Of TitleDTO)

            Titles = rep.GetTitleByID(empid)
            Dim obj As New TitleDTO
            obj = Titles.FirstOrDefault()
            txtParentCode.Text = obj.CODE
            txtParentName.Text = obj.NAME_VN
            txtParentID.Text = empid
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    Private Sub GetParams()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If CurrentState Is Nothing Then
                If Request.Params("FormType") IsNot Nothing Then
                    _FormType = Request.Params("FormType")
                End If
                If Request.Params("ID") IsNot Nothing Then
                    _IDSelect = Decimal.Parse(Request.Params("ID"))
                End If
                If Request.Params("orgID") IsNot Nothing Then
                    Dim orgID = Request.Params("orgID")
                    Dim objOrg As New OrganizationDTO
                    Dim lstOrgTitle As New List(Of OrgTitleDTO)
                    Dim rep As New ProfileRepository
                    objOrg = rep.GetOrganizationByID(orgID)
                    rep.Dispose()
                    hidOrgID.Value = objOrg.ID
                    txtOrgName.Text = objOrg.NAME_VN
                End If
                If _IDSelect IsNot Nothing Then
                    Refresh("UpdateView")
                Else
                    Refresh("InsertView")
                End If
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
#End Region
End Class