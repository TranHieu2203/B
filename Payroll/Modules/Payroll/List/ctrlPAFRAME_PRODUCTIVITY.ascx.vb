Imports System.IO
Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Payroll.PayrollBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlPAFRAME_PRODUCTIVITY
    Inherits Common.CommonView
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Private procedure As PayrollStoreProcedure
    Dim dtOrgLevel As DataTable = Nothing
    Dim dtRegion As DataTable = Nothing
    Dim dtIsunace As DataTable = Nothing
    Public isPhysical As Decimal = Decimal.Parse(ConfigurationManager.AppSettings("PHYSICAL_PATH"))

#Region "Property"

    Property Organization As PA_FRAME_PRODUCTIVITYDTO
        Get
            Return ViewState(Me.ID & "_Organization")
        End Get
        Set(ByVal value As PA_FRAME_PRODUCTIVITYDTO)
            ViewState(Me.ID & "_Organization") = value
        End Set
    End Property

    Public Property Organizations As List(Of PA_FRAME_PRODUCTIVITYDTO)
        Get
            Return ViewState(Me.ID & "_Organizations")
        End Get
        Set(ByVal value As List(Of PA_FRAME_PRODUCTIVITYDTO))
            ViewState(Me.ID & "_Organizations") = value
        End Set
    End Property

    Property ActiveOrganizations As List(Of PA_FRAME_PRODUCTIVITYDTO)
        Get
            Return ViewState(Me.ID & "_ActiveOrganizations")
        End Get
        Set(ByVal value As List(Of PA_FRAME_PRODUCTIVITYDTO))
            ViewState(Me.ID & "_ActiveOrganizations") = value
        End Set
    End Property

    Property SelectOrgFunction As String
        Get
            Return ViewState(Me.ID & "_SelectOrgFunction")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_SelectOrgFunction") = value
        End Set
    End Property

    '0 - normal
    '1 - Employee
    Property isLoadPopup As Integer
        Get
            Return ViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isLoadPopup") = value
        End Set
    End Property
    Property check As String
        Get
            Return ViewState(Me.ID & "_check")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_check") = value
        End Set
    End Property

    Property ImageFile As Telerik.Web.UI.UploadedFile
        Get
            Return ViewState(Me.ID & "_ImageFile")
        End Get
        Set(ByVal value As Telerik.Web.UI.UploadedFile)
            ViewState(Me.ID & "_ImageFile") = value
        End Set
    End Property

    Property LicenseFile As Telerik.Web.UI.UploadedFile
        Get
            Return ViewState(Me.ID & "_LicenseFile")
        End Get
        Set(ByVal value As Telerik.Web.UI.UploadedFile)
            ViewState(Me.ID & "_LicenseFile") = value
        End Set
    End Property

#End Region

#Region "Page"

    Dim org_id As String
    Dim _mylog As New MyLog()

    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Profile/Module/Profile/Setting/" + Me.GetType().Name.ToString()

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Refresh()
            UpdateControlState()
            'FillDataByTree()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        InitControl()
        'If Not IsPostBack Then
        '    ViewConfig(MainPane)
        'End If
    End Sub

    Public Overrides Sub BindData()
        Try
            GetDataCombo()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub InitControl()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarOrgFunctions
            Common.Common.BuildToolbar(Me.MainToolBar,
                                       ToolbarItem.Create,
                                       ToolbarItem.Edit, ToolbarItem.Seperator,
                                       ToolbarItem.Save,
                                       ToolbarItem.Cancel, ToolbarItem.Seperator,
                                       ToolbarItem.Active,
                                       ToolbarItem.Deactive)
            CType(Me.MainToolBar.Items(3), RadToolBarButton).CausesValidation = True
            CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = False
            CType(Me.MainToolBar.Items(4), RadToolBarButton).Enabled = False
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim lstOrganization As List(Of PA_FRAME_PRODUCTIVITYDTO)
            Dim rep As New PayrollRepository
            SelectOrgFunction = String.Empty
            Try
                Dim startTime As DateTime = DateTime.UtcNow
                If Not IsPostBack Then
                    Dim callFunction = Common.CommonRepository.GetOrganizationLocationTreeView()
                    lstOrganization = rep.GetFrame_Productivity("")
                    Dim lstorgper = (From p In Common.Common.OrganizationLocationDataSession Select p.ID).ToList()
                    Dim lst = (From p In lstOrganization Where lstorgper.Contains(p.ID)).ToList()
                    Me.Organizations = lst
                    CurrentState = CommonMessage.STATE_NORMAL
                    org_id = Request.QueryString("id")
                    If org_id IsNot String.Empty Then
                        SelectOrgFunction = org_id
                    End If
                Else

                    Select Case Message
                        Case "UpdateView"
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            Dim callFunction = Common.CommonRepository.GetOrganizationLocationTreeView()
                            lstOrganization = rep.GetFrame_Productivity("")
                            Dim lstorgper = (From p In Common.Common.OrganizationLocationDataSession Select p.ID).ToList()
                            Dim lst = (From p In lstOrganization Where lstorgper.Contains(p.ID)).ToList()
                            Me.Organizations = lst
                            CurrentState = CommonMessage.STATE_NORMAL
                            UpdateToolbarState(CurrentState)
                        Case "InsertView"
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            Dim callFunction = Common.CommonRepository.GetOrganizationLocationTreeView()
                            lstOrganization = rep.GetFrame_Productivity("")
                            'Dim lstorgper = (From p In Common.Common.OrganizationLocationDataSession Select p.ID).ToList()
                            Dim lst = (From p In lstOrganization).ToList()
                            Me.Organizations = lst
                            CurrentState = CommonMessage.STATE_NORMAL
                    End Select
                End If

                'Đưa dữ liệu vào Grid
                If Me.Organizations IsNot Nothing Then
                    If SelectOrgFunction Is String.Empty Then
                        SelectOrgFunction = treeOrgFunction.SelectedValue
                    End If
                    BuildTreeNode(treeOrgFunction, Me.Organizations, cbDissolve.Checked)
                End If
                rep.Dispose()
                _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
            Catch ex As Exception
                DisplayException(Me.ViewName, Me.ID, ex)
                _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            End Try
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub


    Private Sub GetDataCombo()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        Finally
            procedure = Nothing
        End Try
    End Sub

#End Region

#Region "Event"

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            Dim objOrgFunction As New PA_FRAME_PRODUCTIVITYDTO

            Dim gID As Decimal
            Dim rep As New PayrollRepository
            Try
                Dim startTime As DateTime = DateTime.UtcNow
                Select Case CType(e.Item, RadToolBarButton).CommandName
                    Case CommonMessage.TOOLBARITEM_CREATE
                        If Organizations.Count > 0 Then
                            If treeOrgFunction.SelectedNode Is Nothing Then
                                ShowMessage("Chưa chọn khung hệ số năng suất?", NotifyType.Warning)
                                Exit Sub
                            End If
                            txtParent_Name.Text = (From p In Organizations Where p.ID = Decimal.Parse(treeOrgFunction.SelectedNode.Value)).SingleOrDefault.NAME_VN
                        End If
                        CurrentState = CommonMessage.STATE_NEW
                        txtCode.Focus()


                        hidRepresentative.Value = Nothing

                    Case CommonMessage.TOOLBARITEM_EDIT
                        If treeOrgFunction.SelectedNode Is Nothing Then
                            ShowMessage("Chưa chọn khung hệ số năng suất?", NotifyType.Warning)
                            Exit Sub
                        End If

                        Organization = (From p In Organizations Where p.ID = Decimal.Parse(treeOrgFunction.SelectedNode.Value)).SingleOrDefault
                        CurrentState = CommonMessage.STATE_EDIT
                        txtNameVN.Focus()
                    Case CommonMessage.TOOLBARITEM_ACTIVE, CommonMessage.TOOLBARITEM_DEACTIVE
                        Dim sActive As String
                        If treeOrgFunction.SelectedNode Is Nothing Then
                            ShowMessage("Chưa chọn khung hệ số năng suất?", NotifyType.Warning)
                            Exit Sub
                        End If
                        ActiveOrganizations = (From p In Organizations Where p.ID = Decimal.Parse(treeOrgFunction.SelectedNode.Value)).ToList
                        If ActiveOrganizations.Count > 0 Then
                            sActive = ActiveOrganizations(0).ACTFLG
                            If sActive = "A" Then
                                If CType(e.Item, RadToolBarButton).CommandName = CommonMessage.TOOLBARITEM_ACTIVE Then
                                    ShowMessage("Các bản ghi đã ở trạng thái áp dụng trước đó, kiểm tra lại!", NotifyType.Warning)
                                    Exit Sub
                                End If
                            Else
                                If CType(e.Item, RadToolBarButton).CommandName = CommonMessage.TOOLBARITEM_DEACTIVE Then
                                    ShowMessage("Các bản ghi đã ở trạng thái ngưng áp dụng trước đó, kiểm tra lại!", NotifyType.Warning)
                                    Exit Sub
                                End If
                            End If

                            If sActive = "A" Then
                                'If Not rep.CheckEmployeeInOrganization(GetAllChild(ActiveOrganizations(0).ID)) Then
                                '    ShowMessage("Bạn phải điều chuyển toàn bộ nhân viên trước khi giải thể.", NotifyType.Warning)
                                '    Exit Sub
                                'End If
                                ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DEACTIVE)
                            Else

                                ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_ACTIVE)

                            End If
                            ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                            ctrlMessageBox.DataBind()
                            ctrlMessageBox.Show()
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        End If
                        Common.Common.OrganizationLocationDataSession = Nothing
                    Case CommonMessage.TOOLBARITEM_CANCEL
                        CurrentState = CommonMessage.STATE_NORMAL
                        'FillDataByTree()
                    Case CommonMessage.TOOLBARITEM_SAVE
                        Dim strFiles As String = String.Empty
                        If Not Page.IsValid Then
                            Exit Sub
                        End If

                        objOrgFunction.CODE = txtCode.Text

                        objOrgFunction.NAME_EN = txtNameEN.Text
                        objOrgFunction.NAME_VN = txtNameVN.Text
                        objOrgFunction.REMARK = txtREMARK.Text
                        If IsNumeric(txtCoefficient.Value) Then
                            objOrgFunction.COEFFICIENT = txtCoefficient.Value
                        End If
                        objOrgFunction.IS_LEVEL1 = chkIslevel1.Checked
                        objOrgFunction.IS_LEVEL2 = chkIslevel2.Checked
                        objOrgFunction.IS_LEVEL3 = chkIslevel3.Checked
                        Dim objPath As PA_FRAME_PRODUCTIVITY_PATHDTO
                        Dim lstPath As New List(Of PA_FRAME_PRODUCTIVITY_PATHDTO)
                        If hidID.Value IsNot Nothing Then
                            If hidID.Value <> "" Then
                                objOrgFunction.PARENT_ID = Decimal.Parse(hidID.Value)
                            End If
                        End If

                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                If objOrgFunction.PARENT_ID = 0 Then
                                    If Organizations.Select(Function(p) p.PARENT_ID = 0).Count > 0 Then
                                        ShowMessage("Đã tồn tại Khung hệ số lương cao nhất?", NotifyType.Warning)
                                        Exit Sub
                                    End If
                                End If
                                'If objOrgFunction.PARENT_ID = 1 Then
                                '    Dim COUNT = (From p In Organizations Where p.PARENT_ID = 1).Count
                                '    If COUNT >= (CommonConfig.SETUP_NUM_ORG()) Then
                                '        ShowMessage("Số lượng Công ty hoặc chi nhánh vượt quá số lượng cho phép", NotifyType.Warning)
                                '        Exit Sub
                                '    End If
                                'End If
                                objOrgFunction.ACTFLG = "A"
                                Dim vadidate = rep.ValidateFrame_Productivity(objOrgFunction)
                                If Not vadidate Then
                                    ShowMessage("Đã tồn tại mã khung lương", NotifyType.Warning)
                                    Exit Sub
                                End If
                                If treeOrgFunction.Nodes.Count > 0 Then
                                    GetDownLevelByNode(treeOrgFunction.Nodes(0), lstPath)
                                End If
                                Dim id As Decimal
                                id = rep.GetMaxIdFrame_Productivity()
                                id = id + 1
                                If treeOrgFunction.SelectedNode IsNot Nothing Then
                                    objPath = GetUpLevelByNode(treeOrgFunction.SelectedNode)
                                    objOrgFunction.HIERARCHICAL_PATH = objPath.HIERARCHICAL_PATH + ";" + id.ToString
                                    ' objOrgFunction.DESCRIPTION_PATH = objPath.DESCRIPTION_PATH
                                    Dim mang()
                                    Dim str As String = ""
                                    mang = objPath.HIERARCHICAL_PATH.Split(";")
                                    For Each line In mang
                                        Dim chuoi = rep.GetNameFrame_Productivity(line)
                                        str += chuoi + ";"
                                    Next
                                    str += txtNameVN.Text
                                    objOrgFunction.DESCRIPTION_PATH = str
                                End If

                                'GetDownLevelByNode(treeOrgFunction.Nodes(0), lstPath)
                                If rep.InsertFrame_Productivity(objOrgFunction, gID) Then
                                    '' số bản ghi cập nhật
                                    'Dim iSoBanGhi As Integer = 50
                                    '' số dư để làm tròn
                                    'Dim iSoDu As Integer = lstPath.Count Mod iSoBanGhi
                                    'Dim iTongVongLap As Integer
                                    '' số vòng lặp khi làm tròn vs số bản ghi cập nhật
                                    'If iSoDu = 0 Then
                                    '    iTongVongLap = (lstPath.Count - iSoDu) / iSoBanGhi
                                    'Else
                                    '    iTongVongLap = ((lstPath.Count - iSoDu) / iSoBanGhi) + 1
                                    'End If

                                    'For item = 0 To iTongVongLap - 1
                                    '    ' cập nhật từng đợt ( tối đa = số bản ghi cập nhật )
                                    '    Dim lstUpdate As New List(Of OrganizationPathDTO)

                                    '    If item <> iTongVongLap - 1 Then
                                    '        For idx = item * iSoBanGhi To (item + 1) * iSoBanGhi - 1
                                    '            lstUpdate.Add(lstPath(idx))
                                    '        Next
                                    '    Else
                                    '        For idx = item * iSoBanGhi To lstPath.Count - 1
                                    '            lstUpdate.Add(lstPath(idx))
                                    '        Next
                                    '    End If
                                    '    ' cập nhật bản ghi
                                    '    rep.ModifyOrganizationPath(lstUpdate)
                                    'Next
                                    'Using commonRepo = New CommonRepository()
                                    '    Dim orgIds = commonRepo.GetUserOrganization(LogHelper.CurrentUser.ID)
                                    '    Dim userOrgAccessDTOs = orgIds.Select(Function(s) New CommonBusiness.UserOrgAccessDTO() With {
                                    '        .USER_ID = LogHelper.CurrentUser.ID,
                                    '        .ORG_ID = s
                                    '                                              }).ToList

                                    '    userOrgAccessDTOs.Add(New CommonBusiness.UserOrgAccessDTO With {.USER_ID = LogHelper.CurrentUser.ID,
                                    '                          .ORG_ID = gID})

                                    '    commonRepo.UpdateUserOrganization(userOrgAccessDTOs)
                                    'End Using

                                    'CurrentState = CommonMessage.STATE_NORMAL
                                    'Refresh("InsertView")

                                    Refresh("InsertView")
                                    'If treeOrgFunction.Nodes.Count > 0 Then
                                    '    GetDownLevelByNode(treeOrgFunction.Nodes(0), lstPath)
                                    'End If
                                    ' số bản ghi cập nhật
                                    Dim iSoBanGhi As Integer = 50
                                    ' số dư để làm tròn
                                    Dim iSoDu As Integer = lstPath.Count Mod iSoBanGhi
                                    Dim iTongVongLap As Integer
                                    ' số vòng lặp khi làm tròn vs số bản ghi cập nhật
                                    'If iSoDu = 0 Then
                                    '    iTongVongLap = (lstPath.Count - iSoDu) / iSoBanGhi
                                    'Else
                                    '    iTongVongLap = ((lstPath.Count - iSoDu) / iSoBanGhi) + 1
                                    'End If
                                    'For item = 0 To iTongVongLap - 1
                                    '    ' cập nhật từng đợt ( tối đa = số bản ghi cập nhật )
                                    '    Dim lstUpdate As New List(Of OrganizationPathDTO)

                                    '    If item <> iTongVongLap - 1 Then
                                    '        For idx = item * iSoBanGhi To (item + 1) * iSoBanGhi - 1
                                    '            lstUpdate.Add(lstPath(idx))
                                    '        Next
                                    '    Else
                                    '        For idx = item * iSoBanGhi To lstPath.Count - 1
                                    '            lstUpdate.Add(lstPath(idx))
                                    '        Next
                                    '    End If
                                    '    ' cập nhật bản ghi
                                    '    rep.ModifyOrganizationPath(lstUpdate)
                                    'Next

                                    CurrentState = CommonMessage.STATE_NORMAL

                                    Common.Common.OrganizationLocationDataSession = Nothing

                                    Dim store As New CommonProcedureNew
                                    store.UPDATE_AUTHOR_USER(gID)
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                                Common.Common.OrganizationLocationDataSession = Nothing
                            Case CommonMessage.STATE_EDIT
                                If treeOrgFunction.SelectedNode IsNot Nothing Then
                                    objPath = GetUpLevelByNode(treeOrgFunction.SelectedNode)
                                    objOrgFunction.HIERARCHICAL_PATH = objPath.HIERARCHICAL_PATH
                                    objOrgFunction.DESCRIPTION_PATH = objPath.DESCRIPTION_PATH
                                End If
                                objOrgFunction.ID = Decimal.Parse(hidID.Value)
                                Dim vadidate = rep.ValidateFrame_Productivity(objOrgFunction)
                                If Not vadidate Then
                                    ShowMessage("Đã tồn tại mã khung lương", NotifyType.Warning)
                                    Exit Sub
                                End If
                                'objOrgFunction.ID = Decimal.Parse(hidID.Value)
                                'GetDownLevelByNode(treeOrgFunction.Nodes(0), lstPath)
                                'objOrgFunction.DESCRIPTION_PATH = objPath.DESCRIPTION_PATH.Replace(treeOrgFunction.SelectedNode.Text, txtCode.Text + " - " + txtNameVN.Text)
                                If rep.ModifyFrame_Productivity(objOrgFunction, gID) Then
                                    Refresh("UpdateView")
                                    GetDownLevelByNode(treeOrgFunction.Nodes(0), lstPath)
                                    ' số bản ghi cập nhật
                                    Dim iSoBanGhi As Integer = 50
                                    ' số dư để làm tròn
                                    Dim iSoDu As Integer = lstPath.Count Mod iSoBanGhi
                                    Dim iTongVongLap As Integer
                                    ' số vòng lặp khi làm tròn vs số bản ghi cập nhật
                                    If iSoDu = 0 Then
                                        iTongVongLap = (lstPath.Count - iSoDu) / iSoBanGhi
                                    Else
                                        iTongVongLap = ((lstPath.Count - iSoDu) / iSoBanGhi) + 1
                                    End If

                                    For item = 0 To iTongVongLap - 1
                                        ' cập nhật từng đợt ( tối đa = số bản ghi cập nhật )
                                        Dim lstUpdate As New List(Of PA_FRAME_PRODUCTIVITY_PATHDTO)

                                        If item <> iTongVongLap - 1 Then
                                            For idx = item * iSoBanGhi To (item + 1) * iSoBanGhi - 1
                                                lstUpdate.Add(lstPath(idx))
                                            Next
                                        Else
                                            For idx = item * iSoBanGhi To lstPath.Count - 1
                                                lstUpdate.Add(lstPath(idx))
                                            Next
                                        End If
                                        ' cập nhật bản ghi
                                        rep.ModifyFrame_ProductivityPath(lstUpdate)
                                    Next

                                    CurrentState = CommonMessage.STATE_NORMAL

                                    Common.Common.OrganizationLocationDataSession = Nothing
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select
                        If gID <> 0 Then
                            If ImageFile IsNot Nothing Then
                                Dim filePath = AppDomain.CurrentDomain.BaseDirectory & "ReportTemplates\Profile\Organization\Logo\"
                                If Not Directory.Exists(filePath) Then
                                    Directory.CreateDirectory(filePath)
                                End If
                                Dim fileName As String = gID & "_" & ImageFile.GetName
                                Dim dirs As String() = Directory.GetFiles(filePath, gID & "_*")
                                If dirs.Length > 0 Then
                                    For Each str As String In dirs
                                        If File.Exists(str) Then
                                            Try
                                                File.Delete(str)
                                            Catch ex As Exception
                                            End Try
                                        End If
                                    Next
                                End If
                                ImageFile.SaveAs(filePath & fileName)
                                'rbiEmployeeImage.Visible = True
                            End If
                            If LicenseFile IsNot Nothing Then
                                Dim filePath = AppDomain.CurrentDomain.BaseDirectory & "ReportTemplates\Profile\Organization\License\"
                                If Not Directory.Exists(filePath) Then
                                    Directory.CreateDirectory(filePath)
                                End If
                                Dim fileName As String = gID & "_" & LicenseFile.GetName
                                Dim dirs As String() = Directory.GetFiles(filePath, gID & "_*")
                                If dirs.Length > 0 Then
                                    For Each str As String In dirs
                                        If File.Exists(str) Then
                                            Try
                                                File.Delete(str)
                                            Catch ex As Exception
                                            End Try
                                        End If
                                    Next
                                End If
                                LicenseFile.SaveAs(filePath & fileName)
                                'btnDownFile.Visible = True
                            End If
                        End If
                End Select
                rep.Dispose()
                UpdateToolbarState(CurrentState)
                UpdateControlState()
                'FillDataByTree()
                check = ""
                If CurrentState.ToUpper() = "NEW" Then
                    ClearControlValue(txtCode, txtCoefficient)
                    'chkOrgChart.Checked = True
                    txtNameVN.Text = ""
                    txtNameEN.Text = ""
                    txtCode.Text = ""
                    txtREMARK.Text = ""
                    GetDataCombo()
                End If
                _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
            Catch ex As Exception
                DisplayException(Me.ViewName, Me.ID, ex)
                _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            End Try
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    Public Sub ctrlOrganization_OnReceiveData(ByVal sender As IViewListener(Of ViewBase), ByVal e As ViewCommunicationEventArgs) Handles Me.OnReceiveData
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim mess As MessageDTO = CType(e.EventData, MessageDTO)
            Select Case e.FromViewID
                Case "ctrlOrganizationNew"
                    Select Case mess.Mess
                        Case CommonMessage.ACTION_INSERTED
                            CurrentState = CommonMessage.STATE_NEW
                            Refresh("InsertView")
                            UpdateControlState()
                        Case CommonMessage.TOOLBARITEM_CANCEL
                            CurrentState = CommonMessage.STATE_NORMAL
                            UpdateControlState()
                    End Select
                Case "ctrlOrganizationEdit"
                    Select Case mess.Mess
                        Case CommonMessage.ACTION_UPDATED
                            CurrentState = CommonMessage.STATE_NORMAL
                            Refresh("UpdateView")
                            UpdateControlState()
                        Case CommonMessage.TOOLBARITEM_CANCEL
                            CurrentState = CommonMessage.STATE_NORMAL
                            UpdateControlState()
                    End Select
            End Select
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                If ActiveOrganizations(0).ACTFLG = "A" Then
                    CurrentState = CommonMessage.STATE_DEACTIVE
                Else
                    CurrentState = CommonMessage.STATE_ACTIVE
                End If
                UpdateControlState()
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    Protected Sub cbDissolve_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbDissolve.CheckedChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            SelectOrgFunction = treeOrgFunction.SelectedValue
            BuildTreeNode(treeOrgFunction, Organizations, cbDissolve.Checked)
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")

        End Try
    End Sub
    Private Sub chkIslevel3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkIslevel1.CheckedChanged, chkIslevel2.CheckedChanged, chkIslevel3.CheckedChanged
        If chkIslevel3.Checked Then
            txtCoefficient.Enabled = True
            txtCoefficient.ReadOnly = False
            Asterrik_Coefficient.Visible = True
            reqCoefficient.Enabled = True
        Else
            txtCoefficient.Enabled = False
            txtCoefficient.ReadOnly = True
            Asterrik_Coefficient.Visible = False
            reqCoefficient.Enabled = False
        End If
    End Sub
    Private Sub btnCancel_Click(sender As Object, e As System.EventArgs) Handles button.Click
        chkIslevel3_CheckedChanged(Nothing, Nothing)
    End Sub
    Protected Sub treeOrgFunction_NodeClick(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadTreeNodeEventArgs) Handles treeOrgFunction.NodeClick
        Dim lstPath As New List(Of PA_FRAME_PRODUCTIVITY_PATHDTO)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If treeOrgFunction.SelectedNode Is Nothing Then
                Exit Sub
            End If
            GetUpLevelByNode(treeOrgFunction.SelectedNode)
            hidID.Value = treeOrgFunction.SelectedNode.Value
            SelectOrgFunction = treeOrgFunction.SelectedNode.Value
            treeOrgFunction.SelectedNode.ExpandParentNodes()
            FillDataByTree()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub



    Private Sub ctrlFindEmployeePopup_CancelClick(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles ctrlFindEmployeePopup.CancelClicked
        isLoadPopup = 0
    End Sub

    Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        'Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            lstCommonEmployee = CType(ctrlFindEmployeePopup.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            If lstCommonEmployee.Count <> 0 Then
                Dim item = lstCommonEmployee(0)
                hidRepresentative.Value = item.ID.ToString
                'lblChucDanh.Text = item.TITLE_NAME
                DisplayImage(item.ID, item.IMAGE)
            End If
            isLoadPopup = 0
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Custom"

    Public Overrides Sub UpdateControlState()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New PayrollRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            Select Case isLoadPopup
                Case 1
                    If Not phPopup.Controls.Contains(ctrlFindEmployeePopup) Then
                        ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                        phPopup.Controls.Add(ctrlFindEmployeePopup)
                        ctrlFindEmployeePopup.MultiSelect = False
                        ctrlFindEmployeePopup.MustHaveContract = False
                        ctrlFindEmployeePopup.LoadAllOrganization = True
                    End If
            End Select
            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                Case CommonMessage.STATE_NORMAL
                    UpdateToolbarState(CurrentState)
                Case CommonMessage.STATE_EDIT

                Case CommonMessage.STATE_DEACTIVE
                    If rep.ActiveFrame_Productivity(ActiveOrganizations, "I") Then
                        ActiveOrganizations = Nothing
                        Refresh("UpdateView")
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                    End If
                Case CommonMessage.STATE_ACTIVE
                    If rep.ActiveFrame_Productivity(ActiveOrganizations, "A") Then
                        ActiveOrganizations = Nothing
                        Refresh("UpdateView")
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                    End If
            End Select
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    Private Sub UpdateToolbarState(ByVal sState As String)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case sState
                Case CommonMessage.STATE_NORMAL


                    txtNameVN.ReadOnly = True
                    txtNameVN.CausesValidation = False
                    txtNameEN.ReadOnly = True
                    txtCode.CausesValidation = False
                    txtCode.ReadOnly = True

                    txtREMARK.ReadOnly = True
                    chkIslevel1.Enabled = False
                    chkIslevel2.Enabled = False
                    chkIslevel3.Enabled = False
                    treeOrgFunction.Enabled = True
                    cbDissolve.Enabled = True
                    EnableControlAll(False, txtCoefficient)

                Case CommonMessage.STATE_NEW
                    UpdateToolbarState(CommonMessage.STATE_EDIT)

                    txtCode.Enabled = True
                    txtCode.Text = ""
                    txtREMARK.Text = ""
                    check = "EDITFILE"
                    chkIslevel1.Checked = False
                    chkIslevel2.Checked = False
                    chkIslevel3.Checked = False
                    txtREMARK.Text = ""
                    ClearControlValue(txtCoefficient)
                    'EnableControlAll(True, txtCoefficient, txtPromote_month)
                Case (CommonMessage.STATE_EDIT)
                    cbDissolve.Enabled = False
                    treeOrgFunction.Enabled = False
                    txtCode.ReadOnly = False
                    'txtNextCode.ReadOnly = False
                    txtNameVN.ReadOnly = False
                    txtNameEN.ReadOnly = False
                    txtREMARK.ReadOnly = False
                    chkIslevel1.Enabled = True
                    chkIslevel2.Enabled = True
                    chkIslevel3.Enabled = True
                    'EnableControlAll(True, txtCoefficient, txtPromote_month)
                Case "Nothing"
                    txtCode.ReadOnly = False
                    txtCode.Text = ""
                    txtNameVN.Text = ""
                    hidID.Value = ""
                    hidParentID.Value = ""
                    hidRepresentative.Value = ""
                    txtParent_Name.Text = ""
                    txtREMARK.Text = ""
                    ClearControlValue(txtCoefficient)
                    EnableControlAll(False, txtCoefficient)
                    _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
            End Select
            ChangeToolbarState()
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    Protected Sub BuildTreeNode(ByVal tree As RadTreeView,
                                ByVal list As List(Of PA_FRAME_PRODUCTIVITYDTO),
                                ByVal bCheck As Boolean)
        Dim node As New RadTreeNode
        Dim bSelected As Boolean = False
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try

            tree.Nodes.Clear()
            If list.Count = 0 Then
                Exit Sub
            End If
            Dim listTemp = (From t In list
                            Where t.PARENT_ID Is Nothing Select t).FirstOrDefault
            Select Case Common.Common.SystemLanguage.Name
                Case "vi-VN"
                    node.Text = listTemp.NAME_VN
                    node.ToolTip = listTemp.NAME_VN
                Case Else
                    node.Text = listTemp.NAME_EN
                    node.ToolTip = listTemp.NAME_EN
            End Select

            node.Value = listTemp.ID.ToString
            If SelectOrgFunction IsNot Nothing Then
                If node.Value = SelectOrgFunction Then
                    node.Selected = True
                    bSelected = True
                End If

            End If

            tree.Nodes.Add(node)
            BuildTreeChildNode(node, list, bCheck, bSelected)
            'tree.ExpandAllNodes()
            If SelectOrgFunction = "" Then
                If tree.Nodes.Count > 0 Then
                    If tree.Nodes(0).Nodes.Count > 0 Then
                        tree.Nodes(0).Nodes(0).Selected = True
                        tree.Nodes(0).ExpandParentNodes()
                        treeOrgFunction_NodeClick(Nothing, Nothing)
                        bSelected = True
                    End If
                End If
            Else
                Dim treeNode As RadTreeNode = Nothing
                For Each n As RadTreeNode In tree.Nodes
                    treeNode = getNodeSelected(n)
                    If treeNode IsNot Nothing Then
                        Exit For
                    End If
                Next
                If treeNode IsNot Nothing Then
                    treeNode.Selected = True
                    treeNode.ExpandParentNodes()

                End If
            End If
            ' Nếu không có node nào được chọn thì load lại control
            If Not bSelected Then
                SelectOrgFunction = ""
                UpdateToolbarState("Nothing")
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Function getNodeSelected(ByVal node As RadTreeNode) As RadTreeNode
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If node.Value = SelectOrgFunction Then
                Return node
            End If
            For Each n As RadTreeNode In node.Nodes
                If n.Value = SelectOrgFunction Then
                    Return n
                End If
                Dim _node As RadTreeNode = getNodeSelected(n)
                If _node IsNot Nothing Then
                    Return _node
                End If
            Next
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
        Return Nothing
    End Function

    Protected Sub BuildTreeChildNode(ByVal nodeParent As RadTreeNode,
                                     ByVal list As List(Of PA_FRAME_PRODUCTIVITYDTO),
                                     ByVal bCheck As Boolean,
                                     ByRef bSelected As Boolean)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Dim listTemp As List(Of PA_FRAME_PRODUCTIVITYDTO)
        Try
            If bCheck Then
                listTemp = (From t In list
                            Where t.PARENT_ID = Decimal.Parse(nodeParent.Value)).ToList
            Else
                listTemp = (From t In list
                            Where t.PARENT_ID = Decimal.Parse(nodeParent.Value) _
                            And t.ACTFLG = "A").ToList
            End If

            If listTemp.Count = 0 Then
                Exit Sub
            End If
            For index As Integer = 0 To listTemp.Count - 1
                Dim node As New RadTreeNode
                Select Case Common.Common.SystemLanguage.Name
                    Case "vi-VN"
                        'node.Text = listTemp(index).CODE & " - " & listTemp(index).NAME_VN
                        node.Text = listTemp(index).NAME_VN
                        node.ToolTip = listTemp(index).NAME_VN
                    Case Else
                        'node.Text = listTemp(index).CODE & " - " & listTemp(index).NAME_EN
                        node.Text = listTemp(index).NAME_EN
                        node.ToolTip = listTemp(index).NAME_EN
                End Select

                node.Value = listTemp(index).ID.ToString
                If listTemp(index).ACTFLG = "I" Then
                    node.BackColor = Drawing.Color.Yellow
                End If
                If SelectOrgFunction IsNot Nothing Then
                    If node.Value = SelectOrgFunction Then
                        node.Selected = True
                        bSelected = True
                    End If
                End If
                nodeParent.Nodes.Add(node)
                BuildTreeChildNode(node, list, bCheck, bSelected)
            Next
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <summary>
    ''' Author: TUNGNT
    ''' Des: chinh sua theo tinh nang man hinh moi
    ''' Date: 13/05/2018
    ''' </summary>
    Private Sub FillDataByTree()
        Dim orgItem As PA_FRAME_PRODUCTIVITYDTO
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            ClearControlValue(txtCode, txtNameEN, txtNameVN, txtParent_Name, txtREMARK, txtCoefficient)
            chkIslevel1.Checked = False
            chkIslevel2.Checked = False
            chkIslevel3.Checked = False
            If treeOrgFunction.SelectedNode Is Nothing Then
                Exit Sub
            End If
            orgItem = (From p In Organizations Where p.ID = Decimal.Parse(treeOrgFunction.SelectedNode.Value)).SingleOrDefault
            If orgItem IsNot Nothing Then
                hidParentID.Value = orgItem.PARENT_ID.ToString
                txtParent_Name.Text = orgItem.PARENT_NAME
                txtCode.Text = orgItem.CODE
                If IsNumeric(orgItem.COEFFICIENT) Then
                    txtCoefficient.Value = Decimal.Parse(orgItem.COEFFICIENT)
                End If

                txtNameVN.Text = orgItem.NAME_VN
                txtNameEN.Text = orgItem.NAME_EN
                If IsNumeric(orgItem.IS_LEVEL1) AndAlso orgItem.IS_LEVEL1 <> 0 Then
                    chkIslevel1.Checked = True
                Else
                    chkIslevel1.Checked = False
                End If
                If IsNumeric(orgItem.IS_LEVEL2) AndAlso orgItem.IS_LEVEL2 <> 0 Then
                    chkIslevel2.Checked = True
                Else
                    chkIslevel2.Checked = False
                End If
                If IsNumeric(orgItem.IS_LEVEL3) AndAlso orgItem.IS_LEVEL3 <> 0 Then
                    chkIslevel3.Checked = True
                Else
                    chkIslevel3.Checked = False
                End If
                txtREMARK.Text = orgItem.REMARK
                'If treeOrgFunction.SelectedNode.Level = 1 Then
                '    Dim logoPath = AppDomain.CurrentDomain.BaseDirectory & "ReportTemplates\Profile\Organization\Logo\"
                '    If Not Directory.Exists(logoPath) Then
                '        Directory.CreateDirectory(logoPath)
                '    End If
                '    Dim logo = treeOrgFunction.SelectedNode.Value & "_*"
                '    Dim dirs As String() = Directory.GetFiles(logoPath, logo)
                '    Dim licensePath = AppDomain.CurrentDomain.BaseDirectory & "ReportTemplates\Profile\Organization\License\"
                '    If Not Directory.Exists(licensePath) Then
                '        Directory.CreateDirectory(licensePath)
                '    End If
                '    Dim license = treeOrgFunction.SelectedNode.Value & "_*"
                '    dirs = Directory.GetFiles(logoPath, license)
                'End If
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    Private Sub DisplayImage(ByVal empid As String, ByVal image As String)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            Dim startTime As DateTime = DateTime.UtcNow
            'Dim rep As New ProfileBusinessRepository
            Dim sError As String = ""
            If image IsNot Nothing Then
                'rbiImage.DataValue = rep.GetEmployeeImage(empid, sError) 'Lấy ảnh của nhân viên.
            Else
                'rbiImage.DataValue = rep.GetEmployeeImage(0, sError) 'Lấy ảnh mặc định (NoImage.jpg)
            End If
            Exit Sub
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Function GetUpLevelByNode(ByVal node As RadTreeNode) As PA_FRAME_PRODUCTIVITY_PATHDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Dim strText As String = ""
        Dim strValue As String = ""
        Dim iLevel As Integer
        Dim curNode As RadTreeNode
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            curNode = node
            iLevel = curNode.Level

            For idx = 0 To iLevel
                If idx = 0 Then
                    strValue = curNode.Value
                    strText = curNode.Text
                Else
                    curNode = curNode.ParentNode
                    strValue = curNode.Value + ";" + strValue
                    strText = curNode.Text + ";" + strText
                End If
            Next

            Return New PA_FRAME_PRODUCTIVITY_PATHDTO With {.ID = Decimal.Parse(node.Value),
                                                 .HIERARCHICAL_PATH = strValue,
                                                 .DESCRIPTION_PATH = strText}
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Function

    Private Sub GetDownLevelByNode(ByVal node As RadTreeNode, ByRef lstPath As List(Of PA_FRAME_PRODUCTIVITY_PATHDTO))
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If node.Nodes.Count > 0 Then
                For Each child As RadTreeNode In node.Nodes
                    Dim objPath = GetUpLevelByNode(child)
                    objPath.DESCRIPTION_PATH = objPath.DESCRIPTION_PATH.Replace(treeOrgFunction.SelectedNode.Text, txtNameVN.Text)
                    lstPath.Add(objPath)
                    GetDownLevelByNode(child, lstPath)
                Next

            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Public Function GetAllChild(ByVal _orgId As Decimal) As List(Of Decimal)
        Dim query As List(Of Decimal)
        Dim list As List(Of Decimal)
        Dim result As New List(Of Decimal)
        'Dim rep As New CommonRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try

            result.Add(_orgId)
            Dim lstOrgs As List(Of PA_FRAME_PRODUCTIVITYDTO) = Organizations

            query = (From p In lstOrgs Where p.PARENT_ID = _orgId
                     Select p.ID).ToList
            For Each q As Decimal In query
                list = GetAllChild(q)
                result.InsertRange(0, list)
            Next
            Return result
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Function

    Private Sub ctrlUpload_OkClicked(sender As Object, e As System.EventArgs) Handles ctrlUpload.OkClicked
        Try
            'If ctrlUpload.UploadedFiles.Count > 0 Then
            '    For i As Integer = 0 To ctrlUpload.UploadedFiles.Count - 1
            '        Dim fileName As String = String.Empty
            '        Dim file As UploadedFile = ctrlUpload.UploadedFiles(i)
            '        fileName = Server.MapPath("~/ReportTemplates/Training/Upload")
            '        If Not Directory.Exists(fileName) Then
            '            Directory.CreateDirectory(fileName)
            '        End If
            '        'New System.IO.FileInfo(System.IO.Path.Combine(Server.MapPath("~/ReportTemplates/Training/Upload/"), strFiles.Split("#")(i)))
            '        If isPhysical = 1 Then
            '            fileName = System.IO.Path.Combine(fileName, file.FileName)
            '            'fileName = System.IO.Path.Combine(Server.MapPath("~/ReportTemplates/Training/Upload/"), file.FileName)
            '        Else
            '            fileName = System.IO.Path.Combine(Server.MapPath("~/ReportTemplates/Training/Upload/"), file.FileName)
            '        End If

            '        file.SaveAs(fileName, True)
            '        Dim item As New RadListBoxItem(file.FileName, file.FileName)
            '        item.Checked = True
            '        lstFile.Items.Add(item)
            '    Next
            'End If
        Catch ex As Exception
            ShowMessage(Translate("Import bị lỗi"), NotifyType.Error)
        End Try
    End Sub


#End Region

End Class