Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Insurance.InsuranceBusiness
Imports Telerik.Web.UI
Imports WebAppLog


Public Class ctrlInsArising
    Inherits Common.CommonView
    'Private WithEvents viewRegister As ctrlShiftPlanningRegister
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Insurance\Modules\Insurance\Business" + Me.GetType().Name.ToString()

#Region "Property & Variable"
    Public Property popup As RadWindow
    Public Property popupId As String

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
                                       ToolbarItem.Save,
                                       ToolbarItem.Seperator, ToolbarItem.Delete,
                                       ToolbarItem.Export)
            CType(Me.MainToolBar.Items(0), RadToolBarButton).CausesValidation = True
            ctrlOrg.AutoPostBack = True
            ctrlOrg.LoadDataAfterLoaded = True
            ctrlOrg.OrganizationType = OrganizationType.OrganizationLocation
            ctrlOrg.CheckBoxes = TreeNodeTypes.None

            txtFROMDATE.SelectedDate = New Date(Now.Year, 1, 1)
            txtTODATE.SelectedDate = New Date(Now.Year, 12, 31)
            txtMONTH.SelectedDate = New Date(Now.Year, Now.Month, 1)

            'rgGridData.GroupingSettings.GroupContinuedFormatString = "... group continued from the previous page"
            'rgGridData.GroupingSettings.GroupContinuesFormatString = "Group continues on the nextpage"
            'rgGridData.GroupingSettings.GroupSplitDisplayFormat = " Showing {0} of {1}  items "

            rgGridData.GroupingSettings.GroupContinuedFormatString = "... "
            rgGridData.GroupingSettings.GroupContinuesFormatString = " "
            rgGridData.GroupingSettings.GroupSplitDisplayFormat = " Hiển thị {0} của {1} bản ghi "

            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            'Me.rgGridData.SetFilter()
            If Not IsPostBack Then
                UpdateControlState(CommonMessage.STATE_NORMAL)
                UpdateToolbarState(CommonMessage.STATE_NORMAL)
                hidID.Value = "0"
                'ctrlOrg.OrganizationType = OrganizationType.OrganizationLocation
                'Call LoadDataGrid()
                Refresh()
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

            dic.Add("ID", hidID)

            Utilities.OnClientRowSelectedChanged(rgGridData, dic)
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")

        Try
            ctrlOrg.OrganizationType = OrganizationType.OrganizationLocation
            'Call LoadDataGrid(True)
            'ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub UpdateControlState(ByVal sState As String)
        Try
            Select Case sState
                Case CommonMessage.STATE_NORMAL
                    Utilities.EnabledGridNotPostback(rgGridData, True)

                Case CommonMessage.STATE_NEW, CommonMessage.STATE_EDIT
                    Utilities.EnabledGridNotPostback(rgGridData, False)


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

                Case CommonMessage.STATE_NEW

                Case CommonMessage.STATE_EDIT

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

    Protected Sub btnCreate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCreate.Click
        Dim rep As New InsuranceRepository
        If txtFROMDATE.SelectedDate Is Nothing Then
            ShowMessage("Từ tháng bắt buộc nhập.", NotifyType.Warning)
            Exit Sub
        End If

        If txtTODATE.SelectedDate Is Nothing Then
            ShowMessage("Đến tháng bắt buộc nhập", NotifyType.Warning)
            Exit Sub
        End If

        If ddlGROUP_ARISING_TYPE_ID.SelectedValue = "4157" Then
            ShowMessage("Không có dữ liệu biến động được tạo", NotifyType.Warning)
            Exit Sub
        End If

        If ddlGROUP_ARISING_TYPE_ID.SelectedValue = "4155" Then
            If rep.INSERT_INS_ARSING_TANG(txtFROMDATE.SelectedDate, txtTODATE.SelectedDate, Decimal.Parse(ctrlOrg.CurrentValue), ctrlOrg.IsDissolve) Then
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
            End If
        End If

        If ddlGROUP_ARISING_TYPE_ID.SelectedValue = "4156" Then
            If rep.INSERT_INS_ARSING_GIAM(txtFROMDATE.SelectedDate, txtTODATE.SelectedDate, Decimal.Parse(ctrlOrg.CurrentValue), ctrlOrg.IsDissolve) Then
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
            End If
        End If

        If ddlGROUP_ARISING_TYPE_ID.SelectedValue = "" Then
            rep.INSERT_INS_ARSING_TANG(txtFROMDATE.SelectedDate, txtTODATE.SelectedDate, Decimal.Parse(ctrlOrg.CurrentValue), ctrlOrg.IsDissolve)
            rep.INSERT_INS_ARSING_GIAM(txtFROMDATE.SelectedDate, txtTODATE.SelectedDate, Decimal.Parse(ctrlOrg.CurrentValue), ctrlOrg.IsDissolve)
            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
        End If

        rgGridData.Rebind()
        rep.Dispose()
    End Sub

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        'Dim objOrgFunction As New OrganizationDTO
        'Dim gID As Decimal
        Dim rep As New InsuranceBusiness.InsuranceBusinessClient
        Dim rep1 As New InsuranceRepository
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    Call ResetForm()
                    CurrentState = CommonMessage.STATE_NEW
                    'txtCODE.Focus()
                Case CommonMessage.TOOLBARITEM_EDIT

                    CurrentState = CommonMessage.STATE_EDIT
                    'txtCODE.Focus()
                    'Case CommonMessage.TOOLBARITEM_ACTIVE, CommonMessage.TOOLBARITEM_DEACTIVE

                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    'FillDropDownList(cboLevel_ID, ListComboData.LIST_ORG_LEVEL, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboLevel_ID.SelectedValue)
                    'FillDataByTree()
                Case CommonMessage.TOOLBARITEM_NEXT
                    Call SaveData()
                    CurrentState = CommonMessage.STATE_NORMAL
                Case CommonMessage.TOOLBARITEM_SAVE

                    If ddlINS_ARISING_TYPE_ID.SelectedValue = "" Then
                        ShowMessage(Translate("Bạn phải nhập Loại biến động."), NotifyType.Warning)
                        Exit Sub
                    End If

                    If rgGridData.MasterTableView.GetSelectedItems().Length = 0 Then
                        ShowMessage(Translate("Bạn chưa chọn dữ liệu để chuyển sang quản lý biến động."), NotifyType.Warning)
                        Exit Sub
                    End If
                    'Check data trước khi chuyển
                    For Each dr As Telerik.Web.UI.GridDataItem In rgGridData.SelectedItems
                        If DirectCast(dr.GetDataKeyValue("EFFECT_DATE"), Date).Day > 15 Then
                            If Format(DirectCast(dr.GetDataKeyValue("EFFECT_DATE"), Date), "yyyyMM") >= Format(txtMONTH.SelectedDate.Value, "yyyyMM") Then
                                ShowMessage("Tháng khai báo phải lớn hơn tháng của ngày hiệu lực. Lỗi: Mã nhân viên (" & dr.GetDataKeyValue("EMPLOYEE_CODE").ToString() & " - " & dr.GetDataKeyValue("EFFECT_DATE") & ")", NotifyType.Warning, 13)
                                Exit Sub
                            End If
                        End If
                        Dim pDate As String
                        Dim check As Integer = rep1.CHECK_INS_ORG(dr.GetDataKeyValue("EMPID"), ddlINS_ARISING_TYPE_ID.SelectedValue, txtMONTH.SelectedDate, pDate)
                        If check = 1 Then
                            ShowMessage("Biến động bảo hiểm đầu tiên phải là TĂNG MỚI", NotifyType.Warning)
                            Exit Sub
                        End If
                        If check = 2 Then
                            ShowMessage(String.Format("{0} {1}", "Tháng biến động mới phải lớn hơn tháng biến động tăng mới ", pDate), NotifyType.Warning)
                            Exit Sub
                        End If

                        Dim type_code As String = rep1.CHECK_INS_ORG_TANG_GIAM(ddlINS_ARISING_TYPE_ID.SelectedValue)

                        If dr.GetDataKeyValue("NEW_SAL") > 0 Then
                            If type_code = "GIAM" Then
                                ShowMessage("Biến động tăng và điều chỉnh thì không thể khai báo GIẢM bảo hiểm", NotifyType.Warning)
                                Exit Sub
                            End If
                        End If

                        If dr.GetDataKeyValue("NEW_SAL") = 0 Then
                            If type_code <> "GIAM" Then
                                ShowMessage("Biến động giảm thì không thể khai báo tăng và điều chỉnh bảo hiểm", NotifyType.Warning)
                                Exit Sub
                            End If
                        End If

                    Next
                    ctrlMessageBox.MessageText = Translate("Bạn có muốn chuyển sang quản lý biến động?")
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_SAVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                    CurrentState = CommonMessage.STATE_NORMAL
                Case CommonMessage.TOOLBARITEM_DELETE
                    If rgGridData.MasterTableView.GetSelectedItems().Length = 0 Then
                        ShowMessage(Translate("Bạn chưa chọn nội dung cần xóa."), NotifyType.Warning)
                        Exit Sub
                    End If
                    ctrlMessageBox.MessageText = Translate("Bạn có muốn xóa dữ liệu ?")
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                    CurrentState = CommonMessage.STATE_NORMAL
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
                If DeleteData() Then
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                    Call ResetForm()
                    rgGridData.Rebind()
                Else
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                    rgGridData.Rebind()
                End If
            End If
            If e.ActionName = CommonMessage.TOOLBARITEM_SAVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                If SaveData() Then
                    Refresh("UpdateView")
                    ResetForm()
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                    rgGridData.Rebind()
                Else
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                    rgGridData.Rebind()
                End If
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub


    Private Sub ctrlOrg_SelectedNodeChanged(sender As Object, e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            rgGridData.CurrentPageIndex = 0
            rgGridData.Rebind()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Function & Sub"

    Private Sub ResetForm()
        Try
            hidID.Value = "0"
        Catch ex As Exception
        End Try
    End Sub

    Private Function SaveData() As Boolean
        Try
            Dim rep As New InsuranceBusiness.InsuranceBusinessClient

            If rgGridData.MasterTableView.GetSelectedItems().Length = 0 Then
                ShowMessage(Translate("Bạn chưa chọn nội dung cần chuyển sang Quản lý biến động!"), NotifyType.Warning)
                Exit Function
            End If

            Dim isFail As Integer = 0
            Dim isResult As Integer = 0

            For i As Integer = 0 To rgGridData.SelectedItems.Count - 1
                Dim item As GridDataItem = rgGridData.SelectedItems(i)
                Dim idStr = item.GetDataKeyValue("ID").ToString
                Dim empIdStr = item.GetDataKeyValue("EMPID").ToString
                Dim id As Integer = Integer.Parse(idStr)
                Dim empid As Integer = Integer.Parse(empIdStr)
                Dim DECLAREDDATE As Date = New Date(txtMONTH.SelectedDate.Value.Year, txtMONTH.SelectedDate.Value.Month, 1)
                isResult = rep.UpdateInsArising(Common.Common.GetUsername(), DECLAREDDATE, id, empid, InsCommon.getNumber(ddlINS_ARISING_TYPE_ID.SelectedValue))
                If isResult = 0 Then
                    isFail = 1
                End If
            Next
            If isFail = 0 Then
                Return True
                'Refresh("UpdateView")
            Else
                Return False
                'ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
            End If
        Catch ex As Exception

        End Try
    End Function

    Private Sub SaveNote()
        Try
            Dim rep As New InsuranceBusiness.InsuranceBusinessClient

            If rgGridData.MasterTableView.GetSelectedItems().Length = 0 Then
                ShowMessage(Translate("Bạn chưa chọn nội dung cần lưu."), NotifyType.Warning)
                Exit Sub
            End If

            Dim isFail As Integer = 0
            Dim isResult As Integer = 0
            For i As Integer = 0 To rgGridData.SelectedItems.Count - 1
                Dim item As GridDataItem = rgGridData.SelectedItems(i)
                Dim id As Integer = Integer.Parse(item("ID").Text)
                Dim txtBox As RadTextBox = DirectCast(item("NOTE").FindControl("rtbNote"), RadTextBox)
                Dim note = txtBox.Text
                isResult = rep.UpdateInsArisingNote(Common.Common.GetUsername(), id, note)
                If isResult = 0 Then
                    isFail = 1
                End If
            Next
            If isFail = 0 Then
                Refresh("UpdateView")
            Else
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Function DeleteData() As Boolean
        Try
            Dim rep As New InsuranceBusiness.InsuranceBusinessClient
            'objOrgFunction.ID = Decimal.Parse(hidID.Value)

            Dim isFail As Integer = 0
            Dim isResult As Boolean
            For i As Integer = 0 To rgGridData.SelectedItems.Count - 1
                Dim item As GridDataItem = rgGridData.SelectedItems(i)
                Dim id As Integer = Integer.Parse(item.GetDataKeyValue("ID"))
                isResult = rep.DeleteInsArising(Common.Common.GetUsername(), id)
                If isResult = False Then
                    isFail = 1
                End If
            Next
            If isFail = 0 Then
                'Refresh("UpdateView")
                Return True
            Else
                Return False
                'ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
            End If
        Catch ex As Exception

        End Try
    End Function

    Private Sub GetDataCombo()
        Try
            Dim rep As New InsuranceBusinessClient
            Dim dtData = rep.GetOtherList("GROUP_ARISING_TYPE", Common.Common.SystemLanguage.Name, False) 'Loai nhóm bien dong
            FillRadCombobox(ddlGROUP_ARISING_TYPE_ID, dtData, "NAME", "ID", False)

            dtData = rep.GetInsListChangeType() 'Loai bien dong
            FillRadCombobox(ddlINS_ARISING_TYPE_ID, dtData, "ARISING_NAME", "ID", False)




            'dtData = rep.GetInsListInsurance(False) 'Don vi bao hiem
            Dim lstSource As DataTable = rep.GetInsListInsuranceByUsername("ADMIN", False) 'Don vi bao hiem
            FillRadCombobox(ddlINSORG, lstSource, "NAME", "ID", False)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub LoadDataGrid(Optional ByVal IsDataBind As Boolean = True)
        Try
            '--------------------ThanhNT added 22/12/2015---------------------------
            'Add condition for query data in db-
            'Get list org checked
            'Dim lstOrg = ctrlOrg.CheckedValueKeys
            'Dim lstOrgStr = ""
            'If ctrlOrg.CheckedValueKeys.Count > 0 Then
            '    For i As Integer = 0 To lstOrg.Count - 1
            '        If i = lstOrg.Count - 1 Then
            '            lstOrgStr = lstOrgStr & lstOrg(i).ToString
            '        Else
            '            lstOrgStr = lstOrgStr & lstOrg(i).ToString & ","
            '        End If
            '    Next
            'End If
            Dim org_ID As Decimal
            If ctrlOrg.CurrentValue IsNot Nothing Then
                org_ID = Decimal.Parse(ctrlOrg.CurrentValue)
            Else
                rgGridData.DataSource = New List(Of EmployeeDTO)
                Exit Sub
            End If

            Dim is_Disslove = If(ctrlOrg.IsDissolve, 1, 0)
            Dim lstSource As DataTable = (New InsuranceBusiness.InsuranceBusinessClient).GetInsArising(Common.Common.GetUsername(), txtFROMDATE.SelectedDate, txtTODATE.SelectedDate _
                                                                                                   , InsCommon.getNumber(ddlGROUP_ARISING_TYPE_ID.SelectedValue) _
                                                                                                   , org_ID _
                                                                                                   , InsCommon.getNumber(ddlINSORG.SelectedValue), is_Disslove)



            Dim maximumRows As Double = 0
            Dim startRowIndex As Double = 0
            Dim dtb As New DataTable
            dtb.TableName = "TBL"
            dtb.Columns.Add("ID", GetType(String))
            dtb.Columns.Add("EMPID", GetType(String))
            dtb.Columns.Add("EMPLOYEE_CODE", GetType(String))
            dtb.Columns.Add("FULL_NAME", GetType(String))
            dtb.Columns.Add("DEP_NAME", GetType(String))
            dtb.Columns.Add("TITLE_NAME", GetType(String))
            dtb.Columns.Add("TITLE_BHXH_NAME", GetType(String))
            dtb.Columns.Add("EFFECT_DATE", GetType(Date))
            dtb.Columns.Add("MONTH_CONFIRM", GetType(String))
            dtb.Columns.Add("TO_MONTH_CONFIRM", GetType(String))
            dtb.Columns.Add("ARISING_TYPE_NAME", GetType(String))
            dtb.Columns.Add("OLD_SAL", GetType(Decimal))
            dtb.Columns.Add("NEW_SAL", GetType(Decimal))
            dtb.Columns.Add("SALARY_HD", GetType(Decimal))
            dtb.Columns.Add("SALARY_PC", GetType(Decimal))
            dtb.Columns.Add("SI", GetType(Boolean))
            dtb.Columns.Add("HI", GetType(Boolean))
            dtb.Columns.Add("UI", GetType(Boolean))
            dtb.Columns.Add("ARISING_TYPE_ID", GetType(Decimal))
            dtb.Columns.Add("ARISING_GROUP_TYPE", GetType(String))
            dtb.Columns.Add("REASONS", GetType(String))
            dtb.Columns.Add("NOTE", GetType(String))
            dtb.Columns.Add("BHTNLD_BNN", GetType(Boolean))
            dtb.Columns.Add("ORG_DESC", GetType(String))
            dtb.Columns.Add("SOCIAL_NUMBER", GetType(String))
            dtb.Columns.Add("U_INSURANCE_NAME", GetType(String))
            dtb.Columns.Add("EMP_STATUS_NAME", GetType(String))

            If lstSource.Rows.Count > 0 Then
                Dim filterExp = rgGridData.MasterTableView.FilterExpression
                If lstSource.Select(filterExp).AsEnumerable.Count = 0 Then
                    rgGridData.DataSource = dtb
                    rgGridData.MasterTableView.GroupsDefaultExpanded = True
                    rgGridData.MasterTableView.VirtualItemCount = lstSource.Rows.Count
                    rgGridData.CurrentPageIndex = rgGridData.MasterTableView.CurrentPageIndex
                    If IsDataBind Then
                        rgGridData.DataBind()
                    End If
                    Return
                Else
                    lstSource = lstSource.Select(filterExp).AsEnumerable.CopyToDataTable
                End If
                startRowIndex = IIf(lstSource.Rows.Count <= rgGridData.PageSize, 0, rgGridData.CurrentPageIndex * rgGridData.PageSize)
                maximumRows = IIf(lstSource.Rows.Count <= rgGridData.PageSize, lstSource.Rows.Count, Math.Min(startRowIndex + rgGridData.PageSize, lstSource.Rows.Count))
                For i As Integer = startRowIndex To maximumRows - 1
                    Dim dr As DataRow = lstSource.Rows(i)
                    Dim drI As DataRow = dtb.NewRow
                    drI("ID") = dr("ID")
                    drI("EMPID") = dr("EMPID")
                    drI("EMPLOYEE_CODE") = dr("EMPLOYEE_CODE")
                    drI("FULL_NAME") = dr("FULL_NAME")
                    drI("DEP_NAME") = dr("DEP_NAME")
                    drI("TITLE_NAME") = dr("TITLE_NAME")
                    drI("EFFECT_DATE") = dr("EFFECT_DATE")
                    drI("MONTH_CONFIRM") = dr("MONTH_CONFIRM")
                    drI("TO_MONTH_CONFIRM") = dr("TO_MONTH_CONFIRM")
                    drI("ARISING_TYPE_NAME") = dr("ARISING_TYPE_NAME")
                    drI("OLD_SAL") = dr("OLD_SAL")
                    drI("NEW_SAL") = dr("NEW_SAL")
                    drI("SALARY_HD") = dr("SALARY_HD")
                    drI("SALARY_PC") = dr("SALARY_PC")
                    drI("SI") = IIf(dr("SI") = "0", False, True)
                    drI("HI") = IIf(dr("HI") = "0", False, True)
                    drI("UI") = IIf(dr("UI") = "0", False, True)
                    drI("ARISING_TYPE_ID") = dr("ARISING_TYPE_ID")
                    drI("ARISING_GROUP_TYPE") = dr("ARISING_GROUP_TYPE")
                    drI("REASONS") = dr("REASONS")
                    drI("NOTE") = dr("NOTE")
                    drI("BHTNLD_BNN") = IIf(dr("BHTNLD_BNN") = "0", False, True)
                    drI("ORG_DESC") = dr("ORG_DESC")
                    drI("SOCIAL_NUMBER") = dr("SOCIAL_NUMBER")
                    drI("U_INSURANCE_NAME") = dr("U_INSURANCE_NAME")
                    drI("EMP_STATUS_NAME") = dr("EMP_STATUS_NAME")
                    dtb.Rows.Add(drI)
                Next
            End If

            rgGridData.DataSource = dtb
            rgGridData.MasterTableView.GroupsDefaultExpanded = True
            rgGridData.MasterTableView.VirtualItemCount = lstSource.Rows.Count
            rgGridData.CurrentPageIndex = rgGridData.MasterTableView.CurrentPageIndex
            If IsDataBind Then
                rgGridData.DataBind()
            End If

        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Shared Function InlineAssignHelper(Of T)(ByRef target As T, ByVal value As T) As T
        target = value
        Return value
    End Function

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
            '--------------------ThanhNT added 22/12/2015---------------------------
            'Add condition for query data in db-
            'Get list org checked
            Dim org_ID = Decimal.Parse(ctrlOrg.CurrentValue)
            Dim is_Disslove = If(ctrlOrg.IsDissolve, 1, 0)
            Using xls As New ExcelCommon

                Dim lstSource As DataTable = (New InsuranceBusiness.InsuranceBusinessClient).GetInsArising(Common.Common.GetUsername(), txtFROMDATE.SelectedDate, txtTODATE.SelectedDate _
                                                                                                 , InsCommon.getNumber(ddlGROUP_ARISING_TYPE_ID.SelectedValue) _
                                                                                                 , org_ID _
                                                                                                 , InsCommon.getNumber(ddlINSORG.SelectedValue), is_Disslove)


                lstSource.TableName = "TABLE"

                Dim bCheck = xls.ExportExcelTemplate(Server.MapPath("~/ReportTemplates/" & Request.Params("mid") & "/" & Request.Params("group") & "/InsArising.xlsx"),
                    "BienDongBH", lstSource, dtVariable, Response, _error, ExcelCommon.ExportType.Excel)
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