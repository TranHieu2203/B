Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Recruitment.RecruitmentBusiness
Imports Telerik.Web.UI

Public Class ctrlRC_HRPlaningDetailNewEdit
    Inherits CommonView
    Public Overrides Property MustAuthorize As Boolean = True

#Region "Property"

    Property IDSelect As Decimal?
        Get
            Return PageViewState(Me.ID & "_IDSelect")
        End Get
        Set(ByVal value As Decimal?)
            PageViewState(Me.ID & "_IDSelect") = value
        End Set
    End Property

    Property OrgID As Decimal?
        Get
            Return PageViewState(Me.ID & "_OrgID")
        End Get
        Set(ByVal value As Decimal?)
            PageViewState(Me.ID & "_OrgID") = value
        End Set
    End Property

    Property dtHRPlaning As DataTable
        Get
            Return PageViewState(Me.ID & "_dtHRPlaning")
        End Get
        Set(ByVal value As DataTable)
            PageViewState(Me.ID & "_dtHRPlaning") = value
        End Set
    End Property

    Property objYearPl As HRYearPlaningDTO
        Get
            Return PageViewState(Me.ID & "_objYearPl")
        End Get
        Set(ByVal value As HRYearPlaningDTO)
            PageViewState(Me.ID & "_objYearPl") = value
        End Set
    End Property

#End Region

#Region "Page"

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            If Not IsPostBack Then
                GetParams()
                Refresh()
                UpdateControlState()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            InitControl()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub BindData()
        Try
            GetDataCombo()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub InitControl()
        Try
            Me.MainToolBar = tbarMain
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Save, ToolbarItem.Cancel)
            CType(MainToolBar.Items(0), RadToolBarButton).CausesValidation = True
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New RecruitmentRepository
        Dim rep2 As New Common.CommonRepository
        Try
            Select Case Message
                Case "UpdateView"
                    CurrentState = CommonMessage.STATE_EDIT

                Case "InsertView"
                    CurrentState = CommonMessage.STATE_NEW
                    objYearPl = rep.GetHrYearPlaningByID(IDSelect)
                    Dim org = rep2.GetOrganizationLocationInfo(OrgID)
                    If objYearPl IsNot Nothing Then
                        lbYear.Text = objYearPl.YEAR
                        lbVersion.Text = objYearPl.VERSION
                        lbEffectDate.Text = objYearPl.EFFECT_DATE.Value.ToString("dd/MM/yyyy")
                    End If
                    If org IsNot Nothing Then
                        lbOrgName.Text = org.NAME_VN
                    End If


            End Select

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

#End Region

#Region "Event"

    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick

        Dim gID As Decimal
        Dim rep As New RecruitmentRepository
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName

                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        Dim lstObj As New List(Of HRPlaningDetailDTO)
                        For Each item As GridDataItem In rgData.Items
                            If Not rep.ValidateHRPlanDetail(OrgID, item.GetDataKeyValue("TITLE_ID"), IDSelect, 0) Then
                                ShowMessage(Translate("Đã tồn tại dữ liệu về chức danh " & item.GetDataKeyValue("TITLE_NAME") & " trong phòng ban này"), NotifyType.Error)
                                Exit Sub
                            End If
                        Next
                        For Each item As GridDataItem In rgData.Items
                            Dim objPL As New HRPlaningDetailDTO
                            Dim rnMonth1 As RadNumericTextBox = DirectCast(item("MONTH_1").FindControl("MONTH_1"), RadNumericTextBox)
                            Dim rnMonth2 As RadNumericTextBox = DirectCast(item("MONTH_2").FindControl("MONTH_2"), RadNumericTextBox)
                            Dim rnMonth3 As RadNumericTextBox = DirectCast(item("MONTH_3").FindControl("MONTH_3"), RadNumericTextBox)
                            Dim rnMonth4 As RadNumericTextBox = DirectCast(item("MONTH_4").FindControl("MONTH_4"), RadNumericTextBox)
                            Dim rnMonth5 As RadNumericTextBox = DirectCast(item("MONTH_5").FindControl("MONTH_5"), RadNumericTextBox)
                            Dim rnMonth6 As RadNumericTextBox = DirectCast(item("MONTH_6").FindControl("MONTH_6"), RadNumericTextBox)
                            Dim rnMonth7 As RadNumericTextBox = DirectCast(item("MONTH_7").FindControl("MONTH_7"), RadNumericTextBox)
                            Dim rnMonth8 As RadNumericTextBox = DirectCast(item("MONTH_8").FindControl("MONTH_8"), RadNumericTextBox)
                            Dim rnMonth9 As RadNumericTextBox = DirectCast(item("MONTH_9").FindControl("MONTH_9"), RadNumericTextBox)
                            Dim rnMonth10 As RadNumericTextBox = DirectCast(item("MONTH_10").FindControl("MONTH_10"), RadNumericTextBox)
                            Dim rnMonth11 As RadNumericTextBox = DirectCast(item("MONTH_11").FindControl("MONTH_11"), RadNumericTextBox)
                            Dim rnMonth12 As RadNumericTextBox = DirectCast(item("MONTH_12").FindControl("MONTH_12"), RadNumericTextBox)
                            objPL.ORG_ID = OrgID
                            objPL.TITLE_ID = item.GetDataKeyValue("TITLE_ID")
                            objPL.YEAR_PLAN_ID = IDSelect
                            If IsNumeric(rnMonth1.Value) Then
                                objPL.MONTH_1 = rnMonth1.Value
                            End If
                            If IsNumeric(rnMonth2.Value) Then
                                objPL.MONTH_2 = rnMonth2.Value
                            End If
                            If IsNumeric(rnMonth3.Value) Then
                                objPL.MONTH_3 = rnMonth3.Value
                            End If
                            If IsNumeric(rnMonth4.Value) Then
                                objPL.MONTH_4 = rnMonth4.Value
                            End If
                            If IsNumeric(rnMonth5.Value) Then
                                objPL.MONTH_5 = rnMonth5.Value
                            End If
                            If IsNumeric(rnMonth6.Value) Then
                                objPL.MONTH_6 = rnMonth6.Value
                            End If
                            If IsNumeric(rnMonth7.Value) Then
                                objPL.MONTH_7 = rnMonth7.Value
                            End If
                            If IsNumeric(rnMonth8.Value) Then
                                objPL.MONTH_8 = rnMonth8.Value
                            End If
                            If IsNumeric(rnMonth9.Value) Then
                                objPL.MONTH_9 = rnMonth9.Value
                            End If
                            If IsNumeric(rnMonth10.Value) Then
                                objPL.MONTH_10 = rnMonth10.Value
                            End If
                            If IsNumeric(rnMonth11.Value) Then
                                objPL.MONTH_11 = rnMonth11.Value
                            End If
                            If IsNumeric(rnMonth12.Value) Then
                                objPL.MONTH_12 = rnMonth12.Value
                            End If
                            lstObj.Add(objPL)
                        Next
                        If rep.InsertHRPlanDetail(lstObj) Then
                            Dim str As String = "getRadWindow().close('1');"
                            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                        End If
                    End If

                Case CommonMessage.TOOLBARITEM_CANCEL
                    Dim str As String = "getRadWindow().close('0');"
                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
        UpdateControlState()
    End Sub

    Private Sub btnAutoFill_Click(sender As Object, e As EventArgs) Handles btnAutoFill.Click
        Try
            If rgData.SelectedItems.Count < 1 Then
                ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Error)
                Exit Sub
            End If
            If IsNumeric(rnAutoPlaning.Value) Then
                For Each item As GridDataItem In rgData.SelectedItems
                    Dim rnMonth1 As RadNumericTextBox = DirectCast(item("MONTH_1").FindControl("MONTH_1"), RadNumericTextBox)
                    Dim rnMonth2 As RadNumericTextBox = DirectCast(item("MONTH_2").FindControl("MONTH_2"), RadNumericTextBox)
                    Dim rnMonth3 As RadNumericTextBox = DirectCast(item("MONTH_3").FindControl("MONTH_3"), RadNumericTextBox)
                    Dim rnMonth4 As RadNumericTextBox = DirectCast(item("MONTH_4").FindControl("MONTH_4"), RadNumericTextBox)
                    Dim rnMonth5 As RadNumericTextBox = DirectCast(item("MONTH_5").FindControl("MONTH_5"), RadNumericTextBox)
                    Dim rnMonth6 As RadNumericTextBox = DirectCast(item("MONTH_6").FindControl("MONTH_6"), RadNumericTextBox)
                    Dim rnMonth7 As RadNumericTextBox = DirectCast(item("MONTH_7").FindControl("MONTH_7"), RadNumericTextBox)
                    Dim rnMonth8 As RadNumericTextBox = DirectCast(item("MONTH_8").FindControl("MONTH_8"), RadNumericTextBox)
                    Dim rnMonth9 As RadNumericTextBox = DirectCast(item("MONTH_9").FindControl("MONTH_9"), RadNumericTextBox)
                    Dim rnMonth10 As RadNumericTextBox = DirectCast(item("MONTH_10").FindControl("MONTH_10"), RadNumericTextBox)
                    Dim rnMonth11 As RadNumericTextBox = DirectCast(item("MONTH_11").FindControl("MONTH_11"), RadNumericTextBox)
                    Dim rnMonth12 As RadNumericTextBox = DirectCast(item("MONTH_12").FindControl("MONTH_12"), RadNumericTextBox)
                    rnMonth1.Value = rnAutoPlaning.Value
                    rnMonth2.Value = rnAutoPlaning.Value
                    rnMonth3.Value = rnAutoPlaning.Value
                    rnMonth4.Value = rnAutoPlaning.Value
                    rnMonth5.Value = rnAutoPlaning.Value
                    rnMonth6.Value = rnAutoPlaning.Value
                    rnMonth7.Value = rnAutoPlaning.Value
                    rnMonth8.Value = rnAutoPlaning.Value
                    rnMonth9.Value = rnAutoPlaning.Value
                    rnMonth10.Value = rnAutoPlaning.Value
                    rnMonth11.Value = rnAutoPlaning.Value
                    rnMonth12.Value = rnAutoPlaning.Value
                Next
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgData_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
        Try
            CreateDataFilter()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim _filter As New HRPlaningDetailDTO
        Dim rep As New RecruitmentRepository
        Dim bCheck As Boolean = False
        Try
            SetValueObjectByRadGrid(rgData, _filter)

            Dim _param = New ParamDTO With {.ORG_ID = Decimal.Parse(OrgID), _
                                               .IS_DISSOLVE = False}

            _filter.EFFECT_DATE = objYearPl.EFFECT_DATE
            Dim MaximumRows As Integer
            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()
            If Not isFull Then
                If Sorts IsNot Nothing Then
                    dtHRPlaning = rep.GetDetailOrgTitle(_filter, _param, MaximumRows, Sorts)
                Else
                    dtHRPlaning = rep.GetDetailOrgTitle(_filter, _param, MaximumRows)
                End If
            End If

            rgData.DataSource = Nothing

            rgData.DataSource = dtHRPlaning
            rgData.VirtualItemCount = MaximumRows

        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "Custom"

    Public Overrides Sub UpdateControlState()
        Try
        Catch ex As Exception
            Throw ex
        End Try
        'ChangeToolbarState()
    End Sub

    Private Sub GetDataCombo()
        Dim store As New RecruitmentStoreProcedure
        Try
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GetParams()
        Try
            If CurrentState Is Nothing Then
                If Request.Params("ID") IsNot Nothing Then
                    IDSelect = Decimal.Parse(Request.Params("ID"))
                End If
                If Request.Params("ORG_ID") IsNot Nothing Then
                    OrgID = Decimal.Parse(Request.Params("ORG_ID"))
                End If
                If IDSelect IsNot Nothing And OrgID IsNot Nothing Then
                    Refresh("InsertView")
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

End Class