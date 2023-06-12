Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Payroll.PayrollBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlPA_WorkStandard
    Inherits Common.CommonView
    Dim _myLog As New MyLog()
    Dim _classPath As String = "Payroll/Module/Payroll/List/" + Me.GetType().Name.ToString()
#Region "Property"
    Property orgid As Integer
        Get
            Return ViewState(Me.ID & "_orgid")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_orgid") = value
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
    Property WorkStandard As List(Of Work_StandardDTO)
        Get
            Return ViewState(Me.ID & "_WorkStandard")
        End Get
        Set(ByVal value As List(Of Work_StandardDTO))
            ViewState(Me.ID & "_WorkStandard") = value
        End Set
    End Property
#End Region

#Region "Page"
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            If Not IsPostBack Then
                ctrlOrg.AutoPostBack = True
                ctrlOrg.LoadDataAfterLoaded = True
                ctrlOrg.OrganizationType = OrganizationType.OrganizationLocation
                ctrlOrg.CheckBoxes = TreeNodeTypes.None
                Refresh()
                UpdateControlState()
                rgData.SetFilter()
                rgData.AllowCustomPaging = True
                rgData.PageSize = Common.Common.DefaultPageSize
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            InitControl()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarSalaryTypes
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create,
                                       ToolbarItem.Edit,
                                       ToolbarItem.Save, ToolbarItem.Cancel,
                                       ToolbarItem.Active, ToolbarItem.Deactive,
                                       ToolbarItem.Delete, ToolbarItem.Export)
            CType(MainToolBar.Items(2), RadToolBarButton).CausesValidation = True
            CType(Me.MainToolBar.Items(2), RadToolBarButton).Enabled = False
            CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = False
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
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgData.Rebind()
                        SelectedItemDataGridByKey(rgData, IDSelect, , rgData.CurrentPageIndex)

                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgData.CurrentPageIndex = 0
                        rgData.MasterTableView.SortExpressions.Clear()
                        rgData.Rebind()
                        SelectedItemDataGridByKey(rgData, IDSelect)
                    Case "Cancel"
                        rgData.MasterTableView.ClearSelectedItems()
                End Select
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim _filter As New Work_StandardDTO
        _filter.param = New ParamDTO
        Try
            Dim MaximumRows As Integer
            If ctrlOrg.CurrentValue IsNot Nothing Then
                _filter.param.ORG_ID = Utilities.ObjToDecima(ctrlOrg.CurrentValue)
            End If
            _filter.param.IS_DISSOLVE = ctrlOrg.IsDissolve
            SetValueObjectByRadGrid(rgData, _filter)
            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()

            Using rep As New PayrollRepository
                If isFull Then
                    If Sorts IsNot Nothing Then
                        Return rep.GetWorkStandard(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, Sorts).ToTable()
                    Else
                        Return rep.GetWorkStandard(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows).ToTable()
                    End If
                Else
                    If Sorts IsNot Nothing Then
                        WorkStandard = rep.GetWorkStandard(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, Sorts)
                    Else
                        WorkStandard = rep.GetWorkStandard(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows)
                    End If
                    rgData.VirtualItemCount = MaximumRows
                    rgData.DataSource = WorkStandard
                End If
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Overrides Sub UpdateControlState()

        Try
            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                    EnabledGridNotPostback(rgData, False)
                    EnableControlAll(True, rntxtYEAR, cboLabor, cboPeriod, txtRemark, txtWordStandard)
                    rntxtYEAR.Focus()
                    ctrlOrg.Enabled = False
                Case CommonMessage.STATE_NORMAL
                    EnabledGridNotPostback(rgData, True)
                    EnableControlAll(False, rntxtYEAR, cboLabor, cboPeriod, txtRemark, txtWordStandard, txtCongty)
                    cboPeriod.Focus()
                    ctrlOrg.Enabled = True
                Case CommonMessage.STATE_EDIT
                    EnabledGridNotPostback(rgData, False)
                    'rgData.Enabled = False
                    EnableControlAll(True, rntxtYEAR, cboLabor, cboPeriod, txtRemark, txtWordStandard)
                    cboPeriod.Focus()
                    ctrlOrg.Enabled = False
                Case CommonMessage.STATE_DELETE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgData.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgData.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    Using rep As New PayrollRepository
                        If rep.DeleteWorkStandard(lstDeletes) Then
                            Refresh("UpdateView")
                            UpdateControlState()
                        Else
                            CurrentState = CommonMessage.STATE_NORMAL
                            ShowMessage(Translate(CommonMessage.MESSAGE_IS_USING), NotifyType.Error)
                        End If
                    End Using
                    ClearControlValue(rntxtYEAR, cboPeriod, txtRemark, txtWordStandard, cboLabor)
                Case CommonMessage.STATE_ACTIVE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgData.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgData.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    Using rep As New PayrollRepository
                        If rep.ActiveWorkStandard(lstDeletes, "A") Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            CurrentState = CommonMessage.STATE_NORMAL
                            rgData.Rebind()
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                        End If

                    End Using

                Case CommonMessage.STATE_DEACTIVE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgData.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgData.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    Using rep As New PayrollRepository
                        If rep.ActiveWorkStandard(lstDeletes, "I") Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            CurrentState = CommonMessage.STATE_NORMAL
                            rgData.Rebind()
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                        End If

                    End Using
            End Select
            ChangeToolbarState()
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Overrides Sub BindData()
        Dim dic As New Dictionary(Of String, Control)
        dic.Add("ORG_NAME", txtCongty)
        dic.Add("YEAR", rntxtYEAR)
        dic.Add("OBJECT_ID", cboLabor)
        dic.Add("PERIOD_ID", cboPeriod)
        dic.Add("REMARK", txtRemark)
        dic.Add("PERIOD_STANDARD", txtWordStandard)

        Using rep As New PayrollRepository
            FillRadCombobox(cboLabor, rep.GetOtherList("OBJECT_LABOR"), "NAME", "ID", False)
        End Using
        Utilities.OnClientRowSelectedChanged(rgData, dic)
    End Sub
#End Region

#Region "Event"
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objWorkStandardDTO As New Work_StandardDTO
        Dim gID As Decimal
        Dim orgID As String = String.Empty
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    Using rep As New PayrollRepository
                        If Not rep.IsCompanyLevel(ctrlOrg.CurrentValue) Then
                            ShowMessage(Translate("Vui lòng chỉ thiết lập ngày công chuẩn theo công ty"), NotifyType.Warning)
                            Exit Sub
                        End If
                    End Using
                    CurrentState = CommonMessage.STATE_NEW
                    ClearControlValue(rntxtYEAR, cboPeriod, txtRemark, txtWordStandard, cboLabor, txtCongty)
                    txtCongty.Text = ctrlOrg.CurrentText
                    txtCongty.Enabled = False
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_EDIT
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rgData.SelectedItems.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    CurrentState = CommonMessage.STATE_EDIT
                    txtCongty.Text = ctrlOrg.CurrentText
                    txtCongty.Enabled = False
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_DELETE
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    Dim lstID As New List(Of Decimal)
                    For idx = 0 To rgData.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgData.SelectedItems(idx)
                        lstID.Add(item.GetDataKeyValue("ID"))
                    Next

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        dtData = CreateDataFilter(True)
                        If dtData.Rows.Count > 0 Then
                            rgData.ExportExcel(Server, Response, dtData, "WorkStandard")
                        End If
                    End Using
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        objWorkStandardDTO.YEAR = rntxtYEAR.Value
                        objWorkStandardDTO.OBJECT_ID = cboLabor.SelectedValue
                        objWorkStandardDTO.REMARK = txtRemark.Text
                        objWorkStandardDTO.PERIOD_ID = cboPeriod.SelectedValue
                        objWorkStandardDTO.Period_standard = Decimal.Parse(txtWordStandard.Text)
                        Using rep As New PayrollRepository
                            objWorkStandardDTO.ORG_ID = ctrlOrg.CurrentValue
                            Select Case CurrentState

                                Case CommonMessage.STATE_NEW
                                    objWorkStandardDTO.ACTFLG = "A"
                                    If rep.ValidateWorkStandard(objWorkStandardDTO) Then
                                        If rep.InsertWorkStandard(objWorkStandardDTO, gID) Then
                                            CurrentState = CommonMessage.STATE_NORMAL
                                            IDSelect = gID
                                            Refresh("InsertView")
                                            UpdateControlState()
                                        Else
                                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                        End If
                                    Else
                                        ShowMessage(Translate("Kỳ lương đã được thiết lập ngày công chế độ"), Utilities.NotifyType.Information)
                                    End If

                                Case CommonMessage.STATE_EDIT
                                    objWorkStandardDTO.ID = rgData.SelectedValue
                                    If rep.ModifyWorkStandard(objWorkStandardDTO, gID) Then
                                        CurrentState = CommonMessage.STATE_NORMAL
                                        IDSelect = objWorkStandardDTO.ID
                                        Refresh("UpdateView")
                                        UpdateControlState()
                                    Else
                                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                    End If
                            End Select
                        End Using
                    Else
                        ExcuteScript("Resize", "ResizeSplitter()")
                    End If

                Case CommonMessage.TOOLBARITEM_ACTIVE
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_ACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_ACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_DEACTIVE
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DEACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_DEACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    ClearControlValue(rntxtYEAR, cboLabor, cboPeriod, txtRemark, txtWordStandard, txtCongty)
            End Select
            UpdateControlState()

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        If e.ButtonID = MessageBoxButtonType.ButtonYes AndAlso e.ActionName = CommonMessage.TOOLBARITEM_DELETE Then
            CurrentState = CommonMessage.STATE_DELETE
            UpdateControlState()
        End If
        If e.ActionName = CommonMessage.TOOLBARITEM_ACTIVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
            CurrentState = CommonMessage.STATE_ACTIVE
            UpdateControlState()
        End If
        If e.ActionName = CommonMessage.TOOLBARITEM_DEACTIVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
            CurrentState = CommonMessage.STATE_DEACTIVE
            UpdateControlState()
        End If
    End Sub

    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
        Try
            CreateDataFilter()

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub ctrlOrganization_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                    If ctrlOrg.CurrentValue IsNot Nothing Then
                        orgid = Decimal.Parse(ctrlOrg.CurrentValue)
                    Else
                        ShowMessage("Chưa chọn phòng ban?", NotifyType.Warning)
                        Exit Sub
                    End If
                    'orgItem = (From p In Organizations Where p.ID = orgid).SingleOrDefault
                    'Code = orgItem.CODE & "_" & Year & "_"
                    'txtCode.Text = rep.AutoGenCode(Code, "HU_WELFARE_LIST", "CODE")
            End Select
            CreateDataFilter(False)
            Dim startTime As DateTime = DateTime.UtcNow
            rgData.CurrentPageIndex = 0
            rgData.Rebind()
            ClearControlValue(cboLabor, cboPeriod, rntxtYEAR, txtWordStandard, txtRemark)
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub rgData_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgData.SelectedIndexChanged
        Try
            If rgData.SelectedItems.Count Then
                Dim sItem As GridDataItem = rgData.SelectedItems(0)
                If sItem.GetDataKeyValue("ID").ToString <> "" Then
                    Dim item = (From p In WorkStandard Where p.ID = Decimal.Parse(sItem.GetDataKeyValue("ID").ToString) Select p).FirstOrDefault
                    If item IsNot Nothing Then
                        If item.OBJECT_ID IsNot Nothing Then
                            cboLabor.Text = item.OBJECT_NAME
                            cboLabor.SelectedValue = item.OBJECT_ID
                        Else
                            cboLabor.Text = " "
                        End If
                        If item.PERIOD_ID IsNot Nothing Then
                            cboPeriod.Text = item.PERIOD_NAME
                            cboPeriod.SelectedValue = item.PERIOD_ID
                        Else
                            cboPeriod.Text = " "
                        End If
                        If item.YEAR IsNot Nothing Then
                            rntxtYEAR.Text = item.YEAR
                        End If
                        If item.Period_standard IsNot Nothing Then
                            txtWordStandard.Text = item.Period_standard
                        End If
                        txtCongty.Text = item.ORG_NAME
                        txtRemark.Text = item.REMARK
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

#Region "Custom"
    Public Function ParseBoolean(ByVal dValue As String) As Boolean
        If String.IsNullOrEmpty(dValue) Then
            Return False
        Else
            Return If(dValue = "1", True, False)
        End If
    End Function
#End Region
    Protected Sub rntxtYEAR_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rntxtYEAR.TextChanged
        Using rep As New PayrollRepository
            FillRadCombobox(cboPeriod, rep.GetPeriodbyYear(rntxtYEAR.Value), "PERIOD_NAME", "ID", False)
            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()
            Dim WorkStandard As New List(Of Work_StandardDTO)
            WorkStandard = rep.GetWorkStandardByYear(rntxtYEAR.Value)
            rgData.DataSource = WorkStandard
        End Using
    End Sub
End Class