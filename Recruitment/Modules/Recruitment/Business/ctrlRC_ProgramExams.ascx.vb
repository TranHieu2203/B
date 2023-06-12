Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Recruitment.RecruitmentBusiness
Imports Telerik.Web.UI

Public Class ctrlRC_ProgramExams
    Inherits Common.CommonView
    Protected WithEvents ProgramExamsDtlView As ViewBase

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
            dic.Add("COEFFICIENT", rntxtCoefficient)
            dic.Add("REMARK", txtRemark)
            Utilities.OnClientRowSelectedChanged(rgData, dic)
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgData_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles rgData.SelectedIndexChanged
        Try
            If rgData.SelectedItems.Count > 0 Then
                Dim slItem As GridDataItem
                slItem = rgData.SelectedItems(0)
                If IsNumeric(slItem.GetDataKeyValue("ID")) Then
                    hidID.Value = slItem.GetDataKeyValue("ID").ToString
                End If
                txtName.Text = slItem.GetDataKeyValue("NAME").ToString()
                If IsNumeric(slItem.GetDataKeyValue("POINT_LADDER")) Then
                    rntxtPointLadder.Value = slItem.GetDataKeyValue("POINT_LADDER").ToString
                End If
                If IsNumeric(slItem.GetDataKeyValue("POINT_PASS")) Then
                    rntxtPointPass.Value = slItem.GetDataKeyValue("POINT_PASS").ToString
                End If
                If IsNumeric(slItem.GetDataKeyValue("EXAMS_ORDER")) Then
                    rntxtExamsOrder.Value = slItem.GetDataKeyValue("EXAMS_ORDER").ToString
                End If
                If IsNumeric(slItem.GetDataKeyValue("COEFFICIENT")) Then
                    rntxtCoefficient.Value = slItem.GetDataKeyValue("COEFFICIENT").ToString
                End If
                If slItem.GetDataKeyValue("IS_PV") = True Then
                    chkIsPV.Checked = True
                    rntxtPointLadder.Enabled = False
                    rntxtPointPass.Enabled = False
                Else
                    chkIsPV.Checked = False
                    rntxtPointLadder.Enabled = True
                    rntxtPointPass.Enabled = True
                End If
                If slItem.GetDataKeyValue("REMARK") IsNot Nothing Then
                    txtRemark.Text = slItem.GetDataKeyValue("REMARK").ToString
                Else
                    txtRemark.Text = ""
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMain
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create, ToolbarItem.Edit, ToolbarItem.Seperator,
                                       ToolbarItem.Save, ToolbarItem.Cancel, ToolbarItem.Next, ToolbarItem.Delete)
            CType(MainToolBar.Items(3), RadToolBarButton).CausesValidation = True
            CType(MainToolBar.Items(5), RadToolBarButton).Text = "Khai báo UV"
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
                    rntxtCoefficient.Enabled = False
                    txtRemark.Enabled = False
                    EnabledGridNotPostback(rgData, True)
                Case CommonMessage.STATE_NEW, CommonMessage.STATE_EDIT
                    txtName.Enabled = True
                    rntxtExamsOrder.Enabled = True
                    If chkIsPV.Checked Then
                        rntxtPointLadder.Enabled = False
                        rntxtPointPass.Enabled = False
                    Else
                        rntxtPointLadder.Enabled = True
                        rntxtPointPass.Enabled = True
                    End If
                    chkIsPV.Enabled = True
                    rntxtCoefficient.Enabled = True
                    txtRemark.Enabled = True
                    txtName.Focus()
                    EnabledGridNotPostback(rgData, False)
                Case CommonMessage.STATE_DELETE
                    Dim objDelete As ProgramExamsDTO
                    Dim item As GridDataItem = rgData.SelectedItems(0)
                    objDelete = New ProgramExamsDTO With {.ID = item.GetDataKeyValue("ID")}


                    If rep.DeleteProgramExams(objDelete) Then
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
                hidProgramID.Value = Request.Params("PROGRAM_ID")
                Dim obj = rep.GetProgramByID(New ProgramDTO With {.ID = Decimal.Parse(hidProgramID.Value)})
                lblOrgName.Text = obj.ORG_NAME
                lblTitleName.Text = obj.TITLE_NAME
                CurrentState = CommonMessage.STATE_NORMAL
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
    Private Sub chkIsPV_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkIsPV.CheckedChanged
        Try
            If chkIsPV.Checked Then
                rntxtPointLadder.Enabled = False
                rntxtPointPass.Enabled = False
            Else
                rntxtPointLadder.Enabled = True
                rntxtPointPass.Enabled = True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#Region "Event"

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim rep As New RecruitmentRepository

        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    Dim psp As New RecruitmentRepository
                    Dim count = psp.CHECK_EXIST_PROGRAM_SCHEDULE_CAN(hidProgramID.Value)
                    If count > 0 Then
                        ShowMessage(Translate("Bạn phải xóa kết quả vòng tuyển dụng cuối cùng trước khi thêm mới vòng phỏng vấn tiếp theo."), Utilities.NotifyType.Warning)
                        Exit Sub
                    End If
                    CurrentState = CommonMessage.STATE_NEW
                    txtName.Text = ""
                    rntxtExamsOrder.Value = rep.Get_EXAMS_ORDER(hidProgramID.Value)
                    rntxtPointLadder.Value = Nothing
                    rntxtPointPass.Value = Nothing
                    chkIsPV.Checked = False
                    rntxtCoefficient.Value = 1
                    txtRemark.Text = ""
                    ClearControlValue(hidID)
                Case CommonMessage.TOOLBARITEM_EDIT
                    CurrentState = CommonMessage.STATE_EDIT
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        If txtName.Text = "" Then
                            ShowMessage(Translate("Bạn phải nhập Vòng thi"), Utilities.NotifyType.Warning)
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
                        Dim obj As New ProgramExamsDTO
                        obj.RC_PROGRAM_ID = hidProgramID.Value
                        obj.EXAMS_ORDER = rntxtExamsOrder.Value
                        obj.NAME = txtName.Text
                        obj.COEFFICIENT = rntxtCoefficient.Value
                        obj.REMARK = txtRemark.Text
                        If Not chkIsPV.Checked Then
                            obj.POINT_LADDER = rntxtPointLadder.Value
                            obj.POINT_PASS = rntxtPointPass.Value
                        End If
                        obj.IS_PV = chkIsPV.Checked
                        If hidID.Value <> "" Then
                            obj.ID = hidID.Value
                        End If
                        If rep.UpdateProgramExams(obj) Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                            CurrentState = CommonMessage.STATE_NORMAL
                            Refresh("UpdateView")
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                        End If
                    End If
                Case CommonMessage.TOOLBARITEM_DELETE
                    If Decimal.Parse(rntxtExamsOrder.Value) < (Decimal.Parse(rep.Get_EXAMS_ORDER(hidProgramID.Value)) - 1) Then
                        ShowMessage(Translate("Bạn phải xóa vòng phỏng vấn cuối cùng trước"), Utilities.NotifyType.Warning)
                        Exit Sub
                    End If
                    'For Each items As GridDataItem In rgData.SelectedItems
                    '    Dim STATUS_CODE As String = String.Empty
                    '    STATUS_CODE = items.GetDataKeyValue("STATUS_CODE").ToString
                    'Next
                    Dim item As GridDataItem = rgData.SelectedItems(0)
                    Dim COUNT_SCAN = CDec(item.GetDataKeyValue("COUNT_SCAN").ToString)
                    If COUNT_SCAN > 0 Then
                        ShowMessage(Translate("Vòng phỏng vấn được lên lịch phỏng vấn cho ứng viên không được xóa."), Utilities.NotifyType.Warning)
                        Exit Sub
                    End If
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                Case CommonMessage.TOOLBARITEM_NEXT
                    If rgData.VirtualItemCount = 0 Then
                        ShowMessage(Translate("Cần phải tạo vòng phỏng vấn trước khi khai báo ứng viên."), Utilities.NotifyType.Warning)
                        Exit Sub
                    End If
                    Page.Response.Redirect("Default.aspx?mid=Recruitment&fid=ctrlRC_CandidateList&group=Business&PROGRAM_ID=" & hidProgramID.Value & "")
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

    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
        Try
            CreateDataFilter()

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

#End Region

#Region "Custom"

    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim _filter As New ProgramExamsDTO
        Dim rep As New RecruitmentRepository
        Dim bCheck As Boolean = False
        Try
            SetValueObjectByRadGrid(rgData, _filter)
            _filter.RC_PROGRAM_ID = Decimal.Parse(hidProgramID.Value)
            Dim MaximumRows As Integer
            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()
            Dim lstData As List(Of ProgramExamsDTO)
            If Sorts IsNot Nothing Then
                lstData = rep.GetProgramExams(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, Sorts)
            Else
                lstData = rep.GetProgramExams(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows)
            End If
            rgData.VirtualItemCount = MaximumRows
            rgData.DataSource = lstData
        Catch ex As Exception
            Throw ex
        End Try

    End Function

#End Region



End Class