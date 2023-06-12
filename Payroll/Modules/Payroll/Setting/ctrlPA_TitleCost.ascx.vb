Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Payroll.PayrollBusiness
Imports Telerik.Web.UI

Public Class ctrlPA_TitleCost
    Inherits Common.CommonView

#Region "Property"
    Public IDSelect As Integer

    Property ListComboData As ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ListComboData")
        End Get
        Set(ByVal value As ComboBoxDataDTO)
            ViewState(Me.ID & "_ListComboData") = value
        End Set
    End Property
#End Region

#Region "Page"

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        rgData.SetFilter()
        rgData.AllowCustomPaging = True
        InitControl()
    End Sub

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            Refresh()
            UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMenu
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create,
                                       ToolbarItem.Edit, ToolbarItem.Save,
                                       ToolbarItem.Cancel, ToolbarItem.Export)
            CType(MainToolBar.Items(2), RadToolBarButton).CausesValidation = True
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New PayrollBusinessClient
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
                        SelectedItemDataGridByKey(rgData, IDSelect, )
                    Case "Cancel"
                        rgData.MasterTableView.ClearSelectedItems()
                End Select
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim rep As New PayrollRepository
        Dim obj As New PATitleCostDTO
        Try
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgData, obj)
            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()
            If isFull Then
                If Sorts IsNot Nothing Then
                    Return rep.GetTitleCost(obj, Sorts).ToTable()
                Else
                    Return rep.GetTitleCost(obj).ToTable()
                End If
            Else
                If Sorts IsNot Nothing Then
                    rgData.DataSource = rep.GetTitleCost(obj, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, "CREATED_DATE desc")
                Else
                    rgData.DataSource = rep.GetTitleCost(obj, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows)
                End If
            End If
            rgData.VirtualItemCount = MaximumRows
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Overrides Sub UpdateControlState()
        Dim rep As New PayrollRepository
        Try
            Select Case CurrentState

                Case CommonMessage.STATE_NORMAL
                    EnableControlAll(False, rdEffectDate, rdExpireDate, rntxtSalaryAllowance,
                                     rntxtSalaryBasic, rntxtSalaryIns, rntxtSalaryOther,
                                     rntxtSalaryRice, rntxtSalarySoft, cboTitle)
                    EnabledGridNotPostback(rgData, True)
                Case CommonMessage.STATE_NEW
                    EnableControlAll(True, rdEffectDate, rdExpireDate, rntxtSalaryAllowance,
                                     rntxtSalaryBasic, rntxtSalaryIns, rntxtSalaryOther,
                                     rntxtSalaryRice, rntxtSalarySoft, cboTitle)
                    EnabledGridNotPostback(rgData, False)
                Case CommonMessage.STATE_EDIT
                    EnableControlAll(True, rdEffectDate, rdExpireDate, rntxtSalaryAllowance,
                                     rntxtSalaryBasic, rntxtSalaryIns, rntxtSalaryOther,
                                     rntxtSalaryRice, rntxtSalarySoft, cboTitle)
                    EnabledGridNotPostback(rgData, False)
                    rgData.Enabled = False

                Case CommonMessage.STATE_DELETE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgData.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgData.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.DeleteTitleCost(lstDeletes) Then
                        Refresh("UpdateView")
                        UpdateControlState()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        UpdateControlState()
                    End If
            End Select
            UpdateToolbarState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Public Overrides Sub BindData()
        Dim dic As New Dictionary(Of String, Control)
        dic.Add("TITLE_ID", cboTitle)
        dic.Add("SAL_BASIC", rntxtSalaryBasic)
        dic.Add("SAL_MOBILE", rntxtSalaryAllowance)
        dic.Add("SAL_RICE", rntxtSalaryRice)
        dic.Add("SAL_INS", rntxtSalaryIns)
        dic.Add("SAL_SOFT", rntxtSalarySoft)
        dic.Add("SAL_OTHER", rntxtSalaryOther)
        dic.Add("EFFECT_DATE", rdEffectDate)
        dic.Add("EXPIRE_DATE", rdExpireDate)
        Utilities.OnClientRowSelectedChanged(rgData, dic)
        Using rep As New PayrollRepository
            Dim dtData = rep.GetTitleList(True)
            FillRadCombobox(cboTitle, dtData, "NAME", "ID")
        End Using
    End Sub

#End Region

#Region "Event"

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objTitleCost As New PATitleCostDTO
        Dim rep As New PayrollRepository
        Dim gID As Decimal
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    ClearControlValue(rdEffectDate, rdExpireDate, rntxtSalaryAllowance,
                                     rntxtSalaryBasic, rntxtSalaryIns, rntxtSalaryOther,
                                     rntxtSalaryRice, rntxtSalarySoft, cboTitle)
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_EDIT
                    CurrentState = CommonMessage.STATE_EDIT
                    UpdateControlState()

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

                Case CommonMessage.TOOLBARITEM_DELETE
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Using xls As New ExcelCommon
                        Dim dtDatas As DataTable
                        dtDatas = CreateDataFilter(True)
                        If dtDatas.Rows.Count > 0 Then
                            rgData.ExportExcel(Server, Response, dtDatas, "TitleCost")
                        End If
                    End Using
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        objTitleCost.EFFECT_DATE = rdEffectDate.SelectedDate
                        objTitleCost.EXPIRE_DATE = rdExpireDate.SelectedDate
                        objTitleCost.SAL_BASIC = rntxtSalaryBasic.Value
                        objTitleCost.SAL_INS = rntxtSalaryIns.Value
                        objTitleCost.SAL_MOBILE = rntxtSalaryAllowance.Value
                        objTitleCost.SAL_OTHER = rntxtSalaryOther.Value
                        objTitleCost.SAL_RICE = rntxtSalaryRice.Value
                        objTitleCost.SAL_SOFT = rntxtSalarySoft.Value
                        objTitleCost.TITLE_ID = cboTitle.SelectedValue
                        Dim total As Decimal = 0
                        If rntxtSalaryAllowance.Value IsNot Nothing Then
                            total += rntxtSalaryAllowance.Value
                        End If
                        If rntxtSalaryIns.Value IsNot Nothing Then
                            total += rntxtSalaryIns.Value
                        End If
                        If rntxtSalaryBasic.Value IsNot Nothing Then
                            total += rntxtSalaryBasic.Value
                        End If
                        If rntxtSalaryRice.Value IsNot Nothing Then
                            total += rntxtSalaryRice.Value
                        End If
                        If rntxtSalaryOther.Value IsNot Nothing Then
                            total += rntxtSalaryOther.Value
                        End If
                        If rntxtSalarySoft.Value IsNot Nothing Then
                            total += rntxtSalarySoft.Value
                        End If
                        objTitleCost.SAL_TOTAL = total
                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                If rep.InsertTitleCost(objTitleCost, gID) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = gID
                                    Refresh("InsertView")
                                    UpdateControlState()
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT
                                objTitleCost.ID = rgData.SelectedValue
                                If rep.ModifyTitleCost(objTitleCost, rgData.SelectedValue) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = objTitleCost.ID
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
            CreateDataFilter()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        If e.ActionName = CommonMessage.TOOLBARITEM_ACTIVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
            CurrentState = CommonMessage.STATE_ACTIVE
            UpdateControlState()
        End If
        If e.ActionName = CommonMessage.TOOLBARITEM_DEACTIVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
            CurrentState = CommonMessage.STATE_DEACTIVE
            UpdateControlState()
        End If
        If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
            CurrentState = CommonMessage.STATE_DELETE
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