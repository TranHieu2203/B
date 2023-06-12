Imports System.IO
Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Ionic.Zip
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlHU_OrganizationHR
    Inherits Common.CommonView
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Private procedure As ProfileStoreProcedure
    Dim dtOrgLevel As DataTable = Nothing
    Dim dtRegion As DataTable = Nothing
    Dim dtIsunace As DataTable = Nothing
    Public isPhysical As Decimal = Decimal.Parse(ConfigurationManager.AppSettings("PHYSICAL_PATH"))

#Region "Property"

    Property Organization As OrganizationDTO
        Get
            Return ViewState(Me.ID & "_Organization")
        End Get
        Set(ByVal value As OrganizationDTO)
            ViewState(Me.ID & "_Organization") = value
        End Set
    End Property

    Public Property Organizations As List(Of OrganizationDTO)
        Get
            Return ViewState(Me.ID & "_Organizations")
        End Get
        Set(ByVal value As List(Of OrganizationDTO))
            ViewState(Me.ID & "_Organizations") = value
        End Set
    End Property

    Property ActiveOrganizations As List(Of OrganizationDTO)
        Get
            Return ViewState(Me.ID & "_ActiveOrganizations")
        End Get
        Set(ByVal value As List(Of OrganizationDTO))
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
            Dim lstOrganization As List(Of OrganizationDTO)
            Dim rep As New ProfileRepository
            SelectOrgFunction = String.Empty
            Try
                Dim startTime As DateTime = DateTime.UtcNow
                If Not IsPostBack Then
                    Dim callFunction = Common.CommonRepository.GetOrganizationLocationTreeView()
                    lstOrganization = rep.GetOrganization()
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
                            lstOrganization = rep.GetOrganization()
                            Dim lstorgper = (From p In Common.Common.OrganizationLocationDataSession Select p.ID).ToList()
                            Dim lst = (From p In lstOrganization Where lstorgper.Contains(p.ID)).ToList()
                            Me.Organizations = lst
                            CurrentState = CommonMessage.STATE_NORMAL
                            UpdateToolbarState(CurrentState)
                        Case "InsertView"
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            Dim callFunction = Common.CommonRepository.GetOrganizationLocationTreeView()
                            lstOrganization = rep.GetOrganization()
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

    ''' <summary>
    ''' Author: ChienNV,Edit date:11/10/2017
    ''' FillRadCombobox(cboU_insurance)
    ''' FillRadCombobox(cboRegion)
    ''' FillRadCombobox(cboOrg_level)
    ''' Author Edit/Modify: TUNGNT
    ''' Des: Binding data cho combobox dv BH, vung BH, danh sach cap don vi
    ''' Date Edit/Modify: 13/05/2018
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetDataCombo()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            If procedure Is Nothing Then
                procedure = New ProfileStoreProcedure()
            End If
            'Lấy danh sách dv đóng bảo hiểm.
            dtIsunace = procedure.GetListInsurance()
            If Not dtIsunace Is Nothing AndAlso dtIsunace.Rows.Count > 0 Then
                FillRadCombobox(cboU_insurance, dtIsunace, "NAME", "ID")
            End If
            'Lấy danh sách vùng bảo hiểm
            dtRegion = procedure.GetInsListRegion()
            If Not dtRegion Is Nothing AndAlso dtRegion.Rows.Count > 0 Then
                FillRadCombobox(cboRegion, dtRegion, "REGION_NAME", "ID")
                Dim id = (From p In dtRegion Where Not IsDBNull(p("ATTRIBUTE1")) AndAlso p("ATTRIBUTE1").ToString.Contains("1")).FirstOrDefault
                If id IsNot Nothing Then
                    cboRegion.SelectedValue = id("ID").ToString
                End If
            End If
            'Lấy danh sách cấp đơn vị
            dtOrgLevel = procedure.GET_ORG_LEVEL()
            If Not dtOrgLevel Is Nothing AndAlso dtOrgLevel.Rows.Count > 0 Then
                FillRadCombobox(cboOrg_level, dtOrgLevel, "NAME_VN", "ID")
            End If
            Dim rep As New ProfileRepository
            Dim UNIT_LEVEL As New ComboBoxDataDTO
            UNIT_LEVEL.GET_UNIT_LEVEL = True
            Dim isUnitlevel = rep.GetComboList(UNIT_LEVEL)
            If isUnitlevel AndAlso cbUNIT_LEVEL.Items.Count < 1 Then
                'FillRadCombobox(cbUNIT_LEVEL, UNIT_LEVEL.LIST_UNIT_LEVEL, "NAME_VN", "ID")
                FillDropDownList(cbUNIT_LEVEL, UNIT_LEVEL.LIST_UNIT_LEVEL, "NAME_VN", "ID")
            End If
            Dim ORG_REG As New ComboBoxDataDTO
            ORG_REG.GET_ORG_REG = True
            Dim isOrgReg = rep.GetComboList(ORG_REG)
            If isOrgReg AndAlso cboOrgReg.Items.Count < 1 Then
                FillDropDownList(cboOrgReg, ORG_REG.LIST_ORG_REG, "NAME_VN", "ID")
            End If
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
            Dim objOrgFunction As New OrganizationDTO

            Dim gID As Decimal
            Dim rep As New ProfileRepository
            Try
                Dim startTime As DateTime = DateTime.UtcNow
                Select Case CType(e.Item, RadToolBarButton).CommandName
                    Case CommonMessage.TOOLBARITEM_CREATE
                        If Organizations.Count > 0 Then
                            If treeOrgFunction.SelectedNode Is Nothing Then
                                ShowMessage("Chưa chọn phòng ban?", NotifyType.Warning)
                                Exit Sub
                            End If
                            txtParent_Name.Text = (From p In Organizations Where p.ID = Decimal.Parse(treeOrgFunction.SelectedNode.Value)).SingleOrDefault.NAME_VN
                        End If
                        CurrentState = CommonMessage.STATE_NEW
                        txtCode.Focus()
                        'tr01.Visible = False
                        'tr02.Visible = False
                        cbUNIT_LEVEL.ClearValue()
                        objOrgFunction.REPRESENTATIVE_ID = Nothing
                        hidRepresentative.Value = Nothing
                        If treeOrgFunction.SelectedNode.Level = 0 Then
                            'tr01.Visible = True
                            'tr02.Visible = True
                        End If
                    Case CommonMessage.TOOLBARITEM_EDIT
                        If treeOrgFunction.SelectedNode Is Nothing Then
                            ShowMessage("Chưa chọn phòng ban?", NotifyType.Warning)
                            Exit Sub
                        End If

                        Organization = (From p In Organizations Where p.ID = Decimal.Parse(treeOrgFunction.SelectedNode.Value)).SingleOrDefault
                        CurrentState = CommonMessage.STATE_EDIT
                        txtNameVN.Focus()
                    Case CommonMessage.TOOLBARITEM_ACTIVE, CommonMessage.TOOLBARITEM_DEACTIVE
                        Dim sActive As String
                        If treeOrgFunction.SelectedNode Is Nothing Then
                            ShowMessage("Chưa chọn phòng ban?", NotifyType.Warning)
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
                                If Not rep.CheckEmployeeInOrganization(GetAllChild(ActiveOrganizations(0).ID)) Then
                                    ShowMessage("Bạn phải điều chuyển toàn bộ nhân viên trước khi giải thể.", NotifyType.Warning)
                                    Exit Sub
                                End If
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
                        Try
                            strFiles = lstFile.CheckedItems.Select(Function(x) x.Text).Aggregate(Function(x, y) x & "# " & y)
                        Catch ex As Exception
                            strFiles = ""
                        End Try
                        objOrgFunction.CODE = txtCode.Text

                        objOrgFunction.SHORT_NAME = txtShortName.Text
                        objOrgFunction.INFOR_1 = txtInfor1.Text
                        objOrgFunction.INFOR_2 = txtInfor2.Text
                        objOrgFunction.INFOR_3 = txtInfor3.Text
                        objOrgFunction.INFOR_4 = txtInfor4.Text
                        objOrgFunction.INFOR_5 = txtInfor5.Text

                        objOrgFunction.FAX = txtFAX.Text
                        objOrgFunction.MOBILE = txtMobliePhone.Text
                        'objOrgFunction.COST_CENTER_CODE = txtCost_center_code.Text
                        objOrgFunction.PIT_NO = rtNUMBER_BUSINESS.Text
                        objOrgFunction.EMAIL = txtEmail.Text
                        objOrgFunction.COST_CENTER_CODE = txtCost_center_code.Text
                        objOrgFunction.NUMBER_DECISION = txtNumberDecision.Text
                        objOrgFunction.TYPE_DECISION = txtTypeDecision.Text
                        objOrgFunction.LOCATION_WORK = txtLocationWork.Text
                        objOrgFunction.NAME_EN = txtNameEN.Text
                        objOrgFunction.NAME_VN = txtNameVN.Text
                        objOrgFunction.REMARK = txtREMARK.Text
                        objOrgFunction.ADDRESS = rtADDRESS.Text
                        objOrgFunction.FILES = strFiles
                        objOrgFunction.NUMBER_BUSINESS = rtNUMBER_BUSINESS.Text
                        objOrgFunction.DATE_BUSINESS = rdDATE_BUSINESS.SelectedDate
                        Dim ISURANCE As Decimal = 0.0
                        Dim REGION As Decimal = 0.0
                        Dim ORG_LEVEL As Decimal = 0.0
                        If IsNumeric(cbUNIT_LEVEL.SelectedValue) Then
                            objOrgFunction.UNIT_LEVEL = cbUNIT_LEVEL.SelectedValue
                        End If
                        If IsNumeric(cboOrgReg.SelectedValue) Then
                            objOrgFunction.ORG_REG_ID = cboOrgReg.SelectedValue
                        End If
                        If IsDate(rdEffectDate.SelectedDate) Then
                            objOrgFunction.EFFECT_DATE = rdEffectDate.SelectedDate
                        End If
                        If IsDate(rdDATE_BUSINESS.SelectedDate) Then
                            objOrgFunction.DATE_BUSINESS = rdDATE_BUSINESS.SelectedDate
                        End If
                        If IsDate(rdFOUNDATION_DATE.SelectedDate) Then
                            objOrgFunction.FOUNDATION_DATE = rdFOUNDATION_DATE.SelectedDate
                        End If

                        If IsNumeric(rdOrdNo.Value) Then
                            objOrgFunction.ORD_NO = rdOrdNo.Value
                        Else
                            objOrgFunction.ORD_NO = 0
                        End If

                        If IsDate(rdDicision_Date.SelectedDate) Then
                            objOrgFunction.DISSOLVE_DATE = rdDicision_Date.SelectedDate
                        End If
                        If chkOrgChart.Checked = True Then
                            objOrgFunction.CHK_ORGCHART = True
                        Else
                            objOrgFunction.CHK_ORGCHART = False
                        End If
                        If Decimal.TryParse(cboU_insurance.SelectedValue.ToString, ISURANCE) Then
                            objOrgFunction.U_INSURANCE = cboU_insurance.SelectedValue
                        Else
                            objOrgFunction.U_INSURANCE = ISURANCE
                        End If
                        If Decimal.TryParse(cboRegion.SelectedValue.ToString, REGION) Then
                            objOrgFunction.REGION_ID = cboRegion.SelectedValue
                        Else
                            objOrgFunction.REGION_ID = REGION
                        End If
                        If Decimal.TryParse(cboOrg_level.SelectedValue.ToString, ORG_LEVEL) Then
                            objOrgFunction.ORG_LEVEL = cboOrg_level.SelectedValue
                        Else
                            objOrgFunction.ORG_LEVEL = ORG_LEVEL
                        End If
                        If hidRepresentative.Value = "" Then
                            objOrgFunction.REPRESENTATIVE_ID = Nothing
                        Else
                            objOrgFunction.REPRESENTATIVE_ID = hidRepresentative.Value
                        End If
                        Dim objPath As OrganizationPathDTO
                        'If treeOrgFunction.SelectedNode IsNot Nothing Then
                        '    objPath = GetUpLevelByNode(treeOrgFunction.SelectedNode)
                        '    objOrgFunction.HIERARCHICAL_PATH = objPath.HIERARCHICAL_PATH
                        '    objOrgFunction.DESCRIPTION_PATH = objPath.DESCRIPTION_PATH
                        'End If
                        Dim lstPath As New List(Of OrganizationPathDTO)
                        If hidID.Value IsNot Nothing Then
                            If hidID.Value <> "" Then
                                objOrgFunction.PARENT_ID = Decimal.Parse(hidID.Value)
                            End If
                        End If
                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                If objOrgFunction.PARENT_ID = 0 Then
                                    If Organizations.Select(Function(p) p.PARENT_ID = 0).Count > 0 Then
                                        ShowMessage("Đã tồn tại phòng ban cao nhất?", NotifyType.Warning)
                                        Exit Sub
                                    End If
                                End If
                                If objOrgFunction.PARENT_ID = 1 Then
                                    Dim COUNT = (From p In Organizations Where p.PARENT_ID = 1).Count
                                    If COUNT >= (CommonConfig.SETUP_NUM_ORG()) Then
                                        ShowMessage("Số lượng Công ty hoặc chi nhánh vượt quá số lượng cho phép", NotifyType.Warning)
                                        Exit Sub
                                    End If
                                End If
                                objOrgFunction.ACTFLG = "A"
                                If treeOrgFunction.Nodes.Count > 0 Then
                                    GetDownLevelByNode(treeOrgFunction.Nodes(0), lstPath)
                                End If
                                Dim id As Decimal
                                id = rep.GetMaxId()
                                id = id + 1
                                If treeOrgFunction.SelectedNode IsNot Nothing Then
                                    objPath = GetUpLevelByNode(treeOrgFunction.SelectedNode)
                                    objOrgFunction.HIERARCHICAL_PATH = objPath.HIERARCHICAL_PATH + ";" + id.ToString
                                    ' objOrgFunction.DESCRIPTION_PATH = objPath.DESCRIPTION_PATH
                                    Dim mang()
                                    Dim str As String = ""
                                    mang = objPath.HIERARCHICAL_PATH.Split(";")
                                    For Each line In mang
                                        Dim chuoi = rep.GetNameOrg(line)
                                        str += chuoi + ";"
                                    Next
                                    str += txtNameVN.Text
                                    objOrgFunction.DESCRIPTION_PATH = str
                                End If
                                'GetDownLevelByNode(treeOrgFunction.Nodes(0), lstPath)
                                If rep.InsertOrganization(objOrgFunction, gID) Then
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
                                'objOrgFunction.ID = Decimal.Parse(hidID.Value)
                                'GetDownLevelByNode(treeOrgFunction.Nodes(0), lstPath)
                                'objOrgFunction.DESCRIPTION_PATH = objPath.DESCRIPTION_PATH.Replace(treeOrgFunction.SelectedNode.Text, txtCode.Text + " - " + txtNameVN.Text)
                                If rep.ModifyOrganization(objOrgFunction, gID) Then
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
                                        Dim lstUpdate As New List(Of OrganizationPathDTO)

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
                                        rep.ModifyOrganizationPath(lstUpdate)
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
                    chkOrgChart.Checked = True
                    txtNameVN.Text = ""
                    txtNameEN.Text = ""
                    txtRepresentativeName.Text = ""
                    txtCode.Text = ""

                    txtShortName.Text = ""
                    txtInfor1.Text = ""
                    txtInfor2.Text = ""
                    txtInfor3.Text = ""
                    txtInfor4.Text = ""
                    txtInfor5.Text = ""


                    rtNUMBER_BUSINESS.Text = ""
                    rtADDRESS.Text = ""
                    txtLocationWork.Text = ""
                    txtTypeDecision.Text = ""
                    txtNumberDecision.Text = ""
                    rdEffectDate.SelectedDate = Nothing
                    rdFOUNDATION_DATE.SelectedDate = Nothing
                    rdOrdNo.Value = Nothing
                    rdDATE_BUSINESS.SelectedDate = Nothing
                    rdDicision_Date.SelectedDate = Nothing
                    txtREMARK.Text = ""
                    rdOrdNo.Value = Nothing
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

    Protected Sub treeOrgFunction_NodeClick(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadTreeNodeEventArgs) Handles treeOrgFunction.NodeClick
        Dim lstPath As New List(Of OrganizationPathDTO)
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

    ''' <summary>
    ''' Author: TUNGNT
    ''' Des: Show Pop up chon nhan vien quan ly don vi
    ''' Date: 13/05/2018
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Protected Sub btnFindRepresentative_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFindRepresentative.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            Dim startTime As DateTime = DateTime.UtcNow
            isLoadPopup = 1
            UpdateControlState()
            ctrlFindEmployeePopup.Show()
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
                txtRepresentativeName.Text = item.FULLNAME_VN
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
        Dim rep As New ProfileRepository
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
                    ClearControlValue(txtEmail, txtMobliePhone, txtFAX, rtNUMBER_BUSINESS, txtCost_center_code)
                    EnableControlAll(True, txtEmail, txtMobliePhone, txtFAX, rtNUMBER_BUSINESS, txtCost_center_code)
                Case CommonMessage.STATE_NORMAL
                    EnableControlAll(False, txtEmail, txtMobliePhone, txtFAX, rtNUMBER_BUSINESS, txtCost_center_code)
                    UpdateToolbarState(CurrentState)
                Case CommonMessage.STATE_EDIT
                    EnableControlAll(True, txtEmail, txtMobliePhone, txtFAX, rtNUMBER_BUSINESS, txtCost_center_code)
                Case CommonMessage.STATE_DEACTIVE
                    If rep.ActiveOrganization(ActiveOrganizations, "I") Then
                        ActiveOrganizations = Nothing
                        Refresh("UpdateView")
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                    End If
                Case CommonMessage.STATE_ACTIVE
                    If rep.ActiveOrganization(ActiveOrganizations, "A") Then
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

                    lstFile.Items.Clear()
                    txtNameVN.ReadOnly = True
                    txtNameVN.CausesValidation = False
                    txtNameEN.ReadOnly = True
                    txtCode.CausesValidation = False
                    txtCode.ReadOnly = True

                    txtShortName.ReadOnly = True
                    txtInfor1.ReadOnly = True
                    txtInfor2.ReadOnly = True
                    txtInfor3.ReadOnly = True
                    txtInfor4.ReadOnly = True
                    txtInfor5.ReadOnly = True

                    txtREMARK.ReadOnly = True
                    rtADDRESS.ReadOnly = True
                    txtLocationWork.ReadOnly = True
                    txtNumberDecision.ReadOnly = True
                    txtTypeDecision.ReadOnly = True
                    chkOrgChart.Enabled = False
                    btnDownloadFile.Enabled = False
                    btnUploadFile.Enabled = False
                    rtNUMBER_BUSINESS.ReadOnly = True
                    rdDATE_BUSINESS.Enabled = False
                    rdFOUNDATION_DATE.Enabled = False
                    rdEffectDate.Enabled = False
                    rdDicision_Date.Enabled = False
                    rdOrdNo.Enabled = False
                    'txtFax.ReadOnly = True
                    'txtMobile.ReadOnly = True
                    'txtProvinceName.ReadOnly = True
                    'rntxtOrdNo.ReadOnly = True
                    'cboCostCenterCode.Enabled = False
                    'txtNumber_business.ReadOnly = True
                    cboU_insurance.Enabled = False
                    cboOrg_level.Enabled = False
                    cboRegion.Enabled = False
                    cbUNIT_LEVEL.Enabled = False
                    treeOrgFunction.Enabled = True
                    cbDissolve.Enabled = True
                    btnFindRepresentative.Enabled = False
                    cboOrgReg.Enabled = False

                    'AutoGenTimeSheet.Enabled = False
                    '_radAsynceUpload.Enabled = False
                    '_radAsynceUpload1.Enabled = False

                Case CommonMessage.STATE_NEW
                    lstFile.Items.Clear()
                    UpdateToolbarState(CommonMessage.STATE_EDIT)
                    txtCode.Enabled = True
                    txtCode.Text = ""

                    txtShortName.Enabled = True
                    txtInfor1.Enabled = True
                    txtInfor2.Enabled = True
                    txtInfor3.Enabled = True
                    txtInfor4.Enabled = True
                    txtInfor5.Enabled = True

                    txtShortName.Text = ""
                    txtInfor1.Text = ""
                    txtInfor2.Text = ""
                    txtInfor3.Text = ""
                    txtInfor4.Text = ""
                    txtInfor5.Text = ""


                    'AutoGenTimeSheet.Checked = False
                    'txtNameVN.Text = ""
                    txtREMARK.Text = ""
                    rtADDRESS.Text = ""
                    txtLocationWork.Text = ""
                    txtNumberDecision.Text = ""
                    txtTypeDecision.Text = ""
                    check = "EDITFILE"
                    chkOrgChart.Checked = True
                    'txtFax.Text = ""
                    'txtMobile.Text = ""
                    'txtProvinceName.Text = ""
                    'cboCostCenterCode.Text = ""
                    txtRepresentativeName.Text = ""
                    rtADDRESS.Text = ""
                    txtREMARK.Text = ""
                    'txtNumber_business.Text = ""
                    cboU_insurance.Text = ""
                    cboOrg_level.Text = ""
                    cboRegion.Text = ""
                    cboOrgReg.Text = ""

                    'lblChucDanh.Text = ""
                    'rdDate_Business.SelectedDate = Nothing
                    'rntxtOrdNo.Value = Nothing
                    rdEffectDate.SelectedDate = Nothing
                    rdDicision_Date.SelectedDate = Nothing
                    rdFOUNDATION_DATE.SelectedDate = Nothing
                    rdOrdNo.Value = Nothing
                    'AutoGenTimeSheet.Enabled = True
                Case (CommonMessage.STATE_EDIT)
                    cbDissolve.Enabled = False
                    treeOrgFunction.Enabled = False
                    txtCode.ReadOnly = False

                    txtShortName.ReadOnly = False
                    txtInfor1.ReadOnly = False
                    txtInfor2.ReadOnly = False
                    txtInfor3.ReadOnly = False
                    txtInfor4.ReadOnly = False
                    txtInfor5.ReadOnly = False

                    txtNameVN.ReadOnly = False
                    txtNameEN.ReadOnly = False
                    txtREMARK.ReadOnly = False
                    rtADDRESS.ReadOnly = False
                    txtLocationWork.ReadOnly = False
                    txtNumberDecision.ReadOnly = False
                    txtTypeDecision.ReadOnly = False
                    chkOrgChart.Enabled = True
                    btnDownloadFile.Enabled = True
                    btnUploadFile.Enabled = True
                    'txtFax.ReadOnly = False
                    'txtMobile.ReadOnly = False
                    'txtProvinceName.ReadOnly = False
                    'rntxtOrdNo.ReadOnly = False
                    'cboCostCenterCode.Enabled = True
                    btnFindRepresentative.Enabled = True
                    'txtNumber_business.ReadOnly = False
                    cboU_insurance.Enabled = True
                    cboOrg_level.Enabled = True
                    cboRegion.Enabled = True
                    cboOrgReg.Enabled = True
                    cbUNIT_LEVEL.Enabled = True
                    'EnableRadDatePicker(rdDate_Business, True)
                    'AutoGenTimeSheet.Enabled = False
                    EnableRadDatePicker(rdDicision_Date, True)
                    EnableRadDatePicker(rdFOUNDATION_DATE, True)
                    rdOrdNo.Enabled = True
                    EnableRadDatePicker(rdEffectDate, True)
                    '_radAsynceUpload.Enabled = True
                    '_radAsynceUpload1.Enabled = True
                Case "Nothing"
                    txtCode.ReadOnly = False
                    txtCode.Text = ""

                    txtShortName.ReadOnly = False
                    txtInfor1.ReadOnly = False
                    txtInfor2.ReadOnly = False
                    txtInfor3.ReadOnly = False
                    txtInfor4.ReadOnly = False
                    txtInfor5.ReadOnly = False

                    txtShortName.Text = ""
                    txtInfor1.Text = ""
                    txtInfor2.Text = ""
                    txtInfor3.Text = ""
                    txtInfor4.Text = ""
                    txtInfor5.Text = ""

                    txtNameVN.Text = ""
                    'txtRemark.Text = ""
                    'rdDissolveDate.SelectedDate = Nothing
                    'rdFoundationDate.SelectedDate = Nothing
                    hidID.Value = ""
                    hidParentID.Value = ""
                    hidRepresentative.Value = ""
                    txtParent_Name.Text = ""
                    rtADDRESS.Text = ""
                    txtREMARK.Text = ""
                    txtLocationWork.Text = ""
                    txtNumberDecision.Text = ""
                    txtTypeDecision.Text = ""
                    'txtFax.Text = ""
                    'txtMobile.Text = ""
                    'txtProvinceName.Text = ""
                    'cboCostCenterCode.Text = ""
                    txtRepresentativeName.Text = ""
                    'rntxtOrdNo.Value = Nothing
                    'txtNumber_business.Text = ""
                    cboU_insurance.Text = ""
                    cboOrg_level.Text = ""
                    cboRegion.Text = ""
                    cboOrgReg.Text = ""
                    'rdDate_Business.SelectedDate = Nothing
                    _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
            End Select
            ChangeToolbarState()
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    Protected Sub BuildTreeNode(ByVal tree As RadTreeView,
                                ByVal list As List(Of OrganizationDTO),
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
                                     ByVal list As List(Of OrganizationDTO),
                                     ByVal bCheck As Boolean,
                                     ByRef bSelected As Boolean)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Dim listTemp As List(Of OrganizationDTO)
        Try
            If bCheck Then
                listTemp = (From t In list
                            Where t.PARENT_ID = Decimal.Parse(nodeParent.Value)).ToList
            Else
                listTemp = (From t In list
                            Where t.PARENT_ID = Decimal.Parse(nodeParent.Value) _
                            And (t.DISSOLVE_DATE Is Nothing Or (t.DISSOLVE_DATE IsNot Nothing And t.DISSOLVE_DATE >= Date.Now)) _
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
                If bCheck Then
                    If listTemp(index).DISSOLVE_DATE IsNot Nothing Then
                        If listTemp(index).DISSOLVE_DATE < Date.Now Then
                            node.BackColor = Drawing.Color.Yellow
                        End If
                    End If
                End If
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
        Dim orgItem As OrganizationDTO
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If treeOrgFunction.SelectedNode Is Nothing Then
                Exit Sub
            End If
            orgItem = (From p In Organizations Where p.ID = Decimal.Parse(treeOrgFunction.SelectedNode.Value)).SingleOrDefault
            If orgItem IsNot Nothing Then
                hidParentID.Value = orgItem.PARENT_ID.ToString
                txtParent_Name.Text = orgItem.PARENT_NAME
                txtCode.Text = orgItem.CODE

                txtShortName.Text = orgItem.SHORT_NAME
                txtInfor1.Text = orgItem.INFOR_1
                txtInfor2.Text = orgItem.INFOR_2
                txtInfor3.Text = orgItem.INFOR_3
                txtInfor4.Text = orgItem.INFOR_4
                txtInfor5.Text = orgItem.INFOR_5


                txtLocationWork.Text = orgItem.LOCATION_WORK
                txtNumberDecision.Text = orgItem.NUMBER_DECISION
                txtTypeDecision.Text = orgItem.TYPE_DECISION

                txtNameVN.Text = orgItem.NAME_VN
                txtNameEN.Text = orgItem.NAME_EN
                Dim strFiles As String = orgItem.FILES
                If check = "EDITFILE" Then
                    strFiles = ""
                End If
                If strFiles <> "" Then
                    For Each items As String In strFiles.Split("#")
                        Dim i As New RadListBoxItem(items, items)
                        i.Checked = True
                        lstFile.Items.Add(i)
                    Next
                End If
                If IsNumeric(orgItem.U_INSURANCE) AndAlso orgItem.U_INSURANCE > 0 Then
                    cboU_insurance.SelectedValue = orgItem.U_INSURANCE
                Else
                    ClearControlValue(cboU_insurance)
                End If
                'cboU_insurance.SelectedValue = orgItem.U_INSURANCE
                If IsNumeric(orgItem.ORG_LEVEL) AndAlso orgItem.ORG_LEVEL > 0 Then
                    cboOrg_level.SelectedValue = orgItem.ORG_LEVEL
                Else
                    ClearControlValue(cboOrg_level)
                End If
                'cboOrg_level.SelectedValue = orgItem.ORG_LEVEL
                If orgItem.REGION_ID > 0 Then
                    cboRegion.SelectedValue = orgItem.REGION_ID
                Else
                    ClearControlValue(cboRegion)
                End If
                If orgItem.ORG_REG_ID > 0 Then
                    cboOrgReg.SelectedValue = orgItem.ORG_REG_ID
                Else
                    ClearControlValue(cboOrgReg)
                End If
                'cboRegion.SelectedValue = If(orgItem.REGION_ID > 0, orgItem.REGION_ID, -1)
                txtRepresentativeName.Text = orgItem.REPRESENTATIVE_NAME

                rtNUMBER_BUSINESS.Text = orgItem.NUMBER_BUSINESS
                txtEmail.Text = orgItem.EMAIL
                txtMobliePhone.Text = orgItem.MOBILE
                txtFAX.Text = orgItem.FAX
                txtCost_center_code.Text = orgItem.COST_CENTER_CODE
                rdDATE_BUSINESS.SelectedDate = Nothing
                If IsDate(orgItem.DATE_BUSINESS) Then
                    rdDATE_BUSINESS.SelectedDate = orgItem.DATE_BUSINESS
                End If
                rdEffectDate.SelectedDate = Nothing
                If IsDate(orgItem.EFFECT_DATE) Then
                    rdEffectDate.SelectedDate = orgItem.EFFECT_DATE
                Else
                    rdEffectDate.SelectedDate = Nothing
                End If
                If IsDate(orgItem.FOUNDATION_DATE) Then
                    rdFOUNDATION_DATE.SelectedDate = orgItem.FOUNDATION_DATE
                Else
                    ClearControlValue(rdFOUNDATION_DATE)
                End If
                If IsNumeric(orgItem.ORD_NO) Then
                    rdOrdNo.Value = orgItem.ORD_NO
                Else
                    rdOrdNo.Value = 0
                End If
                rdDicision_Date.SelectedDate = Nothing
                If IsDate(orgItem.DISSOLVE_DATE) Then
                    rdDicision_Date.SelectedDate = orgItem.DISSOLVE_DATE
                End If
                If orgItem.CHK_ORGCHART = True Then
                    chkOrgChart.Checked = True
                Else
                    chkOrgChart.Checked = False
                End If
                rtADDRESS.Text = orgItem.ADDRESS
                txtREMARK.Text = orgItem.REMARK
                If orgItem.REPRESENTATIVE_ID IsNot Nothing Then
                    hidRepresentative.Value = orgItem.REPRESENTATIVE_ID
                End If
                If IsNumeric(orgItem.UNIT_LEVEL) AndAlso orgItem.UNIT_LEVEL > 0 Then
                    cbUNIT_LEVEL.SelectedValue = orgItem.UNIT_LEVEL
                Else
                    ClearControlValue(cbUNIT_LEVEL)
                End If
                DisplayImage(Utilities.ObjToDecima(orgItem.REPRESENTATIVE_ID), Utilities.ObjToString(orgItem.IMAGE))
                If treeOrgFunction.SelectedNode.Level = 1 Then
                    Dim logoPath = AppDomain.CurrentDomain.BaseDirectory & "ReportTemplates\Profile\Organization\Logo\"
                    If Not Directory.Exists(logoPath) Then
                        Directory.CreateDirectory(logoPath)
                    End If
                    Dim logo = treeOrgFunction.SelectedNode.Value & "_*"
                    Dim dirs As String() = Directory.GetFiles(logoPath, logo)
                    Dim licensePath = AppDomain.CurrentDomain.BaseDirectory & "ReportTemplates\Profile\Organization\License\"
                    If Not Directory.Exists(licensePath) Then
                        Directory.CreateDirectory(licensePath)
                    End If
                    Dim license = treeOrgFunction.SelectedNode.Value & "_*"
                    dirs = Directory.GetFiles(logoPath, license)
                End If
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

    Private Function GetUpLevelByNode(ByVal node As RadTreeNode) As OrganizationPathDTO
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

            Return New OrganizationPathDTO With {.ID = Decimal.Parse(node.Value),
                                                 .HIERARCHICAL_PATH = strValue,
                                                 .DESCRIPTION_PATH = strText}
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Function

    Private Sub GetDownLevelByNode(ByVal node As RadTreeNode, ByRef lstPath As List(Of OrganizationPathDTO))
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
            Dim lstOrgs As List(Of OrganizationDTO) = Organizations

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
    Private Sub btnUploadFile_Click(sender As Object, e As System.EventArgs) Handles btnUploadFile.Click
        ctrlUpload.MaxFileInput = 10
        ctrlUpload.isMultiple = False
        ctrlUpload.AllowedExtensions = "pdf,png,doc,docx,xls,xlsx,jpg,jpeg,rar"
        ctrlUpload.Show()
    End Sub
    Private Sub ctrlUpload_OkClicked(sender As Object, e As System.EventArgs) Handles ctrlUpload.OkClicked
        Try
            If ctrlUpload.UploadedFiles.Count > 0 Then
                For i As Integer = 0 To ctrlUpload.UploadedFiles.Count - 1
                    Dim fileName As String = String.Empty
                    Dim file As UploadedFile = ctrlUpload.UploadedFiles(i)
                    fileName = Server.MapPath("~/ReportTemplates/Training/Upload")
                    If Not Directory.Exists(fileName) Then
                        Directory.CreateDirectory(fileName)
                    End If
                    'New System.IO.FileInfo(System.IO.Path.Combine(Server.MapPath("~/ReportTemplates/Training/Upload/"), strFiles.Split("#")(i)))
                    If isPhysical = 1 Then
                        fileName = System.IO.Path.Combine(fileName, file.FileName)
                        'fileName = System.IO.Path.Combine(Server.MapPath("~/ReportTemplates/Training/Upload/"), file.FileName)
                    Else
                        fileName = System.IO.Path.Combine(Server.MapPath("~/ReportTemplates/Training/Upload/"), file.FileName)
                    End If

                    file.SaveAs(fileName, True)
                    Dim item As New RadListBoxItem(file.FileName, file.FileName)
                    item.Checked = True
                    lstFile.Items.Add(item)
                Next
            End If
        Catch ex As Exception
            ShowMessage(Translate("Import bị lỗi"), NotifyType.Error)
        End Try
    End Sub
    Private Sub btnDownloadFile_Click(sender As Object, e As System.EventArgs) Handles btnDownloadFile.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim mid As String = "Training" 'module dao tao
            Dim strFiles As String = String.Empty
            If lstFile.CheckedItems.Count = 0 Then
                Exit Sub
            End If
            strFiles = lstFile.CheckedItems.Select(Function(x) x.Text).Aggregate(Function(x, y) x & "#" & y)
            strFiles = strFiles.Replace(" ", "")
            If strFiles.Split("#").Count > 0 Then
                Using zip As New ZipFile
                    zip.AlternateEncodingUsage = ZipOption.AsNecessary
                    zip.AddDirectoryByName("Files")
                    For i As Integer = 0 To strFiles.Split("#").Count - 1
                        Dim file As System.IO.FileInfo
                        If isPhysical = 1 Then
                            'file = New System.IO.FileInfo(System.IO.Path.Combine(PathTemplateInFolder & mid, strFiles.Split("#")(i)))
                            file = New System.IO.FileInfo(System.IO.Path.Combine(Server.MapPath("~/ReportTemplates/Training/Upload/"), strFiles.Split("#")(i)))
                        Else
                            file = New System.IO.FileInfo(System.IO.Path.Combine(Server.MapPath("~/ReportTemplates/Training/Upload/"), strFiles.Split("#")(i)))
                        End If
                        If file.Exists Then
                            zip.AddFile(file.FullName, "Files")
                        Else
                            ShowMessage(Translate(CommonMessage.CM_CTRLPROGRAMS_IS_NOT_EXSIST_TEMPLATE_FILE), NotifyType.Warning)
                        End If
                    Next
                    Response.Clear()
                    Dim zipName As String = [String].Format("AttachFile{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"))
                    Response.ContentType = "application/zip"
                    Response.AddHeader("content-disposition", "attachment; filename=" + zipName)
                    zip.Save(Response.OutputStream)
                    Response.Flush()
                    Response.SuppressContent = True
                    HttpContext.Current.ApplicationInstance.CompleteRequest()
                End Using
            Else
                ShowMessage(Translate(CommonMessage.CM_CTRLPROGRAMS_IS_NOT_SELECTED_MODULE), NotifyType.Warning)
                Exit Sub
            End If
        Catch ex As Exception
            ShowMessage(ex.ToString, NotifyType.Error)
        End Try
    End Sub

#End Region

End Class