Imports System.IO
Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog
Public Class ctrlHU_Template
    Inherits Common.CommonView

    'Content: Write log time and error
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Profile\Modules\Profile\Business" + Me.GetType().Name.ToString()
#Region "Property"

#End Region

#Region "Page"
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức ViewLoad hiển thị các control trên trang
    ''' Làm mới trang
    ''' Cập nhật trạng thái các control trên trang
    ''' Thiết lập ban đầu cho control rgData, rgTemplate
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Refresh()
            UpdateControlState()

            rgData.SetFilter()
            rgTemplate.AllowCustomPaging = False
            rgTemplate.SetFilter()
            rgTemplate.AllowCustomPaging = False
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức khởi tạo các control trên trang
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            InitControl()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Đổ dữ liệu cho các control trên trang
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            GetDataCombo()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức khởi tạo các control trên trang
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức làm mới các trạng thái thiết lập các control trên trang
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        'Dim rep As New ProfileBusinessRepository
        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
#End Region

#Region "Event"
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện button command của control ctrlMessageBox
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DELETE
                UpdateControlState()
            End If
            If e.ActionName = CommonMessage.TOOLBARITEM_EDIT And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                'save data editting
                Dim lstData As New List(Of MergeFieldDTO)
                For Each item As GridDataItem In rgData.EditItems
                    Dim objData As New MergeFieldDTO
                    If item.GetDataKeyValue("ID").ToString <> "" Then
                        objData.ID = item.GetDataKeyValue("ID")
                    End If
                    objData.HU_TEMPLATE_TYPE_ID = item.GetDataKeyValue("TEMPLATE_TYPE_ID")
                    objData.CODE = item.GetDataKeyValue("CODE")
                    objData.NAME = CType(item("NAME").Controls(0), TextBox).Text
                    lstData.Add(objData)
                Next
                Using rep As New ProfileRepository
                    If rep.UpdateMergeField(lstData) Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgData.MasterTableView.ClearEditItems()
                        rgData.Rebind()
                        CurrentState = CommonMessage.STATE_NORMAL
                    End If
                End Using
                UpdateControlState()
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện needDataSource cho control rgData
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub rgData_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If cboTemplateType.SelectedValue = "" Then
                rgData.DataSource = New DataTable
                Exit Sub
            End If
            Dim dtData As DataTable
            Using rep As New ProfileRepository
                dtData = rep.GetHU_MergeFieldList(False, cboTemplateType.SelectedValue)
            End Using
            rgData.DataSource = dtData
            Dim control As RadButton
            control = rgData.MasterTableView.GetItems(GridItemType.CommandItem)(0).FindControl("btnEditMerge")
            If control IsNot Nothing Then
                If (rgData.Items.Count > 0) Then
                    control.Enabled = True
                Else
                    control.Enabled = False
                End If

            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện itemdatabound cho control rgTemplate
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgTemplate_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgTemplate.ItemDataBound
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
                If cboTemplateType.SelectedValue <> "" Then
                    Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
                    Dim link As HyperLink = datarow("LINK_DOWN").FindControl("linkDownload")
                    Dim CODE As String = datarow.GetDataKeyValue("CODE")
                    Dim folderName As String = cboTemplateType.SelectedItem.Attributes("FOLDER_NAME")
                    If folderName = "HRProcess" Then
                        Using rep As New ProfileRepository
                            If Not rep.ExistsTemplateFileHost(folderName & "\" & CODE & ".doc") Then
                                link.Visible = False
                            End If
                        End Using
                    Else
                        If Not ExistTemplateFile(CODE, folderName, "") Then
                            link.Visible = False
                        End If
                    End If
                End If
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện Needdatasource cho rad grid rgTemplate
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub rgTemplate_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgTemplate.NeedDataSource
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If cboTemplateType.SelectedValue = "" Then
                rgTemplate.DataSource = New DataTable
                Exit Sub
            End If
            Dim rep As New ProfileRepository
            Dim dtData As DataTable = rep.GetHU_TemplateList(False, cboTemplateType.SelectedValue)
            rgTemplate.DataSource = dtData
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện itemdatabound  cho control cboTemplateType
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cboTemplateType_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxItemEventArgs) Handles cboTemplateType.ItemDataBound
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim dataSourceRow As DataRowView = e.Item.DataItem
            e.Item.Attributes("FOLDER_NAME") = dataSourceRow("FOLDER_NAME").ToString()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try


    End Sub
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện IndexChanged cho control cboTemplateType
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cboTemplateType_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboTemplateType.SelectedIndexChanged
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            'Check neu dang sua thi comfirm sua
            If (CurrentState = CommonMessage.STATE_EDIT) Then
                ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_IS_CONFIRM_EDIT_SAVE)
                ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_EDIT
                ctrlMessageBox.DataBind()
                ctrlMessageBox.Show()
            Else
                CurrentState = CommonMessage.STATE_NORMAL
                rgData.MasterTableView.ClearEditItems()
                rgData.Rebind()
                rgTemplate.Rebind()

            End If
            Dim control As RadButton
            control = rgData.MasterTableView.GetItems(GridItemType.CommandItem)(0).FindControl("btnEditMerge")
            If control IsNot Nothing Then
                If (rgData.Items.Count > 0) Then
                    control.Enabled = True
                Else
                    control.Enabled = False
                End If

            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện click Ok của control ctrlUpload
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlUpload1_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload1.OkClicked
        Dim folderName As String = ""
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If ctrlUpload1.UploadedFiles.Count = 0 Then
                Exit Sub
            End If
            folderName = cboTemplateType.SelectedItem.Attributes("FOLDER_NAME")
            Dim file As UploadedFile = ctrlUpload1.UploadedFiles(0)
            Dim item = CType(rgTemplate.SelectedItems(0), GridDataItem)

            If cboTemplateType.SelectedValue = 13 Then
                Dim fByte(file.ContentLength) As Byte
                file.InputStream().Read(fByte, 0, file.ContentLength)
                Using rep As New ProfileRepository
                    rep.SaveTemplateFileHost(fByte, "\TemplateDynamic\" & folderName, item.GetDataKeyValue("CODE") & file.GetExtension)
                End Using
            Else
                SaveTemplateFile(item.GetDataKeyValue("CODE") & file.GetExtension, file, folderName)
            End If
            rgTemplate.Rebind()
            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện itemcommand của control rgTemplate với command importFile
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgTemplate_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles rgTemplate.ItemCommand
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Select Case e.CommandName
                Case "ImportFile"
                    If cboTemplateType.SelectedValue = "" Then
                        ShowMessage(Translate("Bạn phải chọn Loại biểu mẫu"), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rgTemplate.SelectedItems.Count = 0 Then
                        ShowMessage(Translate("Bạn phải chọn Biểu mẫu"), NotifyType.Warning)
                        Exit Sub
                    End If
                    ctrlUpload1.Show()

            End Select
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện ItemDataBound cho rad grid rgData
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgData_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgData.ItemDataBound
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If e.Item.Edit Then
                Dim edit = CType(e.Item, GridEditableItem)
                Dim txt As TextBox
                txt = CType(edit("NAME").Controls(0), TextBox)
                txt.Width = Unit.Percentage(100)
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện ItemCommand cho rgcData với command EditMerge, SaveMerge, CancelMerge
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgData_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles rgData.ItemCommand
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Select Case e.CommandName
                Case "EditMerge"
                    CurrentState = CommonMessage.STATE_EDIT
                    For Each item As GridItem In rgData.Items
                        item.Edit = True
                    Next
                    rgData.Rebind()
                    Dim control As RadButton
                    control = rgData.MasterTableView.GetItems(GridItemType.CommandItem)(0).FindControl("btnEditMerge")
                    If control IsNot Nothing Then
                        control.Enabled = False
                    End If
                    control = rgData.MasterTableView.GetItems(GridItemType.CommandItem)(0).FindControl("btnSaveMerge")
                    If control IsNot Nothing Then
                        control.Enabled = True
                    End If
                    control = rgData.MasterTableView.GetItems(GridItemType.CommandItem)(0).FindControl("btnCancelMerge")
                    If control IsNot Nothing Then
                        control.Enabled = True
                    End If
                Case "SaveMerge"
                    Dim lstData As New List(Of MergeFieldDTO)
                    For Each item As GridDataItem In rgData.EditItems
                        Dim objData As New MergeFieldDTO
                        If item.GetDataKeyValue("ID").ToString <> "" Then
                            objData.ID = item.GetDataKeyValue("ID")
                        End If
                        objData.HU_TEMPLATE_TYPE_ID = item.GetDataKeyValue("TEMPLATE_TYPE_ID")
                        objData.CODE = item.GetDataKeyValue("CODE")
                        objData.NAME = CType(item("NAME").Controls(0), TextBox).Text
                        lstData.Add(objData)
                    Next
                    Using rep As New ProfileRepository
                        If rep.UpdateMergeField(lstData) Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            rgData.MasterTableView.ClearEditItems()
                            rgData.Rebind()
                            CurrentState = CommonMessage.STATE_NORMAL
                        End If
                    End Using

                    Dim control As RadButton
                    control = rgData.MasterTableView.GetItems(GridItemType.CommandItem)(0).FindControl("btnEditMerge")
                    If control IsNot Nothing Then
                        If (rgData.Items.Count > 0) Then
                            control.Enabled = True
                        Else
                            control.Enabled = False
                        End If

                    End If
                    control = rgData.MasterTableView.GetItems(GridItemType.CommandItem)(0).FindControl("btnSaveMerge")
                    If control IsNot Nothing Then
                        control.Enabled = False
                    End If
                    control = rgData.MasterTableView.GetItems(GridItemType.CommandItem)(0).FindControl("btnCancelMerge")
                    If control IsNot Nothing Then
                        control.Enabled = False
                    End If
                Case "CancelMerge"
                    CurrentState = CommonMessage.STATE_NORMAL
                    Dim control As RadButton
                    control = rgData.MasterTableView.GetItems(GridItemType.CommandItem)(0).FindControl("btnEditMerge")
                    If control IsNot Nothing Then
                        If (rgData.Items.Count > 0) Then
                            control.Enabled = True
                        Else
                            control.Enabled = False
                        End If

                    End If
                    control = rgData.MasterTableView.GetItems(GridItemType.CommandItem)(0).FindControl("btnSaveMerge")
                    If control IsNot Nothing Then
                        control.Enabled = False
                    End If
                    control = rgData.MasterTableView.GetItems(GridItemType.CommandItem)(0).FindControl("btnCancelMerge")
                    If control IsNot Nothing Then
                        control.Enabled = False
                    End If
                    rgData.MasterTableView.ClearEditItems()
                    rgData.Rebind()
            End Select
            UpdateControlState()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Custom"
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức cập nhật trạng thái control trên trang
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        'Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Select Case CurrentState
                Case CommonMessage.STATE_EDIT
                Case CommonMessage.STATE_NORMAL
            End Select
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức đổ dữ liệu cho combobox cboTemplateType
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetDataCombo()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim dtData
            Using rep As New ProfileRepository
                dtData = rep.GetHU_TemplateType(True)
                FillRadCombobox(cboTemplateType, dtData, "NAME", "ID")
                ClearControlValue()
            End Using
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Hàm xử lý lưu file template 
    ''' </summary>
    ''' <param name="fileName"></param>
    ''' <param name="file"></param>
    ''' <param name="folderName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SaveTemplateFile(ByVal fileName As String,
                                      ByVal file As UploadedFile,
                                      ByVal folderName As String) As Boolean
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim filePath = AppDomain.CurrentDomain.BaseDirectory & "\TemplateDynamic\" & folderName
            If Not Directory.Exists(filePath) Then
                Directory.CreateDirectory(filePath)
            End If
            Dim fPath = Path.Combine(filePath, fileName)
            file.SaveAs(fPath, True)
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
            Return True
        Catch ex As Exception
            Throw
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Function
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Hàm kiểm tra tồn tại file template
    ''' </summary>
    ''' <param name="fileId"></param>
    ''' <param name="folderName"></param>
    ''' <param name="fileName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ExistTemplateFile(ByVal fileID As String,
                                      ByVal folderName As String,
                                      ByRef fileName As String) As Boolean
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim filePath = AppDomain.CurrentDomain.BaseDirectory & "\TemplateDynamic\" & folderName

            If Not Directory.Exists(filePath) Then
                Directory.CreateDirectory(filePath)
            End If
            Dim dirs As String() = Directory.GetFiles(filePath, fileID & ".*")
            If dirs.Length > 0 Then
                Dim fInfo = New FileInfo(dirs(0))
                fileName = fInfo.Name
                Return True
            Else
                Return False
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Function

#End Region

End Class