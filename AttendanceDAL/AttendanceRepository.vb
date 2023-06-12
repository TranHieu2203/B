Imports System.Data.Objects.DataClasses
Imports System.Reflection
Imports Framework.Data
Imports Framework.Data.System.Linq.Dynamic
Imports Framework.Data.SystemConfig

Partial Public Class AttendanceRepository
    Implements IDisposable


    Private _ctx As AttendanceContext
    Private _isAvailable As Boolean

    Public ReadOnly Property Context As AttendanceContext
        Get
            If _ctx Is Nothing Then
                _ctx = New AttendanceContext
                _ctx.ContextOptions.LazyLoadingEnabled = True
            End If
            Return _ctx
        End Get
    End Property

    Public Function CAL_SUMMARY_DATA_INOUT(ByVal Period_id As Decimal) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_AT_LIST.CAL_SUMMARY_DATA_INOUT",
                                         New With {
                                             .P_PERIOD_ID = Period_id,
                                             .P_CUR = cls.OUT_CURSOR})
                Return CBool(dtData(0)(0))
            End Using
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function GetOtherList(ByVal sType As String, ByVal sLang As String, ByVal isBlank As Boolean) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_OTHER_LIST",
                                           New With {.P_TYPE = sType,
                                                     .P_ISBLANK = isBlank,
                                                     .P_LANG = sLang,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function GetProjectList(ByVal isBlank As Boolean) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_ATTENDANCE_LIST.GET_AT_PROJECT_LIST",
                                           New With {.P_ISBLANK = isBlank,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function GetProjectTitleList(ByVal isBlank As Boolean) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_ATTENDANCE_LIST.GET_AT_PROJECT_TITLE_LIST",
                                           New With {.P_ISBLANK = isBlank,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function GetProjectWorkList(ByVal isBlank As Boolean) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_ATTENDANCE_LIST.GET_AT_PROJECT_WORK_LIST",
                                           New With {.P_ISBLANK = isBlank,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function



    Public Function GetComboboxData(ByRef cbxData As ComboBoxDataDTO, Optional ByVal strUSER As String = "ADMIN") As Boolean
        Try
            If cbxData.GET_LIST_ATTENDANCE_CHECKIN_TYPE Then
                cbxData.LIST_LIST_ATTENDANCE_CHECKIN_TYPE = (From p In Context.OT_OTHER_LIST Join t In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals t.ID
                                                             Where p.ACTFLG = "A" And t.CODE = "ATTENDANCE_CHECKIN_TYPE" Order By p.CREATED_DATE Descending
                                                             Select New OT_OTHERLIST_DTO With {
                            .ID = p.ID,
                            .CODE = p.CODE,
                            .NAME_EN = p.NAME_EN,
                            .NAME_VN = p.NAME_VN,
                            .TYPE_ID = p.TYPE_ID}).ToList
            End If
            If cbxData.GET_LIST_TIME_RECORDER Then
                cbxData.LIST_LIST_TIME_RECORDER = (From p In Context.OT_OTHER_LIST Join t In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals t.ID
                                                   Where p.ACTFLG = "A" And t.CODE = "TIME_RECORDER" Order By p.CREATED_DATE Descending
                                                   Select New OT_OTHERLIST_DTO With {
                            .ID = p.ID,
                            .CODE = p.CODE,
                            .NAME_EN = p.NAME_EN,
                            .NAME_VN = p.NAME_VN,
                            .TYPE_ID = p.TYPE_ID}).ToList
            End If
            'GET LOAIJ NGHI BU
            If cbxData.GET_LIST_OFFTIME_TYPE Then
                cbxData.LIST_LIST_OFFTIME = (From p In Context.OT_OTHER_LIST Join t In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals t.ID
                                             Where p.ACTFLG = "A" And t.CODE = "OFFSETTING_TIMEKEEPING" Order By p.CREATED_DATE Descending
                                             Select New OT_OTHERLIST_DTO With {
                             .ID = p.ID,
                             .CODE = p.CODE,
                             .NAME_EN = p.NAME_EN,
                             .NAME_VN = p.NAME_VN
                             }).ToList
            End If
            'Danh sach Loai may TERMINAL_TYPE
            If cbxData.GET_LIST_TERMINAL_TYPE Then
                cbxData.LIST_LIST_TYPETERMINAL = (From p In Context.OT_OTHER_LIST Join t In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals t.ID
                                                  Where p.ACTFLG = "A" And t.CODE = "TERMINAL_TYPE" Order By p.CREATED_DATE Descending
                                                  Select New OT_OTHERLIST_DTO With {
                             .ID = p.ID,
                             .CODE = p.CODE,
                             .NAME_EN = p.NAME_EN,
                             .NAME_VN = p.NAME_VN,
                             .TYPE_ID = p.TYPE_ID}).ToList
            End If
            'Danh sách các đối tượng cư trú
            If cbxData.GET_LIST_TYPEPUNISH Then
                cbxData.LIST_LIST_TYPEPUNISH = (From p In Context.OT_OTHER_LIST Join t In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals t.ID
                                                Where p.ACTFLG = "A" And t.CODE = "TYPE_PUNISH" Order By p.CREATED_DATE Descending
                                                Select New OT_OTHERLIST_DTO With {
                             .ID = p.ID,
                             .CODE = p.CODE,
                             .NAME_EN = p.NAME_EN,
                             .NAME_VN = p.NAME_VN,
                             .TYPE_ID = p.TYPE_ID}).ToList
            End If
            If cbxData.GET_LIST_TYPESHIFT Then
                cbxData.LIST_LIST_TYPESHIFT = (From p In Context.OT_OTHER_LIST Join t In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals t.ID
                                               Where p.ACTFLG = "A" And t.CODE = "TYPE_SHIFT" Order By p.CREATED_DATE Descending
                                               Select New OT_OTHERLIST_DTO With {
                            .ID = p.ID,
                            .CODE = p.CODE,
                            .NAME_EN = p.NAME_EN,
                            .NAME_VN = p.NAME_VN,
                            .TYPE_ID = p.TYPE_ID}).ToList
            End If
            'If cbxData.GET_LIST_SIGN_LEAVE Then
            '    cbxData.LIST_LIST_SIGN_LEAVE = (From p In Context.AT_TIME_MANUAL Where p.ACTFLG = "A" Order By p.NAME
            '                                    Select New AT_TIME_MANUALDTO With {
            '                                       .ID = p.ID,
            '                                       .CODE = p.CODE,
            '                                       .NAME_EN = p.NAME,
            '                                       .NAME_VN = p.NAME}).ToList
            'End If
            If cbxData.GET_LIST_APPLY_LAW Then
                cbxData.LIST_LIST_APPLY_LAW = (From p In Context.AT_GSIGN Where p.ACTFLG = "A" Order By p.NAME_VN Descending
                                               Select New AT_GSIGNDTO With {
                                                   .ID = p.ID,
                                                   .CODE = p.CODE,
                                                   .NAME_VN = "[" & p.CODE & "] " & p.NAME_VN}).ToList
            End If
            If cbxData.GET_LIST_PENALIZEA Then
                cbxData.LIST_LIST_PENALIZEA = (From p In Context.AT_DMVS Where p.ACTFLG = "A" Order By p.NAME_VN Descending
                                               Select New AT_DMVSDTO With {
                                                   .ID = p.ID,
                                                   .CODE = p.CODE,
                                                   .NAME_VN = p.NAME_VN}).ToList
            End If
            If cbxData.GET_LIST_SHIFT Then
                Dim UserID = (From u In Context.SE_USER Where u.USERNAME = strUSER Select u.ID).FirstOrDefault
                Dim lstOrg = (From p In Context.SE_USER_ORG_ACCESS Where p.USER_ID = UserID Select p.ORG_ID).ToList().Distinct.ToList()
                Dim lstID_Access = (From p In Context.AT_SHIFT_ORG_ACCESS
                                    From soa In Context.SE_USER_ORG_ACCESS.Where(Function(f) p.ORG_ID = f.ORG_ID)
                                    From o In Context.HU_ORGANIZATION.Where(Function(f) p.ORG_ID = f.ID)
                                    Where soa.USER_ID = UserID
                                    Select p.AT_SHIFT_ID).Distinct.ToList
                cbxData.LIST_LIST_SHIFT = (From p In Context.AT_SHIFT
                                           Where p.ACTFLG = "A" And (lstID_Access.Contains(p.ID) Or strUSER = "ADMIN" Or p.ORG_ID = -1) Order By p.NAME_VN Descending
                                           Select New AT_SHIFTDTO With {
                                               .ID = p.ID,
                                               .CODE = p.CODE,
                                               .NAME_VN = "[" & p.CODE & "] " & p.NAME_VN}).ToList.Distinct.ToList()
            End If
            If cbxData.GET_LIST_SIGN Then
                cbxData.LIST_LIST_SIGN = (From p In Context.AT_FML Where p.ACTFLG = "A" Order By p.NAME_VN Descending
                                          Select New AT_FMLDTO With {
                                                   .ID = p.ID,
                                                   .CODE = p.CODE,
                                                   .NAME_VN = "[" & p.CODE & "] " & p.NAME_VN}).ToList
            End If
            If cbxData.GET_LIST_TYPEEMPLOYEE Then
                cbxData.LIST_LIST_TYPEEMPLOYEE = (From p In Context.OT_OTHER_LIST Join t In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals t.ID
                                                  Where p.ACTFLG = "A" And t.CODE = "TYPE_EMPLOYEE" Order By p.CREATED_DATE Descending
                                                  Select New OT_OTHERLIST_DTO With {
                            .ID = p.ID,
                            .CODE = p.CODE,
                            .NAME_EN = p.NAME_EN,
                            .NAME_VN = p.NAME_VN,
                            .TYPE_ID = p.TYPE_ID}).ToList
            End If
            If cbxData.GET_LIST_TYPEE_FML Then
                cbxData.LIST_LIST_TYPE_FML = (From p In Context.AT_FML Where p.ACTFLG = "A" Order By p.NAME_VN Descending
                                              Select New AT_FMLDTO With {
                                                   .ID = p.ID,
                                                   .CODE = p.CODE,
                                                   .NAME_VN = "[" & p.CODE & "] " & p.NAME_VN}).ToList
            End If
            If cbxData.GET_LIST_REST_DAY Then
                cbxData.LIST_LIST_REST_DAY = (From p In Context.OT_OTHER_LIST Join t In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals t.ID
                                              Where p.ACTFLG = "A" And t.CODE = "AT_TIMELEAVE" Order By p.CREATED_DATE Descending
                                              Select New OT_OTHERLIST_DTO With {
                             .ID = p.ID,
                             .CODE = p.CODE,
                             .NAME_EN = p.NAME_EN,
                             .NAME_VN = p.NAME_VN,
                             .TYPE_ID = p.TYPE_ID}).ToList
            End If
            If cbxData.GET_LIST_TYPE_DMVS Then
                cbxData.LIST_LIST_TYPE_DMVS = (From p In Context.OT_OTHER_LIST
                                               Where p.ACTFLG = "A" And p.TYPE_CODE = "DTVS" Order By p.CREATED_DATE Descending
                                               Select New OT_OTHERLIST_DTO With {
                              .ID = p.ID,
                              .CODE = p.CODE,
                              .NAME_EN = p.NAME_EN,
                              .NAME_VN = p.NAME_VN,
                              .TYPE_ID = p.TYPE_ID}).ToList
            End If
            If cbxData.GET_LIST_TYPE_INFO Then
                cbxData.LIST_LIST_TYPE_INFO = (From p In Context.OT_OTHER_LIST
                                               Where p.ACTFLG = "A" And p.TYPE_CODE = "LTTDTVS" Order By p.CREATED_DATE Descending
                                               Select New OT_OTHERLIST_DTO With {
                              .ID = p.ID,
                              .CODE = p.CODE,
                              .NAME_EN = p.NAME_EN,
                              .NAME_VN = p.NAME_VN,
                              .TYPE_ID = p.TYPE_ID}).ToList
            End If

            If cbxData.GET_LIST_TYPE_MANUAL_LEAVE_FULL Then
                cbxData.LIST_LIST_TYPE_MANUAL_LEAVE_FULL = (From p In Context.AT_TIME_MANUAL
                                                            From Fd In Context.AT_FML.Where(Function(f) f.CODE.ToUpper.Equals(p.CODE.ToUpper)).DefaultIfEmpty
                                                            From F In Context.AT_FML.Where(Function(f) f.ID = p.MORNING_ID).DefaultIfEmpty
                                                            From F2 In Context.AT_FML.Where(Function(f2) f2.ID = p.AFTERNOON_ID).DefaultIfEmpty
                                                            From F3 In Context.AT_TYPE_PROCESS.Where(Function(f3) f3.ID = p.TYPE_PROSS_ID)
                                                            Where p.ACTFLG = "A" And (F.IS_LEAVE = -1 Or F2.IS_LEAVE = -1) And F3.ID = 1 Order By p.ORDERS Ascending
                                                            Select New AT_TIME_MANUALDTO With {.ID = p.ID,
                                                                                               .CODE = p.CODE,
                                                                                                .CODE1 = F.CODE,
                                                                                                .CODE2 = F2.CODE,
                                                                                               .MORNING_ID = p.MORNING_ID,
                                                                                               .ORDER_W = p.ORDER_W,
                                                                                               .IS_LEAVE = F.IS_NOTIFY,
                                                                                               .NOTE = F.NOTE,
                                                                                               .AFTERNOON_ID = p.AFTERNOON_ID,
                                                                                               .NAME_VN = "[" & p.CODE_KH & "] " & p.NAME,
                                                                                               .IS_REG_SHIFT = p.IS_REG_SHIFT,
                                                                                               .FML_DAYNUM = Fd.DAY_NUM}).ToList
            End If
            If cbxData.GET_LIST_TYPE_MANUAL_LEAVE Then
                cbxData.LIST_LIST_TYPE_MANUAL_LEAVE = (From p In Context.AT_TIME_MANUAL
                                                       From F In Context.AT_FML.Where(Function(f) f.ID = p.MORNING_ID).DefaultIfEmpty
                                                       From F2 In Context.AT_FML.Where(Function(f2) f2.ID = p.AFTERNOON_ID).DefaultIfEmpty
                                                       From F3 In Context.AT_TYPE_PROCESS.Where(Function(f3) f3.ID = p.TYPE_PROSS_ID)
                                                       Where p.ACTFLG = "A" And (F.IS_LEAVE = -1 Or F2.IS_LEAVE = -1) And F3.ID = 1 And p.IS_OTHER <> -1 Order By p.ORDERS Ascending
                                                       Select New AT_TIME_MANUALDTO With {
                                                   .ID = p.ID,
                                                   .CODE = p.CODE,
                                                   .MORNING_ID = p.MORNING_ID,
                                                   .ORDER_W = p.ORDER_W,
                                                   .IS_LEAVE = F.IS_NOTIFY,
                                                   .NOTE = F.NOTE,
                                                   .AFTERNOON_ID = p.AFTERNOON_ID,
                                                   .NAME_VN = "[" & p.CODE_KH & "] " & p.NAME}).ToList
            End If
            If cbxData.GET_LIST_TYPE_MANUAL_LEAVE Then
                cbxData.LIST_LIST_TYPE_MANUAL_LEAVE = (From p In Context.AT_TIME_MANUAL
                                                       From F In Context.AT_FML.Where(Function(f) f.ID = p.MORNING_ID).DefaultIfEmpty
                                                       From F2 In Context.AT_FML.Where(Function(f2) f2.ID = p.AFTERNOON_ID).DefaultIfEmpty
                                                       From F3 In Context.AT_TYPE_PROCESS.Where(Function(f3) f3.ID = p.TYPE_PROSS_ID)
                                                       Where p.ACTFLG = "A" And (F.IS_LEAVE = -1 Or F2.IS_LEAVE = -1) And F3.ID = 1 And p.IS_OTHER <> -1 Order By p.ORDERS Ascending
                                                       Select New AT_TIME_MANUALDTO With {
                                                   .ID = p.ID,
                                                   .CODE = p.CODE,
                                                   .MORNING_ID = p.MORNING_ID,
                                                   .ORDER_W = p.ORDER_W,
                                                   .IS_LEAVE = F.IS_NOTIFY,
                                                   .NOTE = F.NOTE,
                                                   .AFTERNOON_ID = p.AFTERNOON_ID,
                                                   .NAME_VN = "[" & p.CODE_KH & "] " & p.NAME}).ToList
            End If

            If cbxData.GET_LIST_TYPE_MANUAL_LEAVE_CT Then
                cbxData.LIST_LIST_TYPE_MANUAL_LEAVE_CT = (From p In Context.AT_TIME_MANUAL
                                                          From F In Context.AT_FML.Where(Function(f) f.ID = p.MORNING_ID).DefaultIfEmpty
                                                          From F2 In Context.AT_FML.Where(Function(f2) f2.ID = p.AFTERNOON_ID).DefaultIfEmpty
                                                          From F3 In Context.AT_TYPE_PROCESS.Where(Function(f3) f3.ID = p.TYPE_PROSS_ID)
                                                          Where p.ACTFLG = "A" And (F.IS_LEAVE = -1 Or F2.IS_LEAVE = -1) And F3.ID = 1 And p.IS_OTHER = -1 Order By p.ORDERS Ascending
                                                          Select New AT_TIME_MANUALDTO With {
                                                      .ID = p.ID,
                                                      .CODE = p.CODE,
                                                      .MORNING_ID = p.MORNING_ID,
                                                      .ORDER_W = p.ORDER_W,
                                                      .IS_LEAVE = F.IS_NOTIFY,
                                                      .NOTE = F.NOTE,
                                                      .AFTERNOON_ID = p.AFTERNOON_ID,
                                                      .NAME_VN = "[" & p.CODE & "] " & p.NAME}).ToList
            End If

            If cbxData.GET_LIST_TYPE_MANUAL Then
                cbxData.LIST_LIST_TYPE_MANUAL = (From p In Context.AT_TIME_MANUAL Where p.ACTFLG = "A" And p.CODE <> "RVS" And p.CODE <> "RDT" Order By p.NAME Descending
                                                 Select New AT_TIME_MANUALDTO With {
                                                   .ID = p.ID,
                                                   .CODE = p.CODE,
                                                   .NAME_VN = "[" & p.CODE & "] " & p.NAME}).ToList
            End If

            If cbxData.GET_LIST_SHIFT_SUNDAY Then
                cbxData.LIST_LIST_SHIFT_SUNDAY = (From p In Context.AT_TIME_MANUAL Where p.ACTFLG = "A" Order By p.NAME Descending
                                                  Select New AT_TIME_MANUALDTO With {
                                                   .ID = p.ID,
                                                   .CODE = p.CODE,
                                                   .NAME_VN = "[" & p.CODE & "] " & p.NAME}).ToList
            End If
            ' danh mục cấp nhân sự
            If cbxData.GET_LIST_STAFF_RANK Then
                cbxData.LIST_LIST_STAFF_RANK = (From p In Context.HU_STAFF_RANK Where p.ACTFLG = "A" Order By p.NAME Descending
                                                Select New HU_STAFF_RANKDTO With {
                                                   .ID = p.ID,
                                                   .CODE = p.CODE,
                                                   .NAME = p.NAME}).ToList
            End If
            If cbxData.GET_LIST_HS_OT Then
                'cbxData.LIST_LIST_HS_OT = (From p In Context.OT_OTHER_LIST Join t In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals t.ID
                '                           Where p.ACTFLG = "A" And t.CODE = "HS_OT" Order By p.CODE Ascending
                '                           Select New OT_OTHERLIST_DTO With {
                '                                .ID = p.ID,
                '                                .CODE = p.CODE,
                '                                .NAME_EN = p.NAME_EN,
                '                                .NAME_VN = p.NAME_VN,
                '                                .TYPE_ID = p.TYPE_ID}).ToList
                cbxData.LIST_LIST_HS_OT = (From p In Context.AT_COEFF_OVERTIME_EXCEPTION
                                           Where p.ACTFLG = "A" Order By p.EXCEPTION_COEFF Ascending
                                           Select New OT_OTHERLIST_DTO With {
                                                .ID = p.ID,
                                                .CODE = p.EXCEPTION_COEFF_CODE,
                                                .NAME_VN = p.EXCEPTION_COEFF}).ToList
            End If
            If cbxData.GET_LIST_HS_OT_NB Then

                cbxData.LIST_LIST_HS_OT_NB = (From p In Context.AT_COEFF_OVERTIME_EXCEPTION
                                              Where p.ACTFLG = "A" And p.AUTOGEN <> 0 _
                                           And p.HIDE <> -1 And p.IS_BU <> 0 Order By p.EXCEPTION_COEFF Ascending
                                              Select New OT_OTHERLIST_DTO With {
                                                .ID = p.ID,
                                                .CODE = p.EXCEPTION_COEFF_CODE,
                                                .NAME_VN = p.EXCEPTION_COEFF}).ToList
            End If
            If cbxData.GET_LIST_TYPE_OT Then
                cbxData.LIST_LIST_TYPE_OT = (From p In Context.OT_OTHER_LIST Join t In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals t.ID
                                             Where p.ACTFLG = "A" And t.CODE = "TYPE_OT" Order By p.ID Descending
                                             Select New OT_OTHERLIST_DTO With {
                                                .ID = p.ID,
                                                .CODE = p.CODE,
                                                .NAME_EN = p.NAME_EN,
                                                .NAME_VN = p.NAME_VN,
                                                .TYPE_ID = p.TYPE_ID}).ToList
            End If

            If cbxData.GET_LIST_TIME_SHIFT Then
                cbxData.LIST_LIST_TIME_SHIFT = (From p In Context.OT_OTHER_LIST Join t In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals t.ID
                                                Where p.ACTFLG = "A" And t.CODE = "TIME_SHIFT" Order By p.CREATED_DATE Descending
                                                Select New OT_OTHERLIST_DTO With {
                             .ID = p.ID,
                             .CODE = p.CODE,
                             .NAME_EN = p.NAME_EN,
                             .NAME_VN = p.NAME_VN,
                             .TYPE_ID = p.TYPE_ID}).ToList
            End If

            If cbxData.GET_LIST_OT_TYPE Then
                cbxData.LIST_LIST_OT_TYPE = (From p In Context.OT_OTHER_LIST Join t In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals t.ID
                                             Where p.ACTFLG = "A" And t.CODE = "AT_OTTYPE" Order By p.ID Descending
                                             Select New OT_OTHERLIST_DTO With {
                                                .ID = p.ID,
                                                .CODE = p.CODE,
                                                .NAME_EN = p.NAME_EN,
                                                .NAME_VN = p.NAME_VN,
                                                .TYPE_ID = p.TYPE_ID}).ToList
            End If

            ' Get list loại xử lý
            If cbxData.GET_LIST_TYPE_PROCESS Then
                cbxData.LIST_LIST_TYPE_PROCESS = (From p In Context.AT_TYPE_PROCESS Order By p.NAME_VN Descending
                                                  Select New AT_TYPE_PROCESSDTO With {
                                                    .ID = p.ID,
                                                    .NAME_EN = p.NAME_EN,
                                                    .NAME_VN = p.NAME_VN}).ToList
            End If

            If cbxData.GET_LIST_MORNING_RATE Then
                cbxData.LIST_LIST_MORNING_RATE = (From p In Context.AT_TIME_MANUAL_RATE Where p.ACTFLG = "A" Order By p.ID Ascending
                                                  Select New AT_TIME_MANUAL_RATEDTO With {
                                                    .ID = p.ID,
                                                    .VALUE_RATE = p.VALUE_RATE}).ToList
            End If

            If cbxData.GET_LIST_AFTERNOON_RATE Then
                cbxData.LIST_LIST_AFTERNOON_RATE = (From p In Context.AT_TIME_MANUAL_RATE Where p.ACTFLG = "A" Order By p.ID Ascending
                                                    Select New AT_TIME_MANUAL_RATEDTO With {
                                                      .ID = p.ID,
                                                      .VALUE_RATE = p.VALUE_RATE}).ToList
            End If
            If cbxData.GET_LIST_AT_REASON Then
                cbxData.LIST_LIST_AT_REASON = (From p In Context.AT_REASON_LIST Where p.STATUS = 1 Order By p.ORDER_NUM Ascending
                                               Select New AT_REASON_LIST_DTO With {
                                                      .ID = p.ID,
                                                      .NAME = p.NAME,
                                                      .TYPE_ID = p.TYPE_ID}).ToList
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".GetComboboxData")
            Return False
        Finally

        End Try
    End Function


    Public Function GetApproveUsers(ByVal employeeId As Decimal, ByVal processCode As String,
                                    Optional ByVal lstOrg As List(Of Decimal) = Nothing,
                                    Optional ByVal isTimesheet As Boolean = False) As List(Of ApproveUserDTO)
        Try
            Dim listResult As New List(Of ApproveUserDTO)

            Dim process = Context.SE_APP_PROCESS.SingleOrDefault(Function(p) p.PROCESS_CODE = processCode)

            If process Is Nothing Then
                Throw New Exception("Chưa thiết lập quy trình phê duyệt HOẶC Mã quy trình phê duyệt sai.")
            End If

            'Lấy template phê duyệt đang áp dụng cho nhân viên
            Dim usingSetups As List(Of SE_APP_SETUP) = GetCurrentEmployeeSetup(employeeId, process, lstOrg, isTimesheet)

            If usingSetups.Count > 0 Then
                Dim usingS = From t In usingSetups
                             From d In Context.SE_APP_TEMPLATE.Where(Function(F) F.ID = t.TEMPLATE_ID)
                             From dt In Context.SE_APP_TEMPLATE_DTL.Where(Function(F) F.TEMPLATE_ID = d.ID)

                Dim firstTemplate = usingS.OrderByDescending(Function(p) p.d.TEMPLATE_ORDER).FirstOrDefault()

                'Dim usingTemplateDetail As List(Of SE_APP_TEMPLATE_DTL) = firstTemplate.AT_APP_TEMPLATE.AT_APP_TEMPLATE_DTL.ToList

                'For Each detailSetting As SE_APP_TEMPLATE_DTL In usingTemplateDetail
                '    Dim itemAdd As ApproveUserDTO = Nothing
                '    If detailSetting.APP_TYPE = 0 Then
                '        itemAdd = GetDirectManagerApprove(employeeId, detailSetting.APP_LEVEL)
                '    Else
                '        itemAdd = GetEmployeeApprove(detailSetting.APP_ID, detailSetting.APP_LEVEL)
                '    End If

                '    If itemAdd IsNot Nothing Then
                '        itemAdd.INFORM_DATE = detailSetting.INFORM_DATE
                '        itemAdd.INFORM_EMAIL = detailSetting.INFORM_EMAIL

                '        listResult.Add(itemAdd)
                '    End If
                'Next

                Dim usingTemplateDetail = (From t In usingSetups
                                           From d In Context.SE_APP_TEMPLATE.Where(Function(F) F.ID = t.TEMPLATE_ID)
                                           From dt In Context.SE_APP_TEMPLATE_DTL.Where(Function(F) F.TEMPLATE_ID = d.ID) Select dt)



                For Each detailSetting As SE_APP_TEMPLATE_DTL In usingTemplateDetail
                    Dim itemAdd As ApproveUserDTO = Nothing
                    If detailSetting.APP_TYPE = 0 Then
                        itemAdd = GetDirectManagerApprove(employeeId, detailSetting.APP_LEVEL)
                    Else
                        itemAdd = GetEmployeeApprove(detailSetting.APP_ID, detailSetting.APP_LEVEL)
                    End If

                    If itemAdd IsNot Nothing Then
                        itemAdd.INFORM_DATE = detailSetting.INFORM_DATE
                        itemAdd.INFORM_EMAIL = detailSetting.INFORM_EMAIL

                        listResult.Add(itemAdd)
                    End If
                Next

                If usingTemplateDetail.Count = listResult.Count Then
                    Return listResult
                Else
                    Return Nothing
                End If
            End If

            Return listResult
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
    Private Function GetCurrentEmployeeSetup(ByVal employeeId As Decimal, ByVal process As SE_APP_PROCESS,
                                   Optional ByVal lstOrg As List(Of Decimal) = Nothing,
                                   Optional ByVal isTimesheet As Boolean = False) As List(Of SE_APP_SETUP)

        Dim _setup As New List(Of SE_APP_SETUP)
        Dim setupEmployee = Context.SE_APP_SETUP.SingleOrDefault(Function(p) p.EMPLOYEE_ID = employeeId _
                                                                     AndAlso p.PROCESS_ID = process.ID _
                                                                     AndAlso (p.FROM_DATE <= Date.Now _
                                                                              AndAlso (Not p.TO_DATE.HasValue _
                                                                                       OrElse (p.TO_DATE.HasValue _
                                                                                               AndAlso Date.Now <= p.TO_DATE.Value))))

        If setupEmployee IsNot Nothing Then
            _setup.Add(setupEmployee)
        End If

        Dim setupOrg = GetCurrentEmployeeOrgSetup(employeeId, process, lstOrg, isTimesheet)
        If setupOrg.Count > 0 Then
            _setup.AddRange(setupOrg)
        End If
        '_setup.Add(GetCurrentEmployeeOrgSetup(employeeId, process))
        Return _setup
    End Function

    Private Function GetCurrentEmployeeOrgSetup(ByVal employeeId As Decimal, ByVal process As SE_APP_PROCESS,
                                                Optional ByVal lstOrg As List(Of Decimal) = Nothing,
                                                Optional ByVal isTimesheet As Boolean = False) As List(Of SE_APP_SETUP)
        Try
            Dim _setup As List(Of SE_APP_SETUP) = New List(Of SE_APP_SETUP)
            If isTimesheet Then
                ' lấy thiết lập theo org truyền vào
                If lstOrg.Count = 0 Then
                    Return Nothing
                End If
                Dim lstTemp = (From p In Context.HU_ORGANIZATION Where lstOrg.Contains(p.ID)).ToList

                For Each Org In lstTemp
                    Dim currentOrgSetup = GetCurrentOrgSetup(Org.ID, process)
                    If currentOrgSetup IsNot Nothing Then
                        _setup.Add(currentOrgSetup)
                    End If
                Next

                If lstTemp.Count <> _setup.Count Then
                    Dim isParent As Boolean = False
                    For Each Org In lstTemp
                        While Org.PARENT_ID.HasValue
                            Org = Context.HU_ORGANIZATION.SingleOrDefault(Function(p) p.ID = Org.PARENT_ID.Value)
                            Dim OrgSetup = GetCurrentOrgSetup(Org.ID, process)
                            If OrgSetup IsNot Nothing Then
                                isParent = True
                                _setup.Add(OrgSetup)
                            End If
                        End While
                    Next
                    If isParent Then
                        Return _setup
                    Else
                        Return New List(Of SE_APP_SETUP)
                    End If
                Else
                    For Each Org In lstTemp
                        While Org.PARENT_ID.HasValue
                            Org = Context.HU_ORGANIZATION.SingleOrDefault(Function(p) p.ID = Org.PARENT_ID.Value)
                            Dim OrgSetup = GetCurrentOrgSetup(Org.ID, process)
                            If OrgSetup IsNot Nothing Then
                                _setup.Add(OrgSetup)
                            End If
                        End While
                    Next
                End If
            Else
                ' lấy ORG hiện tại của nhân viên (lấy trong HU_WORKING)
                Dim currentWorking = (From T In Context.HU_ORGANIZATION
                                      From W In Context.HU_WORKING.Where(Function(p) p.EMPLOYEE_ID = employeeId AndAlso p.EFFECT_DATE <= Date.Now AndAlso (Not p.EXPIRE_DATE.HasValue OrElse (p.EXPIRE_DATE.HasValue AndAlso Date.Now <= p.EXPIRE_DATE.Value))) Order By W.EFFECT_DATE Select T, W).ToList()

                If (currentWorking IsNot Nothing) Then
                    Dim OrgID = (From p In currentWorking Select p).FirstOrDefault.T.ID
                    Dim OrgPA = (From p In currentWorking Select p).FirstOrDefault.T.PARENT_ID

                    Dim currentOrgSetup = GetCurrentOrgSetup(OrgID, process)

                    If currentOrgSetup IsNot Nothing Then
                        _setup.Add(currentOrgSetup)
                    End If

                    While OrgPA.HasValue
                        OrgID = Context.HU_ORGANIZATION.SingleOrDefault(Function(p) p.ID = OrgPA).ID
                        OrgPA = Context.HU_ORGANIZATION.SingleOrDefault(Function(p) p.ID = OrgPA).PARENT_ID
                        Dim OrgSetup = GetCurrentOrgSetup(OrgID, process)
                        If OrgSetup IsNot Nothing Then
                            _setup.Add(OrgSetup)
                        End If
                    End While

                    '_setup = currentOrgSetup
                End If
            End If
            Return _setup
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
    Private Function GetCurrentOrgSetup(ByVal orgId As Decimal, ByVal process As SE_APP_PROCESS) As SE_APP_SETUP
        Try
            Dim setupOrg = Context.SE_APP_SETUP.SingleOrDefault(Function(p) p.ORG_ID.HasValue _
                                                                AndAlso p.ORG_ID = orgId _
                                                                AndAlso p.PROCESS_ID = process.ID _
                                                                AndAlso (p.FROM_DATE <= Date.Now _
                                                                         AndAlso (Not p.TO_DATE.HasValue _
                                                                                  OrElse (p.TO_DATE.HasValue _
                                                                                          AndAlso Date.Now <= p.TO_DATE.Value))))

            'Nếu có thiết lập riêng cho nhân viên
            Return setupOrg
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
    Private Function GetEmployeeApprove(ByVal employeeId As Decimal, ByVal level As Integer) As ApproveUserDTO

        Dim approveUser = (From p In Context.HU_EMPLOYEE_CV
                           From e In Context.HU_EMPLOYEE.Where(Function(F) F.ID = p.EMPLOYEE_ID).DefaultIfEmpty
                           Where p.EMPLOYEE_ID = employeeId
                           Select New ApproveUserDTO With {.EMPLOYEE_ID = employeeId,
                                                           .EMPLOYEE_NAME = e.FULLNAME_VN,
                                                           .EMAIL = p.WORK_EMAIL,
                                                           .LEVEL = level
                                                          }).SingleOrDefault()

        Return approveUser

    End Function
    Private Function GetDirectManagerApprove(ByVal employeeId As Decimal, ByVal level As Integer) As ApproveUserDTO
        Try
            Dim employee = Context.HU_EMPLOYEE.SingleOrDefault(Function(p) p.ID = employeeId)
            If employee.DIRECT_MANAGER.HasValue Then
                Dim approveUser = (From cv In Context.HU_EMPLOYEE_CV Where cv.EMPLOYEE_ID = employee.DIRECT_MANAGER.Value Select cv).FirstOrDefault

                If approveUser IsNot Nothing Then
                    Return New ApproveUserDTO With {
                        .EMPLOYEE_ID = approveUser.EMPLOYEE_ID,
                        .EMPLOYEE_NAME = employee.FULLNAME_VN,
                        .EMAIL = approveUser.WORK_EMAIL,
                        .LEVEL = level
                    }
                Else
                    Return Nothing
                End If
            Else
                Return Nothing
            End If
            Return Nothing
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Return Nothing
        End Try
    End Function

    Public Function GetConfig(ByVal eModule As ModuleID) As Dictionary(Of String, String)
        Using config As New SystemConfig
            Try
                Return config.GetConfig(eModule)
            Catch ex As Exception
                WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
                Throw ex
            End Try
        End Using
    End Function

    Private Function GetObjectById(Of T As EntityObject)(ByVal id As Decimal) As T
        Dim containerName As String = Context.DefaultContainerName
        Dim setName As String = Context.CreateObjectSet(Of T).EntitySet.Name
        ' Build entity key
        Dim entityKey = New EntityKey(containerName & "." & setName, "ID", id)
        Return DirectCast(Context.GetObjectByKey(entityKey), EntityObject)
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls
    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

#Region "STORE PROCEDURE"
    Public Function GET_MANUAL_BY_ID(ByVal id As Decimal) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_AT_LIST.GET_MANUAL_BY_ID",
                                           New With {.P_ID = id,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
    Public Function GET_INFO_PHEPNAM(ByVal id As Decimal, ByVal fromDate As Date) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_AT_LIST.GET_INFO_PHEPNAM",
                                           New With {.P_EMPLOYEE_ID = id,
                                                     .P_FROM_DATE_TIME = fromDate,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
    Public Function GET_INFO_NGHIBU(ByVal id As Decimal, ByVal fromDate As Date) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_AT_LIST.GET_INFO_NGHIBU",
                                           New With {.P_EMPLOYEE_ID = id,
                                                     .P_FROM_DATE_TIME = fromDate,
                                                     .P_CUR = cls.OUT_CURSOR,
                                                     .P_CUR1 = cls.OUT_CURSOR})


                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
    Public Function GET_INFO_PHEPNAM_IMPORT_CTT(ByVal id As Decimal, ByVal fromDate As Date) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_AT_LIST.GET_INFO_PHEPNAM_IMPORT_CTT",
                                           New With {.P_EMPLOYEE_ID = id,
                                                     .P_FROM_DATE_TIME = fromDate,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
#End Region
#Region "Phe duyet don nghi phep"
    Public Function CheckOverMonth(ByVal employeeId As Decimal, ByVal orgId As Decimal _
                                                                , ByVal FromDate As DateTime,
                                                                 ByVal ToDate As DateTime) As Decimal
        Dim result As Decimal = -1
        Using cls As New DataAccess.QueryData
            Dim obj = New With {
                                    .P_EMPID = employeeId,
                                    .P_FROMDATE = FromDate,
                                    .P_TODATE = ToDate,
                                    .P_ORG_ID = orgId,
                                    .P_OUT = cls.OUT_NUMBER
                                }
            cls.ExecuteStore("PKG_AT.CHECK_ABSENT_LIMT", obj)
            result = CDec(obj.P_OUT)
        End Using

        Return result
    End Function
#End Region
End Class
