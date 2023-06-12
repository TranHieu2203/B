Imports System.Reflection
Imports Aspose.Cells
Imports Aspose.Words
Imports Framework.Data
Imports Framework.Data.System.Linq.Dynamic
Imports Framework.Data.SystemConfig

Public Class PayrollRepository
    Inherits PayrollRepositoryBase

    Public Function GetConfig(ByVal eModule As ModuleID) As Dictionary(Of String, String)
        Using config As New SystemConfig
            Try
                Return config.GetConfig(eModule)
            Catch ex As Exception
                WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
                Throw ex
            End Try
        End Using

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
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Gets the combobox data.
    ''' </summary>
    ''' <param name="cbxData">The CBX data.</param>
    ''' <returns></returns>
    Public Function GetComboboxData(ByRef cbxData As ComboBoxDataDTO) As Boolean
        Try

            'Danh sách nhóm ký hiệu lương
            If cbxData.GET_GROUP_TYPE Then
                cbxData.LIST_GROUP_TYPE = (From p In Context.OT_OTHER_LIST Join t In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals t.ID
                                           Where p.ACTFLG = "A" And t.CODE = "SAL_TYPE" Order By p.CREATED_DATE Descending
                                           Select New OT_OTHERLIST_DTO With {
                                               .ID = p.ID,
                                               .CODE = p.CODE,
                                               .NAME_EN = p.NAME_EN,
                                               .NAME_VN = p.NAME_VN,
                                               .TYPE_ID = p.TYPE_ID
                                           }).ToList
            End If
            'Danh loại bảng lương
            If cbxData.GET_TYPE_PAYMENT Then
                cbxData.LIST_TYPE_PAYMENT = (From p In Context.OT_OTHER_LIST Join t In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals t.ID
                                             Where p.ACTFLG = "A" And t.CODE = "TYPE_PAYMENT" Order By p.CREATED_DATE Descending
                                             Select New OT_OTHERLIST_DTO With {
                                                 .ID = p.ID,
                                                 .CODE = p.CODE,
                                                 .NAME_EN = p.NAME_EN,
                                                 .NAME_VN = p.NAME_VN,
                                                 .TYPE_ID = p.TYPE_ID
                                             }).ToList
            End If
            'Danh sách các đối tượng cư trú
            If cbxData.GET_LIST_RESIDENT Then
                cbxData.LIST_LIST_RESIDENT = (From p In Context.OT_OTHER_LIST Join t In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals t.ID
                                              Where p.ACTFLG = "A" And t.CODE = "PA_RESIDENT" Order By p.CREATED_DATE Descending
                                              Select New OT_OTHERLIST_DTO With {
                                                  .ID = p.ID,
                                                  .CODE = p.CODE,
                                                  .NAME_EN = p.NAME_EN,
                                                  .NAME_VN = p.NAME_VN,
                                     .TYPE_ID = p.TYPE_ID
                                              }).ToList
            End If
            ' Danh sách các khoản tiền 
            If cbxData.GET_LIST_PAYMENT Then
                cbxData.LIST_LIST_PAYMENT = (From p In Context.OT_OTHER_LIST Join t In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals t.ID
                                             Where p.ACTFLG = "A" And t.CODE = "PA_LISTPAYMENT" Order By p.CREATED_DATE Descending
                                             Select New OT_OTHERLIST_DTO With {
                                                 .ID = p.ID,
                                                 .CODE = p.CODE,
                                                 .NAME_EN = p.NAME_EN,
                                                 .NAME_VN = p.NAME_VN,
                                    .TYPE_ID = p.TYPE_ID
                                             }).ToList
            End If
            ' Danh sách các khoản tiền trong bảng lương
            'TYPE_PAYMENT = 4123 là danh mục bảng lương tổng hợp
            If cbxData.GET_LIST_SALARY Then
                cbxData.LIST_LIST_SALARY = (From p In Context.PA_LISTSALARIES
                                            Where p.STATUS = "A" And p.TYPE_PAYMENT = 4123 Order By p.CREATED_DATE Descending
                                            Select New PAListSalariesDTO With {
                                                .ID = p.ID,
                                                .NAME_EN = p.NAME_EN,
                                                .NAME_VN = p.NAME_VN
                                            }).ToList
            End If
            'Danh sách các đối tượng lương(bảng lương)
            If cbxData.GET_OBJECT_PAYMENT Then
                cbxData.LIST_OBJECT_PAYMENT = (From p In Context.PA_OBJECT_SALARY
                                               Where p.ACTFLG = "A" Order By p.DEFAULT_OBJ_SALARY
                                               Select New PAObjectSalaryDTO With {
                             .ID = p.ID,
                             .CODE = p.CODE,
                             .NAME_VN = p.NAME_VN,
                             .NAME_EN = p.NAME_EN
                         }).ToList
            End If
            ''''''''''''''''''''''
            If cbxData.GET_SALARY_LEVEL Then
                cbxData.LIST_SALARY_LEVEL = (From p In Context.PA_SALARY_LEVEL
                                             Join o In Context.PA_SALARY_GROUP On p.SAL_GROUP_ID Equals o.ID
                                             Order By p.NAME.ToUpper
                                             Select New SalaryLevelDTO With {
                                                 .ID = p.ID,
                                                 .NAME = p.NAME,
                                                 .SAL_GROUP_ID = p.SAL_GROUP_ID
                                             }).ToList
            End If
            ''''''''''''''''''''''

            If cbxData.GET_SALARY_GROUP Then
                cbxData.LIST_SALARY_GROUP = (From p In Context.PA_SALARY_GROUP
                                             Where p.ACTFLG = "A"
                                             Order By p.NAME.ToUpper
                                             Select New SalaryGroupDTO With {
                                                 .ID = p.ID,
                                                 .NAME = p.NAME,
                                                 .EFFECT_DATE = p.EFFECT_DATE
                                             }).ToList
            End If
            'Danh muc cap chuc danh
            If cbxData.GET_LIST_TITLE_LEVEL Then
                cbxData.LIST_LIST_TITLE_LEVEL = (From p In Context.OT_OTHER_LIST Join t In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals t.ID
                                                 Where p.ACTFLG = "A" And t.CODE = "HU_TITLE_LEVEL" Order By p.CREATED_DATE Descending
                                                 Select New OT_OTHERLIST_DTO With {
                                                     .ID = p.ID,
                                                     .CODE = p.CODE,
                                                     .NAME_EN = p.NAME_EN,
                                                     .NAME_VN = p.NAME_VN,
                                                     .TYPE_ID = p.TYPE_ID
                                                 }).ToList
            End If
            'Danh loại luong thuong
            If cbxData.GET_INCENTIVE_TYPE Then
                cbxData.LIST_INCENTIVE_TYPE = (From p In Context.OT_OTHER_LIST Join t In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals t.ID
                                               Where p.ACTFLG = "A" And t.CODE = "PA_INCENTIVE_TYPE" Order By p.CREATED_DATE Descending
                                               Select New OT_OTHERLIST_DTO With {
                                                   .ID = p.ID,
                                                   .CODE = p.CODE,
                                                   .NAME_EN = p.NAME_EN,
                                                   .NAME_VN = p.NAME_VN,
                                                   .TYPE_ID = p.TYPE_ID
                                               }).ToList
            End If
            'Danh loại luong thuong
            If cbxData.GET_IMPORT_TYPE Then
                cbxData.LIST_IMPORT_TYPE = (From p In Context.OT_OTHER_LIST Join t In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals t.ID
                                            Where p.ACTFLG = "A" And t.CODE = "IMPORT_TYPE" Order By p.CREATED_DATE Descending
                                            Select New OT_OTHERLIST_DTO With {
                                                .ID = p.ID,
                                                .CODE = p.CODE,
                                                .NAME_EN = p.NAME_EN,
                                                .NAME_VN = p.NAME_VN,
                                                .TYPE_ID = p.TYPE_ID
                                            }).ToList
            End If
            If cbxData.GET_SALARY_TYPE Then
                cbxData.LIST_SALARY_TYPE = (From p In Context.PA_SALARY_TYPE
                                            Where p.ACTFLG = "A"
                                            Order By p.NAME.ToUpper
                                            Select New SalaryTypeDTO With {
                                                 .ID = p.ID,
                                                 .NAME = p.NAME
                                             }).ToList
            End If
            If cbxData.GET_NORM_ID Then
                cbxData.LIST_NORM_ID = (From p In Context.OT_OTHER_LIST
                                        Where p.ACTFLG = "A" And p.TYPE_CODE = "STANDARD_TYPE" Order By p.CREATED_DATE Descending
                                        Select New OT_OTHERLIST_DTO With {
                             .ID = p.ID,
                             .CODE = p.CODE,
                             .NAME_EN = p.NAME_EN,
                             .NAME_VN = p.NAME_VN,
                             .TYPE_ID = p.TYPE_ID,
                             .ATTRIBUTE1 = p.ATTRIBUTE1
                         }).ToList
            End If
            If cbxData.GET_INCOME_TYPE Then
                cbxData.LIST_INCOME_TYPE = (From p In Context.OT_OTHER_LIST
                                            Where p.ACTFLG = "A" And p.TYPE_CODE = "INCOME_TYPE" Order By p.CREATED_DATE Descending
                                            Select New OT_OTHERLIST_DTO With {
                             .ID = p.ID,
                             .CODE = p.CODE,
                             .NAME_EN = p.NAME_EN,
                             .NAME_VN = p.NAME_VN,
                             .TYPE_ID = p.TYPE_ID
                         }).ToList
            End If
            'Danh sách loại cửa hàng
            If cbxData.GET_TYPE_SHOP Then
                cbxData.LIST_TYPE_SHOP = (From p In Context.OT_OTHER_LIST Join t In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals t.ID
                                          Where p.ACTFLG = "A" And t.CODE = "TYPE_SHOP" Order By p.CREATED_DATE Descending
                                          Select New OT_OTHERLIST_DTO With {
                             .ID = p.ID,
                             .CODE = p.CODE,
                             .NAME_EN = p.NAME_EN,
                             .NAME_VN = p.NAME_VN,
                             .TYPE_ID = p.TYPE_ID
                              }).ToList
            End If
            'Danh sách nhan hang
            If cbxData.GET_BRAND Then
                cbxData.LIST_BRANDID = (From p In Context.OT_OTHER_LIST Join t In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals t.ID
                                        Where p.ACTFLG = "A" And t.CODE = "BRAND" Order By p.CREATED_DATE Descending
                                        Select New OT_OTHERLIST_DTO With {
                             .ID = p.ID,
                             .CODE = p.CODE,
                             .NAME_EN = p.NAME_EN,
                             .NAME_VN = p.NAME_VN,
                             .TYPE_ID = p.TYPE_ID
                              }).ToList

            End If
            'Danh sách Nhóm Target
            If cbxData.GET_TARGET_GROUP Then
                cbxData.LIST_TARGET_GROUP = (From p In Context.OT_OTHER_LIST Join t In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals t.ID
                                             Where p.ACTFLG = "A" And t.CODE = "TARGET_GROUP" Order By p.CREATED_DATE Descending
                                             Select New OT_OTHERLIST_DTO With {
                             .ID = p.ID,
                             .CODE = p.CODE,
                             .NAME_EN = p.NAME_EN,
                             .NAME_VN = p.NAME_VN,
                             .TYPE_ID = p.TYPE_ID
                              }).ToList

            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".GetComboboxData")
            Return False
        Finally

        End Try
    End Function

    ''' <summary>
    ''' Kiểm tra dữ liệu đã được sử dụng hay chưa?
    ''' </summary>
    ''' <param name="table">Enum Table_Name</param>
    ''' <returns>true:chưa có/false:có rồi</returns>
    ''' <remarks></remarks>
    Public Function CheckExistInDatabase(ByVal lstID As List(Of Decimal), ByVal table As TABLE_NAME) As Boolean
        Dim isExist As Boolean = False
        Dim strListID As String = lstID.Select(Function(x) x.ToString).Aggregate(Function(x, y) x & "," & y)
        Dim count As Decimal = 0
        Try

            Select Case table
                Case TABLE_NAME.PA_SALARY_GROUP
                    isExist = Execute_ExistInDatabase("PA_SALARY_LEVEL", "SAL_GROUP_ID", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
                    isExist = Execute_ExistInDatabase("HU_WORKING", "SAL_GROUP_ID", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
                Case TABLE_NAME.PA_SALARY_LEVEL
                    isExist = Execute_ExistInDatabase("PA_SALARY_RANK", "SAL_LEVEL_ID", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
                    isExist = Execute_ExistInDatabase("HU_WORKING", "SAL_LEVEL_ID", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
                Case TABLE_NAME.PA_SALARY_RANK
                    isExist = Execute_ExistInDatabase("HU_WORKING", "SAL_RANK_ID", strListID)
                    If Not isExist Then
                        Return isExist
                    End If

            End Select
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Return False
        End Try
    End Function


    Private Function Execute_ExistInDatabase(tableName As String, colName As String, strListID As String)
        Try
            Dim count As Decimal = 0
            Dim Sql = "SELECT COUNT(" & colName & ") FROM " & tableName & " WHERE " & colName & " IN (" & strListID & ")"
            count = Context.ExecuteStoreQuery(Of Decimal)(Sql).FirstOrDefault
            If count > 0 Then
                Return False
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
    Public Function ActionSendPayslip(ByVal lstEmployee As List(Of Decimal),
                             ByVal orgID As Decimal?,
                             ByVal isDissolve As Decimal?,
                             ByVal periodID As Decimal,
                             ByVal log As UserLog) As Boolean
        Try
            Dim objPayslip As New PA_SEND_PAYSLIP
            objPayslip.ID = Utilities.GetNextSequence(Context, Context.PA_SEND_PAYSLIP.EntitySet.Name)
            objPayslip.ACTION_DATE = Date.Now
            objPayslip.ACTION_STATUS = 0
            objPayslip.ACTION_TYPE = 0
            objPayslip.IS_DISSOLVE = isDissolve
            objPayslip.ORG_ID = orgID
            objPayslip.PERIOD_ID = periodID
            objPayslip.USERNAME = log.Username.ToUpper
            Context.PA_SEND_PAYSLIP.AddObject(objPayslip)
            lstEmployee = lstEmployee.Distinct.ToList

            For Each emp As Decimal In lstEmployee
                Dim objNew As New PA_SEND_PAYSLIP_EMP
                objNew.ID = Utilities.GetNextSequence(Context, Context.PA_SEND_PAYSLIP_EMP.EntitySet.Name)
                objNew.EMPLOYEE_ID = emp
                objNew.PA_SEND_PAYSLIP_ID = objPayslip.ID
                Context.PA_SEND_PAYSLIP_EMP.AddObject(objNew)
                objPayslip.ACTION_TYPE = 1
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
    Public Function ActionSendBonusslip(ByVal lstEmployee As List(Of Decimal),
                             ByVal orgID As Decimal?,
                             ByVal isDissolve As Decimal?,
                             ByVal periodID As Decimal,
                             ByVal log As UserLog) As Boolean
        Try
            Dim objPayslip As New PA_SEND_BONUSSLIP
            objPayslip.ID = Utilities.GetNextSequence(Context, Context.PA_SEND_BONUSSLIP.EntitySet.Name)
            objPayslip.ACTION_DATE = Date.Now
            objPayslip.ACTION_STATUS = 0
            objPayslip.ACTION_TYPE = 0
            objPayslip.IS_DISSOLVE = isDissolve
            objPayslip.ORG_ID = orgID
            objPayslip.PERIOD_ID = periodID
            objPayslip.USERNAME = log.Username.ToUpper
            Context.PA_SEND_BONUSSLIP.AddObject(objPayslip)
            lstEmployee = lstEmployee.Distinct.ToList

            For Each emp As Decimal In lstEmployee
                Dim objNew As New PA_SEND_BONUSSLIP_EMP
                objNew.ID = Utilities.GetNextSequence(Context, Context.PA_SEND_BONUSSLIP_EMP.EntitySet.Name)
                objNew.EMPLOYEE_ID = emp
                objNew.PA_SEND_BONNUSSLIP_ID = objPayslip.ID
                Context.PA_SEND_BONUSSLIP_EMP.AddObject(objNew)
                objPayslip.ACTION_TYPE = 1
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
    Public Function CheckAndSendPayslip() As Boolean
        Try
            'Return True 'chay dang bị lỗi nên trả về luôn để services ko lỗi liên tục
            '1. Kiểm tra có action nào đang chạy hay không?
            Dim isExist = (From p In Context.PA_SEND_PAYSLIP
                           Where p.ACTION_STATUS = 1).Any

            If Not isExist Then
                '2. Nếu chưa có action nào chạy thì tiến hành chạy
                'Đổi trạng thái đang thực hiện để không bị chạy chồng chéo
                Dim objAction = (From p In Context.PA_SEND_PAYSLIP
                                 Where p.ACTION_STATUS = 0 Or p.ACTION_STATUS = 3).FirstOrDefault
                If objAction IsNot Nothing Then
                    objAction.ACTION_STATUS = 1
                    Context.SaveChanges()
                    '3. Tiến hành thực hiện
                    Try
                        Dim lstEmp As New List(Of Decimal)
                        If objAction.ACTION_TYPE = 1 Then
                            lstEmp = (From p In Context.PA_SEND_PAYSLIP_EMP
                                      Where p.PA_SEND_PAYSLIP_ID = objAction.ID
                                      Select p.EMPLOYEE_ID.Value).ToList


                            Using cls As New DataAccess.NonQueryData
                                cls.ExecuteSQL("DELETE FROM SE_EMPLOYEE_CHOSEN S WHERE S.USING_USER ='" + objAction.USERNAME.ToUpper + "'")
                                Dim sql As String = "INSERT INTO SE_EMPLOYEE_CHOSEN" & vbNewLine & _
                                    "  (EMPLOYEE_ID, USING_USER)" & vbNewLine & _
                                    "  (SELECT DISTINCT EMPLOYEE_ID, '" + objAction.USERNAME.ToUpper + "'" & vbNewLine & _
                                    "     FROM PA_SEND_PAYSLIP_EMP" & vbNewLine & _
                                    "    WHERE PA_SEND_PAYSLIP_ID = " + objAction.ID.ToString + ")"

                                cls.ExecuteSQL(sql)

                            End Using

                        End If
                        Dim rowTotal As Decimal = 0
                        objAction.RUN_START = Date.Now
                        If SendPayslip(lstEmp,
                                       objAction.ORG_ID,
                                       objAction.IS_DISSOLVE,
                                       objAction.PERIOD_ID,
                                       objAction.USERNAME.ToUpper,
                                       rowTotal) Then
                            objAction.RUN_ROW = rowTotal
                            objAction.ACTION_STATUS = 2
                        Else
                            objAction.ACTION_STATUS = 3
                        End If
                        objAction.RUN_END = Date.Now

                    Catch ex As Exception
                        WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
                        objAction.ACTION_STATUS = 3
                    End Try

                    Context.SaveChanges()
                End If
            End If
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Return False
            Throw ex

        End Try
    End Function


    Private Function CAL_DATA_INOUT() As Boolean

        Try

        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function CheckAndSendBonusSlip() As Boolean
        Try
            '1. Kiểm tra có action nào đang chạy hay không?
            Dim isExist = (From p In Context.PA_SEND_BONUSSLIP
                           Where p.ACTION_STATUS = 1).Any

            If Not isExist Then
                '2. Nếu chưa có action nào chạy thì tiến hành chạy
                'Đổi trạng thái đang thực hiện để không bị chạy chồng chéo
                Dim objAction = (From p In Context.PA_SEND_BONUSSLIP
                                 Where p.ACTION_STATUS = 0 Or p.ACTION_STATUS = 3).FirstOrDefault
                If objAction IsNot Nothing Then
                    objAction.ACTION_STATUS = 1
                    Context.SaveChanges()
                    '3. Tiến hành thực hiện
                    Try
                        Dim lstEmp As New List(Of Decimal)
                        If objAction.ACTION_TYPE = 1 Then
                            lstEmp = (From p In Context.PA_SEND_BONUSSLIP_EMP
                                      Where (p.PA_SEND_BONNUSSLIP_ID = objAction.ID)
                                      Select p.EMPLOYEE_ID.Value).ToList


                            Using cls As New DataAccess.NonQueryData
                                cls.ExecuteSQL("DELETE FROM SE_EMPLOYEE_CHOSEN S WHERE S.USING_USER ='" + objAction.USERNAME.ToUpper + "'")
                                Dim sql As String = "INSERT INTO SE_EMPLOYEE_CHOSEN" & vbNewLine & _
                                    "  (EMPLOYEE_ID, USING_USER)" & vbNewLine & _
                                    "  (SELECT DISTINCT EMPLOYEE_ID, '" + objAction.USERNAME.ToUpper + "'" & vbNewLine & _
                                    "     FROM PA_SEND_BONUSSLIP_EMP" & vbNewLine & _
                                    "    WHERE PA_SEND_BONNUSSLIP_ID = " + objAction.ID.ToString + ")"

                                cls.ExecuteSQL(sql)

                            End Using

                        End If
                        Dim rowTotal As Decimal = 0
                        objAction.RUN_START = Date.Now
                        If SendBonusSlip(lstEmp,
                                       objAction.ORG_ID,
                                       objAction.IS_DISSOLVE,
                                       objAction.PERIOD_ID,
                                       objAction.USERNAME.ToUpper,
                                       rowTotal) Then
                            objAction.RUN_ROW = rowTotal
                            objAction.ACTION_STATUS = 2
                        Else
                            objAction.ACTION_STATUS = 3
                        End If
                        objAction.RUN_END = Date.Now

                    Catch ex As Exception
                        WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
                        objAction.ACTION_STATUS = 3
                    End Try

                    Context.SaveChanges()
                End If
            End If
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Return False
        End Try
    End Function

    Public Function SendPayslip(ByVal lstEmployee As List(Of Decimal),
                             ByVal orgID As Decimal?,
                             ByVal isDissolve As Decimal?,
                             ByVal periodID As Decimal,
                             ByVal userName As String,
                             ByRef rowTotal As Decimal) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PA_BUSINESS.GET_PHIEULUONG",
                                           New With {.P_USERNAME = userName.ToUpper,
                                                     .P_PERIOD = periodID,
                                                     .P_ORGID = orgID,
                                                     .P_ISDISSOLVE = isDissolve,
                                                     .P_SENDMAIL = 1,
                                                     .P_CUR = cls.OUT_CURSOR})

                Dim dtTemplate As DataTable = cls.ExecuteStore("PKG_PA_BUSINESS.GET_TEMPLATE_SEND_MAIL",
                                          New With {.P_CUR = cls.OUT_CURSOR})

                Dim isSave As Boolean = False
                Dim config = GetConfig(ModuleID.All)
                Dim emailFrom = If(config.ContainsKey("MailFrom"), config("MailFrom"), "")
                rowTotal = 0
                'Dim dtDataMerge As DataTable
                For Each row As DataRow In dtData.Rows
                    'Dim reader As StreamReader
                    Dim body As String = dtTemplate.Rows(0)("CONTENT").ToString

                    'Dim filePath As String = AppDomain.CurrentDomain.BaseDirectory & "\ReportTemplates\Payroll\Report\PhieuLuong.docx"
                    'Dim doc As Document = New Document(filePath)
                    'If doc IsNot Nothing AndAlso row("WORK_EMAIL").ToString <> "" And emailFrom <> "" Then
                    '    'filePath = System.AppDomain.CurrentDomain.BaseDirectory & "Payroll\Attachment\" & "PhieuLuong_" & row("EMPLOYEE_CODE").ToString & "_" & Format(Date.Now, "yyyyMMddHHmmss")
                    '    dtDataMerge = dtData.Clone
                    '    dtDataMerge.ImportRow(row)
                    '    SendPhieuLuong_File(doc, filePath, dtDataMerge, False)

                    'reader = New StreamReader("ThuUngVienKhongPhuHop.html")

                    If row("WORK_EMAIL").ToString <> "" AndAlso emailFrom <> "" Then
                        body = body.Replace("{EMPLOYEE_CODE}", row("EMPLOYEE_CODE").ToString)
                        body = body.Replace("{FULLNAME_VN}", row("FULLNAME_VN").ToString)
                        body = body.Replace("{CLCHINH1}", row("CLCHINH1").ToString)
                        body = body.Replace("{CW1}", row("CW1").ToString)
                        body = body.Replace("{CW2}", row("CW2").ToString)
                        body = body.Replace("{SUM1}", row("SUM1").ToString)
                        body = body.Replace("{CL1}", row("CL1").ToString)
                        body = body.Replace("{CL2}", row("CL2").ToString)
                        body = body.Replace("{ADD1}", row("ADD1").ToString)
                        body = body.Replace("{CLCHINH7}", row("CLCHINH7").ToString)
                        body = body.Replace("{CLCHINH4}", row("CLCHINH4").ToString)
                        body = body.Replace("{SUM2}", row("SUM2").ToString)
                        body = body.Replace("{INSU1_2_3}", row("INSU1_2_3").ToString)
                        body = body.Replace("{DEDUCT6}", row("DEDUCT6").ToString)
                        body = body.Replace("{TAX4_5}", row("TAX4_5").ToString)
                        body = body.Replace("{DEDUCT2_3_4}", row("DEDUCT2_3_4").ToString)
                        body = body.Replace("{DEDUCT1}", row("DEDUCT1").ToString)
                        body = body.Replace("{CSUM2}", row("CSUM2").ToString)

                        Dim rData = row.Table()
                        rData.TableName = "DATA"
                        Dim filePath = System.AppDomain.CurrentDomain.BaseDirectory + "ReportTemplates\Payroll\Report\PhieuLuongNV.xlsx"
                        Dim saveLocal = System.AppDomain.CurrentDomain.BaseDirectory + "ReportTemplates\Payroll\"
                        Dim designer As WorkbookDesigner
                        designer = New WorkbookDesigner
                        designer.Open(filePath)
                        designer.SetDataSource(rData)
                        designer.Process()
                        designer.Workbook.CalculateFormula()
                        Dim fileName = "Phieu_luong_" & row("EMPLOYEE_CODE").ToString & ".xls"
                        designer.Workbook.Save(saveLocal + fileName, Aspose.Cells.SaveFormat.Excel97To2003)

                        'If (filePath IsNot Nothing) Then
                        InsertMail(emailFrom,
                               row("WORK_EMAIL").ToString,
                               "Thông báo lương tháng  " & row("TITLE").ToString,
                               body,
                               saveLocal + fileName,
                               "",
                               "",
                               "iPayroll_SendMail")
                        isSave = True
                        rowTotal += 1
                    End If
                    'End If
                Next
                If isSave Then
                    Context.SaveChanges()
                End If
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function SendBonusSlip(ByVal lstEmployee As List(Of Decimal),
                             ByVal orgID As Decimal?,
                             ByVal isDissolve As Decimal?,
                             ByVal periodID As Decimal,
                             ByVal userName As String,
                             ByRef rowTotal As Decimal) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As New DataTable
                'Dim dtData As DataTable = cls.ExecuteStore("PKG_PA_REPORT.GET_PHIEUTHUONG",
                '                           New With {.P_USERNAME = userName.ToUpper,
                '                                     .P_PERIOD = periodID,
                '                                     .P_ORGID = orgID,
                '                                     .P_ISDISSOLVE = isDissolve,
                '                                     .P_SENDMAIL = 1,
                '                                     .P_CUR = cls.OUT_CURSOR})

                Dim isSave As Boolean = False
                Dim config = GetConfig(ModuleID.All)
                Dim emailFrom = If(config.ContainsKey("MailFrom"), config("MailFrom"), "")
                rowTotal = 0
                Dim dtDataMerge As DataTable
                For Each row As DataRow In dtData.Rows
                    Dim filePath As String = AppDomain.CurrentDomain.BaseDirectory & "\ReportTemplates\Payroll\Report\PhieuThuongHQCV.docx"
                    Dim doc As Document = New Document(filePath)
                    If doc IsNot Nothing AndAlso row("WORK_EMAIL").ToString <> "" And emailFrom <> "" Then
                        filePath = System.AppDomain.CurrentDomain.BaseDirectory & "Payroll\Attachment\" & "PhieuThuongHQCV_" & row("EMPLOYEE_CODE").ToString & "_" & Format(Date.Now, "yyyyMMddHHmmss")
                        dtDataMerge = dtData.Clone
                        dtDataMerge.ImportRow(row)
                        SendPhieuLuong_File(doc, filePath, dtDataMerge, False)
                        InsertMail(emailFrom,
                                   row("WORK_EMAIL").ToString,
                                   "[HiStaff] " & row("TITLE_NAME").ToString,
                                   "Kính gửi anh/chị," & vbNewLine & vbNewLine &
                                   "Phòng HCNS gửi Anh/Chị phiếu thưởng HQCV như file đính kèm.",
                                   filePath,
                                   "",
                                   "",
                                   "iBonus_SendMail")
                        isSave = True
                        rowTotal += 1
                    End If
                Next
                If isSave Then
                    Context.SaveChanges()
                End If
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
    Public Function InsertMail(ByVal _from As String,
                           ByVal _to As String,
                           ByVal _subject As String,
                           ByVal _content As String,
                           Optional ByVal _attachment As String = "",
                           Optional ByVal _cc As String = "",
                           Optional ByVal _bcc As String = "",
                           Optional ByVal _viewName As String = "")
        Try
            Dim _newMail As New SE_MAIL
            _newMail.ID = Utilities.GetNextSequence(Context, Context.SE_MAIL.EntitySet.Name)
            _newMail.MAIL_FROM = _from
            _newMail.MAIL_TO = _to
            _newMail.MAIL_CC = _cc
            _newMail.MAIL_BCC = _bcc
            _newMail.SUBJECT = _subject
            _newMail.CONTENT = _content
            _newMail.VIEW_NAME = _viewName
            _newMail.ATTACHMENT = _attachment
            _newMail.ACTFLG = "I"
            Context.SE_MAIL.AddObject(_newMail)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub SendPhieuLuong_File(ByVal doc As Document,
                                ByRef filename As String,
                                ByVal dtData As DataTable,
                                Optional ByVal is2003 As Boolean = True)
        Try
            ' Fill the fields in the document with user data.
            doc.MailMerge.Execute(dtData)
            ' Send the document in Word format to the client browser with an option to save to disk or open inside the current browser.

            'doc.Save(filename, SaveFormat.Doc, SaveType.OpenInApplication, response)
            If is2003 Then
                filename = filename & ".doc"
                doc.Save(filename, Aspose.Words.Saving.SaveOptions.CreateSaveOptions(Aspose.Words.SaveFormat.Doc))
            Else
                filename = filename & ".docx"
                doc.Save(filename, Aspose.Words.Saving.SaveOptions.CreateSaveOptions(Aspose.Words.SaveFormat.Docx))
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function GetTitleList(ByVal sLang As String, ByVal isBlank As Boolean) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_TITLE",
                                           New With {.P_ISBLANK = isBlank,
                                                     .P_LANG = sLang,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
End Class

Public Enum TABLE_NAME
    PA_SALARY_GROUP = 1
    PA_SALARY_LEVEL = 2
    PA_SALARY_RANK = 3
End Enum