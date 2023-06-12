Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Payroll.PayrollBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlPA_ADDTAXNewEdit
    Inherits CommonView
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Public Overrides Property MustAuthorize As Boolean = True

    'Content: Write log time and error
    Dim _mylog As New MyLog()
    Dim _flag As Boolean = True
    Dim _classPath As String = "Payroll\Modules\Business" + Me.GetType().Name.ToString()


#Region "Property"
    Property IDSelect As Decimal
        Get
            Return ViewState(Me.ID & "_IDSelect")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_IDSelect") = value
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

#End Region

#Region "Page"
    ''' <lastupdate>
    ''' 06/07/2017 14:36
    ''' </lastupdate>
    ''' <summary>
    ''' Hiển thị thông tin các control trên page
    ''' Cập nhật các trạng thái của các control
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            'txtThamNien.Enabled = False
            GetParams()
            Refresh()
            UpdateControlState()

            If (_flag = False) Then
                EnableControlAll_Cus(False, LeftPane)
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    '''<lastupdate>
    ''' 
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức khởi tạo giá trị cho các control trên page
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            InitControl()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <lastupdated>
    ''' 06/07/2017 14:40
    ''' </lastupdated>
    ''' <summary>
    ''' Phương thức bind dữ liệu cho các combobox
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim rep As New PayrollRepository
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim table As New DataTable
            table.Columns.Add("YEAR", GetType(Integer))
            table.Columns.Add("ID", GetType(Integer))
            Dim row As DataRow
            For index = 2015 To Date.Now.Year + 1
                row = table.NewRow
                row("ID") = index
                row("YEAR") = index
                table.Rows.Add(row)
            Next
            Dim row2 As DataRow = table.NewRow
            row2("ID") = DBNull.Value
            row2("YEAR") = DBNull.Value
            table.Rows.Add(row2)
            FillRadCombobox(cboYear, table, "YEAR", "ID")
            cboYear.SelectedValue = Date.Now.Year

            ListComboData = New ComboBoxDataDTO
            ListComboData.GET_INCOME_TYPE = True
            rep.GetComboboxData(ListComboData)
            FillDropDownList(cboINCOME_TYPE, ListComboData.LIST_INCOME_TYPE, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboINCOME_TYPE.SelectedValue)
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 06/07/2017 14:56
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc khoi tao cac gia tri cho cac control tren page
    ''' Fixed doi voi user la HR.Admin hoac Admin thi them chuc nang "Mo cho phe duyet"
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.MainToolBar = tbarFamily
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Save, ToolbarItem.Cancel)
            CType(MainToolBar.Items(0), RadToolBarButton).CausesValidation = True
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 06/07/2017 15:17
    ''' </lastupdate>
    ''' <summary>
    ''' Lam moi trang thai cua cac control tren page
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim rep As New PayrollRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Select Case Message
                Case "UpdateView"
                    CurrentState = CommonMessage.STATE_EDIT
                    Dim Working = rep.GetPA_ADDTAX_ByID(hidID.Value)
                    If Working IsNot Nothing Then
                        hidID.Value = Working.ID.ToString
                        hidEmployeeID.Value = Working.EMPLOYEE_ID.ToString
                        txtEmployeeCode.Text = Working.EMPLOYEE_CODE
                        txtEmployeeName.Text = Working.EMPLOYEE_NAME
                        txtOrg_Name.Text = Working.ORG_NAME
                        txtTITLE.Text = Working.TITLE_NAME

                        cboYear.SelectedValue = Working.YEAR
                        cboINCOME_TYPE.SelectedValue = Working.INCOME_TYPE
                        rnTAXABLE_INCOME.Value = Working.TAXABLE_INCOME
                        rnTAX_MONEY.Value = Working.TAX_MONEY
                        txtNOTE.Text = Working.NOTE
                        rnREST_MONEY.Value = Working.REST_MONEY
                    End If
                Case "NormalView"
                    CurrentState = CommonMessage.STATE_NEW
                    Dim dt As New DataTable

            End Select
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Event"
    ''' <lastupdate>
    '''
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện command tren toolbar khi click vao cac item cua no
    ''' Cac command la luu, mo khoa, huy 
    ''' Cập nhật lại trạng thái các control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim rep As New PayrollRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim strUrl As String = Request.Url.ToString()
            Dim isPopup As Boolean = False
            If (strUrl.ToUpper.Contains("DIALOG")) Then
                isPopup = True
            End If
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then

                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW

                                If Execute() Then
                                    Response.Redirect("/Default.aspx?mid=Payroll&fid=ctrlPA_ADDTAX&group=Business")

                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT

                                If Execute() Then
                                    Response.Redirect("/Default.aspx?mid=Payroll&fid=ctrlPA_ADDTAX&group=Business")
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select
                    Else
                        Exit Sub
                    End If

                Case CommonMessage.TOOLBARITEM_CANCEL
                    If (isPopup) Then
                        Dim str As String = "getRadWindow().close('0');"
                        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                    Else
                        Response.Redirect("/Default.aspx?mid=Payroll&fid=ctrlPA_ADDTAX&group=Business")
                    End If
            End Select
            rep.Dispose()
            UpdateControlState()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Function Execute() As Boolean
        Try
            Dim objPA_ADDTAX As New PA_ADDTAXDTO
            Dim rep As New PayrollRepository
            objPA_ADDTAX.EMPLOYEE_ID = Decimal.Parse(hidEmployeeID.Value)
            objPA_ADDTAX.YEAR = cboYear.SelectedValue
            objPA_ADDTAX.INCOME_TYPE = cboINCOME_TYPE.SelectedValue
            objPA_ADDTAX.TAXABLE_INCOME = CDec(rnTAXABLE_INCOME.Value)
            objPA_ADDTAX.TAX_MONEY = CDec(rnTAX_MONEY.Value)
            objPA_ADDTAX.NOTE = txtNOTE.Text
            If IsNumeric(rnREST_MONEY.Value) Then
                objPA_ADDTAX.REST_MONEY = CDec(rnREST_MONEY.Value)
            End If

            If hidID.Value = "" Then
                objPA_ADDTAX.ID = 0
            Else
                objPA_ADDTAX.ID = Decimal.Parse(hidID.Value)
            End If
            If rep.ValidatePA_ADDTAX(objPA_ADDTAX) Then
                ShowMessage(Translate("Dữ liệu đã tồn tại!"), Utilities.NotifyType.Error)
                Return False
            End If
            Dim gID As Decimal
            If hidID.Value = "" Then
                rep.InsertPA_ADDTAX(objPA_ADDTAX, gID)
            Else
                objPA_ADDTAX.ID = Decimal.Parse(hidID.Value)
                rep.ModifyPA_ADDTAX(objPA_ADDTAX, gID)
            End If
            IDSelect = gID
            Return True
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Protected Sub btnEmployee_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEmployee.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            UpdateControlState()
            ctrlFindEmployeePopup.Show()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Dim lstEmpID As New List(Of Decimal)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            lstEmpID = ctrlFindEmployeePopup.SelectedEmployeeID
            If lstEmpID.Count <> 0 Then
                FillData(lstEmpID(0))
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DELETE
                UpdateControlState()
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    Private Sub rd_TextChanged(sender As Object, e As EventArgs) Handles rnTAXABLE_INCOME.TextChanged, rnTAX_MONEY.TextChanged
        If IsNumeric(rnTAXABLE_INCOME.Value) AndAlso IsNumeric(rnTAX_MONEY.Value) Then
            rnREST_MONEY.Value = CDec(rnTAXABLE_INCOME.Value) - CDec(rnTAX_MONEY.Value)
        End If
    End Sub

#End Region

#Region "Custom"

    Public Overrides Sub UpdateControlState()
        'Dim rep As New ProfileBusinessRepository

        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            btnEmployee.Enabled = True
            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                Case CommonMessage.STATE_EDIT
            End Select
            LoadPopup()
            If (hidID.Value = "") Then
                If _toolbar Is Nothing Then Exit Sub
                Dim item As RadToolBarButton
                For i = 0 To _toolbar.Items.Count - 1
                    item = CType(_toolbar.Items(i), RadToolBarButton)
                    'Select Case CurrentState
                    '    Case CommonMessage.STATE_EDIT, CommonMessage.STATE_NEW
                    If item.CommandName = "UNLOCK" Then
                        item.Enabled = False
                    End If
                    'End Select
                Next
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(ViewName, ID, ex)
        End Try

    End Sub

    Private Sub GetParams()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow

            If CurrentState Is Nothing Then
                'cboFamilyType.AutoPostBack = True
                If Request.Params("ID") IsNot Nothing Then
                    hidID.Value = Request.Params("ID")
                    Refresh("UpdateView")
                    Exit Sub
                End If
                Refresh("NormalView")
                If Request.Params("ID") IsNot Nothing Then
                    FillData(Request.Params("ID"))
                End If
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub FillData(ByVal empID As Decimal)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            lstCommonEmployee = CType(ctrlFindEmployeePopup.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            txtEmployeeCode.Text = lstCommonEmployee(0).EMPLOYEE_CODE.ToString
            txtEmployeeName.Text = lstCommonEmployee(0).FULLNAME_VN.ToString
            txtTITLE.Text = lstCommonEmployee(0).TITLE_NAME.ToString
            txtOrg_Name.Text = lstCommonEmployee(0).ORG_NAME.ToString
            hidEmployeeID.Value = lstCommonEmployee(0).EMPLOYEE_ID.ToString
            Dim employeeId As Double = 0
            Double.TryParse(hidEmployeeID.Value, employeeId)
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub


    Private Sub LoadPopup()

        If Not FindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
            ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
            FindEmployee.Controls.Add(ctrlFindEmployeePopup)
            ctrlFindEmployeePopup.MultiSelect = False
            ctrlFindEmployeePopup.MustHaveContract = False
        End If

    End Sub



#End Region

End Class