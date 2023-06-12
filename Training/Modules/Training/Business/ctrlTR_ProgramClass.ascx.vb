Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI
Imports Training.TrainingBusiness

Public Class ctrlTR_ProgramClass
    Inherits Common.CommonView
    Private store As New TrainingStoreProcedure

#Region "Property"

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            Refresh()
            UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Property IDSelect As Decimal
        Get
            Return ViewState(Me.ID & "_IDSelect")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_IDSelect") = value
        End Set
    End Property

#End Region

#Region "Page"

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        SetGridFilter(rgMain)
        rgMain.AllowCustomPaging = True
        rgMain.PageSize = Common.Common.DefaultPageSize
        'rgMain.ClientSettings.EnablePostBackOnRowClick = True
        InitControl()
    End Sub

    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMain
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create,
                                       ToolbarItem.Edit, ToolbarItem.Seperator,
                                       ToolbarItem.Save, ToolbarItem.Cancel,
                                       ToolbarItem.Delete)
            CType(MainToolBar.Items(3), RadToolBarButton).CausesValidation = True
            CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = False
            CType(Me.MainToolBar.Items(4), RadToolBarButton).Enabled = False
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
                hidProgramID.Value = Request.Params("PROGRAM_ID")
                Dim objProgram As ProgramDTO
                Using rep As New TrainingRepository
                    objProgram = rep.GetProgramById(hidProgramID.Value)
                End Using
                txtProgramName.Text = objProgram.TR_PROGRAM_CODE
                txtCourse.Text = objProgram.TR_COURSE_NAME

                rdfrmdate.SelectedDate = objProgram.START_DATE
                rdtodate.SelectedDate = objProgram.END_DATE
            Else

                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgMain.Rebind()

                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgMain.CurrentPageIndex = 0
                        rgMain.MasterTableView.SortExpressions.Clear()
                        rgMain.Rebind()
                    Case "Cancel"
                        rgMain.MasterTableView.ClearSelectedItems()
                End Select
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub CreateDataFilter()
        Dim rep As New TrainingRepository
        Dim obj As New ProgramClassDTO
        Dim lstClass As New DataTable
        Try
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgMain, obj)
            obj.TR_PROGRAM_ID = Decimal.Parse(hidProgramID.Value)
            Dim Sorts As String = rgMain.MasterTableView.SortExpressions.GetSortString()
            If Sorts IsNot Nothing Then
                lstClass = rep.GetClass(obj, rgMain.CurrentPageIndex, rgMain.PageSize, MaximumRows, Sorts)
            Else
                lstClass = rep.GetClass(obj, rgMain.CurrentPageIndex, rgMain.PageSize, MaximumRows)
            End If

            rgMain.VirtualItemCount = MaximumRows
            rgMain.DataSource = lstClass
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()
        Dim rep As New TrainingRepository
        Try
            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                    EnabledGridNotPostback(rgMain, False)
                    Utilities.EnableRadCombo(cboProvince, True)
                    Utilities.EnableRadCombo(cboDictrict, True)
                    txtAddress.ReadOnly = False
                    rnRATIO.ReadOnly = False
                    txtName.ReadOnly = False
                    EnableRadDatePicker(rdStartDate, True)
                    EnableRadDatePicker(rdEndDate, True)
                    cboProvince.AutoPostBack = True

                    EnableRadTimePicker(rtTimeFrom, True)
                    EnableRadTimePicker(rtTimeTo, True)
                    txtRemark.Enabled = True
                    cboTeacher.Enabled = True
                    txtMailContent.ReadOnly = False

                Case CommonMessage.STATE_NORMAL
                    EnabledGridNotPostback(rgMain, True)
                    Utilities.EnableRadCombo(cboProvince, False)
                    Utilities.EnableRadCombo(cboDictrict, False)
                    txtAddress.ReadOnly = True
                    rnRATIO.ReadOnly = True
                    txtName.ReadOnly = True
                    txtMailContent.ReadOnly = True
                    EnableRadDatePicker(rdStartDate, False)
                    EnableRadDatePicker(rdEndDate, False)

                    cboProvince.AutoPostBack = False
                    txtAddress.Text = ""
                    rnRATIO.Text = ""
                    txtMailContent.Text = ""
                    txtName.Text = ""
                    rdEndDate.SelectedDate = Nothing
                    rdStartDate.SelectedDate = Nothing
                    cboProvince.ClearSelection()
                    cboProvince.Text = ""
                    cboDictrict.ClearSelection()
                    cboDictrict.Text = ""

                    EnableRadTimePicker(rtTimeFrom, False)
                    rtTimeFrom.SelectedDate = Nothing
                    EnableRadTimePicker(rtTimeTo, False)
                    rtTimeTo.SelectedDate = Nothing
                    txtRemark.Enabled = False
                    txtRemark.Text = ""
                    cboTeacher.Enabled = False
                    cboTeacher.ClearValue()
                    nmTotalDay.Value = Nothing
                    nmTotalTime.Value = Nothing
                Case CommonMessage.STATE_EDIT
                    EnabledGridNotPostback(rgMain, False)
                    Utilities.EnableRadCombo(cboProvince, True)
                    Utilities.EnableRadCombo(cboDictrict, True)
                    txtAddress.ReadOnly = False
                    rnRATIO.ReadOnly = False
                    txtMailContent.ReadOnly = False
                    txtName.ReadOnly = False
                    EnableRadDatePicker(rdStartDate, True)
                    EnableRadDatePicker(rdEndDate, True)
                    cboProvince.AutoPostBack = True

                    EnableRadTimePicker(rtTimeFrom, True)
                    EnableRadTimePicker(rtTimeTo, True)
                    txtRemark.Enabled = True
                    cboTeacher.Enabled = True

                Case CommonMessage.STATE_DELETE
                    Dim lstDeletes As New List(Of ProgramClassDTO)
                    For idx = 0 To rgMain.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgMain.SelectedItems(idx)
                        lstDeletes.Add(New ProgramClassDTO With {.ID = item.GetDataKeyValue("ID")})
                    Next
                    If rep.DeleteClass(lstDeletes) Then
                        Refresh("UpdateView")
                        UpdateControlState()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        UpdateControlState()
                    End If
            End Select
            txtName.Focus()
            UpdateToolbarState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Public Overrides Sub BindData()
        Try
            Dim dtData As DataTable
            Using rep As New TrainingRepository
                dtData = rep.GetHuProvinceList(True)
                FillRadCombobox(cboProvince, dtData, "NAME", "ID")
            End Using
            Dim store As New TrainingStoreProcedure
            FillRadCombobox(cboTeacher, store.GET_TEACHER_LIST_BY_PROGRAM(Request.Params("PROGRAM_ID")), "NAME", "ID")

            Dim dic As New Dictionary(Of String, Control)
            dic.Add("NAME", txtName)
            dic.Add("PROVINCE_ID", cboProvince)
            dic.Add("DISTRICT_ID;DISTRICT_NAME", cboDictrict)
            dic.Add("ADDRESS", txtAddress)
            dic.Add("RATIO", rnRATIO)
            dic.Add("START_DATE", rdStartDate)
            dic.Add("END_DATE", rdEndDate)

            dic.Add("TOTAL_DAY", nmTotalDay)
            dic.Add("TIME_FROM", rtTimeFrom)
            dic.Add("TIME_TO", rtTimeTo)
            dic.Add("TOTAL_TIME", nmTotalTime)
            dic.Add("REMARK", txtRemark)
            dic.Add("EMAIL_CONTENT", txtMailContent)
            Utilities.OnClientRowSelectedChanged(rgMain, dic)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Event"
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objClass As New ProgramClassDTO
        Dim rep As New TrainingRepository
        Dim gID As Decimal
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    txtAddress.Text = ""
                    rnRATIO.Text = ""
                    txtMailContent.Text = ""
                    txtName.Text = ""
                    rdEndDate.SelectedDate = Nothing
                    rdStartDate.SelectedDate = Nothing
                    cboProvince.ClearSelection()
                    cboProvince.Text = ""
                    cboDictrict.ClearSelection()
                    cboDictrict.Text = ""

                    nmTotalDay.Value = Nothing
                    cboTeacher.ClearValue()
                    rtTimeFrom.SelectedDate = Nothing
                    rtTimeTo.SelectedDate = Nothing
                    nmTotalTime.Value = Nothing
                    txtRemark.Text = ""

                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_EDIT
                    If rgMain.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    If rgMain.SelectedItems.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    CurrentState = CommonMessage.STATE_EDIT
                    cboProvince_SelectedIndexChanged(Nothing, Nothing)
                    Dim item As GridDataItem = rgMain.SelectedItems(0)
                    Dim StrDISTRICT = item.GetDataKeyValue("DISTRICT_ID").ToString
                    If StrDISTRICT <> "" AndAlso item.GetDataKeyValue("DISTRICT_ID") IsNot Nothing Then
                        cboDictrict.SelectedValue = item.GetDataKeyValue("DISTRICT_ID")
                    End If
                    UpdateControlState()

                Case CommonMessage.TOOLBARITEM_DELETE

                    If rgMain.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_EXPORT
                    GridExportExcel(rgMain, "Class")
                Case CommonMessage.TOOLBARITEM_ACTIVE
                    If rgMain.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_ACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_ACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_DEACTIVE
                    If rgMain.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DEACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_DEACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        objClass.ADDRESS = txtAddress.Text
                        If cboDictrict.SelectedValue <> "" Then
                            objClass.DISTRICT_ID = cboDictrict.SelectedValue

                        End If
                        objClass.END_DATE = rdEndDate.SelectedDate
                        objClass.NAME = txtName.Text

                        If cboProvince.SelectedValue <> "" Then
                            objClass.PROVINCE_ID = cboProvince.SelectedValue

                        End If
                        objClass.START_DATE = rdStartDate.SelectedDate
                        objClass.TR_PROGRAM_ID = hidProgramID.Value

                        If nmTotalDay.Value IsNot Nothing Then
                            objClass.TOTAL_DAY = nmTotalDay.Value
                        End If
                        If rnRATIO.Value IsNot Nothing Then
                            objClass.RATIO = rnRATIO.Value
                        End If
                        If rtTimeFrom.SelectedDate IsNot Nothing Then
                            objClass.TIME_FROM = rtTimeFrom.SelectedDate
                        End If
                        If rtTimeTo.SelectedDate IsNot Nothing Then
                            objClass.TIME_TO = rtTimeTo.SelectedDate
                        End If
                        If nmTotalTime.Value IsNot Nothing Then
                            objClass.TOTAL_TIME = nmTotalTime.Value
                        End If
                        objClass.REMARK = txtRemark.Text.Trim()

                        objClass.EMAIL_CONTENT = txtMailContent.Text.Trim()

                        objClass.TEACHER_ID = ""
                        For Each item In cboTeacher.CheckedItems
                            objClass.TEACHER_ID = objClass.TEACHER_ID + item.Value.ToString + ","
                        Next

                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                If rep.InsertClass(objClass, gID) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = gID
                                    Refresh("InsertView")
                                    UpdateControlState()
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT
                                objClass.ID = rgMain.SelectedValue
                                If rep.ModifyClass(objClass, gID) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = objClass.ID
                                    Refresh("UpdateView")
                                    UpdateControlState()
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select
                    Else
                        ExcuteScript("Resize", "ResizeSplitter()")
                    End If

                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    Refresh("Cancel")
                    UpdateControlState()
            End Select

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
            CurrentState = CommonMessage.STATE_DELETE
            UpdateControlState()
        End If
    End Sub

    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgMain.NeedDataSource
        Try
            CreateDataFilter()

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub rgMain_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rgMain.SelectedIndexChanged
        Try
            If rgMain.SelectedItems.Count = 1 Then
                Dim row As GridDataItem = rgMain.SelectedItems(0)
                Dim timeFrom As New DateTime
                Dim timeTo As New DateTime
                Dim TeacherIdStr As String = row.GetDataKeyValue("TEACHER_ID").ToString
                If Not String.IsNullOrEmpty(TeacherIdStr) Then
                    TeacherIdStr = TeacherIdStr.Remove(TeacherIdStr.Length - 1, 1)
                    Dim arr = TeacherIdStr.Split(New String() {","}, StringSplitOptions.None)
                    For Each TeacherId As String In arr
                        Dim cboItem As RadComboBoxItem = New RadComboBoxItem()
                        cboItem = cboTeacher.Items.FindItemByValue(Decimal.Parse(TeacherId))
                        If cboItem IsNot Nothing Then
                            cboItem.Checked = True
                        End If
                    Next
                End If
                If Not IsDBNull(row.GetDataKeyValue("TIME_FROM")) Then
                    timeFrom = row.GetDataKeyValue("TIME_FROM")
                    rtTimeFrom.SelectedDate = timeFrom
                End If
                If Not IsDBNull(row.GetDataKeyValue("TIME_TO")) Then
                    timeTo = row.GetDataKeyValue("TIME_TO")
                    rtTimeTo.SelectedDate = timeTo
                End If
                If Not IsDBNull(row.GetDataKeyValue("RATIO")) AndAlso IsNumeric(row.GetDataKeyValue("RATIO")) Then
                    Dim Ratio = CDec(row.GetDataKeyValue("RATIO"))
                    rnRATIO.Value = Ratio
                End If
                If rdStartDate.SelectedDate IsNot Nothing AndAlso rdEndDate.SelectedDate IsNot Nothing AndAlso rdEndDate.SelectedDate >= rdStartDate.SelectedDate Then
                    Dim DayDiff As TimeSpan = rdEndDate.SelectedDate.Value.Subtract(rdStartDate.SelectedDate.Value)
                    Dim Days As Decimal = DayDiff.TotalDays + 1
                    If rtTimeFrom.SelectedDate IsNot Nothing AndAlso rtTimeTo.SelectedDate IsNot Nothing AndAlso rtTimeTo.SelectedDate >= rtTimeFrom.SelectedDate Then
                        Dim TimeDiff As TimeSpan = rtTimeTo.SelectedDate.Value.Subtract(rtTimeFrom.SelectedDate.Value)
                        Dim Minutes As Decimal = TimeDiff.TotalMinutes
                        Dim Hours As Decimal = Minutes * Days / 60
                        nmTotalTime.Value = Hours
                    End If
                End If

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cboProvince_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboProvince.SelectedIndexChanged
        Try
            If cboProvince.SelectedValue <> "" Then
                Dim dtData As DataTable
                Using rep As New TrainingRepository
                    dtData = rep.GetHuDistrictList(cboProvince.SelectedValue, True)
                    FillRadCombobox(cboDictrict, dtData, "NAME", "ID")
                End Using
                If CurrentState <> CommonMessage.STATE_NORMAL Then
                    cboDictrict.ClearSelection()
                    cboDictrict.Text = ""
                End If
            Else
                cboDictrict.Items.Clear()
                cboDictrict.ClearSelection()
                cboDictrict.Text = ""
                cboProvince.ClearSelection()
                cboProvince.Text = ""
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rd_SelectedDateChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles rdStartDate.SelectedDateChanged, rdEndDate.SelectedDateChanged, rtTimeFrom.SelectedDateChanged, rtTimeTo.SelectedDateChanged
        If rdStartDate.SelectedDate IsNot Nothing AndAlso rdEndDate.SelectedDate IsNot Nothing AndAlso rdEndDate.SelectedDate >= rdStartDate.SelectedDate Then
            Dim DayDiff As TimeSpan = rdEndDate.SelectedDate.Value.Subtract(rdStartDate.SelectedDate.Value)
            Dim Days As Decimal = DayDiff.TotalDays + 1
            nmTotalDay.Value = Days
            If rtTimeFrom.SelectedDate IsNot Nothing AndAlso rtTimeTo.SelectedDate IsNot Nothing AndAlso rtTimeTo.SelectedDate >= rtTimeFrom.SelectedDate Then
                Dim TimeDiff As TimeSpan = rtTimeTo.SelectedDate.Value.Subtract(rtTimeFrom.SelectedDate.Value)
                Dim Minutes As Decimal = TimeDiff.TotalMinutes
                Dim Hours As Decimal = Minutes * Days / 60
                nmTotalTime.Value = Hours
            Else
                nmTotalTime.Value = Nothing
            End If
        Else
            nmTotalDay.Value = Nothing
            nmTotalTime.Value = Nothing
        End If
    End Sub

#End Region

#Region "Custom"
    Private Sub UpdateToolbarState()
        Try
            ChangeToolbarState()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

End Class