Imports Framework.Data
Imports Framework.Data.System.Linq.Dynamic

Partial Class RecruitmentRepository

#Region "lay phuong xa"
    Public Function GetWardList(districtID As Decimal, ByVal isBlank As Boolean) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_HU_WARD",
                                           New With {.P_ISBLANK = isBlank,
                                                     .P_DISTRICT_ID = districtID,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region
#Region "CostCenter"

    Public Function GetCostCenter(ByVal _filter As CostCenterDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CostCenterDTO)

        Try
            Dim query = From p In Context.RC_COST_CENTER

            Dim lst = query.Select(Function(p) New CostCenterDTO With {
                                       .ID = p.ID,
                                       .CODE = p.CODE,
                                       .NAME = p.NAME,
                                       .ACTFLG = p.ACTFLG,
                                       .CREATED_DATE = p.CREATED_DATE})
            If _filter.CODE <> "" Then
                lst = lst.Where(Function(p) p.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
            End If
            If _filter.NAME <> "" Then
                lst = lst.Where(Function(p) p.NAME.ToUpper.Contains(_filter.NAME.ToUpper))
            End If
            If _filter.ACTFLG IsNot Nothing Then
                lst = lst.Where(Function(p) p.ACTFLG = _filter.ACTFLG)
            End If
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetCostCenter")
            Throw ex
        End Try

    End Function

    Public Function InsertCostCenter(ByVal objCostCenter As CostCenterDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objCostCenterData As New RC_COST_CENTER
        Try
            objCostCenterData.ID = Utilities.GetNextSequence(Context, Context.RC_COST_CENTER.EntitySet.Name)
            objCostCenterData.CODE = objCostCenter.CODE.Trim
            objCostCenterData.NAME = objCostCenter.NAME.Trim
            Context.RC_COST_CENTER.AddObject(objCostCenterData)
            Context.SaveChanges(log)
            gID = objCostCenterData.ID
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".InsertCostCenter")
            Throw ex
        End Try
    End Function

    Public Function ValidateCostCenter(ByVal _validate As CostCenterDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.RC_COST_CENTER
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.RC_COST_CENTER
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If

                Return (query Is Nothing)
            End If
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".ValidateCostCenter")
            Throw ex
        End Try
    End Function

    Public Function ModifyCostCenter(ByVal objCostCenter As CostCenterDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objCostCenterData As New RC_COST_CENTER With {.ID = objCostCenter.ID}
        Try
            Context.RC_COST_CENTER.Attach(objCostCenterData)
            objCostCenterData.CODE = objCostCenter.CODE.Trim
            objCostCenterData.NAME = objCostCenter.NAME.Trim
            Context.SaveChanges(log)
            gID = objCostCenterData.ID
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".ModifyCostCenter")
            Throw ex
        End Try

    End Function

    Public Function ActiveCostCenter(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As Boolean) As Boolean
        Dim lstData As List(Of RC_COST_CENTER)
        Try
            lstData = (From p In Context.RC_COST_CENTER Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                lstData(index).ACTFLG = bActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            Throw ex
        End Try

    End Function


    Public Function DeleteCostCenter(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstCostCenterData As List(Of RC_COST_CENTER)
        Try
            lstCostCenterData = (From p In Context.RC_COST_CENTER Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstCostCenterData.Count - 1
                Context.RC_COST_CENTER.DeleteObject(lstCostCenterData(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteCostCenter")
            Throw ex
        End Try

    End Function

#End Region

#Region "ExamsDtl"

    Public Function GetExamsDtl(ByVal _filter As ExamsDtlDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "EXAMS_ORDER") As List(Of ExamsDtlDTO)

        Try
            Dim query = From p In Context.RC_EXAMS
                        From dtl In Context.RC_EXAMS_DTL.Where(Function(f) f.RC_EXAMS_ID = p.ID)
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID)
                        From title In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID)
                        Where p.ORG_ID = _filter.ORG_ID And p.TITLE_ID = _filter.TITLE_ID
                        Select New ExamsDtlDTO With {
                                        .ID = dtl.ID,
                                        .ORG_ID = p.ORG_ID,
                                        .ORG_NAME = org.NAME_VN,
                                        .TITLE_ID = p.TITLE_ID,
                                        .TITLE_NAME = title.NAME_VN,
                                        .NAME = dtl.NAME,
                                        .POINT_LADDER = dtl.POINT_LADDER,
                                        .POINT_PASS = dtl.POINT_PASS,
                                        .EXAMS_ORDER = dtl.EXAMS_ORDER,
                                        .COEFFICIENT = dtl.COEFFICIENT,
                                        .NOTE = dtl.NOTE,
                                        .IS_PV = dtl.IS_PV}

            Dim lst = query

            If _filter.ORG_NAME <> "" Then
                lst = lst.Where(Function(p) p.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If

            If _filter.TITLE_NAME <> "" Then
                lst = lst.Where(Function(p) p.TITLE_NAME.ToUpper.Contains(_filter.TITLE_NAME.ToUpper))
            End If

            If _filter.NAME <> "" Then
                lst = lst.Where(Function(p) p.NAME.ToUpper.Contains(_filter.NAME.ToUpper))
            End If
            If _filter.POINT_LADDER IsNot Nothing Then
                lst = lst.Where(Function(p) p.POINT_LADDER = _filter.POINT_LADDER)
            End If

            If _filter.POINT_PASS IsNot Nothing Then
                lst = lst.Where(Function(p) p.POINT_PASS = _filter.POINT_PASS)
            End If

            If _filter.EXAMS_ORDER IsNot Nothing Then
                lst = lst.Where(Function(p) p.EXAMS_ORDER = _filter.EXAMS_ORDER)
            End If

            If _filter.COEFFICIENT IsNot Nothing Then
                lst = lst.Where(Function(p) p.COEFFICIENT = _filter.COEFFICIENT)
            End If

            If _filter.NOTE IsNot Nothing Then
                lst = lst.Where(Function(p) p.NOTE = _filter.NOTE)
            End If

            If _filter.IS_PV IsNot Nothing Then
                lst = lst.Where(Function(p) p.IS_PV = _filter.IS_PV)
            End If
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetExamsDtl")
            Throw ex
        End Try

    End Function

    Public Function UpdateExamsDtl(ByVal objExams As ExamsDtlDTO, ByVal log As UserLog) As Boolean
        Dim objExamsData As RC_EXAMS
        Dim objExamsDtlData As RC_EXAMS_DTL
        Try
            objExamsData = (From p In Context.RC_EXAMS
                            Where p.ORG_ID = objExams.ORG_ID And _
                            p.TITLE_ID = objExams.TITLE_ID And _
                            p.ID = objExams.ID).FirstOrDefault
            If objExamsData Is Nothing Then
                objExamsData = New RC_EXAMS
                objExamsData.ID = Utilities.GetNextSequence(Context, Context.RC_EXAMS.EntitySet.Name)
                objExamsData.ORG_ID = objExams.ORG_ID
                objExamsData.TITLE_ID = objExams.TITLE_ID
                Context.RC_EXAMS.AddObject(objExamsData)
                Context.SaveChanges(log)
            End If
            objExamsDtlData = (From p In Context.RC_EXAMS_DTL Where p.ID = objExams.ID).FirstOrDefault
            If objExamsDtlData IsNot Nothing Then
                objExamsDtlData.NAME = objExams.NAME
                objExamsDtlData.EXAMS_ORDER = objExams.EXAMS_ORDER
                objExamsDtlData.POINT_LADDER = objExams.POINT_LADDER
                objExamsDtlData.POINT_PASS = objExams.POINT_PASS
                objExamsDtlData.NOTE = objExams.NOTE
                objExamsDtlData.COEFFICIENT = objExams.COEFFICIENT
                objExamsDtlData.IS_PV = objExams.IS_PV
                objExamsDtlData.RC_EXAMS_ID = objExamsData.ID
            Else
                objExamsDtlData = New RC_EXAMS_DTL
                objExamsDtlData.ID = Utilities.GetNextSequence(Context, Context.RC_EXAMS_DTL.EntitySet.Name)
                objExamsDtlData.NAME = objExams.NAME
                objExamsDtlData.EXAMS_ORDER = objExams.EXAMS_ORDER
                objExamsDtlData.POINT_LADDER = objExams.POINT_LADDER
                objExamsDtlData.POINT_PASS = objExams.POINT_PASS
                objExamsDtlData.NOTE = objExams.NOTE
                objExamsDtlData.COEFFICIENT = objExams.COEFFICIENT
                objExamsDtlData.IS_PV = objExams.IS_PV
                objExamsDtlData.RC_EXAMS_ID = objExamsData.ID
                Context.RC_EXAMS_DTL.AddObject(objExamsDtlData)
            End If
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".UpdateExamsDtl")
            Throw ex
        End Try
    End Function

    Public Function DeleteExamsDtl(ByVal obj As ExamsDtlDTO) As Boolean
        Dim objDel As RC_EXAMS_DTL
        Try
            objDel = (From p In Context.RC_EXAMS_DTL Where obj.ID = p.ID).FirstOrDefault
            Context.RC_EXAMS_DTL.DeleteObject(objDel)
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteExamsDtl")
            Throw ex
        End Try

    End Function

#End Region
#Region "Yêu cầu tuyển dụng"
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
#End Region

End Class
