Imports System.Data.Objects
Imports System.Reflection
Imports Framework.Data
Imports Framework.Data.System.Linq.Dynamic

Partial Class TrainingRepository

#Region "Hoadm"

#Region "Certificate"

    Public Function GetCertificate(ByVal _filter As CertificateDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CertificateDTO)

        Try
            Dim query = From p In Context.TR_CERTIFICATE
                        From cer_group In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TR_CER_GROUP_ID).DefaultIfEmpty
                        From dl In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.CATEGORY).DefaultIfEmpty
                        Select New CertificateDTO With {
                            .ID = p.ID,
                            .NAME_VN = p.NAME_VN,
                            .NAME_EN = p.NAME_EN,
                            .TR_CER_GROUP_ID = p.TR_CER_GROUP_ID,
                            .TR_CER_GROUP_NAME = cer_group.NAME_VN,
                            .ACTFLG = If(p.ACTFLG <> 0, "Áp dụng", "Ngưng áp dụng"),
                            .CREATED_DATE = p.CREATED_DATE,
                            .DURATION = p.DURATION,
                            .CATEGORY = p.CATEGORY,
                            .CATEGORY_NAME = dl.NAME_VN,
                            .TRANSPORTATION_TYPE = p.TRANSPORTATION_TYPE,
                            .REMARK = p.REMARK}

            Dim lst = query
            If _filter.NAME_VN <> "" Then
                lst = lst.Where(Function(p) p.NAME_VN.ToUpper.Contains(_filter.NAME_VN.ToUpper))
            End If
            If _filter.NAME_EN <> "" Then
                lst = lst.Where(Function(p) p.NAME_EN.ToUpper.Contains(_filter.NAME_EN.ToUpper))
            End If
            If _filter.TR_CER_GROUP_NAME <> "" Then
                lst = lst.Where(Function(p) p.TR_CER_GROUP_NAME.ToUpper.Contains(_filter.TR_CER_GROUP_NAME.ToUpper))
            End If
            If _filter.ACTFLG IsNot Nothing Then
                lst = lst.Where(Function(p) p.ACTFLG = _filter.ACTFLG)
            End If
            If _filter.DURATION IsNot Nothing Then
                lst = lst.Where(Function(p) p.DURATION = _filter.DURATION)
            End If
            If _filter.CATEGORY_NAME <> "" Then
                lst = lst.Where(Function(p) p.CATEGORY_NAME.ToUpper.Contains(_filter.CATEGORY_NAME.ToUpper))
            End If
            If _filter.TRANSPORTATION_TYPE <> "" Then
                lst = lst.Where(Function(p) p.TRANSPORTATION_TYPE.ToUpper.Contains(_filter.TRANSPORTATION_TYPE.ToUpper))
            End If
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetCertificate")
            Throw ex
        End Try

    End Function

    Public Function InsertCertificate(ByVal objCertificate As CertificateDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objCertificateData As New TR_CERTIFICATE
        Try
            objCertificateData.ID = Utilities.GetNextSequence(Context, Context.TR_CERTIFICATE.EntitySet.Name)
            objCertificateData.NAME_VN = objCertificate.NAME_VN.Trim
            objCertificateData.NAME_EN = objCertificate.NAME_EN.Trim
            objCertificateData.TR_CER_GROUP_ID = objCertificate.TR_CER_GROUP_ID
            objCertificateData.ACTFLG = If(objCertificate.ACTFLG = "True", True, False)
            objCertificateData.DURATION = objCertificate.DURATION
            objCertificateData.CATEGORY = objCertificate.CATEGORY
            objCertificateData.TRANSPORTATION_TYPE = objCertificate.TRANSPORTATION_TYPE
            objCertificateData.REMARK = objCertificate.REMARK

            Context.TR_CERTIFICATE.AddObject(objCertificateData)
            Context.SaveChanges(log)
            gID = objCertificateData.ID
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".InsertCertificate")
            Throw ex
        End Try
    End Function

    Public Function ValidateCertificate(ByVal _validate As CertificateDTO)
        'Dim query
        Try

            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".ValidateCertificate")
            Throw ex
        End Try
    End Function

    Public Function ModifyCertificate(ByVal objCertificate As CertificateDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objCertificateData As New TR_CERTIFICATE With {.ID = objCertificate.ID}
        Try
            Context.TR_CERTIFICATE.Attach(objCertificateData)
            objCertificateData.NAME_VN = objCertificate.NAME_VN.Trim
            objCertificateData.NAME_EN = objCertificate.NAME_EN.Trim
            objCertificateData.TR_CER_GROUP_ID = objCertificate.TR_CER_GROUP_ID
            objCertificateData.DURATION = objCertificate.DURATION
            objCertificateData.CATEGORY = objCertificate.CATEGORY
            objCertificateData.TRANSPORTATION_TYPE = objCertificate.TRANSPORTATION_TYPE
            objCertificateData.REMARK = objCertificate.REMARK
            Context.SaveChanges(log)
            gID = objCertificateData.ID
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".ModifyCertificate")
            Throw ex
        End Try

    End Function


    Public Function ActiveCertificate(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As Boolean) As Boolean
        Dim lstData As List(Of TR_CERTIFICATE)
        Try
            lstData = (From p In Context.TR_CERTIFICATE Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                lstData(index).ACTFLG = bActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            Throw ex
        End Try

    End Function


    Public Function DeleteCertificate(ByVal lstCertificate() As CertificateDTO) As Boolean
        Dim lstCertificateData As List(Of TR_CERTIFICATE)
        Dim lstIDCertificate As List(Of Decimal) = (From p In lstCertificate.ToList Select p.ID).ToList
        Dim count As Decimal = 0
        Dim strListID As String = lstIDCertificate.Select(Function(x) x.ToString).Aggregate(Function(x, y) x & "," & y)
        Dim Sql = "SELECT COUNT(TR_CERTIFICATE_ID) FROM Tr_Course WHERE TR_CERTIFICATE_ID IN (" & strListID & ")"
        count = Context.ExecuteStoreQuery(Of Decimal)(Sql).FirstOrDefault
        If count > 0 Then
            Return False
        End If
        Try
            lstCertificateData = (From p In Context.TR_CERTIFICATE Where lstIDCertificate.Contains(p.ID)).ToList
            For index = 0 To lstCertificateData.Count - 1

                Context.TR_CERTIFICATE.DeleteObject(lstCertificateData(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteCertificate")
            Throw ex
        End Try

    End Function

#End Region

#Region "Course"

    Public Function GetCourse(ByVal _filter As CourseDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CourseDTO)

        Try
            Dim query = From p In Context.TR_COURSE
                        From cer In Context.TR_CERTIFICATE.Where(Function(f) f.ID = p.TR_CERTIFICATE_ID).DefaultIfEmpty
                        From cer_group In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TR_CER_GROUP_ID).DefaultIfEmpty
                        From tf In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TR_TRAIN_FIELD).DefaultIfEmpty
                        From pg In Context.TR_PROGRAM_GROUP.Where(Function(f) f.ID = p.TR_PROGRAM_GROUP).DefaultIfEmpty
                        Select New CourseDTO With {
                            .ID = p.ID,
                            .CODE = p.CODE,
                            .NAME = p.NAME,
                            .TR_CER_GROUP_ID = p.TR_CER_GROUP_ID,
                            .TR_CER_GROUP_NAME = cer_group.NAME_VN,
                            .TR_CERTIFICATE_ID = p.TR_CERTIFICATE_ID,
                            .TR_CERTIFICATE_NAME = cer.NAME_VN,
                            .TR_TRAIN_FIELD_ID = p.TR_TRAIN_FIELD,
                            .TR_TRAIN_FIELD_NAME = tf.NAME_VN,
                            .TR_PROGRAM_GROUP_ID = p.TR_PROGRAM_GROUP,
                            .TR_PROGRAM_GROUP_NAME = pg.NAME,
                            .DRIVER = p.DRIVER,
                            .REMARK = p.REMARK,
                            .ACTFLG = If(p.ACTFLG <> 0, "Áp dụng", "Ngừng áp dụng"),
                            .CREATED_DATE = p.CREATED_DATE}

            Dim lst = query
            If _filter.CODE <> "" Then
                lst = lst.Where(Function(p) p.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
            End If
            If _filter.NAME <> "" Then
                lst = lst.Where(Function(p) p.NAME.ToUpper.Contains(_filter.NAME.ToUpper))
            End If
            If _filter.TR_CER_GROUP_NAME <> "" Then
                lst = lst.Where(Function(p) p.TR_CER_GROUP_NAME.ToUpper.Contains(_filter.TR_CER_GROUP_NAME.ToUpper))
            End If
            If _filter.TR_CERTIFICATE_NAME <> "" Then
                lst = lst.Where(Function(p) p.TR_CERTIFICATE_NAME.ToUpper.Contains(_filter.TR_CERTIFICATE_NAME.ToUpper))
            End If
            If _filter.TR_TRAIN_FIELD_NAME <> "" Then
                lst = lst.Where(Function(p) p.TR_TRAIN_FIELD_NAME.ToUpper.Contains(_filter.TR_TRAIN_FIELD_NAME.ToUpper))
            End If
            If _filter.TR_PROGRAM_GROUP_NAME <> "" Then
                lst = lst.Where(Function(p) p.TR_PROGRAM_GROUP_NAME.ToUpper.Contains(_filter.TR_PROGRAM_GROUP_NAME.ToUpper))
            End If
            If _filter.ACTFLG IsNot Nothing Then
                lst = lst.Where(Function(p) p.ACTFLG = _filter.ACTFLG)
            End If
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetCourse")
            Throw ex
        End Try

    End Function

    Public Function InsertCourse(ByVal objCourse As CourseDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objCourseData As New TR_COURSE
        Try
            objCourseData.ID = Utilities.GetNextSequence(Context, Context.TR_COURSE.EntitySet.Name)
            objCourseData.CODE = objCourse.CODE.Trim
            objCourseData.NAME = objCourse.NAME.Trim
            objCourseData.TR_CER_GROUP_ID = objCourse.TR_CER_GROUP_ID
            objCourseData.TR_CERTIFICATE_ID = objCourse.TR_CERTIFICATE_ID
            objCourseData.TR_TRAIN_FIELD = objCourse.TR_TRAIN_FIELD_ID
            objCourseData.TR_PROGRAM_GROUP = objCourse.TR_PROGRAM_GROUP_ID
            objCourseData.DRIVER = objCourse.DRIVER
            objCourseData.REMARK = objCourse.REMARK
            objCourseData.ACTFLG = If(objCourse.ACTFLG = "True", True, False) 'Do DTO là String nên phải chuyển true,false lại
            Context.TR_COURSE.AddObject(objCourseData)
            Context.SaveChanges(log)
            gID = objCourseData.ID
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".InsertCourse")
            Throw ex
        End Try
    End Function

    Public Function ValidateCourse(ByVal _validate As CourseDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.TR_COURSE
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.TR_COURSE
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If

                Return (query Is Nothing)
            End If
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".ValidateCourse")
            Throw ex
        End Try
    End Function

    Public Function ModifyCourse(ByVal objCourse As CourseDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objCourseData As New TR_COURSE With {.ID = objCourse.ID}
        Try
            Context.TR_COURSE.Attach(objCourseData)
            objCourseData.CODE = objCourse.CODE.Trim
            objCourseData.NAME = objCourse.NAME.Trim
            objCourseData.TR_CER_GROUP_ID = objCourse.TR_CER_GROUP_ID
            objCourseData.TR_CERTIFICATE_ID = objCourse.TR_CERTIFICATE_ID
            objCourseData.TR_TRAIN_FIELD = objCourse.TR_TRAIN_FIELD_ID
            objCourseData.TR_PROGRAM_GROUP = objCourse.TR_PROGRAM_GROUP_ID
            objCourseData.DRIVER = objCourse.DRIVER
            objCourseData.REMARK = objCourse.REMARK
            Context.SaveChanges(log)
            gID = objCourseData.ID
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".ModifyCourse")
            Throw ex
        End Try

    End Function

    Public Function ActiveCourse(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As Boolean) As Boolean
        Dim lstData As List(Of TR_COURSE)
        Try
            lstData = (From p In Context.TR_COURSE Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                lstData(index).ACTFLG = bActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function DeleteCourse(ByVal lstCourse() As CourseDTO) As Boolean
        Dim lstCourseData As List(Of TR_COURSE)
        Dim lstIDCourse As List(Of Decimal) = (From p In lstCourse.ToList Select p.ID).ToList
        Dim count As Decimal = 0
        Dim strListID As String = lstIDCourse.Select(Function(x) x.ToString).Aggregate(Function(x, y) x & "," & y)
        Dim Sql = "SELECT COUNT(TR_COURSE_ID) FROM Tr_Plan WHERE TR_COURSE_ID IN (" & strListID & ")"
        count = Context.ExecuteStoreQuery(Of Decimal)(Sql).FirstOrDefault
        If count > 0 Then
            Return False
        End If
        Try
            lstCourseData = (From p In Context.TR_COURSE Where lstIDCourse.Contains(p.ID)).ToList
            For index = 0 To lstCourseData.Count - 1
                Context.TR_COURSE.DeleteObject(lstCourseData(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteCourse")
            Throw ex
        End Try

    End Function

#End Region

#Region "Center"

    Public Function GetCenter(ByVal _filter As CenterDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CenterDTO)

        Try
            Dim query = From p In Context.TR_CENTER
                        Select New CenterDTO With {
                            .ID = p.ID,
                            .CODE = p.CODE,
                            .NAME_EN = p.NAME_EN,
                            .NAME_VN = p.NAME_VN,
                            .PHONE = p.PHONE,
                            .REPRESENT = p.REPRESENT,
                            .TAX_CODE = p.TAX_CODE,
                            .TRAIN_FIELD = p.TRAIN_FIELD,
                            .WEB = p.WEB,
                            .ADDRESS = p.ADDRESS,
                            .CONTACT_EMAIL = p.CONTACT_EMAIL,
                            .CONTACT_PERSON = p.CONTACT_PERSON,
                            .CONTACT_PHONE = p.CONTACT_PHONE,
                            .DESCRIPTION = p.DESCRIPTION,
                            .FAX = p.FAX,
                            .ACTFLG = If(p.ACTFLG <> 0, "Áp dụng", "Ngừng áp dụng"),
                            .CREATED_DATE = p.CREATED_DATE,
                            .REMARK = p.REMARK}

            Dim lst = query
            If _filter.CODE <> "" Then
                lst = lst.Where(Function(p) p.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
            End If
            If _filter.NAME_EN <> "" Then
                lst = lst.Where(Function(p) p.NAME_EN.ToUpper.Contains(_filter.NAME_EN.ToUpper))
            End If
            If _filter.PHONE <> "" Then
                lst = lst.Where(Function(p) p.PHONE.ToUpper.Contains(_filter.PHONE.ToUpper))
            End If
            If _filter.REPRESENT <> "" Then
                lst = lst.Where(Function(p) p.REPRESENT.ToUpper.Contains(_filter.REPRESENT.ToUpper))
            End If
            If _filter.TAX_CODE <> "" Then
                lst = lst.Where(Function(p) p.TAX_CODE.ToUpper.Contains(_filter.TAX_CODE.ToUpper))
            End If
            If _filter.TRAIN_FIELD <> "" Then
                lst = lst.Where(Function(p) p.TRAIN_FIELD.ToUpper.Contains(_filter.TRAIN_FIELD.ToUpper))
            End If
            If _filter.WEB <> "" Then
                lst = lst.Where(Function(p) p.WEB.ToUpper.Contains(_filter.WEB.ToUpper))
            End If
            If _filter.ADDRESS <> "" Then
                lst = lst.Where(Function(p) p.ADDRESS.ToUpper.Contains(_filter.ADDRESS.ToUpper))
            End If
            If _filter.CONTACT_EMAIL <> "" Then
                lst = lst.Where(Function(p) p.CONTACT_EMAIL.ToUpper.Contains(_filter.CONTACT_EMAIL.ToUpper))
            End If
            If _filter.CONTACT_PERSON <> "" Then
                lst = lst.Where(Function(p) p.CONTACT_PERSON.ToUpper.Contains(_filter.CONTACT_PERSON.ToUpper))
            End If
            If _filter.ADDRESS <> "" Then
                lst = lst.Where(Function(p) p.TAX_CODE.ToUpper.Contains(_filter.TAX_CODE.ToUpper))
            End If
            If _filter.CONTACT_PHONE <> "" Then
                lst = lst.Where(Function(p) p.CONTACT_PHONE.ToUpper.Contains(_filter.CONTACT_PHONE.ToUpper))
            End If
            If _filter.DESCRIPTION <> "" Then
                lst = lst.Where(Function(p) p.DESCRIPTION.ToUpper.Contains(_filter.DESCRIPTION.ToUpper))
            End If
            If _filter.FAX <> "" Then
                lst = lst.Where(Function(p) p.FAX.ToUpper.Contains(_filter.FAX.ToUpper))
            End If
            If _filter.ACTFLG IsNot Nothing Then
                lst = lst.Where(Function(p) p.ACTFLG = _filter.ACTFLG)
            End If
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetCenter")
            Throw ex
        End Try

    End Function

    Public Function InsertCenter(ByVal objCenter As CenterDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objCenterData As New TR_CENTER
        Try
            objCenterData.ID = Utilities.GetNextSequence(Context, Context.TR_CENTER.EntitySet.Name)
            objCenterData.CODE = objCenter.CODE
            objCenterData.NAME_EN = objCenter.NAME_EN
            objCenterData.NAME_VN = objCenter.NAME_VN
            objCenterData.PHONE = objCenter.PHONE
            objCenterData.REPRESENT = objCenter.REPRESENT
            objCenterData.TAX_CODE = objCenter.TAX_CODE
            objCenterData.TRAIN_FIELD = objCenter.TRAIN_FIELD
            objCenterData.WEB = objCenter.WEB
            objCenterData.ADDRESS = objCenter.ADDRESS
            objCenterData.CONTACT_EMAIL = objCenter.CONTACT_EMAIL
            objCenterData.CONTACT_PERSON = objCenter.CONTACT_PERSON
            objCenterData.CONTACT_PHONE = objCenter.CONTACT_PHONE
            objCenterData.DESCRIPTION = objCenter.DESCRIPTION
            objCenterData.FAX = objCenter.FAX
            objCenterData.ACTFLG = If(objCenter.ACTFLG = "True", True, False)
            objCenterData.REMARK = objCenter.REMARK
            Context.TR_CENTER.AddObject(objCenterData)
            Context.SaveChanges(log)
            gID = objCenterData.ID
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".InsertCenter")
            Throw ex
        End Try
    End Function

    Public Function ValidateCenter(ByVal _validate As CenterDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.TR_CENTER
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.TR_CENTER
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If

                Return (query Is Nothing)
            End If
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".ValidateCenter")
            Throw ex
        End Try
    End Function

    Public Function ValidateUsingCenter(ByVal lstID As List(Of Decimal))
        Dim query
        Try
            For Each i In lstID
                query = (From p In Context.TR_LECTURE
                         Where p.TR_CENTER_ID = i).Any
                If query Then
                    Return False
                End If
            Next
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".ValidateCenter")
            Throw ex
        End Try
    End Function

    Public Function ModifyCenter(ByVal objCenter As CenterDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objCenterData As New TR_CENTER With {.ID = objCenter.ID}
        Try
            Context.TR_CENTER.Attach(objCenterData)
            objCenterData.CODE = objCenter.CODE
            objCenterData.NAME_EN = objCenter.NAME_EN
            objCenterData.NAME_VN = objCenter.NAME_VN
            objCenterData.PHONE = objCenter.PHONE
            objCenterData.REPRESENT = objCenter.REPRESENT
            objCenterData.TAX_CODE = objCenter.TAX_CODE
            objCenterData.TRAIN_FIELD = objCenter.TRAIN_FIELD
            objCenterData.WEB = objCenter.WEB
            objCenterData.ADDRESS = objCenter.ADDRESS
            objCenterData.CONTACT_EMAIL = objCenter.CONTACT_EMAIL
            objCenterData.CONTACT_PERSON = objCenter.CONTACT_PERSON
            objCenterData.CONTACT_PHONE = objCenter.CONTACT_PHONE
            objCenterData.DESCRIPTION = objCenter.DESCRIPTION
            objCenterData.FAX = objCenter.FAX
            'objCenterData.ACTFLG = objCenter.ACTFLG
            objCenterData.REMARK = objCenter.REMARK
            Context.SaveChanges(log)
            gID = objCenterData.ID
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".ModifyCenter")
            Throw ex
        End Try

    End Function

    Public Function ActiveCenter(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As Boolean) As Boolean
        Dim lstData As List(Of TR_CENTER)
        Try
            lstData = (From p In Context.TR_CENTER Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                lstData(index).ACTFLG = bActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function DeleteCenter(ByVal lstCenter() As CenterDTO) As Boolean
        Dim lstCenterData As List(Of TR_CENTER)
        Dim lstIDCenter As List(Of Decimal) = (From p In lstCenter.ToList Select p.ID).ToList
        Dim count As Decimal = 0
        Dim strListID As String = lstIDCenter.Select(Function(x) x.ToString).Aggregate(Function(x, y) x & "," & y)
        Dim Sql = "SELECT COUNT(CENTER_ID) FROM TR_PLAN_CENTER WHERE CENTER_ID IN (" & strListID & ")"
        count = Context.ExecuteStoreQuery(Of Decimal)(Sql).FirstOrDefault
        If count > 0 Then
            Return False
        End If
        Try
            lstCenterData = (From p In Context.TR_CENTER Where lstIDCenter.Contains(p.ID)).ToList
            For index = 0 To lstCenterData.Count - 1
                Context.TR_CENTER.DeleteObject(lstCenterData(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteCenter")
            Throw ex
        End Try

    End Function

#End Region

#Region "Lecture"

    Public Function GetLecture(ByVal _filter As LectureDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of LectureDTO)

        Try
            Dim query = From p In Context.TR_LECTURE
                        From center In Context.TR_CENTER.Where(Function(f) f.ID = p.TR_CENTER_ID).DefaultIfEmpty
                        From emp In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.LECTURE_ID).DefaultIfEmpty
                        Select New LectureDTO With {
                            .ID = p.ID,
                            .LECTURE_CODE = If(p.LECTURE_ID IsNot Nothing, emp.EMPLOYEE_CODE, p.CODE),
                            .LECTURE_NAME = If(p.LECTURE_ID IsNot Nothing, emp.FULLNAME_VN, p.NAME),
                            .LECTURE_ID = p.LECTURE_ID,
                            .PHONE = p.PHONE,
                            .EMAIL = p.EMAIL,
                            .IS_LOCAL = p.IS_LOCAL,
                            .REMARK = p.REMARK,
                            .TR_CENTER_ID = p.TR_CENTER_ID,
                            .TR_CENTER_NAME = center.NAME_VN,
                            .ACTFLG = If(p.ACTFLG <> 0, "Áp dụng", "Ngừng áp dụng"),
                            .CREATED_DATE = p.CREATED_DATE,
                            .ADDRESS = p.ADDRESS,
                            .FILENAME = p.FILENAME,
                            .UPLOAD_FILE = p.UPLOAD_FILE,
                            .VENDOR_CODE = p.VENDOR_CODE,
                            .VENDOR_NAME = p.VENDOR_NAME,
                            .SERVICE = p.SERVICE,
                            .WEBSITE = p.WEBSITE}

            Dim lst = query
            If _filter.ADDRESS <> "" Then
                lst = lst.Where(Function(p) p.ADDRESS.ToUpper.Contains(_filter.ADDRESS.ToUpper))
            End If
            If _filter.VENDOR_CODE <> "" Then
                lst = lst.Where(Function(p) p.VENDOR_CODE.ToUpper.Contains(_filter.VENDOR_CODE.ToUpper))
            End If
            If _filter.VENDOR_NAME <> "" Then
                lst = lst.Where(Function(p) p.VENDOR_NAME.ToUpper.Contains(_filter.VENDOR_NAME.ToUpper))
            End If
            If _filter.SERVICE <> "" Then
                lst = lst.Where(Function(p) p.SERVICE.ToUpper.Contains(_filter.SERVICE.ToUpper))
            End If
            If _filter.WEBSITE <> "" Then
                lst = lst.Where(Function(p) p.WEBSITE.ToUpper.Contains(_filter.WEBSITE.ToUpper))
            End If
            If _filter.LECTURE_CODE <> "" Then
                lst = lst.Where(Function(p) p.LECTURE_CODE.ToUpper.Contains(_filter.LECTURE_CODE.ToUpper))
            End If
            If _filter.LECTURE_NAME <> "" Then
                lst = lst.Where(Function(p) p.LECTURE_NAME.ToUpper.Contains(_filter.LECTURE_NAME.ToUpper))
            End If
            If _filter.PHONE <> "" Then
                lst = lst.Where(Function(p) p.PHONE.ToUpper.Contains(_filter.PHONE.ToUpper))
            End If
            If _filter.EMAIL <> "" Then
                lst = lst.Where(Function(p) p.EMAIL.ToUpper.Contains(_filter.EMAIL.ToUpper))
            End If
            If _filter.TR_CENTER_NAME <> "" Then
                lst = lst.Where(Function(p) p.TR_CENTER_NAME.ToUpper.Contains(_filter.TR_CENTER_NAME.ToUpper))
            End If
            If _filter.REMARK <> "" Then
                lst = lst.Where(Function(p) p.REMARK.ToUpper.Contains(_filter.REMARK.ToUpper))
            End If
            If _filter.IS_LOCAL IsNot Nothing Then
                lst = lst.Where(Function(p) p.IS_LOCAL = _filter.IS_LOCAL)
            End If
            If _filter.ACTFLG IsNot Nothing Then
                lst = lst.Where(Function(p) p.ACTFLG = _filter.ACTFLG)
            End If
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetLecture")
            Throw ex
        End Try

    End Function

    Public Function InsertLecture(ByVal objLecture As LectureDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objLectureData As New TR_LECTURE
        Try
            objLectureData.ID = Utilities.GetNextSequence(Context, Context.TR_LECTURE.EntitySet.Name)
            objLectureData.CODE = objLecture.LECTURE_CODE
            objLectureData.NAME = objLecture.LECTURE_NAME
            objLectureData.PHONE = objLecture.PHONE
            objLectureData.ADDRESS = objLecture.ADDRESS
            objLectureData.VENDOR_CODE = objLecture.VENDOR_CODE
            objLectureData.VENDOR_NAME = objLecture.VENDOR_NAME
            objLectureData.WEBSITE = objLecture.WEBSITE
            objLectureData.SERVICE = objLecture.SERVICE
            objLectureData.UPLOAD_FILE = objLecture.UPLOAD_FILE
            objLectureData.FILENAME = objLecture.FILENAME
            objLectureData.EMAIL = objLecture.EMAIL
            objLectureData.IS_LOCAL = objLecture.IS_LOCAL
            objLectureData.REMARK = objLecture.REMARK
            objLectureData.TR_CENTER_ID = objLecture.TR_CENTER_ID
            objLectureData.LECTURE_ID = objLecture.LECTURE_ID
            objLectureData.ACTFLG = If(objLecture.ACTFLG = "True", True, False)
            Context.TR_LECTURE.AddObject(objLectureData)
            Context.SaveChanges(log)
            gID = objLectureData.ID
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".InsertLecture")
            Throw ex
        End Try
    End Function

    Public Function ValidateLecture(ByVal _validate As LectureDTO)
        Dim query
        Try
            If _validate.LECTURE_CODE <> "" Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.TR_LECTURE
                             Where p.CODE.ToUpper = _validate.LECTURE_CODE.ToUpper _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.TR_LECTURE
                             Where p.CODE.ToUpper = _validate.LECTURE_CODE.ToUpper).FirstOrDefault
                End If

                Return (query Is Nothing)
            End If
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".ValidateLecture")
            Throw ex
        End Try
    End Function

    Public Function ModifyLecture(ByVal objLecture As LectureDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objLectureData As New TR_LECTURE With {.ID = objLecture.ID}
        Try
            Context.TR_LECTURE.Attach(objLectureData)
            objLectureData.CODE = objLecture.LECTURE_CODE
            objLectureData.NAME = objLecture.LECTURE_NAME
            objLectureData.PHONE = objLecture.PHONE
            objLectureData.ADDRESS = objLecture.ADDRESS
            objLectureData.VENDOR_CODE = objLecture.VENDOR_CODE
            objLectureData.VENDOR_NAME = objLecture.VENDOR_NAME
            objLectureData.WEBSITE = objLecture.WEBSITE
            objLectureData.SERVICE = objLecture.SERVICE
            objLectureData.UPLOAD_FILE = objLecture.UPLOAD_FILE
            objLectureData.FILENAME = objLecture.FILENAME
            objLectureData.EMAIL = objLecture.EMAIL
            objLectureData.IS_LOCAL = objLecture.IS_LOCAL
            objLectureData.REMARK = objLecture.REMARK
            objLectureData.TR_CENTER_ID = objLecture.TR_CENTER_ID
            objLectureData.LECTURE_ID = objLecture.LECTURE_ID
            'objLectureData.ACTFLG = objLecture.ACTFLG
            Context.SaveChanges(log)
            gID = objLectureData.ID
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".ModifyLecture")
            Throw ex
        End Try

    End Function

    Public Function ActiveLecture(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As Boolean) As Boolean
        Dim lstData As List(Of TR_LECTURE)
        Try
            lstData = (From p In Context.TR_LECTURE Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                lstData(index).ACTFLG = bActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function DeleteLecture(ByVal lstLecture() As LectureDTO) As Boolean
        Dim lstLectureData As List(Of TR_LECTURE)
        Dim lstIDLecture As List(Of Decimal) = (From p In lstLecture.ToList Select p.ID).ToList
        Try
            lstLectureData = (From p In Context.TR_LECTURE Where lstIDLecture.Contains(p.ID)).ToList
            For index = 0 To lstLectureData.Count - 1
                Context.TR_LECTURE.DeleteObject(lstLectureData(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteLecture")
            Throw ex
        End Try

    End Function

#End Region

#Region "CommitAfterTrain"

    Public Function GetCommitAfterTrain(ByVal _filter As CommitAfterTrainDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CommitAfterTrainDTO)

        Try
            Dim query = From p In Context.TR_COMMIT_AFTER_TRAIN
                        From c In Context.OT_OTHER_LIST.Where(Function(f) p.TR_CURRENCY_ID = f.ID)
                        Select New CommitAfterTrainDTO With {
                            .ID = p.ID,
                            .COMMIT_WORK = p.COMMIT_WORK,
                            .COST_TRAIN_TO = p.COST_TRAIN_TO,
                            .COST_TRAIN_FROM = p.COST_TRAIN_FROM,
                            .TR_CURRENCY_ID = p.TR_CURRENCY_ID,
                            .TR_CURRENCY_NAME = c.NAME_VN,
                            .ACTFLG = If(p.ACTFLG <> 0, "Áp dụng", "Ngừng áp dụng"),
                            .CREATED_DATE = p.CREATED_DATE}

            Dim lst = query
            If _filter.TR_CURRENCY_NAME <> "" Then
                lst = lst.Where(Function(p) p.TR_CURRENCY_NAME.ToUpper.Contains(_filter.TR_CURRENCY_NAME.ToUpper))
            End If
            If _filter.COMMIT_WORK IsNot Nothing Then
                lst = lst.Where(Function(p) p.COMMIT_WORK = _filter.COMMIT_WORK)
            End If
            If _filter.COST_TRAIN_FROM IsNot Nothing Then
                lst = lst.Where(Function(p) p.COST_TRAIN_FROM = _filter.COST_TRAIN_FROM)
            End If
            If _filter.COST_TRAIN_FROM IsNot Nothing Then
                lst = lst.Where(Function(p) p.COST_TRAIN_FROM = _filter.COST_TRAIN_FROM)
            End If
            If _filter.ACTFLG IsNot Nothing Then
                lst = lst.Where(Function(p) p.ACTFLG = _filter.ACTFLG)
            End If
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetCommitAfterTrain")
            Throw ex
        End Try

    End Function

    Public Function InsertCommitAfterTrain(ByVal objCommitAfterTrain As CommitAfterTrainDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objCommitAfterTrainData As New TR_COMMIT_AFTER_TRAIN
        Try
            objCommitAfterTrainData.ID = Utilities.GetNextSequence(Context, Context.TR_COMMIT_AFTER_TRAIN.EntitySet.Name)
            objCommitAfterTrainData.COMMIT_WORK = objCommitAfterTrain.COMMIT_WORK
            objCommitAfterTrainData.COST_TRAIN_FROM = objCommitAfterTrain.COST_TRAIN_FROM
            objCommitAfterTrainData.COST_TRAIN_TO = objCommitAfterTrain.COST_TRAIN_TO
            objCommitAfterTrainData.TR_CURRENCY_ID = objCommitAfterTrain.TR_CURRENCY_ID
            objCommitAfterTrainData.ACTFLG = If(objCommitAfterTrain.ACTFLG = "True", True, False)
            Context.TR_COMMIT_AFTER_TRAIN.AddObject(objCommitAfterTrainData)
            Context.SaveChanges(log)
            gID = objCommitAfterTrainData.ID
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".InsertCommitAfterTrain")
            Throw ex
        End Try
    End Function

    Public Function ModifyCommitAfterTrain(ByVal objCommitAfterTrain As CommitAfterTrainDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objCommitAfterTrainData As New TR_COMMIT_AFTER_TRAIN With {.ID = objCommitAfterTrain.ID}
        Try
            Context.TR_COMMIT_AFTER_TRAIN.Attach(objCommitAfterTrainData)
            objCommitAfterTrainData.COMMIT_WORK = objCommitAfterTrain.COMMIT_WORK
            objCommitAfterTrainData.COST_TRAIN_FROM = objCommitAfterTrain.COST_TRAIN_FROM
            objCommitAfterTrainData.COST_TRAIN_TO = objCommitAfterTrain.COST_TRAIN_TO
            objCommitAfterTrainData.TR_CURRENCY_ID = objCommitAfterTrain.TR_CURRENCY_ID
            'objCommitAfterTrainData.ACTFLG = objCommitAfterTrain.ACTFLG
            Context.SaveChanges(log)
            gID = objCommitAfterTrainData.ID
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".ModifyCommitAfterTrain")
            Throw ex
        End Try

    End Function

    Public Function ActiveCommitAfterTrain(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As Boolean) As Boolean
        Dim lstData As List(Of TR_COMMIT_AFTER_TRAIN)
        Try
            lstData = (From p In Context.TR_COMMIT_AFTER_TRAIN Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                lstData(index).ACTFLG = bActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function DeleteCommitAfterTrain(ByVal lstCommitAfterTrain() As CommitAfterTrainDTO) As Boolean
        Dim lstCommitAfterTrainData As List(Of TR_COMMIT_AFTER_TRAIN)
        Dim lstIDCommitAfterTrain As List(Of Decimal) = (From p In lstCommitAfterTrain.ToList Select p.ID).ToList
        Try
            lstCommitAfterTrainData = (From p In Context.TR_COMMIT_AFTER_TRAIN Where lstIDCommitAfterTrain.Contains(p.ID)).ToList
            For index = 0 To lstCommitAfterTrainData.Count - 1
                Context.TR_COMMIT_AFTER_TRAIN.DeleteObject(lstCommitAfterTrainData(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteCommitAfterTrain")
            Throw ex
        End Try

    End Function

#End Region

#Region "Criteria"

    Public Function GetCriteria(ByVal _filter As CriteriaDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CriteriaDTO)

        Try
            Dim query = From p In Context.TR_CRITERIA
                        Select New CriteriaDTO With {
                            .ID = p.ID,
                            .CODE = p.CODE,
                            .NAME = p.NAME,
                            .POINT_MAX = p.POINT_MAX,
                            .REMARK = p.REMARK,
                            .ACTFLG = If(p.ACTFLG <> 0, "Áp dụng", "Ngừng áp dụng"),
                            .CREATED_DATE = p.CREATED_DATE}

            Dim lst = query
            If _filter.CODE <> "" Then
                lst = lst.Where(Function(p) p.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
            End If
            If _filter.NAME <> "" Then
                lst = lst.Where(Function(p) p.NAME.ToUpper.Contains(_filter.NAME.ToUpper))
            End If
            If _filter.REMARK <> "" Then
                lst = lst.Where(Function(p) p.REMARK.ToUpper.Contains(_filter.REMARK.ToUpper))
            End If
            If _filter.POINT_MAX IsNot Nothing Then
                lst = lst.Where(Function(p) p.POINT_MAX = _filter.POINT_MAX)
            End If
            If _filter.ACTFLG IsNot Nothing Then
                lst = lst.Where(Function(p) p.ACTFLG = _filter.ACTFLG)
            End If
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetCriteria")
            Throw ex
        End Try

    End Function

    Public Function InsertCriteria(ByVal objCriteria As CriteriaDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objCriteriaData As New TR_CRITERIA
        Try
            objCriteriaData.ID = Utilities.GetNextSequence(Context, Context.TR_CRITERIA.EntitySet.Name)
            objCriteriaData.CODE = objCriteria.CODE
            objCriteriaData.NAME = objCriteria.NAME
            objCriteriaData.REMARK = objCriteria.REMARK
            objCriteriaData.POINT_MAX = objCriteria.POINT_MAX
            objCriteriaData.ACTFLG = If(objCriteria.ACTFLG = "True", True, False)
            Context.TR_CRITERIA.AddObject(objCriteriaData)
            Context.SaveChanges(log)
            gID = objCriteriaData.ID
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".InsertCriteria")
            Throw ex
        End Try
    End Function

    Public Function ValidateCriteria(ByVal _validate As CriteriaDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.TR_CRITERIA
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.TR_CRITERIA
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If

                Return (query Is Nothing)
            End If
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".ValidateCriteria")
            Throw ex
        End Try
    End Function

    Public Function ModifyCriteria(ByVal objCriteria As CriteriaDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objCriteriaData As New TR_CRITERIA With {.ID = objCriteria.ID}
        Try
            Context.TR_CRITERIA.Attach(objCriteriaData)
            objCriteriaData.CODE = objCriteria.CODE
            objCriteriaData.NAME = objCriteria.NAME
            objCriteriaData.REMARK = objCriteria.REMARK
            objCriteriaData.POINT_MAX = objCriteria.POINT_MAX
            Context.SaveChanges(log)
            gID = objCriteriaData.ID
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".ModifyCriteria")
            Throw ex
        End Try

    End Function

    Public Function ActiveCriteria(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As Boolean) As Boolean
        Dim lstData As List(Of TR_CRITERIA)
        Try
            lstData = (From p In Context.TR_CRITERIA Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                lstData(index).ACTFLG = bActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function DeleteCriteria(ByVal lstCriteria() As CriteriaDTO) As Boolean
        Dim lstCriteriaData As List(Of TR_CRITERIA)
        Dim lstIDCriteria As List(Of Decimal) = (From p In lstCriteria.ToList Select p.ID).ToList
        Try
            lstCriteriaData = (From p In Context.TR_CRITERIA Where lstIDCriteria.Contains(p.ID)).ToList
            For index = 0 To lstCriteriaData.Count - 1
                Context.TR_CRITERIA.DeleteObject(lstCriteriaData(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteCriteria")
            Throw ex
        End Try

    End Function

#End Region

#Region "CriteriaGroup"

    Public Function GetCriteriaGroup(ByVal _filter As CriteriaGroupDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CriteriaGroupDTO)

        Try
            Dim query = From p In Context.TR_CRITERIA_GROUP
                        Select New CriteriaGroupDTO With {
                            .ID = p.ID,
                            .CODE = p.CODE,
                            .NAME = p.NAME,
                            .POINT_MAX = p.POINT_MAX,
                            .REMARK = p.REMARK,
                            .ACTFLG = If(p.ACTFLG <> 0, "Áp dụng", "Ngừng áp dụng"),
                            .CREATED_DATE = p.CREATED_DATE}

            Dim lst = query
            If _filter.CODE <> "" Then
                lst = lst.Where(Function(p) p.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
            End If
            If _filter.NAME <> "" Then
                lst = lst.Where(Function(p) p.NAME.ToUpper.Contains(_filter.NAME.ToUpper))
            End If
            If _filter.REMARK <> "" Then
                lst = lst.Where(Function(p) p.REMARK.ToUpper.Contains(_filter.REMARK.ToUpper))
            End If
            If _filter.POINT_MAX IsNot Nothing Then
                lst = lst.Where(Function(p) p.POINT_MAX = _filter.POINT_MAX)
            End If
            If _filter.ACTFLG IsNot Nothing Then
                lst = lst.Where(Function(p) p.ACTFLG = _filter.ACTFLG)
            End If
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetCriteriaGroup")
            Throw ex
        End Try

    End Function

    Public Function InsertCriteriaGroup(ByVal objCriteriaGroup As CriteriaGroupDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objCriteriaGroupData As New TR_CRITERIA_GROUP
        Try
            objCriteriaGroupData.ID = Utilities.GetNextSequence(Context, Context.TR_CRITERIA_GROUP.EntitySet.Name)
            objCriteriaGroupData.CODE = objCriteriaGroup.CODE
            objCriteriaGroupData.NAME = objCriteriaGroup.NAME
            objCriteriaGroupData.REMARK = objCriteriaGroup.REMARK
            objCriteriaGroupData.POINT_MAX = objCriteriaGroup.POINT_MAX
            objCriteriaGroupData.ACTFLG = If(objCriteriaGroup.ACTFLG = "True", True, False)
            Context.TR_CRITERIA_GROUP.AddObject(objCriteriaGroupData)
            Context.SaveChanges(log)
            gID = objCriteriaGroupData.ID
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".InsertCriteriaGroup")
            Throw ex
        End Try
    End Function

    Public Function ValidateCriteriaGroup(ByVal _validate As CriteriaGroupDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.TR_CRITERIA_GROUP
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.TR_CRITERIA_GROUP
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If

                Return (query Is Nothing)
            End If
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".ValidateCriteriaGroup")
            Throw ex
        End Try
    End Function

    Public Function ModifyCriteriaGroup(ByVal objCriteriaGroup As CriteriaGroupDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objCriteriaGroupData As New TR_CRITERIA_GROUP With {.ID = objCriteriaGroup.ID}
        Try
            Context.TR_CRITERIA_GROUP.Attach(objCriteriaGroupData)
            objCriteriaGroupData.CODE = objCriteriaGroup.CODE
            objCriteriaGroupData.NAME = objCriteriaGroup.NAME
            objCriteriaGroupData.REMARK = objCriteriaGroup.REMARK
            objCriteriaGroupData.POINT_MAX = objCriteriaGroup.POINT_MAX
            Context.SaveChanges(log)
            gID = objCriteriaGroupData.ID
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".ModifyCriteriaGroup")
            Throw ex
        End Try

    End Function

    Public Function ActiveCriteriaGroup(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As Boolean) As Boolean
        Dim lstData As List(Of TR_CRITERIA_GROUP)
        Try
            lstData = (From p In Context.TR_CRITERIA_GROUP Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                lstData(index).ACTFLG = bActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function DeleteCriteriaGroup(ByVal lstCriteriaGroup() As CriteriaGroupDTO) As Boolean
        Dim lstCriteriaGroupData As List(Of TR_CRITERIA_GROUP)
        Dim lstIDCriteriaGroup As List(Of Decimal) = (From p In lstCriteriaGroup.ToList Select p.ID).ToList
        Try
            lstCriteriaGroupData = (From p In Context.TR_CRITERIA_GROUP Where lstIDCriteriaGroup.Contains(p.ID)).ToList
            For index = 0 To lstCriteriaGroupData.Count - 1
                Context.TR_CRITERIA_GROUP.DeleteObject(lstCriteriaGroupData(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteCriteriaGroup")
            Throw ex
        End Try

    End Function

#End Region

#Region "AssessmentRate"

    Public Function GetAssessmentRate(ByVal _filter As AssessmentRateDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AssessmentRateDTO)

        Try
            Dim query = From p In Context.TR_ASSESSMENT_RATE
                        Select New AssessmentRateDTO With {
                            .ID = p.ID,
                            .NAME_VN = p.NAME_VN,
                            .NAME_EN = p.NAME_EN,
                            .POINT_FROM = p.POINT_FROM,
                            .POINT_TO = p.POINT_TO,
                            .REMARK = p.REMARK,
                            .ACTFLG = If(p.ACTFLG <> 0, "Áp dụng", "Ngừng áp dụng"),
                            .CREATED_DATE = p.CREATED_DATE}

            Dim lst = query
            If _filter.NAME_VN <> "" Then
                lst = lst.Where(Function(p) p.NAME_VN.ToUpper.Contains(_filter.NAME_VN.ToUpper))
            End If
            If _filter.NAME_EN <> "" Then
                lst = lst.Where(Function(p) p.NAME_EN.ToUpper.Contains(_filter.NAME_EN.ToUpper))
            End If
            If _filter.REMARK <> "" Then
                lst = lst.Where(Function(p) p.REMARK.ToUpper.Contains(_filter.REMARK.ToUpper))
            End If
            If _filter.POINT_FROM IsNot Nothing Then
                lst = lst.Where(Function(p) p.POINT_FROM = _filter.POINT_FROM)
            End If
            If _filter.POINT_TO IsNot Nothing Then
                lst = lst.Where(Function(p) p.POINT_TO = _filter.POINT_TO)
            End If
            If _filter.ACTFLG IsNot Nothing Then
                lst = lst.Where(Function(p) p.ACTFLG = _filter.ACTFLG)
            End If
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetAssessmentRate")
            Throw ex
        End Try

    End Function

    Public Function InsertAssessmentRate(ByVal objAssessmentRate As AssessmentRateDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objAssessmentRateData As New TR_ASSESSMENT_RATE
        Try
            objAssessmentRateData.ID = Utilities.GetNextSequence(Context, Context.TR_ASSESSMENT_RATE.EntitySet.Name)
            objAssessmentRateData.NAME_EN = objAssessmentRate.NAME_EN
            objAssessmentRateData.NAME_VN = objAssessmentRate.NAME_VN
            objAssessmentRateData.POINT_FROM = objAssessmentRate.POINT_FROM
            objAssessmentRateData.POINT_TO = objAssessmentRate.POINT_TO
            objAssessmentRateData.ACTFLG = If(objAssessmentRate.ACTFLG = "True", True, False)
            objAssessmentRateData.REMARK = objAssessmentRate.REMARK
            Context.TR_ASSESSMENT_RATE.AddObject(objAssessmentRateData)
            Context.SaveChanges(log)
            gID = objAssessmentRateData.ID
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".InsertAssessmentRate")
            Throw ex
        End Try
    End Function

    Public Function ModifyAssessmentRate(ByVal objAssessmentRate As AssessmentRateDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objAssessmentRateData As New TR_ASSESSMENT_RATE With {.ID = objAssessmentRate.ID}
        Try
            Context.TR_ASSESSMENT_RATE.Attach(objAssessmentRateData)
            objAssessmentRateData.NAME_EN = objAssessmentRate.NAME_EN
            objAssessmentRateData.NAME_VN = objAssessmentRate.NAME_VN
            objAssessmentRateData.POINT_FROM = objAssessmentRate.POINT_FROM
            objAssessmentRateData.POINT_TO = objAssessmentRate.POINT_TO
            objAssessmentRateData.REMARK = objAssessmentRate.REMARK
            Context.SaveChanges(log)
            gID = objAssessmentRateData.ID
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".ModifyAssessmentRate")
            Throw ex
        End Try

    End Function

    Public Function ActiveAssessmentRate(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As Boolean) As Boolean
        Dim lstData As List(Of TR_ASSESSMENT_RATE)
        Try
            lstData = (From p In Context.TR_ASSESSMENT_RATE Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                lstData(index).ACTFLG = bActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function DeleteAssessmentRate(ByVal lstAssessmentRate() As AssessmentRateDTO) As Boolean
        Dim lstAssessmentRateData As List(Of TR_ASSESSMENT_RATE)
        Dim lstIDAssessmentRate As List(Of Decimal) = (From p In lstAssessmentRate.ToList Select p.ID).ToList
        Try
            lstAssessmentRateData = (From p In Context.TR_ASSESSMENT_RATE Where lstIDAssessmentRate.Contains(p.ID)).ToList
            For index = 0 To lstAssessmentRateData.Count - 1
                Context.TR_ASSESSMENT_RATE.DeleteObject(lstAssessmentRateData(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteAssessmentRate")
            Throw ex
        End Try

    End Function

#End Region

#Region "AssessmentForm"

    Public Function GetAssessmentForm(ByVal _filter As AssessmentFormDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AssessmentFormDTO)

        Try
            Dim query = From p In Context.TR_ASSESSMENT_FORM
                        Select New AssessmentFormDTO With {
                            .ID = p.ID,
                            .NAME = p.NAME,
                            .REMARK = p.REMARK,
                            .CREATED_DATE = p.CREATED_DATE}

            Dim lst = query
            If _filter.NAME <> "" Then
                lst = lst.Where(Function(p) p.NAME.ToUpper.Contains(_filter.NAME.ToUpper))
            End If
            If _filter.REMARK <> "" Then
                lst = lst.Where(Function(p) p.REMARK.ToUpper.Contains(_filter.REMARK.ToUpper))
            End If
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetAssessmentForm")
            Throw ex
        End Try

    End Function

    Public Function InsertAssessmentForm(ByVal objAssessmentForm As AssessmentFormDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objAssessmentFormData As New TR_ASSESSMENT_FORM
        Try
            objAssessmentFormData.ID = Utilities.GetNextSequence(Context, Context.TR_ASSESSMENT_FORM.EntitySet.Name)
            objAssessmentFormData.NAME = objAssessmentForm.NAME
            objAssessmentFormData.REMARK = objAssessmentForm.REMARK
            Context.TR_ASSESSMENT_FORM.AddObject(objAssessmentFormData)
            Context.SaveChanges(log)
            gID = objAssessmentFormData.ID
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".InsertAssessmentForm")
            Throw ex
        End Try
    End Function

    Public Function ModifyAssessmentForm(ByVal objAssessmentForm As AssessmentFormDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objAssessmentFormData As New TR_ASSESSMENT_FORM With {.ID = objAssessmentForm.ID}
        Try
            Context.TR_ASSESSMENT_FORM.Attach(objAssessmentFormData)
            objAssessmentFormData.NAME = objAssessmentForm.NAME
            objAssessmentFormData.REMARK = objAssessmentForm.REMARK
            Context.SaveChanges(log)
            gID = objAssessmentFormData.ID
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".ModifyAssessmentForm")
            Throw ex
        End Try

    End Function

    Public Function DeleteAssessmentForm(ByVal lstAssessmentForm() As AssessmentFormDTO) As Boolean
        Dim lstAssessmentFormData As List(Of TR_ASSESSMENT_FORM)
        Dim lstIDAssessmentForm As List(Of Decimal) = (From p In lstAssessmentForm.ToList Select p.ID).ToList
        Try
            lstAssessmentFormData = (From p In Context.TR_ASSESSMENT_FORM Where lstIDAssessmentForm.Contains(p.ID)).ToList
            For index = 0 To lstAssessmentFormData.Count - 1
                Context.TR_ASSESSMENT_FORM.DeleteObject(lstAssessmentFormData(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteAssessmentForm")
            Throw ex
        End Try

    End Function

#End Region

#Region "Classification"

    Public Function GetClassification(ByVal _filter As ClassificationDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ClassificationDTO)

        Try
            Dim query = From p In Context.TR_CLASSIFICATION
                        From c In Context.OT_OTHER_LIST.Where(Function(f) p.TR_TYPE_ID = f.ID).DefaultIfEmpty
                        Select New ClassificationDTO With {
                            .ID = p.ID,
                            .CODE = p.CODE,
                            .TR_TYPE_ID = p.TR_TYPE_ID,
                            .TR_TYPE_NAME = c.NAME_VN,
                            .VALUE_FROM = p.VALUE_FROM,
                            .EFFECT_DATE = p.EFFECT_DATE,
                            .VALUE_TO = p.VALUE_TO,
                            .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                            .CREATED_DATE = p.CREATED_DATE}

            Dim lst = query
            If _filter.CODE <> "" Then
                lst = lst.Where(Function(p) p.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
            End If
            If _filter.VALUE_FROM IsNot Nothing Then
                lst = lst.Where(Function(p) p.VALUE_FROM.ToString().Contains(_filter.VALUE_FROM.ToString()))
            End If
            If _filter.VALUE_TO IsNot Nothing Then
                lst = lst.Where(Function(p) p.VALUE_TO.ToString().Contains(_filter.VALUE_TO.ToString()))
            End If
            If _filter.ACTFLG IsNot Nothing Then
                lst = lst.Where(Function(p) p.ACTFLG = _filter.ACTFLG)
            End If
            If _filter.EFFECT_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.EFFECT_DATE = _filter.EFFECT_DATE)
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

    Public Function InsertClassification(ByVal objClassification As ClassificationDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objClassificationData As New TR_CLASSIFICATION
        Try
            objClassificationData.ID = Utilities.GetNextSequence(Context, Context.TR_CLASSIFICATION.EntitySet.Name)
            objClassificationData.CODE = objClassification.CODE
            objClassificationData.TR_TYPE_ID = objClassification.TR_TYPE_ID
            objClassificationData.VALUE_FROM = objClassification.VALUE_FROM
            objClassificationData.VALUE_TO = objClassification.VALUE_TO
            objClassificationData.ACTFLG = objClassification.ACTFLG
            objClassificationData.EFFECT_DATE = objClassification.EFFECT_DATE
            Context.TR_CLASSIFICATION.AddObject(objClassificationData)
            Context.SaveChanges(log)
            gID = objClassificationData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTraining")
            Throw ex
        End Try
    End Function

    Public Function ValidateClassification(ByVal _validate As ClassificationDTO)
        Try
            Dim query = (From p In Context.TR_CLASSIFICATION
                         Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             AndAlso EntityFunctions.TruncateTime(p.EFFECT_DATE) = EntityFunctions.TruncateTime(_validate.EFFECT_DATE) _
                             And p.ID <> _validate.ID)
            Return query.Any
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTraining")
            Throw ex
        End Try
    End Function

    Public Function ModifyClassification(ByVal objClassification As ClassificationDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objClassificationData As New TR_CLASSIFICATION With {.ID = objClassification.ID}
        Try
            objClassificationData = (From p In Context.TR_CLASSIFICATION Where p.ID = objClassification.ID).FirstOrDefault
            objClassificationData.CODE = objClassification.CODE
            objClassificationData.TR_TYPE_ID = objClassification.TR_TYPE_ID
            objClassificationData.VALUE_FROM = objClassification.VALUE_FROM
            objClassificationData.VALUE_TO = objClassification.VALUE_TO
            objClassificationData.EFFECT_DATE = objClassification.EFFECT_DATE
            Context.SaveChanges(log)
            gID = objClassificationData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTraining")
            Throw ex
        End Try

    End Function

    Public Function ActiveClassification(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of TR_CLASSIFICATION)
        Try
            lstData = (From p In Context.TR_CLASSIFICATION Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                lstData(index).ACTFLG = bActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTraining")
            Throw ex
        End Try

    End Function


    Public Function DeleteClassification(ByVal lstClassification() As Decimal) As Boolean
        Dim lstClassificationData As List(Of TR_CLASSIFICATION)
        Try
            lstClassificationData = (From p In Context.TR_CLASSIFICATION Where lstClassification.Contains(p.ID)).ToList
            For index = 0 To lstClassificationData.Count - 1
                Context.TR_CLASSIFICATION.DeleteObject(lstClassificationData(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTraining")
            Throw ex
        End Try

    End Function

#End Region
#End Region

End Class
