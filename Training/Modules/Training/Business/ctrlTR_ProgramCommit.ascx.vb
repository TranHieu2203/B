Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI
Imports Training.TrainingBusiness

Public Class ctrlTR_ProgramCommit
    Inherits CommonView
    Public Overrides Property MustAuthorize As Boolean = False
    Protected WithEvents ctrlFindLecturePopup As ctrlFindEmployeePopup

#Region "Property"

    '0 - normal
    '1 - Lecture
    Property isLoadPopup As Integer
        Get
            Return ViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isLoadPopup") = value
        End Set

    End Property

#End Region

#Region "Page"

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            InitControl()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Protected Sub InitControl()
        Try
            Me.MainToolBar = tbarMain
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Edit, ToolbarItem.Save, ToolbarItem.Cancel, ToolbarItem.Print)
            CType(MainToolBar.Items(1), RadToolBarButton).CausesValidation = True
            CType(MainToolBar.Items(3), RadToolBarButton).Text = "In phiếu cam kết đào tạo"
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            Refresh()
            UpdateControlState()
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

                If objProgram IsNot Nothing Then
                    txtProgramName.Text = objProgram.TR_PROGRAM_CODE + " - " + objProgram.TR_COURSE_NAME
                    txtHinhThuc.Text = objProgram.TRAIN_FORM_NAME
                    rdFromDate.SelectedDate = objProgram.START_DATE
                    rdToDate.SelectedDate = objProgram.END_DATE
                    rnMoneyCommit.Text = objProgram.COST_TOTAL
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Overrides Sub UpdateControlState()
        If phFindLecture.Controls.Contains(ctrlFindLecturePopup) Then
            phFindLecture.Controls.Remove(ctrlFindLecturePopup)
        End If

        Select Case isLoadPopup
            Case 1
                ctrlFindLecturePopup = Me.Register("ctrlFindLecturePopup", "Common", "ctrlFindEmployeePopup")
                ctrlFindLecturePopup.MustHaveContract = False
                phFindLecture.Controls.Add(ctrlFindLecturePopup)
                ctrlFindLecturePopup.MultiSelect = False
        End Select

        Select Case CurrentState
            Case CommonMessage.STATE_EDIT
                SetControlState(True)

            Case CommonMessage.STATE_NORMAL
                SetControlState(False)

        End Select

        ChangeToolbarState()
    End Sub
    Public Sub SetControlState(ByVal state As Boolean)
        txtProgramName.ReadOnly = True
        txtHinhThuc.ReadOnly = True
        rdFromDate.Enabled = False
        rdToDate.Enabled = False

        txtCommitNo.ReadOnly = Not state
        rdCommitDate.Enabled = state
        rnConvertedTime.ReadOnly = Not state
        rntxtCommitWork.ReadOnly = Not state
        btnFindApprove.Enabled = state
        txtAproveName.ReadOnly = True
        txtApproveTitle.ReadOnly = True
        txtRemark.ReadOnly = Not state
        rdCOMMIT_START.Enabled = state
        rdCOMMIT_END.Enabled = state
        rnCommitMoney.Enabled = state
        EnabledGridNotPostback(rgData, Not state)
        rgData.AllowMultiRowSelection = False
    End Sub

    Public Overrides Sub BindData()
        Try
            Dim hidBind As HiddenField = New HiddenField
            Dim dic As New Dictionary(Of String, Control)
            dic.Add("MONEY_COMMIT", rnCommitMoney)
            dic.Add("IS_LOCK", chkIsCommit)
            dic.Add("ID", hidBind)
            'dic.Add("COMMIT_NO", txtCommitNo)
            'dic.Add("COMMIT_DATE", rdCommitDate)
            'dic.Add("CONVERED_TIME", rnConvertedTime)
            'dic.Add("COMMIT_WORK", rntxtCommitWork)
            'dic.Add("APPROVER_NAME", txtAproveName)
            'dic.Add("APPROVER_TITLE", txtApproveTitle)
            'dic.Add("COMMIT_REMARK", txtRemark)
            Utilities.OnClientRowSelectedChanged(rgData, dic)
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Try
            Dim rep As New TrainingRepository
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_EDIT
                    CurrentState = CommonMessage.STATE_EDIT
                    Dim item As GridDataItem = rgData.SelectedItems(0)
                    If Not IsNumeric(item.GetDataKeyValue("MONEY_COMMIT")) Then
                        rnCommitMoney.Value = If(IsNumeric(item.GetDataKeyValue("COST_STUDENT")), CDec(item.GetDataKeyValue("COST_STUDENT")), Nothing)
                    End If
                Case CommonMessage.TOOLBARITEM_SAVE
                    Dim lst As New List(Of ProgramCommitDTO)
                    For Each item As GridDataItem In rgData.SelectedItems
                        Dim obj As New ProgramCommitDTO
                        obj.ID = item.GetDataKeyValue("ID")
                        With obj
                            .EMPLOYEE_ID = item.GetDataKeyValue("EMPLOYEE_ID")
                            .COMMIT_DATE = rdCommitDate.SelectedDate
                            .COMMIT_NO = txtCommitNo.Text
                            .COMMIT_REMARK = txtRemark.Text
                            .CONVERED_TIME = rntxtCommitWork.Value
                            .COMMIT_WORK = rnConvertedTime.Value
                            .COMMIT_START = rdCOMMIT_START.SelectedDate
                            .COMMIT_END = rdCOMMIT_END.SelectedDate
                            .MONEY_COMMIT = rnCommitMoney.Value
                            .COST_STUDENT = rnMoneyCommit.Value
                            .IS_LOCK = chkIsCommit.Checked
                            If hidApproveID.Value <> "" Then
                                .APPROVER_ID = hidApproveID.Value
                            End If
                            .TR_PROGRAM_ID = hidProgramID.Value
                        End With
                        lst.Add(obj)
                    Next

                    If rep.UpdateProgramCommit(lst) Then
                        CurrentState = CommonMessage.STATE_NORMAL
                        For Each item As GridDataItem In rgData.MasterTableView.Items
                            item.Edit = False
                        Next
                        rgData.Rebind()
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    End If

                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL

            End Select
            UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Try
            If e.ButtonID = MessageBoxButtonType.ButtonYes Then
                'Kiểm tra các điều kiện trước khi xóa
                Dim lstCanID As New List(Of Decimal)
                For Each dr As Telerik.Web.UI.GridDataItem In rgData.SelectedItems
                    lstCanID.Add(dr.GetDataKeyValue("CANDIDATE_ID"))
                Next
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub rgData_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
        CreateDataFilterStudent()
    End Sub
    Private Sub CreateDataFilterStudent()
        Try
            Dim rep As New TrainingRepository
            Dim _filter As New ProgramCommitDTO
            Dim MaximumRows As Integer
            _filter.TR_PROGRAM_ID = Decimal.Parse(hidProgramID.Value)
            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()
            Dim lstData As List(Of ProgramCommitDTO)
            If Sorts IsNot Nothing Then
                lstData = rep.GetProgramCommit(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, Sorts)
            Else
                lstData = rep.GetProgramCommit(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows)
            End If
            rgData.VirtualItemCount = MaximumRows
            rgData.DataSource = lstData
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Protected Sub rgData_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rgData.SelectedIndexChanged
        Try
            If rgData.SelectedItems.Count = 0 Then Exit Sub

            Dim item As GridDataItem = CType(rgData.SelectedItems(0), GridDataItem)
            If item.GetDataKeyValue("COMMIT_NO") IsNot Nothing Then
                txtCommitNo.Text = item.GetDataKeyValue("COMMIT_NO").ToString
            Else
                txtCommitNo.Text = ""
            End If
            If item.GetDataKeyValue("COMMIT_DATE") IsNot Nothing Then
                rdCommitDate.SelectedDate = CDate(item.GetDataKeyValue("COMMIT_DATE"))
            Else
                rdCommitDate.ClearValue()
            End If
            If item.GetDataKeyValue("CONVERED_TIME") IsNot Nothing Then
                rntxtCommitWork.Value = CDec(item.GetDataKeyValue("CONVERED_TIME"))
            Else
                rnConvertedTime.ClearValue()
            End If
            If item.GetDataKeyValue("COMMIT_WORK") IsNot Nothing Then
                rnConvertedTime.Value = CDec(item.GetDataKeyValue("COMMIT_WORK"))
            Else
                rntxtCommitWork.ClearValue()
            End If
            If item.GetDataKeyValue("APPROVER_ID") IsNot Nothing Then
                hidApproveID.Value = CDec(item.GetDataKeyValue("APPROVER_ID"))
            Else
                hidApproveID.ClearValue()
            End If
            If item.GetDataKeyValue("APPROVER_NAME") IsNot Nothing Then
                txtAproveName.Text = item.GetDataKeyValue("APPROVER_NAME").ToString
            Else
                txtAproveName.ClearValue()
            End If
            If item.GetDataKeyValue("APPROVER_TITLE") IsNot Nothing Then
                txtApproveTitle.Text = item.GetDataKeyValue("APPROVER_TITLE").ToString
            Else
                txtApproveTitle.ClearValue()
            End If
            If item.GetDataKeyValue("COMMIT_REMARK") IsNot Nothing Then
                txtRemark.Text = item.GetDataKeyValue("COMMIT_REMARK").ToString
            Else
                txtRemark.ClearValue()
            End If

            If item.GetDataKeyValue("COMMIT_START") IsNot Nothing Then
                rdCOMMIT_START.SelectedDate = CDate(item.GetDataKeyValue("COMMIT_START"))
            Else
                rdCOMMIT_START.ClearValue()
            End If
            If item.GetDataKeyValue("COMMIT_END") IsNot Nothing Then
                rdCOMMIT_END.SelectedDate = CDate(item.GetDataKeyValue("COMMIT_END"))
            Else
                rdCOMMIT_END.ClearValue()
            End If
            If item.GetDataKeyValue("MONEY_COMMIT") IsNot Nothing Then
                rnCommitMoney.Value = CDec(item.GetDataKeyValue("MONEY_COMMIT"))
            Else
                rnCommitMoney.ClearValue()
            End If
            If item.GetDataKeyValue("COST_STUDENT") IsNot Nothing Then
                rnMoneyCommit.Value = CDec(item.GetDataKeyValue("COST_STUDENT"))
            Else
                rnMoneyCommit.ClearValue()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Sub btnFindLecture_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFindApprove.Click
        Try
            isLoadPopup = 1
            UpdateControlState()
            ctrlFindLecturePopup.Show()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub ctrlFindLecturePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindLecturePopup.EmployeeSelected
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        Try
            lstCommonEmployee = CType(ctrlFindLecturePopup.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            Dim itm = lstCommonEmployee(0)
            hidApproveID.Value = itm.EMPLOYEE_ID
            txtAproveName.Text = itm.FULLNAME_VN
            txtApproveTitle.Text = itm.TITLE_NAME
            isLoadPopup = 0
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    Private Sub rdCOMMIT_START_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdCOMMIT_START.SelectedDateChanged
        Try
            If rdCOMMIT_START.SelectedDate IsNot Nothing And IsNumeric(rnConvertedTime.Value) Then
                rdCOMMIT_END.SelectedDate = CDate(rdCOMMIT_START.SelectedDate).AddMonths(CDec(rnConvertedTime.Value))
                Dim totalday = DateDiff(DateInterval.Day, CDate(rdCOMMIT_START.SelectedDate), CDate(rdCOMMIT_END.SelectedDate))
                rntxtCommitWork.Value = totalday
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub rdCOMMIT_END_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdCOMMIT_END.SelectedDateChanged
        Try
            If rdCOMMIT_END.SelectedDate IsNot Nothing And rdCOMMIT_START.SelectedDate IsNot Nothing Then
                Dim totalday = DateDiff(DateInterval.Day, CDate(rdCOMMIT_START.SelectedDate), CDate(rdCOMMIT_END.SelectedDate))
                rntxtCommitWork.Value = totalday
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

#Region "Custom"
#End Region
End Class