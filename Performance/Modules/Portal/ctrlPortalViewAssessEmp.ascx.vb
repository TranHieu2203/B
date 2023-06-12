Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Performance.PerformanceBusiness
Imports Telerik.Web.UI

Public Class ctrlPortalViewAssessEmp
    Inherits CommonView
    Protected WithEvents ViewItem As ViewBase

#Region "Property"
    Public Property GridList As List(Of AssessmentDirectDTO)
        Get
            Return PageViewState(Me.ID & "_GridList")
        End Get
        Set(ByVal value As List(Of AssessmentDirectDTO))
            PageViewState(Me.ID & "_GridList") = value
        End Set
    End Property
    Public Property EmployeeID As Decimal
#End Region

#Region "Page"
    Public Overrides Property MustAuthorize As Boolean = False
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            Refresh()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        InitControl()
    End Sub

    Protected Sub InitControl()
        Try
            Me.MainToolBar = tbarMainToolBar
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Print)
            CType(MainToolBar.Items(0), RadToolBarButton).Text = "In đánh giá"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New PerformanceRepository
        Try
            rgData.SetFilter()
            SetValueObjectByRadGrid(rgData, New AssessmentDirectDTO)

            If Not IsPostBack Then
                GridList = rep.GetKPIAssessEmp(EmployeeID)
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                If Message = CommonMessage.ACTION_SAVED Then
                    GridList = rep.GetKPIAssessEmp(EmployeeID)
                End If
            End If

            'Đưa dữ liệu vào Grid
            If Me.GridList IsNot Nothing Then
                rgData.DataSource = Me.GridList
                rgData.DataBind()
            Else
                rgData.DataSource = New List(Of AssessmentDirectDTO)
                rgData.DataBind()
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Event"

    Private Sub rgData_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
        Try
            If IsPostBack Then Exit Sub
            SetValueObjectByRadGrid(rgData, New AssessmentDirectDTO)

            Dim rep As New PerformanceRepository
            GridList = rep.GetKPIAssessEmp(EmployeeID)
            rgData.DataSource = GridList

        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objAssessment As New AssessmentDTO
        Dim rep As New PerformanceRepository
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_PRINT
                    Dim dsData As DataSet
                    Dim sPath As String = ""

                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate("Bạn phải chọn nhân viên trước khi in ."), Utilities.NotifyType.Warning)
                        Exit Sub
                    End If

                    If rgData.SelectedItems.Count > 1 Then
                        ShowMessage(Translate("Chỉ được chọn 1 bản ghi để in ."), Utilities.NotifyType.Warning)
                        Exit Sub
                    End If

                    'Kiểm tra các điều kiện trước khi in
                    Dim lstEmpID As String = ""
                    Dim srtImage As String = ""

                    For Each dr As Telerik.Web.UI.GridDataItem In rgData.SelectedItems
                        dsData = rep.PRINT_PE_ASSESS(dr.GetDataKeyValue("EMPLOYEE_ID"), dr.GetDataKeyValue("PE_PERIO_ID"), dr.GetDataKeyValue("OBJECT_GROUP_ID"))
                    Next

                    sPath = Server.MapPath("~/ReportTemplates/Performance/Report/BC_DANHGIA.xls")

                    Using xls As New ExcelCommon
                        xls.ExportExcelTemplate(sPath,
                            "InDanhGia", Nothing, dsData, Nothing, Response, "", ExcelCommon.ExportType.Excel)
                    End Using
            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Custom"

#End Region
End Class