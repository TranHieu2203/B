Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Training.TrainingBusiness

Public Class ctrlTR_DBCourse
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
    Public Property StatisticData As List(Of StatisticDTO)
        Get
            Return Session(Me.ID & "_StatisticData")
        End Get
        Set(ByVal value As List(Of StatisticDTO))
            Session(Me.ID & "_StatisticData") = value
        End Set
    End Property

#End Region

#Region "Page"
    Private width As Integer
    Private height As Integer
    Public data As String ''{name: 'Tokyo',data: [7.0, 6.9, 9.5, 14.5, 18.2, 21.5, 25.2, 26.5, 23.3, 18.3, 13.9, 9.6]},\n
    Public categories As String
    Public enabled As String
    Public type As String

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
            LoadYear()
            cboYear.SelectedValue = Date.Now.Year

            If Common.Common.OrganizationLocationDataSession Is Nothing Then
                Dim rep1 As New CommonRepository
                rep1.GetOrganizationLocationTreeView()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New TrainingRepository
        Try
            If width > 1000 Then
                btnMax.Text = "Restore"
            Else
                btnMax.Text = "Maximize"
            End If

            data = String.Empty
            If StatisticData Is Nothing Or Message = CommonMessage.ACTION_UPDATED Then
                StatisticData = rep.GetStatisticCourse(Utilities.ObjToInt(cboYear.SelectedValue))
            End If

            If StatisticData.Count > 0 Then
                data = String.Empty
                type = "bar"
                enabled = "false"
                categories = "xAxis: { type: 'category', labels: { rotation: 0 }},"
                data = "{name: 'Tổng số công', colorByPoint: true, data: ["
                For index As Integer = 0 To StatisticData.Count - 1
                    If index = StatisticData.Count - 1 Then
                        data &= "['" & StatisticData(index).NAME & "', " & StatisticData(index).VALUE & "]"
                    Else
                        data &= "['" & StatisticData(index).NAME & "', " & StatisticData(index).VALUE & "],"
                    End If
                Next
                data &= "], tooltip: { valueSuffix: ''}}"
            Else
                data = String.Empty
                type = "bar"
                enabled = "false"
                categories = "xAxis: { type: 'category', labels: { rotation: 0 }},"
                data = "{name: '',data: [], tooltip: { valueSuffix: ''}}"
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

#Region "Event"

    Protected Sub cboYear_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cboYear.TextChanged
        Refresh(CommonMessage.ACTION_UPDATED)
    End Sub


#End Region

#Region "Custom"
    Private Sub LoadYear()
        Try
            Dim table As New DataTable
            table.Columns.Add("YEAR", GetType(Integer))
            table.Columns.Add("ID", GetType(Integer))
            Dim row As DataRow
            For index = 2015 To Date.Now.Year + 1
                row = table.NewRow
                row("ID") = index
                row("YEAR") = index
                table.Rows.Add(row)
            Next
            FillRadCombobox(cboYear, table, "YEAR", "ID")
        Catch ex As Exception

        End Try
    End Sub


#End Region
End Class