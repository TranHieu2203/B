Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlHU_JobNewEdit
    Inherits Common.CommonView

    Protected repHF As HistaffFrameworkRepository
    ''' <creator>HongDX</creator>
    ''' <lastupdate>21/06/2017</lastupdate>
    ''' <summary>Write Log</summary>
    ''' <remarks>_mylog, _pathLog, _classPath</remarks>
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Profile\Modules\Profile\List" + Me.GetType().Name.ToString()

#Region "Property"

    ''' <summary>
    ''' Obj Job
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property JobSelect As Decimal
        Get
            Return ViewState(Me.ID & "_Organization")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_Organization") = value
        End Set
    End Property

    Property Parent_ID As Decimal
        Get
            Return ViewState(Me.ID & "_Parent_ID")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_Parent_ID") = value
        End Set
    End Property

    Property cboFunction_ID As Decimal
        Get
            Return ViewState(Me.ID & "_cboFunction_ID")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_cboFunction_ID") = value
        End Set
    End Property

    'Dim lstPosition As New List(Of JobPositionDTO)
    Dim IDSelect As Decimal?
#End Region

#Region "Page"
    ''' <summary>
    ''' Khởi tạo, Load page, load info cho control theo ID
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If Request.Params("isView") IsNot Nothing AndAlso Decimal.Parse(Request.Params("isView")) = 1 Then
                CType(MainToolBar.Items(0), RadToolBarButton).Enabled = False
            End If
            GetParams()
            Refresh()
            UpdateControlState()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' Khởi tạo load control
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            'rgReason.AllowSorting = False
            InitControl()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <summary>
    ''' Load data cho combobox
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            GetDataCombo()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' Khởi tạo, Load menu toolbar
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Me.MainToolBar = tbarMain
            Common.Common.BuildToolbar(Me.MainToolBar,
                                       ToolbarItem.Save,
                                       ToolbarItem.Cancel)
            CType(MainToolBar.Items(0), RadToolBarButton).CausesValidation = True
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' Reset, Load page theo trạng thái page
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New ProfileRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try

            Select Case Message
                Case "UpdateView"
                    CurrentState = CommonMessage.STATE_EDIT
                    Dim obj = rep.GetjobID(Me.JobSelect)
                    If obj IsNot Nothing Then
                        hidID.Value = obj.ID.ToString
                        txtCode.Text = obj.CODE
                        txtNamVN.Text = obj.NAME_VN
                        txtNamEN.Text = obj.NAME_EN
                        If obj.PHAN_LOAI_ID IsNot Nothing Then
                            cboPhanLoai.SelectedValue = obj.PHAN_LOAI_ID
                        End If
                        If IsNumeric(obj.JOB_BAND_ID) Then
                            cboJobband.SelectedValue = obj.JOB_BAND_ID
                        End If
                        If IsNumeric(obj.JOB_FAMILY_ID) Then
                            cboJobFamily.SelectedValue = obj.JOB_FAMILY_ID
                        End If
                        'txtNote.Text = obj.NOTE
                        'txtPurpose.Text = obj.PURPOSE
                        'txtRequest.Text = obj.REQUEST
                        RadEditor1.Content = obj.PURPOSE
                        RadEditor2.Content = obj.REQUEST
                        RadEditor3.Content = obj.NOTE
                        'lstPosition = obj.lstPosition
                        orgTreeList.Enabled = True
                        orgTreeList.Visible = True
                    Else
                        CurrentState = CommonMessage.STATE_REJECT
                        MainToolBar.Items(0).Enabled = False
                        MainToolBar.Items(1).Enabled = False
                        orgTreeList.Enabled = False
                        orgTreeList.Visible = False
                    End If

                Case "InsertView"
                    CurrentState = CommonMessage.STATE_NEW
                    txtCode.Text = rep.AutoGenCode("0", "HU_JOB", "CODE")
                    orgTreeList.Enabled = False
                    orgTreeList.Visible = False
            End Select

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

#End Region

#Region "Event"

    'Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    '    If Not Page.IsPostBack Then
    '        orgTreeList.ExpandToLevel(1)
    '    End If
    'End Sub

    ''' <summary>
    ''' Event Click item trên menu toolbar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim rep As New ProfileRepository
        Dim _filter As New JobDTO
        Dim dtData As New DataTable
        Dim objjob As New JobDTO

        Dim gid As Decimal
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        If txtCode.Text.Trim <> "" Then
                            objjob.CODE = txtCode.Text
                        Else
                            ShowMessage(Translate("Bạn phải nhập mã công việc"), NotifyType.Warning)
                            Exit Sub
                        End If

                        If txtNamVN.Text.Trim <> "" Then
                            objjob.NAME_VN = txtNamVN.Text
                        Else
                            ShowMessage(Translate("Bạn phải nhập tên tiếng Việt"), NotifyType.Warning)
                            Exit Sub
                        End If

                        'If cboPhanLoai.SelectedValue <> "" Then
                        '    objjob.PHAN_LOAI_ID = cboPhanLoai.SelectedValue
                        'Else
                        '    ShowMessage(Translate("Bạn phải chọn nhóm"), NotifyType.Warning)
                        '    Exit Sub
                        'End If

                        If cboJobband.SelectedValue <> "" Then
                            objjob.JOB_BAND_ID = cboJobband.SelectedValue
                        Else
                            'ShowMessage(Translate("Bạn phải chọn cấp bậc"), NotifyType.Warning)
                            'Exit Sub
                        End If

                        If cboJobFamily.SelectedValue <> "" Then
                            objjob.JOB_FAMILY_ID = cboJobFamily.SelectedValue
                            'Else
                            '    ShowMessage(Translate("Bạn phải chọn lĩnh vực"), NotifyType.Warning)
                            '    Exit Sub
                        End If

                        'If txtPurpose.Text.Trim <> "" Then
                        '    objjob.PURPOSE = txtPurpose.Text
                        'Else
                        '    ShowMessage(Translate("Bạn phải nhập mục đích"), NotifyType.Warning)
                        '    Exit Sub
                        'End If

                        'If txtRequest.Text.Trim <> "" Then
                        '    objjob.REQUEST = txtRequest.Text
                        'Else
                        '    objjob.REQUEST = Nothing
                        'End If

                        'If txtNote.Text.Trim <> "" Then
                        '    objjob.NOTE = txtNote.Text
                        'Else
                        '    objjob.NOTE = Nothing
                        'End If
                        If RadEditor1.Content <> "" Then
                            objjob.PURPOSE = RadEditor1.Content
                            'Else
                            '    ShowMessage(Translate("Bạn phải nhập mục đích"), NotifyType.Warning)
                            '    Exit Sub
                        End If

                        If RadEditor2.Content <> "" Then
                            objjob.REQUEST = RadEditor2.Content
                        Else
                            objjob.REQUEST = Nothing
                        End If

                        If RadEditor3.Content <> "" Then
                            objjob.NOTE = RadEditor3.Content
                        Else
                            objjob.NOTE = Nothing
                        End If

                        If txtNamEN.Text.Trim <> "" Then
                            objjob.NAME_EN = txtNamEN.Text
                        End If

                        objjob.ACTFLG = "A"

                        'objjob.lstPosition = lstPosition

                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                If rep.Insertjob(objjob, gid) Then
                                    ''POPUPTOLINK
                                    'Session("COMPLETE") = 1
                                    'Response.Redirect("/Default.aspx?mid=Organize&fid=ctrlHU_JobNewEdit&group=Business")
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                                    Me.JobSelect = gid
                                    HiddenField1.Value = gid
                                    Refresh("UpdateView")
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT
                                objjob.ID = Decimal.Parse(hidID.Value)

                                If rep.Modifyjob(objjob, gid) Then
                                    ''POPUPTOLINK
                                    Session("COMPLETE") = 1
                                    Response.Redirect("/Default.aspx?mid=Organize&fid=ctrlHU_Job&group=Business", False)

                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select


                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    ''POPUPTOLINK_CANCEL
                    Response.Redirect("/Default.aspx?mid=Organize&fid=ctrlHU_Job&group=Business", False)
            End Select
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary>Check sự kiện validate cho combobox tồn tại hoặc ngừng áp dụng</summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cvalCode_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalCode.ServerValidate
        Dim rep As New ProfileRepository
        Dim _validate As New JobDTO
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            'If CurrentState = CommonMessage.STATE_EDIT Then
            'Else
            '    _validate.CODE = txtCode.Text.Trim
            '    args.IsValid = rep.ValidateJobCode(_validate)
            'End If

            'If Not args.IsValid Then
            '    txtCode.Text = rep.AutoGenCode("0", "HU_JOB", "CODE")
            'End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
#End Region

#Region "Custom"
    ''' <summary>
    ''' Get data cho combobox
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetDataCombo()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        'Dim rep As New ProfileRepository
        Try
            Dim dtData As DataTable
            Using rep As New ProfileRepository
                dtData = rep.GetOtherList("HU_TITLE_GROUP", True)
                FillRadCombobox(cboPhanLoai, dtData, "NAME", "ID")

                'repHF = New HistaffFrameworkRepository
                'Dim dtData1 = repHF.ExecuteToDataSet("PKG_OMS_BUSINESS.GET_JOB_BAND", New List(Of Object)({Common.Common.SystemLanguage.Name})).Tables(0)
                Dim dtData1 = rep.GetDataByProcedures(1, 0, "", Common.Common.SystemLanguage.Name)
                If dtData1 IsNot Nothing Then
                    FillRadCombobox(cboJobband, dtData1, "NAME", "ID")
                End If


                dtData = rep.GetOtherList("HU_JOB_FAMILY", True)
                FillRadCombobox(cboJobFamily, dtData, "NAME", "ID")

            End Using

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' Get data theo ID Ma nhan vien
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetParams()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If CurrentState Is Nothing Then
                If Request.Params("ID") IsNot Nothing Then
                    Me.JobSelect = Decimal.Parse(Request.Params("ID"))
                    'End If
                    'If Me.JobSelect IsNot Nothing Then
                    Refresh("UpdateView")
                Else
                    Refresh("InsertView")
                End If
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Protected Sub orgTreeList_NeedDataSource(ByVal sender As Object, ByVal e As TreeListNeedDataSourceEventArgs)
        LoadOrgTreeList()
    End Sub
    Protected Function LoadOrgTreeList()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim rep As New ProfileRepository
            Dim lst As New List(Of JobFunctionDTO)
            Dim IDJob As Decimal
            Dim JobID = Me.JobSelect
            If JobID > 0 Then
                IDJob = Me.JobSelect
            Else
                IDJob = 0
            End If
            lst = rep.GetjobFunctionByJobID(IDJob)
            orgTreeList.DataSource = lst
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            Return False
        End Try
        Return True
    End Function

    Public Class OT_OtherlistByIDDTO
        Public Property NAME As String

    End Class

    Protected Function getIDByNameOT_Otherlist(ByVal name As String) As Decimal
        Dim rep As New ProfileRepository
        Dim obj As New OT_OtherlistByIDDTO
        obj.NAME = name

        Dim ID_FUNC As Decimal

        'repHF = New HistaffFrameworkRepository
        'Dim dtData1 = repHF.ExecuteToDataSet("PKG_OMS_BUSINESS.GET_ID_BY_NAME", New List(Of Object)({obj.NAME})).Tables(0)
        Dim dtData1 = rep.GetDataByProcedures(2, 0, obj.NAME, Common.Common.SystemLanguage.Name)
        If dtData1 IsNot Nothing Then
            ID_FUNC = dtData1.Rows(0)("ID")
        End If

        Return ID_FUNC

    End Function

    Protected Sub RadTreeList1_InsertCommand(ByVal sender As Object, ByVal e As TreeListCommandEventArgs)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim tab As New Hashtable()
            Dim obj As New JobFunctionDTO
            Dim rep As New ProfileRepository
            Dim gid As Decimal

            Dim item As TreeListEditableItem = TryCast(e.Item, TreeListEditableItem)


            Dim cboFunc As RadComboBox = item.FindControl("FUNCTION_NAMEEditor")
            Dim Vcbo = cboFunc.SelectedValue

            item.ExtractValues(tab)

            ConvertEmptyValuesToDBNull(tab)

            If tab("NAME").ToString.Trim() <> "" Then
                obj.NAME = tab("NAME").ToString
            Else
                ShowMessage("Bạn phải nhập tên chức năng", NotifyType.Warning)
                Exit Sub
            End If

            If tab("NAME_EN").ToString.Trim() <> "" Then
                obj.NAME_EN = tab("NAME_EN").ToString
            Else
                obj.NAME_EN = ""
            End If

            If Vcbo <> "" Then
                Dim FUNC = getIDByNameOT_Otherlist(Vcbo)
                If FUNC > 0 Then
                    obj.FUNCTION_ID = FUNC
                End If
            Else
                obj.FUNCTION_ID = Nothing
            End If

            obj.JOB_ID = Decimal.Parse(hidID.Value)

            If tab("PARENT_ID").ToString <> "" Then
                obj.PARENT_ID = Decimal.Parse(tab("PARENT_ID"))
            Else
                obj.PARENT_ID = 0
            End If

            If rep.InsertjobFunction(obj, gid) Then
                orgTreeList_NeedDataSource(Nothing, Nothing)
                Dim txtNAME = orgTreeList.GetColumn("FUNCTION_NAME")
                txtNAME.Visible = True
            Else
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
            End If
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Protected Sub RadTreeList1_UpdateCommand(ByVal sender As Object, ByVal e As TreeListCommandEventArgs)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim tab As New Hashtable()
            Dim obj As New JobFunctionDTO
            Dim rep As New ProfileRepository
            Dim gid As Decimal

            Dim item As TreeListEditableItem = TryCast(e.Item, TreeListEditableItem)

            Dim cboFunc As RadComboBox = item.FindControl("FUNCTION_NAMEEditor")
            Dim Vcbo = cboFunc.SelectedValue

            item.ExtractValues(tab)

            ConvertEmptyValuesToDBNull(tab)

            If tab("NAME").ToString().Trim() <> "" Then
                obj.NAME = tab("NAME").ToString().Trim()
            Else
                ShowMessage("Bạn phải nhập tên chức năng", NotifyType.Warning)
                Exit Sub
            End If

            If tab("NAME_EN").ToString.Trim() <> "" Then
                obj.NAME_EN = tab("NAME_EN").ToString
            Else
                obj.NAME_EN = ""
            End If

            If Vcbo <> "" Then
                Dim FUNC = getIDByNameOT_Otherlist(Vcbo)
                If FUNC > 0 Then
                    obj.FUNCTION_ID = FUNC
                End If
            Else
                obj.FUNCTION_ID = Nothing
            End If

            obj.ID = Decimal.Parse(tab("ID"))

            obj.JOB_ID = Decimal.Parse(hidID.Value)

            If tab("PARENT_ID") <> "" Then
                obj.PARENT_ID = Decimal.Parse(tab("PARENT_ID"))
            Else
                obj.PARENT_ID = 0
            End If

            If rep.ModifyjobFunction(obj, gid) Then
                orgTreeList_NeedDataSource(Nothing, Nothing)
                Dim txtNAME = orgTreeList.GetColumn("FUNCTION_NAME")
                txtNAME.Visible = True
            Else
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
            End If
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Protected Sub RadTreeList1_DeleteCommand(ByVal sender As Object, ByVal e As TreeListCommandEventArgs)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim tab As New Hashtable()
            Dim obj As New JobFunctionDTO
            Dim rep As New ProfileRepository

            Dim lst As New List(Of Decimal)

            Dim item As TreeListDataItem = TryCast(e.Item, TreeListDataItem)

            Dim IDDel As String = item.GetDataKeyValue("ID").ToString()

            'repHF = New HistaffFrameworkRepository
            'Dim dtData1 = repHF.ExecuteToDataSet("PKG_OMS_BUSINESS.CHECK_FUNCTION_EXIST", New List(Of Object)({Decimal.Parse(item.GetDataKeyValue("ID"))})).Tables(0)
            Dim dtData1 = rep.GetDataByProcedures(3, Decimal.Parse(item.GetDataKeyValue("ID")), "", Common.Common.SystemLanguage.Name)
            If dtData1 IsNot Nothing Then
                If dtData1.Rows.Count > 0 Then
                    ShowMessage("Bạn không thể xóa chức năng có dữ liệu con. Thao tác thực hiện không thành công", NotifyType.Warning)
                    Exit Sub
                End If
            End If

            lst.Add(IDDel)
            rep.DeletejobFunction(lst)
            orgTreeList_NeedDataSource(Nothing, Nothing)

        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub ConvertEmptyValuesToDBNull(ByVal values As Hashtable)
        Dim keysToDbNull As New List(Of Object)()

        For Each entry As DictionaryEntry In values
            If entry.Value Is Nothing OrElse (TypeOf entry.Value Is [String] AndAlso [String].IsNullOrEmpty(DirectCast(entry.Value, [String]))) Then
                keysToDbNull.Add(entry.Key)
            End If
        Next

        For Each key As Object In keysToDbNull
            values(key) = DBNull.Value
        Next
    End Sub

    Private Sub orgTreeList_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.TreeListItemDataBoundEventArgs) Handles orgTreeList.ItemDataBound

        If TypeOf e.Item Is TreeListDataItem Then
            Dim dataItem = CType(e.Item, TreeListDataItem)
            If dataItem.HierarchyIndex.NestedLevel <> 0 Then
                dataItem("InsertCommandColumn").Controls(0).Visible = False
            End If
            If (dataItem.DataItem.FUNCTION_NAME Is Nothing) Then
                dataItem("NAME").CssClass = "NodeFolderContent"
                e.Item.CssClass = "NodeFolderContent1"
            Else
                If dataItem.DataItem.FUNCTION_NAME.Replace("&nbsp;", "").Trim() <> "" Then
                    dataItem("NAME").CssClass = "NodeFileContent"
                    e.Item.CssClass = "NodeFileContent1"
                Else
                    dataItem("NAME").CssClass = "NodeFolderContent"
                    e.Item.CssClass = "NodeFolderContent1"
                End If
            End If

        End If

    End Sub

    Private Sub jobPosTreeList_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.TreeListCommandEventArgs) Handles orgTreeList.ItemCommand
        Dim tab As New Hashtable()
        Dim item As TreeListEditableItem = TryCast(e.Item, TreeListEditableItem)

        orgTreeList.InsertIndexes.Clear()
        orgTreeList.IsItemInserted = False
        orgTreeList.Rebind()

        If (item IsNot Nothing) Then
            item.ExtractValues(tab)

            ConvertEmptyValuesToDBNull(tab)

            Select Case e.CommandName.ToUpper
                Case "INITINSERT"
                    If tab("PARENT_ID").ToString <> "" Then
                        Me.Parent_ID = Decimal.Parse(tab("PARENT_ID"))
                    End If
                Case "EDIT"
                    If tab("PARENT_ID").ToString <> "" Then
                        If Decimal.Parse(tab("PARENT_ID")) > 0 Then
                            Me.Parent_ID = Decimal.Parse(tab("PARENT_ID"))
                        Else
                            Me.Parent_ID = -1
                        End If
                    End If
                Case "CANCEL"
                    Me.Parent_ID = -1
                    Dim txtNAME = orgTreeList.GetColumn("FUNCTION_NAME")
                    txtNAME.Visible = True
            End Select
        Else
            Me.Parent_ID = -1
        End If
    End Sub

    Protected Sub RadTreeList1_CreateColumnEditor(ByVal sender As Object, ByVal e As TreeListCreateColumnEditorEventArgs) Handles orgTreeList.CreateColumnEditor
        If Me.Parent_ID >= 0 Then
            Dim rep As New ProfileRepository
            Dim s As DataTable
            s = rep.GetOtherList("FUNCTION", True)

            If e.Column.DataField = "FUNCTION_NAME" Then
                e.Column.Visible = True
                Dim editor As TreeListComboEditor = New TreeListComboEditor(e.Column)
                editor.ComboBox.DataSource = s
                editor.ComboBox.DataTextField = "NAME"
                editor.ComboBox.DataValueField = "NAME"
                e.CustomEditorInitializer = New TreeListCreateCustomEditorDelegate(Function() editor)
            End If
        Else
            Dim a As DataTable
            If e.Column.DataField = "FUNCTION_NAME" Then
                e.Column.Visible = True
                Dim editor As TreeListComboEditor = New TreeListComboEditor(e.Column)
                editor.ComboBox.DataSource = a
                editor.ComboBox.DataTextField = "NAME"
                editor.ComboBox.DataValueField = "NAME"
                e.CustomEditorInitializer = New TreeListCreateCustomEditorDelegate(Function() editor)
                e.Column.Visible = False
            End If
        End If
    End Sub

    Public Class TreeListComboEditor
        Inherits TreeListColumnEditor
        Public Sub New(ByVal column As TreeListEditableColumn)
            MyBase.New(column)
            InitializeCombo()
        End Sub

        Public Property ComboBox() As RadComboBox
            Get
                Return m_ComboBox
            End Get
            Private Set(ByVal value As RadComboBox)
                m_ComboBox = value
            End Set
        End Property
        Private m_ComboBox As RadComboBox

        Protected Overridable Sub InitializeCombo()
            ComboBox = New RadComboBox()
            ComboBox.ID = MyBase.GenerateControlID()
        End Sub

        Public Overrides Function GetValues() As IEnumerable
            Return (ComboBox.SelectedValue)
        End Function

        Public Overrides Sub Initialize(ByVal editItem As TreeListEditableItem, ByVal container As Control)
            container.Controls.Add(ComboBox)
        End Sub

        Public Overrides Sub SetValues(ByVal values As IEnumerable)
            Dim value As Object = TreeListColumnEditor.GetFirstValueFromEnumerable(values)
            If value IsNot Nothing Then
                ComboBox.SelectedValue = value.ToString()
            End If
        End Sub
    End Class

#End Region

End Class
