Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlHU_ReportList
    Inherits Common.CommonView
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Profile/Modules/Profile/Report/" + Me.GetType().Name.ToString()

#Region "Properties"
    Dim dtable As DataTable
    Dim dsData As DataSet
    Dim rp As New ProfileRepository
    Dim wbook As Aspose.Cells.Workbook
    Dim sPath As String = ""
    Dim _error As String = ""
    Dim sOrgID As String

#End Region

#Region "Page"
    ''' <lastupdate>
    ''' 12/07/2017 14:28
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi de phuong thuc ViewLoad hien thi cac control tren trang
    ''' Thiet lap trang thai cac control tren page
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            ctrlOrganization.AutoPostBack = False
            ctrlOrganization.LoadDataAfterLoaded = True
            ctrlOrganization.OrganizationType = OrganizationType.OrganizationLocation
            ctrlOrganization.CheckBoxes = TreeNodeTypes.All
            ctrlOrganization.CheckParentNodes = False
            ctrlOrganization.CheckChildNodes = True

            If rgEmployeeList.SelectedItems.Count > 0 Then
                Dim item As GridDataItem = rgEmployeeList.SelectedItems(0)
                UpdateControlState(item.GetDataKeyValue("CODE"))
            Else
                rdFromDate.Visible = False
                rdToDate.Visible = False
                rdMonth.Visible = True

                cboMonth.Visible = False
                cboYear.Visible = False

                lblFromDate.Visible = False
                lblToDate.Visible = False
                lblMonth.Visible = True
                lblMonthMin.Visible = False
                lblYear.Visible = False

                rdFromDate.SelectedDate = Nothing
                rdToDate.SelectedDate = Nothing
            End If

            _myLog.WriteLog(_myLog._info, _classPath, method,
                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 12/07/2017 14:28
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi de phuong thuc khoi tao du lieu cho trang
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow

            rgEmployeeList.AllowCustomPaging = True
            'rgEmployeeList.ClientSettings.EnablePostBackOnRowClick = False
            InitControl()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    ''' <lastupdate>
    ''' 12/07/2017 14:28
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc khoi tao du lieu cho trang
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMainToolBar
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Export)
            CType(MainToolBar.Items(0), RadToolBarButton).CausesValidation = True

            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"

            _myLog.WriteLog(_myLog._info, _classPath, method,
                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 12/07/2017 14:28
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi de phuong thuc do du lieu cho cac control tren trang
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim table As New DataTable
            table.Columns.Add("YEAR", GetType(Integer))
            table.Columns.Add("ID", GetType(Integer))
            Dim row As DataRow
            For index = 2015 To Date.Now.Year + 1
                row = table.NewRow
                row("ID") = index
                row("YEAR") = index
                table.Rows.Add(row)
            Next
            FillRadCombobox(cboYear, table, "YEAR", "ID")
            cboYear.SelectedValue = Date.Now.Year

            Dim tableMonth As New DataTable
            tableMonth.Columns.Add("MONTH", GetType(String))
            tableMonth.Columns.Add("ID", GetType(String))
            Dim rowMonth As DataRow
            For index = 0 To 12
                rowMonth = tableMonth.NewRow

                If index = 0 Then
                    rowMonth("ID") = 0
                    rowMonth("MONTH") = ""
                    tableMonth.Rows.Add(rowMonth)
                Else
                    rowMonth("ID") = index
                    rowMonth("MONTH") = index
                    tableMonth.Rows.Add(rowMonth)
                End If
            Next
            FillRadCombobox(cboMonth, tableMonth, "MONTH", "ID")
            cboMonth.SelectedValue = Date.Now.Month

            _myLog.WriteLog(_myLog._info, _classPath, method,
                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"
    ''' <lastupdate>
    ''' 12/07/2017 14:28
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien command cho toolbar khi click
    ''' Command xuat file
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If rgEmployeeList.SelectedItems.Count <= 0 Then
                ShowMessage(Translate("Bạn phải chọn báo cáo"), Utilities.NotifyType.Warning)
                Exit Sub
            End If

            If ctrlOrganization.CheckedValues.Count = 0 Then
                ShowMessage("Bạn chưa tích chọn đơn vị", NotifyType.Warning)
                Exit Sub
            End If
            Dim item As GridDataItem = rgEmployeeList.SelectedItems(0)

            Select Case CType(e.Item, RadToolBarButton).CommandName

                Case CommonMessage.TOOLBARITEM_EXPORT

                    sOrgID = ctrlOrganization.CheckedValues.Aggregate(Function(x, y) x & "," & y)

                    If rgEmployeeList.SelectedItems.Count = 0 Then
                        ShowMessage(Translate("Bạn phải chọn báo cáo"), Utilities.NotifyType.Warning)
                        Exit Sub
                    End If

                    Select Case item.GetDataKeyValue("CODE")
                        Case "HU0101" ' BÁO CÁO DANH SÁCH NHÂN VIÊN
                            If rdMonth.SelectedDate Is Nothing Then
                                ShowMessage(Translate("Bạn chưa nhập điều kiện xuất báo cáo. "), Utilities.NotifyType.Warning)
                                Exit Sub
                            End If

                            HU0101(item.GetDataKeyValue("NAME"))

                        Case "HU0102" ' BÁO CÁO DANH SÁCH hợp đồng hiệu lực
                            If rdMonth.SelectedDate Is Nothing Then
                                ShowMessage(Translate("Bạn chưa nhập điều kiện xuất báo cáo. "), Utilities.NotifyType.Warning)
                                Exit Sub
                            End If

                            HU0102(item.GetDataKeyValue("NAME"))

                        Case "HU0103" ' BÁO CÁO DANH SÁCH hợp đồng hết hạn
                            If rdMonth.SelectedDate Is Nothing Then
                                ShowMessage(Translate("Bạn chưa nhập điều kiện xuất báo cáo. "), Utilities.NotifyType.Warning)
                                Exit Sub
                            End If

                            HU0103(item.GetDataKeyValue("NAME"))

                        Case "HU0104" ' BÁO CÁO DANH SÁCH chấm dứt hợp đồng
                            If rdFromDate.SelectedDate Is Nothing And rdToDate.SelectedDate Is Nothing Then
                                ShowMessage(Translate("Bạn chưa nhập điều kiện xuất báo cáo. "), Utilities.NotifyType.Warning)
                                Exit Sub
                            End If

                            HU0104(item.GetDataKeyValue("NAME"))

                        Case "HU0105" ' BÁO CÁO DANH SÁCH KHEN THƯỞNG
                            If rdFromDate.SelectedDate Is Nothing And rdToDate.SelectedDate Is Nothing Then
                                ShowMessage(Translate("Bạn chưa nhập điều kiện xuất báo cáo. "), Utilities.NotifyType.Warning)
                                Exit Sub
                            End If

                            HU0105(item.GetDataKeyValue("NAME"))

                        Case "HU0106" ' BÁO CÁO DANH SÁCH HỒ SƠ LƯƠNG , PHỤ CẤP
                            If rdMonth.SelectedDate Is Nothing Then
                                ShowMessage(Translate("Bạn chưa nhập điều kiện xuất báo cáo. "), Utilities.NotifyType.Warning)
                                Exit Sub
                            End If

                            HU0106(item.GetDataKeyValue("NAME"))

                        Case "HU0107" ' BÁO CÁO DANH SÁCH KỶ LUẬT
                            If rdFromDate.SelectedDate Is Nothing And rdToDate.SelectedDate Is Nothing Then
                                ShowMessage(Translate("Bạn chưa nhập điều kiện xuất báo cáo. "), Utilities.NotifyType.Warning)
                                Exit Sub
                            End If

                            HU0107(item.GetDataKeyValue("NAME"))

                        Case "HU0108" ' BÁO CÁO DANH SÁCH NHÂN VIÊN ĐIỀU CHUYỂN
                            If rdFromDate.SelectedDate Is Nothing And rdToDate.SelectedDate Is Nothing Then
                                ShowMessage(Translate("Bạn chưa nhập điều kiện xuất báo cáo. "), Utilities.NotifyType.Warning)
                                Exit Sub
                            End If

                            HU0108(item.GetDataKeyValue("NAME"))

                        Case "HU0109" ' BÁO CÁO DANH SÁCH NHÂN VIÊN KIÊM NHIỆM
                            If rdMonth.SelectedDate Is Nothing Then
                                ShowMessage(Translate("Bạn chưa nhập điều kiện xuất báo cáo. "), Utilities.NotifyType.Warning)
                                Exit Sub
                            End If

                            HU0109(item.GetDataKeyValue("NAME"))

                        Case "HU0110" ' BÁO CÁO DANH SÁCH THAY ĐỔI HỒ SƠ LƯƠNG
                            If rdFromDate.SelectedDate Is Nothing And rdToDate.SelectedDate Is Nothing Then
                                ShowMessage(Translate("Bạn chưa nhập điều kiện xuất báo cáo. "), Utilities.NotifyType.Warning)
                                Exit Sub
                            End If

                            HU0110(item.GetDataKeyValue("NAME"))

                        Case "HU0111" ' BÁO CÁO DANH SÁCH THAY ĐỔI CHỨC DANH
                            If rdFromDate.SelectedDate Is Nothing And rdToDate.SelectedDate Is Nothing Then
                                ShowMessage(Translate("Bạn chưa nhập điều kiện xuất báo cáo. "), Utilities.NotifyType.Warning)
                                Exit Sub
                            End If

                            HU0111(item.GetDataKeyValue("NAME"))

                        Case "HU0112" ' BÁO CÁO DANH SÁCH CBNV đang có đơn xin chấm dứt HĐLĐ
                            If rdFromDate.SelectedDate Is Nothing And rdToDate.SelectedDate Is Nothing Then
                                ShowMessage(Translate("Bạn chưa nhập điều kiện xuất báo cáo. "), Utilities.NotifyType.Warning)
                                Exit Sub
                            End If

                            HU0112(item.GetDataKeyValue("NAME"))

                        Case "HU0113" ' BÁO CÁO TỔNG HỢP BIẾN ĐỘNG NHÂN SỰ
                            If cboMonth.SelectedValue <= 0 And cboYear.SelectedValue <= 0 Then
                                ShowMessage(Translate("Bạn chưa nhập điều kiện xuất báo cáo. "), Utilities.NotifyType.Warning)
                                Exit Sub
                            End If

                            If cboMonth.SelectedValue > 0 And cboYear.SelectedValue <= 0 Then
                                ShowMessage(Translate("Bạn phải nhập năm. "), Utilities.NotifyType.Warning)
                                Exit Sub
                            End If

                            HU0113(item.GetDataKeyValue("NAME"))


                        Case "HU0114" ' BÁO CÁO QUÁ TRÌNH LƯƠNG, PHỤ CẤP
                            If cboYear.SelectedValue <= 0 Then
                                ShowMessage(Translate("Bạn chưa nhập điều kiện xuất báo cáo. "), Utilities.NotifyType.Warning)
                                Exit Sub
                            End If

                            HU0114(item.GetDataKeyValue("NAME"))
                    End Select



            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 12/07/2017 14:28
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien NeedDataSource cho rad grid rgEmployeeList
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgEmployeeList.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim rep As New ProfileRepository
            Dim _filter As New Se_ReportDTO
            Dim listReport As List(Of Se_ReportDTO)

            Try
                Dim MaximumRows As Integer
                SetValueObjectByRadGrid(rgEmployeeList, _filter)
                Dim Sorts As String = rgEmployeeList.MasterTableView.SortExpressions.GetSortString()
                _filter.MODULE_ID = 3

                If Sorts IsNot Nothing Then
                    listReport = rep.GetReportById(_filter, rgEmployeeList.CurrentPageIndex, rgEmployeeList.PageSize, MaximumRows, Sorts)
                Else
                    listReport = rep.GetReportById(_filter, rgEmployeeList.CurrentPageIndex, rgEmployeeList.PageSize, MaximumRows)
                End If
                rep.Dispose()
                rgEmployeeList.VirtualItemCount = MaximumRows
                rgEmployeeList.DataSource = listReport

            Catch ex As Exception
                Throw ex
            End Try

            _myLog.WriteLog(_myLog._info, _classPath, method,
                          CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    ''' <lastupdate>
    ''' 12/07/2017 14:28
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien PageIndexChanged cho rad grid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_PageIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles rgEmployeeList.PageIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            CType(sender, RadGrid).CurrentPageIndex = e.NewPageIndex
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 12/07/2017 14:28
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien SelectedIndexChanged cho rad grid rgEmployeeList
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgEmployeeList_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgEmployeeList.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If rgEmployeeList.SelectedItems.Count = 0 Then Exit Sub

            Dim item As GridDataItem = rgEmployeeList.SelectedItems(0)
            UpdateControlState(item.GetDataKeyValue("CODE"))

            _myLog.WriteLog(_myLog._info, _classPath, method,
                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Custom"
    ''' <lastupdate>
    ''' 12/07/2017 14:28
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc xuat bao cao Danh sach nhan vien
    ''' </summary>
    ''' <param name="strName"></param>
    ''' <remarks></remarks>
    Private Sub HU0101(ByVal strName As String)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            dsData = rp.ExportReport("PKG_PROFILE_REPORT.HU0101",
                                     rdMonth.SelectedDate,
                                     rdMonth.SelectedDate,
                                     sOrgID,
                                     ctrlOrganization.IsDissolve, "")
            sPath = Server.MapPath("~/ReportTemplates/Profile/Report" & "/HU0101.xlsx")

            dsData.Tables(0).TableName = "DATA"
            dsData.Tables(1).TableName = "DATA1"

            If dsData.Tables(0).Rows.Count <= 0 Then
                ShowMessage(Translate("Không tồn tại dữ liệu."), Utilities.NotifyType.Warning)
                Exit Sub
            End If

            Using xls As New ExcelCommon
                xls.ExportExcelTemplate(sPath, strName, dsData, Nothing, Response, _error, ExcelCommon.ExportType.Excel, False)
            End Using
            _myLog.WriteLog(_myLog._info, _classPath, method,
                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 12/07/2017 14:28
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc xuat bao cao Danh sach hop dong co hieu luc
    ''' </summary>
    ''' <param name="strName"></param>
    ''' <remarks></remarks>
    Private Sub HU0102(ByVal strName As String)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            dsData = rp.ExportReport("PKG_PROFILE_REPORT.HU0102", rdMonth.SelectedDate, rdMonth.SelectedDate, sOrgID, ctrlOrganization.IsDissolve, "")
            sPath = Server.MapPath("~/ReportTemplates/Profile/Report" & "/HU0102.xls")


            dsData.Tables(0).TableName = "DATA"
            dsData.Tables(1).TableName = "DATA1"

            If dsData.Tables(0).Rows.Count <= 0 Then
                ShowMessage(Translate("Không tồn tại dữ liệu."), Utilities.NotifyType.Warning)
                Exit Sub
            End If

            Using xls As New ExcelCommon
                xls.ExportExcelTemplate(sPath, strName, dsData, Nothing, Response, _error, ExcelCommon.ExportType.Excel)
            End Using

            _myLog.WriteLog(_myLog._info, _classPath, method,
                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 12/07/2017 14:28
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc xuat bao cao BÁO CÁO DANH SÁCH hợp đồng hết hạn
    ''' </summary>
    ''' <param name="strName"></param>
    ''' <remarks></remarks>
    Private Sub HU0103(ByVal strName As String)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            dsData = rp.ExportReport("PKG_PROFILE_REPORT.HU0103", rdMonth.SelectedDate, rdMonth.SelectedDate, sOrgID, ctrlOrganization.IsDissolve, "")
            sPath = Server.MapPath("~/ReportTemplates/Profile/Report" & "/HU0103.xls")

            dsData.Tables(0).TableName = "DATA"
            dsData.Tables(1).TableName = "DATA1"

            If dsData.Tables(0).Rows.Count <= 0 Then
                ShowMessage(Translate("Không tồn tại dữ liệu."), Utilities.NotifyType.Warning)
                Exit Sub
            End If

            Using xls As New ExcelCommon
                xls.ExportExcelTemplate(sPath, strName, dsData, Nothing, Response, _error, ExcelCommon.ExportType.Excel)
            End Using

            _myLog.WriteLog(_myLog._info, _classPath, method,
                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 12/07/2017 14:28
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc xuat bao cao BÁO CÁO DANH SÁCH chấm dứt hợp đồng
    ''' </summary>
    ''' <param name="strName"></param>
    ''' <remarks></remarks>
    Private Sub HU0104(ByVal strName As String)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow

            dsData = rp.ExportReport("PKG_PROFILE_REPORT.HU0104", rdFromDate.SelectedDate, rdToDate.SelectedDate, sOrgID, ctrlOrganization.IsDissolve, "")
            sPath = Server.MapPath("~/ReportTemplates/Profile/Report" & "/HU0104.xls")

            dsData.Tables(0).TableName = "DATA"
            dsData.Tables(1).TableName = "DATA1"

            If dsData.Tables(0).Rows.Count <= 0 Then
                ShowMessage(Translate("Không tồn tại dữ liệu."), Utilities.NotifyType.Warning)
                Exit Sub
            End If

            Using xls As New ExcelCommon
                xls.ExportExcelTemplate(sPath, strName, dsData, Nothing, Response, _error, ExcelCommon.ExportType.Excel)
            End Using

            _myLog.WriteLog(_myLog._info, _classPath, method,
                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 12/07/2017 14:28
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc xuat bao cao BÁO CÁO DANH SÁCH KHEN THƯỞNG
    ''' </summary>
    ''' <param name="strName"></param>
    ''' <remarks></remarks>
    Private Sub HU0105(ByVal strName As String)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            dsData = rp.ExportReport("PKG_PROFILE_REPORT.HU0105", rdFromDate.SelectedDate, rdToDate.SelectedDate, sOrgID, ctrlOrganization.IsDissolve, "")
            sPath = Server.MapPath("~/ReportTemplates/Profile/Report" & "/HU0105.xls")

            dsData.Tables(0).TableName = "DATA"
            dsData.Tables(1).TableName = "DATA1"

            If dsData.Tables(0).Rows.Count <= 0 Then
                ShowMessage(Translate("Không tồn tại dữ liệu."), Utilities.NotifyType.Warning)
                Exit Sub
            End If

            Using xls As New ExcelCommon
                xls.ExportExcelTemplate(sPath, strName, dsData, Nothing, Response, _error, ExcelCommon.ExportType.Excel)
            End Using

            _myLog.WriteLog(_myLog._info, _classPath, method,
                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 12/07/2017 14:28
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc xuat bao cao BÁO CÁO DANH SÁCH HỒ SƠ LƯƠNG , PHỤ CẤP
    ''' </summary>
    ''' <param name="strName"></param>
    ''' <remarks></remarks>
    Private Sub HU0106(ByVal strName As String)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            dsData = rp.ExportReport("PKG_PROFILE_REPORT.HU0106", rdMonth.SelectedDate, rdMonth.SelectedDate, sOrgID, ctrlOrganization.IsDissolve, "")
            sPath = Server.MapPath("~/ReportTemplates/Profile/Report" & "/HU0106.xlsx")

            dsData.Tables(0).TableName = "DATA"
            dsData.Tables(1).TableName = "DATA1"

            If dsData.Tables(0).Rows.Count <= 0 Then
                ShowMessage(Translate("Không tồn tại dữ liệu."), Utilities.NotifyType.Warning)
                Exit Sub
            End If

            Using xls As New ExcelCommon
                xls.ExportExcelTemplate(sPath, strName, dsData, Nothing, Response, _error, ExcelCommon.ExportType.Excel)
            End Using

            _myLog.WriteLog(_myLog._info, _classPath, method,
                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 12/07/2017 14:28
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc xuat bao cao BÁO CÁO DANH SÁCH KỶ LUẬT
    ''' </summary>
    ''' <param name="strName"></param>
    ''' <remarks></remarks>
    Private Sub HU0107(ByVal strName As String)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            dsData = rp.ExportReport("PKG_PROFILE_REPORT.HU0107", rdFromDate.SelectedDate, rdToDate.SelectedDate, sOrgID, ctrlOrganization.IsDissolve, "")
            sPath = Server.MapPath("~/ReportTemplates/Profile/Report" & "/HU0107.xls")

            dsData.Tables(0).TableName = "DATA"
            dsData.Tables(1).TableName = "DATA1"

            If dsData.Tables(0).Rows.Count <= 0 Then
                ShowMessage(Translate("Không tồn tại dữ liệu."), Utilities.NotifyType.Warning)
                Exit Sub
            End If

            Using xls As New ExcelCommon
                xls.ExportExcelTemplate(sPath, strName, dsData, Nothing, Response, _error, ExcelCommon.ExportType.Excel)
            End Using

            _myLog.WriteLog(_myLog._info, _classPath, method,
                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 12/07/2017 14:28
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc xuat bao cao BÁO CÁO DANH SÁCH NHAN VIEN DIEU CHUYEN
    ''' </summary>
    ''' <param name="strName"></param>
    ''' <remarks></remarks>
    Private Sub HU0108(ByVal strName As String)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            dsData = rp.ExportReport("PKG_PROFILE_REPORT.HU0108", rdFromDate.SelectedDate, rdToDate.SelectedDate, sOrgID, ctrlOrganization.IsDissolve, "")
            sPath = Server.MapPath("~/ReportTemplates/Profile/Report" & "/HU0108.xls")

            dsData.Tables(0).TableName = "DATA"
            dsData.Tables(1).TableName = "DATA1"

            If dsData.Tables(0).Rows.Count <= 0 Then
                ShowMessage(Translate("Không tồn tại dữ liệu."), Utilities.NotifyType.Warning)
                Exit Sub
            End If

            Using xls As New ExcelCommon
                xls.ExportExcelTemplate(sPath, strName, dsData, Nothing, Response, _error, ExcelCommon.ExportType.Excel)
            End Using

            _myLog.WriteLog(_myLog._info, _classPath, method,
                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 12/07/2017 14:28
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc xuat bao cao BÁO CÁO DANH SÁCH NHÂN VIÊN KIÊM NHIỆM
    ''' </summary>
    ''' <param name="strName"></param>
    ''' <remarks></remarks>
    Private Sub HU0109(ByVal strName As String)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            dsData = rp.ExportReport("PKG_PROFILE_REPORT.HU0109", rdMonth.SelectedDate, rdMonth.SelectedDate, sOrgID, ctrlOrganization.IsDissolve, "")
            sPath = Server.MapPath("~/ReportTemplates/Profile/Report" & "/HU0109.xls")

            dsData.Tables(0).TableName = "DATA"
            dsData.Tables(1).TableName = "DATA1"

            If dsData.Tables(0).Rows.Count <= 0 Then
                ShowMessage(Translate("Không tồn tại dữ liệu."), Utilities.NotifyType.Warning)
                Exit Sub
            End If

            Using xls As New ExcelCommon
                xls.ExportExcelTemplate(sPath, strName, dsData, Nothing, Response, _error, ExcelCommon.ExportType.Excel)
            End Using

            _myLog.WriteLog(_myLog._info, _classPath, method,
                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 12/07/2017 14:28
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc xuat bao cao BÁO CÁO DANH SÁCH THAY ĐỔI HỒ SƠ LƯƠNG
    ''' </summary>
    ''' <param name="strName"></param>
    ''' <remarks></remarks>
    Private Sub HU0110(ByVal strName As String)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            dsData = rp.ExportReport("PKG_PROFILE_REPORT.HU0110", rdFromDate.SelectedDate, rdToDate.SelectedDate, sOrgID, ctrlOrganization.IsDissolve, "")
            sPath = Server.MapPath("~/ReportTemplates/Profile/Report" & "/HU0110.xls")

            dsData.Tables(0).TableName = "DATA"
            dsData.Tables(1).TableName = "DATA1"

            If dsData.Tables(0).Rows.Count <= 0 Then
                ShowMessage(Translate("Không tồn tại dữ liệu."), Utilities.NotifyType.Warning)
                Exit Sub
            End If

            Using xls As New ExcelCommon
                xls.ExportExcelTemplate(sPath, strName, dsData, Nothing, Response, _error, ExcelCommon.ExportType.Excel)
            End Using

            _myLog.WriteLog(_myLog._info, _classPath, method,
                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 12/07/2017 14:28
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc xuat bao cao BÁO CÁO DANH SÁCH THAY ĐỔI CHỨC DANH
    ''' </summary>
    ''' <param name="strName"></param>
    ''' <remarks></remarks>
    Private Sub HU0111(ByVal strName As String)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            dsData = rp.ExportReport("PKG_PROFILE_REPORT.HU0111", rdFromDate.SelectedDate, rdToDate.SelectedDate, sOrgID, ctrlOrganization.IsDissolve, "")
            sPath = Server.MapPath("~/ReportTemplates/Profile/Report" & "/HU0111.xls")

            dsData.Tables(0).TableName = "DATA"
            dsData.Tables(1).TableName = "DATA1"

            If dsData.Tables(0).Rows.Count <= 0 Then
                ShowMessage(Translate("Không tồn tại dữ liệu."), Utilities.NotifyType.Warning)
                Exit Sub
            End If

            Using xls As New ExcelCommon
                xls.ExportExcelTemplate(sPath, strName, dsData, Nothing, Response, _error, ExcelCommon.ExportType.Excel)
            End Using
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                      CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 12/07/2017 14:28
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc xuat bao cao BÁO CÁO DANH SÁCH CBNV đang có đơn xin chấm dứt HĐLĐ
    ''' </summary>
    ''' <param name="strName"></param>
    ''' <remarks></remarks>
    Private Sub HU0112(ByVal strName As String)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            dsData = rp.ExportReport("PKG_PROFILE_REPORT.HU0112", rdFromDate.SelectedDate, rdToDate.SelectedDate, sOrgID, ctrlOrganization.IsDissolve, "")
            sPath = Server.MapPath("~/ReportTemplates/Profile/Report" & "/HU0112.xls")

            dsData.Tables(0).TableName = "DATA"
            dsData.Tables(1).TableName = "DATA1"

            If dsData.Tables(0).Rows.Count <= 0 Then
                ShowMessage(Translate("Không tồn tại dữ liệu."), Utilities.NotifyType.Warning)
                Exit Sub
            End If

            Using xls As New ExcelCommon
                xls.ExportExcelTemplate(sPath, strName, dsData, Nothing, Response, _error, ExcelCommon.ExportType.Excel)
            End Using

            _myLog.WriteLog(_myLog._info, _classPath, method,
                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 12/07/2017 14:28
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc xuat bao cao BAO CAO TỔNG HỢP BIẾN ĐỘNG NHÂN SỰ
    ''' </summary>
    ''' <param name="strName"></param>
    ''' <remarks></remarks>
    Private Sub HU0113(ByVal strName As String)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim dMontMin As Date
            Dim dYear As Date
            If cboMonth.SelectedValue > 0 And cboYear.SelectedValue > 0 Then
                sPath = Server.MapPath("~/ReportTemplates/Profile/Report" & "/HU0113.xls") ' theo tháng
                dMontMin = Date.Parse(cboMonth.SelectedValue & "/" & cboMonth.SelectedValue & "/" & cboYear.SelectedValue)
                dYear = Date.Parse(cboMonth.SelectedValue & "/" & cboMonth.SelectedValue & "/" & cboYear.SelectedValue)
            ElseIf cboMonth.SelectedValue <= 0 And cboYear.SelectedValue > 0 Then
                sPath = Server.MapPath("~/ReportTemplates/Profile/Report" & "/HU0114.xlsx") ' theo năm
                dMontMin = Date.Parse("1/1/" & cboYear.SelectedValue)
                dYear = Date.Parse("1/1/" & cboYear.SelectedValue)
            End If

            dsData = rp.ExportReport("PKG_PROFILE_REPORT.HU0113", dMontMin, dYear, sOrgID, ctrlOrganization.IsDissolve, "")


            dsData.Tables(0).TableName = "DATA"
            dsData.Tables(1).TableName = "DATA1"

            If dsData.Tables(0).Rows.Count <= 0 Then
                ShowMessage(Translate("Không tồn tại dữ liệu."), Utilities.NotifyType.Warning)
                Exit Sub
            End If

            Using xls As New ExcelCommon
                xls.ExportExcelTemplate(sPath, strName, dsData, Nothing, Response, _error, ExcelCommon.ExportType.Excel)
            End Using
            _myLog.WriteLog(_myLog._info, _classPath, method,
                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 12/07/2017 14:28
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc xuat bao cao BÁO CÁO QUÁ TRÌNH LƯƠNG, PHỤ CẤP
    ''' </summary>
    ''' <param name="strName"></param>
    ''' <remarks></remarks>
    Private Sub HU0114(ByVal strName As String)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim dYear As Date = Date.Parse(cboMonth.SelectedValue & "/" & cboMonth.SelectedValue & "/" & cboYear.SelectedValue)

            dsData = rp.ExportReport("PKG_PROFILE_REPORT.HU0114", dYear, dYear, sOrgID, ctrlOrganization.IsDissolve, "")

            sPath = Server.MapPath("~/ReportTemplates/Profile/Report" & "/HU0115.xls")


            dsData.Tables(0).TableName = "DATA"
            dsData.Tables(1).TableName = "DATA1"

            If dsData.Tables(0).Rows.Count <= 0 Then
                ShowMessage(Translate("Không tồn tại dữ liệu."), Utilities.NotifyType.Warning)
                Exit Sub
            End If

            Using xls As New ExcelCommon
                xls.ExportExcelTemplate(sPath, strName, dsData, Nothing, Response, _error, ExcelCommon.ExportType.Excel)
            End Using
            _myLog.WriteLog(_myLog._info, _classPath, method,
                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 12/07/2017 14:28
    ''' </lastupdate>
    ''' <summary>
    ''' Cap nhat trang thai cac control tren trang
    ''' </summary>
    ''' <param name="strReportID"></param>
    ''' <remarks></remarks>
    Public Sub UpdateControlState(ByVal strReportID As String)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            rdFromDate.Visible = False
            rdToDate.Visible = False
            rdMonth.Visible = False
            cboMonth.Visible = False
            cboYear.Visible = False

            lblFromDate.Visible = False
            lblToDate.Visible = False
            lblMonth.Visible = False
            lblMonthMin.Visible = False
            lblYear.Visible = False

            rdFromDate.SelectedDate = Nothing
            rdToDate.SelectedDate = Nothing

            Select Case strReportID
                Case "HU0101" ' BÁO CÁO DANH SÁCH NHÂN VIÊN
                    rdMonth.Visible = True
                    lblMonth.Visible = True
                Case "HU0102" ' BÁO CÁO DANH SÁCH hợp đồng hiệu lực
                    rdMonth.Visible = True
                    lblMonth.Visible = True
                Case "HU0103" ' BÁO CÁO DANH SÁCH hợp đồng hết hạn
                    rdMonth.Visible = True
                    lblMonth.Visible = True
                Case "HU0104" ' BÁO CÁO DANH SÁCH chấm dứt hợp đồng
                    rdFromDate.Visible = True
                    rdToDate.Visible = True
                    lblFromDate.Visible = True
                    lblToDate.Visible = True
                Case "HU0105" ' BÁO CÁO DANH SÁCH KHEN THƯỞNG
                    rdFromDate.Visible = True
                    rdToDate.Visible = True
                    lblFromDate.Visible = True
                    lblToDate.Visible = True
                Case "HU0106" ' BÁO CÁO DANH SÁCH HỒ SƠ LƯƠNG , PHỤ CẤP
                    rdMonth.Visible = True
                    lblMonth.Visible = True
                Case "HU0107" ' BÁO CÁO DANH SÁCH KỶ LUẬT
                    rdFromDate.Visible = True
                    rdToDate.Visible = True
                    lblFromDate.Visible = True
                    lblToDate.Visible = True
                Case "HU0108" ' BÁO CÁO DANH SÁCH NHÂN VIÊN ĐIỀU CHUYỂN
                    rdFromDate.Visible = True
                    rdToDate.Visible = True
                    lblFromDate.Visible = True
                    lblToDate.Visible = True
                Case "HU0109" ' BÁO CÁO DANH SÁCH NHÂN VIÊN KIÊM NHIỆM
                    rdMonth.Visible = True
                    lblMonth.Visible = True
                Case "HU0110" ' BÁO CÁO DANH SÁCH THAY ĐỔI HỒ SƠ LƯƠNG
                    rdFromDate.Visible = True
                    rdToDate.Visible = True
                    lblFromDate.Visible = True
                    lblToDate.Visible = True
                Case "HU0111" ' BÁO CÁO DANH SÁCH THAY ĐỔI CHỨC DANH
                    rdFromDate.Visible = True
                    rdToDate.Visible = True
                    lblFromDate.Visible = True
                    lblToDate.Visible = True
                Case "HU0112" ' BÁO CÁO DANH SÁCH CBNV đang có đơn xin chấm dứt HĐLĐ
                    rdFromDate.Visible = True
                    rdToDate.Visible = True
                    lblFromDate.Visible = True
                    lblToDate.Visible = True
                Case "HU0113" ' BÁO CÁO TỔNG HỢP BIẾN ĐỘNG NHÂN SỰ
                    cboMonth.Visible = True
                    cboYear.Visible = True
                    lblMonthMin.Visible = True
                    lblYear.Visible = True
                Case "HU0114" ' BÁO CÁO QUÁ TRÌNH LƯƠNG , PHỤ CẤP
                    cboYear.Visible = True
                    lblYear.Visible = True
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try

    End Sub

#End Region

End Class