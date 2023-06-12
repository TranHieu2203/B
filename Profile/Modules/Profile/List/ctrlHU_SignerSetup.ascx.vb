Imports System.IO
Imports Aspose.Cells
Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlHU_SignerSetup
    Inherits Common.CommonView

    ''' <creator>HongDX</creator>
    ''' <lastupdate>21/06/2017</lastupdate>
    ''' <summary>Write Log</summary>
    ''' <remarks>_mylog, _pathLog, _classPath</remarks>
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Profile\Modules\Profile\List" + Me.GetType().Name.ToString()
    ''' <summary>''' ctrl FindEmployeePopup
    ''' </summary>
    ''' <remarks></remarks>
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Protected WithEvents ctrlFindOrgPopup As ctrlFindOrgPopup

#Region "Property"

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

    Public Property List_Oganization_ID As List(Of Decimal)
        Set(ByVal value As List(Of Decimal))
            PageViewState(Me.ID & "_List_Oganization_ID") = value
        End Set
        Get
            Return PageViewState(Me.ID & "_List_Oganization_ID")
        End Get
    End Property

    ''' <summary>
    ''' isLoadPopup
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>
    ''' 1 - Employee
    ''' </remarks>
    Property isLoadPopup As Integer
        Get
            Return ViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isLoadPopup") = value
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


    Property dtFunction As DataTable
        Get
            Return ViewState(Me.ID & "_dtFunction")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtFunction") = value
        End Set
    End Property


    Property dtData As DataTable
        Get
            If ViewState(Me.ID & "_dtData") Is Nothing Then
                Dim dt As New DataTable("DATA")
                dt.Columns.Add("FUNC_NAME", GetType(String))
                dt.Columns.Add("FUNC_ID", GetType(String))
                dt.Columns.Add("SETUP_TYPE_NAME", GetType(String))
                dt.Columns.Add("SETUP_TYPE", GetType(String))
                dt.Columns.Add("ORG_CODE", GetType(String))
                dt.Columns.Add("ORG_NAME", GetType(String))
                dt.Columns.Add("COST_CENTER_CODE", GetType(String))
                dt.Columns.Add("ORG_ID", GetType(String))
                dt.Columns.Add("EFFECT_DATE", GetType(String))
                dt.Columns.Add("EMPLOYEE_CODE", GetType(String))
                dt.Columns.Add("SIGNER_ID", GetType(String))
                dt.Columns.Add("BASE_AUTHOR", GetType(String))
                dt.Columns.Add("AUTHOR_EFFECT_DATE", GetType(String))
                dt.Columns.Add("DEPUTY_AUTHOR", GetType(String))
                dt.Columns.Add("CER_BUS_RESG", GetType(String))
                dt.Columns.Add("CER_BUS_RESG_EFFECT_DATE", GetType(String))
                ViewState(Me.ID & "_dtData") = dt
            End If
            Return ViewState(Me.ID & "_dtData")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtData") = value
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
            Refresh()
            UpdateControlState()
            rgMain.SetFilter()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
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
            rgMain.AllowCustomPaging = True
            InitControl()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary>Khoi tao, load menu toolbar</summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMain

            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create,
                                       ToolbarItem.Edit, ToolbarItem.Save, ToolbarItem.Cancel,
                                       ToolbarItem.Delete, ToolbarItem.Export, ToolbarItem.ExportTemplate, ToolbarItem.Import)

            CType(MainToolBar.Items(2), RadToolBarButton).CausesValidation = True
            CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = False
            CType(Me.MainToolBar.Items(4), RadToolBarButton).Enabled = False
            CType(Me.MainToolBar.Items(6), RadToolBarButton).Text = "Xuất file mẫu"
            CType(Me.MainToolBar.Items(7), RadToolBarButton).Text = "Nhập file mẫu"
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary>Khoi tao, load menu toolbar</summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New ProfileRepository

        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgMain.Rebind()
                        ClearControlValue(rdEffectDate, hidSinger, hidJoinDate, hidOrgID, txtOrgName, txtEmployeeName, txtTitle, txtRemark, cboSetupType, txtAuthorBase, txtAuthorDeputy, rdAuthorEffectDate, rdNgayCap, txtKdNo, txtBase1, txtBase2, txtBase3, txtBase4, txtBase5, txtBase6)
                        cboFunction.ClearSelection()
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgMain.CurrentPageIndex = 0
                        rgMain.MasterTableView.SortExpressions.Clear()
                        rgMain.Rebind()
                        ClearControlValue(rdEffectDate, hidSinger, hidOrgID, hidJoinDate, txtOrgName, txtEmployeeName, txtTitle, txtRemark, cboSetupType, txtAuthorBase, txtAuthorDeputy, rdAuthorEffectDate, rdNgayCap, txtKdNo, txtBase1, txtBase2, txtBase3, txtBase4, txtBase5, txtBase6)
                        cboFunction.ClearSelection()
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "Cancel"
                        rgMain.MasterTableView.ClearSelectedItems()
                        ClearControlValue(rdEffectDate, hidSinger, hidJoinDate, hidOrgID, txtOrgName, txtEmployeeName, txtTitle, txtRemark, cboSetupType, txtAuthorBase, txtAuthorDeputy, rdAuthorEffectDate, rdNgayCap, txtKdNo, txtBase1, txtBase2, txtBase3, txtBase4, txtBase5, txtBase6)
                        cboFunction.ClearSelection()
                End Select
            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary>  Load data len grid </summary>
    ''' <param name="isFull"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        CreateDataFilter = Nothing
        Dim rep As New ProfileRepository
        Dim _filter As New SignerSetupDTO
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgMain, _filter)

            Dim Sorts As String = rgMain.MasterTableView.SortExpressions.GetSortString()
            Dim _param = New ParamDTO With {.ORG_ID = 1,
                                            .IS_DISSOLVE = False}
            If isFull Then
                If Sorts IsNot Nothing Then
                    Return rep.GetListSignerSetup(_filter, _param, Sorts).ToTable
                Else
                    Return rep.GetListSignerSetup(_filter, _param).ToTable
                End If
            Else
                Dim lst As New List(Of SignerSetupDTO)
                If Sorts IsNot Nothing Then
                    lst = rep.GetListSignerSetup(_filter, rgMain.CurrentPageIndex, rgMain.PageSize, MaximumRows, _param, Sorts)
                Else
                    lst = rep.GetListSignerSetup(_filter, rgMain.CurrentPageIndex, rgMain.PageSize, MaximumRows, _param)
                End If
                rgMain.VirtualItemCount = MaximumRows
                rgMain.DataSource = lst
            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Function


    Public Overrides Sub UpdateControlState()
        Dim rep As New ProfileRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            Select Case isLoadPopup
                Case 1
                    If Not phFindEmp.Controls.Contains(ctrlFindEmployeePopup) Then
                        ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                        phFindEmp.Controls.Add(ctrlFindEmployeePopup)
                        ctrlFindEmployeePopup.MultiSelect = False
                        ctrlFindEmployeePopup.LoadAllOrganization = False
                        ctrlFindEmployeePopup.MustHaveContract = False
                    End If
                Case 2
                    ctrlFindOrgPopup = Me.Register("ctrlFindOrgPopup", "Common", "ctrlFindOrgPopup")
                    ctrlFindOrgPopup.OrganizationType = OrganizationType.OrganizationLocation
                    If List_Oganization_ID IsNot Nothing AndAlso List_Oganization_ID.Count > 0 Then
                        ctrlFindOrgPopup.Bind_Find_ValueKeys = List_Oganization_ID

                    End If
                    ctrlFindOrgPopup.IS_HadLoad = False
                    phFindOrg.Controls.Add(ctrlFindOrgPopup)
            End Select

            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                    EnabledGridNotPostback(rgMain, False)
                    cboFunction.AutoPostBack = True
                    EnableControlAll(True, rdEffectDate, hidSinger, hidOrgID, txtRemark, cboSetupType, cboFunction, btnFindEmployee, btnFindOrg, txtAuthorBase, txtAuthorDeputy, rdAuthorEffectDate, rdNgayCap, txtKdNo, txtBase1, txtBase2, txtBase3, txtBase4, txtBase5, txtBase6)
                Case CommonMessage.STATE_NORMAL
                    EnabledGridNotPostback(rgMain, True)
                    cboFunction.AutoPostBack = False
                    EnableControlAll(False, rdEffectDate, hidSinger, txtTitle, hidOrgID, txtEmployeeName, txtRemark, cboSetupType, cboFunction, btnFindEmployee, btnFindOrg, txtAuthorBase, txtAuthorDeputy, rdAuthorEffectDate, rdNgayCap, txtKdNo, txtBase1, txtBase2, txtBase3, txtBase4, txtBase5, txtBase6)
                Case CommonMessage.STATE_EDIT
                    EnabledGridNotPostback(rgMain, False)
                    cboFunction.AutoPostBack = True
                    EnableControlAll(True, rdEffectDate, hidSinger, hidOrgID, txtRemark, cboSetupType, cboFunction, btnFindEmployee, btnFindOrg, txtAuthorBase, txtAuthorDeputy, rdAuthorEffectDate, rdNgayCap, txtKdNo, txtBase1, txtBase2, txtBase3, txtBase4, txtBase5, txtBase6)
                Case CommonMessage.STATE_DELETE
                    Dim lstDeletes As New List(Of Decimal)

                    For idx = 0 To rgMain.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgMain.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next

                    If rep.DeleteSignerSetup(lstDeletes) Then
                        Refresh("UpdateView")
                        UpdateControlState()
                        rgMain.Rebind()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_IS_EFFECT_NOT_DELETE), NotifyType.Error)
                        UpdateControlState()
                    End If

                Case CommonMessage.STATE_ACTIVE
                    Dim lstDeletes As New List(Of Decimal)

                    For idx = 0 To rgMain.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgMain.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next

                    If rep.ActiveSignerSetup(lstDeletes, "A") Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                        rgMain.Rebind()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                    End If

                Case CommonMessage.STATE_DEACTIVE
                    Dim lstDeletes As New List(Of Decimal)

                    For idx = 0 To rgMain.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgMain.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next

                    If rep.ActiveSignerSetup(lstDeletes, "I") Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                        rgMain.Rebind()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                    End If
            End Select
            rep.Dispose()
            UpdateToolbarState()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary>Fill du lieu len combobox</summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim repS As New ProfileStoreProcedure
        Try
            dtFunction = repS.GET_FUNCTION_SIGN(True)
            FillRadCombobox(cboFunction, dtFunction, "NAME", "ID")

            Dim dic As New Dictionary(Of String, Control)
            dic.Add("EFFECT_DATE", rdEffectDate)
            dic.Add("SIGNER_NAME", txtEmployeeName)
            dic.Add("SIGNER_TITLE_NAME", txtTitle)
            dic.Add("REMARK", txtRemark)
            dic.Add("SIGNER_ID", hidSinger)
            dic.Add("FUNC_ID", cboFunction)
            dic.Add("BASE_AUTHOR", txtAuthorBase)
            dic.Add("BASE1", txtBase1)
            dic.Add("BASE2", txtBase2)
            dic.Add("BASE3", txtBase3)
            dic.Add("BASE4", txtBase4)
            dic.Add("BASE5", txtBase5)
            dic.Add("BASE6", txtBase6)
            dic.Add("AUTHOR_EFFECT_DATE", rdAuthorEffectDate)
            dic.Add("DEPUTY_AUTHOR", txtAuthorDeputy)
            dic.Add("ORG_ID", hidOrgID)
            dic.Add("JOIN_DATE", hidJoinDate)
            dic.Add("ORG_NAME", txtOrgName)
            dic.Add("CER_BUS_RESG", txtKdNo)
            dic.Add("CER_BUS_RESG_EFFECT_DATE", rdNgayCap)
            Utilities.OnClientRowSelectedChanged(rgMain, dic)

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
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
        Dim objSigner As New SignerSetupDTO
        Dim rep As New ProfileRepository
        Dim gID As Decimal
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim commonRes As New CommonRepository

        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    ClearControlValue(rdEffectDate, hidSinger, hidJoinDate, hidOrgID, txtOrgName, txtEmployeeName, txtTitle, txtRemark, cboSetupType, txtAuthorBase, txtAuthorDeputy, rdAuthorEffectDate, rdNgayCap, txtKdNo)
                    cboFunction.ClearSelection()

                    UpdateControlState()

                Case CommonMessage.TOOLBARITEM_EDIT
                    If rgMain.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    If rgMain.SelectedItems.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    Dim item As GridDataItem = rgMain.SelectedItems(0)
                    If IsNumeric(item.GetDataKeyValue("SETUP_TYPE_ID")) Then
                        cboSetupType.FindItemByValue(item.GetDataKeyValue("SETUP_TYPE_ID")).Checked = True
                    End If
                    CurrentState = CommonMessage.STATE_EDIT
                    UpdateControlState()

                Case CommonMessage.TOOLBARITEM_DELETE
                    If rgMain.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgMain.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgMain.SelectedItems(idx)
                        lstDeletes.Add(Decimal.Parse(item.GetDataKeyValue("ID")))
                    Next

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                    ClearControlValue(rdEffectDate, hidSinger, hidOrgID, txtOrgName, txtEmployeeName, txtTitle, txtRemark, cboSetupType, hidJoinDate)
                    cboFunction.ClearSelection()

                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        dtData = CreateDataFilter(True)
                        If dtData.Rows.Count > 0 Then
                            rgMain.ExportExcel(Server, Response, dtData, "Signer")
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                        End If
                    End Using

                Case CommonMessage.TOOLBARITEM_ACTIVE
                    If rgMain.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_ACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_ACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case CommonMessage.TOOLBARITEM_DEACTIVE
                    If rgMain.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DEACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_DEACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case CommonMessage.TOOLBARITEM_EXPORT_TEMPLATE
                    Template_Import_Nguoiky()
                Case CommonMessage.TOOLBARITEM_IMPORT
                    ctrlUpload1.Show()
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        If IsDate(rdEffectDate.SelectedDate) Then
                            objSigner.EFFECT_DATE = rdEffectDate.SelectedDate
                        End If
                        If CDate(hidJoinDate.Value) > rdEffectDate.SelectedDate Then
                            ShowMessage(Translate("Ngày hiệu lực phải lớn hơn hoặc bằng ngày vào làm của người ký"), Utilities.NotifyType.Error)
                            Exit Sub
                        End If
                        If IsNumeric(hidSinger.Value) Then
                            objSigner.SIGNER_ID = hidSinger.Value
                        End If
                        If cboFunction.SelectedValue <> "" Then
                            objSigner.FUNC_ID = cboFunction.SelectedValue
                        End If
                        objSigner.REMARK = txtRemark.Text.Trim
                        objSigner.SIGNER_TITLE_NAME = txtTitle.Text.Trim
                        If CurrentState = CommonMessage.STATE_EDIT Then
                            objSigner.ID = rgMain.SelectedValue
                        Else
                            objSigner.ID = 0
                        End If
                        If IsDate(rdAuthorEffectDate.SelectedDate) Then
                            objSigner.AUTHOR_EFFECT_DATE = rdAuthorEffectDate.SelectedDate
                        End If
                        If IsDate(rdNgayCap.SelectedDate) Then
                            objSigner.CER_BUS_RESG_EFFECT_DATE = rdNgayCap.SelectedDate
                        End If
                        objSigner.CER_BUS_RESG = txtKdNo.Text
                        objSigner.BASE_AUTHOR = txtAuthorBase.Text.Trim
                        objSigner.DEPUTY_AUTHOR = txtAuthorDeputy.Text.Trim
                        objSigner.BASE1 = txtBase1.Text
                        objSigner.BASE2 = txtBase2.Text
                        objSigner.BASE3 = txtBase3.Text
                        objSigner.BASE4 = txtBase4.Text
                        objSigner.BASE5 = txtBase5.Text
                        objSigner.BASE6 = txtBase6.Text
                        If IsNumeric(hidOrgID.Value) Then
                            objSigner.ORG_ID = hidOrgID.Value
                        End If
                        Dim lst_type As New List(Of Decimal)
                        For Each item In cboSetupType.CheckedItems
                            objSigner.SETUP_TYPE_ID = item.Value
                            If Not rep.ValidateSignSet(objSigner) Then
                                ShowMessage(Translate("Dữ liệu đã tồn tại"), Utilities.NotifyType.Error)
                                Exit Sub
                            End If
                            lst_type.Add(item.Value)
                        Next
                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                objSigner.ACTFLG = "A"
                                If rep.InsertSignerSetup(objSigner, lst_type) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    Refresh("InsertView")
                                    UpdateControlState()
                                    rgMain.Rebind()
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If

                            Case CommonMessage.STATE_EDIT
                                If rep.ModifySignerSetup(objSigner, lst_type) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    Refresh("UpdateView")
                                    UpdateControlState()
                                    rgMain.Rebind()
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select

                    Else
                        ExcuteScript("Resize", "ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgMain')")
                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    ClearControlValue(rdEffectDate, hidSinger, hidOrgID, txtOrgName, txtEmployeeName, txtTitle, hidJoinDate, txtRemark, cboSetupType, txtAuthorBase, txtAuthorDeputy, rdAuthorEffectDate, rdNgayCap, txtKdNo)
                    cboFunction.ClearSelection()
                    CurrentState = CommonMessage.STATE_NORMAL
                    Refresh("Cancel")
                    UpdateControlState()
            End Select
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary> Event xử lý sự kiện khi ấn button trên MsgBox </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_ACTIVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_ACTIVE
                UpdateControlState()
                ClearControlValue(rdEffectDate, hidSinger, hidJoinDate, hidOrgID, txtOrgName, txtEmployeeName, txtTitle, txtRemark, cboSetupType)
                cboFunction.ClearSelection()
            End If

            If e.ActionName = CommonMessage.TOOLBARITEM_DEACTIVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DEACTIVE
                UpdateControlState()
                ClearControlValue(rdEffectDate, hidSinger, hidJoinDate, hidOrgID, txtOrgName, txtEmployeeName, txtTitle, txtRemark, cboSetupType)
                cboFunction.ClearSelection()
            End If

            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DELETE
                UpdateControlState()
                ClearControlValue(rdEffectDate, hidSinger, hidJoinDate, hidOrgID, txtOrgName, txtEmployeeName, txtTitle, txtRemark, cboSetupType)
                cboFunction.ClearSelection()
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary>Load datasource cho grid</summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgMain.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            CreateDataFilter()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Protected Sub btnFindCommon_Click(ByVal sender As Object,
                                        ByVal e As EventArgs) Handles _
                                        btnFindEmployee.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Select Case sender.ID
                Case btnFindEmployee.ID
                    isLoadPopup = 1
            End Select

            UpdateControlState()

            Select Case sender.ID
                Case btnFindEmployee.ID
                    ctrlFindEmployeePopup.Show()
            End Select

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub ctrlFind_CancelClick(ByVal sender As Object,
                                     ByVal e As System.EventArgs) Handles _
                                 ctrlFindEmployeePopup.CancelClicked, ctrlFindOrgPopup.CancelClicked
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            isLoadPopup = 0
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim empID = ctrlFindEmployeePopup.SelectedEmployeeID(0)

            Using rep As New ProfileBusinessRepository
                Dim obj = rep.GetEmployeeByID(empID)

                If obj.WORK_STATUS = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID Then
                    ShowMessage(Translate("Nhân viên trạng thái nghỉ việc. Không được phép chỉnh sửa thông tin."), Utilities.NotifyType.Warning)
                    Exit Sub
                End If
                hidJoinDate.Value = obj.JOIN_DATE
                hidSinger.Value = obj.ID
                txtEmployeeName.Text = obj.FULLNAME_VN
                txtTitle.Text = obj.TITLE_NAME_VN
            End Using
            isLoadPopup = 0

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub cboFunction_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles cboFunction.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New ProfileRepository
        Dim store As New ProfileStoreProcedure
        ListComboData = New ComboBoxDataDTO
        Try
            cboSetupType.Items.Clear()
            If cboFunction.SelectedValue <> "" Then
                Dim code = (From p In dtFunction Where p("ID") = cboFunction.SelectedValue Select p("CODE")).FirstOrDefault.ToString
                If code.ToUpper.Equals("CTRLHU_CONTRACTNEWEDIT") Then
                    ListComboData.GET_CONTRACTTYPE = True
                    rep.GetComboList(ListComboData)
                    FillRadCombobox(cboSetupType, ListComboData.LIST_CONTRACTTYPE, "NAME", "ID")
                    'ElseIf code.ToUpper.Equals("CTRLHU_CONTRACTTEMPLETE") Then
                    '    Dim lstPLHD As New List(Of ContractTypeDTO)
                    '    lstPLHD = rep.GetListContractType("PLHD")
                    '    FillRadCombobox(cboSetupType, lstPLHD, "NAME", "ID")
                ElseIf code.ToUpper.Equals("CTRLHU_TERMINATENEWEDIT") Then
                    ListComboData.GET_TER_DECISION_TYPE = True
                    rep.GetComboList(ListComboData)
                    FillRadCombobox(cboSetupType, ListComboData.LIST_TER_DECISION_TYPE, "NAME_VN", "ID")
                ElseIf code.ToUpper.Equals("CTRLHU_COMMENDNEWEDIT") Then
                    ListComboData.GET_COMMEND_OBJ = True
                    rep.GetComboList(ListComboData)
                    FillRadCombobox(cboSetupType, ListComboData.LIST_COMMEND_OBJ, "NAME_VN", "ID")
                ElseIf code.ToUpper.Equals("CTRLHU_DISCIPLINENEWEDIT") Then
                    ListComboData.GET_DISCIPLINE_OBJ = True
                    rep.GetComboList(ListComboData)
                    FillRadCombobox(cboSetupType, ListComboData.LIST_DISCIPLINE_OBJ, "NAME_VN", "ID")
                ElseIf code.ToUpper.Equals("CTRLHU_CHANGEINFONEWEDIT") OrElse code.ToUpper.Equals("CTRLHU_APPROVEMULTICHANGEINFO_NEW") Then
                    Dim dtData As DataTable
                    dtData = store.GET_DECISION_TYPE_EXCEPT_NV()
                    FillRadCombobox(cboSetupType, dtData, "NAME", "ID")
                End If
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Protected Sub btnFindOrg_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFindOrg.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            isLoadPopup = 2
            If sender IsNot Nothing Then
                List_Oganization_ID = New List(Of Decimal)
            End If
            UpdateControlState()
            ctrlFindOrgPopup.Show()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub ctrlFindOrgPopup_OrganizationSelected(ByVal sender As Object, ByVal e As Common.OrganizationSelectedEventArgs) Handles ctrlFindOrgPopup.OrganizationSelected
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim orgItem = ctrlFindOrgPopup.CurrentItemDataObject
            If orgItem IsNot Nothing Then
                hidOrgID.Value = e.CurrentValue
                txtOrgName.Text = orgItem.NAME_VN
                If orgItem.DESCRIPTION_PATH IsNot Nothing AndAlso orgItem.DESCRIPTION_PATH <> "" Then
                    txtOrgName.ToolTip = Utilities.DrawTreeByString(orgItem.DESCRIPTION_PATH)
                End If
            End If
            isLoadPopup = 0
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub rgMain_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgMain.ItemDataBound
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
                Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
                datarow("ORG_NAME").ToolTip = Utilities.DrawTreeByString(datarow.GetDataKeyValue("ORG_DESC"))
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub ctrlUpload1_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload1.OkClicked
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim fileName As String
        Dim dsDataPrepare As New DataSet
        Dim workbook As Aspose.Cells.Workbook
        Dim worksheet As Aspose.Cells.Worksheet
        Try
            Dim tempPath As String = ConfigurationManager.AppSettings("ExcelFileFolder")
            Dim savepath = Context.Server.MapPath(tempPath)

            For Each file As UploadedFile In ctrlUpload1.UploadedFiles
                fileName = System.IO.Path.Combine(savepath, Guid.NewGuid().ToString() & ".xls")
                file.SaveAs(fileName, True)
                workbook = New Aspose.Cells.Workbook(fileName)
                worksheet = workbook.Worksheets(0)
                dsDataPrepare.Tables.Add(worksheet.Cells.ExportDataTableAsString(0, 0, worksheet.Cells.MaxRow + 1, worksheet.Cells.MaxColumn + 1, True))
                If System.IO.File.Exists(fileName) Then System.IO.File.Delete(fileName)
            Next
            dtData = dtData.Clone()
            TableMapping(dsDataPrepare.Tables(0))
            For Each rows As DataRow In dsDataPrepare.Tables(0).Select("FUNC_ID<>'""'").CopyToDataTable.Rows
                If String.IsNullOrEmpty(rows("FUNC_ID").ToString) AndAlso String.IsNullOrEmpty(rows("EMPLOYEE_CODE").ToString) Then Continue For
                Dim newRow As DataRow = dtData.NewRow
                newRow("FUNC_NAME") = rows("FUNC_NAME").ToString
                newRow("FUNC_ID") = rows("FUNC_ID")
                newRow("SETUP_TYPE_NAME") = rows("SETUP_TYPE_NAME").ToString
                newRow("SETUP_TYPE") = rows("SETUP_TYPE")
                newRow("ORG_CODE") = rows("ORG_CODE").ToString
                newRow("ORG_NAME") = rows("ORG_NAME").ToString
                newRow("COST_CENTER_CODE") = rows("COST_CENTER_CODE").ToString
                newRow("ORG_ID") = rows("ORG_ID")
                newRow("EFFECT_DATE") = rows("EFFECT_DATE").ToString
                newRow("EMPLOYEE_CODE") = rows("EMPLOYEE_CODE").ToString
                newRow("BASE_AUTHOR") = rows("BASE_AUTHOR").ToString
                newRow("AUTHOR_EFFECT_DATE") = rows("AUTHOR_EFFECT_DATE").ToString
                newRow("DEPUTY_AUTHOR") = rows("DEPUTY_AUTHOR").ToString
                newRow("CER_BUS_RESG") = rows("CER_BUS_RESG").ToString
                newRow("CER_BUS_RESG_EFFECT_DATE") = rows("CER_BUS_RESG_EFFECT_DATE").ToString
                dtData.Rows.Add(newRow)
            Next
            dtData.TableName = "DATA"
            If loadToGrid() Then
                Dim sw As New StringWriter()
                Dim DocXml As String = String.Empty
                dtData.WriteXml(sw, False)
                DocXml = sw.ToString
                Dim sp As New ProfileStoreProcedure
                If sp.IMPORT_SIGN_SETUP(DocXml, LogHelper.GetUserLog().Username.ToUpper) Then
                    ShowMessage(Translate("Import thành công"), NotifyType.Success)
                Else
                    ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
                End If
                rgMain.Rebind()
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub rgMain_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles rgMain.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim store As New ProfileStoreProcedure
        Try
            If rgMain.SelectedItems.Count > 0 Then
                Dim item As GridDataItem
                item = rgMain.SelectedItems(0)
                If item.GetDataKeyValue("ORG_DESC") IsNot Nothing Then
                    txtOrgName.ToolTip = Utilities.DrawTreeByString(item.GetDataKeyValue("ORG_DESC"))
                Else
                    txtOrgName.ToolTip = ""
                End If
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Custom"
    Private Sub Template_Import_Nguoiky()
        Dim store As New ProfileStoreProcedure
        Try
            Dim configPath As String = ConfigurationManager.AppSettings("PathImportFolder")
            Dim dsData As DataSet = store.EXPORT_SIGN_SETUP_DATA
            dsData.Tables(0).TableName = "Table"
            dsData.Tables(1).TableName = "Table1"
            dsData.Tables(2).TableName = "Table2"

            ExportTemplate("Profile\Import\Template_Import_NguoiKy.xls",
                                   dsData, Nothing, "Template_Import_NguoiKy" & Format(Date.Now, "yyyymmdd"))

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function ExportTemplate(ByVal sReportFileName As String,
                                                    ByVal dsData As DataSet,
                                                    ByVal dtVariable As DataTable,
                                                    ByVal filename As String) As Boolean

        Dim filePath As String
        Dim templatefolder As String

        Dim designer As WorkbookDesigner
        Try

            templatefolder = ConfigurationManager.AppSettings("ReportTemplatesFolder")
            filePath = AppDomain.CurrentDomain.BaseDirectory & templatefolder & "\" & sReportFileName

            'cau hinh lai duong dan tren server
            'filePath = sReportFileName

            If Not File.Exists(filePath) Then
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "javascriptfunction", "goBack()", True)
                Return False
            End If

            designer = New WorkbookDesigner
            designer.Open(filePath)
            designer.SetDataSource(dsData)

            If dtVariable IsNot Nothing Then
                Dim intCols As Integer = dtVariable.Columns.Count
                For i As Integer = 0 To intCols - 1
                    designer.SetDataSource(dtVariable.Columns(i).ColumnName.ToString(), dtVariable.Rows(0).ItemArray(i).ToString())
                Next
            End If
            designer.Process()
            designer.Workbook.CalculateFormula()
            designer.Workbook.Save(HttpContext.Current.Response, filename & ".xls", ContentDisposition.Attachment, New XlsSaveOptions())

        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary>Cập nhật trạng thái cho Toolbar</summary>
    ''' <remarks></remarks>
    Private Sub UpdateToolbarState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            ChangeToolbarState()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub TableMapping(ByVal dtdata As DataTable)
        Dim row As DataRow = dtdata.Rows(0)
        Dim index As Integer = 0
        For Each cols As DataColumn In dtdata.Columns
            Try
                cols.ColumnName = row(index)
                index += 1
                If index > row.ItemArray.Length - 1 Then Exit For
            Catch ex As Exception
                Exit For
            End Try
        Next
        dtdata.Rows(0).Delete()
        dtdata.AcceptChanges()
    End Sub

    Function loadToGrid() As Boolean
        Dim dtError As New DataTable("ERROR")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim dtEmpTable As New DataTable
        Dim rep As New ProfileBusinessRepository
        Dim rep2 As New ProfileRepository
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If dtData.Rows.Count = 0 Then
                ShowMessage(Translate(CommonMessage.MESSAGE_NOT_ROW), NotifyType.Warning)
                Return False
            End If
            Dim rowError As DataRow
            Dim isError As Boolean = False
            Dim sError As String = String.Empty
            Dim lstEmp As New List(Of String)
            dtError = dtData.Clone
            dtError.Columns.Add("OTHER")
            Dim iRow = 5
            Dim irowEm = 5
            For Each row As DataRow In dtData.Rows
                rowError = dtError.NewRow
                isError = False
                sError = "Chưa chọn chức năng"
                ImportValidate.EmptyValue("FUNC_NAME", row, rowError, isError, sError)

                sError = "Chưa nhập mã nhân viên"
                ImportValidate.EmptyValue("EMPLOYEE_CODE", row, rowError, isError, sError)

                Dim empId = rep.CheckEmployee_Exits(row("EMPLOYEE_CODE"))
                If empId = 0 Then
                    sError = "Mã nhân viên không tồn tại"
                    ImportValidate.IsValidTime("EMPLOYEE_CODE", row, rowError, isError, sError)
                Else
                    row("SIGNER_ID") = empId
                End If

                If String.IsNullOrEmpty(row("EFFECT_DATE")) Then
                    sError = "Chưa nhập ngày bắt đầu"
                    ImportValidate.EmptyValue("EFFECT_DATE", row, rowError, isError, sError)
                Else
                    If Not IsDate(row("EFFECT_DATE")) Then
                        sError = "Ngày bắt đầu không đúng định dạng"
                        ImportValidate.IsValidTime("EFFECT_DATE", row, rowError, isError, sError)
                    Else
                        If empId <> 0 Then
                            Dim empInfo = rep.GetEmployeeByID(empId)
                            If empInfo.JOIN_DATE > CDate(row("EFFECT_DATE")) Then
                                sError = "Ngày hiệu lực phải lớn hơn hoặc bằng ngày vào làm của người ký"
                                ImportValidate.IsValidNumber("EFFECT_DATE", row, rowError, isError, sError)
                            End If
                        End If

                        Dim _validate As New SignerSetupDTO
                        _validate.EFFECT_DATE = CDate(row("EFFECT_DATE"))
                        _validate.FUNC_ID = CDec(Val(row("FUNC_ID")))
                        _validate.SETUP_TYPE_ID = If(String.IsNullOrEmpty(row("SETUP_TYPE").ToString), Nothing, CType(row("SETUP_TYPE").ToString, Decimal?))
                        _validate.ORG_ID = CDec(Val(row("ORG_ID")))
                        If Not rep2.ValidateSignSet(_validate) Then
                            sError = "Dữ liệu đã tồn tại"
                            ImportValidate.IsValidTime("FUNC_ID", row, rowError, isError, sError)
                            rowError("OTHER") = rowError("FUNC_ID")
                        End If
                    End If
                End If


                If Not String.IsNullOrEmpty(row("AUTHOR_EFFECT_DATE")) AndAlso Not IsDate(row("AUTHOR_EFFECT_DATE")) Then
                    sError = "Ngày hiệu lực giấy ủy quyền sai định dạng"
                    ImportValidate.IsValidTime("AUTHOR_EFFECT_DATE", row, rowError, isError, sError)
                End If

                If Not String.IsNullOrEmpty(row("CER_BUS_RESG_EFFECT_DATE")) AndAlso Not IsDate(row("CER_BUS_RESG_EFFECT_DATE")) Then
                    sError = "Ngày hiệu lực giấy chứng nhận kinh doanh sai định dạng"
                    ImportValidate.IsValidTime("CER_BUS_RESG_EFFECT_DATE", row, rowError, isError, sError)
                End If


                If isError Then
                    If String.IsNullOrEmpty(rowError("FUNC_NAME").ToString) Then
                        rowError("FUNC_NAME") = row("FUNC_NAME").ToString
                    End If
                    If String.IsNullOrEmpty(rowError("EMPLOYEE_CODE").ToString) Then
                        rowError("EMPLOYEE_CODE") = row("EMPLOYEE_CODE").ToString
                    End If
                    rowError("SETUP_TYPE_NAME") = row("SETUP_TYPE_NAME").ToString
                    rowError("ORG_CODE") = row("FUNC_NAME").ToString
                    rowError("ORG_NAME") = row("FUNC_NAME").ToString
                    dtError.Rows.Add(rowError)
                End If
                iRow = iRow + 1
                isError = False
            Next
            If dtError.Rows.Count > 0 Then
                dtError.TableName = "DATA"
                Session("SIGN_ERR") = dtError
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Template_Import_Nguoiky_error')", True)
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                Return False
            End If
            If isError Then
                Return False
            Else
                Return True
            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                       CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Function
#End Region

    'Private Sub txtOrgName_TextChanged(sender As Object, e As EventArgs) Handles txtOrgName.TextChanged
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Dim startTime As DateTime = DateTime.UtcNow
    '    Dim rep As New CommonRepository
    '    Try
    '        If txtOrgName.Text.Trim <> "" Then
    '            Dim List_org = rep.GetOrganizationLocationTreeView()
    '            Dim orgList = (From p In List_org Where p.NAME_VN.ToUpper.Contains(txtOrgName.Text.Trim.ToUpper)).ToList
    '            If orgList.Count <= 0 Then
    '                ShowMessage(Translate("Phòng ban vừa tìm chưa được phân quyền hoặc không tồn tại."), Utilities.NotifyType.Warning)
    '                ClearControlValue(hidOrgID, txtOrgName)
    '            ElseIf orgList.Count = 1 Then
    '                hidOrgID.Value = orgList(0).ID
    '                txtOrgName.Text = orgList(0).NAME_VN
    '            Else
    '                List_Oganization_ID = (From p In orgList Select p.ID).ToList
    '                btnFindOrg_Click(Nothing, Nothing)
    '            End If
    '        End If
    '        _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")

    '    Catch ex As Exception
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '        _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '    End Try
    'End Sub

    Private Sub txtEmployeeName_TextChanged(sender As Object, e As EventArgs) Handles txtEmployeeName.TextChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New CommonRepository
        Try
            'ctrlFindEmployeePopup.EMP_CODE_OR_NAME = Nothing
            If Hid_IsEnter.Value.ToUpper = "ISENTER" Then
                Hid_IsEnter.Value = Nothing
                If txtEmployeeName.Text <> "" Then
                    Reset_Find_Emp()
                    Dim Count = 0
                    Dim EmployeeList As List(Of CommonBusiness.EmployeePopupFindListDTO)
                    Dim _filter As New CommonBusiness.EmployeePopupFindListDTO
                    _filter.EMPLOYEE_CODE = txtEmployeeName.Text
                    EmployeeList = rep.GetEmployeeFind(_filter, Count, "EMPLOYEE_CODE asc", Nothing)
                    If Count <= 0 Then
                        ShowMessage(Translate("Nhân viên vừa tìm chưa được phân quyền hoặc không tồn tại."), Utilities.NotifyType.Warning)
                        txtEmployeeName.Text = ""
                    ElseIf Count = 1 Then
                        'Dim empID = EmployeeList(0)
                        txtEmployeeName.Text = EmployeeList(0).FULLNAME_VN
                        hidSinger.Value = EmployeeList(0).ID.ToString()
                        txtTitle.Text = EmployeeList(0).TITLE_NAME.ToString()
                        isLoadPopup = 0
                    ElseIf Count > 1 Then
                        If phFindEmp.Controls.Contains(ctrlFindEmployeePopup) Then
                            phFindEmp.Controls.Remove(ctrlFindEmployeePopup)
                            'Me.Views.Remove(ctrlFindEmployeePopup.ID.ToUpper)
                        End If
                        If Not phFindEmp.Controls.Contains(ctrlFindEmployeePopup) Then
                            ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                            ctrlFindEmployeePopup.EMP_CODE_OR_NAME = txtEmployeeName.Text
                            ctrlFindEmployeePopup.MultiSelect = False
                            ctrlFindEmployeePopup.MustHaveContract = False
                            phFindEmp.Controls.Add(ctrlFindEmployeePopup)
                            ctrlFindEmployeePopup.Show()
                            isLoadPopup = 1
                        End If
                    End If
                Else
                    Reset_Find_Emp()
                End If
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub Reset_Find_Emp()
        'txtEmployeeName.Text = EmployeeList(0).FULLNAME_VN
        hidSinger.Value = ""
        txtTitle.Text = ""
    End Sub
End Class