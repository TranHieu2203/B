Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI

Public Class ctrlHU_CompetencyAssNewEdit

    Inherits Common.CommonView
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup

#Region "Property"

    '0 - normal
    '1 - Employee
    '2 - Signer
    '3 - Salary
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
            Me.ctrlMessageBox.Listener = Me
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
        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
                hidYear.Value = Request.Params("year")
                hidPeriodID.Value = Request.Params("periodid")
                If Request.Params("emp") IsNot Nothing Then
                    hidEmpID.Value = Request.Params("emp")
                    Using rep As New ProfileRepository
                        Dim obj As New CompetencyAssDTO
                        obj.EMPLOYEE_ID = hidEmpID.Value
                        obj.COMPETENCY_PERIOD_ID = hidPeriodID.Value
                        Dim lst = rep.GetCompetencyAss(obj)
                        If lst.Count > 0 Then
                            obj = lst(0)
                            txtEmployeeCode.Text = obj.EMPLOYEE_CODE
                            txtEmployeeName.Text = obj.EMPLOYEE_NAME
                            hidTitleID.Value = obj.TITLE_ID
                            txtTitleName.Text = obj.TITLE_NAME
                            txtOrgName.Text = obj.ORG_NAME
                            Dim lstAppendix = rep.GetCompetencyAppendix(New CompetencyAppendixDTO With {.TITLE_ID = hidTitleID.Value})
                            If lstAppendix.Count > 0 Then
                                txtRemark.Text = lstAppendix(0).REMARK
                            End If
                            rgMain.Rebind()
                            For Each i As GridItem In rgMain.Items
                                i.Edit = True
                            Next
                            rgMain.Rebind()
                        End If
                    End Using
                End If
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim rep As New ProfileRepository
        Dim _filter As New CompetencyAssDtlDTO
        Try
            SetValueObjectByRadGrid(rgMain, _filter)
            If hidTitleID.Value <> "" Then
                _filter.TITLE_ID = hidTitleID.Value
            End If
            If hidEmpID.Value <> "" Then
                _filter.EMPLOYEE_ID = hidEmpID.Value
            End If
            If hidPeriodID.Value <> "" Then
                _filter.COMPETENCY_PERIOD_ID = hidPeriodID.Value
            End If
            rgMain.DataSource = rep.GetCompetencyAssDtl(_filter)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Overrides Sub UpdateControlState()
        Dim rep As New ProfileRepository
        Try

            Select Case isLoadPopup
                Case 1
                    If Not FindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                        ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                        FindEmployee.Controls.Add(ctrlFindEmployeePopup)
                        ctrlFindEmployeePopup.MultiSelect = False
                        ctrlFindEmployeePopup.MustHaveContract = False
                    End If
            End Select
            Select Case CurrentState
                Case CommonMessage.STATE_EDIT
                    EnableControlAll(False, btnEmployee)
                Case Else
                    EnableControlAll(True, btnEmployee)
            End Select

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Public Overrides Sub BindData()
        Try

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Event"

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim rep As New ProfileRepository
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        Dim objAss As New CompetencyAssDTO
                        objAss.COMPETENCY_PERIOD_ID = hidPeriodID.Value
                        objAss.EMPLOYEE_ID = hidEmpID.Value
                        objAss.TITLE_ID = hidTitleID.Value
                        Dim lst As New List(Of CompetencyAssDtlDTO)
                        For Each item As GridDataItem In rgMain.Items
                            If item.Edit = True Then
                                Dim obj As New CompetencyAssDtlDTO
                                obj.ID = item.GetDataKeyValue("ID")
                                Dim edit = CType(item, GridEditableItem)
                                Dim cboLevelNumber As RadComboBox = CType(edit.FindControl("cboLevelNumber"), RadComboBox)
                                Dim txtRemark As RadTextBox = CType(edit.FindControl("txtRemark"), RadTextBox)
                                With obj
                                    .EMPLOYEE_ID = item.GetDataKeyValue("EMPLOYEE_ID")
                                    .COMPETENCY_ID = item.GetDataKeyValue("COMPETENCY_ID")
                                    .TITLE_ID = item.GetDataKeyValue("TITLE_ID")
                                    If cboLevelNumber.SelectedValue <> "" Then
                                        .LEVEL_NUMBER_ASS = cboLevelNumber.SelectedValue
                                    End If
                                    .REMARK = txtRemark.Text
                                End With
                                lst.Add(obj)
                            End If
                        Next
                        If rep.UpdateCompetencyAssDtl(objAss, lst) Then
                            CurrentState = CommonMessage.STATE_NORMAL
                            'Dim str As String = "getRadWindow().close('1');"
                            'ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                            'UpdateControlState()
                            ''POPUPTOLINK
                            Response.Redirect("/Default.aspx?mid=Competency&fid=ctrlHU_CompetencyAss&group=Business")
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                        End If
                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    ''POPUPTOLINK_CANCEL
                    Response.Redirect("/Default.aspx?mid=Competency&fid=ctrlHU_CompetencyAss&group=Business")
            End Select

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub rgMain_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgMain.NeedDataSource
        Try
            CreateDataFilter()

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub


    Protected Sub btnEmployee_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEmployee.Click
        Try
            isLoadPopup = 1
            UpdateControlState()
            ctrlFindEmployeePopup.Show()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub ctrlFind_CancelClick(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles ctrlFindEmployeePopup.CancelClicked
        isLoadPopup = 0
    End Sub


    Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Try
            Dim emp = ctrlFindEmployeePopup.SelectedEmployee(0)
            txtEmployeeCode.Text = emp.EMPLOYEE_CODE
            txtEmployeeName.Text = emp.FULLNAME_VN
            txtTitleName.Text = emp.TITLE_NAME
            txtOrgName.Text = emp.ORG_NAME
            hidTitleID.Value = emp.TITLE_ID
            hidEmpID.Value = emp.EMPLOYEE_ID
            Using rep As New ProfileRepository
                Dim lstAppendix = rep.GetCompetencyAppendix(New CompetencyAppendixDTO With {.TITLE_ID = hidTitleID.Value})
                If lstAppendix.Count > 0 Then
                    txtRemark.Text = lstAppendix(0).REMARK
                End If
            End Using
            rgMain.Rebind()
            For Each i As GridItem In rgMain.Items
                i.Edit = True
            Next
            rgMain.Rebind()
            isLoadPopup = 0
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgMain_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgMain.ItemDataBound
        If e.Item.Edit Then
            Dim edit = CType(e.Item, GridEditableItem)
            Dim cboLevelNumber As RadComboBox = CType(edit.FindControl("cboLevelNumber"), RadComboBox)
            If edit.GetDataKeyValue("LEVEL_NUMBER_ASS") IsNot Nothing Then
                cboLevelNumber.SelectedValue = edit.GetDataKeyValue("LEVEL_NUMBER_ASS")
            End If
            Dim txtRemark As RadTextBox = CType(edit.FindControl("txtRemark"), RadTextBox)
            If edit.GetDataKeyValue("REMARK") IsNot Nothing Then
                txtRemark.Text = edit.GetDataKeyValue("REMARK")
            End If
        End If
    End Sub


#End Region

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