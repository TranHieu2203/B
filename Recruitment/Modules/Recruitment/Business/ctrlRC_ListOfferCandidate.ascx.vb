Imports System.IO
Imports Aspose.Cells
Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Recruitment.RecruitmentBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlRC_ListOfferCandidate
    Inherits Common.CommonView
    Private store As New RecruitmentStoreProcedure()
    Dim _myLog As New MyLog()
    Dim pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Recruitment/Modules/Recruitment/Business/" + Me.GetType().Name.ToString()
#Region "Property"
    Property IsLoad As Boolean
        Get
            Return ViewState(Me.ID & "_IsLoad")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_IsLoad") = value
        End Set
    End Property
    Property dtData_Import As DataTable
        Get
            If ViewState(Me.ID & "_dtData_Import") Is Nothing Then
                Dim dt As New DataTable("DATA")
                'dt.Columns.Add("STT", GetType(String))
                dt.Columns.Add("CODE_RC", GetType(String))
                dt.Columns.Add("CANDIDATE_CODE", GetType(String))
                dt.Columns.Add("FULLNAME_VN", GetType(String))
                dt.Columns.Add("CONTRACT_TYPE_NAME", GetType(String))
                dt.Columns.Add("CONTRACT_FROMDATE", GetType(String))
                dt.Columns.Add("CONTRACT_TODATE", GetType(String))
                dt.Columns.Add("SAL_TYPE_NAME", GetType(String))
                dt.Columns.Add("TAX_TABLE_NAME", GetType(String))
                dt.Columns.Add("EMPLOYEE_TYPE_NAME", GetType(String))
                dt.Columns.Add("SALARY_PROBATION1", GetType(String))
                dt.Columns.Add("OTHERSALARY1_1", GetType(String))
                dt.Columns.Add("PERCENT_SAL1", GetType(String))
                dt.Columns.Add("SALARY_OFFICIAL1", GetType(String))
                dt.Columns.Add("DK_LUONGCB2", GetType(String))
                dt.Columns.Add("EFFECT_DATE2", GetType(String))
                dt.Columns.Add("SALARY_PROBATION2", GetType(String))
                dt.Columns.Add("OTHERSALARY1_2", GetType(String))
                dt.Columns.Add("PERCENT_SAL2", GetType(String))
                dt.Columns.Add("SALARY_OFFICIAL2", GetType(String))
                dt.Columns.Add("PC1", GetType(String))
                dt.Columns.Add("ALLOWANCE_NAME1", GetType(String))
                dt.Columns.Add("ALLOWANCE_UNIT1", GetType(String))
                dt.Columns.Add("MONEY1", GetType(String))
                dt.Columns.Add("EFFECT_FROM1", GetType(String))
                dt.Columns.Add("EFFECT_TO1", GetType(String))
                dt.Columns.Add("PC2", GetType(String))
                dt.Columns.Add("ALLOWANCE_NAME2", GetType(String))
                dt.Columns.Add("ALLOWANCE_UNIT2", GetType(String))
                dt.Columns.Add("MONEY2", GetType(String))
                dt.Columns.Add("EFFECT_FROM2", GetType(String))
                dt.Columns.Add("EFFECT_TO2", GetType(String))

                dt.Columns.Add("PC3", GetType(String))
                dt.Columns.Add("ALLOWANCE_NAME3", GetType(String))
                dt.Columns.Add("ALLOWANCE_UNIT3", GetType(String))
                dt.Columns.Add("MONEY3", GetType(String))
                dt.Columns.Add("EFFECT_FROM3", GetType(String))
                dt.Columns.Add("EFFECT_TO3", GetType(String))
                dt.Columns.Add("PC4", GetType(String))
                dt.Columns.Add("ALLOWANCE_NAME4", GetType(String))
                dt.Columns.Add("ALLOWANCE_UNIT4", GetType(String))
                dt.Columns.Add("MONEY4", GetType(String))
                dt.Columns.Add("EFFECT_FROM4", GetType(String))
                dt.Columns.Add("EFFECT_TO4", GetType(String))
                dt.Columns.Add("PC5", GetType(String))
                dt.Columns.Add("ALLOWANCE_NAME5", GetType(String))
                dt.Columns.Add("ALLOWANCE_UNIT5", GetType(String))
                dt.Columns.Add("MONEY5", GetType(String))
                dt.Columns.Add("EFFECT_FROM5", GetType(String))
                dt.Columns.Add("EFFECT_TO5", GetType(String))

                'ID
                dt.Columns.Add("RC_PROGRAM_ID", GetType(String))
                dt.Columns.Add("CONTRACT_TYPE_ID", GetType(String))
                dt.Columns.Add("SAL_TYPE_ID", GetType(String))
                dt.Columns.Add("TAX_TABLE_ID", GetType(String))
                dt.Columns.Add("EMPLOYEE_TYPE_ID", GetType(String))
                dt.Columns.Add("PC_ID1", GetType(String))
                dt.Columns.Add("ALLOWANCE_UNIT_ID1", GetType(String))
                dt.Columns.Add("PC_ID2", GetType(String))
                dt.Columns.Add("ALLOWANCE_UNIT_ID2", GetType(String))
                dt.Columns.Add("PC_ID3", GetType(String))
                dt.Columns.Add("ALLOWANCE_UNIT_ID3", GetType(String))
                dt.Columns.Add("PC_ID4", GetType(String))
                dt.Columns.Add("ALLOWANCE_UNIT_ID4", GetType(String))
                dt.Columns.Add("PC_ID5", GetType(String))
                dt.Columns.Add("ALLOWANCE_UNIT_ID5", GetType(String))
                dt.Columns.Add("RC_CANDIDATE_ID", GetType(String))

                ViewState(Me.ID & "_dtData_Import") = dt
            End If
            Return ViewState(Me.ID & "_dtData_Import")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtData_Import") = value
        End Set
    End Property
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

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            rgData.SetFilter()
            rgData.AllowCustomPaging = True
            rgData.PageSize = Common.Common.DefaultPageSize
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            InitControl()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Public Overrides Sub BindData()
        Try
            Dim dtData As DataTable
            Using rep As New RecruitmentRepository
                dtData = store.GET_TITLE_COMBOBOX()
                FillRadCombobox(cboTitle, dtData, "NAME", "ID")

                dtData = store.GET_STATUS_RECRUITMENT()
                FillRadCombobox(cboStatus, dtData, "NAME", "ID")
            End Using
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMain
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Export, ToolbarItem.ExportTemplate, ToolbarItem.Import,
                                       ToolbarItem.Edit, ToolbarItem.Delete)
            CType(MainToolBar.Items(1), RadToolBarButton).Text = "Xuất file Offer"
            CType(MainToolBar.Items(2), RadToolBarButton).Text = "Nhập Offer"
            CType(MainToolBar.Items(3), RadToolBarButton).Text = "Chuyển HSNV"

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()
        Dim rep As New RecruitmentRepository
        Try
            Select Case CurrentState
                Case CommonMessage.STATE_DELETE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgData.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgData.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.DeleteOfferCandidate(lstDeletes) Then
                        Refresh("UpdateView")
                        UpdateControlState()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        UpdateControlState()
                    End If
                    CurrentState = CommonMessage.STATE_NORMAL
                    rgData.Rebind()
            End Select
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New RecruitmentRepository
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

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Try
            Dim rep As New RecruitmentRepository
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        dtData = CreateDataFilter(True)
                        If dtData.Rows.Count = 0 Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                            Exit Sub
                        ElseIf dtData.Rows.Count > 0 Then
                            rgData.ExportExcel(Server, Response, dtData, "ListOfferCandidate")
                            Exit Sub
                        End If
                    End Using

                Case CommonMessage.TOOLBARITEM_EXPORT_TEMPLATE
                    Template_Export()

                Case CommonMessage.TOOLBARITEM_IMPORT
                    ctrlUpload1.AllowedExtensions = "xls,xlsx"
                    ctrlUpload1.Show()

                Case CommonMessage.TOOLBARITEM_EDIT
                    Dim IS_EMP As Integer = 0
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate("Bạn cần chọn 1 UV để thực hiện chuyển sang HSNV"), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rgData.SelectedItems.Count > 1 Then
                        ShowMessage(Translate("Chỉ được chọn 1 ứng viên"), NotifyType.Warning)
                        Exit Sub
                    End If
                    For Each item As GridDataItem In rgData.SelectedItems
                        If item.GetDataKeyValue("OFFER_NAME") = "" Then
                            ShowMessage(Translate("UV chưa có thông tin Offerletter, chọn một UV khác"), NotifyType.Warning)
                            Exit Sub
                        End If
                    Next
                    Dim status As String
                    Dim items As GridDataItem = DirectCast(rgData.SelectedItems(0), GridDataItem)
                    status = items.GetDataKeyValue("STATUS_ID_STR")
                    If status <> RCContant.TRUNGTUYEN Then
                        'ShowMessage(Translate("Vui lòng chọn ứng viên có trạng thái là Trúng tuyển"), NotifyType.Warning)
                        'Exit Sub
                        IS_EMP = 1
                    End If
                    Dim _filter = New RC_TransferCAN_ToEmployeeDTO
                    _filter.CANDIDATE_ID = items.GetDataKeyValue("CANDIDATE_ID")
                    _filter.RC_PROGRAM_ID = items.GetDataKeyValue("RC_PROGRAM_ID")
                    Dim objtran = rep.GET_RC_TRANSFERCAN_TOEMPLOYEE_BYID(_filter)
                    rwPopup.VisibleOnPageLoad = True

                    If objtran IsNot Nothing Then
                        rwPopup.NavigateUrl = "/Dialog.aspx?mid=Recruitment&fid=ctrlRC_HU&group=Business&ProgramID=" & items.GetDataKeyValue("RC_PROGRAM_ID") & "&CandidateID=" & items.GetDataKeyValue("CANDIDATE_ID") & "&ID=" & objtran.ID & "&IS_EMP=" & IS_EMP
                    Else
                        rwPopup.NavigateUrl = "/Dialog.aspx?mid=Recruitment&fid=ctrlRC_HU&group=Business&ProgramID=" & items.GetDataKeyValue("RC_PROGRAM_ID") & "&CandidateID=" & items.GetDataKeyValue("CANDIDATE_ID") & "&IS_EMP=" & IS_EMP
                    End If
                    rwPopup.InitialBehaviors = WindowBehaviors.Maximize
                Case CommonMessage.TOOLBARITEM_DELETE
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate("Bạn cần chọn ứng viên cần xóa Offer"), NotifyType.Warning)
                        Exit Sub
                    End If

                    For Each item As GridDataItem In rgData.SelectedItems
                        If item.GetDataKeyValue("STATUS_ID_STR") = "NHANVIEN" Then
                            ShowMessage(Translate("Tồn tại UV đã là nhân viên, không thể xóa Offer"), NotifyType.Warning)
                            Exit Sub
                        End If
                    Next

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DELETE
                UpdateControlState()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub ctrlOrg_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged

        Try
            rgData.CurrentPageIndex = 0
            rgData.MasterTableView.SortExpressions.Clear()
            rgData.Rebind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
        Try
            CreateDataFilter()
            rwPopup.VisibleOnPageLoad = False
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub RadGrid_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgData.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
            datarow("ORG_NAME").ToolTip = Utilities.DrawTreeByString(datarow("ORG_DESC").Text)
        End If
    End Sub
#End Region

#Region "Custom"

    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim _filter As New ListOfferCandidateDTO
        Dim rep As New RecruitmentRepository
        Try
            SetValueObjectByRadGrid(rgData, _filter)
            If ctrlOrg.CurrentValue IsNot Nothing Then
                _filter.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue)
            Else
                rgData.DataSource = New List(Of ListOfferCandidateDTO)
                Exit Function
            End If
            Dim _param = New ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue), _
                                               .IS_DISSOLVE = ctrlOrg.IsDissolve}

            If rdSendFrom.SelectedDate IsNot Nothing Then
                _filter.SEND_FROM = rdSendFrom.SelectedDate
            End If

            If rdSendTo.SelectedDate IsNot Nothing Then
                _filter.SEND_TO = rdSendTo.SelectedDate
            End If

            If rdExpectedFrom.SelectedDate IsNot Nothing Then
                _filter.EXPECTED_FROM = rdExpectedFrom.SelectedDate
            End If

            If rdExpectedTo.SelectedDate IsNot Nothing Then
                _filter.EXPECTED_TO = rdExpectedTo.SelectedDate
            End If

            If cboTitle.SelectedValue <> "" Then
                _filter.TITLE_ID = cboTitle.SelectedValue
            End If

            If cboStatus.SelectedValue <> "" Then
                _filter.STATUS_ID_DEC = cboStatus.SelectedValue
            End If

            If chkIsOfferletter.Checked Then
                _filter.OFFER_ID_STR = "OFFERLETTER"
            End If

            Dim MaximumRows As Integer
            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()
            Dim lstData As List(Of ListOfferCandidateDTO)
            If Sorts IsNot Nothing Then
                lstData = rep.GetListOfferCandidate(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, _param, Sorts)
            Else
                lstData = rep.GetListOfferCandidate(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, _param)
            End If

            rgData.VirtualItemCount = MaximumRows
            rgData.DataSource = lstData
            Return lstData.ToTable
        Catch ex As Exception
            Throw ex
        End Try

    End Function

#End Region
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        rgData.CurrentPageIndex = 0
        rgData.MasterTableView.SortExpressions.Clear()
        rgData.Rebind()
    End Sub

    Private Sub ctrlUpload1_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload1.OkClicked
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim fileName As String
        Dim dsDataPrepare As New DataSet
        Dim workbook As Aspose.Cells.Workbook
        Dim worksheet As Aspose.Cells.Worksheet

        Try
            Dim tempPath As String = ConfigurationManager.AppSettings("ExcelFileFolder")
            Dim savepath = Context.Server.MapPath(tempPath)

            For Each file As UploadedFile In ctrlUpload1.UploadedFiles
                fileName = System.IO.Path.Combine(savepath, Guid.NewGuid().ToString() & ".xls")
                file.SaveAs(fileName, True)
                workbook = New Aspose.Cells.Workbook(fileName)
                worksheet = workbook.Worksheets(0)
                dsDataPrepare.Tables.Add(worksheet.Cells.ExportDataTableAsString(0, 0, worksheet.Cells.MaxRow + 1, worksheet.Cells.MaxColumn + 1, True))
                If System.IO.File.Exists(fileName) Then System.IO.File.Delete(fileName)
            Next
            dtData_Import = dtData_Import.Clone()
            TableMapping(dsDataPrepare.Tables(0))
            For Each rows As DataRow In dsDataPrepare.Tables(0).Select("CANDIDATE_CODE<>'""'").CopyToDataTable.Rows
                If IsDBNull(rows("CANDIDATE_CODE")) OrElse rows("CANDIDATE_CODE") = "" Then Continue For
                Dim newRow As DataRow = dtData_Import.NewRow
                newRow("CODE_RC") = rows("CODE_RC")
                newRow("CANDIDATE_CODE") = rows("CANDIDATE_CODE")
                newRow("FULLNAME_VN") = rows("FULLNAME_VN")
                newRow("CONTRACT_TYPE_NAME") = rows("CONTRACT_TYPE_NAME")
                newRow("CONTRACT_FROMDATE") = rows("CONTRACT_FROMDATE")
                newRow("CONTRACT_TODATE") = rows("CONTRACT_TODATE")
                newRow("SAL_TYPE_NAME") = rows("SAL_TYPE_NAME")
                newRow("TAX_TABLE_NAME") = rows("TAX_TABLE_NAME")
                newRow("EMPLOYEE_TYPE_NAME") = rows("EMPLOYEE_TYPE_NAME")


                newRow("SALARY_PROBATION1") = rows("SALARY_PROBATION1")
                newRow("OTHERSALARY1_1") = rows("OTHERSALARY1_1")
                newRow("PERCENT_SAL1") = rows("PERCENT_SAL1")
                newRow("SALARY_OFFICIAL1") = rows("SALARY_OFFICIAL1")

                newRow("DK_LUONGCB2") = rows("DK_LUONGCB2")
                newRow("EFFECT_DATE2") = rows("EFFECT_DATE2")
                newRow("SALARY_PROBATION2") = rows("SALARY_PROBATION2")

                newRow("OTHERSALARY1_2") = rows("OTHERSALARY1_2")
                newRow("PERCENT_SAL2") = rows("PERCENT_SAL2")
                newRow("SALARY_OFFICIAL2") = rows("SALARY_OFFICIAL2")
                newRow("PC1") = rows("PC1")
                newRow("ALLOWANCE_NAME1") = rows("ALLOWANCE_NAME1")
                newRow("ALLOWANCE_UNIT1") = rows("ALLOWANCE_UNIT1")
                newRow("MONEY1") = rows("MONEY1")
                newRow("EFFECT_FROM1") = rows("EFFECT_FROM1")
                newRow("EFFECT_TO1") = rows("EFFECT_TO1")
                newRow("PC2") = rows("PC2")
                newRow("ALLOWANCE_NAME2") = rows("ALLOWANCE_NAME2")
                newRow("ALLOWANCE_UNIT2") = rows("ALLOWANCE_UNIT2")
                newRow("MONEY2") = rows("MONEY2")
                newRow("EFFECT_FROM2") = rows("EFFECT_FROM2")
                newRow("EFFECT_TO2") = rows("EFFECT_TO2")

                newRow("PC3") = rows("PC3")
                newRow("ALLOWANCE_NAME3") = rows("ALLOWANCE_NAME3")
                newRow("ALLOWANCE_UNIT3") = rows("ALLOWANCE_UNIT3")
                newRow("MONEY3") = rows("MONEY3")
                newRow("EFFECT_FROM3") = rows("EFFECT_FROM3")
                newRow("EFFECT_TO3") = rows("EFFECT_TO3")
                newRow("PC4") = rows("PC4")
                newRow("ALLOWANCE_NAME4") = rows("ALLOWANCE_NAME4")
                newRow("ALLOWANCE_UNIT4") = rows("ALLOWANCE_UNIT4")
                newRow("MONEY4") = rows("MONEY4")
                newRow("EFFECT_FROM4") = rows("EFFECT_FROM4")
                newRow("EFFECT_TO4") = rows("EFFECT_TO4")
                newRow("PC5") = rows("PC5")
                newRow("ALLOWANCE_NAME5") = rows("ALLOWANCE_NAME5")
                newRow("ALLOWANCE_UNIT5") = rows("ALLOWANCE_UNIT5")
                newRow("MONEY5") = rows("MONEY5")
                newRow("EFFECT_FROM5") = rows("EFFECT_FROM5")
                newRow("EFFECT_TO5") = rows("EFFECT_TO5")
                'id
                newRow("RC_PROGRAM_ID") = If(IsNumeric(rows("RC_PROGRAM_ID")), Decimal.Parse(rows("RC_PROGRAM_ID")), Nothing)
                newRow("CONTRACT_TYPE_ID") = If(IsNumeric(rows("CONTRACT_TYPE_ID")), Decimal.Parse(rows("CONTRACT_TYPE_ID")), Nothing)
                newRow("SAL_TYPE_ID") = If(IsNumeric(rows("SAL_TYPE_ID")), Decimal.Parse(rows("SAL_TYPE_ID")), Nothing)
                newRow("TAX_TABLE_ID") = If(IsNumeric(rows("TAX_TABLE_ID")), Decimal.Parse(rows("TAX_TABLE_ID")), Nothing)
                newRow("EMPLOYEE_TYPE_ID") = If(IsNumeric(rows("EMPLOYEE_TYPE_ID")), Decimal.Parse(rows("EMPLOYEE_TYPE_ID")), Nothing)
                newRow("PC_ID1") = If(IsNumeric(rows("PC_ID1")), Decimal.Parse(rows("PC_ID1")), Nothing)
                newRow("ALLOWANCE_UNIT_ID1") = If(IsNumeric(rows("ALLOWANCE_UNIT_ID1")), Decimal.Parse(rows("ALLOWANCE_UNIT_ID1")), Nothing)
                newRow("PC_ID2") = If(IsNumeric(rows("PC_ID2")), Decimal.Parse(rows("PC_ID2")), Nothing)
                newRow("ALLOWANCE_UNIT_ID2") = If(IsNumeric(rows("ALLOWANCE_UNIT_ID2")), Decimal.Parse(rows("ALLOWANCE_UNIT_ID2")), Nothing)
                newRow("PC_ID3") = If(IsNumeric(rows("PC_ID3")), Decimal.Parse(rows("PC_ID3")), Nothing)
                newRow("ALLOWANCE_UNIT_ID3") = If(IsNumeric(rows("ALLOWANCE_UNIT_ID3")), Decimal.Parse(rows("ALLOWANCE_UNIT_ID3")), Nothing)
                newRow("PC_ID4") = If(IsNumeric(rows("PC_ID4")), Decimal.Parse(rows("PC_ID4")), Nothing)
                newRow("ALLOWANCE_UNIT_ID4") = If(IsNumeric(rows("ALLOWANCE_UNIT_ID4")), Decimal.Parse(rows("ALLOWANCE_UNIT_ID4")), Nothing)
                newRow("PC_ID5") = If(IsNumeric(rows("PC_ID5")), Decimal.Parse(rows("PC_ID5")), Nothing)
                newRow("ALLOWANCE_UNIT_ID5") = If(IsNumeric(rows("ALLOWANCE_UNIT_ID5")), Decimal.Parse(rows("ALLOWANCE_UNIT_ID5")), Nothing)
                newRow("RC_CANDIDATE_ID") = If(IsNumeric(rows("RC_CANDIDATE_ID")), Decimal.Parse(rows("RC_CANDIDATE_ID")), Nothing)

                dtData_Import.Rows.Add(newRow)
            Next
            dtData_Import.TableName = "DATA"
            If loadToGrid() Then
                Dim sw As New StringWriter()
                Dim DocXml As String = String.Empty
                dtData_Import.WriteXml(sw, False)
                DocXml = sw.ToString
                Dim sp As New RecruitmentRepository()
                If sp.IMPORT_OFFERLETTER(DocXml, LogHelper.GetUserLog().Username.ToUpper) Then
                    ShowMessage(Translate("Import thành công"), NotifyType.Success)
                Else
                    ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
                End If
                'End edit;
                'rgWorking.Rebind()
            End If

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Sub TableMapping(ByVal dtData_Import As DataTable)
        Dim row As DataRow = dtData_Import.Rows(1)
        Dim index As Integer = 0
        For Each cols As DataColumn In dtData_Import.Columns
            Try
                cols.ColumnName = row(index)
                index += 1
                If index > row.ItemArray.Length - 1 Then Exit For
            Catch ex As Exception
                Exit For
            End Try
        Next
        dtData_Import.Rows(0).Delete()
        dtData_Import.Rows(0).Delete()
        dtData_Import.AcceptChanges()
    End Sub
    Function loadToGrid() As Boolean
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim dtError As New DataTable("ERROR")
        Try
            If dtData_Import.Rows.Count = 0 Then
                ShowMessage(Translate(CommonMessage.MESSAGE_NOT_ROW), NotifyType.Warning)
                Return False
            End If
            Dim rowError As DataRow
            Dim isError As Boolean = False
            Dim sError As String = String.Empty
            Dim dtDataImportLetter = dtData_Import.Clone
            dtError = dtData_Import.Clone
            Dim iRow = 1
            For Each row As DataRow In dtData_Import.Rows
                rowError = dtError.NewRow
                isError = False
                'sError = "Chưa nhập dữ liệu"
                ImportValidate.IsValidList("RC_PROGRAM_ID", "RC_PROGRAM_ID", row, rowError, isError, "")
                ImportValidate.IsValidList("CONTRACT_TYPE_ID", "CONTRACT_TYPE_ID", row, rowError, isError, "")
                ImportValidate.IsValidList("RC_CANDIDATE_ID", "RC_CANDIDATE_ID", row, rowError, isError, "")
                ImportValidate.EmptyValue("CONTRACT_FROMDATE", row, rowError, isError, "Chưa nhập dữ liệu")
                If row("CONTRACT_FROMDATE") Is Nothing Then
                    ImportValidate.IsValidDate("CONTRACT_FROMDATE", row, rowError, isError, "Ngày sai định dạng")
                End If
                ImportValidate.IsValidList("SAL_TYPE_ID", "SAL_TYPE_ID", row, rowError, isError, "")
                ImportValidate.IsValidList("TAX_TABLE_ID", "TAX_TABLE_ID", row, rowError, isError, "")
                ImportValidate.IsValidList("EMPLOYEE_TYPE_ID", "EMPLOYEE_TYPE_ID", row, rowError, isError, "")
                ImportValidate.EmptyValue("SALARY_PROBATION1", row, rowError, isError, " ")
                ImportValidate.IsValidList("SALARY_PROBATION1", "SALARY_PROBATION1", row, rowError, isError, "")
                ImportValidate.EmptyValue("OTHERSALARY1_1", row, rowError, isError, " ")
                ImportValidate.IsValidList("OTHERSALARY1_1", "OTHERSALARY1_1", row, rowError, isError, "")
                ImportValidate.EmptyValue("PERCENT_SAL1", row, rowError, isError, " ")
                ImportValidate.IsValidList("PERCENT_SAL1", "PERCENT_SAL1", row, rowError, isError, "")
                ImportValidate.EmptyValue("SALARY_OFFICIAL1", row, rowError, isError, " ")
                ImportValidate.IsValidList("SALARY_OFFICIAL1", "SALARY_OFFICIAL1", row, rowError, isError, "")
                If IsNumeric(row("DK_LUONGCB2")) AndAlso CDec(row("DK_LUONGCB2").ToString()) = 1 Then
                    ImportValidate.EmptyValue("EFFECT_DATE2", row, rowError, isError, "Chưa nhập dữ liệu")

                    If row("EFFECT_DATE2") Is Nothing Then
                        ImportValidate.IsValidDate("EFFECT_DATE2", row, rowError, isError, "Ngày sai định dạng")
                    End If
                    ImportValidate.EmptyValue("SALARY_PROBATION2", row, rowError, isError, " ")
                    ImportValidate.IsValidList("SALARY_PROBATION2", "SALARY_PROBATION2", row, rowError, isError, "")
                    ImportValidate.EmptyValue("OTHERSALARY1_2", row, rowError, isError, " ")
                    ImportValidate.IsValidList("OTHERSALARY1_2", "OTHERSALARY1_2", row, rowError, isError, "")
                    ImportValidate.EmptyValue("PERCENT_SAL2", row, rowError, isError, " ")
                    ImportValidate.IsValidList("PERCENT_SAL2", "PERCENT_SAL2", row, rowError, isError, "")
                    ImportValidate.EmptyValue("SALARY_OFFICIAL2", row, rowError, isError, " ")
                    ImportValidate.IsValidList("SALARY_OFFICIAL2", "SALARY_OFFICIAL2", row, rowError, isError, "")
                End If
                If IsNumeric(row("PC1")) AndAlso CDec(row("PC1").ToString()) = 1 Then
                    ImportValidate.EmptyValue("PC_ID1", row, rowError, isError, " ")
                    ImportValidate.IsValidList("PC_ID1", "PC_ID1", row, rowError, isError, "")
                    ImportValidate.EmptyValue("ALLOWANCE_UNIT_ID1", row, rowError, isError, " ")
                    ImportValidate.IsValidList("ALLOWANCE_UNIT_ID1", "ALLOWANCE_UNIT_ID1", row, rowError, isError, "")
                    ImportValidate.EmptyValue("MONEY1", row, rowError, isError, " ")
                    ImportValidate.IsValidList("MONEY1", "MONEY1", row, rowError, isError, "")
                    ImportValidate.EmptyValue("EFFECT_FROM1", row, rowError, isError, "Chưa nhập dữ liệu")
                    If row("EFFECT_FROM1") Is Nothing Then
                        ImportValidate.IsValidDate("EFFECT_FROM1", row, rowError, isError, "Ngày sai định dạng")
                    End If
                End If

                If IsNumeric(row("PC2")) AndAlso CDec(row("PC2").ToString()) = 1 Then
                    ImportValidate.IsValidList("PC_ID2", "PC_ID2", row, rowError, isError, "")
                    ImportValidate.IsValidList("ALLOWANCE_UNIT_ID2", "ALLOWANCE_UNIT_ID2", row, rowError, isError, "")
                    ImportValidate.IsValidList("MONEY2", "MONEY2", row, rowError, isError, "")
                    ImportValidate.EmptyValue("EFFECT_FROM2", row, rowError, isError, "Chưa nhập dữ liệu")
                    If row("EFFECT_FROM2") Is Nothing Then
                        ImportValidate.IsValidDate("EFFECT_FROM2", row, rowError, isError, "Ngày sai định dạng")
                    End If
                End If
                If IsNumeric(row("PC3")) AndAlso CDec(row("PC3").ToString()) = 1 Then
                    ImportValidate.IsValidList("PC_ID3", "PC_ID3", row, rowError, isError, "")
                    ImportValidate.IsValidList("ALLOWANCE_UNIT_ID3", "ALLOWANCE_UNIT_ID3", row, rowError, isError, "")
                    ImportValidate.IsValidList("MONEY3", "MONEY3", row, rowError, isError, "")
                    ImportValidate.EmptyValue("EFFECT_FROM3", row, rowError, isError, "Chưa nhập dữ liệu")
                    If row("EFFECT_FROM3") Is Nothing Then
                        ImportValidate.IsValidDate("EFFECT_FROM3", row, rowError, isError, "Ngày sai định dạng")
                    End If
                End If
                If IsNumeric(row("PC4")) AndAlso CDec(row("PC4").ToString()) = 1 Then
                    ImportValidate.IsValidList("PC_ID4", "PC_ID4", row, rowError, isError, "")
                    ImportValidate.IsValidList("ALLOWANCE_UNIT_ID4", "ALLOWANCE_UNIT_ID4", row, rowError, isError, "")
                    ImportValidate.IsValidList("MONEY4", "MONEY4", row, rowError, isError, "")
                    ImportValidate.EmptyValue("EFFECT_FROM4", row, rowError, isError, "Chưa nhập dữ liệu")
                    If row("EFFECT_FROM4") Is Nothing Then
                        ImportValidate.IsValidDate("EFFECT_FROM4", row, rowError, isError, "Ngày sai định dạng")
                    End If
                End If
                If IsNumeric(row("PC5")) AndAlso CDec(row("PC5").ToString()) = 1 Then
                    ImportValidate.IsValidList("PC_ID5", "PC_ID5", row, rowError, isError, "")
                    ImportValidate.IsValidList("ALLOWANCE_UNIT_ID5", "ALLOWANCE_UNIT_ID5", row, rowError, isError, "")
                    ImportValidate.IsValidList("MONEY5", "MONEY5", row, rowError, isError, "")
                    ImportValidate.EmptyValue("EFFECT_FROM5", row, rowError, isError, "Chưa nhập dữ liệu")
                    If row("EFFECT_FROM5") Is Nothing Then
                        ImportValidate.IsValidDate("EFFECT_FROM5", row, rowError, isError, "Ngày sai định dạng")
                    End If
                End If


                If isError Then
                    rowError("CODE_RC") = row("CODE_RC").ToString
                    rowError("CANDIDATE_CODE") = row("CANDIDATE_CODE").ToString
                    rowError("FULLNAME_VN") = row("FULLNAME_VN").ToString
                    dtError.Rows.Add(rowError)
                Else
                    dtDataImportLetter.ImportRow(row)
                End If
                iRow = iRow + 1
            Next
            If dtError.Rows.Count > 0 Then
                Session("EXPORTREPORT") = dtError
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('TEMP_IMPORT_OFFERLETER');", True)
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
            End If
            If isError OrElse (dtError IsNot Nothing AndAlso dtError.Rows.Count > 0) Then
                Return False
            Else
                Return True
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Function
    Private Sub Template_Export()
        Try
            Dim psp As New RecruitmentRepository
            For Each item As GridDataItem In rgData.SelectedItems
                If item.GetDataKeyValue("OFFER_NAME") <> "" Then
                    ShowMessage(Translate("Tồn tại UV được chọn đã có thông tin Offerletter"), NotifyType.Warning)
                    Exit Sub
                End If
            Next
            Dim strID As String = ""
            For Each dr As GridDataItem In rgData.SelectedItems
                strID &= IIf(strID = vbNullString, dr.GetDataKeyValue("CANDIDATE_ID"), "," & dr.GetDataKeyValue("CANDIDATE_ID"))
            Next
            Dim dt As New DataSet
            'psp.LETTER_RECIEVE(strID)
            dt = psp.GET_HSNV_OFFERLETTER_IMPORT(strID)

            ExportTemplate("Recruitment\Import\Import_OfferLetter.xls",
                            dt, Nothing, "Import_Offerletter" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception

        End Try
    End Sub
    Public Function ExportTemplate(ByVal sReportFileName As String,
                                                    ByVal dsData As DataSet,
                                                    ByVal dtVariable As DataTable,
                                                    ByVal filename As String) As Boolean

        Dim filePath As String
        Dim templatefolder As String

        Dim designer As WorkbookDesigner
        Try

            templatefolder = ConfigurationManager.AppSettings("ReportTemplatesFolder")
            filePath = AppDomain.CurrentDomain.BaseDirectory & templatefolder & "\" & sReportFileName

            If Not File.Exists(filePath) Then
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "javascriptfunction", "goBack()", True)
                Return False
            End If

            designer = New WorkbookDesigner
            designer.Open(filePath)
            dsData.Tables(0).TableName = "Table"
            designer.SetDataSource(dsData)

            If dtVariable IsNot Nothing Then
                Dim intCols As Integer = dtVariable.Columns.Count
                For i As Integer = 0 To intCols - 1
                    designer.SetDataSource(dtVariable.Columns(i).ColumnName.ToString(), dtVariable.Rows(0).ItemArray(i).ToString())
                Next
            End If
            designer.Process()
            designer.Workbook.CalculateFormula()
            designer.Workbook.Save(HttpContext.Current.Response, filename & ".xls", ContentDisposition.Attachment, New XlsSaveOptions())

        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function
End Class