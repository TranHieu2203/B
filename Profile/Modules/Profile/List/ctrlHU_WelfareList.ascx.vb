Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlHU_WelfareList
    Inherits Common.CommonView

    Dim lstOrganization As List(Of OrganizationDTO)
    Dim orgItem As OrganizationDTO
    Dim Code As String
    Dim Year As String = Date.Now.Year.ToString
    Dim rep As New ProfileRepository
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Profile/Module/Profile/List/" + Me.GetType().Name.ToString()

#Region "Property"
    Property Organization As OrganizationDTO
        Get
            Return ViewState(Me.ID & "_Organization")
        End Get
        Set(ByVal value As OrganizationDTO)
            ViewState(Me.ID & "_Organization") = value
        End Set
    End Property

    Public Property Organizations As List(Of OrganizationDTO)
        Get
            Return ViewState(Me.ID & "_Organizations")
        End Get
        Set(ByVal value As List(Of OrganizationDTO))
            ViewState(Me.ID & "_Organizations") = value
        End Set
    End Property
    Property SelectOrgFunction As String
        Get
            Return ViewState(Me.ID & "_SelectOrgFunction")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_SelectOrgFunction") = value
        End Set
    End Property
    Property orgid As Integer
        Get
            Return ViewState(Me.ID & "_orgid")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_orgid") = value
        End Set
    End Property
    Property isEdit As Decimal?
        Get
            Return ViewState(Me.ID & "_isEdit")
        End Get
        Set(ByVal value As Decimal?)
            ViewState(Me.ID & "_isEdit") = value
        End Set
    End Property

    Property WelfareLists As List(Of WelfareListDTO)
        Get
            Return ViewState(Me.ID & "_WelfareLists")
        End Get
        Set(ByVal value As List(Of WelfareListDTO))
            ViewState(Me.ID & "_WelfareLists") = value
        End Set
    End Property
#End Region

#Region "Page"
    Dim org_id As String
    ''' <lastupdate>
    ''' 30/06/2017 11:18
    ''' </lastupdate>
    ''' <summary>
    ''' Goi ham khoi tao gia tri ban dau cho cac control tren page
    ''' khoi tao trang thai cho grid rgAssets voi cac thiet lap filter
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            rgWelfareList.SetFilter()
            rgWelfareList.AllowCustomPaging = True
            InitControl()
            If Not IsPostBack Then
                'ViewConfig(RadPane1)
                GirdConfig(rgWelfareList)
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    ''' <summary>
    ''' Hiển thị thông tin trên trang
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        'Edit by: ChienNV 
        'Trước khi Load thì kiểm tra PostBack
        If Not IsPostBack Then
            Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
            Try
                lstOrganization = rep.GetOrganization()
                Me.Organizations = lstOrganization
                org_id = Request.QueryString("id")
                If org_id IsNot String.Empty Then
                    SelectOrgFunction = org_id
                End If
                Dim startTime As DateTime = DateTime.UtcNow
                dpSTART_DATE.AutoPostBack = True
                ctrlOrg.AutoPostBack = True
                ctrlOrg.LoadDataAfterLoaded = True
                ctrlOrg.OrganizationType = OrganizationType.OrganizationLocation
                ctrlOrg.CheckBoxes = TreeNodeTypes.None
                rgWelfareList.SetFilter()
                Refresh()
                _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
            Catch ex As Exception
                'DisplayException(Me.ViewName, Me.ID, ex)
                _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            End Try
        Else
            If ctrlOrg.CurrentValue IsNot Nothing Then
                orgid = Decimal.Parse(ctrlOrg.CurrentValue)
            Else
                ShowMessage("Chưa chọn phòng ban?", NotifyType.Warning)
                Exit Sub
            End If
        End If
    End Sub

    ''' <lastupdate>
    ''' 30/06/2017 11:18
    ''' </lastupdate>
    ''' <summary>
    ''' Khoi tao gia tri cho cac control tren page
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarWelfareLists
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create,
                                       ToolbarItem.Edit, ToolbarItem.Save, ToolbarItem.Cancel,
                                       ToolbarItem.Delete, ToolbarItem.Active, ToolbarItem.Deactive,
                                       ToolbarItem.Export)
            CType(MainToolBar.Items(2), RadToolBarButton).CausesValidation = True
            Refresh("")
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 30/06/2017 11:18
    ''' </lastupdate>
    ''' <summary>
    ''' Rebind du lieu cho rgAssets theo case message
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        'Dim rep As New ProfileRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgWelfareList.Rebind()
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgWelfareList.CurrentPageIndex = 0
                        rgWelfareList.MasterTableView.SortExpressions.Clear()
                        rgWelfareList.Rebind()
                    Case "Cancel"
                        rgWelfareList.MasterTableView.ClearSelectedItems()
                End Select
            End If
            UpdateControlState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    '''<lastupdate>
    ''' 30/06/2017 15:54
    ''' </lastupdate>
    ''' <summary>
    ''' Ham tao du lieu filter theo cac tham so dau vao
    ''' mac dinh isFull  load full man hinh hay ko?
    ''' </summary>
    ''' <param name="isFull"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim rep As New ProfileRepository
        Dim _filter As New WelfareListDTO
        _filter.param = New ParamDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If ctrlOrg.CurrentValue IsNot Nothing Then
                _filter.param.ORG_ID = Utilities.ObjToDecima(ctrlOrg.CurrentValue)
            Else
                _filter.ORG_ID = 46
            End If
            _filter.param.IS_DISSOLVE = ctrlOrg.IsDissolve
            Dim startTime As DateTime = DateTime.UtcNow
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgWelfareList, _filter)
            Dim Sorts As String = rgWelfareList.MasterTableView.SortExpressions.GetSortString()
            If isFull Then
                If Sorts IsNot Nothing Then
                    Return rep.GetWelfareList(_filter, Sorts).ToTable()
                Else
                    Return rep.GetWelfareList(_filter).ToTable()
                End If
            Else
                'Dim WelfareLists As List(Of WelfareListDTO)
                If Sorts IsNot Nothing Then
                    WelfareLists = rep.GetWelfareList(_filter, rgWelfareList.CurrentPageIndex, rgWelfareList.PageSize, MaximumRows, Sorts)
                Else
                    WelfareLists = rep.GetWelfareList(_filter, rgWelfareList.CurrentPageIndex, rgWelfareList.PageSize, MaximumRows)
                End If

                rgWelfareList.VirtualItemCount = MaximumRows
                rgWelfareList.DataSource = WelfareLists
            End If
            rep.Dispose()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Function

    ''' <lastupdate>
    ''' 30/06/2017 15:52
    ''' </lastupdate>
    ''' <summary>
    ''' Cap nhat trang thai control tren page theo trang thai them moi, sua hay default, kich hoat
    ''' huy kich hoat, update trang thai cac item tren toolbar theo tung truong hop 
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim rep As New ProfileRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                    EnabledGridNotPostback(rgWelfareList, False)
                    EnableControlAll(True, txtCode, cboName, nmSENIORITY, nmDenSoThang, nmCHILD_OLD_FROM, cbGroupTitle,
                                     nmCHILD_OLD_TO, nmMONEY, lstbGender, lstCONTRACT_TYPE,
                                     dpSTART_DATE, dpEND_DATE, chkIS_AUTO)
                    txtCode.ReadOnly = True

                Case CommonMessage.STATE_NORMAL
                    EnabledGridNotPostback(rgWelfareList, True)
                    EnableControlAll(False, txtCode, cboName, nmSENIORITY, nmDenSoThang, nmCHILD_OLD_FROM, cbGroupTitle,
                                     nmCHILD_OLD_TO, nmMONEY, lstbGender, lstCONTRACT_TYPE,
                                     dpSTART_DATE, dpEND_DATE, chkIS_AUTO)
                    txtCode.ReadOnly = True

                Case CommonMessage.STATE_EDIT

                    EnabledGridNotPostback(rgWelfareList, False)
                    EnableControlAll(True, txtCode, cboName, nmSENIORITY, nmDenSoThang, nmCHILD_OLD_FROM, cbGroupTitle,
                                     nmCHILD_OLD_TO, nmMONEY, lstbGender, lstCONTRACT_TYPE,
                                     dpSTART_DATE, dpEND_DATE, chkIS_AUTO)

                    txtCode.ReadOnly = True
                Case CommonMessage.STATE_ACTIVE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgWelfareList.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgWelfareList.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.ActiveWelfareList(lstDeletes, "A") Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                        rgWelfareList.Rebind()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                    End If

                Case CommonMessage.STATE_DEACTIVE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgWelfareList.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgWelfareList.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.ActiveWelfareList(lstDeletes, "I") Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                        rgWelfareList.Rebind()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                    End If
                Case CommonMessage.STATE_DELETE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgWelfareList.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgWelfareList.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.DeleteWelfareList(lstDeletes) Then
                        Refresh("UpdateView")
                        UpdateControlState()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        UpdateControlState()
                    End If
            End Select
            UpdateToolbarState()
            rep.Dispose()
            txtCode.Focus()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try

    End Sub

    ''' <lastupdate>
    ''' 30/06/2017 11:18
    ''' </lastupdate>
    ''' <summary>
    ''' Fill du lieu cho control combobox, thiet lap ngon ngu hien thi cho cac control
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            GetDataCombo()
            Dim dic As New Dictionary(Of String, Control)
            dic.Add("CODE", txtCode)
            'dic.Add("ID_NAME", cboName)
            dic.Add("GENDER", lstbGender)
            dic.Add("CONTRACT_TYPE", lstCONTRACT_TYPE)
            dic.Add("SENIORITY", nmSENIORITY)
            'dic.Add("SENIORITY_FROM", nmDenSoThang)
            dic.Add("MONEY", nmMONEY)
            dic.Add("CHILD_OLD_FROM", nmCHILD_OLD_FROM)
            dic.Add("CHILD_OLD_TO", nmCHILD_OLD_TO)
            dic.Add("ID_NAME", cboName)
            dic.Add("IS_AUTO", chkIS_AUTO)
            dic.Add("START_DATE", dpSTART_DATE)
            dic.Add("END_DATE", dpEND_DATE)
            'dic.Add("TITLE_GROUP_ID", cbGroupTitle)
            'dic.Add("IS_AUTO", chkIS_AUTO)
            'dic.Add("CHILD_OLD_FROM", nmCHILD_OLD_FROM)
            'dic.Add("CHILD_OLD_TO", nmCHILD_OLD_TO)
            Utilities.OnClientRowSelectedChanged(rgWelfareList, dic)
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                     CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

#End Region

#Region "Event"
    ''' <summary>
    ''' Xử lý sự kiện SelectedNodeChanged
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgWelfareList_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgWelfareList.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim rep As New ProfileRepository
        Dim ListComboData As New ComboBoxDataDTO
        Try
            If rgWelfareList.SelectedItems.Count Then
                Dim slItem As GridDataItem
                slItem = rgWelfareList.SelectedItems(0)
                If slItem.GetDataKeyValue("ID").ToString <> "" Then
                    Dim item = (From p In WelfareLists Where p.ID = Decimal.Parse(slItem.GetDataKeyValue("ID").ToString) Select p).FirstOrDefault
                    If item IsNot Nothing Then
                        If item.TITLE_GROUP_ID IsNot Nothing Then
                            cbGroupTitle.Text = item.TITLE_GROUP_NAME
                            cbGroupTitle.SelectedValue = item.TITLE_GROUP_ID
                        Else
                            cbGroupTitle.Text = " "
                        End If
                        dpSTART_DATE.SelectedDate = item.START_DATE
                        dpEND_DATE.SelectedDate = item.END_DATE
                        If item.MONEY IsNot Nothing Then
                            nmMONEY.Text = item.MONEY
                        End If
                        If item.SENIORITY IsNot Nothing Then
                            nmSENIORITY.Text = item.SENIORITY
                        End If
                        If item.SENIORITY_FROM IsNot Nothing Then
                            nmDenSoThang.Text = item.SENIORITY_FROM
                        End If
                        If item.CHILD_OLD_FROM IsNot Nothing Then
                            nmCHILD_OLD_FROM.Text = item.CHILD_OLD_FROM
                        End If
                        If item.CHILD_OLD_TO IsNot Nothing Then
                            nmCHILD_OLD_TO.Text = item.CHILD_OLD_TO
                        End If
                    End If
                    lstbGender.ClearChecked()
                    For Each chk As RadListBoxItem In lstbGender.Items
                        If item.GENDER IsNot Nothing Then
                            If item.GENDER.Contains(chk.Value) Then
                                chk.Checked = True
                            End If
                        End If
                    Next

                    lstCONTRACT_TYPE.ClearChecked()
                    For Each chk As RadListBoxItem In lstCONTRACT_TYPE.Items
                        If item.CONTRACT_TYPE IsNot Nothing Then
                            If item.CONTRACT_TYPE.Contains(chk.Value) Then
                                chk.Checked = True
                            End If
                        End If
                    Next
                    orgid = item.ORG_ID
                    isEdit = item.ID

                    'If item.GENDER = "565" Then
                    '    Dim gan As New RadListBoxItem("565", "565")
                    '    lstbGender.Items.Add(gan)
                    'Else
                    '    Dim gan As New RadListBoxItem("566", "566")
                    '    lstbGender.Items.Add(gan)
                    'End If
                    'If item.GENDER = "565,566" Then
                    '    Dim gan As New RadListBoxItem("565,566", "565,566")
                    '    lstbGender.Items.Add(gan)
                    'End If
                    'For Each line As RadListBoxItem In lstbGender.CheckedItems
                    '    line.Checked = True
                    'Next

                    Dim item2 = (From p In WelfareLists Where p.ID = Decimal.Parse(slItem.GetDataKeyValue("ID").ToString) Select p).FirstOrDefault
                    If item2 IsNot Nothing Then
                        If item2.ID_NAME IsNot Nothing Then
                            cboName.Text = item2.NAME
                            cboName.SelectedValue = item2.ID_NAME
                        Else
                            cboName.Text = " "
                        End If
                    End If

                    'Dim table As New DataTable
                    'table.Columns.Add("NAME_VN", GetType(String))
                    'table.Columns.Add("ID", GetType(Decimal))
                    'If item.GENDER IsNot Nothing Then
                    '    Dim lst = item.GENDER.Split(",")
                    '    For Each line As String In lst
                    '        If line = "565" Then
                    '            table.Rows.Add("Nam", line)
                    '        Else
                    '            table.Rows.Add("Nữ", line)
                    '        End If
                    '    Next
                    '    For Each line As RadListBoxItem In lstbGender.Items
                    '        line.Checked = True
                    '    Next

                    'End If
                End If
            End If
            Dim startTime As DateTime = DateTime.UtcNow
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Sub ctrlOrganization_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                    If ctrlOrg.CurrentValue IsNot Nothing Then
                        orgid = Decimal.Parse(ctrlOrg.CurrentValue)
                    Else
                        ShowMessage("Chưa chọn phòng ban?", NotifyType.Warning)
                        Exit Sub
                    End If
                    'orgItem = (From p In Organizations Where p.ID = orgid).SingleOrDefault
                    'Code = orgItem.CODE & "_" & Year & "_"
                    'txtCode.Text = rep.AutoGenCode(Code, "HU_WELFARE_LIST", "CODE")
            End Select
            CreateDataFilter(False)
            Dim startTime As DateTime = DateTime.UtcNow
            rgWelfareList.CurrentPageIndex = 0
            rgWelfareList.Rebind()
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Sub dpSTART_DATE_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dpSTART_DATE.SelectedDateChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim Datecheck As DateTime
            Datecheck = dpSTART_DATE.SelectedDate.ToString
            Year = Datecheck.Year.ToString
            If ctrlOrg.CurrentValue IsNot Nothing Then
                orgid = Decimal.Parse(ctrlOrg.CurrentValue)
            Else
                ShowMessage("Chưa chọn phòng ban?", NotifyType.Warning)
                Exit Sub
            End If
            'orgItem = (From p In Organizations Where p.ID = orgid).SingleOrDefault
            'Code = orgItem.CODE & "_" & Year & "_"
            'txtCode.Text = rep.AutoGenCode(Code, "HU_WELFARE_LIST", "CODE")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 30/06/2017 15:33
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien command cho control OnMainToolbar
    ''' Cac command la them moi, sua, kich hoat, huy kich hoat, xoa, xuat file, luu
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objWelfareList As New WelfareListDTO
        Dim gID As Decimal
        Dim rep As New ProfileRepository
        Dim procedure As New ProfileStoreProcedure
        Dim dt As New DataTable
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    ClearControlValue(txtCode, cboName, nmSENIORITY, nmDenSoThang, cbGroupTitle,
                                      nmMONEY, lstbGender, lstCONTRACT_TYPE,
                                      dpSTART_DATE, dpEND_DATE, chkIS_AUTO, isEdit, nmCHILD_OLD_FROM, nmCHILD_OLD_TO)
                    chkIS_AUTO.Checked = Nothing
                    isEdit = Nothing
                    rgWelfareList.Rebind()
                    If ctrlOrg.CurrentValue IsNot Nothing Then
                        If (ctrlOrg.CurrentValue = 1) Then
                            ShowMessage("Vui lòng chỉ thiết lập phúc lợi theo công ty", NotifyType.Error)
                            Exit Sub
                        End If
                    Else
                        ShowMessage("Chưa chọn phòng ban?", NotifyType.Warning)
                        Exit Sub
                    End If

                Case CommonMessage.TOOLBARITEM_EDIT
                    If rgWelfareList.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rgWelfareList.SelectedItems.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    CurrentState = CommonMessage.STATE_EDIT
                Case CommonMessage.TOOLBARITEM_ACTIVE
                    If rgWelfareList.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_ACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_ACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_DEACTIVE
                    If rgWelfareList.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DEACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_DEACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_DELETE
                    If rgWelfareList.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgWelfareList.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgWelfareList.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next

                    If Not rep.CheckExistInDatabase(lstDeletes, ProfileCommonTABLE_NAME.HU_WELFARE_LIST) Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_IS_USING), NotifyType.Warning)
                        Return
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        dtData = CreateDataFilter(True)
                        If dtData.Rows.Count > 0 Then
                            rgWelfareList.ExportExcel(Server, Response, dtData, "WelfareList")
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                        End If
                    End Using
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then

                        Dim strGenderID As New List(Of String)
                        Dim strGenderName As New List(Of String)
                        Dim strWorkStatusID As New List(Of String)
                        Dim strWorkStatusName As New List(Of String)

                        If isEdit Is Nothing Then
                            If (ctrlOrg.CurrentValue = 1) Then
                                ShowMessage("Vui lòng chỉ thiết lập phúc lợi theo công ty", NotifyType.Error)
                                Exit Sub
                            End If
                        End If

                        For Each i As RadListBoxItem In lstbGender.CheckedItems
                            strGenderID.Add(i.Value.ToString())
                            strGenderName.Add(i.Text.ToString())
                        Next
                        Try
                            For Each t As RadListBoxItem In lstCONTRACT_TYPE.CheckedItems
                                strWorkStatusID.Add(t.Value.ToString())
                                strWorkStatusName.Add(t.Text.ToString())
                            Next
                        Catch ex As Exception
                            Throw ex
                        End Try
                        If isEdit Is Nothing Then
                            orgItem = (From p In Organizations Where p.ID = orgid).SingleOrDefault
                            Code = orgItem.CODE & "_" & Year & "_"
                            txtCode.Text = rep.AutoGenCode(Code, "HU_WELFARE_LIST", "CODE")
                        End If
                        objWelfareList.CODE = txtCode.Text
                        objWelfareList.NAME = cboName.Text
                        If cboName.SelectedValue <> "" Then
                            objWelfareList.ID_NAME = Decimal.Parse(cboName.SelectedValue)
                        End If
                        If cbGroupTitle.SelectedValue <> "" Then
                            objWelfareList.TITLE_GROUP_ID = cbGroupTitle.SelectedValue
                        End If

                        If strGenderID.Count > 0 Then
                            objWelfareList.GENDER = strGenderID.Aggregate(Function(cur, [next]) cur & "," & [next])
                            objWelfareList.GENDER_NAME = strGenderName.Aggregate(Function(cur, [next]) cur & "," & [next])
                        End If
                        If strWorkStatusID IsNot Nothing And strWorkStatusID.Count > 0 Then
                            objWelfareList.CONTRACT_TYPE = strWorkStatusID.Aggregate(Function(cur, [next]) cur & "," & [next])
                            objWelfareList.CONTRACT_TYPE_NAME = strWorkStatusName.Aggregate(Function(cur, [next]) cur & "," & [next])
                        End If
                        If (nmMONEY.Value Is Nothing) Then
                            ShowMessage(" Bạn phải nhập số tiền.", NotifyType.Error)
                            Exit Sub
                        End If
                        objWelfareList.CHILD_OLD_FROM = nmCHILD_OLD_FROM.Value
                        objWelfareList.CHILD_OLD_TO = nmCHILD_OLD_TO.Value
                        If (objWelfareList.CHILD_OLD_FROM > objWelfareList.CHILD_OLD_TO) Then
                            ShowMessage("Tuổi từ phải nhỏ hơn tuổi đến", NotifyType.Error)
                            Exit Sub
                        End If
                        objWelfareList.SENIORITY = nmSENIORITY.Value
                        objWelfareList.SENIORITY_FROM = nmDenSoThang.Value
                        If (objWelfareList.SENIORITY > objWelfareList.SENIORITY_FROM) Then
                            ShowMessage("Thâm niên (tháng)  phải nhỏ hơn đến số tháng", NotifyType.Error)
                            Exit Sub
                        End If

                        objWelfareList.MONEY = nmMONEY.Value
                        If (lbMoney.Text Is Nothing) Then
                            ShowMessage(" Bạn phải nhập số tiền.", NotifyType.Error)
                            Exit Sub
                        End If
                        If (dpSTART_DATE.SelectedDate Is Nothing) Then
                            ShowMessage("Bạn phải nhập ngày hiệu lực", NotifyType.Error)
                            Exit Sub
                        End If
                        objWelfareList.START_DATE = dpSTART_DATE.SelectedDate
                        objWelfareList.END_DATE = dpEND_DATE.SelectedDate
                        If (objWelfareList.START_DATE > objWelfareList.END_DATE) Then
                            ShowMessage("Nhập ngày hết hiệu lực lớn hơn ngày hiệu lực", NotifyType.Error)
                            Exit Sub
                        End If
                        objWelfareList.IS_AUTO = chkIS_AUTO.Checked
                        objWelfareList.ORG_ID = ctrlOrg.CurrentValue
                        'If isEdit Is Nothing Then
                        '    isEdit = 0
                        'End If
                        'dt = procedure.CHECK_WELFARE(txtCode.Text, isEdit, ctrlOrg.CurrentValue, dpSTART_DATE.SelectedDate)
                        'If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                        '    ShowMessage("Phúc lợi của phòng ban này đang trùng ngày hiệu lực", NotifyType.Error)
                        '    Exit Sub
                        'End If
                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                objWelfareList.ACTFLG = "A"
                                If rep.InsertWelfareList(objWelfareList, gID) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    Refresh("InsertView")
                                    'SelectedItemDataGridByKey(rgWelfareList, gID)
                                    ClearControlValue(txtCode, cboName, nmSENIORITY, nmDenSoThang, cbGroupTitle,
                                       nmMONEY, lstbGender, lstCONTRACT_TYPE,
                                       dpSTART_DATE, dpEND_DATE, chkIS_AUTO, nmCHILD_OLD_FROM, nmCHILD_OLD_TO)
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT
                                objWelfareList.ID = rgWelfareList.SelectedValue
                                If rep.ModifyWelfareList(objWelfareList, gID) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    Refresh("UpdateView")
                                    'SelectedItemDataGridByKey(rgWelfareList, gID, , rgWelfareList.CurrentPageIndex)
                                    ClearControlValue(txtCode, cboName, nmSENIORITY, nmDenSoThang, cbGroupTitle,
                                      nmMONEY, lstbGender, lstCONTRACT_TYPE,
                                      dpSTART_DATE, dpEND_DATE, chkIS_AUTO, nmCHILD_OLD_FROM, nmCHILD_OLD_TO)
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select
                    Else
                        ExcuteScript("Resize", "ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgWelfareList")
                    End If

                    chkIS_AUTO.Checked = Nothing
                    rgWelfareList.Rebind()
                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    ClearControlValue(txtCode, cboName, nmSENIORITY, nmDenSoThang, nmMONEY, cbGroupTitle,
                                      lstbGender, lstCONTRACT_TYPE, dpSTART_DATE,
                                      dpEND_DATE, chkIS_AUTO, nmCHILD_OLD_FROM, nmCHILD_OLD_TO, cboName)
                    chkIS_AUTO.Checked = Nothing
                    Refresh("Cancel")
                    rgWelfareList.Rebind()
            End Select
            rep.Dispose()
            UpdateControlState()

            _myLog.WriteLog(_myLog._info, _classPath, method,
                                     CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Protected Sub cboCommon_ItemsRequested(ByVal sender As Object, ByVal e As RadComboBoxItemsRequestedEventArgs) _
        Handles cboName.ItemsRequested
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Using rep As New ProfileRepository
                Dim dtData As DataTable
                Dim sText As String = e.Text
                Dim dValue As Decimal
                Dim sSelectValue As String = IIf(e.Context("value") IsNot Nothing, e.Context("value"), "")
                Select Case sender.ID
                    Case cboName.ID
                        dtData = rep.GetOtherList("WELFARE", True)
                End Select
                If sText <> "" Then
                    Dim dtExist = (From p In dtData
                                   Where p("NAME") IsNot DBNull.Value AndAlso
                                  p("NAME").ToString.ToUpper = sText.ToUpper)
                    Dim dtFilter = (From p In dtData
                                    Where p("NAME") IsNot DBNull.Value AndAlso
                              p("NAME").ToString.ToUpper.Contains(sText.ToUpper))

                    If dtFilter.Count > 0 Then
                        dtData = dtFilter.CopyToDataTable
                    Else
                        dtData = dtData.Clone
                    End If
                    Dim itemOffset As Integer = e.NumberOfItems
                    Dim endOffset As Integer = Math.Min(itemOffset + sender.ItemsPerRequest, dtData.Rows.Count)
                    e.EndOfItems = endOffset = dtData.Rows.Count
                    sender.Items.Clear()
                    For i As Integer = itemOffset To endOffset - 1
                        Dim radItem As RadComboBoxItem = New RadComboBoxItem(dtData.Rows(i)("NAME").ToString(), dtData.Rows(i)("ID").ToString())
                        sender.Items.Add(radItem)
                    Next
                Else
                    Dim itemOffset As Integer = e.NumberOfItems
                    Dim endOffset As Integer = Math.Min(itemOffset + sender.ItemsPerRequest, dtData.Rows.Count)
                    e.EndOfItems = endOffset = dtData.Rows.Count
                    sender.Items.Clear()
                    For i As Integer = itemOffset To endOffset - 1
                        Dim radItem As RadComboBoxItem = New RadComboBoxItem(dtData.Rows(i)("NAME").ToString(), dtData.Rows(i)("ID").ToString())
                        sender.Items.Add(radItem)
                    Next
                End If
            End Using
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 30/06/2017 15:37
    ''' </lastupdate>
    ''' <summary>
    ''' Xu lu su kien button command cho control ctrlMessageBox
    ''' Xu ly cho cac command kich hoat, huy kich hoat, xoa
    ''' Cap nhat lai trang thai cac control theo tung command
    ''' Rebind lai du lieu tren grid rgAssets
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
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
            rgWelfareList.Rebind()
            ClearControlValue(txtCode, cboName, nmSENIORITY, nmDenSoThang, cbGroupTitle,
                                      nmMONEY, lstbGender, lstCONTRACT_TYPE,
                                      dpSTART_DATE, dpEND_DATE, chkIS_AUTO)
            chkIS_AUTO.Checked = Nothing
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try

    End Sub

    ''' <lastupdate>
    ''' 30/06/2017 15:44
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc dung de ket noi du lieu truoc khi bind du lieu va grid
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgWelfareList.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            CreateDataFilter()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    ''' <lastupdate>
    ''' 30/06/2017 15:46
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly server validate cho control cvalCode
    ''' Call ham AutogenCode(firstChar As String, tableName As String, colName As String)
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cvalCode_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalCode.ServerValidate
        Dim rep As New ProfileRepository
        Dim _validate As New WelfareListDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If CurrentState = CommonMessage.STATE_EDIT Then
                _validate.ID = rgWelfareList.SelectedValue
                _validate.CODE = txtCode.Text.Trim
                args.IsValid = rep.ValidateWelfareList(_validate)
            Else
                _validate.CODE = txtCode.Text.Trim
                args.IsValid = rep.ValidateWelfareList(_validate)
            End If

            If Not args.IsValid Then
                ' txtCode.Text = rep.AutoGenCode("PL", "HU_WELFARE_LIST", "CODE")
            End If
            rep.Dispose()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    'Private Sub dpSTART_DATE_custom_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles dpSTART_DATE_custom.ServerValidate
    '    If dpSTART_DATE.SelectedDate Is Nothing Then
    '        args.IsValid = False
    '        ShowMessage(Translate("Bạn phải nhập ngày hiệu lực."), Utilities.NotifyType.Error)
    '        Exit Sub
    '    Else
    '        args.IsValid = True
    '    End If
    'End Sub

    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien Server Validate cua control cvalDisciplineLevel
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Protected Sub cvalContractType_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalContractType.ServerValidate
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim rep As New ProfileRepository
            Dim validate As New ContractTypeDTO
            Dim lstID As New List(Of Decimal)
            If CurrentState = CommonMessage.STATE_EDIT Or CurrentState = CommonMessage.STATE_NEW Then
                For Each t As RadListBoxItem In lstCONTRACT_TYPE.CheckedItems
                    lstID.Add(t.Value)
                Next
                If (lstID.Count > 0) Then
                    For Each i In lstID
                        validate.ID = i
                        validate.ACTFLG = "A"
                        args.IsValid = rep.ValidateContractType(validate)
                        If Not args.IsValid Then
                            Exit For
                        End If
                    Next
                End If
            End If
            If Not args.IsValid Then
                Dim ListComboData As New ComboBoxDataDTO
                ListComboData.GET_CONTRACTTYPE = True
                rep.GetComboList(ListComboData)
                FillCheckBoxList(lstCONTRACT_TYPE, ListComboData.LIST_CONTRACTTYPE, "NAME", "ID")
            End If
            rep.Dispose()
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Custom"

    '''<lastupdate>
    ''' 30/06/2017 15:54
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc cap nhat trang thai cho control toolbar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub UpdateToolbarState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            ChangeToolbarState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub

    ''' <lastupdate>
    ''' 30/06/2017 16:48
    ''' </lastupdate>
    ''' <summary>
    ''' Lay du lieu cho control combobox ListComboData gioi tinh, loai hop dong
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetDataCombo()
        Dim rep As New ProfileRepository
        Dim ListComboData As New ComboBoxDataDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            ListComboData.GET_GENDER = True
            ListComboData.GET_CONTRACTTYPE = True
            ListComboData.GET_TITLE_GROUP = True
            rep.GetComboList(ListComboData)
            rep.Dispose()
            FillCheckBoxList(lstbGender, ListComboData.LIST_GENDER, "NAME_VN", "ID")
            FillCheckBoxList(lstCONTRACT_TYPE, ListComboData.LIST_CONTRACTTYPE, "NAME", "ID")
            FillDropDownList(cbGroupTitle, ListComboData.LIST_TITLE_GROUP, "NAME_VN", "ID")

            Dim dtData = rep.GetOtherList("WELFARE", False)
            FillRadCombobox(cboName, dtData, "NAME", "ID", True)

            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub

#End Region

End Class