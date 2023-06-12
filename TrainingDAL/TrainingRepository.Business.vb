Imports System.Reflection
Imports System.Text
Imports Framework.Data
Imports Framework.Data.System.Linq.Dynamic

Partial Class TrainingRepository

#Region "Otherlist"

    Public Function GetCourseList() As List(Of CourseDTO)
        Try
            Dim lst As New List(Of CourseDTO)
            lst = (From p In Context.TR_COURSE Where p.ACTFLG = -1 And p.CODE <> "OT"
                   Select New CourseDTO() With {
                           .ID = p.ID,
                           .NAME = p.NAME
                   }).ToList()
            Return lst
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetCourseList")
            Throw ex
        End Try
    End Function

    Public Function GetTitlesByOrgs(ByVal orgIds As List(Of Decimal), ByVal langCode As String) As List(Of PlanTitleDTO)
        Try
            Dim lst As New List(Of PlanTitleDTO)
            Dim iEnum = (From ot In Context.HU_ORG_TITLE
                         Join t In Context.HU_TITLE On ot.TITLE_ID _
                         Equals t.ID Where t.ACTFLG = "A" And orgIds.Contains(ot.ORG_ID)
                         Select New With {
                             .ID = t.ID,
                             .NAME_EN = t.NAME_EN,
                             .NAME_VN = t.NAME_VN
                         })
            If langCode = "vi-VN" Then
                lst = (From ele In iEnum Select New PlanTitleDTO() With {.ID = ele.ID, .NAME = ele.NAME_VN}).Distinct().ToList
            Else
                lst = (From ele In iEnum Select New PlanTitleDTO() With {.ID = ele.ID, .NAME = ele.NAME_EN}).Distinct().ToList
            End If
            Return lst
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetTitlesByOrgs")
            Throw ex
        End Try
    End Function
    Public Function GetWIByTitle(ByVal orgIds As List(Of Decimal), ByVal langCode As String) As List(Of PlanTitleDTO)
        Try

            Dim lst As New List(Of PlanTitleDTO)
            Dim iEnum = (From t In Context.HU_TITLE
                         Join tt In Context.OT_OTHER_LIST On t.WORK_INVOLVE_ID Equals tt.ID
                         Where t.ACTFLG = "A" And orgIds.Contains(t.ID) And t.WORK_INVOLVE_ID.HasValue
                         Select New With {
                             .ID = tt.ID,
                             .NAME_EN = tt.NAME_EN,
                             .NAME_VN = tt.NAME_VN
                         })
            If langCode = "vi-VN" Then
                lst = (From ele In iEnum Select New PlanTitleDTO() With {.ID = ele.ID, .NAME = ele.NAME_VN}).Distinct().ToList
            Else
                lst = (From ele In iEnum Select New PlanTitleDTO() With {.ID = ele.ID, .NAME = ele.NAME_EN}).Distinct().ToList
            End If
            Return lst
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetWIByTitle")
            Throw ex
        End Try
    End Function

    Public Function GetEntryAndFormByCourseID(ByVal CourseId As Decimal, ByVal langCode As String) As CourseDTO
        Try
            Dim Course As New CourseDTO
            Dim query = From p In Context.TR_COURSE
                        From cer In Context.TR_CERTIFICATE.Where(Function(f) f.ID = p.TR_CERTIFICATE_ID).DefaultIfEmpty
                        From cer_group In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TR_CER_GROUP_ID).DefaultIfEmpty
                        From tf In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TR_TRAIN_FIELD).DefaultIfEmpty
                        From pg In Context.TR_PROGRAM_GROUP.Where(Function(f) f.ID = p.TR_PROGRAM_GROUP).DefaultIfEmpty
                        Where p.ID = CourseId
                        Select New CourseDTO With {
                            .ID = p.ID,
                            .CODE = p.CODE,
                            .NAME = p.NAME,
                            .TR_CER_GROUP_ID = cer.TR_CER_GROUP_ID,
                            .TR_CER_GROUP_NAME = cer_group.NAME_VN,
                            .TR_CERTIFICATE_ID = p.TR_CERTIFICATE_ID,
                            .TR_CERTIFICATE_NAME = cer.NAME_VN,
                            .TR_TRAIN_FIELD_ID = p.TR_TRAIN_FIELD,
                            .TR_TRAIN_FIELD_NAME = tf.NAME_VN,
                            .TR_PROGRAM_GROUP_ID = p.TR_PROGRAM_GROUP,
                            .TR_PROGRAM_GROUP_NAME = pg.NAME,
                            .REMARK = p.REMARK,
                            .ACTFLG = p.ACTFLG,
                            .CREATED_DATE = p.CREATED_DATE}
            Course = query.ToList.Item(0)

            Dim lstTitleGroup = From p In Context.TR_COURSE
                                From t In Context.TR_TITLE_COURSE.Where(Function(f) f.TR_COURSE_ID = p.ID)
                                From o In Context.OT_OTHER_LIST.Where(Function(f) f.ID = t.TITLE_GROUP_ID)
                                Where p.ID = CourseId
                                Select New OtherListDTO With {.ID = o.ID,
                                                              .NAME_VN = o.NAME_VN}

            Dim lstTitle = From p In Context.TR_COURSE
                           From t In Context.TR_TITLE_COURSE.Where(Function(f) f.TR_COURSE_ID = p.ID)
                           From o In Context.HU_TITLE.Where(Function(f) f.ID = t.HU_TITLE_ID)
                           Where p.ID = CourseId
                           Select New TitleDTO With {.ID = o.ID,
                                                     .NAME_VN = o.NAME_VN}

            Course.LST_TITLE_GROUP = lstTitleGroup.Distinct.ToList()
            Course.LST_TITLE = lstTitle.ToList()

            Return Course
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetEntryAndFormByCourseID")
            Throw ex
        End Try
    End Function

    Public Function GetCenters() As List(Of CenterDTO)
        Try
            Dim ListCenter As List(Of CenterDTO) = (From record In Context.TR_CENTER
                                                    Where record.ACTFLG = -1
                                                    Select New CenterDTO _
                                                        With {
                                                            .ID = record.ID,
                                                            .NAME_VN = record.NAME_VN,
                                                            .NAME_EN = record.NAME_EN}).ToList
            Return ListCenter
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetCenters")
            Throw ex
        End Try
    End Function

#End Region

#Region "Plan"


    Public Function GetPlans(ByVal filter As PlanDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer, ByVal _param As ParamDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of PlanDTO)
        Try

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using


            Dim query = From p In Context.TR_PLAN
                        From u In Context.TR_UNIT.Where(Function(f) f.ID = p.UNIT_ID).DefaultIfEmpty
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From course In Context.TR_COURSE.Where(Function(f) f.ID = p.TR_COURSE_ID).DefaultIfEmpty
                        From c In Context.TR_COURSE.Where(Function(f) f.ID = p.TR_COURSE_ID)
                        From pg In Context.TR_PROGRAM_GROUP.Where(Function(f) f.ID = c.TR_PROGRAM_GROUP).DefaultIfEmpty
                        From tr_type In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TR_TYPE_ID).DefaultIfEmpty
                        From pn In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.PROPERTIES_NEED_ID).DefaultIfEmpty
                        From tf In Context.OT_OTHER_LIST.Where(Function(f) f.ID = c.TR_TRAIN_FIELD).DefaultIfEmpty
                        From form In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TRAIN_FORM_ID).DefaultIfEmpty
                        From duration In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TR_DURATION_UNIT_ID).DefaultIfEmpty
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) p.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)
                        Select New PlanDTO With {.ID = p.ID,
                                      .YEAR = p.YEAR,
                                      .NAME = p.NAME,
                                      .ORG_NAME = org.NAME_VN,
                                      .TR_COURSE_NAME = course.NAME,
                                      .TR_TRAIN_FORM_ID = p.TRAIN_FORM_ID,
                                      .TR_TRAIN_FORM_NAME = form.NAME_VN,
                                      .TEACHER_NUMBER = p.TEACHER_NUMBER,
                                      .STUDENT_NUMBER = p.STUDENT_NUMBER,
                                      .COST_TRAIN = p.COST_TRAIN,
                                      .COST_INCURRED = p.COST_INCURRED,
                                      .COST_TRAVEL = p.COST_TRAVEL,
                                      .COST_TOTAL = p.COST_TOTAL,
                                      .COST_TOTAL_USD = p.COST_TOTAL_USD,
                                      .COST_OTHER = p.COST_OTHER,
                                      .COST_OF_STUDENT = p.COST_OF_STUDENT,
                                      .COST_OF_STUDENT_USD = p.COST_OF_STUDENT_USD,
                                      .TARGET_TRAIN = p.TARGET_TRAIN,
                                      .VENUE = p.VENUE,
                                      .REMARK = p.REMARK,
                                      .Departments_NAME = p.DEPARTMENTS,
                                      .Titles_NAME = p.TITLES,
                                      .Centers_NAME = p.CENTERS,
                                      .Months_NAME = p.MONTHS,
                                      .DURATION = p.DURATION,
                                      .TR_DURATION_UNIT_NAME = duration.NAME_VN,
                                      .TR_TRAIN_FIELD_NAME = tf.NAME_VN,
                                      .TR_PROGRAM_GROUP_NAME = pg.NAME,
                                      .PROPERTIES_NEED_ID = p.PROPERTIES_NEED_ID,
                                      .PROPERTIES_NEED_NAME = pn.NAME_VN,
                                      .UNIT_ID = p.UNIT_ID,
                                      .UNIT_NAME = u.NAME,
                                      .Work_inv_NAME = p.WORKS,
                                      .ATTACHFILE = p.ATTACHFILE,
                                      .CREATED_DATE = p.CREATED_DATE,
                                      .EXPECT_TR_FROM = p.EXPECT_TR_FROM,
                                      .EXPECT_TR_TO = p.EXPECT_TR_TO,
                                      .TR_PLAN_CODE = p.TR_PLAN_CODE,
                                      .CONTENT = p.CONTENT,
                                      .PLAN_TYPE = p.PLAN_TYPE,
                                      .PLAN_TYPE_TEXT = If(p.PLAN_TYPE = -1, "Đột xuất", "Theo nhu cầu đào tạo"),
                                      .CERTIFICATE = p.CERTIFICATE,
                                      .CERTIFICATE_TEXT = If(p.CERTIFICATE = -1, "Có", "Không"),
                                      .CERTIFICATE_NAME = p.CERTIFICATE_NAME,
                                      .TR_AFTER_TRAIN = p.TR_AFTER_TRAIN,
                                      .TR_AFTER_TRAIN_TEXT = If(p.TR_AFTER_TRAIN = -1, "Có", "Không"),
                                      .TR_COMMIT = p.TR_COMMIT,
                                      .TR_COMMIT_TEXT = If(p.TR_COMMIT = -1, "Có", "Không"),
                                      .EXPECT_CLASS = p.EXPECT_CLASS,
                                      .TR_TYPE_ID = p.TR_TYPE_ID,
                                      .TR_TYPE_NAME = tr_type.NAME_VN}



            Dim lst = query
            If filter.TR_PLAN_CODE <> "" Then
                lst = lst.Where(Function(p) p.TR_PLAN_CODE.ToUpper.Contains(filter.TR_PLAN_CODE.ToUpper))
            End If
            If filter.PLAN_TYPE_TEXT <> "" Then
                lst = lst.Where(Function(p) p.PLAN_TYPE_TEXT.ToUpper.Contains(filter.PLAN_TYPE_TEXT.ToUpper))
            End If
            If filter.TR_TYPE_NAME <> "" Then
                lst = lst.Where(Function(p) p.TR_TYPE_NAME.ToUpper.Contains(filter.TR_TYPE_NAME.ToUpper))
            End If
            If filter.CONTENT <> "" Then
                lst = lst.Where(Function(p) p.CONTENT.ToUpper.Contains(filter.CONTENT.ToUpper))
            End If




            If filter.NAME <> "" Then
                lst = lst.Where(Function(p) p.NAME.ToUpper.Contains(filter.NAME.ToUpper))
            End If
            If filter.ORG_NAME <> "" Then
                lst = lst.Where(Function(p) p.ORG_NAME.ToUpper.Contains(filter.ORG_NAME.ToUpper))
            End If
            If filter.TR_COURSE_NAME <> "" Then
                lst = lst.Where(Function(p) p.TR_COURSE_NAME.ToUpper.Contains(filter.TR_COURSE_NAME.ToUpper))
            End If
            If filter.TR_TRAIN_FORM_NAME <> "" Then
                lst = lst.Where(Function(p) p.TR_TRAIN_FORM_NAME.ToUpper.Contains(filter.TR_TRAIN_FORM_NAME.ToUpper))
            End If
            If filter.TR_TRAIN_ENTRIES_NAME <> "" Then
                lst = lst.Where(Function(p) p.TR_TRAIN_ENTRIES_NAME.ToUpper.Contains(filter.TR_TRAIN_ENTRIES_NAME.ToUpper))
            End If
            If filter.TR_DURATION_UNIT_NAME <> "" Then
                lst = lst.Where(Function(p) p.TR_DURATION_UNIT_NAME.ToUpper.Contains(filter.TR_DURATION_UNIT_NAME.ToUpper))
            End If

            If filter.YEAR.HasValue Then
                lst = lst.Where(Function(p) p.YEAR = filter.YEAR)
            End If
            If filter.DURATION.HasValue Then
                lst = lst.Where(Function(p) p.DURATION = filter.DURATION)
            End If
            If filter.TEACHER_NUMBER.HasValue Then
                lst = lst.Where(Function(p) p.TEACHER_NUMBER = filter.TEACHER_NUMBER)
            End If
            If filter.STUDENT_NUMBER.HasValue Then
                lst = lst.Where(Function(p) p.STUDENT_NUMBER = filter.STUDENT_NUMBER)
            End If
            If filter.TEACHER_NUMBER.HasValue Then
                lst = lst.Where(Function(p) p.TEACHER_NUMBER = filter.TEACHER_NUMBER)
            End If
            If filter.COST_TRAIN.HasValue Then
                lst = lst.Where(Function(p) p.COST_TRAIN = filter.COST_TRAIN)
            End If
            If filter.COST_INCURRED.HasValue Then
                lst = lst.Where(Function(p) p.COST_INCURRED = filter.COST_INCURRED)
            End If
            If filter.COST_TRAVEL.HasValue Then
                lst = lst.Where(Function(p) p.TEACHER_NUMBER = filter.COST_TRAVEL)
            End If
            If filter.COST_TOTAL.HasValue Then
                lst = lst.Where(Function(p) p.COST_TOTAL = filter.COST_TOTAL)
            End If
            If filter.COST_OTHER.HasValue Then
                lst = lst.Where(Function(p) p.COST_OTHER = filter.COST_OTHER)
            End If
            If filter.COST_OF_STUDENT.HasValue Then
                lst = lst.Where(Function(p) p.COST_OF_STUDENT = filter.COST_OF_STUDENT)
            End If
            If filter.CURRENCY <> "" Then
                lst = lst.Where(Function(p) p.CURRENCY.ToUpper.Contains(filter.CURRENCY.ToUpper))
            End If
            If filter.TARGET_TRAIN <> "" Then
                lst = lst.Where(Function(p) p.TARGET_TRAIN.ToUpper.Contains(filter.TARGET_TRAIN.ToUpper))
            End If
            If filter.VENUE <> "" Then
                lst = lst.Where(Function(p) p.VENUE.ToUpper.Contains(filter.VENUE.ToUpper))
            End If
            If filter.REMARK <> "" Then
                lst = lst.Where(Function(p) p.REMARK.ToUpper.Contains(filter.REMARK.ToUpper))
            End If
            If filter.Departments_NAME <> "" Then
                lst = lst.Where(Function(p) p.Departments_NAME.ToUpper.Contains(filter.Departments_NAME.ToUpper))
            End If
            If filter.Titles_NAME <> "" Then
                lst = lst.Where(Function(p) p.Titles_NAME.ToUpper.Contains(filter.Titles_NAME.ToUpper))
            End If
            If filter.Months_NAME <> "" Then
                lst = lst.Where(Function(p) p.Months_NAME.ToUpper.Contains(filter.Months_NAME.ToUpper))
            End If
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList()
            Return Nothing
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetPlans")
            Throw ex
        End Try
    End Function

    Public Function GetPlanById(ByVal Id As Decimal) As PlanDTO
        Try
            Dim objPlan As PlanDTO = (From plan In Context.TR_PLAN
                                      From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = plan.ORG_ID).DefaultIfEmpty
                                      From c In Context.TR_COURSE.Where(Function(f) f.ID = plan.TR_COURSE_ID).DefaultIfEmpty
                                      From t In Context.OT_OTHER_LIST.Where(Function(f) f.ID = c.TR_TRAIN_FIELD).DefaultIfEmpty
                                      Where plan.ID = Id
                                      Select New PlanDTO With {.ID = plan.ID,
                                                               .YEAR = plan.YEAR,
                                                               .COST_INCURRED = plan.COST_INCURRED,
                                                               .COST_OF_STUDENT = plan.COST_OF_STUDENT,
                                                               .COST_OF_STUDENT_USD = plan.COST_OF_STUDENT_USD,
                                                               .COST_OTHER = plan.COST_OTHER,
                                                               .COST_TOTAL = plan.COST_TOTAL,
                                                               .COST_TOTAL_USD = plan.COST_TOTAL_USD,
                                                               .COST_TRAIN = plan.COST_TRAIN,
                                                               .COST_TRAVEL = plan.COST_TRAVEL,
                                                               .DURATION = plan.DURATION,
                                                               .NAME = plan.NAME,
                                                               .ORG_ID = plan.ORG_ID,
                                                               .ORG_NAME = org.NAME_VN,
                                                               .PLAN_T1 = plan.PLAN_T1,
                                                               .PLAN_T10 = plan.PLAN_T10,
                                                               .PLAN_T11 = plan.PLAN_T11,
                                                               .PLAN_T12 = plan.PLAN_T12,
                                                               .PLAN_T2 = plan.PLAN_T2,
                                                               .PLAN_T3 = plan.PLAN_T3,
                                                               .PLAN_T4 = plan.PLAN_T4,
                                                               .PLAN_T5 = plan.PLAN_T5,
                                                               .PLAN_T6 = plan.PLAN_T6,
                                                               .PLAN_T7 = plan.PLAN_T7,
                                                               .PLAN_T8 = plan.PLAN_T8,
                                                               .PLAN_T9 = plan.PLAN_T9,
                                                               .REMARK = plan.REMARK,
                                                               .STUDENT_NUMBER = plan.STUDENT_NUMBER,
                                                               .TARGET_TRAIN = plan.TARGET_TRAIN,
                                                               .TEACHER_NUMBER = plan.TEACHER_NUMBER,
                                                               .TR_COURSE_ID = plan.TR_COURSE_ID,
                                                               .TR_CURRENCY_ID = plan.TR_CURRENCY_ID,
                                                               .TR_DURATION_UNIT_ID = plan.TR_DURATION_UNIT_ID,
                                                               .TR_TRAIN_FORM_ID = plan.TRAIN_FORM_ID,
                                                               .PROPERTIES_NEED_ID = plan.PROPERTIES_NEED_ID,
                                                               .UNIT_ID = plan.UNIT_ID,
                                                               .ATTACHFILE = plan.ATTACHFILE,
                                                               .VENUE = plan.VENUE,
                                                               .TR_PLAN_CODE = plan.TR_PLAN_CODE,
                                                               .PLAN_TYPE = plan.PLAN_TYPE,
                                                               .CONTENT = plan.CONTENT,
                                                               .EXPECT_TR_FROM = plan.EXPECT_TR_FROM,
                                                               .EXPECT_TR_TO = plan.EXPECT_TR_TO,
                                                               .EXPECT_CLASS = plan.EXPECT_CLASS,
                                                               .CERTIFICATE = plan.CERTIFICATE,
                                                               .TR_AFTER_TRAIN = plan.TR_AFTER_TRAIN,
                                                               .CERTIFICATE_NAME = plan.CERTIFICATE_NAME,
                                                               .TR_COMMIT = plan.TR_COMMIT,
                                                               .TR_TYPE_ID = plan.TR_TYPE_ID,
                                                               .DAY_REVIEW_1 = plan.DAY_REVIEW_1,
                                                               .DAY_REVIEW_2 = plan.DAY_REVIEW_2,
                                                               .DAY_REVIEW_3 = plan.DAY_REVIEW_3,
                                                               .TR_PROGRAM_CODE = plan.TR_PROGRAM_CODE,
                                                               .TR_TRAIN_FIELD_NAME = t.NAME_VN,
                                                               .TR_TRAIN_FIELD_ID = c.TR_TRAIN_FIELD,
                                                               .TR_REQUEST_ID = plan.TR_REQUEST_ID}).FirstOrDefault

            objPlan.Units = (From p In Context.TR_PLAN_UNIT
                             From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID)
                             Where p.TR_PLAN_ID = Id
                             Select New PlanOrgDTO With {.ID = p.ORG_ID,
                                                     .NAME = o.NAME_VN}).ToList

            objPlan.Titles = (From p In Context.TR_PLAN_TITLE
                              From t In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID)
                              Where p.TR_PLAN_ID = Id
                              Select New PlanTitleDTO With {.ID = p.TITLE_ID,
                                                        .NAME = t.NAME_VN}).ToList

            objPlan.GroupTitle = (From p In Context.TR_PLAN_TITLE
                                  From t In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
                                  From o In Context.OT_OTHER_LIST.Where(Function(f) f.ID = t.TITLE_GROUP_ID).DefaultIfEmpty
                                  Where p.TR_PLAN_ID = Id
                                  Select New PlanTitleDTO With {.ID = o.ID,
                                                                .NAME = o.NAME_VN}).ToList

            objPlan.Centers = (From p In Context.TR_PLAN_CENTER
                               From c In Context.TR_CENTER.Where(Function(f) f.ID = p.CENTER_ID)
                               Where p.TR_PLAN_ID = Id
                               Select New PlanCenterDTO With {.ID = p.CENTER_ID,
                                                          .NAME_EN = c.NAME_EN,
                                                          .NAME_VN = c.NAME_VN}).ToList

            objPlan.ProgramLecture = (From p In Context.TR_PLAN_CENTER
                                      From c In Context.TR_LECTURE.Where(Function(f) f.ID = p.CENTER_ID)
                                      Where p.TR_PLAN_ID = Id
                                      Select New ProgramLectureDTO With {.ID = p.CENTER_ID,
                                                                         .NAME = c.NAME}).ToList

            Return objPlan
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetPlanById")
            Throw ex
        End Try
    End Function
    Public Function test(ByVal a As CostDetailDTO) As CostDetailDTO
        Dim b As New CostDetailDTO
        Return b
    End Function

    Public Function InsertPlan(ByVal plan As PlanDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objPlan As New TR_PLAN

        Try
            With objPlan
                .ID = Utilities.GetNextSequence(Context, Context.TR_PLAN.EntitySet.Name)
                .COST_INCURRED = plan.COST_INCURRED
                .COST_OF_STUDENT = plan.COST_OF_STUDENT
                .COST_OTHER = plan.COST_OTHER
                .COST_TOTAL = plan.COST_TOTAL
                .COST_TRAIN = plan.COST_TRAIN
                .COST_TRAVEL = plan.COST_TRAVEL
                .DURATION = plan.DURATION
                .NAME = plan.NAME
                .ORG_ID = plan.ORG_ID
                .PLAN_T1 = plan.PLAN_T1
                .PLAN_T10 = plan.PLAN_T10
                .PLAN_T11 = plan.PLAN_T11
                .PLAN_T12 = plan.PLAN_T12
                .PLAN_T2 = plan.PLAN_T2
                .PLAN_T3 = plan.PLAN_T3
                .PLAN_T4 = plan.PLAN_T4
                .PLAN_T5 = plan.PLAN_T5
                .PLAN_T6 = plan.PLAN_T6
                .PLAN_T7 = plan.PLAN_T7
                .PLAN_T8 = plan.PLAN_T8
                .PLAN_T9 = plan.PLAN_T9
                .REMARK = plan.REMARK
                .STUDENT_NUMBER = plan.STUDENT_NUMBER
                .TARGET_TRAIN = plan.TARGET_TRAIN
                .TEACHER_NUMBER = plan.TEACHER_NUMBER
                .TR_COURSE_ID = plan.TR_COURSE_ID
                .TR_CURRENCY_ID = plan.TR_CURRENCY_ID
                .TR_DURATION_UNIT_ID = plan.TR_DURATION_UNIT_ID
                .VENUE = plan.VENUE
                .MONTHS = plan.Months_NAME
                .DEPARTMENTS = plan.Departments_NAME
                .CENTERS = plan.Centers_NAME
                .TITLES = plan.Titles_NAME
                .YEAR = plan.YEAR
                .PROPERTIES_NEED_ID = plan.PROPERTIES_NEED_ID
                .UNIT_ID = plan.UNIT_ID
                .ATTACHFILE = plan.ATTACHFILE
                .COST_TOTAL_USD = plan.COST_TOTAL_USD
                .COST_OF_STUDENT_USD = plan.COST_OF_STUDENT_USD
                .TRAIN_FORM_ID = plan.TR_TRAIN_FORM_ID
                .WORKS = plan.Work_inv_NAME
                .TR_PLAN_CODE = plan.TR_PLAN_CODE
                .PLAN_TYPE = plan.PLAN_TYPE
                .CONTENT = plan.CONTENT
                .EXPECT_TR_FROM = plan.EXPECT_TR_FROM
                .EXPECT_TR_TO = plan.EXPECT_TR_TO
                .EXPECT_CLASS = plan.EXPECT_CLASS
                .TR_COMMIT = plan.TR_COMMIT
                .CERTIFICATE = plan.CERTIFICATE
                .TR_AFTER_TRAIN = plan.TR_AFTER_TRAIN
                .TR_TYPE_ID = plan.TR_TYPE_ID
                .DAY_REVIEW_1 = plan.DAY_REVIEW_1
                .DAY_REVIEW_2 = plan.DAY_REVIEW_2
                .DAY_REVIEW_3 = plan.DAY_REVIEW_3
                .TR_REQUEST_ID = plan.TR_REQUEST_ID
                .CERTIFICATE_NAME = plan.CERTIFICATE_NAME
                .TR_PROGRAM_CODE = plan.TR_PROGRAM_CODE
            End With
            gID = objPlan.ID
            Context.TR_PLAN.AddObject(objPlan)
            If plan.CostDetail IsNot Nothing Then
                For Each unit In plan.CostDetail
                    Context.TR_PLAN_COST_DETAIL.AddObject(New TR_PLAN_COST_DETAIL With {
                                                   .ID = Utilities.GetNextSequence(Context, Context.TR_PLAN_COST_DETAIL.EntitySet.Name),
                                                   .PLAN_ID = objPlan.ID,
                                                   .TYPE_ID = unit.TYPE_ID,
                                                   .MONEY = unit.MONEY,
                                                   .MONEY_TYPE = unit.MONEY_TYPE,
                                                   .MODIFIED_DATE = Date.Now})
                Next
            End If

            If plan.Units IsNot Nothing Then
                For Each unit In plan.Units
                    Context.TR_PLAN_UNIT.AddObject(New TR_PLAN_UNIT With {
                                                   .ID = Utilities.GetNextSequence(Context, Context.TR_PLAN_UNIT.EntitySet.Name),
                                                   .TR_PLAN_ID = objPlan.ID,
                                                   .ORG_ID = unit.ID})
                Next
            End If
            If plan.Titles IsNot Nothing Then
                For Each title In plan.Titles
                    Context.TR_PLAN_TITLE.AddObject(New TR_PLAN_TITLE With {
                                                    .ID = Utilities.GetNextSequence(Context, Context.TR_PLAN_TITLE.EntitySet.Name),
                                                    .TR_PLAN_ID = objPlan.ID,
                                                    .TITLE_ID = title.ID})
                Next
            End If

            If plan.Centers IsNot Nothing Then
                For Each center In plan.Centers
                    Context.TR_PLAN_CENTER.AddObject(New TR_PLAN_CENTER With {
                                                     .ID = Utilities.GetNextSequence(Context, Context.TR_PLAN_CENTER.EntitySet.Name),
                                                     .TR_PLAN_ID = objPlan.ID,
                                                     .CENTER_ID = center.ID})
                Next
            End If
            If plan.Plan_Emp IsNot Nothing Then
                For Each lstemp In plan.Plan_Emp
                    Context.TR_PLAN_EMPLOYEE.AddObject(New TR_PLAN_EMPLOYEE With {
                                                   .ID = Utilities.GetNextSequence(Context, Context.TR_PLAN_EMPLOYEE.EntitySet.Name),
                                                   .TR_PLAN_ID = objPlan.ID,
                                                   .EMPLOYEE_ID = lstemp.EMPLOYEE_ID})
                Next
            End If
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".InsertPlan")
            Throw ex
        End Try
    End Function

    Public Function ModifyPlan(ByVal plan As PlanDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objPlan As New TR_PLAN With {.ID = plan.ID}
        Try
            Context.TR_PLAN.Attach(objPlan)
            With objPlan
                .COST_INCURRED = plan.COST_INCURRED
                .COST_OF_STUDENT = plan.COST_OF_STUDENT
                .COST_OTHER = plan.COST_OTHER
                .COST_TOTAL = plan.COST_TOTAL
                .COST_TRAIN = plan.COST_TRAIN
                .COST_TRAVEL = plan.COST_TRAVEL
                .DURATION = plan.DURATION
                .NAME = plan.NAME
                .ORG_ID = plan.ORG_ID
                .PLAN_T1 = plan.PLAN_T1
                .PLAN_T10 = plan.PLAN_T10
                .PLAN_T11 = plan.PLAN_T11
                .PLAN_T12 = plan.PLAN_T12
                .PLAN_T2 = plan.PLAN_T2
                .PLAN_T3 = plan.PLAN_T3
                .PLAN_T4 = plan.PLAN_T4
                .PLAN_T5 = plan.PLAN_T5
                .PLAN_T6 = plan.PLAN_T6
                .PLAN_T7 = plan.PLAN_T7
                .PLAN_T8 = plan.PLAN_T8
                .PLAN_T9 = plan.PLAN_T9
                .REMARK = plan.REMARK
                .STUDENT_NUMBER = plan.STUDENT_NUMBER
                .TARGET_TRAIN = plan.TARGET_TRAIN
                .TEACHER_NUMBER = plan.TEACHER_NUMBER
                .TR_COURSE_ID = plan.TR_COURSE_ID
                .TR_CURRENCY_ID = plan.TR_CURRENCY_ID
                .TR_DURATION_UNIT_ID = plan.TR_DURATION_UNIT_ID
                .VENUE = plan.VENUE
                .MONTHS = plan.Months_NAME
                .DEPARTMENTS = plan.Departments_NAME
                .CENTERS = plan.Centers_NAME
                .TITLES = plan.Titles_NAME
                .YEAR = plan.YEAR
                .PROPERTIES_NEED_ID = plan.PROPERTIES_NEED_ID
                .UNIT_ID = plan.UNIT_ID
                .ATTACHFILE = plan.ATTACHFILE
                .COST_TOTAL_USD = plan.COST_TOTAL_USD
                .COST_OF_STUDENT_USD = plan.COST_OF_STUDENT_USD
                .TRAIN_FORM_ID = plan.TR_TRAIN_FORM_ID
                .WORKS = plan.Work_inv_NAME
                .TR_PLAN_CODE = plan.TR_PLAN_CODE
                .PLAN_TYPE = plan.PLAN_TYPE
                .CONTENT = plan.CONTENT
                .EXPECT_TR_FROM = plan.EXPECT_TR_FROM
                .EXPECT_TR_TO = plan.EXPECT_TR_TO
                .EXPECT_CLASS = plan.EXPECT_CLASS
                .TR_COMMIT = plan.TR_COMMIT
                .CERTIFICATE = plan.CERTIFICATE
                .TR_AFTER_TRAIN = plan.TR_AFTER_TRAIN
                .TR_TYPE_ID = plan.TR_TYPE_ID
                .DAY_REVIEW_1 = plan.DAY_REVIEW_1
                .DAY_REVIEW_2 = plan.DAY_REVIEW_2
                .DAY_REVIEW_3 = plan.DAY_REVIEW_3
                .TR_REQUEST_ID = plan.TR_REQUEST_ID
                .CERTIFICATE_NAME = plan.CERTIFICATE_NAME
                .TR_PROGRAM_CODE = plan.TR_PROGRAM_CODE
            End With
            Dim oldCostDetail = From i In Context.TR_PLAN_COST_DETAIL Where i.PLAN_ID = plan.ID
            For Each unit In oldCostDetail
                Context.TR_PLAN_COST_DETAIL.DeleteObject(unit)
            Next

            Dim oldUnits = From i In Context.TR_PLAN_UNIT Where i.TR_PLAN_ID = plan.ID
            For Each unit In oldUnits
                Context.TR_PLAN_UNIT.DeleteObject(unit)
            Next

            Dim oldTItles = From i In Context.TR_PLAN_TITLE Where i.TR_PLAN_ID = plan.ID
            For Each item In oldTItles
                Context.TR_PLAN_TITLE.DeleteObject(item)
            Next

            Dim oldCenters = From i In Context.TR_PLAN_CENTER Where i.TR_PLAN_ID = plan.ID
            For Each item In oldCenters
                Context.TR_PLAN_CENTER.DeleteObject(item)
            Next
            Dim oldlstEmp = From i In Context.TR_PLAN_EMPLOYEE Where i.TR_PLAN_ID = plan.ID
            For Each item In oldlstEmp
                Context.TR_PLAN_EMPLOYEE.DeleteObject(item)
            Next

            If plan.CostDetail IsNot Nothing > 0 Then
                For Each unit In plan.CostDetail
                    Context.TR_PLAN_COST_DETAIL.AddObject(New TR_PLAN_COST_DETAIL With {
                                                   .ID = Utilities.GetNextSequence(Context, Context.TR_PLAN_COST_DETAIL.EntitySet.Name),
                                                   .PLAN_ID = objPlan.ID,
                                                   .TYPE_ID = unit.TYPE_ID,
                                                   .MONEY = unit.MONEY,
                                                   .MONEY_TYPE = unit.MONEY_TYPE,
                                                   .MODIFIED_DATE = Date.Now})
                Next
            End If

            If plan.Units IsNot Nothing Then
                For Each unit In plan.Units
                    Context.TR_PLAN_UNIT.AddObject(New TR_PLAN_UNIT With {.ID = Utilities.GetNextSequence(Context, Context.TR_PLAN_UNIT.EntitySet.Name), .TR_PLAN_ID = objPlan.ID, .ORG_ID = unit.ID})
                Next
            End If
            If plan.Titles IsNot Nothing Then
                For Each title In plan.Titles
                    Context.TR_PLAN_TITLE.AddObject(New TR_PLAN_TITLE With {.ID = Utilities.GetNextSequence(Context, Context.TR_PLAN_TITLE.EntitySet.Name), .TR_PLAN_ID = objPlan.ID, .TITLE_ID = title.ID})
                Next
            End If

            If plan.Centers IsNot Nothing Then
                For Each center In plan.Centers
                    Context.TR_PLAN_CENTER.AddObject(New TR_PLAN_CENTER With {.ID = Utilities.GetNextSequence(Context, Context.TR_PLAN_CENTER.EntitySet.Name), .TR_PLAN_ID = objPlan.ID, .CENTER_ID = center.ID})
                Next
            End If
            If plan.Plan_Emp IsNot Nothing Then
                For Each lstemp In plan.Plan_Emp
                    Context.TR_PLAN_EMPLOYEE.AddObject(New TR_PLAN_EMPLOYEE With {.ID = Utilities.GetNextSequence(Context, Context.TR_PLAN_EMPLOYEE.EntitySet.Name), .TR_PLAN_ID = objPlan.ID, .EMPLOYEE_ID = lstemp.EMPLOYEE_ID})
                Next
            End If

            Context.SaveChanges(log)
            gID = objPlan.ID
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".ModifyPlan")
            Throw ex
        End Try

    End Function

    Public Function DeletePlans(ByVal lstId As List(Of Decimal)) As Boolean
        Try
            Dim deletedPlans = (From record In Context.TR_PLAN Where lstId.Contains(record.ID))
            Dim deletedPlanTitle = (From record In Context.TR_PLAN_TITLE Where lstId.Contains(record.TR_PLAN_ID))
            For Each item In deletedPlanTitle
                Context.TR_PLAN_TITLE.DeleteObject(item)
            Next

            Dim deletedPlanDeparment = (From record In Context.TR_PLAN_UNIT Where lstId.Contains(record.TR_PLAN_ID))
            For Each item In deletedPlanDeparment
                Context.TR_PLAN_UNIT.DeleteObject(item)
            Next

            Dim deletedPlanCenter = (From record In Context.TR_PLAN_CENTER Where lstId.Contains(record.TR_PLAN_ID))
            For Each item In deletedPlanCenter
                Context.TR_PLAN_CENTER.DeleteObject(item)
            Next

            Dim deletedPlanCost = (From record In Context.TR_PLAN_COST_DETAIL Where lstId.Contains(record.PLAN_ID))
            For Each item In deletedPlanCost
                Context.TR_PLAN_COST_DETAIL.DeleteObject(item)
            Next

            For Each item In deletedPlans
                Context.TR_PLAN.DeleteObject(item)
            Next
            Dim deletedPlanEmp = (From record In Context.TR_PLAN_EMPLOYEE Where lstId.Contains(record.TR_PLAN_ID))
            For Each item In deletedPlanEmp
                Context.TR_PLAN_EMPLOYEE.DeleteObject(item)
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".DeletePlans")
            Throw ex
        End Try
    End Function

    Public Function GET_PLAN_DATA_IMPORT(ByVal P_ORG_ID As Decimal, ByVal log As UserLog) As DataSet
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataSet = cls.ExecuteStore("PKG_TRAINING_BUSINESS.GET_PLAN_DATA_IMPORT",
                                                           New With {.P_ORG_ID = P_ORG_ID,
                                                                     .P_USER = log.Username.ToUpper,
                                                                     .P_OUT = cls.OUT_CURSOR,
                                                                     .P_OUT1 = cls.OUT_CURSOR,
                                                                     .P_OUT2 = cls.OUT_CURSOR,
                                                                     .P_OUT3 = cls.OUT_CURSOR,
                                                                     .P_OUT4 = cls.OUT_CURSOR,
                                                                     .P_OUT5 = cls.OUT_CURSOR,
                                                                     .P_OUT6 = cls.OUT_CURSOR,
                                                                     .P_OUT7 = cls.OUT_CURSOR}, False)
                Return dtData
            End Using
        Catch ex As Exception
            Return Nothing
        End Try

    End Function
    Public Function GET_TITLE_COURSE_IMPORT() As DataSet
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataSet = cls.ExecuteStore("PKG_TRAINING_BUSINESS.GET_TITLE_COURSE_IMPORT",
                                                           New With {.P_OUT = cls.OUT_CURSOR,
                                                                     .P_OUT1 = cls.OUT_CURSOR,
                                                                     .P_OUT2 = cls.OUT_CURSOR}, False)
                Return dtData
            End Using
        Catch ex As Exception
            Return Nothing
        End Try

    End Function
    Public Function IMPORT_TR_PLAN(ByVal DATA_IN As String, ByVal log As UserLog) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_TRAINING_BUSINESS.IMPORT_TR_PLAN",
                                               New With {.P_DOCXML = DATA_IN,
                                                         .P_USER = log.Username.ToUpper,
                                                         .P_CUR = cls.OUT_CURSOR}, True)
                Return CBool(dtData(0)(0))
            End Using
        Catch ex As Exception
            Return Nothing
        End Try

    End Function
    Public Function IMPORT_TITLECOURSE(ByVal DATA_IN As String, ByVal log As UserLog) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_TRAINING_BUSINESS.IMPORT_TITLECOURSE",
                                               New With {.P_DOCXML = DATA_IN,
                                                         .P_USER = log.Username.ToUpper,
                                                         .P_CUR = cls.OUT_CURSOR}, True)
                Return CBool(dtData(0)(0))
            End Using
        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function GetTitleByGroupID(ByVal _lstGroupID As List(Of Decimal)) As List(Of TitleDTO)
        Try
            Dim lst = (From p In Context.HU_TITLE
                       Where _lstGroupID.Contains(p.TITLE_GROUP_ID)
                       Select New TitleDTO With {.ID = p.ID,
                                                 .NAME_VN = p.NAME_VN}).ToList
            Return lst
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".DeletePlans")
            Throw ex
        End Try
    End Function
#End Region

#Region "Request"

    Public Function ToDate(ByVal item As Object)
        If IsDBNull(item) Then
            Return Nothing
        Else
            Return DateTime.ParseExact(item, "dd/MM/yyyy", Globalization.CultureInfo.InvariantCulture)
        End If
    End Function
    Public Function ToDecimal(ByVal item As Object)
        If IsDBNull(item) Then
            Return Nothing
        Else
            Return CDec(item)
        End If
    End Function

    Public Function IMPORT_PROGRAM_RESULT(ByVal P_DOCXML As String, ByVal log As UserLog) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_TRAINING.IMPORT_PROGRAM_RESULT",
                                 New With {.P_DOCXML = P_DOCXML,
                                           .P_USERNAME = log.Username})
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTraining")
            Throw ex
            Return False
        End Try
    End Function

    Public Function IMPORT_REIMBURSEMENT(ByVal P_DOCXML As String, ByVal log As UserLog) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_TRAINING.IMPORT_REIMBURSEMENT",
                                 New With {.P_DOCXML = P_DOCXML,
                                           .P_USERNAME = log.Username})
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTraining")
            Throw ex
            Return False
        End Try
    End Function

    Public Function IMPORT_TR_REQUEST(ByVal P_DOCXML As String, ByVal log As UserLog) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_TRAINING.IMPORT_TR_REQUEST",
                                 New With {.P_DOCXML = P_DOCXML,
                                           .P_USERNAME = log.Username})
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTraining")
            Throw ex
            Return False
        End Try
    End Function

    Public Function GetTrainingRequests(ByVal filter As RequestDTO,
                                         ByVal PageIndex As Integer, ByVal PageSize As Integer,
                                         ByRef Total As Integer, ByVal _param As ParamDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of RequestDTO)
        Try
            Dim lst As List(Of RequestDTO) = New List(Of RequestDTO)
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_TRAINING.GET_TR_REQUEST",
                                           New With {.P_YEAR = filter.YEAR,
                                                     .P_STATUS = filter.STATUS_ID,
                                                     .P_ORGID = filter.ORG_ID,
                                                     .P_USERNAME = log.Username.ToUpper,
                                                     .P_CUR = cls.OUT_CURSOR})
                If dtData IsNot Nothing Then
                    lst = (From row As DataRow In dtData.Rows
                           Select New RequestDTO With {.ID = row("ID").ToString(),
                                                   .STATUS_ID = row("STATUS_ID").ToString(),
                                                   .STATUS_NAME = row("STATUS_NAME").ToString(),
                                                   .SENDER_CODE = row("SENDER_CODE").ToString(),
                                                   .SENDER_NAME = row("SENDER_NAME").ToString(),
                                                   .SENDER_EMAIL = row("SENDER_EMAIL").ToString(),
                                                   .SENDER_MOBILE = row("SENDER_MOBILE").ToString(),
                                                   .REQUEST_DATE = ToDate(row("REQUEST_DATE")),
                                                   .COURSE_NAME = row("COURSE_NAME").ToString(),
                                                   .TRAIN_FORM = row("TRAIN_FORM").ToString(),
                                                   .TARGET_TRAIN = row("TARGET_TRAIN").ToString(),
                                                   .CONTENT = row("CONTENT").ToString(),
                                                   .COM_ID = ToDecimal(row("COM_ID")),
                                                   .COM_NAME = row("COM_NAME").ToString(),
                                                   .COM_DESC = row("COM_DESC").ToString(),
                                                   .ORG_ID = ToDecimal(row("ORG_ID")),
                                                   .ORG_NAME = row("ORG_NAME").ToString(),
                                                   .ORG_DESC = row("ORG_DESC").ToString(),
                                                   .EXPECTED_DATE = ToDate(row("EXPECTED_DATE")),
                                                   .START_DATE = ToDate(row("START_DATE")),
                                                   .CENTERS = row("CENTERS").ToString(),
                                                   .UNIT_NAME = row("UNIT_NAME").ToString(),
                                                   .TEACHERS = row("TEACHERS").ToString(),
                                                   .VENUE = row("VENUE").ToString(),
                                                   .EXPECTED_COST = ToDecimal(row("EXPECTED_COST")),
                                                   .REMARK = row("REMARK").ToString(),
                                                   .CREATED_DATE = row("CREATED_DATE").ToString(),
                                                   .REJECT_REASON = row("REJECT_REASON").ToString(),
                                                   .TR_PLAN_NAME = row("TR_PLAN_NAME").ToString(),
                                                   .EXPECT_DATE_TO = ToDate(row("EXPECT_DATE_TO")),
                                                   .SENDER_TITLE_ID = ToDecimal(row("SENDER_TITLE_ID")),
                                                   .SENDER_TITLE_NAME = row("SENDER_TITLE_NAME").ToString(),
                                                   .OTHER_COURSE = row("OTHER_COURSE").ToString(),
                                                   .TRAINER_NUMBER = ToDecimal(row("TRAINER_NUMBER")),
                                                   .TR_COMMIT = CBool(row("TR_COMMIT")),
                                                   .CERTIFICATE = CBool(row("CERTIFICATE")),
                                                   .TR_PLACE = row("TR_PLACE").ToString(),
                                                       .REQUEST_CODE = row("REQUEST_CODE").ToString()
                                                  }).ToList
                End If
            End Using

            'Using cls As New DataAccess.QueryData
            '    cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
            '                     New With {.P_USERNAME = log.Username,
            '                               .P_ORGID = _param.ORG_ID,
            '                               .P_ISDISSOLVE = _param.IS_DISSOLVE})
            'End Using

            'Dim lst = (From p In Context.TR_REQUEST
            '           From plan In Context.TR_PLAN.Where(Function(f) f.ID = p.TR_PLAN_ID).DefaultIfEmpty
            '           From Sender In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.REQUEST_SENDER_ID).DefaultIfEmpty
            '           From SenderInfo In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = Sender.ID).DefaultIfEmpty
            '           From Course In Context.TR_COURSE.Where(Function(f) f.ID = plan.TR_COURSE_ID).DefaultIfEmpty
            '           From TrainForm In Context.OT_OTHER_LIST.Where(Function(f) f.ID = plan.TRAIN_FORM_ID).DefaultIfEmpty
            '           From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
            '           From status In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.STATUS_ID).DefaultIfEmpty
            '           From c In Context.TR_COURSE.Where(Function(f) f.ID = plan.TR_COURSE_ID).DefaultIfEmpty
            '           From unit In Context.TR.Where(Function(f) f.ID = plan.TR_COURSE_ID).DefaultIfEmpty
            '           From k In Context.SE_CHOSEN_ORG.Where(Function(f) p.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)
            '           Select New RequestDTO With {.ID = p.ID,
            '                                       .STATUS_ID = status.ID,
            '                                       .REQUEST_SENDER_ID = Sender.ID,
            '                                       .SENDER_CODE = Sender.EMPLOYEE_CODE,
            '                                       .SENDER_NAME = Sender.FULLNAME_VN,
            '                                       .SENDER_MOBILE = SenderInfo.MOBILE_PHONE,
            '                                       .SENDER_EMAIL = SenderInfo.WORK_EMAIL,
            '                                       .REQUEST_DATE = p.REQUEST_DATE,
            '                                       .COURSE_ID = plan.TR_COURSE_ID,
            '                                       .COURSE_NAME = Course.NAME,
            '                                       .TRAIN_FORM_ID = TrainForm.ID,
            '                                       .TRAIN_FORM = TrainForm.NAME_VN,
            '                                       .TARGET_TRAIN = plan.TARGET_TRAIN,
            '                                       .CONTENT = p.CONTENT,
            '                                       .COM_NAME = org.NAME_VN,
            '                                       .COM_DESC = org.DESCRIPTION_PATH,
            '                                       .ORG_NAME = org.NAME_VN,
            '                                       .ORG_DESC = org.DESCRIPTION_PATH,
            '                                       .YEAR = p.YEAR,
            '                                       .STATUS_NAME = status.NAME_VN,
            '                                       .EXPECTED_COST = p.EXPECTED_COST,
            '                                       .EXPECTED_DATE = p.EXPECTED_DATE,
            '                                       .START_DATE = p.START_DATE,
            '                                       .TR_PLAN_NAME = c.NAME & " - " & plan.NAME,
            '                                       .CENTERS = plan.CENTERS,
            '                                       .VENUE = plan.VENUE,
            '                                       .REMARK = p.REMARK,
            '                                       .CREATED_DATE = p.CREATED_DATE})

            'If filter.YEAR IsNot Nothing Then
            '    lst = (From l In lst Where l.YEAR = filter.YEAR).ToList
            'End If

            'If filter.STATUS_ID IsNot Nothing Then
            '    lst = lst.Where(Function(p) p.STATUS_ID = filter.STATUS_ID)
            'End If

            'If filter.ORG_NAME <> "" Then
            '    lst = lst.Where(Function(p) p.ORG_NAME.ToUpper.Contains(filter.ORG_NAME.ToUpper))
            'End If

            'If filter.REQUEST_DATE IsNot Nothing Then
            '    lst = lst.Where(Function(p) p.REQUEST_DATE = filter.REQUEST_DATE)
            'End If

            'If filter.STATUS_NAME <> "" Then
            '    lst = lst.Where(Function(p) p.STATUS_NAME.ToUpper.Contains(filter.STATUS_NAME.ToUpper))
            'End If

            'If filter.EXPECTED_COST IsNot Nothing Then
            '    lst = lst.Where(Function(p) p.EXPECTED_COST = filter.EXPECTED_COST)
            'End If

            'If filter.EXPECTED_DATE IsNot Nothing Then
            '    lst = lst.Where(Function(p) p.EXPECTED_DATE = filter.EXPECTED_DATE)
            'End If

            'If filter.START_DATE IsNot Nothing Then
            '    lst = lst.Where(Function(p) p.START_DATE = filter.START_DATE)
            'End If

            'If filter.CONTENT <> "" Then
            '    lst = lst.Where(Function(p) p.CONTENT.ToUpper.Contains(filter.CONTENT.ToUpper))
            'End If

            'If filter.TR_PLAN_NAME <> "" Then
            '    lst = lst.Where(Function(p) p.TR_PLAN_NAME.ToUpper.Contains(filter.TR_PLAN_NAME.ToUpper))
            'End If

            'If filter.CENTERS <> "" Then
            '    lst = lst.Where(Function(p) p.CENTERS.ToUpper.Contains(filter.CENTERS.ToUpper))
            'End If

            'If filter.VENUE <> "" Then
            '    lst = lst.Where(Function(p) p.VENUE.ToUpper.Contains(filter.VENUE.ToUpper))
            'End If

            'lst = lst.OrderBy(Sorts)
            lst = (From l In lst Order By Sorts Select l
                   Skip PageIndex * PageSize
                   Take PageSize).ToList
            Total = lst.Count()
            'lst = lst.Skip(PageIndex * PageSize)
            'lst = lst.Take(PageSize)
            Return lst '.ToList

        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".LoadTrainingRequests")
            Throw ex
        End Try
    End Function
    Public Function GetTrainingRequestPortalApprove(ByVal filter As RequestDTO,
                                                 ByVal PageIndex As Integer, ByVal PageSize As Integer,
                                                 ByRef Total As Integer, ByVal _param As ParamDTO,
                                                Optional ByVal Sorts As String = "CREATED_DATE desc",
                                                Optional ByVal log As UserLog = Nothing) As List(Of RequestDTO)
        Try
            Dim lst As List(Of RequestDTO) = New List(Of RequestDTO)
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_TRAINING.GET_TR_REQUEST_PORTAL_APPROVE",
                                           New With {.P_YEAR = filter.YEAR,
                                                     .P_STATUS = filter.STATUS_ID,
                                                     .P_EMPLOYEE_ID = filter.EMPLOYEE_ID,
                                                     .P_USERNAME = log.Username.ToUpper,
                                                     .P_CUR = cls.OUT_CURSOR})
                If dtData IsNot Nothing Then
                    lst = (From row As DataRow In dtData.Rows
                           Select New RequestDTO With {.ID = row("ID").ToString(),
                                                   .STATUS_ID = If(row("STATUS_ID") IsNot Nothing, ToDecimal(row("STATUS_ID")), Nothing),
                                                   .STATUS_NAME = row("STATUS_NAME").ToString(),
                                                   .SENDER_CODE = row("SENDER_CODE").ToString(),
                                                   .SENDER_NAME = row("SENDER_NAME").ToString(),
                                                   .SENDER_EMAIL = row("SENDER_EMAIL").ToString(),
                                                   .REQUEST_SENDER_ID = If(row("REQUEST_SENDER_ID") IsNot Nothing, ToDecimal(row("REQUEST_SENDER_ID")), Nothing),
                                                   .SENDER_MOBILE = row("SENDER_MOBILE").ToString(),
                                                   .REQUEST_DATE = ToDate(row("REQUEST_DATE")),
                                                   .COURSE_NAME = row("COURSE_NAME").ToString(),
                                                   .TRAIN_FORM = row("TRAIN_FORM").ToString(),
                                                   .TARGET_TRAIN = row("TARGET_TRAIN").ToString(),
                                                   .CONTENT = row("CONTENT").ToString(),
                                                   .COM_ID = If(row("COM_ID") IsNot Nothing, ToDecimal(row("COM_ID")), Nothing),
                                                   .COM_NAME = row("COM_NAME").ToString(),
                                                   .COM_DESC = row("COM_DESC").ToString(),
                                                   .ORG_ID = If(row("ORG_ID") IsNot Nothing, ToDecimal(row("ORG_ID")), Nothing),
                                                   .ORG_NAME = row("ORG_NAME").ToString(),
                                                   .ORG_DESC = row("ORG_DESC").ToString(),
                                                   .EXPECTED_DATE = ToDate(row("EXPECTED_DATE")),
                                                   .START_DATE = ToDate(row("START_DATE")),
                                                   .CENTERS = row("CENTERS").ToString(),
                                                   .UNIT_NAME = row("UNIT_NAME").ToString(),
                                                   .TEACHERS = row("TEACHERS").ToString(),
                                                   .VENUE = row("VENUE").ToString(),
                                                   .EXPECTED_COST = If(row("EXPECTED_COST") IsNot Nothing, ToDecimal(row("EXPECTED_COST")), Nothing),
                                                   .REMARK = row("REMARK").ToString(),
                                                   .CREATED_DATE = row("CREATED_DATE").ToString(),
                                                   .REJECT_REASON = row("REJECT_REASON").ToString(),
                                                   .TR_PLAN_NAME = row("TR_PLAN_NAME").ToString(),
                                                   .EXPECT_DATE_TO = ToDate(row("EXPECT_DATE_TO")),
                                                   .SENDER_TITLE_ID = If(row("SENDER_TITLE_ID") IsNot Nothing, ToDecimal(row("SENDER_TITLE_ID")), Nothing),
                                                   .SENDER_TITLE_NAME = row("SENDER_TITLE_NAME").ToString(),
                                                   .OTHER_COURSE = row("OTHER_COURSE").ToString(),
                                                   .TRAINER_NUMBER = If(row("TRAINER_NUMBER") IsNot Nothing, ToDecimal(row("TRAINER_NUMBER")), Nothing),
                                                   .TR_COMMIT = If(row("TR_COMMIT") IsNot Nothing, CBool(row("TR_COMMIT")), Nothing),
                                                   .CERTIFICATE = If(row("CERTIFICATE") IsNot Nothing, CBool(row("CERTIFICATE")), Nothing),
                                                   .TR_PLACE = row("TR_PLACE").ToString(),
                                                   .REASON_PORTAL = row("REASON_PORTAL").ToString(),
                                                   .EMPLOYEE_APPROVED = row("EMPLOYEE_APPROVED").ToString(),
                                                   .IS_APPROVE = If(row("IS_APPROVE") IsNot Nothing, ToDecimal(row("IS_APPROVE")), Nothing)
                                                  }).ToList
                End If
            End Using
            lst = (From l In lst Order By Sorts Select l
                   Skip PageIndex * PageSize
                   Take PageSize).ToList
            Total = lst.Count()
            Return lst

        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".LoadTrainingRequests")
            Throw ex
        End Try
    End Function
    Public Function GetTrainingRequestPortal(ByVal filter As RequestDTO,
                                                 ByVal PageIndex As Integer, ByVal PageSize As Integer,
                                                 ByRef Total As Integer, ByVal _param As ParamDTO,
                                                Optional ByVal Sorts As String = "CREATED_DATE desc",
                                                Optional ByVal log As UserLog = Nothing) As List(Of RequestDTO)
        Try
            Dim lst As List(Of RequestDTO) = New List(Of RequestDTO)
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_TRAINING.GET_TR_REQUEST_PORTAL",
                                           New With {.P_YEAR = filter.YEAR,
                                                     .P_STATUS = filter.STATUS_ID,
                                                     .P_EMPLOYEE_ID = filter.EMPLOYEE_ID,
                                                     .P_USERNAME = log.Username.ToUpper,
                                                     .P_CUR = cls.OUT_CURSOR})
                If dtData IsNot Nothing Then
                    lst = (From row As DataRow In dtData.Rows
                           Select New RequestDTO With {.ID = row("ID").ToString(),
                                                   .STATUS_ID = If(row("STATUS_ID") IsNot Nothing, ToDecimal(row("STATUS_ID")), Nothing),
                                                   .STATUS_NAME = row("STATUS_NAME").ToString(),
                                                   .SENDER_CODE = row("SENDER_CODE").ToString(),
                                                   .SENDER_NAME = row("SENDER_NAME").ToString(),
                                                   .SENDER_EMAIL = row("SENDER_EMAIL").ToString(),
                                                   .REQUEST_SENDER_ID = If(row("REQUEST_SENDER_ID") IsNot Nothing, ToDecimal(row("REQUEST_SENDER_ID")), Nothing),
                                                   .SENDER_MOBILE = row("SENDER_MOBILE").ToString(),
                                                   .REQUEST_DATE = ToDate(row("REQUEST_DATE")),
                                                   .COURSE_NAME = row("COURSE_NAME").ToString(),
                                                   .TRAIN_FORM = row("TRAIN_FORM").ToString(),
                                                   .TARGET_TRAIN = row("TARGET_TRAIN").ToString(),
                                                   .CONTENT = row("CONTENT").ToString(),
                                                   .COM_ID = If(row("COM_ID") IsNot Nothing, ToDecimal(row("COM_ID")), Nothing),
                                                   .COM_NAME = row("COM_NAME").ToString(),
                                                   .COM_DESC = row("COM_DESC").ToString(),
                                                   .ORG_ID = If(row("ORG_ID") IsNot Nothing, ToDecimal(row("ORG_ID")), Nothing),
                                                   .ORG_NAME = row("ORG_NAME").ToString(),
                                                   .ORG_DESC = row("ORG_DESC").ToString(),
                                                   .EXPECTED_DATE = ToDate(row("EXPECTED_DATE")),
                                                   .START_DATE = ToDate(row("START_DATE")),
                                                   .CENTERS = row("CENTERS").ToString(),
                                                   .UNIT_NAME = row("UNIT_NAME").ToString(),
                                                   .TEACHERS = row("TEACHERS").ToString(),
                                                   .VENUE = row("VENUE").ToString(),
                                                   .EXPECTED_COST = If(row("EXPECTED_COST") IsNot Nothing, ToDecimal(row("EXPECTED_COST")), Nothing),
                                                   .REMARK = row("REMARK").ToString(),
                                                   .CREATED_DATE = row("CREATED_DATE").ToString(),
                                                   .REJECT_REASON = row("REJECT_REASON").ToString(),
                                                   .TR_PLAN_NAME = row("TR_PLAN_NAME").ToString(),
                                                   .EXPECT_DATE_TO = ToDate(row("EXPECT_DATE_TO")),
                                                   .SENDER_TITLE_ID = If(row("SENDER_TITLE_ID") IsNot Nothing, ToDecimal(row("SENDER_TITLE_ID")), Nothing),
                                                   .SENDER_TITLE_NAME = row("SENDER_TITLE_NAME").ToString(),
                                                   .OTHER_COURSE = row("OTHER_COURSE").ToString(),
                                                   .TRAINER_NUMBER = If(row("TRAINER_NUMBER") IsNot Nothing, ToDecimal(row("TRAINER_NUMBER")), Nothing),
                                                   .TR_COMMIT = If(row("TR_COMMIT") IsNot Nothing, CBool(row("TR_COMMIT")), Nothing),
                                                   .CERTIFICATE = If(row("CERTIFICATE") IsNot Nothing, CBool(row("CERTIFICATE")), Nothing),
                                                   .TR_PLACE = row("TR_PLACE").ToString(),
                                                   .REASON_PORTAL = row("REASON_PORTAL").ToString(),
                                                   .REQUEST_CODE = row("REQUEST_CODE").ToString(),
                                                   .EMPLOYEE_APPROVED = row("EMPLOYEE_APPROVED").ToString(),
                                                   .IS_APPROVE = If(row("IS_APPROVE") IsNot Nothing, ToDecimal(row("IS_APPROVE")), Nothing)
                                                  }).ToList
                End If
            End Using
            lst = (From l In lst Order By Sorts Select l
                   Skip PageIndex * PageSize
                   Take PageSize).ToList
            Total = lst.Count()
            Return lst

        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".LoadTrainingRequests")
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

    Public Function GetTrainingRequestsByID(ByVal filter As RequestDTO) As RequestDTO
        Try
            Dim req As RequestDTO = New RequestDTO
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_TRAINING.GET_TR_REQUEST_BY_ID",
                                           New With {.P_ID = filter.ID,
                                                     .P_CUR = cls.OUT_CURSOR})
                If dtData IsNot Nothing Then
                    req = (From row As DataRow In dtData.Rows
                           Select New RequestDTO With {.ID = row("ID").ToString(),
                                                   .TR_PLAN_ID = row("TR_PLAN_ID").ToString(),
                                                   .ORG_ID = If(IsDBNull(row("ORG_ID")), Nothing, row("ORG_ID").ToString()),
                                                   .ORG_NAME = row("ORG_NAME").ToString(),
                                                   .ORG_DESC = row("ORG_DESC").ToString(),
                                                   .YEAR = row("YEAR").ToString(),
                                                   .IS_IRREGULARLY = row("IS_IRREGULARLY").ToString(),
                                                   .COURSE_ID = row("COURSE_ID").ToString(),
                                                   .COURSE_NAME = row("COURSE_NAME").ToString(),
                                                   .PROGRAM_GROUP = row("PROGRAM_GROUP").ToString(),
                                                   .TRAIN_FIELD = row("TRAIN_FIELD").ToString(),
                                                   .TRAIN_FORM_ID = row("TRAINFORM_ID").ToString(),
                                                   .TRAIN_FORM = row("TRAINFORM").ToString(),
                                                   .PROPERTIES_NEED_ID = row("PROPERTIES_NEED_ID").ToString(),
                                                   .PROPERTIES_NEED = row("PROPERTIES_NEED").ToString(),
                                                   .EXPECTED_DATE = ToDate(row("EXPECTED_DATE")),
                                                   .START_DATE = ToDate(row("START_DATE")),
                                                   .CONTENT = row("CONTENT").ToString(),
                                                   .UNIT_ID = row("UNIT_ID").ToString(),
                                                   .UNIT_NAME = row("UNIT").ToString(),
                                                   .EXPECTED_COST = row("EXPECTED_COST").ToString(),
                                                   .TR_CURRENCY_ID = row("CURRENCY_ID").ToString(),
                                                   .TR_CURRENCY_NAME = row("CURRENCY").ToString(),
                                                   .STATUS_ID = row("STATUS_ID").ToString(),
                                                   .IS_APPROVE = row("APPROVE_ID").ToString(),
                                                   .STATUS_NAME = row("STATUS").ToString(),
                                                   .TARGET_TRAIN = row("TARGET_TRAIN").ToString(),
                                                   .VENUE = row("VENUE").ToString(),
                                                   .REQUEST_SENDER_ID = row("SENDER_ID").ToString(),
                                                   .SENDER_CODE = row("SENDER_CODE").ToString(),
                                                   .SENDER_NAME = row("SENDER_NAME").ToString(),
                                                   .SENDER_EMAIL = row("SENDER_EMAIL").ToString(),
                                                   .SENDER_MOBILE = row("SENDER_PHONE").ToString(),
                                                   .REQUEST_DATE = ToDate(row("REQUEST_DATE")),
                                                   .ATTACH_FILE = row("ATTACH_FILE").ToString(),
                                                   .REMARK = row("REMARK").ToString(),
                                                   .REJECT_REASON = row("REJECT_REASON").ToString(),
                                                   .EXPECT_DATE_TO = ToDate(row("EXPECT_DATE_TO")),
                                                   .SENDER_TITLE_ID = ToDecimal(row("SENDER_TITLE_ID")),
                                                   .OTHER_COURSE = row("OTHER_COURSE").ToString(),
                                                   .TRAINER_NUMBER = ToDecimal(row("TRAINER_NUMBER")),
                                                   .TR_COMMIT = CBool(row("TR_COMMIT")),
                                                   .CERTIFICATE = CBool(row("CERTIFICATE")),
                                                   .CENTERS_ID = row("CENTERS_ID").ToString(),
                                                   .TEACHERS_ID = row("TEACHERS_ID").ToString(),
                                                   .EMPLOYEE_APPROVED_ID = row("EMPLOYEE_APPROVED_ID").ToString(),
                                                       .REQUEST_CODE = row("REQUEST_CODE").ToString(),
                                                   .TR_PLACE = row("TR_PLACE").ToString()}).FirstOrDefault

                    req.lstCenters = (From p In Context.TR_REQUEST_CENTER
                                      From c In Context.TR_CENTER.Where(Function(f) f.ID = p.CENTER_ID)
                                      Where p.TR_REQUEST_ID = filter.ID
                                      Select New PlanCenterDTO With {.ID = p.CENTER_ID,
                                                                           .NAME_EN = c.NAME_EN,
                                                                           .NAME_VN = c.NAME_VN}).ToList
                End If

                dtData = cls.ExecuteStore("PKG_TRAINING.GET_EMPLOYEE_BY_REQUEST_ID",
                                           New With {.P_ID = filter.ID,
                                                     .P_CUR = cls.OUT_CURSOR})
                If dtData IsNot Nothing Then
                    req.lstEmp = (From row As DataRow In dtData.Rows
                                  Select New RequestEmpDTO With {.EMPLOYEE_ID = row("EMPLOYEE_ID").ToString(),
                                                      .EMPLOYEE_CODE = row("EMPLOYEE_CODE").ToString(),
                                                      .EMPLOYEE_NAME = row("EMPLOYEE_NAME").ToString(),
                                                      .BIRTH_DATE = row("BIRTH_DATE").ToString(),
                                                      .TITLE_NAME = row("TITLE_NAME").ToString(),
                                                      .COM_NAME = row("COM_NAME").ToString(),
                                                      .ORG_NAME = row("ORG_NAME").ToString(),
                                                      .WORK_INVOLVE = row("WORK_INVOLVE").ToString()}).ToList
                End If
            End Using

            Return req

            'Dim lst = (From p In Context.TR_REQUEST
            '           From plan In Context.TR_PLAN.Where(Function(f) f.ID = p.TR_PLAN_ID)
            '           From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID)
            '           Where p.ID = filter.ID
            '       Select New RequestDTO With {.ID = p.ID,
            '                                   .ORG_ID = p.ORG_ID,
            '                                   .ORG_NAME = org.NAME_VN,
            '                                   .REQUEST_DATE = p.REQUEST_DATE,
            '                                   .YEAR = p.YEAR,
            '                                   .EXPECTED_COST = p.EXPECTED_COST,
            '                                   .EXPECTED_DATE = p.EXPECTED_DATE,
            '                                   .START_DATE = p.START_DATE,
            '                                   .CONTENT = p.CONTENT,
            '                                   .CENTERS = plan.CENTERS,
            '                                   .VENUE = plan.VENUE,
            '                                   .TR_PLAN_ID = p.TR_PLAN_ID,
            '                                   .TR_CURRENCY_ID = p.TR_CURRENCY_ID,
            '                                   .STATUS_ID = p.STATUS_ID,
            '                                   .CREATED_DATE = p.CREATED_DATE})

            'Dim obj = lst.FirstOrDefault
            'obj.lstEmp = (From req_emp In Context.TR_REQUEST_EMPLOYEE
            '              From p In Context.HU_EMPLOYEE.Where(Function(f) f.ID = req_emp.EMPLOYEE_ID)
            '              From cv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = p.ID).DefaultIfEmpty
            '              From gender In Context.OT_OTHER_LIST.Where(Function(f) f.ID = cv.GENDER).DefaultIfEmpty
            '              From title In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
            '             Where req_emp.TR_REQUEST_ID = obj.ID
            '              Select New RequestEmpDTO With {.EMPLOYEE_ID = p.ID,
            '                                             .EMPLOYEE_CODE = p.EMPLOYEE_CODE,
            '                                             .EMPLOYEE_NAME = p.FULLNAME_VN,
            '                                             .BIRTH_DATE = cv.BIRTH_DATE,
            '                                             .GENDER_NAME = gender.NAME_VN,
            '                                             .TITLE_NAME = title.NAME_VN}).ToList
            'Return obj
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetTrainingRequestsByID")
            Throw ex
        End Try
    End Function

    Public Function GetEmployeeByImportRequest(ByRef lstEmpCode As List(Of RequestEmpDTO)) As String
        Try
            Dim sError As String = ""
            Dim lstError As New List(Of String)
            For Each item In lstEmpCode
                Dim empCode As String = item.EMPLOYEE_CODE
                Dim emp = (From p In Context.HU_EMPLOYEE Where p.EMPLOYEE_CODE = empCode
                           From cv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = p.ID).DefaultIfEmpty
                           From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                           From title In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
                           Select New RequestEmpDTO With {.EMPLOYEE_ID = p.ID,
                                                          .ORG_NAME = org.NAME_VN,
                                                          .COM_NAME = org.NAME_VN,
                                                          .TITLE_NAME = title.NAME_VN,
                                                          .EMPLOYEE_CODE = p.EMPLOYEE_CODE,
                                                          .EMPLOYEE_NAME = p.FULLNAME_VN,
                                                          .BIRTH_DATE = cv.BIRTH_DATE}).FirstOrDefault
                If emp Is Nothing Then
                    lstError.Add(item.EMPLOYEE_CODE)
                Else
                    item.EMPLOYEE_ID = emp.ID
                End If
            Next
            If lstError.Count > 0 Then
                sError = lstError.Aggregate(Function(x, y) x & ", " & y)
            End If
            Return sError

        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetTrainingRequestsByID")
            Throw ex
        End Try
    End Function

    Public Function InsertRequest(ByVal Request As RequestDTO,
                                  ByVal lstEmp As List(Of RequestEmpDTO),
                                  ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objRequest As New TR_REQUEST
        Try
            With objRequest
                .ID = Utilities.GetNextSequence(Context, Context.TR_REQUEST.EntitySet.Name)
                .TR_PLAN_ID = Request.TR_PLAN_ID
                .ORG_ID = Request.ORG_ID
                .YEAR = Request.YEAR
                .IS_IRREGULARLY = Request.IS_IRREGULARLY
                .TR_COURSE_ID = Request.COURSE_ID
                .TRAIN_FORM_ID = Request.TRAIN_FORM_ID
                .PROPERTIES_NEED_ID = Request.PROPERTIES_NEED_ID
                .EXPECTED_DATE = Request.EXPECTED_DATE
                .START_DATE = Request.START_DATE
                .CONTENT = Request.CONTENT
                .TR_UNIT_ID = Request.UNIT_ID
                .EXPECTED_COST = Request.EXPECTED_COST
                .TR_CURRENCY_ID = Request.TR_CURRENCY_ID
                .STATUS_ID = Request.STATUS_ID
                .TARGET_TRAIN = Request.TARGET_TRAIN
                .VENUE = Request.VENUE
                .REQUEST_SENDER_ID = Request.REQUEST_SENDER_ID
                .REQUEST_DATE = Request.REQUEST_DATE
                .ATTACH_FILE = Request.ATTACH_FILE
                .REMARK = Request.REMARK
                .EXPECT_DATE_TO = Request.EXPECT_DATE_TO
                .SENDER_TITLE_ID = Request.SENDER_TITLE_ID
                .OTHER_COURSE = Request.OTHER_COURSE
                .TRAINER_NUMBER = Request.TRAINER_NUMBER
                .TR_COMMIT = Request.TR_COMMIT
                .CERTIFICATE = Request.CERTIFICATE
                .TR_PLACE = Request.TR_PLACE
                .CENTERS_ID = Request.CENTERS_ID
                .TEACHERS_ID = Request.TEACHERS_ID
                .IS_APPROVE = Request.IS_APPROVE
                .IS_PORTAL = Request.IS_PORTAL
                .REQUEST_CODE = Request.REQUEST_CODE
            End With
            Context.TR_REQUEST.AddObject(objRequest)

            If lstEmp IsNot Nothing Then
                For Each item In lstEmp
                    Dim objProgramEmpData As New TR_REQUEST_EMPLOYEE
                    objProgramEmpData.ID = Utilities.GetNextSequence(Context, Context.TR_REQUEST_EMPLOYEE.EntitySet.Name)
                    objProgramEmpData.EMPLOYEE_ID = item.EMPLOYEE_ID
                    objProgramEmpData.TR_REQUEST_ID = objRequest.ID
                    Context.TR_REQUEST_EMPLOYEE.AddObject(objProgramEmpData)
                Next
            End If

            If Request.lstCenters IsNot Nothing Then
                For Each item In Request.lstCenters
                    Dim objProgramCenterData As New TR_REQUEST_CENTER
                    objProgramCenterData.ID = Utilities.GetNextSequence(Context, Context.TR_REQUEST_CENTER.EntitySet.Name)
                    objProgramCenterData.TR_REQUEST_ID = objRequest.ID
                    objProgramCenterData.CENTER_ID = item.ID
                    Context.TR_REQUEST_CENTER.AddObject(objProgramCenterData)
                Next
            End If

            If Request.lstTeachers IsNot Nothing Then
                For Each item In Request.lstTeachers
                    Dim objProgramTeacherData As New TR_REQUEST_TEACHER
                    objProgramTeacherData.ID = Utilities.GetNextSequence(Context, Context.TR_REQUEST_TEACHER.EntitySet.Name)
                    objProgramTeacherData.TR_REQUEST_ID = objRequest.ID
                    objProgramTeacherData.TEACHER_ID = item.ID
                    Context.TR_REQUEST_TEACHER.AddObject(objProgramTeacherData)
                Next
            End If

            Context.SaveChanges(log)
            gID = objRequest.ID
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".InsertRequest")
            Throw ex
        End Try
    End Function

    Public Function ModifyRequest(ByVal Request As RequestDTO,
                                  ByVal lstEmp As List(Of RequestEmpDTO),
                                  ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objRequest As New TR_REQUEST With {.ID = Request.ID}
        Try
            Context.TR_REQUEST.Attach(objRequest)
            With objRequest
                .TR_PLAN_ID = Request.TR_PLAN_ID
                .ORG_ID = Request.ORG_ID
                .YEAR = Request.YEAR
                .IS_IRREGULARLY = Request.IS_IRREGULARLY
                .TR_COURSE_ID = Request.COURSE_ID
                .TRAIN_FORM_ID = Request.TRAIN_FORM_ID
                .PROPERTIES_NEED_ID = Request.PROPERTIES_NEED_ID
                .EXPECTED_DATE = Request.EXPECTED_DATE
                .START_DATE = Request.START_DATE
                .CONTENT = Request.CONTENT
                .TR_UNIT_ID = Request.UNIT_ID
                .EXPECTED_COST = Request.EXPECTED_COST
                .TR_CURRENCY_ID = Request.TR_CURRENCY_ID
                .STATUS_ID = Request.STATUS_ID
                .TARGET_TRAIN = Request.TARGET_TRAIN
                .VENUE = Request.VENUE
                .REQUEST_SENDER_ID = Request.REQUEST_SENDER_ID
                .REQUEST_DATE = Request.REQUEST_DATE
                .ATTACH_FILE = Request.ATTACH_FILE
                .REMARK = Request.REMARK
                .EXPECT_DATE_TO = Request.EXPECT_DATE_TO
                .SENDER_TITLE_ID = Request.SENDER_TITLE_ID
                .OTHER_COURSE = Request.OTHER_COURSE
                .TRAINER_NUMBER = Request.TRAINER_NUMBER
                .TR_COMMIT = Request.TR_COMMIT
                .CERTIFICATE = Request.CERTIFICATE
                .TR_PLACE = Request.TR_PLACE
                .CENTERS_ID = Request.CENTERS_ID
                .TEACHERS_ID = Request.TEACHERS_ID
                .IS_APPROVE = Request.IS_APPROVE
                .IS_PORTAL = Request.IS_PORTAL
                .REQUEST_CODE = Request.REQUEST_CODE
            End With

            Dim lstRegEmp = (From p In Context.TR_REQUEST_EMPLOYEE Where p.TR_REQUEST_ID = Request.ID).ToList
            For Each item In lstRegEmp
                Context.TR_REQUEST_EMPLOYEE.DeleteObject(item)
            Next
            If lstEmp IsNot Nothing Then
                For Each item In lstEmp
                    Dim objProgramEmpData As New TR_REQUEST_EMPLOYEE
                    objProgramEmpData.ID = Utilities.GetNextSequence(Context, Context.TR_REQUEST_EMPLOYEE.EntitySet.Name)
                    objProgramEmpData.EMPLOYEE_ID = item.EMPLOYEE_ID
                    objProgramEmpData.TR_REQUEST_ID = objRequest.ID
                    Context.TR_REQUEST_EMPLOYEE.AddObject(objProgramEmpData)
                Next
            End If

            Dim lstRegCen = (From p In Context.TR_REQUEST_CENTER Where p.TR_REQUEST_ID = Request.ID).ToList
            For Each item In lstRegCen
                Context.TR_REQUEST_CENTER.DeleteObject(item)
            Next
            If Request.lstCenters IsNot Nothing Then
                For Each item In Request.lstCenters
                    Dim objProgramCenterData As New TR_REQUEST_CENTER
                    objProgramCenterData.ID = Utilities.GetNextSequence(Context, Context.TR_REQUEST_CENTER.EntitySet.Name)
                    objProgramCenterData.TR_REQUEST_ID = objRequest.ID
                    objProgramCenterData.CENTER_ID = item.ID
                    Context.TR_REQUEST_CENTER.AddObject(objProgramCenterData)
                Next
            End If

            Dim lstRegTea = (From p In Context.TR_REQUEST_TEACHER Where p.TR_REQUEST_ID = Request.ID).ToList
            For Each item In lstRegTea
                Context.TR_REQUEST_TEACHER.DeleteObject(item)
            Next
            If Request.lstTeachers IsNot Nothing Then
                For Each item In Request.lstTeachers
                    Dim objProgramTeacherData As New TR_REQUEST_TEACHER
                    objProgramTeacherData.ID = Utilities.GetNextSequence(Context, Context.TR_REQUEST_TEACHER.EntitySet.Name)
                    objProgramTeacherData.TR_REQUEST_ID = objRequest.ID
                    objProgramTeacherData.TEACHER_ID = item.ID
                    Context.TR_REQUEST_TEACHER.AddObject(objProgramTeacherData)
                Next
            End If

            Context.SaveChanges(log)
            gID = objRequest.ID
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".ModifyRequest")
            Throw ex
        End Try

    End Function

    Public Function UpdateStatusTrainingRequests(ByVal lstID As List(Of Decimal), ByVal status As Decimal) As Boolean
        Try
            Dim lstRequest = (From p In Context.TR_REQUEST Where lstID.Contains(p.ID)).ToList
            For Each item In lstRequest
                item.STATUS_ID = status
            Next

            'Context.SaveChanges()
            'Return True

            If status = TrainingCommon.TR_REQUEST_STATUS.APPROVE_ID Then
                For Each item In lstRequest
                    Dim objProgram As ProgramDTO
                    If item.TR_PLAN_ID IsNot Nothing Then
                        objProgram = (From p In Context.TR_PLAN
                                      From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID)
                                      Where p.ID = item.TR_PLAN_ID
                                      Select New ProgramDTO With {.YEAR = p.YEAR,
                                                                .DURATION = p.DURATION,
                                                                .NAME = p.NAME,
                                                                .ORG_ID = p.ORG_ID,
                                                                .ORG_NAME = org.NAME_VN,
                                                                .REMARK = p.REMARK,
                                                                .TARGET_TRAIN = p.TARGET_TRAIN,
                                                                .TR_COURSE_ID = p.TR_COURSE_ID,
                                                                .TR_CURRENCY_ID = p.TR_CURRENCY_ID,
                                                                .TR_DURATION_UNIT_ID = p.TR_DURATION_UNIT_ID,
                                                                .VENUE = p.VENUE,
                                                                .Departments_NAME = p.DEPARTMENTS,
                                                                .Titles_NAME = p.TITLES,
                                                                .Centers_NAME = p.CENTERS,
                                                                .TR_PLAN_ID = p.ID}).FirstOrDefault
                    Else
                        objProgram = New ProgramDTO
                        objProgram.YEAR = item.YEAR
                        objProgram.ORG_ID = item.ORG_ID
                        objProgram.REMARK = item.REMARK
                        objProgram.TARGET_TRAIN = item.TARGET_TRAIN
                        objProgram.TR_COURSE_ID = item.TR_COURSE_ID
                        objProgram.TR_CURRENCY_ID = item.TR_CURRENCY_ID
                        objProgram.VENUE = item.VENUE
                    End If


                    With objProgram
                        .IS_REIMBURSE = 0
                        '.IS_LOCAL = 0
                        .TR_REQUEST_ID = item.ID

                    End With

                    objProgram.Units = (From p In Context.TR_PLAN_UNIT
                                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID)
                                        Where p.TR_PLAN_ID = item.TR_PLAN_ID
                                        Select New ProgramOrgDTO With {.ID = p.ORG_ID,
                                                             .NAME = o.NAME_VN}).ToList

                    objProgram.Titles = (From p In Context.TR_PLAN_TITLE
                                         From t In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID)
                                         Where p.TR_PLAN_ID = item.TR_PLAN_ID
                                         Select New ProgramTitleDTO With {.ID = p.TITLE_ID,
                                                                .NAME = t.NAME_VN}).ToList

                    objProgram.Centers = (From p In Context.TR_PLAN_CENTER
                                          From c In Context.TR_CENTER.Where(Function(f) f.ID = p.CENTER_ID)
                                          Where p.TR_PLAN_ID = item.TR_PLAN_ID
                                          Select New ProgramCenterDTO With {.ID = p.CENTER_ID,
                                                                  .NAME_EN = c.NAME_EN,
                                                                  .NAME_VN = c.NAME_VN}).ToList

                    objProgram.Lectures = New List(Of ProgramLectureDTO)
                    InsertProgram(objProgram, Nothing, 0, False)
                Next
            End If
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".UpdateStatusTrainingRequests")
            Throw ex
        End Try
    End Function

    Public Function DeleteTrainingRequests(ByVal lstID As List(Of Decimal)) As Boolean
        Try
            Dim deletedRequestEmployee = (From record In Context.TR_REQUEST_EMPLOYEE Where lstID.Contains(record.TR_REQUEST_ID))
            For Each item In deletedRequestEmployee
                Context.TR_REQUEST_EMPLOYEE.DeleteObject(item)
            Next

            Dim deletedRequestCenter = (From record In Context.TR_REQUEST_CENTER Where lstID.Contains(record.TR_REQUEST_ID))
            For Each item In deletedRequestCenter
                Context.TR_REQUEST_CENTER.DeleteObject(item)
            Next

            Dim deletedRequestTeacher = (From record In Context.TR_REQUEST_TEACHER Where lstID.Contains(record.TR_REQUEST_ID))
            For Each item In deletedRequestTeacher
                Context.TR_REQUEST_TEACHER.DeleteObject(item)
            Next

            Dim deletedRequests = (From record In Context.TR_REQUEST Where lstID.Contains(record.ID))
            For Each item In deletedRequests
                Context.TR_REQUEST.DeleteObject(item)
            Next

            Context.SaveChanges()
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteTrainingRequests")
            Throw ex
        End Try
    End Function
    Public Function SubmitTrainingRequests(ByVal lstID As List(Of Decimal)) As Boolean
        Try
            Dim submitRequestEmployee = (From record In Context.TR_REQUEST Where lstID.Contains(record.ID))
            For Each item In submitRequestEmployee
                item.IS_APPROVE = 0
                Context.SaveChanges()
            Next
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteTrainingRequests")
            Throw ex
        End Try
    End Function
    Public Function RejectTrainingRequests(ByVal lstApprove As List(Of RequestDTO)) As Boolean
        Try
            For Each item In lstApprove
                Dim objRequest As New PROCESS_APPROVED_STATUS

                Dim s As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"
                Dim r As New Random
                Dim sb As New StringBuilder
                For i As Integer = 1 To 32
                    Dim idx As Integer = r.Next(0, 35)
                    sb.Append(s.Substring(idx, 1))
                Next
                Dim token = sb.ToString() + item.EMPLOYEE_ID.ToString

                With objRequest
                    .ID = Utilities.GetNextSequence(Context, Context.PROCESS_APPROVED_STATUS.EntitySet.Name)
                    .EMPLOYEE_ID = item.EMPLOYEE_ID
                    .EMPLOYEE_APPROVED = item.EMPLOYEE_APPROVED
                    .APP_DATE = Date.Now
                    .APP_LEVEL = 1
                    .CREATED_BY = item.EMPLOYEE_ID
                    .CREATED_DATE = Date.Now
                    .APP_STATUS = 1
                    .PE_PERIOD_ID = 0
                    .ID_REGGROUP = item.ID
                    .PROCESS_TYPE = "REQUEST"
                    .TOKEN = token
                End With
                Context.PROCESS_APPROVED_STATUS.AddObject(objRequest)

                Dim submitRequestEmployee = (From record In Context.TR_REQUEST Where record.ID = item.ID).FirstOrDefault
                submitRequestEmployee.IS_APPROVE = 1
                submitRequestEmployee.STATUS_ID = 447

                Context.SaveChanges()
            Next
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteTrainingRequests")
            Throw ex
        End Try
    End Function
    Public Function ApproveTrainingRequests(ByVal lstApprove As List(Of RequestDTO)) As Boolean
        Try
            For Each item In lstApprove
                Dim objRequest As New PROCESS_APPROVED_STATUS

                Dim s As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"
                Dim r As New Random
                Dim sb As New StringBuilder
                For i As Integer = 1 To 32
                    Dim idx As Integer = r.Next(0, 35)
                    sb.Append(s.Substring(idx, 1))
                Next
                Dim token = sb.ToString() + item.EMPLOYEE_ID.ToString

                With objRequest
                    .ID = Utilities.GetNextSequence(Context, Context.PROCESS_APPROVED_STATUS.EntitySet.Name)
                    .EMPLOYEE_ID = item.EMPLOYEE_ID
                    .EMPLOYEE_APPROVED = item.EMPLOYEE_APPROVED
                    .APP_DATE = Date.Now
                    .APP_LEVEL = 1
                    .CREATED_BY = item.EMPLOYEE_ID
                    .CREATED_DATE = Date.Now
                    .APP_STATUS = 1
                    .PE_PERIOD_ID = 0
                    .ID_REGGROUP = item.ID
                    .PROCESS_TYPE = "REQUEST"
                    .TOKEN = token
                End With
                Context.PROCESS_APPROVED_STATUS.AddObject(objRequest)

                Dim submitRequestEmployee = (From record In Context.TR_REQUEST Where record.ID = item.ID).FirstOrDefault
                submitRequestEmployee.IS_APPROVE = 1
                submitRequestEmployee.REASON_PORTAL = item.REASON_PORTAL

                Context.SaveChanges()
            Next
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteTrainingRequests")
            Throw ex
        End Try
    End Function

    Public Function GetEmployeeByPlanID(ByVal filter As RequestDTO) As List(Of RequestEmpDTO)
        Try
            Dim lst As List(Of RequestEmpDTO) = New List(Of RequestEmpDTO)
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_TRAINING.GET_EMPLOYEE_BY_PLANID",
                                           New With {.P_PLAN_ID = filter.TR_PLAN_ID,
                                                     .P_CUR = cls.OUT_CURSOR})
                If dtData IsNot Nothing Then
                    'For Each row As DataRow In dtData.Rows
                    '    lst.Add(New RequestEmpDTO With {.EMPLOYEE_ID = row("EMPLOYEE_ID").ToString(),
                    '                                  .EMPLOYEE_CODE = row("EMPLOYEE_CODE").ToString(),
                    '                                  .EMPLOYEE_NAME = row("EMPLOYEE_NAME").ToString(),
                    '                                  .BIRTH_DATE = row("BIRTH_DATE").ToString(),
                    '                                  .TITLE_NAME = row("TITLE_NAME").ToString(),
                    '                                  .COM_NAME = row("COM_NAME").ToString(),
                    '                                  .ORG_NAME = row("ORG_NAME").ToString(),
                    '                                  .WORK_INVOLVE = row("WORK_INVOLVE").ToString()})
                    'Next
                    lst = (From row As DataRow In dtData.Rows
                           Select New RequestEmpDTO With {.EMPLOYEE_ID = row("EMPLOYEE_ID").ToString(),
                                                      .EMPLOYEE_CODE = row("EMPLOYEE_CODE").ToString(),
                                                      .EMPLOYEE_NAME = row("EMPLOYEE_NAME").ToString(),
                                                      .BIRTH_DATE = row("BIRTH_DATE").ToString(),
                                                      .TITLE_NAME = row("TITLE_NAME").ToString(),
                                                      .COM_NAME = row("COM_NAME").ToString(),
                                                      .ORG_NAME = row("ORG_NAME").ToString(),
                                                      .WORK_INVOLVE = row("WORK_INVOLVE").ToString()}).ToList
                End If
            End Using
            'Dim lst = (From p In Context.HU_EMPLOYEE
            '           From cv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = p.ID).DefaultIfEmpty
            '           From gender In Context.OT_OTHER_LIST.Where(Function(f) f.ID = cv.GENDER).DefaultIfEmpty
            '           From title In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
            '           From org_check In Context.TR_PLAN_UNIT.Where(Function(f) f.ORG_ID = p.ORG_ID)
            '           From title_check In Context.TR_PLAN_TITLE.Where(Function(f) f.TITLE_ID = p.TITLE_ID)
            '           From pro In Context.TR_PLAN.Where(Function(f) f.ID = org_check.TR_PLAN_ID And _
            '                                                    f.ID = title_check.TR_PLAN_ID)
            '           Where p.TER_EFFECT_DATE Is Nothing And pro.ID = filter.TR_PLAN_ID
            '           Select New RequestEmpDTO With {.EMPLOYEE_ID = p.ID,
            '                                          .EMPLOYEE_CODE = p.EMPLOYEE_CODE,
            '                                          .EMPLOYEE_NAME = p.FULLNAME_VN,
            '                                          .BIRTH_DATE = cv.BIRTH_DATE,
            '                                          .GENDER_NAME = gender.NAME_VN,
            '                                          .TITLE_NAME = title.NAME_VN})


            Return lst.ToList

        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetEmployeeByPlanID")
            Throw ex
        End Try
    End Function

    Public Function GetPlanRequestByID(ByVal Id As Decimal) As PlanDTO
        Try
            Dim objPlan As PlanDTO = (From plan In Context.TR_PLAN
                                      From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = plan.ORG_ID)
                                      Where plan.ID = Id
                                      Select New PlanDTO With {.ID = plan.ID,
                                                               .YEAR = plan.YEAR,
                                                               .COST_INCURRED = plan.COST_INCURRED,
                                                               .COST_OF_STUDENT = plan.COST_OF_STUDENT,
                                                               .COST_OF_STUDENT_USD = plan.COST_OF_STUDENT_USD,
                                                               .COST_OTHER = plan.COST_OTHER,
                                                               .COST_TOTAL = plan.COST_TOTAL,
                                                                .COST_TOTAL_USD = plan.COST_TOTAL_USD,
                                                               .COST_TRAIN = plan.COST_TRAIN,
                                                               .COST_TRAVEL = plan.COST_TRAVEL,
                                                               .DURATION = plan.DURATION,
                                                               .NAME = plan.NAME,
                                                               .ORG_ID = plan.ORG_ID,
                                                               .ORG_NAME = org.NAME_VN,
                                                               .PLAN_T1 = plan.PLAN_T1,
                                                               .PLAN_T10 = plan.PLAN_T10,
                                                               .PLAN_T11 = plan.PLAN_T11,
                                                               .PLAN_T12 = plan.PLAN_T12,
                                                               .PLAN_T2 = plan.PLAN_T2,
                                                               .PLAN_T3 = plan.PLAN_T3,
                                                               .PLAN_T4 = plan.PLAN_T4,
                                                               .PLAN_T5 = plan.PLAN_T5,
                                                               .PLAN_T6 = plan.PLAN_T6,
                                                               .PLAN_T7 = plan.PLAN_T7,
                                                               .PLAN_T8 = plan.PLAN_T8,
                                                               .PLAN_T9 = plan.PLAN_T9,
                                                               .REMARK = plan.REMARK,
                                                               .STUDENT_NUMBER = plan.STUDENT_NUMBER,
                                                               .TARGET_TRAIN = plan.TARGET_TRAIN,
                                                               .TEACHER_NUMBER = plan.TEACHER_NUMBER,
                                                               .TR_COURSE_ID = plan.TR_COURSE_ID,
                                                               .TR_CURRENCY_ID = plan.TR_CURRENCY_ID,
                                                               .TR_DURATION_UNIT_ID = plan.TR_DURATION_UNIT_ID,
                                                               .VENUE = plan.VENUE,
                                                               .Centers_NAME = plan.CENTERS,
                                                               .Departments_NAME = plan.DEPARTMENTS,
                                                               .Months_NAME = plan.MONTHS,
                                                               .TR_TRAIN_FORM_ID = plan.TRAIN_FORM_ID,
                                                               .PROPERTIES_NEED_ID = plan.PROPERTIES_NEED_ID,
                                                                    .UNIT_ID = plan.UNIT_ID,
                                                               .ATTACHFILE = plan.ATTACHFILE,
                                                               .Titles_NAME = plan.TITLES}).FirstOrDefault

            Return objPlan
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetPlanRequestByID")
            Throw ex
        End Try
    End Function

    Public Function GetEmployeeByCode(ByVal _employee_Code As String) As Decimal
        Try
            Dim query = From e In Context.HU_EMPLOYEE
                        Where e.EMPLOYEE_CODE.ToUpper.Equals(_employee_Code)
                        Select e.ID

            If query.Count > 0 Then
                Return query.FirstOrDefault
            End If
            Return 0
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetEmployeeByCode")
            Throw ex
        End Try
    End Function
#End Region

#Region "Program"

    Public Function GetPlan_Cost_Detail(ByVal Id As Decimal) As List(Of CostDetailDTO)
        Try
            Dim lst = (From p In Context.TR_PLAN_COST_DETAIL
                       Where p.PLAN_ID = Id
                       Select New CostDetailDTO With {.ID = p.ID,
                                                      .PLAN_ID = p.PLAN_ID,
                                                      .TYPE_ID = p.TYPE_ID,
                                                      .MONEY = p.MONEY,
                                                      .MONEY_TYPE = p.MONEY_TYPE,
                                                      .MODIFIED_DATE = p.MODIFIED_DATE
                                                     }).ToList

            Return lst
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetPlan_Cost_Detail")
            Throw ex
        End Try
    End Function
    Public Function GetProgramByChooseFormId(ByVal Id As Decimal) As ProgramDTO
        Try
            Dim idProgram = (From p In Context.TR_CHOOSE_FORM Where p.ID = Id Select p.TR_PROGRAM_ID).FirstOrDefault
            Dim objProgram As ProgramDTO = (From Program In Context.TR_PROGRAM
                                            Where Program.ID = idProgram
                                            Select New ProgramDTO With {.ID = Program.ID,
                                                                  .YEAR = Program.YEAR,
                                                                  .VENUE = Program.VENUE,
                                                                  .START_DATE = Program.START_DATE,
                                                                  .END_DATE = Program.END_DATE,
                                                                  .Centers_NAME = Program.CENTERS,
                                                                  .Lectures_NAME = Program.LECTURES}).FirstOrDefault

            Return objProgram
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetProgramByChooseFormId")
            Throw ex
        End Try
    End Function

    Public Function GetRequestsForProgram(ByVal ReqID As Decimal) As RequestDTO
        Try
            Dim lst As List(Of RequestDTO) = New List(Of RequestDTO)
            Using cls As New DataAccess.QueryData
                Dim dsData As DataSet = cls.ExecuteStore("PKG_TRAINING.GET_TR_REQUEST_FOR_PROGRAM",
                                           New With {.P_ID = ReqID,
                                                     .P_CUR = cls.OUT_CURSOR,
                                                     .P_CEN = cls.OUT_CURSOR,
                                                     .P_LEC = cls.OUT_CURSOR,
                                                     .P_JOI = cls.OUT_CURSOR}, False)
                Dim dtReq As DataTable = dsData.Tables(0)
                Dim dtCen As DataTable = dsData.Tables(1)
                Dim dtLec As DataTable = dsData.Tables(2)
                Dim dtEmp As DataTable = dsData.Tables(3)
                If dtReq IsNot Nothing Then
                    lst = (From row As DataRow In dtReq.Rows
                           Select New RequestDTO With {.ID = row("ID").ToString(),
                                                       .COURSE_ID = If(row("COURSE_ID") IsNot Nothing, ToDecimal(row("COURSE_ID")), Nothing),
                                                       .COURSE_NAME = row("COURSE_NAME").ToString(),
                                                       .TR_PLAN_NAME = row("PLAN_NAME").ToString(),
                                                       .TRAIN_FORM_ID = If(row("TRAIN_FORM_ID") IsNot Nothing, ToDecimal(row("TRAIN_FORM_ID")), Nothing),
                                                       .PROPERTIES_NEED_ID = If(row("PROPERTIES_NEED_ID") IsNot Nothing, ToDecimal(row("PROPERTIES_NEED_ID")), Nothing),
                                                       .PROGRAM_GROUP = row("PROGRAM_GROUP").ToString(),
                                                       .TRAIN_FIELD = row("TRAIN_FIELD").ToString(),
                                                       .CONTENT = row("CONTENT").ToString(),
                                                       .TARGET_TRAIN = row("TARGET_TRAIN").ToString(),
                                                       .VENUE = row("VENUE").ToString(),
                                                       .REMARK = row("REMARK").ToString(),
                                                       .TR_CURRENCY_NAME = row("CURRENCY_NAME").ToString()
                                                      }).ToList
                    If lst.Count > 0 Then
                        If dtCen IsNot Nothing Then
                            lst(0).lstCenters = (From row As DataRow In dtCen.Rows
                                                 Select New PlanCenterDTO With {.ID = row("ID").ToString(),
                                                                            .NAME_VN = row("NAME").ToString()
                                                                           }).ToList
                        End If
                        If dtLec IsNot Nothing Then
                            lst(0).lstTeachers = (From row As DataRow In dtLec.Rows
                                                  Select New LectureDTO With {.ID = row("ID").ToString(),
                                                                              .LECTURE_NAME = row("NAME").ToString()
                                                                             }).ToList
                        End If
                        If dtEmp IsNot Nothing Then
                            lst(0).NUM_EMP = dtEmp.Rows.Count

                            lst(0).lstOrgs = (From row As DataRow In dtEmp.Rows
                                              Select New PlanOrgDTO With {.ID = ToDecimal(row("ORG_ID")),
                                                                          .NAME = row("ORG_NAME").ToString()
                                                                         }).ToList
                            lst(0).lstOrgs = lst(0).lstOrgs.GroupBy(Function(x) x.ID).Select(Function(y) y.First).ToList

                            lst(0).lstTits = (From row As DataRow In dtEmp.Rows
                                              Select New PlanTitleDTO With {.ID = ToDecimal(row("TIT_ID")),
                                                                            .NAME = row("TIT_NAME").ToString()
                                                                           }).ToList
                            lst(0).lstTits = lst(0).lstTits.GroupBy(Function(x) x.ID).Select(Function(y) y.First).ToList

                            lst(0).lstWIs = (From row As DataRow In dtEmp.Rows
                                             Select New PlanTitleDTO With {.ID = ToDecimal(row("WI_ID")),
                                                                           .NAME = row("WI_NAME").ToString()
                                                                          }).ToList
                            lst(0).lstWIs = lst(0).lstWIs.GroupBy(Function(x) x.ID).Select(Function(y) y.First).ToList
                        End If
                    End If

                    If lst.Count > 0 Then
                        Return lst(0)
                    End If
                End If
            End Using
            Return Nothing
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".LoadTrainingRequests")
            Throw ex
        End Try
    End Function

    Public Function GetPlanEmployee(ByVal filter As RecordEmployeeDTO, ByVal _param As ParamDTO,
                                    ByVal PageIndex As Integer,
                                    ByVal lstTitleId As List(Of Decimal), ByVal lstTitleGR As List(Of Decimal), ByVal PageSize As Integer,
                                    ByRef Total As Integer, Optional ByVal Sorts As String = "CREATED_DATE desc",
                                    Optional ByVal log As UserLog = Nothing) As List(Of RecordEmployeeDTO)
        Try

            'Using cls As New DataAccess.QueryData
            '    cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
            '                     New With {.P_USERNAME = log.Username,
            '                               .P_ORGID = _param.ORG_ID,
            '                               .P_ISDISSOLVE = 0})
            'End Using

            Dim query = From e In Context.HU_EMPLOYEE
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From title In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From title_group In Context.OT_OTHER_LIST.Where(Function(f) f.ID = title.TITLE_GROUP_ID).DefaultIfEmpty
                        Where (lstTitleId.Contains(e.TITLE_ID) OrElse (lstTitleId.Count = 0 AndAlso lstTitleGR.Contains(title.TITLE_GROUP_ID)))
                        Select New RecordEmployeeDTO With {.ID = e.ID,
                                                           .ORG_ID = e.ORG_ID,
                                                           .ORG_NAME = org.NAME_VN,
                                                           .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                                                           .JOIN_DATE = e.JOIN_DATE,
                                                           .FULLNAME_VN = e.FULLNAME_VN,
                                                           .TITLE_ID = e.TITLE_ID,
                                                           .TITLE_NAME_VN = title.NAME_VN,
                                                           .CREATED_DATE = e.CREATED_DATE,
                                                           .TITLE_GROUP_ID = title.TITLE_GROUP_ID,
                                                           .TITLE_GROUP_NAME = title_group.NAME_VN,
                                                           .EMP_TYPE = 0,
                                                           .EMP_TYPE_NAME = "Theo nhóm chức danh"}
            Dim lst = query

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            'lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList()
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetPlanEmployee")
            Throw ex
        End Try
    End Function
    Public Function GetPrograms(ByVal filter As ProgramDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer, ByVal _param As ParamDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of ProgramDTO)
        Try

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = If(_param.IS_DISSOLVE = "False", "0", "1")})
            End Using

            Dim query = From Program In Context.TR_PROGRAM
                        From Request In Context.TR_REQUEST.Where(Function(f) f.ID = Program.TR_REQUEST_ID).DefaultIfEmpty
                        From Plan In Context.TR_PLAN.Where(Function(f) f.ID = Request.TR_PLAN_ID).DefaultIfEmpty
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = Program.ORG_ID).DefaultIfEmpty
                        From cou In Context.TR_COURSE.Where(Function(f) f.ID = Program.TR_COURSE_ID).DefaultIfEmpty
                        From group In Context.TR_PROGRAM_GROUP.Where(Function(f) f.ID = cou.TR_PROGRAM_GROUP).DefaultIfEmpty
                        From field In Context.OT_OTHER_LIST.Where(Function(f) f.ID = cou.TR_TRAIN_FIELD).DefaultIfEmpty
                        From form In Context.OT_OTHER_LIST.Where(Function(f) f.ID = Program.TRAIN_FORM_ID).DefaultIfEmpty
                        From proned In Context.OT_OTHER_LIST.Where(Function(f) f.ID = Program.PROPERTIES_NEED_ID).DefaultIfEmpty
                        From duruni In Context.OT_OTHER_LIST.Where(Function(f) f.ID = Program.TR_DURATION_UNIT_ID).DefaultIfEmpty
                        From type In Context.OT_OTHER_LIST.Where(Function(f) f.ID = Program.TR_TYPE_ID).DefaultIfEmpty
                        From durstu In Context.OT_OTHER_LIST.Where(Function(f) f.ID = Program.DURATION_STUDY_ID).DefaultIfEmpty
                        From lang In Context.OT_OTHER_LIST.Where(Function(f) f.ID = Program.TR_LANGUAGE_ID).DefaultIfEmpty
                        From tc In Context.OT_OTHER_LIST.Where(Function(f) f.ID = Program.PROPERTIES_NEED_ID).DefaultIfEmpty
                        From unit In Context.TR_UNIT.Where(Function(f) f.ID = Program.TR_UNIT_ID).DefaultIfEmpty
                        From cure In Context.OT_OTHER_LIST.Where(Function(f) f.ID = Request.TR_CURRENCY_ID).DefaultIfEmpty
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = Program.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)
                        Select New ProgramDTO With {.ID = Program.ID,
                                                    .TR_REQUEST_ID = Program.TR_REQUEST_ID,
                                                    .TR_COURSE_ID = Program.TR_COURSE_ID,
                                                    .TR_COURSE_NAME = cou.NAME,
                                                    .YEAR = Program.YEAR,
                                                    .ORG_ID = Program.ORG_ID,
                                                    .ORG_NAME = org.NAME_VN,
                                                    .NAME = Program.NAME,
                                                    .PROGRAM_GROUP = group.NAME,
                                                    .TRAIN_FIELD = field.NAME_VN,
                                                    .TRAIN_FORM_ID = Program.TRAIN_FORM_ID,
                                                    .TRAIN_FORM_NAME = form.NAME_VN,
                                                    .PROPERTIES_NEED_ID = Program.PROPERTIES_NEED_ID,
                                                    .PROPERTIES_NEED_NAME = proned.NAME_VN,
                                                    .DURATION = Program.DURATION,
                                                    .TR_DURATION_UNIT_ID = Program.TR_DURATION_UNIT_ID,
                                                    .TR_DURATION_UNIT_NAME = duruni.NAME_VN,
                                                    .START_DATE = Program.START_DATE,
                                                    .END_DATE = Program.END_DATE,
                                                    .DURATION_STUDY_ID = Program.DURATION_STUDY_ID,
                                                    .DURATION_STUDY_NAME = durstu.NAME_VN,
                                                    .DURATION_HC = Program.DURATION_HC,
                                                    .DURATION_OT = Program.DURATION_OT,
                                                    .IS_REIMBURSE = Program.IS_REIMBURSE,
                                                    .IS_RETEST = Program.IS_RETEST,
                                                    .TR_CURRENCY_NAME = cure.NAME_VN,
                                                    .PLAN_STUDENT_NUMBER = Plan.STUDENT_NUMBER,
                                                    .PLAN_COST_TOTAL_US = Plan.COST_TOTAL_USD,
                                                    .PLAN_COST_TOTAL = Plan.COST_TOTAL,
                                                    .STUDENT_NUMBER = Program.STUDENT_NUMBER,
                                                    .COST_TOTAL_US = Program.COST_TOTAL_US,
                                                    .COST_STUDENT_US = Program.COST_STUDENT_US,
                                                    .COST_TOTAL = Program.COST_TOTAL,
                                                    .COST_STUDENT = Program.COST_STUDENT,
                                                    .Departments_NAME = Program.DEPARTMENTS,
                                                    .Titles_NAME = Program.TITLES,
                                                    .TR_LANGUAGE_ID = Program.TR_LANGUAGE_ID,
                                                    .TR_LANGUAGE_NAME = lang.NAME_VN,
                                                    .TR_UNIT_ID = Program.TR_UNIT_ID,
                                                    .TR_UNIT_NAME = unit.NAME,
                                                    .Centers_NAME = Program.CENTERS,
                                                    .Lectures_NAME = Program.LECTURES,
                                                    .CONTENT = Program.CONTENT,
                                                    .TARGET_TRAIN = Program.TARGET_TRAIN,
                                                    .VENUE = Program.VENUE,
                                                    .REMARK = Program.REMARK,
                                                    .CREATED_DATE = Program.CREATED_DATE,
                                                    .TR_PROGRAM_CODE = Program.TR_PROGRAM_CODE,
                                                    .PROGRAM_TYPE = If(Program.IS_PLAN = -1, "Đột xuất", "Theo nhu cầu đào tạo"),
                                                    .TR_TYPE_NAME = type.NAME_VN,
                                                    .TR_COMMIT_BOOL = Program.TR_COMMIT,
                                                    .CERTIFICATE_BOOL = Program.CERTIFICATE,
                                                    .TR_AFTER_TRAIN_BOOL = Program.TR_AFTER_TRAIN,
                                                    .SUM_RATIO = If((From a In Context.TR_CLASS.Where(Function(f) f.TR_PROGRAM_ID = Program.ID) Select a.RATIO).Sum Is Nothing, 0, (From a In Context.TR_CLASS.Where(Function(f) f.TR_PROGRAM_ID = Program.ID) Select a.RATIO).Sum),
                                                    .TR_TYPE_ID = Program.TR_TYPE_ID}

            Dim lst = query
            If filter.START_DATE.HasValue Then
                lst = lst.Where(Function(p) p.START_DATE >= filter.START_DATE)
            End If
            If filter.END_DATE.HasValue Then
                lst = lst.Where(Function(p) p.END_DATE <= filter.END_DATE)
            End If
            'If filter.NAME <> "" Then
            '    lst = lst.Where(Function(p) p.NAME.ToUpper.Contains(filter.NAME.ToUpper))
            'End If
            'If filter.ORG_NAME <> "" Then
            '    lst = lst.Where(Function(p) p.ORG_NAME.ToUpper.Contains(filter.ORG_NAME.ToUpper))
            'End If
            'If filter.TR_COURSE_NAME <> "" Then
            '    lst = lst.Where(Function(p) p.TR_COURSE_NAME.ToUpper.Contains(filter.TR_COURSE_NAME.ToUpper))
            'End If
            'If filter.TR_TRAIN_ENTRIES_NAME <> "" Then
            '    lst = lst.Where(Function(p) p.TR_TRAIN_ENTRIES_NAME.ToUpper.Contains(filter.TR_TRAIN_ENTRIES_NAME.ToUpper))
            'End If
            'If filter.TR_DURATION_UNIT_NAME <> "" Then
            '    lst = lst.Where(Function(p) p.TR_DURATION_UNIT_NAME.ToUpper.Contains(filter.TR_DURATION_UNIT_NAME.ToUpper))
            'End If
            'If filter.DURATION.HasValue Then
            '    lst = lst.Where(Function(p) p.DURATION = filter.DURATION)
            'End If
            'If filter.CURRENCY <> "" Then
            '    lst = lst.Where(Function(p) p.CURRENCY.ToUpper.Contains(filter.CURRENCY.ToUpper))
            'End If
            'If filter.TARGET_TRAIN <> "" Then
            '    lst = lst.Where(Function(p) p.TARGET_TRAIN.ToUpper.Contains(filter.TARGET_TRAIN.ToUpper))
            'End If
            'If filter.VENUE <> "" Then
            '    lst = lst.Where(Function(p) p.VENUE.ToUpper.Contains(filter.VENUE.ToUpper))
            'End If
            'If filter.REMARK <> "" Then
            '    lst = lst.Where(Function(p) p.REMARK.ToUpper.Contains(filter.REMARK.ToUpper))
            'End If
            'If filter.Departments_NAME <> "" Then
            '    lst = lst.Where(Function(p) p.Departments_NAME.ToUpper.Contains(filter.Departments_NAME.ToUpper))
            'End If
            'If filter.Titles_NAME <> "" Then
            '    lst = lst.Where(Function(p) p.Titles_NAME.ToUpper.Contains(filter.Titles_NAME.ToUpper))
            'End If
            'If filter.Lectures_NAME <> "" Then
            '    lst = lst.Where(Function(p) p.Lectures_NAME.ToUpper.Contains(filter.Lectures_NAME.ToUpper))
            'End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            ' Return Nothing
            Dim rs = lst.ToList

            For Each item As ProgramDTO In rs
                Dim centers As String = ""
                Dim lstCenter = (From p In Context.TR_PROGRAM_CENTER
                                 From c In Context.TR_CENTER.Where(Function(f) f.ID = p.TR_CENTER_ID)
                                 Where p.TR_PROGRAM_ID = item.ID
                                 Select New ProgramCenterDTO With {.ID = p.TR_CENTER_ID,
                                                                 .NAME_EN = c.NAME_EN,
                                                                 .NAME_VN = c.NAME_VN}).ToList
                For Each itemCenter As ProgramCenterDTO In lstCenter
                    centers = itemCenter.NAME_VN + "; "
                Next
                item.Centers_NAME = centers
            Next
            Return rs
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetPrograms")
            Throw ex
        End Try
    End Function

    Public Function GetProgramEvaluatePortal(ByVal filter As ProgramDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of ProgramDTO)
        Try

            Dim query = From Program In Context.TR_PROGRAM
                        From pro_emp In Context.TR_PROGRAM_EMP.Where(Function(f) f.TR_PROGRAM_ID = Program.ID And f.EMP_ID = filter.EMPLOYEE_ID And f.STATUS IsNot Nothing)
                        From ar In Context.TR_ASSESSMENT_RESULT.Where(Function(f) f.TR_PROGRAM_ID = pro_emp.TR_PROGRAM_ID AndAlso f.EMPLOYEE_ID = pro_emp.EMP_ID).DefaultIfEmpty
                        From Request In Context.TR_REQUEST.Where(Function(f) f.ID = Program.TR_REQUEST_ID).DefaultIfEmpty
                        From Plan In Context.TR_PLAN.Where(Function(f) f.ID = Request.TR_PLAN_ID).DefaultIfEmpty
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = Program.ORG_ID).DefaultIfEmpty
                        From cou In Context.TR_COURSE.Where(Function(f) f.ID = Program.TR_COURSE_ID).DefaultIfEmpty
                        From group In Context.TR_PROGRAM_GROUP.Where(Function(f) f.ID = cou.TR_PROGRAM_GROUP).DefaultIfEmpty
                        From field In Context.OT_OTHER_LIST.Where(Function(f) f.ID = cou.TR_TRAIN_FIELD).DefaultIfEmpty
                        From form In Context.OT_OTHER_LIST.Where(Function(f) f.ID = Program.TRAIN_FORM_ID).DefaultIfEmpty
                        From proned In Context.OT_OTHER_LIST.Where(Function(f) f.ID = Program.PROPERTIES_NEED_ID).DefaultIfEmpty
                        From duruni In Context.OT_OTHER_LIST.Where(Function(f) f.ID = Program.TR_DURATION_UNIT_ID).DefaultIfEmpty
                        From type In Context.OT_OTHER_LIST.Where(Function(f) f.ID = Program.DURATION_STUDY_ID).DefaultIfEmpty
                        From durstu In Context.OT_OTHER_LIST.Where(Function(f) f.ID = Program.DURATION_STUDY_ID).DefaultIfEmpty
                        From lang In Context.OT_OTHER_LIST.Where(Function(f) f.ID = Program.TR_LANGUAGE_ID).DefaultIfEmpty
                        From tc In Context.OT_OTHER_LIST.Where(Function(f) f.ID = Program.PROPERTIES_NEED_ID).DefaultIfEmpty
                        From unit In Context.TR_UNIT.Where(Function(f) f.ID = Program.TR_UNIT_ID).DefaultIfEmpty
                        From cure In Context.OT_OTHER_LIST.Where(Function(f) f.ID = Request.TR_CURRENCY_ID).DefaultIfEmpty
                        Select New ProgramDTO With {.ID = Program.ID,
                                                    .TR_REQUEST_ID = Program.TR_REQUEST_ID,
                                                    .TR_COURSE_ID = Program.TR_COURSE_ID,
                                                    .TR_COURSE_NAME = cou.NAME,
                                                    .YEAR = Program.YEAR,
                                                    .ORG_ID = Program.ORG_ID,
                                                    .ORG_NAME = org.NAME_VN,
                                                    .NAME = Program.NAME,
                                                    .PROGRAM_GROUP = group.NAME,
                                                    .TRAIN_FIELD = field.NAME_VN,
                                                    .TRAIN_FORM_ID = Program.TRAIN_FORM_ID,
                                                    .TRAIN_FORM_NAME = form.NAME_VN,
                                                    .PROPERTIES_NEED_ID = Program.PROPERTIES_NEED_ID,
                                                    .PROPERTIES_NEED_NAME = proned.NAME_VN,
                                                    .DURATION = Program.DURATION,
                                                    .TR_DURATION_UNIT_ID = Program.TR_DURATION_UNIT_ID,
                                                    .TR_DURATION_UNIT_NAME = duruni.NAME_VN,
                                                    .START_DATE = Program.START_DATE,
                                                    .END_DATE = Program.END_DATE,
                                                    .DURATION_STUDY_ID = Program.DURATION_STUDY_ID,
                                                    .DURATION_STUDY_NAME = durstu.NAME_VN,
                                                    .DURATION_HC = Program.DURATION_HC,
                                                    .DURATION_OT = Program.DURATION_OT,
                                                    .IS_REIMBURSE = Program.IS_REIMBURSE,
                                                    .IS_RETEST = Program.IS_RETEST,
                                                    .TR_CURRENCY_NAME = cure.NAME_VN,
                                                    .PLAN_STUDENT_NUMBER = Plan.STUDENT_NUMBER,
                                                    .PLAN_COST_TOTAL_US = Plan.COST_TOTAL_USD,
                                                    .PLAN_COST_TOTAL = Plan.COST_TOTAL,
                                                    .STUDENT_NUMBER = Program.STUDENT_NUMBER,
                                                    .COST_TOTAL_US = Program.COST_TOTAL_US,
                                                    .COST_STUDENT_US = Program.COST_STUDENT_US,
                                                    .COST_TOTAL = Program.COST_TOTAL,
                                                    .COST_STUDENT = Program.COST_STUDENT,
                                                    .Departments_NAME = Program.DEPARTMENTS,
                                                    .Titles_NAME = Program.TITLES,
                                                    .TR_LANGUAGE_ID = Program.TR_LANGUAGE_ID,
                                                    .TR_LANGUAGE_NAME = lang.NAME_VN,
                                                    .TR_UNIT_ID = Program.TR_UNIT_ID,
                                                    .TR_UNIT_NAME = unit.NAME,
                                                    .Centers_NAME = Program.CENTERS,
                                                    .Lectures_NAME = Program.LECTURES,
                                                    .CONTENT = Program.CONTENT,
                                                    .TARGET_TRAIN = Program.TARGET_TRAIN,
                                                    .VENUE = Program.VENUE,
                                                    .REMARK = Program.REMARK,
                                                    .CREATED_DATE = Program.CREATED_DATE,
                                                    .TR_PROGRAM_CODE = Program.TR_PROGRAM_CODE,
                                                    .TR_TYPE_NAME = type.NAME_VN,
                                                    .ASSESMENT_ID = ar.ID,
                                                    .STATUS_ASSESMENT = If(Not ar.EMPLOYEE_ID.HasValue, "Chưa đánh giá", "Đã đánh giá"),
                                                    .IS_LOCK = ar.IS_LOCK,
                                                    .IS_LOCK_TEXT = If(ar.IS_LOCK.Value = -1, "X", "")}

            Dim lst = query
            lst = lst.OrderBy("START_DATE DESC")
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList()
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetProgramEvaluatePortal")
            Throw ex
        End Try
    End Function

    Public Function GetPrograms_Portal(ByVal filter As ProgramDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of ProgramDTO)
        Try

            Dim query = From Program In Context.TR_PROGRAM
                        From cou In Context.TR_COURSE.Where(Function(f) f.ID = Program.TR_COURSE_ID).DefaultIfEmpty
                        From form In Context.OT_OTHER_LIST.Where(Function(f) f.ID = Program.TRAIN_FORM_ID).DefaultIfEmpty
                        From pro_emp In Context.TR_PROGRAM_EMP.Where(Function(f) f.TR_PROGRAM_ID = Program.ID And f.EMP_ID = filter.EMPLOYEE_ID).DefaultIfEmpty
                        From public_status In Context.OT_OTHER_LIST.Where(Function(f) f.ID = Program.PUBLIC_STATUS).DefaultIfEmpty
                        From type In Context.OT_OTHER_LIST.Where(Function(f) f.ID = Program.TR_TYPE_ID).DefaultIfEmpty
                        Where Program.IS_PUBLIC = -1 And public_status.ID = 788614 And (Program.PORTAL_REGIST_TO Is Nothing Or Program.PORTAL_REGIST_TO <= Date.Now)
                        Select New ProgramDTO With {.ID = Program.ID,
                                                    .TR_PROGRAM_CODE = Program.TR_PROGRAM_CODE,
                                                    .TR_COURSE_CODE = cou.CODE,
                                                    .TR_COURSE_NAME = cou.NAME,
                                                    .TRAIN_FORM_NAME = form.NAME_VN,
                                                    .START_DATE = Program.START_DATE,
                                                    .END_DATE = Program.END_DATE,
                                                    .STUDENT_NUMBER = Program.STUDENT_NUMBER,
                                                    .Centers_NAME = Program.CENTERS,
                                                    .CONTENT = Program.CONTENT,
                                                    .VENUE = Program.VENUE,
                                                    .REMARK = Program.REMARK,
                                                    .CREATED_DATE = Program.CREATED_DATE,
                                                    .STATUS = pro_emp.STATUS,
                                                    .PORTAL_REGIST_FROM = Program.PORTAL_REGIST_FROM,
                                                    .PORTAL_REGIST_TO = Program.PORTAL_REGIST_TO,
                                                    .STATUS_NAME = If(pro_emp.STATUS Is Nothing, Nothing, If(pro_emp.STATUS = 0, "Đã thêm bởi phòng Đào tạo", "Đã đăng ký")),
                                                    .STUDENT_NUMBER_JOIN = (From p In Context.TR_PROGRAM_EMP
                                                                            Where p.TR_PROGRAM_ID = Program.ID And (p.STATUS = 0 Or p.STATUS = 1)).Count,
                                                    .PROGRAM_EMP_ID = pro_emp.ID,
                                                    .TR_TYPE_NAME = type.NAME_VN,
                                                    .TARGET_TRAIN = Program.TARGET_TRAIN}

            Dim lst = query
            If filter.START_DATE.HasValue Then
                lst = lst.Where(Function(p) p.START_DATE >= filter.START_DATE)
            End If
            If filter.END_DATE.HasValue Then
                lst = lst.Where(Function(p) p.END_DATE <= filter.END_DATE)
            End If

            If filter.PORTAL_REGIST_FROM.HasValue Then
                lst = lst.Where(Function(p) p.PORTAL_REGIST_FROM = filter.PORTAL_REGIST_FROM)
            End If

            If filter.PORTAL_REGIST_TO.HasValue Then
                lst = lst.Where(Function(p) p.PORTAL_REGIST_TO = filter.PORTAL_REGIST_TO)
            End If

            If filter.STATUS_NAME IsNot Nothing Then
                lst = lst.Where(Function(p) p.STATUS_NAME.Contains(filter.STATUS_NAME))
            End If
            If filter.TR_COURSE_NAME IsNot Nothing Then
                lst = lst.Where(Function(p) p.TR_COURSE_NAME.Contains(filter.TR_COURSE_NAME))
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Dim rs = lst.ToList

            For Each item As ProgramDTO In rs
                Dim centers As String = ""
                Dim lstCenter = (From p In Context.TR_PROGRAM_CENTER
                                 From c In Context.TR_CENTER.Where(Function(f) f.ID = p.TR_CENTER_ID)
                                 Where p.TR_PROGRAM_ID = item.ID
                                 Select New ProgramCenterDTO With {.ID = p.TR_CENTER_ID,
                                                                 .NAME_EN = c.NAME_EN,
                                                                 .NAME_VN = c.NAME_VN}).ToList
                For Each itemCenter As ProgramCenterDTO In lstCenter
                    centers = itemCenter.NAME_VN + "; "
                Next
                item.Centers_NAME = centers
            Next

            Return rs
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetPrograms_Portal")
            Throw ex
        End Try
    End Function

    Public Function InsertRegisterTraining_Portal(ByVal obj As ProgramEmpDTO, ByVal log As UserLog) As Boolean
        Dim objData As New TR_PROGRAM_EMP
        Try
            objData.ID = Utilities.GetNextSequence(Context, Context.TR_PROGRAM_EMP.EntitySet.Name)
            objData.TR_PROGRAM_ID = obj.TR_PROGRAM_ID
            objData.EMP_ID = obj.EMP_ID
            objData.EMP_TYPE = -1
            objData.STATUS = obj.STATUS
            objData.IS_PORTAL_REGIST = obj.IS_PORTAL_REGIST
            Context.TR_PROGRAM_EMP.AddObject(objData)
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".InsertRegisterTraining_Portal")
            Throw ex
        End Try
    End Function

    Public Function ModifyRegisterTraining_Portal(ByVal obj As ProgramEmpDTO, ByVal log As UserLog) As Boolean
        Dim objData As New TR_PROGRAM_EMP With {.ID = obj.ID}
        Try
            Context.TR_PROGRAM_EMP.Attach(objData)
            objData.STATUS = obj.STATUS
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".ModifyRegisterTraining_Portal")
            Throw ex
        End Try
    End Function

    Public Function GetEmpByTitleAndOrg(ByVal titleId As Decimal, ByVal orgId As Decimal) As List(Of RecordEmployeeDTO)
        Try
            Dim lstEmp As List(Of RecordEmployeeDTO) = (From pro_emp In Context.TR_PROGRAM_EMP
                                                        From employee In Context.HU_EMPLOYEE.Where(Function(f) f.ID = pro_emp.EMP_ID)
                                                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = employee.ORG_ID).DefaultIfEmpty
                                                        From title In Context.HU_TITLE.Where(Function(f) f.ID = employee.TITLE_ID).DefaultIfEmpty
                                                        Where employee.TITLE_ID = titleId
                                                        Select New RecordEmployeeDTO With {
                                                             .ID = employee.ID,
                                                             .EMPLOYEE_CODE = employee.EMPLOYEE_CODE,
                                                             .FULLNAME_VN = employee.FULLNAME_VN,
                                                             .ORG_NAME = org.NAME_VN,
                                                             .TITLE_NAME_VN = title.NAME_VN,
                                                             .ORG_ID = employee.ORG_ID,
                                                             .TITLE_ID = employee.TITLE_ID,
                                                             .JOIN_DATE = employee.JOIN_DATE,
                                                             .REGISTER_TRAINING_STATUS = If(pro_emp.STATUS Is Nothing, Nothing, If(pro_emp.STATUS = 0, "Đã có trong danh sách khóa học", "Đã đăng ký"))}).ToList

            If orgId = 0 Then
            Else
                lstEmp = (From p In lstEmp Where p.ORG_ID = orgId)
            End If
            Return lstEmp
        Catch ex As Exception

        End Try

    End Function
    Public Function GetProgramById(ByVal Id As Decimal) As ProgramDTO
        Try
            Dim str = ""
            Dim objProgram As ProgramDTO = (From Program In Context.TR_PROGRAM
                                            From Request In Context.TR_REQUEST.Where(Function(f) f.ID = Program.TR_REQUEST_ID).DefaultIfEmpty
                                            From Plan In Context.TR_PLAN.Where(Function(f) f.ID = Request.TR_PLAN_ID).DefaultIfEmpty
                                            From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = Program.ORG_ID).DefaultIfEmpty
                                            From cou In Context.TR_COURSE.Where(Function(f) f.ID = Program.TR_COURSE_ID).DefaultIfEmpty
                                            From group In Context.TR_PROGRAM_GROUP.Where(Function(f) f.ID = cou.TR_PROGRAM_GROUP).DefaultIfEmpty
                                            From field In Context.OT_OTHER_LIST.Where(Function(f) f.ID = cou.TR_TRAIN_FIELD).DefaultIfEmpty
                                            From form In Context.OT_OTHER_LIST.Where(Function(f) f.ID = Program.TRAIN_FORM_ID).DefaultIfEmpty
                                            From proned In Context.OT_OTHER_LIST.Where(Function(f) f.ID = Program.PROPERTIES_NEED_ID).DefaultIfEmpty
                                            From duruni In Context.OT_OTHER_LIST.Where(Function(f) f.ID = Program.TR_DURATION_UNIT_ID).DefaultIfEmpty
                                            From durstu In Context.OT_OTHER_LIST.Where(Function(f) f.ID = Program.DURATION_STUDY_ID).DefaultIfEmpty
                                            From lang In Context.OT_OTHER_LIST.Where(Function(f) f.ID = Program.TR_LANGUAGE_ID).DefaultIfEmpty
                                            From unit In Context.TR_UNIT.Where(Function(f) f.ID = Program.TR_UNIT_ID).DefaultIfEmpty
                                            From cure In Context.OT_OTHER_LIST.Where(Function(f) f.ID = Request.TR_CURRENCY_ID).DefaultIfEmpty
                                            From tr_type In Context.OT_OTHER_LIST.Where(Function(f) f.ID = Program.TR_TYPE_ID).DefaultIfEmpty
                                            From public_status In Context.OT_OTHER_LIST.Where(Function(f) f.ID = Program.PUBLIC_STATUS).DefaultIfEmpty
                                            From ass1 In Context.HU_EMPLOYEE.Where(Function(f) f.ID = Program.ASS_EMP1_ID).DefaultIfEmpty
                                            From ass2 In Context.HU_EMPLOYEE.Where(Function(f) f.ID = Program.ASS_EMP2_ID).DefaultIfEmpty
                                            From ass3 In Context.HU_EMPLOYEE.Where(Function(f) f.ID = Program.ASS_EMP3_ID).DefaultIfEmpty
                                            Where Program.ID = Id
                                            Select New ProgramDTO With {.ID = Program.ID,
                                                                        .TR_REQUEST_ID = Program.TR_REQUEST_ID,
                                                                        .TR_COURSE_ID = Program.TR_COURSE_ID,
                                                                        .TR_COURSE_NAME = cou.NAME,
                                                                        .YEAR = Program.YEAR,
                                                                        .ORG_ID = Program.ORG_ID,
                                                                        .ORG_NAME = org.NAME_VN,
                                                                        .NAME = Program.NAME,
                                                                        .PROGRAM_GROUP = group.NAME,
                                                                        .TRAIN_FIELD = field.NAME_VN,
                                                                        .TRAIN_FORM_ID = Program.TRAIN_FORM_ID,
                                                                        .TRAIN_FORM_NAME = form.NAME_VN,
                                                                        .PROPERTIES_NEED_ID = Program.PROPERTIES_NEED_ID,
                                                                        .PROPERTIES_NEED_NAME = proned.NAME_VN,
                                                                        .DURATION = Program.DURATION,
                                                                        .TR_DURATION_UNIT_ID = Program.TR_DURATION_UNIT_ID,
                                                                        .TR_DURATION_UNIT_NAME = duruni.NAME_VN,
                                                                        .START_DATE = Program.START_DATE,
                                                                        .END_DATE = Program.END_DATE,
                                                                        .DURATION_STUDY_ID = Program.DURATION_STUDY_ID,
                                                                        .DURATION_STUDY_NAME = durstu.NAME_VN,
                                                                        .DURATION_HC = Program.DURATION_HC,
                                                                        .DURATION_OT = Program.DURATION_OT,
                                                                        .IS_REIMBURSE = Program.IS_REIMBURSE,
                                                                        .IS_RETEST = Program.IS_RETEST,
                                                                        .PLAN_STUDENT_NUMBER = Plan.STUDENT_NUMBER,
                                                                        .PLAN_COST_TOTAL_US = Plan.COST_TOTAL_USD,
                                                                        .PLAN_COST_TOTAL = Plan.COST_TOTAL,
                                                                        .STUDENT_NUMBER = Program.STUDENT_NUMBER,
                                                                        .COST_TOTAL_US = Program.COST_TOTAL_US,
                                                                        .COST_STUDENT_US = Program.COST_STUDENT_US,
                                                                        .COST_TOTAL = Program.COST_TOTAL,
                                                                        .COST_STUDENT = Program.COST_STUDENT,
                                                                        .Departments_NAME = Program.DEPARTMENTS,
                                                                        .Titles_NAME = Program.TITLES,
                                                                        .TR_LANGUAGE_ID = Program.TR_LANGUAGE_ID,
                                                                        .TR_LANGUAGE_NAME = lang.NAME_VN,
                                                                        .TR_UNIT_ID = Program.TR_UNIT_ID,
                                                                        .TR_UNIT_NAME = unit.NAME,
                                                                        .Centers_NAME = Program.CENTERS,
                                                                        .Lectures_NAME = Program.LECTURES,
                                                                        .CONTENT = Program.CONTENT,
                                                                        .TARGET_TRAIN = Program.TARGET_TRAIN,
                                                                        .VENUE = Program.VENUE,
                                                                        .REMARK = Program.REMARK,
                                                                        .CREATED_DATE = Program.CREATED_DATE,
                                                                        .TR_PROGRAM_CODE = Program.TR_PROGRAM_CODE,
                                                                        .TR_TYPE_ID = Program.TR_TYPE_ID,
                                                                        .TR_TYPE_NAME = tr_type.NAME_VN,
                                                                        .CERTIFICATE = Program.CERTIFICATE,
                                                                        .IS_PUBLIC = Program.IS_PUBLIC,
                                                                        .PUBLIC_STATUS_ID = Program.PUBLIC_STATUS,
                                                                        .PUBLIC_STATUS_NAME = public_status.NAME_VN,
                                                                        .TR_TRAIN_FORM_ID = Program.TRAIN_FORM_ID,
                                                                        .EXPECT_CLASS = Program.EXPECT_CLASS,
                                                                        .TR_CURRENCY_ID = Program.TR_CURRENCY_ID,
                                                                        .TR_CURRENCY_NAME = cure.NAME_VN,
                                                                        .TR_COMMIT = Program.TR_COMMIT,
                                                                        .TR_AFTER_TRAIN = Program.TR_AFTER_TRAIN,
                                                                        .CERTIFICATE_NAME = Program.CERTIFICATE_NAME,
                                                                        .IS_PLAN = Program.IS_PLAN,
                                                                        .PORTAL_REGIST_FROM = Program.PORTAL_REGIST_FROM,
                                                                        .PORTAL_REGIST_TO = Program.PORTAL_REGIST_TO,
                                                                        .DAY_REVIEW_1 = Program.DAY_REVIEW_1,
                                                                        .DAY_REVIEW_2 = Program.DAY_REVIEW_2,
                                                                        .DAY_REVIEW_3 = Program.DAY_REVIEW_3,
                                                                        .ASS_EMP1_ID = Program.ASS_EMP1_ID,
                                                                        .ASS_EMP2_ID = Program.ASS_EMP2_ID,
                                                                        .ASS_EMP3_ID = Program.ASS_EMP3_ID,
                                                                        .ASS_EMP1_NAME = If(ass1.EMPLOYEE_CODE <> "", ass1.EMPLOYEE_CODE + " - " + ass1.FULLNAME_VN, str),
                                                                        .ASS_EMP2_NAME = If(ass2.EMPLOYEE_CODE <> "", ass2.EMPLOYEE_CODE + " - " + ass2.FULLNAME_VN, str),
                                                                        .ASS_EMP3_NAME = If(ass3.EMPLOYEE_CODE <> "", ass3.EMPLOYEE_CODE + " - " + ass3.FULLNAME_VN, str),
                                                .ASS_DATE = Program.ASS_DATE}).FirstOrDefault

            objProgram.Costs = (From p In Context.TR_PROGRAM_COST
                                From org In Context.HU_ORGANIZATION.Where(Function(o) o.ID = p.ORG_ID).DefaultIfEmpty
                                Where p.TR_PROGRAM_ID = Id
                                Select New ProgramCostDTO With {.ID = p.ID,
                                                                .TR_PROGRAM_ID = p.TR_PROGRAM_ID,
                                                                .ORG_ID = p.ORG_ID,
                                                                .ORG_NAME = org.NAME_VN,
                                                                .COST_COMPANY = p.COST_COMPANY}).ToList

            objProgram.Units = (From p In Context.TR_PROGRAM_UNIT
                                From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID)
                                Where p.TR_PROGRAM_ID = Id
                                Select New ProgramOrgDTO With {.ID = p.ORG_ID,
                                                               .NAME = o.NAME_VN}).ToList

            'objProgram.Titles = (From p In Context.TR_PROGRAM_TITLE
            '                     From t In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID)
            '                     Where p.TR_PROGRAM_ID = Id
            '                     Select New ProgramTitleDTO With {.ID = p.TITLE_ID,
            '                                                      .NAME = t.NAME_VN}).ToList
            'objProgram.GroupTitle = (From p In Context.TR_PROGRAM_TITLE
            '                     From t In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TITLE_GROUP_ID)
            '                     Where p.TR_PROGRAM_ID = Id
            '                     Select New ProgramTitleDTO With {.ID = p.TITLE_GROUP_ID,
            '                                                      .NAME = t.NAME_VN}).ToList

            objProgram.WIs = (From p In Context.TR_PROGRAM_TITLE
                              From t In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID)
                              From ot In Context.OT_OTHER_LIST.Where(Function(f) f.ID = t.WORK_INVOLVE_ID)
                              Where p.TR_PROGRAM_ID = Id
                              Select New ProgramTitleDTO With {.ID = p.TITLE_ID,
                                                               .NAME = t.NAME_VN}).ToList

            objProgram.Centers = (From p In Context.TR_PROGRAM_CENTER
                                  From c In Context.TR_CENTER.Where(Function(f) f.ID = p.TR_CENTER_ID)
                                  Where p.TR_PROGRAM_ID = Id
                                  Select New ProgramCenterDTO With {.ID = p.TR_CENTER_ID,
                                                                    .NAME_EN = c.NAME_EN,
                                                                    .NAME_VN = c.NAME_VN}).ToList

            objProgram.Lectures = (From p In Context.TR_PROGRAM_LECTURE
                                   From c In Context.TR_LECTURE.Where(Function(f) f.ID = p.TR_LECTURE_ID)
                                   From emp In Context.HU_EMPLOYEE.Where(Function(f) f.ID = c.LECTURE_ID).DefaultIfEmpty
                                   Where p.TR_PROGRAM_ID = Id
                                   Select New ProgramLectureDTO With {.ID = c.ID,
                                                                      .NAME = If(c.LECTURE_ID IsNot Nothing,
                                                                                 emp.EMPLOYEE_CODE & " - " & emp.FULLNAME_VN,
                                                                                 c.CODE & " - " & c.NAME)}).ToList
            objProgram.Employees = (From pro_emp In Context.TR_PROGRAM_EMP
                                    From employee In Context.HU_EMPLOYEE.Where(Function(f) f.ID = pro_emp.EMP_ID)
                                    From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = employee.ORG_ID).DefaultIfEmpty
                                    From title In Context.HU_TITLE.Where(Function(f) f.ID = employee.TITLE_ID).DefaultIfEmpty
                                    From title_group In Context.OT_OTHER_LIST.Where(Function(f) f.ID = title.TITLE_GROUP_ID).DefaultIfEmpty
                                    Where pro_emp.TR_PROGRAM_ID = Id
                                    Select New RecordEmployeeDTO With {
                                         .ID = employee.ID,
                                         .EMPLOYEE_CODE = employee.EMPLOYEE_CODE,
                                         .FULLNAME_VN = employee.FULLNAME_VN,
                                         .ORG_NAME = org.NAME_VN,
                                         .TITLE_NAME_VN = title.NAME_VN,
                                         .ORG_ID = employee.ORG_ID,
                                         .TITLE_ID = employee.TITLE_ID,
                                         .JOIN_DATE = employee.JOIN_DATE,
                                         .REGISTER_TRAINING_STATUS = If(pro_emp.STATUS Is Nothing, Nothing, If(pro_emp.STATUS = 0, "Đã có trong danh sách khóa học", "Đã đăng ký")),
                                         .TITLE_GROUP_ID = title.TITLE_GROUP_ID,
                                         .TITLE_GROUP_NAME = title_group.NAME_VN,
                                         .EMP_TYPE = pro_emp.EMP_TYPE,
                                         .STATUS_ID = pro_emp.STATUS,
                                         .EMP_TYPE_NAME = If(pro_emp.EMP_TYPE = 0, "Theo nhóm chức danh", "Ngoại lệ")}).ToList
            Return objProgram
            Return Nothing
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetProgramById")
            Throw ex
        End Try
    End Function

    Public Function InsertProgram(ByVal Program As ProgramDTO, ByVal log As UserLog, ByRef gID As Decimal,
                                 Optional ByVal isInsert As Boolean = True) As Boolean
        Dim objProgram As New TR_PROGRAM
        Try
            With objProgram
                .ID = Utilities.GetNextSequence(Context, Context.TR_PROGRAM.EntitySet.Name)
                .IS_PLAN = Program.IS_PLAN
                .TR_COURSE_ID = Program.TR_COURSE_ID
                .YEAR = Program.YEAR
                If Program.ORG_ID IsNot Nothing And Program.ORG_ID <> 0 Then
                    .ORG_ID = Program.ORG_ID
                Else
                    .ORG_ID = Nothing
                End If
                .TR_PROGRAM_CODE = Program.TR_PROGRAM_CODE
                .PROPERTIES_NEED_ID = Program.PROPERTIES_NEED_ID
                .TRAIN_FORM_ID = Program.TRAIN_FORM_ID
                .TR_TRAIN_FIELD = Program.TR_TRAIN_FIELD_ID
                .CONTENT = Program.CONTENT
                .TARGET_TRAIN = Program.TARGET_TRAIN
                .VENUE = Program.VENUE
                .REMARK = Program.REMARK
                .START_DATE = Program.START_DATE
                .END_DATE = Program.END_DATE
                .STUDENT_NUMBER = Program.STUDENT_NUMBER
                .EXPECT_CLASS = Program.EXPECT_CLASS
                .COST_TOTAL = Program.COST_TOTAL
                .TR_CURRENCY_ID = Program.TR_CURRENCY_ID
                .FILE_ATTACH = Program.FILE_ATTACH
                .UPLOAD_FILE = Program.UPLOAD_FILE
                .TR_AFTER_TRAIN = Program.TR_AFTER_TRAIN
                .CERTIFICATE = Program.CERTIFICATE
                .CERTIFICATE_NAME = Program.CERTIFICATE_NAME
                .TR_COMMIT = Program.TR_COMMIT
                .TR_TYPE_ID = Program.TR_TYPE_ID
                .DAY_REVIEW_1 = Program.DAY_REVIEW_1
                .DAY_REVIEW_2 = Program.DAY_REVIEW_2
                .DAY_REVIEW_3 = Program.DAY_REVIEW_3
                .ASS_EMP1_ID = Program.ASS_EMP1_ID
                .ASS_EMP2_ID = Program.ASS_EMP2_ID
                .ASS_EMP3_ID = Program.ASS_EMP3_ID
                .IS_PUBLIC = Program.IS_PUBLIC
                .PUBLIC_STATUS = Program.PUBLIC_STATUS_ID
                .PORTAL_REGIST_FROM = Program.PORTAL_REGIST_FROM
                .PORTAL_REGIST_TO = Program.PORTAL_REGIST_TO
                .ASS_DATE = Program.ASS_DATE
            End With
            gID = objProgram.ID
            Context.TR_PROGRAM.AddObject(objProgram)


            '.Costs = lstOrgCost
            '.Units = (From item In lstPartDepts.Items Select New ProgramOrgDTO With {.ID = item.Value}).ToList()
            '.TITLES = (From item In lstPositions.CheckedItems Select New ProgramTitleDTO With {.ID = item.Value}).ToList()
            '.CENTERS = (From item In lstCenter.CheckedItems Select New ProgramCenterDTO With {.ID = item.Value}).ToList()
            '.LECTURES = (From item In lstLecture.CheckedItems Select New ProgramLectureDTO With {.ID = item.Value}).ToList()

            If Program.Costs IsNot Nothing Then
                For Each Cost As ProgramCostDTO In Program.Costs
                    Context.TR_PROGRAM_COST.AddObject(New TR_PROGRAM_COST With {
                                                   .ID = Utilities.GetNextSequence(Context, Context.TR_PROGRAM_COST.EntitySet.Name),
                                                   .TR_PROGRAM_ID = objProgram.ID,
                                                   .ORG_ID = Cost.ORG_ID,
                                                   .COST_COMPANY = Cost.COST_COMPANY})
                Next
            End If

            If Program.Units IsNot Nothing Then
                For Each unit In Program.Units
                    Context.TR_PROGRAM_UNIT.AddObject(New TR_PROGRAM_UNIT With {
                                                   .ID = Utilities.GetNextSequence(Context, Context.TR_PROGRAM_UNIT.EntitySet.Name),
                                                   .TR_PROGRAM_ID = objProgram.ID,
                                                   .ORG_ID = unit.ID})
                Next
            End If

            'If Program.Titles IsNot Nothing Then
            '    For Each title In Program.Titles
            '        Context.TR_PROGRAM_TITLE.AddObject(New TR_PROGRAM_TITLE With {
            '                                        .ID = Utilities.GetNextSequence(Context, Context.TR_PROGRAM_TITLE.EntitySet.Name),
            '                                        .TR_PROGRAM_ID = objProgram.ID,
            '                                        .TITLE_ID = title.ID})
            '    Next
            'End If

            'If Program.GroupTitle IsNot Nothing Then
            '    For Each title In Program.GroupTitle
            '        Context.TR_PROGRAM_TITLE.AddObject(New TR_PROGRAM_TITLE With {
            '                                        .ID = Utilities.GetNextSequence(Context, Context.TR_PROGRAM_TITLE.EntitySet.Name),
            '                                        .TR_PROGRAM_ID = objProgram.ID,
            '                                        .TITLE_GROUP_ID = title.ID})
            '    Next
            'End If

            If Program.Centers IsNot Nothing Then
                For Each center In Program.Centers
                    Context.TR_PROGRAM_CENTER.AddObject(New TR_PROGRAM_CENTER With {
                                                     .ID = Utilities.GetNextSequence(Context, Context.TR_PROGRAM_CENTER.EntitySet.Name),
                                                     .TR_PROGRAM_ID = objProgram.ID,
                                                     .TR_CENTER_ID = center.ID})
                Next
            End If

            If Program.Lectures IsNot Nothing Then
                For Each lec In Program.Lectures
                    Context.TR_PROGRAM_LECTURE.AddObject(New TR_PROGRAM_LECTURE With {
                                                     .ID = Utilities.GetNextSequence(Context, Context.TR_PROGRAM_LECTURE.EntitySet.Name),
                                                     .TR_PROGRAM_ID = objProgram.ID,
                                                     .TR_LECTURE_ID = lec.ID})
                Next
            End If

            If Program.Employees IsNot Nothing Then
                For Each emp In Program.Employees
                    Context.TR_PROGRAM_EMP.AddObject(New TR_PROGRAM_EMP With {
                                                     .ID = Utilities.GetNextSequence(Context, Context.TR_PROGRAM_EMP.EntitySet.Name),
                                                     .TR_PROGRAM_ID = objProgram.ID,
                                                     .EMP_ID = emp.ID,
                                                     .EMP_TYPE = emp.EMP_TYPE,
                                                     .STATUS = 0})
                Next
            End If
            If isInsert Then
                Context.SaveChanges(log)
            End If
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".InsertProgram")
            Throw ex
        End Try
    End Function

    Public Function ModifyProgram(ByVal Program As ProgramDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objProgram As New TR_PROGRAM With {.ID = Program.ID}
        Try
            Context.TR_PROGRAM.Attach(objProgram)
            With objProgram
                ''.TR_REQUEST_ID = Program.TR_REQUEST_ID
                .IS_PLAN = Program.IS_PLAN
                .TR_COURSE_ID = Program.TR_COURSE_ID
                .YEAR = Program.YEAR
                If Program.ORG_ID IsNot Nothing And Program.ORG_ID <> 0 Then
                    .ORG_ID = Program.ORG_ID
                Else
                    .ORG_ID = Nothing
                End If
                .TR_PROGRAM_CODE = Program.TR_PROGRAM_CODE
                .PROPERTIES_NEED_ID = Program.PROPERTIES_NEED_ID
                .TRAIN_FORM_ID = Program.TRAIN_FORM_ID
                .TR_TRAIN_FIELD = Program.TR_TRAIN_FIELD_ID
                .CONTENT = Program.CONTENT
                .TARGET_TRAIN = Program.TARGET_TRAIN
                .VENUE = Program.VENUE
                .REMARK = Program.REMARK
                .START_DATE = Program.START_DATE
                .END_DATE = Program.END_DATE
                .STUDENT_NUMBER = Program.STUDENT_NUMBER
                .EXPECT_CLASS = Program.EXPECT_CLASS
                .COST_TOTAL = Program.COST_TOTAL
                .TR_CURRENCY_ID = Program.TR_CURRENCY_ID
                .FILE_ATTACH = Program.FILE_ATTACH
                .UPLOAD_FILE = Program.UPLOAD_FILE
                .TR_AFTER_TRAIN = Program.TR_AFTER_TRAIN
                .CERTIFICATE = Program.CERTIFICATE
                .CERTIFICATE_NAME = Program.CERTIFICATE_NAME
                .TR_COMMIT = Program.TR_COMMIT
                .TR_TYPE_ID = Program.TR_TYPE_ID
                .DAY_REVIEW_1 = Program.DAY_REVIEW_1
                .DAY_REVIEW_2 = Program.DAY_REVIEW_2
                .DAY_REVIEW_3 = Program.DAY_REVIEW_3
                .ASS_EMP1_ID = Program.ASS_EMP1_ID
                .ASS_EMP2_ID = Program.ASS_EMP2_ID
                .ASS_EMP3_ID = Program.ASS_EMP3_ID
                .IS_PUBLIC = Program.IS_PUBLIC
                .PUBLIC_STATUS = Program.PUBLIC_STATUS_ID
                .PORTAL_REGIST_FROM = Program.PORTAL_REGIST_FROM
                .PORTAL_REGIST_TO = Program.PORTAL_REGIST_TO
                .ASS_DATE = Program.ASS_DATE
            End With

            Dim oldCosts = From i In Context.TR_PROGRAM_COST Where i.TR_PROGRAM_ID = Program.ID
            For Each cost In oldCosts
                Context.TR_PROGRAM_COST.DeleteObject(cost)
            Next
            If Program.Costs IsNot Nothing Then
                For Each Cost As ProgramCostDTO In Program.Costs
                    Context.TR_PROGRAM_COST.AddObject(New TR_PROGRAM_COST With {
                                                   .ID = Utilities.GetNextSequence(Context, Context.TR_PROGRAM_COST.EntitySet.Name),
                                                   .TR_PROGRAM_ID = objProgram.ID,
                                                   .ORG_ID = Cost.ORG_ID,
                                                   .COST_COMPANY = Cost.COST_COMPANY})
                Next
            End If

            Dim oldUnits = From i In Context.TR_PROGRAM_UNIT Where i.TR_PROGRAM_ID = Program.ID
            For Each unit In oldUnits
                Context.TR_PROGRAM_UNIT.DeleteObject(unit)
            Next
            If Program.Units IsNot Nothing Then
                For Each unit In Program.Units
                    Context.TR_PROGRAM_UNIT.AddObject(New TR_PROGRAM_UNIT With {
                                                      .ID = Utilities.GetNextSequence(Context, Context.TR_PROGRAM_UNIT.EntitySet.Name),
                                                      .TR_PROGRAM_ID = objProgram.ID,
                                                      .ORG_ID = unit.ID})
                Next
            End If

            'Dim oldTItles = From i In Context.TR_PROGRAM_TITLE Where i.TR_PROGRAM_ID = Program.ID
            'For Each item In oldTItles
            '    Context.TR_PROGRAM_TITLE.DeleteObject(item)
            'Next
            'If Program.Titles IsNot Nothing Then
            '    For Each title In Program.Titles
            '        Context.TR_PROGRAM_TITLE.AddObject(New TR_PROGRAM_TITLE With {
            '                                           .ID = Utilities.GetNextSequence(Context, Context.TR_PROGRAM_TITLE.EntitySet.Name),
            '                                           .TR_PROGRAM_ID = objProgram.ID,
            '                                           .TITLE_ID = title.ID})
            '    Next
            'End If

            Dim oldCenters = From i In Context.TR_PROGRAM_CENTER Where i.TR_PROGRAM_ID = Program.ID
            For Each item In oldCenters
                Context.TR_PROGRAM_CENTER.DeleteObject(item)
            Next
            If Program.Centers IsNot Nothing Then
                For Each center In Program.Centers
                    Context.TR_PROGRAM_CENTER.AddObject(New TR_PROGRAM_CENTER With
                                                        {.ID = Utilities.GetNextSequence(Context, Context.TR_PROGRAM_CENTER.EntitySet.Name),
                                                         .TR_PROGRAM_ID = objProgram.ID,
                                                         .TR_CENTER_ID = center.ID})
                Next
            End If

            Dim oldLectures = From i In Context.TR_PROGRAM_LECTURE Where i.TR_PROGRAM_ID = Program.ID
            For Each item In oldLectures
                Context.TR_PROGRAM_LECTURE.DeleteObject(item)
            Next
            If Program.Lectures IsNot Nothing Then
                For Each lec In Program.Lectures
                    Context.TR_PROGRAM_LECTURE.AddObject(New TR_PROGRAM_LECTURE With {
                                                     .ID = Utilities.GetNextSequence(Context, Context.TR_PROGRAM_LECTURE.EntitySet.Name),
                                                     .TR_PROGRAM_ID = objProgram.ID,
                                                     .TR_LECTURE_ID = lec.ID})
                Next
            End If

            Dim oldEmployees = From i In Context.TR_PROGRAM_EMP Where i.TR_PROGRAM_ID = Program.ID 'And i.STATUS <> 1
            For Each Employee In oldEmployees
                Context.TR_PROGRAM_EMP.DeleteObject(Employee)
            Next
            If Program.Employees IsNot Nothing Then
                For Each Employee In Program.Employees
                    Context.TR_PROGRAM_EMP.AddObject(New TR_PROGRAM_EMP With {
                                                 .ID = Utilities.GetNextSequence(Context, Context.TR_PROGRAM_EMP.EntitySet.Name),
                                                 .TR_PROGRAM_ID = objProgram.ID,
                                                 .EMP_ID = Employee.ID,
                                                 .EMP_TYPE = Employee.EMP_TYPE,
                                                 .STATUS = Employee.STATUS_ID})
                Next
            End If

            Context.SaveChanges(log)
            gID = objProgram.ID
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".ModifyProgram")
            Throw ex
        End Try

    End Function

    Public Function DeletePrograms(ByVal lstId As List(Of Decimal)) As Boolean
        Try
            Dim proPrepare = (From p In Context.TR_PREPARE Where lstId.Contains(p.TR_PROGRAM_ID))
            Dim proClass = (From p In Context.TR_CLASS Where lstId.Contains(p.TR_PROGRAM_ID))
            Dim proCommit = (From p In Context.TR_PROGRAM_COMMIT Where lstId.Contains(p.TR_PROGRAM_ID))

            If proPrepare.Count > 0 Or proClass.Count > 0 Or proCommit.Count > 0 Then
                Return False
            End If

            Dim deletedPrograms = (From record In Context.TR_PROGRAM Where lstId.Contains(record.ID))

            Dim deletedProgramDeparment = (From record In Context.TR_PROGRAM_UNIT Where lstId.Contains(record.TR_PROGRAM_ID))
            For Each item In deletedProgramDeparment
                Context.TR_PROGRAM_UNIT.DeleteObject(item)
            Next

            Dim deletedProgramTitle = (From record In Context.TR_PROGRAM_TITLE Where lstId.Contains(record.TR_PROGRAM_ID))
            For Each item In deletedProgramTitle
                Context.TR_PROGRAM_TITLE.DeleteObject(item)
            Next

            Dim deletedProgramCenter = (From record In Context.TR_PROGRAM_CENTER Where lstId.Contains(record.TR_PROGRAM_ID))
            For Each item In deletedProgramCenter
                Context.TR_PROGRAM_CENTER.DeleteObject(item)
            Next

            Dim deletedProgramLecture = (From record In Context.TR_PROGRAM_LECTURE Where lstId.Contains(record.TR_PROGRAM_ID))
            For Each item In deletedProgramLecture
                Context.TR_PROGRAM_LECTURE.DeleteObject(item)
            Next

            Dim deletedProgramCost = (From record In Context.TR_PROGRAM_COST Where lstId.Contains(record.TR_PROGRAM_ID))
            For Each item In deletedProgramCost
                Context.TR_PROGRAM_COST.DeleteObject(item)
            Next

            For Each item In deletedPrograms
                Context.TR_PROGRAM.DeleteObject(item)
            Next

            Context.SaveChanges()
            Return True

        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".DeletePrograms")
            Throw ex
        End Try
    End Function

#End Region

#Region "Prepare"

    Public Function GetPrepare(ByVal _filter As ProgramPrepareDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ProgramPrepareDTO)

        Try
            Dim query = From p In Context.TR_PREPARE
                        From emp In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID).DefaultIfEmpty
                        From pre In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TR_LIST_PREPARE_ID).DefaultIfEmpty
                        Where p.TR_PROGRAM_ID = _filter.TR_PROGRAM_ID
                        Select New ProgramPrepareDTO With {
                            .ID = p.ID,
                            .EMPLOYEE_ID = p.EMPLOYEE_ID,
                            .EMPLOYEE_CODE = emp.EMPLOYEE_CODE,
                            .EMPLOYEE_NAME = emp.FULLNAME_VN,
                            .TR_LIST_PREPARE_ID = p.TR_LIST_PREPARE_ID,
                            .TR_LIST_PREPARE_NAME = pre.NAME_VN,
                            .TR_PROGRAM_ID = p.TR_PROGRAM_ID,
                            .CREATED_DATE = p.CREATED_DATE}

            Dim lst = query
            If _filter.EMPLOYEE_CODE <> "" Then
                lst = lst.Where(Function(p) p.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If
            If _filter.EMPLOYEE_NAME <> "" Then
                lst = lst.Where(Function(p) p.EMPLOYEE_NAME.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If
            If _filter.TR_LIST_PREPARE_NAME <> "" Then
                lst = lst.Where(Function(p) p.TR_LIST_PREPARE_NAME.ToUpper.Contains(_filter.TR_LIST_PREPARE_NAME.ToUpper))
            End If
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetPrepare")
            Throw ex
        End Try

    End Function

    Public Function InsertPrepare(ByVal objPrepare As ProgramPrepareDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objPrepareData As New TR_PREPARE
        Try
            objPrepareData.ID = Utilities.GetNextSequence(Context, Context.TR_PREPARE.EntitySet.Name)
            objPrepareData.EMPLOYEE_ID = objPrepare.EMPLOYEE_ID
            objPrepareData.TR_LIST_PREPARE_ID = objPrepare.TR_LIST_PREPARE_ID
            objPrepareData.TR_PROGRAM_ID = objPrepare.TR_PROGRAM_ID
            Context.TR_PREPARE.AddObject(objPrepareData)
            Context.SaveChanges(log)
            gID = objPrepareData.ID
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".InsertPrepare")
            Throw ex
        End Try
    End Function

    Public Function ModifyPrepare(ByVal objPrepare As ProgramPrepareDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objPrepareData As New TR_PREPARE With {.ID = objPrepare.ID}
        Try
            Context.TR_PREPARE.Attach(objPrepareData)
            objPrepareData.EMPLOYEE_ID = objPrepare.EMPLOYEE_ID
            objPrepareData.TR_LIST_PREPARE_ID = objPrepare.TR_LIST_PREPARE_ID
            objPrepareData.TR_PROGRAM_ID = objPrepare.TR_PROGRAM_ID
            Context.SaveChanges(log)
            gID = objPrepareData.ID
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".ModifyPrepare")
            Throw ex
        End Try

    End Function

    Public Function DeletePrepare(ByVal lstPrepare() As ProgramPrepareDTO) As Boolean
        Dim lstPrepareData As List(Of TR_PREPARE)
        Dim lstIDPrepare As List(Of Decimal) = (From p In lstPrepare.ToList Select p.ID).ToList
        Try
            lstPrepareData = (From p In Context.TR_PREPARE Where lstIDPrepare.Contains(p.ID)).ToList
            For index = 0 To lstPrepareData.Count - 1
                Context.TR_PREPARE.DeleteObject(lstPrepareData(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".DeletePrepare")
            Throw ex
        End Try

    End Function

#End Region

#Region "Class"

    Public Function GetClass(ByVal _filter As ProgramClassDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As DataTable

        Try
            'Dim query = From p In Context.TR_CLASS
            '            From class_teacher In Context.TR_CLASS_TEACHER.Where(Function(f) f.TR_CLASS_ID = p.ID).DefaultIfEmpty
            '            From district In Context.HU_DISTRICT.Where(Function(f) f.ID = p.DISTRICT_ID).DefaultIfEmpty
            '            From province In Context.HU_PROVINCE.Where(Function(f) f.ID = p.PROVINCE_ID).DefaultIfEmpty
            '            Where p.TR_PROGRAM_ID = _filter.TR_PROGRAM_ID
            '            Select New ProgramClassDTO With {
            '                .ID = p.ID,
            '                .NAME = p.NAME,
            '                .ADDRESS = p.ADDRESS,
            '                .DISTRICT_ID = p.DISTRICT_ID,
            '                .DISTRICT_NAME = district.NAME_VN,
            '                .PROVINCE_ID = p.PROVINCE_ID,
            '                .PROVINCE_NAME = province.NAME_VN,
            '                .END_DATE = p.END_DATE,
            '                .START_DATE = p.START_DATE,
            '                .TR_PROGRAM_ID = p.TR_PROGRAM_ID,
            '                .CREATED_DATE = p.CREATED_DATE,
            '                .TIME_FROM = p.TIME_FROM,
            '                .TIME_TO = p.TIME_TO,
            '                .TOTAL_DAY = p.TOTAL_DAY,
            '                .TOTAL_TIME = p.TOTAL_TIME,
            '                .REMARK = p.REMARK,
            '                .TEACHER_ID = class_teacher.TEACHER_ID,
            '                .TEACHER_NAME = class_teacher.TEACHER_NAME}

            'Dim lst = query
            'If _filter.NAME <> "" Then
            '    lst = lst.Where(Function(p) p.NAME.ToUpper.Contains(_filter.NAME.ToUpper))
            'End If
            'If _filter.ADDRESS <> "" Then
            '    lst = lst.Where(Function(p) p.ADDRESS.ToUpper.Contains(_filter.ADDRESS.ToUpper))
            'End If
            'If _filter.DISTRICT_NAME <> "" Then
            '    lst = lst.Where(Function(p) p.DISTRICT_NAME.ToUpper.Contains(_filter.DISTRICT_NAME.ToUpper))
            'End If
            'If _filter.PROVINCE_NAME <> "" Then
            '    lst = lst.Where(Function(p) p.PROVINCE_NAME.ToUpper.Contains(_filter.PROVINCE_NAME.ToUpper))
            'End If
            'If _filter.START_DATE IsNot Nothing Then
            '    lst = lst.Where(Function(p) p.START_DATE = _filter.START_DATE)
            'End If
            'If _filter.END_DATE IsNot Nothing Then
            '    lst = lst.Where(Function(p) p.END_DATE = _filter.END_DATE)
            'End If
            'lst = lst.OrderBy(Sorts)
            'Total = lst.Count
            'lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            'Return lst.ToList

            Using cls As New DataAccess.QueryData
                Dim dsData As DataSet = cls.ExecuteStore("PKG_TRAINING.GET_CLASS",
                                                          New With {.P_PROGRAM_ID = _filter.TR_PROGRAM_ID,
                                                          .P_PAGE_INDEX = PageIndex + 1,
                                                          .P_PAGE_SIZE = PageSize,
                                                          .P_CUR = cls.OUT_CURSOR,
                                                          .P_CURCOUNT = cls.OUT_CURSOR}, False)
                Total = dsData.Tables(1).Rows(0)("TOTAL")
                Return dsData.Tables(0)
            End Using
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetClass")
            Throw ex
        End Try

    End Function

    Public Function GetClassByID(ByVal _filter As ProgramClassDTO) As ProgramClassDTO

        Try
            Dim query = From p In Context.TR_CLASS
                        From district In Context.HU_DISTRICT.Where(Function(f) f.ID = p.DISTRICT_ID).DefaultIfEmpty
                        From province In Context.HU_PROVINCE.Where(Function(f) f.ID = p.PROVINCE_ID).DefaultIfEmpty
                        Where p.ID = _filter.ID
                        Select New ProgramClassDTO With {
                            .ID = p.ID,
                            .NAME = p.NAME,
                            .ADDRESS = p.ADDRESS,
                            .DISTRICT_ID = p.DISTRICT_ID,
                            .DISTRICT_NAME = district.NAME_VN,
                            .PROVINCE_ID = p.PROVINCE_ID,
                            .PROVINCE_NAME = province.NAME_VN,
                            .END_DATE = p.END_DATE,
                            .START_DATE = p.START_DATE,
                            .TR_PROGRAM_ID = p.TR_PROGRAM_ID,
                            .CREATED_DATE = p.CREATED_DATE}

            Return query.FirstOrDefault
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetClass")
            Throw ex
        End Try

    End Function

    Public Function InsertClass(ByVal objClass As ProgramClassDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objClassData As New TR_CLASS
        Dim objClassTeacherData As New TR_CLASS_TEACHER
        Try
            objClassData.ID = Utilities.GetNextSequence(Context, Context.TR_CLASS.EntitySet.Name)
            objClassData.ADDRESS = objClass.ADDRESS
            objClassData.PROVINCE_ID = objClass.PROVINCE_ID
            objClassData.DISTRICT_ID = objClass.DISTRICT_ID
            objClassData.END_DATE = objClass.END_DATE
            objClassData.NAME = objClass.NAME
            objClassData.START_DATE = objClass.START_DATE
            objClassData.TR_PROGRAM_ID = objClass.TR_PROGRAM_ID
            objClassData.TIME_FROM = objClass.TIME_FROM
            objClassData.TIME_TO = objClass.TIME_TO
            objClassData.TOTAL_DAY = objClass.TOTAL_DAY
            objClassData.TOTAL_TIME = objClass.TOTAL_TIME
            objClassData.REMARK = objClass.REMARK
            objClassData.EMAIL_CONTENT = objClass.EMAIL_CONTENT
            objClassData.RATIO = objClass.RATIO
            Context.TR_CLASS.AddObject(objClassData)
            Context.SaveChanges(log)
            gID = objClassData.ID

            objClassTeacherData.ID = Utilities.GetNextSequence(Context, Context.TR_CLASS_TEACHER.EntitySet.Name)
            objClassTeacherData.TR_CLASS_ID = objClassData.ID
            objClassTeacherData.TEACHER_ID = objClass.TEACHER_ID
            Context.TR_CLASS_TEACHER.AddObject(objClassTeacherData)
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".InsertClass")
            Throw ex
        End Try
    End Function

    Public Function ModifyClass(ByVal objClass As ProgramClassDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objClassData As New TR_CLASS With {.ID = objClass.ID}
        Dim objClassTeacherData As New TR_CLASS_TEACHER
        objClassTeacherData = (From p In Context.TR_CLASS_TEACHER
                               Where p.TR_CLASS_ID = objClass.ID).FirstOrDefault
        Try
            Context.TR_CLASS.Attach(objClassData)
            objClassData.ADDRESS = objClass.ADDRESS
            objClassData.PROVINCE_ID = objClass.PROVINCE_ID
            objClassData.DISTRICT_ID = objClass.DISTRICT_ID
            objClassData.END_DATE = objClass.END_DATE
            objClassData.NAME = objClass.NAME
            objClassData.START_DATE = objClass.START_DATE
            objClassData.TR_PROGRAM_ID = objClass.TR_PROGRAM_ID
            objClassData.TIME_FROM = objClass.TIME_FROM
            objClassData.TIME_TO = objClass.TIME_TO
            objClassData.TOTAL_DAY = objClass.TOTAL_DAY
            objClassData.TOTAL_TIME = objClass.TOTAL_TIME
            objClassData.REMARK = objClass.REMARK
            objClassData.EMAIL_CONTENT = objClass.EMAIL_CONTENT
            objClassData.RATIO = objClass.RATIO
            Context.SaveChanges(log)
            gID = objClassData.ID


            Context.TR_CLASS_TEACHER.Attach(objClassTeacherData)
            objClassTeacherData.TEACHER_ID = objClass.TEACHER_ID
            Context.SaveChanges(log)

            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".ModifyClass")
            Throw ex
        End Try

    End Function

    Public Function DeleteClass(ByVal lstClass() As ProgramClassDTO) As Boolean
        Dim lstClassData As List(Of TR_CLASS)
        Dim lstClassTeacherData As List(Of TR_CLASS_TEACHER)
        Dim lstIDClass As List(Of Decimal) = (From p In lstClass.ToList Select p.ID).ToList
        Try
            lstClassData = (From p In Context.TR_CLASS Where lstIDClass.Contains(p.ID)).ToList
            For index = 0 To lstClassData.Count - 1
                Context.TR_CLASS.DeleteObject(lstClassData(index))
            Next
            Context.SaveChanges()

            lstClassTeacherData = (From p In Context.TR_CLASS_TEACHER Where lstIDClass.Contains(p.TR_CLASS_ID)).ToList
            For index = 0 To lstClassTeacherData.Count - 1
                Context.TR_CLASS_TEACHER.DeleteObject(lstClassTeacherData(index))
            Next
            Context.SaveChanges()

            Return True

        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteClass")
            Throw ex
        End Try

    End Function



#End Region

#Region "ClassStudent"
    Private Sub GetOrgChild(ByVal orgID As Decimal, ByRef lstORG As List(Of Decimal))
        Dim orgs = (From o In Context.HU_ORGANIZATION Where o.PARENT_ID = orgID Select o.ID)
        lstORG.Add(orgID)
        If orgs.Count > 0 Then
            lstORG.AddRange(orgs)
            For Each org In orgs
                GetOrgChild(org, lstORG)
            Next
        End If
    End Sub



    Public Function GetEmployeeNotByClassID(ByVal _filter As ProgramClassStudentDTO, ByVal PageIndex As Integer,
                                            ByVal PageSize As Integer, ByRef Total As Integer,
                                            Optional ByVal Sorts As String = "EMPLOYEE_CODE desc") As List(Of ProgramClassStudentDTO)
        Try
            'Where tr_program_emp.TR_PROGRAM_ID = _filter.TR_PROGRAM_ID And tr_program_emp.TR_CLASS_ID Is Nothing
            Dim query As IQueryable(Of ProgramClassStudentDTO)
            If _filter.IS_PLAN Then
                query = From p In Context.HU_EMPLOYEE
                        From cv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = p.ID).DefaultIfEmpty
                        From title In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From ctr In Context.HU_CONTRACT.Where(Function(f) f.ID = p.CONTRACT_ID).DefaultIfEmpty
                        From ctr_type In Context.HU_CONTRACT_TYPE.Where(Function(f) f.ID = ctr.CONTRACT_TYPE_ID).DefaultIfEmpty
                        From gender In Context.OT_OTHER_LIST.Where(Function(f) f.ID = cv.GENDER).DefaultIfEmpty
                        From tr_program_emp In Context.TR_PROGRAM_EMP.Where(Function(f) f.EMP_ID = p.ID)
                        Where tr_program_emp.TR_PROGRAM_ID = _filter.TR_PROGRAM_ID
                        Select New ProgramClassStudentDTO With {
                                 .EMPLOYEE_ID = p.ID,
                                 .EMPLOYEE_CODE = p.EMPLOYEE_CODE,
                                 .EMPLOYEE_NAME = p.FULLNAME_VN,
                                 .BIRTH_DATE = cv.BIRTH_DATE,
                                 .ID_NO = cv.ID_NO,
                                 .ID_DATE = cv.ID_DATE,
                                 .TITLE_NAME = title.NAME_VN,
                                 .ORG_NAME = org.NAME_VN,
                                 .CONTRACT_TYPE_NAME = ctr_type.NAME,
                                 .GENDER_NAME = gender.NAME_VN}
            Else
                query = From p In Context.HU_EMPLOYEE
                        From cv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = p.ID).DefaultIfEmpty
                        From title In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From ctr In Context.HU_CONTRACT.Where(Function(f) f.ID = p.CONTRACT_ID).DefaultIfEmpty
                        From ctr_type In Context.HU_CONTRACT_TYPE.Where(Function(f) f.ID = ctr.CONTRACT_TYPE_ID).DefaultIfEmpty
                        From gender In Context.OT_OTHER_LIST.Where(Function(f) f.ID = cv.GENDER).DefaultIfEmpty
                        From tr_program_emp In Context.TR_PROGRAM_EMP.Where(Function(f) f.EMP_ID = p.ID)
                        Where tr_program_emp.TR_PROGRAM_ID = _filter.TR_PROGRAM_ID And tr_program_emp.TR_CLASS_ID Is Nothing
                        Select New ProgramClassStudentDTO With {.EMPLOYEE_ID = p.ID,
                                                                       .EMPLOYEE_CODE = p.EMPLOYEE_CODE,
                                                                       .EMPLOYEE_NAME = p.FULLNAME_VN,
                                                                       .ORG_ID = p.ORG_ID,
                                                                       .TITLE_ID = p.TITLE_ID,
                                                                       .CONTRACT_TYPE_ID = ctr_type.ID,
                                                                       .BIRTH_DATE = cv.BIRTH_DATE,
                                                                       .ID_NO = cv.ID_NO,
                                                                       .ID_DATE = cv.ID_DATE,
                                                                       .TITLE_NAME = title.NAME_VN,
                                                                       .ORG_NAME = org.NAME_VN,
                                                                       .CONTRACT_TYPE_NAME = ctr_type.NAME,
                                                                       .GENDER_NAME = gender.NAME_VN}

                If _filter.ORG_ID IsNot Nothing Then
                    Dim lstOrg As New List(Of Decimal)
                    GetOrgChild(_filter.ORG_ID, lstOrg)
                    query = query.Where(Function(f) lstOrg.Contains(f.ORG_ID))
                    'query = query.Where(Function(f) f.ORG_ID = _filter.ORG_ID)
                End If

                'If _filter.lstTitle IsNot Nothing Then
                '    query = query.Where(Function(f) _filter.lstTitle.Contains(f.TITLE_ID))
                'End If

                'If _filter.lstContractType IsNot Nothing Then
                '    query = query.Where(Function(f) _filter.lstContractType.Contains(f.CONTRACT_TYPE_ID))
                'End If
            End If
            Dim lsEmpID = (From o In Context.TR_CLASS_STUDENT Where o.TR_CLASS_ID = _filter.TR_CLASS_ID Select o.EMPLOYEE_ID).ToList()
            If lsEmpID.Count > 0 Then
                query = query.Where(Function(f) Not lsEmpID.Contains(f.EMPLOYEE_ID))
            End If
            Dim lst = query

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetEmployeeNotByClassID")
            Throw ex
        End Try

    End Function

    Public Function GetEmployeeByClassID(ByVal _filter As ProgramClassStudentDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "EMPLOYEE_CODE desc") As List(Of ProgramClassStudentDTO)

        Try
            Dim query = From student In Context.TR_CLASS_STUDENT
                        From p In Context.HU_EMPLOYEE.Where(Function(f) f.ID = student.EMPLOYEE_ID)
                        From cv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = p.ID).DefaultIfEmpty
                        From title In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From ctr In Context.HU_CONTRACT.Where(Function(f) f.ID = p.CONTRACT_ID).DefaultIfEmpty
                        From ctr_type In Context.HU_CONTRACT_TYPE.Where(Function(f) f.ID = ctr.CONTRACT_TYPE_ID).DefaultIfEmpty
                        From gender In Context.OT_OTHER_LIST.Where(Function(f) f.ID = cv.GENDER).DefaultIfEmpty
                        Where student.TR_CLASS_ID = _filter.TR_CLASS_ID
                        Select New ProgramClassStudentDTO With {
                            .ID = p.ID,
                            .EMPLOYEE_ID = p.ID,
                            .EMPLOYEE_CODE = p.EMPLOYEE_CODE,
                            .EMPLOYEE_NAME = p.FULLNAME_VN,
                            .BIRTH_DATE = cv.BIRTH_DATE,
                            .ID_NO = cv.ID_NO,
                            .ID_DATE = cv.ID_DATE,
                            .TITLE_NAME = title.NAME_VN,
                            .ORG_NAME = org.NAME_VN,
                            .CONTRACT_TYPE_NAME = ctr_type.NAME,
                            .GENDER_NAME = gender.NAME_VN,
                            .WORK_EMAIL = cv.WORK_EMAIL,
                            .PER_EMAIL = cv.PER_EMAIL,
                            .SEND_MAIL_STATUS = If(student.SENDED_EMAIL = 1, "Đã gửi", "Chưa gửi")}

            Dim lst = query

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
            Return lst.ToList
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetEmployeeByClassID")
            Throw ex
        End Try

    End Function

    Public Function InsertClassStudent(ByVal lst As List(Of ProgramClassStudentDTO),
                                   ByVal log As UserLog) As Boolean
        Dim iCount As Integer = 0
        Dim objData As TR_CLASS_STUDENT
        Dim objResult As TR_PROGRAM_RESULT
        Try
            For Each obj In lst
                Dim isExist = (From p In Context.TR_CLASS_STUDENT
                               Where p.EMPLOYEE_ID = obj.EMPLOYEE_ID _
                               And p.TR_CLASS_ID = obj.TR_CLASS_ID).Any
                If Not isExist Then
                    objData = New TR_CLASS_STUDENT
                    objData.ID = Utilities.GetNextSequence(Context, Context.TR_CLASS_STUDENT.EntitySet.Name)
                    objData.TR_CLASS_ID = obj.TR_CLASS_ID
                    objData.EMPLOYEE_ID = obj.EMPLOYEE_ID
                    Context.TR_CLASS_STUDENT.AddObject(objData)
                End If

                'Dim tr_program_emp = (From p In Context.TR_PROGRAM_EMP
                '                      Where p.EMP_ID = obj.EMPLOYEE_ID And p.TR_PROGRAM_ID = obj.TR_PROGRAM_ID).FirstOrDefault
                'If tr_program_emp IsNot Nothing Then
                '    tr_program_emp.TR_CLASS_ID = obj.TR_CLASS_ID
                'End If
                Dim isExistResult = (From p In Context.TR_PROGRAM_RESULT
                                     Where p.EMPLOYEE_ID = obj.EMPLOYEE_ID _
                                            And p.TR_PROGRAM_ID = obj.TR_PROGRAM_ID).Any
                If Not isExistResult Then
                    objResult = New TR_PROGRAM_RESULT
                    objResult.ID = Utilities.GetNextSequence(Context, Context.TR_PROGRAM_RESULT.EntitySet.Name)
                    objResult.TR_PROGRAM_ID = obj.TR_PROGRAM_ID
                    objResult.EMPLOYEE_ID = obj.EMPLOYEE_ID
                    Context.TR_PROGRAM_RESULT.AddObject(objResult)
                End If


            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function DeleteClassStudent(ByVal lst As List(Of ProgramClassStudentDTO),
                                       ByVal log As UserLog) As Boolean
        Try
            For Each obj As ProgramClassStudentDTO In lst
                Dim Students = (From p In Context.TR_CLASS_STUDENT
                                Where p.EMPLOYEE_ID = obj.EMPLOYEE_ID _
                               And p.TR_CLASS_ID = obj.TR_CLASS_ID).ToList
                For Each Student In Students
                    Context.TR_CLASS_STUDENT.DeleteObject(Student)
                Next

                'Dim tr_program_emp = (From p In Context.TR_PROGRAM_EMP
                '                      Where p.EMP_ID = obj.EMPLOYEE_ID And p.TR_PROGRAM_ID = obj.TR_PROGRAM_ID).FirstOrDefault
                'If tr_program_emp IsNot Nothing Then
                '    tr_program_emp.TR_CLASS_ID = Nothing
                'End If

                Dim Results = (From p In Context.TR_PROGRAM_RESULT
                               Where p.EMPLOYEE_ID = obj.EMPLOYEE_ID _
                               And p.TR_PROGRAM_ID = obj.TR_CLASS_ID).ToList
                For Each Result In Results
                    Context.TR_PROGRAM_RESULT.DeleteObject(Result)
                Next
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception

        End Try
    End Function

#End Region

#Region "ClassSchedule"

    Public Function GetClassSchedule(ByVal _filter As ProgramClassScheduleDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ProgramClassScheduleDTO)

        Try
            Dim query = From p In Context.TR_CLASS_SCHEDULE
                        Where p.TR_CLASS_ID = _filter.TR_CLASS_ID
                        Select New ProgramClassScheduleDTO With {
                            .ID = p.ID,
                            .TR_CLASS_ID = p.TR_CLASS_ID,
                            .START_TIME = p.START_TIME,
                            .END_TIME = p.END_TIME,
                            .CONTENT = p.CONTENT,
                            .CREATED_DATE = p.CREATED_DATE,
                            .TOTAL_TIME = p.TOTAL_TIME}

            Dim lst = query
            If _filter.START_TIME IsNot Nothing Then
                lst = lst.Where(Function(p) p.START_TIME = _filter.START_TIME)
            End If
            If _filter.END_TIME IsNot Nothing Then
                lst = lst.Where(Function(p) p.END_TIME = _filter.END_TIME)
            End If
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetClassSchedule")
            Throw ex
        End Try
    End Function

    Public Function GetClassScheduleByID(ByVal _filter As ProgramClassScheduleDTO) As ProgramClassScheduleDTO
        Try
            Dim query = From p In Context.TR_CLASS_SCHEDULE
                        Where p.TR_CLASS_ID = _filter.TR_CLASS_ID
                        Select New ProgramClassScheduleDTO With {
                            .ID = p.ID,
                            .TR_CLASS_ID = p.TR_CLASS_ID,
                            .START_TIME = p.START_TIME,
                            .END_TIME = p.END_TIME,
                            .CONTENT = p.CONTENT,
                            .CREATED_DATE = p.CREATED_DATE,
                            .TOTAL_TIME = p.TOTAL_TIME}
            Return query.FirstOrDefault
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetClassSchedule")
            Throw ex
        End Try
    End Function

    Public Function InsertClassSchedule(ByVal objClassSchedule As ProgramClassScheduleDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objClassScheduleData As New TR_CLASS_SCHEDULE
        Try
            objClassScheduleData.ID = Utilities.GetNextSequence(Context, Context.TR_CLASS_SCHEDULE.EntitySet.Name)
            objClassScheduleData.TR_CLASS_ID = objClassSchedule.TR_CLASS_ID
            objClassScheduleData.START_TIME = objClassSchedule.START_TIME
            objClassScheduleData.END_TIME = objClassSchedule.END_TIME
            objClassScheduleData.CONTENT = objClassSchedule.CONTENT
            objClassScheduleData.TOTAL_TIME = objClassSchedule.TOTAL_TIME
            Context.TR_CLASS_SCHEDULE.AddObject(objClassScheduleData)
            Context.SaveChanges(log)
            gID = objClassScheduleData.ID
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".InsertClassSchedule")
            Throw ex
        End Try
    End Function

    Public Function ModifyClassSchedule(ByVal objClassSchedule As ProgramClassScheduleDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objClassScheduleData As New TR_CLASS_SCHEDULE With {.ID = objClassSchedule.ID}
        Try
            Context.TR_CLASS_SCHEDULE.Attach(objClassScheduleData)
            objClassScheduleData.START_TIME = objClassSchedule.START_TIME
            objClassScheduleData.END_TIME = objClassSchedule.END_TIME
            objClassScheduleData.CONTENT = objClassSchedule.CONTENT
            objClassScheduleData.TOTAL_TIME = objClassSchedule.TOTAL_TIME
            Context.SaveChanges(log)
            gID = objClassScheduleData.ID
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".ModifyClassSchedule")
            Throw ex
        End Try
    End Function

    Public Function DeleteClassSchedule(ByVal lstClassSchedule() As ProgramClassScheduleDTO) As Boolean
        Dim lstClassScheduleData As List(Of TR_CLASS_SCHEDULE)
        Dim lstIDClassSchedule As List(Of Decimal) = (From p In lstClassSchedule.ToList Select p.ID).ToList
        Try
            lstClassScheduleData = (From p In Context.TR_CLASS_SCHEDULE Where lstIDClassSchedule.Contains(p.ID)).ToList
            For index = 0 To lstClassScheduleData.Count - 1
                Context.TR_CLASS_SCHEDULE.DeleteObject(lstClassScheduleData(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteClassSchedule")
            Throw ex
        End Try
    End Function

#End Region

#Region "ProgramCommit"

    Public Function GetProgramCommit(ByVal _filter As ProgramCommitDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "EMPLOYEE_CODE desc") As List(Of ProgramCommitDTO)
        Try
            Dim query = From student In Context.TR_CLASS_STUDENT
                        From cls In Context.TR_CLASS.Where(Function(f) f.ID = student.TR_CLASS_ID)
                        From emp In Context.HU_EMPLOYEE.Where(Function(f) f.ID = student.EMPLOYEE_ID)
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = emp.ORG_ID).DefaultIfEmpty
                        From commit In Context.TR_PROGRAM_COMMIT.Where(Function(f) f.EMPLOYEE_ID = student.EMPLOYEE_ID _
                                                                               And f.TR_PROGRAM_ID = cls.TR_PROGRAM_ID).DefaultIfEmpty
                        From pro In Context.TR_PROGRAM.Where(Function(f) f.ID = cls.TR_PROGRAM_ID).DefaultIfEmpty
                        From cat In Context.TR_COMMIT_AFTER_TRAIN.Where(Function(f) f.COST_TRAIN_FROM <= pro.COST_STUDENT _
                                                                                And pro.COST_STUDENT <= f.COST_TRAIN_TO).DefaultIfEmpty
                        From app In Context.HU_EMPLOYEE.Where(Function(f) f.ID = commit.APPROVER_ID).DefaultIfEmpty
                        From title In Context.HU_TITLE.Where(Function(f) f.ID = app.TITLE_ID).DefaultIfEmpty
                        Where cls.TR_PROGRAM_ID = _filter.TR_PROGRAM_ID
                        Select New ProgramCommitDTO With {.EMPLOYEE_ID = emp.ID,
                                                          .EMPLOYEE_CODE = emp.EMPLOYEE_CODE,
                                                          .EMPLOYEE_NAME = emp.FULLNAME_VN,
                                                          .ORG_NAME = org.NAME_VN,
                                                          .COMMIT_NO = commit.COMMIT_NO,
                                                          .COMMIT_DATE = commit.COMMIT_DATE,
                                                          .CONVERED_TIME = commit.CONVERED_TIME,
                                                          .COMMIT_WORK = commit.COMMIT_WORK,
                                                          .APPROVER_ID = commit.APPROVER_ID,
                                                          .APPROVER_NAME = app.FULLNAME_VN,
                                                          .APPROVER_TITLE = title.NAME_VN,
                                                          .COMMIT_REMARK = commit.COMMIT_REMARK,
                                                          .COMMIT_START = commit.COMMIT_START,
                                                          .COMMIT_END = commit.COMMIT_END,
                                                          .COST_STUDENT = pro.COST_TOTAL,
                                                          .MONEY_COMMIT = commit.MONEY_COMMIT,
                                                          .IS_LOCK = commit.IS_COMMIT,
                                                          .IS_LOCK_NAME = If(commit.IS_COMMIT = -1, "X", "")}
            Dim lst = query.Distinct

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetProgramCommit")
            Throw ex
        End Try
    End Function

    Public Function UpdateProgramCommit(ByVal lst As List(Of ProgramCommitDTO),
                                   ByVal log As UserLog) As Boolean
        Try
            For Each obj In lst
                Dim objData = (From p In Context.TR_PROGRAM_COMMIT
                               Where p.EMPLOYEE_ID = obj.EMPLOYEE_ID _
                               And p.TR_PROGRAM_ID = obj.TR_PROGRAM_ID).FirstOrDefault

                If objData Is Nothing Then
                    objData = New TR_PROGRAM_COMMIT
                    With objData
                        .ID = Utilities.GetNextSequence(Context, Context.TR_PROGRAM_COMMIT.EntitySet.Name)
                        .TR_PROGRAM_ID = obj.TR_PROGRAM_ID
                        .EMPLOYEE_ID = obj.EMPLOYEE_ID
                        .COMMIT_NO = obj.COMMIT_NO
                        .COMMIT_DATE = obj.COMMIT_DATE
                        .CONVERED_TIME = obj.CONVERED_TIME
                        .COMMIT_START = obj.COMMIT_START
                        .COMMIT_END = obj.COMMIT_END
                        .COMMIT_WORK = obj.COMMIT_WORK
                        .APPROVER_ID = obj.APPROVER_ID
                        .COMMIT_REMARK = obj.COMMIT_REMARK
                        .IS_COMMIT = obj.IS_LOCK
                        .MONEY_COMMIT = obj.MONEY_COMMIT
                        Context.TR_PROGRAM_COMMIT.AddObject(objData)
                    End With
                Else
                    With objData
                        .COMMIT_NO = obj.COMMIT_NO
                        .COMMIT_DATE = obj.COMMIT_DATE
                        .CONVERED_TIME = obj.CONVERED_TIME
                        .COMMIT_START = obj.COMMIT_START
                        .COMMIT_END = obj.COMMIT_END
                        .COMMIT_WORK = obj.COMMIT_WORK
                        .APPROVER_ID = obj.APPROVER_ID
                        .COMMIT_REMARK = obj.COMMIT_REMARK
                        .MONEY_COMMIT = obj.MONEY_COMMIT
                        .IS_COMMIT = obj.IS_LOCK
                    End With
                End If
            Next

            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "ProgramResult"

    Public Function GetProgramResult(ByVal _filter As ProgramResultDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "EMPLOYEE_CODE desc") As List(Of ProgramResultDTO)
        Try
            Dim query = From student In Context.TR_PROGRAM_EMP
                        From emp In Context.HU_EMPLOYEE.Where(Function(f) f.ID = student.EMP_ID).DefaultIfEmpty
                        From title In Context.HU_TITLE.Where(Function(f) f.ID = emp.TITLE_ID).DefaultIfEmpty
                        From cls In Context.TR_CLASS.Where(Function(f) f.ID = student.TR_CLASS_ID).DefaultIfEmpty
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = emp.ORG_ID).DefaultIfEmpty
                        From Result In Context.TR_PROGRAM_RESULT.Where(Function(f) f.EMPLOYEE_ID = student.EMP_ID _
                                                                               And f.TR_PROGRAM_ID = student.TR_PROGRAM_ID).DefaultIfEmpty
                        From rank In Context.OT_OTHER_LIST.Where(Function(f) f.ID = Result.TR_RANK_ID).DefaultIfEmpty
                        From rerank In Context.OT_OTHER_LIST.Where(Function(f) f.ID = Result.RETEST_RANK_ID).DefaultIfEmpty
                        From commit In Context.TR_PROGRAM_COMMIT.Where(Function(f) f.EMPLOYEE_ID = student.EMP_ID _
                                                                               And f.TR_PROGRAM_ID = cls.TR_PROGRAM_ID).DefaultIfEmpty
                        Where student.TR_PROGRAM_ID = _filter.TR_PROGRAM_ID
                        Select New ProgramResultDTO With {.ID = Result.ID,
                                                          .EMPLOYEE_ID = emp.ID,
                                                          .EMPLOYEE_CODE = emp.EMPLOYEE_CODE,
                                                          .EMPLOYEE_NAME = emp.FULLNAME_VN,
                                                          .ORG_NAME = org.NAME_VN,
                                                          .DURATION = Result.DURATION,
                                                          .IS_EXAMS = Result.IS_EXAMS,
                                                          .IS_END = Result.IS_END,
                                                          .IS_REACH = Result.IS_REACH,
                                                          .IS_CERTIFICATE = Result.IS_CERTIFICATE,
                                                          .CERTIFICATE_DATE = Result.CERTIFICATE_DATE,
                                                          .CERTIFICATE_NO = Result.CERTIFICATE_NO,
                                                          .CER_RECEIVE_DATE = Result.CER_RECEIVE_DATE,
                                                          .TOIEC_BENCHMARK = Result.TOIEC_BENCHMARK,
                                                          .TOIEC_SCORE_IN = Result.TOIEC_SCORE_IN,
                                                          .TOIEC_SCORE_OUT = Result.TOIEC_SCORE_OUT,
                                                          .INCREMENT_SCORE = Result.INCREMENT_SCORE,
                                                          .TR_RANK_ID = Result.TR_RANK_ID,
                                                          .TR_RANK_NAME = rank.NAME_VN,
                                                          .RETEST_SCORE = Result.RETEST_SCORE,
                                                          .RETEST_RANK_ID = Result.RETEST_RANK_ID,
                                                          .RETEST_RANK_NAME = rerank.NAME_VN,
                                                          .RETEST_REMARK = Result.RETEST_REMARK,
                                                          .FINAL_SCORE = Result.FINAL_SCORE,
                                                          .ABSENT_REASON = Result.ABSENT_REASON,
                                                          .ABSENT_UNREASON = Result.ABSENT_UNREASON,
                                                          .COMMIT_STARTDATE = Result.COMMIT_STARTDATE,
                                                          .COMMIT_ENDDATE = Result.COMMIT_ENDDATE,
                                                          .COMMIT_WORKMONTH = Result.COMMIT_WORKMONTH,
                                                          .IS_REFUND_FEE = Result.IS_REFUND_FEE,
                                                          .IS_RESERVES = Result.IS_RESERVES,
                                                          .REMARK = Result.REMARK,
                                                          .ATTACH_FILE = Result.ATTACH_FILE,
                                                          .TITLE_ID = title.ID,
                                                          .TITLE_NAME = title.NAME_VN,
                                                          .COMMENT_1 = Result.COMMENT_1,
                                                          .COMMENT_2 = Result.COMMENT_2,
                                                          .COMMENT_3 = Result.COMMENT_3,
                                                          .INSERT_HSNV = If(Result.INSERT_HSNV IsNot Nothing, Result.INSERT_HSNV, 0),
                                                          .INSERT_HSNV_STATUS = If(Result.INSERT_HSNV = 1, True, False),
                                                          .CERT_DATE = Result.CERT_DATE}

            Dim lst = query.Distinct

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetProgramResult")
            Throw ex
        End Try
    End Function
    Public Function CheckProgramResult(ByVal lst As List(Of ProgramResultDTO)) As Boolean
        Try
            For Each obj In lst
                Dim objData = (From p In Context.TR_PROGRAM_RESULT
                               Where p.EMPLOYEE_ID = obj.EMPLOYEE_ID _
                               And p.TR_PROGRAM_ID = obj.TR_PROGRAM_ID).FirstOrDefault
                If objData Is Nothing Then
                    Return False
                End If
            Next
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function UpdateProgramResult(ByVal lst As List(Of ProgramResultDTO),
                                   ByVal log As UserLog) As Boolean
        Try
            For Each obj In lst
                Dim objData = (From p In Context.TR_PROGRAM_RESULT
                               Where p.EMPLOYEE_ID = obj.EMPLOYEE_ID _
                               And p.TR_PROGRAM_ID = obj.TR_PROGRAM_ID).FirstOrDefault

                If objData Is Nothing Then
                    objData = New TR_PROGRAM_RESULT
                    With objData
                        .ID = Utilities.GetNextSequence(Context, Context.TR_PROGRAM_RESULT.EntitySet.Name)
                        .TR_PROGRAM_ID = obj.TR_PROGRAM_ID
                        .EMPLOYEE_ID = obj.EMPLOYEE_ID
                        .DURATION = obj.DURATION
                        .IS_EXAMS = obj.IS_EXAMS
                        .IS_END = obj.IS_END
                        .IS_REACH = obj.IS_REACH
                        .IS_CERTIFICATE = obj.IS_CERTIFICATE
                        .CERTIFICATE_DATE = obj.CERTIFICATE_DATE
                        .CERTIFICATE_NO = obj.CERTIFICATE_NO
                        .CER_RECEIVE_DATE = obj.CER_RECEIVE_DATE
                        .TOIEC_BENCHMARK = obj.TOIEC_BENCHMARK
                        .TOIEC_SCORE_IN = obj.TOIEC_SCORE_IN
                        .TOIEC_SCORE_OUT = obj.TOIEC_SCORE_OUT
                        .INCREMENT_SCORE = obj.INCREMENT_SCORE
                        .TR_RANK_ID = obj.TR_RANK_ID
                        .RETEST_SCORE = obj.RETEST_SCORE
                        .RETEST_RANK_ID = obj.RETEST_RANK_ID
                        .RETEST_REMARK = obj.RETEST_REMARK
                        .FINAL_SCORE = obj.FINAL_SCORE
                        .ABSENT_REASON = obj.ABSENT_REASON
                        .ABSENT_UNREASON = obj.ABSENT_UNREASON
                        .COMMIT_STARTDATE = obj.COMMIT_STARTDATE
                        .COMMIT_ENDDATE = obj.COMMIT_ENDDATE
                        .COMMIT_WORKMONTH = obj.COMMIT_WORKMONTH
                        .COMMENT_1 = obj.COMMENT_1
                        .COMMENT_2 = obj.COMMENT_2
                        .COMMENT_3 = obj.COMMENT_3
                        .IS_REFUND_FEE = obj.IS_REFUND_FEE
                        .IS_RESERVES = obj.IS_RESERVES
                        .REMARK = obj.REMARK
                        .ATTACH_FILE = obj.ATTACH_FILE
                        Context.TR_PROGRAM_RESULT.AddObject(objData)
                    End With
                Else
                    With objData
                        .TR_PROGRAM_ID = obj.TR_PROGRAM_ID
                        .EMPLOYEE_ID = obj.EMPLOYEE_ID
                        .DURATION = obj.DURATION
                        .IS_EXAMS = obj.IS_EXAMS
                        .IS_END = obj.IS_END
                        .IS_REACH = obj.IS_REACH
                        .IS_CERTIFICATE = obj.IS_CERTIFICATE
                        .CERTIFICATE_DATE = obj.CERTIFICATE_DATE
                        .CERTIFICATE_NO = obj.CERTIFICATE_NO
                        .CER_RECEIVE_DATE = obj.CER_RECEIVE_DATE
                        .TOIEC_BENCHMARK = obj.TOIEC_BENCHMARK
                        .TOIEC_SCORE_IN = obj.TOIEC_SCORE_IN
                        .TOIEC_SCORE_OUT = obj.TOIEC_SCORE_OUT
                        .INCREMENT_SCORE = obj.INCREMENT_SCORE
                        .TR_RANK_ID = obj.TR_RANK_ID
                        .RETEST_SCORE = obj.RETEST_SCORE
                        .RETEST_RANK_ID = obj.RETEST_RANK_ID
                        .RETEST_REMARK = obj.RETEST_REMARK
                        .FINAL_SCORE = obj.FINAL_SCORE
                        .ABSENT_REASON = obj.ABSENT_REASON
                        .ABSENT_UNREASON = obj.ABSENT_UNREASON
                        .COMMIT_STARTDATE = obj.COMMIT_STARTDATE
                        .COMMIT_ENDDATE = obj.COMMIT_ENDDATE
                        .COMMIT_WORKMONTH = obj.COMMIT_WORKMONTH
                        .IS_REFUND_FEE = obj.IS_REFUND_FEE
                        .IS_RESERVES = obj.IS_RESERVES
                        .COMMENT_1 = obj.COMMENT_1
                        .COMMENT_2 = obj.COMMENT_2
                        .COMMENT_3 = obj.COMMENT_3
                        .REMARK = obj.REMARK
                        .ATTACH_FILE = obj.ATTACH_FILE
                    End With
                End If
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function SendTrainingToEmployeeProfile(ByVal listTrainingId As List(Of Decimal), ByVal issuedDate As Date, ByVal log As UserLog) As Boolean
        Dim ListObj As List(Of PRO_TRAIN_OUT_COMPANY_DTO)
        Try
            ListObj = (From result In Context.TR_PROGRAM_RESULT
                       From program In Context.TR_PROGRAM.Where(Function(f) f.ID = result.TR_PROGRAM_ID)
                       From rank_name In Context.OT_OTHER_LIST.Where(Function(f) f.ID = result.TR_RANK_ID).DefaultIfEmpty
                       Where listTrainingId.Contains(result.ID)
                       Select New PRO_TRAIN_OUT_COMPANY_DTO With {
                            .EMPLOYEE_ID = result.EMPLOYEE_ID,
                            .CERTIFICATE_NAME = program.CERTIFICATE_NAME,
                            .YEAR_GRA = program.YEAR,
                            .FROM_DATE = program.START_DATE,
                            .TO_DATE = program.END_DATE,
                            .POINT_LEVEL = result.FINAL_SCORE,
                            .RESULT_TRAIN = rank_name.NAME_VN,
                            .TR_PROGRAM_RESULT_ID = result.ID
                          }).ToList()

            For Each Obj As PRO_TRAIN_OUT_COMPANY_DTO In ListObj
                Dim ObjData As New HU_PRO_TRAIN_OUT_COMPANY
                ObjData.ID = Utilities.GetNextSequence(Context, Context.HU_PRO_TRAIN_OUT_COMPANY.EntitySet.Name)
                ObjData.EMPLOYEE_ID = Obj.EMPLOYEE_ID
                ObjData.CERTIFICATE_NAME = Obj.CERTIFICATE_NAME
                ObjData.YEAR_GRA = Obj.YEAR_GRA
                ObjData.FROM_DATE = Obj.FROM_DATE
                ObjData.TO_DATE = Obj.TO_DATE
                ObjData.POINT_LEVEL = Obj.POINT_LEVEL
                ObjData.RESULT_TRAIN = Obj.RESULT_TRAIN
                ObjData.TR_PROGRAM_RESULT_ID = Obj.TR_PROGRAM_RESULT_ID
                Context.HU_PRO_TRAIN_OUT_COMPANY.AddObject(ObjData)
                Context.SaveChanges(log)
            Next

            For Each id In listTrainingId
                Dim Result = (From p In Context.TR_PROGRAM_RESULT
                              Where p.ID = id
                              Select p).FirstOrDefault
                Result.INSERT_HSNV = 1
                Result.CERT_DATE = issuedDate
                Context.SaveChanges(log)
            Next
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".InsertClass")
            Throw ex
        End Try
    End Function

    Public Function GetTRResult(ByVal _filter As ProgramResultDTO) As List(Of ProgramResultDTO)
        Try
            Dim lst As New List(Of ProgramResultDTO)
            If _filter.TR_PROGRAM_ID IsNot Nothing AndAlso _filter.EMPLOYEE_ID IsNot Nothing Then
                Using cls As New DataAccess.QueryData
                    Dim dsData As DataSet = cls.ExecuteStore("PKG_TRAINING_BUSINESS.PRS_GET_TRRESULT",
                                               New With {.P_PROGRAM_ID = _filter.TR_PROGRAM_ID,
                                                         .P_EMPLOYEE_ID = _filter.EMPLOYEE_ID,
                                                         .P_CUR = cls.OUT_CURSOR}, False)
                    Dim dt As DataTable = dsData.Tables(0)
                    Dim nullDec As New Decimal?
                    nullDec = Nothing
                    If dt IsNot Nothing Then
                        lst = (From row As DataRow In dt.Rows
                               Select New ProgramResultDTO With {.FINAL_SCORE = CDec(If(row("FINAL_SCORE").ToString = "", Nothing, row("FINAL_SCORE"))),
                                                                 .TR_RANK_ID = CDec(If(row("TR_RANK_ID").ToString = "", Nothing, row("TR_RANK_ID")))}).ToList
                    End If
                End Using
            End If

            Return lst
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetTRResult")
            Throw ex
        End Try
    End Function


    Public Function ValidateCerificateConfirm(ByVal listTrainingId As List(Of Decimal)) As ProgramResultDTO
        Try
            Dim obj As New ProgramResultDTO

            obj = (From r In Context.TR_PROGRAM_RESULT
                   From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = r.EMPLOYEE_ID)
                   Where listTrainingId.Contains(r.ID) AndAlso r.IS_REACH Is Nothing AndAlso r.TR_RANK_ID Is Nothing _
                       AndAlso r.FINAL_SCORE Is Nothing AndAlso r.COMMENT_1 Is Nothing AndAlso r.COMMENT_2 Is Nothing AndAlso r.COMMENT_3 Is Nothing
                   Select New ProgramResultDTO With {
                            .EMPLOYEE_ID = r.EMPLOYEE_ID,
                            .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                            .EMPLOYEE_NAME = e.FULLNAME_VN
                          }).FirstOrDefault


            Return obj
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".ValidateCerificateConfirm")
            Throw ex
        End Try
    End Function

#End Region

#Region "ProgramCost"

    Public Function GetProgramCost(ByVal _filter As ProgramCostDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ProgramCostDTO)

        Try
            Dim query = From p In Context.TR_PROGRAM_COST
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        Where p.TR_PROGRAM_ID = _filter.TR_PROGRAM_ID
                        Select New ProgramCostDTO With {
                            .ID = p.ID,
                            .ORG_ID = p.ORG_ID,
                            .ORG_NAME = org.NAME_VN,
                            .COST_COMPANY = p.COST_COMPANY,
                            .COST_OF_STUDENT = p.COST_OF_STUDENT,
                            .STUDENT_NUMBER = p.STUDENT_NUMBER,
                            .TR_PROGRAM_ID = p.TR_PROGRAM_ID,
                            .CREATED_DATE = p.CREATED_DATE}

            Dim lst = query
            If _filter.ORG_NAME <> "" Then
                lst = lst.Where(Function(p) p.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If
            If _filter.COST_COMPANY IsNot Nothing Then
                lst = lst.Where(Function(p) p.COST_COMPANY = _filter.COST_COMPANY)
            End If
            If _filter.COST_OF_STUDENT IsNot Nothing Then
                lst = lst.Where(Function(p) p.COST_OF_STUDENT = _filter.COST_OF_STUDENT)
            End If
            If _filter.STUDENT_NUMBER IsNot Nothing Then
                lst = lst.Where(Function(p) p.STUDENT_NUMBER = _filter.STUDENT_NUMBER)
            End If
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetProgramCost")
            Throw ex
        End Try

    End Function

    Public Function InsertProgramCost(ByVal objProgramCost As ProgramCostDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objProgramCostData As New TR_PROGRAM_COST
        Try
            objProgramCostData.ID = Utilities.GetNextSequence(Context, Context.TR_PROGRAM_COST.EntitySet.Name)
            objProgramCostData.COST_COMPANY = objProgramCost.COST_COMPANY
            objProgramCostData.COST_OF_STUDENT = objProgramCost.COST_OF_STUDENT
            objProgramCostData.STUDENT_NUMBER = objProgramCost.STUDENT_NUMBER
            objProgramCostData.ORG_ID = objProgramCost.ORG_ID
            objProgramCostData.TR_PROGRAM_ID = objProgramCost.TR_PROGRAM_ID
            Context.TR_PROGRAM_COST.AddObject(objProgramCostData)
            Context.SaveChanges(log)
            gID = objProgramCostData.ID
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".InsertProgramCost")
            Throw ex
        End Try
    End Function

    Public Function ValidateProgramCost(ByVal _validate As ProgramCostDTO)
        Dim query
        Try
            If _validate.ORG_ID IsNot Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.TR_PROGRAM_COST
                             Where p.ORG_ID = _validate.ORG_ID _
                             And p.TR_PROGRAM_ID = _validate.TR_PROGRAM_ID _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.TR_PROGRAM_COST
                             Where p.ORG_ID = _validate.ORG_ID _
                             And p.TR_PROGRAM_ID = _validate.TR_PROGRAM_ID).FirstOrDefault
                End If

                Return (query Is Nothing)
            End If
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".ValidateProgramCost")
            Throw ex
        End Try
    End Function

    Public Function ModifyProgramCost(ByVal objProgramCost As ProgramCostDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objProgramCostData As New TR_PROGRAM_COST With {.ID = objProgramCost.ID}
        Try
            Context.TR_PROGRAM_COST.Attach(objProgramCostData)
            objProgramCostData.COST_COMPANY = objProgramCost.COST_COMPANY
            objProgramCostData.COST_OF_STUDENT = objProgramCost.COST_OF_STUDENT
            objProgramCostData.STUDENT_NUMBER = objProgramCost.STUDENT_NUMBER
            Context.SaveChanges(log)
            gID = objProgramCostData.ID
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".ModifyProgramCost")
            Throw ex
        End Try

    End Function

    Public Function DeleteProgramCost(ByVal lstProgramCost() As ProgramCostDTO) As Boolean
        Dim lstProgramCostData As List(Of TR_PROGRAM_COST)
        Dim lstIDProgramCost As List(Of Decimal) = (From p In lstProgramCost.ToList Select p.ID).ToList
        Try
            lstProgramCostData = (From p In Context.TR_PROGRAM_COST Where lstIDProgramCost.Contains(p.ID)).ToList
            For index = 0 To lstProgramCostData.Count - 1
                Context.TR_PROGRAM_COST.DeleteObject(lstProgramCostData(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteProgramCost")
            Throw ex
        End Try

    End Function

#End Region

#Region "Reimbursement"

    Public Function GetReimbursement(ByVal _filter As ReimbursementDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ReimbursementDTO)

        Try
            Dim query = From p In Context.TR_REIMBURSEMENT
                        From pro In Context.TR_PROGRAM.Where(Function(f) f.ID = p.TR_PROGRAM_ID)
                        From course In Context.TR_COURSE.Where(Function(f) f.ID = pro.TR_COURSE_ID)
                        From emp In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID).DefaultIfEmpty
                        From tit In Context.HU_TITLE.Where(Function(f) f.ID = emp.TITLE_ID).DefaultIfEmpty
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = emp.ORG_ID).DefaultIfEmpty
                        From ter In Context.HU_TERMINATE.Where(Function(f) f.EMPLOYEE_ID = p.EMPLOYEE_ID).DefaultIfEmpty
                        From cm In Context.TR_PROGRAM_COMMIT.Where(Function(f) f.TR_PROGRAM_ID = pro.ID _
                                                                           And f.EMPLOYEE_ID = emp.ID).DefaultIfEmpty()
                        Select New ReimbursementDTO With {.ID = p.ID,
                                                          .TR_PROGRAM_ID = p.TR_PROGRAM_ID,
                                                          .EMPLOYEE_ID = p.EMPLOYEE_ID,
                                                          .EMPLOYEE_CODE = emp.EMPLOYEE_CODE,
                                                          .EMPLOYEE_NAME = emp.FULLNAME_VN,
                                                          .TITLE = tit.NAME_VN,
                                                          .ORG_NAME = org.NAME_VN,
                                                          .YEAR = p.YEAR,
                                                          .TR_PROGRAM_NAME = pro.TR_PROGRAM_CODE & " - " & course.NAME,
                                                          .FROM_DATE = pro.START_DATE,
                                                          .TO_DATE = pro.END_DATE,
                                                          .COST_OF_STUDENT = pro.COST_STUDENT,
                                                          .COMMIT_WORK = cm.COMMIT_WORK,
                                                          .COST_REIMBURSE = p.COST_REIMBURSE,
                                                          .START_DATE = p.START_DATE,
                                                          .IS_RESERVES = p.IS_RESERVES,
                                                          .CREATED_DATE = p.CREATED_DATE,
                                                          .TER_DATE = ter.TER_DATE,
                                                          .COMMIT_END = cm.COMMIT_END,
                                                          .CONVERED_TIME = cm.CONVERED_TIME,
                                                          .REMARK = p.REMARK}

            Dim lst = query
            If _filter.EMPLOYEE_CODE <> "" Then
                lst = lst.Where(Function(p) p.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If
            If _filter.EMPLOYEE_NAME <> "" Then
                lst = lst.Where(Function(p) p.EMPLOYEE_NAME.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If
            If _filter.TR_PROGRAM_NAME <> "" Then
                lst = lst.Where(Function(p) p.TR_PROGRAM_NAME.ToUpper.Contains(_filter.TR_PROGRAM_NAME.ToUpper))
            End If
            If _filter.START_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.START_DATE = _filter.START_DATE)
            End If
            If _filter.YEAR IsNot Nothing Then
                lst = lst.Where(Function(p) p.YEAR = _filter.YEAR)
            End If
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Dim lstRes = lst.ToList
            For Each item In lstRes
                If item.TO_DATE IsNot Nothing And item.START_DATE IsNot Nothing Then
                    item.WORK_AFTER = Math.Round(DateDiff(DateInterval.Day, item.TO_DATE.Value, item.START_DATE.Value) / 30, 1)
                End If
                If item.TER_DATE IsNot Nothing And item.COMMIT_END IsNot Nothing Then
                    item.COMMIT_DAYS_REMAIN = DateDiff(DateInterval.Day, item.TER_DATE.Value, item.COMMIT_END.Value)
                End If
            Next
            Return lstRes
            'Dim lstRes = lst.ToList
            'For Each item In lstRes
            '    Dim lstCost = (From p In Context.TR_PROGRAM_COST Where p.TR_PROGRAM_ID = item.TR_PROGRAM_ID).ToList
            '    If lstCost.Count > 0 Then
            '        Dim costCompany As Decimal = 0
            '        Dim numberStudent As Decimal = 0
            '        Dim costOfStudent As Decimal = 0
            '        For Each item1 In lstCost
            '            If item1.COST_COMPANY IsNot Nothing Then
            '                costCompany += item1.COST_COMPANY
            '            End If
            '            If item1.STUDENT_NUMBER IsNot Nothing Then
            '                numberStudent += item1.STUDENT_NUMBER
            '            End If
            '        Next
            '        item.COST_COMPANY = costCompany
            '        item.STUDENT_NUMBER = numberStudent
            '        If numberStudent <> 0 Then
            '            costOfStudent = Decimal.Round(costCompany / numberStudent, 2)
            '        Else
            '            costOfStudent = 0
            '        End If
            '        item.COST_OF_STUDENT = costOfStudent
            '    End If
            'Next
            'Return lstRes
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetReimbursement")
            Throw ex
        End Try

    End Function
    Public Function GetReimbursementNew(ByVal _filter As ProgramCommitDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        ByVal _param As ParamDTO,
                                        Optional ByVal log As UserLog = Nothing,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ProgramCommitDTO)

        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using
            'Dim query = From p In Context.TR_REIMBURSEMENT
            '            From pro In Context.TR_PROGRAM.Where(Function(f) f.ID = p.TR_PROGRAM_ID)
            '            From course In Context.TR_COURSE.Where(Function(f) f.ID = pro.TR_COURSE_ID)
            '            From emp In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID).DefaultIfEmpty
            '            From tit In Context.HU_TITLE.Where(Function(f) f.ID = emp.TITLE_ID).DefaultIfEmpty
            '            From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = emp.ORG_ID).DefaultIfEmpty
            '            From ter In Context.HU_TERMINATE.Where(Function(f) f.EMPLOYEE_ID = p.EMPLOYEE_ID).DefaultIfEmpty
            '            From cm In Context.TR_PROGRAM_COMMIT.Where(Function(f) f.TR_PROGRAM_ID = pro.ID _
            '                                                               And f.EMPLOYEE_ID = emp.ID).DefaultIfEmpty()

            Dim query = From p In Context.TR_PROGRAM_COMMIT
                        From pro In Context.TR_PROGRAM.Where(Function(f) f.ID = p.TR_PROGRAM_ID)
                        From course In Context.TR_COURSE.Where(Function(f) f.ID = pro.TR_COURSE_ID)
                        From emp In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID).DefaultIfEmpty
                        From tit In Context.HU_TITLE.Where(Function(f) f.ID = emp.TITLE_ID).DefaultIfEmpty
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = emp.ORG_ID).DefaultIfEmpty
                        From ter In Context.HU_TERMINATE.Where(Function(f) f.EMPLOYEE_ID = p.EMPLOYEE_ID).DefaultIfEmpty
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = emp.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper).DefaultIfEmpty
                        Where k.USERNAME.ToUpper = log.Username.ToUpper
                        Select New ProgramCommitDTO With {.ID = p.ID,
                                                          .EMPLOYEE_ID = p.EMPLOYEE_ID,
                                                          .EMPLOYEE_CODE = emp.EMPLOYEE_CODE,
                                                          .EMPLOYEE_NAME = emp.FULLNAME_VN,
                                                          .TITLE_NAME = tit.NAME_VN,
                                                          .ORG_NAME = org.NAME_VN,
                                                          .IS_LOCK = p.IS_LOCK,
                                                          .IS_LOCK_NAME = If(p.IS_LOCK = -1, "X", ""),
                                                          .TER_DATE = ter.TER_DATE,
                                                          .YEAR = pro.YEAR,
                                                          .TR_PROGRAM_NAME = pro.TR_PROGRAM_CODE & "-" & course.NAME,
                                                          .TR_PROGRAM_START_DATE = pro.START_DATE,
                                                          .TR_PROGRAM_END_DATE = pro.END_DATE,
                                                          .CONVERED_TIME = p.CONVERED_TIME,
                                                          .COMMIT_START = p.COMMIT_START,
                                                          .COMMIT_END = p.COMMIT_END,
                                                          .CLOSING_DATE = p.CLOSING_DATE,
                                                          .REIMBURSE_TIME = p.REIMBURSE_TIME,
                                                          .MONEY_REIMBURSE = p.MONEY_REIMBURSE,
                                                          .MONTH_PERIOD = p.MONTH_PERIOD,
                                                          .REIMBURSE_REMARK = p.REIMBURSE_REMARK,
                                                          .CREATED_DATE = p.CREATED_DATE,
                            .TER_EFFECT_DATE = emp.TER_EFFECT_DATE,
                            .WORK_STATUS = emp.WORK_STATUS,
                            .TR_PROGRAM_ID = p.TR_PROGRAM_ID,
                            .MONEY_COMMIT = p.MONEY_COMMIT,
                            .IS_COMMIT = p.IS_COMMIT}

            Dim lst = query
            Dim dateNow = Date.Now.Date
            Dim terID = 257
            If _filter.IS_TER_SEARCH Then

                lst = lst.Where(Function(p) p.TER_EFFECT_DATE IsNot Nothing)

            End If
            If _filter.EMPLOYEE_CODE <> "" Then
                lst = lst.Where(Function(p) p.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If
            If _filter.EMPLOYEE_NAME <> "" Then
                lst = lst.Where(Function(p) p.EMPLOYEE_NAME.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If
            If _filter.TR_PROGRAM_NAME <> "" Then
                lst = lst.Where(Function(p) p.TR_PROGRAM_NAME.ToUpper.Contains(_filter.TR_PROGRAM_NAME.ToUpper))
            End If

            If _filter.YEAR IsNot Nothing Then
                lst = lst.Where(Function(p) p.YEAR = _filter.YEAR)
            End If
            If _filter.COURSE_SEARCH IsNot Nothing Then
                lst = lst.Where(Function(p) p.TR_PROGRAM_ID = _filter.COURSE_SEARCH)
            End If
            If _filter.YEAR_SEARCH IsNot Nothing Then
                lst = lst.Where(Function(p) p.YEAR = _filter.YEAR_SEARCH)
            End If
            If _filter.EMP_CODE_SEARCH <> "" Then
                lst = lst.Where(Function(p) p.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMP_CODE_SEARCH.ToUpper))
            End If

            If _filter.COMMIT_START_T_SEARCH IsNot Nothing Then
                lst = lst.Where(Function(p) p.COMMIT_START >= _filter.COMMIT_START_T_SEARCH)
            End If
            If _filter.COMMIT_START_E_SEARCH IsNot Nothing Then
                lst = lst.Where(Function(p) p.COMMIT_START <= _filter.COMMIT_START_E_SEARCH)
            End If
            If _filter.COMMIT_END_T_SEARCH IsNot Nothing Then
                lst = lst.Where(Function(p) p.COMMIT_END >= _filter.COMMIT_START_E_SEARCH)
            End If
            If _filter.COMMIT_END_E_SEARCH IsNot Nothing Then
                lst = lst.Where(Function(p) p.COMMIT_END <= _filter.COMMIT_END_E_SEARCH)
            End If
            lst = lst.Where(Function(p) p.IS_COMMIT = 1 Or p.IS_COMMIT = -1)
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Dim lstRes = lst.ToList
            Return lstRes
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetReimbursementNew")
            Throw ex
        End Try
    End Function

    Public Function InsertReimbursement(ByVal objReimbursement As ReimbursementDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objReimbursementData As New TR_REIMBURSEMENT
        Try
            objReimbursementData.ID = Utilities.GetNextSequence(Context, Context.TR_REIMBURSEMENT.EntitySet.Name)
            objReimbursementData.YEAR = objReimbursement.YEAR
            objReimbursementData.TR_PROGRAM_ID = objReimbursement.TR_PROGRAM_ID
            objReimbursementData.EMPLOYEE_ID = objReimbursement.EMPLOYEE_ID
            objReimbursementData.START_DATE = objReimbursement.START_DATE
            'If objReimbursement.COST_OF_STUDENT IsNot Nothing Then
            '    'Thời gian cam kết sau đào tạo
            '    Dim CAT = objReimbursement.COMMIT_WORK
            '    'Thời gian đã làm việc sau đào tạo = Ngày bắt đầu bồi hoàn – Ngày kết thức đào tạo
            '    Dim WAT = Math.Round(DateDiff(DateInterval.Day, objReimbursement.TO_DATE.Value, objReimbursement.START_DATE.Value) / 30, 1)
            '    'COST_REIMBURSE = Thời gian đã làm việc sau đào tạo / Thời gian cam kết sau đào tạo * học phí đào tạo
            '    objReimbursementData.COST_REIMBURSE = WAT / CAT * objReimbursement.COST_OF_STUDENT
            'End If
            objReimbursementData.COST_REIMBURSE = objReimbursement.COST_REIMBURSE
            objReimbursementData.REMARK = objReimbursement.REMARK
            objReimbursementData.IS_RESERVES = objReimbursement.IS_RESERVES
            Context.TR_REIMBURSEMENT.AddObject(objReimbursementData)
            Context.SaveChanges(log)
            gID = objReimbursementData.ID
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".InsertReimbursement")
            Throw ex
        End Try
    End Function

    Public Function ValidateReimbursement(ByVal _validate As ReimbursementDTO)
        Dim query
        Try
            If _validate.EMPLOYEE_ID IsNot Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.TR_REIMBURSEMENT
                             Where p.EMPLOYEE_ID = _validate.EMPLOYEE_ID _
                             And p.ID <> _validate.ID _
                             And p.TR_PROGRAM_ID <> _validate.TR_PROGRAM_ID).FirstOrDefault
                Else
                    query = (From p In Context.TR_REIMBURSEMENT
                             Where p.EMPLOYEE_ID = _validate.EMPLOYEE_ID _
                             And p.TR_PROGRAM_ID <> _validate.TR_PROGRAM_ID).FirstOrDefault
                End If

                Return (query Is Nothing)
            End If
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".ValidateReimbursement")
            Throw ex
        End Try
    End Function

    Public Function ModifyReimbursement(ByVal objReimbursement As ReimbursementDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objReimbursementData As New TR_REIMBURSEMENT With {.ID = objReimbursement.ID}
        Try
            Context.TR_REIMBURSEMENT.Attach(objReimbursementData)
            objReimbursementData.YEAR = objReimbursement.YEAR
            objReimbursementData.TR_PROGRAM_ID = objReimbursement.TR_PROGRAM_ID
            objReimbursementData.EMPLOYEE_ID = objReimbursement.EMPLOYEE_ID
            objReimbursementData.START_DATE = objReimbursement.START_DATE
            'If objReimbursement.COST_OF_STUDENT IsNot Nothing Then
            '    'Thời gian cam kết sau đào tạo
            '    Dim CAT = objReimbursement.COMMIT_WORK
            '    'Thời gian đã làm việc sau đào tạo = Ngày bắt đầu bồi hoàn – Ngày kết thức đào tạo
            '    Dim WAT = Math.Round(DateDiff(DateInterval.Day, objReimbursement.TO_DATE.Value, objReimbursement.START_DATE.Value) / 30, 1)
            '    'COST_REIMBURSE = Thời gian đã làm việc sau đào tạo / Thời gian cam kết sau đào tạo * học phí đào tạo
            '    objReimbursementData.COST_REIMBURSE = WAT / CAT * objReimbursement.COST_OF_STUDENT
            'End If
            objReimbursementData.COST_REIMBURSE = objReimbursement.COST_REIMBURSE
            objReimbursementData.REMARK = objReimbursement.REMARK
            objReimbursementData.IS_RESERVES = objReimbursement.IS_RESERVES
            Context.SaveChanges(log)
            gID = objReimbursementData.ID
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".ModifyReimbursement")
            Throw ex
        End Try
    End Function
    Public Function ActiveReimbursement(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As Boolean) As Boolean
        Dim lstData As List(Of TR_PROGRAM_COMMIT)
        Try
            lstData = (From p In Context.TR_PROGRAM_COMMIT Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                lstData(index).IS_LOCK = bActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".ActiveReimbursement")
            Throw ex
        End Try
    End Function
    Public Function ModifyProgramCommit(ByVal objProgramCommit As ProgramCommitDTO, ByVal log As UserLog) As Boolean
        Dim objProgramCommitData As New TR_PROGRAM_COMMIT With {.ID = objProgramCommit.ID}
        Try
            Context.TR_PROGRAM_COMMIT.Attach(objProgramCommitData)
            objProgramCommitData.CLOSING_DATE = objProgramCommit.CLOSING_DATE
            objProgramCommitData.REIMBURSE_TIME = objProgramCommit.REIMBURSE_TIME
            objProgramCommitData.MONEY_REIMBURSE = objProgramCommit.MONEY_REIMBURSE
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".ModifyProgramCommit")
            Throw ex
        End Try
    End Function
    Public Function FastUpdateProgramCommit(ByVal objProgramCommit As ProgramCommitDTO, ByVal log As UserLog) As Boolean
        Dim objProgramCommitData As New TR_PROGRAM_COMMIT With {.ID = objProgramCommit.ID}
        Try
            Context.TR_PROGRAM_COMMIT.Attach(objProgramCommitData)
            objProgramCommitData.MONTH_PERIOD = objProgramCommit.MONTH_PERIOD
            objProgramCommitData.REIMBURSE_REMARK = objProgramCommit.REIMBURSE_REMARK
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".FastUpdateProgramCommit")
            Throw ex
        End Try
    End Function
    Public Function DeleteReimbursement(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim lstData As List(Of TR_PROGRAM_COMMIT)
        Try
            lstData = (From p In Context.TR_PROGRAM_COMMIT Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                lstData(index).CLOSING_DATE = Nothing
                lstData(index).REIMBURSE_TIME = Nothing
                lstData(index).MONEY_REIMBURSE = Nothing
                lstData(index).MONTH_PERIOD = Nothing
                lstData(index).REIMBURSE_REMARK = Nothing
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".ActiveReimbursement")
            Throw ex
        End Try
    End Function
#End Region

#Region "ChooseForm"

    Public Function GetChooseForm(ByVal _filter As ChooseFormDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ChooseFormDTO)

        Try
            Dim query = From p In Context.TR_CHOOSE_FORM
                        From pro In Context.TR_PROGRAM.Where(Function(f) f.ID = p.TR_PROGRAM_ID)
                        From course In Context.TR_COURSE.Where(Function(f) f.ID = pro.TR_COURSE_ID)
                        From form In Context.TR_ASSESSMENT_FORM.Where(Function(f) f.ID = p.TR_ASSESSMENT_FORM_ID)
                        Select New ChooseFormDTO With {
                            .ID = p.ID,
                            .YEAR = p.YEAR,
                            .TR_PROGRAM_ID = p.TR_PROGRAM_ID,
                            .TR_PROGRAM_NAME = pro.TR_PROGRAM_CODE & " - " & course.NAME,
                            .TR_ASSESSMENT_FORM_NAME = form.NAME,
                            .TR_ASSESSMENT_FORM_ID = p.TR_ASSESSMENT_FORM_ID,
                            .REMARK = p.REMARK,
                            .CREATED_DATE = p.CREATED_DATE}

            Dim lst = query
            If _filter.TR_ASSESSMENT_FORM_NAME <> "" Then
                lst = lst.Where(Function(p) p.TR_ASSESSMENT_FORM_NAME.ToUpper.Contains(_filter.TR_ASSESSMENT_FORM_NAME.ToUpper))
            End If
            If _filter.REMARK <> "" Then
                lst = lst.Where(Function(p) p.REMARK.ToUpper.Contains(_filter.REMARK.ToUpper))
            End If
            If _filter.TR_PROGRAM_NAME <> "" Then
                lst = lst.Where(Function(p) p.TR_PROGRAM_NAME.ToUpper.Contains(_filter.TR_PROGRAM_NAME.ToUpper))
            End If
            If _filter.YEAR IsNot Nothing Then
                lst = lst.Where(Function(p) p.YEAR = _filter.YEAR)
            End If
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetChooseForm")
            Throw ex
        End Try

    End Function

    Public Function InsertChooseForm(ByVal objChooseForm As ChooseFormDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objChooseFormData As New TR_CHOOSE_FORM
        Try
            objChooseFormData.ID = Utilities.GetNextSequence(Context, Context.TR_CHOOSE_FORM.EntitySet.Name)
            objChooseFormData.TR_ASSESSMENT_FORM_ID = objChooseForm.TR_ASSESSMENT_FORM_ID
            objChooseFormData.REMARK = objChooseForm.REMARK
            objChooseFormData.TR_PROGRAM_ID = objChooseForm.TR_PROGRAM_ID
            objChooseFormData.YEAR = objChooseForm.YEAR
            Context.TR_CHOOSE_FORM.AddObject(objChooseFormData)
            Context.SaveChanges(log)
            gID = objChooseFormData.ID
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".InsertChooseForm")
            Throw ex
        End Try
    End Function

    Public Function ValidateChooseForm(ByVal _validate As ChooseFormDTO)
        Dim query
        Try
            If _validate.TR_PROGRAM_ID IsNot Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.TR_CHOOSE_FORM
                             Where p.ID <> _validate.ID _
                             And p.TR_PROGRAM_ID = _validate.TR_PROGRAM_ID).FirstOrDefault
                Else
                    query = (From p In Context.TR_CHOOSE_FORM
                             Where p.TR_PROGRAM_ID = _validate.TR_PROGRAM_ID).FirstOrDefault
                End If

                Return (query Is Nothing)
            End If
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".ValidateChooseForm")
            Throw ex
        End Try
    End Function

    Public Function ModifyChooseForm(ByVal objChooseForm As ChooseFormDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objChooseFormData As New TR_CHOOSE_FORM With {.ID = objChooseForm.ID}
        Try
            Context.TR_CHOOSE_FORM.Attach(objChooseFormData)
            objChooseFormData.TR_ASSESSMENT_FORM_ID = objChooseForm.TR_ASSESSMENT_FORM_ID
            objChooseFormData.REMARK = objChooseForm.REMARK
            objChooseFormData.TR_PROGRAM_ID = objChooseForm.TR_PROGRAM_ID
            objChooseFormData.YEAR = objChooseForm.YEAR
            Context.SaveChanges(log)
            gID = objChooseFormData.ID
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".ModifyChooseForm")
            Throw ex
        End Try

    End Function

    Public Function DeleteChooseForm(ByVal lst() As ChooseFormDTO) As Boolean
        Dim lstProgramCostData As List(Of TR_CHOOSE_FORM)
        Dim lstID As List(Of Decimal) = (From p In lst.ToList Select p.ID).ToList
        Try
            lstProgramCostData = (From p In Context.TR_CHOOSE_FORM Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstProgramCostData.Count - 1
                Context.TR_CHOOSE_FORM.DeleteObject(lstProgramCostData(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteChooseFrom")
            Throw ex
        End Try

    End Function

#End Region

#Region "AssessmentResult"

    Public Function GetEmployeeAssessmentResult(ByVal _filter As AssessmentResultDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "EMPLOYEE_CODE desc") As List(Of AssessmentResultDTO)

        Try
            'Dim query = From student In Context.TR_CLASS_STUDENT
            '            From choose In Context.TR_CHOOSE_FORM.Where(Function(f) f.ID = _filter.TR_CHOOSE_FORM_ID)
            '            From cls In Context.TR_CLASS.Where(Function(f) f.ID = student.TR_CLASS_ID _
            '                                                   And f.TR_PROGRAM_ID = choose.TR_PROGRAM_ID)
            '            From emp In Context.HU_EMPLOYEE.Where(Function(f) f.ID = student.EMPLOYEE_ID)
            '            From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = emp.ORG_ID).DefaultIfEmpty
            '            From tit In Context.HU_TITLE.Where(Function(f) f.ID = emp.TITLE_ID).DefaultIfEmpty
            '            Select New AssessmentResultDTO With {
            '                .EMPLOYEE_ID = emp.ID,
            '                .EMPLOYEE_CODE = emp.EMPLOYEE_CODE,
            '                .EMPLOYEE_NAME = emp.FULLNAME_VN,
            '                .ORG_NAME = org.NAME_VN,
            '                .TITLE_NAME = tit.NAME_VN}
            Dim query = From student In Context.TR_PROGRAM_EMP
                        From ar In Context.TR_ASSESSMENT_RESULT.Where(Function(f) f.TR_PROGRAM_ID = student.TR_PROGRAM_ID AndAlso f.EMPLOYEE_ID = student.EMP_ID).DefaultIfEmpty
                        From emp In Context.HU_EMPLOYEE.Where(Function(f) f.ID = student.EMP_ID).DefaultIfEmpty
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = emp.ORG_ID).DefaultIfEmpty
                        From tit In Context.HU_TITLE.Where(Function(f) f.ID = emp.TITLE_ID).DefaultIfEmpty
                        Where student.TR_PROGRAM_ID = _filter.TR_CHOOSE_FORM_ID
                        Select New AssessmentResultDTO With {
                            .EMPLOYEE_ID = emp.ID,
                            .EMPLOYEE_CODE = emp.EMPLOYEE_CODE,
                            .EMPLOYEE_NAME = emp.FULLNAME_VN,
                            .ORG_NAME = org.NAME_VN,
                            .TITLE_NAME = tit.NAME_VN,
                            .ASSESMENT_ID = ar.ID,
                            .STATUS = If(Not ar.EMPLOYEE_ID.HasValue, "Chưa đánh giá", "Đã đánh giá"),
                            .IS_LOCK = ar.IS_LOCK,
                            .IS_LOCK_TEXT = If(ar.IS_LOCK.Value = -1, "X", "")}

            Dim lst = query.Distinct

            If _filter.EMPLOYEE_CODE <> "" Then
                lst = lst.Where(Function(p) p.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If

            If _filter.EMPLOYEE_NAME <> "" Then
                lst = lst.Where(Function(p) p.EMPLOYEE_NAME.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If
            If _filter.ORG_NAME <> "" Then
                lst = lst.Where(Function(p) p.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If
            If _filter.TITLE_NAME <> "" Then
                lst = lst.Where(Function(p) p.TITLE_NAME.ToUpper.Contains(_filter.TITLE_NAME.ToUpper))
            End If
            If _filter.IS_LOCK_TEXT <> "" Then
                lst = lst.Where(Function(p) p.IS_LOCK_TEXT.ToUpper.Contains(_filter.IS_LOCK_TEXT.ToUpper))
            End If
            If _filter.STATUS <> "" Then
                lst = lst.Where(Function(p) p.STATUS.ToUpper.Contains(_filter.STATUS.ToUpper))
            End If
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetEmployeeAssessmentResult")
            Throw ex
        End Try

    End Function

    Public Function GetAssessmentResultByID(ByVal _filter As TR_CriteriaDTO) As List(Of TR_CriteriaDTO)

        Try
            Dim lst As New List(Of TR_CriteriaDTO)
            If _filter.TR_PROGRAM_ID IsNot Nothing AndAlso _filter.EMPLOYEE_ID IsNot Nothing Then
                Using cls As New DataAccess.QueryData
                    Dim dsData As DataSet = cls.ExecuteStore("PKG_TRAINING.GET_ASSESSMENT_RESULT",
                                               New With {.P_PROGRAM_ID = _filter.TR_PROGRAM_ID,
                                                         .P_EMPLOYEE_ID = _filter.EMPLOYEE_ID,
                                                         .P_CUR = cls.OUT_CURSOR}, False)
                    Dim dt As DataTable = dsData.Tables(0)
                    Dim nullDec As New Decimal?
                    nullDec = Nothing
                    If dt IsNot Nothing Then
                        lst = (From row As DataRow In dt.Rows
                               Select New TR_CriteriaDTO With {.ID = CDec(If(row("ID").ToString = "", Nothing, row("ID"))),
                                                               .TR_CRITERIA_CODE = row("TR_CRITERIA_CODE").ToString(),
                                                               .TR_CRITERIA_ID = If(row("TR_CRITERIA_ID").ToString = "", nullDec, CDec(row("TR_CRITERIA_ID"))),
                                                               .TR_CRITERIA_NAME = row("TR_CRITERIA_NAME").ToString(),
                                                               .TR_CRITERIA_POINT_MAX = If(row("TR_CRITERIA_POINT_MAX").ToString = "", nullDec, CDec(row("TR_CRITERIA_POINT_MAX"))),
                                                               .TR_CRITERIA_RATIO = If(row("TR_CRITERIA_RATIO").ToString = "", nullDec, CDec(row("TR_CRITERIA_RATIO"))),
                                                               .POINT_ASS = If(row("POINT_ASS").ToString = "", nullDec, CDec(row("POINT_ASS"))),
                                                               .CRI_COURSE_ID = If(row("CRI_COURSE_ID").ToString = "", nullDec, CDec(row("CRI_COURSE_ID"))),
                                                               .REMARK = row("REMARK").ToString(),
                                                               .NOTE1 = row("NOTE1").ToString(),
                                                               .NOTE2 = row("NOTE2").ToString(),
                                                               .NOTE3 = row("NOTE3").ToString(),
                                                               .NOTE4 = row("NOTE4").ToString()}).ToList
                    End If
                End Using
            End If
            'Dim query = From d In Context.TR_SETTING_CRI_DETAIL
            '            From c In (Context.TR_SETTING_CRI_COURSE.Where(Function(f) f.ID = d.COURSE_ID))
            '            From p In Context.TR_PROGRAM.Where(Function(f) f.TR_COURSE_ID = c.TR_COURSE_ID)
            '            From cr In Context.TR_CRITERIA.Where(Function(f) f.ID = d.CRITERIA_ID)
            '            From dtl In (From dt In Context.TR_SETTING_CRI_DETAIL
            '                         From rs In Context.TR_ASSESSMENT_RESULT.Where(Function(f) f.CRI_COURSE_ID = dt.COURSE_ID And f.EMPLOYEE_ID = _filter.EMPLOYEE_ID)
            '                         From dtl In Context.TR_ASSESSMENT_RESULT_DTL.Where(Function(f) f.TR_ASSESSMENT_RESULT_ID = rs.ID And f.TR_CRITERIA_ID = dt.CRITERIA_ID).DefaultIfEmpty
            '                         Where rs.CRI_COURSE_ID = c.ID And d.CRITERIA_ID = dtl.TR_CRITERIA_ID
            '                         Select dtl).DefaultIfEmpty
            '            Where (p.ID = _filter.TR_PROGRAM_ID)
            '            Order By d.RATIO Ascending
            '            Select New TR_CriteriaDTO With {
            '                .ID = c.ID,
            '                .TR_CRITERIA_CODE = cr.CODE,
            '                .TR_CRITERIA_ID = cr.ID,
            '                .TR_CRITERIA_NAME = cr.NAME,
            '                .TR_CRITERIA_POINT_MAX = d.POINT_MAX,
            '                .TR_CRITERIA_RATIO = d.RATIO,
            '                .POINT_ASS = dtl.POINT_ASS,
            '                .CRI_COURSE_ID = c.ID,
            '                .REMARK = dtl.REMARK}

            Return lst
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetAssessmentResultByID")
            Throw ex
        End Try

    End Function

    Public Function GetAssessmentResultByID_Portal(ByVal _filter As TR_CriteriaDTO) As List(Of TR_CriteriaDTO)

        Try
            Dim lst As New List(Of TR_CriteriaDTO)
            If _filter.TR_PROGRAM_ID IsNot Nothing AndAlso _filter.EMPLOYEE_ID IsNot Nothing Then
                Using cls As New DataAccess.QueryData
                    Dim dsData As DataSet = cls.ExecuteStore("PKG_TRAINING.GET_ASSESSMENT_RESULT",
                                               New With {.P_PROGRAM_ID = _filter.TR_PROGRAM_ID,
                                                         .P_EMPLOYEE_ID = _filter.EMPLOYEE_ID,
                                                         .P_CUR = cls.OUT_CURSOR}, False)
                    Dim dt As DataTable = dsData.Tables(0)
                    Dim nullDec As New Decimal?
                    nullDec = Nothing
                    If dt IsNot Nothing Then
                        lst = (From row As DataRow In dt.Rows
                               Select New TR_CriteriaDTO With {.ID = CDec(If(row("ID").ToString = "", Nothing, row("ID"))),
                                                               .TR_CRITERIA_CODE = row("TR_CRITERIA_CODE").ToString(),
                                                               .TR_CRITERIA_ID = If(row("TR_CRITERIA_ID").ToString = "", nullDec, CDec(row("TR_CRITERIA_ID"))),
                                                               .TR_CRITERIA_NAME = row("TR_CRITERIA_NAME").ToString(),
                                                               .TR_CRITERIA_POINT_MAX = If(row("TR_CRITERIA_POINT_MAX").ToString = "", nullDec, CDec(row("TR_CRITERIA_POINT_MAX"))),
                                                               .TR_CRITERIA_RATIO = If(row("TR_CRITERIA_RATIO").ToString = "", nullDec, CDec(row("TR_CRITERIA_RATIO"))),
                                                               .POINT_ASS = If(row("POINT_ASS").ToString = "", nullDec, CDec(row("POINT_ASS"))),
                                                               .CRI_COURSE_ID = If(row("CRI_COURSE_ID").ToString = "", nullDec, CDec(row("CRI_COURSE_ID"))),
                                                               .REMARK = row("REMARK").ToString(),
                                                               .NOTE1 = row("NOTE1").ToString(),
                                                               .NOTE2 = row("NOTE2").ToString(),
                                                               .NOTE3 = row("NOTE3").ToString(),
                                                               .NOTE4 = row("NOTE4").ToString()}).ToList
                    End If
                End Using
            End If
            Return lst
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetAssessmentResultByID_Portal")
            Throw ex
        End Try

    End Function

    Public Function GET_DTB(ByVal sType As Decimal, ByVal sEMP As Decimal) As Decimal
        Dim dt As DataTable
        Dim DTB As Decimal
        Using cls As New DataAccess.QueryData
            dt = cls.ExecuteStore("PKG_COMMON_LIST.GET_DTB",
                              New With {.P_DTB = sType,
                                        .P_EMP_ID = sEMP,
                                        .P_CUR = cls.OUT_CURSOR})
        End Using
        If Not IsDBNull(dt.Rows(0).Item(0)) Then
            DTB = Decimal.Parse(dt.Rows(0).Item(0).ToString())
        Else
            DTB = -1
        End If
        Return DTB

    End Function

    Public Function GET_DTB_PORTAL(ByVal sType As Decimal, ByVal sEMP As Decimal, ByVal sPROID As Decimal) As Decimal
        Dim dt As DataTable
        Dim DTB As Decimal
        Using cls As New DataAccess.QueryData
            dt = cls.ExecuteStore("PKG_COMMON_LIST.GET_DTB_PORTAL",
                              New With {.P_DTB = sType,
                                        .P_EMP_ID = sEMP,
                                        .P_PRO_ID = sPROID,
                                        .P_CUR = cls.OUT_CURSOR})
        End Using
        If Not IsDBNull(dt.Rows(0).Item(0)) Then
            DTB = Decimal.Parse(dt.Rows(0).Item(0).ToString())
        Else
            DTB = -1
        End If
        Return DTB

    End Function

    Public Function UpdateAssessmentResult(ByVal obj As AssessmentResultDTO,
                                   ByVal log As UserLog) As Boolean
        Dim assessmentResultID = 0
        Try
            Dim objData = (From p In Context.TR_ASSESSMENT_RESULT
                           Where p.CRI_COURSE_ID = obj.CRI_COURSE_ID _
                           And p.EMPLOYEE_ID = obj.EMPLOYEE_ID _
                           And p.TR_PROGRAM_ID = obj.TR_PROGRAM_ID
                           Select p).FirstOrDefault

            If objData Is Nothing Then
                objData = New TR_ASSESSMENT_RESULT
                assessmentResultID = Utilities.GetNextSequence(Context, Context.TR_ASSESSMENT_RESULT.EntitySet.Name)
                With objData
                    .ID = assessmentResultID
                    .CRI_COURSE_ID = obj.CRI_COURSE_ID
                    .EMPLOYEE_ID = obj.EMPLOYEE_ID
                    .TR_PROGRAM_ID = obj.TR_PROGRAM_ID
                    Context.TR_ASSESSMENT_RESULT.AddObject(objData)
                End With
            Else
                assessmentResultID = objData.ID
            End If

            Dim objNote = (From p In Context.TR_ASSESSMENT_NOTE
                           Where p.EMPLOYEE_COURSE_ID = assessmentResultID
                           Select p).FirstOrDefault

            If objNote Is Nothing Then
                objNote = New TR_ASSESSMENT_NOTE
                With objNote
                    .ID = Utilities.GetNextSequence(Context, Context.TR_ASSESSMENT_NOTE.EntitySet.Name)
                    .EMPLOYEE_COURSE_ID = assessmentResultID
                    .NOTE1 = obj.NOTE1
                    .NOTE2 = obj.NOTE2
                    .NOTE3 = obj.NOTE3
                    .NOTE4 = obj.NOTE4
                    Context.TR_ASSESSMENT_NOTE.AddObject(objNote)
                End With
            Else
                objNote.NOTE1 = obj.NOTE1
                objNote.NOTE2 = obj.NOTE2
                objNote.NOTE3 = obj.NOTE3
                objNote.NOTE4 = obj.NOTE4
            End If

            Dim lstDtl = (From p In Context.TR_ASSESSMENT_RESULT_DTL
                          Where p.TR_ASSESSMENT_RESULT_ID = objData.ID
                          Select p).ToList

            For Each item In lstDtl
                Context.TR_ASSESSMENT_RESULT_DTL.DeleteObject(item)
            Next

            For Each item In obj.lstResult
                Dim objDtl = New TR_ASSESSMENT_RESULT_DTL
                objDtl.ID = Utilities.GetNextSequence(Context, Context.TR_ASSESSMENT_RESULT_DTL.EntitySet.Name)
                objDtl.POINT_ASS = item.POINT_ASS
                objDtl.REMARK = item.REMARK
                objDtl.TR_ASSESSMENT_RESULT_ID = objData.ID
                objDtl.TR_CRITERIA_ID = item.TR_CRITERIA_ID
                objDtl.TR_CRITERIA_GROUP_ID = item.TR_CRITERIA_GROUP_ID
                Context.TR_ASSESSMENT_RESULT_DTL.AddObject(objDtl)
            Next

            Context.SaveChanges(log)

            Return True
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function UpdateAssessmentResult_Portal(ByVal obj As AssessmentResultDTO, ByVal log As UserLog) As Boolean
        Try
            Dim objData = (From p In Context.TR_ASSESSMENT_RESULT
                           Where p.TR_CHOOSE_FORM_ID = obj.TR_CHOOSE_FORM_ID _
                           And p.EMPLOYEE_ID = obj.EMPLOYEE_ID
                           Select p).FirstOrDefault

            If objData Is Nothing Then
                objData = New TR_ASSESSMENT_RESULT
                With objData
                    .ID = Utilities.GetNextSequence(Context, Context.TR_ASSESSMENT_RESULT.EntitySet.Name)
                    .TR_CHOOSE_FORM_ID = obj.TR_CHOOSE_FORM_ID
                    .EMPLOYEE_ID = obj.EMPLOYEE_ID
                    Context.TR_ASSESSMENT_RESULT.AddObject(objData)
                End With
            End If

            Dim lstDtl = (From p In Context.TR_ASSESSMENT_RESULT_DTL
                          Where p.TR_ASSESSMENT_RESULT_ID = objData.ID
                          Select p).ToList

            For Each item In lstDtl
                Context.TR_ASSESSMENT_RESULT_DTL.DeleteObject(item)
            Next

            For Each item In obj.lstResult
                Dim objDtl = New TR_ASSESSMENT_RESULT_DTL
                objDtl.ID = Utilities.GetNextSequence(Context, Context.TR_ASSESSMENT_RESULT_DTL.EntitySet.Name)
                objDtl.POINT_ASS = item.POINT_ASS
                objDtl.REMARK = item.REMARK
                objDtl.TR_ASSESSMENT_RESULT_ID = objData.ID
                objDtl.TR_CRITERIA_GROUP_ID = item.TR_CRITERIA_GROUP_ID
                objDtl.TR_CRITERIA_ID = item.TR_CRITERIA_ID
                objDtl.TR_CHOOSE_FORM_ID = item.TR_CHOOSE_FORM_ID
                Context.TR_ASSESSMENT_RESULT_DTL.AddObject(objDtl)
            Next

            Context.SaveChanges(log)

            Return True
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function ChangeStatusAssessmentResult(ByVal lstID As List(Of Decimal), ByVal _status As Decimal, ByVal _log As UserLog) As Boolean
        Try
            Dim lstObj = From p In Context.TR_ASSESSMENT_RESULT Where lstID.Contains(p.ID)

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

#End Region

#Region "EmployeeRecord"

    Public Function GetListEmployeePaging(ByVal _filter As RecordEmployeeDTO,
                                              ByVal PageIndex As Integer,
                                              ByVal PageSize As Integer,
                                              ByRef Total As Integer, ByVal _param As ParamDTO,
                                              Optional ByVal Sorts As String = "EMPLOYEE_CODE desc",
                                              Optional ByVal log As UserLog = Nothing) As List(Of RecordEmployeeDTO)
        Try
            'Using cls As New DataAccess.QueryData
            '    cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
            '                     New With {.P_USERNAME = log.Username,
            '                               .P_ORGID = _param.ORG_ID,
            '                               .P_ISDISSOLVE = _param.IS_DISSOLVE})
            'End Using
            Dim str = ""
            Dim query = From p In Context.TR_PROGRAM_EMP
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMP_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From pro In Context.TR_PROGRAM.Where(Function(f) f.ID = p.TR_PROGRAM_ID).DefaultIfEmpty
                        From c In Context.TR_COURSE.Where(Function(f) f.ID = pro.TR_COURSE_ID).DefaultIfEmpty
                        From r In Context.TR_PROGRAM_RESULT.Where(Function(f) f.EMPLOYEE_ID = e.ID And f.TR_PROGRAM_ID = pro.ID).DefaultIfEmpty
                        From rank In Context.OT_OTHER_LIST.Where(Function(f) f.ID = r.TR_RANK_ID).DefaultIfEmpty
                        From pc In Context.TR_PROGRAM_COMMIT.Where(Function(f) f.TR_PROGRAM_ID = pro.ID And f.EMPLOYEE_ID = e.ID).DefaultIfEmpty
            Dim lst = query.Select(Function(p) New RecordEmployeeDTO With {
                                         .EMPLOYEE_ID = p.e.ID,
                                         .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                         .FULLNAME_VN = p.e.FULLNAME_VN,
                                         .TITLE_NAME_VN = p.t.NAME_VN,
                                         .ORG_NAME = p.o.NAME_VN,
                                         .TR_COURSE_ID = p.c.ID,
                                         .TR_COURSE_NAME = p.c.NAME,
                                         .FROM_DATE = p.pro.START_DATE,
                                         .TO_DATE = p.pro.END_DATE,
                                         .CONTENT = p.pro.CONTENT,
                                         .TARGET_TRAIN = p.pro.TARGET_TRAIN,
                                         .VENUE = p.pro.VENUE,
                                         .IS_REACH = If(p.r.IS_REACH = -1, "Đạt", "Không đạt"),
                                         .IS_REACHED = If(p.r.IS_REACH = -1, True, False),
                                         .FINAL_SCORE = p.r.FINAL_SCORE,
                                         .TR_RANK_NAME = p.rank.NAME_VN,
                                         .COMMENT_1 = p.r.COMMENT_1,
                                         .COMMENT_2 = p.r.COMMENT_2,
                                         .COMMENT_3 = p.r.COMMENT_3,
                                         .CERTIFICATE_NAME = If(p.r.INSERT_HSNV = 1, p.pro.CERTIFICATE_NAME, str),
                                         .CERTIFICATE_DATE = p.r.CERT_DATE,
                                         .COMMIT_NO = p.pc.COMMIT_NO,
                                         .MONEY_COMMIT = p.pc.MONEY_COMMIT,
                                         .COMMIT_WORK = p.pc.COMMIT_WORK,
                                         .COMMIT_START = p.pc.COMMIT_START,
                                         .COMMIT_END = p.pc.COMMIT_END,
                                         .WORK_STATUS = p.e.WORK_STATUS,
                                         .TER_EFFECT_DATE = p.e.TER_EFFECT_DATE,
                                         .TER_LAST_DATE = p.e.TER_LAST_DATE,
                                         .TR_PROGRAM_ID = p.pro.ID})
            Dim dateNow = Date.Now.Date
            If _filter.IS_TER Then
                lst = lst.Where(Function(p) (p.WORK_STATUS.HasValue And p.WORK_STATUS = 257 And (p.TER_EFFECT_DATE <= dateNow Or p.TER_LAST_DATE <= dateNow)))
            End If

            If _filter.EMPLOYEE_CODE <> "" Then
                'lst = lst.Where(Function(p) p.EMPLOYEE_CODE.ToUpper().IndexOf(_filter.EMPLOYEE_CODE.ToUpper) >= 0)
                'ThanhNT added lấy full thông tin (Mã/Tên/CMND/Mã cũ)
                lst = lst.Where(Function(p) p.EMPLOYEE_CODE.ToUpper.IndexOf(_filter.EMPLOYEE_CODE.ToUpper) >= 0 Or
                                            p.FULLNAME_VN.ToUpper.IndexOf(_filter.EMPLOYEE_CODE.ToUpper) >= 0)
            End If
            If _filter.ORG_NAME <> "" Then
                lst = lst.Where(Function(p) p.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If

            If _filter.FROM_DATE IsNot Nothing AndAlso _filter.TO_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.TO_DATE >= _filter.FROM_DATE And p.FROM_DATE <= _filter.TO_DATE)
            Else
                If _filter.FROM_DATE IsNot Nothing Then
                    lst = lst.Where(Function(p) p.TO_DATE <= _filter.FROM_DATE)
                End If
                If _filter.TO_DATE IsNot Nothing Then
                    lst = lst.Where(Function(p) p.FROM_DATE <= _filter.TO_DATE)
                End If
            End If
            'Lọc theo khoa dao tao
            If _filter.TR_COURSE_ID.ToString() <> "" Then
                lst = lst.Where(Function(p) p.TR_COURSE_ID = _filter.TR_COURSE_ID)
            End If
            'Lọc theo nhom chuong trinh
            If _filter.TR_PROGRAM_GROUP_ID.ToString() <> "" Then
                lst = lst.Where(Function(p) p.TR_PROGRAM_GROUP_ID = _filter.TR_PROGRAM_GROUP_ID)
            End If

            'Lọc theo linh vuc dao tao
            If (_filter.FIELDS_ID IsNot Nothing) Then
                lst = lst.Where(Function(p) p.FIELDS_ID = _filter.FIELDS_ID)
            End If
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Dim rs = lst.ToList
            For Each item As RecordEmployeeDTO In rs
                Dim centers As String = ""
                Dim programID = item.TR_PROGRAM_ID
                Dim lstCenter = (From p In Context.TR_PROGRAM_CENTER
                                 From c In Context.TR_CENTER.Where(Function(f) f.ID = p.TR_CENTER_ID)
                                 Where p.TR_PROGRAM_ID = programID
                                 Select c.NAME_VN).ToList.ToArray
                If lstCenter.Length > 0 Then
                    centers = String.Join("; ", lstCenter)
                End If
                item.Centers_NAME = centers
            Next
            Return rs
        Catch ex As Exception
            WriteExceptionLog(ex, Me.ToString() & ".EmployeeRecord")
            Throw ex
        End Try
    End Function
    Public Function GetEmployeeRecord(ByVal _filter As RecordEmployeeDTO,
                                              ByVal PageIndex As Integer,
                                              ByVal PageSize As Integer,
                                              ByRef Total As Integer, ByVal _param As ParamDTO,
                                              Optional ByVal Sorts As String = "EMPLOYEE_CODE desc",
                                              Optional ByVal log As UserLog = Nothing) As List(Of RecordEmployeeDTO)
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using

            Dim query = From re In Context.TR_REQUEST_EMPLOYEE
                        From r In Context.TR_REQUEST.Where(Function(f) f.ID = re.TR_REQUEST_ID).DefaultIfEmpty
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = re.EMPLOYEE_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From o In Context.OT_OTHER_LIST.Where(Function(f) f.ID = e.WORK_STATUS).DefaultIfEmpty
                        From c In Context.TR_COURSE.Where(Function(f) f.ID = r.TR_COURSE_ID).DefaultIfEmpty
                        From ce In Context.TR_CERTIFICATE.Where(Function(f) f.ID = c.TR_CERTIFICATE_ID)
                        From pr In Context.TR_PROGRAM.Where(Function(f) f.TR_REQUEST_ID = r.ID).DefaultIfEmpty
                        From prg In Context.TR_PROGRAM_GROUP.Where(Function(f) f.ID = c.TR_PROGRAM_GROUP).DefaultIfEmpty
                        From tf In Context.OT_OTHER_LIST.Where(Function(f) f.ID = c.TR_TRAIN_FIELD).DefaultIfEmpty
                        From tfr In Context.OT_OTHER_LIST.Where(Function(f) f.ID = r.TRAIN_FORM_ID).DefaultIfEmpty
                        From lang In Context.OT_OTHER_LIST.Where(Function(f) f.ID = pr.TR_LANGUAGE_ID).DefaultIfEmpty
                        From result In Context.TR_PROGRAM_RESULT.Where(Function(f) f.TR_PROGRAM_ID = pr.ID And f.EMPLOYEE_ID = e.ID).DefaultIfEmpty
                        From rank In Context.OT_OTHER_LIST.Where(Function(f) f.ID = result.TR_RANK_ID).DefaultIfEmpty
                        From pcomit In Context.TR_PROGRAM_COMMIT.Where(Function(f) f.EMPLOYEE_ID = e.ID And f.TR_PROGRAM_ID = pr.ID).DefaultIfEmpty
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = r.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper).DefaultIfEmpty
                        Where k.USERNAME.ToUpper = log.Username.ToUpper
                        Order By re.ID Descending, e.EMPLOYEE_CODE

            Dim lst = query.Select(Function(p) New RecordEmployeeDTO With {
                                         .ID = p.e.ID,
                                         .EMPLOYEE_ID = p.e.ID,
                                         .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                         .FULLNAME_VN = p.e.FULLNAME_VN,
                                         .TITLE_ID = p.t.ID,
                                         .TITLE_NAME_VN = p.t.NAME_VN,
                                         .ORG_ID = p.org.ID,
                                         .ORG_NAME = p.org.NAME_VN,
                                         .ORG_DESC = p.org.DESCRIPTION_PATH,
                                         .WORK_STATUS = p.e.WORK_STATUS,
                                         .WORK_STATUS_NAME = p.o.NAME_VN,
                                         .TR_COURSE_ID = p.r.TR_COURSE_ID,
                                         .TR_COURSE_NAME = p.c.NAME,
                                         .TR_PROGRAM_ID = p.pr.ID,
                                         .TR_PROGRAM_NAME = p.pr.NAME,
                                         .TR_PROGRAM_GROUP_ID = p.prg.ID,
                                         .TR_PROGRAM_GROUP_NAME = p.prg.NAME,
                                         .FIELDS_ID = p.tf.ID,
                                         .TR_TRAIN_FIELD_NAME = p.tf.NAME_VN,
                                         .TR_TRAIN_FORM_ID = p.r.TRAIN_FORM_ID,
                                         .TR_TRAIN_FORM_NAME = p.tfr.NAME_VN,
                                         .DURATION = p.pr.DURATION,
                                         .START_DATE = p.pr.START_DATE,
                                         .END_DATE = p.pr.END_DATE,
                                         .DURATION_HC = p.pr.DURATION_HC,
                                         .DURATION_OT = p.pr.DURATION_OT,
                                         .COST_TOTAL = p.pr.COST_TOTAL,
                                         .COST_OF_STUDENT = p.pr.COST_STUDENT,
                                         .COST_TOTAL_USD = p.pr.COST_TOTAL_US,
                                         .COST_OF_STUDENT_USD = p.pr.COST_STUDENT_US,
                                         .NO_OF_STUDENT = p.pr.STUDENT_NUMBER,
                                         .IS_REIMBURSE = p.pr.IS_REIMBURSE,
                                         .TR_LANGUAGE_ID = p.pr.TR_LANGUAGE_ID,
                                         .TR_LANGUAGE_NAME = p.lang.NAME_VN,
                                         .TR_UNIT_NAME = p.pr.CENTERS,
                                         .CONTENT = p.pr.CONTENT,
                                         .TARGET_TRAIN = p.pr.TARGET_TRAIN,
                                         .VENUE = p.pr.VENUE,
                                         .IS_EXAMS = If(p.result.RETEST_SCORE Is Nothing, "Không", "Có"),
                                         .IS_END = If(p.result.IS_END = -1, True, False),
                                         .IS_REACH = If(p.result.IS_REACH = -1, "Đạt", "Không đạt"),
                                         .IS_CERTIFICATE = p.result.IS_CERTIFICATE,
                                         .CERTIFICATE_NO = p.result.CERTIFICATE_NO,
                                         .CERTIFICATE_DATE = p.result.CERTIFICATE_DATE,
                                         .CER_RECEIVE_DATE = p.result.CER_RECEIVE_DATE,
                                         .CERTIFICATE_DURATION = p.ce.DURATION,
                                         .COMITMENT_TRAIN_NO = p.pcomit.COMMIT_NO,
                                         .COMMIT_WORK = p.result.COMMIT_WORKMONTH,
                                         .COMITMENT_START_DATE = p.result.COMMIT_STARTDATE,
                                         .COMITMENT_END_DATE = p.result.COMMIT_ENDDATE,
                                         .RANK_ID = p.rank.ID,
                                         .RANK_NAME = p.rank.NAME_VN,
                                         .TOEIC_FINAL_SCORE = p.result.FINAL_SCORE,
            .REMARK = p.r.REMARK
                                        })

            'Lọc ra cả những nhân viên nghỉ việc
            'If Not _filter.IS_AND_TER Then  'Lọc ra những nhân viên nghỉ việc
            '    If _filter.IS_TER Then
            '        lst = lst.Where(Function(p) (p.WORK_STATUS.HasValue And p.WORK_STATUS = terID And (p.TER_EFFECT_DATE <= dateNow Or p.TER_LAST_DATE <= dateNow)))
            '    End If
            'End If

            If _filter.EMPLOYEE_CODE <> "" Then
                'lst = lst.Where(Function(p) p.EMPLOYEE_CODE.ToUpper().IndexOf(_filter.EMPLOYEE_CODE.ToUpper) >= 0)
                'ThanhNT added lấy full thông tin (Mã/Tên/CMND/Mã cũ)
                lst = lst.Where(Function(p) p.EMPLOYEE_CODE.ToUpper.IndexOf(_filter.EMPLOYEE_CODE.ToUpper) >= 0 Or
                                            p.FULLNAME_VN.ToUpper.IndexOf(_filter.EMPLOYEE_CODE.ToUpper) >= 0)
            End If
            'If _filter.ORG_NAME <> "" Then
            '    lst = lst.Where(Function(p) p.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            'End If
            If _filter.FROM_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.JOIN_DATE >= _filter.FROM_DATE)
            End If

            If _filter.TO_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.JOIN_DATE <= _filter.TO_DATE)
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, Me.ToString() & ".EmployeeRecord")
            Throw ex
        End Try
    End Function

    Public Function GetPortalListEmployeePaging(ByVal _filter As RecordEmployeeDTO,
                                              ByVal PageIndex As Integer,
                                              ByVal PageSize As Integer,
                                              ByRef Total As Integer, ByVal _param As ParamDTO,
                                              Optional ByVal Sorts As String = "EMPLOYEE_CODE desc",
                                              Optional ByVal log As UserLog = Nothing) As List(Of RecordEmployeeDTO)
        Try
            'Using cls As New DataAccess.QueryData
            '    cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
            '                     New With {.P_USERNAME = log.Username,
            '                               .P_ORGID = _param.ORG_ID,
            '                               .P_ISDISSOLVE = _param.IS_DISSOLVE})
            'End Using
            Dim str = ""
            Dim query = From p In Context.TR_PROGRAM_EMP
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMP_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From pro In Context.TR_PROGRAM.Where(Function(f) f.ID = p.TR_PROGRAM_ID).DefaultIfEmpty
                        From c In Context.TR_COURSE.Where(Function(f) f.ID = pro.TR_COURSE_ID).DefaultIfEmpty
                        From r In Context.TR_PROGRAM_RESULT.Where(Function(f) f.EMPLOYEE_ID = e.ID And f.TR_PROGRAM_ID = pro.ID).DefaultIfEmpty
                        From rank In Context.OT_OTHER_LIST.Where(Function(f) f.ID = r.TR_RANK_ID).DefaultIfEmpty
                        From pc In Context.TR_PROGRAM_COMMIT.Where(Function(f) f.TR_PROGRAM_ID = pro.ID And f.EMPLOYEE_ID = e.ID).DefaultIfEmpty
                        Where p.EMP_ID = _filter.EMPLOYEE_ID
            Dim lst = query.Select(Function(p) New RecordEmployeeDTO With {
                                         .EMPLOYEE_ID = p.e.ID,
                                         .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                         .FULLNAME_VN = p.e.FULLNAME_VN,
                                         .TITLE_NAME_VN = p.t.NAME_VN,
                                         .ORG_NAME = p.o.NAME_VN,
                                         .TR_COURSE_ID = p.c.ID,
                                         .TR_COURSE_NAME = p.c.NAME,
                                         .FROM_DATE = p.pro.START_DATE,
                                         .TO_DATE = p.pro.END_DATE,
                                         .CONTENT = p.pro.CONTENT,
                                         .TARGET_TRAIN = p.pro.TARGET_TRAIN,
                                         .VENUE = p.pro.VENUE,
                                         .IS_REACH = If(p.r.IS_REACH = -1, "Đạt", "Không đạt"),
                                         .IS_REACHED = If(p.r.IS_REACH = -1, True, False),
                                         .FINAL_SCORE = p.r.FINAL_SCORE,
                                         .TR_RANK_NAME = p.rank.NAME_VN,
                                         .COMMENT_1 = p.r.COMMENT_1,
                                         .COMMENT_2 = p.r.COMMENT_2,
                                         .COMMENT_3 = p.r.COMMENT_3,
                                         .CERTIFICATE_NAME = If(p.r.INSERT_HSNV = 1, p.pro.CERTIFICATE_NAME, str),
                                         .CERTIFICATE_DATE = p.r.CERT_DATE,
                                         .COMMIT_NO = p.pc.COMMIT_NO,
                                         .MONEY_COMMIT = p.pc.MONEY_COMMIT,
                                         .COMMIT_WORK = p.pc.COMMIT_WORK,
                                         .COMMIT_START = p.pc.COMMIT_START,
                                         .COMMIT_END = p.pc.COMMIT_END,
                                         .WORK_STATUS = p.e.WORK_STATUS,
                                         .TER_EFFECT_DATE = p.e.TER_EFFECT_DATE,
                                         .TER_LAST_DATE = p.e.TER_LAST_DATE,
                                         .TR_PROGRAM_ID = p.pro.ID})
            Dim dateNow = Date.Now.Date
            If _filter.IS_TER Then
                lst = lst.Where(Function(p) (p.WORK_STATUS.HasValue And p.WORK_STATUS = 257 And (p.TER_EFFECT_DATE <= dateNow Or p.TER_LAST_DATE <= dateNow)))
            End If

            If _filter.EMPLOYEE_CODE <> "" Then
                'lst = lst.Where(Function(p) p.EMPLOYEE_CODE.ToUpper().IndexOf(_filter.EMPLOYEE_CODE.ToUpper) >= 0)
                'ThanhNT added lấy full thông tin (Mã/Tên/CMND/Mã cũ)
                lst = lst.Where(Function(p) p.EMPLOYEE_CODE.ToUpper.IndexOf(_filter.EMPLOYEE_CODE.ToUpper) >= 0 Or
                                            p.FULLNAME_VN.ToUpper.IndexOf(_filter.EMPLOYEE_CODE.ToUpper) >= 0)
            End If
            If _filter.ORG_NAME <> "" Then
                lst = lst.Where(Function(p) p.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If

            If _filter.FROM_DATE IsNot Nothing AndAlso _filter.TO_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.TO_DATE >= _filter.FROM_DATE And p.FROM_DATE <= _filter.TO_DATE)
            Else
                If _filter.FROM_DATE IsNot Nothing Then
                    lst = lst.Where(Function(p) p.TO_DATE <= _filter.FROM_DATE)
                End If
                If _filter.TO_DATE IsNot Nothing Then
                    lst = lst.Where(Function(p) p.FROM_DATE <= _filter.TO_DATE)
                End If
            End If
            'Lọc theo khoa dao tao
            If _filter.TR_COURSE_ID.ToString() <> "" Then
                lst = lst.Where(Function(p) p.TR_COURSE_ID = _filter.TR_COURSE_ID)
            End If
            'Lọc theo nhom chuong trinh
            If _filter.TR_PROGRAM_GROUP_ID.ToString() <> "" Then
                lst = lst.Where(Function(p) p.TR_PROGRAM_GROUP_ID = _filter.TR_PROGRAM_GROUP_ID)
            End If

            'Lọc theo linh vuc dao tao
            If (_filter.FIELDS_ID IsNot Nothing) Then
                lst = lst.Where(Function(p) p.FIELDS_ID = _filter.FIELDS_ID)
            End If
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Dim rs = lst.ToList
            For Each item As RecordEmployeeDTO In rs
                Dim centers As String = ""
                Dim programID = item.TR_PROGRAM_ID
                Dim lstCenter = (From p In Context.TR_PROGRAM_CENTER
                                 From c In Context.TR_CENTER.Where(Function(f) f.ID = p.TR_CENTER_ID)
                                 Where p.TR_PROGRAM_ID = programID
                                 Select c.NAME_VN).ToList.ToArray
                If lstCenter.Length > 0 Then
                    centers = String.Join("; ", lstCenter)
                End If
                item.Centers_NAME = centers
            Next
            Return rs
        Catch ex As Exception
            WriteExceptionLog(ex, Me.ToString() & ".EmployeeRecord")
            Throw ex
        End Try
    End Function
#End Region

#Region "Employee Title Course"
    Public Function GetEmployeeTitleCourse(ByVal filter As EmployeeTitleCourseDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer, ByVal _param As ParamDTO,
                                        Optional ByVal Sorts As String = "HU_EMPLOYEE_ID desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of EmployeeTitleCourseDTO)
        Try

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using


            Dim query = From e In Context.HU_EMPLOYEE
                        From tico In Context.TR_TITLE_COURSE.Where(Function(f) f.HU_TITLE_ID = e.TITLE_ID).DefaultIfEmpty
                        From cou In Context.TR_COURSE.Where(Function(f) f.ID = tico.TR_COURSE_ID).DefaultIfEmpty
                        From title In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From org In Context.SE_CHOSEN_ORG.Where(Function(f) e.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)
                        Select New EmployeeTitleCourseDTO With {
                        .HU_EMPLOYEE_ID = e.ID,
                        .HU_EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                        .HU_EMPLOYEE_NAME = e.FULLNAME_VN,
                        .HU_ORG_ID = e.ORG_ID,
                        .HU_TITLE_ID = e.TITLE_ID,
                        .HU_TITLE_CODE = title.CODE,
                        .HU_TITLE_NAME = title.NAME_VN,
                        .TR_COURSE_ID = cou.ID,
                        .TR_COURSE_CODE = cou.CODE,
                        .TR_COURSE_NAME = cou.NAME,
                        .TR_COURSE_REMARK = cou.REMARK,
                        .CREATED_DATE = tico.CREATED_DATE}

            Dim lst = query
            'If filter.NAME <> "" Then
            '    lst = lst.Where(Function(p) p.NAME.ToUpper.Contains(filter.NAME.ToUpper))
            'End If
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList()
            Return Nothing
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetEmployeeTitleCourse")
            Throw ex
        End Try
    End Function
#End Region

#Region "ProgramClassRollcard"
    Public Function GetStudentInClass(ByVal _filter As ProgramClassRollcardDTO,
                             ByVal PageIndex As Integer,
                             ByVal PageSize As Integer,
                             ByVal _param As ParamDTO,
                             ByRef Total As Integer,
                             Optional ByVal Sorts As String = "ID desc",
                             Optional ByVal log As UserLog = Nothing) As List(Of ProgramClassRollcardDTO)
        Try
            Dim str = ""
            Dim query = From p In Context.TR_CLASS_STUDENT
                        From c In Context.TR_CLASS_ROLLCALL.Where(Function(f) f.EMPLOYEE_ID = p.EMPLOYEE_ID And f.TR_CLASS_ID = p.TR_CLASS_ID And f.CLASS_DATE = _filter.CLASS_DATE).DefaultIfEmpty
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        Select New ProgramClassRollcardDTO With {
                       .ID = c.ID,
                       .TR_CLASS_ID = p.TR_CLASS_ID,
                       .EMPLOYEE_ID = p.EMPLOYEE_ID,
                       .FULLNAME = e.FULLNAME_VN,
                       .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                       .CLASS_DATE = c.CLASS_DATE,
                       .ATTEND = c.ATTEND,
                       .UNATTEND = If(c.ATTEND = True, False, True),
                       .ORG_NAME = o.NAME_VN,
                       .TITLE_NAME = t.NAME_VN,
                       .REMARK = If(c.ATTEND Is Nothing, str, c.REMARK)}

            Dim lst = query.Distinct
            If _filter.TR_CLASS_ID.HasValue Then
                lst = lst.Where(Function(f) f.TR_CLASS_ID = _filter.TR_CLASS_ID)
            End If
            If _filter.EMPLOYEE_ID.HasValue Then
                lst = lst.Where(Function(f) f.EMPLOYEE_ID = _filter.EMPLOYEE_ID)
            End If
            If _filter.FULLNAME <> "" Then
                lst = lst.Where(Function(f) f.FULLNAME.ToUpper.Contains(_filter.FULLNAME.ToUpper))
            End If
            If _filter.EMPLOYEE_CODE <> "" Then
                lst = lst.Where(Function(f) f.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTraining")
            Throw ex
        End Try
    End Function
    Public Function GetProgramClassRollcard(ByVal _filter As ProgramClassRollcardDTO,
                             ByVal PageIndex As Integer,
                             ByVal PageSize As Integer,
                             ByVal _param As ParamDTO,
                             ByRef Total As Integer,
                             Optional ByVal Sorts As String = "ID desc",
                             Optional ByVal log As UserLog = Nothing) As List(Of ProgramClassRollcardDTO)
        Try
            Dim query = From p In Context.TR_CLASS_ROLLCALL
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        Select New ProgramClassRollcardDTO With {
                       .ID = p.ID,
                       .TR_CLASS_ID = p.TR_CLASS_ID,
                       .EMPLOYEE_ID = p.EMPLOYEE_ID,
                       .FULLNAME = e.FULLNAME_VN,
                       .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                       .CLASS_DATE = p.CLASS_DATE,
                       .ATTEND = p.ATTEND,
                       .UNATTEND = If(p.ATTEND = True, False, True),
                       .ORG_NAME = o.NAME_VN,
                       .TITLE_NAME = t.NAME_VN,
                       .REMARK = p.REMARK}

            Dim lst = query.Distinct
            If _filter.TR_CLASS_ID.HasValue Then
                lst = lst.Where(Function(f) f.TR_CLASS_ID = _filter.TR_CLASS_ID)
            End If
            If _filter.EMPLOYEE_ID.HasValue Then
                lst = lst.Where(Function(f) f.EMPLOYEE_ID = _filter.EMPLOYEE_ID)
            End If
            If _filter.FULLNAME <> "" Then
                lst = lst.Where(Function(f) f.FULLNAME.ToUpper.Contains(_filter.FULLNAME.ToUpper))
            End If
            If _filter.EMPLOYEE_CODE <> "" Then
                lst = lst.Where(Function(f) f.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTraining")
            Throw ex
        End Try
    End Function

    Public Function InsertProgramClassRollcard(ByVal obj As ProgramClassRollcardDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objData As New TR_CLASS_ROLLCALL
        Try
            objData.ID = Utilities.GetNextSequence(Context, Context.TR_CLASS_ROLLCALL.EntitySet.Name)
            objData.TR_CLASS_ID = obj.TR_CLASS_ID
            objData.EMPLOYEE_ID = obj.EMPLOYEE_ID
            objData.CLASS_DATE = obj.CLASS_DATE
            objData.ATTEND = obj.ATTEND
            objData.REMARK = obj.REMARK
            Context.TR_CLASS_ROLLCALL.AddObject(objData)
            Context.SaveChanges(log)
            gID = objData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTraining")
            Throw ex
        End Try

    End Function


    Public Function UpdateProgramClassRollcard(ByVal lstObj As List(Of ProgramClassRollcardDTO),
                                               ByVal log As UserLog) As Boolean
        Try
            For Each item As ProgramClassRollcardDTO In lstObj
                Dim objData As TR_CLASS_ROLLCALL
                objData = (From p In Context.TR_CLASS_ROLLCALL Where p.EMPLOYEE_ID = item.EMPLOYEE_ID And p.CLASS_DATE = item.CLASS_DATE And p.TR_CLASS_ID = item.TR_CLASS_ID).FirstOrDefault
                If objData IsNot Nothing Then
                    objData.ATTEND = item.ATTEND
                    objData.REMARK = item.REMARK
                Else
                    objData = New TR_CLASS_ROLLCALL
                    objData.ID = Utilities.GetNextSequence(Context, Context.TR_CLASS_ROLLCALL.EntitySet.Name)
                    objData.TR_CLASS_ID = item.TR_CLASS_ID
                    objData.EMPLOYEE_ID = item.EMPLOYEE_ID
                    objData.CLASS_DATE = item.CLASS_DATE
                    objData.ATTEND = item.ATTEND
                    objData.REMARK = item.REMARK
                    Context.TR_CLASS_ROLLCALL.AddObject(objData)
                End If
                Context.SaveChanges(log)
            Next
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTraining")
            Throw ex
        End Try

    End Function

    Public Function ModifyProgramClassRollcard(ByVal obj As ProgramClassRollcardDTO,
                                  ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objData As TR_CLASS_ROLLCALL
        Try
            objData = (From p In Context.TR_CLASS_ROLLCALL Where p.ID = obj.ID).FirstOrDefault
            objData.TR_CLASS_ID = obj.TR_CLASS_ID
            objData.EMPLOYEE_ID = obj.EMPLOYEE_ID
            objData.CLASS_DATE = obj.CLASS_DATE
            objData.ATTEND = obj.ATTEND
            objData.REMARK = obj.REMARK
            Context.SaveChanges(log)
            gID = objData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTraining")
            Throw ex
        End Try

    End Function

    Public Function DeleteProgramClassRollcard(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim lstData As List(Of TR_CLASS_ROLLCALL)
        Try

            lstData = (From p In Context.TR_CLASS_ROLLCALL Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                Context.TR_CLASS_ROLLCALL.DeleteObject(lstData(index))
            Next

            Context.SaveChanges(log)
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTraining")
            Throw ex
        End Try

    End Function
#End Region

#Region "TR CLASS RESULT"
    Public Function GetClassResult(ByVal _filter As ProgramClassStudentDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "EMPLOYEE_CODE desc") As List(Of ProgramClassStudentDTO)
        Try
            Dim query = From student In Context.TR_CLASS_STUDENT
                        From emp In Context.HU_EMPLOYEE.Where(Function(f) f.ID = student.EMPLOYEE_ID).DefaultIfEmpty
                        From title In Context.HU_TITLE.Where(Function(f) f.ID = emp.TITLE_ID).DefaultIfEmpty
                        From cls In Context.TR_CLASS.Where(Function(f) f.ID = student.TR_CLASS_ID)
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = emp.ORG_ID).DefaultIfEmpty
                        Where cls.TR_PROGRAM_ID = _filter.TR_PROGRAM_ID And student.TR_CLASS_ID = _filter.TR_CLASS_ID
                        Select New ProgramClassStudentDTO With {.ID = student.ID,
                                                          .EMPLOYEE_ID = emp.ID,
                                                          .EMPLOYEE_CODE = emp.EMPLOYEE_CODE,
                                                          .EMPLOYEE_NAME = emp.FULLNAME_VN,
                                                          .ORG_NAME = org.NAME_VN,
                                                          .TITLE_ID = title.ID,
                                                          .TITLE_NAME = title.NAME_VN,
                                                          .NOTE = student.NOTE,
                                                          .IS_EXAM = student.IS_EXAM}

            Dim lst = query.Distinct

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetClassResult")
            Throw ex
        End Try
    End Function
    Public Function UpdateClassResultt(ByVal lst As List(Of ProgramClassStudentDTO),
                                   ByVal log As UserLog) As Boolean
        Try
            For Each obj In lst
                Dim objData = (From p In Context.TR_CLASS_STUDENT
                               Where p.ID = obj.ID).FirstOrDefault

                If objData IsNot Nothing Then

                    With objData
                        .IS_EXAM = obj.IS_EXAM
                        .NOTE = obj.NOTE
                    End With
                End If
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region


End Class