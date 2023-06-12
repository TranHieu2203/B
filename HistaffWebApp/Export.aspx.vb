Imports System.IO
Imports Aspose.Cells
Imports Attendance.AttendanceBusiness
Imports Framework.UI


Public Class Export
    Inherits System.Web.UI.Page

#Region "Page"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                Select Case Request.Params("id")
                    Case "TR_ASSESSMENT_RESULT"
                       ' TR_ASSESSMENT_RESULT()
                    Case "TR_ASSESSMENT_RESULT_ERROR"
                        TR_ASSESSMENT_RESULT_ERROR()
                    Case "TR_REQUEST_EMPLOYEE"
                        TR_REQUEST_EMPLOYEE()
                    Case "TR_REQUEST_EMPLOYEE_ERROR"
                        TR_REQUEST_EMPLOYEE_ERROR()
                    Case "RC_CANDIDATE_IMPORT"
                       ' RC_CANDIDATE_IMPORT()
                    Case "RC_CANDIDATE_IMPORT_ERROR"
                        RC_CANDIDATE_IMPORT_ERROR()
                    Case "ManIOImport"
                        ManIOImport()
                    Case "Time_TimeSheetCCT"
                        Time_TimeSheetCCT()
                    Case "Template_importTimesheet_CTT_Error"
                        Time_TimeSheetCCT_Error()
                    Case "Template_importTimesheet_CTT_Error1"
                        Time_TimeSheetCCT_Error1()
                    Case "WorkShiftImport"
                        WorkShiftImport()
                    Case "WorkNightImport"
                        WorkNightImport()
                    Case "Template_ImportDMVS"
                        Template_ImportDMVS()
                    Case "Template_ImportOT"
                        Template_ImportOT()
                    Case "Template_DeclareOT"
                        Template_DeclareOT()
                    Case "Template_ImportShift_error"
                        Template_ImportShift_error()
                    Case "Template_importIO_error"
                        Template_importIO_error()
                    Case "TEMP_IMPORT_HOSOLUONG"
                        TEMP_IMPORT_HOSOLUONG()
                    Case "TEMP_IMPORT_BCCC"
                        TEMP_IMPORT_BCCC()
                    Case "TEMP_IMPORT_CONTRACT"
                        TEMP_IMPORT_CONTRACT()
                    Case "TEMP_IMPORT_HSTDT"
                        TEMP_IMPORT_HSTDT()
                    Case "Template_ImportDMVS_error"
                        Template_ImportDMVS_error()
                    Case "Template_ImportOT_error"
                        Template_ImportOT_error()
                    Case "Template_GiaiTrinhNgayCong_error"
                        Template_GiaiTrinhNgayCong_error()
                    Case "Template_DeclareOT_error"
                        Template_DeclareOT_error()
                    Case "Template_ImportSalary"
                        Template_ImportSalary()
                        'Case "Template_ImportSeniorityProcess"
                        '    Template_ImportSeniorityProcess()
                        'Case "Template_ImportSeniorityProcess_error"
                        Template_ImportSeniorityProcess_error()
                    Case "Template_ImportDM"
                        Template_ImportDM()
                    Case "Template_DMNoiKhamChuaBenh_ERROR"
                        Template_ImportDMNK_ERROR()
                    Case "Import_InfoIns"
                        Import_InfoIns()
                    Case "Template_ImportInfoIns_error"
                        Template_ImportInfoIns_error()
                    Case "TokhaiA01"
                        TokhaiA01()
                    Case "Template_SingDefault"
                        Template_SingDefault()
                    Case "Import_SingDefault_Error"
                        Import_SingDefault_Error()
                    Case "Template_ImportDeclareTimeRice"
                        Template_DeclareTimeRice()
                    Case "Import_DeclareTimeRice_Error"
                        Import_DeclareTimeRice_Error()
                    Case "Template_ImportTimeSheetRice"
                        Template_ImportTimeSheetRice()
                    Case "Import_TimeSheetRice_Error"
                        Import_TimeSheetRice_Error()
                    Case "Template_ImportSwipeData"
                        Template_ImportSwipeData()
                    Case "Import_SwipeData_Error"
                        Import_SwipeData_Error()
                    Case "HU_ChangeInfo"
                        HU_ChangeInfo()
                    Case "HU_ChangeInfo_Error"
                        HU_ChangeInfo_Error()
                    Case "Template_Register"
                        Template_Register()
                    Case "Template_Register_Error"
                        Template_Register_Error()
                    Case "Time_TimeSheet_Rice"
                        Time_TimeSheet_Rice()
                    Case "Time_TimeSheet_OT"
                        Time_TimeSheet_OT()
                    Case "IMPORT_SALARYPLANNING"
                        IMPORT_SALARYPLANNING()
                    Case "IMPORT_SALARYPLANNING_ERROR"
                        IMPORT_SALARYPLANNING_ERROR()
                    Case "Template_DeclareEntitlement"
                        Template_DeclareEntitlement()
                    Case "Template_DeclareEntitlement_error"
                        Template_DeclareEntitlement_error()
                    Case "Import_INS_SUNCARE"
                        Import_INS_SUNCARE()
                    Case "Import_INS_SUNCARE_ERROR"
                        Import_INS_SUNCARE_error()
                    Case "Template_ImportSalary"
                        Template_ImportSalary()
                    Case "Template_ImportThuongHQCV"
                        Template_ImportThuongHQCV()
                    Case "Template_ImportQuyThuongHQCV"
                        Template_ImportQuyThuongHQCV()
                    Case "Template_ImportThueTNCN"
                        Template_ImportQTThueTNCN()
                    Case "Template_ImportSalary_FundMapping"
                        Template_ImportSalary_FundMapping()
                    Case "HU_ANNUALLEAVE_PLANS_ERROR"
                        HU_ANNUALLEAVE_PLANS_ERROR()
                    Case "ORG_ACCESS"
                        ORG_ACCESS()
                    Case "PA_TARGET_DTTD_LABEL"
                        PA_TARGET_DTTD_LABEL()
                    Case "PA_VEHICLE_NORM"
                        PA_Vehicle_Norm()
                    Case "IMPORT_CV_ERROR"
                        IMPORT_CV_ERROR()
                    Case "IMPORT_OT"
                        IMPORT_OT()
                    Case "CONGNO_ERROR"
                        HU_CONGNO_ERROR()
                    Case "Template_ImportHoSoLuong"
                        Template_ImportHoSoLuong()
                    Case "Timesheet_machineExport"
                        Timesheet_machineExport()
                    Case "Template_yeucautuyendung_Error"
                        Template_yeucautuyendung_Error()
                    Case "Template_Import_HRDetail"
                        Template_Import_HRDetail()
                    Case "Template_Import_HRDetail_error"
                        Template_Import_HRDetail_error()
                    Case "Template_Import_HRBudgetDetail_error"
                        Template_Import_HRBudgetDetail_error()
                    Case "Template_Import_Nguoiky_error"
                        Template_Import_Nguoiky_error()
                    Case "Template_Import_TimeSheet_Monthly_Error"
                        Template_Import_TimeSheet_Monthly_Error()
                    Case "Template_Import_DTTD_DTPB_Error"
                        Template_Import_DTTD_DTPB_Error()
                    Case "Template_Import_DieuChinhConghoachtoan_Err"
                        Template_Import_DieuChinhConghoachtoan_Err()
                    Case "Template_Import_DTTD_ECD_Error"
                        Template_Import_DTTD_ECD_Error()
                    Case "Template_Import_LDT_VP_Error"
                        Template_Import_LDT_VP_Error()
                    Case "TEMP_IMPORT_KPI"
                        TEMP_IMPORT_KPI()
                    Case "Template_import_MA_SCP_QLCH_Error"
                        Template_import_MA_SCP_QLCH_Error()
                    Case "Template_Import_PA_STANDARD_SETUP_Error"
                        Template_Import_PA_STANDARD_SETUP_Error()
                    Case "Import_Taikhoan_Error"
                        Taikhoan_Error()
                    Case "Template_LamNgoaiGio_error"
                        Template_LamNgoaiGio_error()
                    Case "Template_Import_PA_TARGET_STORE_Error"
                        Template_Import_PA_TARGET_STORE_Error()
                    Case "Template_PA_EmpFormuler"
                        Template_PA_EmpFormuler()
                    Case "Template_import_Year_Compenstion_Error"
                        Template_import_Year_Compenstion_Error()
                    Case "Template_import_Year_Conclude_Error"
                        Template_import_Year_Conclude_Error()
                    Case "Template_Import_Criteria_Error"
                        Template_Import_Criteria_Error()
                    Case "Template_ThietLap_TDTTT_NhomNV_Error"
                        Template_ThietLap_TDTTT_NhomNV_Error()
                    Case "IMPORT_ERROR_PA_SETUP_INDEX"
                        IMPORT_ERROR_PA_SETUP_INDEX()
                    Case "IMPORT_ERROR_PA_SETUP_BONUS_KPI_PRODUCTTYPE"
                        IMPORT_ERROR_PA_SETUP_BONUS_KPI_PRODUCTTYPE()
                    Case "Template_import_DTCH_TheoNgay_Error"
                        Template_import_DTCH_TheoNgay_Error()
                    Case "TEMP_IMPORT_OFFERLETER"
                        TEMP_IMPORT_OFFERLETER()
                    Case "IMPORT_RESULT_ERROR"
                        IMPORT_RESULT_ERROR()
                    Case "ImportBoiHoanDT_ERROR"
                        ImportBoiHoanDT_ERROR()
                    Case "Import_RollCall_Error"
                        Import_RollCall_Error()
                    Case "Template_Import_TR_Plan_Error"
                        Template_Import_TR_Plan_Error()
                    Case "Template_Import_TitleCourse_Error"
                        Template_Import_TitleCourse_Error()
                    Case "Template_ThietLap_MucThuong_VP_Error"
                        Template_ThietLap_MucThuong_VP_Error()
                    Case "TR_Program_Employee_Error"
                        TR_Program_Employee_Error()
                    Case "Template_Import_PE_Org_Mr_Rr_Error"
                        Template_Import_PE_Org_Mr_Rr_Error()
                    Case "Template_Import_CTNN_Error"
                        Template_Import_CTNN_Error()
                    Case "Template_Import_Travel_Error"
                        Template_Import_Travel_Error()
                    Case "Template_Import_HDCB_Error"
                        Template_Import_HDCB_Error()
                    Case "Template_Import_CB_Assessent_Error"
                        Template_Import_CB_Assessent_Error()
                    Case "Template_Import_Employee_NPT_Error"
                        Template_Import_Employee_NPT_Error()
                    Case "Template_Import_TLHD_Error"
                        Template_Import_TLHD_Error()
                    Case "Template_Import_Discipline_Emp_Error"
                        Template_Import_Discipline_Emp_Error()
                    Case "TEMP_IMPORT_ACCIDENT"
                        EXPORTREPORT_ACCIDENT()
                    Case "Import_phucap_err"
                        Import_phucap_err()
                    Case "TEMP_IMPORT_HEALTHEXAM"
                        TEMP_IMPORT_HEALTHEXAM()
                    Case "IMPORT_AT_TOXICLEAVE_EMP"
                        IMPORT_AT_TOXICLEAVE_EMP()
                End Select
            Catch ex As Exception
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "javascriptfunction", "goBack()", True)
            End Try
        End If
    End Sub

#End Region

#Region "Process"
    Private Sub Template_ImportHoSoLuong()
        Dim rep As New Profile.ProfileBusinessRepository
        'Dim param As New Profile.ProfileBusiness.ParamDTO
        Try
            'Dim is_disolve = Request.Params("IS_DISSOLVE")
            'Dim org_id = Decimal.Parse(Request.Params("ORG_ID"))
            'param.ORG_ID = org_id
            'param.IS_DISSOLVE = is_disolve
            Dim dsData As DataSet = rep.GetHoSoLuongImport()
            dsData.Tables(0).TableName = "Table"
            dsData.Tables(1).TableName = "Table1"
            dsData.Tables(2).TableName = "Table2"
            dsData.Tables(3).TableName = "Table3"
            dsData.Tables(4).TableName = "Table4"
            dsData.Tables(5).TableName = "Table5"
            rep.Dispose()
            ExportTemplate("Payroll/Business/TEMP_IMPORT_HOSOLUONG.xlsx",
                                      dsData, Nothing, "Template_HoSoLuong_" & Format(Date.Now, "yyyyMMdd"))

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    'Private Sub TR_ASSESSMENT_RESULT()
    '    Try
    '        Dim rep As New Training.TrainingRepository
    '        Dim lst = rep.GetAssessmentResultByID(New Training.TrainingBusiness.AssessmentResultDtlDTO With {
    '                                              .EMPLOYEE_ID = Decimal.Parse(Request.Params("EMPLOYEE_ID")),
    '                                              .TR_CHOOSE_FORM_ID = Request.Params("TR_CHOOSE_FORM_ID")})
    '        Dim dtData As DataTable = lst.ToTable
    '        dtData.TableName = "DATA"
    '        Dim dtVar As DataTable = dtData.Clone
    '        If dtData.Rows.Count > 0 Then
    '            dtVar.ImportRow(dtData.Rows(0))
    '        End If
    '        ExportTemplate("Training\Import\AssessmentResult.xls", _
    '                                  dtData, dtVar, _
    '                                  "TemplateImport_" & Format(Date.Now, "yyyyMMdd"))
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    Private Sub Template_yeucautuyendung_Error()
        Try
            Dim dtData = Session("EXPORTREPORT")
            Dim dtVar As DataTable = dtData.Clone
            If dtData.Rows.Count > 0 Then
                dtVar.ImportRow(dtData.Rows(0))
            End If
            ExportTemplate("Recruitment\Import\Template_yeucautuyendung_Error.xls",
                                      dtData, dtVar,
                                      "TemplateImportError_" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Template_Import_HRDetail_error()
        Try
            Dim dtData = Session("HR_ERR")
            Dim dtVar As DataTable = dtData.Clone
            Session.Remove("HR_ERR")
            If dtData.Rows.Count > 0 Then
                dtVar.ImportRow(dtData.Rows(0))
            End If
            ExportTemplate("Recruitment\Import\Template_Import_HRDetail_error.xls",
                                      dtData, dtVar,
                                      "TemplateImportError_" & Format(Date.Now, "yyyyMMdd"))

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Template_Import_HRBudgetDetail_error()
        Try
            Dim dtData = Session("HR_BUDGET_ERR")
            Dim dtVar As DataTable = dtData.Clone
            Session.Remove("HR_BUDGET_ERR")
            If dtData.Rows.Count > 0 Then
                dtVar.ImportRow(dtData.Rows(0))
            End If
            ExportTemplate("Recruitment\Import\Template_Import_HRBudgetDetail_error.xls",
                                      dtData, dtVar,
                                      "TemplateImportHRBudgetError_" & Format(Date.Now, "yyyyMMdd"))

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Template_Import_Nguoiky_error()
        Try
            Dim dtData = Session("SIGN_ERR")
            Dim dtVar As DataTable = dtData.Clone
            Session.Remove("SIGN_ERR")
            If dtData.Rows.Count > 0 Then
                dtVar.ImportRow(dtData.Rows(0))
            End If
            ExportTemplate("Profile\Import\Template_Import_NguoiKy_Error.xls",
                                      dtData, dtVar,
                                      "Template_Import_NguoiKy_Error" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TEMP_IMPORT_KPI()
        Try
            Dim dtData = Session("EXPORTREPORT")
            ExportTemplate("Payroll\Business\KPI_VP_error.xls",
                                      dtData, Nothing,
                                      "KPI_VP_error" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub Template_Import_TimeSheet_Monthly_Error()
        Try
            Dim dtData As DataTable = Session("TS_MONTHLY_ERR")
            Session.Remove("TS_MONTHLY_ERR")
            Dim dtVar As DataTable = dtData.Clone
            If dtData.Rows.Count > 0 Then
                dtVar.ImportRow(dtData.Rows(0))
            End If
            ExportTemplate("Attendance\Import\Template_Import_TimeSheet_Monthly_Error.xls",
                                      dtData, dtVar,
                                      "Template_Import_TimeSheet_Monthly_Error" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Template_Import_DTTD_DTPB_Error()
        Try
            Dim dtData As DataTable = Session("DTTD_DTPB_ERR")
            Session.Remove("DTTD_DTPB_ERR")
            Dim dtVar As DataTable = dtData.Clone
            If dtData.Rows.Count > 0 Then
                dtVar.ImportRow(dtData.Rows(0))
            End If
            ExportTemplate("Payroll\Import\Template_Import_DTTD_DTPB_Error.xls",
                                      dtData, dtVar,
                                      "Template_Import_DTTD_DTPB_Error" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Template_Import_DieuChinhConghoachtoan_Err()
        Try
            Dim dtData As DataTable = Session("ADJUSTING_ERR")
            Session.Remove("ADJUSTING_ERR")
            Dim dtVar As DataTable = dtData.Clone
            If dtData.Rows.Count > 0 Then
                dtVar.ImportRow(dtData.Rows(0))
            End If
            ExportTemplate("Payroll\Import\Template_Import_DieuChinhConghoachtoan_Err.xls",
                                      dtData, dtVar,
                                      "Template_Import_DieuChinhConghoachtoan_Err" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Template_Import_DTTD_ECD_Error()
        Try
            Dim dtData As DataTable = Session("DTTD_ECD_ERR")
            Session.Remove("DTTD_ECD_ERR")
            Dim dtVar As DataTable = dtData.Clone
            If dtData.Rows.Count > 0 Then
                dtVar.ImportRow(dtData.Rows(0))
            End If
            ExportTemplate("Payroll\Import\Template_Import_DTTD_ECD_Error.xls",
                                      dtData, dtVar,
                                      "Template_Import_DTTD_ECD_Error" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Template_Import_LDT_VP_Error()
        Try
            Dim dtData As DataTable = Session("LDT_VP_ERR")
            Session.Remove("LDT_VP_ERR")
            Dim dtVar As DataTable = dtData.Clone
            If dtData.Rows.Count > 0 Then
                dtVar.ImportRow(dtData.Rows(0))
            End If
            ExportTemplate("Payroll\Import\Template_Import_LDT_VP_Error.xls",
                                      dtData, dtVar,
                                      "Template_Import_LDT_VP_Error" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub Template_import_MA_SCP_QLCH_Error()
        Try
            Dim dtData As DataTable = Session("MA_SCP_QLCH_Error")
            Session.Remove("MA_SCP_QLCH_Error")
            Dim dtVar As DataTable = dtData.Clone
            If dtData.Rows.Count > 0 Then
                dtVar.ImportRow(dtData.Rows(0))
            End If
            ExportTemplate("Payroll\Import\Template_import_MA_SCP_QLCH_Error.xls",
                                      dtData, dtVar,
                                      "Template_import_DTCHTT_Error" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub Template_Import_PA_TARGET_STORE_Error()
        Try
            Dim dtData As DataTable = Session("PA_TARGET_STORE")
            Session.Remove("PA_TARGET_STORE")
            Dim dtVar As DataTable = dtData.Clone
            If dtData.Rows.Count > 0 Then
                dtVar.ImportRow(dtData.Rows(0))
            End If
            ExportTemplate("Payroll\Import\Template_Import_PA_TARGET_STORE_Error.xlsx",
                                      dtData, dtVar,
                                      "Template_Import_PA_TARGET_STORE_Error" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub Template_Import_PA_STANDARD_SETUP_Error()
        Try
            Dim dtData As DataTable = Session("PA_STANDARD_SETUP")
            Session.Remove("PA_STANDARD_SETUP")
            Dim dtVar As DataTable = dtData.Clone
            If dtData.Rows.Count > 0 Then
                dtVar.ImportRow(dtData.Rows(0))
            End If
            ExportTemplate("Payroll\Import\Template_Import_PA_STANDARD_SETUP_Error.xls",
                                      dtData, dtVar,
                                      "Template_Import_PA_STANDARD_SETUP_Error" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub Taikhoan_Error()
        Try
            Dim dtData As DataTable = Session("ListUser")
            Session.Remove("ListUser")
            Dim dtVar As DataTable = dtData.Clone
            If dtData.Rows.Count > 0 Then
                dtVar.ImportRow(dtData.Rows(0))
            End If
            ExportTemplate("Common\Approve\Import_Taikhoan_Error.xls",
                                      dtData, dtVar,
                                      "Import_Taikhoan_Error" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub Template_Import_HRDetail()
        Try
            Dim store As New Recruitment.RecruitmentStoreProcedure
            Dim org_id = Decimal.Parse(Request.Params("ORGID"))
            Dim username = Decimal.Parse(Request.Params("USERNAME"))
            Dim isdissolve = Decimal.Parse(Request.Params("ISDISSOLVE"))
            Dim HRYear_Plan = Decimal.Parse(Request.Params("YEAR_PLANE_ID"))
            Dim dsData As DataSet = store.EXPORT_DATA_HR_DETAIL(org_id, username, isdissolve, HRYear_Plan)
            ExportTemplate("Recruitment\Import\Template_Import_HRDetail.xlsx",
                                      dsData, Nothing,
                                      "DB_NHAN_SU_" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TR_ASSESSMENT_RESULT_ERROR()
        Try
            Dim dtData = Session("EXPORTREPORT")
            Dim dtVar As DataTable = dtData.Clone
            If dtData.Rows.Count > 0 Then
                dtVar.ImportRow(dtData.Rows(0))
            End If
            ExportTemplate("Training\Import\AssessmentResult_Error.xls",
                                      dtData, dtVar,
                                      "TemplateImportError_" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TR_REQUEST_EMPLOYEE()
        Try
            Dim dtData As New DataTable
            dtData.TableName = "DATA"
            ExportTemplate("Training\Import\RequestEmployee.xls",
                                      dtData, Nothing,
                                      "TemplateImport_" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TR_REQUEST_EMPLOYEE_ERROR()
        Try
            Dim dtData = Session("EXPORTREPORT")
            ExportTemplate("Training\Import\RequestEmployee_Error.xls",
                                      dtData, Nothing,
                                      "TemplateImportError_" & Format(Date.Now, "yyyyMMdd"))

            Session("EXPORTREPORT") = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub Template_Import_PE_Org_Mr_Rr_Error()
        Try
            Dim dtData As DataTable = Session("PE_ORG_MR_RR")
            Session.Remove("PE_ORG_MR_RR")
            Dim dtVar As DataTable = dtData.Clone
            If dtData.Rows.Count > 0 Then
                dtVar.ImportRow(dtData.Rows(0))
            End If
            ExportTemplate("Performance\Import\Template_Import_PE_Org_Mr_Rr_Error.xls",
                                      dtData, dtVar,
                                      "Template_Import_PE_Org_Mr_Rr_Error" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Template_Import_CTNN_Error()
        Try
            Dim dsData As DataSet = Session("CTNN_ERR")
            Session.Remove("CTNN_ERR")
            Dim dtVar As DataTable = dsData.Tables(0).Clone
            If dsData.Tables(0).Rows.Count > 0 Then
                dtVar.ImportRow(dsData.Tables(0).Rows(0))
            End If
            ExportTemplate_XLSX("Profile\Import\Template_Import_CTNN_Error.xlsx",
                                      dsData, dtVar,
                                      "Template_Import_CTNN_Error" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Template_Import_Travel_Error()
        Try
            Dim dsData As DataSet = Session("TRAVEL_ERR")
            Session.Remove("TRAVEL_ERR")
            Dim dtVar As DataTable = dsData.Tables(0).Clone
            If dsData.Tables(0).Rows.Count > 0 Then
                dtVar.ImportRow(dsData.Tables(0).Rows(0))
            End If
            ExportTemplate_XLSX("Profile\Import\Template_Import_Travel_Error.xlsx",
                                      dsData, dtVar,
                                      "Template_Import_Travel_Error" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Template_Import_HDCB_Error()
        Try
            Dim dsData As DataSet = Session("CBPLANNING_ERROR")
            Session.Remove("CBPLANNING_ERROR")
            Dim dtVar As DataTable = dsData.Tables(0).Clone
            If dsData.Tables(0).Rows.Count > 0 Then
                dtVar.ImportRow(dsData.Tables(0).Rows(0))
            End If
            ExportTemplate_XLSX("Profile\Import\Template_Import_HDCB_Error.xlsx",
                                      dsData, dtVar,
                                      "Template_Import_HDCB_Error" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Template_Import_CB_Assessent_Error()
        Try
            Dim dsData As DataSet = Session("CBASSESSMENT_ERROR")
            Session.Remove("CBASSESSMENT_ERROR")
            Dim dtVar As DataTable = dsData.Tables(0).Clone
            If dsData.Tables(0).Rows.Count > 0 Then
                dtVar.ImportRow(dsData.Tables(0).Rows(0))
            End If
            ExportTemplate_XLSX("Profile\Import\Template_Import_CB_Assessent_Error.xlsx",
                                      dsData, dtVar,
                                      "Template_Import_CB_Assessent_Error" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Template_Import_Employee_NPT_Error()
        Try
            Dim dsData As DataSet = Session("EMPLOYEE_NPT")
            Session.Remove("EMPLOYEE_NPT")
            Dim dtVar As DataTable = dsData.Tables(0).Clone
            If dsData.Tables(0).Rows.Count > 0 Then
                dtVar.ImportRow(dsData.Tables(0).Rows(0))
            End If
            ExportTemplate_XLSX("Profile\Import\Template_Import_Employee_NPT_Error.xlsx",
                                      dsData, dtVar,
                                      "Template_Import_Employee_NPT_Error" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Template_Import_TLHD_Error()
        Try
            Dim dsData As DataSet = Session("COMMITEE_ERROR")
            Session.Remove("COMMITEE_ERROR")
            Dim dtVar As DataTable = dsData.Tables(0).Clone
            If dsData.Tables(0).Rows.Count > 0 Then
                dtVar.ImportRow(dsData.Tables(0).Rows(0))
            End If
            ExportTemplate_XLSX("Profile\Import\Template_Import_TLHD_Error.xlsx",
                                      dsData, dtVar,
                                      "Template_Import_TLHD_Error" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Import_phucap_err()
        Try
            Dim dsData As DataSet = Session("ALLOWANCE_ERR")
            Session.Remove("ALLOWANCE_ERR")
            Dim dtVar As DataTable = dsData.Tables(0).Clone
            If dsData.Tables(0).Rows.Count > 0 Then
                dtVar.ImportRow(dsData.Tables(0).Rows(0))
            End If
            ExportTemplate("Profile\Import\Import_phucap_err.xls",
                                      dsData, dtVar,
                                      "Import_phucap_err" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Template_Import_Discipline_Emp_Error()
        Try
            Dim dsData As DataSet = Session("DIS_EMP")
            Session.Remove("DIS_EMP")
            Dim dtVar As DataTable = dsData.Tables(0).Clone
            If dsData.Tables(0).Rows.Count > 0 Then
                dtVar.ImportRow(dsData.Tables(0).Rows(0))
            End If
            ExportTemplate_XLSX("Profile\Import\Template_Import_Discipline_Emp_Error.xls",
                                      dsData, dtVar,
                                      "Template_Import_Discipline_Emp_Error" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    'Private Sub RC_CANDIDATE_IMPORT()
    '    Try
    '        Dim rep As New Recruitment.RecruitmentRepository
    '        Dim ds = rep.GetCandidateImport()
    '        ds.Tables(0).TableName = "DATA"
    '        Dim i As Integer = 1
    '        For Each dt As DataTable In ds.Tables
    '            If dt.TableName <> "DATA" Then
    '                dt.TableName = "DATA" & i.ToString
    '                i += 1
    '            Else
    '                ' Tạo dữ liệu vlookup
    '                For i1 = 1 To 500
    '                    Dim row = dt.NewRow
    '                    dt.Rows.Add(row)
    '                Next
    '            End If
    '        Next
    '        ExportTemplate("Recruitment\Import\Candidate.xls", _
    '                                  ds, Nothing, _
    '                                  "TemplateImport_" & Format(Date.Now, "yyyyMMdd"))
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    Private Sub RC_CANDIDATE_IMPORT_ERROR()
        Try
            Dim dtData = Session("EXPORTREPORT")

            ExportTemplate("Recruitment\Import\Candidate_Error.xls",
                                      dtData, Nothing,
                                      "TemplateImportError_" & Format(Date.Now, "yyyyMMdd"))

            Session("EXPORTREPORT") = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ManIOImport()
        Try
            Dim rep As New Attendance.AttendanceRepository
            Dim org_id = Decimal.Parse(Request.Params("orgid"))
            'Dim is_disolve = Boolean.Parse(Request.Params("IS_DISSOLVE"))

            Dim obj As New Attendance.AttendanceBusiness.ParamDTO
            obj.ORG_ID = org_id
            obj.IS_DISSOLVE = False
            obj.P_EXPORT_TYPE = 1
            If Not String.IsNullOrEmpty(Request.Params("PERIOD_ID")) Then
                obj.PERIOD_ID = Decimal.Parse(Request.Params("PERIOD_ID"))
            End If
            Dim dsData As DataSet = rep.GetDataFromOrg(obj)
            dsData.Tables(0).TableName = "Table"
            ExportTemplate("Attendance\Import\Template_importIO.xls",
                                      dsData, Nothing,
                                      "Template_importIO" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Time_TimeSheetCCT()
        Try
            Dim rep As New Attendance.AttendanceRepository
            Dim obj As New Attendance.AttendanceBusiness.ParamDTO
            obj.P_EXPORT_TYPE = 10
            If Not String.IsNullOrEmpty(Request.Params("PERIOD_ID")) Then
                obj.PERIOD_ID = Decimal.Parse(Request.Params("PERIOD_ID"))
            End If
            If Not String.IsNullOrEmpty(Request.Params("orgid")) Then
                obj.ORG_ID = Decimal.Parse(Request.Params("orgid"))
            Else
                obj.ORG_ID = 0
            End If
            obj.IS_DISSOLVE = 0
            If Not String.IsNullOrEmpty(Request.Params("objEmp")) Then
                obj.EMP_OBJ = Decimal.Parse(Request.Params("objEmp"))
            End If
            Dim dsData As DataSet = rep.GetDataFromOrg(obj)
            dsData.Tables(0).TableName = "Table1"
            Dim dtvariable As New DataTable
            dtvariable.Columns.Add("D1", GetType(System.String))
            dtvariable.Columns.Add("D2", GetType(System.String))
            dtvariable.Columns.Add("D3", GetType(System.String))
            dtvariable.Columns.Add("D4", GetType(System.String))
            dtvariable.Columns.Add("D5", GetType(System.String))
            dtvariable.Columns.Add("D6", GetType(System.String))
            dtvariable.Columns.Add("D7", GetType(System.String))
            dtvariable.Columns.Add("D8", GetType(System.String))
            dtvariable.Columns.Add("D9", GetType(System.String))
            dtvariable.Columns.Add("D10", GetType(System.String))

            dtvariable.Columns.Add("D11", GetType(System.String))
            dtvariable.Columns.Add("D12", GetType(System.String))
            dtvariable.Columns.Add("D13", GetType(System.String))
            dtvariable.Columns.Add("D14", GetType(System.String))
            dtvariable.Columns.Add("D15", GetType(System.String))
            dtvariable.Columns.Add("D16", GetType(System.String))
            dtvariable.Columns.Add("D17", GetType(System.String))
            dtvariable.Columns.Add("D18", GetType(System.String))
            dtvariable.Columns.Add("D19", GetType(System.String))
            dtvariable.Columns.Add("D20", GetType(System.String))

            dtvariable.Columns.Add("D21", GetType(System.String))
            dtvariable.Columns.Add("D22", GetType(System.String))
            dtvariable.Columns.Add("D23", GetType(System.String))
            dtvariable.Columns.Add("D24", GetType(System.String))
            dtvariable.Columns.Add("D25", GetType(System.String))
            dtvariable.Columns.Add("D26", GetType(System.String))
            dtvariable.Columns.Add("D27", GetType(System.String))
            dtvariable.Columns.Add("D28", GetType(System.String))
            dtvariable.Columns.Add("D29", GetType(System.String))
            dtvariable.Columns.Add("D30", GetType(System.String))
            dtvariable.Columns.Add("D31", GetType(System.String))

            Dim lsData As AT_PERIODDTO
            Dim period As New AT_PERIODDTO
            period.PERIOD_ID = obj.PERIOD_ID
            'lsData = rep.LOAD_PERIODByID(period)
            lsData = rep.Load_date(obj.PERIOD_ID, obj.EMP_OBJ)
            Dim dDay = lsData.START_DATE
            Dim row = dtvariable.NewRow
            Dim I As Integer = 1
            While dDay <= lsData.END_DATE
                row("D" & I) = dDay.Value.ToString("dd") & "/" & dDay.Value.ToString("MM")
                dDay = dDay.Value.AddDays(1)
                I += 1
            End While
            dtvariable.Rows.Add(row)
            dtvariable.TableName = "Variable"
            Dim dtData = CType(Session("EXPORTTIMESHEETDAILY"), DataTable)
            dtData = dtData.Copy
            dsData.Tables.Add(dtData)
            dsData.Tables(1).TableName = "Table"
            'ExportTemplate("Attendance\Import\Template_importTimesheet_CTT.xls", _
            '                          dsData, dtvariable, _
            '                          "Template_importTimesheet_CTT" & Format(Date.Now, "yyyyMMdd"))
            ExportTemplate("Attendance\Import\Template_importTimesheet_CTT.xls", dsData, dtvariable, "Template_importTimesheet_CTT" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Time_TimeSheet_Rice()
        Try
            Dim rep As New Attendance.AttendanceRepository
            Dim obj As New Attendance.AttendanceBusiness.ParamDTO
            obj.P_EXPORT_TYPE = 4
            If Not String.IsNullOrEmpty(Request.Params("PERIOD_ID")) Then
                obj.PERIOD_ID = Decimal.Parse(Request.Params("PERIOD_ID"))
            End If
            obj.ORG_ID = 0
            obj.IS_DISSOLVE = 0
            Dim dsData As DataSet = rep.GetDataFromOrg(obj)
            dsData.Tables(0).TableName = "Table1"
            Dim dtvariable As New DataTable
            dtvariable.Columns.Add("D1", GetType(System.String))
            dtvariable.Columns.Add("D2", GetType(System.String))
            dtvariable.Columns.Add("D3", GetType(System.String))
            dtvariable.Columns.Add("D4", GetType(System.String))
            dtvariable.Columns.Add("D5", GetType(System.String))
            dtvariable.Columns.Add("D6", GetType(System.String))
            dtvariable.Columns.Add("D7", GetType(System.String))
            dtvariable.Columns.Add("D8", GetType(System.String))
            dtvariable.Columns.Add("D9", GetType(System.String))
            dtvariable.Columns.Add("D10", GetType(System.String))

            dtvariable.Columns.Add("D11", GetType(System.String))
            dtvariable.Columns.Add("D12", GetType(System.String))
            dtvariable.Columns.Add("D13", GetType(System.String))
            dtvariable.Columns.Add("D14", GetType(System.String))
            dtvariable.Columns.Add("D15", GetType(System.String))
            dtvariable.Columns.Add("D16", GetType(System.String))
            dtvariable.Columns.Add("D17", GetType(System.String))
            dtvariable.Columns.Add("D18", GetType(System.String))
            dtvariable.Columns.Add("D19", GetType(System.String))
            dtvariable.Columns.Add("D20", GetType(System.String))

            dtvariable.Columns.Add("D21", GetType(System.String))
            dtvariable.Columns.Add("D22", GetType(System.String))
            dtvariable.Columns.Add("D23", GetType(System.String))
            dtvariable.Columns.Add("D24", GetType(System.String))
            dtvariable.Columns.Add("D25", GetType(System.String))
            dtvariable.Columns.Add("D26", GetType(System.String))
            dtvariable.Columns.Add("D27", GetType(System.String))
            dtvariable.Columns.Add("D28", GetType(System.String))
            dtvariable.Columns.Add("D29", GetType(System.String))
            dtvariable.Columns.Add("D30", GetType(System.String))
            dtvariable.Columns.Add("D31", GetType(System.String))

            Dim lsData As AT_PERIODDTO
            Dim period As New AT_PERIODDTO
            period.PERIOD_ID = obj.PERIOD_ID
            lsData = rep.LOAD_PERIODByID(period)
            Dim dDay = lsData.START_DATE
            Dim row = dtvariable.NewRow
            While dDay <= lsData.END_DATE
                row("D" & dDay.Value.Day) = dDay.Value.ToString("dd") & "/" & dDay.Value.ToString("MM")
                dDay = dDay.Value.AddDays(1)
            End While
            dtvariable.Rows.Add(row)
            dtvariable.TableName = "Variable"
            Dim dtData = CType(Session("EXPORTIMESHEETRICE"), DataTable)
            dtData = dtData.Copy
            dsData.Tables.Add(dtData)
            dsData.Tables(1).TableName = "Table"
            ExportTemplate("Attendance\Import\Template_importTimesheet_Rice.xls",
                                      dsData, dtvariable,
                                      "Template_importTimesheet_Rice" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Time_TimeSheet_OT()
        Try
            Dim rep As New Attendance.AttendanceRepository
            Dim obj As New Attendance.AttendanceBusiness.ParamDTO
            obj.P_EXPORT_TYPE = 4
            If Not String.IsNullOrEmpty(Request.Params("PERIOD_ID")) Then
                obj.PERIOD_ID = Decimal.Parse(Request.Params("PERIOD_ID"))
            End If
            obj.ORG_ID = 0
            obj.IS_DISSOLVE = 0
            Dim dsData As DataSet = rep.GetDataFromOrg(obj)
            dsData.Tables(0).TableName = "Table1"
            Dim dtvariable As New DataTable
            dtvariable.Columns.Add("D1", GetType(System.String))
            dtvariable.Columns.Add("D2", GetType(System.String))
            dtvariable.Columns.Add("D3", GetType(System.String))
            dtvariable.Columns.Add("D4", GetType(System.String))
            dtvariable.Columns.Add("D5", GetType(System.String))
            dtvariable.Columns.Add("D6", GetType(System.String))
            dtvariable.Columns.Add("D7", GetType(System.String))
            dtvariable.Columns.Add("D8", GetType(System.String))
            dtvariable.Columns.Add("D9", GetType(System.String))
            dtvariable.Columns.Add("D10", GetType(System.String))

            dtvariable.Columns.Add("D11", GetType(System.String))
            dtvariable.Columns.Add("D12", GetType(System.String))
            dtvariable.Columns.Add("D13", GetType(System.String))
            dtvariable.Columns.Add("D14", GetType(System.String))
            dtvariable.Columns.Add("D15", GetType(System.String))
            dtvariable.Columns.Add("D16", GetType(System.String))
            dtvariable.Columns.Add("D17", GetType(System.String))
            dtvariable.Columns.Add("D18", GetType(System.String))
            dtvariable.Columns.Add("D19", GetType(System.String))
            dtvariable.Columns.Add("D20", GetType(System.String))

            dtvariable.Columns.Add("D21", GetType(System.String))
            dtvariable.Columns.Add("D22", GetType(System.String))
            dtvariable.Columns.Add("D23", GetType(System.String))
            dtvariable.Columns.Add("D24", GetType(System.String))
            dtvariable.Columns.Add("D25", GetType(System.String))
            dtvariable.Columns.Add("D26", GetType(System.String))
            dtvariable.Columns.Add("D27", GetType(System.String))
            dtvariable.Columns.Add("D28", GetType(System.String))
            dtvariable.Columns.Add("D29", GetType(System.String))
            dtvariable.Columns.Add("D30", GetType(System.String))
            dtvariable.Columns.Add("D31", GetType(System.String))

            Dim lsData As AT_PERIODDTO
            Dim period As New AT_PERIODDTO
            period.PERIOD_ID = obj.PERIOD_ID
            lsData = rep.LOAD_PERIODByID(period)
            Dim dDay = lsData.START_DATE
            Dim row = dtvariable.NewRow
            While dDay <= lsData.END_DATE
                row("D" & dDay.Value.Day) = dDay.Value.ToString("dd") & "/" & dDay.Value.ToString("MM")
                dDay = dDay.Value.AddDays(1)
            End While
            dtvariable.Rows.Add(row)
            dtvariable.TableName = "Variable"
            Dim dtData = CType(Session("EXPORTIMESHEETOT"), DataTable)
            dtData = dtData.Copy
            dsData.Tables.Add(dtData)
            dsData.Tables(1).TableName = "Table"
            ExportTemplate("Attendance\Import\Template_importTimesheet_OT.xls",
                                      dsData, dtvariable,
                                      "Template_importTimesheet_OT" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Timesheet_machineExport()
        Try
            ExportTemplate("Attendance\Import\Template_GiaiTrinhNgayCong.xlsx",
                                      New DataSet(), Nothing,
                                      "Template_GiaiTrinhNgayCong" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub TEMP_IMPORT_BCCC()
        Try
            Dim dtData = Session("EXPORTREPORT")
            ExportTemplate("Profile\Import\TEMP_IMPORT_BCCC_ERR.xls",
                                      dtData, Nothing,
                                      "TEMP_IMPORT_BCCC_ERR" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub TEMP_IMPORT_HOSOLUONG()
        Try
            Dim dtData = Session("EXPORTREPORT")
            ExportTemplate("Payroll\Business\TEMP_IMPORT_HOSOLUONG_error.xls",
                                      dtData, Nothing,
                                      "TEMP_IMPORT_HOSOLUONG_error" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub TEMP_IMPORT_OFFERLETER()
        Try
            Dim dtData = Session("EXPORTREPORT")
            ExportTemplate("\Recruitment\Import\Import_OfferLetter_error.xls",
                                      dtData, Nothing,
                                      "Import_OfferLetter_error" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub IMPORT_RESULT_ERROR()
        Try
            Dim dtData = Session("EXPORTREPORT")
            ExportTemplate("\Training\\Import\ImportResultError.xlsx",
                                      dtData, Nothing,
                                      "ImportResultError" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ImportBoiHoanDT_ERROR()
        Try
            Dim dtData = Session("EXPORTREPORT")
            ExportTemplate("\Training\\Import\ImportBoiHoanDT_ERROR.xlsx",
                                      dtData, Nothing,
                                      "ImportBoiHoanDT_ERROR" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub Import_RollCall_Error()
        Try
            Dim dtData = Session("PROGRAM_CLASS_ROLL")
            ExportTemplate("\Training\\Import\Import_RollCall_Error.xlsx",
                                      dtData, Nothing,
                                      "Import_RollCall_Error" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub EXPORTREPORT_ACCIDENT()
        Try
            Dim dtData = Session("EXPORTREPORT_ACCIDENT")
            ExportTemplate("Profile\Import\Import_BTTN_ERR.xls",
                                      dtData, Nothing,
                                      "TEMP_IMPORT_ACCIDENT_ERR" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TEMP_IMPORT_HEALTHEXAM()
        Try
            Dim dtData = Session("EXPORTREPORT_HEALTHEXAM")
            ExportTemplate("Profile\Import\Import_health_exam_ERR.xls",
                                      dtData, Nothing,
                                      "TEMP_IMPORT_HEALTHEXAM_ERR" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub TEMP_IMPORT_CONTRACT()
        Try
            Dim dtData = Session("EXPORTREPORT")
            ExportTemplate("Profile\Import\TEMP_IMPORT_HOPDONG_ERR.xls",
                                      dtData, Nothing,
                                      "TEMP_IMPORT_HOPDONG_ERR" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TEMP_IMPORT_HSTDT()
        Try
            Dim dtData = Session("EXPORTREPORT")
            ExportTemplate("Payroll\Import\Template_ThietLap_HeSo_TDT_ERROR.xls",
                                      dtData, Nothing,
                                      "Template_ThietLap_HeSo_TDT_ERROR" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub WorkNightImport()
        Try
            Dim rep As New Attendance.AttendanceRepository
            Dim org_id = Decimal.Parse(Request.Params("orgid"))
            Dim emp_obj = Decimal.Parse(Request.Params("emp_obj"))
            'Dim is_disolve = Boolean.Parse(Request.Params("IS_DISSOLVE"))
            Dim obj As New Attendance.AttendanceBusiness.ParamDTO


            obj.ORG_ID = org_id
            obj.IS_DISSOLVE = True
            obj.P_EXPORT_TYPE = 13
            If Not String.IsNullOrEmpty(Request.Params("PERIOD_ID")) Then
                obj.PERIOD_ID = Decimal.Parse(Request.Params("PERIOD_ID"))
            End If
            Dim ddate = rep.Load_date(CDec(Val(obj.PERIOD_ID)), CDec(Val(emp_obj)))

            If ddate.END_DATE IsNot Nothing And ddate.START_DATE IsNot Nothing Then
                obj.START_DATE = ddate.START_DATE
                obj.END_DATE = ddate.END_DATE
            End If
            obj.EMP_OBJ = emp_obj
            Dim dsData As DataSet = rep.GetDataFromOrg(obj)
            dsData.Tables(0).TableName = "Table"
            Dim dtvariable As New DataTable
            dtvariable.Columns.Add("D1", GetType(Date))
            dtvariable.Columns.Add("D2", GetType(Date))
            dtvariable.Columns.Add("D3", GetType(Date))
            dtvariable.Columns.Add("D4", GetType(Date))
            dtvariable.Columns.Add("D5", GetType(Date))
            dtvariable.Columns.Add("D6", GetType(Date))
            dtvariable.Columns.Add("D7", GetType(Date))
            dtvariable.Columns.Add("D8", GetType(Date))
            dtvariable.Columns.Add("D9", GetType(Date))
            dtvariable.Columns.Add("D10", GetType(Date))

            dtvariable.Columns.Add("D11", GetType(Date))
            dtvariable.Columns.Add("D12", GetType(Date))
            dtvariable.Columns.Add("D13", GetType(Date))
            dtvariable.Columns.Add("D14", GetType(Date))
            dtvariable.Columns.Add("D15", GetType(Date))
            dtvariable.Columns.Add("D16", GetType(Date))
            dtvariable.Columns.Add("D17", GetType(Date))
            dtvariable.Columns.Add("D18", GetType(Date))
            dtvariable.Columns.Add("D19", GetType(Date))
            dtvariable.Columns.Add("D20", GetType(Date))

            dtvariable.Columns.Add("D21", GetType(Date))
            dtvariable.Columns.Add("D22", GetType(Date))
            dtvariable.Columns.Add("D23", GetType(Date))
            dtvariable.Columns.Add("D24", GetType(Date))
            dtvariable.Columns.Add("D25", GetType(Date))
            dtvariable.Columns.Add("D26", GetType(Date))
            dtvariable.Columns.Add("D27", GetType(Date))
            dtvariable.Columns.Add("D28", GetType(Date))
            dtvariable.Columns.Add("D29", GetType(Date))
            dtvariable.Columns.Add("D30", GetType(Date))
            dtvariable.Columns.Add("D31", GetType(Date))

            Dim lsData As AT_PERIODDTO
            Dim period As New AT_PERIODDTO
            period.PERIOD_ID = obj.PERIOD_ID
            lsData = rep.LOAD_PERIODByID(period)
            Dim dDay = obj.START_DATE 'lsData.START_DATE
            Dim row = dtvariable.NewRow
            Dim i As Integer = 1
            While dDay <= obj.END_DATE 'lsData.END_DATE
                row("D" & i) = dDay.Value
                dDay = dDay.Value.AddDays(1)
                i = i + 1
            End While
            dtvariable.Rows.Add(row)
            dtvariable.TableName = "Variable"
            dsData.Tables.Add(dtvariable)
            Dim dtHoliday = rep.GetHoliday(New AT_HOLIDAYDTO).ToTable()
            dtHoliday.TableName = "HOLIDAY"
            dsData.Tables.Add(dtHoliday)
            ExportTemplate_XLSX("Attendance\Import\Template_ImportWorkNight.xlsx",
                                      dsData, Nothing,
                                      "Template_ImportWorkNight" & Format(Date.Now, "yyyyMMdd"))

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub WorkShiftImport()
        Try
            Dim rep As New Attendance.AttendanceRepository
            Dim org_id = Decimal.Parse(Request.Params("orgid"))
            Dim emp_obj = Decimal.Parse(Request.Params("emp_obj"))
            'Dim is_disolve = Boolean.Parse(Request.Params("IS_DISSOLVE"))
            Dim obj As New Attendance.AttendanceBusiness.ParamDTO


            obj.ORG_ID = org_id
            obj.IS_DISSOLVE = True
            obj.P_EXPORT_TYPE = 6
            If Not String.IsNullOrEmpty(Request.Params("PERIOD_ID")) Then
                obj.PERIOD_ID = Decimal.Parse(Request.Params("PERIOD_ID"))
            End If
            Dim ddate = rep.Load_date(CDec(Val(obj.PERIOD_ID)), CDec(Val(emp_obj)))

            If ddate.END_DATE IsNot Nothing And ddate.START_DATE IsNot Nothing Then
                obj.START_DATE = ddate.START_DATE
                obj.END_DATE = ddate.END_DATE
            End If
            obj.EMP_OBJ = emp_obj
            Dim dsData As DataSet = rep.GetDataFromOrg(obj)
            dsData.Tables(0).TableName = "Table"
            Dim dtvariable As New DataTable
            dtvariable.Columns.Add("D1", GetType(Date))
            dtvariable.Columns.Add("D2", GetType(Date))
            dtvariable.Columns.Add("D3", GetType(Date))
            dtvariable.Columns.Add("D4", GetType(Date))
            dtvariable.Columns.Add("D5", GetType(Date))
            dtvariable.Columns.Add("D6", GetType(Date))
            dtvariable.Columns.Add("D7", GetType(Date))
            dtvariable.Columns.Add("D8", GetType(Date))
            dtvariable.Columns.Add("D9", GetType(Date))
            dtvariable.Columns.Add("D10", GetType(Date))

            dtvariable.Columns.Add("D11", GetType(Date))
            dtvariable.Columns.Add("D12", GetType(Date))
            dtvariable.Columns.Add("D13", GetType(Date))
            dtvariable.Columns.Add("D14", GetType(Date))
            dtvariable.Columns.Add("D15", GetType(Date))
            dtvariable.Columns.Add("D16", GetType(Date))
            dtvariable.Columns.Add("D17", GetType(Date))
            dtvariable.Columns.Add("D18", GetType(Date))
            dtvariable.Columns.Add("D19", GetType(Date))
            dtvariable.Columns.Add("D20", GetType(Date))

            dtvariable.Columns.Add("D21", GetType(Date))
            dtvariable.Columns.Add("D22", GetType(Date))
            dtvariable.Columns.Add("D23", GetType(Date))
            dtvariable.Columns.Add("D24", GetType(Date))
            dtvariable.Columns.Add("D25", GetType(Date))
            dtvariable.Columns.Add("D26", GetType(Date))
            dtvariable.Columns.Add("D27", GetType(Date))
            dtvariable.Columns.Add("D28", GetType(Date))
            dtvariable.Columns.Add("D29", GetType(Date))
            dtvariable.Columns.Add("D30", GetType(Date))
            dtvariable.Columns.Add("D31", GetType(Date))

            Dim lsData As AT_PERIODDTO
            Dim period As New AT_PERIODDTO
            period.PERIOD_ID = obj.PERIOD_ID
            lsData = rep.LOAD_PERIODByID(period)
            Dim dDay = obj.START_DATE 'lsData.START_DATE
            Dim row = dtvariable.NewRow
            Dim i As Integer = 1
            While dDay <= obj.END_DATE 'lsData.END_DATE
                row("D" & i) = dDay.Value
                dDay = dDay.Value.AddDays(1)
                i = i + 1
            End While
            dtvariable.Rows.Add(row)
            dtvariable.TableName = "Variable"
            dsData.Tables.Add(dtvariable)
            Dim dtHoliday = rep.GetHoliday(New AT_HOLIDAYDTO).ToTable()
            dtHoliday.TableName = "HOLIDAY"
            dsData.Tables.Add(dtHoliday)
            ExportTemplate_XLSX("Attendance\Import\Template_ImportShift.xlsx",
                                      dsData, Nothing,
                                      "Template_ImportShift" & Format(Date.Now, "yyyyMMdd"))

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Template_ImportDMVS()
        Try


            Dim store As New Profile.ProfileStoreProcedure
            Dim org_id = Decimal.Parse(Request.Params("orgid"))
            Dim P_USER = Request.Params("PUSER")
            Dim IS_DISSOLVE = 1
            Dim START_DATE As String = ""
            Dim END_DATE As String = ""
            If Not String.IsNullOrEmpty(Request.Params("START_DATE")) Then
                START_DATE = Request.Params("START_DATE")
            End If
            If Not String.IsNullOrEmpty(Request.Params("END_DATE")) Then
                END_DATE = Request.Params("END_DATE")
            End If
            Dim dsData As DataSet = store.GET_OTHER_LIST_DSVM(org_id, START_DATE, END_DATE, IS_DISSOLVE, P_USER)
            dsData.Tables(0).TableName = "Table"
            dsData.Tables(1).TableName = "Table1"
            dsData.Tables(3).TableName = "Table3"
            ExportTemplate("Attendance\Import\Template_ImportDMVS.xls",
                                       dsData, Nothing,
                                      "Template_ImportDMVS" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Template_ImportOT()
        Try
            'Dim rep As New Attendance.AttendanceRepository
            'Dim org_id = Decimal.Parse(Request.Params("orgid"))
            'Dim is_disolve = Boolean.Parse(Request.Params("IS_DISSOLVE"))
            'Dim obj As New Attendance.AttendanceBusiness.ParamDTO
            'obj.ORG_ID = org_id
            'obj.IS_DISSOLVE = is_disolve
            'obj.P_EXPORT_TYPE = 2
            'If Not String.IsNullOrEmpty(Request.Params("PERIOD_ID")) Then
            '    obj.PERIOD_ID = Decimal.Parse(Request.Params("PERIOD_ID"))
            'End If
            'Dim dsData As DataSet = rep.GetDataFromOrg(obj)
            'dsData.Tables(0).TableName = "Table"

            ExportTemplate("Attendance\Import\Template_ImportOT.xls",
                                      New DataSet, Nothing,
                                      "Template_ImportOT" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Template_DeclareOT()
        Try
            'Dim rep As New Attendance.AttendanceRepository
            'Dim org_id = Decimal.Parse(Request.Params("orgid"))
            'Dim obj As New Attendance.AttendanceBusiness.ParamDTO
            'obj.ORG_ID = org_id
            'obj.IS_DISSOLVE = True
            'obj.P_EXPORT_TYPE = 2
            'If Not String.IsNullOrEmpty(Request.Params("PERIOD_ID")) Then
            '    obj.PERIOD_ID = Decimal.Parse(Request.Params("PERIOD_ID"))
            'End If
            'Dim dsData As DataSet = rep.GetDataFromOrg(obj)
            'dsData.Tables(0).TableName = "Table"
            Dim dsData As New DataSet
            ExportTemplate("Attendance\Import\Template_DeclareOT.xls",
                                      dsData, Nothing,
                                      "Template_DeclareOT" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Template_ImportShift_error()
        Try
            Dim dsData As DataSet = Session("EXPORTREPORT")
            ExportTemplate("Attendance\Import\Template_ImportShift_error.xls",
                                      dsData, Nothing,
                                      "Template_ImportShift_error" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Time_TimeSheetCCT_Error()
        Try
            Dim dsData As DataSet = Session("EXPORTREPORT")
            Dim rep As New Attendance.AttendanceRepository
            Dim obj As New Attendance.AttendanceBusiness.ParamDTO
            obj.P_EXPORT_TYPE = 4
            If Not String.IsNullOrEmpty(Request.Params("PERIOD_ID")) Then
                obj.PERIOD_ID = Decimal.Parse(Request.Params("PERIOD_ID"))
            End If
            If Not String.IsNullOrEmpty(Request.Params("orgid")) Then
                obj.ORG_ID = Decimal.Parse(Request.Params("orgid"))
            Else
                obj.ORG_ID = 0
            End If
            obj.IS_DISSOLVE = 0
            Dim dtvariable As New DataTable
            dtvariable.Columns.Add("D1", GetType(System.String))
            dtvariable.Columns.Add("D2", GetType(System.String))
            dtvariable.Columns.Add("D3", GetType(System.String))
            dtvariable.Columns.Add("D4", GetType(System.String))
            dtvariable.Columns.Add("D5", GetType(System.String))
            dtvariable.Columns.Add("D6", GetType(System.String))
            dtvariable.Columns.Add("D7", GetType(System.String))
            dtvariable.Columns.Add("D8", GetType(System.String))
            dtvariable.Columns.Add("D9", GetType(System.String))
            dtvariable.Columns.Add("D10", GetType(System.String))

            dtvariable.Columns.Add("D11", GetType(System.String))
            dtvariable.Columns.Add("D12", GetType(System.String))
            dtvariable.Columns.Add("D13", GetType(System.String))
            dtvariable.Columns.Add("D14", GetType(System.String))
            dtvariable.Columns.Add("D15", GetType(System.String))
            dtvariable.Columns.Add("D16", GetType(System.String))
            dtvariable.Columns.Add("D17", GetType(System.String))
            dtvariable.Columns.Add("D18", GetType(System.String))
            dtvariable.Columns.Add("D19", GetType(System.String))
            dtvariable.Columns.Add("D20", GetType(System.String))

            dtvariable.Columns.Add("D21", GetType(System.String))
            dtvariable.Columns.Add("D22", GetType(System.String))
            dtvariable.Columns.Add("D23", GetType(System.String))
            dtvariable.Columns.Add("D24", GetType(System.String))
            dtvariable.Columns.Add("D25", GetType(System.String))
            dtvariable.Columns.Add("D26", GetType(System.String))
            dtvariable.Columns.Add("D27", GetType(System.String))
            dtvariable.Columns.Add("D28", GetType(System.String))
            dtvariable.Columns.Add("D29", GetType(System.String))
            dtvariable.Columns.Add("D30", GetType(System.String))
            dtvariable.Columns.Add("D31", GetType(System.String))

            Dim lsData As AT_PERIODDTO
            Dim period As New AT_PERIODDTO
            period.PERIOD_ID = obj.PERIOD_ID
            lsData = rep.LOAD_PERIODByID(period)
            Dim dDay = lsData.START_DATE
            Dim row = dtvariable.NewRow
            While dDay <= lsData.END_DATE
                row("D" & dDay.Value.Day) = dDay.Value.ToString("dd") & "/" & dDay.Value.ToString("MM")
                dDay = dDay.Value.AddDays(1)
            End While
            dtvariable.Rows.Add(row)
            dtvariable.TableName = "DATA_HEADER"
            dsData.Tables.Add(dtvariable)
            ExportTemplate("Attendance\Import\Template_importTimesheet_CTT_error.xls",
                                      dsData, Nothing,
                                      "Template_importTimesheet_CTT_error" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Time_TimeSheetCCT_Error1()
        Try
            Dim dtData = Session("EXPORTREPORT")
            ExportTemplate("Attendance\Import\Template_ImportCTT_error.xls",
                                      dtData, Nothing,
                                      "Template_ImportCTT_error" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Template_importIO_error()
        Try
            Dim dtData = Session("EXPORTREPORT")
            ExportTemplate("Attendance\Import\Template_importIO_error.xls",
                                      dtData, Nothing,
                                      "Template_importIO_error" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Template_ImportDMVS_error()
        Try
            Dim dtData = Session("EXPORTREPORT")
            ExportTemplate("Attendance\Import\Template_ImportDMVS_error.xls",
                                      dtData, Nothing,
                                      "Template_ImportDMVS_error" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Template_LamNgoaiGio_error()
        Try
            Dim dtData = Session("EXPORTREPORT")
            ExportTemplate("Attendance\Import\Template_LamNgoaiGio_error.xls",
                                      dtData, Nothing,
                                      "Template_LamNgoaiGio_error" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub Template_ImportOT_error()
        Try
            Dim dtData = Session("EXPORTREPORT")
            ExportTemplate("Attendance\Import\Template_ImportOT_error.xls",
                                      dtData, Nothing,
                                      "Template_ImportOT_error" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    'Template_GiaiTrinhNgayCong_error
    Private Sub Template_GiaiTrinhNgayCong_error()
        Try
            Dim dtData = Session("EXPORTREPORT")
            ExportTemplate("Attendance\Import\Template_GiaiTrinhNgayCong_error.xlsx",
                                      dtData, Nothing,
                                      "Template_GiaiTrinhNgayCong_error" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub Template_ImportSalary()
        Try
            Dim dtColName = Session("IMPORTSALARY_COLNAME")
            Dim dtData = Session("IMPORTSALARY_DATACOL")
            ExportTemplateWithDataCol("Payroll\Business\TEMP_IMPORT_SALARY.xlsx", dtData, dtColName, "TEMP_IMPORTSALARY" & Format(Date.Now, "yyyyMMdd"))

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub Template_ImportThuongHQCV()
        Try
            Dim dtColName = Session("IMPORTSALARY_COLNAME")
            Dim dtData = Session("IMPORTSALARY_DATACOL")
            ExportTemplateWithDataCol("Payroll\Business\TEMP_IMPORT_SALARY.xlsx", dtData, dtColName, "TEMP_IMPORTTHUONGHQCV" & Format(Date.Now, "yyyyMMdd"))

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub Template_ImportQuyThuongHQCV()
        Try
            Dim dtData = Session("IMPORTSALARY_DATACOL")
            ExportTemplate("Payroll\Business\TEMP_IMPORT_BONNUS.xlsx", dtData, "TEMP_IMPORTQUYTHUONGHQCV" & Format(Date.Now, "yyyyMMdd"))

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Template_ImportQTThueTNCN()
        Try
            Dim dtColName = Session("IMPORTSALARY_COLNAME")
            Dim dtData = Session("IMPORTSALARY_DATACOL")
            ExportTemplateWithDataCol("Payroll\Business\TEMP_IMPORT_SALARY.xlsx", dtData, dtColName, "TEMP_IMPORTTHUETNCN" & Format(Date.Now, "yyyyMMdd"))

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Template_ImportSalary_FundMapping()
        Try
            Dim dtColName = Session("IMPORTSALARY_COLNAME")
            Dim dtData = Session("IMPORTSALARY_DATACOL")
            ExportTemplateWithDataColSP("Payroll\Business\TEMP_IMPORT_SALARY_FUND_MAPPING.xlsx", dtData, dtColName, "TEMP_IMPORT_SALARY_FUND_MAPPING" & Format(Date.Now, "yyyyMMdd"), 2)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function ExportTemplateWithDataColSP(ByVal sReportFileName As String,
                                                ByVal dtDataValue As DataTable,
                                                ByVal dtColname As DataTable,
                                                ByVal filename As String,
                                                ByVal indexCol As Integer) As Boolean

        Dim filePath As String
        Dim templatefolder As String

        Dim designer As WorkbookDesigner
        Try

            templatefolder = ConfigurationManager.AppSettings("ReportTemplatesFolder")
            filePath = AppDomain.CurrentDomain.BaseDirectory & "\" & templatefolder & "\" & sReportFileName

            dtDataValue.TableName = "DATA"

            If Not File.Exists(filePath) Then
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "javascriptfunction", "goBack()", True)
                Return False
            End If

            designer = New WorkbookDesigner
            designer.Open(filePath)
            Dim cell As Cells = designer.Workbook.Worksheets(0).Cells
            Dim i As Integer = indexCol
            For Each dr As DataRow In dtColname.Rows
                cell(1, i).PutValue(dr("COLNAME"))
                cell(2, i).PutValue(dr("COLVAL"))
                cell(3, i).PutValue(dr("COLDATA"))
                i += 1
                cell.InsertColumn(i + 1)
            Next
            ' Xoa 2 cot thua cuoi cung
            cell.DeleteColumn(i + 1)
            cell.DeleteColumn(i)

            designer.SetDataSource(dtDataValue)
            designer.Process()
            designer.Workbook.CalculateFormula()
            designer.Workbook.Worksheets(0).AutoFitColumns()
            designer.Workbook.Save(HttpContext.Current.Response, filename & ".xls", ContentDisposition.Attachment, New XlsSaveOptions())

        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function
    Public Function ExportTemplate(ByVal sReportFileName As String,
                                   ByVal dtData As DataTable,
                                   ByVal filename As String) As Boolean

        Dim filePath As String
        Dim templatefolder As String

        Dim designer As WorkbookDesigner
        Try

            templatefolder = ConfigurationManager.AppSettings("ReportTemplatesFolder")
            filePath = AppDomain.CurrentDomain.BaseDirectory & templatefolder & "\" & sReportFileName

            If Not File.Exists(filePath) Then
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "javascriptfunction", "goBack()", True)
                Return False
            End If

            designer = New WorkbookDesigner
            designer.Open(filePath)
            dtData.TableName = "DATA"
            designer.SetDataSource(dtData)
            designer.Process()
            designer.Workbook.CalculateFormula()
            designer.Workbook.Save(HttpContext.Current.Response, filename & ".xls", ContentDisposition.Attachment, New XlsSaveOptions())

        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function
    'Private Sub Template_ImportSeniorityProcess()
    '    Try
    '        Using rep As New Payroll.PayrollRepository
    '            Dim org_id = Decimal.Parse(Request.Params("orgid"))
    '            Dim obj As New Payroll.PayrollBusiness.PASeniorityProcessDTO
    '            obj.ORG_ID = org_id
    '            obj.IS_DISSOLVE = Request.Params("IS_DISSOLVE")
    '            obj.PERIOD_ID = Decimal.Parse(Request.Params("PERIOD_ID"))
    '            Dim dtData = rep.GetSeniorityProcessImport(obj)
    '            dtData.TableName = "DATA"
    '            ExportTemplate("Payroll\Business\TEMP_IMPORT_SENIORITY.xlsx", _
    '                                      dtData, Nothing, _
    '                                      "Template_SeniorityProcess_" & Format(Date.Now, "yyyyMMdd"))

    '        End Using
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    Private Sub Template_ImportSeniorityProcess_error()
        Try
            Dim dtData = Session("EXPORTREPORT")
            ExportTemplate("Payroll\Business\TEMP_IMPORT_SENIORITY_error.xlsx",
                                      dtData, Nothing,
                                      "Template_SeniorityProcess_error" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Template_DeclareOT_error()
        Try
            Dim dtData = Session("EXPORTREPORT")
            ExportTemplate("Attendance\Import\Template_DeclareOT_error.xls",
                                      dtData, Nothing,
                                      "Template_DeclareOT_error" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Template_ImportDM()
        Try
            Dim rep As New Insurance.InsuranceRepository
            Dim dsData As New DataSet
            Dim dtDataDis As DataTable = rep.GetHU_DISTRICT()
            Dim dtDataPro As DataTable = rep.GetHU_PROVINCE()
            Dim dtData As New DataTable
            dtData.Columns.Add("NAME")
            ' Tạo dữ liệu vlookup
            For i1 = 1 To 500
                Dim row = dtData.NewRow
                dtData.Rows.Add(row)
            Next
            dsData.Tables.Add(dtDataDis)
            dsData.Tables(0).TableName = "TbDis"
            dsData.Tables.Add(dtDataPro)
            dsData.Tables(1).TableName = "TbPro"
            dsData.Tables.Add(dtData)
            dsData.Tables(2).TableName = "DATA"

            ExportTemplate("Insurance\Import\Template_DMNoiKhamChuaBenh.xlsx",
                                      dsData, Nothing,
                                      "Template_DMNoiKhamChuaBenh" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Template_ImportDMNK_ERROR()
        Try
            Dim dtData = Session("EXPORTREPORT")
            ExportTemplate("Insurance\Import\Template_DMNoiKhamChuaBenh_ERROR.xlsx",
                                      dtData, Nothing,
                                      "Import_DMNoiKhamChuaBenh_ERROR" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Import_InfoIns()
        Try
            Dim repIns As New Insurance.InsuranceRepository
            Dim repAtt As New Attendance.AttendanceRepository
            Dim dsData As New DataSet
            Dim org_id = Decimal.Parse(Request.Params("orgid"))
            ' lấy ra danh sách nơi khám chữa bệnh.
            Dim dtWhere As DataTable = repIns.GetINS_WHEREEXPORT().ToTable()
            Dim dtStatusSo As DataTable = repIns.GetStatuSo()
            Dim dtStatusHe As DataTable = repIns.GetStatuHE()

            ' lấy ra danh sách nhân viên trong 1 đơn vị được chọn.
            Dim obj As New Attendance.AttendanceBusiness.ParamDTO
            obj.ORG_ID = org_id
            obj.IS_DISSOLVE = True
            obj.P_EXPORT_TYPE = 5
            If Not String.IsNullOrEmpty(Request.Params("PERIOD_ID")) Then
                obj.PERIOD_ID = Decimal.Parse(Request.Params("PERIOD_ID"))
            End If
            Dim dtEmpInOrg As DataSet = repAtt.GetDataFromOrg(obj)
            dtWhere.TableName = "NoiKham"
            dsData.Tables.Add(dtWhere)
            Dim dtEmp = dtEmpInOrg.Tables(0).Copy
            dtEmp.TableName = "EMP"
            dsData.Tables.Add(dtEmp)

            dtStatusSo.TableName = "SO"
            dsData.Tables.Add(dtStatusSo)

            dtStatusHe.TableName = "HE"
            dsData.Tables.Add(dtStatusHe)

            ExportTemplate("Insurance\Import\Template_ImportInfoIns.xlsx",
                                      dsData, Nothing,
                                      "Template_ImportInfoIns" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Template_ImportInfoIns_error()
        Try
            Dim dtData = Session("EXPORTREPORT")
            ExportTemplate("Insurance\Import\Template_ImportInfoIns_Error.xlsx",
                                      dtData, Nothing,
                                      "Template_ImportInfoIns_Error" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TokhaiA01()
        Try
            Dim dtData = Nothing
            ExportTemplate("Insurance\Report\ToKhai.doc",
                                     dtData, Nothing,
                                     "ToKhai" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Template_Register()
        Try
            Dim rep As New Attendance.AttendanceRepository
            Dim dsData As New DataSet
            Dim dtDataTimeManual = rep.GetDataImportCO()
            dtDataTimeManual.TableName = "Manual"

            If dtDataTimeManual IsNot Nothing Then
                dsData.Tables.Add(dtDataTimeManual)
            End If

            ExportTemplate("Attendance\Import\AT_IMPORT_REGISTER_CO.xlsx",
                                      dsData, Nothing,
                                      "AT_IMPOERT_REGISTER_CO" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Template_Register_Error()
        Try
            Dim org_id As Decimal = Request.Params("ORGID")
            Dim is_disolve As Decimal = Request.Params("IS_DISSOLVE")
            Dim dtData = Session("EXPORTREPORT")
            Dim param As New Profile.ProfileBusiness.ParamDTO
            param.ORG_ID = org_id

            param.IS_DISSOLVE = If(is_disolve = 1, True, False)
            Using rep As New Profile.ProfileBusinessRepository

                ExportTemplate("Attendance\Import\AT_IMPORT_REGISTER_CO_Error.xlsx",
                                          dtData, Nothing,
                                          "AT_IMPORT_REGISTER_CO_Error" & Format(Date.Now, "yyyyMMdd"))
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub HU_ChangeInfo()
        Try
            Dim org_id As Decimal = Request.Params("ORGID")
            Dim is_disolve As Decimal = Request.Params("IS_DISSOLVE")
            Dim param As New Profile.ProfileBusiness.ParamDTO
            param.ORG_ID = org_id

            param.IS_DISSOLVE = If(is_disolve = 1, True, False)
            Using rep As New Profile.ProfileBusinessRepository
                Dim ds = rep.GetChangeInfoImport(param)
                ds.Tables(0).TableName = "DATA"
                Dim i As Integer = 1
                For Each dt As DataTable In ds.Tables
                    If dt.TableName <> "DATA" Then
                        dt.TableName = "DATA" & i.ToString
                        i += 1
                    Else
                        ' Tạo dữ liệu vlookup
                        For i1 = 1 To 500
                            Dim row = dt.NewRow
                            dt.Rows.Add(row)
                        Next
                    End If
                Next
                ExportTemplate("Profile\Import\ChangeInfo.xls",
                                          ds, Nothing,
                                          "TemplateImport_" & Format(Date.Now, "yyyyMMdd"))
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub HU_ChangeInfo_Error()
        Try
            Dim dtData = Session("EXPORTREPORT")
            ExportTemplate("Profile\Import\ChangeInfo_Error.xls",
                                      dtData, Nothing,
                                      "TemplateImport_Error_" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub HU_ANNUALLEAVE_PLANS_ERROR()
        Try
            Dim dtData = Session("EXPORTREPORT")
            ExportTemplate("Attendance\Import\Template_QLKeHoachNghiPN _error.xls",
                                      dtData, Nothing,
                                      "TemplateImport_Error_" & Format(Date.Now, "yyyyMMdd"))
            Session("EXPORTREPORT") = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub ORG_ACCESS()
        Try
            Dim dtData = Session("EXPORTREPORT")
            ExportTemplate("Attendance\Import\Template_ImportShift_Org_error.xls",
                                      dtData, Nothing,
                                      "TemplateImport_Error_" & Format(Date.Now, "yyyyMMdd"))
            Session("EXPORTREPORT") = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub PA_TARGET_DTTD_LABEL()
        Try
            Dim dtData = Session("EXPORTREPORT")
            ExportTemplate("Payroll\Import\Template_Import_PA_TARGET_DTTD_LABEL_error.xls",
                                      dtData, Nothing,
                                      "TemplateImport_Error_" & Format(Date.Now, "yyyyMMdd"))
            Session("EXPORTREPORT") = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub PA_VEHICLE_NORM()
        Try
            Dim dtData = Session("EXPORTREPORT")
            ExportTemplate("Payroll\Import\Template_Import_TienXeHTCH _error.xls",
                                      dtData, Nothing,
                                      "TemplateImport_Error_" & Format(Date.Now, "yyyyMMdd"))
            Session("EXPORTREPORT") = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub IMPORT_CV_ERROR()
        Try
            Dim dtData = Session("EXPORTREPORT")
            ExportTemplate("Profile\Import\cv_error.xls",
                                      dtData, Nothing,
                                      "TemplateImport_Error_" & Format(Date.Now, "yyyyMMdd"))
            Session("EXPORTREPORT") = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub IMPORT_OT()
        Try
            Dim dtData = Session("EXPORTREPORT")
            ExportTemplate("Attendance\Import\Import_OT_error.xls",
                                      dtData, Nothing,
                                      "TemplateImport_Error_" & Format(Date.Now, "yyyyMMdd"))
            Session("EXPORTREPORT") = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub HU_CONGNO_ERROR()
        Try
            Dim dtData = Session("EXPORTREPORT")
            ExportTemplate("Profile\Import\Congno_error.xls",
                                      dtData, Nothing,
                                      "TemplateImport_Error_" & Format(Date.Now, "yyyyMMdd"))
            Session("EXPORTREPORT") = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Template_SingDefault()
        Try
            'Dim is_disolve = Boolean.Parse(Request.Params("IS_DISSOLVE"))
            Dim rep As New Attendance.AttendanceRepository
            Dim repc As New Common.CommonRepository
            Dim org_id = Decimal.Parse(Request.Params("orgid"))
            Dim obj As New Attendance.AttendanceBusiness.ParamDTO
            Dim dtDataShift As New DataTable
            obj.ORG_ID = org_id
            If Request.Params("IS_DISSOLVE") = "1" Then
                obj.IS_DISSOLVE = True
            End If
            obj.P_EXPORT_TYPE = 3
            If Not String.IsNullOrEmpty(Request.Params("PERIOD_ID")) Then
                obj.PERIOD_ID = Decimal.Parse(Request.Params("PERIOD_ID"))
            End If
            dtDataShift = rep.GetAT_ListShift()
            'check is root
            Dim ListComboData = New ComboBoxDataDTO
            ListComboData.GET_LIST_SHIFT = True
            rep.GetComboboxData(ListComboData)
            Dim list_id_shilf = ListComboData.LIST_LIST_SHIFT.Select(Function(n)
                                                                         Return n.ID
                                                                     End Function).ToList
            Dim listremove = dtDataShift.AsEnumerable.Where(Function(n)
                                                                Return Not list_id_shilf.Contains(n.Field(Of Decimal)("ID"))
                                                            End Function).ToList
            listremove.ForEach(Function(n)
                                   dtDataShift.Rows.Remove(n)
                                   Return True
                               End Function)
            Dim dsData As DataSet = rep.GetDataFromOrg(obj)
            dsData.Tables(0).TableName = "Table"
            If dtDataShift IsNot Nothing Then
                dsData.Tables.Add(dtDataShift)
                dsData.Tables(1).TableName = "Table1"
            End If
            ExportTemplate("Attendance\Import\Import_SingDefault.xlsx",
                                      dsData, Nothing,
                                      "Import_SingDefault" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Import_SingDefault_Error()
        Try
            Dim org_id As Decimal = Request.Params("ORGID")
            Dim is_disolve As Decimal = Request.Params("IS_DISSOLVE")
            Dim dtData = Session("EXPORTREPORT")
            Dim param As New Profile.ProfileBusiness.ParamDTO
            param.ORG_ID = org_id

            param.IS_DISSOLVE = If(is_disolve = 1, True, False)
            Using rep As New Profile.ProfileBusinessRepository

                ExportTemplate("Attendance\Import\Import_SingDefault_Error.xlsx",
                                          dtData, Nothing,
                                          "Import_SingDefault_Error_" & Format(Date.Now, "yyyyMMdd"))
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Template_DeclareTimeRice()
        Try
            'Dim is_disolve = Boolean.Parse(Request.Params("IS_DISSOLVE"))
            Dim rep As New Attendance.AttendanceRepository
            Dim org_id = Decimal.Parse(Request.Params("orgid"))
            Dim obj As New Attendance.AttendanceBusiness.ParamDTO
            'Dim dtDataShift As New DataTable
            obj.ORG_ID = org_id
            obj.IS_DISSOLVE = True
            obj.P_EXPORT_TYPE = 7
            If Not String.IsNullOrEmpty(Request.Params("PERIOD_ID")) Then
                obj.PERIOD_ID = Decimal.Parse(Request.Params("PERIOD_ID"))
            End If
            'dtDataShift = rep.GetAT_ListShift()

            Dim dsData As DataSet = rep.GetDataFromOrg(obj)
            dsData.Tables(0).TableName = "Table"
            'If dtDataShift IsNot Nothing Then
            '    dsData.Tables.Add(dtDataShift)
            'dsData.Tables(1).TableName = "Table1"
            'End If
            ExportTemplate("Attendance\Import\Import_DeclareTimeRice.xlsx",
                                      dsData, Nothing,
                                      "Import_DeclareTimeRice" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Import_DeclareTimeRice_Error()
        Try
            Dim org_id As Decimal = Request.Params("ORGID")
            Dim is_disolve As Decimal = Request.Params("IS_DISSOLVE")
            Dim dtData = Session("EXPORTREPORT")
            Dim param As New Profile.ProfileBusiness.ParamDTO
            param.ORG_ID = org_id

            param.IS_DISSOLVE = If(is_disolve = 1, True, False)
            Using rep As New Profile.ProfileBusinessRepository

                ExportTemplate("Attendance\Import\Import_DeclareTimeRice_Error.xlsx",
                                          dtData, Nothing,
                                          "Import_SingDefault_Error_" & Format(Date.Now, "yyyyMMdd"))
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Template_ImportTimeSheetRice()
        Try
            'Dim is_disolve = Boolean.Parse(Request.Params("IS_DISSOLVE"))
            Dim rep As New Attendance.AttendanceRepository
            Dim org_id = Decimal.Parse(Request.Params("orgid"))
            Dim obj As New Attendance.AttendanceBusiness.ParamDTO
            Dim dtDataPERIOD As New DataTable
            obj.ORG_ID = org_id
            obj.IS_DISSOLVE = True
            obj.P_EXPORT_TYPE = 7
            If Not String.IsNullOrEmpty(Request.Params("PERIOD_ID")) Then
                obj.PERIOD_ID = Decimal.Parse(Request.Params("PERIOD_ID"))
            End If
            dtDataPERIOD = rep.GetAT_PERIOD()

            Dim dsData As DataSet = rep.GetDataFromOrg(obj)
            dsData.Tables(0).TableName = "Table"

            If dtDataPERIOD IsNot Nothing Then
                dsData.Tables.Add(dtDataPERIOD)
                dsData.Tables(1).TableName = "Table1"
            End If

            ExportTemplate("Attendance\Import\Import_TimeSheetRice.xlsx",
                                      dsData, Nothing,
                                      "Import_TimeSheetRice" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Import_TimeSheetRice_Error()
        Try
            Dim org_id As Decimal = Request.Params("ORGID")
            Dim is_disolve As Decimal = Request.Params("IS_DISSOLVE")
            Dim dtData = Session("EXPORTREPORT")
            Dim param As New Profile.ProfileBusiness.ParamDTO
            param.ORG_ID = org_id

            param.IS_DISSOLVE = If(is_disolve = 1, True, False)
            Using rep As New Profile.ProfileBusinessRepository

                ExportTemplate("Attendance\Import\Import_TimeSheetRice_Error.xlsx",
                                          dtData, Nothing,
                                          "Import_TimeSheetRice_Error" & Format(Date.Now, "yyyyMMdd"))
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Template_ImportSwipeData()
        Try
            Dim dsData As DataSet = Session("SWIPE_DATA_EXPORT")
            ExportTemplate("Attendance\Import\Import_SwipeData.xlsx",
                                      dsData, Nothing,
                                      "Import_SwipeData" & Format(Date.Now, "yyyyMMdd"))
            Session.Remove("SWIPE_DATA_EXPORT")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Import_SwipeData_Error()
        Try
            Dim org_id As Decimal = Request.Params("ORGID")
            Dim is_disolve As Decimal = Request.Params("IS_DISSOLVE")
            Dim dtData = Session("EXPORTREPORT")
            Dim param As New Profile.ProfileBusiness.ParamDTO
            param.ORG_ID = org_id

            param.IS_DISSOLVE = If(is_disolve = 1, True, False)
            Using rep As New Profile.ProfileBusinessRepository

                ExportTemplate("Attendance\Import\Import_SwipeData_Error.xlsx",
                                          dtData, Nothing,
                                          "Import_SwipeData_Error" & Format(Date.Now, "yyyyMMdd"))
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub IMPORT_SALARYPLANNING()
        Try
            Dim rep As New Payroll.PayrollRepository
            Dim dsData As New DataSet
            Dim org_id As Decimal = Request.Params("ORG_ID")
            dsData = rep.GetSalaryPlanningImport(org_id)
            dsData.Tables(0).TableName = "ORG"
            dsData.Tables(1).TableName = "TITLE"
            ExportTemplate("Payroll\Import\Template_SALARY_PLANNING.xls",
                                      dsData, Nothing,
                                      "Template_SALARY_PLANNING" & Format(Date.Now, "yyyyMMdd"))

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub IMPORT_SALARYPLANNING_ERROR()
        Try
            Dim dtData = Session("EXPORTREPORT")
            ExportTemplate("Payroll\Import\Template_SALARY_PLANNING_error.xls",
                                      dtData, Nothing,
                                      "Template_SALARY_PLANNING_error" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Template_DeclareEntitlement()
        Try
            Dim rep As New Attendance.AttendanceRepository
            Dim org_id = Decimal.Parse(Request.Params("orgid"))
            Dim obj As New Attendance.AttendanceBusiness.ParamDTO
            obj.ORG_ID = org_id
            obj.IS_DISSOLVE = True
            obj.P_EXPORT_TYPE = 3
            If Not String.IsNullOrEmpty(Request.Params("PERIOD_ID")) Then
                obj.PERIOD_ID = Decimal.Parse(Request.Params("PERIOD_ID"))
            End If
            Dim dsData As DataSet = rep.GetDataFromOrg(obj)
            dsData.Tables(0).TableName = "Table"
            ExportTemplate("Attendance\Import\Template_DeclareEntitlement.xls",
                                      dsData, Nothing,
                                      "Template_DeclareOT" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Template_DeclareEntitlement_error()
        Try
            Dim dtData = Session("EXPORTREPORT")
            ExportTemplate("Attendance\Import\Template_DeclareEntitlement_error.xls",
                                      dtData, Nothing,
                                      "Template_DeclareEntitlement_error" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Import_INS_SUNCARE()
        Try
            Dim rep As New Attendance.AttendanceRepository
            Dim repIns As New Insurance.InsuranceRepository
            Dim org_id = Decimal.Parse(Request.Params("orgid"))
            Dim obj As New Attendance.AttendanceBusiness.ParamDTO
            Dim dtDataCost As New DataTable
            obj.ORG_ID = org_id
            obj.IS_DISSOLVE = True
            obj.P_EXPORT_TYPE = 8
            If Not String.IsNullOrEmpty(Request.Params("PERIOD_ID")) Then
                obj.PERIOD_ID = Decimal.Parse(Request.Params("PERIOD_ID"))
            End If
            Dim dsData As DataSet = rep.GetDataFromOrg(obj)
            dsData.Tables(0).TableName = "EMP"
            dtDataCost = repIns.GetLevelImport()
            dtDataCost.TableName = "Cost"
            dsData.Tables.Add(dtDataCost)

            ExportTemplate("Insurance\Import\Import_INS_SUNCARE.xls",
                                      dsData, Nothing,
                                      "Template_DeclareOT" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Import_INS_SUNCARE_error()
        Try
            Dim dtData = Session("EXPORTREPORT")
            ExportTemplate("Insurance\Import\Import_INS_SUNCARE_error.xlsx",
                                      dtData, Nothing,
                                      "Template_DeclareEntitlement_error" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Template_PA_EmpFormuler()
        Try

            Dim org_id = Decimal.Parse(Request.Params("orgid"))
            Dim IS_DISSOLVE As Decimal
            If Request.Params("IS_DISSOLVE") = "1" Then
                IS_DISSOLVE = 1
            Else
                IS_DISSOLVE = 0
            End If

            Dim rep As New Payroll.PayrollRepository
            Dim dsData As DataSet

            dsData = rep.GET_EXPORT_PA_EMP_FORMULER(org_id, IS_DISSOLVE)

            ExportTemplate("Payroll\Import\TEMPLATE_ImportNhanVien_CongThucLuong.xlsx",
                                      dsData, Nothing,
                                      "ImportNhanVien_CongThucLuong" & Format(Date.Now, "yyyyMMdd"))

            rep.Dispose()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Template_import_Year_Compenstion_Error()
        Try
            Dim dtData = Session("AT_COMPEN_YEAR_ERR")
            Session.Remove("AT_COMPEN_YEAR_ERR")
            ExportTemplate("Attendance\Import\Template_import_Year_Compenstion_Error.xls",
                                      dtData, Nothing,
                                      "Template_import_Year_Compenstion_Error" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Template_import_Year_Conclude_Error()
        Try
            Dim dtData = Session("AT_CONCLUDE_YEAR_ERR")
            Session.Remove("AT_CONCLUDE_YEAR_ERR")
            ExportTemplate("Attendance\Import\Template_import_Year_Conclude_Error.xls",
                                      dtData, Nothing,
                                      "Template_import_Year_Conclude_Error" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Template_Import_Criteria_Error()
        Try
            Dim dtData = Session("CRITERIA_ERR")
            Session.Remove("CRITERIA_ERR")
            ExportTemplate("Performance\Import\Template_Import_Criteria_Error.xls",
                                      dtData, Nothing,
                                      "Template_Import_Criteria_Error" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Template_ThietLap_TDTTT_NhomNV_Error()
        Try
            Dim dtData = Session("PA_SETUP_LDTT_NV_QLCH")
            Session.Remove("PA_SETUP_LDTT_NV_QLCH")
            ExportTemplate("Payroll\Import\Template_ThietLap_TDTTT_NhomNV_Error.xls",
                                      dtData, Nothing,
                                      "Template_ThietLap_TDTTT_NhomNV_Error" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub IMPORT_ERROR_PA_SETUP_INDEX()
        Try
            Dim dtData = Session("IMPORT_ERROR_PA_SETUP_INDEX")
            Session.Remove("IMPORT_ERROR_PA_SETUP_INDEX")
            ExportTemplate("Payroll\Import\Template_ThietLap_TiLeDatCacChiSo_Error.xls",
                                      dtData, Nothing,
                                      "Template_ThietLap_TiLeDatCacChiSo_Error" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub IMPORT_ERROR_PA_SETUP_BONUS_KPI_PRODUCTTYPE()
        Try
            Dim dtData = Session("IMPORT_ERROR_PA_SETUP_BONUS_KPI_PRODUCTTYPE")
            Session.Remove("IMPORT_ERROR_PA_SETUP_BONUS_KPI_PRODUCTTYPE")
            ExportTemplate("Payroll\Import\Template_ThietLap_TLT_TLHTKPI_LoaiSP_Error.xls",
                                      dtData, Nothing,
                                      "Template_ThietLap_TLT_TLHTKPI_LoaiSP_Error" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Template_import_DTCH_TheoNgay_Error()
        Try
            Dim dtData As DataTable = Session("DTTDDAILY_ERR")
            Session.Remove("DTTDDAILY_ERR")
            Dim dtVar As DataTable = dtData.Clone
            If dtData.Rows.Count > 0 Then
                dtVar.ImportRow(dtData.Rows(0))
            End If
            ExportTemplate("Payroll\Import\Template_import_DTCH_TheoNgay_Error.xls",
                                      dtData, dtVar,
                                      "Template_import_DTCH_TheoNgay_Error" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Template_Import_TR_Plan_Error()
        Try
            Dim dtData As DataTable = Session("TR_PLAN_ERR")
            Session.Remove("TR_PLAN_ERR")
            Dim dtVar As DataTable = dtData.Clone
            If dtData.Rows.Count > 0 Then
                dtVar.ImportRow(dtData.Rows(0))
            End If
            ExportTemplate("Training\Import\Template_Import_TR_Plan_Error.xls",
                                      dtData, dtVar,
                                      "Template_Import_TR_Plan_Error" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub Template_Import_TitleCourse_Error()
        Try
            Dim dtData As DataTable = Session("TR_COURSE_ERR")
            Session.Remove("TR_COURSE_ERR")
            Dim dtVar As DataTable = dtData.Clone
            If dtData.Rows.Count > 0 Then
                dtVar.ImportRow(dtData.Rows(0))
            End If
            ExportTemplate("Training\Import\Template_Import_TitleCourse_Error.xls",
                                      dtData, dtVar,
                                      "Template_Import_TitleCourse_Error" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Template_ThietLap_MucThuong_VP_Error()
        Try
            Dim dtData As DataTable = Session("FRAMEWORK_ECD")
            Session.Remove("FRAMEWORK_ECD")
            Dim dtVar As DataTable = dtData.Clone
            If dtData.Rows.Count > 0 Then
                dtVar.ImportRow(dtData.Rows(0))
            End If
            ExportTemplate("Payroll\Business\Template_ThietLap_MucThuong_VP_Error.xls",
                                      dtData, dtVar,
                                      "Template_ThietLap_MucThuong_VP_Error" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TR_Program_Employee_Error()
        Try
            Dim dtData As DataTable = Session("TR_PROGRAM_EMPLOYEE_ERROR")
            Session.Remove("TR_PROGRAM_EMPLOYEE_ERROR")
            Dim dtVar As DataTable = dtData.Clone
            If dtData.Rows.Count > 0 Then
                dtVar.ImportRow(dtData.Rows(0))
            End If
            ExportTemplate("Training\Import\Program_Employee_Error.xlsx",
                                      dtData, dtVar,
                                      "Template_Program_Employee_Error" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub IMPORT_AT_TOXICLEAVE_EMP()
        Try
            Dim dtData As DataTable = Session("IMPORT_AT_TOXICLEAVE_EMP")
            dtData.TableName = "Error"
            Session.Remove("IMPORT_AT_TOXICLEAVE_EMP")
            ExportTemplate("Attendance\Import\IMPORT_AT_TOXICLEAVE_EMP_ERROR.xlsx",
                                      dtData, Nothing,
                                      "IMPORT_AT_TOXICLEAVE_EMP_ERROR_" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

#Region "Common"

    Public Function ExportTemplateWithDataCol(ByVal sReportFileName As String,
                                                    ByVal dtDataValue As DataTable,
                                                    ByVal dtColname As DataTable,
                                                    ByVal filename As String) As Boolean

        Dim filePath As String
        Dim templatefolder As String

        Dim designer As WorkbookDesigner
        Try

            templatefolder = ConfigurationManager.AppSettings("ReportTemplatesFolder")
            filePath = AppDomain.CurrentDomain.BaseDirectory & "\" & templatefolder & "\" & sReportFileName

            dtDataValue.TableName = "DATA"

            If Not File.Exists(filePath) Then
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "javascriptfunction", "goBack()", True)
                Return False
            End If

            designer = New WorkbookDesigner
            designer.Open(filePath)
            Dim cell As Cells = designer.Workbook.Worksheets(0).Cells
            Dim st As New Style
            st.Number = 3
            Dim i As Integer = 6
            For Each dr As DataRow In dtColname.Rows
                cell(1, i).PutValue(dr("COLNAME"))
                cell(2, i).PutValue(dr("COLVAL"))
                cell(3, i).PutValue(dr("COLDATA"))
                cell(3, i).SetStyle(st)
                i += 1
                cell.InsertColumn(i + 1)
            Next
            ' Xoa 2 cot thua cuoi cung
            cell.DeleteColumn(i + 1)
            cell.DeleteColumn(i)

            designer.SetDataSource(dtDataValue)
            designer.Process()
            designer.Workbook.CalculateFormula()
            designer.Workbook.Worksheets(0).AutoFitColumns()
            designer.Workbook.Save(HttpContext.Current.Response, filename & ".xls", ContentDisposition.Attachment, New XlsSaveOptions())

        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    Public Function ExportTemplate(ByVal sReportFileName As String,
                                                    ByVal dtData As DataTable,
                                                    ByVal dtVariable As DataTable,
                                                    ByVal filename As String) As Boolean

        Dim filePath As String
        Dim templatefolder As String

        Dim designer As WorkbookDesigner
        Try

            templatefolder = ConfigurationManager.AppSettings("ReportTemplatesFolder")
            filePath = AppDomain.CurrentDomain.BaseDirectory & "\" & templatefolder & "\" & sReportFileName

            If Not File.Exists(filePath) Then
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "javascriptfunction", "goBack()", True)
                Return False
            End If

            designer = New WorkbookDesigner
            designer.Open(filePath)
            designer.SetDataSource(dtData)

            If dtVariable IsNot Nothing Then
                Dim intCols As Integer = dtVariable.Columns.Count
                For i As Integer = 0 To intCols - 1
                    designer.SetDataSource(dtVariable.Columns(i).ColumnName.ToString(), dtVariable.Rows(0).ItemArray(i).ToString())
                Next
            End If
            designer.Process()
            designer.Workbook.CalculateFormula()
            designer.Workbook.Save(HttpContext.Current.Response, filename & ".xls", ContentDisposition.Attachment, New XlsSaveOptions())

        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    Public Function ExportTemplate(ByVal sReportFileName As String,
                                                    ByVal dsData As DataSet,
                                                    ByVal dtVariable As DataTable,
                                                    ByVal filename As String) As Boolean

        Dim filePath As String
        Dim templatefolder As String

        Dim designer As WorkbookDesigner
        Try

            templatefolder = ConfigurationManager.AppSettings("ReportTemplatesFolder")
            filePath = AppDomain.CurrentDomain.BaseDirectory & templatefolder & "\" & sReportFileName

            If Not File.Exists(filePath) Then
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "javascriptfunction", "goBack()", True)
                Return False
            End If

            designer = New WorkbookDesigner
            designer.Open(filePath)
            designer.SetDataSource(dsData)

            If dtVariable IsNot Nothing Then
                Dim intCols As Integer = dtVariable.Columns.Count
                For i As Integer = 0 To intCols - 1
                    designer.SetDataSource(dtVariable.Columns(i).ColumnName.ToString(), dtVariable.Rows(0).ItemArray(i).ToString())
                Next
            End If
            designer.Process()
            designer.Workbook.CalculateFormula()
            designer.Workbook.Save(HttpContext.Current.Response, filename & ".xls", ContentDisposition.Attachment, New XlsSaveOptions())

        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    Public Function ExportTemplate_XLSX(ByVal sReportFileName As String,
                                                    ByVal dsData As DataSet,
                                                    ByVal dtVariable As DataTable,
                                                    ByVal filename As String) As Boolean

        Dim filePath As String
        Dim templatefolder As String

        Dim designer As WorkbookDesigner
        Try

            templatefolder = ConfigurationManager.AppSettings("ReportTemplatesFolder")
            filePath = AppDomain.CurrentDomain.BaseDirectory & templatefolder & "\" & sReportFileName

            If Not File.Exists(filePath) Then
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "javascriptfunction", "goBack()", True)
                Return False
            End If

            designer = New WorkbookDesigner
            designer.Open(filePath)
            designer.SetDataSource(dsData)

            If dtVariable IsNot Nothing Then
                Dim intCols As Integer = dtVariable.Columns.Count
                For i As Integer = 0 To intCols - 1
                    designer.SetDataSource(dtVariable.Columns(i).ColumnName.ToString(), dtVariable.Rows(0).ItemArray(i).ToString())
                Next
            End If
            designer.Process()
            designer.Workbook.CalculateFormula()
            designer.Workbook.Save(HttpContext.Current.Response, filename & ".xlsx", ContentDisposition.Attachment, New XlsSaveOptions(SaveFormat.Xlsx))

        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function


#End Region

End Class