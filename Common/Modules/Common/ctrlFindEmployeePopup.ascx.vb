﻿Imports Common.CommonBusiness
Imports Telerik.Web.UI

Public Class ctrlFindEmployeePopup
    Inherits CommonView

    Delegate Sub EmployeeSelectedDelegate(ByVal sender As Object, ByVal e As EventArgs)
    Event EmployeeSelected As EmployeeSelectedDelegate
    Delegate Sub CancelClick(ByVal sender As Object, ByVal e As EventArgs)
    Event CancelClicked As CancelClick
    Public Overrides Property MustAuthorize As Boolean = False
#Region "Property"
    Private EmployeeListID As List(Of Decimal)
    Public Property popupId As String
    Public WithEvents AjaxManager As RadAjaxManager
    Public AjaxLoading As RadAjaxLoadingPanel
    Public Property AjaxManagerId As String
    Public Property AjaxLoadingId As String

    Public Property LoadAllOrganization As Boolean
        Get
            If ViewState(Me.ID & "_LoadAllOrganization") Is Nothing Then
                ViewState(Me.ID & "_LoadAllOrganization") = False
            End If
            Return ViewState(Me.ID & "_LoadAllOrganization")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_LoadAllOrganization") = value
        End Set
    End Property

    Public ReadOnly Property SelectedEmployee As List(Of EmployeePopupFindDTO)
        Get
            Using rep As New CommonRepository
                Return rep.GetEmployeeToPopupFind_EmployeeID(EmployeeListID)
            End Using
        End Get
    End Property

    Public ReadOnly Property SelectedEmployeeID As List(Of Decimal)
        Get
            Return EmployeeListID
        End Get
    End Property

    ''' <summary>
    ''' Phải có hợp đồng
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MustHaveContract As Boolean
        Get
            If ViewState(Me.ID & "_MustHaveContract") Is Nothing Then
                ViewState(Me.ID & "_MustHaveContract") = True
            End If
            Return ViewState(Me.ID & "_MustHaveContract")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_MustHaveContract") = value
        End Set
    End Property

    Public Property CurrentValue As String
        Get
            If ViewState(Me.ID & "_CurrentValue") Is Nothing Then
                ViewState(Me.ID & "_CurrentValue") = ""
            End If
            Return ViewState(Me.ID & "_CurrentValue")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_CurrentValue") = value
        End Set
    End Property

    ''' <summary>
    ''' Có chọn nhiều nhân viên không?
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MultiSelect() As Boolean
        Get
            If ViewState(Me.ID & "_MultiSelect") Is Nothing Then
                ViewState(Me.ID & "_MultiSelect") = True
            End If
            Return ViewState(Me.ID & "_MultiSelect")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_MultiSelect") = value
        End Set
    End Property

    ''' <summary>
    ''' Có ẩn nút nghỉ việc đi hay không
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IsHideTerminate() As Boolean
        Get
            If ViewState(Me.ID & "_IsHideTerminate") Is Nothing Then
                ViewState(Me.ID & "_IsHideTerminate") = True
            End If
            Return ViewState(Me.ID & "_IsHideTerminate")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_IsHideTerminate") = value
        End Set
    End Property

    Public Property IsShowKiemNhiem() As Boolean
        Get
            If ViewState(Me.ID & "_IsShowKiemNhiem") Is Nothing Then
                ViewState(Me.ID & "_IsShowKiemNhiem") = False
            End If
            Return ViewState(Me.ID & "_IsShowKiemNhiem")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_IsShowKiemNhiem") = value
        End Set
    End Property

    ''' <summary>
    ''' Chỉ hiển thị nhân viên đang làm việc (không bao gồm nhân viên đang chờ nghỉ việc)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IsOnlyWorkingWithoutTer() As Boolean
        Get
            If ViewState(Me.ID & "_IsOnlyWorkingWithoutTer") Is Nothing Then
                ViewState(Me.ID & "_IsOnlyWorkingWithoutTer") = False
            End If
            Return ViewState(Me.ID & "_IsOnlyWorkingWithoutTer")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_IsOnlyWorkingWithoutTer") = value
        End Set
    End Property

    ''' <summary>
    ''' 0. Load tất
    ''' 1. Load 3b
    ''' 2. không load 3b
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IS_3B() As String
        Get
            If ViewState(Me.ID & "_IS_3B") Is Nothing Then
                ViewState(Me.ID & "_IS_3B") = 0
            End If
            Return ViewState(Me.ID & "_IS_3B")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_IS_3B") = value
        End Set
    End Property

    Public Property BackupOnAjaxStart As String
        Get
            Return ViewState(Me.ID & "_BackupOnAjaxStart")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_BackupOnAjaxStart") = value
        End Set
    End Property

    Public Property BackupOnAjaxEnd As String
        Get
            Return ViewState(Me.ID & "BackupOnAjaxEnd")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "BackupOnAjaxEnd") = value
        End Set
    End Property
    ''' <summary>
    ''' true: Load form have check node
    ''' false:
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property is_CheckNode() As Boolean
        Get
            If ViewState(Me.ID & "_is_CheckNode") Is Nothing Then
                ViewState(Me.ID & "_is_CheckNode") = False
            End If
            Return ViewState(Me.ID & "_is_CheckNode")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_is_CheckNode") = value
        End Set
    End Property

    Public Property _isCommend() As Boolean
        Get
            If ViewState(Me.ID & "_isCommend") Is Nothing Then
                ViewState(Me.ID & "_isCommend") = False
            End If
            Return ViewState(Me.ID & "_isCommend")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_isCommend") = value
        End Set
    End Property
    Public Property EMP_CODE_OR_NAME As String
        Get
            If ViewState(Me.ID & "_EMP_CODE_OR_NAME") Is Nothing Then
                ViewState(Me.ID & "_EMP_CODE_OR_NAME") = ""
            End If
            Return ViewState(Me.ID & "_EMP_CODE_OR_NAME")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_EMP_CODE_OR_NAME") = value
        End Set
    End Property
    ''' <summary>
    ''' Hiển thị tất cả, bỏ phân quyền
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property is_All() As Boolean
        Get
            If ViewState(Me.ID & "_is_All") Is Nothing Then
                ViewState(Me.ID & "_is_All") = False
            End If
            Return ViewState(Me.ID & "_is_All")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_is_All") = value
        End Set
    End Property

    Public Property IS_DM() As Boolean
        Get
            If ViewState(Me.ID & "_IS_DM") Is Nothing Then
                ViewState(Me.ID & "_IS_DM") = False
            End If
            Return ViewState(Me.ID & "_IS_DM")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_IS_DM") = value
        End Set
    End Property

    Public Property FunctionName As String
        Get
            If ViewState(Me.ID & "_FunctionName") Is Nothing Then
                ViewState(Me.ID & "_FunctionName") = ""
            End If
            Return ViewState(Me.ID & "_FunctionName")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_FunctionName") = value
        End Set
    End Property
    Public Property EmployeeOrg As Decimal
        Get
            If ViewState(Me.ID & "_EmployeeOrg") Is Nothing Then
                ViewState(Me.ID & "_EmployeeOrg") = ""
            End If
            Return ViewState(Me.ID & "_EmployeeOrg")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_EmployeeOrg") = value
        End Set
    End Property
    Public Property EffectDate As String
        Get
            If ViewState(Me.ID & "_EffectDate") Is Nothing Then
                ViewState(Me.ID & "_EffectDate") = ""
            End If
            Return ViewState(Me.ID & "_EffectDate")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_EffectDate") = value
        End Set
    End Property
#End Region

#Region "Page"

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            popupId = CType(Me.Page, AjaxPage).PopupWindow.ClientID
            AjaxManager = CType(Me.Page, AjaxPage).AjaxManager
            AjaxLoading = CType(Me.Page, AjaxPage).AjaxLoading
            AjaxManagerId = AjaxManager.ClientID
            AjaxLoadingId = AjaxLoading.ClientID
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        If BackupOnAjaxStart Is Nothing Then
            BackupOnAjaxStart = AjaxManager.ClientEvents.OnRequestStart
        End If
        If BackupOnAjaxEnd Is Nothing Then
            BackupOnAjaxEnd = AjaxManager.ClientEvents.OnResponseEnd
        End If
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Try

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"

    Private Sub AjaxManager_AjaxRequest(ByVal sender As Object, ByVal e As Telerik.Web.UI.AjaxRequestEventArgs) Handles AjaxManager.AjaxRequest
        Try
            Dim eventArg As String = e.Argument  'Request("__EVENTARGUMENT")
            Dim a = Left(eventArg, Me.ClientID.Length + 14)
            Dim b = Me.ClientID & "_PopupPostback"
            If Left(eventArg, Me.ClientID.Length + 14) <> Me.ClientID & "_PopupPostback" Then
                Exit Sub
            End If
            If eventArg <> "" Then
                eventArg = Right(eventArg, eventArg.Length - Me.ClientID.Length - 15)
                If eventArg = "Cancel" Then
                    Close()
                    RaiseEvent CancelClicked(Nothing, e)
                Else
                    Dim Ids = eventArg.Split(";")
                    Dim empIds As New List(Of Decimal)
                    For Each str As String In Ids
                        If str <> "" Then
                            Dim id As Decimal
                            id = Decimal.Parse(str)
                            If id <> 0 Then
                                empIds.Add(id)
                            End If
                        End If
                    Next
                    If empIds.Count > 0 Then
                        EmployeeListID = empIds
                        RaiseEvent EmployeeSelected(Nothing, e)
                    End If
                End If
            End If

            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = BackupOnAjaxStart
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnResponseEnd = BackupOnAjaxEnd
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Custom"

    Public Sub Show()
        Dim state As String = ""
        Dim script As String
        If is_All = True Then
            state = "Copy"
        End If
        If Me.ClientID.Contains("ctrlFindSigner") Then
            script = "var oWnd = $find('" & popupId & "');"
            script &= "oWnd.add_close(" & Me.ClientID & "_OnClientClose);"
            script &= "oWnd.setUrl('Dialog.aspx?mid=Common&fid=ctrlFindEmployeeSignPopupDialog&noscroll=1&" &
                "MultiSelect=" & MultiSelect &
                "&CurrentValue=" & CurrentValue &
                "&MustHaveContract=" & MustHaveContract &
                "&IsHideTerminate=" & IsHideTerminate &
                "&IsShowKiemNhiem=" & IsShowKiemNhiem &
                "&LoadAllOrganization=" & If(LoadAllOrganization, "1", "0") &
                "&IsOnlyWorkingWithoutTer=" & If(IsOnlyWorkingWithoutTer, "1", "0") &
                "&IS_3B=" & IS_3B &
                "&FunctionName=" & FunctionName &
                "&EffectDate=" & EffectDate &
                "&EmployeeOrg=" & EmployeeOrg &
                "');"
            script &= "oWnd.show();"
            ScriptManager.RegisterStartupScript(Page, Page.GetType, "UserPopup", script, True)
        Else
            script = "var oWnd = $find('" & popupId & "');"
            script &= "oWnd.add_close(" & Me.ClientID & "_OnClientClose);"
            script &= "oWnd.setUrl('Dialog.aspx?mid=Common&fid=ctrlFindEmployeePopupDialog&noscroll=1&" &
                "MultiSelect=" & MultiSelect &
                "&CurrentValue=" & CurrentValue &
                "&MustHaveContract=" & MustHaveContract &
                "&IsHideTerminate=" & IsHideTerminate &
                "&IsShowKiemNhiem=" & IsShowKiemNhiem &
                "&LoadAllOrganization=" & If(LoadAllOrganization, "1", "0") &
                "&IsOnlyWorkingWithoutTer=" & If(IsOnlyWorkingWithoutTer, "1", "0") &
                "&IS_3B=" & IS_3B &
                "&FunctionName=" & FunctionName &
                "&EMP_CODE_OR_NAME=" & EMP_CODE_OR_NAME &
                "&is_All=" & is_All &
                "&state=" & state &
                "&IS_DM=" & IS_DM &
                "');"
            script &= "oWnd.show();"
            ScriptManager.RegisterStartupScript(Page, Page.GetType, "UserPopup", script, True)
        End If
    End Sub

    Public Sub Show_LoadAllOrganization()

        LoadAllOrganization = True
        Show()
    End Sub

    Public Sub Close()
        Dim script As String
        script = "$find('" & popupId & "').close();"
        ScriptManager.RegisterStartupScript(Page, Page.GetType, "UserPopup", script, True)
    End Sub

#End Region

End Class