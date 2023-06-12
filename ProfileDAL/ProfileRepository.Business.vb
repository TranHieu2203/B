Imports System.Configuration
Imports System.IO
Imports System.Reflection
Imports System.Web
Imports Framework.Data
Imports Framework.Data.System.Linq.Dynamic
Imports System.Data.Objects
'Imports Aspose.Cells

Partial Class ProfileRepository

#Region "calculator salary "
    Public Function Calculator_Salary(ByVal data_in As String) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PA_BUSINESS.CALCULATOR_SALARY",
                                                    New With {.P_DATA_IN = data_in,
                                                                .CUR = cls.OUT_CURSOR})
                Return dtData
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "EmployeeCriteriaRecord"
    Public Function EmployeeCriteriaRecord(ByVal _filter As EmployeeCriteriaRecordDTO,
                                              ByVal PageIndex As Integer,
                                              ByVal PageSize As Integer,
                                              ByRef Total As Integer, ByVal _param As ParamDTO,
                                              Optional ByVal Sorts As String = "EMPLOYEE_CODE desc",
                                              Optional ByVal log As UserLog = Nothing) As List(Of EmployeeCriteriaRecordDTO)

        Try
            'Dim result As List(Of EmployeeCriteriaRecordDTO)
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using

            Dim query = From Competency In Context.HU_COMPETENCY
                        From stand In Context.HU_COMPETENCY_STANDARD.Where(Function(f) f.COMPETENCY_ID = Competency.ID).DefaultIfEmpty
                        From Competencygroup In Context.HU_COMPETENCY_GROUP.Where(Function(f) f.ID = Competency.COMPETENCY_GROUP_ID)
                        From ass In Context.HU_COMPETENCY_ASS.Where(Function(f) stand.TITLE_ID = f.TITLE_ID).DefaultIfEmpty
                        From p In Context.HU_COMPETENCY_ASSDTL.Where(Function(f) f.COMPETENCY_ASS_ID = ass.ID And
                                                                         f.COMPETENCY_ID = stand.COMPETENCY_ID).DefaultIfEmpty
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = ass.EMPLOYEE_ID).DefaultIfEmpty
                        From title In Context.HU_TITLE.Where(Function(f) f.ID = stand.TITLE_ID And f.ID = e.TITLE_ID)
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = e.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)

            Dim lst = query.Select(Function(p) New EmployeeCriteriaRecordDTO With {
                                        .EMPLOYEE_ID = p.e.ID,
                                        .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                        .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                                        .ORG_ID = p.e.ORG_ID,
                                        .ORG_NAME = p.org.NAME_VN,
                                        .TITLE_ID = p.e.TITLE_ID,
                                        .TITLE_NAME = p.title.NAME_VN,
                                        .COMPETENCY_GROUP_ID = p.Competencygroup.ID,
                                        .COMPETENCY_GROUP_NAME = p.Competencygroup.NAME,
                                        .COMPETENCY_ID = p.Competency.ID,
                                        .COMPETENCY_NAME = p.Competency.NAME,
                                        .LEVEL_NUMBER = p.p.LEVEL_NUMBER
                                        })

            'Lọc theo nhom nang luc
            If _filter.COMPETENCY_GROUP_ID.ToString() <> "" Then
                lst = lst.Where(Function(p) p.COMPETENCY_GROUP_ID = _filter.COMPETENCY_GROUP_ID)
            End If
            'Lọc theo muc
            If (_filter.LEVEL_NUMBER IsNot Nothing) Then
                lst = lst.Where(Function(p) p.LEVEL_NUMBER >= _filter.LEVEL_NUMBER)
            End If
            'Lọc theo nang luc
            'If _filter.COMPETENCY_ID.ToString() <> "" Then
            '    lst = lst.Where(Function(p) p.COMPETENCY_ID = _filter.COMPETENCY_ID)
            'End If
            'p => p.Genres.Any(x => listOfGenres.Contains(x)
            '(p.WORK_STATUS.HasValue And p.WORK_STATUS = 257 And (p.TER_EFFECT_DATE <= dateNow Or p.TER_LAST_DATE <= dateNow))

            If _filter.LST_COMPETENCY_ID.Count > 0 Then
                Dim result = From lstResult In lst
                             From filter In _filter.LST_COMPETENCY_ID.Where(Function(f) f = lstResult.COMPETENCY_ID)
                Dim lst_result = result.Select(Function(p) New EmployeeCriteriaRecordDTO With {
                                       .EMPLOYEE_ID = p.lstResult.EMPLOYEE_ID,
                                       .EMPLOYEE_CODE = p.lstResult.EMPLOYEE_CODE,
                                       .EMPLOYEE_NAME = p.lstResult.EMPLOYEE_NAME,
                                       .ORG_ID = p.lstResult.ORG_ID,
                                       .ORG_NAME = p.lstResult.ORG_NAME,
                                       .TITLE_ID = p.lstResult.TITLE_ID,
                                       .TITLE_NAME = p.lstResult.TITLE_NAME,
                                       .COMPETENCY_GROUP_ID = p.lstResult.COMPETENCY_GROUP_ID,
                                       .COMPETENCY_GROUP_NAME = p.lstResult.COMPETENCY_GROUP_NAME,
                                       .COMPETENCY_ID = p.lstResult.COMPETENCY_ID,
                                       .COMPETENCY_NAME = p.lstResult.COMPETENCY_NAME,
                                       .LEVEL_NUMBER = p.lstResult.LEVEL_NUMBER
                                       })
                lst_result = lst_result.OrderBy(Sorts)
                Total = lst_result.Count
                lst_result = lst_result.Skip(PageIndex * PageSize).Take(PageSize)

                Dim lstData = lst_result.ToList
                For Each item In lstData
                    If item.LEVEL_NUMBER IsNot Nothing Then
                        item.LEVEL_NUMBER_ASS_NAME = item.LEVEL_NUMBER.Value.ToString & "/4"
                    End If
                Next
                Return lstData
            Else
                lst = lst.OrderBy(Sorts)
                Total = lst.Count
                lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

                Dim lstData = lst.ToList
                For Each item In lstData
                    If item.LEVEL_NUMBER IsNot Nothing Then
                        item.LEVEL_NUMBER_ASS_NAME = item.LEVEL_NUMBER.Value.ToString & "/4"
                    End If
                Next
                Return lstData
            End If
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

#End Region
    Public Function GetBCCCImport() As DataSet
        Try
            Using cls As New DataAccess.QueryData
                Dim dsdata As DataSet = cls.ExecuteStore("PKG_PROFILE_INTEGRATED.GET_BCCC_IMPORT",
                                                         New With {.P_CUR = cls.OUT_CURSOR,
                                                                   .P_CUR1 = cls.OUT_CURSOR,
                                                                   .P_CUR2 = cls.OUT_CURSOR,
                                                                   .P_CUR3 = cls.OUT_CURSOR,
                                                                   .P_CUR4 = cls.OUT_CURSOR}, False)
                Return dsdata
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function GetQTCTImport() As DataSet
        Try
            Using cls As New DataAccess.QueryData
                Dim dsdata As DataSet = cls.ExecuteStore("PKG_PROFILE_INTEGRATED.GET_QTCT_IMPORT",
                                                         New With {.P_CUR = cls.OUT_CURSOR,
                                                                   .P_CUR1 = cls.OUT_CURSOR,
                                                                   .P_CUR2 = cls.OUT_CURSOR,
                                                                   .P_CUR3 = cls.OUT_CURSOR,
                                                                   .P_CUR4 = cls.OUT_CURSOR,
                                                                   .P_CUR5 = cls.OUT_CURSOR,
                                                                   .P_CUR6 = cls.OUT_CURSOR}, False)
                Return dsdata
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function GetHopdongImport() As DataSet
        Try
            Using cls As New DataAccess.QueryData
                Dim dsdata As DataSet = cls.ExecuteStore("PKG_PROFILE_INTEGRATED.GET_HOPDONG_IMPORT",
                                                         New With {.P_CUR = cls.OUT_CURSOR,
                                                                   .P_CUR1 = cls.OUT_CURSOR,
                                                                   .P_CUR2 = cls.OUT_CURSOR,
                                                                   .P_CUR3 = cls.OUT_CURSOR,
                                                                   .P_CUR4 = cls.OUT_CURSOR,
                                                                   .P_CUR5 = cls.OUT_CURSOR}, False)
                Return dsdata
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetHoSoLuongImport() As DataSet
        Try
            Using cls As New DataAccess.QueryData
                Dim dsdata As DataSet = cls.ExecuteStore("PKG_PROFILE_INTEGRATED.GET_HOSOLUONG_IMPORT",
                                                         New With {.P_CUR = cls.OUT_CURSOR,
                                                                   .P_CUR1 = cls.OUT_CURSOR,
                                                                   .P_CUR2 = cls.OUT_CURSOR,
                                                                   .P_CUR3 = cls.OUT_CURSOR,
                                                                   .P_CUR4 = cls.OUT_CURSOR,
                                                                   .P_CUR5 = cls.OUT_CURSOR}, False)
                Return dsdata
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

#Region "Quản lý công nợ"

    Public Function GetDebtMng(ByVal _filter As DebtDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal log As UserLog = Nothing,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of DebtDTO)
        Try

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _filter.param.ORG_ID,
                                           .P_ISDISSOLVE = _filter.param.IS_DISSOLVE})
            End Using

            ' lấy toàn bộ dữ liệu theo Org
            Dim query = From d In Context.HU_DEBT
                        From e In Context.HU_EMPLOYEE.Where(Function(e) e.ID = d.EMPLOYEE_ID)
                        From o In Context.HU_ORGANIZATION.Where(Function(o) o.ID = e.ORG_ID)
                        From t In Context.HU_TITLE.Where(Function(t) t.ID = e.TITLE_ID)
                        From ot In Context.OT_OTHER_LIST.Where(Function(f) f.ID = d.DEBT_TYPE_ID).DefaultIfEmpty
                        From org In Context.SE_CHOSEN_ORG.Where(Function(org) org.ORG_ID = o.ID And
                                                                    org.USERNAME = log.Username.ToUpper)
                        Select New DebtDTO With {.ID = d.ID,
                                                 .CREATED_BY = d.CREATED_BY,
                                                 .CREATED_DATE = d.CREATED_DATE,
                                                 .CREATED_LOG = d.CREATED_LOG,
                                                 .MODIFIED_BY = d.MODIFIED_BY,
                                                 .MODIFIED_DATE = d.MODIFIED_DATE,
                                                 .MODIFIED_LOG = d.MODIFIED_LOG,
                                                 .EMPLOYEE_ID = d.EMPLOYEE_ID,
                                                 .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                                                 .EMPLOYEE_NAME = e.FULLNAME_VN,
                                                 .ORG_ID = o.ID,
                                                 .ORG_NAME = o.NAME_VN,
                                                 .TITLE_NAME = t.NAME_VN,
                                                 .ATTACH_FILE = d.ATTACH_FILE,
                                                 .DEBT_TYPE_ID = d.DEBT_TYPE_ID,
                                                 .DEBT_TYPE_NAME = ot.NAME_VN,
                                                 .DATE_DEBIT = d.DATE_DEBIT,
                                                 .SALARY_PERIOD_NAME = d.PERIOD_TEXT,
                                                 .REMARK = d.REMARK,
                                                 .DEDUCT_SALARY = d.DEDUCT_SALARY,
                                                 .PAID = d.PAID,
                                                 .PAYBACK = d.PAYBACK,
                                                 .MONEY = d.MONEY,
                                                 .WORK_STATUS = e.WORK_STATUS,
                                                 .TER_EFFECT_DATE = e.TER_EFFECT_DATE,
                                                 .NOTE = d.NOTE}
            Dim dateNow = Date.Now.Date
            Dim terID = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID
            If Not _filter.IS_TERMINATE Then
                query = query.Where(Function(p) Not p.WORK_STATUS.HasValue Or
                                        (p.WORK_STATUS.HasValue And
                                         ((p.WORK_STATUS <> terID) Or (p.WORK_STATUS = terID And p.TER_EFFECT_DATE > dateNow))))

            End If
            If _filter.EMPLOYEE_CODE IsNot Nothing Then
                query = query.Where(Function(p) p.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If

            If _filter.EMPLOYEE_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.EMPLOYEE_NAME.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If
            If _filter.TITLE_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.TITLE_NAME.ToUpper.Contains(_filter.TITLE_NAME.ToUpper))
            End If
            If _filter.ORG_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If
            If _filter.FROM_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.DATE_DEBIT >= _filter.FROM_DATE)
            End If
            If _filter.TO_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.DATE_DEBIT <= _filter.TO_DATE)
            End If

            Dim lst = query

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

#End Region

#Region "Bằng cấp chứng chỉ"
    Public Function GetCertificates(ByVal _filter As HU_PRO_TRAIN_OUT_COMPANYDTO, ByVal _param As ParamDTO,
                                    ByRef PageIndex As Integer,
                                    ByVal PageSize As Integer,
                                    ByRef Total As Integer,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc",
                                    Optional ByVal log As UserLog = Nothing) As List(Of HU_PRO_TRAIN_OUT_COMPANYDTO)
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using

            Dim query = From p In Context.HU_PRO_TRAIN_OUT_COMPANY
                        From o1 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.MAJOR).DefaultIfEmpty
                        From o2 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.GRADUATE_SCHOOL).DefaultIfEmpty
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From title In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From ot In Context.OT_OTHER_LIST.Where(Function(F) F.ID = p.FORM_TRAIN_ID).DefaultIfEmpty
                        From ott In Context.OT_OTHER_LIST.Where(Function(F) F.ID = p.TYPE_TRAIN_ID).DefaultIfEmpty
                        From ot1 In Context.OT_OTHER_LIST.Where(Function(F) F.ID = p.CERTIFICATE).DefaultIfEmpty
                        From ot2 In Context.OT_OTHER_LIST.Where(Function(F) F.ID = p.CERTIFICATE_GROUP_ID).DefaultIfEmpty
                        From ot3 In Context.OT_OTHER_LIST.Where(Function(F) F.ID = p.CERTIFICATE_TYPE_ID).DefaultIfEmpty
                        From ot_train In Context.OT_OTHER_LIST_TYPE.Where(Function(f) f.ID = ot.TYPE_ID And f.CODE = "TRAINING_FORM").DefaultIfEmpty
                        From ot_type In Context.OT_OTHER_LIST_TYPE.Where(Function(f) f.ID = ott.TYPE_ID And f.CODE = "TRAINING_TYPE").DefaultIfEmpty
                        From ot_level In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.LEVEL_ID And f.TYPE_CODE = "LEARNING_LEVEL").DefaultIfEmpty
                        From tp In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TRAIN_PLACE).DefaultIfEmpty
                        From file In Context.HU_USERFILES.Where(Function(f) f.NAME = p.FILE_NAME).DefaultIfEmpty
                        From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = e.ORG_ID And
                                                           f.USERNAME = log.Username.ToUpper)



            Dim lst = query.Select(Function(p) New HU_PRO_TRAIN_OUT_COMPANYDTO With {
                                       .ID = p.p.ID,
                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                                       .ORG_ID = p.e.ORG_ID,
                                       .ORG_NAME = p.org.NAME_VN,
                                       .ORG_DESC = p.org.DESCRIPTION_PATH,
                                       .TITLE_NAME = p.title.NAME_VN,
                                       .TITLE_ID = p.e.TITLE_ID,
                                       .FROM_DATE = p.p.FROM_DATE,
                                       .TO_DATE = p.p.TO_DATE,
                                       .FROM_DATE_1 = If(p.p.FROM_DATE.Value.Month < 10, "0" & p.p.FROM_DATE.Value.Month & "/" & p.p.FROM_DATE.Value.Year, "" & p.p.FROM_DATE.Value.Month & "/" & p.p.FROM_DATE.Value.Year),
                                       .TO_DATE_1 = If(p.p.TO_DATE.Value.Month < 10, "0" & p.p.TO_DATE.Value.Month & "/" & p.p.TO_DATE.Value.Year, "" & p.p.TO_DATE.Value.Month & "/" & p.p.TO_DATE.Value.Year),
                                       .YEAR_GRA = p.p.YEAR_GRA,
                                       .NAME_SHOOLS = p.p.NAME_SHOOLS,
                                       .FORM_TRAIN_ID = p.p.FORM_TRAIN_ID,
                                       .FORM_TRAIN_NAME = p.ot.NAME_VN,
                                       .UPLOAD_FILE = p.file.NAME,
                                       .FILE_NAME = p.file.FILE_NAME,
                                       .SPECIALIZED_TRAIN = p.p.SPECIALIZED_TRAIN,
                                       .RESULT_TRAIN = p.p.RESULT_TRAIN,
                                       .CERTIFICATE = p.ot1.NAME_VN,
                                       .CERTIFICATE_ID = p.p.CERTIFICATE,
                                       .EFFECTIVE_DATE_FROM = p.p.EFFECTIVE_DATE_FROM,
                                       .EFFECTIVE_DATE_TO = p.p.EFFECTIVE_DATE_TO,
                                       .RECEIVE_DEGREE_DATE = p.p.RECEIVE_DEGREE_DATE,
                                       .CREATED_BY = p.p.CREATED_BY,
                                       .CREATED_DATE = p.p.CREATED_DATE,
                                       .CREATED_LOG = p.p.CREATED_LOG,
                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                       .MODIFIED_LOG = p.p.MODIFIED_LOG,
                                       .IS_RENEWED = p.p.IS_RENEWED,
                                       .RENEWED_NAME = If(p.p.IS_RENEWED = 0, "Không", "Có"),
                                       .LEVEL_ID = p.p.LEVEL_ID,
                                       .LEVEL_NAME = p.ot_level.NAME_VN,
                                       .POINT_LEVEL = p.p.POINT_LEVEL,
                                       .CONTENT_LEVEL = p.p.CONTENT_LEVEL,
                                       .NOTE = p.p.NOTE,
                                       .CERTIFICATE_CODE = p.p.CERTIFICATE_CODE,
                                       .TYPE_TRAIN_NAME = p.p.TYPE_TRAIN_NAME,
                                       .CERTIFICATE_NAME = p.p.CERTIFICATE_NAME,
                                       .TRAIN_PLACE = p.p.TRAIN_PLACE,
                                       .WORK_STATUS = p.e.WORK_STATUS,
                                       .TER_LASTDATE = p.e.TER_LAST_DATE,
                                       .MAJOR_NAME = p.o1.NAME_VN,
                                       .GRADUATE_SCHOOL_NAME = p.o2.NAME_VN,
                                       .IS_MAJOR = If(p.p.IS_MAJOR = -1, True, False),
                                       .IS_MAIN = If(p.p.IS_MAIN = -1, True, False),
                                       .TRAIN_PLACE_NAME = p.tp.NAME_VN,
                                       .CERTIFICATE_GROUP_NAME = p.ot2.NAME_VN,
                                       .CERTIFICATE_TYPE_NAME = p.ot3.NAME_VN})

            If Not _filter.IS_TER Then
                lst = lst.Where(Function(p) Not p.WORK_STATUS.HasValue Or
                                    (p.WORK_STATUS.HasValue And
                                     ((p.WORK_STATUS <> 257) Or (p.WORK_STATUS = 257 And p.TER_LASTDATE < DateTime.Now))))
            End If

            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                lst = lst.Where(Function(p) p.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If

            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_NAME) Then
                lst = lst.Where(Function(p) p.EMPLOYEE_NAME.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If

            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                lst = lst.Where(Function(p) p.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If

            If _filter.EMPLOYEE_ID.HasValue Then
                lst = lst.Where(Function(p) p.EMPLOYEE_ID = _filter.EMPLOYEE_ID)
            End If

            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                lst = lst.Where(Function(p) p.TITLE_NAME.ToUpper.Contains(_filter.TITLE_NAME.ToUpper))
            End If

            'If Not String.IsNullOrEmpty(_filter.FROM_DATE_1) Then
            '    lst = lst.Where(Function(p) p.FROM_DATE.Value.Month = _filter.FROM_DATE_1.ToString("MM"))
            'End If

            'If Not String.IsNullOrEmpty(_filter.TO_DATE_1) Then
            '    lst = lst.Where(Function(p) p.TO_DATE.Value.Month = _filter.TO_DATE_1.ToString("MM"))
            'End If

            If _filter.FROM_DATE.HasValue Then
                lst = lst.Where(Function(p) p.FROM_DATE = _filter.FROM_DATE)
            End If

            If _filter.TO_DATE.HasValue Then
                lst = lst.Where(Function(p) p.TO_DATE = _filter.TO_DATE)
            End If

            If Not String.IsNullOrEmpty(_filter.NAME_SHOOLS) Then
                lst = lst.Where(Function(p) p.NAME_SHOOLS.ToUpper.Contains(_filter.NAME_SHOOLS.ToUpper))
            End If

            If Not String.IsNullOrEmpty(_filter.SPECIALIZED_TRAIN) Then
                lst = lst.Where(Function(p) p.SPECIALIZED_TRAIN.ToUpper.Contains(_filter.SPECIALIZED_TRAIN.ToUpper))
            End If

            If Not String.IsNullOrEmpty(_filter.TYPE_TRAIN_NAME) Then
                lst = lst.Where(Function(p) p.TYPE_TRAIN_NAME.ToUpper.Contains(_filter.TYPE_TRAIN_NAME.ToUpper))
            End If

            If Not String.IsNullOrEmpty(_filter.FORM_TRAIN_NAME) Then
                lst = lst.Where(Function(p) p.FORM_TRAIN_NAME.ToUpper.Contains(_filter.FORM_TRAIN_NAME.ToUpper))
            End If

            If Not String.IsNullOrEmpty(_filter.CERTIFICATE_CODE) Then
                lst = lst.Where(Function(p) p.CERTIFICATE_CODE.ToUpper.Contains(_filter.CERTIFICATE_CODE.ToUpper))
            End If

            If Not String.IsNullOrEmpty(_filter.CERTIFICATE) Then
                lst = lst.Where(Function(p) p.CERTIFICATE.ToUpper.Contains(_filter.CERTIFICATE.ToUpper))
            End If

            If Not String.IsNullOrEmpty(_filter.CERTIFICATE_GROUP_NAME) Then
                lst = lst.Where(Function(p) p.CERTIFICATE_GROUP_NAME.ToUpper.Contains(_filter.CERTIFICATE_GROUP_NAME.ToUpper))
            End If

            If Not String.IsNullOrEmpty(_filter.CERTIFICATE_TYPE_NAME) Then
                lst = lst.Where(Function(p) p.CERTIFICATE_TYPE_NAME.ToUpper.Contains(_filter.CERTIFICATE_TYPE_NAME.ToUpper))
            End If

            If Not String.IsNullOrEmpty(_filter.LEVEL_NAME) Then
                lst = lst.Where(Function(p) p.LEVEL_NAME.ToUpper.Contains(_filter.LEVEL_NAME.ToUpper))
            End If

            If _filter.EFFECTIVE_DATE_FROM.HasValue Then
                lst = lst.Where(Function(p) p.EFFECTIVE_DATE_FROM = _filter.EFFECTIVE_DATE_FROM)
            End If

            If _filter.EFFECTIVE_DATE_TO.HasValue Then
                lst = lst.Where(Function(p) p.EFFECTIVE_DATE_TO = _filter.EFFECTIVE_DATE_TO)
            End If

            If Not String.IsNullOrEmpty(_filter.RENEWED_NAME) Then
                lst = lst.Where(Function(p) p.RENEWED_NAME.ToUpper.Contains(_filter.RENEWED_NAME.ToUpper))
            End If

            If Not String.IsNullOrEmpty(_filter.RESULT_TRAIN) Then
                lst = lst.Where(Function(p) p.RESULT_TRAIN.ToUpper.Contains(_filter.RESULT_TRAIN.ToUpper))
            End If

            If _filter.YEAR_GRA.HasValue Then
                lst = lst.Where(Function(p) p.YEAR_GRA = _filter.YEAR_GRA)
            End If

            If Not String.IsNullOrEmpty(_filter.POINT_LEVEL) Then
                lst = lst.Where(Function(p) p.POINT_LEVEL.ToUpper.Contains(_filter.POINT_LEVEL.ToUpper))
            End If

            If Not String.IsNullOrEmpty(_filter.CONTENT_LEVEL) Then
                lst = lst.Where(Function(p) p.CONTENT_LEVEL.ToUpper.Contains(_filter.CONTENT_LEVEL.ToUpper))
            End If

            If Not String.IsNullOrEmpty(_filter.NOTE) Then
                lst = lst.Where(Function(p) p.NOTE.ToUpper.Contains(_filter.NOTE.ToUpper))
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetCertificateById(ByVal _id As Decimal) As HU_PRO_TRAIN_OUT_COMPANYDTO
        Try

            Dim query = From p In Context.HU_PRO_TRAIN_OUT_COMPANY
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From title In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From ot In Context.OT_OTHER_LIST.Where(Function(F) F.ID = p.FORM_TRAIN_ID).DefaultIfEmpty
                        From ott In Context.OT_OTHER_LIST.Where(Function(F) F.ID = p.TYPE_TRAIN_ID).DefaultIfEmpty
                        From ot1 In Context.OT_OTHER_LIST.Where(Function(F) F.ID = p.CERTIFICATE).DefaultIfEmpty
                        From ot_train In Context.OT_OTHER_LIST_TYPE.Where(Function(f) f.ID = ot.TYPE_ID And f.CODE = "TRAINING_FORM").DefaultIfEmpty
                        From ot_type In Context.OT_OTHER_LIST_TYPE.Where(Function(f) f.ID = ott.TYPE_ID And f.CODE = "TRAINING_TYPE").DefaultIfEmpty
                        From ot_level In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.LEVEL_ID And f.TYPE_CODE = "LEARNING_LEVEL").DefaultIfEmpty
                        From file In Context.HU_USERFILES.Where(Function(F) F.NAME = p.FILE_NAME).DefaultIfEmpty
                        From tp In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TRAIN_PLACE).DefaultIfEmpty
                        Where p.ID = _id

            Dim lst = query.Select(Function(p) New HU_PRO_TRAIN_OUT_COMPANYDTO With {
                                       .ID = p.p.ID,
                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                                       .ORG_ID = p.e.ORG_ID,
                                       .ORG_NAME = p.org.NAME_VN,
                                       .TITLE_NAME = p.title.NAME_VN,
                                       .TITLE_ID = p.e.TITLE_ID,
                                       .FROM_DATE = p.p.FROM_DATE,
                                       .TO_DATE = p.p.TO_DATE,
                                       .YEAR_GRA = p.p.YEAR_GRA,
                                       .NAME_SHOOLS = p.p.NAME_SHOOLS,
                                       .FORM_TRAIN_ID = p.p.FORM_TRAIN_ID,
                                       .FORM_TRAIN_NAME = p.ot.NAME_VN,
                                       .UPLOAD_FILE = p.p.FILE_NAME,
                                       .FILE_NAME = p.file.FILE_NAME,
                                       .SPECIALIZED_TRAIN = p.p.SPECIALIZED_TRAIN,
                                       .RESULT_TRAIN = p.p.RESULT_TRAIN,
                                       .CERTIFICATE = p.ot1.NAME_VN,
                                       .CERTIFICATE_ID = p.p.CERTIFICATE,
                                       .EFFECTIVE_DATE_FROM = p.p.EFFECTIVE_DATE_FROM,
                                       .EFFECTIVE_DATE_TO = p.p.EFFECTIVE_DATE_TO,
                                       .RECEIVE_DEGREE_DATE = p.p.RECEIVE_DEGREE_DATE,
                                       .CREATED_BY = p.p.CREATED_BY,
                                       .CREATED_DATE = p.p.CREATED_DATE,
                                       .CREATED_LOG = p.p.CREATED_LOG,
                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                       .MODIFIED_LOG = p.p.MODIFIED_LOG,
                                       .IS_RENEWED = p.p.IS_RENEWED,
                                       .RENEWED_NAME = If(p.p.IS_RENEWED = 0, "Không", "Có"),
                                       .LEVEL_ID = p.p.LEVEL_ID,
                                       .LEVEL_NAME = p.ot_level.NAME_VN,
                                       .POINT_LEVEL = p.p.POINT_LEVEL,
                                       .CONTENT_LEVEL = p.p.CONTENT_LEVEL,
                                       .NOTE = p.p.NOTE,
                                       .CERTIFICATE_CODE = p.p.CERTIFICATE_CODE,
                                       .TYPE_TRAIN_NAME = p.p.TYPE_TRAIN_NAME,
                                       .CERTIFICATE_NAME = p.p.CERTIFICATE_NAME,
                                       .TRAIN_PLACE = p.p.TRAIN_PLACE,
                                       .WORK_STATUS = p.e.WORK_STATUS,
                                       .TER_LASTDATE = p.e.TER_LAST_DATE,
                                       .IS_MAIN = p.p.IS_MAIN,
                                       .MAJOR = p.p.MAJOR,
                                       .GRADUATE_SCHOOL = p.p.GRADUATE_SCHOOL,
                                       .IS_MAJOR = If(p.p.IS_MAJOR = -1, True, False),
                                       .TRAIN_PLACE_NAME = p.tp.NAME_VN,
                                       .CERTIFICATE_GROUP_ID = p.p.CERTIFICATE_GROUP_ID,
                                       .CERTIFICATE_TYPE_ID = p.p.CERTIFICATE_TYPE_ID}).FirstOrDefault

            Return lst
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

#End Region

#Region "CTNN"

    Public Function GetAbroads(ByVal _filter As HUAbroadDTO, ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer, ByVal _param As ParamDTO,
                                Optional ByVal Sorts As String = "CREATED_DATE desc",
                                Optional ByVal log As UserLog = Nothing) As List(Of HUAbroadDTO)

        Try

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using


            Dim query = From p In Context.HU_ABROAD
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From o In Context.HUV_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From n In Context.HU_NATION.Where(Function(f) f.ID = p.NATION).DefaultIfEmpty
                        From mt In Context.HU_TITLE_BLD.Where(Function(f) f.ID = e.MATHE).DefaultIfEmpty
                        From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = e.ORG_ID And
                                                                       f.USERNAME = log.Username.ToUpper)
                        Select New HUAbroadDTO With {.ID = p.ID,
                                                    .EMPLOYEE_ID = p.EMPLOYEE_ID,
                                                    .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                                                    .EMPLOYEE_NAME = e.FULLNAME_VN,
                                                    .TITLE_ID = e.TITLE_ID,
                                                    .TITLE_NAME = t.NAME_VN,
                                                    .ORG_ID = e.ORG_ID,
                                                    .ORG_NAME = o.NAME_VN,
                                                    .ORG_DESC = o.DESCRIPTION_PATH,
                                                    .MATHE = e.MATHE,
                                                    .MATHE_NAME = mt.NAME_VN,
                                                    .CONTENT = p.CONTENT,
                                                    .FROM_DATE = p.FROM_DATE,
                                                    .TO_DATE = p.TO_DATE,
                                                    .NATION = p.NATION,
                                                    .NATION_NAME = n.NAME_VN,
                                                    .PLACE_NAME = p.PLACE_NAME,
                                                    .ADDRESS = p.ADDRESS,
                                                    .SPONSORS = p.SPONSORS,
                                                    .TOTAL_COST = p.TOTAL_COST,
                                                    .REMARK = p.REMARK,
                                                    .DECISION_NO = p.DECISION_NO,
                                                    .CREATED_DATE = p.CREATED_DATE}

            If _filter.EMPLOYEE_CODE <> "" Then
                query = query.Where(Function(f) f.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If
            If _filter.EMPLOYEE_NAME <> "" Then
                query = query.Where(Function(f) f.EMPLOYEE_NAME.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If
            If _filter.TITLE_NAME <> "" Then
                query = query.Where(Function(f) f.TITLE_NAME.ToUpper.Contains(_filter.TITLE_NAME.ToUpper))
            End If
            If _filter.ORG_NAME <> "" Then
                query = query.Where(Function(f) f.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If
            If _filter.MATHE_NAME <> "" Then
                query = query.Where(Function(f) f.MATHE_NAME.ToUpper.Contains(_filter.MATHE_NAME.ToUpper))
            End If
            If _filter.DECISION_NO <> "" Then
                query = query.Where(Function(f) f.DECISION_NO.ToUpper.Contains(_filter.DECISION_NO.ToUpper))
            End If
            If _filter.CONTENT <> "" Then
                query = query.Where(Function(f) f.CONTENT.ToUpper.Contains(_filter.CONTENT.ToUpper))
            End If
            If _filter.PLACE_NAME <> "" Then
                query = query.Where(Function(f) f.PLACE_NAME.ToUpper.Contains(_filter.PLACE_NAME.ToUpper))
            End If
            If _filter.NATION_NAME <> "" Then
                query = query.Where(Function(f) f.NATION_NAME.ToUpper.Contains(_filter.NATION_NAME.ToUpper))
            End If
            If _filter.SPONSORS <> "" Then
                query = query.Where(Function(f) f.SPONSORS.ToUpper.Contains(_filter.SPONSORS.ToUpper))
            End If
            If _filter.ADDRESS <> "" Then
                query = query.Where(Function(f) f.ADDRESS.ToUpper.Contains(_filter.ADDRESS.ToUpper))
            End If
            If _filter.REMARK <> "" Then
                query = query.Where(Function(f) f.REMARK.ToUpper.Contains(_filter.REMARK.ToUpper))
            End If
            If _filter.FROM_DATE.HasValue Then
                query = query.Where(Function(f) f.FROM_DATE = _filter.FROM_DATE)
            End If
            If _filter.TO_DATE.HasValue Then
                query = query.Where(Function(f) f.TO_DATE = _filter.TO_DATE)
            End If
            If _filter.TOTAL_COST.HasValue Then
                query = query.Where(Function(f) f.TOTAL_COST = _filter.TOTAL_COST)
            End If

            query = query.OrderBy(Sorts)
            Total = query.Count
            query = query.Skip(PageIndex * PageSize).Take(PageSize)

            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function InsertAbroad(ByVal objAbroad As HUAbroadDTO, ByVal log As UserLog) As Boolean
        Dim objAbroadData As New HU_ABROAD
        Try
            objAbroadData.ID = Utilities.GetNextSequence(Context, Context.HU_ABROAD.EntitySet.Name)
            objAbroadData.EMPLOYEE_ID = objAbroad.EMPLOYEE_ID
            objAbroadData.FROM_DATE = objAbroad.FROM_DATE
            objAbroadData.TO_DATE = objAbroad.TO_DATE
            objAbroadData.CONTENT = objAbroad.CONTENT
            objAbroadData.NATION = objAbroad.NATION
            objAbroadData.REMARK = objAbroad.REMARK
            objAbroadData.PLACE_NAME = objAbroad.PLACE_NAME
            objAbroadData.ADDRESS = objAbroad.ADDRESS
            objAbroadData.SPONSORS = objAbroad.SPONSORS
            objAbroadData.TOTAL_COST = objAbroad.TOTAL_COST
            objAbroadData.DECISION_NO = objAbroad.DECISION_NO
            Context.HU_ABROAD.AddObject(objAbroadData)
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Return False
        End Try

    End Function

    Public Function ModifyAbroad(ByVal objAbroad As HUAbroadDTO, ByVal log As UserLog) As Boolean
        Dim objAbroadData As New HU_ABROAD
        Try
            objAbroadData = (From p In Context.HU_ABROAD Where p.ID = objAbroad.ID).FirstOrDefault
            objAbroadData.EMPLOYEE_ID = objAbroad.EMPLOYEE_ID
            objAbroadData.FROM_DATE = objAbroad.FROM_DATE
            objAbroadData.TO_DATE = objAbroad.TO_DATE
            objAbroadData.CONTENT = objAbroad.CONTENT
            objAbroadData.NATION = objAbroad.NATION
            objAbroadData.REMARK = objAbroad.REMARK
            objAbroadData.PLACE_NAME = objAbroad.PLACE_NAME
            objAbroadData.ADDRESS = objAbroad.ADDRESS
            objAbroadData.SPONSORS = objAbroad.SPONSORS
            objAbroadData.TOTAL_COST = objAbroad.TOTAL_COST
            objAbroadData.DECISION_NO = objAbroad.DECISION_NO
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Return False
        End Try

    End Function

    Public Function ValidateAbroad(ByVal objAbroad As HUAbroadDTO) As Boolean
        Try
            'HUNGTN === 20/12/2022 === BCG-902
            Dim employeeCode = (From e In Context.HU_EMPLOYEE Where e.ID = objAbroad.EMPLOYEE_ID Select e.EMPLOYEE_CODE).FirstOrDefault

            Dim query = From p In Context.HU_ABROAD
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        Where p.ID <> objAbroad.ID AndAlso e.EMPLOYEE_CODE = employeeCode _
                        AndAlso ((objAbroad.FROM_DATE >= p.FROM_DATE AndAlso objAbroad.FROM_DATE <= p.TO_DATE) _
                                 OrElse (objAbroad.TO_DATE >= p.FROM_DATE AndAlso objAbroad.TO_DATE <= p.TO_DATE) _
                                 OrElse (p.FROM_DATE >= objAbroad.FROM_DATE AndAlso p.FROM_DATE <= objAbroad.TO_DATE) _
                                 OrElse (p.TO_DATE >= objAbroad.FROM_DATE AndAlso p.TO_DATE <= objAbroad.TO_DATE)) _
                        AndAlso p.NATION <> objAbroad.NATION
            Return query.Any
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Return False
        End Try
    End Function

    Public Function DeleteAbroad(ByVal lstID As List(Of Decimal)) As Boolean
        Try
            Dim lstObj = (From p In Context.HU_ABROAD Where lstID.Contains(p.ID))
            For Each item In lstObj
                Context.HU_ABROAD.DeleteObject(item)
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Return False
        End Try
    End Function

    Public Function GET_ABROAD_DATA_IMPORT() As DataSet
        Try
            Using cls As New DataAccess.QueryData
                Dim dsdata As DataSet = cls.ExecuteStore("PKG_PROFILE_BUSINESS.GET_ABROAD_DATA_IMPORT",
                                                         New With {.P_CUR = cls.OUT_CURSOR}, False)
                Return dsdata
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function IMPORT_ABROAD(ByVal P_DOCXML As String, ByVal P_USER As String) As Boolean
        Try

            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PROFILE_BUSINESS.IMPORT_ABROAD",
                                               New With {.P_DOCXML = P_DOCXML,
                                                         .P_USER = P_USER,
                                                         .P_CUR = cls.OUT_CURSOR}, True)
                Return CBool(dtData(0)(0))
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Return False
        End Try

    End Function
#End Region
#Region "BHLD"
    Public Function getOrgName(ByVal id As Decimal) As String
        Return (From p In Context.HU_ORGANIZATION Where p.ID = id Select p.NAME_VN).FirstOrDefault
    End Function
    Public Function saveBHLD(ByVal year As Decimal, ByVal lst_col As List(Of String), ByVal dt As DataTable, ByVal is_import As Boolean, ByVal log As UserLog) As Decimal
        Try
            Dim re = "NON_NAM1 NON_NAM2 NON_NU1 NON_NU2" 'add remark for this code
            If is_import Then
                For Each item As DataRow In dt.Rows
                    Dim id1 = CDec(item("EMP_ID"))
                    Dim lstObj = (From p In Context.HU_BHLD Where p.EMPLOYEE_ID = id1 And p.YEAR = year)
                    For Each it In lstObj
                        Context.HU_BHLD.DeleteObject(it)
                    Next
                    For Each col In lst_col
                        If col <> "REMARK" And col <> "EMP_ID" Then

                            If item(col) = "x" Then
                                Dim objBHLDData As New HU_BHLD
                                Dim item_id = (From p In Context.HU_BHLD_ITEM Where p.CODE.ToUpper = col.ToUpper Select p).FirstOrDefault

                                objBHLDData.ID = Utilities.GetNextSequence(Context, Context.HU_BHLD.EntitySet.Name)
                                objBHLDData.YEAR = year
                                objBHLDData.EMPLOYEE_ID = CDec(Val(item("EMP_ID")))
                                objBHLDData.ITEM_ID = item_id.ID
                                If re.Contains(col) Then
                                    objBHLDData.REMARK = item("REMARK").ToString
                                End If
                                Context.HU_BHLD.AddObject(objBHLDData)
                            End If
                        End If
                    Next
                Next
            Else
                For Each item As DataRow In dt.Rows
                    Dim id1 = CDec(item("EMP_ID"))
                    Dim lstObj = (From p In Context.HU_BHLD Where p.EMPLOYEE_ID = id1 And p.YEAR = year)
                    For Each it In lstObj
                        Context.HU_BHLD.DeleteObject(it)
                    Next
                    For Each col In lst_col
                        If col <> "REMARK" And col <> "EMP_ID" Then

                            If item(col) Then
                                Dim objBHLDData As New HU_BHLD
                                Dim item_id = (From p In Context.HU_BHLD_ITEM Where p.CODE.ToUpper = col.ToUpper Select p).FirstOrDefault

                                objBHLDData.ID = Utilities.GetNextSequence(Context, Context.HU_BHLD.EntitySet.Name)
                                objBHLDData.YEAR = year
                                objBHLDData.EMPLOYEE_ID = item("EMP_ID")
                                objBHLDData.ITEM_ID = item_id.ID
                                If re.Contains(col) Then
                                    objBHLDData.REMARK = item("REMARK").ToString
                                End If

                                Context.HU_BHLD.AddObject(objBHLDData)
                            End If
                        End If
                    Next
                Next
            End If

            Context.SaveChanges(log)
            Return 1
        Catch ex As Exception
            Return 0
        End Try
    End Function
    Public Function countItemPortal(ByVal emp_id As Decimal, ByVal type As String, ByVal tag As String) As Decimal
        Using cls As New DataAccess.QueryData

            Dim ds As DataSet = cls.ExecuteStore("PKG_AT_PROCESS.COUNT_ITEM_PORTAL", New With {.P_EMPLOYEE_ID = emp_id,
                                                                                               .P_TYPE = type,
                                                                                               .P_TAG = tag,
                                                                                               .P_CUR = cls.OUT_CURSOR}, False)
            If ds IsNot Nothing Then
                Dim Total = ds.Tables(0).Rows(0)("TOTAL")
                Return Total
            Else
                Return 0
            End If


        End Using
    End Function
    Public Function getListCol() As List(Of BHLDDTO)
        Try
            Dim re = (From p In Context.HU_BHLD_ITEM Where p.HIDE <> -1 And p.ACTFLG = "A" And p.AUTOGEN = -1
                      Order By p.ORDER_NUM
                      Select New BHLDDTO With {
                          .CODE = p.CODE,
                          .NAME = p.NAME_VN}).ToList
            Return re
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function saveBdldPortal(ByVal year As Decimal, ByVal dt As String, ByVal log As UserLog, ByVal is_send As Decimal) As Decimal
        Try
            Dim tb As New DataTable
            Using cls As New DataAccess.QueryData
                Dim dtData As DataSet = cls.ExecuteStore("PKG_HU_IPROFILE.GET_BHLD_PORTAL_INFO",
                                           New With {.P_DOCXML = dt,
                                                     .P_YEAR = year,
                                                     .P_STATUS = "",
                                                     .P_RET = cls.OUT_CURSOR}, False)

                tb = dtData.Tables(0)
            End Using
            If tb.Rows.Count > 0 Then
                Return saveBHLD_portal(year, tb, log)
            Else
                Return 0
            End If
        Catch ex As Exception
            Return 0
        End Try

    End Function

    Public Function saveBHLD_portal(ByVal year As Decimal, ByVal dt As DataTable, ByVal log As UserLog) As Decimal
        Try
            Dim lst_col = getListCol()
            Dim col_lst = New List(Of String)
            Dim ListKey() = New String(lst_col.Count + 1) {}
            ListKey(0) = "EMP_ID"
            Dim i = 1
            For Each item In lst_col
                ListKey(i) = item.CODE.ToUpper
                i += 1
            Next
            ListKey(i) = "REMARK"
            For Each key In ListKey
                col_lst.Add(key)
            Next
            Dim status_reg = (From p In Context.OT_OTHER_LIST Where p.TYPE_ID = 7 And p.CODE = "R" Select p.ID).FirstOrDefault

            Dim re = "NON_NAM1 NON_NAM2 NON_NU1 NON_NU2" 'add remark for this code
            Dim max_regg = (From p In Context.HU_BHLD_PORTAL Where p.ID_REGGROUP IsNot Nothing Select p.ID_REGGROUP Order By ID_REGGROUP Descending).FirstOrDefault
            If max_regg Is Nothing Then
                max_regg = 0
            End If
            For Each item As DataRow In dt.Rows
                Dim id1 = CDec(item("EMP_ID"))
                max_regg += 1
                Dim lstObj = (From p In Context.HU_BHLD_PORTAL Where p.EMPLOYEE_ID = id1 And p.YEAR = year)
                For Each it In lstObj
                    Context.HU_BHLD_PORTAL.DeleteObject(it)
                Next
                For Each col In col_lst
                    If col <> "REMARK" And col <> "EMP_ID" Then

                        If item(col) Then
                            Dim objBHLDData As New HU_BHLD_PORTAL
                            Dim item_id = (From p In Context.HU_BHLD_ITEM Where p.CODE.ToUpper = col.ToUpper Select p).FirstOrDefault

                            objBHLDData.ID = Utilities.GetNextSequence(Context, Context.HU_BHLD_PORTAL.EntitySet.Name)
                            objBHLDData.YEAR = year
                            objBHLDData.EMPLOYEE_ID = item("EMP_ID")
                            objBHLDData.ITEM_ID = item_id.ID
                            objBHLDData.STATUS_ID = status_reg
                            objBHLDData.ID_REGGROUP = max_regg

                            If re.Contains(col) Then
                                objBHLDData.REMARK = item("REMARK").ToString
                            End If

                            Context.HU_BHLD_PORTAL.AddObject(objBHLDData)
                        End If
                    End If
                Next
            Next

            Context.SaveChanges(log)
            Return 1
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Public Function Excel_DK_PD(ByVal year As Integer, ByVal empcode As String, ByVal statusId As String, ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer, ByVal _param As ParamDTO, ByVal type As String,
                                Optional ByVal Sorts As String = "CREATED_DATE desc",
                                Optional ByVal log As UserLog = Nothing) As Byte()
        Try
            Dim dtData As New DataSet
            Dim dt As New DataTable
            If type = "PD" Then
                Using cls As New DataAccess.QueryData
                    dtData = cls.ExecuteStore("PKG_HU_IPROFILE.LOAD_DATA_APPROVE_BHLD_PORTAL",
                                               New With {.P_USERNAME = log.Username,
                                                         .P_EMP_CODE = empcode,
                                                         .P_ORG_ID = _param.ORG_ID,
                                                         .P_YEAR = year,
                                                         .P_STATUS_ID = statusId,
                                                         .P_PAGE_INDEX = PageIndex + 1,
                                                         .P_PAGE_SIZE = PageSize,
                                                         .P_CUR = cls.OUT_CURSOR,
                                                         .P_CURCOUNT = cls.OUT_CURSOR}, False)
                    dt = dtData.Tables(0)
                End Using
            ElseIf type = "REG" Then
                Using cls As New DataAccess.QueryData
                    dtData = cls.ExecuteStore("PKG_HU_IPROFILE.LOAD_DATA_BHLD_PORTAL",
                                               New With {.P_USERNAME = log.Username,
                                                         .P_EMP_CODE = empcode,
                                                         .P_ORG_ID = _param.ORG_ID,
                                                         .P_YEAR = year,
                                                         .P_STATUS_ID = CDec(Val(statusId)),
                                                         .P_PAGE_INDEX = PageIndex + 1,
                                                         .P_PAGE_SIZE = PageSize,
                                                         .P_CUR = cls.OUT_CURSOR,
                                                         .P_CURCOUNT = cls.OUT_CURSOR}, False)
                    dt = dtData.Tables(0)
                End Using
            Else
                Using cls As New DataAccess.QueryData
                    dtData = cls.ExecuteStore("PKG_HU_IPROFILE.LOAD_DATA_BHLD",
                                               New With {.P_USERNAME = log.Username,
                                                         .P_EMP_CODE = empcode,
                                                         .P_ORG_ID = _param.ORG_ID,
                                                         .P_YEAR = year,
                                                         .P_PAGE_INDEX = PageIndex + 1,
                                                         .P_PAGE_SIZE = PageSize,
                                                         .P_CUR = cls.OUT_CURSOR,
                                                         .P_CURCOUNT = cls.OUT_CURSOR}, False)
                    dt = dtData.Tables(0)
                End Using
            End If

            Dim tb2 As New DataTable
            tb2.Columns.Add("ORG_NAME", GetType(String))
            tb2.Columns.Add("YEAR", GetType(String))
            tb2.Rows.Add(getOrgName(_param.ORG_ID), year)

            Dim configPath As String = ConfigurationManager.AppSettings("PathImportFolder")
            Dim dsData As New DataSet
            Dim dt_cop = dt.Copy()

            dsData.Tables.Add(dt_cop)
            dsData.Tables.Add(tb2)

            dsData.Tables(0).TableName = "Table"
            dsData.Tables(1).TableName = "Table1"
            If type = "PD" Then
                Return ExportTemplate("Profile\Import\BHLD_AP.xls",
                            dsData, "BHLD_Approve" & Format(Date.Now, "yyyymmdd"))
            Else
                Return ExportTemplate("Profile\Import\BHLD.xls",
                            dsData, "BHLD" & Format(Date.Now, "yyyymmdd"))
            End If
        Catch ex As Exception

        End Try

    End Function
    Public Function excel_BHLD_portal(ByVal year As Decimal, ByVal dts As String, ByVal log As UserLog, ByVal status As String) As Byte()
        Try
            Dim dt As New DataTable
            Using cls As New DataAccess.QueryData
                Dim dtData As DataSet = cls.ExecuteStore("PKG_HU_IPROFILE.GET_BHLD_PORTAL_INFO",
                                           New With {.P_DOCXML = dts,
                                                     .P_YEAR = year,
                                                     .P_STATUS = status.ToUpper,
                                                     .P_RET = cls.OUT_CURSOR}, False)

                dt = dtData.Tables(0)
            End Using
            Dim tb2 As New DataTable
            tb2.Columns.Add("ORG_NAME", GetType(String))
            tb2.Columns.Add("YEAR", GetType(String))
            tb2.Rows.Add(getOrgName(1), year)

            Dim configPath As String = ConfigurationManager.AppSettings("PathImportFolder")
            Dim dsData As New DataSet
            Dim dt_cop = dt.Copy()

            dsData.Tables.Add(dt_cop)
            dsData.Tables.Add(tb2)

            dsData.Tables(0).TableName = "Table"
            dsData.Tables(1).TableName = "Table1"
            If status = "DK" Then
                Return ExportTemplate("Profile\Import\BHLD.xls",
                            dsData, "BHLD" & Format(Date.Now, "yyyymmdd"))
            Else
                Return ExportTemplate("Profile\Import\BHLD_AP.xls",
                            dsData, "BHLD_Approve" & Format(Date.Now, "yyyymmdd"))
            End If


            'Return 1
        Catch ex As Exception
            'Return 0
        End Try
    End Function
    Public Function ExportTemplate(ByVal sReportFileName As String,
                                    ByVal dsData As DataSet,
                                    ByVal filename As String) As Byte()

        Dim filePath As String
        Dim templatefolder As String

        Dim designer As Aspose.Cells.WorkbookDesigner
        Try
            Dim msStream As New System.IO.MemoryStream

            templatefolder = ConfigurationManager.AppSettings("ReportTemplatesFolder2")
            filePath = AppDomain.CurrentDomain.BaseDirectory & templatefolder & "\" & sReportFileName

            'cau hinh lai duong dan tren server
            'filePath = sReportFileName

            If Not File.Exists(filePath) Then
                'ScriptManager.RegisterStartupScript(Page, Page.GetType(), "javascriptfunction", "goBack()", True)
                'Return False
            End If

            designer = New Aspose.Cells.WorkbookDesigner
            designer.Open(filePath)
            designer.SetDataSource(dsData)

            'If dtVariable IsNot Nothing Then
            '    Dim intCols As Integer = dtVariable.Columns.Count
            '    For i As Integer = 0 To intCols - 1
            '        designer.SetDataSource(dtVariable.Columns(i).ColumnName.ToString(), dtVariable.Rows(0).ItemArray(i).ToString())
            '    Next
            'End If
            designer.Process()
            designer.Workbook.CalculateFormula()
            designer.Workbook.Save(msStream, Aspose.Cells.SaveFormat.Xlsx)

            msStream.Close()

            Return msStream.ToArray()
        Catch ex As Exception
            'Return New Byte()
        End Try
    End Function

    Public Function GetBHLD_Register(ByVal year As Integer, ByVal empcode As String, ByVal statusId As Decimal, ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer, ByVal _param As ParamDTO,
                                Optional ByVal Sorts As String = "CREATED_DATE desc",
                                Optional ByVal log As UserLog = Nothing) As DataTable

        Try

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using


            Using cls As New DataAccess.QueryData
                Dim dtData As DataSet = cls.ExecuteStore("PKG_HU_IPROFILE.LOAD_DATA_BHLD_PORTAL",
                                           New With {.P_USERNAME = log.Username,
                                                     .P_EMP_CODE = empcode,
                                                     .P_ORG_ID = _param.ORG_ID,
                                                     .P_YEAR = year,
                                                     .P_STATUS_ID = statusId,
                                                     .P_PAGE_INDEX = PageIndex + 1,
                                                     .P_PAGE_SIZE = PageSize,
                                                     .P_CUR = cls.OUT_CURSOR,
                                                     .P_CURCOUNT = cls.OUT_CURSOR}, False)
                Total = dtData.Tables(1).Rows(0)("TOTAL")
                Return dtData.Tables(0)
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetBHLD(ByVal year As Integer, ByVal empcode As String, ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer, ByVal _param As ParamDTO,
                                Optional ByVal Sorts As String = "CREATED_DATE desc",
                                Optional ByVal log As UserLog = Nothing) As DataTable

        Try

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using


            Using cls As New DataAccess.QueryData
                Dim dtData As DataSet = cls.ExecuteStore("PKG_HU_IPROFILE.LOAD_DATA_BHLD",
                                           New With {.P_USERNAME = log.Username,
                                                     .P_EMP_CODE = empcode,
                                                     .P_ORG_ID = _param.ORG_ID,
                                                     .P_YEAR = year,
                                                     .P_PAGE_INDEX = PageIndex + 1,
                                                     .P_PAGE_SIZE = PageSize,
                                                     .P_CUR = cls.OUT_CURSOR,
                                                     .P_CURCOUNT = cls.OUT_CURSOR}, False)
                Total = dtData.Tables(1).Rows(0)("TOTAL")
                Return dtData.Tables(0)
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function GetBHLD1(ByVal year As Integer, ByVal empcode As String, ByVal PageIndex As Integer,
                               ByVal PageSize As Integer,
                               ByRef Total As Integer, ByVal _param As ParamDTO,
                               Optional ByVal Sorts As String = "CREATED_DATE desc",
                               Optional ByVal log As UserLog = Nothing) As DataTable

        Try

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using


            Using cls As New DataAccess.QueryData
                Dim dtData As DataSet = cls.ExecuteStore("PKG_HU_IPROFILE.LOAD_DATA_BHLD1",
                                           New With {.P_USERNAME = log.Username,
                                                     .P_EMP_CODE = empcode,
                                                     .P_ORG_ID = _param.ORG_ID,
                                                     .P_YEAR = year,
                                                     .P_PAGE_INDEX = PageIndex + 1,
                                                     .P_PAGE_SIZE = PageSize,
                                                     .P_CUR = cls.OUT_CURSOR,
                                                     .P_CURCOUNT = cls.OUT_CURSOR}, False)
                Total = dtData.Tables(1).Rows(0)("TOTAL")
                Return dtData.Tables(0)
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetBHLD_Approve(ByVal year As Integer, ByVal empcode As String, ByVal statusId As String, ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer, ByVal _param As ParamDTO,
                                Optional ByVal Sorts As String = "CREATED_DATE desc",
                                Optional ByVal log As UserLog = Nothing) As DataTable

        Try

            Using cls As New DataAccess.QueryData
                Dim dtData As DataSet = cls.ExecuteStore("PKG_HU_IPROFILE.LOAD_DATA_APPROVE_BHLD_PORTAL",
                                           New With {.P_USERNAME = log.Username,
                                                     .P_EMP_CODE = empcode,
                                                     .P_ORG_ID = _param.ORG_ID,
                                                     .P_YEAR = year,
                                                     .P_STATUS_ID = statusId,
                                                     .P_PAGE_INDEX = PageIndex + 1,
                                                     .P_PAGE_SIZE = PageSize,
                                                     .P_CUR = cls.OUT_CURSOR,
                                                     .P_CURCOUNT = cls.OUT_CURSOR}, False)
                Total = dtData.Tables(1).Rows(0)("TOTAL")
                Return dtData.Tables(0)
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function CalculateBHLD(ByVal year As Integer, ByVal _param As ParamDTO, Optional ByVal log As UserLog = Nothing) As Boolean

        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataSet = cls.ExecuteStore("PKG_HU_IPROFILE.CALCULATE_BHLD",
                                           New With {.P_USERNAME = log.Username,
                                                     .P_ORG_ID = _param.ORG_ID,
                                                     .P_ISDISSOLVE = CDec(_param.IS_DISSOLVE),
                                                     .P_YEAR = year,
                                                     .P_CUR = cls.OUT_CURSOR}, False)
                If dtData IsNot Nothing AndAlso dtData.Tables.Count > 0 AndAlso dtData.Tables(0).Rows.Count > 0 Then
                    Return CBool(dtData.Tables(0).Rows(0)(0))
                End If
            End Using
            Return False
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

#End Region


#Region "Travel"

    Public Function GetTravels(ByVal _filter As HUTravelDTO, ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer, ByVal _param As ParamDTO,
                                Optional ByVal Sorts As String = "CREATED_DATE desc",
                                Optional ByVal log As UserLog = Nothing) As List(Of HUTravelDTO)

        Try

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using


            Dim query = From p In Context.HU_TRAVEL
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From o In Context.HUV_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From mt In Context.HU_TITLE_BLD.Where(Function(f) f.ID = e.MATHE).DefaultIfEmpty
                        From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = e.ORG_ID And
                                                                       f.USERNAME = log.Username.ToUpper)
                        Select New HUTravelDTO With {.ID = p.ID,
                                                    .EMPLOYEE_ID = p.EMPLOYEE_ID,
                                                    .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                                                    .EMPLOYEE_NAME = e.FULLNAME_VN,
                                                    .TITLE_ID = e.TITLE_ID,
                                                    .TITLE_NAME = t.NAME_VN,
                                                    .ORG_ID = e.ORG_ID,
                                                    .ORG_NAME = o.NAME_VN,
                                                    .ORG_DESC = o.DESCRIPTION_PATH,
                                                    .MATHE = e.MATHE,
                                                    .MATHE_NAME = mt.NAME_VN,
                                                    .FROM_DATE = p.FROM_DATE,
                                                    .TO_DATE = p.TO_DATE,
                                                    .DAY_NUM = p.DAY_NUM,
                                                    .PLACE_NAME = p.PLACE_NAME,
                                                    .MONEY = p.MONEY,
                                                    .REMARK = p.REMARK,
                                                    .CREATED_DATE = p.CREATED_DATE}

            If _filter.EMPLOYEE_CODE <> "" Then
                query = query.Where(Function(f) f.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If
            If _filter.EMPLOYEE_NAME <> "" Then
                query = query.Where(Function(f) f.EMPLOYEE_NAME.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If
            If _filter.TITLE_NAME <> "" Then
                query = query.Where(Function(f) f.TITLE_NAME.ToUpper.Contains(_filter.TITLE_NAME.ToUpper))
            End If
            If _filter.ORG_NAME <> "" Then
                query = query.Where(Function(f) f.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If
            If _filter.MATHE_NAME <> "" Then
                query = query.Where(Function(f) f.MATHE_NAME.ToUpper.Contains(_filter.MATHE_NAME.ToUpper))
            End If
            If _filter.PLACE_NAME <> "" Then
                query = query.Where(Function(f) f.PLACE_NAME.ToUpper.Contains(_filter.PLACE_NAME.ToUpper))
            End If
            If _filter.REMARK <> "" Then
                query = query.Where(Function(f) f.REMARK.ToUpper.Contains(_filter.REMARK.ToUpper))
            End If
            If _filter.FROM_DATE.HasValue Then
                query = query.Where(Function(f) f.FROM_DATE = _filter.FROM_DATE)
            End If
            If _filter.TO_DATE.HasValue Then
                query = query.Where(Function(f) f.TO_DATE = _filter.TO_DATE)
            End If
            If _filter.DAY_NUM.HasValue Then
                query = query.Where(Function(f) f.DAY_NUM = _filter.DAY_NUM)
            End If
            If _filter.MONEY.HasValue Then
                query = query.Where(Function(f) f.MONEY = _filter.MONEY)
            End If

            query = query.OrderBy(Sorts)
            Total = query.Count
            query = query.Skip(PageIndex * PageSize).Take(PageSize)

            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function InsertTravel(ByVal objTravel As HUTravelDTO, ByVal log As UserLog) As Boolean

        Dim objTravelData As New HU_TRAVEL
        Try
            objTravelData.ID = Utilities.GetNextSequence(Context, Context.HU_TRAVEL.EntitySet.Name)
            objTravelData.EMPLOYEE_ID = objTravel.EMPLOYEE_ID
            objTravelData.FROM_DATE = objTravel.FROM_DATE
            objTravelData.TO_DATE = objTravel.TO_DATE
            objTravelData.REMARK = objTravel.REMARK
            objTravelData.PLACE_NAME = objTravel.PLACE_NAME
            objTravelData.DAY_NUM = objTravel.DAY_NUM
            objTravelData.MONEY = objTravel.MONEY
            Context.HU_TRAVEL.AddObject(objTravelData)
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Return False
        End Try

    End Function

    Public Function ModifyTravel(ByVal objTravel As HUTravelDTO, ByVal log As UserLog) As Boolean
        Dim objTravelData As New HU_TRAVEL
        Try
            objTravelData = (From p In Context.HU_TRAVEL Where p.ID = objTravel.ID).FirstOrDefault
            objTravelData.EMPLOYEE_ID = objTravel.EMPLOYEE_ID
            objTravelData.FROM_DATE = objTravel.FROM_DATE
            objTravelData.TO_DATE = objTravel.TO_DATE
            objTravelData.REMARK = objTravel.REMARK
            objTravelData.PLACE_NAME = objTravel.PLACE_NAME
            objTravelData.DAY_NUM = objTravel.DAY_NUM
            objTravelData.MONEY = objTravel.MONEY
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Return False
        End Try

    End Function

    Public Function ValidateTravel(ByVal objTravel As HUTravelDTO) As Boolean
        Try
            Dim query = From p In Context.HU_TRAVEL Where p.ID <> objTravel.ID AndAlso p.EMPLOYEE_ID = objTravel.EMPLOYEE_ID _
                        AndAlso ((objTravel.FROM_DATE >= p.FROM_DATE AndAlso objTravel.FROM_DATE <= p.TO_DATE) _
                                 OrElse (objTravel.TO_DATE >= p.FROM_DATE AndAlso objTravel.TO_DATE <= p.TO_DATE) _
                                 OrElse (p.FROM_DATE >= objTravel.FROM_DATE AndAlso p.FROM_DATE <= objTravel.TO_DATE) _
                                 OrElse (p.TO_DATE >= objTravel.FROM_DATE AndAlso p.TO_DATE <= objTravel.TO_DATE))
            Return query.Any
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Return False
        End Try
    End Function

    Public Function DeleteTravel(ByVal lstID As List(Of Decimal)) As Boolean
        Try
            Dim lstObj = (From p In Context.HU_TRAVEL Where lstID.Contains(p.ID))
            For Each item In lstObj
                Context.HU_TRAVEL.DeleteObject(item)
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Return False
        End Try
    End Function

    Public Function GET_TRAVEL_DATA_IMPORT() As DataSet
        Try
            Using cls As New DataAccess.QueryData
                Dim dsdata As DataSet = cls.ExecuteStore("PKG_PROFILE_BUSINESS.GET_TRAVEL_DATA_IMPORT",
                                                         New With {.P_CUR = cls.OUT_CURSOR}, False)
                Return dsdata
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function IMPORT_TRAVEL(ByVal P_DOCXML As String, ByVal P_USER As String) As Boolean
        Try

            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PROFILE_BUSINESS.IMPORT_TRAVEL",
                                               New With {.P_DOCXML = P_DOCXML,
                                                         .P_USER = P_USER,
                                                         .P_CUR = cls.OUT_CURSOR}, True)
                Return CBool(dtData(0)(0))
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Return False
        End Try

    End Function
#End Region

#Region "InfoConfirm"

    Public Function GetInfoConfirms(ByVal _filter As HUInfoConfirmDTO, ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer, ByVal _param As ParamDTO,
                                Optional ByVal Sorts As String = "CREATED_DATE desc",
                                Optional ByVal log As UserLog = Nothing) As List(Of HUInfoConfirmDTO)

        Try

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using


            Dim query = From p In Context.HU_INFO_CONFIRM
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From o In Context.HUV_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From mt In Context.HU_TITLE_BLD.Where(Function(f) f.ID = e.MATHE).DefaultIfEmpty
                        From a In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.APPROVER)
                        From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = e.ORG_ID And
                                                                       f.USERNAME = log.Username.ToUpper)
                        Select New HUInfoConfirmDTO With {.ID = p.ID,
                                                    .EMPLOYEE_ID = p.EMPLOYEE_ID,
                                                    .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                                                    .EMPLOYEE_NAME = e.FULLNAME_VN,
                                                    .TITLE_ID = e.TITLE_ID,
                                                    .TITLE_NAME = t.NAME_VN,
                                                    .ORG_ID = e.ORG_ID,
                                                    .ORG_NAME = o.NAME_VN,
                                                    .MATHE = e.MATHE,
                                                    .MATHE_NAME = mt.NAME_VN,
                                                    .APPROVE_DATE = p.APPROVE_DATE,
                                                    .PLACE = p.PLACE,
                                                    .REASON = p.REASON,
                                                    .APPROVER = p.APPROVER,
                                                    .APPROVER_CODE = a.EMPLOYEE_CODE,
                                                    .APPROVER_NAME = a.FULLNAME_VN,
                                                    .CREATED_DATE = p.CREATED_DATE}

            If _filter.EMPLOYEE_CODE <> "" Then
                query = query.Where(Function(f) f.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If
            If _filter.EMPLOYEE_NAME <> "" Then
                query = query.Where(Function(f) f.EMPLOYEE_NAME.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If
            If _filter.TITLE_NAME <> "" Then
                query = query.Where(Function(f) f.TITLE_NAME.ToUpper.Contains(_filter.TITLE_NAME.ToUpper))
            End If
            If _filter.ORG_NAME <> "" Then
                query = query.Where(Function(f) f.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If
            If _filter.MATHE_NAME <> "" Then
                query = query.Where(Function(f) f.MATHE_NAME.ToUpper.Contains(_filter.MATHE_NAME.ToUpper))
            End If
            If _filter.PLACE <> "" Then
                query = query.Where(Function(f) f.PLACE.ToUpper.Contains(_filter.PLACE.ToUpper))
            End If
            If _filter.REASON <> "" Then
                query = query.Where(Function(f) f.REASON.ToUpper.Contains(_filter.REASON.ToUpper))
            End If
            If _filter.APPROVE_DATE.HasValue Then
                query = query.Where(Function(f) f.APPROVE_DATE = _filter.APPROVE_DATE)
            End If
            If _filter.APPROVER.HasValue Then
                query = query.Where(Function(f) f.APPROVER = _filter.APPROVER)
            End If
            If _filter.APPROVER_CODE <> "" Then
                query = query.Where(Function(f) f.APPROVER_CODE.ToUpper.Contains(_filter.APPROVER_CODE.ToUpper))
            End If
            If _filter.APPROVER_NAME <> "" Then
                query = query.Where(Function(f) f.APPROVER_NAME.ToUpper.Contains(_filter.APPROVER_NAME.ToUpper))
            End If

            query = query.OrderBy(Sorts)
            Total = query.Count
            query = query.Skip(PageIndex * PageSize).Take(PageSize)

            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function InsertInfoConfirm(ByVal objInfoConfirm As HUInfoConfirmDTO, ByVal log As UserLog) As Boolean
        Dim objInfoConfirmData As New HU_INFO_CONFIRM
        Try
            objInfoConfirmData.ID = Utilities.GetNextSequence(Context, Context.HU_INFO_CONFIRM.EntitySet.Name)
            objInfoConfirmData.EMPLOYEE_ID = objInfoConfirm.EMPLOYEE_ID
            objInfoConfirmData.PLACE = objInfoConfirm.PLACE
            objInfoConfirmData.REASON = objInfoConfirm.REASON
            objInfoConfirmData.APPROVER = objInfoConfirm.APPROVER
            objInfoConfirmData.APPROVE_DATE = objInfoConfirm.APPROVE_DATE
            Context.HU_INFO_CONFIRM.AddObject(objInfoConfirmData)
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Return False
        End Try

    End Function

    Public Function ModifyInfoConfirm(ByVal objInfoConfirm As HUInfoConfirmDTO, ByVal log As UserLog) As Boolean
        Dim objInfoConfirmData As New HU_INFO_CONFIRM
        Try
            objInfoConfirmData = (From p In Context.HU_INFO_CONFIRM Where p.ID = objInfoConfirm.ID).FirstOrDefault
            objInfoConfirmData.EMPLOYEE_ID = objInfoConfirm.EMPLOYEE_ID
            objInfoConfirmData.PLACE = objInfoConfirm.PLACE
            objInfoConfirmData.REASON = objInfoConfirm.REASON
            objInfoConfirmData.APPROVER = objInfoConfirm.APPROVER
            objInfoConfirmData.APPROVE_DATE = objInfoConfirm.APPROVE_DATE
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Return False
        End Try

    End Function

    Public Function DeleteInfoConfirm(ByVal lstID As List(Of Decimal)) As Boolean
        Try
            Dim lstObj = (From p In Context.HU_INFO_CONFIRM Where lstID.Contains(p.ID))
            For Each item In lstObj
                Context.HU_INFO_CONFIRM.DeleteObject(item)
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Return False
        End Try
    End Function

    Public Function GetInfoConfirmPrintData(ByVal _id As Decimal) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PROFILE_BUSINESS.GET_INFO_CONFIRM_DATA",
                                                         New With {.P_ID = _id,
                                                                   .P_CUR = cls.OUT_CURSOR})
                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

#End Region

#Region "CB Planning"

    Public Function GetCBPlannings(ByVal _filter As CBPlanningDTO, ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer,
                                Optional ByVal Sorts As String = "CREATED_DATE desc",
                                Optional ByVal log As UserLog = Nothing) As List(Of CBPlanningDTO)

        Try



            Dim query = From p In Context.HU_CB_PLANNING
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.SIGNER_ID).DefaultIfEmpty
                        Select New CBPlanningDTO With {.ID = p.ID,
                                                       .DECISION_NO = p.DECISION_NO,
                                                       .EFFECT_DATE = p.EFFECT_DATE,
                                                       .CONTENT = p.CONTENT,
                                                       .SIGNER_NAME = e.FULLNAME_VN,
                                                       .SIGN_DATE = p.SIGN_DATE,
                                                       .CREATED_DATE = p.CREATED_DATE,
                                                       .YEAR = p.YEAR}

            If _filter.DECISION_NO <> "" Then
                query = query.Where(Function(f) f.DECISION_NO.ToUpper.Contains(_filter.DECISION_NO.ToUpper))
            End If
            If _filter.CONTENT <> "" Then
                query = query.Where(Function(f) f.CONTENT.ToUpper.Contains(_filter.CONTENT.ToUpper))
            End If
            If _filter.SIGNER_NAME <> "" Then
                query = query.Where(Function(f) f.SIGNER_NAME.ToUpper.Contains(_filter.SIGNER_NAME.ToUpper))
            End If
            If _filter.EFFECT_DATE.HasValue Then
                query = query.Where(Function(f) f.EFFECT_DATE = _filter.EFFECT_DATE)
            End If
            If _filter.YEAR.HasValue Then
                query = query.Where(Function(f) f.YEAR = _filter.YEAR)
            End If
            If _filter.SIGN_DATE.HasValue Then
                query = query.Where(Function(f) f.SIGN_DATE = _filter.SIGN_DATE)
            End If

            query = query.OrderBy(Sorts)
            Total = query.Count
            query = query.Skip(PageIndex * PageSize).Take(PageSize)

            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetCBPlanning(ByVal _id As Decimal) As CBPlanningDTO

        Try



            Dim query = From p In Context.HU_CB_PLANNING
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.SIGNER_ID).DefaultIfEmpty
                        Where p.ID = _id
                        Select New CBPlanningDTO With {.ID = p.ID,
                                                       .DECISION_NO = p.DECISION_NO,
                                                       .EFFECT_DATE = p.EFFECT_DATE,
                                                       .CONTENT = p.CONTENT,
                                                       .SIGNER_ID = p.SIGNER_ID,
                                                       .SIGNER_NAME = e.FULLNAME_VN,
                                                       .SIGNER_CODE = e.EMPLOYEE_CODE,
                                                       .SIGN_DATE = p.SIGN_DATE,
                                                       .YEAR = p.YEAR}


            Dim queryEmp = From pe In Context.HU_CB_PLANNING_EMP
                           From p In Context.HU_CB_PLANNING.Where(Function(f) f.ID = pe.CB_PLANNING_ID).DefaultIfEmpty
                           From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = pe.EMPLOYEE_ID).DefaultIfEmpty
                           From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                           From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                           From tp In Context.OT_OTHER_LIST.Where(Function(f) f.ID = pe.TITLE_PLANNING_ID).DefaultIfEmpty
                           From mt In Context.HU_TITLE_BLD.Where(Function(f) f.ID = e.MATHE).DefaultIfEmpty
                           Where p.ID = _id
                           Select New CBPlanningEmpDTO With {.ID = p.ID,
                                                             .CB_PLANNING_ID = pe.CB_PLANNING_ID,
                                                             .EMPLOYEE_ID = pe.EMPLOYEE_ID,
                                                             .EMPLOYEE_NAME = pe.EMPLOYEE_NAME,
                                                             .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                                                             .MATHE = e.MATHE,
                                                             .MATHE_NAME = mt.NAME_VN,
                                                             .TITLE_ID = e.TITLE_ID,
                                                             .TITLE_NAME = t.NAME_VN,
                                                             .ORG_ID = e.ORG_ID,
                                                             .ORG_NAME = If(pe.IS_OUTSIDE = -1, pe.ORG_OUTSIDE_NAME, o.NAME_VN),
                                                             .IS_OUTSIDE = pe.IS_OUTSIDE,
                                                             .ORG_OUTSIDE_NAME = pe.ORG_OUTSIDE_NAME,
                                                             .TITLE_PLANNING_ID = pe.TITLE_PLANNING_ID,
                                                             .TITLE_PLANNING_NAME = tp.NAME_VN}

            Dim objCB = query.FirstOrDefault()
            If objCB IsNot Nothing Then
                objCB.lstEmp = queryEmp.ToList()
            End If
            Return objCB
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function DeleteCBPlanning(ByVal lstID As List(Of Decimal)) As Boolean
        Try
            Dim lstObj = (From p In Context.HU_CB_PLANNING Where lstID.Contains(p.ID))
            For Each item In lstObj
                For Each itemEmpH In (From p In Context.HU_CB_PLANNING_EMP_HISTORY Where p.CB_PLANNING_ID = item.ID)
                    Context.HU_CB_PLANNING_EMP_HISTORY.DeleteObject(itemEmpH)
                Next
                For Each itemEmp In (From p In Context.HU_CB_PLANNING_EMP Where p.CB_PLANNING_ID = item.ID)
                    Context.HU_CB_PLANNING_EMP.DeleteObject(itemEmp)
                Next
                Context.HU_CB_PLANNING.DeleteObject(item)
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Return False
        End Try
    End Function

    Public Function InsertCBPlanning(ByVal objCBPlanning As CBPlanningDTO, ByVal log As UserLog) As Boolean
        Dim objCBPlanningData As New HU_CB_PLANNING
        Try
            objCBPlanningData.ID = Utilities.GetNextSequence(Context, Context.HU_CB_PLANNING.EntitySet.Name)
            objCBPlanningData.YEAR = objCBPlanning.YEAR
            objCBPlanningData.SIGNER_ID = objCBPlanning.SIGNER_ID
            objCBPlanningData.DECISION_NO = objCBPlanning.DECISION_NO
            objCBPlanningData.CONTENT = objCBPlanning.CONTENT
            objCBPlanningData.SIGN_DATE = objCBPlanning.SIGN_DATE
            objCBPlanningData.EFFECT_DATE = objCBPlanning.EFFECT_DATE
            Context.HU_CB_PLANNING.AddObject(objCBPlanningData)
            If objCBPlanning.lstEmp.Count > 0 Then
                For Each item In objCBPlanning.lstEmp
                    Dim objCBEmpData As New HU_CB_PLANNING_EMP
                    objCBEmpData.ID = Utilities.GetNextSequence(Context, Context.HU_CB_PLANNING_EMP.EntitySet.Name)
                    objCBEmpData.EMPLOYEE_ID = item.EMPLOYEE_ID
                    objCBEmpData.EMPLOYEE_NAME = item.EMPLOYEE_NAME
                    objCBEmpData.TITLE_PLANNING_ID = item.TITLE_PLANNING_ID
                    objCBEmpData.CB_PLANNING_ID = objCBPlanningData.ID
                    objCBEmpData.IS_OUTSIDE = item.IS_OUTSIDE
                    objCBEmpData.ORG_OUTSIDE_NAME = item.ORG_OUTSIDE_NAME
                    Context.HU_CB_PLANNING_EMP.AddObject(objCBEmpData)

                    Dim objCBEmpHisData As New HU_CB_PLANNING_EMP_HISTORY
                    objCBEmpHisData.ID = Utilities.GetNextSequence(Context, Context.HU_CB_PLANNING_EMP_HISTORY.EntitySet.Name)
                    objCBEmpHisData.EMPLOYEE_ID = item.EMPLOYEE_ID
                    objCBEmpHisData.EMPLOYEE_NAME = item.EMPLOYEE_NAME
                    objCBEmpHisData.TITLE_PLANNING_ID = item.TITLE_PLANNING_ID
                    objCBEmpHisData.CB_PLANNING_ID = objCBPlanningData.ID
                    objCBEmpHisData.IS_OUTSIDE = item.IS_OUTSIDE
                    objCBEmpHisData.ORG_OUTSIDE_NAME = item.ORG_OUTSIDE_NAME
                    objCBEmpHisData.STATUS_ID = 788685
                    Context.HU_CB_PLANNING_EMP_HISTORY.AddObject(objCBEmpHisData)
                Next
            End If
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Return False
        End Try
    End Function

    Public Function ModifyCBPlanning(ByVal objCBPlanning As CBPlanningDTO, ByVal log As UserLog) As Boolean
        Dim objCBPlanningData As New HU_CB_PLANNING
        Try
            objCBPlanningData = (From p In Context.HU_CB_PLANNING Where p.ID = objCBPlanning.ID).FirstOrDefault
            objCBPlanningData.YEAR = objCBPlanning.YEAR
            objCBPlanningData.SIGNER_ID = objCBPlanning.SIGNER_ID
            objCBPlanningData.DECISION_NO = objCBPlanning.DECISION_NO
            objCBPlanningData.CONTENT = objCBPlanning.CONTENT
            objCBPlanningData.SIGN_DATE = objCBPlanning.SIGN_DATE
            objCBPlanningData.EFFECT_DATE = objCBPlanning.EFFECT_DATE
            For Each item In (From p In Context.HU_CB_PLANNING_EMP Where p.CB_PLANNING_ID = objCBPlanning.ID)
                Dim check = (From it In objCBPlanning.lstEmp Where (item.EMPLOYEE_ID IsNot Nothing And it.EMPLOYEE_ID = item.EMPLOYEE_ID) _
                                                               Or (it.EMPLOYEE_NAME.ToUpper.Equals(item.EMPLOYEE_NAME.ToUpper) AndAlso item.EMPLOYEE_ID Is Nothing))
                Dim hisItem = (From p In Context.HU_CB_PLANNING_EMP_HISTORY Where ((item.EMPLOYEE_ID IsNot Nothing And p.EMPLOYEE_ID = item.EMPLOYEE_ID) _
                                                                                OrElse (p.EMPLOYEE_NAME.ToUpper.Equals(item.EMPLOYEE_NAME.ToUpper) AndAlso item.EMPLOYEE_ID Is Nothing)) And p.CB_PLANNING_ID = item.CB_PLANNING_ID).FirstOrDefault
                If check.Any Then
                    If check.FirstOrDefault().TITLE_PLANNING_ID <> item.TITLE_PLANNING_ID Then
                        If hisItem IsNot Nothing Then
                            hisItem.STATUS_ID = 788687
                        End If
                        item.TITLE_PLANNING_ID = check.FirstOrDefault().TITLE_PLANNING_ID
                    End If
                Else
                    If hisItem IsNot Nothing Then
                        hisItem.STATUS_ID = 788686
                    End If
                    Context.HU_CB_PLANNING_EMP.DeleteObject(item)
                End If
            Next

            If objCBPlanning.lstEmp.Count > 0 Then
                For Each item In objCBPlanning.lstEmp
                    Dim check = (From p In Context.HU_CB_PLANNING_EMP Where p.CB_PLANNING_ID = objCBPlanning.ID And ((item.EMPLOYEE_ID IsNot Nothing And p.EMPLOYEE_ID = item.EMPLOYEE_ID) _
                                                                        Or (p.EMPLOYEE_NAME.ToUpper.Equals(item.EMPLOYEE_NAME.ToUpper) AndAlso item.EMPLOYEE_ID Is Nothing)))
                    If Not check.Any Then
                        Dim objCBEmpData As New HU_CB_PLANNING_EMP
                        objCBEmpData.ID = Utilities.GetNextSequence(Context, Context.HU_CB_PLANNING_EMP.EntitySet.Name)
                        objCBEmpData.EMPLOYEE_ID = item.EMPLOYEE_ID
                        objCBEmpData.EMPLOYEE_NAME = item.EMPLOYEE_NAME
                        objCBEmpData.TITLE_PLANNING_ID = item.TITLE_PLANNING_ID
                        objCBEmpData.CB_PLANNING_ID = objCBPlanningData.ID
                        objCBEmpData.IS_OUTSIDE = item.IS_OUTSIDE
                        objCBEmpData.ORG_OUTSIDE_NAME = item.ORG_OUTSIDE_NAME
                        Context.HU_CB_PLANNING_EMP.AddObject(objCBEmpData)

                        Dim objCBEmpHisData As New HU_CB_PLANNING_EMP_HISTORY
                        objCBEmpHisData.ID = Utilities.GetNextSequence(Context, Context.HU_CB_PLANNING_EMP_HISTORY.EntitySet.Name)
                        objCBEmpHisData.EMPLOYEE_ID = item.EMPLOYEE_ID
                        objCBEmpHisData.EMPLOYEE_NAME = item.EMPLOYEE_NAME
                        objCBEmpHisData.TITLE_PLANNING_ID = item.TITLE_PLANNING_ID
                        objCBEmpHisData.CB_PLANNING_ID = objCBPlanningData.ID
                        objCBEmpHisData.IS_OUTSIDE = item.IS_OUTSIDE
                        objCBEmpHisData.ORG_OUTSIDE_NAME = item.ORG_OUTSIDE_NAME
                        objCBEmpHisData.STATUS_ID = 788685
                        Context.HU_CB_PLANNING_EMP_HISTORY.AddObject(objCBEmpHisData)
                    End If
                Next
            End If
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Return False
        End Try
    End Function

    Public Function CopyCBPlanning(ByVal _id As Decimal, ByVal log As UserLog) As Boolean
        Try

            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PROFILE_BUSINESS.COPY_CB_PLANNING",
                                               New With {.P_ID = _id,
                                                         .P_USER = log.Username.ToUpper,
                                                         .P_CUR = cls.OUT_CURSOR}, True)
                Return CBool(dtData(0)(0))
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Return False
        End Try
    End Function

    Public Function GetCBPlanningsHistory(ByVal _filter As CBPlanningEmpHisDTO, ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer,
                                Optional ByVal Sorts As String = "CREATED_DATE desc",
                                Optional ByVal log As UserLog = Nothing) As List(Of CBPlanningEmpHisDTO)

        Try



            Dim query = From pe In Context.HU_CB_PLANNING_EMP_HISTORY
                        From p In Context.HU_CB_PLANNING.Where(Function(f) f.ID = pe.CB_PLANNING_ID)
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = pe.EMPLOYEE_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From tp In Context.OT_OTHER_LIST.Where(Function(f) f.ID = pe.TITLE_PLANNING_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From st In Context.OT_OTHER_LIST.Where(Function(f) f.ID = pe.STATUS_ID).DefaultIfEmpty
                        From mt In Context.HU_TITLE_BLD.Where(Function(f) f.ID = e.MATHE).DefaultIfEmpty
                        Where p.ID = _filter.CB_PLANNING_ID
                        Select New CBPlanningEmpHisDTO With {.ID = p.ID,
                                                             .CB_PLANNING_ID = pe.CB_PLANNING_ID,
                                                             .EMPLOYEE_ID = pe.EMPLOYEE_ID,
                                                             .EMPLOYEE_NAME = pe.EMPLOYEE_NAME,
                                                             .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                                                             .MATHE = e.MATHE,
                                                             .MATHE_NAME = mt.NAME_VN,
                                                             .TITLE_ID = e.TITLE_ID,
                                                             .TITLE_NAME = t.NAME_VN,
                                                             .ORG_ID = e.ORG_ID,
                                                             .ORG_NAME = If(pe.IS_OUTSIDE = -1, pe.ORG_OUTSIDE_NAME, o.NAME_VN),
                                                             .IS_OUTSIDE = pe.IS_OUTSIDE,
                                                             .ORG_OUTSIDE_NAME = pe.ORG_OUTSIDE_NAME,
                                                             .STATUS_ID = pe.STATUS_ID,
                                                             .STATUS_NAME = st.NAME_VN,
                                                             .TITLE_PLANNING_ID = pe.TITLE_PLANNING_ID,
                                                             .TITLE_PLANNING_NAME = tp.NAME_VN,
                                                             .CREATED_DATE = pe.CREATED_DATE}

            If _filter.EMPLOYEE_CODE <> "" Then
                query = query.Where(Function(f) f.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If
            If _filter.EMPLOYEE_NAME <> "" Then
                query = query.Where(Function(f) f.EMPLOYEE_NAME.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If
            If _filter.MATHE_NAME <> "" Then
                query = query.Where(Function(f) f.MATHE_NAME.ToUpper.Contains(_filter.MATHE_NAME.ToUpper))
            End If
            If _filter.TITLE_NAME <> "" Then
                query = query.Where(Function(f) f.TITLE_NAME.ToUpper.Contains(_filter.TITLE_NAME.ToUpper))
            End If
            If _filter.ORG_NAME <> "" Then
                query = query.Where(Function(f) f.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If
            If _filter.ORG_OUTSIDE_NAME <> "" Then
                query = query.Where(Function(f) f.ORG_OUTSIDE_NAME.ToUpper.Contains(_filter.ORG_OUTSIDE_NAME.ToUpper))
            End If
            If _filter.TITLE_PLANNING_NAME <> "" Then
                query = query.Where(Function(f) f.TITLE_PLANNING_NAME.ToUpper.Contains(_filter.TITLE_PLANNING_NAME.ToUpper))
            End If
            If _filter.STATUS_NAME <> "" Then
                query = query.Where(Function(f) f.STATUS_NAME.ToUpper.Contains(_filter.STATUS_NAME.ToUpper))
            End If

            query = query.OrderBy(Sorts)
            Total = query.Count
            query = query.Skip(PageIndex * PageSize).Take(PageSize)

            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

#End Region

#Region "Commitee"

    Public Function GetCommitees(ByVal _filter As CommiteeDTO,
                                 ByVal PageIndex As Integer, ByVal PageSize As Integer, ByRef Total As Integer,
                                 ByVal _param As ParamDTO,
                                 Optional ByVal Sorts As String = "CREATED_DATE desc",
                                 Optional ByVal log As UserLog = Nothing) As List(Of CommiteeDTO)
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using

            Dim query = From p In Context.HU_COMMITEE
                        From emp In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From emp_org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = emp.ORG_ID)
                        From title In Context.HU_TITLE.Where(Function(f) f.ID = emp.TITLE_ID).DefaultIfEmpty
                        From ct In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
                        From rank In Context.HU_JOB_BAND.Where(Function(f) f.ID = emp.STAFF_RANK_ID).DefaultIfEmpty
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID)
                        From signer In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.SIGNER_ID).DefaultIfEmpty
                        From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = p.ORG_ID And f.USERNAME = log.Username.ToUpper)
                        Select New CommiteeDTO With {.ID = p.ID,
                                                     .CREATED_DATE = p.CREATED_DATE,
                                                     .EMPLOYEE_ID = p.EMPLOYEE_ID,
                                                     .EMPLOYEE_CODE = emp.EMPLOYEE_CODE,
                                                     .EMPLOYEE_NAME = emp.FULLNAME_VN,
                                                     .EMPLOYEE_TITLE = title.NAME_VN,
                                                     .EMPLOYEE_LEVEL = rank.LEVEL_FROM,
                                                     .EMPLOYEE_ORG = emp_org.NAME_VN,
                                                     .EMPLOYEE_ORG_DESC = emp_org.DESCRIPTION_PATH,
                                                     .ORG_ID = p.ORG_ID,
                                                     .ORG_ID_EMP = emp.ORG_ID,
                                                     .ORG_NAME = org.NAME_VN,
                                                     .ORG_DESC = org.DESCRIPTION_PATH,
                                                     .COMMITTE_POSITION = p.COMMITTE_POSITION,
                                                     .FROM_DATE = p.FROM_DATE,
                                                     .TO_DATE = p.TO_DATE,
                                                     .DECISION_NO = p.DECISION_NO,
                                                     .REMARK = p.REMARK,
                                                     .SIGNER_ID = p.SIGNER_ID,
                                                     .SIGNER_CODE = signer.EMPLOYEE_CODE,
                                                     .TITLE_ID = p.TITLE_ID,
                                                     .TITLE_NAME = ct.NAME_VN,
                                                     .SIGNER_NAME = signer.FULLNAME_VN,
                                                     .STATUS_NAME = If(p.TO_DATE IsNot Nothing AndAlso p.TO_DATE <= Date.Now, "Đã thôi nhiệm", "Đang làm việc")}

            If _filter.EMPLOYEE_CODE <> "" Then
                query = query.Where(Function(f) f.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If
            If _filter.EMPLOYEE_NAME <> "" Then
                query = query.Where(Function(f) f.EMPLOYEE_NAME.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If
            If _filter.EMPLOYEE_TITLE <> "" Then
                query = query.Where(Function(f) f.EMPLOYEE_TITLE.ToUpper.Contains(_filter.EMPLOYEE_TITLE.ToUpper))
            End If
            If _filter.TITLE_NAME <> "" Then
                query = query.Where(Function(f) f.TITLE_NAME.ToUpper.Contains(_filter.TITLE_NAME.ToUpper))
            End If
            If _filter.EMPLOYEE_LEVEL <> "" Then
                query = query.Where(Function(f) f.EMPLOYEE_LEVEL.ToUpper.Contains(_filter.EMPLOYEE_LEVEL.ToUpper))
            End If
            If _filter.ORG_NAME <> "" Then
                query = query.Where(Function(f) f.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If
            If _filter.COMMITTE_POSITION <> "" Then
                query = query.Where(Function(f) f.COMMITTE_POSITION.ToUpper.Contains(_filter.COMMITTE_POSITION.ToUpper))
            End If
            If _filter.DECISION_NO <> "" Then
                query = query.Where(Function(f) f.DECISION_NO.ToUpper.Contains(_filter.DECISION_NO.ToUpper))
            End If
            If _filter.REMARK <> "" Then
                query = query.Where(Function(f) f.REMARK.ToUpper.Contains(_filter.REMARK.ToUpper))
            End If
            If _filter.SIGNER_CODE <> "" Then
                query = query.Where(Function(f) f.SIGNER_CODE.ToUpper.Contains(_filter.SIGNER_CODE.ToUpper))
            End If
            If _filter.SIGNER_NAME <> "" Then
                query = query.Where(Function(f) f.SIGNER_NAME.ToUpper.Contains(_filter.SIGNER_NAME.ToUpper))
            End If
            If _filter.FROM_DATE.HasValue Then
                query = query.Where(Function(f) f.FROM_DATE = _filter.FROM_DATE)
            End If
            If _filter.TO_DATE.HasValue Then
                query = query.Where(Function(f) f.TO_DATE = _filter.TO_DATE)
            End If
            If _filter.IS_INACTIVE Then
                query = query.Where(Function(f) f.TO_DATE IsNot Nothing AndAlso f.TO_DATE <= Date.Now)
            Else
                query = query.Where(Function(f) Not (f.TO_DATE IsNot Nothing AndAlso f.TO_DATE <= Date.Now))
            End If
            If _filter.EMPLOYEE_ID IsNot Nothing Then
                query = query.Where(Function(f) f.EMPLOYEE_ID = _filter.EMPLOYEE_ID)
            End If

            query = query.OrderBy(Sorts)
            Total = query.Count
            query = query.Skip(PageIndex * PageSize).Take(PageSize)

            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetCommitee(ByVal _id As Decimal) As CommiteeDTO
        Try
            Dim query = From p In Context.HU_COMMITEE
                        From emp In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From emp_org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = emp.ORG_ID)
                        From title In Context.HU_TITLE.Where(Function(f) f.ID = emp.TITLE_ID).DefaultIfEmpty
                        From rank In Context.HU_STAFF_RANK.Where(Function(f) f.ID = emp.STAFF_RANK_ID).DefaultIfEmpty
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From signer In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.SIGNER_ID).DefaultIfEmpty
                        Select New CommiteeDTO With {.ID = p.ID,
                                                     .CREATED_DATE = p.CREATED_DATE,
                                                     .EMPLOYEE_ID = p.EMPLOYEE_ID,
                                                     .EMPLOYEE_CODE = emp.EMPLOYEE_CODE,
                                                     .EMPLOYEE_NAME = emp.FULLNAME_VN,
                                                     .EMPLOYEE_TITLE = title.NAME_VN,
                                                     .EMPLOYEE_LEVEL = rank.NAME,
                                                     .EMPLOYEE_ORG = emp_org.NAME_VN,
                                                     .ORG_ID = p.ORG_ID,
                                                     .ORG_NAME = org.NAME_VN,
                                                     .COMMITTE_POSITION = p.COMMITTE_POSITION,
                                                     .FROM_DATE = p.FROM_DATE,
                                                     .TO_DATE = p.TO_DATE,
                                                     .DECISION_NO = p.DECISION_NO,
                                                     .REMARK = p.REMARK,
                                                     .SIGNER_ID = p.SIGNER_ID,
                                                     .SIGNER_CODE = signer.EMPLOYEE_CODE,
                                                     .SIGNER_NAME = signer.FULLNAME_VN,
                                                     .STATUS_NAME = If(p.TO_DATE IsNot Nothing AndAlso p.TO_DATE <= Date.Now, "Đã thôi nhiệm", "Đang làm việc")}
            Return query.FirstOrDefault
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function DeleteCommitee(ByVal lstID As List(Of Decimal)) As Boolean
        Try
            Dim lstObj = (From p In Context.HU_COMMITEE Where lstID.Contains(p.ID))
            For Each item In lstObj
                Context.HU_COMMITEE.DeleteObject(item)
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Return False
        End Try
    End Function

    Public Function InsertCommitee(ByVal objCommitee As CommiteeDTO, ByVal log As UserLog) As Boolean
        Dim objCommiteeData As New HU_COMMITEE
        Try
            objCommiteeData.ID = Utilities.GetNextSequence(Context, Context.HU_COMMITEE.EntitySet.Name)
            objCommiteeData.EMPLOYEE_ID = objCommitee.EMPLOYEE_ID
            objCommiteeData.DECISION_NO = objCommitee.DECISION_NO
            objCommiteeData.COMMITTE_POSITION = objCommitee.COMMITTE_POSITION
            objCommiteeData.FROM_DATE = objCommitee.FROM_DATE
            objCommiteeData.TO_DATE = objCommitee.TO_DATE
            objCommiteeData.SIGNER_ID = objCommitee.SIGNER_ID
            objCommiteeData.TITLE_ID = objCommitee.TITLE_ID
            objCommiteeData.REMARK = objCommitee.REMARK
            objCommiteeData.ORG_ID = objCommitee.ORG_ID
            Context.HU_COMMITEE.AddObject(objCommiteeData)
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Return False
        End Try
    End Function

    Public Function ModifyCommitee(ByVal objCommitee As CommiteeDTO, ByVal log As UserLog) As Boolean
        Dim objCommiteeData As New HU_COMMITEE
        Try
            objCommiteeData = (From p In Context.HU_COMMITEE Where p.ID = objCommitee.ID).FirstOrDefault
            objCommiteeData.EMPLOYEE_ID = objCommitee.EMPLOYEE_ID
            objCommiteeData.DECISION_NO = objCommitee.DECISION_NO
            objCommiteeData.COMMITTE_POSITION = objCommitee.COMMITTE_POSITION
            objCommiteeData.FROM_DATE = objCommitee.FROM_DATE
            objCommiteeData.TO_DATE = objCommitee.TO_DATE
            objCommiteeData.SIGNER_ID = objCommitee.SIGNER_ID
            objCommiteeData.TITLE_ID = objCommitee.TITLE_ID
            objCommiteeData.REMARK = objCommitee.REMARK
            objCommiteeData.ORG_ID = objCommitee.ORG_ID
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Return False
        End Try
    End Function

    Public Function CopyCommitee(ByVal _id As Decimal, ByVal log As UserLog) As Boolean
        Try

            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PROFILE_BUSINESS.COPY_COMMITEE",
                                               New With {.P_ID = _id,
                                                         .P_USER = log.Username.ToUpper,
                                                         .P_CUR = cls.OUT_CURSOR}, True)
                Return CBool(dtData(0)(0))
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Return False
        End Try
    End Function

    Public Function GetCommiteesHistory(ByVal _filter As CommiteeEmpDTO, ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer,
                                Optional ByVal Sorts As String = "CREATED_DATE desc",
                                Optional ByVal log As UserLog = Nothing) As List(Of CommiteeEmpDTO)

        Try



            Dim query = From pe In Context.HU_COMMITEE_EMP_HISTORY
                        From p In Context.HU_COMMITEE.Where(Function(f) f.ID = pe.COMMITEE_ID).DefaultIfEmpty
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = pe.EMPLOYEE_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From tp In Context.HU_TITLE_TBL.Where(Function(f) f.ID = pe.COMMITEE_TITLE_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From st In Context.OT_OTHER_LIST.Where(Function(f) f.ID = pe.STATUS_ID).DefaultIfEmpty
                        From mt In Context.HU_TITLE_BLD.Where(Function(f) f.ID = e.MATHE).DefaultIfEmpty
                        Where p.ID = _filter.COMMITEE_ID
                        Select New CommiteeEmpDTO With {.ID = p.ID,
                                                           .COMMITEE_ID = pe.COMMITEE_ID,
                                                           .EMPLOYEE_ID = pe.EMPLOYEE_ID,
                                                           .EMPLOYEE_NAME = pe.EMPLOYEE_NAME,
                                                           .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                                                           .MATHE = e.MATHE,
                                                           .MATHE_NAME = mt.NAME_VN,
                                                           .TITLE_ID = e.TITLE_ID,
                                                           .TITLE_NAME = t.NAME_VN,
                                                           .ORG_ID = e.ORG_ID,
                                                           .ORG_NAME = If(pe.IS_OUTSIDE = -1, pe.ORG_OUTSIDE_NAME, o.NAME_VN),
                                                           .IS_OUTSIDE = pe.IS_OUTSIDE,
                                                           .ORG_OUTSIDE_NAME = pe.ORG_OUTSIDE_NAME,
                                                           .STATUS_ID = pe.STATUS_ID,
                                                           .STATUS_NAME = st.NAME_VN,
                                                           .COMMITEE_TITLE_ID = pe.COMMITEE_TITLE_ID,
                                                           .COMMITEE_TITLE_NAME = tp.NAME_VN,
                                                           .CREATED_DATE = pe.CREATED_DATE}

            If _filter.EMPLOYEE_CODE <> "" Then
                query = query.Where(Function(f) f.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If
            If _filter.EMPLOYEE_NAME <> "" Then
                query = query.Where(Function(f) f.EMPLOYEE_NAME.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If
            If _filter.MATHE_NAME <> "" Then
                query = query.Where(Function(f) f.MATHE_NAME.ToUpper.Contains(_filter.MATHE_NAME.ToUpper))
            End If
            If _filter.TITLE_NAME <> "" Then
                query = query.Where(Function(f) f.TITLE_NAME.ToUpper.Contains(_filter.TITLE_NAME.ToUpper))
            End If
            If _filter.ORG_NAME <> "" Then
                query = query.Where(Function(f) f.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If
            If _filter.ORG_OUTSIDE_NAME <> "" Then
                query = query.Where(Function(f) f.ORG_OUTSIDE_NAME.ToUpper.Contains(_filter.ORG_OUTSIDE_NAME.ToUpper))
            End If
            If _filter.COMMITEE_TITLE_NAME <> "" Then
                query = query.Where(Function(f) f.COMMITEE_TITLE_NAME.ToUpper.Contains(_filter.COMMITEE_TITLE_NAME.ToUpper))
            End If
            If _filter.STATUS_NAME <> "" Then
                query = query.Where(Function(f) f.STATUS_NAME.ToUpper.Contains(_filter.STATUS_NAME.ToUpper))
            End If

            query = query.OrderBy(Sorts)
            Total = query.Count
            query = query.Skip(PageIndex * PageSize).Take(PageSize)

            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function Portal_GetCommitee(ByVal _filter As CommiteeDTO,
                                       ByVal PageIndex As Integer, ByVal PageSize As Integer, ByRef Total As Integer,
                                       ByVal _param As ParamDTO,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc",
                                       Optional ByVal log As UserLog = Nothing) As List(Of CommiteeDTO)
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using

            Dim query = From p In Context.HU_COMMITEE
                        From emp In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From emp_org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = emp.ORG_ID)
                        From title In Context.HU_TITLE.Where(Function(f) f.ID = emp.TITLE_ID).DefaultIfEmpty
                        From ct In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
                        From rank In Context.HU_JOB_BAND.Where(Function(f) f.ID = emp.STAFF_RANK_ID).DefaultIfEmpty
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID)
                        From signer In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.SIGNER_ID).DefaultIfEmpty
                        From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = p.ORG_ID And f.USERNAME = log.Username.ToUpper)
                        Select New CommiteeDTO With {.ID = p.ID,
                                                     .CREATED_DATE = p.CREATED_DATE,
                                                     .EMPLOYEE_ID = p.EMPLOYEE_ID,
                                                     .EMPLOYEE_CODE = emp.EMPLOYEE_CODE,
                                                     .EMPLOYEE_NAME = emp.FULLNAME_VN,
                                                     .EMPLOYEE_TITLE = title.NAME_VN,
                                                     .EMPLOYEE_LEVEL = rank.LEVEL_FROM,
                                                     .EMPLOYEE_ORG = emp_org.NAME_VN,
                                                     .EMPLOYEE_ORG_DESC = emp_org.DESCRIPTION_PATH,
                                                     .ORG_ID = p.ORG_ID,
                                                     .ORG_NAME = org.NAME_VN,
                                                     .ORG_DESC = org.DESCRIPTION_PATH,
                                                     .COMMITTE_POSITION = ct.NAME_VN,
                                                     .FROM_DATE = p.FROM_DATE,
                                                     .TO_DATE = p.TO_DATE,
                                                     .DECISION_NO = p.DECISION_NO,
                                                     .REMARK = p.REMARK,
                                                     .SIGNER_ID = p.SIGNER_ID,
                                                     .SIGNER_CODE = signer.EMPLOYEE_CODE,
                                                     .SIGNER_NAME = signer.FULLNAME_VN,
                                                     .STATUS_NAME = If(p.TO_DATE IsNot Nothing AndAlso p.TO_DATE <= Date.Now, "Đã thôi nhiệm", "Đang làm việc")}

            If _filter.EMPLOYEE_CODE <> "" Then
                query = query.Where(Function(f) f.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If
            If _filter.EMPLOYEE_NAME <> "" Then
                query = query.Where(Function(f) f.EMPLOYEE_NAME.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If
            If _filter.EMPLOYEE_TITLE <> "" Then
                query = query.Where(Function(f) f.EMPLOYEE_TITLE.ToUpper.Contains(_filter.EMPLOYEE_TITLE.ToUpper))
            End If
            If _filter.EMPLOYEE_LEVEL <> "" Then
                query = query.Where(Function(f) f.EMPLOYEE_LEVEL.ToUpper.Contains(_filter.EMPLOYEE_LEVEL.ToUpper))
            End If
            If _filter.ORG_NAME <> "" Then
                query = query.Where(Function(f) f.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If
            If _filter.COMMITTE_POSITION <> "" Then
                query = query.Where(Function(f) f.COMMITTE_POSITION.ToUpper.Contains(_filter.COMMITTE_POSITION.ToUpper))
            End If
            If _filter.DECISION_NO <> "" Then
                query = query.Where(Function(f) f.DECISION_NO.ToUpper.Contains(_filter.DECISION_NO.ToUpper))
            End If
            If _filter.REMARK <> "" Then
                query = query.Where(Function(f) f.REMARK.ToUpper.Contains(_filter.REMARK.ToUpper))
            End If
            If _filter.SIGNER_CODE <> "" Then
                query = query.Where(Function(f) f.SIGNER_CODE.ToUpper.Contains(_filter.SIGNER_CODE.ToUpper))
            End If
            If _filter.SIGNER_NAME <> "" Then
                query = query.Where(Function(f) f.SIGNER_NAME.ToUpper.Contains(_filter.SIGNER_NAME.ToUpper))
            End If
            If _filter.FROM_DATE.HasValue Then
                query = query.Where(Function(f) f.FROM_DATE = _filter.FROM_DATE)
            End If
            If _filter.TO_DATE.HasValue Then
                query = query.Where(Function(f) f.TO_DATE = _filter.TO_DATE)
            End If
            If _filter.IS_INACTIVE Then
                query = query.Where(Function(f) f.TO_DATE IsNot Nothing AndAlso f.TO_DATE <= Date.Now)
            Else
                query = query.Where(Function(f) Not (f.TO_DATE IsNot Nothing AndAlso f.TO_DATE <= Date.Now))
            End If
            If _filter.EMPLOYEE_ID IsNot Nothing Then
                query = query.Where(Function(f) f.EMPLOYEE_ID = _filter.EMPLOYEE_ID)
            End If

            query = query.OrderBy(Sorts)
            Total = query.Count
            query = query.Skip(PageIndex * PageSize).Take(PageSize)

            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function GetCommiteeProcess(ByVal Username As String, ByVal empID As Decimal) As List(Of CommiteeDTO)
        Try
            Dim query = From p In Context.HU_COMMITEE
                        From emp In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From emp_org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = emp.ORG_ID)
                        From title In Context.HU_TITLE.Where(Function(f) f.ID = emp.TITLE_ID).DefaultIfEmpty
                        From rank In Context.HU_JOB_BAND.Where(Function(f) f.ID = emp.STAFF_RANK_ID).DefaultIfEmpty
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID)
                        From signer In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.SIGNER_ID).DefaultIfEmpty
                        Where p.EMPLOYEE_ID = empID
                        Select New CommiteeDTO With {.ID = p.ID,
                                                     .CREATED_DATE = p.CREATED_DATE,
                                                     .EMPLOYEE_ID = p.EMPLOYEE_ID,
                                                     .EMPLOYEE_CODE = emp.EMPLOYEE_CODE,
                                                     .EMPLOYEE_NAME = emp.FULLNAME_VN,
                                                     .EMPLOYEE_TITLE = title.NAME_VN,
                                                     .EMPLOYEE_LEVEL = rank.LEVEL_FROM,
                                                     .EMPLOYEE_ORG = emp_org.NAME_VN,
                                                     .ORG_ID = p.ORG_ID,
                                                     .ORG_NAME = org.NAME_VN,
                                                     .ORG_DESC = org.DESCRIPTION_PATH,
                                                     .COMMITTE_POSITION = p.COMMITTE_POSITION,
                                                     .FROM_DATE = p.FROM_DATE,
                                                     .TO_DATE = p.TO_DATE,
                                                     .DECISION_NO = p.DECISION_NO,
                                                     .REMARK = p.REMARK,
                                                     .SIGNER_ID = p.SIGNER_ID,
                                                     .SIGNER_CODE = signer.EMPLOYEE_CODE,
                                                     .SIGNER_NAME = signer.FULLNAME_VN,
                                                     .STATUS_NAME = If(p.TO_DATE IsNot Nothing AndAlso p.TO_DATE <= Date.Now, "Đã thôi nhiệm", "Đang làm việc")}
            query = query.OrderBy("CREATED_DATE desc")

            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

#End Region

#Region "CB Assessment"

    Public Function GetCBAssessments(ByVal _filter As CBAssessmentDTO, ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer,
                                Optional ByVal Sorts As String = "CREATED_DATE desc",
                                Optional ByVal log As UserLog = Nothing) As List(Of CBAssessmentDTO)

        Try



            Dim query = From p In Context.HU_CB_ASSESSMENT
                        Select New CBAssessmentDTO With {.ID = p.ID,
                                                         .CONFIRM_YEAR = p.CONFIRM_YEAR,
                                                         .ASSESSMENT_YEAR = p.ASSESSMENT_YEAR,
                                                         .CONTENT = p.CONTENT,
                                                         .REMARK = p.REMARK,
                                                         .CREATED_DATE = p.CREATED_DATE}


            If _filter.CONTENT <> "" Then
                query = query.Where(Function(f) f.CONTENT.ToUpper.Contains(_filter.CONTENT.ToUpper))
            End If
            If _filter.REMARK <> "" Then
                query = query.Where(Function(f) f.REMARK.ToUpper.Contains(_filter.REMARK.ToUpper))
            End If
            If _filter.CONFIRM_YEAR.HasValue Then
                query = query.Where(Function(f) f.CONFIRM_YEAR = _filter.CONFIRM_YEAR)
            End If
            If _filter.ASSESSMENT_YEAR.HasValue Then
                query = query.Where(Function(f) f.ASSESSMENT_YEAR = _filter.ASSESSMENT_YEAR)
            End If

            query = query.OrderBy(Sorts)
            Total = query.Count
            query = query.Skip(PageIndex * PageSize).Take(PageSize)

            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetCBAssessment(ByVal _id As Decimal) As CBAssessmentDTO

        Try



            Dim query = From p In Context.HU_CB_ASSESSMENT
                        Where p.ID = _id
                        Select New CBAssessmentDTO With {.ID = p.ID,
                                                         .CONFIRM_YEAR = p.CONFIRM_YEAR,
                                                         .ASSESSMENT_YEAR = p.ASSESSMENT_YEAR,
                                                         .CONTENT = p.CONTENT,
                                                         .REMARK = p.REMARK,
                                                         .CREATED_DATE = p.CREATED_DATE}


            Dim queryEmp = From pe In Context.HU_CB_ASSESSMENT_DTL
                           From p In Context.HU_CB_ASSESSMENT.Where(Function(f) f.ID = pe.CB_ASS_ID).DefaultIfEmpty
                           From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = pe.EMPLOYEE_ID).DefaultIfEmpty
                           From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                           From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                           From mt In Context.HU_TITLE_BLD.Where(Function(f) f.ID = e.MATHE).DefaultIfEmpty
                           From rs In Context.OT_OTHER_LIST.Where(Function(f) f.ID = pe.RESULT).DefaultIfEmpty
                           Where p.ID = _id
                           Select New CBAssessmentDtlDTO With {.ID = p.ID,
                                                               .CB_ASS_ID = pe.CB_ASS_ID,
                                                               .EMPLOYEE_ID = pe.EMPLOYEE_ID,
                                                               .EMPLOYEE_NAME = e.FULLNAME_VN,
                                                               .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                                                               .MATHE = e.MATHE,
                                                               .MATHE_NAME = mt.NAME_VN,
                                                               .TITLE_ID = e.TITLE_ID,
                                                               .TITLE_NAME = t.NAME_VN,
                                                               .ORG_ID = e.ORG_ID,
                                                               .ORG_NAME = o.NAME_VN,
                                                               .REMARK = pe.REMARK,
                                                               .RESULT = pe.RESULT,
                                                               .RESULT_NAME = rs.NAME_VN}

            Dim objCB = query.FirstOrDefault()
            If objCB IsNot Nothing Then
                objCB.lstDtl = queryEmp.ToList()
            End If
            Return objCB
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function DeleteCBAssessment(ByVal lstID As List(Of Decimal)) As Boolean
        Try
            Dim lstObj = (From p In Context.HU_CB_ASSESSMENT Where lstID.Contains(p.ID))
            For Each item In lstObj
                For Each itemEmp In (From p In Context.HU_CB_ASSESSMENT_DTL Where p.CB_ASS_ID = item.ID)
                    Context.HU_CB_ASSESSMENT_DTL.DeleteObject(itemEmp)
                Next
                Context.HU_CB_ASSESSMENT.DeleteObject(item)
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Return False
        End Try
    End Function

    Public Function ValidateCBAssessment(ByVal objCBAssessment As CBAssessmentDTO) As Boolean
        Try
            Dim query = From p In Context.HU_CB_ASSESSMENT Where p.ASSESSMENT_YEAR = objCBAssessment.ASSESSMENT_YEAR AndAlso p.ID <> objCBAssessment.ID

            Return query.Any
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Return False
        End Try
    End Function

    Public Function InsertCBAssessment(ByVal objCBAssessment As CBAssessmentDTO, ByVal log As UserLog) As Boolean
        Dim objCBAssessmentData As New HU_CB_ASSESSMENT
        Try
            objCBAssessmentData.ID = Utilities.GetNextSequence(Context, Context.HU_CB_ASSESSMENT.EntitySet.Name)
            objCBAssessmentData.CONFIRM_YEAR = objCBAssessment.CONFIRM_YEAR
            objCBAssessmentData.ASSESSMENT_YEAR = objCBAssessment.ASSESSMENT_YEAR
            objCBAssessmentData.CONTENT = objCBAssessment.CONTENT
            objCBAssessmentData.REMARK = objCBAssessment.REMARK
            Context.HU_CB_ASSESSMENT.AddObject(objCBAssessmentData)
            If objCBAssessment.lstDtl.Count > 0 Then
                For Each item In objCBAssessment.lstDtl
                    Dim objCBEmpData As New HU_CB_ASSESSMENT_DTL
                    objCBEmpData.ID = Utilities.GetNextSequence(Context, Context.HU_CB_ASSESSMENT_DTL.EntitySet.Name)
                    objCBEmpData.EMPLOYEE_ID = item.EMPLOYEE_ID
                    objCBEmpData.CB_ASS_ID = objCBAssessmentData.ID
                    objCBEmpData.REMARK = item.REMARK
                    objCBEmpData.RESULT = item.RESULT
                    Context.HU_CB_ASSESSMENT_DTL.AddObject(objCBEmpData)
                Next
            End If
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Return False
        End Try
    End Function

    Public Function ModifyCBAssessment(ByVal objCBAssessment As CBAssessmentDTO, ByVal log As UserLog) As Boolean
        Dim objCBAssessmentData As New HU_CB_ASSESSMENT
        Try
            objCBAssessmentData = (From p In Context.HU_CB_ASSESSMENT Where p.ID = objCBAssessment.ID).FirstOrDefault
            objCBAssessmentData.CONFIRM_YEAR = objCBAssessment.CONFIRM_YEAR
            objCBAssessmentData.ASSESSMENT_YEAR = objCBAssessment.ASSESSMENT_YEAR
            objCBAssessmentData.CONTENT = objCBAssessment.CONTENT
            objCBAssessmentData.REMARK = objCBAssessment.REMARK
            Dim lstObjDtl = From p In Context.HU_CB_ASSESSMENT_DTL Where p.CB_ASS_ID = objCBAssessmentData.ID

            For Each item In lstObjDtl
                Context.HU_CB_ASSESSMENT_DTL.DeleteObject(item)
            Next
            If objCBAssessment.lstDtl.Count > 0 Then
                For Each item In objCBAssessment.lstDtl
                    Dim objCBEmpData As New HU_CB_ASSESSMENT_DTL
                    objCBEmpData.ID = Utilities.GetNextSequence(Context, Context.HU_CB_ASSESSMENT_DTL.EntitySet.Name)
                    objCBEmpData.EMPLOYEE_ID = item.EMPLOYEE_ID
                    objCBEmpData.CB_ASS_ID = objCBAssessmentData.ID
                    objCBEmpData.REMARK = item.REMARK
                    objCBEmpData.RESULT = item.RESULT
                    Context.HU_CB_ASSESSMENT_DTL.AddObject(objCBEmpData)
                Next
            End If
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Return False
        End Try
    End Function
#End Region

#Region "Emp NPT"

    Public Function GetEmployeeNPTs(ByVal _filter As FamilyDTO, ByVal PageIndex As Integer,
                                    ByVal PageSize As Integer,
                                    ByRef Total As Integer, ByVal _param As ParamDTO,
                                    Optional ByVal Sorts As String = "EMPLOYEE_CODE asc",
                                    Optional ByVal log As UserLog = Nothing) As List(Of FamilyDTO)

        Try

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using


            Dim query = From p In Context.HU_FAMILY Order By p.HU_EMPLOYEE.EMPLOYEE_CODE
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From ecv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = e.ID).DefaultIfEmpty
                        From b In Context.HU_BANK.Where(Function(f) f.ID = ecv.BANK_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From re In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.RELATION_ID).DefaultIfEmpty
                        From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = e.ORG_ID And
                                                                       f.USERNAME = log.Username.ToUpper)
                        Where e.JOIN_DATE IsNot Nothing


            ' lọc điều kiện
            Dim dateNow = Date.Now.Date
            Dim terID = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID

            If Not _filter.IS_TER Then
                query = query.Where(Function(p) Not p.e.WORK_STATUS.HasValue Or
                                        (p.e.WORK_STATUS.HasValue And
                                         ((p.e.WORK_STATUS <> terID) Or (p.e.WORK_STATUS = terID And p.e.TER_EFFECT_DATE > dateNow))))

            End If
            If _filter.EMPLOYEE_CODE <> "" Then
                query = query.Where(Function(p) p.e.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If
            If _filter.EMPLOYEE_ID <> 0 Then
                query = query.Where(Function(p) p.e.ID = _filter.EMPLOYEE_ID)
            End If
            If _filter.EMPLOYEE_NAME <> "" Then
                query = query.Where(Function(p) p.e.FULLNAME_VN.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If
            If _filter.FROM_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.e.JOIN_DATE >= _filter.FROM_DATE)
            End If
            If _filter.TO_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.e.JOIN_DATE <= _filter.TO_DATE)
            End If
            If _filter.BIRTH_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.p.BIRTH_DATE = _filter.BIRTH_DATE)
            End If

            ' select thuộc tính
            Dim lstNpt = query.Select(Function(p) New FamilyDTO With {.ID = p.p.ID,
                                                                      .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                                                      .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                                                      .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                                                                      .ORG_NAME = p.o.NAME_VN,
                                                                      .ORG_DESC = p.o.DESCRIPTION_PATH,
                                                                      .TITLE_NAME = p.t.NAME_VN,
                                                                      .RELATION_ID = p.p.RELATION_ID,
                                                                      .RELATION_NAME = p.re.NAME_VN,
                                                                      .FULLNAME = p.p.FULLNAME,
                                                                      .BIRTH_DATE = p.p.BIRTH_DATE,
                                                                      .IS_DEDUCT = p.p.IS_DEDUCT,
                                                                      .TAXTATION = p.p.TAXTATION,
                                                                      .DEDUCT_FROM = p.p.DEDUCT_FROM,
                                                                      .DEDUCT_TO = p.p.DEDUCT_TO,
                                                                      .CREATED_DATE = p.p.CREATED_DATE,
                                                                      .BANK_NO = p.ecv.BANK_NO,
                                                                      .BANK_ID = p.ecv.BANK_ID,
                                                                      .BANK_NAME = p.b.NAME,
                                                                      .PERSON_INHERITANCE = p.ecv.PERSON_INHERITANCE})

            If _filter.ORG_NAME <> "" Then
                lstNpt = lstNpt.Where(Function(p) p.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If
            If _filter.TITLE_NAME <> "" Then
                lstNpt = lstNpt.Where(Function(p) p.TITLE_NAME.ToUpper.Contains(_filter.TITLE_NAME.ToUpper))
            End If
            If _filter.RELATION_NAME <> "" Then
                lstNpt = lstNpt.Where(Function(p) p.RELATION_NAME.ToUpper.Contains(_filter.RELATION_NAME.ToUpper))
            End If
            If _filter.FULLNAME <> "" Then
                lstNpt = lstNpt.Where(Function(p) p.FULLNAME.ToUpper.Contains(_filter.FULLNAME.ToUpper))
            End If
            If _filter.TAXTATION <> "" Then
                lstNpt = lstNpt.Where(Function(p) p.TAXTATION.ToUpper.Contains(_filter.TAXTATION.ToUpper))
            End If
            If _filter.BANK_NO <> "" Then
                lstNpt = lstNpt.Where(Function(p) p.BANK_NO.ToUpper.Contains(_filter.BANK_NO.ToUpper))
            End If
            If _filter.BANK_NAME <> "" Then
                lstNpt = lstNpt.Where(Function(p) p.BANK_NAME.ToUpper.Contains(_filter.BANK_NAME.ToUpper))
            End If
            If _filter.PERSON_INHERITANCE <> "" Then
                lstNpt = lstNpt.Where(Function(p) p.PERSON_INHERITANCE.ToUpper.Contains(_filter.PERSON_INHERITANCE.ToUpper))
            End If
            If _filter.DEDUCT_FROM IsNot Nothing Then
                lstNpt = lstNpt.Where(Function(p) p.DEDUCT_FROM = _filter.DEDUCT_FROM)
            End If
            If _filter.DEDUCT_TO IsNot Nothing Then
                lstNpt = lstNpt.Where(Function(p) p.DEDUCT_TO = _filter.DEDUCT_TO)
            End If

            lstNpt = lstNpt.OrderBy(Sorts)
            Total = lstNpt.Count
            lstNpt = lstNpt.Skip(PageIndex * PageSize).Take(PageSize)

            Return lstNpt.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function IMPORT_EMPPLOYEE_NPT(ByVal P_DOCXML As String, ByVal P_USER As String) As Boolean
        Try

            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PROFILE_BUSINESS.IMPORT_EMPPLOYEE_NPT",
                                               New With {.P_DOCXML = P_DOCXML,
                                                         .P_USER = P_USER,
                                                         .P_CUR = cls.OUT_CURSOR}, True)
                Return CBool(dtData(0)(0))
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Return False
        End Try

    End Function
#End Region

#Region "Stocks"
    Public Function GetStocks(ByVal _filter As StocksDTO, ByVal PageIndex As Integer,
                              ByVal PageSize As Integer,
                              ByRef Total As Integer, ByVal _param As ParamDTO,
                              Optional ByVal Sorts As String = "CREATED_DATE desc",
                              Optional ByVal log As UserLog = Nothing) As List(Of StocksDTO)

        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using
            Dim query = From p In Context.HU_STOCKS
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From l In Context.HU_LOCATION.Where(Function(f) f.ID = e.CONTRACTED_UNIT).DefaultIfEmpty
                        From ot In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.PAY_TYPE).DefaultIfEmpty
                        From ot2 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.STOCKS_TYPE).DefaultIfEmpty
                        From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = o.ID And
                                                                       f.USERNAME = log.Username.ToUpper)
                        Select New StocksDTO With {
                            .ID = p.ID,
                            .UPLOAD_FILE_NAME = p.UPLOAD_FILE_NAME,
                            .FILE_NAME = p.FILE_NAME,
                            .CODE = p.CODE,
                            .TITLE_NAME = t.NAME_VN,
                            .TITLE_ID = t.ID,
                            .TIME = p.TIME,
                            .STOCK_DEAL = p.STOCK_DEAL,
                            .STOCKS_TYPE_NAME = ot2.NAME_VN,
                            .STOCKS_TYPE = ot2.ID,
                            .STATE_DATE = e.JOIN_DATE_STATE,
                            .PERCENT = p.PERCENT,
                            .PAY_TYPE_NAME = ot.NAME_VN,
                            .PAY_TYPE = ot.ID,
                            .ORG_NAME = o.NAME_VN,
                            .ORG_ID = o.ID,
                            .ORG_DESC = o.HIERARCHICAL_PATH,
                            .NOTE = p.NOTE,
                            .MONTH = p.MONTH,
                            .MONEY_DEAL = p.MONEY_DEAL,
                            .LOCATION_NAME = l.NAME_VN,
                            .LOCATION_ID = l.ID,
                            .EMPLOYEE_NAME = e.FULLNAME_VN,
                            .EMPLOYEE_ID = e.ID,
                            .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                            .EFFECTED_DATE = p.EFFECTED_DATE,
                            .CREATED_DATE = p.CREATED_DATE}

            Dim lst = query

            If Not String.IsNullOrEmpty(_filter.CODE) Then
                lst = lst.Where(Function(p) p.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                lst = lst.Where(Function(p) p.TITLE_NAME.ToUpper.Contains(_filter.TITLE_NAME.ToUpper))
            End If
            If _filter.TIME.HasValue Then
                lst = lst.Where(Function(f) f.TIME = _filter.TIME)
            End If
            If _filter.STOCK_DEAL.HasValue Then
                lst = lst.Where(Function(f) f.STOCK_DEAL = _filter.STOCK_DEAL)
            End If
            If Not String.IsNullOrEmpty(_filter.STOCKS_TYPE_NAME) Then
                lst = lst.Where(Function(p) p.STOCKS_TYPE_NAME.ToUpper.Contains(_filter.STOCKS_TYPE_NAME.ToUpper))
            End If
            If _filter.STATE_DATE.HasValue Then
                lst = lst.Where(Function(f) f.STATE_DATE = _filter.STATE_DATE)
            End If
            If _filter.PERCENT.HasValue Then
                lst = lst.Where(Function(f) f.PERCENT = _filter.PERCENT)
            End If
            If Not String.IsNullOrEmpty(_filter.PAY_TYPE_NAME) Then
                lst = lst.Where(Function(p) p.PAY_TYPE_NAME.ToUpper.Contains(_filter.PAY_TYPE_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                lst = lst.Where(Function(p) p.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If
            If _filter.EFFECTED_DATE.HasValue Then
                lst = lst.Where(Function(f) f.EFFECTED_DATE = _filter.EFFECTED_DATE)
            End If
            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                lst = lst.Where(Function(p) p.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_NAME) Then
                lst = lst.Where(Function(p) p.EMPLOYEE_NAME.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.LOCATION_NAME) Then
                lst = lst.Where(Function(p) p.LOCATION_NAME.ToUpper.Contains(_filter.LOCATION_NAME.ToUpper))
            End If
            If _filter.MONEY_DEAL.HasValue Then
                lst = lst.Where(Function(f) f.MONEY_DEAL = _filter.MONEY_DEAL)
            End If
            If _filter.MONTH.HasValue Then
                lst = lst.Where(Function(f) f.MONTH = _filter.MONTH)
            End If
            If Not String.IsNullOrEmpty(_filter.NOTE) Then
                lst = lst.Where(Function(p) p.NOTE.ToUpper.Contains(_filter.NOTE.ToUpper))
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Dim rs = lst.ToList
            For Each item In rs
                item.EMPLOYEE_STOCK_CODE = item.CODE & " - " & item.EMPLOYEE_CODE & " - " & item.EMPLOYEE_NAME
            Next
            Return rs
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "GetStocks")
            Throw ex
        End Try
    End Function

    Public Function GetStocksByID(ByVal _filter As StocksDTO) As StocksDTO
        Try
            Dim query = From p In Context.HU_STOCKS
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From l In Context.HU_LOCATION.Where(Function(f) f.ID = e.CONTRACTED_UNIT).DefaultIfEmpty
                        From ot In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.PAY_TYPE).DefaultIfEmpty
                        From ot2 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.STOCKS_TYPE).DefaultIfEmpty
                        Where p.ID = _filter.ID
                        Select New StocksDTO With {
                            .ID = p.ID,
                            .UPLOAD_FILE_NAME = p.UPLOAD_FILE_NAME,
                            .FILE_NAME = p.FILE_NAME,
                            .CODE = p.CODE,
                            .TITLE_NAME = t.NAME_VN,
                            .TITLE_ID = t.ID,
                            .TIME = p.TIME,
                            .STOCK_DEAL = p.STOCK_DEAL,
                            .STOCKS_TYPE_NAME = ot2.NAME_VN,
                            .STOCKS_TYPE = ot2.ID,
                            .STATE_DATE = e.JOIN_DATE_STATE,
                            .PERCENT = p.PERCENT,
                            .PAY_TYPE_NAME = ot.NAME_VN,
                            .PAY_TYPE = ot.ID,
                            .ORG_NAME = o.NAME_VN,
                            .ORG_ID = o.ID,
                            .ORG_DESC = o.HIERARCHICAL_PATH,
                            .NOTE = p.NOTE,
                            .MONTH = p.MONTH,
                            .MONEY_DEAL = p.MONEY_DEAL,
                            .LOCATION_NAME = l.NAME_VN,
                            .LOCATION_ID = l.ID,
                            .EMPLOYEE_NAME = e.FULLNAME_VN,
                            .EMPLOYEE_ID = e.ID,
                            .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                            .EFFECTED_DATE = p.EFFECTED_DATE,
                            .CREATED_DATE = p.CREATED_DATE}
            Return query.FirstOrDefault
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "GetStocksByID")
            Throw ex
        End Try
    End Function
    Public Function InsertStocks(ByVal obj As StocksDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objData As New HU_STOCKS
        Try
            objData.ID = Utilities.GetNextSequence(Context, Context.HU_STOCKS.EntitySet.Name)
            objData.EFFECTED_DATE = obj.EFFECTED_DATE
            objData.EMPLOYEE_ID = obj.EMPLOYEE_ID
            objData.FILE_NAME = obj.FILE_NAME
            objData.UPLOAD_FILE_NAME = obj.UPLOAD_FILE_NAME
            objData.MONEY_DEAL = obj.MONEY_DEAL
            objData.MONTH = obj.MONTH
            objData.NOTE = obj.NOTE
            objData.PAY_TYPE = obj.PAY_TYPE
            objData.PERCENT = obj.PERCENT
            objData.STOCKS_TYPE = obj.STOCKS_TYPE
            objData.STOCK_DEAL = obj.STOCK_DEAL
            objData.TIME = obj.TIME
            objData.CODE = obj.CODE
            Context.HU_STOCKS.AddObject(objData)
            Context.SaveChanges(log)
            gID = objData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "InsertStocks")
            Throw ex
        End Try

    End Function

    Public Function ModifyStocks(ByVal obj As StocksDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objData As HU_STOCKS
        Try
            objData = (From p In Context.HU_STOCKS Where p.ID = obj.ID).FirstOrDefault
            objData.EFFECTED_DATE = obj.EFFECTED_DATE
            objData.EMPLOYEE_ID = obj.EMPLOYEE_ID
            objData.FILE_NAME = obj.FILE_NAME
            objData.UPLOAD_FILE_NAME = obj.UPLOAD_FILE_NAME
            objData.MONEY_DEAL = obj.MONEY_DEAL
            objData.MONTH = obj.MONTH
            objData.NOTE = obj.NOTE
            objData.PAY_TYPE = obj.PAY_TYPE
            objData.PERCENT = obj.PERCENT
            objData.STOCKS_TYPE = obj.STOCKS_TYPE
            objData.STOCK_DEAL = obj.STOCK_DEAL
            objData.TIME = obj.TIME
            objData.CODE = obj.CODE
            Context.SaveChanges(log)
            gID = objData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "ModifyStocks")
            Throw ex
        End Try

    End Function

    Public Function ValidateStocks(ByVal obj As StocksDTO) As Boolean
        Try
            Dim query = From p In Context.HU_STOCKS Where p.ID <> obj.ID AndAlso p.EMPLOYEE_ID = obj.EMPLOYEE_ID AndAlso p.STOCKS_TYPE = obj.STOCKS_TYPE AndAlso
                         EntityFunctions.TruncateTime(p.EFFECTED_DATE) >= EntityFunctions.TruncateTime(obj.EFFECTED_DATE)
            Return query.Any
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "ValidateStocks")
            Throw ex
        End Try
    End Function

    Public Function ValidateStocksGenerate(ByVal obj As StocksDTO) As Boolean
        Try
            Dim query = From p In Context.HU_STOCKS_TRANSACTION Where p.STOCK_ID = obj.ID
            Return query.Any
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "ValidateStocksGenerate")
            Throw ex
        End Try
    End Function

    Public Function DeleteStocks(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim lstItems As List(Of HU_STOCKS)
        Try

            lstItems = (From p In Context.HU_STOCKS Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstItems.Count - 1
                Context.HU_STOCKS.DeleteObject(lstItems(index))
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "DeleteStocks")
            Throw ex
        End Try

    End Function

#End Region

#Region "Stocks Transaction"
    Public Function GetStocksTransaction(ByVal _filter As StocksTransactionDTO, ByVal PageIndex As Integer,
                                          ByVal PageSize As Integer,
                                          ByRef Total As Integer, ByVal _param As ParamDTO,
                                          Optional ByVal Sorts As String = "CREATED_DATE desc",
                                          Optional ByVal log As UserLog = Nothing) As List(Of StocksTransactionDTO)

        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using
            Dim query = From s In Context.HU_STOCKS_TRANSACTION
                        From p In Context.HU_STOCKS.Where(Function(f) f.ID = s.STOCK_ID).DefaultIfEmpty
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From l In Context.HU_LOCATION.Where(Function(f) f.ID = e.CONTRACTED_UNIT).DefaultIfEmpty
                        From ot In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.PAY_TYPE).DefaultIfEmpty
                        From ot2 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.STOCKS_TYPE).DefaultIfEmpty
                        From ot3 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = s.PAY_TYPE).DefaultIfEmpty
                        From st In Context.OT_OTHER_LIST.Where(Function(f) f.ID = s.STATUS).DefaultIfEmpty
                        Select New StocksTransactionDTO With {
                            .ID = s.ID,
                            .UPLOAD_FILE_NAME = s.UPLOAD_FILE_NAME,
                            .FILE_NAME = s.FILE_NAME,
                            .CODE = s.CODE,
                            .STOCK_CODE = p.CODE,
                            .TITLE_NAME = t.NAME_VN,
                            .TITLE_ID = t.ID,
                            .ORG_NAME = o.NAME_VN,
                            .ORG_ID = o.ID,
                            .ORG_DESC = o.HIERARCHICAL_PATH,
                            .LOCATION_NAME = l.NAME_VN,
                            .LOCATION_ID = l.ID,
                            .EMPLOYEE_NAME = e.FULLNAME_VN,
                            .EMPLOYEE_ID = e.ID,
                            .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                            .NOTE = s.NOTE,
                            .PAY_DATE = s.PAY_DATE,
                            .PAY_TYPE = s.PAY_TYPE,
                            .PAY_TYPE_NAME = ot3.NAME_VN,
                            .PROBATION_MONTHS = s.PROBATION_MONTHS,
                            .PROBATION_PERCENT = s.PROBATION_PERCENT,
                            .PROBATION_STOCK = s.PROBATION_STOCK,
                            .STOCK_FINAL_PRICE = s.STOCK_FINAL_PRICE,
                            .STOCK_ID = s.STOCK_ID,
                            .STOCK_LEFT = s.STOCK_LEFT,
                            .STOCK_PAY = s.STOCK_PAY,
                            .STOCK_PRICE = s.STOCK_PRICE,
                            .STOCK_TOTAL = s.STOCK_TOTAL,
                            .STOCK_TOTAL_ROUND = s.STOCK_TOTAL_ROUND,
                            .TRADE_DATE = s.TRADE_DATE,
                            .TRADE_MONTH = s.TRADE_MONTH,
                            .CREATED_DATE = s.CREATED_DATE,
                            .STATUS = s.STATUS,
                            .STATUS_NAME = st.NAME_VN,
                            .STATUS_CODE = st.CODE,
                            .stock = New StocksDTO With {
                            .ID = p.ID,
                            .UPLOAD_FILE_NAME = p.UPLOAD_FILE_NAME,
                            .FILE_NAME = p.FILE_NAME,
                            .CODE = p.CODE,
                            .TITLE_NAME = t.NAME_VN,
                            .TITLE_ID = t.ID,
                            .TIME = p.TIME,
                            .STOCK_DEAL = p.STOCK_DEAL,
                            .STOCKS_TYPE_NAME = ot2.NAME_VN,
                            .STOCKS_TYPE = ot2.ID,
                            .STATE_DATE = e.JOIN_DATE_STATE,
                            .PERCENT = p.PERCENT,
                            .PAY_TYPE_NAME = ot.NAME_VN,
                            .PAY_TYPE = ot.ID,
                            .ORG_NAME = o.NAME_VN,
                            .ORG_ID = o.ID,
                            .ORG_DESC = o.HIERARCHICAL_PATH,
                            .NOTE = p.NOTE,
                            .MONTH = p.MONTH,
                            .MONEY_DEAL = p.MONEY_DEAL,
                            .LOCATION_NAME = l.NAME_VN,
                            .LOCATION_ID = l.ID,
                            .EMPLOYEE_NAME = e.FULLNAME_VN,
                            .EMPLOYEE_ID = e.ID,
                            .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                            .EFFECTED_DATE = p.EFFECTED_DATE,
                            .CREATED_DATE = p.CREATED_DATE}}

            Dim lst = query

            If Not String.IsNullOrEmpty(_filter.CODE) Then
                lst = lst.Where(Function(p) p.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
            End If
            If _filter.ID > 0 Then
                Dim StockID = lst.Where(Function(f) f.ID = _filter.ID).Select(Function(p) p.STOCK_ID).FirstOrDefault()
                If StockID IsNot Nothing Then
                    lst = lst.Where(Function(f) f.STOCK_ID = StockID)
                End If
            End If
            If _filter.PAY_DATE.HasValue Then
                query = query.Where(Function(f) f.PAY_DATE = _filter.PAY_DATE)
            End If
            If Not String.IsNullOrEmpty(_filter.PAY_TYPE_NAME) Then
                lst = lst.Where(Function(p) p.PAY_TYPE_NAME.ToUpper.Contains(_filter.PAY_TYPE_NAME.ToUpper))
            End If
            If _filter.PROBATION_MONTHS.HasValue Then
                lst = lst.Where(Function(f) f.PROBATION_MONTHS = _filter.PROBATION_MONTHS)
            End If
            If _filter.PROBATION_PERCENT.HasValue Then
                lst = lst.Where(Function(f) f.PROBATION_PERCENT = _filter.PROBATION_PERCENT)
            End If
            If _filter.STOCK_ID.HasValue Then
                lst = lst.Where(Function(f) f.STOCK_ID = _filter.STOCK_ID)
            End If
            If _filter.PROBATION_STOCK.HasValue Then
                lst = lst.Where(Function(f) f.PROBATION_STOCK = _filter.PROBATION_STOCK)
            End If
            If _filter.STOCK_FINAL_PRICE.HasValue Then
                lst = lst.Where(Function(f) f.STOCK_FINAL_PRICE = _filter.STOCK_FINAL_PRICE)
            End If
            If _filter.STOCK_TOTAL.HasValue Then
                lst = lst.Where(Function(f) f.STOCK_TOTAL = _filter.STOCK_TOTAL)
            End If
            If _filter.STOCK_PAY.HasValue Then
                lst = lst.Where(Function(f) f.STOCK_PAY = _filter.STOCK_PAY)
            End If
            If _filter.STOCK_PRICE.HasValue Then
                lst = lst.Where(Function(f) f.STOCK_PRICE = _filter.STOCK_PRICE)
            End If
            If _filter.STOCK_TOTAL_ROUND.HasValue Then
                lst = lst.Where(Function(f) f.STOCK_TOTAL_ROUND = _filter.STOCK_TOTAL_ROUND)
            End If
            If _filter.TRADE_DATE.HasValue Then
                lst = lst.Where(Function(f) f.TRADE_DATE = _filter.TRADE_DATE)
            End If
            If _filter.TRADE_MONTH.HasValue Then
                lst = lst.Where(Function(f) f.TRADE_MONTH = _filter.TRADE_MONTH)
            End If
            If Not String.IsNullOrEmpty(_filter.NOTE) Then
                lst = lst.Where(Function(p) p.NOTE.ToUpper.Contains(_filter.NOTE.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.FILE_NAME) Then
                lst = lst.Where(Function(p) p.FILE_NAME.ToUpper.Contains(_filter.FILE_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.STATUS_NAME) Then
                lst = lst.Where(Function(p) p.STATUS_NAME.ToUpper.Contains(_filter.STATUS_NAME.ToUpper))
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "GetStocksTransaction")
            Throw ex
        End Try
    End Function

    Public Function InsertStocksTransaction(ByVal obj As StocksTransactionDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objData As New HU_STOCKS_TRANSACTION
        Try
            objData.ID = Utilities.GetNextSequence(Context, Context.HU_STOCKS_TRANSACTION.EntitySet.Name)
            objData.CODE = obj.CODE
            objData.FILE_NAME = obj.FILE_NAME
            objData.NOTE = obj.NOTE
            objData.PAY_DATE = obj.PAY_DATE
            objData.PAY_TYPE = obj.PAY_TYPE
            objData.PROBATION_MONTHS = obj.PROBATION_MONTHS
            objData.PROBATION_PERCENT = obj.PROBATION_PERCENT
            objData.PROBATION_STOCK = obj.PROBATION_STOCK
            objData.STOCK_FINAL_PRICE = obj.STOCK_FINAL_PRICE
            objData.STOCK_ID = obj.STOCK_ID
            objData.STOCK_LEFT = obj.STOCK_LEFT
            objData.STOCK_PAY = obj.STOCK_PAY
            objData.STOCK_PRICE = obj.STOCK_PRICE
            objData.STOCK_TOTAL = obj.STOCK_TOTAL
            objData.STOCK_TOTAL_ROUND = obj.STOCK_TOTAL_ROUND
            objData.TRADE_DATE = obj.TRADE_DATE
            objData.TRADE_MONTH = obj.TRADE_MONTH
            objData.UPLOAD_FILE_NAME = obj.UPLOAD_FILE_NAME
            objData.STATUS = obj.STATUS
            Context.HU_STOCKS_TRANSACTION.AddObject(objData)
            Context.SaveChanges(log)
            gID = objData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "InsertStocksTransaction")
            Throw ex
        End Try

    End Function

    Public Function ModifyStocksTransaction(ByVal obj As StocksTransactionDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objData As HU_STOCKS_TRANSACTION
        Try
            objData = (From p In Context.HU_STOCKS_TRANSACTION Where p.ID = obj.ID).FirstOrDefault
            objData.CODE = obj.CODE
            objData.FILE_NAME = obj.FILE_NAME
            objData.NOTE = obj.NOTE
            objData.PAY_DATE = obj.PAY_DATE
            objData.PAY_TYPE = obj.PAY_TYPE
            objData.PROBATION_MONTHS = obj.PROBATION_MONTHS
            objData.PROBATION_PERCENT = obj.PROBATION_PERCENT
            objData.PROBATION_STOCK = obj.PROBATION_STOCK
            objData.STOCK_FINAL_PRICE = obj.STOCK_FINAL_PRICE
            objData.STOCK_ID = obj.STOCK_ID
            objData.STOCK_LEFT = obj.STOCK_LEFT
            objData.STOCK_PAY = obj.STOCK_PAY
            objData.STOCK_PRICE = obj.STOCK_PRICE
            objData.STOCK_TOTAL = obj.STOCK_TOTAL
            objData.STOCK_TOTAL_ROUND = obj.STOCK_TOTAL_ROUND
            objData.TRADE_DATE = obj.TRADE_DATE
            objData.TRADE_MONTH = obj.TRADE_MONTH
            objData.UPLOAD_FILE_NAME = obj.UPLOAD_FILE_NAME
            objData.STATUS = obj.STATUS
            Context.SaveChanges(log)
            gID = objData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "ModifyStocksTransaction")
            Throw ex
        End Try

    End Function

    Public Function ValidateStocksTransactionStatus(ByVal obj As StocksTransactionDTO) As Boolean
        Try
            Dim query = From p In Context.HU_STOCKS_TRANSACTION
                        From st In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.STATUS).DefaultIfEmpty
                        Where p.ID <> obj.ID AndAlso p.STOCK_ID = obj.STOCK_ID AndAlso st.CODE = "COMPLETE" _
                              AndAlso EntityFunctions.TruncateTime(p.TRADE_DATE) > EntityFunctions.TruncateTime(obj.TRADE_DATE)
            Return query.Any
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "ValidateStocksTransactionStatus")
            Throw ex
        End Try
    End Function
    Public Function ValidateStocksTransactionBefore(ByVal obj As StocksTransactionDTO) As Boolean
        Try
            Dim query = From p In Context.HU_STOCKS_TRANSACTION
                        From st In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.STATUS).DefaultIfEmpty
                        Where p.ID <> obj.ID AndAlso p.STOCK_ID = obj.STOCK_ID AndAlso st.CODE = "INCOMPLETE" _
                              AndAlso EntityFunctions.TruncateTime(p.TRADE_DATE) < EntityFunctions.TruncateTime(obj.TRADE_DATE)
            Return query.Any
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "ValidateStocksTransactionStatus")
            Throw ex
        End Try
    End Function

    Public Function ValidateStocksTransaction(ByVal obj As StocksTransactionDTO) As Boolean
        Try
            Dim query = From p In Context.HU_STOCKS_TRANSACTION Where p.STOCK_ID = obj.STOCK_ID AndAlso
                         EntityFunctions.TruncateTime(p.TRADE_DATE) >= EntityFunctions.TruncateTime(obj.TRADE_DATE)
            Return query.Any
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "ValidateStocksTransaction")
            Throw ex
        End Try
    End Function

    Public Function DeleteStocksTransaction(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim lstItems As List(Of HU_STOCKS_TRANSACTION)
        Try

            lstItems = (From p In Context.HU_STOCKS_TRANSACTION Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstItems.Count - 1
                Context.HU_STOCKS_TRANSACTION.DeleteObject(lstItems(index))
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "DeleteStocksTransaction")
            Throw ex
        End Try

    End Function

#End Region
    Public Function CheckHROrgPermission(ByVal username As String, ByVal empID As Decimal) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = username,
                                           .P_ORGID = 1,
                                           .P_ISDISSOLVE = 0})
            End Using
            Dim rs = (From p In Context.HU_EMPLOYEE
                      From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = p.ORG_ID And
                                                                       f.USERNAME = username.ToUpper)
                      Where p.ID = empID).Any
            Return rs
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
End Class
