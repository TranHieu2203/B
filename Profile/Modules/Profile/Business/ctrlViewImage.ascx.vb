Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports Common
Imports Common.Common
Imports Common.CommonMessage
Imports System.IO
Imports System.Configuration
Public Class ctrlViewImage
    Inherits Common.CommonView

#Region "Properties"
    ''' <summary>
    ''' EmployeeInfo truyền từ view Cha vào.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property EmployeeInfo As EmployeeDTO
        Get
            Return PageViewState(Me.ID & "_EmployeeInfo")
        End Get
        Set(ByVal value As EmployeeDTO)
            PageViewState(Me.ID & "_EmployeeInfo") = value
        End Set
    End Property

    Property ImageFile As Telerik.Web.UI.UploadedFile
        Get
            Return PageViewState(Me.ID & "_ImageFile")
        End Get
        Set(ByVal value As Telerik.Web.UI.UploadedFile)
            PageViewState(Me.ID & "_ImageFile") = value
        End Set
    End Property
    Property LoadDefaultImage As Boolean
        Get
            Return PageViewState(Me.ID & "_LoadDefaultImage")
        End Get
        Set(ByVal value As Boolean)
            PageViewState(Me.ID & "_LoadDefaultImage") = value
        End Set
    End Property
    Property isLoad As Boolean
        Get
            Return PageViewState(Me.ID & "_isLoad")
        End Get
        Set(ByVal value As Boolean)
            PageViewState(Me.ID & "_isLoad") = value
        End Set
    End Property
    Public Property popupId As String
    Public WithEvents AjaxManager As RadAjaxManager
    Public AjaxLoading As RadAjaxLoadingPanel
    Public Property AjaxManagerId As String
    Public Property AjaxLoadingId As String
#End Region

#Region "Page"
    Public Overrides Property MustAuthorize As Boolean = False
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        UpdateControlState()
        Refresh()
    End Sub
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
#End Region

#Region "Event"

#End Region

#Region "Custom"
    Public Overrides Sub UpdateControlState()
        If LoadDefaultImage Then
            DisplayImage()
        End If
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Try
            If Not isLoad Then
                DisplayImage()
                isLoad = True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub BindData()
        Try

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub


    Private Sub DisplayImage()
        Try
            If Request.Params("emp") IsNot Nothing Then
                Dim strFile = Request.Params("emp")
                Dim _fileInfo As IO.FileInfo
                Dim filepath As String = Server.MapPath("~/ReportTemplates/Profile/OrganizationFileAttach/" & strFile)
                If File.Exists(filepath) Then
                    _fileInfo = New FileInfo(filepath)
                    Dim _FStream As New IO.FileStream(_fileInfo.FullName, IO.FileMode.Open, IO.FileAccess.Read)
                    Dim _NumBytes As Long = _fileInfo.Length

                    Dim _BinaryReader As New IO.BinaryReader(_FStream)
                    rbiEmployeeImage.DataValue = _BinaryReader.ReadBytes(Convert.ToInt32(_NumBytes))
                End If
            End If
            If Request.Params("imgUrl") IsNot Nothing Then
                Dim strFile = Request.Params("imgUrl")
                Dim _fileInfo As IO.FileInfo
                strFile = strFile.Replace("(slash)", "/")
                Dim filepath As String = strFile
                If File.Exists(filepath) Then
                    _fileInfo = New FileInfo(filepath)
                    Dim _FStream As New IO.FileStream(_fileInfo.FullName, IO.FileMode.Open, IO.FileAccess.Read)
                    Dim _NumBytes As Long = _fileInfo.Length

                    Dim _BinaryReader As New IO.BinaryReader(_FStream)
                    rbiEmployeeImage.DataValue = _BinaryReader.ReadBytes(Convert.ToInt32(_NumBytes))
                End If
            End If
            Exit Sub
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Shared Function GetFileType(ByVal Extention As String) As String
        Return My.Computer.Registry.GetValue("HKEY_CLASSES_ROOT\" & My.Computer.Registry.GetValue("HKEY_CLASSES_ROOT\" & Extention, "", Extention).ToString, "", Extention).ToString
    End Function
#End Region
End Class