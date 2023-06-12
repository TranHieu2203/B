Imports Common
Imports Common.Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI
Imports Training.TrainingBusiness
Imports WebAppLog

Public Class ctrlPortal_TranningRecord
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
            BuildToolbar(Me.MainToolBar, ToolbarItem.Export)
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub BindData()
        Dim dtData As DataTable
        Using rep As New TrainingRepository
            'dtData = rep.GetOtherList("TR_REGIST_PORTAL", True)
            'FillRadCombobox(cboStatus, dtData, "NAME", "ID")
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
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        dtData = CreateDataFilter(True)
                        If dtData.Rows.Count > 0 Then
                            rgMain.ExportExcel(Server, Response, dtData, "ctrlPortal_TranningRecord")
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                        End If
                    End Using

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
        Dim _filter As New RecordEmployeeDTO
        Dim rep As New TrainingRepository
        Dim total As Integer = 0
        Dim bCheck As Boolean = False
        Try
            SetValueObjectByRadGrid(rgMain, _filter)

            _filter.FROM_DATE = rdFromDate.SelectedDate
            _filter.TO_DATE = rdToDate.SelectedDate
            If cboCourse.SelectedValue <> "" Then
                _filter.TR_COURSE_ID = cboCourse.SelectedValue
            End If
            _filter.EMPLOYEE_ID = EmployeeID
            _filter.IS_TR_COMMIT = chkTran.Checked
            Dim Sorts As String = rgMain.MasterTableView.SortExpressions.GetSortString()
            Dim _param = New ParamDTO With {.ORG_ID = 0, .IS_DISSOLVE = 0}

            If isFull Then
                If Sorts IsNot Nothing Then
                    Return rep.GetPortalListEmployeePaging(_filter, _param, Sorts).ToTable()
                Else
                    Return rep.GetPortalListEmployeePaging(_filter, _param).ToTable()
                End If
            Else
                Dim MaximumRows As Integer
                Dim lstData As List(Of RecordEmployeeDTO)
                If Sorts IsNot Nothing Then
                    lstData = rep.GetPortalListEmployeePaging(_filter, rgMain.CurrentPageIndex, rgMain.PageSize, MaximumRows, _param, Sorts)
                Else
                    lstData = rep.GetPortalListEmployeePaging(_filter, rgMain.CurrentPageIndex, rgMain.PageSize, MaximumRows, _param)
                End If
                rgMain.VirtualItemCount = MaximumRows
                rgMain.DataSource = lstData
            End If
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
    Private Sub RadGrid_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgMain.ItemDataBound
        Try
            If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
                Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
                Dim IS_REACHED = CBool(datarow("IS_REACHED").Text)
                If IS_REACHED Then
                    datarow("IS_REACH").ForeColor = Drawing.Color.Green
                Else
                    datarow("IS_REACH").ForeColor = Drawing.Color.Red
                End If

            End If


        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

End Class