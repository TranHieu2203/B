Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI

Public Class ctrlDBEmployeePaper
    Inherits CommonView
    Protected WithEvents ViewItem As ViewBase
    Public Overrides Property MustAuthorize As Boolean = False
#Region "Property"
    Public Property StatisticType As String
        Get
            Return Session(Me.ID & "_StatisticType")
        End Get
        Set(ByVal value As String)
            Session(Me.ID & "_StatisticType") = value
        End Set
    End Property
    Public Property StatisticList As List(Of OtherListDTO)
        Get
            Return Session(Me.ID & "_StatisticList")
        End Get
        Set(ByVal value As List(Of OtherListDTO))
            Session(Me.ID & "_StatisticList") = value
        End Set
    End Property

#End Region

#Region "Page"
    Private width As Integer
    Private height As Integer
    Public Property EmployeeID As Decimal
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            If Request("width") IsNot Nothing Then
                width = CInt(Request("width"))
            End If
            If Request("height") IsNot Nothing Then
                height = CInt(Request("height"))
            End If
            Dim resize As Integer = 0
            If Request("resize") IsNot Nothing Then
                resize = CInt(Request("resize"))
            End If
            If Not IsPostBack Then
                Refresh(CommonMessage.ACTION_UPDATED)
            Else
                Refresh()
            End If
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub BindData()
        Try
            Dim dtData
            Using rep As New ProfileRepository
                dtData = rep.HU_PAPER_LIST(Session("_EmployeeID"))
            End Using
            FillCheckBoxList(lstbPaperFiled, dtData, "NAME", "ID")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim EmployeeList As EmployeeDTO
        Dim _filter As New EmployeeDTO
        Try
            Using rep As New ProfileBusinessRepository
                EmployeeList = rep.GetEmployeeByEmployeeID(Session("_EmployeeID"))
                lstbPaperFiled.ClearChecked()
                If EmployeeList.lstPaperFiled IsNot Nothing Then
                    For Each item As RadListBoxItem In lstbPaperFiled.Items
                        If EmployeeList.lstPaperFiled.Contains(item.Value) Then
                            item.Checked = True
                        End If
                    Next
                End If
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

#Region "Event"

#End Region

#Region "Custom"
#End Region
End Class