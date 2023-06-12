Imports Common
Imports Common.Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI
Imports Training.TrainingBusiness
Imports WebAppLog

Public Class ctrlRegisterTraining
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
            Refresh()
            UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()
        Dim rep As New TrainingRepository
        Try
            Select Case CurrentState

            End Select
        Catch ex As Exception

        End Try

    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Try
            If Not IsPostBack Then

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        rgMain.AllowCustomPaging = True
        rgMain.PageSize = Common.Common.DefaultPageSize
        SetFilter(rgMain)
        CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        InitControl()
    End Sub

    Protected Sub InitControl()
        Try
            Me.MainToolBar = tbarMainToolBar
            BuildToolbar(Me.MainToolBar, ToolbarItem.Create, ToolbarItem.Seperator, ToolbarItem.Delete)
            CType(MainToolBar.Items(0), RadToolBarButton).Text = "Đăng ký"
            CType(MainToolBar.Items(0), RadToolBarButton).CommandName = "REGISTER"
            CType(MainToolBar.Items(2), RadToolBarButton).Text = "Hủy đăng ký"
            CType(MainToolBar.Items(2), RadToolBarButton).CommandName = "CANCEL_REGISTER"
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub BindData()
        Dim dtData As DataTable
        Using rep As New TrainingRepository
            dtData = rep.GetOtherList("TR_REGIST_PORTAL", True)
            FillRadCombobox(cboStatus, dtData, "NAME", "ID")
            dtData = rep.GetTrPlanByYearOrg2(True, 0, 0, True)
            FillRadCombobox(cboCourse, dtData, "NAME", "ID")
        End Using
    End Sub

#End Region

#Region "Event"

    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgMain.NeedDataSource
        Try
            CreateDataFilter()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Try
            rgMain.Rebind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim rep As New TrainingRepository
        Dim psp As New TrainingStoreProcedure
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case "REGISTER"
                    If rgMain.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rgMain.SelectedItems.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    Dim item As GridDataItem = rgMain.SelectedItems(0)

                    If IsDate(item.GetDataKeyValue("PORTAL_REGIST_FROM")) AndAlso Date.Now < CDate(item.GetDataKeyValue("PORTAL_REGIST_FROM")) Then
                        ShowMessage(Translate("Chương trình chưa đến hạn đăng ký"), NotifyType.Warning)
                        Exit Sub
                    End If

                    If IsDate(item.GetDataKeyValue("PORTAL_REGIST_TO")) AndAlso Date.Now > CDate(item.GetDataKeyValue("PORTAL_REGIST_TO")) Then
                        ShowMessage(Translate("Chương trình đã quá hạn đăng ký"), NotifyType.Warning)
                        Exit Sub
                    End If
                    If item.GetDataKeyValue("STATUS") IsNot Nothing Then
                        ShowMessage(Translate("Nhân viên đã có trong danh sách khóa học"), NotifyType.Warning)
                        Exit Sub
                    End If
                    If item.GetDataKeyValue("STUDENT_NUMBER_JOIN") IsNot Nothing And item.GetDataKeyValue("STUDENT_NUMBER") IsNot Nothing Then
                        If item.GetDataKeyValue("STUDENT_NUMBER_JOIN") >= item.GetDataKeyValue("STUDENT_NUMBER") Then
                            ShowMessage(Translate("Khóa học đã hết chỗ"), NotifyType.Warning)
                            Exit Sub
                        End If
                    End If
                    Dim obj As New ProgramEmpDTO
                    obj.TR_PROGRAM_ID = item.GetDataKeyValue("ID")
                    obj.EMP_ID = EmployeeID
                    obj.STATUS = 1
                    obj.IS_PORTAL_REGIST = 1
                    If psp.CHECK_TR_PROGRAM_EMP(item.GetDataKeyValue("ID"), EmployeeID) = False Then
                        If rep.InsertRegisterTraining_Portal(obj) Then
                            rgMain.Rebind()
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        End If
                    Else
                        obj.ID = item.GetDataKeyValue("PROGRAM_EMP_ID")
                        If rep.ModifyRegisterTraining_Portal(obj) Then
                            rgMain.Rebind()
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        End If
                    End If

                Case "CANCEL_REGISTER"
                    If rgMain.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rgMain.SelectedItems.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    Dim item As GridDataItem = rgMain.SelectedItems(0)
                    If item.GetDataKeyValue("STATUS") <> 1 Then
                        ShowMessage(Translate("Nhân viên chưa đăng ký khóa học hoặc không được phép hủy tham gia khóa học"), NotifyType.Warning)
                        Exit Sub
                    End If
                    If item.GetDataKeyValue("PORTAL_REGIST_TO") < Date.Now And item.GetDataKeyValue("PORTAL_REGIST_TO") IsNot Nothing Then
                        ShowMessage(Translate("Đã quá hạn cho phép hủy tham gia khóa học"), NotifyType.Warning)
                        Exit Sub
                    End If
                    If item.GetDataKeyValue("PROGRAM_EMP_ID") IsNot Nothing Then
                        Dim obj As New ProgramEmpDTO
                        obj.ID = item.GetDataKeyValue("PROGRAM_EMP_ID")
                        obj.STATUS = Nothing
                        If rep.ModifyRegisterTraining_Portal(obj) Then
                            rgMain.Rebind()
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        End If
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                    End If

            End Select
        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBoxTraining.ButtonCommand
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

    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim rep As New TrainingRepository
        Dim _filter As New ProgramDTO
        Dim lstProgram As New List(Of ProgramDTO)
        Try
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgMain, _filter)
            Dim Sorts As String = rgMain.MasterTableView.SortExpressions.GetSortString()
            If rdFromDate.SelectedDate IsNot Nothing Then
                _filter.START_DATE = rdFromDate.SelectedDate
            End If
            If rdToDate.SelectedDate IsNot Nothing Then
                _filter.END_DATE = rdToDate.SelectedDate
            End If
            If cboStatus.Text <> "" Then
                _filter.STATUS_NAME = cboStatus.Text
            End If
            If cboCourse.Text <> "" Then
                _filter.TR_COURSE_NAME = cboCourse.Text
            End If
            _filter.EMPLOYEE_ID = EmployeeID
            If Not isFull Then
                If Sorts IsNot Nothing Then
                    lstProgram = rep.GetPrograms_Portal(_filter, rgMain.CurrentPageIndex, rgMain.PageSize, MaximumRows, "CREATED_DATE desc")
                Else
                    lstProgram = rep.GetPrograms_Portal(_filter, rgMain.CurrentPageIndex, rgMain.PageSize, MaximumRows)
                End If
            Else
                Return rep.GetPrograms_Portal(_filter).ToTable
            End If

            rgMain.VirtualItemCount = MaximumRows
            rgMain.DataSource = lstProgram

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub rgData_ItemCommand(ByVal sender As Object, ByVal e As GridCommandEventArgs) Handles rgMain.ItemCommand
        Try
            Select Case e.CommandName
                Case "DETAIL"
                    Dim item = CType(e.Item, GridDataItem)
                    Dim content = item.GetDataKeyValue("CONTENT")
                    Dim target = item.GetDataKeyValue("TARGET_TRAIN")
                    'Response.Write(HttpUtility.HtmlDecode(content))

                    ctrlMessageBoxTraining.MessageText = "<b>Nội dung đào tạo </b> <br>" & content & "<br><br><b> Mục tiêu đào tạo </b><br>" & target
                    ctrlMessageBoxTraining.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBoxTraining.DataBind()
                    ctrlMessageBoxTraining.Show()
            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

End Class