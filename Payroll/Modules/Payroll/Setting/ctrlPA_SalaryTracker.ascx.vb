Imports System.IO
Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Payroll.PayrollBusiness
Imports Telerik.Web.UI
Imports WebAppLog
Public Class ctrlPA_SalaryTracker
    Inherits Common.CommonView
    Protected WithEvents SalaryPlanningView As ViewBase
    Protected WithEvents ctrlOrgPopup As ctrlFindOrgPopup
    'author: hongdx
    'EditDate: 21-06-2017
    'Content: Write log time and error
    'asign: hdx
    Dim _mylog As New MyLog() 'hdx
    Dim _pathLog As String = _mylog._pathLog 'hdx
#Region "Property"

    Property IsLoad As Boolean
        Get
            Return ViewState(Me.ID & "_IsLoad")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_IsLoad") = value
        End Set
    End Property

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
    Public title As String = ""
    Public name As String = "Số tiền"
    Public data As String ''{name: 'Microsoft Internet Explorer', y: 56.33},\n
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            Dim startTime As DateTime = DateTime.UtcNow 'hdx
            Refresh()
            UpdateControlState()
            Using w As StreamWriter = File.AppendText(_pathLog)
                _mylog.Log("Info | Paroll/Module/Paroll/Setting/ctrlPA_SalaryTracker| ViewLoad :" + CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), w)
                w.Close()
            End Using 'hdx
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            Using w As StreamWriter = File.AppendText(_pathLog)
                _mylog.Log("Error | Paroll/Module/Paroll/Setting/ctrlPA_SalaryTracker | ViewLoad :" + ex.Message, w)
                w.Close()
            End Using 'hdx
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            Dim startTime As DateTime = DateTime.UtcNow 'hdx
            rgData.SetFilter()
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            InitControl()
            Using w As StreamWriter = File.AppendText(_pathLog)
                _mylog.Log("Info | Paroll/Module/Paroll/Setting/ctrlPA_SalaryTracker| ViewInit :" + CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), w)
                w.Close()
            End Using 'hdx
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            Using w As StreamWriter = File.AppendText(_pathLog)
                _mylog.Log("Error | Paroll/Module/Paroll/Setting/ctrlPA_SalaryTracker | ViewInit :" + ex.Message, w)
                w.Close()
            End Using 'hdx
        End Try

    End Sub

    Public Overrides Sub BindData()
        Try
            Dim startTime As DateTime = DateTime.UtcNow 'hdx
            rntxtYear.Value = Date.Now.Year
            rntxtMonth.Value = Date.Now.Month
            Using w As StreamWriter = File.AppendText(_pathLog)
                _mylog.Log("Info | Paroll/Module/Paroll/Setting/ctrlPA_SalaryTracker| BindData :" + CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), w)
                w.Close()
            End Using 'hdx
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            Using w As StreamWriter = File.AppendText(_pathLog)
                _mylog.Log("Error | Paroll/Module/Paroll/Setting/ctrlPA_SalaryTracker | BindData :" + ex.Message, w)
                w.Close()
            End Using 'hdx
        End Try
    End Sub

    Protected Sub InitControl()
        Try
            Dim startTime As DateTime = DateTime.UtcNow 'hdx
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMain
            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem("EXPORT_TEMP",
                                                                    ToolbarIcons.Export,
                                                                    ToolbarAuthorize.Export,
                                                                    Translate("Xuất file mẫu")))
            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem("IMPORT_TEMP",
                                                                     ToolbarIcons.Import,
                                                                     ToolbarAuthorize.Import,
                                                                     Translate("Nhập file mẫu")))
            tbarMain.Visible = False
            Using w As StreamWriter = File.AppendText(_pathLog)
                _mylog.Log("Info | Paroll/Module/Paroll/Setting/ctrlPA_SalaryTracker| InitControl :" + CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), w)
                w.Close()
            End Using 'hdx
        Catch ex As Exception
            Throw ex
            Using w As StreamWriter = File.AppendText(_pathLog)
                _mylog.Log("Error | Paroll/Module/Paroll/Setting/ctrlPA_SalaryTracker | InitControl :" + ex.Message, w)
                w.Close()
            End Using 'hdx
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()
        Dim rep As New PayrollRepository
        Dim startTime As DateTime = DateTime.UtcNow 'hdx
        Try
            Select Case CurrentState
                Case CommonMessage.STATE_DELETE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgData.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgData.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.DeleteSalaryPlanning(lstDeletes) Then
                        Refresh("UpdateView")
                        UpdateControlState()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        UpdateControlState()
                    End If
            End Select
            phPopup.Controls.Clear()
            Select Case isLoadPopup
                Case 1
                    ctrlOrgPopup = Me.Register("ctrlOrgPopup", "Common", "ctrlFindOrgPopup")
                    phPopup.Controls.Add(ctrlOrgPopup)
            End Select
            Using w As StreamWriter = File.AppendText(_pathLog)
                _mylog.Log("Info | Paroll/Module/Paroll/Setting/ctrlPA_SalaryTracker| UpdateControlState :" + CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), w)
                w.Close()
            End Using 'hdx
        Catch ex As Exception
            Throw ex
            Using w As StreamWriter = File.AppendText(_pathLog)
                _mylog.Log("Error | Paroll/Module/Paroll/Setting/ctrlPA_SalaryTracker | UpdateControlState :" + ex.Message, w)
                w.Close()
            End Using 'hdx
        End Try

    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New PayrollRepository
        Dim startTime As DateTime = DateTime.UtcNow 'hdx
        Try
            If Not IsPostBack Then
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
            Using w As StreamWriter = File.AppendText(_pathLog)
                _mylog.Log("Info | Paroll/Module/Paroll/Setting/ctrlPA_SalaryTracker| Refresh :" + CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), w)
                w.Close()
            End Using 'hdx
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            Using w As StreamWriter = File.AppendText(_pathLog)
                _mylog.Log("Error | Paroll/Module/Paroll/Setting/ctrlPA_SalaryTracker | Refresh :" + ex.Message, w)
                w.Close()
            End Using 'hdx
        End Try
    End Sub

#End Region

#Region "Event"

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Try
            Dim startTime As DateTime = DateTime.UtcNow 'hdx

            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_DELETE
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate("Bạn phải chọn nhân viên trước khi xóa"), Utilities.NotifyType.Error)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Using xls As New ExcelCommon
                        Dim dtDatas As DataTable
                        dtDatas = CreateDataFilter()
                        If dtDatas.Rows.Count > 0 Then
                            rgData.ExportExcel(Server, Response, dtDatas, "SalaryPlanningister")
                        End If
                    End Using
                Case "EXPORT_TEMP"
                    isLoadPopup = 1 'Chọn Org
                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('IMPORT_SALARYPLANNING&ORG_ID=" & ctrlOrg.CurrentValue & "');", True)
                    isLoadPopup = 0
                    UpdateControlState()
                Case "IMPORT_TEMP"
                    If rntxtYear.Value Is Nothing Then
                        ShowMessage(Translate("Bạn phải chọn Năm"), Utilities.NotifyType.Warning)
                        Exit Sub
                    End If
                    If rntxtMonth.Value Is Nothing Then
                        ShowMessage(Translate("Bạn phải chọn Tháng"), Utilities.NotifyType.Warning)
                        Exit Sub
                    End If
                    ctrlUpload.Show()
            End Select
            Using w As StreamWriter = File.AppendText(_pathLog)
                _mylog.Log("Info | Paroll/Module/Paroll/Setting/ctrlPA_SalaryTracker| OnToolbar_Command :" + CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), w)
                w.Close()
            End Using 'hdx
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            Using w As StreamWriter = File.AppendText(_pathLog)
                _mylog.Log("Error | Paroll/Module/Paroll/Setting/ctrlPA_SalaryTracker | OnToolbar_Command :" + ex.Message, w)
                w.Close()
            End Using 'hdx
        End Try
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Try
            Dim startTime As DateTime = DateTime.UtcNow 'hdx
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DELETE
                UpdateControlState()
            End If
            Using w As StreamWriter = File.AppendText(_pathLog)
                _mylog.Log("Info | Paroll/Module/Paroll/Setting/ctrlPA_SalaryTracker| ctrlMessageBox_ButtonCommand :" + CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), w)
                w.Close()
            End Using 'hdx
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            Using w As StreamWriter = File.AppendText(_pathLog)
                _mylog.Log("Error | Paroll/Module/Paroll/Setting/ctrlPA_SalaryTracker | ctrlMessageBox_ButtonCommand :" + ex.Message, w)
                w.Close()
            End Using 'hdx
        End Try

    End Sub

    Private Sub ctrlOrg_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged
        Try
            Dim startTime As DateTime = DateTime.UtcNow 'hdx
            hidOrg.Value = ctrlOrg.CurrentValue
            hidOrgName.Value = ctrlOrg.CurrentText
            rgData.CurrentPageIndex = 0
            rgData.MasterTableView.SortExpressions.Clear()
            rgData.Rebind()
            Using w As StreamWriter = File.AppendText(_pathLog)
                _mylog.Log("Info | Paroll/Module/Paroll/Setting/ctrlPA_SalaryTracker| ctrlOrg_SelectedNodeChanged :" + CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), w)
                w.Close()
            End Using 'hdx
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            Using w As StreamWriter = File.AppendText(_pathLog)
                _mylog.Log("Error | Paroll/Module/Paroll/Setting/ctrlPA_SalaryTracker | ctrlOrg_SelectedNodeChanged :" + ex.Message, w)
                w.Close()
            End Using 'hdx
        End Try
    End Sub

    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
        Try
            CreateDataFilter()

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Property dtData As DataTable
        Get
            If ViewState(Me.ID & "_dtData") Is Nothing Then
                Dim dt As New DataTable
                dt.Columns.Add("ID", GetType(String))
                dt.Columns.Add("ORG_NAME", GetType(String))
                dt.Columns.Add("ORG_ID", GetType(String))
                dt.Columns.Add("TITLE_NAME", GetType(String))
                dt.Columns.Add("TITLE_ID", GetType(String))
                dt.Columns.Add("YEAR", GetType(String))
                dt.Columns.Add("MONTH", GetType(String))
                dt.Columns.Add("EMP_NUMBER", GetType(String))
                ViewState(Me.ID & "_dtData") = dt
            End If
            Return ViewState(Me.ID & "_dtData")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtData") = value
        End Set
    End Property

    Function SaveOT() As Boolean
        Try
            Dim startTime As DateTime = DateTime.UtcNow 'hdx
            Dim rep As New PayrollRepository
            Dim obj As New PASalaryPlanningDTO
            Dim gId As New Decimal?
            gId = 0
            dtData.TableName = "Data"
            rep.ImportSalaryPlanning(dtData, gId)
            Using w As StreamWriter = File.AppendText(_pathLog)
                _mylog.Log("Info | Paroll/Module/Paroll/Setting/ctrlPA_SalaryTracker| SaveOT :" + CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), w)
                w.Close()
            End Using 'hdx
            Return True
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            Using w As StreamWriter = File.AppendText(_pathLog)
                _mylog.Log("Error | Paroll/Module/Paroll/Setting/ctrlPA_SalaryTracker | SaveOT :" + ex.Message, w)
                w.Close()
            End Using 'hdx
            Return False
        End Try
    End Function

    Function loadToGrid(ByRef dtDataHeader As DataTable) As Boolean
        Dim dtError As New DataTable("ERROR")
        Dim startTime As DateTime = DateTime.UtcNow 'hdx
        Try
            If dtDataHeader.Rows.Count = 0 Then
                ShowMessage("File import không có dữ liệu ", NotifyType.Warning)
                Return False
            End If
            Dim rowError As DataRow
            Dim isError As Boolean = False
            Dim sError As String = String.Empty
            dtError = dtDataHeader.Clone
            Dim countErr As New Integer
            Dim irow = 5
            Dim objData As New PASalaryPlanningDTO
            For Each row As DataRow In dtDataHeader.Rows
                isError = False
                rowError = dtError.NewRow
                countErr = 0
                sError = "Đơn vị"
                ImportValidate.IsValidList("ORG_NAME", "ORG_ID", row, rowError, isError, sError)
                sError = "Chức danh"
                ImportValidate.IsValidList("TITLE_NAME", "TITLE_ID", row, rowError, isError, sError)
                row("YEAR") = rntxtYear.Value
                row("MONTH") = rntxtMonth.Value
                If row("EMP_NUMBER").ToString <> "" Then
                    sError = "Định biên nhân sự không phải kiểu số"
                    ImportValidate.IsValidNumber("EMP_NUMBER", row, rowError, isError, sError)
                End If

                If isError Then
                    rowError("ID") = irow
                    dtError.Rows.Add(rowError)
                End If
                irow = irow + 1
            Next
            If dtError.Rows.Count > 0 Then
                dtError.TableName = "DATA"
                Session("EXPORTREPORT") = dtError
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('IMPORT_SALARYPLANNING_ERROR')", True)
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
            Else
                Return True
            End If
            Using w As StreamWriter = File.AppendText(_pathLog)
                _mylog.Log("Info | Paroll/Module/Paroll/Setting/ctrlPA_SalaryTracker| loadToGrid :" + CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), w)
                w.Close()
            End Using 'hdx
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            Using w As StreamWriter = File.AppendText(_pathLog)
                _mylog.Log("Error | Paroll/Module/Paroll/Setting/ctrlPA_SalaryTracker | loadToGrid :" + ex.Message, w)
                w.Close()
            End Using 'hdx
        End Try
    End Function

    Private Sub ctrlUpload_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload.OkClicked

        Dim fileName As String
        Dim dsDataPrepare As New DataSet
        Dim workbook As Aspose.Cells.Workbook
        Dim worksheet As Aspose.Cells.Worksheet
        Dim dtDataHeard As New DataTable
        Dim startTime As DateTime = DateTime.UtcNow 'hdx
        Try
            Dim tempPath As String = ConfigurationManager.AppSettings("ExcelFileFolder")
            Dim savepath = Context.Server.MapPath(tempPath)
            For Each file As UploadedFile In ctrlUpload.UploadedFiles
                fileName = System.IO.Path.Combine(savepath, Guid.NewGuid().ToString() & ".xls")
                file.SaveAs(fileName, True)
                workbook = New Aspose.Cells.Workbook(fileName)
                If workbook.Worksheets.GetSheetByCodeName("IMPORT_RES_PLAN") Is Nothing Then
                    ShowMessage(Translate("File mẫu không đúng định dạng"), NotifyType.Warning)
                    Exit Sub
                End If
                worksheet = workbook.Worksheets(0)
                dsDataPrepare.Tables.Add(worksheet.Cells.ExportDataTableAsString(3, 0, worksheet.Cells.MaxRow + 1, worksheet.Cells.MaxColumn + 1, True))
                If System.IO.File.Exists(fileName) Then System.IO.File.Delete(fileName)
            Next
            dtData = dtData.Clone()
            For Each dt As DataTable In dsDataPrepare.Tables
                For Each row In dt.Rows
                    Dim isRow = ImportValidate.TrimRow(row)
                    If Not isRow Then
                        Continue For
                    End If
                    dtData.ImportRow(row)
                Next
            Next
            If loadToGrid(dtData) Then
                If SaveOT() Then
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    rgData.Rebind()
                    CurrentState = CommonMessage.STATE_NORMAL
                Else
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Success)
                    rgData.MasterTableView.SortExpressions.Clear()
                    rgData.Rebind()
                    CurrentState = CommonMessage.STATE_NORMAL
                End If
            End If
            UpdateControlState()
            Using w As StreamWriter = File.AppendText(_pathLog)
                _mylog.Log("Info | Paroll/Module/Paroll/Setting/ctrlPA_SalaryTracker| ctrlUpload_OkClicked :" + CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), w)
                w.Close()
            End Using 'hdx
        Catch ex As Exception
            ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
            Using w As StreamWriter = File.AppendText(_pathLog)
                _mylog.Log("Error | Paroll/Module/Paroll/Setting/ctrlPA_SalaryTracker | ctrlUpload_OkClicked :" + ex.Message, w)
                w.Close()
            End Using 'hdx
        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Try
            Dim startTime As DateTime = DateTime.UtcNow 'hdx
            rgData.Rebind()
            Using w As StreamWriter = File.AppendText(_pathLog)
                _mylog.Log("Info | Paroll/Module/Paroll/Setting/ctrlPA_SalaryTracker| btnSearch_Click :" + CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), w)
                w.Close()
            End Using 'hdx
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            Using w As StreamWriter = File.AppendText(_pathLog)
                _mylog.Log("Error | Paroll/Module/Paroll/Setting/ctrlPA_SalaryTracker | btnSearch_Click :" + ex.Message, w)
                w.Close()
            End Using 'hdx
        End Try
    End Sub
    'Private Sub charData1_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Charting.ChartItemDataBoundEventArgs) Handles charData1.ItemDataBound
    '    Dim data As DataRowView = DirectCast(e.DataItem, DataRowView)
    '    e.SeriesItem.Name = data("NAME")
    'End Sub
#End Region

#Region "Custom"

    Protected Function CreateDataFilter() As DataTable
        Dim dsData As DataSet
        Dim bCheck As Boolean = False
        Dim _filter As New PASalaryPlanningDTO
        Dim startTime As DateTime = DateTime.UtcNow 'hdx
        Try
            Using rep As New PayrollRepository
                Try
                    SetValueObjectByRadGrid(rgData, _filter)
                    If ctrlOrg.CurrentValue IsNot Nothing Then
                        _filter.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue)
                    Else
                        rgData.DataSource = New DataTable
                        Exit Function
                    End If
                    Dim _param = New ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue), _
                                                       .IS_DISSOLVE = ctrlOrg.IsDissolve}

                    Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()
                    _filter.YEAR = rntxtYear.Value
                    _filter.MONTH = rntxtMonth.Value
                    dsData = rep.GetSalaryTracker(_filter, _param)
                    rgData.DataSource = dsData.Tables(0)
                    RefreshPie(dsData.Tables(1))
                Catch ex As Exception
                    Throw ex
                End Try

            End Using
            Using w As StreamWriter = File.AppendText(_pathLog)
                _mylog.Log("Info | Paroll/Module/Paroll/Setting/ctrlPA_SalaryTracker| CreateDataFilter :" + CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), w)
                w.Close()
            End Using 'hdx
        Catch ex As Exception
            Using w As StreamWriter = File.AppendText(_pathLog)
                _mylog.Log("Error | Paroll/Module/Paroll/Setting/ctrlPA_SalaryTracker | CreateDataFilter :" + ex.Message, w)
                w.Close()
            End Using 'hdx
        End Try


    End Function


    Public Sub RefreshPie(ByVal dtData As DataTable)
        Dim dtDisplay As DataTable
        Dim startTime As DateTime = DateTime.UtcNow 'hdx
        Try
            dtDisplay = New DataTable("DATA")
            dtDisplay.Columns.Add("NAME")
            dtDisplay.Columns.Add("VALUE")
            Dim drRow = dtDisplay.NewRow
            drRow("NAME") = "Quỹ lương còn lại"
            drRow("VALUE") = 0
            If dtData.Rows.Count > 0 Then
                drRow("VALUE") = dtData.Rows(0)("TOTAL_CONLAI")
            End If
            dtDisplay.Rows.Add(drRow)
            drRow = dtDisplay.NewRow
            drRow("NAME") = "Chi phí thực tế"
            drRow("VALUE") = 0
            If dtData.Rows.Count > 0 Then
                drRow("VALUE") = dtData.Rows(0)("TOTAL_THUCTE")
            End If
            dtDisplay.Rows.Add(drRow)

            data = String.Empty
            For index As Integer = 0 To dtDisplay.Rows.Count - 1
                If index = dtDisplay.Rows.Count - 1 Then
                    data &= "{name: '" & dtDisplay.Rows(index)("NAME") & "', y: " & dtDisplay.Rows(index)("VALUE") & "}"
                Else
                    data &= "{name: '" & dtDisplay.Rows(index)("NAME") & "', y: " & dtDisplay.Rows(index)("VALUE") & "}," & vbNewLine
                End If
            Next
            'Đặt title cho chart
            'charData1.PlotArea.YAxis.Step = 1
            'charData1.PlotArea.XAxis.Step = 1
            ''charData1.ChartTitle.TextBlock.Text = "Thống kê " & cboType.Text
            'charData1.ChartTitle.Visible = False

            ''Load data
            'charData1.Series(0).DefaultLabelValue = "#ITEM [#%]"
            'charData1.Series(0).Appearance.LegendDisplayMode = ChartSeriesLegendDisplayMode.ItemLabels

            'If dtData.Rows.Count > 0 Then
            '    charData1.DataSource = dtDisplay
            'Else
            '    charData1.DataSource = Nothing
            'End If
            'charData1.Series(0).DataYColumn = "VALUE"
            'charData1.DataBind()
            Using w As StreamWriter = File.AppendText(_pathLog)
                _mylog.Log("Info | Paroll/Module/Paroll/Setting/ctrlPA_SalaryTracker| RefreshPie :" + CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), w)
                w.Close()
            End Using 'hdx
        Catch ex As Exception
            Throw ex
            Using w As StreamWriter = File.AppendText(_pathLog)
                _mylog.Log("Error | Paroll/Module/Paroll/Setting/ctrlPA_SalaryTracker | RefreshPie :" + ex.Message, w)
                w.Close()
            End Using 'hdx
        End Try
    End Sub
#End Region

End Class