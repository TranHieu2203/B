Imports System.Data.Objects
Imports System.Reflection
Imports Framework.Data
Imports Framework.Data.System.Linq.Dynamic
Imports Oracle.DataAccess.Client

Partial Public Class PayrollRepository
#Region "Calculate Salary"
    Public Function Load_Calculate_Load(ByVal OrgId As Integer, ByVal PeriodId As Integer, ByVal IsDissolve As Integer, ByVal IsLoad As Integer, ByVal log As UserLog) As Boolean
        Try
            Using Sql As New DataAccess.NonQueryData
                ' Tải dữ liệu trên bảng dữ liệu tính toán
                'Sql.ExecuteStore("PKG_PA_BUSINESS.LOAD_DATA_CALCULATE",
                '                 New With {.P_PERIOD_ID = PeriodId,
                '                           .P_ORG_ID = OrgId,
                '                           .P_ISDISSOLVE = IsDissolve,
                '                           .P_USERNAME = log.Username,
                '                           .P_ISLOAD = IsLoad})
                '' Chạy công thức tính toán trên bảng dữ liệu lương trong kỳ
                'Sql.ExecuteStore("PKG_PA_BUSINESS.CALCULATE_DATA_TEMP",
                '                 New With {.P_PERIOD_ID = PeriodId,
                '                           .P_ORG_ID = OrgId,
                '                           .P_ISDISSOLVE = IsDissolve,
                '                           .P_USERNAME = log.Username})
                '' Tải dữ liệu sang bảng lương tổng hợp
                'Sql.ExecuteStore("PKG_PA_BUSINESS.LOAD_DATA_SUM",
                '                New With {.P_PERIOD_ID = PeriodId,
                '                          .P_ORG_ID = OrgId,
                '                          .P_ISDISSOLVE = IsDissolve,
                '                          .P_USERNAME = log.Username,
                '                          .P_ISLOAD = IsLoad})
                Sql.ExecuteStore("PKG_PA_BUSINESS.LOAD_DATA",
                               New With {.P_PERIOD_ID = PeriodId,
                                         .P_ORG_ID = OrgId,
                                         .P_ISDISSOLVE = IsDissolve,
                                         .P_USERNAME = log.Username,
                                         .P_ISLOAD = IsLoad})

            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Return False
        End Try
    End Function

    Public Function Calculate_data_sum(ByVal OrgId As Integer, ByVal PeriodId As Integer, ByVal IsDissolve As Integer, ByVal IsLoad As Integer, ByVal log As UserLog) As Boolean
        Try
            Using Sql As New DataAccess.NonQueryData
                Sql.ExecuteStore("PKG_PA_BUSINESS.CALCULATE_DATA_SUM",
                                 New With {.P_PERIOD_ID = PeriodId,
                                           .P_ORG_ID = OrgId,
                                           .P_ISDISSOLVE = IsDissolve,
                                           .P_USERNAME = log.Username})
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Return False
        End Try
    End Function
    Public Function Load_data_sum(ByVal OrgId As Integer, ByVal PeriodId As Integer, ByVal IsDissolve As Integer, ByVal IsLoad As Integer, ByVal log As UserLog) As Boolean
        Try
            Using Sql As New DataAccess.NonQueryData
                Sql.ExecuteStore("PKG_PA_BUSINESS.LOAD_DATA_SUM",
                                 New With {.P_PERIOD_ID = PeriodId,
                                           .P_ORG_ID = OrgId,
                                           .P_ISDISSOLVE = IsDissolve,
                                           .P_USERNAME = log.Username,
                                           .P_ISLOAD = IsLoad})
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Return False
        End Try
    End Function
    Public Function Calculate_data_temp(ByVal OrgId As Integer, ByVal PeriodId As Integer, ByVal IsDissolve As Integer, ByVal IsLoad As Integer, ByVal log As UserLog) As Boolean
        Try
            Using Sql As New DataAccess.NonQueryData
                Sql.ExecuteStore("PKG_PA_BUSINESS.CALCULATE_DATA_TEMP",
                                 New With {.P_PERIOD_ID = PeriodId,
                                           .P_ORG_ID = OrgId,
                                           .P_ISDISSOLVE = IsDissolve,
                                           .P_USERNAME = log.Username})
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Return False
        End Try
    End Function
    Public Function Load_data_calculate(ByVal OrgId As Integer, ByVal PeriodId As Integer, ByVal IsDissolve As Integer, ByVal IsLoad As Integer, ByVal log As UserLog) As Boolean
        Try
            Using Sql As New DataAccess.NonQueryData
                Sql.ExecuteStore("PKG_PA_BUSINESS.LOAD_DATA_CALCULATE",
                                 New With {.P_PERIOD_ID = PeriodId,
                                           .P_ORG_ID = OrgId,
                                           .P_ISDISSOLVE = IsDissolve,
                                           .P_USERNAME = log.Username,
                                           .P_ISLOAD = IsLoad})
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Return False
        End Try
    End Function
    Public Function GetLitsCalculate(ByVal OrgId As Integer, ByVal PeriodId As Integer, ByVal IsDissolve As Integer, ByVal IsLoad As Integer,
                                     ByVal log As UserLog, Optional ByVal Sorts As String = "CREATED_DATE DESC") As DataSet

        Try
            Using cls As New DataAccess.QueryData
                'cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                '                 New With {.P_USERNAME = log.Username,
                '                           .P_ORGID = OrgId,
                '                           .P_ISDISSOLVE = IsDissolve})

                Dim dtData As DataSet = cls.ExecuteStore("PKG_PA_BUSINESS.GET_LIST_CALCULATE",
                                           New With {.P_ORGID = OrgId,
                                                     .P_ISDISSOLVE = IsDissolve,
                                                     .P_USERNAME = log.Username,
                                                     .P_PERIOD_ID = PeriodId,
                                                     .P_SORT = Sorts,
                                                     .IS_LOAD = IsLoad,
                                                     .P_CUR = cls.OUT_CURSOR,
                                                     .P_CUR1 = cls.OUT_CURSOR}, False)
                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
    Public Function LoadCalculate(ByVal PeriodId As Integer, ByVal OrgId As Integer, ByVal IsDissolve As Integer, ByVal listEmployee As List(Of String),
                                     ByVal log As UserLog, Optional ByVal Sorts As String = "CREATED_DATE DESC") As DataSet

        Try

            Using cls As New DataAccess.NonQueryData
                cls.ExecuteSQL("DELETE SE_CHOSEN_CALCULATE")
                If listEmployee.Count <= 0 Then
                    cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_CALCULATE",
                                     New With {.P_USERNAME = log.Username,
                                               .P_ORGID = OrgId,
                                               .P_ISDISSOLVE = IsDissolve})
                Else
                    For Each emp As String In listEmployee
                        Dim objNew As New SE_CHOSEN_CALCULATE
                        objNew.EMPLOYEEID = Utilities.Obj2Decima(emp)
                        objNew.USERNAME = log.Username
                        Context.SE_CHOSEN_CALCULATE.AddObject(objNew)
                    Next
                    Context.SaveChanges()
                End If
            End Using
            Using cls As New DataAccess.QueryData
                Dim dtData As DataSet = cls.ExecuteStore("PKG_PA_BUSINESS.GET_IMPORTSALARY",
                                           New With {.P_ORG_ID = OrgId,
                                                     .P_PERIOD_ID = PeriodId,
                                                     .P_USERNAME = log.Username,
                                                     .P_SORT = Sorts,
                                                     .P_CUR = cls.OUT_CURSOR})
                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
    Public Function GetListSalaryVisibleCol() As List(Of PAListSalariesDTO)
        Try
            Dim query = From s In Context.PA_LISTSALARIES
                        Where s.IS_VISIBLE = True And s.STATUS = "A"
                        Order By s.COL_INDEX
                        Select New PAListSalariesDTO With {
                                         .ID = s.ID,
                                         .TYPE_PAYMENT = s.TYPE_PAYMENT,
                                         .COL_NAME = s.COL_NAME,
                                         .NAME_EN = s.NAME_EN,
                                         .NAME_VN = s.NAME_VN,
                                         .COL_INDEX = s.COL_INDEX,
                                         .CREATED_DATE = s.CREATED_DATE,
                                         .IS_VISIBLE = s.IS_VISIBLE,
                                         .STATUS = s.STATUS}

            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function ActiveOrDeactive(ByVal PeriodId As Integer, ByVal OrgId As Integer, ByVal Active As Integer, ByVal log As UserLog) As Boolean
        Try

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Return False
        End Try
    End Function
#End Region
#Region "Import Bonus"

    Public Function GetImportBonus(ByVal Year As Integer, ByVal obj_sal_id As Integer, ByVal PeriodId As Integer, ByVal OrgId As Integer, ByVal IsDissolve As Integer, ByVal log As UserLog,
                                        Optional ByVal Sorts As String = "CREATED_DATE DESC") As DataTable

        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = OrgId,
                                           .P_ISDISSOLVE = IsDissolve})
            End Using
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PA_BUSINESS.LOAD_DATA_IMPORT_BONUS",
                                           New With {.P_USERNAME = log.Username,
                                                     .P_ORG_ID = OrgId,
                                                     .YEAR = Year,
                                                     .P_PERIOD_ID = PeriodId,
                                                     .P_OBJ_SAL_ID = obj_sal_id,
                                                     .P_CUR = cls.OUT_CURSOR})
                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
#End Region

#Region "Import Salary"

    Public Function GetImportSalary(ByVal obj_sal_id As Integer, ByVal PeriodId As Integer, ByVal OrgId As Integer, ByVal IsDissolve As Integer, ByVal EmployeeId As String, ByVal log As UserLog,
                                        Optional ByVal Sorts As String = "CREATED_DATE DESC") As DataTable

        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = OrgId,
                                           .P_ISDISSOLVE = IsDissolve})
            End Using
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PA_BUSINESS.LOAD_DATA_IMPORT",
                                           New With {.P_USERNAME = log.Username,
                                                     .P_ORG_ID = OrgId,
                                                     .P_PERIOD_ID = PeriodId,
                                                     .P_OBJ_SAL_ID = obj_sal_id,
                                                     .P_EMPLOYEE_ID = EmployeeId,
                                                     .P_CUR = cls.OUT_CURSOR})
                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function GET_DATA_SEND_MAIL(ByVal obj_sal_id As Integer, ByVal PeriodId As Integer, ByVal OrgId As Integer, ByVal IsDissolve As Integer, ByVal EmployeeId As String, ByVal log As UserLog,
                                        Optional ByVal Sorts As String = "CREATED_DATE DESC") As DataTable

        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PA_BUSINESS.GET_DATA_SEND_MAIL",
                                           New With {.P_USERNAME = log.Username,
                                                     .P_ORG_ID = OrgId,
                                                     .P_PERIOD_ID = PeriodId,
                                                     .P_ISDISSOLVE = IsDissolve,
                                                     .P_OBJ_SAL_ID = obj_sal_id,
                                                     .P_EMPLOYEE_ID = EmployeeId,
                                                     .P_CUR = cls.OUT_CURSOR})
                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function GetMappingSalary(ByVal obj_sal_id As Integer, ByVal PeriodId As Integer) As DataTable

        Try

            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PA_BUSINESS.LOAD_DATA_MAPPING",
                                           New With {.P_OBJ_SAL_ID = obj_sal_id,
                                                     .P_PERIOD_ID = PeriodId,
                                                     .P_CUR = cls.OUT_CURSOR})
                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
    Public Function GetMappingSalaryImport(ByVal obj_sal_id As Integer, ByVal PeriodId As Integer) As DataTable

        Try

            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PA_BUSINESS.LOAD_DATA_MAPPING_IMPORT",
                                           New With {.P_OBJ_SAL_ID = obj_sal_id,
                                                     .P_PERIOD_ID = PeriodId,
                                                     .P_CUR = cls.OUT_CURSOR})
                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function GetSalaryList() As List(Of PAListSalariesDTO)
        Try
            Dim query = From p In Context.PA_LISTSALARIES
            Dim lst = query.Select(Function(s) New PAListSalariesDTO With {
                                        .ID = s.ID,
                                        .TYPE_PAYMENT = s.TYPE_PAYMENT,
                                        .COL_NAME = s.COL_NAME,
                                        .NAME_EN = s.NAME_EN,
                                        .NAME_VN = s.NAME_VN,
                                        .COL_INDEX = s.COL_INDEX,
                                        .CREATED_DATE = s.CREATED_DATE,
                                        .IS_IMPORT = s.IS_IMPORT,
                                        .OBJ_SAL_ID = s.OBJ_SAL_ID
                                    }).Where(Function(f) f.IS_IMPORT = -1 And f.OBJ_SAL_ID = 1)
            lst = lst.OrderBy("COL_INDEX ASC")
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
    Public Function GetSalaryList_TYPE(ByVal POBJ_SAL_ID As Decimal) As List(Of PAListSalariesDTO)
        Try
            Dim query = From p In Context.PA_LISTSALARIES
                        Where ((POBJ_SAL_ID = 2 And p.OBJ_SAL_ID = 1) Or p.OBJ_SAL_ID = POBJ_SAL_ID) And p.IS_IMPORT = -1
            Dim lst = query.Select(Function(s) New PAListSalariesDTO With {
                                        .ID = s.ID,
                                        .COL_NAME = s.COL_NAME,
                                        .NAME_EN = s.NAME_EN,
                                        .NAME_VN = s.COL_NAME & " : " & s.NAME_VN,
                                        .COL_INDEX = s.COL_INDEX,
                                        .CREATED_DATE = s.CREATED_DATE,
                                        .IS_IMPORT = s.IS_IMPORT,
                                        .OBJ_SAL_ID = s.OBJ_SAL_ID
                                    })
            lst = lst.OrderBy("COL_INDEX ASC")
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function SaveImport(ByVal SalaryGroup As Decimal, ByVal Period As Decimal, ByVal dtData As DataTable, ByVal lstColVal As List(Of String), ByVal log As UserLog, ByRef RecordSussces As Integer) As Boolean
        Try
            Using conMng As New DataAccess.ConnectionManager
                Using conn As New OracleConnection(conMng.GetConnectionString())
                    Dim cmd As New OracleCommand
                    Dim sqlInsert = lstColVal.Aggregate(Function(cur, [next]) cur & "," & [next])
                    Dim sqlInsert_Temp As String
                    conn.Open()
                    cmd.Connection = conn
                    cmd.Transaction = cmd.Connection.BeginTransaction()
                    RecordSussces = 0
                    For Each dr As DataRow In dtData.Rows
                        If dr("ID").ToString() Is DBNull.Value OrElse dr("ID").ToString() = "" Then
                            Continue For
                        End If
                        sqlInsert_Temp = "," & sqlInsert & ","
                        Dim sqlInsertVal = ""
                        For Each parm As String In lstColVal
                            If Not dr(parm).ToString Is DBNull.Value AndAlso dr(parm).ToString <> "" Then
                                If Not Integer.TryParse(dr(parm).ToString(), 1) Then
                                    Dim res = Replace(dr(parm).ToString, ".", "") '==
                                    res = Replace(res, ",", ".") '==
                                    sqlInsertVal &= "'" & res & "'," '??
                                Else
                                    sqlInsertVal &= dr(parm).ToString & ","
                                End If
                            Else
                                sqlInsert_Temp = sqlInsert_Temp.Replace("," & parm & ",", ",")
                            End If
                        Next
                        If sqlInsertVal <> "" Then
                            sqlInsertVal = sqlInsertVal.Remove(sqlInsertVal.Length - 1, 1)
                        End If
                        If sqlInsert_Temp = "," Then
                            Continue For
                        End If
                        If sqlInsert_Temp <> "" Then
                            sqlInsert_Temp = sqlInsert_Temp.Remove(0, 1)
                            sqlInsert_Temp = sqlInsert_Temp.Remove(sqlInsert_Temp.Length - 1, 1)
                        End If
                        cmd.CommandText = "PKG_PA_BUSINESS.IMPORT_SALARY"
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.Clear()
                        cmd.Parameters.Add("P_SALARY_GROUP_ID", SalaryGroup).Value = SalaryGroup
                        cmd.Parameters.Add("P_PERIOD_SALARY_ID", Period).Value = Period
                        cmd.Parameters.Add("P_EMPLOYEE_ID", dr("ID"))
                        cmd.Parameters.Add("P_CREATED_USER", log.Username)
                        cmd.Parameters.Add("P_CREATED_LOG", log.Ip)
                        cmd.Parameters.Add("P_LISTCOL", sqlInsert_Temp)
                        cmd.Parameters.Add("P_LISTVAL", sqlInsertVal)

                        Dim r As Integer = 0
                        r = cmd.ExecuteNonQuery()
                        RecordSussces += 1
                    Next
                    cmd.Transaction.Commit()
                    cmd.Dispose()
                    conn.Close()
                    conn.Dispose()
                End Using
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function SaveImportBonus(ByVal Org_Id As Decimal, ByVal Year As Decimal, ByVal SalaryGroup As Decimal, ByVal Period As Decimal, ByVal dtData As DataTable, ByVal lstColVal As List(Of String), ByVal log As UserLog, ByRef RecordSussces As Integer) As Boolean
        Try
            Using conMng As New DataAccess.ConnectionManager
                Using conn As New OracleConnection(conMng.GetConnectionString())
                    Dim cmd As New OracleCommand
                    Dim sqlInsert = lstColVal.Aggregate(Function(cur, [next]) cur & "," & [next])

                    conn.Open()
                    cmd.Connection = conn
                    cmd.Transaction = cmd.Connection.BeginTransaction()
                    RecordSussces = 0
                    For Each dr As DataRow In dtData.Rows
                        If sqlInsert.Contains(dr("PAYMENTSOURCES_ID")) Then
                            cmd.CommandText = "PKG_PA_BUSINESS.IMPORT_BONUS"
                            cmd.CommandType = CommandType.StoredProcedure
                            cmd.Parameters.Clear()
                            cmd.Parameters.Add("P_ORG_ID", Org_Id).Value = Org_Id
                            cmd.Parameters.Add("P_YEAR", Year).Value = Year
                            cmd.Parameters.Add("P_SALARY_GROUP_ID", SalaryGroup).Value = SalaryGroup
                            cmd.Parameters.Add("P_PERIOD_SALARY_ID", Period).Value = Period
                            cmd.Parameters.Add("P_CREATED_USER", log.Username)
                            cmd.Parameters.Add("P_CREATED_LOG", log.Ip)
                            cmd.Parameters.Add("P_PAYMENTSOURCES_ID", dr("PAYMENTSOURCES_ID")).Value = dr("PAYMENTSOURCES_ID")
                            cmd.Parameters.Add("P_MONEY", dr("MONEY")).Value = dr("MONEY")
                            Dim r As Integer = 0
                            r = cmd.ExecuteNonQuery()
                            RecordSussces += 1
                        End If
                    Next
                    cmd.Transaction.Commit()
                    cmd.Dispose()
                    conn.Close()
                    conn.Dispose()
                End Using
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function SaveImportSalary_Fund_Mapping(ByVal Year As Decimal, ByVal SalaryGroup As Decimal, ByVal Period As Decimal, ByVal dtData As DataTable, ByVal log As UserLog, ByRef RecordSussces As Integer) As Boolean
        Try
            Using conMng As New DataAccess.ConnectionManager
                Using conn As New OracleConnection(conMng.GetConnectionString())
                    Dim cmd As New OracleCommand
                    conn.Open()
                    cmd.Connection = conn
                    cmd.Transaction = cmd.Connection.BeginTransaction()
                    RecordSussces = 0
                    For Each dr As DataRow In dtData.Rows
                        For Each dc As DataColumn In dtData.Columns
                            If (dc.ColumnName <> "COL_NAME" And dc.ColumnName <> "NAME_VN") AndAlso Not dr(dc.ColumnName) Is DBNull.Value AndAlso dr(dc.ColumnName) = "1" Then
                                cmd.CommandText = "PKG_PA_BUSINESS.IMPORT_SALARY_FUND_MAPPING"
                                cmd.CommandType = CommandType.StoredProcedure
                                cmd.Parameters.Clear()
                                cmd.Parameters.Add("P_YEAR", Year).Value = Year
                                cmd.Parameters.Add("P_PERIOD_ID", Period).Value = Period
                                cmd.Parameters.Add("P_SALARY_GROUP", SalaryGroup).Value = SalaryGroup

                                cmd.Parameters.Add("P_SALARY_NAME", dr("COL_NAME")).Value = dr("COL_NAME")
                                cmd.Parameters.Add("P_SALARY_FUND", dc.ColumnName).Value = dc.ColumnName

                                cmd.Parameters.Add("P_CREATED_BY", log.Username)
                                cmd.Parameters.Add("P_CREATED_LOG", log.Ip)
                                Dim r As Integer = 0
                                r = cmd.ExecuteNonQuery()
                                RecordSussces += 1
                            End If
                        Next
                    Next
                    cmd.Transaction.Commit()
                    cmd.Dispose()
                    conn.Close()
                    conn.Dispose()
                End Using
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
#End Region

#Region "IPORTAL - View phiếu lương"
    Public Function GetPayrollSheetSum(ByVal PeriodId As Integer, ByVal EmployeeId As String, ByVal log As UserLog,
                                        Optional ByVal Sorts As String = "CREATED_DATE DESC") As DataTable

        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PA_BUSINESS.LOAD_PAYROLL_SHEET_SUM",
                                           New With {.P_PERIOD_ID = PeriodId,
                                                     .P_EMPLOYEE = EmployeeId,
                                                     .P_SORT = Sorts,
                                                     .P_CUR = cls.OUT_CURSOR})
                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function GetPayrollSheetSumSheet(ByVal PeriodId As Integer, ByVal EmployeeId As String, ByVal log As UserLog) As DataTable

        Try
            Using cls As New DataAccess.QueryData
                'Dim dtData As DataTable = cls.ExecuteStore("PKG_PA_BUSINESS.LOAD_PAYROLL_SHEET",
                '                           New With {.P_PERIOD_ID = PeriodId,
                '                                     .P_EMPLOYEE = EmployeeId,
                '                                     .P_CUR = cls.OUT_CURSOR})
                Dim dtData As DataTable = cls.ExecuteStore("PKG_API_MOBILE.API_GET_PAYSLIP_MOBILE",
                                           New With {.P_LANGUAGE = "vi-VN",
                                                     .P_USERID = EmployeeId,
                                                     .P_PERIOD = PeriodId,
                                                     .P_CUR = cls.OUT_CURSOR,
                                                     .P_MESSAGE = cls.OUT_STRING,
                                                     .P_RESPONSESTATUS = cls.OUT_NUMBER})
                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function CHECK_OPEN_CLOSE(ByVal PeriodId As Integer, ByVal EmployeeId As String, ByVal log As UserLog) As DataTable

        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PA_BUSINESS.CHECK_OPEN_CLOSE",
                                           New With {.P_PERIOD_ID = PeriodId,
                                                     .P_EMPLOYEE = EmployeeId,
                                                     .P_CUR = cls.OUT_CURSOR})
                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function


    Public Function CheckPeriod(ByVal PeriodId As Integer, ByVal EmployeeId As Decimal) As Boolean
        Try
            'Dim emp As HU_EMPLOYEE
            'emp = (From p In Context.HU_EMPLOYEE Where p.ID = EmployeeId).FirstOrDefault

            'Dim query = (From p In Context.AT_ORG_PERIOD
            '             Where p.PERIOD_ID = PeriodId And p.ORG_ID = emp.ORG_ID).FirstOrDefault

            'If query IsNot Nothing Then
            '    Return query.STATUSCOLEX = 0
            'Else
            '    Return (query Is Nothing)
            'End If
            Dim empquery = (From p In Context.HU_EMPLOYEE Where p.ID = EmployeeId)
            Dim emp = empquery.Select(Function(p) New EmployeeDTO With {
                                       .ID = p.ID,
                                       .ORG_ID = p.ORG_ID
                                       }).FirstOrDefault
            Dim query = (From p In Context.AT_ORG_PERIOD
                         Where p.PERIOD_ID = PeriodId And p.ORG_ID = emp.ORG_ID).FirstOrDefault

            If query IsNot Nothing Then
                Return query.STATUSCOLEX = 0
            Else
                Return (query Is Nothing)
            End If
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

#End Region

#Region "Delegacy Tax"
    Public Function GetDelegacyTax(ByVal _filter As PA_Delegacy_TaxDTO, ByVal PageIndex As Integer,
                                   ByVal PageSize As Integer,
                                    ByRef Total As Integer, ByVal _param As ParamDTO,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc",
                                    Optional ByVal log As UserLog = Nothing) As List(Of PA_Delegacy_TaxDTO)

        Try

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using

            Dim enddate As New DateTime(_filter.YEAR, 12, 31)
            Dim startdate As New DateTime(_filter.YEAR, 1, 1)
            Dim query = From e In Context.HU_EMPLOYEE
                        From ecv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = e.ID).DefaultIfEmpty
                        From ge In Context.OT_OTHER_LIST.Where(Function(f) f.ID = ecv.GENDER).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From p In Context.PA_DELEGACY_TAX.Where(Function(f) f.EMPLOYEE_ID = e.ID AndAlso (f.YEAR = _filter.YEAR OrElse f.YEAR.Value = Nothing)).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) e.TITLE_ID = f.ID).DefaultIfEmpty
                        From emp_stt In Context.OT_OTHER_LIST.Where(Function(f) f.ID = e.EMP_STATUS).DefaultIfEmpty
                        From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = e.ORG_ID And
                                                                       f.USERNAME = log.Username.ToUpper)
                        Where e.IS_KIEM_NHIEM Is Nothing And e.JOIN_DATE <= enddate


            ' lọc điều kiện
            If Not _filter.IS_TER Then
                query = query.Where(Function(p) Not p.e.WORK_STATUS.HasValue OrElse
                                        (p.e.WORK_STATUS.HasValue AndAlso
                                         ((p.e.WORK_STATUS <> 257) OrElse (p.e.WORK_STATUS = 257 AndAlso
                                         p.e.TER_EFFECT_DATE >= startdate And p.e.TER_EFFECT_DATE <= enddate))))

            End If
            If _filter.EMPLOYEE_CODE <> "" Then
                query = query.Where(Function(p) p.e.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If
            If _filter.EMPLOYEE_NAME <> "" Then
                query = query.Where(Function(p) p.e.FULLNAME_VN.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If
            If _filter.ORG_NAME <> "" Then
                query = query.Where(Function(p) p.o.NAME_VN.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If
            If _filter.TITLE_NAME <> "" Then
                query = query.Where(Function(p) p.t.NAME_VN.ToUpper.Contains(_filter.TITLE_NAME.ToUpper))
            End If

            If _filter.GENDER <> "" Then
                query = query.Where(Function(p) p.ge.NAME_VN.ToUpper.Contains(_filter.GENDER.ToUpper))
            End If

            If _filter.MOBILE_PHONE <> "" Then
                query = query.Where(Function(p) p.ecv.MOBILE_PHONE.ToUpper.Contains(_filter.MOBILE_PHONE.ToUpper))
            End If

            If _filter.JOIN_DATE.HasValue Then
                query = query.Where(Function(p) p.e.JOIN_DATE = _filter.JOIN_DATE)
            End If

            If _filter.FROM_DATE.HasValue Then
                query = query.Where(Function(p) p.e.JOIN_DATE >= _filter.FROM_DATE)
            End If

            If _filter.TO_DATE.HasValue Then
                query = query.Where(Function(p) p.e.JOIN_DATE <= _filter.TO_DATE)
            End If

            If _filter.EMP_STATUS.HasValue Then
                query = query.Where(Function(p) p.e.EMP_STATUS = _filter.EMP_STATUS)
            End If
            If _filter.BIRTH_DATE.HasValue Then
                query = query.Where(Function(p) p.ecv.BIRTH_DATE = _filter.BIRTH_DATE)
            End If
            ' select thuộc tính
            Dim delegacy = query.Select(Function(p) New PA_Delegacy_TaxDTO With {
                                            .ID = p.p.ID,
                                            .EMPLOYEE_ID = p.e.ID,
                                            .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                            .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                                            .ORG_ID = p.e.ID,
                                            .ORG_NAME = p.o.NAME_VN,
                                            .ORG_DESC = p.o.DESCRIPTION_PATH,
                                            .TITLE_NAME = p.t.NAME_VN,
                                            .CREATED_DATE = p.p.CREATED_DATE,
                                            .JOIN_DATE = p.e.JOIN_DATE,
                                            .YEAR = p.p.YEAR,
                                            .DELEGACY = p.p.DELEGACY,
                                            .PIT_CODE = p.ecv.PIT_CODE,
                                            .BIRTH_DATE = p.ecv.BIRTH_DATE,
                                            .GENDER = p.ge.NAME_VN,
                                            .MOBILE_PHONE = p.ecv.MOBILE_PHONE,
                                            .EMAIL = If(p.ecv.WORK_EMAIL.Length = 0, p.ecv.PER_EMAIL, p.ecv.WORK_EMAIL),
                                            .EMP_STATUS_NAME = p.emp_stt.NAME_VN})

            If _filter.PIT_CODE <> "" Then
                delegacy = delegacy.Where(Function(p) p.PIT_CODE.ToUpper.Contains(_filter.PIT_CODE.ToUpper))
            End If
            If _filter.EMAIL <> "" Then
                delegacy = delegacy.Where(Function(p) p.EMAIL.ToUpper.Contains(_filter.EMAIL.ToUpper))
            End If
            delegacy = delegacy.OrderBy(Sorts)
            Total = delegacy.Count
            delegacy = delegacy.Skip(PageIndex * PageSize).Take(PageSize)

            Return delegacy.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try


    End Function

    Public Function ModifiDelegacy(ByVal lstObj As List(Of PA_Delegacy_TaxDTO), ByVal userLog As UserLog) As Boolean
        Try
            For Each item In lstObj
                Dim obj = (From o In Context.PA_DELEGACY_TAX Where o.EMPLOYEE_ID = item.EMPLOYEE_ID AndAlso o.YEAR = item.YEAR).FirstOrDefault
                If obj IsNot Nothing Then
                    obj.DELEGACY = item.DELEGACY
                Else
                    Dim objData As New PA_DELEGACY_TAX
                    objData.ID = Utilities.GetNextSequence(Context, Context.PA_DELEGACY_TAX.EntitySet.Name)
                    objData.YEAR = item.YEAR
                    objData.EMPLOYEE_ID = item.EMPLOYEE_ID
                    objData.DELEGACY = item.DELEGACY
                    objData.PIT_CODE = item.PIT_CODE
                    Context.PA_DELEGACY_TAX.AddObject(objData)
                End If
            Next
            Context.SaveChanges(userLog)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
#End Region
#Region "PA_ADDTAX"
    Public Function GetPA_ADDTAX(ByVal _filter As PA_ADDTAXDTO, ByVal PageIndex As Integer,
                                   ByVal PageSize As Integer,
                                    ByRef Total As Integer, ByVal _param As ParamDTO,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc",
                                    Optional ByVal log As UserLog = Nothing) As List(Of PA_ADDTAXDTO)

        Try

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using


            Dim query = From p In Context.PA_ADDTAX
                        From e In Context.HU_EMPLOYEE.Where(Function(f) p.EMPLOYEE_ID = f.ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) e.TITLE_ID = f.ID).DefaultIfEmpty
                        From income In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.INCOME_TYPE).DefaultIfEmpty
                        From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = e.ORG_ID And
                                                                       f.USERNAME = log.Username.ToUpper)


            ' lọc điều kiện
            Dim dateNow = Date.Now.Date

            If Not _filter.IS_TER Then
                query = query.Where(Function(p) Not p.e.WORK_STATUS.HasValue Or
                                        (p.e.WORK_STATUS.HasValue And
                                         ((p.e.WORK_STATUS <> 257) Or (p.e.WORK_STATUS = 257 And p.e.TER_EFFECT_DATE > dateNow))))

            End If
            If _filter.EMPLOYEE_CODE <> "" Then
                query = query.Where(Function(p) p.e.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If
            If _filter.EMPLOYEE_NAME <> "" Then
                query = query.Where(Function(p) p.e.FULLNAME_VN.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If
            If IsNumeric(_filter.YEAR) Then
                query = query.Where(Function(p) p.p.YEAR = _filter.YEAR)
            End If
            If _filter.ORG_NAME <> "" Then
                query = query.Where(Function(p) p.o.NAME_VN.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If
            If _filter.TITLE_NAME <> "" Then
                query = query.Where(Function(p) p.t.NAME_VN.ToUpper.Contains(_filter.TITLE_NAME.ToUpper))
            End If

            ' select thuộc tính
            Dim delegacy = query.Select(Function(p) New PA_ADDTAXDTO With {
                                            .ID = p.p.ID,
                                            .EMPLOYEE_ID = p.e.ID,
                                            .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                            .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                                            .ORG_ID = p.e.ID,
                                            .ORG_NAME = p.o.NAME_VN,
                                            .TITLE_NAME = p.t.NAME_VN,
                                            .CREATED_DATE = p.p.CREATED_DATE,
                                            .YEAR = p.p.YEAR,
                                            .INCOME_TYPE_NAME = p.income.NAME_VN,
                                            .TAXABLE_INCOME = p.p.TAXABLE_INCOME,
                                            .TAX_MONEY = p.p.TAX_MONEY,
                                            .NOTE = p.p.NOTE,
                                            .REST_MONEY = p.p.REST_MONEY})

            delegacy = delegacy.OrderBy(Sorts)
            Total = delegacy.Count
            delegacy = delegacy.Skip(PageIndex * PageSize).Take(PageSize)

            Return delegacy.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try


    End Function
    Public Function GetPA_ADDTAX_ByID(ByVal _id As Decimal?) As PA_ADDTAXDTO
        Try

            Dim query = From p In Context.PA_ADDTAX
                        From e In Context.HU_EMPLOYEE.Where(Function(f) p.EMPLOYEE_ID = f.ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) e.TITLE_ID = f.ID).DefaultIfEmpty
                        From income In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.INCOME_TYPE).DefaultIfEmpty
                        Where p.ID = _id

            Dim lst = query.Select(Function(p) New PA_ADDTAXDTO With {
                                            .ID = p.p.ID,
                                            .EMPLOYEE_ID = p.e.ID,
                                            .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                            .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                                            .ORG_ID = p.e.ID,
                                            .ORG_NAME = p.o.NAME_VN,
                                            .TITLE_NAME = p.t.NAME_VN,
                                            .CREATED_DATE = p.p.CREATED_DATE,
                                            .YEAR = p.p.YEAR,
                                            .INCOME_TYPE = p.p.INCOME_TYPE,
                                            .INCOME_TYPE_NAME = p.income.NAME_VN,
                                            .TAXABLE_INCOME = p.p.TAXABLE_INCOME,
                                            .TAX_MONEY = p.p.TAX_MONEY,
                                            .NOTE = p.p.NOTE,
                                            .REST_MONEY = p.p.REST_MONEY}).FirstOrDefault
            Return lst
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
    Public Function InsertPA_ADDTAX(ByVal lstObj As PA_ADDTAXDTO, ByVal userLog As UserLog, ByRef gID As Decimal) As Boolean
        Try

            Dim objData As New PA_ADDTAX
            objData.ID = Utilities.GetNextSequence(Context, Context.PA_ADDTAX.EntitySet.Name)
            objData.EMPLOYEE_ID = lstObj.EMPLOYEE_ID
            objData.YEAR = lstObj.YEAR
            objData.INCOME_TYPE = lstObj.INCOME_TYPE
            objData.TAXABLE_INCOME = lstObj.TAXABLE_INCOME
            objData.TAX_MONEY = lstObj.TAX_MONEY
            objData.NOTE = lstObj.NOTE
            objData.REST_MONEY = lstObj.REST_MONEY
            Context.PA_ADDTAX.AddObject(objData)
            Context.SaveChanges(userLog)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
    Public Function ModifyPA_ADDTAX(ByVal lstObj As PA_ADDTAXDTO,
                                   ByVal userLog As UserLog, ByRef gID As Decimal) As Boolean
        Dim objData As New PA_ADDTAX With {.ID = lstObj.ID}
        Try
            objData = (From p In Context.PA_ADDTAX Where p.ID = objData.ID).SingleOrDefault
            objData.EMPLOYEE_ID = lstObj.EMPLOYEE_ID
            objData.YEAR = lstObj.YEAR
            objData.INCOME_TYPE = lstObj.INCOME_TYPE
            objData.TAXABLE_INCOME = lstObj.TAXABLE_INCOME
            objData.TAX_MONEY = lstObj.TAX_MONEY
            objData.REST_MONEY = lstObj.REST_MONEY
            objData.NOTE = lstObj.NOTE
            Context.SaveChanges(userLog)
            gID = objData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function
    Public Function GET_IMPORT_PA_ADDTAX() As DataSet
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataSet = cls.ExecuteStore("PKG_COMMON_LIST.GET_IMPORT_PA_ADDTAX",
                                           New With {.P_CUR = cls.OUT_CURSOR}, False)
                Return dtData
            End Using
        Catch ex As Exception
            Return Nothing
        End Try

    End Function
    Public Function ValidatePA_ADDTAX(ByVal _objData As PA_ADDTAXDTO) As Boolean
        Try
            Dim query = (From p In Context.PA_ADDTAX Where p.ID <> _objData.ID AndAlso p.EMPLOYEE_ID = _objData.EMPLOYEE_ID AndAlso p.YEAR = _objData.YEAR _
                          AndAlso p.INCOME_TYPE = _objData.INCOME_TYPE).Any
            Return query
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
    Public Function GET_IMPORT_PA_STORE_DTTD() As DataSet
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataSet = cls.ExecuteStore("PKG_PA_SETTING.GET_IMPORT_PA_STORE_DTTD",
                                           New With {.P_CUR = cls.OUT_CURSOR,
                                                     .P_CUR1 = cls.OUT_CURSOR,
                                                     .P_CUR2 = cls.OUT_CURSOR}, False)
                Return dtData
            End Using
        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function CHECK_EMPLOYEE(ByVal P_EMP_CODE As String) As Integer
        Dim objEmp As HU_EMPLOYEE
        Dim result As Integer
        Try
            objEmp = (From p In Context.HU_EMPLOYEE Where p.EMPLOYEE_CODE = P_EMP_CODE.Replace(" ", "")).SingleOrDefault
            If objEmp IsNot Nothing Then
                result = objEmp.ID
            Else
                result = 0
            End If

            Return result
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
    Public Function SAVE_IMPORT_PA_ADDTAX(ByVal P_DOCXML As String, ByVal log As UserLog) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_PA_BUSINESS.IMPORT_PA_ADDTAX",
                                 New With {.P_DOCXML = P_DOCXML,
                                           .P_USERNAME = log.Username})
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
            Return False
        End Try
    End Function

    Public Function IMPORT_CH(ByVal P_DOCXML As String, ByVal log As UserLog) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_PA_BUSINESS.IMPORT_CH",
                                 New With {.P_DOCXML = P_DOCXML,
                                           .P_USERNAME = log.Username})
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
            Return False
        End Try
    End Function

    Public Function SAVE_IMPORT_PA_STORE_DTTD(ByVal P_DOCXML As String, ByVal log As UserLog) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_PA_SETTING.IMPORT_PA_STORE_DTTD",
                                 New With {.P_DOCXML = P_DOCXML,
                                           .P_USERNAME = log.Username})
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
            Return False
        End Try
    End Function
#End Region

#Region "PA Store DTTĐ"
    Public Function GetPA_STORE_DTTD(ByVal _filter As PA_STORE_DTTDDTO, ByVal PageIndex As Integer,
                                   ByVal PageSize As Integer,
                                    ByRef Total As Integer, ByVal _param As ParamDTO,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc",
                                    Optional ByVal log As UserLog = Nothing) As List(Of PA_STORE_DTTDDTO)

        Try

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using


            Dim query = From p In Context.PA_STORE_DTTD
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From b In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.BRAND_ID).DefaultIfEmpty
                        From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = p.ORG_ID And
                                                                       f.USERNAME = log.Username.ToUpper)


            If _filter.ORG_NAME <> "" Then
                query = query.Where(Function(p) p.o.NAME_VN.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If

            If _filter.TARGET_PLAN.HasValue Then
                query = query.Where(Function(p) p.p.TARGET_PLAN = _filter.TARGET_PLAN)
            End If

            If _filter.REVENUE_MIN.HasValue Then
                query = query.Where(Function(p) p.p.REVENUE_MIN = _filter.REVENUE_MIN)
            End If

            If _filter.BENEFIT_TRCA.HasValue Then
                query = query.Where(Function(p) p.p.BENEFIT_TRCA = _filter.BENEFIT_TRCA)
            End If

            If _filter.BENEFIT_QLCH.HasValue Then
                query = query.Where(Function(p) p.p.BENEFIT_QLCH = _filter.BENEFIT_QLCH)
            End If

            If _filter.NOTE <> "" Then
                query = query.Where(Function(p) p.p.NOTE.ToUpper.Contains(_filter.NOTE.ToUpper))
            End If

            If _filter.PERIOD_ID.HasValue Then
                query = query.Where(Function(p) p.p.PERIOD_ID = _filter.PERIOD_ID)
            End If

            ' select thuộc tính
            Dim delegacy = query.Select(Function(p) New PA_STORE_DTTDDTO With {
                                            .ID = p.p.ID,
                                            .ORG_ID = p.p.ORG_ID,
                                            .ORG_NAME = p.o.NAME_VN,
                                            .TARGET_ACTUAL = p.p.TARGET_ACTUAL,
                                            .TARGET_PLAN = p.p.TARGET_PLAN,
                                            .REVENUE_MIN = p.p.REVENUE_MIN,
                                            .BENEFIT_TRCA = p.p.BENEFIT_TRCA,
                                            .BENEFIT_QLCH = p.p.BENEFIT_QLCH,
                                            .PERIOD_ID = p.p.PERIOD_ID,
                                            .NOTE = p.p.NOTE,
                                            .END_DATE = Date.Now,
                                            .BRAND_ID = p.p.BRAND_ID,
                                            .BRAND_NAME = p.b.NAME_VN,
                                            .CREATED_DATE = p.p.CREATED_DATE})

            If _filter.BRAND_NAME <> "" Then
                delegacy = delegacy.Where(Function(p) p.BRAND_NAME.ToUpper.Contains(_filter.BRAND_NAME.ToUpper))
            End If

            delegacy = delegacy.OrderBy(Sorts)
            Total = delegacy.Count
            delegacy = delegacy.Skip(PageIndex * PageSize).Take(PageSize)

            Return delegacy.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function CalculateStoreDTTD(ByVal P_PERIOD As Decimal, ByVal P_ORG As Decimal, ByVal P_OBJ_EMP As Decimal, ByVal P_ENDDATE As Date, ByVal _log As UserLog) As Boolean
        Try
            Dim cls As New DataAccess.QueryData
            Dim obj_data As DataTable = cls.ExecuteStore("PKG_PA_BUSINESS.CAL_PA_STORE_DTTD",
                                 New With {.P_PERIOD_ID = P_PERIOD,
                                           .P_ORG_ID = P_ORG,
                                           .P_OBJ_EMPLOYEE = P_OBJ_EMP,
                                           .P_ENDDATE = P_ENDDATE,
                                           .P_USERNAME = _log.Username.ToUpper(),
                                           .P_OUT = cls.OUT_CURSOR})
            Return CBool(obj_data(0)(0))
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
            Return False
        End Try
    End Function
#End Region


#Region "Benefit Seniority"
    Public Function GetLstBenefitSeniority(ByVal _filter As PaBenefitSeniorityDTO, ByVal PageIndex As Integer,
                                           ByVal PageSize As Integer,
                                            ByRef Total As Integer, ByVal _param As ParamDTO,
                                            Optional ByVal Sorts As String = "CREATED_DATE desc",
                                            Optional ByVal log As UserLog = Nothing) As List(Of PaBenefitSeniorityDTO)

        Try

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using


            Dim query = From p In Context.PA_BENEFITS_SENIORITY
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) e.TITLE_ID = f.ID).DefaultIfEmpty
                        From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = e.ORG_ID And
                                                           f.USERNAME = log.Username.ToUpper)


            ' lọc điều kiện
            Dim dateNow = Date.Now.Date

            If Not _filter.IS_TER Then
                query = query.Where(Function(p) Not p.e.WORK_STATUS.HasValue Or
                                        (p.e.WORK_STATUS.HasValue And
                                         ((p.e.WORK_STATUS <> 257) Or (p.e.WORK_STATUS = 257 And p.e.TER_EFFECT_DATE > dateNow))))

            End If
            If _filter.EMPLOYEE_CODE <> "" Then
                query = query.Where(Function(p) p.e.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If
            If _filter.EMPLOYEE_NAME <> "" Then
                query = query.Where(Function(p) p.e.FULLNAME_VN.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If
            If IsNumeric(_filter.YEAR) Then
                query = query.Where(Function(p) p.p.YEAR = _filter.YEAR)
            End If
            If _filter.ORG_NAME <> "" Then
                query = query.Where(Function(p) p.o.NAME_VN.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If
            If _filter.TITLE_NAME <> "" Then
                query = query.Where(Function(p) p.t.NAME_VN.ToUpper.Contains(_filter.TITLE_NAME.ToUpper))
            End If


            If IsNumeric(_filter.BENEFIT_MONTH_01) Then
                query = query.Where(Function(p) p.p.BENEFIT_MONTH_01 = _filter.BENEFIT_MONTH_01)
            End If
            If IsNumeric(_filter.BENEFIT_MONTH_02) Then
                query = query.Where(Function(p) p.p.BENEFIT_MONTH_02 = _filter.BENEFIT_MONTH_02)
            End If
            If IsNumeric(_filter.BENEFIT_MONTH_03) Then
                query = query.Where(Function(p) p.p.BENEFIT_MONTH_03 = _filter.BENEFIT_MONTH_03)
            End If
            If IsNumeric(_filter.BENEFIT_MONTH_04) Then
                query = query.Where(Function(p) p.p.BENEFIT_MONTH_04 = _filter.BENEFIT_MONTH_04)
            End If
            If IsNumeric(_filter.BENEFIT_MONTH_05) Then
                query = query.Where(Function(p) p.p.BENEFIT_MONTH_05 = _filter.BENEFIT_MONTH_05)
            End If
            If IsNumeric(_filter.BENEFIT_MONTH_06) Then
                query = query.Where(Function(p) p.p.BENEFIT_MONTH_06 = _filter.BENEFIT_MONTH_06)
            End If
            If IsNumeric(_filter.BENEFIT_MONTH_07) Then
                query = query.Where(Function(p) p.p.BENEFIT_MONTH_07 = _filter.BENEFIT_MONTH_07)
            End If
            If IsNumeric(_filter.BENEFIT_MONTH_08) Then
                query = query.Where(Function(p) p.p.BENEFIT_MONTH_08 = _filter.BENEFIT_MONTH_08)
            End If
            If IsNumeric(_filter.BENEFIT_MONTH_09) Then
                query = query.Where(Function(p) p.p.BENEFIT_MONTH_09 = _filter.BENEFIT_MONTH_09)
            End If
            If IsNumeric(_filter.BENEFIT_MONTH_10) Then
                query = query.Where(Function(p) p.p.BENEFIT_MONTH_10 = _filter.BENEFIT_MONTH_10)
            End If
            If IsNumeric(_filter.BENEFIT_MONTH_11) Then
                query = query.Where(Function(p) p.p.BENEFIT_MONTH_11 = _filter.BENEFIT_MONTH_11)
            End If
            If IsNumeric(_filter.BENEFIT_MONTH_12) Then
                query = query.Where(Function(p) p.p.BENEFIT_MONTH_12 = _filter.BENEFIT_MONTH_12)
            End If
            If IsNumeric(_filter.BENEFIT_TOTAL) Then
                query = query.Where(Function(p) p.p.BENEFIT_TOTAL = _filter.BENEFIT_TOTAL)
            End If

            ' select thuộc tính
            Dim benefit = query.Select(Function(p) New PaBenefitSeniorityDTO With {
                                            .ID = p.p.ID,
                                            .EMPLOYEE_ID = p.e.ID,
                                            .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                            .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                                            .JOIN_DATE = p.e.JOIN_DATE,
                                            .ORG_ID = p.e.ID,
                                            .ORG_NAME = p.o.NAME_VN,
                                            .ORG_DESC = p.o.DESCRIPTION_PATH,
                                            .TITLE_NAME = p.t.NAME_VN,
                                            .CREATED_DATE = p.p.CREATED_DATE,
                                            .YEAR = p.p.YEAR,
                                            .BENEFIT_MONTH_01 = p.p.BENEFIT_MONTH_01,
                                            .BENEFIT_MONTH_02 = p.p.BENEFIT_MONTH_02,
                                            .BENEFIT_MONTH_03 = p.p.BENEFIT_MONTH_03,
                                            .BENEFIT_MONTH_04 = p.p.BENEFIT_MONTH_04,
                                            .BENEFIT_MONTH_05 = p.p.BENEFIT_MONTH_05,
                                            .BENEFIT_MONTH_06 = p.p.BENEFIT_MONTH_06,
                                            .BENEFIT_MONTH_07 = p.p.BENEFIT_MONTH_07,
                                            .BENEFIT_MONTH_08 = p.p.BENEFIT_MONTH_08,
                                            .BENEFIT_MONTH_09 = p.p.BENEFIT_MONTH_09,
                                            .BENEFIT_MONTH_10 = p.p.BENEFIT_MONTH_10,
                                            .BENEFIT_MONTH_11 = p.p.BENEFIT_MONTH_11,
                                            .BENEFIT_MONTH_12 = p.p.BENEFIT_MONTH_12,
                                            .SENIORITY_M1 = p.p.SENIORITY_M1,
                                            .SENIORITY_M2 = p.p.SENIORITY_M2,
                                            .SENIORITY_M3 = p.p.SENIORITY_M3,
                                            .SENIORITY_M4 = p.p.SENIORITY_M4,
                                            .SENIORITY_M5 = p.p.SENIORITY_M5,
                                            .SENIORITY_M6 = p.p.SENIORITY_M6,
                                            .SENIORITY_M7 = p.p.SENIORITY_M7,
                                            .SENIORITY_M8 = p.p.SENIORITY_M8,
                                            .SENIORITY_M9 = p.p.SENIORITY_M9,
                                            .SENIORITY_M10 = p.p.SENIORITY_M10,
                                            .SENIORITY_M11 = p.p.SENIORITY_M11,
                                            .SENIORITY_M12 = p.p.SENIORITY_M12,
                                            .BENEFIT_QUARTER1 = p.p.BENEFIT_QUARTER1,
                                            .BENEFIT_QUARTER2 = p.p.BENEFIT_QUARTER2,
                                            .BENEFIT_QUARTER3 = p.p.BENEFIT_QUARTER3,
                                            .BENEFIT_QUARTER4 = p.p.BENEFIT_QUARTER4,
                                            .BENEFIT_TOTAL = p.p.BENEFIT_TOTAL})

            benefit = benefit.OrderBy(Sorts)
            Total = benefit.Count
            benefit = benefit.Skip(PageIndex * PageSize).Take(PageSize)

            Return benefit.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
    Public Function Calculate_Benefit(ByVal _period_ID As Decimal, ByVal _obj_Emp_ID As Decimal, ByVal _OrgID As Decimal, ByVal _IsDissolve As Decimal, ByVal log As UserLog) As Boolean
        Try
            Using Sql As New DataAccess.NonQueryData
                Sql.ExecuteStore("PKG_PA_BUSINESS.CALCULATE_YEAR_SENIORITY",
                                 New With {.P_PERIOD_ID = _period_ID,
                                           .P_OBJ_EMPLOYEE = _obj_Emp_ID,
                                           .P_ORG_ID = _OrgID,
                                           .P_ISDISSOLVE = _IsDissolve,
                                           .P_USERNAME = log.Username})
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Return False
        End Try
    End Function
#End Region

#Region "Accounting Adjusting"
    Public Function GetAccountingAdjusting(ByVal _filter As PA_Accounting_AdjustingDTO, ByVal PageIndex As Integer,
                                           ByVal PageSize As Integer,
                                            ByRef Total As Integer, ByVal _param As ParamDTO,
                                            Optional ByVal Sorts As String = "CREATED_DATE desc",
                                            Optional ByVal log As UserLog = Nothing) As List(Of PA_Accounting_AdjustingDTO)

        Try

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using


            Dim query = From p In Context.PA_ACCOUNTING_ADJUSTING
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From o_set In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_SET_ID).DefaultIfEmpty
                        From period In Context.AT_PERIOD.Where(Function(f) f.ID = p.PERIOD_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) e.TITLE_ID = f.ID).DefaultIfEmpty
                        From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = e.ORG_ID And
                                                           f.USERNAME = log.Username.ToUpper)


            ' lọc điều kiện
            Dim dateNow = Date.Now.Date

            If Not _filter.IS_TER Then
                query = query.Where(Function(p) Not p.e.WORK_STATUS.HasValue Or
                                        (p.e.WORK_STATUS.HasValue And
                                         ((p.e.WORK_STATUS <> 257) Or (p.e.WORK_STATUS = 257 And p.e.TER_EFFECT_DATE > dateNow))))

            End If
            If _filter.EMPLOYEE_CODE <> "" Then
                query = query.Where(Function(p) p.e.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If
            If _filter.EMPLOYEE_NAME <> "" Then
                query = query.Where(Function(p) p.e.FULLNAME_VN.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If
            If _filter.ORG_NAME <> "" Then
                query = query.Where(Function(p) p.o.NAME_VN.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If
            If _filter.TITLE_NAME <> "" Then
                query = query.Where(Function(p) p.t.NAME_VN.ToUpper.Contains(_filter.TITLE_NAME.ToUpper))
            End If
            If _filter.ORG_SET_NAME <> "" Then
                query = query.Where(Function(p) p.o_set.NAME_VN.ToUpper.Contains(_filter.ORG_SET_NAME.ToUpper))
            End If
            If _filter.NOTE <> "" Then
                query = query.Where(Function(p) p.p.NOTE.ToUpper.Contains(_filter.NOTE.ToUpper))
            End If
            If IsNumeric(_filter.PERIOD_ID) Then
                query = query.Where(Function(p) p.p.PERIOD_ID = _filter.PERIOD_ID)
            End If
            If IsNumeric(_filter.ADJUSTING_X) Then
                query = query.Where(Function(p) p.p.ADJUSTING_X = _filter.ADJUSTING_X)
            End If
            If IsDate(_filter.ADJUSTING_DATE) Then
                query = query.Where(Function(p) p.p.ADJUSTING_DATE = _filter.ADJUSTING_DATE)
            End If
            If IsDate(_filter.FROM_DATE) Then
                query = query.Where(Function(p) p.p.ADJUSTING_DATE >= _filter.FROM_DATE)
            End If
            If IsDate(_filter.TO_DATE) Then
                query = query.Where(Function(p) p.p.ADJUSTING_DATE <= _filter.TO_DATE)
            End If

            ' select thuộc tính
            Dim Adjusting = query.Select(Function(p) New PA_Accounting_AdjustingDTO With {
                                            .ID = p.p.ID,
                                            .EMPLOYEE_ID = p.e.ID,
                                            .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                            .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                                            .ORG_ID = p.e.ID,
                                            .ORG_NAME = p.o.NAME_VN,
                                            .ORG_DESC = p.o.DESCRIPTION_PATH,
                                            .NOTE = p.p.NOTE,
                                            .YEAR = p.period.YEAR,
                                            .TITLE_NAME = p.t.NAME_VN,
                                            .CREATED_DATE = p.p.CREATED_DATE,
                                            .ORG_SET_ID = p.p.ORG_SET_ID,
                                            .ORG_SET_NAME = p.o_set.NAME_VN,
                                            .PERIOD_ID = p.p.PERIOD_ID,
                                            .PERIOD_NAME = p.period.PERIOD_NAME & "/" & p.period.YEAR.Value,
                                            .ADJUSTING_DATE = p.p.ADJUSTING_DATE,
                                            .ADJUSTING_X = p.p.ADJUSTING_X})
            If IsDate(_filter.PERIOD_NAME) Then
                Adjusting = Adjusting.Where(Function(p) p.PERIOD_NAME.ToUpper.Contains(_filter.PERIOD_NAME))
            End If
            Adjusting = Adjusting.OrderBy(Sorts)
            Total = Adjusting.Count
            Adjusting = Adjusting.Skip(PageIndex * PageSize).Take(PageSize)

            Return Adjusting.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function ModifyAccountingAdjust(ByVal _objData As PA_Accounting_AdjustingDTO, ByVal userLog As UserLog) As Boolean
        Try
            Dim obj = (From o In Context.PA_ACCOUNTING_ADJUSTING Where o.ID = _objData.ID).FirstOrDefault
            obj.EMPLOYEE_ID = _objData.EMPLOYEE_ID
            obj.PERIOD_ID = _objData.PERIOD_ID
            obj.ADJUSTING_DATE = _objData.ADJUSTING_DATE
            obj.ADJUSTING_X = _objData.ADJUSTING_X
            obj.ORG_SET_ID = _objData.ORG_SET_ID
            obj.NOTE = _objData.NOTE
            Context.SaveChanges(userLog)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function InsertAccountingAdjust(ByVal _objData As PA_Accounting_AdjustingDTO, ByVal userLog As UserLog) As Boolean
        Try
            Dim obj As New PA_ACCOUNTING_ADJUSTING
            obj.ID = Utilities.GetNextSequence(Context, Context.PA_ACCOUNTING_ADJUSTING.EntitySet.Name)
            obj.EMPLOYEE_ID = _objData.EMPLOYEE_ID
            obj.PERIOD_ID = _objData.PERIOD_ID
            obj.ADJUSTING_DATE = _objData.ADJUSTING_DATE
            obj.ADJUSTING_X = _objData.ADJUSTING_X
            obj.ORG_SET_ID = _objData.ORG_SET_ID
            obj.NOTE = _objData.NOTE
            Context.PA_ACCOUNTING_ADJUSTING.AddObject(obj)
            Context.SaveChanges(userLog)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function DeleteAccountingAdjust(ByVal _lstID As List(Of Decimal)) As Boolean
        Try
            Dim lstOBJ = (From p In Context.PA_ACCOUNTING_ADJUSTING Where _lstID.Contains(p.ID))
            For Each item In lstOBJ
                Context.PA_ACCOUNTING_ADJUSTING.DeleteObject(item)
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function ValidateAccountingAdjust(ByVal _objData As PA_Accounting_AdjustingDTO) As Boolean
        Try
            Dim query = (From p In Context.PA_ACCOUNTING_ADJUSTING Where p.ID <> _objData.ID AndAlso p.EMPLOYEE_ID = _objData.EMPLOYEE_ID AndAlso p.PERIOD_ID = _objData.PERIOD_ID _
                          AndAlso EntityFunctions.TruncateTime(p.ADJUSTING_DATE) = EntityFunctions.TruncateTime(_objData.ADJUSTING_DATE)).Any
            Return query
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
#End Region

#Region "Vehicle Norm"
    Public Function GetVehicleNorm(ByVal _filter As PA_Vehicle_NormDTO, ByVal PageIndex As Integer,
                                           ByVal PageSize As Integer,
                                            ByRef Total As Integer, ByVal _param As ParamDTO,
                                            Optional ByVal Sorts As String = "CREATED_DATE desc",
                                            Optional ByVal log As UserLog = Nothing) As List(Of PA_Vehicle_NormDTO)

        Try

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using


            Dim query = From p In Context.PA_VEHICLE_NORM
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From ls In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TYPE_SHOP).DefaultIfEmpty


            ' lọc điều kiện
            Dim dateNow = Date.Now.Date

            If _filter.ORG_NAME <> "" Then
                query = query.Where(Function(p) p.o.NAME_VN.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If


            If _filter.NOTE <> "" Then
                query = query.Where(Function(p) p.p.NOTE.ToUpper.Contains(_filter.NOTE.ToUpper))
            End If
            If IsDate(_filter.FROM_DATE) Then
                query = query.Where(Function(p) p.p.EFFECT_MONTH >= _filter.FROM_DATE)
            End If
            If IsDate(_filter.TO_DATE) Then
                query = query.Where(Function(p) p.p.EFFECT_MONTH <= _filter.TO_DATE)
            End If

            ' select thuộc tính
            Dim Vehicle = query.Select(Function(p) New PA_Vehicle_NormDTO With {
                                            .ID = p.p.ID,
                                            .ORG_ID = p.p.ORG_ID,
                                            .ORG_NAME = p.o.NAME_VN,
                                            .ORG_CODE = p.o.CODE,
                                            .TYPE_SHOP_ID = p.p.TYPE_SHOP,
                                            .TYPE_SHOP_NAME = p.ls.NAME_VN,
                                            .EFFECT_MONTH = p.p.EFFECT_MONTH,
                                            .MONEY_NORM = p.p.MONEY_NORM,
                                            .ORG_DESC = p.o.DESCRIPTION_PATH,
                                            .CREATED_DATE = p.p.CREATED_DATE,
                                            .NOTE = p.p.NOTE,
                                            .ORG_COST_CENTER_CODE = p.o.COST_CENTER_CODE})
            Vehicle = Vehicle.OrderBy(Sorts)
            Total = Vehicle.Count
            Vehicle = Vehicle.Skip(PageIndex * PageSize).Take(PageSize)

            Return Vehicle.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function ModifyVehicleNorm(ByVal _objData As PA_Vehicle_NormDTO, ByVal userLog As UserLog) As Boolean
        Try
            Dim obj = (From o In Context.PA_VEHICLE_NORM Where o.ID = _objData.ID).FirstOrDefault
            obj.ORG_ID = _objData.ORG_ID
            obj.TYPE_SHOP = _objData.TYPE_SHOP_ID
            obj.EFFECT_MONTH = _objData.EFFECT_MONTH
            obj.MONEY_NORM = _objData.MONEY_NORM
            obj.NOTE = _objData.NOTE
            Context.SaveChanges(userLog)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function InsertVehicleNorm(ByVal _objData As PA_Vehicle_NormDTO, ByVal userLog As UserLog) As Boolean
        Try
            Dim obj As New PA_VEHICLE_NORM
            obj.ID = Utilities.GetNextSequence(Context, Context.PA_VEHICLE_NORM.EntitySet.Name)
            obj.ORG_ID = _objData.ORG_ID
            obj.TYPE_SHOP = _objData.TYPE_SHOP_ID
            obj.EFFECT_MONTH = _objData.EFFECT_MONTH
            obj.MONEY_NORM = _objData.MONEY_NORM
            obj.NOTE = _objData.NOTE
            Context.PA_VEHICLE_NORM.AddObject(obj)
            Context.SaveChanges(userLog)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function DeleteVehicleNorm(ByVal _lstID As List(Of Decimal)) As Boolean
        Try
            Dim lstOBJ = (From p In Context.PA_VEHICLE_NORM Where _lstID.Contains(p.ID))
            For Each item In lstOBJ
                Context.PA_VEHICLE_NORM.DeleteObject(item)
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function ValidateVehicleNorm(ByVal _objData As PA_Vehicle_NormDTO) As Boolean
        Try
            Dim query = (From p In Context.PA_VEHICLE_NORM Where p.ID <> _objData.ID AndAlso p.EFFECT_MONTH = _objData.EFFECT_MONTH AndAlso p.ORG_ID = _objData.ORG_ID).Any
            Return query
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
    Public Function GET_VEHICLE_NORM_IMPORT() As DataSet
        Try
            Using cls As New DataAccess.QueryData


                Dim dtData As DataSet = cls.ExecuteStore("PKG_PA_SETTING.GET_VEHICLE_NORM_IMPORT", New With {.P_CUR = cls.OUT_CURSOR, .P_CUR1 = cls.OUT_CURSOR}, False)
                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
    'Public Function IMPORT_DATA_VEHICLE_NORM_IMPORT(ByVal P_DOCXML As String) As Boolean
    '    Try
    '        Using cls As New DataAccess.QueryData
    '            Dim ds As DataSet = cls.ExecuteStore("PKG_PA_SETTING.GET_VEHICLE_NORM_IMPORT", New With {P_DOCXML}, False)
    '            If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
    '                Dim dt = ds.Tables(0).Rows(0)("RES")
    '                If Decimal.Parse(dt) = 1 Then
    '                    Return True
    '                Else
    '                    Return False
    '                End If
    '            End If
    '        End Using
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function

    Public Function IMPORT_DATA_VEHICLE_NORM_IMPORT(ByVal dtData As DataTable, ByVal log As UserLog) As Boolean
        Try
            Using conMng As New DataAccess.ConnectionManager
                Using conn As New OracleConnection(conMng.GetConnectionString())
                    Dim cmd As New OracleCommand
                    conn.Open()
                    cmd.Connection = conn
                    cmd.Transaction = cmd.Connection.BeginTransaction()
                    'RecordSussces = 0
                    For Each dr As DataRow In dtData.Rows

                        cmd.CommandText = "PKG_PA_SETTING.IMPORT_DATA_VEHICLE_NORM_IMPORT"
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.Clear()

                        cmd.Parameters.Add("P_ORG_ID", dr("ORG_ID")).Value = dr("ORG_ID")
                        cmd.Parameters.Add("P_TYPE_SHOP", dr("TYPE_SHOP")).Value = dr("TYPE_SHOP")
                        cmd.Parameters.Add("P_EFFECT_MONTH", CDate(dr("EFFECT_MONTH"))).Value = CDate(dr("EFFECT_MONTH"))
                        cmd.Parameters.Add("P_MONEY_NORM", CDec(dr("MONEY_NORM"))).Value = CDec(dr("MONEY_NORM"))
                        cmd.Parameters.Add("P_NOTE", dr("NOTE")).Value = dr("NOTE")
                        cmd.Parameters.Add("P_CREATED_BY", log.Username)
                        cmd.Parameters.Add("P_CREATED_LOG", log.Ip)
                        Dim r As Integer = 0
                        r = cmd.ExecuteNonQuery()
                        'RecordSussces += 1
                    Next
                    cmd.Transaction.Commit()
                    cmd.Dispose()
                    conn.Close()
                    conn.Dispose()
                End Using
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
#End Region
#Region "PA_TARGET_DTTD_LABEL"
    Public Function GetPA_TARGET_DTTD_LABEL(ByVal _filter As PA_TARGET_DTTD_LABELDTO, ByVal PageIndex As Integer,
                                   ByVal PageSize As Integer,
                                    ByRef Total As Integer,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc",
                                    Optional ByVal log As UserLog = Nothing) As List(Of PA_TARGET_DTTD_LABELDTO)

        Try

            Dim query = From p In Context.PA_TARGET_DTTD_LABEL
                        From pe In Context.AT_PERIOD.Where(Function(f) f.ID = p.SALEMONTH).DefaultIfEmpty
                        From o In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.BRANDID).DefaultIfEmpty


            ' lọc điều kiện

            If _filter.YEAR IsNot Nothing Then
                query = query.Where(Function(p) p.pe.YEAR = _filter.YEAR)
            End If
            If _filter.SALEMONTH IsNot Nothing Then
                query = query.Where(Function(p) p.p.SALEMONTH = _filter.SALEMONTH)
            End If
            If _filter.BRANDID IsNot Nothing Then
                query = query.Where(Function(p) p.p.BRANDID = _filter.BRANDID)
            End If



            ' select thuộc tính
            Dim delegacy = query.Select(Function(p) New PA_TARGET_DTTD_LABELDTO With {
                                            .ID = p.p.ID,
                                            .BRANDID = p.p.ID,
                                            .SALEMONTH = p.p.SALEMONTH,
                                            .TARGETBRAND = p.p.TARGETBRAND,
                                            .TOTALREVENUE = p.p.TOTALREVENUE,
                                            .CREATED_DATE = p.p.CREATED_DATE,
                                            .MONTH = p.pe.MONTH,
                                            .YEAR = p.pe.YEAR,
                                            .BRAND_NAME = p.o.NAME_VN,
                                            .SALEMONTH_NAME = p.pe.PERIOD_NAME & "/" & p.pe.YEAR.Value,
                                            .NOTE = p.p.NOTE})

            delegacy = delegacy.OrderBy(Sorts)
            Total = delegacy.Count
            delegacy = delegacy.Skip(PageIndex * PageSize).Take(PageSize)

            Return delegacy.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
    Public Function GET_IMPORT_PA_TARGET_DTTD_LABEL() As DataSet
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataSet = cls.ExecuteStore("PKG_COMMON_LIST.GET_IMPORT_PA_TARGET_DTTD_LABEL",
                                           New With {.P_CUR = cls.OUT_CURSOR}, False)
                Return dtData
            End Using
        Catch ex As Exception
            Return Nothing
        End Try

    End Function
    Public Function IMPORT_PA_TARGET_DTTD_LABEL(ByVal dtData As DataTable, ByVal log As UserLog, ByRef RecordSussces As Integer) As Boolean
        Try
            Using conMng As New DataAccess.ConnectionManager
                Using conn As New OracleConnection(conMng.GetConnectionString())
                    Dim cmd As New OracleCommand
                    conn.Open()
                    cmd.Connection = conn
                    cmd.Transaction = cmd.Connection.BeginTransaction()
                    RecordSussces = 0
                    For Each dr As DataRow In dtData.Rows
                        cmd.CommandText = "PKG_PA_SETTING.IMPORT_PA_TARGET_DTTD_LABEL"
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.Clear()
                        cmd.Parameters.Add("P_BRANDID", dr("BRANDID")).Value = dr("BRANDID")
                        cmd.Parameters.Add("P_SALEMONTH", dr("SALEMONTH")).Value = dr("SALEMONTH")
                        cmd.Parameters.Add("P_TARGETBRAND", dr("TARGETBRAND")).Value = dr("TARGETBRAND")
                        cmd.Parameters.Add("P_TOTALREVENUE", dr("TOTALREVENUE")).Value = dr("TOTALREVENUE")
                        cmd.Parameters.Add("P_NOTE", dr("NOTE")).Value = dr("NOTE")
                        cmd.Parameters.Add("P_CREATED_BY", log.Username)
                        cmd.Parameters.Add("P_CREATED_LOG", log.Ip)
                        Dim r As Integer = 0
                        r = cmd.ExecuteNonQuery()
                        RecordSussces += 1
                    Next
                    cmd.Transaction.Commit()
                    cmd.Dispose()
                    conn.Close()
                    conn.Dispose()
                End Using
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
#End Region
#Region "Accounting Time"
    Public Function GetAccountingTime(ByVal _filter As PA_Accounting_TimeDTO, ByVal PageIndex As Integer,
                                           ByVal PageSize As Integer,
                                            ByRef Total As Integer, ByVal _param As ParamDTO,
                                            Optional ByVal Sorts As String = "CREATED_DATE desc",
                                            Optional ByVal log As UserLog = Nothing) As List(Of PA_Accounting_TimeDTO)

        Try

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using


            Dim query = From p In Context.PA_ACCOUNTING_TIME
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From o_set In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_SET_ID).DefaultIfEmpty
                        From period In Context.AT_PERIOD.Where(Function(f) f.ID = p.PERIOD_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) e.TITLE_ID = f.ID).DefaultIfEmpty
                        From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = e.ORG_ID And
                                                           f.USERNAME = log.Username.ToUpper)


            ' lọc điều kiện
            Dim dateNow = Date.Now.Date

            If Not _filter.IS_TER Then
                query = query.Where(Function(p) Not p.e.WORK_STATUS.HasValue Or
                                        (p.e.WORK_STATUS.HasValue And
                                         ((p.e.WORK_STATUS <> 257) Or (p.e.WORK_STATUS = 257 And p.e.TER_EFFECT_DATE > dateNow))))

            End If
            If _filter.EMPLOYEE_CODE <> "" Then
                query = query.Where(Function(p) p.e.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If
            If _filter.EMPLOYEE_NAME <> "" Then
                query = query.Where(Function(p) p.e.FULLNAME_VN.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If
            If _filter.ORG_NAME <> "" Then
                query = query.Where(Function(p) p.o.NAME_VN.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If
            If _filter.TITLE_NAME <> "" Then
                query = query.Where(Function(p) p.t.NAME_VN.ToUpper.Contains(_filter.TITLE_NAME.ToUpper))
            End If
            If _filter.ORG_SET_NAME <> "" Then
                query = query.Where(Function(p) p.o_set.NAME_VN.ToUpper.Contains(_filter.ORG_SET_NAME.ToUpper))
            End If

            If IsNumeric(_filter.PERIOD_ID) Then
                query = query.Where(Function(p) p.p.PERIOD_ID = _filter.PERIOD_ID)
            End If
            If IsNumeric(_filter.TIMEWORK_OLD) Then
                query = query.Where(Function(p) p.p.TIMEWORK_OLD = _filter.TIMEWORK_OLD)
            End If
            If IsNumeric(_filter.NIGHTTIME_OLD) Then
                query = query.Where(Function(p) p.p.NIGHTTIME_OLD = _filter.NIGHTTIME_OLD)
            End If
            If IsNumeric(_filter.SALARY_OLD) Then
                query = query.Where(Function(p) p.p.SALARY_OLD = _filter.SALARY_OLD)
            End If

            If IsNumeric(_filter.TIMEWORK_NEW) Then
                query = query.Where(Function(p) p.p.TIMEWORK_NEW = _filter.TIMEWORK_NEW)
            End If
            If IsNumeric(_filter.NIGHTTIME_NEW) Then
                query = query.Where(Function(p) p.p.NIGHTTIME_NEW = _filter.NIGHTTIME_NEW)
            End If
            If IsNumeric(_filter.SALARY_NEW) Then
                query = query.Where(Function(p) p.p.SALARY_NEW = _filter.SALARY_NEW)
            End If

            ' select thuộc tính
            Dim AccountingTime = query.Select(Function(p) New PA_Accounting_TimeDTO With {
                                            .ID = p.p.ID,
                                            .EMPLOYEE_ID = p.e.ID,
                                            .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                            .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                                            .ORG_ID = p.e.ID,
                                            .ORG_NAME = p.o.NAME_VN,
                                            .ORG_DESC = p.o.DESCRIPTION_PATH,
                                            .YEAR = p.period.YEAR,
                                            .TITLE_NAME = p.t.NAME_VN,
                                            .CREATED_DATE = p.p.CREATED_DATE,
                                            .ORG_SET_ID = p.p.ORG_SET_ID,
                                            .ORG_SET_NAME = p.o_set.NAME_VN,
                                            .PERIOD_ID = p.p.PERIOD_ID,
                                            .PERIOD_NAME = p.period.PERIOD_NAME & "/" & p.period.YEAR.Value,
                                            .TIMEWORK_OLD = p.p.TIMEWORK_OLD,
                                            .TIMEWORK_NEW = p.p.TIMEWORK_NEW,
                                            .NIGHTTIME_OLD = p.p.NIGHTTIME_OLD,
                                            .NIGHTTIME_NEW = p.p.NIGHTTIME_NEW,
                                            .SALARY_OLD = p.p.SALARY_OLD,
                                            .SALARY_NEW = p.p.SALARY_NEW})
            If IsDate(_filter.PERIOD_NAME) Then
                AccountingTime = AccountingTime.Where(Function(p) p.PERIOD_NAME.ToUpper.Contains(_filter.PERIOD_NAME))
            End If
            AccountingTime = AccountingTime.OrderBy(Sorts)
            Total = AccountingTime.Count
            AccountingTime = AccountingTime.Skip(PageIndex * PageSize).Take(PageSize)

            Return AccountingTime.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function DeleteAccountingTime(ByVal _lstID As List(Of Decimal)) As Boolean
        Try
            Dim lstOBJ = (From p In Context.PA_ACCOUNTING_TIME Where _lstID.Contains(p.ID))
            For Each item In lstOBJ
                Context.PA_ACCOUNTING_TIME.DeleteObject(item)
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
#End Region


#Region "Accounting Overtime"
    Public Function GetAccountingOvertime(ByVal _filter As PA_Accounting_OvertimeDTO, ByVal PageIndex As Integer,
                                           ByVal PageSize As Integer,
                                            ByRef Total As Integer, ByVal _param As ParamDTO,
                                            Optional ByVal Sorts As String = "CREATED_DATE desc",
                                            Optional ByVal log As UserLog = Nothing) As List(Of PA_Accounting_OvertimeDTO)

        Try

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using


            Dim query = From p In Context.PA_ACCOUNTING_OVERTIME
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From o_set In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_SET_ID).DefaultIfEmpty
                        From period In Context.AT_PERIOD.Where(Function(f) f.ID = p.PERIOD_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) e.TITLE_ID = f.ID).DefaultIfEmpty
                        From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = e.ORG_ID And
                                                           f.USERNAME = log.Username.ToUpper)


            ' lọc điều kiện
            Dim dateNow = Date.Now.Date

            If Not _filter.IS_TER Then
                query = query.Where(Function(p) Not p.e.WORK_STATUS.HasValue Or
                                        (p.e.WORK_STATUS.HasValue And
                                         ((p.e.WORK_STATUS <> 257) Or (p.e.WORK_STATUS = 257 And p.e.TER_EFFECT_DATE > dateNow))))

            End If
            If _filter.EMPLOYEE_CODE <> "" Then
                query = query.Where(Function(p) p.e.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If
            If _filter.EMPLOYEE_NAME <> "" Then
                query = query.Where(Function(p) p.e.FULLNAME_VN.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If
            If _filter.ORG_NAME <> "" Then
                query = query.Where(Function(p) p.o.NAME_VN.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If
            If _filter.TITLE_NAME <> "" Then
                query = query.Where(Function(p) p.t.NAME_VN.ToUpper.Contains(_filter.TITLE_NAME.ToUpper))
            End If
            If _filter.ORG_SET_NAME <> "" Then
                query = query.Where(Function(p) p.o_set.NAME_VN.ToUpper.Contains(_filter.ORG_SET_NAME.ToUpper))
            End If

            If IsNumeric(_filter.PERIOD_ID) Then
                query = query.Where(Function(p) p.p.PERIOD_ID = _filter.PERIOD_ID)
            End If
            If IsNumeric(_filter.OT_DAY_OLD) Then
                query = query.Where(Function(p) p.p.OT_DAY_OLD = _filter.OT_DAY_OLD)
            End If
            If IsNumeric(_filter.OT_NIGHT_OLD) Then
                query = query.Where(Function(p) p.p.OT_NIGHT_OLD = _filter.OT_NIGHT_OLD)
            End If
            If IsNumeric(_filter.OT_OFFDAY_OLD) Then
                query = query.Where(Function(p) p.p.OT_OFFDAY_OLD = _filter.OT_OFFDAY_OLD)
            End If
            If IsNumeric(_filter.OT_OFFNIGHT_OLD) Then
                query = query.Where(Function(p) p.p.OT_OFFNIGHT_OLD = _filter.OT_OFFNIGHT_OLD)
            End If
            If IsNumeric(_filter.OT_LEDAY_OLD) Then
                query = query.Where(Function(p) p.p.OT_LEDAY_OLD = _filter.OT_LEDAY_OLD)
            End If
            If IsNumeric(_filter.OT_LENIGHT_OLD) Then
                query = query.Where(Function(p) p.p.OT_LENIGHT_OLD = _filter.OT_LENIGHT_OLD)
            End If
            If IsNumeric(_filter.SALARY_OLD) Then
                query = query.Where(Function(p) p.p.SALARY_OLD = _filter.SALARY_OLD)
            End If

            If IsNumeric(_filter.OT_DAY_NEW) Then
                query = query.Where(Function(p) p.p.OT_DAY_NEW = _filter.OT_DAY_NEW)
            End If
            If IsNumeric(_filter.OT_NIGHT_NEW) Then
                query = query.Where(Function(p) p.p.OT_NIGHT_NEW = _filter.OT_NIGHT_NEW)
            End If
            If IsNumeric(_filter.OT_OFFDAY_NEW) Then
                query = query.Where(Function(p) p.p.OT_OFFDAY_NEW = _filter.OT_OFFDAY_NEW)
            End If
            If IsNumeric(_filter.OT_OFFNIGHT_NEW) Then
                query = query.Where(Function(p) p.p.OT_OFFNIGHT_NEW = _filter.OT_OFFNIGHT_NEW)
            End If
            If IsNumeric(_filter.OT_LEDAY_NEW) Then
                query = query.Where(Function(p) p.p.OT_LEDAY_NEW = _filter.OT_LEDAY_NEW)
            End If
            If IsNumeric(_filter.OT_LENIGHT_NEW) Then
                query = query.Where(Function(p) p.p.OT_LENIGHT_NEW = _filter.OT_LENIGHT_NEW)
            End If
            If IsNumeric(_filter.SALARY_NEW) Then
                query = query.Where(Function(p) p.p.SALARY_NEW = _filter.SALARY_NEW)
            End If

            ' select thuộc tính
            Dim AccountingTime = query.Select(Function(p) New PA_Accounting_OvertimeDTO With {
                                            .ID = p.p.ID,
                                            .EMPLOYEE_ID = p.e.ID,
                                            .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                            .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                                            .ORG_ID = p.e.ID,
                                            .ORG_NAME = p.o.NAME_VN,
                                            .ORG_DESC = p.o.DESCRIPTION_PATH,
                                            .YEAR = p.period.YEAR,
                                            .TITLE_NAME = p.t.NAME_VN,
                                            .CREATED_DATE = p.p.CREATED_DATE,
                                            .ORG_SET_ID = p.p.ORG_SET_ID,
                                            .ORG_SET_NAME = p.o_set.NAME_VN,
                                            .PERIOD_ID = p.p.PERIOD_ID,
                                            .PERIOD_NAME = p.period.PERIOD_NAME & "/" & p.period.YEAR.Value,
                                            .OT_DAY_OLD = p.p.OT_DAY_OLD,
                                            .OT_NIGHT_OLD = p.p.OT_NIGHT_OLD,
                                            .OT_OFFDAY_OLD = p.p.OT_OFFDAY_OLD,
                                            .OT_OFFNIGHT_OLD = p.p.OT_OFFNIGHT_OLD,
                                            .OT_LEDAY_OLD = p.p.OT_LEDAY_OLD,
                                            .OT_LENIGHT_OLD = p.p.OT_LENIGHT_OLD,
                                            .OT_DAY_NEW = p.p.OT_DAY_NEW,
                                            .OT_NIGHT_NEW = p.p.OT_NIGHT_NEW,
                                            .OT_OFFDAY_NEW = p.p.OT_OFFDAY_NEW,
                                            .OT_OFFNIGHT_NEW = p.p.OT_OFFNIGHT_NEW,
                                            .OT_LEDAY_NEW = p.p.OT_LEDAY_NEW,
                                            .OT_LENIGHT_NEW = p.p.OT_LENIGHT_NEW,
                                            .SALARY_OLD = p.p.SALARY_OLD,
                                            .IS_LOCK = p.p.IS_LOCK,
                                            .SALARY_NEW = p.p.SALARY_NEW})
            If IsDate(_filter.PERIOD_NAME) Then
                AccountingTime = AccountingTime.Where(Function(p) p.PERIOD_NAME.ToUpper.Contains(_filter.PERIOD_NAME))
            End If
            AccountingTime = AccountingTime.OrderBy(Sorts)
            Total = AccountingTime.Count
            AccountingTime = AccountingTime.Skip(PageIndex * PageSize).Take(PageSize)

            Return AccountingTime.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function DeleteAccountingOvertime(ByVal _lstID As List(Of Decimal)) As Boolean
        Try
            Dim lstOBJ = (From p In Context.PA_ACCOUNTING_OVERTIME Where _lstID.Contains(p.ID))
            For Each item In lstOBJ
                Context.PA_ACCOUNTING_OVERTIME.DeleteObject(item)
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function ChangeStatusAccountingOvertime(ByVal _lstID As List(Of Decimal), ByVal _status As Boolean, ByVal log As UserLog) As Boolean
        Try
            Dim lstOBJ = (From p In Context.PA_ACCOUNTING_OVERTIME Where _lstID.Contains(p.ID))
            For Each item In lstOBJ
                item.IS_LOCK = _status
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
#End Region


#Region "DTTD_DTPB"
    Public Function GetDTTD_DTPB(ByVal _filter As PA_DTTD_DTPB_EMPDTO, ByVal PageIndex As Integer,
                                           ByVal PageSize As Integer,
                                            ByRef Total As Integer, ByVal _param As ParamDTO,
                                            Optional ByVal Sorts As String = "CREATED_DATE desc",
                                            Optional ByVal log As UserLog = Nothing) As List(Of PA_DTTD_DTPB_EMPDTO)

        Try

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using


            Dim query = From p In Context.PA_DTTD_DTPB_EMP
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From store In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.STORE_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) e.TITLE_ID = f.ID).DefaultIfEmpty
                        From period In Context.AT_PERIOD.Where(Function(f) p.PERIOD_ID = f.ID).DefaultIfEmpty
                        From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = p.STORE_ID And
                                                           f.USERNAME = log.Username.ToUpper)


            ' lọc điều kiện
            Dim dateNow = Date.Now.Date

            If Not _filter.IS_TER Then
                query = query.Where(Function(p) Not p.e.WORK_STATUS.HasValue Or
                                        (p.e.WORK_STATUS.HasValue And
                                         ((p.e.WORK_STATUS <> 257) Or (p.e.WORK_STATUS = 257 And p.e.TER_EFFECT_DATE > dateNow))))

            End If
            If _filter.EMPLOYEE_CODE <> "" Then
                query = query.Where(Function(p) p.e.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If
            If _filter.EMPLOYEE_NAME <> "" Then
                query = query.Where(Function(p) p.e.FULLNAME_VN.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If

            If _filter.STORE_CODE <> "" Then
                query = query.Where(Function(p) p.store.CODE.ToUpper.Contains(_filter.STORE_CODE.ToUpper))
            End If
            If _filter.STORE_NAME <> "" Then
                query = query.Where(Function(p) p.store.NAME_VN.ToUpper.Contains(_filter.STORE_NAME.ToUpper))
            End If
            If _filter.ORG_NAME <> "" Then
                query = query.Where(Function(p) p.o.NAME_VN.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If
            If _filter.TITLE_NAME <> "" Then
                query = query.Where(Function(p) p.t.NAME_VN.ToUpper.Contains(_filter.TITLE_NAME.ToUpper))
            End If

            If IsNumeric(_filter.UPT_TD) Then
                query = query.Where(Function(p) p.p.UPT_TD = _filter.UPT_TD)
            End If
            If IsNumeric(_filter.COMPLAIN) Then
                query = query.Where(Function(p) p.p.COMPLAIN = _filter.COMPLAIN)
            End If
            If IsNumeric(_filter.PERIOD_ID) Then
                query = query.Where(Function(p) p.p.PERIOD_ID = _filter.PERIOD_ID)
            End If
            If IsNumeric(_filter.DTTD) Then
                query = query.Where(Function(p) p.p.DTTD = _filter.DTTD)
            End If
            If IsNumeric(_filter.DTTD_NG) Then
                query = query.Where(Function(p) p.p.DTTD_NG = _filter.DTTD_NG)
            End If
            If IsNumeric(_filter.DTTD_KNG1) Then
                query = query.Where(Function(p) p.p.DTTD_KNG1 = _filter.DTTD_KNG1)
            End If
            If IsNumeric(_filter.DTTD_KNG2) Then
                query = query.Where(Function(p) p.p.DTTD_KNG2 = _filter.DTTD_KNG2)
            End If

            If _filter.SALE_DATE.HasValue Then
                query = query.Where(Function(p) p.p.SALE_DATE = _filter.SALE_DATE)
            End If

            If _filter.FROM_DATE.HasValue Then
                query = query.Where(Function(p) p.p.SALE_DATE >= _filter.FROM_DATE)
            End If

            If _filter.TO_DATE.HasValue Then
                query = query.Where(Function(p) p.p.SALE_DATE <= _filter.TO_DATE)
            End If


            ' select thuộc tính
            Dim lstDTTB_DTPB = query.Select(Function(p) New PA_DTTD_DTPB_EMPDTO With {
                                            .ID = p.p.ID,
                                            .EMPLOYEE_ID = p.e.ID,
                                            .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                            .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                                            .STORE_ID = p.store.ID,
                                            .STORE_CODE = p.store.CODE,
                                            .STORE_NAME = p.store.NAME_VN,
                                            .ORG_ID = p.e.ID,
                                            .ORG_NAME = p.o.NAME_VN,
                                            .ORG_DESC = p.o.DESCRIPTION_PATH,
                                            .TITLE_NAME = p.t.NAME_VN,
                                            .CREATED_DATE = p.p.CREATED_DATE,
                                            .DTTD = p.p.DTTD,
                                            .DTTD_NG = p.p.DTTD_NG,
                                            .DTTD_KNG1 = p.p.DTTD_KNG1,
                                            .DTTD_KNG2 = p.p.DTTD_KNG2,
                                            .SALE_DATE = p.p.SALE_DATE,
                                            .PERIOD_ID = p.p.PERIOD_ID,
                                            .UPT_TD = p.p.UPT_TD,
                                            .COMPLAIN = p.p.COMPLAIN,
                                            .MONTH = p.period.MONTH,
                                            .YEAR = p.period.YEAR,
                                            .CON_TD = p.p.CON_TD,
                                            .PERIOD_NAME = p.period.PERIOD_NAME})

            If _filter.YEAR IsNot Nothing Then
                lstDTTB_DTPB = lstDTTB_DTPB.Where(Function(p) p.YEAR = _filter.YEAR)
            End If

            lstDTTB_DTPB = lstDTTB_DTPB.OrderBy(Sorts)
            Total = lstDTTB_DTPB.Count
            lstDTTB_DTPB = lstDTTB_DTPB.Skip(PageIndex * PageSize).Take(PageSize)

            Return lstDTTB_DTPB.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function DeleteDTTD_DTPB(ByVal lstID As List(Of Decimal)) As Boolean
        Try
            Dim lstOBJ = (From p In Context.PA_DTTD_DTPB_EMP Where lstID.Contains(p.ID))
            For Each item In lstOBJ
                Context.PA_DTTD_DTPB_EMP.DeleteObject(item)
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
#End Region
#Region "DTTD_ECD"
    Public Function GetDTTD_ECD(ByVal _filter As PA_DTTD_ECDDTO, ByVal PageIndex As Integer,
                                           ByVal PageSize As Integer,
                                            ByRef Total As Integer,
                                            Optional ByVal Sorts As String = "CREATED_DATE desc",
                                            Optional ByVal log As UserLog = Nothing) As List(Of PA_DTTD_ECDDTO)

        Try

            Dim query = From p In Context.PA_DTTD_ECD
                        From b In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.BRAND_ID).DefaultIfEmpty

            If _filter.BRAND_NAME <> "" Then
                query = query.Where(Function(p) p.b.NAME_VN.ToUpper.Contains(_filter.BRAND_NAME.ToUpper))
            End If

            If IsNumeric(_filter.PERIOD_ID) Then
                query = query.Where(Function(p) p.p.PERIOD_ID = _filter.PERIOD_ID)
            End If
            If IsNumeric(_filter.BRAND_ID) Then
                query = query.Where(Function(p) p.b.ID = _filter.BRAND_ID)
            End If
            If IsNumeric(_filter.DTTD_NHAN) Then
                query = query.Where(Function(p) p.p.DTTD_NHAN = _filter.DTTD_NHAN)
            End If
            If IsNumeric(_filter.DTTD_KENHDT) Then
                query = query.Where(Function(p) p.p.DTTD_KENHDT = _filter.DTTD_KENHDT)
            End If
            If IsNumeric(_filter.DTTD_KENHNB) Then
                query = query.Where(Function(p) p.p.DTTD_KENHNB = _filter.DTTD_KENHNB)
            End If

            ' select thuộc tính
            Dim lstDTTB_ECD = query.Select(Function(p) New PA_DTTD_ECDDTO With {
                                            .ID = p.p.ID,
                                            .BRAND_ID = p.b.ID,
                                            .BRAND_NAME = p.b.CODE,
                                            .CREATED_DATE = p.p.CREATED_DATE,
                                            .PERIOD_ID = p.p.PERIOD_ID,
                                            .DTTD_NHAN = p.p.DTTD_NHAN,
                                            .DTTD_KENHNB = p.p.DTTD_KENHNB,
                                            .DTTD_KENHDT = p.p.DTTD_KENHDT})

            lstDTTB_ECD = lstDTTB_ECD.OrderBy(Sorts)
            Total = lstDTTB_ECD.Count
            lstDTTB_ECD = lstDTTB_ECD.Skip(PageIndex * PageSize).Take(PageSize)

            Return lstDTTB_ECD.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

#End Region

#Region "LDT_VP"
    Public Function GetLDT_VP(ByVal _filter As PA_LDT_VPDTO, ByVal PageIndex As Integer,
                                           ByVal PageSize As Integer,
                                            ByRef Total As Integer, ByVal _param As ParamDTO,
                                            Optional ByVal Sorts As String = "CREATED_DATE desc",
                                            Optional ByVal log As UserLog = Nothing) As List(Of PA_LDT_VPDTO)

        Try

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using


            Dim query = From p In Context.PA_LDT_VP
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From period In Context.AT_PERIOD.Where(Function(f) f.ID = p.PERIOD_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) e.TITLE_ID = f.ID).DefaultIfEmpty
                        From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = e.ORG_ID And
                                                           f.USERNAME = log.Username.ToUpper)


            ' lọc điều kiện
            Dim dateNow = Date.Now.Date

            If Not _filter.IS_TER Then
                query = query.Where(Function(p) Not p.e.WORK_STATUS.HasValue Or
                                        (p.e.WORK_STATUS.HasValue And
                                         ((p.e.WORK_STATUS <> 257) Or (p.e.WORK_STATUS = 257 And p.e.TER_EFFECT_DATE > dateNow))))

            End If
            If _filter.EMPLOYEE_CODE <> "" Then
                query = query.Where(Function(p) p.e.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If
            If _filter.EMPLOYEE_NAME <> "" Then
                query = query.Where(Function(p) p.e.FULLNAME_VN.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If

            If _filter.ORG_NAME <> "" Then
                query = query.Where(Function(p) p.o.NAME_VN.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If
            If _filter.TITLE_NAME <> "" Then
                query = query.Where(Function(p) p.t.NAME_VN.ToUpper.Contains(_filter.TITLE_NAME.ToUpper))
            End If

            If IsNumeric(_filter.PERIOD_ID) Then
                query = query.Where(Function(p) p.p.PERIOD_ID = _filter.PERIOD_ID)
            End If

            If IsNumeric(_filter.MR) Then
                query = query.Where(Function(p) p.p.MR = _filter.MR)
            End If

            If IsNumeric(_filter.AVBA) Then
                query = query.Where(Function(p) p.p.AVBA = _filter.AVBA)
            End If

            If IsNumeric(_filter.RR6_A) Then
                query = query.Where(Function(p) p.p.RR6_A = _filter.RR6_A)
            End If

            If IsNumeric(_filter.BS) Then
                query = query.Where(Function(p) p.p.BS = _filter.BS)
            End If

            If IsNumeric(_filter.SPSG) Then
                query = query.Where(Function(p) p.p.SPSG = _filter.SPSG)
            End If

            If IsNumeric(_filter.D90) Then
                query = query.Where(Function(p) p.p.D90 = _filter.D90)
            End If

            If IsNumeric(_filter.TA) Then
                query = query.Where(Function(p) p.p.TA = _filter.TA)
            End If

            If IsNumeric(_filter.BILL) Then
                query = query.Where(Function(p) p.p.BILL = _filter.BILL)
            End If

            ' select thuộc tính
            Dim lstLDT_VP = query.Select(Function(p) New PA_LDT_VPDTO With {
                                            .ID = p.p.ID,
                                            .EMPLOYEE_ID = p.e.ID,
                                            .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                            .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                                            .ORG_ID = p.e.ID,
                                            .ORG_NAME = p.o.NAME_VN,
                                            .ORG_DESC = p.o.DESCRIPTION_PATH,
                                            .TITLE_NAME = p.t.NAME_VN,
                                            .CREATED_DATE = p.p.CREATED_DATE,
                                            .PERIOD_ID = p.p.PERIOD_ID,
                                            .MR = p.p.MR,
                                            .AVBA = p.p.AVBA,
                                            .RR6_A = p.p.RR6_A,
                                            .BS = p.p.BS,
                                            .SPSG = p.p.SPSG,
                                            .D90 = p.p.D90,
                                            .TA = p.p.TA,
                                            .BILL = p.p.BILL})

            lstLDT_VP = lstLDT_VP.OrderBy(Sorts)
            Total = lstLDT_VP.Count
            lstLDT_VP = lstLDT_VP.Skip(PageIndex * PageSize).Take(PageSize)

            Return lstLDT_VP.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

#End Region

#Region "PA_MA_SCP_QLCH"
    Public Function GetPA_MA_SCP_QLCH(ByVal _filter As PA_MA_SCP_QLCHDTO, ByVal PageIndex As Integer,
                                           ByVal PageSize As Integer,
                                            ByRef Total As Integer, ByVal _param As ParamDTO,
                                            Optional ByVal Sorts As String = "CREATED_DATE desc",
                                            Optional ByVal log As UserLog = Nothing) As List(Of PA_MA_SCP_QLCHDTO)

        Try

            'Using cls As New DataAccess.QueryData
            '    cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
            '                     New With {.P_USERNAME = log.Username,
            '                               .P_ORGID = _param.ORG_ID,
            '                               .P_ISDISSOLVE = _param.IS_DISSOLVE})
            'End Using


            Dim query = From p In Context.PA_MA_SCP_QLCH
                        From store In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.STORE_ID).DefaultIfEmpty
                        From period In Context.AT_PERIOD.Where(Function(f) f.ID = p.PERIOD_ID).DefaultIfEmpty
                        From ls In Context.OT_OTHER_LIST.Where(Function(f) p.TARGET_GROUP = f.ID).DefaultIfEmpty


            ' lọc điều kiện
            Dim dateNow = Date.Now.Date

            'If Not _filter.IS_TER Then
            '    query = query.Where(Function(p) Not p.e.WORK_STATUS.HasValue Or
            '                            (p.e.WORK_STATUS.HasValue And
            '                             ((p.e.WORK_STATUS <> 257) Or (p.e.WORK_STATUS = 257 And p.e.TER_EFFECT_DATE > dateNow))))

            'End If
            'If _filter.EMPLOYEE_CODE <> "" Then
            '    query = query.Where(Function(p) p.e.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            'End If
            'If _filter.EMPLOYEE_NAME <> "" Then
            '    query = query.Where(Function(p) p.e.FULLNAME_VN.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            'End If

            'If _filter.ORG_NAME <> "" Then
            '    query = query.Where(Function(p) p.o.NAME_VN.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            'End If
            If _filter.TARGET_GROUP_NAME <> "" Then
                query = query.Where(Function(p) p.ls.NAME_VN.ToUpper.Contains(_filter.TARGET_GROUP_NAME.ToUpper))
            End If

            If IsNumeric(_filter.PERIOD_ID) Then
                query = query.Where(Function(p) p.p.PERIOD_ID = _filter.PERIOD_ID)
            End If

            If IsNumeric(_filter.SLKH_RETURN_YEAR) Then
                query = query.Where(Function(p) p.p.SLKH_RETURN_YEAR = _filter.SLKH_RETURN_YEAR)
            End If

            If IsNumeric(_filter.SLKH_MEMBER_YEAR) Then
                query = query.Where(Function(p) p.p.SLKH_MEMBER_YEAR = _filter.SLKH_MEMBER_YEAR)
            End If

            'If IsNumeric(_filter.MA) Then
            '    query = query.Where(Function(p) p.p.MA = _filter.MA)
            'End If

            'If IsNumeric(_filter.SCP) Then
            '    query = query.Where(Function(p) p.p.SCP = _filter.SCP)
            'End If

            'If IsNumeric(_filter.TON_90D) Then
            '    query = query.Where(Function(p) p.p.TON_90D = _filter.TON_90D)
            'End If

            'If IsNumeric(_filter.X) Then
            '    query = query.Where(Function(p) p.p.X = _filter.X)
            'End If

            'If IsNumeric(_filter.Y) Then
            '    query = query.Where(Function(p) p.p.Y = _filter.Y)
            'End If

            'If IsNumeric(_filter.Z) Then
            '    query = query.Where(Function(p) p.p.Z = _filter.Z)
            'End If

            ' select thuộc tính
            Dim lstLDT_VP = query.Select(Function(p) New PA_MA_SCP_QLCHDTO With {
                                            .ID = p.p.ID,
                                            .STORE_ID = p.p.STORE_ID,
                                            .PERIOD_ID = p.p.PERIOD_ID,
                                            .STORE_NAME = p.store.NAME_VN,
                                            .PERIOD_NAME = "Tháng " & p.period.PERIOD_NAME & "/" & p.period.YEAR.Value,
                                            .DTTD = p.p.DTTD,
                                            .DTTD_NG = p.p.DTTD_NG,
                                            .DTTD_KNG1 = p.p.DTTD_KNG1,
                                            .DTTD_KNG2 = p.p.DTTD_KNG2,
                                            .UPT_TD = p.p.UPT_TD,
                                            .CON_TD = p.p.CON_TD,
                                            .CREATED_DATE = p.p.CREATED_DATE,
                                            .RATE_RR12 = p.p.RATE_RR12,
                                            .RATE_SPSG = p.p.RATE_SPSG,
                                            .RATE_CSS = p.p.RATE_CSS,
                                            .RATE_FSOM = p.p.RATE_FSOM,
                                            .RATE_MRA = p.p.RATE_MRA,
                                            .RATE_CR = p.p.RATE_CR,
                                            .RATE_EMAILCUSTOMER = p.p.RATE_EMAILCUSTOMER,
                                            .RATE_MBS = p.p.RATE_MBS,
                                            .RATE_90D = p.p.RATE_90D,
                                            .RATE_MA = p.p.RATE_MA,
                                            .RATE_SCP = p.p.RATE_SCP,
                                            .RR6_TD = p.p.RR6_TD,
                                            .SLBILL_TD = p.p.SLBILL_TD,
                                            .SLTV = p.p.SLTV,
                                            .SLTV_6MONTH = p.p.SLTV_6MONTH,
                                            .SLBILL_NONMEMBER = p.p.SLBILL_NONMEMBER,
                                            .SLBILL_NEWMEMBER = p.p.SLBILL_NEWMEMBER,
                                            .TARGET_GROUP_ID = p.p.TARGET_GROUP,
                                            .TARGET_GROUP_NAME = p.ls.NAME_VN,
                                            .SLKH_MEMBER_YEAR = p.p.SLBILL_NEWMEMBER,
                                            .SLKH_RETURN_YEAR = p.p.SLKH_RETURN_YEAR})

            lstLDT_VP = lstLDT_VP.OrderBy(Sorts)
            Total = lstLDT_VP.Count
            lstLDT_VP = lstLDT_VP.Skip(PageIndex * PageSize).Take(PageSize)

            Return lstLDT_VP.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

#End Region


#Region "Accounting Subsidize"
    Public Function GetAccountingSubsidize(ByVal _filter As PA_AccountingSubsidizeDTO, ByVal PageIndex As Integer,
                                           ByVal PageSize As Integer,
                                            ByRef Total As Integer, ByVal _param As ParamDTO,
                                            Optional ByVal Sorts As String = "CREATED_DATE desc",
                                            Optional ByVal log As UserLog = Nothing) As List(Of PA_AccountingSubsidizeDTO)

        Try

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using


            Dim query = From p In Context.PA_ACCOUNTING_SUBSIDIZE
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From o_set In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_SET_ID).DefaultIfEmpty
                        From period In Context.AT_PERIOD.Where(Function(f) f.ID = p.PERIOD_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
                        From sub_t In Context.OT_OTHER_LIST.Where(Function(f) p.SUBSIDIZE_TYPE = f.ID).DefaultIfEmpty
                        From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = p.ORG_SET_ID And
                                                           f.USERNAME = log.Username.ToUpper)


            ' lọc điều kiện
            'Dim dateNow = Date.Now.Date

            'If Not _filter.IS_TER Then
            '    query = query.Where(Function(p) Not p.e.WORK_STATUS.HasValue Or
            '                            (p.e.WORK_STATUS.HasValue And
            '                             ((p.e.WORK_STATUS <> 257) Or (p.e.WORK_STATUS = 257 And p.e.TER_EFFECT_DATE > dateNow))))

            'End If
            If _filter.EMPLOYEE_CODE <> "" Then
                query = query.Where(Function(p) p.e.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If
            If _filter.EMPLOYEE_NAME <> "" Then
                query = query.Where(Function(p) p.e.FULLNAME_VN.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If
            If _filter.ORG_NAME <> "" Then
                query = query.Where(Function(p) p.o.NAME_VN.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If
            If _filter.TITLE_NAME <> "" Then
                query = query.Where(Function(p) p.t.NAME_VN.ToUpper.Contains(_filter.TITLE_NAME.ToUpper))
            End If
            If _filter.ORG_SET_NAME <> "" Then
                query = query.Where(Function(p) p.o_set.NAME_VN.ToUpper.Contains(_filter.ORG_SET_NAME.ToUpper))
            End If

            If IsNumeric(_filter.PERIOD_ID) Then
                query = query.Where(Function(p) p.p.PERIOD_ID = _filter.PERIOD_ID)
            End If
            If IsNumeric(_filter.OBJ_EMPLOYEE_ID) Then
                query = query.Where(Function(p) p.p.OBJ_EMPLOYEE_ID = _filter.OBJ_EMPLOYEE_ID)
            End If
            If IsNumeric(_filter.SUBSIDIZE_TYPE) Then
                query = query.Where(Function(p) p.p.SUBSIDIZE_TYPE = _filter.SUBSIDIZE_TYPE)
            End If
            If _filter.SUBSIDIZE_TYPE_NAME <> "" Then
                query = query.Where(Function(p) p.sub_t.NAME_VN.ToUpper.Contains(_filter.SUBSIDIZE_TYPE_NAME.ToUpper))
            End If
            If IsNumeric(_filter.NUMBERDAY_PERIOD) Then
                query = query.Where(Function(p) p.p.NUMBERDAY_PERIOD = _filter.NUMBERDAY_PERIOD)
            End If
            If IsNumeric(_filter.NUMBERWORKING_PERIOD) Then
                query = query.Where(Function(p) p.p.NUMBERWORKING_PERIOD = _filter.NUMBERWORKING_PERIOD)
            End If
            If IsNumeric(_filter.NORM_MONEY) Then
                query = query.Where(Function(p) p.p.NORM_MONEY = _filter.NORM_MONEY)
            End If
            If IsNumeric(_filter.BHXH) Then
                query = query.Where(Function(p) p.p.BHXH = _filter.BHXH)
            End If
            If IsNumeric(_filter.VALUE) Then
                query = query.Where(Function(p) p.p.VALUE = _filter.VALUE)
            End If

            ' select thuộc tính
            Dim AccountingSubsidize = query.Select(Function(p) New PA_AccountingSubsidizeDTO With {
                                                                .ID = p.p.ID,
                                                                .EMPLOYEE_ID = p.e.ID,
                                                                .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                                                .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                                                                .ORG_ID = p.e.ID,
                                                                .ORG_NAME = p.o.NAME_VN,
                                                                .ORG_DESC = p.o.DESCRIPTION_PATH,
                                                                .TITLE_ID = p.p.TITLE_ID,
                                                                .TITLE_NAME = p.t.NAME_VN,
                                                                .CREATED_DATE = p.p.CREATED_DATE,
                                                                .ORG_SET_ID = p.p.ORG_SET_ID,
                                                                .ORG_SET_NAME = p.o_set.NAME_VN,
                                                                .PERIOD_ID = p.p.PERIOD_ID,
                                                                .PERIOD_NAME = p.period.PERIOD_NAME & "/" & p.period.YEAR.Value,
                                                                .SUBSIDIZE_TYPE = p.p.SUBSIDIZE_TYPE,
                                                                .SUBSIDIZE_TYPE_NAME = p.sub_t.NAME_VN,
                                                                .IS_KIEMNHIEM = p.p.IS_KIEMNHIEM,
                                                                .IS_LOCK = p.p.IS_LOCK,
                                                                .NUMBERDAY_PERIOD = p.p.NUMBERDAY_PERIOD,
                                                                .NUMBERWORKING_PERIOD = p.p.NUMBERWORKING_PERIOD,
                                                                .NORM_MONEY = p.p.NORM_MONEY,
                                                                .BHXH = p.p.BHXH,
                                                                .VALUE = p.p.VALUE})
            If IsDate(_filter.PERIOD_NAME) Then
                AccountingSubsidize = AccountingSubsidize.Where(Function(p) p.PERIOD_NAME.ToUpper.Contains(_filter.PERIOD_NAME))
            End If
            AccountingSubsidize = AccountingSubsidize.OrderBy(Sorts)
            Total = AccountingSubsidize.Count
            AccountingSubsidize = AccountingSubsidize.Skip(PageIndex * PageSize).Take(PageSize)

            Return AccountingSubsidize.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function DeleteAccountingSubsidize(ByVal _lstID As List(Of Decimal)) As Boolean
        Try
            Dim lstOBJ = (From p In Context.PA_ACCOUNTING_SUBSIDIZE Where _lstID.Contains(p.ID))
            For Each item In lstOBJ
                Context.PA_ACCOUNTING_SUBSIDIZE.DeleteObject(item)
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function ChangeStatusAccountingSubsidize(ByVal _lstID As List(Of Decimal), ByVal _status As Boolean, ByVal log As UserLog) As Boolean
        Try
            Dim lstOBJ = (From p In Context.PA_ACCOUNTING_SUBSIDIZE Where _lstID.Contains(p.ID))
            For Each item In lstOBJ
                item.IS_LOCK = _status
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
#End Region

#Region "PA_STORE_SUBSIDIZE"
    Public Function GetPA_STORE_SUBSIDIZE(ByVal _filter As PA_STORE_SUBSIDIZEDTO, ByVal PageIndex As Integer,
                                           ByVal PageSize As Integer,
                                            ByRef Total As Integer, ByVal _param As ParamDTO,
                                            Optional ByVal Sorts As String = "CREATED_DATE desc",
                                            Optional ByVal log As UserLog = Nothing) As List(Of PA_STORE_SUBSIDIZEDTO)

        Try

            'Using cls As New DataAccess.QueryData
            '    cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
            '                     New With {.P_USERNAME = log.Username,
            '                               .P_ORGID = _param.ORG_ID,
            '                               .P_ISDISSOLVE = _param.IS_DISSOLVE})
            'End Using


            Dim query = From p In Context.PA_STORE_SUBSIDIZE
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From ls In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.BRAND_ID).DefaultIfEmpty


            If _filter.ORG_NAME <> "" Then
                query = query.Where(Function(p) p.o.NAME_VN.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If


            If _filter.NOTE <> "" Then
                query = query.Where(Function(p) p.p.NOTE.ToUpper.Contains(_filter.NOTE.ToUpper))
            End If
            If IsDate(_filter.FROM_DATE) Then
                query = query.Where(Function(p) p.p.EFFECT_DATE >= _filter.FROM_DATE)
            End If
            If IsDate(_filter.TO_DATE) Then
                query = query.Where(Function(p) p.p.EFFECT_DATE <= _filter.TO_DATE)
            End If

            ' select thuộc tính
            Dim SUBSIDIZE = query.Select(Function(p) New PA_STORE_SUBSIDIZEDTO With {
                                            .ID = p.p.ID,
                                            .ORG_ID = p.p.ORG_ID,
                                            .ORG_NAME = p.o.NAME_VN,
                                            .ORG_CODE = p.o.CODE,
                                            .BRAND_ID = p.p.BRAND_ID,
                                            .BRAND_NAME = p.ls.NAME_VN,
                                            .BRAND_RATE = p.p.BRAND_RATE,
                                            .CREATED_DATE = p.p.CREATED_DATE,
                                            .NOTE = p.p.NOTE,
                                            .BENEFIT_VALUE = p.p.BENEFIT_VALUE,
                                            .EFFECT_DATE = p.p.EFFECT_DATE,
                                            .TARGET_PLAN = p.p.TARGET_PLAN,
                                            .REVENUE_MIN = p.p.REVENUE_MIN,
                                            .LESS_REVENUE = p.p.LESS_REVENUE,
                                            .THAN_REVENUE = p.p.THAN_REVENUE})
            SUBSIDIZE = SUBSIDIZE.OrderBy(Sorts)
            Total = SUBSIDIZE.Count
            SUBSIDIZE = SUBSIDIZE.Skip(PageIndex * PageSize).Take(PageSize)

            Return SUBSIDIZE.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function ModifyPA_STORE_SUBSIDIZE(ByVal _objData As PA_STORE_SUBSIDIZEDTO, ByVal userLog As UserLog) As Boolean
        Try
            Dim obj = (From o In Context.PA_STORE_SUBSIDIZE Where o.ID = _objData.ID).FirstOrDefault
            obj.ORG_ID = _objData.ORG_ID
            obj.BRAND_ID = _objData.BRAND_ID
            obj.BRAND_RATE = _objData.BRAND_RATE
            obj.NOTE = _objData.NOTE
            obj.BENEFIT_VALUE = _objData.BENEFIT_VALUE
            obj.EFFECT_DATE = _objData.EFFECT_DATE
            obj.TARGET_PLAN = _objData.TARGET_PLAN
            obj.REVENUE_MIN = _objData.REVENUE_MIN
            obj.LESS_REVENUE = _objData.LESS_REVENUE
            obj.THAN_REVENUE = _objData.THAN_REVENUE
            obj.NOTE = _objData.NOTE
            Context.SaveChanges(userLog)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function


    Public Function InsertPA_STORE_SUBSIDIZE(ByVal _objData As PA_STORE_SUBSIDIZEDTO, ByVal userLog As UserLog) As Boolean
        Try
            Dim obj As New PA_STORE_SUBSIDIZE
            obj.ID = Utilities.GetNextSequence(Context, Context.PA_STORE_SUBSIDIZE.EntitySet.Name)
            obj.ORG_ID = _objData.ORG_ID
            obj.BRAND_ID = _objData.BRAND_ID
            obj.BRAND_RATE = _objData.BRAND_RATE
            obj.NOTE = _objData.NOTE
            obj.BENEFIT_VALUE = _objData.BENEFIT_VALUE
            obj.EFFECT_DATE = _objData.EFFECT_DATE
            obj.TARGET_PLAN = _objData.TARGET_PLAN
            obj.REVENUE_MIN = _objData.REVENUE_MIN
            obj.LESS_REVENUE = _objData.LESS_REVENUE
            obj.THAN_REVENUE = _objData.THAN_REVENUE
            obj.NOTE = _objData.NOTE
            Context.PA_STORE_SUBSIDIZE.AddObject(obj)
            Context.SaveChanges(userLog)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function DeletePA_STORE_SUBSIDIZE(ByVal _lstID As List(Of Decimal)) As Boolean
        Try
            Dim lstOBJ = (From p In Context.PA_STORE_SUBSIDIZE Where _lstID.Contains(p.ID))
            For Each item In lstOBJ
                Context.PA_STORE_SUBSIDIZE.DeleteObject(item)
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function ValidatePA_STORE_SUBSIDIZE(ByVal _objData As PA_STORE_SUBSIDIZEDTO) As Boolean
        Try
            Dim query = (From p In Context.PA_STORE_SUBSIDIZE Where p.ID <> _objData.ID AndAlso p.ORG_ID = _objData.ORG_ID AndAlso p.TARGET_PLAN = _objData.TARGET_PLAN AndAlso p.EFFECT_DATE = _objData.EFFECT_DATE).Any
            Return query
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
    Public Function GET_PA_STORE_SUBSIDIZE_IMPORT() As DataSet
        Try
            Using cls As New DataAccess.QueryData


                Dim dtData As DataSet = cls.ExecuteStore("PKG_PA_SETTING.GET_PA_STORE_SUBSIDIZE_IMPORT", New With {.P_CUR = cls.OUT_CURSOR}, False)
                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
    Public Function Get_Brand_Name(ByVal _objData As PA_STORE_SUBSIDIZEDTO) As PA_STORE_SUBSIDIZEDTO
        Try

            Dim query = (From p In Context.HU_ORG_BRAND
                         From o In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.BRAND_ID).DefaultIfEmpty
                         Where p.EFFECT_DATE <= _objData.EFFECT_DATE _
                                     And p.ORG_ID = _objData.ORG_ID
                         Order By p.EFFECT_DATE Descending
                         Select New PA_STORE_SUBSIDIZEDTO With {.BRAND_NAME = o.NAME_VN,
                                                                .BRAND_ID = p.BRAND_ID}).FirstOrDefault
            Return query
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
    Public Function Get_Rate(ByVal _objData As PA_STORE_SUBSIDIZEDTO) As PA_STORE_SUBSIDIZEDTO
        Try

            Dim query = (From p In Context.PA_BRAND_RATE
                         Where p.EFFECT_DATE <= _objData.EFFECT_DATE _
                               And p.BRAND_ID = _objData.BRAND_ID
                         Order By p.EFFECT_DATE Descending
                         Select New PA_STORE_SUBSIDIZEDTO With {.BRAND_RATE = p.RATE}).FirstOrDefault
            Return query
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
#End Region

#Region "PA_TARGET_STORE"
    Public Function GET_PA_TARGET_STORE(ByVal _filter As PA_TARGET_STOREDTO,
                             ByVal PageIndex As Integer,
                             ByVal PageSize As Integer,
                             ByVal _param As ParamDTO,
                             ByRef Total As Integer,
                             Optional ByVal Sorts As String = "CREATED_DATE desc",
                             Optional ByVal log As UserLog = Nothing) As List(Of PA_TARGET_STOREDTO)

        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using
            Dim query = From p In Context.PA_TARGET_STORE
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) (p.STORE_CODE = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper) _
                                                                            OrElse p.STORE_CODE Is Nothing)
                        From period In Context.AT_PERIOD.Where(Function(f) f.ID = p.PERIOD_ID).DefaultIfEmpty
                        From ot In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TARGET_GROUP).DefaultIfEmpty
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.STORE_CODE).DefaultIfEmpty
                        Select New PA_TARGET_STOREDTO With {
                       .ID = p.ID,
                       .PERIOD_ID = period.ID,
                       .PERIOD_NAME = period.PERIOD_NAME,
                       .PERIOD_YEAR = period.YEAR,
                       .PERIOD_MONTH = period.MONTH,
                       .NOTE = p.NOTE,
                       .TARGET_CON = p.TARGET_CON,
                       .TARGET_DT = p.TARGET_DT,
                       .TARGET_UPT = p.TARGET_UPT,
                       .STORE_CODE = p.STORE_CODE,
                       .STORE_NAME = org.CODE,
                       .MBS_TARGET = p.MBS_TARGET,
                       .RR6_TARGET = p.RR6_TARGET,
                       .SLBILL_TARGET = p.SLBILL_TARGET,
                       .TARGET_GROUP = p.TARGET_GROUP,
                       .TARGET_GROUP_NAME = ot.NAME_VN,
                       .DAYS_TARGET_NUMBER = p.DAYS_TARGET_NUMBER,
                       .TARGET_DT_REDUCE = p.TARGET_DT_REDUCE,
                       .CREATED_DATE = p.CREATED_DATE}

            Dim lst = query.Distinct
            If _filter.PERIOD_YEAR.HasValue Then
                lst = lst.Where(Function(f) f.PERIOD_YEAR = _filter.PERIOD_YEAR)
            End If
            If _filter.PERIOD_MONTH.HasValue Then
                lst = lst.Where(Function(f) f.PERIOD_MONTH = _filter.PERIOD_MONTH)
            End If

            If _filter.TARGET_DT_REDUCE.HasValue Then
                lst = lst.Where(Function(f) f.TARGET_DT_REDUCE = _filter.TARGET_DT_REDUCE)
            End If

            If _filter.TARGET_GROUP_NAME <> "" Then
                lst = lst.Where(Function(f) f.TARGET_GROUP_NAME.ToUpper.Contains(_filter.TARGET_GROUP_NAME.ToUpper))
            End If
            If _filter.STORE_NAME <> "" Then
                lst = lst.Where(Function(f) f.STORE_NAME.ToUpper.Contains(_filter.STORE_NAME.ToUpper))
            End If

            If _filter.NOTE <> "" Then
                lst = lst.Where(Function(f) f.NOTE.ToUpper.Contains(_filter.NOTE.ToUpper))
            End If

            If _filter.TARGET_CON.HasValue Then
                lst = lst.Where(Function(f) f.TARGET_CON = _filter.TARGET_CON)
            End If

            If _filter.TARGET_UPT.HasValue Then
                lst = lst.Where(Function(f) f.TARGET_UPT = _filter.TARGET_UPT)
            End If

            If _filter.MBS_TARGET.HasValue Then
                lst = lst.Where(Function(f) f.MBS_TARGET = _filter.MBS_TARGET)
            End If

            If _filter.RR6_TARGET.HasValue Then
                lst = lst.Where(Function(f) f.RR6_TARGET = _filter.RR6_TARGET)
            End If

            If _filter.SLBILL_TARGET.HasValue Then
                lst = lst.Where(Function(f) f.SLBILL_TARGET = _filter.SLBILL_TARGET)
            End If
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function INSERT_PA_TARGET_STORE(ByVal obj As PA_TARGET_STOREDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objData As New PA_TARGET_STORE
        Dim iCount As Integer = 0
        Try
            objData.ID = Utilities.GetNextSequence(Context, Context.PA_TARGET_STORE.EntitySet.Name)
            objData.STORE_CODE = obj.STORE_CODE
            objData.TARGET_UPT = obj.TARGET_UPT
            objData.TARGET_DT = obj.TARGET_DT
            objData.TARGET_DT = obj.TARGET_DT
            objData.TARGET_CON = obj.TARGET_CON
            objData.MBS_TARGET = obj.MBS_TARGET
            objData.RR6_TARGET = obj.RR6_TARGET
            objData.SLBILL_TARGET = obj.SLBILL_TARGET
            objData.PERIOD_ID = obj.PERIOD_ID
            objData.TARGET_GROUP = obj.TARGET_GROUP
            objData.DAYS_TARGET_NUMBER = obj.DAYS_TARGET_NUMBER
            objData.TARGET_DT_REDUCE = obj.TARGET_DT_REDUCE
            objData.NOTE = obj.NOTE
            Context.PA_TARGET_STORE.AddObject(objData)
            Context.SaveChanges(log)
            gID = objData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function MODIFY_PA_TARGET_STORE(ByVal obj As PA_TARGET_STOREDTO,
                                  ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objData As PA_TARGET_STORE
        Try
            objData = (From p In Context.PA_TARGET_STORE Where p.ID = obj.ID).FirstOrDefault
            objData.ID = obj.ID
            objData.STORE_CODE = obj.STORE_CODE
            objData.TARGET_UPT = obj.TARGET_UPT
            objData.TARGET_DT = obj.TARGET_DT
            objData.MBS_TARGET = obj.MBS_TARGET
            objData.RR6_TARGET = obj.RR6_TARGET
            objData.SLBILL_TARGET = obj.SLBILL_TARGET
            objData.PERIOD_ID = obj.PERIOD_ID
            objData.TARGET_DT = obj.TARGET_DT
            objData.TARGET_GROUP = obj.TARGET_GROUP
            objData.DAYS_TARGET_NUMBER = obj.DAYS_TARGET_NUMBER
            objData.TARGET_DT_REDUCE = obj.TARGET_DT_REDUCE
            objData.TARGET_CON = obj.TARGET_CON
            objData.NOTE = obj.NOTE
            Context.SaveChanges(log)
            gID = objData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function DELETE_PA_TARGET_STORE(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim lstData As List(Of PA_TARGET_STORE)
        Try

            lstData = (From p In Context.PA_TARGET_STORE Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                Context.PA_TARGET_STORE.DeleteObject(lstData(index))
            Next

            Context.SaveChanges(log)
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function VALIDATE_PA_TARGET_STORE(ByVal obj As PA_TARGET_STOREDTO) As Boolean
        Try
            'If obj.TARGET_GROUP = 79401 Then
            '    Dim query = (From f In Context.PA_TARGET_STORE Where f.ID <> obj.ID AndAlso f.PERIOD_ID = obj.PERIOD_ID _
            '                                                                   AndAlso f.TARGET_GROUP = obj.TARGET_GROUP _
            '                                                                   AndAlso f.STORE_CODE = obj.STORE_CODE).Any
            '    Return query
            'Else
            '    Dim query = (From f In Context.PA_TARGET_STORE Where f.ID <> obj.ID AndAlso f.PERIOD_ID = obj.PERIOD_ID _
            '                                                                   AndAlso f.TARGET_GROUP = obj.TARGET_GROUP).Any
            '    Return query
            'End If
            Dim query = (From f In Context.PA_TARGET_STORE Where f.ID <> obj.ID AndAlso f.PERIOD_ID = obj.PERIOD_ID _
                          AndAlso f.TARGET_GROUP = obj.TARGET_GROUP AndAlso (f.STORE_CODE = obj.STORE_CODE Or (f.STORE_CODE Is Nothing And obj.STORE_CODE Is Nothing)))
            Return query.Any
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
#End Region

#Region "PA_SALARY_DETENTION"

    Public Function GetPeriod(ByVal isBlank As Decimal) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_PERIOD",
                                           New With {.P_ISBLANK = isBlank,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetSalaryDetentionByID(ByVal _filter As PA_Salary_DetentionDTO) As PA_Salary_DetentionDTO

        Try
            Dim query = From p In Context.PA_SALARY_DETENTION Where p.ID = _filter.ID
                        From emp In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID).DefaultIfEmpty
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = emp.ORG_ID).DefaultIfEmpty
                        From title In Context.HU_TITLE.Where(Function(f) f.ID = emp.TITLE_ID).DefaultIfEmpty
                        From period In Context.AT_PERIOD.Where(Function(f) f.ID = p.PERIOD_ID).DefaultIfEmpty
                        From payrollSum In Context.PA_PAYROLLSHEET_SUM.Where(Function(f) f.PERIOD_ID = p.PERIOD_ID And f.EMPLOYEE_ID = p.EMPLOYEE_ID).DefaultIfEmpty
                        From payMonth In Context.AT_PERIOD.Where(Function(f) f.ID = p.PAY_MONTH).DefaultIfEmpty
                        Where p.ID = _filter.ID
            Dim obj = query.Select(Function(p) New PA_Salary_DetentionDTO With {
                            .ID = p.p.ID,
                            .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                            .EMPLOYEE_CODE = p.emp.EMPLOYEE_CODE,
                            .FULLNAME_VN = p.emp.FULLNAME_VN,
                            .ORG_NAME = p.org.NAME_VN,
                            .ORG_ID = p.org.ID,
                            .CREATED_DATE = p.p.CREATED_DATE,
                            .TITLE_NAME = p.title.NAME_VN,
                            .PERIOD_ID = p.p.PERIOD_ID,
                            .PERIOD_NAME = p.period.PERIOD_T,
                            .PAY_MONTH_NAME = p.payMonth.PERIOD_T,
                            .PAY_MONTH = p.p.PAY_MONTH,
                            .NOTE = p.p.NOTE})
            Return obj.FirstOrDefault
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function GET_PA_SALARY_DETENTION(ByVal _filter As PA_Salary_DetentionDTO, ByVal PageIndex As Integer,
                                          ByVal PageSize As Integer,
                                          ByRef Total As Integer,
                                          ByVal _param As ParamDTO,
                                          Optional ByVal log As UserLog = Nothing,
                                          Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_Salary_DetentionDTO)

        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using
            Dim lst = (From p In Context.PA_SALARY_DETENTION
                       From emp In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID).DefaultIfEmpty
                       From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = emp.ORG_ID).DefaultIfEmpty
                       From title In Context.HU_TITLE.Where(Function(f) f.ID = emp.TITLE_ID).DefaultIfEmpty
                       From period In Context.AT_PERIOD.Where(Function(f) f.ID = p.PERIOD_ID).DefaultIfEmpty
                       From k In Context.SE_CHOSEN_ORG.Where(Function(f) org.ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)
                       From payrollSum In Context.PA_PAYROLLSHEET_SUM.Where(Function(f) f.PERIOD_ID = p.PERIOD_ID And f.EMPLOYEE_ID = p.EMPLOYEE_ID).DefaultIfEmpty
                       From payMonth In Context.AT_PERIOD.Where(Function(f) f.ID = p.PAY_MONTH).DefaultIfEmpty
                       Select New PA_Salary_DetentionDTO With {
                            .ID = p.ID,
                            .EMPLOYEE_ID = p.EMPLOYEE_ID,
                            .EMPLOYEE_CODE = emp.EMPLOYEE_CODE,
                            .FULLNAME_VN = emp.FULLNAME_VN,
                            .ORG_NAME = org.NAME_VN,
                            .ORG_ID = org.ID,
                            .CREATED_DATE = p.CREATED_DATE,
                            .TITLE_NAME = title.NAME_VN,
                            .PERIOD_ID = p.PERIOD_ID,
                            .PERIOD_NAME = period.PERIOD_T,
                            .PAY_MONTH_NAME = payMonth.PERIOD_T,
                            .PAY_MONTH = p.PAY_MONTH,
                            .NOTE = p.NOTE,
                            .IS_DETENTION = p.IS_DETENTION})


            If _filter.EMPLOYEE_CODE <> "" Then
                lst = lst.Where(Function(f) f.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If
            If _filter.FULLNAME_VN <> "" Then
                lst = lst.Where(Function(f) f.FULLNAME_VN.ToUpper.Contains(_filter.FULLNAME_VN.ToUpper))
            End If
            If _filter.ORG_NAME <> "" Then
                lst = lst.Where(Function(f) f.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If
            If _filter.TITLE_NAME <> "" Then
                lst = lst.Where(Function(f) f.TITLE_NAME.ToUpper.Contains(_filter.TITLE_NAME.ToUpper))
            End If
            If _filter.PERIOD_NAME <> "" Then
                lst = lst.Where(Function(f) f.PERIOD_NAME.ToUpper.Contains(_filter.PERIOD_NAME.ToUpper))
            End If
            If _filter.PAY_MONTH_NAME <> "" Then
                lst = lst.Where(Function(f) f.PAY_MONTH_NAME.ToUpper.Contains(_filter.PAY_MONTH_NAME.ToUpper))
            End If
            If IsNumeric(_filter.PAY_MONTH) Then
                lst = lst.Where(Function(f) f.PAY_MONTH = _filter.PAY_MONTH)
            End If
            If IsNumeric(_filter.PERIOD_ID) Then
                lst = lst.Where(Function(f) f.PERIOD_ID = _filter.PERIOD_ID)
            End If
            If _filter.NOTE <> "" Then
                lst = lst.Where(Function(f) f.NOTE.ToUpper.Contains(_filter.NOTE.ToUpper))
            End If
            If IsNumeric(_filter.INCOME) Then
                lst = lst.Where(Function(f) f.INCOME = _filter.INCOME)
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function INSERT_PA_SALARY_DETENTION(ByVal lstObj As List(Of PA_Salary_DetentionDTO), ByVal userLog As UserLog) As Boolean
        Try
            Dim objData As New PA_SALARY_DETENTION
            For Each item In lstObj
                objData.ID = Utilities.GetNextSequence(Context, Context.PA_SALARY_DETENTION.EntitySet.Name)
                objData.EMPLOYEE_ID = item.EMPLOYEE_ID
                objData.PERIOD_ID = item.PERIOD_ID
                objData.PAY_MONTH = item.PAY_MONTH
                objData.NOTE = item.NOTE
                objData.IS_DETENTION = -1
                Context.PA_SALARY_DETENTION.AddObject(objData)
                Context.SaveChanges(userLog)
            Next
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function MODIFY_PA_SALARY_DETENTION(ByVal lstObj As List(Of PA_Salary_DetentionDTO), ByVal userLog As UserLog) As Boolean
        Try
            For Each item In lstObj
                Dim objData As New PA_SALARY_DETENTION With {.ID = item.ID}
                Context.PA_SALARY_DETENTION.Attach(objData)
                objData.ID = item.ID
                objData.EMPLOYEE_ID = item.EMPLOYEE_ID
                objData.PERIOD_ID = item.PERIOD_ID
                objData.PAY_MONTH = item.PAY_MONTH
                objData.NOTE = item.NOTE
                objData.IS_DETENTION = item.IS_DETENTION
            Next
            Context.SaveChanges(userLog)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function DELETE_PA_SALARY_DETENTION(ByVal lstID As List(Of Decimal)) As Boolean
        Try
            Dim lstObj = (From p In Context.PA_SALARY_DETENTION Where lstID.Contains(p.ID))
            For Each item In lstObj
                Context.PA_SALARY_DETENTION.DeleteObject(item)
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
#End Region

    Public Function GetHSTDTImport() As DataSet
        Try
            Using cls As New DataAccess.QueryData
                Dim dsdata As DataSet = cls.ExecuteStore("PKG_PA_BUSINESS.GET_HSTDT_IMPORT",
                                                         New With {.P_CUR1 = cls.OUT_CURSOR,
                                                                   .P_CUR2 = cls.OUT_CURSOR}, False)

                Return dsdata
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

#Region "PA_MANAGE_DTTD_DAILY"
    Public Function GetManageDTTDDaily(ByVal _filter As PA_ManageDTTDDailyDTO, ByVal PageIndex As Integer,
                                           ByVal PageSize As Integer,
                                            ByRef Total As Integer,
                                            Optional ByVal Sorts As String = "CREATED_DATE desc",
                                            Optional ByVal log As UserLog = Nothing) As List(Of PA_ManageDTTDDailyDTO)

        Try

            Dim query = From p In Context.PA_MANAGE_DTTD_DAILY
                        From store In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.STORE_ID).DefaultIfEmpty
                        From ls In Context.OT_OTHER_LIST.Where(Function(f) p.TARGET_GROUP = f.ID).DefaultIfEmpty


            Dim dateNow = Date.Now.Date

            If _filter.TARGET_GROUP_NAME <> "" Then
                query = query.Where(Function(p) p.ls.NAME_VN.ToUpper.Contains(_filter.TARGET_GROUP_NAME.ToUpper))
            End If

            If _filter.TARGET_GROUP_ID.HasValue Then
                query = query.Where(Function(p) p.p.TARGET_GROUP = _filter.TARGET_GROUP_ID)
            End If

            If _filter.SALE_DATE.HasValue Then
                query = query.Where(Function(p) p.p.SALE_DATE = _filter.SALE_DATE)
            End If
            If _filter.FROM_DATE.HasValue Then
                query = query.Where(Function(p) p.p.SALE_DATE >= _filter.FROM_DATE)
            End If
            If _filter.TO_DATE.HasValue Then
                query = query.Where(Function(p) p.p.SALE_DATE <= _filter.TO_DATE)
            End If

            If _filter.DTTD.HasValue Then
                query = query.Where(Function(p) p.p.DTTD = _filter.DTTD)
            End If

            If _filter.DTTD_NG.HasValue Then
                query = query.Where(Function(p) p.p.DTTD_NG = _filter.DTTD_NG)
            End If

            If _filter.DTTD_KNG1.HasValue Then
                query = query.Where(Function(p) p.p.DTTD_KNG1 = _filter.DTTD_KNG1)
            End If

            If _filter.DTTD_KNG2.HasValue Then
                query = query.Where(Function(p) p.p.DTTD_KNG2 = _filter.DTTD_KNG2)
            End If

            ' select thuộc tính
            Dim lstObj = query.Select(Function(p) New PA_ManageDTTDDailyDTO With {
                                            .ID = p.p.ID,
                                            .STORE_ID = p.p.STORE_ID,
                                            .STORE_NAME = p.store.NAME_VN,
                                            .SALE_DATE = p.p.SALE_DATE,
                                            .DTTD = p.p.DTTD,
                                            .DTTD_NG = p.p.DTTD_NG,
                                            .DTTD_KNG1 = p.p.DTTD_KNG1,
                                            .DTTD_KNG2 = p.p.DTTD_KNG2,
                                            .CREATED_DATE = p.p.CREATED_DATE,
                                            .TARGET_GROUP_ID = p.p.TARGET_GROUP,
                                            .TARGET_GROUP_NAME = p.ls.NAME_VN})

            lstObj = lstObj.OrderBy(Sorts)
            Total = lstObj.Count
            lstObj = lstObj.Skip(PageIndex * PageSize).Take(PageSize)

            Return lstObj.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function GET_IMPORT_DTTD_DAILY() As DataSet
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataSet = cls.ExecuteStore("PKG_PA_SETTING.GET_IMPORT_DTTD_DAILY",
                                           New With {.P_CUR = cls.OUT_CURSOR,
                                                     .P_CUR1 = cls.OUT_CURSOR}, False)
                Return dtData
            End Using
        Catch ex As Exception
            Return Nothing
        End Try

    End Function
    Public Function IMPORT_DTTD_DAILY(ByVal DATA_IN As String, ByVal log As UserLog) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PA_SETTING.IMPORT_DTTD_DAILY",
                                               New With {.P_DOCXML = DATA_IN,
                                                         .P_USER = log.Username.ToUpper,
                                                         .P_CUR = cls.OUT_CURSOR}, True)
                Return CBool(dtData(0)(0))
            End Using
        Catch ex As Exception
            Return Nothing
        End Try

    End Function
#End Region

#Region "PayrollSheet lock"
    Public Function GetPayroolSheetLock(ByVal _filter As PA_PayrollSheetLockDTO, ByVal lstOrg As List(Of Decimal),
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "EMPLOYEE_CODE desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of PA_PayrollSheetLockDTO)

        Try

            Dim query = From p In Context.PA_PAYROLLSHEET_SUM
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
                        From obj In Context.OT_OTHER_LIST.Where(Function(f) p.OBJ_EMPLOYEE_ID = f.ID).DefaultIfEmpty
                        From period In Context.AT_PERIOD.Where(Function(f) p.PERIOD_ID = f.ID).DefaultIfEmpty
                        From l In Context.PA_PAYROLLSHEET_LOCK.Where(Function(f) f.EMPLOYEE_ID = p.EMPLOYEE_ID And f.PERIOD_ID = p.PERIOD_ID).DefaultIfEmpty
                        Where lstOrg.Contains(p.ORG_ID)


            If _filter.PERIOD_ID.HasValue Then
                query = query.Where(Function(p) p.p.PERIOD_ID = _filter.PERIOD_ID)
            End If
            ' select thuộc tính
            Dim lstObj = query.Select(Function(p) New PA_PayrollSheetLockDTO With {.EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                                                                   .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                                                                   .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                                                                                   .ORG_ID = p.p.ORG_ID,
                                                                                   .ORG_NAME = p.o.NAME_VN,
                                                                                   .TITLE_ID = p.p.TITLE_ID,
                                                                                   .TITLE_NAME = p.t.NAME_VN,
                                                                                   .PERIOD_ID = p.p.PERIOD_ID,
                                                                                   .PERIOD_MONTH = p.period.PERIOD_T,
                                                                                   .OBJ_EMPLOYEE_ID = p.p.OBJ_EMPLOYEE_ID,
                                                                                   .OBJ_EMPLOYEE_NAME = p.obj.NAME_VN,
                                                                                   .IS_LOCK = p.l.IS_LOCK,
                                                                                   .IS_LOCK_TEXT = If(p.l.IS_LOCK = 1, "X", "")})

            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                lstObj = lstObj.Where(Function(f) f.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_NAME) Then
                lstObj = lstObj.Where(Function(f) f.EMPLOYEE_NAME.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                lstObj = lstObj.Where(Function(f) f.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                lstObj = lstObj.Where(Function(f) f.TITLE_NAME.ToUpper.Contains(_filter.TITLE_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.OBJ_EMPLOYEE_NAME) Then
                lstObj = lstObj.Where(Function(f) f.OBJ_EMPLOYEE_NAME.ToUpper.Contains(_filter.OBJ_EMPLOYEE_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.IS_LOCK_TEXT) Then
                lstObj = lstObj.Where(Function(f) f.IS_LOCK_TEXT.ToUpper.Contains(_filter.IS_LOCK_TEXT.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.PERIOD_MONTH) Then
                lstObj = lstObj.Where(Function(f) f.PERIOD_MONTH.ToUpper.Equals(_filter.PERIOD_MONTH.ToUpper))
            End If
            If _filter.TT_THUNHAP_CONLAI.HasValue Then
                lstObj = lstObj.Where(Function(f) f.TT_THUNHAP_CONLAI = _filter.TT_THUNHAP_CONLAI)
            End If

            lstObj = lstObj.OrderBy(Sorts)
            Total = lstObj.Count
            lstObj = lstObj.Skip(PageIndex * PageSize).Take(PageSize)

            Return lstObj.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function ChangeStatusPASheetLock(ByVal lstOrg As List(Of Decimal), ByVal lstEmp As List(Of Decimal), ByVal _status As Decimal,
                                           ByVal _period_id As Decimal, ByVal _log As UserLog) As Boolean
        Try
            Dim strOrg = String.Join(",", lstOrg.ToArray())
            Dim strEmp = ""
            If lstEmp.Count > 0 Then
                strEmp = "," & String.Join(",", lstEmp.ToArray()) & ","
            End If
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PA_SETTING.CHANGE_STATUS_PA_LOCK",
                                               New With {.P_ORGS = strOrg,
                                                         .P_EMPS = strEmp,
                                                         .P_STATUS = _status,
                                                         .P_PERIOD_ID = _period_id,
                                                         .P_USERNAME = _log.Username.ToUpper,
                                                         .P_CUR = cls.OUT_CURSOR}, True)
                Return CBool(dtData(0)(0))
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
#End Region

#Region "Award 13 month"
    Public Function GetAward13Month(ByVal _filter As PA_Award_13MonthDTO, ByVal lstOrg As List(Of Decimal),
                                   ByVal PageIndex As Integer,
                                   ByVal PageSize As Integer,
                                   ByRef Total As Integer,
                                   Optional ByVal Sorts As String = "EMPLOYEE_CODE desc",
                                   Optional ByVal log As UserLog = Nothing) As List(Of PA_Award_13MonthDTO)

        Try
            Dim strOrg = String.Join(",", lstOrg)

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG2",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = strOrg})
            End Using

            Dim query = From p In Context.PA_AWARD_13MONTH
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
                        From pe In Context.AT_PERIOD.Where(Function(f) f.ID = p.PERIOD_ID).DefaultIfEmpty
                        From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = p.ORG_ID And f.USERNAME = log.Username.ToUpper)


            ' select thuộc tính
            Dim lstObj = query.Select(Function(p) New PA_Award_13MonthDTO With {.EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                                                                .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                                                                .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                                                                                .ID = p.p.ID,
                                                                                .ORG_ID = p.p.ORG_ID,
                                                                                .ORG_NAME = p.o.NAME_VN,
                                                                                .TITLE_ID = p.p.TITLE_ID,
                                                                                .TITLE_NAME = p.t.NAME_VN,
                                                                                .AWARD_YEAR = p.p.AWARD_YEAR,
                                                                                .SALARY_AWARD = p.p.SALARY_AWARD,
                                                                                .SALARY_LIABILITY = p.p.SALARY_LIABILITY,
                                                                                .SENIORITY_MONTH = p.p.SENIORITY_MONTH,
                                                                                .SENIORITY_REALIRY = p.p.SENIORITY_REALIRY,
                                                                                .NONSALARY_UNIT = p.p.NONSALARY_UNIT,
                                                                                .NONSALARY_UNITEDIT = p.p.NONSALARY_UNITEDIT,
                                                                                .AWARD_13MONTH = p.p.AWARD_13MONTH,
                                                                                .NOTE = p.p.NOTE,
                                                                                .PERIOD_NAME = p.pe.PERIOD_T,
                                                                                .PERIOD_ID = p.p.PERIOD_ID,
                                                                                .CREATED_DATE = p.p.CREATED_DATE,
                                                                                .IS_LOCK = p.p.IS_LOCK})

            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                lstObj = lstObj.Where(Function(f) f.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_NAME) Then
                lstObj = lstObj.Where(Function(f) f.EMPLOYEE_NAME.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                lstObj = lstObj.Where(Function(f) f.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                lstObj = lstObj.Where(Function(f) f.TITLE_NAME.ToUpper.Contains(_filter.TITLE_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.NOTE) Then
                lstObj = lstObj.Where(Function(f) f.NOTE.ToUpper.Contains(_filter.NOTE.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.PERIOD_NAME) Then
                lstObj = lstObj.Where(Function(f) f.PERIOD_NAME.ToUpper.Contains(_filter.PERIOD_NAME.ToUpper))
            End If
            If _filter.AWARD_YEAR.HasValue Then
                lstObj = lstObj.Where(Function(f) f.AWARD_YEAR = _filter.AWARD_YEAR)
            End If

            If _filter.SALARY_AWARD.HasValue Then
                lstObj = lstObj.Where(Function(f) f.SALARY_AWARD = _filter.SALARY_AWARD)
            End If

            If _filter.SALARY_LIABILITY.HasValue Then
                lstObj = lstObj.Where(Function(f) f.SALARY_LIABILITY = _filter.SALARY_LIABILITY)
            End If

            If _filter.SENIORITY_MONTH.HasValue Then
                lstObj = lstObj.Where(Function(f) f.SENIORITY_MONTH = _filter.SENIORITY_MONTH)
            End If

            If _filter.SENIORITY_REALIRY.HasValue Then
                lstObj = lstObj.Where(Function(f) f.SENIORITY_REALIRY = _filter.SENIORITY_REALIRY)
            End If

            If _filter.NONSALARY_UNIT.HasValue Then
                lstObj = lstObj.Where(Function(f) f.NONSALARY_UNIT = _filter.NONSALARY_UNIT)
            End If

            If _filter.NONSALARY_UNITEDIT.HasValue Then
                lstObj = lstObj.Where(Function(f) f.NONSALARY_UNITEDIT = _filter.NONSALARY_UNITEDIT)
            End If

            If _filter.AWARD_13MONTH.HasValue Then
                lstObj = lstObj.Where(Function(f) f.AWARD_13MONTH = _filter.AWARD_13MONTH)
            End If

            lstObj = lstObj.OrderBy(Sorts)
            Total = lstObj.Count
            lstObj = lstObj.Skip(PageIndex * PageSize).Take(PageSize)

            Return lstObj.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function ChangeStatusAward13Month(ByVal lstID As List(Of Decimal), ByVal _status As Decimal, ByVal _log As UserLog) As Boolean
        Try
            Dim lstObj = From p In Context.PA_AWARD_13MONTH Where lstID.Contains(p.ID)

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

    Public Function DeleteAward13Month(ByVal lstID As List(Of Decimal)) As Boolean
        Try
            Dim lstObj = From p In Context.PA_AWARD_13MONTH Where lstID.Contains(p.ID)

            For Each item In lstObj
                Context.PA_AWARD_13MONTH.DeleteObject(item)
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function UpdateAward13Month(ByVal lstObj As List(Of PA_Award_13MonthDTO), ByVal log As UserLog) As Boolean
        Try

            For Each item In lstObj
                Dim obj = (From p In Context.PA_AWARD_13MONTH Where p.ID = item.ID).FirstOrDefault
                If obj IsNot Nothing Then
                    obj.NONSALARY_UNITEDIT = item.NONSALARY_UNITEDIT
                    obj.NOTE = item.NOTE
                End If
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function CAL_AWARD_13MONTH(ByVal _org_id As List(Of Decimal), ByVal P_YEAR As Decimal, ByVal log As UserLog) As Boolean
        Try
            Dim strOrg = String.Join(",", _org_id)
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PA_SETTING.CAL_AWARD_13MONTH",
                                               New With {.P_ORG_ID = strOrg,
                                                         .P_YEAR = P_YEAR,
                                                         .P_USER = log.Username.ToUpper,
                                                         .P_OUT = cls.OUT_CURSOR}, True)
                Return CBool(dtData(0)(0))
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function RE_CAL_AWARD_13MONTH(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean
        Try
            Dim strID = String.Join(",", lstID)
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PA_SETTING.RE_CAL_AWARD_13MONTH",
                                               New With {.P_ID = strID,
                                                         .P_USER = log.Username.ToUpper,
                                                         .P_OUT = cls.OUT_CURSOR}, True)
                Return CBool(dtData(0)(0))
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function UpdateAward13MonthPeriod(ByVal lstObj As List(Of PA_Award_13MonthDTO), ByVal log As UserLog) As Boolean
        Try

            For Each item In lstObj
                Dim obj = (From p In Context.PA_AWARD_13MONTH Where p.ID = item.ID).FirstOrDefault
                If obj IsNot Nothing Then
                    obj.PERIOD_ID = item.PERIOD_ID
                End If
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

#End Region

#Region "Salary PIT"
    Public Function GetListSalPITCode(ByVal _filter As PA_Salary_PITCodeDTO, ByVal lstOrg As List(Of Decimal),
                                       ByVal PageIndex As Integer,
                                       ByVal PageSize As Integer,
                                       ByRef Total As Integer,
                                       Optional ByVal Sorts As String = "EMPLOYEE_CODE desc",
                                       Optional ByVal log As UserLog = Nothing) As List(Of PA_Salary_PITCodeDTO)

        Try
            Dim strOrg = String.Join(",", lstOrg)

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG2",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = strOrg})
            End Using

            Dim query = From p In Context.PA_SALARY_PITCODE
                        From ecv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.PIT_CODE = p.PIT_CODE)
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = ecv.EMPLOYEE_ID)
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = e.ORG_ID And f.USERNAME = log.Username.ToUpper)


            ' select thuộc tính
            Dim lstObj = query.Select(Function(p) New PA_Salary_PITCodeDTO With {.EMPLOYEE_ID = p.e.ID,
                                                                                .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                                                                .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                                                                                .ID = p.p.ID,
                                                                                .ORG_ID = p.e.ORG_ID,
                                                                                .ORG_NAME = p.o.NAME_VN,
                                                                                .TITLE_ID = p.e.TITLE_ID,
                                                                                .TITLE_NAME = p.t.NAME_VN,
                                                                                .YEAR = p.p.YEAR,
                                                                                .PIT_CODE = p.p.PIT_CODE,
                                                                                .TT_THUNHAP_CHIUTHUE1 = p.p.TT_THUNHAP_CHIUTHUE1,
                                                                                .TT_GTGC_NV1 = p.p.TT_GTGC_NV1,
                                                                                .TT_SUM_BHXH1 = p.p.TT_SUM_BHXH1,
                                                                                .TT_THUNHAP_TINHTHUE1 = p.p.TT_THUNHAP_TINHTHUE1,
                                                                                .TT_THUETNCN1 = p.p.TT_THUETNCN1,
                                                                                .TT_THUNHAP_CHIUTHUE2 = p.p.TT_THUNHAP_CHIUTHUE2,
                                                                                .TT_GTGC_NV2 = p.p.TT_GTGC_NV2,
                                                                                .TT_SUM_BHXH2 = p.p.TT_SUM_BHXH2,
                                                                                .TT_THUNHAP_TINHTHUE2 = p.p.TT_THUNHAP_TINHTHUE2,
                                                                                .TT_THUETNCN2 = p.p.TT_THUETNCN2,
                                                                                .TT_THUNHAP_CHIUTHUE3 = p.p.TT_THUNHAP_CHIUTHUE3,
                                                                                .TT_GTGC_NV3 = p.p.TT_GTGC_NV3,
                                                                                .TT_SUM_BHXH3 = p.p.TT_SUM_BHXH3,
                                                                                .TT_THUNHAP_TINHTHUE3 = p.p.TT_THUNHAP_TINHTHUE3,
                                                                                .TT_THUETNCN3 = p.p.TT_THUETNCN3,
                                                                                .TT_THUNHAP_CHIUTHUE4 = p.p.TT_THUNHAP_CHIUTHUE4,
                                                                                .TT_GTGC_NV4 = p.p.TT_GTGC_NV4,
                                                                                .TT_SUM_BHXH4 = p.p.TT_SUM_BHXH4,
                                                                                .TT_THUNHAP_TINHTHUE4 = p.p.TT_THUNHAP_TINHTHUE4,
                                                                                .TT_THUETNCN4 = p.p.TT_THUETNCN4,
                                                                                .TT_THUNHAP_CHIUTHUE5 = p.p.TT_THUNHAP_CHIUTHUE5,
                                                                                .TT_GTGC_NV5 = p.p.TT_GTGC_NV5,
                                                                                .TT_SUM_BHXH5 = p.p.TT_SUM_BHXH5,
                                                                                .TT_THUNHAP_TINHTHUE5 = p.p.TT_THUNHAP_TINHTHUE5,
                                                                                .TT_THUETNCN5 = p.p.TT_THUETNCN5,
                                                                                .TT_THUNHAP_CHIUTHUE6 = p.p.TT_THUNHAP_CHIUTHUE6,
                                                                                .TT_GTGC_NV6 = p.p.TT_GTGC_NV6,
                                                                                .TT_SUM_BHXH6 = p.p.TT_SUM_BHXH6,
                                                                                .TT_THUNHAP_TINHTHUE6 = p.p.TT_THUNHAP_TINHTHUE6,
                                                                                .TT_THUETNCN6 = p.p.TT_THUETNCN6,
                                                                                .TT_THUNHAP_CHIUTHUE7 = p.p.TT_THUNHAP_CHIUTHUE7,
                                                                                .TT_GTGC_NV7 = p.p.TT_GTGC_NV7,
                                                                                .TT_SUM_BHXH7 = p.p.TT_SUM_BHXH7,
                                                                                .TT_THUNHAP_TINHTHUE7 = p.p.TT_THUNHAP_TINHTHUE7,
                                                                                .TT_THUETNCN7 = p.p.TT_THUETNCN7,
                                                                                .TT_THUNHAP_CHIUTHUE8 = p.p.TT_THUNHAP_CHIUTHUE8,
                                                                                .TT_GTGC_NV8 = p.p.TT_GTGC_NV8,
                                                                                .TT_SUM_BHXH8 = p.p.TT_SUM_BHXH8,
                                                                                .TT_THUNHAP_TINHTHUE8 = p.p.TT_THUNHAP_TINHTHUE8,
                                                                                .TT_THUETNCN8 = p.p.TT_THUETNCN8,
                                                                                .TT_THUNHAP_CHIUTHUE9 = p.p.TT_THUNHAP_CHIUTHUE9,
                                                                                .TT_GTGC_NV9 = p.p.TT_GTGC_NV9,
                                                                                .TT_SUM_BHXH9 = p.p.TT_SUM_BHXH9,
                                                                                .TT_THUNHAP_TINHTHUE9 = p.p.TT_THUNHAP_TINHTHUE9,
                                                                                .TT_THUETNCN9 = p.p.TT_THUETNCN9,
                                                                                .TT_THUNHAP_CHIUTHUE10 = p.p.TT_THUNHAP_CHIUTHUE10,
                                                                                .TT_GTGC_NV10 = p.p.TT_GTGC_NV10,
                                                                                .TT_SUM_BHXH10 = p.p.TT_SUM_BHXH10,
                                                                                .TT_THUNHAP_TINHTHUE10 = p.p.TT_THUNHAP_TINHTHUE10,
                                                                                .TT_THUETNCN10 = p.p.TT_THUETNCN10,
                                                                                .TT_THUNHAP_CHIUTHUE11 = p.p.TT_THUNHAP_CHIUTHUE11,
                                                                                .TT_GTGC_NV11 = p.p.TT_GTGC_NV11,
                                                                                .TT_SUM_BHXH11 = p.p.TT_SUM_BHXH11,
                                                                                .TT_THUNHAP_TINHTHUE11 = p.p.TT_THUNHAP_TINHTHUE11,
                                                                                .TT_THUETNCN11 = p.p.TT_THUETNCN11,
                                                                                .TT_THUNHAP_CHIUTHUE12 = p.p.TT_THUNHAP_CHIUTHUE12,
                                                                                .TT_GTGC_NV12 = p.p.TT_GTGC_NV12,
                                                                                .TT_SUM_BHXH12 = p.p.TT_SUM_BHXH12,
                                                                                .TT_THUNHAP_TINHTHUE12 = p.p.TT_THUNHAP_TINHTHUE12,
                                                                                .TT_THUETNCN12 = p.p.TT_THUETNCN12,
                                                                                .TT_TAMUNG_LUONG12 = p.p.TT_TAMUNG_LUONG12,
                                                                                .M12_THUNHAP_CHIUTHUE = p.p.M12_THUNHAP_CHIUTHUE,
                                                                                .M12_GTGC_NV = p.p.M12_GTGC_NV,
                                                                                .M12_SUM_BHXH = p.p.M12_SUM_BHXH,
                                                                                .M12_THUNHAP_TINHTHUE = p.p.M12_THUNHAP_TINHTHUE,
                                                                                .M12_THUETNCN = p.p.M12_THUETNCN,
                                                                                .TT_CHIUTHUE_TOTAL = p.p.TT_CHIUTHUE_TOTAL,
                                                                                .TT_BHXH_TOTAL = p.p.TT_BHXH_TOTAL,
                                                                                .TT_THUETNCN_TOTAL = p.p.TT_THUETNCN_TOTAL,
                                                                                .CREATED_DATE = p.p.CREATED_DATE,
                                                                                .IS_LOCK = p.p.IS_LOCK})

            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                lstObj = lstObj.Where(Function(f) f.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_NAME) Then
                lstObj = lstObj.Where(Function(f) f.EMPLOYEE_NAME.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                lstObj = lstObj.Where(Function(f) f.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                lstObj = lstObj.Where(Function(f) f.TITLE_NAME.ToUpper.Contains(_filter.TITLE_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.PIT_CODE) Then
                lstObj = lstObj.Where(Function(f) f.PIT_CODE.ToUpper.Contains(_filter.PIT_CODE.ToUpper))
            End If

            If _filter.TT_THUNHAP_CHIUTHUE1.HasValue Then
                lstObj = lstObj.Where(Function(f) f.TT_THUNHAP_CHIUTHUE1 = _filter.TT_THUNHAP_CHIUTHUE1)
            End If
            If _filter.TT_GTGC_NV1.HasValue Then
                lstObj = lstObj.Where(Function(f) f.TT_GTGC_NV1 = _filter.TT_GTGC_NV1)
            End If
            If _filter.TT_SUM_BHXH1.HasValue Then
                lstObj = lstObj.Where(Function(f) f.TT_SUM_BHXH1 = _filter.TT_SUM_BHXH1)
            End If
            If _filter.TT_THUNHAP_TINHTHUE1.HasValue Then
                lstObj = lstObj.Where(Function(f) f.TT_THUNHAP_TINHTHUE1 = _filter.TT_THUNHAP_TINHTHUE1)
            End If
            If _filter.TT_THUETNCN1.HasValue Then
                lstObj = lstObj.Where(Function(f) f.TT_THUETNCN1 = _filter.TT_THUETNCN1)
            End If

            If _filter.TT_THUNHAP_CHIUTHUE2.HasValue Then
                lstObj = lstObj.Where(Function(f) f.TT_THUNHAP_CHIUTHUE2 = _filter.TT_THUNHAP_CHIUTHUE2)
            End If
            If _filter.TT_GTGC_NV2.HasValue Then
                lstObj = lstObj.Where(Function(f) f.TT_GTGC_NV2 = _filter.TT_GTGC_NV2)
            End If
            If _filter.TT_SUM_BHXH2.HasValue Then
                lstObj = lstObj.Where(Function(f) f.TT_SUM_BHXH2 = _filter.TT_SUM_BHXH2)
            End If
            If _filter.TT_THUNHAP_TINHTHUE2.HasValue Then
                lstObj = lstObj.Where(Function(f) f.TT_THUNHAP_TINHTHUE2 = _filter.TT_THUNHAP_TINHTHUE2)
            End If
            If _filter.TT_THUETNCN2.HasValue Then
                lstObj = lstObj.Where(Function(f) f.TT_THUETNCN2 = _filter.TT_THUETNCN2)
            End If

            If _filter.TT_THUNHAP_CHIUTHUE3.HasValue Then
                lstObj = lstObj.Where(Function(f) f.TT_THUNHAP_CHIUTHUE3 = _filter.TT_THUNHAP_CHIUTHUE3)
            End If
            If _filter.TT_GTGC_NV3.HasValue Then
                lstObj = lstObj.Where(Function(f) f.TT_GTGC_NV3 = _filter.TT_GTGC_NV3)
            End If
            If _filter.TT_SUM_BHXH3.HasValue Then
                lstObj = lstObj.Where(Function(f) f.TT_SUM_BHXH3 = _filter.TT_SUM_BHXH3)
            End If
            If _filter.TT_THUNHAP_TINHTHUE3.HasValue Then
                lstObj = lstObj.Where(Function(f) f.TT_THUNHAP_TINHTHUE3 = _filter.TT_THUNHAP_TINHTHUE3)
            End If
            If _filter.TT_THUETNCN3.HasValue Then
                lstObj = lstObj.Where(Function(f) f.TT_THUETNCN3 = _filter.TT_THUETNCN3)
            End If

            If _filter.TT_THUNHAP_CHIUTHUE4.HasValue Then
                lstObj = lstObj.Where(Function(f) f.TT_THUNHAP_CHIUTHUE4 = _filter.TT_THUNHAP_CHIUTHUE4)
            End If
            If _filter.TT_GTGC_NV4.HasValue Then
                lstObj = lstObj.Where(Function(f) f.TT_GTGC_NV4 = _filter.TT_GTGC_NV4)
            End If
            If _filter.TT_SUM_BHXH4.HasValue Then
                lstObj = lstObj.Where(Function(f) f.TT_SUM_BHXH4 = _filter.TT_SUM_BHXH4)
            End If
            If _filter.TT_THUNHAP_TINHTHUE4.HasValue Then
                lstObj = lstObj.Where(Function(f) f.TT_THUNHAP_TINHTHUE4 = _filter.TT_THUNHAP_TINHTHUE4)
            End If
            If _filter.TT_THUETNCN4.HasValue Then
                lstObj = lstObj.Where(Function(f) f.TT_THUETNCN4 = _filter.TT_THUETNCN4)
            End If

            If _filter.TT_THUNHAP_CHIUTHUE5.HasValue Then
                lstObj = lstObj.Where(Function(f) f.TT_THUNHAP_CHIUTHUE5 = _filter.TT_THUNHAP_CHIUTHUE5)
            End If
            If _filter.TT_GTGC_NV5.HasValue Then
                lstObj = lstObj.Where(Function(f) f.TT_GTGC_NV5 = _filter.TT_GTGC_NV5)
            End If
            If _filter.TT_SUM_BHXH5.HasValue Then
                lstObj = lstObj.Where(Function(f) f.TT_SUM_BHXH5 = _filter.TT_SUM_BHXH5)
            End If
            If _filter.TT_THUNHAP_TINHTHUE5.HasValue Then
                lstObj = lstObj.Where(Function(f) f.TT_THUNHAP_TINHTHUE5 = _filter.TT_THUNHAP_TINHTHUE5)
            End If
            If _filter.TT_THUETNCN5.HasValue Then
                lstObj = lstObj.Where(Function(f) f.TT_THUETNCN5 = _filter.TT_THUETNCN5)
            End If

            If _filter.TT_THUNHAP_CHIUTHUE6.HasValue Then
                lstObj = lstObj.Where(Function(f) f.TT_THUNHAP_CHIUTHUE6 = _filter.TT_THUNHAP_CHIUTHUE6)
            End If
            If _filter.TT_GTGC_NV6.HasValue Then
                lstObj = lstObj.Where(Function(f) f.TT_GTGC_NV6 = _filter.TT_GTGC_NV6)
            End If
            If _filter.TT_SUM_BHXH6.HasValue Then
                lstObj = lstObj.Where(Function(f) f.TT_SUM_BHXH6 = _filter.TT_SUM_BHXH6)
            End If
            If _filter.TT_THUNHAP_TINHTHUE6.HasValue Then
                lstObj = lstObj.Where(Function(f) f.TT_THUNHAP_TINHTHUE6 = _filter.TT_THUNHAP_TINHTHUE6)
            End If
            If _filter.TT_THUETNCN6.HasValue Then
                lstObj = lstObj.Where(Function(f) f.TT_THUETNCN6 = _filter.TT_THUETNCN6)
            End If

            If _filter.TT_THUNHAP_CHIUTHUE7.HasValue Then
                lstObj = lstObj.Where(Function(f) f.TT_THUNHAP_CHIUTHUE7 = _filter.TT_THUNHAP_CHIUTHUE7)
            End If
            If _filter.TT_GTGC_NV7.HasValue Then
                lstObj = lstObj.Where(Function(f) f.TT_GTGC_NV7 = _filter.TT_GTGC_NV7)
            End If
            If _filter.TT_SUM_BHXH7.HasValue Then
                lstObj = lstObj.Where(Function(f) f.TT_SUM_BHXH7 = _filter.TT_SUM_BHXH7)
            End If
            If _filter.TT_THUNHAP_TINHTHUE7.HasValue Then
                lstObj = lstObj.Where(Function(f) f.TT_THUNHAP_TINHTHUE7 = _filter.TT_THUNHAP_TINHTHUE7)
            End If
            If _filter.TT_THUETNCN7.HasValue Then
                lstObj = lstObj.Where(Function(f) f.TT_THUETNCN7 = _filter.TT_THUETNCN7)
            End If

            If _filter.TT_THUNHAP_CHIUTHUE8.HasValue Then
                lstObj = lstObj.Where(Function(f) f.TT_THUNHAP_CHIUTHUE8 = _filter.TT_THUNHAP_CHIUTHUE8)
            End If
            If _filter.TT_GTGC_NV8.HasValue Then
                lstObj = lstObj.Where(Function(f) f.TT_GTGC_NV8 = _filter.TT_GTGC_NV8)
            End If
            If _filter.TT_SUM_BHXH8.HasValue Then
                lstObj = lstObj.Where(Function(f) f.TT_SUM_BHXH8 = _filter.TT_SUM_BHXH8)
            End If
            If _filter.TT_THUNHAP_TINHTHUE8.HasValue Then
                lstObj = lstObj.Where(Function(f) f.TT_THUNHAP_TINHTHUE8 = _filter.TT_THUNHAP_TINHTHUE8)
            End If
            If _filter.TT_THUETNCN8.HasValue Then
                lstObj = lstObj.Where(Function(f) f.TT_THUETNCN8 = _filter.TT_THUETNCN8)
            End If

            If _filter.TT_THUNHAP_CHIUTHUE9.HasValue Then
                lstObj = lstObj.Where(Function(f) f.TT_THUNHAP_CHIUTHUE9 = _filter.TT_THUNHAP_CHIUTHUE9)
            End If
            If _filter.TT_GTGC_NV9.HasValue Then
                lstObj = lstObj.Where(Function(f) f.TT_GTGC_NV9 = _filter.TT_GTGC_NV9)
            End If
            If _filter.TT_SUM_BHXH9.HasValue Then
                lstObj = lstObj.Where(Function(f) f.TT_SUM_BHXH9 = _filter.TT_SUM_BHXH9)
            End If
            If _filter.TT_THUNHAP_TINHTHUE9.HasValue Then
                lstObj = lstObj.Where(Function(f) f.TT_THUNHAP_TINHTHUE9 = _filter.TT_THUNHAP_TINHTHUE9)
            End If
            If _filter.TT_THUETNCN9.HasValue Then
                lstObj = lstObj.Where(Function(f) f.TT_THUETNCN9 = _filter.TT_THUETNCN9)
            End If

            If _filter.TT_THUNHAP_CHIUTHUE10.HasValue Then
                lstObj = lstObj.Where(Function(f) f.TT_THUNHAP_CHIUTHUE10 = _filter.TT_THUNHAP_CHIUTHUE10)
            End If
            If _filter.TT_GTGC_NV10.HasValue Then
                lstObj = lstObj.Where(Function(f) f.TT_GTGC_NV10 = _filter.TT_GTGC_NV10)
            End If
            If _filter.TT_SUM_BHXH10.HasValue Then
                lstObj = lstObj.Where(Function(f) f.TT_SUM_BHXH10 = _filter.TT_SUM_BHXH10)
            End If
            If _filter.TT_THUNHAP_TINHTHUE10.HasValue Then
                lstObj = lstObj.Where(Function(f) f.TT_THUNHAP_TINHTHUE10 = _filter.TT_THUNHAP_TINHTHUE10)
            End If
            If _filter.TT_THUETNCN10.HasValue Then
                lstObj = lstObj.Where(Function(f) f.TT_THUETNCN10 = _filter.TT_THUETNCN10)
            End If

            If _filter.TT_THUNHAP_CHIUTHUE11.HasValue Then
                lstObj = lstObj.Where(Function(f) f.TT_THUNHAP_CHIUTHUE11 = _filter.TT_THUNHAP_CHIUTHUE11)
            End If
            If _filter.TT_GTGC_NV11.HasValue Then
                lstObj = lstObj.Where(Function(f) f.TT_GTGC_NV11 = _filter.TT_GTGC_NV11)
            End If
            If _filter.TT_SUM_BHXH11.HasValue Then
                lstObj = lstObj.Where(Function(f) f.TT_SUM_BHXH11 = _filter.TT_SUM_BHXH11)
            End If
            If _filter.TT_THUNHAP_TINHTHUE11.HasValue Then
                lstObj = lstObj.Where(Function(f) f.TT_THUNHAP_TINHTHUE11 = _filter.TT_THUNHAP_TINHTHUE11)
            End If
            If _filter.TT_THUETNCN11.HasValue Then
                lstObj = lstObj.Where(Function(f) f.TT_THUETNCN11 = _filter.TT_THUETNCN11)
            End If

            If _filter.TT_THUNHAP_CHIUTHUE12.HasValue Then
                lstObj = lstObj.Where(Function(f) f.TT_THUNHAP_CHIUTHUE12 = _filter.TT_THUNHAP_CHIUTHUE12)
            End If
            If _filter.TT_GTGC_NV12.HasValue Then
                lstObj = lstObj.Where(Function(f) f.TT_GTGC_NV12 = _filter.TT_GTGC_NV12)
            End If
            If _filter.TT_SUM_BHXH12.HasValue Then
                lstObj = lstObj.Where(Function(f) f.TT_SUM_BHXH12 = _filter.TT_SUM_BHXH12)
            End If
            If _filter.TT_THUNHAP_TINHTHUE12.HasValue Then
                lstObj = lstObj.Where(Function(f) f.TT_THUNHAP_TINHTHUE12 = _filter.TT_THUNHAP_TINHTHUE12)
            End If
            If _filter.TT_THUETNCN12.HasValue Then
                lstObj = lstObj.Where(Function(f) f.TT_THUETNCN12 = _filter.TT_THUETNCN12)
            End If

            If _filter.TT_TAMUNG_LUONG12.HasValue Then
                lstObj = lstObj.Where(Function(f) f.TT_TAMUNG_LUONG12 = _filter.TT_TAMUNG_LUONG12)
            End If
            If _filter.M12_THUNHAP_CHIUTHUE.HasValue Then
                lstObj = lstObj.Where(Function(f) f.M12_THUNHAP_CHIUTHUE = _filter.M12_THUNHAP_CHIUTHUE)
            End If
            If _filter.M12_GTGC_NV.HasValue Then
                lstObj = lstObj.Where(Function(f) f.M12_GTGC_NV = _filter.M12_GTGC_NV)
            End If
            If _filter.M12_SUM_BHXH.HasValue Then
                lstObj = lstObj.Where(Function(f) f.M12_SUM_BHXH = _filter.M12_SUM_BHXH)
            End If
            If _filter.M12_THUNHAP_TINHTHUE.HasValue Then
                lstObj = lstObj.Where(Function(f) f.M12_THUNHAP_TINHTHUE = _filter.M12_THUNHAP_TINHTHUE)
            End If
            If _filter.M12_THUETNCN.HasValue Then
                lstObj = lstObj.Where(Function(f) f.M12_THUETNCN = _filter.M12_THUETNCN)
            End If

            If _filter.TT_CHIUTHUE_TOTAL.HasValue Then
                lstObj = lstObj.Where(Function(f) f.TT_CHIUTHUE_TOTAL = _filter.TT_CHIUTHUE_TOTAL)
            End If

            If _filter.TT_BHXH_TOTAL.HasValue Then
                lstObj = lstObj.Where(Function(f) f.TT_BHXH_TOTAL = _filter.TT_BHXH_TOTAL)
            End If

            If _filter.TT_THUETNCN_TOTAL.HasValue Then
                lstObj = lstObj.Where(Function(f) f.TT_THUETNCN_TOTAL = _filter.TT_THUETNCN_TOTAL)
            End If

            lstObj = lstObj.OrderBy(Sorts)
            Total = lstObj.Count
            lstObj = lstObj.Skip(PageIndex * PageSize).Take(PageSize)

            Return lstObj.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function ChangeStatusSalPITCode(ByVal lstID As List(Of Decimal), ByVal _status As Decimal, ByVal _log As UserLog) As Boolean
        Try
            Dim lstObj = From p In Context.PA_SALARY_PITCODE Where lstID.Contains(p.ID)

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

    Public Function DeleteSalPITCode(ByVal lstID As List(Of Decimal)) As Boolean
        Try
            Dim lstObj = From p In Context.PA_SALARY_PITCODE Where lstID.Contains(p.ID)

            For Each item In lstObj
                Context.PA_SALARY_PITCODE.DeleteObject(item)
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function CAL_SAL_PITCODE(ByVal _org_id As List(Of Decimal), ByVal P_YEAR As Decimal, ByVal log As UserLog) As Boolean
        Try
            Dim strOrg = String.Join(",", _org_id)
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PA_SETTING.CAL_SAL_PITCODE",
                                               New With {.P_ORG_ID = strOrg,
                                                         .P_YEAR = P_YEAR,
                                                         .P_USER = log.Username.ToUpper,
                                                         .P_OUT = cls.OUT_CURSOR}, True)
                Return CBool(dtData(0)(0))
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

#End Region

#Region "Tax Income Year"
    Public Function GetListTaxIncomeYear(ByVal _filter As PA_TAXINCOME_YEARDTO, ByVal lstOrg As List(Of Decimal),
                                         ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer,
                                         Optional ByVal Sorts As String = "EMPLOYEE_CODE desc",
                                         Optional ByVal log As UserLog = Nothing) As List(Of PA_TAXINCOME_YEARDTO)

        Try
            Dim strOrg = String.Join(",", lstOrg)

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG2",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = strOrg})
            End Using

            Dim query = From p In Context.PA_TAXINCOME_YEAR
                        From ecv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.PIT_CODE = p.PIT_CODE)
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = ecv.EMPLOYEE_ID)
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From pe In Context.AT_PERIOD.Where(Function(f) f.ID = p.PERIOD_ID).DefaultIfEmpty
                        From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = e.ORG_ID And f.USERNAME = log.Username.ToUpper)


            ' select thuộc tính
            Dim lstObj = query.Select(Function(p) New PA_TAXINCOME_YEARDTO With {.EMPLOYEE_ID = p.e.ID,
                                                                                .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                                                                .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                                                                                .ID = p.p.ID,
                                                                                .ORG_ID = p.e.ORG_ID,
                                                                                .ORG_NAME = p.o.NAME_VN,
                                                                                .TITLE_ID = p.e.TITLE_ID,
                                                                                .TITLE_NAME = p.t.NAME_VN,
                                                                                .YEAR = p.p.YEAR,
                                                                                .PIT_CODE = p.p.PIT_CODE,
                                                                                .PERIOD_ID = p.p.PERIOD_ID,
                                                                                .PERIOD_NAME = p.pe.PERIOD_T,
                                                                                .BHXH = p.p.BHXH,
                                                                                .NPT = p.p.NPT,
                                                                                .NPT_MONTH = p.p.NPT_MONTH,
                                                                                .DM_GTBT = p.p.DM_GTBT,
                                                                                .DM_NPT = p.p.DM_NPT,
                                                                                .GTGC = p.p.GTGC,
                                                                                .CHIUTHUE_YEAR = p.p.CHIUTHUE_YEAR,
                                                                                .THUETNCN_YEAR = p.p.THUETNCN_YEAR,
                                                                                .CHIUTHUE_BOSUNG = p.p.CHIUTHUE_BOSUNG,
                                                                                .THUETNCN_BOSUNG = p.p.THUETNCN_BOSUNG,
                                                                                .THUNHAP_CHIUTHUE = p.p.THUNHAP_CHIUTHUE,
                                                                                .THUNHAP_QTT = p.p.THUNHAP_QTT,
                                                                                .THUETNCN_QTT = p.p.THUETNCN_QTT,
                                                                                .THUETNCN_PAY = p.p.THUETNCN_PAY,
                                                                                .THUETNCN_REFUND = p.p.THUETNCN_REFUND,
                                                                                .IS_LOCK = p.p.IS_LOCK,
                                                                                .CREATED_DATE = p.p.CREATED_DATE,
                                                                                .TOTAL_THUE_TNCN = p.p.THUETNCN_YEAR.Value + p.p.THUETNCN_BOSUNG.Value})

            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                lstObj = lstObj.Where(Function(f) f.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_NAME) Then
                lstObj = lstObj.Where(Function(f) f.EMPLOYEE_NAME.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                lstObj = lstObj.Where(Function(f) f.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                lstObj = lstObj.Where(Function(f) f.TITLE_NAME.ToUpper.Contains(_filter.TITLE_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.PIT_CODE) Then
                lstObj = lstObj.Where(Function(f) f.PIT_CODE.ToUpper.Contains(_filter.PIT_CODE.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.PERIOD_NAME) Then
                lstObj = lstObj.Where(Function(f) f.PERIOD_NAME.ToUpper.Contains(_filter.PERIOD_NAME.ToUpper))
            End If

            If _filter.BHXH.HasValue Then
                lstObj = lstObj.Where(Function(f) f.BHXH = _filter.BHXH)
            End If
            If _filter.NPT.HasValue Then
                lstObj = lstObj.Where(Function(f) f.NPT = _filter.NPT)
            End If
            If _filter.NPT_MONTH.HasValue Then
                lstObj = lstObj.Where(Function(f) f.NPT_MONTH = _filter.NPT_MONTH)
            End If
            If _filter.DM_GTBT.HasValue Then
                lstObj = lstObj.Where(Function(f) f.DM_GTBT = _filter.DM_GTBT)
            End If
            If _filter.DM_NPT.HasValue Then
                lstObj = lstObj.Where(Function(f) f.DM_NPT = _filter.DM_NPT)
            End If

            If _filter.GTGC.HasValue Then
                lstObj = lstObj.Where(Function(f) f.GTGC = _filter.GTGC)
            End If
            If _filter.CHIUTHUE_YEAR.HasValue Then
                lstObj = lstObj.Where(Function(f) f.CHIUTHUE_YEAR = _filter.CHIUTHUE_YEAR)
            End If
            If _filter.THUETNCN_YEAR.HasValue Then
                lstObj = lstObj.Where(Function(f) f.THUETNCN_YEAR = _filter.THUETNCN_YEAR)
            End If
            If _filter.CHIUTHUE_BOSUNG.HasValue Then
                lstObj = lstObj.Where(Function(f) f.CHIUTHUE_BOSUNG = _filter.CHIUTHUE_BOSUNG)
            End If
            If _filter.THUETNCN_BOSUNG.HasValue Then
                lstObj = lstObj.Where(Function(f) f.THUETNCN_BOSUNG = _filter.THUETNCN_BOSUNG)
            End If

            If _filter.THUNHAP_CHIUTHUE.HasValue Then
                lstObj = lstObj.Where(Function(f) f.THUNHAP_CHIUTHUE = _filter.THUNHAP_CHIUTHUE)
            End If
            If _filter.THUNHAP_QTT.HasValue Then
                lstObj = lstObj.Where(Function(f) f.THUNHAP_QTT = _filter.THUNHAP_QTT)
            End If
            If _filter.THUETNCN_QTT.HasValue Then
                lstObj = lstObj.Where(Function(f) f.THUETNCN_QTT = _filter.THUETNCN_QTT)
            End If
            If _filter.THUETNCN_PAY.HasValue Then
                lstObj = lstObj.Where(Function(f) f.THUETNCN_PAY = _filter.THUETNCN_PAY)
            End If
            If _filter.THUETNCN_REFUND.HasValue Then
                lstObj = lstObj.Where(Function(f) f.THUETNCN_REFUND = _filter.THUETNCN_REFUND)
            End If

            lstObj = lstObj.OrderBy(Sorts)
            Total = lstObj.Count
            lstObj = lstObj.Skip(PageIndex * PageSize).Take(PageSize)

            Return lstObj.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function ChangeStatusTaxIncomeYear(ByVal lstID As List(Of Decimal), ByVal _status As Decimal, ByVal _log As UserLog) As Boolean
        Try
            Dim lstObj = From p In Context.PA_TAXINCOME_YEAR Where lstID.Contains(p.ID)

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

    Public Function DeleteTaxIncomeYear(ByVal lstID As List(Of Decimal)) As Boolean
        Try
            Dim lstObj = From p In Context.PA_TAXINCOME_YEAR Where lstID.Contains(p.ID)

            For Each item In lstObj
                Context.PA_TAXINCOME_YEAR.DeleteObject(item)
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function UpdateTaxIncomeYearPeriod(ByVal lstID As List(Of Decimal), ByVal _period As Decimal, ByVal _log As UserLog) As Boolean
        Try
            Dim lstObj = From p In Context.PA_TAXINCOME_YEAR Where lstID.Contains(p.ID)

            For Each item In lstObj
                item.PERIOD_ID = _period
            Next
            Context.SaveChanges(_log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function CAL_TAX_INCOME_YEAR(ByVal _org_id As List(Of Decimal), ByVal P_YEAR As Decimal, ByVal log As UserLog) As Boolean
        Try
            Dim strOrg = String.Join(",", _org_id)
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PA_SETTING.CAL_TAX_INCOME_YEAR",
                                               New With {.P_ORG_ID = strOrg,
                                                         .P_YEAR = P_YEAR,
                                                         .P_USER = log.Username.ToUpper,
                                                         .P_OUT = cls.OUT_CURSOR}, True)
                Return CBool(dtData(0)(0))
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

#End Region

#Region "Document PIT"
    Public Function GetDocumentPITs(ByVal _filter As PA_DOCUMENT_PITDTO, ByVal lstOrg As List(Of Decimal),
                                         ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer,
                                         Optional ByVal Sorts As String = "EMPLOYEE_CODE desc",
                                         Optional ByVal log As UserLog = Nothing) As List(Of PA_DOCUMENT_PITDTO)

        Try
            Dim strOrg = String.Join(",", lstOrg)

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG2",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = strOrg})
            End Using

            Dim query = From p In Context.PA_DOCUMENT_PIT
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From ecv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = e.ID)
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From ge In Context.OT_OTHER_LIST.Where(Function(f) f.ID = ecv.GENDER).DefaultIfEmpty
                        From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = e.ORG_ID And f.USERNAME = log.Username.ToUpper)


            ' select thuộc tính
            Dim lstObj = query.Select(Function(p) New PA_DOCUMENT_PITDTO With {.ID = p.p.ID,
                                                                               .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                                                               .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                                                               .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                                                                               .PIT_CODE = p.ecv.PIT_CODE,
                                                                               .PIT_NO = p.p.PIT_NO,
                                                                               .YEAR = p.p.YEAR,
                                                                               .LIEN1 = p.p.LIEN1,
                                                                               .LIEN1_STATUS = If(p.p.LIEN1 = 1, "Đã in", If(p.p.LIEN1 = 2, "Hủy", "")),
                                                                               .LIEN1_DATE = p.p.LIEN1_DATE,
                                                                               .LIEN2 = p.p.LIEN2,
                                                                               .LIEN2_STATUS = If(p.p.LIEN2 = 1, "Đã in", If(p.p.LIEN2 = 2, "Hủy", "")),
                                                                               .LIEN2_DATE = p.p.LIEN2_DATE,
                                                                               .TYPE_INCOME = p.p.TYPE_INCOME,
                                                                               .PERIOD_REPLY = p.p.PERIOD_REPLY,
                                                                               .TAXABLE_INCOME = p.p.TAXABLE_INCOME,
                                                                               .MONEY_PIT = p.p.MONEY_PIT,
                                                                               .REST_INCOME = p.p.REST_INCOME,
                                                                               .BIRTH_DATE = p.ecv.BIRTH_DATE,
                                                                               .GENDER = p.ge.NAME_VN,
                                                                               .ORG_ID = p.e.ORG_ID,
                                                                               .ORG_NAME = p.o.NAME_VN,
                                                                               .CREATED_DATE = p.p.CREATED_DATE})

            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE_SEARCH) Then
                lstObj = lstObj.Where(Function(f) f.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE_SEARCH.ToUpper))
            End If

            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                lstObj = lstObj.Where(Function(f) f.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If

            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_NAME) Then
                lstObj = lstObj.Where(Function(f) f.EMPLOYEE_NAME.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                lstObj = lstObj.Where(Function(f) f.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.PIT_CODE) Then
                lstObj = lstObj.Where(Function(f) f.PIT_CODE.ToUpper.Contains(_filter.PIT_CODE.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.PIT_NO_SEARCH) Then
                lstObj = lstObj.Where(Function(f) f.PIT_NO.ToUpper.Contains(_filter.PIT_NO_SEARCH.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.PIT_NO) Then
                lstObj = lstObj.Where(Function(f) f.PIT_NO.ToUpper.Contains(_filter.PIT_NO.ToUpper))
            End If
            If _filter.YEAR.HasValue Then
                lstObj = lstObj.Where(Function(f) f.YEAR = _filter.YEAR)
            End If
            If Not String.IsNullOrEmpty(_filter.LIEN1_STATUS) Then
                lstObj = lstObj.Where(Function(f) f.LIEN1_STATUS.ToUpper.Contains(_filter.LIEN1_STATUS.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.LIEN2_STATUS) Then
                lstObj = lstObj.Where(Function(f) f.LIEN2_STATUS.ToUpper.Contains(_filter.LIEN2_STATUS.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.TYPE_INCOME) Then
                lstObj = lstObj.Where(Function(f) f.TYPE_INCOME.ToUpper.Contains(_filter.TYPE_INCOME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.PERIOD_REPLY) Then
                lstObj = lstObj.Where(Function(f) f.PERIOD_REPLY.ToUpper.Contains(_filter.PERIOD_REPLY.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.GENDER) Then
                lstObj = lstObj.Where(Function(f) f.GENDER.ToUpper.Contains(_filter.GENDER.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                lstObj = lstObj.Where(Function(f) f.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If

            If _filter.LIEN1_DATE.HasValue Then
                lstObj = lstObj.Where(Function(f) f.LIEN1_DATE = _filter.LIEN1_DATE)
            End If
            If _filter.LIEN2_DATE.HasValue Then
                lstObj = lstObj.Where(Function(f) f.LIEN2_DATE = _filter.LIEN2_DATE)
            End If
            If _filter.TAXABLE_INCOME.HasValue Then
                lstObj = lstObj.Where(Function(f) f.TAXABLE_INCOME = _filter.TAXABLE_INCOME)
            End If
            If _filter.MONEY_PIT.HasValue Then
                lstObj = lstObj.Where(Function(f) f.MONEY_PIT = _filter.MONEY_PIT)
            End If
            If _filter.REST_INCOME.HasValue Then
                lstObj = lstObj.Where(Function(f) f.REST_INCOME = _filter.REST_INCOME)
            End If

            If _filter.BIRTH_DATE.HasValue Then
                lstObj = lstObj.Where(Function(f) f.BIRTH_DATE = _filter.BIRTH_DATE)
            End If

            If _filter.LIEN1_FROMDATE.HasValue Then
                lstObj = lstObj.Where(Function(f) f.LIEN1_DATE >= _filter.LIEN1_FROMDATE)
            End If

            If _filter.LIEN1_TODATE.HasValue Then
                lstObj = lstObj.Where(Function(f) f.LIEN1_DATE <= _filter.LIEN1_TODATE)
            End If

            If _filter.LIEN2_FROMDATE.HasValue Then
                lstObj = lstObj.Where(Function(f) f.LIEN2_DATE >= _filter.LIEN2_FROMDATE)
            End If

            If _filter.LIEN2_TODATE.HasValue Then
                lstObj = lstObj.Where(Function(f) f.LIEN2_DATE <= _filter.LIEN2_TODATE)
            End If

            lstObj = lstObj.OrderBy(Sorts)
            Total = lstObj.Count
            lstObj = lstObj.Skip(PageIndex * PageSize).Take(PageSize)

            Return lstObj.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function GET_EMPLOYEE_PIT_INFO(ByVal P_EMP_ID As Decimal, ByVal P_YEAR As Decimal, ByVal P_ID As Decimal) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PA_SETTING.GET_EMPLOYEE_PIT_INFO",
                                               New With {.P_EMP_ID = P_EMP_ID,
                                                         .P_YEAR = P_YEAR,
                                                         .P_ID = P_ID,
                                                         .P_OUT = cls.OUT_CURSOR}, True)
                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function InsertDocumentPIT(ByVal _objData As PA_DOCUMENT_PITDTO, ByVal userLog As UserLog) As Boolean
        Try
            Dim obj As New PA_DOCUMENT_PIT
            obj.ID = Utilities.GetNextSequence(Context, Context.PA_DOCUMENT_PIT.EntitySet.Name)
            obj.YEAR = _objData.YEAR
            obj.EMPLOYEE_ID = _objData.EMPLOYEE_ID
            obj.PIT_NO = _objData.PIT_NO
            obj.SERIAL_NO = _objData.SERIAL_NO
            obj.LIEN1 = _objData.LIEN1
            obj.LIEN2 = _objData.LIEN2
            obj.CUTRU = _objData.CUTRU
            obj.PERIOD_REPLY = _objData.PERIOD_REPLY
            obj.TYPE_INCOME = _objData.TYPE_INCOME
            obj.TAXABLE_INCOME = _objData.TAXABLE_INCOME
            obj.MONEY_PIT = _objData.MONEY_PIT
            obj.REST_INCOME = _objData.REST_INCOME
            Context.PA_DOCUMENT_PIT.AddObject(obj)
            Context.SaveChanges(userLog)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function ModifyDocumentPIT(ByVal _objData As PA_DOCUMENT_PITDTO, ByVal userLog As UserLog) As Boolean
        Try
            Dim obj = (From o In Context.PA_DOCUMENT_PIT Where o.ID = _objData.ID).FirstOrDefault
            obj.YEAR = _objData.YEAR
            obj.EMPLOYEE_ID = _objData.EMPLOYEE_ID
            obj.PIT_NO = _objData.PIT_NO
            obj.SERIAL_NO = _objData.SERIAL_NO
            obj.LIEN1 = _objData.LIEN1
            obj.LIEN2 = _objData.LIEN2
            obj.CUTRU = _objData.CUTRU
            obj.PERIOD_REPLY = _objData.PERIOD_REPLY
            obj.TYPE_INCOME = _objData.TYPE_INCOME
            obj.TAXABLE_INCOME = _objData.TAXABLE_INCOME
            obj.MONEY_PIT = _objData.MONEY_PIT
            obj.REST_INCOME = _objData.REST_INCOME
            Context.SaveChanges(userLog)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function DeleteDocumentPIT(ByVal _lstID As List(Of Decimal)) As Boolean
        Try
            Dim lstOBJ = (From p In Context.PA_DOCUMENT_PIT Where _lstID.Contains(p.ID))
            For Each item In lstOBJ
                Context.PA_DOCUMENT_PIT.DeleteObject(item)
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function ValidateDocumentPIT(ByVal _objData As PA_DOCUMENT_PITDTO) As Boolean
        Try
            Dim query = (From p In Context.PA_DOCUMENT_PIT Where p.ID <> _objData.ID AndAlso _objData.PIT_NO.Equals(p.PIT_NO)).Any
            Return query
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function ChangePITPrintStatus(ByVal lstID As List(Of Decimal), ByVal _type As String, ByVal pit_type As String, ByVal userLog As UserLog) As Boolean
        Try
            Dim items = (From p In Context.PA_DOCUMENT_PIT Where lstID.Contains(p.ID))
            If _type = "PRINT" Then
                If pit_type = "LIEN1" Then
                    For Each item In items
                        item.LIEN1 = 1
                        item.LIEN1_DATE = DateTime.Now
                    Next
                Else
                    For Each item In items
                        item.LIEN2 = 1
                        item.LIEN2_DATE = DateTime.Now
                    Next
                End If
            ElseIf _type = "CANCEL" Then
                For Each item In items
                    item.LIEN1 = 2
                Next
            End If
            Context.SaveChanges(userLog)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

#End Region

#Region "Payroll Advance"
    Public Function CAL_PAYROLL_ADVANCE(ByVal _period_ID As Decimal,
                                        ByVal _start_date As Date,
                                        ByVal _end_date As Date,
                                        ByVal _end_date_period As Date,
                                        ByVal _OrgID As Decimal,
                                        ByVal _IsDissolve As Decimal,
                                        ByVal _Sal As Decimal,
                                        ByVal _Nosal As Decimal,
                                        ByVal log As UserLog) As Boolean
        Try
            Using Sql As New DataAccess.NonQueryData
                Sql.ExecuteStore("PKG_PA_BUSINESS.CAL_PAYROLL_ADVANCE",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORG_ID = _OrgID,
                                           .P_ISDISSOLVE = _IsDissolve,
                                           .P_PERIOD_ID = _period_ID,
                                           .P_START_DATE = _start_date,
                                           .P_END_DATE = _end_date,
                                           .P_END_DATE_PERIOD = _end_date_period,
                                           .P_WORKING_HAVE_SALARY = _Sal,
                                           .P_WORKING_NO_SALARY = _Nosal})
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Return False
        End Try
    End Function

    Public Function GetPayrollAdvance(ByVal _filter As PayrollAdvanceDTO, ByVal _param As ParamDTO,
                                     ByVal PageIndex As Integer,
                                     ByVal PageSize As Integer,
                                     ByRef Total As Integer,
                                     Optional ByVal Sorts As String = "LOCK_NAME desc",
                                     Optional ByVal log As UserLog = Nothing) As List(Of PayrollAdvanceDTO)

        Try

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using

            Dim query = From p In Context.PA_SALARY_ADVANCE
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = e.ORG_ID And f.USERNAME = log.Username.ToUpper)
                        From period In Context.AT_PERIOD.Where(Function(f) p.PERIOD_ID = f.ID).DefaultIfEmpty
                        Where p.WORKING_HAVE_SALARY <= _filter.HAVE_SAL AndAlso p.WORKING_NO_SALARY <= _filter.NOT_SAL And p.PERIOD_ID = _filter.PERIOD_ID

            ' select thuộc tính
            Dim lstObj = query.Select(Function(p) New PayrollAdvanceDTO With {.EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                                                                   .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                                                                   .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                                                                                   .ORG_ID = p.e.ORG_ID,
                                                                                   .ORG_NAME = p.o.NAME_VN,
                                                                                   .TITLE_ID = p.e.TITLE_ID,
                                                                                   .TITLE_NAME = p.t.NAME_VN,
                                                                                   .PERIOD_ID = p.p.PERIOD_ID,
                                                                                   .IS_LOCK = p.p.IS_LOCK,
                                                                                   .LOCK_NAME = If(p.p.IS_LOCK = 1, "X", ""),
                                                                                   .SALARY_CONTRACT = p.p.SALARY_CONTRACT,
                                                                                   .WORK_STANDARD = p.p.WORK_STANDARD,
                                                                                   .WORKING_X = p.p.WORKING_X,
                                                                                   .WORKING_HAVE_SALARY = p.p.WORKING_HAVE_SALARY,
                                                                                   .WORKING_NO_SALARY = p.p.WORKING_NO_SALARY,
                                                                                   .PERIOD_T = p.period.PERIOD_T,
                                                                                   .SALARY_ADVANCE = p.p.SALARY_ADVANCE})

            If Not String.IsNullOrEmpty(_filter.PERIOD_T) Then
                lstObj = lstObj.Where(Function(f) f.PERIOD_T.ToUpper.Contains(_filter.PERIOD_T.ToUpper))
            End If

            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                lstObj = lstObj.Where(Function(f) f.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If

            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_NAME) Then
                lstObj = lstObj.Where(Function(f) f.EMPLOYEE_NAME.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                lstObj = lstObj.Where(Function(f) f.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                lstObj = lstObj.Where(Function(f) f.TITLE_NAME.ToUpper.Contains(_filter.TITLE_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.LOCK_NAME) Then
                lstObj = lstObj.Where(Function(f) f.LOCK_NAME.ToUpper.Contains(_filter.LOCK_NAME.ToUpper))
            End If

            If _filter.SALARY_CONTRACT.HasValue Then
                lstObj = lstObj.Where(Function(f) f.SALARY_CONTRACT = _filter.SALARY_CONTRACT)
            End If

            If _filter.SALARY_ADVANCE.HasValue Then
                lstObj = lstObj.Where(Function(f) f.SALARY_ADVANCE = _filter.SALARY_ADVANCE)
            End If

            If _filter.WORK_STANDARD.HasValue Then
                lstObj = lstObj.Where(Function(f) f.WORK_STANDARD = _filter.WORK_STANDARD)
            End If

            If _filter.WORKING_X.HasValue Then
                lstObj = lstObj.Where(Function(f) f.WORKING_X = _filter.WORKING_X)
            End If

            If _filter.WORKING_HAVE_SALARY.HasValue Then
                lstObj = lstObj.Where(Function(f) f.WORKING_HAVE_SALARY = _filter.WORKING_HAVE_SALARY)
            End If

            If _filter.WORKING_NO_SALARY.HasValue Then
                lstObj = lstObj.Where(Function(f) f.WORKING_NO_SALARY = _filter.WORKING_NO_SALARY)
            End If

            If _filter.IS_LOCK <> 2 Then
                lstObj = lstObj.Where(Function(f) f.IS_LOCK = _filter.IS_LOCK)
            End If

            lstObj = lstObj.OrderBy(Sorts)
            Total = lstObj.Count
            lstObj = lstObj.Skip(PageIndex * PageSize).Take(PageSize)

            Return lstObj.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function


    Public Function DeletePayrollAdvance(ByVal _lstID As List(Of Decimal), ByVal peroid As Decimal) As Boolean
        Try
            Dim lstOBJ = (From p In Context.PA_SALARY_ADVANCE Where p.PERIOD_ID = peroid And _lstID.Contains(p.EMPLOYEE_ID))
            For Each item In lstOBJ
                Context.PA_SALARY_ADVANCE.DeleteObject(item)
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function ActivePayrollAdvance(ByVal _lstID As List(Of Decimal), ByVal _param As ParamDTO, ByVal peroid As Decimal, ByVal status As Decimal, ByVal Log As UserLog) As Boolean
        Try
            If _param.ORG_ID = 0 Then
                For Each item In _lstID
                    Dim obj = (From o In Context.PA_SALARY_ADVANCE Where o.EMPLOYEE_ID = item AndAlso o.PERIOD_ID = peroid).FirstOrDefault
                    obj.IS_LOCK = status
                Next
            Else
                Using cls As New DataAccess.QueryData
                    cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                     New With {.P_USERNAME = Log.Username,
                                               .P_ORGID = _param.ORG_ID,
                                               .P_ISDISSOLVE = _param.IS_DISSOLVE})
                End Using

                Dim query = From p In Context.PA_SALARY_ADVANCE
                            From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                            From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = e.ORG_ID And f.USERNAME = Log.Username.ToUpper)
                            Where p.PERIOD_ID = peroid

                Dim lstObj = query.Select(Function(p) New PayrollAdvanceDTO With {.EMPLOYEE_ID = p.p.EMPLOYEE_ID})
                For Each item In lstObj
                    Dim obj = (From o In Context.PA_SALARY_ADVANCE Where o.EMPLOYEE_ID = item.EMPLOYEE_ID AndAlso o.PERIOD_ID = peroid).FirstOrDefault
                    obj.IS_LOCK = status
                Next
            End If

            Context.SaveChanges(Log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function ModifyPayrollAdvance(ByVal _lstID As List(Of Decimal), ByVal peroid As Decimal, ByVal salAdvance As Decimal, ByVal Log As UserLog) As Boolean
        Try
            For Each item In _lstID
                Dim obj = (From o In Context.PA_SALARY_ADVANCE Where o.EMPLOYEE_ID = item AndAlso o.PERIOD_ID = peroid And o.IS_LOCK = 0).FirstOrDefault
                obj.SALARY_ADVANCE = salAdvance
            Next

            Context.SaveChanges(Log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

#End Region

End Class

