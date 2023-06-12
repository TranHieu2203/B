Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Payroll.PayrollBusiness
Imports Telerik.Web.UI

Public Class ctrlPA_SalaryFund
    Inherits Common.CommonView

#Region "Property"

#End Region

#Region "Page"
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        ctrlOrg.OrganizationType = OrganizationType.OrganizationLocation
        InitControl()
    End Sub

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            Refresh()
            UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMenu
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Edit, ToolbarItem.Save,
                                       ToolbarItem.Cancel)
            CType(MainToolBar.Items(1), RadToolBarButton).CausesValidation = True
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
                CreateDataFilter()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()
        Dim rep As New PayrollRepository
        Try
            Select Case CurrentState

                Case CommonMessage.STATE_NORMAL
                    EnableControlAll(False, cboMonth, rntxtSalaryAllowance,
                                     rntxtSalaryHard, rntxtSalaryOther,
                                     rntxtSalarySoft, cboYear)
                    EnableControlAll(True, cboMonth, cboYear)
                    ctrlOrg.Enabled = True
                Case CommonMessage.STATE_NEW
                    EnableControlAll(True, cboMonth, rntxtSalaryAllowance,
                                     rntxtSalaryHard, rntxtSalaryOther,
                                     rntxtSalarySoft, cboYear)
                    EnableControlAll(False, cboMonth, cboYear)
                    ctrlOrg.Enabled = False
                Case CommonMessage.STATE_EDIT
                    EnableControlAll(True, cboMonth, rntxtSalaryAllowance,
                                     rntxtSalaryHard, rntxtSalaryOther,
                                     rntxtSalarySoft, cboYear)
                    EnableControlAll(False, cboMonth, cboYear)
                    ctrlOrg.Enabled = False
            End Select
            UpdateToolbarState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Public Overrides Sub BindData()
        Try
            LoadYear()
            LoadMonth()
            cboYear.SelectedValue = Date.Now.Year
            cboMonth.SelectedValue = Date.Now.Month
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objSalaryFund As New PASalaryFundDTO

        Try
            Using rep As New PayrollRepository
                Select Case CType(e.Item, RadToolBarButton).CommandName
                    Case CommonMessage.TOOLBARITEM_CREATE
                        CurrentState = CommonMessage.STATE_NEW
                        ClearControlValue(rntxtSalaryAllowance,
                                          rntxtSalaryHard, rntxtSalaryOther,
                                          rntxtSalarySoft, rntxtSalaryTotal)
                        UpdateControlState()
                    Case CommonMessage.TOOLBARITEM_EDIT
                        CurrentState = CommonMessage.STATE_EDIT
                        UpdateControlState()

                    Case CommonMessage.TOOLBARITEM_SAVE
                        If Page.IsValid Then
                            objSalaryFund.YEAR = cboYear.SelectedValue
                            objSalaryFund.MONTH = cboMonth.SelectedValue
                            objSalaryFund.ORG_ID = ctrlOrg.CurrentValue
                            objSalaryFund.SAL_ALLOWANCE = rntxtSalaryAllowance.Value
                            objSalaryFund.SAL_HARD = rntxtSalaryHard.Value
                            objSalaryFund.SAL_OTHER = rntxtSalaryOther.Value
                            objSalaryFund.SAL_SOFT = rntxtSalarySoft.Value
                            objSalaryFund.SAL_TOTAL = rntxtSalaryTotal.Value

                            If rep.UpdateSalaryFund(objSalaryFund) Then
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                                CurrentState = CommonMessage.STATE_NORMAL
                                CreateDataFilter()
                                UpdateControlState()
                            Else
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                            End If
                        End If
                    Case CommonMessage.TOOLBARITEM_CANCEL
                        CurrentState = CommonMessage.STATE_NORMAL
                        CreateDataFilter()
                        UpdateControlState()
                End Select
            End Using
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlOrg_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged
        CreateDataFilter()
    End Sub

    Protected Sub cboYear_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cboYear.TextChanged
        'LoadMonth()
        CreateDataFilter()
    End Sub
    Protected Sub cboMonth_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboMonth.SelectedIndexChanged
        CreateDataFilter()
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

    Public Sub LoadMonth()
        Dim tableMonth As New DataTable
        tableMonth.Columns.Add("MONTH", GetType(String))
        tableMonth.Columns.Add("ID", GetType(String))
        Dim rowMonth As DataRow
        For index = 1 To 12
            rowMonth = tableMonth.NewRow

            If index = 0 Then
                rowMonth("ID") = 0
                rowMonth("MONTH") = ""
                tableMonth.Rows.Add(rowMonth)
            Else
                rowMonth("ID") = index
                rowMonth("MONTH") = index
                tableMonth.Rows.Add(rowMonth)
            End If
        Next
        FillRadCombobox(cboMonth, tableMonth, "MONTH", "ID")

    End Sub

    Private Sub UpdateToolbarState()
        Try
            ChangeToolbarState()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Sub CreateDataFilter()

        Using rep As New PayrollRepository

            Dim _filter As New PASalaryFundDTO
            _filter.YEAR = cboYear.SelectedValue
            _filter.MONTH = cboMonth.SelectedValue
            _filter.ORG_ID = ctrlOrg.CurrentValue
            Dim obj = rep.GetSalaryFundByID(_filter)
            ClearControlValue(rntxtSalaryAllowance,
                              rntxtSalaryHard, rntxtSalaryOther,
                              rntxtSalarySoft, rntxtSalaryTotal)
            If obj IsNot Nothing Then
                rntxtSalaryAllowance.Value = obj.SAL_ALLOWANCE
                rntxtSalaryHard.Value = obj.SAL_HARD
                rntxtSalaryOther.Value = obj.SAL_OTHER
                rntxtSalarySoft.Value = obj.SAL_SOFT
                rntxtSalaryTotal.Value = obj.SAL_TOTAL
            End If
        End Using

    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        CreateDataFilter()
    End Sub
#End Region

End Class