Imports Common.CommonBusiness
Imports Framework.UI
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlGroupReport
    Inherits CommonView
    Protected WithEvents UserView As ViewBase
    Property GroupInfo As CommonBusiness.GroupDTO
    Public Overrides Property MustAuthorize As Boolean = False
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Common/Modules/Common/Secure/" + Me.GetType().Name.ToString()

#Region "Property"

    ''' <summary>
    ''' Obj GroupID_Old
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property GroupID_Old As Decimal
        Get
            Return PageViewState(Me.ID & "_GroupID_Old")
        End Get
        Set(ByVal value As Decimal)
            PageViewState(Me.ID & "_GroupID_Old") = value
        End Set
    End Property

    ''' <summary>
    ''' Obj ListReports
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ListReports As List(Of GroupReportDTO)
        Get
            Return PageViewState(Me.ID & "_ListReports")
        End Get
        Set(ByVal value As List(Of GroupReportDTO))
            PageViewState(Me.ID & "_ListReports") = value
        End Set
    End Property

    ''' <summary>
    ''' Obj ListModules
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ListModules As List(Of ModuleDTO)
        Get
            Return PageViewState(Me.ID & "_ListModules")
        End Get
        Set(ByVal value As List(Of ModuleDTO))
            PageViewState(Me.ID & "_ListModules") = value
        End Set
    End Property

    ''' <summary>
    ''' Obj CheckState
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CheckState As Boolean
        Get
            Return PageViewState(Me.ID & "_CheckState")
        End Get
        Set(ByVal value As Boolean)
            PageViewState(Me.ID & "_CheckState") = value
        End Set
    End Property

    ''' <summary>
    ''' Obj SelectedItem
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SelectedItem As List(Of Decimal)
        Get
            If PageViewState(Me.ID & "_SelectedItem") Is Nothing Then
                PageViewState(Me.ID & "_SelectedItem") = New List(Of Decimal)
            End If
            Return PageViewState(Me.ID & "_SelectedItem")
        End Get
        Set(ByVal value As List(Of Decimal))
            PageViewState(Me.ID & "_SelectedItem") = value
        End Set
    End Property
#End Region

#Region "Page"

    ''' <lastupdate>
    ''' 26/07/2017 11:00
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
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            End If
            UpdateControlState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 26/07/2017 11:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức khởi tạo dữ liệu cho các control trên trang
    ''' Tạo các command cho màn hình gồm sửa, lưu, hủy
    ''' Gọi phương thức khởi tạo 
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.MainToolBar = rtbMain
            Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Edit,
                                       ToolbarItem.Seperator, _
                                       ToolbarItem.Save, _
                                       ToolbarItem.Cancel)

            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 26/07/2017 11:00
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc lam moi cac control theo state
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub UpdateControlStatus()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CurrentState
                Case CommonMessage.STATE_EDIT
                    txtREPORT_NAME.Enabled = False
                    cboMODULE.Enabled = False
                    btnFIND.Enabled = False
                    Me.MainToolBar.Items(0).Enabled = False
                    Me.MainToolBar.Items(2).Enabled = True
                    Me.MainToolBar.Items(3).Enabled = True
                Case CommonMessage.STATE_NORMAL, ""
                    If GroupInfo Is Nothing Then
                        Me.MainToolBar.Enabled = False
                        btnFIND.Enabled = False
                    Else
                        Me.MainToolBar.Enabled = True
                        btnFIND.Enabled = True
                    End If
                    txtREPORT_NAME.Enabled = True
                    cboMODULE.Enabled = True
                    Me.MainToolBar.Items(0).Enabled = True
                    Me.MainToolBar.Items(2).Enabled = False
                    Me.MainToolBar.Items(3).Enabled = False
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 26/07/2017 11:00
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc lam moi cac grid và các checkbox trong grid theo state
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub UpdateGridState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CurrentState
                Case CommonMessage.STATE_EDIT
                    For Each dr As Telerik.Web.UI.GridDataItem In rgGrid.Items
                        If dr("IS_USE").Controls.Count > 0 Then
                            Dim chk As CheckBox = DirectCast(dr("IS_USE").Controls(1), CheckBox)
                            If chk IsNot Nothing Then
                                chk.Enabled = True
                            End If
                        End If
                    Next
                Case CommonMessage.STATE_NORMAL, ""
                    For Each dr As Telerik.Web.UI.GridDataItem In rgGrid.Items
                        If dr("IS_USE").Controls.Count > 0 Then
                            Dim chk As CheckBox = DirectCast(dr("IS_USE").Controls(1), CheckBox)
                            If chk IsNot Nothing Then
                                chk.Enabled = False
                            End If
                        End If
                    Next
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 26/07/2017 11:00
    ''' </lastupdate>
    ''' <summary>
    ''' Gọi 2 hàm update control và update grid
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            UpdateControlStatus()
            UpdateGridState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                     CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 26/07/2017 11:00
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
            'Load Combobox
            Dim rep As New CommonRepository
            If ListModules Is Nothing Then
                ListModules = rep.GetModuleList()
            End If
            Dim dtbModule = (New CommonProgramsRepository).FillComboboxModules()
            ListModules.Clear()
            For Each item As DataRow In dtbModule.Rows
                Dim mo As New ModuleDTO
                mo.ID = item("ID")
                mo.NAME = item("NAME")
                ListModules.Add(mo)
            Next
            Utilities.FillDropDownList(cboMODULE, ListModules, "NAME", "ID", Common.SystemLanguage)
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                      CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 26/07/2017 11:00
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc lam moi cac control theo tuy chon Message, 
    ''' Message co gia tri default la ""
    ''' Bind lai du lieu cho grid rgGrid
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            rgGrid.Rebind()
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
    ''' 26/07/2017 11:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien command khi click item tren toolbar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_EDIT
                    CurrentState = CommonMessage.STATE_EDIT
                    rgGrid.Rebind()
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_SAVE
                    Dim lst As List(Of GroupReportDTO)
                    Dim rep As New CommonRepository
                    lst = RepareDataForUpdate()
                    If rep.UpdateGroupReport(GroupInfo.ID, lst) Then
                        Me.ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                    Else
                        Me.ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Warning)
                    End If
                    CurrentState = CommonMessage.STATE_NORMAL
                    rgGrid.Rebind()
                    UpdateControlState()

                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    rgGrid.Rebind()
                    UpdateControlState()

            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 26/07/2017 11:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien OnReceiveData cua control ctrlUser
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub ctrlUser_OnReceiveData(ByVal sender As IViewListener(Of ViewBase), ByVal e As ViewCommunicationEventArgs) Handles Me.OnReceiveData
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                       CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 26/07/2017 11:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien ItemDataBound cua control rgGrid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgGrid_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgGrid.ItemDataBound
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If e.Item.ItemType = GridItemType.Header Then
                Dim _chk As CheckBox = DirectCast(DirectCast(e.Item, GridHeaderItem)("IS_USE").FindControl("cbSELECT_ALL"), CheckBox)
                If _chk IsNot Nothing Then
                    _chk.Checked = CheckState
                    Select Case CurrentState
                        Case CommonMessage.STATE_EDIT
                            _chk.Enabled = True
                        Case Else
                            _chk.Enabled = False
                    End Select
                End If
            ElseIf e.Item.ItemType = GridItemType.AlternatingItem Or e.Item.ItemType = GridItemType.Item Then
                Dim _chk As CheckBox = DirectCast(DirectCast(e.Item, GridDataItem)("IS_USE").FindControl("cbIS_USE"), CheckBox)
                If _chk IsNot Nothing Then
                    Select Case CurrentState
                        Case CommonMessage.STATE_EDIT
                            _chk.Enabled = True
                        Case Else
                            _chk.Enabled = False
                    End Select
                End If
                Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
                Dim id As Decimal = Decimal.Parse(datarow("ID").Text)
                If SelectedItem IsNot Nothing AndAlso SelectedItem.Contains(id) Then
                    _chk.Checked = True
                End If
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 26/07/2017 11:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien NeedDataSource cua control rgGrid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgGrid_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgGrid.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim rep As New CommonRepository
            Dim _filter As New GroupReportDTO
            If GroupInfo Is Nothing Then Exit Sub
            _filter.REPORT_NAME = txtREPORT_NAME.Text
            If cboMODULE.SelectedValue <> "" Then
                _filter.MODULE_ID = Decimal.Parse(cboMODULE.SelectedValue)
            End If

            ListReports = rep.GetGroupReportListFilter(GroupInfo.ID, _filter)
            rgGrid.DataSource = ListReports
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 26/07/2017 11:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien PageIndexChanged cua control rgGrid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgGrid_PageIndexChanged(ByVal sender As Object, ByVal e As GridPageChangedEventArgs) Handles rgGrid.PageIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            SaveGridChecked()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                     CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 26/07/2017 11:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien click cua control btnFIND
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnFIND_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFIND.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            rgGrid.Rebind()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                     CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 26/07/2017 11:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien CheckChanged cua control CheckBox
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub CheckBox_CheckChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim _check As CheckBox = DirectCast(sender, CheckBox)
            CheckState = _check.Checked
            If CurrentState = CommonMessage.STATE_EDIT Then
                For Each dr As Telerik.Web.UI.GridDataItem In rgGrid.Items
                    If dr("IS_USE").Controls.Count > 0 Then
                        Dim chk As CheckBox = DirectCast(dr("IS_USE").Controls(1), CheckBox)
                        If chk IsNot Nothing Then
                            chk.Checked = _check.Checked
                        End If
                    End If
                Next
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                     CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Custom"

    ''' <lastupdate>
    ''' 26/07/2017 11:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý add các row được select vào list
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub SaveGridChecked()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            For Each dr As Telerik.Web.UI.GridDataItem In rgGrid.Items
                Dim id As Decimal = Decimal.Parse(dr("ID").Text)
                Dim _chk As CheckBox = CType(dr.FindControl("cbIS_USE"), CheckBox)
                If _chk IsNot Nothing Then
                    If _chk.Checked Then
                        If Not SelectedItem.Contains(id) Then SelectedItem.Add(id)
                    Else
                        If SelectedItem.Contains(id) Then SelectedItem.Remove(id)
                    End If
                End If
            Next
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                     CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 26/07/2017 11:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện load trạng thái của checkbox để chuẩn bị update
    ''' </summary>
    ''' <remarks></remarks>
    Protected Function RepareDataForUpdate() As List(Of GroupReportDTO)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim lst As New List(Of GroupReportDTO)
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            SaveGridChecked()
            For Each dr As Decimal In SelectedItem
                Dim ReportID As Decimal = dr
                Dim intCount As Integer = (From p In lst Where p.ID = ReportID).Count
                If intCount = 0 Then
                    Dim _new As New GroupReportDTO With {.ID = ReportID}
                    _new.IS_USE = True
                    lst.Add(_new)
                End If
            Next
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
        Return lst
    End Function

#End Region
End Class