Imports System.Globalization
Imports Attendance.AttendanceBusiness
Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlAtSeniority
    Inherits Common.CommonView

    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Attendance/Module/Attendance/List/" + Me.GetType().Name.ToString()
    Protected WithEvents ctrlFindOrgPopup As ctrlFindOrgPopup

#Region "Property"
    Public IDSelect As Integer
    Public Property At_Holiday As List(Of AT_SENIORITYDTO)
        Get
            Return ViewState(Me.ID & "_Holiday")
        End Get
        Set(ByVal value As List(Of AT_SENIORITYDTO))
            ViewState(Me.ID & "_Holiday") = value
        End Set
    End Property

    'Kieu man hinh tim kiem
    '0 - normal
    '1 - Employee
    '2 - Sign
    Property isLoadPopup As Integer
        Get
            Return ViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isLoadPopup") = value
        End Set
    End Property
#End Region

#Region "Page"

    ''' <summary>
    ''' Khởi tạo, load page
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Refresh("")
            UpdateControlState()
            SetGridFilter(rgDanhMuc)
            rgDanhMuc.AllowCustomPaging = True
            'rgDanhMuc.ClientSettings.EnablePostBackOnRowClick = True
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                        CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Khởi tạo control
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        InitControl()
    End Sub

    ''' <summary>
    ''' Load cac control, menubar
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarCostCenters
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create,
                                       ToolbarItem.Edit,
                                       ToolbarItem.Save, ToolbarItem.Cancel,
                                       ToolbarItem.Delete,
                                       ToolbarItem.Export)
            CType(MainToolBar.Items(2), RadToolBarButton).CausesValidation = True
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                        CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <summary>
    ''' Load lai du lieu tren grid khi thuc hien update, insert, delete
    ''' (neu co tham so truyen vao thi dua ra messager)
    ''' </summary>
    ''' <param name="Message">tham so truyen vao de phan biet trang thai Insert, Update, Cancel</param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New AttendanceBusinessClient
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgDanhMuc.Rebind()
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgDanhMuc.CurrentPageIndex = 0
                        rgDanhMuc.MasterTableView.SortExpressions.Clear()
                        rgDanhMuc.Rebind()
                    Case "Cancel"
                        rgDanhMuc.MasterTableView.ClearSelectedItems()
                End Select
            End If
            UpdateControlState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                        CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <summary>
    ''' Ham xu ly load du lieu tu DB len grid va filter
    ''' </summary>
    ''' <param name="isFull">Tham so xac dinh viec load full du lieu neu = false hoac Nothing
    ''' Load co phan trang neu isFull = true</param>
    ''' <returns>tra ve datatable</returns>
    ''' <remarks></remarks>
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim rep As New AttendanceRepository
        Dim obj As New AT_SENIORITYDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgDanhMuc, obj)
            Dim Sorts As String = rgDanhMuc.MasterTableView.SortExpressions.GetSortString()
            If Not isFull Then

                If Sorts IsNot Nothing Then
                    Me.At_Holiday = rep.GetAtSeniority(obj, rgDanhMuc.CurrentPageIndex, rgDanhMuc.PageSize, MaximumRows, Sorts)
                Else
                    Me.At_Holiday = rep.GetAtSeniority(obj, rgDanhMuc.CurrentPageIndex, rgDanhMuc.PageSize, MaximumRows)
                End If
                rgDanhMuc.VirtualItemCount = MaximumRows
                rgDanhMuc.DataSource = Me.At_Holiday
            Else
                Return rep.GetAtSeniority(obj).ToTable
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                        CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Update trang thai control khi an nut them moi, sua, xoa, luu, ap dung, ngung ap dung
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim rep As New AttendanceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow

            Select Case isLoadPopup
                Case 1
                    If Not phFindOrg.Controls.Contains(ctrlFindOrgPopup) Then
                        ctrlFindOrgPopup = Me.Register("ctrlFindOrgPopup", "Common", "ctrlFindOrgPopup")
                        phFindOrg.Controls.Add(ctrlFindOrgPopup)
                    End If
            End Select

            Select Case CurrentState
                Case CommonMessage.STATE_NORMAL
                    ClearControlValue(rdEffectDate, txtNote, ntxtYear1, ntxtYear2, ntxtYear3, ntxtYear4, ntxtYear5, ntxtYear6, ntxtYear7, ntxtYear8, ntxtYear9, ntxtYear10, ntxtYear11, ntxtYear12,
                                        ntxtYear13, ntxtYear14, ntxtYear15, ntxtYear16, ntxtYear17, ntxtYear18, ntxtYear19, ntxtYear20, ntxtYear21, ntxtYear22, ntxtYear23, ntxtYear24, ntxtYear25,
                                        ntxtYear26, ntxtYear27, ntxtYear28, ntxtYear29, ntxtYear30, ntxtYear31, ntxtYear32, ntxtYear33, ntxtYear34, ntxtYear35, ntxtYear36, ntxtYear37, ntxtYear38, ntxtYear39,
                                        ntxtYear40, ntxtYear41, ntxtYear42, ntxtYear43, ntxtYear44, ntxtYear45, ntxtYear46, ntxtYear47, ntxtYear48, ntxtYear49, ntxtYear50, ntxtYear51, ntxtYear52, ntxtYear53,
                                        ntxtYear54, ntxtYear55, ntxtYear56, ntxtYear57, ntxtYear58, ntxtYear59, ntxtYear60)

                    EnableControlAll(False, rdEffectDate, txtNote, ntxtYear1, ntxtYear2, ntxtYear3, ntxtYear4, ntxtYear5, ntxtYear6, ntxtYear7, ntxtYear8, ntxtYear9, ntxtYear10, ntxtYear11, ntxtYear12,
                                        ntxtYear13, ntxtYear14, ntxtYear15, ntxtYear16, ntxtYear17, ntxtYear18, ntxtYear19, ntxtYear20, ntxtYear21, ntxtYear22, ntxtYear23, ntxtYear24, ntxtYear25,
                                        ntxtYear26, ntxtYear27, ntxtYear28, ntxtYear29, ntxtYear30, ntxtYear31, ntxtYear32, ntxtYear33, ntxtYear34, ntxtYear35, ntxtYear36, ntxtYear37, ntxtYear38, ntxtYear39,
                                        ntxtYear40, ntxtYear41, ntxtYear42, ntxtYear43, ntxtYear44, ntxtYear45, ntxtYear46, ntxtYear47, ntxtYear48, ntxtYear49, ntxtYear50, ntxtYear51, ntxtYear52, ntxtYear53,
                                        ntxtYear54, ntxtYear55, ntxtYear56, ntxtYear57, ntxtYear58, ntxtYear59, ntxtYear60)

                    EnabledGridNotPostback(rgDanhMuc, True)
                Case CommonMessage.STATE_EDIT

                    EnableControlAll(True, rdEffectDate, txtNote, ntxtYear1, ntxtYear2, ntxtYear3, ntxtYear4, ntxtYear5, ntxtYear6, ntxtYear7, ntxtYear8, ntxtYear9, ntxtYear10, ntxtYear11, ntxtYear12,
                                        ntxtYear13, ntxtYear14, ntxtYear15, ntxtYear16, ntxtYear17, ntxtYear18, ntxtYear19, ntxtYear20, ntxtYear21, ntxtYear22, ntxtYear23, ntxtYear24, ntxtYear25,
                                        ntxtYear26, ntxtYear27, ntxtYear28, ntxtYear29, ntxtYear30, ntxtYear31, ntxtYear32, ntxtYear33, ntxtYear34, ntxtYear35, ntxtYear36, ntxtYear37, ntxtYear38, ntxtYear39,
                                        ntxtYear40, ntxtYear41, ntxtYear42, ntxtYear43, ntxtYear44, ntxtYear45, ntxtYear46, ntxtYear47, ntxtYear48, ntxtYear49, ntxtYear50, ntxtYear51, ntxtYear52, ntxtYear53,
                                        ntxtYear54, ntxtYear55, ntxtYear56, ntxtYear57, ntxtYear58, ntxtYear59, ntxtYear60)
                    EnabledGridNotPostback(rgDanhMuc, False)

                Case CommonMessage.STATE_DELETE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgDanhMuc.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgDanhMuc.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.DeleteAtSeniority(lstDeletes) Then
                        Refresh("UpdateView")
                        UpdateControlState()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        UpdateControlState()
                    End If
            End Select
            UpdateToolbarState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                        CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <summary>
    ''' Set ngon ngu cho cac lable control va cac column name tren grid
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow

            Dim dic As New Dictionary(Of String, Control)
            dic.Add("ID", hidID)
            dic.Add("EFFECTDATE", rdEffectDate)
            dic.Add("NOTE", txtNote)
            dic.Add("YEAR1", ntxtYear1)
            dic.Add("YEAR2", ntxtYear2)
            dic.Add("YEAR3", ntxtYear3)
            dic.Add("YEAR4", ntxtYear4)
            dic.Add("YEAR5", ntxtYear5)
            dic.Add("YEAR6", ntxtYear6)
            dic.Add("YEAR7", ntxtYear7)
            dic.Add("YEAR8", ntxtYear8)
            dic.Add("YEAR9", ntxtYear9)
            dic.Add("YEAR10", ntxtYear10)
            dic.Add("YEAR11", ntxtYear11)
            dic.Add("YEAR12", ntxtYear12)
            dic.Add("YEAR13", ntxtYear13)
            dic.Add("YEAR14", ntxtYear14)
            dic.Add("YEAR15", ntxtYear15)
            dic.Add("YEAR16", ntxtYear16)
            dic.Add("YEAR17", ntxtYear17)
            dic.Add("YEAR18", ntxtYear18)
            dic.Add("YEAR19", ntxtYear19)
            dic.Add("YEAR20", ntxtYear20)
            dic.Add("YEAR21", ntxtYear21)
            dic.Add("YEAR22", ntxtYear22)
            dic.Add("YEAR23", ntxtYear23)
            dic.Add("YEAR24", ntxtYear24)
            dic.Add("YEAR25", ntxtYear25)
            dic.Add("YEAR26", ntxtYear26)
            dic.Add("YEAR27", ntxtYear27)
            dic.Add("YEAR28", ntxtYear28)
            dic.Add("YEAR29", ntxtYear29)
            dic.Add("YEAR30", ntxtYear30)
            dic.Add("YEAR31", ntxtYear31)
            dic.Add("YEAR32", ntxtYear32)
            dic.Add("YEAR33", ntxtYear33)
            dic.Add("YEAR34", ntxtYear34)
            dic.Add("YEAR35", ntxtYear35)
            dic.Add("YEAR36", ntxtYear36)
            dic.Add("YEAR37", ntxtYear37)
            dic.Add("YEAR38", ntxtYear38)
            dic.Add("YEAR39", ntxtYear39)
            dic.Add("YEAR40", ntxtYear40)
            dic.Add("YEAR41", ntxtYear41)
            dic.Add("YEAR42", ntxtYear42)
            dic.Add("YEAR43", ntxtYear43)
            dic.Add("YEAR44", ntxtYear44)
            dic.Add("YEAR45", ntxtYear45)
            dic.Add("YEAR46", ntxtYear46)
            dic.Add("YEAR47", ntxtYear47)
            dic.Add("YEAR48", ntxtYear48)
            dic.Add("YEAR49", ntxtYear49)
            dic.Add("YEAR50", ntxtYear50)
            dic.Add("YEAR51", ntxtYear51)
            dic.Add("YEAR52", ntxtYear52)
            dic.Add("YEAR53", ntxtYear53)
            dic.Add("YEAR54", ntxtYear54)
            dic.Add("YEAR55", ntxtYear55)
            dic.Add("YEAR56", ntxtYear56)
            dic.Add("YEAR57", ntxtYear57)
            dic.Add("YEAR58", ntxtYear58)
            dic.Add("YEAR59", ntxtYear59)
            dic.Add("YEAR60", ntxtYear60)
            Utilities.OnClientRowSelectedChanged(rgDanhMuc, dic)
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub

#End Region

#Region "Event"

    ''' <summary>
    ''' Ham xu ly cac event click menu tren menu toolbar: them moi, sua, xoa, xuat excel, huy, Ap dung, Ngung Ap dung
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim obj As New AT_SENIORITYDTO
        Dim rep As New AttendanceRepository
        Dim gID As Decimal
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    EnableControlAll(True, rdEffectDate, txtNote, ntxtYear1, ntxtYear2, ntxtYear3, ntxtYear4, ntxtYear5, ntxtYear6, ntxtYear7, ntxtYear8, ntxtYear9, ntxtYear10, ntxtYear11, ntxtYear12,
                                    ntxtYear13, ntxtYear14, ntxtYear15, ntxtYear16, ntxtYear17, ntxtYear18, ntxtYear19, ntxtYear20, ntxtYear21, ntxtYear22, ntxtYear23, ntxtYear24, ntxtYear25,
                                    ntxtYear26, ntxtYear27, ntxtYear28, ntxtYear29, ntxtYear30, ntxtYear31, ntxtYear32, ntxtYear33, ntxtYear34, ntxtYear35, ntxtYear36, ntxtYear37, ntxtYear38, ntxtYear39,
                                    ntxtYear40, ntxtYear41, ntxtYear42, ntxtYear43, ntxtYear44, ntxtYear45, ntxtYear46, ntxtYear47, ntxtYear48, ntxtYear49, ntxtYear50, ntxtYear51, ntxtYear52, ntxtYear53,
                                    ntxtYear54, ntxtYear55, ntxtYear56, ntxtYear57, ntxtYear58, ntxtYear59, ntxtYear60)
                    ClearControlValue(rdEffectDate, txtNote, ntxtYear1, ntxtYear2, ntxtYear3, ntxtYear4, ntxtYear5, ntxtYear6, ntxtYear7, ntxtYear8, ntxtYear9, ntxtYear10, ntxtYear11, ntxtYear12,
                                        ntxtYear13, ntxtYear14, ntxtYear15, ntxtYear16, ntxtYear17, ntxtYear18, ntxtYear19, ntxtYear20, ntxtYear21, ntxtYear22, ntxtYear23, ntxtYear24, ntxtYear25,
                                        ntxtYear26, ntxtYear27, ntxtYear28, ntxtYear29, ntxtYear30, ntxtYear31, ntxtYear32, ntxtYear33, ntxtYear34, ntxtYear35, ntxtYear36, ntxtYear37, ntxtYear38, ntxtYear39,
                                        ntxtYear40, ntxtYear41, ntxtYear42, ntxtYear43, ntxtYear44, ntxtYear45, ntxtYear46, ntxtYear47, ntxtYear48, ntxtYear49, ntxtYear50, ntxtYear51, ntxtYear52, ntxtYear53,
                                        ntxtYear54, ntxtYear55, ntxtYear56, ntxtYear57, ntxtYear58, ntxtYear59, ntxtYear60)

                    rgDanhMuc.SelectedIndexes.Clear()
                    EnabledGridNotPostback(rgDanhMuc, False)
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_EDIT
                    If rgDanhMuc.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rgDanhMuc.SelectedItems.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    CurrentState = CommonMessage.STATE_EDIT
                    UpdateControlState()

                Case CommonMessage.TOOLBARITEM_ACTIVE
                    If rgDanhMuc.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_ACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_ACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case CommonMessage.TOOLBARITEM_DEACTIVE
                    If rgDanhMuc.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DEACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_DEACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case CommonMessage.TOOLBARITEM_DELETE
                    If rgDanhMuc.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        obj.EFFECTDATE = rdEffectDate.SelectedDate
                        obj.NOTE = txtNote.Text
                        obj.YEAR1 = If(ntxtYear1.Value Is Nothing, 0, ntxtYear1.Value)
                        obj.YEAR2 = If(ntxtYear2.Value Is Nothing, 0, ntxtYear2.Value)
                        obj.YEAR3 = If(ntxtYear3.Value Is Nothing, 0, ntxtYear3.Value)
                        obj.YEAR4 = If(ntxtYear4.Value Is Nothing, 0, ntxtYear4.Value)
                        obj.YEAR5 = If(ntxtYear5.Value Is Nothing, 0, ntxtYear5.Value)
                        obj.YEAR6 = If(ntxtYear6.Value Is Nothing, 0, ntxtYear6.Value)
                        obj.YEAR7 = If(ntxtYear7.Value Is Nothing, 0, ntxtYear7.Value)
                        obj.YEAR8 = If(ntxtYear8.Value Is Nothing, 0, ntxtYear8.Value)
                        obj.YEAR9 = If(ntxtYear9.Value Is Nothing, 0, ntxtYear9.Value)
                        obj.YEAR10 = If(ntxtYear10.Value Is Nothing, 0, ntxtYear10.Value)
                        obj.YEAR11 = If(ntxtYear11.Value Is Nothing, 0, ntxtYear11.Value)
                        obj.YEAR12 = If(ntxtYear12.Value Is Nothing, 0, ntxtYear12.Value)
                        obj.YEAR13 = If(ntxtYear13.Value Is Nothing, 0, ntxtYear13.Value)
                        obj.YEAR14 = If(ntxtYear14.Value Is Nothing, 0, ntxtYear14.Value)
                        obj.YEAR15 = If(ntxtYear15.Value Is Nothing, 0, ntxtYear15.Value)
                        obj.YEAR16 = If(ntxtYear16.Value Is Nothing, 0, ntxtYear16.Value)
                        obj.YEAR17 = If(ntxtYear17.Value Is Nothing, 0, ntxtYear17.Value)
                        obj.YEAR18 = If(ntxtYear18.Value Is Nothing, 0, ntxtYear18.Value)
                        obj.YEAR19 = If(ntxtYear19.Value Is Nothing, 0, ntxtYear19.Value)
                        obj.YEAR20 = If(ntxtYear20.Value Is Nothing, 0, ntxtYear20.Value)
                        obj.YEAR21 = If(ntxtYear21.Value Is Nothing, 0, ntxtYear21.Value)
                        obj.YEAR22 = If(ntxtYear22.Value Is Nothing, 0, ntxtYear22.Value)
                        obj.YEAR23 = If(ntxtYear23.Value Is Nothing, 0, ntxtYear23.Value)
                        obj.YEAR24 = If(ntxtYear24.Value Is Nothing, 0, ntxtYear24.Value)
                        obj.YEAR25 = If(ntxtYear25.Value Is Nothing, 0, ntxtYear25.Value)
                        obj.YEAR26 = If(ntxtYear26.Value Is Nothing, 0, ntxtYear26.Value)
                        obj.YEAR27 = If(ntxtYear27.Value Is Nothing, 0, ntxtYear27.Value)
                        obj.YEAR28 = If(ntxtYear28.Value Is Nothing, 0, ntxtYear28.Value)
                        obj.YEAR29 = If(ntxtYear29.Value Is Nothing, 0, ntxtYear29.Value)
                        obj.YEAR30 = If(ntxtYear30.Value Is Nothing, 0, ntxtYear30.Value)
                        obj.YEAR31 = If(ntxtYear31.Value Is Nothing, 0, ntxtYear31.Value)
                        obj.YEAR32 = If(ntxtYear32.Value Is Nothing, 0, ntxtYear32.Value)
                        obj.YEAR33 = If(ntxtYear33.Value Is Nothing, 0, ntxtYear33.Value)
                        obj.YEAR34 = If(ntxtYear34.Value Is Nothing, 0, ntxtYear34.Value)
                        obj.YEAR35 = If(ntxtYear35.Value Is Nothing, 0, ntxtYear35.Value)
                        obj.YEAR36 = If(ntxtYear36.Value Is Nothing, 0, ntxtYear36.Value)
                        obj.YEAR37 = If(ntxtYear37.Value Is Nothing, 0, ntxtYear37.Value)
                        obj.YEAR38 = If(ntxtYear38.Value Is Nothing, 0, ntxtYear38.Value)
                        obj.YEAR39 = If(ntxtYear39.Value Is Nothing, 0, ntxtYear39.Value)
                        obj.YEAR40 = If(ntxtYear40.Value Is Nothing, 0, ntxtYear40.Value)
                        obj.YEAR41 = If(ntxtYear41.Value Is Nothing, 0, ntxtYear41.Value)
                        obj.YEAR42 = If(ntxtYear42.Value Is Nothing, 0, ntxtYear42.Value)
                        obj.YEAR43 = If(ntxtYear43.Value Is Nothing, 0, ntxtYear43.Value)
                        obj.YEAR44 = If(ntxtYear44.Value Is Nothing, 0, ntxtYear44.Value)
                        obj.YEAR45 = If(ntxtYear45.Value Is Nothing, 0, ntxtYear45.Value)
                        obj.YEAR46 = If(ntxtYear46.Value Is Nothing, 0, ntxtYear46.Value)
                        obj.YEAR47 = If(ntxtYear47.Value Is Nothing, 0, ntxtYear47.Value)
                        obj.YEAR48 = If(ntxtYear48.Value Is Nothing, 0, ntxtYear48.Value)
                        obj.YEAR49 = If(ntxtYear49.Value Is Nothing, 0, ntxtYear49.Value)
                        obj.YEAR50 = If(ntxtYear50.Value Is Nothing, 0, ntxtYear50.Value)
                        obj.YEAR51 = If(ntxtYear51.Value Is Nothing, 0, ntxtYear51.Value)
                        obj.YEAR52 = If(ntxtYear52.Value Is Nothing, 0, ntxtYear52.Value)
                        obj.YEAR53 = If(ntxtYear53.Value Is Nothing, 0, ntxtYear53.Value)
                        obj.YEAR54 = If(ntxtYear54.Value Is Nothing, 0, ntxtYear54.Value)
                        obj.YEAR55 = If(ntxtYear55.Value Is Nothing, 0, ntxtYear55.Value)
                        obj.YEAR56 = If(ntxtYear56.Value Is Nothing, 0, ntxtYear56.Value)
                        obj.YEAR57 = If(ntxtYear57.Value Is Nothing, 0, ntxtYear57.Value)
                        obj.YEAR58 = If(ntxtYear58.Value Is Nothing, 0, ntxtYear58.Value)
                        obj.YEAR59 = If(ntxtYear59.Value Is Nothing, 0, ntxtYear59.Value)
                        obj.YEAR60 = If(ntxtYear60.Value Is Nothing, 0, ntxtYear60.Value)

                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                If rep.CheckAtSeniority_DATE(obj) = False Then
                                    ShowMessage(Translate("Đã tồn tại thông tin đã thiết lập."), Utilities.NotifyType.Warning)
                                    Exit Sub
                                End If

                                If rep.InsertAtSeniority(obj, gID) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = gID
                                    Refresh("InsertView")
                                    UpdateControlState()
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT
                                obj.ID = rgDanhMuc.SelectedValue
                                If rep.CheckAtSeniority_DATE(obj) = False Then
                                    ShowMessage(Translate("Đã tồn tại thông tin đã thiết lập."), Utilities.NotifyType.Warning)
                                    Exit Sub
                                End If
                                If rep.ModifyAtSeniority(obj, rgDanhMuc.SelectedValue) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = obj.ID
                                    Refresh("UpdateView")
                                    UpdateControlState()
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select
                        rgDanhMuc.SelectedIndexes.Clear()
                    Else
                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    Refresh("Cancel")
                    UpdateControlState()

                Case CommonMessage.TOOLBARITEM_EXPORT
                    Using xls As New ExcelCommon
                        Dim dtDatas As DataTable
                        dtDatas = CreateDataFilter(True)
                        If dtDatas.Rows.Count > 0 Then
                            rgDanhMuc.ExportExcel(Server, Response, dtDatas, "Thiết lập thâm niên")
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                            Exit Sub
                        End If
                    End Using
            End Select
            CreateDataFilter()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                        CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Function CheckDate(ByVal value As String, ByRef result As Date) As Boolean
        Dim dateCheck As Boolean
        If value = "" Or value = "&nbsp;" Then
            value = ""
            Return True
        End If

        Try
            dateCheck = DateTime.TryParseExact(value, "dd/MM/yyyy", New CultureInfo("en-US"), DateTimeStyles.None, result)
            Return dateCheck
        Catch ex As Exception
            Return False
        End Try
    End Function

    Protected Sub rgDanhMuc_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rgDanhMuc.SelectedIndexChanged
        Dim dtdata As DataTable = Nothing
        Dim item = CType(rgDanhMuc.SelectedItems(0), GridDataItem)
        Dim ID = item.GetDataKeyValue("ID").ToString
        Dim At_ListParamSystem1 = (From p In At_Holiday Where p.ID = Decimal.Parse(ID)).SingleOrDefault
    End Sub

    ''' <summary>
    ''' Event Yes, No tren popup message khi click nut: xoa, ap dung, ngung ap dung
    ''' va Set trang thai cho form va update trang thai control 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If e.ActionName = CommonMessage.TOOLBARITEM_ACTIVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_ACTIVE
                UpdateControlState()
            End If
            If e.ActionName = CommonMessage.TOOLBARITEM_DEACTIVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DEACTIVE
                UpdateControlState()
            End If
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DELETE
                UpdateControlState()
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try
    End Sub

    ''' <summary>
    ''' Load data len grid
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgDanhMuc.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            CreateDataFilter()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                        CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Custom"

    ''' <summary>
    ''' Update trang thai menu toolbar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub UpdateToolbarState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            ChangeToolbarState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                       CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub
#End Region

End Class