Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Payroll.PayrollBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlPA_FormulaDetail
    Inherits Common.CommonView

    ''' <creator>HongDX</creator>
    ''' <lastupdate>21/06/2017</lastupdate>
    ''' <summary>Write Log</summary>
    ''' <remarks>_mylog, _pathLog, _classPath</remarks>
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Payroll\Modules\Payroll\Setting" + Me.GetType().Name.ToString()

#Region "Property"

    ''' <summary>
    ''' lstCalculation
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property lstCalculation As List(Of OT_OTHERLIST_DTO)

    ''' <summary>
    ''' typePaymentId
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property typePaymentId As Decimal
        Get
            Return ViewState(Me.ID & "_typePaymentId")
        End Get
        Set(value As Decimal)
            ViewState(Me.ID & "_typePaymentId") = value
        End Set
    End Property

    ''' <summary>
    ''' iGrpFomul
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property iGrpFomul As Decimal
        Get
            Return ViewState(Me.ID & "_iGrpFomul")
        End Get
        Set(value As Decimal)
            ViewState(Me.ID & "_iGrpFomul") = value
        End Set
    End Property

    ''' <summary>
    ''' vListSalary
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property vListSalary As List(Of PAFomuler)
        Get
            Return ViewState(Me.ID & "_dtListAllSalary")
        End Get
        Set(ByVal value As List(Of PAFomuler))
            ViewState(Me.ID & "_dtListAllSalary") = value
        End Set
    End Property

    ''' <summary>
    ''' vListInputColum
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property vListInputColum As DataTable
        Get
            Return ViewState(Me.ID & "_dtListInpulColumn")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtListInpulColumn") = value
        End Set
    End Property

#End Region

#Region "Page"

    ''' <lastupdate>23/08/2017</lastupdate>
    ''' <summary>
    ''' Ke thua ViewLoad tu CommnView, la ham load du lieu, control states cua usercontrol
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            'Refresh()
            'UpdateControlState()
            EnabledGridNotPostback(rgData, True)

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>23/08/2017</lastupdate>
    ''' <summary>
    ''' Ham duoc ke thua tu commonView va goi phuong thuc InitControl
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            rgData.SetFilter()
            rgData.AllowCustomPaging = True
            rgData.PageSize = Common.Common.DefaultPageSize
            rgData.ClientSettings.EnablePostBackOnRowClick = False
            InitControl()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>23/08/2017</lastupdate>
    ''' <summary>Load cac control, menubar</summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            'Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarSalaryGroups

            Common.Common.BuildToolbar(Me.MainToolBar,
                                       ToolbarItem.Save,
                                       ToolbarItem.Approve)

            CType(MainToolBar.Items(0), RadToolBarButton).CausesValidation = True
            MainToolBar.Items(1).Text = Translate("Kiểm tra công thức")
            'Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>23/08/2017</lastupdate>
    ''' <summary>
    ''' Load lai du lieu tren grid khi thuc hien update, insert, delete
    ''' (neu co tham so truyen vao thi dua ra messager)
    ''' </summary>
    ''' <param name="Message">tham so truyen vao de phan biet trang thai Insert, Update, Cancel</param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
                rgData.Rebind()
            Else
                CurrentState = CommonMessage.STATE_NORMAL

                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        LoadListAllSalary()
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "Cancel"
                        rgData.MasterTableView.ClearSelectedItems()
                End Select
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>23/08/2017</lastupdate>
    ''' <summary>
    ''' Ham xu ly load du lieu tu DB len grid va filter
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub CreateDataFilter()
        Dim obj As New SalaryGroupDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgData, obj)
            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()
            rgData.VirtualItemCount = MaximumRows
            rgData.DataSource = New DataTable

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>23/08/2017</lastupdate>
    ''' <summary>
    ''' Update trang thai control khi an nut them moi, sua, xoa, luu, ap dung, ngung ap dung
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            EnabledGridNotPostback(rgData, True)
            Select Case CurrentState
                Case CommonMessage.STATE_NORMAL
                    ClearControlValue(txtFormula)
                    EnableControlAll(False, txtFormula)
                    ctrlCalculation.Enabled = False
                    ctrlListSalary.Enabled = False

                    EnabledGrid(rgData, True)

                Case CommonMessage.STATE_EDIT
                    EnableControlAll(True, txtFormula)
                    ctrlCalculation.Enabled = True
                    ctrlListSalary.Enabled = True
                    EnabledGrid(rgData, False)
            End Select

            ChangeToolbarState()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>23/08/2017</lastupdate>
    ''' <summary>
    ''' Get data va bind lên form, tren grid
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            GetParams()
            LoadListAllSalary()
            LoadCalculation()
            LoadListInputColumn()

            Dim dic As New Dictionary(Of String, Control)
            dic.Add("COL_INDEX", rnmIndex)
            dic.Add("FORMULER", txtFormula)
            Utilities.OnClientRowSelectedChanged(rgData, dic)

            CurrentState = CommonMessage.STATE_NORMAL

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"

    ''' <lastupdate>23/08/2017</lastupdate>
    ''' <summary>
    ''' Ham xu ly cac event click menu tren menu toolbar: them moi, sua, xoa, xuat excel, huy, Ap dung, Ngung Ap dung
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objSalaryGroup As New SalaryGroupDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim gId As Decimal

        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_APPROVE
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    If rgData.SelectedItems.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    Dim item As GridDataItem = rgData.SelectedItems(0)
                    Using rep As New PayrollRepository
                        If Not rep.CheckFomuler(item("COL_NAME").Text.ToUpper(), Utilities.ObjToString(txtFormula.Text.Trim), iGrpFomul) Then
                            ShowMessage(Translate("Cú pháp công thức chưa chính xác. Kiểm tra lại"), NotifyType.Warning)
                        Else
                            ShowMessage(Translate("Cú pháp công thức đã chính xác. Bạn có thể lưu"), NotifyType.Success)
                        End If
                    End Using

                    txtFormula.Focus()

                Case CommonMessage.TOOLBARITEM_SAVE
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    If rgData.SelectedItems.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    EnabledGrid(rgData, False)
                    Dim item As GridDataItem = rgData.SelectedItems(0)
                    Dim obj As New PAFomuler
                    obj.COL_NAME = item("COL_NAME").Text
                    obj.INDEX_FML = Utilities.ObjToDecima(rnmIndex.Value)
                    obj.FORMULER = Utilities.ObjToString(txtFormula.Text)
                    obj.GROUP_FML = iGrpFomul

                    Using rep As New PayrollRepository
                        If Not rep.CheckFomuler(item("COL_NAME").Text.ToUpper(), Utilities.ObjToString(txtFormula.Text.Trim()), iGrpFomul) Then
                            ShowMessage(Translate("Cú pháp công thức chưa chính xác. Kiểm tra lại"), NotifyType.Warning)
                            txtFormula.Focus()
                            Exit Sub
                        End If

                        If rep.SaveFomuler(obj, gId) Then
                            CurrentState = CommonMessage.STATE_NORMAL
                            Refresh("UpdateView")
                            SelectedItemDataGridByKey(rgData, gId, , rgData.CurrentPageIndex)
                        End If
                    End Using

                    EnabledGrid(rgData, True)
            End Select

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>23/08/2017</lastupdate>
    ''' <summary>
    ''' Load data len grid
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            rgData.DataSource = vListSalary
            rgData.VirtualItemCount = vListSalary.Count

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Custom"

    ''' <lastupdate>23/08/2017</lastupdate>
    ''' <summary>
    ''' Ham xử lý get tham số tư URL
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetParams()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            If CurrentState Is Nothing Then
                If Request.Params("gUID") IsNot Nothing Then
                    iGrpFomul = Decimal.Parse(Request.Params("gUID"))
                End If
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>23/08/2017</lastupdate>
    ''' <summary>
    ''' Load dữ liệu danh mục tính lương
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub LoadListAllSalary()
        Dim rep As New PayrollRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            vListSalary = rep.GetListAllSalary(iGrpFomul)
            rgData.Rebind()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>23/08/2017</lastupdate>
    ''' <summary>
    ''' Load dữ liệu column đầu vào
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub LoadListInputColumn()
        Dim rep As New PayrollRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            vListInputColum = rep.GetListInputColumn(iGrpFomul)
            ctrlListSalary.DataValueField = "COL_NAME"
            ctrlListSalary.DataTextField = "NAME_VN"
            ctrlListSalary.DataSource = vListInputColum
            ctrlListSalary.DataBind()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>23/08/2017</lastupdate>
    ''' <summary>
    ''' Load dữ liệu về công thức tinh toán
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub LoadCalculation()
        Dim rep As New PayrollRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            lstCalculation = rep.GetListCalculation()
            ctrlCalculation.DataValueField = "CODE"
            ctrlCalculation.DataTextField = "NAME_VN"
            ctrlCalculation.DataSource = lstCalculation
            ctrlCalculation.DataBind()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

End Class