Imports Common
Imports Common.Common
Imports Common.CommonMessage
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Recruitment.RecruitmentBusiness
Imports Telerik.Web.UI
Public Class ctrlRC_CanDtlFamily
    Inherits CommonView
    Dim CandidateCode As String

#Region "Properties"
    Property CandidateInfo As CandidateDTO
        Get
            Return PageViewState(Me.ID & "_CandidateInfo")
        End Get
        Set(ByVal value As CandidateDTO)
            PageViewState(Me.ID & "_CandidateInfo") = value
        End Set
    End Property
    Property isClear As Boolean
        Get
            Return ViewState(Me.ID & "_isClear")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_isClear") = value
        End Set
    End Property
    Property isLoad As Boolean
        Get
            Return ViewState(Me.ID & "_isLoad")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_isLoad") = value
        End Set
    End Property
    Property IDSelect As Decimal
        Get
            Return ViewState(Me.ID & "_IDSelect")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_IDSelect") = value
        End Set
    End Property
    Property lstFamily As List(Of CandidateFamilyDTO)
        Get
            Return ViewState(Me.ID & "_lstFamily")
        End Get
        Set(ByVal value As List(Of CandidateFamilyDTO))
            ViewState(Me.ID & "_lstFamily") = value
        End Set
    End Property
    Property SelectedItem As List(Of Decimal)
        Get
            Return ViewState(Me.ID & "_SelectedItem")
        End Get
        Set(ByVal value As List(Of Decimal))
            ViewState(Me.ID & "_SelectedItem") = value
        End Set
    End Property
    Property ComboBoxDataDTO As ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ComboBoxDataDTO")
        End Get
        Set(ByVal value As ComboBoxDataDTO)
            ViewState(Me.ID & "_ComboBoxDataDTO") = value
        End Set
    End Property

    'Lưu lại đang ở View nào để load khi dùng nút Next và Previous để chuyển sang xem thông tin nhân viên khác.
    Public Property CurrentPlaceHolder As String
        Get
            Return PageViewState(Me.ID & "_CurrentPlaceHolder")
        End Get
        Set(ByVal value As String)
            PageViewState(Me.ID & "_CurrentPlaceHolder") = value
        End Set
    End Property
#End Region

#Region "Page"

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            UpdateControlState()
            Refresh()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        InitControl()
    End Sub

    Protected Sub InitControl()
        Try
            Me.MainToolBar = tbarMainToolBar
            BuildToolbar(Me.MainToolBar, ToolbarItem.Create, ToolbarItem.Seperator, ToolbarItem.Edit,
                         ToolbarItem.Seperator, ToolbarItem.Save, ToolbarItem.Seperator,
                         ToolbarItem.Cancel, ToolbarItem.Export, ToolbarItem.Seperator, ToolbarItem.Delete)
            CType(Me.MainToolBar.Items(4), RadToolBarButton).CausesValidation = True
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Try
            GetFamily()
            Me.CurrentPlaceHolder = Me.ViewName
            ctrlRC_CanBasicInfo.SetProperty("CurrentPlaceHolder", Me.CurrentPlaceHolder)

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub BindData()
        Try
            Dim rep As New RecruitmentRepository
            If ComboBoxDataDTO Is Nothing Then
                ComboBoxDataDTO = New ComboBoxDataDTO
            End If
            ComboBoxDataDTO.GET_RELATION = True
            ComboBoxDataDTO.GET_NATION = True
            ComboBoxDataDTO.GET_PROVINCE = True
            ComboBoxDataDTO.GET_DISTRICT = True
            rep.GetComboList(ComboBoxDataDTO)
            If ComboBoxDataDTO IsNot Nothing Then
                FillDropDownList(cboRelationship, ComboBoxDataDTO.LIST_RELATION, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboRelationship.SelectedValue)
                FillDropDownList(cboBIRTH_NAT, ComboBoxDataDTO.LIST_NATION, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboBIRTH_NAT.SelectedValue)
                FillDropDownList(cboBIRTH_NAT2, ComboBoxDataDTO.LIST_NATION, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboBIRTH_NAT2.SelectedValue)
                FillDropDownList(cboBIRTH_PRO, ComboBoxDataDTO.LIST_PROVINCE, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboBIRTH_PRO.SelectedValue)
                FillDropDownList(cboBIRTH_DIS, ComboBoxDataDTO.LIST_DISTRICT, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboBIRTH_DIS.SelectedValue)
                FillDropDownList(cboID_PLACE, ComboBoxDataDTO.LIST_PROVINCE, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboID_PLACE.SelectedValue)
            End If
            rntxtYear.MaxValue = Date.Now.Year
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    isClear = False
                Case TOOLBARITEM_EDIT
                    If SelectedItem Is Nothing Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If SelectedItem.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If SelectedItem.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    CurrentState = CommonMessage.STATE_EDIT
                Case TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        'If cbIsDied.Checked = True And dtpDIED_DATE.SelectedDate Is Nothing Then
                        '    ShowMessage(Translate("Chưa chọn ngày mất"), Utilities.NotifyType.Warning)
                        '    Exit Sub
                        'End If
                        Select Case CurrentState
                            Case STATE_NEW
                                If Execute() Then
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                                    CurrentState = CommonMessage.STATE_NORMAL
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case STATE_EDIT
                                If Execute() Then
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                                    CurrentState = CommonMessage.STATE_NORMAL
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select
                        SelectedItem = Nothing
                        isClear = False
                        isLoad = False
                        GetFamily()
                    End If
                Case TOOLBARITEM_CANCEL
                    SelectedItem = Nothing
                    ' If CurrentState = STATE_NEW Then
                    ResetControlValue()
                    'End If
                    CurrentState = CommonMessage.STATE_NORMAL
                Case TOOLBARITEM_DELETE
                    If SelectedItem Is Nothing Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If SelectedItem.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.Show()
                Case TOOLBARITEM_EXPORT
                    Dim _error As String = ""
                    Using xls As New ExcelCommon
                        Dim dtData = lstFamily.ToTable
                        dtData.Columns.Add("DEDUCT")
                        dtData.TableName = "Data"
                        For Each dr As DataRow In dtData.Rows
                            If dr("IS_DEDUCT").ToString() = "1" Then
                                dr("DEDUCT") = "Có"
                            Else
                                dr("DEDUCT") = "Không"
                            End If
                        Next
                        Dim bCheck = xls.ExportExcelTemplate(
                            Server.MapPath("~/ReportTemplates/" & Request.Params("mid") & "/ctrlRC_CanDtlFamily.xls"),
                            "CandidateFamily_" & CandidateInfo.CANDIDATE_CODE, dtData, Response, _error)
                        If Not bCheck Then
                            Select Case _error
                                Case 1
                                    ShowMessage(Translate("Mẫu báo cáo không tồn tại"), NotifyType.Warning)
                                Case 2
                                    ShowMessage(Translate("Dữ liệu không tồn tại"), NotifyType.Warning)
                            End Select
                            Exit Sub
                        End If
                    End Using
            End Select
            ctrlRC_CanBasicInfo.SetProperty("CurrentState", Me.CurrentState)
            ctrlRC_CanBasicInfo.Refresh()
            UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub cvalRelationship_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalRelationship.ServerValidate
        Try
            If cboRelationship.SelectedValue <> "" Then
                args.IsValid = True
            Else
                args.IsValid = False
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub cvalBirthDate_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalBirthDate.ServerValidate
        If rntxtDay.Value Is Nothing AndAlso rntxtMonth.Value Is Nothing And rntxtYear.Value Is Nothing Then
            cvalBirthDate.ErrorMessage = Translate("Bạn phải nhập ngày sinh")
            cvalBirthDate.ToolTip = Translate("Bạn phải nhập ngày sinh")
            args.IsValid = False
        End If
        If rntxtDay.Value IsNot Nothing AndAlso rntxtMonth.Value Is Nothing Then
            cvalBirthDate.ErrorMessage = "Bạn phải nhập tháng sinh."
            cvalBirthDate.ToolTip = "Bạn phải nhập tháng sinh."
            args.IsValid = False
        End If
        If rntxtDay.Value IsNot Nothing AndAlso rntxtMonth.Value IsNot Nothing Then
            Try
                Dim BirthDate = New Date(Date.Now.Year, rntxtMonth.Value, rntxtDay.Value)
                args.IsValid = IsDate(BirthDate)
            Catch ex As Exception
                cvalBirthDate.ErrorMessage = "Ngày tháng năm sinh không hợp lệ."
                cvalBirthDate.ToolTip = "Ngày tháng năm sinh không hợp lệ."
                args.IsValid = False
            End Try
        End If
    End Sub

    Private Sub rgFamily_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgFamily.ItemDataBound
        Try
            If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
                Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
                If SelectedItem IsNot Nothing Then
                    If SelectedItem.Contains(Decimal.Parse(datarow("ID").Text)) Then
                        e.Item.Selected = True
                    End If
                End If

                Dim a = CType(datarow.DataItem, CandidateFamilyDTO)

                Dim strBirthDate As String = ""
                Dim lblBirthDate As Label = CType(datarow.FindControl("lblBirthDate"), Label)
                If a.BIRTH_DAY IsNot Nothing Then
                    strBirthDate = a.BIRTH_DAY.ToString
                End If
                If a.BIRTH_DAY IsNot Nothing And a.BIRTH_MONTH IsNot Nothing Then
                    strBirthDate = strBirthDate + "/" + a.BIRTH_MONTH.ToString
                End If
                If a.BIRTH_MONTH IsNot Nothing And a.BIRTH_YEAR IsNot Nothing Then
                    strBirthDate = strBirthDate + "/" + a.BIRTH_YEAR.ToString
                End If
                If a.BIRTH_MONTH Is Nothing And a.BIRTH_YEAR IsNot Nothing Then
                    strBirthDate = a.BIRTH_YEAR.ToString
                End If
                If a.BIRTH_DAY IsNot Nothing And a.BIRTH_MONTH Is Nothing And a.BIRTH_YEAR IsNot Nothing Then
                    strBirthDate = a.BIRTH_DAY.ToString + "/_/" + a.BIRTH_YEAR.ToString
                End If

                If lblBirthDate IsNot Nothing Then
                    lblBirthDate.Text = strBirthDate
                End If
                Dim item = (From p In lstFamily Where p.ID = Decimal.Parse(datarow("ID").Text)).FirstOrDefault
                item.BIRTH_DATE = strBirthDate
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub rgFamily_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgFamily.SelectedIndexChanged
        Try
            If rgFamily.SelectedItems.Count = 0 Then Exit Sub
            'Lưu vào viewStates để giữ những item được select phục vụ cho phương thức delete.
            SelectedItem = New List(Of Decimal)
            For Each dr As Telerik.Web.UI.GridDataItem In rgFamily.SelectedItems
                SelectedItem.Add(Decimal.Parse(dr("ID").Text))
            Next

            Dim item As GridDataItem = rgFamily.SelectedItems(0)
            hidFamilyID.Value = HttpUtility.HtmlDecode(item("ID").Text).Trim()
            hidCanID.Value = HttpUtility.HtmlDecode(item("Candidate_ID").Text).Trim()
            txtCompany.Text = HttpUtility.HtmlDecode(item("COMPANY").Text).Trim()
            txtADDRESS.Text = HttpUtility.HtmlDecode(item("ADDRESS").Text).Trim()
            txtIDNO.Text = HttpUtility.HtmlDecode(item("ID_NO").Text).Trim()
            txtFullName.Text = HttpUtility.HtmlDecode(item("FULLNAME").Text).Trim()
            txtJob.Text = HttpUtility.HtmlDecode(item("JOB").Text).Trim()

            If HttpUtility.HtmlDecode(item("BIRTH_DAY").Text).Trim() <> "" Then
                rntxtDay.Value = HttpUtility.HtmlDecode(item("BIRTH_DAY").Text).Trim()
            Else
                rntxtDay.Value = Nothing
            End If

            If HttpUtility.HtmlDecode(item("BIRTH_MONTH").Text).Trim() <> "" Then
                rntxtMonth.Value = HttpUtility.HtmlDecode(item("BIRTH_MONTH").Text).Trim()
            Else
                rntxtMonth.Value = Nothing
            End If

            If HttpUtility.HtmlDecode(item("BIRTH_YEAR").Text).Trim() <> "" Then
                rntxtYear.Value = HttpUtility.HtmlDecode(item("BIRTH_YEAR").Text).Trim()
            Else
                rntxtYear.Value = Nothing
            End If

            If HttpUtility.HtmlDecode(item("RELATION_ID").Text).Trim() <> "" Then
                cboRelationship.SelectedValue = HttpUtility.HtmlDecode(item("RELATION_ID").Text).Trim()
            Else
                cboRelationship.SelectedIndex = 0
            End If

            If HttpUtility.HtmlDecode(item("DIED_DATE").Text).Trim() <> "" Then
                dtpDIED_DATE.SelectedDate = HttpUtility.HtmlDecode(item("DIED_DATE").Text).Trim()
            Else
                dtpDIED_DATE.SelectedDate = Nothing
            End If

            'Set Checkboxes
            cbIsMLG.Checked = HttpUtility.HtmlDecode(item("IS_MLG").Text).Trim()
            cbIsDied.Checked = HttpUtility.HtmlDecode(item("IS_DIED").Text).Trim()

            txtREMARK.Text = HttpUtility.HtmlDecode(item("REMARK").Text).Trim()

            txtPIT_CODE.Text = HttpUtility.HtmlDecode(item("PIT_CODE").Text).Trim()

            If HttpUtility.HtmlDecode(item("PIT_DATE").Text).Trim() <> "" Then
                dtpPIT_DATE.SelectedDate = HttpUtility.HtmlDecode(item("PIT_DATE").Text).Trim()
            Else
                dtpPIT_DATE.SelectedDate = Nothing
            End If

            If HttpUtility.HtmlDecode(item("ID_DATE").Text).Trim() <> "" Then
                dtpID_DATE.SelectedDate = HttpUtility.HtmlDecode(item("ID_DATE").Text).Trim()
            Else
                dtpID_DATE.SelectedDate = Nothing
            End If

            If HttpUtility.HtmlDecode(item("ID_PLACE").Text).Trim() <> "" Then
                cboID_PLACE.SelectedValue = HttpUtility.HtmlDecode(item("ID_PLACE").Text).Trim()
            Else
                cboID_PLACE.SelectedIndex = 0
            End If

            txtPER_ADDRESS.Text = HttpUtility.HtmlDecode(item("PER_ADDRESS").Text).Trim()

            If HttpUtility.HtmlDecode(item("BIRTH_NO").Text).Trim() <> "" Then
                txtBIRTH_NO.Value = HttpUtility.HtmlDecode(item("BIRTH_NO").Text).Trim()
            Else
                txtBIRTH_NO.Value = Nothing
            End If

            If HttpUtility.HtmlDecode(item("BIRTH_BOOK").Text).Trim() <> "" Then
                txtBIRTH_BOOK.Value = HttpUtility.HtmlDecode(item("BIRTH_BOOK").Text).Trim()
            Else
                txtBIRTH_BOOK.Value = Nothing
            End If

            If HttpUtility.HtmlDecode(item("BIRTH_NAT").Text).Trim() <> "" Then
                cboBIRTH_NAT.SelectedValue = HttpUtility.HtmlDecode(item("BIRTH_NAT").Text).Trim()
            Else
                cboBIRTH_NAT.SelectedIndex = 0
            End If

            If HttpUtility.HtmlDecode(item("BIRTH_PRO").Text).Trim() <> "" Then
                cboBIRTH_PRO.SelectedValue = HttpUtility.HtmlDecode(item("BIRTH_PRO").Text).Trim()
            Else
                cboBIRTH_PRO.SelectedIndex = 0
            End If

            If HttpUtility.HtmlDecode(item("BIRTH_DIS").Text).Trim() <> "" Then
                cboBIRTH_DIS.SelectedValue = HttpUtility.HtmlDecode(item("BIRTH_DIS").Text).Trim()
            Else
                cboBIRTH_DIS.SelectedIndex = 0
            End If

            If HttpUtility.HtmlDecode(item("BIRTH_NAT2").Text).Trim() <> "" Then
                cboBIRTH_NAT2.SelectedValue = HttpUtility.HtmlDecode(item("BIRTH_NAT2").Text).Trim()
            Else
                cboBIRTH_NAT2.SelectedIndex = 0
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub cvalIDNO_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalIDNO.ServerValidate
        Try
            Dim rep As New RecruitmentRepository
            Dim _validate As New CandidateFamilyDTO

            'Nếu có Số CMND thì check trùng.
            If txtIDNO.Text.Trim() <> "" Then
                _validate.ID_NO = txtIDNO.Text.Trim()
                If hidFamilyID.Value <> "" Then
                    _validate.ID = Decimal.Parse(hidFamilyID.Value)
                End If
                If (rep.ValidateFamily(_validate)) Then
                    args.IsValid = True
                Else
                    args.IsValid = False
                End If
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                If DeleteFamily() Then  'Xóa
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                    GetFamily() 'Load lại data cho lưới.
                    'Hủy selectedItem của grid.
                    SelectedItem = Nothing
                    CurrentState = CommonMessage.STATE_NEW
                    isClear = False
                    UpdateControlState()
                Else
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                End If

            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub cboBIRTH_NAT_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) _
        Handles cboBIRTH_NAT.SelectedIndexChanged
        Dim lst As New List(Of ProvinceDTO)
        Try
            If cboBIRTH_NAT.SelectedValue <> "" Then
                If ComboBoxDataDTO.LIST_PROVINCE.Count > 0 Then
                    lst = (From p In ComboBoxDataDTO.LIST_PROVINCE Where p.NATION_ID = cboBIRTH_NAT.SelectedValue).ToList
                    FillDropDownList(cboBIRTH_PRO, lst, "NAME_VN", "ID", Common.Common.SystemLanguage, True)
                End If
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub cboBIRTH_PRO_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) _
        Handles cboBIRTH_PRO.SelectedIndexChanged
        Dim lst As New List(Of DistrictDTO)
        Try
            If cboBIRTH_PRO.SelectedValue <> "" Then
                If ComboBoxDataDTO.LIST_DISTRICT.Count > 0 Then
                    lst = (From p In ComboBoxDataDTO.LIST_DISTRICT Where p.PROVINCE_ID = cboBIRTH_PRO.SelectedValue).ToList
                    FillDropDownList(cboBIRTH_DIS, lst, "NAME_VN", "ID", Common.Common.SystemLanguage, True)
                End If
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Custom"
    Private Function Execute() As Boolean
        Try
            Dim objFamily As New CandidateFamilyDTO
            Dim rep As New RecruitmentRepository
            objFamily.Candidate_ID = CandidateInfo.ID
            objFamily.FULLNAME = txtFullName.Text.Trim()
            objFamily.BIRTH_DAY = rntxtDay.Value
            objFamily.BIRTH_MONTH = rntxtMonth.Value
            objFamily.BIRTH_YEAR = rntxtYear.Value

            objFamily.ID_NO = txtIDNO.Text.Trim()
            If cboRelationship.SelectedValue <> "" Then
                objFamily.RELATION_ID = Decimal.Parse(cboRelationship.SelectedValue)
            End If
            objFamily.JOB = txtJob.Text.Trim()
            objFamily.COMPANY = txtCompany.Text.Trim()
            objFamily.ADDRESS = txtADDRESS.Text.Trim()
            objFamily.IS_MLG = cbIsMLG.Checked
            objFamily.IS_DIED = cbIsDied.Checked
            objFamily.DIED_DATE = dtpDIED_DATE.SelectedDate
            objFamily.REMARK = txtREMARK.Text

            objFamily.PIT_CODE = txtPIT_CODE.Text
            objFamily.PIT_DATE = dtpPIT_DATE.SelectedDate
            objFamily.ID_DATE = dtpID_DATE.SelectedDate
            If cboID_PLACE.SelectedValue <> "" Then
                objFamily.ID_PLACE = cboID_PLACE.SelectedValue
            End If
            objFamily.PER_ADDRESS = txtPER_ADDRESS.Text
            objFamily.BIRTH_NO = txtBIRTH_NO.Value
            objFamily.BIRTH_BOOK = txtBIRTH_BOOK.Value
            If cboBIRTH_NAT.SelectedValue <> "" Then
                objFamily.BIRTH_NAT = cboBIRTH_NAT.SelectedValue
            End If
            If cboBIRTH_PRO.SelectedValue <> "" Then
                objFamily.BIRTH_PRO = cboBIRTH_PRO.SelectedValue
            End If
            If cboBIRTH_DIS.SelectedValue <> "" Then
                objFamily.BIRTH_DIS = cboBIRTH_DIS.SelectedValue
            End If
            If cboBIRTH_NAT2.SelectedValue <> "" Then
                objFamily.BIRTH_NAT2 = cboBIRTH_NAT2.SelectedValue
            End If

            Dim gID As Decimal
            If hidFamilyID.Value = "" Then
                rep.InsertCandidateFamily(objFamily, gID)
            Else
                objFamily.ID = Decimal.Parse(hidFamilyID.Value)
                objFamily.Candidate_ID = Decimal.Parse(hidCanID.Value)
                rep.ModifyCandidateFamily(objFamily, gID)
            End If
            IDSelect = gID
            Return True
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Overrides Sub UpdateControlState()
        Try
            If CurrentState Is Nothing Then
                CurrentState = STATE_NORMAL
            End If
            Select Case CurrentState
                Case STATE_NEW
                    EnabledGrid(rgFamily, False)
                    SetStatusControl(True)
                    If Not isClear Then
                        ResetControlValue()
                        isClear = True
                    End If
                Case STATE_EDIT
                    EnabledGrid(rgFamily, False)
                    SetStatusControl(True)
                Case STATE_NORMAL
                    EnabledGrid(rgFamily, True)
                    SetStatusControl(False)
            End Select
            Me.Send(CurrentState)
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Protected Sub SetStatusToolBar()
        Try
            Select Case CurrentState
                Case STATE_NORMAL
                    Me.MainToolBar.Items(0).Enabled = True 'Add
                    Me.MainToolBar.Items(2).Enabled = True 'Edit
                    Me.MainToolBar.Items(8).Enabled = True 'Delete

                    Me.MainToolBar.Items(4).Enabled = False 'Save
                    Me.MainToolBar.Items(6).Enabled = False 'Cancel
                Case STATE_NEW, STATE_EDIT
                    Me.MainToolBar.Items(0).Enabled = False 'Add
                    Me.MainToolBar.Items(2).Enabled = False 'Edit
                    Me.MainToolBar.Items(8).Enabled = False 'Delete

                    Me.MainToolBar.Items(4).Enabled = True 'Save
                    Me.MainToolBar.Items(6).Enabled = True 'Cancel

            End Select
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub SetStatusControl(ByVal sTrangThai As Boolean)
        txtADDRESS.ReadOnly = Not sTrangThai
        txtJob.ReadOnly = Not sTrangThai
        txtCompany.ReadOnly = Not sTrangThai
        txtIDNO.ReadOnly = Not sTrangThai
        txtFullName.ReadOnly = Not sTrangThai
        cbIsMLG.Enabled = sTrangThai
        cbIsDied.Enabled = sTrangThai
        Utilities.ReadOnlyRadComBo(cboRelationship, Not sTrangThai)
        rntxtDay.ReadOnly = Not sTrangThai
        rntxtMonth.ReadOnly = Not sTrangThai
        rntxtYear.ReadOnly = Not sTrangThai
        dtpDIED_DATE.Enabled = sTrangThai
        txtREMARK.Enabled = sTrangThai
        txtPIT_CODE.Enabled = sTrangThai
        dtpPIT_DATE.Enabled = sTrangThai
        dtpID_DATE.Enabled = sTrangThai
        cboID_PLACE.Enabled = sTrangThai
        txtPER_ADDRESS.Enabled = sTrangThai
        txtBIRTH_NO.Enabled = sTrangThai
        txtBIRTH_BOOK.Enabled = sTrangThai
        cboBIRTH_NAT.Enabled = sTrangThai
        cboBIRTH_PRO.Enabled = sTrangThai
        cboBIRTH_DIS.Enabled = sTrangThai
        cboBIRTH_NAT2.Enabled = sTrangThai

        cboBIRTH_NAT.AutoPostBack = sTrangThai
        cboBIRTH_PRO.AutoPostBack = sTrangThai
        SetStatusToolBar()
    End Sub

    Private Sub ResetControlValue()
        txtFullName.Text = ""
        txtADDRESS.Text = ""
        txtJob.Text = ""
        txtCompany.Text = ""
        txtIDNO.Text = ""
        hidFamilyID.Value = ""
        hidCanID.Value = ""
        cbIsMLG.Checked = False
        cbIsDied.Checked = False
        cboRelationship.SelectedIndex = 0
        rntxtDay.Value = Nothing
        rntxtMonth.Value = Nothing
        rntxtYear.Value = Nothing
        dtpDIED_DATE.Clear()
        txtREMARK.Text = ""
        txtPIT_CODE.Text = ""
        dtpPIT_DATE.Clear()
        dtpID_DATE.Clear()
        cboID_PLACE.SelectedIndex = 0
        txtPER_ADDRESS.Text = ""
        txtBIRTH_NO.Text = ""
        txtBIRTH_BOOK.Text = ""
        cboBIRTH_NAT.SelectedIndex = 0
        cboBIRTH_PRO.SelectedIndex = 0
        cboBIRTH_DIS.SelectedIndex = 0
        cboBIRTH_NAT2.SelectedIndex = 0
    End Sub

    Private Sub GetFamily()
        Try
            If CandidateInfo IsNot Nothing Then
                Dim rep As New RecruitmentRepository
                Dim objFamily As New CandidateFamilyDTO
                objFamily.Candidate_ID = CandidateInfo.ID
                lstFamily = rep.GetCandidateFamily(objFamily)
                rgFamily.DataSource = lstFamily
                rgFamily.DataBind()
                If IDSelect <> 0 Then
                    If rgFamily.Items.Count > 0 Then
                        SelectedItemDataGridByKey(rgFamily, IDSelect)
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Function DeleteFamily() As Boolean
        Try
            Dim rep As New RecruitmentRepository
            Return rep.DeleteCandidateFamily(SelectedItem)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function


#End Region

End Class