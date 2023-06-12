Imports System.IO
Imports Common.CommonBusiness
Imports Framework.UI
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlDynamicControl
    Inherits CommonView

    ''' <summary>
    ''' ApproveProcessView
    ''' </summary>
    ''' <remarks></remarks>
    Protected WithEvents ApproveProcessView As ViewBase

    ''' <creator>HongDX</creator>
    ''' <lastupdate>21/06/2017</lastupdate>
    ''' <summary>Write Log</summary>
    ''' <remarks>_mylog, _pathLog, _classPath</remarks>
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Common/" + Me.GetType().Name.ToString()

#Region "Property"

    ''' <summary>
    ''' ApproveProcesss
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ApproveProcesss As List(Of ApproveProcessDTO)
        Get
            Return ViewState(Me.ID & "_ApproveProcesss")
        End Get

        Set(ByVal value As List(Of ApproveProcessDTO))
            ViewState(Me.ID & "_ApproveProcesss") = value
        End Set
    End Property

    ''' <summary>
    ''' DeleteApproveProcesss
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DeleteApproveProcesss As List(Of ApproveProcessDTO)
        Get
            Return ViewState(Me.ID & "_DeleteApproveProcesss")
        End Get

        Set(ByVal value As List(Of ApproveProcessDTO))
            ViewState(Me.ID & "_DeleteApproveProcesss") = value
        End Set
    End Property

    ''' <summary>
    ''' IDSelect
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IDSelect As Decimal
        Get
            Return ViewState(Me.ID & "_IDSelect")
        End Get

        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_IDSelect") = value
        End Set
    End Property

#End Region
#Region "Dynamic Control"
    Dim _listcontrol As New Dictionary(Of String, Object)
    Dim _dsView As New DataSet
    Sub LoadDynamicControl()
        Using DB As New CommonRepository
            Dim strDataView = DB.GetListControl("VIEW_STAFF").Rows(0)("DATAVIEW").ToString()
            _dsView.ReadXml(New StringReader(strDataView))

            For Each Item As DataRow In _dsView.Tables("ListControl").Rows
                tblControl.Rows().Add(New TableRow)
                Dim index = tblControl.Rows.Count - 1
                tblControl.Rows(index).Cells.Add(New TableCell)
                tblControl.Rows(index).Cells.Add(New TableCell)

                tblControl.Rows(index).Cells(0).Text = Item("Label").ToString()
                tblControl.Rows(index).Cells(0).CssClass = "lb"

                Dim a = New RadTextBox
                a.ID = Item("ID").ToString()
                a.MaxLength = 255

                tblControl.Rows(index).Cells(1).Controls.Add(a)
                _listcontrol.Add(Item("ID").ToString(), a)
            Next


            '

            'Dim txt = CType(_listcontrol(0), RadTextBox)
            'txt.Text = "hello"

            'Dim ds As New DataSet("DynamicView")
            'Dim dt As New DataTable("ViewInfo")
            'Dim obj As New StringWriter()

            'dt.Columns.Add("ViewName")
            'dt.Columns.Add("language")
            'dt.Columns.Add("ViewHeight")
            'dt.Columns.Add("ViewWidth")

            'dt.Rows.Add("Nhập thông tin nhân viên", "vi-VN", 100, 100)

            'Dim dtvar As New DataTable("ListControl")
            'dtvar.Columns.Add("ID")
            'dtvar.Columns.Add("Label")
            'dtvar.Columns.Add("TypeControl")
            'dtvar.Columns.Add("ControlWidth")

            'dtvar.Rows.Add("StaffName", "Tên nhân viên", "RadTextBox", 200)
            'dtvar.Rows.Add("StaffFamily", "Tên người thân nhân viên", "RadTextBox", 200)


            'ds.Tables.Add(dt)
            'ds.Tables.Add(dtvar)
            'ds.WriteXml(obj)
            'Dim sxml = obj.ToString()

            'Dim dsget As New DataSet
            'Dim reader As New StringReader(sxml)
            'dsget.ReadXml(reader)
            'Dim resule = DB.Insert_Edit_Dynamic_Control("VIEW_STAFF", sxml)



        End Using
    End Sub

#End Region

#Region "Page"

    ''' <lastupdate>25/07/2017</lastupdate>
    ''' <summary>Load page, trang thai page, trang thai control</summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            If Not IsPostBack Then
                Refresh()
            End If


            UpdateControlState()
            LoadDynamicControl()

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>25/07/2017</lastupdate>
    ''' <summary> Khoi tao control, set thuoc tinh cho grid </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            rgApproveProcess.AllowCustomPaging = True

            InitControl()

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>25/07/2017</lastupdate>
    ''' <summary>Khoi tao, load menu toolbar</summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            'Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarApproveProcesss

            Common.BuildToolbar(Me.MainToolBar,
                                        ToolbarItem.Create,
                                       ToolbarItem.Edit,
                                       ToolbarItem.Seperator,
                                       ToolbarItem.Save,
                                       ToolbarItem.Cancel)

            CType(MainToolBar.Items(2), RadToolBarButton).CausesValidation = True

            rgApproveProcess.ClientSettings.Selecting.EnableDragToSelectRows = False
            rgApproveProcess.ClientSettings.EnablePostBackOnRowClick = True
            rgApproveProcess.AllowMultiRowSelection = False

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>25/07/2017</lastupdate>
    ''' <summary>Khoi tao, load menu toolbar</summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim lstApproveProcess As List(Of ApproveProcessDTO)
        Dim rep As New CommonRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            lstApproveProcess = rep.GetApproveProcessList
            Me.ApproveProcesss = lstApproveProcess
            rgApproveProcess.Rebind()

            CurrentState = CommonMessage.STATE_NORMAL

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>25/07/2017</lastupdate>
    ''' <summary>
    ''' Load control, Khoi tao popup tim Ma Nhan Vien, Tim Don Vi To Chuc
    ''' Set Trang thai control
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim rep As New CommonRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            SetStatusToolbar(tbarApproveProcesss, CurrentState)

            Select Case CurrentState
                Case CommonMessage.STATE_NORMAL
                    rgApproveProcess.Enabled = True
                    'txtName.ReadOnly = True
                    'txtRequestDate.ReadOnly = True
                    'txtEmail.ReadOnly = True

                Case CommonMessage.STATE_EDIT
                    rgApproveProcess.Enabled = False
                    'txtName.ReadOnly = False
                    'txtRequestDate.ReadOnly = False
                    'txtEmail.ReadOnly = False
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"

    ''' <lastupdate>25/07/2017</lastupdate>
    ''' <summary> 
    ''' Event click item tren menu toolbar
    ''' Set trang thai control sau khi process xong 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim objApproveProcess As New ApproveProcessDTO





        Try
            Using rep As New CommonRepository
                Select Case CType(e.Item, RadToolBarButton).CommandName
                    Case CommonMessage.TOOLBARITEM_CREATE

                        CurrentState = CommonMessage.STATE_NEW
                        UpdateControlState()

                    Case CommonMessage.TOOLBARITEM_EDIT

                        'If rgApproveProcess.SelectedItems.Count = 0 Then
                        '    ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        '    Exit Sub
                        'End If

                        'If rgApproveProcess.SelectedItems.Count > 1 Then
                        '    ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        '    Exit Sub
                        'End If

                        'IDSelect = rgApproveProcess.SelectedValue

                        CurrentState = CommonMessage.STATE_EDIT
                        UpdateControlState()

                    Case CommonMessage.TOOLBARITEM_SAVE
                        If True Then 'If Page.IsValid Then

                            'objApproveProcess = rep.GetApproveProcess(IDSelect)

                            'objApproveProcess.NAME = _txtName.Text.Trim
                            'objApproveProcess.NUMREQUEST = 1 'txtRequestDate.Value
                            ''objApproveProcess.EMAIL = txtEmail.Text.Trim

                            ''objApproveProcess.ID = Decimal.Parse(hidID.Value)
                            'If rep.UpdateApproveProcess(objApproveProcess) Then
                            '    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                            'Else
                            '    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                            'End If
                            Dim listparam As New Dictionary(Of String, Object)
                            'CType(_listcontrol("txtname",RadTextBox)
                            For Each item In _listcontrol
                                Dim key = item.Key

                                Dim strtype = (From n In _dsView.Tables("ListControl").AsEnumerable()
                                               Where n("ID").ToString() = key
                                               Select n("TypeControl")).First().ToString()

                                Select Case strtype
                                    Case "RadTextBox"
                                        listparam.Add(key, CType(item.Value, RadTextBox).Text)
                                End Select



                            Next
                            Try
                                'rep.ExecuteDynamicProcedure("", listparam)
                            Catch ex As Exception

                            End Try


                            CurrentState = CommonMessage.STATE_NORMAL
                            Refresh()
                            UpdateControlState()
                        End If

                        'ClearControlValue(txtEmail, txtName)

                    Case CommonMessage.TOOLBARITEM_CANCEL
                        CurrentState = CommonMessage.STATE_NORMAL
                        UpdateControlState()
                        'ClearControlValue(txtEmail, txtName)
                        rgApproveProcess.MasterTableView.ClearSelectedItems()
                        Me.ExcuteScript("Resize", "ResizeSplitter")
                End Select
            End Using
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>25/07/2017</lastupdate>
    ''' <summary> 
    ''' Event xử lý trạng thái form khi postback page
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub ctrlDynamicControl_OnReceiveData(ByVal sender As IViewListener(Of ViewBase), ByVal e As ViewCommunicationEventArgs) Handles Me.OnReceiveData
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim mess As MessageDTO = CType(e.EventData, MessageDTO)

        Try
            Select Case e.FromViewID
                Case "ctrlDynamicControlNewEdit"
                    Select Case mess.Mess
                        Case CommonMessage.ACTION_INSERTED
                            CurrentState = CommonMessage.STATE_NORMAL
                            IDSelect = mess.ID
                            Refresh("InsertView")
                            UpdateControlState()

                        Case CommonMessage.ACTION_UPDATED
                            CurrentState = CommonMessage.STATE_NORMAL
                            IDSelect = mess.ID
                            Refresh("UpdateView")
                            UpdateControlState()

                        Case CommonMessage.ACTION_CANCEL
                            CurrentState = CommonMessage.STATE_NORMAL
                            UpdateControlState()
                    End Select
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>25/07/2017</lastupdate>
    ''' <summary>Load datasource cho grid</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgApproveProcess_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgApproveProcess.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim MaximumRows As Integer

        Try
            If Me.ApproveProcesss IsNot Nothing Then
                MaximumRows = ApproveProcesss.Count
                rgApproveProcess.VirtualItemCount = MaximumRows
                rgApproveProcess.DataSource = Me.ApproveProcesss
            End If

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>25/07/2017</lastupdate>
    ''' <summary>Load data vào textbox khi chọn checkbox ở grid</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgApproveProcess_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgApproveProcess.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            LoadDataToEdit()
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Custom"

    ''' <lastupdate>25/07/2017</lastupdate>
    ''' <summary>Load data tương ứng theo row ở grid </summary>
    ''' <remarks></remarks>
    Private Sub LoadDataToEdit()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New CommonRepository

        Try
            If rgApproveProcess.SelectedItems.Count <> 0 Then
                IDSelect = CType(rgApproveProcess.SelectedItems(0), GridDataItem).GetDataKeyValue("ID")

                Dim itemEdit = ApproveProcesss.SingleOrDefault(Function(p) p.ID = IDSelect)

                If itemEdit IsNot Nothing Then
                    'txtName.Text = itemEdit.NAME
                    'txtRequestDate.Value = itemEdit.NUMREQUEST
                    'txtEmail.Text = itemEdit.EMAIL
                End If
            Else
                'txtName.Text = ""
                'txtRequestDate.Value = 0
                'txtEmail.Text = ""
            End If

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>25/07/2017</lastupdate>
    ''' <summary>Thiết lập trạng thái của thanh Toolbar</summary>
    ''' <param name="_tbar"></param>
    ''' <param name="formState"></param>
    ''' <param name="stateToAll"></param>
    ''' <remarks></remarks>
    Private Sub SetStatusToolbar(ByVal _tbar As RadToolBar, ByVal formState As String, Optional ByVal stateToAll As Boolean? = Nothing)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            If stateToAll.HasValue Then
                For Each item As RadToolBarItem In _tbar.Items
                    item.Enabled = stateToAll.Value
                Next
                Exit Sub
            End If

            Select Case formState
                Case CommonMessage.STATE_NORMAL
                    For Each item As RadToolBarItem In _tbar.Items
                        Select Case CType(item, RadToolBarButton).CommandName
                            Case CommonMessage.TOOLBARITEM_CANCEL, CommonMessage.TOOLBARITEM_SAVE
                                item.Enabled = False
                            Case Else
                                item.Enabled = True
                        End Select
                    Next

                Case CommonMessage.STATE_EDIT, CommonMessage.STATE_NEW
                    For Each item As RadToolBarItem In _tbar.Items
                        Select Case CType(item, RadToolBarButton).CommandName
                            Case CommonMessage.TOOLBARITEM_CANCEL, CommonMessage.TOOLBARITEM_SAVE
                                item.Enabled = True
                            Case Else
                                item.Enabled = False
                        End Select
                    Next
            End Select

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

End Class