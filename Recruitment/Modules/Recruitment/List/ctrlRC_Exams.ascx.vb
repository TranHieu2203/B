Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Recruitment.RecruitmentBusiness
Imports Telerik.Web.UI

Public Class ctrlRC_Exams
    Inherits Common.CommonView
    Protected WithEvents ExamsDtlView As ViewBase

#Region "Property"

    Property IsLoad As Boolean
        Get
            Return ViewState(Me.ID & "_IsLoad")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_IsLoad") = value
        End Set
    End Property

#End Region

#Region "Page"

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            Refresh()
            UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            rgData.SetFilter()
            rgData.AllowCustomPaging = True
            rgData.PageSize = Common.Common.DefaultPageSize
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            InitControl()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Public Overrides Sub BindData()
        Try
            Dim dic As New Dictionary(Of String, Control)
            dic.Add("ID", hidID)
            dic.Add("NAME", txtName)
            dic.Add("EXAMS_ORDER", rntxtExamsOrder)
            dic.Add("POINT_LADDER", rntxtPointLadder)
            dic.Add("POINT_PASS", rntxtPointPass)
            dic.Add("IS_PV", chkIsPV)
            dic.Add("COEFFICIENT", txtHeso)
            Utilities.OnClientRowSelectedChanged(rgData, dic)
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMain
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create, ToolbarItem.Edit, ToolbarItem.Seperator,
                                       ToolbarItem.Save, ToolbarItem.Cancel, ToolbarItem.Delete)
            CType(MainToolBar.Items(3), RadToolBarButton).CausesValidation = True
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()
        Dim rep As New RecruitmentRepository
        Try
            Select Case CurrentState
                Case CommonMessage.STATE_NORMAL
                    txtName.Enabled = False
                    rntxtExamsOrder.Enabled = False
                    rntxtPointLadder.Enabled = False
                    rntxtPointPass.Enabled = False
                    chkIsPV.Enabled = False
                    EnableRadCombo(cboTitle, True)
                    EnabledGridNotPostback(rgData, True)
                    ctrlOrg.Enabled = True
                Case CommonMessage.STATE_NEW, CommonMessage.STATE_EDIT
                    txtName.Enabled = True
                    rntxtExamsOrder.Enabled = True
                    If chkIsPV.Checked Then
                        rntxtPointLadder.Enabled = False
                        rntxtPointPass.Enabled = False
                        rntxtPointLadder.Value = 0
                        rntxtPointPass.Value = 0
                    Else
                        rntxtPointLadder.Enabled = True
                        rntxtPointPass.Enabled = True
                    End If
                    chkIsPV.Enabled = True
                    EnableRadCombo(cboTitle, False)
                    EnabledGridNotPostback(rgData, False)
                    ctrlOrg.Enabled = False
                Case CommonMessage.STATE_DELETE
                    Dim objDelete As ExamsDtlDTO
                    Dim item As GridDataItem = rgData.SelectedItems(0)
                    objDelete = New ExamsDtlDTO With {.ID = item.GetDataKeyValue("ID")}
                    If rep.DeleteExamsDtl(objDelete) Then
                        Refresh("UpdateView")
                        UpdateControlState()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        UpdateControlState()
                    End If
            End Select
            ChangeToolbarState()
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New RecruitmentRepository
        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
                txtHeso.Text = "1"
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgData.Rebind()
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "Cancel"
                        rgData.MasterTableView.ClearSelectedItems()
                End Select
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim rep As New RecruitmentRepository

        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    If cboTitle.SelectedValue = "" Then
                        ShowMessage(Translate("Bạn phải chọn Vị trí tuyển dụng"), Utilities.NotifyType.Warning)
                        Exit Sub
                    End If
                    CurrentState = CommonMessage.STATE_NEW
                    txtName.Text = ""
                    txtGhichu.Text = ""
                    rntxtExamsOrder.Value = Nothing
                    rntxtPointLadder.Value = Nothing
                    rntxtPointPass.Value = Nothing
                    chkIsPV.Checked = False
                    hidID.Value = ""
                Case CommonMessage.TOOLBARITEM_EDIT
                    CurrentState = CommonMessage.STATE_EDIT
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        If txtName.Text = "" Then
                            ShowMessage(Translate("Bạn phải nhập Tên môn thi"), Utilities.NotifyType.Warning)
                            Exit Sub
                        End If
                        If rntxtPointLadder.Value Is Nothing Then
                            ShowMessage(Translate("Bạn phải nhập Thang điểm"), Utilities.NotifyType.Warning)
                            Exit Sub
                        End If
                        If txtHeso.Text = "" Then
                            ShowMessage(Translate("Bạn phải nhập Hệ số"), Utilities.NotifyType.Warning)
                            Exit Sub
                        End If
                        If rntxtPointPass.Value Is Nothing Then
                            ShowMessage(Translate("Bạn phải nhập Điểm đạt"), Utilities.NotifyType.Warning)
                            Exit Sub
                        End If
                        If rntxtPointLadder.Value < rntxtPointPass.Value Then
                            ShowMessage(Translate("Thang điểm phải lớn hơn Điểm đạt"), NotifyType.Warning)
                            Exit Sub
                        End If
                        If rntxtExamsOrder.Value Is Nothing Then
                            ShowMessage(Translate("Bạn phải nhập Thứ tự sắp xếp"), Utilities.NotifyType.Warning)
                            Exit Sub
                        End If
                        If Not chkIsPV.Checked Then
                            If rntxtPointLadder.Value Is Nothing Then
                                ShowMessage(Translate("Bạn phải nhập Thang điểm"), Utilities.NotifyType.Warning)
                                Exit Sub
                            End If
                            If rntxtPointPass.Value Is Nothing Then
                                ShowMessage(Translate("Bạn phải nhập Điểm đạt"), Utilities.NotifyType.Warning)
                                Exit Sub
                            End If
                            If rntxtPointLadder.Value < rntxtPointPass.Value Then
                                ShowMessage(Translate("Thang điểm phải lớn hơn Điểm đạt"), NotifyType.Warning)
                                Exit Sub
                            End If
                        End If
                        Dim obj As New ExamsDtlDTO
                        If hidID.Value <> "" Then
                            obj.ID = Decimal.Parse(hidID.Value)
                        End If
                        obj.ORG_ID = hidOrgID.Value
                        obj.TITLE_ID = cboTitle.SelectedValue
                        obj.EXAMS_ORDER = rntxtExamsOrder.Value
                        obj.NAME = txtName.Text
                        obj.COEFFICIENT = txtHeso.Text
                        If Not chkIsPV.Checked Then
                            obj.POINT_LADDER = rntxtPointLadder.Value
                            obj.POINT_PASS = rntxtPointPass.Value
                        End If
                        obj.IS_PV = chkIsPV.Checked
                        obj.NOTE = txtGhichu.Text
                        If rep.UpdateExamsDtl(obj) Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                            CurrentState = CommonMessage.STATE_NORMAL
                            Refresh("UpdateView")
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                        End If
                    End If
                Case CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    txtName.Text = ""
                    rntxtExamsOrder.Value = Nothing
                    rntxtPointLadder.Value = Nothing
                    rntxtPointPass.Value = Nothing
            End Select
            rep.Dispose()
            UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DELETE
                UpdateControlState()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub ctrlOrg_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged

        Dim dtData As DataTable
        Try
            Using rep As New RecruitmentRepository
                hidOrgID.Value = ""
                lblOrgName.Text = ""
                cboTitle.Items.Clear()
                cboTitle.ClearSelection()
                cboTitle.Text = ""
                If ctrlOrg.CurrentValue <> "" Then
                    hidOrgID.Value = ctrlOrg.CurrentValue
                    lblOrgName.Text = ctrlOrg.CurrentText
                    dtData = rep.GetTitleByOrgList(Decimal.Parse(ctrlOrg.CurrentValue), False)
                    FillRadCombobox(cboTitle, dtData, "NAME", "ID")
                End If
                rgData.Rebind()
            End Using
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
        Try
            CreateDataFilter()

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub cboTitle_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboTitle.SelectedIndexChanged
        Try
            rgData.CurrentPageIndex = 0
            rgData.MasterTableView.SortExpressions.Clear()
            rgData.Rebind()

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)

        End Try
    End Sub

    Protected Sub chkIsPV_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        rntxtPointLadder.Enabled = Not chkIsPV.Checked
        rntxtPointPass.Enabled = Not chkIsPV.Checked
        rntxtPointLadder.CausesValidation = Not chkIsPV.Checked
        rntxtPointPass.CausesValidation = Not chkIsPV.Checked
        rntxtPointLadder.Value = 0
        rntxtPointPass.Value = 0
    End Sub
#End Region

#Region "Custom"

    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim _filter As New ExamsDtlDTO
        Dim rep As New RecruitmentRepository
        Dim bCheck As Boolean = False
        Try
            SetValueObjectByRadGrid(rgData, _filter)
            If ctrlOrg.CurrentValue <> "" Then
                _filter.ORG_ID = ctrlOrg.CurrentValue
            End If
            If cboTitle.SelectedValue <> "" Then
                _filter.TITLE_ID = cboTitle.SelectedValue
            End If
            Dim MaximumRows As Integer
            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()
            Dim lstData As List(Of ExamsDtlDTO)
            If Sorts IsNot Nothing Then
                lstData = rep.GetExamsDtl(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, Sorts)
            Else
                lstData = rep.GetExamsDtl(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows)
            End If

            rgData.VirtualItemCount = MaximumRows
            rgData.DataSource = lstData
        Catch ex As Exception
            Throw ex
        End Try

    End Function

#End Region
End Class