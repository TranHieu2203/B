Imports System.Data.Objects
Imports System.Reflection
Imports Framework.Data
Imports Framework.Data.System.Linq.Dynamic

Partial Class ProfileRepository

#Region "HU_CERTIFICATE_edit"
    Public Function GetCertificateEdit(ByVal _filter As HU_PRO_TRAIN_OUT_COMPANYDTOEDIT) As List(Of HU_PRO_TRAIN_OUT_COMPANYDTOEDIT)
        Dim query As ObjectQuery(Of HU_PRO_TRAIN_OUT_COMPANYDTOEDIT)
        Try
            query = (From p In Context.HU_PRO_TRAIN_OUT_COMPANY_EDIT
                     From ot In Context.OT_OTHER_LIST.Where(Function(F) F.ID = p.FORM_TRAIN_ID).DefaultIfEmpty
                     From OT1 In Context.OT_OTHER_LIST.Where(Function(F) F.ID = p.CERTIFICATE).DefaultIfEmpty
                     From OT2 In Context.OT_OTHER_LIST.Where(Function(F) F.ID = p.LEVEL_ID).DefaultIfEmpty
                     Select New HU_PRO_TRAIN_OUT_COMPANYDTOEDIT With {
                         .ID = p.ID,
                        .EMPLOYEE_ID = p.EMPLOYEE_ID,
                        .FROM_DATE = p.FROM_DATE,
                        .TO_DATE = p.TO_DATE,
                         .RECEIVE_DEGREE_DATE = p.RECEIVE_DEGREE_DATE,
                        .YEAR_GRA = p.YEAR_GRA,
                        .NAME_SHOOLS = p.NAME_SHOOLS,
                        .FORM_TRAIN_ID = p.FORM_TRAIN_ID,
                        .FORM_TRAIN_NAME = ot.NAME_VN,
                         .LEVEL_ID = p.LEVEL_ID,
                         .LEVEL_NAME = OT2.NAME_VN,
                         .SCORE = p.SCORE,
                         .CONTENT_TRAIN = p.CONTENT_TRAIN,
                         .REMARK = p.REMARK,
                         .CODE_CERTIFICATE = p.CODE_CERTIFICATE,
                        .SPECIALIZED_TRAIN = p.SPECIALIZED_TRAIN,
                        .RESULT_TRAIN = p.RESULT_TRAIN,
                        .CERTIFICATE = OT1.NAME_VN,
                        .CERTIFICATE_ID = p.CERTIFICATE,
                        .EFFECTIVE_DATE_FROM = p.EFFECTIVE_DATE_FROM,
                        .EFFECTIVE_DATE_TO = p.EFFECTIVE_DATE_TO,
                        .CREATED_BY = p.CREATED_BY,
                         .IS_RENEWED = p.IS_RENEW,
                         .TYPE_TRAIN_ID = p.TYPE_TRAIN_ID,
                         .TYPE_TRAIN_NAME = p.TYPE_TRAIN_NAME,
                        .CREATED_DATE = p.CREATED_DATE,
                        .CREATED_LOG = p.CREATED_LOG,
                        .MODIFIED_BY = p.MODIFIED_BY,
                        .MODIFIED_DATE = p.MODIFIED_DATE,
                        .MODIFIED_LOG = p.MODIFIED_LOG,
                        .REASON_UNAPROVE = p.REASON_UNAPROVE,
                          .RENEWED_NAME = If(p.IS_RENEW = 0, "Không", "Có"),
                        .FK_PKEY = p.FK_PKEY,
                         .UPLOAD_FILE = p.UPLOAD_FILE,
                         .FILE_NAME = p.FILE_NAME,
                        .STATUS = p.STATUS,
                        .STATUS_NAME = If(p.STATUS = 0, "Chưa gửi duyệt",
                                           If(p.STATUS = 1, "Chờ phê duyệt",
                                              If(p.STATUS = 2, "Phê duyệt",
                                                 If(p.STATUS = 3, "Không phê duyệt", ""))))})

            If _filter.EMPLOYEE_ID <> 0 Then
                query = query.Where(Function(p) p.EMPLOYEE_ID = _filter.EMPLOYEE_ID)
            End If
            If _filter.STATUS <> "" Then
                query = query.Where(Function(p) p.STATUS = _filter.STATUS)
            End If
            If _filter.ID <> 0 Then
                query = query.Where(Function(p) p.ID = _filter.ID)
            End If
            Return query.ToList
        Catch ex As Exception

        End Try
    End Function
    Public Function InsertCertificateEdit(ByVal objCertificateEdit As HU_PRO_TRAIN_OUT_COMPANYDTOEDIT,
                                             ByVal log As UserLog,
                                             ByRef gID As Decimal) As Boolean
        Try

            Dim objCertificatetData As New HU_PRO_TRAIN_OUT_COMPANY_EDIT
            objCertificatetData.ID = Utilities.GetNextSequence(Context, Context.HU_PRO_TRAIN_OUT_COMPANY_EDIT.EntitySet.Name)
            objCertificatetData.FROM_DATE = objCertificateEdit.FROM_DATE
            objCertificatetData.TO_DATE = objCertificateEdit.TO_DATE
            objCertificatetData.YEAR_GRA = objCertificateEdit.YEAR_GRA
            objCertificatetData.NAME_SHOOLS = objCertificateEdit.NAME_SHOOLS
            objCertificatetData.UPLOAD_FILE = objCertificateEdit.UPLOAD_FILE
            objCertificatetData.FILE_NAME = objCertificateEdit.FILE_NAME
            objCertificatetData.FORM_TRAIN_ID = objCertificateEdit.FORM_TRAIN_ID
            objCertificatetData.SPECIALIZED_TRAIN = objCertificateEdit.SPECIALIZED_TRAIN
            objCertificatetData.RESULT_TRAIN = objCertificateEdit.RESULT_TRAIN
            objCertificatetData.CERTIFICATE = objCertificateEdit.CERTIFICATE
            objCertificatetData.EFFECTIVE_DATE_FROM = objCertificateEdit.EFFECTIVE_DATE_FROM
            objCertificatetData.EFFECTIVE_DATE_TO = objCertificateEdit.EFFECTIVE_DATE_TO
            objCertificatetData.EMPLOYEE_ID = objCertificateEdit.EMPLOYEE_ID
            objCertificatetData.SCORE = objCertificateEdit.SCORE
            objCertificatetData.CONTENT_TRAIN = objCertificateEdit.CONTENT_TRAIN
            objCertificatetData.TYPE_TRAIN_NAME = objCertificateEdit.TYPE_TRAIN_NAME
            objCertificatetData.CODE_CERTIFICATE = objCertificateEdit.CODE_CERTIFICATE
            objCertificatetData.REMARK = objCertificateEdit.REMARK
            objCertificatetData.LEVEL_ID = objCertificateEdit.LEVEL_ID
            objCertificatetData.TYPE_TRAIN_ID = objCertificateEdit.TYPE_TRAIN_ID
            objCertificatetData.FK_PKEY = objCertificateEdit.FK_PKEY
            objCertificatetData.STATUS = 0
            objCertificatetData.RECEIVE_DEGREE_DATE = objCertificateEdit.RECEIVE_DEGREE_DATE
            objCertificatetData.IS_RENEW = objCertificateEdit.IS_RENEWED
            Context.HU_PRO_TRAIN_OUT_COMPANY_EDIT.AddObject(objCertificatetData)
            Context.SaveChanges(log)
            gID = objCertificatetData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function ModifyCertificateEdit(ByVal objCertificateEdit As HU_PRO_TRAIN_OUT_COMPANYDTOEDIT,
                                             ByVal log As UserLog,
                                             ByRef gID As Decimal) As Boolean
        Dim objCertificatetData As New HU_PRO_TRAIN_OUT_COMPANY_EDIT With {.ID = objCertificateEdit.ID}
        Try
            objCertificatetData = (From p In Context.HU_PRO_TRAIN_OUT_COMPANY_EDIT Where p.ID = objCertificateEdit.ID).FirstOrDefault
            objCertificatetData.ID = objCertificateEdit.ID
            objCertificatetData.FROM_DATE = objCertificateEdit.FROM_DATE
            objCertificatetData.TO_DATE = objCertificateEdit.TO_DATE
            objCertificatetData.UPLOAD_FILE = objCertificateEdit.UPLOAD_FILE
            objCertificatetData.FILE_NAME = objCertificateEdit.FILE_NAME
            objCertificatetData.YEAR_GRA = objCertificateEdit.YEAR_GRA
            objCertificatetData.NAME_SHOOLS = objCertificateEdit.NAME_SHOOLS
            objCertificatetData.FORM_TRAIN_ID = objCertificateEdit.FORM_TRAIN_ID
            objCertificatetData.SPECIALIZED_TRAIN = objCertificateEdit.SPECIALIZED_TRAIN
            objCertificatetData.RESULT_TRAIN = objCertificateEdit.RESULT_TRAIN
            objCertificatetData.CERTIFICATE = objCertificateEdit.CERTIFICATE
            objCertificatetData.EFFECTIVE_DATE_FROM = objCertificateEdit.EFFECTIVE_DATE_FROM
            objCertificatetData.EFFECTIVE_DATE_TO = objCertificateEdit.EFFECTIVE_DATE_TO
            objCertificatetData.EMPLOYEE_ID = objCertificateEdit.EMPLOYEE_ID
            objCertificatetData.SCORE = objCertificateEdit.SCORE
            objCertificatetData.CONTENT_TRAIN = objCertificateEdit.CONTENT_TRAIN
            objCertificatetData.TYPE_TRAIN_NAME = objCertificateEdit.TYPE_TRAIN_NAME
            objCertificatetData.CODE_CERTIFICATE = objCertificateEdit.CODE_CERTIFICATE
            objCertificatetData.LEVEL_ID = objCertificateEdit.LEVEL_ID
            objCertificatetData.REMARK = objCertificateEdit.REMARK
            objCertificatetData.TYPE_TRAIN_ID = objCertificateEdit.TYPE_TRAIN_ID
            objCertificatetData.FK_PKEY = objCertificateEdit.FK_PKEY
            objCertificatetData.STATUS = 0
            objCertificatetData.RECEIVE_DEGREE_DATE = objCertificateEdit.RECEIVE_DEGREE_DATE
            objCertificatetData.IS_RENEW = objCertificateEdit.IS_RENEWED
            Context.SaveChanges(log)
            gID = objCertificatetData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function CheckExistCertificateEdit(ByVal pk_key As Decimal) As HU_PRO_TRAIN_OUT_COMPANYDTOEDIT
        Try
            Dim query = (From p In Context.HU_PRO_TRAIN_OUT_COMPANY_EDIT
                         Where p.STATUS <> 2 And p.FK_PKEY = pk_key
                         Select New HU_PRO_TRAIN_OUT_COMPANYDTOEDIT With {
                             .ID = p.ID,
                             .STATUS = p.STATUS}).FirstOrDefault

            Return query
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
#End Region
#Region "HU_CERTIFICATE"
    Public Function SendCertificateEdit(ByVal lstID As List(Of Decimal),
                                           ByVal log As UserLog) As Boolean
        Try
            Dim lstObj = (From p In Context.HU_PRO_TRAIN_OUT_COMPANY_EDIT Where lstID.Contains(p.ID)).ToList
            For Each item In lstObj
                item.STATUS = 1
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    'get dl bang hu_certificate
    Public Function GetCertificate(ByVal _filter As CETIFICATEDTO) As List(Of CETIFICATEDTO)
        Dim query As ObjectQuery(Of CETIFICATEDTO)
        Try
            query = (From p In Context.HU_CERTIFICATE
                     From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID).DefaultIfEmpty
                     From ot In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.FIELD_TRAIN).DefaultIfEmpty
                     From ot1 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.MAJOR).DefaultIfEmpty
                     From ot2 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.LEVEL_TRAIN).DefaultIfEmpty
                     Select New CETIFICATEDTO With {
                         .ID = p.ID,
                         .FIELD = p.FIELD_TRAIN,
                         .FIELD_NAME = ot.NAME_VN,
                         .FROM_DATE = p.FROM_DATE,
                         .TO_DATE = p.TO_DATE,
                         .SCHOOL_NAME = p.SCHOOL_NAME,
                         .MAJOR = p.MAJOR,
                         .MAJOR_NAME = ot1.NAME_VN,
                         .LEVEL = p.LEVEL_TRAIN,
                         .LEVEL_NAME = ot2.NAME_VN,
                         .MARK = p.MARK,
                         .CONTENT_NAME = p.CONTENT_TRAIN,
                         .TYPE_NAME = p.TYPE_TRAIN,
                         .CODE_CERTIFICATE = p.CODE_CETIFICATE,
                         .EFFECT_FROM = p.EFFECT_FROM,
                         .EFFECT_TO = p.EFFECT_TO,
                         .CLASSIFICATION = p.CLASSIFICATION,
                         .YEAR = p.YEAR,
                         .REMARK = p.REMARK,
                         .RENEW = p.RENEW,
                         .UPLOAD = p.UPLOAD_FILE,
                         .FILENAME = p.FILE_NAME
                         })
            Return query.ToList
        Catch ex As Exception

        End Try
    End Function
#End Region
#Region "Family"

    Public Function InsertEmployeeFamily(ByVal objFamily As FamilyDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Try

            Dim objFamilyData As New HU_FAMILY
            objFamilyData.ID = Utilities.GetNextSequence(Context, Context.HU_FAMILY.EntitySet.Name)
            objFamilyData.EMPLOYEE_ID = objFamily.EMPLOYEE_ID
            objFamilyData.FULLNAME = objFamily.FULLNAME
            objFamilyData.RELATION_ID = objFamily.RELATION_ID
            objFamilyData.PROVINCE_ID = objFamily.PROVINCE_ID
            objFamilyData.CAREER = objFamily.CAREER
            objFamilyData.IS_SAME_COMPANY = objFamily.IS_SAME_COMPANY
            objFamilyData.TITLE_NAME = objFamily.TITLE_NAME
            objFamilyData.BIRTH_DATE = objFamily.BIRTH_DATE
            objFamilyData.DEDUCT_REG = objFamily.DEDUCT_REG
            objFamilyData.ID_NO = objFamily.ID_NO
            objFamilyData.TAXTATION = objFamily.TAXTATION
            objFamilyData.IS_DEDUCT = objFamily.IS_DEDUCT
            objFamilyData.DEDUCT_FROM = objFamily.DEDUCT_FROM
            objFamilyData.DEDUCT_TO = objFamily.DEDUCT_TO
            objFamilyData.REMARK = objFamily.REMARK
            objFamilyData.ADDRESS = objFamily.ADDRESS
            objFamilyData.ADDRESS_TT = objFamily.ADDRESS_TT
            objFamilyData.AD_DISTRICT_ID = objFamily.AD_DISTRICT_ID
            objFamilyData.AD_PROVINCE_ID = objFamily.AD_PROVINCE_ID
            objFamilyData.AD_VILLAGE = objFamily.AD_VILLAGE
            objFamilyData.AD_WARD_ID = objFamily.AD_WARD_ID
            objFamilyData.TT_DISTRICT_ID = objFamily.TT_DISTRICT_ID
            objFamilyData.TT_PROVINCE_ID = objFamily.TT_PROVINCE_ID
            objFamilyData.TT_WARD_ID = objFamily.TT_WARD_ID
            objFamilyData.IS_OWNER = objFamily.IS_OWNER
            objFamilyData.IS_PASS = objFamily.IS_PASS
            objFamilyData.CERTIFICATE_CODE = objFamily.CERTIFICATE_CODE
            objFamilyData.CERTIFICATE_NUM = objFamily.CERTIFICATE_NUM

            objFamilyData.NATION_ID = objFamily.NATION_ID
            objFamilyData.ID_NO_DATE = objFamily.ID_NO_DATE
            objFamilyData.ID_NO_PLACE_NAME = objFamily.ID_NO_PLACE_NAME
            objFamilyData.PHONE = objFamily.PHONE
            objFamilyData.TAXTATION_DATE = objFamily.TAXTATION_DATE
            objFamilyData.TAXTATION_PLACE = objFamily.TAXTATION_PLACE
            objFamilyData.BIRTH_CODE = objFamily.BIRTH_CODE
            objFamilyData.QUYEN = objFamily.QUYEN
            objFamilyData.BIRTH_NATION_ID = objFamily.BIRTH_NATION_ID
            objFamilyData.BIRTH_PROVINCE_ID = objFamily.BIRTH_PROVINCE_ID
            objFamilyData.BIRTH_DISTRICT_ID = objFamily.BIRTH_DISTRICT_ID
            objFamilyData.BIRTH_WARD_ID = objFamily.BIRTH_WARD_ID
            objFamilyData.GENDER = objFamily.GENDER
            objFamilyData.RELATE_OWNER = objFamily.RELATE_OWNER
            objFamilyData.NATIVE = objFamily.NATIVE
            objFamilyData.FILE_FAMILY = objFamily.FILE_FAMILY
            objFamilyData.FILE_NPT = objFamily.FILE_NPT

            If objFamily.IS_OWNER Then
                Dim objEmpCv = (From p In Context.HU_EMPLOYEE_CV Where p.EMPLOYEE_ID = objFamily.EMPLOYEE_ID).FirstOrDefault
                objEmpCv.NO_HOUSEHOLDS = objFamily.CERTIFICATE_NUM
            End If

            Context.HU_FAMILY.AddObject(objFamilyData)
            Context.SaveChanges(log)
            gID = objFamilyData.ID
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function ModifyEmployeeFamily(ByVal objFamily As FamilyDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objFamilyData As New HU_FAMILY With {.ID = objFamily.ID}
        Try
            objFamilyData = (From p In Context.HU_FAMILY Where p.ID = objFamily.ID).FirstOrDefault
            objFamilyData.EMPLOYEE_ID = objFamily.EMPLOYEE_ID
            objFamilyData.FULLNAME = objFamily.FULLNAME
            objFamilyData.RELATION_ID = objFamily.RELATION_ID
            objFamilyData.PROVINCE_ID = objFamily.PROVINCE_ID
            objFamilyData.CAREER = objFamily.CAREER
            objFamilyData.IS_SAME_COMPANY = objFamily.IS_SAME_COMPANY
            objFamilyData.TITLE_NAME = objFamily.TITLE_NAME
            objFamilyData.BIRTH_DATE = objFamily.BIRTH_DATE
            objFamilyData.DEDUCT_REG = objFamily.DEDUCT_REG
            objFamilyData.ID_NO = objFamily.ID_NO
            objFamilyData.TAXTATION = objFamily.TAXTATION
            objFamilyData.IS_DEDUCT = objFamily.IS_DEDUCT
            objFamilyData.DEDUCT_FROM = objFamily.DEDUCT_FROM
            objFamilyData.DEDUCT_TO = objFamily.DEDUCT_TO
            objFamilyData.REMARK = objFamily.REMARK
            objFamilyData.ADDRESS = objFamily.ADDRESS
            objFamilyData.ADDRESS_TT = objFamily.ADDRESS_TT
            objFamilyData.AD_DISTRICT_ID = objFamily.AD_DISTRICT_ID
            objFamilyData.AD_PROVINCE_ID = objFamily.AD_PROVINCE_ID
            objFamilyData.AD_VILLAGE = objFamily.AD_VILLAGE
            objFamilyData.AD_WARD_ID = objFamily.AD_WARD_ID
            objFamilyData.TT_DISTRICT_ID = objFamily.TT_DISTRICT_ID
            objFamilyData.TT_PROVINCE_ID = objFamily.TT_PROVINCE_ID
            objFamilyData.TT_WARD_ID = objFamily.TT_WARD_ID
            objFamilyData.IS_OWNER = objFamily.IS_OWNER
            objFamilyData.IS_PASS = objFamily.IS_PASS
            objFamilyData.CERTIFICATE_CODE = objFamily.CERTIFICATE_CODE
            objFamilyData.CERTIFICATE_NUM = objFamily.CERTIFICATE_NUM

            objFamilyData.NATION_ID = objFamily.NATION_ID
            objFamilyData.ID_NO_DATE = objFamily.ID_NO_DATE
            objFamilyData.ID_NO_PLACE_NAME = objFamily.ID_NO_PLACE_NAME
            objFamilyData.PHONE = objFamily.PHONE
            objFamilyData.TAXTATION_DATE = objFamily.TAXTATION_DATE
            objFamilyData.TAXTATION_PLACE = objFamily.TAXTATION_PLACE
            objFamilyData.BIRTH_CODE = objFamily.BIRTH_CODE
            objFamilyData.QUYEN = objFamily.QUYEN
            objFamilyData.BIRTH_NATION_ID = objFamily.BIRTH_NATION_ID
            objFamilyData.BIRTH_PROVINCE_ID = objFamily.BIRTH_PROVINCE_ID
            objFamilyData.BIRTH_DISTRICT_ID = objFamily.BIRTH_DISTRICT_ID
            objFamilyData.BIRTH_WARD_ID = objFamily.BIRTH_WARD_ID
            objFamilyData.GENDER = objFamily.GENDER
            objFamilyData.RELATE_OWNER = objFamily.RELATE_OWNER
            objFamilyData.NATIVE = objFamily.NATIVE
            objFamilyData.FILE_FAMILY = objFamily.FILE_FAMILY
            objFamilyData.FILE_NPT = objFamily.FILE_NPT
            If objFamily.IS_OWNER Then
                Dim objEmpCv = (From p In Context.HU_EMPLOYEE_CV Where p.EMPLOYEE_ID = objFamily.EMPLOYEE_ID).FirstOrDefault
                objEmpCv.NO_HOUSEHOLDS = objFamily.CERTIFICATE_NUM
            End If
            Context.SaveChanges(log)
            gID = objFamilyData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function
    Public Function CheckChuho(ByVal emp_id As Decimal, ByVal fa_id As Decimal) As Decimal
        Dim re As Decimal = 0
        If fa_id = 0 Then
            re = (From p In Context.HU_FAMILY Where p.EMPLOYEE_ID = emp_id And p.IS_OWNER = -1 Select p.ID).Count
        Else
            re = (From p In Context.HU_FAMILY Where p.EMPLOYEE_ID = emp_id And p.IS_OWNER = -1 And p.ID <> fa_id Select p.ID).Count
        End If
        Return re
    End Function
    Public Function GetFamilyByID(ByVal id As Decimal) As FamilyDTO
        Try
            Dim obj = From p In Context.HU_FAMILY
                      From p_g In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.RELATION_ID).DefaultIfEmpty
                      From n_g In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.PROVINCE_ID).DefaultIfEmpty
                      From pv_g In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.AD_PROVINCE_ID).DefaultIfEmpty
                      From d_g In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.AD_DISTRICT_ID).DefaultIfEmpty
                      From w_g In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.AD_WARD_ID).DefaultIfEmpty
                      From pvT_g In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TT_PROVINCE_ID).DefaultIfEmpty
                      From dT_g In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TT_DISTRICT_ID).DefaultIfEmpty
                      From wT_g In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TT_WARD_ID).DefaultIfEmpty
                      From n_tt In Context.HU_NATION.Where(Function(F) F.ID = p.NATION_ID).DefaultIfEmpty
                      From n_ks In Context.HU_NATION.Where(Function(F) F.ID = p.BIRTH_NATION_ID).DefaultIfEmpty
                      From p_ks In Context.HU_PROVINCE.Where(Function(F) F.ID = p.BIRTH_PROVINCE_ID).DefaultIfEmpty
                      From d_ks In Context.HU_DISTRICT.Where(Function(F) F.ID = p.BIRTH_DISTRICT_ID).DefaultIfEmpty
                      From w_ks In Context.HU_WARD.Where(Function(F) F.ID = p.BIRTH_WARD_ID).DefaultIfEmpty
                      From g In Context.OT_OTHER_LIST.Where(Function(F) F.ID = p.GENDER).DefaultIfEmpty
                      From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID).DefaultIfEmpty
                      From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                      From ti In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                      From ro In Context.OT_OTHER_LIST.Where(Function(F) F.ID = p.RELATE_OWNER).DefaultIfEmpty
                      From r In Context.OT_OTHER_LIST.Where(Function(F) F.ID = p.NATIVE).DefaultIfEmpty
                      From f_family In Context.HU_USERFILES.Where(Function(f) f.NAME = p.FILE_FAMILY).DefaultIfEmpty
                      From f_npt In Context.HU_USERFILES.Where(Function(F) F.NAME = p.FILE_NPT).DefaultIfEmpty
                      Where p.ID = id

            Dim family = obj.Select(Function(k) New FamilyDTO With {
                      .ID = k.p.ID,
                      .ADDRESS = k.p.ADDRESS,
                      .WORK_STATUS = k.e.WORK_STATUS,
                      .TER_EFFECT_DATE = k.e.TER_EFFECT_DATE,
                      .ADDRESS_TT = k.p.ADDRESS_TT,
                      .EMPLOYEE_ID = k.p.EMPLOYEE_ID,
                      .EMPLOYEE_CODE = k.e.EMPLOYEE_CODE,
                      .EMPLOYEE_NAME = k.e.FULLNAME_VN,
                      .FULLNAME = k.p.FULLNAME,
                      .RELATION_ID = k.p.RELATION_ID,
                      .RELATION_NAME = k.p_g.NAME_VN,
                      .PROVINCE_ID = k.p.PROVINCE_ID,
                      .PROVINCE_NAME = k.n_g.NAME_VN,
                      .CAREER = k.p.CAREER,
                      .TITLE_NAME = k.ti.NAME_VN,
                      .ORG_NAME = k.org.NAME_VN,
                      .BIRTH_DATE = k.p.BIRTH_DATE,
                      .TAXTATION = k.p.TAXTATION,
                      .DEDUCT_REG = k.p.DEDUCT_REG,
                      .ID_NO = k.p.ID_NO,
                      .IS_DEDUCT = k.p.IS_DEDUCT,
                      .DEDUCT_FROM = k.p.DEDUCT_FROM,
                      .DEDUCT_TO = k.p.DEDUCT_TO,
                      .REMARK = k.p.REMARK,
                      .AD_DISTRICT_ID = k.p.AD_DISTRICT_ID,
                      .AD_DISTRICT_NAME = k.d_g.NAME_VN,
                      .AD_PROVINCE_ID = k.p.AD_PROVINCE_ID,
                      .AD_PROVINCE_NAME = k.pv_g.NAME_VN,
                      .AD_VILLAGE = k.p.AD_VILLAGE,
                      .AD_WARD_ID = k.p.AD_WARD_ID,
                      .AD_WARD_NAME = k.w_g.NAME_EN,
                      .TT_DISTRICT_ID = k.p.TT_DISTRICT_ID,
                       .TT_DISTRICT_NAME = k.dT_g.NAME_VN,
                      .TT_PROVINCE_ID = k.p.TT_PROVINCE_ID,
                      .TT_PROVINCE_NAME = k.pvT_g.NAME_VN,
                      .TT_WARD_ID = k.p.TT_WARD_ID,
                      .TT_WARD_NAME = k.wT_g.NAME_EN,
                      .IS_OWNER = k.p.IS_OWNER,
                      .IS_PASS = k.p.IS_PASS,
                      .CERTIFICATE_CODE = k.p.CERTIFICATE_CODE,
                      .CERTIFICATE_NUM = k.p.CERTIFICATE_NUM,
                      .NATION_ID = k.p.NATION_ID,
                      .NATION_NAME = k.n_tt.NAME_VN,
                      .ID_NO_DATE = k.p.ID_NO_DATE,
                      .ID_NO_PLACE_NAME = k.p.ID_NO_PLACE_NAME,
                      .PHONE = k.p.PHONE,
                      .TAXTATION_DATE = k.p.TAXTATION_DATE,
                      .TAXTATION_PLACE = k.p.TAXTATION_PLACE,
                      .BIRTH_CODE = k.p.BIRTH_CODE,
                      .QUYEN = k.p.QUYEN,
                      .BIRTH_NATION_ID = k.p.BIRTH_NATION_ID,
                      .BIRTH_PROVINCE_ID = k.p.BIRTH_PROVINCE_ID,
                      .BIRTH_DISTRICT_ID = k.p.BIRTH_DISTRICT_ID,
                      .BIRTH_WARD_ID = k.p.BIRTH_WARD_ID,
                      .BIRTH_NATION_NAME = k.n_ks.NAME_VN,
                      .BIRTH_PROVINCE_NAME = k.p_ks.NAME_VN,
                      .BIRTH_DISTRICT_NAME = k.d_ks.NAME_VN,
                      .BIRTH_WARD_NAME = k.w_ks.NAME_VN,
                      .GENDER = k.p.GENDER,
                      .CREATED_DATE = k.p.CREATED_DATE,
                      .IS_SAME_COMPANY = k.p.IS_SAME_COMPANY,
                      .RELATE_OWNER = k.p.RELATE_OWNER,
                      .RELATE_OWNER_NAME = k.ro.NAME_VN,
                      .NATIVE = k.p.NATIVE,
                      .NATIVE_NAME = k.r.NAME_VN,
                      .GENDER_NAME = k.g.NAME_VN,
                    .UPLOAD_FILE_FAMILY = k.f_family.NAME,
                    .UPLOAD_FILE_NPT = k.f_npt.NAME,
                    .FILE_FAMILY = k.f_family.FILE_NAME,
                    .FILE_NPT = k.f_npt.FILE_NAME}).FirstOrDefault
            Return family
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function GetWorkingBeforeByID(ByVal id As Decimal) As WorkingBeforeDTO
        Dim query = From b In Context.HU_WORKING_BEFORE
                    From e In Context.HU_EMPLOYEE.Where(Function(p) p.ID = b.EMPLOYEE_ID)
                    From o In Context.HU_ORGANIZATION.Where(Function(p) p.ID = e.ORG_ID).DefaultIfEmpty
                    From f In Context.HU_USERFILES.Where(Function(p) p.NAME = b.FILE_NAME).DefaultIfEmpty
                    From t In Context.HU_TITLE.Where(Function(p) p.ID = e.TITLE_ID).DefaultIfEmpty
                    Where b.ID = id

        Dim Working = query.Select(Function(p) New WorkingBeforeDTO With {
                             .ID = p.b.ID,
                             .EMPLOYEE_ID = p.e.ID,
                             .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                             .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                             .ORG_NAME = p.o.NAME_VN,
                             .TITLE_NAME = p.t.NAME_VN,
                             .JOIN_DATE = p.b.JOIN_DATE,
                             .END_DATE = p.b.END_DATE,
                             .THAM_NIEN = p.b.THAM_NIEN,
                             .COMPANY_NAME = p.b.COMPANY_NAME,
                             .COMPANY_ADDRESS = p.b.COMPANY_ADDRESS,
                             .TITLE_NAME_BEFORE = p.b.TITLE_NAME,
                             .WORK = p.b.WORK,
                             .TER_REASON = p.b.TER_REASON,
                             .IS_HSV = p.b.IS_HSV,
                             .IS_THAMNIEN = p.b.IS_THAMNIEN,
                             .DEPARTMENT = p.b.DEPARTMENT,
                             .FILE_NAME = p.f.FILE_NAME,
                             .UPLOAD_FILE = p.f.NAME}).FirstOrDefault
        Return Working
    End Function
    Public Function GetEmployeeFamily_1(ByVal _filter As FamilyDTO, ByVal PageIndex As Integer,
                               ByVal PageSize As Integer,
                               ByRef Total As Integer, ByVal _param As ParamDTO,
                               Optional ByVal Sorts As String = "CREATED_DATE desc",
                               Optional ByVal log As UserLog = Nothing) As List(Of FamilyDTO)
        'Dim query As ObjectQuery(Of FamilyDTO)
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using

            Dim query = From p In Context.HU_FAMILY
                        Group Join m In Context.OT_OTHER_LIST On p.RELATION_ID Equals m.ID Into gGroup = Group
                        From p_g In gGroup.DefaultIfEmpty
                        Group Join n In Context.HU_PROVINCE On p.PROVINCE_ID Equals n.ID Into nGroup = Group
                        From n_g In nGroup.DefaultIfEmpty
                        Group Join pv In Context.HU_PROVINCE On p.AD_PROVINCE_ID Equals pv.ID Into pvGroup = Group
                        From pv_g In pvGroup.DefaultIfEmpty
                        Group Join d In Context.HU_DISTRICT On p.AD_DISTRICT_ID Equals d.ID Into dGroup = Group
                        From d_g In dGroup.DefaultIfEmpty
                        Group Join w In Context.HU_WARD On p.AD_WARD_ID Equals w.ID Into wGroup = Group
                        From w_g In wGroup.DefaultIfEmpty
                        Group Join pvT In Context.HU_PROVINCE On p.TT_PROVINCE_ID Equals pvT.ID Into pvTGroup = Group
                        From pvT_g In pvTGroup.DefaultIfEmpty
                        Group Join dT In Context.HU_DISTRICT On p.TT_DISTRICT_ID Equals dT.ID Into dTGroup = Group
                        From dT_g In dTGroup.DefaultIfEmpty
                        Group Join wT In Context.HU_WARD On p.TT_WARD_ID Equals wT.ID Into wTGroup = Group
                        From wT_g In wTGroup.DefaultIfEmpty
                        From n_tt In Context.HU_NATION.Where(Function(F) F.ID = p.NATION_ID).DefaultIfEmpty
                        From n_ks In Context.HU_NATION.Where(Function(F) F.ID = p.BIRTH_NATION_ID).DefaultIfEmpty
                        From p_ks In Context.HU_PROVINCE.Where(Function(F) F.ID = p.BIRTH_PROVINCE_ID).DefaultIfEmpty
                        From d_ks In Context.HU_DISTRICT.Where(Function(F) F.ID = p.BIRTH_DISTRICT_ID).DefaultIfEmpty
                        From w_ks In Context.HU_WARD.Where(Function(F) F.ID = p.BIRTH_WARD_ID).DefaultIfEmpty
                        From g In Context.OT_OTHER_LIST.Where(Function(F) F.ID = p.GENDER).DefaultIfEmpty
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID).DefaultIfEmpty
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From ti In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From f_family In Context.HU_USERFILES.Where(Function(f) f.NAME = p.FILE_FAMILY).DefaultIfEmpty
                        From f_npt In Context.HU_USERFILES.Where(Function(F) F.NAME = p.FILE_NPT).DefaultIfEmpty
                        From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = e.ORG_ID And
                                                                       f.USERNAME = log.Username.ToUpper)

            Dim dateNow = Date.Now.Date
            Dim terID = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID

            If Not _filter.IS_TER Then
                query = query.Where(Function(p) Not p.e.WORK_STATUS.HasValue Or
                                        (p.e.WORK_STATUS.HasValue And
                                         ((p.e.WORK_STATUS <> terID) Or (p.e.WORK_STATUS = terID And p.e.TER_EFFECT_DATE > dateNow))))

            End If

            If _filter.IS_DEDUCT Then
                query = query.Where(Function(p) p.p.IS_DEDUCT = -1)
            End If

            If _filter.EMPLOYEE_ID <> 0 Then
                query = query.Where(Function(p) p.p.EMPLOYEE_ID = _filter.EMPLOYEE_ID)
            End If
            If _filter.ID <> 0 Then
                query = query.Where(Function(p) p.p.ID = _filter.ID)
            End If
            If _filter.EMPLOYEE_CODE <> "" Then
                query = query.Where(Function(p) p.e.EMPLOYEE_CODE.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower))
            End If
            If _filter.EMPLOYEE_NAME <> "" Then
                query = query.Where(Function(p) p.e.FULLNAME_VN.ToLower().Contains(_filter.EMPLOYEE_NAME.ToLower))
            End If
            If _filter.ORG_NAME <> "" Then
                query = query.Where(Function(p) p.org.NAME_VN.ToLower().Contains(_filter.ORG_NAME.ToLower))
            End If
            If _filter.TITLE_NAME <> "" Then
                query = query.Where(Function(p) p.ti.NAME_VN.ToLower().Contains(_filter.TITLE_NAME.ToLower))
            End If
            If _filter.FULLNAME <> "" Then
                query = query.Where(Function(p) p.p.FULLNAME.ToLower().Contains(_filter.FULLNAME.ToLower))
            End If
            If _filter.RELATION_NAME <> "" Then
                query = query.Where(Function(p) p.p_g.NAME_VN.ToLower().Contains(_filter.RELATION_NAME.ToLower))
            End If
            If _filter.BIRTH_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.p.BIRTH_DATE = _filter.BIRTH_DATE)
            End If
            If _filter.ID_NO <> "" Then
                query = query.Where(Function(p) p.p.ID_NO.ToLower().Contains(_filter.ID_NO.ToLower))
            End If
            If _filter.TAXTATION <> "" Then
                query = query.Where(Function(p) p.p.TAXTATION.ToLower().Contains(_filter.TAXTATION.ToLower))
            End If
            If _filter.DEDUCT_REG IsNot Nothing Then
                query = query.Where(Function(p) p.p.DEDUCT_REG = _filter.DEDUCT_REG)
            End If
            If _filter.DEDUCT_FROM IsNot Nothing Then
                query = query.Where(Function(p) p.p.DEDUCT_FROM = _filter.DEDUCT_FROM)
            End If
            If _filter.DEDUCT_TO IsNot Nothing Then
                query = query.Where(Function(p) p.p.DEDUCT_TO = _filter.DEDUCT_TO)
            End If


            Dim family = query.Select(Function(k) New FamilyDTO With {
                    .ID = k.p.ID,
                    .ADDRESS = k.p.ADDRESS,
                    .WORK_STATUS = k.e.WORK_STATUS,
                    .TER_EFFECT_DATE = k.e.TER_EFFECT_DATE,
                    .ADDRESS_TT = k.p.ADDRESS_TT,
                    .EMPLOYEE_ID = k.p.EMPLOYEE_ID,
                    .EMPLOYEE_CODE = k.e.EMPLOYEE_CODE,
                    .EMPLOYEE_NAME = k.e.FULLNAME_VN,
                    .FULLNAME = k.p.FULLNAME,
                    .RELATION_ID = k.p.RELATION_ID,
                    .RELATION_NAME = k.p_g.NAME_VN,
                    .PROVINCE_ID = k.p.PROVINCE_ID,
                    .PROVINCE_NAME = k.n_g.NAME_VN,
                    .CAREER = k.p.CAREER,
                    .TITLE_NAME = k.ti.NAME_VN,
                    .ORG_NAME = k.org.NAME_VN,
                    .ORG_DESC = k.org.DESCRIPTION_PATH,
                    .BIRTH_DATE = k.p.BIRTH_DATE,
                    .TAXTATION = k.p.TAXTATION,
                    .DEDUCT_REG = k.p.DEDUCT_REG,
                    .ID_NO = k.p.ID_NO,
                    .IS_DEDUCT = k.p.IS_DEDUCT,
                    .DEDUCT_FROM = k.p.DEDUCT_FROM,
                    .DEDUCT_TO = k.p.DEDUCT_TO,
                    .REMARK = k.p.REMARK,
                    .AD_DISTRICT_ID = k.p.AD_DISTRICT_ID,
                    .AD_DISTRICT_NAME = k.d_g.NAME_VN,
                    .AD_PROVINCE_ID = k.p.AD_PROVINCE_ID,
                    .AD_PROVINCE_NAME = k.pv_g.NAME_VN,
                    .AD_VILLAGE = k.p.AD_VILLAGE,
                    .AD_WARD_ID = k.p.AD_WARD_ID,
                    .AD_WARD_NAME = k.w_g.NAME_EN,
                    .TT_DISTRICT_ID = k.p.TT_DISTRICT_ID,
                     .TT_DISTRICT_NAME = k.dT_g.NAME_VN,
                    .TT_PROVINCE_ID = k.p.TT_PROVINCE_ID,
                    .TT_PROVINCE_NAME = k.pvT_g.NAME_VN,
                    .TT_WARD_ID = k.p.TT_WARD_ID,
                    .TT_WARD_NAME = k.wT_g.NAME_EN,
                    .IS_OWNER = k.p.IS_OWNER,
                    .IS_PASS = k.p.IS_PASS,
                    .CERTIFICATE_CODE = k.p.CERTIFICATE_CODE,
                    .CERTIFICATE_NUM = k.p.CERTIFICATE_NUM,
                    .NATION_ID = k.p.NATION_ID,
                    .NATION_NAME = k.n_tt.NAME_VN,
                    .ID_NO_DATE = k.p.ID_NO_DATE,
                    .ID_NO_PLACE_NAME = k.p.ID_NO_PLACE_NAME,
                    .PHONE = k.p.PHONE,
                    .TAXTATION_DATE = k.p.TAXTATION_DATE,
                    .TAXTATION_PLACE = k.p.TAXTATION_PLACE,
                    .BIRTH_CODE = k.p.BIRTH_CODE,
                    .QUYEN = k.p.QUYEN,
                    .BIRTH_NATION_ID = k.p.BIRTH_NATION_ID,
                    .BIRTH_PROVINCE_ID = k.p.BIRTH_PROVINCE_ID,
                    .BIRTH_DISTRICT_ID = k.p.BIRTH_DISTRICT_ID,
                    .BIRTH_WARD_ID = k.p.BIRTH_WARD_ID,
                    .BIRTH_NATION_NAME = k.n_ks.NAME_VN,
                    .BIRTH_PROVINCE_NAME = k.p_ks.NAME_VN,
                    .BIRTH_DISTRICT_NAME = k.d_ks.NAME_VN,
                    .BIRTH_WARD_NAME = k.w_ks.NAME_VN,
                    .GENDER = k.p.GENDER,
                    .CREATED_DATE = k.p.CREATED_DATE,
                    .GENDER_NAME = k.g.NAME_VN,
                    .UPLOAD_FILE_FAMILY = k.f_family.NAME,
                    .UPLOAD_FILE_NPT = k.f_npt.NAME,
                    .FILE_FAMILY = k.f_family.FILE_NAME,
                    .FILE_NPT = k.f_npt.FILE_NAME})

            family = family.OrderBy(Sorts)
            Total = query.Count
            family = family.Skip(PageIndex * PageSize).Take(PageSize)

            Return family.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function


    Public Function GetEmployeeFamily(ByVal _filter As FamilyDTO) As List(Of FamilyDTO)
        Dim query As ObjectQuery(Of FamilyDTO)
        Try
            query = (From p In Context.HU_FAMILY
                     Group Join m In Context.OT_OTHER_LIST On p.RELATION_ID Equals m.ID Into gGroup = Group
                     From p_g In gGroup.DefaultIfEmpty
                     Group Join n In Context.HU_PROVINCE On p.PROVINCE_ID Equals n.ID Into nGroup = Group
                     From n_g In nGroup.DefaultIfEmpty
                     Group Join pv In Context.HU_PROVINCE On p.AD_PROVINCE_ID Equals pv.ID Into pvGroup = Group
                     From pv_g In pvGroup.DefaultIfEmpty
                     Group Join d In Context.HU_DISTRICT On p.AD_DISTRICT_ID Equals d.ID Into dGroup = Group
                     From d_g In dGroup.DefaultIfEmpty
                     Group Join w In Context.HU_WARD On p.AD_WARD_ID Equals w.ID Into wGroup = Group
                     From w_g In wGroup.DefaultIfEmpty
                     Group Join pvT In Context.HU_PROVINCE On p.TT_PROVINCE_ID Equals pvT.ID Into pvTGroup = Group
                     From pvT_g In pvTGroup.DefaultIfEmpty
                     Group Join dT In Context.HU_DISTRICT On p.TT_DISTRICT_ID Equals dT.ID Into dTGroup = Group
                     From dT_g In dTGroup.DefaultIfEmpty
                     Group Join wT In Context.HU_WARD On p.TT_WARD_ID Equals wT.ID Into wTGroup = Group
                     From wT_g In wTGroup.DefaultIfEmpty
                     From n_tt In Context.HU_NATION.Where(Function(F) F.ID = p.NATION_ID).DefaultIfEmpty
                     From n_ks In Context.HU_NATION.Where(Function(F) F.ID = p.BIRTH_NATION_ID).DefaultIfEmpty
                     From p_ks In Context.HU_PROVINCE.Where(Function(F) F.ID = p.BIRTH_PROVINCE_ID).DefaultIfEmpty
                     From d_ks In Context.HU_DISTRICT.Where(Function(F) F.ID = p.BIRTH_DISTRICT_ID).DefaultIfEmpty
                     From w_ks In Context.HU_WARD.Where(Function(F) F.ID = p.BIRTH_WARD_ID).DefaultIfEmpty
                     From g In Context.OT_OTHER_LIST.Where(Function(F) F.ID = p.GENDER).DefaultIfEmpty
                     From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID).DefaultIfEmpty
                     From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                     From ti In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                     From f_family In Context.HU_USERFILES.Where(Function(f) f.NAME = p.FILE_FAMILY).DefaultIfEmpty
                     From f_npt In Context.HU_USERFILES.Where(Function(F) F.NAME = p.FILE_NPT).DefaultIfEmpty
                     From native In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.NATIVE).DefaultIfEmpty
                     From relate_owner In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.RELATE_OWNER).DefaultIfEmpty
                     From ed In Context.HU_FAMILY_EDIT.Where(Function(f) f.FK_PKEY = p.ID).DefaultIfEmpty
                     Where p.EMPLOYEE_ID = _filter.EMPLOYEE_ID
                     Select New FamilyDTO With {
                    .ID = p.ID,
                         .STATUS_TAB = 1,
                         .STATUS = ed.STATUS,
                         .FK_PKEY = ed.ID,
                    .ADDRESS = p.ADDRESS,
                    .ADDRESS_TT = p.ADDRESS_TT,
                    .EMPLOYEE_ID = p.EMPLOYEE_ID,
                    .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                    .FULLNAME = p.FULLNAME,
                    .RELATION_ID = p.RELATION_ID,
                    .RELATION_NAME = p_g.NAME_VN,
                    .PROVINCE_ID = p.PROVINCE_ID,
                    .PROVINCE_NAME = n_g.NAME_VN,
                    .CAREER = p.CAREER,
                    .TITLE_NAME = ti.NAME_VN,
                    .ORG_NAME = org.NAME_VN,
                    .BIRTH_DATE = p.BIRTH_DATE,
                    .TAXTATION = p.TAXTATION,
                    .DEDUCT_REG = p.DEDUCT_REG,
                    .ID_NO = p.ID_NO,
                    .IS_DEDUCT = p.IS_DEDUCT,
                    .DEDUCT_FROM = p.DEDUCT_FROM,
                    .DEDUCT_TO = p.DEDUCT_TO,
                    .REMARK = p.REMARK,
                    .AD_DISTRICT_ID = p.AD_DISTRICT_ID,
                    .AD_DISTRICT_NAME = d_g.NAME_VN,
                    .AD_PROVINCE_ID = p.AD_PROVINCE_ID,
                    .AD_PROVINCE_NAME = pv_g.NAME_VN,
                    .AD_VILLAGE = p.AD_VILLAGE,
                    .AD_WARD_ID = p.AD_WARD_ID,
                    .AD_WARD_NAME = w_g.NAME_EN,
                    .TT_DISTRICT_ID = p.TT_DISTRICT_ID,
                     .TT_DISTRICT_NAME = dT_g.NAME_VN,
                    .TT_PROVINCE_ID = p.TT_PROVINCE_ID,
                    .TT_PROVINCE_NAME = pvT_g.NAME_VN,
                    .TT_WARD_ID = p.TT_WARD_ID,
                    .TT_WARD_NAME = wT_g.NAME_EN,
                    .IS_OWNER = p.IS_OWNER,
                    .IS_PASS = p.IS_PASS,
                    .CERTIFICATE_CODE = p.CERTIFICATE_CODE,
                    .CERTIFICATE_NUM = p.CERTIFICATE_NUM,
                    .NATION_ID = p.NATION_ID,
                    .NATION_NAME = n_tt.NAME_VN,
                    .ID_NO_DATE = p.ID_NO_DATE,
                    .ID_NO_PLACE_NAME = p.ID_NO_PLACE_NAME,
                    .PHONE = p.PHONE,
                    .TAXTATION_DATE = p.TAXTATION_DATE,
                    .TAXTATION_PLACE = p.TAXTATION_PLACE,
                    .BIRTH_CODE = p.BIRTH_CODE,
                    .QUYEN = p.QUYEN,
                    .BIRTH_NATION_ID = p.BIRTH_NATION_ID,
                    .BIRTH_PROVINCE_ID = p.BIRTH_PROVINCE_ID,
                    .BIRTH_DISTRICT_ID = p.BIRTH_DISTRICT_ID,
                    .BIRTH_WARD_ID = p.BIRTH_WARD_ID,
                    .BIRTH_NATION_NAME = n_ks.NAME_VN,
                    .BIRTH_PROVINCE_NAME = p_ks.NAME_VN,
                    .BIRTH_DISTRICT_NAME = d_ks.NAME_VN,
                    .BIRTH_WARD_NAME = w_ks.NAME_VN,
                    .GENDER = p.GENDER,
                    .NATIVE_NAME = native.NAME_VN,
                    .RELATE_OWNER_NAME = relate_owner.NAME_VN,
                    .UPLOAD_FILE_FAMILY = f_family.NAME,
                    .UPLOAD_FILE_NPT = f_npt.NAME,
                    .FILE_FAMILY = f_family.FILE_NAME,
                    .FILE_NPT = f_npt.FILE_NAME,
                    .IS_SAME_COMPANY = p.IS_SAME_COMPANY,
                    .GENDER_NAME = g.NAME_VN})
            'If _filter.EMPLOYEE_ID <> 0 Then
            '    query = query.Where(Function(p) p.EMPLOYEE_ID = _filter.EMPLOYEE_ID)
            'End If
            'If _filter.ID <> 0 Then
            '    query = query.Where(Function(p) p.ID = _filter.ID)
            'End If
            'If _filter.RELATION_NAME <> "" Then
            '    query = query.Where(Function(p) p.RELATION_NAME.ToLower().Contains(_filter.RELATION_NAME.ToLower))
            'End If
            'If _filter.PROVINCE_NAME <> "" Then
            '    query = query.Where(Function(p) p.PROVINCE_NAME.ToLower().Contains(_filter.PROVINCE_NAME.ToLower))
            'End If
            'If _filter.FULLNAME <> "" Then
            '    query = query.Where(Function(p) p.FULLNAME.ToLower().Contains(_filter.FULLNAME.ToLower))
            'End If
            If _filter.BIRTH_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.BIRTH_DATE = _filter.BIRTH_DATE)
            End If
            'If _filter.ID_NO <> "" Then
            '    query = query.Where(Function(p) p.ID_NO.ToUpper().IndexOf(_filter.ID_NO.ToUpper) >= 0)
            'End If
            If _filter.DEDUCT_REG IsNot Nothing Then
                query = query.Where(Function(p) p.DEDUCT_REG = _filter.DEDUCT_REG)
            End If
            If _filter.DEDUCT_FROM IsNot Nothing Then
                query = query.Where(Function(p) p.DEDUCT_FROM = _filter.DEDUCT_FROM)
            End If
            If _filter.DEDUCT_TO IsNot Nothing Then
                query = query.Where(Function(p) p.DEDUCT_TO = _filter.DEDUCT_TO)
            End If
            'If _filter.ADDRESS <> "" Then
            '    query = query.Where(Function(p) p.ADDRESS.ToLower().Contains(_filter.ADDRESS.ToLower))
            'End If
            'If _filter.ADDRESS_TT <> "" Then
            '    query = query.Where(Function(p) p.ADDRESS_TT.ToLower().Contains(_filter.ADDRESS_TT.ToLower))
            'End If
            'If _filter.REMARK <> "" Then
            '    query = query.Where(Function(p) p.REMARK.ToLower().Contains(_filter.REMARK.ToLower))
            'End If
            'If _filter.AD_DISTRICT_NAME <> "" Then
            '    query = query.Where(Function(p) p.AD_DISTRICT_NAME.ToLower().Contains(_filter.AD_DISTRICT_NAME.ToLower))
            'End If
            'If _filter.AD_PROVINCE_NAME <> "" Then
            '    query = query.Where(Function(p) p.AD_PROVINCE_NAME.ToLower().Contains(_filter.AD_PROVINCE_NAME.ToLower))
            'End If
            'If _filter.AD_VILLAGE <> "" Then
            '    query = query.Where(Function(p) p.AD_VILLAGE.ToLower().Contains(_filter.AD_VILLAGE.ToLower))
            'End If
            'If _filter.AD_WARD_NAME <> "" Then
            '    query = query.Where(Function(p) p.AD_WARD_NAME.ToLower().Contains(_filter.AD_WARD_NAME.ToLower))
            'End If
            'If _filter.TT_DISTRICT_NAME <> "" Then
            '    query = query.Where(Function(p) p.TT_DISTRICT_NAME.ToLower().Contains(_filter.TT_DISTRICT_NAME.ToLower))
            'End If
            'If _filter.TT_PROVINCE_NAME <> "" Then
            '    query = query.Where(Function(p) p.TT_PROVINCE_NAME.ToLower().Contains(_filter.TT_PROVINCE_NAME.ToLower))
            'End If
            'If _filter.TT_WARD_NAME <> "" Then
            '    query = query.Where(Function(p) p.TT_WARD_NAME.ToLower().Contains(_filter.TT_WARD_NAME.ToLower))
            'End If
            If _filter.ID <> 0 Then
                query = query.Where(Function(p) p.ID = _filter.ID)
            End If

            Dim count = query.ToList.Count
            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function DeleteEmployeeFamily(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim lst As List(Of HU_FAMILY)
        Try
            lst = (From p In Context.HU_FAMILY Where lstDecimals.Contains(p.ID)).ToList
            For i As Int16 = 0 To lst.Count - 1
                Context.HU_FAMILY.DeleteObject(lst(i))
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' Hàm check trùng CMND của thân nhân.
    ''' </summary>
    ''' <param name="_validate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateFamily(ByVal _validate As FamilyDTO)
        Try
            If _validate.ID_NO <> "" Then
                Dim query = (From p In Context.HU_FAMILY
                             Where p.ID_NO.ToUpper = _validate.ID_NO.ToUpper And
                             p.ID <> _validate.ID)

                Return query.Count = 0
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Return False
            Throw ex
        End Try
    End Function

#End Region

#Region "FamilyEdit"
    Public Function GetChangedFamilyList(ByVal lstFamilyEdit As List(Of FamilyEditDTO)) As Dictionary(Of String, String)
        Try
            Dim dic As New Dictionary(Of String, String)
            For Each familyEdit As FamilyEditDTO In lstFamilyEdit
                Dim colNames As String = String.Empty
                Dim family = Context.HU_FAMILY.Where(Function(f) f.ID = familyEdit.FK_PKEY).FirstOrDefault
                If family IsNot Nothing Then
                    Dim ownerEdit As Decimal? = familyEdit.IS_OWNER
                    Dim owner As Decimal? = family.IS_OWNER
                    Dim passEdit As Decimal? = familyEdit.IS_PASS
                    Dim pass As Decimal? = family.IS_PASS
                    Dim deductEdit As Decimal? = familyEdit.IS_DEDUCT
                    Dim deduct As Decimal? = family.IS_DEDUCT
                    If (If(familyEdit.FULLNAME Is Nothing, "", familyEdit.FULLNAME) <> If(family.FULLNAME Is Nothing, "", family.FULLNAME)) Then
                        colNames = "FULLNAME"
                    End If
                    If (If(familyEdit.RELATION_ID.ToString() Is Nothing, "", familyEdit.RELATION_ID.ToString()) <> If(family.RELATION_ID.ToString() Is Nothing, "", family.RELATION_ID.ToString())) Then
                        colNames = If(colNames <> String.Empty, colNames + "," + "RELATION_NAME", "RELATION_NAME")
                    End If
                    If (If(familyEdit.GENDER.ToString() Is Nothing, "", familyEdit.GENDER.ToString()) <> If(family.GENDER.ToString() Is Nothing, "", family.GENDER.ToString())) Then
                        colNames = If(colNames <> String.Empty, colNames + "," + "GENDER_NAME", "GENDER_NAME")
                    End If
                    If (If(familyEdit.NATIVE.ToString() Is Nothing, "", familyEdit.NATIVE.ToString()) <> If(family.NATIVE.ToString() Is Nothing, "", family.NATIVE.ToString())) Then
                        colNames = If(colNames <> String.Empty, colNames + "," + "NATIVE_NAME", "NATIVE_NAME")
                    End If
                    If (If(familyEdit.RELATE_OWNER.ToString() Is Nothing, "", familyEdit.RELATE_OWNER.ToString()) <> If(family.RELATE_OWNER.ToString() Is Nothing, "", family.RELATE_OWNER.ToString())) Then
                        colNames = If(colNames <> String.Empty, colNames + "," + "RELATE_OWNER_NAME", "RELATE_OWNER_NAME")
                    End If
                    If (If(familyEdit.BIRTH_DATE Is Nothing, "", familyEdit.BIRTH_DATE.ToString()) <> If(family.BIRTH_DATE Is Nothing, "", family.BIRTH_DATE.ToString())) Then
                        colNames = If(colNames <> String.Empty, colNames + "," + "BIRTH_DATE", "BIRTH_DATE")
                    End If
                    If (If(familyEdit.ID_NO Is Nothing, "", familyEdit.ID_NO) <> If(family.ID_NO Is Nothing, "", family.ID_NO)) Then
                        colNames = If(colNames <> String.Empty, colNames + "," + "ID_NO", "ID_NO")
                    End If
                    If (If(familyEdit.ID_NO_DATE Is Nothing, "", familyEdit.ID_NO_DATE.ToString) <> If(family.ID_NO_DATE Is Nothing, "", family.ID_NO_DATE.ToString)) Then
                        colNames = If(colNames <> String.Empty, colNames + "," + "ID_NO_DATE", "ID_NO_DATE")
                    End If
                    If (If(familyEdit.ID_NO_PLACE_NAME Is Nothing, "", familyEdit.ID_NO_PLACE_NAME) <> If(family.ID_NO_PLACE_NAME Is Nothing, "", family.ID_NO_PLACE_NAME)) Then
                        colNames = If(colNames <> String.Empty, colNames + "," + "ID_NO_PLACE_NAME", "ID_NO_PLACE_NAME")
                    End If
                    If (If(familyEdit.CAREER Is Nothing, "", familyEdit.CAREER) <> If(family.CAREER Is Nothing, "", family.CAREER)) Then
                        colNames = If(colNames <> String.Empty, colNames + "," + "CAREER", "CAREER")
                    End If
                    If (If(ownerEdit.ToString Is Nothing, "", ownerEdit.ToString) <> If(owner.ToString Is Nothing, "", owner.ToString)) Then
                        colNames = If(colNames <> String.Empty, colNames + "," + "IS_OWNER", "IS_OWNER")
                    End If
                    If (If(familyEdit.CERTIFICATE_NUM Is Nothing, "", familyEdit.CERTIFICATE_NUM) <> If(family.CERTIFICATE_NUM Is Nothing, "", family.CERTIFICATE_NUM)) Then
                        colNames = If(colNames <> String.Empty, colNames + "," + "CERTIFICATE_NUM", "CERTIFICATE_NUM")
                    End If
                    If (If(familyEdit.CERTIFICATE_CODE Is Nothing, "", familyEdit.CERTIFICATE_CODE) <> If(family.CERTIFICATE_CODE Is Nothing, "", family.CERTIFICATE_CODE)) Then
                        colNames = If(colNames <> String.Empty, colNames + "," + "CERTIFICATE_CODE", "CERTIFICATE_CODE")
                    End If
                    If (If(familyEdit.ADDRESS Is Nothing, "", familyEdit.ADDRESS) <> If(family.ADDRESS Is Nothing, "", family.ADDRESS)) Then
                        colNames = If(colNames <> String.Empty, colNames + "," + "ADDRESS", "ADDRESS")
                    End If
                    If (If(familyEdit.NATION_ID.ToString Is Nothing, "", familyEdit.NATION_ID.ToString) <> If(family.NATION_ID.ToString Is Nothing, "", family.NATION_ID.ToString)) Then
                        colNames = If(colNames <> String.Empty, colNames + "," + "NATION_NAME", "NATION_NAME")
                    End If
                    If (If(familyEdit.AD_PROVINCE_ID.ToString Is Nothing, "", familyEdit.AD_PROVINCE_ID.ToString) <> If(family.AD_PROVINCE_ID.ToString Is Nothing, "", family.AD_PROVINCE_ID.ToString)) Then
                        colNames = If(colNames <> String.Empty, colNames + "," + "AD_PROVINCE_NAME", "AD_PROVINCE_NAME")
                    End If
                    If (If(familyEdit.AD_DISTRICT_ID.ToString Is Nothing, "", familyEdit.AD_DISTRICT_ID.ToString) <> If(family.AD_DISTRICT_ID.ToString Is Nothing, "", family.AD_DISTRICT_ID.ToString)) Then
                        colNames = If(colNames <> String.Empty, colNames + "," + "AD_DISTRICT_NAME", "AD_DISTRICT_NAME")
                    End If
                    If (If(familyEdit.AD_WARD_ID.ToString Is Nothing, "", familyEdit.AD_WARD_ID.ToString) <> If(family.AD_WARD_ID.ToString Is Nothing, "", family.AD_WARD_ID.ToString)) Then
                        colNames = If(colNames <> String.Empty, colNames + "," + "AD_WARD_NAME", "AD_WARD_NAME")
                    End If
                    If (If(familyEdit.AD_VILLAGE Is Nothing, "", familyEdit.AD_VILLAGE) <> If(family.AD_VILLAGE Is Nothing, "", family.AD_VILLAGE)) Then
                        colNames = If(colNames <> String.Empty, colNames + "," + "AD_VILLAGE", "AD_VILLAGE")
                    End If
                    If (If(familyEdit.ADDRESS_TT Is Nothing, "", familyEdit.ADDRESS_TT) <> If(family.ADDRESS_TT Is Nothing, "", family.ADDRESS_TT)) Then
                        colNames = If(colNames <> String.Empty, colNames + "," + "ADDRESS_TT", "ADDRESS_TT")
                    End If
                    If (If(familyEdit.TT_PROVINCE_ID.ToString Is Nothing, "", familyEdit.TT_PROVINCE_ID.ToString) <> If(family.TT_PROVINCE_ID.ToString Is Nothing, "", family.TT_PROVINCE_ID.ToString)) Then
                        colNames = If(colNames <> String.Empty, colNames + "," + "TT_PROVINCE_NAME", "TT_PROVINCE_NAME")
                    End If
                    If (If(familyEdit.TT_DISTRICT_ID.ToString Is Nothing, "", familyEdit.TT_DISTRICT_ID.ToString) <> If(family.TT_DISTRICT_ID.ToString Is Nothing, "", family.TT_DISTRICT_ID.ToString)) Then
                        colNames = If(colNames <> String.Empty, colNames + "," + "TT_DISTRICT_NAME", "TT_DISTRICT_NAME")
                    End If
                    If (If(familyEdit.TT_WARD_ID.ToString Is Nothing, "", familyEdit.TT_WARD_ID.ToString) <> If(family.TT_WARD_ID.ToString Is Nothing, "", family.TT_WARD_ID.ToString)) Then
                        colNames = If(colNames <> String.Empty, colNames + "," + "TT_WARD_NAME", "TT_WARD_NAME")
                    End If
                    If (If(familyEdit.PHONE Is Nothing, "", familyEdit.PHONE) <> If(family.PHONE Is Nothing, "", family.PHONE)) Then
                        colNames = If(colNames <> String.Empty, colNames + "," + "PHONE", "PHONE")
                    End If
                    If (If(familyEdit.TAXTATION Is Nothing, "", familyEdit.TAXTATION) <> If(family.TAXTATION Is Nothing, "", family.TAXTATION)) Then
                        colNames = If(colNames <> String.Empty, colNames + "," + "TAXTATION", "TAXTATION")
                    End If
                    If (If(familyEdit.TAXTATION_DATE Is Nothing, "", familyEdit.TAXTATION_DATE.ToString) <> If(family.TAXTATION_DATE Is Nothing, "", family.TAXTATION_DATE.ToString)) Then
                        colNames = If(colNames <> String.Empty, colNames + "," + "TAXTATION_DATE", "TAXTATION_DATE")
                    End If
                    If (If(familyEdit.TAXTATION_PLACE Is Nothing, "", familyEdit.TAXTATION_PLACE) <> If(family.TAXTATION_PLACE Is Nothing, "", family.TAXTATION_PLACE)) Then
                        colNames = If(colNames <> String.Empty, colNames + "," + "TAXTATION_PLACE", "TAXTATION_PLACE")
                    End If
                    If (If(familyEdit.BIRTH_CODE Is Nothing, "", familyEdit.BIRTH_CODE) <> If(family.BIRTH_CODE Is Nothing, "", family.BIRTH_CODE)) Then
                        colNames = If(colNames <> String.Empty, colNames + "," + "BIRTH_CODE", "BIRTH_CODE")
                    End If
                    If (If(familyEdit.QUYEN Is Nothing, "", familyEdit.QUYEN) <> If(family.QUYEN Is Nothing, "", family.QUYEN)) Then
                        colNames = If(colNames <> String.Empty, colNames + "," + "QUYEN", "QUYEN")
                    End If
                    If (If(familyEdit.BIRTH_NATION_ID.ToString Is Nothing, "", familyEdit.BIRTH_NATION_ID.ToString) <> If(family.BIRTH_NATION_ID.ToString Is Nothing, "", family.BIRTH_NATION_ID.ToString)) Then
                        colNames = If(colNames <> String.Empty, colNames + "," + "BIRTH_NATION_NAME", "BIRTH_NATION_NAME")
                    End If
                    If (If(familyEdit.BIRTH_PROVINCE_ID.ToString Is Nothing, "", familyEdit.BIRTH_PROVINCE_ID.ToString) <> If(family.BIRTH_PROVINCE_ID.ToString Is Nothing, "", family.BIRTH_PROVINCE_ID.ToString)) Then
                        colNames = If(colNames <> String.Empty, colNames + "," + "BIRTH_PROVINCE_NAME", "BIRTH_PROVINCE_NAME")
                    End If
                    If (If(familyEdit.BIRTH_DISTRICT_ID.ToString Is Nothing, "", familyEdit.BIRTH_DISTRICT_ID.ToString) <> If(family.BIRTH_DISTRICT_ID.ToString Is Nothing, "", family.BIRTH_DISTRICT_ID.ToString)) Then
                        colNames = If(colNames <> String.Empty, colNames + "," + "BIRTH_DISTRICT_NAME", "BIRTH_DISTRICT_NAME")
                    End If
                    If (If(familyEdit.BIRTH_WARD_ID.ToString Is Nothing, "", familyEdit.BIRTH_WARD_ID.ToString) <> If(family.BIRTH_WARD_ID.ToString Is Nothing, "", family.BIRTH_WARD_ID.ToString)) Then
                        colNames = If(colNames <> String.Empty, colNames + "," + "BIRTH_WARD_NAME", "BIRTH_WARD_NAME")
                    End If
                    If (If(passEdit.ToString Is Nothing, "", passEdit.ToString) <> If(pass.ToString Is Nothing, "", pass.ToString)) Then
                        colNames = If(colNames <> String.Empty, colNames + "," + "IS_PASS", "IS_PASS")
                    End If
                    If (If(deductEdit.ToString Is Nothing, "", deductEdit.ToString) <> If(deduct.ToString Is Nothing, "", deduct.ToString)) Then
                        colNames = If(colNames <> String.Empty, colNames + "," + "IS_DEDUCT", "IS_DEDUCT")
                    End If
                    If (If(familyEdit.DEDUCT_REG Is Nothing, "", familyEdit.DEDUCT_REG.ToString()) <> If(family.DEDUCT_REG Is Nothing, "", family.DEDUCT_REG.ToString())) Then
                        colNames = If(colNames <> String.Empty, colNames + "," + "DEDUCT_REG", "DEDUCT_REG")
                    End If
                    If (If(familyEdit.DEDUCT_FROM Is Nothing, "", familyEdit.DEDUCT_FROM.ToString()) <> If(family.DEDUCT_FROM Is Nothing, "", family.DEDUCT_FROM.ToString())) Then
                        colNames = If(colNames <> String.Empty, colNames + "," + "DEDUCT_FROM", "DEDUCT_FROM")
                    End If
                    If (If(familyEdit.DEDUCT_TO Is Nothing, "", familyEdit.DEDUCT_TO.ToString()) <> If(family.DEDUCT_TO Is Nothing, "", family.DEDUCT_TO.ToString())) Then
                        colNames = If(colNames <> String.Empty, colNames + "," + "DEDUCT_TO", "DEDUCT_TO")
                    End If
                    If (If(familyEdit.REMARK Is Nothing, "", familyEdit.REMARK) <> If(family.REMARK Is Nothing, "", family.REMARK)) Then
                        colNames = If(colNames <> String.Empty, colNames + "," + "REMARK", "REMARK")
                    End If
                    dic.Add(familyEdit.ID.ToString, colNames)
                End If
            Next
            Return dic
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function InsertEmployeeFamilyEdit(ByVal objFamilyEdit As FamilyEditDTO,
                                             ByVal log As UserLog,
                                             ByRef gID As Decimal) As Boolean
        Try

            Dim objFamilyEditData As New HU_FAMILY_EDIT
            objFamilyEditData.ID = Utilities.GetNextSequence(Context, Context.HU_FAMILY_EDIT.EntitySet.Name)
            objFamilyEditData.EMPLOYEE_ID = objFamilyEdit.EMPLOYEE_ID
            objFamilyEditData.FULLNAME = objFamilyEdit.FULLNAME
            objFamilyEditData.RELATION_ID = objFamilyEdit.RELATION_ID
            objFamilyEditData.BIRTH_DATE = objFamilyEdit.BIRTH_DATE
            objFamilyEditData.TAXTATION = objFamilyEdit.TAXTATION
            objFamilyEditData.DEDUCT_REG = objFamilyEdit.DEDUCT_REG
            objFamilyEditData.ID_NO = objFamilyEdit.ID_NO
            objFamilyEditData.IS_DEDUCT = objFamilyEdit.IS_DEDUCT
            objFamilyEditData.DEDUCT_FROM = objFamilyEdit.DEDUCT_FROM
            objFamilyEditData.DEDUCT_TO = objFamilyEdit.DEDUCT_TO
            objFamilyEditData.REMARK = objFamilyEdit.REMARK
            objFamilyEditData.ADDRESS = objFamilyEdit.ADDRESS
            objFamilyEditData.STATUS = If(objFamilyEdit.IS_SEND = 1, 1, 0)
            objFamilyEditData.REASON_UNAPROVE = objFamilyEdit.REASON_UNAPROVE
            objFamilyEditData.FK_PKEY = objFamilyEdit.ID ' If(objFamilyEdit.FK_PKEY IsNot Nothing AndAlso objFamilyEdit.FK_PKEY <> 0, objFamilyEdit.FK_PKEY, Nothing)
            objFamilyEditData.CAREER = objFamilyEdit.CAREER
            objFamilyEditData.TITLE_NAME = objFamilyEdit.TITLE_NAME
            objFamilyEditData.PROVINCE_ID = objFamilyEdit.PROVINCE_ID
            objFamilyEditData.CERTIFICATE_CODE = objFamilyEdit.CERTIFICATE_CODE
            objFamilyEditData.CERTIFICATE_NUM = objFamilyEdit.CERTIFICATE_NUM
            objFamilyEditData.ADDRESS_TT = objFamilyEdit.ADDRESS_TT
            objFamilyEditData.AD_PROVINCE_ID = If(objFamilyEdit.AD_PROVINCE_ID IsNot Nothing AndAlso objFamilyEdit.AD_PROVINCE_ID <> 0, objFamilyEdit.AD_PROVINCE_ID, Nothing)
            objFamilyEditData.AD_DISTRICT_ID = If(objFamilyEdit.AD_DISTRICT_ID IsNot Nothing AndAlso objFamilyEdit.AD_DISTRICT_ID <> 0, objFamilyEdit.AD_DISTRICT_ID, Nothing)
            objFamilyEditData.AD_WARD_ID = If(objFamilyEdit.AD_WARD_ID IsNot Nothing AndAlso objFamilyEdit.AD_WARD_ID <> 0, objFamilyEdit.AD_WARD_ID, Nothing)
            objFamilyEditData.AD_VILLAGE = objFamilyEdit.AD_VILLAGE
            objFamilyEditData.TT_PROVINCE_ID = If(objFamilyEdit.TT_PROVINCE_ID IsNot Nothing AndAlso objFamilyEdit.TT_PROVINCE_ID <> 0, objFamilyEdit.TT_PROVINCE_ID, Nothing)
            objFamilyEditData.TT_DISTRICT_ID = If(objFamilyEdit.TT_DISTRICT_ID IsNot Nothing AndAlso objFamilyEdit.TT_DISTRICT_ID <> 0, objFamilyEdit.TT_DISTRICT_ID, Nothing)
            objFamilyEditData.TT_WARD_ID = If(objFamilyEdit.TT_WARD_ID IsNot Nothing AndAlso objFamilyEdit.TT_WARD_ID <> 0, objFamilyEdit.TT_WARD_ID, Nothing)
            objFamilyEditData.IS_OWNER = objFamilyEdit.IS_OWNER
            objFamilyEditData.IS_PASS = objFamilyEdit.IS_PASS
            objFamilyEditData.NATIVE = objFamilyEdit.NATIVE
            objFamilyEditData.RELATE_OWNER = objFamilyEdit.RELATE_OWNER

            objFamilyEditData.NATION_ID = If(objFamilyEdit.NATION_ID IsNot Nothing AndAlso objFamilyEdit.NATION_ID <> 0, objFamilyEdit.NATION_ID, Nothing)
            objFamilyEditData.ID_NO_DATE = objFamilyEdit.ID_NO_DATE
            objFamilyEditData.ID_NO_PLACE_NAME = objFamilyEdit.ID_NO_PLACE_NAME
            objFamilyEditData.PHONE = objFamilyEdit.PHONE
            objFamilyEditData.TAXTATION_DATE = objFamilyEdit.TAXTATION_DATE
            objFamilyEditData.TAXTATION_PLACE = objFamilyEdit.TAXTATION_PLACE
            objFamilyEditData.BIRTH_CODE = objFamilyEdit.BIRTH_CODE
            objFamilyEditData.QUYEN = objFamilyEdit.QUYEN
            objFamilyEditData.BIRTH_NATION_ID = If(objFamilyEdit.BIRTH_NATION_ID IsNot Nothing AndAlso objFamilyEdit.BIRTH_NATION_ID <> 0, objFamilyEdit.BIRTH_NATION_ID, Nothing)
            objFamilyEditData.BIRTH_PROVINCE_ID = If(objFamilyEdit.BIRTH_PROVINCE_ID IsNot Nothing AndAlso objFamilyEdit.BIRTH_PROVINCE_ID <> 0, objFamilyEdit.BIRTH_PROVINCE_ID, Nothing)
            objFamilyEditData.BIRTH_DISTRICT_ID = If(objFamilyEdit.BIRTH_DISTRICT_ID IsNot Nothing AndAlso objFamilyEdit.BIRTH_DISTRICT_ID <> 0, objFamilyEdit.BIRTH_DISTRICT_ID, Nothing)
            objFamilyEditData.BIRTH_WARD_ID = If(objFamilyEdit.BIRTH_WARD_ID IsNot Nothing AndAlso objFamilyEdit.BIRTH_WARD_ID <> 0, objFamilyEdit.BIRTH_WARD_ID, Nothing)
            objFamilyEditData.GENDER = objFamilyEdit.GENDER
            objFamilyEditData.IS_SAME_COMPANY = objFamilyEdit.IS_SAME_COMPANY
            objFamilyEditData.SEND_DATE = objFamilyEdit.SEND_DATE
            If Not String.IsNullOrEmpty(objFamilyEdit.FILE_FAMILY) Then
                objFamilyEditData.FILE_FAMILY = objFamilyEdit.FILE_FAMILY
            End If
            If Not String.IsNullOrEmpty(objFamilyEdit.FILE_NPT) Then
                objFamilyEditData.FILE_NPT = objFamilyEdit.FILE_NPT
            End If
            Context.HU_FAMILY_EDIT.AddObject(objFamilyEditData)
            Context.SaveChanges(log)
            gID = objFamilyEditData.ID

            If objFamilyEdit.IS_SEND = 1 Then
                Dim lst = New List(Of Decimal)
                lst.Add(gID)
                SendEmployeeFamilyEdit(lst, log)
            End If

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function ModifyEmployeeFamilyEdit(ByVal objFamilyEdit As FamilyEditDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objFamilyEditData As New HU_FAMILY_EDIT With {.ID = objFamilyEdit.ID}
        Try
            objFamilyEditData = (From p In Context.HU_FAMILY_EDIT Where p.ID = objFamilyEdit.ID).FirstOrDefault
            'objFamilyEditData.EMPLOYEE_ID = objFamilyEdit.EMPLOYEE_ID
            objFamilyEditData.FULLNAME = objFamilyEdit.FULLNAME
            objFamilyEditData.RELATION_ID = objFamilyEdit.RELATION_ID
            objFamilyEditData.BIRTH_DATE = objFamilyEdit.BIRTH_DATE
            objFamilyEditData.DEDUCT_REG = objFamilyEdit.DEDUCT_REG
            objFamilyEditData.ID_NO = objFamilyEdit.ID_NO
            objFamilyEditData.TAXTATION = objFamilyEdit.TAXTATION
            objFamilyEditData.IS_DEDUCT = objFamilyEdit.IS_DEDUCT
            objFamilyEditData.DEDUCT_FROM = objFamilyEdit.DEDUCT_FROM
            objFamilyEditData.DEDUCT_TO = objFamilyEdit.DEDUCT_TO
            objFamilyEditData.REMARK = objFamilyEdit.REMARK
            objFamilyEditData.ADDRESS = objFamilyEdit.ADDRESS
            objFamilyEditData.REASON_UNAPROVE = objFamilyEdit.REASON_UNAPROVE
            'objFamilyEditData.FK_PKEY = If(objFamilyEdit.FK_PKEY IsNot Nothing AndAlso objFamilyEdit.FK_PKEY <> 0, objFamilyEdit.FK_PKEY, Nothing)
            objFamilyEditData.CAREER = objFamilyEdit.CAREER
            objFamilyEditData.TITLE_NAME = objFamilyEdit.TITLE_NAME
            objFamilyEditData.PROVINCE_ID = objFamilyEdit.PROVINCE_ID
            objFamilyEditData.CERTIFICATE_CODE = objFamilyEdit.CERTIFICATE_CODE
            objFamilyEditData.CERTIFICATE_NUM = objFamilyEdit.CERTIFICATE_NUM
            objFamilyEditData.ADDRESS_TT = objFamilyEdit.ADDRESS_TT
            objFamilyEditData.AD_PROVINCE_ID = If(objFamilyEdit.AD_PROVINCE_ID IsNot Nothing AndAlso objFamilyEdit.AD_PROVINCE_ID <> 0, objFamilyEdit.AD_PROVINCE_ID, Nothing)
            objFamilyEditData.AD_DISTRICT_ID = If(objFamilyEdit.AD_DISTRICT_ID IsNot Nothing AndAlso objFamilyEdit.AD_DISTRICT_ID <> 0, objFamilyEdit.AD_DISTRICT_ID, Nothing)
            objFamilyEditData.AD_WARD_ID = If(objFamilyEdit.AD_WARD_ID IsNot Nothing AndAlso objFamilyEdit.AD_WARD_ID <> 0, objFamilyEdit.AD_WARD_ID, Nothing)
            objFamilyEditData.AD_VILLAGE = objFamilyEdit.AD_VILLAGE
            objFamilyEditData.TT_PROVINCE_ID = If(objFamilyEdit.TT_PROVINCE_ID IsNot Nothing AndAlso objFamilyEdit.TT_PROVINCE_ID <> 0, objFamilyEdit.TT_PROVINCE_ID, Nothing)
            objFamilyEditData.TT_DISTRICT_ID = If(objFamilyEdit.TT_DISTRICT_ID IsNot Nothing AndAlso objFamilyEdit.TT_DISTRICT_ID <> 0, objFamilyEdit.TT_DISTRICT_ID, Nothing)
            objFamilyEditData.TT_WARD_ID = If(objFamilyEdit.TT_WARD_ID IsNot Nothing AndAlso objFamilyEdit.TT_WARD_ID <> 0, objFamilyEdit.TT_WARD_ID, Nothing)
            objFamilyEditData.IS_OWNER = objFamilyEdit.IS_OWNER
            objFamilyEditData.IS_PASS = objFamilyEdit.IS_PASS
            objFamilyEditData.NATIVE = objFamilyEdit.NATIVE
            objFamilyEditData.RELATE_OWNER = objFamilyEdit.RELATE_OWNER

            objFamilyEditData.NATION_ID = If(objFamilyEdit.NATION_ID IsNot Nothing AndAlso objFamilyEdit.NATION_ID <> 0, objFamilyEdit.NATION_ID, Nothing)
            objFamilyEditData.ID_NO_DATE = objFamilyEdit.ID_NO_DATE
            objFamilyEditData.ID_NO_PLACE_NAME = objFamilyEdit.ID_NO_PLACE_NAME
            objFamilyEditData.PHONE = objFamilyEdit.PHONE
            objFamilyEditData.TAXTATION_DATE = objFamilyEdit.TAXTATION_DATE
            objFamilyEditData.TAXTATION_PLACE = objFamilyEdit.TAXTATION_PLACE
            objFamilyEditData.BIRTH_CODE = objFamilyEdit.BIRTH_CODE
            objFamilyEditData.QUYEN = objFamilyEdit.QUYEN
            objFamilyEditData.BIRTH_NATION_ID = If(objFamilyEdit.BIRTH_NATION_ID IsNot Nothing AndAlso objFamilyEdit.BIRTH_NATION_ID <> 0, objFamilyEdit.BIRTH_NATION_ID, Nothing)
            objFamilyEditData.BIRTH_PROVINCE_ID = If(objFamilyEdit.BIRTH_PROVINCE_ID IsNot Nothing AndAlso objFamilyEdit.BIRTH_PROVINCE_ID <> 0, objFamilyEdit.BIRTH_PROVINCE_ID, Nothing)
            objFamilyEditData.BIRTH_DISTRICT_ID = If(objFamilyEdit.BIRTH_DISTRICT_ID IsNot Nothing AndAlso objFamilyEdit.BIRTH_DISTRICT_ID <> 0, objFamilyEdit.BIRTH_DISTRICT_ID, Nothing)
            objFamilyEditData.BIRTH_WARD_ID = If(objFamilyEdit.BIRTH_WARD_ID IsNot Nothing AndAlso objFamilyEdit.BIRTH_WARD_ID <> 0, objFamilyEdit.BIRTH_WARD_ID, Nothing)
            objFamilyEditData.GENDER = objFamilyEdit.GENDER

            objFamilyEditData.STATUS = If(objFamilyEdit.IS_SEND = 1, 1, 0)

            objFamilyEditData.IS_SAME_COMPANY = objFamilyEdit.IS_SAME_COMPANY
            If Not String.IsNullOrEmpty(objFamilyEdit.FILE_FAMILY) Then
                objFamilyEditData.FILE_FAMILY = objFamilyEdit.FILE_FAMILY
            End If
            If Not String.IsNullOrEmpty(objFamilyEdit.FILE_NPT) Then
                objFamilyEditData.FILE_NPT = objFamilyEdit.FILE_NPT
            End If
            Context.SaveChanges(log)
            gID = objFamilyEditData.ID

            If objFamilyEdit.IS_SEND = 1 Then
                Dim lst = New List(Of Decimal)
                lst.Add(gID)
                SendEmployeeFamilyEdit(lst, log)
            End If

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function GetEmployeeFamilyEdit(ByVal _filter As FamilyEditDTO) As List(Of FamilyEditDTO)
        Dim query As ObjectQuery(Of FamilyEditDTO)
        Try
            query = (From p In Context.HU_FAMILY_EDIT
                     Group Join m In Context.OT_OTHER_LIST On p.RELATION_ID Equals m.ID Into gGroup = Group
                     From p_g In gGroup.DefaultIfEmpty
                     Group Join n In Context.HU_PROVINCE On p.PROVINCE_ID Equals n.ID Into nGroup = Group
                     From n_g In nGroup.DefaultIfEmpty
                     From n_tt In Context.HU_NATION.Where(Function(F) F.ID = p.NATION_ID).DefaultIfEmpty
                     From n_ks In Context.HU_NATION.Where(Function(F) F.ID = p.BIRTH_NATION_ID).DefaultIfEmpty
                     From p_ks In Context.HU_PROVINCE.Where(Function(F) F.ID = p.BIRTH_PROVINCE_ID).DefaultIfEmpty
                     From d_ks In Context.HU_DISTRICT.Where(Function(F) F.ID = p.BIRTH_DISTRICT_ID).DefaultIfEmpty
                     From w_ks In Context.HU_WARD.Where(Function(F) F.ID = p.BIRTH_WARD_ID).DefaultIfEmpty
                     From g In Context.OT_OTHER_LIST.Where(Function(F) F.ID = p.GENDER).DefaultIfEmpty
                     From f_family In Context.HU_USERFILES.Where(Function(F) F.NAME = p.FILE_FAMILY).DefaultIfEmpty
                     From f_npt In Context.HU_USERFILES.Where(Function(F) F.NAME = p.FILE_NPT).DefaultIfEmpty
                     From ad_province In Context.HU_PROVINCE.Where(Function(F) F.ID = p.AD_PROVINCE_ID).DefaultIfEmpty
                     From ad_district In Context.HU_DISTRICT.Where(Function(F) F.ID = p.AD_DISTRICT_ID).DefaultIfEmpty
                     From ad_ward In Context.HU_WARD.Where(Function(F) F.ID = p.AD_WARD_ID).DefaultIfEmpty
                     From tt_province In Context.HU_PROVINCE.Where(Function(F) F.ID = p.TT_PROVINCE_ID).DefaultIfEmpty
                     From tt_distric In Context.HU_DISTRICT.Where(Function(F) F.ID = p.TT_DISTRICT_ID).DefaultIfEmpty
                     From tt_ward In Context.HU_WARD.Where(Function(F) F.ID = p.TT_WARD_ID).DefaultIfEmpty
                     From native In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.NATIVE).DefaultIfEmpty
                     From relate_owner In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.RELATE_OWNER).DefaultIfEmpty
                     Select New FamilyEditDTO With {
                         .ID = p.ID,
                         .STATUS_TAB = 2,
                         .ADDRESS = p.ADDRESS,
                         .EMPLOYEE_ID = p.EMPLOYEE_ID,
                         .FULLNAME = p.FULLNAME,
                         .RELATION_ID = p.RELATION_ID,
                         .RELATION_NAME = p_g.NAME_VN,
                         .PROVINCE_ID = p.PROVINCE_ID,
                         .PROVINCE_NAME = n_g.NAME_VN,
                         .BIRTH_DATE = p.BIRTH_DATE,
                         .TAXTATION = p.TAXTATION,
                         .DEDUCT_REG = p.DEDUCT_REG,
                         .ID_NO = p.ID_NO,
                         .IS_DEDUCT = p.IS_DEDUCT,
                         .DEDUCT_FROM = p.DEDUCT_FROM,
                         .DEDUCT_TO = p.DEDUCT_TO,
                         .REMARK = p.REMARK,
                         .CERTIFICATE_NUM = p.CERTIFICATE_NUM,
                         .CERTIFICATE_CODE = p.CERTIFICATE_CODE,
                         .ADDRESS_TT = p.ADDRESS_TT,
                         .AD_PROVINCE_ID = p.AD_PROVINCE_ID,
                         .AD_DISTRICT_ID = p.AD_DISTRICT_ID,
                         .AD_WARD_ID = p.AD_WARD_ID,
                         .AD_VILLAGE = p.AD_VILLAGE,
                         .TT_PROVINCE_ID = p.TT_PROVINCE_ID,
                         .TT_DISTRICT_ID = p.TT_DISTRICT_ID,
                         .TT_WARD_ID = p.TT_WARD_ID,
                         .IS_OWNER = p.IS_OWNER,
                         .IS_PASS = p.IS_PASS,
                         .REASON_UNAPROVE = p.REASON_UNAPROVE,
                         .FK_PKEY = p.FK_PKEY,
                         .TITLE_NAME = p.TITLE_NAME,
                         .CAREER = p.CAREER,
                         .NATION_ID = p.NATION_ID,
                         .NATION_NAME = n_tt.NAME_VN,
                         .ID_NO_DATE = p.ID_NO_DATE,
                         .ID_NO_PLACE_NAME = p.ID_NO_PLACE_NAME,
                         .PHONE = p.PHONE,
                         .TAXTATION_DATE = p.TAXTATION_DATE,
                         .TAXTATION_PLACE = p.TAXTATION_PLACE,
                         .BIRTH_CODE = p.BIRTH_CODE,
                         .QUYEN = p.QUYEN,
                         .BIRTH_NATION_ID = p.BIRTH_NATION_ID,
                         .BIRTH_PROVINCE_ID = p.BIRTH_PROVINCE_ID,
                         .BIRTH_DISTRICT_ID = p.BIRTH_DISTRICT_ID,
                         .BIRTH_WARD_ID = p.BIRTH_WARD_ID,
                         .BIRTH_NATION_NAME = n_ks.NAME_VN,
                         .BIRTH_PROVINCE_NAME = p_ks.NAME_VN,
                         .BIRTH_DISTRICT_NAME = d_ks.NAME_VN,
                         .BIRTH_WARD_NAME = w_ks.NAME_VN,
                         .AD_PROVINCE_NAME = ad_province.NAME_VN,
                         .AD_DISTRICT_NAME = ad_district.NAME_VN,
                         .AD_WARD_NAME = ad_ward.NAME_VN,
                         .TT_PROVINCE_NAME = tt_province.NAME_VN,
                         .TT_DISTRICT_NAME = tt_distric.NAME_VN,
                         .TT_WARD_NAME = tt_ward.NAME_VN,
                         .GENDER = p.GENDER,
                         .GENDER_NAME = g.NAME_VN,
                         .NATIVE = p.NATIVE,
                         .NATIVE_NAME = native.NAME_VN,
                         .RELATE_OWNER = p.RELATE_OWNER,
                         .RELATE_OWNER_NAME = relate_owner.NAME_VN,
                         .STATUS = p.STATUS,
                         .IS_SAME_COMPANY = p.IS_SAME_COMPANY,
                         .UPLOAD_FILE_FAMILY = f_family.NAME,
                         .UPLOAD_FILE_NPT = f_npt.NAME,
                         .FILE_FAMILY = f_family.FILE_NAME,
                         .FILE_NPT = f_npt.FILE_NAME,
                         .STATUS_NAME = If(p.STATUS = 0, "Chưa gửi duyệt",
                                           If(p.STATUS = 1, "Chờ phê duyệt",
                                              If(p.STATUS = 2, "Phê duyệt",
                                                 If(p.STATUS = 3, "Không phê duyệt", ""))))})

            If _filter.EMPLOYEE_ID <> 0 Then
                query = query.Where(Function(p) p.EMPLOYEE_ID = _filter.EMPLOYEE_ID)
            End If
            If _filter.STATUS <> "" Then
                query = query.Where(Function(p) p.STATUS = _filter.STATUS)
            End If
            If _filter.ID <> 0 Then
                query = query.Where(Function(p) p.ID = _filter.ID)
            End If

            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function GetApproveEmployeeCertificateEdit(ByVal _filter As HU_PRO_TRAIN_OUT_COMPANYDTOEDIT,
                                         ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer, ByVal _param As ParamDTO,
                                         Optional ByVal Sorts As String = "EMPLOYEE_CODE desc",
                                         Optional ByVal log As UserLog = Nothing) As List(Of HU_PRO_TRAIN_OUT_COMPANYDTOEDIT)
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using
            Dim query = (From p In Context.HU_PRO_TRAIN_OUT_COMPANY_EDIT
                         From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID).DefaultIfEmpty
                         From o1 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.MAJOR).DefaultIfEmpty
                         From ot In Context.OT_OTHER_LIST.Where(Function(F) F.ID = p.FORM_TRAIN_ID).DefaultIfEmpty
                         From OT1 In Context.OT_OTHER_LIST.Where(Function(F) F.ID = p.CERTIFICATE).DefaultIfEmpty
                         From OT2 In Context.OT_OTHER_LIST.Where(Function(F) F.ID = p.LEVEL_ID).DefaultIfEmpty
                         From OT3 In Context.OT_OTHER_LIST.Where(Function(F) F.ID = p.CERTIFICATE_GROUP_ID).DefaultIfEmpty
                         From OT4 In Context.OT_OTHER_LIST.Where(Function(F) F.ID = p.CERTIFICATE_TYPE_ID).DefaultIfEmpty
                         From file In Context.HU_USERFILES.Where(Function(f) f.NAME = p.FILE_NAME).DefaultIfEmpty
                         From o2 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.GRADUATE_SCHOOL).DefaultIfEmpty
                         From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = e.ORG_ID And f.USERNAME = log.Username.ToUpper)
                         Where p.STATUS = "1"
                         Select New HU_PRO_TRAIN_OUT_COMPANYDTOEDIT With {
                          .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                          .EMPLOYEE_NAME = e.FULLNAME_VN,
                          .ID = p.ID,
                          .EMPLOYEE_ID = p.EMPLOYEE_ID,
                          .FROM_DATE = p.FROM_DATE,
                          .TO_DATE = p.TO_DATE,
                          .RECEIVE_DEGREE_DATE = p.RECEIVE_DEGREE_DATE,
                          .YEAR_GRA = p.YEAR_GRA,
                          .NAME_SHOOLS = p.NAME_SHOOLS,
                          .FORM_TRAIN_ID = p.FORM_TRAIN_ID,
                          .FORM_TRAIN_NAME = ot.NAME_VN,
                          .SPECIALIZED_TRAIN = p.SPECIALIZED_TRAIN,
                          .RESULT_TRAIN = p.RESULT_TRAIN,
                          .CERTIFICATE = OT1.NAME_VN,
                          .CERTIFICATE_GROUP_ID = p.CERTIFICATE_GROUP_ID,
                          .CERTIFICATE_GROUP_NAME = OT3.NAME_VN,
                          .CERTIFICATE_TYPE_ID = p.CERTIFICATE_TYPE_ID,
                          .CERTIFICATE_TYPE_NAME = OT4.NAME_VN,
                          .CERTIFICATE_ID = p.CERTIFICATE,
                          .EFFECTIVE_DATE_FROM = p.EFFECTIVE_DATE_FROM,
                          .EFFECTIVE_DATE_TO = p.EFFECTIVE_DATE_TO,
                          .CREATED_BY = p.CREATED_BY,
                          .IS_RENEWED = If(p.IS_RENEW = -1, True, False),
                          .CONTENT_TRAIN = p.CONTENT_TRAIN,
                          .LEVEL_ID = p.LEVEL_ID,
                          .LEVEL_NAME = OT2.NAME_VN,
                          .POINT_LEVEL = p.POINT_LEVEL,
                          .SCORE = p.SCORE,
                          .CODE_CERTIFICATE = p.CODE_CERTIFICATE,
                          .REMARK = p.REMARK,
                          .NOTE = p.NOTE,
                          .TYPE_TRAIN_ID = p.TYPE_TRAIN_ID,
                          .TYPE_TRAIN_NAME = p.TYPE_TRAIN_NAME,
                          .CREATED_DATE = p.CREATED_DATE,
                          .CREATED_LOG = p.CREATED_LOG,
                          .MODIFIED_BY = p.MODIFIED_BY,
                          .MODIFIED_DATE = p.MODIFIED_DATE,
                          .MODIFIED_LOG = p.MODIFIED_LOG,
                          .REASON_UNAPROVE = p.REASON_UNAPROVE,
                          .FK_PKEY = p.FK_PKEY,
                          .UPLOAD_FILE = file.NAME,
                          .FILE_NAME = file.FILE_NAME,
                          .STATUS = p.STATUS,
                          .GRADUATE_SCHOOL = p.GRADUATE_SCHOOL,
                          .GRADUATE_SCHOOL_NAME = o2.NAME_VN,
                          .CONTENT_LEVEL = p.CONTENT_LEVEL,
                          .CERTIFICATE_NAME = p.CERTIFICATE_NAME,
                          .MAJOR = p.MAJOR,
                          .MAJOR_NAME = o1.NAME_VN,
                          .IS_MAIN = If(p.IS_MAIN = -1, True, False),
                          .IS_MAJOR = If(p.IS_MAJOR = -1, True, False),
                          .STATUS_NAME = If(p.STATUS = 0, "Chưa gửi duyệt",
                                             If(p.STATUS = 1, "Chờ phê duyệt",
                                                If(p.STATUS = 2, "Phê duyệt",
                                                   If(p.STATUS = 3, "Không phê duyệt", ""))))})
            Return query.ToList()
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetChangedEmployeeCertificateList(ByVal lstEmpEdit As List(Of HU_PRO_TRAIN_OUT_COMPANYDTOEDIT)) As Dictionary(Of String, String)
        Try
            Dim dic As New Dictionary(Of String, String)
            For Each empEdit As HU_PRO_TRAIN_OUT_COMPANYDTOEDIT In lstEmpEdit
                Dim colNames As String = String.Empty
                Dim empCertificate = Context.HU_PRO_TRAIN_OUT_COMPANY.Where(Function(f) f.ID = empEdit.FK_PKEY).FirstOrDefault
                If empCertificate IsNot Nothing Then
                    If (If(empEdit.CERTIFICATE_ID Is Nothing, "", empEdit.CERTIFICATE_ID.ToString()) <> If(empCertificate.CERTIFICATE Is Nothing, "", empCertificate.CERTIFICATE.ToString())) Then
                        colNames = If(colNames <> String.Empty, colNames + "," + "CERTIFICATE", "CERTIFICATE")
                    End If

                    If (If(empEdit.CERTIFICATE_GROUP_ID Is Nothing, "", empEdit.CERTIFICATE_GROUP_ID.ToString()) <> If(empCertificate.CERTIFICATE_GROUP_ID Is Nothing, "", empCertificate.CERTIFICATE_GROUP_ID.ToString())) Then
                        colNames = If(colNames <> String.Empty, colNames + "," + "CERTIFICATE_GROUP_NAME", "CERTIFICATE_GROUP_NAME")
                    End If

                    If (If(empEdit.CERTIFICATE_TYPE_ID Is Nothing, "", empEdit.CERTIFICATE_TYPE_ID.ToString()) <> If(empCertificate.CERTIFICATE_TYPE_ID Is Nothing, "", empCertificate.CERTIFICATE_TYPE_ID.ToString())) Then
                        colNames = If(colNames <> String.Empty, colNames + "," + "CERTIFICATE_TYPE_NAME", "CERTIFICATE_TYPE_NAME")
                    End If

                    If (If(empEdit.CERTIFICATE_NAME Is Nothing, "", empEdit.CERTIFICATE_NAME.ToString()) <> If(empCertificate.CERTIFICATE_NAME Is Nothing, "", empCertificate.CERTIFICATE_NAME.ToString())) Then
                        colNames = If(colNames <> String.Empty, colNames + "," + "CERTIFICATE_NAME", "CERTIFICATE_NAME")
                    End If

                    If (If(empEdit.FROM_DATE Is Nothing, "", empEdit.FROM_DATE.ToString()) <> If(empCertificate.FROM_DATE Is Nothing, "", empCertificate.FROM_DATE.ToString())) Then
                        colNames = If(colNames <> String.Empty, colNames + "," + "FROM_DATE", "FROM_DATE")
                    End If

                    If (If(empEdit.TO_DATE Is Nothing, "", empEdit.TO_DATE.ToString()) <> If(empCertificate.TO_DATE Is Nothing, "", empCertificate.TO_DATE.ToString())) Then
                        colNames = If(colNames <> String.Empty, colNames + "," + "TO_DATE", "TO_DATE")
                    End If

                    If (If(empEdit.GRADUATE_SCHOOL Is Nothing, "", empEdit.GRADUATE_SCHOOL.ToString()) <> If(empCertificate.GRADUATE_SCHOOL Is Nothing, "", empCertificate.GRADUATE_SCHOOL.ToString())) Then
                        colNames = If(colNames <> String.Empty, colNames + "," + "GRADUATE_SCHOOL_NAME", "GRADUATE_SCHOOL_NAME")
                    End If

                    If (If(empEdit.MAJOR Is Nothing, "", empEdit.MAJOR.ToString()) <> If(empCertificate.MAJOR Is Nothing, "", empCertificate.MAJOR.ToString())) Then
                        colNames = If(colNames <> String.Empty, colNames + "," + "MAJOR_NAME", "MAJOR_NAME")
                    End If

                    If (If(empEdit.SPECIALIZED_TRAIN Is Nothing, "", empEdit.SPECIALIZED_TRAIN.ToString()) <> If(empCertificate.SPECIALIZED_TRAIN Is Nothing, "", empCertificate.SPECIALIZED_TRAIN.ToString())) Then
                        colNames = If(colNames <> String.Empty, colNames + "," + "SPECIALIZED_TRAIN", "SPECIALIZED_TRAIN")
                    End If

                    If (If(empEdit.LEVEL_ID Is Nothing, "", empEdit.LEVEL_ID.ToString()) <> If(empCertificate.LEVEL_ID Is Nothing, "", empCertificate.LEVEL_ID.ToString())) Then
                        colNames = If(colNames <> String.Empty, colNames + "," + "LEVEL_NAME", "LEVEL_NAME")
                    End If

                    If (If(empEdit.POINT_LEVEL Is Nothing, "", empEdit.POINT_LEVEL.ToString()) <> If(empCertificate.POINT_LEVEL Is Nothing, "", empCertificate.POINT_LEVEL.ToString())) Then
                        colNames = If(colNames <> String.Empty, colNames + "," + "POINT_LEVEL", "POINT_LEVEL")
                    End If

                    If (If(empEdit.CONTENT_LEVEL Is Nothing, "", empEdit.CONTENT_LEVEL.ToString()) <> If(empCertificate.CONTENT_LEVEL Is Nothing, "", empCertificate.CONTENT_LEVEL.ToString())) Then
                        colNames = If(colNames <> String.Empty, colNames + "," + "CONTENT_LEVEL", "CONTENT_LEVEL")
                    End If

                    If (If(empEdit.FORM_TRAIN_ID Is Nothing, "", empEdit.FORM_TRAIN_ID.ToString()) <> If(empCertificate.FORM_TRAIN_ID Is Nothing, "", empCertificate.FORM_TRAIN_ID.ToString())) Then
                        colNames = If(colNames <> String.Empty, colNames + "," + "FORM_TRAIN_NAME", "FORM_TRAIN_NAME")
                    End If

                    If (If(empEdit.EFFECTIVE_DATE_FROM Is Nothing, "", empEdit.EFFECTIVE_DATE_FROM.ToString()) <> If(empCertificate.EFFECTIVE_DATE_FROM Is Nothing, "", empCertificate.EFFECTIVE_DATE_FROM.ToString())) Then
                        colNames = If(colNames <> String.Empty, colNames + "," + "EFFECTIVE_DATE_FROM", "EFFECTIVE_DATE_FROM")
                    End If

                    If (If(empEdit.EFFECTIVE_DATE_TO Is Nothing, "", empEdit.EFFECTIVE_DATE_TO.ToString()) <> If(empCertificate.EFFECTIVE_DATE_TO Is Nothing, "", empCertificate.EFFECTIVE_DATE_TO.ToString())) Then
                        colNames = If(colNames <> String.Empty, colNames + "," + "EFFECTIVE_DATE_TO", "EFFECTIVE_DATE_TO")
                    End If

                    If (If(empEdit.RESULT_TRAIN Is Nothing, "", empEdit.RESULT_TRAIN.ToString()) <> If(empCertificate.RESULT_TRAIN Is Nothing, "", empCertificate.RESULT_TRAIN.ToString())) Then
                        colNames = If(colNames <> String.Empty, colNames + "," + "RESULT_TRAIN", "RESULT_TRAIN")
                    End If

                    If (If(empEdit.YEAR_GRA Is Nothing, "", empEdit.YEAR_GRA.ToString()) <> If(empCertificate.YEAR_GRA Is Nothing, "", empCertificate.YEAR_GRA.ToString())) Then
                        colNames = If(colNames <> String.Empty, colNames + "," + "YEAR_GRA", "YEAR_GRA")
                    End If

                    If (If(empEdit.NOTE Is Nothing, "", empEdit.NOTE.ToString()) <> If(empCertificate.NOTE Is Nothing, "", empCertificate.NOTE.ToString())) Then
                        colNames = If(colNames <> String.Empty, colNames + "," + "NOTE", "NOTE")
                    End If

                    If (If(empEdit.IS_MAIN Is Nothing, 0, If(empEdit.IS_MAIN.ToString().ToLower = "false", 0, -1)) <> If(empCertificate.IS_MAIN Is Nothing, 0, empCertificate.IS_MAIN.ToString())) Then
                        colNames = If(colNames <> String.Empty, colNames + "," + "IS_MAIN", "IS_MAIN")
                    End If

                    'If (If(empEdit.IS_MAJOR Is Nothing, "", empEdit.IS_MAJOR.ToString()) <> If(empCertificate.IS_MAJOR Is Nothing, "", empCertificate.IS_MAJOR.ToString())) Then
                    '    colNames = If(colNames <> String.Empty, colNames + "," + "IS_MAJOR", "IS_MAJOR")
                    'End If

                    dic.Add(empEdit.ID.ToString, colNames)
                End If
            Next
            Return dic
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetApproveFamilyEdit(ByVal _filter As FamilyEditDTO,
                                         ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer, ByVal _param As ParamDTO,
                                         Optional ByVal Sorts As String = "EMPLOYEE_CODE desc",
                                         Optional ByVal log As UserLog = Nothing) As List(Of FamilyEditDTO)

        Try

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using


            Dim query = (From p In Context.HU_FAMILY_EDIT
                         From p_g In Context.OT_OTHER_LIST.Where(Function(f) p.RELATION_ID = f.ID).DefaultIfEmpty
                         From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID).DefaultIfEmpty
                         From gender In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.GENDER).DefaultIfEmpty
                         From nation In Context.HU_NATION.Where(Function(f) f.ID = p.NATION_ID).DefaultIfEmpty
                         From pro In Context.HU_PROVINCE.Where(Function(f) f.ID = p.AD_PROVINCE_ID).DefaultIfEmpty
                         From dis In Context.HU_DISTRICT.Where(Function(f) f.ID = p.AD_DISTRICT_ID).DefaultIfEmpty
                         From ward In Context.HU_WARD.Where(Function(f) f.ID = p.AD_WARD_ID).DefaultIfEmpty
                         From tt_pro In Context.HU_PROVINCE.Where(Function(f) f.ID = p.TT_PROVINCE_ID).DefaultIfEmpty
                         From tt_dis In Context.HU_DISTRICT.Where(Function(f) f.ID = p.TT_DISTRICT_ID).DefaultIfEmpty
                         From tt_ward In Context.HU_WARD.Where(Function(f) f.ID = p.TT_WARD_ID).DefaultIfEmpty
                         From b_nation In Context.HU_NATION.Where(Function(f) f.ID = p.BIRTH_NATION_ID).DefaultIfEmpty
                         From b_pro In Context.HU_PROVINCE.Where(Function(f) f.ID = p.BIRTH_PROVINCE_ID).DefaultIfEmpty
                         From b_dis In Context.HU_DISTRICT.Where(Function(f) f.ID = p.BIRTH_DISTRICT_ID).DefaultIfEmpty
                         From b_ward In Context.HU_WARD.Where(Function(f) f.ID = p.BIRTH_WARD_ID).DefaultIfEmpty
                         From f_family In Context.HU_USERFILES.Where(Function(F) F.NAME = p.FILE_FAMILY).DefaultIfEmpty
                         From f_npt In Context.HU_USERFILES.Where(Function(F) F.NAME = p.FILE_NPT).DefaultIfEmpty
                         From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                         From native In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.NATIVE).DefaultIfEmpty
                         From relate_owner In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.RELATE_OWNER).DefaultIfEmpty
                         From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = e.ORG_ID And f.USERNAME = log.Username.ToUpper)
                         Where (p.STATUS = 1)
                         Select New FamilyEditDTO With {
                            .ID = p.ID,
                            .ADDRESS = p.ADDRESS,
                            .ADDRESS_TT = p.ADDRESS_TT,
                            .NATION_ID = p.NATION_ID,
                            .NATION_NAME = nation.NAME_VN,
                            .EMPLOYEE_ID = p.EMPLOYEE_ID,
                            .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                            .EMPLOYEE_NAME = e.FULLNAME_VN,
                            .EMPLOYEE_ORG = org.NAME_VN,
                            .FULLNAME = p.FULLNAME,
                            .RELATION_ID = p.RELATION_ID,
                            .RELATION_NAME = p_g.NAME_VN,
                            .BIRTH_DATE = p.BIRTH_DATE,
                            .BIRTH_CODE = p.BIRTH_CODE,
                            .GENDER = p.GENDER,
                            .GENDER_NAME = gender.NAME_VN,
                            .NATIVE_NAME = native.NAME_VN,
                            .RELATE_OWNER_NAME = relate_owner.NAME_VN,
                            .ID_NO = p.ID_NO,
                            .ID_NO_DATE = p.ID_NO_DATE,
                            .ID_NO_PLACE_NAME = p.ID_NO_PLACE_NAME,
                            .IS_OWNER = p.IS_OWNER,
                            .CERTIFICATE_NUM = p.CERTIFICATE_NUM,
                            .CERTIFICATE_CODE = p.CERTIFICATE_CODE,
                            .IS_DEDUCT = p.IS_DEDUCT,
                            .DEDUCT_REG = p.DEDUCT_REG,
                            .DEDUCT_FROM = p.DEDUCT_FROM,
                            .DEDUCT_TO = p.DEDUCT_TO,
                            .TITLE_ID = e.TITLE_ID,
                            .TITLE_NAME = p.TITLE_NAME,
                            .CAREER = p.CAREER,
                            .FK_PKEY = p.FK_PKEY,
                            .AD_PROVINCE_ID = p.AD_PROVINCE_ID,
                            .AD_PROVINCE_NAME = pro.NAME_VN,
                            .AD_DISTRICT_ID = p.AD_DISTRICT_ID,
                            .AD_DISTRICT_NAME = dis.NAME_VN,
                            .AD_WARD_ID = p.AD_WARD_ID,
                            .AD_WARD_NAME = ward.NAME_VN,
                            .AD_VILLAGE = p.AD_VILLAGE,
                            .TT_PROVINCE_ID = p.TT_PROVINCE_ID,
                            .TT_PROVINCE_NAME = tt_pro.NAME_VN,
                            .TT_DISTRICT_ID = p.TT_DISTRICT_ID,
                            .TT_DISTRICT_NAME = tt_dis.NAME_VN,
                            .TT_WARD_ID = p.TT_WARD_ID,
                            .TT_WARD_NAME = tt_ward.NAME_VN,
                            .PHONE = p.PHONE,
                            .TAXTATION = p.TAXTATION,
                            .TAXTATION_DATE = p.TAXTATION_DATE,
                            .TAXTATION_PLACE = p.TAXTATION_PLACE,
                            .QUYEN = p.QUYEN,
                            .BIRTH_NATION_ID = p.BIRTH_NATION_ID,
                            .BIRTH_NATION_NAME = b_nation.NAME_VN,
                            .BIRTH_PROVINCE_ID = p.BIRTH_PROVINCE_ID,
                            .BIRTH_PROVINCE_NAME = b_pro.NAME_VN,
                            .BIRTH_DISTRICT_ID = p.BIRTH_DISTRICT_ID,
                            .BIRTH_DISTRICT_NAME = b_dis.NAME_VN,
                            .BIRTH_WARD_ID = p.BIRTH_WARD_ID,
                            .BIRTH_WARD_NAME = b_ward.NAME_VN,
                            .IS_PASS = p.IS_PASS,
                            .REMARK = p.REMARK,
                            .STATUS = p.STATUS,
                            .UPLOAD_FILE_FAMILY = f_family.NAME,
                            .UPLOAD_FILE_NPT = f_npt.NAME,
                            .FILE_FAMILY = f_family.FILE_NAME,
                            .FILE_NPT = f_npt.FILE_NAME,
                            .IS_SAME_COMPANY = p.IS_SAME_COMPANY,
                            .STATUS_NAME = If(p.STATUS = 0, "Chưa gửi duyệt",
                                              If(p.STATUS = 1, "Chờ phê duyệt",
                                                 If(p.STATUS = 2, "Phê duyệt",
                                                    If(p.STATUS = 3, "Không phê duyệt", ""))))})


            If _filter.DEDUCT_REG IsNot Nothing Then
                query = query.Where(Function(p) p.DEDUCT_REG = _filter.DEDUCT_REG)
            End If

            If _filter.EMPLOYEE_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.EMPLOYEE_NAME.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If

            If _filter.EMPLOYEE_CODE IsNot Nothing Then
                query = query.Where(Function(p) p.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If

            If _filter.FULLNAME IsNot Nothing Then
                query = query.Where(Function(p) p.FULLNAME.ToUpper.Contains(_filter.FULLNAME.ToUpper))
            End If

            If _filter.ADDRESS IsNot Nothing Then
                query = query.Where(Function(p) p.ADDRESS.ToUpper.Contains(_filter.ADDRESS.ToUpper))
            End If

            If _filter.RELATION_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.RELATION_NAME.ToUpper.Contains(_filter.RELATION_NAME.ToUpper))
            End If

            If _filter.STATUS_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.STATUS_NAME.ToUpper.Contains(_filter.STATUS_NAME.ToUpper))
            End If

            If _filter.ID_NO IsNot Nothing Then
                query = query.Where(Function(p) p.ID_NO.ToUpper.Contains(_filter.ID_NO.ToUpper))
            End If

            If _filter.REMARK IsNot Nothing Then
                query = query.Where(Function(p) p.REMARK.ToUpper.Contains(_filter.REMARK.ToUpper))
            End If

            If _filter.DEDUCT_TO IsNot Nothing Then
                query = query.Where(Function(p) p.DEDUCT_TO = _filter.DEDUCT_TO)
            End If

            If _filter.DEDUCT_FROM IsNot Nothing Then
                query = query.Where(Function(p) p.DEDUCT_FROM = _filter.DEDUCT_FROM)
            End If

            If _filter.IS_DEDUCT IsNot Nothing Then
                query = query.Where(Function(p) p.IS_DEDUCT = _filter.IS_DEDUCT)
            End If
            Dim working = query

            working = working.OrderBy(Sorts)
            Total = working.Count
            working = working.Skip(PageIndex * PageSize).Take(PageSize)

            Return working.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function DeleteEmployeeFamilyEdit(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim lst As List(Of HU_FAMILY_EDIT)
        Try
            lst = (From p In Context.HU_FAMILY_EDIT Where lstDecimals.Contains(p.ID)).ToList
            For i As Int16 = 0 To lst.Count - 1
                Context.HU_FAMILY_EDIT.DeleteObject(lst(i))
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function CheckExistFamilyEdit(ByVal pk_key As Decimal, ByVal tab As Decimal) As FamilyEditDTO
        Try
            'Dim query = (From p In Context.HU_FAMILY_EDIT
            '             Where p.STATUS <> 2 And p.STATUS <> 3 And p.FK_PKEY = pk_key
            '             Select New FamilyEditDTO With {
            '                 .ID = p.ID,
            '                 .STATUS = p.STATUS}).FirstOrDefault

            'Return query

            Dim re As New FamilyEditDTO
            Dim re1 = 0

            If pk_key = 0 Then
                re.STATUS = 0
            ElseIf tab = 1 Then
                re1 = (From p In Context.HU_FAMILY_EDIT Where p.FK_PKEY = pk_key Select p).Count
                If re1 = 0 Then
                    re.STATUS = 0
                Else
                    re.STATUS = 1
                End If
            ElseIf tab = 2 Then
                re.STATUS = 1
            End If
            Return re

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function SendEmployeeFamilyEdit(ByVal lstID As List(Of Decimal),
                                           ByVal log As UserLog) As Boolean

        Try
            Dim lstObj = (From p In Context.HU_FAMILY_EDIT Where lstID.Contains(p.ID)).ToList
            For Each item In lstObj
                item.SEND_DATE = Date.Now
                item.STATUS = 1
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function
    Public Function ReadNotifi(ByVal id As Decimal) As Decimal
        Try
            Dim noti = (From p In Context.SE_NOTIFICATION Where p.ID = id).FirstOrDefault
            noti.SEND_LOG = "4"
            noti.SENT_DATE = Date.Now
            Context.SaveChanges()
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function UpdateStatusEmployeeFamilyEdit(ByVal lstID As List(Of Decimal),
                                                   status As String,
                                                   ByVal log As UserLog) As Boolean
        Dim lst As List(Of HU_FAMILY_EDIT)
        Dim sStatus() As String = status.Split(":")
        Dim emp_from = (From p In Context.SE_USER Where p.USERNAME = log.Username.ToUpper Select p.EMPLOYEE_ID).FirstOrDefault

        Try
            lst = (From p In Context.HU_FAMILY_EDIT Where lstID.Contains(p.ID)).ToList
            For Each item In lst
                item.STATUS = sStatus(0)
                item.REASON_UNAPROVE = sStatus(1)

                Dim noti As New SE_NOTIFICATION
                noti.ID = Utilities.GetNextSequence(Context, Context.SE_NOTIFICATION.EntitySet.Name)
                noti.FROM_EMPLOYEE_ID = emp_from
                noti.TO_EMPLOYEE_ID = item.EMPLOYEE_ID
                noti.PROCESS_TYPE = "EMP_RELATION"
                noti.SEND_STATUS = sStatus(0)
                noti.MESSAGE = sStatus(1)
                noti.SENT_DATE = System.DateTime.Now
                Context.SE_NOTIFICATION.AddObject(noti)

                If sStatus(0) = 2 Then
                    If item.FK_PKEY IsNot Nothing And item.FK_PKEY <> 0 Then
                        Dim objFamilyData As HU_FAMILY
                        objFamilyData = (From p In Context.HU_FAMILY Where p.ID = item.FK_PKEY).FirstOrDefault
                        objFamilyData.EMPLOYEE_ID = item.EMPLOYEE_ID
                        objFamilyData.FULLNAME = item.FULLNAME
                        objFamilyData.RELATION_ID = item.RELATION_ID
                        objFamilyData.BIRTH_DATE = item.BIRTH_DATE
                        objFamilyData.TAXTATION = item.TAXTATION
                        objFamilyData.DEDUCT_REG = item.DEDUCT_REG
                        objFamilyData.ID_NO = item.ID_NO
                        objFamilyData.IS_DEDUCT = item.IS_DEDUCT
                        objFamilyData.DEDUCT_FROM = item.DEDUCT_FROM
                        objFamilyData.DEDUCT_TO = item.DEDUCT_TO
                        objFamilyData.REMARK = item.REMARK
                        objFamilyData.ADDRESS = item.ADDRESS
                        'cap nhat them day du cac truong issue 297 TNG
                        objFamilyData.ADDRESS_TT = item.ADDRESS_TT
                        objFamilyData.CERTIFICATE_CODE = item.CERTIFICATE_CODE
                        objFamilyData.CERTIFICATE_NUM = item.CERTIFICATE_NUM
                        objFamilyData.IS_OWNER = item.IS_OWNER
                        objFamilyData.AD_PROVINCE_ID = item.AD_PROVINCE_ID
                        objFamilyData.AD_DISTRICT_ID = item.AD_DISTRICT_ID
                        objFamilyData.AD_WARD_ID = item.AD_WARD_ID
                        objFamilyData.TT_PROVINCE_ID = item.TT_PROVINCE_ID
                        objFamilyData.TT_DISTRICT_ID = item.TT_DISTRICT_ID
                        objFamilyData.TT_WARD_ID = item.TT_WARD_ID
                        objFamilyData.IS_PASS = item.IS_PASS
                        objFamilyData.AD_VILLAGE = item.AD_VILLAGE
                        objFamilyData.NATION_ID = item.NATION_ID
                        objFamilyData.ID_NO_DATE = item.ID_NO_DATE
                        objFamilyData.ID_NO_PLACE_NAME = item.ID_NO_PLACE_NAME
                        objFamilyData.PHONE = item.PHONE
                        objFamilyData.TAXTATION_DATE = item.TAXTATION_DATE
                        objFamilyData.TAXTATION_PLACE = item.TAXTATION_PLACE
                        objFamilyData.BIRTH_CODE = item.BIRTH_CODE
                        objFamilyData.QUYEN = item.QUYEN
                        objFamilyData.BIRTH_NATION_ID = item.BIRTH_NATION_ID
                        objFamilyData.BIRTH_PROVINCE_ID = item.BIRTH_PROVINCE_ID
                        objFamilyData.BIRTH_DISTRICT_ID = item.BIRTH_DISTRICT_ID
                        objFamilyData.BIRTH_WARD_ID = item.BIRTH_WARD_ID
                        objFamilyData.GENDER = item.GENDER
                        objFamilyData.NATIVE = item.NATIVE
                        objFamilyData.RELATE_OWNER = item.RELATE_OWNER
                        ' 20190520 CanhNX: Edit cho lưu Nguyên quán, Nghề nghiệp, Chức danh
                        objFamilyData.CAREER = item.CAREER
                        objFamilyData.TITLE_NAME = item.TITLE_NAME
                        objFamilyData.PROVINCE_ID = item.PROVINCE_ID
                        objFamilyData.IS_SAME_COMPANY = item.IS_SAME_COMPANY

                        Dim oldFileFamily = (From p In Context.HU_USERFILES Where p.NAME = objFamilyData.FILE_FAMILY Select p.FILE_NAME).FirstOrDefault
                        Dim newFileFamily = item.FILE_FAMILY
                        If oldFileFamily <> newFileFamily Then
                            objFamilyData.FILE_FAMILY = item.FILE_FAMILY
                        End If

                        Dim oldFileNPT = (From p In Context.HU_USERFILES Where p.NAME = objFamilyData.FILE_NPT Select p.FILE_NAME).FirstOrDefault
                        Dim newFileNPT = item.FILE_NPT
                        If oldFileNPT <> newFileNPT Then
                            objFamilyData.FILE_NPT = item.FILE_NPT
                        End If
                    Else
                        Dim objFamilyData As New HU_FAMILY
                        objFamilyData.ID = Utilities.GetNextSequence(Context, Context.HU_FAMILY.EntitySet.Name)

                        item.FK_PKEY = objFamilyData.ID

                        objFamilyData.EMPLOYEE_ID = item.EMPLOYEE_ID
                        objFamilyData.FULLNAME = item.FULLNAME
                        objFamilyData.RELATION_ID = item.RELATION_ID
                        objFamilyData.BIRTH_DATE = item.BIRTH_DATE
                        objFamilyData.DEDUCT_REG = item.DEDUCT_REG
                        objFamilyData.ID_NO = item.ID_NO
                        objFamilyData.IS_DEDUCT = item.IS_DEDUCT
                        objFamilyData.DEDUCT_FROM = item.DEDUCT_FROM
                        objFamilyData.TAXTATION = item.TAXTATION
                        objFamilyData.DEDUCT_TO = item.DEDUCT_TO
                        objFamilyData.REMARK = item.REMARK
                        objFamilyData.ADDRESS = item.ADDRESS
                        'cap nhat them day du cac truong issue 297 TNG
                        objFamilyData.ADDRESS_TT = item.ADDRESS_TT
                        objFamilyData.CERTIFICATE_CODE = item.CERTIFICATE_CODE
                        objFamilyData.CERTIFICATE_NUM = item.CERTIFICATE_NUM
                        objFamilyData.IS_OWNER = item.IS_OWNER
                        objFamilyData.AD_PROVINCE_ID = item.AD_PROVINCE_ID
                        objFamilyData.AD_DISTRICT_ID = item.AD_DISTRICT_ID
                        objFamilyData.AD_WARD_ID = item.AD_WARD_ID
                        objFamilyData.TT_PROVINCE_ID = item.TT_PROVINCE_ID
                        objFamilyData.TT_DISTRICT_ID = item.TT_DISTRICT_ID
                        objFamilyData.TT_WARD_ID = item.TT_WARD_ID
                        objFamilyData.IS_PASS = item.IS_PASS
                        objFamilyData.AD_VILLAGE = item.AD_VILLAGE
                        objFamilyData.NATION_ID = item.NATION_ID
                        objFamilyData.ID_NO_DATE = item.ID_NO_DATE
                        objFamilyData.ID_NO_PLACE_NAME = item.ID_NO_PLACE_NAME
                        objFamilyData.PHONE = item.PHONE
                        objFamilyData.TAXTATION_DATE = item.TAXTATION_DATE
                        objFamilyData.TAXTATION_PLACE = item.TAXTATION_PLACE
                        objFamilyData.BIRTH_CODE = item.BIRTH_CODE
                        objFamilyData.QUYEN = item.QUYEN
                        objFamilyData.BIRTH_NATION_ID = item.BIRTH_NATION_ID
                        objFamilyData.BIRTH_PROVINCE_ID = item.BIRTH_PROVINCE_ID
                        objFamilyData.BIRTH_DISTRICT_ID = item.BIRTH_DISTRICT_ID
                        objFamilyData.BIRTH_WARD_ID = item.BIRTH_WARD_ID
                        objFamilyData.GENDER = item.GENDER
                        objFamilyData.NATIVE = item.NATIVE
                        objFamilyData.RELATE_OWNER = item.RELATE_OWNER
                        objFamilyData.IS_SAME_COMPANY = item.IS_SAME_COMPANY
                        ' 20190520 CanhNX: Edit cho lưu Nguyên quán, Nghề nghiệp, Chức danh
                        objFamilyData.CAREER = item.CAREER
                        objFamilyData.TITLE_NAME = item.TITLE_NAME
                        objFamilyData.PROVINCE_ID = item.PROVINCE_ID
                        objFamilyData.FILE_FAMILY = item.FILE_FAMILY
                        objFamilyData.FILE_NPT = item.FILE_NPT

                        Context.HU_FAMILY.AddObject(objFamilyData)
                    End If
                End If
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function UpdateStatusEmployeeCetificateEdit(ByVal lstID As List(Of Decimal),
                                                   status As String,
                                                   ByVal log As UserLog) As Boolean
        Dim lst As List(Of HU_PRO_TRAIN_OUT_COMPANY_EDIT)
        Dim sStatus() As String = status.Split(":")
        Dim emp_from = (From p In Context.SE_USER Where p.USERNAME = log.Username.ToUpper Select p.EMPLOYEE_ID).FirstOrDefault
        Try
            lst = (From p In Context.HU_PRO_TRAIN_OUT_COMPANY_EDIT Where lstID.Contains(p.ID)).ToList
            For Each item In lst
                item.STATUS = sStatus(0)
                item.REASON_UNAPROVE = sStatus(1)


                Dim noti As New SE_NOTIFICATION
                noti.ID = Utilities.GetNextSequence(Context, Context.SE_NOTIFICATION.EntitySet.Name)
                noti.FROM_EMPLOYEE_ID = emp_from
                noti.TO_EMPLOYEE_ID = item.EMPLOYEE_ID
                noti.PROCESS_TYPE = "EMP_CER"
                noti.SEND_STATUS = sStatus(0)
                noti.MESSAGE = sStatus(1)
                noti.SENT_DATE = System.DateTime.Now
                Context.SE_NOTIFICATION.AddObject(noti)



                If sStatus(0) = 2 Then
                    If item.FK_PKEY IsNot Nothing Then
                        Dim objFamilyData As HU_PRO_TRAIN_OUT_COMPANY
                        objFamilyData = (From p In Context.HU_PRO_TRAIN_OUT_COMPANY Where p.ID = item.FK_PKEY).FirstOrDefault
                        objFamilyData.EMPLOYEE_ID = item.EMPLOYEE_ID
                        objFamilyData.FROM_DATE = item.FROM_DATE
                        objFamilyData.TO_DATE = item.TO_DATE
                        objFamilyData.YEAR_GRA = item.YEAR_GRA
                        objFamilyData.NAME_SHOOLS = item.NAME_SHOOLS
                        objFamilyData.FORM_TRAIN_ID = item.FORM_TRAIN_ID
                        objFamilyData.SPECIALIZED_TRAIN = item.SPECIALIZED_TRAIN
                        objFamilyData.RESULT_TRAIN = item.RESULT_TRAIN
                        objFamilyData.EFFECTIVE_DATE_FROM = item.EFFECTIVE_DATE_FROM
                        objFamilyData.EFFECTIVE_DATE_TO = item.EFFECTIVE_DATE_TO
                        objFamilyData.POINT_LEVEL = item.POINT_LEVEL
                        objFamilyData.LEVEL_ID = item.LEVEL_ID
                        objFamilyData.TYPE_TRAIN_ID = item.TYPE_TRAIN_ID
                        objFamilyData.TYPE_TRAIN_NAME = item.TYPE_TRAIN_NAME
                        objFamilyData.CERTIFICATE = item.CERTIFICATE
                        objFamilyData.CERTIFICATE_CODE = item.CODE_CERTIFICATE
                        objFamilyData.NOTE = item.NOTE
                        objFamilyData.COMMITMENT_TIME = item.COMMITMENT_TIME
                        If item.FILE_NAME <> "" Then
                            objFamilyData.FILE_NAME = item.FILE_NAME
                        End If
                        objFamilyData.IS_MAIN = item.IS_MAIN
                        objFamilyData.IS_MAIN = item.IS_MAIN
                        objFamilyData.CERTIFICATE_GROUP_ID = item.CERTIFICATE_GROUP_ID
                        objFamilyData.CERTIFICATE_TYPE_ID = item.CERTIFICATE_TYPE_ID
                        objFamilyData.CERTIFICATE_NAME = item.CERTIFICATE_NAME
                        objFamilyData.MAJOR = item.MAJOR
                        objFamilyData.IS_MAJOR = item.IS_MAJOR
                        objFamilyData.CONTENT_LEVEL = item.CONTENT_LEVEL
                        objFamilyData.GRADUATE_SCHOOL = item.GRADUATE_SCHOOL
                    Else
                        Dim objFamilyData As New HU_PRO_TRAIN_OUT_COMPANY
                        objFamilyData.ID = Utilities.GetNextSequence(Context, Context.HU_PRO_TRAIN_OUT_COMPANY.EntitySet.Name)
                        objFamilyData.EMPLOYEE_ID = item.EMPLOYEE_ID
                        objFamilyData.FROM_DATE = item.FROM_DATE
                        objFamilyData.TO_DATE = item.TO_DATE
                        objFamilyData.YEAR_GRA = item.YEAR_GRA
                        objFamilyData.NAME_SHOOLS = item.NAME_SHOOLS
                        objFamilyData.FORM_TRAIN_ID = item.FORM_TRAIN_ID
                        objFamilyData.SPECIALIZED_TRAIN = item.SPECIALIZED_TRAIN
                        objFamilyData.RESULT_TRAIN = item.RESULT_TRAIN
                        objFamilyData.EFFECTIVE_DATE_FROM = item.EFFECTIVE_DATE_FROM
                        objFamilyData.EFFECTIVE_DATE_TO = item.EFFECTIVE_DATE_TO
                        objFamilyData.POINT_LEVEL = item.POINT_LEVEL
                        objFamilyData.LEVEL_ID = item.LEVEL_ID
                        objFamilyData.TYPE_TRAIN_ID = item.TYPE_TRAIN_ID
                        objFamilyData.TYPE_TRAIN_NAME = item.TYPE_TRAIN_NAME
                        objFamilyData.CERTIFICATE = item.CERTIFICATE
                        objFamilyData.CERTIFICATE_CODE = item.CODE_CERTIFICATE
                        objFamilyData.NOTE = item.NOTE
                        objFamilyData.COMMITMENT_TIME = item.COMMITMENT_TIME
                        If item.FILE_NAME <> "" Then
                            objFamilyData.FILE_NAME = item.FILE_NAME
                        End If
                        objFamilyData.IS_MAIN = item.IS_MAIN
                        objFamilyData.IS_MAIN = item.IS_MAIN
                        objFamilyData.CERTIFICATE_GROUP_ID = item.CERTIFICATE_GROUP_ID
                        objFamilyData.CERTIFICATE_TYPE_ID = item.CERTIFICATE_TYPE_ID
                        objFamilyData.CERTIFICATE_NAME = item.CERTIFICATE_NAME
                        objFamilyData.MAJOR = item.MAJOR
                        objFamilyData.IS_MAJOR = item.IS_MAJOR
                        objFamilyData.CONTENT_LEVEL = item.CONTENT_LEVEL
                        objFamilyData.GRADUATE_SCHOOL = item.GRADUATE_SCHOOL
                        Context.HU_PRO_TRAIN_OUT_COMPANY.AddObject(objFamilyData)
                    End If
                End If
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function copyAddress(ByVal emp_id As Decimal) As FamilyEditDTO
        Try
            Dim re = (From p In Context.HU_EMPLOYEE_CV
                      From n In Context.HU_NATION.Where(Function(f) f.ID = p.NATIONALITY).DefaultIfEmpty
                      From p1 In Context.HU_PROVINCE.Where(Function(f) f.ID = p.PER_PROVINCE).DefaultIfEmpty
                      From p2 In Context.HU_PROVINCE.Where(Function(f) f.ID = p.NAV_PROVINCE).DefaultIfEmpty
                      From d1 In Context.HU_DISTRICT.Where(Function(f) f.ID = p.PER_DISTRICT).DefaultIfEmpty
                      From d2 In Context.HU_DISTRICT.Where(Function(f) f.ID = p.NAV_DISTRICT).DefaultIfEmpty
                      From w1 In Context.HU_WARD.Where(Function(f) f.ID = p.PER_WARD).DefaultIfEmpty
                      From w2 In Context.HU_WARD.Where(Function(f) f.ID = p.NAV_WARD).DefaultIfEmpty
                      Where p.EMPLOYEE_ID = emp_id
                      Select New FamilyEditDTO With {
                          .EMPLOYEE_ID = p.EMPLOYEE_ID,
                          .AD_PROVINCE_ID = p.PER_PROVINCE,
                          .AD_PROVINCE_NAME = p1.NAME_VN,
                          .NATION_NAME = n.NAME_VN,
                             .ADDRESS = p.PER_ADDRESS,
                             .AD_DISTRICT_ID = p.PER_DISTRICT,
                          .AD_DISTRICT_NAME = d1.NAME_VN,
                          .AD_WARD_ID = p.PER_WARD,
                          .AD_WARD_NAME = w1.NAME_VN,
                          .ADDRESS_TT = p.NAV_ADDRESS,
                          .TT_PROVINCE_ID = p.NAV_PROVINCE,
                          .TT_PROVINCE_NAME = p2.NAME_VN,
                          .TT_DISTRICT_ID = p.NAV_DISTRICT,
                          .TT_DISTRICT_NAME = d2.NAME_VN,
                          .TT_WARD_ID = p.NAV_WARD,
                          .TT_WARD_NAME = w2.NAME_VN,
                          .NATION_ID = p.NATIONALITY
                          }).FirstOrDefault
            Return re

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
#End Region

End Class
