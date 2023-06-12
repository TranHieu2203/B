Imports Common.CommonBusiness
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlListGroupNewEdit
    Inherits CommonView
    Public Overrides Property MustAuthorize As Boolean = False
    Protected WithEvents UserViewGroup As ViewBase
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Common/Modules/Common/Secure/" + Me.GetType().Name.ToString()

#Region "Property"

    ''' <summary>
    ''' Obj CommonBusiness
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Group As CommonBusiness.GroupDTO
        Get
            Return PageViewState(Me.ID & "_Group")
        End Get
        Set(ByVal value As CommonBusiness.GroupDTO)
            PageViewState(Me.ID & "_Group") = value
        End Set
    End Property

    ''' <summary>
    ''' Obj ListGroups
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ListGroups As List(Of GroupDTO)
        Get
            Return PageViewState(Me.ID & "_ListGroups")
        End Get
        Set(ByVal value As List(Of GroupDTO))
            PageViewState(Me.ID & "_ListGroups") = value
        End Set
    End Property
#End Region

#Region "Page"

    ''' <lastupdate>
    ''' 25/07/2017 15:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức ViewLoad, hiển thị dữ liệu trên trang
    ''' Làm mới trang
    ''' Cập nhật các trạng thái của các control trên page
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            UpdateControlState()
            Refresh()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 25/07/2017 15:00
    ''' </lastupdate>
    ''' <summary>
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 25/07/2017 15:00
    ''' </lastupdate>
    ''' <summary>
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow

            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 25/07/2017 15:00
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức enable data grid, control theo state
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CurrentState
                Case CommonMessage.STATE_NORMAL, ""
                    rtGROUP_CODE.ReadOnly = True
                    rtGROUP_NAME.ReadOnly = True
                    EnableControlAll(False, chkIsOrgPermission, chkIsFunctionPermission)
                    Utilities.EnableRadDatePicker(rdEFFECT_DATE, False)
                    Utilities.EnableRadDatePicker(rdEXPIRE_DATE, False)
                Case CommonMessage.STATE_EDIT, CommonMessage.STATE_NEW
                    rtGROUP_CODE.ReadOnly = False
                    rtGROUP_NAME.ReadOnly = False
                    EnableControlAll(True, chkIsOrgPermission, chkIsFunctionPermission)
                    Utilities.EnableRadDatePicker(rdEFFECT_DATE, True)
                    Utilities.EnableRadDatePicker(rdEXPIRE_DATE, True)
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try
    End Sub

    ''' <lastupdate>
    ''' 25/07/2017 15:00
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc lam moi cac control theo tuy chon state
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If Message = CommonMessage.ACTION_SAVED Then
                BindData()
            End If

            Select Case CurrentState
                Case CommonMessage.STATE_EDIT, CommonMessage.STATE_NORMAL
                    If Group IsNot Nothing Then
                        rtGROUP_CODE.Text = Group.CODE
                        rtGROUP_NAME.Text = Group.NAME
                        rdEFFECT_DATE.SelectedDate = Group.EFFECT_DATE
                        rdEXPIRE_DATE.SelectedDate = Group.EXPIRE_DATE
                        hidID.Value = Group.ID.ToString
                        If Group.IS_FUNCTION_PERMISSION IsNot Nothing Then
                            chkIsFunctionPermission.Checked = Group.IS_FUNCTION_PERMISSION
                        Else
                            chkIsFunctionPermission.Checked = False
                        End If
                        If Group.IS_FUNCTION_PERMISSION IsNot Nothing Then
                            chkIsOrgPermission.Checked = Group.IS_ORG_PERMISSION
                        Else
                            chkIsFunctionPermission.Checked = False
                        End If
                    Else
                        If Not IsPostBack Then
                            rtGROUP_CODE.Text = ""
                            rtGROUP_NAME.Text = ""
                            rdEFFECT_DATE.SelectedDate = Nothing
                            rdEXPIRE_DATE.SelectedDate = Nothing
                            chkIsFunctionPermission.Checked = False
                            chkIsOrgPermission.Checked = False
                        End If
                    End If
                Case CommonMessage.STATE_NEW
                    If Not IsPostBack Then
                        rtGROUP_CODE.Text = ""
                        rtGROUP_NAME.Text = ""
                        rdEFFECT_DATE.SelectedDate = Nothing
                        rdEXPIRE_DATE.SelectedDate = Nothing
                        chkIsFunctionPermission.Checked = False
                        chkIsOrgPermission.Checked = False
                    End If
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
        MyBase.Refresh(Message)
    End Sub
#End Region

#Region "Event"

    ''' <lastupdate>
    ''' 25/07/2017 15:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien command khi click item tren toolbar
    ''' Command lưu
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs)
        Dim objGroup As New CommonBusiness.GroupDTO
        Dim lstConflict As New List(Of Decimal)
        Dim rep As New CommonRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        objGroup.NAME = rtGROUP_NAME.Text.Trim
                        objGroup.CODE = rtGROUP_CODE.Text.Trim
                        objGroup.IS_ADMIN = False
                        objGroup.IS_FUNCTION_PERMISSION = chkIsFunctionPermission.Checked
                        objGroup.IS_ORG_PERMISSION = chkIsOrgPermission.Checked
                        If Not chkIsFunctionPermission.Checked And Not chkIsOrgPermission.Checked Then
                            ShowMessage(Translate("Bạn phải chọn Nhóm PQ chức năng hoặc Nhóm PQ SĐTC"), NotifyType.Warning)
                            Exit Sub
                        End If
                        objGroup.EFFECT_DATE = rdEFFECT_DATE.SelectedDate
                        objGroup.EXPIRE_DATE = rdEXPIRE_DATE.SelectedDate
                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                If rep.InsertGroup(objGroup) Then
                                    Me.Send(CommonMessage.ACTION_SUCCESS)
                                Else
                                    Me.Send(CommonMessage.ACTION_UNSUCCESS)
                                End If
                            Case CommonMessage.STATE_EDIT
                                objGroup.ID = Decimal.Parse(hidID.Value)
                                Dim lstID As New List(Of Decimal)
                                lstID.Add(objGroup.ID)
                                Using ret As New CommonRepository
                                    If ret.CheckExistIDTable(lstID, "SE_GROUP", "ID") Then
                                        ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXIST_DATABASE), NotifyType.Error)
                                        Exit Sub
                                    End If
                                End Using
                                If rep.UpdateGroup(objGroup) Then
                                    Me.Send(CommonMessage.ACTION_SUCCESS)
                                Else
                                    Me.Send(CommonMessage.ACTION_UNSUCCESS)
                                End If
                        End Select
                    End If
                    UserViewGroup = Me.Register("ctrlListGroup", "Common", "ctrlListGroup", "Secure")
                    UserViewGroup.ReSize()
                Case CommonMessage.TOOLBARITEM_CANCEL
                    Me.Send(CommonMessage.ACTION_CANCEL)
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub chkIsFunctionPermission_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkIsFunctionPermission.CheckedChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            If chkIsFunctionPermission.Checked Then
                chkIsOrgPermission.Checked = False
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub chkIsOrgPermission_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkIsOrgPermission.CheckedChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            If chkIsOrgPermission.Checked Then
                chkIsFunctionPermission.Checked = False
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 25/07/2017 15:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện validate cho rtGROUP_CODE
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub CustomValidator1_ServerValidate(source As Object, args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles CustomValidator1.ServerValidate
        Dim rep As New CommonRepository
        Dim _validate As New GroupDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If CurrentState = CommonMessage.STATE_EDIT Then
                If rtGROUP_CODE.Text.ToUpper.Trim = Group.CODE.ToUpper.Trim Then
                    args.IsValid = True
                Else
                    _validate.CODE = rtGROUP_CODE.Text
                    args.IsValid = rep.ValidateGroupList(_validate)
                End If
            Else
                _validate.CODE = rtGROUP_CODE.Text
                args.IsValid = rep.ValidateGroupList(_validate)
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 25/07/2017 15:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện validate cho rtGROUP_NAME
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub CustomValidator2_ServerValidate(source As Object, args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles CustomValidator2.ServerValidate
        Dim rep As New CommonRepository
        Dim _validate As New GroupDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If CurrentState = CommonMessage.STATE_EDIT Then
                If rtGROUP_NAME.Text.Trim = Group.NAME.Trim Then
                    args.IsValid = True
                Else
                    _validate.NAME = rtGROUP_NAME.Text
                    _validate.ID = Group.ID
                    args.IsValid = rep.ValidateGroupList(_validate)
                End If
            Else
                _validate.NAME = rtGROUP_NAME.Text
                args.IsValid = rep.ValidateGroupList(_validate)
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Custom"

#End Region

End Class