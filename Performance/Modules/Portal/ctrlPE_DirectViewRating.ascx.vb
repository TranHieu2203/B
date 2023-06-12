Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Performance.PerformanceBusiness
Imports Telerik.Web.UI


Public Class ctrlPE_DirectViewRating
    Inherits Common.CommonView
    Protected WithEvents ViewItem As ViewBase
    Public Overrides Property MustAuthorize As Boolean = False
#Region "Property"
#End Region
    Public title As String = "Biểu Đồ Xếp Hạng Nhân Viên"
    Public titleX As String = "category"
    Public titleY As String = "Số lượng nhân viên"
    Public name As String = "Xếp loại"
    Public data As String ''{name: 'Microsoft Internet Explorer', y: 56.33},\n
    Public Property EmployeeID As Decimal
#Region "Page"
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            Refresh()
            UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        InitControl()
    End Sub

    Protected Sub InitControl()
        Try
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Public Overrides Sub BindData()
        Try
            Using rep As New PerformanceRepository
                'Kieu danh gia
                Dim dtData = rep.GetOtherList("TYPE_ASS", True)
                FillRadCombobox(cboTypeAss, dtData, "NAME", "ID")
            End Using
            txtYear.Value = Date.Now.Year
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

#Region "Event"
    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim rep As New PerformanceRepository
        Dim _filter As New AssessRatingDTO
        Dim lst As New List(Of AssessRatingDTO)
        Try
            _filter.PERIOD_YEAR = Utilities.ObjToInt(txtYear.Value)
            _filter.PERIOD_ID = Utilities.ObjToInt(cboPeriod.SelectedValue)
            _filter.PERIOD_TYPEASS_ID = Utilities.ObjToInt(cboTypeAss.SelectedValue)
            _filter.DIRECT_ID = EmployeeID
            lst = rep.GetAssessRatingEmployee(_filter)
            If lst.Count > 0 Then
                For index As Integer = 0 To lst.Count - 1
                    If index = lst.Count - 1 Then
                        data &= "{name: '" & lst(index).CLASSIFICATION_CODE & "', y: " & lst(index).COUNT_EMP & "}"
                    Else
                        data &= "{name: '" & lst(index).CLASSIFICATION_CODE & "', y: " & lst(index).COUNT_EMP & "}," & vbNewLine
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objAssessment As New AssessmentDTO
        Dim rep As New PerformanceRepository
        Try
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Protected Sub txtYear_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtYear.TextChanged
        LoadPeriod()
    End Sub
    Protected Sub cboTypeAss_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboTypeAss.SelectedIndexChanged
        LoadPeriod()
    End Sub
#End Region
    Public Sub LoadPeriod()
        Dim rep As New PerformanceRepository
        Dim Periods As List(Of PeriodDTO)
        Dim _filter As New PeriodDTO
        _filter.TYPE_ASS = Utilities.ObjToInt(cboTypeAss.SelectedValue)
        _filter.YEAR = Utilities.ObjToInt(txtYear.Value)
        Periods = rep.GetPeriod(_filter)
        FillRadCombobox(cboPeriod, Periods.ToTable(), "NAME", "ID")
    End Sub

#Region "Custom"
    Private Sub UpdateToolbarState()
        Try
            ChangeToolbarState()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

End Class