Imports Common
Imports Common.CommonMessage
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Insurance.InsuranceBusiness
Imports Telerik.Web.UI

Public Class ctrlImport_ManagerInfo
    Inherits Common.CommonView
    Dim dtDataImp As DataTable
    Public Overrides Property MustAuthorize As Boolean = False
#Region "Properties"
    Property dtDataImportEmployee As DataTable
        Get
            Return ViewState(Me.ID & "_dtDataImportEmployee")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtDataImportEmployee") = value
        End Set
    End Property
#End Region

#Region "Page"
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            ctrlOrganization.AutoPostBack = True
            ctrlOrganization.LoadDataAfterLoaded = True
            ctrlOrganization.OrganizationType = OrganizationType.OrganizationLocation
            ctrlOrganization.CheckBoxes = TreeNodeTypes.None

            Refresh()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            rgImport_ManagerInfo.AllowCustomPaging = True
            rgImport_ManagerInfo.ClientSettings.EnablePostBackOnRowClick = False
            InitControl()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMainToolBar
            Common.Common.BuildToolbar(Me.MainToolBar,
                                       ToolbarItem.Export,
                                       ToolbarItem.Import,
                                       ToolbarItem.Save,
                                       ToolbarItem.Cancel)
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub BindData()
        Try

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Try
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "EventHandle"
    Private Sub ctrlOrganization_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrganization.SelectedNodeChanged
        rgImport_ManagerInfo.CurrentPageIndex = 0
        'Me.sender = Nothing
        rgImport_ManagerInfo.Rebind()
    End Sub

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Try
            'Dim objEmployee As New EmployeeDTO
            'Dim rep As New ProfileBusinessRepository

            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case TOOLBARITEM_DELETE
                    'Kiểm tra các điều kiện để xóa.
                    If rgImport_ManagerInfo.SelectedItems.Count = 0 Then
                        ShowMessage(Translate("Bạn phải chọn nhân viên trước khi xóa"), Utilities.NotifyType.Error)
                        Exit Sub
                    End If
                    'Hiển thị Confirm delete.
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_EXPORT
                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Import_InfoIns&orgid=" & ctrlOrganization.CurrentValue & "');", True)
                Case CommonMessage.TOOLBARITEM_IMPORT
                    ctrlUpload.Show()
                Case CommonMessage.TOOLBARITEM_SAVE
                    If SaveData() Then
                        ShowMessage(Translate("Lưu dữ liệu thành công"), NotifyType.Success)
                        Dim str As String = "getRadWindow().close('1');"
                        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                    Else
                        ShowMessage(Translate("Lưu dữ liệu không thành công"), NotifyType.Error)
                    End If
                Case CommonMessage.TOOLBARITEM_SAVE
                    Dim str As String = "getRadWindow().close('1');"
                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
            End Select
            ' UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Dim strError As String = ""

                '  DeleteEmployee(strError)
                If strError = "" Then
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                Else
                    ShowMessage(Translate("Nhân viên đã có hợp đồng " & strError.Substring(1, strError.Length - 1) & ". Hãy xóa hợp đồng trước khi xóa nhân viên."), Utilities.NotifyType.Error)
                End If
                Refresh(ACTION_UPDATED)
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgImport_ManagerInfo.NeedDataSource
        Try
            ' SearchData()
            rgImport_ManagerInfo.DataSource = New List(Of INS_INFORMATIONDTO)
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub ctrlUpload_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload.OkClicked

        Dim fileName As String
        Dim dsDataPrepare As New DataSet
        Dim workbook As Aspose.Cells.Workbook
        Dim worksheet As Aspose.Cells.Worksheet
        Dim objShift As New INS_INFORMATIONDTO
        Dim rep As New InsuranceRepository
        dtDataImp = New DataTable
        'Dim gID As Decimal
        Try
            If ctrlUpload.UploadedFiles.Count = 0 Then
                ShowMessage(Translate("Bạn chưa chọn biễu mẫu import"), NotifyType.Warning)
                Exit Sub
            End If
            Dim tempPath As String = ConfigurationManager.AppSettings("ExcelFileFolder")
            Dim savepath = Context.Server.MapPath(tempPath)
            For Each file As UploadedFile In ctrlUpload.UploadedFiles
                fileName = System.IO.Path.Combine(savepath, Guid.NewGuid().ToString() & ".xls")
                file.SaveAs(fileName, True)
                workbook = New Aspose.Cells.Workbook(fileName)
                If workbook.Worksheets.GetSheetByCodeName("ImportInfomationIns") Is Nothing Then
                    ShowMessage(Translate("File mẫu không đúng định dạng"), NotifyType.Warning)
                    Exit Sub
                End If
                worksheet = workbook.Worksheets(0)
                dsDataPrepare.Tables.Add(worksheet.Cells.ExportDataTableAsString(5, 0, worksheet.Cells.MaxRow + 1, worksheet.Cells.MaxColumn + 1, True))
                If System.IO.File.Exists(fileName) Then System.IO.File.Delete(fileName)
            Next
            dtData = dtData.Clone()
            dtDataImp = dsDataPrepare.Tables(0).Clone()
            For Each dt As DataTable In dsDataPrepare.Tables
                For Each row As DataRow In dt.Rows
                    Dim isRow = ImportValidate.TrimRow(row)
                    If Not isRow Then
                        Continue For
                    End If
                    dtData.ImportRow(row)
                    dtDataImp.ImportRow(row)
                Next
            Next
            If loadToGrid() Then
                dtDataImportEmployee = dtData.Clone
                dtDataImportEmployee = dtDataImp
                rgImport_ManagerInfo.DataSource = dtDataImp
                rgImport_ManagerInfo.DataBind()
            End If
        Catch ex As Exception
            ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
        End Try
    End Sub

    Function SaveData() As Boolean
        Try
            Dim rep As New InsuranceRepository
            Dim obj As New INS_INFORMATIONDTO
            Dim gId As New Decimal?
            gId = 0
            For Each row As DataRow In dtDataImportEmployee.Rows
                obj.EMPLOYEE_ID = Decimal.Parse(row("EMPLOYEE_ID"))
                obj.EMPLOYEE_CODE = row("EMPLOYEE_CODE")
                obj.EMPLOYEE_NAME = row("FULLNAME")
                If row("SOBOOKNO") <> "" Then
                    obj.SOBOOKNO = row("SOBOOKNO")
                End If
                If row("SOPRVDBOOKDAY") <> "" Then
                    obj.SOPRVDBOOKDAY = ToDate(row("SOPRVDBOOKDAY"))
                End If
                If row("DAYPAYMENTCOMPANY") <> "" Then
                    obj.DAYPAYMENTCOMPANY = ToDate(row("DAYPAYMENTCOMPANY"))
                End If
                If row("HECARDNO") <> "" Then
                    obj.HECARDNO = row("HECARDNO")
                End If
                If row("HECARDEFFFROM") <> "" Then
                    obj.HECARDEFFFROM = ToDate(row("HECARDEFFFROM"))
                End If
                If row("HECARDEFFTO") <> "" Then
                    obj.HECARDEFFTO = ToDate(row("HECARDEFFTO"))
                End If
                If row("HEWHRREGISKEY") <> "" Then
                    obj.HEWHRREGISKEY = row("HEWHRREGISKEY")
                End If
                rep.InsertINS_INFO(obj, gId)
            Next
            Return True
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
        Return False
    End Function

    Property dtData As DataTable
        Get
            If ViewState(Me.ID & "_dtData") Is Nothing Then
                Dim dt As New DataTable
                dt.Columns.Add("STT", GetType(String))
                dt.Columns.Add("EMPLOYEE_ID", GetType(String))
                dt.Columns.Add("EMPLOYEE_CODE", GetType(String))
                dt.Columns.Add("FULLNAME", GetType(String))
                dt.Columns.Add("SOBOOKNO", GetType(String))
                dt.Columns.Add("SOPRVDBOOKDAY", GetType(String))
                dt.Columns.Add("DAYPAYMENTCOMPANY", GetType(String))
                dt.Columns.Add("HECARDNO", GetType(String))
                dt.Columns.Add("HECARDEFFFROM", GetType(String))
                dt.Columns.Add("HECARDEFFTO", GetType(String))
                dt.Columns.Add("HEWHRREGISKEY_NAME", GetType(String))
                dt.Columns.Add("HEWHRREGISKEY", GetType(String))
                ViewState(Me.ID & "_dtData") = dt
            End If
            Return ViewState(Me.ID & "_dtData")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtData") = value
        End Set
    End Property

    Function loadToGrid() As Boolean
        Dim dtError As New DataTable("ERROR")
        Dim dtEmpID As DataTable
        Try
            If dtData.Rows.Count = 0 Then
                ShowMessage("File mẫu không đúng định dạng", NotifyType.Warning)
                Return False
            End If
            Dim rowError As DataRow
            Dim isError As Boolean = False
            Dim sError As String = String.Empty
            Dim rep As New InsuranceRepository
            Dim lstEmp As New List(Of String)
            dtDataImportEmployee = dtData.Clone
            dtError = dtData.Clone
            Dim irow = 7
            For Each row As DataRow In dtData.Rows
                isError = False
                rowError = dtError.NewRow
                sError = "Chưa nhập mã nhân viên"
                ImportValidate.EmptyValue("EMPLOYEE_CODE", row, rowError, isError, sError)
                'If row("EMPLOYEE_CODE").ToString <> "" And rowError("EMPLOYEE_CODE").ToString = "" Then
                '    Dim empCode = row("EMPLOYEE_CODE").ToString
                '    If Not lstEmp.Contains(empCode) Then
                '        lstEmp.Add(empCode)
                '    Else
                '        isError = True
                '        rowError("EMPLOYEE_CODE") = "Nhân viên bị trùng trong file import"
                '    End If
                'End If
                If row("SOPRVDBOOKDAY") <> "" Then
                    ImportValidate.IsValidDate("SOPRVDBOOKDAY", row, rowError, isError, sError)
                End If
                If row("DAYPAYMENTCOMPANY") <> "" Then
                    ImportValidate.IsValidDate("DAYPAYMENTCOMPANY", row, rowError, isError, sError)
                End If
                If rowError("SOPRVDBOOKDAY").ToString = "" And _
                  rowError("DAYPAYMENTCOMPANY").ToString = "" And _
                   row("SOPRVDBOOKDAY").ToString <> "" And _
                  row("DAYPAYMENTCOMPANY").ToString <> "" Then
                    Dim startdate = ToDate(row("SOPRVDBOOKDAY"))
                    Dim enddate = ToDate(row("DAYPAYMENTCOMPANY"))
                    If startdate > enddate Then
                        rowError("SOPRVDBOOKDAY") = "Ngày cấp sổ phải nhỏ hơn ngày nộp sổ cho công ty"
                        isError = True
                    End If
                End If
                If row("HECARDEFFFROM") <> "" Then
                    ImportValidate.IsValidDate("HECARDEFFFROM", row, rowError, isError, sError)
                End If
                If row("HECARDEFFTO") <> "" Then
                    ImportValidate.IsValidDate("HECARDEFFTO", row, rowError, isError, sError)
                End If
                If rowError("HECARDEFFFROM").ToString = "" And _
                  rowError("HECARDEFFTO").ToString = "" And _
                   row("HECARDEFFFROM").ToString <> "" And _
                  row("HECARDEFFTO").ToString <> "" Then
                    Dim startdate = ToDate(row("HECARDEFFFROM"))
                    Dim enddate = ToDate(row("HECARDEFFTO"))
                    If startdate > enddate Then
                        rowError("HECARDEFFFROM") = "Ngày hiệu lực phải nhỏ hơn ngày hết hiệu lực thẻ"
                        isError = True
                    End If
                End If
                If row("HEWHRREGISKEY_NAME") <> "" Then
                    ImportValidate.IsValidList("HEWHRREGISKEY_NAME", "HEWHRREGISKEY", row, rowError, isError, sError)
                End If


                If isError Then
                    rowError("STT") = irow
                    dtError.Rows.Add(rowError)
                End If
                irow = irow + 1
            Next
            If dtError.Rows.Count > 0 Then
                dtError.TableName = "DATA"
                Session("EXPORTREPORT") = dtError
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Template_ImportInfoIns_error')", True)
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
            End If
            If isError Then
                Return False
            Else
                ' check nv them vao file import có nằm trong hệ thống không.
                For j As Integer = 0 To dtDataImp.Rows.Count - 1
                    If dtDataImp(j)("EMPLOYEE_ID") = "" Then
                        dtEmpID = New DataTable
                        dtEmpID = rep.GetEmployeeID(dtDataImp(j)("EMPLOYEE_CODE"))
                        rowError = dtError.NewRow
                        If dtEmpID Is Nothing Then
                            rowError("STT") = irow
                            rowError("EMPLOYEE_CODE") = "Mã nhân viên " & dtDataImp(j)("EMPLOYEE_CODE") & " không tồn tại trên hệ thống."
                            dtError.Rows.Add(rowError)
                        Else
                            dtDataImp(j)("EMPLOYEE_ID") = dtEmpID.Rows(0)("EMPLOYEE_ID")
                        End If
                        irow = irow + 1
                    End If
                Next
                If dtError.Rows.Count > 0 Then
                    Session("EXPORTREPORT") = dtError
                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Import_SingDefault_Error')", True)
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                Else
                    Return True
                End If
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Function
#End Region

#Region "Custom"
    Private Sub UpdateToolbarState()
        Try
            ChangeToolbarState()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region
End Class