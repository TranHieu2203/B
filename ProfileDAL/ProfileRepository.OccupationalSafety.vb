Imports System.Reflection
Imports Framework.Data
Imports Framework.Data.System.Linq.Dynamic

Partial Class ProfileRepository

#Region "quản lý an toàn lao động"

    Public Function GetOccupationalSafety(ByVal _filter As OccupationalSafetyDTO, ByVal IsDissolve As Integer, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        ByVal UserLog As UserLog,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of OccupationalSafetyDTO)

        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = UserLog.Username,
                                           .P_ORGID = _filter.ORG_ID,
                                           .P_ISDISSOLVE = IsDissolve})
            End Using

            Dim query = From p In Context.HU_OCCUPATIONAL_SAFETY
                        From ot In Context.OT_OTHER_LIST.Where(Function(ot) ot.ID = p.REASON_ID).DefaultIfEmpty
                        From tt In Context.OT_OTHER_LIST_TYPE.Where(Function(tt) tt.CODE = "REASON" And tt.ID = ot.TYPE_ID).DefaultIfEmpty
                        From e In Context.HU_EMPLOYEE.Where(Function(e) p.EMPLOYEE_ID = e.ID)
                        From o In Context.HU_ORGANIZATION.Where(Function(o) e.ORG_ID = o.ID)
                        From t In Context.HU_TITLE.Where(Function(t) e.TITLE_ID = t.ID)
                        From se In Context.SE_CHOSEN_ORG.Where(Function(se) se.ORG_ID = o.ID).DefaultIfEmpty
                        Where (se.USERNAME = UserLog.Username.ToUpper)
                        Order By e.EMPLOYEE_CODE

            ' lọc điều kiện
            If Not _filter.IS_TERMINATE Then
                query = query.Where(Function(p) p.e.WORK_STATUS <> 257 Or (p.e.WORK_STATUS = 257 And p.e.TER_LAST_DATE >= Date.Now) Or p.e.WORK_STATUS Is Nothing)
            End If
            If _filter.FROM_DATE_SEARCH.HasValue Then
                query = query.Where(Function(p) p.p.DATE_OF_ACCIDENT >= _filter.FROM_DATE_SEARCH)
            End If
            If _filter.TO_DATE_SEARCH.HasValue Then
                query = query.Where(Function(p) p.p.DATE_OF_ACCIDENT <= _filter.TO_DATE_SEARCH)
            End If

            If _filter.EMPLOYEE_CODE IsNot Nothing Then
                query = query.Where(Function(p) p.e.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper) _
                                        Or p.e.FULLNAME_VN.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If
            If _filter.EMPLOYEE_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.e.FULLNAME_VN.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If
            If _filter.TITLE_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.t.NAME_VN.ToUpper.Contains(_filter.TITLE_NAME.ToUpper))
            End If
            If _filter.ORG_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.o.NAME_VN.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If
            If _filter.DATE_OF_ACCIDENT.HasValue Then
                query = query.Where(Function(p) p.p.DATE_OF_ACCIDENT = _filter.DATE_OF_ACCIDENT)
            End If
            If _filter.REASON_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.ot.NAME_VN.ToUpper.Contains(_filter.REASON_NAME.ToUpper))
            End If
            If _filter.HOLIDAY_ACCIDENTS.HasValue Then
                query = query.Where(Function(p) p.p.HOLIDAY_ACCIDENTS = _filter.HOLIDAY_ACCIDENTS)
            End If
            If _filter.DESCRIBED_INCIDENT IsNot Nothing Then
                query = query.Where(Function(p) p.p.DESCRIBED_INCIDENT.ToUpper.Contains(_filter.DESCRIBED_INCIDENT.ToUpper))
            End If
            If _filter.EXTENT_OF_INJURY IsNot Nothing Then
                query = query.Where(Function(p) p.p.EXTENT_OF_INJURY.ToUpper.Contains(_filter.EXTENT_OF_INJURY.ToUpper))
            End If
            If _filter.THE_COST_OF_ACCIDENTS.HasValue Then
                query = query.Where(Function(p) p.p.THE_COST_OF_ACCIDENTS = _filter.THE_COST_OF_ACCIDENTS)
            End If
            If _filter.SDESC IsNot Nothing Then
                query = query.Where(Function(p) p.p.SDESC.ToUpper.Contains(_filter.SDESC.ToUpper))
            End If

            ' select thuộc tính
            Dim wel = query.Select(Function(p) New OccupationalSafetyDTO With {
                                       .ID = p.p.ID,
                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                                       .ORG_ID = p.o.ID,
                                       .ORG_NAME = p.o.NAME_VN,
                                       .TITLE_NAME = p.t.NAME_VN,
                                        .DATE_OF_ACCIDENT = p.p.DATE_OF_ACCIDENT,
                                        .REASON_ID = p.p.REASON_ID,
                                        .REASON_NAME = p.ot.NAME_VN,
                                        .HOLIDAY_ACCIDENTS = p.p.HOLIDAY_ACCIDENTS,
                                        .DESCRIBED_INCIDENT = p.p.DESCRIBED_INCIDENT,
                                        .EXTENT_OF_INJURY = p.p.EXTENT_OF_INJURY,
                                        .THE_COST_OF_ACCIDENTS = p.p.THE_COST_OF_ACCIDENTS,
                                        .SDESC = p.p.SDESC,
                                        .WORK_STATUS = p.e.WORK_STATUS,
                                       .TER_LAST_DATE = p.e.TER_EFFECT_DATE,
                                       .CREATED_DATE = p.p.CREATED_DATE})
            wel = wel.OrderBy(Sorts)
            Total = wel.Count
            wel = wel.Skip(PageIndex * PageSize).Take(PageSize)
            Return wel.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function GetOccupationalSafetyById(ByVal Id As Integer
                                        ) As OccupationalSafetyDTO

        Try
            Dim query = From p In Context.HU_OCCUPATIONAL_SAFETY
                        From ot In Context.OT_OTHER_LIST.Where(Function(ot) ot.ID = p.REASON_ID).DefaultIfEmpty
                        From tt In Context.OT_OTHER_LIST_TYPE.Where(Function(tt) tt.CODE = "REASON" And tt.ID = ot.TYPE_ID).DefaultIfEmpty
                        From e In Context.HU_EMPLOYEE.Where(Function(e) p.EMPLOYEE_ID = e.ID)
                        From o In Context.HU_ORGANIZATION.Where(Function(o) e.ORG_ID = o.ID)
                        From t In Context.HU_TITLE.Where(Function(t) e.TITLE_ID = t.ID)
                        Where p.ID = Id
                        Order By p.EMPLOYEE_ID

            ' select thuộc tính
            Dim wel = query.Select(Function(p) New OccupationalSafetyDTO With {
                                       .ID = p.p.ID,
                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                                       .ORG_ID = p.o.ID,
                                       .ORG_NAME = p.o.NAME_VN,
                                       .TITLE_NAME = p.t.NAME_VN,
                                        .DATE_OF_ACCIDENT = p.p.DATE_OF_ACCIDENT,
                                        .REASON_ID = p.p.REASON_ID,
                                        .HOLIDAY_ACCIDENTS = p.p.HOLIDAY_ACCIDENTS,
                                        .DESCRIBED_INCIDENT = p.p.DESCRIBED_INCIDENT,
                                        .EXTENT_OF_INJURY = p.p.EXTENT_OF_INJURY,
                                        .THE_COST_OF_ACCIDENTS = p.p.THE_COST_OF_ACCIDENTS,
                                        .SDESC = p.p.SDESC,
                                        .WORK_STATUS = p.e.WORK_STATUS,
                                       .CREATED_DATE = p.p.CREATED_DATE})
            Return wel.SingleOrDefault
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function InsertOccupationalSafety(ByVal objTitle As OccupationalSafetyDTO,
                                   ByVal log As UserLog) As Boolean
        Dim iCount As Integer = 0
        Try
            Dim objTitleData As New HU_OCCUPATIONAL_SAFETY
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.HU_OCCUPATIONAL_SAFETY.EntitySet.Name)
            objTitleData.EMPLOYEE_ID = objTitle.EMPLOYEE_ID
            objTitleData.DATE_OF_ACCIDENT = objTitle.DATE_OF_ACCIDENT
            objTitleData.REASON_ID = objTitle.REASON_ID
            objTitleData.HOLIDAY_ACCIDENTS = objTitle.HOLIDAY_ACCIDENTS
            objTitleData.DESCRIBED_INCIDENT = objTitle.DESCRIBED_INCIDENT
            objTitleData.EXTENT_OF_INJURY = objTitle.EXTENT_OF_INJURY
            objTitleData.THE_COST_OF_ACCIDENTS = objTitle.THE_COST_OF_ACCIDENTS
            objTitleData.SDESC = objTitle.SDESC
            Context.HU_OCCUPATIONAL_SAFETY.AddObject(objTitleData)
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function ModifyOccupationalSafety(ByVal objTitle As OccupationalSafetyDTO,
                                   ByVal log As UserLog) As Boolean
        Dim objTitleData As New HU_OCCUPATIONAL_SAFETY
        Try
            objTitleData = (From p In Context.HU_OCCUPATIONAL_SAFETY Where p.ID = objTitle.ID).SingleOrDefault
            Context.HU_OCCUPATIONAL_SAFETY.Attach(objTitleData)

            objTitleData.EMPLOYEE_ID = objTitle.EMPLOYEE_ID
            objTitleData.DATE_OF_ACCIDENT = objTitle.DATE_OF_ACCIDENT
            objTitleData.REASON_ID = objTitle.REASON_ID
            objTitleData.HOLIDAY_ACCIDENTS = objTitle.HOLIDAY_ACCIDENTS
            objTitleData.DESCRIBED_INCIDENT = objTitle.DESCRIBED_INCIDENT
            objTitleData.EXTENT_OF_INJURY = objTitle.EXTENT_OF_INJURY
            objTitleData.THE_COST_OF_ACCIDENTS = objTitle.THE_COST_OF_ACCIDENTS
            objTitleData.SDESC = objTitle.SDESC

            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function DeleteOccupationalSafety(ByVal lstOccupationalSafety() As OccupationalSafetyDTO,
                                   ByVal log As UserLog) As Boolean
        Dim lstOccupationalSafetyData As List(Of HU_OCCUPATIONAL_SAFETY)
        Dim lstIDOccupationalSafety As List(Of Decimal) = (From p In lstOccupationalSafety.ToList Select p.ID).ToList
        lstOccupationalSafetyData = (From p In Context.HU_OCCUPATIONAL_SAFETY Where lstIDOccupationalSafety.Contains(p.ID)).ToList
        For index = 0 To lstOccupationalSafetyData.Count - 1
            Context.HU_OCCUPATIONAL_SAFETY.DeleteObject(lstOccupationalSafetyData(index))
        Next
        Context.SaveChanges(log)
        Return True
    End Function

#End Region

End Class
