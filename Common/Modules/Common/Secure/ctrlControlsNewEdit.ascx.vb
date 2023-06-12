Imports System.IO
Imports Common.CommonBusiness
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlControlsNewEdit
    Inherits CommonView
    Protected WithEvents UserView As ViewBase
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Common/Modules/Common/Secure/" + Me.GetType().Name.ToString()

#Region "Property"
    Property IDSelect As String
        Get
            Return ViewState(Me.ID & "_IDSelect")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_IDSelect") = value
        End Set
    End Property
#End Region

#Region "Page"

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            GetParams()
            Refresh()
            If Cache("ctrlControlsNewEdit_" & IDSelect & "_IsSuccess") IsNot Nothing Then
                ShowMessage(Translate("Chỉnh sửa thành công !"), NotifyType.Success)
                Cache.Remove("ctrlControlsNewEdit_" & IDSelect & "_IsSuccess")
            End If

            'Insert logs
            Dim startTime As DateTime = DateTime.UtcNow
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = rtbMainToolBar
            Common.BuildToolbar(Me.MainToolBar,
                                ToolbarItem.Save, _
                                ToolbarItem.Cancel)
            Me.MainToolBar.OnClientButtonClicking = "clientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"

            'Insert logs
            Dim startTime As DateTime = DateTime.UtcNow
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim _filter As New FunctionDTO
            Dim _lstFunc As New List(Of FunctionDTO)
            Dim dtData As New DataTable
            Using rep As New CommonRepository
                _lstFunc = rep.Get_FunctionWithControl_List(_filter)
                dtData = _lstFunc.ToTable
                Dim rw_first As DataRow = dtData.NewRow
                rw_first("NAME") = ""
                rw_first("FID") = "0"
                dtData.Rows.InsertAt(rw_first, 0)
                FillRadCombobox(cboFunctionName, dtData, "NAME", "FID", False)
            End Using

            'Insert logs
            Dim startTime As DateTime = DateTime.UtcNow
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If IDSelect <> "" Then
                Dim vcf = New DataSet
                Using rep = New CommonRepository
                    vcf.ReadXml(New IO.StringReader(rep.GetConfigView(IDSelect).Rows(0)("config_data").ToString()))
                End Using

                Dim dtCtrl As DataTable = vcf.Tables("control")
                Dim dtGrid As DataTable = vcf.Tables("girdColumm")

                'Controls
                If dtCtrl IsNot Nothing AndAlso dtCtrl.Rows.Count > 0 Then
                    rgListControls.DataSource = dtCtrl
                    rgListControls.DataBind()
                    For Each item As GridDataItem In rgListControls.MasterTableView.Items
                        item.Edit = True
                    Next
                    rgListControls.MasterTableView.Rebind()
                End If

                'Grids
                If dtGrid IsNot Nothing AndAlso dtGrid.Rows.Count > 0 Then
                    dtGrid.DefaultView.Sort = "Orderby ASC"
                    dtGrid = dtGrid.DefaultView.ToTable
                    rgListGrid.DataSource = dtGrid
                    rgListGrid.DataBind()
                    For Each item As GridDataItem In rgListGrid.MasterTableView.Items
                        item.Edit = True
                    Next
                    rgListGrid.MasterTableView.Rebind()
                End If
            Else
                rgListControls.DataSource = New DataTable
                rgListControls.DataBind()
                rgListControls.MasterTableView.Rebind()

                rgListGrid.DataSource = New DataTable
                rgListGrid.DataBind()
                rgListGrid.MasterTableView.Rebind()
            End If

            'Insert logs
            Dim startTime As DateTime = DateTime.UtcNow
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            InitControl()

            'Insert logs
            Dim startTime As DateTime = DateTime.UtcNow
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Event"

    ''' <summary>
    ''' Xử lý sự kiện Buttons
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim rep As New CommonRepository
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE

                    If IDSelect = "" Then
                        ShowMessage(Translate("Chưa chọn Tên chức năng để lưu. Vui lòng chọn lại !"), NotifyType.Alert)
                        Exit Sub
                    End If

                    Dim ds As New DataSet("viewconfig")

                    'Controls
                    Dim dtCtrl As New DataTable("control")
                    dtCtrl.Columns.Add("Ctl_ID", GetType(String))
                    dtCtrl.Columns.Add("Label_ID", GetType(String))
                    dtCtrl.Columns.Add("Label_text", GetType(String))
                    dtCtrl.Columns.Add("Is_Visible", GetType(Boolean))
                    dtCtrl.Columns.Add("Is_Validator", GetType(Boolean))
                    dtCtrl.Columns.Add("ErrorMessage", GetType(String))
                    dtCtrl.Columns.Add("ErrorToolTip", GetType(String))
                    dtCtrl.Columns.Add("Validator_ID", GetType(String))
                    For Each item_Ctrl As GridDataItem In rgListControls.EditItems
                        Dim rw_new As DataRow = dtCtrl.NewRow
                        Dim edit = CType(item_Ctrl, GridEditableItem)
                        rw_new("Ctl_ID") = CType(edit("Ctl_ID").Controls(0), TextBox).Text
                        rw_new("Label_ID") = CType(edit("Label_ID").Controls(0), TextBox).Text
                        rw_new("Label_text") = CType(edit("Label_text").Controls(0), TextBox).Text
                        rw_new("Is_Visible") = If(CType(edit("Is_Visible").Controls(0), CheckBox).Checked, True, False)
                        rw_new("Is_Validator") = If(CType(edit("Is_Validator").Controls(0), CheckBox).Checked, True, False)
                        rw_new("ErrorMessage") = CType(edit("ErrorMessage").Controls(0), TextBox).Text
                        rw_new("ErrorToolTip") = CType(edit("ErrorTooltip").Controls(0), TextBox).Text
                        rw_new("Validator_ID") = CType(edit("Validator_ID").Controls(0), TextBox).Text
                        dtCtrl.Rows.Add(rw_new)
                    Next

                    'Grids
                    Dim dtGrid As New DataTable("girdColumm")
                    dtGrid.Columns.Add("ID", GetType(String))
                    dtGrid.Columns.Add("Name", GetType(String))
                    dtGrid.Columns.Add("Is_Visible", GetType(Boolean))
                    dtGrid.Columns.Add("Width", GetType(Integer))
                    dtGrid.Columns.Add("Orderby", GetType(Integer))
                    dtGrid.Columns.Add("DataType", GetType(String))
                    For Each item_Grid As GridDataItem In rgListGrid.EditItems
                        Try
                            Dim rw_new As DataRow = dtGrid.NewRow
                            Dim edit = CType(item_Grid, GridEditableItem)
                            rw_new("ID") = CType(edit("ID").Controls(0), TextBox).Text
                            rw_new("Name") = CType(edit("Name").Controls(0), TextBox).Text
                            rw_new("DataType") = CType(edit("DataType").Controls(0), TextBox).Text
                            rw_new("Is_Visible") = If(CType(edit("Is_Visible").Controls(0), CheckBox).Checked, True, False)
                            rw_new("Width") = CType(edit("Width").Controls(0), TextBox).Text
                            If IsNumeric(rw_new("Width")) = False Then
                                ShowMessage(Translate("Nhập sai định dạng. Vui lòng nhập lại !"), NotifyType.Warning)
                                Exit Sub
                            End If
                            rw_new("Orderby") = CType(edit("OrderBy").Controls(0), TextBox).Text
                            If IsNumeric(rw_new("Orderby")) = False Then
                                ShowMessage(Translate("Nhập sai định dạng. Vui lòng nhập lại !"), NotifyType.Warning)
                                Exit Sub
                            End If
                            dtGrid.Rows.Add(rw_new)
                        Catch ex As Exception
                            Throw ex
                        End Try
                    Next

                    'Add DtTable into DtSet
                    ds.Tables.Add(dtCtrl)
                    ds.Tables.Add(dtGrid)

                    Dim sw As New StringWriter()
                    Dim DocXml As String = String.Empty
                    ds.WriteXml(sw, False)
                    DocXml = sw.ToString

                    If rep.Insert_Update_Control_List(IDSelect, DocXml) = True Then
                        CacheManager.ClearValue("CONFIG" + IDSelect) ' clear cache config grid neu co
                        'ShowMessage(Translate("Chỉnh sửa thành công !"), NotifyType.Success)
                        'Threading.Thread.Sleep(3000)
                        Cache("ctrlControlsNewEdit_" & IDSelect & "_IsSuccess") = 1
                        Response.Redirect("/Default.aspx?mid=Common&fid=ctrlControlsNewEdit&group=Secure&noscroll=1&FormType=1&ID=" & IDSelect)
                    End If

                Case CommonMessage.TOOLBARITEM_CANCEL
                    IDSelect = ""
                    Response.Redirect("/Default.aspx?mid=Common&fid=ctrlControlsManage&group=Secure")
            End Select

            'Insert logs
            Dim startTime As DateTime = DateTime.UtcNow
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <summary>
    ''' Chọn chức năng
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cboFunctionName_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboFunctionName.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If cboFunctionName.SelectedValue <> "" AndAlso cboFunctionName.SelectedValue <> "0" Then
                IDSelect = cboFunctionName.SelectedValue
            Else
                IDSelect = ""
            End If

            Refresh()
            'Insert logs
            Dim startTime As DateTime = DateTime.UtcNow
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Custom"

    Private Sub GetParams()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If Request.Params("ID") IsNot Nothing Then
                IDSelect = Request.Params("ID").ToString
                cboFunctionName.SelectedValue = Request.Params("ID").ToString
            End If

            'Insert logs
            Dim startTime As DateTime = DateTime.UtcNow
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

End Class