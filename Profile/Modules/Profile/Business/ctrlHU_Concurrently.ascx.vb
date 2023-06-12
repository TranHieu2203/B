Imports System.Globalization
Imports System.IO
Imports Common
Imports Common.CommonBusiness
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI

Public Class ctrlHU_Concurrently
    Inherits Common.CommonView
    Dim log As New UserLog
    Dim psp As New ProfileStoreProcedure
    Dim com As New CommonProcedureNew
    Dim cons_com As New Contant_Common

#Region "Property"
    Private Property strID As String
        Get
            Return PageViewState(Me.ID & "_strID")
        End Get
        Set(ByVal value As String)
            PageViewState(Me.ID & "_strID") = value
        End Set

    End Property

    Public Property Concurrently As List(Of Temp_ConcurrentlyDTO)
        Get
            Return ViewState(Me.ID & "_Concurrently")
        End Get

        Set(ByVal value As List(Of Temp_ConcurrentlyDTO))
            ViewState(Me.ID & "_Concurrently") = value
        End Set
    End Property
    Private Property dtContract As DataTable
        Get
            Return PageViewState(Me.ID & "_dtContract")
        End Get
        Set(ByVal value As DataTable)
            PageViewState(Me.ID & "_dtContract") = value
        End Set
    End Property

    Property ListComboData As Profile.ProfileBusiness.ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ListComboData")
        End Get
        Set(ByVal value As Profile.ProfileBusiness.ComboBoxDataDTO)
            ViewState(Me.ID & "_ListComboData") = value
        End Set
    End Property

    Property IsLoad As Boolean
        Get
            Return ViewState(Me.ID & "_IsUpdate")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_IsUpdate") = value
        End Set
    End Property

    Property IDSelect As Decimal
        Get
            Return ViewState(Me.ID & "_IDSelect")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_IDSelect") = value
        End Set
    End Property

    '0 - Điều chuyển thay đổi lương
    '1 - Điều chuyển hàng loạt
    Property FormType As String
        Get
            Return ViewState(Me.ID & "_FormType")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_FormType") = value
        End Set
    End Property

    Public Property popupId As String

#End Region

#Region "Page"

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            GetParams()
            Refresh()
            UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            rgConcurrently.SetFilter()
            rgConcurrently.AllowCustomPaging = True
            rgConcurrently.PageSize = Common.Common.DefaultPageSize


            Dim popup As RadWindow
            popup = CType(Me.Page, AjaxPage).PopupWindow
            popup.Title = "Thông tin nhân viên"
            'popup.Height = 643
            'popup.Width = 1350
            popupId = popup.ClientID

            GetParams()
            InitControl()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub BindData()
        Try
            GetDataCombo()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub InitControl()
        Try
            Me.MainToolBar = tbarContracts
            Common.Common.BuildToolbar(Me.MainToolBar,
                                       ToolbarItem.Create,
                                       ToolbarItem.Edit,
                                       ToolbarItem.Export,
                                       ToolbarItem.Unlock,
                                       ToolbarItem.CreateBatch,
                                       ToolbarItem.Approve,
                                       ToolbarItem.Delete)

            CType(MainToolBar.Items(0), RadToolBarButton).Text = Translate("Thêm mới")
            CType(MainToolBar.Items(3), RadToolBarButton).Text = Translate("Mở phê duyệt")
            CType(MainToolBar.Items(4), RadToolBarButton).Text = Translate("Khai báo thôi kiêm nhiệm")

            'CType(MainToolBar.Items(1), RadToolBarButton).Text = Translate("Khai báo thôi kiêm nhiệm")
            'CType(MainToolBar.Items(1), RadToolBarButton).ImageUrl = CType(MainToolBar.Items(0), RadToolBarButton).ImageUrl


            'MainToolBar.Items.Add(Common.Common.CreateToolbarItem("SYNC", ToolbarIcons.Sync, ToolbarAuthorize.Special1, "Phê duyệt hàng loạt"))

            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        'Dim rep As New ProfileBusinessRepository
        Try
            ctrlOrg.OrganizationType = OrganizationType.OrganizationLocation

            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                Select Case Message
                    Case "UpdateView"
                        rgConcurrently.Rebind()
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        rgConcurrently.Rebind()
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                End Select
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objOrgFunction As New Profile.ProfileBusiness.OrganizationDTO
        Dim sError As String = ""
        'Dim rep As New ProfileBusinessRepository
        Dim dtData As DataTable
        Dim tempPath As String = ConfigurationManager.AppSettings("WordFileFolder")
        Dim status As Integer
        Dim id As Integer
        Dim reportName As String = String.Empty
        Dim reportNameOut As String = "String.Empty"
        Dim sv_sID As String = String.Empty
        Dim codeDecision As String
        Dim decisiontype_name As String
        Dim rep As New ProfileBusinessRepository
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_EDIT
                    If rgConcurrently.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    CurrentState = CommonMessage.STATE_EDIT
                Case CommonMessage.TOOLBARITEM_REJECT
                    If rgConcurrently.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    Dim item As GridDataItem = rgConcurrently.SelectedItems(0)
                    Dim checkTypeDecision As String = ""
                    'For Each dr As Telerik.Web.UI.GridDataItem In rgConcurrently.SelectedItems
                    '    sv_sID &= IIf(sv_sID = vbNullString, dr.GetDataKeyValue("ID").ToString, "," & dr.GetDataKeyValue("ID").ToString)
                    'Next
                    For Each dr As Telerik.Web.UI.GridDataItem In rgConcurrently.SelectedItems
                        codeDecision = dr.GetDataKeyValue("CONCURENTLY_TYPE_CAN_ID").ToString
                        decisiontype_name = dr("CONCURENTLY_TYPE_CAN_NAME").Text.ToString
                        If checkTypeDecision = "" Then
                            checkTypeDecision = codeDecision
                        ElseIf checkTypeDecision <> codeDecision Then
                            ShowMessage("Vui lòng chọn cùng 1 loại quyết định để in", NotifyType.Error)
                            Exit Sub
                        End If
                        sv_sID &= IIf(sv_sID = vbNullString, dr.GetDataKeyValue("ID").ToString, "," & dr.GetDataKeyValue("ID").ToString)
                    Next
                    'dtData = psp.PRINT_CONCURRENTLY(sv_sID)
                    If dtData Is Nothing OrElse dtData.Rows.Count <= 0 Then
                        ShowMessage("Không có dữ liệu in quyết định", NotifyType.Warning)
                        Exit Sub
                    End If
                    reportName = "Decision\" + codeDecision + ".doc"
                    reportNameOut = decisiontype_name + ".doc"

                    If File.Exists(System.IO.Path.Combine(Server.MapPath(tempPath), reportName)) Then
                        ExportWordMailMerge(System.IO.Path.Combine(Server.MapPath(tempPath), reportName),
                               reportNameOut,
                               dtData,
                               Response)
                    Else
                        ShowMessage("Mẫu báo cáo không tồn tại", NotifyType.Error)
                    End If

                Case CommonMessage.TOOLBARITEM_PRINT
                    If rgConcurrently.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    Dim item As GridDataItem = rgConcurrently.SelectedItems(0)

                    Dim checkTypeDecision As String = ""
                    'For Each dr As Telerik.Web.UI.GridDataItem In rgConcurrently.SelectedItems
                    '    sv_sID &= IIf(sv_sID = vbNullString, dr.GetDataKeyValue("ID").ToString, "," & dr.GetDataKeyValue("ID").ToString)
                    'Next
                    For Each dr As Telerik.Web.UI.GridDataItem In rgConcurrently.SelectedItems
                        codeDecision = dr.GetDataKeyValue("CONCURENTLY_TYPE_ID").ToString
                        decisiontype_name = dr("CONCURENTLY_TYPE").Text.ToString
                        If checkTypeDecision = "" Then
                            checkTypeDecision = codeDecision
                        ElseIf checkTypeDecision <> codeDecision Then
                            ShowMessage("Vui lòng chọn cùng 1 loại quyết định để in", NotifyType.Error)
                            Exit Sub
                        End If
                        sv_sID &= IIf(sv_sID = vbNullString, dr.GetDataKeyValue("ID").ToString, "," & dr.GetDataKeyValue("ID").ToString)
                    Next
                    'dtData = psp.PRINT_CONCURRENTLY(sv_sID)
                    If dtData Is Nothing OrElse dtData.Rows.Count <= 0 Then
                        ShowMessage("Không có dữ liệu in quyết định", NotifyType.Warning)
                        Exit Sub
                    End If
                    reportName = "Decision\" + checkTypeDecision + ".doc"
                    reportNameOut = decisiontype_name + ".doc"

                    If File.Exists(System.IO.Path.Combine(Server.MapPath(tempPath), reportName)) Then
                        ExportWordMailMerge(System.IO.Path.Combine(Server.MapPath(tempPath), reportName),
                               reportNameOut,
                               dtData,
                               Response)
                    Else
                        ShowMessage("Mẫu báo cáo không tồn tại", NotifyType.Error)
                    End If

                Case CommonMessage.TOOLBARITEM_ACTIVE
                    If rgConcurrently.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rgConcurrently.SelectedItems.Count > 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    For Each grid As GridDataItem In rgConcurrently.SelectedItems
                        status = If(grid.GetDataKeyValue("STATUS").ToString() = "", 0, Decimal.Parse(grid.GetDataKeyValue("STATUS").ToString()))
                        If status <> 447 Then
                            ShowMessage("Không thỏa điều kiện, vui lòng kiểm tra lại", NotifyType.Warning)
                            Exit Sub
                        End If

                        If (grid("EXPIRE_DATE_CON").Text = "&nbsp;" AndAlso
                            DateTime.ParseExact(grid("EXPIRE_DATE_CON").Text, "dd/MM/yyyy", CultureInfo.InvariantCulture) <= DateTime.Now) Then
                            ShowMessage("Không thỏa điều kiện, vui lòng kiểm tra lại", NotifyType.Warning)
                            Exit Sub
                        End If

                    Next
                Case CommonMessage.TOOLBARITEM_SUBMIT
                    If rgConcurrently.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    Dim str As String = "OpenDialogEmployee();"
                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                Case CommonMessage.TOOLBARITEM_DELETE
                    Dim lstDeletes As New List(Of Decimal)
                    If rgConcurrently.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    For Each grid As GridDataItem In rgConcurrently.SelectedItems
                        id = Decimal.Parse(grid.GetDataKeyValue("ID").ToString())
                        lstDeletes.Add(id)
                        status = If(grid.GetDataKeyValue("STATUS") Is Nothing, 0, Decimal.Parse(grid.GetDataKeyValue("STATUS").ToString()))
                        If status = 447 Then
                            ShowMessage("Bản ghi đã được phê duyệt", NotifyType.Warning)
                            Exit Sub
                        End If
                        strID &= IIf(strID = vbNullString, id, "," & id)
                    Next
                    If rep.DELETE_CONCURRENTLY(lstDeletes) Then
                        Refresh("UpdateView")
                        UpdateControlState()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_IS_EFFECT_NOT_DELETE), NotifyType.Error)
                        UpdateControlState()
                    End If
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Using xls As New ExcelCommon
                        dtData = CreateDataFilter(True)
                        If dtData.Rows.Count = 0 Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                            Exit Sub
                        ElseIf dtData.Rows.Count > 0 Then
                            rgConcurrently.ExportExcel(Server, Response, dtData, "Danhsachkiemnhiem")
                            Exit Sub
                        End If
                    End Using
                Case CommonMessage.TOOLBARITEM_PRINT
                    If rgConcurrently.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                Case CommonMessage.TOOLBARITEM_SYNC
                    If rgConcurrently.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    For Each grid As GridDataItem In rgConcurrently.SelectedItems
                        id = Decimal.Parse(grid.GetDataKeyValue("ID").ToString())
                        status = If(grid.GetDataKeyValue("STATUS").ToString() = "", 0, Decimal.Parse(grid.GetDataKeyValue("STATUS").ToString()))
                        If status = 447 Then
                            ShowMessage("Bản ghi đã được phê duyệt", NotifyType.Warning)
                            Exit Sub
                        End If
                        strID &= IIf(strID = vbNullString, id, "," & id)
                    Next
                    ctrlMessageBox.MessageText = "Bạn thực sự muốn phê duyệt ?"
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_SYNC
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_UNLOCK
                    If rgConcurrently.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    Dim lstID As New List(Of Decimal)
                    For Each item As GridDataItem In rgConcurrently.SelectedItems
                        If item.GetDataKeyValue("STATUS") <> 447 Then
                            ShowMessage(Translate("Tồn tại thông tin chưa phải trạng thái phê duyệt"), NotifyType.Warning)
                            Exit Sub
                        End If
                        lstID.Add(item.GetDataKeyValue("ID"))
                    Next
                    Dim strID = String.Join(",", lstID)
                    Dim store As New ProfileStoreProcedure
                    Dim dbResult = store.UPDATE_STATUS_CONCURRENTHLY(strID, 2)
                    If dbResult.Rows.Count > 0 And dbResult.Rows(0)("RESULT") = 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                        rgConcurrently.Rebind()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                    End If

                Case CommonMessage.TOOLBARITEM_APPROVE
                    If rgConcurrently.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    Dim lstID As New List(Of Decimal)
                    For Each grid As GridDataItem In rgConcurrently.SelectedItems
                        status = If(grid.GetDataKeyValue("STATUS").ToString() = "", 0, Decimal.Parse(grid.GetDataKeyValue("STATUS").ToString()))
                        If status = 447 Then
                            ShowMessage("Tồn tại bản ghi có trạng thái phê duyệt, vui lòng kiểm tra lại", NotifyType.Warning)
                            Exit Sub
                        End If
                        lstID.Add(grid.GetDataKeyValue("ID"))
                    Next
                    Dim strID = String.Join(",", lstID)
                    Dim store As New ProfileStoreProcedure
                    Dim dbResult = store.APPROVE_CONCURRENTHLY(strID)
                    If dbResult.Rows.Count > 0 And dbResult.Rows(0)("RESULT") = 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                        rgConcurrently.Rebind()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                    End If
            End Select

            UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Sub ctrlTransferMng_OnReceiveData(ByVal sender As IViewListener(Of ViewBase), ByVal e As ViewCommunicationEventArgs) Handles Me.OnReceiveData
        Try
            Dim mess As MessageDTO = CType(e.EventData, MessageDTO)
            Select Case e.FromViewID
                Case "ctrlTransferMngNew"
                    Select Case mess.Mess
                        Case CommonMessage.ACTION_INSERTED
                            CurrentState = CommonMessage.STATE_NEW
                            IDSelect = mess.ID
                            Refresh("InsertView")
                            UpdateControlState()
                        Case CommonMessage.TOOLBARITEM_CANCEL
                            CurrentState = CommonMessage.STATE_NORMAL
                            UpdateControlState()
                    End Select
            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlOrg_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged
        Try
            rgConcurrently.Rebind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Try
            rgConcurrently.Rebind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        log = LogHelper.GetUserLog
        Dim rep As New ProfileBusinessRepository
        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DELETE
                'If com.DELETE_DATA_BY_TABLE(cons.HU_CONCURRENTLY, strID, log.Username, log.Ip + "/" + log.ComputerName) = 1 Then
                '    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                '    rgConcurrently.Rebind()
                'Else
                '    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                'End If
            End If
            If e.ActionName = CommonMessage.TOOLBARITEM_SYNC And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.TOOLBARITEM_SYNC
                'If com.APPROVE_DATA_BY_TABLE(cons.HU_CONCURRENTLY, strID, log.Username, log.Ip + "/" + log.ComputerName) = 1 Then
                '    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                '    rgConcurrently.Rebind()
                'Else
                '    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                'End If 
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub RadGrid_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgConcurrently.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
            If datarow.GetDataKeyValue("ORG_ID_DESC") IsNot Nothing Then
                datarow("ORG_CON_NAME").ToolTip = Utilities.DrawTreeByString(datarow.GetDataKeyValue("ORG_ID_DESC").ToString)
            End If
        End If

        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
            datarow("ORG_NAME").ToolTip = Utilities.DrawTreeByString(datarow.GetDataKeyValue("ORG_DESC"))
        End If
    End Sub

    Protected Sub cboCommon_ItemsRequested(ByVal sender As Object, ByVal e As RadComboBoxItemsRequestedEventArgs) _
       Handles cbSTATUS.ItemsRequested
        Using rep As New ProfileRepository
            Dim dtData As DataTable
            Dim sText As String = e.Text
            'Dim dValue As Decimal
            'Dim sSelectValue As String = IIf(e.Context("value") IsNot Nothing, e.Context("value"), "")
            Select Case sender.ID
                ' Trạng thái quyết định
                Case cbSTATUS.ID
                    dtData = com.GET_COMBOBOX("OT_OTHER_LIST", "ID", "NAME", " AND TYPE_CODE='" + OtherTypes.DecisionStatus + "' ", "NAME", True)
            End Select

            If sText <> "" And sText <> " " Then
                Dim dtExist = (From p In dtData
                               Where p(1) IsNot DBNull.Value AndAlso
                              p(1).ToString.ToUpper = sText.ToUpper)

                If dtExist.Count = 0 Then
                    Dim dtFilter = (From p In dtData
                                    Where p(1) IsNot DBNull.Value AndAlso
                              p(1).ToString.ToUpper.Contains(sText.ToUpper))

                    If dtFilter.Count > 0 Then
                        dtData = dtFilter.CopyToDataTable
                    Else
                        dtData = dtData.Clone
                    End If

                    Dim itemOffset As Integer = e.NumberOfItems
                    Dim endOffset As Integer = Math.Min(itemOffset + sender.ItemsPerRequest, dtData.Rows.Count)
                    e.EndOfItems = endOffset = dtData.Rows.Count

                    For i As Integer = itemOffset To endOffset - 1
                        Dim radItem As RadComboBoxItem = New RadComboBoxItem(dtData.Rows(i)(1).ToString(), dtData.Rows(i)(0).ToString())
                        sender.Items.Add(radItem)
                    Next
                Else

                    Dim itemOffset As Integer = e.NumberOfItems
                    Dim endOffset As Integer = dtData.Rows.Count
                    e.EndOfItems = True
                    For i As Integer = itemOffset To endOffset - 1
                        Dim radItem As RadComboBoxItem = New RadComboBoxItem(dtData.Rows(i)(1).ToString(), dtData.Rows(i)(0).ToString())
                        sender.Items.Add(radItem)
                    Next
                End If
            Else
                Dim itemOffset As Integer = e.NumberOfItems
                Dim endOffset As Integer = Math.Min(itemOffset + sender.ItemsPerRequest, dtData.Rows.Count)
                e.EndOfItems = endOffset = dtData.Rows.Count
                For i As Integer = itemOffset To endOffset - 1
                    Dim radItem As RadComboBoxItem = New RadComboBoxItem(dtData.Rows(i)(1).ToString(), dtData.Rows(i)(0).ToString())
                    sender.Items.Add(radItem)
                Next
            End If
        End Using
    End Sub

    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgConcurrently.NeedDataSource
        Try
            CreateDataFilter(False)
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 07/07/2017 08:24
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc tao du lieu filter
    ''' </summary>
    ''' <param name="isFull"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim rep As New ProfileBusinessRepository
        Dim _filter As New Temp_ConcurrentlyDTO
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim MaximumRows As Integer
        Try
            log = LogHelper.GetUserLog
            _filter.USER_NAME = log.Username.ToUpper
            _filter.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue)
            _filter.IS_DISSOLVE = ctrlOrg.IsDissolve
            _filter.EMPLOYEE_CODE = txtEmployee.Text
            If cbSTATUS.SelectedValue <> "" Then
                _filter.STATUS = cbSTATUS.SelectedValue
            End If
            _filter.START_DATE = rdStartDate.SelectedDate
            _filter.END_DATE = rdExpireDate.SelectedDate
            _filter.IS_TERMINATE = chkTerminate.Checked
            SetValueObjectByRadGrid(rgConcurrently, _filter)

            'If isFull Then
            '    dtContract = rep.GET_LIST_CONCURRENTLY(_filter)
            'Else
            '    dtContract = rep.GET_LIST_CONCURRENTLY(_filter, MaximumRows, rgConcurrently.CurrentPageIndex, rgConcurrently.PageSize)
            'End If

            'If Not IsPostBack Then
            '    DesignGrid(dtContract)
            'End If

            Dim Sorts As String = rgConcurrently.MasterTableView.SortExpressions.GetSortString()

            If isFull Then
                If Sorts IsNot Nothing Then
                    Return rep.GET_LIST_CONCURRENTLY(_filter, Sorts).ToTable()
                Else
                    Return rep.GET_LIST_CONCURRENTLY(_filter).ToTable()
                End If
            Else
                If Sorts IsNot Nothing Then
                    Me.Concurrently = rep.GET_LIST_CONCURRENTLY(_filter, rgConcurrently.CurrentPageIndex, rgConcurrently.PageSize, MaximumRows, Sorts)
                Else
                    Me.Concurrently = rep.GET_LIST_CONCURRENTLY(_filter, rgConcurrently.CurrentPageIndex, rgConcurrently.PageSize, MaximumRows)
                End If
                rep.Dispose()
                rgConcurrently.VirtualItemCount = MaximumRows
                rgConcurrently.DataSource = Me.Concurrently
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Function

#End Region

#Region "Custom"
    Protected Sub DesignGrid(ByVal dt As DataTable)
        Dim rCol As GridBoundColumn
        Dim rColCheck As GridClientSelectColumn
        Dim rColDate As GridDateTimeColumn
        rgConcurrently.MasterTableView.Columns.Clear()
        For Each column As DataColumn In dt.Columns
            If CommonConfig.APP_SETTING_13() Then
                If column.ColumnName = "SIGN_NAME" Or column.ColumnName = "SIGN_TITLE_NAME" Or column.ColumnName = "SIGN_DATE" Then
                    Continue For
                End If
            End If

            If column.ColumnName = "CBSTATUS" Then
                rColCheck = New GridClientSelectColumn()
                rgConcurrently.MasterTableView.Columns.Add(rColCheck)
                rColCheck.HeaderStyle.Width = 30
                rColCheck.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
            End If
            If Not column.ColumnName.Contains("ID") And column.ColumnName <> "STATUS" And
             column.ColumnName <> "CBSTATUS" And Not column.ColumnName.Contains("DATE") Then
                If column.ColumnName.Contains("NUMBER") Then
                    rCol = New GridBoundColumn()
                    rgConcurrently.MasterTableView.Columns.Add(rCol)
                    rCol.DataField = column.ColumnName
                    rCol.HeaderText = Translate(column.ColumnName)
                    rCol.HeaderStyle.Width = 150
                    rCol.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    rCol.AutoPostBackOnFilter = True
                    rCol.CurrentFilterFunction = GridKnownFunction.Contains
                    rCol.ShowFilterIcon = False
                    rCol.FilterControlWidth = 130
                    rCol.DataFormatString = "{0:N0}"
                    rCol.HeaderTooltip = Translate(column.ColumnName)
                    rCol.FilterControlToolTip = Translate(column.ColumnName)
                Else
                    rCol = New GridBoundColumn()
                    rgConcurrently.MasterTableView.Columns.Add(rCol)
                    rCol.DataField = column.ColumnName
                    rCol.HeaderText = Translate(column.ColumnName)
                    rCol.HeaderStyle.Width = 150
                    rCol.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    rCol.AutoPostBackOnFilter = True
                    rCol.CurrentFilterFunction = GridKnownFunction.Contains
                    rCol.ShowFilterIcon = False
                    rCol.FilterControlWidth = 130
                    rCol.HeaderTooltip = Translate(column.ColumnName)
                    rCol.FilterControlToolTip = Translate(column.ColumnName)
                End If
            End If
            If column.ColumnName.Contains("DATE") Then
                rColDate = New GridDateTimeColumn()
                rgConcurrently.MasterTableView.Columns.Add(rColDate)
                rColDate.DataField = column.ColumnName
                rColDate.DataFormatString = ConfigurationManager.AppSettings("FDATEGRID")
                rColDate.HeaderText = Translate(column.ColumnName)
                rColDate.HeaderStyle.Width = 150
                rColDate.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                rColDate.AutoPostBackOnFilter = True
                rColDate.CurrentFilterFunction = GridKnownFunction.EqualTo
                rColDate.ShowFilterIcon = False
                rColDate.FilterControlWidth = 130
                rColDate.HeaderTooltip = Translate(column.ColumnName)
                rColDate.FilterControlToolTip = Translate(column.ColumnName)
            End If
        Next
    End Sub

    Public Overrides Sub UpdateControlState()
        'Dim rep As New ProfileBusinessRepository
        Try


        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub GetDataCombo()

        Dim dtData As New DataTable
        Try
            dtData = com.GET_COMBOBOX("OT_OTHER_LIST", "ID", "NAME", " TYPE_CODE='" + OtherTypes.DecisionStatus + "' ", "NAME", True)
            If dtData.Rows.Count > 0 Then
                FillRadCombobox(cbSTATUS, dtData, "NAME", "ID", False)
            End If
            'Dim dtData As New DataTable
            ' loại quyết định kiêm nhiệm
            'dtData = psp.GET_DECISION_BY_CONCURENTLY("CONCURRENTLY_2")
            'FillRadCombobox(cboConcurrentlyType, dtData, "DECISION_TYPE_NAME", "TYPE_CODE", False)
            ' Lay chuc danh theo phong ban
            'dtData = psp.GET_TITLE_ORG(Decimal.Parse(If(ctrlOrg.CurrentValue = "", 0, ctrlOrg.CurrentValue)))
            'If dtData.Rows.Count > 0 Then
            'FillRadCombobox(cboTITLE_CON, dtData, "NAME_VN", "ID", False)
            'cboTITLE_CON.Text = String.Empty
            'End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub GetParams()
        Try
            If Request.Params("FormType") IsNot Nothing Then
                FormType = Request.Params("FormType")
            End If
            Me.ViewDescription = Translate("Quản lý quyết định")

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub rgConcurrently_PreRender(sender As Object, e As EventArgs) Handles rgConcurrently.PreRender
        Try
            If CommonConfig.APP_SETTING_13() Then
                rgConcurrently.MasterTableView.GetColumn("SIGN_NAME").Visible = False
                rgConcurrently.MasterTableView.GetColumn("SIGN_TITLE_NAME").Visible = False
                rgConcurrently.MasterTableView.GetColumn("SIGN_DATE").Visible = False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Property ViewDescription As String
#End Region

End Class