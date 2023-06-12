Imports System.Reflection
Imports Framework.Data
Imports Framework.Data.System.Linq.Dynamic

Partial Public Class PayrollRepository

#Region "IncentiveRank list"

#Region "Get IncentiveRank"

    Public Function GetIncentiveRank(ByVal _filter As IncentiveRankDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of IncentiveRankDTO)

        Try

            Dim query = From i In Context.PA_INCENTIVE_RANK
                        Join p In Context.PA_SALARY_RANK On i.SAL_RANK_ID Equals p.ID
                        Join o In Context.PA_SALARY_LEVEL On p.SAL_LEVEL_ID Equals o.ID
                        Join q In Context.PA_SALARY_GROUP On o.SAL_GROUP_ID Equals q.ID
                        Join l In Context.OT_OTHER_LIST On i.SAL_INCENTIVE_ID Equals l.ID
                        Where (i.SAL_GROUP_ID = _filter.SAL_GROUP_ID _
                                And o.OTHER_USE.Equals("U"))

            Dim lst = query.Select(Function(e) New IncentiveRankDTO With {
                                       .ID = e.i.ID,
                                       .SAL_GROUP_ID = e.i.SAL_GROUP_ID,
                                       .SAL_GROUP_NAME = e.q.NAME,
                                       .SAL_LEVEL_ID = e.i.SAL_LEVEL_ID,
                                       .SAL_LEVEL_NAME = e.o.NAME,
                                       .SAL_RANK_ID = e.i.SAL_RANK_ID,
                                       .RANK = e.p.RANK,
                                       .SAL_INCENTIVE_ID = e.i.SAL_INCENTIVE_ID,
                                       .SAL_INCENTIVE_NAME = e.l.NAME_VN,
                                       .EFFECT_DATE = e.i.EFFECT_DATE,
                                       .REMARK = e.i.REMARK,
                                       .ACTFLG = e.i.ACTFLG,
                                       .ORDERS = e.i.ORDERS,
                                       .CREATED_DATE = e.i.CREATED_DATE
                                   })

            If _filter.SAL_LEVEL_ID <> 0 Then
                lst = lst.Where(Function(p) p.SAL_LEVEL_ID = _filter.SAL_LEVEL_ID)
            End If

            If _filter.RANK <> 0 Then
                lst = lst.Where(Function(p) p.RANK = _filter.RANK)
            End If

            If _filter.ACTFLG <> "" Then
                lst = lst.Where(Function(p) p.ACTFLG.ToUpper.Contains(_filter.ACTFLG.ToUpper))
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
    Public Function GetIncentiveRankDetail(ByVal _filter As IncentiveRankDetailDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "TO_TARGET, CREATED_DATE desc") As List(Of IncentiveRankDetailDTO)

        Try

            Dim query = From i In Context.PA_INCENTIVE_RANK_DETAIL
                        Join p In Context.PA_INCENTIVE_RANK On i.INCENTIVE_RANK_ID Equals p.ID
                        Where i.INCENTIVE_RANK_ID = _filter.INCENTIVE_RANK_ID

            Dim lst = query.Select(Function(e) New IncentiveRankDetailDTO With {
                                                                 .ID = e.i.ID,
                                                                 .INCENTIVE_RANK_ID = e.i.INCENTIVE_RANK_ID,
                                                                 .FROM_TARGET = e.i.FROM_TARGET,
                                                                 .TO_TARGET = e.i.TO_TARGET,
                                                                 .AMOUNT = e.i.AMOUNT,
                                                                 .INCENTIVE_PERCENT = e.i.INCENTIVE_PERCENT,
                                                                 .INCENTIVE_AMOUNT = e.i.INCENTIVE_AMOUNT,
                                                                 .CREATED_DATE = e.i.CREATED_DATE,
                                                                 .REMARK = e.i.REMARK,
                                                                 .ACTFLG = e.i.ACTFLG,
                                                                 .ORDERS = e.i.ORDERS
                                                             })
            If _filter.ID <> 0 Then
                lst = lst.Where(Function(p) p.ID = _filter.ID)
            End If
            If _filter.FROM_TARGET <> 0 Then
                lst = lst.Where(Function(p) p.FROM_TARGET = _filter.FROM_TARGET)
            End If

            If _filter.TO_TARGET <> 0 Then
                lst = lst.Where(Function(p) p.TO_TARGET = _filter.TO_TARGET)
            End If

            If _filter.AMOUNT <> 0 Then
                lst = lst.Where(Function(p) p.AMOUNT = _filter.AMOUNT)
            End If

            If _filter.ACTFLG <> "" Then
                lst = lst.Where(Function(p) p.ACTFLG.ToUpper.Contains(_filter.ACTFLG.ToUpper))
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
    Public Function GetIncentiveRankIncludeDetail(ByVal _filter As IncentiveRankDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of IncentiveRankDTO)

        Try

            'Dim query = From i In Context.PA_INCENTIVE_RANK.Include("PA_INCENTIVE_RANK_DETAIL")
            '            Join p In Context.PA_SALARY_RANK On i.SAL_RANK_ID Equals p.ID
            '            Join o In Context.PA_SALARY_LEVEL On p.SAL_LEVEL_ID Equals o.ID
            '            Join q In Context.PA_SALARY_GROUP On o.SAL_GROUP_ID Equals q.ID
            '            Join l In Context.OT_OTHER_LIST On i.SAL_INCENTIVE_ID Equals l.ID
            '            Join t In Context.OT_OTHER_LIST_TYPE On l.TYPE_ID Equals t.ID
            '            Where (i.SAL_GROUP_ID = _filter.SAL_GROUP_ID _
            '                    And t.CODE = "HU_TITLE_LEVEL" _
            '                    And o.OTHER_USE.Equals("U"))

            'Dim lst = query.Select(Function(e) New IncentiveRankDTO With {
            '                           .ID = e.i.ID,
            '                           .SAL_GROUP_ID = e.i.SAL_GROUP_ID,
            '                           .SAL_GROUP_NAME = e.q.NAME,
            '                           .SAL_LEVEL_ID = e.i.SAL_LEVEL_ID,
            '                           .SAL_LEVEL_NAME = e.o.NAME,
            '                           .SAL_RANK_ID = e.i.SAL_RANK_ID,
            '                           .RANK = e.p.RANK,
            '                           .SAL_INCENTIVE_ID = e.i.SAL_INCENTIVE_ID,
            '                           .SAL_INCENTIVE_NAME = e.l.NAME_VN,
            '                           .EFFECT_DATE = e.i.EFFECT_DATE,
            '                           .REMARK = e.p.REMARK,
            '                           .ACTFLG = If(e.p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
            '                           .ORDERS = e.p.ORDERS,
            '                           .CREATED_DATE = e.p.CREATED_DATE,
            '                            .INCENTIVERANKDETAIL = e.i.PA_INCENTIVE_RANK_DETAIL.Select(Function(detail) New IncentiveRankDetailDTO With {
            '                                        .ID = detail.ID,
            '                                        .INCENTIVE_RANK_ID = detail.INCENTIVE_RANK_ID,
            '                                        .FROM_TARGET = detail.FROM_TARGET,
            '                                        .TO_TARGET = detail.TO_TARGET,
            '                                        .AMOUNT = detail.AMOUNT,
            '                                        .INCENTIVE_PERCENT = detail.INCENTIVE_PERCENT,
            '                                        .INCENTIVE_AMOUNT = detail.INCENTIVE_AMOUNT,
            '                                        .CREATED_DATE = detail.CREATED_DATE,
            '                                        .REMARK = detail.REMARK,
            '                                        .ACTFLG = detail.ACTFLG,
            '                                        .ORDERS = detail.ORDERS
            '                                    })
            '                       })

            'If _filter.SAL_LEVEL_ID <> 0 Then
            '    lst = lst.Where(Function(p) p.SAL_LEVEL_ID = _filter.SAL_LEVEL_ID)
            'End If

            'If _filter.RANK <> 0 Then
            '    lst = lst.Where(Function(p) p.RANK = _filter.RANK)
            'End If

            'If _filter.ACTFLG <> "" Then
            '    lst = lst.Where(Function(p) p.ACTFLG.ToUpper.Contains(_filter.ACTFLG.ToUpper))
            'End If
            'lst = lst.OrderBy(Sorts)
            'Total = lst.Count
            'lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            'Return lst.ToList
            Return Nothing

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try


    End Function
    Public Function GetIncentiveRankIdIncludeDetail(ByVal _filter As IncentiveRankDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As IncentiveRankDTO

        Try

            Dim query = From i In Context.PA_INCENTIVE_RANK
                        Join p In Context.PA_SALARY_RANK On i.SAL_RANK_ID Equals p.ID
                        Join o In Context.PA_SALARY_LEVEL On p.SAL_LEVEL_ID Equals o.ID
                        Join q In Context.PA_SALARY_GROUP On o.SAL_GROUP_ID Equals q.ID
                        Join l In Context.OT_OTHER_LIST On i.SAL_INCENTIVE_ID Equals l.ID
                        Join t In Context.OT_OTHER_LIST_TYPE On l.TYPE_ID Equals t.ID
                        Where (i.ID = _filter.ID)
                        Select New IncentiveRankDTO With {
                                       .ID = i.ID,
                                       .SAL_GROUP_ID = i.SAL_GROUP_ID,
                                       .SAL_GROUP_NAME = q.NAME,
                                       .SAL_LEVEL_ID = i.SAL_LEVEL_ID,
                                       .SAL_LEVEL_NAME = o.NAME,
                                       .SAL_RANK_ID = i.SAL_RANK_ID,
                                       .RANK = p.RANK,
                                       .SAL_INCENTIVE_ID = i.SAL_INCENTIVE_ID,
                                       .SAL_INCENTIVE_NAME = l.NAME_VN,
                                       .EFFECT_DATE = i.EFFECT_DATE,
                                       .REMARK = p.REMARK,
                                       .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                                       .ORDERS = p.ORDERS,
                                       .CREATED_DATE = p.CREATED_DATE
                                   }
            Dim incentive = query.FirstOrDefault
            incentive.INCENTIVERANKDETAIL = (
                From p In Context.PA_INCENTIVE_RANK_DETAIL
                Where p.INCENTIVE_RANK_ID = incentive.ID
                Select New IncentiveRankDetailDTO With {
                    .ID = p.ID,
                    .INCENTIVE_RANK_ID = p.INCENTIVE_RANK_ID,
                        .FROM_TARGET = p.FROM_TARGET,
                        .TO_TARGET = p.TO_TARGET,
                        .AMOUNT = p.AMOUNT,
                        .INCENTIVE_PERCENT = p.INCENTIVE_PERCENT,
                        .INCENTIVE_AMOUNT = p.INCENTIVE_AMOUNT,
                        .REMARK = p.REMARK,
                        .ACTFLG = p.REMARK,
                        .ORDERS = p.ORDERS
                }
            ).OrderBy("TO_TARGET ASC").ToList()

            Return incentive

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try


    End Function

#End Region

#Region "Insert IncentiveRank"

    Public Function InsertIncentiveRankIncludeDetail(ByVal objIncentiveRank As IncentiveRankDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim iCount As Integer = 0
        Dim objIncentiveRankData As New PA_INCENTIVE_RANK
        Try
            objIncentiveRankData.ID = Utilities.GetNextSequence(Context, Context.PA_INCENTIVE_RANK.EntitySet.Name)
            objIncentiveRank.ID = objIncentiveRankData.ID
            objIncentiveRankData.SAL_GROUP_ID = objIncentiveRank.SAL_GROUP_ID
            objIncentiveRankData.SAL_LEVEL_ID = objIncentiveRank.SAL_LEVEL_ID
            objIncentiveRankData.SAL_RANK_ID = objIncentiveRank.SAL_RANK_ID
            objIncentiveRankData.SAL_INCENTIVE_ID = objIncentiveRank.SAL_INCENTIVE_ID
            objIncentiveRankData.EFFECT_DATE = objIncentiveRank.EFFECT_DATE
            objIncentiveRankData.REMARK = objIncentiveRank.REMARK
            objIncentiveRankData.ACTFLG = objIncentiveRank.ACTFLG
            objIncentiveRankData.ORDERS = objIncentiveRank.ORDERS
            Context.PA_INCENTIVE_RANK.AddObject(objIncentiveRankData)
            If objIncentiveRank.INCENTIVERANKDETAIL IsNot Nothing Then

                Dim objIncentiveDetail As New PA_INCENTIVE_RANK_DETAIL
                For Each objIncentiveRankDetail In objIncentiveRank.INCENTIVERANKDETAIL
                    objIncentiveDetail.ID = Utilities.GetNextSequence(Context, Context.PA_INCENTIVE_RANK_DETAIL.EntitySet.Name)
                    objIncentiveDetail.INCENTIVE_RANK_ID = objIncentiveRank.ID
                    objIncentiveDetail.FROM_TARGET = objIncentiveRankDetail.FROM_TARGET
                    objIncentiveDetail.TO_TARGET = objIncentiveRankDetail.TO_TARGET
                    objIncentiveDetail.AMOUNT = objIncentiveRankDetail.AMOUNT
                    objIncentiveDetail.INCENTIVE_PERCENT = objIncentiveRankDetail.INCENTIVE_PERCENT
                    objIncentiveDetail.INCENTIVE_AMOUNT = objIncentiveRankDetail.INCENTIVE_AMOUNT
                    objIncentiveDetail.REMARK = objIncentiveRankDetail.REMARK
                    objIncentiveDetail.ACTFLG = objIncentiveRankDetail.ACTFLG
                    objIncentiveDetail.ORDERS = objIncentiveRankDetail.ORDERS
                    Context.PA_INCENTIVE_RANK_DETAIL.AddObject(objIncentiveDetail)
                Next

            End If
            Context.SaveChanges(log)
            gID = objIncentiveRank.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function
    Public Function InsertIncentiveRank(ByVal objIncentiveRank As IncentiveRankDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim iCount As Integer = 0
        Dim objIncentiveRankData As New PA_INCENTIVE_RANK
        Try
            objIncentiveRankData.ID = Utilities.GetNextSequence(Context, Context.PA_INCENTIVE_RANK.EntitySet.Name)
            objIncentiveRankData.SAL_GROUP_ID = objIncentiveRank.SAL_GROUP_ID
            objIncentiveRankData.SAL_LEVEL_ID = objIncentiveRank.SAL_LEVEL_ID
            objIncentiveRankData.SAL_RANK_ID = objIncentiveRank.SAL_RANK_ID
            objIncentiveRankData.SAL_INCENTIVE_ID = objIncentiveRank.SAL_INCENTIVE_ID
            objIncentiveRankData.EFFECT_DATE = objIncentiveRank.EFFECT_DATE
            objIncentiveRankData.REMARK = objIncentiveRank.REMARK
            objIncentiveRankData.ACTFLG = objIncentiveRank.ACTFLG
            objIncentiveRankData.ORDERS = objIncentiveRank.ORDERS
            Context.PA_INCENTIVE_RANK.AddObject(objIncentiveRankData)
            Context.SaveChanges(log)
            gID = objIncentiveRankData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function
    Public Function InsertIncentiveRankDetail(ByVal objIncentiveRankDetail As IncentiveRankDetailDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim iCount As Integer = 0
        Dim objIncentiveDetail As New PA_INCENTIVE_RANK_DETAIL
        Try
            objIncentiveDetail.ID = Utilities.GetNextSequence(Context, Context.PA_INCENTIVE_RANK_DETAIL.EntitySet.Name)
            objIncentiveDetail.INCENTIVE_RANK_ID = objIncentiveRankDetail.INCENTIVE_RANK_ID
            objIncentiveDetail.FROM_TARGET = objIncentiveRankDetail.FROM_TARGET
            objIncentiveDetail.TO_TARGET = objIncentiveRankDetail.TO_TARGET
            objIncentiveDetail.AMOUNT = objIncentiveRankDetail.AMOUNT
            objIncentiveDetail.INCENTIVE_PERCENT = objIncentiveRankDetail.INCENTIVE_PERCENT
            objIncentiveDetail.INCENTIVE_AMOUNT = objIncentiveRankDetail.INCENTIVE_AMOUNT
            objIncentiveDetail.REMARK = objIncentiveRankDetail.REMARK
            objIncentiveDetail.ACTFLG = objIncentiveRankDetail.ACTFLG
            objIncentiveDetail.ORDERS = objIncentiveRankDetail.ORDERS
            Context.PA_INCENTIVE_RANK_DETAIL.AddObject(objIncentiveDetail)
            Context.SaveChanges(log)
            gID = objIncentiveDetail.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function
    Public Function InsertIncentiveListRankDetail(ByVal objIncentiveRankDetail As List(Of IncentiveRankDetailDTO),
                                   ByVal log As UserLog, ByRef gID As List(Of Decimal)) As Boolean
        Dim iCount As Integer = 0
        Dim objIncentiveDetail As New PA_INCENTIVE_RANK_DETAIL
        Try
            For Each obj As IncentiveRankDetailDTO In objIncentiveRankDetail
                objIncentiveDetail.ID = Utilities.GetNextSequence(Context, Context.PA_INCENTIVE_RANK_DETAIL.EntitySet.Name)
                objIncentiveDetail.INCENTIVE_RANK_ID = obj.INCENTIVE_RANK_ID
                objIncentiveDetail.FROM_TARGET = obj.FROM_TARGET
                objIncentiveDetail.TO_TARGET = obj.TO_TARGET
                objIncentiveDetail.AMOUNT = obj.AMOUNT
                objIncentiveDetail.INCENTIVE_PERCENT = obj.INCENTIVE_PERCENT
                objIncentiveDetail.INCENTIVE_AMOUNT = obj.INCENTIVE_AMOUNT
                objIncentiveDetail.REMARK = obj.REMARK
                objIncentiveDetail.ACTFLG = obj.ACTFLG
                objIncentiveDetail.ORDERS = obj.ORDERS
                Context.PA_INCENTIVE_RANK_DETAIL.AddObject(objIncentiveDetail)
                Context.SaveChanges(log)
                gID.Add(objIncentiveDetail.ID)
            Next

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

#End Region

#Region "Modify Incentive"

    Public Function ModifyIncentiveRank(ByVal objIncentiveRank As IncentiveRankDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objIncentiveRankData As New PA_INCENTIVE_RANK With {.ID = objIncentiveRank.ID}
        Try
            Context.PA_INCENTIVE_RANK.Attach(objIncentiveRankData)
            objIncentiveRankData.SAL_INCENTIVE_ID = objIncentiveRank.SAL_INCENTIVE_ID
            objIncentiveRankData.EFFECT_DATE = objIncentiveRank.EFFECT_DATE
            objIncentiveRankData.REMARK = objIncentiveRank.REMARK
            objIncentiveRankData.ORDERS = objIncentiveRank.ORDERS
            Context.SaveChanges(log)
            gID = objIncentiveRankData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function ModifyIncentiveRankDetail(ByVal objIncentiveRankDetail As IncentiveRankDetailDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objIncentiveRankDetailData As New PA_INCENTIVE_RANK_DETAIL With {
                .ID = objIncentiveRankDetail.ID,
                .INCENTIVE_RANK_ID = objIncentiveRankDetail.INCENTIVE_RANK_ID
            }
        Try
            Context.PA_INCENTIVE_RANK_DETAIL.Attach(objIncentiveRankDetailData)
            objIncentiveRankDetailData.FROM_TARGET = objIncentiveRankDetail.FROM_TARGET
            objIncentiveRankDetailData.TO_TARGET = objIncentiveRankDetail.TO_TARGET
            objIncentiveRankDetailData.AMOUNT = objIncentiveRankDetail.AMOUNT
            objIncentiveRankDetailData.INCENTIVE_PERCENT = objIncentiveRankDetail.INCENTIVE_PERCENT
            objIncentiveRankDetailData.INCENTIVE_AMOUNT = objIncentiveRankDetail.INCENTIVE_AMOUNT
            objIncentiveRankDetailData.REMARK = objIncentiveRankDetail.REMARK
            objIncentiveRankDetailData.ORDERS = objIncentiveRankDetail.ORDERS
            Context.SaveChanges(log)
            gID = objIncentiveRankDetailData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function
    Public Function ModifyIncentiveRankIncludeDetail(ByVal objIncentive As IncentiveRankDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Try
            Dim objIncentiveData = (From p In Context.PA_INCENTIVE_RANK Where objIncentive.ID = p.ID).FirstOrDefault
            objIncentiveData.EFFECT_DATE = objIncentive.EFFECT_DATE
            objIncentiveData.REMARK = objIncentive.REMARK
            objIncentiveData.ORDERS = objIncentive.ORDERS
            objIncentiveData.ACTFLG = objIncentive.ACTFLG

            If objIncentive.INCENTIVERANKDETAIL IsNot Nothing Then
                'Xoa detail cua incentive
                Dim obj = (From p In Context.PA_INCENTIVE_RANK_DETAIL Where p.INCENTIVE_RANK_ID = objIncentive.ID)
                For Each item In obj
                    Context.PA_INCENTIVE_RANK_DETAIL.DeleteObject(item)
                Next
                For Each item In objIncentive.INCENTIVERANKDETAIL
                    Dim detail As New PA_INCENTIVE_RANK_DETAIL
                    detail.ID = Utilities.GetNextSequence(Context, Context.PA_INCENTIVE_RANK_DETAIL.EntitySet.Name)
                    detail.INCENTIVE_RANK_ID = objIncentive.ID
                    detail.FROM_TARGET = item.FROM_TARGET
                    detail.TO_TARGET = item.TO_TARGET
                    detail.AMOUNT = item.AMOUNT
                    detail.INCENTIVE_PERCENT = item.INCENTIVE_PERCENT
                    detail.INCENTIVE_AMOUNT = item.INCENTIVE_AMOUNT
                    detail.REMARK = item.REMARK
                    detail.ACTFLG = item.ACTFLG
                    detail.ORDERS = item.ORDERS
                    Context.PA_INCENTIVE_RANK_DETAIL.AddObject(detail)
                Next
            End If
            Context.SaveChanges(log)
            gID = objIncentive.ID
            Return True
        Catch ex As Exception

        End Try
    End Function
#End Region

    Public Function ValidateIncentiveRank(ByVal _validate As IncentiveRankDTO)
        Dim query
        Try
            If _validate.ID <> 0 Then
                query = (From p In Context.PA_INCENTIVE_RANK
                         Where p.SAL_RANK_ID = _validate.SAL_RANK_ID _
                         And p.SAL_GROUP_ID = _validate.SAL_GROUP_ID _
                         And p.SAL_LEVEL_ID = _validate.SAL_LEVEL_ID _
                         And p.SAL_INCENTIVE_ID = _validate.SAL_INCENTIVE_ID _
                         And p.ID <> _validate.ID).SingleOrDefault
            Else
                query = (From p In Context.PA_INCENTIVE_RANK
                         Where p.SAL_RANK_ID = _validate.SAL_RANK_ID _
                         And p.SAL_GROUP_ID = _validate.SAL_GROUP_ID _
                         And p.SAL_LEVEL_ID = _validate.SAL_LEVEL_ID _
                         And p.SAL_INCENTIVE_ID = _validate.SAL_INCENTIVE_ID).FirstOrDefault
            End If
            Return (query Is Nothing)
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

#Region "Active/ Inactive IncentiveRank"

    Public Function ActiveIncentiveRank(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of PA_INCENTIVE_RANK)
        Dim lstDataDetail As List(Of PA_INCENTIVE_RANK_DETAIL)
        Dim parentID As Decimal = 0
        Try
            lstData = (From p In Context.PA_INCENTIVE_RANK Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                lstData(index).ACTFLG = bActive
                parentID = lstData(index).ID
                lstDataDetail = (From p In Context.PA_INCENTIVE_RANK_DETAIL Where p.INCENTIVE_RANK_ID = parentID).ToList()
                For Each item In lstDataDetail
                    Dim detail As New PA_INCENTIVE_RANK_DETAIL
                    detail.ACTFLG = bActive
                Next
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function ActiveIncentiveRankDetail(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of PA_INCENTIVE_RANK_DETAIL)
        Try
            lstData = (From p In Context.PA_INCENTIVE_RANK_DETAIL Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                lstData(index).ACTFLG = bActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function DeleteIncentiveRank(ByVal lstIncentiveRank As List(Of IncentiveRankDTO)) As Boolean
        Dim lstIncentiveRankData As List(Of PA_INCENTIVE_RANK)
        Dim lstIncentiveRankDataDetail As List(Of PA_INCENTIVE_RANK_DETAIL)
        Dim parentID As Decimal = 0
        Try
            Dim lstIDIncentiveRank As List(Of Decimal) = (From p In lstIncentiveRank.ToList Select p.ID).ToList
            lstIncentiveRankData = (From p In Context.PA_INCENTIVE_RANK Where lstIDIncentiveRank.Contains(p.ID)).ToList
            For idx = 0 To lstIncentiveRankData.Count - 1
                parentID = lstIncentiveRankData(idx).ID
                lstIncentiveRankDataDetail = (From p In Context.PA_INCENTIVE_RANK_DETAIL Where p.INCENTIVE_RANK_ID = parentID).ToList()
                For Each item In lstIncentiveRankDataDetail
                    Context.PA_INCENTIVE_RANK_DETAIL.DeleteObject(item)
                Next
                Context.PA_INCENTIVE_RANK.DeleteObject(lstIncentiveRankData(idx))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function DeleteIncentiveRankDetail(ByVal lstIncentiveRank() As IncentiveRankDetailDTO) As Boolean
        Dim lstIncentiveRankData As List(Of PA_INCENTIVE_RANK_DETAIL)
        Dim lstIdIncentiveRank As List(Of Decimal) = (From p In lstIncentiveRank.ToList Select p.ID).ToList
        Try
            lstIncentiveRankData = (From p In Context.PA_INCENTIVE_RANK_DETAIL Where lstIdIncentiveRank.Contains(p.ID)).ToList
            For idx = 0 To lstIncentiveRankData.Count - 1
                Context.PA_INCENTIVE_RANK_DETAIL.DeleteObject(lstIncentiveRankData(idx))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

#End Region

#End Region

End Class

