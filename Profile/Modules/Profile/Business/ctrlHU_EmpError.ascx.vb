Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlHU_EmpError
    Inherits Common.CommonView

    'author: hongdx
    'EditDate: 21-06-2017
    'Content: Write log time and error
    'asign: hdx
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Profile\Modules\Profile\Business" + Me.GetType().Name.ToString()
    Public Overrides Property MustAuthorize As Boolean = False

    Private TYPEREPORT As ArrayList

#Region "Properties"
    Private Property fullName As String
        Get
            Return PageViewState(Me.ID & "_fullName")
        End Get
        Set(ByVal value As String)
            PageViewState(Me.ID & "_fullName") = value
        End Set
    End Property

    Private Property ID_NO As String
        Get
            Return PageViewState(Me.ID & "_ID_NO")
        End Get
        Set(ByVal value As String)
            PageViewState(Me.ID & "_ID_NO") = value
        End Set
    End Property

    Public WithEvents AjaxManager As RadAjaxManager
    Public Property AjaxManagerId As String

#End Region

#Region "Page"
    ''' <summary>
    ''' Hiển thị thông tin trên trang
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        'Edit by: ChienNV 
        'Trước khi Load thì kiểm tra PostBack
        If Not IsPostBack Then
            Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
            Try
                Dim startTime As DateTime = DateTime.UtcNow
                GetParams()
                rgEmployeeList.SetFilter()
                Refresh()
                _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
            Catch ex As Exception
                'DisplayException(Me.ViewName, Me.ID, ex)
                _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            End Try
        Else
            Exit Sub
        End If
    End Sub
    Public Sub GetParams()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            fullName = Request.Params("FULLNAME")
            ID_NO = Request.Params("ID_NO")
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                       CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <summary>
    ''' Ghi đè phương thức khởi tạo các thiết lập, giá trị các control trên trang
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            AjaxManager = CType(Me.Page, AjaxPage).AjaxManager
            AjaxManagerId = AjaxManager.ClientID

            rgEmployeeList.SetFilter()
            rgEmployeeList.AllowCustomPaging = True
            rgEmployeeList.PageSize = Common.Common.DefaultPageSize
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            InitControl()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <summary>
    ''' khởi tạo các thành phần trên trang
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")

        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <summary>
    ''' Edit by: ChienNV;
    ''' Fill data in control cboPrintSupport
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Try
            GetDataCombo()
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow

            If Message = CommonMessage.ACTION_UPDATED Then
                rgEmployeeList.Rebind()
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
#End Region

#Region "Event"


    ''' <summary>
    ''' Xử lý sự kiện Command khi click item trên toolbar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim objEmployee As New EmployeeDTO
            Dim rep As New ProfileBusinessRepository
            Dim startTime As DateTime = DateTime.UtcNow

            rep.Dispose()
            ' UpdateControlState()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' Xử lý sự kiện item databound
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgEmployeeList_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgEmployeeList.ItemDataBound
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim store As New ProfileStoreProcedure
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
                Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
                datarow("ORG_NAME").ToolTip = Utilities.DrawTreeByString(datarow.GetDataKeyValue("ORG_DESC"))
            End If

            If TypeOf e.Item Is GridDataItem Then
                Dim item = CType(e.Item, GridDataItem)
                Dim ds1 = store.GET_EMP_INF(fullName, ID_NO)
                For Each item1 In ds1.Tables(0).Rows
                    If item.GetDataKeyValue("EMPLOYEE_CODE") = item1("EMPLOYEE_CODE") Then
                        e.Item.BackColor = Drawing.Color.Yellow
                        e.Item.ForeColor = Drawing.Color.Black
                    End If
                Next

                For Each item2 In ds1.Tables(1).Rows
                    If item.GetDataKeyValue("EMPLOYEE_CODE") = item2("EMPLOYEE_CODE") Then
                        e.Item.BackColor = Drawing.Color.Green
                        e.Item.ForeColor = Drawing.Color.Black
                    End If
                Next

                For Each item1 In ds1.Tables(0).Rows
                    For Each item2 In ds1.Tables(1).Rows
                        If item.GetDataKeyValue("EMPLOYEE_CODE") = item2("EMPLOYEE_CODE") And item.GetDataKeyValue("EMPLOYEE_CODE") = item1("EMPLOYEE_CODE") Then
                            e.Item.BackColor = Drawing.Color.Red
                            e.Item.ForeColor = Drawing.Color.Black
                        End If
                    Next
                Next
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    ''' <summary>
    ''' Xử lý sự kiện NeedDataSource của rad grid rgEmployeeList
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgEmployeeList.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try

            CreateDataFilter()

        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' RadGrid_PageIndexChanged
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_PageIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles rgEmployeeList.PageIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            CType(sender, RadGrid).CurrentPageIndex = e.NewPageIndex
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' AjaxManager_AjaxRequest
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub AjaxManager_AjaxRequest(ByVal sender As Object, ByVal e As Telerik.Web.UI.AjaxRequestEventArgs) Handles AjaxManager.AjaxRequest
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim url = e.Argument
            If (url.Contains("reload=1")) Then
                rgEmployeeList.CurrentPageIndex = 0
                rgEmployeeList.Rebind()
                If rgEmployeeList.Items IsNot Nothing AndAlso rgEmployeeList.Items.Count > 0 Then
                    rgEmployeeList.Items(0).Selected = True
                End If
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub


#End Region

#Region "Custom"
    ''' <summary>
    ''' Hàm xử lý tạo dữ liệu filter
    ''' </summary>
    ''' <param name="isFull"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim EmployeeList As List(Of EmployeeDTO)
        'Dim EmployeeList_ID_NO As List(Of EmployeeDTO)
        Dim MaximumRows As Integer
        Dim Sorts As String
        Dim _filter As New EmployeeDTO
        Dim _filter_ID_NO As New EmployeeDTO
        Dim startTime As DateTime = DateTime.UtcNow
        Dim store As New ProfileStoreProcedure
        Try
            Using rep As New ProfileBusinessRepository


                Dim _param = New ParamDTO With {.ORG_ID = 1,
                                                .IS_DISSOLVE = False}
                SetValueObjectByRadGrid(rgEmployeeList, _filter)

                If fullName <> "" Then
                    _filter.FULLNAME_VN = fullName
                End If
                If ID_NO <> "" Then
                    _filter_ID_NO.ID_NO = ID_NO
                End If

                Sorts = rgEmployeeList.MasterTableView.SortExpressions.GetSortString()
                If isFull Then
                    If Sorts IsNot Nothing Then
                        Return rep.GetListEmployeePaging(_filter, _param, Sorts).ToTable()
                    Else
                        Return rep.GetListEmployeePaging(_filter, _param).ToTable()
                    End If
                Else
                    'EmployeeList = rep.GetListEmployeePaging(_filter, rgEmployeeList.CurrentPageIndex, rgEmployeeList.PageSize, MaximumRows, _param)
                    'EmployeeList_ID_NO = rep.GetListEmployeePaging(_filter_ID_NO, rgEmployeeList.CurrentPageIndex, rgEmployeeList.PageSize, MaximumRows, _param)
                    'EmployeeList.AddRange(EmployeeList_ID_NO)
                    'EmployeeList = (From p In EmployeeList Where p.ID_NO.Length = ID_NO.Length Or p.FULLNAME_VN.Length = fullName.Length).Distinct.ToList
                    Dim ds1 = store.GET_EMP_INF(fullName, ID_NO)
                    rgEmployeeList.VirtualItemCount = If(ds1.Tables(2) IsNot Nothing, ds1.Tables(2).Rows.Count, 0)
                    rgEmployeeList.DataSource = ds1.Tables(2)
                End If

            End Using
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Function

    ''' <summary>
    ''' Xử lý sự kiện xóa nhân viên
    ''' </summary>
    ''' <param name="strError"></param>
    ''' <remarks></remarks>
    Private Sub DeleteEmployee(ByRef strError As String)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim rep As New ProfileBusinessRepository

            'Kiểm tra các điều kiện trước khi xóa
            Dim lstEmpID As New List(Of Decimal)

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub GetDataCombo()
        Dim dtData As DataTable
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub


#End Region


End Class