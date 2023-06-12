Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile
Imports Telerik.Web.UI


Public Class ctrlInsModifyInfor
    Inherits Common.CommonView
    'Private WithEvents viewRegister As ctrlShiftPlanningRegister

    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Protected WithEvents ctrlOrgSULPPopup As ctrlFindOrgPopup
    Public Property popup As RadWindow
    Public Property popupId As String
    Public Property isLoadPopup As Integer
        Get
            Return ViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isLoadPopup") = value
        End Set
    End Property

#Region "Property & Variable"
    Private Property LastStartDate As Nullable(Of Date)
        Get
            Return PageViewState("LastStartDate")
        End Get
        Set(ByVal value As Nullable(Of Date))
            PageViewState("LastStartDate") = value
        End Set
    End Property

    Private Property LastEndDate As Nullable(Of Date)
        Get
            Return PageViewState("LastEndDate")
        End Get
        Set(ByVal value As Nullable(Of Date))
            PageViewState("LastEndDate") = value
        End Set
    End Property

    'Private Property ListSign As List(Of SIGN_DTO)
    '    Get
    '        Return PageViewState(Me.ID & "_ListSign")
    '    End Get
    '    Set(ByVal value As List(Of SIGN_DTO))
    '        PageViewState(Me.ID & "_ListSign") = value
    '    End Set
    'End Property

    Public ReadOnly Property PageId As String
        Get
            Return Me.ID
        End Get
    End Property

    'Private Property ListPeriod As List(Of PERIOD_DTO)
    '    Get
    '        Return ViewState(Me.ID & "_ListPeriod")
    '    End Get
    '    Set(ByVal value As List(Of PERIOD_DTO))
    '        ViewState(Me.ID & "_ListPeriod") = value
    '    End Set
    'End Property

    Public Property InsCompList As DataTable
        Get
            Return ViewState(Me.ID & "_InsCompList")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_InsCompList") = value
        End Set
    End Property

    Public Property SelectedItemList As List(Of Decimal)
        Get
            Return PageViewState(Me.ID & "_SelectedItemList")
        End Get
        Set(ByVal value As List(Of Decimal))
            PageViewState(Me.ID & "_SelectedItemList") = value
        End Set
    End Property

#End Region

#Region "Page"

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            GetDataCombo()

            rgGridData.AllowCustomPaging = True
            rgGridData.PageSize = Common.Common.DefaultPageSize
            popup = CType(Me.Page, AjaxPage).PopupWindow
            popup.Behaviors = WindowBehaviors.Close
            popupId = popup.ClientID

            Me.MainToolBar = rtbMain
            Common.Common.BuildToolbar(Me.MainToolBar,
                                       ToolbarItem.Create,
                                       ToolbarItem.Edit,
                                       ToolbarItem.Seperator,
                                       ToolbarItem.Save,
                                       ToolbarItem.Cancel,
                                       ToolbarItem.Seperator,
                                       ToolbarItem.Delete,
                                       ToolbarItem.Seperator,
                                       ToolbarItem.Export,
                                        ToolbarItem.Seperator)
            CType(Me.MainToolBar.Items(3), RadToolBarButton).CausesValidation = True
            CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = False
            CType(Me.MainToolBar.Items(4), RadToolBarButton).Enabled = False

            ctrlOrg.AutoPostBack = True
            ctrlOrg.LoadDataAfterLoaded = True
            ctrlOrg.OrganizationType = OrganizationType.OrganizationLocation

            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            Me.rgGridData.SetFilter()
            ShowPopupEmployee()
            If Not IsPostBack Then
                UpdateControlState(CommonMessage.STATE_NORMAL)
                UpdateToolbarState(CommonMessage.STATE_NORMAL)
                txtID.Text = "0"
                ' Refresh()
                ' UpdateControlState()
            End If
            'rgGridData.Culture = Common.Common.SystemLanguage
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub BindData()
        Try
            Dim dic As New Dictionary(Of String, Control)

            dic.Add("ID", txtID)
            Utilities.OnClientRowSelectedChanged(rgGridData, dic)

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Try
            Call LoadDataGrid()
            CurrentState = CommonMessage.STATE_NORMAL
            UpdateToolbarState(CurrentState)
            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub UpdateControlState(ByVal sState As String)
        Try
            Select Case sState
                Case CommonMessage.STATE_NORMAL
                    Utilities.EnabledGridNotPostback(rgGridData, True)
                    EnableControlAll(False, btnSearchEmp, txtEMPLOYEE_ID, ddlINS_MODIFIER_TYPE_ID, _
                                     txtREASON, txtOLD_INFO, txtNEW_INFO, txtMODIFIER_DATE, txtNote, _
                                     txtBIRTH_DATE, txtID_DATE)
                    trConAddress.Visible = False
                    trConAddress1.Visible = False
                    trPerAddress.Visible = False
                    trPerAddress1.Visible = False
                Case CommonMessage.STATE_NEW, CommonMessage.STATE_EDIT
                    Utilities.EnabledGridNotPostback(rgGridData, False)
                    EnableControlAll(True, btnSearchEmp, txtEMPLOYEE_ID, ddlINS_MODIFIER_TYPE_ID, _
                                     txtREASON, txtOLD_INFO, txtNEW_INFO, txtMODIFIER_DATE, txtNote, _
                                     txtBIRTH_DATE, txtID_DATE)

                Case CommonMessage.STATE_DELETE

                Case "Nothing"
                    Utilities.EnabledGridNotPostback(rgGridData, True)
            End Select

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub UpdateToolbarState(ByVal sState As String)
        Try
            Select Case sState
                Case CommonMessage.STATE_NORMAL
                    Utilities.EnabledGridNotPostback(rgGridData, True)
                    CType(Me.MainToolBar.Items(0), RadToolBarButton).Enabled = True
                    CType(Me.MainToolBar.Items(1), RadToolBarButton).Enabled = True

                    CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = False
                    CType(Me.MainToolBar.Items(4), RadToolBarButton).Enabled = False

                    CType(Me.MainToolBar.Items(6), RadToolBarButton).Enabled = True

                Case CommonMessage.STATE_NEW
                    Utilities.EnabledGridNotPostback(rgGridData, False)
                    CType(Me.MainToolBar.Items(0), RadToolBarButton).Enabled = False
                    CType(Me.MainToolBar.Items(1), RadToolBarButton).Enabled = False

                    CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = True
                    CType(Me.MainToolBar.Items(4), RadToolBarButton).Enabled = True

                    CType(Me.MainToolBar.Items(6), RadToolBarButton).Enabled = False


                Case CommonMessage.STATE_EDIT
                    Utilities.EnabledGridNotPostback(rgGridData, False)
                    CType(Me.MainToolBar.Items(0), RadToolBarButton).Enabled = False
                    CType(Me.MainToolBar.Items(1), RadToolBarButton).Enabled = False

                    CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = True
                    CType(Me.MainToolBar.Items(4), RadToolBarButton).Enabled = True

                    CType(Me.MainToolBar.Items(6), RadToolBarButton).Enabled = False

                Case CommonMessage.STATE_DELETE

                Case "Nothing"
                    Utilities.EnabledGridNotPostback(rgGridData, True)

            End Select
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

#End Region

#Region "Event"

    Protected Sub btnFIND_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFind.Click
        Call LoadDataGrid()
    End Sub

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick

        Dim rep As New InsuranceBusiness.InsuranceBusinessClient
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    Call ResetForm()
                    CurrentState = CommonMessage.STATE_NEW
                Case CommonMessage.TOOLBARITEM_EDIT
                    If rgGridData.MasterTableView.GetSelectedItems().Length = 0 Then
                        ShowMessage(Translate("Bạn chưa chọn nội dung cần sửa."), NotifyType.Warning)
                        Exit Sub
                    End If
                    UpdateControl()
                    CurrentState = CommonMessage.STATE_EDIT

                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    Call ResetForm()
                Case CommonMessage.TOOLBARITEM_SAVE
                    Call SaveData()
                Case CommonMessage.TOOLBARITEM_DELETE
                    If rgGridData.MasterTableView.GetSelectedItems().Length = 0 Then
                        ShowMessage(Translate("Bạn chưa chọn nội dung cần xóa."), NotifyType.Warning)
                        Exit Sub
                    End If
                    If CheckData_Delete() Then
                        ctrlMessageBox.MessageText = Translate("Bạn có muốn xóa dữ liệu ?")
                        ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                        ctrlMessageBox.DataBind()
                        ctrlMessageBox.Show()
                    Else
                        ShowMessage(Translate("Dữ liệu đang dùng không được xóa"), NotifyType.Warning)
                    End If
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Call Export()
            End Select
            UpdateControlState(CurrentState)
            UpdateToolbarState(CurrentState)
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgGridData_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgGridData.NeedDataSource
        Call LoadDataGrid(False)
    End Sub

    Protected Function RepareDataForAction() As List(Of Decimal)
        Dim lst As New List(Of Decimal)
        For Each dr As Telerik.Web.UI.GridDataItem In rgGridData.SelectedItems
            lst.Add(dr.GetDataKeyValue("ID"))
        Next
        Return lst
    End Function

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Call DeleteData()
                Call ResetForm()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Function CheckData_Delete() As Boolean
        Try
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

#End Region

#Region "Function & Sub"

    Private Sub ResetForm()
        Try
            ClearControlValue(txtEMPID, txtEMPLOYEE_ID, txtFULLNAME, txtREASON, txtOLD_INFO, ddlINS_MODIFIER_TYPE_ID, _
                              txtMODIFIER_DATE, txtNote, txtNEW_INFO, ddlHEALTH_AREA_INS_ID, txtBIRTH_DATE, txtID_DATE, _
                              trPerAddress, cboPer_Province, cboPer_District, _
                              cboPer_Ward, txtContactAddress, cboCon_Province, cboCon_District, cboCon_Ward)
            txtID.Text = "0"
            'Dia chi thuong` tru'
            trPerAddress.Visible = False
            trPerAddress1.Visible = False
            'Dia chi lien lac
            trConAddress.Visible = False
            trConAddress1.Visible = False
        Catch ex As Exception
        End Try
    End Sub

    Private Sub SaveData()
        Try
            Dim rep As New InsuranceBusiness.InsuranceBusinessClient
            If ddlINS_MODIFIER_TYPE_ID.SelectedValue = 6 AndAlso txtNEW_INFO.Text.ToUpper <> "NAM" AndAlso txtNEW_INFO.Text.ToUpper <> "NỮ" Then
                ShowMessage("Thông tin giới tính phải nhập (Nam hoặc Nữ)!", NotifyType.Warning)
                CurrentState = CommonMessage.STATE_EDIT
                Exit Sub
            End If
            Select Case ddlINS_MODIFIER_TYPE_ID.SelectedValue
                Case "4"
                    If txtBIRTH_DATE.SelectedDate Is Nothing Then
                        ShowMessage("Vui lòng nhập ngày sinh mới!", NotifyType.Warning)
                        CurrentState = CommonMessage.STATE_EDIT
                        Exit Sub
                    End If
                    txtNEW_INFO.Text = txtBIRTH_DATE.SelectedDate
                Case "7"
                    txtNEW_INFO.Text = ddlHEALTH_AREA_INS_ID.Text
                    'Case "8"
                    '    If cboIDPlace.Text = "" Then
                    '        ShowMessage("Vui lòng nhập Nơi cấp CMND mới", NotifyType.Warning)
                    '        CurrentState = CommonMessage.STATE_EDIT
                    '        Exit Sub
                    '    End If
                    '    txtNEW_INFO.Text = cboIDPlace.Text
                Case "9"
                    If txtID_DATE.SelectedDate Is Nothing Then
                        ShowMessage("Vui lòng nhập ngày cấp CMND mới", NotifyType.Warning)
                        CurrentState = CommonMessage.STATE_EDIT
                        Exit Sub
                    End If
                    'Case "10"
                    '    If cboBirthPlace.Text = "" Then
                    '        ShowMessage("Vui lòng nhập nơi sinh mới", NotifyType.Warning)
                    '        CurrentState = CommonMessage.STATE_EDIT
                    '        Exit Sub
                    '    End If
                    '    txtNEW_INFO.Text = cboBirthPlace.Text
                Case "11"
                    If txtPerAddress.Text = "" OrElse cboPer_Province.Text = "" OrElse cboPer_District.Text = "" Then
                        ShowMessage("Vui lòng nhập Thông tin địa chỉ thường trú đầy đủ", NotifyType.Warning)
                        CurrentState = CommonMessage.STATE_EDIT
                        Exit Sub
                    End If
                    If cboPer_Ward.Text = "" Then
                        txtNEW_INFO.Text = txtPerAddress.Text & ", " & cboPer_District.Text & ", " & cboPer_Province.Text
                    Else
                        txtNEW_INFO.Text = txtPerAddress.Text & ", " & cboPer_Ward.Text & ", " & cboPer_District.Text & ", " & cboPer_Province.Text
                    End If
                Case "12"
                    If txtContactAddress.Text = "" OrElse cboCon_Province.Text = "" OrElse cboCon_District.Text = "" Then
                        ShowMessage("Vui lòng nhập Thông tin địa chỉ thường trú đầy đủ", NotifyType.Warning)
                        CurrentState = CommonMessage.STATE_EDIT
                        Exit Sub
                    End If
                    If cboCon_Ward.Text = "" Then
                        txtNEW_INFO.Text = txtContactAddress.Text & ", " & cboCon_District.Text & ", " & cboCon_Province.Text & ", "
                    Else
                        txtNEW_INFO.Text = txtContactAddress.Text & ", " & cboCon_Ward.Text & ", " & cboCon_District.Text & ", " & cboCon_Province.Text & ", "
                    End If
            End Select

            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                    If rep.UpdateInsModifier(Common.Common.GetUsername(), InsCommon.getNumber(txtID.Text) _
                                                                        , InsCommon.getNumber(txtEMPID.Text) _
                                                                        , InsCommon.getNumber(ddlINS_MODIFIER_TYPE_ID.SelectedValue) _
                                                                        , txtREASON.Text _
                                                                        , txtOLD_INFO.Text _
                                                                        , txtNEW_INFO.Text _
                                                                        , (txtMODIFIER_DATE.SelectedDate) _
                                                                        , txtNote.Text, IIf(chkIsUpdate.Checked, "1", "0") _
                                                                        , txtBIRTH_DATE.SelectedDate _
                                                                        , InsCommon.getNumber(ddlHEALTH_AREA_INS_ID.SelectedValue) _
                                                                        , txtBIRTH_DATE.SelectedDate _
                                                                        , 0 _
                                                                        , 0 _
                                                                        , txtPerAddress.Text _
                                                                        , 0 _
                                                                        , InsCommon.getNumber(cboPer_Province.SelectedValue) _
                                                                        , InsCommon.getNumber(cboPer_District.SelectedValue) _
                                                                        , InsCommon.getNumber(cboPer_Ward.SelectedValue) _
                                                                        , txtContactAddress.Text _
                                                                        , 0 _
                                                                        , InsCommon.getNumber(cboCon_Province.SelectedValue) _
                                                                        , InsCommon.getNumber(cboCon_District.SelectedValue) _
                                                                        , InsCommon.getNumber(cboCon_Ward.SelectedValue)) Then
                        Refresh("InsertView")

                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                    End If
                Case CommonMessage.STATE_EDIT
                    If rep.UpdateInsModifier(Common.Common.GetUsername(), InsCommon.getNumber(txtID.Text) _
                                                                        , InsCommon.getNumber(txtEMPID.Text) _
                                                                        , InsCommon.getNumber(ddlINS_MODIFIER_TYPE_ID.SelectedValue) _
                                                                        , txtREASON.Text _
                                                                        , txtOLD_INFO.Text _
                                                                        , txtNEW_INFO.Text _
                                                                        , (txtMODIFIER_DATE.SelectedDate) _
                                                                        , txtNote.Text, IIf(chkIsUpdate.Checked, "1", "0") _
                                                                        , txtBIRTH_DATE.SelectedDate _
                                                                        , InsCommon.getNumber(ddlHEALTH_AREA_INS_ID.SelectedValue) _
                                                                        , txtID_DATE.SelectedDate _
                                                                        , 0 _
                                                                        , 0 _
                                                                        , txtPerAddress.Text _
                                                                        , 0 _
                                                                        , InsCommon.getNumber(cboPer_Province.SelectedValue) _
                                                                        , InsCommon.getNumber(cboPer_District.SelectedValue) _
                                                                        , InsCommon.getNumber(cboPer_Ward.SelectedValue) _
                                                                        , txtContactAddress.Text _
                                                                        , 0 _
                                                                        , InsCommon.getNumber(cboCon_Province.SelectedValue) _
                                                                        , InsCommon.getNumber(cboCon_District.SelectedValue) _
                                                                        , InsCommon.getNumber(cboCon_Ward.SelectedValue)) Then

                        Refresh("UpdateView")
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                    End If
            End Select
            CurrentState = CommonMessage.STATE_NORMAL
        Catch ex As Exception

        End Try
    End Sub

    Private Sub DeleteData()
        Try
            Dim rep As New InsuranceBusiness.InsuranceBusinessClient
            If rep.DeleteInsModifier(Common.Common.GetUserName(), InsCommon.getNumber(txtID.Text)) Then
                Refresh("UpdateView")
            Else
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GetDataCombo()
        Try
            Dim rep As New InsuranceBusiness.InsuranceBusinessClient
            Dim lstSource = rep.GetOtherList("TYPE_EDIT_INS", Common.Common.SystemLanguage.Name, False) 'Loại điều chỉnh
            FillRadCombobox(ddlINS_MODIFIER_TYPE_ID, lstSource, "NAME", "CODE", False)
            FillRadCombobox(ddlMODIFIER_TYPE_SEARCH, lstSource, "NAME", "CODE", False)

            lstSource = rep.GetInsListWhereHealth()
            FillRadCombobox(ddlHEALTH_AREA_INS_ID, lstSource, "NAME_VN", "ID", False)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub LoadDataGrid(Optional ByVal IsDataBind As Boolean = True)
        Try
            Dim lstSource As DataTable = (New InsuranceBusiness.InsuranceBusinessClient).GetInsModifier(Common.Common.GetUserName(), InsCommon.getNumber(0) _
                                                                                    , txtEMPLOYEEID_SEARCH.Text _
                                                                                    , InsCommon.getNumber(ctrlOrg.CurrentValue) _
                                                                                    , InsCommon.getNumber(ddlMODIFIER_TYPE_SEARCH.SelectedValue) _
                                                                                    , txtFROMDATE.SelectedDate _
                                                                                    , txtTODATE.SelectedDate _
                                                                                    , InsCommon.getNumber(IIf(chkSTATUS.Checked, 1, 0))
                                                                                    )
            rgGridData.DataSource = lstSource
            rgGridData.MasterTableView.VirtualItemCount = lstSource.Rows.Count
            rgGridData.CurrentPageIndex = rgGridData.MasterTableView.CurrentPageIndex

            If IsDataBind Then
                rgGridData.DataBind()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgGridData_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgGridData.SelectedIndexChanged
        Try
            If rgGridData.SelectedItems.Count > 0 Then
                Dim dr As GridDataItem
                dr = rgGridData.SelectedItems(0)
                Session("dr") = dr
                txtID.Text = dr("ID").Text

                Dim lstSource As DataTable = (New InsuranceBusiness.InsuranceBusinessClient).GetInsModifier(Common.Common.GetUserName(), InsCommon.getNumber(txtID.Text) _
                                                                , txtEMPLOYEEID_SEARCH.Text _
                                                                , InsCommon.getNumber(ctrlOrg.CurrentValue) _
                                                                , InsCommon.getNumber(ddlINS_MODIFIER_TYPE_ID.SelectedValue) _
                                                                , txtFROMDATE.SelectedDate _
                                                                , txtTODATE.SelectedDate _
                                                                , InsCommon.getNumber(IIf(chkSTATUS.Checked, 1, 0))
                                                                )

                If lstSource IsNot Nothing AndAlso lstSource.Rows.Count > 0 Then
                    GetDataCombo()
                    UpdateControl()
                    InsCommon.SetString(txtEMPID, lstSource.Rows(0)("EMPID"))

                    InsCommon.SetString(txtEMPLOYEE_ID, lstSource.Rows(0)("EMPLOYEE_CODE"))
                    InsCommon.SetString(txtFULLNAME, lstSource.Rows(0)("FULL_NAME"))
                    InsCommon.SetString(txtSOCIAL_NUMBER, lstSource.Rows(0)("SOCIAL_NUMBER"))

                    InsCommon.SetNumber(ddlINS_MODIFIER_TYPE_ID, lstSource.Rows(0)("INS_MODIFIER_TYPE_ID"))
                    InsCommon.SetString(txtREASON, lstSource.Rows(0)("REASON"))
                    InsCommon.SetString(txtOLD_INFO, lstSource.Rows(0)("OLD_INFO"))
                    InsCommon.SetString(txtNEW_INFO, lstSource.Rows(0)("NEW_INFO"))
                    InsCommon.SetDate(txtMODIFIER_DATE, lstSource.Rows(0)("MODIFIER_DATE"))
                    InsCommon.SetString(txtNote, lstSource.Rows(0)("NOTE"))
                    If InsCommon.getNumber(lstSource.Rows(0)("ISUPDATE")) = 1 Then
                        chkIsUpdate.Checked = True
                    Else
                        chkIsUpdate.Checked = False
                    End If

                    '1	Điều chỉnh số sổ BHXH
                    '3	Điều chỉnh họ tên
                    '4	Điều chỉnh ngày sinh            
                    '5	Điều chỉnh số CMND
                    '6	Điều chỉnh giới tính
                    '7	Thay đổi nơi khám chữa bệnh
                    '8  Điều chỉnh nơi cấp CMND
                    '9  Điều chỉnh ngày cấp CMND
                    '10  Điều chỉnh nơi sinh
                    '11  Điều chỉnh địa chỉ thường trú
                    '12  Điều chỉnh địa chỉ liên hệ                  
                    InsCommon.SetString(txtOLD_SOCIAL_NUMBER1, lstSource.Rows(0)("SOCIAL_NUMBER"))
                    InsCommon.SetString(txtOLD_NAME3, lstSource.Rows(0)("VN_FULLNAME"))
                    InsCommon.SetString(txtOLD_BIRTHDAY4, lstSource.Rows(0)("BIRTH_DATE_NM"))
                    InsCommon.SetString(txtOLD_ID_NO5, lstSource.Rows(0)("ID_NO"))
                    InsCommon.SetString(txtOLD_GENDER6, lstSource.Rows(0)("GENDER_NM"))
                    InsCommon.SetString(txtAREA_NM7, lstSource.Rows(0)("AREA_NM"))

                    InsCommon.SetString(txtOLD_ID_PLACE5, lstSource.Rows(0)("ID_PLACE_NM"))
                    InsCommon.SetString(txtOLD_ID_DATE5, lstSource.Rows(0)("ID_DATE_NM"))
                    InsCommon.SetString(txtOLD_BIRTH_PLACE4, lstSource.Rows(0)("BIRTH_PLACE_NM"))
                    InsCommon.SetString(txtOLD_PER_ADDRESS, lstSource.Rows(0)("PER_ADDRESS_NM"))
                    InsCommon.SetString(txtOLD_CON_ADDRESS, lstSource.Rows(0)("CON_ADDRESS_NM"))

                    InsCommon.SetDate(txtBIRTH_DATE, lstSource.Rows(0)("BIRTH_DATE"))
                    InsCommon.SetDate(txtID_DATE, lstSource.Rows(0)("ID_DATE"))
                    InsCommon.SetNumber(ddlHEALTH_AREA_INS_ID, lstSource.Rows(0)("HEALTH_AREA_INS_ID"))

                    'If lstSource.Rows(0)("ID_PLACE_NAME").ToString() <> "" Then
                    '    cboIDPlace.SelectedValue = lstSource.Rows(0)("ID_PLACE")
                    '    cboIDPlace.Text = lstSource.Rows(0)("ID_PLACE_NAME")
                    'End If
                    'If lstSource.Rows(0)("BIRTH_PLACE_NAME").ToString() <> "" Then
                    '    cboBirthPlace.SelectedValue = lstSource.Rows(0)("BIRTH_PLACE")
                    '    cboBirthPlace.Text = lstSource.Rows(0)("BIRTH_PLACE_NAME")
                    'End If
                    txtPerAddress.Text = lstSource.Rows(0)("PER_ADDRESS").ToString()
                    If lstSource.Rows(0)("PER_PROVINCE_NAME").ToString() <> "" Then
                        cboPer_Province.SelectedValue = lstSource.Rows(0)("PER_PROVINCE")
                        cboPer_Province.Text = lstSource.Rows(0)("PER_PROVINCE_NAME")
                    End If
                    If lstSource.Rows(0)("PER_DISTRICT_NAME").ToString() <> "" Then
                        cboPer_District.SelectedValue = lstSource.Rows(0)("PER_DISTRICT")
                        cboPer_District.Text = lstSource.Rows(0)("PER_DISTRICT_NAME")
                    End If
                    If lstSource.Rows(0)("PER_WARD_NAME").ToString() <> "" Then
                        cboPer_Ward.SelectedValue = lstSource.Rows(0)("PER_WARD")
                        cboPer_Ward.Text = lstSource.Rows(0)("PER_WARD_NAME")
                    End If
                    txtContactAddress.Text = lstSource.Rows(0)("CON_ADDRESS").ToString()
                    'If lstSource.Rows(0)("CON_COUNTRY_NAME").ToString() <> "" Then
                    '    cboCon_Country.SelectedValue = lstSource.Rows(0)("CON_COUNTRY")
                    '    cboCon_Country.Text = lstSource.Rows(0)("CON_COUNTRY_NAME")
                    'End If
                    If lstSource.Rows(0)("CON_PROVINCE_NAME").ToString() <> "" Then
                        cboCon_Province.SelectedValue = lstSource.Rows(0)("CON_PROVINCE")
                        cboCon_Province.Text = lstSource.Rows(0)("CON_PROVINCE_NAME")
                    End If
                    If lstSource.Rows(0)("CON_DISTRICT_NAME").ToString() <> "" Then
                        cboCon_District.SelectedValue = lstSource.Rows(0)("CON_DISTRICT")
                        cboCon_District.Text = lstSource.Rows(0)("CON_DISTRICT_NAME")
                    End If
                    If lstSource.Rows(0)("CON_WARD_NAME").ToString() <> "" Then
                        cboCon_Ward.SelectedValue = lstSource.Rows(0)("CON_WARD")
                        cboCon_Ward.Text = lstSource.Rows(0)("CON_WARD_NAME")
                    End If

                End If
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub Export()
        Try
            Dim _error As String = ""

            Dim dtVariable = New DataTable
            dtVariable.Columns.Add(New DataColumn("FROM_DATE"))
            dtVariable.Columns.Add(New DataColumn("TO_DATE"))
            Dim drVariable = dtVariable.NewRow()
            drVariable("FROM_DATE") = Now.ToString("dd/MM/yyyy")
            drVariable("TO_DATE") = Now.ToString("dd/MM/yyyy")
            dtVariable.Rows.Add(drVariable)

            Using xls As New ExcelCommon

                Dim lstSource As DataTable = (New InsuranceBusiness.InsuranceBusinessClient).GetInsModifier(Common.Common.GetUserName(), InsCommon.getNumber(0) _
                                                , txtEMPLOYEEID_SEARCH.Text _
                                                , InsCommon.getNumber(ctrlOrg.CurrentValue) _
                                                , InsCommon.getNumber(ddlINS_MODIFIER_TYPE_ID.SelectedValue) _
                                                , txtFROMDATE.SelectedDate _
                                                , txtTODATE.SelectedDate _
                                                , InsCommon.getNumber(IIf(chkSTATUS.Checked, 1, 0))
                                                )
                lstSource.TableName = "TABLE"

                Dim bCheck = xls.ExportExcelTemplate(Server.MapPath("~/ReportTemplates/" & Request.Params("mid") & "/" & Request.Params("group") & "/InsModifyInfor.xlsx"),
                    "DieuChinhHoSo", lstSource, dtVariable, Response, _error, ExcelCommon.ExportType.Excel)
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
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ddlINS_MODIFIER_TYPE_ID_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles ddlINS_MODIFIER_TYPE_ID.SelectedIndexChanged
        UpdateControl()
    End Sub

    Private Sub UpdateControl()
        Try
            '1	Điều chỉnh số sổ BHXH
            '3	Điều chỉnh họ tên
            '4	Điều chỉnh ngày sinh            
            '5	Điều chỉnh số CMND
            '6	Điều chỉnh giới tính
            '7	Thay đổi nơi khám chữa bệnh
            '8  Điều chỉnh nơi cấp CMND
            '9  Điều chỉnh ngày cấp CMND
            '10  Điều chỉnh nơi sinh
            '11  Điều chỉnh địa chỉ thường trú
            '12  Điều chỉnh địa chỉ liên hệ
            txtNEW_INFO.Visible = True
            ddlHEALTH_AREA_INS_ID.Visible = False
            txtBIRTH_DATE.Visible = False
            txtID_DATE.Visible = False
            trPerAddress.Visible = False
            trPerAddress1.Visible = False
            trConAddress.Visible = False
            trConAddress1.Visible = False
            '-------------------------------------------
            Select Case ddlINS_MODIFIER_TYPE_ID.SelectedValue
                Case "1"
                    txtOLD_INFO.Text = txtOLD_SOCIAL_NUMBER1.Text
                Case "2"
                    txtOLD_INFO.Text = txtOLD_NAME3.Text
                Case "3"
                    txtOLD_INFO.Text = txtOLD_BIRTHDAY4.Text
                    txtNEW_INFO.Visible = False
                    txtBIRTH_DATE.Visible = True
                Case "4"
                    txtOLD_INFO.Text = txtOLD_ID_NO5.Text
                Case "5"
                    txtOLD_INFO.Text = txtOLD_GENDER6.Text
                Case "6"
                    txtOLD_INFO.Text = txtAREA_NM7.Text
                    txtNEW_INFO.Visible = False
                    ddlHEALTH_AREA_INS_ID.Visible = True
                    'Case "8"
                    '    txtOLD_INFO.Text = txtOLD_ID_PLACE5.Text
                    '    txtNEW_INFO.Visible = False
                    '    cboIDPlace.Visible = True
                Case "8"
                    txtOLD_INFO.Text = txtOLD_ID_DATE5.Text
                    txtNEW_INFO.Visible = False
                    txtID_DATE.Visible = True
                    'Case "10"
                    '    txtOLD_INFO.Text = txtOLD_BIRTH_PLACE4.Text
                    '    txtNEW_INFO.Visible = False
                    '    cboBirthPlace.Visible = True
                Case "10"
                    txtOLD_INFO.Text = txtOLD_PER_ADDRESS.Text
                    txtNEW_INFO.Visible = False
                    trPerAddress.Visible = True
                    trPerAddress1.Visible = True
                Case "11"
                    txtOLD_INFO.Text = txtOLD_CON_ADDRESS.Text
                    txtNEW_INFO.Visible = False
                    trConAddress.Visible = True
                    trConAddress1.Visible = True
            End Select

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub cboCommon_ItemsRequested(ByVal sender As Object, ByVal e As RadComboBoxItemsRequestedEventArgs) _
    Handles cboCon_Province.ItemsRequested, cboCon_District.ItemsRequested, cboCon_Ward.ItemsRequested, _
            cboPer_Province.ItemsRequested, cboPer_District.ItemsRequested, cboPer_Ward.ItemsRequested
        Using rep As New ProfileRepository
            Dim dtData As DataTable
            Dim sText As String = e.Text
            Dim dValue As Decimal
            Dim sSelectValue As String = IIf(e.Context("value") IsNot Nothing, e.Context("value"), "")
            Select Case sender.ID
                Case cboPer_Province.ID, cboCon_Province.ID
                    dtData = rep.GetProvinceList(True)
                Case cboPer_District.ID, cboCon_District.ID
                    dValue = IIf(e.Context("valueCustom") IsNot Nothing, e.Context("valueCustom"), 0)
                    dtData = rep.GetDistrictList(dValue, True)
                Case cboPer_Ward.ID, cboCon_Ward.ID
                    dValue = IIf(e.Context("valueCustom") IsNot Nothing, e.Context("valueCustom"), 0)
                    dtData = rep.GetWardList(dValue, True)
            End Select
            If sText <> "" Then
                Dim dtExist = (From p In dtData
                               Where p("NAME") IsNot DBNull.Value AndAlso _
                               p("NAME").ToString.ToUpper = sText.ToUpper)

                If dtExist.Count = 0 Then
                    Dim dtFilter = (From p In dtData
                                    Where p("NAME") IsNot DBNull.Value AndAlso _
                                    p("NAME").ToString.ToUpper.Contains(sText.ToUpper))

                    If dtFilter.Count > 0 Then
                        dtData = dtFilter.CopyToDataTable
                    Else
                        dtData = dtData.Clone
                    End If

                    Dim itemOffset As Integer = e.NumberOfItems
                    Dim endOffset As Integer = Math.Min(itemOffset + sender.ItemsPerRequest, dtData.Rows.Count)
                    e.EndOfItems = endOffset = dtData.Rows.Count

                    For i As Integer = itemOffset To endOffset - 1
                        Dim radItem As RadComboBoxItem = New RadComboBoxItem(dtData.Rows(i)("NAME").ToString(), dtData.Rows(i)("ID").ToString())
                        sender.Items.Add(radItem)
                    Next
                Else

                    Dim itemOffset As Integer = e.NumberOfItems
                    Dim endOffset As Integer = dtData.Rows.Count
                    e.EndOfItems = True

                    For i As Integer = itemOffset To endOffset - 1
                        Dim radItem As RadComboBoxItem = New RadComboBoxItem(dtData.Rows(i)("NAME").ToString(), dtData.Rows(i)("ID").ToString())
                        sender.Items.Add(radItem)
                    Next
                End If
            Else
                Dim itemOffset As Integer = e.NumberOfItems
                Dim endOffset As Integer = Math.Min(itemOffset + sender.ItemsPerRequest, dtData.Rows.Count)
                e.EndOfItems = endOffset = dtData.Rows.Count

                For i As Integer = itemOffset To endOffset - 1
                    Dim radItem As RadComboBoxItem = New RadComboBoxItem(dtData.Rows(i)("NAME").ToString(), dtData.Rows(i)("ID").ToString())
                    sender.Items.Add(radItem)
                Next
            End If
        End Using

    End Sub


#End Region

#Region "FindEmployeeButton"

    Private Sub ctrlFindEmployeePopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.CancelClicked
        isLoadPopup = 0
    End Sub

    Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Dim lstCommonEmployee As New List(Of Common.CommonBusiness.EmployeePopupFindDTO)
        'Dim rep As New AttendanceRepository
        Try
            Dim a As Object = ctrlFindEmployeePopup.SelectedEmployee
            lstCommonEmployee = CType(ctrlFindEmployeePopup.SelectedEmployee, List(Of Common.CommonBusiness.EmployeePopupFindDTO))
            If lstCommonEmployee.Count <> 0 Then
                Dim item = lstCommonEmployee(0)
                Dim lstSource As DataTable = (New InsuranceBusiness.InsuranceBusinessClient).GetEmpInfo(item.EMPLOYEE_ID, 0)
                If (Not (lstSource Is Nothing) AndAlso lstSource.Rows.Count > 0) Then
                    txtEMPLOYEE_ID.Text = lstSource.Rows(0)("EMPLOYEE_ID")
                    InsCommon.SetString(txtFULLNAME, lstSource.Rows(0)("VN_FULLNAME"))
                    InsCommon.SetString(txtSOCIAL_NUMBER, lstSource.Rows(0)("SOCIAL_NUMBER"))

                    'txtDEP.Text = lstSource.Rows(0)("ORG_NAME")
                    'txtDoB.Text = lstSource.Rows(0)("BIRTH_DATE")
                    'txtBirthPlace.Text = lstSource.Rows(0)("EMPLOYEE_ID")
                    'txtCMND.Text = lstSource.Rows(0)("ID_NO")
                    'txtDateIssue.Text = lstSource.Rows(0)("ID_DATE")
                    'txtPOSITION.Text = lstSource.Rows(0)("POSITION_NAME")
                    'txtINSORG.Text = lstSource.Rows(0)("INS_ORG_NAME")

                    '1	Điều chỉnh số sổ BHXH
                    '3	Điều chỉnh họ tên
                    '4	Điều chỉnh ngày sinh
                    '4   Điều chỉnh nơi sinh
                    '5	Điều chỉnh số CMND
                    '5   Điều chỉnh nơi cấp CMND
                    '5   Điều chỉnh ngày cấp CMND
                    '6	Điều chỉnh giới tính
                    '7	Thay đổi nơi khám chữa bệnh
                    '8   Điều chỉnh địa chỉ thường trú
                    '9   Điều chỉnh địa chỉ liên hệ
                    InsCommon.SetString(txtOLD_SOCIAL_NUMBER1, lstSource.Rows(0)("SOCIAL_NUMBER"))
                    InsCommon.SetString(txtOLD_NAME3, lstSource.Rows(0)("VN_FULLNAME"))
                    InsCommon.SetString(txtOLD_BIRTHDAY4, lstSource.Rows(0)("BIRTH_DATE_NM"))
                    InsCommon.SetString(txtOLD_ID_NO5, lstSource.Rows(0)("ID_NO"))
                    InsCommon.SetString(txtOLD_GENDER6, lstSource.Rows(0)("GENDER_NM"))
                    InsCommon.SetString(txtAREA_NM7, lstSource.Rows(0)("AREA_NM"))
                    InsCommon.SetString(txtOLD_ID_PLACE5, lstSource.Rows(0)("ID_PLACE_NM"))
                    InsCommon.SetString(txtOLD_ID_DATE5, lstSource.Rows(0)("ID_DATE_NM"))
                    InsCommon.SetString(txtOLD_BIRTH_PLACE4, lstSource.Rows(0)("BIRTH_PLACE_NM"))
                    InsCommon.SetString(txtOLD_PER_ADDRESS, lstSource.Rows(0)("PER_ADDRESS_NM"))
                    InsCommon.SetString(txtOLD_CON_ADDRESS, lstSource.Rows(0)("CON_ADDRESS_NM"))

                    txtEMPID.Text = lstSource.Rows(0)("EMPID")
                End If
            End If
            isLoadPopup = 0
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub btnSearchEmp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearchEmp.Click
        Try
            isLoadPopup = 1

            ShowPopupEmployee()
            ctrlFindEmployeePopup.Show()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub ShowPopupEmployee()
        Try
            If FindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                FindEmployee.Controls.Remove(ctrlFindEmployeePopup)
            End If

            If isLoadPopup = 1 Then
                ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                ctrlFindEmployeePopup.MustHaveContract = False
                ctrlFindEmployeePopup.MultiSelect = False
                FindEmployee.Controls.Add(ctrlFindEmployeePopup)
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

    Private Sub ctrlOrg_SelectedNodeChanged(sender As Object, e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged
        Call LoadDataGrid()
    End Sub
End Class