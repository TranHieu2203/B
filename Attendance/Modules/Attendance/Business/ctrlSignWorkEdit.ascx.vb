Imports System.Globalization
Imports Attendance.AttendanceBusiness
Imports Common
Imports Framework.UI.Utilities
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlSignWorkEdit
    Inherits CommonView
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Protected WithEvents ctrlFindSigner As ctrlFindEmployeePopup
    Public Overrides Property MustAuthorize As Boolean = False
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Attendance/Module/Attendance/Business/" + Me.GetType().Name.ToString()

#Region "Property"

    ''' <summary>
    ''' 0 - normal
    ''' 1 - Employee
    ''' 2 - Sign
    ''' 3 - Org
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
    ''' <summary>
    ''' periodid
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property periodid As Integer
        Get
            Return ViewState(Me.ID & "_periodid")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_periodid") = value
        End Set
    End Property

    Property EmpObj As Integer
        Get
            Return ViewState(Me.ID & "_EmpObj")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_EmpObj") = value
        End Set
    End Property

    Property EmpId As Integer
        Get
            Return ViewState(Me.ID & "_EmpId")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_EmpId") = value
        End Set
    End Property

    Property dtDataD As DataTable
        Get
            Return ViewState(Me.ID & "_dtDataD")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtDataD") = value
        End Set
    End Property

    Property startDate As Date
        Get
            Return ViewState(Me.ID & "_startDate")
        End Get
        Set(ByVal value As Date)
            ViewState(Me.ID & "_startDate") = value
        End Set
    End Property

    Property EndDate As Date
        Get
            Return ViewState(Me.ID & "_EndDate")
        End Get
        Set(ByVal value As Date)
            ViewState(Me.ID & "_EndDate") = value
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
    ''' <summary>
    ''' Insertworksign
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Insertworksign As List(Of AT_WORKSIGNDTO)
        Get
            Return ViewState(Me.ID & "_AT_WORKSIGNDTO")
        End Get
        Set(ByVal value As List(Of AT_WORKSIGNDTO))
            ViewState(Me.ID & "_AT_WORKSIGNDTO") = value
        End Set
    End Property
#End Region

#Region "Page"
    ''' <summary>
    ''' ghi de phuong thuc viewload
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Refresh()
            UpdateControlState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                                   CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Ghi de phuong thuc khoi tao
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            InitControl()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                                   CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    ''' <summary>
    ''' Ghi de phuong thuc BindData bind du lieu cho cac combobox
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                                   CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Phuong thuc khoi tao, thiet lap cac control tren trang: toolbar
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.MainToolBar = tbarMain
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Save,
                                       ToolbarItem.Cancel)
            CType(MainToolBar.Items(0), RadToolBarButton).CausesValidation = True
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                                   CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Lam moi, thiet lap cac thanh phan tren trang
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim rep = New AttendanceRepository
            If Not IsPostBack Then
                If Request.Params("periodid") IsNot Nothing AndAlso Request.Params("periodid").ToString <> "" Then
                    periodid = Request.Params("periodid")
                End If

                If Request.Params("EmpObj") IsNot Nothing AndAlso Request.Params("EmpObj").ToString <> "" Then
                    EmpObj = Request.Params("EmpObj")
                End If

                Dim ddate = rep.Load_date(periodid, EmpObj)

                If ddate.END_DATE IsNot Nothing And ddate.START_DATE IsNot Nothing Then
                    lbStartDate.Text = String.Format("{0}/{1}/{2}", If(ddate.START_DATE.Value.Day < 10, "0" & ddate.START_DATE.Value.Day, ddate.START_DATE.Value.Day), If(ddate.START_DATE.Value.Month < 10, "0" & ddate.START_DATE.Value.Month, ddate.START_DATE.Value.Month), ddate.START_DATE.Value.Year)
                    lbEndDate.Text = String.Format("{0}/{1}/{2}", If(ddate.END_DATE.Value.Day < 10, "0" & ddate.END_DATE.Value.Day, ddate.END_DATE.Value.Day), If(ddate.END_DATE.Value.Month < 10, "0" & ddate.END_DATE.Value.Month, ddate.END_DATE.Value.Month), ddate.END_DATE.Value.Year)
                    lbPeroidName.Text = ddate.PERIOD_T
                    startDate = ddate.START_DATE
                    EndDate = ddate.END_DATE
                End If

                Dim emp = rep.GetEmpId(Request.Params("empID"))
                EmpId = Request.Params("empID")
                txtEmployeeCode.Text = emp.EMPLOYEE_CODE
                txtEmployeeName.Text = emp.FULLNAME_VN
                txtOrgName.Text = emp.ORG_NAME
                txtTitleName.Text = emp.TITLE_NAME_VN

                tr1.Visible = False
                tr2.Visible = False
                tr3.Visible = False
                tr4.Visible = False
                tr5.Visible = False
                tr6.Visible = False
                tr7.Visible = False
                tr8.Visible = False
                tr9.Visible = False
                tr10.Visible = False
                tr11.Visible = False
                tr12.Visible = False
                tr13.Visible = False
                tr14.Visible = False
                tr15.Visible = False
                tr16.Visible = False
                tr17.Visible = False
                tr18.Visible = False
                tr19.Visible = False
                tr20.Visible = False
                tr21.Visible = False
                tr22.Visible = False
                tr23.Visible = False
                tr24.Visible = False
                tr25.Visible = False
                tr26.Visible = False
                tr27.Visible = False
                tr28.Visible = False
                tr29.Visible = False
                tr30.Visible = False
                tr31.Visible = False
                GetDataCombo()
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                                   CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try

    End Sub
#End Region

#Region "Event"
    ''' <summary>
    ''' Xu ly su kien Command khi click cac item tren toolbar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim obj As List(Of AT_WORKSIGNEDITDTO)
        Dim rep As New AttendanceRepository
        Dim gCODE As String = ""
        Dim gstatus As Integer = 0
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName

                Case CommonMessage.TOOLBARITEM_SAVE
                    obj = New List(Of AT_WORKSIGNEDITDTO)
                    Dim i As Integer = 0

                    For Each row As DataRow In dtDataD.Rows
                        i = i + 1

                        Dim objData As New AT_WORKSIGNEDITDTO

                        If i = 1 Then
                            'If cboCode1.SelectedValue = "" Then
                            '    ShowMessage(Translate(String.Format("Ngày {0}: {1}", row("WORKINGDAY"), " Chưa chọn Ca làm việc, Vui lòng kiểm tra lại.")), NotifyType.Warning)
                            '    Exit Sub
                            'End If
                            If cboCode1.SelectedValue <> "" Then
                                objData.SHIFT_ID = cboCode1.SelectedValue
                                objData.WORKINGDAY = CheckDate(row("WORKINGDAY"))

                                obj.Add(objData)
                            End If
                        End If

                        If i = 2 Then
                            'If cboCode2.SelectedValue = "" Then
                            '    ShowMessage(Translate(String.Format("Ngày {0}: {1}", row("WORKINGDAY"), " Chưa chọn Ca làm việc, Vui lòng kiểm tra lại.")), NotifyType.Warning)
                            '    Exit Sub
                            'End If
                            If cboCode2.SelectedValue <> "" Then
                                objData.SHIFT_ID = cboCode2.SelectedValue
                                objData.WORKINGDAY = CheckDate(row("WORKINGDAY"))

                                obj.Add(objData)
                            End If
                        End If

                        If i = 3 Then
                            'If cboCode3.SelectedValue = "" Then
                            '    ShowMessage(Translate(String.Format("Ngày {0}: {1}", row("WORKINGDAY"), " Chưa chọn Ca làm việc, Vui lòng kiểm tra lại.")), NotifyType.Warning)
                            '    Exit Sub
                            'End If
                            If cboCode3.SelectedValue <> "" Then
                                objData.SHIFT_ID = cboCode3.SelectedValue
                                objData.WORKINGDAY = CheckDate(row("WORKINGDAY"))

                                obj.Add(objData)
                            End If
                        End If

                        If i = 4 Then
                            'If cboCode4.SelectedValue = "" Then
                            '    ShowMessage(Translate(String.Format("Ngày {0}: {1}", row("WORKINGDAY"), " Chưa chọn Ca làm việc, Vui lòng kiểm tra lại.")), NotifyType.Warning)
                            '    Exit Sub
                            'End If
                            If cboCode4.SelectedValue <> "" Then
                                objData.SHIFT_ID = cboCode4.SelectedValue
                                objData.WORKINGDAY = CheckDate(row("WORKINGDAY"))

                                obj.Add(objData)
                            End If
                        End If

                        If i = 5 Then
                            'If cboCode5.SelectedValue = "" Then
                            '    ShowMessage(Translate(String.Format("Ngày {0}: {1}", row("WORKINGDAY"), " Chưa chọn Ca làm việc, Vui lòng kiểm tra lại.")), NotifyType.Warning)
                            '    Exit Sub
                            'End If
                            If cboCode5.SelectedValue <> "" Then
                                objData.SHIFT_ID = cboCode5.SelectedValue
                                objData.WORKINGDAY = CheckDate(row("WORKINGDAY"))

                                obj.Add(objData)
                            End If
                        End If

                        If i = 6 Then
                            'If cboCode6.SelectedValue = "" Then
                            '    ShowMessage(Translate(String.Format("Ngày {0}: {1}", row("WORKINGDAY"), " Chưa chọn Ca làm việc, Vui lòng kiểm tra lại.")), NotifyType.Warning)
                            '    Exit Sub
                            'End If
                            If cboCode6.SelectedValue <> "" Then
                                objData.SHIFT_ID = cboCode6.SelectedValue
                                objData.WORKINGDAY = CheckDate(row("WORKINGDAY"))

                                obj.Add(objData)
                            End If
                        End If

                        If i = 7 Then
                            'If cboCode7.SelectedValue = "" Then
                            '    ShowMessage(Translate(String.Format("Ngày {0}: {1}", row("WORKINGDAY"), " Chưa chọn Ca làm việc, Vui lòng kiểm tra lại.")), NotifyType.Warning)
                            '    Exit Sub
                            'End If
                            If cboCode7.SelectedValue <> "" Then
                                objData.SHIFT_ID = cboCode7.SelectedValue
                                objData.WORKINGDAY = CheckDate(row("WORKINGDAY"))

                                obj.Add(objData)
                            End If
                        End If

                        If i = 8 Then
                            'If cboCode8.SelectedValue = "" Then
                            '    ShowMessage(Translate(String.Format("Ngày {0}: {1}", row("WORKINGDAY"), " Chưa chọn Ca làm việc, Vui lòng kiểm tra lại.")), NotifyType.Warning)
                            '    Exit Sub
                            'End If
                            If cboCode8.SelectedValue <> "" Then
                                objData.SHIFT_ID = cboCode8.SelectedValue
                                objData.WORKINGDAY = CheckDate(row("WORKINGDAY"))

                                obj.Add(objData)
                            End If
                        End If

                        If i = 9 Then
                            'If cboCode9.SelectedValue = "" Then
                            '    ShowMessage(Translate(String.Format("Ngày {0}: {1}", row("WORKINGDAY"), " Chưa chọn Ca làm việc, Vui lòng kiểm tra lại.")), NotifyType.Warning)
                            '    Exit Sub
                            'End If
                            If cboCode9.SelectedValue <> "" Then
                                objData.SHIFT_ID = cboCode9.SelectedValue
                                objData.WORKINGDAY = CheckDate(row("WORKINGDAY"))

                                obj.Add(objData)
                            End If
                        End If

                        If i = 10 Then
                            'If cboCode10.SelectedValue = "" Then
                            '    ShowMessage(Translate(String.Format("Ngày {0}: {1}", row("WORKINGDAY"), " Chưa chọn Ca làm việc, Vui lòng kiểm tra lại.")), NotifyType.Warning)
                            '    Exit Sub
                            'End If
                            If cboCode10.SelectedValue <> "" Then
                                objData.SHIFT_ID = cboCode10.SelectedValue
                                objData.WORKINGDAY = CheckDate(row("WORKINGDAY"))

                                obj.Add(objData)
                            End If
                        End If

                        If i = 11 Then
                            'If cboCode11.SelectedValue = "" Then
                            '    ShowMessage(Translate(String.Format("Ngày {0}: {1}", row("WORKINGDAY"), " Chưa chọn Ca làm việc, Vui lòng kiểm tra lại.")), NotifyType.Warning)
                            '    Exit Sub
                            'End If
                            If cboCode11.SelectedValue <> "" Then
                                objData.SHIFT_ID = cboCode11.SelectedValue
                                objData.WORKINGDAY = CheckDate(row("WORKINGDAY"))

                                obj.Add(objData)
                            End If
                        End If

                        If i = 12 Then
                            'If cboCode12.SelectedValue = "" Then
                            '    ShowMessage(Translate(String.Format("Ngày {0}: {1}", row("WORKINGDAY"), " Chưa chọn Ca làm việc, Vui lòng kiểm tra lại.")), NotifyType.Warning)
                            '    Exit Sub
                            'End If
                            If cboCode12.SelectedValue <> "" Then
                                objData.SHIFT_ID = cboCode12.SelectedValue
                                objData.WORKINGDAY = CheckDate(row("WORKINGDAY"))

                                obj.Add(objData)
                            End If
                        End If

                        If i = 13 Then
                            'If cboCode13.SelectedValue = "" Then
                            '    ShowMessage(Translate(String.Format("Ngày {0}: {1}", row("WORKINGDAY"), " Chưa chọn Ca làm việc, Vui lòng kiểm tra lại.")), NotifyType.Warning)
                            '    Exit Sub
                            'End If
                            If cboCode13.SelectedValue <> "" Then
                                objData.SHIFT_ID = cboCode13.SelectedValue
                                objData.WORKINGDAY = CheckDate(row("WORKINGDAY"))

                                obj.Add(objData)
                            End If
                        End If

                        If i = 14 Then
                            'If cboCode14.SelectedValue = "" Then
                            '    ShowMessage(Translate(String.Format("Ngày {0}: {1}", row("WORKINGDAY"), " Chưa chọn Ca làm việc, Vui lòng kiểm tra lại.")), NotifyType.Warning)
                            '    Exit Sub
                            'End If
                            If cboCode14.SelectedValue <> "" Then
                                objData.SHIFT_ID = cboCode14.SelectedValue
                                objData.WORKINGDAY = CheckDate(row("WORKINGDAY"))

                                obj.Add(objData)
                            End If
                        End If

                        If i = 15 Then
                            'If cboCode15.SelectedValue = "" Then
                            '    ShowMessage(Translate(String.Format("Ngày {0}: {1}", row("WORKINGDAY"), " Chưa chọn Ca làm việc, Vui lòng kiểm tra lại.")), NotifyType.Warning)
                            '    Exit Sub
                            'End If
                            If cboCode15.SelectedValue <> "" Then
                                objData.SHIFT_ID = cboCode15.SelectedValue
                                objData.WORKINGDAY = CheckDate(row("WORKINGDAY"))

                                obj.Add(objData)
                            End If
                        End If

                        If i = 16 Then
                            'If cboCode16.SelectedValue = "" Then
                            '    ShowMessage(Translate(String.Format("Ngày {0}: {1}", row("WORKINGDAY"), " Chưa chọn Ca làm việc, Vui lòng kiểm tra lại.")), NotifyType.Warning)
                            '    Exit Sub
                            'End If
                            If cboCode16.SelectedValue <> "" Then
                                objData.SHIFT_ID = cboCode16.SelectedValue
                                objData.WORKINGDAY = CheckDate(row("WORKINGDAY"))

                                obj.Add(objData)
                            End If
                        End If

                        If i = 17 Then
                            'If cboCode17.SelectedValue = "" Then
                            '    ShowMessage(Translate(String.Format("Ngày {0}: {1}", row("WORKINGDAY"), " Chưa chọn Ca làm việc, Vui lòng kiểm tra lại.")), NotifyType.Warning)
                            '    Exit Sub
                            'End If
                            If cboCode17.SelectedValue <> "" Then
                                objData.SHIFT_ID = cboCode17.SelectedValue
                                objData.WORKINGDAY = CheckDate(row("WORKINGDAY"))

                                obj.Add(objData)
                            End If
                        End If

                        If i = 18 Then
                            'If cboCode18.SelectedValue = "" Then
                            '    ShowMessage(Translate(String.Format("Ngày {0}: {1}", row("WORKINGDAY"), " Chưa chọn Ca làm việc, Vui lòng kiểm tra lại.")), NotifyType.Warning)
                            '    Exit Sub
                            'End If
                            If cboCode18.SelectedValue <> "" Then
                                objData.SHIFT_ID = cboCode18.SelectedValue
                                objData.WORKINGDAY = CheckDate(row("WORKINGDAY"))

                                obj.Add(objData)
                            End If
                        End If

                        If i = 19 Then
                            'If cboCode19.SelectedValue = "" Then
                            '    ShowMessage(Translate(String.Format("Ngày {0}: {1}", row("WORKINGDAY"), " Chưa chọn Ca làm việc, Vui lòng kiểm tra lại.")), NotifyType.Warning)
                            '    Exit Sub
                            'End If
                            If cboCode19.SelectedValue <> "" Then
                                objData.SHIFT_ID = cboCode19.SelectedValue
                                objData.WORKINGDAY = CheckDate(row("WORKINGDAY"))

                                obj.Add(objData)
                            End If
                        End If

                        If i = 20 Then
                            'If cboCode20.SelectedValue = "" Then
                            '    ShowMessage(Translate(String.Format("Ngày {0}: {1}", row("WORKINGDAY"), " Chưa chọn Ca làm việc, Vui lòng kiểm tra lại.")), NotifyType.Warning)
                            '    Exit Sub
                            'End If
                            If cboCode20.SelectedValue <> "" Then
                                objData.SHIFT_ID = cboCode20.SelectedValue
                                objData.WORKINGDAY = CheckDate(row("WORKINGDAY"))

                                obj.Add(objData)
                            End If
                        End If

                        If i = 21 Then
                            'If cboCode21.SelectedValue = "" Then
                            '    ShowMessage(Translate(String.Format("Ngày {0}: {1}", row("WORKINGDAY"), " Chưa chọn Ca làm việc, Vui lòng kiểm tra lại.")), NotifyType.Warning)
                            '    Exit Sub
                            'End If
                            If cboCode21.SelectedValue <> "" Then
                                objData.SHIFT_ID = cboCode21.SelectedValue
                                objData.WORKINGDAY = CheckDate(row("WORKINGDAY"))

                                obj.Add(objData)
                            End If
                        End If

                        If i = 22 Then
                            'If cboCode22.SelectedValue = "" Then
                            '    ShowMessage(Translate(String.Format("Ngày {0}: {1}", row("WORKINGDAY"), " Chưa chọn Ca làm việc, Vui lòng kiểm tra lại.")), NotifyType.Warning)
                            '    Exit Sub
                            'End If
                            If cboCode22.SelectedValue <> "" Then
                                objData.SHIFT_ID = cboCode22.SelectedValue
                                objData.WORKINGDAY = CheckDate(row("WORKINGDAY"))

                                obj.Add(objData)
                            End If
                        End If

                        If i = 23 Then
                            'If cboCode23.SelectedValue = "" Then
                            '    ShowMessage(Translate(String.Format("Ngày {0}: {1}", row("WORKINGDAY"), " Chưa chọn Ca làm việc, Vui lòng kiểm tra lại.")), NotifyType.Warning)
                            '    Exit Sub
                            'End If
                            If cboCode23.SelectedValue <> "" Then
                                objData.SHIFT_ID = cboCode23.SelectedValue
                                objData.WORKINGDAY = CheckDate(row("WORKINGDAY"))

                                obj.Add(objData)
                            End If
                        End If

                        If i = 24 Then
                            'If cboCode24.SelectedValue = "" Then
                            '    ShowMessage(Translate(String.Format("Ngày {0}: {1}", row("WORKINGDAY"), " Chưa chọn Ca làm việc, Vui lòng kiểm tra lại.")), NotifyType.Warning)
                            '    Exit Sub
                            'End If
                            If cboCode24.SelectedValue <> "" Then
                                objData.SHIFT_ID = cboCode24.SelectedValue
                                objData.WORKINGDAY = CheckDate(row("WORKINGDAY"))

                                obj.Add(objData)
                            End If
                        End If

                        If i = 25 Then
                            'If cboCode25.SelectedValue = "" Then
                            '    ShowMessage(Translate(String.Format("Ngày {0}: {1}", row("WORKINGDAY"), " Chưa chọn Ca làm việc, Vui lòng kiểm tra lại.")), NotifyType.Warning)
                            '    Exit Sub
                            'End If
                            If cboCode25.SelectedValue <> "" Then
                                objData.SHIFT_ID = cboCode25.SelectedValue
                                objData.WORKINGDAY = CheckDate(row("WORKINGDAY"))

                                obj.Add(objData)
                            End If
                        End If

                        If i = 26 Then
                            'If cboCode26.SelectedValue = "" Then
                            '    ShowMessage(Translate(String.Format("Ngày {0}: {1}", row("WORKINGDAY"), " Chưa chọn Ca làm việc, Vui lòng kiểm tra lại.")), NotifyType.Warning)
                            '    Exit Sub
                            'End If
                            If cboCode26.SelectedValue <> "" Then
                                objData.SHIFT_ID = cboCode26.SelectedValue
                                objData.WORKINGDAY = CheckDate(row("WORKINGDAY"))

                                obj.Add(objData)
                            End If
                        End If

                        If i = 27 Then
                            'If cboCode27.SelectedValue = "" Then
                            '    ShowMessage(Translate(String.Format("Ngày {0}: {1}", row("WORKINGDAY"), " Chưa chọn Ca làm việc, Vui lòng kiểm tra lại.")), NotifyType.Warning)
                            '    Exit Sub
                            'End If
                            If cboCode27.SelectedValue <> "" Then
                                objData.SHIFT_ID = cboCode27.SelectedValue
                                objData.WORKINGDAY = CheckDate(row("WORKINGDAY"))

                                obj.Add(objData)
                            End If
                        End If

                        If i = 28 Then
                            'If cboCode28.SelectedValue = "" Then
                            '    ShowMessage(Translate(String.Format("Ngày {0}: {1}", row("WORKINGDAY"), " Chưa chọn Ca làm việc, Vui lòng kiểm tra lại.")), NotifyType.Warning)
                            '    Exit Sub
                            'End If
                            If cboCode28.SelectedValue <> "" Then
                                objData.SHIFT_ID = cboCode28.SelectedValue
                                objData.WORKINGDAY = CheckDate(row("WORKINGDAY"))

                                obj.Add(objData)
                            End If
                        End If

                        If i = 29 Then
                            'If cboCode29.SelectedValue = "" Then
                            '    ShowMessage(Translate(String.Format("Ngày {0}: {1}", row("WORKINGDAY"), " Chưa chọn Ca làm việc, Vui lòng kiểm tra lại.")), NotifyType.Warning)
                            '    Exit Sub
                            'End If
                            If cboCode29.SelectedValue <> "" Then
                                objData.SHIFT_ID = cboCode29.SelectedValue
                                objData.WORKINGDAY = CheckDate(row("WORKINGDAY"))

                                obj.Add(objData)
                            End If
                        End If

                        If i = 30 Then
                            'If cboCode30.SelectedValue = "" Then
                            '    ShowMessage(Translate(String.Format("Ngày {0}: {1}", row("WORKINGDAY"), " Chưa chọn Ca làm việc, Vui lòng kiểm tra lại.")), NotifyType.Warning)
                            '    Exit Sub
                            'End If
                            If cboCode30.SelectedValue <> "" Then
                                objData.SHIFT_ID = cboCode30.SelectedValue
                                objData.WORKINGDAY = CheckDate(row("WORKINGDAY"))

                                obj.Add(objData)
                            End If
                        End If

                        If i = 31 Then
                            'If cboCode31.SelectedValue = "" Then
                            '    ShowMessage(Translate(String.Format("Ngày {0}: {1}", row("WORKINGDAY"), " Chưa chọn Ca làm việc, Vui lòng kiểm tra lại.")), NotifyType.Warning)
                            '    Exit Sub
                            'End If
                            If cboCode31.SelectedValue <> "" Then
                                objData.SHIFT_ID = cboCode31.SelectedValue
                                objData.WORKINGDAY = CheckDate(row("WORKINGDAY"))

                                obj.Add(objData)
                            End If
                        End If
                    Next

                    If rep.Modify_WorkSign_ByEmp(EmpId, startDate, EndDate, periodid, obj) Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                    End If

                Case CommonMessage.TOOLBARITEM_CANCEL
                    Dim i As Integer = 0
                    For Each row As DataRow In dtDataD.Rows
                        i = i + 1

                        If i = 1 Then
                            cboCode1.SelectedValue = If(IsDBNull(row("SHIFT_ID")), Nothing, row("SHIFT_ID"))
                        End If

                        If i = 2 Then
                            cboCode2.SelectedValue = If(IsDBNull(row("SHIFT_ID")), Nothing, row("SHIFT_ID"))
                        End If

                        If i = 3 Then
                            cboCode3.SelectedValue = If(IsDBNull(row("SHIFT_ID")), Nothing, row("SHIFT_ID"))
                        End If

                        If i = 4 Then
                            cboCode4.SelectedValue = If(IsDBNull(row("SHIFT_ID")), Nothing, row("SHIFT_ID"))
                        End If

                        If i = 5 Then
                            cboCode5.SelectedValue = If(IsDBNull(row("SHIFT_ID")), Nothing, row("SHIFT_ID"))
                        End If

                        If i = 6 Then
                            cboCode6.SelectedValue = If(IsDBNull(row("SHIFT_ID")), Nothing, row("SHIFT_ID"))
                        End If

                        If i = 7 Then
                            cboCode7.SelectedValue = If(IsDBNull(row("SHIFT_ID")), Nothing, row("SHIFT_ID"))
                        End If

                        If i = 8 Then
                            cboCode8.SelectedValue = If(IsDBNull(row("SHIFT_ID")), Nothing, row("SHIFT_ID"))
                        End If

                        If i = 9 Then
                            cboCode9.SelectedValue = If(IsDBNull(row("SHIFT_ID")), Nothing, row("SHIFT_ID"))
                        End If

                        If i = 10 Then
                            cboCode10.SelectedValue = If(IsDBNull(row("SHIFT_ID")), Nothing, row("SHIFT_ID"))
                        End If

                        If i = 11 Then
                            cboCode11.SelectedValue = If(IsDBNull(row("SHIFT_ID")), Nothing, row("SHIFT_ID"))
                        End If

                        If i = 12 Then
                            cboCode12.SelectedValue = If(IsDBNull(row("SHIFT_ID")), Nothing, row("SHIFT_ID"))
                        End If

                        If i = 13 Then
                            cboCode13.SelectedValue = If(IsDBNull(row("SHIFT_ID")), Nothing, row("SHIFT_ID"))
                        End If

                        If i = 14 Then
                            cboCode14.SelectedValue = If(IsDBNull(row("SHIFT_ID")), Nothing, row("SHIFT_ID"))
                        End If

                        If i = 15 Then
                            cboCode15.SelectedValue = If(IsDBNull(row("SHIFT_ID")), Nothing, row("SHIFT_ID"))
                        End If

                        If i = 16 Then
                            cboCode16.SelectedValue = If(IsDBNull(row("SHIFT_ID")), Nothing, row("SHIFT_ID"))
                        End If

                        If i = 17 Then
                            cboCode17.SelectedValue = If(IsDBNull(row("SHIFT_ID")), Nothing, row("SHIFT_ID"))
                        End If

                        If i = 18 Then
                            cboCode18.SelectedValue = If(IsDBNull(row("SHIFT_ID")), Nothing, row("SHIFT_ID"))
                        End If

                        If i = 19 Then
                            cboCode19.SelectedValue = If(IsDBNull(row("SHIFT_ID")), Nothing, row("SHIFT_ID"))
                        End If

                        If i = 20 Then
                            cboCode20.SelectedValue = If(IsDBNull(row("SHIFT_ID")), Nothing, row("SHIFT_ID"))
                        End If

                        If i = 21 Then
                            cboCode21.SelectedValue = If(IsDBNull(row("SHIFT_ID")), Nothing, row("SHIFT_ID"))
                        End If

                        If i = 22 Then
                            cboCode22.SelectedValue = If(IsDBNull(row("SHIFT_ID")), Nothing, row("SHIFT_ID"))
                        End If

                        If i = 23 Then
                            cboCode23.SelectedValue = If(IsDBNull(row("SHIFT_ID")), Nothing, row("SHIFT_ID"))
                        End If

                        If i = 24 Then
                            cboCode24.SelectedValue = If(IsDBNull(row("SHIFT_ID")), Nothing, row("SHIFT_ID"))
                        End If

                        If i = 25 Then
                            cboCode25.SelectedValue = If(IsDBNull(row("SHIFT_ID")), Nothing, row("SHIFT_ID"))
                        End If

                        If i = 26 Then
                            cboCode26.SelectedValue = If(IsDBNull(row("SHIFT_ID")), Nothing, row("SHIFT_ID"))
                        End If

                        If i = 27 Then
                            cboCode27.SelectedValue = If(IsDBNull(row("SHIFT_ID")), Nothing, row("SHIFT_ID"))
                        End If

                        If i = 28 Then
                            cboCode28.SelectedValue = If(IsDBNull(row("SHIFT_ID")), Nothing, row("SHIFT_ID"))
                        End If

                        If i = 29 Then
                            cboCode29.SelectedValue = If(IsDBNull(row("SHIFT_ID")), Nothing, row("SHIFT_ID"))
                        End If

                        If i = 30 Then
                            cboCode30.SelectedValue = If(IsDBNull(row("SHIFT_ID")), Nothing, row("SHIFT_ID"))
                        End If

                        If i = 31 Then
                            cboCode31.SelectedValue = If(IsDBNull(row("SHIFT_ID")), Nothing, row("SHIFT_ID"))
                        End If
                    Next
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                                   CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Function CheckDate(ByVal value As String) As Date
        Dim dateCheck As Boolean
        Dim result As Date
        Try
            dateCheck = DateTime.TryParseExact(value, "dd/MM/yyyy", New CultureInfo("en-US"), DateTimeStyles.None, result)
            Return result
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Function


    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand

    End Sub
    Private Sub cboCode1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCode1.SelectedIndexChanged
        Dim rep As New AttendanceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim date_check As Date
            Dim join_date As New Date
            Dim ter_effect_date As New Date
            rep.Get_Date(EmpId, join_date, ter_effect_date)
            If join_date <> date_check Then
                If Convert.ToDateTime(lbDayEdit1.Text) < join_date Then
                    cboCode1.ClearSelection()
                    ShowMessage(Translate("Ngày thay đổi đang nhỏ hơn ngày vào làm việc."), NotifyType.Error)
                End If
            End If
            If ter_effect_date <> date_check Then
                If Convert.ToDateTime(lbDayEdit1.Text) > ter_effect_date Then
                    cboCode1.ClearSelection()
                    ShowMessage(Translate("Ngày thay đổi đang lớn hơn ngày nghỉ việc."), NotifyType.Error)
                End If
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub cboCode2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCode2.SelectedIndexChanged
        Dim rep As New AttendanceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim date_check As Date
            Dim join_date As New Date
            Dim ter_effect_date As New Date
            rep.Get_Date(EmpId, join_date, ter_effect_date)
            If join_date <> date_check Then
                If Convert.ToDateTime(lbDayEdit2.Text) < join_date Then
                    cboCode2.ClearSelection()
                    ShowMessage(Translate("Ngày thay đổi đang nhỏ hơn ngày vào làm việc."), NotifyType.Error)
                End If
            End If
            If ter_effect_date <> date_check Then
                If Convert.ToDateTime(lbDayEdit2.Text) > ter_effect_date Then
                    cboCode2.ClearSelection()
                    ShowMessage(Translate("Ngày thay đổi đang lớn hơn ngày nghỉ việc."), NotifyType.Error)
                End If
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub cboCode3_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCode3.SelectedIndexChanged
        Dim rep As New AttendanceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim date_check As Date
            Dim join_date As New Date
            Dim ter_effect_date As New Date
            rep.Get_Date(EmpId, join_date, ter_effect_date)
            If join_date <> date_check Then
                If Convert.ToDateTime(lbDayEdit3.Text) < join_date Then
                    cboCode3.ClearSelection()
                    ShowMessage(Translate("Ngày thay đổi đang nhỏ hơn ngày vào làm việc."), NotifyType.Error)
                End If
            End If
            If ter_effect_date <> date_check Then
                If Convert.ToDateTime(lbDayEdit3.Text) > ter_effect_date Then
                    cboCode3.ClearSelection()
                    ShowMessage(Translate("Ngày thay đổi đang lớn hơn ngày nghỉ việc."), NotifyType.Error)
                End If
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub cboCode4_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCode4.SelectedIndexChanged
        Dim rep As New AttendanceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim date_check As Date
            Dim join_date As New Date
            Dim ter_effect_date As New Date
            rep.Get_Date(EmpId, join_date, ter_effect_date)
            If join_date <> date_check Then
                If Convert.ToDateTime(lbDayEdit4.Text) < join_date Then
                    cboCode4.ClearSelection()
                    ShowMessage(Translate("Ngày thay đổi đang nhỏ hơn ngày vào làm việc."), NotifyType.Error)
                End If
            End If
            If ter_effect_date <> date_check Then
                If Convert.ToDateTime(lbDayEdit4.Text) > ter_effect_date Then
                    cboCode4.ClearSelection()
                    ShowMessage(Translate("Ngày thay đổi đang lớn hơn ngày nghỉ việc."), NotifyType.Error)
                End If
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub cboCode5_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCode5.SelectedIndexChanged
        Dim rep As New AttendanceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim date_check As Date
            Dim join_date As New Date
            Dim ter_effect_date As New Date
            rep.Get_Date(EmpId, join_date, ter_effect_date)
            If join_date <> date_check Then
                If Convert.ToDateTime(lbDayEdit5.Text) < join_date Then
                    cboCode5.ClearSelection()
                    ShowMessage(Translate("Ngày thay đổi đang nhỏ hơn ngày vào làm việc."), NotifyType.Error)
                End If
            End If
            If ter_effect_date <> date_check Then
                If Convert.ToDateTime(lbDayEdit5.Text) > ter_effect_date Then
                    cboCode5.ClearSelection()
                    ShowMessage(Translate("Ngày thay đổi đang lớn hơn ngày nghỉ việc."), NotifyType.Error)
                End If
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub cboCode6_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCode6.SelectedIndexChanged
        Dim rep As New AttendanceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim date_check As Date
            Dim join_date As New Date
            Dim ter_effect_date As New Date
            rep.Get_Date(EmpId, join_date, ter_effect_date)
            If join_date <> date_check Then
                If Convert.ToDateTime(lbDayEdit6.Text) < join_date Then
                    cboCode6.ClearSelection()
                    ShowMessage(Translate("Ngày thay đổi đang nhỏ hơn ngày vào làm việc."), NotifyType.Error)
                End If
            End If
            If ter_effect_date <> date_check Then
                If Convert.ToDateTime(lbDayEdit6.Text) > ter_effect_date Then
                    cboCode6.ClearSelection()
                    ShowMessage(Translate("Ngày thay đổi đang lớn hơn ngày nghỉ việc."), NotifyType.Error)
                End If
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub cboCode7_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCode7.SelectedIndexChanged
        Dim rep As New AttendanceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim date_check As Date
            Dim join_date As New Date
            Dim ter_effect_date As New Date
            rep.Get_Date(EmpId, join_date, ter_effect_date)
            If join_date <> date_check Then
                If Convert.ToDateTime(lbDayEdit7.Text) < join_date Then
                    cboCode7.ClearSelection()
                    ShowMessage(Translate("Ngày thay đổi đang nhỏ hơn ngày vào làm việc."), NotifyType.Error)
                End If
            End If
            If ter_effect_date <> date_check Then
                If Convert.ToDateTime(lbDayEdit7.Text) > ter_effect_date Then
                    cboCode7.ClearSelection()
                    ShowMessage(Translate("Ngày thay đổi đang lớn hơn ngày nghỉ việc."), NotifyType.Error)
                End If
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub cboCode8_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCode8.SelectedIndexChanged
        Dim rep As New AttendanceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim date_check As Date
            Dim join_date As New Date
            Dim ter_effect_date As New Date
            rep.Get_Date(EmpId, join_date, ter_effect_date)
            If join_date <> date_check Then
                If Convert.ToDateTime(lbDayEdit8.Text) < join_date Then
                    cboCode8.ClearSelection()
                    ShowMessage(Translate("Ngày thay đổi đang nhỏ hơn ngày vào làm việc."), NotifyType.Error)
                End If
            End If
            If ter_effect_date <> date_check Then
                If Convert.ToDateTime(lbDayEdit8.Text) > ter_effect_date Then
                    cboCode8.ClearSelection()
                    ShowMessage(Translate("Ngày thay đổi đang lớn hơn ngày nghỉ việc."), NotifyType.Error)
                End If
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub cboCode9_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCode9.SelectedIndexChanged
        Dim rep As New AttendanceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim date_check As Date
            Dim join_date As New Date
            Dim ter_effect_date As New Date
            rep.Get_Date(EmpId, join_date, ter_effect_date)
            If join_date <> date_check Then
                If Convert.ToDateTime(lbDayEdit9.Text) < join_date Then
                    cboCode9.ClearSelection()
                    ShowMessage(Translate("Ngày thay đổi đang nhỏ hơn ngày vào làm việc."), NotifyType.Error)
                End If
            End If
            If ter_effect_date <> date_check Then
                If Convert.ToDateTime(lbDayEdit9.Text) > ter_effect_date Then
                    cboCode9.ClearSelection()
                    ShowMessage(Translate("Ngày thay đổi đang lớn hơn ngày nghỉ việc."), NotifyType.Error)
                End If
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub cboCode10_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCode10.SelectedIndexChanged
        Dim rep As New AttendanceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim date_check As Date
            Dim join_date As New Date
            Dim ter_effect_date As New Date
            rep.Get_Date(EmpId, join_date, ter_effect_date)
            If join_date <> date_check Then
                If Convert.ToDateTime(lbDayEdit10.Text) < join_date Then
                    cboCode1.ClearSelection()
                    ShowMessage(Translate("Ngày thay đổi đang nhỏ hơn ngày vào làm việc."), NotifyType.Error)
                End If
            End If
            If ter_effect_date <> date_check Then
                If Convert.ToDateTime(lbDayEdit1.Text) > ter_effect_date Then
                    cboCode1.ClearSelection()
                    ShowMessage(Translate("Ngày thay đổi đang lớn hơn ngày nghỉ việc."), NotifyType.Error)
                End If
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub cboCode11_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCode11.SelectedIndexChanged
        Dim rep As New AttendanceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim date_check As Date
            Dim join_date As New Date
            Dim ter_effect_date As New Date
            rep.Get_Date(EmpId, join_date, ter_effect_date)
            If join_date <> date_check Then
                If Convert.ToDateTime(lbDayEdit11.Text) < join_date Then
                    cboCode11.ClearSelection()
                    ShowMessage(Translate("Ngày thay đổi đang nhỏ hơn ngày vào làm việc."), NotifyType.Error)
                End If
            End If
            If ter_effect_date <> date_check Then
                If Convert.ToDateTime(lbDayEdit11.Text) > ter_effect_date Then
                    cboCode11.ClearSelection()
                    ShowMessage(Translate("Ngày thay đổi đang lớn hơn ngày nghỉ việc."), NotifyType.Error)
                End If
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub cboCode12_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCode12.SelectedIndexChanged
        Dim rep As New AttendanceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim date_check As Date
            Dim join_date As New Date
            Dim ter_effect_date As New Date
            rep.Get_Date(EmpId, join_date, ter_effect_date)
            If join_date <> date_check Then
                If Convert.ToDateTime(lbDayEdit12.Text) < join_date Then
                    cboCode12.ClearSelection()
                    ShowMessage(Translate("Ngày thay đổi đang nhỏ hơn ngày vào làm việc."), NotifyType.Error)
                End If
            End If
            If ter_effect_date <> date_check Then
                If Convert.ToDateTime(lbDayEdit12.Text) > ter_effect_date Then
                    cboCode12.ClearSelection()
                    ShowMessage(Translate("Ngày thay đổi đang lớn hơn ngày nghỉ việc."), NotifyType.Error)
                End If
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub cboCode13_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCode13.SelectedIndexChanged
        Dim rep As New AttendanceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim date_check As Date
            Dim join_date As New Date
            Dim ter_effect_date As New Date
            rep.Get_Date(EmpId, join_date, ter_effect_date)
            If join_date <> date_check Then
                If Convert.ToDateTime(lbDayEdit13.Text) < join_date Then
                    cboCode13.ClearSelection()
                    ShowMessage(Translate("Ngày thay đổi đang nhỏ hơn ngày vào làm việc."), NotifyType.Error)
                End If
            End If
            If ter_effect_date <> date_check Then
                If Convert.ToDateTime(lbDayEdit13.Text) > ter_effect_date Then
                    cboCode13.ClearSelection()
                    ShowMessage(Translate("Ngày thay đổi đang lớn hơn ngày nghỉ việc."), NotifyType.Error)
                End If
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub cboCode14_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCode14.SelectedIndexChanged
        Dim rep As New AttendanceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim date_check As Date
            Dim join_date As New Date
            Dim ter_effect_date As New Date
            rep.Get_Date(EmpId, join_date, ter_effect_date)
            If join_date <> date_check Then
                If Convert.ToDateTime(lbDayEdit14.Text) < join_date Then
                    cboCode14.ClearSelection()
                    ShowMessage(Translate("Ngày thay đổi đang nhỏ hơn ngày vào làm việc."), NotifyType.Error)
                End If
            End If
            If ter_effect_date <> date_check Then
                If Convert.ToDateTime(lbDayEdit14.Text) > ter_effect_date Then
                    cboCode14.ClearSelection()
                    ShowMessage(Translate("Ngày thay đổi đang lớn hơn ngày nghỉ việc."), NotifyType.Error)
                End If
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub cboCode15_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCode15.SelectedIndexChanged
        Dim rep As New AttendanceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim date_check As Date
            Dim join_date As New Date
            Dim ter_effect_date As New Date
            rep.Get_Date(EmpId, join_date, ter_effect_date)
            If join_date <> date_check Then
                If Convert.ToDateTime(lbDayEdit15.Text) < join_date Then
                    cboCode15.ClearSelection()
                    ShowMessage(Translate("Ngày thay đổi đang nhỏ hơn ngày vào làm việc."), NotifyType.Error)
                End If
            End If
            If ter_effect_date <> date_check Then
                If Convert.ToDateTime(lbDayEdit15.Text) > ter_effect_date Then
                    cboCode15.ClearSelection()
                    ShowMessage(Translate("Ngày thay đổi đang lớn hơn ngày nghỉ việc."), NotifyType.Error)
                End If
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub cboCode16_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCode16.SelectedIndexChanged
        Dim rep As New AttendanceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim date_check As Date
            Dim join_date As New Date
            Dim ter_effect_date As New Date
            rep.Get_Date(EmpId, join_date, ter_effect_date)
            If join_date <> date_check Then
                If Convert.ToDateTime(lbDayEdit16.Text) < join_date Then
                    cboCode16.ClearSelection()
                    ShowMessage(Translate("Ngày thay đổi đang nhỏ hơn ngày vào làm việc."), NotifyType.Error)
                End If
            End If
            If ter_effect_date <> date_check Then
                If Convert.ToDateTime(lbDayEdit16.Text) > ter_effect_date Then
                    cboCode16.ClearSelection()
                    ShowMessage(Translate("Ngày thay đổi đang lớn hơn ngày nghỉ việc."), NotifyType.Error)
                End If
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub cboCode17_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCode17.SelectedIndexChanged
        Dim rep As New AttendanceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim date_check As Date
            Dim join_date As New Date
            Dim ter_effect_date As New Date
            rep.Get_Date(EmpId, join_date, ter_effect_date)
            If join_date <> date_check Then
                If Convert.ToDateTime(lbDayEdit17.Text) < join_date Then
                    cboCode17.ClearSelection()
                    ShowMessage(Translate("Ngày thay đổi đang nhỏ hơn ngày vào làm việc."), NotifyType.Error)
                End If
            End If
            If ter_effect_date <> date_check Then
                If Convert.ToDateTime(lbDayEdit17.Text) > ter_effect_date Then
                    cboCode17.ClearSelection()
                    ShowMessage(Translate("Ngày thay đổi đang lớn hơn ngày nghỉ việc."), NotifyType.Error)
                End If
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub cboCode18_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCode18.SelectedIndexChanged
        Dim rep As New AttendanceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim date_check As Date
            Dim join_date As New Date
            Dim ter_effect_date As New Date
            rep.Get_Date(EmpId, join_date, ter_effect_date)
            If join_date <> date_check Then
                If Convert.ToDateTime(lbDayEdit18.Text) < join_date Then
                    cboCode18.ClearSelection()
                    ShowMessage(Translate("Ngày thay đổi đang nhỏ hơn ngày vào làm việc."), NotifyType.Error)
                End If
            End If
            If ter_effect_date <> date_check Then
                If Convert.ToDateTime(lbDayEdit18.Text) > ter_effect_date Then
                    cboCode18.ClearSelection()
                    ShowMessage(Translate("Ngày thay đổi đang lớn hơn ngày nghỉ việc."), NotifyType.Error)
                End If
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub cboCode19_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCode19.SelectedIndexChanged
        Dim rep As New AttendanceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim date_check As Date
            Dim join_date As New Date
            Dim ter_effect_date As New Date
            rep.Get_Date(EmpId, join_date, ter_effect_date)
            If join_date <> date_check Then
                If Convert.ToDateTime(lbDayEdit19.Text) < join_date Then
                    cboCode19.ClearSelection()
                    ShowMessage(Translate("Ngày thay đổi đang nhỏ hơn ngày vào làm việc."), NotifyType.Error)
                End If
            End If
            If ter_effect_date <> date_check Then
                If Convert.ToDateTime(lbDayEdit19.Text) > ter_effect_date Then
                    cboCode19.ClearSelection()
                    ShowMessage(Translate("Ngày thay đổi đang lớn hơn ngày nghỉ việc."), NotifyType.Error)
                End If
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub cboCode20_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCode20.SelectedIndexChanged
        Dim rep As New AttendanceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim date_check As Date
            Dim join_date As New Date
            Dim ter_effect_date As New Date
            rep.Get_Date(EmpId, join_date, ter_effect_date)
            If join_date <> date_check Then
                If Convert.ToDateTime(lbDayEdit20.Text) < join_date Then
                    cboCode20.ClearSelection()
                    ShowMessage(Translate("Ngày thay đổi đang nhỏ hơn ngày vào làm việc."), NotifyType.Error)
                End If
            End If
            If ter_effect_date <> date_check Then
                If Convert.ToDateTime(lbDayEdit20.Text) > ter_effect_date Then
                    cboCode20.ClearSelection()
                    ShowMessage(Translate("Ngày thay đổi đang lớn hơn ngày nghỉ việc."), NotifyType.Error)
                End If
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub cboCode21_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCode21.SelectedIndexChanged
        Dim rep As New AttendanceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim date_check As Date
            Dim join_date As New Date
            Dim ter_effect_date As New Date
            rep.Get_Date(EmpId, join_date, ter_effect_date)
            If join_date <> date_check Then
                If Convert.ToDateTime(lbDayEdit21.Text) < join_date Then
                    cboCode21.ClearSelection()
                    ShowMessage(Translate("Ngày thay đổi đang nhỏ hơn ngày vào làm việc."), NotifyType.Error)
                End If
            End If
            If ter_effect_date <> date_check Then
                If Convert.ToDateTime(lbDayEdit21.Text) > ter_effect_date Then
                    cboCode21.ClearSelection()
                    ShowMessage(Translate("Ngày thay đổi đang lớn hơn ngày nghỉ việc."), NotifyType.Error)
                End If
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub cboCode22_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCode22.SelectedIndexChanged
        Dim rep As New AttendanceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim date_check As Date
            Dim join_date As New Date
            Dim ter_effect_date As New Date
            rep.Get_Date(EmpId, join_date, ter_effect_date)
            If join_date <> date_check Then
                If Convert.ToDateTime(lbDayEdit22.Text) < join_date Then
                    cboCode22.ClearSelection()
                    ShowMessage(Translate("Ngày thay đổi đang nhỏ hơn ngày vào làm việc."), NotifyType.Error)
                End If
            End If
            If ter_effect_date <> date_check Then
                If Convert.ToDateTime(lbDayEdit22.Text) > ter_effect_date Then
                    cboCode22.ClearSelection()
                    ShowMessage(Translate("Ngày thay đổi đang lớn hơn ngày nghỉ việc."), NotifyType.Error)
                End If
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub cboCode23_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCode23.SelectedIndexChanged
        Dim rep As New AttendanceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim date_check As Date
            Dim join_date As New Date
            Dim ter_effect_date As New Date
            rep.Get_Date(EmpId, join_date, ter_effect_date)
            If join_date <> date_check Then
                If Convert.ToDateTime(lbDayEdit23.Text) < join_date Then
                    cboCode23.ClearSelection()
                    ShowMessage(Translate("Ngày thay đổi đang nhỏ hơn ngày vào làm việc."), NotifyType.Error)
                End If
            End If
            If ter_effect_date <> date_check Then
                If Convert.ToDateTime(lbDayEdit23.Text) > ter_effect_date Then
                    cboCode23.ClearSelection()
                    ShowMessage(Translate("Ngày thay đổi đang lớn hơn ngày nghỉ việc."), NotifyType.Error)
                End If
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub cboCode24_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCode24.SelectedIndexChanged
        Dim rep As New AttendanceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim date_check As Date
            Dim join_date As New Date
            Dim ter_effect_date As New Date
            rep.Get_Date(EmpId, join_date, ter_effect_date)
            If join_date <> date_check Then
                If Convert.ToDateTime(lbDayEdit24.Text) < join_date Then
                    cboCode24.ClearSelection()
                    ShowMessage(Translate("Ngày thay đổi đang nhỏ hơn ngày vào làm việc."), NotifyType.Error)
                End If
            End If
            If ter_effect_date <> date_check Then
                If Convert.ToDateTime(lbDayEdit24.Text) > ter_effect_date Then
                    cboCode24.ClearSelection()
                    ShowMessage(Translate("Ngày thay đổi đang lớn hơn ngày nghỉ việc."), NotifyType.Error)
                End If
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub cboCode25_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCode25.SelectedIndexChanged
        Dim rep As New AttendanceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim date_check As Date
            Dim join_date As New Date
            Dim ter_effect_date As New Date
            rep.Get_Date(EmpId, join_date, ter_effect_date)
            If join_date <> date_check Then
                If Convert.ToDateTime(lbDayEdit25.Text) < join_date Then
                    cboCode25.ClearSelection()
                    ShowMessage(Translate("Ngày thay đổi đang nhỏ hơn ngày vào làm việc."), NotifyType.Error)
                End If
            End If
            If ter_effect_date <> date_check Then
                If Convert.ToDateTime(lbDayEdit25.Text) > ter_effect_date Then
                    cboCode25.ClearSelection()
                    ShowMessage(Translate("Ngày thay đổi đang lớn hơn ngày nghỉ việc."), NotifyType.Error)
                End If
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub cboCode26_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCode26.SelectedIndexChanged
        Dim rep As New AttendanceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim date_check As Date
            Dim join_date As New Date
            Dim ter_effect_date As New Date
            rep.Get_Date(EmpId, join_date, ter_effect_date)
            If join_date <> date_check Then
                If Convert.ToDateTime(lbDayEdit26.Text) < join_date Then
                    cboCode26.ClearSelection()
                    ShowMessage(Translate("Ngày thay đổi đang nhỏ hơn ngày vào làm việc."), NotifyType.Error)
                End If
            End If
            If ter_effect_date <> date_check Then
                If Convert.ToDateTime(lbDayEdit26.Text) > ter_effect_date Then
                    cboCode26.ClearSelection()
                    ShowMessage(Translate("Ngày thay đổi đang lớn hơn ngày nghỉ việc."), NotifyType.Error)
                End If
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub cboCode27_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCode27.SelectedIndexChanged
        Dim rep As New AttendanceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim date_check As Date
            Dim join_date As New Date
            Dim ter_effect_date As New Date
            rep.Get_Date(EmpId, join_date, ter_effect_date)
            If join_date <> date_check Then
                If Convert.ToDateTime(lbDayEdit27.Text) < join_date Then
                    cboCode27.ClearSelection()
                    ShowMessage(Translate("Ngày thay đổi đang nhỏ hơn ngày vào làm việc."), NotifyType.Error)
                End If
            End If
            If ter_effect_date <> date_check Then
                If Convert.ToDateTime(lbDayEdit27.Text) > ter_effect_date Then
                    cboCode27.ClearSelection()
                    ShowMessage(Translate("Ngày thay đổi đang lớn hơn ngày nghỉ việc."), NotifyType.Error)
                End If
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub cboCode28_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCode28.SelectedIndexChanged
        Dim rep As New AttendanceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim date_check As Date
            Dim join_date As New Date
            Dim ter_effect_date As New Date
            rep.Get_Date(EmpId, join_date, ter_effect_date)
            If join_date <> date_check Then
                If Convert.ToDateTime(lbDayEdit28.Text) < join_date Then
                    cboCode28.ClearSelection()
                    ShowMessage(Translate("Ngày thay đổi đang nhỏ hơn ngày vào làm việc."), NotifyType.Error)
                End If
            End If
            If ter_effect_date <> date_check Then
                If Convert.ToDateTime(lbDayEdit28.Text) > ter_effect_date Then
                    cboCode28.ClearSelection()
                    ShowMessage(Translate("Ngày thay đổi đang lớn hơn ngày nghỉ việc."), NotifyType.Error)
                End If
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub cboCode29_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCode29.SelectedIndexChanged
        Dim rep As New AttendanceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim date_check As Date
            Dim join_date As New Date
            Dim ter_effect_date As New Date
            rep.Get_Date(EmpId, join_date, ter_effect_date)
            If join_date <> date_check Then
                If Convert.ToDateTime(lbDayEdit29.Text) < join_date Then
                    cboCode29.ClearSelection()
                    ShowMessage(Translate("Ngày thay đổi đang nhỏ hơn ngày vào làm việc."), NotifyType.Error)
                End If
            End If
            If ter_effect_date <> date_check Then
                If Convert.ToDateTime(lbDayEdit29.Text) > ter_effect_date Then
                    cboCode29.ClearSelection()
                    ShowMessage(Translate("Ngày thay đổi đang lớn hơn ngày nghỉ việc."), NotifyType.Error)
                End If
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub cboCode30_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCode30.SelectedIndexChanged
        Dim rep As New AttendanceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim date_check As Date
            Dim join_date As New Date
            Dim ter_effect_date As New Date
            rep.Get_Date(EmpId, join_date, ter_effect_date)
            If join_date <> date_check Then
                If Convert.ToDateTime(lbDayEdit30.Text) < join_date Then
                    cboCode30.ClearSelection()
                    ShowMessage(Translate("Ngày thay đổi đang nhỏ hơn ngày vào làm việc."), NotifyType.Error)
                End If
            End If
            If ter_effect_date <> date_check Then
                If Convert.ToDateTime(lbDayEdit30.Text) > ter_effect_date Then
                    cboCode30.ClearSelection()
                    ShowMessage(Translate("Ngày thay đổi đang lớn hơn ngày nghỉ việc."), NotifyType.Error)
                End If
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub cboCode31_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCode31.SelectedIndexChanged
        Dim rep As New AttendanceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim date_check As Date
            Dim join_date As New Date
            Dim ter_effect_date As New Date
            rep.Get_Date(EmpId, join_date, ter_effect_date)
            If join_date <> date_check Then
                If Convert.ToDateTime(lbDayEdit31.Text) < join_date Then
                    cboCode31.ClearSelection()
                    ShowMessage(Translate("Ngày thay đổi đang nhỏ hơn ngày vào làm việc."), NotifyType.Error)
                End If
            End If
            If ter_effect_date <> date_check Then
                If Convert.ToDateTime(lbDayEdit31.Text) > ter_effect_date Then
                    cboCode31.ClearSelection()
                    ShowMessage(Translate("Ngày thay đổi đang lớn hơn ngày nghỉ việc."), NotifyType.Error)
                End If
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Custom"
    ''' <lastupdate>
    ''' 110/08/2017 09:55
    ''' </lastupdate>
    ''' <summary>
    ''' Khi click Huy popup thi gan isLoadPopup = 0
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindPopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.CancelClicked
        isLoadPopup = 0
    End Sub
    ''' <lastupdate>
    ''' 17/08/2017 09:55
    ''' </lastupdate>
    ''' <summary>
    ''' Cap nhat trang thai cac control tren trang
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow

            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                                   CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try

    End Sub
    ''' <lastupdate>
    ''' 17/08/2017 09:55
    ''' </lastupdate>
    ''' <summary>
    ''' Lay danh sach combobox
    ''' fill du lieu cho combobox tren trang
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetDataCombo()
        Dim rep As New AttendanceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim dtdata As DataTable = rep.GET_AT_SHIFT()
            FillRadCombobox(cboCode1, dtdata, "CODE", "ID", True)
            FillRadCombobox(cboCode2, dtdata, "CODE", "ID", True)
            FillRadCombobox(cboCode3, dtdata, "CODE", "ID", True)
            FillRadCombobox(cboCode4, dtdata, "CODE", "ID", True)
            FillRadCombobox(cboCode5, dtdata, "CODE", "ID", True)
            FillRadCombobox(cboCode6, dtdata, "CODE", "ID", True)
            FillRadCombobox(cboCode7, dtdata, "CODE", "ID", True)
            FillRadCombobox(cboCode8, dtdata, "CODE", "ID", True)
            FillRadCombobox(cboCode9, dtdata, "CODE", "ID", True)
            FillRadCombobox(cboCode10, dtdata, "CODE", "ID", True)
            FillRadCombobox(cboCode11, dtdata, "CODE", "ID", True)
            FillRadCombobox(cboCode12, dtdata, "CODE", "ID", True)
            FillRadCombobox(cboCode13, dtdata, "CODE", "ID", True)
            FillRadCombobox(cboCode14, dtdata, "CODE", "ID", True)
            FillRadCombobox(cboCode15, dtdata, "CODE", "ID", True)
            FillRadCombobox(cboCode16, dtdata, "CODE", "ID", True)
            FillRadCombobox(cboCode17, dtdata, "CODE", "ID", True)
            FillRadCombobox(cboCode18, dtdata, "CODE", "ID", True)
            FillRadCombobox(cboCode19, dtdata, "CODE", "ID", True)
            FillRadCombobox(cboCode20, dtdata, "CODE", "ID", True)
            FillRadCombobox(cboCode21, dtdata, "CODE", "ID", True)
            FillRadCombobox(cboCode22, dtdata, "CODE", "ID", True)
            FillRadCombobox(cboCode23, dtdata, "CODE", "ID", True)
            FillRadCombobox(cboCode24, dtdata, "CODE", "ID", True)
            FillRadCombobox(cboCode25, dtdata, "CODE", "ID", True)
            FillRadCombobox(cboCode26, dtdata, "CODE", "ID", True)
            FillRadCombobox(cboCode27, dtdata, "CODE", "ID", True)
            FillRadCombobox(cboCode28, dtdata, "CODE", "ID", True)
            FillRadCombobox(cboCode29, dtdata, "CODE", "ID", True)
            FillRadCombobox(cboCode30, dtdata, "CODE", "ID", True)
            FillRadCombobox(cboCode31, dtdata, "CODE", "ID", True)


            dtDataD = rep.GET_AT_WORKSIGN_EDIT(EmpId, startDate, EndDate)
            Dim i As Integer = 0
            For Each row As DataRow In dtDataD.Rows
                i = i + 1

                If i = 1 Then
                    tr1.Visible = True
                    lbSTT1.Text = row("STT")
                    lbDay1.Text = row("WORKINGDAY")
                    lbCode1.Text = If(IsDBNull(row("SHIFT_CODE")), Nothing, row("SHIFT_CODE"))
                    lbDayEdit1.Text = row("WORKINGDAY")
                    cboCode1.SelectedValue = If(IsDBNull(row("SHIFT_ID")), Nothing, row("SHIFT_ID"))
                End If

                If i = 2 Then
                    tr2.Visible = True
                    lbSTT2.Text = row("STT")
                    lbDay2.Text = row("WORKINGDAY")
                    lbCode2.Text = If(IsDBNull(row("SHIFT_CODE")), Nothing, row("SHIFT_CODE"))
                    lbDayEdit2.Text = row("WORKINGDAY")
                    cboCode2.SelectedValue = If(IsDBNull(row("SHIFT_ID")), Nothing, row("SHIFT_ID"))
                End If

                If i = 3 Then
                    tr3.Visible = True
                    lbSTT3.Text = row("STT")
                    lbDay3.Text = row("WORKINGDAY")
                    lbCode3.Text = If(IsDBNull(row("SHIFT_CODE")), Nothing, row("SHIFT_CODE"))
                    lbDayEdit3.Text = row("WORKINGDAY")
                    cboCode3.SelectedValue = If(IsDBNull(row("SHIFT_ID")), Nothing, row("SHIFT_ID"))
                End If

                If i = 4 Then
                    tr4.Visible = True
                    lbSTT4.Text = row("STT")
                    lbDay4.Text = row("WORKINGDAY")
                    lbCode4.Text = If(IsDBNull(row("SHIFT_CODE")), Nothing, row("SHIFT_CODE"))
                    lbDayEdit4.Text = row("WORKINGDAY")
                    cboCode4.SelectedValue = If(IsDBNull(row("SHIFT_ID")), Nothing, row("SHIFT_ID"))
                End If

                If i = 5 Then
                    tr5.Visible = True
                    lbSTT5.Text = row("STT")
                    lbDay5.Text = row("WORKINGDAY")
                    lbCode5.Text = If(IsDBNull(row("SHIFT_CODE")), Nothing, row("SHIFT_CODE"))
                    lbDayEdit5.Text = row("WORKINGDAY")
                    cboCode5.SelectedValue = If(IsDBNull(row("SHIFT_ID")), Nothing, row("SHIFT_ID"))
                End If

                If i = 6 Then
                    tr6.Visible = True
                    lbSTT6.Text = row("STT")
                    lbDay6.Text = row("WORKINGDAY")
                    lbCode6.Text = If(IsDBNull(row("SHIFT_CODE")), Nothing, row("SHIFT_CODE"))
                    lbDayEdit6.Text = row("WORKINGDAY")
                    cboCode6.SelectedValue = If(IsDBNull(row("SHIFT_ID")), Nothing, row("SHIFT_ID"))
                End If

                If i = 7 Then
                    tr7.Visible = True
                    lbSTT7.Text = row("STT")
                    lbDay7.Text = row("WORKINGDAY")
                    lbCode7.Text = If(IsDBNull(row("SHIFT_CODE")), Nothing, row("SHIFT_CODE"))
                    lbDayEdit7.Text = row("WORKINGDAY")
                    cboCode7.SelectedValue = If(IsDBNull(row("SHIFT_ID")), Nothing, row("SHIFT_ID"))
                End If

                If i = 8 Then
                    tr8.Visible = True
                    lbSTT8.Text = row("STT")
                    lbDay8.Text = row("WORKINGDAY")
                    lbCode8.Text = If(IsDBNull(row("SHIFT_CODE")), Nothing, row("SHIFT_CODE"))
                    lbDayEdit8.Text = row("WORKINGDAY")
                    cboCode8.SelectedValue = If(IsDBNull(row("SHIFT_ID")), Nothing, row("SHIFT_ID"))
                End If

                If i = 9 Then
                    tr9.Visible = True
                    lbSTT9.Text = row("STT")
                    lbDay9.Text = row("WORKINGDAY")
                    lbCode9.Text = If(IsDBNull(row("SHIFT_CODE")), Nothing, row("SHIFT_CODE"))
                    lbDayEdit9.Text = row("WORKINGDAY")
                    cboCode9.SelectedValue = If(IsDBNull(row("SHIFT_ID")), Nothing, row("SHIFT_ID"))
                End If

                If i = 10 Then
                    tr10.Visible = True
                    lbSTT10.Text = row("STT")
                    lbDay10.Text = row("WORKINGDAY")
                    lbCode10.Text = If(IsDBNull(row("SHIFT_CODE")), Nothing, row("SHIFT_CODE"))
                    lbDayEdit10.Text = row("WORKINGDAY")
                    cboCode10.SelectedValue = If(IsDBNull(row("SHIFT_ID")), Nothing, row("SHIFT_ID"))
                End If

                If i = 11 Then
                    tr11.Visible = True
                    lbSTT11.Text = row("STT")
                    lbDay11.Text = row("WORKINGDAY")
                    lbCode11.Text = If(IsDBNull(row("SHIFT_CODE")), Nothing, row("SHIFT_CODE"))
                    lbDayEdit11.Text = row("WORKINGDAY")
                    cboCode11.SelectedValue = If(IsDBNull(row("SHIFT_ID")), Nothing, row("SHIFT_ID"))
                End If

                If i = 12 Then
                    tr12.Visible = True
                    lbSTT12.Text = row("STT")
                    lbDay12.Text = row("WORKINGDAY")
                    lbCode12.Text = If(IsDBNull(row("SHIFT_CODE")), Nothing, row("SHIFT_CODE"))
                    lbDayEdit12.Text = row("WORKINGDAY")
                    cboCode12.SelectedValue = If(IsDBNull(row("SHIFT_ID")), Nothing, row("SHIFT_ID"))
                End If

                If i = 13 Then
                    tr13.Visible = True
                    lbSTT13.Text = row("STT")
                    lbDay13.Text = row("WORKINGDAY")
                    lbCode13.Text = If(IsDBNull(row("SHIFT_CODE")), Nothing, row("SHIFT_CODE"))
                    lbDayEdit13.Text = row("WORKINGDAY")
                    cboCode13.SelectedValue = If(IsDBNull(row("SHIFT_ID")), Nothing, row("SHIFT_ID"))
                End If

                If i = 14 Then
                    tr14.Visible = True
                    lbSTT14.Text = row("STT")
                    lbDay14.Text = row("WORKINGDAY")
                    lbCode14.Text = If(IsDBNull(row("SHIFT_CODE")), Nothing, row("SHIFT_CODE"))
                    lbDayEdit14.Text = row("WORKINGDAY")
                    cboCode14.SelectedValue = If(IsDBNull(row("SHIFT_ID")), Nothing, row("SHIFT_ID"))
                End If

                If i = 15 Then
                    tr15.Visible = True
                    lbSTT15.Text = row("STT")
                    lbDay15.Text = row("WORKINGDAY")
                    lbCode15.Text = If(IsDBNull(row("SHIFT_CODE")), Nothing, row("SHIFT_CODE"))
                    lbDayEdit15.Text = row("WORKINGDAY")
                    cboCode15.SelectedValue = If(IsDBNull(row("SHIFT_ID")), Nothing, row("SHIFT_ID"))
                End If

                If i = 16 Then
                    tr16.Visible = True
                    lbSTT16.Text = row("STT")
                    lbDay16.Text = row("WORKINGDAY")
                    lbCode16.Text = If(IsDBNull(row("SHIFT_CODE")), Nothing, row("SHIFT_CODE"))
                    lbDayEdit16.Text = row("WORKINGDAY")
                    cboCode16.SelectedValue = If(IsDBNull(row("SHIFT_ID")), Nothing, row("SHIFT_ID"))
                End If

                If i = 17 Then
                    tr17.Visible = True
                    lbSTT17.Text = row("STT")
                    lbDay17.Text = row("WORKINGDAY")
                    lbCode17.Text = If(IsDBNull(row("SHIFT_CODE")), Nothing, row("SHIFT_CODE"))
                    lbDayEdit17.Text = row("WORKINGDAY")
                    cboCode17.SelectedValue = If(IsDBNull(row("SHIFT_ID")), Nothing, row("SHIFT_ID"))
                End If

                If i = 18 Then
                    tr18.Visible = True
                    lbSTT18.Text = row("STT")
                    lbDay18.Text = row("WORKINGDAY")
                    lbCode18.Text = If(IsDBNull(row("SHIFT_CODE")), Nothing, row("SHIFT_CODE"))
                    lbDayEdit18.Text = row("WORKINGDAY")
                    cboCode18.SelectedValue = If(IsDBNull(row("SHIFT_ID")), Nothing, row("SHIFT_ID"))
                End If

                If i = 19 Then
                    tr19.Visible = True
                    lbSTT19.Text = row("STT")
                    lbDay19.Text = row("WORKINGDAY")
                    lbCode19.Text = If(IsDBNull(row("SHIFT_CODE")), Nothing, row("SHIFT_CODE"))
                    lbDayEdit19.Text = row("WORKINGDAY")
                    cboCode19.SelectedValue = If(IsDBNull(row("SHIFT_ID")), Nothing, row("SHIFT_ID"))
                End If

                If i = 20 Then
                    tr20.Visible = True
                    lbSTT20.Text = row("STT")
                    lbDay20.Text = row("WORKINGDAY")
                    lbCode20.Text = If(IsDBNull(row("SHIFT_CODE")), Nothing, row("SHIFT_CODE"))
                    lbDayEdit20.Text = row("WORKINGDAY")
                    cboCode20.SelectedValue = If(IsDBNull(row("SHIFT_ID")), Nothing, row("SHIFT_ID"))
                End If

                If i = 21 Then
                    tr21.Visible = True
                    lbSTT21.Text = row("STT")
                    lbDay21.Text = row("WORKINGDAY")
                    lbCode21.Text = If(IsDBNull(row("SHIFT_CODE")), Nothing, row("SHIFT_CODE"))
                    lbDayEdit21.Text = row("WORKINGDAY")
                    cboCode21.SelectedValue = If(IsDBNull(row("SHIFT_ID")), Nothing, row("SHIFT_ID"))
                End If

                If i = 22 Then
                    tr22.Visible = True
                    lbSTT22.Text = row("STT")
                    lbDay22.Text = row("WORKINGDAY")
                    lbCode22.Text = If(IsDBNull(row("SHIFT_CODE")), Nothing, row("SHIFT_CODE"))
                    lbDayEdit22.Text = row("WORKINGDAY")
                    cboCode22.SelectedValue = If(IsDBNull(row("SHIFT_ID")), Nothing, row("SHIFT_ID"))
                End If

                If i = 23 Then
                    tr23.Visible = True
                    lbSTT23.Text = row("STT")
                    lbDay23.Text = row("WORKINGDAY")
                    lbCode23.Text = If(IsDBNull(row("SHIFT_CODE")), Nothing, row("SHIFT_CODE"))
                    lbDayEdit23.Text = row("WORKINGDAY")
                    cboCode23.SelectedValue = If(IsDBNull(row("SHIFT_ID")), Nothing, row("SHIFT_ID"))
                End If

                If i = 24 Then
                    tr24.Visible = True
                    lbSTT24.Text = row("STT")
                    lbDay24.Text = row("WORKINGDAY")
                    lbCode24.Text = If(IsDBNull(row("SHIFT_CODE")), Nothing, row("SHIFT_CODE"))
                    lbDayEdit24.Text = row("WORKINGDAY")
                    cboCode24.SelectedValue = If(IsDBNull(row("SHIFT_ID")), Nothing, row("SHIFT_ID"))
                End If

                If i = 25 Then
                    tr25.Visible = True
                    lbSTT25.Text = row("STT")
                    lbDay25.Text = row("WORKINGDAY")
                    lbCode25.Text = If(IsDBNull(row("SHIFT_CODE")), Nothing, row("SHIFT_CODE"))
                    lbDayEdit25.Text = row("WORKINGDAY")
                    cboCode25.SelectedValue = If(IsDBNull(row("SHIFT_ID")), Nothing, row("SHIFT_ID"))
                End If

                If i = 26 Then
                    tr26.Visible = True
                    lbSTT26.Text = row("STT")
                    lbDay26.Text = row("WORKINGDAY")
                    lbCode26.Text = If(IsDBNull(row("SHIFT_CODE")), Nothing, row("SHIFT_CODE"))
                    lbDayEdit26.Text = row("WORKINGDAY")
                    cboCode26.SelectedValue = If(IsDBNull(row("SHIFT_ID")), Nothing, row("SHIFT_ID"))
                End If

                If i = 27 Then
                    tr27.Visible = True
                    lbSTT27.Text = row("STT")
                    lbDay27.Text = row("WORKINGDAY")
                    lbCode27.Text = If(IsDBNull(row("SHIFT_CODE")), Nothing, row("SHIFT_CODE"))
                    lbDayEdit27.Text = row("WORKINGDAY")
                    cboCode27.SelectedValue = If(IsDBNull(row("SHIFT_ID")), Nothing, row("SHIFT_ID"))
                End If

                If i = 28 Then
                    tr28.Visible = True
                    lbSTT28.Text = row("STT")
                    lbDay28.Text = row("WORKINGDAY")
                    lbCode28.Text = If(IsDBNull(row("SHIFT_CODE")), Nothing, row("SHIFT_CODE"))
                    lbDayEdit28.Text = row("WORKINGDAY")
                    cboCode28.SelectedValue = If(IsDBNull(row("SHIFT_ID")), Nothing, row("SHIFT_ID"))
                End If

                If i = 29 Then
                    tr29.Visible = True
                    lbSTT29.Text = row("STT")
                    lbDay29.Text = row("WORKINGDAY")
                    lbCode29.Text = If(IsDBNull(row("SHIFT_CODE")), Nothing, row("SHIFT_CODE"))
                    lbDayEdit29.Text = row("WORKINGDAY")
                    cboCode29.SelectedValue = If(IsDBNull(row("SHIFT_ID")), Nothing, row("SHIFT_ID"))
                End If

                If i = 30 Then
                    tr30.Visible = True
                    lbSTT30.Text = row("STT")
                    lbDay30.Text = row("WORKINGDAY")
                    lbCode30.Text = If(IsDBNull(row("SHIFT_CODE")), Nothing, row("SHIFT_CODE"))
                    lbDayEdit30.Text = row("WORKINGDAY")
                    cboCode30.SelectedValue = If(IsDBNull(row("SHIFT_ID")), Nothing, row("SHIFT_ID"))
                End If

                If i = 31 Then
                    tr31.Visible = True
                    lbSTT31.Text = row("STT")
                    lbDay31.Text = row("WORKINGDAY")
                    lbCode31.Text = If(IsDBNull(row("SHIFT_CODE")), Nothing, row("SHIFT_CODE"))
                    lbDayEdit31.Text = row("WORKINGDAY")
                    cboCode31.SelectedValue = If(IsDBNull(row("SHIFT_ID")), Nothing, row("SHIFT_ID"))
                End If
            Next

            rep.Dispose()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                                   CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub

#End Region


End Class

