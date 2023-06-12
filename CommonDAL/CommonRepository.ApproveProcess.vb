Imports System.Data.Objects
Imports System.Reflection
Imports Framework.Data
Imports Framework.Data.System.Linq.Dynamic

Partial Public Class CommonRepository

#Region "Process Setup"
    Public Function GetLeavePlanList() As List(Of OtherListDTO)
        Try
            Dim listLeavePlan = (From t In Context.OT_OTHER_LIST
                                 From type In Context.OT_OTHER_LIST_TYPE.Where(Function(f) f.ID = t.TYPE_ID)
                                 Where type.CODE = "LEAVE_PLAN" And t.ACTFLG = "A"
                                 Select New OtherListDTO With {
                                    .ID = t.ID, .NAME_VN = t.NAME_VN
                                 }).ToList()
            Return listLeavePlan
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetSignList() As List(Of ATTimeManualDTO)
        Try
            Dim listSign = (From t In Context.AT_TIME_MANUAL
                            Where t.ACTFLG = "A"
                            Select New ATTimeManualDTO With {
                                .ID = t.ID, .NAME = t.NAME
                            }).ToList()
            Return listSign
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetTitleList() As List(Of OtherListDTO)
        Try
            Dim dateNow = Date.Now.Date
            Dim listTitle = (From p In Context.HU_TITLE
                             From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                             From m In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.MASTER).DefaultIfEmpty
                             Where (m.WORK_STATUS Is Nothing Or (m.WORK_STATUS IsNot Nothing And (m.WORK_STATUS <> 257 Or (m.WORK_STATUS = 257 And m.TER_EFFECT_DATE > dateNow)))) And p.ACTFLG = "A"
                             Order By p.CREATED_DATE Descending
                             Select New OtherListDTO With {
                                .ID = p.ID, .CODE = p.CODE, .NAME_VN = p.CODE & " - " & p.NAME_VN, .MASTER_NAME = m.EMPLOYEE_CODE & " - " & m.FULLNAME_VN
                             }).ToList()
            Return listTitle
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetApproveProcess() As List(Of ApproveProcessDTO)

        Try
            Dim listProcess = (From pr In Context.SE_APP_PROCESS
                               Select New ApproveProcessDTO With {
                                                                .ID = pr.ID,
                                                                .NAME = pr.NAME,
                                                                .ACTFLG = pr.ACTFLG,
                                                                .PROCESS_CODE = pr.PROCESS_CODE,
                                                                .NUMREQUEST = pr.NUMREQUEST,
                                                                .EMAIL = pr.EMAIL
                                                                }).ToList()

            Return listProcess
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetApproveProcess(ByVal processId As Decimal) As ApproveProcessDTO
        Try
            Dim itemGet = (From a In Context.SE_APP_PROCESS
                           Where a.ID = processId
                           Select New ApproveProcessDTO With {
                               .ID = a.ID,
                               .ACTFLG = a.ACTFLG,
                               .NAME = a.NAME,
                               .NUMREQUEST = a.NUMREQUEST,
                               .EMAIL = a.EMAIL
                           }).FirstOrDefault


            Return itemGet

        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function InsertApproveProcess(ByVal item As ApproveProcessDTO) As Boolean
        Try
            Dim itemInsert As New SE_APP_PROCESS With {.ID = Utilities.GetNextSequence(Context, Context.SE_APP_PROCESS.EntitySet.Name),
                                                       .NAME = item.NAME,
                                                       .ACTFLG = "A",
                                                       .NUMREQUEST = item.NUMREQUEST,
                                                       .EMAIL = item.EMAIL,
                                                       .PROCESS_CODE = "AA"
                                                      }

            Context.SE_APP_PROCESS.AddObject(itemInsert)

            Context.SaveChanges()

            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function UpdateApproveProcess(ByVal item As ApproveProcessDTO) As Boolean
        Try
            Dim objectEdit As SE_APP_PROCESS = Context.SE_APP_PROCESS.FirstOrDefault(Function(p) p.ID = item.ID)

            If objectEdit IsNot Nothing Then
                With objectEdit
                    .NAME = item.NAME
                    .NUMREQUEST = item.NUMREQUEST
                    .EMAIL = item.EMAIL
                End With
            End If

            Context.SaveChanges()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function UpdateApproveProcessStatus(ByVal itemUpdates As List(Of Decimal), ByVal status As String) As Boolean
        Try
            Dim objectEdit As List(Of SE_APP_PROCESS) = Context.SE_APP_PROCESS.Where(Function(p) itemUpdates.Contains(p.ID))

            If objectEdit IsNot Nothing Then
                For Each item In objectEdit
                    With item
                        .ACTFLG = status
                    End With
                Next
            End If

            Context.SaveChanges()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Approve Setup"

    Public Function GetApproveSetupByEmployee(ByVal employeeId As Decimal,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ApproveSetupDTO)
        Try
            Dim itemReturn = From s In Context.SE_APP_SETUP
                             From proc In Context.SE_APP_PROCESS.Where(Function(f) f.ID = s.PROCESS_ID)
                             From temp In Context.SE_APP_TEMPLATE.Where(Function(f) f.ID = s.TEMPLATE_ID)
                             Where s.EMPLOYEE_ID = employeeId
                             Select New ApproveSetupDTO With {
                                 .ID = s.ID,
                                 .EMPLOYEE_ID = s.EMPLOYEE_ID,
                                 .TEMPLATE_ID = s.TEMPLATE_ID,
                                 .ORG_ID = s.ORG_ID,
                                 .FROM_DATE = s.FROM_DATE,
                                 .TO_DATE = s.TO_DATE,
                                 .PROCESS_NAME = proc.NAME,
                                 .TEMPLATE_NAME = temp.TEMPLATE_NAME,
                                 .NUM_REQUEST = proc.NUMREQUEST,
                                 .REQUEST_EMAIL = proc.EMAIL
                             }

            Return itemReturn.OrderBy(Function(p) Sorts).ThenByDescending(Function(p) p.FROM_DATE).ToList

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetApproveSetupByOrg(ByVal orgId As Decimal,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ApproveSetupDTO)
        Try
            Dim itemReturn = From s In Context.SE_APP_SETUP
                             From sign_name In Context.AT_TIME_MANUAL.Where(Function(f) f.ID = s.SIGN_ID And f.ACTFLG = "A").DefaultIfEmpty
                             From title_name In Context.HU_TITLE.Where(Function(f) f.ID = s.TITLE_ID And f.ACTFLG = "A").DefaultIfEmpty
                             From title_group_name In Context.OT_OTHER_LIST.Where(Function(f) f.ID = s.GROUP_TITLE_ID And f.TYPE_CODE = "HU_TITLE_GROUP" And f.ACTFLG = "A").DefaultIfEmpty
                             From proc In Context.SE_APP_PROCESS.Where(Function(f) f.ID = s.PROCESS_ID And f.ACTFLG = "A")
                             From temp In Context.SE_APP_TEMPLATE.Where(Function(f) f.ID = s.TEMPLATE_ID And f.ACTFLG = "A")
                             Where s.ORG_ID = orgId
                             Select New ApproveSetupDTO With {
                                 .ID = s.ID,
                                 .EMPLOYEE_ID = s.EMPLOYEE_ID,
                                 .TEMPLATE_ID = s.TEMPLATE_ID,
                                 .ORG_ID = s.ORG_ID,
                                 .FROM_DATE = s.FROM_DATE,
                                 .TO_DATE = s.TO_DATE,
                                 .FROM_DAY = s.FROM_DAY,
                                 .TO_DAY = s.TO_DAY,
                                 .PROCESS_NAME = proc.NAME,
                                 .TEMPLATE_NAME = temp.TEMPLATE_NAME,
                                 .NUM_REQUEST = proc.NUMREQUEST,
                                 .REQUEST_EMAIL = proc.EMAIL,
                                 .SIGN_ID = s.SIGN_ID,
                                 .SIGN_NAME = sign_name.NAME,
                                 .TITLE_ID = s.TITLE_ID,
                                 .TITLE_NAME = title_name.NAME_VN,
                                 .LEAVEPLAN_ID = s.LEAVEPLAN_ID,
                                 .FROM_HOUR = s.FROM_HOUR,
                                 .TO_HOUR = s.TO_HOUR,
                                 .MAIL_ACCEPTING = s.MAIL_ACCEPTING,
                                 .MAIL_ACCEPTED = s.MAIL_ACCEPTED,
                                 .IS_IGNORE_APPROVE_LEVEL = s.IS_IGNORE_APPROVE_LEVEL,
                                 .GROUP_TITLE_ID = s.GROUP_TITLE_ID,
                                 .GROUP_TITLE_NAME = title_group_name.NAME_VN,
                                 .IS_OVER_LIMIT = s.IS_OVER_LIMIT
                             }

            Return itemReturn.OrderBy(Function(p) Sorts).ThenByDescending(Function(p) p.FROM_DATE).ToList 'itemReturn.OrderBy(Function(p) p.PROCESS_NAME).ThenByDescending(Function(p) p.FROM_DATE).ToList

        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function GetApproveSetup(ByVal id As Decimal) As ApproveSetupDTO
        Try
            Dim itemReturn = (From s In Context.SE_APP_SETUP
                              Where s.ID = id
                              Select New ApproveSetupDTO With {
                                  .ID = s.ID,
                                  .PROCESS_ID = s.PROCESS_ID,
                                  .TEMPLATE_ID = s.TEMPLATE_ID,
                                  .TITLE_ID = s.TITLE_ID,
                                  .SIGN_ID = s.SIGN_ID,
                                  .LEAVEPLAN_ID = s.LEAVEPLAN_ID,
                                  .FROM_HOUR = s.FROM_HOUR,
                                  .TO_HOUR = s.TO_HOUR,
                                  .FROM_DAY = s.FROM_DAY,
                                  .TO_DAY = s.TO_DAY,
                                  .EMPLOYEE_ID = s.EMPLOYEE_ID,
                                  .ORG_ID = s.ORG_ID,
                                  .FROM_DATE = s.FROM_DATE,
                                  .TO_DATE = s.TO_DATE,
                                  .MAIL_ACCEPTED = s.MAIL_ACCEPTED,
                                  .MAIL_ACCEPTING = s.MAIL_ACCEPTING,
                                  .IS_IGNORE_APPROVE_LEVEL = s.IS_IGNORE_APPROVE_LEVEL,
                                  .IS_OVER_LIMIT = s.IS_OVER_LIMIT
                              }).FirstOrDefault

            Return itemReturn

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function InsertApproveSetup(ByVal item As ApproveSetupDTO, ByVal log As UserLog) As Boolean
        Try
            Dim itemInsert As New SE_APP_SETUP With {
                .ID = Utilities.GetNextSequence(Context, Context.SE_APP_SETUP.EntitySet.Name),
                .PROCESS_ID = item.PROCESS_ID,
                .TEMPLATE_ID = item.TEMPLATE_ID,
                .EMPLOYEE_ID = item.EMPLOYEE_ID,
                .ORG_ID = item.ORG_ID,
                .TITLE_ID = item.TITLE_ID,
                .SIGN_ID = item.SIGN_ID,
                .LEAVEPLAN_ID = item.LEAVEPLAN_ID,
                .FROM_HOUR = item.FROM_HOUR,
                .TO_HOUR = item.TO_HOUR,
                .FROM_DAY = item.FROM_DAY,
                .TO_DAY = item.TO_DAY,
                .FROM_DATE = item.FROM_DATE,
                .TO_DATE = item.TO_DATE,
                .MAIL_ACCEPTED = item.MAIL_ACCEPTED,
                .MAIL_ACCEPTING = item.MAIL_ACCEPTING,
                .CREATED_BY = log.Username,
                .CREATED_LOG = log.ComputerName,
                .CREATED_DATE = Date.Now,
                .MODIFIED_BY = log.Username,
                .MODIFIED_DATE = Date.Now,
                .MODIFIED_LOG = log.ComputerName,
                .IS_IGNORE_APPROVE_LEVEL = item.IS_IGNORE_APPROVE_LEVEL,
                .GROUP_TITLE_ID = item.GROUP_TITLE_ID,
                .IS_OVER_LIMIT = item.IS_OVER_LIMIT
            }

            Context.SE_APP_SETUP.AddObject(itemInsert)

            Context.SaveChanges()

            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function UpdateApproveSetup(ByVal item As ApproveSetupDTO, ByVal log As UserLog) As Boolean
        Try

            Dim itemUpdate = Context.SE_APP_SETUP.FirstOrDefault(Function(p) p.ID = item.ID)

            If itemUpdate IsNot Nothing Then
                With itemUpdate
                    .PROCESS_ID = item.PROCESS_ID
                    .TEMPLATE_ID = item.TEMPLATE_ID
                    .TITLE_ID = item.TITLE_ID
                    .SIGN_ID = item.SIGN_ID
                    .LEAVEPLAN_ID = item.LEAVEPLAN_ID
                    .FROM_HOUR = item.FROM_HOUR
                    .TO_HOUR = item.TO_HOUR
                    .FROM_DAY = item.FROM_DAY
                    .TO_DAY = item.TO_DAY
                    .FROM_DATE = item.FROM_DATE
                    .TO_DATE = item.TO_DATE

                    .MAIL_ACCEPTED = item.MAIL_ACCEPTED
                    .MAIL_ACCEPTING = item.MAIL_ACCEPTING

                    .MODIFIED_BY = log.Username
                    .MODIFIED_DATE = Date.Now
                    .MODIFIED_LOG = log.ComputerName

                    .IS_IGNORE_APPROVE_LEVEL = item.IS_IGNORE_APPROVE_LEVEL
                    .GROUP_TITLE_ID = item.GROUP_TITLE_ID
                    .IS_OVER_LIMIT = item.IS_OVER_LIMIT
                End With

                Context.SaveChanges()
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function DeleteApproveSetup(ByVal itemDeletes As List(Of Decimal)) As Boolean
        Try
            Dim items = Context.SE_APP_SETUP.Where(Function(p) itemDeletes.Contains(p.ID))

            For Each item As SE_APP_SETUP In items
                Context.SE_APP_SETUP.DeleteObject(item)
            Next

            Context.SaveChanges()

            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function IsExistSetupByDate(ByVal itemCheck As ApproveSetupDTO, Optional ByVal idExclude As Decimal? = Nothing) As Boolean
        Try
            Dim toDate As Date

            If itemCheck.TO_DATE.HasValue Then
                toDate = itemCheck.TO_DATE.Value
            Else
                toDate = Date.MaxValue
            End If

            Dim orgId As Decimal? = itemCheck.ORG_ID
            Dim employeeId As Decimal? = itemCheck.EMPLOYEE_ID
            Dim processId As Decimal = itemCheck.PROCESS_ID
            Dim fromDate As Date = itemCheck.FROM_DATE

            Dim tempCheck = From a In Context.SE_APP_SETUP
                            Where _
                            ((orgId.HasValue AndAlso a.ORG_ID = orgId) OrElse (Not orgId.HasValue)) _
                            AndAlso ((employeeId.HasValue AndAlso a.EMPLOYEE_ID = employeeId) OrElse (Not employeeId.HasValue)) _
                            AndAlso a.PROCESS_ID = processId _
                            AndAlso (Not idExclude.HasValue OrElse (idExclude.HasValue AndAlso a.ID <> idExclude)) _
                            AndAlso
                                ( _
                                    (a.TO_DATE.HasValue _
                                        AndAlso
                                        ( _
                                            (a.FROM_DATE <= fromDate AndAlso fromDate <= a.TO_DATE.Value) OrElse _
                                            (a.FROM_DATE <= toDate AndAlso toDate <= a.TO_DATE.Value) OrElse _
                                            (fromDate <= a.FROM_DATE AndAlso a.FROM_DATE <= toDate) OrElse _
                                            (fromDate <= a.TO_DATE.Value AndAlso a.TO_DATE.Value <= toDate) _
                                        ) _
                                    ) _
                                OrElse _
                                    (Not a.TO_DATE.HasValue _
                                        AndAlso _
                                        ( _
                                            (fromDate < a.FROM_DATE AndAlso toDate >= a.FROM_DATE) OrElse _
                                            (fromDate >= a.FROM_DATE) _
                                        ) _
                                    ) _
                                )

            'Logger.LogInfo(tempCheck)

            If tempCheck.Count() > 0 Then
                Return False
            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "Approve Template"
    ''' <lastupdate>
    ''' 24/07/2017 14:03
    ''' </lastupdate>
    ''' <summary>
    ''' Thêm mới thiết lập template
    ''' </summary>
    ''' <param name="item"></param>
    ''' <param name="log"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsertApproveTemplate(ByVal item As ApproveTemplateDTO, ByVal log As UserLog) As Boolean
        Try
            Dim itemInsert As New SE_APP_TEMPLATE With {
                .ID = Utilities.GetNextSequence(Context, Context.SE_APP_TEMPLATE.EntitySet.Name),
                .TEMPLATE_NAME = item.TEMPLATE_NAME,
                .TEMPLATE_TYPE = item.TEMPLATE_TYPE,
                .TEMPLATE_ORDER = item.TEMPLATE_ORDER,
                .ACTFLG = "A",
                .CREATED_BY = log.Username,
                .CREATED_LOG = log.ComputerName,
                .CREATED_DATE = Date.Now,
                .MODIFIED_BY = log.Username,
                .MODIFIED_DATE = Date.Now,
                .MODIFIED_LOG = log.ComputerName
            }

            Context.SE_APP_TEMPLATE.AddObject(itemInsert)
            Context.SaveChanges()

            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <lastupdate>
    ''' 24/07/2017 14:03
    ''' </lastupdate>
    ''' <summary>
    ''' Cập nhật thiết lập template
    ''' </summary>
    ''' <param name="item"></param>
    ''' <param name="log"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdateApproveTemplate(ByVal item As ApproveTemplateDTO, ByVal log As UserLog) As Boolean
        Try

            Dim itemUpdate = Context.SE_APP_TEMPLATE.FirstOrDefault(Function(p) p.ID = item.ID)

            If itemUpdate IsNot Nothing Then
                With itemUpdate
                    .TEMPLATE_NAME = item.TEMPLATE_NAME
                    .TEMPLATE_TYPE = item.TEMPLATE_TYPE
                    .TEMPLATE_ORDER = item.TEMPLATE_ORDER
                    .ACTFLG = item.ACTFLG
                    .MODIFIED_BY = log.Username
                    .MODIFIED_DATE = Date.Now
                    .MODIFIED_LOG = log.ComputerName
                End With

                Context.SaveChanges()
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <lastupdate>
    ''' 24/07/2017 14:03
    ''' </lastupdate>
    ''' <summary>
    ''' Lấy thiết lập template
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetApproveTemplate(ByVal id As Decimal) As ApproveTemplateDTO
        Try
            Dim itemReturn = (From s In Context.SE_APP_TEMPLATE
                              Where s.ID = id
                              Select New ApproveTemplateDTO With {
                                      .ID = s.ID,
                                  .TEMPLATE_NAME = s.TEMPLATE_NAME,
                                  .TEMPLATE_TYPE = s.TEMPLATE_TYPE,
                                  .TEMPLATE_ORDER = s.TEMPLATE_ORDER,
                                  .ACTFLG = s.ACTFLG
                              }).FirstOrDefault

            Return itemReturn

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <lastupdate>
    ''' 24/07/2017 14:03
    ''' </lastupdate>
    ''' <summary>
    ''' Lấy danh sách thiết lập template
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetApproveTemplate() As List(Of ApproveTemplateDTO)
        Try
            Dim itemReturn = (From s In Context.SE_APP_TEMPLATE
                              Select New ApproveTemplateDTO With {
                                  .ID = s.ID,
                                  .TEMPLATE_NAME = s.TEMPLATE_NAME,
                                  .TEMPLATE_TYPE = s.TEMPLATE_TYPE,
                                  .TEMPLATE_ORDER = s.TEMPLATE_ORDER,
                                  .TEMPLATE_CODE = s.TEMPLATE_CODE,
                                  .ACTFLG = s.ACTFLG
                              }).ToList

            Return itemReturn

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <lastupdate>
    ''' 24/07/2017 14:03
    ''' </lastupdate>
    ''' <summary>
    ''' Xóa thiết lập template
    ''' </summary>
    ''' <param name="itemDeletes"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteApproveTemplate(ByVal itemDeletes As List(Of Decimal)) As Boolean
        Try
            Dim items = Context.SE_APP_TEMPLATE.Where(Function(p) itemDeletes.Contains(p.ID)).ToList

            For Each item As SE_APP_TEMPLATE In items
                Dim lstDtl = (From p In Context.SE_APP_TEMPLATE_DTL Where p.TEMPLATE_ID = item.ID)
                For Each dtl In lstDtl
                    Context.SE_APP_TEMPLATE_DTL.DeleteObject(dtl)
                Next
                Dim lstExt = (From p In Context.SE_APP_TEMPLATE_DTL Where item.ID = p.TEMPLATE_ID).ToList
                For Each ext In lstExt
                    Context.SE_APP_TEMPLATE_DTL.DeleteObject(ext)
                Next
                Context.SE_APP_TEMPLATE.DeleteObject(item)
            Next

            Context.SaveChanges()

            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Check thiết lập template đã được sử dụng hay chưa
    ''' </summary>
    ''' <param name="templateID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IsApproveTemplateUsed(ByVal templateID As Decimal) As Boolean
        Try
            Dim listUsed = Context.SE_APP_SETUP.Count(Function(p) p.TEMPLATE_ID = templateID)

            If listUsed > 0 Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "Approve Template Detail"
    ''' <lastupdate>
    ''' 24/07/2017
    ''' </lastupdate>
    ''' <summary>
    ''' Thêm mới thiết lập phê duyệt chi tiết template
    ''' </summary>
    ''' <param name="item"></param>
    ''' <param name="log"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsertApproveTemplateDetail(ByVal item As ApproveTemplateDetailDTO, ByVal log As UserLog) As Boolean
        Try
            Dim itemInsert As New SE_APP_TEMPLATE_DTL With {
                .ID = Utilities.GetNextSequence(Context, Context.SE_APP_TEMPLATE_DTL.EntitySet.Name),
                .TEMPLATE_ID = item.TEMPLATE_ID,
                .APP_LEVEL = item.APP_LEVEL,
                .APP_TYPE = item.APP_TYPE,
                .APP_ID = item.APP_ID,
                .INFORM_DATE = item.INFORM_DATE,
                .INFORM_EMAIL = item.INFORM_EMAIL,
                .CREATED_BY = log.Username,
                .CREATED_LOG = log.ComputerName,
                .CREATED_DATE = Date.Now,
                .MODIFIED_BY = log.Username,
                .MODIFIED_DATE = Date.Now,
                .MODIFIED_LOG = log.ComputerName
            }

            Context.SE_APP_TEMPLATE_DTL.AddObject(itemInsert)

            Context.SaveChanges()

            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <lastupdate>
    ''' 24/07/2017
    ''' </lastupdate>
    ''' <summary>
    ''' Cập nhật thiết lập cho chi tiết template
    ''' </summary>
    ''' <param name="item"></param>
    ''' <param name="log"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdateApproveTemplateDetail(ByVal item As ApproveTemplateDetailDTO, ByVal log As UserLog) As Boolean
        Try

            Dim itemUpdate = Context.SE_APP_TEMPLATE_DTL.FirstOrDefault(Function(p) p.ID = item.ID)

            If itemUpdate IsNot Nothing Then
                With itemUpdate
                    .APP_LEVEL = item.APP_LEVEL
                    .APP_TYPE = item.APP_TYPE
                    .APP_ID = item.APP_ID
                    .INFORM_DATE = item.INFORM_DATE
                    .INFORM_EMAIL = item.INFORM_EMAIL

                    .MODIFIED_BY = log.Username
                    .MODIFIED_DATE = Date.Now
                    .MODIFIED_LOG = log.ComputerName
                End With

                Context.SaveChanges()
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <lastupdate>
    ''' 24/07/2017
    ''' </lastupdate>
    ''' <summary>
    ''' Lấy thông tin thiết lập chi tiết template
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetApproveTemplateDetail(ByVal id As Decimal) As ApproveTemplateDetailDTO
        Try
            Dim strNULL = ""
            Dim itemReturn = (From item In Context.SE_APP_TEMPLATE_DTL
                              From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = item.APP_ID).DefaultIfEmpty
                              From t In Context.HU_TITLE.Where(Function(f) f.ID = item.APP_ID).DefaultIfEmpty
                              Where item.ID = id
                              Select New ApproveTemplateDetailDTO With {
                                .ID = item.ID,
                                .TEMPLATE_ID = item.TEMPLATE_ID,
                                .APP_LEVEL = item.APP_LEVEL,
                                .APP_TYPE = item.APP_TYPE,
                                .APP_ID = item.APP_ID,
                                .TITLE_NAME = If(item.APP_TYPE = 6, t.NAME_VN, strNULL),
                                .INFORM_DATE = item.INFORM_DATE,
                                .INFORM_EMAIL = item.INFORM_EMAIL,
                                .EMPLOYEE_CODE = If(item.APP_TYPE <> 6, e.EMPLOYEE_CODE, strNULL),
                                .EMPLOYEE_NAME = If(item.APP_TYPE <> 6, e.FULLNAME_VN, strNULL)
                              }).FirstOrDefault

            Return itemReturn

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <lastupdate>
    ''' 24/07/2017
    ''' </lastupdate>
    ''' <summary>
    ''' Lấy danh sách thông tin chi tiết thiết lập template 
    ''' </summary>
    ''' <param name="templateId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetApproveTemplateDetailList(ByVal templateId As Decimal) As List(Of ApproveTemplateDetailDTO)
        Try
            Dim strNULL = ""
            Dim itemReturn = (From item In Context.SE_APP_TEMPLATE_DTL
                              From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = item.APP_ID).DefaultIfEmpty
                              From t In Context.HU_TITLE.Where(Function(f) f.ID = item.APP_ID).DefaultIfEmpty
                              Where item.TEMPLATE_ID = templateId
                              Select New ApproveTemplateDetailDTO With {
                                .ID = item.ID,
                                .TEMPLATE_ID = item.TEMPLATE_ID,
                                .APP_LEVEL = item.APP_LEVEL,
                                .APP_TYPE = item.APP_TYPE,
                                .APP_ID = item.APP_ID,
                                .TITLE_NAME = If(item.APP_TYPE = 6, t.NAME_VN, strNULL),
                                .INFORM_DATE = item.INFORM_DATE,
                                .INFORM_EMAIL = item.INFORM_EMAIL,
                                .EMPLOYEE_CODE = If(item.APP_TYPE <> 6, e.EMPLOYEE_CODE, strNULL),
                                .EMPLOYEE_NAME = If(item.APP_TYPE <> 6, e.FULLNAME_VN, strNULL)
                              }).ToList

            Return itemReturn

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Xóa thông tin chi tiết thiết lập cho template
    ''' </summary>
    ''' <param name="itemDeletes"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteApproveTemplateDetail(ByVal itemDeletes As List(Of Decimal)) As Boolean
        Try
            Dim items = Context.SE_APP_TEMPLATE_DTL.Where(Function(p) itemDeletes.Contains(p.ID))
            For Each item As SE_APP_TEMPLATE_DTL In items
                Context.SE_APP_TEMPLATE_DTL.DeleteObject(item)
            Next

            Context.SaveChanges()
            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Kiểm tra thêm mới cấp độ
    ''' </summary>
    ''' <param name="level"></param>
    ''' <param name="idExclude"></param>
    ''' <param name="idTemplate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CheckLevelInsert(ByVal level As Decimal, ByVal idExclude As Decimal, ByVal idTemplate As Decimal) As Boolean
        Try
            Dim item = Context.SE_APP_TEMPLATE_DTL.Where(Function(p) p.APP_LEVEL = level And p.TEMPLATE_ID = idTemplate)

            If idExclude <> 0 Then
                item = item.Where(Function(p) p.ID <> idExclude)
            End If

            If item.Count > 0 Then
                Return False
            End If

            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "Setup Approve Ext - Thiết lập người thay thế"
    Public Function InsertApproveSetupExt(ByVal item As ApproveSetupExtDTO, ByVal log As UserLog) As Boolean
        Try
            Dim itemInsert As New SE_APP_SETUPEXT With {
                .ID = Utilities.GetNextSequence(Context, Context.SE_APP_SETUPEXT.EntitySet.Name),
                .EMPLOYEE_ID = item.EMPLOYEE_ID,
                .SUB_EMPLOYEE_ID = item.SUB_EMPLOYEE_ID,
                .PROCESS_ID = item.PROCESS_ID,
                .FROM_DATE = item.FROM_DATE,
                .TO_DATE = item.TO_DATE,
                .CREATED_BY = log.Username,
                .CREATED_LOG = log.ComputerName,
                .CREATED_DATE = Date.Now,
                .MODIFIED_BY = log.Username,
                .MODIFIED_DATE = Date.Now,
                .REPLACEALL = item.REPALCEALL,
                .MODIFIED_LOG = log.ComputerName
            }

            Context.SE_APP_SETUPEXT.AddObject(itemInsert)

            Context.SaveChanges()
            Dim check = Context.SE_APP_SETUPEXT.OrderByDescending(Function(p) p.ID).FirstOrDefault().ID
            UpdateIfCheckReplace(check)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function UpdateApproveSetupExt(ByVal item As ApproveSetupExtDTO, ByVal log As UserLog) As Boolean
        Try

            Dim itemUpdate = Context.SE_APP_SETUPEXT.FirstOrDefault(Function(p) p.ID = item.ID)

            If itemUpdate IsNot Nothing Then
                With itemUpdate
                    .PROCESS_ID = item.PROCESS_ID
                    .SUB_EMPLOYEE_ID = item.SUB_EMPLOYEE_ID
                    .FROM_DATE = item.FROM_DATE
                    .TO_DATE = item.TO_DATE
                    .REPLACEALL = item.REPALCEALL
                    .MODIFIED_BY = log.Username
                    .MODIFIED_DATE = Date.Now
                    .MODIFIED_LOG = log.ComputerName
                End With

                Context.SaveChanges()
            End If
            UpdateIfCheckReplace(item.ID)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function UpdateIfCheckReplace(ByVal check As Integer) As Boolean
        Try
            Dim ktra = Context.SE_APP_SETUPEXT.FirstOrDefault(Function(p) p.ID = check).REPLACEALL
            If ktra = -1 Then
                Dim s1 = Context.SE_APP_SETUPEXT.FirstOrDefault(Function(p) p.ID = check).EMPLOYEE_ID
                Dim s2 = Context.SE_APP_SETUPEXT.FirstOrDefault(Function(p) p.ID = check).SUB_EMPLOYEE_ID

                Dim itemUpdate = Context.SE_APP_TEMPLATE_DTL.FirstOrDefault(Function(p) p.APP_ID = s1)
                If itemUpdate IsNot Nothing Then
                    With itemUpdate
                        .APP_ID = s2
                    End With
                    Context.SaveChanges()
                End If
            End If

        Catch ex As Exception

        End Try
    End Function
    Public Function GetApproveSetupExt(ByVal id As Decimal) As ApproveSetupExtDTO
        Try
            Dim itemReturn = (From item In Context.SE_APP_SETUPEXT
                              From empExt In Context.HU_EMPLOYEE.Where(Function(f) f.ID = item.SUB_EMPLOYEE_ID).DefaultIfEmpty
                              From proc In Context.SE_APP_PROCESS.Where(Function(f) f.ID = item.PROCESS_ID)
                              Where item.ID = id
                              Select New ApproveSetupExtDTO With {
                                .ID = item.ID,
                                .EMPLOYEE_ID = item.EMPLOYEE_ID,
                                .SUB_EMPLOYEE_ID = item.SUB_EMPLOYEE_ID,
                                .SUB_EMPLOYEE_CODE = empExt.EMPLOYEE_CODE,
                                .SUB_EMPLOYEE_NAME = empExt.FULLNAME_VN,
                                .PROCESS_ID = item.PROCESS_ID,
                                .PROCESS_NAME = proc.NAME,
                                .FROM_DATE = item.FROM_DATE,
                                .TO_DATE = item.TO_DATE,
                                .REPALCEALL = item.REPLACEALL
                              }).FirstOrDefault

            Return itemReturn

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetApproveSetupExtList(ByVal employeeId As Decimal,
                                           Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ApproveSetupExtDTO)
        Try
            Dim itemReturn = (From item In Context.SE_APP_SETUPEXT
                              From empExt In Context.HU_EMPLOYEE.Where(Function(f) f.ID = item.SUB_EMPLOYEE_ID).DefaultIfEmpty
                              From proc In Context.SE_APP_PROCESS.Where(Function(f) f.ID = item.PROCESS_ID)
                              Where item.EMPLOYEE_ID = employeeId
                              Select New ApproveSetupExtDTO With {
                                .ID = item.ID,
                                .EMPLOYEE_ID = item.EMPLOYEE_ID,
                                .SUB_EMPLOYEE_ID = item.SUB_EMPLOYEE_ID,
                                .SUB_EMPLOYEE_CODE = empExt.EMPLOYEE_CODE,
                                .SUB_EMPLOYEE_NAME = empExt.FULLNAME_VN,
                                .PROCESS_ID = item.PROCESS_ID,
                                .PROCESS_NAME = proc.NAME,
                                .FROM_DATE = item.FROM_DATE,
                                .TO_DATE = item.TO_DATE
                              }).OrderBy(Function(p) Sorts).ToList

            Return itemReturn

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function DeleteApproveSetupExt(ByVal itemDeletes As List(Of Decimal)) As Boolean
        Try
            Dim items = Context.SE_APP_SETUPEXT.Where(Function(p) itemDeletes.Contains(p.ID))

            For Each item As SE_APP_SETUPEXT In items
                Context.SE_APP_SETUPEXT.DeleteObject(item)
            Next

            Context.SaveChanges()

            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function IsExistSetupExtByDate(ByVal itemCheck As ApproveSetupExtDTO, Optional ByVal idExclude As Decimal? = Nothing) As Boolean
        Try

            Dim fromDate As Date = itemCheck.FROM_DATE
            Dim toDate As Date = itemCheck.TO_DATE

            Dim employeeId As Decimal = itemCheck.EMPLOYEE_ID
            Dim processId As Decimal = itemCheck.PROCESS_ID

            Dim tempCheck = From a In Context.SE_APP_SETUPEXT
                            Where _
                            a.EMPLOYEE_ID = employeeId _
                            AndAlso a.PROCESS_ID = processId _
                            AndAlso (Not idExclude.HasValue OrElse (idExclude.HasValue AndAlso a.ID <> idExclude)) _
                            AndAlso _
                            ( _
                                (a.FROM_DATE <= fromDate AndAlso fromDate <= a.TO_DATE) OrElse _
                                (a.FROM_DATE <= toDate AndAlso toDate <= a.TO_DATE) OrElse _
                                (fromDate <= a.FROM_DATE AndAlso a.FROM_DATE <= toDate) OrElse _
                                (fromDate <= a.TO_DATE AndAlso a.TO_DATE <= toDate) _
                            )

            If tempCheck.Count() > 0 Then
                Return False
            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "Lấy thông tin về quy trình phê duyệt cho nhân viên khi đăng ký"

    ''' <summary>
    ''' Lấy danh sách người phê duyệt đối với nhân viên đang xử lý
    ''' </summary>
    ''' <param name="employeeId">ID của nhân viên cần lấy</param>
    ''' <param name="processCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetApproveUsers(ByVal employeeId As Decimal, ByVal processCode As String,
                                    Optional ByVal lstOrg As List(Of Decimal) = Nothing,
                                    Optional ByVal isTimesheet As Boolean = False) As List(Of ApproveUserDTO)
        Try
            Dim listResult As New List(Of ApproveUserDTO)

            Dim process = Context.SE_APP_PROCESS.FirstOrDefault(Function(p) p.PROCESS_CODE = processCode)

            If process Is Nothing Then
                Throw New Exception("Chưa thiết lập quy trình phê duyệt HOẶC Mã quy trình phê duyệt sai.")
            End If

            'Lấy template phê duyệt đang áp dụng cho nhân viên
            Dim usingSetups As List(Of SE_APP_SETUP) = GetCurrentEmployeeSetup(employeeId, process, lstOrg, isTimesheet)

            If usingSetups.Count > 0 Then

                Dim firstTemplate = (From p In usingSetups
                                     From temp In Context.SE_APP_TEMPLATE.Where(Function(f) f.ID = p.TEMPLATE_ID)
                                     Select temp.TEMPLATE_ORDER
                                     Order By TEMPLATE_ORDER Descending).FirstOrDefault()

                Dim usingTemplateDetail As List(Of SE_APP_TEMPLATE_DTL) = (From p In Context.SE_APP_TEMPLATE_DTL
                                                                           Where p.TEMPLATE_ID = firstTemplate).ToList()

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
            Throw ex
        End Try
    End Function

#Region "Lấy người phê duyệt theo từng cấp của nhân viên"
    Private Function GetDirectManagerApprove(ByVal employeeId As Decimal, ByVal level As Integer) As ApproveUserDTO
        Dim employee = Context.HU_EMPLOYEE.FirstOrDefault(Function(p) p.ID = employeeId)

        If employee.DIRECT_MANAGER.HasValue Then
            Dim approveUser = (From cv In Context.HU_EMPLOYEE_CV
                               From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = cv.EMPLOYEE_ID)
                               Where cv.EMPLOYEE_ID = employee.DIRECT_MANAGER).FirstOrDefault

            If approveUser IsNot Nothing Then
                Return New ApproveUserDTO With {
                    .EMPLOYEE_ID = approveUser.cv.EMPLOYEE_ID,
                    .EMPLOYEE_NAME = approveUser.e.FULLNAME_VN,
                    .EMAIL = approveUser.cv.WORK_EMAIL,
                    .LEVEL = level
                }
            Else
                Return Nothing
            End If
        Else
            Return Nothing
        End If
        Return Nothing
    End Function

    Private Function GetEmployeeApprove(ByVal employeeId As Decimal, ByVal level As Integer) As ApproveUserDTO

        Dim approveUser = (From p In Context.HU_EMPLOYEE_CV
                           From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                           Where p.EMPLOYEE_ID = employeeId
                           Select New ApproveUserDTO With {.EMPLOYEE_ID = employeeId,
                                                           .EMPLOYEE_NAME = e.FULLNAME_VN,
                                                           .EMAIL = p.WORK_EMAIL,
                                                           .LEVEL = level
                                                          }).FirstOrDefault()

        Return approveUser

    End Function
#End Region

#Region "Lấy thông tin thiết lập phê duyệt cho nhân viên"
    Private Function GetCurrentEmployeeSetup(ByVal employeeId As Decimal, ByVal process As SE_APP_PROCESS,
                                    Optional ByVal lstOrg As List(Of Decimal) = Nothing,
                                    Optional ByVal isTimesheet As Boolean = False) As List(Of SE_APP_SETUP)

        Dim _setup As New List(Of SE_APP_SETUP)
        Dim setupEmployee = Context.SE_APP_SETUP.FirstOrDefault(Function(p) p.EMPLOYEE_ID = employeeId _
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
                        Org = Context.HU_ORGANIZATION.FirstOrDefault(Function(p) p.ID = Org.PARENT_ID.Value)
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
                        Org = Context.HU_ORGANIZATION.FirstOrDefault(Function(p) p.ID = Org.PARENT_ID.Value)
                        Dim OrgSetup = GetCurrentOrgSetup(Org.ID, process)
                        If OrgSetup IsNot Nothing Then
                            _setup.Add(OrgSetup)
                        End If
                    End While
                Next
            End If
        Else
            ' lấy ORG hiện tại của nhân viên (lấy trong HU_WORKING)
            Dim Org = (From p In Context.HU_EMPLOYEE
                       From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID)
                       Where p.ID = employeeId
                       Select o).FirstOrDefault

            Dim currentOrgSetup = GetCurrentOrgSetup(Org.ID, process)

            If currentOrgSetup IsNot Nothing Then
                _setup.Add(currentOrgSetup)
            End If

            While Org.PARENT_ID.HasValue

                Org = Context.HU_ORGANIZATION.FirstOrDefault(Function(p) p.ID = Org.PARENT_ID.Value)

                Dim OrgSetup = GetCurrentOrgSetup(Org.ID, process)
                If OrgSetup IsNot Nothing Then
                    _setup.Add(OrgSetup)
                End If
            End While
        End If


        Return _setup
    End Function

    Private Function GetCurrentOrgSetup(ByVal orgId As Decimal, ByVal process As SE_APP_PROCESS) As SE_APP_SETUP

        Dim setupOrg = Context.SE_APP_SETUP.FirstOrDefault(Function(p) p.ORG_ID.HasValue _
                                                                AndAlso p.ORG_ID = orgId _
                                                                AndAlso p.PROCESS_ID = process.ID _
                                                                AndAlso (p.FROM_DATE <= Date.Now _
                                                                         AndAlso (Not p.TO_DATE.HasValue _
                                                                                  OrElse (p.TO_DATE.HasValue _
                                                                                          AndAlso Date.Now <= p.TO_DATE.Value))))

        'Nếu có thiết lập riêng cho nhân viên
        Return setupOrg

    End Function
#End Region


#End Region

    Public Function GetListEmployee(ByVal _orgIds As List(Of Decimal), Optional ByVal is_ter As Decimal = 0) As List(Of EmployeeDTO)
        Dim query As ObjectQuery(Of EmployeeDTO)
        Dim str As String = "Kiêm nhiệm"
        If is_ter <> 0 Then
            query = (From p In Context.HU_EMPLOYEE
                     From t In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
                     From emp_stt In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.EMP_STATUS).DefaultIfEmpty
                     From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                     Where _orgIds.Contains(p.ORG_ID) And p.CONTRACT_ID.HasValue
                     Order By p.EMPLOYEE_CODE
                     Select New EmployeeDTO With {
                      .EMPLOYEE_CODE = p.EMPLOYEE_CODE,
                      .ID = p.ID,
                      .FULLNAME_VN = p.FULLNAME_VN,
                         .ORG_NAME = o.NAME_VN,
                        .TITLE_NAME_VN = t.NAME_VN,
                        .EMP_STATUS = If(p.IS_KIEM_NHIEM IsNot Nothing, str, emp_stt.NAME_VN)
                     })
        Else
            query = (From p In Context.HU_EMPLOYEE
                     From t In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
                     From emp_stt In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.EMP_STATUS).DefaultIfEmpty
                     From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                     Where _orgIds.Contains(p.ORG_ID) And
                         ((p.WORK_STATUS.HasValue And p.WORK_STATUS <> CommonCommon.OT_WORK_STATUS.TERMINATE_ID) Or Not p.WORK_STATUS.HasValue)
                     Order By p.EMPLOYEE_CODE
                     Select New EmployeeDTO With {
                      .EMPLOYEE_CODE = p.EMPLOYEE_CODE,
                      .ID = p.ID,
                      .FULLNAME_VN = p.FULLNAME_VN,
                         .ORG_NAME = o.NAME_VN,
                        .TITLE_NAME_VN = t.NAME_VN,
                        .EMP_STATUS = If(p.IS_KIEM_NHIEM.HasValue, str, emp_stt.NAME_VN)
                     })

            'p.CONTRACT_ID.HasValue And
        End If

        Return query.ToList

    End Function
    Public Function GetTitleByRank_ID(ByVal lst_Rank As List(Of OtherListDTO)) As List(Of TitleDTO)
        Try
            Dim listTitleT As New List(Of TitleDTO)
            For Each item In lst_Rank
                Dim listTitle = (From t In Context.HU_TITLE
                                 From level In Context.OT_OTHER_LIST.Where(Function(f) f.ID = t.TITLE_GROUP_ID And f.TYPE_CODE = "HU_TITLE_GROUP")
                                 Where (level.ID = item.ID And t.ACTFLG = "A")
                                 Select New TitleDTO With {
                                    .ID = t.ID,
                                    .CODE = t.CODE,
                .NAME_VN = t.NAME_VN
                                 }).ToList()
                listTitleT.AddRange(listTitle)
            Next

            Return listTitleT
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetTitleAll() As List(Of TitleDTO)
        Try
            Dim listTitle = (From t In Context.HU_TITLE
                             Where t.ACTFLG = "A"
                             Select New TitleDTO With {
                                    .ID = t.ID,
                                    .CODE = t.CODE,
                                    .NAME_VN = t.NAME_VN
                                 }).ToList()

            Return listTitle
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#Region "Work Process"

    Public Function GetHRProcessList(ByVal _filter As HR_PROCESS_DTO,
                                      ByVal _param As ParamDTO,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByVal Sorts As String = "CREATED_DATE DESC", Optional ByVal log As UserLog = Nothing) As DataTable
        Try
            Dim WHERE_CONDITION As String = " AND 1=1 "

            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                WHERE_CONDITION &= " AND ( UPPER(X.EMPLOYEE_CODE) LIKE  '%" & _filter.EMPLOYEE_CODE.ToUpper() & "%'"
                WHERE_CONDITION &= " OR UPPER(X.EMPLOYEE_NAME) LIKE '%" & _filter.EMPLOYEE_CODE.ToUpper() & "%')"
            End If
            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_NAME) Then
                WHERE_CONDITION &= " AND UPPER(X.EMPLOYEE_NAME) LIKE '%" & _filter.EMPLOYEE_NAME.ToUpper() & "%'"
            End If
            If Not String.IsNullOrEmpty(_filter.PROCESS_TYPE_NAME) Then
                WHERE_CONDITION &= " AND UPPER(X.PROCESS_TYPE_NAME) LIKE '%" & _filter.PROCESS_TYPE_NAME.ToUpper() & "%'"
            End If
            If Not String.IsNullOrEmpty(_filter.STATUS_NAME) Then
                WHERE_CONDITION &= " AND UPPER(X.STATUS_NAME) LIKE '%" & _filter.STATUS_NAME.ToUpper() & "%'"
            End If

            If _filter.FROM_DATE.HasValue Then
                WHERE_CONDITION &= " AND TO_CHAR(X.CREATED_DATE,'YYYYMMDD') >= '" & _filter.FROM_DATE.Value.ToString("yyyyMMdd") & "'"
            End If

            If _filter.CREATED_DATE.HasValue Then
                WHERE_CONDITION &= " AND TO_CHAR(X.CREATED_DATE,'YYYYMMDD') = '" & _filter.CREATED_DATE.Value.ToString("yyyyMMdd") & "'"
            End If

            If _filter.MODIFIED_DATE.HasValue Then
                WHERE_CONDITION &= " AND TO_CHAR(X.MODIFIED_DATE,'YYYYMMDD') = '" & _filter.MODIFIED_DATE.Value.ToString("yyyyMMdd") & "'"
            End If

            If _filter.TO_DATE.HasValue Then
                WHERE_CONDITION &= " AND TO_CHAR(X.CREATED_DATE,'YYYYMMDD') <= '" & _filter.TO_DATE.Value.ToString("yyyyMMdd") & "'"
            End If

            If _filter.PROCESS_TYPE_ID.HasValue Then
                WHERE_CONDITION &= " AND X.PROCESS_TYPE_ID =" & _filter.PROCESS_TYPE_ID.ToString.Substring(0, 1)
            End If

            If _filter.ID <> 0 Then
                WHERE_CONDITION &= " AND X.ID =" & _filter.ID.ToString.Replace(",0", "").Replace(".0", "")
            End If

            If _filter.STATUS.HasValue AndAlso _filter.STATUS.Value >= 0 Then
                WHERE_CONDITION &= " AND X.STATUS  = " & If(_filter.STATUS < 0, _filter.STATUS.ToString.Substring(0, 2), _filter.STATUS.ToString.Substring(0, 1))
            End If

            If Not String.IsNullOrEmpty(_filter.CREATED_BY) Then
                WHERE_CONDITION &= " AND UPPER(X.CREATED_BY) LIKE '%" & _filter.CREATED_BY.ToUpper() & "%'"
            End If

            WHERE_CONDITION &= " ORDER BY " & Sorts

            Using cls As New DataAccess.QueryData
                Dim dsData As DataSet = cls.ExecuteStore("PKG_AT_PROCESS.GET_HR_PROCESS_DATA",
                                               New With {.P_USERNAME = log.Username.ToUpper,
                                                         .P_ORG_ID = _param.ORG_ID,
                                                         .P_ISDISSOLVE = _param.IS_DISSOLVE,
                                                         .P_EMPLOYEE_ID = _filter.EMPLOYEE_ID,
                                                         .P_PAGE_INDEX = PageIndex + 1,
                                                         .P_PAGE_SIZE = PageSize,
                                                         .P_WHERE_CONDITION = WHERE_CONDITION,
                                                         .P_CUR = cls.OUT_CURSOR,
                                                         .P_CURCOUNT = cls.OUT_CURSOR}, False)
                Total = dsData.Tables(1).Rows(0)("TOTAL")
                Return dsData.Tables(0)
            End Using
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetHRProcessList")
            Return New DataTable
        End Try
    End Function

    Public Function GetPeWorkProcessByID(ByVal id As Decimal) As PE_WORK_PROCESSDTO
        Try
            Dim query = (From p In Context.PE_WORK_PROCESS
                         From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                         From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                         From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                         From s In Context.HU_JOB_BAND.Where(Function(f) f.ID = e.STAFF_RANK_ID).DefaultIfEmpty
                         From m In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.MANAGER_ID).DefaultIfEmpty
                         Where p.ID = id
                         Select New PE_WORK_PROCESSDTO With {
                                .ID = p.ID,
                                .EMPLOYEE_ID = p.EMPLOYEE_ID,
                                .EMPLOYEE_NAME = e.FULLNAME_VN,
                                .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                                .STAFF_RANK_ID = e.STAFF_RANK_ID,
                                .STAFF_RANK_NAME = s.LEVEL_FROM,
                                .TITLE_NAME = t.NAME_VN,
                                .ORG_NAME = o.NAME_VN,
                                .JOIN_DATE = e.JOIN_DATE,
                                .PROBLEMS = p.PROBLEMS,
                                .REQUESTS = p.REQUESTS,
                                .TYPE_ID = p.TYPE_ID,
                                .MANAGER_ID = p.MANAGER_ID,
                                .MANAGER_CODE = m.EMPLOYEE_CODE,
                                .MANAGER_NAME = m.FULLNAME_VN,
                                .IS_BONHIEM = p.IS_BONHIEM,
                                .IS_CHANGE_SAL = p.IS_CHANGE_SAL,
                                .IS_CHANGE_TITLE = p.IS_CHANGE_TITLE,
                                .IS_THOINHIEM = p.IS_THOINHIEM,
                                .STATUS = p.STATUS,
                                .CREATED_DATE = p.CREATED_DATE,
                                .NOTE = p.NOTE}).FirstOrDefault

            Dim lstEmp = (From p In Context.PE_WORK_EMPLOYEE Where p.PROCESS_ID = id
                          Select New PE_WORK_EMPLOYEEDTO With {
                             .ID = p.ID,
                             .WORK_NAME = p.WORK_NAME,
                             .RESULT = p.RESULT,
                             .PROCESS_ID = p.PROCESS_ID}).ToList

            query.lstWorkEmp = lstEmp
            Return query
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetPeWorkProcessByID")
            Throw ex
        End Try
    End Function

    Public Function GetPeWorkEmployeeByProcess(ByVal id As Decimal) As List(Of PE_WORK_EMPLOYEEDTO)
        Try
            Dim lstEmp = (From p In Context.PE_WORK_EMPLOYEE Where p.PROCESS_ID = id
                          Select New PE_WORK_EMPLOYEEDTO With {
                             .ID = p.ID,
                             .WORK_NAME = p.WORK_NAME,
                             .RESULT = p.RESULT,
                             .PROCESS_ID = p.PROCESS_ID}).ToList
            Return lstEmp.ToList
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetPeWorkEmployeeByProcess")
            Throw ex
        End Try
    End Function

    Public Function GetPeWorkManByProcess(ByVal id As Decimal) As List(Of PE_WORK_MANAGERDTO)
        Try

            Dim check = From p In Context.PE_WORK_MANAGER Where p.PROCESS_ID = id
            If check.Any Then
                Dim query = From p In Context.PE_WORK_MANAGER
                            From o In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.WORK_ID).DefaultIfEmpty
                            Where p.PROCESS_ID = id
                            Select New PE_WORK_MANAGERDTO With {
                                .ID = p.ID,
                                .WORK_ID = p.WORK_ID,
                                .WORK_NAME = If(p.WORK_ID Is Nothing, p.WORK_NAME, o.NAME_VN),
                                .RESULT = p.RESULT,
                                .REMARK = p.REMARK,
                                .PROCESS_ID = p.PROCESS_ID}
                Return query.ToList
            Else
                Dim query = From p In Context.OT_OTHER_LIST
                            From t In Context.OT_OTHER_LIST_TYPE.Where(Function(f) f.ID = p.TYPE_ID)
                            Where p.ACTFLG = "A" And t.CODE = "PE_WORK_PROCESS_MAN"
                            Select New PE_WORK_MANAGERDTO With {
                                .WORK_ID = p.ID,
                                .WORK_NAME = p.NAME_VN,
                                .RESULT = 0}
                Return query.ToList
            End If
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetPeWorkManByProcess")
            Throw ex
        End Try
    End Function

    Public Function GetPeWorkManConcludeByProcess(ByVal id As Decimal) As PE_WORK_MANAGER_CONCLUDEDTO
        Try

            Dim query = From p In Context.PE_WORK_MANAGER_CONCLUDE
                        Where p.PROCESS_ID = id
                        Select New PE_WORK_MANAGER_CONCLUDEDTO With {
                            .ID = p.ID,
                            .PROCESS_ID = p.PROCESS_ID,
                            .CONCLUDE_TYPE = p.CONCLUDE_TYPE,
                            .DATE_PROBATION = p.DATE_PROBATION,
                            .CONTRACT_TYPE = p.CONTRACT_TYPE,
                            .APOINT_TITLE = p.APOINT_TITLE,
                            .APOINT_OTHER = p.APOINT_OTHER,
                            .MOVE_TITLE = p.MOVE_TITLE,
                            .MOVE_OTHER = p.MOVE_OTHER,
                            .IS_SAL_CHANGE = p.IS_SAL_CHANGE,
                            .IS_JOBBAND_CHANGE = p.IS_JOBBAND_CHANGE,
                            .SAL_SUGGET = p.SAL_SUGGET,
                            .IS_CHANGE_TITLE = p.IS_CHANGE_TITLE,
                            .CHANGE_TITLE = p.CHANGE_TITLE,
                            .CHANGE_OTHER = p.CHANGE_OTHER,
                            .STAFF_RANK = p.STAFF_RANK,
                            .IS_CONFIRM = p.IS_CONFIRM,
                            .NOTE = p.NOTE}

            Return query.FirstOrDefault
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetPeWorkManConcludeByProcess")
            Throw ex
        End Try
    End Function

    Public Function InsertPeWorkProcess(ByVal obj As PE_WORK_PROCESSDTO, ByVal log As UserLog) As Integer
        Dim objWorkProecss As New PE_WORK_PROCESS
        Dim id = Utilities.GetNextSequence(Context, Context.PE_WORK_PROCESS.EntitySet.Name)
        Try
            With objWorkProecss
                .ID = id
                .EMPLOYEE_ID = obj.EMPLOYEE_ID
                .STATUS = obj.STATUS
                .TYPE_ID = obj.TYPE_ID
                .PROBLEMS = obj.PROBLEMS
                .REQUESTS = obj.REQUESTS
            End With
            Context.PE_WORK_PROCESS.AddObject(objWorkProecss)
            If obj.lstWorkEmp IsNot Nothing Then
                For Each item In obj.lstWorkEmp
                    Context.PE_WORK_EMPLOYEE.AddObject(New PE_WORK_EMPLOYEE With {
                                                   .ID = Utilities.GetNextSequence(Context, Context.PE_WORK_EMPLOYEE.EntitySet.Name),
                                                   .PROCESS_ID = id,
                                                   .WORK_NAME = item.WORK_NAME,
                                                   .RESULT = item.RESULT})
                Next
            End If
            Context.SaveChanges(log)
            If obj.IS_SEND_APP = 1 Then
                Using cls As New DataAccess.QueryData
                    Dim priProcessApp = New With {.P_EMPLOYEE_ID = obj.EMPLOYEE_ID, .P_PERIOD_ID = 0, .P_PROCESS_TYPE = "WORK_PROCESS", .P_TOTAL_HOURS = 0, .P_TOTAL_DAY = 0, .P_SIGN_ID = 0, .P_ID_REGGROUP = id, .P_TOKEN = "", .P_RESULT = cls.OUT_NUMBER}
                    Dim store = cls.ExecuteStore("PKG_AT_PROCESS.PRI_PROCESS_APP", priProcessApp)
                    Dim outNumber As Integer = Int32.Parse(priProcessApp.P_RESULT)
                    If outNumber <> 0 Then
                        Dim lstID As New List(Of Decimal)
                        lstID.Add(id)
                        DeleteWorkProcess(lstID)
                    End If
                    Return outNumber
                End Using
            End If
            Return 0
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".InsertPeWorkProcess")
            Return -1
        End Try
    End Function

    Public Function ModifyPeWorkProcess(ByVal obj As PE_WORK_PROCESSDTO, ByVal log As UserLog) As Integer
        Try
            Dim objWorkProcess = (From p In Context.PE_WORK_PROCESS Where p.ID = obj.ID).FirstOrDefault
            objWorkProcess.TYPE_ID = obj.TYPE_ID
            objWorkProcess.PROBLEMS = obj.PROBLEMS
            objWorkProcess.REQUESTS = obj.REQUESTS
            If obj.lstWorkEmp IsNot Nothing Then
                Dim lstDel = (From p In Context.PE_WORK_EMPLOYEE Where p.PROCESS_ID = obj.ID)
                For Each item In lstDel
                    Context.PE_WORK_EMPLOYEE.DeleteObject(item)
                Next
                For Each item In obj.lstWorkEmp
                    Context.PE_WORK_EMPLOYEE.AddObject(New PE_WORK_EMPLOYEE With {
                                                   .ID = Utilities.GetNextSequence(Context, Context.PE_WORK_EMPLOYEE.EntitySet.Name),
                                                   .PROCESS_ID = obj.ID,
                                                   .WORK_NAME = item.WORK_NAME,
                                                   .RESULT = item.RESULT})
                Next
            End If
            Context.SaveChanges(log)
            If obj.IS_SEND_APP = 1 Then
                Using cls As New DataAccess.QueryData
                    Dim priProcessApp = New With {.P_EMPLOYEE_ID = obj.EMPLOYEE_ID, .P_PERIOD_ID = 0, .P_PROCESS_TYPE = "WORK_PROCESS", .P_TOTAL_HOURS = 0, .P_TOTAL_DAY = 0, .P_SIGN_ID = 0, .P_ID_REGGROUP = obj.ID, .P_TOKEN = "", .P_RESULT = cls.OUT_NUMBER}
                    Dim store = cls.ExecuteStore("PKG_AT_PROCESS.PRI_PROCESS_APP", priProcessApp)
                    Dim outNumber As Integer = Int32.Parse(priProcessApp.P_RESULT)
                    If outNumber <> 0 Then
                        Dim lstID As New List(Of Decimal)
                        lstID.Add(obj.ID)
                        DeleteWorkProcess(lstID)
                    End If
                    Return outNumber
                End Using
            End If
            Return 0
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".ModifyPeWorkProcess")
            Return -1
        End Try
    End Function

    Public Function DeleteWorkProcess(ByVal lstID As List(Of Decimal)) As Boolean
        Try
            Dim lstDel = From p In Context.PE_WORK_PROCESS Where lstID.Contains(p.ID)
            For Each item In lstDel
                Dim lstemp = From p In Context.PE_WORK_EMPLOYEE Where p.PROCESS_ID = item.ID
                For Each empItem In lstemp
                    Context.PE_WORK_EMPLOYEE.DeleteObject(empItem)
                Next
                Context.PE_WORK_PROCESS.DeleteObject(item)
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteWorkProcess")
            Throw ex
        End Try
    End Function

    Public Function ValidateWorkProcess(ByVal obj As PE_WORK_PROCESSDTO) As Boolean
        Try
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".ValidateWorkProcess")
            Throw ex
        End Try
    End Function

    Public Function InsertManagerConclude(ByVal obj As PE_WORK_MANAGER_CONCLUDEDTO, ByVal log As UserLog) As Boolean
        Dim objMan As New PE_WORK_MANAGER_CONCLUDE
        Dim id = Utilities.GetNextSequence(Context, Context.PE_WORK_PROCESS.EntitySet.Name)
        Try
            With objMan
                .ID = id
                .PROCESS_ID = obj.PROCESS_ID
                .CONCLUDE_TYPE = obj.CONCLUDE_TYPE
                .DATE_PROBATION = obj.DATE_PROBATION
                .CONTRACT_TYPE = obj.CONTRACT_TYPE
                .APOINT_TITLE = obj.APOINT_TITLE
                .APOINT_OTHER = obj.APOINT_OTHER
                .MOVE_TITLE = obj.MOVE_TITLE
                .MOVE_OTHER = obj.MOVE_OTHER
                .IS_SAL_CHANGE = obj.IS_SAL_CHANGE
                .SAL_SUGGET = obj.SAL_SUGGET
                .IS_CHANGE_TITLE = obj.IS_CHANGE_TITLE
                .CHANGE_TITLE = obj.CHANGE_TITLE
                .CHANGE_OTHER = obj.CHANGE_OTHER
                .STAFF_RANK = obj.STAFF_RANK
                .IS_CONFIRM = obj.IS_CONFIRM
                .IS_JOBBAND_CHANGE = obj.IS_JOBBAND_CHANGE
                .NOTE = obj.NOTE
            End With
            Context.PE_WORK_MANAGER_CONCLUDE.AddObject(objMan)
            If obj.lstWorkMan IsNot Nothing Then
                For Each item In obj.lstWorkMan
                    Dim id1 = Utilities.GetNextSequence(Context, Context.PE_WORK_MANAGER.EntitySet.Name)
                    Context.PE_WORK_MANAGER.AddObject(New PE_WORK_MANAGER With {
                                                   .ID = id1,
                                                   .PROCESS_ID = obj.PROCESS_ID,
                                                   .WORK_ID = item.WORK_ID,
                                                   .WORK_NAME = item.WORK_NAME,
                                                   .RESULT = item.RESULT,
                                                   .REMARK = item.REMARK})
                Next
            End If
            If obj.STATUS = 1 AndAlso (Not obj.IS_SAL_CHANGE.HasValue OrElse obj.IS_SAL_CHANGE.Value = 0) AndAlso (Not obj.IS_CHANGE_TITLE.HasValue OrElse obj.IS_CHANGE_TITLE.Value = 0) Then
                Using cls As New DataAccess.QueryData
                    Dim priProcessApp = New With {.P_ID_REGGROUP = obj.PROCESS_ID, .P_PROCESS_TYPE = "WORK_PROCESS", .P_OUT = cls.OUT_NUMBER}
                    Dim store = cls.ExecuteStore("PKG_AT_PROCESS.TRIM_PROCESS", priProcessApp)
                    Dim outNumber As Integer = Int32.Parse(priProcessApp.P_OUT)
                    If outNumber <> 1 Then
                        Return False
                    End If
                End Using
            End If

            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".InsertManagerEvaluate")
            Throw ex
        End Try
    End Function

    Public Function ModifyManagerConclude(ByVal obj As PE_WORK_MANAGER_CONCLUDEDTO, ByVal log As UserLog) As Boolean
        Try
            Dim objMan = (From p In Context.PE_WORK_MANAGER_CONCLUDE Where p.ID = obj.ID).FirstOrDefault
            objMan.CONCLUDE_TYPE = obj.CONCLUDE_TYPE
            objMan.DATE_PROBATION = obj.DATE_PROBATION
            objMan.CONTRACT_TYPE = obj.CONTRACT_TYPE
            objMan.APOINT_TITLE = obj.APOINT_TITLE
            objMan.APOINT_OTHER = obj.APOINT_OTHER
            objMan.MOVE_TITLE = obj.MOVE_TITLE
            objMan.MOVE_OTHER = obj.MOVE_OTHER
            objMan.IS_SAL_CHANGE = obj.IS_SAL_CHANGE
            objMan.IS_JOBBAND_CHANGE = obj.IS_JOBBAND_CHANGE
            objMan.SAL_SUGGET = obj.SAL_SUGGET
            objMan.IS_CHANGE_TITLE = obj.IS_CHANGE_TITLE
            objMan.CHANGE_TITLE = obj.CHANGE_TITLE
            objMan.CHANGE_OTHER = obj.CHANGE_OTHER
            objMan.STAFF_RANK = obj.STAFF_RANK
            objMan.IS_CONFIRM = obj.IS_CONFIRM
            objMan.NOTE = obj.NOTE
            If obj.lstWorkMan IsNot Nothing Then
                Dim lstDel = (From p In Context.PE_WORK_MANAGER Where p.PROCESS_ID = obj.ID)
                For Each item In lstDel
                    Context.PE_WORK_MANAGER.DeleteObject(lstDel)
                Next
                For Each item In obj.lstWorkMan
                    Dim id1 = Utilities.GetNextSequence(Context, Context.PE_WORK_MANAGER.EntitySet.Name)
                    Context.PE_WORK_MANAGER.AddObject(New PE_WORK_MANAGER With {
                                                   .ID = id1,
                                                   .PROCESS_ID = obj.PROCESS_ID,
                                                   .WORK_ID = item.WORK_ID,
                                                   .WORK_NAME = item.WORK_NAME,
                                                   .RESULT = item.RESULT,
                                                   .REMARK = item.REMARK})
                Next
            End If
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".ModifyManagerConclude")
            Throw ex
        End Try
    End Function

    Public Function HRProcessCancel(ByVal id As Decimal, ByVal process As String, ByVal log As UserLog) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                Dim priProcessApp = New With {.P_ID_REGGROUP = id, .P_USERNAME = log.Username.ToUpper, .P_PROCESS_TYPE = process, .P_OUT = cls.OUT_NUMBER}
                Dim store = cls.ExecuteStore("PKG_AT_PROCESS.HR_PROCESS_CANCEL", priProcessApp)
                Dim outNumber As Integer = Int32.Parse(priProcessApp.P_OUT)
                If outNumber <> 1 Then
                    Return False
                End If
            End Using
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".HRProcessCancel")
            Throw ex
        End Try
    End Function

    Public Function HRProcessChangeAssignee(ByVal id As Decimal, ByVal _empID As Decimal) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                Dim priProcessApp = New With {.P_ID = id, .P_EMP_APP = _empID, .P_OUT = cls.OUT_NUMBER}
                Dim store = cls.ExecuteStore("PKG_AT_PROCESS.HR_PROCESS_CHANGE_ASSIGNEE", priProcessApp)
                Dim outNumber As Integer = Int32.Parse(priProcessApp.P_OUT)
                If outNumber <> 1 Then
                    Return False
                End If
            End Using
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".HRProcessChangeAssignee")
            Throw ex
        End Try
    End Function

    Public Function GetDataPrintWorkProcess(ByVal _Id_reggroup As Decimal) As DataSet
        Try
            Using cls As New DataAccess.QueryData
                Dim dsData As DataSet = cls.ExecuteStore("PKG_AT_PROCESS.PRINT_WORK_PROCESS",
                                               New With {.P_ID_REGGROUP = _Id_reggroup,
                                                         .P_CUR = cls.OUT_CURSOR,
                                                         .P_CUR1 = cls.OUT_CURSOR,
                                                         .P_CUR2 = cls.OUT_CURSOR,
                                                         .P_CUR3 = cls.OUT_CURSOR}, False)
                Return dsData
            End Using
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetDataPrintWorkProcess")
            Return New DataSet
        End Try
    End Function
    Public Function GetDataPrintManProcess(ByVal _Id_reggroup As Decimal) As DataSet
        Try
            Using cls As New DataAccess.QueryData
                Dim dsData As DataSet = cls.ExecuteStore("PKG_AT_PROCESS.PRINT_MAN_PROCESS",
                                               New With {.P_ID_REGGROUP = _Id_reggroup,
                                                         .P_CUR = cls.OUT_CURSOR,
                                                         .P_CUR1 = cls.OUT_CURSOR,
                                                         .P_CUR2 = cls.OUT_CURSOR,
                                                         .P_CUR3 = cls.OUT_CURSOR}, False)
                Return dsData
            End Using
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetDataPrintManProcess")
            Return New DataSet
        End Try
    End Function

    Public Function GetDataPrintTerProcess(ByVal _Id_reggroup As Decimal) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_AT_PROCESS.PRINT_TER_PROCESS",
                                               New With {.P_ID_REGGROUP = _Id_reggroup,
                                                         .P_CUR = cls.OUT_CURSOR})
                Return dtData
            End Using
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetDataPrintTerProcess")
            Return New DataTable
        End Try
    End Function

    Public Function GetTitlesByOrg2(ByVal _emp As Decimal, Optional ByVal _Is_Blank As Boolean = False) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_TITLES_BY_ORG",
                                               New With {.P_EMP_ID = _emp,
                                                         .P_ISBLANK = _Is_Blank,
                                                         .P_CUR = cls.OUT_CURSOR})
                Return dtData
            End Using
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetTitlesByOrg2")
            Return New DataTable
        End Try
    End Function
    Public Function GetEmpInfoNoty(ByVal empID As Decimal, ByVal processType As Decimal) As List(Of HR_PROCESS_DTO)
        Try
            Dim lstResult As New List(Of HR_PROCESS_DTO)
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_AT_PROCESS.GET_EMP_INFO_NOTI",
                                               New With {.P_EMP_ID = empID,
                                                         .P_TYPE = processType,
                                                         .P_CUR = cls.OUT_CURSOR})
                If dtData IsNot Nothing AndAlso dtData.Rows.Count > 0 Then
                    lstResult = (From dr In dtData.Rows
                                 Select New HR_PROCESS_DTO With {
                                    .ID = CDec(Val(dr("ID"))),
                                    .EMPLOYEE_ID = CDec(Val(dr("EMPLOYEE_ID"))),
                                    .PROCESS_TYPE = dr("PROCESS_TYPE").ToString,
                                    .IS_READ = CDec(Val(dr("IS_READ"))),
                                    .NOTY_CONTENT = dr("NOTY_CONTENT").ToString}).ToList()
                End If
            End Using
            Return lstResult
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "ICommon")
            Throw ex
        End Try
    End Function

    Public Function GetEmpHrProcessNoty(ByVal empID As Decimal, ByVal processType As Decimal) As List(Of HR_PROCESS_DTO)
        Try
            Dim lstResult As New List(Of HR_PROCESS_DTO)
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_AT_PROCESS.GET_EMP_HR_PROCESS_NOTY",
                                               New With {.P_EMP_ID = empID,
                                                         .P_TYPE = processType,
                                                         .P_CUR = cls.OUT_CURSOR})
                If dtData IsNot Nothing AndAlso dtData.Rows.Count > 0 Then
                    lstResult = (From dr In dtData.Rows
                                 Select New HR_PROCESS_DTO With {
                                    .ID = dr("ID"),
                                    .EMPLOYEE_ID = dr("EMPLOYEE_ID"),
                                    .IS_READ = dr("IS_READ"),
                                    .NOTY_DATE = If(IsDBNull(dr("NOTY_DATE")), Nothing, CDate(dr("NOTY_DATE"))),
                                    .NOTY_CONTENT = dr("NOTY_CONTENT")}).ToList()
                End If
            End Using
            Return lstResult
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "ICommon")
            Throw ex
        End Try
    End Function

    Public Function ReadHRNoty(ByVal id As Decimal, ByVal processType As String) As Boolean
        Try
            If processType = "WORK_PROCESS" OrElse processType = "MAN_PROCESS" Then
                Dim objQ = (From p In Context.PE_WORK_PROCESS Where p.ID = id).FirstOrDefault
                If objQ IsNot Nothing Then
                    objQ.IS_READ = -1
                    objQ.NOTY_DATE = DateTime.Now
                    Context.SaveChanges()
                End If

            ElseIf processType = "TER_PROCESS" Then
                Dim objQ = (From p In Context.HU_TERMINATION_PROCESS Where p.ID = id).FirstOrDefault
                If objQ IsNot Nothing Then
                    objQ.IS_READ = -1
                    objQ.NOTY_DATE = DateTime.Now
                    Context.SaveChanges()
                End If
            ElseIf processType = "ATPROCESS" Then
                Dim objN = (From p In Context.SE_NOTIFICATION Where p.ID = id).FirstOrDefault
                If objN IsNot Nothing Then
                    objN.SEND_STATUS = 3
                    objN.SENT_DATE = DateTime.Now
                    objN.SEEN_DATE = DateTime.Now
                    Context.SaveChanges()
                End If
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "ICommon")
            Throw ex
        End Try
    End Function

    Public Function GetAppProcessNoty(ByVal empID As Decimal, ByVal processType As Decimal) As List(Of HR_PROCESS_DTO)
        Try
            Dim lstResult As New List(Of HR_PROCESS_DTO)
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_AT_PROCESS.GET_APP_HR_PROCESS_NOTY",
                                               New With {.P_EMP_ID = empID,
                                                         .P_TYPE = processType,
                                                         .P_CUR = cls.OUT_CURSOR})
                If dtData IsNot Nothing AndAlso dtData.Rows.Count > 0 Then
                    lstResult = (From dr In dtData.Rows
                                 Select New HR_PROCESS_DTO With {
                                    .ID = dr("ID"),
                                    .EMPLOYEE_ID = dr("EMPLOYEE_ID"),
                                    .NOTY_CONTENT = dr("NOTY_CONTENT")}).ToList()
                End If
            End Using
            Return lstResult
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "ICommon")
            Throw ex
        End Try
    End Function
#End Region


#Region "Ter Process"

    Public Function GetTerProcessByID(ByVal id As Decimal) As HU_TERMINATION_PROCESSDTO
        Try
            Dim query = (From p In Context.HU_TERMINATION_PROCESS
                         From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                         From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                         From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                         From r In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.REASON_ID).DefaultIfEmpty
                         From s In Context.HU_JOB_BAND.Where(Function(f) f.ID = e.STAFF_RANK_ID).DefaultIfEmpty
                         Where p.ID = id
                         Select New HU_TERMINATION_PROCESSDTO With {
                                .ID = p.ID,
                                .EMPLOYEE_ID = p.EMPLOYEE_ID,
                                .EMPLOYEE_NAME = e.FULLNAME_VN,
                                .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                                .TITLE_NAME = t.NAME_VN,
                                .ORG_NAME = o.NAME_VN,
                                .STAFF_RANK_ID = e.STAFF_RANK_ID,
                                .STAFF_RANK_NAME = s.LEVEL_FROM,
                                .JOIN_DATE = e.JOIN_DATE,
                                .TIME_OVER = p.TIME_OVER,
                                .TIME_OVER_OTHER = p.TIME_OVER_OTHER,
                                .TER_LASTDATE = p.TER_LASTDATE,
                                .REASON_DETAIL = p.REASON_DETAIL,
                                .REASON_ID = p.REASON_ID,
                                .REASON_NAME = r.NAME_VN,
                                .IS_COMMIT = p.IS_COMMIT,
                                .STATUS = p.STATUS,
                                .CREATED_DATE = p.CREATED_DATE}).FirstOrDefault
            Return query
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetTerProcessByID")
            Throw ex
        End Try
    End Function

    Public Function InsertTerProcess(ByVal obj As HU_TERMINATION_PROCESSDTO, ByVal log As UserLog) As Integer
        Dim objProcess As New HU_TERMINATION_PROCESS
        Dim id = Utilities.GetNextSequence(Context, Context.HU_TERMINATION_PROCESS.EntitySet.Name)
        Try
            With objProcess
                .ID = id
                .EMPLOYEE_ID = obj.EMPLOYEE_ID
                .STATUS = obj.STATUS
                .TIME_OVER = obj.TIME_OVER
                .TIME_OVER_OTHER = obj.TIME_OVER_OTHER
                .TER_LASTDATE = obj.TER_LASTDATE
                .REASON_DETAIL = obj.REASON_DETAIL
                .REASON_ID = obj.REASON_ID
                .IS_COMMIT = obj.IS_COMMIT
            End With
            Context.HU_TERMINATION_PROCESS.AddObject(objProcess)
            Context.SaveChanges(log)
            If obj.IS_SEND_APP = 1 Then
                Using cls As New DataAccess.QueryData
                    Dim priProcessApp = New With {.P_EMPLOYEE_ID = obj.EMPLOYEE_ID, .P_PERIOD_ID = 0, .P_PROCESS_TYPE = "TER_PROCESS", .P_TOTAL_HOURS = 0, .P_TOTAL_DAY = 0, .P_SIGN_ID = 0, .P_ID_REGGROUP = id, .P_TOKEN = "", .P_RESULT = cls.OUT_NUMBER}
                    Dim store = cls.ExecuteStore("PKG_AT_PROCESS.PRI_PROCESS_APP", priProcessApp)
                    Dim outNumber As Integer = Int32.Parse(priProcessApp.P_RESULT)
                    If outNumber <> 0 Then
                        Dim lstID As New List(Of Decimal)
                        lstID.Add(id)
                        DeleteTerProcess(lstID)
                    End If
                    Return outNumber
                End Using
            End If
            Return 0
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".InsertTerProcess")
            Return -1
        End Try
    End Function

    Public Function ModifyTerProcess(ByVal obj As HU_TERMINATION_PROCESSDTO, ByVal log As UserLog) As Integer
        Try
            Dim objProcess = (From p In Context.HU_TERMINATION_PROCESS Where p.ID = obj.ID).FirstOrDefault
            objProcess.TIME_OVER = obj.TIME_OVER
            objProcess.TIME_OVER_OTHER = obj.TIME_OVER_OTHER
            objProcess.TER_LASTDATE = obj.TER_LASTDATE
            objProcess.REASON_DETAIL = obj.REASON_DETAIL
            objProcess.REASON_ID = obj.REASON_ID
            objProcess.IS_COMMIT = obj.IS_COMMIT

            Context.SaveChanges(log)
            If obj.IS_SEND_APP = 1 Then
                Using cls As New DataAccess.QueryData
                    Dim priProcessApp = New With {.P_EMPLOYEE_ID = obj.EMPLOYEE_ID, .P_PERIOD_ID = 0, .P_PROCESS_TYPE = "TER_PROCESS", .P_TOTAL_HOURS = 0, .P_TOTAL_DAY = 0, .P_SIGN_ID = 0, .P_ID_REGGROUP = obj.ID, .P_TOKEN = "", .P_RESULT = cls.OUT_NUMBER}
                    Dim store = cls.ExecuteStore("PKG_AT_PROCESS.PRI_PROCESS_APP", priProcessApp)
                    Dim outNumber As Integer = Int32.Parse(priProcessApp.P_RESULT)
                    If outNumber <> 0 Then
                        Dim lstID As New List(Of Decimal)
                        lstID.Add(obj.ID)
                        DeleteTerProcess(lstID)
                    End If
                    Return outNumber
                End Using
            End If
            Return 0
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".ModifyTerProcess")
            Return -1
        End Try
    End Function

    Public Function DeleteTerProcess(ByVal lstID As List(Of Decimal)) As Boolean
        Try
            Dim lstDel = From p In Context.HU_TERMINATION_PROCESS Where lstID.Contains(p.ID)
            For Each item In lstDel
                Context.HU_TERMINATION_PROCESS.DeleteObject(item)
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteTerProcess")
            Throw ex
        End Try
    End Function

    Public Function ValidateTerProcess(ByVal obj As HU_TERMINATION_PROCESSDTO) As Boolean
        Try
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".ValidateTerProcess")
            Throw ex
        End Try
    End Function

#End Region

#Region "Man Process"

    Public Function InsertManProcess(ByVal obj As PE_MANAGER_OFFER_DTO, ByVal log As UserLog) As Integer
        Dim objWorkProecss As New PE_WORK_PROCESS
        Dim objMan As New PE_WORK_MANAGER_CONCLUDE
        Dim processId = Utilities.GetNextSequence(Context, Context.PE_WORK_PROCESS.EntitySet.Name)
        Try
            Dim objProcess = obj.processDTO
            With objWorkProecss
                .ID = processId
                .EMPLOYEE_ID = objProcess.EMPLOYEE_ID
                .MANAGER_ID = objProcess.MANAGER_ID
                .STATUS = objProcess.STATUS
                .TYPE_ID = objProcess.TYPE_ID
                .PROBLEMS = objProcess.PROBLEMS
                .REQUESTS = objProcess.REQUESTS
                .IS_BONHIEM = objProcess.IS_BONHIEM
                .IS_CHANGE_SAL = objProcess.IS_CHANGE_SAL
                .IS_CHANGE_TITLE = objProcess.IS_CHANGE_TITLE
                .IS_THOINHIEM = objProcess.IS_THOINHIEM
                .NOTE = objProcess.NOTE
            End With
            Context.PE_WORK_PROCESS.AddObject(objWorkProecss)
            If objProcess.lstWorkEmp IsNot Nothing Then
                For Each item In objProcess.lstWorkEmp
                    Context.PE_WORK_EMPLOYEE.AddObject(New PE_WORK_EMPLOYEE With {
                                                   .ID = Utilities.GetNextSequence(Context, Context.PE_WORK_EMPLOYEE.EntitySet.Name),
                                                   .PROCESS_ID = processId,
                                                   .WORK_NAME = item.WORK_NAME,
                                                   .RESULT = item.RESULT})
                Next
            End If

            Dim manConcludeID = Utilities.GetNextSequence(Context, Context.PE_WORK_PROCESS.EntitySet.Name)
            Dim objManConclude = obj.manConcludeDTO
            With objMan
                .ID = manConcludeID
                .PROCESS_ID = processId
                .CONCLUDE_TYPE = objManConclude.CONCLUDE_TYPE
                .DATE_PROBATION = objManConclude.DATE_PROBATION
                .CONTRACT_TYPE = objManConclude.CONTRACT_TYPE
                .APOINT_TITLE = objManConclude.APOINT_TITLE
                .APOINT_OTHER = objManConclude.APOINT_OTHER
                .MOVE_TITLE = objManConclude.MOVE_TITLE
                .MOVE_OTHER = objManConclude.MOVE_OTHER
                .IS_SAL_CHANGE = objManConclude.IS_SAL_CHANGE
                .IS_JOBBAND_CHANGE = objManConclude.IS_JOBBAND_CHANGE
                .SAL_SUGGET = objManConclude.SAL_SUGGET
                .IS_CHANGE_TITLE = objManConclude.IS_CHANGE_TITLE
                .CHANGE_TITLE = objManConclude.CHANGE_TITLE
                .CHANGE_OTHER = objManConclude.CHANGE_OTHER
                .STAFF_RANK = objManConclude.STAFF_RANK
                .IS_CONFIRM = objManConclude.IS_CONFIRM
                .NOTE = objManConclude.NOTE
            End With
            Context.PE_WORK_MANAGER_CONCLUDE.AddObject(objMan)
            If objManConclude.lstWorkMan IsNot Nothing Then
                For Each item In objManConclude.lstWorkMan
                    Dim id1 = Utilities.GetNextSequence(Context, Context.PE_WORK_MANAGER.EntitySet.Name)
                    Context.PE_WORK_MANAGER.AddObject(New PE_WORK_MANAGER With {
                                                       .ID = id1,
                                                       .PROCESS_ID = processId,
                                                       .WORK_ID = item.WORK_ID,
                                                       .WORK_NAME = item.WORK_NAME,
                                                       .RESULT = item.RESULT,
                                                       .REMARK = item.REMARK})
                Next
            End If
            Context.SaveChanges(log)

            If objProcess.IS_SEND_APP = 1 Then
                Using cls As New DataAccess.QueryData
                    Dim priProcessApp = New With {.P_EMPLOYEE_ID = objProcess.EMPLOYEE_ID, .P_PERIOD_ID = 0, .P_PROCESS_TYPE = "MAN_PROCESS", .P_TOTAL_HOURS = 0, .P_TOTAL_DAY = 0, .P_SIGN_ID = 0, .P_ID_REGGROUP = processId, .P_TOKEN = "", .P_RESULT = cls.OUT_NUMBER}
                    Dim store = cls.ExecuteStore("PKG_AT_PROCESS.PRI_PROCESS_APP", priProcessApp)
                    Dim outNumber As Integer = Int32.Parse(priProcessApp.P_RESULT)
                    If outNumber <> 0 Then
                        Dim lstID As New List(Of Decimal)
                        lstID.Add(processId)
                        DeleteManProcess(lstID)
                    End If
                    Return outNumber
                End Using
            End If
            Return 0
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".InsertManProcess")
            Return -1
        End Try
    End Function

    Public Function ModifyManProcess(ByVal obj As PE_MANAGER_OFFER_DTO, ByVal log As UserLog) As Integer
        Try
            Dim objProcess = obj.processDTO
            Dim objWorkProcess = (From p In Context.PE_WORK_PROCESS Where p.ID = objProcess.ID).FirstOrDefault
            objWorkProcess.TYPE_ID = objProcess.TYPE_ID
            objWorkProcess.PROBLEMS = objProcess.PROBLEMS
            objWorkProcess.REQUESTS = objProcess.REQUESTS
            objWorkProcess.IS_BONHIEM = objProcess.IS_BONHIEM
            objWorkProcess.IS_CHANGE_SAL = objProcess.IS_CHANGE_SAL
            objWorkProcess.IS_CHANGE_TITLE = objProcess.IS_CHANGE_TITLE
            objWorkProcess.IS_THOINHIEM = objProcess.IS_THOINHIEM
            objWorkProcess.NOTE = objProcess.NOTE
            If objProcess.lstWorkEmp IsNot Nothing Then
                Dim lstDel = (From p In Context.PE_WORK_EMPLOYEE Where p.PROCESS_ID = objProcess.ID)
                For Each item In lstDel
                    Context.PE_WORK_EMPLOYEE.DeleteObject(item)
                Next
                For Each item In objProcess.lstWorkEmp
                    Context.PE_WORK_EMPLOYEE.AddObject(New PE_WORK_EMPLOYEE With {
                                                   .ID = Utilities.GetNextSequence(Context, Context.PE_WORK_EMPLOYEE.EntitySet.Name),
                                                   .PROCESS_ID = objProcess.ID,
                                                   .WORK_NAME = item.WORK_NAME,
                                                   .RESULT = item.RESULT})
                Next
            End If

            Dim objManConclude = obj.manConcludeDTO
            Dim objMan = (From p In Context.PE_WORK_MANAGER_CONCLUDE Where p.ID = objManConclude.ID).FirstOrDefault
            objMan.CONCLUDE_TYPE = objManConclude.CONCLUDE_TYPE
            objMan.DATE_PROBATION = objManConclude.DATE_PROBATION
            objMan.CONTRACT_TYPE = objManConclude.CONTRACT_TYPE
            objMan.APOINT_TITLE = objManConclude.APOINT_TITLE
            objMan.APOINT_OTHER = objManConclude.APOINT_OTHER
            objMan.MOVE_TITLE = objManConclude.MOVE_TITLE
            objMan.MOVE_OTHER = objManConclude.MOVE_OTHER
            objMan.IS_SAL_CHANGE = objManConclude.IS_SAL_CHANGE
            objMan.IS_JOBBAND_CHANGE = objManConclude.IS_JOBBAND_CHANGE
            objMan.SAL_SUGGET = objManConclude.SAL_SUGGET
            objMan.IS_CHANGE_TITLE = objManConclude.IS_CHANGE_TITLE
            objMan.CHANGE_TITLE = objManConclude.CHANGE_TITLE
            objMan.CHANGE_OTHER = objManConclude.CHANGE_OTHER
            objMan.STAFF_RANK = objManConclude.STAFF_RANK
            objMan.IS_CONFIRM = objManConclude.IS_CONFIRM
            objMan.NOTE = objManConclude.NOTE
            If objManConclude.lstWorkMan IsNot Nothing Then
                Dim lstDel = (From p In Context.PE_WORK_MANAGER Where p.PROCESS_ID = objProcess.ID)
                For Each item In lstDel
                    Context.PE_WORK_MANAGER.DeleteObject(item)
                Next
                For Each item In objManConclude.lstWorkMan
                    Dim id1 = Utilities.GetNextSequence(Context, Context.PE_WORK_MANAGER.EntitySet.Name)
                    Context.PE_WORK_MANAGER.AddObject(New PE_WORK_MANAGER With {
                                                   .ID = id1,
                                                   .PROCESS_ID = objManConclude.PROCESS_ID,
                                                   .WORK_ID = item.WORK_ID,
                                                   .WORK_NAME = item.WORK_NAME,
                                                   .RESULT = item.RESULT,
                                                   .REMARK = item.REMARK})
                Next
            End If

            Context.SaveChanges(log)

            If objProcess.IS_SEND_APP = 1 Then
                Using cls As New DataAccess.QueryData
                    Dim priProcessApp = New With {.P_EMPLOYEE_ID = objProcess.EMPLOYEE_ID, .P_PERIOD_ID = 0, .P_PROCESS_TYPE = "MAN_PROCESS", .P_TOTAL_HOURS = 0, .P_TOTAL_DAY = 0, .P_SIGN_ID = 0, .P_ID_REGGROUP = objProcess.ID, .P_TOKEN = "", .P_RESULT = cls.OUT_NUMBER}
                    Dim store = cls.ExecuteStore("PKG_AT_PROCESS.PRI_PROCESS_APP", priProcessApp)
                    Dim outNumber As Integer = Int32.Parse(priProcessApp.P_RESULT)
                    'If outNumber <> 0 Then
                    '    Dim lstID As New List(Of Decimal)
                    '    lstID.Add(objProcess.ID)
                    '    DeleteManProcess(lstID)
                    'End If
                    Return outNumber
                End Using
            End If
            Return 0
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".ModifyManProcess")
            Return -1
        End Try
    End Function

    Public Function DeleteManProcess(ByVal lstID As List(Of Decimal)) As Boolean
        Try
            Dim lstDel = From p In Context.PE_WORK_PROCESS Where lstID.Contains(p.ID)
            For Each item In lstDel
                Dim lstemp = From p In Context.PE_WORK_EMPLOYEE Where p.PROCESS_ID = item.ID
                For Each empItem In lstemp
                    Context.PE_WORK_EMPLOYEE.DeleteObject(empItem)
                Next
                Dim lstMng = From p In Context.PE_WORK_MANAGER Where p.PROCESS_ID = item.ID
                For Each mngItem In lstMng
                    Context.PE_WORK_MANAGER.DeleteObject(mngItem)
                Next
                Dim lstMngCon = From p In Context.PE_WORK_MANAGER_CONCLUDE Where p.PROCESS_ID = item.ID
                For Each mngConItem In lstMngCon
                    Context.PE_WORK_MANAGER_CONCLUDE.DeleteObject(mngConItem)
                Next
                Context.PE_WORK_PROCESS.DeleteObject(item)
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteManProcess")
            Throw ex
        End Try
    End Function

    Public Function GetEmployeeByManager(ByVal man_id As Decimal) As List(Of EmployeeDTO)
        Try
            Dim empCode = (From e In Context.HU_EMPLOYEE Where e.ID = man_id Select e.EMPLOYEE_CODE).FirstOrDefault
            If empCode Is Nothing Then
                empCode = ""
            End If
            Dim query = From e In Context.HU_EMPLOYEE
                        From et In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID)
                        From mt In Context.HU_TITLE.Where(Function(f) f.ID = et.LM)
                        From m In Context.HU_EMPLOYEE.Where(Function(f) f.TITLE_ID = mt.ID)
                        Where m.EMPLOYEE_CODE = empCode
                        Select New EmployeeDTO With {.ID = e.ID,
                                                     .FULLNAME_VN = String.Concat(e.EMPLOYEE_CODE, String.Concat(" - ", e.FULLNAME_VN)),
                                                     .TITLE_NAME_VN = String.Concat(et.CODE, String.Concat(" - ", et.NAME_VN))}
            Return query.ToList
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetEmployeeByManager")
            Throw ex
        End Try
    End Function

#End Region
End Class