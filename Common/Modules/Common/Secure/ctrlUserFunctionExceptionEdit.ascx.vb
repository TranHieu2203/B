Imports Common
Imports Common.CommonBusiness
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI
Imports WebAppLog
Public Class ctrlUserFunctionExceptionEdit
    Inherits CommonView
    Public Overrides Property MustAuthorize As Boolean = True

    'Content: Write log time and error
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _flag As Boolean = True
    Dim _classPath As String = "Common\Modules\Common\Secure" + Me.GetType().Name.ToString()

    ''' <summary>
    ''' isPhysical
    ''' </summary>
    ''' <remarks></remarks>
    Public isPhysical As Decimal = Decimal.Parse(ConfigurationManager.AppSettings("PHYSICAL_PATH"))

#Region "Property"

    Public Property MaximumRows As Integer
        Get
            If PageViewState(Me.ID & "_MaximumRows") Is Nothing Then
                Return 0
            End If
            Return PageViewState(Me.ID & "_MaximumRows")
        End Get
        Set(ByVal value As Integer)
            PageViewState(Me.ID & "_MaximumRows") = value
        End Set
    End Property
    Public Property UserFunction As List(Of UserFunctionDTO)
        Get
            Return PageViewState(Me.ID & "_UserFunction")
        End Get
        Set(ByVal value As List(Of UserFunctionDTO))
            PageViewState(Me.ID & "_UserFunction") = value
        End Set
    End Property
#End Region

#Region "Page"

    ''' <lastupdate>
    ''' 06/07/2017 14:36
    ''' </lastupdate>
    ''' <summary>
    ''' Hiển thị thông tin các control trên page
    ''' Cập nhật các trạng thái của các control
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            GetParams()
            Refresh()
            UpdateControlState()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    '''<lastupdate>
    ''' 06/07/2017 14:39
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức khởi tạo giá trị cho các control trên page
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            InitControl()
            If Not IsPostBack Then
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    ''' <lastupdated>
    ''' 06/07/2017 14:40
    ''' </lastupdated>
    ''' <summary>
    ''' Phương thức bind dữ liệu cho các combobox
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            GetDataCombo()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 06/07/2017 14:56
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc khoi tao cac gia tri cho cac control tren page
    ''' Fixed doi voi user la HR.Admin hoac Admin thi them chuc nang "Mo cho phe duyet"
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.MainToolBar = rtbMain

            Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Save, ToolbarItem.Cancel)
            CType(MainToolBar.Items(0), RadToolBarButton).CausesValidation = True
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 06/07/2017 15:17
    ''' </lastupdate>
    ''' <summary>
    ''' Lam moi trang thai cua cac control tren page
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Select Case Message
                Case "UpdateView"
                    CurrentState = CommonMessage.STATE_EDIT
                Case "NormalView"
                    CurrentState = CommonMessage.STATE_NEW
            End Select
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Event"

    ''' <lastupdate>
    ''' 06/07/2017 15:41
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện command tren toolbar khi click vao cac item cua no
    ''' Cac command la luu, mo khoa, huy 
    ''' Cập nhật lại trạng thái các control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim gID As Decimal
        'Dim stt As OtherListDTO
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE
                    If rgGrid.SelectedItems.Count = 0 Then
                        Me.ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), Utilities.NotifyType.Warning)
                        Exit Sub
                    End If
                    If rgGrid.SelectedItems.Count > 1 Then
                        Me.ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), Utilities.NotifyType.Warning)
                        Exit Sub
                    End If
                    Dim idSelect As Decimal
                    Dim objFunction As New CommonBusiness.UserFunctionDTO
                    For Each i As GridDataItem In rgGrid.SelectedItems
                        idSelect = CDec(i.GetDataKeyValue("ID"))
                        objFunction = (From p In UserFunction Where p.ID = idSelect).FirstOrDefault

                        Dim cbo As RadComboBox
                        cbo = DirectCast(i("WORK_LEVEL_NAME").FindControl("cboWorkLevel"), RadComboBox)
                        If cbo.CheckedItems.Count > 0 Then
                            objFunction.LST_WL = (From p In cbo.CheckedItems Select CDec(p.Value)).ToList()
                        End If

                        Dim chkCREATE As CheckBox
                        chkCREATE = DirectCast(i("ALLOW_CREATE").FindControl("chkCREATE"), CheckBox)
                        objFunction.ALLOW_CREATE = chkCREATE.Checked

                        Dim chkMODIFY As CheckBox
                        chkMODIFY = DirectCast(i("ALLOW_MODIFY").FindControl("chkMODIFY"), CheckBox)
                        objFunction.ALLOW_MODIFY = chkMODIFY.Checked

                        Dim chkDELETE As CheckBox
                        chkDELETE = DirectCast(i("ALLOW_DELETE").FindControl("chkDELETE"), CheckBox)
                        objFunction.ALLOW_DELETE = chkDELETE.Checked

                        Dim chkIMPORT As CheckBox
                        chkIMPORT = DirectCast(i("ALLOW_IMPORT").FindControl("chkIMPORT"), CheckBox)
                        objFunction.ALLOW_IMPORT = chkIMPORT.Checked

                        Dim chkPRINT As CheckBox
                        chkPRINT = DirectCast(i("ALLOW_PRINT").FindControl("chkPRINT"), CheckBox)
                        objFunction.ALLOW_PRINT = chkPRINT.Checked

                        Dim chkEXPORT As CheckBox
                        chkEXPORT = DirectCast(i("ALLOW_EXPORT").FindControl("chkEXPORT"), CheckBox)
                        objFunction.ALLOW_EXPORT = chkEXPORT.Checked

                        Dim chkSPECIAL1 As CheckBox
                        chkSPECIAL1 = DirectCast(i("ALLOW_SPECIAL1").FindControl("chkSPECIAL1"), CheckBox)
                        objFunction.ALLOW_SPECIAL1 = chkSPECIAL1.Checked

                        Dim chkSPECIAL2 As CheckBox
                        chkSPECIAL2 = DirectCast(i("ALLOW_SPECIAL2").FindControl("chkSPECIAL2"), CheckBox)
                        objFunction.ALLOW_SPECIAL2 = chkSPECIAL2.Checked

                        Dim chkSPECIAL3 As CheckBox
                        chkSPECIAL3 = DirectCast(i("ALLOW_SPECIAL3").FindControl("chkSPECIAL3"), CheckBox)
                        objFunction.ALLOW_SPECIAL3 = chkSPECIAL3.Checked

                        Dim chkSPECIAL4 As CheckBox
                        chkSPECIAL4 = DirectCast(i("ALLOW_SPECIAL4").FindControl("chkSPECIAL4"), CheckBox)
                        objFunction.ALLOW_SPECIAL4 = chkSPECIAL4.Checked

                        Dim chkSPECIAL5 As CheckBox
                        chkSPECIAL5 = DirectCast(i("ALLOW_SPECIAL5").FindControl("chkSPECIAL5"), CheckBox)
                        objFunction.ALLOW_SPECIAL5 = chkSPECIAL5.Checked

                        If ctrlOrg.CheckedValueKeys.Count > 0 Then
                            objFunction.LST_ORG = (From p In ctrlOrg.CheckedValueKeys Select p).ToList()
                        End If
                    Next
                    Dim lst As New List(Of CommonBusiness.UserFunctionDTO)
                    Dim rep As New CommonRepository
                    lst.Add(objFunction)
                    If rep.UpdateUserFunctionException(lst) Then
                        Me.ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                    Else
                        Me.ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Warning)
                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    Dim str As String = "getRadWindow().close('1');"
                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
            End Select
            UpdateControlState()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub rgGrid_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgGrid.SelectedIndexChanged
        Dim item As GridDataItem
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            item = CType(rgGrid.SelectedItems(0), GridDataItem)
            If UserFunction IsNot Nothing Then
                Dim objUserFunction = (From p In UserFunction Where p.ID = item.GetDataKeyValue("ID")).FirstOrDefault

                If objUserFunction IsNot Nothing Then
                    If objUserFunction.LST_ORG IsNot Nothing AndAlso objUserFunction.LST_ORG.Count > 0 Then
                        ctrlOrg.CheckedValueKeys = objUserFunction.LST_ORG
                    Else
                        ctrlOrg.CheckedValueKeys = New List(Of Decimal)
                    End If
                End If
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method,
                          CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub rgGrid_ItemDataBound(ByVal sender As Object, ByVal e As GridItemEventArgs) Handles rgGrid.ItemDataBound
        Try
            If (TypeOf e.Item Is GridDataItem) Then
                Dim edit = CType(e.Item, GridEditableItem)
                Dim cbo As RadComboBox
                cbo = CType(edit.FindControl("cboWorkLevel"), RadComboBox)

                If cbo IsNot Nothing Then
                    Dim rep As New CommonProcedureNew
                    Dim jobLevel = rep.GET_JOB_LEVEL_BY_TITLE(0)
                    FillRadCombobox(cbo, jobLevel, "NAME_VN", "ID")
                    If Not IsDBNull(edit.GetDataKeyValue("ID")) Then
                        Dim id = CDec(edit.GetDataKeyValue("ID"))
                        Dim objUserFunction = (From p In UserFunction Where p.ID = id).FirstOrDefault
                        If objUserFunction.LST_WL IsNot Nothing AndAlso objUserFunction.LST_WL.Count > 0 Then
                            For Each item As RadComboBoxItem In cbo.Items
                                For Each cen As Decimal In objUserFunction.LST_WL
                                    If Not item.Value = "" Then
                                        If cen = CDec(item.Value) Then
                                            item.Checked = True
                                        End If
                                    End If
                                Next
                            Next
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub rgGrid_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgGrid.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            CreateDataFilter()
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        If hidID.Value <> "" Then
            Dim rep As New CommonRepository
            Dim Sorts As String = rgGrid.MasterTableView.SortExpressions.GetSortString()
            Dim filter As New UserFunctionDTO
            If Sorts IsNot Nothing Then
                UserFunction = rep.GetUserFunctionPermisionException(hidID.Value, Filter, rgGrid.CurrentPageIndex, rgGrid.PageSize, MaximumRows, Sorts)
            Else
                UserFunction = rep.GetUserFunctionPermisionException(hidID.Value, Filter, rgGrid.CurrentPageIndex, rgGrid.PageSize, MaximumRows)
            End If
        Else
            UserFunction = New List(Of UserFunctionDTO)
        End If
        rgGrid.VirtualItemCount = MaximumRows
        rgGrid.DataSource = UserFunction
    End Function
#End Region

#Region "Custom"

    Public Overrides Sub UpdateControlState()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(ViewName, ID, ex)
        End Try

    End Sub
    Private Sub GetDataCombo()
    End Sub
    Private Sub GetParams()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow

            If CurrentState Is Nothing Then
                If Request.Params("IDSelect") IsNot Nothing Then
                    hidID.Value = Request.Params("IDSelect")
                    Exit Sub
                End If
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
#End Region

End Class