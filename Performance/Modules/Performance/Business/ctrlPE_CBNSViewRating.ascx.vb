Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Performance.PerformanceBusiness
Imports Telerik.Web.UI

Public Class ctrlPE_CBNSViewRating
    Inherits Common.CommonView
    Protected WithEvents RequestView As ViewBase

#Region "Property"

#End Region

#Region "Page"
    Public title As String = "Biểu Đồ Xếp Hạng Nhân Viên"
    Public titleX As String = "category"
    Public titleY As String = "Số lượng nhân viên"
    Public name As String = "Xếp loại"
    Public data As String ''{name: 'Microsoft Internet Explorer', y: 56.33},\n
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            Refresh()
            UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            InitControl()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()
        Dim rep As New PerformanceRepository
        Try
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New PerformanceRepository
        Try
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

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Try

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DELETE
                UpdateControlState()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub ctrlOrg_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged
        Try
            Dim rep As New PerformanceRepository
            Dim _filter As New AssessRatingDTO
            Dim lst As New List(Of AssessRatingDTO)
            Try
                _filter.PERIOD_YEAR = Utilities.ObjToInt(txtYear.Value)
                _filter.PERIOD_ID = Utilities.ObjToInt(cboPeriod.SelectedValue)
                _filter.PERIOD_TYPEASS_ID = Utilities.ObjToInt(cboTypeAss.SelectedValue)
                _filter.ORG_ID = Utilities.ObjToInt(ctrlOrg.CurrentValue)
                lst = rep.GetAssessRatingEmployeeOrg(_filter)
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
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            Dim rep As New PerformanceRepository
            Dim _filter As New AssessRatingDTO
            Dim lst As New List(Of AssessRatingDTO)
            Try
                _filter.PERIOD_YEAR = Utilities.ObjToInt(txtYear.Value)
                _filter.PERIOD_ID = Utilities.ObjToInt(cboPeriod.SelectedValue)
                _filter.PERIOD_TYPEASS_ID = Utilities.ObjToInt(cboTypeAss.SelectedValue)
                _filter.ORG_ID = Utilities.ObjToInt(ctrlOrg.CurrentValue)
                lst = rep.GetAssessRatingEmployeeOrg(_filter)
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

#Region "Custom"
    Public Sub LoadPeriod()
        Dim rep As New PerformanceRepository
        Dim Periods As List(Of PeriodDTO)
        Dim _filter As New PeriodDTO
        _filter.TYPE_ASS = Utilities.ObjToInt(cboTypeAss.SelectedValue)
        _filter.YEAR = Utilities.ObjToInt(txtYear.Value)
        Periods = rep.GetPeriod(_filter)
        FillRadCombobox(cboPeriod, Periods.ToTable(), "NAME", "ID")
    End Sub
#End Region

End Class