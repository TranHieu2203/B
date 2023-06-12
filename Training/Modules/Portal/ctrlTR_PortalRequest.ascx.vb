Imports System.Globalization
Imports System.IO
Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic
Imports Telerik.Web.UI
Imports Training.TrainingBusiness

Public Class ctrlTR_PortalRequest
    Inherits Common.CommonView
    Protected WithEvents RequestView As ViewBase

    Private store As New TrainingStoreProcedure
    Dim rep As New TrainingRepository

#Region "Property"
    Private Property dtLogs As DataTable
        Get
            Return PageViewState(Me.ID & "_dtLogs")
        End Get
        Set(ByVal value As DataTable)
            PageViewState(Me.ID & "_dtLogs") = value
        End Set
    End Property

    Private Const CONST_YCDT_CODE As String = "REQUEST_YCDT"
    Private Const CONST_GROUP_MAIL As String = "Training"

    Public Property EmployeeID As Decimal

    Property IsLoad As Boolean
        Get
            Return ViewState(Me.ID & "_IsLoad")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_IsLoad") = value
        End Set
    End Property

#End Region

#Region "Page"

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
    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMain
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create, ToolbarItem.Edit, ToolbarItem.Delete, ToolbarItem.Submit, ToolbarItem.Export)
            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem(CommonMessage.TOOLBARITEM_NEXT,
                                                 ToolbarIcons.Export,
                                                 ToolbarAuthorize.Export,
                                                 Translate("Xuất file mẫu")))

            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem(CommonMessage.TOOLBARITEM_IMPORT,
                                                 ToolbarIcons.Import,
                                                 ToolbarAuthorize.Import,
                                                 Translate("Nhập file mẫu")))
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            Refresh()
            UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New TrainingRepository
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
    Public Overrides Sub UpdateControlState()
        Dim rep As New TrainingRepository
        Try
            Select Case CurrentState
                Case CommonMessage.TOOLBARITEM_SUBMIT
                    Dim lstApp As New List(Of TrainingBusiness.RequestDTO)
                    Dim strId As String
                    Dim countSuccess As Integer
                    If rgData.SelectedItems.Count = 1 Then

                        For Each dr As GridDataItem In rgData.SelectedItems
                            strId = dr.GetDataKeyValue("ID").ToString + ","
                        Next

                        strId = strId.Remove(strId.LastIndexOf(",")).ToString

                        Dim outNumber As Decimal

                        Try
                            outNumber = rep.PRI_PROCESS_APP(EmployeeID, 0, "TRAINING", 0, 0, 0, strId, "")
                        Catch ex As Exception
                            ShowMessage(ex.ToString, NotifyType.Error)
                        End Try

                        If outNumber = 0 Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        ElseIf outNumber = 1 Then
                            ShowMessage(Translate("Quy trình phê duyệt chưa được thiết lập"), NotifyType.Success)
                        ElseIf outNumber = 2 Then
                            ShowMessage(Translate("Thao tác xảy ra lỗi,bạn kiểm tra lại quy trình"), NotifyType.Error)
                        ElseIf outNumber = 3 Then
                            ShowMessage(Translate("Nhân viên chưa có thiết lập nhóm chức danh"), NotifyType.Error)
                        End If
                        Dim Is_Exits As Boolean = True
                        If outNumber <> 0 Or outNumber <> 1 Or outNumber <> 2 Then
                            Dim dt = store.GET_APP_ALL_TEMPLATES()
                            If dt IsNot Nothing Then
                                For Each item In dt.Rows
                                    If Decimal.Parse(item("ID")) = outNumber Then
                                        Is_Exits = False
                                        ShowMessage(Translate("Không tồn tại cấp phê duyệt theo template phê duyệt (" + item("TEMPLATE_NAME") + " )"), NotifyType.Warning)
                                        Exit For
                                    End If
                                Next
                            End If
                        End If
                        If Is_Exits Then
                            Refresh("UpdateView")
                            UpdateControlState()
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                            CurrentState = CommonMessage.STATE_NORMAL
                            UpdateControlState()
                        End If
                    ElseIf rgData.SelectedItems.Count > 1 Then
                        For Each dr As GridDataItem In rgData.SelectedItems
                            strId = ""
                            strId = dr.GetDataKeyValue("ID").ToString + ","
                            strId = strId.Remove(strId.LastIndexOf(",")).ToString

                            Dim outNumber As Decimal

                            Try
                                outNumber = rep.PRI_PROCESS_APP(EmployeeID, 0, "TRAINING", 0, 0, 0, strId, "")
                            Catch ex As Exception
                                ShowMessage(ex.ToString, NotifyType.Error)
                            End Try

                            Dim Is_Exits As Boolean = True
                            If outNumber <> 0 Or outNumber <> 1 Or outNumber <> 2 Then
                                Dim dt = store.GET_APP_ALL_TEMPLATES()
                                If dt IsNot Nothing Then
                                    For Each item In dt.Rows
                                        If Decimal.Parse(item("ID")) = outNumber Then
                                            Is_Exits = False
                                            'ShowMessage(Translate("Không tồn tại cấp phê duyệt theo template phê duyệt (" + item("TEMPLATE_NAME") + " )"), NotifyType.Warning)
                                            Exit For
                                        End If
                                    Next
                                End If
                            End If
                            If Is_Exits Then
                                'ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                                countSuccess += 1
                            End If
                        Next
                        If countSuccess > 0 Then
                            Refresh("UpdateView")
                            UpdateControlState()
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                            CurrentState = CommonMessage.STATE_NORMAL
                            UpdateControlState()
                        End If
                    End If
            End Select
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub BindData()
        Try
            MyBase.BindData()
            Dim dtData As DataTable
            dtData = rep.GetOtherList("PROCESS_STATUS", True)
            FillRadCombobox(cboStatus, dtData, "NAME", "ID")

            rntYear.Value = Today.Date.Year
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Try

            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    UpdateControlState()

                Case CommonMessage.TOOLBARITEM_EDIT
                    CurrentState = CommonMessage.STATE_EDIT
                    UpdateControlState()

                Case CommonMessage.TOOLBARITEM_SUBMIT
                    Dim lstSubmit As New List(Of Decimal)
                    For idx = 0 To rgData.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgData.SelectedItems(idx)
                        If item.GetDataKeyValue("IS_APPROVE") <> 3 Then
                            ShowMessage(Translate("Chỉ gửi phê duyệt cho các đề xuất yêu cầu đào tạo trạng thái chưa gửi duyệt"), NotifyType.Warning)
                            Exit Sub
                        End If
                        lstSubmit.Add(item.GetDataKeyValue("ID"))
                    Next
                    CurrentState = "SUBMIT"
                    ctrlMessageBox.MessageText = "Bạn có muốn gửi duyệt"
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_SUBMIT
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_DELETE
                    For Each item As GridDataItem In rgData.SelectedItems
                        If item.GetDataKeyValue("IS_APPROVE") <> 3 Then
                            ShowMessage(Translate("Chỉ có thể xóa yêu cầu đào tạo Chưa gửi duyệt"), NotifyType.Warning)
                            Exit Sub
                        End If
                    Next
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                    CurrentState = CommonMessage.STATE_EDIT
                    UpdateControlState()

                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        dtData = CreateDataFilter(True)
                        If dtData.Rows.Count > 0 Then
                            rgData.ExportExcel(Server, Response, dtData, "TR_PortalRequest")
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                        End If
                    End Using
                Case Common.CommonMessage.TOOLBARITEM_NEXT
                    Dim repo As New TrainingStoreProcedure
                    Dim tempPath = "~/ReportTemplates/Import_TR_Request.xlsx"
                    If Not File.Exists(System.IO.Path.Combine(Server.MapPath(tempPath))) Then
                        ShowMessage(Translate("Không tồn tại file template"), Framework.UI.Utilities.NotifyType.Warning)
                        Exit Sub
                    End If
                    Dim dsDanhMuc As New DataSet
                    dsDanhMuc = repo.EXPORT_REQUEST()

                    Using xls As New AsposeExcelCommon
                        Dim bCheck = xls.ExportExcelTemplate(
                          System.IO.Path.Combine(Server.MapPath(tempPath)), "Import_TR_Request" & Format(Date.Now, "yyyyMMddHHmmss"), dsDanhMuc, Nothing, Response)
                    End Using
                Case Common.CommonMessage.TOOLBARITEM_IMPORT
                    ctrlUpload1.isMultiple = False
                    ctrlUpload1.Show()
            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_SUBMIT And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                UpdateControlState()
            End If
            If e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Select Case e.ActionName
                    Case CommonMessage.TOOLBARITEM_DELETE
                        Dim lstDeletes As New List(Of Decimal)
                        For idx = 0 To rgData.SelectedItems.Count - 1
                            Dim item As GridDataItem = rgData.SelectedItems(idx)
                            lstDeletes.Add(item.GetDataKeyValue("ID"))
                        Next
                        If rep.DeleteTrainingRequests(lstDeletes) Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            rgData.Rebind()
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        End If

                    Case CommonMessage.TOOLBARITEM_APPROVE
                        Dim lstDeletes As New List(Of Decimal)
                        For idx = 0 To rgData.SelectedItems.Count - 1
                            Dim item As GridDataItem = rgData.SelectedItems(idx)
                            lstDeletes.Add(item.GetDataKeyValue("ID"))
                        Next
                        If rep.UpdateStatusTrainingRequests(lstDeletes, TrainingCommon.TR_REQUEST_STATUS.APPROVE_ID) Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            CurrentState = CommonMessage.STATE_NORMAL
                            rgData.Rebind()
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                        End If

                    Case CommonMessage.TOOLBARITEM_REJECT
                        Dim sListRejectID As String = ""
                        For idx = 0 To rgData.SelectedItems.Count - 1
                            Dim item As GridDataItem = rgData.SelectedItems(idx)
                            sListRejectID &= item.GetDataKeyValue("ID").ToString & ","
                        Next
                        rwPopup.NavigateUrl = "~/Dialog.aspx?mid=Training&fid=ctrlTR_RequestReject&group=Business&noscroll=1&RejectID=" & sListRejectID
                        rwPopup.Width = "500"
                        rwPopup.Height = "250"
                        rwPopup.VisibleOnPageLoad = True
                End Select
            End If
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
    Protected Sub rgData_ItemDataBound(ByVal source As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgData.ItemDataBound
        Try
            If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
                Dim datarow As GridDataItem = DirectCast(e.item, GridDataItem)
                datarow("TARGET_TRAIN").Text = datarow("TARGET_TRAIN").Text.Replace(vbCrLf, "<br/>")
                datarow("CONTENT").Text = datarow("CONTENT").Text.Replace(vbCrLf, "<br/>")
                datarow("REMARK").Text = datarow("REMARK").Text.Replace(vbCrLf, "<br/>")
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim _filter As New TrainingBusiness.RequestDTO
        Dim rep As New TrainingRepository
        Dim total As Integer = 0
        Dim bCheck As Boolean = False
        Try
            Dim _param = New TrainingBusiness.ParamDTO With {.ORG_ID = Nothing, _
                                               .IS_DISSOLVE = 0}

            If cboStatus.SelectedValue <> String.Empty Then
                _filter.STATUS_ID = cboStatus.SelectedValue
            End If
            _filter.YEAR = rntYear.Value
            _filter.EMPLOYEE_ID = EmployeeID
            Dim MaximumRows As Integer
            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()
            Dim lstData As List(Of TrainingBusiness.RequestDTO)
            If isFull Then
                Return rep.GetTrainingRequestPortal(_filter, 0, Integer.MaxValue, MaximumRows, _param).ToTable()
            Else
                If Sorts IsNot Nothing Then
                    lstData = rep.GetTrainingRequestPortal(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, _param, Sorts)
                Else
                    lstData = rep.GetTrainingRequestPortal(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, _param)
                End If
            End If
            rgData.VirtualItemCount = MaximumRows
            rgData.DataSource = lstData
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Function
    Private Sub RadGrid_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgData.ItemDataBound
        Try
            If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
                Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
                'datarow("COM_NAME").ToolTip = Utilities.DrawTreeByString(datarow("COM_DESC").Text)
                datarow("ORG_NAME").ToolTip = Utilities.DrawTreeByString(datarow("ORG_DESC").Text)
            End If

            'If TypeOf (e.Item) Is GridPagerItem Then
            '    Dim myPageSizeCombo As RadComboBox = e.Item.FindControl("PageSizeComboBox")
            '    myPageSizeCombo.Items.Clear()
            '    Dim arrPageSizes() As String = {"10", "20", "50", "100", "200", "500", "1000"}
            '    For x As Integer = 0 To UBound(arrPageSizes)
            '        Dim myRadComboBoxItem As New RadComboBoxItem(arrPageSizes(x))
            '        myPageSizeCombo.Items.Add(myRadComboBoxItem)
            '        'add the following line
            '        myRadComboBoxItem.Attributes.Add("ownerTableViewId", rgWorking.MasterTableView.ClientID)
            '    Next
            '    myPageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = True
            'End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        rgData.Rebind()
    End Sub

#End Region

#Region "Custom"
    Private Sub ctrlUpload_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload1.OkClicked
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Import_Data()
        Catch ex As Exception
            ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
        End Try
    End Sub

    Private Sub Import_Data()
        Try
            Dim rep As New TrainingRepository
            Dim tempPath As String = ConfigurationManager.AppSettings("ExcelFileFolder")
            Dim countFile As Integer = ctrlUpload1.UploadedFiles.Count
            Dim fileName As String
            Dim savepath = Context.Server.MapPath(tempPath)
            Dim ds As New DataSet
            If countFile > 0 Then
                Dim file As UploadedFile = ctrlUpload1.UploadedFiles(countFile - 1)
                fileName = System.IO.Path.Combine(savepath, file.FileName)
                file.SaveAs(fileName, True)
                Using ep As New ExcelPackage
                    ds = ep.ReadExcelToDataSet(fileName, False)
                End Using
            End If
            If ds Is Nothing Then
                Exit Sub
            End If
            TableMapping(ds.Tables(0))

            If dtLogs Is Nothing Or dtLogs.Rows.Count <= 0 Then
                'Dim count As Integer = ds.Tables(0).Columns.Count - 6
                'For i = 0 To count
                '    If ds.Tables(0).Columns(i).ColumnName.Contains("Column") Then
                '        ds.Tables(0).Columns.RemoveAt(i)
                '        i = i - 1
                '    End If
                'Next

                Dim DocXml As String = String.Empty
                Dim sw As New StringWriter()
                If ds.Tables(0) IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                    ds.Tables(0).WriteXml(sw, False)
                    DocXml = sw.ToString
                    If rep.IMPORT_TR_REQUEST(DocXml) Then
                        ShowMessage(Translate(Common.CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Framework.UI.Utilities.NotifyType.Success)
                        rgData.Rebind()
                    Else
                        ShowMessage(Translate(Common.CommonMessage.MESSAGE_TRANSACTION_FAIL), Framework.UI.Utilities.NotifyType.Warning)
                    End If
                End If
            Else
                Session("EXPORTREPORT") = dtLogs
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('IMPORT_CV_ERROR')", True)
                ShowMessage(Translate("Có lỗi trong quá trình import. Lưu file lỗi chi tiết"), Utilities.NotifyType.Error)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TableMapping(ByVal dtTemp As System.Data.DataTable)
        Dim rep As New TrainingBusinessClient
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            ' lấy dữ liệu thô từ excel vào và tinh chỉnh dữ liệu
            'dtTemp.Columns(0).ColumnName = "STT"
            'dtTemp.Columns(1).ColumnName = "REQUEST_DATE"
            'dtTemp.Columns(2).ColumnName = "FULLNAME_VN"
            'dtTemp.Columns(3).ColumnName = "YEAR"
            'dtTemp.Columns(4).ColumnName = "ORG_CODE"
            'dtTemp.Columns(5).ColumnName = "TR_COURSE_NAME"
            'dtTemp.Columns(6).ColumnName = "OTHER_COURSE"
            'dtTemp.Columns(7).ColumnName = "TR_TRAIN_FIELD_NAME"
            'dtTemp.Columns(8).ColumnName = "TR_TRAIN_FORM_NAME"
            'dtTemp.Columns(9).ColumnName = "TR_PROPERTIES_NEED_NAME"
            'dtTemp.Columns(10).ColumnName = "TR_PLACE"
            'dtTemp.Columns(11).ColumnName = "CONTENT"
            'dtTemp.Columns(12).ColumnName = "CERTIFICATE"
            'dtTemp.Columns(13).ColumnName = "TR_COMMIT"
            'dtTemp.Columns(14).ColumnName = "EXPECT_DATE"
            'dtTemp.Columns(15).ColumnName = "EXPECT_DATE_TO"
            'dtTemp.Columns(16).ColumnName = "CENTER"
            'dtTemp.Columns(17).ColumnName = "TRAINER_NUMBER"
            'dtTemp.Columns(18).ColumnName = "EXPECTED_COST"
            'dtTemp.Columns(19).ColumnName = "TR_CURRENCY_NAME"
            'dtTemp.Columns(20).ColumnName = "REMARK"
            'dtTemp.Columns(21).ColumnName = "REQUEST_SENDER_ID"
            'dtTemp.Columns(22).ColumnName = "ORG_ID"
            'dtTemp.Columns(23).ColumnName = "TR_COURSE_ID"
            'dtTemp.Columns(24).ColumnName = "TR_TRAIN_FIELD_ID"
            'dtTemp.Columns(25).ColumnName = "TR_TRAIN_FORM_ID"
            'dtTemp.Columns(26).ColumnName = "TR_PROPERTIES_NEED_ID"
            'dtTemp.Columns(27).ColumnName = "CENTER_ID"
            'dtTemp.Columns(28).ColumnName = "TR_CURRENCY_ID"

            ' lấy dữ liệu thô từ excel vào và tinh chỉnh dữ liệu
            dtTemp.Columns(0).ColumnName = "STT"
            dtTemp.Columns(1).ColumnName = "REQUEST_CODE"
            dtTemp.Columns(2).ColumnName = "REQUEST_DATE"
            dtTemp.Columns(3).ColumnName = "FULLNAME_VN"
            dtTemp.Columns(4).ColumnName = "EMPLOYEE_CODE"
            dtTemp.Columns(5).ColumnName = "YEAR"
            dtTemp.Columns(6).ColumnName = "ORG_CODE"
            dtTemp.Columns(7).ColumnName = "TR_COURSE_NAME"
            dtTemp.Columns(8).ColumnName = "OTHER_COURSE"
            dtTemp.Columns(9).ColumnName = "TR_TRAIN_FIELD_NAME"
            dtTemp.Columns(10).ColumnName = "TR_TRAIN_FORM_NAME"
            dtTemp.Columns(11).ColumnName = "TR_PROPERTIES_NEED_NAME"
            dtTemp.Columns(12).ColumnName = "TR_PLACE"
            dtTemp.Columns(13).ColumnName = "CONTENT"
            dtTemp.Columns(14).ColumnName = "CERTIFICATE"
            dtTemp.Columns(15).ColumnName = "TR_COMMIT"
            dtTemp.Columns(16).ColumnName = "EXPECT_DATE"
            dtTemp.Columns(17).ColumnName = "EXPECT_DATE_TO"
            'dtTemp.Columns(18).ColumnName = "CENTER"
            dtTemp.Columns(18).ColumnName = "TRAINER_NUMBER"
            dtTemp.Columns(19).ColumnName = "EXPECTED_COST"
            dtTemp.Columns(20).ColumnName = "TR_CURRENCY_NAME"
            dtTemp.Columns(21).ColumnName = "REMARK"
            dtTemp.Columns(22).ColumnName = "REQUEST_SENDER_ID"
            dtTemp.Columns(23).ColumnName = "ORG_ID"
            dtTemp.Columns(24).ColumnName = "TR_COURSE_ID"
            dtTemp.Columns(25).ColumnName = "TR_TRAIN_FIELD_ID"
            dtTemp.Columns(26).ColumnName = "TR_TRAIN_FORM_ID"
            dtTemp.Columns(27).ColumnName = "TR_PROPERTIES_NEED_ID"
            dtTemp.Columns(28).ColumnName = "ID_CERTIFICATE"
            dtTemp.Columns(29).ColumnName = "ID_COMMIT"
            'dtTemp.Columns(31).ColumnName = "CENTER_ID"
            dtTemp.Columns(30).ColumnName = "TR_CURRENCY_ID"
            dtTemp.Columns.Add("IS_APP")

            'XOA DONG TIEU DE VA HEADER
            dtTemp.Rows(0).Delete()
            dtTemp.Rows(1).Delete()
            dtTemp.Rows(2).Delete()
            ' add Log
            Dim _error As Boolean = True
            Dim _error1 As Boolean = True
            Dim _error2 As Boolean = True
            Dim newRow As DataRow

            dtLogs = dtTemp.Clone
            dtLogs.TableName = "DATA"

            'XOA NHUNG DONG DU LIEU NULL EMPLOYYE CODE
            Dim rowDel As DataRow
            For i As Integer = 0 To dtTemp.Rows.Count - 1
                If dtTemp.Rows(i).RowState = DataRowState.Deleted OrElse dtTemp.Rows(i).RowState = DataRowState.Detached Then Continue For
                rowDel = dtTemp.Rows(i)
                If rowDel("STT").ToString.Trim = "" Then
                    dtTemp.Rows(i).Delete()
                End If
            Next

            Dim sError As String
            Dim rep1 As New CommonRepository
            Dim store As New TrainingStoreProcedure
            Dim lstEmp As New List(Of String)

            For Each rows As DataRow In dtTemp.Rows
                If rows.RowState = DataRowState.Deleted OrElse rows.RowState = DataRowState.Detached Then Continue For

                _error = False
                _error1 = False
                _error2 = False
                newRow = dtLogs.NewRow
                newRow("STT") = rows("STT")

                'bắt buộc nhập
                sError = "Chưa nhập dữ liệu"
                ImportValidate.EmptyValue("FULLNAME_VN", rows, newRow, _error, sError)
                ImportValidate.EmptyValue("REQUEST_CODE", rows, newRow, _error, sError)
                ImportValidate.EmptyValue("REQUEST_DATE", rows, newRow, _error, sError)
                ImportValidate.EmptyValue("YEAR", rows, newRow, _error, sError)
                ImportValidate.EmptyValue("ORG_CODE", rows, newRow, _error, sError)
                ImportValidate.EmptyValue("TR_COURSE_NAME", rows, newRow, _error1, sError)
                ImportValidate.EmptyValue("OTHER_COURSE", rows, newRow, _error2, sError)
                If _error1 And _error2 Then
                    _error = True
                    newRow("TR_COURSE_NAME") = ""
                    newRow("OTHER_COURSE") = ""
                Else

                End If
                ImportValidate.EmptyValue("EXPECT_DATE", rows, newRow, _error, sError)

                'CHECK DATE
                sError = "Ngày sai định dạng"
                If rows("REQUEST_DATE") Is Nothing Then
                    ImportValidate.IsValidDate("REQUEST_DATE", rows, newRow, _error, sError)
                End If
                If rows("EXPECT_DATE") Is Nothing Then
                    ImportValidate.IsValidDate("EXPECT_DATE", rows, newRow, _error, sError)
                End If
                If rows("EXPECT_DATE_TO") Is Nothing Then
                    ImportValidate.IsValidDate("EXPECT_DATE_TO", rows, newRow, _error, sError)
                End If

                If _error Then
                    dtLogs.Rows.Add(newRow)
                    _error = False
                End If
                rows("IS_APP") = 0
            Next
            dtTemp.AcceptChanges()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Function CheckDate(ByVal value As String) As Boolean
        Dim dateCheck As Boolean
        Dim result As Date

        If value = "" Or value = "&nbsp;" Then
            value = ""
            Return True
        End If

        Try
            dateCheck = DateTime.TryParseExact(value, "dd/MM/yyyy", New CultureInfo("en-US"), DateTimeStyles.None, result)
            Return dateCheck
        Catch ex As Exception
            Return False
        End Try
    End Function
#End Region

End Class