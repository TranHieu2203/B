Imports System.IO
Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlHU_MngProfileSavedNewEdit
    Inherits CommonView
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Protected WithEvents ctrlFindSigner As ctrlFindEmployeePopup
    Public Overrides Property MustAuthorize As Boolean = False
    'author: hongdx
    'EditDate: 21-06-2017
    'Content: Write log time and error
    'asign: hdx
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Public WithEvents AjaxManager As RadAjaxManager
    Public Property AjaxManagerId As String
    Dim _classPath As String = "Profile\Modules\Profile\Business" + Me.GetType().Name.ToString()
#Region "Property"
    Property Upload4Emp As Decimal
        Get
            Return ViewState(Me.ID & "_Upload4Emp")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_Upload4Emp") = value
        End Set
    End Property
    ''' <summary>
    ''' Obj ListComboData
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' 
    Property ListComboData As ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ListComboData")
        End Get
        Set(ByVal value As ComboBoxDataDTO)
            ViewState(Me.ID & "_ListComboData") = value
        End Set
    End Property
    Private Property dtTable As DataTable
        Get
            Return ViewState(Me.ID & "_dtTable")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtTable") = value
        End Set
    End Property
    Private Property dtbImport As DataTable
        Get
            Return PageViewState(Me.ID & "_dtbImport")
        End Get
        Set(ByVal value As DataTable)
            PageViewState(Me.ID & "_dtbImport") = value
        End Set
    End Property
    Public Property dem As Integer
        Get
            Return ViewState(Me.ID & "_dem")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_dem") = value
        End Set
    End Property
    Private Property dtAllowList As DataTable
        Get
            Return PageViewState(Me.ID & "_dtAllowList")
        End Get
        Set(ByVal value As DataTable)
            PageViewState(Me.ID & "_dtAllowList") = value
        End Set
    End Property
    Public Property ColumnImportWelfare As Integer
        Get
            Return ViewState(Me.ID & "_ColumnImportWelfare")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_ColumnImportWelfare") = value
        End Set
    End Property

    ''' <summary>
    ''' Obj WelfareMng
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property WelfareMng As WelfareMngDTO
        Get
            Return ViewState(Me.ID & "_WelfareMngDTO")
        End Get
        Set(ByVal value As WelfareMngDTO)
            ViewState(Me.ID & "_WelfareMngDTO") = value
        End Set
    End Property
    Property Employee_Info As EmployeeDTO
        Get
            Return ViewState(Me.ID & "_Employee_Info")
        End Get
        Set(ByVal value As EmployeeDTO)
            ViewState(Me.ID & "_Employee_Info") = value
        End Set
    End Property
    Property Employee_PL As List(Of Welfatemng_empDTO)
        Get
            Return ViewState(Me.ID & "_Employee_PL")
        End Get
        Set(value As List(Of Welfatemng_empDTO))
            ViewState(Me.ID & "_Employee_PL") = value
        End Set
    End Property

    ''' <summary>
    ''' Obj _Id
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property _Id As Integer
        Get
            Return ViewState(Me.ID & "_Id")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_Id") = value
        End Set
    End Property
    Property checkDelete As Integer
        Get
            Return ViewState(Me.ID & "_checkDelete")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_checkDelete") = value
        End Set
    End Property
    Property Total_money As Decimal
        Get
            Return ViewState(Me.ID & "_Total_money")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_Total_money") = value
        End Set
    End Property
    ''' <summary>
    ''' Obj popupID
    ''' 0 - normal
    ''' 1 - Employee
    ''' 2 - Sign
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
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

    ''' <lastupdate>
    ''' 10/07/2017 10:00
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
            If Not Page.IsPostBack Then
                Refresh()
            End If
            UpdateControlState()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If Request.Params("emp") IsNot Nothing Then
                _Id = Integer.Parse(Request.Params("emp"))
                LoadData()
                rgEmployee.Rebind()

                For Each i As GridItem In rgEmployee.Items
                    i.Edit = True
                Next
                rgEmployee.Rebind()
            End If


        Catch ex As Exception

        End Try
    End Sub
    ''' <lastupdate>
    ''' 10/07/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức khởi tạo dữ liệu cho các control trên trang
    ''' Gọi phương thức khởi tạo 
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        ColumnImportWelfare = 13
        SetGridFilter(rgEmployee)
        AjaxManager = CType(Me.Page, AjaxPage).AjaxManager
        AjaxManagerId = AjaxManager.ClientID
        rgEmployee.AllowCustomPaging = True
        rgEmployee.ClientSettings.EnablePostBackOnRowClick = False
        InitControl()
    End Sub

    ''' <lastupdate>
    ''' 10/07/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức khởi tạo giá trị cho các control trên page
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Me.MainToolBar = tbarMenu
            Common.Common.BuildToolbar(Me.MainToolBar,
                                       ToolbarItem.Save,
                                        ToolbarItem.Seperator)

            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem("CANCEL",
                                                                     ToolbarIcons.Delete,
                                                                     ToolbarAuthorize.Special5,
                                                                     Translate("Trờ về DS")))
            CType(Me.MainToolBar.Items(0), RadToolBarButton).CausesValidation = True
            'Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            'LoadControlPopup()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Try
                If phFindEmp.Controls.Contains(ctrlFindEmployeePopup) Then
                    phFindEmp.Controls.Remove(ctrlFindEmployeePopup)
                    'Me.Views.Remove(ctrlFindEmployeePopup.ID.ToUpper)
                End If
                Select Case isLoadPopup
                    Case 1
                        ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                        ctrlFindEmployeePopup.MustHaveContract = True
                        ctrlFindEmployeePopup.IsOnlyWorkingWithoutTer = True
                        phFindEmp.Controls.Add(ctrlFindEmployeePopup)
                End Select
            Catch ex As Exception
                Throw ex
            End Try
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub




    ''' <lastupdate>
    ''' 10/07/2017 09:40
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức bind dữ liệu cho các combobox
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

#End Region

#Region "Event"

    Private Sub ctrlFindEmployeePopup_CancelClicked(sender As Object, e As System.EventArgs) Handles ctrlFindEmployeePopup.CancelClicked
        isLoadPopup = 0
    End Sub





    ''' <lastupdate>
    ''' 10/07/2017 10:10
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện command tren toolbar khi click vao cac item cua no
    ''' Cac command la luu, huy 
    ''' Cập nhật lại trạng thái các control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim objdata As MngProfileSavedDTO
            Dim objList As New List(Of MngProfileSavedDTO)
            Dim lstemp As New List(Of Welfatemng_empDTO)
            Select Case CType(e.Item, RadToolBarButton).CommandName


                Case CommonMessage.TOOLBARITEM_SAVE
                    objdata = New MngProfileSavedDTO
                    objdata.EMPLOYEE_ID = hidIDEmp.Value
                    Dim dtrgEmployee As DataTable = GetDataFromGrid(rgEmployee)
                    For Each row As DataRow In dtrgEmployee.Rows
                        Dim o As New MngProfileSavedDTO
                        o.EMPLOYEE_ID = hidIDEmp.Value
                        o.DOCUMENT_ID = row("ID").ToString
                        o.DATE_SUBMIT = If(IsDate(row("DATE_SUBMIT")), Date.Parse(row("DATE_SUBMIT")), Nothing)
                        o.IS_SUBMITED = If(row("IS_SUBMITED") = False, 0, -1)
                        o.REMARK = row("REMARK").ToString
                        o.UPLOAD_FILE = row("UPLOAD_FILE").ToString
                        o.MUST_HAVE = row("MUST_HAVE")
                        objList.Add(o)
                    Next
                    objdata.LIST_MNG = objList
                    If CurrentState = CommonMessage.STATE_NEW Then
                        Dim rep As New ProfileBusinessRepository
                        If rep.InsertMngProfileSaved(objdata) Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            ''POPUPTOLINK
                            'Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_MngProfileSaved&group=Business")
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        End If
                        rep.Dispose()
                    ElseIf CurrentState = CommonMessage.STATE_EDIT Then

                        Dim rep As New ProfileBusinessRepository
                        If rep.InsertMngProfileSaved(objdata) Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            ''POPUPTOLINK
                            'Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_MngProfileSaved&group=Business")
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        End If
                        rep.Dispose()
                        'ChangeToolbarState()
                    End If
                Case "CANCEL"
                    ''POPUPTOLINK_CANCEL
                    Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_MngProfileSaved&group=Business")
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
            End Select
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub


    Private Function GetDataFromGrid(ByVal gr As RadGrid) As DataTable
        Dim bsSource As DataTable
        Try
            bsSource = New DataTable()
            For Each Col As GridColumn In gr.Columns
                Dim DataColumn As DataColumn = New DataColumn(Col.UniqueName)
                bsSource.Columns.Add(DataColumn)
            Next
            'coppy data to grid
            For Each Item As GridDataItem In gr.EditItems
                If Item.Display = False Then Continue For
                Dim Dr As DataRow = bsSource.NewRow()
                For Each col As GridColumn In gr.Columns
                    Try
                        If col.UniqueName = "cbStatus" Then
                            If Item.Selected = True Then
                                Dr(col.UniqueName) = 1
                            Else
                                Dr(col.UniqueName) = 0
                            End If
                            Continue For
                        End If
                        If InStr(",DATE_SUBMIT,IS_SUBMITED,REMARK,UPLOAD_FILE,", "," + col.UniqueName + ",") > 0 Then
                            Select Case Item(col.UniqueName).Controls(1).ID.ToString.Substring(0, 2)
                                Case "cb"
                                    Dr(col.UniqueName) = CType(Item.FindControl(Item(col.UniqueName).Controls(1).ID.ToString), RadComboBox).SelectedValue
                                Case "rn"
                                    Dr(col.UniqueName) = CType(Item.FindControl(Item(col.UniqueName).Controls(1).ID.ToString), RadNumericTextBox).Value
                                Case "rt"
                                    Dr(col.UniqueName) = CType(Item.FindControl(Item(col.UniqueName).Controls(1).ID.ToString), RadTextBox).Text.Trim
                                Case "rd"
                                    Dr(col.UniqueName) = CType(Item.FindControl(Item(col.UniqueName).Controls(1).ID.ToString), RadDatePicker).SelectedDate
                                Case "ch"
                                    Dr(col.UniqueName) = CType(Item.FindControl(Item(col.UniqueName).Controls(1).ID.ToString), CheckBox).Checked
                                Case "lb"
                                    Dr(col.UniqueName) = CType(Item.FindControl(Item(col.UniqueName).Controls(1).ID.ToString), LinkButton).Text
                            End Select
                        Else
                            Dr(col.UniqueName) = Item.GetDataKeyValue(col.UniqueName)
                        End If
                    Catch ex As Exception
                        Continue For
                    End Try
                Next
                bsSource.Rows.Add(Dr)
            Next
            bsSource.AcceptChanges()
            Return bsSource
        Catch ex As Exception
        End Try
    End Function


    Private Sub rgEmployee_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles rgEmployee.ItemDataBound
        Try
            If e.Item.Edit Then
                Dim edit = CType(e.Item, GridEditableItem)
                Dim item As GridDataItem = CType(e.Item, GridDataItem)
                'Dim rtxtmoney As New RadNumericTextBox
                'rtxtmoney = CType(edit.FindControl("rnMONEY"), RadNumericTextBox)
                SetDataToGrid_Org(edit)
                edit.Dispose()
                item.Dispose()
            End If
            enableOnGrid()
        Catch ex As Exception

        End Try
    End Sub


    ''' <lastupdate>
    ''' 10/07/2017 10:20
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức xử lý việc load dữ liệu cho các combobox
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetDataCombo()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim rep As New ProfileRepository
        Try

            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 10/07/2017 10:20
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức xử lý việc load dữ liệu cho các textbox
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadData()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Dim rep As New ProfileBusinessRepository
        Try
            Employee_Info = rep.GetEmpSaved(_Id)
            If Not Employee_Info Is Nothing Then
                hidIDEmp.Value = Employee_Info.EMPLOYEE_ID
                txtCode.Text = Employee_Info.EMPLOYEE_CODE
                txtTitleName.Text = Employee_Info.TITLE_NAME_VN
                txtName.Text = Employee_Info.FULLNAME_VN
                txtOrgName.Text = Employee_Info.ORG_NAME


                If checkDelete <> 1 Then
                    Dim repst = New ProfileStoreProcedure
                    dtbImport = repst.Get_List_Document(_Id)
                    'Employee_PL = rep.GetlistWelfareEMP(_Id)
                    'dtbImport = Employee_PL.ToTable()
                End If

            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
#End Region
    Private Sub rgEmployee_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgEmployee.NeedDataSource
        Try
            Dim rep As New ProfileBusinessRepository
            If Request.Params("emp") IsNot Nothing Then
                _Id = Integer.Parse(Request.Params("emp"))
                CurrentState = CommonMessage.STATE_EDIT

            Else
                CurrentState = CommonMessage.STATE_NEW
                If Not IsPostBack Then
                    Employee_PL = New List(Of Welfatemng_empDTO)
                    dtbImport = Employee_PL.ToTable()
                End If
            End If
            rgEmployee.VirtualItemCount = dtbImport.Rows.Count 'Employee_PL.Count
            rgEmployee.DataSource = dtbImport

        Catch ex As Exception

        End Try
    End Sub

    Private Sub SetDataToGrid_Org(ByVal EditItem As GridEditableItem)
        Try
            For Each col As GridColumn In rgEmployee.Columns
                Try
                    'Dim groupid
                    'For Each LINE In Employee_PL
                    '    groupid = LINE.GROUP_ID
                    'Next
                    ' Employee_PL.Find(Function(f) f.EMPLOYEE_CODE = empployee_code)
                    'Dim empployee_code = EditItem.GetDataKeyValue("EMPLOYEE_CODE")
                    'Dim ds = dtbImport.AsEnumerable().ToList()
                    Dim document_id = EditItem.GetDataKeyValue("ID")
                    Dim rowData = (From p In dtbImport Where p("ID") = document_id).ToList
                    If rowData Is Nothing Then
                        Exit Sub
                    End If
                    If InStr(",DATE_SUBMIT,IS_SUBMITED,REMARK,UPLOAD_FILE,", "," + col.UniqueName + ",") > 0 Then
                        Select Case EditItem(col.UniqueName).Controls(1).ID.ToString.Substring(0, 2)
                            Case "ch"
                                Dim radCheckBox As New CheckBox
                                radCheckBox = CType(EditItem.FindControl(EditItem(col.UniqueName).Controls(1).ID.ToString), CheckBox)
                                radCheckBox.ClearValue()
                                radCheckBox.Checked = rowData(0)("IS_SUBMITED")
                            Case "rt"
                                Dim radTextBox As New RadTextBox
                                radTextBox = CType(EditItem.FindControl(EditItem(col.UniqueName).Controls(1).ID.ToString), RadTextBox)
                                radTextBox.ClearValue()
                                radTextBox.Text = rowData(0)("REMARK").ToString
                            Case "rd"
                                Dim radDatePicker As New RadDatePicker
                                radDatePicker = CType(EditItem.FindControl(EditItem(col.UniqueName).Controls(1).ID.ToString), RadDatePicker)
                                radDatePicker.ClearValue()
                                radDatePicker.SelectedDate = rowData(0)("DATE_SUBMIT")
                            Case "lb"
                                Dim radText As New LinkButton
                                radText = CType(EditItem.FindControl(EditItem(col.UniqueName).Controls(1).ID.ToString), LinkButton)
                                radText.Text = rowData(0)("UPLOAD_FILE").ToString
                        End Select
                    Else
                        Continue For
                    End If
                Catch ex As Exception
                    Continue For
                End Try
            Next
        Catch ex As Exception

        End Try
    End Sub

    Private Function ValidateGrid_Emp() As Object
        Dim flag As Boolean = True
        Dim msgError As String = "Bạn chưa nhập đầy đủ thông tin. Vui lòng xem vị trí tô màu đỏ và gợi nhắc ở lưới."
        Try
            For Each items In rgEmployee.Items
                Dim txtMoney = CType(items.FindControl("rnMONEY"), RadNumericTextBox)
                If IsNumeric(txtMoney.Value) = False Then
                    txtMoney.BackColor = System.Drawing.Color.Red
                    txtMoney.ToolTip = "Bạn phải nhập mức thưởng"
                    flag = False
                End If
                Total_money = txtMoney.Value
            Next
        Catch ex As Exception
        End Try
        Dim Tuple As New Tuple(Of Boolean, String)(flag, msgError)
        Return Tuple
    End Function

    Private Sub rgEmployee_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles rgEmployee.ItemCommand
        Dim fileName As String = ""
        Dim chuoi As String = ""
        If e.CommandName = "DeleteFile" Then
            Upload4Emp = e.CommandArgument
            fileName = Server.MapPath("~/ReportTemplates/Profile/SavedProfile/Result")
            'My.Computer.FileSystem.DeleteFile(fileName)
            For Each abc As GridDataItem In rgEmployee.MasterTableView.Items
                If abc.GetDataKeyValue("ID").ToString = Upload4Emp Then
                    Dim txtATTACH_FILE As LinkButton = DirectCast(abc("UPLOAD_FILE").FindControl("lbtnUpload"), LinkButton)
                    Dim chkIS_SUBMITED As CheckBox = DirectCast(abc("IS_SUBMITED").FindControl("chkSubmited"), CheckBox)
                    My.Computer.FileSystem.DeleteFile(fileName + "\" + txtATTACH_FILE.Text)
                    txtATTACH_FILE.Text = ""
                    chkIS_SUBMITED.Checked = False
                    Exit For
                End If
            Next
        End If
        If e.CommandName = "UploadFile" Then
            Upload4Emp = e.CommandArgument
            ctrlUpload1.MaxFileSize = 2097152
            ctrlUpload1.Show()
        End If
        If e.CommandName = "DownloadFile" Then
            Dim control As GridDataItem = CType(e.Item, GridDataItem)
            Dim txtATTACH_FILE As LinkButton = DirectCast(control("UPLOAD_FILE").FindControl("lbtnUpload"), LinkButton)

            Dim url As String = "Download.aspx?" & "ctrlHU_MngProfileSavedNewEdit," & txtATTACH_FILE.Text
            Dim str As String = "window.open('" & url + "');"
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
        End If
    End Sub
    Private Sub ctrlUpload1_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload1.OkClicked
        Dim fileName As String
        Dim check As Decimal
        Dim gtri As String
        Try

            If ctrlUpload1.UploadedFiles.Count > 1 Then
                ShowMessage("Chỉ được chọn 1 file để upload", NotifyType.Error)
                Exit Sub
            Else
                If ctrlUpload1.UploadedFiles.Count > 0 Then
                    Dim file As UploadedFile = ctrlUpload1.UploadedFiles(0)
                    fileName = Server.MapPath("~/ReportTemplates/Profile/SavedProfile/Result")
                    If Not Directory.Exists(fileName) Then
                        Directory.CreateDirectory(fileName)
                    End If
                    If System.IO.File.Exists(fileName + "\" + file.FileName) = True Then
                        check = 1
                        For i As Integer = 1 To 10
                            If System.IO.File.Exists(fileName + "\" + i.ToString + file.FileName) = True Then
                                Continue For
                            Else
                                fileName = System.IO.Path.Combine(fileName, i.ToString + file.FileName)
                                gtri = i.ToString
                                Exit For
                            End If
                        Next
                    Else
                        fileName = System.IO.Path.Combine(fileName, file.FileName)
                    End If

                    file.SaveAs(fileName, True)
                    For Each abc As GridDataItem In rgEmployee.MasterTableView.Items
                        If abc.GetDataKeyValue("ID").ToString = Upload4Emp Then
                            Dim txtATTACH_FILE As LinkButton = DirectCast(abc("UPLOAD_FILE").FindControl("lbtnUpload"), LinkButton)
                            Dim chkIS_SUBMITED As CheckBox = DirectCast(abc("IS_SUBMITED").FindControl("chkSubmited"), CheckBox)
                            If check = 1 Then
                                txtATTACH_FILE.Text = gtri + file.FileName
                            Else
                                txtATTACH_FILE.Text = file.FileName
                            End If
                            chkIS_SUBMITED.Checked = True
                            Exit For
                        End If
                    Next

                Else
                    ShowMessage(Translate("Chưa upload được file"), NotifyType.Error)
                End If

            End If
        Catch ex As Exception
            ShowMessage(Translate("Import bị lỗi"), NotifyType.Error)
        End Try
    End Sub

    Private Sub enableOnGrid()
        Try
            For Each abc As GridDataItem In rgEmployee.MasterTableView.Items
                If abc.GetDataKeyValue("ALLOW_UPLOAD_FILE") = False Then
                    Dim txtATTACH_FILE As RadButton = DirectCast(abc("ID").FindControl("btnUpload"), RadButton)
                    txtATTACH_FILE.Enabled = False
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub

End Class