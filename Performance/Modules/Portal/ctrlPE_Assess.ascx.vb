Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Performance.PerformanceBusiness
Imports Telerik.Web.UI


Public Class ctrlPE_Assess
    Inherits Common.CommonView
    Protected WithEvents ViewItem As ViewBase
    Public Overrides Property MustAuthorize As Boolean = False
#Region "Property"

    Property IDSelect As Decimal
        Get
            Return ViewState(Me.ID & "_IDSelect")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_IDSelect") = value
        End Set
    End Property

    ''' <summary>
    ''' AT_SHIFT
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Classifications As List(Of AssessmentDTO)
        Get
            Return ViewState(Me.ID & "_Classifications")
        End Get
        Set(ByVal value As List(Of AssessmentDTO))
            ViewState(Me.ID & "_Classifications") = value
        End Set
    End Property
#End Region
    Public iYear As Decimal?
    Public iPeriodId As Decimal?
    Public iTypeAssId As Decimal?
    Public iEmpId As Decimal?
    Public status As Decimal?
    Public isDirect As Boolean = False
    Public Property EmployeeID As Decimal
#Region "Page"
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            Refresh()
            UpdateControlState()
            rgMain.SetFilter()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        rgMain.AllowCustomPaging = True
        InitControl()
    End Sub

    Protected Sub InitControl()
        Try
            Me.MainToolBar = tbarMainToolBar
            Me.ctrlMessageBox.Listener = Me
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Edit, ToolbarItem.Save, ToolbarItem.Cancel, ToolbarItem.Approve, ToolbarItem.Create)
            CType(MainToolBar.Items(1), RadToolBarButton).CausesValidation = True
            CType(Me.MainToolBar.Items(1), RadToolBarButton).Enabled = False
            CType(Me.MainToolBar.Items(2), RadToolBarButton).Enabled = False
            CType(MainToolBar.Items(3), RadToolBarButton).Text = "Đã đánh giá"
            CType(MainToolBar.Items(4), RadToolBarButton).Text = "Lịch sử phê duyệt"
            CType(MainToolBar.Items(4), RadToolBarButton).ImageUrl = CType(MainToolBar.Items(3), RadToolBarButton).ImageUrl
            isDiv1.Visible = False

            iYear = Request.QueryString("Year")
            iPeriodId = Request.QueryString("PeriodId")
            iTypeAssId = Request.QueryString("TypeAssId")
            iEmpId = Request.QueryString("EmpId")
            status = Request.QueryString("status")
            If (iYear IsNot Nothing AndAlso iYear > 0) And (iPeriodId IsNot Nothing AndAlso iPeriodId > 0) And (iTypeAssId IsNot Nothing AndAlso iTypeAssId > 0) And (iEmpId IsNot Nothing AndAlso iEmpId > 0) Then
                Dim lstEmployeeData As List(Of EmployeeDTO)
                Using rep As New PerformanceRepository
                    Dim _filter = New EmployeeDTO
                    _filter.DIRECT_MANAGER = EmployeeID
                    _filter.ID = iEmpId
                    lstEmployeeData = rep.GetListEmployeePortal(_filter)
                End Using
                If lstEmployeeData.Count <= 0 Then
                    Response.Redirect("/")
                Else
                    isDirect = True
                End If
                'If iEmpId <> EmployeeID Then
                CType(MainToolBar.Items(0), RadToolBarButton).Text = "Đánh giá kết quả công việc"
                'Else
                '    CType(MainToolBar.Items(0), RadToolBarButton).Text = "Nhập kết quả thực hiện KPI"
                'End If

                CType(MainToolBar.Items(3), RadToolBarButton).Text = "Duyệt"

                If status = 1 Or status = 2 Then
                    CType(Me.MainToolBar.Items(0), RadToolBarButton).Enabled = False
                    CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = False
                    isDiv1.Visible = False
                Else
                    isDiv1.Visible = True
                End If
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New PerformanceRepository
        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                Select Case Message
                    Case "ModifyView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgMain.Rebind()
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgMain.CurrentPageIndex = 0
                        rgMain.MasterTableView.SortExpressions.Clear()
                        rgMain.Rebind()
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "Cancel"
                        rgMain.MasterTableView.ClearSelectedItems()
                End Select
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Protected Sub GetTotalPointRating()
        Dim rep As New PerformanceRepository
        Dim _filter As New AssessmentDTO
        Dim dt As New DataTable
        Try
            _filter.PE_PERIO_ID = Utilities.ObjToInt(cboPeriod.SelectedValue)
            _filter.EMPLOYEE_ID = lblEmpID.Text
            _filter.PE_OBJECT_ID = Utilities.ObjToInt(hidPE_OBJECT_ID.Value)
            _filter.PE_EMPLOYEE_ASSESSMENT_ID = Utilities.ObjToInt(hidPE_EMPLOYEE_ASSESSMENT_ID.Value)
            dt = rep.GetTotalPointRating(_filter)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 AndAlso (dt.Rows(0)(0) IsNot DBNull.Value And dt.Rows(0)(1) IsNot DBNull.Value) Then
                lblTotalPoint.Text = dt.Rows(0)("TOTAL")
                lblRating.Text = dt.Rows(0)("RATING")
                lblTotalPointQL.Text = If(IsDBNull(dt.Rows(0)("TOTALQL")), String.Empty, dt.Rows(0)("TOTALQL"))
                lblRatingQL.Text = If(IsDBNull(dt.Rows(0)("RATINGQL")), String.Empty, dt.Rows(0)("RATINGQL"))
            Else
                lblTotalPoint.Text = String.Empty
                lblRating.Text = String.Empty
                lblTotalPointQL.Text = String.Empty
                lblRatingQL.Text = String.Empty
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim rep As New PerformanceRepository
        Dim _filter As New AssessmentDTO
        Try
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgMain, _filter)
            Dim Sorts As String = rgMain.MasterTableView.SortExpressions.GetSortString()
            _filter.PE_PERIO_ID = Utilities.ObjToInt(cboPeriod.SelectedValue)
            _filter.EMPLOYEE_ID = lblEmpID.Text
            _filter.PE_OBJECT_ID = Utilities.ObjToInt(hidPE_OBJECT_ID.Value)
            If isFull Then
                If Sorts IsNot Nothing Then
                    Return rep.GetAssessment(_filter, Sorts).ToTable()
                Else
                    Return rep.GetAssessment(_filter).ToTable()
                End If
            Else
                'Dim Classifications As List(Of AssessmentDTO)
                If Sorts IsNot Nothing Then
                    Classifications = rep.GetAssessment(_filter, rgMain.CurrentPageIndex, rgMain.PageSize, MaximumRows, Sorts)
                Else
                    Classifications = rep.GetAssessment(_filter, rgMain.CurrentPageIndex, rgMain.PageSize, MaximumRows)
                End If

                rgMain.VirtualItemCount = MaximumRows
                rgMain.DataSource = Classifications
            End If
            GetTotalPointRating()
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Overrides Sub UpdateControlState()
        Dim rep As New PerformanceRepository
        Try
            Select Case CurrentState
                Case CommonMessage.STATE_EDIT
                    CType(Me.MainToolBar.Items(1), RadToolBarButton).Enabled = True
                    CType(Me.MainToolBar.Items(2), RadToolBarButton).Enabled = True
                    CType(Me.MainToolBar.Items(0), RadToolBarButton).Enabled = False
                    CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = False
                    EnabledGridNotPostback(rgMain, False)
                    'common'
                    EnableControlAll(False, txtYear, cboTypeAss, cboPeriod, rdStartDate, rdEndDate)
                    If Not isDirect Then
                        'Employee'
                        EnableControlAll(True, txtResult, rdUpdateDate, cboEmp_Status, txtRemark)
                    Else
                        'Direct'
                        EnableControlAll(True, txtResult_Dir, rdAss_Date, txtRemark_Dir, cboTrangThai)
                    End If
                Case CommonMessage.STATE_NORMAL
                    EnabledGridNotPostback(rgMain, True)
                    CType(Me.MainToolBar.Items(1), RadToolBarButton).Enabled = False
                    CType(Me.MainToolBar.Items(2), RadToolBarButton).Enabled = False
                    If status = 1 Or status = 2 Then
                        CType(Me.MainToolBar.Items(0), RadToolBarButton).Enabled = False
                        CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = False
                    Else
                        CType(Me.MainToolBar.Items(0), RadToolBarButton).Enabled = True
                        CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = True
                    End If
                    'CType(Me.MainToolBar.Items(0), RadToolBarButton).Enabled = True
                    'CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = True
                    'common'
                    EnableControlAll(True, txtYear, cboTypeAss, cboPeriod)
                    EnableControlAll(False, rdStartDate, rdEndDate)
                    'Employee'
                    EnableControlAll(False, txtResult, rdUpdateDate, cboEmp_Status, txtRemark)
                    'Direct'
                    EnableControlAll(False, txtResult_Dir, rdAss_Date, txtRemark_Dir, cboTrangThai)

                Case CommonMessage.STATE_APPROVE
                    EnabledGridNotPostback(rgMain, True)

                    CType(Me.MainToolBar.Items(1), RadToolBarButton).Enabled = False
                    CType(Me.MainToolBar.Items(2), RadToolBarButton).Enabled = False
                    CType(Me.MainToolBar.Items(0), RadToolBarButton).Enabled = False
                    CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = False
                    isDiv1.Visible = False
                    'common'
                    EnableControlAll(True, txtYear, cboTypeAss, cboPeriod)
                    EnableControlAll(True, rdStartDate, rdEndDate)
                    'Employee'
                    EnableControlAll(True, txtResult, rdUpdateDate, cboEmp_Status, txtRemark)
                    'Direct'
                    EnableControlAll(True, txtResult_Dir, rdAss_Date, txtRemark_Dir, cboTrangThai)

                Case CommonMessage.STATE_ACTIVE
                    CType(Me.MainToolBar.Items(0), RadToolBarButton).Enabled = False
                    CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = False
                    CType(Me.MainToolBar.Items(1), RadToolBarButton).Enabled = False
                    CType(Me.MainToolBar.Items(2), RadToolBarButton).Enabled = False

                    isDiv1.Visible = False

                    EnabledGridNotPostback(rgMain, True)
                    'common'
                    EnableControlAll(True, txtYear, cboTypeAss, cboPeriod)
                    EnableControlAll(True, rdStartDate, rdEndDate)
                    'Employee'
                    EnableControlAll(True, txtResult, rdUpdateDate, cboEmp_Status, txtRemark)
                    'Direct'
                    EnableControlAll(True, txtResult_Dir, rdAss_Date, txtRemark_Dir, cboTrangThai)
            End Select
            UpdateToolbarState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub BindData()
        Try
            Using rep As New PerformanceRepository
                'Kieu danh gia
                Dim dtData = rep.GetOtherList("TYPE_ASS", True)
                FillRadCombobox(cboTypeAss, dtData, "NAME", "ID")
                dtData = rep.GetOtherList("STATUS_ASS", True)
                FillRadCombobox(cboTrangThai, dtData, "NAME", "ID")
                FillRadCombobox(cboEmp_Status, dtData, "NAME", "ID")
            End Using
            Dim dic As New Dictionary(Of String, Control)
            dic.Add("PE_CRITERIA_CODE", txtMaKPI)
            dic.Add("PE_CRITERIA_NAME", txtTieuChiKPI)
            dic.Add("EXPENSE", txtChiTieu)
            dic.Add("AMONG", txtTrongSo)
            dic.Add("RESULT_CONVERT", txtQuyDoi)
            dic.Add("FROM_DATE", rdTuNgay)
            dic.Add("TO_DATE", rdDenNgay)
            dic.Add("RESULT", txtResult)
            dic.Add("UPDATE_DATE", rdUpdateDate)
            dic.Add("STATUS_EMP_ID", cboEmp_Status)
            dic.Add("REMARK", txtRemark)
            dic.Add("RESULT_DIRECT", txtResult_Dir)
            dic.Add("ASS_DATE", rdAss_Date)
            dic.Add("REMARK_DIRECT", txtRemark_Dir)
            dic.Add("PE_CRITERIA_ID", lblPE_CRITERIA_ID)
            Utilities.OnClientRowSelectedChanged(rgMain, dic)
            If (iYear IsNot Nothing AndAlso iYear > 0) And (iPeriodId IsNot Nothing AndAlso iPeriodId > 0) And (iTypeAssId IsNot Nothing AndAlso iTypeAssId > 0) And (iEmpId IsNot Nothing AndAlso iEmpId > 0) Then
                txtYear.Value = iYear
                cboTypeAss.SelectedValue = iTypeAssId
                LoadPeriod()
                cboPeriod.SelectedValue = iPeriodId
                Dim list As New List(Of Common.CommonBusiness.EmployeePopupFindDTO)
                Using rep As New CommonRepository
                    Dim lst As New List(Of Decimal) From {
                        iEmpId
                    }
                    list = rep.GetEmployeeToPopupFind_EmployeeID(lst)
                End Using
                lblEmpID.Text = list(0).EMPLOYEE_ID
                lblMaNhanVien.Text = list(0).EMPLOYEE_CODE
                lblFullName.Text = list(0).FULLNAME_VN
                lblChucDanh.Text = list(0).TITLE_NAME
                lblDonVi.Text = list(0).ORG_NAME
                cboPeriod_SelectedIndexChanged(Nothing, Nothing)
                txtYear.Enabled = False
                cboTypeAss.Enabled = False
                cboPeriod.Enabled = False
            Else
                Dim list As New List(Of Common.CommonBusiness.EmployeePopupFindDTO)
                Using rep As New CommonRepository
                    Dim lst As New List(Of Decimal) From {
                        EmployeeID
                    }
                    list = rep.GetEmployeeToPopupFind_EmployeeID(lst)
                End Using
                lblEmpID.Text = list(0).EMPLOYEE_ID
                lblMaNhanVien.Text = list(0).EMPLOYEE_CODE
                lblFullName.Text = list(0).FULLNAME_VN
                lblChucDanh.Text = list(0).TITLE_NAME
                lblDonVi.Text = list(0).ORG_NAME
                txtYear.Value = Date.Now.Year
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

#Region "Event"

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objAssessment As New AssessmentDTO
        Dim rep As New PerformanceRepository
        Dim gID As Decimal
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_EDIT
                    If rgMain.SelectedItems.Count <= 0 Then
                        ShowMessage(Translate("Bạn chưa chọn dữ liệu cần chỉnh sửa."), NotifyType.Warning)
                        Exit Sub
                    End If
                    CurrentState = CommonMessage.STATE_EDIT
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        If rgMain.SelectedItems.Count > 0 Then

                            If txtResult.Text = "" Then
                                ShowMessage(Translate("Kết quả bắt buộc nhập, vui lòng kiểm tra lại."), Utilities.NotifyType.Warning)
                                Exit Sub
                            End If

                            If Decimal.Parse(txtResult.Text) <= 0 Then
                                ShowMessage(Translate("Kết quả không được nhỏ hơn bằng 0, vui lòng kiểm tra lại."), Utilities.NotifyType.Warning)
                                Exit Sub
                            End If
                            If Decimal.Parse(txtChiTieu.Text) < Decimal.Parse(txtResult.Text) Then
                                ShowMessage(Translate("Kết quả không được lớn hơn chỉ tiêu, vui lòng kiểm tra lại."), Utilities.NotifyType.Warning)
                                Exit Sub
                            End If

                            If txtResult_Dir.Text <> String.Empty Then
                                If Decimal.Parse(txtChiTieu.Text) < Decimal.Parse(txtResult_Dir.Text) Then
                                    ShowMessage(Translate("Quản lý đánh giá kết quả công việc không được lớn hơn chỉ tiêu, vui lòng kiểm tra lại."), Utilities.NotifyType.Warning)
                                    Exit Sub
                                End If
                            End If

                            If txtResult.Text IsNot Nothing Or txtResult.Text <> "" Then
                                If rdUpdateDate.SelectedDate Is Nothing Then
                                    ShowMessage(Translate("Bạn phải nhập Ngày cập nhật."), Utilities.NotifyType.Warning)
                                    Exit Sub
                                End If
                            End If

                            If txtResult_Dir.Text <> "" Then
                                If rdAss_Date.SelectedDate Is Nothing Then
                                    ShowMessage(Translate("Bạn phải nhập Ngày đánh giá."), Utilities.NotifyType.Warning)
                                    Exit Sub
                                End If
                            End If

                            Dim item = CType(rgMain.SelectedItems(0), GridDataItem)
                            objAssessment.ID = Utilities.ObjToInt(item.GetDataKeyValue("ID"))
                            objAssessment.PE_EMPLOYEE_ASSESSMENT_ID = Utilities.ObjToInt(hidPE_EMPLOYEE_ASSESSMENT_ID.Value)
                            objAssessment.EMPLOYEE_ID = Utilities.ObjToInt(lblEmpID.Text)
                            objAssessment.PE_PERIO_ID = Utilities.ObjToInt(cboPeriod.SelectedValue)
                            objAssessment.PE_OBJECT_ID = Utilities.ObjToInt(hidPE_OBJECT_ID.Value)
                            objAssessment.PE_CRITERIA_ID = Utilities.ObjToInt(item.GetDataKeyValue("PE_CRITERIA_ID"))
                            objAssessment.RESULT = txtResult.Text
                            objAssessment.STATUS_EMP_ID = cboEmp_Status.SelectedValue
                            objAssessment.UPDATE_DATE = rdUpdateDate.SelectedDate
                            objAssessment.REMARK = txtRemark.Text
                            'Quan ly truc tiep
                            objAssessment.DIRECT_ID = Utilities.ObjToInt(EmployeeID)
                            If txtResult_Dir.Text <> "" Then
                                objAssessment.RESULT_DIRECT = Utilities.ObjToInt(txtResult_Dir.Text)
                            End If
                            objAssessment.ASS_DATE = rdAss_Date.SelectedDate
                            objAssessment.REMARK_DIRECT = txtRemark_Dir.Text
                            If txtResult.Text <> String.Empty And txtTrongSo.Text <> String.Empty Then
                                objAssessment.RESULT_CONVERT = (Utilities.ObjToInt(txtTrongSo.Text) / 100) * Utilities.ObjToInt(txtResult.Text) * 10
                            End If
                            If txtResult_Dir.Text <> String.Empty And txtTrongSo.Text <> String.Empty Then
                                objAssessment.RESULT_CONVERT_QL = (Utilities.ObjToInt(txtTrongSo.Text) / 100) * Utilities.ObjToInt(txtResult_Dir.Text) * 10
                            End If
                            objAssessment.PE_STATUS = cboTrangThai.SelectedValue
                            objAssessment.ACTFLG = "A"
                            If rep.ModifyAssessment(objAssessment, gID) Then

                                If (iYear IsNot Nothing AndAlso iYear > 0) And (iPeriodId IsNot Nothing AndAlso iPeriodId > 0) And (iTypeAssId IsNot Nothing AndAlso iTypeAssId > 0) And (iEmpId IsNot Nothing AndAlso iEmpId > 0) Then
                                    rep.INSERT_PE_ASSESSMENT_HISTORY(objAssessment.ID, objAssessment.RESULT_DIRECT, objAssessment.ASS_DATE, objAssessment.REMARK_DIRECT, objAssessment.CREATED_BY, objAssessment.CREATED_LOG, objAssessment.EMPLOYEE_ID, EmployeeID)
                                End If

                                CurrentState = CommonMessage.STATE_NORMAL
                                IDSelect = gID
                                Refresh("ModifyView")
                                UpdateControlState()
                            Else
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                            End If
                        End If
                    Else
                        ExcuteScript("Resize", "ResizeSplitter()")
                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    Refresh("Cancel")
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_APPROVE

                    If (iYear IsNot Nothing AndAlso iYear > 0) And (iPeriodId IsNot Nothing AndAlso iPeriodId > 0) And (iTypeAssId IsNot Nothing AndAlso iTypeAssId > 0) And (iEmpId IsNot Nothing AndAlso iEmpId > 0) Then
                        Dim data = rep.CHECK_APP_2(Utilities.ObjToInt(lblEmpID.Text), cboPeriod.SelectedValue)

                        For Each item1 In data.Rows
                            If item1("RESULT").ToString = "1" Then
                                ShowMessage(Translate("Vẫn còn tiêu chí chưa được đánh giá, vui lòng kiểm tra lại."), NotifyType.Warning)
                                Exit Sub
                            End If
                        Next

                        ctrlMessageBox.MessageText = "Bảng đánh giá sẽ không thể điều chỉnh, Bạn có tiếp tục không?"
                        ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_APPROVE_BATCH
                        ctrlMessageBox.DataBind()
                        ctrlMessageBox.Show()
                    Else

                        If cboPeriod.SelectedValue = "" Then
                            ShowMessage(Translate("Bạn chưa chọn kỳ đánh giá không thể thực hiện chức năng này."), NotifyType.Warning)
                            Exit Sub
                        End If

                        If rgMain.Items.Count <= 0 Then
                            ShowMessage(Translate("Kỳ đánh giá không có dự liệu không thể thực hiện chức năng này."), NotifyType.Warning)
                            Exit Sub
                        End If

                        Dim data = rep.CHECK_APP(Utilities.ObjToInt(lblEmpID.Text), cboPeriod.SelectedValue)

                        For Each item In data.Rows
                            If item("RESULT").ToString = "1" Then
                                ShowMessage(Translate("Vẫn còn tiêu chí chưa được đánh giá, vui lòng kiểm tra lại."), NotifyType.Warning)
                                Exit Sub
                            End If
                        Next

                        ctrlMessageBox.MessageText = "Bảng đánh giá sẽ không thể điều chỉnh, Bạn có tiếp tục không?"
                        ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_APPROVE
                        ctrlMessageBox.DataBind()
                        ctrlMessageBox.Show()

                    End If

            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Protected Sub txtYear_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtYear.TextChanged
        LoadPeriod()
    End Sub
    Protected Sub cboTypeAss_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboTypeAss.SelectedIndexChanged
        LoadPeriod()
    End Sub
    Protected Sub cboPeriod_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboPeriod.SelectedIndexChanged
        Dim rep As New PerformanceRepository
        Dim Periods As List(Of PeriodDTO)
        Dim _filter As New PeriodDTO
        _filter.ID = Utilities.ObjToInt(cboPeriod.SelectedValue)
        _filter.EMPLOYEE_ID = Utilities.ObjToInt(lblEmpID.Text)
        Periods = rep.GetPeriodById(_filter)
        If Periods.Count > 0 Then
            hidPE_OBJECT_ID.Value = Utilities.ObjToInt(Periods(0).OBJECT_GROUP_ID)
            rdStartDate.SelectedDate = Periods(0).START_DATE
            rdEndDate.SelectedDate = Periods(0).END_DATE
            cboTrangThai.SelectedValue = Utilities.ObjToInt(Periods(0).PE_STATUS)
            cboEmp_Status.SelectedValue = Utilities.ObjToInt(Periods(0).PE_STATUS)
            hidPE_EMPLOYEE_ASSESSMENT_ID.Value = Utilities.ObjToInt(Periods(0).ID)
            rgMain.Rebind()
        End If
        If cboPeriod.SelectedValue <> "" Then
            If (iYear IsNot Nothing AndAlso iYear > 0) And (iPeriodId IsNot Nothing AndAlso iPeriodId > 0) And (iTypeAssId IsNot Nothing AndAlso iTypeAssId > 0) And (iEmpId IsNot Nothing AndAlso iEmpId > 0) Then
                If status = 1 Or status = 2 Then
                    CType(Me.MainToolBar.Items(0), RadToolBarButton).Enabled = False
                    CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = False
                End If

            Else
                Dim data1 = rep.CHECK_APP_1(Utilities.ObjToInt(lblEmpID.Text), cboPeriod.SelectedValue)

                For Each item In data1.Rows
                    If Utilities.ObjToInt(item("RESULT").ToString) > 0 Then
                        CType(Me.MainToolBar.Items(0), RadToolBarButton).Enabled = False
                        CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = False
                    End If
                Next
            End If
        End If
    End Sub

    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgMain.NeedDataSource
        Try
            CreateDataFilter()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    'Private Sub rgMain_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgMain.ItemDataBound
    '    Try
    '        If TypeOf e.Item Is GridDataItem Then
    '            Dim item = CType(e.Item, GridDataItem)
    '            Dim link As LinkButton
    '            link = CType(item("LINK_POPUP").FindControl("lbtnLink"), LinkButton)
    '            If item.GetDataKeyValue("LINK_POPUP") Is Nothing Then
    '                link.Visible = False
    '            Else
    '                link.OnClientClick = item.GetDataKeyValue("LINK_POPUP")
    '            End If
    '        End If
    '    Catch ex As Exception
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '    End Try
    'End Sub
#End Region
    Public Sub LoadPeriod()
        Dim rep As New PerformanceRepository
        Dim Periods As List(Of PeriodDTO)
        Dim _filter As New PeriodDTO
        _filter.TYPE_ASS = Utilities.ObjToInt(cboTypeAss.SelectedValue)
        _filter.YEAR = Utilities.ObjToInt(txtYear.Value)
        Periods = rep.GetPeriod(_filter)
        FillRadCombobox(cboPeriod, Periods.ToTable(), "NAME", "ID")
    End Sub
#Region "Custom"
    Private Sub UpdateToolbarState()
        Try
            ' ChangeToolbarState()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien click vao button command ctrlMessageBox
    ''' voi cac trang thai xoa, duyet, huy kich hoat
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim rep As New PerformanceRepository
            If e.ActionName = CommonMessage.TOOLBARITEM_APPROVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                rep.PRI_PERFORMACE_APP(Utilities.ObjToInt(lblEmpID.Text), cboPeriod.SelectedValue)
                CurrentState = CommonMessage.STATE_APPROVE
                UpdateControlState()
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
            End If
            If e.ActionName = CommonMessage.TOOLBARITEM_CANCEL And e.ButtonID = MessageBoxButtonType.ButtonYes Then

                rep.PRI_PERFORMACE_PROCESS(EmployeeID, Utilities.ObjToInt(lblEmpID.Text), cboPeriod.SelectedValue, 2, txtNote.Text)
                CurrentState = CommonMessage.STATE_ACTIVE
                UpdateControlState()
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
            End If

            If e.ActionName = CommonMessage.TOOLBARITEM_APPROVE_BATCH And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                rep.PRI_PERFORMACE_PROCESS(EmployeeID, Utilities.ObjToInt(lblEmpID.Text), Decimal.Parse(cboPeriod.SelectedValue), 1, txtNote.Text)
                CurrentState = CommonMessage.STATE_ACTIVE
                UpdateControlState()
            End If

        Catch ex As Exception
        End Try

    End Sub

    Protected Sub rgDanhMuc_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rgMain.SelectedIndexChanged

        Dim item = CType(rgMain.SelectedItems(0), GridDataItem)
        Dim ID = item.GetDataKeyValue("PE_CRITERIA_CODE").ToString
        Dim Classifications1 = (From p In Classifications Where p.PE_CRITERIA_CODE = ID).SingleOrDefault

        txtMaKPI.Text = Classifications1.PE_CRITERIA_CODE
        txtTieuChiKPI.Text = Classifications1.PE_CRITERIA_NAME
        txtChiTieu.Text = Classifications1.EXPENSE
        txtTrongSo.Text = Classifications1.AMONG
        txtQuyDoi.Text = If(Classifications1.RESULT_CONVERT Is Nothing, "", Classifications1.RESULT_CONVERT)
        rdTuNgay.SelectedDate = Classifications1.FROM_DATE
        rdDenNgay.SelectedDate = Classifications1.TO_DATE
        txtTieuChiKPI.Text = Classifications1.PE_CRITERIA_NAME
        txtResult.Text = Classifications1.RESULT
        rdUpdateDate.SelectedDate = Classifications1.UPDATE_DATE

        If Classifications1.STATUS_EMP_ID IsNot Nothing Then
            cboEmp_Status.SelectedValue = Classifications1.STATUS_EMP_ID
        Else
            cboEmp_Status.SelectedValue = 6140
        End If
        txtRemark.Text = Classifications1.REMARK
        txtResult_Dir.Text = Classifications1.RESULT_DIRECT
        rdAss_Date.SelectedDate = Classifications1.ASS_DATE
        txtRemark_Dir.Text = Classifications1.REMARK_DIRECT
        lblPE_CRITERIA_ID.Text = Classifications1.PE_CRITERIA_ID

    End Sub


    'Protected Sub btnDeny_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDeny.Click
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Try

    '        If txtNote.Text = "" Then
    '            ShowMessage(Translate("Một số nhân viên được chọn chưa có ý kiến đánh giá chung, Bạn vui lòng kiểm tra lại."), NotifyType.Warning)
    '            Exit Sub
    '        End If

    '        If status = 1 Or status = 2 Then
    '            ShowMessage(Translate("Trạng thái Đã thực hiện hoặc đã huỷ không thể thực hiện chức năng này., Vui lòng kiểm tra lại."), NotifyType.Warning)
    '            Exit Sub
    '        End If

    '        ctrlMessageBox.MessageText = "Bảng đánh giá sẽ trả lại cho các nhân viên được chọn , Bạn có tiếp tục không?"
    '        ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_CANCEL
    '        ctrlMessageBox.DataBind()
    '        ctrlMessageBox.Show()
    '    Catch ex As Exception
    '        DisplayException(Me.ViewName, Me.ID, ex)

    '    End Try
    'End Sub

#End Region


End Class