Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Performance.PerformanceBusiness
Imports Telerik.Web.UI

Public Class ctrlPortalIETDetailResult
    Inherits Common.CommonView
    Dim _classPath As String = "Performance/Module/Portal/" + Me.GetType().Name.ToString()

#Region "Property"

    Property dtDetail As List(Of PE_KPI_ASSESMENT_DETAIL_DTO)
        Get
            Return ViewState(Me.ID & "dtDetail")
        End Get
        Set(ByVal value As List(Of PE_KPI_ASSESMENT_DETAIL_DTO))
            ViewState(Me.ID & "dtDetail") = value
        End Set
    End Property
    Public Property Criteria As String
    ''' <summary>
    ''' IDSelect
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IDSelect As Decimal
        Get
            Return ViewState(Me.ID & "_IDSelect")
        End Get

        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_IDSelect") = value
        End Set
    End Property
#End Region

#Region "Page"

    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary>Load page, trang thai page, trang thai control</summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary> Khoi tao control, set thuoc tinh cho grid </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            InitControl()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary>Khoi tao, load menu toolbar</summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Me.MainToolBar = tbarOT
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Save, ToolbarItem.Cancel)
            CType(MainToolBar.Items(0), RadToolBarButton).CausesValidation = True
            'Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"
    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary> 
    ''' Event click item tren menu toolbar
    ''' Set trang thai control sau khi process xong 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim rep As New PerformanceRepository
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE
                    If rep.UpdateKpiAssessmentDetail(dtDetail) Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub txtNOTE_TextChanged(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim edit = CType(sender, RadTextBox)
            Dim row = CType(edit.NamingContainer, GridEditableItem)
            Dim note = edit.Text
            Dim id = row.GetDataKeyValue("ID")
            For Each item As PE_KPI_ASSESMENT_DETAIL_DTO In dtDetail
                If item.ID = id Then
                    item.NOTE = note
                    Exit For
                End If
            Next
            RebindGrid()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Protected Sub rnEMPLOYEE_POINT_TextChanged(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim edit = CType(sender, RadNumericTextBox)
            Dim row = CType(edit.NamingContainer, GridEditableItem)
            Dim empPoint = edit.Value
            Dim id = row.GetDataKeyValue("ID")
            If Not IsNumeric(empPoint) AndAlso empPoint <> "" Then
                ShowMessage(Translate("Chỉ được nhập số"), NotifyType.Warning)
                RebindGrid()
                Exit Sub
            Else
                For Each item As PE_KPI_ASSESMENT_DETAIL_DTO In dtDetail
                    If item.ID = id Then
                        item.EMPLOYEE_POINT = empPoint
                        Exit For
                    End If
                Next
                RebindGrid()
                Exit Sub
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub txtEMPLOYEE_ACTUAL_TextChanged(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim edit = CType(sender, RadTextBox)
            Dim row = CType(edit.NamingContainer, GridEditableItem)
            Dim empActual = edit.Text
            Dim id = row.GetDataKeyValue("ID")
            Dim code = If(row.GetDataKeyValue("TARGET_TYPE_CODE") Is Nothing, "", row.GetDataKeyValue("TARGET_TYPE_CODE").ToString)
            If code = "NUMBER" Then
                If Not IsNumeric(empActual) AndAlso empActual <> "" Then
                    ShowMessage(Translate("Chỉ được nhập số"), NotifyType.Warning)
                    RebindGrid()
                    Exit Sub
                Else
                    For Each item As PE_KPI_ASSESMENT_DETAIL_DTO In dtDetail
                        If item.ID = id Then
                            item.EMPLOYEE_ACTUAL = empActual
                            If item.GOAL_TYPE_CODE = "CNCT" Then
                                If IsNumeric(item.EMPLOYEE_ACTUAL) AndAlso IsNumeric(item.TARGET) AndAlso IsNumeric(item.BENCHMARK) Then
                                    If Integer.Parse(item.EMPLOYEE_ACTUAL) >= Integer.Parse(item.TARGET) Then
                                        item.EMPLOYEE_POINT = Integer.Parse(item.TARGET) / Integer.Parse(item.EMPLOYEE_ACTUAL) * Integer.Parse(item.BENCHMARK) * item.SOA / 100
                                    Else
                                        item.EMPLOYEE_POINT = Integer.Parse(item.BENCHMARK)
                                    End If
                                End If
                            ElseIf item.GOAL_TYPE_CODE = "CLCT" Then
                                If IsNumeric(item.EMPLOYEE_ACTUAL) AndAlso IsNumeric(item.TARGET) AndAlso IsNumeric(item.BENCHMARK) Then
                                    If Integer.Parse(item.EMPLOYEE_ACTUAL) <= Integer.Parse(item.TARGET) Then
                                        item.EMPLOYEE_POINT = Integer.Parse(item.BENCHMARK) / Integer.Parse(item.TARGET) * Integer.Parse(item.EMPLOYEE_ACTUAL) * item.SOA / 100
                                    Else
                                        item.EMPLOYEE_POINT = Integer.Parse(item.BENCHMARK)
                                    End If
                                End If
                            ElseIf item.GOAL_TYPE_CODE = "DKD" Then
                                If IsNumeric(item.EMPLOYEE_ACTUAL) AndAlso IsNumeric(item.BENCHMARK) Then
                                    If Integer.Parse(item.EMPLOYEE_ACTUAL) >= Integer.Parse(item.TARGET) Then
                                        item.EMPLOYEE_POINT = Integer.Parse(item.BENCHMARK)
                                    Else
                                        item.EMPLOYEE_POINT = 0
                                    End If
                                End If
                            End If
                            Exit For
                        End If
                    Next
                    RebindGrid()
                    Exit Sub
                End If
            ElseIf code = "DATE" Then
                If Not IsDate(empActual) AndAlso empActual <> "" Then
                    ShowMessage(Translate("Chỉ được nhập ngày (dd/MM/yyyy)"), NotifyType.Warning)
                    RebindGrid()
                    Exit Sub
                Else
                    For Each item As PE_KPI_ASSESMENT_DETAIL_DTO In dtDetail
                        If item.ID = id Then
                            item.EMPLOYEE_ACTUAL = empActual
                            Exit For
                        End If
                    Next
                    RebindGrid()
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary>Load datasource cho grid</summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgMain.NeedDataSource
        Dim rep As New PerformanceRepository
        Try
            Dim _filter As New PE_KPI_ASSESMENT_DETAIL_DTO
            Dim str As String = Request.Params("Criteria")
            Dim goal = Request.Params("GoalID")
            _filter.GOAL_ID = Decimal.Parse(goal)
            Dim lstCode As New List(Of String)
            lstCode = str.Split(",").ToList
            SetValueObjectByRadGrid(rgMain, _filter)
            Dim _param As New ParamDTO

            Dim MaximumRows As Integer
            Dim Sorts As String = rgMain.MasterTableView.SortExpressions.GetSortString()
            If dtDetail IsNot Nothing Then
            Else
                If Sorts IsNot Nothing Then
                    dtDetail = rep.GetKpiAssessmentDetailByListCode(lstCode, _filter, rgMain.CurrentPageIndex, rgMain.PageSize, MaximumRows, _param, Sorts)
                Else
                    dtDetail = rep.GetKpiAssessmentDetailByListCode(lstCode, _filter, rgMain.CurrentPageIndex, rgMain.PageSize, MaximumRows, _param)
                End If
            End If
            rgMain.DataSource = dtDetail
            rgMain.VirtualItemCount = MaximumRows
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub RadGrid_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgMain.PreRender
        Try
            For Each items As GridDataItem In rgMain.MasterTableView.Items
                items.Edit = True
            Next
            rgMain.MasterTableView.Rebind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub RadGrid_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgMain.ItemDataBound
        Dim txtEMPLOYEE_ACTUAL As New RadTextBox
        Dim rnEMPLOYEE_POINT As New RadNumericTextBox
        Try
            If e.Item.Edit Then
                Dim edit = CType(e.Item, GridEditableItem)
                SetDataToGrid(edit)
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Custom"
    Protected Sub RebindGrid()
        Try
            rgMain.Rebind()
            For Each items As GridDataItem In rgMain.MasterTableView.Items
                items.Edit = True
            Next
            rgMain.MasterTableView.Rebind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub SetDataToGrid(ByVal EditItem As GridEditableItem)
        Dim txtEMPLOYEE_ACTUAL, txtDIRECT_ACTUAL, txtNOTE_QLTT, txtNOTE As New RadTextBox
        Dim rnEMPLOYEE_POINT, rnDIRECT_POINT As New RadNumericTextBox
        Try
            txtEMPLOYEE_ACTUAL = CType(EditItem.FindControl("txtEMPLOYEE_ACTUAL"), RadTextBox)
            rnEMPLOYEE_POINT = CType(EditItem.FindControl("rnEMPLOYEE_POINT"), RadNumericTextBox)
            txtNOTE = CType(EditItem.FindControl("txtNOTE"), RadTextBox)
            Dim id = EditItem.GetDataKeyValue("ID")
            For Each item As PE_KPI_ASSESMENT_DETAIL_DTO In dtDetail
                If item.ID = id Then
                    txtEMPLOYEE_ACTUAL.Text = item.EMPLOYEE_ACTUAL
                    rnEMPLOYEE_POINT.Value = item.EMPLOYEE_POINT
                    txtNOTE.Text = item.NOTE
                End If
            Next
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region
End Class