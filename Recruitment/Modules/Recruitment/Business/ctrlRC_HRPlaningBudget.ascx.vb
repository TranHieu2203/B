Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Recruitment.RecruitmentBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlRC_HRPlaningBudget
    Inherits Common.CommonView
    Protected WithEvents RequestView As ViewBase
    Public Overrides Property MustAuthorize As Boolean = True
    Dim _myLog As New MyLog()
    Dim pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Recruitment/Modules/Recruitment/Business/" + Me.GetType().Name.ToString()
#Region "Property"

#End Region

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
        Try
            rgData.SetFilter()
            rgData.AllowCustomPaging = True
            rgData.PageSize = Common.Common.DefaultPageSize
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            InitControl()
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

    Protected Sub InitControl()
        Try
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()
        Dim rep As New RecruitmentRepository
        Try
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New RecruitmentRepository
        Try
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"
    Private Sub rgData_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles rgData.ItemCommand
        Dim rep As New RecruitmentRepository
        Try
            Select Case e.CommandName
                Case "DETAIL"
                    Dim item = CType(e.Item, GridDataItem)
                    If rep.CheckExistData(item.GetDataKeyValue("ID")) Then
                        ShowMessage(Translate("Dữ liệu chưa được khởi tạo định biên chi tiết"), NotifyType.Warning)
                        Exit Sub
                    End If
                    Dim str As String = "window.open('Default.aspx?mid=Recruitment&fid=ctrlRC_HRBudgetDetail&group=Business&ID=" & item.GetDataKeyValue("ID") & "');"
                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
        Try
            CreateDataFilter()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        rgData.CurrentPageIndex = 0
        rgData.MasterTableView.SortExpressions.Clear()
        rgData.Rebind()
    End Sub

#End Region

#Region "Custom"

    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim _filter As New HRYearPlaningDTO
        Dim rep As New RecruitmentRepository
        Dim bCheck As Boolean = False
        Try
            SetValueObjectByRadGrid(rgData, _filter)
            If IsNumeric(rnYear.Value) Then
                _filter.YEAR = rnYear.Value
            End If
            _filter.VERSION = txtVersion.Text.Trim
            _filter.NOTE = txtNote.Text.Trim
            If IsDate(rdEffectDate.SelectedDate) Then
                _filter.EFFECT_DATE = rdEffectDate.SelectedDate
            End If
            If IsDate(rdExpireDate.SelectedDate) Then
                _filter.EXPIRE_DATE = rdExpireDate.SelectedDate
            End If
            Dim MaximumRows As Integer
            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()
            Dim lstData As List(Of HRYearPlaningDTO)

            If Not isFull Then
                If Sorts IsNot Nothing Then
                    lstData = rep.GetHRYearPlaning(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, Sorts)
                Else
                    lstData = rep.GetHRYearPlaning(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows)
                End If
            Else
                If Sorts IsNot Nothing Then
                    Return rep.GetHRYearPlaning(_filter, Sorts).ToTable
                Else
                    Return rep.GetHRYearPlaning(_filter).ToTable
                End If
            End If

            rgData.VirtualItemCount = MaximumRows
            rgData.DataSource = lstData
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

End Class