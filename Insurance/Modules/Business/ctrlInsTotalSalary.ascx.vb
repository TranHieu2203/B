Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI

Public Class ctrlInsTotalSalary
    Inherits Common.CommonView

    Public Overrides Property MustAuthorize As Boolean = True
#Region "Property"

    Public Property popupId As String

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

    Private Property StatusCheck As Decimal
        Get
            Return ViewState(Me.ID & "_StatusCheck")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_StatusCheck") = value
        End Set
    End Property

#End Region

#Region "Page"

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            'Me.MainToolBar = tbarOtherLists
            Me.ctrlMessageBox.Listener = Me
            Me.rgGridData.SetFilter()
            If Not IsPostBack Then
                UpdateControlState(CommonMessage.STATE_NORMAL)
                UpdateToolbarState(CommonMessage.STATE_NORMAL)
                txtID.Text = "0"
                ' Refresh()
                ' UpdateControlState()
                StatusCheck = 0
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        rgGridData.AllowCustomPaging = True
        rgGridData.PageSize = Common.Common.DefaultPageSize
        Me.ctrlMessageBox.Listener = Me
        Dim popup As RadWindow
        popup = CType(Me.Page, AjaxPage).PopupWindow
        popup.Title = "Tổng quỹ lương hàng loạt"
        popupId = popup.ClientID
        Me.MainToolBar = tbarOtherLists
        Common.Common.BuildToolbar(Me.MainToolBar,
                                    ToolbarItem.Calculate,
                                    ToolbarItem.Export,
                                    ToolbarItem.Seperator,
                                    ToolbarItem.Create,
                                    ToolbarItem.Deactive,
                                   ToolbarItem.Active)
        CType(Me.MainToolBar.Items(3), RadToolBarButton).Text = Translate("Tổng hợp hàng loạt")
        'tbarOtherLists.Items(3).Text = Translate("Tổng hợp hàng loạt")
        MainToolBar.Items(4).Text = Translate("Đóng")
        MainToolBar.Items(5).Text = Translate("Mở")
        Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
        CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        'InitControl()
        If Not IsPostBack Then
            GirdConfig(rgGridData)
        End If
    End Sub

    Public Overrides Sub BindData()
        Try
            GetDataCombo()

            Dim dic As New Dictionary(Of String, Control)
            'InitControl()

            dic.Add("ID", txtID)

            Utilities.OnClientRowSelectedChanged(rgGridData, dic)

            txtYear.Text = DateTime.Now.Year
            txtMonth.Text = DateTime.Now.Month
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub InitControl()
        Try


        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        ClearControlValue(ddlINS_ORG_ID)
        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
        LoadData(True)
    End Sub

    Private Sub UpdateControlState(ByVal sState As String)
        Try
            Select Case sState
                Case CommonMessage.STATE_NORMAL
                    Utilities.EnabledGridNotPostback(rgGridData, True)
                    divCal.Visible = True
                    divCalBatch.Visible = False
                Case CommonMessage.STATE_NEW
                    divCal.Visible = False
                    divCalBatch.Visible = True
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
                    'CType(Me.MainToolBar.Items(0), RadToolBarButton).Enabled = True
                    CType(Me.MainToolBar.Items(1), RadToolBarButton).Enabled = True
                    CType(Me.MainToolBar.Items(2), RadToolBarButton).Enabled = True

                Case CommonMessage.STATE_NEW
                    Utilities.EnabledGridNotPostback(rgGridData, False)
                    CType(Me.MainToolBar.Items(0), RadToolBarButton).Enabled = True
                    CType(Me.MainToolBar.Items(1), RadToolBarButton).Enabled = True
                    CType(Me.MainToolBar.Items(2), RadToolBarButton).Enabled = True
                    CType(Me.MainToolBar.Items(4), RadToolBarButton).Enabled = True


                Case CommonMessage.STATE_EDIT
                    Utilities.EnabledGridNotPostback(rgGridData, False)
                    CType(Me.MainToolBar.Items(0), RadToolBarButton).Enabled = True
                    CType(Me.MainToolBar.Items(1), RadToolBarButton).Enabled = True
                    CType(Me.MainToolBar.Items(2), RadToolBarButton).Enabled = True

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

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        'Dim objOrgFunction As New OrganizationDTO
        'Dim gID As Decimal
        Dim rep As New InsuranceBusiness.InsuranceBusinessClient
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    ClearControlValue(ddlINS_ORG_ID)
                    CurrentState = CommonMessage.STATE_NEW
                    StatusCheck = 1
                Case CommonMessage.TOOLBARITEM_EDIT
                    CurrentState = CommonMessage.STATE_EDIT
                    'txtCODE.Focus()
                    'Case CommonMessage.TOOLBARITEM_ACTIVE, CommonMessage.TOOLBARITEM_DEACTIVE

                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    'FillDropDownList(cboLevel_ID, ListComboData.LIST_ORG_LEVEL, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboLevel_ID.SelectedValue)
                    'FillDataByTree()
                Case CommonMessage.TOOLBARITEM_SAVE
                    Call SaveData()
                    CurrentState = CommonMessage.STATE_NORMAL

                Case CommonMessage.TOOLBARITEM_DELETE
                    Call DeleteData()
                    CurrentState = CommonMessage.STATE_NORMAL

                Case CommonMessage.TOOLBARTIEM_CALCULATE
                    CurrentState = CommonMessage.STATE_NORMAL
                    StatusCheck = 0
                    If divCal.Visible = False Then
                        ddlPeriod.Text = ""
                        ddlPeriod.SelectedIndex = -1
                        divCal.Visible = True
                        divCalBatch.Visible = False
                    End If
                    If ddlINS_ORG_ID.SelectedValue = "" Then
                        ShowMessage("Bạn phải chọn đơn vị bảo hiểm?", NotifyType.Warning)
                        Exit Sub
                    End If
                    If InsCommon.getString(ddlPeriod.Text) = "" Then
                        ShowMessage("Bạn phải chọn đợt khai báo?", NotifyType.Warning)
                        Exit Sub
                    End If
                    If rep.CHECK_INS_TOTALSALARY_UNLOCK(InsCommon.getNumber(txtYear.Text) _
                                                   , InsCommon.getNumber(txtMonth.Text) _
                                                   , InsCommon.getNumber(ddlINS_ORG_ID.SelectedValue)) Then
                        ShowMessage("Đã tồn tại dữ liệu ở trạng thái khóa", NotifyType.Warning)
                        Exit Sub
                    End If
                    If IsExistDataCal() Then
                        ctrlMessageBox.MessageText = Translate("Đã có dữ liệu, bạn có thực sự muốn tổng hợp lại quỹ lương không?")
                        ctrlMessageBox.ActionName = CommonMessage.TOOLBARTIEM_CALCULATE
                        ctrlMessageBox.DataBind()
                        ctrlMessageBox.Show()
                    Else
                        CalData()
                    End If
                Case CommonMessage.TOOLBARITEM_RESET
                    If ddlINS_ORG_ID.SelectedValue = "" Or ddlINS_ORG_ID.SelectedValue Is Nothing Then
                        ShowMessage("Vui lòng chọn đơn vị bảo hiểm để kiểm tra!", NotifyType.Warning)
                        Exit Sub
                    End If
                    Dim obj1 As Object = (New InsuranceBusiness.InsuranceBusinessClient).CheckInvalidArising(ddlINS_ORG_ID.SelectedValue)
                    If obj1 Is Nothing Then
                        ShowMessage("Quá trình kiểm tra đã xảy ra lỗi!", NotifyType.Warning)
                        Exit Sub
                    Else
                        Dim rs As String = obj1(0).ToString()
                        ShowMessage("Danh sách nhân viên không đúng quy trình: " & rs, NotifyType.Warning, 60)
                        Exit Sub
                    End If
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Call Export()
                Case CommonMessage.TOOLBARITEM_DEACTIVE
                    If ddlINS_ORG_ID.SelectedValue = "" Then
                        ShowMessage("Bạn phải chọn đơn vị bảo hiểm?", NotifyType.Warning)
                        Exit Sub
                    End If
                    If StatusCheck = 0 Then
                        If txtYear.Value Is Nothing Then
                            ShowMessage("Bạn phải chọn năm?", NotifyType.Warning)
                            Exit Sub
                        End If
                        If txtMonth.Value Is Nothing Then
                            ShowMessage("Bạn phải chọn tháng?", NotifyType.Warning)
                            Exit Sub
                        End If
                    Else
                        If txtFROMDATE.SelectedDate Is Nothing Then
                            ShowMessage("Bạn phải chọn từ ngày", NotifyType.Warning)
                            Exit Sub
                        End If
                        If txtTODATE.SelectedDate Is Nothing Then
                            ShowMessage("Bạn phải chọn đến ngày", NotifyType.Warning)
                            Exit Sub
                        End If
                    End If
                    LockData(1)
                Case CommonMessage.TOOLBARITEM_ACTIVE
                    If ddlINS_ORG_ID.SelectedValue = "" Then
                        ShowMessage("Bạn phải chọn đơn vị bảo hiểm?", NotifyType.Warning)
                        Exit Sub
                    End If
                    If StatusCheck = 0 Then
                        If txtYear.Value Is Nothing Then
                            ShowMessage("Bạn phải chọn năm?", NotifyType.Warning)
                            Exit Sub
                        End If
                        If txtMonth.Value Is Nothing Then
                            ShowMessage("Bạn phải chọn tháng?", NotifyType.Warning)
                            Exit Sub
                        End If
                    Else
                        If txtFROMDATE.SelectedDate Is Nothing Then
                            ShowMessage("Bạn phải chọn từ ngày", NotifyType.Warning)
                            Exit Sub
                        End If
                        If txtTODATE.SelectedDate Is Nothing Then
                            ShowMessage("Bạn phải chọn đến ngày", NotifyType.Warning)
                            Exit Sub
                        End If
                    End If
                    LockData(0)
            End Select
            UpdateControlState(CurrentState)
            UpdateToolbarState(CurrentState)
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgGridData_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgGridData.NeedDataSource
        Try
            Call LoadData()
        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
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
            If e.ActionName = CommonMessage.TOOLBARTIEM_CALCULATE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Call CalData()
            End If
            If e.ActionName = CommonMessage.TOOLBARITEM_CREATE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Call CalDataBatch()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub btnCalBatch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCalBatch.Click
        Dim rep As New InsuranceBusiness.InsuranceBusinessClient
        If ddlINS_ORG_ID.SelectedValue = "" Then
            ShowMessage("Bạn phải chọn đơn vị bảo hiểm?", NotifyType.Warning)
            Exit Sub
        End If
        Dim check = rep.CHECK_INS_TOTALSALARY_UNLOCK_BYDATE(InsCommon.getDate(txtFROMDATE.SelectedDate) _
                                                                     , InsCommon.getDate(txtTODATE.SelectedDate) _
                                                                     , InsCommon.getNumber(ddlINS_ORG_ID.SelectedValue))
        If check Then
            ctrlMessageBox.MessageText = Translate("Tồn tại dữ liệu ở trạng thái đã khóa, hệ thống sẽ bỏ qua dữ liệu đã khóa và tổng hợp cho những kỳ ở trạng thái Mở. Bạn có chắc chắn muốn tiếp tục?")
            ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_CREATE
        Else
            ctrlMessageBox.MessageText = Translate("Vui lòng xác nhận để tổng quỹ lương hàng loạt !")
            ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_CREATE
        End If

        ctrlMessageBox.MessageTitle = Translate("Tổng quỹ lương hàng loạt")
        ctrlMessageBox.DataBind()
        ctrlMessageBox.Show()
    End Sub


#End Region

#Region "Function & Sub"
    Private Function LockData(ByVal status As Integer)
        Try
            Dim rep As New InsuranceBusiness.InsuranceBusinessClient
            If StatusCheck = 0 Then
                If rep.LockInsTotalSalary(Common.Common.GetUsername(), InsCommon.getNumber(txtYear.Text) _
                                           , InsCommon.getNumber(txtMonth.Text) _
                                           , InsCommon.getNumber(ddlINS_ORG_ID.SelectedValue) _
                                           , status
                                           ) Then
                    Refresh("")
                Else
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                End If
            Else
                If rep.LockInsTotalSalaryByDate(txtFROMDATE.SelectedDate _
                                           , txtTODATE.SelectedDate _
                                           , InsCommon.getNumber(ddlINS_ORG_ID.SelectedValue) _
                                           , status
                                           ) Then
                    Refresh("")
                Else
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                End If
            End If
        Catch ex As Exception

        End Try
    End Function
    Private Sub ResetForm()
        Try
            txtID.Text = "0"
        Catch ex As Exception
        End Try
    End Sub

    Private Sub SaveData()
        Try

        Catch ex As Exception

        End Try
    End Sub

    Private Sub DeleteData()
        Try
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GetDataCombo()
        Try
            Dim rep As New InsuranceBusiness.InsuranceBusinessClient
            Dim lstSource As DataTable = rep.GetInsListInsuranceByUsername("ADMIN", False)
            Dim newRow As DataRow
            newRow = lstSource.NewRow()
            lstSource.Rows.InsertAt(newRow, 0)
            FillRadCombobox(ddlINS_ORG_ID, lstSource, "NAME", "ID", True)
            'If ddlINS_ORG_ID.Items.Count > 0 Then
            '    ddlINS_ORG_ID.SelectedValue = lstSource.Rows(0)("ID")
            '    LoadComboboxPeriod()
            'End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function IsExistDataCal() As Boolean
        Try
            Dim lstSource As DataTable = (New InsuranceBusiness.InsuranceBusinessClient).GetInsTotalSalary(Common.Common.GetUserName(), InsCommon.getNumber(txtYear.Text) _
                                              , InsCommon.getNumber(txtMonth.Text) _
                                              , InsCommon.getNumber(ddlINS_ORG_ID.SelectedValue) _
                                               , InsCommon.getString(ddlPeriod.SelectedValue) _
                                              )
            If (lstSource Is Nothing) OrElse (lstSource IsNot Nothing AndAlso lstSource.Rows.Count > 0) Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return True
        End Try

    End Function

    Private Sub LoadData(Optional ByVal isBind As Boolean = False)
        Try
            Dim rep As New InsuranceBusiness.InsuranceBusinessClient
            If txtYear.Text.Equals("") Then
                txtYear.Text = Now.Year
                txtMonth.Text = Now.Month
            End If
            Dim lstSource As DataTable = (New InsuranceBusiness.InsuranceBusinessClient).GetInsTotalSalary(Common.Common.GetUsername(), InsCommon.getNumber(txtYear.Text) _
                                        , InsCommon.getNumber(txtMonth.Text) _
                                        , InsCommon.getNumber(ddlINS_ORG_ID.SelectedValue) _
                                         , InsCommon.getString(ddlPeriod.SelectedValue)
                                        )
            If lstSource IsNot Nothing AndAlso lstSource.Rows.Count > 0 Then
                If lstSource.Rows(0).Item("RATE_SI_EMP") IsNot Nothing Then
                    rgGridData.MasterTableView.GetColumn("SI_EMP").HeaderText = "BHXH nhân viên (" & lstSource.Rows(0).Item("RATE_SI_EMP").ToString & "%)"
                End If
                If lstSource.Rows(0).Item("RATE_SI_COM") IsNot Nothing Then
                    rgGridData.MasterTableView.GetColumn("SI_COM").HeaderText = "BHXH công ty (" & lstSource.Rows(0).Item("RATE_SI_COM").ToString & "%)"
                End If
                If lstSource.Rows(0).Item("RATE_HI_EMP") IsNot Nothing Then
                    rgGridData.MasterTableView.GetColumn("HI_EMP").HeaderText = "BHYT nhân viên (" & lstSource.Rows(0).Item("RATE_HI_EMP").ToString & "%)"
                End If
                If lstSource.Rows(0).Item("RATE_HI_COM") IsNot Nothing Then
                    rgGridData.MasterTableView.GetColumn("HI_COM").HeaderText = "BHYT công ty (" & lstSource.Rows(0).Item("RATE_HI_COM").ToString & "%)"
                End If
                If lstSource.Rows(0).Item("RATE_UI_EMP") IsNot Nothing Then
                    rgGridData.MasterTableView.GetColumn("UI_EMP").HeaderText = "BHTN nhân viên (" & lstSource.Rows(0).Item("RATE_UI_EMP").ToString & "%)"
                End If
                If lstSource.Rows(0).Item("RATE_UI_COM") IsNot Nothing Then
                    rgGridData.MasterTableView.GetColumn("UI_COM").HeaderText = "BHTN công ty (" & lstSource.Rows(0).Item("RATE_UI_COM").ToString & "%)"
                End If
                If lstSource.Rows(0).Item("RATE_BHTNLD_BNN_EMP") IsNot Nothing Then
                    rgGridData.MasterTableView.GetColumn("BHTNLD_BNN_EMP").HeaderText = "BHTNLD – BNN nhân viên (" & lstSource.Rows(0).Item("RATE_BHTNLD_BNN_EMP").ToString & "%)"
                End If
                If lstSource.Rows(0).Item("RATE_BHTNLD_BNN_COM") IsNot Nothing Then
                    rgGridData.MasterTableView.GetColumn("BHTNLD_BNN_COM").HeaderText = "BHTNLD – BNN Công ty (" & lstSource.Rows(0).Item("RATE_BHTNLD_BNN_COM").ToString & "%)"
                End If
            End If
            rgGridData.DataSource = lstSource
            If isBind Then
                rgGridData.DataBind()
            End If

            Dim lstDataPre As DataTable = (New InsuranceBusiness.InsuranceBusinessClient).GetInsTotalSalary_Summary(Common.Common.GetUsername(), InsCommon.getNumber(txtYear.Text) _
                                        , InsCommon.getNumber(txtMonth.Text) _
                                        , InsCommon.getNumber(ddlINS_ORG_ID.SelectedValue) _
                                         , InsCommon.getString(ddlPeriod.SelectedValue) _
                                         , "1"
                                        )
            Dim lstDataCur As DataTable = (New InsuranceBusiness.InsuranceBusinessClient).GetInsTotalSalary_Summary(Common.Common.GetUsername(), InsCommon.getNumber(txtYear.Text) _
                                      , InsCommon.getNumber(txtMonth.Text) _
                                      , InsCommon.getNumber(ddlINS_ORG_ID.SelectedValue) _
                                      , InsCommon.getString(ddlPeriod.SelectedValue) _
                                       , "0"
                                      )
            If Not (lstDataCur Is Nothing) AndAlso lstDataCur.Rows.Count > 0 Then
                '<%# Translate("1. Số lao động")%>
                InsCommon.SetNumber(txtHC_1_SI_T, lstDataCur.Rows(0)("HC_1_SI_T"))
                InsCommon.SetNumber(txtHC_1_SI_G, lstDataCur.Rows(0)("HC_1_SI_G"))
                InsCommon.SetNumber(txtHC_1_HI_T, lstDataCur.Rows(0)("HC_1_HI_T"))
                InsCommon.SetNumber(txtHC_1_HI_G, lstDataCur.Rows(0)("HC_1_HI_G"))
                InsCommon.SetNumber(txtHC_1_UI_T, lstDataCur.Rows(0)("HC_1_UI_T"))
                InsCommon.SetNumber(txtHC_1_UI_G, lstDataCur.Rows(0)("HC_1_UI_G"))
                ' THEM MOI
                InsCommon.SetNumber(txtHC_1_BHTNLD_T, lstDataCur.Rows(0)("HC_1_TNLD_T"))
                InsCommon.SetNumber(txtHC_1_BHTNLD_G, lstDataCur.Rows(0)("HC_1_TNLD_G"))


                '<%# Translate("2. Tổng quỹ lương")%>
                InsCommon.SetNumber(txtTotalSal_1_SI_T, lstDataCur.Rows(0)("TotalSal_1_SI_T"))
                InsCommon.SetNumber(txtTotalSal_1_SI_G, lstDataCur.Rows(0)("TotalSal_1_SI_G"))
                InsCommon.SetNumber(txtTotalSal_1_HI_T, lstDataCur.Rows(0)("TotalSal_1_HI_T"))
                InsCommon.SetNumber(txtTotalSal_1_HI_G, lstDataCur.Rows(0)("TotalSal_1_HI_G"))
                InsCommon.SetNumber(txtTotalSal_1_UI_T, lstDataCur.Rows(0)("TotalSal_1_UI_T"))
                InsCommon.SetNumber(txtTotalSal_1_UI_G, lstDataCur.Rows(0)("TotalSal_1_UI_G"))
                ' THEM MOI
                InsCommon.SetNumber(txtTotalSal_1_BHTNLD_T, lstDataCur.Rows(0)("TOTALSAL_1_TNLD_T"))
                InsCommon.SetNumber(txtTotalSal_1_BHTNLD_G, lstDataCur.Rows(0)("TOTALSAL_1_TNLD_G"))

                '<%# Translate("3. Số phải đóng")%>
                InsCommon.SetNumber(txtSumit_1_SI_T, lstDataCur.Rows(0)("Sumit_1_SI_T"))
                InsCommon.SetNumber(txtSumit_1_SI_G, lstDataCur.Rows(0)("Sumit_1_SI_G"))
                InsCommon.SetNumber(txtSumit_1_HI_T, lstDataCur.Rows(0)("Sumit_1_HI_T"))
                InsCommon.SetNumber(txtSumit_1_HI_G, lstDataCur.Rows(0)("Sumit_1_HI_G"))
                InsCommon.SetNumber(txtSumit_1_UI_T, lstDataCur.Rows(0)("Sumit_1_UI_T"))
                InsCommon.SetNumber(txtSumit_1_UI_G, lstDataCur.Rows(0)("Sumit_1_UI_G"))
                ' THEM MOI
                InsCommon.SetNumber(txtSumit_1_BHTNLD_T, lstDataCur.Rows(0)("SUMIT_1_TNLD_T"))
                InsCommon.SetNumber(txtSumit_1_BHTNLD_G, lstDataCur.Rows(0)("SUMIT_1_TNLD_G"))

                '<%# Translate("4. Điều chỉnh")%>
                InsCommon.SetNumber(txtAdjust_1_SI_T, lstDataCur.Rows(0)("Adjust_1_SI_T"))
                InsCommon.SetNumber(txtAdjust_1_SI_G, lstDataCur.Rows(0)("Adjust_1_SI_G"))
                InsCommon.SetNumber(txtAdjust_1_HI_T, lstDataCur.Rows(0)("Adjust_1_HI_T"))
                InsCommon.SetNumber(txtAdjust_1_HI_G, lstDataCur.Rows(0)("Adjust_1_HI_G"))
                InsCommon.SetNumber(txtAdjust_1_UI_T, lstDataCur.Rows(0)("Adjust_1_UI_T"))
                InsCommon.SetNumber(txtAdjust_1_UI_G, lstDataCur.Rows(0)("Adjust_1_UI_G"))
                'THEM MOI
                InsCommon.SetNumber(txtAdjust_1_BHTNLD_T, lstDataCur.Rows(0)("ADJUST_1_TNLD_T"))
                InsCommon.SetNumber(txtAdjust_1_BHTNLD_G, lstDataCur.Rows(0)("ADJUST_1_TNLD_G"))
            End If


            If Not (lstDataPre Is Nothing) AndAlso lstDataPre.Rows.Count > 0 Then
                '<%# Translate("1. Số lao động")%>
                InsCommon.SetNumber(txtHC_2_SI_T, lstDataPre.Rows(0)("HC_1_SI_T"))
                InsCommon.SetNumber(txtHC_2_SI_G, lstDataPre.Rows(0)("HC_1_SI_G"))
                InsCommon.SetNumber(txtHC_2_HI_T, lstDataPre.Rows(0)("HC_1_HI_T"))
                InsCommon.SetNumber(txtHC_2_HI_G, lstDataPre.Rows(0)("HC_1_HI_G"))
                InsCommon.SetNumber(txtHC_2_UI_T, lstDataPre.Rows(0)("HC_1_UI_T"))
                InsCommon.SetNumber(txtHC_2_UI_G, lstDataPre.Rows(0)("HC_1_UI_G"))
                ' THEM MOI
                InsCommon.SetNumber(txtHC_2_BHTNLD_T, lstDataPre.Rows(0)("HC_1_TNLD_T"))
                InsCommon.SetNumber(txtHC_2_BHTNLD_G, lstDataPre.Rows(0)("HC_1_TNLD_G"))

                '<%# Translate("2. Tổng quỹ lương")%>
                InsCommon.SetNumber(txtTotalSal_2_SI_T, lstDataPre.Rows(0)("TotalSal_1_SI_T"))
                InsCommon.SetNumber(txtTotalSal_2_SI_G, lstDataPre.Rows(0)("TotalSal_1_SI_G"))
                InsCommon.SetNumber(txtTotalSal_2_HI_T, lstDataPre.Rows(0)("TotalSal_1_HI_T"))
                InsCommon.SetNumber(txtTotalSal_2_HI_G, lstDataPre.Rows(0)("TotalSal_1_HI_G"))
                InsCommon.SetNumber(txtTotalSal_2_UI_T, lstDataPre.Rows(0)("TotalSal_1_UI_T"))
                InsCommon.SetNumber(txtTotalSal_2_UI_G, lstDataPre.Rows(0)("TotalSal_1_UI_G"))
                ' THEM MOI
                InsCommon.SetNumber(txtTotalSal_2_BHTNLD_T, lstDataPre.Rows(0)("TOTALSAL_1_TNLD_T"))
                InsCommon.SetNumber(txtTotalSal_2_BHTNLD_G, lstDataPre.Rows(0)("TOTALSAL_1_TNLD_G"))

                '<%# Translate("3. Số phải đóng")%>
                InsCommon.SetNumber(txtSumit_2_SI_T, lstDataPre.Rows(0)("Sumit_1_SI_T"))
                InsCommon.SetNumber(txtSumit_2_SI_G, lstDataPre.Rows(0)("Sumit_1_SI_G"))
                InsCommon.SetNumber(txtSumit_2_HI_T, lstDataPre.Rows(0)("Sumit_1_HI_T"))
                InsCommon.SetNumber(txtSumit_2_HI_G, lstDataPre.Rows(0)("Sumit_1_HI_G"))
                InsCommon.SetNumber(txtSumit_2_UI_T, lstDataPre.Rows(0)("Sumit_1_UI_T"))
                InsCommon.SetNumber(txtSumit_2_UI_G, lstDataPre.Rows(0)("Sumit_1_UI_G"))
                ' THEM MOI
                InsCommon.SetNumber(txtSumit_2_BHTNLD_T, lstDataPre.Rows(0)("SUMIT_1_TNLD_T"))
                InsCommon.SetNumber(txtSumit_2_BHTNLD_G, lstDataPre.Rows(0)("SUMIT_1_TNLD_G"))
                '<%# Translate("4. Điều chỉnh")%>
                'txtAdjust_2_SI_T.Text = lstDataPre.Rows(0)("Adjust_1_SI_T")
                'txtAdjust_2_SI_G.Text = lstDataPre.Rows(0)("Adjust_1_SI_G")
                'txtAdjust_2_HI_T.Text = lstDataPre.Rows(0)("Adjust_1_HI_T")
                'txtAdjust_2_HI_G.Text = lstDataPre.Rows(0)("Adjust_1_HI_G")
                'txtAdjust_2_UI_T.Text = lstDataPre.Rows(0)("Adjust_1_UI_T")
                'txtAdjust_2_UI_G.Text = lstDataPre.Rows(0)("Adjust_1_UI_G")
            End If
            If rep.CHECK_INS_TOTALSALARY_UNLOCK(InsCommon.getNumber(txtYear.Text) _
                                                   , InsCommon.getNumber(txtMonth.Text) _
                                                   , InsCommon.getNumber(ddlINS_ORG_ID.SelectedValue)) Then

                MainToolBar.Items(4).Enabled = False
                MainToolBar.Items(0).Enabled = False
            Else
                MainToolBar.Items(4).Enabled = True
                MainToolBar.Items(0).Enabled = True
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub LoadComboboxPeriod()
        Try
            Dim lstSource As DataTable = (New InsuranceBusiness.InsuranceBusinessClient).GetInsTotalSalaryPeriod(Common.Common.GetUsername(), If(txtYear.Text = "", Now.Year, InsCommon.getNumber(txtYear.Text)) _
                                        , If(txtMonth.Text = "", Now.Month, InsCommon.getNumber(txtMonth.Text)) _
                                        , InsCommon.getNumber(ddlINS_ORG_ID.SelectedValue) _
                                         , InsCommon.getString(ddlPeriod.SelectedValue) _
                                        )
            FillRadCombobox(ddlPeriod, lstSource, "PERIOD_NM", "PERIOD_VAL", True)
            If ddlPeriod.Items.Count > 0 Then
                ddlPeriod.SelectedValue = lstSource.Rows(0)("PERIOD_VAL")
            End If
            LoadData(True)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ddlINS_ORG_ID_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles ddlINS_ORG_ID.SelectedIndexChanged
        Call LoadComboboxPeriod()
        'Call LoadData()
    End Sub

    Private Sub txtMonth_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMonth.TextChanged
        ddlPeriod.Text = ""
        Call LoadComboboxPeriod()
        'Call LoadData()
    End Sub

    Private Sub txtYear_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtYear.TextChanged
        Call LoadComboboxPeriod()
        'Call LoadData()
    End Sub

    Private Sub ddlPeriod_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles ddlPeriod.SelectedIndexChanged
        Call LoadData(True)
    End Sub
    '----------------------------------------------------------------------
    Private Sub btnCAL_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCAL.Click
        Try
            If ddlINS_ORG_ID.SelectedValue = "" Then
                ShowMessage("Bạn phải chọn đơn vị bảo hiểm?", NotifyType.Warning)
                Exit Sub
            End If
            If InsCommon.getString(ddlINS_ORG_ID.SelectedValue) = "" Then
                ShowMessage("Bạn phải chọn đợt khai báo?", NotifyType.Warning)
                Exit Sub
            End If
            Call CalData()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub btnLock_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLock.Click
        If ddlINS_ORG_ID.SelectedValue = "" Then
            ShowMessage("Bạn phải chọn đơn vị bảo hiểm?", NotifyType.Warning)
            Exit Sub
        End If
        If InsCommon.getString(ddlINS_ORG_ID.SelectedValue) = "" Then
            ShowMessage("Bạn phải chọn đợt khai báo?", NotifyType.Warning)
            Exit Sub
        End If
        Call LockData()
    End Sub
    '----------------------------------------------------------------------
    Private Sub CalData()
        Try
            Dim rep As New InsuranceBusiness.InsuranceBusinessClient
            If rep.CalInsTotalSalary(Common.Common.GetUserName(), InsCommon.getNumber(txtYear.Text) _
                                       , InsCommon.getNumber(txtMonth.Text) _
                                       , InsCommon.getNumber(ddlINS_ORG_ID.SelectedValue) _
                                       , InsCommon.getString(ddlPeriod.SelectedValue)
                                       ) Then
                Refresh("")
            Else
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub CalDataBatch()
        Try
            Dim rep As New InsuranceBusiness.InsuranceBusinessClient
            If rep.CalInsTotalSalaryBatch(Common.Common.GetUserName(), InsCommon.getDate(txtFROMDATE.SelectedDate) _
                                                                     , InsCommon.getDate(txtTODATE.SelectedDate) _
                                                                     , InsCommon.getNumber(ddlINS_ORG_ID.SelectedValue)) Then
                CurrentState = CommonMessage.STATE_NORMAL
                UpdateControlState(CurrentState)
                UpdateToolbarState(CurrentState)
                Refresh("")
            Else
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub LockData()
        Try
            Dim rep As New InsuranceBusiness.InsuranceBusinessClient
            If rep.LockInsTotalSalary(Common.Common.GetUserName(), InsCommon.getNumber(txtYear.Text) _
                                       , InsCommon.getNumber(txtMonth.Text) _
                                       , InsCommon.getNumber(ddlINS_ORG_ID.SelectedValue) _
                                       , InsCommon.getString(ddlPeriod.SelectedValue)
                                       ) Then
                Refresh("")
            Else
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Call Export()
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

                Dim lstSource As DataTable = (New InsuranceBusiness.InsuranceBusinessClient).SPI_INS_TOTALSALARY_EXPORT(Common.Common.GetUsername(), InsCommon.getNumber(txtYear.Text) _
                                    , InsCommon.getNumber(txtMonth.Text) _
                                    , InsCommon.getNumber(ddlINS_ORG_ID.SelectedValue) _
                                     , InsCommon.getString(ddlPeriod.SelectedValue)
                                    )
                lstSource.TableName = "DATA"
                Dim dsData As New DataSet
                If lstSource IsNot Nothing AndAlso lstSource.Rows.Count > 0 Then
                    dsData.Tables.Add(lstSource)
                    Dim dtData As DataTable
                    dtData = New DataTable("DATA2")
                    Dim column1 As DataColumn = New DataColumn("RATE_SI_EMP")
                    column1.DataType = System.Type.GetType("System.String")
                    Dim column2 As DataColumn = New DataColumn("RATE_SI_COM")
                    column2.DataType = System.Type.GetType("System.String")
                    Dim column3 As DataColumn = New DataColumn("RATE_HI_EMP")
                    column3.DataType = System.Type.GetType("System.String")
                    Dim column4 As DataColumn = New DataColumn("RATE_HI_COM")
                    column4.DataType = System.Type.GetType("System.String")
                    Dim column5 As DataColumn = New DataColumn("RATE_UI_EMP")
                    column5.DataType = System.Type.GetType("System.String")
                    Dim column6 As DataColumn = New DataColumn("RATE_UI_COM")
                    column6.DataType = System.Type.GetType("System.String")
                    Dim column7 As DataColumn = New DataColumn("RATE_BHTNLD_BNN_EMP")
                    column7.DataType = System.Type.GetType("System.String")
                    Dim column8 As DataColumn = New DataColumn("RATE_BHTNLD_BNN_COM")
                    column8.DataType = System.Type.GetType("System.String")

                    dtData.Columns.Add(column1)
                    dtData.Columns.Add(column2)
                    dtData.Columns.Add(column3)
                    dtData.Columns.Add(column4)
                    dtData.Columns.Add(column5)
                    dtData.Columns.Add(column6)
                    dtData.Columns.Add(column7)
                    dtData.Columns.Add(column8)

                    Dim row1 As DataRow
                    row1 = dtData.NewRow()
                    If lstSource.Rows(1).Item("RATE_SI_EMP") IsNot Nothing Then
                        row1.Item("RATE_SI_EMP") = "BHXH Nhân viên (" & lstSource.Rows(1).Item("RATE_SI_EMP").ToString & "%)"
                    End If
                    If lstSource.Rows(1).Item("RATE_SI_COM") IsNot Nothing Then
                        row1.Item("RATE_SI_COM") = "BHXH Công ty (" & lstSource.Rows(1).Item("RATE_SI_COM").ToString & "%)"
                    End If
                    If lstSource.Rows(1).Item("RATE_HI_EMP") IsNot Nothing Then
                        row1.Item("RATE_HI_EMP") = "BHYT Nhân viên (" & lstSource.Rows(1).Item("RATE_HI_EMP").ToString & "%)"
                    End If
                    If lstSource.Rows(1).Item("RATE_HI_COM") IsNot Nothing Then
                        row1.Item("RATE_HI_COM") = "BHYT Công ty (" & lstSource.Rows(1).Item("RATE_HI_COM").ToString & "%)"
                    End If
                    If lstSource.Rows(1).Item("RATE_UI_EMP") IsNot Nothing Then
                        row1.Item("RATE_UI_EMP") = "BHTN Nhân viên (" & lstSource.Rows(1).Item("RATE_UI_EMP").ToString & "%)"
                    End If
                    If lstSource.Rows(1).Item("RATE_UI_COM") IsNot Nothing Then
                        row1.Item("RATE_UI_COM") = "BHTN Công ty (" & lstSource.Rows(1).Item("RATE_UI_COM").ToString & "%)"
                    End If
                    If lstSource.Rows(1).Item("RATE_BHTNLD_BNN_EMP") IsNot Nothing Then
                        row1.Item("RATE_BHTNLD_BNN_EMP") = "BHTNLD – BNN Nhân viên (" & lstSource.Rows(1).Item("RATE_BHTNLD_BNN_EMP").ToString & "%)"
                    End If
                    If lstSource.Rows(1).Item("RATE_BHTNLD_BNN_COM") IsNot Nothing Then
                        row1.Item("RATE_BHTNLD_BNN_COM") = "BHTNLD – BNN Công ty (" & lstSource.Rows(1).Item("RATE_BHTNLD_BNN_COM").ToString & "%)"
                    End If

                    dtData.Rows.Add(row1)
                    dsData.Tables.Add(dtData)
                End If
                Dim bCheck = xls.ExportExcelTemplate(Server.MapPath("~/ReportTemplates/" & Request.Params("mid") & "/" & Request.Params("group") & "/InsTotalSalary.xlsx"),
                    "TongQuyLuong", dsData, dtVariable, Me.Response, _error, ExcelCommon.ExportType.Excel)
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
    ''' <lastupdate>
    ''' 17/08/2017 08:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện ItemDataBound của rad grid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgGridData_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgGridData.ItemDataBound
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
                Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
                datarow("DEP_NAME").ToolTip = Utilities.DrawTreeByString(datarow.GetDataKeyValue("ORG_DESC").ToString)
            End If

        Catch ex As Exception

        End Try
    End Sub
#End Region
End Class