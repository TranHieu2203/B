Imports Common.CommonMessage
Imports Framework.UI
Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic
Imports HistaffFrameworkPublic.FrameworkUtilities
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlValueSetT
    Inherits CommonView
    Protected WithEvents rCbb As RadComboBox
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Common/Secure/" + Me.GetType().Name.ToString()

#Region "Property"
    ''' <summary>
    ''' repCPR
    ''' </summary>
    ''' <remarks></remarks>
    Private repCPR As CommonProgramsRepository
    ''' <summary>
    ''' IDSelected
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
    ''' <summary>
    ''' valueSetName
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
    ''' <summary>
    ''' dtbTableView
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property dtbTableView As DataTable
        Get
            Return PageViewState(Me.ID & "_dtbTableView")
        End Get
        Set(ByVal value As DataTable)
            PageViewState(Me.ID & "_dtbTableView") = value
        End Set
    End Property
    ''' <summary>
    ''' dtbGridViewValuSets
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
    ''' 25/07/2017 9:42
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức khởi tạo ViewInit
    ''' thiết lập ban đầu cho các control trên trang
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
                                 ToolbarItem.Delete)
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                  CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try

    End Sub
    ''' <lastupdate>
    ''' 25/07/2017 9:42
    ''' </lastupdate>
    ''' <summary>
    ''' Hiển thị dữ liệu các control trên trang
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            rgValueSets.SetFilter()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                     CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try
        'LoadAllParameterInRequest()
    End Sub
    ''' <lastupdate>
    ''' 25/07/2017 9:42
    ''' </lastupdate>
    ''' <summary>
    ''' Đổ dữ liệu cho rtbValueSetName
    ''' Cập nhật lại trạng thái các control trên trang
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            GetParams()
            rtbValueSetName.Text = valueSetName
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
    ''' 25/07/2017 9:42
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện CheckedChanged của cbbCheck
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cbbCheck_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbbCheck.CheckedChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If CurrentState <> CommonMessage.STATE_NORMAL Then
                If cbbCheck.Checked Then
                    rcbbTableView.Enabled = True
                    rcbbColumnName.Enabled = True
                    rcbbIDColumn.Enabled = True
                    rtbTableView.ReadOnly = True
                    rtbColumnName.ReadOnly = True
                    FillDataAllCombobox()
                Else
                    rcbbTableView.Enabled = False
                    rcbbColumnName.Enabled = False
                    rcbbIDColumn.Enabled = False
                    rtbTableView.ReadOnly = False
                    rtbColumnName.ReadOnly = False
                End If
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                         CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try
    End Sub
    ''' <lastupdate>
    ''' 25/07/2017 10:16
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện selectedIndexChanged cho control rcbbTableView
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub rcbbTableView_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles rcbbTableView.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            'neu select thi lay cac column in table de show len rcbbColumnName
            Dim rep As New HistaffFrameworkRepository
            Dim ds = rep.ExecuteStore("PKG_HCM_SYSTEM.READ_ALL_COLUMN_TABLE",
                                        CreateParameterList(New With {.P_TABLE_NAME = rcbbTableView.Text, .P_CUR = OUT_CURSOR}))
            dtbTableView = ds.Tables(0)
            rcbbColumnName.DataSource = dtbTableView
            rcbbColumnName.DataTextField = "COLUMN_NAME"
            rcbbColumnName.DataValueField = "COLUMN_NAME"
            rcbbColumnName.DataBind()
            rcbbColumnName.ClearValue()
            rcbbColumnName.Items.Insert(0, New RadComboBoxItem("", String.Empty))

            rcbbIDColumn.DataSource = dtbTableView
            rcbbIDColumn.DataTextField = "COLUMN_NAME"
            rcbbIDColumn.DataValueField = "COLUMN_NAME"
            rcbbIDColumn.DataBind()
            rcbbIDColumn.ClearValue()
            rcbbIDColumn.Items.Insert(0, New RadComboBoxItem("", String.Empty))
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                          CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <lastupdate>
    ''' 25/07/2017 10:16
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện Click khi clicks vào rtbCheckSQL
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub rtbCheckSQL_Click(ByVal sender As Object, ByVal e As EventArgs) Handles rtbCheckSQL.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim sql As String = ""
            Dim rep As New HistaffFrameworkRepository
            If cbbCheck.Checked Then
                sql = "select " & rcbbIDColumn.Text & " ID, " & rcbbColumnName.Text & " CODE" & " from " & rcbbTableView.Text & IIf(rtbCondition.Text = "", "", " WHERE " & rtbCondition.Text)
            Else
                sql = "select " & rtbColumnName.Text & " from " & rtbTableView.Text & IIf(rtbCondition.Text = "", "", " WHERE " & rtbCondition.Text)
            End If
            Dim obj As Object = rep.ExecuteStoreScalar("PKG_HCM_SYSTEM.CHECK_SQL_STATEMENT",
                                                                            New List(Of Object)(New Object() {sql, OUT_NUMBER}))

            Dim check = Decimal.Parse(obj(0).ToString)
            If check = 1 Then
                ShowMessage("Câu truy vấn thực hiện KHÔNG mắc lỗi", NotifyType.Success)
            Else
                ShowMessage("Câu truy vấn thực hiện mắc lỗi !!!! Câu truy vấn hiện tại : " & sql, NotifyType.Error)
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                         CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try
    End Sub
    ''' <lastupdate>
    ''' 25/07/2017 10:16
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện selectedIndexChanged của control rgValueSets
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
                IDSelected = Decimal.Parse(slItem("ID").Text)
                If CType(slItem("TABLE_COUNT").Controls(0), CheckBox).Checked Then
                    UpdateControlState()
                    cbbCheck.Checked = True
                    rtbTableView.Text = ""
                    rtbColumnName.Text = ""
                    rcbbTableView.SelectedValue = slItem("TABLE_VIEW_SELECT").Text
                    Dim rep As New HistaffFrameworkRepository
                    Dim ds = rep.ExecuteStore("PKG_HCM_SYSTEM.READ_ALL_COLUMN_TABLE",
                                                CreateParameterList(New With {.P_TABLE_NAME = slItem("TABLE_VIEW_SELECT").Text, .P_CUR = OUT_CURSOR}))
                    dtbTableView = ds.Tables(0)
                    rcbbColumnName.DataSource = dtbTableView
                    rcbbColumnName.DataTextField = "COLUMN_NAME"
                    rcbbColumnName.DataValueField = "COLUMN_NAME"
                    rcbbColumnName.DataBind()
                    rcbbColumnName.ClearValue()

                    rcbbIDColumn.DataSource = dtbTableView
                    rcbbIDColumn.DataTextField = "COLUMN_NAME"
                    rcbbIDColumn.DataValueField = "COLUMN_NAME"
                    rcbbIDColumn.DataBind()
                    rcbbIDColumn.ClearValue()

                    rcbbIDColumn.SelectedValue = slItem("ID_COLUMN_NAME").Text
                    rcbbColumnName.SelectedValue = slItem("VALUE_COLUMN_NAME").Text
                Else
                    cbbCheck.Checked = CType(slItem("TABLE_COUNT").Controls(0), CheckBox).Checked
                    rtbTableView.ReadOnly = False
                    rtbTableView.Text = slItem("TABLE_VIEW_SELECT").Text
                    rtbColumnName.Text = slItem("VALUE_COLUMN_NAME").Text
                    UpdateControlState()
                End If
                rtbCondition.Text = slItem("CONDITION_WHERE_CLAUSE").Text
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                      CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try
    End Sub
    ''' <lastupdate>
    ''' 25/07/2017 10:16
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện command của control toolbar khi click vào toolbar 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim rep As New HistaffFrameworkRepository
        Dim id As Decimal = -1
        Dim tableView As String = ""
        Dim idcolumn As String = ""
        Dim columnName As String = ""
        Dim condition As String = ""
        Dim tableCount As Decimal
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    'kiem tra xem thu da co line nao hay chua?
                    'neu co roi thi show message
                    'Process : Insert
                    Dim obj As Object = rep.ExecuteStoreScalar("PKG_HCM_SYSTEM.CHECK_EXIST_VALUESET_T",
                               New List(Of Object)(New Object() {valueSetName, OUT_NUMBER}))
                    Dim check = Decimal.Parse(obj(0).ToString)
                    If check = 1 Then
                        ShowMessage("Không thể thêm thông tin cho value set này (Chú ý: Value set loại này chỉ được định nghĩa 1 line) !!!", NotifyType.Warning)
                        FillDataToGridView()
                    Else
                        CurrentState = CommonMessage.STATE_NEW  'begin create program with all information in form
                        FillDataAllCombobox()
                        UpdateControlState()
                    End If
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
                    'Neu la New => insert vao AD_FLEX_VALIDATION_TABLES vs id  = -1
                    'Neu la Edit => Update vao AD_FLEX_VALIDATION_TABLES vs id = idSelected in gridview
                    Select Case CurrentState
                        Case CommonMessage.STATE_NEW
                            'Dim sql As String = ""
                            'If cbbCheck.Checked Then
                            '    sql = "select " & rcbbIDColumn.Text & " ID, " & rcbbColumnName.Text & " CODE" & " from " & rcbbTableView.Text & " where 1 = 1 " & rtbCondition.Text
                            'Else
                            '    sql = "select " & rtbColumnName.Text & " from " & rtbTableView.Text & " where 1 = 1 " & rtbCondition.Text
                            'End If
                            'Dim objSQL As Object = rep.ExecuteStoreScalar("PKG_HCM_SYSTEM.CHECK_SQL_STATEMENT",
                            '                                                                New List(Of Object)(New Object() {sql, OUT_NUMBER}))

                            'Dim checkSQL = Decimal.Parse(objSQL(0).ToString)
                            'If checkSQL = 1 Then
                            id = -1
                            If cbbCheck.Checked Then  '1 table --> cbbTableView enable (get value)
                                tableView = rcbbTableView.SelectedValue
                                idcolumn = rcbbIDColumn.SelectedValue
                                columnName = rcbbColumnName.SelectedValue
                                tableCount = 1
                            Else
                                tableView = rtbTableView.Text
                                columnName = rtbColumnName.Text
                                idcolumn = ""
                                tableCount = 0
                            End If
                            condition = rtbCondition.Text

                            'Process : Insert
                            Dim obj As Object = rep.ExecuteStoreScalar("PKG_HCM_SYSTEM.UPDATE_AD_FLEX_VALID_TABLES",
                                                                        New List(Of Object)(New Object() {id, valueSetName, tableView, idcolumn, columnName, condition, tableCount, OUT_NUMBER}))

                            Dim check = Decimal.Parse(obj(0).ToString)
                            If check = 1 Then
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                                CurrentState = CommonMessage.STATE_NORMAL
                                UpdateControlState()
                                ClearAllInputControl()
                                rgValueSets.Rebind()
                            Else
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                                CurrentState = CommonMessage.STATE_NORMAL
                                Exit Sub
                            End If
                            'Else
                            '    ShowMessage("Câu truy vấn thực hiện mắc lỗi !!!! Đề nghị nhập lại", NotifyType.Error)
                            '    Exit Sub
                            'End If
                            'Lấy tất cả thông tin trên form và insert vào AD_FLEX_VALIDATION_TABLES                               

                        Case CommonMessage.STATE_EDIT
                            If rgValueSets.SelectedItems.Count = 1 Then
                                Dim slItem As GridDataItem
                                slItem = rgValueSets.SelectedItems(0)
                                id = Decimal.Parse(slItem("ID").Text)
                                'Dim sql As String = ""
                                'If cbbCheck.Checked Then
                                '    sql = "select " & rcbbIDColumn.Text & " ID, " & rcbbColumnName.Text & " CODE" & " from " & rcbbTableView.Text & " where 1 = 1 " & rtbCondition.Text
                                'Else
                                '    sql = "select " & rtbColumnName.Text & " from " & rtbTableView.Text & " where 1 = 1 " & rtbCondition.Text
                                'End If
                                'Dim objSQL As Object = rep.ExecuteStoreScalar("PKG_HCM_SYSTEM.CHECK_SQL_STATEMENT",
                                '                                                                New List(Of Object)(New Object() {sql, OUT_NUMBER}))

                                'Dim checkSQL = Decimal.Parse(objSQL(0).ToString)
                                'If checkSQL = 1 Then
                                '    ShowMessage("Câu truy vấn thực hiện KHÔNG mắc lỗi", NotifyType.Success)
                                If cbbCheck.Checked Then  '1 table --> cbbTableView enable (get value)
                                    tableView = rcbbTableView.SelectedValue
                                    idcolumn = rcbbIDColumn.SelectedValue
                                    columnName = rcbbColumnName.SelectedValue
                                    tableCount = 1
                                Else
                                    tableView = rtbTableView.Text
                                    columnName = rtbColumnName.Text
                                    idcolumn = ""
                                    tableCount = 0
                                End If
                                condition = rtbCondition.Text

                                'Process : Insert
                                Dim obj As Object = rep.ExecuteStoreScalar("PKG_HCM_SYSTEM.UPDATE_AD_FLEX_VALID_TABLES",
                                                                            New List(Of Object)(New Object() {id, valueSetName, tableView, idcolumn, columnName, condition, tableCount, OUT_NUMBER}))

                                Dim check = Decimal.Parse(obj(0).ToString)
                                If check = 1 Then
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    UpdateControlState()
                                    ClearAllInputControl()
                                    rgValueSets.Rebind()
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    Exit Sub
                                End If
                                'Else
                                '    ShowMessage("Câu truy vấn thực hiện mắc lỗi !!!! Đề nghị nhập lại", NotifyType.Error)
                                '    Exit Sub
                                'End If
                            Else
                                ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                            End If
                    End Select
                    CurrentState = CommonMessage.STATE_NORMAL
                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    ClearAllInputControl()
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_DELETE
                    'CALL FUNCTION DELETE
                    If rgValueSets.SelectedItems.Count > 0 Then
                        Dim slItem As GridDataItem
                        slItem = rgValueSets.SelectedItems(0)
                        IDSelected = Decimal.Parse(slItem("ID").Text)
                        ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                        ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                        ctrlMessageBox.DataBind()
                        ctrlMessageBox.Show()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                    End If
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                          CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 25/07/2017 10:16
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện NeedDataSource của control rgValueSet
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
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

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New HistaffFrameworkRepository
        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Dim db As New CommonRepository

                Dim obj As Object = rep.ExecuteStoreScalar("PKG_HCM_SYSTEM.DELETE_AD_FLEX_VALID_TABLES",
                                                                        New List(Of Object)(New Object() {IDSelected, OUT_NUMBER}))

                Dim check = Decimal.Parse(obj(0).ToString)
                If check = 1 Then
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    CurrentState = CommonMessage.STATE_NORMAL
                    UpdateControlState()
                    ClearAllInputControl()
                    rgValueSets.Rebind()
                Else
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                    CurrentState = CommonMessage.STATE_NORMAL
                    Exit Sub
                End If

            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                             CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

#End Region

#Region "Custom"
    ''' <lastupdate>
    ''' 25/07/2017 10:16
    ''' </lastupdate>
    ''' <summary>
    ''' Fill dữ liệu lên tất cả control combobox trên form
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FillDataAllCombobox()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            dtbTableView = New DataTable
            Dim rep As New HistaffFrameworkRepository
            'get data in server
            Dim ds = rep.ExecuteStore("PKG_HCM_SYSTEM.READ_ALL_TABLE_VIEW",
                                        CreateParameterList(New With {.P_CUR = OUT_CURSOR}))
            dtbTableView = ds.Tables(0)
            rcbbTableView.DataSource = dtbTableView
            rcbbTableView.DataTextField = "NAME"
            rcbbTableView.DataValueField = "NAME"
            rcbbTableView.DataBind()
            rcbbTableView.Items.Insert(0, New RadComboBoxItem([String].Empty, [String].Empty))
            rcbbTableView.SelectedIndex = 0
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                  CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <lastupdate>
    ''' 25/07/2017 10:16
    ''' </lastupdate>
    ''' <summary>
    ''' Fill dữ liệu lên GridView Programs
    ''' Nếu gọi từ NeedDataSource thì không cần
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FillDataToGridView()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            '1. Khởi tạo DataSource cho GridView => tránh trường hợp DataSource bị null => GridView không create được
            Dim dtb As New DataTable
            Dim rep As New HistaffFrameworkRepository
            dtb = CreateDataTable("AD_FLEX_VALUES")
            '2. Get dữ liệu từ đầu đó vào DataSoure              

            Dim ds = rep.ExecuteStore("PKG_HCM_SYSTEM.READ_FLEX_VALIDATION_TABLES",
                                        CreateParameterList(New With {.P_CS_NAME = valueSetName, .P_CUR = OUT_CURSOR}))
            dtb = ds.Tables(0)
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
    ''' <lastupdate>
    ''' 25/07/2017 10:16
    ''' </lastupdate>
    ''' <summary>
    ''' Tạo dữ liệu cho table
    ''' </summary>
    ''' <param name="tableName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CreateDataTable(ByVal tableName As String) As DataTable
        Dim dtb As New DataTable
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            dtb.TableName = tableName
            dtb.Columns.Add("ID", GetType(Decimal))
            dtb.Columns.Add("FLEX_VALUE_SET_NAME", GetType(String))
            dtb.Columns.Add("TABLE_VIEW_SELECT", GetType(String))
            dtb.Columns.Add("ID_COLUMN_NAME", GetType(String))
            dtb.Columns.Add("VALUE_COLUMN_NAME", GetType(String))
            dtb.Columns.Add("CONDITION_WHERE_CLAUSE", GetType(String))
            dtb.Columns.Add("TABLE_COUNT", GetType(Decimal))
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
        Return dtb
    End Function
    ''' <lastupdate>
    ''' 25/07/2017 10:16
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức cập nhật trạng thái các control trên trang
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CurrentState
                ''-----------Tab Nation----------------'
                Case STATE_NORMAL
                    EnabledGrid(rgValueSets, True, True)
                    cbbCheck.Checked = False
                    rtbCheckSQL.Enabled = False
                    rcbbTableView.Enabled = False
                    rcbbColumnName.Enabled = False
                    rcbbIDColumn.Enabled = False
                    cbbCheck.Enabled = False
                    rtbTableView.ReadOnly = True
                    rtbCondition.ReadOnly = True
                    rtbColumnName.ReadOnly = True
                Case STATE_EDIT
                    EnabledGrid(rgValueSets, False, False)
                    cbbCheck.Enabled = True
                    rtbCheckSQL.Enabled = True
                    If cbbCheck.Checked Then
                        rcbbTableView.Enabled = True
                        rcbbColumnName.Enabled = True
                        rcbbIDColumn.Enabled = True
                        rtbTableView.ReadOnly = True
                        rtbColumnName.ReadOnly = True
                    Else
                        rcbbTableView.Enabled = False
                        rcbbColumnName.Enabled = False
                        rcbbIDColumn.Enabled = False
                        rtbTableView.ReadOnly = False
                        rtbColumnName.ReadOnly = False
                    End If
                    rtbCondition.ReadOnly = False
                Case STATE_NEW
                    EnabledGrid(rgValueSets, False, False)
                    cbbCheck.Enabled = True
                    rtbCheckSQL.Enabled = True
                    ClearAllInputControl()
                    rcbbTableView.Enabled = False
                    rcbbColumnName.Enabled = False
                    rcbbIDColumn.Enabled = False
                    rtbCondition.ReadOnly = False
                    rtbTableView.ReadOnly = False
                    rtbColumnName.ReadOnly = False
            End Select
            ChangeToolbarState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                   CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub
    ''' <lastupdate>
    ''' 25/07/2017 10:16
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức xóa bỏ dữ liệu các control về trạng thái mặc định
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub ClearAllInputControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            cbbCheck.Checked = False
            rcbbTableView.ClearValue()
            rcbbColumnName.ClearValue()
            rcbbIDColumn.ClearValue()
            rtbTableView.Text = String.Empty
            rtbCondition.Text = String.Empty
            rtbColumnName.Text = String.Empty
            rgValueSets.MasterTableView.ClearSelectedItems()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                  CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 25/07/2017 10:16
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức lấy toàn bộ params
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetParams()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If CurrentState Is Nothing Then
                If Request.Params("vsName") IsNot Nothing Then
                    valueSetName = Request.Params("vsName")
                End If
            End If

            _myLog.WriteLog(_myLog._info, _classPath, method,
                                   CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub

#End Region


End Class