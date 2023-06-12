Imports Aspose.Cells
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Payroll.PayrollBusiness
Imports Common
Imports Telerik.Web.UI

Public Class ctrlPA_SalaryGroup
    Inherits Common.CommonView

#Region "Property"

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
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try

            Refresh()
            UpdateControlState()
            rgData.SetFilter()
            rgData.AllowCustomPaging = True
            rgData.PageSize = Common.Common.DefaultPageSize
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
            Me.MainToolBar = tbarSalaryGroups
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create,
                                       ToolbarItem.Edit,
                                       ToolbarItem.Save, ToolbarItem.Cancel,
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

        Dim obj As New SalaryGroupDTO
        Try
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgData, obj)
            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()

            If isFull Then
                If Sorts IsNot Nothing Then
                    Return PayrollRepositoryStatic.Instance.GetSalaryGroup(obj, Sorts).ToTable
                Else
                    Return PayrollRepositoryStatic.Instance.GetSalaryGroup(obj).ToTable
                End If
            Else
                Dim SalaryGroups As New List(Of SalaryGroupDTO)
                If Sorts IsNot Nothing Then
                    SalaryGroups = PayrollRepositoryStatic.Instance.GetSalaryGroup(obj, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, Sorts)
                Else
                    SalaryGroups = PayrollRepositoryStatic.Instance.GetSalaryGroup(obj, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows)
                End If
                rgData.VirtualItemCount = MaximumRows

                rgData.DataSource = SalaryGroups
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Overrides Sub UpdateControlState()

        Try
            Select Case CurrentState
                Case CommonMessage.STATE_NEW

                    EnabledGridNotPostback(rgData, False)
                    EnableControlAll(True, txtCode, txtName, txtRemark, rdEffectDate, cbIS_COEFFICIENT, cbIS_INCENTIVE, rntxtOrders)
                    'txtCode.ReadOnly = True
                    txtCode.Focus()
                Case CommonMessage.STATE_NORMAL

                    EnabledGridNotPostback(rgData, True)
                    EnableControlAll(False, txtCode, txtName, txtRemark, rdEffectDate, cbIS_COEFFICIENT, cbIS_INCENTIVE, rntxtOrders)
                    txtCode.ReadOnly = True
                    txtName.Focus()
                Case CommonMessage.STATE_EDIT

                    EnabledGridNotPostback(rgData, False)
                    EnableControlAll(True, txtCode, txtName, txtRemark, rdEffectDate, cbIS_COEFFICIENT, cbIS_INCENTIVE, rntxtOrders)
                    txtCode.ReadOnly = True
                    txtName.Focus()
                Case CommonMessage.STATE_DELETE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgData.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgData.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If PayrollRepositoryStatic.Instance.DeleteSalaryGroup(lstDeletes) Then
                        Refresh("UpdateView")
                        UpdateControlState()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_IS_USING), NotifyType.Error)
                    End If
            End Select
            ChangeToolbarState()
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Overrides Sub BindData()
        Dim dic As New Dictionary(Of String, Control)

        dic.Add("CODE", txtCode)
        dic.Add("NAME", txtName)
        dic.Add("EFFECT_DATE", rdEffectDate)
        dic.Add("REMARK", txtRemark)
        dic.Add("IS_COEFFICIENT", cbIS_COEFFICIENT)
        dic.Add("IS_INCENTIVE", cbIS_INCENTIVE)
        dic.Add("ORDERS", rntxtOrders)
        Utilities.OnClientRowSelectedChanged(rgData, dic)
    End Sub

#End Region

#Region "Event"
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objSalaryGroup As New SalaryGroupDTO
        Dim gID As Decimal

        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    ClearControlValue(txtCode, txtName, txtRemark, rdEffectDate, cbIS_COEFFICIENT, cbIS_INCENTIVE)
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

                    UpdateControlState()
                
                Case CommonMessage.TOOLBARITEM_DELETE
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    Dim lstDeletes As New List(Of SalaryGroupDTO)
                    For idx = 0 To rgData.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgData.SelectedItems(idx)
                        lstDeletes.Add(New SalaryGroupDTO With {.ID = Decimal.Parse(item("ID").Text)})
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
                            rgData.ExportExcel(Server, Response, dtData, "SalaryGroup")
                        End If
                    End Using
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        objSalaryGroup.CODE = txtCode.Text
                        objSalaryGroup.NAME = txtName.Text
                        objSalaryGroup.EFFECT_DATE = rdEffectDate.SelectedDate
                        objSalaryGroup.REMARK = txtRemark.Text
                        objSalaryGroup.IS_COEFFICIENT = If(cbIS_COEFFICIENT.Checked, 1, 0)
                        objSalaryGroup.IS_INCENTIVE = If(cbIS_INCENTIVE.Checked, 1, 0)
                        objSalaryGroup.ORDERS = If(rntxtOrders.Text = "", 1, Decimal.Parse(rntxtOrders.Text))
                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                If PayrollRepositoryStatic.Instance.InsertSalaryGroup(objSalaryGroup, gID) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = gID
                                    Refresh("InsertView")
                                    UpdateControlState()
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT
                                objSalaryGroup.ID = rgData.SelectedValue
                                If PayrollRepositoryStatic.Instance.ModifySalaryGroup(objSalaryGroup, gID) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = objSalaryGroup.ID
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
                    ClearControlValue(txtCode, txtName, txtRemark, rdEffectDate, cbIS_COEFFICIENT, cbIS_INCENTIVE)
            End Select
            UpdateControlState()

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        If e.ButtonID = MessageBoxButtonType.ButtonYes Then
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE Then
                CurrentState = CommonMessage.STATE_DELETE
            End If
            UpdateControlState()
        End If
    End Sub
    'Protected Sub RadGrid_ItemDataBound(ByVal sender As Object, ByVal e As GridItemEventArgs) Handles rgData.ItemDataBound
    '    If TypeOf e.Item Is GridDataItem Then
    '        Dim item As GridDataItem = DirectCast(e.Item, GridDataItem)
    '        Dim row As DataRowView = DirectCast(item.DataItem, DataRowView)

    '        Dim chk As CheckBox = DirectCast(item("IS_INCENTIVE").Controls(0), CheckBox)
    '        If item("IS_INCENTIVE").Text = "1" Then
    '            'your code 
    '            item("IS_INCENTIVE").Text = "Yes"
    '        Else
    '            'your code 
    '            item("IS_INCENTIVE").Text = "No"
    '        End If
    '    End If
    'End Sub
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
        Try
            CreateDataFilter()

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub cvalCode_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalCode.ServerValidate

        Dim _validate As New SalaryGroupDTO
        Try
            If CurrentState = CommonMessage.STATE_EDIT Then
                _validate.CODE = txtCode.Text.Trim
                _validate.ID = rgData.SelectedValue
                args.IsValid = PayrollRepositoryStatic.Instance.ValidateSalaryGroup(_validate)

            Else
                _validate.CODE = txtCode.Text.Trim
                args.IsValid = PayrollRepositoryStatic.Instance.ValidateSalaryGroup(_validate)
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
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


End Class