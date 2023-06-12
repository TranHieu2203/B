Imports System.Reflection
Imports Framework.Data
Imports Framework.Data.System.Linq.Dynamic

Partial Class ProfileRepository

#Region "Working"
    'check phê duyệt và đã có đính kèm file hay chưa
    'yêu cầu nếu phê duyệt thì phải có phải đính kèm
    'màn hinh luong va hop dong
    Public Function CheckHasFile(ByVal id As List(Of Decimal)) As Decimal
        Try
            Dim workings = Context.HU_WORKING.Where(Function(p) p.STATUS_ID = ProfileCommon.DECISION_STATUS.WAIT_APPROVE_ID And id.Contains(p.ID)).ToList()
            For Each working As HU_WORKING In workings
                If working.FILENAME Is Nothing Or working.FILENAME = "" Then
                    Return 1
                End If
            Next
            Return 2
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function ApproveWorkings(ByVal ids As List(Of Decimal), ByVal acti As Decimal, ByVal log As UserLog) As CommandResult
        Dim result = New CommandResult
        Try
            If acti = 446 Then
                Dim workings = Context.HU_WORKING.Where(Function(p) p.STATUS_ID = ProfileCommon.DECISION_STATUS.WAIT_APPROVE_ID And ids.Contains(p.ID)).ToList()
                For Each working As HU_WORKING In workings
                    working.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID
                Next
                Context.SaveChanges(log)
            Else
                Dim workings = Context.HU_WORKING.Where(Function(p) p.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID And ids.Contains(p.ID)).ToList()
                For Each working As HU_WORKING In workings
                    working.STATUS_ID = ProfileCommon.DECISION_STATUS.WAIT_APPROVE_ID
                Next
                Context.SaveChanges(log)
            End If
        Catch ex As Exception
            result.Status = 0
            result.Exception = ex
            Return result
        End Try
        result.Status = 1
        Return result
    End Function
    Public Function DeleteWorkingAllowance(ByVal lstWorkingAllowance() As HUAllowanceDTO,
                                  ByVal log As UserLog) As Boolean


        Dim lstWageLevelData As List(Of HU_ALLOWANCE)
        Try
            Dim lst As New List(Of Decimal)
            For Each item In lstWorkingAllowance
                'Dim check = Context.HU_ALLOWANCE.Where(Function(f) f.ID = item.ID).FirstOrDefault
                'If Date.Now >= check.EFFECT_DATE Then
                '    Return False
                'End If
                lst.Add(item.ID)
            Next
            lstWageLevelData = (From p In Context.HU_ALLOWANCE Where lst.Contains(p.ID)).ToList

            For idx = 0 To lstWageLevelData.Count - 1
                Context.HU_ALLOWANCE.DeleteObject(lstWageLevelData(idx))
                Del_EXPIRE_DATE_IN_ALLOWANCE(lstWageLevelData(idx))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
    Public Function InsertWorkingAllowance(ByVal objWorkingAllowance As HUAllowanceDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Try

            Dim check = Context.HU_ALLOWANCE.Where(Function(f) f.EMPLOYEE_ID = objWorkingAllowance.EMPLOYEE_ID And
                                                       f.EFFECT_DATE = objWorkingAllowance.EFFECT_DATE And f.ALLOWANCE_TYPE = objWorkingAllowance.ALLOWANCE_LIST_ID
                                                     ).FirstOrDefault

            If check IsNot Nothing Then
                Return False
            End If

            Dim objWorkingAllowanceData As New HU_ALLOWANCE
            objWorkingAllowanceData.ID = Utilities.GetNextSequence(Context, Context.HU_ALLOWANCE.EntitySet.Name)
            objWorkingAllowanceData.ALLOWANCE_TYPE = objWorkingAllowance.ALLOWANCE_LIST_ID
            objWorkingAllowanceData.EMPLOYEE_ID = objWorkingAllowance.EMPLOYEE_ID
            objWorkingAllowanceData.AMOUNT = objWorkingAllowance.AMOUNT
            objWorkingAllowanceData.EFFECT_DATE = objWorkingAllowance.EFFECT_DATE
            objWorkingAllowanceData.EXP_DATE = objWorkingAllowance.EXPIRE_DATE
            objWorkingAllowanceData.FACTOR = objWorkingAllowance.FACTOR
            objWorkingAllowanceData.REMARK = objWorkingAllowance.REMARK
            objWorkingAllowanceData.FILE_NAME = objWorkingAllowance.FILE_NAME
            'objWorkingAllowanceData.EMPLOYEE_SIGNER_ID = objWorkingAllowance.EMPLOYEE_SIGNER_ID
            'If objWorkingAllowance.IS_INSUR IsNot Nothing Then
            '    objWorkingAllowanceData.IS_INSURRANCE = objWorkingAllowance.IS_INSUR
            'Else
            '    objWorkingAllowanceData.IS_INSURRANCE = objWorkingAllowance.IS_INSURRANCE
            'End If
            objWorkingAllowanceData.ACTFLG = objWorkingAllowance.ACTFLG
            objWorkingAllowanceData.DETAIL_ADD = objWorkingAllowance.DETAIL_ADD
            objWorkingAllowanceData.IS_DEDUCT = objWorkingAllowance.IS_DEDUCT
            objWorkingAllowanceData.PERCENT_PIT = objWorkingAllowance.PERCENT_PIT
            objWorkingAllowanceData.AMOUNT_PIT = objWorkingAllowance.AMOUNT_PIT
            'objWorkingAllowanceData.DATE_SIGNER = objWorkingAllowance.DATE_SIGNER
            'objWorkingAllowanceData.SIGNER_NAME = objWorkingAllowance.SIGNER_NAME
            'objWorkingAllowanceData.SIGNER_LEVEL_NAME = objWorkingAllowance.SIGNER_LEVEL_NAME
            'objWorkingAllowanceData.SIGNER_TITLE_NAME = objWorkingAllowance.SIGNER_TITLE_NAME
            Context.HU_ALLOWANCE.AddObject(objWorkingAllowanceData)
            Context.SaveChanges(log)
            gID = objWorkingAllowanceData.ID
            Update_EXPIRE_DATE_IN_ALLOWANCE(objWorkingAllowance, log)
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".InsertWorkingAllowance")
            Throw ex
        End Try
    End Function
    Public Function Update_EXPIRE_DATE_IN_ALLOWANCE(ByVal objWorkingAllowance As HUAllowanceDTO, ByVal log As UserLog) As Boolean

        Try
            ' Phê duyệt và cập nhật sang hồ sơ
            If objWorkingAllowance.EFFECT_DATE IsNot Nothing Then
                Dim query_old = (From p In Context.HU_ALLOWANCE
                                 Where p.EFFECT_DATE < objWorkingAllowance.EFFECT_DATE _
                                     And p.EMPLOYEE_ID = objWorkingAllowance.EMPLOYEE_ID _
                                     And p.ALLOWANCE_TYPE = objWorkingAllowance.ALLOWANCE_LIST_ID
                                 Order By p.EFFECT_DATE Descending).FirstOrDefault
                If query_old IsNot Nothing AndAlso query_old.EXP_DATE Is Nothing Then
                    Dim dates As Date = CDate(objWorkingAllowance.EFFECT_DATE)
                    query_old.EXP_DATE = dates.AddDays(-1)
                End If
                Context.SaveChanges(log)
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function Del_EXPIRE_DATE_IN_ALLOWANCE(ByVal objWorkingAllowance As HU_ALLOWANCE) As Boolean

        Try

            If objWorkingAllowance.EFFECT_DATE IsNot Nothing Then
                Dim query_old = (From p In Context.HU_ALLOWANCE
                                 Where p.EFFECT_DATE < objWorkingAllowance.EFFECT_DATE _
                                     And p.EMPLOYEE_ID = objWorkingAllowance.EMPLOYEE_ID _
                                     And p.ALLOWANCE_TYPE = objWorkingAllowance.ALLOWANCE_TYPE
                                 Order By p.EFFECT_DATE Descending).FirstOrDefault
                If query_old IsNot Nothing AndAlso query_old.EXP_DATE IsNot Nothing Then
                    query_old.EXP_DATE = Nothing
                End If
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function ValidateNewEdit(ByVal objWL As HUAllowanceDTO) As Boolean
        Try
            If objWL.ID <> 0 Then
                Dim check = Context.HU_ALLOWANCE.Where(Function(f) f.EMPLOYEE_ID = objWL.EMPLOYEE_ID And f.EFFECT_DATE > objWL.EFFECT_DATE And f.ID <> objWL.ID And f.ALLOWANCE_TYPE = objWL.ALLOWANCE_LIST_ID).FirstOrDefault
                If check IsNot Nothing Then
                    Return True
                Else
                    Return False
                End If
            Else
                Dim check = Context.HU_ALLOWANCE.Where(Function(f) f.EMPLOYEE_ID = objWL.EMPLOYEE_ID And f.EFFECT_DATE > objWL.EFFECT_DATE And f.ALLOWANCE_TYPE = objWL.ALLOWANCE_LIST_ID).FirstOrDefault
                If check IsNot Nothing Then
                    Return True
                Else
                    Return False
                End If
            End If
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".ValidateWorkingAllowance")
            Throw ex
        End Try
    End Function
    Public Function ModifyWorkingAllowanceNew(ByVal objWorkingAllowance As HUAllowanceDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Try
            Dim check = Context.HU_ALLOWANCE.Where(Function(f) f.EMPLOYEE_ID = objWorkingAllowance.EMPLOYEE_ID And
                                                      f.EFFECT_DATE = objWorkingAllowance.EFFECT_DATE And
                                                      f.ID <> objWorkingAllowance.ID And f.ALLOWANCE_TYPE = objWorkingAllowance.ALLOWANCE_LIST_ID
                                                      ).FirstOrDefault

            If check IsNot Nothing Then
                Return False
            End If
            Dim objWorkingAllowanceData As New HU_ALLOWANCE With {.ID = objWorkingAllowance.ID}
            'Context.HU_ALLOWANCE.Attach(objWorkingAllowanceData)
            objWorkingAllowanceData = (From p In Context.HU_ALLOWANCE Where p.ID = objWorkingAllowance.ID).FirstOrDefault
            objWorkingAllowanceData.ALLOWANCE_TYPE = objWorkingAllowance.ALLOWANCE_LIST_ID
            objWorkingAllowanceData.EMPLOYEE_ID = objWorkingAllowance.EMPLOYEE_ID
            objWorkingAllowanceData.AMOUNT = objWorkingAllowance.AMOUNT
            objWorkingAllowanceData.EFFECT_DATE = objWorkingAllowance.EFFECT_DATE
            objWorkingAllowanceData.EXP_DATE = objWorkingAllowance.EXPIRE_DATE
            objWorkingAllowanceData.FACTOR = objWorkingAllowance.FACTOR
            objWorkingAllowanceData.REMARK = objWorkingAllowance.REMARK
            objWorkingAllowanceData.FILE_NAME = objWorkingAllowance.FILE_NAME
            objWorkingAllowanceData.DETAIL_ADD = objWorkingAllowance.DETAIL_ADD
            objWorkingAllowanceData.IS_DEDUCT = objWorkingAllowance.IS_DEDUCT
            objWorkingAllowanceData.PERCENT_PIT = objWorkingAllowance.PERCENT_PIT
            objWorkingAllowanceData.AMOUNT_PIT = objWorkingAllowance.AMOUNT_PIT
            'objWorkingAllowanceData.EMPLOYEE_SIGNER_ID = objWorkingAllowance.EMPLOYEE_SIGNER_ID
            'objWorkingAllowanceData.DATE_SIGNER = objWorkingAllowance.DATE_SIGNER
            'objWorkingAllowanceData.SIGNER_NAME = objWorkingAllowance.SIGNER_NAME
            'objWorkingAllowanceData.SIGNER_LEVEL_NAME = objWorkingAllowance.SIGNER_LEVEL_NAME
            'objWorkingAllowanceData.SIGNER_TITLE_NAME = objWorkingAllowance.SIGNER_TITLE_NAME
            Context.SaveChanges(log)
            gID = objWorkingAllowanceData.ID
            Update_EXPIRE_DATE_IN_ALLOWANCE(objWorkingAllowance, log)
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".ModifyWorkingAllowance")
            Throw ex
        End Try
    End Function
    Public Function GET_EMPDTL_HU_ALLOWAMCE(ByVal _filter As HUAllowanceDTO,
                                           Optional ByVal Sorts As String = "CREATED_DATE desc",
                                           Optional ByVal log As UserLog = Nothing) As List(Of HUAllowanceDTO)

        Try

            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PROFILE_BUSINESS.GET_EMPDTL_HU_ALLOWAMCE",
                                                    New With {.P_EMPID = _filter.EMPLOYEE_ID,
                                                              .CUR = cls.OUT_CURSOR})

                Dim query As New List(Of HUAllowanceDTO)
                For Each row As DataRow In dtData.Rows
                    Dim objWA As New HUAllowanceDTO
                    If row("ID").ToString <> "" Then
                        objWA.ID = Decimal.Parse(row("ID"))
                    End If
                    If row("EMPLOYEE_ID").ToString <> "" Then
                        objWA.EMPLOYEE_ID = Decimal.Parse(row("EMPLOYEE_ID"))
                    End If
                    objWA.EMPLOYEE_CODE = IIf(IsDBNull(row("EMPLOYEE_CODE")), Nothing, row("EMPLOYEE_CODE").ToString)
                    objWA.EMPLOYEE_NAME = IIf(IsDBNull(row("EMPLOYEE_NAME")), Nothing, row("EMPLOYEE_NAME").ToString)
                    'objWA.EMPLOYEE_SIGNER_NAME = IIf(IsDBNull(row("EMPLOYEE_SIGNER_NAME")), Nothing, row("EMPLOYEE_SIGNER_NAME").ToString)
                    'If row("EMPLOYEE_SIGNER_ID").ToString <> "" Then
                    '    objWA.EMPLOYEE_SIGNER_ID = Decimal.Parse(row("EMPLOYEE_SIGNER_ID"))
                    'End If
                    If row("AMOUNT").ToString <> "" Then
                        objWA.AMOUNT = Decimal.Parse(row("AMOUNT"))
                    End If
                    objWA.ALLOWANCE_LIST_NAME = IIf(IsDBNull(row("ALLOWANCE_LIST_NAME")), Nothing, row("ALLOWANCE_LIST_NAME").ToString)
                    'objWA.DATE_SIGNER = If(IsDBNull(row("DATE_SIGNER")), Nothing, row("DATE_SIGNER"))
                    'If row("ALLOWANCE_LIST_ID").ToString <> "" Then
                    '    objWA.ALLOWANCE_LIST_ID = Decimal.Parse(row("ALLOWANCE_LIST_ID"))
                    'End If
                    'objWA.ALLOWANCE_LIST_NAME = IIf(IsDBNull(row("ALLOWANCE_LIST_NAME")), Nothing, row("ALLOWANCE_LIST_NAME").ToString)

                    If row("FACTOR").ToString <> "" Then
                        objWA.FACTOR = Decimal.Parse(row("FACTOR"))
                    End If
                    objWA.REMARK = IIf(IsDBNull(row("REMARK")), Nothing, row("REMARK").ToString)
                    objWA.ALLOWANCE_LIST_ID = Decimal.Parse(row("ALLOWANCE_TYPE"))
                    objWA.ORG_DESC = IIf(IsDBNull(row("ORG_DESC")), Nothing, row("ORG_DESC").ToString)
                    objWA.ORG_NAME1 = IIf(IsDBNull(row("ORG_NAME1")), Nothing, row("ORG_NAME1").ToString)
                    objWA.ORG_NAME2 = IIf(IsDBNull(row("ORG_NAME2")), Nothing, row("ORG_NAME2").ToString)
                    objWA.ORG_NAME3 = IIf(IsDBNull(row("ORG_NAME3")), Nothing, row("ORG_NAME3").ToString)
                    'objWA.PAYROLL = IIf(IsDBNull(row("PAYROLL")), Nothing, row("PAYROLL").ToString)
                    'objWA.STAFF_RANK_NAME = IIf(IsDBNull(row("STAFF_RANK_NAME")), Nothing, row("STAFF_RANK_NAME").ToString)
                    'objWA.TITLE_NAME_VN = IIf(IsDBNull(row("TITLE_NAME_VN")), Nothing, row("TITLE_NAME_VN").ToString)
                    'objWA.TITLE_JOB_NAME = IIf(IsDBNull(row("TITLE_JOB_NAME")), Nothing, row("TITLE_JOB_NAME").ToString)
                    'objWA.LEARNING_LEVEL_NAME = IIf(IsDBNull(row("LEARNING_LEVEL_NAME")), Nothing, row("LEARNING_LEVEL_NAME").ToString)
                    objWA.DETAIL_ADD_NAME = IIf(IsDBNull(row("DETAIL_ADD_NAME")), Nothing, row("DETAIL_ADD_NAME").ToString)
                    objWA.DETAIL_ADD = If(IsDBNull(row("DETAIL_ADD")), Nothing, row("DETAIL_ADD"))
                    objWA.EFFECT_DATE = If(IsDBNull(row("EFFECT_DATE")), Nothing, row("EFFECT_DATE"))
                    objWA.EXPIRE_DATE = If(IsDBNull(row("EXP_DATE")), Nothing, row("EXP_DATE"))
                    'objWA.EMPLOYEE_SIGNER_SR_NAME = IIf(IsDBNull(row("EMPLOYEE_SIGNER_SR_NAME")), Nothing, row("EMPLOYEE_SIGNER_SR_NAME").ToString)
                    'objWA.EMPLOYEE_SIGNER_TITLE_NAME = IIf(IsDBNull(row("EMPLOYEE_SIGNER_TITLE_NAME")), Nothing, row("EMPLOYEE_SIGNER_TITLE_NAME").ToString)
                    objWA.CREATED_DATE = If(IsDBNull(row("CREATED_DATE")), Nothing, row("CREATED_DATE"))
                    'objWA.IS_DEDUCT = If(IsDBNull(row("IS_DEDUCT")), Nothing, row("IS_DEDUCT"))
                    If Not IsDBNull(row("IS_DEDUCT")) AndAlso row("IS_DEDUCT") = -1 Then
                        objWA.IS_DEDUCT = True
                    Else
                        objWA.IS_DEDUCT = False
                    End If
                    If row("WORK_STATUS").ToString <> "" Then
                        objWA.WORK_STATUS = Decimal.Parse(row("WORK_STATUS"))
                    End If
                    'objWA.TER_EFFECT_DATE = If(IsDBNull(row("TER_EFFECT_DATE")), Nothing, row("TER_EFFECT_DATE"))
                    query.Add(objWA)
                Next
                'If _filter.ORG_DESC IsNot Nothing Then
                '    query = (From l In query
                '             Where l.ORG_DESC IsNot Nothing AndAlso l.ORG_DESC.ToUpper.Contains(_filter.ORG_DESC.ToUpper)
                '             Select l).ToList()
                'End If
                'If _filter.ORG_NAME1 IsNot Nothing Then
                '    query = (From l In query
                '             Where l.ORG_NAME1 IsNot Nothing AndAlso l.ORG_NAME1.ToUpper.Contains(_filter.ORG_NAME1.ToUpper)
                '             Select l).ToList()
                'End If
                'If _filter.ORG_NAME2 IsNot Nothing Then
                '    query = (From l In query
                '             Where l.ORG_NAME2 IsNot Nothing AndAlso l.ORG_NAME2.ToUpper.Contains(_filter.ORG_NAME2.ToUpper)
                '             Select l).ToList()
                'End If
                'If _filter.ORG_NAME3 IsNot Nothing Then
                '    query = (From l In query
                '             Where l.ORG_NAME3 IsNot Nothing AndAlso l.ORG_NAME3.ToUpper.Contains(_filter.ORG_NAME3.ToUpper)
                '             Select l).ToList()
                'End If
                'If _filter.PAYROLL IsNot Nothing Then
                '    query = (From l In query
                '             Where l.PAYROLL IsNot Nothing AndAlso l.PAYROLL.ToUpper.Contains(_filter.PAYROLL.ToUpper)
                '             Select l).ToList()
                'End If
                'If _filter.STAFF_RANK_NAME IsNot Nothing Then
                '    query = (From l In query
                '             Where l.STAFF_RANK_NAME IsNot Nothing AndAlso l.STAFF_RANK_NAME.ToUpper.Contains(_filter.STAFF_RANK_NAME.ToUpper)
                '             Select l).ToList()
                'End If
                'If _filter.TITLE_NAME_VN IsNot Nothing Then
                '    query = (From l In query
                '             Where l.TITLE_NAME_VN IsNot Nothing AndAlso l.TITLE_NAME_VN.ToUpper.Contains(_filter.TITLE_NAME_VN.ToUpper)
                '             Select l).ToList()
                'End If
                'If _filter.TITLE_JOB_NAME IsNot Nothing Then
                '    query = (From l In query
                '             Where l.TITLE_JOB_NAME IsNot Nothing AndAlso l.TITLE_JOB_NAME.ToUpper.Contains(_filter.TITLE_JOB_NAME.ToUpper)
                '             Select l).ToList()
                'End If
                'If _filter.LEARNING_LEVEL_NAME IsNot Nothing Then
                '    query = (From l In query
                '             Where l.LEARNING_LEVEL_NAME IsNot Nothing AndAlso l.LEARNING_LEVEL_NAME.ToUpper.Contains(_filter.LEARNING_LEVEL_NAME.ToUpper)
                '             Select l).ToList()
                'End If

                'Dim terID = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID
                'If _filter.EMPLOYEE_CODE IsNot Nothing Then
                '    query = (From l In query
                '             Where l.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper) _
                '          Or l.EMPLOYEE_NAME.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper)
                '             Select l).ToList()
                'End If
                'If _filter.EMPLOYEE_NAME IsNot Nothing Then
                '    query = (From l In query
                '             Where l.EMPLOYEE_NAME.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper)
                '             Select l).ToList()
                'End If
                'If _filter.AMOUNT IsNot Nothing Then
                '    query = (From l In query
                '             Where l.AMOUNT = _filter.AMOUNT
                '             Select l).ToList()
                'End If
                'If _filter.ALLOWANCE_LIST_NAME IsNot Nothing Then
                '    query = (From l In query
                '             Where l.ALLOWANCE_LIST_NAME IsNot Nothing AndAlso l.ALLOWANCE_LIST_NAME.ToUpper.Contains(_filter.ALLOWANCE_LIST_NAME.ToUpper)
                '             Select l).ToList()
                'End If
                'If _filter.FACTOR IsNot Nothing Then
                '    query = (From l In query
                '             Where l.FACTOR = _filter.FACTOR
                '             Select l).ToList()
                'End If
                'If _filter.REMARK IsNot Nothing Then
                '    query = (From l In query
                '             Where l.REMARK IsNot Nothing AndAlso l.REMARK.ToUpper.Contains(_filter.REMARK.ToUpper)
                '             Select l).ToList()
                'End If
                'If _filter.EMPLOYEE_SIGNER_NAME IsNot Nothing Then
                '    query = (From l In query
                '             Where l.EMPLOYEE_SIGNER_NAME IsNot Nothing AndAlso l.EMPLOYEE_SIGNER_NAME.ToUpper.Contains(_filter.EMPLOYEE_SIGNER_NAME.ToUpper)
                '             Select l).ToList()
                'End If
                'If _filter.EMPLOYEE_SIGNER_SR_NAME IsNot Nothing Then
                '    query = (From l In query
                '             Where l.EMPLOYEE_SIGNER_SR_NAME IsNot Nothing AndAlso l.EMPLOYEE_SIGNER_SR_NAME.ToUpper.Contains(_filter.EMPLOYEE_SIGNER_SR_NAME.ToUpper)
                '             Select l).ToList()
                'End If
                'If _filter.EMPLOYEE_SIGNER_TITLE_NAME IsNot Nothing Then
                '    query = (From l In query
                '             Where l.EMPLOYEE_SIGNER_TITLE_NAME IsNot Nothing AndAlso l.EMPLOYEE_SIGNER_TITLE_NAME.ToUpper.Contains(_filter.EMPLOYEE_SIGNER_TITLE_NAME.ToUpper)
                '             Select l).ToList()
                'End If
                'If _filter.ALLOWANCE_LIST_ID IsNot Nothing Then
                '    query = (From l In query
                '             Where l.ALLOWANCE_LIST_ID = _filter.ALLOWANCE_LIST_ID
                '             Select l).ToList()
                'End If
                'If _filter.FROM_DATE IsNot Nothing Then
                '    query = (From l In query
                '             Where l.EFFECT_DATE >= _filter.FROM_DATE
                '             Select l).ToList()
                'End If
                'If _filter.TO_DATE IsNot Nothing Then
                '    query = (From l In query
                '             Where l.EFFECT_DATE <= _filter.TO_DATE
                '             Select l).ToList()
                'End If

                'Dim dateNow = Date.Now.Date
                'If Not _filter.IS_TER Then
                '    query = (From l In query
                '             Where Not l.WORK_STATUS.HasValue Or (l.WORK_STATUS.HasValue And
                '                             ((l.WORK_STATUS <> terID) Or (l.WORK_STATUS = terID And l.TER_EFFECT_DATE > dateNow)))
                '             Select l).ToList()
                'End If

                query = (From l In query
                         Order By Sorts
                         Select l).ToList()
                'Total = query.Count
                'query = (From l In query.Skip(PageIndex * PageSize).Take(PageSize)
                '         Select l).ToList()

                Return query.ToList
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function GetWorkingAllowance1(ByVal _filter As HUAllowanceDTO,
                                        ByVal _param As ParamDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of HUAllowanceDTO)

        Try
            Dim P_IS_NEW As Integer = -1
            If _filter.IS_NEW = True Then
                P_IS_NEW = -1
            Else
                P_IS_NEW = 0
            End If

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username.ToUpper,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using

            ' Dim query As IQueryable(Of HUAllowanceDTO)
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PROFILE_BUSINESS.GET_HU_ALLOWAMCE",
                                                    New With {.P_IS_NEW = P_IS_NEW,
                                                              .P_USERNAME = log.Username.ToUpper,
                                                              .CUR = cls.OUT_CURSOR})

                Dim query As New List(Of HUAllowanceDTO)
                For Each row As DataRow In dtData.Rows
                    Dim objWA As New HUAllowanceDTO
                    If Not IsDBNull(row("ID")) AndAlso row("ID").ToString <> "" Then
                        objWA.ID = Decimal.Parse(row("ID"))
                    End If
                    If Not IsDBNull(row("EMPLOYEE_ID")) AndAlso row("EMPLOYEE_ID").ToString <> "" Then
                        objWA.EMPLOYEE_ID = Decimal.Parse(row("EMPLOYEE_ID"))
                    End If
                    objWA.EMPLOYEE_CODE = IIf(IsDBNull(row("EMPLOYEE_CODE")), "", row("EMPLOYEE_CODE").ToString)
                    objWA.EMPLOYEE_NAME = IIf(IsDBNull(row("EMPLOYEE_NAME")), "", row("EMPLOYEE_NAME").ToString)
                    'objWA.EMPLOYEE_SIGNER_NAME = IIf(IsDBNull(row("EMPLOYEE_SIGNER_NAME")), Nothing, row("EMPLOYEE_SIGNER_NAME").ToString)
                    'If row("EMPLOYEE_SIGNER_ID").ToString <> "" Then
                    '    objWA.EMPLOYEE_SIGNER_ID = Decimal.Parse(row("EMPLOYEE_SIGNER_ID"))
                    'End If
                    If Not IsDBNull(row("AMOUNT")) AndAlso row("AMOUNT").ToString <> "" Then
                        objWA.AMOUNT = Decimal.Parse(row("AMOUNT"))
                    End If

                    If Not IsDBNull(row("PERCENT_PIT")) AndAlso row("PERCENT_PIT").ToString <> "" Then
                        objWA.PERCENT_PIT = Decimal.Parse(row("PERCENT_PIT"))
                    End If

                    If Not IsDBNull(row("AMOUNT_PIT")) AndAlso row("AMOUNT_PIT").ToString <> "" Then
                        objWA.AMOUNT_PIT = Decimal.Parse(row("AMOUNT_PIT"))
                    End If
                    objWA.ALLOWANCE_LIST_NAME = IIf(IsDBNull(row("ALLOWANCE_LIST_NAME")), "", row("ALLOWANCE_LIST_NAME").ToString)
                    'objWA.DATE_SIGNER = If(IsDBNull(row("DATE_SIGNER")), Nothing, row("DATE_SIGNER"))
                    'If row("ALLOWANCE_LIST_ID").ToString <> "" Then
                    '    objWA.ALLOWANCE_LIST_ID = Decimal.Parse(row("ALLOWANCE_LIST_ID"))
                    'End If
                    'objWA.ALLOWANCE_LIST_NAME = IIf(IsDBNull(row("ALLOWANCE_LIST_NAME")), Nothing, row("ALLOWANCE_LIST_NAME").ToString)

                    If Not IsDBNull(row("FACTOR")) AndAlso row("FACTOR").ToString <> "" Then
                        objWA.FACTOR = Decimal.Parse(row("FACTOR"))
                    End If
                    objWA.REMARK = IIf(IsDBNull(row("REMARK")), "", row("REMARK").ToString)
                    objWA.FILE_NAME = IIf(IsDBNull(row("FILE_NAME")), "", row("FILE_NAME").ToString)
                    objWA.UPLOAD_FILE = IIf(IsDBNull(row("UPLOAD_FILE")), "", row("UPLOAD_FILE").ToString)
                    objWA.ALLOWANCE_LIST_ID = If(IsDBNull(row("ALLOWANCE_TYPE")), Nothing, Decimal.Parse(row("ALLOWANCE_TYPE")))
                    objWA.ORG_DESC = IIf(IsDBNull(row("ORG_DESC")), "", row("ORG_DESC").ToString)
                    objWA.ORG_NAME1 = IIf(IsDBNull(row("ORG_NAME1")), "", row("ORG_NAME1").ToString)
                    objWA.ORG_NAME2 = IIf(IsDBNull(row("ORG_NAME2")), "", row("ORG_NAME2").ToString)
                    objWA.ORG_NAME3 = IIf(IsDBNull(row("ORG_NAME3")), "", row("ORG_NAME3").ToString)
                    'objWA.PAYROLL = IIf(IsDBNull(row("PAYROLL")), Nothing, row("PAYROLL").ToString)
                    'objWA.STAFF_RANK_NAME = IIf(IsDBNull(row("STAFF_RANK_NAME")), Nothing, row("STAFF_RANK_NAME").ToString)
                    'objWA.TITLE_NAME_VN = IIf(IsDBNull(row("TITLE_NAME_VN")), Nothing, row("TITLE_NAME_VN").ToString)
                    'objWA.TITLE_JOB_NAME = IIf(IsDBNull(row("TITLE_JOB_NAME")), Nothing, row("TITLE_JOB_NAME").ToString)
                    'objWA.LEARNING_LEVEL_NAME = IIf(IsDBNull(row("LEARNING_LEVEL_NAME")), Nothing, row("LEARNING_LEVEL_NAME").ToString)
                    objWA.DETAIL_ADD_NAME = IIf(IsDBNull(row("DETAIL_ADD_NAME")), "", row("DETAIL_ADD_NAME").ToString)
                    objWA.DETAIL_ADD = If(IsDBNull(row("DETAIL_ADD")), Nothing, row("DETAIL_ADD"))
                    objWA.EFFECT_DATE = If(IsDBNull(row("EFFECT_DATE")), Nothing, row("EFFECT_DATE"))
                    objWA.EXPIRE_DATE = If(IsDBNull(row("EXP_DATE")), Nothing, row("EXP_DATE"))
                    'objWA.EMPLOYEE_SIGNER_SR_NAME = IIf(IsDBNull(row("EMPLOYEE_SIGNER_SR_NAME")), Nothing, row("EMPLOYEE_SIGNER_SR_NAME").ToString)
                    'objWA.EMPLOYEE_SIGNER_TITLE_NAME = IIf(IsDBNull(row("EMPLOYEE_SIGNER_TITLE_NAME")), Nothing, row("EMPLOYEE_SIGNER_TITLE_NAME").ToString)
                    objWA.CREATED_DATE = If(IsDBNull(row("CREATED_DATE")), Nothing, row("CREATED_DATE"))
                    'objWA.IS_DEDUCT = If(IsDBNull(row("IS_DEDUCT")), Nothing, row("IS_DEDUCT"))
                    If Not IsDBNull(row("IS_DEDUCT")) AndAlso row("IS_DEDUCT") = -1 Then
                        objWA.IS_DEDUCT = True
                    Else
                        objWA.IS_DEDUCT = False
                    End If
                    If Not IsDBNull(row("WORK_STATUS")) AndAlso row("WORK_STATUS").ToString <> "" Then
                        objWA.WORK_STATUS = Decimal.Parse(row("WORK_STATUS"))
                    End If
                    'objWA.TER_EFFECT_DATE = If(IsDBNull(row("TER_EFFECT_DATE")), Nothing, row("TER_EFFECT_DATE"))
                    query.Add(objWA)
                Next
                If _filter.ORG_DESC IsNot Nothing Then
                    query = (From l In query
                             Where l.ORG_DESC IsNot Nothing AndAlso l.ORG_DESC.ToUpper.Contains(_filter.ORG_DESC.ToUpper)
                             Select l).ToList()
                End If
                If _filter.ORG_NAME1 IsNot Nothing Then
                    query = (From l In query
                             Where l.ORG_NAME1 IsNot Nothing AndAlso l.ORG_NAME1.ToUpper.Contains(_filter.ORG_NAME1.ToUpper)
                             Select l).ToList()
                End If
                If _filter.ORG_NAME2 IsNot Nothing Then
                    query = (From l In query
                             Where l.ORG_NAME2 IsNot Nothing AndAlso l.ORG_NAME2.ToUpper.Contains(_filter.ORG_NAME2.ToUpper)
                             Select l).ToList()
                End If

                If _filter.EMPLOYEE_ID.HasValue Then
                    query = (From l In query
                             Where l.EMPLOYEE_ID = _filter.EMPLOYEE_ID
                             Select l).ToList()
                End If

                If _filter.ORG_NAME3 IsNot Nothing Then
                    query = (From l In query
                             Where l.ORG_NAME3 IsNot Nothing AndAlso l.ORG_NAME3.ToUpper.Contains(_filter.ORG_NAME3.ToUpper)
                             Select l).ToList()
                End If
                If _filter.PAYROLL IsNot Nothing Then
                    query = (From l In query
                             Where l.PAYROLL IsNot Nothing AndAlso l.PAYROLL.ToUpper.Contains(_filter.PAYROLL.ToUpper)
                             Select l).ToList()
                End If
                If _filter.STAFF_RANK_NAME IsNot Nothing Then
                    query = (From l In query
                             Where l.STAFF_RANK_NAME IsNot Nothing AndAlso l.STAFF_RANK_NAME.ToUpper.Contains(_filter.STAFF_RANK_NAME.ToUpper)
                             Select l).ToList()
                End If
                If _filter.TITLE_NAME_VN IsNot Nothing Then
                    query = (From l In query
                             Where l.TITLE_NAME_VN IsNot Nothing AndAlso l.TITLE_NAME_VN.ToUpper.Contains(_filter.TITLE_NAME_VN.ToUpper)
                             Select l).ToList()
                End If
                If _filter.TITLE_JOB_NAME IsNot Nothing Then
                    query = (From l In query
                             Where l.TITLE_JOB_NAME IsNot Nothing AndAlso l.TITLE_JOB_NAME.ToUpper.Contains(_filter.TITLE_JOB_NAME.ToUpper)
                             Select l).ToList()
                End If
                If _filter.LEARNING_LEVEL_NAME IsNot Nothing Then
                    query = (From l In query
                             Where l.LEARNING_LEVEL_NAME IsNot Nothing AndAlso l.LEARNING_LEVEL_NAME.ToUpper.Contains(_filter.LEARNING_LEVEL_NAME.ToUpper)
                             Select l).ToList()
                End If

                Dim terID = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID
                If _filter.EMPLOYEE_CODE IsNot Nothing Then
                    query = (From l In query
                             Where l.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper) _
                             Or l.EMPLOYEE_NAME.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper)
                             Select l).ToList()
                End If
                If _filter.EMPLOYEE_ID IsNot Nothing Then
                    query = (From l In query
                             Where l.EMPLOYEE_ID IsNot Nothing AndAlso l.EMPLOYEE_ID = _filter.EMPLOYEE_ID
                             Select l).ToList()
                End If
                If _filter.EMPLOYEE_NAME IsNot Nothing Then
                    query = (From l In query
                             Where l.EMPLOYEE_NAME.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper)
                             Select l).ToList()
                End If
                If _filter.AMOUNT IsNot Nothing Then
                    query = (From l In query
                             Where l.AMOUNT = _filter.AMOUNT
                             Select l).ToList()
                End If
                If _filter.ALLOWANCE_LIST_NAME IsNot Nothing Then
                    query = (From l In query
                             Where l.ALLOWANCE_LIST_NAME IsNot Nothing AndAlso l.ALLOWANCE_LIST_NAME.ToUpper.Contains(_filter.ALLOWANCE_LIST_NAME.ToUpper)
                             Select l).ToList()
                End If
                If _filter.FACTOR IsNot Nothing Then
                    query = (From l In query
                             Where l.FACTOR = _filter.FACTOR
                             Select l).ToList()
                End If
                If _filter.REMARK IsNot Nothing Then
                    query = (From l In query
                             Where l.REMARK IsNot Nothing AndAlso l.REMARK.ToUpper.Contains(_filter.REMARK.ToUpper)
                             Select l).ToList()
                End If
                If _filter.EMPLOYEE_SIGNER_NAME IsNot Nothing Then
                    query = (From l In query
                             Where l.EMPLOYEE_SIGNER_NAME IsNot Nothing AndAlso l.EMPLOYEE_SIGNER_NAME.ToUpper.Contains(_filter.EMPLOYEE_SIGNER_NAME.ToUpper)
                             Select l).ToList()
                End If
                If _filter.EMPLOYEE_SIGNER_SR_NAME IsNot Nothing Then
                    query = (From l In query
                             Where l.EMPLOYEE_SIGNER_SR_NAME IsNot Nothing AndAlso l.EMPLOYEE_SIGNER_SR_NAME.ToUpper.Contains(_filter.EMPLOYEE_SIGNER_SR_NAME.ToUpper)
                             Select l).ToList()
                End If
                If _filter.EMPLOYEE_SIGNER_TITLE_NAME IsNot Nothing Then
                    query = (From l In query
                             Where l.EMPLOYEE_SIGNER_TITLE_NAME IsNot Nothing AndAlso l.EMPLOYEE_SIGNER_TITLE_NAME.ToUpper.Contains(_filter.EMPLOYEE_SIGNER_TITLE_NAME.ToUpper)
                             Select l).ToList()
                End If
                If _filter.ALLOWANCE_LIST_ID IsNot Nothing Then
                    query = (From l In query
                             Where l.ALLOWANCE_LIST_ID = _filter.ALLOWANCE_LIST_ID
                             Select l).ToList()
                End If
                If _filter.FROM_DATE IsNot Nothing Then
                    query = (From l In query
                             Where l.EFFECT_DATE >= _filter.FROM_DATE
                             Select l).ToList()
                End If
                If _filter.TO_DATE IsNot Nothing Then
                    query = (From l In query
                             Where l.EFFECT_DATE <= _filter.TO_DATE
                             Select l).ToList()
                End If

                If _filter.PERCENT_PIT.HasValue Then
                    query = (From p In query Where p.PERCENT_PIT = _filter.PERCENT_PIT)
                End If

                If _filter.AMOUNT_PIT.HasValue Then
                    query = (From p In query Where p.AMOUNT_PIT = _filter.AMOUNT_PIT)
                End If
                Dim dateNow = Date.Now.Date
                If Not _filter.IS_TER Then
                    query = (From l In query
                             Where Not l.WORK_STATUS.HasValue Or (l.WORK_STATUS.HasValue And _
                                                ((l.WORK_STATUS <> terID) Or (l.WORK_STATUS = terID And l.TER_EFFECT_DATE > dateNow)))
                             Select l).ToList()
                End If

                query = (From l In query
                         Order By Sorts
                         Select l).ToList()
                Total = query.Count
                query = (From l In query.Skip(PageIndex * PageSize).Take(PageSize)
                         Select l).ToList()

                Return query.ToList
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function GetWorking_1(ByVal _filter As WorkingDTO,
                               ByVal PageIndex As Integer,
                               ByVal PageSize As Integer,
                               ByRef Total As Integer, ByVal _param As ParamDTO,
                               Optional ByVal Sorts As String = "CREATED_DATE desc",
                               Optional ByVal log As UserLog = Nothing) As List(Of WorkingDTO)

        Try
            Dim terID = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using

            Dim query = From p In Context.HU_WORKING
                        From o1 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.EMPLOYEE_TYPE).DefaultIfEmpty
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
                        From sale_commision In Context.PA_SALE_COMMISION.Where(Function(f) f.ID = p.SALE_COMMISION_ID).DefaultIfEmpty
                        From sal_type In Context.PA_SALARY_TYPE.Where(Function(f) p.SAL_TYPE_ID = f.ID).DefaultIfEmpty
                        From sal_group In Context.PA_SALARY_GROUP.Where(Function(f) p.SAL_GROUP_ID = f.ID).DefaultIfEmpty
                        From sal_level In Context.PA_SALARY_LEVEL.Where(Function(f) p.SAL_LEVEL_ID = f.ID).DefaultIfEmpty
                        From sal_rank In Context.PA_SALARY_RANK.Where(Function(f) p.SAL_RANK_ID = f.ID).DefaultIfEmpty
                        From status In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.STATUS_ID).DefaultIfEmpty
                        From deci_type In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.DECISION_TYPE_ID).DefaultIfEmpty
                        From obj_att In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.OBJECT_ATTENDANCE).DefaultIfEmpty
                        From obj_att_type In Context.OT_OTHER_LIST_TYPE.Where(Function(f) f.ID = obj_att.TYPE_ID).DefaultIfEmpty
                        From staffrank In Context.HU_STAFF_RANK.Where(Function(f) f.ID = p.STAFF_RANK_ID).DefaultIfEmpty
                        From taxTable In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TAX_TABLE_ID).DefaultIfEmpty
                        From direct In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.DIRECT_MANAGER).DefaultIfEmpty
                        From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = e.ORG_ID And
                                                           f.USERNAME = log.Username.ToUpper)
                        From objectLabor In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.OBJECT_LABOR And
                                                                        f.TYPE_ID = 6963).DefaultIfEmpty
                        Where p.STATUS_ID = 447 And p.EFFECT_DATE <= If(_filter.EFFECT_DATE IsNot Nothing, _filter.EFFECT_DATE, DateTime.Now)
                        Select New WorkingDTO With {.ID = p.ID,
                            .GAS_SAL = p.GAS_SAL,
                            .PHONE_SAL = p.PHONE_SAL,
                            .ADDITIONAL_SAL = p.ADDITIONAL_SAL,
                            .OTHERSALARY1 = p.OTHERSALARY1,
                            .PC1 = p.PC1,
                            .PC2 = p.PC2,
                            .PC3 = p.PC3,
                            .PC4 = p.PC4,
                            .PC5 = p.PC5,
                                                    .DECISION_NO = p.DECISION_NO,
                                                    .EMPLOYEE_TYPE = p.EMPLOYEE_TYPE,
                                                    .EMPLOYEE_TYPE_NAME = o1.NAME_VN,
                                                    .SALARY_BHXH = p.SALARY_BHXH,
                                                    .DECISION_TYPE_ID = p.DECISION_TYPE_ID,
                                                    .DECISION_TYPE_NAME = deci_type.NAME_VN,
                                                    .CODE = deci_type.CODE,
                                                    .EMPLOYEE_ID = p.EMPLOYEE_ID,
                                                    .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                                                    .EMPLOYEE_NAME = e.FULLNAME_VN,
                                                    .ORG_NAME = o.NAME_VN,
                                                    .SALE_COMMISION_ID = p.SALE_COMMISION_ID,
                                                    .SALE_COMMISION_NAME = sale_commision.NAME,
                                                    .ORG_DESC = o.DESCRIPTION_PATH,
                                                    .TITLE_NAME = t.NAME_VN,
                                                    .EFFECT_DATE = p.EFFECT_DATE,
                                                    .EXPIRE_DATE = p.EXPIRE_DATE,
                                                    .STATUS_ID = p.STATUS_ID,
                                                    .STATUS_NAME = status.NAME_VN,
                                                    .STAFF_RANK_ID = p.STAFF_RANK_ID,
                                                    .STAFF_RANK_NAME = staffrank.NAME,
                                                    .SAL_BASIC = p.SAL_BASIC,
                                                    .IS_MISSION = p.IS_MISSION,
                                                    .IS_WAGE = p.IS_WAGE,
                                                    .IS_PROCESS = p.IS_PROCESS,
                                                    .IS_3B = p.IS_3B,
                                                    .IS_MISSION_SHORT = p.IS_MISSION,
                                                    .IS_WAGE_SHORT = p.IS_WAGE,
                                                    .IS_PROCESS_SHORT = p.IS_PROCESS,
                                                    .IS_3B_SHORT = p.IS_3B,
                                                    .SAL_GROUP_ID = p.SAL_GROUP_ID,
                                                    .SAL_GROUP_NAME = sal_group.NAME,
                                                    .SAL_TYPE_ID = p.SAL_TYPE_ID,
                                                    .SAL_TYPE_NAME = sal_type.NAME,
                                                    .SAL_LEVEL_ID = p.SAL_LEVEL_ID,
                                                    .SAL_LEVEL_NAME = sal_level.NAME,
                                                    .SAL_RANK_ID = p.SAL_RANK_ID,
                                                    .SAL_RANK_NAME = sal_rank.RANK,
                                                    .COST_SUPPORT = p.COST_SUPPORT,
                                                    .PERCENT_SALARY = p.PERCENT_SALARY,
                                                    .PERCENTSALARY = p.PERCENTSALARY,
                                                    .SAL_TOTAL = p.SAL_TOTAL + If((From a In Context.HU_WORKING_ALLOW.Where(Function(f) f.HU_WORKING_ID = p.ID) Select a.AMOUNT).Sum Is Nothing, 0, (From a In Context.HU_WORKING_ALLOW.Where(Function(f) f.HU_WORKING_ID = p.ID) Select a.AMOUNT).Sum),
                                                    .EMPLOYEE_3B_ID = p.EMPLOYEE_3B_ID,
                                                    .WORK_STATUS = e.WORK_STATUS,
                                                    .TER_EFFECT_DATE = e.TER_EFFECT_DATE,
                                                    .CREATED_DATE = p.CREATED_DATE,
                                                    .SAL_INS = p.SAL_INS,
                                                    .ALLOWANCE_TOTAL = p.ALLOWANCE_TOTAL,
                                                    .TAX_TABLE_ID = p.TAX_TABLE_ID,
                                                    .TAX_TABLE_Name = taxTable.NAME_VN,
                                                    .OBJECT_ATTENDANCE_NAME = obj_att.NAME_VN,
                                                    .FILING_DATE = p.FILING_DATE,
                                                    .SIGN_DATE = p.SIGN_DATE,
                                                    .SIGN_NAME = p.SIGN_NAME,
                                                    .SIGN_TITLE = p.SIGN_TITLE,
                                                    .REMARK = p.REMARK,
                                                    .DIRECT_MANAGER_NAME = direct.FULLNAME_VN,
                                                    .OTHERSALARY2 = p.OTHERSALARY2,
                                                    .FACTORSALARY = p.FACTORSALARY,
                                                    .OBJECT_LABOR = p.OBJECT_LABOR,
                                                    .OBJECT_LABORNAME = objectLabor.NAME_VN,
                                                    .OTHERSALARY3 = p.OTHERSALARY3
                                                    }
            Dim dateNow = Date.Now.Date
            If Not _filter.IS_TER Then
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

            If _filter.ORG_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If

            If _filter.TITLE_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.TITLE_NAME.ToUpper.Contains(_filter.TITLE_NAME.ToUpper))
            End If

            If _filter.EMPLOYEE_ID <> 0 Then
                query = query.Where(Function(p) p.EMPLOYEE_ID = _filter.EMPLOYEE_ID)
            End If

            If _filter.DECISION_TYPE_ID IsNot Nothing Then
                query = query.Where(Function(p) p.DECISION_TYPE_ID = _filter.DECISION_TYPE_ID)
            End If

            If _filter.DECISION_TYPE_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.DECISION_TYPE_NAME.ToUpper.Contains(_filter.DECISION_TYPE_NAME.ToUpper))
            End If

            If _filter.IS_MISSION IsNot Nothing Then
                query = query.Where(Function(p) p.IS_MISSION_SHORT = _filter.IS_MISSION)
            End If

            If _filter.IS_PROCESS IsNot Nothing Then
                query = query.Where(Function(p) p.IS_PROCESS_SHORT = _filter.IS_PROCESS)
            End If

            If _filter.IS_WAGE IsNot Nothing Then
                query = query.Where(Function(p) p.IS_WAGE_SHORT = _filter.IS_WAGE)
            End If

            If _filter.IS_3B IsNot Nothing Then
                query = query.Where(Function(p) p.IS_3B_SHORT = _filter.IS_3B)
            End If

            'If _filter.EFFECT_DATE IsNot Nothing Then
            '    query = query.Where(Function(p) p.EFFECT_DATE = _filter.EFFECT_DATE)
            'End If

            If _filter.FROM_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.EFFECT_DATE >= _filter.FROM_DATE)
            End If

            If _filter.TO_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.EFFECT_DATE <= _filter.TO_DATE)
            End If

            If _filter.STATUS_ID IsNot Nothing Then
                query = query.Where(Function(p) p.STATUS_ID = _filter.STATUS_ID)
            End If

            If _filter.SAL_GROUP_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.SAL_GROUP_NAME.ToUpper.Contains(_filter.SAL_GROUP_NAME.ToUpper))
            End If
            If _filter.SAL_TYPE_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.SAL_TYPE_NAME.ToUpper.Contains(_filter.SAL_TYPE_NAME.ToUpper))
            End If

            If _filter.SAL_LEVEL_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.SAL_LEVEL_NAME.ToUpper.Contains(_filter.SAL_LEVEL_NAME.ToUpper))
            End If

            If _filter.SAL_RANK_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.SAL_RANK_NAME.ToUpper.Contains(_filter.SAL_RANK_NAME.ToUpper))
            End If

            If _filter.SAL_BASIC IsNot Nothing Then
                query = query.Where(Function(p) p.SAL_BASIC = _filter.SAL_BASIC)
            End If

            If _filter.COST_SUPPORT IsNot Nothing Then
                query = query.Where(Function(p) p.COST_SUPPORT = _filter.COST_SUPPORT)
            End If

            If _filter.SAL_TOTAL IsNot Nothing Then
                query = query.Where(Function(p) p.COST_SUPPORT = _filter.SAL_TOTAL)
            End If
            If _filter.FACTORSALARY IsNot Nothing Then
                query = query.Where(Function(p) p.FACTORSALARY = _filter.FACTORSALARY)
            End If
            If _filter.STAFF_RANK_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.STAFF_RANK_NAME.ToUpper.Contains(_filter.STAFF_RANK_NAME.ToUpper))
            End If
            If _filter.STATUS_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.STATUS_NAME.ToUpper.Contains(_filter.STATUS_NAME.ToUpper))
            End If
            If _filter.DIRECT_MANAGER_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.DIRECT_MANAGER_NAME.ToUpper.Contains(_filter.DIRECT_MANAGER_NAME.ToUpper))
            End If
            If _filter.OTHERSALARY1 IsNot Nothing Then
                query = query.Where(Function(p) p.OTHERSALARY1 = _filter.OTHERSALARY1)
            End If
            If _filter.OTHERSALARY2 IsNot Nothing Then
                query = query.Where(Function(p) p.OTHERSALARY2 = _filter.OTHERSALARY2)
            End If
            If _filter.Ids IsNot Nothing Then
                If _filter.Ids.Any() Then
                    query = query.Where(Function(p) _filter.Ids.Contains(p.ID))
                End If
            End If
            ' danh sách thông tin quá trình công tác
            Dim working = query

            working = working.OrderBy(Sorts)
            Total = working.Count
            working = working.Skip(PageIndex * PageSize).Take(PageSize)
            Dim result = working.ToList
            Dim workingIds = From p In result
                             Select p.ID
            Dim allowListIds As Integer() = New Integer() {1, 2, 3, 4, 5}
            Dim workingAllows = (From p In Context.HU_WORKING_ALLOW
                                 Where workingIds.Contains(p.HU_WORKING_ID) And allowListIds.Contains(p.ALLOWANCE_LIST_ID)
                                 Select New WorkingAllowView With {.HU_WORKING_ID = p.HU_WORKING_ID,
                                     .ALLOWANCE_LIST_ID = p.ALLOWANCE_LIST_ID,
                                     .AMOUNT = p.AMOUNT}).ToList()
            result = GetAllowanceInfos(result, workingAllows)

            For Each workOld As WorkingDTO In result
                Dim effectDate = workOld.EFFECT_DATE
                Dim empId = workOld.EMPLOYEE_ID
                Dim rowQuery = (From p In Context.HU_WORKING
                                Where p.EFFECT_DATE < effectDate _
                                And p.EMPLOYEE_ID = empId _
                                And p.IS_MISSION = -1 _
                                Order By p.EFFECT_DATE Descending).FirstOrDefault 'And p.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID _

                If rowQuery IsNot Nothing Then
                    Dim working_old = (From p In Context.HU_WORKING
                                       From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                                       From o In Context.HU_ORGANIZATION.Where(Function(f) p.ORG_ID = f.ID)
                                       From t In Context.HU_TITLE.Where(Function(f) p.TITLE_ID = f.ID)
                                       From sal_type In Context.PA_SALARY_TYPE.Where(Function(f) p.SAL_TYPE_ID = f.ID).DefaultIfEmpty
                                       From titlegroup In Context.OT_OTHER_LIST.Where(Function(f) t.TITLE_GROUP_ID = f.ID).DefaultIfEmpty
                                       From deci_type In Context.OT_OTHER_LIST.Where(Function(f) p.DECISION_TYPE_ID = f.ID).DefaultIfEmpty
                                       From staffrank In Context.HU_STAFF_RANK.Where(Function(f) p.STAFF_RANK_ID = f.ID).DefaultIfEmpty
                                       From sal_group In Context.PA_SALARY_GROUP.Where(Function(f) p.SAL_GROUP_ID = f.ID).DefaultIfEmpty
                                       From sal_level In Context.PA_SALARY_LEVEL.Where(Function(f) p.SAL_LEVEL_ID = f.ID).DefaultIfEmpty
                                       From sal_rank In Context.PA_SALARY_RANK.Where(Function(f) p.SAL_RANK_ID = f.ID).DefaultIfEmpty
                                       From taxTable In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TAX_TABLE_ID).DefaultIfEmpty
                                       From oldDirect In Context.HU_EMPLOYEE.Where(Function(f) p.DIRECT_MANAGER = f.ID).DefaultIfEmpty
                                       From OBJ_ATT In Context.OT_OTHER_LIST.Where(Function(F) F.ID = p.OBJECT_ATTENDANCE).DefaultIfEmpty
                                       Where (p.ID = rowQuery.ID)
                                       Select New WorkingDTO With {
                                                          .ID = p.ID,
                                                          .DECISION_NO = p.DECISION_NO,
                                                          .FILING_DATE = p.FILING_DATE,
                                                          .OBJECT_ATTENDANCE = p.OBJECT_ATTENDANCE,
                                                          .OBJECT_ATTENDANCE_NAME = OBJ_ATT.NAME_VN,
                                                          .DECISION_TYPE_ID = p.DECISION_TYPE_ID,
                                                          .DECISION_TYPE_NAME = deci_type.NAME_VN,
                                                          .EFFECT_DATE = p.EFFECT_DATE,
                                                          .EXPIRE_DATE = p.EXPIRE_DATE,
                                                          .ORG_ID = p.ORG_ID,
                                                          .ORG_NAME = o.NAME_VN,
                                                          .TITLE_ID = p.TITLE_ID,
                                                          .TITLE_NAME = t.NAME_VN,
                                                          .SIGN_DATE = p.SIGN_DATE,
                                                          .SIGN_NAME = p.SIGN_NAME,
                                                          .SIGN_TITLE = p.SIGN_TITLE,
                                                          .DIRECT_MANAGER_NAME = oldDirect.FULLNAME_VN}).FirstOrDefault
                    workOld.ORG_NAME_OLD = working_old.ORG_NAME
                    workOld.TITLE_NAME_OLD = working_old.TITLE_NAME
                    workOld.OBJECT_ATTENDANCE_NAME_OLD = working_old.OBJECT_ATTENDANCE_NAME
                    workOld.FILING_DATE_OLD = working_old.FILING_DATE
                    workOld.DECISION_TYPE_NAME_OLD = working_old.DECISION_TYPE_NAME
                    workOld.EFFECT_DATE_OLD = working_old.EFFECT_DATE
                    workOld.EXPIRE_DATE_OLD = working_old.EXPIRE_DATE
                    workOld.DECISION_NO_OLD = working_old.DECISION_NO
                    workOld.SIGN_DATE_OLD = working_old.SIGN_DATE
                    workOld.SIGN_NAME_OLD = working_old.SIGN_NAME
                    workOld.SIGN_TITLE_OLD = working_old.SIGN_TITLE
                    workOld.REMARK_OLD = working_old.REMARK
                    workOld.DIRECT_MANAGER_NAME_OLD = working_old.DIRECT_MANAGER_NAME
                End If
            Next

            Return result
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetWorking(ByVal _filter As WorkingDTO,
                               ByVal PageIndex As Integer,
                               ByVal PageSize As Integer,
                               ByRef Total As Integer, ByVal _param As ParamDTO,
                               Optional ByVal Sorts As String = "CREATED_DATE desc",
                               Optional ByVal log As UserLog = Nothing) As List(Of WorkingDTO)

        Try
            Dim terID = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using

            Dim query = From p In Context.HU_WORKING
                        From o1 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.EMPLOYEE_TYPE).DefaultIfEmpty
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID).DefaultIfEmpty
                        From o In Context.HUV_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
                        From sale_commision In Context.PA_SALE_COMMISION.Where(Function(f) f.ID = p.SALE_COMMISION_ID).DefaultIfEmpty
                        From sal_type In Context.PA_SALARY_TYPE.Where(Function(f) p.SAL_TYPE_ID = f.ID).DefaultIfEmpty
                        From sal_group In Context.PA_SALARY_GROUP.Where(Function(f) p.SAL_GROUP_ID = f.ID).DefaultIfEmpty
                        From sal_rank In Context.PA_FRAME_SALARY.Where(Function(f) p.SAL_RANK_ID = f.ID).DefaultIfEmpty
                        From sal_level In Context.PA_FRAME_SALARY.Where(Function(f) sal_rank.CODE_NGACHLUONG.ToUpper.Equals(f.CODE.ToUpper)).DefaultIfEmpty
                        From status In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.STATUS_ID).DefaultIfEmpty
                        From deci_type In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.DECISION_TYPE_ID).DefaultIfEmpty
                        From obj_emp In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.OBJECT_EMPLOYEE_ID).DefaultIfEmpty
                        From obj_attendant In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.OBJECT_ATTENDANT_ID).DefaultIfEmpty
                        From obj_att In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.OBJECT_ATTENDANCE).DefaultIfEmpty
                        From obj_att_type In Context.OT_OTHER_LIST_TYPE.Where(Function(f) f.ID = obj_att.TYPE_ID).DefaultIfEmpty
                        From staffrank In Context.HU_STAFF_RANK.Where(Function(f) f.ID = p.STAFF_RANK_ID).DefaultIfEmpty
                        From taxTable In Context.PA_FRAME_SALARY.Where(Function(f) sal_rank.PARENT_ID = f.ID AndAlso f.IS_LEVEL2 = -1).DefaultIfEmpty
                        From direct In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.DIRECT_MANAGER).DefaultIfEmpty
                        From wp In Context.HU_WORK_PLACE.Where(Function(f) f.ID = p.WORK_PLACE_ID).DefaultIfEmpty
                        From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = e.ORG_ID And
                                                           f.USERNAME = log.Username.ToUpper)
                        From objectLabor In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.OBJECT_LABOR And
                                                                        f.TYPE_ID = 6963).DefaultIfEmpty
                        From salPayment In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.SAL_PAYMENT_ID).DefaultIfEmpty
                        Select New WorkingDTO With {.ID = p.ID,
                                        .DECISION_NO = p.DECISION_NO,
                                        .EMPLOYEE_TYPE = p.EMPLOYEE_TYPE,
                                        .EMPLOYEE_TYPE_NAME = o1.NAME_VN,
                                        .SALARY_BHXH = p.SALARY_BHXH,
                                        .DECISION_TYPE_ID = p.DECISION_TYPE_ID,
                                        .DECISION_TYPE_NAME = deci_type.NAME_VN,
                                        .SAL_PAYMENT_ID = p.SAL_PAYMENT_ID,
                                        .SAL_PAYMENT_NAME = salPayment.NAME_VN,
                                        .DECISION_TYPE_CODE = deci_type.CODE,
                                        .CODE = deci_type.CODE,
                                        .EMPLOYEE_ID = p.EMPLOYEE_ID,
                                        .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                                        .EMPLOYEE_NAME = e.FULLNAME_VN,
                                        .ORG_NAME = o.NAME_VN,
                                        .ORG_NAME2 = o.ORG_NAME2,
                                        .ORG_NAME3 = o.ORG_NAME3,
                                        .ORG_NAME4 = o.ORG_NAME4,
                                        .ORG_NAME5 = o.ORG_NAME5,
                                        .ORG_NAME6 = o.ORG_NAME6,
                                        .ORG_NAME7 = o.ORG_NAME7,
                                        .SALE_COMMISION_ID = p.SALE_COMMISION_ID,
                                        .SALE_COMMISION_NAME = sale_commision.NAME,
                                        .ORG_DESC = o.DESCRIPTION_PATH,
                                        .TITLE_NAME = t.CODE + " - " + t.NAME_VN,
                                        .EFFECT_DATE = p.EFFECT_DATE,
                                        .EXPIRE_DATE = p.EXPIRE_DATE,
                                        .STATUS_ID = p.STATUS_ID,
                                        .STATUS_NAME = status.NAME_VN,
                                        .STAFF_RANK_ID = p.STAFF_RANK_ID,
                                        .STAFF_RANK_NAME = staffrank.NAME,
                                        .SAL_BASIC = p.SAL_BASIC,
                                        .IS_MISSION = p.IS_MISSION,
                                        .IS_WAGE = p.IS_WAGE,
                                        .IS_PROCESS = p.IS_PROCESS,
                                        .IS_3B = p.IS_3B,
                                        .IS_MISSION_SHORT = p.IS_MISSION,
                                        .IS_WAGE_SHORT = p.IS_WAGE,
                                        .IS_PROCESS_SHORT = p.IS_PROCESS,
                                        .IS_3B_SHORT = p.IS_3B,
                                        .SAL_GROUP_ID = p.SAL_GROUP_ID,
                                        .SAL_GROUP_NAME = sal_group.NAME,
                                        .SAL_TYPE_ID = p.SAL_TYPE_ID,
                                        .SAL_TYPE_NAME = sal_type.NAME,
                                        .SAL_LEVEL_ID = sal_level.ID,
                                        .SAL_LEVEL_NAME = sal_level.NAME_VN,
                                        .SAL_RANK_ID = p.SAL_RANK_ID,
                                        .SAL_RANK_NAME = sal_rank.NAME_VN,
                                        .COST_SUPPORT = p.COST_SUPPORT,
                                        .PERCENT_SALARY = p.PERCENT_SALARY,
                                        .PERCENTSALARY = p.PERCENTSALARY,
                                        .SAL_TOTAL = p.SAL_TOTAL + If((From a In Context.HU_WORKING_ALLOW.Where(Function(f) f.HU_WORKING_ID = p.ID) Select a.AMOUNT).Sum Is Nothing, 0, (From a In Context.HU_WORKING_ALLOW.Where(Function(f) f.HU_WORKING_ID = p.ID) Select a.AMOUNT).Sum),
                                        .EMPLOYEE_3B_ID = p.EMPLOYEE_3B_ID,
                                        .WORK_STATUS = e.WORK_STATUS,
                                        .TER_EFFECT_DATE = e.TER_EFFECT_DATE,
                                        .CREATED_DATE = p.CREATED_DATE,
                                        .SAL_INS = p.SAL_INS,
                                        .ALLOWANCE_TOTAL = p.ALLOWANCE_TOTAL,
                                        .TAX_TABLE_ID = p.TAX_TABLE_ID,
                                        .TAX_TABLE_Name = taxTable.NAME_VN,
                                        .OBJECT_ATTENDANCE_NAME = obj_att.NAME_VN,
                                        .OBJECT_EMPLOYEE_NAME = obj_emp.NAME_VN,
                                        .OBJECT_ATTENDANT_NAME = obj_attendant.NAME_VN,
                                        .WORK_PLACE_NAME = wp.NAME_VN,
                                        .FILING_DATE = p.FILING_DATE,
                                        .SIGN_DATE = p.SIGN_DATE,
                                        .SIGN_NAME = p.SIGN_NAME,
                                        .SIGN_TITLE = p.SIGN_TITLE,
                                        .REMARK = p.REMARK,
                                        .DIRECT_MANAGER_NAME = direct.EMPLOYEE_CODE + " - " + direct.FULLNAME_VN,
                                        .OTHERSALARY2 = p.OTHERSALARY2,
                                        .OTHERSALARY1 = p.OTHERSALARY1,
                                        .FACTORSALARY = p.FACTORSALARY,
                                        .OBJECT_LABOR = p.OBJECT_LABOR,
                                        .OBJECT_LABORNAME = objectLabor.NAME_VN,
                                        .OTHERSALARY3 = p.OTHERSALARY3,
                                        .TOXIC_RATE = p.TOXIC_RATE,
                                        .TOXIC_SALARY = p.TOXIC_SALARY,
                                        .COEFFICIENT = sal_rank.COEFFICIENT,
                                        .NEXTSALARY_DATE = p.NEXTSALARY_DATE,
                                        .GAS_SAL = p.GAS_SAL,
                                        .PHONE_SAL = p.PHONE_SAL,
                                        .ADDITIONAL_SAL = p.ADDITIONAL_SAL,
                                        .SHARE_SAL = p.SHARE_SAL,
                                        .PC1 = p.PC1,
                                        .PC2 = p.PC2,
                                        .PC3 = p.PC3,
                                        .PC4 = p.PC4,
                                        .PC5 = p.PC5}
            Dim dateNow = Date.Now.Date
            If Not _filter.IS_TER Then
                query = query.Where(Function(p) Not p.WORK_STATUS.HasValue Or
                                        (p.WORK_STATUS.HasValue And
                                         ((p.WORK_STATUS <> terID) Or (p.WORK_STATUS = terID And p.TER_EFFECT_DATE > dateNow))))
            End If

            If _filter.EMPLOYEE_CODE IsNot Nothing Then
                query = query.Where(Function(p) p.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If

            If _filter.EMPLOYEE_ID <> 0 Then
                query = query.Where(Function(p) p.EMPLOYEE_ID = _filter.EMPLOYEE_ID)
            End If

            If _filter.EMPLOYEE_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.EMPLOYEE_NAME.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If

            If _filter.ORG_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If

            If _filter.ORG_NAME2 <> "" Then
                query = query.Where(Function(p) p.ORG_NAME2.ToUpper().IndexOf(_filter.ORG_NAME2.ToUpper) >= 0)
            End If

            If _filter.ORG_NAME3 <> "" Then
                query = query.Where(Function(p) p.ORG_NAME3.ToUpper().IndexOf(_filter.ORG_NAME3.ToUpper) >= 0)
            End If

            If _filter.ORG_NAME4 <> "" Then
                query = query.Where(Function(p) p.ORG_NAME4.ToUpper().IndexOf(_filter.ORG_NAME4.ToUpper) >= 0)
            End If

            If _filter.ORG_NAME5 <> "" Then
                query = query.Where(Function(p) p.ORG_NAME5.ToUpper().IndexOf(_filter.ORG_NAME5.ToUpper) >= 0)
            End If

            If _filter.ORG_NAME6 <> "" Then
                query = query.Where(Function(p) p.ORG_NAME6.ToUpper().IndexOf(_filter.ORG_NAME6.ToUpper) >= 0)
            End If

            If _filter.ORG_NAME7 <> "" Then
                query = query.Where(Function(p) p.ORG_NAME7.ToUpper().IndexOf(_filter.ORG_NAME7.ToUpper) >= 0)
            End If

            If _filter.TITLE_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.TITLE_NAME.ToUpper.Contains(_filter.TITLE_NAME.ToUpper))
            End If

            If _filter.EMPLOYEE_ID <> 0 Then
                query = query.Where(Function(p) p.EMPLOYEE_ID = _filter.EMPLOYEE_ID)
            End If

            If _filter.DECISION_TYPE_ID IsNot Nothing Then
                query = query.Where(Function(p) p.DECISION_TYPE_ID = _filter.DECISION_TYPE_ID)
            End If

            If _filter.TOXIC_RATE IsNot Nothing Then
                query = query.Where(Function(p) p.TOXIC_RATE = _filter.TOXIC_RATE)
            End If

            If _filter.TOXIC_SALARY IsNot Nothing Then
                query = query.Where(Function(p) p.TOXIC_SALARY = _filter.TOXIC_SALARY)
            End If

            If _filter.COEFFICIENT IsNot Nothing Then
                query = query.Where(Function(p) p.COEFFICIENT = _filter.COEFFICIENT)
            End If

            If _filter.DECISION_TYPE_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.DECISION_TYPE_NAME.ToUpper.Contains(_filter.DECISION_TYPE_NAME.ToUpper))
            End If

            If _filter.IS_MISSION IsNot Nothing Then
                query = query.Where(Function(p) p.IS_MISSION_SHORT = _filter.IS_MISSION)
            End If

            If _filter.IS_PROCESS IsNot Nothing Then
                query = query.Where(Function(p) p.IS_PROCESS_SHORT = _filter.IS_PROCESS)
            End If

            If _filter.IS_WAGE IsNot Nothing Then
                query = query.Where(Function(p) p.IS_WAGE_SHORT = _filter.IS_WAGE)
            End If

            If _filter.IS_3B IsNot Nothing Then
                query = query.Where(Function(p) p.IS_3B_SHORT = _filter.IS_3B)
            End If

            If _filter.NEXTSALARY_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.NEXTSALARY_DATE = _filter.NEXTSALARY_DATE)
            End If

            If _filter.EFFECT_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.EFFECT_DATE = _filter.EFFECT_DATE)
            End If

            If _filter.FROM_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.EFFECT_DATE >= _filter.FROM_DATE)
            End If

            If _filter.TO_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.EFFECT_DATE <= _filter.TO_DATE)
            End If

            If _filter.STATUS_ID IsNot Nothing Then
                query = query.Where(Function(p) p.STATUS_ID = _filter.STATUS_ID)
            End If

            If _filter.SAL_GROUP_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.SAL_GROUP_NAME.ToUpper.Contains(_filter.SAL_GROUP_NAME.ToUpper))
            End If
            If _filter.SAL_TYPE_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.SAL_TYPE_NAME.ToUpper.Contains(_filter.SAL_TYPE_NAME.ToUpper))
            End If

            If _filter.SAL_LEVEL_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.SAL_LEVEL_NAME.ToUpper.Contains(_filter.SAL_LEVEL_NAME.ToUpper))
            End If

            If _filter.SAL_RANK_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.SAL_RANK_NAME.ToUpper.Contains(_filter.SAL_RANK_NAME.ToUpper))
            End If

            If _filter.SAL_PAYMENT_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.SAL_PAYMENT_NAME.ToUpper.Contains(_filter.SAL_PAYMENT_NAME.ToUpper))
            End If

            If _filter.SAL_BASIC IsNot Nothing Then
                query = query.Where(Function(p) p.SAL_BASIC = _filter.SAL_BASIC)
            End If

            If _filter.COST_SUPPORT IsNot Nothing Then
                query = query.Where(Function(p) p.COST_SUPPORT = _filter.COST_SUPPORT)
            End If

            If _filter.SAL_TOTAL IsNot Nothing Then
                query = query.Where(Function(p) p.COST_SUPPORT = _filter.SAL_TOTAL)
            End If
            If _filter.FACTORSALARY IsNot Nothing Then
                query = query.Where(Function(p) p.FACTORSALARY = _filter.FACTORSALARY)
            End If
            If _filter.STAFF_RANK_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.STAFF_RANK_NAME.ToUpper.Contains(_filter.STAFF_RANK_NAME.ToUpper))
            End If
            If _filter.STATUS_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.STATUS_NAME.ToUpper.Contains(_filter.STATUS_NAME.ToUpper))
            End If
            If _filter.DIRECT_MANAGER_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.DIRECT_MANAGER_NAME.ToUpper.Contains(_filter.DIRECT_MANAGER_NAME.ToUpper))
            End If
            If _filter.OTHERSALARY1 IsNot Nothing Then
                query = query.Where(Function(p) p.OTHERSALARY1 = _filter.OTHERSALARY1)
            End If
            If _filter.OTHERSALARY2 IsNot Nothing Then
                query = query.Where(Function(p) p.OTHERSALARY2 = _filter.OTHERSALARY2)
            End If
            If _filter.Ids IsNot Nothing Then
                If _filter.Ids.Any() Then
                    query = query.Where(Function(p) _filter.Ids.Contains(p.ID))
                End If
            End If
            ' danh sách thông tin quá trình công tác
            Dim working = query

            working = working.OrderBy(Sorts)
            Total = working.Count
            working = working.Skip(PageIndex * PageSize).Take(PageSize)
            Dim result = working.ToList
            Dim workingIds = From p In result
                             Select p.ID
            Dim allowListIds As Integer() = New Integer() {1, 2, 3, 4, 5}
            Dim workingAllows = (From p In Context.HU_WORKING_ALLOW
                                 Where workingIds.Contains(p.HU_WORKING_ID) And allowListIds.Contains(p.ALLOWANCE_LIST_ID)
                                 Select New WorkingAllowView With {.HU_WORKING_ID = p.HU_WORKING_ID,
                                     .ALLOWANCE_LIST_ID = p.ALLOWANCE_LIST_ID,
                                     .AMOUNT = p.AMOUNT}).ToList()
            result = GetAllowanceInfos(result, workingAllows)

            For Each workOld As WorkingDTO In result
                Dim effectDate = workOld.EFFECT_DATE
                Dim empId = workOld.EMPLOYEE_ID
                Dim rowQuery = (From p In Context.HU_WORKING
                                Where p.EFFECT_DATE < effectDate _
                                And p.EMPLOYEE_ID = empId _
                                And p.IS_MISSION = -1 _
                                Order By p.EFFECT_DATE Descending).FirstOrDefault 'And p.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID _

                If rowQuery IsNot Nothing Then
                    Dim working_old = (From p In Context.HU_WORKING
                                       From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                                       From o In Context.HU_ORGANIZATION.Where(Function(f) p.ORG_ID = f.ID)
                                       From t In Context.HU_TITLE.Where(Function(f) p.TITLE_ID = f.ID)
                                       From sal_type In Context.PA_SALARY_TYPE.Where(Function(f) p.SAL_TYPE_ID = f.ID).DefaultIfEmpty
                                       From titlegroup In Context.OT_OTHER_LIST.Where(Function(f) t.TITLE_GROUP_ID = f.ID).DefaultIfEmpty
                                       From deci_type In Context.OT_OTHER_LIST.Where(Function(f) p.DECISION_TYPE_ID = f.ID).DefaultIfEmpty
                                       From staffrank In Context.HU_STAFF_RANK.Where(Function(f) p.STAFF_RANK_ID = f.ID).DefaultIfEmpty
                                       From sal_group In Context.PA_SALARY_GROUP.Where(Function(f) p.SAL_GROUP_ID = f.ID).DefaultIfEmpty
                                       From sal_level In Context.PA_SALARY_LEVEL.Where(Function(f) p.SAL_LEVEL_ID = f.ID).DefaultIfEmpty
                                       From sal_rank In Context.PA_SALARY_RANK.Where(Function(f) p.SAL_RANK_ID = f.ID).DefaultIfEmpty
                                       From taxTable In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TAX_TABLE_ID).DefaultIfEmpty
                                       From oldDirect In Context.HU_EMPLOYEE.Where(Function(f) p.DIRECT_MANAGER = f.ID).DefaultIfEmpty
                                       From OBJ_ATT In Context.OT_OTHER_LIST.Where(Function(F) F.ID = p.OBJECT_ATTENDANCE).DefaultIfEmpty
                                       Where (p.ID = rowQuery.ID)
                                       Select New WorkingDTO With {
                                                          .ID = p.ID,
                                                          .DECISION_NO = p.DECISION_NO,
                                                          .FILING_DATE = p.FILING_DATE,
                                                          .OBJECT_ATTENDANCE = p.OBJECT_ATTENDANCE,
                                                          .OBJECT_ATTENDANCE_NAME = OBJ_ATT.NAME_VN,
                                                          .DECISION_TYPE_ID = p.DECISION_TYPE_ID,
                                                          .DECISION_TYPE_NAME = deci_type.NAME_VN,
                                                          .EFFECT_DATE = p.EFFECT_DATE,
                                                          .EXPIRE_DATE = p.EXPIRE_DATE,
                                                          .ORG_ID = p.ORG_ID,
                                                          .ORG_NAME = o.NAME_VN,
                                                          .TITLE_ID = p.TITLE_ID,
                                                          .TITLE_NAME = t.NAME_VN,
                                                          .SIGN_DATE = p.SIGN_DATE,
                                                          .SIGN_NAME = p.SIGN_NAME,
                                                          .SIGN_TITLE = p.SIGN_TITLE,
                                                          .DIRECT_MANAGER_NAME = oldDirect.FULLNAME_VN}).FirstOrDefault
                    workOld.ORG_NAME_OLD = working_old.ORG_NAME
                    workOld.TITLE_NAME_OLD = working_old.TITLE_NAME
                    workOld.OBJECT_ATTENDANCE_NAME_OLD = working_old.OBJECT_ATTENDANCE_NAME
                    workOld.FILING_DATE_OLD = working_old.FILING_DATE
                    workOld.DECISION_TYPE_NAME_OLD = working_old.DECISION_TYPE_NAME
                    workOld.EFFECT_DATE_OLD = working_old.EFFECT_DATE
                    workOld.EXPIRE_DATE_OLD = working_old.EXPIRE_DATE
                    workOld.DECISION_NO_OLD = working_old.DECISION_NO
                    workOld.SIGN_DATE_OLD = working_old.SIGN_DATE
                    workOld.SIGN_NAME_OLD = working_old.SIGN_NAME
                    workOld.SIGN_TITLE_OLD = working_old.SIGN_TITLE
                    workOld.REMARK_OLD = working_old.REMARK
                    workOld.DIRECT_MANAGER_NAME_OLD = working_old.DIRECT_MANAGER_NAME
                End If
            Next

            Return result
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function ApproveListChangeInfoMng(ByVal listID As List(Of Decimal), ByVal acti As String, ByVal log As UserLog) As Boolean
        Try
            Dim item As Decimal = 0
            For idx = 0 To listID.Count - 1
                item = listID(idx)
                Dim objWorking As New WorkingDTO
                Dim objWorkingData = (From p In Context.HU_WORKING Where p.ID = item).FirstOrDefault

                objWorking.ID = objWorkingData.ID
                objWorking.EMPLOYEE_ID = objWorkingData.EMPLOYEE_ID
                objWorking.OBJECT_ATTENDANCE = objWorkingData.OBJECT_ATTENDANCE
                objWorking.FILING_DATE = objWorkingData.FILING_DATE
                objWorking.TITLE_ID = objWorkingData.TITLE_ID
                objWorking.ORG_ID = objWorkingData.ORG_ID
                objWorking.STAFF_RANK_ID = objWorkingData.STAFF_RANK_ID
                objWorking.STATUS_ID = objWorkingData.STATUS_ID
                objWorking.SALE_COMMISION_ID = objWorkingData.SALE_COMMISION_ID
                objWorking.EFFECT_DATE = objWorkingData.EFFECT_DATE
                objWorking.EXPIRE_DATE = objWorkingData.EXPIRE_DATE
                objWorking.DECISION_TYPE_ID = objWorkingData.DECISION_TYPE_ID
                objWorking.DECISION_NO = objWorkingData.DECISION_NO
                objWorking.SAL_TYPE_ID = objWorkingData.SAL_TYPE_ID
                objWorking.SAL_GROUP_ID = objWorkingData.SAL_GROUP_ID
                objWorking.SAL_LEVEL_ID = objWorkingData.SAL_LEVEL_ID
                objWorking.SAL_RANK_ID = objWorkingData.SAL_RANK_ID
                objWorking.SAL_BASIC = objWorkingData.SAL_BASIC
                objWorking.COST_SUPPORT = objWorkingData.COST_SUPPORT
                objWorking.DIRECT_MANAGER = objWorkingData.DIRECT_MANAGER
                objWorking.SIGN_DATE = objWorkingData.SIGN_DATE
                objWorking.SIGN_ID = objWorkingData.SIGN_ID
                objWorking.SIGN_NAME = objWorkingData.SIGN_NAME
                objWorking.SIGN_TITLE = objWorkingData.SIGN_TITLE
                objWorking.REMARK = objWorkingData.REMARK
                objWorking.IS_PROCESS = objWorkingData.IS_PROCESS
                objWorking.IS_MISSION = objWorkingData.IS_MISSION
                objWorking.IS_WAGE = objWorkingData.IS_WAGE
                objWorking.IS_3B = False
                objWorking.PERCENT_SALARY = objWorkingData.PERCENT_SALARY
                objWorking.STAFF_RANK_ID = objWorkingData.STAFF_RANK_ID
                objWorking.ATTACH_FILE = objWorkingData.ATTACH_FILE
                objWorking.FILENAME = objWorkingData.FILENAME
                objWorking.TAX_TABLE_ID = objWorkingData.TAX_TABLE_ID
                objWorking.SAL_TOTAL = objWorkingData.SAL_TOTAL
                objWorking.SAL_INS = objWorkingData.SAL_INS
                objWorking.ALLOWANCE_TOTAL = objWorkingData.ALLOWANCE_TOTAL
                objWorking.SAL_INS = objWorkingData.SAL_INS
                objWorking.ATTACH_FILE = objWorkingData.ATTACH_FILE
                objWorking.PERCENTSALARY = objWorkingData.PERCENTSALARY
                objWorking.FACTORSALARY = objWorkingData.FACTORSALARY
                objWorking.OTHERSALARY1 = objWorkingData.OTHERSALARY1
                objWorking.OTHERSALARY2 = objWorkingData.OTHERSALARY2
                objWorking.OTHERSALARY3 = objWorkingData.OTHERSALARY3
                objWorking.OTHERSALARY4 = objWorkingData.OTHERSALARY4
                objWorking.OTHERSALARY5 = objWorkingData.OTHERSALARY5
                objWorking.OBJECT_LABOR = objWorkingData.OBJECT_LABOR

                If objWorking.STATUS_ID = ProfileCommon.DECISION_STATUS.WAIT_APPROVE_ID And acti = "P" Then
                    objWorking.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID
                    objWorkingData.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID
                    ApproveWorking1(objWorking)
                End If
                If objWorking.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID And acti = "M" Then
                    objWorking.STATUS_ID = ProfileCommon.DECISION_STATUS.WAIT_APPROVE_ID
                    objWorkingData.STATUS_ID = ProfileCommon.DECISION_STATUS.WAIT_APPROVE_ID
                    'ApproveWorking1(objWorking)
                    'Del_EXPIRE_DATE_IN_WORKING(objWorking)
                End If
                If objWorking.lstAllowance IsNot Nothing Then
                    objWorking.ALLOWANCE_TOTAL = objWorking.lstAllowance.Sum(Function(f) f.AMOUNT)
                    For Each row In objWorking.lstAllowance
                        If objWorking.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID Then
                            Dim objALlow = (From p In Context.HU_WORKING_ALLOW
                                            From w In Context.HU_WORKING.Where(Function(f) f.ID = p.HU_WORKING_ID)
                                            Where p.ALLOWANCE_LIST_ID = row.ALLOWANCE_LIST_ID And
                                            w.EMPLOYEE_ID = objWorking.EMPLOYEE_ID And
                                            p.EFFECT_DATE < row.EFFECT_DATE And
                                            p.EXPIRE_DATE Is Nothing
                                            Select p).FirstOrDefault

                            If objALlow IsNot Nothing Then
                                objALlow.EXPIRE_DATE = row.EFFECT_DATE.Value.AddDays(-1)
                            End If
                        End If
                        Dim allow As New HU_WORKING_ALLOW
                        allow.ID = Utilities.GetNextSequence(Context, Context.HU_WORKING_ALLOW.EntitySet.Name)
                        allow.HU_WORKING_ID = objWorkingData.ID
                        allow.ALLOWANCE_LIST_ID = row.ALLOWANCE_LIST_ID
                        allow.AMOUNT = row.AMOUNT
                        allow.IS_INSURRANCE = row.IS_INSURRANCE
                        allow.EFFECT_DATE = row.EFFECT_DATE
                        allow.EXPIRE_DATE = row.EXPIRE_DATE
                        Context.HU_WORKING_ALLOW.AddObject(allow)
                    Next
                End If
                objWorkingData.ALLOWANCE_TOTAL = Context.HU_WORKING_ALLOW.Where(Function(f) f.HU_WORKING_ID = objWorking.ID).Sum(Function(f) f.AMOUNT)
                Dim allowanceTotal = 0
                If objWorkingData.ALLOWANCE_TOTAL.HasValue Then
                    allowanceTotal = objWorkingData.ALLOWANCE_TOTAL.Value
                End If
                objWorkingData.SAL_TOTAL = objWorkingData.SAL_BASIC + allowanceTotal

                Dim insurranceAllow = Context.HU_WORKING_ALLOW.Where(Function(f) f.HU_WORKING_ID = objWorking.ID And f.IS_INSURRANCE = True).Sum(Function(f) f.AMOUNT)
                If insurranceAllow.HasValue = False Then
                    insurranceAllow = 0
                End If
                objWorkingData.SAL_INS = objWorkingData.SAL_INS

                'Dim sumAllowIns As Decimal?
                'Dim sumAllow = GetAllowanceTotalByDate(objWorking.EMPLOYEE_ID, objWorking.EFFECT_DATE, sumAllowIns)
                ' Dim total As Decimal? = 0
                'If objWorkingData.SAL_BASIC IsNot Nothing Then
                '    total = total + objWorkingData.SAL_BASIC
                'End If
                If objWorking.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID Then
                    'Dim totalIns As Decimal = 0
                    'If sumAllowIns IsNot Nothing Then
                    '    totalIns += sumAllowIns
                    'End If
                    AutoGenInsChangeByWorking(objWorkingData.EMPLOYEE_ID,
                                     objWorkingData.ORG_ID,
                                     objWorkingData.TITLE_ID,
                                     objWorkingData.EFFECT_DATE,
                                              objWorkingData.SAL_INS,
                                     log)
                End If
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Private Class WorkingAllowView  'p.ALLOWANCE_LIST_ID, p.HU_WORKING_ID, p.AMOUNT
        Public Property ALLOWANCE_LIST_ID As Decimal?
        Public Property HU_WORKING_ID As Decimal?
        Public Property AMOUNT As Decimal?
    End Class

    Private Function GetAllowanceInfos(ByRef result As List(Of WorkingDTO), ByRef workingAllows As List(Of WorkingAllowView)) As List(Of WorkingDTO)
        For Each item As WorkingDTO In result
            Dim ResponsibilityAllowances = (From p In workingAllows
                                            Where p.ALLOWANCE_LIST_ID = 1 And p.HU_WORKING_ID = item.ID).FirstOrDefault()
            If ResponsibilityAllowances IsNot Nothing Then
                item.ResponsibilityAllowances = ResponsibilityAllowances.AMOUNT
            End If

            Dim WorkAllowances = (From p In workingAllows
                                  Where p.ALLOWANCE_LIST_ID = 2 And p.HU_WORKING_ID = item.ID).FirstOrDefault()
            If WorkAllowances IsNot Nothing Then
                item.WorkAllowances = WorkAllowances.AMOUNT
            End If

            Dim AttendanceAllowances = (From p In workingAllows
                                        Where p.ALLOWANCE_LIST_ID = 3 And p.HU_WORKING_ID = item.ID).FirstOrDefault()
            If AttendanceAllowances IsNot Nothing Then
                item.AttendanceAllowances = AttendanceAllowances.AMOUNT
            End If

            Dim HousingAllowances = (From p In workingAllows
                                     Where p.ALLOWANCE_LIST_ID = 4 And p.HU_WORKING_ID = item.ID).FirstOrDefault()
            If HousingAllowances IsNot Nothing Then
                item.HousingAllowances = HousingAllowances.AMOUNT
            End If

            Dim CarRentalAllowances = (From p In workingAllows
                                       Where p.ALLOWANCE_LIST_ID = 5 And p.HU_WORKING_ID = item.ID).FirstOrDefault()
            If CarRentalAllowances IsNot Nothing Then
                item.CarRentalAllowances = CarRentalAllowances.AMOUNT
            End If
        Next
        Return result
    End Function
    Public Function GetLastWorkingSalary(ByVal _filter As WorkingDTO) As WorkingDTO

        Try
            Dim query = From p In Context.HU_WORKING
                        From sal_type In Context.PA_SALARY_TYPE.Where(Function(f) p.SAL_TYPE_ID = f.ID).DefaultIfEmpty
                        From sal_group In Context.PA_SALARY_GROUP.Where(Function(f) p.SAL_GROUP_ID = f.ID).DefaultIfEmpty
                        From sal_level In Context.PA_SALARY_LEVEL.Where(Function(f) p.SAL_LEVEL_ID = f.ID).DefaultIfEmpty
                        From sal_rank In Context.PA_SALARY_RANK.Where(Function(f) p.SAL_RANK_ID = f.ID).DefaultIfEmpty
                        From taxTable In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TAX_TABLE_ID).DefaultIfEmpty
                        Where p.EMPLOYEE_ID = _filter.EMPLOYEE_ID And p.IS_WAGE = -1 _
                        And p.STATUS_ID = 447 And p.EFFECT_DATE <= If(_filter.EFFECT_DATE IsNot Nothing, _filter.EFFECT_DATE, DateTime.Now)
                        Order By p.EFFECT_DATE Descending
                        Select New WorkingDTO With {
                             .ID = p.ID,
                             .DECISION_NO = p.DECISION_NO,
                             .EFFECT_DATE = p.EFFECT_DATE,
                             .PERCENT_SALARY = p.PERCENTSALARY,
                             .SAL_BASIC = p.SAL_BASIC,
                             .SALARY_BHXH = p.SALARY_BHXH,
                             .SAL_TYPE_ID = p.SAL_TYPE_ID,
                             .SAL_TYPE_NAME = sal_type.NAME,
                             .SAL_GROUP_ID = p.SAL_GROUP_ID,
                             .SAL_GROUP_NAME = sal_group.NAME,
                             .SAL_LEVEL_ID = p.SAL_LEVEL_ID,
                             .SAL_LEVEL_NAME = sal_level.NAME,
                             .SAL_RANK_ID = p.SAL_RANK_ID,
                             .SAL_RANK_NAME = sal_rank.RANK,
                            .SAL_INS = p.SAL_INS,
                            .SAL_TOTAL = p.SAL_TOTAL,
                            .ALLOWANCE_TOTAL = p.ALLOWANCE_TOTAL,
                            .TAX_TABLE_ID = p.TAX_TABLE_ID,
                            .TAX_TABLE_Name = taxTable.NAME_VN,
                             .OTHERSALARY1 = p.OTHERSALARY1,
                             .OTHERSALARY2 = p.OTHERSALARY2,
                            .OTHERSALARY3 = p.OTHERSALARY3
                            }

            Dim working = query.FirstOrDefault
            If working IsNot Nothing Then
                Dim allowListIds As Integer() = New Integer() {1, 2, 3, 4, 5}
                Dim workingAllows = (From p In Context.HU_WORKING_ALLOW
                                     Where p.HU_WORKING_ID = working.ID And allowListIds.Contains(p.ALLOWANCE_LIST_ID)
                                     Select New WorkingAllowView With {.HU_WORKING_ID = p.HU_WORKING_ID,
                        .ALLOWANCE_LIST_ID = p.ALLOWANCE_LIST_ID,
                        .AMOUNT = p.AMOUNT}).ToList()
                Dim workings As New List(Of WorkingDTO)
                workings.Add(working)
                working = GetAllowanceInfos(workings, workingAllows).FirstOrDefault()

                working.lstAllowance = (From p In Context.HU_WORKING_ALLOW
                                        From allow In Context.HU_ALLOWANCE_LIST.Where(Function(f) f.ID = p.ALLOWANCE_LIST_ID)
                                        Where p.HU_WORKING_ID = working.ID
                                        Select New WorkingAllowanceDTO With {.ALLOWANCE_LIST_ID = p.ALLOWANCE_LIST_ID,
                                                                             .ALLOWANCE_LIST_NAME = allow.NAME,
                                                                             .AMOUNT = p.AMOUNT,
                                                                             .EFFECT_DATE = p.EFFECT_DATE,
                                                                             .EXPIRE_DATE = p.EXPIRE_DATE,
                                                                             .IS_INSURRANCE = p.IS_INSURRANCE}).ToList
                If working.lstAllowance IsNot Nothing AndAlso working.lstAllowance.Count > 0 Then
                    working.ALLOWANCE_TOTAL = working.lstAllowance.Sum(Function(f) f.AMOUNT)
                End If
            End If

            Return working
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try


    End Function
    Public Function GetWorkingByID_1(ByVal _filter As WorkingDTO) As WorkingDTO
        Try
            Dim query = From p In Context.HU_WORKING
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From o In Context.HU_ORGANIZATION.Where(Function(f) p.ORG_ID = f.ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) p.TITLE_ID = f.ID).DefaultIfEmpty
                        From sale_commision In Context.PA_SALE_COMMISION.Where(Function(f) f.ID = p.SALE_COMMISION_ID).DefaultIfEmpty
                        From titlegroup In Context.OT_OTHER_LIST.Where(Function(f) t.TITLE_GROUP_ID = f.ID).DefaultIfEmpty
                        From deci_type In Context.OT_OTHER_LIST.Where(Function(f) p.DECISION_TYPE_ID = f.ID).DefaultIfEmpty
                        From sal_type In Context.PA_SALARY_TYPE.Where(Function(f) p.SAL_TYPE_ID = f.ID).DefaultIfEmpty
                        From sal_group In Context.PA_SALARY_GROUP.Where(Function(f) p.SAL_GROUP_ID = f.ID).DefaultIfEmpty
                        From sal_level In Context.PA_SALARY_LEVEL.Where(Function(f) p.SAL_LEVEL_ID = f.ID).DefaultIfEmpty
                        From sal_rank In Context.PA_SALARY_RANK.Where(Function(f) p.SAL_RANK_ID = f.ID).DefaultIfEmpty
                        From status In Context.OT_OTHER_LIST.Where(Function(f) p.STATUS_ID = f.ID).DefaultIfEmpty
                        From staffrank In Context.HU_STAFF_RANK.Where(Function(f) f.ID = p.STAFF_RANK_ID).DefaultIfEmpty
                        From direct In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.DIRECT_MANAGER).DefaultIfEmpty
                        From taxTable In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TAX_TABLE_ID).DefaultIfEmpty
                        From obj_att In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.OBJECT_ATTENDANCE).DefaultIfEmpty
                        From o1 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.EMPLOYEE_TYPE).DefaultIfEmpty
                        From obj_emp In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.OBJECT_EMPLOYEE_ID).DefaultIfEmpty
                        From obj_attendant In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.OBJECT_ATTENDANT_ID).DefaultIfEmpty
                        From work_place In Context.HU_WORK_PLACE.Where(Function(f) f.ID = p.WORK_PLACE_ID).DefaultIfEmpty
                        From objectLabor In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.OBJECT_LABOR And
                                                                        f.TYPE_ID = 6963).DefaultIfEmpty
                        Where p.ID = _filter.ID And p.STATUS_ID = 447 And p.EFFECT_DATE = If(_filter.EFFECT_DATE IsNot Nothing, _filter.EFFECT_DATE, DateTime.Now)
                        Select New WorkingDTO With {
                             .ID = p.ID,
                             .COST_SUPPORT = p.COST_SUPPORT,
                             .OBJECT_ATTENDANCE = p.OBJECT_ATTENDANCE,
                             .OBJECT_ATTENDANCE_NAME = obj_att.NAME_VN,
                             .FILING_DATE = p.FILING_DATE,
                             .DECISION_NO = p.DECISION_NO,
                             .DECISION_TYPE_ID = p.DECISION_TYPE_ID,
                             .DECISION_TYPE_NAME = deci_type.NAME_VN,
                             .OBJECT_LABOR = p.OBJECT_LABOR,
                             .OBJECT_LABORNAME = objectLabor.NAME_VN,
                             .EFFECT_DATE = p.EFFECT_DATE,
                             .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                             .EMPLOYEE_ID = e.ID,
                             .EMPLOYEE_NAME = e.FULLNAME_VN,
                             .STAFF_RANK_ID = p.STAFF_RANK_ID,
                             .STAFF_RANK_NAME = staffrank.NAME,
                             .SALE_COMMISION_ID = p.SALE_COMMISION_ID,
                             .SALE_COMMISION_NAME = sale_commision.NAME,
                             .EXPIRE_DATE = p.EXPIRE_DATE,
                             .IS_PROCESS = p.IS_PROCESS,
                             .IS_WAGE = p.IS_WAGE,
                             .IS_MISSION = p.IS_MISSION,
                             .IS_3B = p.IS_3B,
                             .ORG_ID = If(p.ORG_ID Is Nothing, 0, p.ORG_ID),
                             .ORG_NAME = o.NAME_VN,
                             .ORG_DESC = o.DESCRIPTION_PATH,
                             .PERCENT_SALARY = p.PERCENTSALARY,
                             .REMARK = p.REMARK,
                             .SAL_BASIC = p.SAL_BASIC,
                             .SAL_TYPE_ID = p.SAL_TYPE_ID,
                             .SAL_TYPE_NAME = sal_type.NAME,
                             .SAL_GROUP_ID = p.SAL_GROUP_ID,
                             .SAL_GROUP_NAME = sal_group.NAME,
                             .SAL_LEVEL_ID = p.SAL_LEVEL_ID,
                             .SAL_LEVEL_NAME = sal_level.NAME,
                             .SAL_RANK_ID = p.SAL_RANK_ID,
                             .SAL_RANK_NAME = sal_rank.RANK,
                             .SIGN_DATE = p.SIGN_DATE,
                             .SIGN_ID = p.SIGN_ID,
                             .SIGN_NAME = p.SIGN_NAME,
                             .SIGN_TITLE = p.SIGN_TITLE,
                             .TITLE_ID = If(p.TITLE_ID Is Nothing, 0, p.TITLE_ID),
                             .TITLE_NAME = t.NAME_VN,
                             .TITLE_GROUP_ID = t.TITLE_GROUP_ID,
                             .TITLE_GROUP_NAME = titlegroup.NAME_VN,
                             .DIRECT_MANAGER = p.DIRECT_MANAGER,
                             .DIRECT_MANAGER_NAME = direct.FULLNAME_VN,
                             .STATUS_ID = p.STATUS_ID,
                             .STATUS_NAME = status.NAME_VN,
                             .TAX_TABLE_ID = p.TAX_TABLE_ID,
                             .SAL_INS = p.SAL_INS,
                             .ALLOWANCE_TOTAL = p.ALLOWANCE_TOTAL,
                            .TAX_TABLE_Name = taxTable.NAME_VN,
                            .SAL_TOTAL = p.SAL_TOTAL,
                             .FILENAME = p.FILENAME,
                            .ATTACH_FILE = p.ATTACH_FILE,
                             .PERCENTSALARY = p.PERCENTSALARY,
                             .FACTORSALARY = p.FACTORSALARY,
                             .OTHERSALARY1 = p.OTHERSALARY1,
                             .OTHERSALARY2 = p.OTHERSALARY2,
                             .OTHERSALARY3 = p.OTHERSALARY3,
                             .OTHERSALARY4 = p.OTHERSALARY4,
                            .OTHERSALARY5 = p.OTHERSALARY5,
                             .EMPLOYEE_TYPE = p.EMPLOYEE_TYPE,
                             .EMPLOYEE_TYPE_NAME = o1.NAME_VN,
                             .SALARY_BHXH = p.SALARY_BHXH,
                             .OBJECT_EMPLOYEE_ID = p.OBJECT_EMPLOYEE_ID,
                             .OBJECT_EMPLOYEE_NAME = obj_emp.NAME_VN,
                             .OBJECT_ATTENDANT_ID = p.OBJECT_ATTENDANT_ID,
                             .OBJECT_ATTENDANT_NAME = obj_attendant.NAME_VN,
                             .WORK_PLACE_ID = p.WORK_PLACE_ID,
            .WORK_PLACE_NAME = work_place.NAME_VN
                         }

            Dim working = query.FirstOrDefault
            If working Is Nothing Then
                Return Nothing
            End If
            working.lstAllowance = (From p In Context.HU_WORKING_ALLOW
                                    From allow In Context.HU_ALLOWANCE_LIST.Where(Function(f) f.ID = p.ALLOWANCE_LIST_ID)
                                    Where p.HU_WORKING_ID = working.ID
                                    Select New WorkingAllowanceDTO With {.ALLOWANCE_LIST_ID = p.ALLOWANCE_LIST_ID,
                                                                         .ALLOWANCE_LIST_NAME = allow.NAME,
                                                                         .AMOUNT = p.AMOUNT,
                                                                         .EFFECT_DATE = p.EFFECT_DATE,
                                                                         .EXPIRE_DATE = p.EXPIRE_DATE,
                                                                         .IS_INSURRANCE = p.IS_INSURRANCE}).ToList
            If working.lstAllowance IsNot Nothing AndAlso working.lstAllowance.Count > 0 Then
                working.ALLOWANCE_TOTAL = working.lstAllowance.Sum(Function(f) f.AMOUNT)
            End If
            Dim query_old = (From p In Context.HU_WORKING
                             Where p.EFFECT_DATE < working.EFFECT_DATE _
                             And p.EMPLOYEE_ID = working.EMPLOYEE_ID _
                             And p.IS_MISSION = -1
                             Order By p.EFFECT_DATE Descending).FirstOrDefault
            'And p.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID

            If query_old IsNot Nothing Then
                Dim working_old = (From p In Context.HU_WORKING
                                   From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                                   From o In Context.HU_ORGANIZATION.Where(Function(f) p.ORG_ID = f.ID)
                                   From t In Context.HU_TITLE.Where(Function(f) p.TITLE_ID = f.ID)
                                   From direct In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.DIRECT_MANAGER).DefaultIfEmpty
                                   From sal_type In Context.PA_SALARY_TYPE.Where(Function(f) p.SAL_TYPE_ID = f.ID).DefaultIfEmpty
                                   From titlegroup In Context.OT_OTHER_LIST.Where(Function(f) t.TITLE_GROUP_ID = f.ID).DefaultIfEmpty
                                   From deci_type In Context.OT_OTHER_LIST.Where(Function(f) p.DECISION_TYPE_ID = f.ID).DefaultIfEmpty
                                   From staffrank In Context.HU_STAFF_RANK.Where(Function(f) p.STAFF_RANK_ID = f.ID).DefaultIfEmpty
                                   From sal_group In Context.PA_SALARY_GROUP.Where(Function(f) p.SAL_GROUP_ID = f.ID).DefaultIfEmpty
                                   From sal_level In Context.PA_SALARY_LEVEL.Where(Function(f) p.SAL_LEVEL_ID = f.ID).DefaultIfEmpty
                                   From sal_rank In Context.PA_SALARY_RANK.Where(Function(f) p.SAL_RANK_ID = f.ID).DefaultIfEmpty
                                   From taxTable In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TAX_TABLE_ID).DefaultIfEmpty
                                   From obj_emp In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.OBJECT_EMPLOYEE_ID).DefaultIfEmpty
                                   From obj_attendant In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.OBJECT_ATTENDANT_ID).DefaultIfEmpty
                                   From work_place In Context.HU_WORK_PLACE.Where(Function(f) f.ID = p.WORK_PLACE_ID).DefaultIfEmpty
                                   From OBJ_ATT In Context.OT_OTHER_LIST.Where(Function(F) F.ID = p.OBJECT_ATTENDANCE).DefaultIfEmpty
                                   From objectLabor In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.OBJECT_LABOR And
                                                                        f.TYPE_ID = 6963).DefaultIfEmpty
                                   Where (p.ID = query_old.ID)
                                   Select New WorkingDTO With {
                                       .ID = p.ID,
                                       .DECISION_NO = p.DECISION_NO,
                                       .FILING_DATE = p.FILING_DATE,
                                       .OBJECT_ATTENDANCE = p.OBJECT_ATTENDANCE,
                                       .OBJECT_ATTENDANCE_NAME = OBJ_ATT.NAME_VN,
                                       .DECISION_TYPE_ID = p.DECISION_TYPE_ID,
                                       .DECISION_TYPE_NAME = deci_type.NAME_VN,
                                       .OBJECT_LABOR = p.OBJECT_LABOR,
                                       .OBJECT_LABORNAME = objectLabor.NAME_VN,
                                       .EFFECT_DATE = p.EFFECT_DATE,
                                       .EXPIRE_DATE = p.EXPIRE_DATE,
                                       .ORG_ID = p.ORG_ID,
                                       .ORG_NAME = o.NAME_VN,
                                       .TITLE_ID = p.TITLE_ID,
                                       .TITLE_NAME = t.NAME_VN,
                                       .SAL_BASIC = p.SAL_BASIC,
                                       .SAL_INS = p.SAL_INS,
                                       .SAL_TYPE_ID = p.SAL_TYPE_ID,
                                       .SAL_TYPE_NAME = sal_type.NAME,
                                       .TAX_TABLE_ID = p.TAX_TABLE_ID,
                                       .TAX_TABLE_Name = taxTable.NAME_VN,
                                       .SAL_TOTAL = p.SAL_TOTAL,
                                       .ALLOWANCE_TOTAL = p.ALLOWANCE_TOTAL,
                                       .TITLE_GROUP_ID = t.TITLE_GROUP_ID,
                                       .TITLE_GROUP_NAME = titlegroup.NAME_VN,
                                       .STAFF_RANK_ID = p.STAFF_RANK_ID,
                                       .STAFF_RANK_NAME = staffrank.NAME,
                                       .ATTACH_FILE = p.ATTACH_FILE,
                                       .FILENAME = p.FILENAME,
                                       .COST_SUPPORT = p.COST_SUPPORT,
                                       .PERCENT_SALARY = p.PERCENTSALARY,
                                       .SAL_GROUP_ID = p.SAL_GROUP_ID,
                                       .SAL_GROUP_NAME = sal_group.NAME,
                                       .SAL_LEVEL_ID = p.SAL_LEVEL_ID,
                                       .SAL_LEVEL_NAME = sal_level.NAME,
                                       .SAL_RANK_ID = p.SAL_RANK_ID,
                                       .SAL_RANK_NAME = sal_rank.RANK,
                                       .OBJECT_EMPLOYEE_ID = p.OBJECT_EMPLOYEE_ID,
                                       .OBJECT_EMPLOYEE_NAME = obj_emp.NAME_VN,
                                       .OBJECT_ATTENDANT_ID = p.OBJECT_ATTENDANT_ID,
                                       .OBJECT_ATTENDANT_NAME = obj_attendant.NAME_VN,
                                       .WORK_PLACE_ID = p.WORK_PLACE_ID,
                                       .WORK_PLACE_NAME = work_place.NAME_VN,
                                       .DIRECT_MANAGER = p.DIRECT_MANAGER,
                                       .DIRECT_MANAGER_NAME = direct.FULLNAME_VN}).FirstOrDefault

                working.WORKING_OLD = working_old
            End If

            Return working
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try


    End Function

    Public Function GetWorkingByID(ByVal _filter As WorkingDTO) As WorkingDTO
        Try
            Dim query = From p In Context.HU_WORKING
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From o In Context.HU_ORGANIZATION.Where(Function(f) p.ORG_ID = f.ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) p.TITLE_ID = f.ID).DefaultIfEmpty
                        From sale_commision In Context.PA_SALE_COMMISION.Where(Function(f) f.ID = p.SALE_COMMISION_ID).DefaultIfEmpty
                        From titlegroup In Context.OT_OTHER_LIST.Where(Function(f) t.TITLE_GROUP_ID = f.ID).DefaultIfEmpty
                        From deci_type In Context.OT_OTHER_LIST.Where(Function(f) p.DECISION_TYPE_ID = f.ID).DefaultIfEmpty
                        From sal_type In Context.PA_SALARY_TYPE.Where(Function(f) p.SAL_TYPE_ID = f.ID).DefaultIfEmpty
                        From sal_group In Context.PA_SALARY_GROUP.Where(Function(f) p.SAL_GROUP_ID = f.ID).DefaultIfEmpty
                        From sal_level In Context.PA_SALARY_LEVEL.Where(Function(f) p.SAL_LEVEL_ID = f.ID).DefaultIfEmpty
                        From sal_rank In Context.PA_FRAME_SALARY.Where(Function(f) p.SAL_RANK_ID = f.ID).DefaultIfEmpty
                        From status In Context.OT_OTHER_LIST.Where(Function(f) p.STATUS_ID = f.ID).DefaultIfEmpty
                        From staffrank In Context.HU_JOB_BAND.Where(Function(f) f.ID = p.STAFF_RANK_ID).DefaultIfEmpty
                        From direct In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.DIRECT_MANAGER).DefaultIfEmpty
                        From taxTable In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TAX_TABLE_ID).DefaultIfEmpty
                        From obj_att In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.OBJECT_ATTENDANCE).DefaultIfEmpty
                        From o1 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.EMPLOYEE_TYPE).DefaultIfEmpty
                        From obj_emp In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.OBJECT_EMPLOYEE_ID).DefaultIfEmpty
                        From obj_attendant In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.OBJECT_ATTENDANT_ID).DefaultIfEmpty
                        From work_place In Context.HU_WORK_PLACE.Where(Function(f) f.ID = p.WORK_PLACE_ID).DefaultIfEmpty
                        From objectLabor In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.OBJECT_LABOR And
                                                                        f.TYPE_ID = 6963).DefaultIfEmpty
                        From salPayment In Context.OT_OTHER_LIST.Where(Function(f) p.SAL_PAYMENT_ID = f.ID).DefaultIfEmpty
                        Where p.ID = _filter.ID
                        Select New WorkingDTO With {
                             .ID = p.ID,
                             .COST_SUPPORT = p.COST_SUPPORT,
                             .OBJECT_ATTENDANCE = p.OBJECT_ATTENDANCE,
                             .OBJECT_ATTENDANCE_NAME = obj_att.NAME_VN,
                             .FILING_DATE = p.FILING_DATE,
                             .DECISION_NO = p.DECISION_NO,
                             .DECISION_TYPE_ID = p.DECISION_TYPE_ID,
                             .DECISION_TYPE_NAME = deci_type.NAME_VN,
                             .OBJECT_LABOR = p.OBJECT_LABOR,
                             .OBJECT_LABORNAME = objectLabor.NAME_VN,
                             .EFFECT_DATE = p.EFFECT_DATE,
                             .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                             .EMPLOYEE_ID = e.ID,
                             .EMPLOYEE_NAME = e.FULLNAME_VN,
                             .STAFF_RANK_ID = p.STAFF_RANK_ID,
                             .STAFF_RANK_NAME = staffrank.LEVEL_FROM,
                             .SAL_PAYMENT_ID = p.SAL_PAYMENT_ID,
                             .SAL_PAYMENT_NAME = salPayment.NAME_VN,
                             .SALE_COMMISION_ID = p.SALE_COMMISION_ID,
                             .SALE_COMMISION_NAME = sale_commision.NAME,
                             .EXPIRE_DATE = p.EXPIRE_DATE,
                             .IS_PROCESS = p.IS_PROCESS,
                             .IS_WAGE = p.IS_WAGE,
                             .IS_MISSION = p.IS_MISSION,
                             .IS_3B = p.IS_3B,
                             .ORG_ID = If(p.ORG_ID Is Nothing, 0, p.ORG_ID),
                             .ORG_NAME = o.NAME_VN,
                             .ORG_DESC = o.DESCRIPTION_PATH,
                             .PERCENT_SALARY = p.PERCENTSALARY,
                             .REMARK = p.REMARK,
                             .SAL_BASIC = p.SAL_BASIC,
                             .SAL_TYPE_ID = p.SAL_TYPE_ID,
                             .SAL_TYPE_NAME = sal_type.NAME,
                             .SAL_GROUP_ID = p.SAL_GROUP_ID,
                             .SAL_GROUP_NAME = sal_group.NAME,
                             .SAL_LEVEL_ID = p.SAL_LEVEL_ID,
                             .SAL_LEVEL_NAME = sal_level.NAME,
                             .SAL_RANK_ID = p.SAL_RANK_ID,
                             .SAL_RANK_NAME = sal_rank.NAME_VN,
                             .SIGN_DATE = p.SIGN_DATE,
                             .SIGN_ID = p.SIGN_ID,
                             .SIGN_NAME = p.SIGN_NAME,
                             .SIGN_TITLE = p.SIGN_TITLE,
                             .TITLE_ID = If(p.TITLE_ID Is Nothing, 0, p.TITLE_ID),
                             .TITLE_NAME = t.NAME_VN,
                             .TITLE_GROUP_ID = t.TITLE_GROUP_ID,
                             .TITLE_GROUP_NAME = titlegroup.NAME_VN,
                             .DIRECT_MANAGER = p.DIRECT_MANAGER,
                             .DIRECT_MANAGER_NAME = direct.FULLNAME_VN,
                             .STATUS_ID = p.STATUS_ID,
                             .STATUS_NAME = status.NAME_VN,
                             .TAX_TABLE_ID = p.TAX_TABLE_ID,
                             .SAL_INS = p.SAL_INS,
                             .ALLOWANCE_TOTAL = p.ALLOWANCE_TOTAL,
                            .TAX_TABLE_Name = taxTable.NAME_VN,
                            .SAL_TOTAL = p.SAL_TOTAL,
                             .FILENAME = p.FILENAME,
                            .ATTACH_FILE = p.ATTACH_FILE,
                             .PERCENTSALARY = p.PERCENTSALARY,
                             .FACTORSALARY = p.FACTORSALARY,
                             .OTHERSALARY1 = p.OTHERSALARY1,
                             .OTHERSALARY2 = p.OTHERSALARY2,
                             .OTHERSALARY3 = p.OTHERSALARY3,
                             .OTHERSALARY4 = p.OTHERSALARY4,
                            .OTHERSALARY5 = p.OTHERSALARY5,
                             .EMPLOYEE_TYPE = p.EMPLOYEE_TYPE,
                             .EMPLOYEE_TYPE_NAME = o1.NAME_VN,
                             .SALARY_BHXH = p.SALARY_BHXH,
                             .OBJECT_EMPLOYEE_ID = p.OBJECT_EMPLOYEE_ID,
                             .OBJECT_EMPLOYEE_NAME = obj_emp.NAME_VN,
                             .OBJECT_ATTENDANT_ID = p.OBJECT_ATTENDANT_ID,
                             .OBJECT_ATTENDANT_NAME = obj_attendant.NAME_VN,
                             .WORK_PLACE_ID = p.WORK_PLACE_ID,
                             .WORK_PLACE_NAME = work_place.NAME_VN,
                            .TOXIC_RATE = p.TOXIC_RATE,
                            .TOXIC_SALARY = p.TOXIC_SALARY,
                            .NEXTSALARY_DATE = p.NEXTSALARY_DATE,
                            .COEFFICIENT = sal_rank.COEFFICIENT,
                            .REGION_SAL = p.REGION_SAL,
                            .GAS_SAL = p.GAS_SAL,
                            .ADDITIONAL_SAL = p.ADDITIONAL_SAL,
                            .PHONE_SAL = p.PHONE_SAL,
                            .SHARE_SAL = p.SHARE_SAL,
                            .PC1 = p.PC1,
                            .PC2 = p.PC2,
                            .PC3 = p.PC3,
                            .PC4 = p.PC4,
                            .PC5 = p.PC5,
                            .IS_ALLOW_SALARY_LESS_THAN = p.IS_ALLOW_SALARY_LESS_THAN
                         }

            Dim working = query.FirstOrDefault
            If working Is Nothing Then
                Return Nothing
            End If
            working.lstAllowance = (From p In Context.HU_WORKING_ALLOW
                                    From allow In Context.HU_ALLOWANCE_LIST.Where(Function(f) f.ID = p.ALLOWANCE_LIST_ID)
                                    Where p.HU_WORKING_ID = working.ID
                                    Select New WorkingAllowanceDTO With {.ALLOWANCE_LIST_ID = p.ALLOWANCE_LIST_ID,
                                                                         .ALLOWANCE_LIST_NAME = allow.NAME,
                                                                         .AMOUNT = p.AMOUNT,
                                                                         .EFFECT_DATE = p.EFFECT_DATE,
                                                                         .EXPIRE_DATE = p.EXPIRE_DATE,
                                                                         .IS_INSURRANCE = p.IS_INSURRANCE}).ToList
            If working.lstAllowance IsNot Nothing AndAlso working.lstAllowance.Count > 0 Then
                working.ALLOWANCE_TOTAL = working.lstAllowance.Sum(Function(f) f.AMOUNT)
            End If
            Dim query_old = (From p In Context.HU_WORKING
                             Where p.EFFECT_DATE < working.EFFECT_DATE _
                             And p.EMPLOYEE_ID = working.EMPLOYEE_ID _
                             And p.IS_MISSION = -1
                             Order By p.EFFECT_DATE Descending).FirstOrDefault
            'And p.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID

            If query_old IsNot Nothing Then
                Dim working_old = (From p In Context.HU_WORKING
                                   From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                                   From o In Context.HU_ORGANIZATION.Where(Function(f) p.ORG_ID = f.ID)
                                   From t In Context.HU_TITLE.Where(Function(f) p.TITLE_ID = f.ID)
                                   From direct In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.DIRECT_MANAGER).DefaultIfEmpty
                                   From sal_type In Context.PA_SALARY_TYPE.Where(Function(f) p.SAL_TYPE_ID = f.ID).DefaultIfEmpty
                                   From titlegroup In Context.OT_OTHER_LIST.Where(Function(f) t.TITLE_GROUP_ID = f.ID).DefaultIfEmpty
                                   From deci_type In Context.OT_OTHER_LIST.Where(Function(f) p.DECISION_TYPE_ID = f.ID).DefaultIfEmpty
                                   From staffrank In Context.HU_JOB_BAND.Where(Function(f) p.STAFF_RANK_ID = f.ID).DefaultIfEmpty
                                   From sal_group In Context.PA_SALARY_GROUP.Where(Function(f) p.SAL_GROUP_ID = f.ID).DefaultIfEmpty
                                   From sal_level In Context.PA_SALARY_LEVEL.Where(Function(f) p.SAL_LEVEL_ID = f.ID).DefaultIfEmpty
                                   From sal_rank In Context.PA_FRAME_SALARY.Where(Function(f) p.SAL_RANK_ID = f.ID).DefaultIfEmpty
                                   From taxTable In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TAX_TABLE_ID).DefaultIfEmpty
                                   From obj_emp In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.OBJECT_EMPLOYEE_ID).DefaultIfEmpty
                                   From obj_attendant In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.OBJECT_ATTENDANT_ID).DefaultIfEmpty
                                   From work_place In Context.HU_WORK_PLACE.Where(Function(f) f.ID = p.WORK_PLACE_ID).DefaultIfEmpty
                                   From OBJ_ATT In Context.OT_OTHER_LIST.Where(Function(F) F.ID = p.OBJECT_ATTENDANCE).DefaultIfEmpty
                                   From objectLabor In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.OBJECT_LABOR And
                                                                        f.TYPE_ID = 6963).DefaultIfEmpty
                                   From salPayment In Context.OT_OTHER_LIST.Where(Function(f) p.SAL_PAYMENT_ID = f.ID).DefaultIfEmpty
                                   Where (p.ID = query_old.ID)
                                   Select New WorkingDTO With {
                                       .ID = p.ID,
                                       .DECISION_NO = p.DECISION_NO,
                                       .FILING_DATE = p.FILING_DATE,
                                       .OBJECT_ATTENDANCE = p.OBJECT_ATTENDANCE,
                                       .OBJECT_ATTENDANCE_NAME = OBJ_ATT.NAME_VN,
                                       .DECISION_TYPE_ID = p.DECISION_TYPE_ID,
                                       .DECISION_TYPE_NAME = deci_type.NAME_VN,
                                       .OBJECT_LABOR = p.OBJECT_LABOR,
                                       .OBJECT_LABORNAME = objectLabor.NAME_VN,
                                       .EFFECT_DATE = p.EFFECT_DATE,
                                       .EXPIRE_DATE = p.EXPIRE_DATE,
                                       .ORG_ID = p.ORG_ID,
                                       .ORG_NAME = o.NAME_VN,
                                       .TITLE_ID = p.TITLE_ID,
                                       .TITLE_NAME = t.NAME_VN,
                                      .SAL_PAYMENT_ID = p.SAL_PAYMENT_ID,
                                      .SAL_PAYMENT_NAME = salPayment.NAME_VN,
                                       .SAL_BASIC = p.SAL_BASIC,
                                       .SAL_INS = p.SAL_INS,
                                       .SAL_TYPE_ID = p.SAL_TYPE_ID,
                                       .SAL_TYPE_NAME = sal_type.NAME,
                                       .TAX_TABLE_ID = p.TAX_TABLE_ID,
                                       .TAX_TABLE_Name = taxTable.NAME_VN,
                                       .SAL_TOTAL = p.SAL_TOTAL,
                                       .ALLOWANCE_TOTAL = p.ALLOWANCE_TOTAL,
                                       .TITLE_GROUP_ID = t.TITLE_GROUP_ID,
                                       .TITLE_GROUP_NAME = titlegroup.NAME_VN,
                                       .STAFF_RANK_ID = p.STAFF_RANK_ID,
                                       .STAFF_RANK_NAME = staffrank.LEVEL_FROM,
                                       .ATTACH_FILE = p.ATTACH_FILE,
                                       .FILENAME = p.FILENAME,
                                       .COST_SUPPORT = p.COST_SUPPORT,
                                       .PERCENT_SALARY = p.PERCENTSALARY,
                                       .SAL_GROUP_ID = p.SAL_GROUP_ID,
                                       .SAL_GROUP_NAME = sal_group.NAME,
                                       .SAL_LEVEL_ID = p.SAL_LEVEL_ID,
                                       .SAL_LEVEL_NAME = sal_level.NAME,
                                       .SAL_RANK_ID = p.SAL_RANK_ID,
                                       .SAL_RANK_NAME = sal_rank.NAME_VN,
                                       .OBJECT_EMPLOYEE_ID = p.OBJECT_EMPLOYEE_ID,
                                       .OBJECT_EMPLOYEE_NAME = obj_emp.NAME_VN,
                                       .OBJECT_ATTENDANT_ID = p.OBJECT_ATTENDANT_ID,
                                       .OBJECT_ATTENDANT_NAME = obj_attendant.NAME_VN,
                                       .WORK_PLACE_ID = p.WORK_PLACE_ID,
                                       .WORK_PLACE_NAME = work_place.NAME_VN,
                                       .DIRECT_MANAGER = p.DIRECT_MANAGER,
                                       .COEFFICIENT = sal_rank.COEFFICIENT,
                                       .DIRECT_MANAGER_NAME = direct.FULLNAME_VN,
                                        .REGION_SAL = p.REGION_SAL,
                                        .GAS_SAL = p.GAS_SAL,
                                        .ADDITIONAL_SAL = p.ADDITIONAL_SAL,
                                        .PHONE_SAL = p.PHONE_SAL,
                                        .SHARE_SAL = p.SHARE_SAL}).FirstOrDefault

                working.WORKING_OLD = working_old
            End If

            Return working
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try


    End Function

    Public Function GetEmployeCurrentByID(ByVal _filter As WorkingDTO) As WorkingDTO
        Try
            Dim workingOLD = (From w In Context.HU_WORKING
                              Where w.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID And w.IS_WAGE = 0 And w.EMPLOYEE_ID = _filter.EMPLOYEE_ID And w.EFFECT_DATE <= Date.Now Order By w.EFFECT_DATE Descending
                        ).FirstOrDefault
            If workingOLD IsNot Nothing Then
                Dim query = From e In Context.HU_EMPLOYEE
                            From p In Context.HU_WORKING.Where(Function(f) f.EMPLOYEE_ID = e.ID)
                            From o In Context.HU_ORGANIZATION.Where(Function(f) p.ORG_ID = f.ID).DefaultIfEmpty
                            From o1 In Context.HUV_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                            From o2 In Context.HU_ORGANIZATION.Where(Function(f) f.ID = o1.ORG_ID2).DefaultIfEmpty
                            From t In Context.HU_TITLE.Where(Function(f) p.TITLE_ID = f.ID).DefaultIfEmpty
                            From sale_commision In Context.PA_SALE_COMMISION.Where(Function(f) f.ID = p.SALE_COMMISION_ID).DefaultIfEmpty
                            From titlegroup In Context.OT_OTHER_LIST.Where(Function(f) t.TITLE_GROUP_ID = f.ID).DefaultIfEmpty
                            From deci_type In Context.OT_OTHER_LIST.Where(Function(f) p.DECISION_TYPE_ID = f.ID).DefaultIfEmpty
                            From sal_type In Context.PA_SALARY_TYPE.Where(Function(f) p.SAL_TYPE_ID = f.ID).DefaultIfEmpty
                            From sal_group In Context.PA_SALARY_GROUP.Where(Function(f) p.SAL_GROUP_ID = f.ID).DefaultIfEmpty
                            From sal_level In Context.PA_SALARY_LEVEL.Where(Function(f) p.SAL_LEVEL_ID = f.ID).DefaultIfEmpty
                            From sal_rank In Context.PA_SALARY_RANK.Where(Function(f) p.SAL_RANK_ID = f.ID).DefaultIfEmpty
                            From status In Context.OT_OTHER_LIST.Where(Function(f) p.STATUS_ID = f.ID).DefaultIfEmpty
                            From staffrank In Context.HU_JOB_BAND.Where(Function(f) f.ID = p.STAFF_RANK_ID).DefaultIfEmpty
                            From taxTable In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TAX_TABLE_ID).DefaultIfEmpty
                            From filecontract In Context.HU_FILECONTRACT.Where(Function(f) f.EMP_ID = e.ID).DefaultIfEmpty
                            From obj_att In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.OBJECT_ATTENDANCE).DefaultIfEmpty
                            From direct In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.DIRECT_MANAGER).DefaultIfEmpty
                            From wp In Context.HU_WORK_PLACE.Where(Function(f) f.ID = p.WORK_PLACE_ID).DefaultIfEmpty
                            From objectLabor In Context.OT_OTHER_LIST.Where(Function(f) f.ID = e.OBJECT_LABOR And
                                                                        f.TYPE_ID = 6963).DefaultIfEmpty
                            From obj_attendant In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.OBJECT_ATTENDANT_ID).DefaultIfEmpty
                            From obj_emp In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.OBJECT_EMPLOYEE_ID).DefaultIfEmpty
                            From work_place In Context.HU_WORK_PLACE.Where(Function(f) f.ID = p.WORK_PLACE_ID).DefaultIfEmpty
                            Where e.ID = _filter.EMPLOYEE_ID And p.ID = workingOLD.ID
                            Order By p.EFFECT_DATE Descending
                            Select New WorkingDTO With {
                                 .ID = p.ID,
                                 .ORG_NAME2 = o1.ORG_NAME2,
                                 .SHORT_NAME = o2.SHORT_NAME,
                                 .COST_SUPPORT = p.COST_SUPPORT,
                                 .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                                 .EMPLOYEE_ID = e.ID,
                                 .EMPLOYEE_NAME = e.FULLNAME_VN,
                                 .STAFF_RANK_ID = p.STAFF_RANK_ID,
                                 .SALE_COMMISION_ID = p.SALE_COMMISION_ID,
                                 .SALE_COMMISION_NAME = sale_commision.NAME,
                                 .STAFF_RANK_NAME = staffrank.LEVEL_FROM,
                                 .ORG_ID = p.ORG_ID,
                                 .ORG_NAME = o.NAME_VN,
                                 .CODE_ATTENDANCE = e.OBJECTTIMEKEEPING,
                                 .ORG_DESC = o.DESCRIPTION_PATH,
                                 .EFFECT_DATE = p.EFFECT_DATE,
                                 .EXPIRE_DATE = p.EXPIRE_DATE,
                                 .DECISION_NO = p.DECISION_NO,
                                 .DECISION_TYPE_NAME = deci_type.NAME_VN,
                                 .PERCENT_SALARY = p.PERCENT_SALARY,
                                 .SAL_BASIC = p.SAL_BASIC,
                                 .SAL_TYPE_ID = p.SAL_TYPE_ID,
                                 .SAL_TYPE_NAME = sal_type.NAME,
                                 .SAL_GROUP_ID = p.SAL_GROUP_ID,
                                 .SAL_GROUP_NAME = sal_group.NAME,
                                 .SAL_LEVEL_ID = p.SAL_LEVEL_ID,
                                 .SAL_LEVEL_NAME = sal_level.NAME,
                                 .SAL_RANK_ID = p.SAL_RANK_ID,
                                 .SAL_RANK_NAME = sal_rank.RANK,
                                 .TITLE_ID = p.TITLE_ID,
                                 .TITLE_NAME = t.NAME_VN,
                                 .TITLE_GROUP_ID = t.TITLE_GROUP_ID,
                                 .TITLE_GROUP_NAME = titlegroup.NAME_VN,
                                 .WORK_STATUS = e.WORK_STATUS,
                                 .TER_EFFECT_DATE = e.TER_EFFECT_DATE,
                                .TAX_TABLE_ID = p.TAX_TABLE_ID,
                                .SAL_TOTAL = p.SAL_TOTAL,
                                .SAL_INS = p.SAL_INS,
                                .ALLOWANCE_TOTAL = p.ALLOWANCE_TOTAL,
                                 .FILENAME = p.FILENAME,
                                 .ATTACH_FILE = p.ATTACH_FILE,
                                 .FILENAME1 = filecontract.FILENAME,
                                 .UPLOADFILE = filecontract.UPLOADFILE,
                                 .OBJECT_ATTENDANCE = p.OBJECT_ATTENDANCE,
                                 .OBJECT_ATTENDANCE_NAME = obj_att.NAME_VN,
                                 .FILING_DATE = p.FILING_DATE,
                                .TAX_TABLE_Name = taxTable.NAME_VN,
                                 .DIRECT_MANAGER = p.DIRECT_MANAGER,
                                 .DIRECT_MANAGER_NAME = direct.FULLNAME_VN,
                                 .OBJECT_LABOR = e.OBJECT_LABOR,
                                 .OBJECT_LABORNAME = objectLabor.NAME_VN,
                                 .OBJECT_ATTENDANT_ID = p.OBJECT_ATTENDANT_ID,
                                 .OBJECT_ATTENDANT_NAME = obj_attendant.NAME_VN,
                                 .OBJECT_EMPLOYEE_ID = p.OBJECT_EMPLOYEE_ID,
                                 .OBJECT_EMPLOYEE_NAME = obj_emp.NAME_VN,
                                 .WORK_PLACE_ID = p.WORK_PLACE_ID,
                                 .WORK_PLACE_NAME = work_place.NAME_VN,
                                .DM_ID = e.DM_ID,
                                .TOXIC_SALARY = p.TOXIC_SALARY,
                                .TOXIC_RATE = p.TOXIC_RATE,
                                .NEXTSALARY_DATE = p.NEXTSALARY_DATE
                                 }

                Dim working = query.First()
                Dim allowListIds = New Integer() {1, 2, 3, 4, 5}
                Dim workingAllows = (From p In Context.HU_WORKING_ALLOW
                                     Where p.HU_WORKING_ID = working.ID And allowListIds.Contains(p.ALLOWANCE_LIST_ID)
                                     Select New WorkingAllowView With {.HU_WORKING_ID = p.HU_WORKING_ID,
                        .ALLOWANCE_LIST_ID = p.ALLOWANCE_LIST_ID,
                        .AMOUNT = p.AMOUNT}).ToList()
                Dim newList = New List(Of WorkingDTO)
                newList.Add(working)
                Dim result = GetAllowanceInfos(newList, workingAllows)
                Return result.First()
            Else
                Dim query = From e In Context.HU_EMPLOYEE
                            From o1 In Context.HUV_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                            From o2 In Context.HU_ORGANIZATION.Where(Function(f) f.ID = o1.ORG_ID2).DefaultIfEmpty
                            From o In Context.HU_ORGANIZATION.Where(Function(f) e.ORG_ID = f.ID).DefaultIfEmpty
                            From t In Context.HU_TITLE.Where(Function(f) e.TITLE_ID = f.ID).DefaultIfEmpty
                            From staffrank In Context.HU_JOB_BAND.Where(Function(f) f.ID = e.STAFF_RANK_ID).DefaultIfEmpty
                            From titlegroup In Context.OT_OTHER_LIST.Where(Function(f) t.TITLE_GROUP_ID = f.ID).DefaultIfEmpty
                            Where e.ID = _filter.EMPLOYEE_ID
                            Select New WorkingDTO With {.EMPLOYEE_ID = e.ID,
                                                        .SHORT_NAME = o2.SHORT_NAME,
                                                        .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                                                        .EMPLOYEE_NAME = e.FULLNAME_VN,
                                                        .TITLE_ID = e.TITLE_ID,
                                                        .DIRECT_MANAGER = e.DIRECT_MANAGER,
                                                        .OBJECT_ATTENDANCE = e.OBJECTTIMEKEEPING,
                                                        .OBJECT_LABOR = e.OBJECT_LABOR,
                                                        .CODE_ATTENDANCE = e.OBJECTTIMEKEEPING,
                                                        .TITLE_NAME = t.NAME_VN,
                                                        .STAFF_RANK_ID = e.STAFF_RANK_ID,
                                                        .STAFF_RANK_NAME = staffrank.LEVEL_FROM,
                                                        .ORG_ID = e.ORG_ID,
                                                        .ORG_NAME = o.NAME_VN,
                                                        .TITLE_GROUP_NAME = titlegroup.NAME_VN,
                                                        .DM_ID = e.DM_ID}

                Return query.FirstOrDefault
            End If

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try


    End Function
    Public Function InsertListWorking1(ByVal objWorking As WorkingDTO, ByVal log As UserLog) As Boolean
        Try
            InsertWorking1(objWorking, log, 0)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetEmpSaved(ByVal Id As Integer) As EmployeeDTO
        Try
            Dim query = From p In Context.HU_EMPLOYEE
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From title In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
                        Where p.ID = Id

            Dim wel = query.Select(Function(p) New EmployeeDTO With {
                                     .EMPLOYEE_ID = p.p.ID,
                                     .TITLE_NAME_VN = p.title.NAME_VN,
                                     .ORG_NAME = p.org.NAME_VN,
                                     .EMPLOYEE_CODE = p.p.EMPLOYEE_CODE,
                                     .FULLNAME_VN = p.p.FULLNAME_VN
                           })
            Return wel.SingleOrDefault

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function InsertMngProfileSaved(ByVal lstMngProSaved As MngProfileSavedDTO, ByVal log As UserLog) As Boolean
        Dim check As Decimal = 0
        Dim bien = True
        Dim lst_empid As New List(Of Decimal)
        Dim updateHS As New HU_EMPLOYEE With {.ID = lstMngProSaved.EMPLOYEE_ID}
        Try
            check = (From p In Context.HU_MNGPROFILE_SAVED Where p.EMPLOYEE_ID = lstMngProSaved.EMPLOYEE_ID).ToList.Count
            If check > 0 Then
                Dim lst = (From p In Context.HU_MNGPROFILE_SAVED Where p.EMPLOYEE_ID = lstMngProSaved.EMPLOYEE_ID).ToList
                For Each line As HU_MNGPROFILE_SAVED In lst
                    Context.HU_MNGPROFILE_SAVED.DeleteObject(line)
                Next
            End If
            For Each line In lstMngProSaved.LIST_MNG
                Dim objMngProfileSaved As New HU_MNGPROFILE_SAVED
                objMngProfileSaved.ID = Utilities.GetNextSequence(Context, Context.HU_MNGPROFILE_SAVED.EntitySet.Name)
                objMngProfileSaved.EMPLOYEE_ID = line.EMPLOYEE_ID
                objMngProfileSaved.DOCUMENT_ID = line.DOCUMENT_ID
                objMngProfileSaved.DATE_SUBMIT = line.DATE_SUBMIT
                objMngProfileSaved.IS_SUBMITED = line.IS_SUBMITED

                If bien = True Then
                    If line.MUST_HAVE = True And line.IS_SUBMITED = False Then
                        bien = False
                    End If
                End If


                objMngProfileSaved.REMARK = line.REMARK
                objMngProfileSaved.UPLOAD_FILE = line.UPLOAD_FILE
                Context.HU_MNGPROFILE_SAVED.AddObject(objMngProfileSaved)
            Next
            'neu check du thi cap nhat cot sang hsnv la -1
            updateHS = (From p In Context.HU_EMPLOYEE Where p.ID = lstMngProSaved.EMPLOYEE_ID).FirstOrDefault()
            If bien = True Then
                updateHS.SUBMITED_ENOUGH = -1
            Else
                updateHS.SUBMITED_ENOUGH = 0
            End If

            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function InsertWorking1(ByVal objWorking As WorkingDTO,
                                 ByVal log As UserLog,
                                 ByRef gID As Decimal) As Boolean
        Dim iCount As Integer = 0

        Try

            Dim objWorkingData As New HU_WORKING
            objWorkingData.ID = Utilities.GetNextSequence(Context, Context.HU_WORKING.EntitySet.Name)
            objWorking.ID = objWorkingData.ID
            objWorkingData.EMPLOYEE_ID = objWorking.EMPLOYEE_ID
            objWorkingData.OBJECT_ATTENDANCE = objWorking.OBJECT_ATTENDANCE
            objWorkingData.FILING_DATE = objWorking.FILING_DATE
            objWorkingData.TITLE_ID = objWorking.TITLE_ID
            objWorkingData.ORG_ID = objWorking.ORG_ID
            objWorkingData.STAFF_RANK_ID = objWorking.STAFF_RANK_ID
            objWorkingData.STATUS_ID = objWorking.STATUS_ID
            objWorkingData.SALE_COMMISION_ID = objWorking.SALE_COMMISION_ID
            objWorkingData.EFFECT_DATE = objWorking.EFFECT_DATE
            objWorkingData.EXPIRE_DATE = objWorking.EXPIRE_DATE
            objWorkingData.DECISION_TYPE_ID = objWorking.DECISION_TYPE_ID
            objWorkingData.DECISION_NO = objWorking.DECISION_NO
            objWorkingData.SAL_TYPE_ID = objWorking.SAL_TYPE_ID
            objWorkingData.SAL_GROUP_ID = objWorking.SAL_GROUP_ID
            objWorkingData.SAL_LEVEL_ID = objWorking.SAL_LEVEL_ID
            objWorkingData.SAL_RANK_ID = objWorking.SAL_RANK_ID
            objWorkingData.SAL_BASIC = objWorking.SAL_BASIC
            objWorkingData.COST_SUPPORT = objWorking.COST_SUPPORT
            objWorkingData.DIRECT_MANAGER = objWorking.DIRECT_MANAGER
            objWorkingData.SIGN_DATE = objWorking.SIGN_DATE
            objWorkingData.SIGN_ID = objWorking.SIGN_ID
            objWorkingData.SIGN_NAME = objWorking.SIGN_NAME
            objWorkingData.SIGN_TITLE = objWorking.SIGN_TITLE
            objWorkingData.REMARK = objWorking.REMARK
            objWorkingData.IS_PROCESS = objWorking.IS_PROCESS
            objWorkingData.IS_MISSION = objWorking.IS_MISSION
            objWorkingData.IS_WAGE = objWorking.IS_WAGE
            objWorkingData.IS_3B = False
            objWorkingData.PERCENT_SALARY = objWorking.PERCENT_SALARY
            objWorkingData.STAFF_RANK_ID = objWorking.STAFF_RANK_ID
            objWorkingData.ATTACH_FILE = objWorking.ATTACH_FILE
            objWorkingData.FILENAME = objWorking.FILENAME
            objWorkingData.TAX_TABLE_ID = objWorking.TAX_TABLE_ID
            objWorkingData.SAL_TOTAL = objWorking.SAL_TOTAL
            objWorkingData.SAL_INS = objWorking.SAL_INS
            objWorkingData.ALLOWANCE_TOTAL = objWorking.ALLOWANCE_TOTAL
            objWorkingData.SAL_INS = objWorking.SAL_INS
            objWorkingData.ATTACH_FILE = objWorking.ATTACH_FILE
            objWorkingData.PERCENTSALARY = objWorking.PERCENTSALARY
            objWorkingData.FACTORSALARY = objWorking.FACTORSALARY
            objWorkingData.OTHERSALARY1 = objWorking.OTHERSALARY1
            objWorkingData.OTHERSALARY2 = objWorking.OTHERSALARY2
            objWorkingData.OTHERSALARY3 = objWorking.OTHERSALARY3
            objWorkingData.OTHERSALARY4 = objWorking.OTHERSALARY4
            objWorkingData.OTHERSALARY5 = objWorking.OTHERSALARY5
            objWorkingData.OBJECT_LABOR = objWorking.OBJECT_LABOR
            objWorkingData.OBJECT_ATTENDANT_ID = objWorking.OBJECT_ATTENDANT_ID
            objWorkingData.OBJECT_EMPLOYEE_ID = objWorking.OBJECT_EMPLOYEE_ID
            objWorkingData.WORK_PLACE_ID = objWorking.WORK_PLACE_ID
            Context.HU_WORKING.AddObject(objWorkingData)

            ' nếu phê duyệt
            If objWorking.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID Then
                ApproveWorking1(objWorking)

            End If

            If objWorking.lstAllowance IsNot Nothing Then
                objWorking.ALLOWANCE_TOTAL = objWorking.lstAllowance.Sum(Function(f) f.AMOUNT)
                For Each item In objWorking.lstAllowance
                    If objWorking.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID Then
                        Dim objALlow = (From p In Context.HU_WORKING_ALLOW
                                        From w In Context.HU_WORKING.Where(Function(f) f.ID = p.HU_WORKING_ID)
                                        Where p.ALLOWANCE_LIST_ID = item.ALLOWANCE_LIST_ID And
                                        w.EMPLOYEE_ID = objWorking.EMPLOYEE_ID And
                                        p.EFFECT_DATE < item.EFFECT_DATE And
                                        p.EXPIRE_DATE Is Nothing
                                        Select p).FirstOrDefault

                        If objALlow IsNot Nothing Then
                            objALlow.EXPIRE_DATE = item.EFFECT_DATE.Value.AddDays(-1)
                        End If
                    End If
                    Dim allow As New HU_WORKING_ALLOW
                    allow.ID = Utilities.GetNextSequence(Context, Context.HU_WORKING_ALLOW.EntitySet.Name)
                    allow.HU_WORKING_ID = objWorkingData.ID
                    allow.ALLOWANCE_LIST_ID = item.ALLOWANCE_LIST_ID
                    allow.AMOUNT = item.AMOUNT
                    allow.IS_INSURRANCE = item.IS_INSURRANCE
                    allow.EFFECT_DATE = item.EFFECT_DATE
                    allow.EXPIRE_DATE = item.EXPIRE_DATE
                    Context.HU_WORKING_ALLOW.AddObject(allow)
                Next
            End If


            'Dim insurranceAllow = Context.HU_WORKING_ALLOW.Where(Function(f) f.HU_WORKING_ID = objWorking.ID And f.IS_INSURRANCE = True).Sum(Function(f) f.AMOUNT)
            'If insurranceAllow.HasValue = False Then
            '    insurranceAllow = 0
            'End If
            'objWorkingData.SAL_INS = objWorkingData.SAL_BASIC + insurranceAllow
            Context.SaveChanges(log)
            'Dim sumAllowIns As Decimal?
            'Dim sumAllow = GetAllowanceTotalByDate(objWorking.EMPLOYEE_ID, objWorking.EFFECT_DATE, sumAllowIns)
            'Dim total As Decimal? = 0
            'If objWorkingData.SAL_BASIC IsNot Nothing Then
            '    total = total + objWorkingData.SAL_BASIC
            'End If
            If objWorking.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID Then
                Dim totalIns As Decimal = 0
                'If sumAllowIns IsNot Nothing Then
                '    totalIns += sumAllowIns
                'End If
                AutoGenInsChangeByWorking(objWorkingData.EMPLOYEE_ID,
                                 objWorkingData.ORG_ID,
                                 objWorkingData.TITLE_ID,
                                 objWorkingData.EFFECT_DATE,
                                 objWorkingData.SAL_INS,
                                 log)
            End If


            Context.SaveChanges()

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Return False
        End Try

    End Function

    Public Function InsertWorking(ByVal objWorking As WorkingDTO,
                                   ByVal log As UserLog,
                                   ByRef gID As Decimal) As Boolean
        Dim iCount As Integer = 0

        Try
            Dim objWorkingData As New HU_WORKING
            objWorkingData.ID = Utilities.GetNextSequence(Context, Context.HU_WORKING.EntitySet.Name)
            objWorking.ID = objWorkingData.ID
            objWorkingData.EMPLOYEE_ID = objWorking.EMPLOYEE_ID

            objWorkingData.EMPLOYEE_TYPE = objWorking.EMPLOYEE_TYPE
            objWorkingData.SALARY_BHXH = objWorking.SALARY_BHXH
            ' objWorkingData.OBJECT_ATTENDANCE = objWorking.OBJECT_ATTENDANCE
            objWorkingData.FILING_DATE = objWorking.FILING_DATE
            objWorkingData.TITLE_ID = objWorking.TITLE_ID
            objWorkingData.ORG_ID = objWorking.ORG_ID
            objWorkingData.OBJECT_ATTENDANCE = objWorking.CODE_ATTENDANCE
            objWorkingData.STAFF_RANK_ID = objWorking.STAFF_RANK_ID
            objWorkingData.STATUS_ID = objWorking.STATUS_ID
            objWorkingData.SALE_COMMISION_ID = objWorking.SALE_COMMISION_ID
            objWorkingData.SAL_PAYMENT_ID = objWorking.SAL_PAYMENT_ID
            objWorkingData.EFFECT_DATE = objWorking.EFFECT_DATE
            objWorkingData.EXPIRE_DATE = objWorking.EXPIRE_DATE
            objWorkingData.DECISION_TYPE_ID = objWorking.DECISION_TYPE_ID
            objWorkingData.DECISION_NO = objWorking.DECISION_NO
            objWorkingData.SAL_TYPE_ID = objWorking.SAL_TYPE_ID
            objWorkingData.SAL_GROUP_ID = objWorking.SAL_GROUP_ID
            objWorkingData.SAL_LEVEL_ID = objWorking.SAL_LEVEL_ID
            objWorkingData.SAL_RANK_ID = objWorking.SAL_RANK_ID
            objWorkingData.SAL_BASIC = objWorking.SAL_BASIC
            objWorkingData.COST_SUPPORT = objWorking.COST_SUPPORT
            objWorkingData.DIRECT_MANAGER = objWorking.DIRECT_MANAGER
            objWorkingData.SIGN_DATE = objWorking.SIGN_DATE
            objWorkingData.SIGN_ID = objWorking.SIGN_ID
            objWorkingData.SIGN_NAME = objWorking.SIGN_NAME
            objWorkingData.SIGN_TITLE = objWorking.SIGN_TITLE
            objWorkingData.REMARK = objWorking.REMARK
            objWorkingData.IS_PROCESS = objWorking.IS_PROCESS
            objWorkingData.IS_MISSION = objWorking.IS_MISSION
            objWorkingData.IS_WAGE = objWorking.IS_WAGE
            objWorkingData.IS_3B = False
            objWorkingData.PERCENT_SALARY = objWorking.PERCENT_SALARY
            objWorkingData.STAFF_RANK_ID = objWorking.STAFF_RANK_ID
            objWorkingData.ATTACH_FILE = objWorking.ATTACH_FILE
            objWorkingData.FILENAME = objWorking.FILENAME
            objWorkingData.TAX_TABLE_ID = objWorking.TAX_TABLE_ID
            objWorkingData.SAL_TOTAL = objWorking.SAL_TOTAL
            objWorkingData.ALLOWANCE_TOTAL = objWorking.ALLOWANCE_TOTAL
            objWorkingData.SAL_INS = objWorking.SALARY_BHXH
            objWorkingData.ATTACH_FILE = objWorking.ATTACH_FILE
            objWorkingData.PERCENTSALARY = objWorking.PERCENTSALARY
            objWorkingData.FACTORSALARY = objWorking.FACTORSALARY
            objWorkingData.OTHERSALARY1 = objWorking.OTHERSALARY1
            objWorkingData.OTHERSALARY2 = objWorking.OTHERSALARY2
            objWorkingData.OTHERSALARY3 = objWorking.OTHERSALARY3
            objWorkingData.OTHERSALARY4 = objWorking.OTHERSALARY4
            objWorkingData.OTHERSALARY5 = objWorking.OTHERSALARY5
            objWorkingData.REGION_SAL = objWorking.REGION_SAL
            objWorkingData.GAS_SAL = objWorking.GAS_SAL
            objWorkingData.ADDITIONAL_SAL = objWorking.ADDITIONAL_SAL
            objWorkingData.PHONE_SAL = objWorking.PHONE_SAL
            objWorkingData.SHARE_SAL = objWorking.SHARE_SAL
            objWorkingData.TOXIC_RATE = objWorking.TOXIC_RATE
            objWorkingData.TOXIC_SALARY = objWorking.TOXIC_SALARY
            objWorkingData.NEXTSALARY_DATE = objWorking.NEXTSALARY_DATE
            objWorkingData.PC1 = objWorking.PC1
            objWorkingData.PC2 = objWorking.PC2
            objWorkingData.PC3 = objWorking.PC3
            objWorkingData.PC4 = objWorking.PC4
            objWorkingData.PC5 = objWorking.PC5
            objWorkingData.IS_ALLOW_SALARY_LESS_THAN = objWorking.IS_ALLOW_SALARY_LESS_THAN
            Context.HU_WORKING.AddObject(objWorkingData)

            ' nếu phê duyệt
            If objWorking.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID Then
                ApproveWorking(objWorking)
            End If

            If objWorking.lstAllowance IsNot Nothing Then
                objWorking.ALLOWANCE_TOTAL = objWorking.lstAllowance.Sum(Function(f) f.AMOUNT)
                For Each item In objWorking.lstAllowance
                    If objWorking.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID Then
                        Dim objALlow = (From p In Context.HU_WORKING_ALLOW
                                        From w In Context.HU_WORKING.Where(Function(f) f.ID = p.HU_WORKING_ID)
                                        Where p.ALLOWANCE_LIST_ID = item.ALLOWANCE_LIST_ID And
                                        w.EMPLOYEE_ID = objWorking.EMPLOYEE_ID And
                                        p.EFFECT_DATE < item.EFFECT_DATE And
                                        p.EXPIRE_DATE Is Nothing
                                        Select p).FirstOrDefault

                        If objALlow IsNot Nothing Then
                            objALlow.EXPIRE_DATE = item.EFFECT_DATE.Value.AddDays(-1)
                        End If
                    End If
                    Dim allow As New HU_WORKING_ALLOW
                    allow.ID = Utilities.GetNextSequence(Context, Context.HU_WORKING_ALLOW.EntitySet.Name)
                    allow.HU_WORKING_ID = objWorkingData.ID
                    allow.ALLOWANCE_LIST_ID = item.ALLOWANCE_LIST_ID
                    allow.AMOUNT = item.AMOUNT
                    allow.IS_INSURRANCE = item.IS_INSURRANCE
                    allow.EFFECT_DATE = item.EFFECT_DATE
                    allow.EXPIRE_DATE = item.EXPIRE_DATE
                    Context.HU_WORKING_ALLOW.AddObject(allow)
                Next
            End If


            'Dim insurranceAllow = Context.HU_WORKING_ALLOW.Where(Function(f) f.HU_WORKING_ID = objWorking.ID And f.IS_INSURRANCE = True).Sum(Function(f) f.AMOUNT)
            'If insurranceAllow.HasValue = False Then
            '    insurranceAllow = 0
            'End If
            'objWorkingData.SAL_INS = objWorkingData.SAL_BASIC + insurranceAllow
            Context.SaveChanges(log)
            'Dim sumAllowIns As Decimal?
            'Dim sumAllow = GetAllowanceTotalByDate(objWorking.EMPLOYEE_ID, objWorking.EFFECT_DATE, sumAllowIns)
            'Dim total As Decimal? = 0
            'If objWorkingData.SAL_BASIC IsNot Nothing Then
            '    total = total + objWorkingData.SAL_BASIC
            'End If
            If objWorking.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID Then
                Dim totalIns As Decimal = 0
                'If sumAllowIns IsNot Nothing Then
                '    totalIns += sumAllowIns
                'End If
                AutoGenInsChangeByWorking(objWorkingData.EMPLOYEE_ID,
                                 objWorkingData.ORG_ID,
                                 objWorkingData.TITLE_ID,
                                 objWorkingData.EFFECT_DATE,
                                 objWorkingData.SAL_INS,
                                 log)
            End If


            Context.SaveChanges()

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Return False
        End Try

    End Function

    Public Function GetAllowanceTotalByDate(ByVal employeeID As Decimal,
                                            ByVal dDate As Date,
                                            ByRef dInsuran As Decimal?) As Decimal?

        Dim sumAllow = (From p In Context.HU_WORKING_ALLOW
                        From w In Context.HU_WORKING.Where(Function(f) f.ID = p.HU_WORKING_ID)
                        Where w.EMPLOYEE_ID = employeeID And
                        ((p.EXPIRE_DATE Is Nothing And p.EFFECT_DATE <= dDate) Or
                         (p.EXPIRE_DATE IsNot Nothing And p.EFFECT_DATE <= dDate And p.EXPIRE_DATE >= dDate))
                        Select p.AMOUNT).Sum

        dInsuran = (From p In Context.HU_WORKING_ALLOW
                    From w In Context.HU_WORKING.Where(Function(f) f.ID = p.HU_WORKING_ID)
                    Where w.EMPLOYEE_ID = employeeID And
                    ((p.EXPIRE_DATE Is Nothing And p.EFFECT_DATE <= dDate) Or
                     (p.EXPIRE_DATE IsNot Nothing And p.EFFECT_DATE <= dDate And p.EXPIRE_DATE >= dDate)) And
                    p.IS_INSURRANCE = -1
                    Select p.AMOUNT).Sum

        Return sumAllow
    End Function
    Public Function ModifyWorking1(ByVal objWorking As WorkingDTO,
                                 ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        Try
            Dim objWorkingData = (From p In Context.HU_WORKING Where objWorking.ID = p.ID).First()
            objWorkingData.EMPLOYEE_ID = objWorking.EMPLOYEE_ID
            objWorkingData.TITLE_ID = objWorking.TITLE_ID
            objWorkingData.ORG_ID = objWorking.ORG_ID
            objWorkingData.OBJECT_ATTENDANCE = objWorking.OBJECT_ATTENDANCE

            objWorkingData.EMPLOYEE_TYPE = objWorking.EMPLOYEE_TYPE
            objWorkingData.SALARY_BHXH = objWorking.SALARY_BHXH
            'objWorkingData.OBJECT_ATTENDANCE = objWorking.CODE_ATTENDANCE
            objWorkingData.FILING_DATE = objWorking.FILING_DATE
            objWorkingData.STAFF_RANK_ID = objWorking.STAFF_RANK_ID
            objWorkingData.STATUS_ID = objWorking.STATUS_ID
            objWorkingData.EFFECT_DATE = objWorking.EFFECT_DATE
            objWorkingData.EXPIRE_DATE = objWorking.EXPIRE_DATE
            objWorkingData.DECISION_TYPE_ID = objWorking.DECISION_TYPE_ID
            objWorkingData.DECISION_NO = objWorking.DECISION_NO
            objWorkingData.SAL_TYPE_ID = objWorking.SAL_TYPE_ID
            objWorkingData.SAL_GROUP_ID = objWorking.SAL_GROUP_ID
            objWorkingData.SAL_LEVEL_ID = objWorking.SAL_LEVEL_ID
            objWorkingData.SAL_RANK_ID = objWorking.SAL_RANK_ID
            objWorkingData.SALE_COMMISION_ID = objWorking.SALE_COMMISION_ID
            objWorkingData.SAL_BASIC = objWorking.SAL_BASIC
            objWorkingData.COST_SUPPORT = objWorking.COST_SUPPORT
            objWorkingData.DIRECT_MANAGER = objWorking.DIRECT_MANAGER
            objWorkingData.SIGN_DATE = objWorking.SIGN_DATE
            objWorkingData.SIGN_ID = objWorking.SIGN_ID
            objWorkingData.SIGN_NAME = objWorking.SIGN_NAME
            objWorkingData.SIGN_TITLE = objWorking.SIGN_TITLE
            objWorkingData.REMARK = objWorking.REMARK
            objWorkingData.IS_PROCESS = objWorking.IS_PROCESS
            objWorkingData.IS_MISSION = objWorking.IS_MISSION
            objWorkingData.PERCENT_SALARY = objWorking.PERCENT_SALARY
            objWorkingData.IS_WAGE = objWorking.IS_WAGE
            objWorkingData.IS_3B = False
            objWorkingData.SAL_TOTAL = objWorking.SAL_TOTAL
            objWorkingData.SAL_INS = objWorking.SAL_INS
            objWorkingData.TAX_TABLE_ID = objWorking.TAX_TABLE_ID
            objWorkingData.ATTACH_FILE = objWorking.ATTACH_FILE
            objWorkingData.FILENAME = objWorking.FILENAME
            objWorkingData.PERCENTSALARY = objWorking.PERCENTSALARY
            objWorkingData.FACTORSALARY = objWorking.FACTORSALARY
            objWorkingData.OTHERSALARY1 = objWorking.OTHERSALARY1
            objWorkingData.OTHERSALARY2 = objWorking.OTHERSALARY2
            objWorkingData.OTHERSALARY3 = objWorking.OTHERSALARY3
            objWorkingData.OTHERSALARY4 = objWorking.OTHERSALARY4
            objWorkingData.OTHERSALARY5 = objWorking.OTHERSALARY5
            objWorkingData.OBJECT_LABOR = objWorking.OBJECT_LABOR
            objWorkingData.OBJECT_ATTENDANT_ID = objWorking.OBJECT_ATTENDANT_ID
            objWorkingData.OBJECT_EMPLOYEE_ID = objWorking.OBJECT_EMPLOYEE_ID
            objWorkingData.WORK_PLACE_ID = objWorking.WORK_PLACE_ID
            objWorkingData.PC1 = objWorking.PC1
            objWorkingData.PC2 = objWorking.PC2
            objWorkingData.PC3 = objWorking.PC3
            objWorkingData.PC4 = objWorking.PC4
            If objWorking.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID Then
                ApproveWorking1(objWorking)
            End If

            If objWorking.lstAllowance IsNot Nothing Then
                objWorking.ALLOWANCE_TOTAL = objWorking.lstAllowance.Sum(Function(f) f.AMOUNT)
                Dim lstAll = (From p In Context.HU_WORKING_ALLOW Where p.HU_WORKING_ID = objWorking.ID).ToList
                For Each item In lstAll
                    Context.HU_WORKING_ALLOW.DeleteObject(item)
                Next

                For Each item In objWorking.lstAllowance
                    If objWorking.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID Then
                        Dim objALlow = (From p In Context.HU_WORKING_ALLOW
                                        From w In Context.HU_WORKING.Where(Function(f) f.ID = p.HU_WORKING_ID)
                                        Where p.ALLOWANCE_LIST_ID = item.ALLOWANCE_LIST_ID And
                                        w.EMPLOYEE_ID = objWorking.EMPLOYEE_ID And
                                        p.EFFECT_DATE < item.EFFECT_DATE And
                                        p.EXPIRE_DATE Is Nothing And w.ID <> objWorking.ID
                                        Select p).FirstOrDefault

                        If objALlow IsNot Nothing Then
                            objALlow.EXPIRE_DATE = item.EFFECT_DATE.Value.AddDays(-1)
                        End If
                    End If
                    Dim allow As New HU_WORKING_ALLOW
                    allow.ID = Utilities.GetNextSequence(Context, Context.HU_WORKING_ALLOW.EntitySet.Name)
                    allow.HU_WORKING_ID = objWorkingData.ID
                    allow.ALLOWANCE_LIST_ID = item.ALLOWANCE_LIST_ID
                    allow.AMOUNT = item.AMOUNT
                    allow.IS_INSURRANCE = item.IS_INSURRANCE
                    allow.EFFECT_DATE = item.EFFECT_DATE
                    allow.EXPIRE_DATE = item.EXPIRE_DATE
                    Context.HU_WORKING_ALLOW.AddObject(allow)
                Next
            End If

            Context.SaveChanges(log)
            objWorkingData.ALLOWANCE_TOTAL = Context.HU_WORKING_ALLOW.Where(Function(f) f.HU_WORKING_ID = objWorking.ID).Sum(Function(f) f.AMOUNT)
            Dim allowanceTotal = 0
            If objWorkingData.ALLOWANCE_TOTAL.HasValue Then
                allowanceTotal = objWorkingData.ALLOWANCE_TOTAL.Value
            End If
            objWorkingData.SAL_TOTAL = objWorkingData.SAL_BASIC + allowanceTotal

            Dim insurranceAllow = Context.HU_WORKING_ALLOW.Where(Function(f) f.HU_WORKING_ID = objWorking.ID And f.IS_INSURRANCE = True).Sum(Function(f) f.AMOUNT)
            If insurranceAllow.HasValue = False Then
                insurranceAllow = 0
            End If
            objWorkingData.SAL_INS = objWorkingData.SAL_INS

            'Dim sumAllowIns As Decimal?
            'Dim sumAllow = GetAllowanceTotalByDate(objWorking.EMPLOYEE_ID, objWorking.EFFECT_DATE, sumAllowIns)
            ' Dim total As Decimal? = 0
            'If objWorkingData.SAL_BASIC IsNot Nothing Then
            '    total = total + objWorkingData.SAL_BASIC
            'End If
            If objWorking.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID Then
                'Dim totalIns As Decimal = 0
                'If sumAllowIns IsNot Nothing Then
                '    totalIns += sumAllowIns
                'End If
                AutoGenInsChangeByWorking(objWorkingData.EMPLOYEE_ID,
                                 objWorkingData.ORG_ID,
                                 objWorkingData.TITLE_ID,
                                 objWorkingData.EFFECT_DATE,
                                          objWorkingData.SAL_INS,
                                 log)
            End If
            'If objWorkingData.COST_SUPPORT IsNot Nothing Then
            '    total = total + objWorkingData.COST_SUPPORT
            'End If
            'If objWorkingData.PERCENT_SALARY IsNot Nothing Then
            '    total = total * objWorkingData.PERCENT_SALARY / 100
            'Else
            '    total = 0
            'End If
            'objWorkingData.SAL_TOTAL = total


            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Return False
        End Try

    End Function
    Public Function ModifyWorking(ByVal objWorking As WorkingDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        Try
            Dim objWorkingData = (From p In Context.HU_WORKING Where objWorking.ID = p.ID).First()
            objWorkingData.EMPLOYEE_ID = objWorking.EMPLOYEE_ID
            objWorkingData.TITLE_ID = objWorking.TITLE_ID
            objWorkingData.ORG_ID = objWorking.ORG_ID
            'objWorkingData.OBJECT_ATTENDANCE = objWorking.OBJECT_ATTENDANCE
            objWorkingData.OBJECT_ATTENDANCE = objWorking.CODE_ATTENDANCE
            objWorkingData.FILING_DATE = objWorking.FILING_DATE
            objWorkingData.STAFF_RANK_ID = objWorking.STAFF_RANK_ID
            objWorkingData.STATUS_ID = objWorking.STATUS_ID
            objWorkingData.EFFECT_DATE = objWorking.EFFECT_DATE
            objWorkingData.EXPIRE_DATE = objWorking.EXPIRE_DATE
            objWorkingData.DECISION_TYPE_ID = objWorking.DECISION_TYPE_ID
            objWorkingData.SAL_PAYMENT_ID = objWorking.SAL_PAYMENT_ID
            objWorkingData.DECISION_NO = objWorking.DECISION_NO
            objWorkingData.SAL_TYPE_ID = objWorking.SAL_TYPE_ID
            objWorkingData.SAL_GROUP_ID = objWorking.SAL_GROUP_ID
            objWorkingData.SAL_LEVEL_ID = objWorking.SAL_LEVEL_ID
            objWorkingData.SAL_RANK_ID = objWorking.SAL_RANK_ID
            objWorkingData.SALE_COMMISION_ID = objWorking.SALE_COMMISION_ID
            objWorkingData.SAL_BASIC = objWorking.SAL_BASIC
            objWorkingData.COST_SUPPORT = objWorking.COST_SUPPORT
            objWorkingData.DIRECT_MANAGER = objWorking.DIRECT_MANAGER
            objWorkingData.SIGN_DATE = objWorking.SIGN_DATE
            objWorkingData.SIGN_ID = objWorking.SIGN_ID
            objWorkingData.SIGN_NAME = objWorking.SIGN_NAME
            objWorkingData.SIGN_TITLE = objWorking.SIGN_TITLE
            objWorkingData.REMARK = objWorking.REMARK
            objWorkingData.IS_PROCESS = objWorking.IS_PROCESS
            objWorkingData.IS_MISSION = objWorking.IS_MISSION
            objWorkingData.PERCENT_SALARY = objWorking.PERCENT_SALARY
            objWorkingData.IS_WAGE = objWorking.IS_WAGE
            objWorkingData.IS_3B = False
            objWorkingData.EMPLOYEE_TYPE = objWorking.EMPLOYEE_TYPE
            objWorkingData.SAL_TOTAL = objWorking.SAL_TOTAL
            objWorkingData.SALARY_BHXH = objWorking.SALARY_BHXH



            objWorkingData.SAL_INS = objWorking.SALARY_BHXH
            objWorkingData.TAX_TABLE_ID = objWorking.TAX_TABLE_ID
            objWorkingData.ATTACH_FILE = objWorking.ATTACH_FILE
            objWorkingData.FILENAME = objWorking.FILENAME
            objWorkingData.PERCENTSALARY = objWorking.PERCENTSALARY
            objWorkingData.FACTORSALARY = objWorking.FACTORSALARY
            objWorkingData.OTHERSALARY1 = objWorking.OTHERSALARY1
            objWorkingData.OTHERSALARY2 = objWorking.OTHERSALARY2
            objWorkingData.OTHERSALARY3 = objWorking.OTHERSALARY3
            objWorkingData.OTHERSALARY4 = objWorking.OTHERSALARY4
            objWorkingData.OTHERSALARY5 = objWorking.OTHERSALARY5
            objWorkingData.REGION_SAL = objWorking.REGION_SAL
            objWorkingData.GAS_SAL = objWorking.GAS_SAL
            objWorkingData.ADDITIONAL_SAL = objWorking.ADDITIONAL_SAL
            objWorkingData.PHONE_SAL = objWorking.PHONE_SAL
            objWorkingData.SHARE_SAL = objWorking.SHARE_SAL
            objWorkingData.TOXIC_RATE = objWorking.TOXIC_RATE
            objWorkingData.TOXIC_SALARY = objWorking.TOXIC_SALARY
            objWorkingData.NEXTSALARY_DATE = objWorking.NEXTSALARY_DATE
            objWorkingData.PC1 = objWorking.PC1
            objWorkingData.PC2 = objWorking.PC2
            objWorkingData.PC3 = objWorking.PC3
            objWorkingData.PC4 = objWorking.PC4
            objWorkingData.PC5 = objWorking.PC5
            objWorkingData.IS_ALLOW_SALARY_LESS_THAN = objWorking.IS_ALLOW_SALARY_LESS_THAN
            If objWorking.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID Then
                ApproveWorking(objWorking)
            End If

            If objWorking.lstAllowance IsNot Nothing Then
                objWorking.ALLOWANCE_TOTAL = objWorking.lstAllowance.Sum(Function(f) f.AMOUNT)
                Dim lstAll = (From p In Context.HU_WORKING_ALLOW Where p.HU_WORKING_ID = objWorking.ID).ToList
                For Each item In lstAll
                    Context.HU_WORKING_ALLOW.DeleteObject(item)
                Next

                For Each item In objWorking.lstAllowance
                    If objWorking.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID Then
                        Dim objALlow = (From p In Context.HU_WORKING_ALLOW
                                        From w In Context.HU_WORKING.Where(Function(f) f.ID = p.HU_WORKING_ID)
                                        Where p.ALLOWANCE_LIST_ID = item.ALLOWANCE_LIST_ID And
                                        w.EMPLOYEE_ID = objWorking.EMPLOYEE_ID And
                                        p.EFFECT_DATE < item.EFFECT_DATE And
                                        p.EXPIRE_DATE Is Nothing And w.ID <> objWorking.ID
                                        Select p).FirstOrDefault

                        If objALlow IsNot Nothing Then
                            objALlow.EXPIRE_DATE = item.EFFECT_DATE.Value.AddDays(-1)
                        End If
                    End If
                    Dim allow As New HU_WORKING_ALLOW
                    allow.ID = Utilities.GetNextSequence(Context, Context.HU_WORKING_ALLOW.EntitySet.Name)
                    allow.HU_WORKING_ID = objWorkingData.ID
                    allow.ALLOWANCE_LIST_ID = item.ALLOWANCE_LIST_ID
                    allow.AMOUNT = item.AMOUNT
                    allow.IS_INSURRANCE = item.IS_INSURRANCE
                    allow.EFFECT_DATE = item.EFFECT_DATE
                    allow.EXPIRE_DATE = item.EXPIRE_DATE
                    Context.HU_WORKING_ALLOW.AddObject(allow)
                Next
            End If

            Context.SaveChanges(log)
            objWorkingData.ALLOWANCE_TOTAL = Context.HU_WORKING_ALLOW.Where(Function(f) f.HU_WORKING_ID = objWorking.ID).Sum(Function(f) f.AMOUNT)
            Dim allowanceTotal = 0
            If objWorkingData.ALLOWANCE_TOTAL.HasValue Then
                allowanceTotal = objWorkingData.ALLOWANCE_TOTAL.Value
            End If
            objWorkingData.SAL_TOTAL = objWorking.SAL_TOTAL + allowanceTotal

            Dim insurranceAllow = Context.HU_WORKING_ALLOW.Where(Function(f) f.HU_WORKING_ID = objWorking.ID And f.IS_INSURRANCE = True).Sum(Function(f) f.AMOUNT)
            If insurranceAllow.HasValue = False Then
                insurranceAllow = 0
            End If
            'objWorkingData.SAL_INS = objWorkingData.SAL_INS

            'Dim sumAllowIns As Decimal?
            'Dim sumAllow = GetAllowanceTotalByDate(objWorking.EMPLOYEE_ID, objWorking.EFFECT_DATE, sumAllowIns)
            ' Dim total As Decimal? = 0
            'If objWorkingData.SAL_BASIC IsNot Nothing Then
            '    total = total + objWorkingData.SAL_BASIC
            'End If
            If objWorking.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID Then
                'Dim totalIns As Decimal = 0
                'If sumAllowIns IsNot Nothing Then
                '    totalIns += sumAllowIns
                'End If
                AutoGenInsChangeByWorking(objWorkingData.EMPLOYEE_ID,
                                 objWorkingData.ORG_ID,
                                 objWorkingData.TITLE_ID,
                                 objWorkingData.EFFECT_DATE,
                                          objWorkingData.SAL_INS,
                                 log)
            End If
            'If objWorkingData.COST_SUPPORT IsNot Nothing Then
            '    total = total + objWorkingData.COST_SUPPORT
            'End If
            'If objWorkingData.PERCENT_SALARY IsNot Nothing Then
            '    total = total * objWorkingData.PERCENT_SALARY / 100
            'Else
            '    total = 0
            'End If
            'objWorkingData.SAL_TOTAL = total


            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Return False
        End Try

    End Function

    Private Function AutoGenInsChangeByWorking(ByVal employeeID As Decimal,
                                               ByVal orgID As Decimal,
                                               ByVal titleID As Decimal,
                                               ByVal effectDate As Date,
                                               ByVal total As Decimal?,
                                               ByVal log As UserLog) As Boolean



        Try
            Return True
            '1. Check khai báo đóng mới - INS_INFOMATION
            If (From p In Context.INS_INFORMATION Where p.EMPLOYEE_ID = employeeID).Count = 0 Then
                Return False
            End If
            '2. Lấy biến động gần nhất theo ngày hiệu lực truyền vào
            Dim insChangeBefore = (From p In Context.INS_CHANGE
                                   Where p.EMPLOYEE_ID = employeeID And
                                   p.EFFECTDATE <= effectDate
                                   Order By p.EFFECTDATE Descending).FirstOrDefault

            '3. lấy mức trần đóng bảo hiểm để kiểm tra xem mức lương có vượt mức trần không. nếu vựt thì lấy mức trần. không vượt thì lấy mức lương đóng bảo hiểm
            Dim insObject = (From p In Context.INS_SPECIFIED_OBJECTS
                             Where p.EFFECTIVE_DATE <= effectDate
                             Order By p.EFFECTIVE_DATE).FirstOrDefault


            '4. So sánh lương gần nhất với tổng lương truyền vào. lớn hơn thì biến động tăng, nhỏ hơn thì biến động giảm
            ' Đẩy data vào bảng biến động INS_CHANGE
            If insChangeBefore IsNot Nothing Then
                If insChangeBefore.NEWSALARY IsNot Nothing And insChangeBefore.CHANGE_TYPE <> 4 Then
                    If insChangeBefore.NEWSALARY = total Then
                        Return False
                    End If
                    Dim insChangeNew As New INS_CHANGE
                    insChangeNew.ID = Utilities.GetNextSequence(Context, Context.INS_CHANGE.EntitySet.Name)
                    insChangeNew.EMPLOYEE_ID = employeeID
                    insChangeNew.ORG_ID = orgID
                    insChangeNew.TITLE_ID = titleID
                    ' kiểm tra nếu lương cũ < hơn lương mới chuyền vào thì đó là biến động tắng tăng mức đóng, ko là giảm mức đóng
                    If insChangeBefore.NEWSALARY < total Then
                        insChangeNew.CHANGE_TYPE = 3
                    Else
                        insChangeNew.CHANGE_TYPE = 65
                    End If
                    insChangeNew.OLDSALARY = insChangeBefore.NEWSALARY
                    If insObject IsNot Nothing Then ' check mức trần đóng bảo hiểm xã hội.
                        If insObject.SI IsNot Nothing Then
                            If total < insObject.SI Then
                                insChangeNew.NEWSALARY = total
                            Else
                                insChangeNew.NEWSALARY = insObject.SI
                            End If
                        End If
                    End If
                    insChangeNew.EFFECTDATE = effectDate
                    insChangeNew.CREATED_BY = log.Username
                    insChangeNew.CREATED_DATE = DateTime.Now
                    insChangeNew.CREATED_LOG = log.Ip & "-" & log.ComputerName
                    insChangeNew.MODIFIED_BY = log.Username
                    insChangeNew.MODIFIED_DATE = DateTime.Now
                    insChangeNew.MODIFIED_LOG = log.Ip & "-" & log.ComputerName
                    If effectDate.Day >= 15 Then
                        insChangeNew.CHANGE_MONTH = effectDate.AddMonths(1)
                    Else
                        insChangeNew.CHANGE_MONTH = effectDate
                    End If
                    insChangeNew.NOTE = "HiStaff tự sinh biến động thay đổi mức đóng bảo hiểm từ Quyết định"
                    Context.INS_CHANGE.AddObject(insChangeNew)

                    '' update lại mức lương đóng bảo hiểm của nhân viên này trên form quản lý thông tin bảo hiểm
                    'Dim insinfo As New INS_INFORMATION With {.EMPLOYEE_ID = employeeID}
                    'insinfo = (From p In Context.INS_INFORMATION Where p.EMPLOYEE_ID = employeeID).SingleOrDefault
                    'insinfo.SALARY = total
                    'Context.SaveChanges(log)
                End If
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function DeleteWorking(ByVal objWorking As WorkingDTO) As Boolean
        Dim objWorkingData As HU_WORKING
        Try

            objWorkingData = (From p In Context.HU_WORKING Where objWorking.ID = p.ID).FirstOrDefault
            If objWorkingData IsNot Nothing Then
                Dim lstAll = (From p In Context.HU_WORKING_ALLOW Where p.ID = objWorkingData.ID).ToList
                For Each item In lstAll
                    Context.HU_WORKING_ALLOW.DeleteObject(item)
                Next
                Context.HU_WORKING.DeleteObject(objWorkingData)
                Context.SaveChanges()
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function
    Public Function ApproveWorking1(ByVal objWorking As WorkingDTO) As Boolean

        Try
            ' Phê duyệt và cập nhật sang hồ sơ

            Dim item = (From p In Context.HU_EMPLOYEE Where objWorking.EMPLOYEE_ID = p.ID).FirstOrDefault
            If item.STAFF_RANK_ID <> objWorking.STAFF_RANK_ID AndAlso item.IS_KIEM_NHIEM Is Nothing Then
                Dim lstCon = (From p In Context.HU_CONCURRENTLY Where p.EMPLOYEE_ID = objWorking.EMPLOYEE_ID _
                                                              AndAlso (objWorking.EFFECT_DATE <= p.EXPIRE_DATE_CON OrElse p.EXPIRE_DATE_CON Is Nothing)).ToList
                For Each itemCon In lstCon
                    itemCon.STAFF_RANK_ID = objWorking.STAFF_RANK_ID
                    Dim objEmp = (From p In Context.HU_EMPLOYEE Where itemCon.ID = p.IS_KIEM_NHIEM).FirstOrDefault
                    If objEmp IsNot Nothing Then
                        objEmp.STAFF_RANK_ID = objWorking.STAFF_RANK_ID
                    End If
                Next
            End If
            If Format(objWorking.EFFECT_DATE, "yyyyMMdd") > Format(Date.Now, "yyyyMMdd") Then
                Exit Function
            End If

            If objWorking.IS_MISSION Then
                Dim titleId = (From p In Context.HU_EMPLOYEE Where p.ID = objWorking.EMPLOYEE_ID Select p.TITLE_ID).FirstOrDefault()
                Dim objTitleOld = (From p In Context.HU_TITLE Where p.ID = titleId).FirstOrDefault()
                If objTitleOld IsNot Nothing Then
                    If objTitleOld.MASTER.ToString = objWorking.EMPLOYEE_ID.ToString Then
                        objTitleOld.MASTER = Nothing
                    End If
                    If objTitleOld.INTERIM.ToString = objWorking.EMPLOYEE_ID.ToString Then
                        objTitleOld.INTERIM = Nothing
                    End If
                End If
                Dim objTitleNew = (From p In Context.HU_TITLE Where p.ID = objWorking.TITLE_ID).FirstOrDefault()
                If objTitleNew IsNot Nothing Then
                    If objTitleNew.MASTER IsNot Nothing Then
                        objTitleNew.INTERIM = objTitleNew.MASTER
                    End If
                    objTitleNew.MASTER = objWorking.EMPLOYEE_ID
                End If
            End If

            If item IsNot Nothing Then
                item.TITLE_ID = IIf(IsNumeric(objWorking.TITLE_ID), objWorking.TITLE_ID, item.TITLE_ID)
                item.ORG_ID = IIf(IsNumeric(objWorking.ORG_ID), objWorking.ORG_ID, item.ORG_ID) ' objWorking.ORG_ID
                item.STAFF_RANK_ID = IIf(IsNumeric(objWorking.STAFF_RANK_ID), objWorking.STAFF_RANK_ID, item.STAFF_RANK_ID) ' objWorking.STAFF_RANK_ID
                item.DIRECT_MANAGER = IIf(IsNumeric(objWorking.DIRECT_MANAGER), objWorking.DIRECT_MANAGER, item.DIRECT_MANAGER) 'objWorking.DIRECT_MANAGER
                item.LAST_WORKING_ID = IIf(IsNumeric(objWorking.ID), objWorking.ID, item.ID) ' objWorking.ID
                item.MODIFIED_DATE = Date.Now
                item.OBJECT_ATTENDANT_ID = IIf(IsNumeric(objWorking.OBJECT_ATTENDANT_ID), objWorking.OBJECT_ATTENDANT_ID, item.OBJECT_ATTENDANT_ID)
                item.OBJECT_EMPLOYEE_ID = IIf(IsNumeric(objWorking.OBJECT_EMPLOYEE_ID), objWorking.OBJECT_EMPLOYEE_ID, item.OBJECT_EMPLOYEE_ID)
                item.WORK_PLACE_ID = IIf(IsNumeric(objWorking.WORK_PLACE_ID), objWorking.WORK_PLACE_ID, item.WORK_PLACE_ID)
                item.OBJECTTIMEKEEPING = IIf(IsNumeric(objWorking.OBJECT_ATTENDANCE), objWorking.OBJECT_ATTENDANCE, item.OBJECTTIMEKEEPING) 'objWorking.OBJECT_ATTENDANCE
                item.OBJECT_LABOR = IIf(IsNumeric(objWorking.OBJECT_LABOR), objWorking.OBJECT_LABOR, item.OBJECT_LABOR) ' objWorking.OBJECT_LABOR
            End If
            Update_EXPIRE_DATE_IN_WORKING(objWorking)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function
    Public Function Update_EXPIRE_DATE_IN_WORKING(ByVal objWorking As WorkingDTO) As Boolean

        Try
            ' Phê duyệt và cập nhật sang hồ sơ
            If objWorking.EFFECT_DATE IsNot Nothing Then
                Dim query_old = (From p In Context.HU_WORKING
                                 Where p.EFFECT_DATE < objWorking.EFFECT_DATE _
                             And p.EMPLOYEE_ID = objWorking.EMPLOYEE_ID _
                             And p.IS_MISSION = -1 And p.IS_WAGE = 0 And p.STATUS_ID = 447
                                 Order By p.EFFECT_DATE Descending).FirstOrDefault
                If query_old IsNot Nothing Then
                    Dim dates As Date = CDate(objWorking.EFFECT_DATE)
                    query_old.EXPIRE_DATE = dates.AddDays(-1)
                End If
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function
    'Public Function Del_EXPIRE_DATE_IN_WORKING(ByVal objWorking As WorkingDTO) As Boolean

    '    Try
    '        ' Phê duyệt và cập nhật sang hồ sơ
    '        If objWorking.EFFECT_DATE IsNot Nothing Then
    '            Dim query_old = (From p In Context.HU_WORKING
    '                             Where p.EFFECT_DATE < objWorking.EFFECT_DATE _
    '                         And p.EMPLOYEE_ID = objWorking.EMPLOYEE_ID _
    '                         And p.IS_MISSION = -1 And p.IS_WAGE = 0 And p.STATUS_ID = 447
    '                             Order By p.EFFECT_DATE Descending).FirstOrDefault
    '            If query_old IsNot Nothing AndAlso query_old.EXPIRE_DATE IsNot Nothing Then
    '                query_old.EXPIRE_DATE = Nothing
    '            End If
    '        End If
    '        Return True
    '    Catch ex As Exception
    '        WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
    '        Throw ex
    '    End Try

    'End Function
    Public Function ApproveWorking(ByVal objWorking As WorkingDTO) As Boolean

        Try
            ' Phê duyệt và cập nhật sang hồ sơ

            If Format(objWorking.EFFECT_DATE, "yyyyMMdd") > Format(Date.Now, "yyyyMMdd") Then
                Exit Function
            End If

            Dim item = (From p In Context.HU_EMPLOYEE Where objWorking.EMPLOYEE_ID = p.ID).FirstOrDefault
            If item IsNot Nothing Then

                item.LAST_WORKING_ID = objWorking.ID

            End If

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function ValidateWorking(ByVal sType As String, ByVal obj As WorkingDTO) As Boolean
        Try
            Select Case sType
                Case "VALIDATE_INS_REGION"
                    Dim salRegion = (From p In Context.HU_EMPLOYEE
                                     From cv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = p.ID)
                                     From ins In Context.INS_REGION.Where(Function(f) f.ID = cv.INS_REGION_ID)
                                     Where p.ID = obj.EMPLOYEE_ID
                                     Select ins.MONEY).FirstOrDefault
                    If salRegion Is Nothing Then
                        Return True
                    Else
                        Return Not (obj.SAL_BASIC < salRegion)
                    End If
                Case "EXIST_EFFECT_DATE"
                    Return (From e In Context.HU_WORKING
                            Where e.EMPLOYEE_ID = obj.EMPLOYEE_ID And
                            e.EFFECT_DATE >= obj.EFFECT_DATE And
                            e.IS_MISSION = -1 And
                            e.ID <> obj.ID And
                            e.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID).Count = 0
                Case "EXIST_MISSION_EFFECT_DATE"
                    Return (From e In Context.HU_WORKING
                            Where e.EMPLOYEE_ID = obj.EMPLOYEE_ID And
                            e.EFFECT_DATE >= obj.EFFECT_DATE And
                            e.ID <> obj.ID AndAlso
                            e.IS_MISSION = -1).Count = 0
                Case "EXIST_MISSION_EFFECT_DATE"
                    Return (From e In Context.HU_WORKING
                            Where e.EMPLOYEE_ID = obj.EMPLOYEE_ID And
                            e.EFFECT_DATE >= obj.EFFECT_DATE And
                            e.ID <> obj.ID AndAlso
                            e.IS_MISSION = -1).Count = 0
                Case "EXIST_EFFECT_DATE_IS_WAGE"
                    'e.IS_MISSION = -1 And
                    'e.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID
                    Return (From e In Context.HU_WORKING
                            Where e.EMPLOYEE_ID = obj.EMPLOYEE_ID And
                            e.EFFECT_DATE = obj.EFFECT_DATE And
                            e.IS_WAGE = -1 And
                            e.ID <> obj.ID).Count = 0
                Case "EXIST_DECISION_NO"
                    Return (From e In Context.HU_WORKING
                            Where e.DECISION_NO.ToUpper = obj.DECISION_NO.ToUpper And
                            e.ID <> obj.ID).Count = 0
                Case "3B_EXIST_WORKING"
                    Return (From p In Context.HU_CONTRACT
                            Where p.EMPLOYEE_ID = obj.EMPLOYEE_3B_ID And
                            p.STATUS_ID = ProfileCommon.OT_CONTRACT_STATUS.APPROVE_ID).Count = 0
            End Select
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".ValidateWorking")
            Throw ex
        End Try
    End Function

    Public Function ValEffectdateByEmpCode(ByVal emp_code As String, ByVal effect_date As Date) As Boolean
        Dim empID As Decimal = 0D
        Try
            empID = (From p In Context.HU_EMPLOYEE.Where(Function(f) f.EMPLOYEE_CODE.ToLower = emp_code.ToLower) Select p.ID).FirstOrDefault
            Dim query = (From p In Context.HU_WORKING.Where(Function(f) f.EMPLOYEE_ID = empID And f.EFFECT_DATE = effect_date AndAlso f.IS_WAGE = -1))
            Return query.Count = 0
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetAllowanceByDate(ByVal _filter As WorkingAllowanceDTO) As List(Of WorkingAllowanceDTO)

        Try
            Dim lst = (From p In Context.HU_WORKING_ALLOW
                       From w In Context.HU_WORKING.Where(Function(f) f.ID = p.HU_WORKING_ID)
                       From allow In Context.HU_ALLOWANCE_LIST.Where(Function(f) f.ID = p.ALLOWANCE_LIST_ID)
                       Where w.EMPLOYEE_ID = _filter.EMPLOYEE_ID And w.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID And
                       ((p.EXPIRE_DATE Is Nothing And p.EFFECT_DATE <= _filter.EFFECT_DATE) Or
                       (p.EXPIRE_DATE IsNot Nothing And p.EFFECT_DATE <= _filter.EFFECT_DATE And p.EXPIRE_DATE >= _filter.EFFECT_DATE))
                       Order By p.EFFECT_DATE Descending
                       Select New WorkingAllowanceDTO With {.ALLOWANCE_LIST_ID = p.ALLOWANCE_LIST_ID,
                                                            .ALLOWANCE_LIST_NAME = allow.NAME,
                                                            .AMOUNT = p.AMOUNT,
                                                            .EFFECT_DATE = p.EFFECT_DATE,
                                                            .EXPIRE_DATE = p.EXPIRE_DATE,
                                                            .IS_INSURRANCE = p.IS_INSURRANCE,
                                                            .HU_WORKING_ID = p.HU_WORKING_ID})

            If _filter.HU_WORKING_ID > 0 Then
                lst = lst.Where(Function(f) f.HU_WORKING_ID = _filter.HU_WORKING_ID)
            End If
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".GetAllowanceByDate")
            Throw ex
        End Try


    End Function

    Public Function GetAllowanceByWorkingID(ByVal _filter As WorkingAllowanceDTO) As List(Of WorkingAllowanceDTO)

        Try
            Dim query = From p In Context.HU_WORKING_ALLOW
                        From w In Context.HU_WORKING.Where(Function(f) f.ID = p.HU_WORKING_ID)
                        From allow In Context.HU_ALLOWANCE_LIST.Where(Function(f) f.ID = p.ALLOWANCE_LIST_ID)
                        Where w.ID = _filter.HU_WORKING_ID
                        Order By p.EFFECT_DATE Descending
                        Select New WorkingAllowanceDTO With {.ALLOWANCE_LIST_ID = p.ALLOWANCE_LIST_ID,
                                                             .ALLOWANCE_LIST_NAME = allow.NAME,
                                                             .AMOUNT = p.AMOUNT,
                                                             .EFFECT_DATE = p.EFFECT_DATE,
                                                             .EXPIRE_DATE = p.EXPIRE_DATE,
                                                             .IS_INSURRANCE = p.IS_INSURRANCE}

            Dim lst = query.ToList
            Return lst
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".GetAllowanceByWorkingID")
            Throw ex
        End Try


    End Function

    Public Function GetChangeInfoImport(ByVal param As ParamDTO,
                                        ByVal log As UserLog) As DataSet
        Try
            Using cls As New DataAccess.QueryData
                Dim dsData As DataSet = cls.ExecuteStore("PKG_PROFILE_BUSINESS.GET_CHANGEINFO_IMPORT",
                                           New With {.P_USERNAME = log.Username,
                                                     .P_ORGID = param.ORG_ID,
                                                     .P_ISDISSOLVE = param.IS_DISSOLVE,
                                                     .P_CUR = cls.OUT_CURSOR,
                                                     .P_CUR1 = cls.OUT_CURSOR,
                                                     .P_CUR2 = cls.OUT_CURSOR,
                                                     .P_CUR3 = cls.OUT_CURSOR,
                                                     .P_CUR4 = cls.OUT_CURSOR,
                                                     .P_CUR5 = cls.OUT_CURSOR,
                                                     .P_CUR6 = cls.OUT_CURSOR,
                                                     .P_CUR7 = cls.OUT_CURSOR}, False)

                Return dsData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".GetChangeInfoImport")
            Throw ex
        End Try
    End Function

    Public Function ImportChangeInfo(ByVal lstData As List(Of WorkingDTO),
                                     ByRef dtError As DataTable,
                                     ByVal log As UserLog) As Boolean
        Dim rowError As DataRow
        Dim isError As Boolean = False
        Dim iRow As Integer = 4
        Try
            ' Validate các thông tin
            For Each obj In lstData
                isError = False
                iRow += 1
                rowError = dtError.NewRow
                rowError("STT") = iRow
                ' 1. Kiểm tra sự tồn tại nhân viên
                Dim emp = (From p In Context.HU_EMPLOYEE
                           From cv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = p.ID).DefaultIfEmpty
                           Where p.EMPLOYEE_CODE = obj.EMPLOYEE_CODE
                           Order By p.ID Descending
                           Select New EmployeeDTO With {
                               .ID = p.ID,
                               .WORK_STATUS = p.WORK_STATUS,
                               .ID_NO = cv.ID_NO}).FirstOrDefault
                If emp Is Nothing Then
                    isError = True
                    rowError("EMPLOYEE_CODE") = "Mã " & obj.EMPLOYEE_CODE & " không tồn tại" & vbNewLine
                Else
                    obj.EMPLOYEE_ID = emp.ID
                    ' 2. Kiểm tra có nghỉ việc hay không
                    If emp.WORK_STATUS = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID Then
                        isError = True
                        If rowError("EMPLOYEE_CODE").ToString <> "" Then
                            rowError("EMPLOYEE_CODE") += vbNewLine
                        End If
                        rowError("EMPLOYEE_CODE") = "Nhân viên trạng thái nghỉ việc"
                    End If
                    ' 3. Kiểm tra CMND trong danh sách đen
                    If Not (From p In Context.HU_TERMINATE
                            From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                            From cv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = e.ID).DefaultIfEmpty
                            Where cv.ID_NO = emp.ID_NO And p.IS_NOHIRE = True).Count = 0 Then
                        isError = True
                        If rowError("EMPLOYEE_CODE").ToString <> "" Then
                            rowError("EMPLOYEE_CODE") += vbNewLine
                        End If
                        rowError("EMPLOYEE_CODE") = "Mã " & obj.EMPLOYEE_CODE & " tồn tại mã CMND trong danh sách đen"
                    End If

                    ' 5. Kiểm tra ngày hiệu lực
                    If Not (From e In Context.HU_WORKING
                            Where e.EMPLOYEE_ID = obj.EMPLOYEE_ID And
                            e.EFFECT_DATE >= obj.EFFECT_DATE And
                            e.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID).Count = 0 Then
                        isError = True
                        rowError("EFFECT_DATE") = "Ngày hiệu lực phải lớn hơn ngày hiệu lực gần nhất"
                    End If

                    ' 6. Kiểm tra xem đã có hồ sơ lương hay chưa
                    If (From p In Context.HU_WORKING
                        Where p.EMPLOYEE_ID = obj.EMPLOYEE_ID And
                        p.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID).Count = 0 Then
                        isError = True
                        If rowError("EMPLOYEE_CODE").ToString <> "" Then
                            rowError("EMPLOYEE_CODE") += vbNewLine
                        End If
                        rowError("EMPLOYEE_CODE") = "Bạn phải thêm Hồ sơ lương trước khi làm Thay đổi thông tin nhân sự"
                    End If

                    ' 7. Kiểm tra xem người ký
                    If obj.SIGN_CODE <> "" Then
                        Dim sign = (From p In Context.HU_EMPLOYEE
                                    From title In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
                                    Where p.EMPLOYEE_CODE = obj.SIGN_CODE
                                    Order By p.ID Descending
                                    Select New EmployeeDTO With {
                                        .ID = p.ID,
                                        .EMPLOYEE_CODE = p.EMPLOYEE_CODE,
                                        .FULLNAME_VN = p.FULLNAME_VN,
                                        .TITLE_NAME_VN = title.NAME_VN}).FirstOrDefault
                        If sign Is Nothing Then
                            isError = True
                            rowError("SIGN_CODE") = "Người ký không tồn tại"
                        Else
                            obj.SIGN_ID = sign.ID
                            obj.SIGN_NAME = sign.FULLNAME_VN
                            obj.SIGN_TITLE = sign.TITLE_NAME_VN
                        End If
                    End If

                    If Not isError Then
                        ' 7. Kiểm tra xem có thay đổi lương hay không
                        Dim working = (From p In Context.HU_WORKING
                                       Where p.EMPLOYEE_ID = obj.EMPLOYEE_ID And
                                       p.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID
                                       Order By p.EFFECT_DATE Descending).FirstOrDefault

                        If obj.SAL_GROUP_ID <> working.SAL_GROUP_ID Or
                            obj.SAL_LEVEL_ID <> working.SAL_LEVEL_ID Or
                            obj.SAL_RANK_ID <> working.SAL_RANK_ID Or
                            obj.COST_SUPPORT <> working.COST_SUPPORT Or
                            obj.SAL_BASIC <> working.SAL_BASIC Then
                            obj.IS_WAGE = True
                        End If
                    End If

                    If isError Then
                        dtError.Rows.Add(rowError)
                    End If
                End If
            Next

            If dtError.Rows.Count > 0 Then
                Return False
            End If

            For Each obj In lstData
                InsertChangeInfo(obj, log)
            Next
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".ImportChangeInfo")
            Throw ex
        End Try
    End Function

    Public Function InsertChangeInfo(ByRef objWorking As WorkingDTO,
                                     ByVal log As UserLog) As Boolean
        Dim iCount As Integer = 0

        Try
            Dim objWorkingData As New HU_WORKING
            objWorkingData.ID = Utilities.GetNextSequence(Context, Context.HU_WORKING.EntitySet.Name)
            objWorking.ID = objWorkingData.ID
            objWorkingData.EMPLOYEE_ID = objWorking.EMPLOYEE_ID
            objWorkingData.TITLE_ID = objWorking.TITLE_ID
            objWorkingData.ORG_ID = objWorking.ORG_ID
            objWorkingData.STAFF_RANK_ID = objWorking.STAFF_RANK_ID
            objWorkingData.STATUS_ID = objWorking.STATUS_ID
            objWorkingData.EFFECT_DATE = objWorking.EFFECT_DATE
            objWorkingData.EXPIRE_DATE = objWorking.EXPIRE_DATE
            objWorkingData.DECISION_TYPE_ID = objWorking.DECISION_TYPE_ID
            objWorkingData.DECISION_NO = objWorking.DECISION_NO
            objWorkingData.SAL_GROUP_ID = objWorking.SAL_GROUP_ID
            objWorkingData.SAL_LEVEL_ID = objWorking.SAL_LEVEL_ID
            objWorkingData.SAL_RANK_ID = objWorking.SAL_RANK_ID
            objWorkingData.SAL_BASIC = objWorking.SAL_BASIC
            objWorkingData.COST_SUPPORT = objWorking.COST_SUPPORT
            objWorkingData.SIGN_DATE = objWorking.SIGN_DATE
            objWorkingData.SIGN_ID = objWorking.SIGN_ID
            objWorkingData.SIGN_NAME = objWorking.SIGN_NAME
            objWorkingData.SIGN_TITLE = objWorking.SIGN_TITLE
            objWorkingData.REMARK = objWorking.REMARK
            objWorkingData.IS_PROCESS = objWorking.IS_PROCESS
            objWorkingData.IS_MISSION = objWorking.IS_MISSION
            objWorkingData.IS_WAGE = objWorking.IS_WAGE
            objWorkingData.IS_3B = False
            objWorkingData.PERCENT_SALARY = objWorking.PERCENT_SALARY
            objWorkingData.STAFF_RANK_ID = objWorking.STAFF_RANK_ID
            Context.HU_WORKING.AddObject(objWorkingData)

            ' nếu phê duyệt
            If objWorking.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID Then
                ApproveWorking(objWorking)
            End If

            Context.SaveChanges(log)

            Dim sumAllowIns As Decimal?
            Dim sumAllow = GetAllowanceTotalByDate(objWorking.EMPLOYEE_ID, objWorking.EFFECT_DATE, sumAllowIns)
            Dim total As Decimal? = 0
            If objWorking.SAL_BASIC IsNot Nothing Then
                total = total + objWorking.SAL_BASIC
            End If
            If objWorking.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID Then
                Dim totalIns As Decimal = 0
                If sumAllowIns IsNot Nothing Then
                    totalIns += sumAllowIns
                End If
                AutoGenInsChangeByWorking(objWorking.EMPLOYEE_ID,
                                          objWorking.ORG_ID,
                                          objWorking.TITLE_ID,
                                          objWorking.EFFECT_DATE,
                                          totalIns + total,
                                          log)
            End If
            If objWorking.COST_SUPPORT IsNot Nothing Then
                total = total + objWorkingData.COST_SUPPORT
            End If
            If objWorking.PERCENT_SALARY IsNot Nothing Then
                total = total * objWorking.PERCENT_SALARY / 100
            Else
                total = 0
            End If
            objWorkingData.SAL_TOTAL = total
            Context.SaveChanges()

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function UnApproveWorking(ByVal objWorking As WorkingDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Try
            Dim objWorkingData = (From p In Context.HU_WORKING Where objWorking.ID = p.ID).FirstOrDefault
            objWorkingData.STATUS_ID = objWorking.STATUS_ID
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function GetDataExport() As DataSet
        Try
            Using cls As New DataAccess.QueryData
                Dim dsdata As DataSet = cls.ExecuteStore("PKG_PROFILE_BUSINESS.GET_ALLOWANCE_EXPORT",
                                                         New With {.P_CUR = cls.OUT_CURSOR,
                                                         .P_CUR1 = cls.OUT_CURSOR}, False)
                Return dsdata
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GET_REGION_BY_DATE(ByVal empId As Decimal, ByVal pDate As Date) As Decimal
        Try
            Using cls As New DataAccess.QueryData
                Dim dsdata As DataTable = cls.ExecuteStore("PKG_HU_IPROFILE_WORKING.GET_REGION_BY_DATE",
                                                         New With {.P_EMP_ID = empId,
                                                                   .P_DATE = pDate,
                                                                   .P_CUR = cls.OUT_CURSOR}, True)
                Return dsdata(0)("MIN_AMOUNT")
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GET_SALARY_BY_DATE(ByVal empId As Decimal, ByVal pDate As Date) As Decimal
        Try
            Using cls As New DataAccess.QueryData
                Dim dsdata As DataTable = cls.ExecuteStore("PKG_HU_IPROFILE_WORKING.GET_SALARY_BY_DATE",
                                                         New With {.P_EMP_ID = empId,
                                                                   .P_DATE = pDate,
                                                                   .P_CUR = cls.OUT_CURSOR}, True)
                Return dsdata(0)("ID")
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GET_CODE_EMP_TYPE(ByVal Id As Decimal) As String
        Try
            Using cls As New DataAccess.QueryData
                Dim dsdata As DataTable = cls.ExecuteStore("PKG_HU_IPROFILE_WORKING.GET_CODE_EMP_TYPE",
                                                         New With {.P_ID = Id,
                                                                  .P_CUR = cls.OUT_CURSOR}, True)
                Return dsdata(0)("CODE")
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function


#End Region

#Region "Working Allow"


    Public Function GetWorkingAllowance(ByVal _filter As WorkingAllowanceDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "EMPLOYEE_CODE desc") As List(Of WorkingAllowanceDTO)

        Try
            Dim query = From p In Context.HU_WORKING
                        From allow In Context.HU_WORKING_ALLOW.Where(Function(f) f.HU_WORKING_ID = p.ID)
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From allow_type In Context.HU_ALLOWANCE_LIST.Where(Function(f) f.ID = allow.ALLOWANCE_LIST_ID)
                        Where p.EMPLOYEE_ID = _filter.EMPLOYEE_ID And
                        allow_type.ID <> 5 And
                        allow.EXPIRE_DATE Is Nothing
                        Select New WorkingAllowanceDTO With {.ID = allow.ID,
                                                             .EMPLOYEE_ID = p.EMPLOYEE_ID,
                                                             .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                                                             .EMPLOYEE_NAME = e.FULLNAME_VN,
                                                             .ALLOWANCE_LIST_NAME = allow_type.NAME,
                                                             .EFFECT_DATE = p.EFFECT_DATE,
                                                             .EXPIRE_DATE = p.EXPIRE_DATE}

            If _filter.EMPLOYEE_CODE IsNot Nothing Then
                query = query.Where(Function(p) p.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If

            If _filter.EMPLOYEE_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.EMPLOYEE_NAME.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If

            If _filter.EFFECT_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.EFFECT_DATE = _filter.EFFECT_DATE)
            End If

            ' danh sách thông tin quá trình công tác
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


    Public Function ModifyWorkingAllowance(ByVal objWorking As WorkingAllowanceDTO,
                                           ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        Try
            Dim objWorkingData = (From p In Context.HU_WORKING_ALLOW
                                  Where objWorking.ID = p.ID).FirstOrDefault
            objWorkingData.EXPIRE_DATE = objWorking.EXPIRE_DATE
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

#End Region

#Region "3B"

    Public Function GetWorking3B(ByVal _filter As WorkingDTO,
                               ByVal PageIndex As Integer,
                               ByVal PageSize As Integer,
                               ByRef Total As Integer, ByVal _param As ParamDTO,
                               Optional ByVal Sorts As String = "CREATED_DATE desc",
                               Optional ByVal log As UserLog = Nothing) As List(Of WorkingDTO)

        Try
            Dim terID = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using
            Dim query = From p In Context.HU_WORKING
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From o_move In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
                        From t_move In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From sal_group In Context.PA_SALARY_GROUP.Where(Function(f) p.SAL_GROUP_ID = f.ID).DefaultIfEmpty
                        From sal_level In Context.PA_SALARY_LEVEL.Where(Function(f) p.SAL_LEVEL_ID = f.ID).DefaultIfEmpty
                        From sal_rank In Context.PA_SALARY_RANK.Where(Function(f) p.SAL_RANK_ID = f.ID).DefaultIfEmpty
                        From status In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.STATUS_ID).DefaultIfEmpty
                        From deci_type In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.DECISION_TYPE_ID).DefaultIfEmpty
                        From staffrank In Context.HU_STAFF_RANK.Where(Function(f) f.ID = p.STAFF_RANK_ID).DefaultIfEmpty
                        From staffrank_move In Context.HU_STAFF_RANK.Where(Function(f) f.ID = e.STAFF_RANK_ID).DefaultIfEmpty
                        From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = e.ORG_ID And
                                                                       f.USERNAME = log.Username.ToUpper)
                        Select New WorkingDTO With {.ID = p.ID,
                                                    .DECISION_NO = p.DECISION_NO,
                                                    .DECISION_TYPE_ID = p.DECISION_TYPE_ID,
                                                    .DECISION_TYPE_NAME = deci_type.NAME_VN,
                                                    .EMPLOYEE_ID = p.EMPLOYEE_ID,
                                                    .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                                                    .EMPLOYEE_NAME = e.FULLNAME_VN,
                                                    .ORG_NAME = o.NAME_VN,
                                                    .ORG_DESC = o.DESCRIPTION_PATH,
                                                    .ORG_MOVE_NAME = o_move.NAME_VN,
                                                    .ORG_MOVE_DESC = o_move.DESCRIPTION_PATH,
                                                    .TITLE_NAME = t.NAME_VN,
                                                    .TITLE_MOVE_NAME = t_move.NAME_VN,
                                                    .EFFECT_DATE = p.EFFECT_DATE,
                                                    .EXPIRE_DATE = p.EXPIRE_DATE,
                                                    .STATUS_ID = p.STATUS_ID,
                                                    .STATUS_NAME = status.NAME_VN,
                                                    .STAFF_RANK_ID = p.STAFF_RANK_ID,
                                                    .STAFF_RANK_NAME = staffrank.NAME,
                                                    .STAFF_RANK_MOVE_NAME = staffrank_move.NAME,
                                                    .SAL_BASIC = p.SAL_BASIC,
                                                    .IS_MISSION = p.IS_MISSION,
                                                    .IS_WAGE = p.IS_WAGE,
                                                    .IS_PROCESS = p.IS_PROCESS,
                                                    .IS_3B = p.IS_3B,
                                                    .IS_MISSION_SHORT = p.IS_MISSION,
                                                    .IS_WAGE_SHORT = p.IS_WAGE,
                                                    .IS_PROCESS_SHORT = p.IS_PROCESS,
                                                    .IS_3B_SHORT = p.IS_3B,
                                                    .SAL_GROUP_ID = p.SAL_GROUP_ID,
                                                    .SAL_GROUP_NAME = sal_group.NAME,
                                                    .SAL_LEVEL_ID = p.SAL_LEVEL_ID,
                                                    .SAL_LEVEL_NAME = sal_level.NAME,
                                                    .SAL_RANK_ID = p.SAL_RANK_ID,
                                                    .SAL_RANK_NAME = sal_rank.RANK,
                                                    .COST_SUPPORT = p.COST_SUPPORT,
                                                    .PERCENT_SALARY = p.PERCENT_SALARY,
                                                    .SAL_TOTAL = p.SAL_TOTAL,
                                                    .EMPLOYEE_3B_ID = p.EMPLOYEE_3B_ID,
                                                    .WORK_STATUS = e.WORK_STATUS,
                                                    .TER_EFFECT_DATE = e.TER_EFFECT_DATE,
                                                    .CREATED_DATE = p.CREATED_DATE}


            Dim dateNow = Date.Now.Date
            If Not _filter.IS_TER Then
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

            If _filter.ORG_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If

            If _filter.TITLE_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.TITLE_NAME.ToUpper.Contains(_filter.TITLE_NAME.ToUpper))
            End If

            If _filter.ORG_MOVE_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.ORG_MOVE_NAME.ToUpper.Contains(_filter.ORG_MOVE_NAME.ToUpper))
            End If

            If _filter.TITLE_MOVE_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.TITLE_MOVE_NAME.ToUpper.Contains(_filter.TITLE_MOVE_NAME.ToUpper))
            End If

            If _filter.EMPLOYEE_ID <> 0 Then
                query = query.Where(Function(p) p.EMPLOYEE_ID = _filter.EMPLOYEE_ID)
            End If

            If _filter.DECISION_TYPE_ID IsNot Nothing Then
                query = query.Where(Function(p) p.DECISION_TYPE_ID = _filter.DECISION_TYPE_ID)
            End If

            If _filter.DECISION_TYPE_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.DECISION_TYPE_NAME.ToUpper.Contains(_filter.DECISION_TYPE_NAME.ToUpper))
            End If

            If _filter.IS_MISSION IsNot Nothing Then
                query = query.Where(Function(p) p.IS_MISSION_SHORT = _filter.IS_MISSION)
            End If

            If _filter.IS_PROCESS IsNot Nothing Then
                query = query.Where(Function(p) p.IS_PROCESS_SHORT = _filter.IS_PROCESS)
            End If

            If _filter.IS_WAGE IsNot Nothing Then
                query = query.Where(Function(p) p.IS_WAGE_SHORT = _filter.IS_WAGE)
            End If

            If _filter.IS_3B IsNot Nothing Then
                query = query.Where(Function(p) p.IS_3B_SHORT = _filter.IS_3B)
            End If

            If _filter.EFFECT_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.EFFECT_DATE = _filter.EFFECT_DATE)
            End If

            If _filter.FROM_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.EFFECT_DATE >= _filter.FROM_DATE)
            End If

            If _filter.TO_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.EFFECT_DATE <= _filter.TO_DATE)
            End If

            If _filter.STATUS_ID IsNot Nothing Then
                query = query.Where(Function(p) p.STATUS_ID = _filter.STATUS_ID)
            End If

            If _filter.SAL_GROUP_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.SAL_GROUP_NAME.ToUpper.Contains(_filter.SAL_GROUP_NAME.ToUpper))
            End If

            If _filter.SAL_LEVEL_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.SAL_LEVEL_NAME.ToUpper.Contains(_filter.SAL_LEVEL_NAME.ToUpper))
            End If

            If _filter.SAL_RANK_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.SAL_RANK_NAME.ToUpper.Contains(_filter.SAL_RANK_NAME.ToUpper))
            End If

            If _filter.SAL_BASIC IsNot Nothing Then
                query = query.Where(Function(p) p.SAL_BASIC = _filter.SAL_BASIC)
            End If

            If _filter.COST_SUPPORT IsNot Nothing Then
                query = query.Where(Function(p) p.COST_SUPPORT = _filter.COST_SUPPORT)
            End If

            If _filter.SAL_TOTAL IsNot Nothing Then
                query = query.Where(Function(p) p.COST_SUPPORT = _filter.SAL_TOTAL)
            End If

            If _filter.STAFF_RANK_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.STAFF_RANK_NAME.ToUpper.Contains(_filter.STAFF_RANK_NAME.ToUpper))
            End If

            ' danh sách thông tin quá trình công tác
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

    Public Function InsertWorking3B(ByVal objWorking As WorkingDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim iCount As Integer = 0

        Try

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_PROFILE_BUSINESS.UPDATE_WORKING_3B",
                                 New With {.P_ID = objWorking.ID,
                                           .P_EMPLOYEE_ID = objWorking.EMPLOYEE_ID,
                                           .P_TITLE_ID = objWorking.TITLE_ID,
                                           .P_ORG_ID = objWorking.ORG_ID,
                                           .P_REMARK = objWorking.REMARK,
                                           .P_EFFECT_DATE = objWorking.EFFECT_DATE,
                                           .P_EXPIRE_DATE = objWorking.EXPIRE_DATE,
                                           .P_STATUS_ID = objWorking.STATUS_ID,
                                           .P_DECISION_TYPE_ID = objWorking.DECISION_TYPE_ID,
                                           .P_DECISION_NO = objWorking.DECISION_NO,
                                           .P_SAL_GROUP_ID = objWorking.SAL_GROUP_ID,
                                           .P_SAL_LEVEL_ID = objWorking.SAL_LEVEL_ID,
                                           .P_SAL_RANK_ID = objWorking.SAL_RANK_ID,
                                           .P_SAL_BASIC = objWorking.SAL_BASIC,
                                           .P_COST_SUPPORT = objWorking.COST_SUPPORT,
                                           .P_SIGN_DATE = objWorking.SIGN_DATE,
                                           .P_SIGN_ID = objWorking.SIGN_ID,
                                           .P_SIGN_NAME = objWorking.SIGN_NAME,
                                           .P_SIGN_TITLE = objWorking.SIGN_TITLE,
                                           .P_PERCENT_SALARY = objWorking.PERCENT_SALARY,
                                           .P_IS_3B = objWorking.IS_3B,
                                           .P_STAFF_RANK_ID = objWorking.STAFF_RANK_ID,
                                           .P_DIRECT_MANAGER = objWorking.DIRECT_MANAGER,
                                           .P_USER_BY = log.Username,
                                           .P_USER_LOG = log.Ip & "-" & log.ComputerName})
            End Using

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".InsertWorking3B", "PKG_PROFILE_BUSINESS.UPDATE_WORKING_3B")
            Throw ex
        End Try

    End Function

    Public Function ModifyWorking3B(ByVal objWorking As WorkingDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_PROFILE_BUSINESS.UPDATE_WORKING_3B",
                                 New With {.P_ID = objWorking.ID,
                                           .P_EMPLOYEE_ID = objWorking.EMPLOYEE_ID,
                                           .P_TITLE_ID = objWorking.TITLE_ID,
                                           .P_ORG_ID = objWorking.ORG_ID,
                                           .P_REMARK = objWorking.REMARK,
                                           .P_EFFECT_DATE = objWorking.EFFECT_DATE,
                                           .P_EXPIRE_DATE = objWorking.EXPIRE_DATE,
                                           .P_STATUS_ID = objWorking.STATUS_ID,
                                           .P_DECISION_TYPE_ID = objWorking.DECISION_TYPE_ID,
                                           .P_DECISION_NO = objWorking.DECISION_NO,
                                           .P_SAL_GROUP_ID = objWorking.SAL_GROUP_ID,
                                           .P_SAL_LEVEL_ID = objWorking.SAL_LEVEL_ID,
                                           .P_SAL_RANK_ID = objWorking.SAL_RANK_ID,
                                           .P_SAL_BASIC = objWorking.SAL_BASIC,
                                           .P_COST_SUPPORT = objWorking.COST_SUPPORT,
                                           .P_SIGN_DATE = objWorking.SIGN_DATE,
                                           .P_SIGN_ID = objWorking.SIGN_ID,
                                           .P_SIGN_NAME = objWorking.SIGN_NAME,
                                           .P_SIGN_TITLE = objWorking.SIGN_TITLE,
                                           .P_PERCENT_SALARY = objWorking.PERCENT_SALARY,
                                           .P_IS_3B = objWorking.IS_3B,
                                           .P_STAFF_RANK_ID = objWorking.STAFF_RANK_ID,
                                           .P_DIRECT_MANAGER = objWorking.DIRECT_MANAGER,
                                           .P_USER_BY = log.Username,
                                           .P_USER_LOG = log.Ip & "-" & log.ComputerName})
            End Using

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".ModifyWorking3B", "PKG_PROFILE_BUSINESS.UPDATE_WORKING_3B")
            Throw ex
        End Try

    End Function

    Public Function DeleteWorking3B(ByVal objWorking As WorkingDTO) As Boolean

        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_PROFILE_BUSINESS.DELETE_WORKING_3B",
                                 New With {.P_ID = objWorking.ID,
                                           .P_STATUS_ID = objWorking.STATUS_ID,
                                           .P_EMPLOYEE_3B_ID = objWorking.EMPLOYEE_3B_ID})
            End Using

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteWorking3B", "PKG_PROFILE_BUSINESS.DELETE_WORKING_3B")
            Throw ex
        End Try

    End Function

#End Region
#Region "HU_SALARY"
    Public Function GetHu_Salary(ByVal _filter As HU_SALARYDTO,
                               ByVal PageIndex As Integer,
                               ByVal PageSize As Integer,
                               ByRef Total As Integer, ByVal _param As ParamDTO,
                               Optional ByVal Sorts As String = "CREATED_DATE desc",
                               Optional ByVal log As UserLog = Nothing) As List(Of HU_SALARYDTO)

        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using

            Dim query = From p In Context.HU_SALARY
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID).DefaultIfEmpty
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From ti In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From prod In Context.PA_FRAME_PRODUCTIVITY.Where(Function(f) f.ID = p.UNCLE_SALARY).DefaultIfEmpty
                        From prod1 In Context.PA_FRAME_PRODUCTIVITY.Where(Function(f) f.CODE = prod.SHORT_NAME).DefaultIfEmpty
                        From prod2 In Context.PA_FRAME_PRODUCTIVITY.Where(Function(f) f.ID = prod.PARENT_ID And (f.IS_LEVEL2 = 1 Or f.IS_LEVEL2 = -1)).DefaultIfEmpty
                        From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = e.ORG_ID And
                                                                       f.USERNAME = log.Username.ToUpper)

            Dim dateNow = Date.Now.Date
            Dim terID = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID

            If Not _filter.IS_TER Then
                query = query.Where(Function(p) Not p.e.WORK_STATUS.HasValue Or
                                        (p.e.WORK_STATUS.HasValue And
                                         ((p.e.WORK_STATUS <> terID) Or (p.e.WORK_STATUS = terID And p.e.TER_EFFECT_DATE > dateNow))))
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



            Dim data = query.Select(Function(k) New HU_SALARYDTO With {
                    .ID = k.p.ID,
                    .EMPLOYEE_ID = k.p.EMPLOYEE_ID,
                    .EMPLOYEE_CODE = k.e.EMPLOYEE_CODE,
                    .EMPLOYEE_NAME = k.e.FULLNAME_VN,
                    .ORG_ID = k.e.ORG_ID,
                    .ORG_NAME = k.org.NAME_VN,
                    .TITLE_ID = k.e.TITLE_ID,
                    .TITLE_NAME = k.ti.NAME_VN,
                    .EFFECT_DATE = k.p.EFFECT_DATE,
                    .PRODUCTIVITY_NAME = k.prod1.NAME_VN,
                    .PRODUCTIVITY_GROUP = k.prod2.NAME_VN,
                    .PRODUCTIVITY_RANK = k.prod.NAME_VN,
                    .COEFICIENT_NSLDBQ = k.p.COEFICIENT_NSLDBQ,
                    .REMARK = k.p.REMARK,
                    .CREATED_DATE = k.p.CREATED_DATE})

            data = data.OrderBy(Sorts)
            Total = query.Count
            data = data.Skip(PageIndex * PageSize).Take(PageSize)

            Return data.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function GetHuSalaryByID(ByVal _filter As HU_SALARYDTO) As HU_SALARYDTO
        Try
            Dim query = From p In Context.HU_SALARY
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From o In Context.HU_ORGANIZATION.Where(Function(f) p.ORG_ID = f.ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) p.TITLE_ID = f.ID).DefaultIfEmpty
                        From prod In Context.PA_FRAME_PRODUCTIVITY.Where(Function(f) f.ID = p.UNCLE_SALARY).DefaultIfEmpty
                        Where p.ID = _filter.ID
                        Select New HU_SALARYDTO With {
                             .ID = p.ID,
                             .EFFECT_DATE = p.EFFECT_DATE,
                             .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                             .EMPLOYEE_ID = e.ID,
                             .EMPLOYEE_NAME = e.FULLNAME_VN,
                            .TITLE_ID = p.TITLE_ID,
                            .ORG_ID = p.ORG_ID,
                            .TITLE_NAME = t.NAME_VN,
                            .ORG_NAME = t.NAME_VN,
                            .UNCLE_SALARY = p.UNCLE_SALARY,
                            .COEFICIENT_NSLDBQ = p.COEFICIENT_NSLDBQ,
                            .PRODUCTIVITY_NAME = prod.NAME_VN,
                            .REMARK = p.REMARK
                         }

            Return query.FirstOrDefault
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function InsertHu_Salary(ByVal objWorking As HU_SALARYDTO,
                                  ByVal log As UserLog,
                                  ByRef gID As Decimal) As Boolean
        Dim iCount As Integer = 0

        Try
            Dim objWorkingData As New HU_SALARY
            objWorkingData.ID = Utilities.GetNextSequence(Context, Context.HU_SALARY.EntitySet.Name)
            objWorking.ID = objWorkingData.ID
            objWorkingData.EMPLOYEE_ID = objWorking.EMPLOYEE_ID
            objWorkingData.TITLE_ID = objWorking.TITLE_ID
            objWorkingData.ORG_ID = objWorking.ORG_ID
            objWorkingData.EFFECT_DATE = objWorking.EFFECT_DATE
            objWorkingData.UNCLE_SALARY = objWorking.UNCLE_SALARY
            objWorkingData.COEFICIENT_NSLDBQ = objWorking.COEFICIENT_NSLDBQ
            objWorkingData.REMARK = objWorking.REMARK
            Context.HU_SALARY.AddObject(objWorkingData)
            Context.SaveChanges(log)

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Return False
        End Try

    End Function
    Public Function ModifyHu_Salary(ByVal objWorking As HU_SALARYDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        Try
            Dim objWorkingData = (From p In Context.HU_SALARY Where objWorking.ID = p.ID).First()
            objWorkingData.EMPLOYEE_ID = objWorking.EMPLOYEE_ID
            objWorkingData.TITLE_ID = objWorking.TITLE_ID
            objWorkingData.ORG_ID = objWorking.ORG_ID
            objWorkingData.EFFECT_DATE = objWorking.EFFECT_DATE
            objWorkingData.UNCLE_SALARY = objWorking.UNCLE_SALARY
            objWorkingData.COEFICIENT_NSLDBQ = objWorking.COEFICIENT_NSLDBQ
            objWorkingData.REMARK = objWorking.REMARK
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Return False
        End Try

    End Function
#End Region
End Class