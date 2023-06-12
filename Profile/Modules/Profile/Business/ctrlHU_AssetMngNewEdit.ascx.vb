Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlHU_AssetMngNewEdit
    Inherits CommonView

    ''' <summary>
    ''' ctrl FindAssetPopup
    ''' </summary>
    ''' <remarks></remarks>
    Protected WithEvents ctrlFindAssetPopup As ctrlFindAssetPopup

    ''' <summary>
    ''' ctrl FindEmployeePopup
    ''' </summary>
    ''' <remarks></remarks>
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup

    ''' <summary>
    ''' ctrl FindOrgPopup
    ''' </summary>
    ''' <remarks></remarks>
    Protected WithEvents ctrlFindOrgPopup As ctrlFindOrgPopup

    ''' <summary>
    ''' ctrl FindOrgReceivePopup
    ''' </summary>
    ''' <remarks></remarks>
    Protected WithEvents ctrlFindOrgReceivePopup As ctrlFindOrgPopup

    ''' <summary>
    ''' MustAuthorize
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Property MustAuthorize As Boolean = False

    ''' <summary>
    ''' IsHasInfoEmp
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IsHasInfoEmp As Boolean = False

    ''' <creator>HongDX</creator>
    ''' <lastupdate>21/06/2017</lastupdate>
    ''' <summary>Write Log</summary>
    ''' <remarks>_mylog, _pathLog, _classPath</remarks>
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Profile\Modules\Profile\Business" + Me.GetType().Name.ToString()

#Region "Property"

    ''' <summary>
    ''' AssetMng
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property AssetMng As AssetMngDTO
        Get
            Return ViewState(Me.ID & "_AssetMng")
        End Get

        Set(ByVal value As AssetMngDTO)
            ViewState(Me.ID & "_AssetMng") = value
        End Set
    End Property


    ''' <summary>
    ''' isLoadPopup
    ''' </summary>
    ''' <value></value>
    ''' <returns>
    ''' 0 - normal
    ''' 1 - Employee
    ''' 2 - Asset
    ''' </returns>
    ''' <remarks></remarks>
    Property isLoadPopup As Integer
        Get
            Return ViewState(Me.ID & "_isLoadPopup")
        End Get

        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isLoadPopup") = value
        End Set
    End Property

    ''' <summary>
    ''' ListComboData
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ListComboData As ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ListComboData")
        End Get

        Set(ByVal value As ComboBoxDataDTO)
            ViewState(Me.ID & "_ListComboData") = value
        End Set
    End Property

#End Region

#Region "Page"

    ''' <lastupdate>10/07/2017</lastupdate>
    ''' <summary>Load page, trang thai page, trang thai control</summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            GetParams()
            Refresh()
            UpdateControlState()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>10/07/2017</lastupdate>
    ''' <summary> Khoi tao control, set thuoc tinh cho grid </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            InitControl()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>10/07/2017</lastupdate>
    ''' <summary>Khoi tao, load menu toolbar</summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            Me.MainToolBar = tbarAssetMng
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Save, ToolbarItem.Seperator,
                                       ToolbarItem.Cancel)

            CType(MainToolBar.Items(0), RadToolBarButton).CausesValidation = True
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>10/07/2017</lastupdate>
    ''' <summary>Khoi tao, load menu toolbar</summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            Select Case Message
                Case "UpdateView"
                    btnEmployee.Enabled = False
                    If AssetMng IsNot Nothing Then
                        txtAssetID.Text = AssetMng.ASSET_CODE
                        txtEmployeeID.Text = AssetMng.EMPLOYEE_CODE
                        txtEmployeName.Text = AssetMng.EMPLOYEE_NAME
                        txtOrgName.Text = AssetMng.ORG_NAME
                        txtTitleName.Text = AssetMng.TITLE_NAME
                        txtAssetName.Text = AssetMng.ASSET_NAME
                        rntxtAmount.Value = AssetMng.ASSET_VALUE
                        txtORG_TRANFER.Text = AssetMng.ORG_TRANFER
                        txtORG_RECEIVE.Text = AssetMng.ORG_RECEIVE
                        txtAssetBarcode.Text = AssetMng.ASSET_BARCODE
                        txtAssetSerial.Text = AssetMng.ASSET_SERIAL

                        If AssetMng.STATUS_ID IsNot Nothing Then
                            cboSTATUS_ID.SelectedValue = AssetMng.STATUS_ID
                        End If

                        txtAssGroup.Text = AssetMng.ASSET_GROUP_NAME
                        nmDeposits.Value = AssetMng.DEPOSITS
                        txtDesc.Text = AssetMng.DESC
                        rdReturnDate.SelectedDate = AssetMng.RETURN_DATE
                        rdIssueDate.SelectedDate = AssetMng.ISSUE_DATE
                        hidEmployeeID.Value = AssetMng.EMPLOYEE_ID.ToString
                        hidAssetID.Value = AssetMng.ASSET_DECLARE_ID.ToString
                        hidID.Value = AssetMng.ID.ToString
                    End If

                Case "NormalView"
                    If AssetMng IsNot Nothing Then
                        txtAssetID.Text = AssetMng.ASSET_CODE
                        txtEmployeeID.Text = AssetMng.EMPLOYEE_CODE
                        txtEmployeName.Text = AssetMng.EMPLOYEE_NAME
                        txtOrgName.Text = AssetMng.ORG_NAME
                        txtAssetName.Text = AssetMng.ASSET_NAME
                        txtTitleName.Text = AssetMng.TITLE_NAME
                        rntxtAmount.Value = AssetMng.ASSET_VALUE
                        txtORG_TRANFER.Text = AssetMng.ORG_TRANFER
                        txtORG_RECEIVE.Text = AssetMng.ORG_RECEIVE
                        txtAssetBarcode.Text = AssetMng.ASSET_BARCODE
                        txtAssetSerial.Text = AssetMng.ASSET_SERIAL
                        cboSTATUS_ID.SelectedValue = AssetMng.STATUS_ID
                        txtAssGroup.Text = AssetMng.ASSET_GROUP_NAME
                        nmDeposits.Value = AssetMng.DEPOSITS
                        txtDesc.Text = AssetMng.DESC
                        rdReturnDate.SelectedDate = AssetMng.RETURN_DATE
                        rdIssueDate.SelectedDate = AssetMng.ISSUE_DATE
                        hidEmployeeID.Value = AssetMng.EMPLOYEE_ID.ToString
                        hidAssetID.Value = AssetMng.ASSET_DECLARE_ID.ToString
                        hidID.Value = AssetMng.ID.ToString
                    End If

                    btnEmployee.Enabled = False
                    btnAsset.Enabled = False
                    txtAssetID.ReadOnly = True
                    txtEmployeeID.ReadOnly = True
                    txtEmployeName.ReadOnly = True
                    txtOrgName.ReadOnly = True
                    txtTitleName.ReadOnly = True
                    txtAssetName.ReadOnly = True
                    EnableRadDatePicker(rdReturnDate, False)
                    EnableRadDatePicker(rdIssueDate, False)
                    nmDeposits.Enabled = False
                    rntxtAmount.Enabled = False
                    txtDesc.Enabled = False
                    txtORG_TRANFER.Enabled = False
                    txtORG_RECEIVE.Enabled = False
                    txtAssetBarcode.Enabled = False
                    txtAssetSerial.Enabled = False
                    cboSTATUS_ID.Enabled = False
                    txtAssGroup.Enabled = False
            End Select
            'tbarAssetMng.Visible = False
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>10/07/2017</lastupdate>
    ''' <summary>Fill du lieu len combobox</summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            GetDataCombo()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Event"

    ''' <lastupdate>10/07/2017</lastupdate>
    ''' <summary> 
    ''' Event click item tren menu toolbar
    ''' Set trang thai control sau khi process xong 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objAssetMng As New AssetMngDTO
        Dim gID As Decimal
        Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        objAssetMng.RETURN_DATE = rdReturnDate.SelectedDate
                        objAssetMng.ASSET_DECLARE_ID = Decimal.Parse(hidAssetID.Value)
                        objAssetMng.EMPLOYEE_ID = Decimal.Parse(hidEmployeeID.Value)
                        objAssetMng.ISSUE_DATE = rdIssueDate.SelectedDate
                        objAssetMng.ASSET_VALUE = rntxtAmount.Value
                        objAssetMng.DEPOSITS = nmDeposits.Value
                        objAssetMng.DESC = txtDesc.Text.Trim
                        objAssetMng.ORG_TRANFER = txtORG_TRANFER.Text
                        objAssetMng.ORG_RECEIVE = txtORG_RECEIVE.Text
                        objAssetMng.ASSET_BARCODE = txtAssetBarcode.Text.Trim
                        objAssetMng.ASSET_SERIAL = txtAssetSerial.Text.Trim

                        If cboSTATUS_ID.SelectedValue <> "" Then
                            objAssetMng.STATUS_ID = cboSTATUS_ID.SelectedValue
                        End If

                        objAssetMng.ASSET_GROUP_NAME = txtAssGroup.Text

                        'If DateTime.Compare(rdIssueDate.SelectedDate.Value, DateTime.Now) < 0 And
                        '    objAssetMng.STATUS_ID = ProfileCommon.HU_ASSET_MNG_STATUS.ASSET_WAIT Then
                        '    ShowMessage(Translate("Tài sản đang chờ cấp. Ngày cấp phát phải lớn hơn ngày hiện tại"), Utilities.NotifyType.Warning)
                        '    Exit Sub
                        'End If

                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                If rep.InsertAssetMng(objAssetMng, gID) Then
                                    'Me.Send(New MessageDTO With {.ID = gID, .Mess = CommonMessage.ACTION_INSERTED})
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                                    'Dim str As String = "getRadWindow().close('1');"
                                    'ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                                    ''POPUPTOLINK
                                    Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_AssetMng&group=Business")
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                                End If

                            Case CommonMessage.STATE_EDIT

                                Dim cmRep As New CommonRepository
                                Dim lstID As New List(Of Decimal)

                                lstID.Add(Convert.ToDecimal(hidID.Value))

                                If cmRep.CheckExistIDTable(lstID, "HU_ASSET_MNG", "ID") Then
                                    ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXIST_DATABASE), NotifyType.Warning)
                                    Exit Sub
                                End If

                                objAssetMng.ID = Decimal.Parse(hidID.Value)
                                If rep.ModifyAssetMng(objAssetMng, gID) Then
                                    'Me.Send(New MessageDTO With {.ID = objAssetMng.ID, .Mess = CommonMessage.ACTION_UPDATED})
                                    'Dim str As String = "getRadWindow().close('1');"
                                    'ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                                    ''POPUPTOLINK
                                    Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_AssetMng&group=Business")
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select
                    Else
                        ExcuteScript("Resize", "ResizeSplitter()")
                    End If

                Case CommonMessage.TOOLBARITEM_CANCEL
                    Me.Send(New MessageDTO With {.Mess = CommonMessage.ACTION_CANCEL})
                    ''POPUPTOLINK_CANCEL
                    Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_AssetMng&group=Business")
            End Select
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>10/07/2017</lastupdate>
    ''' <summary>Bắt sự kiện khi chọn 1 Employee cua ctrlFindEmployeePopup</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        'Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            lstCommonEmployee = CType(ctrlFindEmployeePopup.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))

            If lstCommonEmployee.Count <> 0 Then
                Dim item = lstCommonEmployee(0)
                hidEmployeeID.Value = item.ID.ToString
                txtEmployeeID.Text = item.EMPLOYEE_CODE
                txtEmployeName.Text = item.FULLNAME_VN
                txtOrgName.Text = item.ORG_NAME
                txtTitleName.Text = item.TITLE_NAME
                txtStaffRank.Text = item.STAFF_RANK_NAME
            End If

            isLoadPopup = 0

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>10/07/2017</lastupdate>
    ''' <summary>Bắt sự kiện khi chọn 1 Organization cua ctrlOrgPopup</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlOrgPopup_OrganizationSelected(sender As Object, e As Common.OrganizationSelectedEventArgs) Handles ctrlFindOrgPopup.OrganizationSelected
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            Dim orgItem = ctrlFindOrgPopup.CurrentItemDataObject

            If orgItem IsNot Nothing Then
                txtORG_TRANFER.Text = orgItem.NAME_VN
            End If

            isLoadPopup = 0

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>10/07/2017</lastupdate>
    ''' <summary>Bắt sự kiện khi chọn 1 Organization cua ctrlFindOrgReceivePopup</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindOrgReceivePopup_OrganizationSelected(ByVal sender As Object, ByVal e As Common.OrganizationSelectedEventArgs) Handles ctrlFindOrgReceivePopup.OrganizationSelected
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim orgItem = ctrlFindOrgReceivePopup.CurrentItemDataObject

            If orgItem IsNot Nothing Then
                txtORG_RECEIVE.Text = orgItem.NAME_VN
            End If

            isLoadPopup = 0

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>10/07/2017</lastupdate>
    ''' <summary>Bắt sự kiện khi chọn 1 Employee cua ctrlFindAssetPopup</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindAssetPopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindAssetPopup.AssetDeclareSelected
        Dim lstCommonAsset As New List(Of AssetDTO)
        'Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            lstCommonAsset = CType(ctrlFindAssetPopup.SelectedAssetDeclare, List(Of AssetDTO))

            If lstCommonAsset.Count <> 0 Then
                Dim item = lstCommonAsset(0)
                hidAssetID.Value = item.ID.ToString
                txtAssetID.Text = item.CODE
                txtAssetName.Text = item.NAME
                txtAssGroup.Text = item.GROUP_NAME
            End If

            isLoadPopup = 0

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>10/07/2017</lastupdate>
    ''' <summary>Sự kiện hủy tìm kiếm của ctrlFindPopup</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindPopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.CancelClicked, ctrlFindAssetPopup.CancelClicked, ctrlFindOrgPopup.CancelClicked, ctrlFindOrgReceivePopup.CancelClicked
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            isLoadPopup = 0

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>10/07/2017</lastupdate>
    ''' <summary>Button bật popup tìm kiếm Employee</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>    
    Protected Sub btnEmployee_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEmployee.Click
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            isLoadPopup = 1
            UpdateControlState()
            ctrlFindEmployeePopup.Show()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>10/07/2017</lastupdate>
    ''' <summary>Thay đổi trang thái của popup</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnAsset_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAsset.Click
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            isLoadPopup = 2
            UpdateControlState()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>10/07/2017</lastupdate>
    ''' <summary>Button bật popup tìm kiếm ORG</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnORGTRANSFER_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnORGTRANSFER.Click
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            isLoadPopup = 3
            UpdateControlState()
            ctrlFindOrgPopup.Show()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>10/07/2017</lastupdate>
    ''' <summary>Button bật popup tìm kiếm Org Receive</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnORGRECEIVE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnORGRECEIVE.Click
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            isLoadPopup = 4
            UpdateControlState()
            ctrlFindOrgReceivePopup.Show()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>10/07/2017</lastupdate>
    ''' <summary>Kiểm tra hợp lệ của ngày nhập</summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Protected Sub cvalReturnDate_IssueDate_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalReturnDate_IssueDate.ServerValidate
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            If rdReturnDate.SelectedDate IsNot Nothing Then
                If rdReturnDate.SelectedDate < rdIssueDate.SelectedDate Then
                    args.IsValid = False
                Else
                    args.IsValid = True
                End If
            Else
                args.IsValid = True
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>07/07/2017</lastupdate>
    ''' <summary>
    ''' Validate combobox trạng thái tài sản
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cvalStatusID_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalStatusID.ServerValidate
        Dim rep As New ProfileRepository
        Dim validate As New OtherListDTO
        Dim dtData As DataTable = Nothing
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            If CurrentState = CommonMessage.STATE_EDIT Or CurrentState = CommonMessage.STATE_NEW Then
                validate.ID = cboSTATUS_ID.SelectedValue
                validate.ACTFLG = "A"
                validate.CODE = "ASSET_STATUS"
                args.IsValid = rep.ValidateOtherList(validate)
            End If
            If Not args.IsValid Then
                GetDataCombo()
            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>07/07/2017</lastupdate>
    ''' <summary> Check textbox tài sản không tồn tại hoặc ngừng áp dụng </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cvalAsset_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalAsset.ServerValidate
        Dim rep As New ProfileRepository
        Dim validate As New AssetDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            validate.ID = Convert.ToDecimal(hidAssetID.Value)
            validate.ACTFLG = "A"
            args.IsValid = rep.ValidateAsset(validate)
            If Not args.IsValid Then
                hidAssetID.Value = String.Empty
                txtAssetID.Text = String.Empty
                txtAssetName.Text = String.Empty
                txtAssGroup.Text = String.Empty
            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Custom"

    ''' <lastupdate>10/07/2017</lastupdate>
    ''' <summary>Cập nhật trạn thái control</summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            If phFindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                phFindEmployee.Controls.Remove(ctrlFindEmployeePopup)
                'Me.Views.Remove(ctrlFindEmployeePopup.ID.ToUpper)
            End If

            If phFindAsset.Controls.Contains(ctrlFindAssetPopup) Then
                phFindAsset.Controls.Remove(ctrlFindAssetPopup)
                'Me.Views.Remove(ctrlFindAssetPopup.ID.ToUpper)
            End If

            If phFindOrgTransfer.Controls.Contains(ctrlFindOrgPopup) Then
                phFindOrgTransfer.Controls.Remove(ctrlFindOrgPopup)
                'Me.Views.Remove(ctrlFindAssetPopup.ID.ToUpper)
            End If

            If phFindOrgReceive.Controls.Contains(ctrlFindOrgReceivePopup) Then
                phFindOrgReceive.Controls.Remove(ctrlFindOrgReceivePopup)
                'Me.Views.Remove(ctrlFindAssetPopup.ID.ToUpper)
            End If

            Select Case isLoadPopup
                Case 1
                    ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                    phFindEmployee.Controls.Add(ctrlFindEmployeePopup)
                    ctrlFindEmployeePopup.MultiSelect = False

                Case 2
                    ctrlFindAssetPopup = Me.Register("ctrlFindAssetPopup", "Profile", "ctrlFindAssetPopup", "Shared")
                    phFindAsset.Controls.Add(ctrlFindAssetPopup)
                    ctrlFindAssetPopup.MultiSelect = False
                    ctrlFindAssetPopup.Show()

                Case 3
                    ctrlFindOrgPopup = Me.Register("ctrlFindOrgPopup", "Common", "ctrlFindOrgPopup")
                    phFindOrgTransfer.Controls.Add(ctrlFindOrgPopup)

                Case 4
                    ctrlFindOrgReceivePopup = Me.Register("ctrlFindOrgPopup", "Common", "ctrlFindOrgPopup")
                    phFindOrgReceive.Controls.Add(ctrlFindOrgReceivePopup)
            End Select

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>10/07/2017</lastupdate>
    ''' <summary>Get các tham số</summary>
    ''' <remarks></remarks>
    Private Sub GetParams()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            cboSTATUS_ID.ClearSelection()
            cboSTATUS_ID.SelectedIndex = 0

            If Request.Params("gUID") IsNot Nothing Then
                hidID.Value = Request.Params("gUID")

                If Request.Params("IsHasInfoEmp") IsNot Nothing Then
                    IsHasInfoEmp = True
                End If

                If IsHasInfoEmp Then
                    CurrentState = CommonMessage.STATE_NEW
                Else
                    CurrentState = CommonMessage.STATE_EDIT
                End If

                LoadData(IsHasInfoEmp)
            Else
                CurrentState = CommonMessage.STATE_NEW
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>10/07/2017</lastupdate>
    ''' <summary>Load data cho cac textbox</summary>
    ''' <param name="IsHasInfoEmp"></param>
    ''' <remarks></remarks>
    Private Sub LoadData(ByVal IsHasInfoEmp As Boolean)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim rep As New ProfileBusinessRepository

        Try
            Dim AssetMng = rep.GetAssetMngById(hidID.Value)
            If AssetMng IsNot Nothing Then
                hidEmployeeID.Value = AssetMng.EMPLOYEE_ID
                txtEmployeeID.Text = AssetMng.EMPLOYEE_CODE
                txtEmployeName.Text = AssetMng.EMPLOYEE_NAME
                txtOrgName.Text = AssetMng.ORG_NAME
                txtTitleName.Text = AssetMng.TITLE_NAME
                txtStaffRank.Text = AssetMng.STAFF_RANK_NAME

                If IsHasInfoEmp = False Then
                    txtAssetID.Text = AssetMng.ASSET_CODE
                    txtAssetName.Text = AssetMng.ASSET_NAME
                    rntxtAmount.Value = AssetMng.ASSET_VALUE
                    txtORG_TRANFER.Text = AssetMng.ORG_TRANFER
                    txtORG_RECEIVE.Text = AssetMng.ORG_RECEIVE
                    txtAssetBarcode.Text = AssetMng.ASSET_BARCODE
                    txtAssetSerial.Text = AssetMng.ASSET_SERIAL

                    If AssetMng.STATUS_ID IsNot Nothing Then
                        cboSTATUS_ID.SelectedValue = AssetMng.STATUS_ID
                    End If

                    txtAssGroup.Text = AssetMng.ASSET_GROUP_NAME
                    nmDeposits.Value = AssetMng.DEPOSITS
                    txtDesc.Text = AssetMng.DESC
                    rdReturnDate.SelectedDate = AssetMng.RETURN_DATE
                    rdIssueDate.SelectedDate = AssetMng.ISSUE_DATE
                    hidEmployeeID.Value = AssetMng.EMPLOYEE_ID.ToString
                    hidAssetID.Value = AssetMng.ASSET_DECLARE_ID.ToString
                    hidID.Value = AssetMng.ID.ToString

                    If AssetMng.WORK_STATUS = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID Then
                        MainToolBar.Items(0).Enabled = False
                        LeftPane.Enabled = False
                    End If
                End If
            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <summary>
    ''' Load data cho combobox
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetDataCombo()
        Dim rep As New ProfileRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            If ListComboData Is Nothing Then
                ListComboData = New ComboBoxDataDTO
                ListComboData.GET_ASSET_STATUS = True
                rep.GetComboList(ListComboData)
            End If

            FillRadCombobox(cboSTATUS_ID, ListComboData.LIST_ASSET_STATUS, "NAME_VN", "ID")
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

End Class