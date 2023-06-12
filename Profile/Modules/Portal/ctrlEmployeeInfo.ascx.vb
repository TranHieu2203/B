Imports Common
Imports Framework.UI

Public Class ctrlEmployeeInfo
    Inherits CommonView
    Protected WithEvents ViewItem As ViewBase

#Region "Property"
    Public Property ImageBinary As Byte()
        Get
            Return PageViewState(Me.ID & "_ImageBinary")
        End Get
        Set(ByVal value As Byte())
            PageViewState(Me.ID & "_ImageBinary") = value
        End Set
    End Property
    Public Property EmployeeID As Decimal
#End Region

#Region "Page"
    Public Overrides Property MustAuthorize As Boolean = False
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            If Not IsPostBack Then
                Refresh()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Try
            If LogHelper.CurrentUser Is Nothing Then
                Exit Sub
            End If
            lblEMPLOYEE_CODE.Text = LogHelper.CurrentUser.EMPLOYEE_CODE
            lblFULLNAME.Text = LogHelper.CurrentUser.FULLNAME
            Dim rep As New ProfileBusinessRepository
            Dim sError As String = ""
            If EmployeeID <> 0 Then
                rbiEmployeeImage.DataValue = rep.GetEmployeeImage(EmployeeID, sError) 'Lấy ảnh của nhân viên.
            Else
                rbiEmployeeImage.DataValue = rep.GetEmployeeImage(0, sError) 'Lấy ảnh mặc định (NoImage.jpg)
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Event"

#End Region

#Region "Custom"
#End Region
End Class