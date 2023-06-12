Imports System.Data.Objects
Imports System.Reflection
Imports Framework.Data
Imports Framework.Data.System.Linq.Dynamic

Partial Class PerformanceRepository

#Region "PE_PERSONAL Quản lý dữ liệu đánh giá cá nhân"

    Public Function GetEmployeeImport(ByVal group_obj_id As Decimal, ByVal period_id As Decimal, ByVal log As UserLog) As DataSet
        Try
            Using cls As New DataAccess.QueryData
                Dim dsData As DataSet = cls.ExecuteStore("PKG_PERFORMANCE_BUSINESS.GET_EMPLOYEE_EXPORT",
                                           New With {.P_USERNAME = log.Username,
                                                     .P_ORG_ID = 46,
                                                     .P_ISDISSOLVE = False,
                                                     .P_PERIOD_ID = period_id,
                                                     .P_OBJECT_ID = group_obj_id,
                                                     .P_CUR = cls.OUT_CURSOR}, False)

                Return dsData
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetCriteriaImport(ByVal group_obj_id As Decimal) As DataSet
        Try
            Using cls As New DataAccess.QueryData
                Dim dsData As DataSet = cls.ExecuteStore("PKG_PERFORMANCE_BUSINESS.GET_CRITERIA_IMPORT",
                                           New With {.P_OBJECT_ID = group_obj_id,
                                                        .P_CUR = cls.OUT_CURSOR}, False)

                Return dsData
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ImportEmployeeAssessment(ByVal dtData As DataTable,
                                             ByVal periodID As Decimal,
                                             ByVal group_obj_ID As Decimal,
                                             ByVal log As UserLog) As Boolean
        Try
            For i = 0 To dtData.Rows.Count - 1
                For j = 9 To dtData.Columns.Count - 2
                    Dim objData As New PE_EMPLOYEE_ASSESSMENT_DTL
                    Dim CriteriaId As Decimal
                    Dim CandidateID As Decimal
                    CriteriaId = Replace(dtData.Columns(j).ColumnName, "ID_", "")
                    CandidateID = dtData.Rows(i)(1)
                    Dim exists = (From r In Context.PE_EMPLOYEE_ASSESSMENT_DTL
                                  Where r.PE_EMPLOYEE_ASSESSMENT_ID = CandidateID And
                                  r.CRITERIA_ID = CriteriaId And
                                  r.PERIOD_ID = periodID And
                                  r.GROUP_OBJ_ID = group_obj_ID).Any
                    If exists Then
                        Dim obj = (From r In Context.PE_EMPLOYEE_ASSESSMENT_DTL
                                   Where r.PE_EMPLOYEE_ASSESSMENT_ID = CandidateID And
                                   r.CRITERIA_ID = CriteriaId And
                                   r.PERIOD_ID = periodID And
                                   r.GROUP_OBJ_ID = group_obj_ID).FirstOrDefault
                        obj.POINT = If(dtData(i)(j) = "", Nothing, dtData(i)(j))
                        obj.CLASSIFICATION = dtData(i)("CLASSIFICATION")
                        Context.SaveChanges(log)
                    Else
                        objData.ID = Utilities.GetNextSequence(Context, Context.PE_EMPLOYEE_ASSESSMENT_DTL.EntitySet.Name)
                        objData.PE_EMPLOYEE_ASSESSMENT_ID = CandidateID
                        objData.CRITERIA_ID = CriteriaId
                        objData.POINT = If(dtData(i)(j) = "", Nothing, dtData(i)(j))
                        objData.PERIOD_ID = periodID
                        objData.GROUP_OBJ_ID = group_obj_ID
                        objData.CLASSIFICATION = dtData(i)("CLASSIFICATION")
                        Context.PE_EMPLOYEE_ASSESSMENT_DTL.AddObject(objData)
                        Context.SaveChanges(log)
                    End If
                Next
            Next
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "Performance")
            Return False
            Throw ex
        End Try
    End Function

    Public Function GetEmployeeImportAssessment(ByVal _param As ParamDTO,
                                                ByVal obj As PE_EMPLOYEE_ASSESSMENT_DTLDTO,
                                                ByVal P_PAGE_INDEX As Decimal,
                                                ByVal P_PAGE_SIZE As Decimal,
                                                ByVal log As UserLog) As DataSet
        Try
            Using cls As New DataAccess.QueryData
                Dim dsData As DataSet = cls.ExecuteStore("PKG_PERFORMANCE_BUSINESS.GET_EMPLOYEE_ASS_IMPORT",
                                           New With {.P_USERNAME = log.Username,
                                                     .P_ORG_ID = _param.ORG_ID,
                                                     .P_ISDISSOLVE = _param.IS_DISSOLVE,
                                                     .P_PERIOD_ID = obj.PERIOD_ID,
                                                     .P_OBJECT_ID = obj.GROUP_OBJ_ID,
                                                     .P_PAGE_INDEX = P_PAGE_INDEX,
                                                     .P_PAGE_SIZE = P_PAGE_SIZE,
                                                     .P_CUR = cls.OUT_CURSOR,
                                                     .P_CURCOUNR = cls.OUT_CURSOR}, False)

                Return dsData
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "PE_ORGANIZATION Đánh giá đơn vị"
    Public Function GetPe_Organization(ByVal _filter As PE_ORGANIZATIONDTO,
                            ByVal _param As ParamDTO,
                            Optional ByVal PageIndex As Integer = 0,
                            Optional ByVal PageSize As Integer = Integer.MaxValue,
                            Optional ByRef Total As Integer = 0,
                            Optional ByVal Sorts As String = "CREATED_DATE desc",
                            Optional ByVal log As UserLog = Nothing) As List(Of PE_ORGANIZATIONDTO)

        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using

            Dim query = From p In Context.PE_ORGANIZATION
                        From d In Context.PE_PERIOD.Where(Function(f) f.ID = p.PERIOD_ID).DefaultIfEmpty
                        From o In Context.HUV_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) p.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)
                        Select New PE_ORGANIZATIONDTO With {
                                       .ID = p.ID,
                                       .PERIOD_ID = p.PERIOD_ID,
                                       .PERIOD_NAME = d.NAME,
                                       .ORG_ID = p.ORG_ID,
                                       .ORG_NAME = o.NAME_VN,
                                       .ORG_DESC = o.DESCRIPTION_PATH,
                                       .REMARK = p.REMARK,
                                       .RESULT = p.RESULT,
                                       .CREATED_DATE = p.CREATED_DATE}

            Dim lst = query

            If _filter.PERIOD_NAME <> "" Then
                lst = lst.Where(Function(p) p.PERIOD_NAME.ToUpper.Contains(_filter.PERIOD_NAME.ToUpper))
            End If
            If _filter.ORG_NAME <> "" Then
                lst = lst.Where(Function(p) p.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If
            If _filter.REMARK <> "" Then
                lst = lst.Where(Function(p) p.REMARK.ToUpper.Contains(_filter.REMARK.ToUpper))
            End If
            If _filter.RESULT <> "" Then
                lst = lst.Where(Function(p) p.RESULT.ToUpper.Contains(_filter.RESULT.ToUpper))
            End If
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetPe_Organization")
            Throw ex
        End Try

    End Function

    Public Function GetPe_OrganizationByID(ByVal _filter As PE_ORGANIZATIONDTO) As PE_ORGANIZATIONDTO

        Try

            Dim query = From p In Context.PE_ORGANIZATION
                        From d In Context.PE_PERIOD.Where(Function(f) f.ID = p.PERIOD_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        Where p.ID = _filter.ID
                        Select New PE_ORGANIZATIONDTO With {
                                        .ID = p.ID,
                                         .PERIOD_ID = p.PERIOD_ID,
                                         .PERIOD_NAME = d.NAME,
                                         .ORG_ID = p.ORG_ID,
                                         .ORG_NAME = o.NAME_VN,
                                         .REMARK = p.REMARK,
                                         .RESULT = p.RESULT,
                                         .CREATED_DATE = p.CREATED_DATE}

            Dim obj = query.FirstOrDefault
            Return obj
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetPe_OrganizationByID")
            Throw ex
        End Try


    End Function

    Public Function InsertPe_Organization(ByVal objData As PE_ORGANIZATIONDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objPlanRegData As New PE_ORGANIZATION
        Try
            objPlanRegData.ID = Utilities.GetNextSequence(Context, Context.PE_ORGANIZATION.EntitySet.Name)
            objPlanRegData.PERIOD_ID = objData.PERIOD_ID
            objPlanRegData.ORG_ID = objData.ORG_ID
            objPlanRegData.RESULT = objData.RESULT
            objPlanRegData.REMARK = objData.REMARK
            Context.PE_ORGANIZATION.AddObject(objPlanRegData)
            Context.SaveChanges(log)
            gID = objPlanRegData.ID
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".InsertRegisterNo")
            Throw ex
        End Try
    End Function

    Public Function ModifyPe_Organization(ByVal objData As PE_ORGANIZATIONDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objPlanRegData As New PE_ORGANIZATION With {.ID = objData.ID}
        Try
            objPlanRegData = (From p In Context.PE_ORGANIZATION Where p.ID = objData.ID).SingleOrDefault
            objPlanRegData.PERIOD_ID = objData.PERIOD_ID
            objPlanRegData.ORG_ID = objData.ORG_ID
            objPlanRegData.RESULT = objData.RESULT
            objPlanRegData.REMARK = objData.REMARK
            Context.SaveChanges(log)
            gID = objPlanRegData.ID
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".ModifyRegisterNo")
            Throw ex
        End Try

    End Function

    Public Function DeletePe_Organization(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstRegData As List(Of PE_ORGANIZATION)
        Try
            lstRegData = (From p In Context.PE_ORGANIZATION Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstRegData.Count - 1
                Context.PE_ORGANIZATION.DeleteObject(lstRegData(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".DeletePlanReg")
            Throw ex
        End Try

    End Function
#End Region

#Region "Assessment"

    Public Function GetAssessment(ByVal _filter As AssessmentDTO, ByVal PageIndex As Integer,
                                  ByVal PageSize As Integer,
                                  ByRef Total As Integer,
                                  Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AssessmentDTO)

        Try
            Dim query = From p In Context.PE_CRITERIA_OBJECT_GROUP
                        From b In Context.PE_EMPLOYEE_ASSESSMENT.Where(Function(d) d.OBJECT_GROUP_ID = p.OBJECT_GROUP_ID)
                        From obj In Context.PE_CRITERIA.Where(Function(f) f.ID = p.CRITERIA_ID)
                        From a In Context.PE_ASSESSMENT.Where(Function(s) s.PE_EMPLOYEE_ASSESSMENT_ID = b.ID And s.PE_PERIO_ID = b.PERIOD_ID _
                                                                And s.PE_OBJECT_ID = p.OBJECT_GROUP_ID And s.PE_CRITERIA_ID = p.CRITERIA_ID).DefaultIfEmpty
                        From o In Context.OT_OTHER_LIST.Where(Function(c) c.ID = a.STATUS_EMP_ID).DefaultIfEmpty
                        From stt In Context.OT_OTHER_LIST.Where(Function(f) f.ID = b.PE_STATUS).DefaultIfEmpty
                        Where (b.OBJECT_GROUP_ID = _filter.PE_OBJECT_ID And b.EMPLOYEE_ID = _filter.EMPLOYEE_ID _
                              And b.PERIOD_ID = _filter.PE_PERIO_ID)
                        Select New AssessmentDTO With {
                                .ID = a.ID,
                                .PE_EMPLOYEE_ASSESSMENT_ID = a.PE_EMPLOYEE_ASSESSMENT_ID,
                                .EMPLOYEE_ID = a.EMPLOYEE_ID,
                                .PE_PERIO_ID = a.PE_PERIO_ID,
                                .PE_OBJECT_ID = a.PE_OBJECT_ID,
                                .PE_CRITERIA_ID = obj.ID,
                                .RESULT = a.RESULT,
                                .STATUS_EMP_ID = a.STATUS_EMP_ID,
                                .DIRECT_ID = a.DIRECT_ID,
                                .UPDATE_DATE = a.UPDATE_DATE,
                                .REMARK = a.REMARK,
                                .RESULT_DIRECT = a.RESULT_DIRECT,
                                .ASS_DATE = a.ASS_DATE,
                                .REMARK_DIRECT = a.REMARK_DIRECT,
                                .RESULT_CONVERT = a.RESULT_CONVERT,
                                .ACTFLG = If(a.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                                .CREATED_DATE = a.CREATED_DATE,
                                .PE_CRITERIA_CODE = obj.CODE,
                                .PE_CRITERIA_NAME = obj.NAME,
                                .EXPENSE = p.EXPENSE,
                                .AMONG = p.AMONG,
                                .FROM_DATE = p.FROM_DATE,
                                .TO_DATE = p.TO_DATE,
                                .STATUS_EMP_NAME = o.NAME_VN,
                                .PE_STATUS_NAME = stt.NAME_VN,
                                .LINK_POPUP = a.ID,
                                .RESULT_CONVERT_QL = a.RESULT_CONVERT_QL}
            Dim lst = query
            If _filter.PE_CRITERIA_CODE <> "" Then
                lst = lst.Where(Function(p) p.PE_CRITERIA_CODE.ToUpper.Contains(_filter.PE_CRITERIA_CODE.ToUpper))
            End If
            If _filter.PE_CRITERIA_NAME <> "" Then
                lst = lst.Where(Function(p) p.PE_CRITERIA_NAME.ToUpper.Contains(_filter.PE_CRITERIA_NAME.ToUpper))
            End If
            If _filter.EXPENSE <> Nothing Then
                lst = lst.Where(Function(p) p.EXPENSE.ToString().Contains(_filter.EXPENSE.ToString()))
            End If
            If _filter.AMONG <> Nothing Then
                lst = lst.Where(Function(p) p.AMONG.ToString().Contains(_filter.AMONG.ToString()))
            End If
            If _filter.FROM_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.FROM_DATE.ToString().Contains(_filter.FROM_DATE.ToString()))
            End If
            If _filter.TO_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.TO_DATE.ToString().Contains(_filter.TO_DATE.ToString()))
            End If
            If _filter.RESULT <> Nothing Then
                lst = lst.Where(Function(p) p.RESULT.ToString().Contains(_filter.RESULT.ToString()))
            End If
            If _filter.UPDATE_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.UPDATE_DATE.ToString().Contains(_filter.UPDATE_DATE.ToString()))
            End If
            If _filter.RESULT_DIRECT <> Nothing Then
                lst = lst.Where(Function(p) p.RESULT_DIRECT.ToString().Contains(_filter.RESULT_DIRECT.ToString()))
            End If
            If _filter.RESULT_CONVERT <> Nothing Then
                lst = lst.Where(Function(p) p.RESULT_CONVERT.ToString().Contains(_filter.RESULT_CONVERT.ToString()))
            End If
            If _filter.RESULT_CONVERT_QL <> Nothing Then
                lst = lst.Where(Function(p) p.RESULT_CONVERT.ToString().Contains(_filter.RESULT_CONVERT_QL.ToString()))
            End If
            If _filter.ASS_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.ASS_DATE.ToString().Contains(_filter.ASS_DATE.ToString()))
            End If
            If _filter.STATUS_EMP_NAME <> Nothing Then
                lst = lst.Where(Function(p) p.STATUS_EMP_NAME.ToString().Contains(_filter.STATUS_EMP_NAME.ToString()))
            End If
            If _filter.ACTFLG IsNot Nothing Then
                lst = lst.Where(Function(p) p.ACTFLG = _filter.ACTFLG)
            End If
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try

    End Function

    Public Function ModifyAssessment(ByVal objAssessment As AssessmentDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objAssessmentData As New PE_ASSESSMENT
        Try
            If objAssessment.ID <= 0 Then
                objAssessmentData.ID = Utilities.GetNextSequence(Context, Context.PE_ASSESSMENT.EntitySet.Name)
                objAssessmentData.PE_EMPLOYEE_ASSESSMENT_ID = objAssessment.PE_EMPLOYEE_ASSESSMENT_ID
                objAssessmentData.EMPLOYEE_ID = objAssessment.EMPLOYEE_ID
                objAssessmentData.PE_PERIO_ID = objAssessment.PE_PERIO_ID
                objAssessmentData.PE_OBJECT_ID = objAssessment.PE_OBJECT_ID
                objAssessmentData.PE_CRITERIA_ID = objAssessment.PE_CRITERIA_ID
                objAssessmentData.RESULT = objAssessment.RESULT
                objAssessmentData.STATUS_EMP_ID = objAssessment.STATUS_EMP_ID
                If objAssessment.DIRECT_ID <> objAssessment.EMPLOYEE_ID Then
                    objAssessmentData.DIRECT_ID = objAssessment.DIRECT_ID
                End If
                objAssessmentData.UPDATE_DATE = objAssessment.UPDATE_DATE
                objAssessmentData.REMARK = objAssessment.REMARK
                objAssessmentData.RESULT_DIRECT = objAssessment.RESULT_DIRECT
                objAssessmentData.ASS_DATE = objAssessment.ASS_DATE
                objAssessmentData.REMARK_DIRECT = objAssessment.REMARK_DIRECT
                objAssessmentData.RESULT_CONVERT = objAssessment.RESULT_CONVERT
                objAssessmentData.RESULT_CONVERT_QL = objAssessment.RESULT_CONVERT_QL
                Context.PE_ASSESSMENT.AddObject(objAssessmentData)
                Context.SaveChanges(log)
                gID = objAssessmentData.ID
            Else
                Dim objAssessmentUpdate As New PE_ASSESSMENT With {.ID = objAssessment.ID}
                objAssessmentUpdate = (From p In Context.PE_ASSESSMENT Where p.ID = objAssessment.ID).FirstOrDefault
                objAssessmentUpdate.RESULT = objAssessment.RESULT
                objAssessmentUpdate.STATUS_EMP_ID = objAssessment.STATUS_EMP_ID
                If objAssessment.DIRECT_ID <> objAssessment.EMPLOYEE_ID Then
                    objAssessmentUpdate.DIRECT_ID = objAssessment.DIRECT_ID
                End If
                objAssessmentUpdate.UPDATE_DATE = objAssessment.UPDATE_DATE
                objAssessmentUpdate.REMARK = objAssessment.REMARK
                objAssessmentUpdate.RESULT_DIRECT = objAssessment.RESULT_DIRECT
                objAssessmentUpdate.ASS_DATE = objAssessment.ASS_DATE
                objAssessmentUpdate.REMARK_DIRECT = objAssessment.REMARK_DIRECT
                objAssessmentUpdate.RESULT_CONVERT = objAssessment.RESULT_CONVERT
                objAssessmentUpdate.RESULT_CONVERT_QL = objAssessment.RESULT_CONVERT_QL
                Context.SaveChanges(log)
                gID = objAssessmentUpdate.ID
            End If
            Dim objUpdate As New PE_EMPLOYEE_ASSESSMENT With {.OBJECT_GROUP_ID = objAssessment.PE_OBJECT_ID _
                                                                      And .PERIOD_ID = objAssessment.PE_PERIO_ID _
                                                                      And .EMPLOYEE_ID = objAssessment.EMPLOYEE_ID}
            objUpdate = (From p In Context.PE_EMPLOYEE_ASSESSMENT
                         Where p.OBJECT_GROUP_ID = objAssessment.PE_OBJECT_ID _
                         And p.PERIOD_ID = objAssessment.PE_PERIO_ID _
                         And p.EMPLOYEE_ID = objAssessment.EMPLOYEE_ID).FirstOrDefault
            objUpdate.PE_STATUS = objAssessment.PE_STATUS
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try
    End Function

    Public Function UpdateStatusEmployeeAssessment(ByVal obj As AssessmentDTO,
                                                   ByVal log As UserLog) As Boolean
        Try

            Dim objUpdate As New PE_EMPLOYEE_ASSESSMENT With {.OBJECT_GROUP_ID = obj.PE_OBJECT_ID _
                                                                       And .PERIOD_ID = obj.PE_PERIO_ID _
                                                                       And .EMPLOYEE_ID = obj.EMPLOYEE_ID}
            objUpdate = (From p In Context.PE_EMPLOYEE_ASSESSMENT
                         Where p.OBJECT_GROUP_ID = obj.PE_OBJECT_ID _
                         And p.PERIOD_ID = obj.PE_PERIO_ID _
                         And p.EMPLOYEE_ID = obj.EMPLOYEE_ID).FirstOrDefault
            objUpdate.PE_STATUS = obj.PE_STATUS
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try

    End Function
    Public Function GetTotalPointRating(ByVal obj As AssessmentDTO) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PERFORMANCE_BUSINESS.GET_PE_ASSESS",
                                           New With {.P_PE_EMPLOYEE_ASSESSMENT_ID = obj.PE_EMPLOYEE_ASSESSMENT_ID,
                                                     .P_EMPLOYEE_ID = obj.EMPLOYEE_ID,
                                                     .P_PE_PERIO_ID = obj.PE_PERIO_ID,
                                                     .P_PE_OBJECT_ID = obj.PE_OBJECT_ID,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try
    End Function

    Public Function Delete_PE_KPI_ASSESSMENT_DETAIL(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstRegData As List(Of PE_KPI_ASSESSMENT_DETAIL)
        Try
            lstRegData = (From p In Context.PE_KPI_ASSESSMENT_DETAIL Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstRegData.Count - 1
                Context.PE_KPI_ASSESSMENT_DETAIL.DeleteObject(lstRegData(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".Delete_PE_KPI_ASSESSMENT_DETAIL")
            Throw ex
        End Try

    End Function
#End Region

#Region "Common"
    Public Function GetListEmployeePortal(ByVal _filter As EmployeeDTO) As List(Of EmployeeDTO)
        Try
            Dim wstt = PerformanceCommon.OT_WORK_STATUS.TERMINATE_ID

            Dim query = From p In Context.HU_EMPLOYEE Where p.ID = _filter.DIRECT_MANAGER
                        Order By p.EMPLOYEE_CODE

            Dim lst = query.Select(Function(p) New EmployeeDTO With {
                                       .EMPLOYEE_CODE = p.EMPLOYEE_CODE,
                                       .ID = p.ID,
                                       .FULLNAME_VN = p.FULLNAME_VN,
                                       .TER_EFFECT_DATE = p.TER_EFFECT_DATE,
                                       .DIRECT_MANAGER = p.DIRECT_MANAGER,
                                       .WORK_STATUS = p.WORK_STATUS})

            'Dim dateNow = Date.Now.Date
            'Dim terID = PerformanceCommon.OT_WORK_STATUS.TERMINATE_ID
            'If Not _filter.IS_TER Then
            '    lst = lst.Where(Function(p) Not p.WORK_STATUS.HasValue Or _
            '                        (p.WORK_STATUS.HasValue And _
            '                         ((p.WORK_STATUS <> terID) Or (p.WORK_STATUS = terID And p.TER_EFFECT_DATE > dateNow))))

            'End If

            'If _filter.DIRECT_MANAGER IsNot Nothing Then
            '    lst = lst.Where(Function(p) p.DIRECT_MANAGER = _filter.DIRECT_MANAGER)
            'End If
            'If _filter.ID <> Nothing Then
            '    lst = lst.Where(Function(p) p.ID = _filter.ID)
            'End If
            Return lst.ToList

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try
    End Function
#End Region

#Region "PORTAL QL PKI cua nhan vien"

    Public Function GetAssessmentDirect(ByVal _empId As Decimal, ByVal _year As Decimal?, ByVal _status As Decimal?) As List(Of AssessmentDirectDTO)
        Try
            Dim lst As List(Of AssessmentDirectDTO) = New List(Of AssessmentDirectDTO)
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PERFORMANCE_BUSINESS.GET_PE_ASSESS_EMP_BY_DIRECT",
                                           New With {.P_DIRECT_ID = _empId,
                                                     .P_YEAR = _year,
                                                     .P_STATUS = _status,
                                                    .P_CUR = cls.OUT_CURSOR})
                If dtData IsNot Nothing Then
                    lst = (From row As DataRow In dtData.Rows
                           Select New AssessmentDirectDTO With {.ID = row("ID").ToString(),
                                                               .PE_PERIO_ID = row("PE_PERIO_ID").ToString(),
                                                               .PE_STATUS_ID = row("PE_STATUS_ID").ToString(),
                                                               .EMPLOYEE_ID = row("EMPLOYEE_ID").ToString(),
                                                               .EMPLOYEE_CODE = row("EMPLOYEE_CODE").ToString(),
                                                               .EMPLOYEE_NAME = row("EMPLOYEE_NAME").ToString(),
                                                                .ORG_ID = row("ORG_ID").ToString(),
                                                                .ORG_NAME = row("ORG_NAME").ToString(),
                                                                .TITLE_NAME = row("TITLE_NAME").ToString(),
                                                               .PE_PERIO_YEAR = row("PE_PERIO_YEAR").ToString(),
                                                               .PE_PERIO_NAME = row("PE_PERIO_NAME").ToString(),
                                                                .PE_PERIO_TYPE_ASS = row("PE_PERIO_TYPE_ASS").ToString(),
                                                               .PE_PERIO_TYPE_ASS_NAME = row("PE_PERIO_TYPE_ASS_NAME").ToString(),
                                                               .PE_PERIO_START_DATE = ToDate(row("PE_PERIO_START_DATE")),
                                                               .PE_PERIO_END_DATE = ToDate(row("PE_PERIO_END_DATE")),
                                                               .RESULT_CONVERT = row("RESULT_CONVERT").ToString(),
                                                               .PE_STATUS_NAME = row("PE_STATUS_NAME").ToString(),
                                                                .CLASSIFICATION_NAME = row("CLASSIFICATION_NAME").ToString(),
                                                                .CLASSIFICATION_NAME_NV = row("CLASSIFICATION_NAME_NV").ToString(),
                                                                .RESULT_CONVERT_NV = row("RESULT_CONVERT_NV").ToString(),
                                                                .OBJECT_GROUP_ID = row("OBJECT_GROUP_ID").ToString()
                                                              }).ToList
                End If
            End Using
            Return lst
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "GetAssessmentDirect")
            Throw ex
        End Try
    End Function

    Public Function GET_DM_STATUS() As DataTable
        Try
            Dim dtData As DataTable
            Using cls As New DataAccess.QueryData
                dtData = cls.ExecuteStore("PKG_PERFORMANCE_BUSINESS.GET_DM_STATUS",
                                           New With {.P_CUR = cls.OUT_CURSOR})

            End Using
            Return dtData
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "GET_DM_STATUS")
            Throw ex
        End Try
    End Function

    Public Function UpdateStatusAssessmentDirect(ByVal obj As Decimal, ByVal log As UserLog) As Boolean
        Try
            'Dim objUpdate As New PE_EMPLOYEE_ASSESSMENT With {.ID = obj}
            'objUpdate = (From p In Context.PE_EMPLOYEE_ASSESSMENT
            '                       Where p.ID = obj).FirstOrDefault
            'objUpdate.PE_STATUS = 6141
            'Context.SaveChanges(log)
            'Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try
    End Function

    Public Function GetKPIAssessEmp(ByVal _empId As Decimal) As List(Of AssessmentDirectDTO)
        Try
            Dim lst As List(Of AssessmentDirectDTO) = New List(Of AssessmentDirectDTO)
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PERFORMANCE_BUSINESS.GET_PE_ASSESS_EMP_BY_EMP",
                                           New With {.P_EMPLOYEE_ID = _empId,
                                                    .P_CUR = cls.OUT_CURSOR})
                If dtData IsNot Nothing Then
                    lst = (From row As DataRow In dtData.Rows
                           Select New AssessmentDirectDTO With {.ID = row("ID").ToString(),
                                                       .EMPLOYEE_ID = row("EMPLOYEE_ID").ToString(),
                                                       .EMPLOYEE_CODE = row("EMPLOYEE_CODE").ToString(),
                                                       .EMPLOYEE_NAME = row("EMPLOYEE_NAME").ToString(),
                                                       .PE_PERIO_YEAR = row("PE_PERIO_YEAR").ToString(),
                                                       .PE_PERIO_NAME = row("PE_PERIO_NAME").ToString(),
                                                        .PE_PERIO_TYPE_ASS = row("PE_PERIO_TYPE_ASS").ToString(),
                                                       .PE_PERIO_TYPE_ASS_NAME = row("PE_PERIO_TYPE_ASS_NAME").ToString(),
                                                       .PE_PERIO_START_DATE = ToDate(row("PE_PERIO_START_DATE")),
                                                       .PE_PERIO_END_DATE = ToDate(row("PE_PERIO_END_DATE")),
                                                       .RESULT_CONVERT = row("RESULT_CONVERT").ToString(),
                                                       .PE_STATUS_NAME = row("PE_STATUS_NAME").ToString(),
                                                       .CLASSIFICATION_NAME = row("CLASSIFICATION_NAME").ToString(),
                                                       .OBJECT_GROUP_ID = row("OBJECT_GROUP_ID").ToString(),
                                                       .PE_PERIO_ID = row("PE_PERIO_ID").ToString(),
                                                      .RESULT_CONVERT_NV = row("RESULT_CONVERT_NV").ToString(),
                                                      .CLASSIFICATION_NAME_NV = row("CLASSIFICATION_NAME_NV").ToString()
                                                      }).ToList
                End If
            End Using
            Return lst
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "GetKPIAssessEmp")
            Throw ex
        End Try
    End Function


    Public Function CHECK_APP(ByVal _empId As Decimal, ByVal __PE_PERIOD_ID As Decimal) As DataTable
        Try
            Dim dtData As DataTable
            Using cls As New DataAccess.QueryData
                dtData = cls.ExecuteStore("PKG_PERFORMANCE_BUSINESS.CHECK_APP",
                                           New With {.P_EMPLOYEE_ID = _empId,
                                                     .P_PE_PERIOD_ID = __PE_PERIOD_ID,
                                                    .P_CUR = cls.OUT_CURSOR})

            End Using
            Return dtData
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "CHECK_APP")
            Throw ex
        End Try
    End Function

    Public Function GET_PE_ASSESSMENT_HISTORY(ByVal _Id As Decimal) As DataTable
        Try
            Dim dtData As DataTable
            Using cls As New DataAccess.QueryData
                dtData = cls.ExecuteStore("PKG_PERFORMANCE_BUSINESS.GET_PE_ASSESSMENT_HISTORY",
                                           New With {.P_ID = _Id,
                                                    .P_CUR = cls.OUT_CURSOR})

            End Using
            Return dtData
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "GET_PE_ASSESSMENT_HISTORY")
            Throw ex
        End Try
    End Function

    Public Function CHECK_APP_1(ByVal _empId As Decimal, ByVal __PE_PERIOD_ID As Decimal) As DataTable
        Try
            Dim dtData As DataTable
            Using cls As New DataAccess.QueryData
                dtData = cls.ExecuteStore("PKG_PERFORMANCE_BUSINESS.CHECK_APP_1",
                                           New With {.P_EMPLOYEE_ID = _empId,
                                                     .P_PE_PERIOD_ID = __PE_PERIOD_ID,
                                                    .P_CUR = cls.OUT_CURSOR})

            End Using
            Return dtData
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "CHECK_APP_1")
            Throw ex
        End Try
    End Function

    Public Function CHECK_APP_2(ByVal _empId As Decimal, ByVal __PE_PERIOD_ID As Decimal) As DataTable
        Try
            Dim dtData As DataTable
            Using cls As New DataAccess.QueryData
                dtData = cls.ExecuteStore("PKG_PERFORMANCE_BUSINESS.CHECK_APP_2",
                                           New With {.P_EMPLOYEE_ID = _empId,
                                                     .P_PE_PERIOD_ID = __PE_PERIOD_ID,
                                                    .P_CUR = cls.OUT_CURSOR})

            End Using
            Return dtData
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "CHECK_APP_1")
            Throw ex
        End Try
    End Function

    Public Function PRI_PERFORMACE_APP(ByVal _empId As Decimal, ByVal __PE_PERIOD_ID As Decimal) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_PERFORMANCE_BUSINESS.PRI_PERFORMACE_APP",
                                           New With {.P_EMPLOYEE_ID = _empId,
                                                     .P_PE_PERIOD_ID = __PE_PERIOD_ID})

            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "PRI_PERFORMACE_APP")
            Throw ex
        End Try
    End Function

    Public Function INSERT_PE_ASSESSMENT_HISTORY(ByVal P_PE_PE_ASSESSMENT_ID As Decimal,
                                                 ByVal P_RESULT_DIRECT As String,
                                                 ByVal P_ASS_DATE As Date?,
                                                 ByVal P_REMARK_DIRECT As String,
                                                 ByVal P_CREATED_BY As String,
                                                 ByVal P_CREATED_LOG As String,
                                                 ByVal P_EMPLOYEE_ID As Decimal,
                                                 ByVal P_SIGN_ID As Decimal) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_PERFORMANCE_BUSINESS.INSERT_PE_ASSESSMENT_HISTORY",
                                           New With {.P_PE_PE_ASSESSMENT_ID = P_PE_PE_ASSESSMENT_ID,
                                                     .P_RESULT_DIRECT = P_RESULT_DIRECT,
                                                     .P_ASS_DATE = P_ASS_DATE,
                                                     .P_REMARK_DIRECT = P_REMARK_DIRECT,
                                                     .P_CREATED_BY = P_CREATED_BY,
                                                     .P_CREATED_LOG = P_CREATED_LOG,
                                                     .P_EMPLOYEE_ID = P_EMPLOYEE_ID,
                                                     .P_SIGN_ID = P_SIGN_ID})

            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "PRI_PERFORMACE_APP")
            Throw ex
        End Try
    End Function



    Public Function PRI_PERFORMACE_PROCESS(ByVal P_EMPLOYEE_APP_ID As Decimal, ByVal P_EMPLOYEE_ID As Decimal,
                                           ByVal P_PE_PERIOD_ID As Decimal, ByVal P_STATUS_ID As Decimal,
                                           ByVal P_NOTES As String) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_PERFORMANCE_BUSINESS.PRI_PERFORMACE_PROCESS",
                                           New With {.P_EMPLOYEE_APP_ID = P_EMPLOYEE_APP_ID,
                                                     .P_EMPLOYEE_ID = P_EMPLOYEE_ID,
                                                     .P_PE_PERIOD_ID = P_PE_PERIOD_ID,
                                                     .P_STATUS_ID = P_STATUS_ID,
                                                     .P_NOTES = P_NOTES})

            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "PRI_PERFORMACE_APP")
            Throw ex
        End Try
    End Function


#End Region

#Region "CBNS xem KPI cua nhan vien"
    Public Function GetDirectKPIEmployee(ByVal filter As AssessmentDirectDTO, ByVal PageIndex As Integer,
                                    ByVal PageSize As Integer,
                                    ByRef Total As Integer, ByVal _param As ParamDTO,
                                    Optional ByVal Sorts As String = "EMPLOYEE_ID desc",
                                    Optional ByVal log As UserLog = Nothing) As List(Of AssessmentDirectDTO)
        Try
            Dim lst As List(Of AssessmentDirectDTO) = New List(Of AssessmentDirectDTO)
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PERFORMANCE_BUSINESS.GET_PE_ASSESS_EMP_BY_ORG",
                                           New With {.P_YEAR = filter.PE_PERIO_YEAR,
                                                     .P_ORGID = _param.ORG_ID,
                                                     .P_USERNAME = log.Username,
                                                    .P_CUR = cls.OUT_CURSOR})
                If dtData IsNot Nothing Then
                    Dim query = From row As DataRow In dtData.Rows
                                Select New AssessmentDirectDTO With {.ID = row("ID").ToString(),
                                                                     .OBJECT_GROUP_ID = row("OBJECT_GROUP_ID").ToString(),
                                                                     .PE_PERIO_ID = row("PE_PERIO_ID").ToString(),
                                                                     .PE_STATUS_ID = row("PE_STATUS_ID").ToString(),
                                                                     .EMPLOYEE_ID = row("EMPLOYEE_ID").ToString(),
                                                                     .EMPLOYEE_CODE = row("EMPLOYEE_CODE").ToString(),
                                                                     .EMPLOYEE_NAME = row("EMPLOYEE_NAME").ToString(),
                                                                      .ORG_ID = row("ORG_ID").ToString(),
                                                                      .ORG_NAME = row("ORG_NAME").ToString(),
                                                                      .TITLE_NAME = row("TITLE_NAME").ToString(),
                                                                     .PE_PERIO_YEAR = row("PE_PERIO_YEAR").ToString(),
                                                                     .PE_PERIO_NAME = row("PE_PERIO_NAME").ToString(),
                                                                      .PE_PERIO_TYPE_ASS = row("PE_PERIO_TYPE_ASS").ToString(),
                                                                     .PE_PERIO_TYPE_ASS_NAME = row("PE_PERIO_TYPE_ASS_NAME").ToString(),
                                                                     .PE_PERIO_START_DATE = ToDate(row("PE_PERIO_START_DATE")),
                                                                     .PE_PERIO_END_DATE = ToDate(row("PE_PERIO_END_DATE")),
                                                                     .RESULT_CONVERT = row("RESULT_CONVERT").ToString(),
                                                                     .PE_STATUS_NAME = row("PE_STATUS_NAME").ToString(),
                                                                     .RESULT_CONVERT_NV = row("RESULT_CONVERT_NV").ToString(),
                                                                     .CLASSIFICATION_NAME_NV = row("CLASSIFICATION_NAME_NV").ToString(),
                              .CLASSIFICATION_NAME = row("CLASSIFICATION_NAME").ToString()}
                    Dim lst1 = query
                    'lst1 = lst1.OrderBy(Sorts)
                    Total = lst1.Count
                    lst1 = lst1.Skip(PageIndex * PageSize).Take(PageSize)
                    Return lst1.ToList
                End If
            End Using
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetDirectKPIEmployee")
            Throw ex
        End Try
    End Function
#End Region

#Region "Bieu do xep hang nhan vien"

    Public Function GetAssessRatingEmployee(ByVal filter As AssessRatingDTO) As List(Of AssessRatingDTO)
        Try
            Dim lst As List(Of AssessRatingDTO) = New List(Of AssessRatingDTO)
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PERFORMANCE_BUSINESS.GET_PE_ASSESS_RATING",
                                           New With {.P_YEAR = filter.PERIOD_YEAR,
                                                     .P_PERIOD_ID = filter.PERIOD_ID,
                                                     .P_TYPEASS_ID = filter.PERIOD_TYPEASS_ID,
                                                     .P_DIRECT_ID = filter.DIRECT_ID,
                                                     .P_CUR = cls.OUT_CURSOR})
                If dtData IsNot Nothing Then
                    lst = (From row As DataRow In dtData.Rows
                           Select New AssessRatingDTO With {.CLASSIFICATION_ID = row("CLASSIFICATION_ID").ToString(),
                                                               .CLASSIFICATION_CODE = row("CLASSIFICATION_CODE").ToString(),
                                                               .CLASSIFICATION_NAME = row("CLASSIFICATION_NAME").ToString(),
                                                               .COUNT_EMP = row("COUNT_EMP").ToString()
                                                              }).ToList
                End If
            End Using
            Return lst
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetAssessRatingEmployee")
            Throw ex
        End Try
    End Function
    Public Function GetAssessRatingEmployeeOrg(ByVal filter As AssessRatingDTO, Optional ByVal log As UserLog = Nothing) As List(Of AssessRatingDTO)
        Try
            Dim lst As List(Of AssessRatingDTO) = New List(Of AssessRatingDTO)
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PERFORMANCE_BUSINESS.GET_PE_ASSESS_RATING_ORG",
                                           New With {.P_YEAR = filter.PERIOD_YEAR,
                                                     .P_PERIOD_ID = filter.PERIOD_ID,
                                                     .P_TYPEASS_ID = filter.PERIOD_TYPEASS_ID,
                                                     .P_USERNAME = log.Username,
                                                     .P_ORGID = filter.ORG_ID,
                                                     .P_CUR = cls.OUT_CURSOR})
                If dtData IsNot Nothing Then
                    lst = (From row As DataRow In dtData.Rows
                           Select New AssessRatingDTO With {.CLASSIFICATION_ID = row("CLASSIFICATION_ID").ToString(),
                                                               .CLASSIFICATION_CODE = row("CLASSIFICATION_CODE").ToString(),
                                                               .CLASSIFICATION_NAME = row("CLASSIFICATION_NAME").ToString(),
                                                               .COUNT_EMP = row("COUNT_EMP").ToString()
                                                              }).ToList
                End If
            End Using
            Return lst
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetAssessRatingEmployeeOrg")
            Throw ex
        End Try
    End Function

#End Region

    Public Function PRINT_PE_ASSESS(ByVal empID As Decimal, ByVal period As Decimal, ByVal obj As Decimal) As DataSet
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataSet = cls.ExecuteStore("PKG_PERFORMANCE_LIST.PRINT_PE_ASSESS",
                                           New With {.P_EMPLOYEE_ID = empID,
                                                     .P_PE_PERIOD_ID = period,
                                                     .P_OBJECT_GROUP_ID = obj,
                                                     .P_CUR = cls.OUT_CURSOR,
                                                     .P_CUR1 = cls.OUT_CURSOR,
                                                     .P_CUR2 = cls.OUT_CURSOR}, False) ' FALSE : no datatable

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function UpdateKpiAssessmentDetail(ByVal lstObj As List(Of PE_KPI_ASSESMENT_DETAIL_DTO),
                                              Optional ByVal log As UserLog = Nothing) As Boolean
        Try
            Dim objAssessment As New PE_KPI_ASSESSMENT_DETAIL
            For Each item As PE_KPI_ASSESMENT_DETAIL_DTO In lstObj
                Dim objAssessmentDetail = (From p In Context.PE_KPI_ASSESSMENT_DETAIL Where p.ID = item.ID).FirstOrDefault
                objAssessmentDetail.ID = item.ID
                objAssessmentDetail.EMPLOYEE_ACTUAL = item.EMPLOYEE_ACTUAL
                objAssessmentDetail.EMPLOYEE_POINTS = item.EMPLOYEE_POINT
                objAssessmentDetail.DIRECT_ACTUAL = item.DIRECT_ACTUAL
                objAssessmentDetail.DIRECT_POINTS = item.DIRECT_POINT
                objAssessmentDetail.NOTE = item.NOTE
                objAssessmentDetail.NOTE_QLTT = item.NOTE_QLTT
                objAssessmentDetail.RATIO = item.RATIO
                objAssessmentDetail.SOURCE_TEXT = item.SOURCE_TEXT
                Context.SaveChanges()

                'Dim objAssessmentHistory = (From p In Context.PE_ASSESSMENT_DETAIL_HISTORY Where p.KPI_ASSESSMENT_ID = item.ID).FirstOrDefault
                'If objAssessmentHistory IsNot Nothing Then
                '    objAssessmentHistory.CREATED_DATE = Date.Now
                '    objAssessmentHistory.CREATED_BY = log.Username
                '    objAssessmentHistory.DIRECT_ACTUAL = item.DIRECT_ACTUAL
                '    objAssessmentHistory.DIRECT_POINTS = item.DIRECT_POINT
                '    objAssessmentHistory.EMPLOYEE_ACTUAL = item.EMPLOYEE_ACTUAL
                '    objAssessmentHistory.EMPLOYEE_POINTS = item.EMPLOYEE_POINT
                '    objAssessmentHistory.NOTE_NV = item.NOTE
                '    objAssessmentHistory.NOTE_QLTT = item.NOTE_QLTT
                '    Context.SaveChanges()
                'Else
                Dim objAssessmentHistoryDetail As New PE_ASSESSMENT_DETAIL_HISTORY
                objAssessmentHistoryDetail.ID = Utilities.GetNextSequence(Context, Context.PE_ASSESSMENT_DETAIL_HISTORY.EntitySet.Name)
                objAssessmentHistoryDetail.KPI_ASSESSMENT_ID = item.ID
                objAssessmentHistoryDetail.DIRECT_ACTUAL = item.DIRECT_ACTUAL
                If Not IsNothing(item.DIRECT_POINT) Then
                    objAssessmentHistoryDetail.DIRECT_POINTS = item.DIRECT_POINT
                End If
                objAssessmentHistoryDetail.EMPLOYEE_ACTUAL = item.EMPLOYEE_ACTUAL
                objAssessmentHistoryDetail.EMPLOYEE_POINTS = If(item.EMPLOYEE_POINT Is Nothing, "", item.EMPLOYEE_POINT.ToString)
                objAssessmentHistoryDetail.NOTE_NV = item.NOTE
                objAssessmentHistoryDetail.NOTE_QLTT = item.NOTE_QLTT
                Context.PE_ASSESSMENT_DETAIL_HISTORY.AddObject(objAssessmentHistoryDetail)
                Context.SaveChanges(log)
                'End If
            Next
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".UpdateKpiAssessmentDetail")
            Throw ex
        End Try
    End Function
    Public Function DeleteKpiAssessmentDetail(ByVal lstObj As List(Of PE_KPI_ASSESMENT_DETAIL_DTO)) As Boolean
        Try
            For Each item As PE_KPI_ASSESMENT_DETAIL_DTO In lstObj
                Dim objAssessmentDetailAdd = (From p In Context.PE_KPI_ASSESSMENT_DETAIL Where p.GOAL_ID = item.GOAL_ID AndAlso p.KPI_ASSESSMENT_TEXT = item.KPI_ASSESSMENT_TEXT)
                Context.PE_KPI_ASSESSMENT_DETAIL.DeleteObject(objAssessmentDetailAdd)
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".UpdateKpiAssessmentDetail")
            Throw ex
        End Try
    End Function

    Public Function UpdateKpiAssessment(ByVal obj As KPI_ASSESSMENT_DTO) As Boolean
        Try
            Dim lstCode As New List(Of String)
            Dim objAssessment = (From p In Context.PE_KPI_ASSESSMENT Where p.ID = obj.ID).FirstOrDefault
            objAssessment.EMPLOYEE = obj.EMPLOYEE
            objAssessment.GOAL = obj.GOAL
            objAssessment.NUMBER_MONTH = obj.NUMBER_MONTH
            objAssessment.EFFECT_DATE = obj.EFFECT_DATE
            objAssessment.END_DATE = obj.END_DATE
            objAssessment.START_DATE = obj.START_DATE
            objAssessment.PE_PERIOD_ID = obj.PE_PERIOD_ID
            objAssessment.YEAR = obj.YEAR
            objAssessment.STATUS_ID = obj.STATUS_ID
            objAssessment.PORTAL_ID = obj.PORTAL_ID
            objAssessment.IS_CONFIRM = obj.IS_CONFIRM
            For Each item As PE_KPI_ASSESMENT_DETAIL_DTO In obj.assesmentDetail
                Dim objAssessmentDetailAdd = (From p In Context.PE_KPI_ASSESSMENT_DETAIL Where p.GOAL_ID = obj.ID AndAlso p.KPI_ASSESSMENT_TEXT = item.KPI_ASSESSMENT_TEXT).FirstOrDefault
                If objAssessmentDetailAdd Is Nothing Then
                    Context.PE_KPI_ASSESSMENT_DETAIL.AddObject(New PE_KPI_ASSESSMENT_DETAIL With {
                                                   .ID = Utilities.GetNextSequence(Context, Context.PE_KPI_ASSESSMENT_DETAIL.EntitySet.Name),
                                                   .GOAL_ID = obj.ID,
                                                   .KPI_ASSESSMENT = item.KPI_ASSESSMENT,
                                                   .KPI_ASSESSMENT_TEXT = item.KPI_ASSESSMENT_TEXT,
                                                   .DVT = item.UNIT_ID,
                                                   .DESCRIPTION = item.DESCRIPTION,
                                                   .FREQUENCY = item.FREQUENCY_ID,
                                                   .SOURCE = item.SOURCE_ID,
                                                   .GOAL_TYPE = item.GOAL_TYPE,
                                                   .RATIO = item.RATIO,
                                                   .TARGET_TYPE = item.TARGET_TYPE_ID,
                                                   .TARGET = item.TARGET,
                                                   .TARGET_MIN = item.TARGET_MIN,
                                                   .SOURCE_TEXT = item.SOURCE_TEXT,
                                                   .BENCHMARK = item.BENCHMARK})
                Else
                    objAssessmentDetailAdd.BENCHMARK = item.BENCHMARK
                    objAssessmentDetailAdd.RATIO = item.RATIO
                End If
                lstCode.Add(item.KPI_ASSESSMENT_TEXT)
            Next
            If lstCode.Count > 0 Then
                Dim objAssessmentDetailDelete = (From p In Context.PE_KPI_ASSESSMENT_DETAIL Where p.GOAL_ID = obj.ID AndAlso Not lstCode.Contains(p.KPI_ASSESSMENT_TEXT))
                For Each item As PE_KPI_ASSESSMENT_DETAIL In objAssessmentDetailDelete
                    Context.PE_KPI_ASSESSMENT_DETAIL.DeleteObject(item)
                Next
            End If
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".UpdateKpiAssessmentDetail")
            Throw ex
        End Try
    End Function

    Public Function ValidateDateAssessment(ByVal obj As KPI_ASSESSMENT_DTO) As Boolean
        Try
            Dim query = From p In Context.PE_KPI_ASSESSMENT Where p.EMPLOYEE = obj.EMPLOYEE AndAlso p.PE_PERIOD_ID = obj.PE_PERIOD_ID _
                        AndAlso EntityFunctions.TruncateTime(p.EFFECT_DATE) >= obj.EFFECT_DATE And p.ID <> obj.ID
            Return query.Any
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".ValidateDateAssessment")
            Throw ex
        End Try
    End Function

    Public Function GetKpiAssessmentDetailHistory(ByVal lstID As List(Of String), ByVal _filter As PE_KPI_ASSESMENT_DETAIL_DTO,
                                                     ByVal PageIndex As Integer,
                                                     ByVal PageSize As Integer,
                                                     ByRef Total As Integer, ByVal _param As ParamDTO,
                                                     Optional ByVal Sorts As String = "CREATED_DATE desc",
                                                     Optional ByVal log As UserLog = Nothing) As List(Of PE_KPI_ASSESMENT_DETAIL_DTO)
        Try
            If lstID.Count = 1 AndAlso lstID(0) = "All" Then
                Dim query = From k In Context.PE_ASSESSMENT_DETAIL_HISTORY
                            From s In Context.PE_KPI_ASSESSMENT_DETAIL.Where(Function(f) f.ID = k.KPI_ASSESSMENT_ID).DefaultIfEmpty
                            From p In Context.PE_KPI_ASSESSMENT.Where(Function(f) f.ID = s.GOAL_ID)
                            Where s.GOAL_ID = _filter.GOAL_ID
                            Select New PE_KPI_ASSESMENT_DETAIL_DTO With {
                                    .ID = k.ID,
                                    .DIRECT_ACTUAL = k.DIRECT_ACTUAL,
                                    .DIRECT_POINT_NAME = k.DIRECT_POINTS,
                                    .EMPLOYEE_ACTUAL = k.EMPLOYEE_ACTUAL,
                                    .EMPLOYEE_POINT_NAME = k.EMPLOYEE_POINTS,
                                    .KPI_ASSESSMENT_TEXT = s.KPI_ASSESSMENT_TEXT,
                                    .NOTE = k.NOTE_NV,
                                    .NOTE_QLTT = k.NOTE_QLTT,
                                    .CREATED_DATE = k.CREATED_DATE,
                                    .CREATED_BY = k.CREATED_BY}
                query = query.OrderBy("KPI_ASSESSMENT_TEXT, CREATED_DATE DESC")
                Total = query.Count
                query = query.Skip(PageIndex * PageSize).Take(PageSize)
                Dim rs = query.ToList
                Return rs
            Else
                Dim ids As New List(Of Int32)
                For Each id As Int32 In lstID
                    ids.Add(id)
                Next
                Dim query = From k In Context.PE_ASSESSMENT_DETAIL_HISTORY
                            From s In Context.PE_KPI_ASSESSMENT_DETAIL.Where(Function(f) f.ID = k.KPI_ASSESSMENT_ID).DefaultIfEmpty
                            From p In Context.PE_KPI_ASSESSMENT.Where(Function(f) f.ID = s.GOAL_ID)
                            Where ids.Contains(k.KPI_ASSESSMENT_ID) And s.GOAL_ID = _filter.GOAL_ID
                            Select New PE_KPI_ASSESMENT_DETAIL_DTO With {
                                    .ID = k.ID,
                                    .DIRECT_ACTUAL = k.DIRECT_ACTUAL,
                                    .DIRECT_POINT_NAME = k.DIRECT_POINTS,
                                    .EMPLOYEE_ACTUAL = k.EMPLOYEE_ACTUAL,
                                    .EMPLOYEE_POINT_NAME = k.EMPLOYEE_POINTS,
                                    .KPI_ASSESSMENT_TEXT = s.KPI_ASSESSMENT_TEXT,
                                    .NOTE = k.NOTE_NV,
                                    .NOTE_QLTT = k.NOTE_QLTT,
                                    .CREATED_DATE = k.CREATED_DATE,
                                    .CREATED_BY = k.CREATED_BY}
                query = query.OrderBy("KPI_ASSESSMENT_TEXT, CREATED_DATE DESC")
                Total = query.Count
                query = query.Skip(PageIndex * PageSize).Take(PageSize)
                Dim rs = query.ToList
                Return rs
            End If
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try
    End Function

    Public Function GetKpiAssessmentDetailByListCode(ByVal lstCode As List(Of String), ByVal _filter As PE_KPI_ASSESMENT_DETAIL_DTO,
                                                     ByVal PageIndex As Integer,
                                                     ByVal PageSize As Integer,
                                                     ByRef Total As Integer, ByVal _param As ParamDTO,
                                                     Optional ByVal Sorts As String = "CREATED_DATE desc",
                                                     Optional ByVal log As UserLog = Nothing) As List(Of PE_KPI_ASSESMENT_DETAIL_DTO)
        Try
            Dim query = From k In Context.PE_KPI_ASSESSMENT_DETAIL
                        From o In Context.OT_OTHER_LIST.Where(Function(f) f.ID = k.SOURCE).DefaultIfEmpty
                        From p In Context.PE_KPI_ASSESSMENT.Where(Function(f) f.ID = k.GOAL_ID)
                        From s In Context.PA_PAYMENT_LIST.Where(Function(f) f.CODE = "HS_DGHSV" AndAlso f.EFFECTIVE_DATE <= p.START_DATE).DefaultIfEmpty
                        From o2 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = k.FREQUENCY).DefaultIfEmpty
                        From o3 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = k.GOAL_TYPE).DefaultIfEmpty
                        From o4 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = k.TARGET_TYPE).DefaultIfEmpty
                        From o5 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = k.DVT).DefaultIfEmpty
                        Where lstCode.Contains(k.KPI_ASSESSMENT_TEXT) AndAlso k.GOAL_ID = _filter.GOAL_ID
                        Select New PE_KPI_ASSESMENT_DETAIL_DTO With {
                                .ID = k.ID,
                                .BENCHMARK = k.BENCHMARK,
                                .DESCRIPTION = k.DESCRIPTION,
                                .DIRECT_ACTUAL = k.DIRECT_ACTUAL,
                                .DIRECT_POINT = k.DIRECT_POINTS,
                                .EMPLOYEE_ACTUAL = k.EMPLOYEE_ACTUAL,
                                .EMPLOYEE_POINT = k.EMPLOYEE_POINTS,
                                .FREQUENCY_ID = k.FREQUENCY,
                                .FREQUENCY_NAME = o2.NAME_VN,
                                .GOAL_ID = k.GOAL_ID,
                                .GOAL_TYPE = k.GOAL_TYPE,
                                .GOAL_TYPE_NAME = o3.NAME_VN,
                                .GOAL_TYPE_CODE = o3.CODE,
                                .KPI_ASSESSMENT = k.KPI_ASSESSMENT,
                                .KPI_ASSESSMENT_TEXT = k.KPI_ASSESSMENT_TEXT,
                                .NOTE = k.NOTE,
                                .NOTE_QLTT = k.NOTE_QLTT,
                                .RATIO = k.RATIO,
                                .SOURCE_ID = k.SOURCE,
                                .SOURCE_NAME = o.NAME_VN,
                                .TARGET = k.TARGET,
                                .TARGET_MIN = k.TARGET_MIN,
                                .TARGET_TYPE_ID = k.TARGET_TYPE,
                                .TARGET_TYPE_NAME = o4.NAME_VN,
                                .TARGET_TYPE_CODE = o4.CODE,
                                .UNIT_ID = k.DVT,
                                .UNIT_NAME = o5.NAME_VN,
                                .SOA = s.VALUE}
            query = query.OrderBy("ID DESC")
            Total = query.Count
            query = query.Skip(PageIndex * PageSize).Take(PageSize)
            Dim rs = query.ToList
            For Each item As PE_KPI_ASSESMENT_DETAIL_DTO In rs
                If item.TARGET_TYPE_CODE <> "DATE" Then
                    If item.GOAL_TYPE_CODE = "CNCT" Then
                        If IsNumeric(item.EMPLOYEE_ACTUAL) AndAlso IsNumeric(item.TARGET) AndAlso IsNumeric(item.BENCHMARK) Then
                            If Integer.Parse(item.EMPLOYEE_ACTUAL) >= Integer.Parse(item.TARGET) Then
                                item.EMPLOYEE_POINT = Integer.Parse(item.TARGET) / Integer.Parse(item.EMPLOYEE_ACTUAL) * Integer.Parse(item.BENCHMARK) * item.SOA / 100
                            Else
                                item.EMPLOYEE_POINT = Integer.Parse(item.BENCHMARK)
                            End If
                        End If
                        If IsNumeric(item.DIRECT_ACTUAL) AndAlso IsNumeric(item.TARGET) AndAlso IsNumeric(item.BENCHMARK) Then
                            If Integer.Parse(item.DIRECT_ACTUAL) >= Integer.Parse(item.TARGET) Then
                                item.DIRECT_POINT = Integer.Parse(item.TARGET) / Integer.Parse(item.DIRECT_ACTUAL) * Integer.Parse(item.BENCHMARK) * item.SOA / 100
                            Else
                                item.DIRECT_POINT = Integer.Parse(item.BENCHMARK)
                            End If
                        End If
                    ElseIf item.GOAL_TYPE_CODE = "CLCT" Then
                        If IsNumeric(item.EMPLOYEE_ACTUAL) AndAlso IsNumeric(item.TARGET) AndAlso IsNumeric(item.BENCHMARK) Then
                            If Integer.Parse(item.EMPLOYEE_ACTUAL) <= Integer.Parse(item.TARGET) Then
                                item.EMPLOYEE_POINT = Integer.Parse(item.BENCHMARK) / Integer.Parse(item.TARGET) * Integer.Parse(item.EMPLOYEE_ACTUAL) * item.SOA / 100
                            Else
                                item.EMPLOYEE_POINT = Integer.Parse(item.BENCHMARK)
                            End If
                        End If
                        If IsNumeric(item.DIRECT_ACTUAL) AndAlso IsNumeric(item.TARGET) AndAlso IsNumeric(item.BENCHMARK) Then
                            If Integer.Parse(item.DIRECT_ACTUAL) <= Integer.Parse(item.TARGET) Then
                                item.DIRECT_POINT = Integer.Parse(item.BENCHMARK) / Integer.Parse(item.TARGET) * Integer.Parse(item.DIRECT_ACTUAL) * item.SOA / 100
                            Else
                                item.DIRECT_POINT = Integer.Parse(item.BENCHMARK)
                            End If
                        End If
                    ElseIf item.GOAL_TYPE_CODE = "DKD" Then
                        If IsNumeric(item.EMPLOYEE_ACTUAL) AndAlso IsNumeric(item.BENCHMARK) Then
                            If Integer.Parse(item.EMPLOYEE_ACTUAL) >= Integer.Parse(item.TARGET) Then
                                item.EMPLOYEE_POINT = Integer.Parse(item.BENCHMARK)
                            Else
                                item.EMPLOYEE_POINT = 0
                            End If
                        End If
                        If IsNumeric(item.DIRECT_ACTUAL) AndAlso IsNumeric(item.BENCHMARK) Then
                            If Integer.Parse(item.DIRECT_ACTUAL) >= Integer.Parse(item.TARGET) Then
                                item.DIRECT_POINT = Integer.Parse(item.BENCHMARK)
                            Else
                                item.DIRECT_POINT = 0
                            End If
                        End If
                    End If
                End If
            Next

            Return rs
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try
    End Function
    Public Function GetKpiAssessmentDetailByGoal(ByVal _filter As PE_KPI_ASSESMENT_DETAIL_DTO,
                                 ByVal PageIndex As Integer,
                                 ByVal PageSize As Integer,
                                 ByRef Total As Integer, ByVal _param As ParamDTO,
                                 Optional ByVal Sorts As String = "CREATED_DATE desc",
                                 Optional ByVal log As UserLog = Nothing) As List(Of PE_KPI_ASSESMENT_DETAIL_DTO)
        Try
            Dim query = From k In Context.PE_KPI_ASSESSMENT_DETAIL
                        From o In Context.OT_OTHER_LIST.Where(Function(f) f.ID = k.SOURCE).DefaultIfEmpty
                        From o2 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = k.FREQUENCY).DefaultIfEmpty
                        From o3 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = k.GOAL_TYPE).DefaultIfEmpty
                        From o4 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = k.TARGET_TYPE).DefaultIfEmpty
                        From o5 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = k.DVT).DefaultIfEmpty
                        Where k.GOAL_ID = _filter.GOAL_ID
                        Select New PE_KPI_ASSESMENT_DETAIL_DTO With {
                                .ID = k.ID,
                                .BENCHMARK = k.BENCHMARK,
                                .DESCRIPTION = k.DESCRIPTION,
                                .DIRECT_ACTUAL = k.DIRECT_ACTUAL,
                                .DIRECT_POINT = k.DIRECT_POINTS,
                                .EMPLOYEE_ACTUAL = k.EMPLOYEE_ACTUAL,
                                .EMPLOYEE_POINT = k.EMPLOYEE_POINTS,
                                .FREQUENCY_ID = k.FREQUENCY,
                                .FREQUENCY_NAME = o2.NAME_VN,
                                .GOAL_ID = k.GOAL_ID,
                                .GOAL_TYPE = k.GOAL_TYPE,
                                .GOAL_TYPE_NAME = o3.NAME_VN,
                                .KPI_ASSESSMENT = k.KPI_ASSESSMENT,
                                .KPI_ASSESSMENT_TEXT = k.KPI_ASSESSMENT_TEXT,
                                .NOTE = k.NOTE,
                                .NOTE_QLTT = k.NOTE_QLTT,
                                .RATIO = k.RATIO,
                                .SOURCE_ID = k.SOURCE,
                                .SOURCE_NAME = o.NAME_VN,
                                .TARGET = k.TARGET,
                                .TARGET_MIN = k.TARGET_MIN,
                                .TARGET_TYPE_ID = k.TARGET_TYPE,
                                .TARGET_TYPE_NAME = o4.NAME_VN,
                                .UNIT_ID = k.DVT,
                                .UNIT_NAME = o5.NAME_VN}

            query = query.OrderBy("ID DESC")
            Total = query.Count
            query = query.Skip(PageIndex * PageSize).Take(PageSize)
            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try
    End Function
    Public Function GetKpiAssessmentByID(ByVal id As Decimal) As KPI_ASSESSMENT_DTO
        Try
            Dim obj As KPI_ASSESSMENT_DTO = (From kpi_assessment In Context.PE_KPI_ASSESSMENT
                                             From employee In Context.HU_EMPLOYEE.Where(Function(f) f.ID = kpi_assessment.EMPLOYEE)
                                             From title In Context.HU_TITLE.Where(Function(f) f.ID = employee.TITLE_ID).DefaultIfEmpty
                                             From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = employee.ORG_ID).DefaultIfEmpty
                                             From period In Context.PE_PERIOD.Where(Function(f) f.ID = kpi_assessment.PE_PERIOD_ID).DefaultIfEmpty
                                             From status In Context.OT_OTHER_LIST.Where(Function(f) f.ID = kpi_assessment.STATUS_ID).DefaultIfEmpty
                                             Where kpi_assessment.ID = id
                                             Select New KPI_ASSESSMENT_DTO With {
                                                     .ID = kpi_assessment.ID,
                                                     .EMPLOYEE = kpi_assessment.EMPLOYEE,
                                                     .EMPLOYEE_CODE = employee.EMPLOYEE_CODE,
                                                     .EMPLOYEE_NAME = employee.FULLNAME_VN,
                                                     .TITLE_ID = employee.TITLE_ID,
                                                     .TITLE_NAME = title.NAME_VN,
                                                     .ORG_ID = employee.ORG_ID,
                                                     .ORG_NAME = org.NAME_VN,
                                                     .YEAR = kpi_assessment.YEAR,
                                                     .PE_PERIOD_ID = kpi_assessment.PE_PERIOD_ID,
                                                     .PE_PERIOD_NAME = period.NAME,
                                                     .START_DATE = kpi_assessment.START_DATE,
                                                     .END_DATE = kpi_assessment.END_DATE,
                                                     .NUMBER_MONTH = kpi_assessment.NUMBER_MONTH,
                                                     .GOAL = kpi_assessment.GOAL,
                                                     .EVALUATION_POINTS = kpi_assessment.EVALUATION_POINTS,
                                                     .CLASSIFICATION = kpi_assessment.CLASSIFICATION,
                                                     .REMARK = kpi_assessment.REMARK,
                                                     .REASON = kpi_assessment.REASON,
                                                     .STATUS_ID = kpi_assessment.STATUS_ID,
                                                     .STATUS_NAME = status.NAME_VN,
                                                     .PORTAL_ID = kpi_assessment.PORTAL_ID,
                                                     .IS_CONFIRM = kpi_assessment.IS_CONFIRM,
                                                     .IS_FROM_PORTAL = If(kpi_assessment.PORTAL_ID = -1, True, False),
                                                     .EFFECT_DATE = kpi_assessment.EFFECT_DATE}).FirstOrDefault

            obj.assesmentDetail = (From k In Context.PE_KPI_ASSESSMENT_DETAIL
                                   From o In Context.OT_OTHER_LIST.Where(Function(f) f.ID = k.SOURCE).DefaultIfEmpty
                                   From o2 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = k.FREQUENCY).DefaultIfEmpty
                                   From o3 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = k.GOAL_TYPE).DefaultIfEmpty
                                   From o4 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = k.TARGET_TYPE).DefaultIfEmpty
                                   From o5 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = k.DVT).DefaultIfEmpty
                                   Where k.GOAL_ID = obj.ID
                                   Select New PE_KPI_ASSESMENT_DETAIL_DTO With {
                                           .ID = k.ID,
                                           .BENCHMARK = k.BENCHMARK,
                                           .DESCRIPTION = k.DESCRIPTION,
                                           .DIRECT_ACTUAL = k.DIRECT_ACTUAL,
                                           .DIRECT_POINT = k.DIRECT_POINTS,
                                           .EMPLOYEE_ACTUAL = k.EMPLOYEE_ACTUAL,
                                           .EMPLOYEE_POINT = k.EMPLOYEE_POINTS,
                                           .FREQUENCY_ID = k.FREQUENCY,
                                           .FREQUENCY_NAME = o2.NAME_VN,
                                           .GOAL_ID = k.GOAL_ID,
                                           .GOAL_TYPE = k.GOAL_TYPE,
                                           .GOAL_TYPE_NAME = o3.NAME_VN,
                                           .KPI_ASSESSMENT = k.KPI_ASSESSMENT,
                                           .KPI_ASSESSMENT_TEXT = k.KPI_ASSESSMENT_TEXT,
                                           .NOTE = k.NOTE,
                                           .KPI_TYPE = If(k.KPI_ASSESSMENT.HasValue, "", "KPI mới"),
                                           .NOTE_QLTT = k.NOTE_QLTT,
                                           .RATIO = k.RATIO,
                                           .SOURCE_ID = k.SOURCE,
                                           .SOURCE_NAME = o.NAME_VN,
                                           .TARGET = k.TARGET,
                                           .TARGET_MIN = k.TARGET_MIN,
                                           .TARGET_TYPE_ID = k.TARGET_TYPE,
                                           .TARGET_TYPE_NAME = o4.NAME_VN,
                                           .UNIT_ID = k.DVT,
                                           .UNIT_NAME = o5.NAME_VN,
                                           .IS_IN_DB = "X"}).ToList

            Return obj
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try
    End Function
    Public Function GetKpiAssessmentDetail(ByVal _filter As PE_KPI_ASSESMENT_DETAIL_DTO,
                                 ByVal PageIndex As Integer,
                                 ByVal PageSize As Integer,
                                 ByRef Total As Integer, ByVal _param As ParamDTO,
                                 Optional ByVal Sorts As String = "CREATED_DATE desc",
                                 Optional ByVal log As UserLog = Nothing) As List(Of PE_KPI_ASSESMENT_DETAIL_DTO)

        Try

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using

            Dim query = From k In Context.PE_KPI_ASSESSMENT_DETAIL
                        From p In Context.PE_KPI_ASSESSMENT.Where(Function(f) f.ID = k.GOAL_ID).DefaultIfEmpty
                        From employee In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE)
                        From o In Context.OT_OTHER_LIST.Where(Function(f) f.ID = k.SOURCE).DefaultIfEmpty
                        From o2 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = k.FREQUENCY).DefaultIfEmpty
                        From o3 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = k.GOAL_TYPE).DefaultIfEmpty
                        From o4 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = k.TARGET_TYPE).DefaultIfEmpty
                        From o5 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = k.DVT).DefaultIfEmpty
                        From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = employee.ORG_ID And f.USERNAME = log.Username.ToUpper)
                        From status In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.STATUS_ID And f.TYPE_CODE = "PROCESS_STATUS")
                        From title In Context.HU_TITLE.Where(Function(f) f.ID = employee.TITLE_ID).DefaultIfEmpty
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = employee.ORG_ID).DefaultIfEmpty
                        Select New PE_KPI_ASSESMENT_DETAIL_DTO With {
                                .ID = k.ID,
                                .BENCHMARK = k.BENCHMARK,
                                .DESCRIPTION = k.DESCRIPTION,
                                .DIRECT_ACTUAL = k.DIRECT_ACTUAL,
                                .DIRECT_POINT = k.DIRECT_POINTS,
                                .EMPLOYEE_ACTUAL = k.EMPLOYEE_ACTUAL,
                                .EMPLOYEE_POINT = k.EMPLOYEE_POINTS,
                                .FREQUENCY_ID = k.FREQUENCY,
                                .FREQUENCY_NAME = o2.NAME_VN,
                                .GOAL_ID = k.GOAL_ID,
                                .GOAL_TYPE = k.GOAL_TYPE,
                                .GOAL_TYPE_NAME = o3.NAME_VN,
                                .KPI_ASSESSMENT = k.KPI_ASSESSMENT,
                                .KPI_ASSESSMENT_TEXT = k.KPI_ASSESSMENT_TEXT,
                                .KPI_TYPE = If(Not k.KPI_ASSESSMENT.HasValue, "KPI mới", ""),
                                .NOTE = k.NOTE,
                                .NOTE_QLTT = k.NOTE_QLTT,
                                .RATIO = k.RATIO,
                                .SOURCE_ID = k.SOURCE,
                                .SOURCE_NAME = o.NAME_VN,
                                .TARGET = k.TARGET,
                                .TARGET_MIN = k.TARGET_MIN,
                                .TARGET_TYPE_ID = k.TARGET_TYPE,
                                .TARGET_TYPE_NAME = o4.NAME_VN,
                                .UNIT_ID = k.DVT,
                                .UNIT_NAME = o5.NAME_VN,
                                .YEAR = p.YEAR,
                                .PE_PERIOD_ID = p.PE_PERIOD_ID,
                                .STATUS_ID = p.STATUS_ID,
                                .STATUS_NAME = status.NAME_VN,
                                .EMPLOYEE_CODE = employee.EMPLOYEE_CODE,
                                .EMPLOYEE_NAME = employee.FULLNAME_VN,
                                .TITLE_ID = employee.TITLE_ID,
                                .TITLE_NAME = title.NAME_VN,
                                .ORG_ID = employee.ORG_ID,
                                .ORG_NAME = org.NAME_VN}

            If _filter.YEAR IsNot Nothing Then
                query = query.Where(Function(f) f.YEAR = _filter.YEAR)
            End If
            If _filter.PE_PERIOD_ID IsNot Nothing Then
                query = query.Where(Function(f) f.PE_PERIOD_ID = _filter.PE_PERIOD_ID)
            End If
            If Not String.IsNullOrEmpty(_filter.KPI_TYPE) Then
                query = query.Where(Function(f) f.KPI_TYPE.ToUpper.Contains(_filter.KPI_TYPE.ToUpper))
            End If
            If _filter.STATUS_ID IsNot Nothing Then
                query = query.Where(Function(f) f.STATUS_ID = _filter.STATUS_ID)
            End If
            query = query.OrderBy("ID DESC")
            Total = query.Count
            query = query.Skip(PageIndex * PageSize).Take(PageSize)
            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try
    End Function
    Public Function GetKpiAssessment(ByVal _filter As KPI_ASSESSMENT_DTO,
                                 ByVal PageIndex As Integer,
                                 ByVal PageSize As Integer,
                                 ByRef Total As Integer, ByVal _param As ParamDTO,
                                 Optional ByVal Sorts As String = "CREATED_DATE desc",
                                 Optional ByVal log As UserLog = Nothing) As List(Of KPI_ASSESSMENT_DTO)

        Try

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using

            Dim query = From kpi_assessment In Context.PE_KPI_ASSESSMENT
                        From employee In Context.HU_EMPLOYEE.Where(Function(f) f.ID = kpi_assessment.EMPLOYEE)
                        From title In Context.HU_TITLE.Where(Function(f) f.ID = employee.TITLE_ID).DefaultIfEmpty
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = employee.ORG_ID).DefaultIfEmpty
                        From period In Context.PE_PERIOD.Where(Function(f) f.ID = kpi_assessment.PE_PERIOD_ID).DefaultIfEmpty
                        From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = employee.ORG_ID And f.USERNAME = log.Username.ToUpper)
                        From status In Context.OT_OTHER_LIST.Where(Function(f) f.ID = kpi_assessment.STATUS_ID And f.TYPE_CODE = "PROCESS_STATUS").DefaultIfEmpty
                        From status_kpi In Context.OT_OTHER_LIST.Where(Function(f) f.ID = kpi_assessment.IS_CONFIRM And f.TYPE_CODE = "PROCESS_STATUS").DefaultIfEmpty
                        Select New KPI_ASSESSMENT_DTO With {
                                .ID = kpi_assessment.ID,
                                .EMPLOYEE = kpi_assessment.EMPLOYEE,
                                .EMPLOYEE_CODE = employee.EMPLOYEE_CODE,
                                .EMPLOYEE_NAME = employee.FULLNAME_VN,
                                .TITLE_ID = employee.TITLE_ID,
                                .TITLE_NAME = title.NAME_VN,
                                .ORG_ID = employee.ORG_ID,
                                .ORG_NAME = org.NAME_VN,
                                .YEAR = kpi_assessment.YEAR,
                                .PE_PERIOD_ID = kpi_assessment.PE_PERIOD_ID,
                                .PE_PERIOD_NAME = period.NAME,
                                .START_DATE = kpi_assessment.START_DATE,
                                .END_DATE = kpi_assessment.END_DATE,
                                .NUMBER_MONTH = kpi_assessment.NUMBER_MONTH,
                                .GOAL = kpi_assessment.GOAL,
                                .EVALUATION_POINTS = kpi_assessment.EVALUATION_POINTS,
                                .CLASSIFICATION = kpi_assessment.CLASSIFICATION,
                                .REMARK = kpi_assessment.REMARK,
                                .REASON = kpi_assessment.REASON,
                                .STATUS_ID = kpi_assessment.STATUS_ID,
                                .STATUS_NAME = status.NAME_VN,
                                .IS_CONFIRM_STT = status_kpi.NAME_VN,
                                .PORTAL_ID = kpi_assessment.PORTAL_ID,
                                .REASON_CONFIRM = kpi_assessment.REASON_CONFIRM,
                                .RATIO = kpi_assessment.RATIO,
                                .IS_FROM_PORTAL = If(kpi_assessment.PORTAL_ID = 1, True, False),
                                .EFFECT_DATE = kpi_assessment.EFFECT_DATE}

            If _filter.YEAR IsNot Nothing Then
                query = query.Where(Function(f) f.YEAR = _filter.YEAR)
            End If
            If _filter.PE_PERIOD_ID IsNot Nothing Then
                query = query.Where(Function(f) f.PE_PERIOD_ID = _filter.PE_PERIOD_ID)
            End If
            If _filter.STATUS_ID IsNot Nothing Then
                query = query.Where(Function(f) f.STATUS_ID = _filter.STATUS_ID)
            End If
            query = query.OrderBy("ID DESC")
            Total = query.Count
            query = query.Skip(PageIndex * PageSize).Take(PageSize)
            Dim list = query.ToList
            If _filter.MULTI_CRITERIA Then
                list = list.Where(Function(x) list.Where(Function(y) x.EMPLOYEE = y.EMPLOYEE).Count() > 1).ToList
            End If
            Return list
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try
    End Function
    Public Function GetKpiAssessmentResult(ByVal _filter As KPI_ASSESSMENT_RESULT_DTO,
                             ByVal PageIndex As Integer,
                             ByVal PageSize As Integer,
                             ByRef Total As Integer, ByVal _param As ParamDTO,
                             Optional ByVal Sorts As String = "CREATED_DATE desc",
                             Optional ByVal log As UserLog = Nothing) As List(Of KPI_ASSESSMENT_RESULT_DTO)

        Try

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using

            Dim query = From kpi_assessment In Context.PE_KPI_ASSESSMENT_RESULT
                        From employee In Context.HU_EMPLOYEE.Where(Function(f) f.ID = kpi_assessment.EMPLOYEE_ID)
                        From title In Context.HU_TITLE.Where(Function(f) f.ID = employee.TITLE_ID).DefaultIfEmpty
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = employee.ORG_ID).DefaultIfEmpty
                        From period In Context.PE_PERIOD.Where(Function(f) f.ID = kpi_assessment.PE_PERIOD_ID).DefaultIfEmpty
                        From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = employee.ORG_ID And f.USERNAME = log.Username.ToUpper)
                        Select New KPI_ASSESSMENT_RESULT_DTO With {
                                .ID = kpi_assessment.ID,
                                .EMPLOYEE = kpi_assessment.EMPLOYEE_ID,
                                .EMPLOYEE_CODE = employee.EMPLOYEE_CODE,
                                .EMPLOYEE_NAME = employee.FULLNAME_VN,
                                .TITLE_ID = employee.TITLE_ID,
                                .TITLE_NAME = title.NAME_VN,
                                .ORG_ID = employee.ORG_ID,
                                .ORG_NAME = org.NAME_VN,
                                .YEAR = kpi_assessment.YEAR,
                                .PE_PERIOD_ID = kpi_assessment.PE_PERIOD_ID,
                                .PE_PERIOD_NAME = period.NAME,
                                .START_DATE = kpi_assessment.START_DATE,
                                .END_DATE = kpi_assessment.END_DATE,
                                .EVALUATION_POINTS = kpi_assessment.EVALUATION_POINTS,
                                .CLASSIFICATION = kpi_assessment.CLASSIFICATION,
                                .IS_LOCK = If(kpi_assessment.IS_LOCK = -1, True, False)}

            If _filter.YEAR IsNot Nothing Then
                query = query.Where(Function(f) f.YEAR = _filter.YEAR)
            End If
            If _filter.PE_PERIOD_ID IsNot Nothing Then
                query = query.Where(Function(f) f.PE_PERIOD_ID = _filter.PE_PERIOD_ID)
            End If
            If _filter.START_DATE IsNot Nothing Then
                query = query.Where(Function(f) f.START_DATE = _filter.START_DATE)
            End If
            If _filter.END_DATE IsNot Nothing Then
                query = query.Where(Function(f) f.END_DATE = _filter.END_DATE)
            End If
            query = query.OrderBy("ID DESC")
            Total = query.Count
            query = query.Skip(PageIndex * PageSize).Take(PageSize)
            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try
    End Function
    Public Function ChangeStatusAssessmentResult(ByVal lstID As List(Of Decimal), ByVal _status As Decimal, ByVal _log As UserLog) As Boolean
        Try
            Dim lstObj = From p In Context.PE_KPI_ASSESSMENT_RESULT Where lstID.Contains(p.ID)

            For Each item In lstObj
                item.IS_LOCK = _status
            Next
            Context.SaveChanges(_log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
    Public Function SaveChangeRatio(ByVal list As List(Of KPI_ASSESSMENT_DTO), ByVal log As UserLog) As Boolean

        Try
            For Each item In list
                Dim objdata = (From p In Context.PE_KPI_ASSESSMENT Where p.ID = item.ID).FirstOrDefault
                objdata.RATIO = item.RATIO
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try
    End Function
    Public Function DeleteKpiAssessment(ByVal objID As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim objKpiAssessment As PE_KPI_ASSESSMENT
        Dim objKpiAssessmentDetail As PE_KPI_ASSESSMENT_DETAIL
        Try
            For Each id In objID
                objKpiAssessment = (From p In Context.PE_KPI_ASSESSMENT Where p.ID = id).FirstOrDefault
                objKpiAssessmentDetail = (From p In Context.PE_KPI_ASSESSMENT_DETAIL Where p.GOAL_ID = id).FirstOrDefault
                Context.PE_KPI_ASSESSMENT.DeleteObject(objKpiAssessment)
                Context.PE_KPI_ASSESSMENT_DETAIL.DeleteObject(objKpiAssessmentDetail)
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try
    End Function
    Public Function DeleteKpiAssessmentResult(ByVal objID As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim objKpiAssessment As PE_KPI_ASSESSMENT_RESULT
        Try
            For Each id In objID
                objKpiAssessment = (From p In Context.PE_KPI_ASSESSMENT_RESULT Where p.ID = id).FirstOrDefault
                Context.PE_KPI_ASSESSMENT_RESULT.DeleteObject(objKpiAssessment)
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try
    End Function

    Public Function GetPeriodByYear(ByVal Year As Decimal) As DataTable
        Try
            Dim query = From p1 In Context.PE_PERIOD
                        From p2 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p1.TYPE_ASS)
                        Where p1.YEAR = Year And p1.ACTFLG = "A"
                        Select p1.ID, p1.NAME

            Return query.ToList.ToTable

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try
    End Function

    Public Function GetDateByPeriod(ByVal Period As Decimal) As DataTable
        Try
            Dim query = From p In Context.PE_PERIOD
                        Where p.ID = Period And p.ACTFLG = "A"
                        Select p.START_DATE, p.END_DATE

            Return query.ToList.ToTable

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try
    End Function

    Public Function InsertKpiAssessment(ByVal obj As KPI_ASSESSMENT_DTO) As Boolean
        Dim objAssessment As New PE_KPI_ASSESSMENT
        Dim id = Utilities.GetNextSequence(Context, Context.PE_KPI_ASSESSMENT.EntitySet.Name)
        Try
            With objAssessment
                .ID = id
                .EMPLOYEE = obj.EMPLOYEE
                .YEAR = obj.YEAR
                .PE_PERIOD_ID = obj.PE_PERIOD_ID
                .START_DATE = obj.START_DATE
                .END_DATE = obj.END_DATE
                .EFFECT_DATE = obj.EFFECT_DATE
                .NUMBER_MONTH = obj.NUMBER_MONTH
                .GOAL = obj.GOAL
                .EVALUATION_POINTS = obj.EVALUATION_POINTS
                .CLASSIFICATION = obj.CLASSIFICATION
                .REMARK = obj.REMARK
                .REASON = obj.REASON
                .STATUS_ID = obj.STATUS_ID
                .PORTAL_ID = obj.PORTAL_ID
                .IS_CONFIRM = obj.IS_CONFIRM
            End With
            Context.PE_KPI_ASSESSMENT.AddObject(objAssessment)
            If obj.assesmentDetail IsNot Nothing Then
                For Each item As PE_KPI_ASSESMENT_DETAIL_DTO In obj.assesmentDetail
                    Dim id1 = Utilities.GetNextSequence(Context, Context.PE_KPI_ASSESSMENT_DETAIL.EntitySet.Name)
                    Context.PE_KPI_ASSESSMENT_DETAIL.AddObject(New PE_KPI_ASSESSMENT_DETAIL With {
                                                   .ID = id1,
                                                   .GOAL_ID = id,
                                                   .KPI_ASSESSMENT = item.KPI_ASSESSMENT,
                                                   .KPI_ASSESSMENT_TEXT = item.KPI_ASSESSMENT_TEXT,
                                                   .DVT = item.UNIT_ID,
                                                   .DESCRIPTION = item.DESCRIPTION,
                                                   .FREQUENCY = item.FREQUENCY_ID,
                                                   .SOURCE = item.SOURCE_ID,
                                                   .GOAL_TYPE = item.GOAL_TYPE,
                                                   .RATIO = item.RATIO,
                                                   .TARGET_TYPE = item.TARGET_TYPE_ID,
                                                   .TARGET = item.TARGET,
                                                   .TARGET_MIN = item.TARGET_MIN,
                                                   .SOURCE_TEXT = item.SOURCE_TEXT,
                                                   .BENCHMARK = item.BENCHMARK})
                Next
            End If
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".InsertKpiAssessment")
            Throw ex
        End Try
    End Function

#Region "Portal Assessment"
    Public Function GetPortalAssessment(ByVal _filter As KPI_ASSESSMENT_DTO,
                                         ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer,
                                         Optional ByVal Sorts As String = "EFFECT_DATE desc",
                                         Optional ByVal log As UserLog = Nothing) As List(Of KPI_ASSESSMENT_DTO)

        Try

            Dim query = From p In Context.PE_KPI_ASSESSMENT
                        From employee In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE)
                        From title In Context.HU_TITLE.Where(Function(f) f.ID = employee.TITLE_ID).DefaultIfEmpty
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = employee.ORG_ID).DefaultIfEmpty
                        From period In Context.PE_PERIOD.Where(Function(f) f.ID = p.PE_PERIOD_ID).DefaultIfEmpty
                        From status In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.STATUS_ID).DefaultIfEmpty
                        From sc In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.IS_CONFIRM).DefaultIfEmpty
                        From papp In Context.PROCESS_APPROVED_STATUS.Where(Function(f) f.ID_REGGROUP = p.ID And f.APP_STATUS = 0 _
                            And f.PROCESS_TYPE.ToUpper = "PERFORMANCE" And f.APP_LEVEL = (Context.PROCESS_APPROVED_STATUS.Where(Function(h) h.ID_REGGROUP = p.ID And h.APP_STATUS = 0 And h.PROCESS_TYPE.ToUpper = "PERFORMANCE").Min(Function(k) k.APP_LEVEL))).DefaultIfEmpty()
                        From eeap In Context.HU_EMPLOYEE.Where(Function(f) f.ID = papp.EMPLOYEE_APPROVED And p.STATUS_ID <> 2).DefaultIfEmpty
                        From last_app In Context.PROCESS_APPROVED_STATUS.Where(Function(f) f.ID_REGGROUP = p.ID And f.APP_STATUS = 1 _
                            And f.PROCESS_TYPE.ToUpper = "PERFORMANCE" And f.APP_LEVEL = (Context.PROCESS_APPROVED_STATUS.Where(Function(h) h.ID_REGGROUP = p.ID And h.APP_STATUS = 1 And h.PROCESS_TYPE.ToUpper = "PERFORMANCE").Max(Function(k) k.APP_LEVEL))).DefaultIfEmpty()
                        From e_last_ap In Context.HU_EMPLOYEE.Where(Function(f) f.ID = last_app.EMPLOYEE_APPROVED).DefaultIfEmpty
                        From last_not_app In Context.PROCESS_APPROVED_STATUS.Where(Function(f) f.ID_REGGROUP = p.ID And f.APP_STATUS = 2 _
                                        And f.PROCESS_TYPE.ToUpper = "PERFORMANCE" And f.APP_LEVEL = (Context.PROCESS_APPROVED_STATUS.Where(Function(h) h.ID_REGGROUP = p.ID And h.APP_STATUS = 2 And h.PROCESS_TYPE.ToUpper = "PERFORMANCE").Max(Function(k) k.APP_LEVEL))).DefaultIfEmpty()
                        From e_last_not_ap In Context.HU_EMPLOYEE.Where(Function(f) f.ID = last_not_app.EMPLOYEE_APPROVED).DefaultIfEmpty
                        From kpi_papp In Context.PROCESS_APPROVED_STATUS.Where(Function(f) f.ID_REGGROUP = p.ID And f.APP_STATUS = 0 _
                            And f.PROCESS_TYPE.ToUpper = "PERFORMANCE_KPI" And f.APP_LEVEL = (Context.PROCESS_APPROVED_STATUS.Where(Function(h) h.ID_REGGROUP = p.ID And h.APP_STATUS = 0 And h.PROCESS_TYPE.ToUpper = "PERFORMANCE_KPI").Min(Function(k) k.APP_LEVEL))).DefaultIfEmpty()
                        From kpi_eeap In Context.HU_EMPLOYEE.Where(Function(f) f.ID = kpi_papp.EMPLOYEE_APPROVED And p.IS_CONFIRM <> 2).DefaultIfEmpty
                        From kpi_last_app In Context.PROCESS_APPROVED_STATUS.Where(Function(f) f.ID_REGGROUP = p.ID And f.APP_STATUS = 1 _
                            And f.PROCESS_TYPE.ToUpper = "PERFORMANCE_KPI" And f.APP_LEVEL = (Context.PROCESS_APPROVED_STATUS.Where(Function(h) h.ID_REGGROUP = p.ID And h.APP_STATUS = 1 And h.PROCESS_TYPE.ToUpper = "PERFORMANCE_KPI").Max(Function(k) k.APP_LEVEL))).DefaultIfEmpty()
                        From kpi_e_last_ap In Context.HU_EMPLOYEE.Where(Function(f) f.ID = kpi_last_app.EMPLOYEE_APPROVED).DefaultIfEmpty
                        From kpi_last_not_app In Context.PROCESS_APPROVED_STATUS.Where(Function(f) f.ID_REGGROUP = p.ID And f.APP_STATUS = 2 _
                                        And f.PROCESS_TYPE.ToUpper = "PERFORMANCE_KPI" And f.APP_LEVEL = (Context.PROCESS_APPROVED_STATUS.Where(Function(h) h.ID_REGGROUP = p.ID And h.APP_STATUS = 2 And h.PROCESS_TYPE.ToUpper = "PERFORMANCE_KPI").Max(Function(k) k.APP_LEVEL))).DefaultIfEmpty()
                        From kpi_e_last_not_ap In Context.HU_EMPLOYEE.Where(Function(f) f.ID = kpi_last_not_app.EMPLOYEE_APPROVED).DefaultIfEmpty
                        Where (p.EMPLOYEE = _filter.EMPLOYEE And p.PORTAL_ID = 1)
                        Select New KPI_ASSESSMENT_DTO With {
                               .ID = p.ID,
                               .EMPLOYEE = p.EMPLOYEE,
                               .EMPLOYEE_CODE = employee.EMPLOYEE_CODE,
                               .EMPLOYEE_NAME = employee.FULLNAME_VN,
                               .TITLE_ID = employee.TITLE_ID,
                               .TITLE_NAME = title.NAME_VN,
                               .ORG_ID = employee.ORG_ID,
                               .ORG_NAME = org.NAME_VN,
                               .YEAR = p.YEAR,
                               .PE_PERIOD_ID = p.PE_PERIOD_ID,
                               .PE_PERIOD_NAME = period.NAME,
                               .START_DATE = p.START_DATE,
                               .END_DATE = p.END_DATE,
                               .NUMBER_MONTH = p.NUMBER_MONTH,
                               .GOAL = p.GOAL,
                               .EVALUATION_POINTS = p.EVALUATION_POINTS,
                               .CLASSIFICATION = p.CLASSIFICATION,
                               .REMARK = p.REMARK,
                               .REASON = p.REASON,
                               .STATUS_ID = p.STATUS_ID,
                               .STATUS_NAME = status.NAME_VN,
                               .PORTAL_ID = p.PORTAL_ID,
                               .EMP_APPROVES_NAME = If(p.STATUS_ID.Value = 0, eeap.EMPLOYEE_CODE & " - " & eeap.FULLNAME_VN, If(p.STATUS_ID.Value = 1, e_last_ap.EMPLOYEE_CODE & " - " & e_last_ap.FULLNAME_VN, If(p.STATUS_ID.Value = 2, e_last_not_ap.EMPLOYEE_CODE & " - " & e_last_not_ap.FULLNAME_VN, Nothing))),
                               .IS_FROM_PORTAL = If(p.PORTAL_ID = -1, True, False),
                               .REASON_CONFIRM = p.REASON_CONFIRM,
                               .IS_CONFIRM = p.IS_CONFIRM,
                               .CONFIRM_NAME = sc.NAME_VN,
                               .KPI_EMP_APPROVES_NAME = If(p.IS_CONFIRM.Value = 0, kpi_eeap.EMPLOYEE_CODE & " - " & kpi_eeap.FULLNAME_VN, If(p.IS_CONFIRM.Value = 1, kpi_e_last_ap.EMPLOYEE_CODE & " - " & kpi_e_last_ap.FULLNAME_VN, If(p.IS_CONFIRM.Value = 2, kpi_e_last_not_ap.EMPLOYEE_CODE & " - " & kpi_e_last_not_ap.FULLNAME_VN, Nothing))),
                               .EFFECT_DATE = p.EFFECT_DATE}


            If _filter.YEAR IsNot Nothing Then
                query = query.Where(Function(f) f.YEAR = _filter.YEAR)
            End If
            If _filter.PE_PERIOD_ID IsNot Nothing Then
                query = query.Where(Function(f) f.PE_PERIOD_ID = _filter.PE_PERIOD_ID)
            End If

            If _filter.STATUS_ID IsNot Nothing Then
                query = query.Where(Function(f) f.STATUS_ID = _filter.STATUS_ID)
            End If
            If _filter.IS_CONFIRM IsNot Nothing Then
                query = query.Where(Function(f) f.IS_CONFIRM = _filter.IS_CONFIRM)
            End If

            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                query = query.Where(Function(f) f.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_NAME) Then
                query = query.Where(Function(f) f.EMPLOYEE_NAME.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                query = query.Where(Function(f) f.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                query = query.Where(Function(f) f.TITLE_NAME.ToUpper.Contains(_filter.TITLE_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.PE_PERIOD_NAME) Then
                query = query.Where(Function(f) f.PE_PERIOD_NAME.ToUpper.Contains(_filter.PE_PERIOD_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.REASON) Then
                query = query.Where(Function(f) f.REASON.ToUpper.Contains(_filter.REASON.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.CLASSIFICATION) Then
                query = query.Where(Function(f) f.CLASSIFICATION.ToUpper.Contains(_filter.CLASSIFICATION.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.REMARK) Then
                query = query.Where(Function(f) f.REMARK.ToUpper.Contains(_filter.REMARK.ToUpper))
            End If

            If _filter.EFFECT_DATE.HasValue Then
                query = query.Where(Function(f) f.EFFECT_DATE = _filter.EFFECT_DATE)
            End If

            If _filter.START_DATE.HasValue Then
                query = query.Where(Function(f) f.START_DATE = _filter.START_DATE)
            End If

            If _filter.END_DATE.HasValue Then
                query = query.Where(Function(f) f.END_DATE = _filter.END_DATE)
            End If

            If _filter.NUMBER_MONTH.HasValue Then
                query = query.Where(Function(f) f.NUMBER_MONTH = _filter.NUMBER_MONTH)
            End If

            If Not String.IsNullOrEmpty(_filter.EVALUATION_POINTS) Then
                query = query.Where(Function(f) f.EVALUATION_POINTS.ToUpper.Contains(_filter.EVALUATION_POINTS.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.KPI_EMP_APPROVES_NAME) Then
                query = query.Where(Function(f) f.KPI_EMP_APPROVES_NAME.ToUpper.Contains(_filter.KPI_EMP_APPROVES_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.CONFIRM_NAME) Then
                query = query.Where(Function(f) f.CONFIRM_NAME.ToUpper.Contains(_filter.CONFIRM_NAME.ToUpper))
            End If

            query = query.OrderBy(Sorts)
            Total = query.Count
            query = query.Skip(PageIndex * PageSize).Take(PageSize)
            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try
    End Function
    Public Function Get_Portal_Target_Detail(ByVal _filter As PE_KPI_ASSESMENT_DETAIL_DTO,
                                     ByVal PageIndex As Integer,
                                     ByVal PageSize As Integer,
                                     ByRef Total As Integer,
                                     Optional ByVal Sorts As String = "CREATED_DATE desc",
                                     Optional ByVal log As UserLog = Nothing) As List(Of PE_KPI_ASSESMENT_DETAIL_DTO)

        Try

            Dim query = From k In Context.PE_KPI_ASSESSMENT_DETAIL
                        From p In Context.PE_KPI_ASSESSMENT.Where(Function(f) f.ID = k.GOAL_ID)
                        From employee In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE)
                        From title In Context.HU_TITLE.Where(Function(f) f.ID = employee.TITLE_ID).DefaultIfEmpty
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = employee.ORG_ID).DefaultIfEmpty
                        From period In Context.PE_PERIOD.Where(Function(f) f.ID = p.PE_PERIOD_ID).DefaultIfEmpty
                        From status In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.STATUS_ID).DefaultIfEmpty
                        From sc In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.IS_CONFIRM).DefaultIfEmpty
                        From o In Context.OT_OTHER_LIST.Where(Function(f) f.ID = k.SOURCE).DefaultIfEmpty
                        From o2 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = k.FREQUENCY).DefaultIfEmpty
                        From o3 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = k.GOAL_TYPE).DefaultIfEmpty
                        From o4 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = k.TARGET_TYPE).DefaultIfEmpty
                        From o5 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = k.DVT).DefaultIfEmpty
                        From process_approve In Context.PROCESS_APPROVED_STATUS.Where(Function(f) f.ID_REGGROUP = p.ID And f.PROCESS_TYPE = "PERFORMANCE")
                        From bf_process In Context.PROCESS_APPROVED_STATUS.Where(Function(f) f.ID_REGGROUP = p.ID And f.PROCESS_TYPE = "PERFORMANCE" AndAlso f.APP_LEVEL = process_approve.APP_LEVEL - 1).DefaultIfEmpty
                        Where process_approve.EMPLOYEE_APPROVED = _filter.EMPLOYEE AndAlso ((process_approve.APP_STATUS = 0 AndAlso (bf_process.APP_STATUS Is Nothing Or bf_process.APP_STATUS = 1)) _
                            OrElse (process_approve.APP_STATUS = 1 Or process_approve.APP_STATUS = 2))
                        Select New PE_KPI_ASSESMENT_DETAIL_DTO With {
                                .ID = k.ID,
                                .BENCHMARK = k.BENCHMARK,
                                .DESCRIPTION = k.DESCRIPTION,
                                .DIRECT_ACTUAL = k.DIRECT_ACTUAL,
                                .DIRECT_POINT = k.DIRECT_POINTS,
                                .EMPLOYEE_ACTUAL = k.EMPLOYEE_ACTUAL,
                                .EMPLOYEE_POINT = k.EMPLOYEE_POINTS,
                                .FREQUENCY_ID = k.FREQUENCY,
                                .FREQUENCY_NAME = o2.NAME_VN,
                                .GOAL_ID = k.GOAL_ID,
                                .GOAL_TYPE = k.GOAL_TYPE,
                                .GOAL_TYPE_NAME = o3.NAME_VN,
                                .GOAL_TYPE_CODE = o3.CODE,
                                .START_DATE = p.START_DATE,
                                .END_DATE = p.END_DATE,
                                .KPI_ASSESSMENT = k.KPI_ASSESSMENT,
                                .KPI_ASSESSMENT_TEXT = k.KPI_ASSESSMENT_TEXT,
                                .NOTE = k.NOTE,
                                .NOTE_QLTT = k.NOTE_QLTT,
                                .RATIO = k.RATIO,
                                .SOURCE_ID = k.SOURCE,
                                .SOURCE_NAME = o.NAME_VN,
                                .KPI_TYPE = If(Not k.KPI_ASSESSMENT.HasValue, "KPI mới", ""),
                                .TARGET = k.TARGET,
                                .TARGET_MIN = k.TARGET_MIN,
                                .TARGET_TYPE_ID = k.TARGET_TYPE,
                                .TARGET_TYPE_NAME = o4.NAME_VN,
                                .TARGET_TYPE_CODE = o4.CODE,
                                .UNIT_ID = k.DVT,
                                .UNIT_NAME = o5.NAME_VN,
                                .YEAR = p.YEAR,
                                .PE_PERIOD_ID = p.PE_PERIOD_ID,
                                .STATUS_ID = p.STATUS_ID,
                                .STATUS_NAME = status.NAME_VN,
                                .EMPLOYEE_CODE = employee.EMPLOYEE_CODE,
                                .EMPLOYEE_NAME = employee.FULLNAME_VN,
                                .TITLE_ID = employee.TITLE_ID,
                                .TITLE_NAME = title.NAME_VN,
                                .ORG_ID = employee.ORG_ID,
                                .IS_CONFIRM = p.IS_CONFIRM,
                                .CONFIRM_NAME = sc.NAME_VN,
                                .ORG_NAME = org.NAME_VN}

            If _filter.YEAR IsNot Nothing Then
                query = query.Where(Function(f) f.YEAR = _filter.YEAR)
            End If
            If _filter.PE_PERIOD_ID IsNot Nothing Then
                query = query.Where(Function(f) f.PE_PERIOD_ID = _filter.PE_PERIOD_ID)
            End If

            If _filter.STATUS_ID IsNot Nothing Then
                query = query.Where(Function(f) f.STATUS_ID = _filter.STATUS_ID)
            End If
            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                query = query.Where(Function(f) f.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.KPI_TYPE) Then
                query = query.Where(Function(f) f.KPI_TYPE.ToUpper.Contains(_filter.KPI_TYPE.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_NAME) Then
                query = query.Where(Function(f) f.EMPLOYEE_NAME.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                query = query.Where(Function(f) f.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                query = query.Where(Function(f) f.TITLE_NAME.ToUpper.Contains(_filter.TITLE_NAME.ToUpper))
            End If

            If _filter.IS_CONFIRM IsNot Nothing Then
                query = query.Where(Function(f) f.IS_CONFIRM = _filter.IS_CONFIRM)
            End If
            If Not String.IsNullOrEmpty(_filter.CONFIRM_NAME) Then
                query = query.Where(Function(f) f.CONFIRM_NAME.ToUpper.Contains(_filter.CONFIRM_NAME.ToUpper))
            End If
            query = query.OrderBy("ID DESC")
            Total = query.Count
            query = query.Skip(PageIndex * PageSize).Take(PageSize)
            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try
    End Function

    Public Function Get_Portal_Approve_Evaluate_Target(ByVal _filter As KPI_ASSESSMENT_DTO,
                                         ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer,
                                         Optional ByVal Sorts As String = "EFFECT_DATE desc",
                                         Optional ByVal log As UserLog = Nothing) As List(Of KPI_ASSESSMENT_DTO)

        Try

            Dim query1 = From p In Context.PE_KPI_ASSESSMENT
                         From employee In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE)
                         From title In Context.HU_TITLE.Where(Function(f) f.ID = employee.TITLE_ID).DefaultIfEmpty
                         From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = employee.ORG_ID).DefaultIfEmpty
                         From period In Context.PE_PERIOD.Where(Function(f) f.ID = p.PE_PERIOD_ID).DefaultIfEmpty
                         From process_approve In Context.PROCESS_APPROVED_STATUS.Where(Function(f) f.ID_REGGROUP = p.ID And f.PROCESS_TYPE = "PERFORMANCE")
                         From bf_process In Context.PROCESS_APPROVED_STATUS.Where(Function(f) f.ID_REGGROUP = p.ID And f.PROCESS_TYPE = "PERFORMANCE" AndAlso f.APP_LEVEL = process_approve.APP_LEVEL - 1).DefaultIfEmpty
                         From status In Context.OT_OTHER_LIST.Where(Function(f) f.ID = process_approve.APP_STATUS).DefaultIfEmpty
                         Where process_approve.EMPLOYEE_APPROVED = _filter.EMPLOYEE AndAlso ((process_approve.APP_STATUS = 0 AndAlso (bf_process.APP_STATUS Is Nothing Or bf_process.APP_STATUS = 1)) _
                             OrElse (process_approve.APP_STATUS = 1 Or process_approve.APP_STATUS = 2))
                         Select New KPI_ASSESSMENT_DTO With {
                                 .ID = p.ID,
                                 .EMPLOYEE = p.EMPLOYEE,
                                 .EMPLOYEE_CODE = employee.EMPLOYEE_CODE,
                                 .EMPLOYEE_NAME = employee.FULLNAME_VN,
                                 .TITLE_ID = employee.TITLE_ID,
                                 .TITLE_NAME = title.NAME_VN,
                                 .ORG_ID = employee.ORG_ID,
                                 .ORG_NAME = org.NAME_VN,
                                 .YEAR = p.YEAR,
                                 .PE_PERIOD_ID = p.PE_PERIOD_ID,
                                 .PE_PERIOD_NAME = period.NAME,
                                 .START_DATE = p.START_DATE,
                                 .END_DATE = p.END_DATE,
                                 .NUMBER_MONTH = p.NUMBER_MONTH,
                                 .GOAL = p.GOAL,
                                 .EVALUATION_POINTS = p.EVALUATION_POINTS,
                                 .CLASSIFICATION = p.CLASSIFICATION,
                                 .REMARK = p.REMARK,
                                 .REASON = p.REASON,
                                 .STATUS_ID = process_approve.APP_STATUS,
                                 .STATUS_NAME = status.NAME_VN,
                                 .PORTAL_ID = p.PORTAL_ID,
                                 .IS_FROM_PORTAL = If(p.PORTAL_ID = -1, True, False),
                                 .EFFECT_DATE = p.EFFECT_DATE}

            Dim query2 = From p In Context.PE_KPI_ASSESSMENT
                         From employee In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE)
                         From title In Context.HU_TITLE.Where(Function(f) f.ID = employee.TITLE_ID).DefaultIfEmpty
                         From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = employee.ORG_ID).DefaultIfEmpty
                         From period In Context.PE_PERIOD.Where(Function(f) f.ID = p.PE_PERIOD_ID).DefaultIfEmpty
                         From process_approve In Context.PROCESS_APPROVED_STATUS.Where(Function(f) f.ID_REGGROUP = p.ID And f.PROCESS_TYPE = "PERFORMANCE")
                         From bf_process In Context.PROCESS_APPROVED_STATUS.Where(Function(f) f.ID_REGGROUP = p.ID And f.PROCESS_TYPE = "PERFORMANCE" AndAlso f.APP_LEVEL = process_approve.APP_LEVEL - 1).DefaultIfEmpty
                         From status In Context.OT_OTHER_LIST.Where(Function(f) f.ID = process_approve.APP_STATUS).DefaultIfEmpty
                         From app In Context.HUV_PERFORMANCE_KPI.Where(Function(f) f.EMPLOYEE_ID = process_approve.EMPLOYEE_APPROVED And f.PROCESS_CODE = "PERFORMANCE" And f.SUB_EMPLOYEE_ID = _filter.EMPLOYEE)
                         Where ((app.REPLACEALL = -1 And 1 = 1) Or (app.REPLACEALL = 0 And process_approve.CREATED_DATE >= app.FROM_DATE And process_approve.CREATED_DATE <= app.TO_DATE)) AndAlso ((process_approve.APP_STATUS = 0 AndAlso (bf_process.APP_STATUS Is Nothing Or bf_process.APP_STATUS = 1)) _
                             OrElse (process_approve.APP_STATUS = 1 Or process_approve.APP_STATUS = 2))
                         Select New KPI_ASSESSMENT_DTO With {
                                 .ID = p.ID,
                                 .EMPLOYEE = p.EMPLOYEE,
                                 .EMPLOYEE_CODE = employee.EMPLOYEE_CODE,
                                 .EMPLOYEE_NAME = employee.FULLNAME_VN,
                                 .TITLE_ID = employee.TITLE_ID,
                                 .TITLE_NAME = title.NAME_VN,
                                 .ORG_ID = employee.ORG_ID,
                                 .ORG_NAME = org.NAME_VN,
                                 .YEAR = p.YEAR,
                                 .PE_PERIOD_ID = p.PE_PERIOD_ID,
                                 .PE_PERIOD_NAME = period.NAME,
                                 .START_DATE = p.START_DATE,
                                 .END_DATE = p.END_DATE,
                                 .NUMBER_MONTH = p.NUMBER_MONTH,
                                 .GOAL = p.GOAL,
                                 .EVALUATION_POINTS = p.EVALUATION_POINTS,
                                 .CLASSIFICATION = p.CLASSIFICATION,
                                 .REMARK = p.REMARK,
                                 .REASON = p.REASON,
                                 .STATUS_ID = process_approve.APP_STATUS,
                                 .STATUS_NAME = status.NAME_VN,
                                 .PORTAL_ID = p.PORTAL_ID,
                                 .IS_FROM_PORTAL = If(p.PORTAL_ID = -1, True, False),
                                 .EFFECT_DATE = p.EFFECT_DATE}

            Dim query = query1.Union(query2)

            If _filter.YEAR IsNot Nothing Then
                query = query.Where(Function(f) f.YEAR = _filter.YEAR)
            End If
            If _filter.PE_PERIOD_ID IsNot Nothing Then
                query = query.Where(Function(f) f.PE_PERIOD_ID = _filter.PE_PERIOD_ID)
            End If

            If _filter.STATUS_ID IsNot Nothing Then
                query = query.Where(Function(f) f.STATUS_ID = _filter.STATUS_ID)
            End If

            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                query = query.Where(Function(f) f.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_NAME) Then
                query = query.Where(Function(f) f.EMPLOYEE_NAME.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                query = query.Where(Function(f) f.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                query = query.Where(Function(f) f.TITLE_NAME.ToUpper.Contains(_filter.TITLE_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.PE_PERIOD_NAME) Then
                query = query.Where(Function(f) f.PE_PERIOD_NAME.ToUpper.Contains(_filter.PE_PERIOD_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.REASON) Then
                query = query.Where(Function(f) f.REASON.ToUpper.Contains(_filter.REASON.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.CLASSIFICATION) Then
                query = query.Where(Function(f) f.CLASSIFICATION.ToUpper.Contains(_filter.CLASSIFICATION.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.REMARK) Then
                query = query.Where(Function(f) f.REMARK.ToUpper.Contains(_filter.REMARK.ToUpper))
            End If

            If _filter.EFFECT_DATE.HasValue Then
                query = query.Where(Function(f) f.EFFECT_DATE = _filter.EFFECT_DATE)
            End If

            If _filter.START_DATE.HasValue Then
                query = query.Where(Function(f) f.START_DATE = _filter.START_DATE)
            End If

            If _filter.END_DATE.HasValue Then
                query = query.Where(Function(f) f.END_DATE = _filter.END_DATE)
            End If

            If _filter.NUMBER_MONTH.HasValue Then
                query = query.Where(Function(f) f.NUMBER_MONTH = _filter.NUMBER_MONTH)
            End If

            If Not String.IsNullOrEmpty(_filter.EVALUATION_POINTS) Then
                query = query.Where(Function(f) f.EVALUATION_POINTS.ToUpper.Contains(_filter.EVALUATION_POINTS.ToUpper))
            End If

            query = query.OrderBy(Sorts)
            Total = query.Count
            query = query.Skip(PageIndex * PageSize).Take(PageSize)
            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try
    End Function

    Public Function GetPortalApprovedKPIAssessment(ByVal _filter As KPI_ASSESSMENT_DTO,
                                                    ByVal PageIndex As Integer,
                                                    ByVal PageSize As Integer,
                                                    ByRef Total As Integer,
                                                    Optional ByVal Sorts As String = "EFFECT_DATE desc",
                                                    Optional ByVal log As UserLog = Nothing) As List(Of KPI_ASSESSMENT_DTO)

        Try

            Dim query1 = From p In Context.PE_KPI_ASSESSMENT
                         From employee In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE)
                         From title In Context.HU_TITLE.Where(Function(f) f.ID = employee.TITLE_ID).DefaultIfEmpty
                         From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = employee.ORG_ID).DefaultIfEmpty
                         From period In Context.PE_PERIOD.Where(Function(f) f.ID = p.PE_PERIOD_ID).DefaultIfEmpty
                         From process_approve In Context.PROCESS_APPROVED_STATUS.Where(Function(f) f.ID_REGGROUP = p.ID And f.PROCESS_TYPE = "PERFORMANCE_KPI")
                         From bf_process In Context.PROCESS_APPROVED_STATUS.Where(Function(f) f.ID_REGGROUP = p.ID And f.PROCESS_TYPE = "PERFORMANCE_KPI" AndAlso f.APP_LEVEL = process_approve.APP_LEVEL - 1).DefaultIfEmpty
                         From sc In Context.OT_OTHER_LIST.Where(Function(f) f.ID = process_approve.APP_STATUS).DefaultIfEmpty
                         Where process_approve.EMPLOYEE_APPROVED = _filter.EMPLOYEE AndAlso ((process_approve.APP_STATUS = 0 AndAlso (bf_process.APP_STATUS Is Nothing Or bf_process.APP_STATUS = 1)) _
                             OrElse (process_approve.APP_STATUS = 1 Or process_approve.APP_STATUS = 2))
                         Select New KPI_ASSESSMENT_DTO With {
                                 .ID = p.ID,
                                 .EMPLOYEE = p.EMPLOYEE,
                                 .EMPLOYEE_CODE = employee.EMPLOYEE_CODE,
                                 .EMPLOYEE_NAME = employee.FULLNAME_VN,
                                 .TITLE_ID = employee.TITLE_ID,
                                 .TITLE_NAME = title.NAME_VN,
                                 .ORG_ID = employee.ORG_ID,
                                 .ORG_NAME = org.NAME_VN,
                                 .YEAR = p.YEAR,
                                 .PE_PERIOD_ID = p.PE_PERIOD_ID,
                                 .PE_PERIOD_NAME = period.NAME,
                                 .START_DATE = p.START_DATE,
                                 .END_DATE = p.END_DATE,
                                 .NUMBER_MONTH = p.NUMBER_MONTH,
                                 .GOAL = p.GOAL,
                                 .EVALUATION_POINTS = p.EVALUATION_POINTS,
                                 .CLASSIFICATION = p.CLASSIFICATION,
                                 .REMARK = p.REMARK,
                                 .REASON = p.REASON,
                                 .IS_CONFIRM = process_approve.APP_STATUS,
                                 .CONFIRM_NAME = sc.NAME_VN,
                                 .PORTAL_ID = p.PORTAL_ID,
                                 .IS_FROM_PORTAL = If(p.PORTAL_ID = -1, True, False),
                                 .EFFECT_DATE = p.EFFECT_DATE}

            Dim query2 = From p In Context.PE_KPI_ASSESSMENT
                         From employee In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE)
                         From title In Context.HU_TITLE.Where(Function(f) f.ID = employee.TITLE_ID).DefaultIfEmpty
                         From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = employee.ORG_ID).DefaultIfEmpty
                         From period In Context.PE_PERIOD.Where(Function(f) f.ID = p.PE_PERIOD_ID).DefaultIfEmpty
                         From process_approve In Context.PROCESS_APPROVED_STATUS.Where(Function(f) f.ID_REGGROUP = p.ID And f.PROCESS_TYPE = "PERFORMANCE_KPI")
                         From bf_process In Context.PROCESS_APPROVED_STATUS.Where(Function(f) f.ID_REGGROUP = p.ID And f.PROCESS_TYPE = "PERFORMANCE_KPI" AndAlso f.APP_LEVEL = process_approve.APP_LEVEL - 1).DefaultIfEmpty
                         From sc In Context.OT_OTHER_LIST.Where(Function(f) f.ID = process_approve.APP_STATUS).DefaultIfEmpty
                         From app In Context.HUV_PERFORMANCE_KPI.Where(Function(f) f.EMPLOYEE_ID = process_approve.EMPLOYEE_APPROVED And f.PROCESS_CODE = "PERFORMANCE_KPI" And f.SUB_EMPLOYEE_ID = _filter.EMPLOYEE)
                         Where ((app.REPLACEALL = -1 And 1 = 1) Or (app.REPLACEALL = 0 And process_approve.CREATED_DATE >= app.FROM_DATE And process_approve.CREATED_DATE <= app.TO_DATE)) AndAlso ((process_approve.APP_STATUS = 0 AndAlso (bf_process.APP_STATUS Is Nothing Or bf_process.APP_STATUS = 1)) _
                             OrElse (process_approve.APP_STATUS = 1 Or process_approve.APP_STATUS = 2))
                         Select New KPI_ASSESSMENT_DTO With {
                                 .ID = p.ID,
                                 .EMPLOYEE = p.EMPLOYEE,
                                 .EMPLOYEE_CODE = employee.EMPLOYEE_CODE,
                                 .EMPLOYEE_NAME = employee.FULLNAME_VN,
                                 .TITLE_ID = employee.TITLE_ID,
                                 .TITLE_NAME = title.NAME_VN,
                                 .ORG_ID = employee.ORG_ID,
                                 .ORG_NAME = org.NAME_VN,
                                 .YEAR = p.YEAR,
                                 .PE_PERIOD_ID = p.PE_PERIOD_ID,
                                 .PE_PERIOD_NAME = period.NAME,
                                 .START_DATE = p.START_DATE,
                                 .END_DATE = p.END_DATE,
                                 .NUMBER_MONTH = p.NUMBER_MONTH,
                                 .GOAL = p.GOAL,
                                 .EVALUATION_POINTS = p.EVALUATION_POINTS,
                                 .CLASSIFICATION = p.CLASSIFICATION,
                                 .REMARK = p.REMARK,
                                 .REASON = p.REASON,
                                 .IS_CONFIRM = process_approve.APP_STATUS,
                                 .CONFIRM_NAME = sc.NAME_VN,
                                 .PORTAL_ID = p.PORTAL_ID,
                                 .IS_FROM_PORTAL = If(p.PORTAL_ID = -1, True, False),
                                 .EFFECT_DATE = p.EFFECT_DATE}


            Dim query = query1.Union(query2)

            If _filter.YEAR IsNot Nothing Then
                query = query.Where(Function(f) f.YEAR = _filter.YEAR)
            End If
            If _filter.PE_PERIOD_ID IsNot Nothing Then
                query = query.Where(Function(f) f.PE_PERIOD_ID = _filter.PE_PERIOD_ID)
            End If

            If _filter.IS_CONFIRM IsNot Nothing Then
                query = query.Where(Function(f) f.IS_CONFIRM = _filter.IS_CONFIRM)
            End If

            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                query = query.Where(Function(f) f.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_NAME) Then
                query = query.Where(Function(f) f.EMPLOYEE_NAME.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                query = query.Where(Function(f) f.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                query = query.Where(Function(f) f.TITLE_NAME.ToUpper.Contains(_filter.TITLE_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.PE_PERIOD_NAME) Then
                query = query.Where(Function(f) f.PE_PERIOD_NAME.ToUpper.Contains(_filter.PE_PERIOD_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.REASON) Then
                query = query.Where(Function(f) f.REASON.ToUpper.Contains(_filter.REASON.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.CLASSIFICATION) Then
                query = query.Where(Function(f) f.CLASSIFICATION.ToUpper.Contains(_filter.CLASSIFICATION.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.REMARK) Then
                query = query.Where(Function(f) f.REMARK.ToUpper.Contains(_filter.REMARK.ToUpper))
            End If

            If _filter.EFFECT_DATE.HasValue Then
                query = query.Where(Function(f) f.EFFECT_DATE = _filter.EFFECT_DATE)
            End If

            If _filter.START_DATE.HasValue Then
                query = query.Where(Function(f) f.START_DATE = _filter.START_DATE)
            End If

            If _filter.END_DATE.HasValue Then
                query = query.Where(Function(f) f.END_DATE = _filter.END_DATE)
            End If

            If _filter.NUMBER_MONTH.HasValue Then
                query = query.Where(Function(f) f.NUMBER_MONTH = _filter.NUMBER_MONTH)
            End If

            If Not String.IsNullOrEmpty(_filter.EVALUATION_POINTS) Then
                query = query.Where(Function(f) f.EVALUATION_POINTS.ToUpper.Contains(_filter.EVALUATION_POINTS.ToUpper))
            End If

            query = query.OrderBy(Sorts)
            Total = query.Count
            query = query.Skip(PageIndex * PageSize).Take(PageSize)
            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try
    End Function

    Public Function Get_Portal_KPI_Detail(ByVal _filter As PE_KPI_ASSESMENT_DETAIL_DTO,
                                          ByVal PageIndex As Integer,
                                          ByVal PageSize As Integer,
                                          ByRef Total As Integer,
                                          Optional ByVal Sorts As String = "EMPLOYEE_CODE desc",
                                          Optional ByVal log As UserLog = Nothing) As List(Of PE_KPI_ASSESMENT_DETAIL_DTO)

        Try

            Dim query = From k In Context.PE_KPI_ASSESSMENT_DETAIL
                        From p In Context.PE_KPI_ASSESSMENT.Where(Function(f) f.ID = k.GOAL_ID)
                        From employee In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE)
                        From title In Context.HU_TITLE.Where(Function(f) f.ID = employee.TITLE_ID).DefaultIfEmpty
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = employee.ORG_ID).DefaultIfEmpty
                        From period In Context.PE_PERIOD.Where(Function(f) f.ID = p.PE_PERIOD_ID).DefaultIfEmpty
                        From status In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.STATUS_ID).DefaultIfEmpty
                        From sc In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.IS_CONFIRM).DefaultIfEmpty
                        From o In Context.OT_OTHER_LIST.Where(Function(f) f.ID = k.SOURCE).DefaultIfEmpty
                        From o2 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = k.FREQUENCY).DefaultIfEmpty
                        From o3 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = k.GOAL_TYPE).DefaultIfEmpty
                        From o4 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = k.TARGET_TYPE).DefaultIfEmpty
                        From o5 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = k.DVT).DefaultIfEmpty
                        From process_approve In Context.PROCESS_APPROVED_STATUS.Where(Function(f) f.ID_REGGROUP = p.ID And f.PROCESS_TYPE = "PERFORMANCE_KPI")
                        From bf_process In Context.PROCESS_APPROVED_STATUS.Where(Function(f) f.ID_REGGROUP = p.ID And f.PROCESS_TYPE = "PERFORMANCE_KPI" AndAlso f.APP_LEVEL = process_approve.APP_LEVEL - 1).DefaultIfEmpty
                        Where process_approve.EMPLOYEE_APPROVED = _filter.EMPLOYEE AndAlso ((process_approve.APP_STATUS = 0 AndAlso (bf_process.APP_STATUS Is Nothing Or bf_process.APP_STATUS = 1)) _
                            OrElse (process_approve.APP_STATUS = 1 Or process_approve.APP_STATUS = 2))
                        Select New PE_KPI_ASSESMENT_DETAIL_DTO With {
                                .ID = k.ID,
                                .BENCHMARK = k.BENCHMARK,
                                .DESCRIPTION = k.DESCRIPTION,
                                .DIRECT_ACTUAL = k.DIRECT_ACTUAL,
                                .DIRECT_POINT = k.DIRECT_POINTS,
                                .EMPLOYEE_ACTUAL = k.EMPLOYEE_ACTUAL,
                                .EMPLOYEE_POINT = k.EMPLOYEE_POINTS,
                                .FREQUENCY_ID = k.FREQUENCY,
                                .FREQUENCY_NAME = o2.NAME_VN,
                                .GOAL_ID = k.GOAL_ID,
                                .GOAL_TYPE = k.GOAL_TYPE,
                                .GOAL_TYPE_NAME = o3.NAME_VN,
                                .KPI_ASSESSMENT = k.KPI_ASSESSMENT,
                                .KPI_ASSESSMENT_TEXT = k.KPI_ASSESSMENT_TEXT,
                                .KPI_TYPE = If(Not k.KPI_ASSESSMENT.HasValue, "KPI mới", ""),
                                .NOTE = k.NOTE,
                                .NOTE_QLTT = k.NOTE_QLTT,
                                .RATIO = k.RATIO,
                                .SOURCE_ID = k.SOURCE,
                                .SOURCE_NAME = o.NAME_VN,
                                .TARGET = k.TARGET,
                                .TARGET_MIN = k.TARGET_MIN,
                                .TARGET_TYPE_ID = k.TARGET_TYPE,
                                .TARGET_TYPE_NAME = o4.NAME_VN,
                                .UNIT_ID = k.DVT,
                                .UNIT_NAME = o5.NAME_VN,
                                .YEAR = p.YEAR,
                                .PE_PERIOD_ID = p.PE_PERIOD_ID,
                                .STATUS_ID = p.STATUS_ID,
                                .STATUS_NAME = status.NAME_VN,
                                .EMPLOYEE_CODE = employee.EMPLOYEE_CODE,
                                .EMPLOYEE_NAME = employee.FULLNAME_VN,
                                .TITLE_ID = employee.TITLE_ID,
                                .TITLE_NAME = title.NAME_VN,
                                .ORG_ID = employee.ORG_ID,
                                .IS_CONFIRM = p.IS_CONFIRM,
                                .CONFIRM_NAME = sc.NAME_VN,
                                .ORG_NAME = org.NAME_VN}

            If _filter.YEAR IsNot Nothing Then
                query = query.Where(Function(f) f.YEAR = _filter.YEAR)
            End If
            If _filter.PE_PERIOD_ID IsNot Nothing Then
                query = query.Where(Function(f) f.PE_PERIOD_ID = _filter.PE_PERIOD_ID)
            End If

            If _filter.STATUS_ID IsNot Nothing Then
                query = query.Where(Function(f) f.STATUS_ID = _filter.STATUS_ID)
            End If
            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                query = query.Where(Function(f) f.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.KPI_TYPE) Then
                query = query.Where(Function(f) f.KPI_TYPE.ToUpper.Contains(_filter.KPI_TYPE.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_NAME) Then
                query = query.Where(Function(f) f.EMPLOYEE_NAME.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                query = query.Where(Function(f) f.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                query = query.Where(Function(f) f.TITLE_NAME.ToUpper.Contains(_filter.TITLE_NAME.ToUpper))
            End If

            If _filter.IS_CONFIRM IsNot Nothing Then
                query = query.Where(Function(f) f.IS_CONFIRM = _filter.IS_CONFIRM)
            End If
            If Not String.IsNullOrEmpty(_filter.CONFIRM_NAME) Then
                query = query.Where(Function(f) f.CONFIRM_NAME.ToUpper.Contains(_filter.CONFIRM_NAME.ToUpper))
            End If
            query = query.OrderBy("ID DESC")
            Total = query.Count
            query = query.Skip(PageIndex * PageSize).Take(PageSize)
            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try
    End Function

    Public Function PRI_PROCESS(ByVal employee_id_app As Decimal, ByVal employee_id As Decimal, ByVal period_id As Integer, ByVal status As Decimal, ByVal process_type As String, ByVal notes As String, ByVal id_reggroup As Integer, Optional ByVal log As UserLog = Nothing) As Int32
        Using cls As New DataAccess.QueryData
            Dim obj = New With {.p_employee_app_id = employee_id_app, .P_EMPLOYEE_ID = employee_id, .P_PERIOD_ID = period_id, .P_STATUS = status, .P_PROCESS_TYPE = process_type, .P_NOTE = notes, .P_ID_REGGROUP = id_reggroup, .P_RESULT = cls.OUT_NUMBER}
            Dim store = cls.ExecuteStore("PKG_AT_PROCESS.PRI_PROCESS", obj)
            Return Int32.Parse(obj.P_RESULT)
        End Using
    End Function

    Public Function PRI_PROCESS_APP(ByVal employee_id As Decimal, ByVal period_id As Integer, ByVal process_type As String, ByVal totalHours As Decimal, ByVal totalDay As Decimal, ByVal sign_id As Integer, ByVal id_reggroup As Integer, ByVal token As String) As Int32
        Using cls As New DataAccess.QueryData
            Dim obj = New With {.P_EMPLOYEE_ID = employee_id, .P_PERIOD_ID = period_id, .P_PROCESS_TYPE = process_type, .P_TOTAL_HOURS = totalHours, .P_TOTAL_DAY = totalDay, .P_SIGN_ID = sign_id, .P_ID_REGGROUP = id_reggroup, .P_TOKEN = token, .P_RESULT = cls.OUT_NUMBER}
            Dim store = cls.ExecuteStore("PKG_AT_PROCESS.PRI_PROCESS_APP", obj)
            Return Int32.Parse(obj.P_RESULT)
        End Using
    End Function

    Public Function IMPORT_KPI_ASSESSMENT(ByVal DocXML As String, ByVal log As UserLog) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PERFORMANCE_BUSINESS.IMPORT_KPI_ASSESSMENT",
                                               New With {.P_DOCXML = DocXML,
                                                         .P_USER = log.Username.ToUpper,
                                                         .P_OUT = cls.OUT_CURSOR}, True)
                Return CBool(dtData(0)(0))
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try

    End Function

    Public Function ValidateSubmit(ByVal _kpi_ID As Decimal, ByVal _type As String) As Boolean
        Try
            Dim query As Boolean = True
            If _type = "SEND_APP" Then
                query = (From p In Context.PE_KPI_ASSESSMENT_DETAIL Where p.GOAL_ID = _kpi_ID And (p.EMPLOYEE_ACTUAL Is Nothing Or p.EMPLOYEE_POINTS Is Nothing)).Any
            ElseIf _type = "APPROVE" Then
                query = (From p In Context.PE_KPI_ASSESSMENT_DETAIL Where p.GOAL_ID = _kpi_ID And (p.DIRECT_ACTUAL Is Nothing Or p.DIRECT_POINTS Is Nothing)).Any
            End If
            Return query
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try

    End Function

#End Region

#Region "HTCH Assessment"
    Public Function GetHTCHAssessment(ByVal _filter As PE_HTCH_ASSESSMENT_DTO, ByVal lstOrg As List(Of Decimal),
                                         ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer,
                                         Optional ByVal Sorts As String = "CREATED_DATE desc",
                                         Optional ByVal log As UserLog = Nothing) As List(Of PE_HTCH_ASSESSMENT_DTO)

        Try
            Dim strOrg = String.Join(",", lstOrg)

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG2",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = strOrg})
            End Using

            Dim query = From p In Context.PE_HTCH_ASSESSMENT
                        From employee In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From title In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From period In Context.PE_PERIOD_HTCH.Where(Function(f) f.ID = p.PERIOD_ID).DefaultIfEmpty
                        From status In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.STATUS_ID).DefaultIfEmpty
                        From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = p.ORG_ID And f.USERNAME = log.Username.ToUpper)
                        Select New PE_HTCH_ASSESSMENT_DTO With {
                               .ID = p.ID,
                               .EMPLOYEE_ID = p.EMPLOYEE_ID,
                               .EMPLOYEE_CODE = employee.EMPLOYEE_CODE,
                               .EMPLOYEE_NAME = employee.FULLNAME_VN,
                               .TITLE_ID = employee.TITLE_ID,
                               .TITLE_NAME = title.NAME_VN,
                               .TITLE_GROUP = p.TITLEGROUP_ID,
                               .ORG_ID = employee.ORG_ID,
                               .ORG_NAME = org.NAME_VN,
                               .YEAR = p.YEAR,
                               .PERIOD_ID = p.PERIOD_ID,
                               .PERIOD_NAME = period.NAME,
                               .START_DATE = p.START_DATE,
                               .END_DATE = p.END_DATE,
                               .EVALUATION_POINTS = p.EVALUATION_POINTS,
                               .CLASSIFICATION = p.CLASSIFICATION,
                               .STRENGTH_NOTE = p.STRENGTH_NOTE,
                               .IMPROVE_NOTE = p.IMPROVE_NOT,
                               .PROSPECT_NOTE = p.PROSPECT_NOTE,
                               .BRANCH_EVALUATE = p.BRANCH_EVALUATE,
                               .REMARK = p.REMARK,
                               .REASON = p.REASON,
                               .STATUS_ID = p.STATUS_ID,
                               .STATUS_NAME = status.NAME_VN,
                               .CREATED_DATE = p.CREATED_DATE}


            If _filter.YEAR IsNot Nothing Then
                query = query.Where(Function(f) f.YEAR = _filter.YEAR)
            End If
            If _filter.PERIOD_ID IsNot Nothing Then
                query = query.Where(Function(f) f.PERIOD_ID = _filter.PERIOD_ID)
            End If

            If _filter.STATUS_ID IsNot Nothing Then
                query = query.Where(Function(f) f.STATUS_ID = _filter.STATUS_ID)
            End If

            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                query = query.Where(Function(f) f.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_NAME) Then
                query = query.Where(Function(f) f.EMPLOYEE_NAME.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                query = query.Where(Function(f) f.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                query = query.Where(Function(f) f.TITLE_NAME.ToUpper.Contains(_filter.TITLE_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.PERIOD_NAME) Then
                query = query.Where(Function(f) f.PERIOD_NAME.ToUpper.Contains(_filter.PERIOD_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.REASON) Then
                query = query.Where(Function(f) f.REASON.ToUpper.Contains(_filter.REASON.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.CLASSIFICATION) Then
                query = query.Where(Function(f) f.CLASSIFICATION.ToUpper.Contains(_filter.CLASSIFICATION.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.REMARK) Then
                query = query.Where(Function(f) f.REMARK.ToUpper.Contains(_filter.REMARK.ToUpper))
            End If

            If _filter.START_DATE.HasValue Then
                query = query.Where(Function(f) f.START_DATE = _filter.START_DATE)
            End If

            If _filter.END_DATE.HasValue Then
                query = query.Where(Function(f) f.END_DATE = _filter.END_DATE)
            End If

            If Not String.IsNullOrEmpty(_filter.EVALUATION_POINTS) Then
                query = query.Where(Function(f) f.EVALUATION_POINTS.ToUpper.Contains(_filter.EVALUATION_POINTS.ToUpper))
            End If

            If Not String.IsNullOrEmpty(_filter.BRANCH_EVALUATE) Then
                query = query.Where(Function(f) f.BRANCH_EVALUATE.ToUpper.Contains(_filter.BRANCH_EVALUATE.ToUpper))
            End If

            If Not String.IsNullOrEmpty(_filter.STRENGTH_NOTE) Then
                query = query.Where(Function(f) f.STRENGTH_NOTE.ToUpper.Contains(_filter.STRENGTH_NOTE.ToUpper))
            End If

            If Not String.IsNullOrEmpty(_filter.IMPROVE_NOTE) Then
                query = query.Where(Function(f) f.IMPROVE_NOTE.ToUpper.Contains(_filter.IMPROVE_NOTE.ToUpper))
            End If

            If Not String.IsNullOrEmpty(_filter.PROSPECT_NOTE) Then
                query = query.Where(Function(f) f.PROSPECT_NOTE.ToUpper.Contains(_filter.PROSPECT_NOTE.ToUpper))
            End If

            If _filter.STATUS_ID.HasValue Then
                query = query.Where(Function(f) f.STATUS_ID = _filter.STATUS_ID)
            End If

            query = query.OrderBy(Sorts)
            Total = query.Count
            query = query.Skip(PageIndex * PageSize).Take(PageSize)
            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try
    End Function
    Public Function GetHTCHAssessment_Detail(ByVal _filter As PE_HTCH_ASSESSMENT_DTL_DTO, ByVal lstOrg As List(Of Decimal),
                                         ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer,
                                         Optional ByVal Sorts As String = "CREATED_DATE desc",
                                         Optional ByVal log As UserLog = Nothing) As List(Of PE_HTCH_ASSESSMENT_DTL_DTO)

        Try
            Dim strOrg = String.Join(",", lstOrg)

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG2",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = strOrg})
            End Using

            Dim query = From t In Context.PE_HTCH_ASSESSMENT_DETAIL
                        From p In Context.PE_HTCH_ASSESSMENT.Where(Function(f) f.ID = t.HTCH_ASSESSMENT_ID)
                        From employee In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From title In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From period In Context.PE_PERIOD_HTCH.Where(Function(f) f.ID = p.PERIOD_ID).DefaultIfEmpty
                        From status In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.STATUS_ID).DefaultIfEmpty
                        From criterial In Context.PE_CRITERIA_HTCH.Where(Function(f) f.ID = t.CRITERIA_ID).DefaultIfEmpty
                        From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = p.ORG_ID And f.USERNAME = log.Username.ToUpper)
                        Select New PE_HTCH_ASSESSMENT_DTL_DTO With {
                                .ID = p.ID,
                                .EMPLOYEE_ID = p.EMPLOYEE_ID,
                                .EMPLOYEE_CODE = employee.EMPLOYEE_CODE,
                                .EMPLOYEE_NAME = employee.FULLNAME_VN,
                                .TITLE_ID = employee.TITLE_ID,
                                .TITLE_NAME = title.NAME_VN,
                                .TITLE_GROUP = p.TITLEGROUP_ID,
                                .ORG_ID = employee.ORG_ID,
                                .ORG_NAME = org.NAME_VN,
                                .YEAR = p.YEAR,
                                .PERIOD_ID = p.PERIOD_ID,
                                .PERIOD_NAME = period.NAME,
                                .STATUS_ID = p.STATUS_ID,
                                .STATUS_NAME = status.NAME_VN,
                                .CREATED_DATE = p.CREATED_DATE,
                                .CRITERIA_NAME = criterial.NAME,
                                .RATIO = t.RATIO,
                                .POINTS_ACTUAL = t.POINTS_ACTUAL,
                                .RESULT_ACTUAL = t.RESULT_ACTUAL,
                                .POINTS_FINAL = t.POINTS_FINAL,
                                .NOTE = t.NOTE}


            If _filter.YEAR IsNot Nothing Then
                query = query.Where(Function(f) f.YEAR = _filter.YEAR)
            End If
            If _filter.PERIOD_ID IsNot Nothing Then
                query = query.Where(Function(f) f.PERIOD_ID = _filter.PERIOD_ID)
            End If
            If _filter.STATUS_ID IsNot Nothing Then
                query = query.Where(Function(f) f.STATUS_ID = _filter.STATUS_ID)
            End If
            If _filter.RATIO IsNot Nothing Then
                query = query.Where(Function(f) f.RATIO = _filter.RATIO)
            End If
            If _filter.POINTS_ACTUAL IsNot Nothing Then
                query = query.Where(Function(f) f.POINTS_ACTUAL = _filter.POINTS_ACTUAL)
            End If
            If _filter.RESULT_ACTUAL IsNot Nothing Then
                query = query.Where(Function(f) f.RESULT_ACTUAL = _filter.RESULT_ACTUAL)
            End If
            If _filter.POINTS_FINAL IsNot Nothing Then
                query = query.Where(Function(f) f.POINTS_FINAL = _filter.POINTS_FINAL)
            End If

            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                query = query.Where(Function(f) f.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_NAME) Then
                query = query.Where(Function(f) f.EMPLOYEE_NAME.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                query = query.Where(Function(f) f.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                query = query.Where(Function(f) f.TITLE_NAME.ToUpper.Contains(_filter.TITLE_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.PERIOD_NAME) Then
                query = query.Where(Function(f) f.PERIOD_NAME.ToUpper.Contains(_filter.PERIOD_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.CRITERIA_NAME) Then
                query = query.Where(Function(f) f.CRITERIA_NAME.ToUpper.Contains(_filter.CRITERIA_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.STATUS_NAME) Then
                query = query.Where(Function(f) f.STATUS_NAME.ToUpper.Contains(_filter.STATUS_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.NOTE) Then
                query = query.Where(Function(f) f.NOTE.ToUpper.Contains(_filter.NOTE.ToUpper))
            End If
            query = query.OrderBy(Sorts)
            Total = query.Count
            query = query.Skip(PageIndex * PageSize).Take(PageSize)
            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try
    End Function
    Public Function GetHTCHAssessmentByID(ByVal _id As Decimal) As PE_HTCH_ASSESSMENT_DTO

        Try

            Dim query = (From p In Context.PE_HTCH_ASSESSMENT
                         From employee In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                         From title In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
                         From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                         From period In Context.PE_PERIOD_HTCH.Where(Function(f) f.ID = p.PERIOD_ID).DefaultIfEmpty
                         Where p.ID = _id
                         Select New PE_HTCH_ASSESSMENT_DTO With {
                                .ID = p.ID,
                                .EMPLOYEE_ID = p.EMPLOYEE_ID,
                                .EMPLOYEE_CODE = employee.EMPLOYEE_CODE,
                                .EMPLOYEE_NAME = employee.FULLNAME_VN,
                                .TITLE_ID = employee.TITLE_ID,
                                .TITLE_NAME = title.NAME_VN,
                                .TITLE_GROUP = p.TITLEGROUP_ID,
                                .ORG_ID = employee.ORG_ID,
                                .ORG_NAME = org.NAME_VN,
                                .YEAR = p.YEAR,
                                .PERIOD_ID = p.PERIOD_ID,
                                .PERIOD_NAME = period.NAME,
                                .START_DATE = p.START_DATE,
                                .END_DATE = p.END_DATE,
                                .EVALUATION_POINTS = p.EVALUATION_POINTS,
                                .CLASSIFICATION = p.CLASSIFICATION,
                                .STRENGTH_NOTE = p.STRENGTH_NOTE,
                                .IMPROVE_NOTE = p.IMPROVE_NOT,
                                .PROSPECT_NOTE = p.PROSPECT_NOTE,
                                .BRANCH_EVALUATE = p.BRANCH_EVALUATE,
                                .REMARK = p.REMARK,
                                .REASON = p.REASON,
                                .STATUS_ID = p.STATUS_ID,
                                .CREATED_DATE = p.CREATED_DATE}).FirstOrDefault

            query.lstDetail = (From p In Context.PE_HTCH_ASSESSMENT_DETAIL
                               From c In Context.PE_CRITERIA_HTCH.Where(Function(f) f.ID = p.CRITERIA_ID).DefaultIfEmpty
                               Where p.HTCH_ASSESSMENT_ID = query.ID
                               Select New PE_HTCH_ASSESSMENT_DTL_DTO With {
                                   .ID = p.ID,
                                   .CRITERIA_ID = p.CRITERIA_ID,
                                   .CRITERIA_NAME = c.NAME,
                                   .IS_CHECK = c.IS_CHECK,
                                   .RATIO = p.RATIO,
                                   .POINTS_ACTUAL = p.POINTS_ACTUAL,
                                   .RESULT_ACTUAL = p.RESULT_ACTUAL,
                                   .POINTS_FINAL = p.POINTS_FINAL,
                                   .NOTE = p.NOTE}).ToList
            Return query
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try
    End Function

    Public Function GetHTCHAssessmentListDetail(ByVal _id As Decimal) As List(Of PE_HTCH_ASSESSMENT_DTL_DTO)

        Try
            Dim lstDetail As New List(Of PE_HTCH_ASSESSMENT_DTL_DTO)
            lstDetail = (From p In Context.PE_HTCH_ASSESSMENT_DETAIL
                         From c In Context.PE_CRITERIA_HTCH.Where(Function(f) f.ID = p.CRITERIA_ID).DefaultIfEmpty
                         Where p.HTCH_ASSESSMENT_ID = _id
                         Select New PE_HTCH_ASSESSMENT_DTL_DTO With {
                                   .ID = p.ID,
                                   .CRITERIA_ID = p.CRITERIA_ID,
                                   .CRITERIA_NAME = c.NAME,
                                   .IS_CHECK = c.IS_CHECK,
                                   .RATIO = p.RATIO,
                                   .POINTS_ACTUAL = p.POINTS_ACTUAL,
                                   .RESULT_ACTUAL = p.RESULT_ACTUAL,
                                   .POINTS_FINAL = p.POINTS_FINAL,
                                   .NOTE = p.NOTE}).ToList
            Return lstDetail
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try
    End Function

    Public Function SaveHTCHAssessment(ByVal objData As PE_HTCH_ASSESSMENT_DTO, ByVal _log As UserLog) As Boolean
        Try
            Dim obj = (From p In Context.PE_HTCH_ASSESSMENT Where p.ID = objData.ID).FirstOrDefault
            If obj Is Nothing Then
                Return False
            End If

            obj.STRENGTH_NOTE = objData.STRENGTH_NOTE
            obj.IMPROVE_NOT = objData.IMPROVE_NOTE
            obj.PROSPECT_NOTE = objData.PROSPECT_NOTE
            obj.BRANCH_EVALUATE = objData.BRANCH_EVALUATE
            obj.REMARK = objData.REMARK

            'Dim lstDtl = (From p In Context.PE_HTCH_ASSESSMENT_DETAIL Where p.HTCH_ASSESSMENT_ID = obj.ID)
            'For Each item In lstDtl
            '    Context.PE_HTCH_ASSESSMENT_DETAIL.DeleteObject(item)
            'Next
            For Each n_Item In objData.lstDetail
                Dim objDtl = (From p In Context.PE_HTCH_ASSESSMENT_DETAIL Where p.ID = n_Item.ID).FirstOrDefault

                objDtl.POINTS_ACTUAL = n_Item.POINTS_ACTUAL
                objDtl.RESULT_ACTUAL = n_Item.RESULT_ACTUAL
                objDtl.POINTS_FINAL = n_Item.POINTS_FINAL
                objDtl.NOTE = n_Item.NOTE

            Next

            Context.SaveChanges(_log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try
    End Function

    Public Function SaveHTCHAssessmentListDetail(ByVal lstObj As List(Of PE_HTCH_ASSESSMENT_DTL_DTO), ByVal _log As UserLog) As Boolean
        Try

            For Each n_Item In lstObj
                Dim objDtl = (From p In Context.PE_HTCH_ASSESSMENT_DETAIL Where p.ID = n_Item.ID).FirstOrDefault

                objDtl.POINTS_ACTUAL = n_Item.POINTS_ACTUAL
                objDtl.RESULT_ACTUAL = n_Item.RESULT_ACTUAL
                objDtl.POINTS_FINAL = n_Item.POINTS_FINAL
                objDtl.NOTE = n_Item.NOTE
            Next

            Context.SaveChanges(_log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try
    End Function

    Public Function DeleteHTCHAssessment(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean
        Try
            Dim lstObj = From p In Context.PE_HTCH_ASSESSMENT Where lstID.Contains(p.ID)
            For Each item In lstObj
                Dim lstObjDtl = From p In Context.PE_HTCH_ASSESSMENT_DETAIL Where p.HTCH_ASSESSMENT_ID = item.ID

                For Each itemDtl In lstObjDtl
                    Context.PE_HTCH_ASSESSMENT_DETAIL.DeleteObject(itemDtl)
                Next

                Context.PE_HTCH_ASSESSMENT.DeleteObject(item)
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try
    End Function

    Public Function GetPeriodHTCHByYear(ByVal Year As Decimal) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PERFORMANCE_BUSINESS.GET_HTCH_PEROIOD_BY_YEAR",
                                               New With {.P_YEAR = Year,
                                                         .P_OUT = cls.OUT_CURSOR}, True)
                Return dtData
            End Using

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try
    End Function

    Public Function GetYearHTCH(ByVal IS_BLANK As Boolean) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PERFORMANCE_LIST.GET_YEAR_HTCH",
                                               New With {.P_BLANK = IS_BLANK,
                                                         .P_OUT = cls.OUT_CURSOR}, True)
                Return dtData
            End Using

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try
    End Function

    Public Function CAL_HTCH_ASSESSMENT(ByVal _org_id As List(Of Decimal), ByVal P_YEAR As Decimal, ByVal P_PERIOD As Decimal, ByVal log As UserLog) As Boolean
        Try
            Dim strOrg = String.Join(",", _org_id)
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PERFORMANCE_BUSINESS.CAL_HTCH_ASSESSMENT",
                                               New With {.P_ORG_ID = strOrg,
                                                         .P_YEAR = P_YEAR,
                                                         .P_PERIOD = P_PERIOD,
                                                         .P_USER = log.Username.ToUpper,
                                                         .P_OUT = cls.OUT_CURSOR}, True)
                Return CBool(dtData(0)(0))
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function CAL_HTCT_ASSESS_DTL(ByVal p_id As Decimal, ByVal log As UserLog) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PERFORMANCE_LIST.CAL_HTCT_ASSESS_DTL",
                                               New With {.P_ID = p_id,
                                                         .P_USER = log.Username.ToUpper,
                                                         .P_OUT = cls.OUT_CURSOR}, True)
                Return CBool(dtData(0)(0))
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function CHANGE_HTCH_ASSESSMENT_DTL(ByVal p_id As Decimal, ByVal log As UserLog) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PERFORMANCE_BUSINESS.CHANGE_HTCH_ASSESSMENT_DTL",
                                               New With {.P_ID = p_id,
                                                         .P_USER = log.Username.ToUpper,
                                                         .P_OUT = cls.OUT_CURSOR}, True)
                Return CBool(dtData(0)(0))
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function GetAproveHTCHAssessment(ByVal _filter As PE_HTCH_ASSESSMENT_DTO,
                                            ByVal PageIndex As Integer,
                                            ByVal PageSize As Integer,
                                            ByRef Total As Integer,
                                            Optional ByVal Sorts As String = "CREATED_DATE desc",
                                            Optional ByVal log As UserLog = Nothing) As List(Of PE_HTCH_ASSESSMENT_DTO)

        Try

            Dim query1 = From p In Context.PE_HTCH_ASSESSMENT
                         From employee In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                         From title In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
                         From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                         From period In Context.PE_PERIOD_HTCH.Where(Function(f) f.ID = p.PERIOD_ID).DefaultIfEmpty
                         From process_approve In Context.PROCESS_APPROVED_STATUS.Where(Function(f) f.ID_REGGROUP = p.ID And f.PROCESS_TYPE = "PERFORMANCE_HTCH")
                         From bf_process In Context.PROCESS_APPROVED_STATUS.Where(Function(f) f.ID_REGGROUP = p.ID And f.PROCESS_TYPE = "PERFORMANCE_HTCH" AndAlso f.APP_LEVEL = process_approve.APP_LEVEL - 1).DefaultIfEmpty
                         From status In Context.OT_OTHER_LIST.Where(Function(f) f.ID = process_approve.APP_STATUS).DefaultIfEmpty
                         Where process_approve.EMPLOYEE_APPROVED = _filter.EMPLOYEE_ID AndAlso ((process_approve.APP_STATUS = 0 AndAlso (bf_process.APP_STATUS Is Nothing Or bf_process.APP_STATUS = 1)) _
                             OrElse (process_approve.APP_STATUS = 1 Or process_approve.APP_STATUS = 2))
                         Select New PE_HTCH_ASSESSMENT_DTO With {
                                .ID = p.ID,
                                .EMPLOYEE_ID = p.EMPLOYEE_ID,
                                .EMPLOYEE_CODE = employee.EMPLOYEE_CODE,
                                .EMPLOYEE_NAME = employee.FULLNAME_VN,
                                .TITLE_ID = employee.TITLE_ID,
                                .TITLE_NAME = title.NAME_VN,
                                .TITLE_GROUP = p.TITLEGROUP_ID,
                                .ORG_ID = employee.ORG_ID,
                                .ORG_NAME = org.NAME_VN,
                                .YEAR = p.YEAR,
                                .PERIOD_ID = p.PERIOD_ID,
                                .PERIOD_NAME = period.NAME,
                                .START_DATE = p.START_DATE,
                                .END_DATE = p.END_DATE,
                                .EVALUATION_POINTS = p.EVALUATION_POINTS,
                                .CLASSIFICATION = p.CLASSIFICATION,
                                .STRENGTH_NOTE = p.STRENGTH_NOTE,
                                .IMPROVE_NOTE = p.IMPROVE_NOT,
                                .PROSPECT_NOTE = p.PROSPECT_NOTE,
                                .BRANCH_EVALUATE = p.BRANCH_EVALUATE,
                                .REMARK = p.REMARK,
                                .REASON = p.REASON,
                                .STATUS_ID = process_approve.APP_STATUS,
                                .STATUS_NAME = status.NAME_VN,
                                .CREATED_DATE = p.CREATED_DATE}

            Dim query2 = From p In Context.PE_HTCH_ASSESSMENT
                         From employee In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                         From title In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
                         From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                         From period In Context.PE_PERIOD_HTCH.Where(Function(f) f.ID = p.PERIOD_ID).DefaultIfEmpty
                         From process_approve In Context.PROCESS_APPROVED_STATUS.Where(Function(f) f.ID_REGGROUP = p.ID And f.PROCESS_TYPE = "PERFORMANCE_HTCH")
                         From bf_process In Context.PROCESS_APPROVED_STATUS.Where(Function(f) f.ID_REGGROUP = p.ID And f.PROCESS_TYPE = "PERFORMANCE_HTCH" AndAlso f.APP_LEVEL = process_approve.APP_LEVEL - 1).DefaultIfEmpty
                         From status In Context.OT_OTHER_LIST.Where(Function(f) f.ID = process_approve.APP_STATUS).DefaultIfEmpty
                         From app In Context.HUV_PERFORMANCE_KPI.Where(Function(f) f.EMPLOYEE_ID = process_approve.EMPLOYEE_APPROVED And f.PROCESS_CODE = "PERFORMANCE_HTCH" And f.SUB_EMPLOYEE_ID = _filter.EMPLOYEE_ID)
                         Where ((app.REPLACEALL = -1 And 1 = 1) Or (app.REPLACEALL = 0 And process_approve.CREATED_DATE >= app.FROM_DATE And process_approve.CREATED_DATE <= app.TO_DATE)) AndAlso ((process_approve.APP_STATUS = 0 AndAlso (bf_process.APP_STATUS Is Nothing Or bf_process.APP_STATUS = 1)) _
                             OrElse (process_approve.APP_STATUS = 1 Or process_approve.APP_STATUS = 2))
                         Select New PE_HTCH_ASSESSMENT_DTO With {
                                .ID = p.ID,
                                .EMPLOYEE_ID = p.EMPLOYEE_ID,
                                .EMPLOYEE_CODE = employee.EMPLOYEE_CODE,
                                .EMPLOYEE_NAME = employee.FULLNAME_VN,
                                .TITLE_ID = employee.TITLE_ID,
                                .TITLE_NAME = title.NAME_VN,
                                .TITLE_GROUP = p.TITLEGROUP_ID,
                                .ORG_ID = employee.ORG_ID,
                                .ORG_NAME = org.NAME_VN,
                                .YEAR = p.YEAR,
                                .PERIOD_ID = p.PERIOD_ID,
                                .PERIOD_NAME = period.NAME,
                                .START_DATE = p.START_DATE,
                                .END_DATE = p.END_DATE,
                                .EVALUATION_POINTS = p.EVALUATION_POINTS,
                                .CLASSIFICATION = p.CLASSIFICATION,
                                .STRENGTH_NOTE = p.STRENGTH_NOTE,
                                .IMPROVE_NOTE = p.IMPROVE_NOT,
                                .PROSPECT_NOTE = p.PROSPECT_NOTE,
                                .BRANCH_EVALUATE = p.BRANCH_EVALUATE,
                                .REMARK = p.REMARK,
                                .REASON = p.REASON,
                                .STATUS_ID = process_approve.APP_STATUS,
                                .STATUS_NAME = status.NAME_VN,
                                .CREATED_DATE = p.CREATED_DATE}

            Dim query = query1.Union(query2)

            If _filter.YEAR IsNot Nothing Then
                query = query.Where(Function(f) f.YEAR = _filter.YEAR)
            End If
            If _filter.PERIOD_ID IsNot Nothing Then
                query = query.Where(Function(f) f.PERIOD_ID = _filter.PERIOD_ID)
            End If

            If _filter.STATUS_ID IsNot Nothing Then
                query = query.Where(Function(f) f.STATUS_ID = _filter.STATUS_ID)
            End If

            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                query = query.Where(Function(f) f.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_NAME) Then
                query = query.Where(Function(f) f.EMPLOYEE_NAME.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                query = query.Where(Function(f) f.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                query = query.Where(Function(f) f.TITLE_NAME.ToUpper.Contains(_filter.TITLE_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.PERIOD_NAME) Then
                query = query.Where(Function(f) f.PERIOD_NAME.ToUpper.Contains(_filter.PERIOD_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.REASON) Then
                query = query.Where(Function(f) f.REASON.ToUpper.Contains(_filter.REASON.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.CLASSIFICATION) Then
                query = query.Where(Function(f) f.CLASSIFICATION.ToUpper.Contains(_filter.CLASSIFICATION.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.REMARK) Then
                query = query.Where(Function(f) f.REMARK.ToUpper.Contains(_filter.REMARK.ToUpper))
            End If

            If _filter.START_DATE.HasValue Then
                query = query.Where(Function(f) f.START_DATE = _filter.START_DATE)
            End If

            If _filter.END_DATE.HasValue Then
                query = query.Where(Function(f) f.END_DATE = _filter.END_DATE)
            End If

            If Not String.IsNullOrEmpty(_filter.EVALUATION_POINTS) Then
                query = query.Where(Function(f) f.EVALUATION_POINTS.ToUpper.Contains(_filter.EVALUATION_POINTS.ToUpper))
            End If

            If Not String.IsNullOrEmpty(_filter.BRANCH_EVALUATE) Then
                query = query.Where(Function(f) f.BRANCH_EVALUATE.ToUpper.Contains(_filter.BRANCH_EVALUATE.ToUpper))
            End If

            If Not String.IsNullOrEmpty(_filter.STRENGTH_NOTE) Then
                query = query.Where(Function(f) f.STRENGTH_NOTE.ToUpper.Contains(_filter.STRENGTH_NOTE.ToUpper))
            End If

            If Not String.IsNullOrEmpty(_filter.IMPROVE_NOTE) Then
                query = query.Where(Function(f) f.IMPROVE_NOTE.ToUpper.Contains(_filter.IMPROVE_NOTE.ToUpper))
            End If

            If Not String.IsNullOrEmpty(_filter.PROSPECT_NOTE) Then
                query = query.Where(Function(f) f.PROSPECT_NOTE.ToUpper.Contains(_filter.PROSPECT_NOTE.ToUpper))
            End If

            query = query.OrderBy(Sorts)
            Total = query.Count
            query = query.Skip(PageIndex * PageSize).Take(PageSize)
            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try
    End Function

    Public Function GetApproveHTCHAssessmentDetail(ByVal _filter As PE_HTCH_ASSESSMENT_DTL_DTO,
                                                    ByVal PageIndex As Integer,
                                                    ByVal PageSize As Integer,
                                                    ByRef Total As Integer,
                                                    Optional ByVal Sorts As String = "CREATED_DATE desc",
                                                    Optional ByVal log As UserLog = Nothing) As List(Of PE_HTCH_ASSESSMENT_DTL_DTO)

        Try
            Dim query = From t In Context.PE_HTCH_ASSESSMENT_DETAIL
                        From p In Context.PE_HTCH_ASSESSMENT.Where(Function(f) f.ID = t.HTCH_ASSESSMENT_ID)
                        From employee In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From title In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From period In Context.PE_PERIOD_HTCH.Where(Function(f) f.ID = p.PERIOD_ID).DefaultIfEmpty
                        From criterial In Context.PE_CRITERIA_HTCH.Where(Function(f) f.ID = t.CRITERIA_ID).DefaultIfEmpty
                        From process_approve In Context.PROCESS_APPROVED_STATUS.Where(Function(f) f.ID_REGGROUP = p.ID And f.PROCESS_TYPE = "PERFORMANCE_HTCH")
                        From bf_process In Context.PROCESS_APPROVED_STATUS.Where(Function(f) f.ID_REGGROUP = p.ID And f.PROCESS_TYPE = "PERFORMANCE_HTCH" AndAlso f.APP_LEVEL = process_approve.APP_LEVEL - 1).DefaultIfEmpty
                        From status In Context.OT_OTHER_LIST.Where(Function(f) f.ID = process_approve.APP_STATUS).DefaultIfEmpty
                        Where process_approve.EMPLOYEE_APPROVED = _filter.EMPLOYEE_ID AndAlso ((process_approve.APP_STATUS = 0 AndAlso (bf_process.APP_STATUS Is Nothing Or bf_process.APP_STATUS = 1)) _
                            OrElse (process_approve.APP_STATUS = 1 Or process_approve.APP_STATUS = 2))
                        Select New PE_HTCH_ASSESSMENT_DTL_DTO With {
                                .ID = t.ID,
                                .HTCH_ASSESSMENT_ID = p.ID,
                                .EMPLOYEE_ID = p.EMPLOYEE_ID,
                                .EMPLOYEE_CODE = employee.EMPLOYEE_CODE,
                                .EMPLOYEE_NAME = employee.FULLNAME_VN,
                                .TITLE_ID = employee.TITLE_ID,
                                .TITLE_NAME = title.NAME_VN,
                                .TITLE_GROUP = p.TITLEGROUP_ID,
                                .ORG_ID = employee.ORG_ID,
                                .ORG_NAME = org.NAME_VN,
                                .YEAR = p.YEAR,
                                .PERIOD_ID = p.PERIOD_ID,
                                .PERIOD_NAME = period.NAME,
                                .STATUS_ID = process_approve.APP_STATUS,
                                .STATUS_NAME = status.NAME_VN,
                                .CREATED_DATE = p.CREATED_DATE,
                                .CRITERIA_NAME = criterial.NAME,
                                .RATIO = t.RATIO,
                                .POINTS_ACTUAL = t.POINTS_ACTUAL,
                                .RESULT_ACTUAL = t.RESULT_ACTUAL,
                                .POINTS_FINAL = t.POINTS_FINAL,
                                .NOTE = t.NOTE}


            If _filter.YEAR IsNot Nothing Then
                query = query.Where(Function(f) f.YEAR = _filter.YEAR)
            End If
            If _filter.PERIOD_ID IsNot Nothing Then
                query = query.Where(Function(f) f.PERIOD_ID = _filter.PERIOD_ID)
            End If
            If _filter.STATUS_ID IsNot Nothing Then
                query = query.Where(Function(f) f.STATUS_ID = _filter.STATUS_ID)
            End If
            If _filter.RATIO IsNot Nothing Then
                query = query.Where(Function(f) f.RATIO = _filter.RATIO)
            End If
            If _filter.POINTS_ACTUAL IsNot Nothing Then
                query = query.Where(Function(f) f.POINTS_ACTUAL = _filter.POINTS_ACTUAL)
            End If
            If _filter.RESULT_ACTUAL IsNot Nothing Then
                query = query.Where(Function(f) f.RESULT_ACTUAL = _filter.RESULT_ACTUAL)
            End If
            If _filter.POINTS_FINAL IsNot Nothing Then
                query = query.Where(Function(f) f.POINTS_FINAL = _filter.POINTS_FINAL)
            End If

            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                query = query.Where(Function(f) f.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_NAME) Then
                query = query.Where(Function(f) f.EMPLOYEE_NAME.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                query = query.Where(Function(f) f.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                query = query.Where(Function(f) f.TITLE_NAME.ToUpper.Contains(_filter.TITLE_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.PERIOD_NAME) Then
                query = query.Where(Function(f) f.PERIOD_NAME.ToUpper.Contains(_filter.PERIOD_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.CRITERIA_NAME) Then
                query = query.Where(Function(f) f.CRITERIA_NAME.ToUpper.Contains(_filter.CRITERIA_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.STATUS_NAME) Then
                query = query.Where(Function(f) f.STATUS_NAME.ToUpper.Contains(_filter.STATUS_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.NOTE) Then
                query = query.Where(Function(f) f.NOTE.ToUpper.Contains(_filter.NOTE.ToUpper))
            End If
            query = query.OrderBy(Sorts)
            Total = query.Count
            query = query.Skip(PageIndex * PageSize).Take(PageSize)
            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try
    End Function

    Public Function IMPORT_HTCH_ASSESSMENT(ByVal DocXML As String, ByVal log As UserLog) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PERFORMANCE_BUSINESS.IMPORT_HTCH_ASSESSMENT",
                                               New With {.P_DOCXML = DocXML,
                                                         .P_USER = log.Username.ToUpper,
                                                         .P_OUT = cls.OUT_CURSOR}, True)
                Return CBool(dtData(0)(0))
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try
    End Function

#End Region

#Region "Employee Period"
    Public Function GetPeriod2(ByVal year As Decimal, ByVal isBlank As Decimal) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_PERIOD_BY_YEAR",
                                           New With {.P_YEAR = year,
                                                     .P_ISBLANK = isBlank,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try
    End Function
    Public Function GetPeriodByYear2(ByVal Year As Decimal) As DataTable
        Try
            Dim query = From p1 In Context.PE_PERIOD_HTCH
                        Where p1.YEAR = Year
                        Select p1.ID, p1.NAME
            Return query.ToList.ToTable
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try
    End Function

    Public Function GetDateByPeriod2(ByVal Period As Decimal) As DataTable
        Try
            Dim query = From p In Context.PE_PERIOD_HTCH
                        Where p.ID = Period
                        Select p.START_DATE, p.END_DATE

            Return query.ToList.ToTable

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try
    End Function
    Public Function GetEmployeePeriodHCTH(ByVal _filter As EmployeePeriodDTO,
                                         ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer,
                                         ByVal lstOrg As List(Of Decimal),
                                         Optional ByVal Sorts As String = "CREATED_DATE desc",
                                         Optional ByVal log As UserLog = Nothing) As List(Of EmployeePeriodDTO)

        Try
            Dim strOrg = String.Join(",", lstOrg)

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG2",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = strOrg})
            End Using

            Dim query = From p In Context.PE_EMPLOYEEHTCH_PERIOD
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From m In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.DIRECT_MANAGER).DefaultIfEmpty
                        From htch In Context.PE_HTCH_ASSESSMENT.Where(Function(f) f.EMPLOYEE_ID = p.EMPLOYEE_ID And f.PERIOD_ID = p.PE_PERIOD_ID).DefaultIfEmpty
                        From e_cv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = p.DIRECT_MANAGER).DefaultIfEmpty
                        From title In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From period In Context.PE_PERIOD.Where(Function(f) f.ID = p.PE_PERIOD_ID).DefaultIfEmpty
                        From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = e.ORG_ID And f.USERNAME = log.Username.ToUpper)
                        Select New EmployeePeriodDTO With {
                                .ID = p.ID,
                                .EMPLOYEE_ID = p.EMPLOYEE_ID,
                                .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                                .EMPLOYEE_NAME = e.FULLNAME_VN,
                                .TITLE_ID = p.TITLE_ID,
                                .TITLE_NAME = title.NAME_VN,
                                .ORG_ID = p.ORG_ID,
                                .ORG_NAME = org.NAME_VN,
                                .PE_PERIOD_ID = p.PE_PERIOD_ID,
                                .PE_PERIOD_NAME = period.NAME,
                                .MONTH_NUMBER = p.MONTH_NUMBER,
                                .JOIN_DATE = p.JOIN_DATE,
                                .JOIN_DATE_STATE = p.JOIN_DATE_STATE,
                                .SEND_MAIL = p.SEND_MAIL,
                                .SEND_MAIL_STT = If(p.SEND_MAIL.Value = 1, "Đã gửi", "Chưa gửi"),
                                .CREATED_DATE = p.CREATED_DATE,
                                .EMAIL = e_cv.WORK_EMAIL,
                                .SALARY = p.SALARY,
                                .CHANGE_SALARY_DATE = p.CHANGESALARY_DATE,
                                .DIRECT_MANAGER = p.DIRECT_MANAGER,
                                .DIRECT_MANAGER_NAME = m.FULLNAME_VN,
                                .EVALUATION_POINTS = htch.EVALUATION_POINTS,
                                .CLASSIFICATION = htch.CLASSIFICATION,
                                .PER_STATUS = If(htch.EMPLOYEE_ID Is Nothing, "", If((From x In Context.PE_HTCH_ASSESSMENT_DETAIL Where x.HTCH_ASSESSMENT_ID = htch.ID).Count > 0, "Đã tham gia", ""))}

            If _filter.PE_PERIOD_ID IsNot Nothing Then
                query = query.Where(Function(f) f.PE_PERIOD_ID = _filter.PE_PERIOD_ID)
            End If

            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                query = query.Where(Function(f) f.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_NAME) Then
                query = query.Where(Function(f) f.EMPLOYEE_NAME.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                query = query.Where(Function(f) f.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                query = query.Where(Function(f) f.TITLE_NAME.ToUpper.Contains(_filter.TITLE_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.PE_PERIOD_NAME) Then
                query = query.Where(Function(f) f.PE_PERIOD_NAME.ToUpper.Contains(_filter.PE_PERIOD_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.SEND_MAIL_STT) Then
                query = query.Where(Function(f) f.SEND_MAIL_STT.ToUpper.Contains(_filter.SEND_MAIL_STT.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.EMAIL) Then
                query = query.Where(Function(f) f.EMAIL.ToUpper.Contains(_filter.EMAIL.ToUpper))
            End If

            If Not String.IsNullOrEmpty(_filter.EVALUATION_POINTS) Then
                query = query.Where(Function(f) f.EVALUATION_POINTS.ToUpper.Contains(_filter.EVALUATION_POINTS.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.CLASSIFICATION) Then
                query = query.Where(Function(f) f.CLASSIFICATION.ToUpper.Contains(_filter.CLASSIFICATION.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.PER_STATUS) Then
                query = query.Where(Function(f) f.PER_STATUS.ToUpper.Contains(_filter.PER_STATUS.ToUpper))
            End If
            If _filter.JOIN_DATE.HasValue Then
                query = query.Where(Function(f) f.JOIN_DATE = _filter.JOIN_DATE)
            End If

            If _filter.JOIN_DATE_STATE.HasValue Then
                query = query.Where(Function(f) f.JOIN_DATE_STATE = _filter.JOIN_DATE_STATE)
            End If
            If _filter.CHANGE_SALARY_DATE.HasValue Then
                query = query.Where(Function(f) f.CHANGE_SALARY_DATE = _filter.CHANGE_SALARY_DATE)
            End If

            query = query.OrderBy(Sorts)
            Total = query.Count
            query = query.Skip(PageIndex * PageSize).Take(PageSize)
            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try
    End Function
    Public Function DeleteEmployeeHTCHPeriod(ByVal _lstID As List(Of Decimal)) As Boolean
        Try
            Dim lstObj = From p In Context.PE_EMPLOYEEHTCH_PERIOD Where _lstID.Contains(p.ID)
            For Each item In lstObj
                Context.PE_EMPLOYEEHTCH_PERIOD.DeleteObject(item)
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try
    End Function
    Public Function CAL_EMPLOYEEHTCH_PERIOD(ByVal _org_id As List(Of Decimal), ByVal _Period_ID As Decimal, ByVal log As UserLog) As Boolean
        Try
            Dim strOrg = String.Join(",", _org_id)
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PERFORMANCE_BUSINESS.CAL_EMPLOYEEHTCH_PERIOD",
                                               New With {.P_ORG_ID = strOrg,
                                                         .P_USER = log.Username.ToUpper,
                                                         .P_PERIOD_ID = _Period_ID,
                                                         .P_CUR = cls.OUT_CURSOR}, True)
                Return CBool(dtData(0)(0))
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try

    End Function

    Public Function CAL_EMP_RECOMEND_RESULT(ByVal _org_id As List(Of Decimal), ByVal P_PERIOD As Decimal, ByVal log As UserLog) As Boolean
        Try
            Dim strOrg = String.Join(",", _org_id)
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PERFORMANCE_BUSINESS.CAL_EMP_RECOMEND_RESULT",
                                               New With {.P_ORG_ID = strOrg,
                                                         .P_PERIOD_ID = P_PERIOD,
                                                         .P_USER = log.Username.ToUpper,
                                                         .P_OUT = cls.OUT_CURSOR}, True)
                Return CBool(dtData(0)(0))
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function


    Public Function GetEmployeePeriods(ByVal _filter As EmployeePeriodDTO,
                                         ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer,
                                         ByVal lstOrg As List(Of Decimal),
                                         Optional ByVal Sorts As String = "CREATED_DATE desc",
                                         Optional ByVal log As UserLog = Nothing) As List(Of EmployeePeriodDTO)

        Try
            Dim strOrg = String.Join(",", lstOrg)

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG2",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = strOrg})
            End Using

            Dim query = From p In Context.PE_EMPLOYEE_PERIOD
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From e_cv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = p.EMPLOYEE_ID)
                        From title In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From period In Context.PE_PERIOD.Where(Function(f) f.ID = p.PE_PERIOD_ID).DefaultIfEmpty
                        From kpi In Context.PE_KPI_ASSESSMENT.Where(Function(f) f.EMPLOYEE = p.EMPLOYEE_ID And f.PE_PERIOD_ID = p.PE_PERIOD_ID).DefaultIfEmpty
                        From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = e.ORG_ID And f.USERNAME = log.Username.ToUpper)
                        Select New EmployeePeriodDTO With {
                                .ID = p.ID,
                                .EMPLOYEE_ID = p.EMPLOYEE_ID,
                                .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                                .EMPLOYEE_NAME = e.FULLNAME_VN,
                                .TITLE_ID = p.TITLE_ID,
                                .TITLE_NAME = title.NAME_VN,
                                .ORG_ID = p.ORG_ID,
                                .ORG_NAME = org.NAME_VN,
                                .PE_PERIOD_ID = p.PE_PERIOD_ID,
                                .PE_PERIOD_NAME = period.NAME,
                                .MONTH_NUMBER = p.MONTH_NUMBER,
                                .JOIN_DATE = p.JOIN_DATE,
                                .JOIN_DATE_STATE = p.JOIN_DATE_STATE,
                                .SEND_MAIL = p.SEND_MAIL,
                                .SEND_MAIL_STT = If(p.SEND_MAIL.Value = 1, "Đã gửi", "Chưa gửi"),
                                .CREATED_DATE = p.CREATED_DATE,
                                .EMAIL = e_cv.WORK_EMAIL,
                                .PER_STATUS = If(kpi.EMPLOYEE Is Nothing, "", If((From x In Context.PE_KPI_ASSESSMENT_DETAIL Where x.GOAL_ID = kpi.ID).Count > 0, "Đã tham gia", ""))}

            If _filter.PE_PERIOD_ID IsNot Nothing Then
                query = query.Where(Function(f) f.PE_PERIOD_ID = _filter.PE_PERIOD_ID)
            End If

            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                query = query.Where(Function(f) f.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_NAME) Then
                query = query.Where(Function(f) f.EMPLOYEE_NAME.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                query = query.Where(Function(f) f.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                query = query.Where(Function(f) f.TITLE_NAME.ToUpper.Contains(_filter.TITLE_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.PE_PERIOD_NAME) Then
                query = query.Where(Function(f) f.PE_PERIOD_NAME.ToUpper.Contains(_filter.PE_PERIOD_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.SEND_MAIL_STT) Then
                query = query.Where(Function(f) f.SEND_MAIL_STT.ToUpper.Contains(_filter.SEND_MAIL_STT.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.EMAIL) Then
                query = query.Where(Function(f) f.EMAIL.ToUpper.Contains(_filter.EMAIL.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.PER_STATUS) Then
                query = query.Where(Function(f) f.PER_STATUS.ToUpper.Contains(_filter.PER_STATUS.ToUpper))
            End If

            If _filter.JOIN_DATE.HasValue Then
                query = query.Where(Function(f) f.JOIN_DATE = _filter.JOIN_DATE)
            End If

            If _filter.JOIN_DATE_STATE.HasValue Then
                query = query.Where(Function(f) f.JOIN_DATE_STATE = _filter.JOIN_DATE_STATE)
            End If

            query = query.OrderBy(Sorts)
            Total = query.Count
            query = query.Skip(PageIndex * PageSize).Take(PageSize)
            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try
    End Function

    Public Function CAL_EMPLOYEE_PERIOD(ByVal _org_id As List(Of Decimal), ByVal _Period_ID As Decimal, ByVal log As UserLog) As Boolean
        Try
            Dim strOrg = String.Join(",", _org_id)
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PERFORMANCE_BUSINESS.CAL_EMPLOYEE_PERIOD",
                                               New With {.P_ORG_ID = strOrg,
                                                         .P_USER = log.Username.ToUpper,
                                                         .P_PERIOD_ID = _Period_ID,
                                                         .P_CUR = cls.OUT_CURSOR}, True)
                Return CBool(dtData(0)(0))
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try

    End Function

    Public Function DeleteEmployeePeriod(ByVal _lstID As List(Of Decimal)) As Boolean
        Try
            Dim lstObj = From p In Context.PE_EMPLOYEE_PERIOD Where _lstID.Contains(p.ID)
            For Each item In lstObj
                Context.PE_EMPLOYEE_PERIOD.DeleteObject(item)
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try
    End Function
#End Region

    Public Function APPROVE_EVALUATE_TARGET(ByVal P_ID_REGGROUP As Decimal, ByVal P_APPROVE_ID As Decimal, ByVal P_EMPLOYEE_ID As Decimal, ByVal P_STATUS_ID As Decimal, ByVal P_REASON As String) As Boolean
        Using cls As New DataAccess.QueryData
            Dim obj = New With {.P_ID_REGGROUP = P_ID_REGGROUP, .P_APPROVE_ID = P_APPROVE_ID, .P_EMPLOYEE_ID = P_EMPLOYEE_ID, .P_STATUS_ID = P_STATUS_ID, .P_REASON = P_REASON, .P_OUT = cls.OUT_NUMBER}
            Dim store = cls.ExecuteStore("PKG_PERFORMANCE_BUSINESS.APPROVE_EVALUATE_TARGET", obj)
            If Int32.Parse(obj.P_OUT) = 1 Then
                Return True
            End If
            Return False
        End Using
    End Function

#Region "PE_ORG_MR_RR -> Quản lý dữ liệu RR và MR theo tháng"

    Public Function GetPe_Org_Mr_Rr(ByVal _filter As PE_ORG_MR_RRDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        ByVal lstOrg As List(Of Decimal),
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of PE_ORG_MR_RRDTO)

        Try
            Dim strOrg = String.Join(",", lstOrg)

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG2",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = strOrg})
            End Using

            Dim query = From p In Context.PE_ORG_MR_RR
                        From period In Context.AT_PERIOD.Where(Function(f) f.ID = p.PERIOD_ID).DefaultIfEmpty
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = p.ORG_ID And f.USERNAME = log.Username.ToUpper)
                        Select New PE_ORG_MR_RRDTO With {
                                .ID = p.ID,
                                .ORG_ID = p.ORG_ID,
                                .ORG_NAME = org.NAME_VN,
                                .AT_PERIOD_ID = p.PERIOD_ID,
                                .AT_PERIOD_NAME = period.PERIOD_NAME,
                                .MONTH = period.MONTH,
                                .MR_LE = p.MR_LE,
                                .MR_THE = p.MR_THE,
                                .RR = p.RR,
                                .YEAR = period.YEAR,
                                .CREATED_DATE = p.CREATED_DATE,
                            .CREATED_BY = p.CREATED_BY,
                            .CREATED_LOG = p.CREATED_LOG,
                            .MODIFIED_BY = p.MODIFIED_BY,
                            .MODIFIED_DATE = p.MODIFIED_DATE,
                            .MODIFIED_LOG = p.MODIFIED_LOG}

            If _filter.AT_PERIOD_ID IsNot Nothing And _filter.AT_PERIOD_ID <> 0 Then
                query = query.Where(Function(f) f.AT_PERIOD_ID = _filter.AT_PERIOD_ID)
            End If
            If _filter.YEAR IsNot Nothing Then
                query = query.Where(Function(f) f.YEAR = _filter.YEAR)
            End If
            If Not String.IsNullOrEmpty(_filter.AT_PERIOD_NAME) Then
                query = query.Where(Function(f) f.AT_PERIOD_NAME.ToUpper.Contains(_filter.AT_PERIOD_NAME.ToUpper))
            End If

            If _filter.MR_LE.HasValue Then
                query = query.Where(Function(f) f.MR_LE = _filter.MR_LE)
            End If

            If _filter.MR_THE.HasValue Then
                query = query.Where(Function(f) f.MR_THE = _filter.MR_THE)
            End If
            If _filter.RR.HasValue Then
                query = query.Where(Function(f) f.RR = _filter.RR)
            End If

            query = query.OrderBy(Sorts)
            Total = query.Count
            query = query.Skip(PageIndex * PageSize).Take(PageSize)
            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try
    End Function

    Public Function GetATPeriodByYear(ByVal Year As Decimal) As DataTable
        Try
            Dim query = From p1 In Context.AT_PERIOD
                        Where p1.YEAR = Year
                        Order By p1.PERIOD_NAME
                        Select p1.ID, p1.PERIOD_NAME
            Return query.ToList.ToTable
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try
    End Function
    Public Function GetYearATPeriod() As DataTable
        Try

            Dim query = From p In Context.AT_PERIOD
                        Order By p.YEAR

            Dim lst = query.Select(Function(p) New AT_PERIODDTO With {
                                       .YEAR = p.YEAR}).Distinct.ToList
            Return lst.ToTable
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function



    Public Function ValidatePe_Org_Mr_Rr(ByVal obj As PE_ORG_MR_RRDTO) As Boolean
        Try
            Dim query = From p In Context.PE_ORG_MR_RR Where p.ORG_ID = obj.ORG_ID AndAlso p.PERIOD_ID = obj.AT_PERIOD_ID
            Return query.Any
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".ValidatePe_Org_Mr_Rr")
            Throw ex
        End Try
    End Function

#End Region

End Class
