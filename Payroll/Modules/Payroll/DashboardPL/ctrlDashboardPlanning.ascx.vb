Imports Common
Imports Framework.UI
Imports Payroll.PayrollBusiness

Public Class ctrlDashboardPlanning
    Inherits Common.CommonView
    Protected WithEvents SalaryPlanningView As ViewBase
    Protected WithEvents ctrlOrgPopup As ctrlFindOrgPopup

#Region "Property"

    Property IsLoad As Boolean
        Get
            Return ViewState(Me.ID & "_IsLoad")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_IsLoad") = value
        End Set
    End Property

    Property isLoadPopup As Integer
        Get
            Return ViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isLoadPopup") = value
        End Set
    End Property
#End Region

#Region "Page"
    Public data As String ''{name: 'Tokyo',data: [7.0, 6.9, 9.5, 14.5, 18.2, 21.5, 25.2, 26.5, 23.3, 18.3, 13.9, 9.6]},\n
    Public data1 As String ''{name: 'Tokyo',data: [7.0, 6.9, 9.5, 14.5, 18.2, 21.5, 25.2, 26.5, 23.3, 18.3, 13.9, 9.6]},\n
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

    Public Overrides Sub BindData()
        Try
            rntxtYear.Value = Date.Now.Year
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMain
            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem("EXPORT_TEMP",
                                                                    ToolbarIcons.Export,
                                                                    ToolbarAuthorize.Export,
                                                                    Translate("Xuất file mẫu")))
            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem("IMPORT_TEMP",
                                                                     ToolbarIcons.Import,
                                                                     ToolbarAuthorize.Import,
                                                                     Translate("Nhập file mẫu")))
            tbarMain.Visible = False
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()
        'Dim rep As New PayrollRepository
        Try
            Select Case CurrentState
                Case CommonMessage.STATE_DELETE

            End Select
            phPopup.Controls.Clear()
            Select Case isLoadPopup
                Case 1
                    ctrlOrgPopup = Me.Register("ctrlOrgPopup", "Common", "ctrlFindOrgPopup")
                    phPopup.Controls.Add(ctrlOrgPopup)
            End Select
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        'Dim rep As New PayrollRepository
        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"

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

            hidOrg.Value = ctrlOrg.CurrentValue
            hidOrgName.Value = ctrlOrg.CurrentText
            CreateDataFilter()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub


    Property dtData As DataTable
        Get
            If ViewState(Me.ID & "_dtData") Is Nothing Then
                Dim dt As New DataTable
                dt.Columns.Add("ID", GetType(String))
                dt.Columns.Add("ORG_NAME", GetType(String))
                dt.Columns.Add("ORG_ID", GetType(String))
                dt.Columns.Add("TITLE_NAME", GetType(String))
                dt.Columns.Add("TITLE_ID", GetType(String))
                dt.Columns.Add("YEAR", GetType(String))
                dt.Columns.Add("MONTH", GetType(String))
                dt.Columns.Add("EMP_NUMBER", GetType(String))
                ViewState(Me.ID & "_dtData") = dt
            End If
            Return ViewState(Me.ID & "_dtData")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtData") = value
        End Set
    End Property

    Function SaveOT() As Boolean
        Try
            Dim rep As New PayrollRepository
            Dim obj As New PASalaryPlanningDTO
            Dim gId As New Decimal?
            gId = 0
            dtData.TableName = "Data"
            rep.ImportSalaryPlanning(dtData, gId)
            rep.Dispose()
            Return True
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            Return False
        End Try
    End Function

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Try
            CreateDataFilter()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Custom"

    Protected Sub CreateDataFilter()
        Dim dsData As DataSet
        Dim _filter As New PASalaryPlanningDTO
        Using rep As New PayrollRepository
            Try
                If ctrlOrg.CurrentValue IsNot Nothing Then
                    _filter.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue)
                End If
                Dim _param = New ParamDTO With {.ORG_ID = Decimal.Parse(0), _
                                                   .IS_DISSOLVE = ctrlOrg.IsDissolve}

                _filter.YEAR = rntxtYear.Value
                dsData = rep.GetSalaryEmpTracker(_filter, _param)
                RefreshBar(dsData)
            Catch ex As Exception
                Throw ex
            End Try

        End Using

    End Sub

    Public Sub RefreshBar(ByVal dsData As DataSet)
        Try
            'Đặt title cho chart
            'charData1.PlotArea.YAxis.Step = 1
            'charData1.PlotArea.XAxis.Step = 1

            'charData1.Series(0).Appearance.LegendDisplayMode = ChartSeriesLegendDisplayMode.SeriesName
            'charData1.Series(0).DataYColumn = "TOTAL_DINHBIEN"
            'charData1.Series(0).DefaultLabelValue = "#Y{n0}"
            'charData1.Series(1).Appearance.LegendDisplayMode = ChartSeriesLegendDisplayMode.SeriesName
            'charData1.Series(1).DataYColumn = "TOTAL_THUCTE"
            'charData1.Series(1).DefaultLabelValue = "#Y{n0}"
            'charData1.PlotArea.XAxis.DataLabelsColumn = "MONTH"
            'charData1.DataSource = dsData.Tables(0)
            'charData1.DataBind()
            data = "{name: '" & Translate("Tổng quỹ lương theo định biên") & "',data: ["
            For index As Integer = 0 To dsData.Tables(0).Rows.Count - 1
                If index = dsData.Tables(0).Rows.Count - 1 Then
                    data &= dsData.Tables(0).Rows(index)("TOTAL_DINHBIEN")
                Else
                    data &= dsData.Tables(0).Rows(index)("TOTAL_DINHBIEN") & ","
                End If
            Next
            data &= "]},"
            data &= " {name: '" & Translate("Tổng chi phí lương thực tế") & "',data: ["
            For index As Integer = 0 To dsData.Tables(0).Rows.Count - 1
                If index = dsData.Tables(0).Rows.Count - 1 Then
                    data &= dsData.Tables(0).Rows(index)("TOTAL_THUCTE")
                Else
                    data &= dsData.Tables(0).Rows(index)("TOTAL_THUCTE") & ","
                End If
            Next
            data &= "]}"

            'Đặt title cho chart
            'charData2.PlotArea.YAxis.Step = 1
            'charData2.PlotArea.XAxis.Step = 1

            'charData2.Series(0).Appearance.LegendDisplayMode = ChartSeriesLegendDisplayMode.SeriesName
            'charData2.Series(0).DataYColumn = "TOTAL_DINHBIEN"
            'charData2.Series(1).Appearance.LegendDisplayMode = ChartSeriesLegendDisplayMode.SeriesName
            'charData2.Series(1).DataYColumn = "TOTAL_THUCTE"
            'charData2.PlotArea.XAxis.DataLabelsColumn = "MONTH"
            'charData2.DataSource = dsData.Tables(1)
            'charData2.DataBind()
            data1 = "{name: '" & Translate("Tổng nhân sự định biên") & "',data: ["
            For index As Integer = 0 To dsData.Tables(1).Rows.Count - 1
                If index = dsData.Tables(1).Rows.Count - 1 Then
                    data1 &= dsData.Tables(1).Rows(index)("TOTAL_DINHBIEN")
                Else
                    data1 &= dsData.Tables(1).Rows(index)("TOTAL_DINHBIEN") & ","
                End If
            Next
            data1 &= "]},"
            data1 &= " {name: '" & Translate("Tổng nhân sự thực tế") & "',data: ["
            For index As Integer = 0 To dsData.Tables(1).Rows.Count - 1
                If index = dsData.Tables(1).Rows.Count - 1 Then
                    data1 &= dsData.Tables(1).Rows(index)("TOTAL_THUCTE")
                Else
                    data1 &= dsData.Tables(1).Rows(index)("TOTAL_THUCTE") & ","
                End If
            Next
            data1 &= "]}"
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


#End Region

End Class