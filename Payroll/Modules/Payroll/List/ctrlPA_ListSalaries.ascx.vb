Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Payroll.PayrollBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlPA_ListSalaries
    Inherits Common.CommonView

    ''' <creator>HongDX</creator>
    ''' <lastupdate>21/06/2017</lastupdate>
    ''' <summary>Write Log</summary>
    ''' <remarks>_mylog, _pathLog, _classPath</remarks>
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Payroll\Modules\Payroll\List" + Me.GetType().Name.ToString()

#Region "Property"

    ''' <summary>
    ''' IDSelect
    ''' </summary>
    ''' <remarks></remarks>
    Public IDSelect As Integer

    ''' <summary>
    ''' REGISTER_OT
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property PA_ListSalaries As List(Of PAListSalariesDTO)
        Get
            Return ViewState(Me.ID & "_PA_ListSalaries")
        End Get
        Set(ByVal value As List(Of PAListSalariesDTO))
            ViewState(Me.ID & "_PA_ListSalaries") = value
        End Set
    End Property

    ''' <summary>
    ''' ListComboData
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ListComboData As ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ListComboData")
        End Get
        Set(ByVal value As ComboBoxDataDTO)
            ViewState(Me.ID & "_ListComboData") = value
        End Set
    End Property

    ''' <summary>
    ''' Va
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ValueObjSalary As Decimal
        Get
            Return ViewState(Me.ID & "_ValueObjSalary")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_ValueObjSalary") = value
        End Set
    End Property

    ''' <summary>
    ''' ListComboData
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ValueGroupType As Decimal
        Get
            Return ViewState(Me.ID & "_ValueGroupType")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_ValueGroupType") = value
        End Set
    End Property

    ''' <summary>
    ''' ListComboData
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ValueField As Decimal
        Get
            Return ViewState(Me.ID & "_ValueField")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_ValueField") = value
        End Set
    End Property

#End Region

#Region "Page"

    ''' <lastupdate>22/08/2017</lastupdate>
    ''' <summary>
    ''' Ham duoc ke thua tu commonView va goi phuong thuc InitControl
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            SetGridFilter(rgData)
            rgData.AllowCustomPaging = True
            rgData.PageSize = 50
            InitControl()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>22/08/2017</lastupdate>
    ''' <summary>
    ''' Ke thua ViewLoad tu CommnView, la ham load du lieu, control states cua usercontrol
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
                Refresh()
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>22/08/2017</lastupdate>
    ''' <summary>Load cac control, menubar</summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMenu
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create,
                                       ToolbarItem.Edit, ToolbarItem.Seperator,
                                       ToolbarItem.Save, ToolbarItem.Cancel,
                                       ToolbarItem.Delete, ToolbarItem.Seperator,
                                       ToolbarItem.Active, ToolbarItem.Seperator,
                                       ToolbarItem.Deactive)

            CType(MainToolBar.Items(3), RadToolBarButton).CausesValidation = True
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>22/08/2017</lastupdate>
    ''' <summary>
    ''' Get data va bind lên form, tren grid
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            GetDataObject_Sal()

            Dim dic As New Dictionary(Of String, Control)
            dic.Add("ID", hidID)
            dic.Add("NAME_VN", txtNAME_VN)
            dic.Add("DATA_TYPE", cboDATA_TYPE)
            dic.Add("GROUP_TYPE_ID", cboGROUP_TYPE)
            dic.Add("COL_NAME;COL_NAME", cboFIELD)
            'dic.Add("OBJ_SAL_ID", cboOBJ_SALARY)
            dic.Add("COL_INDEX", nmCOL_INDEX)
            dic.Add("IS_VISIBLE", chkIS_VISIBLE)
            dic.Add("IS_IMPORT", chkIS_IMPORT)
            dic.Add("IS_WORKARISING", chkIS_WorkArising)
            dic.Add("IS_SUMARISING", chkIS_SumArising)
            dic.Add("IS_PAYBACK", chkIS_Payback)
            dic.Add("IS_SUMDAY", chkIS_SumDay)
            dic.Add("REMARK", txtRemark)
            dic.Add("EFFECTIVE_DATE", dtpEffect)

            Utilities.OnClientRowSelectedChanged(rgData, dic)

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>22/08/2017</lastupdate>
    ''' <summary>
    ''' Load lai du lieu tren grid khi thuc hien update, insert, delete
    ''' (neu co tham so truyen vao thi dua ra messager)
    ''' </summary>
    ''' <param name="Message">tham so truyen vao de phan biet trang thai Insert, Update, Cancel</param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        'Dim rep As New PayrollBusinessClient

        Try
            UpdateControlState()
            ChangeToolbarState()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>22/08/2017</lastupdate>
    ''' <summary>
    ''' Ham xu ly load du lieu tu DB len grid va filter
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub CreateDataFilter()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim objData As New List(Of PAListSalariesDTO)
        Dim _filter As New PAListSalariesDTO

        Try
            SetValueObjectByRadGrid(rgData, _filter)

            _filter.IS_DELETED = 0
            _filter.OBJ_SAL_ID = cboOBJ_SALARY.SelectedValue

            Dim MaximumRows As Integer
            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()

            Using rep As New PayrollRepository
                If Sorts IsNot Nothing Then
                    Me.PA_ListSalaries = rep.GetListSalaries(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, "COL_INDEX ASC")
                Else
                    Me.PA_ListSalaries = rep.GetListSalaries(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows)
                End If

                rgData.VirtualItemCount = MaximumRows
                rgData.DataSource = Me.PA_ListSalaries
            End Using

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>22/08/2017</lastupdate>
    ''' <summary>
    ''' Update trang thai control khi an nut them moi, sua, xoa, luu, ap dung, ngung ap dung
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Select Case CurrentState
                Case CommonMessage.STATE_NORMAL
                    EnabledGridNotPostback(rgData, True)
                    cboFIELD.AutoPostBack = False
                    cboFIELD.Text = ""
                    EnableControlAll(False, cboDATA_TYPE, txtNAME_VN, nmCOL_INDEX, chkIS_VISIBLE, chkIS_IMPORT, txtRemark, chkIS_Payback, chkIS_SumDay, chkIS_WorkArising, chkIS_SumArising, dtpEffect, cboGROUP_TYPE, cboFIELD)
                    rgData.Rebind()

                    chkIS_IMPORT.Checked = False
                    chkIS_Payback.Checked = False
                    chkIS_SumArising.Checked = False
                    chkIS_SumDay.Checked = False
                    chkIS_VISIBLE.Checked = False
                    chkIS_WorkArising.Checked = False
                    cboFIELD.Items.Clear()

                Case CommonMessage.STATE_NEW
                    rgData.Rebind()
                    EnabledGridNotPostback(rgData, False)
                    cboFIELD.AutoPostBack = True
                    EnableControlAll(True, cboDATA_TYPE, txtNAME_VN, nmCOL_INDEX, chkIS_VISIBLE, chkIS_IMPORT, txtRemark, chkIS_Payback, chkIS_SumDay, chkIS_WorkArising, chkIS_SumArising, dtpEffect, cboGROUP_TYPE, cboFIELD)
                    ClearControlValue(cboDATA_TYPE, txtNAME_VN, nmCOL_INDEX, chkIS_VISIBLE, chkIS_IMPORT, txtRemark, chkIS_Payback, chkIS_SumDay, chkIS_WorkArising, chkIS_SumArising, chkIS_Payback, dtpEffect, cboGROUP_TYPE, cboFIELD)
                    ExcuteScript("Clear", "clRadDatePicker()")

                Case CommonMessage.STATE_EDIT
                    EnabledGridNotPostback(rgData, False)
                    EnableControlAll(True, cboDATA_TYPE, txtNAME_VN, nmCOL_INDEX, chkIS_VISIBLE, chkIS_IMPORT, txtRemark, chkIS_Payback, chkIS_SumDay, chkIS_WorkArising, chkIS_SumArising, dtpEffect, cboGROUP_TYPE, cboFIELD)
                    GetField()

                Case CommonMessage.STATE_DEACTIVE, CommonMessage.STATE_ACTIVE, CommonMessage.STATE_DELETE
                    EnabledGridNotPostback(rgData, True)
                    EnableControlAll(False, cboDATA_TYPE, txtNAME_VN, nmCOL_INDEX, chkIS_VISIBLE, chkIS_IMPORT, txtRemark, chkIS_Payback, chkIS_SumDay, chkIS_WorkArising, chkIS_SumArising, dtpEffect, cboGROUP_TYPE, cboFIELD)
                    ClearControlValue(cboDATA_TYPE, txtNAME_VN, nmCOL_INDEX, chkIS_VISIBLE, chkIS_IMPORT, txtRemark, chkIS_Payback, chkIS_SumDay, chkIS_WorkArising, chkIS_SumArising, dtpEffect, cboGROUP_TYPE, cboFIELD)
            End Select

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"

    ''' <summary>
    ''' Event click button Tìm kiếm
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            rgData.Rebind()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>22/08/2017</lastupdate>
    ''' <summary>
    ''' Ham xu ly cac event click menu tren menu toolbar: them moi, sua, xoa, xuat excel, huy, Ap dung, Ngung Ap dung
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim gID As Decimal
        Dim objdata As PAListSalariesDTO

        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW

                Case CommonMessage.TOOLBARITEM_EDIT
                    If rgData.MasterTableView.GetSelectedItems().Length = 0 Then
                        ShowMessage(Translate("Bạn chưa chọn nội dung cần sửa."), NotifyType.Warning)
                        Exit Sub
                    End If

                    If rgData.SelectedItems.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    CurrentState = CommonMessage.STATE_EDIT

                Case CommonMessage.TOOLBARITEM_ACTIVE, CommonMessage.TOOLBARITEM_DEACTIVE, CommonMessage.TOOLBARITEM_DELETE
                    If rgData.MasterTableView.GetSelectedItems().Length = 0 Then
                        ShowMessage(Translate("Bạn chưa chọn nội dung cần thao tác."), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate("Bạn có chắc chắn muốn thực hiện ?")
                    ctrlMessageBox.ActionName = CType(e.Item, RadToolBarButton).CommandName
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                    Select Case CType(e.Item, RadToolBarButton).CommandName
                        Case CommonMessage.TOOLBARITEM_ACTIVE
                            CurrentState = CommonMessage.STATE_ACTIVE
                        Case CommonMessage.TOOLBARITEM_DEACTIVE
                            CurrentState = CommonMessage.STATE_DEACTIVE
                        Case CommonMessage.TOOLBARITEM_DELETE
                            CurrentState = CommonMessage.STATE_DELETE
                    End Select

                Case CommonMessage.TOOLBARITEM_SAVE
                    'If cboTYPE_PAYMENT.SelectedIndex < 0 Then
                    '    ShowMessage("Bạn chưa chọn bảng lương, kiểm tra lại!", NotifyType.Warning)
                    '    cboTYPE_PAYMENT.Focus()
                    '    Exit Sub
                    'End If
                    If Page.IsValid Then
                        Using rep As New PayrollRepository

                            objdata = New PAListSalariesDTO
                            objdata.COL_NAME = cboFIELD.SelectedValue
                            objdata.COL_CODE = cboFIELD.SelectedValue
                            objdata.TYPE_PAYMENT = Nothing
                            objdata.DATA_TYPE = cboDATA_TYPE.SelectedValue
                            objdata.NAME_VN = txtNAME_VN.Text.Trim
                            objdata.NAME_EN = txtNAME_VN.Text.Trim
                            objdata.INPUT_FORMULER = Nothing
                            objdata.COL_INDEX = Utilities.ObjToDecima(nmCOL_INDEX.Value)
                            objdata.IS_VISIBLE = chkIS_VISIBLE.Checked
                            objdata.IS_INPUT = Nothing
                            objdata.IS_IMPORT = chkIS_IMPORT.Checked
                            objdata.IS_CALCULATE = Nothing
                            objdata.IS_WORKDAY = Nothing
                            objdata.IS_SUMDAY = chkIS_SumDay.Checked
                            objdata.IS_WORKARISING = chkIS_WorkArising.Checked
                            objdata.IS_SUMARISING = chkIS_SumArising.Checked
                            objdata.IS_PAYBACK = chkIS_Payback.Checked

                            objdata.GROUP_TYPE_ID = If(String.IsNullOrEmpty(cboGROUP_TYPE.SelectedValue), Nothing, ValueGroupType)
                            objdata.OBJ_SAL_ID = If(String.IsNullOrEmpty(cboOBJ_SALARY.SelectedValue), Nothing, ValueObjSalary)
                            objdata.REMARK = txtRemark.Text.Trim
                            objdata.EFFECTIVE_DATE = dtpEffect.SelectedDate
                            objdata.EXPIRE_DATE = Nothing 'dtpExpire.SelectedDate

                            If CurrentState = CommonMessage.STATE_NEW Then
                                objdata.STATUS = "A"
                                objdata.IS_DELETED = 0

                                For Each item As PAListSalariesDTO In Me.PA_ListSalaries
                                    If item.NAME_VN Is Nothing Then
                                        Continue For
                                    End If
                                    If item.NAME_VN.ToLower = txtNAME_VN.Text.Trim.ToLower Then
                                        ShowMessage(Translate("Tên tiếng việt đã tồn tại"), NotifyType.Warning)
                                        Exit Sub
                                    End If
                                Next

                                If rep.InsertListSalaries(objdata, gID) Then
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                                End If
                                CurrentState = CommonMessage.STATE_NORMAL

                            ElseIf CurrentState = CommonMessage.STATE_EDIT Then

                                Dim cmRep As New CommonRepository
                                Dim lstID As New List(Of Decimal)

                                lstID.Add(Convert.ToDecimal(hidID.Value))

                                If cmRep.CheckExistIDTable(lstID, "PA_LISTSALARIES", "ID") Then
                                    ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXIST_DATABASE), NotifyType.Warning)
                                    Exit Sub
                                End If

                                objdata.ID = hidID.Value
                                If rep.ModifyListSalaries(objdata, gID) Then
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                                End If
                                CurrentState = CommonMessage.STATE_NORMAL
                            End If
                        End Using
                    Else
                        ExcuteScript("Resize", "setDefaultSize()")
                    End If

                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    ExcuteScript("Clear", "clRadDatePicker()")
                    ClearControlValue(cboDATA_TYPE, txtNAME_VN, nmCOL_INDEX, chkIS_VISIBLE, chkIS_IMPORT, txtRemark, chkIS_Payback, chkIS_SumDay, chkIS_WorkArising, chkIS_SumArising, chkIS_Payback, dtpEffect, cboGROUP_TYPE, cboFIELD)
                    cboFIELD.ClearSelection()
                    cboFIELD.Items.Clear()
            End Select

            Refresh()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>22/08/2017</lastupdate>
    ''' <summary>
    ''' Event Yes, No tren popup message khi click nut: xoa, ap dung, ngung ap dung
    ''' va Set trang thai cho form va update trang thai control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New PayrollRepository
        Dim lst As New List(Of Decimal)

        Try
            Dim item = rgData.SelectedItems

            For Each i As GridDataItem In item
                lst.Add(i.GetDataKeyValue("ID"))
            Next

            If e.ActionName = CommonMessage.TOOLBARITEM_ACTIVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_ACTIVE
                If rep.ActiveListSalaries(lst, "A") Then
                    CurrentState = CommonMessage.STATE_NORMAL
                End If

            ElseIf e.ActionName = CommonMessage.TOOLBARITEM_DEACTIVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DEACTIVE
                If rep.ActiveListSalaries(lst, "I") Then
                    CurrentState = CommonMessage.STATE_NORMAL
                End If

            ElseIf e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DELETE
                'If rep.DeleteListSalaries(lst) Then
                '    CurrentState = CommonMessage.STATE_NORMAL
                'End If
                If rep.DeleteListSalariesStatus(lst, 1) Then
                    CurrentState = CommonMessage.STATE_NORMAL
                End If
            Else
                CurrentState = CommonMessage.STATE_NORMAL
            End If
            rep.Dispose()
            Refresh()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>22/08/2017</lastupdate>
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
            CreateDataFilter()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <summary>
    ''' Event request item cua combobox FIELD
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub cboFIELD_ItemsRequested(ByVal sender As Object, ByVal e As RadComboBoxItemsRequestedEventArgs) _
    Handles cboFIELD.ItemsRequested
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Using rep As New PayrollRepository
                Dim dtData As DataTable
                Dim sText As String = e.Text
                Dim dValue As Decimal


                Select Case sender.ID
                    Case cboFIELD.ID
                        dValue = IIf(e.Context("valueCustom") IsNot Nothing, e.Context("valueCustom"), 0)
                        'dtData = rep.GetListSal(
                        'If e.Context("valueCustom") Is Nothing Then
                        '    dateValue = Date.Now
                        'Else
                        '    dateValue = Date.Parse(e.Context("valueCustom"))
                        'End If
                        dtData = rep.GetListSalColunm(dValue)
                End Select

                If sText <> "" Then
                    Select Case sender.ID
                        Case cboGROUP_TYPE.ID
                            Dim dtExist = (From p In dtData
                                           Where p("NAME") IsNot DBNull.Value AndAlso _
                                           p("NAME").ToString.ToUpper = sText.ToUpper)

                            If dtExist.Count = 0 Then
                                Dim dtFilter = (From p In dtData
                                                Where p("NAME") IsNot DBNull.Value AndAlso _
                                                p("NAME").ToString.ToUpper.Contains(sText.ToUpper))

                                If dtFilter.Count > 0 Then
                                    dtData = dtFilter.CopyToDataTable
                                Else
                                    dtData = dtData.Clone
                                End If

                                Dim itemOffset As Integer = e.NumberOfItems
                                Dim endOffset As Integer = Math.Min(itemOffset + sender.ItemsPerRequest, dtData.Rows.Count)
                                e.EndOfItems = endOffset = dtData.Rows.Count
                                sender.Items.Clear()

                                For i As Integer = itemOffset To endOffset - 1
                                    Dim radItem As RadComboBoxItem = New RadComboBoxItem(dtData.Rows(i)("NAME").ToString(), dtData.Rows(i)("ID").ToString())
                                    sender.Items.Add(radItem)
                                Next
                            Else
                                Dim itemOffset As Integer = e.NumberOfItems
                                Dim endOffset As Integer = dtData.Rows.Count
                                e.EndOfItems = True
                                sender.Items.Clear()

                                For i As Integer = itemOffset To endOffset - 1
                                    Dim radItem As RadComboBoxItem = New RadComboBoxItem(dtData.Rows(i)("NAME").ToString(), dtData.Rows(i)("ID").ToString())
                                    sender.Items.Add(radItem)
                                Next
                            End If

                        Case cboFIELD.ID
                            Dim dtExist = (From p In dtData
                                           Where p("COL_NAME") IsNot DBNull.Value AndAlso _
                                           p("COL_NAME").ToString.ToUpper = sText.ToUpper)

                            If dtExist.Count = 0 Then
                                Dim dtFilter = (From p In dtData
                                                Where p("COL_NAME") IsNot DBNull.Value AndAlso _
                                                p("COL_NAME").ToString.ToUpper.Contains(sText.ToUpper))

                                If dtFilter.Count > 0 Then
                                    dtData = dtFilter.CopyToDataTable
                                Else
                                    dtData = dtData.Clone
                                End If

                                Dim itemOffset As Integer = e.NumberOfItems
                                Dim endOffset As Integer = Math.Min(itemOffset + sender.ItemsPerRequest, dtData.Rows.Count)
                                e.EndOfItems = endOffset = dtData.Rows.Count
                                sender.Items.Clear()

                                For i As Integer = itemOffset To endOffset - 1
                                    Dim radItem As RadComboBoxItem = New RadComboBoxItem(dtData.Rows(i)("COL_NAME").ToString(), dtData.Rows(i)("COL_NAME").ToString())

                                    sender.Items.Add(radItem)
                                Next
                            Else
                                Dim itemOffset As Integer = e.NumberOfItems
                                Dim endOffset As Integer = dtData.Rows.Count
                                e.EndOfItems = True
                                sender.Items.Clear()

                                For i As Integer = itemOffset To endOffset - 1
                                    Dim radItem As RadComboBoxItem = New RadComboBoxItem(dtData.Rows(i)("COL_NAME").ToString(), dtData.Rows(i)("COL_NAME").ToString())

                                    sender.Items.Add(radItem)
                                Next
                            End If
                    End Select
                Else
                    Dim itemOffset As Integer = e.NumberOfItems
                    Dim endOffset As Integer = Math.Min(itemOffset + sender.ItemsPerRequest, dtData.Rows.Count)
                    e.EndOfItems = endOffset = dtData.Rows.Count

                    Select Case sender.ID
                        Case cboFIELD.ID
                            sender.Items.Clear()
                            For i As Integer = itemOffset To endOffset - 1
                                Dim radItem As RadComboBoxItem = New RadComboBoxItem(dtData.Rows(i)("COL_NAME").ToString(), dtData.Rows(i)("COL_NAME").ToString())

                                sender.Items.Add(radItem)
                            Next
                    End Select
                End If
            End Using

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <summary>
    ''' Kiem tra gia tri dang đươc chọn cua combox ĐỐI TƯỢNG LƯƠNG có tồn tại hoặc bị ngừng áp dụng hay không? 
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cvalOBJ_SALARY_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalOBJ_SALARY.ServerValidate
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New PayrollRepository

        Try
            ValueObjSalary = cboOBJ_SALARY.SelectedValue
            ListComboData = New ComboBoxDataDTO
            Dim dto As New PAObjectSalaryDTO
            Dim list As New List(Of PAObjectSalaryDTO)

            dto.ID = Convert.ToDecimal(cboOBJ_SALARY.SelectedValue)
            list.Add(dto)

            ListComboData.GET_OBJECT_PAYMENT = True
            ListComboData.LIST_OBJECT_PAYMENT = list

            If rep.ValidateCombobox(ListComboData) Then
                args.IsValid = True
            Else
                args.IsValid = False
                cboOBJ_SALARY.ClearSelection()
                rep.GetComboboxData(ListComboData)
                FillDropDownList(cboOBJ_SALARY, ListComboData.LIST_OBJECT_PAYMENT, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboOBJ_SALARY.SelectedValue)
                cboOBJ_SALARY.SelectedIndex = 0
            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <summary>
    ''' Kiem tra gia tri dang đươc chọn cua combox NHÓM KÝ HIỆU LƯƠNG có tồn tại hoặc bị ngừng áp dụng hay không? 
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cvalGROUP_TYPE_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalGROUP_TYPE.ServerValidate
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New PayrollRepository

        Try
            ValueGroupType = cboGROUP_TYPE.SelectedValue
            ListComboData = New ComboBoxDataDTO
            Dim dto As New OT_OTHERLIST_DTO
            Dim list As New List(Of OT_OTHERLIST_DTO)

            dto.ID = Convert.ToDecimal(cboGROUP_TYPE.SelectedValue)
            list.Add(dto)

            ListComboData.GET_GROUP_TYPE = True
            ListComboData.LIST_GROUP_TYPE = list

            If rep.ValidateCombobox(ListComboData) Then
                args.IsValid = True
            Else
                args.IsValid = False
                cboGROUP_TYPE.ClearSelection()
                rep.GetComboboxData(ListComboData)
                FillDropDownList(cboGROUP_TYPE, ListComboData.LIST_GROUP_TYPE, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboGROUP_TYPE.SelectedValue)
                cboGROUP_TYPE.SelectedIndex = 0
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    'Private Sub cboTYPE_PAYMENT_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboTYPE_PAYMENT.SelectedIndexChanged
    '    Try
    '        Dim s = cboTYPE_PAYMENT.SelectedValue
    '        CreateDataFilter()
    '    Catch ex As Exception
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '    End Try
    'End Sub

    'Private Sub cboTYPE_PAYMENT_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboTYPE_PAYMENT.SelectedIndexChanged
    '    rgData.Rebind()
    'End Sub

#End Region

#Region "Custom"

    'Private Sub GetDataCombo()
    '    Dim rep As New PayrollRepository
    '    Try
    '        If ListComboData Is Nothing Then
    '            ListComboData = New ComboBoxDataDTO
    '            ListComboData.GET_TYPE_PAYMENT = True
    '            rep.GetComboboxData(ListComboData)
    '        End If
    '        FillDropDownList(cboTYPE_PAYMENT, ListComboData.LIST_TYPE_PAYMENT, "NAME_VN", "ID", Common.Common.SystemLanguage, False, cboTYPE_PAYMENT.SelectedValue)            
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    ''' <lastupdate>22/08/2017</lastupdate>
    ''' <summary>
    ''' Get data và bind vao combobox
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetDataObject_Sal()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New PayrollRepository

        Try
            If ListComboData Is Nothing Then
                ListComboData = New ComboBoxDataDTO
                ListComboData.GET_OBJECT_PAYMENT = True
                ListComboData.GET_GROUP_TYPE = True
                rep.GetComboboxData(ListComboData)
            End If
            FillDropDownList(cboOBJ_SALARY, ListComboData.LIST_OBJECT_PAYMENT, "NAME_VN", "ID", Common.Common.SystemLanguage, False, cboOBJ_SALARY.SelectedValue)
            FillDropDownList(cboGROUP_TYPE, ListComboData.LIST_GROUP_TYPE, "NAME_VN", "ID", Common.Common.SystemLanguage, False, cboGROUP_TYPE.SelectedValue)

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub


    ''' <lastupdate>22/08/2017</lastupdate>
    ''' <summary>
    ''' Get data và bind vao combobox cboFIELD
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetField()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New PayrollRepository
        Dim dtData As DataTable

        Try
            cboFIELD.Items.Clear()
            dtData = rep.GetListSalColunm(Convert.ToDecimal(cboGROUP_TYPE.SelectedValue))
            For i As Integer = 0 To dtData.Rows.Count - 1
                Dim radItem As RadComboBoxItem = New RadComboBoxItem(dtData.Rows(i)("COL_NAME").ToString(), dtData.Rows(i)("COL_NAME").ToString())
                cboFIELD.Items.Add(radItem)
            Next

            For Each item As GridDataItem In rgData.SelectedItems
                cboFIELD.SelectedValue = item("COL_NAME").Text
            Next

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

End Class