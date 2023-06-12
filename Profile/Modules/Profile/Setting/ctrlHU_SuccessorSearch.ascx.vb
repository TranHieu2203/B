Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Common
Imports Telerik.Web.UI
Imports System.IO
Imports WebAppLog

Public Class ctrlHU_SuccessorSearch
    Inherits Common.CommonView
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Public postBack As String

    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Profile/Module/Profile/Setting/" + Me.GetType().Name.ToString()

#Region "Property"

    ''' <summary>
    ''' Obj ListComboData
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
    Property dtData As DataTable
        Get
            If ViewState(Me.ID & "_dtData") Is Nothing Then
                Dim dt As New DataTable
                dt.Columns.Add("ID", GetType(String))
                dt.Columns.Add("CODE", GetType(String))
                dt.Columns.Add("FULL_NAME", GetType(String))
                dt.Columns.Add("DATE_OF_BIRTH", GetType(Date))
                dt.Columns.Add("WORK_STATUS", GetType(String))
                dt.Columns.Add("ORG_ID", GetType(Decimal))
                dt.Columns.Add("TER_LAST_DATE", GetType(Date))
                dt.Columns.Add("AGE", GetType(Decimal))
                dt.Columns.Add("GENDER", GetType(Decimal))
                dt.Columns.Add("GENDER_NAME", GetType(String))
                dt.Columns.Add("TD_HV_NAME", GetType(String))
                dt.Columns.Add("TD_HV", GetType(Decimal))
                dt.Columns.Add("ORGANIZATION_ID", GetType(Decimal))
                dt.Columns.Add("SENIOR_KN", GetType(String))
                dt.Columns.Add("POSITION_ID", GetType(Decimal))
                dt.Columns.Add("POSITION_NAME", GetType(String))
                dt.Columns.Add("KN_VT", GetType(String))
                dt.Columns.Add("RANK_ID", GetType(Decimal))
                dt.Columns.Add("STAFF_NAME", GetType(String))
                dt.Columns.Add("DT_CT", GetType(Decimal))
                dt.Columns.Add("DT_CT_NAME", GetType(String))
                dt.Columns.Add("KQ_DG", GetType(String))
                dt.Columns.Add("KHOA_DT", GetType(String))
                dt.Columns.Add("KHOA_DT_ID", GetType(Decimal))
                dt.Columns.Add("BC_CC", GetType(String))
                dt.Columns.Add("BC_CC_ID", GetType(Decimal))
                dt.Columns.Add("NL_CM", GetType(String))
                ViewState(Me.ID & "_dtData") = dt
            End If
            Return ViewState(Me.ID & "_dtData")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtData") = value
        End Set
    End Property
#End Region

#Region "Page"

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
            postBack = "0"
            If Page.IsPostBack Then
                postBack = "1"
                For Each item As RadComboBoxItem In cboFilterList.Items
                    item.Enabled = True
                Next
                
            Else
                cboFilterList.Items(1).Enabled = False
            End If
            ctrlOrganization.AutoPostBack = False
            ctrlOrganization.LoadDataAfterLoaded = True
            ctrlOrganization.OrganizationType = OrganizationType.OrganizationLocation
            ctrlOrganization.CheckBoxes = TreeNodeTypes.All
            ctrlOrganization.CheckParentNodes = False
            ctrlOrganization.CheckChildNodes = True
            rgData.SetFilter()
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
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarOrgFunctions
            Common.Common.BuildToolbar(Me.MainToolBar,
                                       ToolbarItem.Calculate,
                                       ToolbarItem.Save, ToolbarItem.Seperator)
            tbarOrgFunctions.Items(0).Text = Translate("Quy hoạch nhân sự")
            Try
                tbarOrgFunctions.Items(0).ImageUrl = "~/Static/Images/Toolbar/qh.png"
            Catch ex As Exception
            End Try
            CType(Me.MainToolBar.Items(0), RadToolBarButton).CausesValidation = True
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
            Dim rep As New ProfileRepository
            Try
                Dim startTime As DateTime = DateTime.UtcNow

                _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
            Catch ex As Exception
                DisplayException(Me.ViewName, Me.ID, ex)
                _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            End Try
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

#End Region

#Region "Event"

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim rep As New ProfileRepository
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARTIEM_CALCULATE
                    If ctrlOrganization.CheckedValues.Count = 0 Then
                        ShowMessage(Translate("Bạn phải chọn đơn vị tìm kiếm!"), NotifyType.Error)
                        Exit Sub
                    End If
                    If chkExperience.Checked Then '1 - Kinh nghiệm vị trí'
                        If txtEperience.Text = "" Then
                            ShowMessage(Translate("Bạn phải điền giá trị để so sánh!"), NotifyType.Error)
                            Exit Sub
                            cboFilterList.Items(1).Enabled = False
                        End If
                    End If
                    If chkPositions.Checked Then '2 - Đã từng công tác tại vị trí'
                        cboFilterList.Items(2).Enabled = False
                    End If
                    If chkAge.Checked Then '4 - Tuổi'
                        If txtAge.Text = "" Then
                            ShowMessage(Translate("Bạn phải điền giá trị để so sánh!"), NotifyType.Error)
                            Exit Sub
                            cboFilterList.Items(3).Enabled = False
                        End If
                    End If
                    If chkGender.Checked Then '5 - Giới tính'
                        cboFilterList.Items(4).Enabled = False
                    End If
                    If chkSeniority.Checked Then '6 - Thâm niên'
                        If txtSeniority.Text = "" Then
                            ShowMessage(Translate("Bạn phải điền giá trị để so sánh!"), NotifyType.Error)
                            Exit Sub
                            cboFilterList.Items(5).Enabled = False
                        End If
                    End If
                    If chkStaffRank.Checked Then '7 - Cấp nhân sự'
                        cboFilterList.Items(6).Enabled = False
                    End If
                    If chkAcademicLevel.Checked Then '8 - Trình độ học vấn'
                        cboFilterList.Items(7).Enabled = False
                    End If
                    If chkCertificate.Checked Then '9 - Bằng cấp chứng chỉ'
                        cboFilterList.Items(8).Enabled = False
                    End If
                    If chkCourses.Checked Then '10 - Khóa đào tạo'
                        cboFilterList.Items(9).Enabled = False
                    End If
                    If chkEvaluation.Checked Then '11 - Kết quả đánh giá'
                        cboFilterList.Items(10).Enabled = False
                    End If
                    If chkTalents.Checked Then '12 - Năng lực'
                        cboFilterList.Items(11).Enabled = False
                        If chkTalent1.Checked Then '13 - Năng lực 1'
                            If txtTalent1.Text = "" Then
                                ShowMessage(Translate("Bạn phải điền giá trị để so sánh!"), NotifyType.Error)
                                Exit Sub
                            End If
                        End If
                        If chkTalent2.Checked Then '13 - Năng lực 2'
                            If txtTalent2.Text = "" Then
                                ShowMessage(Translate("Bạn phải điền giá trị để so sánh!"), NotifyType.Error)
                                Exit Sub
                            End If
                        End If
                        If chkTalent3.Checked Then '13 - Năng lực 3'
                            If txtTalent3.Text = "" Then
                                ShowMessage(Translate("Bạn phải điền giá trị để so sánh!"), NotifyType.Error)
                                Exit Sub
                            End If
                        End If
                        If chkTalent4.Checked Then '13 - Năng lực 4'
                            If txtTalent4.Text = "" Then
                                ShowMessage(Translate("Bạn phải điền giá trị để so sánh!"), NotifyType.Error)
                                Exit Sub
                            End If
                        End If
                        If chkTalent5.Checked Then '13 - Năng lực 5'
                            If txtTalent5.Text = "" Then
                                ShowMessage(Translate("Bạn phải điền giá trị để so sánh!"), NotifyType.Error)
                                Exit Sub
                            End If
                        End If
                        If chkTalent6.Checked Then '13 - Năng lực 6'
                            If txtTalent6.Text = "" Then
                                ShowMessage(Translate("Bạn phải điền giá trị để so sánh!"), NotifyType.Error)
                                Exit Sub
                            End If
                        End If
                        If chkTalent7.Checked Then '13 - Năng lực 7'
                            If txtTalent7.Text = "" Then
                                ShowMessage(Translate("Bạn phải điền giá trị để so sánh!"), NotifyType.Error)
                                Exit Sub
                            End If
                        End If
                    End If
                    rgData.Rebind()
                    ExcuteScript("Resize", "saveFilter()")
                Case CommonMessage.TOOLBARITEM_SAVE
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Me.Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), Utilities.NotifyType.Warning)
                        Exit Sub
                    End If

                    Dim lstTalentPool As New List(Of TalentPoolDTO)
                    For idx = 0 To rgData.SelectedItems.Count - 1
                        Dim objTalentPool As New TalentPoolDTO
                        Dim item As GridDataItem = rgData.SelectedItems(idx)
                        objTalentPool.EMPLOYEE_ID = item.GetDataKeyValue("ID")
                        objTalentPool.FILTER_ID = item.GetDataKeyValue("ID") + 1
                        lstTalentPool.Add(objTalentPool)
                    Next

                    If rep.InsertTalentPool(lstTalentPool) Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                    End If
                    rgData.Rebind()
            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub cboYear_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboYear.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim dtData As DataTable
            Dim repCommon As New CommonRepository
            If Not cboYear.SelectedValue Is Nothing Then
                dtData = repCommon.GetPeriodByYear(False, cboYear.SelectedValue)
                FillRadCombobox(cboPeriod, dtData, "NAME", "ID", True)
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Custom"

    ''' <summary>
    ''' Phương thức tạo dữ liệu filter cho trang
    ''' Xét giá trị cho các object trên radgrid
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub CreateDataFilter()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New ProfileRepository

        Try
            If Not IsPostBack Then
                rgData.DataSource = dtData
                Exit Sub
            End If
            Dim P_CON_POSTION = Nothing
            Dim P_CON_KN_VT = Nothing
            Dim P_CON_DT_CT = Nothing
            Dim P_CON_SENIOR_KN = Nothing
            Dim P_CON_AGE = Nothing
            Dim P_CON_TD_HV = Nothing
            Dim P_CON_BC_CC = Nothing
            Dim P_CON_KDT = Nothing
            Dim P_CON_NL_CM = Nothing
            Dim P_CON_GENDER = Nothing
            Dim P_CON_KY_DG = 0
            Dim P_CON_KQ_DG = Nothing
            Dim P_CON_STAFF_RANK = Nothing
            Dim P_ORG_ID = Nothing
            P_ORG_ID = ctrlOrganization.CheckedValues.Aggregate(Function(x, y) x & "," & y)
            If chkExperience.Checked Then '1 - Kinh nghiệm vị trí
                P_CON_POSTION = cboPosition.SelectedValue
                P_CON_KN_VT = cboComparisonOperators1.SelectedValue + txtEperience.Text.Trim
            End If
            If chkPositions.Checked Then '2 - Đã từng công tác tại vị trí'
                P_CON_DT_CT = GetIDs(cboPositions)
            End If
            If chkAge.Checked Then '4 - Tuổi'
                P_CON_AGE = cboComparisonOperators2.SelectedValue + txtAge.Text.Trim
            End If
            If chkGender.Checked Then '5 - Giới tính'
                P_CON_GENDER = GetIDs(cboGender)
            End If
            If chkSeniority.Checked Then '6 - Thâm niên'
                P_CON_SENIOR_KN = cboComparisonOperators3.SelectedValue + txtSeniority.Text.Trim
            End If
            If chkStaffRank.Checked Then '7 - Cấp nhân sự'
                P_CON_STAFF_RANK = GetIDs(cboStaffRank)
            End If
            If chkAcademicLevel.Checked Then '8 - Trình độ học vấn'
                P_CON_TD_HV = cboComparisonOperators4.SelectedValue + GetIDs(cboAcademicLevel)
            End If
            If chkCertificate.Checked Then '9 - Bằng cấp chứng chỉ'
                P_CON_BC_CC = GetIDs(cboCertificate)
            End If
            If chkCourses.Checked Then '10 - Khóa đào tạo'
                P_CON_KDT = GetIDs(cboCourses)
            End If
            If chkEvaluation.Checked Then '11 - Kết quả đánh giá'
                P_CON_KY_DG = cboPeriod.SelectedValue
                If cboComparisonOperators5.SelectedValue = "<" Then
                    P_CON_KQ_DG = ">"
                ElseIf cboComparisonOperators5.SelectedValue = "<=" Then
                    P_CON_KQ_DG = ">="
                ElseIf cboComparisonOperators5.SelectedValue = ">" Then
                    P_CON_KQ_DG = "<"
                ElseIf cboComparisonOperators5.SelectedValue = ">=" Then
                    P_CON_KQ_DG = "<="
                End If
                P_CON_KQ_DG += "'" + cboGrade.SelectedValue + "'"
            End If
            If chkTalents.Checked Then '12 - Năng lực'
                If chkTalent1.Checked Then '13 - Năng lực 1'
                    P_CON_NL_CM = cboTalent1.SelectedValue + cboComparisonOperatorsTalent1.SelectedValue + txtTalent1.Text.Trim
                End If
                If chkTalent2.Checked Then '13 - Năng lực 2'
                    P_CON_NL_CM += " " + cboRelativeOperatorsTalent2.SelectedValue + " " + cboTalent2.SelectedValue + cboComparisonOperatorsTalent2.SelectedValue + txtTalent2.Text.Trim
                End If
                If chkTalent3.Checked Then '13 - Năng lực 3'
                    P_CON_NL_CM += " " + cboRelativeOperatorsTalent3.SelectedValue + " " + cboTalent3.SelectedValue + cboComparisonOperatorsTalent3.SelectedValue + txtTalent3.Text.Trim
                End If
                If chkTalent4.Checked Then '13 - Năng lực 4'
                    P_CON_NL_CM += " " + cboRelativeOperatorsTalent4.SelectedValue + " " + cboTalent4.SelectedValue + cboComparisonOperatorsTalent4.SelectedValue + txtTalent4.Text.Trim
                End If
                If chkTalent5.Checked Then '13 - Năng lực 5'
                    P_CON_NL_CM += " " + cboRelativeOperatorsTalent5.SelectedValue + " " + cboTalent5.SelectedValue + cboComparisonOperatorsTalent5.SelectedValue + txtTalent5.Text.Trim
                End If
                If chkTalent6.Checked Then '13 - Năng lực 6'
                    P_CON_NL_CM += " " + cboRelativeOperatorsTalent6.SelectedValue + " " + cboTalent6.SelectedValue + cboComparisonOperatorsTalent6.SelectedValue + txtTalent6.Text.Trim
                End If
                If chkTalent7.Checked Then '13 - Năng lực 7'
                    P_CON_NL_CM += " " + cboRelativeOperatorsTalent7.SelectedValue + " " + cboTalent7.SelectedValue + cboComparisonOperatorsTalent7.SelectedValue + txtTalent7.Text.Trim
                End If
            End If
            Dim _param As FilterParamDTO
            _param = New FilterParamDTO With {.P_CON_POSTION = P_CON_POSTION,
                                              .P_CON_KN_VT = P_CON_KN_VT,
                                              .P_CON_DT_CT = P_CON_DT_CT,
                                              .P_CON_SENIOR_KN = P_CON_SENIOR_KN,
                                              .P_CON_AGE = P_CON_AGE,
                                              .P_CON_TD_HV = P_CON_TD_HV,
                                              .P_CON_BC_CC = P_CON_BC_CC,
                                              .P_CON_KDT = P_CON_KDT,
                                              .P_CON_NL_CM = P_CON_NL_CM,
                                              .P_CON_GENDER = P_CON_GENDER,
                                              .P_CON_KY_DG = P_CON_KY_DG,
                                              .P_CON_KQ_DG = P_CON_KQ_DG,
                                              .P_CON_STAFF_RANK = P_CON_STAFF_RANK,
                                              .P_ORG_ID = P_ORG_ID}
            dtData = rep.FILTER_TALENT_POOL(_param)

            If dtData IsNot Nothing Then
                rgData.DataSource = dtData
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Public Sub NeedDataSource(ByVal sender As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            CreateDataFilter()
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub GetDataCombo()
        Dim repProfile As New ProfileRepository
        Dim repCommon As New CommonRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim dtData As DataTable
            ListComboData = New ComboBoxDataDTO
            ListComboData.GET_TITLE = True
            ListComboData.GET_GENDER = True
            repProfile.GetComboList(ListComboData)
            FillRadCombobox(cboPosition, ListComboData.LIST_TITLE, "NAME_VN", "ID", True)
            FillRadCombobox(cboPositions, ListComboData.LIST_TITLE, "NAME_VN", "ID", True)
            FillRadCombobox(cboGender, ListComboData.LIST_GENDER, "NAME_VN", "ID", True)

            dtData = repProfile.GetStaffRankList(False)
            FillRadCombobox(cboStaffRank, dtData, "NAME", "ID", True)

            dtData = repCommon.GetLearningLevel(False)
            FillRadCombobox(cboAcademicLevel, dtData, "NAME", "CODE", True)

            dtData = repCommon.GetAllTrCertificateList(False)
            FillRadCombobox(cboCertificate, dtData, "NAME", "ID", True)

            dtData = repCommon.GetCourseByList(False)
            FillRadCombobox(cboCourses, dtData, "NAME", "ID", True)

            dtData = repCommon.GetPeriodYear(False)
            FillRadCombobox(cboYear, dtData, "YEAR", "YEAR", True)

            dtData = repCommon.GetClassification(False)
            FillRadCombobox(cboGrade, dtData, "CODE", "CODE", True)

            dtData = repCommon.GetHU_CompetencyList(True)
            FillRadCombobox(cboTalents, dtData, "NAME", "CODE", True)

            _mylog.WriteLog(_mylog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Function GetIDs(ByVal combo As RadComboBox) As String
        Dim selectedIDs = String.Empty
        For i As Integer = 0 To combo.CheckedItems.Count - 1
            Dim item = combo.CheckedItems(i)
            selectedIDs += item.Value + ","
        Next
        selectedIDs = selectedIDs.Trim().Remove(selectedIDs.Length - 1)
        Return selectedIDs
    End Function

#End Region

End Class