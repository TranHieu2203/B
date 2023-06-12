
Imports System.Reflection
Imports Framework.Data
Imports Framework.Data.System.Linq.Dynamic

Partial Class ProfileRepository


    Public Function GetLabourProtectionMng(ByVal _filter As LabourProtectionMngDTO,
                                           ByVal IsDissolve As Integer,
                                           ByVal PageIndex As Integer,
                                            ByVal PageSize As Integer,
                                            ByRef Total As Integer,
                                            ByVal UserLog As UserLog,
                                            Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of LabourProtectionMngDTO)

        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = UserLog.Username,
                                           .P_ORGID = _filter.ORG_ID,
                                           .P_ISDISSOLVE = IsDissolve})
            End Using

            Dim query = From p In Context.HU_LABOURPROTECTION_MNG
                        From l In Context.HU_LABOURPROTECTION.Where(Function(l) l.ID = p.LABOURPROTECTION_ID)
                        From size In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.LABOUR_SIZE_ID).DefaultIfEmpty
                        From e In Context.HU_EMPLOYEE.Where(Function(e) p.EMPLOYEE_ID = e.ID)
                        From o In Context.HU_ORGANIZATION.Where(Function(o) e.ORG_ID = o.ID)
                        From t In Context.HU_TITLE.Where(Function(t) e.TITLE_ID = t.ID)
                        From staffrank In Context.HU_STAFF_RANK.Where(Function(staffrank) e.STAFF_RANK_ID = staffrank.ID).DefaultIfEmpty
                        From se In Context.SE_CHOSEN_ORG.Where(Function(se) se.ORG_ID = o.ID And _
                                                                  se.USERNAME = UserLog.Username.ToUpper)
                        Order By e.EMPLOYEE_CODE

            ' lọc điều kiện
            If Not _filter.IS_TERMINATE Then
                query = query.Where(Function(p) p.e.WORK_STATUS <> 257 Or (p.e.WORK_STATUS = 257 And p.e.TER_LAST_DATE >= Date.Now) Or p.e.WORK_STATUS Is Nothing)
            End If
            If _filter.FROM_DATE_SEARCH.HasValue Then
                query = query.Where(Function(p) p.p.DAYS_ALLOCATED >= _filter.FROM_DATE_SEARCH)
            End If
            If _filter.TO_DATE_SEARCH.HasValue Then
                query = query.Where(Function(p) p.p.DAYS_ALLOCATED <= _filter.TO_DATE_SEARCH)
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
            If _filter.STAFF_RANK_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.staffrank.NAME.ToUpper.Contains(_filter.STAFF_RANK_NAME.ToUpper))
            End If
            If _filter.LABOURPROTECTION_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.l.NAME.ToUpper.Contains(_filter.LABOURPROTECTION_NAME.ToUpper))
            End If
            If _filter.LABOUR_SIZE_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.size.NAME_VN.ToUpper.Contains(_filter.LABOUR_SIZE_NAME.ToUpper))
            End If
            If _filter.QUANTITY.HasValue Then
                query = query.Where(Function(p) p.p.QUANTITY = _filter.QUANTITY)
            End If
            If _filter.DAYS_ALLOCATED.HasValue Then
                query = query.Where(Function(p) p.p.DAYS_ALLOCATED = _filter.DAYS_ALLOCATED)
            End If
            If _filter.RETRIEVE_DATE.HasValue Then
                query = query.Where(Function(p) p.p.RETRIEVE_DATE = _filter.RETRIEVE_DATE)
            End If
            If _filter.DEPOSIT.HasValue Then
                query = query.Where(Function(p) p.p.DEPOSIT = _filter.DEPOSIT)
            End If
            'If _filter.IS_RETRIEVED.HasValue Then
            '    query = query.Where(Function(p) p.p.IS_RETRIEVED = _filter.IS_RETRIEVED)
            'End If
            If _filter.RECOVERY_DATE.HasValue Then
                query = query.Where(Function(p) p.p.RECOVERY = _filter.RECOVERY_DATE)
            End If
            If _filter.SDESC IsNot Nothing Then
                query = query.Where(Function(p) p.p.SDESC.ToUpper.Contains(_filter.SDESC.ToUpper))
            End If
            ' select thuộc tính
            Dim wel = query.Select(Function(p) New LabourProtectionMngDTO With {
                                       .ID = p.p.ID,
                                       .LABOURPROTECTION_ID = p.p.LABOURPROTECTION_ID,
                                       .LABOURPROTECTION_NAME = p.l.NAME,
                                       .UNIT_PRICE = p.l.UNIT_PRICE,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                                       .ORG_NAME = p.o.NAME_VN,
                                       .ORG_DESC = p.o.DESCRIPTION_PATH,
                                       .TITLE_NAME = p.t.NAME_VN,
                                       .STAFF_RANK_NAME = p.staffrank.NAME,
                                       .QUANTITY = p.p.QUANTITY,
                                       .DAYS_ALLOCATED = p.p.DAYS_ALLOCATED,
                                       .LABOUR_SIZE_ID = p.p.LABOUR_SIZE_ID,
                                       .LABOUR_SIZE_NAME = p.size.NAME_VN,
                                       .DEPOSIT = p.p.DEPOSIT,
                                       .RETRIEVE_DATE = p.p.RETRIEVE_DATE,
                                       .RETRIEVED = p.p.RETRIEVED,
                                       .IS_RETRIEVED = p.p.RETRIEVED,
                                       .RECOVERY_DATE = p.p.RECOVERY,
                                       .INDEMNITY = p.p.INDEMNITY,
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

    Public Function GetLabourProtectionMngById(ByVal Id As Integer
                                        ) As LabourProtectionMngDTO
        Dim LabourProtectionMng As LabourProtectionMngDTO
        Try
            Dim query = From p In Context.HU_LABOURPROTECTION_MNG
                        From l In Context.HU_LABOURPROTECTION.Where(Function(l) l.ID = p.LABOURPROTECTION_ID)
                        From e In Context.HU_EMPLOYEE.Where(Function(e) p.EMPLOYEE_ID = e.ID)
                        From o In Context.HU_ORGANIZATION.Where(Function(o) e.ORG_ID = o.ID)
                        From t In Context.HU_TITLE.Where(Function(t) e.TITLE_ID = t.ID)
                        Where p.ID = Id
                        Order By p.EMPLOYEE_ID

            ' select thuộc tính
            Dim wel = query.Select(Function(p) New LabourProtectionMngDTO With {
                                       .ID = p.p.ID,
                                       .LABOURPROTECTION_ID = p.p.LABOURPROTECTION_ID,
                                       .EMPLOYEE_ID = p.e.ID,
                                       .LABOURPROTECTION_NAME = p.l.NAME,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                                       .ORG_NAME = p.o.NAME_VN,
                                       .ORG_DESC = p.o.DESCRIPTION_PATH,
                                       .TITLE_NAME = p.t.NAME_VN,
                                       .QUANTITY = p.p.QUANTITY,
                                       .RECOVERY_DATE = p.p.RECOVERY,
                                       .LABOUR_SIZE_ID = p.p.LABOUR_SIZE_ID,
                                       .RETRIEVE_DATE = p.p.RETRIEVE_DATE,
                                       .RETRIEVED = p.p.RETRIEVED,
                                       .UNIT_PRICE = p.l.UNIT_PRICE,
                                       .DAYS_ALLOCATED = p.p.DAYS_ALLOCATED,
                                       .WORK_STATUS = p.e.WORK_STATUS,
                                        .SDESC = p.p.SDESC
                                       })
            LabourProtectionMng = wel.FirstOrDefault
            Return LabourProtectionMng
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function InsertLabourProtectionMng(ByVal lstLabourProtectionMng As List(Of LabourProtectionMngDTO),
                                   ByVal log As UserLog) As Boolean
        Dim iCount As Integer = 0
        Try
            For idx = 0 To lstLabourProtectionMng.Count - 1
                Dim item As LabourProtectionMngDTO = lstLabourProtectionMng(idx)
                Dim objLabourProtectionMngData As New HU_LABOURPROTECTION_MNG
                objLabourProtectionMngData.ID = Utilities.GetNextSequence(Context, Context.HU_LABOURPROTECTION_MNG.EntitySet.Name)
                objLabourProtectionMngData.LABOURPROTECTION_ID = item.LABOURPROTECTION_ID
                objLabourProtectionMngData.EMPLOYEE_ID = item.EMPLOYEE_ID
                objLabourProtectionMngData.QUANTITY = item.QUANTITY
                objLabourProtectionMngData.RECOVERY = item.RECOVERY_DATE
                objLabourProtectionMngData.LABOUR_SIZE_ID = item.LABOUR_SIZE_ID
                objLabourProtectionMngData.DEPOSIT = item.DEPOSIT
                objLabourProtectionMngData.DAYS_ALLOCATED = item.DAYS_ALLOCATED
                objLabourProtectionMngData.RETRIEVE_DATE = item.RETRIEVE_DATE
                objLabourProtectionMngData.RETRIEVED = item.RETRIEVED
                objLabourProtectionMngData.SDESC = item.SDESC
                Context.HU_LABOURPROTECTION_MNG.AddObject(objLabourProtectionMngData)
            Next

            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function ModifyLabourProtectionMng(ByVal lstLabourProtectionMng As List(Of LabourProtectionMngDTO),
                                   ByVal log As UserLog) As Boolean
        Dim lstID As List(Of Decimal) = (From p In lstLabourProtectionMng Select p.ID.Value).ToList
        Dim lstLabourProtectionMngData As New List(Of HU_LABOURPROTECTION_MNG)
        Try
            lstLabourProtectionMngData = (From p In Context.HU_LABOURPROTECTION_MNG Where lstID.Contains(p.ID)).ToList

            For idx = 0 To lstLabourProtectionMngData.Count - 1
                lstLabourProtectionMngData(idx).ID = lstLabourProtectionMng(idx).ID
                lstLabourProtectionMngData(idx).LABOURPROTECTION_ID = lstLabourProtectionMng(idx).LABOURPROTECTION_ID
                lstLabourProtectionMngData(idx).EMPLOYEE_ID = lstLabourProtectionMng(idx).EMPLOYEE_ID
                lstLabourProtectionMngData(idx).QUANTITY = lstLabourProtectionMng(idx).QUANTITY
                lstLabourProtectionMngData(idx).LABOUR_SIZE_ID = lstLabourProtectionMng(idx).LABOUR_SIZE_ID
                lstLabourProtectionMngData(idx).DAYS_ALLOCATED = lstLabourProtectionMng(idx).DAYS_ALLOCATED
                lstLabourProtectionMngData(idx).DEPOSIT = lstLabourProtectionMng(idx).DEPOSIT
                lstLabourProtectionMngData(idx).RETRIEVE_DATE = lstLabourProtectionMng(idx).RETRIEVE_DATE
                lstLabourProtectionMngData(idx).RETRIEVED = lstLabourProtectionMng(idx).RETRIEVED
                lstLabourProtectionMngData(idx).SDESC = lstLabourProtectionMng(idx).SDESC
                lstLabourProtectionMngData(idx).RECOVERY = lstLabourProtectionMng(idx).RECOVERY_DATE
                lstLabourProtectionMngData(idx).MODIFIED_DATE = DateTime.Now
                lstLabourProtectionMngData(idx).MODIFIED_BY = log.Username
                lstLabourProtectionMngData(idx).MODIFIED_LOG = log.ComputerName
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function DeleteLabourProtectionMng(ByVal lstLabourProtectionMng() As LabourProtectionMngDTO,
                                   ByVal log As UserLog) As Boolean
        Dim lstLabourProtectionMngData As List(Of HU_LABOURPROTECTION_MNG)
        Dim lstIDLabourProtectionMng As List(Of Decimal) = (From p In lstLabourProtectionMng.ToList Select p.ID.Value).ToList
        lstLabourProtectionMngData = (From p In Context.HU_LABOURPROTECTION_MNG Where lstIDLabourProtectionMng.Contains(p.ID)).ToList
        For index = 0 To lstLabourProtectionMngData.Count - 1
            Context.HU_LABOURPROTECTION_MNG.DeleteObject(lstLabourProtectionMngData(index))
        Next
        Context.SaveChanges(log)
        Return True
    End Function


End Class
