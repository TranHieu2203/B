Imports Common
Imports Profile.ProfileBusiness

Public Class ctrlDBInfoLeave
    Inherits Common.CommonView
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
    Public Property EmployeeID As Decimal
#End Region

#Region "Page"
    Private width As Integer
    Private height As Integer
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
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        'Dim rep As New ProfileDashboardRepository
        Try
            Dim _filter As New TotalDayOffDTO
            _filter.DATE_REGISTER = Date.Now
            _filter.LEAVE_TYPE = 251
            _filter.EMPLOYEE_ID = Session("_EmployeeID")
            _filter.ID_PORTAL_REG = 0
            Dim obj As New TotalDayOffDTO
            Using rep As New ProfileDashboardRepository
                obj = rep.GetTotalDayOff(_filter)
                If obj IsNot Nothing Then
                    'PHÉP NGOÀI CÔNG TY
                    If obj.TIME_OUTSIDE_COMPANY IsNot Nothing Then
                        lblPhepTQD.Text = If(obj.TIME_OUTSIDE_COMPANY = 0, 0, Decimal.Parse(obj.TIME_OUTSIDE_COMPANY).ToString())
                    Else
                        lblPhepTQD.Text = Decimal.Parse(0).ToString()
                    End If
                    'phep chế độ
                    If obj.TOTAL_HAVE1 IsNot Nothing Then
                        lblNgayPhepCD.Text = If(obj.TOTAL_HAVE1 = 0, 0, Decimal.Parse(obj.TOTAL_HAVE1).ToString())
                    Else
                        lblNgayPhepCD.Text = Decimal.Parse(0).ToString()
                    End If
                    'phep dã nghĩ
                    If obj.USED_DAY IsNot Nothing Then
                        lblNgayPhepSD.Text = If(obj.USED_DAY = 0, 0, Decimal.Parse(obj.USED_DAY).ToString())
                    Else
                        lblNgayPhepSD.Text = Decimal.Parse(0).ToString()
                    End If
                    'phep tham nien
                    If obj.SENIORITYHAVE IsNot Nothing Then
                        lblPhepTN.Text = If(obj.SENIORITYHAVE = 0, 0, Decimal.Parse(obj.SENIORITYHAVE).ToString())
                    Else
                        lblPhepTN.Text = Decimal.Parse(0).ToString()
                    End If
                    'phep nam truoc con lai
                    If obj.PREVTOTAL_HAVE IsNot Nothing Then
                        lblPhepNT.Text = If(obj.PREVTOTAL_HAVE = 0, 0, Decimal.Parse(obj.PREVTOTAL_HAVE).ToString())
                    Else
                        lblPhepNT.Text = Decimal.Parse(0).ToString()
                    End If
                    'phép còn lại
                    If obj.REST_DAY IsNot Nothing Then
                        lblPhepConLai.Text = If(obj.REST_DAY = 0, 0, Decimal.Parse(obj.REST_DAY).ToString())
                    Else
                        lblPhepConLai.Text = Decimal.Parse(0).ToString()
                    End If
                Else
                    lblPhepTQD.Text = Decimal.Parse(0).ToString()
                    lblNgayPhepCD.Text = Decimal.Parse(0).ToString()
                    lblNgayPhepSD.Text = Decimal.Parse(0).ToString()
                    lblPhepTN.Text = Decimal.Parse(0).ToString()
                    lblPhepConLai.Text = Decimal.Parse(0).ToString()
                    lblPhepNT.Text = Decimal.Parse(0).ToString()
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