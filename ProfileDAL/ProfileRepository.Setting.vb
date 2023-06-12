Imports System.Data.Objects
Imports System.Reflection
Imports Framework.Data
Imports Framework.Data.System.Linq.Dynamic

Partial Class ProfileRepository

#Region "Competency Course"

    Public Function GetCompetencyCourse(ByVal _filter As CompetencyCourseDTO,
                             ByVal PageIndex As Integer,
                             ByVal PageSize As Integer,
                             ByRef Total As Integer,
                             Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CompetencyCourseDTO)

        Try
            Dim query = From p In Context.HU_COMPETENCY_COURSE
                        From Competencygroup In Context.HU_COMPETENCY_GROUP.Where(Function(f) f.ID = p.COMPETENCY_GROUP_ID).DefaultIfEmpty
                        From Competency In Context.HU_COMPETENCY.Where(Function(f) f.ID = p.COMPETENCY_ID).DefaultIfEmpty
                        From Course In Context.TR_COURSE.Where(Function(f) f.ID = p.TR_COURSE_ID).DefaultIfEmpty
                        Select New CompetencyCourseDTO With {
                            .ID = p.ID,
                            .COMPETENCY_GROUP_ID = p.COMPETENCY_GROUP_ID,
                            .COMPETENCY_GROUP_NAME = Competencygroup.NAME,
                            .COMPETENCY_ID = p.COMPETENCY_ID,
                            .COMPETENCY_NAME = Competency.NAME,
                            .LEVEL_NUMBER = p.LEVEL_NUMBER,
                            .TR_COURSE_ID = p.TR_COURSE_ID,
                            .TR_COURSE_NAME = Course.NAME,
                            .CREATED_DATE = p.CREATED_DATE}

            Dim lst = query

            If Not String.IsNullOrEmpty(_filter.COMPETENCY_GROUP_NAME) Then
                lst = lst.Where(Function(p) p.COMPETENCY_GROUP_NAME.ToUpper.Contains(_filter.COMPETENCY_GROUP_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.COMPETENCY_NAME) Then
                lst = lst.Where(Function(p) p.COMPETENCY_NAME.ToUpper.Contains(_filter.COMPETENCY_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.TR_COURSE_NAME) Then
                lst = lst.Where(Function(p) p.TR_COURSE_NAME.ToUpper.Contains(_filter.TR_COURSE_NAME.ToUpper))
            End If
            If _filter.LEVEL_NUMBER IsNot Nothing Then
                lst = lst.Where(Function(p) p.LEVEL_NUMBER = _filter.LEVEL_NUMBER)
            End If
            'If _filter.COMPETENCY_GROUP_ID IsNot Nothing Then
            lst = lst.Where(Function(p) p.COMPETENCY_GROUP_ID = _filter.COMPETENCY_GROUP_ID)
            'End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iCompetency")
            Throw ex
        End Try
    End Function
    Public Function InsertCompetencyCourse(ByVal obj As CompetencyCourseDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objData As New HU_COMPETENCY_COURSE
        Dim iCount As Integer = 0
        Try
            objData = (From p In Context.HU_COMPETENCY_COURSE
                       Where p.COMPETENCY_ID = obj.COMPETENCY_ID And
                                       p.COMPETENCY_GROUP_ID = obj.COMPETENCY_GROUP_ID).FirstOrDefault

            If objData IsNot Nothing Then
                objData.LEVEL_NUMBER = obj.LEVEL_NUMBER
                objData.TR_COURSE_ID = obj.TR_COURSE_ID
            Else
                objData = New HU_COMPETENCY_COURSE
                objData.ID = Utilities.GetNextSequence(Context, Context.HU_COMPETENCY_COURSE.EntitySet.Name)
                objData.COMPETENCY_GROUP_ID = obj.COMPETENCY_GROUP_ID
                objData.COMPETENCY_ID = obj.COMPETENCY_ID
                objData.LEVEL_NUMBER = obj.LEVEL_NUMBER
                objData.TR_COURSE_ID = obj.TR_COURSE_ID
                Context.HU_COMPETENCY_COURSE.AddObject(objData)
            End If
            Context.SaveChanges(log)
            gID = objData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iCompetency")
            Throw ex
        End Try

    End Function
    Public Function ModifyCompetencyCourse(ByVal obj As CompetencyCourseDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objData As HU_COMPETENCY_COURSE
        Try
            objData = (From p In Context.HU_COMPETENCY_COURSE Where p.ID = obj.ID).FirstOrDefault
            objData.COMPETENCY_GROUP_ID = obj.COMPETENCY_GROUP_ID
            objData.COMPETENCY_ID = obj.COMPETENCY_ID
            objData.LEVEL_NUMBER = obj.LEVEL_NUMBER
            objData.TR_COURSE_ID = obj.TR_COURSE_ID
            Context.SaveChanges(log)
            gID = objData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iCompetency")
            Throw ex
        End Try

    End Function

    Public Function DeleteCompetencyCourse(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim lstCompetencyCourseData As List(Of HU_COMPETENCY_COURSE)
        Try

            lstCompetencyCourseData = (From p In Context.HU_COMPETENCY_COURSE Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstCompetencyCourseData.Count - 1
                Context.HU_COMPETENCY_COURSE.DeleteObject(lstCompetencyCourseData(index))
            Next

            Context.SaveChanges(log)
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iCompetency")
            Throw ex
        End Try

    End Function

#End Region


#Region "Salary Items Percent"

    Public Function GetSalItemsPercent(ByVal _filter As SalaryItemsPercentDTO, ByVal PageIndex As Integer,
                                       ByVal PageSize As Integer,
                                       ByRef Total As Integer, ByVal _param As ParamDTO,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc",
                                       Optional ByVal log As UserLog = Nothing) As List(Of SalaryItemsPercentDTO)

        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using
            Dim query = From p In Context.HU_SETUP_SALARYITEMSPERCENT
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = p.ORG_ID And
                                                                       f.USERNAME = log.Username.ToUpper)
                        Select New SalaryItemsPercentDTO With {
                            .ID = p.ID,
                            .ORG_ID = p.ORG_ID,
                            .ORG_NAME = o.NAME_VN,
                            .EFFECT_DATE = p.EFFECT_DATE,
                            .LUONGCB = p.LUONGCB.Replace(".", ","),
                            .XANGXE = p.XANGXE.Replace(".", ","),
                            .DIENTHOAI = p.DIENTHOAI.Replace(".", ","),
                            .LUONGBS = p.LUONGBS.Replace(".", ","),
                            .THUONGYTCLCV = p.THUONGYTCLCV.Replace(".", ","),
                            .OTHER1 = p.OTHER1.Replace(".", ","),
                            .OTHER2 = p.OTHER2.Replace(".", ","),
                            .OTHER3 = p.OTHER3.Replace(".", ","),
                            .OTHER4 = p.OTHER4.Replace(".", ","),
                            .OTHER5 = p.OTHER5.Replace(".", ","),
                            .UNUSE_RATIO = p.UNUSE_RATIO,
                            .NOTE = p.NOTE,
                            .ACTFLG = p.ACTFLG,
                            .VIOLATE = p.VIOLATE,
                            .UNION_PERCENT = p.UNION_PERCENT,
                            .UNION_PERMANENT = p.UNION_PERMANENT,
                            .UNION_MONEY = p.UNION_MONEY,
                            .ACTFLG_NAME = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                            .CREATED_DATE = p.CREATED_DATE}

            Dim lst = query

            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                lst = lst.Where(Function(p) p.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.LUONGCB) Then
                lst = lst.Where(Function(p) p.LUONGCB.ToUpper.Contains(_filter.LUONGCB.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.XANGXE) Then
                lst = lst.Where(Function(p) p.XANGXE.ToUpper.Contains(_filter.XANGXE.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.DIENTHOAI) Then
                lst = lst.Where(Function(p) p.DIENTHOAI.ToUpper.Contains(_filter.DIENTHOAI.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.LUONGBS) Then
                lst = lst.Where(Function(p) p.LUONGBS.ToUpper.Contains(_filter.LUONGBS.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.THUONGYTCLCV) Then
                lst = lst.Where(Function(p) p.THUONGYTCLCV.ToUpper.Contains(_filter.THUONGYTCLCV.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.OTHER1) Then
                lst = lst.Where(Function(p) p.OTHER1.ToUpper.Contains(_filter.OTHER1.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.OTHER2) Then
                lst = lst.Where(Function(p) p.OTHER2.ToUpper.Contains(_filter.OTHER2.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.OTHER3) Then
                lst = lst.Where(Function(p) p.OTHER3.ToUpper.Contains(_filter.OTHER3.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.OTHER4) Then
                lst = lst.Where(Function(p) p.OTHER4.ToUpper.Contains(_filter.OTHER4.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.OTHER5) Then
                lst = lst.Where(Function(p) p.OTHER5.ToUpper.Contains(_filter.OTHER5.ToUpper))
            End If
            If _filter.UNION_MONEY IsNot Nothing Then
                lst = lst.Where(Function(p) p.UNION_MONEY = _filter.UNION_MONEY)
            End If
            If Not String.IsNullOrEmpty(_filter.NOTE) Then
                lst = lst.Where(Function(p) p.NOTE.ToUpper.Contains(_filter.NOTE.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.ACTFLG_NAME) Then
                lst = lst.Where(Function(p) p.ACTFLG_NAME.ToUpper.Contains(_filter.ACTFLG_NAME.ToUpper))
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "GetSalItemsPercent")
            Throw ex
        End Try
    End Function

    Public Function GetPaymentListAll(Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PAPaymentListDTO)
        Try

            Dim query = From p In Context.PA_PAYMENT_LIST
            Dim lst = query.Select(Function(p) New PAPaymentListDTO With {
                                        .ID = p.ID,
                                        .OBJ_PAYMENT_ID = p.OBJ_PAYMENT_ID,
                                        .CODE = p.CODE,
                                        .NAME = p.NAME,
                                        .EFFECTIVE_DATE = p.EFFECTIVE_DATE,
                                        .VALUE = p.VALUE,
                                        .SDESC = p.SDESC,
                                        .ACTFLG = p.ACTFLG,
                                        .ORDERS = p.ORDERS,
                                        .CREATED_DATE = p.CREATED_DATE
                                     })
            lst = lst.OrderBy(Sorts)
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function InsertSalItemsPercent(ByVal obj As SalaryItemsPercentDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objData As New HU_SETUP_SALARYITEMSPERCENT
        Try
            objData.ID = Utilities.GetNextSequence(Context, Context.HU_SETUP_SALARYITEMSPERCENT.EntitySet.Name)
            objData.ORG_ID = obj.ORG_ID
            objData.EFFECT_DATE = obj.EFFECT_DATE
            objData.LUONGCB = obj.LUONGCB
            objData.XANGXE = obj.XANGXE
            objData.DIENTHOAI = obj.DIENTHOAI
            objData.LUONGBS = obj.LUONGBS
            objData.THUONGYTCLCV = obj.THUONGYTCLCV
            objData.OTHER1 = obj.OTHER1
            objData.OTHER2 = obj.OTHER2
            objData.OTHER3 = obj.OTHER3
            objData.OTHER4 = obj.OTHER4
            objData.OTHER5 = obj.OTHER5
            objData.NOTE = obj.NOTE
            objData.ACTFLG = obj.ACTFLG
            objData.UNUSE_RATIO = obj.UNUSE_RATIO
            objData.VIOLATE = obj.VIOLATE
            objData.UNION_PERCENT = obj.UNION_PERCENT
            objData.UNION_PERMANENT = obj.UNION_PERMANENT
            objData.UNION_MONEY = obj.UNION_MONEY
            Context.HU_SETUP_SALARYITEMSPERCENT.AddObject(objData)
            Context.SaveChanges(log)
            gID = objData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "InsertSalItemsPercent")
            Throw ex
        End Try

    End Function

    Public Function ModifySalItemsPercent(ByVal obj As SalaryItemsPercentDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objData As HU_SETUP_SALARYITEMSPERCENT
        Try
            objData = (From p In Context.HU_SETUP_SALARYITEMSPERCENT Where p.ID = obj.ID).FirstOrDefault
            'objData.ORG_ID = obj.ORG_ID
            objData.EFFECT_DATE = obj.EFFECT_DATE
            objData.LUONGCB = obj.LUONGCB
            objData.XANGXE = obj.XANGXE
            objData.DIENTHOAI = obj.DIENTHOAI
            objData.LUONGBS = obj.LUONGBS
            objData.THUONGYTCLCV = obj.THUONGYTCLCV
            objData.OTHER1 = obj.OTHER1
            objData.OTHER2 = obj.OTHER2
            objData.OTHER3 = obj.OTHER3
            objData.OTHER4 = obj.OTHER4
            objData.OTHER5 = obj.OTHER5
            objData.NOTE = obj.NOTE
            objData.ACTFLG = obj.ACTFLG
            objData.UNUSE_RATIO = obj.UNUSE_RATIO
            objData.UNION_PERCENT = obj.UNION_PERCENT
            objData.UNION_PERMANENT = obj.UNION_PERMANENT
            objData.UNION_MONEY = obj.UNION_MONEY
            objData.VIOLATE = obj.VIOLATE
            Context.SaveChanges(log)
            gID = objData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "ModifySalItemsPercent")
            Throw ex
        End Try

    End Function

    Public Function ValidateSalItemsPercent(ByVal obj As SalaryItemsPercentDTO) As Boolean
        Try
            Dim query = From p In Context.HU_SETUP_SALARYITEMSPERCENT Where p.ID <> obj.ID AndAlso p.ORG_ID = obj.ORG_ID AndAlso
                         EntityFunctions.TruncateTime(p.EFFECT_DATE) = EntityFunctions.TruncateTime(obj.EFFECT_DATE)
            Return query.Any
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "ValidateSalItemsPercent")
            Throw ex
        End Try

    End Function

    Public Function DeleteSalItemsPercent(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim lstItems As List(Of HU_SETUP_SALARYITEMSPERCENT)
        Try

            lstItems = (From p In Context.HU_SETUP_SALARYITEMSPERCENT Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstItems.Count - 1
                Context.HU_SETUP_SALARYITEMSPERCENT.DeleteObject(lstItems(index))
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "DeleteSalItemsPercent")
            Throw ex
        End Try

    End Function

    Public Function ActiveSalItemsPercent(ByVal lstID As List(Of Decimal), ByVal status As String, ByVal log As UserLog) As Boolean
        Dim lstItems As List(Of HU_SETUP_SALARYITEMSPERCENT)
        Try

            lstItems = (From p In Context.HU_SETUP_SALARYITEMSPERCENT Where lstID.Contains(p.ID)).ToList
            For Each item In lstItems
                Dim objData As HU_SETUP_SALARYITEMSPERCENT
                objData = (From p In Context.HU_SETUP_SALARYITEMSPERCENT Where p.ID = item.ID).FirstOrDefault
                objData.ACTFLG = status
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "ActiveSalItemsPercent")
            Throw ex
        End Try

    End Function

#End Region
End Class
