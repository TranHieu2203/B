Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI

Public Class ctrlHU_EmpDtlTraining
    Inherits CommonView
    Public Overrides Property MustAuthorize As Boolean = False

#Region "Property"

    Property EmployeeInfo As EmployeeDTO
        Get
            Return PageViewState(Me.ID & "_EmployeeInfo")
        End Get
        Set(ByVal value As EmployeeDTO)
            PageViewState(Me.ID & "_EmployeeInfo") = value
        End Set
    End Property

    Public Property EmployeeTrain As List(Of EmployeeTrainForCompanyDTO)
        Get
            Return ViewState(Me.ID & "_EmployeeTrain")
        End Get
        Set(ByVal value As List(Of EmployeeTrainForCompanyDTO))
            ViewState(Me.ID & "_EmployeeTrain") = value
        End Set
    End Property

    Property DeleteEmployeeTrain As List(Of Decimal)
        Get
            Return ViewState(Me.ID & "_DeleteEmployeeTrain")
        End Get
        Set(ByVal value As List(Of Decimal))
            ViewState(Me.ID & "_DeleteEmployeeTrain") = value
        End Set
    End Property

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
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            ctrlEmpBasicInfo.SetProperty("EmployeeInfo", EmployeeInfo)
            Refresh()
            UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            rgEmployeeTrain.SetFilter()
            rgEmployeeTrain.AllowCustomPaging = True
            'rgEmployeeTrain.ClientSettings.EnablePostBackOnRowClick = True
            InitControl()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMainToolBar
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create,
                                       ToolbarItem.Edit,
                                       ToolbarItem.Save, ToolbarItem.Cancel,
                                       ToolbarItem.Delete)
            CType(MainToolBar.Items(2), RadToolBarButton).CausesValidation = True
            CType(Me.MainToolBar.Items(2), RadToolBarButton).Enabled = False
            CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = False
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
                rgEmployeeTrain.Rebind()
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgEmployeeTrain.Rebind()
                        SelectedItemDataGridByKey(rgEmployeeTrain, IDSelect, , rgEmployeeTrain.CurrentPageIndex)

                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgEmployeeTrain.CurrentPageIndex = 0
                        rgEmployeeTrain.MasterTableView.SortExpressions.Clear()
                        rgEmployeeTrain.Rebind()
                        SelectedItemDataGridByKey(rgEmployeeTrain, IDSelect, )
                    Case "Cancel"
                        rgEmployeeTrain.MasterTableView.ClearSelectedItems()
                End Select
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub CreateDataFilter()

        Dim obj As New EmployeeTrainDTO
        Try
            SetValueObjectByRadGrid(rgEmployeeTrain, obj)

            Dim rep As New ProfileBusinessRepository
            Dim objEmployeeTrain As New EmployeeTrainForCompanyDTO
            objEmployeeTrain.EMPLOYEE_ID = EmployeeInfo.ID

            Me.EmployeeTrain = rep.GetEmployeeTrainForCompany(objEmployeeTrain)
            rep.Dispose()
            rgEmployeeTrain.DataSource = Me.EmployeeTrain
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()

        Try
            'Select Case CurrentState
            '    Case CommonMessage.STATE_NEW

            '        EnabledGridNotPostback(rgEmployeeTrain, False)

            '        rnbFMonth.Text = ""
            '        rnbTMonth.Text = ""
            '        rnbFYear.Text = ""
            '        rnbTYear.Text = ""
            '        txtTrainingSchool.Text = ""
            '        cboTrainingForm.SelectedValue = ""
            '        cboLearningLevel.SelectedValue = ""
            '        cboMajor.SelectedValue = ""
            '        rntGraduateYear.Value = Now.Year
            '        cboMark.SelectedValue = ""
            '        rtbTrainingContent.Text = ""

            '        rnbFMonth.ReadOnly = False
            '        rnbTMonth.ReadOnly = False
            '        rnbFYear.ReadOnly = False
            '        rnbTYear.ReadOnly = False

            '        txtTrainingSchool.ReadOnly = False
            '        Utilities.EnableRadCombo(cboTrainingForm, True)
            '        Utilities.EnableRadCombo(cboLearningLevel, True)
            '        Utilities.EnableRadCombo(cboMajor, True)
            '        rntGraduateYear.ReadOnly = False
            '        Utilities.EnableRadCombo(cboMark, True)
            '        rtbTrainingContent.ReadOnly = False

            '    Case CommonMessage.STATE_NORMAL
            '        rnbFMonth.Text = ""
            '        rnbTMonth.Text = ""
            '        rnbFYear.Text = ""
            '        rnbTYear.Text = ""
            '        txtTrainingSchool.Text = ""
            '        cboTrainingForm.SelectedValue = ""
            '        cboLearningLevel.SelectedValue = ""
            '        cboMajor.SelectedValue = ""
            '        rntGraduateYear.Value = Now.Year
            '        cboMark.SelectedValue = ""
            '        rtbTrainingContent.Text = ""

            '        EnabledGridNotPostback(rgEmployeeTrain, True)
            '        rnbFMonth.ReadOnly = True
            '        rnbTMonth.ReadOnly = True
            '        rnbFYear.ReadOnly = True
            '        rnbTYear.ReadOnly = True
            '        txtTrainingSchool.ReadOnly = True
            '        Utilities.EnableRadCombo(cboTrainingForm, False)
            '        Utilities.EnableRadCombo(cboLearningLevel, False)
            '        Utilities.EnableRadCombo(cboMajor, False)
            '        rntGraduateYear.ReadOnly = True
            '        Utilities.EnableRadCombo(cboMark, False)
            '        rtbTrainingContent.ReadOnly = True
            '    Case CommonMessage.STATE_EDIT

            '        EnabledGridNotPostback(rgEmployeeTrain, False)

            '        rnbFMonth.ReadOnly = False
            '        rnbTMonth.ReadOnly = False
            '        rnbFYear.ReadOnly = False
            '        rnbTYear.ReadOnly = False
            '        txtTrainingSchool.ReadOnly = False
            '        Utilities.EnableRadCombo(cboTrainingForm, True)
            '        Utilities.EnableRadCombo(cboLearningLevel, True)
            '        Utilities.EnableRadCombo(cboMajor, True)
            '        rntGraduateYear.ReadOnly = False
            '        Utilities.EnableRadCombo(cboMark, True)
            '        rtbTrainingContent.ReadOnly = False
            '    Case CommonMessage.STATE_DELETE
            '        Dim rep As New ProfileBusinessRepository
            '        If rep.DeleteEmployeeTrain(DeleteEmployeeTrain) Then
            '            DeleteEmployeeTrain = Nothing
            '            Refresh("UpdateView")
            '            UpdateControlState()
            '        Else
            '            CurrentState = CommonMessage.STATE_NORMAL
            '            UpdateControlState()
            '            ShowMessage(Translate(CommonMessage.MESSAGE_IS_USING), NotifyType.Error)
            '        End If
            'End Select
            'ChangeToolbarState()
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Overrides Sub BindData()
        'Dim dic As New Dictionary(Of String, Control)
        'dic.Add("FMONTH", rnbFMonth)
        'dic.Add("FYEAR", rnbFYear)
        'dic.Add("TMONTH", rnbTMonth)
        'dic.Add("TYEAR", rnbTYear)
        'dic.Add("SCHOOL_NAME", txtTrainingSchool)
        'dic.Add("TRAINING_FORM", cboTrainingForm)
        'dic.Add("LEARNING_LEVEL", cboLearningLevel)
        'dic.Add("MAJOR", cboMajor)
        'dic.Add("MARK", cboMark)
        'dic.Add("TRAINING_CONTENT", rtbTrainingContent)
        'Utilities.OnClientRowSelectedChanged(rgEmployeeTrain, dic)

        Try
            'Dim rep As New ProfileRepository
            'Dim comboBoxDataDTO As New ComboBoxDataDTO
            'If comboBoxDataDTO Is Nothing Then
            '    comboBoxDataDTO = New ComboBoxDataDTO
            'End If
            'comboBoxDataDTO.GET_TRAINING_FORM = True
            'comboBoxDataDTO.GET_LEARNING_LEVEL = True
            'comboBoxDataDTO.GET_MAJOR = True
            'comboBoxDataDTO.GET_MARK_EDU = True
            'rep.GetComboList(comboBoxDataDTO)
            'If comboBoxDataDTO IsNot Nothing Then
            '    FillDropDownList(cboTrainingForm, comboBoxDataDTO.LIST_TRAINING_FORM, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboTrainingForm.SelectedValue)
            '    FillDropDownList(cboLearningLevel, comboBoxDataDTO.LIST_LEARNING_LEVEL, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboLearningLevel.SelectedValue)
            '    FillDropDownList(cboMajor, comboBoxDataDTO.LIST_MAJOR, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboMajor.SelectedValue)
            '    FillDropDownList(cboMark, comboBoxDataDTO.LIST_MARK_EDU, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboMark.SelectedValue)
            'End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

#End Region

#Region "Event"
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objEmployeeTrain As New EmployeeTrainDTO
        'Dim gID As Decimal

        Try
            'Select Case CType(e.Item, RadToolBarButton).CommandName
            '    Case CommonMessage.TOOLBARITEM_CREATE
            '        CurrentState = CommonMessage.STATE_NEW

            '        UpdateControlState()
            '    Case CommonMessage.TOOLBARITEM_EDIT
            '        If rgEmployeeTrain.SelectedItems.Count = 0 Then
            '            ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
            '            Exit Sub
            '        End If
            '        If rgEmployeeTrain.SelectedItems.Count > 1 Then
            '            ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
            '            Exit Sub
            '        End If

            '        CurrentState = CommonMessage.STATE_EDIT

            '        UpdateControlState()

            '    Case CommonMessage.TOOLBARITEM_DELETE

            '        If rgEmployeeTrain.SelectedItems.Count = 0 Then
            '            ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
            '            Exit Sub
            '        End If
            '        DeleteEmployeeTrain = New List(Of Decimal)
            '        For idx = 0 To rgEmployeeTrain.SelectedItems.Count - 1
            '            Dim item As GridDataItem = rgEmployeeTrain.SelectedItems(idx)
            '            DeleteEmployeeTrain.Add(item.GetDataKeyValue("ID"))
            '        Next


            '        ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
            '        ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
            '        ctrlMessageBox.DataBind()
            '        ctrlMessageBox.Show()
            '    Case CommonMessage.TOOLBARITEM_EXPORT
            '        Utilities.GridExportExcel(rgEmployeeTrain, "EmployeeTrain")
            '    Case CommonMessage.TOOLBARITEM_SAVE
            '        If Page.IsValid Then
            '            Dim rep As New ProfileBusinessRepository

            '            If rnbFMonth.Text = "" Or rnbFYear.Text = "" Then
            '                ShowMessage(Translate("Chưa nhập đủ thời gian bắt đầu"), NotifyType.Warning)
            '                Exit Sub
            '            End If
            '            If (rnbTMonth.Text = "" And rnbTYear.Text <> "") Or (rnbTMonth.Text <> "" And rnbTYear.Text = "") Then
            '                ShowMessage(Translate("Chưa nhập đủ thông tin giờ gian kết thúc"), NotifyType.Warning)
            '                Exit Sub
            '            End If

            '            objEmployeeTrain.EMPLOYEE_ID = EmployeeInfo.ID
            '            objEmployeeTrain.FROM_DATE = Date.Parse(rnbFYear.Text + "/" + rnbFMonth.Text + "/" + "01")
            '            If rnbTMonth.Text <> "" And rnbTYear.Text <> "" Then
            '                objEmployeeTrain.TO_DATE = Date.Parse(rnbTYear.Text + "/" + rnbTMonth.Text + "/" + "01")
            '            End If
            '            If objEmployeeTrain.FROM_DATE > objEmployeeTrain.TO_DATE Then
            '                ShowMessage(Translate("Ngày bắt đầu phải nhỏ hơn ngày kết thúc"), NotifyType.Warning)
            '                Exit Sub
            '            End If
            '            objEmployeeTrain.SCHOOL_NAME = txtTrainingSchool.Text
            '            If cboTrainingForm.SelectedValue <> "" And cboTrainingForm.SelectedValue <> "-1" Then
            '                objEmployeeTrain.TRAINING_FORM = Decimal.Parse(cboTrainingForm.SelectedValue)
            '            End If
            '            If cboMajor.SelectedValue <> "" And cboMajor.SelectedValue <> "-1" Then
            '                objEmployeeTrain.MAJOR = cboMajor.SelectedValue
            '            End If
            '            If rntGraduateYear.Value IsNot Nothing Then
            '                objEmployeeTrain.GRADUATE_YEAR = rntGraduateYear.Value
            '            End If
            '            If cboMark.SelectedValue <> "" And cboMark.SelectedValue <> "-1" Then
            '                objEmployeeTrain.MARK = cboMark.SelectedValue
            '            End If
            '            If cboLearningLevel.SelectedValue <> "" And cboLearningLevel.SelectedValue <> "-1" Then
            '                objEmployeeTrain.LEARNING_LEVEL = cboLearningLevel.SelectedValue
            '            End If
            '            objEmployeeTrain.TRAINING_CONTENT = rtbTrainingContent.Text

            '            Select Case CurrentState
            '                Case CommonMessage.STATE_NEW
            '                    Dim objValidate As New EmployeeTrainDTO
            '                    objValidate.EMPLOYEE_ID = EmployeeInfo.ID
            '                    If objEmployeeTrain.HIGHEST_LEVEL = -1 And rep.ValidateEmployeeTrain(objValidate) = True Then
            '                        ShowMessage(Translate("Bạn đã chọn một trình độ chuyên môn cao nhất trước đó"), Utilities.NotifyType.Warning)
            '                        Return
            '                    End If
            '                    If rep.InsertEmployeeTrain(objEmployeeTrain, gID) Then
            '                        CurrentState = CommonMessage.STATE_NORMAL
            '                        IDSelect = gID
            '                        Refresh("InsertView")
            '                        UpdateControlState()
            '                    Else
            '                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
            '                    End If
            '                Case CommonMessage.STATE_EDIT
            '                    objEmployeeTrain.ID = rgEmployeeTrain.SelectedValue

            '                    Dim objValidate As New EmployeeTrainDTO
            '                    objValidate.ID = rgEmployeeTrain.SelectedValue
            '                    objValidate.EMPLOYEE_ID = EmployeeInfo.ID
            '                    If objEmployeeTrain.HIGHEST_LEVEL = -1 And rep.ValidateEmployeeTrain(objValidate) = True Then
            '                        ShowMessage(Translate("Bạn đã chọn một trình độ chuyên môn cao nhất trước đó"), Utilities.NotifyType.Warning)
            '                        Return
            '                    End If

            '                    If rep.ModifyEmployeeTrain(objEmployeeTrain, gID) Then
            '                        CurrentState = CommonMessage.STATE_NORMAL
            '                        IDSelect = objEmployeeTrain.ID
            '                        Refresh("UpdateView")
            '                        UpdateControlState()
            '                    Else
            '                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
            '                    End If
            '            End Select
            '        Else
            '            ExcuteScript("Resize", "ResizeSplitter()")
            '        End If

            '    Case CommonMessage.TOOLBARITEM_CANCEL
            '        CurrentState = CommonMessage.STATE_NORMAL
            '        Refresh("Cancel")
            '        UpdateControlState()
            'End Select

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        If e.ButtonID = MessageBoxButtonType.ButtonYes Then
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE Then
                CurrentState = CommonMessage.STATE_DELETE
            End If
            UpdateControlState()
        End If
    End Sub

    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgEmployeeTrain.NeedDataSource
        Try
            CreateDataFilter()

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub RadGrid_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgEmployeeTrain.ItemDataBound
        Try
            'If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            '    Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
            '    If IDSelect <> 0 Then
            '        If IDSelect = datarow.GetDataKeyValue("ID") Then
            '            e.Item.Selected = True
            '        End If
            '    End If

            '    Dim a = CType(datarow.DataItem, EmployeeTrainForCompanyDTO)
            '    Dim strFromMonthYear As String = ""
            '    Dim strToMonthYear As String = ""
            '    Dim lblFromMonthYear As Label = CType(datarow.FindControl("lblFromMonthYear"), Label)
            '    Dim lblToMonthYear As Label = CType(datarow.FindControl("lblToMonthYear"), Label)

            '    strFromMonthYear = a.FMONTH.ToString + "/" + a.FYEAR.ToString
            '    strToMonthYear = a.TMONTH.ToString + "/" + a.TYEAR.ToString

            '    If lblFromMonthYear IsNot Nothing Then
            '        lblFromMonthYear.Text = strFromMonthYear
            '    End If
            '    If lblToMonthYear IsNot Nothing Then
            '        lblToMonthYear.Text = strToMonthYear
            '    End If
            'End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
#End Region

#Region "Custom"

#End Region
End Class