Imports Common
Imports Framework.UI
Imports Payroll.PayrollBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlPA_DocumentPITDetail
    Inherits Common.CommonView

    'Content: Write log time and error
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Payroll\Modules\Business" + Me.GetType().Name.ToString()


#Region "Property"
#End Region

#Region "Page"
    ''' <lastupdate>
    ''' 06/07/2017 18:11
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức ViewLoad, hiển thị dữ liệu trên trang
    ''' Làm mới trang
    ''' Cập nhật các trạng thái của các control trên page
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            GetParams()
            Refresh()
            UpdateControlState()
            If Not IsPostBack Then
                If CurrentState = CommonMessage.STATE_EDIT Then
                    CType(MainToolBar.Items(0), RadToolBarButton).Enabled = False
                End If
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 06/07/2017 18:11
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức khởi tạo dữ liệu cho các control trên trang
    ''' Xét các trạng thái của grid rgData
    ''' Gọi phương thức khởi tạo 
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

    ''' <lastupdate>
    ''' 06/07/2017 18:11
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức BindData
    ''' Gọi phương thức load dữ liệu cho các combobox
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim rep As New PayrollRepository
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 07/07/2017 08:24
    ''' </lastupdate>
    ''' <summary>
    ''' Ham khoi taso toolbar voi cac button them moi, sua, xuat file, xoa, in hop dong
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.MainToolBar = tbarMain
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Save, ToolbarItem.Cancel)
            CType(MainToolBar.Items(0), RadToolBarButton).CausesValidation = True
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 07/07/2017 08:24
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc lam moi cac control theo tuy chon Message, 
    ''' Message co gia tri default la ""
    ''' Xet cac tuy chon gia tri cua Message la UpdateView, InsertView
    ''' Bind lai du lieu cho grid rgData
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If Not IsPostBack Then
                Select Case Message
                    Case "UpdateView"
                        CurrentState = CommonMessage.STATE_EDIT
                        Dim rep As New PayrollRepository
                        If Request.Params("strDT") IsNot Nothing Then
                            Dim strDT = Encoding.UTF8.GetString(Convert.FromBase64String(Request.Params("strDT")))
                            Dim dtData As New DataTable
                            dtData = rep.GET_EMPLOYEE_PIT_INFO(0, 0, CDec(strDT))
                            If dtData IsNot Nothing AndAlso dtData.Rows.Count > 0 Then
                                Dim item = dtData.Rows(0)
                                txtSymbol.Text = If(Not IsDBNull(item("SERIAL_NO")), item("SERIAL_NO").ToString, "")
                                txtPITNo.Text = If(Not IsDBNull(item("PIT_NO")), item("PIT_NO").ToString, "")
                                txtTypeIncome.Text = If(Not IsDBNull(item("TYPE_INCOME")), item("TYPE_INCOME").ToString, "")
                                txtMonth.Text = If(Not IsDBNull(item("PERIOD_REPLY")), item("PERIOD_REPLY").ToString, "")
                                txtYear.Text = If(Not IsDBNull(item("YEAR")), item("YEAR").ToString, "")
                                If Not IsDBNull(item("CUTRU")) Then
                                    If CDec(item("CUTRU")) = 1 Then
                                        chkResident.Checked = True
                                    ElseIf CDec(item("CUTRU")) = 2 Then
                                        chkNonResident.Checked = True
                                    End If
                                Else
                                    chkResident.Checked = True
                                End If
                                txtOrgName.Text = If(Not IsDBNull(item("ORG_NAME")), item("ORG_NAME").ToString, "")
                                txtOrgPITNo.Text = If(Not IsDBNull(item("ORG_PIT_NO")), item("ORG_PIT_NO").ToString, "")
                                txtOrgAddress.Text = If(Not IsDBNull(item("ORG_ADDRESS")), item("ORG_ADDRESS").ToString, "")
                                txtOrgPhoneNumber.Text = If(Not IsDBNull(item("ORG_PHONE_NUMBER")), item("ORG_PHONE_NUMBER").ToString, "")
                                txtEmployeeName.Text = If(Not IsDBNull(item("EMPLOYEE_NAME")), item("EMPLOYEE_NAME").ToString, "")
                                txtEmployeePITCode.Text = If(Not IsDBNull(item("EMP_PIT_CODE")), item("EMP_PIT_CODE").ToString, "")
                                txtNationality.Text = If(Not IsDBNull(item("NATIONALITY")), item("NATIONALITY").ToString, "")
                                txtContact.Text = If(Not IsDBNull(item("EMP_ADDRESS")), item("EMP_ADDRESS").ToString, "")
                                txtIDNo.Text = If(Not IsDBNull(item("ID_NO")), item("ID_NO").ToString, "")
                                txtIDPlace.Text = If(Not IsDBNull(item("ID_PLACE")), item("ID_PLACE").ToString, "")
                                txtIDDate.Text = If(Not IsDBNull(item("ID_DATE")), item("ID_DATE").ToString, "")
                                rnThuNhapQTT.Value = If(Not IsDBNull(item("TAXABLE_INCOME")), CDec(item("TAXABLE_INCOME").ToString), 0)
                                rnThueTNCNQTT.Value = If(Not IsDBNull(item("MONEY_PIT")), CDec(item("MONEY_PIT").ToString), 0)
                                rnAmount.Value = If(Not IsDBNull(item("REST_INCOME")), CDec(item("REST_INCOME").ToString), 0)
                                txtStatus.Text = If(Not IsDBNull(item("STATUS")), item("STATUS").ToString, "")
                            End If
                        End If
                    Case "InsertView"
                        Dim rep As New PayrollRepository
                        CurrentState = CommonMessage.STATE_NEW
                        If Request.Params("strDT") IsNot Nothing Then
                            Dim strDT = Encoding.UTF8.GetString(Convert.FromBase64String(Request.Params("strDT")))
                            Dim arrInfo = strDT.Split(";")

                            txtSymbol.Text = "AA/" & arrInfo(1) & "/T"
                            txtTypeIncome.Text = arrInfo(2)
                            txtMonth.Text = arrInfo(3)
                            txtYear.Text = arrInfo(1)
                            chkResident.Checked = True
                            Dim dtData As New DataTable
                            dtData = rep.GET_EMPLOYEE_PIT_INFO(CDec(arrInfo(0)), CDec(arrInfo(1)), 0)
                            If dtData IsNot Nothing AndAlso dtData.Rows.Count > 0 Then
                                Dim item = dtData.Rows(0)
                                hidEmp.Value = If(Not IsDBNull(item("EMPLOYEE_ID")), CDec(item("EMPLOYEE_ID").ToString), Nothing)
                                txtPITNo.Text = If(Not IsDBNull(item("PIT_NO")), item("PIT_NO").ToString, "")
                                txtOrgName.Text = If(Not IsDBNull(item("ORG_NAME")), item("ORG_NAME").ToString, "")
                                txtOrgPITNo.Text = If(Not IsDBNull(item("ORG_PIT_NO")), item("ORG_PIT_NO").ToString, "")
                                txtOrgAddress.Text = If(Not IsDBNull(item("ORG_ADDRESS")), item("ORG_ADDRESS").ToString, "")
                                txtOrgPhoneNumber.Text = If(Not IsDBNull(item("ORG_PHONE_NUMBER")), item("ORG_PHONE_NUMBER").ToString, "")
                                txtEmployeeName.Text = If(Not IsDBNull(item("EMPLOYEE_NAME")), item("EMPLOYEE_NAME").ToString, "")
                                txtEmployeePITCode.Text = If(Not IsDBNull(item("EMP_PIT_CODE")), item("EMP_PIT_CODE").ToString, "")
                                txtNationality.Text = If(Not IsDBNull(item("NATIONALITY")), item("NATIONALITY").ToString, "")
                                txtContact.Text = If(Not IsDBNull(item("EMP_ADDRESS")), item("EMP_ADDRESS").ToString, "")
                                txtIDNo.Text = If(Not IsDBNull(item("ID_NO")), item("ID_NO").ToString, "")
                                txtIDPlace.Text = If(Not IsDBNull(item("ID_PLACE")), item("ID_PLACE").ToString, "")
                                txtIDDate.Text = If(Not IsDBNull(item("ID_DATE")), item("ID_DATE").ToString, "")
                                rnThuNhapQTT.Value = If(Not IsDBNull(item("THUNHAP_QTT")), CDec(item("THUNHAP_QTT").ToString), 0)
                                rnThueTNCNQTT.Value = If(Not IsDBNull(item("THUETNCN_QTT")), CDec(item("THUETNCN_QTT").ToString), 0)
                                rnAmount.Value = If(Not IsDBNull(item("AMOUNT")), CDec(item("AMOUNT").ToString), 0)
                            End If
                        End If
                End Select
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 07/07/2017 08:24
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc cap nhat trang thai cac control trong page
    ''' Cap nhat trang thai tbarContracts, rgData, ctrlOrg
    ''' Cap nhat trang thai hien tai, neu trang thai hien tai cua trang la trang thai xoa
    ''' thi xoa hop dong vaf lam moi lai UpdateView
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            ChangeToolbarState()
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Event"

    ''' <lastupdate>
    ''' 07/07/2017 08:24
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien command khi click item tren toolbar
    ''' Command in hop dong, xoa hop dong, xuat file excel
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim rep As New PayrollRepository
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        If Not chkNonResident.Checked AndAlso Not chkResident.Checked Then
                            ShowMessage(Translate("Chưa chọn thông tin cư trú"), Utilities.NotifyType.Warning)
                            Exit Sub
                        End If
                        Dim obj As New PA_DOCUMENT_PITDTO

                        obj.YEAR = txtYear.Text.Trim
                        obj.SERIAL_NO = txtSymbol.Text.Trim
                        obj.PIT_NO = txtPITNo.Text.Trim
                        obj.EMPLOYEE_ID = hidEmp.Value
                        obj.LIEN1 = 0
                        obj.LIEN2 = 0
                        obj.CUTRU = If(chkNonResident.Checked, 2, 1)
                        obj.PERIOD_REPLY = txtMonth.Text.Trim
                        obj.TYPE_INCOME = txtTypeIncome.Text.Trim
                        obj.TAXABLE_INCOME = rnThuNhapQTT.Value
                        obj.MONEY_PIT = rnThueTNCNQTT.Value
                        obj.REST_INCOME = rnAmount.Value
                        If rep.ValidateDocumentPIT(obj) Then
                            ShowMessage(Translate("Chưa chọn thông tin cư trú"), Utilities.NotifyType.Warning)
                            Exit Sub
                        End If
                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                If rep.InsertDocumentPIT(obj) Then
                                    Response.Redirect("/Default.aspx?mid=Payroll&fid=ctrlPA_DocumentPIT&group=Business")
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select
                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    Response.Redirect("/Default.aspx?mid=Payroll&fid=ctrlPA_DocumentPIT&group=Business")
            End Select
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Custom"

    Private Sub GetParams()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow

            If CurrentState Is Nothing Then
                If Request.Params("strDT") IsNot Nothing Then
                    Dim strDT = Encoding.UTF8.GetString(Convert.FromBase64String(Request.Params("strDT")))
                    If strDT.Split(";").Count > 1 Then
                        Refresh("InsertView")
                        Exit Sub
                    End If
                    Refresh("UpdateView")
                    Exit Sub
                End If
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
#End Region
End Class