Imports Common.CommonBusiness
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlApproveProcessTest
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
    Dim _classPath As String = "Common/ApproveProcess/" + Me.GetType().Name.ToString()

#Region "View config"
    Dim vcf As DataSet
    Sub ViewConfig()
        vcf = New DataSet
        Using rep = New CommonRepository
            vcf.ReadXml(New IO.StringReader(rep.GetConfigView("ctrlApproveProcessTest.ascx").Rows(0)("config_data").ToString()))

        End Using


        Dim lscontrol = (From n In vcf.Tables("control").AsEnumerable
                         Select New CommonBusiness.se_view_config_control_DTO With {
                                         .Ctl_ID = n.Field(Of String)("Ctl_ID").Trim(),
                                         .Label_ID = n.Field(Of String)("Label_ID").Trim(),
                                         .Validator_ID = n.Field(Of String)("Validator_ID").Trim(),
                                         .Label_text = n.Field(Of String)("Label_text").Trim(),
                                         .Is_Visible = Boolean.Parse(n("Is_Visible").ToString()),
                                         .Is_Validator = Boolean.Parse(n("Is_Validator").ToString()),
                                         .ErrorMessage = n.Field(Of String)("ErrorMessage").Trim(),
                                         .ErrorToolTip = n.Field(Of String)("ErrorToolTip").Trim()
                                     }).ToList

        'Dim Control = (From n In lscontrol
        '         Where n.Ctl_ID = "txtName"
        '         Select n).FirstOrDefault
        For Each Control In lscontrol
            For Each item In RadPane1.Controls
                Dim a = item.GetType
                Dim typecontrol = "RadTextBox"
                If a.Name = typecontrol Then
                    'Dim z = CTypeDynamic(item, Type.GetType("RadTextBox"))
                    Dim i = CType(item, Telerik.Web.UI.RadWebControl)
                    If i.ID = Control.Ctl_ID Then
                        i.Visible = Control.Is_Visible

                        Dim varlidator = CType(FindControl(Control.Validator_ID), BaseValidator)
                        varlidator.Enabled = Control.Is_Validator And Control.Is_Visible
                        varlidator.ErrorMessage = Control.ErrorMessage
                        varlidator.ToolTip = Control.ErrorToolTip

                        Dim lbl = CType(FindControl(Control.Label_ID), Label)
                        lbl.Text = Control.Label_text 'Translate(Control.Label_text)
                        'celllblName.Visible = False
                        'celltxtName.Visible = False
                        Exit For
                    End If
                End If
            Next
        Next
    End Sub
    Sub GirdConfig()

        Dim lsgirdcollum = (From n In vcf.Tables("girdColumm").AsEnumerable
                            Select New CommonBusiness.se_view_config_girdColumm_DTO With {
                             .ID = n.Field(Of String)("ID").Trim(),
                             .Name = n.Field(Of String)("Name").Trim(),
                             .Is_Visible = Boolean.Parse(n("Is_Visible").ToString()),
                             .Width = Integer.Parse(n("Width").ToString()),
                             .Orderby = Boolean.Parse(n("Orderby").ToString()),
                             .DataType = n.Field(Of String)("DataType").Trim()
                             }).ToList

        'rgApproveProcess.MasterTableView.Columns.Clear()
        ''rgApproveProcess.MasterTableView.DataKeyNames = "ID"

        'Dim colcb = New GridClientSelectColumn
        'colcb.UniqueName = "cbStatus"
        'colcb.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
        'colcb.HeaderStyle.Width = "30px"
        'colcb.ItemStyle.HorizontalAlign = HorizontalAlign.Center
        'rgApproveProcess.MasterTableView.Columns.Add(colcb)

        'Dim col = New GridBoundColumn
        'col.DataField = "ID"
        'col.UniqueName = "ID"
        'col.Visible = False
        'rgApproveProcess.MasterTableView.Columns.Add(col)

        'col = New GridBoundColumn
        'col.HeaderText = "Tên quy trình"
        'col.DataField = "NAME"
        'col.UniqueName = "NAME"
        'col.SortExpression = "NAME"
        'rgApproveProcess.MasterTableView.Columns.Add(col)

        'col = New GridBoundColumn
        'col.HeaderText = "Email thông báo"
        'col.DataField = "EMAIL"
        'col.UniqueName = "EMAIL"
        'col.SortExpression = "EMAIL"
        'rgApproveProcess.MasterTableView.Columns.Add(col)

        'For Each item In lsgirdcollum
        '    ' If item.DataType Then
        '    Dim col = New GridBoundColumn

        '    col.DataField = item.ID
        '    col.Visible = item.Is_Visible
        '    col.HeaderText = item.Name

        '    If item.Width > 0 Then
        '        col.HeaderStyle.Width = item.Width
        '    End If

        '    col.AllowFiltering = item.Orderby
        '    rgApproveProcess.MasterTableView.Columns.Add(col)
        '    ' End If
        'Next
    End Sub


#End Region

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
            ViewConfig()
            GirdConfig()

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
                    txtName.ReadOnly = True
                    'txtRequestDate.ReadOnly = True
                    txtEmail.ReadOnly = True

                Case CommonMessage.STATE_EDIT
                    rgApproveProcess.Enabled = False
                    txtName.ReadOnly = False
                    'txtRequestDate.ReadOnly = False
                    txtEmail.ReadOnly = False
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
        Dim rep As New CommonRepository
        Dim objApproveProcess As New ApproveProcessDTO

        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_EDIT
                    If rgApproveProcess.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    If rgApproveProcess.SelectedItems.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    'IDSelect = rgApproveProcess.SelectedValue

                    CurrentState = CommonMessage.STATE_EDIT
                    UpdateControlState()

                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        objApproveProcess = rep.GetApproveProcess(IDSelect)

                        objApproveProcess.NAME = txtName.Text.Trim
                        objApproveProcess.NUMREQUEST = 1 'txtRequestDate.Value
                        objApproveProcess.EMAIL = txtEmail.Text.Trim

                        'objApproveProcess.ID = Decimal.Parse(hidID.Value)
                        If rep.UpdateApproveProcess(objApproveProcess) Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                        End If

                        CurrentState = CommonMessage.STATE_NORMAL
                        Refresh()
                        UpdateControlState()
                    End If

                    ClearControlValue(txtEmail, txtName)

                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    UpdateControlState()
                    ClearControlValue(txtEmail, txtName)
                    rgApproveProcess.MasterTableView.ClearSelectedItems()
                    Me.ExcuteScript("Resize", "ResizeSplitter")
            End Select

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
    Public Sub ctrlApproveProcessTest_OnReceiveData(ByVal sender As IViewListener(Of ViewBase), ByVal e As ViewCommunicationEventArgs) Handles Me.OnReceiveData
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim mess As MessageDTO = CType(e.EventData, MessageDTO)

        Try
            Select Case e.FromViewID
                Case "ctrlApproveProcessTestNewEdit"
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
                    txtName.Text = itemEdit.NAME
                    'txtRequestDate.Value = itemEdit.NUMREQUEST
                    txtEmail.Text = itemEdit.EMAIL
                End If
            Else
                txtName.Text = ""
                'txtRequestDate.Value = 0
                txtEmail.Text = ""
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