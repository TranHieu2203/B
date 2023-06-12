Imports Framework.UI
Imports Framework.UI.Utilities
Imports Recruitment.RecruitmentBusiness
Imports Common
Imports Telerik.Web.UI

Public Class ctrlRC_Required_Votes
    Inherits Common.CommonView
    Protected WithEvents ctrlFindOrgPopup As ctrlFindOrgPopup

#Region "Property"
    Public IDSelect As Integer
    Public Property RC_REQUIRED_VOTES_REG As List(Of RC_REQUIRED_REGDTO)
        Get
            Return ViewState(Me.ID & "_RC_REQUIRED_VOTES_REG")
        End Get
        Set(ByVal value As List(Of RC_REQUIRED_REGDTO))
            ViewState(Me.ID & "_RC_REQUIRED_VOTES_REG") = value
        End Set
    End Property
    Property ListComboData As ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ListComboData")
        End Get
        Set(ByVal value As ComboBoxDataDTO)
            ViewState(Me.ID & "_ListComboData") = value
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
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        SetGridFilter(rgDanhMuc)
        rgDanhMuc.AllowCustomPaging = True
        'rgCostCenter.ClientSettings.EnablePostBackOnRowClick = True
        InitControl()
    End Sub

    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarCostCenters
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create,
                                       ToolbarItem.Edit, ToolbarItem.Save,
                                       ToolbarItem.Cancel, ToolbarItem.Delete,
                                        ToolbarItem.Export, ToolbarItem.Print)

            CType(MainToolBar.Items(2), RadToolBarButton).CausesValidation = True
            CType(MainToolBar.Items(6), RadToolBarButton).Text = "In phiếu yêu cầu"
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            Refresh("")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New RecruitmentBusinessClient
        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgDanhMuc.Rebind()
                        SelectedItemDataGridByKey(rgDanhMuc, IDSelect, , rgDanhMuc.CurrentPageIndex)
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgDanhMuc.CurrentPageIndex = 0
                        rgDanhMuc.MasterTableView.SortExpressions.Clear()
                        rgDanhMuc.Rebind()
                        SelectedItemDataGridByKey(rgDanhMuc, IDSelect, )
                    Case "Cancel"
                        rgDanhMuc.MasterTableView.ClearSelectedItems()
                End Select
            End If
            UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim rep As New RecruitmentRepository
        Dim obj As New RC_REQUIRED_REGDTO
        Try
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgDanhMuc, obj)
            Dim Sorts As String = rgDanhMuc.MasterTableView.SortExpressions.GetSortString()
            Dim _param = New ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue),
                                           .IS_DISSOLVE = ctrlOrg.IsDissolve}
            If Not isFull Then
                If Sorts IsNot Nothing Then
                    Me.RC_REQUIRED_VOTES_REG = rep.GetRequiredReg(obj, rgDanhMuc.CurrentPageIndex, rgDanhMuc.PageSize, MaximumRows, _param, "CREATED_DATE desc")
                Else
                    Me.RC_REQUIRED_VOTES_REG = rep.GetRequiredReg(obj, rgDanhMuc.CurrentPageIndex, rgDanhMuc.PageSize, MaximumRows, _param)
                End If
                rgDanhMuc.VirtualItemCount = MaximumRows
                rgDanhMuc.DataSource = Me.RC_REQUIRED_VOTES_REG
            Else
                Return rep.GetRequiredReg(obj, 0, Integer.MaxValue, 0, _param).ToTable
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Overrides Sub UpdateControlState()
        Dim rep As New RecruitmentRepository
        Try
            If phFindOrg.Controls.Contains(ctrlFindOrgPopup) Then
                phFindOrg.Controls.Remove(ctrlFindOrgPopup)
                'Me.Views.Remove(ctrlFindOrgPopup.ID.ToUpper)
            End If
            Select Case isLoadPopup
                Case 2
                    ctrlFindOrgPopup = Me.Register("ctrlFindOrgPopup", "Common", "ctrlFindOrgPopup")
                    ctrlFindOrgPopup.OrganizationType = OrganizationType.OrganizationLocation
                    phFindOrg.Controls.Add(ctrlFindOrgPopup)
                    ctrlFindOrgPopup.Show()
            End Select

            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                    ClearControlValue(txtCode, txtOrgName, rdDateCreatVotes, rdDateAppVotes, rdEND_DATE_VOTES, cboACTFLG, txtNote)
                    EnableControlAll(True, btnFindOrg, rdDateCreatVotes, rdDateAppVotes, rdEND_DATE_VOTES, cboACTFLG, txtNote)
                    txtCode.Text = rep.AutoGenCode("PYC", "RC_REQUIRED_REG", "CODE")
                    EnabledGridNotPostback(rgDanhMuc, False)

                Case CommonMessage.STATE_NORMAL
                    ClearControlValue(txtCode, txtOrgName, rdDateCreatVotes, rdDateAppVotes, rdEND_DATE_VOTES, cboACTFLG, txtNote)
                    EnableControlAll(False, btnFindOrg, txtCode, rdDateCreatVotes, rdDateAppVotes, rdEND_DATE_VOTES, cboACTFLG, txtNote)

                    EnabledGridNotPostback(rgDanhMuc, True)

                Case CommonMessage.STATE_EDIT
                    EnableControlAll(True, btnFindOrg, rdDateCreatVotes, rdDateAppVotes, rdEND_DATE_VOTES, cboACTFLG, txtNote)
                    EnabledGridNotPostback(rgDanhMuc, False)

                Case CommonMessage.STATE_DELETE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgDanhMuc.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgDanhMuc.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.DeleteRequiredReg(lstDeletes) Then
                        Refresh("UpdateView")
                        UpdateControlState()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        UpdateControlState()
                    End If
            End Select
            txtCode.Focus()
            UpdateToolbarState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub BindData()
        getDataCombobox()
        Dim dic As New Dictionary(Of String, Control)
        dic.Add("CODE", txtCode)
        dic.Add("ORG_NAME", txtOrgName)
        dic.Add("DATE_CREATE_VOTES", rdDateCreatVotes)
        dic.Add("DATE_APP_VOTES", rdDateAppVotes)
        dic.Add("END_DATE_VOTES", rdEND_DATE_VOTES)
        dic.Add("ACTFLG", cboACTFLG)
        dic.Add("NOTE", txtNote)
        Utilities.OnClientRowSelectedChanged(rgDanhMuc, dic)
    End Sub

   


#End Region

#Region "Event"
    Private Sub ctrlOrg_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged
        Try
            rgDanhMuc.CurrentPageIndex = 0
            rgDanhMuc.MasterTableView.SortExpressions.Clear()
            rgDanhMuc.Rebind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objShift As New RC_REQUIRED_REGDTO
        Dim rep As New RecruitmentRepository
        Dim gID As Decimal
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_EDIT
                    If rgDanhMuc.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    CurrentState = CommonMessage.STATE_EDIT
                    UpdateControlState()

                Case CommonMessage.TOOLBARITEM_DELETE
                    If rgDanhMuc.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    'Dim lstID As New List(Of Decimal)
                    'For idx = 0 To rgDanhMuc.SelectedItems.Count - 1
                    '    Dim item As GridDataItem = rgDanhMuc.SelectedItems(idx)
                    '    lstID.Add(Decimal.Parse(item("ID").Text))
                    'Next
                    'If Not rep.CheckExistInDatabase(lstID, AttendanceCommonTABLE_NAME.AT_SHIFT) Then
                    '    ShowMessage(Translate(CommonMessage.MESSAGE_IS_USING), NotifyType.Warning)
                    '    Return
                    'End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case CommonMessage.TOOLBARITEM_SAVE

                    If Page.IsValid Then
                        objShift.CODE = txtCode.Text
                        objShift.ORG_ID = hidOrgID.Value
                        objShift.DATE_CREATE_VOTES = rdDateCreatVotes.SelectedDate
                        objShift.DATE_APP_VOTES = rdDateAppVotes.SelectedDate
                        objShift.END_DATE_VOTES = rdEND_DATE_VOTES.SelectedDate
                        If cboACTFLG.SelectedValue <> "" Then
                            objShift.ACTFLG = cboACTFLG.SelectedValue
                        End If
                        objShift.NOTE = txtNote.Text
                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                If rep.InsertRequiredReg(objShift, gID) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = gID
                                    Refresh("InsertView")
                                    UpdateControlState()
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT
                                objShift.ID = rgDanhMuc.SelectedValue
                                If rep.ModifyRequiredReg(objShift, rgDanhMuc.SelectedValue) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = objShift.ID
                                    Refresh("UpdateView")
                                    UpdateControlState()
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select
                    Else
                        ExcuteScript("Resize", "ResizeSplitter()")
                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    Refresh("Cancel")
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Using xls As New ExcelCommon
                        Dim dtDatas As DataTable
                        dtDatas = CreateDataFilter(True)
                        If dtDatas.Rows.Count > 0 Then
                            rgDanhMuc.ExportExcel(Server, Response, dtDatas, "CaLamViec")
                        End If
                    End Using
            End Select
            CreateDataFilter()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        If e.ActionName = CommonMessage.TOOLBARITEM_ACTIVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
            CurrentState = CommonMessage.STATE_ACTIVE
            UpdateControlState()
        End If
        If e.ActionName = CommonMessage.TOOLBARITEM_DEACTIVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
            CurrentState = CommonMessage.STATE_DEACTIVE
            UpdateControlState()
        End If
        If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
            CurrentState = CommonMessage.STATE_DELETE
            UpdateControlState()
        End If
    End Sub

    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgDanhMuc.NeedDataSource
        Try
            CreateDataFilter()
            'rgDanhMuc.DataSource = GetTable()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub btnFindOrg_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFindOrg.Click
        Try
            isLoadPopup = 2
            UpdateControlState()
            ctrlFindOrgPopup.Show()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub ctrlFindOrgPopup_OrganizationSelected(ByVal sender As Object, ByVal e As Common.OrganizationSelectedEventArgs) Handles ctrlFindOrgPopup.OrganizationSelected
        Try
            Dim orgItem = ctrlFindOrgPopup.CurrentItemDataObject
            If orgItem IsNot Nothing Then
                hidOrgID.Value = e.CurrentValue 'gán org đã chọn vào hiddenfield
                txtOrgName.Text = orgItem.NAME_VN
                txtOrgName.ToolTip = Utilities.DrawTreeByString(orgItem.DESCRIPTION_PATH)
            End If
            isLoadPopup = 0
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlFindPopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindOrgPopup.CancelClicked
        isLoadPopup = 0
    End Sub

    Private Sub RadGrid_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgDanhMuc.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
            datarow("ORG_NAME").ToolTip = Utilities.DrawTreeByString(datarow.GetDataKeyValue("ORG_DESC"))
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

    Private Sub getDataCombobox()
        Try
            Dim dtData As New DataTable
            Dim rep As New RecruitmentRepository
            dtData = rep.GetOtherList("RC_PROGRAM_STATUS", True)

            cboACTFLG.DataSource = dtData
            cboACTFLG.DataTextField = "NAME"
            cboACTFLG.DataValueField = "ID"
            cboACTFLG.DataBind()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

  
  
End Class