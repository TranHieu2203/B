Imports Common.CommonMessage
Imports Framework.UI
Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic
Imports HistaffFrameworkPublic.FrameworkUtilities
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlValueSetI
    Inherits CommonView
    Protected WithEvents rCbb As RadComboBox
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Common/Secure/" + Me.GetType().Name.ToString()

#Region "Property"
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
    ''' 25/07/2017 13:44
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức khởi tạo các thiết lập, các giá trị ban đầu cho các control trên trang
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
    ''' 25/07/2017 13:44
    ''' </lastupdate>
    ''' <summary>
    ''' Hiển thị các giá trị trên trang
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
    ''' 25/07/2017 13:44
    ''' </lastupdate>
    ''' <summary>
    ''' Phương phức bind dữ liệu:
    ''' Lấy thông tin các params
    ''' gán trạng thái hiện tại của view là STATE_NOMAL
    ''' Cập nhật trạng thái các control trên trang
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
    ''' 25/07/2017 13:44
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện SelectedIndexChanged cho control rgValueSets
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgValueSets_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgValueSets.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If rgValueSets.SelectedItems.Count > 0 Then
                Dim slItem As GridDataItem
                slItem = rgValueSets.SelectedItems(0)
                IDSelected = Decimal.Parse(slItem("FLEX_VALUE_ID").Text)
                rtbValue.Text = slItem("FLEX_VALUE").Text
                rtbValueDesc.Text = slItem("FLEX_VALUE_DESCRIPTION").Text
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                 CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 25/07/2017 13:44
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện command khi click toolbar 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim rep As New HistaffFrameworkRepository
        Dim id As Decimal = -1
        Dim value As String = ""
        Dim valuedesc As String = ""
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    ClearAllInputControl()
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
                Case CommonMessage.TOOLBARITEM_SAVE
                    'Neu la New => insert vao AD_DESC_FLEX_COL vs id  = -1
                    'Neu la Edit => Update vao AD_DESC_FLEX_COL vs id = idSelected in gridview
                    Select Case CurrentState
                        Case CommonMessage.STATE_NEW
                            repCPR = New CommonProgramsRepository
                            'Lấy tất cả thông tin trên form và insert vào AD_FLEX_VALUES                               
                            id = -1
                            If Check_Value() Then
                                value = rtbValue.Text
                            Else
                                Exit Sub
                            End If
                            valuedesc = rtbValueDesc.Text

                            'Process : Insert
                            Dim obj As Object = rep.ExecuteStoreScalar("PKG_HCM_SYSTEM.UPDATE_AD_FLEX_VALUES",
                                                                        New List(Of Object)(New Object() {valueSetName, id, value, valuedesc, OUT_NUMBER}))

                            Dim check = Decimal.Parse(obj(0).ToString)
                            If check = 1 Then
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                                rgValueSets.Rebind()
                                CurrentState = CommonMessage.STATE_NORMAL
                                ClearAllInputControl()
                            Else
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                            End If
                        Case CommonMessage.STATE_EDIT
                            If rgValueSets.SelectedItems.Count > 0 Then
                                Dim slItem As GridDataItem
                                slItem = rgValueSets.SelectedItems(0)
                                id = Decimal.Parse(slItem("FLEX_VALUE_ID").Text)
                            Else
                                ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                            End If
                            value = rtbValue.Text
                            valuedesc = rtbValueDesc.Text

                            'Process : Insert
                            Dim obj As Object = rep.ExecuteStoreScalar("PKG_HCM_SYSTEM.UPDATE_AD_FLEX_VALUES",
                                                                        New List(Of Object)(New Object() {valueSetName, id, value, valuedesc, OUT_NUMBER}))

                            Dim check = Decimal.Parse(obj(0).ToString)
                            If check = 1 Then
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                                rgValueSets.Rebind()
                                CurrentState = CommonMessage.STATE_NORMAL
                                ClearAllInputControl()
                            Else
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                            End If
                    End Select
                    CurrentState = CommonMessage.STATE_NORMAL
                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    ClearAllInputControl()
                Case CommonMessage.TOOLBARITEM_DELETE
                    'CALL FUNCTION DELETE
                    If rgValueSets.SelectedItems.Count > 0 Then
                        Dim slItem As GridDataItem
                        slItem = rgValueSets.SelectedItems(0)
                        IDSelected = Decimal.Parse(slItem("FLEX_VALUE_ID").Text)
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                    End If
                    Dim obj As Object = rep.ExecuteStoreScalar("PKG_HCM_SYSTEM.DELETE_AD_FLEX_VALUES",
                                                                        New List(Of Object)(New Object() {IDSelected, OUT_NUMBER}))
                    If obj IsNot Nothing Then
                        Dim check = Decimal.Parse(obj(0).ToString)
                        If check = 1 Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            rgValueSets.Rebind()
                            CurrentState = CommonMessage.STATE_NORMAL
                            ClearAllInputControl()
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                        End If
                    Else
                        _myLog.WriteLog(_myLog._error, _classPath, method, 0, Nothing, "Lỗi thủ tục PKG_HCM_SYSTEM.DELETE_AD_FLEX_VALUES: " + valueSetName + " | " + IDSelected + " | " + value)
                    End If
                    CurrentState = CommonMessage.STATE_NORMAL
            End Select
            UpdateControlState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                         CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 25/07/2017 13:44
    ''' </lastupdate>       
    ''' <summary>
    ''' Xử lý sự kiện NeedDataSource cho control rgValueSets
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

#End Region

#Region "Custom"
    ''' <lastupdate>
    ''' 25/07/2017 13:44
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
            Dim ds = rep.ExecuteStore("PKG_HCM_SYSTEM.READ_FLEX_VALUES_I",
                                        CreateParameterList(New With {.P_CUR = OUT_CURSOR}))
            If (ds IsNot Nothing) Then
                dtb = ds.Tables(0)
            End If

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
    ''' 25/07/2017 13:44
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
            dtb.Columns.Add("FLEX_VALUE_SET_ID", GetType(Decimal))
            dtb.Columns.Add("FLEX_VALUE_SET_NAME", GetType(String))
            dtb.Columns.Add("FLEX_VALUE_ID", GetType(Decimal))
            dtb.Columns.Add("FLEX_VALUE", GetType(String))
            dtb.Columns.Add("FLEX_VALUE_DESCRIPTION", GetType(String))
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                 CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try

        Return dtb
    End Function
    ''' <lastupdate>
    ''' 25/07/2017 13:44
    ''' </lastupdate>
    ''' <summary>
    ''' Cập nhật trạng thái các control trên trang
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CurrentState
                ''-----------Tab Nation----------------'
                Case STATE_NORMAL
                    rgValueSets.Enabled = True
                    rtbValue.ReadOnly = True
                    rtbValueDesc.ReadOnly = True
                Case STATE_EDIT
                    rtbValue.ReadOnly = True
                    rtbValueDesc.ReadOnly = False
                Case STATE_NEW
                    ClearAllInputControl()
                    rtbValue.ReadOnly = False
                    rtbValueDesc.ReadOnly = False
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
    ''' 25/07/2017 13:44
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý làm mới dữ liệu các control trên page về trạng thái mặc định
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub ClearAllInputControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            rtbValue.Text = String.Empty
            rtbValueDesc.Text = String.Empty
            rgValueSets.MasterTableView.ClearSelectedItems()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                  CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <lastupdate>
    ''' 25/07/2017 13:44
    ''' </lastupdate>
    ''' <summary>
    ''' Hàm check tồn tại giá trị được định nghĩa
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Check_Value()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim rep As New HistaffFrameworkRepository
            '  Dim obj As Object = rep.ExecuteStoreScalar("PKG_HCM_SYSTEM.CHECK_FLEX_VALUES",
            '                                            New List(Of Object)(New Object() {rtbValue.Text, OUT_NUMBER})

            Dim obj As Object = rep.ExecuteStoreScalar("PKG_HCM_SYSTEM.CHECK_FLEX_VALUES",
                                                                        New List(Of Object)(New Object() {rtbValue.Text, OUT_NUMBER}))

            Dim rs = Decimal.Parse(obj(0).ToString)
            If rs = 1 Then
                Return True
            Else
                ShowMessage("Đã có tên value này trong value set " & valueSetName & ". đề nghị nhập khác !!! ", NotifyType.Warning)
                Return False
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                  CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Return False

        End Try

    End Function
    ''' <lastupdate>
    ''' 25/07/2017 13:44
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức lấy tất cả các params
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