Imports Common.CommonBusiness
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlGroupFunctionAddEdit
    Inherits CommonView
    Public Property GroupID As Decimal
    Public Overrides Property MustAuthorize As Boolean = False
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Common/Modules/Common/Secure/" + Me.GetType().Name.ToString()

#Region "Property"

    ''' <summary>
    ''' Obj ViewShowed
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ViewShowed As Boolean
        Get
            Return ViewState(Me.ID & "_ViewShowed")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_ViewShowed") = value
        End Set
    End Property
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Property ComboBoxData As ComboBoxDataDTO
        Get
            Return PageViewState(Me.ID & "_ComboBoxData")
        End Get
        Set(ByVal value As ComboBoxDataDTO)
            PageViewState(Me.ID & "_ComboBoxData") = value
        End Set
    End Property
    ''' <summary>
    ''' Obj GroupID_Old
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property GroupID_Old As Decimal
        Get
            Return ViewState(Me.ID & "_GroupID_Old")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_GroupID_Old") = value
        End Set
    End Property

    ''' <summary>
    ''' Obj GroupFunction
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property GroupFunction As List(Of FunctionDTO)
        Get
            Return ViewState(Me.ID & "_GroupFunction")
        End Get
        Set(ByVal value As List(Of FunctionDTO))
            ViewState(Me.ID & "_GroupFunction") = value
        End Set
    End Property

    ''' <summary>
    ''' Obj MaximumRows
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MaximumRows As Integer
        Get
            If ViewState(Me.ID & "_MaximumRows") Is Nothing Then
                Return 0
            End If
            Return ViewState(Me.ID & "_MaximumRows")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_MaximumRows") = value
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
            If ViewState(Me.ID & "_SelectedItem") Is Nothing Then
                ViewState(Me.ID & "_SelectedItem") = New List(Of Decimal)
            End If
            Return ViewState(Me.ID & "_SelectedItem")
        End Get
        Set(ByVal value As List(Of Decimal))
            ViewState(Me.ID & "_SelectedItem") = value
        End Set
    End Property

    ''' <summary>
    ''' Obj PageSize
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PageSize As Integer
        Get
            If ViewState(Me.ID & "_PageSize") Is Nothing Then
                Return rgGrid.PageSize
            End If
            Return ViewState(Me.ID & "_PageSize")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_PageSize") = value
        End Set
    End Property
#End Region

#Region "Page"

    ''' <lastupdate>
    ''' 26/07/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức khởi tạo dữ liệu cho các control trên trang
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            rgGrid.AllowCustomPaging = True
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 26/07/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc lam moi cac control theo tuy chon Message, 
    ''' Message co gia tri default la ""
    ''' Bind lai du lieu cho grid rgGrid
    ''' Gọi hàm chuyển đổi trạng thái control
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If GroupID = Nothing Then Exit Sub
            If Not ViewShowed Then Exit Sub
            Dim Sorts As String = rgGrid.MasterTableView.SortExpressions.GetSortString()
            Dim filter As New FunctionDTO

            'Modify: TUNGLD - 20/09/19
            filter.FID = rgGrid.MasterTableView.GetColumn("FID").CurrentFilterValue.Normalize(NormalizationForm.FormC)
            filter.NAME = rgGrid.MasterTableView.GetColumn("NAME").CurrentFilterValue.Normalize(NormalizationForm.FormC)
            filter.FUNCTION_GROUP_NAME = rgGrid.MasterTableView.GetColumn("FUNCTION_GROUP_NAME").CurrentFilterValue.Normalize(NormalizationForm.FormC)
            filter.MODULE_NAME = rgGrid.MasterTableView.GetColumn("MODULE_NAME").CurrentFilterValue.Normalize(NormalizationForm.FormC)
            filter.MODULE_NAME_FID = cboMODULE.Items(cboMODULE.SelectedIndex).Text
            filter.FUNCTION_GROUP_NAME_FID = cboFunctionGroup.Items(cboFunctionGroup.SelectedIndex).Text
            filter.FUNCTION_NAME_FID = txtFUNCTION_NAME.Text.Trim
            'If GroupID_Old = Nothing Or GroupID_Old <> GroupID _
            '    Or GroupFunction Is Nothing Or Message = CommonMessage.ACTION_SAVED _
            '     Or Message = CommonMessage.ACTION_UPDATED Then
            '    Dim rep As New CommonRepository
            '    If Sorts IsNot Nothing Then
            '        GroupFunction = rep.GetGroupFunctionNotPermision(GroupID, filter, rgGrid.CurrentPageIndex, PageSize, MaximumRows, Sorts)
            '    Else
            '        GroupFunction = rep.GetGroupFunctionNotPermision(GroupID, filter, rgGrid.CurrentPageIndex, PageSize, MaximumRows)
            '    End If
            'End If

            Dim rep As New CommonRepository
            If Sorts IsNot Nothing Then
                GroupFunction = rep.GetGroupFunctionNotPermision(GroupID, filter, rgGrid.CurrentPageIndex, PageSize, MaximumRows, Sorts)
            Else
                GroupFunction = rep.GetGroupFunctionNotPermision(GroupID, filter, rgGrid.CurrentPageIndex, PageSize, MaximumRows)
            End If

            'Đưa dữ liệu vào Grid
            rgGrid.VirtualItemCount = MaximumRows
            rgGrid.DataSource = GroupFunction
            If Message <> CommonMessage.ACTION_UPDATED Then rgGrid.DataBind()

            GroupID_Old = GroupID
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim dtbModule = New DataTable
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            'Load Combobox
            Dim rep As New CommonRepository
            If ComboBoxData Is Nothing Then
                ComboBoxData = New ComboBoxDataDTO
                ComboBoxData.GET_MODULE = True
                ComboBoxData.GET_FUNCTION_GROUP = True
                rep.GetComboList(ComboBoxData)
            End If
            FillDropDownList(cboMODULE, ComboBoxData.LIST_MODULE, "NAME", "ID", Common.SystemLanguage, True, cboMODULE.SelectedValue)
            FillDropDownList(cboFunctionGroup, ComboBoxData.LIST_FUNCTION_GROUP, "NAME", "ID", Common.SystemLanguage, True)
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
    ''' 26/07/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien command khi click item tren toolbar chỉ khi ấn lưu 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadToolBarEventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, Telerik.Web.UI.RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE
                    If CurrentState = CommonMessage.STATE_NEW Then
                        Dim lst As List(Of CommonBusiness.GroupFunctionDTO)
                        Dim rep As New CommonRepository
                        CurrentState = CommonMessage.STATE_NORMAL
                        lst = RepareData()
                        If rep.InsertGroupFunction(lst) Then
                            'Refresh(CommonMessage.ACTION_SAVED)
                            'Me.ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                            'Gửi thông điệp cho Parent View
                            Me.Send(CommonMessage.ACTION_SUCCESS)
                        Else
                            'Me.ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Warning)
                            'Gửi thông điệp cho Parent View
                            Me.Send(CommonMessage.ACTION_UNSUCCESS)
                        End If
                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    'Gửi thông điệp cho Parent View
                    Me.Send(CommonMessage.ACTION_UPDATED)
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 26/07/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien ItemCommand cua control rgGrid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgGrid_ItemCommand(ByVal sender As Object, ByVal e As GridCommandEventArgs) Handles rgGrid.ItemCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If e.CommandName = RadGrid.FilterCommandName Then
                Refresh(CommonMessage.ACTION_SAVED)
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                     CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try

    End Sub
    Private Sub btnFIND_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFIND.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.GroupFunction = Nothing
            rgGrid.CurrentPageIndex = 0
            Refresh()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 26/07/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien PageIndexChanged cua control rgGrid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgGrid_PageIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles rgGrid.PageIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            SaveGridChecked()
            rgGrid.CurrentPageIndex = e.NewPageIndex
            Refresh(CommonMessage.ACTION_SAVED)
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                     CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try

    End Sub

    ''' <lastupdate>
    ''' 26/07/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien PageSizeChanged cua control rgGrid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgGrid_PageSizeChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageSizeChangedEventArgs) Handles rgGrid.PageSizeChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            PageSize = e.NewPageSize
            Refresh(CommonMessage.ACTION_SAVED)
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    ''' <lastupdate>
    ''' 26/07/2017 10:00
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
            If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
                Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
                Dim id As Decimal = Decimal.Parse(datarow.GetDataKeyValue("ID").ToString)
                If SelectedItem IsNot Nothing AndAlso SelectedItem.Contains(id) Then
                    datarow.Selected = True
                End If
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                      CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    Public Sub CheckBox_CheckChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If chkAll.Checked Then
                cbALLOW_CREATE.Checked = True
                cbALLOW_DELETE.Checked = True
                cbALLOW_EXPORT.Checked = True
                cbALLOW_IMPORT.Checked = True
                cbALLOW_MODIFY.Checked = True
                cbALLOW_PRINT.Checked = True
                cbALLOW_SPECIAL1.Checked = True
                cbALLOW_SPECIAL2.Checked = True
                cbALLOW_SPECIAL3.Checked = True
                cbALLOW_SPECIAL4.Checked = True
                cbALLOW_SPECIAL5.Checked = True
            Else
                cbALLOW_CREATE.Checked = False
                cbALLOW_DELETE.Checked = False
                cbALLOW_EXPORT.Checked = False
                cbALLOW_IMPORT.Checked = False
                cbALLOW_MODIFY.Checked = False
                cbALLOW_PRINT.Checked = False
                cbALLOW_SPECIAL1.Checked = False
                cbALLOW_SPECIAL2.Checked = False
                cbALLOW_SPECIAL3.Checked = False
                cbALLOW_SPECIAL4.Checked = False
                cbALLOW_SPECIAL5.Checked = False

            End If
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
#End Region

#Region "Custom"

    ''' <lastupdate>
    ''' 26/07/2017 10:00
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
                Dim id As Decimal = Decimal.Parse(dr.GetDataKeyValue("ID").ToString)
                If dr.Selected Then
                    If Not SelectedItem.Contains(id) Then SelectedItem.Add(id)
                Else
                    If SelectedItem.Contains(id) Then SelectedItem.Remove(id)
                End If
            Next
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                         CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    ''' <lastupdate>
    ''' 26/07/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý data trước khi gọi hàm update hoặc insert
    ''' </summary>
    ''' <remarks></remarks>
    Protected Function RepareData() As List(Of CommonBusiness.GroupFunctionDTO)
        Dim lst As New List(Of CommonBusiness.GroupFunctionDTO)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            SaveGridChecked()
            For Each dr As Decimal In SelectedItem
                Dim item As New CommonBusiness.GroupFunctionDTO
                item.ALLOW_CREATE = If(cbALLOW_CREATE.Checked, 1, 0)
                item.ALLOW_DELETE = If(cbALLOW_DELETE.Checked, 1, 0)
                item.ALLOW_EXPORT = If(cbALLOW_EXPORT.Checked, 1, 0)
                item.ALLOW_IMPORT = If(cbALLOW_IMPORT.Checked, 1, 0)
                item.ALLOW_MODIFY = If(cbALLOW_MODIFY.Checked, 1, 0)
                item.ALLOW_PRINT = If(cbALLOW_PRINT.Checked, 1, 0)
                item.ALLOW_SPECIAL1 = If(cbALLOW_SPECIAL1.Checked, 1, 0)
                item.ALLOW_SPECIAL2 = If(cbALLOW_SPECIAL2.Checked, 1, 0)
                item.ALLOW_SPECIAL3 = If(cbALLOW_SPECIAL3.Checked, 1, 0)
                item.ALLOW_SPECIAL4 = If(cbALLOW_SPECIAL4.Checked, 1, 0)
                item.ALLOW_SPECIAL5 = If(cbALLOW_SPECIAL5.Checked, 1, 0)
                item.FUNCTION_ID = dr
                item.GROUP_ID = GroupID
                lst.Add(item)
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