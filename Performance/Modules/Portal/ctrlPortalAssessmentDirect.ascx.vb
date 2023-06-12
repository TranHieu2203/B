Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Performance.PerformanceBusiness
Imports Telerik.Web.UI

Public Class ctrlPortalAssessmentDirect
    Inherits CommonView
    Protected WithEvents ViewItem As ViewBase

#Region "Property"
    Public Property GridList As List(Of AssessmentDirectDTO)
        Get
            Return PageViewState(Me.ID & "_GridList")
        End Get
        Set(ByVal value As List(Of AssessmentDirectDTO))
            PageViewState(Me.ID & "_GridList") = value
        End Set
    End Property
    Public Property EmployeeID As Decimal

    Public Property dtData As DataTable
        Get
            Return PageViewState(Me.ID & "_dtData")
        End Get
        Set(ByVal value As DataTable)
            PageViewState(Me.ID & "_dtData") = value
        End Set
    End Property
#End Region

#Region "Page"
    Public Overrides Property MustAuthorize As Boolean = False
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            'Refresh()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        InitControl()
    End Sub

    Protected Sub InitControl()
        Try
            Me.MainToolBar = tbarMainToolBar
            Me.ctrlMessageBox.Listener = Me
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Print)
            CType(MainToolBar.Items(0), RadToolBarButton).Text = "In đánh giá"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New PerformanceRepository
        Try

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region


    Public Overrides Sub BindData()
        Try
            txtYear.Value = Date.Now.Year
            Dim rep As New PerformanceRepository
            Dim dtData As DataTable
            dtData = rep.GET_DM_STATUS()
            FillRadCombobox(cboStatus, dtData, "NAME", "ID")
            cboStatus.SelectedValue = 0

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try

            rgData.Rebind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)

        End Try
    End Sub

    Protected Sub btnApprove_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnApprove.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If rgData.SelectedItems.Count = 0 Then
                ShowMessage(Translate("Bạn chưa chọn bản ghi, Vui lòng kiểm tra lại."), NotifyType.Warning)
                Exit Sub
            End If

            For Each item In rgData.SelectedItems
                Dim GridList1 = (From p In GridList Where p.ID = item.GetDataKeyValue("ID")).SingleOrDefault

                If GridList1.PE_STATUS_ID = 1 Or GridList1.PE_STATUS_ID = 2 Then
                    ShowMessage(Translate("Các dòng đã chọn có trạng thái Đã thực hiện., Vui lòng kiểm tra lại."), NotifyType.Warning)
                    Exit Sub
                End If

                Dim rep As New PerformanceRepository

                Dim data = rep.CHECK_APP_2(Utilities.ObjToInt(GridList1.EMPLOYEE_ID), GridList1.PE_PERIO_ID)

                For Each item1 In data.Rows
                    If item1("RESULT").ToString = "1" Then
                        ShowMessage(Translate("Vẫn còn tiêu chí chưa được đánh giá, vui lòng kiểm tra lại."), NotifyType.Warning)
                        Exit Sub
                    End If
                Next
            Next

            ctrlMessageBox.MessageText = "Xác nhận kết quả đánh giá cho những nhân viên được chọn, Bạn có tiếp tục không?"
            ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_APPROVE
            ctrlMessageBox.DataBind()
            ctrlMessageBox.Show()

            'rgData.Rebind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)

        End Try
    End Sub

    'Protected Sub btnDeny_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDeny.Click
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Dim newRow As DataRow
    '    Try
    '        If dtData Is Nothing Then
    '            dtData = New DataTable("data")
    '            dtData.Columns.Add("EMPLOYEE_ID", GetType(Integer))
    '            dtData.Columns.Add("PE_PERIO_ID", GetType(Integer))
    '        End If
    '        dtData.Clear()

    '        If rgData.SelectedItems.Count = 0 Then
    '            ShowMessage(Translate("Bạn chưa chọn bản ghi, Vui lòng kiểm tra lại."), NotifyType.Warning)
    '            Exit Sub
    '        End If

    '        If txtNote.Text = "" Then
    '            ShowMessage(Translate("Một số nhân viên được chọn chưa có ý kiến đánh giá chung, Bạn vui lòng kiểm tra lại."), NotifyType.Warning)
    '            Exit Sub
    '        End If

    '        For Each item In rgData.SelectedItems
    '            Dim GridList1 = (From p In GridList Where p.ID = item.GetDataKeyValue("ID")).SingleOrDefault

    '            If GridList1.PE_STATUS_ID = 1 Or GridList1.PE_STATUS_ID = 2 Then
    '                ShowMessage(Translate("Các dòng đã chọn có trạng thái Đã thực hiện Or Huỷ thực hiện., Vui lòng kiểm tra lại."), NotifyType.Warning)
    '                Exit Sub
    '            End If

    '            newRow = dtData.NewRow
    '            newRow("EMPLOYEE_ID") = GridList1.EMPLOYEE_ID
    '            newRow("PE_PERIO_ID") = GridList1.PE_PERIO_ID
    '            dtData.Rows.Add(newRow)

    '        Next

    '        ctrlMessageBox.MessageText = "Bảng đánh giá sẽ trả lại cho các nhân viên được chọn , Bạn có tiếp tục không?"
    '        ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_CANCEL
    '        ctrlMessageBox.DataBind()
    '        ctrlMessageBox.Show()


    '        'rgData.Rebind()
    '    Catch ex As Exception
    '        DisplayException(Me.ViewName, Me.ID, ex)

    '    End Try
    'End Sub

#Region "Event"
    Private Sub rgData_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
        Try
            Dim rep As New PerformanceRepository
            'rgData.SetFilter()
            SetValueObjectByRadGrid(rgData, New AssessmentDirectDTO)
            Dim status As Decimal?
            If cboStatus.SelectedValue IsNot Nothing Then
                status = cboStatus.SelectedValue
            End If
            If Not IsPostBack Then
                GridList = rep.GetAssessmentDirect(EmployeeID, txtYear.Value, status)
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                GridList = rep.GetAssessmentDirect(EmployeeID, txtYear.Value, status)
            End If

            'Đưa dữ liệu vào Grid
            If Me.GridList IsNot Nothing Then
                rgData.DataSource = Me.GridList

            Else
                rgData.DataSource = New List(Of AssessmentDirectDTO)

            End If

        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objAssessment As New AssessmentDTO
        Dim rep As New PerformanceRepository
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_PRINT
                    Dim dsData As DataSet
                    Dim sPath As String = ""

                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate("Bạn phải chọn nhân viên trước khi in ."), Utilities.NotifyType.Warning)
                        Exit Sub
                    End If

                    If rgData.SelectedItems.Count > 1 Then
                        ShowMessage(Translate("Chỉ được chọn 1 bản ghi để in ."), Utilities.NotifyType.Warning)
                        Exit Sub
                    End If

                    'Kiểm tra các điều kiện trước khi in
                    Dim lstEmpID As String = ""
                    Dim srtImage As String = ""

                    For Each dr As Telerik.Web.UI.GridDataItem In rgData.SelectedItems
                        dsData = rep.PRINT_PE_ASSESS(dr.GetDataKeyValue("EMPLOYEE_ID"), dr.GetDataKeyValue("PE_PERIO_ID"), dr.GetDataKeyValue("OBJECT_GROUP_ID"))
                    Next

                    sPath = Server.MapPath("~/ReportTemplates/Performance/Report/BC_DANHGIA.xls")

                    Using xls As New ExcelCommon
                        xls.ExportExcelTemplate(sPath,
                            "InDanhGia", Nothing, dsData, Nothing, Response, "", ExcelCommon.ExportType.Excel)
                    End Using
            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Custom"
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien click vao button command ctrlMessageBox
    ''' voi cac trang thai xoa, duyet, huy kich hoat
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_APPROVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                For Each item In rgData.SelectedItems
                    Dim GridList1 = (From p In GridList Where p.ID = item.GetDataKeyValue("ID")).SingleOrDefault
                    Dim rep As New PerformanceRepository

                    rep.PRI_PERFORMACE_PROCESS(EmployeeID, GridList1.EMPLOYEE_ID, GridList1.PE_PERIO_ID, 1, txtNote.Text)
                Next
                rgData.Rebind()
            End If

            If e.ActionName = CommonMessage.TOOLBARITEM_CANCEL And e.ButtonID = MessageBoxButtonType.ButtonYes Then

                For Each item In dtData.Rows
                    'Dim GridList1 = (From p In GridList Where p.ID = item.GetDataKeyValue("ID")).SingleOrDefault
                    Dim rep As New PerformanceRepository
                    rep.PRI_PERFORMACE_PROCESS(EmployeeID, item("EMPLOYEE_ID"), item("PE_PERIO_ID"), 2, txtNote.Text)
                Next
                rgData.Rebind()
            End If

        Catch ex As Exception
        End Try

    End Sub

#End Region
End Class