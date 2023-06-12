Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Insurance.InsuranceBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlINS_ReportList
    Inherits Common.CommonView

    ''' <creator>HongDX</creator>
    ''' <lastupdate>21/06/2017</lastupdate>
    ''' <summary>Write Log</summary>
    ''' <remarks>_mylog, _pathLog, _classPath</remarks>
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Insurance\Modules\Report" + Me.GetType().Name.ToString()

#Region "Property"

    ''' <summary>
    ''' ValueCodeSelected
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ValueCodeSelected As String
        Get
            Return ViewState(Me.ID & "_ValueCodeSelected")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_ValueCodeSelected") = value
        End Set
    End Property

#End Region

#Region "Page"

    ''' <lastupdate>07/09/2017</lastupdate>
    ''' <summary>
    ''' Ke thua ViewLoad tu CommnView, la ham load du lieu, control states cua usercontrol
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            ctrlOrganization.AutoPostBack = False
            ctrlOrganization.LoadDataAfterLoaded = True
            ctrlOrganization.OrganizationType = OrganizationType.OrganizationLocation
            ctrlOrganization.CheckBoxes = TreeNodeTypes.None

            Refresh()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>07/09/2017</lastupdate>
    ''' <summary>
    ''' Ham duoc ke thua tu commonView va goi phuong thuc InitControl
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            InitControl()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>07/09/2017</lastupdate>
    ''' <summary>Load cac control, menubar</summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMainToolBar

            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Export)

            CType(MainToolBar.Items(0), RadToolBarButton).CausesValidation = True
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>07/09/2017</lastupdate>
    ''' <summary>
    ''' Load lai du lieu tren grid khi thuc hien update, insert, delete
    ''' (neu co tham so truyen vao thi dua ra messager)
    ''' </summary>
    ''' <param name="Message">tham so truyen vao de phan biet trang thai Insert, Update, Cancel</param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New InsuranceBusinessClient

        Try
            ClearControlValue(rdFromDate, rdToDate, cboMonth, cboYear)

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>07/09/2017</lastupdate>
    ''' <summary>
    ''' Ham xu ly load du lieu tu DB len grid va filter
    ''' </summary>
    ''' <remarks></remarks>
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False)
        CreateDataFilter = Nothing
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New InsuranceRepository
        Dim _filter As New Se_ReportDTO
        Dim listReport As List(Of Se_ReportDTO)
        Dim MaximumRows As Integer

        Try
            SetValueObjectByRadGrid(rgDanhMuc, _filter)
            Dim Sorts As String = rgDanhMuc.MasterTableView.SortExpressions.GetSortString()
            _filter.MODULE_ID = 9

            If Sorts IsNot Nothing Then
                listReport = rep.GetReportById(_filter, rgDanhMuc.CurrentPageIndex, rgDanhMuc.PageSize, MaximumRows, Sorts)
            Else
                listReport = rep.GetReportById(_filter, rgDanhMuc.CurrentPageIndex, rgDanhMuc.PageSize, MaximumRows)
            End If

            rgDanhMuc.VirtualItemCount = MaximumRows
            rgDanhMuc.DataSource = listReport

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Function

    ''' <lastupdate>07/09/2017</lastupdate>
    ''' <summary>
    ''' Get data va bind lên form, tren grid
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            GetDataCombo()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"

    ''' <lastupdate>07/09/2017</lastupdate>
    ''' <summary>
    ''' Ham xu ly cac event click menu tren menu toolbar: them moi, sua, xoa, xuat excel, huy, Ap dung, Ngung Ap dung
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim objShift As New INS_WHEREHEALTHDTO
        Dim rep As New InsuranceRepository

        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_EXPORT
                    If rgDanhMuc.SelectedItems.Count = 0 Then
                        ShowMessage(Translate("Bạn phải chọn báo cáo"), Utilities.NotifyType.Warning)
                        Exit Sub
                    End If

                    If Not Page.IsValid Then
                        ExcuteScript("resize", "setDefaultSize()")
                        Exit Sub
                    End If

                    Dim item As GridDataItem = rgDanhMuc.SelectedItems(0)
                    Dim code = item.GetDataKeyValue("CODE")

                    Select Case code
                        Case "INS_001" ' báo cáo biến động bảo hiểm D02_TS
                            If cboMonth.SelectedValue = "" And cboYear.SelectedValue = "" Then
                                ShowMessage(Translate("Bạn chưa nhập điều kiện xuất báo cáo. "), Utilities.NotifyType.Warning)
                                Exit Sub
                            End If

                            printD02_TS()

                        Case "INS_002" ' báo cáo chế độ bảo hiểm C70_HD
                            If cboMonth.SelectedValue = "" And cboYear.SelectedValue = "" Then
                                ShowMessage(Translate("Bạn chưa nhập điều kiện xuất báo cáo. "), Utilities.NotifyType.Warning)
                                Exit Sub
                            End If

                            printC70_HD()

                        Case "INS_003" ' báo cáo tổng quỹ lương đóng bảo hiểm
                            If cboMonth.SelectedValue = "" And cboYear.SelectedValue = "" Then
                                ShowMessage(Translate("Bạn chưa nhập điều kiện xuất báo cáo. "), Utilities.NotifyType.Warning)
                                Exit Sub
                            End If

                            printQuyLuongBH()

                        Case "INS_004" ' báo cáo danh sách đóng bảo hiểm sun care
                            printDsBHSunCare()

                        Case "INS_005" ' báo cáo danh sách điểu chỉnh bảo hiểm sun care
                            printDsDieuChinhSunCare()

                        Case "INS_006" ' báo cáo danh sách tổng hợp chi phí bảo hiểm sun care
                            printChiPhiSunCare()
                    End Select
            End Select

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>07/09/2017</lastupdate>
    ''' <summary>
    ''' Event Yes, No tren popup message khi click nut: xoa, ap dung, ngung ap dung
    ''' va Set trang thai cho form va update trang thai control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Dim strError As String = ""

                If strError = "" Then
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                Else
                    ShowMessage(Translate("Nhân viên đã có hợp đồng " & strError.Substring(1, strError.Length - 1) & " không thực hiện được thao tác này."), Utilities.NotifyType.Error)
                End If

                Refresh()
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>07/09/2017</lastupdate>
    ''' <summary>
    ''' Load data len grid
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgDanhMuc.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            CreateDataFilter()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>07/09/2017</lastupdate>
    ''' <summary>
    ''' Event selected row in RadGrid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgDanhMuc_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgDanhMuc.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim dID As Decimal

        Try
            For Each item As GridDataItem In rgDanhMuc.MasterTableView.Items
                If item.Selected = True Then
                    ValueCodeSelected = item("CODE").Text
                    dID = Convert.ToDecimal(item("ID").Text)
                End If
            Next

            Utilities.SelectedItemDataGridByKey(rgDanhMuc, dID)
            SetHeader(ValueCodeSelected)

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Custom"

    ''' <lastupdate>07/09/2017</lastupdate>
    ''' <summary>
    ''' Get data và bind vao combobox
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetDataCombo()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New InsuranceRepository
        Dim table As New DataTable
        Dim row As DataRow
        Dim tableMonth As New DataTable
        Dim rowMonth As DataRow

        Try
            Table.Columns.Add("YEAR", GetType(Integer))
            Table.Columns.Add("ID", GetType(Integer))

            For index = 2014 To Date.Now.Year + 2
                row = Table.NewRow
                row("ID") = index
                row("YEAR") = index
                Table.Rows.Add(row)
            Next

            FillRadCombobox(cboYear, Table, "YEAR", "ID")
            cboYear.SelectedValue = Date.Now.Year

            tableMonth.Columns.Add("MONTH", GetType(String))
            tableMonth.Columns.Add("ID", GetType(String))

            For index = 0 To 12
                rowMonth = tableMonth.NewRow

                If index = 0 Then
                    'rowMonth("ID") = 0
                    'rowMonth("MONTH") = ""
                    'tableMonth.Rows.Add(rowMonth)
                    Continue For
                Else
                    rowMonth("ID") = index
                    rowMonth("MONTH") = index
                    tableMonth.Rows.Add(rowMonth)
                End If
            Next

            FillRadCombobox(cboMonth, tableMonth, "MONTH", "ID")
            cboMonth.SelectedValue = Date.Now.Month

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>07/09/2017</lastupdate>
    ''' <summary>
    ''' Thiết lập trạng thái Header
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetHeader(ByVal valueCode As String)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Select Case valueCode
                Case "INS_001", "INS_002", "INS_003"
                    lblFromDate.Visible = False
                    lblToDate.Visible = False
                    lblYear.Visible = True
                    lblMonth.Visible = True

                    rdFromDate.Visible = False
                    rdToDate.Visible = False
                    cboMonth.Visible = True
                    cboYear.Visible = True

                    reqFromdate.Enabled = False
                    reqToDate.Enabled = False
                    compareValDate.Enabled = False

                    customValMonth.Enabled = True
                    customValYear.Enabled = True

                Case "INS_004", "INS_005", "INS_006"
                    lblFromDate.Visible = True
                    lblToDate.Visible = True
                    lblYear.Visible = False
                    lblMonth.Visible = False

                    rdFromDate.Visible = True
                    rdToDate.Visible = True
                    cboMonth.Visible = False
                    cboYear.Visible = False

                    reqFromdate.Enabled = True
                    reqToDate.Enabled = True
                    compareValDate.Enabled = True

                    customValMonth.Enabled = False
                    customValYear.Enabled = False

                Case Else
                    lblFromDate.Visible = False
                    lblToDate.Visible = False
                    lblYear.Visible = True
                    lblMonth.Visible = True

                    rdFromDate.Visible = True
                    rdToDate.Visible = True
                    cboMonth.Visible = False
                    cboYear.Visible = False

                    reqFromdate.Enabled = False
                    reqToDate.Enabled = False
                    compareValDate.Enabled = False

                    customValMonth.Enabled = True
                    customValYear.Enabled = True
            End Select

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>07/09/2017</lastupdate>
    ''' <summary>
    ''' Set data and xuất ra template báo cáo DANH SACH THAM GIA BẢO HIỂM
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub printD02_TS()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim _error As String = ""
        Dim dsData As New DataSet
        Dim dtDataTang As New DataTable
        Dim dtDataGiam As New DataTable
        Dim dtOrg As New DataTable
        Dim rep As New Insurance.InsuranceRepository

        Try
            Dim item As GridDataItem = rgDanhMuc.SelectedItems(0)
            dtDataTang = rep.GetD02Tang(Utilities.ObjToDecima(cboMonth.SelectedValue), Utilities.ObjToDecima(cboYear.SelectedValue), Common.Common.GetUsername, Utilities.ObjToInt(ctrlOrganization.CurrentValue))

            Dim ASC() As String = {"A", "B", "C", "D", "E", "F", "G", "H", "I", "K", "L", "N", "M"}
            Dim LM() As String = {"I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX", "X", "XI", "XII", "XIII", "XIV", "XV", "XVI", "XVII", "XVIII", "XIX", "XX", "XXI", "XXII", "XXIII", "XXIV", "XXV", "XXVI", "XXVII", "XXVIII"}
            Dim index_ASC As Integer = 0
            Dim index_LM As Integer = 0
            Dim index_NV As Integer = 1

            For index As Integer = 0 To dtDataTang.Rows.Count - 1
                If dtDataTang.Rows(index)("STT").ToString() = "C1" Then
                    dtDataTang.Rows(index)("STT") = ASC(index_ASC)
                    index_ASC = index_ASC + 1
                    index_LM = 0
                End If

                If dtDataTang.Rows(index)("STT").ToString() = "C2" Then
                    dtDataTang.Rows(index)("STT") = LM(index_LM)
                    index_LM = index_LM + 1
                    index_NV = 1
                End If

                If dtDataTang.Rows(index)("STT").ToString() = "C3" Then
                    dtDataTang.Rows(index)("STT") = index_NV
                    index_NV = index_NV + 1
                End If
            Next

            dsData.Tables.Add(dtDataTang)
            dsData.Tables(0).TableName = "Tab1"

            dtDataGiam = rep.GetD02Giam(Utilities.ObjToDecima(cboMonth.SelectedValue), Utilities.ObjToDecima(cboYear.SelectedValue), Common.Common.GetUsername, Utilities.ObjToInt(ctrlOrganization.CurrentValue))

            For index As Integer = 0 To dtDataGiam.Rows.Count - 1
                If dtDataGiam.Rows(index)("STT").ToString() = "C1" Then
                    dtDataGiam.Rows(index)("STT") = ASC(index_ASC)
                    index_ASC = index_ASC + 1
                    index_LM = 0
                End If

                If dtDataGiam.Rows(index)("STT").ToString() = "C2" Then
                    dtDataGiam.Rows(index)("STT") = LM(index_LM)
                    index_LM = index_LM + 1
                    index_NV = 1
                End If

                If dtDataGiam.Rows(index)("STT").ToString() = "C3" Then
                    dtDataGiam.Rows(index)("STT") = index_NV
                    index_NV = index_NV + 1
                End If
            Next

            dsData.Tables.Add(dtDataGiam)
            dsData.Tables(1).TableName = "tab2"

            dtOrg = rep.GetOrgInfoMONTH(Utilities.ObjToDecima(cboMonth.SelectedValue), Utilities.ObjToDecima(cboYear.SelectedValue), Utilities.ObjToInt(ctrlOrganization.CurrentValue))
            dsData.Tables.Add(dtOrg)
            dsData.Tables(2).TableName = "Org"

            Using xls As New ExcelCommon
                xls.ExportExcelTemplate(
                    Server.MapPath("~/ReportTemplates//" & "Insurance" & "/" & "Report/INS_001.xlsx"),
                    item.GetDataKeyValue("NAME"), dsData, Nothing, Response, _error, ExcelCommon.ExportType.Excel)
            End Using

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>07/09/2017</lastupdate>
    ''' <summary>
    ''' Set data and xuất ra template báo cáo DANH SACH BIẾN ĐỘNG BẢO HIỂM
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub printC70_HD()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim _error As String = ""
        Dim dsData As New DataSet
        Dim dtDataTang As New DataTable
        Dim dtDataGiam As New DataTable
        Dim dtOrg As New DataTable
        Dim rep As New Insurance.InsuranceRepository

        Try
            Dim item As GridDataItem = rgDanhMuc.SelectedItems(0)
            dtDataTang = rep.GetC70_HD(Utilities.ObjToDecima(cboMonth.SelectedValue), Utilities.ObjToDecima(cboYear.SelectedValue), Common.Common.GetUsername, Utilities.ObjToInt(ctrlOrganization.CurrentValue))

            Dim ASC() As String = {"A", "B", "C", "D", "E", "F", "G", "H", "I", "K", "L", "N", "M"}
            Dim LM() As String = {"I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX", "X", "XI", "XII", "XIII", "XIV", "XV", "XVI", "XVII", "XVIII", "XIX", "XX", "XXI", "XXII", "XXIII", "XXIV", "XXV", "XXVI", "XXVII", "XXVIII"}
            Dim index_ASC As Integer = 0
            Dim index_LM As Integer = 0
            Dim index_NV As Integer = 1

            For index As Integer = 0 To dtDataTang.Rows.Count - 1
                If dtDataTang.Rows(index)("STT").ToString() = "C1" Then
                    dtDataTang.Rows(index)("STT") = ASC(index_ASC)
                    index_ASC = index_ASC + 1
                    index_LM = 0
                End If

                If dtDataTang.Rows(index)("STT").ToString() = "C2" Then
                    dtDataTang.Rows(index)("STT") = LM(index_LM)
                    index_LM = index_LM + 1
                    index_NV = 1
                End If

                If dtDataTang.Rows(index)("STT").ToString() = "C3" Then
                    dtDataTang.Rows(index)("STT") = index_NV
                    index_NV = index_NV + 1
                End If
            Next

            dsData.Tables.Add(dtDataTang)
            dsData.Tables(0).TableName = "Tab1"

            dtOrg = rep.GetOrgInfoMONTH(Utilities.ObjToDecima(cboMonth.SelectedValue), Utilities.ObjToDecima(cboYear.SelectedValue), Utilities.ObjToInt(ctrlOrganization.CurrentValue))
            dsData.Tables.Add(dtOrg)
            dsData.Tables(1).TableName = "Org"

            Using xls As New ExcelCommon
                xls.ExportExcelTemplate(
                    Server.MapPath("~/ReportTemplates//" & "Insurance" & "/" & "Report/INS_002.xlsx"),
                    item.GetDataKeyValue("NAME"), dsData, Nothing, Response, _error, ExcelCommon.ExportType.Excel)
            End Using

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>07/09/2017</lastupdate>
    ''' <summary>
    ''' Set data and xuất ra template báo cáo TỔNG QUỸ LƯƠNG BẢO HIỂM
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub printQuyLuongBH()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim _error As String = ""
        Dim dsDataPri As New DataSet
        Dim dtData As New DataTable
        Dim DtOrgInfo As New DataTable
        Dim rep As New Insurance.InsuranceRepository

        Try
            Dim item As GridDataItem = rgDanhMuc.SelectedItems(0)
            dtData = rep.GetQuyLuongBH(Utilities.ObjToDecima(cboMonth.SelectedValue), Utilities.ObjToDecima(cboYear.SelectedValue), Common.Common.GetUsername, Utilities.ObjToInt(ctrlOrganization.CurrentValue))
            DtOrgInfo = rep.GetOrgInfoMONTH(Utilities.ObjToDecima(cboMonth.SelectedValue), Utilities.ObjToDecima(cboYear.SelectedValue), Utilities.ObjToInt(ctrlOrganization.CurrentValue))

            If dtData IsNot Nothing Then
                dsDataPri.Tables.Add(dtData)
                dsDataPri.Tables(0).TableName = "Tab1"
                dsDataPri.Tables.Add(DtOrgInfo)
                dsDataPri.Tables(1).TableName = "Org"

                Using xls As New ExcelCommon
                    xls.ExportExcelTemplate(
                        Server.MapPath("~/ReportTemplates//" & "Insurance" & "/" & "Report/INS_003.xlsx"),
                        item.GetDataKeyValue("NAME"), dsDataPri, Nothing, Response, _error, ExcelCommon.ExportType.Excel)
                End Using
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>07/09/2017</lastupdate>
    ''' <summary>
    ''' Set data and xuất ra template báo cáo DANH SACH BẢO HIỂM HEALTH CARE
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub printDsBHSunCare()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim _error As String = ""
        Dim dsDataPri As New DataSet
        Dim dtData As New DataTable
        Dim dtOrgInfo As New DataTable
        Dim rep As New Insurance.InsuranceRepository

        Try
            Dim item As GridDataItem = rgDanhMuc.SelectedItems(0)
            dtData = rep.GetDsBHSunCare(rdFromDate.SelectedDate, rdToDate.SelectedDate, Common.Common.GetUsername, Utilities.ObjToInt(ctrlOrganization.CurrentValue))
            dtOrgInfo = rep.GetOrgInfo(rdFromDate.SelectedDate, rdToDate.SelectedDate, Utilities.ObjToInt(ctrlOrganization.CurrentValue))

            If dtData IsNot Nothing Then
                dsDataPri.Tables.Add(dtData)
                dsDataPri.Tables(0).TableName = "Tab1"
                dsDataPri.Tables.Add(dtOrgInfo)
                dsDataPri.Tables(1).TableName = "Tab2"

                Using xls As New ExcelCommon
                    xls.ExportExcelTemplate(
                        Server.MapPath("~/ReportTemplates//" & "Insurance" & "/" & "Report/INS_004.xlsx"),
                        item.GetDataKeyValue("NAME"), dsDataPri, Nothing, Response, _error, ExcelCommon.ExportType.Excel)
                End Using
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>07/09/2017</lastupdate>
    ''' <summary>
    ''' Set data and xuất ra template báo cáo DANH SACH ĐIỀU CHỈNH BẢO HIỂM HEALTH CARE
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub printDsDieuChinhSunCare()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim _error As String = ""
        Dim dsDataPri As New DataSet
        Dim dtData As New DataTable
        Dim dtOrgInfo As New DataTable
        Dim rep As New Insurance.InsuranceRepository

        Try
            Dim item As GridDataItem = rgDanhMuc.SelectedItems(0)
            dtData = rep.GetDsDieuChinhSunCare(rdFromDate.SelectedDate, rdToDate.SelectedDate, Common.Common.GetUsername, Utilities.ObjToInt(ctrlOrganization.CurrentValue))
            dtOrgInfo = rep.GetOrgInfo(rdFromDate.SelectedDate, rdToDate.SelectedDate, Utilities.ObjToInt(ctrlOrganization.CurrentValue))

            If dtData IsNot Nothing Then
                dsDataPri.Tables.Add(dtData)
                dsDataPri.Tables(0).TableName = "Tab1"
                dsDataPri.Tables.Add(dtOrgInfo)
                dsDataPri.Tables(1).TableName = "Tab2"

                Using xls As New ExcelCommon
                    xls.ExportExcelTemplate(
                        Server.MapPath("~/ReportTemplates//" & "Insurance" & "/" & "Report/INS_005.xlsx"),
                        item.GetDataKeyValue("NAME"), dsDataPri, Nothing, Response, _error, ExcelCommon.ExportType.Excel)
                End Using
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>07/09/2017</lastupdate>
    ''' <summary>
    ''' Set data and xuất ra template báo cáo TỔNG HỢP CHI PHÍ BẢO HIỂM HEALTH CARE
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub printChiPhiSunCare()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim _error As String = ""
        Dim dsDataPri As New DataSet
        Dim dtData As New DataTable
        Dim dtOrgInfo As New DataTable
        Dim rep As New Insurance.InsuranceRepository

        Try
            Dim item As GridDataItem = rgDanhMuc.SelectedItems(0)
            dtData = rep.GetChiPhiSunCare(rdFromDate.SelectedDate, rdToDate.SelectedDate, Common.Common.GetUsername, Utilities.ObjToInt(ctrlOrganization.CurrentValue))
            dtOrgInfo = rep.GetOrgInfo(rdFromDate.SelectedDate, rdToDate.SelectedDate, Utilities.ObjToInt(ctrlOrganization.CurrentValue))

            If dtData IsNot Nothing Then
                dsDataPri.Tables.Add(dtData)
                dsDataPri.Tables(0).TableName = "Tab1"
                dsDataPri.Tables.Add(dtOrgInfo)
                dsDataPri.Tables(1).TableName = "Org"

                Using xls As New ExcelCommon
                    xls.ExportExcelTemplate(
                        Server.MapPath("~/ReportTemplates//" & "Insurance" & "/" & "Report/INS_006.xlsx"),
                        item.GetDataKeyValue("NAME"), dsDataPri, Nothing, Response, _error, ExcelCommon.ExportType.Excel)
                End Using
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

End Class