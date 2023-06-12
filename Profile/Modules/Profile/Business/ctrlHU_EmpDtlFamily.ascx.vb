Imports Common
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Public Class ctrlHU_EmpDtlFamily
    Inherits CommonView
    Dim employeeCode As String
    'Public Overrides Property MustAuthorize As Boolean = False

#Region "Properties"
    Public Property GridList As List(Of FamilyDTO)
        Get
            Return PageViewState(Me.ID & "_GridList")
        End Get
        Set(ByVal value As List(Of FamilyDTO))
            PageViewState(Me.ID & "_GridList") = value
        End Set
    End Property
    Public Property EmployeeID As Decimal
    Property EmployeeInfo As EmployeeDTO
        Get
            Return PageViewState(Me.ID & "_EmployeeInfo")
        End Get
        Set(ByVal value As EmployeeDTO)
            PageViewState(Me.ID & "_EmployeeInfo") = value
        End Set
    End Property

    Property isLoad As Boolean
        Get
            Return ViewState(Me.ID & "_isLoad")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_isLoad") = value
        End Set
    End Property

    Property IDSelect As Decimal
        Get
            Return ViewState(Me.ID & "_IDSelect")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_IDSelect") = value
        End Set
    End Property

    Property lstFamily As List(Of FamilyDTO)
        Get
            Return ViewState(Me.ID & "_lstFamily")
        End Get
        Set(ByVal value As List(Of FamilyDTO))
            ViewState(Me.ID & "_lstFamily") = value
        End Set
    End Property

    Property SelectedItem As List(Of Decimal)
        Get
            Return ViewState(Me.ID & "_SelectedItem")
        End Get
        Set(ByVal value As List(Of Decimal))
            ViewState(Me.ID & "_SelectedItem") = value
        End Set
    End Property

    Property ComboBoxDataDTO As ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ComboBoxDataDTO")
        End Get
        Set(ByVal value As ComboBoxDataDTO)
            ViewState(Me.ID & "_ComboBoxDataDTO") = value
        End Set
    End Property

    'Lưu lại đang ở View nào để load khi dùng nút Next và Previous để chuyển sang xem thông tin nhân viên khác.
    Public Property CurrentPlaceHolder As String
        Get
            Return PageViewState(Me.ID & "_CurrentPlaceHolder")
        End Get
        Set(ByVal value As String)
            PageViewState(Me.ID & "_CurrentPlaceHolder") = value
        End Set
    End Property
#End Region

#Region "Page"

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            ctrlEmpBasicInfo.SetProperty("EmployeeInfo", EmployeeInfo)
            'UpdateControlState()
            Refresh()
            Me.CurrentPlaceHolder = Me.ViewName
            ctrlEmpBasicInfo.SetProperty("CurrentPlaceHolder", Me.CurrentPlaceHolder)
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub


    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New ProfileBusinessRepository
        Try
            If EmployeeInfo IsNot Nothing Then
                EmployeeID = EmployeeInfo.ID
                Dim objFamily As New FamilyDTO
                objFamily.EMPLOYEE_ID = EmployeeInfo.ID
                GridList = rep.GetEmployeeFamily(objFamily)
            End If
            'Đưa dữ liệu vào Grid
            If Me.GridList IsNot Nothing Then
                rgFamily.DataSource = Me.GridList
                rgFamily.DataBind()
            Else
                rgFamily.DataSource = New List(Of ContractDTO)
                rgFamily.DataBind()
            End If
            rep.Dispose()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub


#End Region

#Region "Event"

    Private Sub rgFamily_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgFamily.NeedDataSource
        Try
            If IsPostBack Then Exit Sub

            Dim rep As New ProfileBusinessRepository
            Dim objFamily As New FamilyDTO
            objFamily.EMPLOYEE_ID = EmployeeInfo.ID

            SetValueObjectByRadGrid(rgFamily, objFamily)

            GridList = rep.GetEmployeeFamily(objFamily)
            rgFamily.DataSource = GridList
            rep.Dispose()
        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
        'If EmployeeInfo IsNot Nothing Then
        '    Dim rep As New ProfileBusinessRepository
        '    Dim objFamily As New FamilyDTO
        '    objFamily.EMPLOYEE_ID = EmployeeInfo.ID
        '    SetValueObjectByRadGrid(rgFamily, objFamily)
        '    rep.Dispose()
        '    lstFamily = rep.GetEmployeeFamily(objFamily)
        '    rgFamily.DataSource = lstFamily
        'Else
        '    rgFamily.DataSource = New List(Of FamilyDTO)
        'End If
    End Sub


#End Region

#Region "Custom"


#End Region


End Class