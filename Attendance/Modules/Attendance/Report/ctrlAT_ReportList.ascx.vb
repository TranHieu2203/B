Imports Attendance.AttendanceBusiness
Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlAT_ReportList
    Inherits Common.CommonView

    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Attendance/Module/Attendance/Report/" + Me.GetType().Name.ToString()

#Region "Properties"
    Dim sOrgID As String
#End Region

#Region "Page"

    ''' <lastupdate>
    ''' 17/08/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức ViewLoad, hiển thị dữ liệu trên trang
    ''' Làm mới trang
    ''' Cập nhật các trạng thái của các control trên page
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
            If Not IsPostBack Then
                rdDate.Visible = False
                rdFromDate.Visible = False
                rdToDate.Visible = False
                lblDenNgay.Visible = False
                lblNgay.Visible = False
                lblTuNgay.Visible = False
            End If
            Refresh()
            'rdDate.SelectedDate = Date.Now
            'rdFromDate.SelectedDate = Date.Now
            'rdToDate.SelectedDate = Date.Now
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 17/08/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức khởi tạo dữ liệu cho các control trên trang
    ''' Gọi phương thức khởi tạo 
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            rgReportList.AllowCustomPaging = True
            'rgReportList.ClientSettings.EnablePostBackOnRowClick = False
            InitControl()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                          CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 17/08/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ham khoi taso toolbar voi cac button them moi, sua, xuat file, xoa, in hop dong
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMainToolBar
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Export)

            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 17/08/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức BindData
    ''' Gọi phương thức load dữ liệu cho các combobox
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim lsData As List(Of AT_PERIODDTO)
            Dim rep As New AttendanceRepository
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
            Dim period As New AT_PERIODDTO
            period.ORG_ID = 1
            period.YEAR = Date.Now.Year
            lsData = rep.LOAD_PERIODBylinq(period)
            FillRadCombobox(cboPeriod, lsData, "PERIOD_NAME", "PERIOD_ID", True)
            If lsData.Count > 0 Then
                Dim periodid = (From d In lsData Where d.START_DATE.Value.ToString("yyyyMM").Equals(Date.Now.ToString("yyyyMM")) Select d).FirstOrDefault
                If periodid IsNot Nothing Then
                    cboPeriod.SelectedValue = periodid.PERIOD_ID.ToString()
                Else
                    cboPeriod.SelectedIndex = 0
                End If
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 17/08/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc lam moi cac control theo tuy chon Message, 
    ''' Message co gia tri default la ""
    ''' Xet cac tuy chon gia tri cua Message la UpdateView, InsertView
    ''' Bind lai du lieu cho grid rgRegisterLeave
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If Message = CommonMessage.ACTION_UPDATED Then
                rgReportList.Rebind()
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Event"

    ''' <lastupdate>
    ''' 17/08/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien command khi click item tren toolbar
    ''' Command xuat excel
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim dsData As DataSet
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim sReportId As String = ""
            Dim _param As ParamDTO
            If rgReportList.SelectedItems.Count = 0 Then
                ShowMessage(Translate("Bạn phải chọn báo cáo"), Utilities.NotifyType.Error)
                Exit Sub
            End If
            If ctrlOrganization.CheckedValues.Count = 0 Then
                ShowMessage("Bạn chưa tích chọn đơn vị", NotifyType.Warning)
                Exit Sub
            End If
            Dim item As GridDataItem = rgReportList.SelectedItems(0)
            sReportId = item.GetDataKeyValue("CODE")
            Select Case CType(e.Item, RadToolBarButton).CommandName

                Case CommonMessage.TOOLBARITEM_EXPORT

                    sOrgID = ctrlOrganization.CheckedValues.Aggregate(Function(x, y) x & "," & y)


                    If sReportId = "AT_008" Then
                        _param = New ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrganization.CurrentValue)
                                                   }
                    Else
                        If cboPeriod.SelectedValue = "" Then
                            _param = New ParamDTO With {.S_ORG_ID = sOrgID, _
                                                        .PERIOD_ID = 0,
                                                        .IS_DISSOLVE = ctrlOrganization.IsDissolve,
                                                        .YEAR = cboYear.SelectedValue}
                        Else
                            _param = New ParamDTO With {.S_ORG_ID = sOrgID, _
                                                        .PERIOD_ID = Decimal.Parse(cboPeriod.SelectedValue),
                                                        .IS_DISSOLVE = ctrlOrganization.IsDissolve,
                                                        .YEAR = cboYear.SelectedValue}
                        End If
                    End If
                    If sReportId = "AT_009" Then
                        _param = Nothing
                        _param = New ParamDTO With {.S_ORG_ID = sOrgID}
                    End If
                    If sReportId = "AT_010" Then
                        _param = Nothing
                        _param = New ParamDTO With {.S_ORG_ID = sOrgID, _
                                                        .PERIOD_ID = Decimal.Parse(cboPeriod.SelectedValue)
                                                        }

                    End If
                    If sReportId = "AT_011" Then
                        _param = Nothing
                        _param = New ParamDTO With {.S_ORG_ID = sOrgID, _
                                                        .PERIOD_ID = Decimal.Parse(cboPeriod.SelectedValue)
                                                        }

                    End If
                    Using rep As New AttendanceRepository
                        Select Case sReportId
                            'bảng chấm công
                            Case "AT_001"

                                dsData = rep.GET_AT001(_param)
                                Dim dsDataTR As DataSet = rep.GET_AT002(_param)
                                PrintReport_AT_001(dsData.Tables(0),
                                                   dsData.Tables(1),
                                                   dsData.Tables(2),
                                                   dsDataTR,
                                                   "\ReportTemplates\Attendance\Report\AT_001.xlsx",
                                                   "Bảng công tháng" & DateTime.Now.Day & "_" & DateTime.Now.Hour & "_" &
                                                   DateTime.Now.Minute & "_" & DateTime.Now.Millisecond & ".xls")
                                ' bảng chấm cơm
                            Case "AT_002"
                                dsData = rep.GET_AT002(_param)
                                PrintReport_AT_002(dsData.Tables(0), dsData.Tables(1),
                                                   "\ReportTemplates\Attendance\Report\AT_002.xlsx",
                                                   "Bảng chấm công cơm" & DateTime.Now.Day & "_" & DateTime.Now.Hour & "_" &
                                                   DateTime.Now.Minute & "_" & DateTime.Now.Millisecond & ".xls")

                                'nghỉ bù và nghỉ phép
                            Case "AT_003"
                                Dim dtDataPB As New DataTable

                                dtDataPB = rep.GET_AT005(_param).Tables(0)
                                dtDataPB.TableName = "DataPB"
                                dsData = rep.GET_AT003(_param)

                                dsData.Tables.Add(dtDataPB.Copy)
                                PrintReport_AT_003(dsData, "Báo cáo theo dõi nghỉ bù" & DateTime.Now.Day & "_" & DateTime.Now.Hour & "_" & DateTime.Now.Minute & "_" & DateTime.Now.Millisecond & ".xls")

                                'Bảng tổng hợp thời gian làm thêm giờ
                            Case "AT_004"
                                dsData = rep.GET_AT004(_param)
                                PrintReport_AT_004(dsData, item.GetDataKeyValue("NAME"))

                                ' nghỉ phép
                            Case "AT_005"
                                dsData = rep.GET_AT005(_param)
                                PrintReport_AT_005(dsData, "\ReportTemplates\Attendance\Report\AT_005.xlsx", "Báo cáo theo dõi nghỉ phép" & DateTime.Now.Day & "_" & DateTime.Now.Hour & "_" & DateTime.Now.Minute & "_" & DateTime.Now.Millisecond & ".xls")

                                'Tổng hợp kiểm soát chấm công & Dánh sách công cần xác nhận
                            Case "AT_006"
                                Dim dtDataXN As New DataTable
                                dtDataXN = rep.GET_AT007(_param).Tables(0)
                                dtDataXN.TableName = "Data1"
                                dsData = rep.GET_AT006(_param)
                                dsData.Tables.Add(dtDataXN.Copy)
                                PrintReport_AT_006(dsData, item.GetDataKeyValue("NAME"))

                                'Dánh sách công cần xác nhận
                            Case "AT_007"
                                dsData = rep.GET_AT007(_param)
                                PrintReport_AT_007(dsData, item.GetDataKeyValue("NAME"))
                            Case "AT_008"
                                If rdDate.SelectedDate Is Nothing Then
                                    ShowMessage(Translate("Bạn phải chọn ngày cần xem báo cáo!"), NotifyType.Warning)
                                    Exit Sub
                                End If
                                If ctrlOrganization.CurrentValue Is Nothing Then
                                    ShowMessage(Translate("Bạn phải chọn đơn vị cần xem báo cáo!"), NotifyType.Warning)
                                    Exit Sub
                                End If
                                If ctrlOrganization.CurrentValue = 0 Then
                                    ShowMessage(Translate("Bạn phải chọn đơn vị cần xem báo cáo!"), NotifyType.Warning)
                                    Exit Sub
                                End If
                                Dim dtparam As New DataTable
                                Dim column1 As DataColumn = New DataColumn("WORKDAY")
                                column1.DataType = System.Type.GetType("System.String")
                                dtparam.Columns.Add(column1)
                                Dim Row1 As DataRow
                                Row1 = dtparam.NewRow()
                                Row1("WORKDAY") = Format(rdDate.SelectedDate, "dd/MM/yyyy")
                                dtparam.Rows.Add(Row1)
                                dsData = rep.GET_AT008(_param, rdDate.SelectedDate)
                                dsData.Tables.Add(dtparam)
                                PrintReport_AT_008(dsData, item.GetDataKeyValue("NAME"))
                            Case "AT_009"
                                If rdFromDate.SelectedDate Is Nothing Then
                                    ShowMessage(Translate("Bạn phải chọn từ ngày!"), NotifyType.Warning)
                                    Exit Sub
                                End If
                                If rdToDate.SelectedDate Is Nothing Then
                                    ShowMessage(Translate("Bạn phải chọn đến ngày!"), NotifyType.Warning)
                                    Exit Sub
                                End If
                                If ctrlOrganization.CurrentValue Is Nothing Then
                                    ShowMessage(Translate("Bạn phải chọn đơn vị cần xem báo cáo!"), NotifyType.Warning)
                                    Exit Sub
                                End If
                                If ctrlOrganization.CurrentValue = 0 Then
                                    ShowMessage(Translate("Bạn phải chọn đơn vị cần xem báo cáo!"), NotifyType.Warning)
                                    Exit Sub
                                End If
                                If rdFromDate.SelectedDate > rdToDate.SelectedDate Then
                                    ShowMessage(Translate("Từ ngày không được lớn hơn đến ngày"), NotifyType.Warning)
                                    Exit Sub
                                End If
                                Dim dtparam As New DataTable
                                Dim column1 As DataColumn = New DataColumn("FROMDATE")
                                Dim column2 As DataColumn = New DataColumn("TODATE")
                                column1.DataType = System.Type.GetType("System.String")
                                column2.DataType = System.Type.GetType("System.String")
                                dtparam.Columns.Add(column1)
                                dtparam.Columns.Add(column2)
                                Dim Row1 As DataRow
                                Row1 = dtparam.NewRow()
                                Row1("FROMDATE") = Format(rdFromDate.SelectedDate, "dd/MM/yyyy")
                                Row1("TODATE") = Format(rdToDate.SelectedDate, "dd/MM/yyyy")
                                dtparam.Rows.Add(Row1)
                                dsData = rep.GET_AT009(_param, rdFromDate.SelectedDate, rdToDate.SelectedDate)
                                dsData.Tables.Add(dtparam)
                                PrintReport_AT_009(dsData, item.GetDataKeyValue("NAME"))
                            Case "AT_010"
                                dsData = rep.GET_AT010(_param)
                                Dim dsDataTR As DataSet = rep.GET_AT002(_param)
                                PrintReport_AT_010(dsDataTR.Tables(0),
                                                   dsData.Tables(0),
                                                   "\ReportTemplates\Attendance\Report\AT_010.xls",
                                                   "Bảng tổng hợp nhân viên đi làm theo tháng" & DateTime.Now.Day & "_" & DateTime.Now.Hour & "_" &
                                                   DateTime.Now.Minute & "_" & DateTime.Now.Millisecond & ".xls")
                            Case "AT_011"
                                dsData = rep.GET_AT011(_param)
                                Dim dsDataTR As DataSet = rep.GET_AT002(_param)
                                PrintReport_AT_011(dsDataTR.Tables(0),
                                                   dsData.Tables(0),
                                                   "\ReportTemplates\Attendance\Report\AT_011.xls",
                                                   "Bảng tổng hợp nghỉ theo tháng" & DateTime.Now.Day & "_" & DateTime.Now.Hour & "_" &
                                                   DateTime.Now.Minute & "_" & DateTime.Now.Millisecond & ".xls")


                        End Select
                    End Using

            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                       CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 17/08/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' xu ly su kien SelectedIndexChanged cua control cboYear
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlYear_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboYear.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim dtData As List(Of AT_PERIODDTO)
            Dim rep As New AttendanceRepository
            Dim period As New AT_PERIODDTO
            period.ORG_ID = Decimal.Parse(ctrlOrganization.CurrentValue)
            period.YEAR = Decimal.Parse(cboYear.SelectedValue)
            dtData = rep.LOAD_PERIODBylinq(period)
            cboPeriod.ClearSelection()
            FillRadCombobox(cboPeriod, dtData, "PERIOD_NAME", "PERIOD_ID", True)
            If dtData.Count > 0 Then
                Dim periodid = (From d In dtData Where d.START_DATE.Value.ToString("yyyyMM").Equals(Date.Now.ToString("yyyyMM")) Select d).FirstOrDefault
                If periodid IsNot Nothing Then
                    cboPeriod.SelectedValue = periodid.PERIOD_ID.ToString()
                Else
                    cboPeriod.SelectedIndex = 0
                End If
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                   CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 17/08/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien NeedDataSource cho rgReportList
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgReportList.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim rep As New AttendanceRepository
            Dim _filter As New Se_ReportDTO
            Dim listReport As List(Of Se_ReportDTO)

            Try
                Dim MaximumRows As Integer
                SetValueObjectByRadGrid(rgReportList, _filter)
                Dim Sorts As String = rgReportList.MasterTableView.SortExpressions.GetSortString()
                _filter.MODULE_ID = 2

                If Sorts IsNot Nothing Then
                    listReport = rep.GetReportById(_filter, rgReportList.CurrentPageIndex, rgReportList.PageSize, MaximumRows, Sorts)
                Else
                    listReport = rep.GetReportById(_filter, rgReportList.CurrentPageIndex, rgReportList.PageSize, MaximumRows)
                End If

                rgReportList.VirtualItemCount = MaximumRows
                rgReportList.DataSource = listReport

            Catch ex As Exception
                Throw ex
            End Try
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 17/08/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien PageIndexChanged cho rgReportList
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_PageIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles rgReportList.PageIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            CType(sender, RadGrid).CurrentPageIndex = e.NewPageIndex
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                       CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 17/08/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien SelectedIndexChanged cho rgReportList
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgReportList_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgReportList.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If rgReportList.SelectedItems.Count = 0 Then Exit Sub

            Dim item As GridDataItem = rgReportList.SelectedItems(0)
            UpdateControlState(item.GetDataKeyValue("CODE"))
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Custom"

    ''' <lastupdate>
    ''' 17/08/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Update trang thai menu toolbar
    ''' </summary>
    ''' <param name="strReportID"></param>
    ''' <remarks></remarks>
    Public Sub UpdateControlState(ByVal strReportID As String)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            lblYear.Visible = False
            cboYear.Visible = False
            lblPeriod.Visible = False
            cboPeriod.Visible = False
            Select Case strReportID
                Case "AT_001" ' BÁO CÁO BẢNG CÔNG THÁNG
                    lblYear.Visible = True
                    cboYear.Visible = True
                    lblPeriod.Visible = True
                    cboPeriod.Visible = True
                    rdDate.Visible = False
                    rdFromDate.Visible = False
                    rdToDate.Visible = False
                    lblDenNgay.Visible = False
                    lblNgay.Visible = False
                    lblTuNgay.Visible = False
                Case "AT_002" ' BÁO CÁO BẢNG CHẤM CÔNG CƠM
                    lblYear.Visible = True
                    cboYear.Visible = True
                    lblPeriod.Visible = True
                    cboPeriod.Visible = True
                    rdDate.Visible = False
                    rdFromDate.Visible = False
                    rdToDate.Visible = False
                    lblDenNgay.Visible = False
                    lblNgay.Visible = False
                    lblTuNgay.Visible = False
                Case "AT_003" ' BÁO CÁO THEO DÕI NGHỈ BÙ
                    lblYear.Visible = True
                    cboYear.Visible = True
                    rdDate.Visible = False
                    rdFromDate.Visible = False
                    rdToDate.Visible = False
                    lblDenNgay.Visible = False
                    lblNgay.Visible = False
                    lblTuNgay.Visible = False
                Case "AT_004" ' BÁO CÁO TỔNG HỢP THỜI GIAN LÀM THÊM GIỜ
                    lblYear.Visible = True
                    cboYear.Visible = True
                    lblPeriod.Visible = True
                    cboPeriod.Visible = True
                    rdDate.Visible = False
                    rdFromDate.Visible = False
                    rdToDate.Visible = False
                    lblDenNgay.Visible = False
                    lblNgay.Visible = False
                    lblTuNgay.Visible = False
                Case "AT_005" ' BÁO CÁO THEO DÕI NGHỈ PHÉP
                    lblYear.Visible = True
                    cboYear.Visible = True
                    rdDate.Visible = False
                    rdFromDate.Visible = False
                    rdToDate.Visible = False
                    lblDenNgay.Visible = False
                    lblNgay.Visible = False
                    lblTuNgay.Visible = False
                Case "AT_006" ' Tổng hợp kiểm soát chấm công
                    lblYear.Visible = True
                    cboYear.Visible = True
                    lblPeriod.Visible = True
                    cboPeriod.Visible = True
                    rdDate.Visible = False
                    rdFromDate.Visible = False
                    rdToDate.Visible = False
                    lblDenNgay.Visible = False
                    lblNgay.Visible = False
                    lblTuNgay.Visible = False
                Case "AT_007" ' DANH SÁCH CÔNG CẦN XÁC NHẬN
                    lblYear.Visible = True
                    cboYear.Visible = True
                    lblPeriod.Visible = True
                    cboPeriod.Visible = True
                    rdDate.Visible = False
                    rdFromDate.Visible = False
                    rdToDate.Visible = False
                    lblDenNgay.Visible = False
                    lblNgay.Visible = False
                    lblTuNgay.Visible = False
                Case "AT_008" ' BÁO CÁO TỶ LỆ ĐI LÀM
                    lblYear.Visible = False
                    cboYear.Visible = False
                    lblPeriod.Visible = False
                    cboPeriod.Visible = False
                    rdDate.Visible = True
                    rdFromDate.Visible = False
                    rdToDate.Visible = False
                    lblDenNgay.Visible = False
                    lblNgay.Visible = True
                    lblTuNgay.Visible = False
                Case "AT_009" ' BÁO CÁO DANH SÁCH NHÂN VIÊN CÓ ĐI LÀM
                    lblYear.Visible = False
                    cboYear.Visible = False
                    lblPeriod.Visible = False
                    cboPeriod.Visible = False
                    rdDate.Visible = False
                    rdFromDate.Visible = True
                    rdToDate.Visible = True
                    lblDenNgay.Visible = True
                    lblNgay.Visible = False
                    lblTuNgay.Visible = True
                Case "AT_010" ' BÁO CÁO THỐNG KÊ NHÂN VIÊN ĐI LÀM
                    lblYear.Visible = True
                    cboYear.Visible = True
                    lblPeriod.Visible = True
                    cboPeriod.Visible = True
                    rdDate.Visible = False
                    rdFromDate.Visible = False
                    rdToDate.Visible = False
                    lblDenNgay.Visible = False
                    lblNgay.Visible = False
                    lblTuNgay.Visible = False
                Case "AT_011" ' BÁO CÁO TỔNG HỢP NGHỈ
                    lblYear.Visible = True
                    cboYear.Visible = True
                    lblPeriod.Visible = True
                    cboPeriod.Visible = True
                    rdDate.Visible = False
                    rdFromDate.Visible = False
                    rdToDate.Visible = False
                    lblDenNgay.Visible = False
                    lblNgay.Visible = False
                    lblTuNgay.Visible = False
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 17/08/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ham xu ly load du lieu vao file excel cho report 
    ''' </summary>
    ''' <param name="dDataDetail"></param>
    ''' <param name="sPathTemplate"></param>
    ''' <param name="sStringFileName"></param>
    ''' <remarks></remarks>
    Private Sub PrintReport_AT_005(ByVal dDataDetail As DataSet, ByVal sPathTemplate As String, ByVal sStringFileName As String)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If dDataDetail.Tables(0).Rows.Count = 0 Then
                ShowMessage(Translate("Không có dữ liệu in báo cáo"), NotifyType.Warning)
                Exit Sub
            End If
            Dim iLastRow As Integer = 0

            Dim sFileTmp As String = Server.MapPath("~" + sPathTemplate)
            Dim wBook As New Aspose.Cells.Workbook(sFileTmp)
            Dim wSheet As Aspose.Cells.Worksheet = wBook.Worksheets.Item(0)
            Dim cells As Aspose.Cells.Cells = wSheet.Cells
            Dim style As New Aspose.Cells.Style
            style.VerticalAlignment = Aspose.Cells.TextAlignmentType.Center
            cells("C2").PutValue(ctrlOrganization.CurrentText.ToUpper)
            cells("J5").PutValue("NĂM " & cboYear.Text.ToString)
            cells("L6").PutValue("Số ngày đã nghỉ " & vbCrLf & "Năm " & cboYear.Text)
            Dim desig As New Aspose.Cells.WorkbookDesigner
            dDataDetail.Tables(0).TableName = "DATA"
            wBook.CalculateFormula()
            desig.Workbook = wBook
            desig.SetDataSource(dDataDetail)
            desig.Process()
            wBook.Save(HttpContext.Current.Response, sStringFileName, Aspose.Cells.ContentDisposition.Attachment, New Aspose.Cells.OoxmlSaveOptions(Aspose.Cells.SaveFormat.Excel97To2003))
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                   CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 17/08/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ham xu ly load du lieu vao file excel cho report 
    ''' </summary>
    ''' <param name="dsData"></param>
    ''' <param name="sName"></param>
    ''' <remarks></remarks>
    Private Sub PrintReport_AT_004(ByVal dsData As DataSet, ByVal sName As String)
        Dim sPath As String
        Dim _error As String
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            sPath = Server.MapPath("~/ReportTemplates/Attendance/Report/AT_004.xlsx")
            dsData.Tables(0).TableName = "DATA"
            dsData.Tables(1).TableName = "DATA1"
            dsData.Tables(2).TableName = "PARAM"
            If dsData.Tables(0).Rows.Count <= 0 Then
                ShowMessage(Translate("Không có dữ liệu theo điều kiện bạn chọn."), Utilities.NotifyType.Error)
                Exit Sub
            End If
            Using xls As New ExcelCommon
                xls.ExportExcelTemplate(sPath, sName, dsData, Nothing, Response, _error, ExcelCommon.ExportType.Excel)
            End Using
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 17/08/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ham xu ly load du lieu vao file excel cho report 
    ''' </summary>
    ''' <param name="dsData"></param>
    ''' <param name="sName"></param>
    ''' <remarks></remarks>
    Private Sub PrintReport_AT_006(ByVal dsData As DataSet, ByVal sName As String)
        Dim sPath As String
        Dim _error As String
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            sPath = Server.MapPath("~/ReportTemplates/Attendance/Report/AT_006.xlsx")
            dsData.Tables(0).TableName = "DATA"
            dsData.Tables(1).TableName = "PARAM"
            If dsData.Tables(0).Rows.Count <= 0 Then
                ShowMessage(Translate("Không có dữ liệu theo điều kiện bạn chọn."), Utilities.NotifyType.Error)
                Exit Sub
            End If
            Using xls As New ExcelCommon
                xls.ExportExcelTemplate(sPath, sName, dsData, Nothing, Response, _error, ExcelCommon.ExportType.Excel, False)
            End Using
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 17/08/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ham xu ly load du lieu vao file excel cho report 
    ''' </summary>
    ''' <param name="dsData"></param>
    ''' <param name="sName"></param>
    ''' <remarks></remarks>
    Private Sub PrintReport_AT_007(ByVal dsData As DataSet, ByVal sName As String)
        Dim sPath As String
        Dim _error As String
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            sPath = Server.MapPath("~/ReportTemplates/Attendance/Report/AT_007.xlsx")
            dsData.Tables(0).TableName = "DATA"
            dsData.Tables(1).TableName = "PARAM"
            If dsData.Tables(0).Rows.Count <= 0 Then
                ShowMessage(Translate("Không có dữ liệu theo điều kiện bạn chọn."), Utilities.NotifyType.Error)
                Exit Sub
            End If
            Using xls As New ExcelCommon
                xls.ExportExcelTemplate(sPath, sName, dsData, Nothing, Response, _error, ExcelCommon.ExportType.Excel, False)
            End Using
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 17/08/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ham xu ly load du lieu vao file excel cho report 
    ''' </summary>
    ''' <param name="dsData"></param>
    ''' <param name="sName"></param>
    ''' <remarks></remarks>
    Private Sub PrintReport_AT_008(ByVal dsData As DataSet, ByVal sName As String)
        Dim sPath As String
        Dim _error As String
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            sPath = Server.MapPath("~/ReportTemplates/Attendance/Report/AT_008.xlsx")
            dsData.Tables(0).TableName = "DATA"
            dsData.Tables(1).TableName = "PARAM"
            If dsData.Tables(0).Rows.Count <= 0 Then
                ShowMessage(Translate("Không có dữ liệu theo điều kiện bạn chọn."), Utilities.NotifyType.Error)
                Exit Sub
            End If
            Using xls As New ExcelCommon
                xls.ExportExcelTemplate(sPath, sName, dsData, Nothing, Response, _error, ExcelCommon.ExportType.Excel, False)
            End Using
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 17/08/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ham xu ly load du lieu vao file excel cho report 
    ''' </summary>
    ''' <param name="dsData"></param>
    ''' <param name="sName"></param>
    ''' <remarks></remarks>
    Private Sub PrintReport_AT_009(ByVal dsData As DataSet, ByVal sName As String)
        Dim sPath As String
        Dim _error As String
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            sPath = Server.MapPath("~/ReportTemplates/Attendance/Report/AT_009.xlsx")
            dsData.Tables(0).TableName = "DATA"
            dsData.Tables(1).TableName = "PARAM"
            If dsData.Tables(0).Rows.Count <= 0 Then
                ShowMessage(Translate("Không có dữ liệu theo điều kiện bạn chọn."), Utilities.NotifyType.Error)
                Exit Sub
            End If
            Using xls As New ExcelCommon
                xls.ExportExcelTemplate(sPath, sName, dsData, Nothing, Response, _error, ExcelCommon.ExportType.Excel, False)
            End Using
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 17/08/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ham xu ly load du lieu vao file excel cho report 
    ''' </summary>
    ''' <param name="dDataDetail"></param>
    ''' <param name="sStringFileName"></param>
    ''' <remarks></remarks>
    Public Sub PrintReport_AT_003(ByVal dDataDetail As DataSet, ByVal sStringFileName As String)
        Dim sPath As String
        Dim _error As String
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            sPath = Server.MapPath("~/ReportTemplates/Attendance/Report/AT_003.xlsx")
            dDataDetail.Tables(0).TableName = "DATA"
            dDataDetail.Tables(1).TableName = "PARAM"
            If dDataDetail.Tables(0).Rows.Count <= 0 Then
                ShowMessage(Translate("Không có dữ liệu theo điều kiện bạn chọn."), Utilities.NotifyType.Error)
                Exit Sub
            End If
            Using xls As New ExcelCommon
                xls.ExportExcelTemplate(sPath, sStringFileName, dDataDetail, Nothing, Response, _error, ExcelCommon.ExportType.Excel)
            End Using
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 17/08/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ham xu ly load du lieu vao file excel cho report 
    ''' </summary>
    ''' <param name="dDataDetailPeriod"></param>
    ''' <param name="sPathTemplate"></param>
    ''' <param name="sStringFileName"></param>
    ''' <param name="dDataHeadColPeriod"></param>
    ''' <remarks></remarks>
    Public Sub PrintReport_AT_002(ByVal dDataHeadColPeriod As DataTable, ByVal dDataDetailPeriod As DataTable, ByVal sPathTemplate As String, ByVal sStringFileName As String)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If dDataDetailPeriod.Rows.Count = 0 Then
                ShowMessage(Translate("Không có dữ liệu in báo cáo"), NotifyType.Warning)
                Exit Sub
            End If
            Dim iLastRow As Integer = 0

            Dim sFileTmp As String = Server.MapPath("~" + sPathTemplate)
            Dim wBook As New Aspose.Cells.Workbook(sFileTmp)
            Dim wSheetTR As Aspose.Cells.Worksheet = wBook.Worksheets.Item(0)
            Dim cellsTR As Aspose.Cells.Cells = wSheetTR.Cells
            Dim styleTR As New Aspose.Cells.Style
            styleTR.VerticalAlignment = Aspose.Cells.TextAlignmentType.Center

            Dim iStartColumnTR As Integer = 5
            Dim iStartRowTR As Integer = 4
            Dim totalColumnTR As Integer = 0
            Dim iDelTR As Integer = 0
            '1. INSERT FROMDATE TO ENDDATE
            Dim startdateTR = Date.Parse(dDataHeadColPeriod.Rows(0)("START_DATE"))
            Dim enddateTR = Date.Parse(dDataHeadColPeriod.Rows(0)("END_DATE"))
            While startdateTR <= enddateTR
                cellsTR.InsertColumn(iStartColumnTR + 1, 1)
                cellsTR(iStartRowTR, iStartColumnTR).PutValue(startdateTR.Day.ToString())
                startdateTR = startdateTR.AddDays(1)
                iStartColumnTR = iStartColumnTR + 1
            End While
            iDelTR = iStartColumnTR
            totalColumnTR = iStartColumnTR - 1
            iStartRowTR = 5
            For irow = 0 To dDataDetailPeriod.Rows.Count - 1
                cellsTR.InsertRows(iStartRowTR + 1, 1)
                For index = 0 To totalColumnTR
                    If dDataDetailPeriod.Rows(irow)(index) IsNot DBNull.Value Then
                        cellsTR(iStartRowTR, index).PutValue(dDataDetailPeriod.Rows(irow)(index))
                    End If
                Next
                iStartRowTR = iStartRowTR + 1
            Next
            If iDelTR < 30 Then
                cellsTR.DeleteColumns(iDelTR, 3, True)
            Else
                cellsTR.DeleteColumns(iDelTR, 2, True)
            End If
            Dim index1 = 5
            For i = 0 To dDataDetailPeriod.Rows.Count - 1
                If dDataDetailPeriod.Rows(i)("NDAY_RICE") IsNot DBNull.Value Then
                    cellsTR(index1, iDelTR).PutValue(dDataDetailPeriod.Rows(i)("NDAY_RICE"))
                    cellsTR(index1, iDelTR + 1).PutValue(dDataDetailPeriod.Rows(i)("PRICE"))
                End If
                cellsTR(index1, iDelTR + 2).Formula = String.Format("={0}*{1}", cellsTR(index1, iDelTR).Name, cellsTR(index1, iDelTR + 1).Name)
                cellsTR(index1, iDelTR + 6).Formula = String.Format("={0}+{1}", cellsTR(index1, iDelTR + 2).Name, cellsTR(index1, iDelTR + 5).Name)
                index1 = index1 + 1
            Next
            cellsTR("C1").PutValue(ctrlOrganization.CurrentText.ToUpper)
            cellsTR("A2").PutValue("BẢNG CHẤM CÔNG CƠM THÁNG " & dDataHeadColPeriod.Rows(0)("MONTH") & " NĂM " & cboYear.SelectedValue)
            wBook.CalculateFormula()
            wBook.Save(HttpContext.Current.Response, sStringFileName, Aspose.Cells.ContentDisposition.Attachment, New Aspose.Cells.OoxmlSaveOptions(Aspose.Cells.SaveFormat.Excel97To2003))
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                   CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Public Sub PrintReport_AT_011(ByVal dDataHeadColPeriod As DataTable,
                                  ByVal dDataDetailPeriod As DataTable,
                                  ByVal sPathTemplate As String,
                                  ByVal sStringFileName As String)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If dDataDetailPeriod.Rows.Count = 0 Then
                ShowMessage(Translate("Không có dữ liệu in báo cáo"), NotifyType.Warning)
                Exit Sub
            End If
            Dim iLastRow As Integer = 0

            Dim sFileTmp As String = Server.MapPath("~" + sPathTemplate)
            Dim wBook As New Aspose.Cells.Workbook(sFileTmp)
            '-------------------------sheet 1 chấm công ----------------------------
            Dim wSheet As Aspose.Cells.Worksheet = wBook.Worksheets.Item(0)
            Dim cells As Aspose.Cells.Cells = wSheet.Cells
            Dim style As New Aspose.Cells.Style
            style.VerticalAlignment = Aspose.Cells.TextAlignmentType.Center

            Dim iStartColumn As Integer = 10
            Dim iStartRow As Integer = 5
            Dim totalColumn As Integer = 0
            '1. INSERT FROMDATE TO ENDDATE
            Dim startdate = Date.Parse(dDataHeadColPeriod.Rows(0)("START_DATE")).AddDays(7)
            Dim enddate = Date.Parse(dDataHeadColPeriod.Rows(0)("END_DATE"))
            Dim dayStart = Date.Parse(dDataHeadColPeriod.Rows(0)("START_DATE")).Day
            Dim endStart = Date.Parse(dDataHeadColPeriod.Rows(0)("END_DATE")).Day
            While startdate <= enddate
                cells.InsertColumn(iStartColumn + 1, 1)
                cells(iStartRow, iStartColumn).PutValue(startdate.Day.ToString())
                startdate = startdate.AddDays(1)
                iStartColumn = iStartColumn + 1
            End While
            totalColumn = iStartColumn
            iStartRow = iStartRow + 1
            iStartColumn = 3
            startdate = Date.Parse(dDataHeadColPeriod.Rows(0)("START_DATE"))
            For j = 0 To 30
                If startdate <= enddate Then
                    If startdate.DayOfWeek = DayOfWeek.Sunday Then
                        cells(iStartRow, iStartColumn).PutValue("CN")
                        style.BackgroundColor = Drawing.Color.Red
                        style.VerticalAlignment = Aspose.Cells.TextAlignmentType.Center
                        style.Font.IsBold = True
                        cells(iStartRow, iStartColumn).SetStyle(style)
                    ElseIf startdate.DayOfWeek = DayOfWeek.Monday Then
                        cells(iStartRow, iStartColumn).PutValue("T2")
                    ElseIf startdate.DayOfWeek = DayOfWeek.Tuesday Then
                        cells(iStartRow, iStartColumn).PutValue("T3")
                    ElseIf startdate.DayOfWeek = DayOfWeek.Wednesday Then
                        cells(iStartRow, iStartColumn).PutValue("T4")
                    ElseIf startdate.DayOfWeek = DayOfWeek.Thursday Then
                        cells(iStartRow, iStartColumn).PutValue("T5")
                    ElseIf startdate.DayOfWeek = DayOfWeek.Friday Then
                        cells(iStartRow, iStartColumn).PutValue("T6")
                    ElseIf startdate.DayOfWeek = DayOfWeek.Saturday Then
                        cells(iStartRow, iStartColumn).PutValue("T7")
                    End If
                    startdate = startdate.AddDays(1)
                    iStartColumn = iStartColumn + 1
                End If
            Next
            style = cells(0, 4).GetStyle()
            style.Font.IsBold = False
            cells(0, 3).SetStyle(style)
            Dim colEnd = 0
            Dim irowManual As Integer = 0
            Dim l As Integer = 0
            'For index = iStartColumn To iStartColumn + dDataHeadColPeriod.Rows.Count - 1
            '    '    cells.InsertColumn(index, 1)
            '    '    If dDataHeadColManual.Rows(irowManual)("CODE_MANUAL").Equals("TOTAL_WORKING_XJ") Then
            '    '        style = cells(10, index - 1).GetStyle
            '    '        style.IsTextWrapped = True
            '    '        cells(iStartRow, index).PutValue("Tổng cộng")
            '    '        cells(iStartRow, index).SetStyle(style)
            '    '    ElseIf dDataHeadColManual.Rows(irowManual)("CODE_MANUAL").Equals("TOTAL_TS_V") Then
            '    '        style = cells(10, index - 1).GetStyle
            '    '        style.IsTextWrapped = True
            '    '        cells(iStartRow, index).PutValue("Tổng cộng")
            '    '        cells(iStartRow, index).SetStyle(style)
            '    '    ElseIf dDataHeadColManual.Rows(irowManual)("CODE_MANUAL").Equals("TOTAL_FACTOR_CONVERT") Then
            '    '        style = cells(10, index - 1).GetStyle
            '    '        style.IsTextWrapped = True
            '    '        cells(iStartRow, index).PutValue("Tổng cộng")
            '    '        cells(iStartRow, index).SetStyle(style)
            '    '    ElseIf dDataHeadColManual.Rows(irowManual)("CODE_MANUAL").Equals("WORKING_DA") Then
            '    '        style = cells(10, index - 1).GetStyle
            '    '        style.IsTextWrapped = True
            '    '        cells(iStartRow, index).PutValue("Công dự án")
            '    '        cells(iStartRow, index).SetStyle(style)
            '    '    Else
            '    '        cells(iStartRow, index).PutValue(dDataHeadColManual.Rows(irowManual)("CODE_MANUAL"))
            '    '    End If

            '    '    'If dDataHeadColManual.Rows(irowManual)("CODE_MANUAL") IsNot DBNull.Value Then
            '    '    '    If dDataHeadColManual.Rows(irowManual)("CODE_MANUAL").Equals("J") Then
            '    '    '        l = index
            '    '    '    End If
            '    '    'End If
            '    irowManual = irowManual + 1
            '    colEnd = index
            'Next
            cells.DeleteColumns(iStartColumn, 1, True)
            iStartColumn = 4
            iStartRow = 8
            Dim k = 0
            ' kiểm tra nếu tháng có 30 ngày thì xóa trong cột D31 trong datatable dDataDetailPeriod tương tự nếu 29 ngày
            If totalColumn = 33 Then
                dDataDetailPeriod.Columns.Remove("D31")
            ElseIf totalColumn = 32 Then
                dDataDetailPeriod.Columns.Remove("D31")
                dDataDetailPeriod.Columns.Remove("D30")
            End If


            For k = k To dDataDetailPeriod.Rows.Count - 1
                cells.InsertRows(iStartRow, 1)
                For index = 0 To dDataDetailPeriod.Columns.Count - 1
                    cells(iStartRow - 1, index).PutValue(dDataDetailPeriod.Rows(k)(index))
                Next
                iStartRow = iStartRow + 1
                colEnd = iStartRow
            Next
            '' đổ dữ liệu kiểu công ra cuối
            'colEnd = dDataDetailPeriod.Columns.Count + 2
            'For index = 10 To dDataHeadColManual.Rows.Count - 1
            '    cells(index + 1, colEnd).PutValue(dDataHeadColManual.Rows(index)("STT"))
            '    cells(index + 1, colEnd + 1).PutValue(dDataHeadColManual.Rows(index)("CODE_MANUAL"))
            '    cells(index + 1, colEnd + 2).PutValue(dDataHeadColManual.Rows(index)("NAME"))
            'Next

            ' tính dòng tổng cuối 
            Dim columnTotalMonth As Decimal

            Dim rowFoot As Decimal
            Dim listStr() As String = {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "AA", "AB", "AC", "AD", "AE", "AF", "AG", "AH", "AI", "AJ", "AK", "AL", "AM", "AN", "AO", "AP", "AQ", "AR", "AS", "AT", "AU", "AV", "AW", "AX", "AY", "AZ", "BA", "BB", "BC", "BD", "BE", "BF", "BG", "BH", "BI", "BJ", "BK", "BL", "BM", "BN", "BO", "BP"}

            'tính countdata trong phần tháng
            columnTotalMonth = totalColumn
            rowFoot = 8 + dDataDetailPeriod.Rows.Count
            For i = totalColumn To totalColumn + 10 - 1 '10 là số cột loại nghỉ có 10 loại
                cells(rowFoot, i).Formula = "=SUM(" & listStr(i) & "8:" & listStr(i) & rowFoot & ")"
            Next

            ' tính sum trong phần kiểu công
            'For i = 1 To dDataHeadColManual.Rows.Count
            '    cells(rowFoot, columnTotalMonth).Formula = "=SUM(" & listStr(columnTotalMonth) & "12:" & listStr(columnTotalMonth) & rowFoot & ")"
            '    columnTotalMonth = columnTotalMonth + 1
            'Next
            ' tính sum cột ghi chú
            'columnTotalComment = columnTotalMonth
            'Dim totalComment As String = "=(" & listStr(columnTotalComment - 13) & rowFoot + 1 & " + " & listStr(columnTotalComment - 9) & rowFoot + 1 & ") - SUM(D" & rowFoot + 1 & ":" & listStr(columnTotalComment - (dDataHeadColManual.Rows.Count + 1)) & rowFoot + 1 & ")"
            'cells(rowFoot, columnTotalComment).Formula = totalComment.ToString()


            ' merge các cột trong tháng
            cells.Merge(4, 3, 1, totalColumn - 3)

            ' merge các loại nghỉ
            cells.Merge(4, totalColumn, 1, 10) '10 là số cột loại nghỉ gồm 10 loại nghỉ
            cells(4, totalColumn).PutValue("Tổng hợp")
            style = cells(4, 3).GetStyle()
            style.Font.Size = 10
            cells(4, totalColumn).SetStyle(style)
            '' merge các cột cộng hưởng lương BHXH hoặc ko lương
            'cells.Merge(8, totalColumn + dDataHeadColManual.Rows.Count - 12, 2, 4)
            'cells(8, totalColumn + dDataHeadColManual.Rows.Count - 12).PutValue("Công hưởng lương BHXH hoặc không lương")
            'style = cells(8, 3).GetStyle()
            'style.Font.Size = 10
            'style.IsTextWrapped = True
            'cells(8, totalColumn + dDataHeadColManual.Rows.Count - 12).SetStyle(style)
            '' merge các cột công OT
            'cells.Merge(8, totalColumn + dDataHeadColManual.Rows.Count - 8, 2, 7)
            'cells(8, totalColumn + dDataHeadColManual.Rows.Count - 8).PutValue("Công làm ngoài giờ")
            'style = cells(8, 3).GetStyle()
            'style.Font.Size = 10
            'style.IsTextWrapped = True
            'cells(8, totalColumn + dDataHeadColManual.Rows.Count - 8).SetStyle(style)
            '' merge cột Công dự án
            'cells.Merge(8, totalColumn + dDataHeadColManual.Rows.Count - 1, 3, 1)
            'cells(8, totalColumn + dDataHeadColManual.Rows.Count - 1).PutValue("Công dự án")
            'style = cells(8, 3).GetStyle()
            'style.Font.Size = 10
            'style.IsTextWrapped = True
            'cells(8, totalColumn + dDataHeadColManual.Rows.Count - 1).SetStyle(style)

            cells("C1").PutValue(ctrlOrganization.CurrentText.ToUpper)
            cells.Merge(2, 0, 1, totalColumn + 10) '10 là số cột loại nghỉ gồm 10 loại nghỉ
            cells("A3").PutValue("BẢNG TỔNG HỢP NGHỈ THÁNG " & dDataHeadColPeriod.Rows(0)("MONTH") & " NĂM " & cboYear.SelectedValue)
            style = cells(0, 2).GetStyle()
            cells("A3").SetStyle(style)
            wBook.CalculateFormula()
            wBook.Save(HttpContext.Current.Response, sStringFileName, Aspose.Cells.ContentDisposition.Attachment, New Aspose.Cells.OoxmlSaveOptions(Aspose.Cells.SaveFormat.Excel97To2003))
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                   CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Public Sub PrintReport_AT_010(ByVal dDataHeadColPeriod As DataTable,
                                  ByVal dDataDetailPeriod As DataTable,
                                  ByVal sPathTemplate As String,
                                  ByVal sStringFileName As String)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If dDataDetailPeriod.Rows.Count = 0 Then
                ShowMessage(Translate("Không có dữ liệu in báo cáo"), NotifyType.Warning)
                Exit Sub
            End If
            Dim iLastRow As Integer = 0

            Dim sFileTmp As String = Server.MapPath("~" + sPathTemplate)
            Dim wBook As New Aspose.Cells.Workbook(sFileTmp)
            '-------------------------sheet 1 chấm công ----------------------------
            Dim wSheet As Aspose.Cells.Worksheet = wBook.Worksheets.Item(0)
            Dim cells As Aspose.Cells.Cells = wSheet.Cells
            Dim style As New Aspose.Cells.Style
            style.VerticalAlignment = Aspose.Cells.TextAlignmentType.Center

            Dim iStartColumn As Integer = 10
            Dim iStartRow As Integer = 5
            Dim totalColumn As Integer = 0
            '1. INSERT FROMDATE TO ENDDATE
            Dim startdate = Date.Parse(dDataHeadColPeriod.Rows(0)("START_DATE")).AddDays(7)
            Dim enddate = Date.Parse(dDataHeadColPeriod.Rows(0)("END_DATE"))
            Dim dayStart = Date.Parse(dDataHeadColPeriod.Rows(0)("START_DATE")).Day
            Dim endStart = Date.Parse(dDataHeadColPeriod.Rows(0)("END_DATE")).Day
            While startdate <= enddate
                cells.InsertColumn(iStartColumn + 1, 1)
                cells(iStartRow, iStartColumn).PutValue(startdate.Day.ToString())
                startdate = startdate.AddDays(1)
                iStartColumn = iStartColumn + 1
            End While
            totalColumn = iStartColumn
            iStartRow = iStartRow + 1
            iStartColumn = 3
            startdate = Date.Parse(dDataHeadColPeriod.Rows(0)("START_DATE"))
            For j = 0 To 30
                If startdate <= enddate Then
                    If startdate.DayOfWeek = DayOfWeek.Sunday Then
                        cells(iStartRow, iStartColumn).PutValue("CN")
                        style.BackgroundColor = Drawing.Color.Red
                        style.VerticalAlignment = Aspose.Cells.TextAlignmentType.Center
                        style.Font.IsBold = True
                        cells(iStartRow, iStartColumn).SetStyle(style)
                    ElseIf startdate.DayOfWeek = DayOfWeek.Monday Then
                        cells(iStartRow, iStartColumn).PutValue("T2")
                    ElseIf startdate.DayOfWeek = DayOfWeek.Tuesday Then
                        cells(iStartRow, iStartColumn).PutValue("T3")
                    ElseIf startdate.DayOfWeek = DayOfWeek.Wednesday Then
                        cells(iStartRow, iStartColumn).PutValue("T4")
                    ElseIf startdate.DayOfWeek = DayOfWeek.Thursday Then
                        cells(iStartRow, iStartColumn).PutValue("T5")
                    ElseIf startdate.DayOfWeek = DayOfWeek.Friday Then
                        cells(iStartRow, iStartColumn).PutValue("T6")
                    ElseIf startdate.DayOfWeek = DayOfWeek.Saturday Then
                        cells(iStartRow, iStartColumn).PutValue("T7")
                    End If
                    startdate = startdate.AddDays(1)
                    iStartColumn = iStartColumn + 1
                End If
            Next
            style = cells(0, 4).GetStyle()
            style.Font.IsBold = False
            cells(0, 3).SetStyle(style)
            Dim colEnd = 0
            Dim irowManual As Integer = 0
            Dim l As Integer = 0

            cells.DeleteColumns(iStartColumn, 1, True)
            iStartColumn = 4
            iStartRow = 8
            Dim k = 0
            ' kiểm tra nếu tháng có 30 ngày thì xóa trong cột D31 trong datatable dDataDetailPeriod tương tự nếu 29 ngày
            If totalColumn = 33 Then
                dDataDetailPeriod.Columns.Remove("D31")
            ElseIf totalColumn = 32 Then
                dDataDetailPeriod.Columns.Remove("D31")
                dDataDetailPeriod.Columns.Remove("D30")
            End If


            For k = k To dDataDetailPeriod.Rows.Count - 1
                cells.InsertRows(iStartRow, 1)
                For index = 0 To dDataDetailPeriod.Columns.Count - 1
                    cells(iStartRow - 1, index).PutValue(dDataDetailPeriod.Rows(k)(index))
                Next
                iStartRow = iStartRow + 1
                colEnd = iStartRow
            Next
            Dim columnTotalMonth As Decimal

            Dim rowFoot As Decimal
            Dim listStr() As String = {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "AA", "AB", "AC", "AD", "AE", "AF", "AG", "AH", "AI", "AJ", "AK", "AL", "AM", "AN", "AO", "AP", "AQ", "AR", "AS", "AT", "AU", "AV", "AW", "AX", "AY", "AZ", "BA", "BB", "BC", "BD", "BE", "BF", "BG", "BH", "BI", "BJ", "BK", "BL", "BM", "BN", "BO", "BP"}

            'tính countdata trong phần tháng
            columnTotalMonth = totalColumn
            rowFoot = 8 + dDataDetailPeriod.Rows.Count
            For i = 3 To totalColumn - 1 '3 là số cột bắt đầu tính SUM
                cells(rowFoot, i).Formula = "=SUM(" & listStr(i) & "8:" & listStr(i) & rowFoot & ")"
            Next

            ' merge các cột trong tháng
            cells.Merge(4, 3, 1, totalColumn - 3)

            cells("C1").PutValue(ctrlOrganization.CurrentText.ToUpper)
            cells.Merge(2, 0, 1, totalColumn) '10 là số cột loại nghỉ gồm 10 loại nghỉ
            cells("A3").PutValue("BẢNG TỔNG HỢP NHÂN VIÊN ĐI LÀM THÁNG " & dDataHeadColPeriod.Rows(0)("MONTH") & " NĂM " & cboYear.SelectedValue)
            style = cells(0, 2).GetStyle()
            cells("A3").SetStyle(style)
            wBook.CalculateFormula()
            wBook.Save(HttpContext.Current.Response, sStringFileName, Aspose.Cells.ContentDisposition.Attachment, New Aspose.Cells.OoxmlSaveOptions(Aspose.Cells.SaveFormat.Excel97To2003))
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                   CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 17/08/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ham xu ly load du lieu vao file excel cho report 
    ''' </summary>
    ''' <param name="dDataDetailPeriod"></param>
    ''' <param name="sPathTemplate"></param>
    ''' <param name="sStringFileName"></param>
    ''' <param name="dDataHeadColManual"></param>
    ''' <param name="dDataHeadColPeriod"></param>
    ''' <param name="dsDataDetailTimeRice"></param>
    ''' <remarks></remarks>
    Public Sub PrintReport_AT_001(ByVal dDataHeadColPeriod As DataTable,
                                  ByVal dDataHeadColManual As DataTable,
                                  ByVal dDataDetailPeriod As DataTable,
                                  ByVal dsDataDetailTimeRice As DataSet,
                                  ByVal sPathTemplate As String,
                                  ByVal sStringFileName As String)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If dDataDetailPeriod.Rows.Count = 0 Then
                ShowMessage(Translate("Không có dữ liệu in báo cáo"), NotifyType.Warning)
                Exit Sub
            End If
            Dim iLastRow As Integer = 0

            Dim sFileTmp As String = Server.MapPath("~" + sPathTemplate)
            Dim wBook As New Aspose.Cells.Workbook(sFileTmp)
            '-------------------------sheet 1 chấm công ----------------------------
            Dim wSheet As Aspose.Cells.Worksheet = wBook.Worksheets.Item(0)
            Dim cells As Aspose.Cells.Cells = wSheet.Cells
            Dim style As New Aspose.Cells.Style
            style.VerticalAlignment = Aspose.Cells.TextAlignmentType.Center

            Dim iStartColumn As Integer = 10
            Dim iStartRow As Integer = 9
            Dim totalColumn As Integer = 0
            '1. INSERT FROMDATE TO ENDDATE
            Dim startdate = Date.Parse(dDataHeadColPeriod.Rows(0)("START_DATE")).AddDays(7)
            Dim enddate = Date.Parse(dDataHeadColPeriod.Rows(0)("END_DATE"))
            Dim dayStart = Date.Parse(dDataHeadColPeriod.Rows(0)("START_DATE")).Day
            Dim endStart = Date.Parse(dDataHeadColPeriod.Rows(0)("END_DATE")).Day
            While startdate <= enddate
                cells.InsertColumn(iStartColumn + 1, 1)
                cells(iStartRow, iStartColumn).PutValue(startdate.Day.ToString())
                startdate = startdate.AddDays(1)
                iStartColumn = iStartColumn + 1
            End While
            totalColumn = iStartColumn
            iStartRow = iStartRow + 1
            iStartColumn = 3
            startdate = Date.Parse(dDataHeadColPeriod.Rows(0)("START_DATE"))
            For j = 0 To 30
                If startdate <= enddate Then
                    If startdate.DayOfWeek = DayOfWeek.Sunday Then
                        cells(iStartRow, iStartColumn).PutValue("CN")
                        style.BackgroundColor = Drawing.Color.Red
                        style.VerticalAlignment = Aspose.Cells.TextAlignmentType.Center
                        style.Font.IsBold = True
                        cells(iStartRow, iStartColumn).SetStyle(style)
                    ElseIf startdate.DayOfWeek = DayOfWeek.Monday Then
                        cells(iStartRow, iStartColumn).PutValue("T2")
                    ElseIf startdate.DayOfWeek = DayOfWeek.Tuesday Then
                        cells(iStartRow, iStartColumn).PutValue("T3")
                    ElseIf startdate.DayOfWeek = DayOfWeek.Wednesday Then
                        cells(iStartRow, iStartColumn).PutValue("T4")
                    ElseIf startdate.DayOfWeek = DayOfWeek.Thursday Then
                        cells(iStartRow, iStartColumn).PutValue("T5")
                    ElseIf startdate.DayOfWeek = DayOfWeek.Friday Then
                        cells(iStartRow, iStartColumn).PutValue("T6")
                    ElseIf startdate.DayOfWeek = DayOfWeek.Saturday Then
                        cells(iStartRow, iStartColumn).PutValue("T7")
                    End If
                    startdate = startdate.AddDays(1)
                    iStartColumn = iStartColumn + 1
                End If
            Next
            style = cells(0, 4).GetStyle()
            style.Font.IsBold = False
            cells(0, 3).SetStyle(style)
            Dim colEnd = 0
            Dim irowManual As Integer = 0
            Dim l As Integer = 0
            For index = iStartColumn To iStartColumn + dDataHeadColManual.Rows.Count - 1
                cells.InsertColumn(index, 1)
                If dDataHeadColManual.Rows(irowManual)("CODE_MANUAL").Equals("TOTAL_WORKING_XJ") Then
                    style = cells(10, index - 1).GetStyle
                    style.IsTextWrapped = True
                    cells(iStartRow, index).PutValue("Tổng cộng")
                    cells(iStartRow, index).SetStyle(style)
                ElseIf dDataHeadColManual.Rows(irowManual)("CODE_MANUAL").Equals("TOTAL_TS_V") Then
                    style = cells(10, index - 1).GetStyle
                    style.IsTextWrapped = True
                    cells(iStartRow, index).PutValue("Tổng cộng")
                    cells(iStartRow, index).SetStyle(style)
                ElseIf dDataHeadColManual.Rows(irowManual)("CODE_MANUAL").Equals("TOTAL_FACTOR_CONVERT") Then
                    style = cells(10, index - 1).GetStyle
                    style.IsTextWrapped = True
                    cells(iStartRow, index).PutValue("Tổng cộng")
                    cells(iStartRow, index).SetStyle(style)
                ElseIf dDataHeadColManual.Rows(irowManual)("CODE_MANUAL").Equals("WORKING_DA") Then
                    style = cells(10, index - 1).GetStyle
                    style.IsTextWrapped = True
                    cells(iStartRow, index).PutValue("Công dự án")
                    cells(iStartRow, index).SetStyle(style)
                Else
                    cells(iStartRow, index).PutValue(dDataHeadColManual.Rows(irowManual)("CODE_MANUAL"))
                End If

                'If dDataHeadColManual.Rows(irowManual)("CODE_MANUAL") IsNot DBNull.Value Then
                '    If dDataHeadColManual.Rows(irowManual)("CODE_MANUAL").Equals("J") Then
                '        l = index
                '    End If
                'End If
                irowManual = irowManual + 1
                colEnd = index
            Next
            cells.DeleteColumns(colEnd + 1, 1, True)
            iStartColumn = 4
            iStartRow = 12
            Dim k = 0
            ' kiểm tra nếu tháng có 30 ngày thì xóa trong cột D31 trong datatable dDataDetailPeriod tương tự nếu 29 ngày
            If totalColumn = 33 Then
                dDataDetailPeriod.Columns.Remove("D31")
            ElseIf totalColumn = 32 Then
                dDataDetailPeriod.Columns.Remove("D31")
                dDataDetailPeriod.Columns.Remove("D30")
            End If


            For k = k To dDataDetailPeriod.Rows.Count - 1
                cells.InsertRows(iStartRow, 1)
                For index = 0 To dDataDetailPeriod.Columns.Count - 1
                    cells(iStartRow - 1, index).PutValue(dDataDetailPeriod.Rows(k)(index))
                Next
                iStartRow = iStartRow + 1
                colEnd = iStartRow
            Next
            '' đổ dữ liệu kiểu công ra cuối
            'colEnd = dDataDetailPeriod.Columns.Count + 2
            'For index = 10 To dDataHeadColManual.Rows.Count - 1
            '    cells(index + 1, colEnd).PutValue(dDataHeadColManual.Rows(index)("STT"))
            '    cells(index + 1, colEnd + 1).PutValue(dDataHeadColManual.Rows(index)("CODE_MANUAL"))
            '    cells(index + 1, colEnd + 2).PutValue(dDataHeadColManual.Rows(index)("NAME"))
            'Next

            ' tính dòng tổng cuối 
            Dim columnTotalMonth As Decimal

            Dim columnTotalComment As Decimal
            Dim rowFoot As Decimal
            Dim listStr() As String = {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "AA", "AB", "AC", "AD", "AE", "AF", "AG", "AH", "AI", "AJ", "AK", "AL", "AM", "AN", "AO", "AP", "AQ", "AR", "AS", "AT", "AU", "AV", "AW", "AX", "AY", "AZ", "BA", "BB", "BC", "BD", "BE", "BF", "BG", "BH", "BI", "BJ", "BK", "BL", "BM", "BN", "BO", "BP"}

            ' tính countdata trong phần tháng
            columnTotalMonth = totalColumn
            rowFoot = 12 + dDataDetailPeriod.Rows.Count
            For i = 3 To columnTotalMonth - 1
                cells(rowFoot, i).Formula = "=COUNTA(" & listStr(i) & "12:" & listStr(i) & rowFoot & ") - COUNTIF(" & listStr(i) & "12:" & listStr(i) & rowFoot & ", ""RD"") "
            Next

            ' tính sum trong phần kiểu công
            For i = 1 To dDataHeadColManual.Rows.Count
                cells(rowFoot, columnTotalMonth).Formula = "=SUM(" & listStr(columnTotalMonth) & "12:" & listStr(columnTotalMonth) & rowFoot & ")"
                columnTotalMonth = columnTotalMonth + 1
            Next
            ' tính sum cột ghi chú
            columnTotalComment = columnTotalMonth
            Dim totalComment As String = "=(" & listStr(columnTotalComment - 13) & rowFoot + 1 & " + " & listStr(columnTotalComment - 9) & rowFoot + 1 & ") - SUM(D" & rowFoot + 1 & ":" & listStr(columnTotalComment - (dDataHeadColManual.Rows.Count + 1)) & rowFoot + 1 & ")"
            cells(rowFoot, columnTotalComment).Formula = totalComment.ToString()


            ' merge các cột trong tháng
            cells.Merge(8, 3, 1, totalColumn - 3)

            ' merge các cộng hưởng lương hoặc trợ cấp
            cells.Merge(8, totalColumn, 2, dDataHeadColManual.Rows.Count - 12)
            cells(8, totalColumn).PutValue("Công hưởng lương hoặc đơn vị trợ cấp (Đvt: Ngày)")
            style = cells(8, 3).GetStyle()
            style.Font.Size = 10
            cells(8, totalColumn).SetStyle(style)
            ' merge các cột cộng hưởng lương BHXH hoặc ko lương
            cells.Merge(8, totalColumn + dDataHeadColManual.Rows.Count - 12, 2, 4)
            cells(8, totalColumn + dDataHeadColManual.Rows.Count - 12).PutValue("Công hưởng lương BHXH hoặc không lương")
            style = cells(8, 3).GetStyle()
            style.Font.Size = 10
            style.IsTextWrapped = True
            cells(8, totalColumn + dDataHeadColManual.Rows.Count - 12).SetStyle(style)
            ' merge các cột công OT
            cells.Merge(8, totalColumn + dDataHeadColManual.Rows.Count - 8, 2, 7)
            cells(8, totalColumn + dDataHeadColManual.Rows.Count - 8).PutValue("Công làm ngoài giờ")
            style = cells(8, 3).GetStyle()
            style.Font.Size = 10
            style.IsTextWrapped = True
            cells(8, totalColumn + dDataHeadColManual.Rows.Count - 8).SetStyle(style)
            ' merge cột Công dự án
            cells.Merge(8, totalColumn + dDataHeadColManual.Rows.Count - 1, 3, 1)
            cells(8, totalColumn + dDataHeadColManual.Rows.Count - 1).PutValue("Công dự án")
            style = cells(8, 3).GetStyle()
            style.Font.Size = 10
            style.IsTextWrapped = True
            cells(8, totalColumn + dDataHeadColManual.Rows.Count - 1).SetStyle(style)

            cells("C1").PutValue(ctrlOrganization.CurrentText.ToUpper)
            cells("C3").PutValue("BẢNG CHẤM CÔNG THÁNG " & dDataHeadColPeriod.Rows(0)("MONTH") & " NĂM " & cboYear.SelectedValue)


            '------------------------------- sheet 2 công cơm -------------------------------------
            Dim wSheetTR As Aspose.Cells.Worksheet = wBook.Worksheets.Item(1)
            Dim cellsTR As Aspose.Cells.Cells = wSheetTR.Cells
            Dim styleTR As New Aspose.Cells.Style
            styleTR.VerticalAlignment = Aspose.Cells.TextAlignmentType.Center

            Dim iStartColumnTR As Integer = 5
            Dim iStartRowTR As Integer = 4
            Dim totalColumnTR As Integer = 0
            Dim iDelTR As Integer = 0
            '1. INSERT FROMDATE TO ENDDATE
            Dim startdateTR = Date.Parse(dsDataDetailTimeRice.Tables(0).Rows(0)("START_DATE"))
            Dim enddateTR = Date.Parse(dsDataDetailTimeRice.Tables(0).Rows(0)("END_DATE"))
            While startdateTR <= enddateTR
                cellsTR.InsertColumn(iStartColumnTR + 1, 1)
                cellsTR(iStartRowTR, iStartColumnTR).PutValue(startdateTR.Day.ToString())
                startdateTR = startdateTR.AddDays(1)
                iStartColumnTR = iStartColumnTR + 1
            End While
            iDelTR = iStartColumnTR
            totalColumnTR = iStartColumnTR - 1
            iStartRowTR = 5
            For irow = 0 To dsDataDetailTimeRice.Tables(1).Rows.Count - 1
                cellsTR.InsertRows(iStartRowTR + 1, 1)
                For index = 0 To totalColumnTR
                    If dsDataDetailTimeRice.Tables(1).Rows(irow)(index) IsNot DBNull.Value Then
                        cellsTR(iStartRowTR, index).PutValue(dsDataDetailTimeRice.Tables(1).Rows(irow)(index))
                    End If
                Next
                iStartRowTR = iStartRowTR + 1
            Next
            If iDelTR < 30 Then
                cellsTR.DeleteColumns(iDelTR, 3, True)
            Else
                cellsTR.DeleteColumns(iDelTR, 2, True)
            End If
            Dim index1 = 5
            For i = 0 To dsDataDetailTimeRice.Tables(1).Rows.Count - 1
                If dsDataDetailTimeRice.Tables(1).Rows(i)("NDAY_RICE") IsNot DBNull.Value Then
                    cellsTR(index1, iDelTR).PutValue(dsDataDetailTimeRice.Tables(1).Rows(i)("NDAY_RICE"))
                    cellsTR(index1, iDelTR + 1).PutValue(dsDataDetailTimeRice.Tables(1).Rows(i)("PRICE"))
                End If
                cellsTR(index1, iDelTR + 2).Formula = String.Format("={0}*{1}", cellsTR(index1, iDelTR).Name, cellsTR(index1, iDelTR + 1).Name)
                cellsTR(index1, iDelTR + 6).Formula = String.Format("={0}+{1}", cellsTR(index1, iDelTR + 2).Name, cellsTR(index1, iDelTR + 5).Name)
                index1 = index1 + 1
            Next
            cellsTR("C1").PutValue(ctrlOrganization.CurrentText.ToUpper)
            cellsTR("A2").PutValue("BẢNG CHẤM CÔNG CƠM THÁNG " & dsDataDetailTimeRice.Tables(0).Rows(0)("MONTH") & " NĂM " & cboYear.SelectedValue)
            wBook.CalculateFormula()
            wBook.Save(HttpContext.Current.Response, sStringFileName, Aspose.Cells.ContentDisposition.Attachment, New Aspose.Cells.OoxmlSaveOptions(Aspose.Cells.SaveFormat.Excel97To2003))
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                   CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

End Class