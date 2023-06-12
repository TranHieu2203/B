Imports System.Data.Objects
Imports System.Reflection
Imports Framework.Data
Imports Framework.Data.System.Linq.Dynamic
Imports Common

Partial Class ProfileRepository

#Region "Service Auto Update Employee Information"
    Public Function CheckAndUpdateEmployeeInformation() As Boolean
        UpdateWorking()
        UpdateTerminate()
        UpdateContract()
        UpdateKiemNhiem()
        UpdateUser()
        Return True
    End Function
    Public Function CheckSendMailStockRemind() As Boolean
        SendMailStockRemind()
        Return True
    End Function

    Private Sub UpdateWorking()
        Try
            Dim query = (From p In Context.HUV_CURRENT_WORKING
                         Select New WorkingDTO With {
                             .EMPLOYEE_ID = p.EMPLOYEE_ID,
                             .TITLE_ID = p.TITLE_ID,
                             .ORG_ID = p.ORG_ID,
                             .ID = p.ID,
                             .STAFF_RANK_ID = p.STAFF_RANK_ID,
                             .EFFECT_DATE = p.EFFECT_DATE,
                             .DIRECT_MANAGER = p.DIRECT_MANAGER,
                             .OBJECT_ATTENDANCE = p.OBJECT_ATTENDANCE,
                             .OBJECT_LABOR = p.OBJECT_LABOR
                             }).ToList

            For i As Integer = 0 To query.Count - 1
                ApproveWorking1(query(i))
                'ApproveWorking(query(i))
            Next
            If query.Count > 0 Then Context.SaveChanges()

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
        End Try
    End Sub

    Private Sub UpdateTerminate()
        Try
            Dim query = (From p In Context.HUV_TERMINATE_CURRENT
                         Select New TerminateDTO With {
                             .EMPLOYEE_ID = p.EMPLOYEE_ID,
                             .ID = p.ID,
                             .EFFECT_DATE = p.EFFECT_DATE,
                             .LAST_DATE = p.LAST_DATE
                             }).ToList

            For i As Integer = 0 To query.Count - 1
                Dim empId = query(i).EMPLOYEE_ID
                Dim item = (From p In Context.HU_EMPLOYEE Where p.ID = empId).FirstOrDefault
                If item IsNot Nothing Then
                    Try
                        Dim Employeelist = (From p In Context.HU_EMPLOYEE Where item.EMPLOYEE_CODE = p.EMPLOYEE_CODE Select p).ToList
                        For Each objEmployeeData As HU_EMPLOYEE In Employeelist
                            objEmployeeData.WORK_STATUS = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID
                            objEmployeeData.TER_EFFECT_DATE = query(i).EFFECT_DATE
                            objEmployeeData.TER_LAST_DATE = query(i).LAST_DATE
                            'objEmployeeData.EMP_STATUS = 6
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
                        Next
                    Catch ex As Exception
                        Continue For
                    End Try
                End If
            Next

            If query.Count > 0 Then Context.SaveChanges()

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
        End Try
    End Sub

    Private Sub UpdateUser()
        Try
            Dim curDate = Date.Now.Date
            Dim query = (From p In Context.SE_USER
                         Where p.EXPIRE_DATE <= curDate AndAlso p.ACTFLG = "A").ToList

            For i As Integer = 0 To query.Count - 1
                Dim userId = query(i).ID
                Dim objUser = (From p In Context.SE_USER Where p.ID = userId).FirstOrDefault
                objUser.ACTFLG = "I"
                Using _Context As New ProfileContext
                    Dim queryUpdate As String = ""

                    queryUpdate = " DELETE SE_GRP_SE_USR WHERE SE_USERS_ID = " + objUser.ID.ToString

                    _Context.ExecuteStoreCommand(queryUpdate)
                End Using
            Next
            If query.Count > 0 Then Context.SaveChanges()

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
        End Try
    End Sub

    Private Sub UpdateContract()
        Try
            Dim query = (From p In Context.HUV_CURRENT_CONTRACT
                         Select New ContractDTO With {
                             .ID = p.ID,
                             .START_DATE = p.START_DATE,
                             .EXPIRE_DATE = p.EXPIRE_DATE,
                             .WORKING_ID = p.WORKING_ID,
                             .EMPLOYEE_ID = p.EMPLOYEE_ID,
                             .CONTRACTTYPE_ID = p.CONTRACT_TYPE_ID}).ToList

            For i As Integer = 0 To query.Count - 1
                Try
                    Dim empId = query(i).EMPLOYEE_ID
                    Dim contractID = query(i).ID
                    Dim emp = (From p In Context.HU_EMPLOYEE Where p.ID = empId).FirstOrDefault
                    If (emp.TER_EFFECT_DATE Is Nothing) OrElse (emp.TER_EFFECT_DATE IsNot Nothing AndAlso query(i).START_DATE <= emp.TER_EFFECT_DATE) Then
                        ApproveContract_Background(query(i))
                        Dim objContract As ContractDTO = (From p In Context.HU_CONTRACT
                                                          From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID).DefaultIfEmpty
                                                          Where p.ID = contractID
                                                          Select New ContractDTO With {
                                                  .EMPLOYEE_ID = p.EMPLOYEE_ID,
                                                  .WORKING_ID = p.WORKING_ID,
                                                  .ORG_ID = p.ORG_ID,
                                                  .TITLE_ID = p.TITLE_ID,
                                                  .START_DATE = p.START_DATE,
                                                  .SIGN_ID = p.SIGN_ID,
                                                  .SIGNER_TITLE = p.SIGNER_TITLE,
                                                  .SIGN_DATE = p.SIGN_DATE,
                                                  .OBJECTTIMEKEEPING = e.OBJECTTIMEKEEPING,
                                                  .DIRECT_MANAGER = e.DIRECT_MANAGER,
                                                  .SIGNER_NAME = p.SIGNER_NAME
                                            }).FirstOrDefault
                        If IsFirstContract(objContract) Then
                            InsertDecision_Background(objContract)
                        End If
                    End If
                Catch ex As Exception
                    Continue For
                End Try
            Next
            If query.Count > 0 Then Context.SaveChanges()
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
        End Try
    End Sub

    ''' <summary>
    ''' Cập nhật trạng thái Chờ nghỉ việc => Nghỉ việc (nếu thời điểm nghỉ việc là ngày hiện tại)
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub UpdateTrangThaiNghiViec()

        Using _Context As New ProfileContext
            Try
                Dim queryUpdate As String = ""

                queryUpdate = " UPDATE HU_TITLE SET MASTER = NULL, CONCURRENT = 0 WHERE ID IN (SELECT T.ID FROM HU_EMPLOYEE E JOIN HU_TITLE T ON T.ID = E.TITLE_ID WHERE T.MASTER = E.ID AND E.WORK_STATUS = 256 AND TO_CHAR(E.TER_LAST_DATE) = TO_CHAR(SYSDATE)) "

                _Context.ExecuteStoreCommand(queryUpdate)

                queryUpdate = " UPDATE HU_TITLE SET INTERIM = NULL WHERE ID IN (SELECT T.ID FROM HU_EMPLOYEE E JOIN HU_TITLE T ON T.ID = E.TITLE_ID WHERE T.INTERIM = E.ID AND E.WORK_STATUS = 256 AND TO_CHAR(E.TER_LAST_DATE) = TO_CHAR(SYSDATE)) "

                _Context.ExecuteStoreCommand(queryUpdate)

                ' cập nhật trạng thái làm việc từ chờ nghỉ việc sang nghỉ việc
                queryUpdate = " UPDATE HU_EMPLOYEE SET WORK_STATUS = 257 WHERE WORK_STATUS = 256 AND TO_CHAR(TER_LAST_DATE) = TO_CHAR(SYSDATE) "

                _Context.ExecuteStoreCommand(queryUpdate)
            Catch ex As Exception
                WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
                Throw ex
            End Try
        End Using
    End Sub

    ''' <summary>
    ''' Cập nhật tổ chức, đơn vị
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub UPDATE_ORG_OM_TEMP()
        Try
            Using q As New DataAccess.QueryData()
                Dim params As New Dictionary(Of String, Object)
                'q.ExecuteStoreProcedureNonQueryTask("PKG_OMS_BUSINESS.JOB_AUTO_UPDATE_ORG_OM_TEMP", params)
            End Using
        Catch ex As Exception

        End Try
    End Sub

    Private Sub UpdateKiemNhiem()
        Try
            Dim query = (From p In Context.HUV_CONCURRENTLY).ToList

            For i As Integer = 0 To query.Count - 1
                Using cls As New DataAccess.QueryData
                    Dim dtData = cls.ExecuteStore("PKG_HU_IPROFILE_CONCURRENTLY.INSERT_EMPLOYEE_KN",
                                               New With {.P_EMPLOYEE_CODE = query(i).EMPLOYEE_CODE,
                                                         .P_ORG_ID = query(i).ORG_CON,
                                                         .P_TITLE = query(i).TITLE_CON,
                                                         .P_DATE = query(i).EFFECT_DATE_CON,
                                                         .P_ID_KN = query(i).ID
                                                         })

                End Using
            Next

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
        End Try
    End Sub

    Private Sub SendMailStockRemind()
        Try
            Dim dataMail As List(Of String)
            Dim body As String = ""
            Dim mail As String = ""
            Dim mailCC As String = ""
            Dim titleMail As String = ""
            Dim bodyNew As String = ""
            Dim lstReminder = (From p In Context.SE_REMINDER Where p.TYPE = 40 Select p.VALUE).ToList
            For Each days In lstReminder
                If days Is Nothing Then
                    Exit Sub
                End If
                Dim defaultFrom = (From p In Context.SE_CONFIG Where p.CODE = "MailFrom" Select p.VALUE).FirstOrDefault
                If defaultFrom = "" Then
                    Exit Sub
                End If
                Dim DateNow = Date.Now.AddDays(days)
                Dim query = (From p In Context.HU_STOCKS_TRANSACTION
                             From s In Context.HU_STOCKS.Where(Function(f) f.ID = p.STOCK_ID)
                             From st In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.STATUS)
                             Where EntityFunctions.TruncateTime(p.TRADE_DATE) = EntityFunctions.TruncateTime(DateNow) And st.CODE.ToUpper = "INCOMPLETE"
                             Select New StocksTransactionDTO With {
                                 .ID = p.ID,
                                 .TRADE_DATE = p.TRADE_DATE,
                                 .EMPLOYEE_ID = s.EMPLOYEE_ID
                                 }).ToList
                If query Is Nothing Then
                    Continue For
                End If
                Dim res = (From p In Context.SE_MAIL_TEMPLATE Where p.CODE = "STOCKS_REMIND" And p.GROUP_MAIL = "Profile" Select p.CONTENT, p.MAIL_CC, p.TITLE).FirstOrDefault
                body = res.CONTENT
                mail = res.MAIL_CC.Split(";")(0)
                mailCC = res.MAIL_CC
                titleMail = res.TITLE
                For Each item In query
                    Dim query1 = (From p In Context.SE_MAIL Where p.MAIL_TO = mail And p.SUBJECT = titleMail And EntityFunctions.TruncateTime(p.CREATE_DATE) = EntityFunctions.TruncateTime(Date.Now)).Any
                    If query1 Then
                        Continue For
                    End If
                    Dim _newMail As New SE_MAIL
                    _newMail.ID = Utilities.GetNextSequence(Context, Context.SE_MAIL.EntitySet.Name)
                    _newMail.MAIL_FROM = defaultFrom
                    _newMail.MAIL_TO = mail
                    _newMail.MAIL_CC = mailCC
                    _newMail.MAIL_BCC = ""
                    _newMail.SUBJECT = titleMail
                    _newMail.CONTENT = body
                    _newMail.VIEW_NAME = ""
                    _newMail.ACTFLG = "I"
                    _newMail.CREATE_DATE = Date.Now
                    _newMail.ORDER_BY = 1
                    _newMail.WAITING = 0
                    _newMail.RUN_ROW = 0
                    Context.SE_MAIL.AddObject(_newMail)
                    Context.SaveChanges()
                Next
            Next
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
        End Try
    End Sub
#End Region
End Class
