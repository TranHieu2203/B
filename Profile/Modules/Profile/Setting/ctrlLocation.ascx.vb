Imports Common
Imports Common.CommonBusiness
Imports Framework.UI
Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic
Imports Ionic.Crc
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlLocation
    Inherits Common.CommonView

    'Popup
    Protected WithEvents ctrlFindEmployee As ctrlFindEmployeePopup
    Protected WithEvents ctrlFindEmployee_Contract As ctrlFindEmployeePopup
    Protected WithEvents ctrlFindOrgPopup As ctrlFindOrgPopup
    Private psp As New ProfileStoreProcedure()
    Dim log As UserLog = LogHelper.GetUserLog
    Private hfr As New HistaffFrameworkRepository

    ''' <creator>HongDX</creator>
    ''' <lastupdate>21/06/2017</lastupdate>
    ''' <summary>Write Log</summary>
    ''' <remarks>_mylog, _pathLog, _classPath</remarks>
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Profile\Modules\Profile\List" + Me.GetType().Name.ToString()

#Region "Property"
    Property IDSelect As Decimal
        Get
            Return ViewState(Me.ID & "_IDSelect")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_IDSelect") = value
        End Set
    End Property

    Property Location As LocationDTO
        Get
            Return ViewState(Me.ID & "_Location")
        End Get
        Set(ByVal value As LocationDTO)
            ViewState(Me.ID & "_Location") = value
        End Set
    End Property

    Property Locations As List(Of LocationDTO)
        Get
            Return ViewState(Me.ID & "_Locations")
        End Get
        Set(value As List(Of LocationDTO))
            ViewState(Me.ID & "_Locations") = value
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

    Property ItemList As List(Of Decimal)
        Get
            Return PageViewState(Me.ID & "_ItemList")
        End Get
        Set(ByVal value As List(Of Decimal))
            PageViewState(Me.ID & "_ItemList") = value
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

    Property ComboData As Profile.ProfileBusiness.ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ComboData")
        End Get
        Set(ByVal value As Profile.ProfileBusiness.ComboBoxDataDTO)
            ViewState(Me.ID & "_ComboData") = value
        End Set
    End Property

    Property dtData As DataTable
        Get
            Return ViewState(Me.ID & "_dtData")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtData") = value
        End Set
    End Property

    Property OldBankID As String
        Get
            Return ViewState(Me.ID & "_OldBankID")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_OldBankID") = value
        End Set
    End Property

    Property IsUpload As Decimal
        Get
            Return ViewState(Me.ID & "_IsUpload")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_IsUpload") = value
        End Set
    End Property

    Property ActiveLocations As List(Of LocationDTO)
        Get
            Return ViewState(Me.ID & "_ActiveLocations")
        End Get
        Set(value As List(Of LocationDTO))
            ViewState(Me.ID & "_ActiveLocations") = value
        End Set
    End Property

    Property Down_File As String
        Get
            Return ViewState(Me.ID & "_Down_File")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_Down_File") = value
        End Set
    End Property
#End Region

#Region "Page"

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMain
            If Not IsPostBack Then
                'Định nghĩa Property
                InitProperty()

                'Định nghĩa ToolBar
                InitToolBar()

                GetDataCombo()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            UpdatePage()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    'Cập nhật lại trang
    Protected Sub UpdatePage(Optional ByVal Message As String = "")
        Dim rep As New ProfileRepository
        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            End If

            Select Case CurrentState
                Case CommonMessage.STATE_NORMAL

                Case CommonMessage.STATE_EDIT

                Case CommonMessage.STATE_NEW

                Case CommonMessage.STATE_DELETE
                Case CommonMessage.STATE_DETAIL
                Case CommonMessage.STATE_DEACTIVE
                    If rep.ActiveLocationID(Location, "I") Then
                        Refresh("UpdateView")
                        UpdatePage()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        UpdatePage()
                        ShowMessage(Translate(CommonMessage.MESSAGE_IS_USING), NotifyType.Error)
                    End If
                Case CommonMessage.STATE_ACTIVE
                    If rep.ActiveLocationID(Location, "A") Then
                        Refresh("UpdateView")
                        UpdatePage()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        CurrentState = CommonMessage.STATE_NORMAL
                        UpdatePage()
                    End If
            End Select
            ChangeToolbarState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    'Đổ dữ liệu vào Combobox
    Private Sub GetDataCombo()
        Dim rep As New ProfileRepository
        Try
            If ListComboData Is Nothing Then
                ListComboData = New Profile.ProfileBusiness.ComboBoxDataDTO
                ListComboData.GET_BANK = True
                'ListComboData.GET_BUSINESS = True
                'ListComboData.GET_BUSINESSTYPE = True
                ListComboData.GET_NATION = True
                ListComboData.GET_PROVINCE = True
                ListComboData.GET_DISTRICT = True
                rep.GetComboList(ListComboData)
                ComboData = ListComboData
            End If

            'Provide
            'Dim dtPlace = rep.GetProvinceList(True)
            'FillRadCombobox(cboProvince, dtPlace, "NAME", "ID")
            'Dim dt = rep.GetOtherList("LOCATION", True)
            'If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            '    FillRadCombobox(cboRegion, dt, "NAME", "ID")
            'End If

            'FillDropDownList(cbBank, ListComboData.LIST_BANK, "NAME", "ID", Common.Common.SystemLanguage, True, cbBank.SelectedValue)

            'Dim lstSource As DataTable = psp.GetInsListInsurance(False)
            'If lstSource IsNot Nothing AndAlso lstSource.Rows.Count > 0 Then
            '    FillRadCombobox(rcOrgInsurance, lstSource, "NAME", "ID")
            'End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

#Region "Event"

    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objLocationFunction As New LocationDTO
        Dim gID As Decimal
        Dim result As Int32 = 0
        Dim rep As New ProfileRepository
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE

                Case CommonMessage.TOOLBARITEM_EDIT

                Case CommonMessage.TOOLBARITEM_ACTIVE
                    If rgLocation.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ActiveLocations = RepareDataForAction()

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_ACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_ACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case CommonMessage.TOOLBARITEM_DEACTIVE
                    If rgLocation.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ActiveLocations = RepareDataForAction()

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DEACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_DEACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_EXPORT

                    'Dim _error As Integer = 0
                    'Using xls As New ExcelCommon
                    '    Dim bCheck = xls.ExportExcelTemplate(
                    '        Server.MapPath("~/ReportTemplates/" & Request.Params("mid") & "/" & Request.Params("fid") & ".xls"),
                    '        "Location", dtData, Response, _error)
                    '    If Not bCheck Then
                    '        Select Case _error
                    '            Case 1
                    '                ShowMessage(Translate("Mẫu báo cáo không tồn tại"), NotifyType.Warning)
                    '            Case 2
                    '                ShowMessage(Translate("Dữ liệu không tồn tại"), NotifyType.Warning)
                    '        End Select
                    '        Exit Sub
                    '    End If
                    'End Using
                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        dtData = CreateDataFilter(True)
                        If dtData.Rows.Count = 0 Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                            Exit Sub
                        ElseIf dtData.Rows.Count > 0 Then
                            rgLocation.ExportExcel(Server, Response, dtData, "Location")
                            Exit Sub
                        End If
                    End Using
                Case CommonMessage.TOOLBARITEM_SAVE

                Case CommonMessage.TOOLBARITEM_DELETE
                    If rgLocation.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    For Each item As GridDataItem In rgLocation.SelectedItems
                        Dim status As String = item.GetDataKeyValue("ACTFLG").ToString()
                        If status = "Áp dụng" Then
                            ShowMessage(Translate("Tồn tại bản ghi có trạng thái Áp dụng. Không thể xóa"), NotifyType.Warning)
                            Exit Sub
                        End If
                    Next
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    isLoadPopup = 0
                    Refresh("Cancel")
                    UpdatePage()
                    'Exit Sub
            End Select

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim rep As New ProfileRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim _filter As New LocationDTO
        Try
            If ctrlOrganization.CurrentValue IsNot Nothing Then
                _filter.ORG_ID = Decimal.Parse(ctrlOrganization.CurrentValue)
            Else
                rgLocation.DataSource = New List(Of LocationDTO)
                Exit Function
            End If

            Dim _param = New ProfileBusiness.ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrganization.CurrentValue),
                                                            .IS_DISSOLVE = ctrlOrganization.IsDissolve}

            SetValueObjectByRadGrid(rgLocation, _filter)

            Dim MaximumRows As Integer
            Dim Sorts As String = rgLocation.MasterTableView.SortExpressions.GetSortString()
            If isFull Then
                If Sorts IsNot Nothing Then
                    Return rep.GetLocation_V2(_filter, _param, Sorts).ToTable()
                Else
                    Return rep.GetLocation_V2(_filter, _param).ToTable()
                End If
            Else
                If Sorts IsNot Nothing Then
                    rgLocation.DataSource = rep.GetLocation_V2(_filter, rgLocation.CurrentPageIndex, rgLocation.PageSize, MaximumRows, _param, Sorts)
                Else
                    rgLocation.DataSource = rep.GetLocation_V2(_filter, rgLocation.CurrentPageIndex, rgLocation.PageSize, MaximumRows, _param)
                End If

                rgLocation.VirtualItemCount = MaximumRows
            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Function

    'Private Function CreateDataFilter(v As Boolean) As DataTable
    '    Dim rep As New ProfileRepository
    '    Dim repC As New CommonRepository
    '    Dim lstID As New List(Of Decimal)

    '    Dim lstOrgPermission As New List(Of Common.CommonBusiness.OrganizationDTO)

    '    If Common.Common.OrganizationLocationDataSession Is Nothing Then
    '        lstOrgPermission = repC.GetOrganizationLocationTreeView()
    '    Else
    '        lstOrgPermission = Common.Common.OrganizationLocationDataSession
    '    End If

    '    For Each org In lstOrgPermission
    '        lstID.Add(org.ID)
    '    Next

    '    Locations = rep.GetLocation("", lstID)
    '    Return Locations.ToTable()
    'End Function

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim rep As New ProfileRepository
        Dim result As Boolean
        Dim idList As String = String.Empty
        Try
            If e.ButtonID = MessageBoxButtonType.ButtonYes Then
                'For Each dr As Decimal In ItemList
                '    idList &= IIf(idList = vbNullString, dr, "," & dr)
                'Next
                Select Case e.ActionName
                    Case CommonMessage.ACTION_ACTIVE
                        result = rep.ActiveLocation(ActiveLocations, "A")
                    Case CommonMessage.ACTION_DEACTIVE
                        result = rep.ActiveLocation(ActiveLocations, "I")
                    Case CommonMessage.TOOLBARITEM_DELETE
                        For Each i As GridDataItem In rgLocation.SelectedItems
                            Dim _idLocation = i.GetDataKeyValue("ID")
                            'result = rep.DeleteLocationID(Location.ID)
                            result = rep.DeleteLocationID(_idLocation)
                        Next
                        'Case CommonMessage.TOOLBARITEM_DELETE
                        '    result = psp.Location_Delete(idList)
                End Select

                ActiveLocations = Nothing

                'ItemList = Nothing

                If result Then
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    rgLocation.Rebind()
                Else
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                End If
                CurrentState = CommonMessage.STATE_NORMAL
                UpdatePage()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub rgLocation_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgLocation.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            CreateDataFilter()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    'Private Sub rgLocation_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgLocation.NeedDataSource
    '    Dim rep As New ProfileRepository
    '    Dim repC As New CommonRepository
    '    Dim lstID As New List(Of Decimal)

    '    Dim lstOrgPermission As New List(Of Common.CommonBusiness.OrganizationDTO)

    '    If Common.Common.OrganizationLocationDataSession Is Nothing Then
    '        lstOrgPermission = repC.GetOrganizationLocationTreeView()
    '    Else
    '        lstOrgPermission = Common.Common.OrganizationLocationDataSession
    '    End If

    '    For Each org In lstOrgPermission
    '        lstID.Add(org.ID)
    '    Next

    '    Locations = rep.GetLocation("", lstID)
    '    Try
    '        rgLocation.VirtualItemCount = Locations.Count
    '        rgLocation.DataSource = Locations
    '    Catch ex As Exception
    '        Me.DisplayException(Me.ViewName, Me.ID, ex)
    '    End Try
    'End Sub

    Private Sub ctrlOrganization_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrganization.SelectedNodeChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            rgLocation.Rebind()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
#End Region

#Region "Custom"

    ''' <summary>
    ''' Định nghĩa ToolBar
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitToolBar()
        Try
            rgLocation.SetFilter()

            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            End If
            Common.Common.BuildToolbar(Me.MainToolBar,
                                 ToolbarItem.Create,
                                 ToolbarItem.Edit,
                                 ToolbarItem.Seperator,
                                 ToolbarItem.Save,
                                 ToolbarItem.Cancel,
                                 ToolbarItem.Active,
                                 ToolbarItem.Deactive,
                                 ToolbarItem.Export,
                                 ToolbarItem.Delete)
            CType(Me.MainToolBar.Items(3), RadToolBarButton).CausesValidation = True

            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <summary>
    ''' Định nghĩa Property cho tất cả Control
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitProperty()
        Try
            isLoadPopup = 0

            'Grid
            rgLocation.AllowPaging = True
            rgLocation.AllowCustomPaging = True
            rgLocation.PageSize = 50
            'rgLocation.ClientSettings.EnablePostBackOnRowClick = True
            rgLocation.MasterTableView.FilterExpression = ""

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    'Lấy danh sách Select cho sự kiện
    Protected Function RepareDataForAction() As List(Of LocationDTO)
        Dim lst As New List(Of LocationDTO)
        For Each dr As GridDataItem In rgLocation.SelectedItems
            Dim item As New LocationDTO
            item.ID = Decimal.Parse(dr("ID").Text)
            lst.Add(item)
        Next
        Return lst
    End Function

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New ProfileRepository
        Try
            ctrlOrganization.OrganizationType = OrganizationType.OrganizationLocation

            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgLocation.Rebind()
                        SelectedItemDataGridByKey(rgLocation, IDSelect, , rgLocation.CurrentPageIndex)

                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgLocation.CurrentPageIndex = 0
                        rgLocation.MasterTableView.SortExpressions.Clear()
                        rgLocation.Rebind()
                        SelectedItemDataGridByKey(rgLocation, IDSelect, )
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "Cancel"
                        rgLocation.MasterTableView.ClearSelectedItems()
                End Select
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region
End Class