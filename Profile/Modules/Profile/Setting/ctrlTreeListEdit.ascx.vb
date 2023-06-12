Imports System.IO
Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports ICSharpCode.SharpZipLib.Checksums
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlTreeListEdit
    Inherits Common.CommonView
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup

#Region "Property"

    ''' <summary>
    ''' Obj Organization
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Organization As OrganizationDTO
        Get
            Return ViewState(Me.ID & "_Organization")
        End Get
        Set(ByVal value As OrganizationDTO)
            ViewState(Me.ID & "_Organization") = value
        End Set
    End Property

    ''' <summary>
    ''' Obj Organizations
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Organizations As List(Of OrganizationDTO)
        Get
            Return ViewState(Me.ID & "_Organizations")
        End Get
        Set(ByVal value As List(Of OrganizationDTO))
            ViewState(Me.ID & "_Organizations") = value
        End Set
    End Property

    ''' <summary>
    ''' Obj ActiveOrganizations
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ActiveOrganizations As List(Of OrganizationDTO)
        Get
            Return ViewState(Me.ID & "_ActiveOrganizations")
        End Get
        Set(ByVal value As List(Of OrganizationDTO))
            ViewState(Me.ID & "_ActiveOrganizations") = value
        End Set
    End Property

    ''' <summary>
    ''' Obj SelectOrgFunction
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SelectOrgFunction As String
        Get
            Return ViewState(Me.ID & "_SelectOrgFunction")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_SelectOrgFunction") = value
        End Set
    End Property

    ''' <summary>
    ''' Obj LicenseFile
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>
    ''' 0 - normal
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

    ''' <summary>
    ''' Obj ImageFile
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ImageFile As Telerik.Web.UI.UploadedFile
        Get
            Return ViewState(Me.ID & "_ImageFile")
        End Get
        Set(ByVal value As Telerik.Web.UI.UploadedFile)
            ViewState(Me.ID & "_ImageFile") = value
        End Set
    End Property

    ''' <summary>
    ''' Obj LicenseFile
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property LicenseFile As Telerik.Web.UI.UploadedFile
        Get
            Return ViewState(Me.ID & "_LicenseFile")
        End Get
        Set(ByVal value As Telerik.Web.UI.UploadedFile)
            ViewState(Me.ID & "_LicenseFile") = value
        End Set
    End Property
    Property Down_File As String
        Get
            Return ViewState(Me.ID & "_Down_File")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_Down_File") = value
        End Set
    End Property

#End Region

#Region "Page"
    Dim org_id As String
    Dim _mylog As New MyLog()

    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Profile/Module/Profile/Setting/" + Me.GetType().Name.ToString()

    ''' <lastupdate>
    ''' 24/07/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức ViewLoad, hiển thị dữ liệu trên trang
    ''' Làm mới trang
    ''' Cập nhật các trạng thái của các control trên page
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Refresh()
            UpdateControlState()
            FillDataByTree()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 24/07/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức khởi tạo dữ liệu cho các control trên trang
    ''' Gọi phương thức khởi tạo 
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        InitControl()
    End Sub

    ''' <lastupdate>
    ''' 24/07/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức BindData
    ''' Gọi phương thức load dữ liệu cho các combobox
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Try
            GetDataCombo()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 24/07/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ham khoi tao toolbar voi cac button them moi, sua, luu, huy, ap dung, ngung ap dung
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Me.MainToolBar = tbarOrgFunctions
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Save, ToolbarItem.Seperator,
                                       ToolbarItem.Cancel)
            CType(MainToolBar.Items(0), RadToolBarButton).CausesValidation = True
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 24/07/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc lam moi cac control theo tuy chon Message, 
    ''' Message co gia tri default la ""
    ''' Xet cac tuy chon gia tri cua Message la UpdateView, InsertView
    ''' Bind lai du lieu cho tree view
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim lstOrganization As List(Of OrganizationDTO)
            Dim rep As New ProfileRepository
            SelectOrgFunction = Request("orgid")
            Dim dtData As DataTable
            dtData = rep.GetOtherList("STATUS_ORG", True)
            FillRadCombobox(cboStatus, dtData, "NAME", "ID")
            Try
                Dim startTime As DateTime = DateTime.UtcNow
                If Not IsPostBack Then
                    lstOrganization = rep.GetOrgTreeEmp("")
                    Me.Organizations = lstOrganization
                    CurrentState = CommonMessage.STATE_NORMAL
                    SelectOrgFunction = Request("orgid")
                    Dim orgItem As OrganizationDTO
                    Dim orgItemParent As OrganizationDTO
                    orgItem = (From p In Organizations Where p.ID = Decimal.Parse(Request("orgid"))).SingleOrDefault

                    If orgItem IsNot Nothing Then
                        hidParentID.Value = orgItem.PARENT_ID.ToString
                        If (orgItem.PARENT_ID IsNot Nothing) Then
                            orgItemParent = (From p In Organizations Where p.ID = Decimal.Parse(orgItem.PARENT_ID)).SingleOrDefault
                            If (orgItemParent IsNot Nothing And orgItemParent.NAME_VN IsNot Nothing) Then
                                txtNameVN.Text = orgItemParent.NAME_VN
                            Else
                                txtNameVN.Text = ""
                            End If
                        Else
                            txtNameVN.Text = ""
                        End If
                        hidID.Value = Request("orgid")
                        txtCode.Text = orgItem.CODE
                        txtNameVN.Text = orgItem.NAME_VN
                        txtNameEN.Text = orgItem.NAME_EN
                        If IsNumeric(orgItem.POSITION_ID) Then
                            cboTitle.SelectedValue = orgItem.POSITION_ID
                        End If
                        txtGdBp.Text = orgItem.POSITION_NAME
                        'txtRemark.Text = orgItem.REMARK
                        rdDissolveDate.SelectedDate = orgItem.DISSOLVE_DATE
                        rdFoundationDate.SelectedDate = orgItem.FOUNDATION_DATE
                        txtAddress.Text = orgItem.ADDRESS
                        txtMobile.Text = orgItem.MOBILE
                        If orgItem.STATUS_ID IsNot Nothing Then
                            cboStatus.SelectedValue = orgItem.STATUS_ID
                        End If
                        txtProvinceName.Text = orgItem.PROVINCE_NAME
                        'rntxtOrdNo.Value = orgItem.ORD_NO
                        'txtNumber_business.Text = orgItem.NUMBER_BUSINESS
                        rdDate_Business.SelectedDate = orgItem.DATE_BUSINESS
                        txtPIT_NO.Text = orgItem.PIT_NO
                        txtCostCenterCode.Text = orgItem.COST_CENTER_CODE

                        If orgItem.NLEVEL IsNot Nothing Then
                            txtCap.Text = orgItem.NLEVEL
                        End If
                        ckbnhom.Checked = False
                        If orgItem.GROUPPROJECT IsNot Nothing Then
                            ckbnhom.Checked = orgItem.GROUPPROJECT
                        End If
                        If orgItem.REPRESENTATIVE_ID IsNot Nothing Then
                            hidRepresentative.Value = orgItem.REPRESENTATIVE_ID
                        End If
                        tr01.Visible = False
                        tr02.Visible = False
                        txtUploadFile.Text = orgItem.ATTACH_FILE
                        txtUpload.Text = orgItem.FILENAME
                    End If
                Else
                    Select Case Message
                        Case "UpdateView"
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            lstOrganization = rep.GetOrgTreeEmp()
                            Me.Organizations = lstOrganization
                            CurrentState = CommonMessage.STATE_NORMAL
                            UpdateToolbarState(CurrentState)
                        Case "InsertView"
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            lstOrganization = rep.GetOrgTreeEmp()
                            Me.Organizations = lstOrganization
                            CurrentState = CommonMessage.STATE_NORMAL
                    End Select
                End If

                'Đưa dữ liệu vào Grid
                If Me.Organizations IsNot Nothing Then
                    If SelectOrgFunction Is String.Empty Then
                        SelectOrgFunction = treeorgfunction.SelectedValue
                    End If
                    BuildTreeNode(treeorgfunction, Me.Organizations, cbdissolve.Checked)
                End If
                _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")

            Catch ex As Exception
                DisplayException(Me.ViewName, Me.ID, ex)
                _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            End Try
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    ''' <lastupdate>
    ''' 24/07/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc fill du lieu cho cac combobox
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetDataCombo()
        Try
            'chiennv add OM
            ''Loading combobox vị trí công việc
            Dim dtDataTitle As DataTable
            Dim obja As New ParamIDDTO
            Dim repa As New ProfileRepository
            If IsNumeric(Request("orgid")) Then
                dtDataTitle = repa.GetDataByProcedures(9, Decimal.Parse(Request("orgid")), "0", Common.Common.SystemLanguage.Name)
                FillRadCombobox(cboTitle, dtDataTitle, "NAME", "ID", False)
            End If
            '----------------------------------------'
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Event"

    ''' <lastupdate>
    ''' 24/07/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien command khi click item tren toolbar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            Dim objOrgFunction As New OrganizationDTO
            Dim gID As Decimal
            Dim rep As New ProfileRepository
            Try
                Dim startTime As DateTime = DateTime.UtcNow
                Select Case CType(e.Item, RadToolBarButton).CommandName
                    Case CommonMessage.TOOLBARITEM_CANCEL
                        Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlTreelistEmp&group=Business")
                    Case CommonMessage.TOOLBARITEM_SAVE
                        If Not Page.IsValid Then
                            Exit Sub
                        End If

                        Dim dtDataCheckOM = rep.GetDataByProcedures(11, Request("orgid"), "", Common.Common.SystemLanguage.Name)
                        If dtDataCheckOM IsNot Nothing Then
                            If dtDataCheckOM.Rows.Count >= 1 Then
                                ShowMessage(Translate("Không thể lưu khi còn tổ chức cha Mới khởi tạo"), NotifyType.Warning)
                                Exit Sub
                            End If
                        End If

                        Dim orgItem As New OrganizationDTO
                        orgItem = (From p In Organizations Where p.ID = Decimal.Parse(Request("orgid"))).SingleOrDefault
                        If orgItem.STATUS_NAME = "Mới khởi tạo" Then
                            ShowMessage(Translate("Không thể lưu khi tổ chức đang ở trạng thái mới khởi tạo"), NotifyType.Warning)
                            Exit Sub
                        End If
                        If orgItem.DISSOLVE_DATE IsNot Nothing Then
                            Dim orgItemParent As Decimal
                            'orgItemParent = (From p In Organizations Where p.PARENT_ID = Decimal.Parse(Request("orgid")) And p.STATUS_NAME = 8527).Count
                            orgItemParent = (From p In Organizations Where p.PARENT_ID = Decimal.Parse(Request("orgid"))).Count
                            If orgItemParent >= 1 Then
                                ShowMessage(Translate("Không thể lưu khi còn tổ chức con đang kích hoạt"), NotifyType.Warning)
                                Exit Sub
                            End If
                            If Not rep.CheckEmployeeInOrganization(GetAllChild(Request("orgid"))) Then
                                ShowMessage("Bạn phải điều chuyển toàn bộ nhân viên trước khi lưu.", NotifyType.Warning)
                                Exit Sub
                            End If
                        End If
                        objOrgFunction.DISSOLVE_DATE = rdDissolveDate.SelectedDate
                        objOrgFunction.FOUNDATION_DATE = rdFoundationDate.SelectedDate
                        objOrgFunction.ADDRESS = txtAddress.Text.Trim
                        objOrgFunction.MOBILE = txtMobile.Text.Trim
                        objOrgFunction.PROVINCE_NAME = txtProvinceName.Text.Trim
                        If IsNumeric(cboTitle.SelectedValue) Then
                            objOrgFunction.POSITION_ID = Decimal.Parse(cboTitle.SelectedValue)
                        End If
                        'If cboStatus.SelectedValue <> "" Then
                        '    objOrgFunction.STATUS_ID = cboStatus.SelectedValue
                        'End If
                        objOrgFunction.COST_CENTER_CODE = txtCostCenterCode.Text.Trim
                        objOrgFunction.DATE_BUSINESS = rdDate_Business.SelectedDate
                        objOrgFunction.PIT_NO = txtPIT_NO.Text.Trim
                        If hidRepresentative.Value <> "" Then
                            objOrgFunction.REPRESENTATIVE_ID = hidRepresentative.Value
                        End If
                        Dim lstPath As New List(Of OrganizationPathDTO)
                        'If hidID.Value IsNot Nothing Then
                        '    If hidID.Value <> "" Then
                        '        objOrgFunction.PARENT_ID = Decimal.Parse(hidID.Value)
                        '    End If
                        'End If
                        objOrgFunction.ID = Decimal.Parse(hidID.Value)
                        objOrgFunction.ATTACH_FILE = txtUploadFile.Text 'Guid directory
                        objOrgFunction.FILENAME = txtUpload.Text
                        GetDownLevelByNode(treeorgfunction.Nodes(0), lstPath)
                        'objOrgFunction.DESCRIPTION_PATH = objPath.DESCRIPTION_PATH.Replace(treeorgfunction.SelectedNode.Text, txtCode.Text + " - " + txtNameVN.Text)
                        If rep.ModifyOrgTreeEmp(objOrgFunction, gID) Then
                            ' số bản ghi cập nhật
                            Dim iSoBanGhi As Integer = 50
                            ' số dư để làm tròn
                            Dim iSoDu As Integer = lstPath.Count Mod iSoBanGhi
                            Dim iTongVongLap As Integer
                            ' số vòng lặp khi làm tròn vs số bản ghi cập nhật
                            If iSoDu = 0 Then
                                iTongVongLap = (lstPath.Count - iSoDu) / iSoBanGhi
                            Else
                                iTongVongLap = ((lstPath.Count - iSoDu) / iSoBanGhi) + 1
                            End If


                            For item = 0 To iTongVongLap - 1
                                ' cập nhật từng đợt ( tối đa = số bản ghi cập nhật )
                                Dim lstUpdate As New List(Of OrganizationPathDTO)

                                If item <> iTongVongLap - 1 Then
                                    For idx = item * iSoBanGhi To (item + 1) * iSoBanGhi - 1
                                        lstUpdate.Add(lstPath(idx))
                                    Next
                                Else
                                    For idx = item * iSoBanGhi To lstPath.Count - 1
                                        lstUpdate.Add(lstPath(idx))
                                    Next
                                End If
                                ' cập nhật bản ghi
                                rep.ModifyOrganizationPath(lstUpdate)
                            Next

                            CurrentState = CommonMessage.STATE_NORMAL
                            Refresh("UpdateView")
                            Common.Common.OrganizationLocationDataSession = Nothing
                            If gID <> 0 Then
                                If ImageFile IsNot Nothing Then
                                    Dim filePath = AppDomain.CurrentDomain.BaseDirectory & "ReportTemplates\Profile\Organization\Logo\"
                                    If Not Directory.Exists(filePath) Then
                                        Directory.CreateDirectory(filePath)
                                    End If
                                    Dim fileName As String = gID & "_" & ImageFile.GetName
                                    Dim dirs As String() = Directory.GetFiles(filePath, gID & "_*")
                                    If dirs.Length > 0 Then
                                        For Each str As String In dirs
                                            If File.Exists(str) Then
                                                Try
                                                    File.Delete(str)
                                                Catch ex As Exception
                                                End Try
                                            End If
                                        Next
                                    End If
                                    ImageFile.SaveAs(filePath & fileName)
                                    rbiEmployeeImage.Visible = True
                                End If
                                If LicenseFile IsNot Nothing Then
                                    Dim filePath = AppDomain.CurrentDomain.BaseDirectory & "ReportTemplates\Profile\Organization\License\"
                                    If Not Directory.Exists(filePath) Then
                                        Directory.CreateDirectory(filePath)
                                    End If
                                    Dim fileName As String = gID & "_" & LicenseFile.GetName
                                    Dim dirs As String() = Directory.GetFiles(filePath, gID & "_*")
                                    If dirs.Length > 0 Then
                                        For Each str As String In dirs
                                            If File.Exists(str) Then
                                                Try
                                                    File.Delete(str)
                                                Catch ex As Exception
                                                End Try
                                            End If
                                        Next
                                    End If
                                    LicenseFile.SaveAs(filePath & fileName)
                                    btnDownFile.Visible = True
                                End If
                            End If
                            ''POPUPTOLINK
                            Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlTreelistEmp&group=Business")
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                        End If

                End Select
                UpdateToolbarState(CurrentState)
                UpdateControlState()
                Session.Remove("OrganizationOMLocationDataCache")
                _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
            Catch ex As Exception
                DisplayException(Me.ViewName, Me.ID, ex)
                _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            End Try
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    ''' <lastupdate>
    ''' 24/07/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien OnReceiveData cua control ctrlOrganization
    ''' Cap nhat trang thai cua cac control theo params
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub ctrlOrganization_OnReceiveData(ByVal sender As IViewListener(Of ViewBase), ByVal e As ViewCommunicationEventArgs) Handles Me.OnReceiveData
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim mess As MessageDTO = CType(e.EventData, MessageDTO)
            Select Case e.FromViewID
                Case "ctrlOrganizationNew"
                    Select Case mess.Mess
                        Case CommonMessage.ACTION_INSERTED
                            CurrentState = CommonMessage.STATE_NEW
                            Refresh("InsertView")
                            UpdateControlState()
                        Case CommonMessage.TOOLBARITEM_CANCEL
                            CurrentState = CommonMessage.STATE_NORMAL
                            UpdateControlState()
                    End Select
                Case "ctrlOrganizationEdit"
                    Select Case mess.Mess
                        Case CommonMessage.ACTION_UPDATED
                            CurrentState = CommonMessage.STATE_NORMAL
                            Refresh("UpdateView")
                            UpdateControlState()
                        Case CommonMessage.TOOLBARITEM_CANCEL
                            CurrentState = CommonMessage.STATE_NORMAL
                            UpdateControlState()
                    End Select
            End Select
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub


    ''' <lastupdate>
    ''' 24/07/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien CheckedChanged cua control cbDissolve
    ''' Cap nhat trang thai cua cac control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub cbDissolve_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbdissolve.CheckedChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            'SelectOrgFunction = treeOrgFunction.SelectedValue
            'BuildTreeNode(treeOrgFunction, Organizations, cbDissolve.Checked)
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")


        End Try
    End Sub

    ''' <lastupdate>
    ''' 24/07/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien validate cua control cvalCostCenterCode
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    'Private Sub cvalCostCenterCode_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalCostCenterCode.ServerValidate
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Try
    '        Dim startTime As DateTime = DateTime.UtcNow
    '        Dim rep As New ProfileRepository
    '        Dim _validate As New OrganizationDTO
    '        Try

    '            _validate.COST_CENTER_CODE = txtCostCenterCode.Text.Trim
    '            _validate.ID = Request("orgid")
    '            args.IsValid = rep.ValidateCostCenterCode(_validate)

    '            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '        Catch ex As Exception
    '            DisplayException(Me.ViewName, Me.ID, ex)
    '            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '        End Try
    '    Catch ex As Exception

    '    End Try

    'End Sub

    ''' <lastupdate>
    ''' 24/07/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien validate cua control cval_FoundDate_DissDate
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Protected Sub cval_FoundDate_DissDate_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cval_FoundDate_DissDate.ServerValidate
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If rdDissolveDate.SelectedDate IsNot Nothing Then
                If rdDissolveDate.SelectedDate < rdFoundationDate.SelectedDate Then
                    args.IsValid = False
                    Exit Sub
                End If
            End If
            args.IsValid = True
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 24/07/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien node click cua control treeOrgFunction
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub treeOrgFunction_NodeClick(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadTreeNodeEventArgs) Handles treeorgfunction.NodeClick
        Dim lstPath As New List(Of OrganizationPathDTO)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If treeorgfunction.SelectedNode Is Nothing Then
                Exit Sub
            End If
            GetUpLevelByNode(treeorgfunction.SelectedNode)
            hidID.Value = Request("orgid")
            SelectOrgFunction = Request("orgid")
            treeorgfunction.SelectedNode.ExpandParentNodes()
            FillDataByTree()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 24/07/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien click cua control btnSaveImage
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSaveImage_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSaveImage.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If _radAsynceUpload.UploadedFiles.Count = 0 Then
                Exit Sub
            End If
            Dim files = _radAsynceUpload.UploadedFiles(0)
            Dim fileContent As Byte() = New Byte(files.ContentLength) {}
            Using str As Stream = files.InputStream
                str.Read(fileContent, 0, files.ContentLength)
            End Using
            'Lưu  image vào PageViewStates , khi insert Employee vào Database mới lưu image này lên folder trên server.
            ImageFile = files
            'Hiển thị ảnh mới.
            rbiEmployeeImage.DataValue = fileContent
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    ''' <lastupdate>
    ''' 24/07/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien click cua control btnSaveFile
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSaveFile_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSaveFile.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If _radAsynceUpload1.UploadedFiles.Count = 0 Then
                Exit Sub
            End If
            Dim files = _radAsynceUpload1.UploadedFiles(0)
            Dim fileContent As Byte() = New Byte(files.ContentLength) {}
            Using str As Stream = files.InputStream
                str.Read(fileContent, 0, files.ContentLength)
            End Using
            'Lưu  image vào PageViewStates , khi insert Employee vào Database mới lưu image này lên folder trên server.
            LicenseFile = files
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    ''' <lastupdate>
    ''' 24/07/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien click cua control btnDownFile
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnDownFile_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDownFile.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim linkPath = AppDomain.CurrentDomain.BaseDirectory & "ReportTemplates\Profile\Organization\License\"
            Dim license = Request("orgid") & "_*"
            Dim dirs As String() = Directory.GetFiles(linkPath, license)
            If dirs.Length > 0 Then
                Dim fileInfo = New FileInfo(dirs(0))
                Response.Clear()
                Response.ClearHeaders()
                Response.ClearContent()
                Response.AddHeader("Content-Disposition", "attachment; filename=" + fileInfo.Name)
                Response.AddHeader("Content-Length", fileInfo.Length.ToString())
                Response.ContentType = "text/plain"
                Response.Flush()
                Response.TransmitFile(fileInfo.FullName)
                Response.End()
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Sub btnUploadFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUploadFile.Click
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            ctrlUpload1.AllowedExtensions = "xls,xlsx,txt,ctr,doc,docx,xml,png,jpg,bitmap,jpeg,gif,pdf,rar,zip,ppt,pptx"
            ctrlUpload1.Show()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Sub ctrlUpload1_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload1.OkClicked
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            txtUploadFile.Text = ""
            Dim listExtension = New List(Of String)
            listExtension.Add(".xls")
            listExtension.Add(".xlsx")
            listExtension.Add(".txt")
            listExtension.Add(".ctr")
            listExtension.Add(".doc")
            listExtension.Add(".docx")
            listExtension.Add(".xml")
            listExtension.Add(".png")
            listExtension.Add(".jpg")
            listExtension.Add(".bitmap")
            listExtension.Add(".jpeg")
            listExtension.Add(".gif")
            listExtension.Add(".pdf")
            listExtension.Add(".rar")
            listExtension.Add(".zip")
            listExtension.Add(".ppt")
            listExtension.Add(".pptx")
            Dim fileName As String

            Dim strPath As String = Server.MapPath("~/ReportTemplates/Profile/Organization/File/")
            If ctrlUpload1.UploadedFiles.Count >= 1 Then
                For i = 0 To ctrlUpload1.UploadedFiles.Count - 1
                    Dim file As UploadedFile = ctrlUpload1.UploadedFiles(i)
                    Dim str_Filename = Guid.NewGuid.ToString()
                    Dim str_Filename1 = str_Filename + "\"
                    If listExtension.Any(Function(x) x.ToUpper().Trim() = file.GetExtension.ToUpper().Trim()) Then
                        System.IO.Directory.CreateDirectory(strPath + str_Filename1)
                        strPath = strPath + str_Filename1
                        fileName = System.IO.Path.Combine(strPath, file.FileName)
                        file.SaveAs(fileName, True)
                        txtUpload.Text = file.FileName
                        Down_File = str_Filename
                    Else
                        ShowMessage(Translate("Vui lòng chọn file đúng định dạng. !!! Hệ thống chỉ nhận file xls,xlsx,txt,ctr,doc,docx,xml,png,jpg,bitmap,jpeg,gif,pdf,rar,zip,ppt,pptx"), NotifyType.Warning)
                        Exit Sub
                    End If
                Next
                loadDatasource(txtUpload.Text)
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
#End Region

#Region "Custom"

    ''' <lastupdate>
    ''' 24/07/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc cap nhat trang thai cac control trong page
    ''' goi lai ham de update lai trang thai cua cac control theo state
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New ProfileRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            Select Case isLoadPopup
                Case 1
                    If Not phPopup.Controls.Contains(ctrlFindEmployeePopup) Then
                        ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                        phPopup.Controls.Add(ctrlFindEmployeePopup)
                        ctrlFindEmployeePopup.MultiSelect = False
                        ctrlFindEmployeePopup.MustHaveContract = False
                        ctrlFindEmployeePopup.LoadAllOrganization = True
                    End If
            End Select

            Select Case CurrentState
                Case CommonMessage.STATE_NEW

                Case CommonMessage.STATE_NORMAL
                    UpdateToolbarState(CurrentState)
                Case CommonMessage.STATE_EDIT

                Case CommonMessage.STATE_DEACTIVE
                    If rep.ActiveOrganization(ActiveOrganizations, "I") Then
                        ActiveOrganizations = Nothing
                        Refresh("UpdateView")

                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                    End If
                Case CommonMessage.STATE_ACTIVE
                    If rep.ActiveOrganization(ActiveOrganizations, "A") Then
                        ActiveOrganizations = Nothing
                        Refresh("UpdateView")
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                    End If
            End Select
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    ''' <lastupdate>
    ''' 24/07/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc cap nhat trang thai cac control trong page
    ''' goi lai ham de update lai trang thai cua cac control theo state
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub UpdateToolbarState(ByVal sState As String)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            Dim startTime As DateTime = DateTime.UtcNow


            cbdissolve.Enabled = False
            treeorgfunction.Enabled = False

            txtCode.ReadOnly = True
            txtNameVN.ReadOnly = True
            txtCap.ReadOnly = True
            ckbnhom.Enabled = False
            txtAddress.ReadOnly = False
            txtMobile.ReadOnly = False
            txtProvinceName.ReadOnly = False
            txtCostCenterCode.ReadOnly = False
            txtPIT_NO.ReadOnly = False
            EnableRadDatePicker(rdDate_Business, True)

            EnableRadDatePicker(rdDissolveDate, True)
            EnableRadDatePicker(rdFoundationDate, True)
            _radAsynceUpload.Enabled = True
            _radAsynceUpload1.Enabled = True

        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 24/07/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc build lai tree view
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub BuildTreeNode(ByVal tree As RadTreeView,
                                ByVal list As List(Of OrganizationDTO),
                                ByVal bCheck As Boolean)
        Dim node As New RadTreeNode
        Dim bSelected As Boolean = False
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try

            tree.Nodes.Clear()
            If list.Count = 0 Then
                Exit Sub
            End If
            Dim listTemp = (From t In list
                            Where t.PARENT_ID Is Nothing Select t).FirstOrDefault
            Select Case Common.Common.SystemLanguage.Name
                Case "vi-VN"
                    node.Text = listTemp.CODE & " - " & listTemp.NAME_VN
                    node.ToolTip = listTemp.NAME_VN
                Case Else
                    node.Text = listTemp.CODE & " - " & listTemp.NAME_EN
                    node.ToolTip = listTemp.NAME_EN
            End Select

            node.Value = listTemp.ID.ToString
            If SelectOrgFunction IsNot Nothing Then
                If node.Value = SelectOrgFunction Then
                    node.Selected = True
                    bSelected = True
                End If

            End If

            tree.Nodes.Add(node)
            BuildTreeChildNode(node, list, bCheck, bSelected)
            tree.ExpandAllNodes()
            If SelectOrgFunction = "" Then
                If tree.Nodes.Count > 0 Then
                    If tree.Nodes(0).Nodes.Count > 0 Then
                        tree.Nodes(0).Nodes(0).Selected = True
                        tree.Nodes(0).ExpandParentNodes()
                        treeOrgFunction_NodeClick(Nothing, Nothing)
                        bSelected = True
                    End If
                End If
            Else
                Dim treeNode As RadTreeNode = Nothing
                For Each n As RadTreeNode In tree.Nodes
                    treeNode = getNodeSelected(n)
                    If treeNode IsNot Nothing Then
                        Exit For
                    End If
                Next
                If treeNode IsNot Nothing Then
                    treeNode.Selected = True
                    treeNode.ExpandParentNodes()

                End If
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 24/07/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc lay ra node duoc chon cua treeview
    ''' </summary>
    ''' <remarks></remarks>
    Private Function getNodeSelected(ByVal node As RadTreeNode) As RadTreeNode
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If node.Value = SelectOrgFunction Then
                Return node
            End If
            For Each n As RadTreeNode In node.Nodes
                If n.Value = SelectOrgFunction Then
                    Return n
                End If
                Dim _node As RadTreeNode = getNodeSelected(n)
                If _node IsNot Nothing Then
                    Return _node
                End If
            Next
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
        Return Nothing
    End Function

    ''' <lastupdate>
    ''' 24/07/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc build lai tree view
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub BuildTreeChildNode(ByVal nodeParent As RadTreeNode,
                                     ByVal list As List(Of OrganizationDTO),
                                     ByVal bCheck As Boolean,
                                     ByRef bSelected As Boolean)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Dim listTemp As List(Of OrganizationDTO)
        Try
            If bCheck Then
                listTemp = (From t In list
                            Where t.PARENT_ID = Decimal.Parse(nodeParent.Value)).ToList
            Else
                listTemp = (From t In list
                            Where t.PARENT_ID = Decimal.Parse(nodeParent.Value) _
                            And (t.DISSOLVE_DATE Is Nothing Or (t.DISSOLVE_DATE IsNot Nothing And t.DISSOLVE_DATE >= Date.Now)) _
                            And t.ACTFLG = "A").ToList
            End If

            If listTemp.Count = 0 Then
                Exit Sub
            End If
            For index As Integer = 0 To listTemp.Count - 1
                Dim node As New RadTreeNode
                Select Case Common.Common.SystemLanguage.Name
                    Case "vi-VN"
                        node.Text = listTemp(index).CODE & " - " & listTemp(index).NAME_VN
                        node.ToolTip = listTemp(index).NAME_VN
                    Case Else
                        node.Text = listTemp(index).CODE & " - " & listTemp(index).NAME_EN
                        node.ToolTip = listTemp(index).NAME_EN
                End Select

                node.Value = listTemp(index).ID.ToString
                If bCheck Then
                    If listTemp(index).DISSOLVE_DATE IsNot Nothing Then
                        If listTemp(index).DISSOLVE_DATE < Date.Now Then
                            node.BackColor = Drawing.Color.Yellow
                        End If
                    End If
                End If
                If listTemp(index).ACTFLG = "I" Then
                    node.BackColor = Drawing.Color.Yellow
                End If
                If SelectOrgFunction IsNot Nothing Then
                    If node.Value = SelectOrgFunction Then
                        node.Selected = True
                        bSelected = True
                    End If
                End If
                nodeParent.Nodes.Add(node)
                BuildTreeChildNode(node, list, bCheck, bSelected)
            Next
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 24/07/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc lay du lieu trong treeview va dua vao textbox
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FillDataByTree()
        Dim orgItem As OrganizationDTO
        Dim orgItemParent As OrganizationDTO
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            If treeorgfunction.SelectedNode Is Nothing Then
                Exit Sub
            End If
            orgItem = (From p In Organizations Where p.ID = Decimal.Parse(Request("orgid"))).SingleOrDefault

            If orgItem IsNot Nothing Then
                hidParentID.Value = orgItem.PARENT_ID.ToString
                If (orgItem.PARENT_ID IsNot Nothing) Then
                    orgItemParent = (From p In Organizations Where p.ID = Decimal.Parse(orgItem.PARENT_ID)).SingleOrDefault
                    If (orgItemParent IsNot Nothing And orgItemParent.NAME_VN IsNot Nothing) Then
                        txtNameVN.Text = orgItemParent.NAME_VN
                    Else
                        txtNameVN.Text = ""
                    End If
                Else
                    txtNameVN.Text = ""
                End If
                txtCode.Text = orgItem.CODE
                txtNameVN.Text = orgItem.NAME_VN
                txtNameEN.Text = orgItem.NAME_EN
                rdDissolveDate.SelectedDate = orgItem.DISSOLVE_DATE
                rdFoundationDate.SelectedDate = orgItem.FOUNDATION_DATE
                txtAddress.Text = orgItem.ADDRESS
                txtMobile.Text = orgItem.MOBILE
                'If orgItem.STATUS_ID IsNot Nothing Then
                '    cboStatus.SelectedValue = orgItem.STATUS_ID
                'End If
                txtProvinceName.Text = orgItem.PROVINCE_NAME
                rdDate_Business.SelectedDate = orgItem.DATE_BUSINESS
                txtPIT_NO.Text = orgItem.PIT_NO
                txtCostCenterCode.Text = orgItem.COST_CENTER_CODE

                If orgItem.NLEVEL IsNot Nothing Then
                    txtCap.Text = orgItem.NLEVEL
                End If
                ckbnhom.Checked = False
                If orgItem.GROUPPROJECT IsNot Nothing Then
                    ckbnhom.Checked = orgItem.GROUPPROJECT
                End If
                If orgItem.REPRESENTATIVE_ID IsNot Nothing Then
                    hidRepresentative.Value = orgItem.REPRESENTATIVE_ID
                End If
                tr01.Visible = False
                tr02.Visible = False
                If treeorgfunction.SelectedNode.Level = 1 Then
                    tr01.Visible = True
                    tr02.Visible = True
                    ' Lấy logo nếu có
                    Dim logoPath = AppDomain.CurrentDomain.BaseDirectory & "ReportTemplates\Profile\Organization\Logo\"
                    If Not Directory.Exists(logoPath) Then
                        Directory.CreateDirectory(logoPath)
                    End If
                    Dim logo = Request("orgid") & "_*"
                    Dim dirs As String() = Directory.GetFiles(logoPath, logo)
                    If dirs.Length > 0 Then
                        rbiEmployeeImage.Visible = True
                        Using FileStream = File.Open(dirs(0), FileMode.Open)
                            Dim fileContent As Byte() = New Byte(FileStream.Length) {}
                            Using ms As New MemoryStream
                                FileStream.CopyTo(ms)
                                fileContent = ms.ToArray
                            End Using
                            rbiEmployeeImage.DataValue = fileContent
                            rbiEmployeeImage.DataBind()
                        End Using
                    Else
                        rbiEmployeeImage.Visible = False
                    End If

                    ' Lấy file đính kèm
                    Dim licensePath = AppDomain.CurrentDomain.BaseDirectory & "ReportTemplates\Profile\Organization\License\"
                    If Not Directory.Exists(licensePath) Then
                        Directory.CreateDirectory(licensePath)
                    End If
                    Dim license = Request("orgid") & "_*"
                    dirs = Directory.GetFiles(logoPath, license)
                    If dirs.Length > 0 Then
                        btnDownFile.Visible = True
                    Else
                        btnDownFile.Visible = False
                    End If

                End If
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    ''' <lastupdate>
    ''' 24/07/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc lay ra don vi cha khi them
    ''' </summary>
    ''' <remarks></remarks>
    Private Function GetUpLevelByNode(ByVal node As RadTreeNode) As OrganizationPathDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim strText As String = ""
        Dim strValue As String = ""
        Dim iLevel As Integer
        Dim curNode As RadTreeNode
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            curNode = node
            iLevel = curNode.Level

            For idx = 0 To iLevel
                If idx = 0 Then
                    strValue = curNode.Value
                    strText = curNode.Text
                Else
                    curNode = curNode.ParentNode
                    strValue = curNode.Value + ";" + strValue
                    strText = curNode.Text + ";" + strText
                End If
            Next

            Return New OrganizationPathDTO With {.ID = Decimal.Parse(node.Value),
                                                 .HIERARCHICAL_PATH = strValue,
                                                 .DESCRIPTION_PATH = strText}
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Function

    ''' <lastupdate>
    ''' 24/07/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc lay ra cac don vi con cua treeview
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetDownLevelByNode(ByVal node As RadTreeNode, ByRef lstPath As List(Of OrganizationPathDTO))
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If node.Nodes.Count > 0 Then
                For Each child As RadTreeNode In node.Nodes
                    Dim objPath = GetUpLevelByNode(child)
                    lstPath.Add(objPath)
                    GetDownLevelByNode(child, lstPath)
                Next

            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 24/07/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc lay tat ca cac con cua treeview
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetAllChild(ByVal _orgId As Decimal) As List(Of Decimal)
        Dim query As List(Of Decimal)
        Dim list As List(Of Decimal)
        Dim result As New List(Of Decimal)
        Dim rep As New CommonRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try

            result.Add(_orgId)
            Dim lstOrgs As List(Of OrganizationDTO) = Organizations

            query = (From p In lstOrgs Where p.PARENT_ID = _orgId
                     Select p.ID).ToList
            For Each q As Decimal In query
                list = GetAllChild(q)
                result.InsertRange(0, list)
            Next
            Return result
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Function

    Private Sub cboTitle_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles cboTitle.SelectedIndexChanged
        Dim rep As New ProfileRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim obj = rep.GetPositionID(Decimal.Parse(cboTitle.SelectedValue))
            txtGdBp.Text = obj.MASTER_NAME
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Sub loadDatasource(ByVal strUpload As String)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If strUpload <> "" Then
                txtUploadFile.Text = Down_File
                txtUpload.Text = strUpload
            Else
                strUpload = String.Empty
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub btnDownload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If txtUpload.Text <> "" Then
                Dim strPath_Down As String = Server.MapPath("~/ReportTemplates/Profile/Organization/File" + txtUploadFile.Text + txtUpload.Text)
                'bCheck = True
                ZipFiles(strPath_Down, 2)
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Sub ZipFiles(ByVal path As String, ByVal _ID As Decimal)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim crc As New Crc32()
            'Dim fileNameZip As String
            'fileNameZip = txtUploadFile.Text.Trim
            'Dim file As System.IO.FileInfo = New System.IO.FileInfo(path & fileNameZip)
            Dim file As System.IO.FileInfo = New System.IO.FileInfo(path)
            Response.Clear()
            Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name)
            Response.AddHeader("Content-Length", file.Length.ToString())
            Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document "
            Response.WriteFile(file.FullName)
            Response.End()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            HttpContext.Current.Trace.Warn(ex.ToString())
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
#End Region

End Class