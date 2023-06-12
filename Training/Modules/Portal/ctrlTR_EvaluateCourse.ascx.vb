Imports Common
Imports Common.Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI
Imports Training.TrainingBusiness
Imports WebAppLog

Public Class ctrlTR_EvaluateCourse
    Inherits CommonView
    Protected WithEvents ViewItem As ViewBase
    Public Overrides Property MustAuthorize As Boolean = False
    Dim psp As New TrainingStoreProcedure
    Dim _myLog As New MyLog()
    Dim _classPath As String = "Training/Module/Portal/" + Me.GetType().Name.ToString()
#Region "Property"
    Public Property EmployeeID As Decimal
#End Region

#Region "Page"

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            rgCourse.SetFilter()
            rgCourse.PageSize = Common.Common.DefaultPageSize
            rgCourse.AllowCustomPaging = True
            rgCourse.ClientSettings.EnablePostBackOnRowClick = True

            rgEvaluate.SetFilter()
            rgEvaluate.PageSize = Common.Common.DefaultPageSize
            rgEvaluate.AllowCustomPaging = True

            UpdateControlState()
            Refresh()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()
        Select Case CurrentState
            Case CommonMessage.STATE_NEW
                EnabledGrid(rgCourse, False, True)
                EnableControlAll(True, txtNote1, txtNote3, txtNote2, txtNote4)
            Case CommonMessage.STATE_NORMAL
                EnabledGrid(rgCourse, True, True)
                EnabledGrid(rgEvaluate, False, True)
                EnableControlAll(False, txtNote1, txtNote3, txtNote2, txtNote4)
            Case CommonMessage.STATE_EDIT
                EnabledGrid(rgCourse, False, True)
                EnabledGrid(rgEvaluate, True, True)
                EnableControlAll(True, txtNote1, txtNote3, txtNote2, txtNote4)
        End Select
        ChangeToolbarState()
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Try
            If Not IsPostBack Then
                EnabledGrid(rgCourse, True, True)
                EnabledGrid(rgEvaluate, False, True)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        InitControl()
    End Sub

    Protected Sub InitControl()
        Try
            Me.MainToolBar = tbarMainToolBar
            BuildToolbar(Me.MainToolBar, ToolbarItem.Edit, ToolbarItem.Seperator, ToolbarItem.Save, ToolbarItem.Cancel)
            MainToolBar.Items(0).Text = Translate("Cập nhật đánh giá")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub BindData()

    End Sub

#End Region

#Region "Event"

    Protected Sub rgCourse_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgCourse.NeedDataSource
        Try
            CreateDataFilterCourse()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Protected Sub rgEvaluate_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgEvaluate.NeedDataSource
        Try
            CreateDataFilterEvaluate()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim rep As New TrainingRepository
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_EDIT
                    If rgCourse.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rgCourse.SelectedItems.Count > 1 Then
                        ShowMessage(Translate("Không thể cập nhật nhiều nhân viên một lúc"), NotifyType.Warning)
                        Exit Sub
                    End If
                    For Each item As GridDataItem In rgCourse.SelectedItems
                        If item.GetDataKeyValue("IS_LOCK") = -1 Then
                            ShowMessage(Translate("Bản ghi đã khóa"), NotifyType.Warning)
                            Exit Sub
                        End If
                    Next
                    CurrentState = CommonMessage.STATE_EDIT
                    For Each item As GridDataItem In rgEvaluate.MasterTableView.Items
                        item.Edit = True
                    Next
                    rgEvaluate.MasterTableView.Rebind()

                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    For Each item As GridDataItem In rgEvaluate.MasterTableView.Items
                        item.Edit = False
                    Next
                    rgEvaluate.MasterTableView.Rebind()

                Case CommonMessage.TOOLBARITEM_SAVE
                    Page.Validate()
                    If Page.IsValid Then
                        Dim objResult As New AssessmentResultDTO
                        objResult.EMPLOYEE_ID = EmployeeID
                        Dim programID As GridDataItem = rgCourse.SelectedItems.Item(0)
                        objResult.TR_PROGRAM_ID = Convert.ToDecimal(programID("ID").Text)
                        objResult.NOTE1 = txtNote1.Text
                        objResult.NOTE2 = txtNote2.Text
                        objResult.NOTE3 = txtNote3.Text
                        objResult.NOTE4 = txtNote4.Text
                        Dim lst As New List(Of AssessmentResultDtlDTO)
                        For Each item As GridDataItem In rgEvaluate.Items
                            If item.Edit = True Then
                                Dim obj As New AssessmentResultDtlDTO
                                obj.ID = item.GetDataKeyValue("ID")
                                objResult.CRI_COURSE_ID = item.GetDataKeyValue("CRI_COURSE_ID")
                                Dim pointMax = CDec(item.GetDataKeyValue("TR_CRITERIA_POINT_MAX"))
                                Dim edit = CType(item, GridEditableItem)
                                Dim rntxtPointAss As RadNumericTextBox = CType(edit("POINT_ASS").Controls(0), RadNumericTextBox)
                                Dim txtRemark As TextBox = CType(edit("REMARK").Controls(0), TextBox)
                                If rntxtPointAss.Value < 0 Or rntxtPointAss.Value > pointMax Then
                                    ShowMessage("Giá trị đánh giá chỉ từ 0 đến" + pointMax.ToString, NotifyType.Warning)
                                    CurrentState = CommonMessage.STATE_EDIT
                                    UpdateControlState()
                                    Exit Sub
                                ElseIf rntxtPointAss.Value Is Nothing Then
                                    ShowMessage("Vui lòng nhập đầy đủ điểm đánh giá", NotifyType.Warning)
                                    CurrentState = CommonMessage.STATE_EDIT
                                    UpdateControlState()
                                    Exit Sub
                                End If
                                With obj
                                    .EMPLOYEE_ID = EmployeeID
                                    .POINT_ASS = rntxtPointAss.Value
                                    .REMARK = txtRemark.Text
                                    .TR_CRITERIA_GROUP_ID = item.GetDataKeyValue("TR_CRITERIA_GROUP_ID")
                                    .TR_CRITERIA_ID = item.GetDataKeyValue("TR_CRITERIA_ID")
                                    .TR_PROGRAM_ID = Convert.ToDecimal(programID("ID").Text)
                                End With
                                lst.Add(obj)
                            End If
                        Next
                        objResult.lstResult = lst
                        If rep.UpdateAssessmentResult(objResult) Then
                            CurrentState = CommonMessage.STATE_NORMAL
                            For Each item As GridDataItem In rgEvaluate.MasterTableView.Items
                                item.Edit = False
                            Next
                            rgEvaluate.Rebind()
                            CalculateDTB()
                            rgCourse.Rebind()
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        Else
                            rgEvaluate.Rebind()
                            rgCourse.Rebind()
                            CalculateDTB()
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        End If
                    End If
            End Select
            UpdateControlState()
        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Custom"

    Protected Function CreateDataFilterCourse(Optional ByVal isFull As Boolean = False) As DataTable
        Dim _filter As New ProgramDTO
        Dim rep As New TrainingRepository
        Dim total As Integer = 0
        Dim bCheck As Boolean = False
        Try
            SetValueObjectByRadGrid(rgCourse, _filter)
            Dim MaximumRows As Integer
            Dim Sorts As String = rgCourse.MasterTableView.SortExpressions.GetSortString()
            _filter.EMPLOYEE_ID = EmployeeID
            Dim lstData As List(Of ProgramDTO)
            If Sorts IsNot Nothing Then
                lstData = rep.GetProgramEvaluatePortal(_filter, rgCourse.CurrentPageIndex, rgCourse.PageSize, MaximumRows, Sorts)
            Else
                lstData = rep.GetProgramEvaluatePortal(_filter, rgCourse.CurrentPageIndex, rgCourse.PageSize, MaximumRows)
            End If
            rgCourse.VirtualItemCount = MaximumRows
            rgCourse.DataSource = lstData
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Protected Function CreateDataFilterEvaluate(Optional ByVal isFull As Boolean = False) As DataTable
        Try
            Dim rep As New TrainingRepository
            Dim _filter As New TR_CriteriaDTO
            Dim Sorts As String = rgEvaluate.MasterTableView.SortExpressions.GetSortString()

            If rgCourse.MasterTableView.GetSelectedItems.Count = 1 Then
                _filter.TR_PROGRAM_ID = rgCourse.MasterTableView.GetSelectedItems(0).GetDataKeyValue("ID")
            ElseIf rgCourse.MasterTableView.GetSelectedItems.Count = 0 Then
                _filter.TR_PROGRAM_ID = -1
            End If

            _filter.EMPLOYEE_ID = EmployeeID
            Dim MaximumRows As Decimal = 0
            Dim lstData As List(Of TR_CriteriaDTO)
            If Sorts IsNot Nothing Then
                lstData = rep.GetAssessmentResultByID_Portal(_filter)
            Else

                lstData = rep.GetAssessmentResultByID_Portal(_filter)
            End If
            rgEvaluate.DataSource = lstData
            rgEvaluate.VirtualItemCount = lstData.Count
            If rgCourse.SelectedItems.Count <> 0 Then
                CalculateDTB()
            End If
            If lstData.Count > 0 Then
                txtNote1.Text = lstData(0).NOTE1
                txtNote2.Text = lstData(0).NOTE2
                txtNote3.Text = lstData(0).NOTE3
                txtNote4.Text = lstData(0).NOTE4
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Function

#End Region

    Public Sub CalculateDTB()
        Dim rep As New TrainingRepository
        If rgEvaluate.Items.Count > 0 Then
            Dim item As GridDataItem = rgEvaluate.Items(0)
            Dim programID As GridDataItem = rgCourse.SelectedItems.Item(0)
            Dim dtb As Decimal = rep.GET_DTB_PORTAL(item.GetDataKeyValue("CRI_COURSE_ID"), EmployeeID, Convert.ToDecimal(programID("ID").Text))
            txtDTB.Text = If(dtb <> -1, dtb, "")
        Else
            txtDTB.Text = ""
        End If
    End Sub

    Protected Sub rgCourse_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rgCourse.SelectedIndexChanged
        rgEvaluate.Rebind()
        CalculateDTB()
    End Sub

    Private Sub rgEvaluate_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgEvaluate.ItemDataBound
        If e.Item.Edit Then
            Dim edit = CType(e.Item, GridEditableItem)
            Dim rntxt As RadNumericTextBox
            Dim txt As TextBox
            rntxt = CType(edit("POINT_ASS").Controls(0), RadNumericTextBox)
            rntxt.Width = Unit.Percentage(95)
            rntxt.MinValue = 0
            txt = CType(edit("REMARK").Controls(0), TextBox)
            txt.Width = Unit.Percentage(95)
            txt.MaxLength = 1023
        End If
    End Sub
End Class