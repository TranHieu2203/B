Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic
Imports Attendance.AttendanceBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlSetupGPSNewEdit
    Inherits Common.CommonView
    Protected repHF As HistaffFrameworkRepository

    Public lngDefault As String = "105.8342"
    Public latDefault As String = "21.0278"
    Public radDefault As Decimal = 300
    Public zoomDefault As Decimal = 15
    Property PosSelect As Decimal
        Get
            Return ViewState(Me.ID & "_Organization")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_Organization") = value
        End Set
    End Property

    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Attendance\Modules\Attendance\List" + Me.GetType().Name.ToString()

    Protected WithEvents ctrlFindOrgPopupOM As ctrlFindOrgPopup

    Property isLoadPopup As Integer
        Get
            Return ViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isLoadPopup") = value
        End Set
    End Property
    Public Property IDSelected As Decimal
        Get
            Return ViewState(Me.ID & "_IDSelected")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_IDSelected") = value
        End Set
    End Property

    Private Sub GetParams()
        Try
            If CurrentState Is Nothing Then
                hidLatDefault.Value = latDefault
                hidLngDefault.Value = lngDefault
                hidRadiusDefault.Value = radDefault
                hidZoomDefault.Value = 15
                If Request.Params("ID") IsNot Nothing Then
                    IDSelected = Request.Params("ID")
                End If
                If IDSelected <> 0 Then
                    Refresh("UpdateView")
                Else
                    Refresh("InsertView")
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#Region "Property"

#End Region

#Region "Page"

    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary>Load page, trang thai page, trang thai control</summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' 
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim starttime As DateTime = DateTime.UtcNow
        Try
            GetParams()
            UpdateControlState()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(starttime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary> Khoi tao control, set thuoc tinh cho grid </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            InitControl()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary>Khoi tao, load menu toolbar</summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Try
            Me.MainToolBar = tbarMain
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Save, ToolbarItem.Cancel, ToolbarItem.Seperator)
            CType(MainToolBar.Items(0), RadToolBarButton).CausesValidation = True
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <summary>
    ''' Reset, Load page theo trạng thái page
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New AttendanceRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim _filter As New AT_SETUP_WIFI_GPS_DTO
        Try
            Select Case Message
                Case "UpdateView"
                    CurrentState = CommonMessage.STATE_EDIT
                    _filter.ID = IDSelected
                    Dim obj = rep.GetSetupGPSByID(_filter)
                    If obj IsNot Nothing Then
                        txtNameVN.Text = obj.NAME
                        txtaddress.Text = obj.ADDRESS
                        txtLatVD.Text = obj.LAT_VD
                        txtLongKD.Text = obj.LONG_KD
                        hidLat.Value = obj.LAT_VD
                        hidLng.Value = obj.LONG_KD
                        If obj.RADIUS IsNot Nothing Then
                            txtRadius.Value = obj.RADIUS
                            hidRadius.Value = obj.RADIUS
                        End If
                        If obj.ORG_ID IsNot Nothing Then
                            hidOrgID.Value = obj.ORG_ID
                        End If
                        txtOrgName.Text = obj.ORG_NAME
                    End If
                Case "InsertView"
                    CurrentState = CommonMessage.STATE_NEW
                    hidRadius.Value = radDefault.ToString
                    hidLat.Value = latDefault.ToString
                    hidLng.Value = lngDefault.ToString
            End Select

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Event"
    ''' <summary>
    ''' Event Click item trên menu toolbar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objHoliday As New AT_SETUP_WIFI_GPS_DTO
        Dim rep As New AttendanceRepository
        Dim gID As Decimal
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        If IsNumeric(txtLatVD.Text) Then
                            If Double.Parse(txtLatVD.Text.Replace(".", ",")) < -90 Or Double.Parse(txtLatVD.Text.Replace(".", ",")) > 90 Then
                                ShowMessage("Vĩ độ không hợp lệ", Utilities.NotifyType.Warning)
                                InitMap()
                                Exit Sub
                            End If
                        End If
                        If IsNumeric(txtLongKD.Text) Then
                            If Double.Parse(txtLongKD.Text.Replace(".", ",")) < -180 Or Double.Parse(txtLongKD.Text.Replace(".", ",")) > 180 Then
                                ShowMessage("Kinh độ không hợp lệ", Utilities.NotifyType.Warning)
                                InitMap()
                                Exit Sub
                            End If
                        End If
                        If txtRadius.Value <= 0 Then
                            ShowMessage("Bán kính không hợp lệ", Utilities.NotifyType.Warning)
                            InitMap()
                            Exit Sub
                        End If
                        objHoliday.NAME = txtNameVN.Text
                        objHoliday.ADDRESS = txtaddress.Text
                        objHoliday.LAT_VD = txtLatVD.Text
                        objHoliday.LONG_KD = txtLongKD.Text
                        objHoliday.RADIUS = CDec(Val(txtRadius.Value))
                        objHoliday.ORG_ID = CDec(Val(hidOrgID.Value))
                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                Dim Validate As New AT_HOLIDAYDTO
                                objHoliday.ACTFLG = "A"
                                'If rep.ValidateWIFI_GPS(objHoliday.ORG_ID, 0, "gps") <> 0 Then
                                '    ShowMessage("Đơn vị đã thiết lập chấm công GPS", Utilities.NotifyType.Error)
                                '    Exit Sub
                                'End If
                                If rep.InsertSetupGPS(objHoliday, gID) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelected = gID
                                    Refresh("InsertView")
                                    UpdateControlState()
                                    Dim str As String = "getRadWindow().close('1');"
                                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT
                                Dim Validate As New AT_SETUP_WIFI_GPS_DTO
                                Validate.ID = IDSelected

                                'If rep.ValidateWIFI_GPS(objHoliday.ORG_ID, Validate.ID, "gps") <> 0 Then
                                '    ShowMessage("Đơn vị đã thiết lập chấm công GPS", Utilities.NotifyType.Error)
                                '    Exit Sub
                                'End If

                                objHoliday.ID = IDSelected
                                If rep.ModifySetupGPS(objHoliday, IDSelected) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelected = objHoliday.ID
                                    Refresh("UpdateView")
                                    UpdateControlState()
                                    Dim str As String = "getRadWindow().close('1');"
                                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select
                    End If
                    InitMap()
                Case CommonMessage.TOOLBARITEM_CANCEL
                    Dim str As String = "getRadWindow().close('1');"
                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New AttendanceRepository
        Try
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub InitMap()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            hidLat.Value = txtLatVD.Text
            hidLng.Value = txtLongKD.Text
            If txtRadius.Value IsNot Nothing Then
                hidRadius.Value = txtRadius.Value
            End If
            If hidLat.Value = "" Then
                hidLat.Value = latDefault.ToString
            End If
            If hidLng.Value = "" Then
                hidLng.Value = lngDefault.ToString
            End If
            Dim str As String = "initMap();"
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                       CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub
    Protected Sub btnFindOrg_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFindOrg.Click
        Try
            isLoadPopup = 2
            UpdateControlState()
            ctrlFindOrgPopupOM.Show()
            InitMap()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub ctrlFindOrgPopupOM_CancelClick(ByVal sender As Object,
                                     ByVal e As System.EventArgs) Handles _
                                 ctrlFindOrgPopupOM.CancelClicked
        isLoadPopup = 0
        InitMap()
    End Sub

    Private Sub ctrlFindOrgPopupOM_OrganizationSelected(ByVal sender As Object, ByVal e As Common.OrganizationSelectedEventArgs) Handles ctrlFindOrgPopupOM.OrganizationSelected
        Try
            Dim orgItem = ctrlFindOrgPopupOM.CurrentItemDataObject
            If orgItem IsNot Nothing Then
                hidOrgID.Value = e.CurrentValue
                txtOrgName.Text = orgItem.NAME_VN
                If orgItem.DESCRIPTION_PATH IsNot Nothing AndAlso orgItem.DESCRIPTION_PATH <> "" Then
                    txtOrgName.ToolTip = Utilities.DrawTreeByString(orgItem.DESCRIPTION_PATH)
                End If
            End If
            isLoadPopup = 0
            InitMap()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region


    Public Overrides Sub UpdateControlState()
        Try
            If phFindOrg.Controls.Contains(ctrlFindOrgPopupOM) Then
                phFindOrg.Controls.Remove(ctrlFindOrgPopupOM)
            End If
            Select Case isLoadPopup
                Case 2
                    If Not phFindOrg.Controls.Contains(ctrlFindOrgPopupOM) Then
                        ctrlFindOrgPopupOM = Me.Register("ctrlFindOrgPopup", "Common", "ctrlFindOrgPopup")
                        ctrlFindOrgPopupOM.OrganizationType = OrganizationType.OrganizationLocation
                        phFindOrg.Controls.Add(ctrlFindOrgPopupOM)
                    End If
            End Select
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class