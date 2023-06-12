Imports System.Reflection
Imports Framework.Data
Imports Framework.Data.System.Linq.Dynamic

Partial Class ProfileRepository

#Region "Other"
    Public Function GetCurrentPeriod(ByVal _year As Decimal) As DataTable
        Try
            Dim query = (From p In Context.AT_PERIOD
                         Where p.YEAR = _year
                         Select New With {.ID = p.ID, .PERIOD_NAME = p.PERIOD_NAME}).ToList.ToTable
            Return query
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region
#Region "Debt"

    Public Function GetDebt(ByVal Id As Decimal) As DebtDTO
        Try
            Dim query = (From p In Context.HU_DEBT
                         From debt_type In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.DEBT_TYPE_ID).DefaultIfEmpty
                         From debt_status In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.DEBT_STATUS).DefaultIfEmpty
                         From sp In Context.AT_PERIOD.Where(Function(f) f.ID = p.SALARY_PERIOD).DefaultIfEmpty
                         Where p.ID = Id)
            Dim sql = query.Select(Function(p) New DebtDTO With {
                                         .ID = p.p.ID,
                                         .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                         .DEBT_STATUS = p.p.DEBT_STATUS,
                                         .DEBT_STATUS_NAME = p.debt_status.NAME_VN,
                                         .DEBT_TYPE_ID = p.p.DEBT_TYPE_ID,
                                         .DEBT_TYPE_NAME = p.debt_type.NAME_VN,
                                         .PAYBACK = p.p.PAYBACK,
                                         .PAID = p.p.PAID,
                                         .NOTE = p.p.NOTE,
                                         .SALARY_PERIOD = p.p.SALARY_PERIOD,
                                         .DEDUCT_SALARY = p.p.DEDUCT_SALARY,
                                         .DATE_DEBIT = p.p.DATE_DEBIT,
                                         .ATTACH_FILE = p.p.ATTACH_FILE,
                                         .MONEY = p.p.MONEY,
                                         .PERIOD_TEXT = p.p.PERIOD_TEXT,
                                         .REMARK = p.p.REMARK})
            Return sql.ToList.First
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function InsertDebt(ByVal objDebt As DebtDTO,
                                   ByVal log As UserLog) As Boolean
        Try
            Dim objDebtData As New HU_DEBT
            objDebtData.ID = Utilities.GetNextSequence(Context, Context.HU_DEBT.EntitySet.Name)
            objDebtData.DEBT_TYPE_ID = objDebt.DEBT_TYPE_ID
            objDebtData.DEBT_STATUS = objDebt.DEBT_STATUS
            objDebtData.MONEY = objDebt.MONEY
            objDebtData.REMARK = objDebt.REMARK
            objDebtData.CREATED_DATE = DateTime.Now
            objDebtData.CREATED_BY = log.Username
            objDebtData.CREATED_LOG = log.ComputerName
            objDebtData.MODIFIED_DATE = DateTime.Now
            objDebtData.MODIFIED_BY = log.Username
            objDebtData.MODIFIED_LOG = log.ComputerName

            objDebtData.ATTACH_FILE = objDebt.ATTACH_FILE
            objDebtData.DATE_DEBIT = objDebt.DATE_DEBIT
            objDebtData.DEBT_TYPE_ID = objDebt.DEBT_TYPE_ID
            objDebtData.EMPLOYEE_ID = objDebt.EMPLOYEE_ID
            objDebtData.DEDUCT_SALARY = objDebt.DEDUCT_SALARY
            objDebtData.PAID = objDebt.PAID
            objDebtData.PAYBACK = objDebt.PAYBACK
            objDebtData.SALARY_PERIOD = objDebt.SALARY_PERIOD
            objDebtData.NOTE = objDebt.NOTE
            objDebtData.PERIOD_TEXT = objDebt.PERIOD_TEXT
            Context.HU_DEBT.AddObject(objDebtData)
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function ModifyDebt(ByVal objDebt As DebtDTO, ByVal log As UserLog) As Boolean
        Dim objDebtData As New HU_DEBT With {.ID = objDebt.ID}
        Try
            objDebtData = (From p In Context.HU_DEBT Where p.ID = objDebtData.ID).FirstOrDefault
            objDebtData.DEBT_TYPE_ID = objDebt.DEBT_TYPE_ID
            objDebtData.DEBT_STATUS = objDebt.DEBT_STATUS
            objDebtData.MONEY = objDebt.MONEY
            objDebtData.REMARK = objDebt.REMARK
            objDebtData.MODIFIED_DATE = DateTime.Now
            objDebtData.MODIFIED_BY = log.Username
            objDebtData.MODIFIED_LOG = log.ComputerName
            objDebtData.PERIOD_TEXT = objDebt.PERIOD_TEXT
            objDebtData.ATTACH_FILE = objDebt.ATTACH_FILE
            objDebtData.DATE_DEBIT = objDebt.DATE_DEBIT
            objDebtData.DEBT_TYPE_ID = objDebt.DEBT_TYPE_ID
            objDebtData.EMPLOYEE_ID = objDebt.EMPLOYEE_ID
            objDebtData.DEDUCT_SALARY = objDebt.DEDUCT_SALARY
            objDebtData.PAID = objDebt.PAID
            objDebtData.PAYBACK = objDebt.PAYBACK
            objDebtData.SALARY_PERIOD = objDebt.SALARY_PERIOD
            objDebtData.NOTE = objDebt.NOTE

            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function DeleteDebt(ByVal obj As List(Of Decimal)) As Boolean
        Try
            Dim lstDisciplineData As List(Of HU_DEBT)
            lstDisciplineData = (From p In Context.HU_DEBT Where obj.Contains(p.ID)).ToList
            For index = 0 To lstDisciplineData.Count - 1
                Context.HU_DEBT.DeleteObject(lstDisciplineData(index))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

#End Region
#Region "accident"
    Public Function GetAccident(ByVal _filter As AccidentDTO,
                                 ByVal PageIndex As Integer,
                                 ByVal PageSize As Integer,
                                 ByRef Total As Integer, ByVal _param As ParamDTO,
                                 Optional ByVal Sorts As String = "CREATED_DATE desc",
                                 Optional ByVal log As UserLog = Nothing) As List(Of AccidentDTO)

        Try

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using


            Dim query = From p In Context.HU_ACCIDENT
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID).DefaultIfEmpty
                        From mt In Context.HU_TITLE_BLD.Where(Function(f) f.ID = e.MATHE).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From re In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.REASON).DefaultIfEmpty
                        From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = e.ORG_ID And f.USERNAME = log.Username.ToUpper)


            Dim accident = query.Select(Function(p) New AccidentDTO With {
                                             .ID = p.p.ID,
                                             .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                             .MA_THE = p.mt.CODE,
                                             .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                                             .TITLE_NAME = p.t.NAME_VN,
                                             .ORG_NAME = p.o.NAME_VN,
                                             .ACCIDENT_DATE = p.p.ACCIDENT_DATE,
                                             .COST = p.p.COST,
                                             .REASON_NAME = p.re.NAME_VN,
                                             .INFORMATION = p.p.INFORMATION,
                                             .TREATMENT_PLACE = p.p.TREATMENT_PLACE,
                                             .MONEY = p.p.MONEY,
                                             .MONEY_DATE = p.p.MONEY_DATE,
                                             .REMARK = p.p.REMARK,
                                             .CREATED_BY = p.p.CREATED_BY,
                                             .CREATED_DATE = p.p.CREATED_DATE,
                                             .CREATED_LOG = p.p.CREATED_LOG,
                                             .MODIFIED_BY = p.p.MODIFIED_BY,
                                             .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                             .MODIFIED_LOG = p.p.MODIFIED_DATE})

            If _filter.EMPLOYEE_CODE IsNot Nothing Then
                accident = accident.Where(Function(p) p.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If

            If _filter.MA_THE IsNot Nothing Then
                accident = accident.Where(Function(p) p.MA_THE.ToUpper.Contains(_filter.MA_THE.ToUpper))
            End If

            If _filter.EMPLOYEE_NAME IsNot Nothing Then
                accident = accident.Where(Function(p) p.EMPLOYEE_NAME.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If

            If _filter.TITLE_NAME IsNot Nothing Then
                accident = accident.Where(Function(p) p.TITLE_NAME.ToUpper.Contains(_filter.TITLE_NAME.ToUpper))
            End If

            If _filter.ORG_NAME IsNot Nothing Then
                accident = accident.Where(Function(p) p.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If

            If _filter.ACCIDENT_DATE IsNot Nothing Then
                accident = accident.Where(Function(p) p.ACCIDENT_DATE = _filter.ACCIDENT_DATE)
            End If

            If _filter.COST IsNot Nothing Then
                accident = accident.Where(Function(p) p.COST = _filter.COST)
            End If

            If _filter.REASON_NAME IsNot Nothing Then
                accident = accident.Where(Function(p) p.REASON_NAME.ToUpper.Contains(_filter.REASON_NAME.ToUpper))
            End If

            If _filter.INFORMATION IsNot Nothing Then
                accident = accident.Where(Function(p) p.INFORMATION.ToUpper.Contains(_filter.INFORMATION.ToUpper))
            End If

            If _filter.TREATMENT_PLACE IsNot Nothing Then
                accident = accident.Where(Function(p) p.TREATMENT_PLACE.ToUpper.Contains(_filter.TREATMENT_PLACE.ToUpper))
            End If

            If _filter.MONEY IsNot Nothing Then
                accident = accident.Where(Function(p) p.MONEY = _filter.MONEY)
            End If


            If _filter.MONEY_DATE IsNot Nothing Then
                accident = accident.Where(Function(p) p.MONEY_DATE = _filter.MONEY_DATE)
            End If
            If _filter.REMARK IsNot Nothing Then
                accident = accident.Where(Function(p) p.REMARK.ToUpper.Contains(_filter.REMARK.ToUpper))
            End If


            If _filter.FROM_DATE IsNot Nothing Then
                accident = accident.Where(Function(p) p.ACCIDENT_DATE >= _filter.FROM_DATE)
            End If
            If _filter.TO_DATE IsNot Nothing Then
                accident = accident.Where(Function(p) p.ACCIDENT_DATE <= _filter.TO_DATE)
            End If


            accident = accident.OrderBy(Sorts)
            Total = accident.Count
            accident = accident.Skip(PageIndex * PageSize).Take(PageSize)
            Return accident.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function DeleteAccident(ByVal objID As Decimal, log As UserLog) As Boolean
        Dim objAccidentData As HU_ACCIDENT
        Try
            objAccidentData = (From p In Context.HU_ACCIDENT Where objID = p.ID).FirstOrDefault

            Context.HU_ACCIDENT.DeleteObject(objAccidentData)
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetAccidentByID(ByVal _filter As AccidentDTO) As AccidentDTO

        Try
            Dim query = From p In Context.HU_ACCIDENT
                        Join e In Context.HU_EMPLOYEE On p.EMPLOYEE_ID Equals e.ID
                        Join o In Context.HU_ORGANIZATION On e.ORG_ID Equals o.ID
                        Join t In Context.HU_TITLE On e.TITLE_ID Equals t.ID
                        From mt In Context.HU_TITLE_BLD.Where(Function(f) f.ID = e.MATHE).DefaultIfEmpty
                        From re In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.REASON).DefaultIfEmpty

            query = query.Where(Function(p) _filter.ID = p.p.ID)

            Dim obj = query.Select(Function(p) New AccidentDTO With {
                                             .ID = p.p.ID,
                                             .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                             .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                             .MA_THE = p.mt.CODE,
                                             .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                                             .TITLE_NAME = p.t.NAME_VN,
                                             .ORG_NAME = p.o.NAME_VN,
                                             .ACCIDENT_DATE = p.p.ACCIDENT_DATE,
                                             .COST = p.p.COST,
                                             .REASON_ID = p.p.REASON,
                                             .REASON_NAME = p.re.NAME_VN,
                                             .INFORMATION = p.p.INFORMATION,
                                             .TREATMENT_PLACE = p.p.TREATMENT_PLACE,
                                             .MONEY = p.p.MONEY,
                                             .MONEY_DATE = p.p.MONEY_DATE,
                                             .REMARK = p.p.REMARK})

            Dim ter = obj.FirstOrDefault

            Return ter
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try


    End Function

    Public Function InsertAccident(ByVal objTerminate As AccidentDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim iCount As Integer = 0
        Dim objTerminateData As New HU_ACCIDENT
        Try

            objTerminateData.ID = Utilities.GetNextSequence(Context, Context.HU_ACCIDENT.EntitySet.Name)

            If objTerminate.ID = 0 Then
                objTerminate.ID = objTerminateData.ID
            End If

            objTerminateData.EMPLOYEE_ID = objTerminate.EMPLOYEE_ID
            objTerminateData.ACCIDENT_DATE = objTerminate.ACCIDENT_DATE
            objTerminateData.COST = objTerminate.COST
            objTerminateData.REASON = objTerminate.REASON_ID
            objTerminateData.INFORMATION = objTerminate.INFORMATION
            objTerminateData.TREATMENT_PLACE = objTerminate.TREATMENT_PLACE
            objTerminateData.MONEY = objTerminate.MONEY
            objTerminateData.MONEY_DATE = objTerminate.MONEY_DATE

            objTerminateData.REMARK = objTerminate.REMARK
            objTerminateData.CREATED_DATE = DateTime.Now
            objTerminateData.CREATED_BY = log.Username
            objTerminateData.CREATED_LOG = log.ComputerName
            objTerminateData.MODIFIED_DATE = DateTime.Now
            objTerminateData.MODIFIED_BY = log.Username
            objTerminateData.MODIFIED_LOG = log.ComputerName


            Context.HU_ACCIDENT.AddObject(objTerminateData)

            Context.SaveChanges(log)
            gID = objTerminateData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function
    Public Function ModifyAccident(ByVal objTerminate As AccidentDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTerminateData As New HU_ACCIDENT With {.ID = objTerminate.ID}
        Try
            objTerminateData = (From p In Context.HU_ACCIDENT Where p.ID = objTerminate.ID).FirstOrDefault

            objTerminateData.EMPLOYEE_ID = objTerminate.EMPLOYEE_ID
            objTerminateData.ACCIDENT_DATE = objTerminate.ACCIDENT_DATE
            objTerminateData.COST = objTerminate.COST
            objTerminateData.REASON = objTerminate.REASON_ID
            objTerminateData.INFORMATION = objTerminate.INFORMATION
            objTerminateData.TREATMENT_PLACE = objTerminate.TREATMENT_PLACE
            objTerminateData.MONEY = objTerminate.MONEY
            objTerminateData.MONEY_DATE = objTerminate.MONEY_DATE

            objTerminateData.REMARK = objTerminate.REMARK
            objTerminateData.CREATED_DATE = DateTime.Now
            objTerminateData.CREATED_BY = log.Username
            objTerminateData.CREATED_LOG = log.ComputerName
            objTerminateData.MODIFIED_DATE = DateTime.Now
            objTerminateData.MODIFIED_BY = log.Username
            objTerminateData.MODIFIED_LOG = log.ComputerName

            Context.SaveChanges(log)
            gID = objTerminateData.ID

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

#End Region

#Region "Terminate"
    Public Function Check_has_Ter(ByVal empid As Decimal) As Decimal
        Dim value As Decimal = 0
        Try
            value = (From p In Context.HU_TERMINATE
                     Where p.EMPLOYEE_ID = empid).ToList.Count
            If value = 0 Then
                Return 0
            Else
                Return 1
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetMoneyReimburseTerminate(ByVal EmployeeId As Decimal) As Decimal
        Try
            Dim listValue As New List(Of Decimal?)
            listValue = (From p In Context.TR_PROGRAM_COMMIT
                         Where p.EMPLOYEE_ID = EmployeeId
                         Select p.MONEY_REIMBURSE).ToList()
            Return listValue.Sum()
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function CalculateTerminate(ByVal EmployeeId As Decimal, ByVal TerLateDate As Date) As DataTable
        Try
            Using rep As New DataAccess.QueryData
                Dim dt As DataTable = rep.ExecuteStore("PKG_PROFILE_BUSINESS.GET_INFO_TERMINATE",
                                                       New With {.P_EMPLOYEE_ID = EmployeeId,
                                                                 .P_TER_DATE = TerLateDate,
                                                                 .P_CUR = rep.OUT_CURSOR}, True)
                Return dt
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Return Nothing
        End Try
    End Function

    Public Function GetLabourProtectByTerminate(ByVal gID As Decimal) As List(Of LabourProtectionMngDTO)
        Try
            Dim sql = (From l In Context.HU_LABOURPROTECTION_MNG
                       From p In Context.HU_LABOURPROTECTION.Where(Function(p) p.ID = l.LABOURPROTECTION_ID)
                       Where l.EMPLOYEE_ID = gID
                       Select New LabourProtectionMngDTO With
                              {.ID = l.ID,
                               .LABOURPROTECTION_ID = l.LABOURPROTECTION_ID,
                               .LABOURPROTECTION_NAME = p.NAME,
                               .QUANTITY = l.QUANTITY,
                               .RETRIEVED = l.RETRIEVED,
                               .DAYS_ALLOCATED = l.DAYS_ALLOCATED,
                               .RETRIEVE_DATE = l.RETRIEVE_DATE,
                               .INDEMNITY = l.INDEMNITY,
                               .UNIT_PRICE = p.UNIT_PRICE,
                               .SDESC = l.SDESC,
                               .DEPOSIT = l.DEPOSIT}).ToList

            Return sql
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")

        End Try
    End Function

    Public Function GetAssetByTerminate(ByVal gID As Decimal) As List(Of AssetMngDTO)
        Try
            Dim query = (From p In Context.HU_ASSET_MNG
                         From d In Context.HU_ASSET.Where(Function(d) d.ID = p.ASSET_DECLARE_ID)
                         From assetgroup In Context.OT_OTHER_LIST.Where(Function(f) f.ID = d.GROUP_ID _
                                                                            And f.TYPE_ID = ProfileCommon.OT_ASSET_GROUP.TYPE_ID).DefaultIfEmpty
                         From assetstatus In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.STATUS_ID _
                                                                            And f.TYPE_ID = ProfileCommon.OT_ASSET_STATUS.TYPE_ID).DefaultIfEmpty
                         Where p.EMPLOYEE_ID = gID
                         Order By d.CODE)


            Dim sql = query.Select(Function(p) New AssetMngDTO With {
                                         .ID = p.p.ID,
                                         .ASSET_CODE = p.d.CODE,
                                         .ASSET_NAME = p.d.NAME,
                                         .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                         .ASSET_VALUE = p.p.ASSET_VALUE,
                                         .ASSET_GROUP_NAME = p.assetgroup.NAME_VN,
                                         .DEPOSITS = p.p.DEPOSITS,
                                         .DESC = p.p.SDESC,
                                         .ISSUE_DATE = p.p.ISSUE_DATE,
                                         .RETURN_DATE = p.p.RETURN_DATE,
                                         .ORG_TRANFER = p.p.ORG_TRANFER,
                                         .ORG_RECEIVE = p.p.ORG_RECEIVE,
                                         .ASSET_BARCODE = p.p.ASSET_BARCODE,
                                         .ASSET_SERIAL = p.p.ASSET_SERIAL,
                                         .STATUS_ID = p.p.STATUS_ID,
                                         .REMARK = p.d.REMARK,
                                         .QUANTITY = p.d.QUANTITY,
                                         .STATUS_NAME = p.assetstatus.NAME_VN}).ToList

            For Each item In sql
                If item.RETURN_DATE IsNot Nothing And item.ISSUE_DATE IsNot Nothing Then
                    item.DATE_USE = (item.RETURN_DATE - item.ISSUE_DATE).Value.Days + 1
                End If
            Next

            Return sql
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")

        End Try
    End Function

    Public Function ApproveListTerminate(ByVal listID As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim objTerData As HU_TERMINATE
        Try
            Dim item As Decimal = 0
            For idx = 0 To listID.Count - 1
                item = listID(idx)
                Dim objTerminateData As New TerminateDTO
                Dim objTerminate = (From p In Context.HU_TERMINATE Where p.ID = item).FirstOrDefault
                objTerminateData.EMPLOYEE_ID = objTerminate.EMPLOYEE_ID


                Dim objSe_User = (From p In Context.SE_USER Where p.EMPLOYEE_ID = objTerminate.EMPLOYEE_ID).FirstOrDefault
                If objSe_User IsNot Nothing Then
                    objSe_User.EFFECTDATE_TERMINATION = objTerminate.EFFECT_DATE
                    objSe_User.EXPIRE_DATE = objTerminate.EFFECT_DATE
                End If

                objTerminateData.IS_NOHIRE = objTerminate.IS_NOHIRE
                objTerminateData.UPLOADFILE = objTerminate.UPLOADFILE
                objTerminateData.FILENAME = objTerminate.FILENAME
                objTerminateData.LAST_DATE = objTerminate.LAST_DATE
                objTerminateData.SEND_DATE = objTerminate.SEND_DATE
                objTerminateData.STATUS_ID = objTerminate.STATUS_ID
                objTerminate.STATUS_ID = 447
                objTerminateData.STATUS_ID = 447
                objTerminateData.TER_REASON_DETAIL = objTerminate.TER_REASON_DETAIL
                objTerminateData.EMPLOYEE_SENIORITY = objTerminate.EMP_SENIORITY
                objTerminateData.REMARK = objTerminate.REMARK
                objTerminateData.MODIFIED_DATE = DateTime.Now
                objTerminateData.MODIFIED_BY = log.Username
                objTerminateData.MODIFIED_LOG = log.ComputerName

                objTerminateData.APPROVAL_DATE = objTerminate.APPROVAL_DATE
                objTerminateData.IDENTIFI_CARD = objTerminate.IDENTIFI_CARD
                objTerminateData.IDENTIFI_RDATE = objTerminate.IDENTIFI_RDATE
                objTerminateData.IDENTIFI_STATUS = objTerminate.IDENTIFI_STATUS
                objTerminateData.IDENTIFI_MONEY = objTerminate.IDENTIFI_MONEY
                objTerminateData.SUN_CARD = objTerminate.SUN_CARD
                objTerminateData.SUN_RDATE = objTerminate.SUN_RDATE
                objTerminateData.SUN_STATUS = objTerminate.SUN_STATUS
                objTerminateData.SUN_MONEY = objTerminate.SUN_MONEY
                objTerminateData.INSURANCE_CARD = objTerminate.INSURANCE_CARD
                objTerminateData.INSURANCE_RDATE = objTerminate.INSURANCE_RDATE
                objTerminateData.INSURANCE_STATUS = objTerminate.INSURANCE_STATUS
                objTerminateData.INSURANCE_MONEY = objTerminate.INSURANCE_MONEY
                objTerminateData.REMAINING_LEAVE = objTerminate.REMAINING_LEAVE
                objTerminateData.PAYMENT_LEAVE = objTerminate.PAYMENT_LEAVE
                objTerminateData.AMOUNT_VIOLATIONS = objTerminate.AMOUNT_VIOLATIONS
                objTerminateData.AMOUNT_WRONGFUL = objTerminate.AMOUNT_WRONGFUL
                objTerminateData.ALLOWANCE_TERMINATE = objTerminate.ALLOWANCE_TERMINATE
                objTerminateData.TRAINING_COSTS = objTerminate.TRAINING_COSTS
                objTerminateData.OTHER_COMPENSATION = objTerminate.OTHER_COMPENSATION
                objTerminateData.COMPENSATORY_LEAVE = objTerminate.COMPENSATORY_LEAVE
                objTerminateData.COMPENSATORY_PAYMENT = objTerminate.COMPENSATORY_PAYMENT
                objTerminateData.EFFECT_DATE = objTerminate.EFFECT_DATE
                objTerminateData.DECISION_NO = objTerminate.DECISION_NO
                objTerminateData.SIGN_DATE = objTerminate.SIGN_DATE
                objTerminateData.SIGN_ID = objTerminate.SIGN_ID
                objTerminateData.SIGN_NAME = objTerminate.SIGN_NAME
                objTerminateData.SIGN_TITLE = objTerminate.SIGN_TITLE

                'bỔ XUNG THÊM TRƯỜNG
                objTerminateData.SALARYMEDIUM = objTerminate.SALARYMEDIUM
                objTerminateData.YEARFORALLOW = objTerminate.YEARFORALLOW
                objTerminateData.MONEYALLOW = objTerminate.MONEYALLOW
                objTerminateData.REMAINING_LEAVE_MONEY = objTerminate.REMAINING_LEAVE_MONEY
                objTerminateData.IDENTIFI_VIOLATE_MONEY = objTerminate.IDENTIFI_VIOLATE_MONEY
                objTerminateData.MONEY_RETURN = objTerminate.MONEY_RETURN
                objTerminateData.ORG_ID = objTerminate.ORG_ID
                objTerminateData.TITLE_ID = objTerminate.TITLE_ID
                objTerminateData.TYPE_TERMINATE = objTerminate.TYPE_TERMINATE
                objTerminateData.SALARYMEDIUM_LOSS = objTerminate.SALARYMEDIUM_LOSS
                'objTerminateData.Expire_Date = objTerminate.EXPIRE_DATE


                Dim lstAll = (From p In Context.HU_TERMINATE_REASON Where p.HU_TERMINATE_ID = objTerminate.ID).ToList
                For Each row In lstAll
                    Context.HU_TERMINATE_REASON.DeleteObject(row)
                Next

                If objTerminateData.lstReason IsNot Nothing Then
                    For Each obj In objTerminateData.lstReason
                        Dim allow As New HU_TERMINATE_REASON
                        allow.ID = Utilities.GetNextSequence(Context, Context.HU_TERMINATE_REASON.EntitySet.Name)
                        allow.HU_TERMINATE_ID = objTerminateData.ID
                        allow.TER_REASON_ID = obj.TER_REASON_ID
                        allow.DENSITY = obj.DENSITY
                        Context.HU_TERMINATE_REASON.AddObject(allow)
                    Next
                End If

                If objTerminate.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID Then
                    objTerminateData.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID
                    objTerminate.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID
                    ApproveTerminate(objTerminateData, log)
                End If
                InsertOrUpdateAssetByTerminate(objTerminateData, log)
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetTerminate(ByVal _filter As TerminateDTO,
                                 ByVal PageIndex As Integer,
                                 ByVal PageSize As Integer,
                                 ByRef Total As Integer, ByVal _param As ParamDTO,
                                 Optional ByVal Sorts As String = "CREATED_DATE desc",
                                 Optional ByVal log As UserLog = Nothing) As List(Of TerminateDTO)

        Try

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using


            Dim query = From p In Context.HU_TERMINATE
                        From type In Context.OT_OTHER_LIST.Where(Function(f) p.DECISION_TYPE = f.ID).DefaultIfEmpty
                        From e In Context.HU_EMPLOYEE.Where(Function(f) p.EMPLOYEE_ID = f.ID)
                        From cv In Context.HU_EMPLOYEE_CV.Where(Function(f) p.EMPLOYEE_ID = f.EMPLOYEE_ID)
                        From o In Context.HUV_ORGANIZATION.Where(Function(f) e.ORG_ID = f.ID)
                        From t In Context.HU_TITLE.Where(Function(f) e.TITLE_ID = f.ID)
                        From deci_type In Context.OT_OTHER_LIST.Where(Function(f) p.TYPE_TERMINATE = f.ID).DefaultIfEmpty
                        From status In Context.OT_OTHER_LIST.Where(Function(f) p.STATUS_ID = f.ID).DefaultIfEmpty
                        From status_BH In Context.OT_OTHER_LIST.Where(Function(f) p.INSURANCE_STATUS = f.ID).DefaultIfEmpty
                        From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = e.ORG_ID And f.USERNAME = log.Username.ToUpper)
                        From ter_rea_name In Context.OT_OTHER_LIST.Where(Function(f) p.TER_REASON = f.ID).DefaultIfEmpty
            Dim terminate = query.Select(Function(p) New TerminateDTO With {
                                             .ID = p.p.ID,
                                             .REASON_BLACK_LIST = p.p.REASON_BLACK_LIST,
                                             .DECISION_NO = p.p.DECISION_NO,
                                             .STATUS_NAME = p.status.NAME_VN,
                                             .STATUS_CODE = p.status.CODE,
                                             .STATUS_ID = p.status.ID,
                                             .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                             .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                             .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                                             .JOIN_DATE = p.e.JOIN_DATE,
                                             .CODE = p.deci_type.CODE,
                                             .IS_NOHIRE = p.p.IS_NOHIRE,
                                             .IS_NOHIRE_SHORT = p.p.IS_NOHIRE,
                                             .LAST_DATE = p.p.LAST_DATE,
                                             .SEND_DATE = p.p.SEND_DATE,
                                             .EFFECT_DATE = p.p.EFFECT_DATE,
                                             .ORG_NAME = p.o.NAME_VN,
                                             .ORG_NAME2 = p.o.ORG_NAME2,
                                             .ORG_NAME3 = p.o.ORG_NAME3,
                                             .ORG_NAME4 = p.o.ORG_NAME4,
                                             .ORG_NAME5 = p.o.ORG_NAME5,
                                             .ORG_NAME6 = p.o.ORG_NAME6,
                                             .ORG_NAME7 = p.o.ORG_NAME7,
                                             .ORG_DESC = p.o.DESCRIPTION_PATH,
                                             .TER_REASON_DETAIL = p.p.TER_REASON_DETAIL,
                                             .TITLE_NAME = p.t.NAME_VN,
                                             .APPROVAL_DATE = p.p.APPROVAL_DATE,
                                            .IDENTIFI_CARD = p.p.IDENTIFI_CARD,
                                            .IDENTIFI_RDATE = p.p.IDENTIFI_RDATE,
                                            .IDENTIFI_STATUS = p.p.IDENTIFI_STATUS,
                                             .IDENTIFI_MONEY = p.p.IDENTIFI_MONEY,
                                            .SUN_CARD = p.p.SUN_CARD,
                                            .SUN_RDATE = p.p.SUN_RDATE,
                                            .SUN_STATUS = p.p.SUN_STATUS,
                                            .SUN_MONEY = p.p.SUN_MONEY,
                                            .INSURANCE_CARD = p.p.INSURANCE_CARD,
                                            .INSURANCE_RDATE = p.p.INSURANCE_RDATE,
                                            .INSURANCE_STATUS = p.status_BH.NAME_VN,
                                             .INSURANCE_STATUS_NAME = p.status_BH.NAME_EN,
                                            .INSURANCE_MONEY = p.p.INSURANCE_MONEY,
                                            .REMAINING_LEAVE = p.p.REMAINING_LEAVE,
                                            .PAYMENT_LEAVE = p.p.PAYMENT_LEAVE,
                                            .AMOUNT_VIOLATIONS = p.p.AMOUNT_VIOLATIONS,
                                            .AMOUNT_WRONGFUL = p.p.AMOUNT_WRONGFUL,
                                            .ALLOWANCE_TERMINATE = p.p.ALLOWANCE_TERMINATE,
                                            .TRAINING_COSTS = p.p.TRAINING_COSTS,
                                            .OTHER_COMPENSATION = p.p.OTHER_COMPENSATION,
                                             .COMPENSATORY_LEAVE = p.p.COMPENSATORY_LEAVE,
                                             .COMPENSATORY_PAYMENT = p.p.COMPENSATORY_PAYMENT,
                                             .CREATED_DATE = p.p.CREATED_DATE,
                                             .ID_NO = p.cv.ID_NO,
                                             .TYPE_TERMINATE = p.p.TYPE_TERMINATE,
                                             .SALARYMEDIUM = p.p.SALARYMEDIUM,
                                             .YEARFORALLOW = p.p.YEARFORALLOW,
                                             .MONEYALLOW = p.p.MONEYALLOW,
                                             .REMAINING_LEAVE_MONEY = p.p.REMAINING_LEAVE_MONEY,
                                             .IDENTIFI_VIOLATE_MONEY = p.p.IDENTIFI_VIOLATE_MONEY,
                                             .MONEY_RETURN = p.p.MONEY_RETURN,
                                             .REMARK = p.p.REMARK,
                                             .SIGN_NAME = p.p.SIGN_NAME,
                                             .SIGN_DATE = p.p.SIGN_DATE,
                                             .SIGN_TITLE = p.p.SIGN_TITLE,
                                             .SALARYMEDIUM_LOSS = p.p.SALARYMEDIUM_LOSS,
                                             .FILENAME = p.p.FILENAME,
                                             .SUM_DEBT = p.p.SUM_DEBT,
                                             .UPLOADFILE = p.p.UPLOADFILE,
                                             .TER_REASON_NAME = p.ter_rea_name.NAME_VN,
                                             .SUM_COLLECT_DEBT = p.p.SUM_COLLECT_DEBT,
                                             .DECISION_TYPE_NAME = p.type.NAME_VN,
                                             .ALLOW_NAME = If(p.p.IS_ALLOW <> 0, "x", ""),
                                             .JOB_LOSS_ALLOWANCE_NAME = If(p.p.IS_JOB_LOSS_ALLOWANCE <> 0, "x", ""),
                                             .IS_BLACK_LIST = p.p.IS_BLACK_LIST})

            If _filter.EMPLOYEE_CODE IsNot Nothing Then
                terminate = terminate.Where(Function(p) p.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If

            If _filter.EMPLOYEE_NAME IsNot Nothing Then
                terminate = terminate.Where(Function(p) p.EMPLOYEE_NAME.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If

            If _filter.DECISION_TYPE_NAME IsNot Nothing Then
                terminate = terminate.Where(Function(p) p.DECISION_TYPE_NAME.ToUpper.Contains(_filter.DECISION_TYPE_NAME.ToUpper))
            End If

            If _filter.ALLOW_NAME IsNot Nothing Then
                terminate = terminate.Where(Function(p) p.ALLOW_NAME.ToUpper.Contains(_filter.ALLOW_NAME.ToUpper))
            End If

            If _filter.JOB_LOSS_ALLOWANCE_NAME IsNot Nothing Then
                terminate = terminate.Where(Function(p) p.JOB_LOSS_ALLOWANCE_NAME.ToUpper.Contains(_filter.JOB_LOSS_ALLOWANCE_NAME.ToUpper))
            End If

            If _filter.REMARK IsNot Nothing Then
                terminate = terminate.Where(Function(p) p.REMARK.ToUpper.Contains(_filter.REMARK.ToUpper))
            End If
            If _filter.EMPLOYEE_ID IsNot Nothing Then
                terminate = terminate.Where(Function(p) p.EMPLOYEE_ID = _filter.EMPLOYEE_ID)
            End If
            If _filter.FROM_LAST_DATE IsNot Nothing Then
                terminate = terminate.Where(Function(p) p.LAST_DATE >= _filter.FROM_LAST_DATE)
            End If
            If _filter.TO_LAST_DATE IsNot Nothing Then
                terminate = terminate.Where(Function(p) p.LAST_DATE <= _filter.TO_LAST_DATE)
            End If
            If _filter.FROM_SEND_DATE IsNot Nothing Then
                terminate = terminate.Where(Function(p) p.SEND_DATE >= _filter.FROM_SEND_DATE)
            End If
            If _filter.TO_SEND_DATE IsNot Nothing Then
                terminate = terminate.Where(Function(p) p.SEND_DATE <= _filter.TO_SEND_DATE)
            End If
            'If _filter.STATUS_ID <> 0 Then
            '    terminate = terminate.Where(Function(p) p.STATUS_ID = _filter.STATUS_ID)
            'End If
            If _filter.IS_NOHIRE Then
                terminate = terminate.Where(Function(p) p.IS_NOHIRE_SHORT = _filter.IS_NOHIRE)
            End If
            If _filter.ID_NO IsNot Nothing Then
                terminate = terminate.Where(Function(p) p.ID_NO.ToUpper.Contains(_filter.ID_NO.ToUpper))
            End If

            If _filter.ORG_NAME2 <> "" Then
                terminate = terminate.Where(Function(p) p.ORG_NAME2.ToUpper().IndexOf(_filter.ORG_NAME2.ToUpper) >= 0)
            End If

            If _filter.ORG_NAME3 <> "" Then
                terminate = terminate.Where(Function(p) p.ORG_NAME3.ToUpper().IndexOf(_filter.ORG_NAME3.ToUpper) >= 0)
            End If

            If _filter.ORG_NAME4 <> "" Then
                terminate = terminate.Where(Function(p) p.ORG_NAME4.ToUpper().IndexOf(_filter.ORG_NAME4.ToUpper) >= 0)
            End If

            If _filter.ORG_NAME5 <> "" Then
                terminate = terminate.Where(Function(p) p.ORG_NAME5.ToUpper().IndexOf(_filter.ORG_NAME5.ToUpper) >= 0)
            End If

            If _filter.ORG_NAME6 <> "" Then
                terminate = terminate.Where(Function(p) p.ORG_NAME6.ToUpper().IndexOf(_filter.ORG_NAME6.ToUpper) >= 0)
            End If

            If _filter.ORG_NAME7 <> "" Then
                terminate = terminate.Where(Function(p) p.ORG_NAME7.ToUpper().IndexOf(_filter.ORG_NAME7.ToUpper) >= 0)
            End If

            If _filter.IS_BLACK_LIST IsNot Nothing Then
                terminate = terminate.Where(Function(p) p.IS_BLACK_LIST = _filter.IS_BLACK_LIST)
            End If

            terminate = terminate.OrderBy(Sorts)
            Total = terminate.Count
            terminate = terminate.Skip(PageIndex * PageSize).Take(PageSize)
            Return terminate.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetTerminateSeverance(ByVal _filter As TerminateDTO,
                             ByVal PageIndex As Integer,
                             ByVal PageSize As Integer,
                             ByRef Total As Integer, ByVal _param As ParamDTO,
                             Optional ByVal Sorts As String = "CREATED_DATE desc",
                             Optional ByVal log As UserLog = Nothing) As List(Of TerminateDTO)

        Try

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using


            Dim query = From p In Context.HU_TERMINATE
                        From type In Context.OT_OTHER_LIST.Where(Function(f) p.DECISION_TYPE = f.ID).DefaultIfEmpty
                        From e In Context.HU_EMPLOYEE.Where(Function(f) p.EMPLOYEE_ID = f.ID)
                        From cv In Context.HU_EMPLOYEE_CV.Where(Function(f) p.EMPLOYEE_ID = f.EMPLOYEE_ID)
                        From o In Context.HUV_ORGANIZATION.Where(Function(f) e.ORG_ID = f.ID)
                        From t In Context.HU_TITLE.Where(Function(f) e.TITLE_ID = f.ID)
                        From deci_type In Context.OT_OTHER_LIST.Where(Function(f) p.TYPE_TERMINATE = f.ID).DefaultIfEmpty
                        From status In Context.OT_OTHER_LIST.Where(Function(f) p.STATUS_ID = f.ID).DefaultIfEmpty
                        From status_BH In Context.OT_OTHER_LIST.Where(Function(f) p.INSURANCE_STATUS = f.ID).DefaultIfEmpty
                        From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = e.ORG_ID And f.USERNAME = log.Username.ToUpper)
                        From ter_rea_name In Context.OT_OTHER_LIST.Where(Function(f) p.TER_REASON = f.ID).DefaultIfEmpty
                        Where p.STATUS_ID = 447 AndAlso p.IS_ALLOW = -1
            Dim terminate = query.Select(Function(p) New TerminateDTO With {
                                             .ID = p.p.ID,
                                             .REASON_BLACK_LIST = p.p.REASON_BLACK_LIST,
                                             .DECISION_NO = p.p.DECISION_NO,
                                             .STATUS_NAME = p.status.NAME_VN,
                                             .STATUS_CODE = p.status.CODE,
                                             .STATUS_ID = p.status.ID,
                                             .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                             .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                             .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                                             .JOIN_DATE = p.e.JOIN_DATE,
                                             .CODE = p.deci_type.CODE,
                                             .IS_NOHIRE = p.p.IS_NOHIRE,
                                             .IS_NOHIRE_SHORT = p.p.IS_NOHIRE,
                                             .LAST_DATE = p.p.LAST_DATE,
                                             .SEND_DATE = p.p.SEND_DATE,
                                             .EFFECT_DATE = p.p.EFFECT_DATE,
                                             .ORG_NAME = p.o.NAME_VN,
                                             .ORG_NAME2 = p.o.ORG_NAME2,
                                             .ORG_NAME3 = p.o.ORG_NAME3,
                                             .ORG_NAME4 = p.o.ORG_NAME4,
                                             .ORG_NAME5 = p.o.ORG_NAME5,
                                             .ORG_NAME6 = p.o.ORG_NAME6,
                                             .ORG_NAME7 = p.o.ORG_NAME7,
                                             .ORG_DESC = p.o.DESCRIPTION_PATH,
                                             .TER_REASON_DETAIL = p.p.TER_REASON_DETAIL,
                                             .TITLE_NAME = p.t.NAME_VN,
                                             .APPROVAL_DATE = p.p.APPROVAL_DATE,
                                            .IDENTIFI_CARD = p.p.IDENTIFI_CARD,
                                            .IDENTIFI_RDATE = p.p.IDENTIFI_RDATE,
                                            .IDENTIFI_STATUS = p.p.IDENTIFI_STATUS,
                                             .IDENTIFI_MONEY = p.p.IDENTIFI_MONEY,
                                            .SUN_CARD = p.p.SUN_CARD,
                                            .SUN_RDATE = p.p.SUN_RDATE,
                                            .SUN_STATUS = p.p.SUN_STATUS,
                                            .SUN_MONEY = p.p.SUN_MONEY,
                                            .INSURANCE_CARD = p.p.INSURANCE_CARD,
                                            .INSURANCE_RDATE = p.p.INSURANCE_RDATE,
                                            .INSURANCE_STATUS = p.status_BH.NAME_VN,
                                             .INSURANCE_STATUS_NAME = p.status_BH.NAME_EN,
                                            .INSURANCE_MONEY = p.p.INSURANCE_MONEY,
                                            .REMAINING_LEAVE = p.p.REMAINING_LEAVE,
                                            .PAYMENT_LEAVE = p.p.PAYMENT_LEAVE,
                                            .AMOUNT_VIOLATIONS = p.p.AMOUNT_VIOLATIONS,
                                            .AMOUNT_WRONGFUL = p.p.AMOUNT_WRONGFUL,
                                            .ALLOWANCE_TERMINATE = p.p.ALLOWANCE_TERMINATE,
                                            .TRAINING_COSTS = p.p.TRAINING_COSTS,
                                            .OTHER_COMPENSATION = p.p.OTHER_COMPENSATION,
                                             .COMPENSATORY_LEAVE = p.p.COMPENSATORY_LEAVE,
                                             .COMPENSATORY_PAYMENT = p.p.COMPENSATORY_PAYMENT,
                                             .CREATED_DATE = p.p.CREATED_DATE,
                                             .ID_NO = p.cv.ID_NO,
                                             .TYPE_TERMINATE = p.p.TYPE_TERMINATE,
                                             .SALARYMEDIUM = p.p.SALARYMEDIUM,
                                             .YEARFORALLOW = p.p.YEARFORALLOW,
                                             .MONEYALLOW = p.p.MONEYALLOW,
                                             .REMAINING_LEAVE_MONEY = p.p.REMAINING_LEAVE_MONEY,
                                             .IDENTIFI_VIOLATE_MONEY = p.p.IDENTIFI_VIOLATE_MONEY,
                                             .MONEY_RETURN = p.p.MONEY_RETURN,
                                             .REMARK = p.p.REMARK,
                                             .SIGN_NAME = p.p.SIGN_NAME,
                                             .SIGN_DATE = p.p.SIGN_DATE,
                                             .SIGN_TITLE = p.p.SIGN_TITLE,
                                             .SALARYMEDIUM_LOSS = p.p.SALARYMEDIUM_LOSS,
                                             .FILENAME = p.p.FILENAME,
                                             .SUM_DEBT = p.p.SUM_DEBT,
                                             .UPLOADFILE = p.p.UPLOADFILE,
                                             .TER_REASON_NAME = p.ter_rea_name.NAME_VN,
                                             .SUM_COLLECT_DEBT = p.p.SUM_COLLECT_DEBT,
                                             .DECISION_TYPE_NAME = p.type.NAME_VN,
                                             .ALLOW_NAME = If(p.p.IS_ALLOW <> 0, "x", ""),
                                             .JOB_LOSS_ALLOWANCE_NAME = If(p.p.IS_JOB_LOSS_ALLOWANCE <> 0, "x", "")})

            If _filter.EMPLOYEE_CODE IsNot Nothing Then
                terminate = terminate.Where(Function(p) p.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If

            If _filter.EMPLOYEE_NAME IsNot Nothing Then
                terminate = terminate.Where(Function(p) p.EMPLOYEE_NAME.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If

            If _filter.DECISION_TYPE_NAME IsNot Nothing Then
                terminate = terminate.Where(Function(p) p.DECISION_TYPE_NAME.ToUpper.Contains(_filter.DECISION_TYPE_NAME.ToUpper))
            End If

            If _filter.ALLOW_NAME IsNot Nothing Then
                terminate = terminate.Where(Function(p) p.ALLOW_NAME.ToUpper.Contains(_filter.ALLOW_NAME.ToUpper))
            End If

            If _filter.JOB_LOSS_ALLOWANCE_NAME IsNot Nothing Then
                terminate = terminate.Where(Function(p) p.JOB_LOSS_ALLOWANCE_NAME.ToUpper.Contains(_filter.JOB_LOSS_ALLOWANCE_NAME.ToUpper))
            End If

            If _filter.REMARK IsNot Nothing Then
                terminate = terminate.Where(Function(p) p.REMARK.ToUpper.Contains(_filter.REMARK.ToUpper))
            End If
            If _filter.EMPLOYEE_ID IsNot Nothing Then
                terminate = terminate.Where(Function(p) p.EMPLOYEE_ID = _filter.EMPLOYEE_ID)
            End If
            If _filter.FROM_LAST_DATE IsNot Nothing Then
                terminate = terminate.Where(Function(p) p.LAST_DATE >= _filter.FROM_LAST_DATE)
            End If
            If _filter.TO_LAST_DATE IsNot Nothing Then
                terminate = terminate.Where(Function(p) p.LAST_DATE <= _filter.TO_LAST_DATE)
            End If
            If _filter.FROM_SEND_DATE IsNot Nothing Then
                terminate = terminate.Where(Function(p) p.SEND_DATE >= _filter.FROM_SEND_DATE)
            End If
            If _filter.TO_SEND_DATE IsNot Nothing Then
                terminate = terminate.Where(Function(p) p.SEND_DATE <= _filter.TO_SEND_DATE)
            End If
            'If _filter.STATUS_ID <> 0 Then
            '    terminate = terminate.Where(Function(p) p.STATUS_ID = _filter.STATUS_ID)
            'End If
            If _filter.IS_NOHIRE Then
                terminate = terminate.Where(Function(p) p.IS_NOHIRE_SHORT = _filter.IS_NOHIRE)
            End If
            If _filter.ID_NO IsNot Nothing Then
                terminate = terminate.Where(Function(p) p.ID_NO.ToUpper.Contains(_filter.ID_NO.ToUpper))
            End If

            If _filter.ORG_NAME2 <> "" Then
                terminate = terminate.Where(Function(p) p.ORG_NAME2.ToUpper().IndexOf(_filter.ORG_NAME2.ToUpper) >= 0)
            End If

            If _filter.ORG_NAME3 <> "" Then
                terminate = terminate.Where(Function(p) p.ORG_NAME3.ToUpper().IndexOf(_filter.ORG_NAME3.ToUpper) >= 0)
            End If

            If _filter.ORG_NAME4 <> "" Then
                terminate = terminate.Where(Function(p) p.ORG_NAME4.ToUpper().IndexOf(_filter.ORG_NAME4.ToUpper) >= 0)
            End If

            If _filter.ORG_NAME5 <> "" Then
                terminate = terminate.Where(Function(p) p.ORG_NAME5.ToUpper().IndexOf(_filter.ORG_NAME5.ToUpper) >= 0)
            End If

            If _filter.ORG_NAME6 <> "" Then
                terminate = terminate.Where(Function(p) p.ORG_NAME6.ToUpper().IndexOf(_filter.ORG_NAME6.ToUpper) >= 0)
            End If

            If _filter.ORG_NAME7 <> "" Then
                terminate = terminate.Where(Function(p) p.ORG_NAME7.ToUpper().IndexOf(_filter.ORG_NAME7.ToUpper) >= 0)
            End If

            terminate = terminate.OrderBy(Sorts)
            Total = terminate.Count
            terminate = terminate.Skip(PageIndex * PageSize).Take(PageSize)
            Return terminate.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetPensionBenefits(ByVal _filter As TerminateDTO,
                         ByVal PageIndex As Integer,
                         ByVal PageSize As Integer,
                         ByRef Total As Integer, ByVal _param As ParamDTO,
                         Optional ByVal Sorts As String = "CREATED_DATE desc",
                         Optional ByVal log As UserLog = Nothing) As List(Of TerminateDTO)

        Try

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using

            Dim query = From p In Context.HU_TERMINATE
                        From type In Context.OT_OTHER_LIST.Where(Function(f) p.DECISION_TYPE = f.ID).DefaultIfEmpty
                        From e In Context.HU_EMPLOYEE.Where(Function(f) p.EMPLOYEE_ID = f.ID)
                        From cv In Context.HU_EMPLOYEE_CV.Where(Function(f) p.EMPLOYEE_ID = f.EMPLOYEE_ID)
                        From ac In Context.HUV_ADDRESS.Where(Function(f) cv.EMPLOYEE_ID = f.EMPLOYEE_ID).DefaultIfEmpty
                        From g In Context.OT_OTHER_LIST.Where(Function(f) cv.GENDER = f.ID).DefaultIfEmpty
                        From o In Context.HUV_ORGANIZATION.Where(Function(f) e.ORG_ID = f.ID)
                        From t In Context.HU_TITLE.Where(Function(f) e.TITLE_ID = f.ID)
                        From deci_type In Context.OT_OTHER_LIST.Where(Function(f) p.TYPE_TERMINATE = f.ID).DefaultIfEmpty
                        From status In Context.OT_OTHER_LIST.Where(Function(f) p.STATUS_ID = f.ID).DefaultIfEmpty
                        From status_BH In Context.OT_OTHER_LIST.Where(Function(f) p.INSURANCE_STATUS = f.ID).DefaultIfEmpty
                        From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = e.ORG_ID And f.USERNAME = log.Username.ToUpper)
                        From ter_rea_name In Context.OT_OTHER_LIST.Where(Function(f) p.TER_REASON = f.ID).DefaultIfEmpty
                        Where p.STATUS_ID = 447 AndAlso p.IS_ALLOW = -1
            Dim terminate = query.Select(Function(p) New TerminateDTO With {
                                             .ID = p.p.ID,
                                             .MOBILE_PHONE = p.cv.MOBILE_PHONE,
                                             .GENDER_NAME = p.g.NAME_VN,
                                             .PER_ADDRESS = p.ac.PER_ADDRESS,
                                             .NAV_ADDRESS = p.ac.NAV_ADDRESS,
                                             .BIRTH_DATE = p.cv.BIRTH_DATE,
                                             .IS_DAMAT_NAME = If(p.p.IS_DAMAT = 1, "Đã mất", ""),
                                             .REASON_BLACK_LIST = p.p.REASON_BLACK_LIST,
                                             .DECISION_NO = p.p.DECISION_NO,
                                             .STATUS_NAME = p.status.NAME_VN,
                                             .STATUS_CODE = p.status.CODE,
                                             .STATUS_ID = p.status.ID,
                                             .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                             .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                             .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                                             .JOIN_DATE = p.e.JOIN_DATE,
                                             .CODE = p.deci_type.CODE,
                                             .IS_NOHIRE = p.p.IS_NOHIRE,
                                             .IS_NOHIRE_SHORT = p.p.IS_NOHIRE,
                                             .LAST_DATE = p.p.LAST_DATE,
                                             .SEND_DATE = p.p.SEND_DATE,
                                             .EFFECT_DATE = p.p.EFFECT_DATE,
                                             .ORG_NAME = p.o.NAME_VN,
                                             .ORG_NAME2 = p.o.ORG_NAME2,
                                             .ORG_NAME3 = p.o.ORG_NAME3,
                                             .ORG_NAME4 = p.o.ORG_NAME4,
                                             .ORG_NAME5 = p.o.ORG_NAME5,
                                             .ORG_NAME6 = p.o.ORG_NAME6,
                                             .ORG_NAME7 = p.o.ORG_NAME7,
                                             .ORG_DESC = p.o.DESCRIPTION_PATH,
                                             .TER_REASON_DETAIL = p.p.TER_REASON_DETAIL,
                                             .TITLE_NAME = p.t.NAME_VN,
                                             .APPROVAL_DATE = p.p.APPROVAL_DATE,
                                            .IDENTIFI_CARD = p.p.IDENTIFI_CARD,
                                            .IDENTIFI_RDATE = p.p.IDENTIFI_RDATE,
                                            .IDENTIFI_STATUS = p.p.IDENTIFI_STATUS,
                                             .IDENTIFI_MONEY = p.p.IDENTIFI_MONEY,
                                            .SUN_CARD = p.p.SUN_CARD,
                                            .SUN_RDATE = p.p.SUN_RDATE,
                                            .SUN_STATUS = p.p.SUN_STATUS,
                                            .SUN_MONEY = p.p.SUN_MONEY,
                                            .INSURANCE_CARD = p.p.INSURANCE_CARD,
                                            .INSURANCE_RDATE = p.p.INSURANCE_RDATE,
                                            .INSURANCE_STATUS = p.status_BH.NAME_VN,
                                             .INSURANCE_STATUS_NAME = p.status_BH.NAME_EN,
                                            .INSURANCE_MONEY = p.p.INSURANCE_MONEY,
                                            .REMAINING_LEAVE = p.p.REMAINING_LEAVE,
                                            .PAYMENT_LEAVE = p.p.PAYMENT_LEAVE,
                                            .AMOUNT_VIOLATIONS = p.p.AMOUNT_VIOLATIONS,
                                            .AMOUNT_WRONGFUL = p.p.AMOUNT_WRONGFUL,
                                            .ALLOWANCE_TERMINATE = p.p.ALLOWANCE_TERMINATE,
                                            .TRAINING_COSTS = p.p.TRAINING_COSTS,
                                            .OTHER_COMPENSATION = p.p.OTHER_COMPENSATION,
                                             .COMPENSATORY_LEAVE = p.p.COMPENSATORY_LEAVE,
                                             .COMPENSATORY_PAYMENT = p.p.COMPENSATORY_PAYMENT,
                                             .CREATED_DATE = p.p.CREATED_DATE,
                                             .ID_NO = p.cv.ID_NO,
                                             .TYPE_TERMINATE = p.p.TYPE_TERMINATE,
                                             .SALARYMEDIUM = p.p.SALARYMEDIUM,
                                             .YEARFORALLOW = p.p.YEARFORALLOW,
                                             .MONEYALLOW = p.p.MONEYALLOW,
                                             .REMAINING_LEAVE_MONEY = p.p.REMAINING_LEAVE_MONEY,
                                             .IDENTIFI_VIOLATE_MONEY = p.p.IDENTIFI_VIOLATE_MONEY,
                                             .MONEY_RETURN = p.p.MONEY_RETURN,
                                             .REMARK = p.p.REMARK,
                                             .SIGN_NAME = p.p.SIGN_NAME,
                                             .SIGN_DATE = p.p.SIGN_DATE,
                                             .SIGN_TITLE = p.p.SIGN_TITLE,
                                             .SALARYMEDIUM_LOSS = p.p.SALARYMEDIUM_LOSS,
                                             .FILENAME = p.p.FILENAME,
                                             .SUM_DEBT = p.p.SUM_DEBT,
                                             .UPLOADFILE = p.p.UPLOADFILE,
                                             .TER_REASON_NAME = p.ter_rea_name.NAME_VN,
                                             .SUM_COLLECT_DEBT = p.p.SUM_COLLECT_DEBT,
                                             .DECISION_TYPE_NAME = p.type.NAME_VN,
                                             .ALLOW_NAME = If(p.p.IS_ALLOW <> 0, "x", ""),
                                             .JOB_LOSS_ALLOWANCE_NAME = If(p.p.IS_JOB_LOSS_ALLOWANCE <> 0, "x", "")})

            If _filter.EMPLOYEE_CODE IsNot Nothing Then
                terminate = terminate.Where(Function(p) p.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If

            If _filter.EMPLOYEE_NAME IsNot Nothing Then
                terminate = terminate.Where(Function(p) p.EMPLOYEE_NAME.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If

            If _filter.NAV_ADDRESS IsNot Nothing Then
                terminate = terminate.Where(Function(p) p.NAV_ADDRESS.ToUpper.Contains(_filter.NAV_ADDRESS.ToUpper))
            End If

            If _filter.PER_ADDRESS IsNot Nothing Then
                terminate = terminate.Where(Function(p) p.PER_ADDRESS.ToUpper.Contains(_filter.PER_ADDRESS.ToUpper))
            End If

            If _filter.IS_DAMAT_NAME IsNot Nothing Then
                terminate = terminate.Where(Function(p) p.IS_DAMAT_NAME.ToUpper.Contains(_filter.IS_DAMAT_NAME.ToUpper))
            End If

            If _filter.DECISION_TYPE_NAME IsNot Nothing Then
                terminate = terminate.Where(Function(p) p.DECISION_TYPE_NAME.ToUpper.Contains(_filter.DECISION_TYPE_NAME.ToUpper))
            End If

            If _filter.ALLOW_NAME IsNot Nothing Then
                terminate = terminate.Where(Function(p) p.ALLOW_NAME.ToUpper.Contains(_filter.ALLOW_NAME.ToUpper))
            End If

            If _filter.JOB_LOSS_ALLOWANCE_NAME IsNot Nothing Then
                terminate = terminate.Where(Function(p) p.JOB_LOSS_ALLOWANCE_NAME.ToUpper.Contains(_filter.JOB_LOSS_ALLOWANCE_NAME.ToUpper))
            End If

            If _filter.REMARK IsNot Nothing Then
                terminate = terminate.Where(Function(p) p.REMARK.ToUpper.Contains(_filter.REMARK.ToUpper))
            End If
            If _filter.EMPLOYEE_ID IsNot Nothing Then
                terminate = terminate.Where(Function(p) p.EMPLOYEE_ID = _filter.EMPLOYEE_ID)
            End If
            If _filter.FROM_LAST_DATE IsNot Nothing Then
                terminate = terminate.Where(Function(p) p.LAST_DATE >= _filter.FROM_LAST_DATE)
            End If
            If _filter.TO_LAST_DATE IsNot Nothing Then
                terminate = terminate.Where(Function(p) p.LAST_DATE <= _filter.TO_LAST_DATE)
            End If
            If _filter.FROM_SEND_DATE IsNot Nothing Then
                terminate = terminate.Where(Function(p) p.SEND_DATE >= _filter.FROM_SEND_DATE)
            End If
            If _filter.TO_SEND_DATE IsNot Nothing Then
                terminate = terminate.Where(Function(p) p.SEND_DATE <= _filter.TO_SEND_DATE)
            End If
            'If _filter.STATUS_ID <> 0 Then
            '    terminate = terminate.Where(Function(p) p.STATUS_ID = _filter.STATUS_ID)
            'End If
            If _filter.IS_NOHIRE Then
                terminate = terminate.Where(Function(p) p.IS_NOHIRE_SHORT = _filter.IS_NOHIRE)
            End If
            If _filter.ID_NO IsNot Nothing Then
                terminate = terminate.Where(Function(p) p.ID_NO.ToUpper.Contains(_filter.ID_NO.ToUpper))
            End If

            If _filter.ORG_NAME2 <> "" Then
                terminate = terminate.Where(Function(p) p.ORG_NAME2.ToUpper().IndexOf(_filter.ORG_NAME2.ToUpper) >= 0)
            End If

            If _filter.ORG_NAME3 <> "" Then
                terminate = terminate.Where(Function(p) p.ORG_NAME3.ToUpper().IndexOf(_filter.ORG_NAME3.ToUpper) >= 0)
            End If

            If _filter.ORG_NAME4 <> "" Then
                terminate = terminate.Where(Function(p) p.ORG_NAME4.ToUpper().IndexOf(_filter.ORG_NAME4.ToUpper) >= 0)
            End If

            If _filter.ORG_NAME5 <> "" Then
                terminate = terminate.Where(Function(p) p.ORG_NAME5.ToUpper().IndexOf(_filter.ORG_NAME5.ToUpper) >= 0)
            End If

            If _filter.ORG_NAME6 <> "" Then
                terminate = terminate.Where(Function(p) p.ORG_NAME6.ToUpper().IndexOf(_filter.ORG_NAME6.ToUpper) >= 0)
            End If

            If _filter.ORG_NAME7 <> "" Then
                terminate = terminate.Where(Function(p) p.ORG_NAME7.ToUpper().IndexOf(_filter.ORG_NAME7.ToUpper) >= 0)
            End If

            terminate = terminate.OrderBy(Sorts)
            Total = terminate.Count
            terminate = terminate.Skip(PageIndex * PageSize).Take(PageSize)
            Return terminate.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function UpdatePensinBenefits(ByVal lstID As List(Of Decimal), ByVal status As Decimal, ByVal log As UserLog) As Boolean
        Try
            For Each item In lstID
                Dim obj As New HU_TERMINATE With {.ID = item}
                Context.HU_TERMINATE.Attach(obj)
                obj.IS_DAMAT = status
            Next

            Context.SaveChanges(log)

            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function GetTerminateCopy(ByVal _filter As TerminateDTO,
                                 ByVal PageIndex As Integer,
                                 ByVal PageSize As Integer,
                                 ByRef Total As Integer, ByVal _param As ParamDTO,
                                 Optional ByVal Sorts As String = "CREATED_DATE desc",
                                 Optional ByVal log As UserLog = Nothing) As List(Of TerminateDTO)

        Try

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using


            Dim query = From p In Context.HU_TERMINATE
                        From e In Context.HU_EMPLOYEE.Where(Function(f) p.EMPLOYEE_ID = f.ID)
                        From cv In Context.HU_EMPLOYEE_CV.Where(Function(f) p.EMPLOYEE_ID = f.EMPLOYEE_ID)
                        From o In Context.HU_ORGANIZATION.Where(Function(f) e.ORG_ID = f.ID)
                        From t In Context.HU_TITLE.Where(Function(f) e.TITLE_ID = f.ID)
                        From deci_type In Context.OT_OTHER_LIST.Where(Function(f) p.TYPE_TERMINATE = f.ID).DefaultIfEmpty
                        From status In Context.OT_OTHER_LIST.Where(Function(f) p.STATUS_ID = f.ID)
                        From status_BH In Context.OT_OTHER_LIST.Where(Function(f) p.INSURANCE_STATUS = f.ID).DefaultIfEmpty
                        From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = e.ORG_ID And f.USERNAME = log.Username.ToUpper)
                        From ter_rea_name In Context.OT_OTHER_LIST.Where(Function(f) p.TER_REASON = f.ID).DefaultIfEmpty
            Dim terminate = query.Select(Function(p) New TerminateDTO With {
                                             .ID = p.e.ID,
                                             .DECISION_NO = p.p.DECISION_NO,
                                             .STATUS_NAME = p.status.NAME_VN,
                                             .STATUS_CODE = p.status.CODE,
                                             .STATUS_ID = p.status.ID,
                                             .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                             .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                             .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                                             .JOIN_DATE = p.e.JOIN_DATE,
                                             .CODE = p.deci_type.CODE,
                                             .IS_NOHIRE = p.p.IS_NOHIRE,
                                             .IS_NOHIRE_SHORT = p.p.IS_NOHIRE,
                                             .LAST_DATE = p.p.LAST_DATE,
                                             .SEND_DATE = p.p.SEND_DATE,
                                             .EFFECT_DATE = p.p.EFFECT_DATE,
                                             .ORG_NAME = p.o.NAME_VN,
                                             .ORG_DESC = p.o.DESCRIPTION_PATH,
                                             .TER_REASON_DETAIL = p.p.TER_REASON_DETAIL,
                                             .TITLE_NAME = p.t.NAME_VN,
                                             .APPROVAL_DATE = p.p.APPROVAL_DATE,
                                            .IDENTIFI_CARD = p.p.IDENTIFI_CARD,
                                            .IDENTIFI_RDATE = p.p.IDENTIFI_RDATE,
                                            .IDENTIFI_STATUS = p.p.IDENTIFI_STATUS,
                                             .IDENTIFI_MONEY = p.p.IDENTIFI_MONEY,
                                            .SUN_CARD = p.p.SUN_CARD,
                                            .SUN_RDATE = p.p.SUN_RDATE,
                                            .SUN_STATUS = p.p.SUN_STATUS,
                                            .SUN_MONEY = p.p.SUN_MONEY,
                                            .INSURANCE_CARD = p.p.INSURANCE_CARD,
                                            .INSURANCE_RDATE = p.p.INSURANCE_RDATE,
                                            .INSURANCE_STATUS = p.status_BH.NAME_VN,
                                             .INSURANCE_STATUS_NAME = p.status_BH.NAME_EN,
                                            .INSURANCE_MONEY = p.p.INSURANCE_MONEY,
                                            .REMAINING_LEAVE = p.p.REMAINING_LEAVE,
                                            .PAYMENT_LEAVE = p.p.PAYMENT_LEAVE,
                                            .AMOUNT_VIOLATIONS = p.p.AMOUNT_VIOLATIONS,
                                            .AMOUNT_WRONGFUL = p.p.AMOUNT_WRONGFUL,
                                            .ALLOWANCE_TERMINATE = p.p.ALLOWANCE_TERMINATE,
                                            .TRAINING_COSTS = p.p.TRAINING_COSTS,
                                            .OTHER_COMPENSATION = p.p.OTHER_COMPENSATION,
                                             .COMPENSATORY_LEAVE = p.p.COMPENSATORY_LEAVE,
                                             .COMPENSATORY_PAYMENT = p.p.COMPENSATORY_PAYMENT,
                                             .CREATED_DATE = p.p.CREATED_DATE,
                                             .ID_NO = p.cv.ID_NO,
                                             .TYPE_TERMINATE = p.p.TYPE_TERMINATE,
                                             .SALARYMEDIUM = p.p.SALARYMEDIUM,
                                             .YEARFORALLOW = p.p.YEARFORALLOW,
                                             .MONEYALLOW = p.p.MONEYALLOW,
                                             .REMAINING_LEAVE_MONEY = p.p.REMAINING_LEAVE_MONEY,
                                             .IDENTIFI_VIOLATE_MONEY = p.p.IDENTIFI_VIOLATE_MONEY,
                                             .MONEY_RETURN = p.p.MONEY_RETURN,
                                             .REMARK = p.p.REMARK,
                                             .SIGN_NAME = p.p.SIGN_NAME,
                                             .SIGN_DATE = p.p.SIGN_DATE,
                                             .SIGN_TITLE = p.p.SIGN_TITLE,
                                             .SALARYMEDIUM_LOSS = p.p.SALARYMEDIUM_LOSS,
                                             .FILENAME = p.p.FILENAME,
                                             .SUM_DEBT = p.p.SUM_DEBT,
                                             .UPLOADFILE = p.p.UPLOADFILE,
                                             .TER_REASON_NAME = p.ter_rea_name.NAME_VN,
                                             .SUM_COLLECT_DEBT = p.p.SUM_COLLECT_DEBT,
                                             .IS_COPY = p.p.IS_COPY,
                                             .COPY_STATUS_NAME = If(p.p.IS_COPY = -1, "Đã sao chép", "")})

            If _filter.EMPLOYEE_CODE IsNot Nothing Then
                terminate = terminate.Where(Function(p) p.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If

            If _filter.EMPLOYEE_NAME IsNot Nothing Then
                terminate = terminate.Where(Function(p) p.EMPLOYEE_NAME.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If

            If _filter.REMARK IsNot Nothing Then
                terminate = terminate.Where(Function(p) p.REMARK.ToUpper.Contains(_filter.REMARK.ToUpper))
            End If
            If _filter.EMPLOYEE_ID IsNot Nothing Then
                terminate = terminate.Where(Function(p) p.EMPLOYEE_ID = _filter.EMPLOYEE_ID)
            End If
            If _filter.FROM_LAST_DATE IsNot Nothing Then
                terminate = terminate.Where(Function(p) p.LAST_DATE >= _filter.FROM_LAST_DATE)
            End If
            If _filter.TO_LAST_DATE IsNot Nothing Then
                terminate = terminate.Where(Function(p) p.LAST_DATE <= _filter.TO_LAST_DATE)
            End If
            If _filter.FROM_SEND_DATE IsNot Nothing Then
                terminate = terminate.Where(Function(p) p.SEND_DATE >= _filter.FROM_SEND_DATE)
            End If
            If _filter.TO_SEND_DATE IsNot Nothing Then
                terminate = terminate.Where(Function(p) p.SEND_DATE <= _filter.TO_SEND_DATE)
            End If
            If _filter.STATUS_ID <> 0 Then
                terminate = terminate.Where(Function(p) p.STATUS_ID = _filter.STATUS_ID)
            End If
            If _filter.IS_NOHIRE Then
                terminate = terminate.Where(Function(p) p.IS_NOHIRE_SHORT = _filter.IS_NOHIRE)
            End If
            If _filter.ID_NO IsNot Nothing Then
                terminate = terminate.Where(Function(p) p.ID_NO.ToUpper.Contains(_filter.ID_NO.ToUpper))
            End If

            terminate = terminate.OrderBy(Sorts)
            Total = terminate.Count
            terminate = terminate.Skip(PageIndex * PageSize).Take(PageSize)
            Return terminate.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetTerminateByID(ByVal _filter As TerminateDTO) As TerminateDTO

        Try
            Dim query = From p In Context.HU_TERMINATE
                        Join e In Context.HU_EMPLOYEE On p.EMPLOYEE_ID Equals e.ID
                        Join o In Context.HU_ORGANIZATION On e.ORG_ID Equals o.ID
                        Join t In Context.HU_TITLE On e.TITLE_ID Equals t.ID
                        From te In Context.AT_ENTITLEMENT.Where(Function(f) p.EMPLOYEE_ID = f.EMPLOYEE_ID).DefaultIfEmpty()

            query = query.Where(Function(p) _filter.ID = p.p.ID)

            Dim obj = query.Select(Function(p) New TerminateDTO With {
                                             .ID = p.p.ID,
                                             .DECISION_NO = p.p.DECISION_NO,
                                             .STATUS_ID = p.p.STATUS_ID,
                                             .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                             .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                             .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                                             .JOIN_DATE = p.e.JOIN_DATE,
                                             .IS_NOHIRE = p.p.IS_NOHIRE,
                                             .LAST_DATE = p.p.LAST_DATE,
                                             .SEND_DATE = p.p.SEND_DATE,
                                             .ORG_ID = p.o.ID,
                                             .ORG_NAME = p.o.NAME_VN,
                                             .ORG_DESC = p.o.NAME_VN,
                                             .TER_REASON_DETAIL = p.p.TER_REASON_DETAIL,
                                             .TITLE_ID = p.t.ID,
                                             .TITLE_NAME = p.t.NAME_VN,
                                             .EMPLOYEE_SENIORITY = p.p.EMP_SENIORITY,
                                             .REMARK = p.p.REMARK,
                                             .CREATED_DATE = p.p.CREATED_DATE,
                                             .APPROVAL_DATE = p.p.APPROVAL_DATE,
                                            .IDENTIFI_CARD = p.p.IDENTIFI_CARD,
                                            .IDENTIFI_RDATE = p.p.IDENTIFI_RDATE,
                                            .IDENTIFI_STATUS = p.p.IDENTIFI_STATUS,
                                             .IDENTIFI_MONEY = p.p.IDENTIFI_MONEY,
                                            .SUN_CARD = p.p.SUN_CARD,
                                            .SUN_RDATE = p.p.SUN_RDATE,
                                            .SUN_STATUS = p.p.SUN_STATUS,
                                             .SUN_MONEY = p.p.SUN_MONEY,
                                            .INSURANCE_CARD = p.p.INSURANCE_CARD,
                                            .INSURANCE_RDATE = p.p.INSURANCE_RDATE,
                                            .INSURANCE_STATUS = p.p.INSURANCE_STATUS,
                                             .INSURANCE_MONEY = p.p.INSURANCE_MONEY,
                                            .REMAINING_LEAVE = p.p.REMAINING_LEAVE,
                                            .PAYMENT_LEAVE = p.p.PAYMENT_LEAVE,
                                            .AMOUNT_VIOLATIONS = p.p.AMOUNT_VIOLATIONS,
                                            .AMOUNT_WRONGFUL = p.p.AMOUNT_WRONGFUL,
                                            .ALLOWANCE_TERMINATE = p.p.ALLOWANCE_TERMINATE,
                                            .TRAINING_COSTS = p.p.TRAINING_COSTS,
                                            .OTHER_COMPENSATION = p.p.OTHER_COMPENSATION,
                                             .COMPENSATORY_PAYMENT = p.p.COMPENSATORY_PAYMENT,
                                             .COMPENSATORY_LEAVE = p.p.COMPENSATORY_LEAVE,
                                             .EFFECT_DATE = p.p.EFFECT_DATE,
                                             .SIGN_ID = p.p.SIGN_ID,
                                             .SIGN_DATE = p.p.SIGN_DATE,
                                             .SIGN_NAME = p.p.SIGN_NAME,
                                             .SIGN_TITLE = p.p.SIGN_TITLE,
                                             .UPLOADFILE = p.p.UPLOADFILE,
                                             .FILENAME = p.p.FILENAME,
                                             .SALARYMEDIUM = p.p.SALARYMEDIUM,
                                             .YEARFORALLOW = p.p.YEARFORALLOW,
                                             .MONEYALLOW = p.p.MONEYALLOW,
                                             .REMAINING_LEAVE_MONEY = p.p.REMAINING_LEAVE_MONEY,
                                             .IDENTIFI_VIOLATE_MONEY = p.p.IDENTIFI_VIOLATE_MONEY,
                                             .MONEY_RETURN = p.p.MONEY_RETURN,
                                             .TYPE_TERMINATE = p.p.TYPE_TERMINATE,
                                             .WORK_STATUS = p.e.WORK_STATUS,
                                             .SALARYMEDIUM_LOSS = p.p.SALARYMEDIUM_LOSS,
                                             .TER_REASON = p.p.TER_REASON,
                                             .SUM_DEBT = p.p.SUM_DEBT,
                                             .SUM_COLLECT_DEBT = p.p.SUM_COLLECT_DEBT,
                                             .AMOUNT_PAYMENT_CASH = p.p.AMOUNT_PAYMENT_CASH,
                                             .AMOUNT_DEDUCT_FROM_SAL = p.p.AMOUNT_DEDUCT_FROM_SAL,
                                             .PERIOD_ID = p.p.PERIOD_ID,
                                             .DECISION_TYPE = p.p.DECISION_TYPE,
                                             .IS_ALLOW = p.p.IS_ALLOW,
                                             .NOTIFY_NO = p.p.NOTIFY_NO,
                                             .TER_DATE = p.p.TER_DATE,
                                             .IS_BLACK_LIST = p.p.IS_BLACK_LIST,
                                             .REASON_BLACK_LIST = p.p.REASON_BLACK_LIST,
                                             .TRUYTHU_BHYT = p.p.TRUYTHU_BHYT,
                                             .O_HI_COM = p.p.O_HI_COM,
                                             .O_HI_EMP = p.p.O_HI_EMP,
                                             .IS_JOB_LOSS_ALLOWANCE = p.p.IS_JOB_LOSS_ALLOWANCE,
                                             .YEAR_JOB_LOSS_ALLOWANCE = p.p.YEAR_JOB_LOSS_ALLOWANCE,
                                             .MONEY_JOB_LOSS_ALLOWANCE = p.p.MONEY_JOB_LOSS_ALLOWANCE,
                                             .MONTH_JOB_LOSS_ALLOWANCE = p.p.MONTH_JOB_LOSS_ALLOWANCE,
                                             .PAYPACK_UNIFORM = p.p.PAYPACK_UNIFORM,
                                             .PERIOD_TEXT = p.p.PERIOD_TEXT,
                                             .IS_RETURN_INSAL = p.p.IS_RETURN_INSAL,
                                             .IS_REPLACE_POS = p.p.IS_REPLACE_POS,
                                             .REVERSE_SENIORITY = p.p.REVERSE_SENIORITY,
                                             .REMARK_QT = p.p.REMARK_QT,
                                             .UNIFORM = p.p.UNIFORM})

            Dim ter = obj.FirstOrDefault

            'ter.lstDebts = (From p In Context.HU_DEBT
            '                From t In Context.HU_TERMINATE.Where(Function(f) f.EMPLOYEE_ID = p.EMPLOYEE_ID)
            '                Select New DebtDTO With)

            'ter.lstHandoverContent = (From hc In Context.OT_OTHER_LIST
            '                          From p In Context.HU_TRANSFER_TERMINATE.Where(Function(f) f.CONTENT_ID = hc.ID And f.TERMINATE_ID = ter.ID).DefaultIfEmpty
            '                          Where hc.TYPE_ID = 2270
            '                          Order By hc.NAME_VN
            '                          Select New HandoverContentDTO With {.TERMINATE_ID = p.TERMINATE_ID,
            '                                                              .CONTENT_NAME = hc.NAME_VN,
            '                                                              .IS_FINISH = p.IS_FINISH}).ToList
            ter.lstHandoverContent = (From tr In Context.HU_TRANSFER_TERMINATE
                                      From ot In Context.OT_OTHER_LIST.Where(Function(f) f.ID = tr.CONTENT_ID).DefaultIfEmpty
                                      Where tr.TERMINATE_ID = ter.ID
                                      Order By ot.NAME_VN
                                      Select New HandoverContentDTO With {.TERMINATE_ID = tr.TERMINATE_ID,
                                                                          .CONTENT_ID = tr.CONTENT_ID,
                                                                          .CONTENT_NAME = ot.NAME_VN,
                                                                          .IS_FINISH = tr.IS_FINISH}).ToList

            Return ter
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try


    End Function

    Public Function GetListEmployeeConcurrentlyByID(ByVal gID As Decimal) As List(Of EmployeeDTO)
        Dim lst As List(Of EmployeeDTO)
        Try
            Dim empCode = (From p In Context.HU_EMPLOYEE Where p.ID = gID Select p.EMPLOYEE_CODE).FirstOrDefault
            lst = (From p In Context.HU_EMPLOYEE
                   From mt In Context.HU_TITLE_BLD.Where(Function(f) f.ID = p.MATHE).DefaultIfEmpty
                   From staffrank In Context.HU_STAFF_RANK.Where(Function(f) p.STAFF_RANK_ID = f.ID).DefaultIfEmpty
                   From o In Context.HU_ORGANIZATION.Where(Function(f) p.ORG_ID = f.ID)
                   From t In Context.HU_TITLE.Where(Function(f) p.TITLE_ID = f.ID)
                   From c In Context.HU_CONTRACT.Where(Function(f) p.CONTRACT_ID = f.ID).DefaultIfEmpty
                   From working In Context.HU_WORKING.Where(Function(f) f.ID = p.LAST_WORKING_ID).DefaultIfEmpty
                   From l In Context.HU_LOCATION.Where(Function(f) f.ID = p.CONTRACTED_UNIT).DefaultIfEmpty
                   Where p.EMPLOYEE_CODE.Contains(empCode)
                   Select New EmployeeDTO With {
                       .ID = p.ID,
                       .MA_THE = mt.CODE,
                       .EMPLOYEE_CODE = p.EMPLOYEE_CODE,
                       .FULLNAME_VN = p.FULLNAME_VN,
                       .ORG_ID = p.ORG_ID,
                       .ORG_NAME = o.NAME_VN,
                       .ORG_CODE = o.CODE,
                       .TITLE_ID = t.ID,
                       .TITLE_NAME_VN = t.NAME_VN,
                       .JOIN_DATE = p.JOIN_DATE,
                       .JOIN_DATE_STATE = p.JOIN_DATE_STATE,
                       .CONTRACT_NO = c.CONTRACT_NO,
                       .CONTRACT_EFFECT_DATE = c.START_DATE,
                       .OBJECT_EMPLOYEE_ID = p.OBJECT_EMPLOYEE_ID,
                       .CONTRACT_EXPIRE_DATE = c.EXPIRE_DATE,
                       .STAFF_RANK_ID = p.STAFF_RANK_ID,
                       .STAFF_RANK_NAME = staffrank.NAME,
                       .WORK_STATUS = p.WORK_STATUS,
                       .TER_EFFECT_DATE = p.TER_EFFECT_DATE,
                       .LAST_WORKING_ID = p.LAST_WORKING_ID,
                       .SAL_BASIC = working.SAL_BASIC,
                       .CONTRACTED_UNIT_NAME = l.NAME_VN,
                       .COST_SUPPORT = working.COST_SUPPORT}).ToList

            Return lst
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function GetEmployeeByID(ByVal gID As Decimal) As EmployeeDTO
        Dim obj As EmployeeDTO
        Try
            obj = (From p In Context.HU_EMPLOYEE
                   From mt In Context.HU_TITLE_BLD.Where(Function(f) f.ID = p.MATHE).DefaultIfEmpty
                   From staffrank In Context.HU_STAFF_RANK.Where(Function(f) p.STAFF_RANK_ID = f.ID).DefaultIfEmpty
                   From o In Context.HU_ORGANIZATION.Where(Function(f) p.ORG_ID = f.ID)
                   From t In Context.HU_TITLE.Where(Function(f) p.TITLE_ID = f.ID)
                   From c In Context.HU_CONTRACT.Where(Function(f) p.CONTRACT_ID = f.ID).DefaultIfEmpty
                   From working In Context.HU_WORKING.Where(Function(f) f.ID = p.LAST_WORKING_ID).DefaultIfEmpty
                   From l In Context.HU_LOCATION.Where(Function(f) f.ID = p.CONTRACTED_UNIT).DefaultIfEmpty
                   Where p.ID = gID
                   Select New EmployeeDTO With {
                       .ID = p.ID,
                       .MA_THE = mt.CODE,
                       .EMPLOYEE_CODE = p.EMPLOYEE_CODE,
                       .FULLNAME_VN = p.FULLNAME_VN,
                       .ORG_ID = p.ORG_ID,
                       .ORG_NAME = o.NAME_VN,
                       .ORG_CODE = o.CODE,
                       .TITLE_ID = t.ID,
                       .TITLE_NAME_VN = t.NAME_VN,
                       .JOIN_DATE = p.JOIN_DATE,
                       .JOIN_DATE_STATE = p.JOIN_DATE_STATE,
                       .CONTRACT_NO = c.CONTRACT_NO,
                       .CONTRACT_EFFECT_DATE = c.START_DATE,
                       .OBJECT_EMPLOYEE_ID = p.OBJECT_EMPLOYEE_ID,
                       .CONTRACT_EXPIRE_DATE = c.EXPIRE_DATE,
                       .STAFF_RANK_ID = p.STAFF_RANK_ID,
                       .STAFF_RANK_NAME = staffrank.NAME,
                       .WORK_STATUS = p.WORK_STATUS,
                       .TER_EFFECT_DATE = p.TER_EFFECT_DATE,
                       .LAST_WORKING_ID = p.LAST_WORKING_ID,
                       .CONTRACT_ID = c.ID,
                       .SAL_BASIC = working.SAL_BASIC,
                       .CONTRACTED_UNIT_NAME = l.NAME_VN,
                       .COST_SUPPORT = working.COST_SUPPORT}).SingleOrDefault

            Return obj

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function InsertTerminate(ByVal objTerminate As TerminateDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim iCount As Integer = 0
        Dim objTerminateData As New HU_TERMINATE
        Try
            If objTerminate.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID Then
                Dim objSe_User = (From p In Context.SE_USER Where p.EMPLOYEE_ID = objTerminate.EMPLOYEE_ID).FirstOrDefault
                If objSe_User IsNot Nothing Then
                    objSe_User.EFFECTDATE_TERMINATION = objTerminate.EFFECT_DATE
                    objSe_User.EXPIRE_DATE = objTerminate.EFFECT_DATE
                    If objTerminate.EFFECT_DATE <= Date.Now.Date Then
                        objSe_User.ACTFLG = "I"
                    End If
                End If
            End If
            ' thêm nghỉ việc
            objTerminateData.ID = Utilities.GetNextSequence(Context, Context.HU_TERMINATE.EntitySet.Name)
            If objTerminate.ID = 0 Then
                objTerminate.ID = objTerminateData.ID
            End If
            ' bo sung truong HSV
            objTerminateData.NOTIFY_NO = objTerminate.NOTIFY_NO
            objTerminateData.TER_DATE = objTerminate.TER_DATE
            objTerminateData.IS_BLACK_LIST = objTerminate.IS_BLACK_LIST
            objTerminateData.REASON_BLACK_LIST = objTerminate.REASON_BLACK_LIST
            objTerminateData.TRUYTHU_BHYT = objTerminate.TRUYTHU_BHYT
            objTerminateData.O_HI_COM = objTerminate.O_HI_COM
            objTerminateData.O_HI_EMP = objTerminate.O_HI_EMP
            objTerminateData.IS_JOB_LOSS_ALLOWANCE = objTerminate.IS_JOB_LOSS_ALLOWANCE
            objTerminateData.YEAR_JOB_LOSS_ALLOWANCE = objTerminate.YEAR_JOB_LOSS_ALLOWANCE
            objTerminateData.MONTH_JOB_LOSS_ALLOWANCE = objTerminate.MONTH_JOB_LOSS_ALLOWANCE
            objTerminateData.MONEY_JOB_LOSS_ALLOWANCE = objTerminate.MONEY_JOB_LOSS_ALLOWANCE
            objTerminateData.PAYPACK_UNIFORM = objTerminate.PAYPACK_UNIFORM
            '--------------------
            objTerminateData.EMPLOYEE_ID = objTerminate.EMPLOYEE_ID
            objTerminateData.IS_NOHIRE = objTerminate.IS_NOHIRE
            objTerminateData.LAST_DATE = objTerminate.LAST_DATE
            objTerminateData.SEND_DATE = objTerminate.SEND_DATE
            objTerminateData.UPLOADFILE = objTerminate.UPLOADFILE
            objTerminateData.FILENAME = objTerminate.FILENAME
            objTerminateData.STATUS_ID = objTerminate.STATUS_ID
            objTerminateData.TER_REASON_DETAIL = objTerminate.TER_REASON_DETAIL
            objTerminateData.EMP_SENIORITY = objTerminate.EMPLOYEE_SENIORITY
            objTerminateData.REMARK = objTerminate.REMARK
            objTerminateData.CREATED_DATE = DateTime.Now
            objTerminateData.CREATED_BY = log.Username
            objTerminateData.CREATED_LOG = log.ComputerName
            objTerminateData.MODIFIED_DATE = DateTime.Now
            objTerminateData.MODIFIED_BY = log.Username
            objTerminateData.MODIFIED_LOG = log.ComputerName
            Dim empID = objTerminate.EMPLOYEE_ID
            Dim query As Decimal = (From p In Context.HU_WORKING Order By p.EFFECT_DATE Descending
                                    Where p.EMPLOYEE_ID = empID And p.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID Select p.ID).FirstOrDefault
            If query <> 0 Then
                objTerminateData.LAST_WORKING_ID = query
            End If
            objTerminateData.APPROVAL_DATE = objTerminate.APPROVAL_DATE
            objTerminateData.IDENTIFI_CARD = objTerminate.IDENTIFI_CARD
            objTerminateData.IDENTIFI_RDATE = objTerminate.IDENTIFI_RDATE
            objTerminateData.IDENTIFI_STATUS = objTerminate.IDENTIFI_STATUS
            objTerminateData.IDENTIFI_MONEY = objTerminate.IDENTIFI_MONEY
            objTerminateData.SUN_CARD = objTerminate.SUN_CARD
            objTerminateData.SUN_RDATE = objTerminate.SUN_RDATE
            objTerminateData.SUN_STATUS = objTerminate.SUN_STATUS
            objTerminateData.SUN_MONEY = objTerminate.SUN_MONEY
            objTerminateData.INSURANCE_CARD = objTerminate.INSURANCE_CARD
            objTerminateData.INSURANCE_RDATE = objTerminate.INSURANCE_RDATE
            objTerminateData.INSURANCE_STATUS = objTerminate.INSURANCE_STATUS
            objTerminateData.INSURANCE_MONEY = objTerminate.INSURANCE_MONEY
            objTerminateData.REMAINING_LEAVE = objTerminate.REMAINING_LEAVE
            objTerminateData.PAYMENT_LEAVE = objTerminate.PAYMENT_LEAVE
            objTerminateData.AMOUNT_VIOLATIONS = objTerminate.AMOUNT_VIOLATIONS
            objTerminateData.AMOUNT_WRONGFUL = objTerminate.AMOUNT_WRONGFUL
            objTerminateData.ALLOWANCE_TERMINATE = objTerminate.ALLOWANCE_TERMINATE
            objTerminateData.TRAINING_COSTS = objTerminate.TRAINING_COSTS
            objTerminateData.OTHER_COMPENSATION = objTerminate.OTHER_COMPENSATION
            objTerminateData.COMPENSATORY_LEAVE = objTerminate.COMPENSATORY_LEAVE
            objTerminateData.COMPENSATORY_PAYMENT = objTerminate.COMPENSATORY_PAYMENT

            objTerminateData.EFFECT_DATE = objTerminate.EFFECT_DATE
            objTerminateData.DECISION_NO = objTerminate.DECISION_NO
            objTerminateData.SIGN_DATE = objTerminate.SIGN_DATE
            objTerminateData.SIGN_ID = objTerminate.SIGN_ID
            objTerminateData.SIGN_NAME = objTerminate.SIGN_NAME
            objTerminateData.SIGN_TITLE = objTerminate.SIGN_TITLE

            'bỔ XUNG THÊM TRƯỜNG
            objTerminateData.SALARYMEDIUM = objTerminate.SALARYMEDIUM
            objTerminateData.YEARFORALLOW = objTerminate.YEARFORALLOW
            objTerminateData.MONEYALLOW = objTerminate.MONEYALLOW
            objTerminateData.REMAINING_LEAVE_MONEY = objTerminate.REMAINING_LEAVE_MONEY
            objTerminateData.IDENTIFI_VIOLATE_MONEY = objTerminate.IDENTIFI_VIOLATE_MONEY
            objTerminateData.MONEY_RETURN = objTerminate.MONEY_RETURN
            objTerminateData.ORG_ID = objTerminate.ORG_ID
            objTerminateData.TITLE_ID = objTerminate.TITLE_ID
            objTerminateData.TYPE_TERMINATE = objTerminate.TYPE_TERMINATE
            'objTerminateData.Expire_Date = objTerminate.EXPIRE_DATE
            objTerminateData.SALARYMEDIUM_LOSS = objTerminate.SALARYMEDIUM_LOSS
            objTerminateData.TER_REASON = objTerminate.TER_REASON
            objTerminateData.DECISION_TYPE = objTerminate.DECISION_TYPE
            objTerminateData.SUM_COLLECT_DEBT = objTerminate.SUM_COLLECT_DEBT
            objTerminateData.AMOUNT_PAYMENT_CASH = objTerminate.AMOUNT_PAYMENT_CASH
            objTerminateData.AMOUNT_DEDUCT_FROM_SAL = objTerminate.AMOUNT_DEDUCT_FROM_SAL
            objTerminateData.PERIOD_ID = objTerminate.PERIOD_ID
            objTerminateData.IS_ALLOW = objTerminate.IS_ALLOW
            objTerminateData.IS_REPLACE_POS = objTerminate.IS_REPLACE_POS
            objTerminateData.REVERSE_SENIORITY = objTerminate.REVERSE_SENIORITY
            objTerminateData.IS_RETURN_INSAL = objTerminate.IS_RETURN_INSAL
            objTerminateData.PERIOD_TEXT = objTerminate.PERIOD_TEXT
            Context.HU_TERMINATE.AddObject(objTerminateData)

            If objTerminate.lstHandoverContent IsNot Nothing Then
                For Each item In objTerminate.lstHandoverContent
                    Dim handover As New HU_TRANSFER_TERMINATE
                    handover.ID = Utilities.GetNextSequence(Context, Context.HU_TRANSFER_TERMINATE.EntitySet.Name)
                    handover.TERMINATE_ID = objTerminate.ID
                    handover.IS_FINISH = item.IS_FINISH
                    handover.CONTENT_ID = item.CONTENT_ID
                    Context.HU_TRANSFER_TERMINATE.AddObject(handover)
                Next
            End If

            Context.SaveChanges(log)
            If objTerminate.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID Then
                ApproveTerminate(objTerminate, log)
            Else
                ApproveTerminate_Customer(objTerminate, log)
            End If

            'InsertOrUpdateDebtByTerminate(objTerminate, log)
            gID = objTerminateData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Sub InsertOrUpdateDebtByTerminate(ByVal terminate As TerminateDTO, ByVal log As UserLog)
        If terminate Is Nothing Then
            Exit Sub
        End If
        Dim existedDebts = (From debt In Context.HU_DEBT
                            Where debt.EMPLOYEE_ID = terminate.EMPLOYEE_ID
                            Select debt).ToList
        For Each debt As HU_DEBT In existedDebts
            Context.HU_DEBT.DeleteObject(debt)
        Next

        If terminate.lstDebts IsNot Nothing AndAlso terminate.lstDebts.Any Then
            For Each debt As DebtDTO In terminate.lstDebts
                Dim item As New HU_DEBT With
                {
                    .ID = Utilities.GetNextSequence(Context, Context.HU_DEBT.EntitySet.Name),
                    .DEBT_TYPE_ID = debt.DEBT_TYPE_ID,
                    .DEBT_STATUS = debt.DEBT_STATUS,
                    .MONEY = debt.MONEY,
                    .EMPLOYEE_ID = terminate.EMPLOYEE_ID,
                    .REMARK = debt.REMARK,
                    .CREATED_BY = log.Username,
                    .CREATED_DATE = DateTime.Now,
                    .CREATED_LOG = log.ComputerName,
                    .MODIFIED_DATE = DateTime.Now,
                    .MODIFIED_BY = log.Username,
                    .MODIFIED_LOG = log.ComputerName
                }
                Context.HU_DEBT.AddObject(item)
            Next
        End If
        Context.SaveChanges(log)
    End Sub

    Public Sub InsertOrUpdateAssetByTerminate(ByVal terminate As TerminateDTO, ByVal log As UserLog)
        If terminate Is Nothing Then
            Exit Sub
        End If
        Dim existedAsstes = (From asset_mng In Context.HU_ASSET_MNG
                             Where asset_mng.EMPLOYEE_ID = terminate.EMPLOYEE_ID
                             Select asset_mng).ToList
        For Each asset As HU_ASSET_MNG In existedAsstes
            Context.HU_ASSET_MNG.DeleteObject(asset)
        Next

        If terminate.AssetMngs IsNot Nothing AndAlso terminate.AssetMngs.Any Then
            For Each assetMngDto As AssetMngDTO In terminate.AssetMngs
                Dim item As New HU_ASSET_MNG With
                {
                    .ID = Utilities.GetNextSequence(Context, Context.HU_ASSET_MNG.EntitySet.Name),
                    .ASSET_DECLARE_ID = assetMngDto.ASSET_DECLARE_ID,
                    .ASSET_VALUE = assetMngDto.ASSET_VALUE,
                    .ASSET_STATUS_ID = assetMngDto.STATUS_ID,
                    .STATUS_ID = assetMngDto.STATUS_ID,
                    .EMPLOYEE_ID = terminate.EMPLOYEE_ID,
                    .QUANTITY = assetMngDto.QUANTITY
                }
                Context.HU_ASSET_MNG.AddObject(item)

            Next
        End If
        Context.SaveChanges(log)
    End Sub

    Public Function ModifyTerminate(ByVal objTerminate As TerminateDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTerminateData As New HU_TERMINATE With {.ID = objTerminate.ID}
        Try
            objTerminateData = (From p In Context.HU_TERMINATE Where p.ID = objTerminate.ID).FirstOrDefault
            ' bo sung truong HSV
            objTerminateData.NOTIFY_NO = objTerminate.NOTIFY_NO
            objTerminateData.TER_DATE = objTerminate.TER_DATE
            objTerminateData.IS_BLACK_LIST = objTerminate.IS_BLACK_LIST
            objTerminateData.REASON_BLACK_LIST = objTerminate.REASON_BLACK_LIST
            objTerminateData.TRUYTHU_BHYT = objTerminate.TRUYTHU_BHYT
            objTerminateData.O_HI_COM = objTerminate.O_HI_COM
            objTerminateData.O_HI_EMP = objTerminate.O_HI_EMP
            objTerminateData.IS_JOB_LOSS_ALLOWANCE = objTerminate.IS_JOB_LOSS_ALLOWANCE
            objTerminateData.YEAR_JOB_LOSS_ALLOWANCE = objTerminate.YEAR_JOB_LOSS_ALLOWANCE
            objTerminateData.MONTH_JOB_LOSS_ALLOWANCE = objTerminate.MONTH_JOB_LOSS_ALLOWANCE
            objTerminateData.MONEY_JOB_LOSS_ALLOWANCE = objTerminate.MONEY_JOB_LOSS_ALLOWANCE
            objTerminateData.PAYPACK_UNIFORM = objTerminate.PAYPACK_UNIFORM

            ' sửa nghỉ việc
            objTerminateData.EMPLOYEE_ID = objTerminate.EMPLOYEE_ID
            objTerminateData.IS_NOHIRE = objTerminate.IS_NOHIRE
            objTerminateData.UPLOADFILE = objTerminate.UPLOADFILE
            objTerminateData.FILENAME = objTerminate.FILENAME
            objTerminateData.LAST_DATE = objTerminate.LAST_DATE
            objTerminateData.SEND_DATE = objTerminate.SEND_DATE
            objTerminateData.STATUS_ID = objTerminate.STATUS_ID
            objTerminateData.TER_REASON_DETAIL = objTerminate.TER_REASON_DETAIL
            objTerminateData.EMP_SENIORITY = objTerminate.EMPLOYEE_SENIORITY
            objTerminateData.REMARK = objTerminate.REMARK
            objTerminateData.MODIFIED_DATE = DateTime.Now
            objTerminateData.MODIFIED_BY = log.Username
            objTerminateData.MODIFIED_LOG = log.ComputerName

            objTerminateData.APPROVAL_DATE = objTerminate.APPROVAL_DATE
            objTerminateData.IDENTIFI_CARD = objTerminate.IDENTIFI_CARD
            objTerminateData.IDENTIFI_RDATE = objTerminate.IDENTIFI_RDATE
            objTerminateData.IDENTIFI_STATUS = objTerminate.IDENTIFI_STATUS
            objTerminateData.IDENTIFI_MONEY = objTerminate.IDENTIFI_MONEY
            objTerminateData.SUN_CARD = objTerminate.SUN_CARD
            objTerminateData.SUN_RDATE = objTerminate.SUN_RDATE
            objTerminateData.SUN_STATUS = objTerminate.SUN_STATUS
            objTerminateData.SUN_MONEY = objTerminate.SUN_MONEY
            objTerminateData.INSURANCE_CARD = objTerminate.INSURANCE_CARD
            objTerminateData.INSURANCE_RDATE = objTerminate.INSURANCE_RDATE
            objTerminateData.INSURANCE_STATUS = objTerminate.INSURANCE_STATUS
            objTerminateData.INSURANCE_MONEY = objTerminate.INSURANCE_MONEY
            objTerminateData.REMAINING_LEAVE = objTerminate.REMAINING_LEAVE
            objTerminateData.PAYMENT_LEAVE = objTerminate.PAYMENT_LEAVE
            objTerminateData.AMOUNT_VIOLATIONS = objTerminate.AMOUNT_VIOLATIONS
            objTerminateData.AMOUNT_WRONGFUL = objTerminate.AMOUNT_WRONGFUL
            objTerminateData.ALLOWANCE_TERMINATE = objTerminate.ALLOWANCE_TERMINATE
            objTerminateData.TRAINING_COSTS = objTerminate.TRAINING_COSTS
            objTerminateData.OTHER_COMPENSATION = objTerminate.OTHER_COMPENSATION
            objTerminateData.COMPENSATORY_LEAVE = objTerminate.COMPENSATORY_LEAVE
            objTerminateData.COMPENSATORY_PAYMENT = objTerminate.COMPENSATORY_PAYMENT
            objTerminateData.EFFECT_DATE = objTerminate.EFFECT_DATE
            objTerminateData.DECISION_NO = objTerminate.DECISION_NO
            objTerminateData.SIGN_DATE = objTerminate.SIGN_DATE
            objTerminateData.SIGN_ID = objTerminate.SIGN_ID
            objTerminateData.SIGN_NAME = objTerminate.SIGN_NAME
            objTerminateData.SIGN_TITLE = objTerminate.SIGN_TITLE

            objTerminateData.IS_RETURN_INSAL = objTerminate.IS_RETURN_INSAL
            objTerminateData.PERIOD_TEXT = objTerminate.PERIOD_TEXT
            'bỔ XUNG THÊM TRƯỜNG
            objTerminateData.SALARYMEDIUM = objTerminate.SALARYMEDIUM
            objTerminateData.YEARFORALLOW = objTerminate.YEARFORALLOW
            objTerminateData.MONEYALLOW = objTerminate.MONEYALLOW
            objTerminateData.REMAINING_LEAVE_MONEY = objTerminate.REMAINING_LEAVE_MONEY
            objTerminateData.IDENTIFI_VIOLATE_MONEY = objTerminate.IDENTIFI_VIOLATE_MONEY
            objTerminateData.MONEY_RETURN = objTerminate.MONEY_RETURN
            objTerminateData.ORG_ID = objTerminate.ORG_ID
            objTerminateData.TITLE_ID = objTerminate.TITLE_ID
            objTerminateData.TYPE_TERMINATE = objTerminate.TYPE_TERMINATE
            objTerminateData.SALARYMEDIUM_LOSS = objTerminate.SALARYMEDIUM_LOSS
            'objTerminateData.Expire_Date = objTerminate.EXPIRE_DATE
            objTerminateData.TER_REASON = objTerminate.TER_REASON
            objTerminateData.DECISION_TYPE = objTerminate.DECISION_TYPE
            objTerminateData.SUM_COLLECT_DEBT = objTerminate.SUM_COLLECT_DEBT
            objTerminateData.AMOUNT_PAYMENT_CASH = objTerminate.AMOUNT_PAYMENT_CASH
            objTerminateData.AMOUNT_DEDUCT_FROM_SAL = objTerminate.AMOUNT_DEDUCT_FROM_SAL
            objTerminateData.PERIOD_ID = objTerminate.PERIOD_ID
            objTerminateData.IS_ALLOW = objTerminate.IS_ALLOW
            objTerminateData.IS_REPLACE_POS = objTerminate.IS_REPLACE_POS
            objTerminateData.REVERSE_SENIORITY = objTerminate.REVERSE_SENIORITY
            Dim lstHandover = (From p In Context.HU_TRANSFER_TERMINATE Where p.TERMINATE_ID = objTerminate.ID).ToList
            For Each item In lstHandover
                Context.HU_TRANSFER_TERMINATE.DeleteObject(item)
            Next

            If objTerminate.lstHandoverContent IsNot Nothing Then
                For Each item In objTerminate.lstHandoverContent
                    Dim handover As New HU_TRANSFER_TERMINATE
                    handover.ID = Utilities.GetNextSequence(Context, Context.HU_TRANSFER_TERMINATE.EntitySet.Name)
                    handover.TERMINATE_ID = objTerminate.ID
                    handover.IS_FINISH = item.IS_FINISH
                    handover.CONTENT_ID = item.CONTENT_ID
                    Context.HU_TRANSFER_TERMINATE.AddObject(handover)
                Next
            End If

            Context.SaveChanges(log)
            If objTerminate.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID Then
                ApproveTerminate(objTerminate, log)
                'AutoGenInsChangeByTerminate(objTerminate.EMPLOYEE_ID,
                '                            objTerminate.ORG_ID,
                '                            objTerminate.TITLE_ID,
                '                            objTerminate.EFFECT_DATE,
                '                            0,
                '                            log)
            Else

                Dim objSe_User = (From p In Context.SE_USER Where p.EMPLOYEE_ID = objTerminate.EMPLOYEE_ID).FirstOrDefault
                If objSe_User IsNot Nothing Then
                    objSe_User.EFFECTDATE_TERMINATION = Nothing
                    objSe_User.EXPIRE_DATE = Nothing
                    Context.SaveChanges(log)
                End If
            End If

            gID = objTerminateData.ID
            'InsertOrUpdateDebtByTerminate(objTerminate, log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function Delete_Ins_Arising_While_Unapprove(ByVal empID As Decimal, ByVal effect_Date As Date, ByVal Log As UserLog) As Boolean
        Try
            Dim objInsArising As INS_ARISING
            objInsArising = (From p In Context.INS_ARISING Where p.EMPLOYEE_ID = empID And p.EFFECT_DATE = effect_Date).FirstOrDefault
            If objInsArising IsNot Nothing And InStr(RemoveSign4VietnameseString(objInsArising.REASONS.ToUpper), "GIAM DO NGHI VIEC") <> 0 Then
                Context.INS_ARISING.DeleteObject(objInsArising)
                Context.SaveChanges(Log)
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private ReadOnly VietnameseSigns As String() = New String() {"aAeEoOuUiIdDyY", "áàạảãâấầậẩẫăắằặẳẵ", "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ", "éèẹẻẽêếềệểễ", "ÉÈẸẺẼÊẾỀỆỂỄ", "óòọỏõôốồộổỗơớờợởỡ", "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ", "úùụủũưứừựửữ", "ÚÙỤỦŨƯỨỪỰỬỮ", "íìịỉĩ", "ÍÌỊỈĨ", "đ", "Đ", "ýỳỵỷỹ", "ÝỲỴỶỸ"}
    Public Function RemoveSign4VietnameseString(ByVal str As String) As String
        For i As Integer = 1 To VietnameseSigns.Length - 1

            For j As Integer = 0 To VietnameseSigns(i).Length - 1
                str = str.Replace(VietnameseSigns(i)(j), VietnameseSigns(0)(i - 1))
            Next
        Next

        Return str
    End Function
    Public Function ModifyTerminate_TV(ByVal objTerminate As TerminateDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTerminateData As New HU_TERMINATE With {.ID = objTerminate.ID}
        Try
            objTerminateData = (From p In Context.HU_TERMINATE Where p.ID = objTerminate.ID).FirstOrDefault
            objTerminateData.TRUYTHU_BHYT = objTerminate.TRUYTHU_BHYT
            objTerminateData.O_HI_EMP = objTerminate.O_HI_EMP
            objTerminateData.UNIFORM = objTerminate.UNIFORM
            objTerminateData.PAYPACK_UNIFORM = objTerminate.PAYPACK_UNIFORM
            objTerminateData.TRAINING_COSTS = objTerminate.TRAINING_COSTS
            objTerminateData.AMOUNT_VIOLATIONS = objTerminate.AMOUNT_VIOLATIONS
            objTerminateData.OTHER_COMPENSATION = objTerminate.OTHER_COMPENSATION
            objTerminateData.SALARYMEDIUM = objTerminate.SALARYMEDIUM
            objTerminateData.SALARYMEDIUM_LOSS = objTerminate.SALARYMEDIUM_LOSS
            objTerminateData.PAYMENT_LEAVE = objTerminate.PAYMENT_LEAVE
            objTerminateData.YEARFORALLOW = objTerminate.YEARFORALLOW
            objTerminateData.ALLOWANCE_TERMINATE = objTerminate.ALLOWANCE_TERMINATE
            objTerminateData.YEAR_JOB_LOSS_ALLOWANCE = objTerminate.YEAR_JOB_LOSS_ALLOWANCE
            objTerminateData.MONTH_JOB_LOSS_ALLOWANCE = objTerminate.MONTH_JOB_LOSS_ALLOWANCE
            objTerminateData.MONEY_JOB_LOSS_ALLOWANCE = objTerminate.MONEY_JOB_LOSS_ALLOWANCE
            objTerminateData.MONEY_RETURN = objTerminate.MONEY_RETURN
            objTerminateData.IS_RETURN_INSAL = objTerminate.IS_RETURN_INSAL
            objTerminateData.PERIOD_TEXT = objTerminate.PERIOD_TEXT
            objTerminateData.REMARK_QT = objTerminate.REMARK_QT
            objTerminateData.REMAINING_LEAVE = objTerminate.REMAINING_LEAVE

            Context.SaveChanges(log)

            gID = objTerminateData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function


    Public Function DeleteTerminate(ByVal objID As Decimal, log As UserLog) As Boolean
        Dim objTerminateData As HU_TERMINATE
        Dim objHuDebt As List(Of HU_DEBT)
        Try
            objTerminateData = (From p In Context.HU_TERMINATE Where objID = p.ID).FirstOrDefault
            objHuDebt = (From q In Context.HU_DEBT Where q.EMPLOYEE_ID = objTerminateData.EMPLOYEE_ID).ToList

            Dim objSe_User = (From p In Context.SE_USER Where p.EMPLOYEE_ID = objTerminateData.EMPLOYEE_ID).FirstOrDefault
            If objSe_User IsNot Nothing And objTerminateData.STATUS_ID = 447 Then
                objSe_User.EFFECTDATE_TERMINATION = Nothing
                objSe_User.EXPIRE_DATE = Nothing
            End If


            For index = 0 To objHuDebt.Count - 1
                Context.HU_DEBT.DeleteObject(objHuDebt(index))
            Next
            Context.HU_TERMINATE.DeleteObject(objTerminateData)
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function DeleteBlackList(ByVal objID As Decimal, log As UserLog) As Boolean
        Dim objTerminateData As HU_TERMINATE
        Try
            objTerminateData = (From p In Context.HU_TERMINATE Where objID = p.ID).FirstOrDefault
            objTerminateData.IS_BLACK_LIST = 0
            objTerminateData.REASON_BLACK_LIST = String.Empty
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function ApproveTerminate(ByVal objTerminate As TerminateDTO, ByVal log As UserLog) As Boolean
        Dim objEmployeeData1 As HU_EMPLOYEE
        Dim objAtWorkSign As AT_WORKSIGN
        Try
            If Not objTerminate.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID Then
                Return False
            End If

            objEmployeeData1 = (From p In Context.HU_EMPLOYEE Where objTerminate.EMPLOYEE_ID = p.ID).FirstOrDefault
            Dim Employeelist = (From p In Context.HU_EMPLOYEE Where objEmployeeData1.EMPLOYEE_CODE = p.EMPLOYEE_CODE Select p).ToList

            Dim objSe_User = (From p In Context.SE_USER Where p.EMPLOYEE_ID = objTerminate.EMPLOYEE_ID).FirstOrDefault

            If objSe_User IsNot Nothing Then
                objSe_User.EFFECTDATE_TERMINATION = objTerminate.EFFECT_DATE
                objSe_User.EXPIRE_DATE = objTerminate.EFFECT_DATE
                If objTerminate.EFFECT_DATE <= Date.Now.Date Then
                    objSe_User.ACTFLG = "I"
                End If
            End If

            For Each objEmployeeData As HU_EMPLOYEE In Employeelist
                If objTerminate.EFFECT_DATE <= DateTime.Now Then
                    objEmployeeData.WORK_STATUS = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID
                    Dim title = (From p In Context.HU_TITLE Where objEmployeeData.TITLE_ID = p.ID).FirstOrDefault
                    If title IsNot Nothing Then
                        If title.MASTER IsNot Nothing Then
                            If title.MASTER = objEmployeeData.ID Then
                                title.MASTER = Nothing
                            End If
                        End If
                        If title.INTERIM IsNot Nothing Then
                            If title.INTERIM = objEmployeeData.ID Then
                                title.INTERIM = Nothing
                            End If
                        End If
                    End If
                Else
                    objEmployeeData.WORK_STATUS = ProfileCommon.OT_WORK_STATUS.WAIT_TERMINATE_ID
                End If
                objEmployeeData.TER_EFFECT_DATE = objTerminate.EFFECT_DATE
                objEmployeeData.TER_LAST_DATE = objTerminate.LAST_DATE

                'Dim objAtWorkSignDataList = (From p In Context.AT_WORKSIGN Where p.EMPLOYEE_ID = objEmployeeData.ID And p.WORKINGDAY >= objTerminate.EFFECT_DATE Select p.ID).ToList
                'For Each objAtWorkSignData As Decimal In objAtWorkSignDataList
                '    objAtWorkSign = (From p In Context.AT_WORKSIGN Where p.ID = objAtWorkSignData).FirstOrDefault
                '    Context.AT_WORKSIGN.DeleteObject(objAtWorkSign)
                'Next
            Next

            If log IsNot Nothing Then
                Context.SaveChanges(log)
            Else
                Context.SaveChanges()
            End If

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function ApproveTerminate_Customer(ByVal objTerminate As TerminateDTO, ByVal log As UserLog) As Boolean
        Dim objEmployeeData As HU_EMPLOYEE
        Try
            If Not objTerminate.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID Then
                Return False
            End If
            objEmployeeData = (From p In Context.HU_EMPLOYEE Where objTerminate.EMPLOYEE_ID = p.ID).FirstOrDefault
            objEmployeeData.EMP_STATUS = ProfileCommon.OT_WORK_STATUS.EMP_STATUS
            If objTerminate.EFFECT_DATE <= DateTime.Now Then
                objEmployeeData.WORK_STATUS = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID
            Else
                objEmployeeData.WORK_STATUS = ProfileCommon.OT_WORK_STATUS.WAIT_TERMINATE_ID
            End If
            If log IsNot Nothing Then
                Context.SaveChanges(log)
            Else
                Context.SaveChanges()
            End If

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Private Function AutoGenInsChangeByTerminate(employeeID As Decimal,
                                                 orgID As Decimal,
                                                 titleID As Decimal,
                                                 effectDate As Date,
                                                 id As Decimal,
                                               ByVal log As UserLog) As Boolean


        Try
            '1. Check khai báo đóng mới - INS_INFOMATION
            If (From p In Context.INS_INFORMATION Where p.EMPLOYEE_ID = employeeID).Count = 0 Then
                Return False
            End If
            '2. Lấy biến động gần nhất theo ngày hiệu lực truyền vào
            Dim insChangeBefore = (From p In Context.INS_CHANGE
                                   Where p.EMPLOYEE_ID = employeeID And
                                   p.EFFECTDATE <= effectDate
                                   Order By p.EFFECTDATE Descending).FirstOrDefault

            '3. So sánh lương gần nhất với tổng lương truyền vào. lớn hơn thì biến động tăng, nhỏ hơn thì biến động giảm
            ' Đẩy data vào bảng biến động INS_CHANGE
            If insChangeBefore IsNot Nothing Then
                If insChangeBefore.NEWSALARY IsNot Nothing Then
                    Dim insChangeNew As New INS_CHANGE
                    insChangeNew.ID = Utilities.GetNextSequence(Context, Context.INS_CHANGE.EntitySet.Name)
                    insChangeNew.EMPLOYEE_ID = employeeID
                    insChangeNew.ORG_ID = orgID
                    insChangeNew.TITLE_ID = titleID
                    insChangeNew.CHANGE_TYPE = 2
                    insChangeNew.OLDSALARY = insChangeBefore.NEWSALARY
                    insChangeNew.NEWSALARY = 0
                    insChangeNew.EFFECTDATE = effectDate
                    insChangeNew.CREATED_BY = log.Username
                    insChangeNew.CREATED_DATE = DateTime.Now
                    insChangeNew.CREATED_LOG = log.Ip & "-" & log.ComputerName
                    insChangeNew.MODIFIED_BY = log.Username
                    insChangeNew.MODIFIED_DATE = DateTime.Now
                    insChangeNew.MODIFIED_LOG = log.Ip & "-" & log.ComputerName
                    If id <> 0 Then
                        insChangeNew.TER_PKEY = id
                    End If
                    If effectDate.Day >= 15 Then
                        insChangeNew.CHANGE_MONTH = effectDate.AddMonths(1)
                    Else
                        insChangeNew.CHANGE_MONTH = effectDate
                    End If
                    insChangeNew.NOTE = "HiStaff tự sinh biến động chấm dứt hợp đồng lao động từ Nghỉ việc"
                    Context.INS_CHANGE.AddObject(insChangeNew)
                    Context.SaveChanges()
                End If
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function CheckConcurrentlyExpireDate(ByVal objTerminate As TerminateDTO) As Decimal
        Try
            Dim query = (From p In Context.HU_CONCURRENTLY
                         Where p.EMPLOYEE_ID = objTerminate.EMPLOYEE_ID Select p).ToList
            If query IsNot Nothing Then
                For Each item In query
                    If item.EXPIRE_DATE_CON Is Nothing Then
                        Return 1
                    Else
                        If item.EXPIRE_DATE_CON > objTerminate.EFFECT_DATE Then
                            Return 2
                        End If
                    End If
                Next
            End If
            Return 0
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function
    Public Function CheckTerminateNo(ByVal objTerminate As TerminateDTO) As Boolean
        Try
            Dim query = (From p In Context.HU_TERMINATE
                         Where p.DECISION_NO = objTerminate.DECISION_NO And
                         p.ID <> objTerminate.ID).FirstOrDefault
            Return (query Is Nothing)
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function GetDataPrintBBBR(ByVal id As Decimal) As DataSet
        Try
            Using cls As New DataAccess.QueryData
                Dim dsData As DataSet = cls.ExecuteStore("PKG_PROFILE_BUSINESS.GET_TERMINATE_PRINT_BBBG",
                                           New With {.P_ID = id,
                                                     .P_CUR = cls.OUT_CURSOR,
                                                     .P_CUR1 = cls.OUT_CURSOR,
                                                     .P_CUR2 = cls.OUT_CURSOR}, False)

                dsData.Tables(1).TableName = "DATA"
                dsData.Tables(2).TableName = "DATA1"

                Return dsData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetDataPrintBBBR3B(ByVal id As Decimal) As DataSet
        Try
            Using cls As New DataAccess.QueryData
                Dim dsData As DataSet = cls.ExecuteStore("PKG_PROFILE_BUSINESS.GET_TERMINATE_PRINT_BBBG3B",
                                           New With {.P_ID = id,
                                                     .P_CUR = cls.OUT_CURSOR,
                                                     .P_CUR1 = cls.OUT_CURSOR,
                                                     .P_CUR2 = cls.OUT_CURSOR}, False)

                dsData.Tables(1).TableName = "DATA"
                dsData.Tables(2).TableName = "DATA1"

                Return dsData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
#End Region

#Region "Insurance"
    Public Function GetTyleNV() As DataTable
        Using cls As New DataAccess.QueryData
            Dim dtData As DataTable = cls.ExecuteStore("PKG_INSURANCE.GETTILEDONG",
                                           New With {.P_CUR = cls.OUT_CURSOR})
            Return dtData
        End Using
        Return Nothing
    End Function

    Public Function GetSalaryNew(ByRef P_EMPLOYEEID As Integer) As DataTable
        Using cls As New DataAccess.QueryData
            Dim dtData As DataTable = cls.ExecuteStore("PKG_INSURANCE.GETLUONGBIENDONG",
                                           New With {.P_EMPLOYEE_ID = P_EMPLOYEEID,
                                                     .P_CUR = cls.OUT_CURSOR})
            Return dtData
        End Using
        Return Nothing
    End Function
#End Region

#Region "Terminate3b"

    Public Function GetTerminate3b(ByVal _filter As Terminate3BDTO,
                                 ByVal PageIndex As Integer,
                                 ByVal PageSize As Integer,
                                 ByRef Total As Integer, ByVal _param As ParamDTO,
                                 Optional ByVal Sorts As String = "CREATED_DATE desc",
                                 Optional ByVal log As UserLog = Nothing) As List(Of Terminate3BDTO)

        Try

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using


            Dim query = From p In Context.HU_TERMINATE_3B
                        From e In Context.HU_EMPLOYEE.Where(Function(f) p.EMPLOYEE_ID = f.ID)
                        From cv In Context.HU_EMPLOYEE_CV.Where(Function(f) p.EMPLOYEE_ID = f.EMPLOYEE_ID)
                        From o In Context.HU_ORGANIZATION.Where(Function(f) e.ORG_ID = f.ID)
                        From t In Context.HU_TITLE.Where(Function(f) e.TITLE_ID = f.ID)
                        From status In Context.OT_OTHER_LIST.Where(Function(f) p.STATUS_ID = f.ID)
                        From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = e.ORG_ID And
                                                                       f.USERNAME = log.Username.ToUpper)

            If _filter.EMPLOYEE_CODE IsNot Nothing Then
                query = query.Where(Function(p) p.e.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If

            If _filter.EMPLOYEE_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.e.FULLNAME_VN.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If
            If _filter.EMPLOYEE_ID IsNot Nothing Then
                query = query.Where(Function(p) p.p.EMPLOYEE_ID = _filter.EMPLOYEE_ID)
            End If
            If _filter.STATUS_ID <> 0 Then
                query = query.Where(Function(p) p.p.STATUS_ID = _filter.STATUS_ID)
            End If
            If _filter.ID_NO IsNot Nothing Then
                query = query.Where(Function(p) p.cv.ID_NO.ToUpper.Contains(_filter.ID_NO.ToUpper))
            End If
            If _filter.EFFECT_FROM IsNot Nothing Then
                query = query.Where(Function(p) p.p.EFFECT_DATE >= _filter.EFFECT_FROM)
            End If
            If _filter.EFFECT_TO IsNot Nothing Then
                query = query.Where(Function(p) p.p.EFFECT_DATE <= _filter.EFFECT_FROM)
            End If
            If _filter.EFFECT_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.p.EFFECT_DATE = _filter.EFFECT_DATE)
            End If
            Dim Terminate3b = query.Select(Function(p) New Terminate3BDTO With {
                                             .ID = p.p.ID,
                                             .STATUS_NAME = p.status.NAME_VN,
                                             .STATUS_CODE = p.status.CODE,
                                             .STATUS_ID = p.status.ID,
                                             .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                             .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                             .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                                             .JOIN_DATE = p.e.JOIN_DATE,
                                             .EFFECT_DATE = p.p.EFFECT_DATE,
                                             .ORG_NAME = p.o.NAME_VN,
                                             .ORG_DESC = p.o.DESCRIPTION_PATH,
                                             .TITLE_NAME = p.t.NAME_VN,
                                            .IDENTIFI_CARD = p.p.IDENTIFI_CARD,
                                            .IDENTIFI_RDATE = p.p.IDENTIFI_RDATE,
                                            .IDENTIFI_STATUS = p.p.IDENTIFI_STATUS,
                                             .IDENTIFI_MONEY = p.p.IDENTIFI_MONEY,
                                            .SUN_CARD = p.p.SUN_CARD,
                                            .SUN_RDATE = p.p.SUN_RDATE,
                                            .SUN_STATUS = p.p.SUN_STATUS,
                                            .SUN_MONEY = p.p.SUN_MONEY,
                                            .INSURANCE_CARD = p.p.INSURANCE_CARD,
                                            .INSURANCE_RDATE = p.p.INSURANCE_RDATE,
                                            .INSURANCE_STATUS = p.p.INSURANCE_STATUS,
                                            .INSURANCE_MONEY = p.p.INSURANCE_MONEY,
                                            .REMAINING_LEAVE = p.p.REMAINING_LEAVE,
                                             .COMPENSATORY_LEAVE = p.p.COMPENSATORY_LEAVE,
                                             .IS_REMAINING_LEAVE = p.p.IS_REMAINING_LEAVE,
                                             .IS_COMPENSATORY_LEAVE = p.p.IS_COMPENSATORY_LEAVE,
                                             .CREATED_DATE = p.p.CREATED_DATE,
                                             .ID_NO = p.cv.ID_NO})

            Terminate3b = Terminate3b.OrderBy(Sorts)
            Total = Terminate3b.Count
            Terminate3b = Terminate3b.Skip(PageIndex * PageSize).Take(PageSize)
            Return Terminate3b.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try


    End Function

    Public Function GetTerminate3bByID(ByVal _filter As Terminate3BDTO) As Terminate3BDTO

        Try
            Dim query = From p In Context.HU_TERMINATE_3B
                        Join e In Context.HU_EMPLOYEE On p.EMPLOYEE_ID Equals e.ID
                        Join o In Context.HU_ORGANIZATION On e.ORG_ID Equals o.ID
                        Join t In Context.HU_TITLE On e.TITLE_ID Equals t.ID

            query = query.Where(Function(p) _filter.ID = p.p.ID)

            Dim obj = query.Select(Function(p) New Terminate3BDTO With {
                                       .ID = p.p.ID,
                                       .STATUS_ID = p.p.STATUS_ID,
                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                                       .JOIN_DATE = p.e.JOIN_DATE,
                                       .ORG_ID = p.o.ID,
                                       .ORG_NAME = p.o.NAME_VN,
                                       .ORG_DESC = p.o.NAME_VN,
                                       .TITLE_ID = p.t.ID,
                                       .TITLE_NAME = p.t.NAME_VN,
                                       .EMPLOYEE_SENIORITY = p.p.EMP_SENIORITY,
                                       .CREATED_DATE = p.p.CREATED_DATE,
                                       .IDENTIFI_CARD = p.p.IDENTIFI_CARD,
                                       .IDENTIFI_RDATE = p.p.IDENTIFI_RDATE,
                                       .IDENTIFI_STATUS = p.p.IDENTIFI_STATUS,
                                       .IDENTIFI_MONEY = p.p.IDENTIFI_MONEY,
                                       .SUN_CARD = p.p.SUN_CARD,
                                       .SUN_RDATE = p.p.SUN_RDATE,
                                       .SUN_STATUS = p.p.SUN_STATUS,
                                       .SUN_MONEY = p.p.SUN_MONEY,
                                       .INSURANCE_CARD = p.p.INSURANCE_CARD,
                                       .INSURANCE_RDATE = p.p.INSURANCE_RDATE,
                                       .INSURANCE_STATUS = p.p.INSURANCE_STATUS,
                                       .INSURANCE_MONEY = p.p.INSURANCE_MONEY,
                                       .REMAINING_LEAVE = p.p.REMAINING_LEAVE,
                                       .COMPENSATORY_LEAVE = p.p.COMPENSATORY_LEAVE,
                                       .IS_COMPENSATORY_LEAVE = p.p.IS_COMPENSATORY_LEAVE,
                                       .IS_REMAINING_LEAVE = p.p.IS_REMAINING_LEAVE,
                                       .EFFECT_DATE = p.p.EFFECT_DATE,
                                       .SIGN_ID = p.p.SIGN_ID,
                                       .SIGN_DATE = p.p.SIGN_DATE,
                                       .SIGN_NAME = p.p.SIGN_NAME,
                                       .SIGN_TITLE = p.p.SIGN_TITLE,
                                      .WORK_STATUS = p.e.WORK_STATUS})

            Return obj.FirstOrDefault
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try


    End Function

    Public Function GetTerminate3bEmployeeByID(ByVal gID As Decimal) As EmployeeDTO
        Dim obj As EmployeeDTO
        Try
            obj = (From p In Context.HU_EMPLOYEE
                   From staffrank In Context.HU_STAFF_RANK.Where(Function(f) p.STAFF_RANK_ID = f.ID).DefaultIfEmpty
                   From c In Context.HU_CONTRACT.Where(Function(f) p.CONTRACT_ID = f.ID).DefaultIfEmpty
                   From o In Context.HU_ORGANIZATION.Where(Function(f) p.ORG_ID = f.ID)
                   From t In Context.HU_TITLE.Where(Function(f) p.TITLE_ID = f.ID)
                   From working In Context.HU_WORKING.Where(Function(f) f.EMPLOYEE_ID = p.ID And f.IS_3B = True)
                   Where p.ID = gID
                   Select New EmployeeDTO With {
                       .ID = p.ID,
                       .EMPLOYEE_CODE = p.EMPLOYEE_CODE,
                       .FULLNAME_VN = p.FULLNAME_VN,
                       .ORG_ID = p.ORG_ID,
                       .ORG_NAME = o.NAME_VN,
                       .ORG_CODE = o.CODE,
                       .TITLE_ID = t.ID,
                       .TITLE_NAME_VN = t.NAME_VN,
                       .JOIN_DATE = p.JOIN_DATE,
                       .JOIN_DATE_STATE = p.JOIN_DATE_STATE,
                       .CONTRACT_NO = c.CONTRACT_NO,
                       .CONTRACT_EFFECT_DATE = c.START_DATE,
                       .CONTRACT_EXPIRE_DATE = c.EXPIRE_DATE,
                       .STAFF_RANK_ID = p.STAFF_RANK_ID,
                       .STAFF_RANK_NAME = staffrank.NAME,
                       .WORK_STATUS = p.WORK_STATUS,
                       .TER_EFFECT_DATE = p.TER_EFFECT_DATE,
                       .LAST_WORKING_ID = p.LAST_WORKING_ID,
                       .SAL_BASIC = working.SAL_BASIC,
                       .COST_SUPPORT = working.COST_SUPPORT,
                       .EFFECT_DATE = working.EFFECT_DATE}).FirstOrDefault

            Return obj

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function InsertTerminate3b(ByVal objTerminate3b As Terminate3BDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim iCount As Integer = 0
        Dim objTerminate3bData As New HU_TERMINATE_3B
        Try
            ' thêm nghỉ việc
            objTerminate3bData.ID = Utilities.GetNextSequence(Context, Context.HU_TERMINATE_3B.EntitySet.Name)
            objTerminate3bData.EMPLOYEE_ID = objTerminate3b.EMPLOYEE_ID
            objTerminate3bData.STATUS_ID = objTerminate3b.STATUS_ID
            objTerminate3bData.EMP_SENIORITY = objTerminate3b.EMPLOYEE_SENIORITY
            Dim empID = objTerminate3b.EMPLOYEE_ID
            Dim query As Decimal = (From p In Context.HU_WORKING
                                    Order By p.EFFECT_DATE Descending
                                    Where p.EMPLOYEE_ID = empID And p.STATUS_ID = 447 Select p.ID).FirstOrDefault
            If query <> 0 Then
                objTerminate3bData.LAST_WORKING_ID = query
            End If
            objTerminate3bData.IDENTIFI_CARD = objTerminate3b.IDENTIFI_CARD
            objTerminate3bData.IDENTIFI_RDATE = objTerminate3b.IDENTIFI_RDATE
            objTerminate3bData.IDENTIFI_STATUS = objTerminate3b.IDENTIFI_STATUS
            objTerminate3bData.IDENTIFI_MONEY = objTerminate3b.IDENTIFI_MONEY
            objTerminate3bData.SUN_CARD = objTerminate3b.SUN_CARD
            objTerminate3bData.SUN_RDATE = objTerminate3b.SUN_RDATE
            objTerminate3bData.SUN_STATUS = objTerminate3b.SUN_STATUS
            objTerminate3bData.SUN_MONEY = objTerminate3b.SUN_MONEY
            objTerminate3bData.INSURANCE_CARD = objTerminate3b.INSURANCE_CARD
            objTerminate3bData.INSURANCE_RDATE = objTerminate3b.INSURANCE_RDATE
            objTerminate3bData.INSURANCE_STATUS = objTerminate3b.INSURANCE_STATUS
            objTerminate3bData.INSURANCE_MONEY = objTerminate3b.INSURANCE_MONEY
            objTerminate3bData.REMAINING_LEAVE = objTerminate3b.REMAINING_LEAVE
            objTerminate3bData.COMPENSATORY_LEAVE = objTerminate3b.COMPENSATORY_LEAVE
            objTerminate3bData.IS_REMAINING_LEAVE = objTerminate3b.IS_REMAINING_LEAVE
            objTerminate3bData.IS_COMPENSATORY_LEAVE = objTerminate3b.IS_COMPENSATORY_LEAVE

            objTerminate3bData.EFFECT_DATE = objTerminate3b.EFFECT_DATE
            objTerminate3bData.LAST_DATE = objTerminate3b.EFFECT_DATE.Value.AddDays(-1)
            objTerminate3bData.SIGN_DATE = objTerminate3b.SIGN_DATE
            objTerminate3bData.SIGN_ID = objTerminate3b.SIGN_ID
            objTerminate3bData.SIGN_NAME = objTerminate3b.SIGN_NAME
            objTerminate3bData.SIGN_TITLE = objTerminate3b.SIGN_TITLE
            Context.HU_TERMINATE_3B.AddObject(objTerminate3bData)
            Context.SaveChanges(log)
            If objTerminate3b.STATUS_ID = 262 Then
                ApproveTerminate3b(objTerminate3b)
                AutoGenInsChangeByTerminate(objTerminate3b.EMPLOYEE_ID,
                                            objTerminate3b.ORG_ID,
                                            objTerminate3b.TITLE_ID,
                                            objTerminate3b.EFFECT_DATE,
                                            objTerminate3bData.ID,
                                            log)
            End If

            gID = objTerminate3bData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function CheckExistApproveTerminate3b(ByVal gID As Decimal) As Boolean
        Try
            Dim idTerminate3b = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID
            Dim objEmployeeData = (From p In Context.HU_EMPLOYEE Where gID = p.ID).FirstOrDefault
            If idTerminate3b = objEmployeeData.WORK_STATUS Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function ModifyTerminate3b(ByVal objTerminate3b As Terminate3BDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTerminate3bData As New HU_TERMINATE_3B With {.ID = objTerminate3b.ID}
        Try
            objTerminate3bData = (From p In Context.HU_TERMINATE_3B Where p.ID = objTerminate3b.ID).FirstOrDefault
            ' sửa nghỉ việc
            objTerminate3bData.EMPLOYEE_ID = objTerminate3b.EMPLOYEE_ID
            objTerminate3bData.STATUS_ID = objTerminate3b.STATUS_ID
            objTerminate3bData.EMP_SENIORITY = objTerminate3b.EMPLOYEE_SENIORITY

            objTerminate3bData.IDENTIFI_CARD = objTerminate3b.IDENTIFI_CARD
            objTerminate3bData.IDENTIFI_RDATE = objTerminate3b.IDENTIFI_RDATE
            objTerminate3bData.IDENTIFI_STATUS = objTerminate3b.IDENTIFI_STATUS
            objTerminate3bData.IDENTIFI_MONEY = objTerminate3b.IDENTIFI_MONEY
            objTerminate3bData.SUN_CARD = objTerminate3b.SUN_CARD
            objTerminate3bData.SUN_RDATE = objTerminate3b.SUN_RDATE
            objTerminate3bData.SUN_STATUS = objTerminate3b.SUN_STATUS
            objTerminate3bData.SUN_MONEY = objTerminate3b.SUN_MONEY
            objTerminate3bData.INSURANCE_CARD = objTerminate3b.INSURANCE_CARD
            objTerminate3bData.INSURANCE_RDATE = objTerminate3b.INSURANCE_RDATE
            objTerminate3bData.INSURANCE_STATUS = objTerminate3b.INSURANCE_STATUS
            objTerminate3bData.INSURANCE_MONEY = objTerminate3b.INSURANCE_MONEY
            objTerminate3bData.REMAINING_LEAVE = objTerminate3b.REMAINING_LEAVE
            objTerminate3bData.COMPENSATORY_LEAVE = objTerminate3b.COMPENSATORY_LEAVE
            objTerminate3bData.IS_REMAINING_LEAVE = objTerminate3b.IS_REMAINING_LEAVE
            objTerminate3bData.IS_COMPENSATORY_LEAVE = objTerminate3b.IS_COMPENSATORY_LEAVE
            objTerminate3bData.EFFECT_DATE = objTerminate3b.EFFECT_DATE
            objTerminate3bData.LAST_DATE = objTerminate3b.EFFECT_DATE.Value.AddDays(-1)
            objTerminate3bData.SIGN_DATE = objTerminate3b.SIGN_DATE
            objTerminate3bData.SIGN_ID = objTerminate3b.SIGN_ID
            objTerminate3bData.SIGN_NAME = objTerminate3b.SIGN_NAME
            objTerminate3bData.SIGN_TITLE = objTerminate3b.SIGN_TITLE
            Context.SaveChanges(log)
            If objTerminate3b.STATUS_ID = 262 Then
                ApproveTerminate3b(objTerminate3b)
                AutoGenInsChangeByTerminate(objTerminate3b.EMPLOYEE_ID,
                                            objTerminate3b.ORG_ID,
                                            objTerminate3b.TITLE_ID,
                                            objTerminate3b.EFFECT_DATE,
                                            objTerminate3b.ID,
                                            log)
            End If

            gID = objTerminate3bData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function DeleteTerminate3b(ByVal objID As Decimal) As Boolean
        Dim objTerminate3bData As HU_TERMINATE_3B
        Try
            objTerminate3bData = (From p In Context.HU_TERMINATE_3B Where objID = p.ID).FirstOrDefault
            Context.HU_TERMINATE_3B.DeleteObject(objTerminate3bData)
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function ApproveTerminate3b(ByVal objTerminate3b As Terminate3BDTO) As Boolean
        Dim objEmployeeData As HU_EMPLOYEE
        Try
            objEmployeeData = (From p In Context.HU_EMPLOYEE Where objTerminate3b.EMPLOYEE_ID = p.ID).SingleOrDefault

            objEmployeeData.WORK_STATUS = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID
            objEmployeeData.TER_EFFECT_DATE = objTerminate3b.EFFECT_DATE.Value.AddDays(-1)
            objEmployeeData.TER_LAST_DATE = objTerminate3b.EFFECT_DATE.Value.AddDays(-1)

            If objTerminate3b.IS_COMPENSATORY_LEAVE Then
                Dim objEmp3b = (From p In Context.HU_EMPLOYEE Where p.EMPLOYEE_3B_ID = objEmployeeData.ID).FirstOrDefault
                If objEmp3b IsNot Nothing Then
                    Dim obj As New AT_DECLARE_ENTITLEMENT
                    obj.ID = Utilities.GetNextSequence(Context, Context.AT_DECLARE_ENTITLEMENT.EntitySet.Name)
                    obj.EMPLOYEE_ID = objEmp3b.ID
                    obj.ADJUST_NB = objTerminate3b.COMPENSATORY_LEAVE
                    obj.REMARK_NB = "Hệ thống chuyển nghỉ bù khi điều chuyển 3 bên"
                    obj.START_MONTH_NB = objTerminate3b.EFFECT_DATE.Value.Month
                    obj.YEAR_NB = objTerminate3b.EFFECT_DATE.Value.Year
                    obj.YEAR_ENTITLEMENT = objTerminate3b.EFFECT_DATE.Value.Year
                    obj.YEAR = objTerminate3b.EFFECT_DATE.Value.Year
                    obj.CREATED_BY = "ADMIN"
                    obj.CREATED_DATE = Date.Now
                    obj.CREATED_LOG = "ADMIN"
                    obj.MODIFIED_BY = "ADMIN"
                    obj.MODIFIED_DATE = Date.Now
                    obj.MODIFIED_LOG = "ADMIN"
                    Context.AT_DECLARE_ENTITLEMENT.AddObject(obj)

                End If
            End If
            If objTerminate3b.IS_REMAINING_LEAVE Then
                Dim objEmp3b = (From p In Context.HU_EMPLOYEE Where p.EMPLOYEE_3B_ID = objEmployeeData.ID).FirstOrDefault
                If objEmp3b IsNot Nothing Then
                    Dim obj As New AT_DECLARE_ENTITLEMENT
                    obj.ID = Utilities.GetNextSequence(Context, Context.AT_DECLARE_ENTITLEMENT.EntitySet.Name)
                    obj.EMPLOYEE_ID = objEmp3b.ID
                    obj.ADJUST_ENTITLEMENT = objTerminate3b.REMAINING_LEAVE
                    obj.REMARK_ENTITLEMENT = "Hệ thống chuyển nghỉ phép khi điều chuyển 3 bên"
                    obj.ADJUST_MONTH_ENTITLEMENT = objTerminate3b.EFFECT_DATE.Value.Month
                    obj.YEAR_NB = objTerminate3b.EFFECT_DATE.Value.Year
                    obj.YEAR_ENTITLEMENT = objTerminate3b.EFFECT_DATE.Value.Year
                    obj.YEAR = objTerminate3b.EFFECT_DATE.Value.Year
                    obj.CREATED_BY = "ADMIN"
                    obj.CREATED_DATE = Date.Now
                    obj.CREATED_LOG = "ADMIN"
                    obj.MODIFIED_BY = "ADMIN"
                    obj.MODIFIED_DATE = Date.Now
                    obj.MODIFIED_LOG = "ADMIN"
                    Context.AT_DECLARE_ENTITLEMENT.AddObject(obj)

                End If
            End If
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

#End Region

End Class
