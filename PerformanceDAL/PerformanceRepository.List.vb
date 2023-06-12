Imports System.Data.Objects
Imports System.Reflection
Imports Framework.Data
Imports Framework.Data.System.Linq.Dynamic

Partial Class PerformanceRepository

#Region "Criteria"

    Public Function GetCriteria(ByVal _filter As CriteriaDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CriteriaDTO)

        Try
            Dim query = From p In Context.PE_CRITERIA
                        From u In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.UNIT_ID).DefaultIfEmpty
                        From fr In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.FREQUENCY_ID).DefaultIfEmpty
                        From s In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.SOURCE_ID).DefaultIfEmpty
                        Select New CriteriaDTO With {
                            .ID = p.ID,
                            .CODE = p.CODE,
                            .NAME = p.NAME,
                            .REMARK = p.REMARK,
                            .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                            .CREATED_DATE = p.CREATED_DATE,
                            .UNIT_ID = p.UNIT_ID,
                            .UNIT_NAME = u.NAME_VN,
                            .FREQUENCY_ID = p.FREQUENCY_ID,
                            .FREQUENCY_NAME = fr.NAME_VN,
                            .SOURCE_ID = p.SOURCE_ID,
                            .SOURCE_NAME = s.NAME_VN}

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
            If _filter.SOURCE_NAME <> "" Then
                lst = lst.Where(Function(p) p.SOURCE_NAME.ToUpper.Contains(_filter.SOURCE_NAME.ToUpper))
            End If
            If _filter.FREQUENCY_NAME <> "" Then
                lst = lst.Where(Function(p) p.FREQUENCY_NAME.ToUpper.Contains(_filter.FREQUENCY_NAME.ToUpper))
            End If
            If _filter.UNIT_NAME <> "" Then
                lst = lst.Where(Function(p) p.UNIT_NAME.ToUpper.Contains(_filter.UNIT_NAME.ToUpper))
            End If
            If _filter.ACTFLG IsNot Nothing Then
                lst = lst.Where(Function(p) p.ACTFLG = _filter.ACTFLG)
            End If
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try

    End Function


    Public Function InsertCriteria(ByVal objCriteria As CriteriaDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objCriteriaData As New PE_CRITERIA
        Try
            objCriteriaData.ID = Utilities.GetNextSequence(Context, Context.PE_CRITERIA.EntitySet.Name)
            objCriteriaData.CODE = objCriteria.CODE
            objCriteriaData.NAME = objCriteria.NAME
            objCriteriaData.REMARK = objCriteria.REMARK
            objCriteriaData.ACTFLG = objCriteria.ACTFLG
            objCriteriaData.UNIT_ID = objCriteria.UNIT_ID
            objCriteriaData.FREQUENCY_ID = objCriteria.FREQUENCY_ID
            objCriteriaData.SOURCE_ID = objCriteria.SOURCE_ID
            Context.PE_CRITERIA.AddObject(objCriteriaData)
            Context.SaveChanges(log)
            gID = objCriteriaData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try
    End Function

    Public Function ValidateCriteria(ByVal _validate As CriteriaDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.PE_CRITERIA
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.PE_CRITERIA
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If

                Return (query Is Nothing)
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try
    End Function

    Public Function ModifyCriteria(ByVal objCriteria As CriteriaDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objCriteriaData As New PE_CRITERIA With {.ID = objCriteria.ID}
        Try
            objCriteriaData = (From p In Context.PE_CRITERIA Where p.ID = objCriteria.ID).FirstOrDefault
            objCriteriaData.CODE = objCriteria.CODE
            objCriteriaData.NAME = objCriteria.NAME
            objCriteriaData.REMARK = objCriteria.REMARK
            objCriteriaData.UNIT_ID = objCriteria.UNIT_ID
            objCriteriaData.FREQUENCY_ID = objCriteria.FREQUENCY_ID
            objCriteriaData.SOURCE_ID = objCriteria.SOURCE_ID
            Context.SaveChanges(log)
            gID = objCriteriaData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try

    End Function

    Public Function ActiveCriteria(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of PE_CRITERIA)
        Try
            lstData = (From p In Context.PE_CRITERIA Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                lstData(index).ACTFLG = bActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try

    End Function

    Public Function DeleteCriteria(ByVal lstCriteria() As Decimal) As Boolean
        Dim lstCriteriaData As List(Of PE_CRITERIA)
        Try
            lstCriteriaData = (From p In Context.PE_CRITERIA Where lstCriteria.Contains(p.ID)).ToList
            For index = 0 To lstCriteriaData.Count - 1
                Context.PE_CRITERIA.DeleteObject(lstCriteriaData(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try

    End Function
    Public Function GetPE_Criteria_HTCH(ByVal _filter As PE_Criteria_HTCHDTO, ByVal PageIndex As Integer,
                                       ByVal PageSize As Integer,
                                       ByRef Total As Integer,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PE_Criteria_HTCHDTO)

        Try
            Dim query = From p In Context.PE_CRITERIA_HTCH
                        Select New PE_Criteria_HTCHDTO With {
                            .ID = p.ID,
                            .CODE = p.CODE,
                            .NAME = p.NAME,
                            .DESCRIPTION = p.DESCRIPTION,
                            .IS_CHECK = p.IS_CHECK,
                            .CREATED_DATE = p.CREATED_DATE,
                            .IS_KYLUAT = p.IS_KYLUAT}

            Dim lst = query
            If _filter.CODE <> "" Then
                lst = lst.Where(Function(p) p.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
            End If
            If _filter.NAME <> "" Then
                lst = lst.Where(Function(p) p.NAME.ToUpper.Contains(_filter.NAME.ToUpper))
            End If
            If _filter.DESCRIPTION <> "" Then
                lst = lst.Where(Function(p) p.DESCRIPTION.ToUpper.Contains(_filter.DESCRIPTION.ToUpper))
            End If
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try

    End Function
    Public Function InsertPE_Criteria_HTCH(ByVal objCriteria As PE_Criteria_HTCHDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objCriteriaData As New PE_CRITERIA_HTCH
        Try
            objCriteriaData.ID = Utilities.GetNextSequence(Context, Context.PE_CRITERIA_HTCH.EntitySet.Name)
            objCriteriaData.CODE = objCriteria.CODE
            objCriteriaData.NAME = objCriteria.NAME
            objCriteriaData.DESCRIPTION = objCriteria.DESCRIPTION
            objCriteriaData.IS_CHECK = objCriteria.IS_CHECK
            objCriteriaData.IS_KYLUAT = objCriteria.IS_KYLUAT
            Context.PE_CRITERIA_HTCH.AddObject(objCriteriaData)
            Context.SaveChanges(log)
            gID = objCriteriaData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try
    End Function

    Public Function ModifyPE_Criteria_HTCH(ByVal objCriteria As PE_Criteria_HTCHDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objCriteriaData As New PE_CRITERIA_HTCH With {.ID = objCriteria.ID}
        Try
            objCriteriaData = (From p In Context.PE_CRITERIA_HTCH Where p.ID = objCriteria.ID).FirstOrDefault
            objCriteriaData.CODE = objCriteria.CODE
            objCriteriaData.NAME = objCriteria.NAME
            objCriteriaData.DESCRIPTION = objCriteria.DESCRIPTION
            objCriteriaData.IS_CHECK = objCriteria.IS_CHECK
            objCriteriaData.IS_KYLUAT = objCriteria.IS_KYLUAT
            Context.SaveChanges(log)
            gID = objCriteriaData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try

    End Function
    Public Function DeletePE_Criteria_HTCH(ByVal lstCriteria() As Decimal) As Boolean
        Dim lstCriteriaData As List(Of PE_CRITERIA_HTCH)
        Try
            lstCriteriaData = (From p In Context.PE_CRITERIA_HTCH Where lstCriteria.Contains(p.ID)).ToList
            For index = 0 To lstCriteriaData.Count - 1
                Context.PE_CRITERIA_HTCH.DeleteObject(lstCriteriaData(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try

    End Function
    Public Function ValidatePE_Criteria_HTCH(ByVal _validate As PE_Criteria_HTCHDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.PE_CRITERIA_HTCH
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.PE_CRITERIA_HTCH
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If

                Return (query Is Nothing)
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try
    End Function

    Public Function GET_CRITERIAL_DATA_IMPORT() As DataSet
        Try
            Using cls As New DataAccess.QueryData
                Dim dsData As DataSet = cls.ExecuteStore("PKG_PERFORMANCE_LIST.GET_CRITERIAL_DATA_IMPORT",
                                               New With {.P_CUR = cls.OUT_CURSOR,
                                                         .P_OUT1 = cls.OUT_CURSOR,
                                                         .P_OUT2 = cls.OUT_CURSOR}, False)
                Return dsData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try
    End Function

    Public Function IMPORT_CRITERIAL_DATA(ByVal log As UserLog, ByVal DATA_IN As String) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PERFORMANCE_LIST.IMPORT_CRITERIAL_DATA",
                                               New With {.P_DOCXML = DATA_IN,
                                                         .P_USER = log.Username.ToUpper,
                                                         .P_CUR = cls.OUT_CURSOR}, True)
                Return CBool(dtData(0)(0))
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
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
            Dim query = From p In Context.PE_CLASSIFICATION
                        Select New ClassificationDTO With {
                            .ID = p.ID,
                            .CODE = p.CODE,
                            .NAME = p.NAME,
                            .VALUE_FROM = p.VALUE_FROM,
                            .EFFECT_DATE = p.EFFECT_DATE,
                            .VALUE_TO = p.VALUE_TO,
                            .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                            .CREATED_DATE = p.CREATED_DATE}

            Dim lst = query
            If _filter.CODE <> "" Then
                lst = lst.Where(Function(p) p.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
            End If
            If _filter.NAME <> "" Then
                lst = lst.Where(Function(p) p.NAME.ToUpper.Contains(_filter.NAME.ToUpper))
            End If
            If _filter.VALUE_FROM <> Nothing Then
                lst = lst.Where(Function(p) p.VALUE_FROM.ToString().Contains(_filter.VALUE_FROM.ToString()))
            End If
            If _filter.VALUE_TO <> Nothing Then
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
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try

    End Function

    Public Function InsertClassification(ByVal objClassification As ClassificationDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objClassificationData As New PE_CLASSIFICATION
        Try
            objClassificationData.ID = Utilities.GetNextSequence(Context, Context.PE_CLASSIFICATION.EntitySet.Name)
            objClassificationData.CODE = objClassification.CODE
            objClassificationData.NAME = objClassification.NAME
            objClassificationData.VALUE_FROM = objClassification.VALUE_FROM
            objClassificationData.VALUE_TO = objClassification.VALUE_TO
            objClassificationData.ACTFLG = objClassification.ACTFLG
            objClassificationData.EFFECT_DATE = objClassification.EFFECT_DATE
            Context.PE_CLASSIFICATION.AddObject(objClassificationData)
            Context.SaveChanges(log)
            gID = objClassificationData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try
    End Function

    Public Function ValidateClassification(ByVal _validate As ClassificationDTO)
        Try
            Dim query = (From p In Context.PE_CLASSIFICATION
                         Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             AndAlso EntityFunctions.TruncateTime(p.EFFECT_DATE) = EntityFunctions.TruncateTime(_validate.EFFECT_DATE) _
                             And p.ID <> _validate.ID)
            Return query.Any
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try
    End Function

    Public Function ModifyClassification(ByVal objClassification As ClassificationDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objClassificationData As New PE_CLASSIFICATION With {.ID = objClassification.ID}
        Try
            objClassificationData = (From p In Context.PE_CLASSIFICATION Where p.ID = objClassification.ID).FirstOrDefault
            objClassificationData.CODE = objClassification.CODE
            objClassificationData.NAME = objClassification.NAME
            objClassificationData.VALUE_FROM = objClassification.VALUE_FROM
            objClassificationData.VALUE_TO = objClassification.VALUE_TO
            objClassificationData.EFFECT_DATE = objClassification.EFFECT_DATE
            Context.SaveChanges(log)
            gID = objClassificationData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try

    End Function

    Public Function ActiveClassification(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of PE_CLASSIFICATION)
        Try
            lstData = (From p In Context.PE_CLASSIFICATION Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                lstData(index).ACTFLG = bActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try

    End Function

    Public Function CalKPI(ByVal param As ParamDTO, ByVal log As UserLog) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PERFORMANCE_BUSINESS.CAL_KPI",
                                               New With {.P_USERNAME = log.Username.ToUpper,
                                                         .P_ORG_ID = param.ORG_ID,
                                                         .P_PERIOD_ID = param.PERIOD_ID,
                                                         .P_ISDISSOLVE = param.IS_DISSOLVE,
                                                         .P_CUR = cls.OUT_CURSOR})
                Return dtData
            End Using
            Return Nothing
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex

        End Try
    End Function
    Public Function CalKPI_Result(ByVal param As ParamDTO, ByVal log As UserLog) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PERFORMANCE_BUSINESS.CAL_KPI_RESULT",
                                               New With {.P_USERNAME = log.Username.ToUpper,
                                                         .P_ORG_ID = param.ORG_ID,
                                                         .P_PERIOD_ID = param.PERIOD_ID,
                                                         .P_ISDISSOLVE = param.IS_DISSOLVE,
                                                         .P_CUR = cls.OUT_CURSOR})
                Return dtData
            End Using
            Return Nothing
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex

        End Try
    End Function

    Public Function DeleteClassification(ByVal lstClassification() As Decimal) As Boolean
        Dim lstClassificationData As List(Of PE_CLASSIFICATION)
        Try
            lstClassificationData = (From p In Context.PE_CLASSIFICATION Where lstClassification.Contains(p.ID)).ToList
            For index = 0 To lstClassificationData.Count - 1
                Context.PE_CLASSIFICATION.DeleteObject(lstClassificationData(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try

    End Function

#End Region
#Region "Classification HTCH"

    Public Function GetClassificationHTCH(ByVal _filter As ClassificationHTCHDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ClassificationHTCHDTO)

        Try
            Dim query = From p In Context.PE_CLASSIFICATION_HTCH
                        Select New ClassificationHTCHDTO With {
                            .ID = p.ID,
                            .CODE = p.CODE,
                            .NAME = p.NAME,
                            .VALUE_FROM = p.VALUE_FROM,
                            .EFFECT_DATE = p.EFFECT_DATE,
                            .VALUE_TO = p.VALUE_TO,
                            .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                            .CREATED_DATE = p.CREATED_DATE}

            Dim lst = query
            If _filter.CODE <> "" Then
                lst = lst.Where(Function(p) p.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
            End If
            If _filter.NAME <> "" Then
                lst = lst.Where(Function(p) p.NAME.ToUpper.Contains(_filter.NAME.ToUpper))
            End If
            If _filter.VALUE_FROM <> Nothing Then
                lst = lst.Where(Function(p) p.VALUE_FROM.ToString().Contains(_filter.VALUE_FROM.ToString()))
            End If
            If _filter.VALUE_TO <> Nothing Then
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
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try

    End Function

    Public Function InsertClassificationHTCH(ByVal objClassification As ClassificationHTCHDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objClassificationData As New PE_CLASSIFICATION_HTCH
        Try
            objClassificationData.ID = Utilities.GetNextSequence(Context, Context.PE_CLASSIFICATION_HTCH.EntitySet.Name)
            objClassificationData.CODE = objClassification.CODE
            objClassificationData.NAME = objClassification.NAME
            objClassificationData.VALUE_FROM = objClassification.VALUE_FROM
            objClassificationData.VALUE_TO = objClassification.VALUE_TO
            objClassificationData.ACTFLG = objClassification.ACTFLG
            objClassificationData.EFFECT_DATE = objClassification.EFFECT_DATE
            Context.PE_CLASSIFICATION_HTCH.AddObject(objClassificationData)
            Context.SaveChanges(log)
            gID = objClassificationData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try
    End Function

    Public Function ValidateClassificationHTCH(ByVal _validate As ClassificationHTCHDTO)
        Try
            Dim query = (From p In Context.PE_CLASSIFICATION_HTCH
                         Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             AndAlso EntityFunctions.TruncateTime(p.EFFECT_DATE) = EntityFunctions.TruncateTime(_validate.EFFECT_DATE) _
                             And p.ID <> _validate.ID)
            Return query.Any
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try
    End Function

    Public Function ModifyClassificationHTCH(ByVal objClassification As ClassificationHTCHDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objClassificationData As New PE_CLASSIFICATION_HTCH With {.ID = objClassification.ID}
        Try
            objClassificationData = (From p In Context.PE_CLASSIFICATION_HTCH Where p.ID = objClassification.ID).FirstOrDefault
            objClassificationData.CODE = objClassification.CODE
            objClassificationData.NAME = objClassification.NAME
            objClassificationData.VALUE_FROM = objClassification.VALUE_FROM
            objClassificationData.VALUE_TO = objClassification.VALUE_TO
            objClassificationData.EFFECT_DATE = objClassification.EFFECT_DATE
            Context.SaveChanges(log)
            gID = objClassificationData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try

    End Function

    Public Function ActiveClassificationHTCH(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of PE_CLASSIFICATION_HTCH)
        Try
            lstData = (From p In Context.PE_CLASSIFICATION_HTCH Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                lstData(index).ACTFLG = bActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try

    End Function
    Public Function DeleteClassificationHTCH(ByVal lstClassification() As Decimal) As Boolean
        Dim lstClassificationData As List(Of PE_CLASSIFICATION_HTCH)
        Try
            lstClassificationData = (From p In Context.PE_CLASSIFICATION_HTCH Where lstClassification.Contains(p.ID)).ToList
            For index = 0 To lstClassificationData.Count - 1
                Context.PE_CLASSIFICATION_HTCH.DeleteObject(lstClassificationData(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try

    End Function

#End Region
#Region "ObjectGroup"

    Public Function GetObjectGroup(ByVal _filter As ObjectGroupDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ObjectGroupDTO)

        Try
            Dim query = From p In Context.PE_OBJECT_GROUP
                        Select New ObjectGroupDTO With {
                            .ID = p.ID,
                            .CODE = p.CODE,
                            .NAME = p.NAME,
                            .REMARK = p.REMARK,
                            .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
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
            If _filter.ACTFLG IsNot Nothing Then
                lst = lst.Where(Function(p) p.ACTFLG = _filter.ACTFLG)
            End If
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try

    End Function

    Public Function InsertObjectGroup(ByVal objObjectGroup As ObjectGroupDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objObjectGroupData As New PE_OBJECT_GROUP
        Try
            objObjectGroupData.ID = Utilities.GetNextSequence(Context, Context.PE_OBJECT_GROUP.EntitySet.Name)
            objObjectGroupData.CODE = objObjectGroup.CODE
            objObjectGroupData.NAME = objObjectGroup.NAME
            objObjectGroupData.REMARK = objObjectGroup.REMARK
            objObjectGroupData.ACTFLG = objObjectGroup.ACTFLG
            Context.PE_OBJECT_GROUP.AddObject(objObjectGroupData)
            Context.SaveChanges(log)
            gID = objObjectGroupData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try
    End Function

    Public Function ValidateObjectGroup(ByVal _validate As ObjectGroupDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.PE_OBJECT_GROUP
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.PE_OBJECT_GROUP
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If

                Return (query Is Nothing)
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try
    End Function

    Public Function ModifyObjectGroup(ByVal objObjectGroup As ObjectGroupDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objObjectGroupData As New PE_OBJECT_GROUP With {.ID = objObjectGroup.ID}
        Try
            objObjectGroupData = (From p In Context.PE_OBJECT_GROUP Where p.ID = objObjectGroup.ID).FirstOrDefault
            objObjectGroupData.CODE = objObjectGroup.CODE
            objObjectGroupData.NAME = objObjectGroup.NAME
            objObjectGroupData.REMARK = objObjectGroup.REMARK
            Context.SaveChanges(log)
            gID = objObjectGroupData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try

    End Function

    Public Function ActiveObjectGroup(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of PE_OBJECT_GROUP)
        Try
            lstData = (From p In Context.PE_OBJECT_GROUP Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                lstData(index).ACTFLG = bActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try

    End Function

    Public Function DeleteObjectGroup(ByVal lstObjectGroup() As Decimal) As Boolean
        Dim lstObjectGroupData As List(Of PE_OBJECT_GROUP)
        Try
            lstObjectGroupData = (From p In Context.PE_OBJECT_GROUP Where lstObjectGroup.Contains(p.ID)).ToList
            For index = 0 To lstObjectGroupData.Count - 1
                Context.PE_OBJECT_GROUP.DeleteObject(lstObjectGroupData(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try

    End Function

#End Region

#Region "Period"

    Public Function GetPeriod(ByVal _filter As PeriodDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PeriodDTO)

        Try
            Dim query = (From p In Context.PE_PERIOD
                         From s In Context.OT_OTHER_LIST.Where(Function(s) s.ID = p.TYPE_ASS)
                         From e In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.EMPLOYEE_TYPE).DefaultIfEmpty
                         Select New PeriodDTO With {
                             .ID = p.ID,
                             .CODE = p.CODE,
                             .NAME = p.NAME,
                             .TYPE_ASS_NAME = s.NAME_VN,
                             .TYPE_ASS = p.TYPE_ASS,
                             .REMARK = p.REMARK,
                             .START_DATE = p.START_DATE,
                             .END_DATE = p.END_DATE,
                             .EMPLOYEE_TYPE = p.EMPLOYEE_TYPE,
                             .EMPLOYEE_TYPE_NAME = e.NAME_VN,
                             .NUMBER_DAY = p.NUMBER_DAY,
                             .NUMBER_MONTH = p.NUMBER_MONTH,
                             .YEAR = p.YEAR,
                             .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                             .CREATED_DATE = p.CREATED_DATE})
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
            If _filter.START_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.START_DATE = _filter.START_DATE)
            End If
            If _filter.END_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.END_DATE = _filter.END_DATE)
            End If
            If _filter.ACTFLG IsNot Nothing Then
                lst = lst.Where(Function(p) p.ACTFLG = _filter.ACTFLG)
            End If
            If _filter.TYPE_ASS_NAME IsNot Nothing Then
                lst = lst.Where(Function(p) p.TYPE_ASS_NAME = _filter.TYPE_ASS_NAME)
            End If
            If _filter.TYPE_ASS IsNot Nothing Then
                lst = lst.Where(Function(p) p.TYPE_ASS = _filter.TYPE_ASS)
            End If
            If _filter.EMPLOYEE_TYPE IsNot Nothing Then
                lst = lst.Where(Function(p) p.EMPLOYEE_TYPE = _filter.EMPLOYEE_TYPE)
            End If
            If _filter.NUMBER_MONTH IsNot Nothing Then
                lst = lst.Where(Function(p) p.NUMBER_MONTH = _filter.NUMBER_MONTH)
            End If
            If _filter.NUMBER_DAY IsNot Nothing Then
                lst = lst.Where(Function(p) p.NUMBER_DAY = _filter.NUMBER_DAY)
            End If
            If _filter.EMPLOYEE_TYPE_NAME <> "" Then
                lst = lst.Where(Function(p) p.EMPLOYEE_TYPE_NAME.ToUpper.Contains(_filter.EMPLOYEE_TYPE_NAME.ToUpper))
            End If
            If _filter.YEAR IsNot Nothing Then
                lst = lst.Where(Function(p) p.YEAR = _filter.YEAR)
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try

    End Function
    Public Function GetPeriodById(ByVal _filter As PeriodDTO, ByVal PageIndex As Integer,
                                       ByVal PageSize As Integer,
                                       ByRef Total As Integer,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PeriodDTO)
        Try
            Dim query = (From s In Context.PE_PERIOD
                         From p In Context.PE_EMPLOYEE_ASSESSMENT.Where(Function(f) f.PERIOD_ID = s.ID And f.EMPLOYEE_ID = _filter.EMPLOYEE_ID)
                         Where s.ID = _filter.ID
                         Select New PeriodDTO With {
                         .ID = p.ID,
                         .CODE = s.CODE,
                         .NAME = s.NAME,
                         .TYPE_ASS = s.TYPE_ASS,
                         .EMPLOYEE_TYPE = s.EMPLOYEE_TYPE,
                         .NUMBER_DAY = s.NUMBER_DAY,
                         .NUMBER_MONTH = s.NUMBER_MONTH,
                         .REMARK = s.REMARK,
                         .START_DATE = s.START_DATE,
                         .END_DATE = s.END_DATE,
                         .YEAR = s.YEAR,
                         .OBJECT_GROUP_ID = p.OBJECT_GROUP_ID,
                         .PE_STATUS = p.PE_STATUS,
                         .CREATED_DATE = s.CREATED_DATE})
            Dim lst = query
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try

    End Function
    Public Function InsertPeriod(ByVal objPeriod As PeriodDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objPeriodData As New PE_PERIOD
        Try
            objPeriodData.ID = Utilities.GetNextSequence(Context, Context.PE_PERIOD.EntitySet.Name)
            objPeriodData.YEAR = objPeriod.YEAR
            objPeriodData.CODE = objPeriod.CODE
            objPeriodData.NAME = objPeriod.NAME
            objPeriodData.REMARK = objPeriod.REMARK
            objPeriodData.START_DATE = objPeriod.START_DATE
            objPeriodData.END_DATE = objPeriod.END_DATE
            objPeriodData.ACTFLG = objPeriod.ACTFLG
            objPeriodData.TYPE_ASS = objPeriod.TYPE_ASS
            objPeriodData.EMPLOYEE_TYPE = objPeriod.EMPLOYEE_TYPE
            objPeriodData.NUMBER_MONTH = objPeriod.NUMBER_MONTH
            objPeriodData.NUMBER_DAY = objPeriod.NUMBER_DAY
            Context.PE_PERIOD.AddObject(objPeriodData)
            Context.SaveChanges(log)
            gID = objPeriodData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try
    End Function

    Public Function ValidatePeriod(ByVal _validate As PeriodDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.PE_PERIOD
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.PE_PERIOD
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If

                Return (query Is Nothing)
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try
    End Function

    Public Function ModifyPeriod(ByVal objPeriod As PeriodDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objPeriodData As New PE_PERIOD With {.ID = objPeriod.ID}
        Try
            objPeriodData = (From p In Context.PE_PERIOD Where p.ID = objPeriod.ID).FirstOrDefault
            objPeriodData.YEAR = objPeriod.YEAR
            objPeriodData.CODE = objPeriod.CODE
            objPeriodData.NAME = objPeriod.NAME
            objPeriodData.REMARK = objPeriod.REMARK
            objPeriodData.START_DATE = objPeriod.START_DATE
            objPeriodData.END_DATE = objPeriod.END_DATE
            objPeriodData.TYPE_ASS = objPeriod.TYPE_ASS
            objPeriodData.EMPLOYEE_TYPE = objPeriod.EMPLOYEE_TYPE
            objPeriodData.NUMBER_MONTH = objPeriod.NUMBER_MONTH
            objPeriodData.NUMBER_DAY = objPeriod.NUMBER_DAY
            Context.SaveChanges(log)
            gID = objPeriodData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try

    End Function

    Public Function ActivePeriod(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of PE_PERIOD)
        Try
            lstData = (From p In Context.PE_PERIOD Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                lstData(index).ACTFLG = bActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function DeletePeriod(ByVal lstPeriod() As Decimal) As Boolean
        Dim lstPeriodData As List(Of PE_PERIOD)
        Try
            lstPeriodData = (From p In Context.PE_PERIOD Where lstPeriod.Contains(p.ID)).ToList
            For index = 0 To lstPeriodData.Count - 1
                Context.PE_PERIOD.DeleteObject(lstPeriodData(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try

    End Function

#End Region
#Region "Period HTCH"

    Public Function GetPeriodHTCH(ByVal _filter As PE_Period_HTCHDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PE_Period_HTCHDTO)

        Try
            Dim query = (From p In Context.PE_PERIOD_HTCH
                         Select New PE_Period_HTCHDTO With {
                             .ID = p.ID,
                             .CODE = p.CODE,
                             .NAME = p.NAME,
                             .REMARK = p.REMARK,
                             .START_DATE = p.START_DATE,
                             .END_DATE = p.END_DATE,
                             .YEAR = p.YEAR,
                             .CREATED_DATE = p.CREATED_DATE,
                             .MONTH = p.MONTH})
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
            If _filter.START_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.START_DATE = _filter.START_DATE)
            End If
            If _filter.END_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.END_DATE = _filter.END_DATE)
            End If
            If _filter.YEAR IsNot Nothing Then
                lst = lst.Where(Function(p) p.YEAR = _filter.YEAR)
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try

    End Function
    Public Function GetPeriodHTCHById(ByVal _filter As PE_Period_HTCHDTO, ByVal PageIndex As Integer,
                                       ByVal PageSize As Integer,
                                       ByRef Total As Integer,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PE_Period_HTCHDTO)
        Try
            Dim query = (From s In Context.PE_PERIOD_HTCH
                         Where (s.ID = _filter.ID)
                         Select New PE_Period_HTCHDTO With {
                         .ID = s.ID,
                         .CODE = s.CODE,
                         .NAME = s.NAME,
                         .REMARK = s.REMARK,
                         .START_DATE = s.START_DATE,
                         .END_DATE = s.END_DATE,
                         .YEAR = s.YEAR,
                         .MONTH = s.MONTH})
            Dim lst = query
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try

    End Function
    Public Function InsertPeriodHTCH(ByVal objPeriod As PE_Period_HTCHDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objPeriodData As New PE_PERIOD_HTCH
        Try
            objPeriodData.ID = Utilities.GetNextSequence(Context, Context.PE_PERIOD_HTCH.EntitySet.Name)
            objPeriodData.YEAR = objPeriod.YEAR
            objPeriodData.CODE = objPeriod.CODE
            objPeriodData.NAME = objPeriod.NAME
            objPeriodData.REMARK = objPeriod.REMARK
            objPeriodData.START_DATE = objPeriod.START_DATE
            objPeriodData.END_DATE = objPeriod.END_DATE
            objPeriodData.MONTH = objPeriod.MONTH
            Context.PE_PERIOD_HTCH.AddObject(objPeriodData)
            Context.SaveChanges(log)
            gID = objPeriodData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try
    End Function

    Public Function ValidatePeriodHTCH(ByVal _validate As PE_Period_HTCHDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.PE_PERIOD_HTCH
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.YEAR = _validate.YEAR _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.PE_PERIOD_HTCH
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.YEAR = _validate.YEAR).FirstOrDefault
                End If

                Return (query Is Nothing)
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try
    End Function

    Public Function ModifyPeriodHTCH(ByVal objPeriod As PE_Period_HTCHDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objPeriodData As New PE_PERIOD_HTCH With {.ID = objPeriod.ID}
        Try
            objPeriodData = (From p In Context.PE_PERIOD_HTCH Where p.ID = objPeriod.ID).FirstOrDefault
            objPeriodData.YEAR = objPeriod.YEAR
            objPeriodData.CODE = objPeriod.CODE
            objPeriodData.NAME = objPeriod.NAME
            objPeriodData.REMARK = objPeriod.REMARK
            objPeriodData.START_DATE = objPeriod.START_DATE
            objPeriodData.END_DATE = objPeriod.END_DATE
            objPeriodData.MONTH = objPeriod.MONTH
            Context.SaveChanges(log)
            gID = objPeriodData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try

    End Function


    Public Function DeletePeriodHTCH(ByVal lstPeriod() As Decimal) As Boolean
        Dim lstPeriodData As List(Of PE_PERIOD_HTCH)
        Try
            lstPeriodData = (From p In Context.PE_PERIOD_HTCH Where lstPeriod.Contains(p.ID)).ToList
            For index = 0 To lstPeriodData.Count - 1
                Context.PE_PERIOD_HTCH.DeleteObject(lstPeriodData(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try

    End Function

#End Region

End Class
