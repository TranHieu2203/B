Imports Framework.Data
Imports Framework.Data.System.Linq.Dynamic

Partial Public Class TrainingRepository
    Inherits RepositoryBase

    Private _ctx As TrainingContext

    Public ReadOnly Property Context As TrainingContext
        Get
            If _ctx Is Nothing Then
                _ctx = New TrainingContext
                _ctx.ContextOptions.LazyLoadingEnabled = True
            End If
            Return _ctx
        End Get
    End Property

#Region "Hoadm"

#Region "Lấy data danh mục Combo"

    Public Function GetOtherList(ByVal sType As String, ByVal sLang As String, ByVal isBlank As Boolean) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_OTHER_LIST",
                                           New With {.P_TYPE = sType,
                                                     .P_ISBLANK = isBlank,
                                                     .P_LANG = sLang,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetTrCertificateList(dGroupID As Decimal, ByVal sLang As String, ByVal isBlank As Boolean) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_TR_CERTIFICATE",
                                           New With {.P_GROUP_ID = dGroupID,
                                                     .P_ISBLANK = isBlank,
                                                     .P_LANG = sLang,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetTrCenterList(ByVal sLang As String, ByVal isBlank As Boolean) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_TR_CENTER",
                                           New With {.P_ISBLANK = isBlank,
                                                     .P_LANG = sLang,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetTrPlanByYearOrg(ByVal isBlank As Boolean, ByVal dYear As Decimal, ByVal dOrg As Decimal, ByVal log As UserLog) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_TRAINING.GET_TR_REQUEST_BY_YEARORG",
                                           New With {.P_ISBLANK = isBlank,
                                                     .P_YEAR = dYear,
                                                     .P_ORGID = dOrg,
                                                     .P_USERNAME = log.Username,
                                                     .P_CUR = cls.OUT_CURSOR})
                Return dtData
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetTrPlanByYearOrg2(ByVal isBlank As Boolean, ByVal dYear As Decimal, ByVal dOrg As Decimal, ByVal log As UserLog, Optional ByVal isIrregularly As Boolean = False) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_TRAINING.GET_TR_PLAN_BY_YEARORG2",
                                           New With {.P_ISBLANK = isBlank,
                                                     .P_YEAR = dYear,
                                                     .P_ORGID = dOrg,
                                                     .P_USERNAME = log.Username,
                                                     .P_Irregularly = isIrregularly,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetTrLectureList(ByVal isLocal As Decimal, ByVal isBlank As Boolean) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_TR_LECTURE",
                                           New With {.P_ISBLANK = isBlank,
                                                     .P_ISLOCAL = isLocal,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetHuProvinceList(ByVal isBlank As Boolean) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_HU_PROVINCE",
                                           New With {.P_ISBLANK = isBlank,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetHuDistrictList(ByVal provinceID As Decimal, ByVal isBlank As Boolean) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_HU_DISTRICT",
                                           New With {.P_ISBLANK = isBlank,
                                                     .P_PROVINCE_ID = provinceID,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetHuContractTypeList(ByVal isBlank As Boolean) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_CONTRACT_TYPE",
                                           New With {.P_ISBLANK = isBlank,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetTrProgramByYear(ByVal isBlank As Boolean, ByVal dYear As Decimal) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_TR_PROGRAM_BY_YEAR",
                                           New With {.P_ISBLANK = isBlank,
                                                     .P_YEAR = dYear,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetTrCriteriaGroupList(ByVal isBlank As Boolean) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_TR_CRITERIA_GROUP",
                                           New With {.P_ISBLANK = isBlank,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetTrAssFormList(ByVal isBlank As Boolean) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_TR_ASS_FORM",
                                           New With {.P_ISBLANK = isBlank,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetTrChooseProgramFormByYear(ByVal isBlank As Boolean, ByVal dYear As Decimal) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_TR_CHOOSE_PROGRAM_FORM",
                                           New With {.P_ISBLANK = isBlank,
                                                     .P_YEAR = dYear,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetTrProgramById(ByVal id As Decimal) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_TR_PROGRAM_BY_ID",
                                           New With {.P_PROGRAM_ID = id,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "CostCenter"

    Public Function GetCostCenter(ByVal _filter As CostCenterDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CostCenterDTO)

        Try
            Dim query = From p In Context.TR_COST_CENTER

            Dim lst = query.Select(Function(p) New CostCenterDTO With {
                                       .ID = p.ID,
                                       .CODE = p.CODE,
                                       .NAME = p.NAME,
                                       .ACTFLG = p.ACTFLG,
                                       .CREATED_DATE = p.CREATED_DATE})
            If _filter.CODE <> "" Then
                lst = lst.Where(Function(p) p.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
            End If
            If _filter.NAME <> "" Then
                lst = lst.Where(Function(p) p.NAME.ToUpper.Contains(_filter.NAME.ToUpper))
            End If
            If _filter.ACTFLG IsNot Nothing Then
                lst = lst.Where(Function(p) p.ACTFLG = _filter.ACTFLG)
            End If
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetCostCenter")
            Throw ex
        End Try

    End Function

    Public Function InsertCostCenter(ByVal objCostCenter As CostCenterDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objCostCenterData As New TR_COST_CENTER
        Try
            objCostCenterData.ID = Utilities.GetNextSequence(Context, Context.TR_COST_CENTER.EntitySet.Name)
            objCostCenterData.CODE = objCostCenter.CODE.Trim
            objCostCenterData.NAME = objCostCenter.NAME.Trim
            Context.TR_COST_CENTER.AddObject(objCostCenterData)
            Context.SaveChanges(log)
            gID = objCostCenterData.ID
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".InsertCostCenter")
            Throw ex
        End Try
    End Function

    Public Function ValidateCostCenter(ByVal _validate As CostCenterDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.TR_COST_CENTER
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.TR_COST_CENTER
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If

                Return (query Is Nothing)
            End If
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".ValidateCostCenter")
            Throw ex
        End Try
    End Function

    Public Function ModifyCostCenter(ByVal objCostCenter As CostCenterDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objCostCenterData As New TR_COST_CENTER With {.ID = objCostCenter.ID}
        Try
            Context.TR_COST_CENTER.Attach(objCostCenterData)
            objCostCenterData.CODE = objCostCenter.CODE.Trim
            objCostCenterData.NAME = objCostCenter.NAME.Trim
            Context.SaveChanges(log)
            gID = objCostCenterData.ID
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".ModifyCostCenter")
            Throw ex
        End Try

    End Function

    Public Function ActiveCostCenter(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As Boolean) As Boolean
        Dim lstData As List(Of TR_COST_CENTER)
        Try
            lstData = (From p In Context.TR_COST_CENTER Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                lstData(index).ACTFLG = bActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            Throw ex
        End Try

    End Function


    Public Function DeleteCostCenter(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstCostCenterData As List(Of TR_COST_CENTER)
        Try
            lstCostCenterData = (From p In Context.TR_COST_CENTER Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstCostCenterData.Count - 1
                Context.TR_COST_CENTER.DeleteObject(lstCostCenterData(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteCostCenter")
            Throw ex
        End Try

    End Function

#End Region

#End Region

#Region " NhatHV Get List"
    Public Function GetTitleByList(ByVal sLang As String, ByVal isBlank As Boolean) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_TITLE",
                                           New With {.P_ISBLANK = isBlank,
                                                     .P_LANG = sLang,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetCourseByList(ByVal sLang As String, ByVal isBlank As Boolean) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_TR_COURSE",
                                           New With {.P_ISBLANK = isBlank,
                                                     .P_LANG = sLang,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region
End Class
