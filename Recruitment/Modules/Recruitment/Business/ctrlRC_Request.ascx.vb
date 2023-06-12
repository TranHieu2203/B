Imports System.IO
Imports Aspose.Cells
Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic
Imports Recruitment.RecruitmentBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlRC_Request
    Inherits Common.CommonView
    Protected WithEvents RequestView As ViewBase
    Private store As New RecruitmentStoreProcedure()
    Protected WithEvents ctrlCommon_Reject As ctrlCommon_Reject
    'Dim sListRejectID As String = ""
    Dim dsDataComper As New DataTable
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
    Property IS_INSERT_PROGRAM As Boolean
        Get
            Return ViewState(Me.ID & "_IS_INSERT_PROGRAM")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_IS_INSERT_PROGRAM") = value
        End Set
    End Property

    Property sListRejectID As String
        Get
            Return ViewState(Me.ID & "_ListRejectID")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_ListRejectID") = value
        End Set
    End Property

    Property dtData As DataTable
        Get
            If ViewState(Me.ID & "_dtData") Is Nothing Then
                Dim dt As New DataTable
                dt.Columns.Add("STT", GetType(String))
                dt.Columns.Add("ORG_NAME", GetType(String))
                dt.Columns.Add("ORG_ID", GetType(String))
                dt.Columns.Add("TITLE_NAME", GetType(String))
                dt.Columns.Add("TITLE_ID", GetType(String))
                dt.Columns.Add("Work_Location_name", GetType(String))
                dt.Columns.Add("Work_Location_ID", GetType(String))
                dt.Columns.Add("SEND_DATE", GetType(String))
                dt.Columns.Add("EXPECTED_JOIN_DATE", GetType(String))
                dt.Columns.Add("LABOR_TYPE_NAME", GetType(String))
                dt.Columns.Add("LABOR_TYPE_ID", GetType(String))
                dt.Columns.Add("RC_RECRUIT_PROPERTY_NAME", GetType(String))
                dt.Columns.Add("RC_RECRUIT_PROPERTY", GetType(String))
                dt.Columns.Add("RECRUIT_REASON_NAME", GetType(String))
                dt.Columns.Add("RECRUIT_REASON_ID", GetType(String))
                dt.Columns.Add("RECRUIT_NUMBER", GetType(String))
                dt.Columns.Add("IS_OVER_LIMIT_NAME", GetType(String))
                dt.Columns.Add("IS_OVER_LIMIT", GetType(String))
                dt.Columns.Add("IS_SUPPORT_NAME", GetType(String))
                dt.Columns.Add("IS_SUPPORT", GetType(String))
                dt.Columns.Add("RECRUIT_REASON", GetType(String))
                dt.Columns.Add("LEARNING_LEVEL_NAME", GetType(String))
                dt.Columns.Add("LEARNING_LEVEL_ID", GetType(String))
                dt.Columns.Add("AGE_FROM", GetType(String))
                dt.Columns.Add("AGE_TO", GetType(String))
                dt.Columns.Add("QUALIFICATION_NAME", GetType(String))
                dt.Columns.Add("QUALIFICATION", GetType(String))
                dt.Columns.Add("LANGUAGE_NAME", GetType(String))
                dt.Columns.Add("LANGUAGE", GetType(String))
                dt.Columns.Add("LANGUAGELEVEL_NAME", GetType(String))
                dt.Columns.Add("LANGUAGELEVEL", GetType(String))
                dt.Columns.Add("LANGUAGESCORES", GetType(String))
                dt.Columns.Add("FOREIGN_ABILITY", GetType(String))
                dt.Columns.Add("EXPERIENCE_NUMBER", GetType(String))
                dt.Columns.Add("GENDER_PRIORITY_NAME", GetType(String))
                dt.Columns.Add("GENDER_PRIORITY", GetType(String))
                dt.Columns.Add("COMPUTER_LEVEL_NAME", GetType(String))
                dt.Columns.Add("COMPUTER_LEVEL", GetType(String))
                dt.Columns.Add("COMPUTER_APP_LEVEL", GetType(String))
                dt.Columns.Add("DESCRIPTION", GetType(String))
                dt.Columns.Add("MAINTASK", GetType(String))
                'dt.Columns.Add("SPECIALSKILLS_NAME", GetType(String))
                'dt.Columns.Add("SPECIALSKILLS", GetType(String))
                dt.Columns.Add("REQUEST_EXPERIENCE", GetType(String))
                dt.Columns.Add("REQUEST_OTHER", GetType(String))
                dt.Columns.Add("STATUS_NAME", GetType(String))
                dt.Columns.Add("STATUS_ID", GetType(String))
                'dt.Columns.Add("REMARK", GetType(String))
                ViewState(Me.ID & "_dtData") = dt
            End If
            Return ViewState(Me.ID & "_dtData")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtData") = value
        End Set
    End Property

    Property dtDataImport As DataTable
        Get
            Return ViewState(Me.ID & "_dtDataImport")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtDataImport") = value
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
                dtData = rep.GetOtherList("RC_REQUEST_STATUS", True)
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
            ''TODO: DAIDM comment
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create, ToolbarItem.Edit, ToolbarItem.Export,
                                       ToolbarItem.Delete, ToolbarItem.Approve, ToolbarItem.Create)
            CType(MainToolBar.Items(2), RadToolBarButton).Text = "Xuất Excel"
            CType(MainToolBar.Items(4), RadToolBarButton).Text = "Phê duyệt"
            CType(MainToolBar.Items(5), RadToolBarButton).Text = "Sao chép"
            CType(MainToolBar.Items(5), RadToolBarButton).CommandName = "COPY_REQUEST"
            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem(CommonMessage.TOOLBARITEM_CANCEL,
                                                                  ToolbarIcons.Cancel,
                                                                  ToolbarAuthorize.Special1,
                                                                  "Hủy"))

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
                    If rep.DeleteRequest(lstDeletes) Then
                        Refresh("UpdateView")
                        UpdateControlState()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        UpdateControlState()
                    End If
                Case CommonMessage.STATE_APPROVE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgData.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgData.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If store.CHECK_EXIST_SE_CONFIG("PERSONRE_TCTD") = -1 Then
                        IS_INSERT_PROGRAM = False
                    Else
                        IS_INSERT_PROGRAM = True
                    End If
                    If rep.UpdateStatusRequest(lstDeletes, RecruitmentCommon.RC_REQUEST_STATUS.APPROVE_ID, IS_INSERT_PROGRAM) Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                        rgData.Rebind()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                    End If

                Case CommonMessage.STATE_REJECT
                    'Dim lstDeletes As New List(Of Decimal)
                    'For idx = 0 To rgData.SelectedItems.Count - 1
                    '    Dim item As GridDataItem = rgData.SelectedItems(idx)
                    '    lstDeletes.Add(item.GetDataKeyValue("ID"))
                    'Next
                    'If rep.UpdateStatusRequest(lstDeletes, RecruitmentCommon.RC_REQUEST_STATUS.NOT_APPROVE_ID) Then
                    '    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    '    CurrentState = CommonMessage.STATE_NORMAL
                    '    rgData.Rebind()
                    'Else
                    '    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                    'End If

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

            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_EDIT
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rgData.SelectedItems.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    CurrentState = CommonMessage.STATE_EDIT
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_DELETE
                    For Each item As GridDataItem In rgData.SelectedItems
                        If item.GetDataKeyValue("STATUS_ID") = RecruitmentCommon.RC_REQUEST_STATUS.APPROVE_ID Then
                            ShowMessage(Translate("Tồn tại bản ghi đã ở trạng thái phê duyệt, thao tác thực hiện không thành công"), NotifyType.Warning)
                            Exit Sub
                        End If
                        If item.GetDataKeyValue("STATUS_ID") = RecruitmentCommon.RC_REQUEST_STATUS.NOT_APPROVE_ID Then
                            ShowMessage(Translate("Tồn tại bản ghi đã ở trạng thái không phê duyệt, thao tác thực hiện không thành công"), NotifyType.Warning)
                            Exit Sub
                        End If
                    Next

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_APPROVE
                    For Each item As GridDataItem In rgData.SelectedItems
                        If item.GetDataKeyValue("STATUS_ID") = RecruitmentCommon.RC_REQUEST_STATUS.APPROVE_ID Then
                            ShowMessage(Translate("Tồn tại bản ghi đã ở trạng thái phê duyệt, thao tác thực hiện không thành công"), NotifyType.Warning)
                            Exit Sub
                        End If
                    Next

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_APPROVE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_APPROVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_REJECT
                    Dim bStatus As Boolean = True
                    sListRejectID = ""
                    For Each item As GridDataItem In rgData.SelectedItems
                        If item.GetDataKeyValue("STATUS_ID") = RecruitmentCommon.RC_REQUEST_STATUS.APPROVE_ID Then
                            ShowMessage(Translate("Tồn tại bản ghi đã ở trạng thái phê duyệt, thao tác thực hiện không thành công"), NotifyType.Warning)
                            bStatus = False
                            Exit Sub
                        Else
                            sListRejectID = sListRejectID & item.GetDataKeyValue("ID") & ","
                        End If
                    Next

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_REJECT)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_REJECT
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                    'If bStatus Then

                    '    'rwPopup.NavigateUrl = ConfigurationManager.AppSettings("HostPath").ToString() & hddLinkPopup.Value & "&RejectID=" & sListRejectID
                    '    rwPopup.NavigateUrl = "http://" & Request.Url.Host & ":" & Request.Url.Port & "/" & hddLinkPopup.Value & "&RejectID=" & sListRejectID
                    '    rwPopup.Width = "500"
                    '    rwPopup.Height = "250"
                    '    rwPopup.VisibleOnPageLoad = True
                    'End If
                    'Case CommonMessage.TOOLBARITEM_EXPORT
                    'If rgData.SelectedItems.Count = 0 Then
                    '    ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                    '    Exit Sub
                    'End If

                    'Dim sIDList As String = ""
                    'Dim hfr As New HistaffFrameworkRepository
                    'Dim tempPath As String = "~/Word/Recruitment/BM04_TT_Ke_hoach_TD.doc"
                    'For Each dr As Telerik.Web.UI.GridDataItem In rgData.SelectedItems
                    '    sIDList &= IIf(sIDList = vbNullString, Decimal.Parse(dr("ID").Text).ToString, "," & Decimal.Parse(dr("ID").Text).ToString)
                    'Next
                    'If sIDList = "," Then
                    '    sIDList = ""
                    'End If
                    'If sIDList <> "" Then

                    '    'If store.CHECK_REQUEST_NOT_APPROVE(sIDList) > 0 Then
                    '    '    ShowMessage(Translate("Tồn tại thông tin chưa được phê duyệt.<br /> Vui lòng kiểm tra lại!"), NotifyType.Warning)
                    '    '    Exit Sub
                    '    'End If

                    '    Dim dsData As DataSet = New DataSet
                    '    Dim arr() As String
                    '    arr = sIDList.Split(",")
                    '    For Each val As String In arr
                    '        Dim ds = hfr.ExecuteToDataSet("PKG_RECRUITMENT.EXPORT_RC_NEEDS_BYLISTID", New List(Of Object)({val}))
                    '        dsData.Merge(ds, True, MissingSchemaAction.Add)
                    '    Next

                    '    If (dsData Is Nothing OrElse dsData.Tables(0).Rows.Count = 0) Then
                    '        ShowMessage(Translate("Không có dữ liệu để in báo cáo"), NotifyType.Warning)
                    '        Exit Sub
                    '    End If

                    '    Dim tbl1 As New DataTable
                    '    Dim col_NAM As New DataColumn("NAM", GetType(String))
                    '    Dim col_THANG As New DataColumn("THANG", GetType(String))
                    '    Dim col_NGAY As New DataColumn("NGAY", GetType(String))

                    '    tbl1.Columns.Add(col_NAM)
                    '    tbl1.Columns.Add(col_THANG)
                    '    tbl1.Columns.Add(col_NGAY)

                    '    Dim row = tbl1.NewRow
                    '    row(0) = DateTime.Now.Year.ToString()
                    '    row(1) = DateTime.Now.Month.ToString()
                    '    row(2) = DateTime.Now.Month.ToString()
                    '    tbl1.Rows.Add(row)

                    '    dsData.Tables.Add(tbl1)
                    '    dsData.Tables(0).TableName = "DT1"
                    '    dsData.Tables(1).TableName = "DT"

                    '    For i As Int32 = 0 To dsData.Tables(0).Rows.Count - 1
                    '        dsData.Tables(0).Rows(i)("STT") = i + 1
                    '    Next

                    '    If Not System.IO.File.Exists(MapPath(tempPath)) Then
                    '        ShowMessage(Translate("Mẫu báo cáo không tồn tại"), NotifyType.Warning)
                    '        Exit Sub
                    '    End If

                    '    ExportWordMailMergeDS(System.IO.Path.Combine(Server.MapPath(tempPath)),
                    '                        "TT_KeHoach_TD_" + DateTime.Now.ToString("HHmmssddMMyyyy") + ".doc",
                    '                        dsData, Response)
                    'End If
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        dtData = CreateDataFilter(True)
                        If dtData.Rows.Count = 0 Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                            Exit Sub
                        ElseIf dtData.Rows.Count > 0 Then
                            rgData.ExportExcel(Server, Response, dtData, "Title")
                            Exit Sub
                        End If
                    End Using
                Case CommonMessage.TOOLBARITEM_NEXT
                    Export_TemplateRecruitment()
                Case CommonMessage.TOOLBARITEM_IMPORT
                    ctrlUpload1.AllowedExtensions = "xls,xlsx"
                    ctrlUpload1.Show()
                Case CommonMessage.TOOLBARITEM_CANCEL
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    ctrlCommon_Reject.Show()
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
            If e.ActionName = CommonMessage.TOOLBARITEM_APPROVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_APPROVE
                UpdateControlState()
            End If
            If e.ActionName = CommonMessage.TOOLBARITEM_REJECT And e.ButtonID = MessageBoxButtonType.ButtonYes Then

                'rwPopup.NavigateUrl = ConfigurationManager.AppSettings("HostPath").ToString() & hddLinkPopup.Value & "&RejectID=" & sListRejectID
                'rwPopup.NavigateUrl = "http://" & Request.Url.Host & ":" & Request.Url.Port & "/" & hddLinkPopup.Value & "&RejectID=" & sListRejectID
                rwPopup.NavigateUrl = "~/" & hddLinkPopup.Value & "&RejectID=" & sListRejectID
                rwPopup.Width = "500"
                rwPopup.Height = "250"
                rwPopup.VisibleOnPageLoad = True

                'CurrentState = CommonMessage.STATE_REJECT
                'UpdateControlState()
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

    Private Sub ctrlCommon_Reject_ButtonCommand(ByVal sender As Object, ByVal e As CommandSaveEventArgs) Handles ctrlCommon_Reject.ButtonCommand
        Dim rep As New RecruitmentRepository
        Try
            Dim strComment As String = e.Comment
            Dim lstID As New List(Of Decimal)
            For Each dr As GridDataItem In rgData.SelectedItems
                lstID.Add(dr.GetDataKeyValue("ID"))
            Next

            If rep.CancelRequest(lstID, strComment) Then
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                rgData.Rebind()
            Else
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Custom"

    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim _filter As New RequestDTO
        Dim rep As New RecruitmentRepository
        Dim bCheck As Boolean = False
        Try
            SetValueObjectByRadGrid(rgData, _filter)
            If ctrlOrg.CurrentValue IsNot Nothing Then
                _filter.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue)
            Else
                rgData.DataSource = New List(Of RequestDTO)
                Exit Function
            End If
            Dim _param = New ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue), _
                                               .IS_DISSOLVE = ctrlOrg.IsDissolve}
            _filter.FROM_DATE = rdFromDate.SelectedDate
            _filter.TO_DATE = rdToDate.SelectedDate
            If cboStatus.SelectedValue <> "" Then
                _filter.STATUS_ID = Decimal.Parse(cboStatus.SelectedValue)
            End If
            Dim MaximumRows As Integer
            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()
            Dim lstData As List(Of RequestDTO)
            If Sorts IsNot Nothing Then
                lstData = rep.GetRequest(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, _param, Sorts)
            Else
                lstData = rep.GetRequest(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, _param)
            End If

            rgData.VirtualItemCount = MaximumRows
            rgData.DataSource = lstData
            Return lstData.ToTable
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Sub Export_TemplateRecruitment()
        Dim rep As New RecruitmentStoreProcedure
        Try
            Dim configPath As String = ConfigurationManager.AppSettings("ReportTemplatesFolder")
            Dim filePath As String = AppDomain.CurrentDomain.BaseDirectory & configPath & "\"
            Dim user As String = UserLogHelper.GetUsername

            Dim _param = New ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue), _
                                             .IS_DISSOLVE = ctrlOrg.IsDissolve}
            Dim dsData As DataSet = rep.GetRecruitmentImport(user, _param)
            dsData.Tables(11).TableName = "Table12"
            dsData.Tables(10).TableName = "Table11"
            dsData.Tables(9).TableName = "Table10"
            dsData.Tables(8).TableName = "Table9"
            dsData.Tables(7).TableName = "Table8"
            dsData.Tables(6).TableName = "Table7"
            dsData.Tables(5).TableName = "Table6"
            dsData.Tables(4).TableName = "Table5"
            dsData.Tables(3).TableName = "Table4"
            dsData.Tables(2).TableName = "Table3"
            dsData.Tables(1).TableName = "Table2"
            dsData.Tables(0).TableName = "Table1"
            If File.Exists(filePath + "\Recruitment\Import\Template_yeucautuyendung.xls") Then
                ExportTemplate(filePath + "\Recruitment\Import\Template_yeucautuyendung.xls",
                                      dsData, Nothing, "Template_Recruitment_" & Format(Date.Now, "yyyyMMdd"))
            Else
                ShowMessage(Translate("Template không tồn tại"), Utilities.NotifyType.Error)
                Exit Sub
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function ExportTemplate(ByVal sReportFileName As String,
                                                    ByVal dsData As DataSet,
                                                    ByVal dtVariable As DataTable,
                                                    ByVal filename As String) As Boolean

        Dim filePath As String
        'Dim templatefolder As String

        Dim designer As WorkbookDesigner
        Try
            'cau hinh lai duong dan tren server
            filePath = sReportFileName

            If Not File.Exists(filePath) Then
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "javascriptfunction", "goBack()", True)
                Return False
            End If

            designer = New WorkbookDesigner
            designer.Open(filePath)
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
#End Region
    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        rgData.CurrentPageIndex = 0
        rgData.MasterTableView.SortExpressions.Clear()
        rgData.Rebind()
    End Sub

    Private Sub ctrlUpload1_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload1.OkClicked
        Dim fileName As String
        Dim dsDataPrepare As New DataSet
        Dim rep As New RecruitmentRepository
        Dim workbook As Aspose.Cells.Workbook
        Dim worksheet As Aspose.Cells.Worksheet
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If ctrlUpload1.UploadedFiles.Count = 0 Then
                ShowMessage(Translate("Bảng dữ liệu chưa có cột hoặc file không đúng định dạng"), NotifyType.Warning)
                Exit Sub
            End If
            Dim tempPath As String = ConfigurationManager.AppSettings("ExcelFileFolder")
            Dim savepath = Context.Server.MapPath(tempPath)
            For Each file As UploadedFile In ctrlUpload1.UploadedFiles
                fileName = System.IO.Path.Combine(savepath, Guid.NewGuid().ToString() & ".xls")
                file.SaveAs(fileName, True)
                Try
                    workbook = New Aspose.Cells.Workbook(fileName)
                Catch ex As Exception
                    If ex.ToString.Contains("This file's format is not supported or you don't specify a correct format") Then
                        ShowMessage(Translate("Bảng dữ liệu chưa có cột hoặc file không đúng định dạng"), NotifyType.Warning)
                        Exit Sub
                    End If
                End Try
                'If workbook.Worksheets.GetSheetByCodeName("IMPORT_REGISTER_CO") Is Nothing Then
                '    ShowMessage(Translate("File mẫu không đúng định dạng"), NotifyType.Warning)
                '    Exit Sub
                'End If
                worksheet = workbook.Worksheets(0)
                dsDataPrepare.Tables.Add(worksheet.Cells.ExportDataTableAsString(2, 0, worksheet.Cells.MaxRow + 1, worksheet.Cells.MaxColumn + 1, True))
                If System.IO.File.Exists(fileName) Then System.IO.File.Delete(fileName)
            Next
            dtData = dtData.Clone()
            dsDataComper = dsDataPrepare.Tables(0).Clone()
            For Each dt As DataTable In dsDataPrepare.Tables
                For Each row In dt.Rows
                    Dim isRow = ImportValidate.TrimRow(row)
                    If Not isRow Then
                        Continue For
                    End If
                    dtData.ImportRow(row)
                    dsDataComper.ImportRow(row)
                Next
            Next
            If loadToGrid() Then
                Dim dto As RequestDTO
                Dim gID As Decimal
                Dim dtDataImp As DataTable = dsDataPrepare.Tables(0)
                For Each dr In dsDataComper.Rows
                    dto = New RequestDTO
                    dto.ORG_ID = CDec(dr("ORG_ID"))
                    dto.TITLE_ID = CDec(dr("TITLE_ID"))
                    dto.LOCATION_ID = If(dr("Work_Location_ID") <> "", CDec(dr("Work_Location_ID")), Nothing)
                    dto.SEND_DATE = ToDate(dr("SEND_DATE"))
                    dto.EXPECTED_JOIN_DATE = ToDate(dr("EXPECTED_JOIN_DATE"))
                    dto.CONTRACT_TYPE_ID = If(dr("LABOR_TYPE_ID") <> "", CDec(dr("LABOR_TYPE_ID")), Nothing)
                    dto.RC_RECRUIT_PROPERTY = If(dr("RC_RECRUIT_PROPERTY") <> "", CDec(dr("RC_RECRUIT_PROPERTY")), Nothing)
                    dto.RECRUIT_REASON_ID = If(dr("RECRUIT_REASON_ID") <> "", CDec(dr("RECRUIT_REASON_ID")), Nothing)
                    dto.RECRUIT_NUMBER = If(dr("RECRUIT_NUMBER") <> "", CDec(dr("RECRUIT_NUMBER")), Nothing)
                    dto.IS_OVER_LIMIT = If(dr("IS_OVER_LIMIT") <> "", CDec(dr("IS_OVER_LIMIT")), Nothing)
                    'dto.IS_SUPPORT = If(dr("IS_SUPPORT") <> "", CDec(dr("IS_SUPPORT")), Nothing)
                    dto.RECRUIT_REASON = dr("RECRUIT_REASON")
                    dto.LEARNING_LEVEL_ID = If(dr("LEARNING_LEVEL_ID") <> "", CDec(dr("LEARNING_LEVEL_ID")), Nothing)
                    dto.AGE_FROM = If(dr("AGE_FROM") <> "", CDec(dr("AGE_FROM")), Nothing)
                    dto.AGE_TO = If(dr("AGE_TO") <> "", CDec(dr("AGE_TO")), Nothing)
                    dto.QUALIFICATION = If(dr("QUALIFICATION") <> "", dr("QUALIFICATION"), Nothing)
                    'dto.SPECIALSKILLS = If(dr("SPECIALSKILLS") <> "", dr("SPECIALSKILLS"), Nothing)
                    dto.LANGUAGE = If(dr("LANGUAGE") <> "", dr("LANGUAGE"), Nothing)
                    dto.LANGUAGELEVEL = If(dr("LANGUAGELEVEL") <> "", dr("LANGUAGELEVEL"), Nothing)
                    dto.LANGUAGESCORES = If(dr("LANGUAGESCORES") <> "", CDec(dr("LANGUAGESCORES")), Nothing)
                    dto.FOREIGN_ABILITY = dr("FOREIGN_ABILITY")
                    dto.EXPERIENCE_NUMBER = If(dr("EXPERIENCE_NUMBER") <> "", CDec(dr("EXPERIENCE_NUMBER")), Nothing)
                    dto.GENDER_PRIORITY = If(dr("GENDER_PRIORITY") <> "", CDec(dr("GENDER_PRIORITY")), Nothing)
                    dto.COMPUTER_LEVEL = dr("COMPUTER_LEVEL")
                    dto.COMPUTER_APP_LEVEL = dr("COMPUTER_APP_LEVEL")
                    dto.DESCRIPTION = dr("DESCRIPTION")
                    dto.MAINTASK = dr("MAINTASK")
                    dto.REQUEST_EXPERIENCE = dr("REQUEST_EXPERIENCE")
                    dto.REQUEST_OTHER = dr("REQUEST_OTHER")
                    dto.STATUS_ID = If(dr("STATUS_ID") <> "", CDec(dr("STATUS_ID")), Nothing)
                    'dto.REMARK = dr("REMARK")
                    rep.InsertRequest(dto, gID)
                Next
                CurrentState = CommonMessage.STATE_NORMAL
                Refresh("InsertView")
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                rgData.Rebind()
                _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
            End If
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
        End Try
    End Sub

    Function loadToGrid() As Boolean
        Dim dtError As New DataTable("ERROR")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If dtData.Rows.Count = 0 Then
                ShowMessage("File mẫu không đúng định dạng", NotifyType.Warning)
                Return False
            End If
            Dim rowError As DataRow
            Dim isError As Boolean = False
            Dim sError As String = String.Empty
            'Dim is_Validate As Boolean
            Dim _validate As New RequestDTO
            Dim rep As New RecruitmentRepository
            Dim store As New RecruitmentStoreProcedure
            dtData.TableName = "DATA"
            dtDataImport = dtData.Clone
            dtError = dtData.Clone
            Dim irow = 4
            Dim irowEm = 4

            For Each row As DataRow In dtData.Rows
                rowError = dtError.NewRow
                sError = "Ban/Phòng"
                ImportValidate.IsValidList("ORG_NAME", "ORG_ID", row, rowError, isError, sError)
                sError = "Vị trí tuyển dụng"
                ImportValidate.IsValidList("TITLE_NAME", "TITLE_ID", row, rowError, isError, sError)
                sError = "Ngày gửi yêu cầu không được để trống"
                ImportValidate.IsValidDate("SEND_DATE", row, rowError, isError, sError)
                sError = "Ngày cần đáp ứng không được để trống"
                ImportValidate.IsValidDate("EXPECTED_JOIN_DATE", row, rowError, isError, sError)
                sError = "Số lượng cần tuyển"
                ImportValidate.IsValidList("RECRUIT_NUMBER", "RECRUIT_NUMBER", row, rowError, isError, sError)
                sError = "Lý do tuyển dụng"
                ImportValidate.IsValidList("RECRUIT_REASON_NAME", "RECRUIT_REASON_ID", row, rowError, isError, sError)
                sError = "Trình độ học vấn"
                ImportValidate.IsValidList("LEARNING_LEVEL_NAME", "LEARNING_LEVEL_ID", row, rowError, isError, sError)
                sError = "Độ tuổi từ"
                ImportValidate.IsValidList("AGE_FROM", "AGE_FROM", row, rowError, isError, sError)
                sError = "Độ tuổi đến"
                ImportValidate.IsValidList("AGE_TO", "AGE_TO", row, rowError, isError, sError)
                sError = "Nghiệp vụ chuyên môn"
                ImportValidate.IsValidList("QUALIFICATION_NAME", "QUALIFICATION", row, rowError, isError, sError)
                sError = "Chưa nhập Mô tả công việc"
                ImportValidate.EmptyValue("DESCRIPTION", row, rowError, isError, sError)
                If isError Then
                    row("STT") = irow
                    rowError("STT") = irow
                    row("ORG_NAME") = rowError("ORG_NAME").ToString
                    row("TITLE_NAME") = rowError("TITLE_NAME").ToString
                    row("SEND_DATE") = rowError("SEND_DATE").ToString
                    row("EXPECTED_JOIN_DATE") = rowError("EXPECTED_JOIN_DATE").ToString
                    row("RECRUIT_NUMBER") = rowError("RECRUIT_NUMBER").ToString
                    row("RECRUIT_REASON_NAME") = rowError("RECRUIT_REASON_NAME").ToString
                    row("LEARNING_LEVEL_NAME") = rowError("LEARNING_LEVEL_NAME").ToString
                    row("AGE_FROM") = rowError("AGE_FROM").ToString
                    row("AGE_TO") = rowError("AGE_TO").ToString
                    row("QUALIFICATION_NAME") = rowError("QUALIFICATION_NAME").ToString
                    row("DESCRIPTION") = rowError("DESCRIPTION").ToString
                    dtError.Rows.Add(rowError)
                Else
                    'Check exist
                    Dim dto As New RequestDTO
                    dto.ORG_ID = CDec(row("ORG_ID"))
                    dto.TITLE_ID = CDec(row("TITLE_ID"))
                    dto.SEND_DATE = ToDate(row("SEND_DATE"))
                    dto.EXPECTED_JOIN_DATE = ToDate(row("EXPECTED_JOIN_DATE"))
                    If store.CheckExistRequest(dto) > 0 Then
                        rowError("STT") = irow
                        rowError("ORG_NAME") = "Đã có tuyển dụng này."
                        dtError.Rows.Add(rowError)
                    Else
                        dtDataImport.ImportRow(row)
                    End If
                End If
                irow = irow + 1
                isError = False
            Next
            If dtError.Rows.Count > 0 Then
                dtError.TableName = "DATA"
                Session("EXPORTREPORT") = dtError
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Template_yeucautuyendung_Error')", True)
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
            End If
            Return Not isError
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class