Imports Common
Imports Telerik.Web.UI
Public Class ctrlFindEmployeeImportPopup
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

    Public ReadOnly Property SelectedEmployee As List(Of Common.CommonBusiness.EmployeePopupFindDTO)
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
    Public Property CommendDate As Date
        Get
            Return ViewState(Me.ID & "_CommendDate")
        End Get
        Set(value As Date)
            ViewState(Me.ID & "_CommendDate") = value
        End Set
    End Property
    Public Property CommendList_Code As String
        Get
            Return ViewState(Me.ID & "_CommendList_Code")
        End Get
        Set(value As String)
            ViewState(Me.ID & "_CommendList_Code") = value
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
        Dim script As String
        script = "var oWnd = $find('" & popupId & "');"
        script &= "oWnd.add_close(" & Me.ClientID & "_OnClientClose);"
        script &= "oWnd.setUrl('Dialog.aspx?mid=Profile/Shared&fid=ctrlFindEmployeeImportPopupDialog&noscroll=1&" & _
            "MultiSelect=" & MultiSelect & _
            "&CurrentValue=" & CurrentValue & _
            "&is_CheckNode=" & is_CheckNode & _
            "');"
        script &= "oWnd.show();"
        ScriptManager.RegisterStartupScript(Page, Page.GetType, "UserPopup", script, True)

    End Sub

    Public Sub Close()
        Dim script As String
        script = "$find('" & popupId & "').close();"
        ScriptManager.RegisterStartupScript(Page, Page.GetType, "UserPopup", script, True)
    End Sub

#End Region

End Class