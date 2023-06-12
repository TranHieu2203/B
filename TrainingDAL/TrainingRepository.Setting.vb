Imports Framework.Data
Imports Framework.Data.System.Linq.Dynamic

Partial Class TrainingRepository

#Region "SettingCriteriaGroup"

    Public Function GetCriteriaNotByGroupID(ByVal _filter As SettingCriteriaGroupDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "TR_CRITERIA_CODE desc") As List(Of SettingCriteriaGroupDTO)

        Try
            Dim query As IQueryable(Of SettingCriteriaGroupDTO)
            query = From p In Context.TR_CRITERIA
                    Where p.ACTFLG = True _
                    And Not (From can In Context.TR_SETTING_CRI_GRP
                             Where can.TR_CRITERIA_GROUP_ID = _filter.TR_CRITERIA_GROUP_ID
                             Select can.TR_CRITERIA_ID).Contains(p.ID)
                    Select New SettingCriteriaGroupDTO With {
                        .TR_CRITERIA_ID = p.ID,
                        .TR_CRITERIA_CODE = p.CODE,
                        .TR_CRITERIA_NAME = p.NAME,
                        .TR_CRITERIA_POINT_MAX = p.POINT_MAX}
            Dim lst = query

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetCriteriaNotByGroupID")
            Throw ex
        End Try

    End Function

    Public Function GetCriteriaByGroupID(ByVal _filter As SettingCriteriaGroupDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "TR_CRITERIA_CODE desc") As List(Of SettingCriteriaGroupDTO)

        Try
            Dim query = From sett In Context.TR_SETTING_CRI_GRP
                        From p In Context.TR_CRITERIA.Where(Function(f) f.ID = sett.TR_CRITERIA_ID)
                        Where sett.TR_CRITERIA_GROUP_ID = _filter.TR_CRITERIA_GROUP_ID
                        Select New SettingCriteriaGroupDTO With {
                            .ID = sett.ID,
                            .TR_CRITERIA_ID = p.ID,
                            .TR_CRITERIA_CODE = p.CODE,
                            .TR_CRITERIA_NAME = p.NAME,
                            .TR_CRITERIA_POINT_MAX = p.POINT_MAX}

            Dim lst = query

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
            Return lst.ToList
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetCriteriaByGroupID")
            Throw ex
        End Try

    End Function

    Public Function InsertSettingCriteriaGroup(ByVal lst As List(Of SettingCriteriaGroupDTO),
                                   ByVal log As UserLog) As Boolean
        Dim iCount As Integer = 0
        Dim objData As TR_SETTING_CRI_GRP
        Try
            For Each obj In lst
                objData = New TR_SETTING_CRI_GRP
                objData.ID = Utilities.GetNextSequence(Context, Context.TR_SETTING_CRI_GRP.EntitySet.Name)
                objData.TR_CRITERIA_GROUP_ID = obj.TR_CRITERIA_GROUP_ID
                objData.TR_CRITERIA_ID = obj.TR_CRITERIA_ID
                Context.TR_SETTING_CRI_GRP.AddObject(objData)
            Next

            Context.SaveChanges(log)

            Return True
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function DeleteSettingCriteriaGroup(ByVal lst As List(Of Decimal),
                                   ByVal log As UserLog) As Boolean
        Try
            Dim lstData = (From p In Context.TR_SETTING_CRI_GRP Where lst.Contains(p.ID)).ToList
            For Each objData In lstData
                Context.TR_SETTING_CRI_GRP.DeleteObject(objData)
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception

        End Try
    End Function

#End Region

#Region "SettingAssForm"

    Public Function GetCriteriaGroupNotByFormID(ByVal _filter As SettingAssFormDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "TR_CRITERIA_GROUP_CODE desc") As List(Of SettingAssFormDTO)

        Try
            Dim query As IQueryable(Of SettingAssFormDTO)
            query = From p In Context.TR_CRITERIA_GROUP
                    Where p.ACTFLG = True _
                    And Not (From can In Context.TR_SETTING_ASS_FORM
                             Where can.TR_ASSESSMENT_FORM_ID = _filter.TR_ASSESSMENT_FORM_ID
                             Select can.TR_CRITERIA_GROUP_ID).Contains(p.ID)
                    Select New SettingAssFormDTO With {
                        .TR_CRITERIA_GROUP_ID = p.ID,
                        .TR_CRITERIA_GROUP_CODE = p.CODE,
                        .TR_CRITERIA_GROUP_NAME = p.NAME,
                        .REMARK = p.REMARK}
            Dim lst = query

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetCriteriaGroupNotByFormID")
            Throw ex
        End Try

    End Function

    Public Function GetCriteriaGroupByFormID(ByVal _filter As SettingAssFormDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "TR_CRITERIA_GROUP_CODE desc") As List(Of SettingAssFormDTO)

        Try
            Dim query = From sett In Context.TR_SETTING_ASS_FORM
                        From p In Context.TR_CRITERIA_GROUP.Where(Function(f) f.ID = sett.TR_CRITERIA_GROUP_ID)
                        Where sett.TR_ASSESSMENT_FORM_ID = _filter.TR_ASSESSMENT_FORM_ID
                        Select New SettingAssFormDTO With {
                            .ID = sett.ID,
                            .TR_CRITERIA_GROUP_ID = p.ID,
                            .TR_CRITERIA_GROUP_CODE = p.CODE,
                            .TR_CRITERIA_GROUP_NAME = p.NAME,
                            .REMARK = p.REMARK}

            Dim lst = query

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
            Return lst.ToList
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetCriteriaGroupByFormID")
            Throw ex
        End Try

    End Function

    Public Function InsertSettingAssForm(ByVal lst As List(Of SettingAssFormDTO),
                                   ByVal log As UserLog) As Boolean
        Dim iCount As Integer = 0
        Dim objData As TR_SETTING_ASS_FORM
        Try
            For Each obj In lst
                objData = New TR_SETTING_ASS_FORM
                objData.ID = Utilities.GetNextSequence(Context, Context.TR_SETTING_ASS_FORM.EntitySet.Name)
                objData.TR_CRITERIA_GROUP_ID = obj.TR_CRITERIA_GROUP_ID
                objData.TR_ASSESSMENT_FORM_ID = obj.TR_ASSESSMENT_FORM_ID
                Context.TR_SETTING_ASS_FORM.AddObject(objData)
            Next

            Context.SaveChanges(log)

            Return True
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function DeleteSettingAssForm(ByVal lst As List(Of Decimal),
                                   ByVal log As UserLog) As Boolean
        Try
            Dim lstData = (From p In Context.TR_SETTING_ASS_FORM Where lst.Contains(p.ID)).ToList
            For Each objData In lstData
                Context.TR_SETTING_ASS_FORM.DeleteObject(objData)
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Setting Title Course"
    Public Function GetTitleCourse(ByVal _filter As TitleCourseDTO, ByVal PageIndex As Integer,
                                    ByVal PageSize As Integer,
                                    ByRef Total As Integer,
                                    Optional ByVal Sorts As String = "CREATED_DATE") As List(Of TitleCourseDTO)

        Try
            Dim query = From p In Context.TR_TITLE_COURSE
                        From course In Context.TR_COURSE.Where(Function(f) f.ID = p.TR_COURSE_ID).DefaultIfEmpty
                        From groupTitle In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TITLE_GROUP_ID).DefaultIfEmpty
                        From title In Context.HU_TITLE.Where(Function(f) f.ID = p.HU_TITLE_ID).DefaultIfEmpty
                        Select New TitleCourseDTO With {
                                        .ID = p.ID,
                                        .HU_TITLE_ID = p.HU_TITLE_ID,
                                        .TR_COURSE_ID = p.TR_COURSE_ID,
                                        .TR_COURSE_CODE = course.CODE,
                                        .TR_COURSE_NAME = course.NAME,
                                        .TR_COURSE_REMARK = p.NOTE,
                                        .CREATED_DATE = p.CREATED_DATE,
                                        .IS_CHECK = p.IS_CHECK,
                                        .EFFECT_DATE = p.EFFECT_DATE,
                                        .TITLE_GROUP_ID = p.TITLE_GROUP_ID,
                                        .TITLE_GROUP_NAME = groupTitle.NAME_VN,
                                        .HU_TITLE_NAME = title.NAME_VN}

            Dim lst = query
            'If _filter.HU_TITLE_ID.HasValue Then
            '    lst = lst.Where(Function(p) p.HU_TITLE_ID = _filter.HU_TITLE_ID)
            'End If

            'If _filter.TITLE_GROUP_ID.HasValue Then
            '    lst = lst.Where(Function(p) p.TITLE_GROUP_ID = _filter.TITLE_GROUP_ID)
            'End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetTitleCourse")
            Throw ex
        End Try

    End Function
    Public Function UpdateTitleCourse(ByVal objExams As TitleCourseDTO, ByVal log As UserLog) As Boolean
        Dim objTitleCourse As TR_TITLE_COURSE
        Try
            If objExams.ID IsNot Nothing Then
                objTitleCourse = (From p In Context.TR_TITLE_COURSE
                                  Where p.ID = objExams.ID).FirstOrDefault
            Else
                objTitleCourse = Nothing
            End If
            If objTitleCourse Is Nothing Then
                objTitleCourse = New TR_TITLE_COURSE
                objTitleCourse.ID = Utilities.GetNextSequence(Context, Context.TR_TITLE_COURSE.EntitySet.Name)
                objTitleCourse.HU_TITLE_ID = objExams.HU_TITLE_ID
                objTitleCourse.TR_COURSE_ID = objExams.TR_COURSE_ID
                objTitleCourse.TITLE_GROUP_ID = objExams.TITLE_GROUP_ID
                objTitleCourse.IS_CHECK = objExams.IS_CHECK
                objTitleCourse.EFFECT_DATE = objExams.EFFECT_DATE
                objTitleCourse.NOTE = objExams.TR_COURSE_REMARK
                objTitleCourse.ACTFLG = "A"
                Context.TR_TITLE_COURSE.AddObject(objTitleCourse)
                Context.SaveChanges(log)
            Else
                Context.TR_TITLE_COURSE.Attach(objTitleCourse)
                objTitleCourse.HU_TITLE_ID = objExams.HU_TITLE_ID
                objTitleCourse.TITLE_GROUP_ID = objExams.TITLE_GROUP_ID
                objTitleCourse.IS_CHECK = objExams.IS_CHECK
                objTitleCourse.EFFECT_DATE = objExams.EFFECT_DATE
                objTitleCourse.NOTE = objExams.TR_COURSE_REMARK
                objTitleCourse.TR_COURSE_ID = objExams.TR_COURSE_ID
                objTitleCourse.ACTFLG = objExams.ACTFLG
                'Context.TR_TITLE_COURSE.AddObject(objTitleCourse)
                Context.SaveChanges(log)
            End If
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".UpdateTitleCourse")
            Throw ex
        End Try
    End Function

    Public Function DeleteTitleCourse(ByVal obj As TitleCourseDTO) As Boolean
        Dim objDel As TR_TITLE_COURSE
        Try
            objDel = (From p In Context.TR_TITLE_COURSE Where obj.ID = p.ID).FirstOrDefault
            Context.TR_TITLE_COURSE.DeleteObject(objDel)
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteTitleCourse")
            Throw ex
        End Try

    End Function



#End Region

#Region "SettingCriteriaCourse"
    Public Function GET_SETTING_CRITERIA_COURSE(ByVal _filter As SettingCriteriaCourseDTO,
                             ByVal PageIndex As Integer,
                             ByVal PageSize As Integer,
                             ByRef Total As Integer,
                             Optional ByVal Sorts As String = "ID desc") As List(Of SettingCriteriaCourseDTO)

        Try
            Dim query = From p In Context.TR_SETTING_CRI_COURSE
                        From gr In Context.TR_CRITERIA_GROUP.Where(Function(f) f.ID = p.TR_CRITERIA_GROUP_ID).DefaultIfEmpty
                        From c In Context.TR_COURSE.Where(Function(f) f.ID = p.TR_COURSE_ID).DefaultIfEmpty
                        Select New SettingCriteriaCourseDTO With {
                       .ID = p.ID,
                       .TR_COURSE_ID = p.TR_COURSE_ID,
                       .TR_COURSE_NAME = c.NAME,
                       .TR_CRITERIA_GROUP_ID = p.TR_CRITERIA_GROUP_ID,
                       .TR_CRITERIA_GROUP_NAME = gr.NAME,
                       .EFFECT_FROM = p.EFFECT_FROM,
                       .EFFECT_TO = p.EFFECT_TO,
                       .REMARK = p.REMARK,
                .SCALE_POINT = p.SCALE_POINT}

            Dim lst = query

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Dim rs = lst.ToList
            For Each item As SettingCriteriaCourseDTO In rs
                Dim lstDetail = From p In Context.TR_SETTING_CRI_DETAIL
                                From c In Context.TR_COURSE.Where(Function(f) f.ID = p.COURSE_ID).DefaultIfEmpty
                                From t In Context.TR_CRITERIA.Where(Function(f) f.ID = p.CRITERIA_ID).DefaultIfEmpty
                                Where p.COURSE_ID = item.ID
                                Select New SettingCriteriaDetailDTO With {
                                           .ID = p.ID,
                                           .COURSE_ID = p.COURSE_ID,
                                           .COURSE_NAME = c.NAME,
                                           .CRITERIA_ID = t.ID,
                                           .CRITERIA_NAME = t.NAME,
                                           .POINT_MAX = p.POINT_MAX,
                                           .RATIO = p.RATIO}
                lstDetail.ToList()
                Dim groupName = ""
                For Each itemDetail As SettingCriteriaDetailDTO In lstDetail
                    groupName = groupName + itemDetail.CRITERIA_NAME + " - Tỷ trọng: " + itemDetail.RATIO.ToString + "% - Mức độ hữu ích: " + itemDetail.POINT_MAX.ToString + "<br>"
                Next
                item.TR_CRITERIA_GROUP_NAME = groupName
            Next
            Return rs
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GET_SETTING_CRITERIA_COURSE")
            Throw ex
        End Try
    End Function
    Public Function GET_SETTING_CRITERIA_DETAIL_BY_COURSE(ByVal _filter As SettingCriteriaDetailDTO,
                             Optional ByVal Sorts As String = "ID desc") As List(Of SettingCriteriaDetailDTO)

        Try
            Dim query = From p In Context.TR_SETTING_CRI_DETAIL
                        From c In Context.TR_COURSE.Where(Function(f) f.ID = p.COURSE_ID).DefaultIfEmpty
                        From t In Context.TR_CRITERIA.Where(Function(f) f.ID = p.CRITERIA_ID).DefaultIfEmpty
                        Where p.COURSE_ID = _filter.COURSE_ID
                        Select New SettingCriteriaDetailDTO With {
                                   .ID = p.ID,
                                   .COURSE_ID = p.COURSE_ID,
                                   .COURSE_NAME = c.NAME,
                                   .CRITERIA_ID = p.CRITERIA_ID,
                                   .CRITERIA_NAME = t.NAME,
                                   .POINT_MAX = p.POINT_MAX,
                                   .RATIO = p.RATIO}

            Dim lst = query

            Return lst.ToList
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GET_SETTING_CRITERIA_DETAIL_BY_COURSE")
            Throw ex
        End Try
    End Function

    Public Function INSERT_SETTING_CRITERIA_COURSE(ByVal obj As SettingCriteriaCourseDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objData As New TR_SETTING_CRI_COURSE
        Dim iCount As Integer = 0
        Try
            Dim id = Utilities.GetNextSequence(Context, Context.TR_SETTING_CRI_COURSE.EntitySet.Name)
            objData.ID = id
            objData.EFFECT_FROM = obj.EFFECT_FROM
            objData.EFFECT_TO = obj.EFFECT_TO
            objData.REMARK = obj.REMARK
            objData.TR_COURSE_ID = obj.TR_COURSE_ID
            objData.SCALE_POINT = obj.SCALE_POINT
            Context.TR_SETTING_CRI_COURSE.AddObject(objData)
            For Each item As SettingCriteriaDetailDTO In obj.SettingCriteriaDetail
                Dim objDataDetail As New TR_SETTING_CRI_DETAIL
                objDataDetail.ID = Utilities.GetNextSequence(Context, Context.TR_SETTING_CRI_DETAIL.EntitySet.Name)
                objDataDetail.CRITERIA_ID = item.CRITERIA_ID
                objDataDetail.POINT_MAX = item.POINT_MAX
                objDataDetail.RATIO = item.RATIO
                objDataDetail.COURSE_ID = id
                Context.TR_SETTING_CRI_DETAIL.AddObject(objDataDetail)
            Next
            Context.SaveChanges(log)
            gID = objData.ID
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".INSERT_SETTING_CRITERIA_COURSE")
            Throw ex
        End Try
    End Function

    Public Function MODIFY_SETTING_CRITERIA_COURSE(ByVal obj As SettingCriteriaCourseDTO,
                                  ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objData As TR_SETTING_CRI_COURSE
        Dim listID As New List(Of Decimal)
        Try
            objData = (From p In Context.TR_SETTING_CRI_COURSE Where p.ID = obj.ID).FirstOrDefault
            objData.EFFECT_FROM = obj.EFFECT_FROM
            objData.EFFECT_TO = obj.EFFECT_TO
            objData.REMARK = obj.REMARK
            objData.TR_COURSE_ID = obj.TR_COURSE_ID
            objData.SCALE_POINT = obj.SCALE_POINT
            For Each item As SettingCriteriaDetailDTO In obj.SettingCriteriaDetail
                If item.ID = 0 Then
                    Context.TR_SETTING_CRI_DETAIL.AddObject(New TR_SETTING_CRI_DETAIL With {
                                                   .ID = Utilities.GetNextSequence(Context, Context.TR_SETTING_CRI_DETAIL.EntitySet.Name),
                                                   .CRITERIA_ID = item.CRITERIA_ID,
                                                   .POINT_MAX = item.POINT_MAX,
                                                   .RATIO = item.RATIO,
                                                   .COURSE_ID = obj.ID})
                Else
                    Dim objSettingCriteriaDetail = (From p In Context.TR_SETTING_CRI_DETAIL Where p.ID = item.ID).FirstOrDefault
                    If objSettingCriteriaDetail IsNot Nothing Then
                        objSettingCriteriaDetail.CRITERIA_ID = item.CRITERIA_ID
                        objSettingCriteriaDetail.POINT_MAX = item.POINT_MAX
                        objSettingCriteriaDetail.RATIO = item.RATIO
                        objSettingCriteriaDetail.COURSE_ID = item.COURSE_ID
                    End If
                End If
                listID.Add(item.ID)
            Next
            If listID.Count > 0 Then
                Dim objSettingCriteriaDetailDelete = (From p In Context.TR_SETTING_CRI_DETAIL Where (Not listID.Contains(p.ID)) And p.COURSE_ID = obj.ID)
                For Each item As TR_SETTING_CRI_DETAIL In objSettingCriteriaDetailDelete
                    Context.TR_SETTING_CRI_DETAIL.DeleteObject(item)
                Next
            End If
            Context.SaveChanges(log)
            gID = objData.ID
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".MODIFY_SETTING_CRITERIA_COURSE")
            Throw ex
        End Try
    End Function

    Public Function DELETE_SETTING_CRITERIA_COURSE(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim lstData As List(Of TR_SETTING_CRI_COURSE)
        Dim lstDataDetail As List(Of TR_SETTING_CRI_DETAIL)
        Try
            lstData = (From p In Context.TR_SETTING_CRI_COURSE Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                Context.TR_SETTING_CRI_COURSE.DeleteObject(lstData(index))
            Next
            lstDataDetail = (From p In Context.TR_SETTING_CRI_DETAIL Where lstID.Contains(p.COURSE_ID)).ToList
            For index = 0 To lstDataDetail.Count - 1
                Context.TR_SETTING_CRI_DETAIL.DeleteObject(lstDataDetail(index))
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".DELETE_SETTING_CRITERIA_COURSE")
            Throw ex
        End Try
    End Function
#End Region

End Class