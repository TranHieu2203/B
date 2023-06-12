Imports Common.CommonMessage
Imports Framework.UI
Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic
Imports HistaffFrameworkPublic.FrameworkUtilities
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlValueSet
    Inherits CommonView
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Common/Secure/" + Me.GetType().Name.ToString()
    Protected WithEvents rCbb As RadComboBox

#Region "Property"
    Private repCPR As CommonProgramsRepository
    ''' <lastupdate>
    ''' 24/07/2017 14:54
    ''' </lastupdate>
    ''' <summary>
    ''' Mã popup
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property popupId As String
    ''' <lastupdate>
    ''' 24/07/2017 14:54
    ''' </lastupdate>
    ''' <summary>
    ''' Mã bản ghi được chọn
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property IDSelected As Decimal
        Get
            Return PageViewState(Me.ID & "_IDSelected")
        End Get
        Set(ByVal value As Decimal)
            PageViewState(Me.ID & "_IDSelected") = value
        End Set
    End Property
    ''' <lastupdate>
    ''' 24/07/2017 14:54
    ''' </lastupdate>
    ''' <summary>
    ''' Tên giá trị được định nghĩa
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property valueSetName As String
        Get
            Return PageViewState(Me.ID & "_valueSetName")
        End Get
        Set(ByVal value As String)
            PageViewState(Me.ID & "_valueSetName") = value
        End Set
    End Property
    ''' <lastupdate>
    ''' 24/07/2017 14:54
    ''' </lastupdate>
    ''' <summary>
    ''' data table giá trị được định nghĩa
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property dtbValueSet As DataTable
        Get
            Return PageViewState(Me.ID & "_dtbValueSet")
        End Get
        Set(ByVal value As DataTable)
            PageViewState(Me.ID & "_dtbValueSet") = value
        End Set
    End Property
    ''' <lastupdate>
    ''' 24/07/2017 14:54
    ''' </lastupdate>
    ''' <summary>
    ''' Data table loại giá trị được định nghĩa
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property dtbVSType As DataTable
        Get
            Return PageViewState(Me.ID & "_dtbVSType")
        End Get
        Set(ByVal value As DataTable)
            PageViewState(Me.ID & "_dtbVSType") = value
        End Set
    End Property
    ''' <lastupdate>
    ''' 24/07/2017 14:54
    ''' </lastupdate>
    ''' <summary>
    ''' data table loại dữ liệu
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property dtbDataType As DataTable
        Get
            Return PageViewState(Me.ID & "_dtbDataType")
        End Get
        Set(ByVal value As DataTable)
            PageViewState(Me.ID & "_dtbDataType") = value
        End Set
    End Property
    ''' <lastupdate>
    ''' 24/07/2017 14:54
    ''' </lastupdate>
    ''' <summary>
    ''' data table grid view các giá trị được định nghĩa
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property dtbGridViewValuSets As DataTable
        Get
            Return PageViewState(Me.ID & "_dtbGridViewValuSets")
        End Get
        Set(ByVal value As DataTable)
            PageViewState(Me.ID & "_dtbGridViewValuSets") = value
        End Set
    End Property

#End Region

#Region "Page"
    ''' <lastupdate>
    ''' 24/07/2017 14:54
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức khởi tạo các thiết lập, giá trị các control trên trang
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.MainToolBar = rtbMain
            Common.BuildToolbar(Me.MainToolBar,
                                ToolbarItem.Create, _
                                 ToolbarItem.Edit, _
                                 ToolbarItem.Seperator, _
                                 ToolbarItem.Save, _
                                 ToolbarItem.Cancel, _
                                 ToolbarItem.Seperator, _
                                 ToolbarItem.Seperator, _
                                 ToolbarItem.Search,
                                 ToolbarItem.Seperator, _
                                 ToolbarItem.Calculate)
            rtbMain.Items(9).Text = Translate("Định nghĩa chi tiết tham số")
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            Dim popup As RadWindow
            popup = CType(Me.Page, AjaxPage).PopupWindow
            popup.Title = "Thông tin value set"
            popupId = popup.ClientID
            _myLog.WriteLog(_myLog._info, _classPath, method,
                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try

    End Sub
    ''' <lastupdate>
    ''' 24/07/2017 14:54
    ''' </lastupdate>
    ''' <summary>
    ''' Hiển thị thông tin dữ liệu cho các control trên trang
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            rgValueSets.SetFilter()
            'LoadAllParameterInRequest()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                 CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <lastupdate>
    ''' 24/07/2017 14:54
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức BindData: cập nhật trạng thái hiện thời của trang
    ''' Cập nhật trạng thái của các control trên trang
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            'GetParams()
            CurrentState = CommonMessage.STATE_NORMAL
            UpdateControlState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                             CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try

    End Sub

#End Region

#Region "Event"
    ''' <lastupdate>
    ''' 24/07/2017 14:54
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện SelectedIndexChanged cho rad grid rgValueSets
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgValueSets_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgValueSets.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If rgValueSets.SelectedItems.Count > 0 Then
                FillDataAllCombobox()
                Dim slItem As GridDataItem
                slItem = rgValueSets.SelectedItems(0)
                IDSelected = Decimal.Parse(slItem("FLEX_VALUE_SET_ID").Text)
                rtbValueSetName.Text = slItem("FLEX_VALUE_SET_NAME").Text
                rtbVSDescr.Text = slItem("DESCRIPTION").Text
                rcbbVSType.SelectedValue = slItem("TYPE_ID").Text
                rcbbVS_DataType.SelectedValue = slItem("DATA_TYPE").Text
                rtbCode.Text = slItem("CODE").Text
                If slItem("TYPE").Text = "Loại câu truy vấn" Or slItem("TYPE").Text = "Loại tự xác định" Then
                    CType(Me.MainToolBar.Items(9), RadToolBarButton).Enabled = True
                Else
                    CType(Me.MainToolBar.Items(9), RadToolBarButton).Enabled = False
                End If
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                             CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try

    End Sub
    ''' <lastupdate>
    ''' 24/07/2017 14:54
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện Command OnMainToolbar khi click vào các item
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim rep As New HistaffFrameworkRepository
        Dim id As Decimal = -1
        Dim name As String = ""
        Dim desc As String = ""
        Dim type As String = ""
        Dim dataType As String = ""
        Dim code As String = ""
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW  'begin create program with all information in form
                    'setup any Radcombobox in form
                    FillDataAllCombobox()
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_EDIT
                    If rgValueSets.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rgValueSets.SelectedItems.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    CurrentState = CommonMessage.STATE_EDIT
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_SAVE
                    'Neu la New => insert vao AD_DESC_FLEX_COL vs id  = -1
                    'Neu la Edit => Update vao AD_DESC_FLEX_COL vs id = idSelected in gridview
                    Select Case CurrentState
                        Case CommonMessage.STATE_NEW
                            repCPR = New CommonProgramsRepository
                            'Lấy tất cả thông tin trên form và insert vào AD_PROGRAMS                               
                            id = -1
                            If rtbCode.Text.Substring(0, 1) <> "$" Then
                                ShowMessage("Mã bắt buộc phải có kí tự '$' !!! ", NotifyType.Warning)
                                Exit Sub
                            End If
                            If Check_VSName() Then
                                name = Trim(rtbValueSetName.Text)
                            Else
                                Exit Sub
                            End If
                            desc = rtbVSDescr.Text
                            type = rcbbVSType.SelectedValue
                            dataType = rcbbVS_DataType.SelectedValue
                            code = rtbCode.Text
                            'Process : Insert
                            Dim obj As Object = rep.ExecuteStoreScalar("PKG_HCM_SYSTEM.UPDATE_AD_FLEX_VALUE_SETS",
                                                                        New List(Of Object)(New Object() {id, name, desc, type, dataType, code, OUT_NUMBER}))

                            Dim check = Decimal.Parse(obj(0).ToString)
                            If check = 1 Then
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                                CurrentState = CommonMessage.STATE_NORMAL
                                UpdateControlState()
                                ClearAllInputControl()
                                rgValueSets.Rebind()
                            Else
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                                Exit Sub
                            End If
                        Case CommonMessage.STATE_EDIT
                            If rgValueSets.SelectedItems.Count > 0 Then
                                Dim slItem As GridDataItem
                                slItem = rgValueSets.SelectedItems(0)
                                id = Decimal.Parse(slItem("FLEX_VALUE_SET_ID").Text)
                            Else
                                ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                                Exit Sub
                            End If
                            repCPR = New CommonProgramsRepository
                            'Lấy tất cả thông tin trên form và update vào AD_FLEX_VALUE_SETS theo parameter da~ duoc selected     
                            name = Trim(rtbValueSetName.Text)
                            desc = rtbVSDescr.Text
                            type = rcbbVSType.SelectedValue
                            dataType = rcbbVS_DataType.SelectedValue
                            code = rtbCode.Text
                            'Process : Update
                            Dim obj As Object = rep.ExecuteStoreScalar("PKG_HCM_SYSTEM.UPDATE_AD_FLEX_VALUE_SETS",
                                                                        New List(Of Object)(New Object() {id, name, desc, type, dataType, code, OUT_NUMBER}))

                            Dim check = Decimal.Parse(obj(0).ToString)
                            If check = 1 Then
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                                CurrentState = CommonMessage.STATE_NORMAL
                                UpdateControlState()
                                ClearAllInputControl()
                                rgValueSets.Rebind()
                            Else
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                                Exit Sub
                            End If
                    End Select
                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    ClearAllInputControl()
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_SEARCH
                    CurrentState = CommonMessage.STATE_NORMAL
                    ClearAllInputControl()
                    UpdateControlState()
                    ClearRadGridFilter(rgValueSets)
                    rgValueSets.Rebind()
            End Select

            _myLog.WriteLog(_myLog._info, _classPath, method,
                             CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub rgValueSets_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgValueSets.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            FillDataToGridView()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                  CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            ShowMessage(ex.Message, Utilities.NotifyType.Error)
        End Try
    End Sub

#End Region

#Region "Custom"

    ''' <summary>
    ''' Fill dữ liệu lên tất cả control combobox trên form
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FillDataAllCombobox()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            dtbValueSet = New DataTable
            'get data in server
            '========================================================================='
            'Program type
            dtbValueSet = (New CommonProgramsRepository).FillComboboxValueSetType()
            rcbbVSType.DataSource = dtbValueSet
            rcbbVSType.DataTextField = "NAME"
            rcbbVSType.DataValueField = "ID"
            rcbbVSType.DataBind()
            rcbbVSType.Items.Insert(0, New RadComboBoxItem([String].Empty, [String].Empty))
            rcbbVSType.SelectedIndex = 0

            'Template type in
            dtbDataType = New DataTable
            dtbDataType = (New CommonProgramsRepository).FillComboboxValueSetTypeNone()
            rcbbVS_DataType.DataSource = dtbDataType
            rcbbVS_DataType.DataTextField = "NAME"
            rcbbVS_DataType.DataValueField = "ID"
            rcbbVS_DataType.DataBind()
            rcbbVS_DataType.Items.Insert(0, New RadComboBoxItem([String].Empty, [String].Empty))
            rcbbVS_DataType.SelectedIndex = 0
            '========================================================================'
            _myLog.WriteLog(_myLog._info, _classPath, method,
                             CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try

    End Sub

    ''' <summary>
    ''' Fill dữ liệu lên GridView Programs
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FillDataToGridView()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            '1. Khởi tạo DataSource cho GridView => tránh trường hợp DataSource bị null => GridView không create được
            Dim dtb As New DataTable
            dtb = CreateDataTable("AD_FLEX_VALUE_SETS")
            '2. Get dữ liệu từ đầu đó vào DataSoure              

            dtb = (New CommonProgramsRepository).FillGridViewValueSets("", "")

            '3. Fill DataSoure vào GridView
            If dtb IsNot Nothing Then
                rgValueSets.DataSource = dtb
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                             CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try

    End Sub

    Private Function CreateDataTable(ByVal tableName As String) As DataTable
        Dim dtb As New DataTable
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            dtb.TableName = tableName
            dtb.Columns.Add("FLEX_VALUE_SET_ID", GetType(Decimal))
            dtb.Columns.Add("FLEX_VALUE_SET_NAME", GetType(String))
            dtb.Columns.Add("DESCRIPTION", GetType(String))
            dtb.Columns.Add("TYPE_ID", GetType(String))
            dtb.Columns.Add("TYPE", GetType(String))
            dtb.Columns.Add("DATA_TYPE", GetType(Decimal))
            dtb.Columns.Add("CODE", GetType(String))
            _myLog.WriteLog(_myLog._info, _classPath, method,
                             CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try

        Return dtb
    End Function

    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CurrentState
                ''-----------Tab Nation----------------'
                Case STATE_NORMAL
                    EnabledGrid(rgValueSets, True)
                    rgValueSets.Enabled = True
                    rtbValueSetName.ReadOnly = True
                    rcbbVSType.Enabled = False
                    rcbbVS_DataType.Enabled = False
                    rtbVSDescr.ReadOnly = True
                    rtbCode.ReadOnly = True
                Case STATE_EDIT
                    EnabledGrid(rgValueSets, False, False)
                    rcbbVSType.Enabled = True
                    rcbbVS_DataType.Enabled = True
                    rtbValueSetName.ReadOnly = True
                    rtbVSDescr.ReadOnly = False
                    rtbCode.ReadOnly = True
                Case STATE_NEW
                    ClearAllInputControl()
                    EnabledGrid(rgValueSets, False, False)
                    rtbVSDescr.ReadOnly = False
                    rcbbVSType.Enabled = True
                    rcbbVS_DataType.Enabled = True
                    rtbValueSetName.ReadOnly = False
                    rtbCode.ReadOnly = False
            End Select
            ChangeToolbarState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                             CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub

    Protected Sub ClearAllInputControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            rtbValueSetName.Text = String.Empty
            rcbbVSType.ClearValue()
            rcbbVS_DataType.ClearValue()
            rtbVSDescr.Text = String.Empty
            rtbCode.Text = String.Empty
            rgValueSets.MasterTableView.ClearSelectedItems()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                 CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try

    End Sub

    Private Function Check_VSName()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim rep As New HistaffFrameworkRepository
            Dim obj As Object = rep.ExecuteStoreScalar("PKG_HCM_SYSTEM.CHECK_FLEX_VALUE_SETS",
                                                       New List(Of Object)(New Object() {rtbValueSetName.Text, rtbCode.Text, OUT_NUMBER}))

            Dim rs = Decimal.Parse(obj(0).ToString)
            If rs = 0 Then
                Return True
            ElseIf rs = 1 Then
                ShowMessage("Đã có tên value set này trong hệ thống hiện tại. đề nghị nhập khác !!! ", NotifyType.Warning)
                Return False
            Else
                ShowMessage("Đã có MÃ value set này trong hệ thống hiện tại. đề nghị nhập khác !!! ", NotifyType.Warning)
                Return False
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                             CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Return False
        End Try

    End Function

#End Region

End Class