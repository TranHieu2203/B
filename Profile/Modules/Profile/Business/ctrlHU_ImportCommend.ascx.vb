Imports Common
Imports Common.Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog
Public Class ctrlHU_ImportCommend
    Inherits Common.CommonView
    Protected st As New ProfileStoreProcedure
    'author: hongdx
    'EditDate: 21-06-2017
    'Content: Write log time and error
    'asign: hdx
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Profile\Modules\Profile\Business" + Me.GetType().Name.ToString()
#Region "Properties"
    Property CommendList As List(Of CommendListDTO)
        Get
            Return PageViewState(Me.ID & "_CommendList")
        End Get
        Set(ByVal value As List(Of CommendListDTO))
            PageViewState(Me.ID & "_CommendList") = value
        End Set
    End Property
    Private Property dtbImportList As DataTable
        Get
            Return CType(PageViewState(Me.ID & "_dtbImportList"), DataTable)
        End Get
        Set(ByVal value As DataTable)
            PageViewState(Me.ID & "_dtbImportList") = value
        End Set
    End Property
    Property EmployeeInfo As EmployeeDTO
        Get
            Return PageViewState(Me.ID & "_EmployeeInfo")
        End Get
        Set(ByVal value As EmployeeDTO)
            PageViewState(Me.ID & "_EmployeeInfo") = value
        End Set
    End Property
    Private Property importTypeCode As String
        Get
            Return CStr(PageViewState(Me.ID & "_importTypeCode"))
        End Get
        Set(ByVal value As String)
            PageViewState(Me.ID & "_importTypeCode") = value
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
    Property List_Org As List(Of CommendOrgDTO)
        Get
            Return ViewState(Me.ID & "_List_Org")
        End Get
        Set(ByVal value As List(Of CommendOrgDTO))
            ViewState(Me.ID & "_List_Org") = value
        End Set
    End Property
#End Region

#Region "Page - Overrides"
    ''' <summary>
    ''' 1. Init - Khởi tạo View
    ''' Việc load dữ liệu ở hàm này sẽ dẫn đến lỗi "null reference object not set to an instance"
    ''' Nên việc Init này chỉ xử lý nếu là lần đầu Load View => Check Not IsPostBack thì mới thực hiện
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Me.MainToolBar = tbarMain


            If Not IsPostBack Then 'Khời tạo lần đầu LoadView
                BuildToolbar(Me.MainToolBar, ToolbarItem.Save, _
                                             ToolbarItem.Seperator, _
                                             ToolbarItem.Import, _
                                             ToolbarItem.ExportTemplate)
                tbarMain.Items(3).Text = Translate("Xuất file mẫu")
                ctrlOrganization.LoadDataAfterLoaded = True
                ctrlOrganization.OrganizationType = OrganizationType.OrganizationLocation
                'cbbPeriodSelectedValue = String.Empty


                'importTypeCode = "IMPORT_ADD"
                rgvEmployees.SetFilter()
            End If

            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    ''' <summary>
    ''' Lần đầu load trang thì sẽ BindData ở bước thứ 2: Vì hàm ở lớp cha đã check IsPostBack
    ''' Nhưng từ lần PostBack thứ 2 thì không tự thực hiện hàm này nữa
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            LoadCombobox()
            ctrlOrganization.OrganizationType = OrganizationType.OrganizationLocation
            ctrlOrganization.CheckBoxes = TreeNodeTypes.All
            ctrlOrganization.CheckChildNodes = True
            ctrlOrganization.LoadAllOrganization = False
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try



    End Sub
    ''' <summary>
    ''' Load Combobox
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadCombobox()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim rep As New ProfileRepository
        Try
            If ListComboData Is Nothing Then
                ListComboData = New ComboBoxDataDTO
                ListComboData.GET_COMMEND_OBJ = True
                rep.GetComboList(ListComboData)
            End If
            rep.Dispose()
            FillDropDownList(cboCommendObj, ListComboData.LIST_COMMEND_OBJ, "NAME_VN", "ID", Common.Common.SystemLanguage, False)
            cboCommendObj.SelectedIndex = 0
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    ''' <summary>
    ''' Nếu lần đầu load trang thì ViewLoad thực hiện ở bước 3
    ''' Từ lần thứ 2 trở đi sẽ load ở bước thứ 2
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        'Custome execute
    End Sub

    ''' <summary>
    ''' Hàm này chỉ nên set trạng thái các Control, không nên xử lý dữ liệu vì mỗi lần postback nó sẽ nhảy vào hàm này
    ''' từ ViewLoad
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
    End Sub
#End Region

#Region "Event"
    ''' <summary>
    ''' Load data cho grid Hình thức khen thưởng
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgvCommendList_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgvCommendList.NeedDataSource

        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            'Dim rep As New ProfileRepository
            'CommendList = rep.GetListCommendList("A")

            Dim data As New DataTable
            data = st.GET_COMMEND_LIST_IMPORT(cboCommendObj.SelectedValue)

            rgvCommendList.DataSource = data

            ' rgvCommendList.Rebind()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            ShowMessage(ex.Message, Utilities.NotifyType.Error)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <summary>
    ''' Load data cho grid Danh sách nhân viên
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgvEmployees_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgvEmployees.NeedDataSource
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            FillListEmployee()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            ShowMessage(ex.Message, Utilities.NotifyType.Error)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' Load data cho grid Danh sách nhân viên hoặc tổ chức được chọn xét khen thưởng
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgvDataPrepare_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgvDataPrepare.NeedDataSource
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            DesginGridViewDataPrepare()
            FillGridViewDataPrepare(Nothing, False)
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            ShowMessage(ex.Message, Utilities.NotifyType.Error)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' Event selected Node trong treeview sơ đồ tổ chức
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlOrganization_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrganization.SelectedNodeChanged
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            hidOrg.Value = ctrlOrganization.CurrentValue
            If cboCommendObj.SelectedIndex = 0 Then
                FillListEmployee()
                rgvEmployees.DataBind()
            Else

            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            ShowMessage(ex.Message, Utilities.NotifyType.Error)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' Event Click chọn row trên grid Hình thức khen thưởng
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub rgvCommendList_CheckBoxRowSelection(ByVal sender As Object, ByVal e As EventArgs)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            GridViewRowSelection(sender, rgvCommendList, "ImportList_IS_SELECTED", "ImportList_HEADER_IS_SELECTED")
            SelectImportList()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            ShowMessage(ex.Message, Utilities.NotifyType.Error)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' Event click check All grid Hình thức khen thưởng
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub rgvCommendList_HeaderCheckBoxSelection(ByVal sender As Object, ByVal e As EventArgs)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            GridViewHeaderSelection(sender, rgvCommendList, "ImportList_IS_SELECTED")
            SelectImportList()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            ShowMessage(ex.Message, Utilities.NotifyType.Error)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' Event Click button chọn
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSelect_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelect.Click
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If cboCommendObj.SelectedIndex = 0 Then
                SelectEmployees()
            Else
                SelectOrg()
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            ShowMessage(ex.Message, Utilities.NotifyType.Error)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' Event button click bỏ chọn
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnDeSelect_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDeSelect.Click
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If cboCommendObj.SelectedIndex = 0 Then
                DeSelectEmployees()
            Else
                DeSelectOrg()
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            ShowMessage(ex.Message, Utilities.NotifyType.Error)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' Event click item trên menu toolbar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub rtbMain_ButtonClick(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE
                    If rdCommend.SelectedDate Is Nothing Then
                        ShowMessage(Translate("Vui lòng chọn ngày xét thưởng"), NotifyType.Warning)
                        Exit Sub
                    End If
                    SaveDataPrepareToDatabase()
                Case CommonMessage.TOOLBARITEM_EXPORT_TEMPLATE
                    ExportToExcel()
                Case CommonMessage.TOOLBARITEM_IMPORT
                    ctrlUpload1.AllowedExtensions = "xls,xlsx"
                    ctrlUpload1.Show()
            End Select
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            ShowMessage(ex.Message, Utilities.NotifyType.Error)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' Event click button Hoàn tất trên form popup khi click nhập excel
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlUpload1_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload1.OkClicked
        Import()
    End Sub
    ''' <summary>
    ''' Event Click button Xem trên tab: Dữ liệu đã nhập
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnShow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnShow.Click
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If rdCommendView.SelectedDate Is Nothing Then
                ShowMessage(Translate("Vui lòng chọn ngày xét thưởng muốn xem "), NotifyType.Warning)
                Exit Sub
            End If

            FillGridViewDataImported()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
#End Region

#Region "Custom"
    ''' <summary>
    ''' Load data Danh sách nhân viên cho grid Danh sách nhân viên
    ''' </summary>
    ''' <param name="isFull"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function FillListEmployee(Optional ByVal isFull As Boolean = False) As DataTable
        Dim EmployeeList As List(Of EmployeeDTO)
        Dim MaximumRows As Integer
        Dim Sorts As String
        Dim _filter As New EmployeeDTO
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try

            Using rep As New ProfileBusinessRepository

                If ctrlOrganization.CurrentValue IsNot Nothing Then
                    _filter.ORG_ID = Decimal.Parse(ctrlOrganization.CurrentValue)
                Else
                    rgvEmployees.DataSource = New List(Of EmployeeDTO)
                    Exit Function
                End If
                Dim _param = New ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrganization.CurrentValue),
                                                .IS_DISSOLVE = ctrlOrganization.IsDissolve}


                SetValueObjectByRadGrid(rgvEmployees, _filter)
                _filter.MustHaveContract = True
                Sorts = rgvEmployees.MasterTableView.SortExpressions.GetSortString()
                If isFull Then
                    If Sorts IsNot Nothing Then
                        Return rep.GetListEmployeePaging(_filter, _param, Sorts).ToTable()
                    Else
                        Return rep.GetListEmployeePaging(_filter, _param).ToTable()
                    End If
                Else
                    If Sorts IsNot Nothing Then
                        EmployeeList = rep.GetListEmployeePaging(_filter, rgvEmployees.CurrentPageIndex, rgvEmployees.PageSize, MaximumRows, _param, Sorts)
                    Else
                        EmployeeList = rep.GetListEmployeePaging(_filter, rgvEmployees.CurrentPageIndex, rgvEmployees.PageSize, MaximumRows, _param)
                    End If

                    rgvEmployees.VirtualItemCount = MaximumRows
                    rgvEmployees.DataSource = EmployeeList

                    'rgvEmployees.Rebind()
                End If


            End Using
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Function

    ''' <summary>
    ''' Design GridView cho PageView1
    ''' </summary>
    ''' <param name="idRowItems">Id Checkbox items ở GridView ImportList</param>
    ''' <remarks></remarks>
    ''' 
    Private Sub DesginGridViewDataPrepare(Optional ByVal idRowItems As String = "ImportList_IS_SELECTED")
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            rgvDataPrepare.MasterTableView.Columns.Clear()
            'Gán UniqueName để khi import có thể rename ColumnName của Datatable của dữ liệu import
            Dim rColSelection = New GridClientSelectColumn
            rgvDataPrepare.MasterTableView.Columns.Add(rColSelection)
            rColSelection.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
            rColSelection.HeaderStyle.Width = 25
            rColSelection.UniqueName = "X"
            rColSelection.ItemStyle.HorizontalAlign = HorizontalAlign.Center

            If cboCommendObj.SelectedIndex = 0 Then
                '1. Column cố định        

                Dim rCol As GridBoundColumn
                rCol = New GridBoundColumn()
                rgvDataPrepare.MasterTableView.Columns.Add(rCol)
                rCol.DataField = "EMPLOYEE_ID"
                rCol.UniqueName = "EMPLOYEE_ID"
                rCol.HeaderText = Translate("ID nhân viên")
                rCol.ReadOnly = True
                rCol.Visible = False
                rCol.EmptyDataText = "0"

                rCol = New GridBoundColumn()
                rgvDataPrepare.MasterTableView.Columns.Add(rCol)
                rCol.DataField = "EMPLOYEE_CODE"
                rCol.UniqueName = "EMPLOYEE_CODE"
                rCol.HeaderText = Translate("Mã nhân viên")
                rCol.ReadOnly = True
                rCol.Visible = False
                rCol.EmptyDataText = "0"

                rCol = New GridBoundColumn()
                rgvDataPrepare.MasterTableView.Columns.Add(rCol)
                rCol.DataField = "FULLNAME_VN"
                rCol.UniqueName = "FULLNAME_VN"
                rCol.HeaderText = Translate("Tên nhân viên")
                rCol.HeaderStyle.Width = 150
                rCol.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                rCol.AllowFiltering = False
                rCol.AllowSorting = False
                rCol.ReadOnly = True

                rCol = New GridBoundColumn()
                rgvDataPrepare.MasterTableView.Columns.Add(rCol)
                rCol.DataField = "ID_NO"
                rCol.UniqueName = "ID_NO"
                rCol.HeaderText = Translate("CMND")
                rCol.HeaderStyle.Width = 80
                rCol.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                rCol.AllowFiltering = False
                rCol.AllowSorting = False
                rCol.ReadOnly = True

                rCol = New GridBoundColumn()
                rgvDataPrepare.MasterTableView.Columns.Add(rCol)
                rCol.DataField = "ORG_ID"
                rCol.UniqueName = "ORG_ID"
                rCol.ReadOnly = True
                rCol.Visible = False
                rCol.EmptyDataText = "0"

                rCol = New GridBoundColumn()
                rgvDataPrepare.MasterTableView.Columns.Add(rCol)
                rCol.DataField = "ORG_NAME"
                rCol.UniqueName = "ORG_NAME"
                rCol.HeaderText = Translate("Tên phòng ban")
                rCol.HeaderStyle.Width = 150
                rCol.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                rCol.AllowFiltering = False
                rCol.AllowSorting = False
                rCol.ReadOnly = True
            Else
                Dim rCol = New GridBoundColumn()
                rgvDataPrepare.MasterTableView.Columns.Add(rCol)
                rCol.DataField = "ORG_ID"
                rCol.UniqueName = "ORG_ID"
                rCol.ReadOnly = True
                rCol.Visible = False
                rCol.EmptyDataText = "0"

                rCol = New GridBoundColumn()
                rgvDataPrepare.MasterTableView.Columns.Add(rCol)
                rCol.DataField = "ORG_NAME"
                rCol.UniqueName = "ORG_NAME"
                rCol.HeaderText = Translate("Tên phòng ban")
                rCol.HeaderStyle.Width = 150
                rCol.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                rCol.AllowFiltering = False
                rCol.AllowSorting = False
                rCol.ReadOnly = True
            End If


            '2. Add các cột được check ở Import List
            Dim rColEdit As GridNumericColumn
            For Each dataItem As GridDataItem In rgvCommendList.MasterTableView.Items
                If TryCast(dataItem.FindControl(idRowItems), CheckBox).Checked Then 'Nếu có check
                    Dim columnCode As String = dataItem("CODE").Text
                    rColEdit = New GridNumericColumn()
                    rgvDataPrepare.MasterTableView.Columns.Add(rColEdit)
                    rColEdit.DataField = columnCode 'DataField của cột trong GridView DataPrepare là ID khen thưởng Import
                    rColEdit.UniqueName = columnCode
                    rColEdit.HeaderText = Translate(dataItem("NAME").Text) 'Header là Name khen thưởng Import
                    rColEdit.HeaderStyle.Width = 150
                    rColEdit.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    rColEdit.AllowFiltering = False
                    rColEdit.NumericType = NumericType.Number
                    ' rColEdit.MaxLength = 5  'ThanhNT thêm (Cho phép nhập tối đa 5 số)

                    'rColEdit.DataFormatString = "{0:### ##0.0}"
                    rColEdit.DecimalDigits = 5
                    rColEdit.AllowSorting = False
                    rColEdit.EmptyDataText = String.Empty

                End If
            Next
            rgvDataPrepare.MasterTableView.EditMode = GridEditMode.InPlace
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <summary>
    ''' Load data cho grid Danh sách nhân viên, tổ chức được chọn xét khen thưởng
    ''' </summary>
    ''' <param name="dtb"></param>
    ''' <param name="isDataBind"></param>
    ''' <remarks></remarks>
    Private Sub FillGridViewDataPrepare(ByVal dtb As DataTable, Optional ByVal isDataBind As Boolean = True)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If dtb Is Nothing Then
                dtb = CreateDataTableDataPrepare()
            End If

            rgvDataPrepare.DataSource = dtb
            rgvDataPrepare.PageSize = CInt(IIf(dtb.Rows.Count = 0, 100, dtb.Rows.Count))
            If isDataBind Then
                Try
                    rgvDataPrepare.DataBind()
                Catch ex As Exception
                    If ex.ToString.Contains("The string was not recognized as a valid format.") Then
                        ShowMessage(Translate("Sai kiểu dữ liệu, kiểm tra lại file"), NotifyType.Error)
                    End If
                End Try

            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <summary>
    ''' Tự động create thêm cột, load lại grid Danh sách được chọn khi click vào grid Hình thức khen thưởng
    ''' </summary>
    ''' <param name="tableName"></param>
    ''' <param name="idRowItems"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CreateDataTableDataPrepare(Optional ByVal tableName As String = "table", Optional ByVal idRowItems As String = "ImportList_IS_SELECTED") As DataTable
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim dtb As New DataTable
        Try
            Dim columnCode As String = String.Empty
            Dim columnName As String = String.Empty

            'Các cột đi theo thứ tự của các cột trong store 

            dtb.TableName = tableName


            If cboCommendObj.SelectedIndex = 0 Then
                dtb.Columns.Add("EMPLOYEE_ID", GetType(String))
                dtb.Columns("EMPLOYEE_ID").Caption = "ID nhân viên"
                dtb.Columns.Add("EMPLOYEE_CODE", GetType(String))
                dtb.Columns("EMPLOYEE_CODE").Caption = "Mã nhân viên"
                dtb.Columns.Add("FULLNAME_VN", GetType(String))
                dtb.Columns("FULLNAME_VN").Caption = "Tên nhân viên"
                dtb.Columns.Add("ID_NO", GetType(String))
                dtb.Columns("ID_NO").Caption = "CMND"
                dtb.Columns.Add("ORG_ID", GetType(Integer))
                dtb.Columns("ORG_ID").Caption = "Mã phòng ban"
                dtb.Columns.Add("ORG_NAME", GetType(String)) ' Org Name
                dtb.Columns("ORG_NAME").Caption = "Tên phòng ban"
            Else
                dtb.Columns.Add("ORG_ID", GetType(String))
                dtb.Columns("ORG_ID").Caption = "ORG_ID"
                dtb.Columns.Add("ORG_NAME", GetType(String))
                dtb.Columns("ORG_NAME").Caption = "ORG_NAME"
            End If


            'Add các cột được check ở Import List
            For Each dataItem As GridDataItem In rgvCommendList.MasterTableView.Items
                If TryCast(dataItem.FindControl(idRowItems), CheckBox).Checked Then 'Nếu có check
                    columnCode = dataItem("CODE").Text
                    dtb.Columns.Add(columnCode)
                    dtb.Columns(columnCode).Caption = dataItem("NAME").Text
                    'dtb.Columns.Add(columnCode + "NOTE")
                    'dtb.Columns(columnCode + "NOTE").Caption = dataItem("NAME").Text
                End If
            Next

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
        Return dtb
    End Function
#Region "Xử lý sự kiện check rows trong GridView"
    ''' <summary>
    ''' Item Column Checkbox Events
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>


    ''' <summary>
    ''' Header Column CheckBox selection
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>


    ''' <summary>
    ''' Xử lý sự kiện check vào CheckBox ở từng row
    ''' </summary>
    ''' <param name="sender">sender của sự kiện Handler trực tiếp từ events</param>
    ''' <param name="rgv">Id gridView</param>
    ''' <param name="idRowItems">id Checkbox trong từng row item</param>
    ''' <param name="idHeaderColumnCheckBox">id Checkbox ở Column Header</param>
    ''' <remarks></remarks>
    Private Sub GridViewRowSelection(ByVal sender As Object, ByVal rgv As Telerik.Web.UI.RadGrid, ByVal idRowItems As String, ByVal idHeaderColumnCheckBox As String)
        TryCast(TryCast(sender, CheckBox).NamingContainer, GridItem).Selected = TryCast(sender, CheckBox).Checked
        Dim checkHeader As Boolean = True
        For Each dataItem As GridDataItem In rgv.MasterTableView.Items
            If Not TryCast(dataItem.FindControl(idRowItems), CheckBox).Checked Then
                checkHeader = False
                Exit For
            End If
        Next
        Dim headerItem As GridHeaderItem = TryCast(rgv.MasterTableView.GetItems(GridItemType.Header)(0), GridHeaderItem)
        TryCast(headerItem.FindControl(idHeaderColumnCheckBox), CheckBox).Checked = checkHeader
    End Sub

    ''' <summary>
    ''' Xử lý sự kiện check vào CheckBox ở Header
    ''' </summary>
    ''' <param name="sender">sender của sự kiện Handler trực tiếp từ events</param>
    ''' <param name="rgv">Id gridView</param>
    ''' <param name="idRowItems">id Checkbox trong từng row item</param>
    ''' <remarks></remarks>
    Private Sub GridViewHeaderSelection(ByVal sender As Object, ByVal rgv As Telerik.Web.UI.RadGrid, ByVal idRowItems As String)
        Dim headerCheckBox As CheckBox = TryCast(sender, CheckBox)
        For Each dataItem As GridDataItem In rgv.MasterTableView.Items
            TryCast(dataItem.FindControl(idRowItems), CheckBox).Checked = headerCheckBox.Checked
            dataItem.Selected = headerCheckBox.Checked
        Next
    End Sub
    ''' <summary>
    ''' Xử lý sự kiện khi select Khoản Import thì đổ lại dữ liệu ở GridView DataPrepare
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SelectImportList()
        Dim dtb As New DataTable
        dtb = CreateDataTableDataPrepare()

        For Each emp As GridDataItem In rgvDataPrepare.EditItems
            Dim dr As DataRow
            dr = dtb.NewRow()
            If cboCommendObj.SelectedIndex = 0 Then
                'dr("EMPLOYEE_ID") = emp.GetDataKeyValue("EMPLOYEE_ID")
                dr("EMPLOYEE_ID") = CType(emp.Item("EMPLOYEE_ID").Controls(0), TextBox).Text
                dr("EMPLOYEE_CODE") = CType(emp.Item("EMPLOYEE_CODE").Controls(0), TextBox).Text
                dr("FULLNAME_VN") = CType(emp.Item("FULLNAME_VN").Controls(0), TextBox).Text
                dr("ID_NO") = CType(emp.Item("ID_NO").Controls(0), TextBox).Text
                dr("ORG_ID") = CType(emp.Item("ORG_ID").Controls(0), TextBox).Text
                dr("ORG_NAME") = CType(emp.Item("ORG_NAME").Controls(0), TextBox).Text
            Else
                dr("ORG_ID") = CType(emp.Item("ORG_ID").Controls(0), TextBox).Text
                dr("ORG_NAME") = CType(emp.Item("ORG_NAME").Controls(0), TextBox).Text
            End If


            ''Add các cột được check ở Import List
            'For Each dataItem As GridDataItem In rgvCommendList.MasterTableView.Items
            '    'Bỏ chọn Khoản import thì remove data cột tương ứng trong GridView DataPrepare
            '    If TryCast(dataItem.FindControl("ImportList_IS_SELECTED"), CheckBox).Checked Then 'Nếu có check
            '        Dim columnName As String = dataItem("CODE").Text
            '        Try
            '            dr(columnName) = CType(emp.Item(columnName).Controls(0), RadNumericTextBox).Value
            '            'dr(columnName + "NOTE") = CType(emp.Item(columnName + "NOTE").Controls(0), TextBox).Text
            '        Catch ex As Exception
            '            dr(columnName) = String.Empty 'Trường hợp cột chưa có trong GridView Prepare thì default value = 0
            '            ' dr(columnName + "NOTE") = String.Empty
            '            Continue For
            '        End Try

            '    End If
            'Next
            dtb.Rows.Add(dr)
        Next

        DesginGridViewDataPrepare() 'Design lại GridView DataPrepare
        FillGridViewDataPrepare(dtb) 'Fill DataSource
        ReBindGridViewDataPrepare() ' Chuyển sang FormEdit
    End Sub
    Private Sub ReBindGridViewDataPrepare(Optional ByVal IsEdit As Boolean = True)
        '3. Chuyển form Data Prepare thành EditForm với các cột Khoản Import
        If IsEdit Then
            'rgvDataPrepare.MasterTableView.EditMode = GridEditMode.InPlace
            For Each item As GridItem In rgvDataPrepare.MasterTableView.Items
                item.Edit = True
            Next
        Else
            rgvDataPrepare.MasterTableView.ClearEditItems()
        End If

        rgvDataPrepare.Rebind()
    End Sub
#End Region


    ''' <summary>
    ''' Chọn những nhân viên được Check chọn ở GridView Employees
    ''' </summary>
    ''' <param name="idRowItems"></param>
    ''' <remarks></remarks>
    ''' 
    Private Sub SelectEmployees(Optional ByVal idRowItems As String = "ImportList_IS_SELECTED")
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim dtb As New DataTable
            dtb = CreateDataTableDataPrepare()

            '1. Giữ nguyên những employee đang có ở GridView DataPrepare
            For Each emp As GridDataItem In rgvDataPrepare.EditItems
                Dim dr As DataRow
                dr = dtb.NewRow()
                'dr("EMPLOYEE_ID") = emp.GetDataKeyValue("EMPLOYEE_ID")
                dr("EMPLOYEE_ID") = CType(emp.Item("EMPLOYEE_ID").Controls(0), TextBox).Text
                dr("EMPLOYEE_CODE") = CType(emp.Item("EMPLOYEE_CODE").Controls(0), TextBox).Text
                dr("FULLNAME_VN") = CType(emp.Item("FULLNAME_VN").Controls(0), TextBox).Text
                dr("ID_NO") = CType(emp.Item("ID_NO").Controls(0), TextBox).Text
                dr("ORG_ID") = CType(emp.Item("ORG_ID").Controls(0), TextBox).Text
                dr("ORG_NAME") = CType(emp.Item("ORG_NAME").Controls(0), TextBox).Text
                'Add các cột được check ở Import List
                For Each dataItem As GridDataItem In rgvCommendList.MasterTableView.Items
                    If TryCast(dataItem.FindControl(idRowItems), CheckBox).Checked Then 'Nếu có check
                        Dim columnName As String = dataItem("CODE").Text
                        dr(columnName) = CType(emp.Item(columnName).Controls(0), RadNumericTextBox).Value
                    End If
                Next
                dtb.Rows.Add(dr)
            Next

            '2. Thêm những item mới được check chọn từ GridView Employees mà chưa có trong DataPrepare
            For Each emp As GridDataItem In rgvEmployees.SelectedItems
                'Dim employeeId As String = emp.GetDataKeyValue("EMPLOYEE_ID").ToString()
                Dim employeeId As String = emp.Item("EMPLOYEE_ID").Text
                If dtb.Select(String.Format("EMPLOYEE_ID = '{0}'", employeeId)).Length = 0 Then
                    Dim dr As DataRow
                    dr = dtb.NewRow()
                    dr("EMPLOYEE_ID") = emp.Item("EMPLOYEE_ID").Text
                    dr("EMPLOYEE_CODE") = emp.Item("EMPLOYEE_CODE").Text
                    dr("FULLNAME_VN") = emp.Item("FULLNAME_VN").Text
                    dr("ID_NO") = emp.Item("ID_NO").Text
                    dr("ORG_ID") = emp.Item("ORG_ID").Text
                    dr("ORG_NAME") = emp.Item("ORG_NAME").Text
                    'Add các cột được check ở Import List gán value = 0
                    For Each dataItem As GridDataItem In rgvCommendList.MasterTableView.Items
                        If TryCast(dataItem.FindControl(idRowItems), CheckBox).Checked Then 'Nếu có check
                            Dim columnName As String = dataItem("CODE").Text
                            dr(columnName) = String.Empty
                        End If
                    Next
                    dtb.Rows.Add(dr)
                End If
            Next

            FillGridViewDataPrepare(dtb)
            ReBindGridViewDataPrepare()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <summary>
    ''' Process nút chọn
    ''' </summary>
    ''' <param name="idRowItems"></param>
    ''' <remarks></remarks>
    Private Sub SelectOrg(Optional ByVal idRowItems As String = "ImportList_IS_SELECTED")
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim dtb As New DataTable
            dtb = CreateDataTableDataPrepare()

            '1. Giữ nguyên những employee đang có ở GridView DataPrepare
            For Each emp As GridDataItem In rgvDataPrepare.EditItems
                Dim dr As DataRow
                dr = dtb.NewRow()
                dr("ORG_ID") = CType(emp.Item("ORG_ID").Controls(0), TextBox).Text
                dr("ORG_NAME") = CType(emp.Item("ORG_NAME").Controls(0), TextBox).Text
                'Add các cột được check ở Import List
                For Each dataItem As GridDataItem In rgvCommendList.MasterTableView.Items
                    If TryCast(dataItem.FindControl(idRowItems), CheckBox).Checked Then 'Nếu có check
                        Dim columnName As String = dataItem("CODE").Text
                        dr(columnName) = CType(emp.Item(columnName).Controls(0), RadNumericTextBox).Value
                    End If
                Next
                dtb.Rows.Add(dr)
            Next

            '2. Thêm những item mới được check chọn từ GridView Employees mà chưa có trong DataPrepare
            Dim lstOrg As List(Of Common.CommonBusiness.OrganizationDTO) = ctrlOrganization.ListOrg_Checked()
            List_Org = New List(Of CommendOrgDTO)
            Dim org As New CommendOrgDTO
            For Each org_Check As Common.CommonBusiness.OrganizationDTO In lstOrg
                If dtb.Select(String.Format("ORG_ID = '{0}'", org_Check.ID)).Length = 0 Then
                    Dim dr As DataRow
                    dr = dtb.NewRow()
                    dr("ORG_ID") = org_Check.ID.ToString()
                    dr("ORG_NAME") = org_Check.NAME_VN
                    'Add các cột được check ở Import List gán value = 0
                    For Each dataItem As GridDataItem In rgvCommendList.MasterTableView.Items
                        If TryCast(dataItem.FindControl(idRowItems), CheckBox).Checked Then 'Nếu có check
                            Dim columnName As String = dataItem("CODE").Text
                            dr(columnName) = String.Empty
                        End If
                    Next
                    dtb.Rows.Add(dr)
                End If
            Next

            FillGridViewDataPrepare(dtb)
            ReBindGridViewDataPrepare()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <summary>
    ''' Bỏ chọn những nhân viên được Check ở GridView DataPrepare
    ''' Nhân viên nào ĐƯỢC check ở GridView DataPrapare thì giữ lại
    ''' </summary>
    ''' <param name="idRowItems"></param>
    ''' <remarks></remarks>
    ''' 
    Private Sub DeSelectEmployees(Optional ByVal idRowItems As String = "ImportList_IS_SELECTED")
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim dtb As New DataTable
            dtb = CreateDataTableDataPrepare()

            For Each emp As GridDataItem In rgvDataPrepare.EditItems
                If emp.Selected = False Then 'Nếu không được Check thì giữ lại
                    Dim dr As DataRow
                    dr = dtb.NewRow()
                    dr("EMPLOYEE_ID") = CType(emp.Item("EMPLOYEE_ID").Controls(0), TextBox).Text
                    dr("EMPLOYEE_CODE") = CType(emp.Item("EMPLOYEE_CODE").Controls(0), TextBox).Text
                    dr("FULLNAME_VN") = CType(emp("FULLNAME_VN").Controls(0), TextBox).Text
                    dr("ID_NO") = CType(emp("ID_NO").Controls(0), TextBox).Text
                    dr("ORG_ID") = CType(emp("ORG_ID").Controls(0), TextBox).Text
                    dr("ORG_NAME") = CType(emp("ORG_NAME").Controls(0), TextBox).Text
                    'Add các cột được check ở Import List
                    For Each dataItem As GridDataItem In rgvCommendList.MasterTableView.Items
                        If TryCast(dataItem.FindControl(idRowItems), CheckBox).Checked Then 'Nếu có check
                            Dim columnName As String = dataItem("CODE").Text
                            dr(columnName) = CType(emp(columnName).Controls(0), RadNumericTextBox).Value
                        End If
                    Next
                    dtb.Rows.Add(dr)
                End If
            Next

            FillGridViewDataPrepare(dtb)
            ReBindGridViewDataPrepare()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    ''' <summary>
    ''' Process nút bỏ chọn
    ''' </summary>
    ''' <param name="idRowItems"></param>
    ''' <remarks></remarks>
    Private Sub DeSelectOrg(Optional ByVal idRowItems As String = "ImportList_IS_SELECTED")
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim dtb As New DataTable
            dtb = CreateDataTableDataPrepare()

            For Each emp As GridDataItem In rgvDataPrepare.EditItems
                If emp.Selected = False Then 'Nếu không được Check thì giữ lại
                    Dim dr As DataRow
                    dr = dtb.NewRow()
                    dr("ORG_ID") = CType(emp("ORG_ID").Controls(0), TextBox).Text
                    dr("ORG_NAME") = CType(emp("ORG_NAME").Controls(0), TextBox).Text
                    'Add các cột được check ở Import List
                    For Each dataItem As GridDataItem In rgvCommendList.MasterTableView.Items
                        If TryCast(dataItem.FindControl(idRowItems), CheckBox).Checked Then 'Nếu có check
                            Dim columnName As String = dataItem("CODE").Text
                            dr(columnName) = CType(emp(columnName).Controls(0), RadNumericTextBox).Value
                        End If
                    Next
                    dtb.Rows.Add(dr)
                End If
            Next

            FillGridViewDataPrepare(dtb)
            ReBindGridViewDataPrepare()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <summary>
    ''' Export Grid To Excel => phải set EnableAjax = False
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
    Private Sub ExportToExcel(Optional ByVal idRowItems As String = "ImportList_IS_SELECTED")
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            '1. Get dữ có header vào Row 1
            Dim dtb As New DataTable
            dtb = CreateDataTableDataPrepare()

            'Export từng dòng từ DataPrepare ra Excel
            For Each emp As GridDataItem In rgvDataPrepare.EditItems
                Dim dr As DataRow
                dr = dtb.NewRow()
                If cboCommendObj.SelectedIndex = 0 Then
                    dr("EMPLOYEE_ID") = CType(emp.Item("EMPLOYEE_ID").Controls(0), TextBox).Text
                    dr("EMPLOYEE_CODE") = CType(emp.Item("EMPLOYEE_CODE").Controls(0), TextBox).Text
                    dr("FULLNAME_VN") = CType(emp.Item("FULLNAME_VN").Controls(0), TextBox).Text
                    dr("ID_NO") = CType(emp.Item("ID_NO").Controls(0), TextBox).Text
                    dr("ORG_ID") = CType(emp.Item("ORG_ID").Controls(0), TextBox).Text
                    dr("ORG_NAME") = CType(emp.Item("ORG_NAME").Controls(0), TextBox).Text
                Else
                    dr("ORG_ID") = CType(emp.Item("ORG_ID").Controls(0), TextBox).Text
                    dr("ORG_NAME") = CType(emp.Item("ORG_NAME").Controls(0), TextBox).Text
                End If

                'Add các cột được check ở Import List
                For Each dataItem As GridDataItem In rgvCommendList.MasterTableView.Items
                    If TryCast(dataItem.FindControl(idRowItems), CheckBox).Checked Then 'Nếu có check
                        Dim columnCode As String = dataItem("CODE").Text
                        dr(columnCode) = CType(emp.Item(columnCode).Controls(0), RadNumericTextBox).Value
                        'dr(columnCode + "NOTE") = CType(emp.Item(columnCode + "NOTE").Controls(0), TextBox).Text
                    End If
                Next
                dtb.Rows.Add(dr)
            Next

            '2. Export dữ liệu nếu có
            If dtb IsNot Nothing Then
                Using xls As New AsposeExcelCommon
                    xls.ExportExcelByRadGrid(rgvDataPrepare, dtb, "HU_ImportCommendTemplete", Server, Response, "~/ReportTemplates/" & Request.Params("mid") & "/" & "HU_ImportCommendTemplete.xls")
                End Using
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' Lưu dữ liệu
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SaveDataPrepareToDatabase()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim rep As New ProfileBusinessRepository
            '1. Get data
            Dim dtb As New DataTable
            Dim log As New CurrentUserLog
            Dim currentDate As Date = Now
            Dim orgID, employeeId As String
            'Dim OrgImport As String
            Dim importValue As Decimal?
            Dim note As String = String.Empty
            Dim columnCode As String = String.Empty

            orgID = ctrlOrganization.CurrentValue


            dtb = CreateDataTableDataPrepareForSave()
            log = UserLogHelper.GetCurrentLogUser


            Dim lstCommend As New List(Of ImportCommendDTO)
            Dim commend As New ImportCommendDTO



            '3. Dữ liệu mỗi khoản Import sẽ được Insert thành 1 dòng trên Database
            For Each item As GridDataItem In rgvDataPrepare.Items
                For Each imp As GridDataItem In rgvCommendList.Items
                    If TryCast(imp.FindControl("ImportList_IS_SELECTED"), CheckBox).Checked = True Then
                        If cboCommendObj.SelectedIndex = 0 Then
                            employeeId = CType(item("EMPLOYEE_ID").Controls(0), TextBox).Text

                            columnCode = imp("CODE").Text
                            importValue = CType(CType(item(columnCode).Controls(0), RadNumericTextBox).Value, Decimal?)
                            'note = CType(item(columnCode).Controls(0), TextBox).Text

                            If importValue IsNot Nothing Then
                                'nếu dữ liệu nhập khac null hoặc khác 1 thì không cho lưu
                                If importValue <> 1 Then
                                    ShowMessage(Translate("Định dạng lưu trữ bị sai, vui lòng kiểm tra lại !"), Utilities.NotifyType.Warning)
                                    Exit Sub
                                End If

                                Dim dr As DataRow = dtb.NewRow()
                                dr("P_EMPLOYEE_ID") = employeeId
                                dr("P_IMPORT_VALUE") = importValue
                                dr("P_FIELD_DATA_CODE") = imp("CODE").Text
                                dr("P_ORG_ID") = CType(item.Item("ORG_ID").Controls(0), TextBox).Text 'Lấy ORG_ID của nhân viên                        
                                dr("P_CREATED_USER") = log.Username
                                dr("P_CREATED_DATE") = currentDate
                                dr("P_CREATED_LOG") = log.Ip + "-" + log.ComputerName
                                'dr("P_NOTE") = note
                                commend = New ImportCommendDTO
                                commend.COMMEND_DATE = rdCommend.SelectedDate
                                commend.EMPLOYEE_ID = employeeId
                                commend.IMPORT_VALUE = importValue
                                commend.FIELD_DATA_CODE = imp("CODE").Text
                                commend.ORG_ID = CType(item.Item("ORG_ID").Controls(0), TextBox).Text
                                commend.COMMEND_OBJ = cboCommendObj.SelectedValue
                                lstCommend.Add(commend)
                                dtb.Rows.Add(dr)
                            End If
                        Else
                            'OrgImport = item.GetDataKeyValue("EMPLOYEE_ID").ToString()

                            columnCode = imp("CODE").Text
                            importValue = CType(CType(item(columnCode).Controls(0), RadNumericTextBox).Value, Decimal?)
                            'note = CType(item(columnCode).Controls(0), TextBox).Text

                            If importValue IsNot Nothing Then
                                'nếu dữ liệu nhập khac null hoặc khác 1 thì không cho lưu
                                If importValue <> 1 Then
                                    ShowMessage(Translate("Định dạng lưu trữ bị sai, vui lòng kiểm tra lại !"), Utilities.NotifyType.Warning)
                                    Exit Sub
                                End If

                                Dim dr As DataRow = dtb.NewRow()

                                dr("P_IMPORT_VALUE") = importValue
                                dr("P_FIELD_DATA_CODE") = imp("CODE").Text
                                dr("P_ORG_ID") = CType(item.Item("ORG_ID").Controls(0), TextBox).Text 'Lấy ORG_ID của nhân viên                        
                                dr("P_CREATED_USER") = log.Username
                                dr("P_CREATED_DATE") = currentDate
                                dr("P_CREATED_LOG") = log.Ip + "-" + log.ComputerName
                                'dr("P_NOTE") = note
                                commend = New ImportCommendDTO
                                commend.COMMEND_DATE = rdCommend.SelectedDate
                                commend.IMPORT_VALUE = importValue
                                commend.FIELD_DATA_CODE = imp("CODE").Text
                                commend.ORG_ID = CType(item.Item("ORG_ID").Controls(0), TextBox).Text
                                commend.COMMEND_OBJ = cboCommendObj.SelectedValue
                                lstCommend.Add(commend)
                                dtb.Rows.Add(dr)
                            End If
                        End If

                    End If

                Next
            Next

            '4. Save database
            If dtb IsNot Nothing AndAlso dtb.Rows.Count > 0 Then
                If rep.InsertImportCommend(lstCommend) Then
                    'FillGridViewDataImported()
                    ShowMessage(Translate("Dữ liệu lưu trữ thành công!"), Utilities.NotifyType.Success)
                Else
                    ShowMessage(Translate("Dữ liệu lưu trữ thất bại!"), Utilities.NotifyType.Warning)
                End If
            Else
                ShowMessage(Translate("Không có dữ liệu cần lưu trữ!"), Utilities.NotifyType.Information)
            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            ShowMessage(ex.Message, Utilities.NotifyType.Error)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' Create dataTable cho grid Danh sách được chọn
    ''' </summary>
    ''' <param name="tableName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CreateDataTableDataPrepareForSave(Optional ByVal tableName As String = "table") As DataTable
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        'Các cột đi theo thứ tự của các cột trong store 
        Dim dtb As New DataTable
        Try
            dtb.TableName = tableName
            dtb.Columns.Add("P_EMPLOYEE_ID", GetType(String))
            dtb.Columns.Add("P_IMPORT_TYPE_CODE", GetType(String))
            dtb.Columns.Add("P_IMPORT_VALUE", GetType(Decimal))
            dtb.Columns.Add("P_FIELD_DATA_CODE", GetType(String))
            dtb.Columns.Add("P_ORG_ID", GetType(Integer))
            dtb.Columns.Add("P_CREATED_USER", GetType(String))
            dtb.Columns.Add("P_CREATED_DATE", GetType(Date))
            dtb.Columns.Add("P_CREATED_LOG", GetType(String))
            dtb.Columns.Add("P_NOTE", GetType(String))
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

        Return dtb
    End Function
    ''' <summary>
    ''' Fill data được import lên grid khi process xong import
    ''' </summary>
    ''' <param name="isDataBind"></param>
    ''' <remarks></remarks>
    Private Sub FillGridViewDataImported(Optional ByVal isDataBind As Boolean = True)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            '1. Get tham số input để lấy data từ Database
            Dim userName, orgID
            userName = UserLogHelper.GetUsername()
            orgID = ctrlOrganization.CurrentValue

            '2. Check cả 4 tham số phải có dữ liệu. Nếu không thì Create table (DataSource) để fill vào GridView
            Dim dtb As New DataTable
            If orgID IsNot Nothing AndAlso Not userName.Equals(String.Empty) AndAlso Not orgID.Equals(String.Empty) Then
                dtb = st.ReadDataForGridViewDataImported(userName, CInt(orgID), rdCommendView.SelectedDate, cboCommendObj.SelectedIndex, cboCommendObj.SelectedValue)
            Else
                dtb = CreateDataTableDataImported()
            End If

            DesginGridViewDataImported(dtb)

            '3. Fill vào GridView
            If dtb IsNot Nothing Then
                rgvDataImported.PageSize = CInt(IIf(dtb.Rows.Count = 0, 100, dtb.Rows.Count))
                rgvDataImported.DataSource = dtb
                If isDataBind Then
                    rgvDataImported.DataBind()
                End If
            Else
                rgvDataImported.PageSize = 100
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    ''' <summary>
    ''' Tạo DataSource cho GridView DataImported
    ''' </summary>
    ''' <param name="tableName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' 
    Private Function CreateDataTableDataImported(Optional ByVal tableName As String = "table") As DataTable
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        'Các cột đi theo thứ tự của các cột trong store 
        Dim dtb As New DataTable
        Try
            dtb.TableName = tableName
            dtb.Columns.Add("EMPLOYEE_ID", GetType(String))
            dtb.Columns.Add("EMPLOYEE_CODE", GetType(String))
            dtb.Columns.Add("FULLNAME_VN", GetType(String))
            dtb.Columns.Add("ORG_ID", GetType(Integer))
            dtb.Columns.Add("ORG_NAME", GetType(String))

            'Add các cột được check ở Import List
            For Each dataItem As GridDataItem In rgvCommendList.MasterTableView.Items
                Dim columnName As String = dataItem("CODE").Text
                dtb.Columns.Add(columnName)
            Next
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

        Return dtb
    End Function

    ''' <summary>
    ''' Import template commend
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
    Private Sub Import()
        Dim fileName As String
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try

            '1. Đọc dữ liệu từ file Excel
            Dim tempPath As String = ConfigurationManager.AppSettings("ExcelFileFolder")
            Dim savepath = Context.Server.MapPath(tempPath)
            Dim countFile As Integer = ctrlUpload1.UploadedFiles.Count
            Dim ds As New DataSet
            If countFile > 0 Then
                Dim file As UploadedFile = ctrlUpload1.UploadedFiles(countFile - 1)
                fileName = System.IO.Path.Combine(savepath, file.FileName)
                '1.1 Lưu file lên server
                file.SaveAs(fileName, True)

                '2.1 Đọc dữ liệu trong file Excel
                Using ep As New ExcelPackage
                    ds = ep.ReadExcelToDataSet(fileName, True)
                End Using
            End If

            Dim columnNames As New List(Of String)
            If ds IsNot Nothing AndAlso ds.Tables IsNot Nothing Then
                Dim countColumnImport As Integer
                Dim countColumnDataPrepare As Integer
                Try
                    countColumnImport = ds.Tables(0).Columns.Count - 1 '-1 để lấy chỉ số chạy trong vòng For
                Catch ex As Exception
                    ShowMessage(Translate("Bảng dữ liệu chưa có cột hoặc file không đúng định dạng"), NotifyType.Warning)
                    Exit Sub
                End Try
                If ds.Tables(0).Rows.Count = 0 Then
                    ShowMessage(Translate("File không có dữ liệu!"), NotifyType.Warning)
                    Exit Sub
                End If
                countColumnDataPrepare = rgvDataPrepare.MasterTableView.Columns.Count - 2 'Bỏ cột Selection (đầu tiên)



                '2. Check file import với Design hiện tại của GridView Dataprepare
                If countColumnImport = countColumnDataPrepare Then
                    '2.1 Check danh sách nhân viên file import phải thuộc danh sách nhân viên ở GridView Import
                    For Each dr As DataRow In ds.Tables(0).Rows
                        If rgvEmployees.MasterTableView.FindItemByKeyValue("EMPLOYEE_ID", dr(1)) IsNot Nothing Then 'dr(1) là cột Mã nhân viên
                            ShowMessage("File Import có nhân viên không hợp lệ, sử dụng chức năng Xuất excel để lấy file import mẫu", Utilities.NotifyType.Alert)
                            Return
                        End If
                    Next
                Else

                    'lấy danh sách tên column trong file excel
                    columnNames = ds.Tables(0).Columns.Cast(Of DataColumn)().[Select](Function(x) x.ColumnName).ToList()

                    'If columnNames.Count < countColumnDataPrepare Then
                    '    ShowMessage("Vui lòng chọn đúng đối tượng trước khi import", Utilities.NotifyType.Error)
                    '    Exit Sub
                    'End If
                    Try
                        'xoa di hết những cột mặc định bann đầu và giữ lại những cột thêm mới
                        columnNames.RemoveRange(0, countColumnDataPrepare + 1)
                    Catch ex As Exception
                        If ex.ToString.Contains("Offset and length were out of bounds for the array") Then
                            ShowMessage(Translate("File dữ liệu bị thiếu cột!"), NotifyType.Warning)
                            Exit Sub
                        End If
                    End Try
                    'kiểm tra và cỏ check đã chọn trong danh sách khen thưởng
                    For Each dataItem As GridDataItem In rgvCommendList.MasterTableView.Items
                        If TryCast(dataItem.FindControl("ImportList_IS_SELECTED"), CheckBox).Checked Then 'Nếu có check
                            TryCast(dataItem.FindControl("ImportList_IS_SELECTED"), CheckBox).Checked = False
                        End If
                    Next

                    'kiểm tra xem danh sách tên cột trong file excel có khớp với danh sách tên khen thưởng, nếu trùng thì check đã chọn
                    For Each dataItem As GridDataItem In rgvCommendList.Items
                        If columnNames.Any(Function(x) x = dataItem.GetDataKeyValue("NAME")) Then
                            TryCast(dataItem.FindControl("ImportList_IS_SELECTED"), CheckBox).Checked = True

                        End If
                    Next

                    'điển thông tin nhân viên trong file excel vào lưới
                    SelectImportList()

                End If

                Try
                    For j = 0 To ds.Tables(0).Columns.Count - 1 'Reanme column Code => để đưa vào gridview prepayment và sau đó saveDB nếu có
                        ds.Tables(0).Columns(j).ColumnName = rgvDataPrepare.MasterTableView.Columns(j + 1).UniqueName
                    Next
                Catch ex As Exception
                    If ex.ToString.Contains("Failed accessing GridColumn by index") Then
                        ShowMessage(Translate("Không đúng định dạng file, thừa cột dữ liệu"), NotifyType.Warning)
                        Exit Sub
                    End If
                End Try


                '3. Gán dữ liệu vào GridView DataPrepare

                FillGridViewDataPrepare(ds.Tables(0))
                ReBindGridViewDataPrepare()
            Else
                ShowMessage(Translate("Không đúng định dạng file nên không thể đọc dữ liệu từ file"), NotifyType.Error)
                Exit Sub
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            ShowMessage(ex.Message, Utilities.NotifyType.Error)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' Design GridView cho PageView2
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DesginGridViewDataImported(ByVal dtb As DataTable)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            rgvDataImported.MasterTableView.Columns.Clear()
            'lay danh sach column duoi du lieu import
            Dim listColumn = dtb.DataSet.Tables(0).Columns.Cast(Of DataColumn)().[Select](Function(x) x.ColumnName).ToList()

            If cboCommendObj.SelectedIndex = 0 Then
                Dim rCol As GridBoundColumn
                rCol = New GridBoundColumn()
                rgvDataImported.MasterTableView.Columns.Add(rCol)
                rCol.DataField = "EMPLOYEE_ID"
                rCol.HeaderText = Translate("ID nhân viên")
                rCol.Visible = False

                rCol = New GridBoundColumn()
                rgvDataImported.MasterTableView.Columns.Add(rCol)
                rCol.DataField = "EMPLOYEE_CODE"
                rCol.Visible = True
                rCol.HeaderText = Translate("Mã nhân viên")
                rCol.HeaderStyle.Width = 50
                rCol.EmptyDataText = "0"

                rCol = New GridBoundColumn()
                rgvDataImported.MasterTableView.Columns.Add(rCol)
                rCol.DataField = "FULLNAME_VN"
                rCol.Visible = True
                rCol.HeaderText = Translate("Họ tên nhân viên")
                rCol.HeaderStyle.Width = 120
                rCol.EmptyDataText = String.Empty

                rCol = New GridBoundColumn()
                rgvDataImported.MasterTableView.Columns.Add(rCol)
                rCol.DataField = "ORG_ID"
                rCol.HeaderText = Translate("ORG_ID")
                rCol.Visible = False
                rCol.HeaderStyle.Width = 80
                rCol.EmptyDataText = "0"

                rCol = New GridBoundColumn()
                rgvDataImported.MasterTableView.Columns.Add(rCol)
                rCol.DataField = "ORG_NAME"
                rCol.Visible = True
                rCol.HeaderText = Translate("Tên phòng ban")
                rCol.HeaderStyle.Width = 150
                rCol.EmptyDataText = String.Empty
            Else
                Dim rCol = New GridBoundColumn()
                rgvDataImported.MasterTableView.Columns.Add(rCol)
                rCol.DataField = "ORG_ID"
                rCol.HeaderText = Translate("ORG_ID")
                rCol.Visible = False
                rCol.HeaderStyle.Width = 80
                rCol.EmptyDataText = "0"

                rCol = New GridBoundColumn()
                rgvDataImported.MasterTableView.Columns.Add(rCol)
                rCol.DataField = "ORG_NAME"
                rCol.Visible = True
                rCol.HeaderText = Translate("Tên phòng ban")
                rCol.HeaderStyle.Width = 150
                rCol.EmptyDataText = String.Empty
            End If


            '2. Add các cột được check ở Import List
            Dim rColNum As GridNumericColumn
            Dim column As String = ""
            Dim columnDataField As String = ""
            For Each dataItem As GridDataItem In rgvCommendList.Items
                column = listColumn.Find(Function(x) x = "" + (dataItem("CODE").Text) + "")
                If column <> "" Then
                    columnDataField = column.ToString.Replace("'", "")
                    Dim columnCode As String = dataItem("CODE").Text
                    rColNum = New GridNumericColumn()
                    rgvDataImported.MasterTableView.Columns.Add(rColNum)
                    rColNum.DataField = columnDataField 'DataField của cột trong GridView DataPrepare là ID khen thưởng Import
                    rColNum.HeaderText = Translate(dataItem("NAME").Text) 'Header là Name khen thưởng Import
                    rColNum.HeaderStyle.Width = 100
                    rColNum.EmptyDataText = String.Empty
                    rColNum.DecimalDigits = 5
                End If
            Next

            rgvDataImported.SetFilter()
            rgvDataImported.Rebind()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
#End Region


    ''' <summary>
    ''' Event selected Index change Combobox ĐỐi tượng
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cboCommendObj_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboCommendObj.SelectedIndexChanged
        Try
            Dim validate As New OtherListDTO
            validate.ID = cboCommendObj.SelectedValue
            validate.ACTFLG = "A"
            validate.CODE = ProfileCommon.COMMEND_OBJECT.Name
            Dim rep As New ProfileRepository
            If Not rep.ValidateOtherList(validate) Then
                ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXIST_DATABASE), NotifyType.Error)
                ListComboData = New ComboBoxDataDTO
                ListComboData.GET_COMMEND_OBJ = True
                rep.GetComboList(ListComboData)
                FillDropDownList(cboCommendObj, ListComboData.LIST_COMMEND_OBJ, "NAME_VN", "ID", Common.Common.SystemLanguage, False)
                cboCommendObj.SelectedIndex = 0
                Exit Sub
            End If
            rep.Dispose()
            EventCboCommend()

        Catch ex As Exception

        End Try

    End Sub
    ''' <summary>
    ''' Load lại toàn bộ dữ liệu khi thay đổi giá trị combobox Đối tượng
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub EventCboCommend()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If cboCommendObj.SelectedIndex = 0 Then
                RadPane2.Collapsed = False

            Else
                RadPane2.Collapsed = True

            End If
            Dim data As New DataTable
            data = st.GET_COMMEND_LIST_IMPORT(cboCommendObj.SelectedValue)

            rgvCommendList.DataSource = data
            rgvCommendList.Rebind()
            DesginGridViewDataPrepare()
            FillGridViewDataPrepare(Nothing, True)
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
End Class